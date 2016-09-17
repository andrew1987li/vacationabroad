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

public partial class DefaultOriginal : CommonPage
{
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
    //protected System.Data.SqlClient.SqlConnection Connection;

    private string regions = "";

    protected void Page_Load(object sender, System.EventArgs e)
    {
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



        ((System.Web.UI.WebControls.Image)Master.FindControl("Logo")).AlternateText = temp + "Vacations-Abroad.com";
        //((System.Web.UI.WebControls.Image)Master.FindControl ("MainLogo")).AlternateText = temp + "@ Vacations-Abroad.com";

        foreach (DataRow datarow in RegionsSet.Tables["Regions"].Rows)
            if (datarow["Region"] is string)
                regions += " " + (string)datarow["Region"];

        HtmlHead head = Page.Header;
        Page.ClientScript.RegisterClientScriptInclude("aKeyToIdentifyIt", "/scripts/countryStateCity.js");
        HtmlMeta keywords = new HtmlMeta();

        keywords.Name = "keywords";
        if (PropertiesFullSet.Tables["Properties"].Rows.Count < 1)
            keywords.Content = "View property";
        else
            keywords.Content = Keywords.Text.Replace("%regions%", regions.Trim());

        head.Controls.Add(keywords);
        HtmlMeta description = new HtmlMeta();

        description.Name = "description";
        if (PropertiesFullSet.Tables["Properties"].Rows.Count < 1)
            description.Content = "View property";
        else
            description.Content = Description.Text.Replace("%regions%", regions.Trim());

        head.Controls.Add(description);

        if (!IsPostBack)
        {
            DataBind();

            Page.Header.Controls.Add(new LiteralControl("<link href='http://vacations-abroad.com/css/StyleSheet.css' rel='stylesheet' type='text/css'></script>"));
        }
        FillRegionMid();
    }

    public string GetTitle()
    {
        return Title.Text.Replace("%regions%", regions.Trim());
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

        return script.ToString();
    }

    protected void FindByLocation_Click(object sender, System.EventArgs e)
    {
        NothingFound.Visible = false;

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

    protected void FindByPropertyNumber_Click(object sender, System.EventArgs e)
    {
        NothingFound.Visible = false;

        PropertyNumberRequired.Validate();
        PropertyNumberFormat.Validate();

        if (!PropertyNumberRequired.IsValid || !PropertyNumberFormat.IsValid)
            return;

        int propertyid = Convert.ToInt32(PropertyNumber.Text);

        object retval = null;
        using (SqlConnection connection = CommonFunctions.GetConnection())
        {
            connection.Open();
            SqlCommand check = new SqlCommand("SELECT COUNT(*) FROM Properties WHERE ID = @PropertyID", connection);
            check.Parameters.Add("@PropertyID", SqlDbType.Int, 4, "PropertyID");
            check.Parameters["@PropertyID"].Value = propertyid;

            retval = check.ExecuteScalar();
            connection.Close();
        }

        if ((retval is int) && ((int)retval > 0))
        {
            //Response.Redirect (CommonFunctions.PrepareURL ("ViewProperty.aspx?PropertyID=" + propertyid.ToString (), "Home page"));
            string country = string.Empty, stateProvince = string.Empty,
                   city = string.Empty;

            using (SqlConnection connection = CommonFunctions.GetConnection())
            {
                connection.Open();

                string sql = "select Country, StateProvince, City from Countries, StateProvinces, Cities, Properties where Countries.Id = StateProvinces.CountryId and StateProvinces.Id = Cities.StateProvinceId and Cities.Id = Properties.CityId and Properties.Id =@PropertyID";

                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add("@PropertyID", SqlDbType.Int,4,"Properties.Id");
                cmd.Parameters["PropertyID"].Value = propertyid.ToString();
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();

                country = reader.GetString(0);
                stateProvince = reader.GetString(1);
                city = reader.GetString(2);

                reader.Close();
                connection.Close();
            }
            Response.Redirect(CommonFunctions.PrepareURL(country.Replace(" ", "_") + "/" + stateProvince.Replace(" ", "_") + "/" + city.Replace(" ", "_") + "/" + propertyid.ToString() + "/default.aspx"));
        }
        else
        {
            NothingFound.Visible = true;
        }
            
    }

    protected void FindByAttractions_Click(object sender, System.EventArgs e)
    {
        NothingFound.Visible = false;

        //AttractionRequired.Validate();

        //if (!AttractionRequired.IsValid)
        //    return;

        //Response.Redirect(CommonFunctions.PrepareURL("SearchResults.aspx?Attractions=" + AttractionsList.SelectedValue, "Home page"));
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
        this.sqlSelectCommand5.CommandText ="RegionDistinctList";
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
        ((System.ComponentModel.ISupportInitialize)(this.RegionsSet)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.StateProvincesSet)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.AmenitiesSet)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.CitiesSet)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.CountriesSet)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.AttractionsSet)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.PropertiesFullSet)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.CountriesSet2)).EndInit();

    }
    #endregion
    

    public void FillRegionMid()
    {
        DBConnection obj = new DBConnection();
        DataTable dt = new DataTable();
        DataTable dtTemp = new DataTable();
        string query = "";
        try
        {
            //africa
            dt = VADBCommander.CountiesByRegionList("1");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    divAfrica.InnerHtml += "<a class=\"mainPgCountry\" href=\"" + CommonFunctions.PrepareURL(row["country"].ToString().ToLower().Replace(" ", "_") + "/default.aspx") + "\"><b>" + row["country"].ToString() + "</b></a><br />";

                    dtTemp = VADBCommander.StateProvinceByCountryList(row["id"].ToString());
                    if (dtTemp.Rows.Count > 0)
                    {
                        foreach (DataRow rowTemp in dtTemp.Rows)
                        {
                            divAfrica.InnerHtml += "<a class=\"mainState\" href=\"" + CommonFunctions.PrepareURL(row["country"].ToString().ToLower().Replace(" ", "_").Replace(" ", "_").ToLower() +
                                "/" + rowTemp["stateprovince"].ToString().Replace(" ", "_").ToLower() + "/default.aspx") + "\">" + rowTemp["stateprovince"].ToString().Replace(" ", "&nbsp;").Replace("-", "&#8209;") + "</a>";


                            //add cities to lower
                            

                            DataTable dtTemp2 = VADBCommander.CityStateCountryRegionList(rowTemp["id"].ToString());
                            if (dtTemp2.Rows.Count > 0)
                            {
                                divAfrica.InnerHtml += ":  ";
                                foreach (DataRow row1 in dtTemp2.Rows)
                                {
                                    divAfrica.InnerHtml += "<a class=\"mainCity\" href=\"" + CommonFunctions.PrepareURL(row["country"].ToString().ToLower().Replace(" ", "_") + "/" +
                                        rowTemp["stateProvince"].ToString().Replace(" ", "_").ToLower() + "/" + row1["city"].ToString().ToLower().Replace(" ", "_") + "/default.aspx") + "\" class=\"mainCity\">" + row1["city"].ToString().Replace(" ", "&nbsp;").Replace("-", "&#8209;") + "</a>, ";
                                }
                                divAfrica.InnerHtml = divAfrica.InnerHtml.Remove(divAfrica.InnerHtml.Length - 2, 2);
                                //divAfrica.InnerHtml += "</font>";
                            }
                            //add cities to lower
                            divAfrica.InnerHtml += ", ";
                        }
                        divAfrica.InnerHtml = divAfrica.InnerHtml.Remove(divAfrica.InnerHtml.Length - 2, 2);
                        divAfrica.InnerHtml += "<br />";
                    }
                }
            }


            //asia
            dt = VADBCommander.CountiesByRegionList("2");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    divAsia.InnerHtml += "<a class=\"mainPgCountry\" href=\"" + CommonFunctions.PrepareURL(row["country"].ToString().ToLower().Replace(" ", "_") + "/default.aspx") +
                        "\"><b>" + row["country"].ToString() + "</b></a><br />";

                    dtTemp = VADBCommander.StateProvinceByCountryList(row["id"].ToString());
                    if (dtTemp.Rows.Count > 0)
                    {
                        foreach (DataRow rowTemp in dtTemp.Rows)
                        {
                            divAsia.InnerHtml += "<a class=\"mainState\" href=\"" + CommonFunctions.PrepareURL(row["country"].ToString().ToLower().Replace(" ", "_").Replace(" ", "_").ToLower() +
                                "/" + rowTemp["stateprovince"].ToString().Replace(" ", "_").ToLower() + "/default.aspx") + "\">" + rowTemp["stateprovince"].ToString().Replace(" ", "&nbsp;").Replace("-", "&#8209;") + "</a>";

                            //add cities to lower

                            DataTable dtTemp2 =VADBCommander.CityStateCountryRegionList(rowTemp["id"].ToString());
                            if (dtTemp2.Rows.Count > 0)
                            {
                                divAsia.InnerHtml += ":  ";
                                foreach (DataRow row1 in dtTemp2.Rows)
                                {
                                    divAsia.InnerHtml += "<a class=\"mainCity\" href=\"" + CommonFunctions.PrepareURL(row["country"].ToString().ToLower().Replace(" ", "_") + "/" +
                                        rowTemp["stateProvince"].ToString().Replace(" ", "_").ToLower() + "/" + row1["city"].ToString().ToLower().Replace(" ", "_") + "/default.aspx") + "\" class=\"mainCity\">" + row1["city"].ToString().Replace(" ", "&nbsp;").Replace("-", "&#8209;") + "</a>, ";
                                }
                                divAsia.InnerHtml = divAsia.InnerHtml.Remove(divAsia.InnerHtml.Length - 2, 2);
                                //divAsia.InnerHtml += "</font>";
                            }
                            //add cities to lower
                            divAsia.InnerHtml += ", ";
                        }
                        divAsia.InnerHtml = divAsia.InnerHtml.Remove(divAsia.InnerHtml.Length - 2, 2);
                        divAsia.InnerHtml += "<br />";
                    }
                }
            }

            //caribbean
            dt = VADBCommander.CountiesByRegionList("4");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    divCaribbean.InnerHtml += "<a class=\"mainPgCountry\" href=\"" + CommonFunctions.PrepareURL(row["country"].ToString().ToLower().Replace(" ", "_") + "/default.aspx") +
                        "\"><b>" + row["country"].ToString() + "</b></a><br />";
                    bool stateDown = false;

                    dtTemp = VADBCommander.StateProvinceByCountryList(row["id"].ToString());
                    if (dtTemp.Rows.Count > 0)
                    {
                        bool remBr = true;
                        foreach (DataRow rowTemp in dtTemp.Rows)
                        {
                            
                            DataTable dtTemp2 = VADBCommander.CityStateCountryRegionList(rowTemp["id"].ToString());
                            //add cities to lower

                            if (dtTemp2.Rows.Count > 0)
                            {
                                if (stateDown == true)
                                {
                                    if (divCaribbean.InnerHtml.Substring(divCaribbean.InnerHtml.Length - 6, 6) != "<br />")
                                        divCaribbean.InnerHtml += "<br />";
                                }

                                divCaribbean.InnerHtml += "<a class=\"mainState\" href=\"" + CommonFunctions.PrepareURL(row["country"].ToString().ToLower().Replace(" ", "_").Replace(" ", "_").ToLower() +
                                "/" + rowTemp["stateprovince"].ToString().Replace(" ", "_").ToLower() + "/default.aspx") + "\">" + rowTemp["stateprovince"].ToString().Replace(" ", "&nbsp;").Replace("-", "&#8209;") + "</a>";
                                divCaribbean.InnerHtml += ":  ";
                                foreach (DataRow row1 in dtTemp2.Rows)
                                {
                                    divCaribbean.InnerHtml += "<a class=\"mainCity\" href=\"" + CommonFunctions.PrepareURL(row["country"].ToString().ToLower().Replace(" ", "_") + "/" +
                                        rowTemp["stateProvince"].ToString().Replace(" ", "_").ToLower() + "/" + row1["city"].ToString().ToLower().Replace(" ", "_") + "/default.aspx") + "\" class=\"mainCity\">" + row1["city"].ToString().Replace(" ", "&nbsp;").Replace("-", "&#8209;") + "</a>, ";
                                }
                                divCaribbean.InnerHtml = divCaribbean.InnerHtml.Remove(divCaribbean.InnerHtml.Length - 2, 2);
                                //divCaribbean.InnerHtml += "</font>";
                            }
                            else
                                divCaribbean.InnerHtml += "<a class=\"mainState\" href=\"" + CommonFunctions.PrepareURL(row["country"].ToString().ToLower().Replace(" ", "_").Replace(" ", "_").ToLower() +
                                "/" + rowTemp["stateprovince"].ToString().Replace(" ", "_").ToLower() + "/default.aspx") + "\">" + rowTemp["stateprovince"].ToString().Replace(" ", "&nbsp;").Replace("-", "&#8209;") + "</a>";
                            if (dtTemp2.Rows.Count > 0)
                            {
                                divCaribbean.InnerHtml += "<br />";
                                remBr = false;
                            }
                            else
                            {
                                divCaribbean.InnerHtml += ", ";
                                remBr = true;
                            }
                            stateDown = true;
                            //add cities to lower

                        }
                        divCaribbean.InnerHtml = divCaribbean.InnerHtml.Remove(divCaribbean.InnerHtml.Length - 2, 2);
                        divCaribbean.InnerHtml += "<br />";
                    }

                }
            }

            //central america
           dt = VADBCommander.CountiesByRegionList("5");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    divcAmerica.InnerHtml += "<a class=\"mainPgCountry\" href=\"" + CommonFunctions.PrepareURL(row["country"].ToString().ToLower().Replace(" ", "_") + "/default.aspx") +
                        "\"><b>" + row["country"].ToString() + "</b></a><br />";
                    bool stateDown = false;
                    
                    dtTemp = VADBCommander.StateProvinceByCountryList(row["id"].ToString());
                    if (dtTemp.Rows.Count > 0)
                    {
                        bool remBr = true;
                        foreach (DataRow rowTemp in dtTemp.Rows)
                        {
                            
                            DataTable dtTemp2 =VADBCommander.CityStateCountryRegionList(rowTemp["id"].ToString());
                            //add cities to lower

                            if (dtTemp2.Rows.Count > 0)
                            {
                                if (stateDown == true)
                                {
                                    if (divcAmerica.InnerHtml.Substring(divcAmerica.InnerHtml.Length - 6, 6) != "<br />")
                                        divcAmerica.InnerHtml += "<br />";
                                }

                                divcAmerica.InnerHtml += "<a class=\"mainState\" href=\"" + CommonFunctions.PrepareURL(row["country"].ToString().ToLower().Replace(" ", "_").Replace(" ", "_").ToLower() +
                                "/" + rowTemp["stateprovince"].ToString().Replace(" ", "_").ToLower() + "/default.aspx") + "\">" + rowTemp["stateprovince"].ToString().Replace(" ", "&nbsp;").Replace("-", "&#8209;") + "</a>";
                                divcAmerica.InnerHtml += ":  ";
                                foreach (DataRow row1 in dtTemp2.Rows)
                                {
                                    divcAmerica.InnerHtml += "<a class=\"mainCity\" href=\"" + CommonFunctions.PrepareURL(row["country"].ToString().ToLower().Replace(" ", "_") + "/" +
                                        rowTemp["stateProvince"].ToString().Replace(" ", "_").ToLower() + "/" + row1["city"].ToString().ToLower().Replace(" ", "_") + "/default.aspx") + "\" class=\"mainCity\">" + row1["city"].ToString().Replace(" ", "&nbsp;").Replace("-", "&#8209;") + "</a>, ";
                                }
                                divcAmerica.InnerHtml = divcAmerica.InnerHtml.Remove(divcAmerica.InnerHtml.Length - 2, 2);
                                //divcAmerica.InnerHtml += "</font>";
                            }
                            else
                                divcAmerica.InnerHtml += "<a class=\"mainState\" href=\"" + CommonFunctions.PrepareURL(row["country"].ToString().ToLower().Replace(" ", "_").Replace(" ", "_").ToLower() +
                                "/" + rowTemp["stateprovince"].ToString().Replace(" ", "_").ToLower() + "/default.aspx") + "\">" + rowTemp["stateprovince"].ToString().Replace(" ", "&nbsp;").Replace("-", "&#8209;") + "</a>";
                            if (dtTemp2.Rows.Count > 0)
                            {
                                divcAmerica.InnerHtml += "<br />";
                                remBr = false;
                            }
                            else
                            {
                                divcAmerica.InnerHtml += ", ";
                                remBr = true;
                            }
                            stateDown = true;
                            //add cities to lower
                        }
                    }
                }
            }
            //europe
            dt = VADBCommander.CountriesByRegionEuropeFirstList("6");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    divEurope.InnerHtml += "<a class=\"mainPgCountry\" href=\"" + CommonFunctions.PrepareURL(row["country"].ToString().ToLower().Replace(" ", "_") + "/default.aspx") +
                        "\"><b>" + row["country"].ToString() + "</b></a><br />";
                    bool stateDown = false;
                    dtTemp = VADBCommander.StateProvinceByCountryList(row["id"].ToString());
                    if (dtTemp.Rows.Count > 0)
                    {
                        bool remBr = true;
                        foreach (DataRow rowTemp in dtTemp.Rows)
                        {
                            DataTable dtTemp2 = VADBCommander.CityStateCountryRegionList(rowTemp["id"].ToString());
                            //add cities to lower

                            if (dtTemp2.Rows.Count > 0)
                            {
                                if (stateDown == true)
                                {
                                    if (divEurope.InnerHtml.Substring(divEurope.InnerHtml.Length - 6, 6) != "<br />")
                                        divEurope.InnerHtml += "<br />";
                                }

                                divEurope.InnerHtml += "<a class=\"mainState\" href=\"" + CommonFunctions.PrepareURL(row["country"].ToString().ToLower().Replace(" ", "_").Replace(" ", "_").ToLower() +
                                "/" + rowTemp["stateprovince"].ToString().Replace(" ", "_").ToLower() + "/default.aspx") + "\">" + rowTemp["stateprovince"].ToString().Replace(" ", "&nbsp;").Replace("-", "&#8209;") + "</a>";
                                divEurope.InnerHtml += ":  ";
                                foreach (DataRow row1 in dtTemp2.Rows)
                                {
                                    divEurope.InnerHtml += "<a class=\"mainCity\" href=\"" + CommonFunctions.PrepareURL(row["country"].ToString().ToLower().Replace(" ", "_") + "/" +
                                        rowTemp["stateProvince"].ToString().Replace(" ", "_").ToLower() + "/" + row1["city"].ToString().ToLower().Replace(" ", "_") + "/default.aspx") + "\" class=\"mainCity\">" + row1["city"].ToString().Replace(" ", "&nbsp;").Replace("-", "&#8209;") + "</a>, ";
                                }
                                divEurope.InnerHtml = divEurope.InnerHtml.Remove(divEurope.InnerHtml.Length - 2, 2);
                                //divEurope.InnerHtml += "</font>";
                            }
                            else
                                divEurope.InnerHtml += "<a class=\"mainState\" href=\"" + CommonFunctions.PrepareURL(row["country"].ToString().ToLower().Replace(" ", "_").Replace(" ", "_").ToLower() +
                                "/" + rowTemp["stateprovince"].ToString().Replace(" ", "_").ToLower() + "/default.aspx") + "\">" + rowTemp["stateprovince"].ToString().Replace(" ", "&nbsp;").Replace("-", "&#8209;") + "</a>";
                            if (dtTemp2.Rows.Count > 0)
                            {
                                divEurope.InnerHtml += "<br />";
                                remBr = false;
                            }
                            else
                            {
                                divEurope.InnerHtml += ", ";
                                remBr = true;
                            }
                            stateDown = true;
                            //add cities to lower
                        }
                        if (remBr == true)
                        {
                            divEurope.InnerHtml = divEurope.InnerHtml.Remove(divEurope.InnerHtml.Length - 2, 2);
                            divEurope.InnerHtml += "<br />";
                        }
                    }

                }
            }

            //europe 2
            dt = VADBCommander.CountriesByRegionEuropeSecondList("6");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    divEurope2.InnerHtml += "<a class=\"mainPgCountry\" href=\"" + CommonFunctions.PrepareURL(row["country"].ToString().ToLower().Replace(" ", "_") + "/default.aspx") +
                        "\"><b>" + row["country"].ToString() + "</b></a><br />";
                    bool stateDown = false;
                    
                    dtTemp = VADBCommander.StateProvinceByCountryList(row["id"].ToString());
                    if (dtTemp.Rows.Count > 0)
                    {
                        bool remBr = true;
                        foreach (DataRow rowTemp in dtTemp.Rows)
                        {
                            DataTable dtTemp2 = VADBCommander.CityStateCountryRegionList(rowTemp["id"].ToString());
                            //add cities to lower

                            if (dtTemp2.Rows.Count > 0)
                            {
                                if (stateDown == true)
                                {
                                    if (divEurope2.InnerHtml.Substring(divEurope2.InnerHtml.Length - 6, 6) != "<br />")
                                        divEurope2.InnerHtml += "<br />";
                                }

                                divEurope2.InnerHtml += "<a class=\"mainState\" href=\"" + CommonFunctions.PrepareURL(row["country"].ToString().ToLower().Replace(" ", "_").Replace(" ", "_").ToLower() +
                                "/" + rowTemp["stateprovince"].ToString().Replace(" ", "_").ToLower() + "/default.aspx") + "\">" + rowTemp["stateprovince"].ToString().Replace(" ", "&nbsp;").Replace("-", "&#8209;") + "</a>";
                                divEurope2.InnerHtml += ":  ";
                                foreach (DataRow row1 in dtTemp2.Rows)
                                {
                                    divEurope2.InnerHtml += "<a class=\"mainCity\" href=\"" + CommonFunctions.PrepareURL(row["country"].ToString().ToLower().Replace(" ", "_") + "/" +
                                        rowTemp["stateProvince"].ToString().Replace(" ", "_").ToLower() + "/" + row1["city"].ToString().ToLower().Replace(" ", "_") + "/default.aspx") + "\" class=\"mainCity\">" + row1["city"].ToString().Replace(" ", "&nbsp;").Replace("-", "&#8209;") + "</a>, ";
                                }
                                divEurope2.InnerHtml = divEurope2.InnerHtml.Remove(divEurope2.InnerHtml.Length - 2, 2);
                                //divEurope2.InnerHtml += "</font>";
                            }
                            else
                                divEurope2.InnerHtml += "<a class=\"mainState\" href=\"" + CommonFunctions.PrepareURL(row["country"].ToString().ToLower().Replace(" ", "_").Replace(" ", "_").ToLower() +
                                "/" + rowTemp["stateprovince"].ToString().Replace(" ", "_").ToLower() + "/default.aspx") + "\">" + rowTemp["stateprovince"].ToString().Replace(" ", "&nbsp;").Replace("-", "&#8209;") + "</a>";
                            if (dtTemp2.Rows.Count > 0)
                            {
                                divEurope2.InnerHtml += "<br />";
                                remBr = false;
                            }
                            else
                            {
                                divEurope2.InnerHtml += ", ";
                                remBr = true;
                            }
                            stateDown = true;
                            //add cities to lower
                        }
                        if (remBr == true)
                        {
                            divEurope2.InnerHtml = divEurope2.InnerHtml.Remove(divEurope2.InnerHtml.Length - 2, 2);
                            divEurope2.InnerHtml += "<br />";
                        }


                    }

                }
            }


            //mid east
            dt = VADBCommander.CountiesByRegionList("7");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    divmEast.InnerHtml += "<a class=\"mainPgCountry\" href=\"" + CommonFunctions.PrepareURL(row["country"].ToString().ToLower().Replace(" ", "_") + "/default.aspx") +
                        "\"><b>" + row["country"].ToString() + "</b></a><br />";

                    dtTemp = VADBCommander.StateProvinceByCountryList(row["id"].ToString());
                    if (dtTemp.Rows.Count > 0)
                    {
                        bool remBr = true;
                        foreach (DataRow rowTemp in dtTemp.Rows)
                        {
                            divmEast.InnerHtml += "<a class=\"mainState\" href=\"" + CommonFunctions.PrepareURL(row["country"].ToString().ToLower().Replace(" ", "_").Replace(" ", "_").ToLower() +
                                "/" + rowTemp["stateprovince"].ToString().Replace(" ", "_").ToLower() + "/default.aspx") + "\">" + rowTemp["stateprovince"].ToString().Replace(" ", "&nbsp;").Replace("-", "&#8209;") + "</a>";

                            //add cities to lower

                            DataTable dtTemp2 = VADBCommander.CityStateCountryRegionList(rowTemp["id"].ToString());
                            if (dtTemp2.Rows.Count > 0)
                            {
                                divmEast.InnerHtml += ":  ";
                                foreach (DataRow row1 in dtTemp2.Rows)
                                {
                                    divmEast.InnerHtml += "<a class=\"mainCity\" href=\"" + CommonFunctions.PrepareURL(row["country"].ToString().ToLower().Replace(" ", "_") + "/" +
                                        rowTemp["stateProvince"].ToString().Replace(" ", "_").ToLower() + "/" + row1["city"].ToString().ToLower().Replace(" ", "_") + "/default.aspx") + "\" class=\"mainCity\">" + row1["city"].ToString().Replace(" ", "&nbsp;").Replace("-", "&#8209;") + "</a>, ";
                                }
                                divmEast.InnerHtml = divmEast.InnerHtml.Remove(divmEast.InnerHtml.Length - 2, 2);
                                //divmEast.InnerHtml += "</font>";
                            }

                            if (dtTemp2.Rows.Count > 0)
                            {
                                divmEast.InnerHtml += "<br />";
                                remBr = false;
                            }
                            else
                            {
                                divmEast.InnerHtml += ", ";
                                remBr = true;
                            }
                            //add cities to lower
                        }
                        if (remBr == true)
                        {
                            divmEast.InnerHtml = divmEast.InnerHtml.Remove(divmEast.InnerHtml.Length - 2, 2);
                            divmEast.InnerHtml += "<br />";
                        }
                    }
                }
            }

            //north amer
            dt = VADBCommander.CountiesByRegionList("8");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    divnAmerica.InnerHtml += "<a class=\"mainPgCountry\" href=\"" + CommonFunctions.PrepareURL(row["country"].ToString().ToLower().Replace(" ", "_") + "/default.aspx") +
                        "\"><b>" + row["country"].ToString() + "</b></a><br />";
                    bool stateDown = false;

                    dtTemp = VADBCommander.StateProvinceByCountryList(row["id"].ToString());
                    if (dtTemp.Rows.Count > 0)
                    {
                        bool remBr = true;
                        foreach (DataRow rowTemp in dtTemp.Rows)
                        {
                            DataTable dtTemp2 = VADBCommander.CityStateCountryRegionList(rowTemp["id"].ToString());
                            //add cities to lower

                            if (dtTemp2.Rows.Count > 0)
                            {
                                if (stateDown == true)
                                {
                                    if (divnAmerica.InnerHtml.Substring(divnAmerica.InnerHtml.Length - 6, 6) != "<br />")
                                        divnAmerica.InnerHtml += "<br />";
                                }

                                divnAmerica.InnerHtml += "<a class=\"mainState\" href=\"" + CommonFunctions.PrepareURL(row["country"].ToString().ToLower().Replace(" ", "_").Replace(" ", "_").ToLower() +
                                "/" + rowTemp["stateprovince"].ToString().Replace(" ", "_").ToLower() + "/default.aspx") + "\">" + rowTemp["stateprovince"].ToString().Replace(" ", "&nbsp;").Replace("-", "&#8209;") + "</a>";
                                divnAmerica.InnerHtml += ":  ";
                                foreach (DataRow row1 in dtTemp2.Rows)
                                {
                                    divnAmerica.InnerHtml += "<a class=\"mainCity\" href=\"" + CommonFunctions.PrepareURL(row["country"].ToString().ToLower().Replace(" ", "_") + "/" +
                                        rowTemp["stateProvince"].ToString().Replace(" ", "_").ToLower() + "/" + row1["city"].ToString().ToLower().Replace(" ", "_") + "/default.aspx") + "\" class=\"mainCity\">" + row1["city"].ToString().Replace(" ", "&nbsp;").Replace("-", "&#8209;") + "</a>, ";
                                }
                                divnAmerica.InnerHtml = divnAmerica.InnerHtml.Remove(divnAmerica.InnerHtml.Length - 2, 2);
                                //divnAmerica.InnerHtml += "</font>";
                            }
                            else
                                divnAmerica.InnerHtml += "<a class=\"mainState\" href=\"" + CommonFunctions.PrepareURL(row["country"].ToString().ToLower().Replace(" ", "_").Replace(" ", "_").ToLower() +
                                "/" + rowTemp["stateprovince"].ToString().Replace(" ", "_").ToLower() + "/default.aspx") + "\">" + rowTemp["stateprovince"].ToString().Replace(" ", "&nbsp;").Replace("-", "&#8209;") + "</a>";
                            if (dtTemp2.Rows.Count > 0)
                            {
                                divnAmerica.InnerHtml += "<br />";
                                remBr = false;
                            }
                            else
                            {
                                divnAmerica.InnerHtml += ", ";
                                remBr = true;
                            }
                            stateDown = true;
                            //add cities to lower

                        }
                        if (remBr == true)
                        {
                            divnAmerica.InnerHtml = divnAmerica.InnerHtml.Remove(divnAmerica.InnerHtml.Length - 2, 2);
                            divnAmerica.InnerHtml += "<br />";
                        }
                    }
                }
            }

            //oceania
            dt = VADBCommander.CountiesByRegionList("3");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    divOceania.InnerHtml += "<a class=\"mainPgCountry\" href=\"" + CommonFunctions.PrepareURL(row["country"].ToString().ToLower().Replace(" ", "_") + "/default.aspx") +
                        "\"><b>" + row["country"].ToString() + "</b></a><br />";

                    dtTemp = VADBCommander.StateProvinceByCountryList(row["id"].ToString());
                    if (dtTemp.Rows.Count > 0)
                    {
                        bool remBr = true;
                        foreach (DataRow rowTemp in dtTemp.Rows)
                        {
                            divOceania.InnerHtml += "<a class=\"mainState\" href=\"" + CommonFunctions.PrepareURL(row["country"].ToString().ToLower().Replace(" ", "_").Replace(" ", "_").ToLower() +
                                "/" + rowTemp["stateprovince"].ToString().Replace(" ", "_").ToLower() + "/default.aspx") + "\">" + rowTemp["stateprovince"].ToString().Replace(" ", "&nbsp;").Replace("-", "&#8209;") + "</a>";
                            //add cities to lower

                            DataTable dtTemp2 = VADBCommander.CityStateCountryRegionList(rowTemp["id"].ToString());

                            if (dtTemp2.Rows.Count > 0)
                            {
                                divOceania.InnerHtml += ":  ";
                                foreach (DataRow row1 in dtTemp2.Rows)
                                {
                                    divOceania.InnerHtml += "<a class=\"mainCity\" href=\"" + CommonFunctions.PrepareURL(row["country"].ToString().ToLower().Replace(" ", "_") + "/" +
                                        rowTemp["stateProvince"].ToString().Replace(" ", "_").ToLower() + "/" + row1["city"].ToString().ToLower().Replace(" ", "_") + "/default.aspx") + "\" class=\"mainCity\">" + row1["city"].ToString().Replace(" ", "&nbsp;").Replace("-", "&#8209;") + "</a>, ";
                                }
                                divOceania.InnerHtml = divOceania.InnerHtml.Remove(divOceania.InnerHtml.Length - 2, 2);
                                //divOceania.InnerHtml += "</font>";
                            }

                            if (dtTemp2.Rows.Count > 0)
                            {
                                divOceania.InnerHtml += "<br />";
                                remBr = false;
                            }
                            else
                            {
                                divOceania.InnerHtml += ", ";
                                remBr = true;
                            }
                            //add cities to lower
                        }
                        if (remBr == true)
                        {
                            divOceania.InnerHtml = divOceania.InnerHtml.Remove(divOceania.InnerHtml.Length - 2, 2);
                            divOceania.InnerHtml += "<br />";
                        }
                    }
                }
            }

            //south amer
            dt = VADBCommander.CountiesByRegionList("9");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    divsAmerica.InnerHtml += "<a href=\"" + CommonFunctions.PrepareURL(row["country"].ToString().ToLower().Replace(" ", "_") + "/default.aspx") +
                        "\"><b>" + row["country"].ToString() + "</b></a><br />";

                    dtTemp = VADBCommander.StateProvinceByCountryList(row["id"].ToString());
                    if (dtTemp.Rows.Count > 0)
                    {
                        bool remBr = true;
                        foreach (DataRow rowTemp in dtTemp.Rows)
                        {
                            divsAmerica.InnerHtml += "<a href=\"" + CommonFunctions.PrepareURL(row["country"].ToString().ToLower().Replace(" ", "_").Replace(" ", "_").ToLower() +
                                "/" + rowTemp["stateprovince"].ToString().Replace(" ", "_").ToLower() + "/default.aspx") + "\">" + rowTemp["stateprovince"].ToString().Replace(" ", "&nbsp;").Replace("-", "&#8209;") + "</a>";

                            //add cities to lower

                            
                            DataTable dtTemp2 = VADBCommander.CityStateCountryRegionList(rowTemp["id"].ToString());
                            if (dtTemp2.Rows.Count > 0)
                            {
                                divsAmerica.InnerHtml += ":  ";
                                foreach (DataRow row1 in dtTemp2.Rows)
                                {
                                    divsAmerica.InnerHtml += "<a class=\"mainCity\" href=\"" + CommonFunctions.PrepareURL(row["country"].ToString().ToLower().Replace(" ", "_") + "/" +
                                        rowTemp["stateProvince"].ToString().Replace(" ", "_").ToLower() + "/" + row1["city"].ToString().ToLower().Replace(" ", "_") + "/default.aspx") + "\" class=\"mainCity\">" + row1["city"].ToString().Replace(" ", "&nbsp;").Replace("-", "&#8209;") + "</a>, ";
                                }
                                divsAmerica.InnerHtml = divsAmerica.InnerHtml.Remove(divsAmerica.InnerHtml.Length - 2, 2);
                                //divsAmerica.InnerHtml += "</font>";
                            }

                            if (dtTemp2.Rows.Count > 0)
                            {
                                divsAmerica.InnerHtml += "<br />";
                                remBr = false;
                            }
                            else
                            {
                                divsAmerica.InnerHtml += ", ";
                                remBr = true;
                            }
                            //add cities to lower
                        }
                        if (remBr == true)
                        {
                            divsAmerica.InnerHtml = divsAmerica.InnerHtml.Remove(divsAmerica.InnerHtml.Length - 2, 2);
                            divsAmerica.InnerHtml += "<br />";
                        }
                    }
                }
            }
        }
        catch (Exception ex) { lblInfo.Text = ex.Message; }
        finally { obj.CloseConnection(); }
    }
}

