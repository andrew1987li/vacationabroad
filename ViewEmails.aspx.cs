using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class ViewEmails : ClosedPage
{
	protected bool ifauction;
	protected bool ifallow;

	//protected System.Data.SqlClient.SqlConnection Connection;
	protected Vacations.EmailsDataset EmailsSet;
	protected System.Data.SqlClient.SqlDataAdapter EmailsAdapter;
	protected System.Data.SqlClient.SqlCommand sqlSelectCommand1;

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (propertyid == -1)
			Response.Redirect (backlinkurl);

		//string connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

		//CommonFunctions.Connection.ConnectionString = connectionstring;

		//if (CommonFunctions.Connection.State == System.Data.ConnectionState.Closed)
			//CommonFunctions.Connection.Open ();

        object retval = null;
        using(SqlConnection connection = CommonFunctions.GetConnection()) {
            connection.Open();
            SqlCommand checkuserid = new SqlCommand("SELECT UserID FROM Properties WHERE ID=@PropertyID", connection);
            checkuserid.Parameters.Add("@PropertyID", SqlDbType.Int, 4, "PropertyID");
            checkuserid.Parameters["@PropertyID"].Value = propertyid;

            retval = checkuserid.ExecuteScalar();
            connection.Close();
        }

		if (!(retval is int))
			Response.Redirect (backlinkurl);

		if (((int)retval != userid) && !AuthenticationManager.IfAdmin)
			Response.Redirect (CommonFunctions.PrepareURL ("Login.aspx?BackLink=" + HttpUtility.UrlEncode (Request.Url.ToString ())));

        object retval2 = null;
        using(SqlConnection connection = CommonFunctions.GetConnection()) {
            connection.Open();
            SqlCommand checkauction = new SqlCommand("SELECT COUNT(*) " +
                "FROM Auctions WHERE (Auctions.PropertyID = @PropertyID)", connection);
            checkauction.Parameters.Add("@PropertyID", SqlDbType.Int, 4, "PropertyID");
            checkauction.Parameters["@PropertyID"].Value = propertyid;

            retval2 = checkauction.ExecuteScalar();
            connection.Close();
        }

		ifauction = (retval2 is int) && ((int)retval2 > 0);

		if (ifauction)
		{
            object retval3 = null;
            using(SqlConnection connection = CommonFunctions.GetConnection()) {
                connection.Open();
                SqlCommand checkpaid = new SqlCommand("SELECT COUNT(*) " +
                    "FROM Transactions INNER JOIN Auctions ON Transactions.AuctionID = Auctions.ID " +
                    "WHERE (Auctions.PropertyID = @PropertyID) AND (Transactions.IfListingFee = 1)" +
                    " AND (Transactions.PaymentAmount >= Transactions.InvoiceAmount) AND (AuctionEnd < GETDATE())", connection);
                checkpaid.Parameters.Add("@PropertyID", SqlDbType.Int, 4, "PropertyID");
                checkpaid.Parameters["@PropertyID"].Value = propertyid;

                retval3 = checkpaid.ExecuteScalar();
                connection.Close();
            }

			ifallow = (retval3 is int) && ((int)retval3 > 0);

			AllowedWarning.Text = "You can review emails only for finished and paid auctions.";
		}
		else
		{
            using(SqlConnection connection = CommonFunctions.GetConnection()) {
                connection.Open();
                SqlCommand checkpaid = new SqlCommand("SELECT COUNT(*) FROM Invoices WHERE (Invoices.PropertyID =" +
                    " @PropertyID) AND (PaymentAmount >= InvoiceAmount) AND (GETDATE() <= RenewalDate)", connection);
                checkpaid.Parameters.Add("@PropertyID", SqlDbType.Int, 4, "PropertyID");
                checkpaid.Parameters["@PropertyID"].Value = propertyid;

                object retval3 = checkpaid.ExecuteScalar();

                SqlCommand getstartdate = new SqlCommand("SELECT DateStartViewed FROM Properties WHERE ID = @PropertyID",
                    connection);
                getstartdate.Parameters.Add("@PropertyID", SqlDbType.Int, 4, "PropertyID");
                getstartdate.Parameters["@PropertyID"].Value = propertyid;

                object retval4 = getstartdate.ExecuteScalar();

                ifallow = ((retval3 is int) && ((int)retval3 > 0)) || ((retval4 is DateTime) &&
                    ((DateTime.Now - (DateTime)retval4).TotalDays <= 60));

                connection.Close();
            }
			AllowedWarning.Text = "You can review emails only for paid properties.";
		}

		if (AuthenticationManager.IfAuthenticated && AuthenticationManager.IfAdmin)
			ifallow = true;

		AllowedWarning.Visible = !ifallow;

		EmailsAdapter.SelectCommand.Parameters["@PropertyID"].Value = propertyid;
		//lock (CommonFunctions.Connection)
			EmailsAdapter.Fill (EmailsSet);

		if (!IsPostBack)
			DataBind ();
	}
	
	public string GetText (DataRowView rowview)
	{
		string retval = "";

		if ((rowview.Row["IfCustom"] is bool) && !(bool)rowview.Row["IfCustom"])
		{
			if (rowview.Row["ContactName"] is string)
				retval += "Contact Name: " + (string)rowview.Row["ContactName"] + "<br />";
			if (rowview.Row["ContactEmail"] is string)
				retval += "Contact Email: " + (string)rowview.Row["ContactEmail"] + "<br />";
			if (rowview.Row["ContactTelephone"] is string)
				retval += "Contact Telephone: " + (string)rowview.Row["ContactTelephone"] + "<br />";
			if (rowview.Row["ArrivalDate"] is string)
				retval += "Arrival Date: " + (string)rowview.Row["ArrivalDate"] + "<br />";
			if (rowview.Row["DepartureDate"] is string)
				retval += "Departure Date: " + (string)rowview.Row["DepartureDate"] + "<br />";
			if (rowview.Row["Nights"] is string)
				retval += "How Many Nights: " + (string)rowview.Row["Nights"] + "<br />";
			if (rowview.Row["Adults"] is string)
				retval += "How Many Adults: " + (string)rowview.Row["Adults"] + "<br />";
			if (rowview.Row["Children"] is string)
				retval += "How Many Children: " + (string)rowview.Row["Children"] + "<br />";
			if (rowview.Row["Telephone"] is string)
				retval += "Telephone: " + (string)rowview.Row["Telephone"] + "<br />";
			if (rowview.Row["Telephone2"] is string)
				retval += "Telephone 2: " + (string)rowview.Row["Telephone2"] + "<br />";
			if (rowview.Row["Notes"] is string)
				retval += "Comments: " + (string)rowview.Row["Notes"] + "<br />";
		}
		else
			if (rowview.Row["Email"] is string)
				retval += "Email from administration:<br />" + (string)rowview.Row["Email"] + "<br />";

		return retval;
	}

	#region Web Form Designer generated code
	override protected void OnInit(EventArgs e)
	{
		//
		// CODEGEN: This call is required by the ASP.NET Web Form Designer.
		//
		InitializeComponent();
		base.OnInit(e);
	}
	
	/// <summary>
	/// Required method for Designer support - do not modify
	/// the contents of this method with the code editor.
	/// </summary>
	private void InitializeComponent()
	{    
		//CommonFunctions.Connection = new System.Data.SqlClient.SqlConnection();
		this.EmailsSet = new Vacations.EmailsDataset();
		this.EmailsAdapter = new System.Data.SqlClient.SqlDataAdapter();
		this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
		((System.ComponentModel.ISupportInitialize)(this.EmailsSet)).BeginInit();
		// 
		// CommonFunctions.Connection
		// 
		//CommonFunctions.Connection.ConnectionString = "workstation id=MAIN;packet size=4096;integrated security=SSPI;data source=MAIN;pe" +
			//"rsist security info=False;initial catalog=Vacations";
		// 
		// EmailsSet
		// 
		this.EmailsSet.DataSetName = "EmailsDataset";
		this.EmailsSet.Locale = new System.Globalization.CultureInfo("en-US");
		// 
		// EmailsAdapter
		// 
		this.EmailsAdapter.SelectCommand = this.sqlSelectCommand1;
		this.EmailsAdapter.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																								new System.Data.Common.DataTableMapping("Table", "Emails", new System.Data.Common.DataColumnMapping[] {
																																																		  new System.Data.Common.DataColumnMapping("ID", "ID"),
																																																		  new System.Data.Common.DataColumnMapping("PropertyID", "PropertyID"),
																																																		  new System.Data.Common.DataColumnMapping("DateTime", "DateTime"),
																																																		  new System.Data.Common.DataColumnMapping("ContactName", "ContactName"),
																																																		  new System.Data.Common.DataColumnMapping("ContactEmail", "ContactEmail"),
																																																		  new System.Data.Common.DataColumnMapping("ContactTelephone", "ContactTelephone"),
																																																		  new System.Data.Common.DataColumnMapping("ArrivalDate", "ArrivalDate"),
																																																		  new System.Data.Common.DataColumnMapping("DepartureDate", "DepartureDate"),
																																																		  new System.Data.Common.DataColumnMapping("Nights", "Nights"),
																																																		  new System.Data.Common.DataColumnMapping("Adults", "Adults"),
																																																		  new System.Data.Common.DataColumnMapping("Children", "Children"),
																																																		  new System.Data.Common.DataColumnMapping("Telephone", "Telephone"),
																																																		  new System.Data.Common.DataColumnMapping("Telephone2", "Telephone2"),
																																																		  new System.Data.Common.DataColumnMapping("Notes", "Notes"),
																																																		  new System.Data.Common.DataColumnMapping("Email", "Email"),
																																																		  new System.Data.Common.DataColumnMapping("IfCustom", "IfCustom")})});
		// 
		// sqlSelectCommand1
		// 
		this.sqlSelectCommand1.CommandText = @"SELECT ID, PropertyID, DateTime, ContactName, ContactEmail, ContactTelephone, ArrivalDate, DepartureDate, Nights, Adults, Children, Telephone, Telephone2, Notes, Email, IfCustom FROM Emails WHERE (PropertyID = @PropertyID) AND (DATEDIFF(dd, DateTime, GETDATE()) < 356) ORDER BY DateTime DESC";
        this.sqlSelectCommand1.Connection = CommonFunctions.GetConnection();
		this.sqlSelectCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PropertyID", System.Data.SqlDbType.Int, 4, "PropertyID"));
		((System.ComponentModel.ISupportInitialize)(this.EmailsSet)).EndInit();

	}
	#endregion
}
