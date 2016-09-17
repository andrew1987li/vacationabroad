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

public partial class FreeTrial : AdminPage
{
	protected System.Data.SqlClient.SqlDataAdapter GetFreeTrialProperties;
	protected Vacations.PropertiesFullDataset PropertiesSet;
	protected System.Data.SqlClient.SqlCommand sqlSelectCommand1;
	//protected System.Data.SqlClient.SqlConnection Connection;

	protected void Page_Load(object sender, System.EventArgs e)
	{
		userid = AuthenticationManager.UserID;

		//string connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

		//CommonFunctions.Connection.ConnectionString = connectionstring;

		if (Request.Params["SortOrder"] != null)
		{
			string sortfield = "";

			switch (Request.Params["SortOrder"])
			{
				case "1":
					sortfield = "Properties.DateAdded";
					break;
				case "2":
					sortfield = "Properties.ID";
					break;
				case "3":
					sortfield = "Countries.Country";
					break;
				case "4":
					sortfield = "StateProvinces.StateProvince";
					break;
				case "5":
					sortfield = "Users.FirstName";
					break;
				case "6":
					sortfield = "Users.LastName";
					break;
				case "7":
					sortfield = "OwnerCountry";
					break;
				case "8":
					sortfield = "Users.PrimaryTelephone";
					break;
				case "9":
					sortfield = "Users.DaytimeTelephone";
					break;
				case "10":
					sortfield = "Users.EveningTelephone";
					break;
				case "11":
					sortfield = "Users.MobileTelephone";
					break;
				case "12":
					sortfield = "Users.Email";
					break;
			}

			if (sortfield.Length > 0)
				GetFreeTrialProperties.SelectCommand.CommandText += " ORDER BY " + sortfield;
		}

		//lock (CommonFunctions.Connection)
			GetFreeTrialProperties.Fill (PropertiesSet);

		DataBind ();
	}

	public string GetQueryStringWithoutSortOrder ()
	{
		string querystring = Request.QueryString.ToString ();

		querystring = querystring.Replace ("&SortOrder=10", "");
		querystring = querystring.Replace ("&SortOrder=11", "");
		querystring = querystring.Replace ("&SortOrder=12", "");
		querystring = querystring.Replace ("&SortOrder=1", "");
		querystring = querystring.Replace ("&SortOrder=2", "");
		querystring = querystring.Replace ("&SortOrder=3", "");
		querystring = querystring.Replace ("&SortOrder=4", "");
		querystring = querystring.Replace ("&SortOrder=5", "");
		querystring = querystring.Replace ("&SortOrder=6", "");
		querystring = querystring.Replace ("&SortOrder=7", "");
		querystring = querystring.Replace ("&SortOrder=8", "");
		querystring = querystring.Replace ("&SortOrder=9", "");

		querystring = querystring.Replace ("SortOrder=10&", "");
		querystring = querystring.Replace ("SortOrder=11&", "");
		querystring = querystring.Replace ("SortOrder=12&", "");
		querystring = querystring.Replace ("SortOrder=1&", "");
		querystring = querystring.Replace ("SortOrder=2&", "");
		querystring = querystring.Replace ("SortOrder=3&", "");
		querystring = querystring.Replace ("SortOrder=4&", "");
		querystring = querystring.Replace ("SortOrder=5&", "");
		querystring = querystring.Replace ("SortOrder=6&", "");
		querystring = querystring.Replace ("SortOrder=7&", "");
		querystring = querystring.Replace ("SortOrder=8&", "");
		querystring = querystring.Replace ("SortOrder=9&", "");

		return querystring;
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
		this.GetFreeTrialProperties = new System.Data.SqlClient.SqlDataAdapter();
		this.PropertiesSet = new Vacations.PropertiesFullDataset();
		this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
		((System.ComponentModel.ISupportInitialize)(this.PropertiesSet)).BeginInit();
		// 
		// CommonFunctions.Connection
		// 
		//CommonFunctions.Connection.ConnectionString = "workstation id=MAIN;packet size=4096;integrated security=SSPI;data source=MAIN;pe" +
			//"rsist security info=False;initial catalog=Vacations";
		// 
		// GetFreeTrialProperties
		// 
		this.GetFreeTrialProperties.SelectCommand = this.sqlSelectCommand1;
		this.GetFreeTrialProperties.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
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
																																																					   new System.Data.Common.DataColumnMapping("HomeExchangeCityID1", "HomeExchangeCityID1"),
																																																					   new System.Data.Common.DataColumnMapping("HomeExchangeCityID2", "HomeExchangeCityID2"),
																																																					   new System.Data.Common.DataColumnMapping("HomeExchangeCityID3", "HomeExchangeCityID3")})});
		// 
		// PropertiesSet
		// 
		this.PropertiesSet.DataSetName = "PropertiesFullDataset";
		this.PropertiesSet.Locale = new System.Globalization.CultureInfo("en-US");
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
			"erties.NumTVs, Properties.NumVCRs, Properties.NumCDPlayers, Properties.IfMoreThan7Photos" +
			"Allowed, Properties.IfFinished, Properties.IfApproved, CASE WHEN EXISTS (SELECT " +
			"* FROM Invoices WHERE (Invoices.PropertyID = Properties.ID) AND (PaymentAmount >= InvoiceAmount) AND (GETDATE() <= RenewalDate)) THEN 1" +
			" ELSE 0 END AS IfPaid, Properties.DateAdded, Properties.DateStartViewed, Propert" +
			"ies.VirtualTour, Properties.RatesTable, Properties.PricesCurrency, Properties.Ch" +
			"eckIn, Properties.CheckOut, Properties.LodgingTax, Properties.TaxIncluded, Prope" +
			"rties.DateAvailable, Properties.IfDiscounted, Properties.IfLastMinuteCancellatio" +
			"ns, Properties.HomeExchangeCityID1, Properties.HomeExchangeCityID2, Properties.H" +
			"omeExchangeCityID3 FROM Properties INNER JOIN Cities ON Properties.CityID = Citi" +
			"es.ID INNER JOIN StateProvinces ON StateProvinces.ID = Cities.StateProvinceID IN" +
			"NER JOIN Countries ON StateProvinces.CountryID = Countries.ID INNER JOIN Regions" +
			" ON Countries.RegionID = Regions.ID INNER JOIN Users ON Properties.UserID = User" +
			"s.ID LEFT OUTER JOIN MinimumNightlyRentalTypes ON Properties.MinimumNightlyRenta" +
			"lID = MinimumNightlyRentalTypes.ID LEFT OUTER JOIN PropertyTypes ON Properties.T" +
			"ypeID = PropertyTypes.ID WHERE (NOT EXISTS (SELECT * FROM Invoices WHERE (Invoic" +
			"es.PropertyID = Properties.ID) AND (PaymentAmount >= InvoiceAmount) AND (GETDATE() <= RenewalDate))) AND (NOT EXISTS (SELECT * FROM Auctions WHERE Auctions.PropertyID = Properties.ID))";
		this.sqlSelectCommand1.Connection = CommonFunctions.GetConnection();
		((System.ComponentModel.ISupportInitialize)(this.PropertiesSet)).EndInit();

	}
	#endregion
}
