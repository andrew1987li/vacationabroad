//live
using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Linq;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class allPropertiesList : CommonPage
{

    protected Vacations.PropertiesDataset PropertiesSet;
    public string region;
    public string country;
    public string stateprovince;
    public string city;
    public string pageTitle;
    public string Property;
    public string cities;
    private const string STR_SELECTCitiesFROMCitiesWHERECitiesStateProvinceID = "SELECT Cities.*, Counties.CountyID FROM Cities  INNER JOIN StateProvinces ON StateProvinces.ID = Cities.StateProvinceID left outer join counties on cities.id=counties.cityid WHERE (StateProvinces.CountryID = @CountryID) AND EXISTS ( SELECT * FROM Properties WHERE (Properties.IfFinished = 1) AND (Properties.IfApproved = 1) AND (Properties.CityID = Cities.ID)  AND NOT EXISTS (SELECT * FROM Auctions WHERE PropertyID = Properties.ID)) ORDER BY City";

    private int regionid = -1;
    private int countryid = -1;
    private int stateprovinceid = -1;
    private int cityid = -1;

    protected SqlDataAdapter PropertiesAdapter;
    protected SqlDataAdapter Properties2Adapter;
    protected SqlDataAdapter AmenitiesAdapter;
    protected SqlDataAdapter LocationAdapter;
    protected SqlDataAdapter TypesAdapter;
    protected SqlDataAdapter CitiesAdapter;
    protected SqlDataAdapter StateProvincesAdapter;
    protected SqlDataAdapter LocationAdapterCountry;
    protected SqlDataAdapter PropertyTypesAdapter;
    protected SqlDataAdapter RegionCountriesAdapter;

    protected DataSet MainDataSet = new DataSet();
    protected DataSet SecondaryDataSet = new DataSet();

    private string propertytypes = "";

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
        Properties2Adapter = CommonFunctions.PrepareAdapter(CommonFunctions.GetConnection(), "Select * from Properties", SqlDbType.Int);

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

        if ((Request.Params["CountryID"] != null))
            try
            {
                countryid = Convert.ToInt32(Request.Params["CountryID"]);
            }
            catch (Exception)
            {
            }

        if (countryid == -1)
            Response.Redirect(CommonFunctions.PrepareURL("InternalError1.aspx"));
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
        Properties2Adapter.Fill(SecondaryDataSet, "Properties");
        AmenitiesAdapter.Fill(MainDataSet, "Amenities");
        StateProvincesAdapter.Fill(MainDataSet, "StateProvinces");
        PropertyTypesAdapter.Fill(MainDataSet, "PropertyTypes");
        RegionCountriesAdapter.Fill(MainDataSet, "CountriesRegion");
        DBConnection objTemp = new DBConnection();
        try
        {
            MainDataSet.Relations.Add("CitiesProperties", MainDataSet.Tables["Cities"].Columns["ID"],
                MainDataSet.Tables["Properties"].Columns["CityID"]);
            MainDataSet.Relations.Add("PropertiesAmenities", MainDataSet.Tables["Properties"].Columns["ID"],
                MainDataSet.Tables["Amenities"].Columns["PropertyID"]);
        }
        catch (Exception ex) {  }
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
        DataTable dtCategories = new DataTable();
        DBConnection obj = new DBConnection();

        try
        {
            dt = VADBCommander.CityTextInd(cityid.ToString());
        }
        catch (Exception ex) { lblInfo22.Text = ex.Message; }
        finally { obj.CloseConnection(); }

        //NO NEED TO CHECK IF EMPTY FOR BOTTOM, OK TO SHOW NOTHING


        if (!IsPostBack)
        {
            DataTable dt1 = new DataTable();
            DataFunctions obj1 = new DataFunctions();

            try
            {
                dt1 = obj1.PropertiesByCase(vList, countryid, "Country");
                DataView dv = dt1.DefaultView;
                dv.Sort = "minNightRate desc";
                dt1 = dv.ToTable();

                FillPropertiesBox(dt1, string.Empty);

                //create rdo items from categories table
                dtCategories = obj1.FindNumCategories(dt1);
                //int vCategoryCount = 0;


                DataView dvMax = dtCategories.DefaultView;
                dvMax.Sort = "count desc";
                DataTable dtMax = dvMax.ToTable();
                int vCategoryCount = 0;
                string firstCategory = "";
                string PropertyName = "";
                string subCategory = "";

                foreach (DataRow row in dtMax.Rows)
                {
                    int index = dtMax.Rows.IndexOf(row);
                    if (index == 0)
                    {
                        firstCategory = row["category"].ToString();
                        subCategory = dt1.Rows[0]["SubCategory"].ToString();
                    }
                    PropertyName = PropertyName + row["category"].ToString() + "s" + ", ";
                    string vTemp = row["category"].ToString() + " (" + row["count"].ToString() + ")";
                    vTemp = vTemp.Replace(" ", "&nbsp;");
                    //rdoTypes.Items.Add(vTemp + " ");
                    vCategoryCount = vCategoryCount + Convert.ToInt32(row["count"].ToString());
                }

                if (!IsPostBack)
                {
                    Session["t"] = dtCategories;
                }

                Page page = (Page)HttpContext.Current.Handler;

                if (dt1.Rows.Count <= 10)
                {
                    //Implement 404 logic less then 10 property with Prorerty in URL - Develop By Nimesh Sapovadiya
                    if (Request.QueryString["category"] != null)
                    {
                        Response.Clear();
                        Response.StatusCode = 404;
                        Response.End();
                    }
                    string dispString2 = "";
                    string dispString = "";

                    if (firstCategory.Contains("_"))
                    {
                        string[] strSplit = firstCategory.Split('_');
                        dispString2 = UppercaseFirst(strSplit[0]) + " " + UppercaseFirst(strSplit[1]) + "s";
                    }
                    else
                    {
                        dispString2 = UppercaseFirst(firstCategory) + "s";
                    }

                    Session["dtRecalc"] = dt1;
                    Session["dt"] = dt1;

                    //City_datagrid.DataSource = dt1;
                    //City_datagrid.DataBind();


                    ltrH11.Text = "Vacation Properties in " + country;


                    hyplnkCountryBackLink.NavigateUrl = "/" + country.ToLower().Replace(" ", "_") + "/default.aspx";
                    ltrCountryBackText.Text = country + "<<";

                    page.Title = "Vacation Properties in " + country;
                }
                else
                {
                    if (Request.QueryString["category"] != null)
                    {
                        firstCategory = Convert.ToString(Request.QueryString["category"]);

                        string dispString = "";
                        if (firstCategory.Contains("_"))
                        {
                            string[] strSplit = firstCategory.Split('_');
                            dispString = UppercaseFirst(strSplit[0]) + " " + UppercaseFirst(strSplit[1]) + "s";
                            foreach (DataRow dr in dt1.Rows)
                            {
                                if (dr["Category"].ToString().ToLower().Contains(strSplit[0]))
                                {
                                    subCategory = dr["SubCategory"].ToString();
                                    break;
                                }
                            }
                        }
                        else
                        {
                            dispString = UppercaseFirst(firstCategory) + "s";
                            foreach (DataRow dr in dt1.Rows)
                            {
                                if (dr["Category"].ToString().ToLower().Equals(firstCategory))
                                {
                                    subCategory = dr["SubCategory"].ToString();
                                    break;
                                }
                            }
                        }



                        ltrH11.Text = char.ToUpper(city[0]) + city.Substring(1) + " " + dispString;

                        hyplnkCountryBackLink.NavigateUrl = "/" + country.ToLower().Replace(" ", "_") + "/default.aspx";
                        ltrCountryBackText.Text = country + "<<";

                        page.Title = country + " " + dispString + " " + "and" + " " + char.ToUpper(city[0]) + city.Substring(1) + " " + "Property Rentals | Vacations Abroad";

                    }
                    else
                    {
                        string dispString = "";
                        string dispString2 = "";

                        if (firstCategory.Contains("_"))
                        {
                            string[] strSplit = firstCategory.Split('_');
                            dispString2 = UppercaseFirst(strSplit[0]) + " " + UppercaseFirst(strSplit[1]) + "s";
                        }
                        else
                        {
                            dispString2 = UppercaseFirst(firstCategory) + "s";
                        }

                        ltrH11.Text = "Vacation Properties in " + country;

                        hyplnkCountryBackLink.NavigateUrl = "/" + country.ToLower().Replace(" ", "_") + "/default.aspx";
                        ltrCountryBackText.Text = country + "<<";

                        page.Title = "Vacation Properties in " + country;

                    }
                    DataTable dtCategory = dt1.Clone();



                    foreach (DataRow dr in dt1.Rows)
                    {
                        string vTemp = dr["category"].ToString().Replace("&", "and").ToLower(); //+ " (" + dr["count"].ToString() + ")";
                                                                                                // vTemp = vTemp.Replace(" ", "&nbsp;");

                        if (vTemp.ToLower().Replace(" ", "").Trim() == firstCategory.ToLower().Replace("_", " ").Replace(" ", ""))
                        {
                            //subCategory = dr["SubCategory"].ToString();
                            dtCategory.ImportRow(dr);

                        }

                    }
                    Session["dtRecalc"] = dtCategory.DefaultView.Table;
                    Session["dt"] = dtCategory.DefaultView.Table;

                    DataView dv1 = dtCategory.DefaultView;
                    dv1.Sort = "MinNightRate asc";




                    //Session["dt"] = dv1.ToTable();
                    //City_datagrid.DataSource = dv1;
                    //City_datagrid.DataBind();
                }


            }
            catch (Exception ex) { lblInfo22.Text = ex.Message; }
        }

        //add cities to right column  
        DBConnection obj3 = new DBConnection();
        try
        {
            #region Cities within State
            SqlDataReader reader3 = obj3.ExecuteRecordSetArtificial("SELECT Cities.* FROM Cities WHERE (Cities.StateProvinceID = " + stateprovinceid + ") AND EXISTS ( SELECT * FROM Properties WHERE (Properties.IfFinished = 1) AND (Properties.IfApproved = 1) AND (Properties.CityID = Cities.ID)  AND NOT EXISTS (SELECT * FROM Auctions WHERE PropertyID = Properties.ID)) ORDER BY City");
            while (reader3.Read())
            {
                if (reader3["City"] is string)
                {
                    string temp = "/" + country + "/" + stateprovince + "/" + reader3["city"].ToString() + "/default.aspx";
                    temp = temp.ToLower();
                    temp = temp.Replace(' ', '_');

                }
            }
            reader3.Close();

            #endregion

            #region States within Country
            DataTable dtCountries = new DataTable();
            dtCountries = obj3.spStateProvByCountries(countryid);
            foreach (DataRow row in dtCountries.Rows)
            {
                if (row["stateprovince"] is string)
                {
                    string temp = CommonFunctions.GetSiteAddress() + "/" + country + "/" + row["stateprovince"].ToString() + "/default.aspx";
                    temp = temp.ToLower();
                    temp = temp.Replace(' ', '_');

                    // divCitiesRt.InnerHtml += "<a  href=\"" + temp + "\">" + row["stateprovince"].ToString().Replace(" ", "&nbsp;") + "</a>, ";
                }
            }
            //divCitiesRt.InnerHtml = divCitiesRt.InnerHtml.Remove(divCitiesRt.InnerHtml.Length - 2, 2);
            #endregion

        }
        catch (Exception ex) { lblInfo22.Text = ex.Message + "22"; }
        finally { obj3.CloseConnection(); }

        Session["state"] = stateprovince;
        Session["country"] = country;


//        Page.Header.Controls.Add(new LiteralControl("<link href='http://vacations-abroad.com/css/StyleSheetBig4.css' rel='stylesheet' type='text/css' />"));
        //Page.Header.Controls.Add(new LiteralControl("<script src='http://vacations-abroad.com/wz_tooltip.js' type='text/javascript'></script>"));

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

    protected void rdoBedrooms_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Session["dt"] != null)
        {
            DataTable dtCategories = new DataTable();
            DataFunctions obj = new DataFunctions();
            DataTable dt = (DataTable)Session["dt"];

            dtCategories = dt;
            string[] vTypeSelect = rdoBedrooms.SelectedItem.Text.Split('(');
            vTypeSelect[0] = vTypeSelect[0].Replace("&nbsp;", " ");

            List<string> vList = new List<string>();
            DataFunctions obj1 = new DataFunctions();
            var dt1 = obj1.PropertiesByCase(vList, countryid, "Country");
            DataView dv = dt1.DefaultView;
            dv.Sort = "minNightRate desc";
            dt1 = dv.ToTable();
            var categoryCondition = vTypeSelect[0].Trim();
            string typeFilterCondition = string.Empty;
            if (categoryCondition != "Display All")
            {
                typeFilterCondition = "Category = '" + vTypeSelect[0].Trim() + "'";
            }
            FillPropertiesBox(dt1, typeFilterCondition);

            rdoFilter.SelectedIndex = 3;
        }
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        List<string> vList = new List<string>();
        DataFunctions obj1 = new DataFunctions();
        var dt1 = obj1.PropertiesByCase(vList, countryid, "Country");
        DataView dv = dt1.DefaultView;
        dv.Sort = "minNightRate desc";
        dt1 = dv.ToTable();

        string[] vTypeSelect = rdoBedrooms.SelectedItem.Text.Split('(');
        vTypeSelect[0] = vTypeSelect[0].Replace("&nbsp;", " ");
        string filterCondition = vTypeSelect[0].Trim() == "Display All" ? string.Empty : "Category = '" + vTypeSelect[0].Trim() + "'";
        switch (rdoFilter.SelectedIndex)
        {
            case 0:
                {
                    if (!string.IsNullOrEmpty(filterCondition))
                    {
                        filterCondition += " AND ";
                    }
                    filterCondition += string.Format("NumSleeps >= 1 AND NumSleeps <= 4");
                }
                break;
            case 1:
                {
                    if (!string.IsNullOrEmpty(filterCondition))
                    {
                        filterCondition += " AND ";
                    }
                    filterCondition += string.Format("NumSleeps >= 5 AND NumSleeps <= 8");
                }
                break;
            case 2:
                {
                    if (!string.IsNullOrEmpty(filterCondition))
                    {
                        filterCondition += " AND ";
                    }
                    filterCondition += string.Format("NumSleeps >= 9");
                }
                break;
        }
        FillPropertiesBox(dt1, filterCondition);
    }

    protected void dtlStates_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        Label lblCategory = e.Item.FindControl("lblCat") as Label;
        HyperLink hCategory = e.Item.FindControl("hCategory") as HyperLink;
        if (e.Item.ItemIndex == 0)
        {
            hCategory.NavigateUrl = CommonFunctions.PrepareURL(country + "/" + stateprovince + "/" + city + "/default.aspx");
        }
        else
        {
            hCategory.NavigateUrl = CommonFunctions.PrepareURL(country + "/" + stateprovince + "/" + city + "/" + lblCategory.Text.Replace(" ", "_").Replace("&", "and").ToLower() + "/default.aspx");

        }
    }

    public string GetTitle()
    {
        string titlereplacement = MainDataSet.Tables["Location"].Rows[0]["titleoverride"].ToString();
        if (titlereplacement.Length > 0)
            Title.Text = "" + titlereplacement;
        else
            return Title.Text.Replace("%country%", country).Replace("%stateprovince%", stateprovince).Replace("%city%", city).Replace("%propertytypes%", propertytypes);

        return Title.Text;
    }

    public string TableTitle()
    {
        string temp = city + " " + stateprovince;

        return temp + " Vacation Rentals " + temp + " Holiday Rentals";

    }
    private DataRow GetSpecificRowFromTable(string tableName, string searchExpression)
    {
        return SecondaryDataSet.Tables[tableName].Select(searchExpression)[0];
    }

    private void FillPropertiesBox(DataTable dt1, string filterExpression)
    {
        DataRow[] rows = dt1.Select(filterExpression);
        string propertyHtmlText = "<table id='results'><tr>";
        bool wasTrClosed = true;
        List<string> propertyTypes = new List<String>();
        var metadataPropertyTypes = new List<string>();
        if (rows.Any())
        {
            foreach (DataRow row in rows)
            {
                if (Array.IndexOf(rows, row) > 1 && Array.IndexOf(rows, row)%4 == 0)
                {
                    propertyHtmlText += "<tr>";
                    wasTrClosed = false;
                }
                if (metadataPropertyTypes.Count < 3)
                {
                    if (!metadataPropertyTypes.Contains(row["Type"].ToString()))
                    {
                        metadataPropertyTypes.Add(row["Type"].ToString());
                    }
                }
                string propertyType = row["Category"].ToString();
                if (!propertyTypes.Contains(propertyType))
                {
                    propertyTypes.Add(propertyType);
                }
                propertyHtmlText += "<td><div class='clink' ><div style='text-align: center;'>" +
                                    "<a href='http://www.vacations-abroad.com" + CommonFunctions.PrepareURL(row["Country"].ToString().Replace(" ", "_").ToLower() +
                                                               "/" +
                                                               row["StateProvince"].ToString()
                                                                   .Replace(" ", "_")
                                                                   .ToLower() + "/" +
                                                               row["City"].ToString().Replace(" ", "_").ToLower() + "/default.aspx") + "'>" + row["City"].ToString() + "</a>" + ", " +
                                    "<a href='http://www.vacations-abroad.com" + CommonFunctions.PrepareURL(row["Country"].ToString().Replace(" ", "_").ToLower() +
                                                               "/" +
                                                               row["StateProvince"].ToString()
                                                                   .Replace(" ", "_")
                                                                   .ToLower() + "/default.aspx") + "'>" + row["StateProvince"].ToString() + "</a></div>" +
                                    "<a class=\"linkImgClickToView\" href='http://www.vacations-abroad.com" +
                                    CommonFunctions.PrepareURL(row["Country"].ToString().Replace(" ", "_").ToLower() +
                                                               "/" +
                                                               row["StateProvince"].ToString()
                                                                   .Replace(" ", "_")
                                                                   .ToLower() + "/" +
                                                               row["City"].ToString().Replace(" ", "_").ToLower() + "/" +
                                                               row["ID"].ToString().Replace(" ", "_").ToLower() +
                                                               "/default.aspx") + "'>" +
                                    "<img Width='170px' Height='140px' Style='border: 1px solid #ACA593; box - shadow: 1px 3px 3px 3px #E9E3D6; padding: 0px;margin-bottom:2px; ' src ='http://www.vacations-abroad.com/images/" +
                                    row["PhotoImage"].ToString() +
                                    "' runat='server' alt='no image' /></a><p style='display: none; margin - top: 7px; font - family: arial; font - size: 14px; color:#9B7F59; width: 147px; text - align:center; '></p>" +
                                    "<br/>" +
                                    "<span class='comments'>" + propertyType + "</span>" +
                                    "<span class='comments'>Sleeps " + row["NumSleeps"].ToString() + "</span>" 
                                     +
                                    "<span class='comments'>Rates " + row["minNightRate"].ToString() + "-" + row["HiNightRate"].ToString() + " " + GetSpecificRowFromTable("Properties", "ID = " + Convert.ToInt32(row["ID"]))[
                                        "minRateCurrency"] + "</span>" +
                                    "</div>" +
                                    "</td>";
                if (Array.IndexOf(rows, row) > 1 && Array.IndexOf(rows, row)%4 == 3)
                {
                    propertyHtmlText += "</tr>";
                    wasTrClosed = true;
                }
            }
            Description.Text += " " + String.Join(",", metadataPropertyTypes);
            if (!wasTrClosed)
            {
                propertyHtmlText += "</tr>";
            }
            propertyHtmlText += "</table><div id='pageNavPosition' style='padding: 10px;'></div>";
        }
        else
        {
            propertyHtmlText = "<div style='padding: 10px; text-align: center;'>No Propeties Found</div>";
        }
        allcountryproperties.InnerHtml = propertyHtmlText;

        if (rdoBedrooms.Items.Count == 0)
        {
            for (int i = 0; i < propertyTypes.Count; i++)
            {
                rdoBedrooms.Items.Add(
                    new ListItem(
                        propertyTypes[i] + " (" + dt1.Select("Category = '" + propertyTypes[i] + "'").Length + ")  ",
                        i.ToString()));
            }
            var selectedListItem = new ListItem("Display All (" + dt1.Select().Length + ")  ",
                propertyTypes.Count.ToString());
            if (rdoBedrooms.SelectedItem == null)
                selectedListItem.Selected = true;
            rdoBedrooms.Items.Add(selectedListItem);
        }

        UpdateNumSleeps(dt1);
    }

    private void UpdateNumSleeps(DataTable dt)
    {
        string[] vTypeSelect = rdoBedrooms.SelectedItem.Text.Split('(');
        vTypeSelect[0] = vTypeSelect[0].Replace("&nbsp;", " ");
        var categoryCondition = vTypeSelect[0].Trim();
        if (categoryCondition == "Display All")
        {
            categoryCondition = string.Empty;
        }
        else
        {
            categoryCondition = "Category = '" + categoryCondition + "'";
        }
        rdoFilter.Items[0].Text = "Sleeps 1-4 (" + dt.Select(categoryCondition + (!string.IsNullOrEmpty(categoryCondition) ? " AND " : string.Empty) + "NumSleeps >= 1 AND NumSleeps <= 4").Length + ") ";
        rdoFilter.Items[1].Text = "Sleeps 5-8 (" + dt.Select(categoryCondition + (!string.IsNullOrEmpty(categoryCondition) ? " AND " : string.Empty) + "NumSleeps >= 5 AND NumSleeps <= 8").Length + ") ";
        rdoFilter.Items[2].Text = "Sleeps 9+ (" + dt.Select(categoryCondition + (!string.IsNullOrEmpty(categoryCondition) ? " AND " : string.Empty) + "NumSleeps >= 9").Length + ") ";
        rdoFilter.Items[3].Text = "Display All (" + dt.Select(categoryCondition).Length + ") ";
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
    }
}
