using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class EditLastMinute : ClosedPage
{
	protected Vacations.PropertiesFullDataset PropertiesFullSet;
	protected System.Data.SqlClient.SqlDataAdapter GetPropertiesAdapter;
	protected Vacations.AmenitiesDataset AmenitiesSet;
	protected Vacations.PhotosDataset PhotosSet;
	protected System.Data.SqlClient.SqlDataAdapter PhotosAdapter;
	protected System.Data.SqlClient.SqlCommand sqlCommand1;
	protected System.Data.SqlClient.SqlDataAdapter AmenitiesAdapter;
	protected System.Data.SqlClient.SqlCommand sqlInsertCommand1;
	protected System.Data.SqlClient.SqlCommand sqlSelectCommand2;
	protected Vacations.PropertiesDataset PropertiesSet;
	protected System.Data.SqlClient.SqlDataAdapter PropertiesAdapter;
	protected System.Data.SqlClient.SqlCommand sqlSelectCommand3;
	protected System.Data.SqlClient.SqlCommand sqlSelectCommand1;
	//protected System.Data.SqlClient.SqlConnection Connection;

	protected void Page_Load(object sender, System.EventArgs e)
	{
		System.Data.SqlClient.SqlCommandBuilder builder = new System.Data.SqlClient.SqlCommandBuilder (PropertiesAdapter);

		if (propertyid == -1)
			Response.Redirect (CommonFunctions.PrepareURL ("InternalError.aspx"), true);

		//string connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

		//CommonFunctions.Connection.ConnectionString = connectionstring;

		GetPropertiesAdapter.SelectCommand.Parameters["@PropertyID"].Value = propertyid;

		//lock (CommonFunctions.Connection)
			if (GetPropertiesAdapter.Fill (PropertiesFullSet) < 1)
				Response.Redirect (backlinkurl);

		if ((int)PropertiesFullSet.Tables["Properties"].Rows[0]["IfPaid"] != 1)
			Response.Redirect (backlinkurl);

		AmenitiesAdapter.SelectCommand.Parameters["@PropertyID"].Value = propertyid;
		//lock (CommonFunctions.Connection)
			AmenitiesAdapter.Fill (AmenitiesSet);

		if (!(bool)PropertiesFullSet.Tables["Properties"].Rows[0]["IfApproved"] &&
				(!AuthenticationManager.IfAuthenticated ||
				((AuthenticationManager.UserID != (int)PropertiesFullSet.Tables["Properties"].Rows[0]["UserID"]) &&
				!AuthenticationManager.IfAdmin)))
			Response.Redirect (backlinkurl);

		PhotosAdapter.SelectCommand.Parameters["@PropertyID"].Value = propertyid;

		//lock (CommonFunctions.Connection)
			PhotosAdapter.Fill (PhotosSet);

		if (!IsPostBack)
			DataBind ();
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
		this.PropertiesFullSet = new Vacations.PropertiesFullDataset();
		this.GetPropertiesAdapter = new System.Data.SqlClient.SqlDataAdapter();
		this.AmenitiesSet = new Vacations.AmenitiesDataset();
		this.PhotosSet = new Vacations.PhotosDataset();
		this.PhotosAdapter = new System.Data.SqlClient.SqlDataAdapter();
		this.sqlCommand1 = new System.Data.SqlClient.SqlCommand();
		this.AmenitiesAdapter = new System.Data.SqlClient.SqlDataAdapter();
		this.sqlInsertCommand1 = new System.Data.SqlClient.SqlCommand();
		this.sqlSelectCommand2 = new System.Data.SqlClient.SqlCommand();
		this.PropertiesSet = new Vacations.PropertiesDataset();
		this.PropertiesAdapter = new System.Data.SqlClient.SqlDataAdapter();
		this.sqlSelectCommand3 = new System.Data.SqlClient.SqlCommand();
		this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
		((System.ComponentModel.ISupportInitialize)(this.PropertiesFullSet)).BeginInit();
		((System.ComponentModel.ISupportInitialize)(this.AmenitiesSet)).BeginInit();
		((System.ComponentModel.ISupportInitialize)(this.PhotosSet)).BeginInit();
		((System.ComponentModel.ISupportInitialize)(this.PropertiesSet)).BeginInit();
		// 
		// CommonFunctions.Connection
		// 
		//CommonFunctions.Connection.ConnectionString = "workstation id=MAIN;packet size=4096;integrated security=SSPI;data source=MAIN;pe" +
			//"rsist security info=False;initial catalog=Vacations";
		// 
		// PropertiesFullSet
		// 
		this.PropertiesFullSet.DataSetName = "PropertiesFullDataset";
		this.PropertiesFullSet.Locale = new System.Globalization.CultureInfo("en-US");
		// 
		// GetPropertiesAdapter
		// 
		this.GetPropertiesAdapter.SelectCommand = this.sqlSelectCommand1;
		this.GetPropertiesAdapter.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																									   new System.Data.Common.DataTableMapping("Table", "Properties", new System.Data.Common.DataColumnMapping[] {
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
																																																					 new System.Data.Common.DataColumnMapping("Registered", "Registered"),
																																																					 new System.Data.Common.DataColumnMapping("IfPayTravelAgents", "IfPayTravelAgents"),
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
																																																					 new System.Data.Common.DataColumnMapping("TaxIncluded", "TaxIncluded"),
																																																					 new System.Data.Common.DataColumnMapping("DateAvailable", "DateAvailable"),
																																																					 new System.Data.Common.DataColumnMapping("IfDiscounted", "IfDiscounted"),
																																																					 new System.Data.Common.DataColumnMapping("IfLastMinuteCancellations", "IfLastMinuteCancellations"),
																																																					 new System.Data.Common.DataColumnMapping("LastMinuteComments", "LastMinuteComments"),
																																																					 new System.Data.Common.DataColumnMapping("HomeExchangeCityID1", "HomeExchangeCityID1"),
																																																					 new System.Data.Common.DataColumnMapping("HomeExchangeCityID2", "HomeExchangeCityID2"),
																																																					 new System.Data.Common.DataColumnMapping("HomeExchangeCityID3", "HomeExchangeCityID3"),
																																																					 new System.Data.Common.DataColumnMapping("HomeExchangeComments", "HomeExchangeComments")})});
		// 
		// AmenitiesSet
		// 
		this.AmenitiesSet.DataSetName = "AmenitiesDataset";
		this.AmenitiesSet.Locale = new System.Globalization.CultureInfo("en-US");
		// 
		// PhotosSet
		// 
		this.PhotosSet.DataSetName = "PhotosDataset";
		this.PhotosSet.Locale = new System.Globalization.CultureInfo("en-US");
		// 
		// PhotosAdapter
		// 
		this.PhotosAdapter.SelectCommand = this.sqlCommand1;
		this.PhotosAdapter.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																								new System.Data.Common.DataTableMapping("Table", "PropertyPhotos", new System.Data.Common.DataColumnMapping[] {
																																																				  new System.Data.Common.DataColumnMapping("ID", "ID"),
																																																				  new System.Data.Common.DataColumnMapping("PropertyID", "PropertyID"),
																																																				  new System.Data.Common.DataColumnMapping("FileName", "FileName"),
																																																				  new System.Data.Common.DataColumnMapping("OrderNumber", "OrderNumber"),
																																																				  new System.Data.Common.DataColumnMapping("Width", "Width"),
																																																				  new System.Data.Common.DataColumnMapping("Height", "Height")})});
		// 
		// sqlCommand1
		// 
		this.sqlCommand1.CommandText = "SELECT TOP 7 ID, PropertyID, FileName, OrderNumber, Width, Height FROM PropertyPh" +
			"otos WHERE (PropertyID = @PropertyID) ORDER BY OrderNumber";
        this.sqlCommand1.Connection = CommonFunctions.GetConnection();
		this.sqlCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PropertyID", System.Data.SqlDbType.Int, 4, "PropertyID"));
		// 
		// AmenitiesAdapter
		// 
		this.AmenitiesAdapter.InsertCommand = this.sqlInsertCommand1;
		this.AmenitiesAdapter.SelectCommand = this.sqlSelectCommand2;
		this.AmenitiesAdapter.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																								   new System.Data.Common.DataTableMapping("Table", "Amenities", new System.Data.Common.DataColumnMapping[] {
																																																				new System.Data.Common.DataColumnMapping("ID", "ID"),
																																																				new System.Data.Common.DataColumnMapping("Amenity", "Amenity")})});
		// 
		// sqlInsertCommand1
		// 
		this.sqlInsertCommand1.CommandText = "INSERT INTO Amenities(ID, Amenity) VALUES (@ID, @Amenity)";
        this.sqlInsertCommand1.Connection = CommonFunctions.GetConnection();
		this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ID", System.Data.SqlDbType.Int, 4, "ID"));
		this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Amenity", System.Data.SqlDbType.NVarChar, 300, "Amenity"));
		// 
		// sqlSelectCommand2
		// 
		this.sqlSelectCommand2.CommandText = @"SELECT Amenities.ID, Amenities.Amenity FROM Amenities INNER JOIN PropertiesAmenities ON Amenities.ID = PropertiesAmenities.AmenityID WHERE (PropertiesAmenities.PropertyID = @PropertyID) AND (Amenities.Amenity <> 'TV') AND (Amenities.Amenity <> 'VCR') AND (Amenities.Amenity <> 'CD Player')";
        this.sqlSelectCommand2.Connection = CommonFunctions.GetConnection();
		this.sqlSelectCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PropertyID", System.Data.SqlDbType.Int, 4, "PropertyID"));
		// 
		// PropertiesSet
		// 
		this.PropertiesSet.DataSetName = "PropertiesDataset";
		this.PropertiesSet.Locale = new System.Globalization.CultureInfo("en-US");
		// 
		// PropertiesAdapter
		// 
		this.PropertiesAdapter.SelectCommand = this.sqlSelectCommand3;
		this.PropertiesAdapter.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																									new System.Data.Common.DataTableMapping("Table", "Properties", new System.Data.Common.DataColumnMapping[] {
																																																				  new System.Data.Common.DataColumnMapping("ID", "ID"),
																																																				  new System.Data.Common.DataColumnMapping("UserID", "UserID"),
																																																				  new System.Data.Common.DataColumnMapping("Name", "Name"),
																																																				  new System.Data.Common.DataColumnMapping("TypeID", "TypeID"),
																																																				  new System.Data.Common.DataColumnMapping("Address", "Address"),
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
																																																				  new System.Data.Common.DataColumnMapping("IfApproved", "IfApproved"),
																																																				  new System.Data.Common.DataColumnMapping("CityID", "CityID"),
																																																				  new System.Data.Common.DataColumnMapping("IfFinished", "IfFinished"),
																																																				  new System.Data.Common.DataColumnMapping("DateAdded", "DateAdded"),
																																																				  new System.Data.Common.DataColumnMapping("DateStartViewed", "DateStartViewed"),
																																																				  new System.Data.Common.DataColumnMapping("VirtualTour", "VirtualTour"),
																																																				  new System.Data.Common.DataColumnMapping("RatesTable", "RatesTable"),
																																																				  new System.Data.Common.DataColumnMapping("PricesCurrency", "PricesCurrency"),
																																																				  new System.Data.Common.DataColumnMapping("CheckIn", "CheckIn"),
																																																				  new System.Data.Common.DataColumnMapping("CheckOut", "CheckOut"),
																																																				  new System.Data.Common.DataColumnMapping("LodgingTax", "LodgingTax"),
																																																				  new System.Data.Common.DataColumnMapping("TaxIncluded", "TaxIncluded"),
																																																				  new System.Data.Common.DataColumnMapping("DateAvailable", "DateAvailable"),
																																																				  new System.Data.Common.DataColumnMapping("IfDiscounted", "IfDiscounted"),
																																																				  new System.Data.Common.DataColumnMapping("IfLastMinuteCancellations", "IfLastMinuteCancellations"),
																																																				  new System.Data.Common.DataColumnMapping("LastMinuteComments", "LastMinuteComments"),
																																																				  new System.Data.Common.DataColumnMapping("HomeExchangeCityID1", "HomeExchangeCityID1"),
																																																				  new System.Data.Common.DataColumnMapping("HomeExchangeCityID2", "HomeExchangeCityID2"),
																																																				  new System.Data.Common.DataColumnMapping("HomeExchangeCityID3", "HomeExchangeCityID3"),
																																																				  new System.Data.Common.DataColumnMapping("HomeExchangeComments", "HomeExchangeComments")})});
		// 
		// sqlSelectCommand3
		// 
		this.sqlSelectCommand3.CommandText = @"SELECT ID, UserID, Name, TypeID, Address, IfShowAddress, NumBedrooms, NumBaths, NumSleeps, MinimumNightlyRentalID, NumTVs, NumVCRs, NumCDPlayers, Description, Amenities, LocalAttractions, Rates, CancellationPolicy, DepositRequired, IfMoreThan7PhotosAllowed, IfApproved, CityID, IfFinished, DateAdded, DateStartViewed, VirtualTour, RatesTable, PricesCurrency, CheckIn, CheckOut, LodgingTax, TaxIncluded, DateAvailable, IfDiscounted, IfLastMinuteCancellations, LastMinuteComments, HomeExchangeCityID1, HomeExchangeCityID2, HomeExchangeCityID3, HomeExchangeComments FROM Properties WHERE (ID = @PropertyID)";
        this.sqlSelectCommand3.Connection = CommonFunctions.GetConnection();
		this.sqlSelectCommand3.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PropertyID", System.Data.SqlDbType.Int, 4, "ID"));
		// 
		// sqlSelectCommand1
		// 
		this.sqlSelectCommand1.CommandText = "SELECT MinimumNightlyRentalTypes.Name AS MinimumNightlyRental, PropertyTypes.Name" +
			" AS Type, Users.FirstName, Users.LastName, Users.PrimaryTelephone, Users.Country" +
			" AS OwnerCountry, Users.Email, Users.Username, Users.Address AS OwnerAddress, Us" +
			"ers.EveningTelephone, Users.DaytimeTelephone, Users.MobileTelephone, Users.Websi" +
			"te, Users.City AS OwnerCity, Users.State AS OwnerState, Users.Zip AS OwnerZip, U" +
			"sers.Registered, Users.IfPayTravelAgents, Cities.City, StateProvinces.StateProvi" +
			"nce, Countries.Country, Regions.Region, CASE WHEN EXISTS (SELECT * FROM Properti" +
			"esAmenities INNER JOIN Amenities ON PropertiesAmenities.AmenityID = Amenities.ID" +
			" WHERE (PropertiesAmenities.PropertyID = Properties.ID) AND (Amenities.Amenity =" +
			" \'Smoking Permitted\')) THEN \'Yes\' ELSE \'No\' END AS Smoking, CASE WHEN EXISTS (SE" +
			"LECT * FROM PropertiesAmenities INNER JOIN Amenities ON PropertiesAmenities.Amen" +
			"ityID = Amenities.ID WHERE (PropertiesAmenities.PropertyID = Properties.ID) AND " +
			"(Amenities.Amenity = \'Pet Friendly\')) THEN \'Yes\' ELSE \'No\' END AS PetFriendly, P" +
			"roperties.ID, Properties.UserID, Properties.Name, Properties.TypeID, Properties." +
			"Address, Properties.CityID, Properties.IfShowAddress, Properties.NumBedrooms, Pr" +
			"operties.NumBaths, Properties.NumSleeps, Properties.MinimumNightlyRentalID, Prop" +
			"erties.NumTVs, Properties.NumVCRs, Properties.NumCDPlayers, Properties.Descripti" +
			"on, Properties.Amenities, Properties.LocalAttractions, Properties.Rates, Propert" +
			"ies.CancellationPolicy, Properties.DepositRequired, Properties.IfMoreThan7Photos" +
			"Allowed, Properties.IfFinished, Properties.IfApproved, CASE WHEN EXISTS (SELECT " +
			"* FROM Invoices WHERE (Invoices.PropertyID = Properties.ID) AND (PaymentAmount >= InvoiceAmount) AND (GETDATE() <= RenewalDate)) THEN 1" +
			" ELSE 0 END AS IfPaid, Properties.DateAdded, Properties.DateStartViewed, Propert" +
			"ies.VirtualTour, Properties.RatesTable, Properties.PricesCurrency, Properties.Ch" +
			"eckIn, Properties.CheckOut, Properties.LodgingTax, Properties.TaxIncluded, Prope" +
			"rties.DateAvailable, Properties.IfDiscounted, Properties.IfLastMinuteCancellatio" +
			"ns, Properties.LastMinuteComments, Properties.HomeExchangeCityID1, Properties.Ho" +
			"meExchangeCityID2, Properties.HomeExchangeCityID3, Properties.HomeExchangeCommen" +
			"ts FROM Properties INNER JOIN Cities ON Properties.CityID = Cities.ID INNER JOIN" +
			" StateProvinces ON StateProvinces.ID = Cities.StateProvinceID INNER JOIN Countri" +
			"es ON StateProvinces.CountryID = Countries.ID INNER JOIN Regions ON Countries.Re" +
			"gionID = Regions.ID INNER JOIN Users ON Properties.UserID = Users.ID LEFT OUTER " +
			"JOIN MinimumNightlyRentalTypes ON Properties.MinimumNightlyRentalID = MinimumNig" +
			"htlyRentalTypes.ID LEFT OUTER JOIN PropertyTypes ON Properties.TypeID = Property" +
			"Types.ID WHERE (Properties.ID = @PropertyID)";
        this.sqlSelectCommand1.Connection = CommonFunctions.GetConnection();
		this.sqlSelectCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PropertyID", System.Data.SqlDbType.Int, 4, "ID"));
		((System.ComponentModel.ISupportInitialize)(this.PropertiesFullSet)).EndInit();
		((System.ComponentModel.ISupportInitialize)(this.AmenitiesSet)).EndInit();
		((System.ComponentModel.ISupportInitialize)(this.PhotosSet)).EndInit();
		((System.ComponentModel.ISupportInitialize)(this.PropertiesSet)).EndInit();

	}
	#endregion

	protected void SubmitButton_Click(object sender, System.EventArgs e)
	{
		if (!IsValid)
			return;

		//if (CommonFunctions.Connection.State == System.Data.ConnectionState.Closed)
			//CommonFunctions.Connection.Open ();

		PropertiesAdapter.SelectCommand.Parameters["@PropertyID"].Value = propertyid;

		//lock (CommonFunctions.Connection)
			if (PropertiesAdapter.Fill (PropertiesSet) > 0)
			{
				PropertiesSet.Tables["Properties"].Rows[0]["DateAvailable"] = new DateTime (AvailableYear.SelectedIndex + 2005,
					AvailableMonth.SelectedIndex + 1, AvailableDay.SelectedIndex + 1);
				PropertiesSet.Tables["Properties"].Rows[0]["IfDiscounted"] = IfDiscounted.Checked;
				PropertiesSet.Tables["Properties"].Rows[0]["IfLastMinuteCancellations"] = IfLastMinuteCancellations.Checked;
				PropertiesSet.Tables["Properties"].Rows[0]["LastMinuteComments"] = LastMinuteComments.Text;

				//lock (CommonFunctions.Connection)
					PropertiesAdapter.Update (PropertiesSet);
			}

		Response.Redirect (backlinkurl);
	}

	protected void CancelButton_Click(object sender, System.EventArgs e)
	{
		Response.Redirect (backlinkurl);
	}
}
