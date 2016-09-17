using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net;
using System.IO;
using System.Text;
using Newtonsoft.Json;

public class Locationss
{
    public string title;
    public double lat;
    public double lng;
    public string description;
    public string URL;
}
public partial class Country : CommonPage
{
    #region Page Variables

    private const string STR_SELECTCitiesFROMCitiesWHERECitiesStateProvinceID = "SELECT Cities.*, Counties.CountyID FROM Cities  INNER JOIN StateProvinces ON StateProvinces.ID = Cities.StateProvinceID left outer join counties on cities.id=counties.cityid WHERE (StateProvinces.CountryID = @CountryID) AND EXISTS ( SELECT * FROM Properties WHERE (Properties.IfFinished = 1) AND (Properties.IfApproved = 1) AND (Properties.CityID = Cities.ID)  AND NOT EXISTS (SELECT * FROM Auctions WHERE PropertyID = Properties.ID)) ORDER BY City";

    private const string STR_SELECTCitiesFROMCitiesWHERECitiesStateProvinceIDNew = "SELECT     CityLatLong.*" +
 " FROM         Cities INNER JOIN " +
  " StateProvinces ON Cities.StateProvinceID = StateProvinces.ID INNER JOIN " +
                      " Countries ON StateProvinces.CountryID = Countries.ID INNER JOIN " +
                      "CityLatLong ON Cities.City = CityLatLong.City AND Countries.Country = CityLatLong.Country AND StateProvinces.StateProvince = CityLatLong.StateProvince " +
"where Countries.ID=@CountryID AND EXISTS ( SELECT * FROM Properties WHERE (Properties.IfFinished = 1) AND (Properties.IfApproved = 1) AND (Properties.CityID = Cities.ID)  AND NOT EXISTS (SELECT * FROM Auctions WHERE PropertyID = Properties.ID))";

    public string region;
    public string country;
    public string stateprovince;
    public string cities;
    public string County;
    public string altTag;

    private int regionid = -1;
    private int countryid = -1;
    private int stateprovinceid = -1;
    private int propertycount;

    //protected SqlConnection Connection = new SqlConnection (ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
    protected SqlDataAdapter CitiesAdapter;
    protected SqlDataAdapter PropertiesAdapter;
    protected SqlDataAdapter AmenitiesAdapter;
    protected SqlDataAdapter LocationAdapter;
    protected SqlDataAdapter StateProvincesAdapter;
    protected SqlDataAdapter LocationAdapterCountry;
    protected SqlDataAdapter PropertyTypesAdapter;
    protected SqlDataAdapter RegionCountriesAdapter;
    protected SqlDataAdapter CountryMapAdapter;
    //protected SqlDataAdapter CountyAdapter;

    protected DataSet MainDataSet = new DataSet();
    protected DataSet MainDataSetCountries = new DataSet();

    #endregion

    protected void Page_Load(object sender, System.EventArgs e)
    {

        RegionCountriesAdapter = CommonFunctions.PrepareAdapter(CommonFunctions.GetConnection(), "select distinct Country,Region from Countries inner join Regions on Countries.RegionID=Regions.ID " +
            " inner join StateProvinces on StateProvinces.CountryID=Countries.ID " +
            "where (RegionID=@RegionID)  AND EXISTS (" +
                " SELECT * FROM Properties INNER JOIN Cities ON Properties.CityID = Cities.ID" +
                " WHERE (Properties.IfFinished = 1) AND (Properties.IfApproved = 1)" +
                " AND (Cities.StateProvinceID = StateProvinces.ID) " +
                " AND NOT EXISTS (SELECT * FROM Auctions WHERE PropertyID = Properties.ID)) " +
                "ORDER BY Country", SqlDbType.Int);
        StateProvincesAdapter = CommonFunctions.PrepareAdapter(CommonFunctions.GetConnection(), "SELECT StateProvinces.* " +
            "FROM StateProvinces " +
            "WHERE (StateProvinces.CountryID = @CountryID) AND EXISTS (" +
            " SELECT * FROM Properties INNER JOIN Cities ON Properties.CityID = Cities.ID" +
            " WHERE (Properties.IfFinished = 1) AND (Properties.IfApproved = 1)" +
            " AND (Cities.StateProvinceID = StateProvinces.ID) " +
            " AND NOT EXISTS (SELECT * FROM Auctions WHERE PropertyID = Properties.ID)) " +
            "ORDER BY StateProvince", SqlDbType.Int);
        // StateCodeInfo.Text = SqlDbType.Int.
        CitiesAdapter = CommonFunctions.PrepareAdapter(CommonFunctions.GetConnection(), String.Format(STR_SELECTCitiesFROMCitiesWHERECitiesStateProvinceID), SqlDbType.Int);

        const string STR_SELECTPropertiesInfo = "SELECT Properties.Name2 as PropertyName2, Properties.Name, Properties.NumBedrooms, Properties.NumBaths, Properties.NumSleeps, Properties.NumTVs, Properties.NumVCRs, Properties.CityID, Properties.NumCDPlayers, Properties.ID, CASE WHEN EXISTS (SELECT * FROM PropertiesAmenities INNER JOIN Amenities ON PropertiesAmenities.AmenityID = Amenities.ID WHERE (PropertiesAmenities.PropertyID = Properties.ID) AND (Amenities.Amenity = 'Beach Front')) THEN 'Beach Front' ELSE '' END AS BeachFront, CASE WHEN EXISTS (SELECT * FROM PropertiesAmenities INNER JOIN Amenities ON PropertiesAmenities.AmenityID = Amenities.ID WHERE (PropertiesAmenities.PropertyID = Properties.ID) AND (Amenities.Amenity = 'Seaside')) THEN 'Seaside' ELSE '' END AS Seaside, CASE WHEN EXISTS (SELECT * FROM PropertiesAmenities INNER JOIN Amenities ON PropertiesAmenities.AmenityID = Amenities.ID WHERE (PropertiesAmenities.PropertyID = Properties.ID) AND (Amenities.Amenity = 'Lake Front')) THEN 'Lake Front' ELSE '' END AS LakeFront, CASE WHEN EXISTS (SELECT * FROM PropertiesAmenities INNER JOIN Amenities ON PropertiesAmenities.AmenityID = Amenities.ID WHERE (PropertiesAmenities.PropertyID = Properties.ID) AND (Amenities.Amenity = 'River Front')) THEN 'River Front' ELSE '' END AS RiverFront, CASE WHEN EXISTS (SELECT * FROM PropertiesAmenities INNER JOIN Amenities ON PropertiesAmenities.AmenityID = Amenities.ID WHERE (PropertiesAmenities.PropertyID = Properties.ID) AND (Amenities.Amenity = 'Ski In Ski Out')) THEN 'Ski In Ski Out' ELSE '' END AS Ski, Cities.City, StateProvinces.StateProvince, Countries.Country, Regions.Region, MinimumNightlyRentalTypes.Name AS MinimumNightlyRental, PropertyTypes.Name AS Type FROM Properties INNER JOIN Cities ON Properties.CityID = Cities.ID INNER JOIN StateProvinces ON StateProvinces.ID = Cities.StateProvinceID INNER JOIN Countries ON StateProvinces.CountryID = Countries.ID INNER JOIN Regions ON Countries.RegionID = Regions.ID INNER JOIN Users ON Properties.UserID = Users.ID LEFT OUTER JOIN MinimumNightlyRentalTypes ON Properties.MinimumNightlyRentalID = MinimumNightlyRentalTypes.ID LEFT OUTER JOIN PropertyTypes ON Properties.TypeID = PropertyTypes.ID WHERE (Properties.IfFinished = 1) AND (Properties.IfApproved = 1) AND (StateProvinces.CountryID = @CountryID) AND NOT EXISTS (SELECT * FROM Auctions WHERE PropertyID = Properties.ID) ORDER BY StateProvinces.StateProvince, Cities.City, Type, CASE WHEN EXISTS (SELECT * FROM Invoices WHERE (PropertyID = Properties.ID) AND (PaymentAmount >= InvoiceAmount) AND (GETDATE() <= Invoices.RenewalDate)) THEN 1 ELSE 0 END DESC, Properties.ID";

        PropertiesAdapter = CommonFunctions.PrepareAdapter(CommonFunctions.GetConnection(), String.Format(STR_SELECTPropertiesInfo), SqlDbType.Int);

        AmenitiesAdapter = CommonFunctions.PrepareAdapter(CommonFunctions.GetConnection(), "SELECT Amenities.ID, Amenity," +
            " PropertiesAmenities.PropertyID " +
            "FROM Amenities INNER JOIN PropertiesAmenities ON Amenities.ID = PropertiesAmenities.AmenityID" +
            " INNER JOIN Properties ON PropertiesAmenities.PropertyID = Properties.ID " +
            " INNER JOIN Cities ON Properties.CityID = Cities.ID INNER JOIN StateProvinces ON StateProvinces.ID = Cities.StateProvinceID " +
            "WHERE (Properties.IfFinished = 1) AND (Properties.IfApproved = 1) AND (StateProvinces.CountryID = @CountryID)" +
            " AND NOT EXISTS (SELECT * FROM Auctions WHERE PropertyID = Properties.ID) AND (Amenities.Amenity NOT IN" +
            " ('Lake Front', 'Beach Front', 'River Front', 'Seaside', 'Ski In Ski Out', 'TV', 'VCR', 'CD Player'))",
            SqlDbType.Int);

        LocationAdapter = CommonFunctions.PrepareAdapter(CommonFunctions.GetConnection(), "SELECT StateProvinces.ID AS StateProvinceID," +
            " StateProvinces.StateProvince, Countries.ID AS CountryID, Countries.Country, Regions.ID AS RegionID," +
            " Regions.Region, Countries.titleoverride, stateprovinces.descriptionoverride " +
            "FROM StateProvinces INNER JOIN Countries ON StateProvinces.CountryID = Countries.ID" +
            " INNER JOIN Regions ON Countries.RegionID = Regions.ID WHERE (Countries.ID = @CountryId)",
            SqlDbType.Int);

        PropertyTypesAdapter = CommonFunctions.PrepareAdapter(CommonFunctions.GetConnection(), "SELECT     PropertyTypes.Name,COUNT(*) as Count, PropertyTypes.ID " +
                     " FROM         Cities INNER JOIN " +
                      " Properties ON Cities.ID = Properties.CityID  " +
                      " INNER JOIN " +
                      " PropertyTypes ON Properties.TypeID = PropertyTypes.ID INNER JOIN " +
                      " StateProvinces ON Cities.StateProvinceID = StateProvinces.ID INNER JOIN " +
                      " Countries ON StateProvinces.CountryID = Countries.ID  WHERE (Countries.ID = @CountryId) " +
                      "group by PropertyTypes.Name,PropertyTypes.ID",
           SqlDbType.Int);


        if ((Request.Params["CountryID"] != null) && (Request.Params["CountryID"].Length > 0))
            try
            {
                countryid = Convert.ToInt32(Request.Params["CountryID"]);
            }
            catch (Exception)
            {
            }

        if (countryid == -1)
            Response.Redirect(CommonFunctions.PrepareURL("InternalError.aspx"));

        // Map Display Of Country Page
        SqlConnection con = CommonFunctions.GetConnection();
        CountryMapAdapter = new SqlDataAdapter(STR_SELECTCitiesFROMCitiesWHERECitiesStateProvinceIDNew, con);//CommonFunctions.PrepareAdapter(CommonFunctions.GetConnection(), String.Format(STR_SELECTCitiesFROMCitiesWHERECitiesStateProvinceID), SqlDbType.Int);
        CountryMapAdapter.SelectCommand.Parameters.Add("@CountryID", SqlDbType.Int);
        CountryMapAdapter.SelectCommand.Parameters["@CountryID"].Value = Convert.ToInt32(countryid);

        DataTable dtmap = new DataTable();
        CountryMapAdapter.Fill(dtmap);
        List<Locationss> eList = new List<Locationss>();

        foreach (DataRow dr in dtmap.Rows)
        {
            try
            {
                Locationss e1 = new Locationss();
                e1.title = dr["City"].ToString();
                e1.lat = Convert.ToDouble(dr["Latitude"]);
                e1.lng = Convert.ToDouble(dr["Longitude"]);
                e1.description = dr["City"].ToString();
                string temp = CommonFunctions.GetSiteAddress() + "/" + dr["Country"].ToString().ToLower().Replace(" ", "_") +
                 "/" + dr["StateProvince"].ToString().ToLower().Replace(" ", "_") + "/" + dr["City"].ToString().ToLower().Replace(" ", "_") + "/default.aspx";
                e1.URL = temp;
                eList.Add(e1);
            }
            catch { }
        }
        string ans = JsonConvert.SerializeObject(eList, Formatting.Indented);
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(Page.GetType(), "JSON", "initialize(" + ans + ");", true);


        CitiesAdapter.SelectCommand.Parameters["@CountryId"].Value = countryid;
        LocationAdapter.SelectCommand.Parameters["@CountryId"].Value = countryid;
        PropertiesAdapter.SelectCommand.Parameters["@CountryId"].Value = countryid;
        AmenitiesAdapter.SelectCommand.Parameters["@CountryId"].Value = countryid;
        PropertyTypesAdapter.SelectCommand.Parameters["@CountryId"].Value = countryid;

        Session["CountryID"] = countryid;
        if (LocationAdapter.Fill(MainDataSet, "Location") > 0)
        {
            regionid = (int)MainDataSet.Tables["Location"].Rows[0]["RegionID"];
            countryid = (int)MainDataSet.Tables["Location"].Rows[0]["CountryID"];
            region = (string)MainDataSet.Tables["Location"].Rows[0]["Region"];
            country = (string)MainDataSet.Tables["Location"].Rows[0]["Country"];
            stateprovince = (string)MainDataSet.Tables["Location"].Rows[0]["StateProvince"];
        }
        else
            Response.Redirect(CommonFunctions.PrepareURL("InternalError.aspx"));

        StateProvincesAdapter.SelectCommand.Parameters["@CountryID"].Value = countryid;
        RegionCountriesAdapter.SelectCommand.Parameters["@RegionId"].Value = regionid;
        CitiesAdapter.Fill(MainDataSet, "Cities");
        PropertiesAdapter.Fill(MainDataSet, "Properties");
        AmenitiesAdapter.Fill(MainDataSet, "Amenities");
        StateProvincesAdapter.Fill(MainDataSet, "StateProvinces");
        PropertyTypesAdapter.Fill(MainDataSet, "PropertyTypes");
        RegionCountriesAdapter.Fill(MainDataSet, "CountriesRegion");
        DBConnection objTemp = new DBConnection();
        DataTable dtTemp = new DataTable();
        try
        {
            dtTemp = VADBCommander.CountyNamesWithProperties(Request.Params["StateProvinceID"].ToString());
            DataTable dtCopy = dtTemp.Copy();
            dtCopy.TableName = "dtcopy";
            dtCopy.Namespace = "dtcopy";
            MainDataSet.Tables.Add(dtCopy);
            MainDataSet.Relations.Add("CitiesProperties", MainDataSet.Tables["Cities"].Columns["ID"],
                MainDataSet.Tables["Properties"].Columns["CityID"]);
            MainDataSet.Relations.Add("PropertiesAmenities", MainDataSet.Tables["Properties"].Columns["ID"],
                MainDataSet.Tables["Amenities"].Columns["PropertyID"]);

            MainDataSet.Relations.Add("CountyCities", MainDataSet.Tables["dtcopy"].Columns["ID"],
                MainDataSet.Tables["Cities"].Columns["countyID"]);
            LocationAdapterCountry.SelectCommand.Parameters["@CountryID"].Value = countryid;
        }
        catch (Exception ex) { lblInfo22.Text += ex.Message; }
        finally { objTemp.CloseConnection(); }

        foreach (DataRow datarow in MainDataSet.Tables["Cities"].Rows)
            if (datarow["City"] is string)
            {
                cities += " " + (string)datarow["City"];
            }
        HtmlHead head = Page.Header;

        DataBind();

        /////// common for postback and ! postback
        List<string> vList = new List<string>();
        DataTable dt = new DataTable();
        DataFunctions obj = new DataFunctions();
        DataTable dtCategories = new DataTable();
        DBConnection obj1 = new DBConnection();

        try
        {
            if (!IsPostBack)
            {
                dt = obj.PropertiesByCase(vList, countryid, "Country");
                DataView dv = dt.DefaultView;
                dv.Sort = "category asc";
                dt = dv.ToTable();
                test123.Text = "Country " + countryid.ToString() + " Count " + dt.Rows.Count.ToString();
                Session["dt"] = dt;
                int[] i = new int[4];
                i = FindNumAmenities(dt);

                //dtCategories = obj.FindNumCategories(dt);
                dtCategories = obj.FindNumCategorieswithImage(dt);
                DataView dvMax = dtCategories.DefaultView;
                dvMax.Sort = "count desc";
                DataTable dtMax = dvMax.ToTable();
                int vCategoryCount = 0;
                string firstCategory = "";
                string subCategory = "";
                foreach (DataRow row in dtMax.Rows)
                {
                    int index = dtMax.Rows.IndexOf(row);
                    if (index == 0)
                    {
                        firstCategory = row["category"].ToString();
                        subCategory = dt.Rows[0]["SubCategory"].ToString();
                    }
                    test123.Text = test123.Text + row["count"].ToString() + ",";
                    string vTemp = row["category"].ToString() + " (" + row["count"].ToString() + ")";
                    vTemp = vTemp.Replace(" ", "&nbsp;");
                    vCategoryCount = vCategoryCount + Convert.ToInt32(row["count"].ToString());
                }

                if (!IsPostBack)
                {

                    dtlStates.DataSource = dtCategories;
                    dtlStates.DataBind();
                }
                //numbedrooms filter
                dtCategories = obj.FindNumBedrooms(dt);
                int vBedCount = 0;
                foreach (DataRow row in dtCategories.Rows)
                {
                    vBedCount += Convert.ToInt32(row["count"]);
                }

                Page page = (Page)HttpContext.Current.Handler;
                

                    dtlStates.Style.Add("display", "block");
                    filerMain.Style.Add("display", "block");
                    if (Request.QueryString["category"] != null)
                    {
                        firstCategory = Convert.ToString(Request.QueryString["category"]);
                    }
                    {
                        string dispString = "";
                        string dispString2 = "";
                        if (subCategory.Contains("_"))
                        {
                            string[] strSplit = subCategory.Split('_');
                            dispString = UppercaseFirst(strSplit[0]) + " " + UppercaseFirst(strSplit[1]) + "s";
                        }
                        else
                        {
                            dispString = UppercaseFirst(subCategory) + "s";
                        }
                        if (firstCategory.Contains("_"))
                        {
                            string[] strSplit = firstCategory.Split('_');
                            dispString2 = UppercaseFirst(strSplit[0]) + " " + UppercaseFirst(strSplit[1]) + "s";
                        }
                        else
                        {
                            dispString2 = UppercaseFirst(firstCategory) + "s";
                        }
                        altTag = char.ToUpper(country[0]) + country.Substring(1) + " Vacation Rentals";
                        ltrH11.Text = char.ToUpper(country[0]) + country.Substring(1) + " " + char.ToUpper(firstCategory[0]) + firstCategory.Substring(1) + "s";
                        ltrH12.Text = char.ToUpper(country[0]) + country.Substring(1) + " Vacation Rentals";
                        //ltrH1.Text = char.ToUpper(country[0]) + country.Substring(1) + " Vacation Rentals And " + dispString2;
                        page.Title = char.ToUpper(country[0]) + country.Substring(1) + " Vacation Rentals | Vacations Abroad";

                    }
                    if (firstCategory == "bandb")
                    {
                        firstCategory = "B&B";
                    }


                    DataTable dtCategory = dt.Clone();
                    foreach (DataRow dr in dt.Rows)
                    {
                        string vTemp = dr["Category"].ToString(); //+ " (" + dr["count"].ToString() + ")";
                        if (vTemp.ToLower().Replace(" ", "").Trim() == firstCategory.ToLower().Replace("_", " ").Replace(" ", ""))
                        {
                            subCategory = dr["SubCategory"].ToString();
                            dtCategory.ImportRow(dr);
                        }
                    }
                    DataView dv1 = dtCategory.DefaultView;
                    dv1.Sort = "MinNightRate desc";
                   
                    dtlStates.Visible = true;

                    if (Request.QueryString["category"] != null)
                    {
                        string dispString = "";
                        if (firstCategory.Contains("_"))
                        {
                            string[] strSplit = firstCategory.Split('_');
                            dispString = UppercaseFirst(strSplit[0]) + " " + UppercaseFirst(strSplit[1]) + "s";
                        }
                        else
                        {
                            dispString = UppercaseFirst(firstCategory) + "s";
                        }
                        ltrH11.Text = char.ToUpper(country[0]) + country.Substring(1) + " " + char.ToUpper(firstCategory[0]) + firstCategory.Substring(1) + "s";
                        ltrH12.Text = char.ToUpper(country[0]) + country.Substring(1) + " " + subCategory + "s";
                        //ltrH1.Text = char.ToUpper(country[0]) + country.Substring(1) + " " + dispString;
                        page.Title = char.ToUpper(country[0]) + country.Substring(1) + " " + char.ToUpper(firstCategory[0]) + firstCategory.Substring(1) + "s And " + subCategory + "s | Vacations Abroad";
                        altTag = subCategory + " in " + country;
                        Label3.Text = altTag;
                    }
                    else
                    {
                        string dispString = "";
                        string dispString2 = "";
                        if (subCategory.Contains("_"))
                        {
                            string[] strSplit = subCategory.Split('_');
                            dispString = UppercaseFirst(strSplit[0]) + " " + UppercaseFirst(strSplit[1]) + "s";
                        }
                        else
                        {
                            dispString = UppercaseFirst(subCategory) + "s";
                        }
                        if (firstCategory.Contains("_"))
                        {
                            string[] strSplit = firstCategory.Split('_');
                            dispString2 = UppercaseFirst(strSplit[0]) + " " + UppercaseFirst(strSplit[1]) + "s";
                        }
                        else
                        {
                            dispString2 = UppercaseFirst(firstCategory) + "s";
                        }
                        altTag = char.ToUpper(country[0]) + country.Substring(1) + " Vacation Rentals";
                        ltrH11.Text = char.ToUpper(country[0]) + country.Substring(1) + " Vacations ";
                        ltrH12.Text = char.ToUpper(country[0]) + country.Substring(1) + " Vacation Rentals";
                        //ltrH1.Text = char.ToUpper(country[0]) + country.Substring(1) + " Vacation Rentals And " + char.ToUpper(country[0]) + country.Substring(1) + " " + dispString2;
                        page.Title = char.ToUpper(country[0]) + country.Substring(1) + " Vacation Rentals And " + " " + dispString + " | Vacations Abroad";

                    }
                    string tempcountry = CommonFunctions.GetSiteAddress() + "/" + country.ToLower().Replace(" ", "_") +
                                                "/default.aspx";
                    lbltText.Text = "<a href=\"" + tempcountry + "\"><span class=\"CountryInternalLink\" style=\"font-weight:normal;font-style:normal\">" + "</span></a></sp>";

                    HtmlMeta description = new HtmlMeta();
                    description.Name = "description";
                    description.Content = "Book Now " + char.ToUpper(country[0]) + country.Substring(1) + char.ToUpper(firstCategory[0]) + firstCategory.Substring(1) + "s And unique " + subCategory + "s";
                    head.Controls.Add(description);
                //}

                ViewState["firstCategory"] = firstCategory;
                // get the Page Text
                DataTable dt1 = new DataTable();
                try
                {
                    dt1 = VADBCommander.GetMainCountryText(countryid.ToString());
                
                }
                catch (Exception ex) { lblInfo.Text = ex.Message; }

                if (dt1.Rows.Count > 0)
                {
                    if (dt1.Rows[0]["countryText"] != null)
                    {
                        if (!IsPostBack)
                        {
                            lblCountryInfo.Text = dt1.Rows[0]["countryText"].ToString();
                            txtCountryText.Text = dt1.Rows[0]["countryText"].ToString().Replace("<br />", Environment.NewLine);
                        }
                        
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(dt1.Rows[0]["countryText2"])))
                    {
                        if(!IsPostBack)
                        {
                            lblInfo2.Text = dt1.Rows[0]["countryText2"].ToString();
                            counrtyregions.Visible = true;
                            txtCountryText2.Text = dt1.Rows[0]["countryText2"].ToString().Replace("<br />", Environment.NewLine);
                        }
                    }
                }
            }

            string vText = "Vacations-abroad.com is a " + stateprovince + " accommodation directory of " + stateprovince + " rentals by owner and privately owned " + stateprovince + " holiday accommodation. Our short term " + stateprovince + " rentals include luxury " +
               stateprovince + " holiday homes, " + stateprovince + " vacation homes and " + stateprovince + " vacation home rentals which are perfect for group or family vacation rentals in " + stateprovince + " " + country;



        }
        catch (Exception ex) { lblInfo.Text = ex.Message; }

        DBConnection obj3 = new DBConnection();
        SqlDataReader reader = obj3.ExecuteRecordSetArtificial("SELECT Cities.* FROM Cities WHERE (Cities.StateProvinceID = " + stateprovinceid + ") " + "AND EXISTS ( SELECT * FROM Properties WHERE (Properties.IfFinished = 1) AND (Properties.IfApproved = 1) AND " + "(Properties.CityID = Cities.ID)  AND NOT EXISTS (SELECT * FROM Auctions WHERE PropertyID = Properties.ID)) ORDER " + "BY City");
        string states1 = "";
        string regionCountry = "";
        foreach (DataRow dr in MainDataSet.Tables["CountriesRegion"].Rows)
        {
            string temp = CommonFunctions.GetSiteAddress() +
                   "/" + dr["Country"].ToString().ToLower().Replace(" ", "_") + "/default.aspx";
            temp = temp.ToLower();
            temp = temp.Replace(' ', '_');
            if (Convert.ToString(MainDataSet.Tables["Location"].Rows[0]["Country"]) == Convert.ToString(dr["Country"]))
            {
                rtLow3.InnerHtml += "<a href=\"" + temp + "\"><span class=\"CountryInternalLink\" style=\"text-decoration:underline;\">" + dr["Country"].ToString().Replace(" ", "&nbsp;") + "</span></a>, ";
            }
            else
            {
                rtLow3.InnerHtml += "<a href=\"" + temp + "\"><span class=\"CountryInternalLink\">" + dr["Country"].ToString().Replace(" ", "&nbsp;") + "</span></a>, ";
            }
                //rtLow3.InnerHtml += "<span class=\"CountryInternalLink\">" + dr["Country"].ToString().Replace(" ", "&nbsp;") + "</span>, ";
            states1 += "<a href=\"" + temp + "\"><span class=\"CountryInternalLink\" style=\"font-weight:normal;font-style:normal\">" + dr["Country"].ToString().Replace(" ", "&nbsp;") + "</span></a>, ";
            regionCountry = dr["Region"].ToString();
        }
        states1 = "";
        foreach (DataRow dr in MainDataSet.Tables["StateProvinces"].Rows)
        {
            string temp = CommonFunctions.GetSiteAddress() + "/" + country.ToLower().Replace(" ", "_") +
                  "/" + dr["StateProvince"].ToString().ToLower().Replace(" ", "_") + "/default.aspx";
            states1 += "<a href=\"" + temp + "\"><span class=\"CountryInternalLink\" style=\"font-weight:normal;font-style:normal\">" + dr["StateProvince"].ToString().Replace(" ", "&nbsp;") + "</span></a>, ";
        }
        rtLow3.InnerHtml = rtLow3.InnerHtml.Remove(rtLow3.InnerHtml.Length - 2, 2);
        rtHd3.InnerHtml = regionCountry + ":";

        //add counties to right column        
        //add counties within state
        string query = "";

        dt = obj1.spGetRightSideCounties(stateprovinceid);
        if (dt.Rows.Count > 0)
        {
            rtCountiesHd.InnerHtml = stateprovince + " Counties";
            foreach (DataRow datarow in dt.Rows)
            {
                if (datarow["county"] is string)
                {
                    string temp = CommonFunctions.GetSiteAddress() + "/" + stateprovince + "/Holiday-Rentals/" +
                           datarow["county"].ToString() + "-Vacation_Rentals/default.aspx";
                    temp = temp.ToLower();
                    temp = temp.Replace(' ', '_');
                    divCitiesRt.InnerHtml += "<a href=\"" + temp + "\"><span class=\"CountryInternalLink\">" + datarow["county"].ToString().Replace(" ", "&nbsp;") + "</span></a>, ";
                }
            }
            divCitiesRt.InnerHtml = divCitiesRt.InnerHtml.Remove(divCitiesRt.InnerHtml.Length - 2, 2);
        }
        else
        {
            rtCountyOut.Visible = false;
            lblBr.Text = "";
            lblBr.Visible = false;
        }

        //add counties within state                
        DataTable dtCountries = obj1.spStateProvByCountries(countryid);
        rtLowerHd.InnerHtml = country + " States";
        foreach (DataRow row in dtCountries.Rows)
        {
            if (row["stateprovince"] is string)
            {
                string temp = CommonFunctions.GetSiteAddress() + "/" + country + "/" + row["stateprovince"].ToString() + "/default.aspx";
                temp = temp.ToLower();
                temp = temp.Replace(' ', '_');

                rtLower.InnerHtml += "<a href=\"" + temp + "\">" + row["stateprovince"].ToString().Replace(" ", "&nbsp;") + "</a>, ";
            }
        }
        rtLower.InnerHtml = rtLower.InnerHtml.Remove(rtLower.InnerHtml.Length - 2, 2);
        /////// common for postback and ! postback ////////



        string tempstate = CommonFunctions.GetSiteAddress() + "/" + country.ToLower().Replace(" ", "_") +
                                         "/default.aspx";

        Session["tempstate"] = stateprovince;
        Session["tempcountry"] = country;


        lbltText.Text = lbltText.Text + states1;

        Page.Header.Controls.Add(new LiteralControl("<link href='/css/StyleSheetBig4.css?6=4' rel='stylesheet' type='text/css'></script>"));

       

        //HtmlMeta description = new HtmlMeta();

        //description.Name = "description";
        //description.Content = Description.Text.Replace("%country%", country).Replace("%stateprovince%", stateprovince).
        //    Replace("%cities%", cities);
        //// Description OVER RIDE area

        //string DescripReplacement = MainDataSet.Tables["Location"].Rows[0]["descriptionoverride"].ToString();
        //if (DescripReplacement.Length > 0)
        //    description.Content = DescripReplacement;
        Page page1 = (Page)HttpContext.Current.Handler;
        //description.Content = page1.Title;
        //head.Controls.Add(description);
        HtmlMeta keywords = new HtmlMeta();

        keywords.Name = "keywords";
        keywords.Content = Keywords.Text.Replace("%country%", country).Replace("%stateprovince%", stateprovince).
            Replace("%cities%", cities);
        keywords.Content = page1.Title;
        head.Controls.Add(keywords);
        ((System.Web.UI.WebControls.Image)Master.FindControl("Logo")).AlternateText = page1.Title;
       
    }

    private void BindCountryFirst()
    {
        DataTable dt = new DataTable();
        List<string> vList = new List<string>();
        DataTable dtCategories = new DataTable();
        DataFunctions obj = new DataFunctions();
        dt = obj.PropertiesByCase(vList, countryid, "Country");
        DataView dv = dt.DefaultView;
        dv.Sort = "category asc";
        dt = dv.ToTable();
        dtCategories = obj.FindNumCategorieswithImage(dt);
        dtlStates.DataSource = dtCategories;
        dtlStates.DataBind();

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        
        string strCountryText = txtCountryText.Text.Replace(Environment.NewLine, "<br />");
        DataTable dt = VADBCommander.GetMainCountryText(countryid.ToString());
        try
        {
            if (dt.Rows.Count > 0)
            {
                VADBCommander.CountryTextEdit(countryid.ToString(), strCountryText, null);

            }
            else
            {
                VADBCommander.CountryTextAdd(countryid.ToString(), strCountryText, null);
            }
            lblInfo.Text = "Data Saved successfully";
            lblerrormsg.Text = string.Empty;
            DataTable dt4 = VADBCommander.GetMainCountryText(countryid.ToString());
            if (dt4.Rows.Count > 0)
            {
                if (dt4.Rows[0]["countryText"] != null)
                {
                    lblCountryInfo.Text = dt4.Rows[0]["countryText"].ToString();
                    txtCountryText.Text = dt4.Rows[0]["countryText"].ToString().Replace("<br />-ipx-", Environment.NewLine);

                }
                if (dt4.Rows[0]["countryText2"] != null)
                {
                    lblInfo2.Text = dt4.Rows[0]["countryText2"].ToString();
                    txtCountryText2.Text = dt4.Rows[0]["countryText2"].ToString().Replace("<br />", Environment.NewLine);
                }
            }
            BindCountryFirst();
        }
        catch (Exception ex)
        {
            lblInfo.Text = ex.Message;
        }

    }

    protected void btnSubmit2_Click(object sender, EventArgs e)
    {
        string strCoutryText2 = txtCountryText2.Text.Replace(Environment.NewLine, "<br />");
        DataTable dt = VADBCommander.GetMainCountryText(countryid.ToString());
        try
        {
            if (dt.Rows.Count > 0)
            {
                VADBCommander.CountryText2Edit(countryid.ToString(), strCoutryText2, null);
            }
            else
            {
                VADBCommander.CountryText2Add(countryid.ToString(), strCoutryText2, null);
            }
            lblerrormsg.Text = "Data Saved successfully";
            lblInfo.Text = string.Empty;
            DataTable dt4 = VADBCommander.GetMainCountryText(countryid.ToString());
            if (dt4.Rows.Count > 0)
            {
                if (dt4.Rows[0]["CountryText"] != null)
                {
                    lblCountryInfo.Text = dt4.Rows[0]["CountryText"].ToString();
                    txtCountryText.Text = dt4.Rows[0]["CountryText"].ToString().Replace("<br />-ipx-", Environment.NewLine);

                }
                if (dt4.Rows[0]["CountryText2"] != null)
                {
                    lblInfo2.Text = dt4.Rows[0]["CountryText2"].ToString();
                    txtCountryText2.Text = dt4.Rows[0]["CountryText2"].ToString().Replace("<br />", Environment.NewLine);
                }
            }
            BindCountryFirst();
        }
        catch (Exception ex)
        {
            lblerrormsg.Text = ex.Message;
        }
        
    }
    
    protected void rptBound(object sender, EventArgs e)
    {
        //HtmlGenericControl div = 
    }

    
    

    private void FillCitiesColumn()
    {
        DBConnection obj = new DBConnection();
        string query = "select city from cities where stateprovinceid=" + stateprovinceid;
        SqlDataReader reader;
        bool start = true;
        try
        {
            //find all cities within a state and format into right column
            //try to use cities variable in state page
            reader = obj.ExecuteRecordSetArtificial(query);

            divCitiesRt.InnerHtml = "<center>Other Cities in " + stateprovince + "</center><br/>";
            while (reader.Read())
            {
                if (start == true)
                {
                    divCitiesRt.InnerHtml += reader["city"].ToString();
                    start = false;
                }
                else
                    divCitiesRt.InnerHtml += ", " + reader["city"].ToString();
            }
        }
        catch (Exception ex) { lblInfo22.Text = ex.Message; }
        finally { obj.CloseConnection(); }
    }
   
    public int[] FindNumAmenities(DataTable dt)
    {
        int[] i = new int[4];
        int vHot = 0;
        int vInternet = 0;
        int vPets = 0;
        int vPool = 0;
        for (int x = 0; x < dt.Rows.Count; x++)
        {

            if (dt.Rows[x]["InternetAccess"].ToString() != "")
                vInternet++;
            if (dt.Rows[x]["PetFriendly"].ToString() != "")
                vPets++;
            if ((dt.Rows[x]["SharedPool"].ToString() != "") || (dt.Rows[x]["PrivPool"].ToString() != ""))
                vPool++;
            if (dt.Rows[x]["HotTub"].ToString() != "")
                vHot++;
        }
        i[0] = vHot;
        i[1] = vInternet;
        i[2] = vPets;
        i[3] = vPool;

        return i;
    }
    public string GetTitle()
    {

        string titlereplacement = MainDataSet.Tables["Location"].Rows[0]["titleoverride"].ToString();
        if (titlereplacement.Length > 0)
            Title.Text = "" + titlereplacement;
        else
            return Title.Text.Replace("%country%", country);
        return Title.Text;
    }

    public string TableTitle()
    {


        string temp = stateprovince + " " + country;

        return temp + " Vacation Rentals " + temp + " Holiday Rentals";
    }

    public string commalove()
    {
        string temp = null;

        String.Format("{0:yes;;no}", temp);
        return temp;

    }


    

    
    protected DataTable TableRandomizer(DataTable dt)
    {
        DataTable dtRandom = dt.Clone();

        Random rnd = new Random();
        int left = 0;
        List<int> lstDiscard = new List<int>();
        int j = 0;
        while (left < dt.Rows.Count - 5)
        {
            DataRow rowRnd = dtRandom.NewRow();
            j = rnd.Next(dt.Rows.Count - 1);

            if (!lstDiscard.Contains(j))
            {
                rowRnd.ItemArray = dt.Rows[j].ItemArray;
                lstDiscard.Add(j);
                dtRandom.Rows.Add(rowRnd);

                left++;
            }
        }
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (!lstDiscard.Contains(i))
            {
                DataRow rowNew = dtRandom.NewRow();
                rowNew.ItemArray = dt.Rows[i].ItemArray;
                dtRandom.Rows.Add(rowNew);

            }
        }

        return dtRandom;
    }
    
    protected int GetPropertyCountForCity(int cityId)
    {
        return MainDataSet.Tables["Properties"].Select("CityID = " + cityId).Length;
    }
   
    protected void dtlStates_ItemCommand(object source, DataListCommandEventArgs e)
    {
        

    }
    protected void dtlStates_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        Label lblCategory = e.Item.FindControl("lblCat") as Label;
        HyperLink hCategory = e.Item.FindControl("hCategory") as HyperLink;
        
        hCategory.NavigateUrl = CommonFunctions.PrepareURL(country + "/" + lblCategory.Text.Replace(" ", "_").Replace("&", "and").ToLower() + "/default.aspx");
        
    }
    static string UppercaseFirst(string s)
    {
        if (string.IsNullOrEmpty(s))
        {
            return string.Empty;
        }
        char[] a = s.ToCharArray();
        a[0] = char.ToUpper(a[0]);
        return new string(a);
    }
    protected void lnkViewMap_Click(object sender, EventArgs e)
    {
        Session["CountryId"] = countryid;
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(Page.GetType(), "PopupForMaps", "window.open(\"Maps.aspx\", \"_blank\", \"toolbar=yes, scrollbars=yes, resizable=yes, top=500, left=500, width=400, height=400\");", true);
    }
}
