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

public partial class OutstandingInvoices : AdminPage
{
	protected System.Data.SqlClient.SqlDataAdapter GetOutstandingInvoices;
	protected Vacations.InvoicesPropertiesDataset InvoicesPropertiesSet;
	protected System.Data.SqlClient.SqlCommand sqlSelectCommand1;
	//protected System.Data.SqlClient.SqlConnection Connection;

	protected void Page_Load(object sender, System.EventArgs e)
	{
		userid = AuthenticationManager.UserID;

		//string connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

		//CommonFunctions.Connection.ConnectionString = connectionstring;

		//if (CommonFunctions.Connection.State == ConnectionState.Closed)
			//CommonFunctions.Connection.Open ();

		if (Request.Params["SortOrder"] != null)
		{
			string sortfield = "";

			switch (Request.Params["SortOrder"])
			{
				case "1":
					sortfield = "Invoices.ID";
					break;
				case "2":
					sortfield = "InvoiceDate";
					break;
				case "3":
					sortfield = "InvoiceAmount";
					break;
				case "4":
					sortfield = "Properties.ID";
					break;
				case "5":
					sortfield = "PrimaryTelephone";
					break;
				case "6":
					sortfield = "DaytimeTelephone";
					break;
				case "7":
					sortfield = "EveningTelephone";
					break;
				case "8":
					sortfield = "MobileTelephone";
					break;
			}

			if (sortfield.Length > 0)
				GetOutstandingInvoices.SelectCommand.CommandText += " ORDER BY " + sortfield;
		}

		//lock (CommonFunctions.Connection)
			GetOutstandingInvoices.Fill (InvoicesPropertiesSet);

		DataBind ();
	}

	public string GetQueryStringWithoutSortOrder ()
	{
		string querystring = Request.QueryString.ToString ();

		querystring = querystring.Replace ("&SortOrder=1", "");
		querystring = querystring.Replace ("&SortOrder=2", "");
		querystring = querystring.Replace ("&SortOrder=3", "");
		querystring = querystring.Replace ("&SortOrder=4", "");
		querystring = querystring.Replace ("&SortOrder=5", "");
		querystring = querystring.Replace ("&SortOrder=6", "");
		querystring = querystring.Replace ("&SortOrder=7", "");
		querystring = querystring.Replace ("&SortOrder=8", "");

		querystring = querystring.Replace ("SortOrder=1&", "");
		querystring = querystring.Replace ("SortOrder=2&", "");
		querystring = querystring.Replace ("SortOrder=3&", "");
		querystring = querystring.Replace ("SortOrder=4&", "");
		querystring = querystring.Replace ("SortOrder=5&", "");
		querystring = querystring.Replace ("SortOrder=6&", "");
		querystring = querystring.Replace ("SortOrder=7&", "");
		querystring = querystring.Replace ("SortOrder=8&", "");

		return querystring;
	}

	public string SumInvoiceAmounts ()
	{
		decimal sum = 0;

		foreach (DataRow datarow in InvoicesPropertiesSet.Tables["Invoices"].Rows)
			if (datarow.RowState != DataRowState.Deleted)
				if (datarow["InvoiceAmount"] is decimal)
					sum += (decimal)datarow["InvoiceAmount"];

		return sum.ToString ("c");
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
		this.GetOutstandingInvoices = new System.Data.SqlClient.SqlDataAdapter();
		this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
		this.InvoicesPropertiesSet = new Vacations.InvoicesPropertiesDataset();
		((System.ComponentModel.ISupportInitialize)(this.InvoicesPropertiesSet)).BeginInit();
		// 
		// CommonFunctions.Connection
		// 
		//CommonFunctions.Connection.ConnectionString = "workstation id=MAIN;packet size=4096;integrated security=SSPI;data source=MAIN;pe" +
			//"rsist security info=False;initial catalog=Vacations";
		// 
		// GetOutstandingInvoices
		// 
		this.GetOutstandingInvoices.SelectCommand = this.sqlSelectCommand1;
		this.GetOutstandingInvoices.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																										 new System.Data.Common.DataTableMapping("Table", "Invoices", new System.Data.Common.DataColumnMapping[] {
																																																					 new System.Data.Common.DataColumnMapping("InvoiceID", "InvoiceID"),
																																																					 new System.Data.Common.DataColumnMapping("InvoiceDate", "InvoiceDate"),
																																																					 new System.Data.Common.DataColumnMapping("InvoiceAmount", "InvoiceAmount"),
																																																					 new System.Data.Common.DataColumnMapping("PaymentDate", "PaymentDate"),
																																																					 new System.Data.Common.DataColumnMapping("PaymentAmount", "PaymentAmount"),
																																																					 new System.Data.Common.DataColumnMapping("PaymentType", "PaymentType"),
																																																					 new System.Data.Common.DataColumnMapping("MinimumNightlyRental", "MinimumNightlyRental"),
																																																					 new System.Data.Common.DataColumnMapping("Type", "Type"),
																																																					 new System.Data.Common.DataColumnMapping("FirstName", "FirstName"),
																																																					 new System.Data.Common.DataColumnMapping("LastName", "LastName"),
																																																					 new System.Data.Common.DataColumnMapping("PrimaryTelephone", "PrimaryTelephone"),
																																																					 new System.Data.Common.DataColumnMapping("OwnerCountry", "OwnerCountry"),
																																																					 new System.Data.Common.DataColumnMapping("Email", "Email"),
																																																					 new System.Data.Common.DataColumnMapping("Username", "Username"),
																																																					 new System.Data.Common.DataColumnMapping("OwnerAddress", "OwnerAddress"),
																																																					 new System.Data.Common.DataColumnMapping("EveningTelephone", "EveningTelephone"),
																																																					 new System.Data.Common.DataColumnMapping("DaytimeTelephone", "DaytimeTelephone"),
																																																					 new System.Data.Common.DataColumnMapping("MobileTelephone", "MobileTelephone"),
																																																					 new System.Data.Common.DataColumnMapping("Website", "Website"),
																																																					 new System.Data.Common.DataColumnMapping("OwnerCity", "OwnerCity"),
																																																					 new System.Data.Common.DataColumnMapping("OwnerState", "OwnerState"),
																																																					 new System.Data.Common.DataColumnMapping("OwnerZip", "OwnerZip"),
																																																					 new System.Data.Common.DataColumnMapping("City", "City"),
																																																					 new System.Data.Common.DataColumnMapping("StateProvince", "StateProvince"),
																																																					 new System.Data.Common.DataColumnMapping("Country", "Country"),
																																																					 new System.Data.Common.DataColumnMapping("Region", "Region"),
																																																					 new System.Data.Common.DataColumnMapping("Smoking", "Smoking"),
																																																					 new System.Data.Common.DataColumnMapping("PetFriendly", "PetFriendly"),
																																																					 new System.Data.Common.DataColumnMapping("ID", "ID"),
																																																					 new System.Data.Common.DataColumnMapping("UserID", "UserID"),
																																																					 new System.Data.Common.DataColumnMapping("Name", "Name"),
																																																					 new System.Data.Common.DataColumnMapping("TypeID", "TypeID"),
																																																					 new System.Data.Common.DataColumnMapping("Address", "Address"),
																																																					 new System.Data.Common.DataColumnMapping("CityID", "CityID"),
																																																					 new System.Data.Common.DataColumnMapping("IfShowAddress", "IfShowAddress"),
																																																					 new System.Data.Common.DataColumnMapping("NumBedrooms", "NumBedrooms"),
																																																					 new System.Data.Common.DataColumnMapping("NumBaths", "NumBaths"),
																																																					 new System.Data.Common.DataColumnMapping("NumSleeps", "NumSleeps"),
																																																					 new System.Data.Common.DataColumnMapping("MinimumNightlyRentalID", "MinimumNightlyRentalID"),
																																																					 new System.Data.Common.DataColumnMapping("NumTVs", "NumTVs"),
																																																					 new System.Data.Common.DataColumnMapping("NumVCRs", "NumVCRs"),
																																																					 new System.Data.Common.DataColumnMapping("NumCDPlayers", "NumCDPlayers"),
																																																					 new System.Data.Common.DataColumnMapping("IfMoreThan7PhotosAllowed", "IfMoreThan7PhotosAllowed"),
																																																					 new System.Data.Common.DataColumnMapping("IfFinished", "IfFinished"),
																																																					 new System.Data.Common.DataColumnMapping("IfApproved", "IfApproved"),
																																																					 new System.Data.Common.DataColumnMapping("IfPaid", "IfPaid"),
																																																					 new System.Data.Common.DataColumnMapping("DateAdded", "DateAdded"),
																																																					 new System.Data.Common.DataColumnMapping("DateStartViewed", "DateStartViewed"),
																																																					 new System.Data.Common.DataColumnMapping("VirtualTour", "VirtualTour"),
																																																					 new System.Data.Common.DataColumnMapping("RatesTable", "RatesTable"),
																																																					 new System.Data.Common.DataColumnMapping("PricesCurrency", "PricesCurrency"),
																																																					 new System.Data.Common.DataColumnMapping("CheckIn", "CheckIn"),
																																																					 new System.Data.Common.DataColumnMapping("CheckOut", "CheckOut"),
																																																					 new System.Data.Common.DataColumnMapping("LodgingTax", "LodgingTax"),
																																																					 new System.Data.Common.DataColumnMapping("TaxIncluded", "TaxIncluded")})});
		// 
		// sqlSelectCommand1
		// 
		this.sqlSelectCommand1.CommandText = "SELECT Invoices.ID AS InvoiceID, Invoices.InvoiceDate, Invoices.InvoiceAmount, In" +
			"voices.PaymentDate, Invoices.PaymentAmount, Invoices.PaymentType, MinimumNightlyRentalTypes.Name AS MinimumNightlyRental, PropertyTypes.Name A" +
			"S Type, Users.FirstName, Users.LastName, Users.PrimaryTelephone, Users.Country A" +
			"S OwnerCountry, Users.Email, Users.Username, Users.Address AS OwnerAddress, User" +
			"s.EveningTelephone, Users.DaytimeTelephone, Users.MobileTelephone, Users.Website" +
			", Users.City AS OwnerCity, Users.State AS OwnerState, Users.Zip AS OwnerZip, Cit" +
			"ies.City, StateProvinces.StateProvince, Countries.Country, Regions.Region, CASE " +
			"WHEN EXISTS (SELECT * FROM PropertiesAmenities INNER JOIN Amenities ON Propertie" +
			"sAmenities.AmenityID = Amenities.ID WHERE (PropertiesAmenities.PropertyID = Prop" +
			"erties.ID) AND (Amenities.Amenity = \'Smoking Permitted\')) THEN \'Yes\' ELSE \'No\' E" +
			"ND AS Smoking, CASE WHEN EXISTS (SELECT * FROM PropertiesAmenities INNER JOIN Am" +
			"enities ON PropertiesAmenities.AmenityID = Amenities.ID WHERE (PropertiesAmeniti" +
			"es.PropertyID = Properties.ID) AND (Amenities.Amenity = \'Pet Friendly\')) THEN \'Y" +
			"es\' ELSE \'No\' END AS PetFriendly, Properties.ID, Properties.UserID, Properties.N" +
			"ame, Properties.TypeID, Properties.Address, Properties.CityID, Properties.IfShow" +
			"Address, Properties.NumBedrooms, Properties.NumBaths, Properties.NumSleeps, Prop" +
			"erties.MinimumNightlyRentalID, Properties.NumTVs, Properties.NumVCRs, Properties" +
			".NumCDPlayers, Properties.IfMoreThan7PhotosAllowed, Properties.IfFinished, Prope" +
			"rties.IfApproved, CASE WHEN EXISTS (SELECT * FROM Invoices WHERE (Invoices.Prope" +
			"rtyID = Properties.ID) AND (PaymentAmount >= InvoiceAmount) AND (GETDATE() <= RenewalDate)) THEN 1 ELSE 0 END AS IfPaid, Properties.Dat" +
			"eAdded, Properties.DateStartViewed, Properties.VirtualTour, Properties.RatesTabl" +
			"e, Properties.PricesCurrency, Properties.CheckIn, Properties.CheckOut, Propertie" +
			"s.LodgingTax, Properties.TaxIncluded FROM Invoices INNER JOIN Properties ON Invo" +
			"ices.PropertyID = Properties.ID INNER JOIN Cities ON Properties.CityID = Cities." +
			"ID INNER JOIN StateProvinces ON StateProvinces.ID = Cities.StateProvinceID INNER" +
			" JOIN Countries ON StateProvinces.CountryID = Countries.ID INNER JOIN Regions ON" +
			" Countries.RegionID = Regions.ID INNER JOIN Users ON Properties.UserID = Users.I" +
			"D LEFT OUTER JOIN MinimumNightlyRentalTypes ON Properties.MinimumNightlyRentalID" +
			" = MinimumNightlyRentalTypes.ID LEFT OUTER JOIN PropertyTypes ON Properties.Type" +
			"ID = PropertyTypes.ID WHERE (Invoices.PaymentAmount = 0)";
		this.sqlSelectCommand1.Connection = CommonFunctions.GetConnection();
		// 
		// InvoicesPropertiesSet
		// 
		this.InvoicesPropertiesSet.DataSetName = "InvoicesPropertiesDataset";
		this.InvoicesPropertiesSet.Locale = new System.Globalization.CultureInfo("en-US");
		((System.ComponentModel.ISupportInitialize)(this.InvoicesPropertiesSet)).EndInit();
	}
	#endregion
}
