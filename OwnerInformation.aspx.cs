using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class OwnerInformation : ClosedPage
{
	//protected SqlConnection Connection = new SqlConnection (ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
	protected SqlDataAdapter MainAdapter;
	protected SqlDataAdapter CountriesAdapter;
	protected SqlDataAdapter AgentsAdapter;
	protected DataSet MainDataSet = new DataSet ();

	protected void Page_Load(object sender, System.EventArgs e)
	{
		//CommonFunctions.Connection.Open ();

		MainAdapter = CommonFunctions.PrepareAdapter (CommonFunctions.GetConnection(), "SELECT * FROM Users WHERE ID = @UserID", SqlDbType.Int);

		CountriesAdapter = CommonFunctions.PrepareAdapter (CommonFunctions.GetConnection(), "SELECT DISTINCT(Country) AS Country " +
			"FROM Users WHERE (IfAgent = 1) AND (LEN(Country) > 0) AND NOT (ID = @UserID)", SqlDbType.Int);

        AgentsAdapter = CommonFunctions.PrepareAdapter(CommonFunctions.GetConnection(), "SELECT -1 AS ID, 'Not referred by anyone' AS FullName " +
			"UNION SELECT ID, LastName + ', ' + FirstName AS FullName FROM Users WHERE (Country = @Country) AND" +
			" (IfAgent = 1) AND NOT (ID = @UserID)", SqlDbType.NVarChar, 300, SqlDbType.Int);

		IfAdmin.Visible = AuthenticationManager.IfAdmin && (AuthenticationManager.UserID != userid);
		IfAdminLabel.Visible = AuthenticationManager.IfAdmin && (AuthenticationManager.UserID != userid);

		MainAdapter.SelectCommand.Parameters["@UserID"].Value = userid;
		//lock (CommonFunctions.Connection)
			if (MainAdapter.Fill (MainDataSet, "Users") == 0)
				Response.Redirect (CommonFunctions.PrepareURL ("InternalError.aspx"), true);

        //CountryRequired.Enabled = (MainDataSet.Tables["Users"].Rows[0]["IfAgent"] is bool) &&
        //    (bool)MainDataSet.Tables["Users"].Rows[0]["IfAgent"];
            DBConnection obj = new DBConnection();
            try
            {
                if (!IsPostBack)
                {
                    DataTable dt = VADBCommander.CountyDistinctSimpleList();
                    ddlCountries.DataSource = dt;
                    //ddlCountries.DataTextField = "country";
                    //ddlCountries.DataValueField = "country";

                    if (ddlCountries.Items.Contains(new ListItem(MainDataSet.Tables["Users"].Rows[0]["country"].ToString(),
                        MainDataSet.Tables["Users"].Rows[0]["country"].ToString())))
                    {
                        ddlCountries.SelectedValue = MainDataSet.Tables["Users"].Rows[0]["country"].ToString();
                    }
                    //ddlCountries.DataBind();
                }
                
            }
            catch (Exception ex) { Response.Write(ex.Message); }
            finally { obj.CloseConnection(); }


		if (!IsPostBack)
		{
			EmailAddress.Text = MainDataSet.Tables["Users"].Rows[0]["Email"].ToString ();
			FirstName.Text = MainDataSet.Tables["Users"].Rows[0]["FirstName"].ToString ();
			LastName.Text = MainDataSet.Tables["Users"].Rows[0]["LastName"].ToString ();
			CompanyName.Text = MainDataSet.Tables["Users"].Rows[0]["CompanyName"].ToString ();
			Address.Text = MainDataSet.Tables["Users"].Rows[0]["Address"].ToString ();
			City.Text = MainDataSet.Tables["Users"].Rows[0]["City"].ToString ();
			State.Text = MainDataSet.Tables["Users"].Rows[0]["State"].ToString ();
			Zip.Text = MainDataSet.Tables["Users"].Rows[0]["Zip"].ToString ();

            //foreach (ListItem li in ddlCountries.Items)
            //{
            //    if (li.Value.ToLower() == MainDataSet.Tables["Users"].Rows[0]["Country"].ToString().ToLower())
            //    {
            //        ddlCountries.SelectedValue = MainDataSet.Tables["Users"].Rows[0]["Country"].ToString();                    
            //    }               
            //}                
            
			PrimaryTelephone.Text = MainDataSet.Tables["Users"].Rows[0]["PrimaryTelephone"].ToString ();
			EveningTelephone.Text = MainDataSet.Tables["Users"].Rows[0]["EveningTelephone"].ToString ();
			DaytimeTelephone.Text = MainDataSet.Tables["Users"].Rows[0]["DaytimeTelephone"].ToString ();
			MobileTelephone.Text = MainDataSet.Tables["Users"].Rows[0]["MobileTelephone"].ToString ();
			Website.Text = MainDataSet.Tables["Users"].Rows[0]["Website"].ToString ();
			Registered.Text = MainDataSet.Tables["Users"].Rows[0]["Registered"].ToString ();
			PayTravelAgents.Checked = MainDataSet.Tables["Users"].Rows[0]["IfPayTravelAgents"] is bool ?
				(bool)MainDataSet.Tables["Users"].Rows[0]["IfPayTravelAgents"] : false;
			ReservationSame.Checked = MainDataSet.Tables["Users"].Rows[0]["IfReservationSame"] is bool ?
				(bool)MainDataSet.Tables["Users"].Rows[0]["IfReservationSame"] : false;
			ReservationNotSame.Checked = !ReservationSame.Checked;
			ReservationEmail.Text = MainDataSet.Tables["Users"].Rows[0]["ReservationEmail"].ToString ();
			ReservationFirstName.Text = MainDataSet.Tables["Users"].Rows[0]["ReservationFirstName"].ToString ();
			ReservationLastName.Text = MainDataSet.Tables["Users"].Rows[0]["ReservationLastName"].ToString ();
			if (IfAdmin.Visible)
				IfAdmin.SelectedIndex = (MainDataSet.Tables["Users"].Rows[0]["IfAdmin"] is bool) &&
					(bool)MainDataSet.Tables["Users"].Rows[0]["IfAdmin"] ? 0 : 1;

			ReservationSame_CheckedChanged (sender, e);

			CountriesAdapter.SelectCommand.Parameters["@UserID"].Value = userid;

			//lock (CommonFunctions.Connection)
				CountriesAdapter.Fill (MainDataSet, "Countries");

			AgentCountries.DataSource = MainDataSet;

			if (!IsPostBack)
				DataBind ();

			if (MainDataSet.Tables["Users"].Rows[0]["ReferredByID"] is int)
			{
                object country = null;
                using(SqlConnection connection = CommonFunctions.GetConnection()) {
                    connection.Open();
                    SqlCommand getcountry = new SqlCommand("SELECT Country FROM Users WHERE ID = @UserID", connection);
                    getcountry.Parameters.Add("@UserID", SqlDbType.Int, 4);
                    getcountry.Parameters["@UserID"].Value = (int)MainDataSet.Tables["Users"].Rows[0]["ReferredByID"];

                    country = getcountry.ExecuteScalar();
                    connection.Close();
                }
				if (country is string)
					try
					{
						AgentCountries.SelectedValue = (string)country;
					}
					catch (Exception)
					{
					}

				AgentCountries_SelectedIndexChanged (sender, e);

				try
				{
					Agents.SelectedValue = ((int)MainDataSet.Tables["Users"].Rows[0]["ReferredByID"]).ToString ();
				}
				catch (Exception)
				{
				}
			}
			else
				AgentCountries_SelectedIndexChanged (sender, e);
		}
	}

	protected void AgentCountries_SelectedIndexChanged (object sender, EventArgs e)
	{
		AgentsAdapter.SelectCommand.Parameters["@Country"].Value = AgentCountries.SelectedValue;
		AgentsAdapter.SelectCommand.Parameters["@UserID"].Value = userid;

		//lock (CommonFunctions.Connection)
			AgentsAdapter.Fill (MainDataSet, "Agents");
		Agents.DataSource = MainDataSet;
		Agents.DataMember = "Agents";
		Agents.DataValueField = "ID";
		Agents.DataTextField = "FullName";

		Agents.DataBind ();
	}

	protected void ReservationSame_CheckedChanged (object sender, EventArgs e)
	{
		if (ReservationSame.Checked)
		{
			ReservationEmail.Text = EmailAddress.Text;
			ReservationFirstName.Text = FirstName.Text;
			ReservationLastName.Text = LastName.Text;
		}

		ReservationEmail.ReadOnly = ReservationSame.Checked;
		ReservationFirstName.ReadOnly = ReservationSame.Checked;
		ReservationLastName.ReadOnly = ReservationSame.Checked;
		ReservationEmailRequired.Enabled = !ReservationSame.Checked;
		ReservationEmailInvalid.Enabled = !ReservationSame.Checked;
		ReservationEmailTooLong.Enabled = !ReservationSame.Checked;
		ReservationFirstNameRequired.Enabled = !ReservationSame.Checked;
		ReservationFirstNameTooLong.Enabled = !ReservationSame.Checked;
		ReservationLastNameRequired.Enabled = !ReservationSame.Checked;
		ReservationLastNameTooLong.Enabled = !ReservationSame.Checked;
	}

	protected void ReservationNotSame_CheckedChanged (object sender, EventArgs e)
	{
		ReservationEmail.ReadOnly = ReservationSame.Checked;
		ReservationFirstName.ReadOnly = ReservationSame.Checked;
		ReservationLastName.ReadOnly = ReservationSame.Checked;
		ReservationEmailRequired.Enabled = !ReservationSame.Checked;
		ReservationEmailInvalid.Enabled = !ReservationSame.Checked;
		ReservationEmailTooLong.Enabled = !ReservationSame.Checked;
		ReservationFirstNameRequired.Enabled = !ReservationSame.Checked;
		ReservationFirstNameTooLong.Enabled = !ReservationSame.Checked;
		ReservationLastNameRequired.Enabled = !ReservationSame.Checked;
		ReservationLastNameTooLong.Enabled = !ReservationSame.Checked;
	}

	protected void SubmitButton_Click(object sender, System.EventArgs e)
	{
		if (!IsValid)
			return;

		MainDataSet.Tables["Users"].Rows[0]["Email"] = EmailAddress.Text;
		MainDataSet.Tables["Users"].Rows[0]["FirstName"] = FirstName.Text;
		MainDataSet.Tables["Users"].Rows[0]["LastName"] = LastName.Text;
		MainDataSet.Tables["Users"].Rows[0]["CompanyName"] = CompanyName.Text;
		MainDataSet.Tables["Users"].Rows[0]["Address"] = Address.Text;
		MainDataSet.Tables["Users"].Rows[0]["City"] = City.Text;
		MainDataSet.Tables["Users"].Rows[0]["State"] = State.Text;
		MainDataSet.Tables["Users"].Rows[0]["Zip"] = Zip.Text;
        MainDataSet.Tables["Users"].Rows[0]["Country"] = ddlCountries.SelectedItem.Text;
		MainDataSet.Tables["Users"].Rows[0]["PrimaryTelephone"] = PrimaryTelephone.Text;
		MainDataSet.Tables["Users"].Rows[0]["EveningTelephone"] = EveningTelephone.Text;
		MainDataSet.Tables["Users"].Rows[0]["DaytimeTelephone"] = DaytimeTelephone.Text;
		MainDataSet.Tables["Users"].Rows[0]["MobileTelephone"] = MobileTelephone.Text;
        MainDataSet.Tables["Users"].Rows[0]["dateModified"] = DateTime.Today.ToString();

		if (Website.Text.Length > 0)
		{
			if (!Website.Text.StartsWith ("http://"))
				Website.Text = "http://" + Website.Text;
			MainDataSet.Tables["Users"].Rows[0]["Website"] = Website.Text;
		}
		else
			MainDataSet.Tables["Users"].Rows[0]["Website"] = "";

		MainDataSet.Tables["Users"].Rows[0]["Registered"] = Registered.Text;
		MainDataSet.Tables["Users"].Rows[0]["IfPayTravelAgents"] = PayTravelAgents.Checked;

		int referredby = -1;
		try
		{
			referredby = Convert.ToInt32 (Agents.SelectedValue);
		}
		catch (Exception)
		{
		}
		if (referredby != -1)
			MainDataSet.Tables["Users"].Rows[0]["ReferredByID"] = referredby;
		else
			MainDataSet.Tables["Users"].Rows[0]["ReferredByID"] = DBNull.Value;

		if (ReservationSame.Checked)
		{
			MainDataSet.Tables["Users"].Rows[0]["IfReservationSame"] = true;
			MainDataSet.Tables["Users"].Rows[0]["ReservationEmail"] = MainDataSet.Tables["Users"].Rows[0]["Email"];
			MainDataSet.Tables["Users"].Rows[0]["ReservationFirstName"] = MainDataSet.Tables["Users"].Rows[0]["FirstName"];
			MainDataSet.Tables["Users"].Rows[0]["ReservationLastName"] = MainDataSet.Tables["Users"].Rows[0]["LastName"];
		}
		else
		{
			MainDataSet.Tables["Users"].Rows[0]["IfReservationSame"] = false;
			MainDataSet.Tables["Users"].Rows[0]["ReservationEmail"] = ReservationEmail.Text;
			MainDataSet.Tables["Users"].Rows[0]["ReservationFirstName"] = ReservationFirstName.Text;
			MainDataSet.Tables["Users"].Rows[0]["ReservationLastName"] = ReservationLastName.Text;
		}

		if (AuthenticationManager.IfAdmin && IfAdmin.Visible)
			MainDataSet.Tables["Users"].Rows[0]["IfAdmin"] = IfAdmin.Items[IfAdmin.SelectedIndex].Text == "Yes";

		
            DBConnection obj = new DBConnection();
            DataTable dt = new DataTable();
            try
            {
//lock (CommonFunctions.Connection)
			MainAdapter.Update (MainDataSet, "Users");

                dt =VADBCommander.NewsletterListByEmail(EmailAddress.Text);
                if (dt.Rows.Count == 0)
                {
                    string strFullName = FirstName.Text + " " + LastName.Text;
                    VADBCommander.NewsLetterEmailShortAdd(EmailAddress.Text, strFullName);
                }
            }
            catch (Exception ex) { lblInfo.Text = ex.Message; }
            finally { obj.CloseConnection(); }

		Response.Redirect (CommonFunctions.PrepareURL("listings.aspx"));
	}

	protected void CancelButton_Click (object sender, System.EventArgs e)
	{
		Response.Redirect (backlinkurl);
	}
}
