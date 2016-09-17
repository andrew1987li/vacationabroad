using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Configuration;
using System.Net.Mail;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class SendCustomEmail : AdminPage
{
	//protected System.Data.SqlClient.SqlConnection Connection;
	protected Vacations.EmailsDataset EmailsSet;
	protected System.Data.SqlClient.SqlDataAdapter EmailsAdapter;
	protected System.Data.SqlClient.SqlCommand sqlSelectCommand3;
	protected System.Data.SqlClient.SqlDataAdapter GetIDsByUsername;
	protected System.Data.SqlClient.SqlDataAdapter GetTrialIDs;
	protected System.Data.SqlClient.SqlDataAdapter GetAnnualIDs;
	protected System.Data.SqlClient.SqlDataAdapter GetIDsByNumber;
	protected Vacations.PropertiesFullDataset PropertiesSet;
	protected System.Data.SqlClient.SqlCommand sqlSelectCommand5;
	protected System.Data.SqlClient.SqlCommand sqlSelectCommand1;
	protected System.Data.SqlClient.SqlCommand sqlSelectCommand2;
	protected System.Data.SqlClient.SqlCommand sqlSelectCommand4;

	protected void Page_Load(object sender, System.EventArgs e)
	{
		System.Data.SqlClient.SqlCommandBuilder builder = new System.Data.SqlClient.SqlCommandBuilder (EmailsAdapter);
		
		if (!IsPostBack)
		{
            //if (propertyid != -1)
            //{
            //    SendProperty.Checked = true;
            //    SendOwner.Checked = false;
            //    SendTrial.Checked = false;
            //    SendAnnual.Checked = false;

            //    PropertyNumber.Text = propertyid.ToString ();
            //}
		    if (Request.Params["Username"] != null)
			{
				string username = Request.Params["Username"];

				SendProperty.Checked = false;
				SendOwner.Checked = true;
				SendTrial.Checked = false;
				SendAnnual.Checked = false;

				OwnerUsername.Text = username;
			}
			else
			{
				SendProperty.Checked = true;
				SendOwner.Checked = false;
				SendTrial.Checked = false;
				SendAnnual.Checked = false;

                if (propertyid != -1)
                    PropertyNumber.Text = propertyid.ToString ();
			}

			DataBind ();
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
		this.EmailsSet = new Vacations.EmailsDataset();
		this.EmailsAdapter = new System.Data.SqlClient.SqlDataAdapter();
		this.sqlSelectCommand3 = new System.Data.SqlClient.SqlCommand();
		this.GetIDsByUsername = new System.Data.SqlClient.SqlDataAdapter();
		this.GetTrialIDs = new System.Data.SqlClient.SqlDataAdapter();
		this.GetAnnualIDs = new System.Data.SqlClient.SqlDataAdapter();
		this.GetIDsByNumber = new System.Data.SqlClient.SqlDataAdapter();
		this.PropertiesSet = new Vacations.PropertiesFullDataset();
		this.sqlSelectCommand5 = new System.Data.SqlClient.SqlCommand();
		this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
		this.sqlSelectCommand2 = new System.Data.SqlClient.SqlCommand();
		this.sqlSelectCommand4 = new System.Data.SqlClient.SqlCommand();
		((System.ComponentModel.ISupportInitialize)(this.EmailsSet)).BeginInit();
		((System.ComponentModel.ISupportInitialize)(this.PropertiesSet)).BeginInit();
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
		this.EmailsAdapter.SelectCommand = this.sqlSelectCommand3;
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
		// sqlSelectCommand3
		// 
		this.sqlSelectCommand3.CommandText = "SELECT ID, PropertyID, DateTime, ContactName, ContactEmail, ContactTelephone, Arr" +
			"ivalDate, DepartureDate, Nights, Adults, Children, Telephone, Telephone2, Notes," +
			" Email, IfCustom FROM Emails";
        this.sqlSelectCommand3.Connection = CommonFunctions.GetConnection();
		// 
		// GetIDsByUsername
		// 
		this.GetIDsByUsername.SelectCommand = this.sqlSelectCommand1;
		this.GetIDsByUsername.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
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
		// GetTrialIDs
		// 
		this.GetTrialIDs.SelectCommand = this.sqlSelectCommand2;
		this.GetTrialIDs.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
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
		// GetAnnualIDs
		// 
		this.GetAnnualIDs.SelectCommand = this.sqlSelectCommand4;
		this.GetAnnualIDs.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
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
		// GetIDsByNumber
		// 
		this.GetIDsByNumber.SelectCommand = this.sqlSelectCommand5;
		this.GetIDsByNumber.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
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
		// sqlSelectCommand5
		// 
		this.sqlSelectCommand5.CommandText = "SELECT MinimumNightlyRentalTypes.Name AS MinimumNightlyRental, PropertyTypes.Name" +
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
			"ypeID = PropertyTypes.ID WHERE (Properties.ID = @PropertyID)";
        this.sqlSelectCommand5.Connection = CommonFunctions.GetConnection();
		this.sqlSelectCommand5.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PropertyID", System.Data.SqlDbType.Int, 4, "ID"));
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
			"ypeID = PropertyTypes.ID WHERE (Users.Username = @Username)";
        this.sqlSelectCommand1.Connection = CommonFunctions.GetConnection();
		this.sqlSelectCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Username", System.Data.SqlDbType.NVarChar, 30, "Username"));
		// 
		// sqlSelectCommand2
		// 
		this.sqlSelectCommand2.CommandText = "SELECT MinimumNightlyRentalTypes.Name AS MinimumNightlyRental, PropertyTypes.Name" +
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
			"ypeID = PropertyTypes.ID WHERE NOT EXISTS (SELECT * FROM Auctions WHERE (Auctions.PropertyID = Properties.ID) AND EXISTS (SELECT * FROM Transactions WHERE Transactions.AuctionID = Auctions.ID)) AND NOT EXISTS (SELECT * FROM Invoices WHERE (Invoices.PropertyID = Properties.ID) AND (PaymentAmount >= InvoiceAmount) AND (GETDATE() <= RenewalDate))";
        this.sqlSelectCommand2.Connection = CommonFunctions.GetConnection();
		// 
		// sqlSelectCommand4
		// 
		this.sqlSelectCommand4.CommandText = "SELECT MinimumNightlyRentalTypes.Name AS MinimumNightlyRental, PropertyTypes.Name" +
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
			"ypeID = PropertyTypes.ID WHERE EXISTS (SELECT * FROM Auctions WHERE (Auctions.PropertyID = Properties.ID) AND EXISTS (SELECT * FROM Transactions WHERE Transactions.AuctionID = Auctions.ID)) OR EXISTS (SELECT * FROM Invoices WHERE (Invoices.PropertyID = Properties.ID) AND (PaymentAmount >= InvoiceAmount) AND (GETDATE() <= RenewalDate))";
        this.sqlSelectCommand4.Connection = CommonFunctions.GetConnection();
		((System.ComponentModel.ISupportInitialize)(this.EmailsSet)).EndInit();
		((System.ComponentModel.ISupportInitialize)(this.PropertiesSet)).EndInit();

	}
	#endregion

	protected void SendButton_Click(object sender, System.EventArgs e)
	{
		EmailRequired.Validate ();

		if (!EmailRequired.IsValid)
			return;

		PropertiesSet.Clear ();
		if (SendProperty.Checked)
		{
			PropertyNumberRequired.Validate ();
			PropertyNumberValid.Validate ();

			if (!PropertyNumberRequired.IsValid || !PropertyNumberValid.IsValid)
				return;

			GetIDsByNumber.SelectCommand.Parameters["@PropertyID"].Value = Convert.ToInt32 (PropertyNumber.Text);
			//lock (CommonFunctions.Connection)
				GetIDsByNumber.Fill (PropertiesSet);
		}
		else if (SendOwner.Checked)
		{
			OwnerUsernameRequired.Validate ();
			OwnerUsernameValid.Validate ();

			if (!OwnerUsernameRequired.IsValid || !OwnerUsernameValid.IsValid)
				return;

			GetIDsByUsername.SelectCommand.Parameters["@Username"].Value = OwnerUsername.Text;
			//lock (CommonFunctions.Connection)
				GetIDsByUsername.Fill (PropertiesSet);
		}
		else if (SendTrial.Checked)
			//lock (CommonFunctions.Connection)
				GetTrialIDs.Fill (PropertiesSet);
		else if (SendAnnual.Checked)
			//lock (CommonFunctions.Connection)
				GetAnnualIDs.Fill (PropertiesSet);

		foreach (DataRow datarow in PropertiesSet.Tables["Properties"].Rows)
		{
			System.Text.RegularExpressions.Regex regex =
				new System.Text.RegularExpressions.Regex ("^\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$");

			if ((datarow["Email"] is string) && regex.Match ((string)datarow["Email"]).Success)
			{
                SmtpClient smtpclient = new SmtpClient("204.12.125.187", 25);

				MailMessage message = new MailMessage ("noreply@" + CommonFunctions.GetDomainName (), (string)datarow["Email"]);
				message.Subject = CommonFunctions.GetSiteAddress () +
					CommonFunctions.PrepareURL (((string)datarow["Country"]).Replace (" ", "_").ToLower () + "/" +
					((string)datarow["StateProvince"]).Replace (" ", "_").ToLower () + "/" +
					((string)datarow["City"]).Replace (" ", "_").ToLower () + "/" + ((int)datarow["ID"]).ToString () +
					"/default.aspx");
				message.Body = "Dear " + (string)datarow["FirstName"] + " " + (string)datarow["LastName"] + "!\n\n" +
					"You received a new message from " + CommonFunctions.GetSiteName () + " administration:\n\n"+
					EmailBody.Text;
				message.IsBodyHtml = false;

				message.Body = message.Body.Replace("\r", "").Replace("\n", Environment.NewLine);
				message.Headers["Content-Type"] = "text/plain; charset = \"iso-8859-1\"";

				smtpclient.Send (message);
			}

			DataRow newrow = EmailsSet.Tables["Emails"].NewRow ();

			newrow["PropertyID"] = datarow["ID"];
			newrow["DateTime"] = DateTime.Now;
			newrow["Email"] = EmailBody.Text;
			newrow["IfCustom"] = true;

			EmailsSet.Tables["Emails"].Rows.Add (newrow);
		}

		//lock (CommonFunctions.Connection)
			EmailsAdapter.Update (EmailsSet);

		EmailsSent.Text = PropertiesSet.Tables["Properties"].Rows.Count.ToString () + " e-mails sent";
		EmailsSent.Visible = true;
	}
}
