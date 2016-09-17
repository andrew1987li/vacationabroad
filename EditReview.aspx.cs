using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class EditReview : ClosedPage
{
	//protected SqlConnection Connection = new SqlConnection (ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
	protected SqlDataAdapter AuctionsAdapter;
	protected DataSet MainDataSet = new DataSet ();

	protected void Page_Load (object sender, EventArgs e)
    {
		if (auctionid == -1)
			Response.Redirect (CommonFunctions.PrepareURL ("InternalError.aspx"), true);

		//CommonFunctions.Connection.Open ();

		AuctionsAdapter = CommonFunctions.PrepareAdapter (CommonFunctions.GetConnection(), "SELECT *," +
			" (SELECT UserID FROM Properties WHERE Properties.ID = Auctions.PropertyID) AS UserID," +
			" (SELECT ISNULL(Users.UserID, Users.Username) FROM Users INNER JOIN Properties ON Users.ID = Properties.UserID" +
			" WHERE Properties.ID = Auctions.PropertyID) AS Username," +
			" (SELECT TOP 1 FileName FROM PropertyPhotos INNER JOIN Properties ON" +
			" PropertyPhotos.PropertyID = Properties.ID WHERE Properties.ID = Auctions.PropertyID ORDER BY OrderNumber) AS FileName," +
			" (SELECT TOP 1 Width FROM PropertyPhotos INNER JOIN Properties ON" +
			" PropertyPhotos.PropertyID = Properties.ID WHERE Properties.ID = Auctions.PropertyID ORDER BY OrderNumber) AS Width," +
			" (SELECT TOP 1 Height FROM PropertyPhotos INNER JOIN Properties ON" +
			" PropertyPhotos.PropertyID = Properties.ID WHERE Properties.ID = Auctions.PropertyID ORDER BY OrderNumber) AS Height " +
			"FROM Auctions WHERE (ID = @AuctionID) AND (AuctionEnd <= GETDATE ()) AND (HighestBidderID IS NOT NULL)",
			SqlDbType.Int);

		AuctionsAdapter.SelectCommand.Parameters["@AuctionID"].Value = auctionid;

		//lock (CommonFunctions.Connection)
			if (AuctionsAdapter.Fill (MainDataSet, "Auctions") == 0)
				Response.Redirect (CommonFunctions.PrepareURL ("InternalError.aspx"), true);

		if (((int)MainDataSet.Tables["Auctions"].Rows[0]["HighestBidderID"] != AuthenticationManager.UserID) &&
				!AuthenticationManager.IfAdmin)
			Response.Redirect (CommonFunctions.PrepareURL ("Login.aspx"), true);

		if (!IsPostBack)
		{
			DisplayRadioGroup (Controls, "Auction");
			DisplayRadioGroup (Controls, "AccuratelyRepresented");
			DisplayRadioGroup (Controls, "CustomerService");
			DisplayRadioGroup (Controls, "Cleanliness");
			DisplayRadioGroup (Controls, "GoodValue");

			Comments.Text = CommonFunctions.DecodeInput (MainDataSet.Tables["Auctions"].Rows[0]["Notes"].ToString ());
		}

		DataBind ();
	}

	protected bool DisplayRadioGroup (ControlCollection Controls, string GroupName)
	{
		if (!(MainDataSet.Tables["Auctions"].Rows[0][GroupName] is int))
			return false;

		int value = (int)MainDataSet.Tables["Auctions"].Rows[0][GroupName];
		string ending = "Mark" + value.ToString ();

		foreach (Control control in Controls)
			if ((control is RadioButton) && (string.Compare (((RadioButton)control).GroupName, GroupName, true) == 0) &&
				control.ID.EndsWith (ending))
			{
				((RadioButton)control).Checked = true;
				return true;
			}
			else if (control.Controls.Count > 0)
				if (DisplayRadioGroup (control.Controls, GroupName))
					return true;

		return false;
	}

	protected bool SaveRadioGroup (ControlCollection Controls, string GroupName)
	{
		foreach (Control control in Controls)
			if ((control is RadioButton) && (string.Compare (((RadioButton)control).GroupName, GroupName, true) == 0) &&
				((RadioButton)control).Checked)
			{
				string ending = control.ID.Substring (control.ID.Length - 1);

				try
				{
					MainDataSet.Tables["Auctions"].Rows[0][GroupName] = Convert.ToInt32 (ending);
				}
				catch (Exception)
				{
				}

				return true;
			}
			else if (control.Controls.Count > 0)
				if (SaveRadioGroup (control.Controls, GroupName))
					return true;

		return false;
	}

	protected void SaveButton_Click (object sender, EventArgs e)
	{
		SaveRadioGroup (Controls, "Auction");
		SaveRadioGroup (Controls, "AccuratelyRepresented");
		SaveRadioGroup (Controls, "CustomerService");
		SaveRadioGroup (Controls, "Cleanliness");
		SaveRadioGroup (Controls, "GoodValue");

		MainDataSet.Tables["Auctions"].Rows[0]["Notes"] = CommonFunctions.EncodeInput (Comments.Text);
		MainDataSet.Tables["Auctions"].Rows[0]["ReviewDate"] = DateTime.Now;

		//lock (CommonFunctions.Connection)
			AuctionsAdapter.Update (MainDataSet, "Auctions");

		Response.Redirect (CommonFunctions.PrepareURL ("MyAccount.aspx"));
	}

	protected void CancelButton_Click (object sender, EventArgs e)
	{
		Response.Redirect (backlinkurl);
	}
}
