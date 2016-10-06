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

/*
public class Location
{
    public string title;
    public double lat;
    public double lng;
    public string description;
}
*/
public partial class CountryList : CommonPage
{
    #region Page Variables

    private const string STR_SELECTCitiesFROMCitiesWHERECitiesStateProvinceID = "SELECT Cities.*, Counties.CountyID FROM Cities  INNER JOIN StateProvinces ON StateProvinces.ID = Cities.StateProvinceID left outer join counties on cities.id=counties.cityid WHERE (StateProvinces.CountryID = @CountryID) AND EXISTS ( SELECT * FROM Properties WHERE (Properties.IfFinished = 1) AND (Properties.IfApproved = 1) AND (Properties.CityID = Cities.ID)  AND NOT EXISTS (SELECT * FROM Auctions WHERE PropertyID = Properties.ID)) ORDER BY City";
    
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
    //protected SqlDataAdapter CountyAdapter;

    protected DataSet MainDataSet = new DataSet();
    protected DataSet MainDataSetCountries = new DataSet();

    #endregion

    protected void Page_Load(object sender, System.EventArgs e)
    {
        
       
       
	RegionCountriesAdapter = CommonFunctions.PrepareAdapter(CommonFunctions.GetConnection(), "select distinct Country,Region from Countries inner join Regions on Countries.RegionID=Regions.ID "+
        " inner join StateProvinces on StateProvinces.CountryID=Countries.ID "+
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

        PropertyTypesAdapter = CommonFunctions.PrepareAdapter(CommonFunctions.GetConnection(), "SELECT     PropertyTypes.Name,COUNT(*) as Count, PropertyTypes.ID "+
                     " FROM         Cities INNER JOIN "+
                      " Properties ON Cities.ID = Properties.CityID  "+
                      " INNER JOIN "+
                      " PropertyTypes ON Properties.TypeID = PropertyTypes.ID INNER JOIN "+
                      " StateProvinces ON Cities.StateProvinceID = StateProvinces.ID INNER JOIN "+
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
                Session["dt"] = dt;
                 int[] i = new int[4];
                i = FindNumAmenities(dt);

                dtCategories = obj.FindNumCategories(dt);
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
                    string vTemp = row["category"].ToString() + " (" + row["count"].ToString() + ")";
                    vTemp = vTemp.Replace(" ", "&nbsp;");
                    vCategoryCount = vCategoryCount + Convert.ToInt32(row["count"].ToString());
                }
                
                if (!IsPostBack)
                {
                    
                    //dtlStates.DataSource = dtCategories;
                    //dtlStates.DataBind();
                }
                //numbedrooms filter
                dtCategories = obj.FindNumBedrooms(dt);
                int vBedCount = 0;
                foreach (DataRow row in dtCategories.Rows)
                {
                    vBedCount += Convert.ToInt32(row["count"]);
                }
               
                Page page = (Page)HttpContext.Current.Handler;
                if (Request.QueryString["category"] != null)
                {
                    Response.Clear();
                    Response.StatusCode = 404;
                    Response.End();
                }
                if (dt.Rows.Count <= 10)
                {
					//Implement 404 logic less then 10 property with Prorerty in URL - Develop By Nimesh Sapovadiya
					if(Request.QueryString["category"] != null)
					{
						Response.Clear();
						Response.StatusCode = 404;
						Response.End(); 
					}
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
                    firstCategory = dt.Rows[0]["category"].ToString();
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
                    ltrH11.Text = char.ToUpper(country[0]) + country.Substring(1) + " Vacation Rentals";
                    hyplnkBackLink.NavigateUrl = "/" + region.ToLower().Replace(" ", "_")+"/default.aspx";
                    ltrBackText.Text = region + "<<";
		    hyplnkAllProps.NavigateUrl = "/" + country.ToLower().Replace(" ", "_") + "/countryproperties.aspx";
                    ltrAllProps.Text = " View all " + char.ToUpper(country[0]) + country.Substring(1) + " properties";
                    ltrHeading.Text = char.ToUpper(country[0]) + country.Substring(1) + " Vacations";
                    string iframe = "<iframe height='260' width='95%' frameborder='0' src='/" + country + "/Maps.aspx'></iframe>";
                    ltrCountryThing.Text = char.ToUpper(country[0]) + country.Substring(1);
                    googleCountrymap.InnerHtml = iframe;
                    page.Title = char.ToUpper(country[0]) + country.Substring(1) + " Vacation Rentals, Boutique Hotels | Vacations Abroad";

                   string tempcountry1 = CommonFunctions.GetSiteAddress() + "/" + country.ToLower().Replace(" ", "_") +
                                               "/default.aspx";

                  /*  HtmlMeta description = new HtmlMeta();
                    description.Name = "description";
                    description.Content = "Plan your next " + char.ToUpper(country[0]) + country.Substring(1) + " vacation: where to stay and places to visit ";
                    head.Controls.Add(description);
                    */
                }
                else
                {

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
                            dispString = UppercaseFirst(strSplit[0]) + " " + UppercaseFirst(strSplit[1])+"s";
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
                        ltrH11.Text = char.ToUpper(country[0]) + country.Substring(1) + " Vacation Rentals";
                        hyplnkBackLink.NavigateUrl = "/" + region.ToLower().Replace(" ", "_")+"/default.aspx";
                        ltrBackText.Text = region + "<<";
			hyplnkAllProps.NavigateUrl = "/" + country.ToLower().Replace(" ", "_") + "/countryproperties.aspx";
			ltrAllProps.Text = " View all " + char.ToUpper(country[0]) + country.Substring(1) + " properties";
                        ltrHeading.Text = char.ToUpper(country[0]) + country.Substring(1) + " Vacations";

                        string iframe = "<iframe height='260' frameborder='0' width='95%' src='/" + country + "/Maps.aspx'></iframe>";
                        googleCountrymap.InnerHtml = iframe;
                        ltrCountryThing.Text = char.ToUpper(country[0]) + country.Substring(1);
                        page.Title = char.ToUpper(country[0]) + country.Substring(1) + " Vacation Rentals, Boutique Hotels | Vacations Abroad";
                       
                    }
                    if (firstCategory == "bandb")
                    {
                        firstCategory = "B&B";
                    }


                    DataTable dtCategory = dt.Clone();
                    foreach (DataRow dr in dt.Rows)
                    {
                        string vTemp = dr["Category"].ToString(); 
                        if (vTemp.ToLower().Replace(" ", "").Trim() == firstCategory.ToLower().Replace("_", " ").Replace(" ", ""))
                        {
                            subCategory = dr["SubCategory"].ToString();
                            dtCategory.ImportRow(dr);
                        }
                    }
                    DataView dv1 = dtCategory.DefaultView;
                    dv1.Sort = "MinNightRate desc";

                    if (Request.QueryString["category"] != null)
                    {
                        string dispString = "";
                        if (firstCategory.Contains("_"))
                        {
                            string[] strSplit = firstCategory.Split('_');
                            dispString = UppercaseFirst(strSplit[0]) + " " + UppercaseFirst(strSplit[1])+"s";
                        }
                        else
                        {
                            dispString = UppercaseFirst(firstCategory) + "s";
                        }
                        ltrH11.Text = char.ToUpper(country[0]) + country.Substring(1) + " Vacation Rentals";
                        page.Title = char.ToUpper(country[0]) + country.Substring(1) + "Vacation Rentals, Boutique Hotels | Vacations Abroad";
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
                        ltrH11.Text = char.ToUpper(country[0]) + country.Substring(1) + " Vacation Rentals";
                        page.Title = char.ToUpper(country[0]) + country.Substring(1) + " Vacation Rentals, Boutique Hotels | Vacations Abroad";
                        
                    }
                    string tempcountry = CommonFunctions.GetSiteAddress() + "/" + country.ToLower().Replace(" ", "_") +
                                                "/default.aspx";

                   /* HtmlMeta description = new HtmlMeta();
                    description.Name = "description";
                    description.Content = "Plan your next " + char.ToUpper(country[0]) + country.Substring(1) + " vacation: where to stay and places to visit ";// + char.ToUpper(country[0]) + country.Substring(1) + " Vacations with unique " + char.ToUpper(country[0]) + country.Substring(1) + " vacation rentals.";
                    head.Controls.Add(description);
                    */
                }

                ViewState["firstCategory"] = firstCategory;
                DataTable dt1 = new DataTable();
                try
                {
                    dt1 = VADBCommander.CountryTextInd(countryid.ToString(), firstCategory);
                }
                catch (Exception ex) { lblInfo.Text = ex.Message; }

                string vText = "Vacations-abroad.com is a " + country + " accommodation directory of " + country + " rentals by owner and privately owned " + country + " holiday accommodation. Our short term " + country + " rentals include luxury " +
               country + " holiday homes, " + country + " vacation homes and " + country + " vacation home rentals which are perfect for group or family vacation rentals in " + country + " " + region;

                if (dt1.Rows.Count > 0)
                {

                    if (dt1.Rows[0]["countryText"] != null)
                    {
                        if (!IsPostBack)
                        {
                            lblCountryInfo.Text = dt1.Rows[0]["countryText"].ToString();
                            txtCountryText.Text = dt1.Rows[0]["countryText"].ToString().Replace("<br />", Environment.NewLine);
                        }
                        ////Editor.Value = dt.Rows[0]["cityText"].ToString();
                    }
                    else
                    {
                        lblCountryInfo.Text = vText;
                        txtCountryText.Text = vText;
                    }
                    if (dt1.Rows[0]["countryText2"] != null)
                    {
                        if (!IsPostBack)
                        {
                            lblInfo2.Text = dt1.Rows[0]["countryText2"].ToString();
                            if (string.IsNullOrEmpty(lblInfo2.Text) || lblInfo2.Text == "")
                            {
                                OrangeTitle.Visible = false;
                            }
                            txtCountryText2.Text = dt1.Rows[0]["countryText2"].ToString().Replace("<br />", Environment.NewLine);
                        }
                    }
                    else
                    {
                        OrangeTitle.Visible = false;
                    }
                }
                else
                {
                    lblCountryInfo.Text = vText;
                    txtCountryText.Text = vText;
                    OrangeTitle.Visible = false;
                }
            }
        }
        catch (Exception ex) { lblInfo.Text = ex.Message; }

        DBConnection obj3 = new DBConnection();
        SqlDataReader reader = obj3.ExecuteRecordSetArtificial("SELECT Cities.* FROM Cities WHERE (Cities.StateProvinceID = " + stateprovinceid + ") " + "AND EXISTS ( SELECT * FROM Properties WHERE (Properties.IfFinished = 1) AND (Properties.IfApproved = 1) AND " + "(Properties.CityID = Cities.ID)  AND NOT EXISTS (SELECT * FROM Auctions WHERE PropertyID = Properties.ID)) ORDER " + "BY City");
        string states1 = "";
        string regionCountry = "";
        foreach (DataRow dr in MainDataSet.Tables["CountriesRegion"].Rows)
        {
            string temp = "/" + dr["Country"].ToString().ToLower().Replace(" ", "_") + "/default.aspx";
            temp = temp.ToLower();
            temp = temp.Replace(' ', '_');
            rtLow3.Text += "<li><a href=\"" + temp + "\"><span class=\"tdNoSleeps\">" + dr["Country"].ToString().Replace(" ", "&nbsp;") + ",</span></a></li>";
            states1 += "<a href=\"" + temp + "\"><span class=\"tdNoSleeps\" style=\"font-weight:normal;font-style:normal\">" + dr["Country"].ToString().Replace(" ", "&nbsp;") + "</span></a>, ";
            regionCountry = dr["Region"].ToString();
        }
        states1 = "";
        foreach (DataRow dr in MainDataSet.Tables["StateProvinces"].Rows)
        {
            string temp = "/" + country.ToLower().Replace(" ", "_") + "/" + dr["StateProvince"].ToString().ToLower().Replace(" ", "_") + "/default.aspx";
            states1 += "<a href=\"" + temp + "\"><span class=\"tdNoSleeps\" style=\"font-weight:normal;font-style:normal\">" + dr["StateProvince"].ToString().Replace(" ", "&nbsp;") + "</span></a>, ";
        }

        states1 = "";
        string str_states = "";
        string str_keyword = "";
        int ind = 0;
        // string cls = " class='borderright' ";
        string cls = "border-right:1px solid #0094ff;";
        string li = "";
        foreach (DataRow dr in MainDataSet.Tables["StateProvinces"].Rows)
        {
            DataFunctions objcate = new DataFunctions();
            DataTable dt1 = new DataTable();
            dt = obj.PropertiesByCase(vList, Convert.ToInt32(dr["id"]), "State");

            li =" style='"+ ((ind > 4) ? "border-top:0px;" : "")+ (((ind++ % 5) == 4) ? cls : "")+"'";

            string temp = "/" + country.ToLower().Replace(" ", "_") + "/" + dr["StateProvince"].ToString().ToLower().Replace(" ", "_") + "/default.aspx";
            states1 += "<li"+li +"><a href='" + temp + "' class='StateTitle'>" + dr["StateProvince"].ToString().Replace(" ", "&nbsp;") + "</a><br/> ";
            states1 += "<a href=\"" + temp + "\"><img width='160' height='125' src='http://www.vacations-abroad.com/images/" + Convert.ToString(dt.Rows[0]["PhotoImage"]) + "' alt='" + Convert.ToString(dr["StateProvince"]) + " vacation rentals and boutique hotels ' title='" + Convert.ToString(dr["StateProvince"]) + " vacation rentals and boutique hotels ' /></a></li>";
            str_states += Convert.ToString(dr["StateProvince"]) + ", ";
            str_keyword += Convert.ToString(dr["StateProvince"]) + " " + country + ", ";
        }
        rtLow3.Text = rtLow3.Text.Remove(rtLow3.Text.Length - 2, 2);
        rtHd3.InnerHtml = regionCountry + " Countries";

        //add counties to right column        
        //add counties within state
        string query = "";

        /////// common for postback and ! postback ////////



        string tempstate = "/" + country.ToLower().Replace(" ", "_") + "/default.aspx";
       
        Session["tempstate"] = stateprovince;
        Session["tempcountry"] = country;
        

        Statesul.InnerHtml = states1;
        //Page.Header.Controls.Add(new LiteralControl("<link href='http://vacations-abroad.com/css/StyleSheetBig4.css' rel='stylesheet' type='text/css'></script>"));

        int num_properties = MainDataSet.Tables["Properties"].Rows.Count;
        
        
        Page page1 = (Page)HttpContext.Current.Handler;

        HtmlMeta newdescription = new HtmlMeta();

        string str_meta = "(%number%) %country% vacation rentals and boutique hotels in %states% etc.";

        newdescription.Name = "description";
        newdescription.Content = str_meta.Replace("%country%", country).Replace("%states%", str_states).Replace("%number%", Convert.ToString(num_properties));

        head.Controls.Add(newdescription);

        HtmlMeta keywords = new HtmlMeta();

        keywords.Name = "keywords";
        // keywords.Content = Keywords.Text.Replace("%country%", country).Replace("%stateprovince%", stateprovince).
        //    Replace("%cities%", cities);
        //  keywords.Content = page1.Title;
        keywords.Content = str_keyword + "etc.";
        head.Controls.Add(keywords);
    }

    private HtmlControl FindControl(string p)
    {
        throw new NotImplementedException();
    }

   
    protected void rptBound(object sender, EventArgs e)
    {
        //HtmlGenericControl div = 
    }

    protected void btnSubmit2_Click(object sender, EventArgs e)
    {
        string strCoutryText2 = txtCountryText2.Text.Replace(Environment.NewLine, "<br />");
        DataTable dt = VADBCommander.CountryTextInd(countryid.ToString(), Convert.ToString(ViewState["firstCategory"]));
        try
        {
            if (dt.Rows.Count > 0)
            {
                VADBCommander.CountryText2Edit(countryid.ToString(), strCoutryText2, Convert.ToString(ViewState["firstCategory"]));
            }
            else
            {
                VADBCommander.CountryText2Add(countryid.ToString(), strCoutryText2, Convert.ToString(ViewState["firstCategory"]));
            }
            lblInfo2.Text = "Data saved.";
            DataTable dt4 = VADBCommander.CountryTextInd(countryid.ToString(), Convert.ToString(ViewState["firstCategory"]));
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
                    if (string.IsNullOrEmpty(lblInfo2.Text) || lblInfo2.Text == "")
                    {
                        OrangeTitle.Visible = false;
                    }
                    txtCountryText2.Text = dt4.Rows[0]["CountryText2"].ToString().Replace("<br />", Environment.NewLine);
                }
                else
                {
                    OrangeTitle.Visible = false;
                }
            }
            else
            {
                OrangeTitle.Visible = false;
            }
        }
        catch (Exception ex)
        {
            lblInfo2.Text = ex.Message;
        }
        lblInfo2.ForeColor = System.Drawing.Color.Red;
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        HttpResponse.RemoveOutputCacheItem("/StateProvinceList.aspx");
        string strCountryText = txtCountryText.Text.Replace(Environment.NewLine, "<br />");
        DataTable dt = VADBCommander.CountryTextInd(countryid.ToString(), Convert.ToString(ViewState["firstCategory"]));
        try
        {
            if (dt.Rows.Count > 0)
            {
                VADBCommander.CountryTextEdit(countryid.ToString(), strCountryText, Convert.ToString(ViewState["firstCategory"]));

            }
            else
            {
                VADBCommander.CountryTextAdd(countryid.ToString(), strCountryText, Convert.ToString(ViewState["firstCategory"]));
            }
            lblInfo.Text = "Data saved.";
            DataTable dt4 = VADBCommander.CountryTextInd(countryid.ToString(), Convert.ToString(ViewState["firstCategory"]));
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

                    if (string.IsNullOrEmpty(lblInfo2.Text))
                    {
                        OrangeTitle.Visible = false;
                    }
                    txtCountryText2.Text = dt4.Rows[0]["countryText2"].ToString().Replace("<br />", Environment.NewLine);
                }
            }
        }
        catch (Exception ex)
        {
            lblInfo.Text = ex.Message;
        }

    }

    private void FillCitiesColumn()
    {
        //DBConnection obj = new DBConnection();
        //string query = "select city from cities where stateprovinceid=" + stateprovinceid;
        //SqlDataReader reader;
        //bool start = true;
        //try
        //{
        //    //find all cities within a state and format into right column
        //    //try to use cities variable in state page
        //    reader = obj.ExecuteRecordSetArtificial(query);

        //    divCitiesRt.InnerHtml = "<center>Other Cities in " + stateprovince + "</center><br/>";
        //    while (reader.Read())
        //    {
        //        if (start == true)
        //        {
        //            divCitiesRt.InnerHtml += reader["city"].ToString();
        //            start = false;
        //        }
        //        else
        //            divCitiesRt.InnerHtml += ", " + reader["city"].ToString();
        //    }
        //}
        //catch (Exception ex) { lblInfo22.Text = ex.Message; }
        //finally { obj.CloseConnection(); }
    }
    public void bindStates()
    {
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


    protected void CheckBoxList1_SelectedIndexChanged(object sender, EventArgs e)
    {


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
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex != -1)
        {

        }
    }
    protected void dtlStates_ItemCommand(object source, DataListCommandEventArgs e)
    {
        DataTable dt = new DataTable();
        DataTable dtCategories = new DataTable();
        DataFunctions obj = new DataFunctions();
        List<string> vList = new List<string>();
        dt = (DataTable)Session["dt"];
        DataFunctions obj1 = new DataFunctions();
        DataTable dtCategory = dt.Clone();
        
            foreach (DataRow dr in dt.Rows)
            {
                string vTemp = dr["Category"].ToString(); //+ " (" + dr["count"].ToString() + ")";
                 if (vTemp.Replace(" ", "").Trim() == e.CommandArgument.ToString().Replace(" ",""))
                {
                    dtCategory.ImportRow(dr);
                }
            }
            //State_datagrid.DataSource = dtCategory;
            //State_datagrid.DataBind();

    }
    protected void dtlStates_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        Label lblCategory = e.Item.FindControl("lblCat") as Label;
        HyperLink hCategory = e.Item.FindControl("hCategory") as HyperLink;
        if (e.Item.ItemIndex == 0)
        {
            hCategory.NavigateUrl = CommonFunctions.PrepareURL(country + "/default.aspx");
        }
        else
        {
            hCategory.NavigateUrl = CommonFunctions.PrepareURL(country + "/" + lblCategory.Text.Replace(" ", "_").Replace("&", "and").ToLower() + "/default.aspx");
        }
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
    protected void dtlStates_ItemDataBound1(object sender, RepeaterItemEventArgs e)
    {
        Label lblCategory = e.Item.FindControl("lblCat") as Label;
        HyperLink hCategory = e.Item.FindControl("hCategory") as HyperLink;
        if (e.Item.ItemIndex == 0)
        {
            hCategory.NavigateUrl = CommonFunctions.PrepareURL(country + "/default.aspx");
        }
        else
        {
            hCategory.NavigateUrl = CommonFunctions.PrepareURL(country + "/" + lblCategory.Text.Replace(" ", "_").Replace("&", "and").ToLower() + "/default.aspx");
        }
    }
    protected void dtlStates_ItemCommand1(object source, RepeaterCommandEventArgs e)
    {
        DataTable dt = new DataTable();
        DataTable dtCategories = new DataTable();
        DataFunctions obj = new DataFunctions();
        List<string> vList = new List<string>();
        dt = (DataTable)Session["dt"];
        DataFunctions obj1 = new DataFunctions();
        DataTable dtCategory = dt.Clone();

        foreach (DataRow dr in dt.Rows)
        {
            string vTemp = dr["Category"].ToString(); //+ " (" + dr["count"].ToString() + ")";
            if (vTemp.Replace(" ", "").Trim() == e.CommandArgument.ToString().Replace(" ", ""))
            {
                dtCategory.ImportRow(dr);
            }
        }
    }
}
