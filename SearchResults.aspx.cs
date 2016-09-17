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

public partial class SearchResults : CommonPage
{
	private int regionid;
	private int countryid;
	private int stateprovinceid;
	private int cityid;
	private string[] amenitiesstr;
	private string[] attractionsstr;
	private ArrayList amenities;
	private ArrayList attractions;
	private int minbedrooms;
	private int maxbedrooms;
	private int minsleeps;
	private int maxsleeps;

	protected Vacations.RegionsDataset RegionsSet;
	protected Vacations.StateProvincesDataset StateProvincesSet;
	protected Vacations.AmenitiesDataset AmenitiesSet;
	protected Vacations.CitiesDataset CitiesSet;
	protected Vacations.CountriesDataset CountriesSet;
	protected Vacations.AttractionsDataset AttractionsSet;
	protected System.Data.SqlClient.SqlDataAdapter AttractionsAdapter;
	protected System.Data.SqlClient.SqlCommand sqlInsertCommand4;
	protected System.Data.SqlClient.SqlCommand sqlSelectCommand1;
	protected System.Data.SqlClient.SqlDataAdapter AmenitiesAdapter;
	protected System.Data.SqlClient.SqlCommand sqlInsertCommand5;
	protected System.Data.SqlClient.SqlCommand sqlSelectCommand3;
	protected System.Data.SqlClient.SqlDataAdapter RegionsAdapter;
	protected System.Data.SqlClient.SqlCommand sqlInsertCommand6;
	protected System.Data.SqlClient.SqlCommand sqlSelectCommand5;
	protected System.Data.SqlClient.SqlDataAdapter CitiesAdapter;
	protected System.Data.SqlClient.SqlCommand sqlInsertCommand1;
	protected System.Data.SqlClient.SqlCommand sqlSelectCommand7;
	protected System.Data.SqlClient.SqlDataAdapter StateProvincesAdapter;
	protected System.Data.SqlClient.SqlCommand sqlInsertCommand3;
	protected System.Data.SqlClient.SqlCommand sqlSelectCommand2;
	protected System.Data.SqlClient.SqlDataAdapter CountriesAdapter;
	protected System.Data.SqlClient.SqlCommand sqlInsertCommand2;
	protected System.Data.SqlClient.SqlCommand sqlSelectCommand4;
	protected System.Data.SqlClient.SqlDataAdapter PropertiesAdapter;
	protected Vacations.PropertiesFullDataset PropertiesFullSet;
	protected System.Web.UI.WebControls.HyperLink BackLink;
	protected System.Data.SqlClient.SqlCommand sqlSelectCommand6;
	//protected System.Data.SqlClient.SqlConnection Connection;

	protected void Page_Load(object sender, System.EventArgs e)
	{
		//string connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

		//CommonFunctions.Connection.ConnectionString = connectionstring;

		//if (CommonFunctions.Connection.State == System.Data.ConnectionState.Closed)
			//CommonFunctions.Connection.Open ();

		if (Request.Params["RegionID"] != null)
			regionid = Convert.ToInt32 (Request.Params["RegionID"]);
		else
			regionid = -1;

		if (Request.Params["CountryID"] != null)
			countryid = Convert.ToInt32 (Request.Params["CountryID"]);
		else
			countryid = -1;

		if (Request.Params["StateProvinceID"] != null)
			stateprovinceid = Convert.ToInt32 (Request.Params["StateProvinceID"]);
		else
			stateprovinceid = -1;

		if (Request.Params["CityID"] != null)
			cityid = Convert.ToInt32 (Request.Params["CityID"]);
		else
			cityid = -1;

		if (Request.Params["Amenities"] != null)
			amenitiesstr = Request.Params["Amenities"].Split (' ');
		else
			amenitiesstr = new string[0];

		if (Request.Params["Attractions"] != null)
			attractionsstr = Request.Params["Attractions"].Split (' ');
		else
			attractionsstr = new string[0];

		amenities = new ArrayList ();
		foreach (string amenity in amenitiesstr)
			try
			{
				amenities.Add (Convert.ToInt32 (amenity));
			}
			catch (Exception)
			{
			}

		attractions = new ArrayList ();
		foreach (string attraction in attractionsstr)
			try
			{
				attractions.Add (Convert.ToInt32 (attraction));
			}
			catch (Exception)
			{
			}

		if (Request.Params["MinBedrooms"] != null)
			minbedrooms = Convert.ToInt32 (Request.Params["MinBedrooms"]);
		else
			minbedrooms = -1;

		if (Request.Params["MaxBedrooms"] != null)
			maxbedrooms = Convert.ToInt32 (Request.Params["MaxBedrooms"]);
		else
			maxbedrooms = -1;

		if (Request.Params["MinSleeps"] != null)
			minsleeps = Convert.ToInt32 (Request.Params["MinSleeps"]);
		else
			minsleeps = -1;

		if (Request.Params["MaxSleeps"] != null)
			maxsleeps = Convert.ToInt32 (Request.Params["MaxSleeps"]);
		else
			maxsleeps = -1;

		SearchedAttractions.Text = "";
		foreach (int attraction in attractions)
		{
			SearchedAttractions.Visible = true;
			SearchedAttractionsLabel.Visible = true;

            object result = null;
            using(SqlConnection connection = CommonFunctions.GetConnection()) {
                connection.Open();

                SqlCommand getattraction = new SqlCommand("SELECT Attraction FROM Attractions WHERE ID=@AttractionID", connection);
                getattraction.Parameters.Add("@AttractionID", SqlDbType.Int, 4, "ID");
                getattraction.Parameters["@AttractionID"].Value = attraction;

                result = getattraction.ExecuteScalar();
                connection.Close();
            }
			if (result is string)
			{
				if (SearchedAttractions.Text.Length > 0)
					SearchedAttractions.Text += ", ";
				else
					SearchedAttractions.Text += " ";
				SearchedAttractions.Text += "\"" + (string)result + "\"";
			}
		}
		SearchedLocation.Text = "";
		if ((stateprovinceid != -1) && (countryid != -1))
		{
			SearchedLocationLabel.Visible = true;
			SearchedLocation.Visible = true;

			if (countryid != -1)
			{
                object result = null;
                using(SqlConnection connection = CommonFunctions.GetConnection()) {
                    connection.Open();
                    SqlCommand getcountry = new SqlCommand("SELECT Country FROM Countries WHERE ID=@CountryID",connection);
                    getcountry.Parameters.Add("@CountryID", System.Data.SqlDbType.Int, 4);
                    getcountry.Parameters["@CountryID"].Value = countryid;

                    //if (CommonFunctions.Connection.State == ConnectionState.Closed)
                    //CommonFunctions.Connection.Open ();

                    result = getcountry.ExecuteScalar();
                    connection.Close();
                }
				if (result is string)
				{
					if (SearchedLocation.Text.Length > 0)
						SearchedLocation.Text += ",";
					SearchedLocation.Text += (string)result;
				}
			}

			if (stateprovinceid != -1)
			{
                object result = null;
                using(SqlConnection connection = CommonFunctions.GetConnection()) {
                    connection.Open();
                    SqlCommand getstateprovince = new SqlCommand("SELECT StateProvince FROM StateProvinces WHERE ID=@StateProvinceID",
                        connection);
                    getstateprovince.Parameters.Add("@StateProvinceID", System.Data.SqlDbType.Int, 4);
                    getstateprovince.Parameters["@StateProvinceID"].Value = stateprovinceid;

                    //if (CommonFunctions.Connection.State == ConnectionState.Closed)
                    //CommonFunctions.Connection.Open ();

                    result = getstateprovince.ExecuteScalar();
                    connection.Close();
                }

				if (result is string)
				{
					if (SearchedLocation.Text.Length > 0)
						SearchedLocation.Text += ",";
					SearchedLocation.Text += (string)result;
				}
			}

			if (cityid != -1)
			{
                object result = null;
                using(SqlConnection connection = CommonFunctions.GetConnection()) {
                    connection.Open();

                    SqlCommand getcity = new SqlCommand("SELECT City FROM Cities WHERE ID=@CityID", connection);
                    getcity.Parameters.Add("@CityID", System.Data.SqlDbType.Int, 4);
                    getcity.Parameters["@CityID"].Value = cityid;

                    //if (CommonFunctions.Connection.State == ConnectionState.Closed)
                    //CommonFunctions.Connection.Open ();

                    result = getcity.ExecuteScalar();
                    connection.Close();
                }

				if (result is string)
				{
					if (SearchedLocation.Text.Length > 0)
						SearchedLocation.Text += ",";
					SearchedLocation.Text += (string)result;
				}
			}

			SearchedLocation.Text = "\"" + SearchedLocation.Text + "\"";
		}

		PropertiesAdapter.SelectCommand.Parameters["@RegionID"].Value = regionid;
		PropertiesAdapter.SelectCommand.Parameters["@CountryID"].Value = countryid;
		PropertiesAdapter.SelectCommand.Parameters["@StateProvinceID"].Value = stateprovinceid;
		PropertiesAdapter.SelectCommand.Parameters["@CityID"].Value = cityid;
		PropertiesAdapter.SelectCommand.Parameters["@MinBedrooms"].Value = minbedrooms;
		PropertiesAdapter.SelectCommand.Parameters["@MaxBedrooms"].Value = maxbedrooms;
		PropertiesAdapter.SelectCommand.Parameters["@MinSleeps"].Value = minsleeps;
		PropertiesAdapter.SelectCommand.Parameters["@MaxSleeps"].Value = maxsleeps;

		foreach (int amenity in amenities)
			PropertiesAdapter.SelectCommand.CommandText += " AND EXISTS(SELECT * FROM PropertiesAmenities " +
				"WHERE (PropertiesAmenities.PropertyID=Properties.ID) AND (PropertiesAmenities.AmenityID=" +
				amenity.ToString () + "))";

		foreach (int attraction in attractions)
			PropertiesAdapter.SelectCommand.CommandText += " AND EXISTS(SELECT * FROM PropertiesAttractions " +
				"WHERE (PropertiesAttractions.PropertyID=Properties.ID) AND (PropertiesAttractions.AttractionID=" +
				attraction.ToString () + "))";

		PropertiesAdapter.SelectCommand.CommandText += " ORDER BY Countries.Country, StateProvinces.StateProvince, Cities.City";

		//lock (CommonFunctions.Connection)
			PropertiesAdapter.Fill (PropertiesFullSet);
		//lock (CommonFunctions.Connection)
			RegionsAdapter.Fill (RegionsSet);
		//lock (CommonFunctions.Connection)
			AmenitiesAdapter.Fill (AmenitiesSet);
		//lock (CommonFunctions.Connection)
			AttractionsAdapter.Fill (AttractionsSet);
		
		if (!IsPostBack)
		{
			DataBind ();

			if (regionid != -1)
				Region.SelectedValue = regionid.ToString ();

			Region_SelectedIndexChanged (Region, null);

			if (countryid != -1)
			{
				Country.SelectedValue = countryid.ToString ();
				Country_SelectedIndexChanged (Region, null);
			}

			if (stateprovinceid != -1)
			{
				StateProvince.SelectedValue = stateprovinceid.ToString ();
				StateProvince_SelectedIndexChanged (Region, null);
			}

			if (cityid != -1)
				City.SelectedValue = cityid.ToString ();
			else
				City.SelectedValue = "0";
		}

		HtmlHead head = Page.Header;

		HtmlMeta keywords = new HtmlMeta ();

		keywords.Name = "keywords";
		keywords.Content = "Vacation Cottages Holiday Cabins Vacation Apartments Holiday Condos Vacation Hotels Holiday" +
			" Lodges Vacation Homes Holiday Inns Vacation Resorts";
		head.Controls.Add (keywords);

		HtmlMeta description = new HtmlMeta ();

		description.Name = "description";
		description.Content = "Vacation Cottages Holiday Cabins Vacation Apartments Holiday Condos Vacation Hotels" +
			" Holiday Lodges Vacation Homes Holiday Inns Vacation Resorts";
		head.Controls.Add (description);
        Page.Header.Controls.Add(new LiteralControl("<link href='/css/StyleSheetBig4.css' rel='stylesheet' type='text/css'></script>"));

	}

	protected void FindLocation_Click(object sender, System.EventArgs e)
	{
		string redirect;

		if (Region.SelectedValue.Length > 0)
			redirect = "SearchResults.aspx?RegionID=" + Region.SelectedValue;
		else
			redirect = "SearchResults.aspx?RegionID=" + regionid.ToString ();

		if (Country.SelectedValue.Length > 0)
			redirect += "&CountryID=" + Country.SelectedValue;
		else
			redirect += "&CountryID=" + countryid.ToString ();

		if (StateProvince.SelectedValue.Length > 0)
			redirect += "&StateProvinceID=" + StateProvince.SelectedValue;
		else
			redirect += "&StateProvinceID=" + stateprovinceid.ToString ();

		if (City.SelectedValue.Length > 0)
		{
			if (City.SelectedValue != "0")
				redirect += "&CityID=" + City.SelectedValue;
		}
		else
			redirect += "&CityID=" + cityid.ToString ();

		string attr = "";
		foreach (int attraction in attractions)
		{
			if (attr.Length > 0)
				attr += "+";
			attr += attraction.ToString ();
		}
		if (attr.Length > 0)
			redirect += "&Attractions=" + attr;

		Response.Redirect (CommonFunctions.PrepareURL (redirect, "Search Page"));
	}
/*
	private void FindAttractions_Click(object sender, System.EventArgs e)
	{
		string redirect;

		redirect = "SearchResults.aspx?RegionID=" + regionid.ToString ();
		redirect += "&CountryID=" + countryid.ToString ();
		redirect += "&StateProvinceID=" + stateprovinceid.ToString ();
		redirect += "&CityID=" + cityid.ToString ();

		string attr = "";
		if ((AttractionsList.SelectedValue.Length > 0) && (AttractionsList.SelectedValue != "0"))
			attractions.Add (Convert.ToInt32 (AttractionsList.SelectedValue));
		foreach (int attraction in attractions)
		{
			if (attr.Length > 0)
				attr += "+";
			attr += attraction.ToString ();
		}
		if (attr.Length > 0)
			redirect += "&Attractions=" + attr;

		Response.Redirect (CommonFunctions.PrepareURL (redirect, "Search Page"));
	}
*/
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
		this.RegionsSet = new Vacations.RegionsDataset();
		this.StateProvincesSet = new Vacations.StateProvincesDataset();
		this.AmenitiesSet = new Vacations.AmenitiesDataset();
		this.CitiesSet = new Vacations.CitiesDataset();
		this.CountriesSet = new Vacations.CountriesDataset();
		this.AttractionsSet = new Vacations.AttractionsDataset();
		this.AttractionsAdapter = new System.Data.SqlClient.SqlDataAdapter();
		this.sqlInsertCommand4 = new System.Data.SqlClient.SqlCommand();
		this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
		this.AmenitiesAdapter = new System.Data.SqlClient.SqlDataAdapter();
		this.sqlInsertCommand5 = new System.Data.SqlClient.SqlCommand();
		this.sqlSelectCommand3 = new System.Data.SqlClient.SqlCommand();
		this.RegionsAdapter = new System.Data.SqlClient.SqlDataAdapter();
		this.sqlInsertCommand6 = new System.Data.SqlClient.SqlCommand();
		this.sqlSelectCommand5 = new System.Data.SqlClient.SqlCommand();
		this.CitiesAdapter = new System.Data.SqlClient.SqlDataAdapter();
		this.sqlInsertCommand1 = new System.Data.SqlClient.SqlCommand();
		this.sqlSelectCommand7 = new System.Data.SqlClient.SqlCommand();
		this.StateProvincesAdapter = new System.Data.SqlClient.SqlDataAdapter();
		this.sqlInsertCommand3 = new System.Data.SqlClient.SqlCommand();
		this.sqlSelectCommand2 = new System.Data.SqlClient.SqlCommand();
		this.CountriesAdapter = new System.Data.SqlClient.SqlDataAdapter();
		this.sqlInsertCommand2 = new System.Data.SqlClient.SqlCommand();
		this.sqlSelectCommand4 = new System.Data.SqlClient.SqlCommand();
		this.PropertiesAdapter = new System.Data.SqlClient.SqlDataAdapter();
		this.sqlSelectCommand6 = new System.Data.SqlClient.SqlCommand();
		this.PropertiesFullSet = new Vacations.PropertiesFullDataset();
		((System.ComponentModel.ISupportInitialize)(this.RegionsSet)).BeginInit();
		((System.ComponentModel.ISupportInitialize)(this.StateProvincesSet)).BeginInit();
		((System.ComponentModel.ISupportInitialize)(this.AmenitiesSet)).BeginInit();
		((System.ComponentModel.ISupportInitialize)(this.CitiesSet)).BeginInit();
		((System.ComponentModel.ISupportInitialize)(this.CountriesSet)).BeginInit();
		((System.ComponentModel.ISupportInitialize)(this.AttractionsSet)).BeginInit();
		((System.ComponentModel.ISupportInitialize)(this.PropertiesFullSet)).BeginInit();
		// 
		// CommonFunctions.Connection
		// 
		//CommonFunctions.Connection.ConnectionString = "workstation id=MAIN;packet size=4096;integrated security=SSPI;data source=MAIN;pe" +
			//"rsist security info=False;initial catalog=Vacations";
		// 
		// RegionsSet
		// 
		this.RegionsSet.DataSetName = "RegionsDataset";
		this.RegionsSet.Locale = new System.Globalization.CultureInfo("en-US");
		// 
		// StateProvincesSet
		// 
		this.StateProvincesSet.DataSetName = "StateProvincesDataset";
		this.StateProvincesSet.Locale = new System.Globalization.CultureInfo("en-US");
		// 
		// AmenitiesSet
		// 
		this.AmenitiesSet.DataSetName = "AmenitiesDataset";
		this.AmenitiesSet.Locale = new System.Globalization.CultureInfo("en-US");
		// 
		// CitiesSet
		// 
		this.CitiesSet.DataSetName = "CitiesDataset";
		this.CitiesSet.Locale = new System.Globalization.CultureInfo("en-US");
		// 
		// CountriesSet
		// 
		this.CountriesSet.DataSetName = "CountriesDataset";
		this.CountriesSet.Locale = new System.Globalization.CultureInfo("en-US");
		// 
		// AttractionsSet
		// 
		this.AttractionsSet.DataSetName = "AttractionsDataset";
		this.AttractionsSet.Locale = new System.Globalization.CultureInfo("en-US");
		// 
		// AttractionsAdapter
		// 
		this.AttractionsAdapter.InsertCommand = this.sqlInsertCommand4;
		this.AttractionsAdapter.SelectCommand = this.sqlSelectCommand1;
		this.AttractionsAdapter.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																									 new System.Data.Common.DataTableMapping("Table", "Attractions", new System.Data.Common.DataColumnMapping[] {
																																																					new System.Data.Common.DataColumnMapping("ID", "ID"),
																																																					new System.Data.Common.DataColumnMapping("Attraction", "Attraction")})});
		// 
		// sqlInsertCommand4
		// 
		this.sqlInsertCommand4.CommandText = "INSERT INTO Attractions(ID, Attraction) VALUES (@ID, @Attraction)";
        this.sqlInsertCommand4.Connection = CommonFunctions.GetConnection();
		this.sqlInsertCommand4.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ID", System.Data.SqlDbType.Int, 4, "ID"));
		this.sqlInsertCommand4.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Attraction", System.Data.SqlDbType.NVarChar, 300, "Attraction"));
		// 
		// sqlSelectCommand1
		// 
		this.sqlSelectCommand1.CommandText = @"SELECT DISTINCT Attractions.ID, Attractions.Attraction FROM Attractions INNER JOIN PropertiesAttractions ON PropertiesAttractions.AttractionID = Attractions.ID INNER JOIN Properties ON PropertiesAttractions.PropertyID = Properties.ID WHERE (Properties.IfFinished = 1) AND (Properties.IfApproved = 1) UNION SELECT 0, 'All Attractions'";
        this.sqlSelectCommand1.Connection = CommonFunctions.GetConnection();
		// 
		// AmenitiesAdapter
		// 
		this.AmenitiesAdapter.InsertCommand = this.sqlInsertCommand5;
		this.AmenitiesAdapter.SelectCommand = this.sqlSelectCommand3;
		this.AmenitiesAdapter.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																								   new System.Data.Common.DataTableMapping("Table", "Amenities", new System.Data.Common.DataColumnMapping[] {
																																																				new System.Data.Common.DataColumnMapping("ID", "ID"),
																																																				new System.Data.Common.DataColumnMapping("Amenity", "Amenity")})});
		// 
		// sqlInsertCommand5
		// 
		this.sqlInsertCommand5.CommandText = "INSERT INTO Amenities(ID, Amenity) VALUES (@ID, @Amenity)";
        this.sqlInsertCommand5.Connection = CommonFunctions.GetConnection();
		this.sqlInsertCommand5.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ID", System.Data.SqlDbType.Int, 4, "ID"));
		this.sqlInsertCommand5.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Amenity", System.Data.SqlDbType.NVarChar, 300, "Amenity"));
		// 
		// sqlSelectCommand3
		// 
		this.sqlSelectCommand3.CommandText = @"SELECT DISTINCT Amenities.ID, Amenities.Amenity FROM Amenities INNER JOIN PropertiesAmenities ON PropertiesAmenities.AmenityID = Amenities.ID INNER JOIN Properties ON PropertiesAmenities.PropertyID = Properties.ID WHERE (Properties.IfFinished = 1) AND (Properties.IfApproved = 1) UNION SELECT 0, 'All Amenities'";
        this.sqlSelectCommand3.Connection = CommonFunctions.GetConnection();
		// 
		// RegionsAdapter
		// 
		this.RegionsAdapter.InsertCommand = this.sqlInsertCommand6;
		this.RegionsAdapter.SelectCommand = this.sqlSelectCommand5;
		this.RegionsAdapter.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																								 new System.Data.Common.DataTableMapping("Table", "Regions", new System.Data.Common.DataColumnMapping[] {
																																																			new System.Data.Common.DataColumnMapping("ID", "ID"),
																																																			new System.Data.Common.DataColumnMapping("Region", "Region")})});
		// 
		// sqlInsertCommand6
		// 
		this.sqlInsertCommand6.CommandText = "INSERT INTO Regions(Region) VALUES (@Region)";
        this.sqlInsertCommand6.Connection = CommonFunctions.GetConnection();
		this.sqlInsertCommand6.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Region", System.Data.SqlDbType.NVarChar, 300, "Region"));
		// 
		// sqlSelectCommand5
		// 
		this.sqlSelectCommand5.CommandText = @"SELECT DISTINCT Regions.ID, Regions.Region FROM Regions INNER JOIN Countries ON Regions.ID = Countries.RegionID INNER JOIN StateProvinces ON Countries.ID = StateProvinces.CountryID INNER JOIN Cities ON StateProvinces.ID = Cities.StateProvinceID INNER JOIN Properties ON Cities.ID = Properties.CityID WHERE (Properties.IfFinished = 1) AND (Properties.IfApproved = 1) ORDER BY Region";
        this.sqlSelectCommand5.Connection = CommonFunctions.GetConnection();
		// 
		// CitiesAdapter
		// 
		this.CitiesAdapter.InsertCommand = this.sqlInsertCommand1;
		this.CitiesAdapter.SelectCommand = this.sqlSelectCommand7;
		this.CitiesAdapter.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																								new System.Data.Common.DataTableMapping("Table", "Cities", new System.Data.Common.DataColumnMapping[] {
																																																		  new System.Data.Common.DataColumnMapping("ID", "ID"),
																																																		  new System.Data.Common.DataColumnMapping("StateProvinceID", "StateProvinceID"),
																																																		  new System.Data.Common.DataColumnMapping("City", "City")})});
		// 
		// sqlInsertCommand1
		// 
		this.sqlInsertCommand1.CommandText = "INSERT INTO Cities(StateProvinceID, City) VALUES (@StateProvinceID, @City)";
        this.sqlInsertCommand1.Connection = CommonFunctions.GetConnection();
		this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@StateProvinceID", System.Data.SqlDbType.Int, 4, "StateProvinceID"));
		this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@City", System.Data.SqlDbType.NVarChar, 300, "City"));
		// 
		// sqlSelectCommand7
		// 
		this.sqlSelectCommand7.CommandText = @"SELECT DISTINCT Cities.ID, Cities.StateProvinceID, Cities.City FROM Cities INNER JOIN Properties ON Cities.ID=Properties.CityID WHERE (Properties.IfFinished = 1) AND (Properties.IfApproved = 1) AND (StateProvinceID = @StateProvinceID) UNION SELECT 0, 0, ' Include All' ORDER BY City";
        this.sqlSelectCommand7.Connection = CommonFunctions.GetConnection();
		this.sqlSelectCommand7.Parameters.Add(new System.Data.SqlClient.SqlParameter("@StateProvinceID", System.Data.SqlDbType.Int, 4, "StateProvinceID"));
		// 
		// StateProvincesAdapter
		// 
		this.StateProvincesAdapter.InsertCommand = this.sqlInsertCommand3;
		this.StateProvincesAdapter.SelectCommand = this.sqlSelectCommand2;
		this.StateProvincesAdapter.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																										new System.Data.Common.DataTableMapping("Table", "StateProvinces", new System.Data.Common.DataColumnMapping[] {
																																																						  new System.Data.Common.DataColumnMapping("ID", "ID"),
																																																						  new System.Data.Common.DataColumnMapping("CountryID", "CountryID"),
																																																						  new System.Data.Common.DataColumnMapping("StateProvince", "StateProvince")})});
		// 
		// sqlInsertCommand3
		// 
		this.sqlInsertCommand3.CommandText = "INSERT INTO StateProvinces(CountryID, StateProvince) VALUES (@CountryID, @StatePr" +
			"ovince)";
        this.sqlInsertCommand3.Connection = CommonFunctions.GetConnection();
		this.sqlInsertCommand3.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CountryID", System.Data.SqlDbType.Int, 4, "CountryID"));
		this.sqlInsertCommand3.Parameters.Add(new System.Data.SqlClient.SqlParameter("@StateProvince", System.Data.SqlDbType.NVarChar, 300, "StateProvince"));
		// 
		// sqlSelectCommand2
		// 
		this.sqlSelectCommand2.CommandText = @"SELECT DISTINCT StateProvinces.ID, StateProvinces.CountryID, StateProvinces.StateProvince FROM StateProvinces INNER JOIN Cities ON StateProvinces.ID = Cities.StateProvinceID INNER JOIN Properties ON Cities.ID = Properties.CityID WHERE (Properties.IfFinished = 1) AND (Properties.IfApproved = 1) AND (StateProvinces.CountryID = @CountryID) ORDER BY StateProvince";
        this.sqlSelectCommand2.Connection = CommonFunctions.GetConnection();
		this.sqlSelectCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CountryID", System.Data.SqlDbType.Int, 4, "CountryID"));
		// 
		// CountriesAdapter
		// 
		this.CountriesAdapter.InsertCommand = this.sqlInsertCommand2;
		this.CountriesAdapter.SelectCommand = this.sqlSelectCommand4;
		this.CountriesAdapter.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																								   new System.Data.Common.DataTableMapping("Table", "Countries", new System.Data.Common.DataColumnMapping[] {
																																																				new System.Data.Common.DataColumnMapping("ID", "ID"),
																																																				new System.Data.Common.DataColumnMapping("RegionID", "RegionID"),
																																																				new System.Data.Common.DataColumnMapping("Country", "Country")})});
		// 
		// sqlInsertCommand2
		// 
		this.sqlInsertCommand2.CommandText = "INSERT INTO Countries(RegionID, Country) VALUES (@RegionID, @Country)";
        this.sqlInsertCommand2.Connection = CommonFunctions.GetConnection();
		this.sqlInsertCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RegionID", System.Data.SqlDbType.Int, 4, "RegionID"));
		this.sqlInsertCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Country", System.Data.SqlDbType.NVarChar, 300, "Country"));
		// 
		// sqlSelectCommand4
		// 
		this.sqlSelectCommand4.CommandText = @"SELECT DISTINCT Countries.ID, Countries.RegionID, Countries.Country FROM Countries INNER JOIN StateProvinces ON Countries.ID = StateProvinces.CountryID INNER JOIN Cities ON StateProvinces.ID = Cities.StateProvinceID INNER JOIN Properties ON Cities.ID = Properties.CityID WHERE (Properties.IfFinished = 1) AND (Properties.IfApproved = 1) AND (Countries.RegionID = @RegionID) ORDER By Country";
        this.sqlSelectCommand4.Connection = CommonFunctions.GetConnection();
		this.sqlSelectCommand4.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RegionID", System.Data.SqlDbType.Int, 4, "RegionID"));
		// 
		// PropertiesAdapter
		// 
		this.PropertiesAdapter.SelectCommand = this.sqlSelectCommand6;
		this.PropertiesAdapter.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
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
		// sqlSelectCommand6
		// 
		this.sqlSelectCommand6.CommandText = "SELECT MinimumNightlyRentalTypes.Name AS MinimumNightlyRental, PropertyTypes.Name" +
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
			"erties.NumTVs, Properties.NumVCRs, Properties.NumCDPlayers, Properties.IfMoreTha" +
			"n7PhotosAllowed, Properties.IfFinished, Properties.IfApproved, CASE WHEN EXISTS " +
			"(SELECT * FROM Invoices WHERE (Invoices.PropertyID = Properties.ID) AND (PaymentAmount >= InvoiceAmount) AND (GETDATE() <= RenewalDate)" +
			") THEN 1 ELSE 0 END AS IfPaid, Properties.DateAdded, Properties.DateStartViewed," +
			" Properties.VirtualTour, Properties.RatesTable, Properties.PricesCurrency, Prope" +
			"rties.CheckIn, Properties.CheckOut, Properties.LodgingTax, Properties.TaxInclude" +
			"d, Properties.DateAvailable, Properties.IfDiscounted, Properties.IfLastMinuteCan" +
			"cellations, Properties.HomeExchangeCityID1, Properties.HomeExchangeCityID2, Prop" +
			"erties.HomeExchangeCityID3 FROM Properties INNER JOIN Cities ON Properties.CityI" +
			"D = Cities.ID INNER JOIN StateProvinces ON StateProvinces.ID = Cities.StateProvi" +
			"nceID INNER JOIN Countries ON StateProvinces.CountryID = Countries.ID INNER JOIN" +
			" Regions ON Countries.RegionID = Regions.ID INNER JOIN Users ON Properties.UserI" +
			"D = Users.ID LEFT OUTER JOIN MinimumNightlyRentalTypes ON Properties.MinimumNigh" +
			"tlyRentalID = MinimumNightlyRentalTypes.ID LEFT OUTER JOIN PropertyTypes ON Prop" +
			"erties.TypeID = PropertyTypes.ID WHERE (Properties.IfFinished = 1) AND (Properti" +
			"es.IfApproved = 1) AND (@RegionID = - 1 OR Regions.ID = @RegionID) AND (@Country" +
			"ID = - 1 OR Countries.ID = @CountryID) AND (@StateProvinceID = - 1 OR StateProvi" +
			"nces.ID = @StateProvinceID) AND (@CityID = - 1 OR Cities.ID = @CityID) AND (@Min" +
			"Bedrooms = - 1 OR Properties.NumBedrooms >= @MinBedrooms) AND (@MaxBedrooms = - " +
			"1 OR Properties.NumBedrooms <= @MaxBedrooms) AND (@MinSleeps = - 1 OR Properties" +
			".NumSleeps >= @MinSleeps) AND (@MaxSleeps = - 1 OR Properties.NumSleeps <= @MaxS" +
			"leeps)";
        this.sqlSelectCommand6.Connection = CommonFunctions.GetConnection();
		this.sqlSelectCommand6.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RegionID", System.Data.SqlDbType.Int, 4, "ID"));
		this.sqlSelectCommand6.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CountryID", System.Data.SqlDbType.Int, 4, "ID"));
		this.sqlSelectCommand6.Parameters.Add(new System.Data.SqlClient.SqlParameter("@StateProvinceID", System.Data.SqlDbType.Int, 4, "ID"));
		this.sqlSelectCommand6.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CityID", System.Data.SqlDbType.Int, 4, "ID"));
		this.sqlSelectCommand6.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MinBedrooms", System.Data.SqlDbType.Int, 4, "NumBedrooms"));
		this.sqlSelectCommand6.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MaxBedrooms", System.Data.SqlDbType.Int, 4, "NumBedrooms"));
		this.sqlSelectCommand6.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MinSleeps", System.Data.SqlDbType.Int, 4, "NumSleeps"));
		this.sqlSelectCommand6.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MaxSleeps", System.Data.SqlDbType.Int, 4, "NumSleeps"));
		// 
		// PropertiesFullSet
		// 
		this.PropertiesFullSet.DataSetName = "PropertiesFullDataset";
		this.PropertiesFullSet.Locale = new System.Globalization.CultureInfo("en-US");
		((System.ComponentModel.ISupportInitialize)(this.RegionsSet)).EndInit();
		((System.ComponentModel.ISupportInitialize)(this.StateProvincesSet)).EndInit();
		((System.ComponentModel.ISupportInitialize)(this.AmenitiesSet)).EndInit();
		((System.ComponentModel.ISupportInitialize)(this.CitiesSet)).EndInit();
		((System.ComponentModel.ISupportInitialize)(this.CountriesSet)).EndInit();
		((System.ComponentModel.ISupportInitialize)(this.AttractionsSet)).EndInit();
		((System.ComponentModel.ISupportInitialize)(this.PropertiesFullSet)).EndInit();

	}
	#endregion

	protected void Region_SelectedIndexChanged(object sender, System.EventArgs e)
	{
		CountriesSet.Clear ();
		CountriesAdapter.SelectCommand.Parameters["@RegionID"].Value = Convert.ToInt32 (Region.SelectedValue);
		//lock (CommonFunctions.Connection)
			CountriesAdapter.Fill (CountriesSet);
		Country.DataBind ();
		Country_SelectedIndexChanged (Country, EventArgs.Empty);
	}

	protected void Country_SelectedIndexChanged(object sender, System.EventArgs e)
	{
		try
		{
			if (Convert.ToInt32 (Country.SelectedValue) != 0)
			{
				StateProvincesSet.Clear ();
				StateProvincesAdapter.SelectCommand.Parameters["@CountryID"].Value = Convert.ToInt32 (Country.SelectedValue);
				//lock (CommonFunctions.Connection)
					StateProvincesAdapter.Fill (StateProvincesSet);
			}
			else
				StateProvincesSet.Clear ();
		}
		catch (Exception)
		{
			StateProvincesSet.Clear ();
		}
		StateProvince.DataBind ();
		StateProvince_SelectedIndexChanged (StateProvince, EventArgs.Empty);
	}

	protected void StateProvince_SelectedIndexChanged(object sender, System.EventArgs e)
	{
		try
		{
			if (Convert.ToInt32 (StateProvince.SelectedValue) != 0)
			{
				CitiesSet.Clear ();
				CitiesAdapter.SelectCommand.Parameters["@StateProvinceID"].Value = Convert.ToInt32 (StateProvince.SelectedValue);
				//lock (CommonFunctions.Connection)
					CitiesAdapter.Fill (CitiesSet);
			}
			else
				CitiesSet.Clear ();
		}
		catch (Exception)
		{
			CitiesSet.Clear ();
		}
		City.DataBind ();
	}
}
