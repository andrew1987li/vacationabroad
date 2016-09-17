using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class ViewInvoices : ClosedPage
{
	protected System.Web.UI.WebControls.Label Label2;
	protected System.Web.UI.WebControls.Label Label3;
	protected System.Web.UI.WebControls.Button NewProperty;
	protected System.Web.UI.WebControls.Label Label1;
	protected System.Data.SqlClient.SqlDataAdapter GetOutstandingInvoices;
	protected System.Data.SqlClient.SqlDataAdapter GetPaidInvoices;
	protected System.Data.SqlClient.SqlCommand sqlSelectCommand1;
	protected Vacations.InvoicesPropertiesDataset OutstandingInvoicesSet;
	protected Vacations.InvoicesPropertiesDataset PaidInvoicesSet;
	protected System.Data.SqlClient.SqlCommand sqlSelectCommand2;

	//protected SqlConnection Connection = new SqlConnection (ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
	protected SqlDataAdapter OutstandingInvoices;
	protected SqlDataAdapter PaidInvoices;
	protected DataSet MainDataSet = new DataSet ();

	protected void Page_Load(object sender, System.EventArgs e)
	{
        OutstandingInvoices = CommonFunctions.PrepareAdapter(CommonFunctions.GetConnection(), "SELECT Invoices.ID AS InvoiceID," +
			" Invoices.InvoiceDate, Invoices.InvoiceAmount, Invoices.PaymentDate, Invoices.PaymentAmount," +
            " Invoices.PaymentType, Invoices.InvoiceAmount - ISNULL(Invoices.PaymentAmount, 0) AS Balance, Properties.ID," +
			" Users.ID AS UserID " +
			"FROM Invoices INNER JOIN Properties ON Invoices.PropertyID = Properties.ID" +
			" INNER JOIN Users ON Properties.UserID = Users.ID " +
            "WHERE (ISNULL (Invoices.PaymentAmount, 0) = 0) AND (invoices.paymentamount>0) and (Users.ID = @UserID) ORDER BY Invoices.InvoiceDate",
			SqlDbType.Int);

        PaidInvoices = CommonFunctions.PrepareAdapter(CommonFunctions.GetConnection(), "SELECT 0 AS IfAuction, Invoices.ID AS InvoiceID," +
			" Invoices.InvoiceDate, Invoices.RenewalDate AS RenewalDate, Invoices.InvoiceAmount, Invoices.PaymentDate, Invoices.PaymentAmount," +
			" Invoices.PaymentType, Invoices.InvoiceAmount - ISNULL(Invoices.PaymentAmount, 0) AS Balance, Properties.ID," +
			" Users.ID AS UserID " +
			"FROM Invoices INNER JOIN Properties ON Invoices.PropertyID = Properties.ID" +
			" INNER JOIN Users ON Properties.UserID = Users.ID " +
			"WHERE (Invoices.PaymentAmount > 0) AND (Users.ID = @UserID) " +
            "ORDER BY PaymentDate DESC", SqlDbType.Int);
            //"UNION " +
            //"SELECT 1 AS IfAuction, Transactions.ID AS InvoiceID, Transactions.InvoiceDate, Transactions.InvoiceAmount," +
            //" Transactions.PaymentDate, Transactions.PaymentAmount, 'Credit card' AS PaymentType," +
            //" Transactions.InvoiceAmount - ISNULL(Transactions.PaymentAmount, 0) AS Balance, Auctions.ID, Users.ID AS UserID " +
            //"FROM Transactions INNER JOIN Auctions ON Transactions.AuctionID = Auctions.ID" +
            //" INNER JOIN Properties ON Auctions.PropertyID = Properties.ID" +
            //" INNER JOIN Users ON Properties.UserID = Users.ID " +
            //"WHERE (Transactions.PaymentAmount > 0) AND (Users.ID = @UserID) ORDER BY PaymentDate DESC",
			

		OutstandingInvoices.SelectCommand.Parameters["@UserID"].Value = userid;
		PaidInvoices.SelectCommand.Parameters["@UserID"].Value = userid;

		//lock (CommonFunctions.Connection)
			OutstandingInvoices.Fill (MainDataSet, "OutstandingInvoices");
		//lock (CommonFunctions.Connection)
			PaidInvoices.Fill (MainDataSet, "PaidInvoices");

		if (!IsPostBack)
			DataBind ();

		if (userid != AuthenticationManager.UserID)
			OwnerInformationLink.Text = "Update this owner's personal information";
		else
			OwnerInformationLink.Text = "Update my personal information";
		OwnerInformationLink.NavigateUrl = "OwnerInformation.aspx?UserID=" + userid.ToString ();
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
		this.OutstandingInvoicesSet = new Vacations.InvoicesPropertiesDataset();
		this.GetPaidInvoices = new System.Data.SqlClient.SqlDataAdapter();
		this.sqlSelectCommand2 = new System.Data.SqlClient.SqlCommand();
		this.PaidInvoicesSet = new Vacations.InvoicesPropertiesDataset();
		((System.ComponentModel.ISupportInitialize)(this.OutstandingInvoicesSet)).BeginInit();
		((System.ComponentModel.ISupportInitialize)(this.PaidInvoicesSet)).BeginInit();
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
																																																					 new System.Data.Common.DataColumnMapping("Balance", "Balance"),
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
																																																					 new System.Data.Common.DataColumnMapping("Description", "Description"),
																																																					 new System.Data.Common.DataColumnMapping("Amenities", "Amenities"),
																																																					 new System.Data.Common.DataColumnMapping("LocalAttractions", "LocalAttractions"),
																																																					 new System.Data.Common.DataColumnMapping("Rates", "Rates"),
																																																					 new System.Data.Common.DataColumnMapping("CancellationPolicy", "CancellationPolicy"),
																																																					 new System.Data.Common.DataColumnMapping("DepositRequired", "DepositRequired"),
																																																					 new System.Data.Common.DataColumnMapping("IfMoreThan7PhotosAllowed", "IfMoreThan7PhotosAllowed"),
																																																					 new System.Data.Common.DataColumnMapping("IfFinished", "IfFinished"),
																																																					 new System.Data.Common.DataColumnMapping("IfApproved", "IfApproved"),
																																																					 new System.Data.Common.DataColumnMapping("IfPaid", "IfPaid"),
																																																					 new System.Data.Common.DataColumnMapping("DateAdded", "DateAdded"),
																																																					 new System.Data.Common.DataColumnMapping("DateStartViewed", "DateStartViewed")})});
		// 
		// sqlSelectCommand1
		// 
		this.sqlSelectCommand1.CommandText = "SELECT Invoices.ID AS InvoiceID, Invoices.InvoiceDate, Invoices.InvoiceAmount, In" +
			"voices.PaymentDate, Invoices.PaymentAmount, Invoices.PaymentType, Invoices.InvoiceAmount - ISNULL(Invoices.PaymentAmount, 0) AS Balance, MinimumNightlyRentalTypes.Name AS MinimumNightlyRental, PropertyTypes.Name A" +
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
			".NumCDPlayers, Properties.IfMoreThan7PhotosAllowed, Properties.IfFinished, Properties.If" +
			"Approved, CASE WHEN EXISTS (SELECT * FROM Invoices WHERE (Invoices.PropertyID = " +
			"Properties.ID) AND (PaymentAmount >= InvoiceAmount) AND (GETDATE() <= RenewalDate)) THEN 1 ELSE 0 END AS IfPaid, Properties.DateAdded, Properties.DateStar" +
			"tViewed, Properties.VirtualTour, Properties.RatesTable, Properties.PricesCurrenc" +
			"y, Properties.CheckIn, Properties.CheckOut, Properties.LodgingTax, Properties.Ta" +
			"xIncluded, Properties.DateAvailable, Properties.IfDiscounted, Properties.IfLastMinuteCancellations, Properties.HomeExchangeCityID1, Properties.HomeExchangeCityID2, Properties.HomeExchangeCityID3 FROM Invoices INNER JOIN Properties ON Invoices.PropertyID = Propertie" +
			"s.ID INNER JOIN Cities ON Properties.CityID = Cities.ID INNER JOIN StateProvince" +
			"s ON StateProvinces.ID = Cities.StateProvinceID INNER JOIN Countries ON StatePro" +
			"vinces.CountryID = Countries.ID INNER JOIN Regions ON Countries.RegionID = Regio" +
			"ns.ID INNER JOIN Users ON Properties.UserID = Users.ID LEFT OUTER JOIN MinimumNi" +
			"ghtlyRentalTypes ON Properties.MinimumNightlyRentalID = MinimumNightlyRentalType" +
			"s.ID LEFT OUTER JOIN PropertyTypes ON Properties.TypeID = PropertyTypes.ID WHERE" +
			" (ISNULL (Invoices.PaymentAmount, 0) = 0) AND (Users.ID = @UserID) AND (Users.ID = @U" +
			"serID) ORDER BY Invoices.InvoiceDate";
        this.sqlSelectCommand1.Connection = CommonFunctions.GetConnection();
		this.sqlSelectCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserID", System.Data.SqlDbType.Int, 4, "ID"));
		// 
		// OutstandingInvoicesSet
		// 
		this.OutstandingInvoicesSet.DataSetName = "InvoicesPropertiesDataset";
		this.OutstandingInvoicesSet.Locale = new System.Globalization.CultureInfo("en-US");
		// 
		// GetPaidInvoices
		// 
		this.GetPaidInvoices.SelectCommand = this.sqlSelectCommand2;
		this.GetPaidInvoices.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																								  new System.Data.Common.DataTableMapping("Table", "Invoices", new System.Data.Common.DataColumnMapping[] {
																																																			  new System.Data.Common.DataColumnMapping("InvoiceID", "InvoiceID"),
																																																			  new System.Data.Common.DataColumnMapping("InvoiceDate", "InvoiceDate"),
																																																			  new System.Data.Common.DataColumnMapping("InvoiceAmount", "InvoiceAmount"),
																																																			  new System.Data.Common.DataColumnMapping("PaymentDate", "PaymentDate"),
																																																			  new System.Data.Common.DataColumnMapping("PaymentAmount", "PaymentAmount"),
																																																			  new System.Data.Common.DataColumnMapping("PaymentType", "PaymentType"),
																																																			  new System.Data.Common.DataColumnMapping("Balance", "Balance"),
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
																																																			  new System.Data.Common.DataColumnMapping("Description", "Description"),
																																																			  new System.Data.Common.DataColumnMapping("Amenities", "Amenities"),
																																																			  new System.Data.Common.DataColumnMapping("LocalAttractions", "LocalAttractions"),
																																																			  new System.Data.Common.DataColumnMapping("Rates", "Rates"),
																																																			  new System.Data.Common.DataColumnMapping("CancellationPolicy", "CancellationPolicy"),
																																																			  new System.Data.Common.DataColumnMapping("DepositRequired", "DepositRequired"),
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
		// sqlSelectCommand2
		// 
		this.sqlSelectCommand2.CommandText = "SELECT Invoices.ID AS InvoiceID, Invoices.InvoiceDate, Invoices.InvoiceAmount, In" +
			"voices.PaymentDate, Invoices.PaymentAmount, Invoices.PaymentType, Invoices.InvoiceAmount - ISNULL(Invoices.PaymentAmount, 0) AS Balance, MinimumNightlyRentalTypes.Name AS MinimumNightlyRental, PropertyTypes.Name A" +
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
			".NumCDPlayers, Properties.IfMoreThan7PhotosAllowed, Properties.IfFinished, Properties.If" +
			"Approved, CASE WHEN EXISTS (SELECT * FROM Invoices WHERE (Invoices.PropertyID = " +
			"Properties.ID) AND (PaymentAmount >= InvoiceAmount) AND (GETDATE() <= RenewalDate)) THEN 1 ELSE 0 END AS IfPaid, Properties.DateAdded, " +
			"Properties.DateStartViewed, Properties.VirtualTour, Properties.RatesTable, Prope" +
			"rties.PricesCurrency, Properties.CheckIn, Properties.CheckOut, Properties.Lodgin" +
			"gTax, Properties.TaxIncluded, Properties.DateAvailable, Properties.IfDiscounted, Properties.IfLastMinuteCancellations, Properties.HomeExchangeCityID1, Properties.HomeExchangeCityID2, Properties.HomeExchangeCityID3 FROM Invoices INNER JOIN Properties ON Invoices.Pro" +
			"pertyID = Properties.ID INNER JOIN Cities ON Properties.CityID = Cities.ID INNER" +
			" JOIN StateProvinces ON StateProvinces.ID = Cities.StateProvinceID INNER JOIN Co" +
			"untries ON StateProvinces.CountryID = Countries.ID INNER JOIN Regions ON Countri" +
			"es.RegionID = Regions.ID INNER JOIN Users ON Properties.UserID = Users.ID LEFT O" +
			"UTER JOIN MinimumNightlyRentalTypes ON Properties.MinimumNightlyRentalID = Minim" +
			"umNightlyRentalTypes.ID LEFT OUTER JOIN PropertyTypes ON Properties.TypeID = Pro" +
			"pertyTypes.ID WHERE (Invoices.PaymentAmount > 0) AND (Users.ID = @UserID) ORDER BY Invoices.PaymentDate DESC";
        this.sqlSelectCommand2.Connection = CommonFunctions.GetConnection();
		this.sqlSelectCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserID", System.Data.SqlDbType.Int, 4, "ID"));
		// 
		// PaidInvoicesSet
		// 
		this.PaidInvoicesSet.DataSetName = "InvoicesPropertiesDataset";
		this.PaidInvoicesSet.Locale = new System.Globalization.CultureInfo("en-US");
	}
	#endregion
    protected void Repeater1_ItemCreated(object sender, RepeaterItemEventArgs e)
    {
        if (Repeater1.Items.Count == 0)
            Repeater1.Visible = false;
    }
}
