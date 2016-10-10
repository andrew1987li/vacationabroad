using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class Default : CommonPage
{
    protected Vacations.CountriesStates CountriesStates;
    protected Vacations.RegionsDataset RegionsSet;
    protected Vacations.StateProvincesDataset StateProvincesSet;
    protected Vacations.AmenitiesDataset AmenitiesSet;
    protected System.Data.SqlClient.SqlDataAdapter CountriesAdapter;
    protected System.Data.SqlClient.SqlDataAdapter AmenitiesAdapter;
    protected System.Data.SqlClient.SqlDataAdapter RegionsAdapter;
    protected System.Data.SqlClient.SqlDataAdapter AttractionsAdapter;
    protected Vacations.CitiesDataset CitiesSet;
    protected Vacations.CountriesDataset CountriesSet;
    protected System.Data.SqlClient.SqlDataAdapter StateProvincesAdapter;
    protected Vacations.AttractionsDataset AttractionsSet;
    protected System.Data.SqlClient.SqlDataAdapter CitiesAdapter;
    protected System.Data.SqlClient.SqlCommand sqlInsertCommand1;
    protected System.Data.SqlClient.SqlCommand sqlSelectCommand7;
    protected System.Web.UI.WebControls.Label Label2;
    protected System.Web.UI.WebControls.Label Label3;
    protected System.Web.UI.WebControls.Label Label4;
    protected System.Data.SqlClient.SqlCommand sqlSelectCommand1;
    protected System.Data.SqlClient.SqlCommand sqlInsertCommand4;
    protected System.Data.SqlClient.SqlCommand sqlSelectCommand3;
    protected System.Data.SqlClient.SqlCommand sqlInsertCommand5;
    protected System.Web.UI.WebControls.RequiredFieldValidator CityRequired;
    protected System.Data.SqlClient.SqlCommand sqlSelectCommand2;
    protected System.Data.SqlClient.SqlCommand sqlInsertCommand3;
    protected System.Data.SqlClient.SqlCommand sqlSelectCommand5;
    protected System.Data.SqlClient.SqlCommand sqlInsertCommand6;
    protected System.Web.UI.WebControls.HyperLink Hyperlink1;
    protected Vacations.PropertiesFullDataset PropertiesFullSet;
    protected System.Data.SqlClient.SqlDataAdapter PropertiesAdapter;
    protected Vacations.CountriesDataset CountriesSet2;
    protected System.Data.SqlClient.SqlCommand sqlSelectCommand4;
    protected System.Data.SqlClient.SqlCommand sqlSelectCommand6;
    protected System.Data.SqlClient.SqlDataAdapter GetStateProvinces;
    protected System.Data.SqlClient.SqlDataAdapter GetCountries;
    //protected System.Data.SqlClient.SqlConnection Connection;

    private string regions = "";

    protected void Page_Load(object sender, System.EventArgs e)
    {
        //footerPropDescContainer.Visible = false;

        //string connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;


        //CommonFunctions.Connection.ConnectionString = connectionstring;

        //if (CommonFunctions.Connection.State == System.Data.ConnectionState.Closed)
        //CommonFunctions.Connection.Open ();

        //lock (CommonFunctions.Connection)
        PropertiesAdapter.Fill(PropertiesFullSet);

        //lock (CommonFunctions.Connection)
        RegionsAdapter.Fill(RegionsSet);
        //lock (CommonFunctions.Connection)
        CountriesAdapter.Fill(CountriesSet);
        //lock (CommonFunctions.Connection)
        StateProvincesAdapter.Fill(StateProvincesSet);
        //lock (CommonFunctions.Connection)
        CitiesAdapter.Fill(CitiesSet);

        //lock (CommonFunctions.Connection)
        AmenitiesAdapter.Fill(AmenitiesSet);
        //lock (CommonFunctions.Connection)
        AttractionsAdapter.Fill(AttractionsSet);

        //if (Master.FindControl ("BodyTag") is HtmlGenericControl)
        //{
        //    HtmlGenericControl body = (HtmlGenericControl)Master.FindControl ("BodyTag");
        //    body.Attributes["onload"] = "InitializeDropdowns ();";
        //}

        string temp = "Vacation rentals at ";



        //((System.Web.UI.WebControls.Image)Master.FindControl("Logo")).AlternateText = temp + "Vacations-Abroad.com";
        //((System.Web.UI.WebControls.Image)Master.FindControl ("MainLogo")).AlternateText = temp + "@ Vacations-Abroad.com";

        foreach (DataRow datarow in RegionsSet.Tables["Regions"].Rows)
            if (datarow["Region"] is string)
                regions += " " + (string)datarow["Region"];

        HtmlHead head = Page.Header;
        //Page.ClientScript.RegisterClientScriptInclude("aKeyToIdentifyIt", "/scripts/countryStateCity.js");

        string Keywords = "North America Vacation Rentals, North America Vacation Apartments, North America Family Beach Resorts, North America Romantic Boutique Hotels";
        HtmlMeta keywords = new HtmlMeta();

        keywords.Name = "keywords";
        if (PropertiesFullSet.Tables["Properties"].Rows.Count < 1)
            keywords.Content = "View property";
        else
            keywords.Content = Keywords.Replace("%regions%", regions.Trim());

        head.Controls.Add(keywords);
        string Description = "Book now your North America Vacation rental. Explore the Caribbean, Canada, Mexico, USA, Panama, Costa Rica, Guatemala, Nicaragua.";
        HtmlMeta description = new HtmlMeta();

        description.Name = "description";
        if (PropertiesFullSet.Tables["Properties"].Rows.Count < 1)
            description.Content = "View property";
        else
            description.Content = Description.Replace("%regions%", regions.Trim());

        head.Controls.Add(description);

        if (!IsPostBack)
        {
            DataBind();
            //Page.Header.Controls.Add(new LiteralControl("<link href='http://vacations-abroad.com/css/StyleSheet.css' rel='stylesheet' type='text/css' />"));
        }
        DataSet ds = Utility.dsGrab("RegionTextList");
        


    }
    public string GenerateCountryLinks(string regionID)
    {
        DataTable dtCountryList = new DataTable();
        DataTable dtStateList = new DataTable();
        StringBuilder sb = new StringBuilder();
        //----------------------------North America
        //north amer
        dtCountryList = VADBCommander.CountiesByRegionList(regionID);
        //dtCountryList = VADBCommander.CountriesByRegionList(regionID);
        if (dtCountryList.Rows.Count > 0)
        {
            //sb.AppendLine("<ul class=\"TripleListMain\">");
            foreach (DataRow row in dtCountryList.Rows)
            {
                //we list the country first
                sb.AppendLine("<a class=\"mainPgCountry\" href=\"" + CommonFunctions.PrepareURL(row["country"].ToString().ToLower().Replace(" ", "_") + "/default.aspx") + "\"><b>" + row["country"].ToString().ToUpper() + "</b></a>, ");
                bool stateDown = false;

                //we grab all of the states for that country

                dtStateList = VADBCommander.StateProvinceByCountryList(row["id"].ToString());

                if (dtStateList.Rows.Count > 0)
                {
                    if (dtStateList.Rows.Count > 0)
                    {
                        foreach (DataRow rowTemp in dtStateList.Rows)
                        {
                            DataTable dtCityList = VADBCommander.CityStateCountryRegionList(rowTemp["id"].ToString());
                            //add cities to lower
                            if (dtCityList.Rows.Count > 0)
                            {
                                sb.AppendLine("<a class=\"mainState\" href=\"" + CommonFunctions.PrepareURL(row["country"].ToString().ToLower().Replace(" ", "_").Replace(" ", "_").ToLower() + "/" + rowTemp["stateprovince"].ToString().Replace(" ", "_").ToLower() + "/default.aspx") + "\">" + rowTemp["stateprovince"].ToString().Replace(" ", "&nbsp;").Replace("-", "&#8209;") + "</a>");
                                sb.AppendLine(":  ");
                                foreach (DataRow row1 in dtCityList.Rows)
                                {
                                    sb.AppendLine("<a class=\"mainCity\" href=\"" + CommonFunctions.PrepareURL(row["country"].ToString().ToLower().Replace(" ", "_") + "/" + rowTemp["stateProvince"].ToString().Replace(" ", "_").ToLower() + "/" + row1["city"].ToString().ToLower().Replace(" ", "_") + "/default.aspx") + "\" class=\"mainCity\">" + row1["city"].ToString().Replace(" ", "&nbsp;").Replace("-", "&#8209;") + "</a>, ");
                                }
                            }
                            else
                            {
                                sb.AppendLine("<a class=\"mainState\" href=\"" + CommonFunctions.PrepareURL(row["country"].ToString().ToLower().Replace(" ", "_").Replace(" ", "_").ToLower() + "/" + rowTemp["stateprovince"].ToString().Replace(" ", "_").ToLower() + "/default.aspx") + "\">" + rowTemp["stateprovince"].ToString().Replace(" ", "&nbsp;").Replace("-", "&#8209;") + "</a>");
                            }
                        }
                    }
                    else
                    {
                        sb.AppendLine(", ");

                    }

                } /**/

            }
        }
        string str = sb.ToString();
        str = str.Remove(str.Length - 2);
        return str;

    }

    public string GetTitle()
    {
        return "North America Vacation Rentals | Vacations-Abroad.com";
    }
  

    public string DropDownScript()
    {
        StringBuilder script = new StringBuilder();

        script.Append("var initialregion = -1;\n");
        script.Append("var initialcountry = -1;\n");
        script.Append("var initialstateprovince = -1;\n");
        script.Append("var initialcity = -1;\n");
        script.Append("var defaultpage = true;\n");

        script.Append("var numregions = " + RegionsSet.Tables["Regions"].Rows.Count.ToString() + ";\n");
        script.Append("var regionids = new Array (");
        foreach (DataRow datarow in RegionsSet.Tables["Regions"].Rows)
        {
            script.Append(datarow["ID"].ToString());
            script.Append(", ");
        }
        if (RegionsSet.Tables["Regions"].Rows.Count > 0)
            script.Remove(script.Length - 2, 2);
        script.Append(");\n");
        script.Append("var regionstrs = new Array (");
        foreach (DataRow datarow in RegionsSet.Tables["Regions"].Rows)
        {
            script.Append("\"");
            script.Append(datarow["Region"].ToString());
            script.Append("\", ");
        }
        if (RegionsSet.Tables["Regions"].Rows.Count > 0)
            script.Remove(script.Length - 2, 2);
        script.Append(");\n");

        script.Append("var numcountries = " + CountriesSet.Tables["Countries"].Rows.Count.ToString() + ";\n");
        script.Append("var countryids = new Array (");
        foreach (DataRow datarow in CountriesSet.Tables["Countries"].Rows)
        {
            script.Append(datarow["ID"].ToString());
            script.Append(", ");
        }
        if (CountriesSet.Tables["Countries"].Rows.Count > 0)
            script.Remove(script.Length - 2, 2);
        script.Append(");\n");
        script.Append("var countryregions = new Array (");
        foreach (DataRow datarow in CountriesSet.Tables["Countries"].Rows)
        {
            if (datarow["RegionID"] is DBNull)
                script.Append("0");
            else
                script.Append(datarow["RegionID"].ToString());
            script.Append(", ");
        }
        if (CountriesSet.Tables["Countries"].Rows.Count > 0)
            script.Remove(script.Length - 2, 2);
        script.Append(");\n");
        script.Append("var countrystrs = new Array (");
        foreach (DataRow datarow in CountriesSet.Tables["Countries"].Rows)
        {
            script.Append("\"");
            script.Append(datarow["Country"].ToString());
            script.Append("\", ");
        }
        if (CountriesSet.Tables["Countries"].Rows.Count > 0)
            script.Remove(script.Length - 2, 2);
        script.Append(");\n");

        script.Append("var numprovinces = " + StateProvincesSet.Tables["StateProvinces"].Rows.Count.ToString() + ";\n");
        script.Append("var provinceids = new Array (");
        foreach (DataRow datarow in StateProvincesSet.Tables["StateProvinces"].Rows)
        {
            script.Append(datarow["ID"].ToString());
            script.Append(", ");
        }
        if (StateProvincesSet.Tables["StateProvinces"].Rows.Count > 0)
            script.Remove(script.Length - 2, 2);
        script.Append(");\n");
        script.Append("var provincecountries = new Array (");
        foreach (DataRow datarow in StateProvincesSet.Tables["StateProvinces"].Rows)
        {
            if (datarow["CountryID"] is DBNull)
                script.Append("0");
            else
                script.Append(datarow["CountryID"].ToString());
            script.Append(", ");
        }
        if (StateProvincesSet.Tables["StateProvinces"].Rows.Count > 0)
            script.Remove(script.Length - 2, 2);
        script.Append(");\n");
        script.Append("var provincestrs = new Array (");
        foreach (DataRow datarow in StateProvincesSet.Tables["StateProvinces"].Rows)
        {
            script.Append("\"");
            script.Append(datarow["StateProvince"].ToString());
            script.Append("\", ");
        }
        if (StateProvincesSet.Tables["StateProvinces"].Rows.Count > 0)
            script.Remove(script.Length - 2, 2);
        script.Append(");\n");

        script.Append("var numcities = " + CitiesSet.Tables["Cities"].Rows.Count.ToString() + ";\n");
        script.Append("var cityids = new Array (");
        foreach (DataRow datarow in CitiesSet.Tables["Cities"].Rows)
        {
            script.Append(datarow["ID"].ToString());
            script.Append(", ");
        }
        if (CitiesSet.Tables["Cities"].Rows.Count > 0)
            script.Remove(script.Length - 2, 2);
        script.Append(");\n");
        script.Append("var cityprovinces = new Array (");
        foreach (DataRow datarow in CitiesSet.Tables["Cities"].Rows)
        {
            if (datarow["StateProvinceID"] is DBNull)
                script.Append("0");
            else
                script.Append(datarow["StateProvinceID"].ToString());
            script.Append(", ");
        }
        if (CitiesSet.Tables["Cities"].Rows.Count > 0)
            script.Remove(script.Length - 2, 2);
        script.Append(");\n");
        script.Append("var citystrs = new Array (");
        foreach (DataRow datarow in CitiesSet.Tables["Cities"].Rows)
        {
            script.Append("\"");
            script.Append(datarow["City"].ToString());
            script.Append("\", ");
        }
        if (CitiesSet.Tables["Cities"].Rows.Count > 0)
            script.Remove(script.Length - 2, 2);
        script.Append(");\n");

        //return script.ToString();
        return "";
    }

    protected void FindByLocation_Click(object sender, System.EventArgs e)
    {
        //NothingFound.Visible = false;

        int regionid = -1;
        if (Request.Params["Region"] != null)
            regionid = Convert.ToInt32(Request.Params["Region"]);

        int countryid = -1;
        if (Request.Params["Country"] != null)
            countryid = Convert.ToInt32(Request.Params["Country"]);

        int stateprovinceid = -1;
        if (Request.Params["StateProvince"] != null)
            stateprovinceid = Convert.ToInt32(Request.Params["StateProvince"]);

        int cityid = -1;
        if (Request.Params["City"] != null)
            cityid = Convert.ToInt32(Request.Params["City"]);


        Response.Redirect(CommonFunctions.PrepareURL("SearchResults.aspx?RegionID=" + regionid.ToString() +
            "&CountryID=" + countryid.ToString() + "&StateProvinceID=" + stateprovinceid.ToString() + "&CityID=" +
            cityid.ToString(), "Home"), true);
    }





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
        this.CountriesAdapter = new System.Data.SqlClient.SqlDataAdapter();
        this.sqlSelectCommand4 = new System.Data.SqlClient.SqlCommand();
        this.AmenitiesAdapter = new System.Data.SqlClient.SqlDataAdapter();
        this.sqlInsertCommand5 = new System.Data.SqlClient.SqlCommand();
        this.sqlSelectCommand3 = new System.Data.SqlClient.SqlCommand();
        this.RegionsAdapter = new System.Data.SqlClient.SqlDataAdapter();
        this.sqlInsertCommand6 = new System.Data.SqlClient.SqlCommand();
        this.sqlSelectCommand5 = new System.Data.SqlClient.SqlCommand();
        this.AttractionsAdapter = new System.Data.SqlClient.SqlDataAdapter();
        this.sqlInsertCommand4 = new System.Data.SqlClient.SqlCommand();
        this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
        this.CitiesSet = new Vacations.CitiesDataset();
        this.CountriesSet = new Vacations.CountriesDataset();
        this.StateProvincesAdapter = new System.Data.SqlClient.SqlDataAdapter();
        this.sqlInsertCommand3 = new System.Data.SqlClient.SqlCommand();
        this.sqlSelectCommand2 = new System.Data.SqlClient.SqlCommand();
        this.AttractionsSet = new Vacations.AttractionsDataset();
        this.CitiesAdapter = new System.Data.SqlClient.SqlDataAdapter();
        this.sqlInsertCommand1 = new System.Data.SqlClient.SqlCommand();
        this.sqlSelectCommand7 = new System.Data.SqlClient.SqlCommand();
        this.PropertiesFullSet = new Vacations.PropertiesFullDataset();
        this.PropertiesAdapter = new System.Data.SqlClient.SqlDataAdapter();
        this.sqlSelectCommand6 = new System.Data.SqlClient.SqlCommand();
        this.CountriesSet2 = new Vacations.CountriesDataset();
        ((System.ComponentModel.ISupportInitialize)(this.RegionsSet)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.StateProvincesSet)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.AmenitiesSet)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.CitiesSet)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.CountriesSet)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.AttractionsSet)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.PropertiesFullSet)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.CountriesSet2)).BeginInit();
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
        // CountriesAdapter
        // 
        this.CountriesAdapter.SelectCommand = this.sqlSelectCommand4;
        this.CountriesAdapter.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																								   new System.Data.Common.DataTableMapping("Table", "Countries", new System.Data.Common.DataColumnMapping[] {
																																																				new System.Data.Common.DataColumnMapping("ID", "ID"),
																																																				new System.Data.Common.DataColumnMapping("RegionID", "RegionID"),
																																																				new System.Data.Common.DataColumnMapping("Country", "Country")})});
        // 
        // sqlSelectCommand4
        // 
        this.sqlSelectCommand4.CommandText = "CountryDistinctList";
        this.sqlSelectCommand4.CommandType = CommandType.StoredProcedure;
        this.sqlSelectCommand4.Connection = CommonFunctions.GetConnection();
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
        this.sqlSelectCommand3.CommandText = "AmenityDistinctList";
        this.sqlSelectCommand3.Connection = CommonFunctions.GetConnection();
        this.sqlSelectCommand3.CommandType = CommandType.StoredProcedure;
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
        this.sqlSelectCommand5.CommandText = "RegionDistinctList";
        this.sqlSelectCommand5.Connection = CommonFunctions.GetConnection();
        this.sqlSelectCommand5.CommandType = CommandType.StoredProcedure;
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
        this.sqlSelectCommand1.CommandText = "AttractionsDistinctList";
        this.sqlSelectCommand1.Connection = CommonFunctions.GetConnection();
        this.sqlInsertCommand1.CommandType = CommandType.StoredProcedure;
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
        this.sqlInsertCommand3.CommandText = "INSERT INTO StateProvinces(CountryID, StateProvince) VALUES (@CountryID, @StateProvince)";
        this.sqlInsertCommand3.Connection = CommonFunctions.GetConnection();
        this.sqlInsertCommand3.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CountryID", System.Data.SqlDbType.Int, 4, "CountryID"));
        this.sqlInsertCommand3.Parameters.Add(new System.Data.SqlClient.SqlParameter("@StateProvince", System.Data.SqlDbType.NVarChar, 300, "StateProvince"));
        // 
        // sqlSelectCommand2
        // 
        this.sqlSelectCommand2.CommandText = "StateProvinceDistinctList";
        this.sqlSelectCommand2.Connection = CommonFunctions.GetConnection();
        this.sqlSelectCommand2.CommandType = CommandType.StoredProcedure;
        // 
        // AttractionsSet
        // 
        this.AttractionsSet.DataSetName = "AttractionsDataset";
        this.AttractionsSet.Locale = new System.Globalization.CultureInfo("en-US");
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
        this.sqlSelectCommand7.CommandText = @"SELECT DISTINCT Cities.ID, Cities.StateProvinceID, Cities.City FROM Cities INNER JOIN Properties ON Cities.ID=Properties.CityID WHERE (Properties.IfFinished = 1) AND (Properties.IfApproved = 1) UNION SELECT -1, -1, ' Include All' ORDER BY City";
        this.sqlSelectCommand7.Connection = CommonFunctions.GetConnection();
        // 
        // PropertiesFullSet
        // 
        this.PropertiesFullSet.DataSetName = "PropertiesFullDataset";
        this.PropertiesFullSet.Locale = new System.Globalization.CultureInfo("en-US");
        // 
        // PropertiesAdapter
        // 
        this.PropertiesAdapter.SelectCommand = this.sqlSelectCommand6;
        this.PropertiesAdapter.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																									new System.Data.Common.DataTableMapping("Table", "Properties", new System.Data.Common.DataColumnMapping[] {
																																																				  new System.Data.Common.DataColumnMapping("MinimumNightlyRental", "MinimumNightlyRental"),
																																																				  new System.Data.Common.DataColumnMapping("TaxIncluded", "TaxIncluded")})});
        // 
        // sqlSelectCommand6
        // 
        this.sqlSelectCommand6.CommandText = "TopThreePropertiesList";
        this.sqlSelectCommand6.Connection = CommonFunctions.GetConnection();
        this.sqlSelectCommand6.CommandType = CommandType.StoredProcedure;
        // 
        // CountriesSet2
        // 
        this.CountriesSet2.DataSetName = "CountriesDataset";
        this.CountriesSet2.Locale = new System.Globalization.CultureInfo("en-US");
        this.CountriesStates = new Vacations.CountriesStates();
        this.GetStateProvinces = new System.Data.SqlClient.SqlDataAdapter();
        this.GetCountries = new System.Data.SqlClient.SqlDataAdapter();
        this.sqlSelectCommand2 = new System.Data.SqlClient.SqlCommand();
        this.CountriesStates = new Vacations.CountriesStates();
        this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
        ((System.ComponentModel.ISupportInitialize)(this.CountriesStates)).BeginInit();
        // 
        // CommonFunctions.Connection
        // 
        //CommonFunctions.Connection.ConnectionString = "workstation id=MAIN;packet size=4096;integrated security=SSPI;data source=MAIN;pe" +
        //"rsist security info=False;initial catalog=Vacations";
        // 
        // GetStateProvinces
        // 
        this.GetStateProvinces.SelectCommand = this.sqlSelectCommand1;
        this.GetStateProvinces.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																									new System.Data.Common.DataTableMapping("Table", "StateProvinces", new System.Data.Common.DataColumnMapping[] {
																																																					  new System.Data.Common.DataColumnMapping("ID", "ID"),
																																																					  new System.Data.Common.DataColumnMapping("CountryID", "CountryID"),
																																																					  new System.Data.Common.DataColumnMapping("StateProvince", "StateProvince"),
																																																					  new System.Data.Common.DataColumnMapping("Country", "Country")})});
        // 
        // GetCountries
        // 
        this.GetCountries.SelectCommand = this.sqlSelectCommand2;
        this.GetCountries.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																							   new System.Data.Common.DataTableMapping("Table", "Countries", new System.Data.Common.DataColumnMapping[] {
																																																			new System.Data.Common.DataColumnMapping("ID", "ID"),
																																																			new System.Data.Common.DataColumnMapping("RegionID", "RegionID"),
																																																			new System.Data.Common.DataColumnMapping("Country", "Country")})});
        // 
        // sqlSelectCommand2
        // 
        //this.sqlSelectCommand2.CommandText = @"SELECT ID, RegionID, Country FROM Countries WHERE (RegionID = @RegionID) AND EXISTS (SELECT * FROM (Properties INNER JOIN Cities ON Properties.CityID = Cities.ID) INNER JOIN StateProvinces ON Cities.StateProvinceID = StateProvinces.ID WHERE (StateProvinces.CountryID = Countries.ID) AND (Properties.IfFinished = 1) AND (Properties.IfApproved = 1) AND NOT EXISTS (SELECT * FROM Auctions WHERE PropertyID = Properties.ID)) ORDER BY Country";
        this.sqlSelectCommand2.CommandText = @"SELECT ID, RegionID, Country FROM Countries WHERE (RegionID = @RegionID) AND EXISTS (SELECT * FROM (Properties INNER JOIN Cities ON Properties.CityID = Cities.ID) INNER JOIN StateProvinces ON Cities.StateProvinceID = StateProvinces.ID WHERE (StateProvinces.CountryID = Countries.ID) AND (Properties.IfFinished = 1) AND (Properties.IfApproved = 1) ) ORDER BY Country";
        this.sqlSelectCommand2.Connection = CommonFunctions.GetConnection();
        this.sqlSelectCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RegionID", System.Data.SqlDbType.Int, 4, "RegionID"));
        this.sqlSelectCommand1.CommandText = @"SELECT StateProvinces.ID, StateProvinces.CountryID, StateProvinces.StateProvince, Countries.Country FROM StateProvinces INNER JOIN Countries ON StateProvinces.CountryID = Countries.ID WHERE (Countries.RegionID = @RegionID) AND EXISTS (SELECT * FROM Properties INNER JOIN Cities ON Properties.CityID = Cities.ID WHERE (Cities.StateProvinceID = StateProvinces.ID) AND (Properties.IfFinished = 1) AND (Properties.IfApproved = 1)) ORDER BY StateProvinces.StateProvince";
        this.sqlSelectCommand1.Connection = CommonFunctions.GetConnection();
        this.sqlSelectCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RegionID", System.Data.SqlDbType.Int, 4, "RegionID"));
        // 


        // CountriesStates
        // 
        this.CountriesStates.DataSetName = "CountriesStates";
        this.CountriesStates.Locale = new System.Globalization.CultureInfo("en-US");

        //GetStateProvinces = new System.Data.SqlClient.SqlDataAdapter();
        //System.Data.SqlClient.SqlDataAdapter GetCountries= new System.Data.SqlClient.SqlDataAdapter();

        this.GetCountries.SelectCommand.Parameters["@RegionID"].Value = 6;
        this.GetStateProvinces.SelectCommand.Parameters["@RegionID"].Value = 6;

        ////lock (CommonFunctions.Connection)
        this.GetCountries.Fill(this.CountriesStates);
        ////lock (CommonFunctions.Connection)
        this.GetStateProvinces.Fill(this.CountriesStates);

        this.CountriesStates.Relations.Add("CountriesStates", this.CountriesStates.Tables["Countries"].Columns["ID"],
            this.CountriesStates.Tables["StateProvinces"].Columns["CountryID"]);
        //ltlAsia.Text = ds.Tables[0].Rows[1]["RegionTextValue"].ToString();
        //ltlEurope.Text = ds.Tables[0].Rows[2]["RegionTextValue"].ToString();
        //ltlNorthAmerica.Text = ds.Tables[0].Rows[3]["RegionTextValue"].ToString();
        //ltlSouthAmerica.Text = ds.Tables[0].Rows[4]["RegionTextValue"].ToString();
        //ltlOceania.Text = ds.Tables[0].Rows[5]["RegionTextValue"].ToString();
        //ltlAsiaList.Text = GenerateCountryLinks("2");
        //ltlOceaniaList.Text = GenerateCountryLinks("3");
        //ltlSouthAmericaList.Text = GenerateCountryLinks("9");
        //ltlEuropeList.Text = GenerateCountryLinks("6");
        //ltlNorthAmericaList.Text = GenerateCountryLinks("8");

        ((System.ComponentModel.ISupportInitialize)(this.RegionsSet)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.StateProvincesSet)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.AmenitiesSet)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.CitiesSet)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.CountriesSet)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.AttractionsSet)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.PropertiesFullSet)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.CountriesSet2)).EndInit();

    }



}

