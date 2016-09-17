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

public partial class EditHomeExchanges : ClosedPage
{
	protected Vacations.PhotosDataset PhotosSet;
	protected System.Data.SqlClient.SqlDataAdapter PhotosAdapter;
	protected System.Data.SqlClient.SqlCommand sqlCommand1;
	protected System.Data.SqlClient.SqlDataAdapter PropertiesAdapter;
	protected System.Data.SqlClient.SqlCommand sqlSelectCommand3;
	protected Vacations.AmenitiesDataset AmenitiesSet;
	protected System.Data.SqlClient.SqlCommand sqlSelectCommand1;
	protected System.Data.SqlClient.SqlDataAdapter AmenitiesAdapter;
	protected System.Data.SqlClient.SqlCommand sqlInsertCommand1;
	protected System.Data.SqlClient.SqlCommand sqlSelectCommand2;
	protected Vacations.PropertiesFullDataset PropertiesFullSet;
	protected Vacations.PropertiesDataset PropertiesSet;
	protected System.Data.SqlClient.SqlDataAdapter GetPropertiesAdapter;
	protected System.Data.SqlClient.SqlDataAdapter RegionsAdapter;
	protected System.Data.SqlClient.SqlCommand sqlSelectCommand11;
	protected Vacations.CitiesDataset CitiesSet1;
	protected Vacations.StateProvincesDataset StateProvincesSet1;
	protected Vacations.RegionsDataset RegionsSet1;
	protected System.Data.SqlClient.SqlDataAdapter StateProvincesAdapter;
	protected System.Data.SqlClient.SqlDataAdapter CountriesAdapter;
	protected Vacations.CountriesDataset CountriesSet1;
	protected System.Data.SqlClient.SqlDataAdapter CitiesAdapter;
	protected Vacations.StateProvincesDataset StateProvincesSet2;
	protected Vacations.RegionsDataset RegionsSet2;
	protected Vacations.CountriesDataset CountriesSet2;
	protected Vacations.CitiesDataset CitiesSet2;
	protected Vacations.StateProvincesDataset StateProvincesSet3;
	protected Vacations.RegionsDataset RegionsSet3;
	protected Vacations.CountriesDataset CountriesSet3;
	protected Vacations.CitiesDataset CitiesSet3;
	protected Vacations.FullLocationDataset FullLocationSet;
	protected System.Data.SqlClient.SqlDataAdapter GetLocationInfo;
	protected System.Data.SqlClient.SqlCommand sqlSelectCommand8;
	protected System.Data.SqlClient.SqlCommand sqlSelectCommand4;
	protected System.Data.SqlClient.SqlCommand sqlSelectCommand5;
	protected System.Data.SqlClient.SqlCommand sqlSelectCommand6;
	protected System.Data.SqlClient.SqlCommand sqlSelectCommand7;
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

		//lock (CommonFunctions.Connection)
			RegionsAdapter.Fill (RegionsSet1);
		//lock (CommonFunctions.Connection)
			RegionsAdapter.Fill (RegionsSet2);
		//lock (CommonFunctions.Connection)
			RegionsAdapter.Fill (RegionsSet3);

		if (!IsPostBack)
		{
			DataBind ();

			if ((PropertiesFullSet.Tables["Properties"].Rows[0]["HomeExchangeCityID1"] != null) &&
				!(PropertiesFullSet.Tables["Properties"].Rows[0]["HomeExchangeCityID1"] is DBNull))
			{
				GetLocationInfo.SelectCommand.Parameters["@CityID"].Value =
					PropertiesFullSet.Tables["Properties"].Rows[0]["HomeExchangeCityID1"];
				FullLocationSet.Clear ();
				if (GetLocationInfo.Fill (FullLocationSet) > 0)
				{
					Region1.SelectedValue = ((int)FullLocationSet.Tables["Cities"].Rows[0]["RegionID"]).ToString ();
					Region1_SelectedIndexChanged (Region1, null);
					Country1.SelectedValue = ((int)FullLocationSet.Tables["Cities"].Rows[0]["CountryID"]).ToString ();
					Country1_SelectedIndexChanged (Country1, null);
					StateProvince1.SelectedValue = ((int)FullLocationSet.Tables["Cities"].Rows[0]["StateProvinceID"]).ToString ();
					StateProvince1_SelectedIndexChanged (StateProvince1, null);
					City1.SelectedValue = ((int)PropertiesFullSet.Tables["Properties"].Rows[0]["HomeExchangeCityID1"]).ToString ();
				}
				else
				{
					Region1.SelectedValue = "-2";
					Region1_SelectedIndexChanged (Region1, EventArgs.Empty);
				}
			}
			else
			{
				Region1.SelectedValue = "-2";
				Region1_SelectedIndexChanged (Region1, EventArgs.Empty);
			}

			if ((PropertiesFullSet.Tables["Properties"].Rows[0]["HomeExchangeCityID2"] != null) &&
				!(PropertiesFullSet.Tables["Properties"].Rows[0]["HomeExchangeCityID2"] is DBNull))
			{
				GetLocationInfo.SelectCommand.Parameters["@CityID"].Value =
					PropertiesFullSet.Tables["Properties"].Rows[0]["HomeExchangeCityID2"];
				FullLocationSet.Clear ();
				//lock (CommonFunctions.Connection)
					if (GetLocationInfo.Fill (FullLocationSet) > 0)
					{
						Region2.SelectedValue = ((int)FullLocationSet.Tables["Cities"].Rows[0]["RegionID"]).ToString ();
						Region2_SelectedIndexChanged (Region2, null);
						Country2.SelectedValue = ((int)FullLocationSet.Tables["Cities"].Rows[0]["CountryID"]).ToString ();
						Country2_SelectedIndexChanged (Country2, null);
						StateProvince2.SelectedValue = ((int)FullLocationSet.Tables["Cities"].Rows[0]["StateProvinceID"]).ToString ();
						StateProvince2_SelectedIndexChanged (StateProvince2, null);
						City2.SelectedValue = ((int)PropertiesFullSet.Tables["Properties"].Rows[0]["HomeExchangeCityID2"]).ToString ();
					}
					else
					{
						Region2.SelectedValue = "-2";
						Region2_SelectedIndexChanged (Region1, EventArgs.Empty);
					}
			}
			else
			{
				Region2.SelectedValue = "-2";
				Region2_SelectedIndexChanged (Region1, EventArgs.Empty);
			}

			if ((PropertiesFullSet.Tables["Properties"].Rows[0]["HomeExchangeCityID3"] != null) &&
				!(PropertiesFullSet.Tables["Properties"].Rows[0]["HomeExchangeCityID3"] is DBNull))
			{
				GetLocationInfo.SelectCommand.Parameters["@CityID"].Value =
					PropertiesFullSet.Tables["Properties"].Rows[0]["HomeExchangeCityID3"];
				FullLocationSet.Clear ();
				//lock (CommonFunctions.Connection)
					if (GetLocationInfo.Fill (FullLocationSet) > 0)
					{
						Region3.SelectedValue = ((int)FullLocationSet.Tables["Cities"].Rows[0]["RegionID"]).ToString ();
						Region3_SelectedIndexChanged (Region3, null);
						Country3.SelectedValue = ((int)FullLocationSet.Tables["Cities"].Rows[0]["CountryID"]).ToString ();
						Country3_SelectedIndexChanged (Country3, null);
						StateProvince3.SelectedValue = ((int)FullLocationSet.Tables["Cities"].Rows[0]["StateProvinceID"]).ToString ();
						StateProvince3_SelectedIndexChanged (StateProvince3, null);
						City3.SelectedValue = ((int)PropertiesFullSet.Tables["Properties"].Rows[0]["HomeExchangeCityID3"]).ToString ();
					}
					else
					{
						Region3.SelectedValue = "-2";
						Region3_SelectedIndexChanged (Region1, EventArgs.Empty);
					}
			}
			else
			{
				Region3.SelectedValue = "-2";
				Region3_SelectedIndexChanged (Region1, EventArgs.Empty);
			}
		}
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
		this.PhotosSet = new Vacations.PhotosDataset();
		this.PhotosAdapter = new System.Data.SqlClient.SqlDataAdapter();
		this.sqlCommand1 = new System.Data.SqlClient.SqlCommand();
		this.PropertiesAdapter = new System.Data.SqlClient.SqlDataAdapter();
		this.sqlSelectCommand3 = new System.Data.SqlClient.SqlCommand();
		this.AmenitiesSet = new Vacations.AmenitiesDataset();
		this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
		this.AmenitiesAdapter = new System.Data.SqlClient.SqlDataAdapter();
		this.sqlInsertCommand1 = new System.Data.SqlClient.SqlCommand();
		this.sqlSelectCommand2 = new System.Data.SqlClient.SqlCommand();
		this.PropertiesFullSet = new Vacations.PropertiesFullDataset();
		this.PropertiesSet = new Vacations.PropertiesDataset();
		this.GetPropertiesAdapter = new System.Data.SqlClient.SqlDataAdapter();
		this.RegionsAdapter = new System.Data.SqlClient.SqlDataAdapter();
		this.CitiesSet1 = new Vacations.CitiesDataset();
		this.StateProvincesSet1 = new Vacations.StateProvincesDataset();
		this.RegionsSet1 = new Vacations.RegionsDataset();
		this.StateProvincesAdapter = new System.Data.SqlClient.SqlDataAdapter();
		this.CountriesAdapter = new System.Data.SqlClient.SqlDataAdapter();
		this.CountriesSet1 = new Vacations.CountriesDataset();
		this.CitiesAdapter = new System.Data.SqlClient.SqlDataAdapter();
		this.StateProvincesSet2 = new Vacations.StateProvincesDataset();
		this.RegionsSet2 = new Vacations.RegionsDataset();
		this.CountriesSet2 = new Vacations.CountriesDataset();
		this.CitiesSet2 = new Vacations.CitiesDataset();
		this.StateProvincesSet3 = new Vacations.StateProvincesDataset();
		this.RegionsSet3 = new Vacations.RegionsDataset();
		this.CountriesSet3 = new Vacations.CountriesDataset();
		this.CitiesSet3 = new Vacations.CitiesDataset();
		this.FullLocationSet = new Vacations.FullLocationDataset();
		this.GetLocationInfo = new System.Data.SqlClient.SqlDataAdapter();
		this.sqlSelectCommand8 = new System.Data.SqlClient.SqlCommand();
		this.sqlSelectCommand4 = new System.Data.SqlClient.SqlCommand();
		this.sqlSelectCommand5 = new System.Data.SqlClient.SqlCommand();
		this.sqlSelectCommand6 = new System.Data.SqlClient.SqlCommand();
		this.sqlSelectCommand7 = new System.Data.SqlClient.SqlCommand();
		((System.ComponentModel.ISupportInitialize)(this.PhotosSet)).BeginInit();
		((System.ComponentModel.ISupportInitialize)(this.AmenitiesSet)).BeginInit();
		((System.ComponentModel.ISupportInitialize)(this.PropertiesFullSet)).BeginInit();
		((System.ComponentModel.ISupportInitialize)(this.PropertiesSet)).BeginInit();
		((System.ComponentModel.ISupportInitialize)(this.CitiesSet1)).BeginInit();
		((System.ComponentModel.ISupportInitialize)(this.StateProvincesSet1)).BeginInit();
		((System.ComponentModel.ISupportInitialize)(this.RegionsSet1)).BeginInit();
		((System.ComponentModel.ISupportInitialize)(this.CountriesSet1)).BeginInit();
		((System.ComponentModel.ISupportInitialize)(this.StateProvincesSet2)).BeginInit();
		((System.ComponentModel.ISupportInitialize)(this.RegionsSet2)).BeginInit();
		((System.ComponentModel.ISupportInitialize)(this.CountriesSet2)).BeginInit();
		((System.ComponentModel.ISupportInitialize)(this.CitiesSet2)).BeginInit();
		((System.ComponentModel.ISupportInitialize)(this.StateProvincesSet3)).BeginInit();
		((System.ComponentModel.ISupportInitialize)(this.RegionsSet3)).BeginInit();
		((System.ComponentModel.ISupportInitialize)(this.CountriesSet3)).BeginInit();
		((System.ComponentModel.ISupportInitialize)(this.CitiesSet3)).BeginInit();
		((System.ComponentModel.ISupportInitialize)(this.FullLocationSet)).BeginInit();
		// 
		// CommonFunctions.Connection
		// 
		//CommonFunctions.Connection.ConnectionString = "workstation id=MAIN;packet size=4096;integrated security=SSPI;data source=MAIN;pe" +
			//"rsist security info=False;initial catalog=Vacations";
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
		// AmenitiesSet
		// 
		this.AmenitiesSet.DataSetName = "AmenitiesDataset";
		this.AmenitiesSet.Locale = new System.Globalization.CultureInfo("en-US");
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
		// PropertiesFullSet
		// 
		this.PropertiesFullSet.DataSetName = "PropertiesFullDataset";
		this.PropertiesFullSet.Locale = new System.Globalization.CultureInfo("en-US");
		// 
		// PropertiesSet
		// 
		this.PropertiesSet.DataSetName = "PropertiesDataset";
		this.PropertiesSet.Locale = new System.Globalization.CultureInfo("en-US");
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
		// RegionsAdapter
		// 
		this.RegionsAdapter.SelectCommand = this.sqlSelectCommand4;
		this.RegionsAdapter.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																								 new System.Data.Common.DataTableMapping("Table", "Regions", new System.Data.Common.DataColumnMapping[] {
																																																			new System.Data.Common.DataColumnMapping("ID", "ID"),
																																																			new System.Data.Common.DataColumnMapping("Region", "Region")})});
		// 
		// CitiesSet1
		// 
		this.CitiesSet1.DataSetName = "CitiesDataset";
		this.CitiesSet1.Locale = new System.Globalization.CultureInfo("en-US");
		// 
		// StateProvincesSet1
		// 
		this.StateProvincesSet1.DataSetName = "StateProvincesDataset";
		this.StateProvincesSet1.Locale = new System.Globalization.CultureInfo("en-US");
		// 
		// RegionsSet1
		// 
		this.RegionsSet1.DataSetName = "RegionsDataset";
		this.RegionsSet1.Locale = new System.Globalization.CultureInfo("en-US");
		// 
		// StateProvincesAdapter
		// 
		this.StateProvincesAdapter.SelectCommand = this.sqlSelectCommand5;
		this.StateProvincesAdapter.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																										new System.Data.Common.DataTableMapping("Table", "StateProvinces", new System.Data.Common.DataColumnMapping[] {
																																																						  new System.Data.Common.DataColumnMapping("ID", "ID"),
																																																						  new System.Data.Common.DataColumnMapping("CountryID", "CountryID"),
																																																						  new System.Data.Common.DataColumnMapping("StateProvince", "StateProvince")})});
		// 
		// CountriesAdapter
		// 
		this.CountriesAdapter.SelectCommand = this.sqlSelectCommand6;
		this.CountriesAdapter.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																								   new System.Data.Common.DataTableMapping("Table", "Countries", new System.Data.Common.DataColumnMapping[] {
																																																				new System.Data.Common.DataColumnMapping("ID", "ID"),
																																																				new System.Data.Common.DataColumnMapping("RegionID", "RegionID"),
																																																				new System.Data.Common.DataColumnMapping("Country", "Country")})});
		// 
		// CountriesSet1
		// 
		this.CountriesSet1.DataSetName = "CountriesDataset";
		this.CountriesSet1.Locale = new System.Globalization.CultureInfo("en-US");
		// 
		// CitiesAdapter
		// 
		this.CitiesAdapter.SelectCommand = this.sqlSelectCommand7;
		this.CitiesAdapter.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																								new System.Data.Common.DataTableMapping("Table", "Cities", new System.Data.Common.DataColumnMapping[] {
																																																		  new System.Data.Common.DataColumnMapping("ID", "ID"),
																																																		  new System.Data.Common.DataColumnMapping("StateProvinceID", "StateProvinceID"),
																																																		  new System.Data.Common.DataColumnMapping("City", "City")})});
		// 
		// StateProvincesSet2
		// 
		this.StateProvincesSet2.DataSetName = "StateProvincesDataset";
		this.StateProvincesSet2.Locale = new System.Globalization.CultureInfo("en-US");
		// 
		// RegionsSet2
		// 
		this.RegionsSet2.DataSetName = "RegionsDataset";
		this.RegionsSet2.Locale = new System.Globalization.CultureInfo("en-US");
		// 
		// CountriesSet2
		// 
		this.CountriesSet2.DataSetName = "CountriesDataset";
		this.CountriesSet2.Locale = new System.Globalization.CultureInfo("en-US");
		// 
		// CitiesSet2
		// 
		this.CitiesSet2.DataSetName = "CitiesDataset";
		this.CitiesSet2.Locale = new System.Globalization.CultureInfo("en-US");
		// 
		// StateProvincesSet3
		// 
		this.StateProvincesSet3.DataSetName = "StateProvincesDataset";
		this.StateProvincesSet3.Locale = new System.Globalization.CultureInfo("en-US");
		// 
		// RegionsSet3
		// 
		this.RegionsSet3.DataSetName = "RegionsDataset";
		this.RegionsSet3.Locale = new System.Globalization.CultureInfo("en-US");
		// 
		// CountriesSet3
		// 
		this.CountriesSet3.DataSetName = "CountriesDataset";
		this.CountriesSet3.Locale = new System.Globalization.CultureInfo("en-US");
		// 
		// CitiesSet3
		// 
		this.CitiesSet3.DataSetName = "CitiesDataset";
		this.CitiesSet3.Locale = new System.Globalization.CultureInfo("en-US");
		// 
		// FullLocationSet
		// 
		this.FullLocationSet.DataSetName = "FullLocationDataset";
		this.FullLocationSet.Locale = new System.Globalization.CultureInfo("en-US");
		// 
		// GetLocationInfo
		// 
		this.GetLocationInfo.SelectCommand = this.sqlSelectCommand8;
		this.GetLocationInfo.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																								  new System.Data.Common.DataTableMapping("Table", "Cities", new System.Data.Common.DataColumnMapping[] {
																																																			new System.Data.Common.DataColumnMapping("CityID", "CityID"),
																																																			new System.Data.Common.DataColumnMapping("City", "City"),
																																																			new System.Data.Common.DataColumnMapping("StateProvinceID", "StateProvinceID"),
																																																			new System.Data.Common.DataColumnMapping("StateProvince", "StateProvince"),
																																																			new System.Data.Common.DataColumnMapping("CountryID", "CountryID"),
																																																			new System.Data.Common.DataColumnMapping("Country", "Country"),
																																																			new System.Data.Common.DataColumnMapping("RegionID", "RegionID"),
																																																			new System.Data.Common.DataColumnMapping("Region", "Region")})});
		// 
		// sqlSelectCommand8
		// 
		this.sqlSelectCommand8.CommandText = @"SELECT Cities.ID AS CityID, Cities.City, StateProvinces.ID AS StateProvinceID, StateProvinces.StateProvince, Countries.ID AS CountryID, Countries.Country, Regions.ID AS RegionID, Regions.Region FROM Cities INNER JOIN StateProvinces ON StateProvinces.ID = Cities.StateProvinceID INNER JOIN Countries ON StateProvinces.CountryID = Countries.ID INNER JOIN Regions ON Countries.RegionID = Regions.ID WHERE (Cities.ID = @CityID)";
        this.sqlSelectCommand8.Connection = CommonFunctions.GetConnection();
		this.sqlSelectCommand8.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CityID", System.Data.SqlDbType.Int, 4, "CityID"));
		// 
		// sqlSelectCommand4
		// 
		this.sqlSelectCommand4.CommandText = @"SELECT DISTINCT Regions.ID, Regions.Region FROM Regions INNER JOIN Countries ON Regions.ID = Countries.RegionID INNER JOIN StateProvinces ON Countries.ID = StateProvinces.CountryID INNER JOIN Cities ON StateProvinces.ID = Cities.StateProvinceID INNER JOIN Properties ON Cities.ID = Properties.CityID WHERE (Properties.IfFinished = 1) AND (Properties.IfApproved = 1) UNION SELECT -2 AS ID, ' None' AS Region ORDER BY Regions.Region";
        this.sqlSelectCommand4.Connection = CommonFunctions.GetConnection();
		// 
		// sqlSelectCommand5
		// 
		this.sqlSelectCommand5.CommandText = @"SELECT DISTINCT StateProvinces.ID, StateProvinces.CountryID, StateProvinces.StateProvince FROM StateProvinces INNER JOIN Cities ON StateProvinces.ID = Cities.StateProvinceID INNER JOIN Properties ON Cities.ID = Properties.CityID WHERE (Properties.IfFinished = 1) AND (Properties.IfApproved = 1) AND (StateProvinces.CountryID = @CountryID) ORDER BY StateProvinces.StateProvince";
        this.sqlSelectCommand5.Connection = CommonFunctions.GetConnection();
		this.sqlSelectCommand5.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CountryID", System.Data.SqlDbType.Int, 4, "CountryID"));
		// 
		// sqlSelectCommand6
		// 
		this.sqlSelectCommand6.CommandText = @"SELECT DISTINCT Countries.ID, Countries.RegionID, Countries.Country FROM Countries INNER JOIN StateProvinces ON Countries.ID = StateProvinces.CountryID INNER JOIN Cities ON StateProvinces.ID = Cities.StateProvinceID INNER JOIN Properties ON Cities.ID = Properties.CityID WHERE (Properties.IfFinished = 1) AND (Properties.IfApproved = 1) AND (Countries.RegionID = @RegionID OR @RegionID = - 1) ORDER BY Countries.Country";
        this.sqlSelectCommand6.Connection = CommonFunctions.GetConnection();
		this.sqlSelectCommand6.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RegionID", System.Data.SqlDbType.Int, 4, "RegionID"));
		// 
		// sqlSelectCommand7
		// 
		this.sqlSelectCommand7.CommandText = @"SELECT DISTINCT Cities.ID, Cities.StateProvinceID, Cities.City FROM Cities INNER JOIN Properties ON Cities.ID = Properties.CityID WHERE (Properties.IfFinished = 1) AND (Properties.IfApproved = 1) AND (Cities.StateProvinceID = @StateProvinceID) ORDER BY Cities.City";
        this.sqlSelectCommand7.Connection = CommonFunctions.GetConnection();
		this.sqlSelectCommand7.Parameters.Add(new System.Data.SqlClient.SqlParameter("@StateProvinceID", System.Data.SqlDbType.Int, 4, "StateProvinceID"));
		((System.ComponentModel.ISupportInitialize)(this.PhotosSet)).EndInit();
		((System.ComponentModel.ISupportInitialize)(this.AmenitiesSet)).EndInit();
		((System.ComponentModel.ISupportInitialize)(this.PropertiesFullSet)).EndInit();
		((System.ComponentModel.ISupportInitialize)(this.PropertiesSet)).EndInit();
		((System.ComponentModel.ISupportInitialize)(this.CitiesSet1)).EndInit();
		((System.ComponentModel.ISupportInitialize)(this.StateProvincesSet1)).EndInit();
		((System.ComponentModel.ISupportInitialize)(this.RegionsSet1)).EndInit();
		((System.ComponentModel.ISupportInitialize)(this.CountriesSet1)).EndInit();
		((System.ComponentModel.ISupportInitialize)(this.StateProvincesSet2)).EndInit();
		((System.ComponentModel.ISupportInitialize)(this.RegionsSet2)).EndInit();
		((System.ComponentModel.ISupportInitialize)(this.CountriesSet2)).EndInit();
		((System.ComponentModel.ISupportInitialize)(this.CitiesSet2)).EndInit();
		((System.ComponentModel.ISupportInitialize)(this.StateProvincesSet3)).EndInit();
		((System.ComponentModel.ISupportInitialize)(this.RegionsSet3)).EndInit();
		((System.ComponentModel.ISupportInitialize)(this.CountriesSet3)).EndInit();
		((System.ComponentModel.ISupportInitialize)(this.CitiesSet3)).EndInit();
		((System.ComponentModel.ISupportInitialize)(this.FullLocationSet)).EndInit();

	}
	#endregion

	protected void Region1_SelectedIndexChanged(object sender, System.EventArgs e)
	{
		CountriesSet1.Clear ();
		try
		{
			if (Convert.ToInt32 (Region1.SelectedValue) != 0)
			{
				CountriesAdapter.SelectCommand.Parameters["@RegionID"].Value = Convert.ToInt32 (Region1.SelectedValue);
				//lock (CommonFunctions.Connection)
					CountriesAdapter.Fill (CountriesSet1);
			}
		}
		catch (Exception)
		{
		}

		Country1.DataBind ();
		Country1_SelectedIndexChanged (Country1, EventArgs.Empty);
	}

	protected void Country1_SelectedIndexChanged(object sender, System.EventArgs e)
	{
		StateProvincesSet1.Clear ();
		try
		{
			if (Convert.ToInt32 (Country1.SelectedValue) != 0)
			{
				StateProvincesAdapter.SelectCommand.Parameters["@CountryID"].Value = Convert.ToInt32 (Country1.SelectedValue);
				//lock (CommonFunctions.Connection)
					StateProvincesAdapter.Fill (StateProvincesSet1);
			}
		}
		catch (Exception)
		{
		}

		StateProvince1.DataBind ();
		StateProvince1_SelectedIndexChanged (StateProvince1, EventArgs.Empty);
	}

	protected void StateProvince1_SelectedIndexChanged(object sender, System.EventArgs e)
	{
		CitiesSet1.Clear ();
		try
		{
			if (Convert.ToInt32 (StateProvince1.SelectedValue) != 0)
			{
				CitiesAdapter.SelectCommand.Parameters["@StateProvinceID"].Value = Convert.ToInt32 (StateProvince1.SelectedValue);
				//lock (CommonFunctions.Connection)
					CitiesAdapter.Fill (CitiesSet1);
			}
		}
		catch (Exception)
		{
		}

		City1.DataBind ();
	}

	protected void Region2_SelectedIndexChanged(object sender, System.EventArgs e)
	{
		CountriesSet2.Clear ();
		try
		{
			if (Convert.ToInt32 (Region2.SelectedValue) != 0)
			{
				CountriesAdapter.SelectCommand.Parameters["@RegionID"].Value = Convert.ToInt32 (Region2.SelectedValue);
				//lock (CommonFunctions.Connection)
					CountriesAdapter.Fill (CountriesSet2);
			}
		}
		catch (Exception)
		{
		}

		Country2.DataBind ();
		Country2_SelectedIndexChanged (Country2, EventArgs.Empty);
	}

	protected void Country2_SelectedIndexChanged(object sender, System.EventArgs e)
	{
		StateProvincesSet2.Clear ();
		try
		{
			if (Convert.ToInt32 (Country2.SelectedValue) != 0)
			{
				StateProvincesAdapter.SelectCommand.Parameters["@CountryID"].Value = Convert.ToInt32 (Country2.SelectedValue);
				//lock (CommonFunctions.Connection)
					StateProvincesAdapter.Fill (StateProvincesSet2);
			}
		}
		catch (Exception)
		{
		}

		StateProvince2.DataBind ();
		StateProvince2_SelectedIndexChanged (StateProvince2, EventArgs.Empty);
	}

	protected void StateProvince2_SelectedIndexChanged(object sender, System.EventArgs e)
	{
		CitiesSet2.Clear ();
		try
		{
			if (Convert.ToInt32 (StateProvince2.SelectedValue) != 0)
			{
				CitiesAdapter.SelectCommand.Parameters["@StateProvinceID"].Value = Convert.ToInt32 (StateProvince2.SelectedValue);
				//lock (CommonFunctions.Connection)
					CitiesAdapter.Fill (CitiesSet2);
			}
		}
		catch (Exception)
		{
		}

		City2.DataBind ();
	}

	protected void Region3_SelectedIndexChanged(object sender, System.EventArgs e)
	{
		CountriesSet3.Clear ();
		try
		{
			if (Convert.ToInt32 (Region3.SelectedValue) != 0)
			{
				CountriesAdapter.SelectCommand.Parameters["@RegionID"].Value = Convert.ToInt32 (Region3.SelectedValue);
				//lock (CommonFunctions.Connection)
					CountriesAdapter.Fill (CountriesSet3);
			}
		}
		catch (Exception)
		{
		}

		Country3.DataBind ();
		Country3_SelectedIndexChanged (Country3, EventArgs.Empty);
	}

	protected void Country3_SelectedIndexChanged(object sender, System.EventArgs e)
	{
		StateProvincesSet3.Clear ();
		try
		{
			if (Convert.ToInt32 (Country3.SelectedValue) != 0)
			{
				StateProvincesAdapter.SelectCommand.Parameters["@CountryID"].Value = Convert.ToInt32 (Country3.SelectedValue);
				//lock (CommonFunctions.Connection)
					StateProvincesAdapter.Fill (StateProvincesSet3);
			}
		}
		catch (Exception)
		{
		}

		StateProvince3.DataBind ();
		StateProvince3_SelectedIndexChanged (StateProvince3, EventArgs.Empty);
	}

	protected void StateProvince3_SelectedIndexChanged(object sender, System.EventArgs e)
	{
		CitiesSet3.Clear ();
		try
		{
			if (Convert.ToInt32 (StateProvince3.SelectedValue) != 0)
			{
				CitiesAdapter.SelectCommand.Parameters["@StateProvinceID"].Value = Convert.ToInt32 (StateProvince3.SelectedValue);
				//lock (CommonFunctions.Connection)
					CitiesAdapter.Fill (CitiesSet3);
			}
		}
		catch (Exception)
		{
		}

		City3.DataBind ();
	}

	protected void SubmitButton_Click(object sender, System.EventArgs e)
	{
		if (!IsValid)
			return;

		//if (CommonFunctions.Connection.State == System.Data.ConnectionState.Closed)
			//CommonFunctions.Connection.Open ();

		PropertiesAdapter.SelectCommand.Parameters["@PropertyID"].Value = propertyid;

		if (CommonFunctions.SyncFill (PropertiesAdapter, PropertiesSet) > 0)
		{
			if (City1.SelectedValue.Length > 0)
				PropertiesSet.Tables["Properties"].Rows[0]["HomeExchangeCityID1"] = Convert.ToInt32 (City1.SelectedValue);
			else
				PropertiesSet.Tables["Properties"].Rows[0]["HomeExchangeCityID1"] = DBNull.Value;
			
			if (City2.SelectedValue.Length > 0)
				PropertiesSet.Tables["Properties"].Rows[0]["HomeExchangeCityID2"] = Convert.ToInt32 (City2.SelectedValue);
			else
				PropertiesSet.Tables["Properties"].Rows[0]["HomeExchangeCityID2"] = DBNull.Value;

			if (City3.SelectedValue.Length > 0)
				PropertiesSet.Tables["Properties"].Rows[0]["HomeExchangeCityID3"] = Convert.ToInt32 (City3.SelectedValue);
			else
				PropertiesSet.Tables["Properties"].Rows[0]["HomeExchangeCityID3"] = DBNull.Value;

			PropertiesSet.Tables["Properties"].Rows[0]["HomeExchangeComments"] = HomeExchangeComments.Text;

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
