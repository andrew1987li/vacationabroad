
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

public partial class StateProvinceList : CommonPage
{
    private const string STR_SELECTCitiesFROMCitiesWHERECitiesStateProvinceID = "SELECT Distinct Cities.* FROM Cities left outer join counties on cities.id=counties.cityid WHERE (Cities.StateProvinceID = @stateprovinceID) AND EXISTS ( SELECT * FROM Properties WHERE (Properties.IfFinished = 1) AND (Properties.IfApproved = 1) AND (Properties.CityID = Cities.ID)  AND NOT EXISTS (SELECT * FROM Auctions WHERE PropertyID = Properties.ID)) ORDER BY City";
    
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

    protected SqlDataAdapter CitiesAdapter;
    protected SqlDataAdapter PropertiesAdapter;
    protected SqlDataAdapter AmenitiesAdapter;
    protected SqlDataAdapter LocationAdapter;
    protected SqlDataAdapter StateProvincesAdapter;
    protected SqlDataAdapter LocationAdapterCountry;
    protected DataSet MainDataSet = new DataSet();
    protected DataSet MainDataSetCountries = new DataSet();

    protected void Page_Load(object sender, System.EventArgs e)
    {
        
	StateProvincesAdapter = CommonFunctions.PrepareAdapter(CommonFunctions.GetConnection(), "SELECT StateProvinces.* " +
            "FROM StateProvinces " +
            "WHERE (StateProvinces.CountryID = @CountryID) AND EXISTS (" +
            " SELECT * FROM Properties INNER JOIN Cities ON Properties.CityID = Cities.ID" +
            " WHERE (Properties.IfFinished = 1) AND (Properties.IfApproved = 1)" +
            " AND (Cities.StateProvinceID = StateProvinces.ID) " +
            " AND NOT EXISTS (SELECT * FROM Auctions WHERE PropertyID = Properties.ID)) " +
            "ORDER BY StateProvince", SqlDbType.Int);
        CitiesAdapter = CommonFunctions.PrepareAdapter(CommonFunctions.GetConnection(), String.Format(STR_SELECTCitiesFROMCitiesWHERECitiesStateProvinceID), SqlDbType.Int);

        const string STR_SELECTPropertiesInfo = "SELECT Properties.Name, Properties.NumBedrooms, Properties.NumBaths, Properties.NumSleeps, Properties.NumTVs, Properties.NumVCRs, Properties.CityID, Properties.NumCDPlayers, Properties.ID, CASE WHEN EXISTS (SELECT * FROM PropertiesAmenities INNER JOIN Amenities ON PropertiesAmenities.AmenityID = Amenities.ID WHERE (PropertiesAmenities.PropertyID = Properties.ID) AND (Amenities.Amenity = 'Beach Front')) THEN 'Beach Front' ELSE '' END AS BeachFront, CASE WHEN EXISTS (SELECT * FROM PropertiesAmenities INNER JOIN Amenities ON PropertiesAmenities.AmenityID = Amenities.ID WHERE (PropertiesAmenities.PropertyID = Properties.ID) AND (Amenities.Amenity = 'Seaside')) THEN 'Seaside' ELSE '' END AS Seaside, CASE WHEN EXISTS (SELECT * FROM PropertiesAmenities INNER JOIN Amenities ON PropertiesAmenities.AmenityID = Amenities.ID WHERE (PropertiesAmenities.PropertyID = Properties.ID) AND (Amenities.Amenity = 'Lake Front')) THEN 'Lake Front' ELSE '' END AS LakeFront, CASE WHEN EXISTS (SELECT * FROM PropertiesAmenities INNER JOIN Amenities ON PropertiesAmenities.AmenityID = Amenities.ID WHERE (PropertiesAmenities.PropertyID = Properties.ID) AND (Amenities.Amenity = 'River Front')) THEN 'River Front' ELSE '' END AS RiverFront, CASE WHEN EXISTS (SELECT * FROM PropertiesAmenities INNER JOIN Amenities ON PropertiesAmenities.AmenityID = Amenities.ID WHERE (PropertiesAmenities.PropertyID = Properties.ID) AND (Amenities.Amenity = 'Ski In Ski Out')) THEN 'Ski In Ski Out' ELSE '' END AS Ski, Cities.City, StateProvinces.StateProvince, Countries.Country, Regions.Region, MinimumNightlyRentalTypes.Name AS MinimumNightlyRental, PropertyTypes.Name AS Type FROM Properties INNER JOIN Cities ON Properties.CityID = Cities.ID INNER JOIN StateProvinces ON StateProvinces.ID = Cities.StateProvinceID INNER JOIN Countries ON StateProvinces.CountryID = Countries.ID INNER JOIN Regions ON Countries.RegionID = Regions.ID INNER JOIN Users ON Properties.UserID = Users.ID LEFT OUTER JOIN MinimumNightlyRentalTypes ON Properties.MinimumNightlyRentalID = MinimumNightlyRentalTypes.ID LEFT OUTER JOIN PropertyTypes ON Properties.TypeID = PropertyTypes.ID WHERE (Properties.IfFinished = 1) AND (Properties.IfApproved = 1) AND (Cities.StateProvinceID = @StateProvinceID) AND NOT EXISTS (SELECT * FROM Auctions WHERE PropertyID = Properties.ID) ORDER BY StateProvinces.StateProvince, Cities.City, Type, CASE WHEN EXISTS (SELECT * FROM Invoices WHERE (PropertyID = Properties.ID) AND (PaymentAmount >= InvoiceAmount) AND (GETDATE() <= Invoices.RenewalDate)) THEN 1 ELSE 0 END DESC, Properties.ID";
        PropertiesAdapter = CommonFunctions.PrepareAdapter(CommonFunctions.GetConnection(), String.Format(STR_SELECTPropertiesInfo), SqlDbType.Int);

        AmenitiesAdapter = CommonFunctions.PrepareAdapter(CommonFunctions.GetConnection(), "SELECT Amenities.ID, Amenity," +
            " PropertiesAmenities.PropertyID " +
            "FROM Amenities INNER JOIN PropertiesAmenities ON Amenities.ID = PropertiesAmenities.AmenityID" +
            " INNER JOIN Properties ON PropertiesAmenities.PropertyID = Properties.ID " +
            " INNER JOIN Cities ON Properties.CityID = Cities.ID " +
            "WHERE (Properties.IfFinished = 1) AND (Properties.IfApproved = 1) AND (Cities.StateProvinceID = @StateProvinceID)" +
            " AND NOT EXISTS (SELECT * FROM Auctions WHERE PropertyID = Properties.ID) AND (Amenities.Amenity NOT IN" +
            " ('Lake Front', 'Beach Front', 'River Front', 'Seaside', 'Ski In Ski Out', 'TV', 'VCR', 'CD Player'))",
            SqlDbType.Int);

        LocationAdapter = CommonFunctions.PrepareAdapter(CommonFunctions.GetConnection(), "SELECT StateProvinces.ID AS StateProvinceID," +
            " StateProvinces.StateProvince, Countries.ID AS CountryID, Countries.Country, Regions.ID AS RegionID," +
            " Regions.Region, stateprovinces.titleoverride, stateprovinces.descriptionoverride " +
            "FROM StateProvinces INNER JOIN Countries ON StateProvinces.CountryID = Countries.ID" +
            " INNER JOIN Regions ON Countries.RegionID = Regions.ID WHERE (StateProvinces.ID = @StateProvinceID)",
            SqlDbType.Int);


        if ((Request.Params["StateProvinceID"] != null) && (Request.Params["StateProvinceID"].Length > 0))
            try
            {
                stateprovinceid = Convert.ToInt32(Request.Params["StateProvinceID"]);
            }
            catch (Exception)
            {
            }
        // IF state is not found throw an error
        if (stateprovinceid == -1)
            Response.Redirect(CommonFunctions.PrepareURL("InternalError.aspx"));

        CitiesAdapter.SelectCommand.Parameters["@StateProvinceID"].Value = stateprovinceid;
        LocationAdapter.SelectCommand.Parameters["@StateProvinceID"].Value = stateprovinceid;
        PropertiesAdapter.SelectCommand.Parameters["@StateProvinceID"].Value = stateprovinceid;
        AmenitiesAdapter.SelectCommand.Parameters["@StateProvinceID"].Value = stateprovinceid;
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
        CitiesAdapter.Fill(MainDataSet, "Cities");
        PropertiesAdapter.Fill(MainDataSet, "Properties");
        AmenitiesAdapter.Fill(MainDataSet, "Amenities");
        StateProvincesAdapter.Fill(MainDataSet, "StateProvinces");
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
                cities += " " + (string)datarow["City"];

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
            Page page = (Page)HttpContext.Current.Handler;
            if (!IsPostBack)
            {
                dt = obj.PropertiesByCase(vList, stateprovinceid, "State");
                DataView dv = dt.DefaultView;
                dv.Sort = "category asc";
                dt = dv.ToTable();
                Session["dt"] = dt;
                
                int[] i = new int[4];
                i = FindNumAmenities(dt);

               

                //create rdo items from categories table
                dtCategories = obj.FindNumCategories(dt);
                int vCategoryCount = 0;
                string firstCategory = "";
                string subCategory = "";
                foreach (DataRow row in dtCategories.Rows)
                {
                    int index = dtCategories.Rows.IndexOf(row);
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

                    
                }        //numbedrooms filter
                dtCategories = obj.FindNumBedrooms(dt);
               
                int vBedCount = 0;
                foreach (DataRow row in dtCategories.Rows)
                {
                    vBedCount += Convert.ToInt32(row["count"]);
                }
                
					//Implement 404 logic less then 10 property with Prorerty in URL - Develop By Nimesh Sapovadiya
					if(Request.QueryString["category"] != null)
					{
						Response.Clear();
						Response.StatusCode = 404;
						Response.End(); 
					}
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

                    
                    altTag = char.ToUpper(stateprovince[0]) + stateprovince.Substring(1) + " Vacation Rentals";
                    ltrH1.Text = char.ToUpper(stateprovince[0]) + stateprovince.Substring(1) + " Vacations";// Rentals";// And " +  dispString;
                    ltrHeading.Text = char.ToUpper(stateprovince[0]) + stateprovince.Substring(1) + " Vacation Rentals and Hotels";
                    ltrStateThing.Text = char.ToUpper(stateprovince[0]) + stateprovince.Substring(1);
                    
                    hyplinkBackRegion.NavigateUrl = "/" + region.ToLower().ToLower().Replace(" ", "_") + "/" + "default.aspx";
                    ltrRegion.Text = region + "<<";

                    hyplnkBackLink.NavigateUrl = "/" + country.ToLower().ToLower().Replace(" ", "_") +"/" + "default.aspx";
                    ltrBackText.Text = country  + "<<";
                    string iframe = "<iframe height='260' width='500' frameborder='0' src='/" + country + "/" + stateprovince + "/Maps.aspx'></iframe>";
                    googlemap.InnerHtml = iframe;                    
                    page.Title = char.ToUpper(stateprovince[0]) + stateprovince.Substring(1) + " Vacation Rentals, Boutique Hotels | Vacations Abroad";
                    
                    string tempcountry1 = CommonFunctions.GetSiteAddress() + "/" + country.ToLower().Replace(" ", "_") + "/" + stateprovince.ToLower().Replace(" ", "_") +
                                               "/default.aspx";
                   
            }

            string vText = "Vacations-abroad.com is a " + stateprovince + " accommodation directory of " + stateprovince + " rentals by owner and privately owned " + stateprovince + " holiday accommodation. Our short term " + stateprovince + " rentals include luxury " +
               stateprovince + " holiday homes, " + stateprovince + " vacation homes and " + stateprovince + " vacation home rentals which are perfect for group or family vacation rentals in " + stateprovince + " " + country;

            //TOP DEFAULT TEXT
            if (!IsPostBack)
            {
                txtCityText.Text = vText;
            }
            lblcityInfo.Text = vText;

            //BOTTOM, NO DEFAULT TEXT                
            dt = VADBCommander.CityTextByStateInd(stateprovinceid.ToString());

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["cityText"] != null)
                {
                    if (!IsPostBack)
                    {
                        lblcityInfo.Text = dt.Rows[0]["cityText"].ToString();
                        txtCityText.Text = dt.Rows[0]["cityText"].ToString().Replace("<br />-ipx-", Environment.NewLine);
                    }

                }
                if (dt.Rows[0]["cityText2"] != null)
                {
                    if (!IsPostBack)
                    {
                        lblInfo2.Text = dt.Rows[0]["cityText2"].ToString();

                        if (string.IsNullOrEmpty(dt.Rows[0]["cityText2"].ToString()) || dt.Rows[0]["cityText2"].ToString() == "")
                        {
                            OrangeTitle.Visible = false;
                        }
                        txtCityText2.Text = dt.Rows[0]["cityText2"].ToString().Replace("<br />", Environment.NewLine);
                    }
                }
                else
                {
                    OrangeTitle.Visible = false;
                    ltrStateThing.Visible = false;
                }
            }
            else
            {
                OrangeTitle.Visible = false;
                ltrStateThing.Visible = false;
            }

            if (String.IsNullOrEmpty(lblcityInfo.Text))
            {
                //IF EMPTY VALUES OR 'DELETES' FROM ADMIN FOR TOP

                txtCityText.Text = vText;
                lblcityInfo.Text = vText;
            }
            if (Request.QueryString["Category"] != null)
            {
                lblInfo2.Visible = false;
                OrangeTitle.Visible = false;
                txtCityText.Visible = false;
            }
        }
        catch (Exception ex) { lblInfo.Text = ex.Message; }

        DBConnection obj3 = new DBConnection();
        SqlDataReader reader = obj3.ExecuteRecordSetArtificial("SELECT Cities.* FROM Cities WHERE (Cities.StateProvinceID = " + stateprovinceid + ") " + "AND EXISTS ( SELECT * FROM Properties WHERE (Properties.IfFinished = 1) AND (Properties.IfApproved = 1) AND " + "(Properties.CityID = Cities.ID)  AND NOT EXISTS (SELECT * FROM Auctions WHERE PropertyID = Properties.ID)) ORDER " + "BY City");
        foreach (DataRow dr in MainDataSet.Tables["StateProvinces"].Rows)
        {
            string temp = CommonFunctions.GetSiteAddress() + "/" + country +
                   "/" + dr["StateProvince"].ToString() + "/default.aspx";
            temp = temp.ToLower();
            temp = temp.Replace(' ', '_');
            rtLow3.Text += "<li><a href='" + temp + "'>" + dr["StateProvince"].ToString().Replace(" ", "&nbsp;") + ", </a></li>";
        }
        rtLow3.Text = rtLow3.Text;
        rtHd3.InnerHtml = country + " Regions: ";

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
                    divCitiesRt.Text += "<li><a href='" + temp + "'>" + datarow["county"].ToString().Replace(" ", "&nbsp;") + "</a></li> ";
                }
            }
            divCitiesRt.Text = divCitiesRt.Text.Remove(divCitiesRt.Text.Length - 2, 2);
        }
        else
        {
        }

        //add counties within state                
        DataTable dtCountries = obj1.spStateProvByCountries(countryid);
        foreach (DataRow row in dtCountries.Rows)
        {
            if (row["stateprovince"] is string)
            {
                string temp = CommonFunctions.GetSiteAddress() + "/" + country + "/" + row["stateprovince"].ToString() + "/default.aspx";
                temp = temp.ToLower();
                temp = temp.Replace(' ', '_');
            }
        }        
        /////// common for postback and ! postback ////////
        string cities1 = "";
        string citiesNew = "";
        foreach (DataRow dr in MainDataSet.Tables["Cities"].Rows)
        {
            string temp = CommonFunctions.GetSiteAddress() + "/" + country +
                  "/"+stateprovince+"/" + dr["City"].ToString() + "/default.aspx";
            temp = temp.ToLower();
            temp = temp.Replace(' ', '_');
            cities1 += "<a href='" + temp + "'><span class=\"tdNoSleeps\" style=\"font-weight:normal;font-style:normal\">" + dr["City"].ToString().Replace(" ", "&nbsp;") + "</span></a>, ";

           
        }

        string str_cities = "";
        int ind = 0;
        // string cls = " class='borderright' ";
        string cls = "border-right:1px solid #0094ff;";
        string li = "";

        foreach (DataRow dr in MainDataSet.Tables["Cities"].Rows)
        {
            string temp = "/" + country +
                  "/" + stateprovince + "/" + dr["City"].ToString() + "/default.aspx";
            temp = temp.ToLower();
            temp = temp.Replace(' ', '_');

            DataTable dt1 = new DataTable();
            dt1 = obj.PropertiesByCase(vList, Convert.ToInt32(dr["id"]), "City");

            li = " style='" + ((ind > 4) ? "border-top:0px;" : "") + (((ind++ % 5) == 4) ? cls : "") + "'";

            citiesNew += "<li"+li+">" +
                "<a href='" + temp + "' class='StateTitle'>" + dr["City"].ToString() + "</a> <br/>" +
                "<a href='" + temp + "'>" +
                "<img width='160' height='125' src='http://www.vacations-abroad.com/images/" + Convert.ToString(dt1.Rows[0]["PhotoImage"]) + "' alt='" + dr["City"].ToString() + " vacation rentals and boutique hotels' title='" + dr["City"].ToString() + " vacation rentals and boutique hotels' /></a>" +
            "</li>";
            str_cities += dr["City"].ToString() + ",";
        }

        ulManiGrid.InnerHtml = citiesNew;

        string tempcountry2 = CommonFunctions.GetSiteAddress() + "/" + country.ToLower().Replace(" ", "_") + "/"+stateprovince.ToLower().Replace(" ", "_") +
                                             "/default.aspx";
        
        string tempstate = CommonFunctions.GetSiteAddress() + "/" + country.ToLower().Replace(" ", "_") +
                                         "/" + stateprovince.ToLower().Replace(" ", "_") + "/default.aspx";
        string tempcountry3 = CommonFunctions.GetSiteAddress() + "/" + country.ToLower().Replace(" ", "_") +
                                                 "/default.aspx";
        string tempregion = CommonFunctions.GetSiteAddress() + "/" + region.ToLower().Replace(" ", "_") +
                                             "/default.aspx";
        Session["tempstate"] = stateprovince;
        Session["tempcountry"] = country;

        //FillCitiesColumn();
        HtmlMeta description = new HtmlMeta();

        description.Name = "description";
        description.Content = Description.Text.Replace("%country%", country).Replace("%stateprovince%", stateprovince).
            Replace("%cities%", cities);
        // Description OVER RIDE area

        string DescripReplacement = MainDataSet.Tables["Location"].Rows[0]["descriptionoverride"].ToString();
        if (DescripReplacement.Length > 0)
            description.Content = DescripReplacement;
        Page page1 = (Page)HttpContext.Current.Handler;
        description.Content = "Plan your next " + stateprovince + " vacation: where to stay and places to visit!";

        head.Controls.Add(description);

        HtmlMeta newdescription = new HtmlMeta();

        string str_meta = "%state% vacation rentals and boutique hotels in %cities% etc.";

        newdescription.Name = "description";
        newdescription.Content = str_meta.Replace("%state%", stateprovince ).Replace("%cities%", str_cities);

        head.Controls.Add(newdescription);



        HtmlMeta keywords = new HtmlMeta();

        keywords.Name = "keywords";
        keywords.Content = Keywords.Text.Replace("%country%", country).Replace("%stateprovince%", stateprovince).
            Replace("%cities%", cities);
        keywords.Content = page1.Title;
        head.Controls.Add(keywords);
       // ((System.Web.UI.WebControls.Image)Master.FindControl("Logo")).AlternateText = page1.Title;
        Page.Header.Controls.Add(new LiteralControl("<link href='http://vacations-abroad.com/css/StyleSheetBig4.css' rel='stylesheet' type='text/css'></script>"));

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
             if (vTemp.Replace(" ", "").Trim() == e.CommandArgument.ToString().Replace(" ", ""))
            {
               dtCategory.ImportRow(dr);
            }
        }
        

    }
    protected void dtlStates_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        Label lblCategory = e.Item.FindControl("lblCat") as Label;
        HyperLink hCategory = e.Item.FindControl("hCategory") as HyperLink;
        if (e.Item.ItemIndex == 0)
        {
            hCategory.NavigateUrl = CommonFunctions.PrepareURL(country + "/" + stateprovince + "/default.aspx");
        }
        else
        {
            hCategory.NavigateUrl = CommonFunctions.PrepareURL(country + "/" + stateprovince + "/" + lblCategory.Text.Replace(" ", "_").Replace("&", "and").ToLower() + "/default.aspx");
        }
    }
    protected void rptBound(object sender, EventArgs e)
    {
    }
    protected void btnSubmit2_Click(object sender, EventArgs e)
    {
        HttpResponse.RemoveOutputCacheItem("/StateProvinceList.aspx");
        string strCityText2 = txtCityText2.Text.Replace(Environment.NewLine, "<br />");
        DataTable dt = VADBCommander.CityTextByStateInd(stateprovinceid.ToString());
        try
        {
            if (dt.Rows.Count > 0)
            {
                VADBCommander.CityText2ByStateEdit(stateprovinceid.ToString(), strCityText2);
            }
            else
            {
                VADBCommander.CityText2ByStateAdd(stateprovinceid.ToString(), strCityText2);
            }
            lblInfo2.Text = "Data saved.";
            DataTable dt4 = VADBCommander.CityTextByStateInd(stateprovinceid.ToString());
            if (dt4.Rows.Count > 0)
            {
                if (dt4.Rows[0]["cityText"] != null)
                {
                    lblcityInfo.Text = dt4.Rows[0]["cityText"].ToString();
                    txtCityText.Text = dt4.Rows[0]["cityText"].ToString().Replace("<br />-ipx-", Environment.NewLine);

                }
                if (dt4.Rows[0]["cityText2"] != null)
                {
                    lblInfo2.Text = dt4.Rows[0]["cityText2"].ToString();
                    
                    if (string.IsNullOrEmpty(dt4.Rows[0]["cityText2"].ToString()) || dt4.Rows[0]["cityText2"].ToString() == "")
                    {
                        OrangeTitle.Visible = false;
                    }
                    txtCityText2.Text = dt4.Rows[0]["cityText2"].ToString().Replace("<br />", Environment.NewLine);
                }
                else
                {
                    OrangeTitle.Visible = false;
                }
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
       string strCityText = txtCityText.Text.Replace(Environment.NewLine, "<br />");
        DataTable dt = VADBCommander.CityTextByStateInd(stateprovinceid.ToString());
        try
        {
            if (dt.Rows.Count > 0)
            {
                VADBCommander.CityTextByStateEdit(stateprovinceid.ToString(), strCityText);

            }
            else
            {
                VADBCommander.CityTextByStateAdd(stateprovinceid.ToString(), strCityText);
            }
            lblInfo.Text = "Data saved.";
            DataTable dt4 = VADBCommander.CityTextByStateInd(stateprovinceid.ToString());
            if (dt4.Rows.Count > 0)
            {
                if (dt4.Rows[0]["cityText"] != null)
                {
                    lblcityInfo.Text = dt4.Rows[0]["cityText"].ToString();
                    txtCityText.Text = dt4.Rows[0]["cityText"].ToString().Replace("<br />-ipx-", Environment.NewLine);

                }
                if (dt4.Rows[0]["cityText2"] != null)
                {
                    lblInfo2.Text = dt4.Rows[0]["cityText2"].ToString();
                    if (string.IsNullOrEmpty(dt4.Rows[0]["cityText2"].ToString()) || dt4.Rows[0]["cityText2"].ToString() == "")
                    {
                        OrangeTitle.Visible = false;
                    }
                    txtCityText2.Text = dt4.Rows[0]["cityText2"].ToString().Replace("<br />", Environment.NewLine);
                }
                else
                {
                    OrangeTitle.Visible = false;
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
        DBConnection obj = new DBConnection();
        string query = "select city from cities where stateprovinceid=" + stateprovinceid;
        SqlDataReader reader;
        bool start = true;
        try
        {
            //find all cities within a state and format into right column
            //try to use cities variable in state page
            reader = obj.ExecuteRecordSetArtificial(query);

            while (reader.Read())
            {
                if (start == true)
                {
                    divCitiesRt.Text += "<li>" + reader["city"].ToString() +"</li>";
                    start = false;
                }
                else
                    divCitiesRt.Text += "<li>" + reader["city"].ToString() +"</li>";
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
            return Title.Text.Replace("%country%", country).Replace("%stateprovince%", stateprovince).
                Replace("%cities%", cities);
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
    
    protected void State_datagrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //State_datagrid.PageIndex = e.NewPageIndex;
        //State_datagrid.DataSource = (DataTable)Session["dt"];
        //State_datagrid.DataBind();
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
            //lblInfo.Text += j.ToString() + ",";
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
    protected void State_datagrid_PageIndexChanged(object sender, EventArgs e)
    {
        //Response.Status = "301 Moved Permanently";
        //string newURL = CommonFunctions.GetSiteAddress() + "/" + country + "/" + stateprovince +
        //    "/Page" + (State_datagrid.PageIndex + 1).ToString() + ".aspx";
        //newURL = newURL.Replace(" ", "_").ToLower();
        //Response.AddHeader("Location", newURL);
        //Response.End();
    }
    protected void State_datagrid_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {
        //State_datagrid.PageIndex = e.NewPageIndex;
        //Session["curPage"] = e.NewPageIndex;
        //State_datagrid.DataSource = (DataTable)Session["dt"];
        //State_datagrid.DataBind();
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
    protected void dtlStates_ItemDataBound1(object sender, RepeaterItemEventArgs e)
    {
        Label lblCategory = e.Item.FindControl("lblCat") as Label;
        HyperLink hCategory = e.Item.FindControl("hCategory") as HyperLink;
        if (e.Item.ItemIndex == 0)
        {
            hCategory.NavigateUrl = CommonFunctions.PrepareURL(country + "/" + stateprovince + "/default.aspx");
        }
        else
        {
            hCategory.NavigateUrl = CommonFunctions.PrepareURL(country + "/" + stateprovince + "/" + lblCategory.Text.Replace(" ", "_").Replace("&", "and").ToLower() + "/default.aspx");
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
