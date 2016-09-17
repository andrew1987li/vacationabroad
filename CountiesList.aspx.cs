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
    //private const string STR_SELECTCitiesFROMCitiesWHERECitiesStateProvinceID = "SELECT Cities.* FROM Cities WHERE (Cities.StateProvinceID = @StateProvinceID) AND EXISTS ( SELECT * FROM Properties WHERE (Properties.IfFinished = 1) AND (Properties.IfApproved = 1) AND (Properties.CityID = Cities.ID)  AND NOT EXISTS (SELECT * FROM Auctions WHERE PropertyID = Properties.ID)) ORDER BY City";
   
    private const string STR_SELECTCitiesFROMCitiesWHERECitiesStateProvinceID = "select distinct cities.city from cities, counties where counties.cityid=cities.id and counties.county=@countyID " +
                "AND EXISTS ( SELECT * FROM Properties WHERE (Properties.IfFinished = 1) AND (Properties.IfApproved = 1) AND (Properties.CityID = Cities.ID)  AND NOT EXISTS (SELECT * FROM Auctions WHERE PropertyID = Properties.ID)) ORDER BY City";
    public string region;
    public string country;
    public string stateprovince;
    public string cities;

    private int regionid = -1;
    private int countryid = -1;
    public int countyID = -1;
    public string county = "";
    public int stateID = -1;

    //protected SqlConnection Connection = new SqlConnection (ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
    protected SqlDataAdapter CitiesAdapter;
    protected SqlDataAdapter PropertiesAdapter;
    protected SqlDataAdapter AmenitiesAdapter;
    protected SqlDataAdapter LocationAdapter;
    protected DataSet MainDataSet = new DataSet();

    protected void Page_Load(object sender, System.EventArgs e)
    {
        DBConnection obj = new DBConnection();
        DataTable dt = new DataTable();

        if (Request.QueryString["county"] != null)
            try
            {
                countyID = Convert.ToInt32(Request.QueryString["county"]);
            }
            catch (Exception)
            {
            }

        //GET REGION, COUNTRY, STATE
        try
        {
            string vCityID = "";
            string vCountryID = "";
            string vStateID = "";

            dt = VADBCommander.CountyStateCountryRegionInd(countyID.ToString());
            if (dt.Rows.Count > 0)
            {
                region = dt.Rows[0]["region"].ToString();
                country = dt.Rows[0]["country"].ToString();
                stateprovince = dt.Rows[0]["stateprovince"].ToString();
                county = dt.Rows[0]["countyName"].ToString();
                stateID = Convert.ToInt32(dt.Rows[0]["StateID"]);
                countryid = Convert.ToInt32(dt.Rows[0]["countryid"]);
            }
            
        }
        catch (Exception ex) { lblInfo.Text = ex.Message; }
        finally { obj.CloseConnection(); }

        //CommonFunctions.Connection.Open ();
        // StateCodeInfo.Text = SqlDbType.Int.
        CitiesAdapter = CommonFunctions.PrepareAdapter(CommonFunctions.GetConnection(), String.Format(STR_SELECTCitiesFROMCitiesWHERECitiesStateProvinceID), SqlDbType.Int);

        //const string STR_SELECTPropertiesInfo = "SELECT Properties.Name, Properties.NumBedrooms, Properties.NumBaths, Properties.NumSleeps, Properties.NumTVs, Properties.NumVCRs, Properties.CityID, Properties.NumCDPlayers, Properties.ID, CASE WHEN EXISTS (SELECT * FROM PropertiesAmenities INNER JOIN Amenities ON PropertiesAmenities.AmenityID = Amenities.ID WHERE (PropertiesAmenities.PropertyID = Properties.ID) AND (Amenities.Amenity = 'Beach Front')) THEN 'Beach Front' ELSE '' END AS BeachFront, CASE WHEN EXISTS (SELECT * FROM PropertiesAmenities INNER JOIN Amenities ON PropertiesAmenities.AmenityID = Amenities.ID WHERE (PropertiesAmenities.PropertyID = Properties.ID) AND (Amenities.Amenity = 'Seaside')) THEN 'Seaside' ELSE '' END AS Seaside, CASE WHEN EXISTS (SELECT * FROM PropertiesAmenities INNER JOIN Amenities ON PropertiesAmenities.AmenityID = Amenities.ID WHERE (PropertiesAmenities.PropertyID = Properties.ID) AND (Amenities.Amenity = 'Lake Front')) THEN 'Lake Front' ELSE '' END AS LakeFront, CASE WHEN EXISTS (SELECT * FROM PropertiesAmenities INNER JOIN Amenities ON PropertiesAmenities.AmenityID = Amenities.ID WHERE (PropertiesAmenities.PropertyID = Properties.ID) AND (Amenities.Amenity = 'River Front')) THEN 'River Front' ELSE '' END AS RiverFront, CASE WHEN EXISTS (SELECT * FROM PropertiesAmenities INNER JOIN Amenities ON PropertiesAmenities.AmenityID = Amenities.ID WHERE (PropertiesAmenities.PropertyID = Properties.ID) AND (Amenities.Amenity = 'Ski In Ski Out')) THEN 'Ski In Ski Out' ELSE '' END AS Ski, Cities.City, StateProvinces.StateProvince, Countries.Country, Regions.Region, MinimumNightlyRentalTypes.Name AS MinimumNightlyRental, PropertyTypes.Name AS Type FROM Properties INNER JOIN Cities ON Properties.CityID = Cities.ID INNER JOIN StateProvinces ON StateProvinces.ID = Cities.StateProvinceID INNER JOIN Countries ON StateProvinces.CountryID = Countries.ID INNER JOIN Regions ON Countries.RegionID = Regions.ID INNER JOIN Users ON Properties.UserID = Users.ID LEFT OUTER JOIN MinimumNightlyRentalTypes ON Properties.MinimumNightlyRentalID = MinimumNightlyRentalTypes.ID LEFT OUTER JOIN PropertyTypes ON Properties.TypeID = PropertyTypes.ID WHERE (Properties.IfFinished = 1) AND (Properties.IfApproved = 1) AND (Cities.StateProvinceID = @StateProvinceID) AND NOT EXISTS (SELECT * FROM Auctions WHERE PropertyID = Properties.ID) ORDER BY StateProvinces.StateProvince, Cities.City, Type, CASE WHEN EXISTS (SELECT * FROM Invoices WHERE (PropertyID = Properties.ID) AND (PaymentAmount >= InvoiceAmount) AND (GETDATE() <= Invoices.RenewalDate)) THEN 1 ELSE 0 END DESC, Properties.ID";
        string STR_SELECTPropertiesInfo = "SELECT Properties.Name, Properties.NumBedrooms, Properties.Name2, Properties.NumBaths, Properties.NumSleeps, Properties.NumTVs, Properties.NumVCRs, Properties.CityID," +
                    " Properties.NumCDPlayers, Properties.ID," +
                    " CASE WHEN EXISTS (SELECT * FROM PropertiesAmenities INNER JOIN Amenities" +
                    " ON PropertiesAmenities.AmenityID = Amenities.ID WHERE (PropertiesAmenities.PropertyID = Properties.ID)" +
                    " AND (Amenities.Amenity = 'Beach Front')) THEN 'Beach Front' ELSE '' END AS BeachFront," +
                    " CASE WHEN EXISTS (SELECT * FROM PropertiesAmenities INNER JOIN Amenities" +
                    " ON PropertiesAmenities.AmenityID = Amenities.ID WHERE (PropertiesAmenities.PropertyID = Properties.ID)" +
                    " AND (Amenities.Amenity = 'Seaside')) THEN 'Seaside' ELSE '' END AS Seaside," +
                    " CASE WHEN EXISTS (SELECT * FROM PropertiesAmenities INNER JOIN Amenities" +
                    " ON PropertiesAmenities.AmenityID = Amenities.ID WHERE (PropertiesAmenities.PropertyID = Properties.ID)" +
                    " AND (Amenities.Amenity = 'Lake Front')) THEN 'Lake Front' ELSE '' END AS LakeFront," +
                    " CASE WHEN EXISTS (SELECT * FROM PropertiesAmenities INNER JOIN Amenities" +
                    " ON PropertiesAmenities.AmenityID = Amenities.ID WHERE (PropertiesAmenities.PropertyID = Properties.ID)" +
                    " AND (Amenities.Amenity = 'River Front')) THEN 'River Front' ELSE '' END AS RiverFront," +
                    " CASE WHEN EXISTS (SELECT * FROM PropertiesAmenities INNER JOIN Amenities" +
                    " ON PropertiesAmenities.AmenityID = Amenities.ID WHERE (PropertiesAmenities.PropertyID = Properties.ID)" +
                    " AND (Amenities.Amenity = 'Ski In Ski Out')) THEN 'Ski In Ski Out' ELSE '' END AS Ski," +
                    " Cities.City, StateProvinces.StateProvince, Countries.Country, Regions.Region," +
                    " MinimumNightlyRentalTypes.Name AS MinimumNightlyRental, PropertyTypes.Name AS Type " +
                    "FROM Properties INNER JOIN Cities ON Properties.CityID = Cities.ID" +
                    " INNER JOIN StateProvinces ON StateProvinces.ID = Cities.StateProvinceID" +
                    " INNER JOIN Countries ON StateProvinces.CountryID = Countries.ID" +
                    " INNER JOIN Regions ON Countries.RegionID = Regions.ID" +
                    " INNER JOIN Users ON Properties.UserID = Users.ID" +
                    " LEFT OUTER JOIN MinimumNightlyRentalTypes ON Properties.MinimumNightlyRentalID = MinimumNightlyRentalTypes.ID" +
                    " LEFT OUTER JOIN PropertyTypes ON Properties.TypeID = PropertyTypes.ID, counties " +
                    "WHERE (Properties.IfFinished = 1) AND (Properties.IfApproved = 1) " +
                    "AND ( counties.county='" + countyID + "' ) " +
"and (cities.id=counties.cityID) " +
                    " AND NOT EXISTS (SELECT * FROM Auctions WHERE PropertyID = Properties.ID) " +                    
                    "ORDER BY StateProvinces.StateProvince, Cities.City, Properties.NumBedrooms, Properties.NumSleeps," +
                    " CASE WHEN EXISTS (SELECT * FROM Invoices WHERE (PropertyID = Properties.ID)" +
                    " AND (PaymentAmount >= InvoiceAmount) AND (GETDATE() <= Invoices.RenewalDate))" +
                    " THEN 1 ELSE 0 END DESC, Properties.ID";
        
        PropertiesAdapter = CommonFunctions.PrepareAdapter(CommonFunctions.GetConnection(), String.Format(STR_SELECTPropertiesInfo), SqlDbType.Int);

        //Dummyadapter = CommonFunctions.PrepareAdapter()
        AmenitiesAdapter = CommonFunctions.PrepareAdapter(CommonFunctions.GetConnection(), "SELECT Amenities.ID, Amenity," +
            " PropertiesAmenities.PropertyID " +
            "FROM Amenities INNER JOIN PropertiesAmenities ON Amenities.ID = PropertiesAmenities.AmenityID" +
            " INNER JOIN Properties ON PropertiesAmenities.PropertyID = Properties.ID " +
            " INNER JOIN Cities ON Properties.CityID = Cities.ID " +
            " INNER JOIN Counties ON Cities.ID = Counties.CityID " +
            "WHERE (Properties.IfFinished = 1) AND (Properties.IfApproved = 1) AND (Counties.county = @countyID)" +
            " AND NOT EXISTS (SELECT * FROM Auctions WHERE PropertyID = Properties.ID) AND (Amenities.Amenity NOT IN" +
            " ('Lake Front', 'Beach Front', 'River Front', 'Seaside', 'Ski In Ski Out', 'TV', 'VCR', 'CD Player'))",
            SqlDbType.Int);

        LocationAdapter = CommonFunctions.PrepareAdapter(CommonFunctions.GetConnection(), "SELECT StateProvinces.ID AS StateProvinceID," +
            " StateProvinces.StateProvince, Countries.ID AS CountryID, Countries.Country, Regions.ID AS RegionID," +
            " Regions.Region, stateprovinces.titleoverride, stateprovinces.descriptionoverride " +
            "FROM StateProvinces INNER JOIN Countries ON StateProvinces.CountryID = Countries.ID" +
            " INNER JOIN Regions ON Countries.RegionID = Regions.ID WHERE (counties.county = @countyID)",
            SqlDbType.Int);
                      
        HtmlHead head = Page.Header;

        HtmlMeta keywords = new HtmlMeta();
        HtmlMeta description = new HtmlMeta();

        DataBind();

        if (!IsPostBack)
        {
            //INDIVIDUAL CITY TEXT INSERT HERE*****
            //DBConnection obj = new DBConnection();
            //DataTable dt = new DataTable();
            string vText = county + " Region on Vacations-abroad.com is a " + county + " Regional directory  of " + county + " Regional rentals by owner and privately owned " +
                   county + " Regional vacation accommodation. Our short term " + county + " Regional rentals include luxury or budget " + county + " Regional vacation homes, " + county + " Regional vacation apartments and " + county + " Regional vacation condos that are perfect for family and group vacations in the " + county + " Region of " + country + " ";

            //TOP DEFAULT TEXT
            txtCityText.Text = vText;
            lblcityInfo.Text = vText;

            //BOTTOM, NO DEFAULT TEXT

            try
            {
                dt = VADBCommander.CityTextByCountyInd(countyID.ToString());
            }
            catch (Exception ex) { lblInfo.Text = ex.Message; }
            finally { obj.CloseConnection(); }

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["cityText"] != null)
                {
                    lblcityInfo.Text = dt.Rows[0]["cityText"].ToString();
                    txtCityText.Text = dt.Rows[0]["cityText"].ToString().Replace("<br />", Environment.NewLine);
                }
                if (dt.Rows[0]["cityText2"] != null)
                {
                    lblInfo2.Text = dt.Rows[0]["cityText2"].ToString();
                    txtCityText2.Text = dt.Rows[0]["cityText2"].ToString().Replace("<br />", Environment.NewLine);
                }
            }

            if (String.IsNullOrEmpty(lblcityInfo.Text))
            {
                //IF EMPTY VALUES OR 'DELETES' FROM ADMIN FOR TOP
                txtCityText.Text = vText;
                lblcityInfo.Text = vText;
            }
            //NO NEED TO CHECK IF EMPTY FOR BOTTOM, OK TO SHOW NOTHING

            List<string> vList = new List<string>();
            DataTable dt1 = new DataTable();
            DataTable dtCategories = new DataTable();
            try
            {
                DataFunctions objF = new DataFunctions();
                dt1 = objF.PropertiesByCase(vList, countyID, "Counties");
                Session["dt"] = dt1;
                State_datagrid.DataSource = dt1;
                State_datagrid.DataBind();

                //add num of properties w/amenities to chk texts
                int[] i = new int[4];
                i = FindNumAmenities(dt1);

                rdoFilter.Items[0].Text += " (" + i[0].ToString() + ") ";
                rdoFilter.Items[1].Text += " (" + i[1].ToString() + ") ";
                rdoFilter.Items[2].Text += " (" + i[2].ToString() + ") ";
                rdoFilter.Items[3].Text += " (" + i[3].ToString() + ") ";
                //rdoFilter.Items[4].Text += " in " + city;
                rdoFilter.Items[4].Text += " (" + dt1.Rows.Count.ToString() + ") ";

                //numbedrooms filter
                dtCategories = objF.FindNumBedrooms(dt1);
                rdoBedrooms.Items[0].Text += " (" + dtCategories.Rows[0]["count"].ToString() + ")  ";
                rdoBedrooms.Items[1].Text += " (" + dtCategories.Rows[1]["count"].ToString() + ")  ";
                rdoBedrooms.Items[2].Text += " (" + dtCategories.Rows[2]["count"].ToString() + ")  ";
                int vBedCount = 0;
                foreach (DataRow row in dtCategories.Rows)
                {
                    vBedCount += Convert.ToInt32(row["count"]);
                }
                rdoBedrooms.Items[3].Text += " (" + vBedCount.ToString() + ") ";
                rdoBedrooms.DataBind();


                //create rdo items from categories table
                dtCategories = objF.FindNumCategories(dt1);
                int vCategoryCount = 0;

                foreach (DataRow row in dtCategories.Rows)
                {
                    string vTemp = row["category"].ToString() + " (" + row["count"].ToString() + ")";
                    vTemp = vTemp.Replace(" ", "&nbsp;");
                    rdoTypes.Items.Add(vTemp + " ");
                    vCategoryCount = vCategoryCount + Convert.ToInt32(row["count"].ToString());
                }
                rdoTypes.Items.Add("Display&nbsp;All&nbsp;(" + vCategoryCount.ToString() + ") ");
                rdoTypes.SelectedIndex = rdoTypes.Items.Count - 1;
                rdoTypes.DataBind();

                //add cities to right column   
                dt = VADBCommander.CitiesInCountyList(countyID.ToString());
                foreach (DataRow datarow in dt.Rows)
                {
                    if (datarow["city"] is string)
                    {
                        string temp = CommonFunctions.GetSiteAddress() + "/" + country +
                               "/" + stateprovince + "/" + datarow["city"].ToString() + "/default.aspx";
                        temp = temp.ToLower();
                        temp = temp.Replace(' ', '_');

                        //divCitiesRt.InnerHtml += "<a href=\"" + temp + "\">" + datarow["city"].ToString().Replace(" ", "&nbsp;") + "&nbsp;Vacation&nbsp;Rentals</a>\n";
                        divCitiesRt.InnerHtml += "<a href=\"" + temp + "\">" + datarow["city"].ToString().Replace(" ", "&nbsp;") + "</a>,  ";
                    }
                }
                divCitiesRt.InnerHtml = divCitiesRt.InnerHtml.Remove(divCitiesRt.InnerHtml.Length-2, 2);
                //add cities to right column

                //add counties within state               
                dt = obj.spGetRightSideCounties(stateID);
                foreach (DataRow datarow in dt.Rows)
                {
                    if (datarow["county"] is string)
                    {
                        string temp = CommonFunctions.GetSiteAddress() + "/" + stateprovince + "/Holiday-Rentals/" +
                               datarow["county"].ToString() + "-Vacation_Rentals/default.aspx";
                        temp = temp.ToLower();
                        temp = temp.Replace(' ', '_');
                        divCountiesRt.InnerHtml += "<a  href=\"" + temp + "\">" + datarow["county"].ToString().Replace(" ", "&nbsp;").Replace("Province", "").Replace("of", "") + "</a>, ";
                        //divCountiesRt.InnerHtml += "<a href=\"" + temp + "\">" + datarow["county"].ToString().Replace(" ", "&nbsp;") + "&nbsp;Vacation&nbsp;Rentals</a>\n";
                      //  divCountiesRt.InnerHtml += "<a   class='rttextlow' href=\"" + temp + "\">" + datarow["county"].ToString().Replace(" ", "&nbsp;") + "&nbsp;Vacation&nbsp;Rentals</a>  ";
                    }
                }
                divCountiesRt.InnerHtml = divCountiesRt.InnerHtml.Remove(divCountiesRt.InnerHtml.Length-2, 2);
                //add counties within state   
              
                dt = obj.spStateProvByCountries(countryid);
                foreach (DataRow row in dt.Rows)
                {
                    if (row["stateprovince"] is string)
                    {
                        string temp = CommonFunctions.GetSiteAddress() + "/" + country + "/" + row["stateprovince"].ToString() + "/default.aspx";
                        temp = temp.ToLower();
                        temp = temp.Replace(' ', '_');

                        divStates.InnerHtml += "<a  href=\"" + temp + "\">" + row["stateprovince"].ToString().Replace(" ", "&nbsp;") + "</a>, ";
                    }
                }
                divStates.InnerHtml = divStates.InnerHtml.Remove(divStates.InnerHtml.Length - 2, 2);
            }
            catch (Exception ex) { lblInfo22.Text = ex.Message; }
        }
        //FillCitiesColumn();
        ((System.Web.UI.WebControls.Image)Master.FindControl("Logo")).AlternateText = "Vacation Rentals at Vacations-Abroad.com";
        Page.Header.Controls.Add(new LiteralControl("<link href='/css/StyleSheetBig4.css' rel='stylesheet' type='text/css' />"));
        
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

        //string titlereplacement = MainDataSet.Tables["Location"].Rows[0]["titleoverride"].ToString();
        //if (titlereplacement.Length > 0)
        //    Title.Text = "" + titlereplacement;
        //else
            return Title.Text.Replace("%country%", country).Replace("%stateprovince%", stateprovince).
                Replace("%county%", county);
        //return Title.Text;
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
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        DataTable dtCategories = new DataTable();
        DataFunctions obj = new DataFunctions();
        List<string> vList = new List<string>();
        try
        {
            //1.get main table  2.filter out by type  3.filter out by bedrooms  4.filter out by amenities        
            //1.
            //dt = obj.PropertiesByCase(vList, cityid, "City");
            if (Session["dt"] != null)
                dt = (DataTable)Session["dt"];

            //2.
            string[] vTypeSelect;
            vTypeSelect = rdoTypes.SelectedItem.Text.Split('(');
            vTypeSelect[0] = vTypeSelect[0].Replace("&nbsp;", " ");

            if (vTypeSelect[0].Trim() != "Display All")
            {
                dt.DefaultView.RowFilter = "Category = '" + vTypeSelect[0].Trim() + "'";
            }
            
            //3.
            if (rdoBedrooms.SelectedIndex != 3)
            {
                DataTable dtBedrooms = dt.DefaultView.ToTable();
                if (rdoBedrooms.SelectedIndex == 0)
                    dtBedrooms.DefaultView.RowFilter = "NumBedrooms < 3";
                else if (rdoBedrooms.SelectedIndex == 1)
                    dtBedrooms.DefaultView.RowFilter = "NumBedrooms > 2 AND NumBedrooms < 5";
                else if (rdoBedrooms.SelectedIndex == 2)
                    dtBedrooms.DefaultView.RowFilter = "NumBedrooms > 4";
                dt = dtBedrooms.DefaultView.ToTable();                
            }
            
            //4.
            if (rdoFilter.SelectedIndex != 4)
            {
                DataTable dtAmenities = dt.DefaultView.ToTable();
                if (rdoFilter.SelectedIndex == 0)
                {
                    dtAmenities.DefaultView.RowFilter = "HotTub LIKE 'Hot Tub%'";
                }
                else if (rdoFilter.SelectedIndex == 1)
                {
                    dtAmenities.DefaultView.RowFilter = "InternetAccess LIKE 'Internet Access%'";
                }
                else if (rdoFilter.SelectedIndex == 2)
                {
                    dtAmenities.DefaultView.RowFilter = "Petfriendly LIKE 'Pet%'";
                }
                else if (rdoFilter.SelectedIndex == 3)
                {
                    dtAmenities.DefaultView.RowFilter = "PrivPool LIKE 'Private%' OR SharedPool LIKE 'Shared%'";
                }
                dt = dtAmenities.DefaultView.ToTable();
            }

            State_datagrid.DataSource = dt.DefaultView.ToTable();
            State_datagrid.DataBind();

        }
        catch (Exception ex) { lblInfo.Text = ex.Message; }
    }
    protected void State_datagrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        DataTable dt = new DataTable();
        DBConnection obj = new DBConnection();
        if (e.Row.RowIndex != -1)
        {
            try
            {
                int Vid = Convert.ToInt32(State_datagrid.DataKeys[e.Row.RowIndex][0]);

                dt = VADBCommander.PropertyAvailDatesList(Vid.ToString(), DateTime.Today.ToString());
                HtmlGenericControl div = (HtmlGenericControl)e.Row.FindControl("divCalendar");

                if (dt.Rows.Count > 0)
                {
                    div.Visible = true;
                }
                else
                {
                    div.Visible = false;
                }
                //show reviews button..select * where propertyID match
                HtmlGenericControl divReview = (HtmlGenericControl)e.Row.FindControl("divWrite");
                dt = obj.spSelectCommentExist(Vid);
                if (dt.Rows.Count > 0)
                    divReview.Visible = true;
                else
                    divReview.Visible = false;


                //show amenities
                Label lblAmenities = (Label)e.Row.FindControl("lblAmenities");

                dt = VADBCommander.AmenitiesByProperty(Vid.ToString());

                List<string> vList = new List<string>();
                List<string> vList2 = new List<string>();
                vList2.Add("Lake Front");
                vList2.Add("River Front");
                vList2.Add("Pet Friendly");
                vList2.Add("Private Swimming Pool");
                vList2.Add("Shared Swimming Pool");
                vList2.Add("BBQ Grill");
                vList2.Add("Internet Access");
                vList2.Add("Children Not Allowed");
                vList2.Add("Hot Tub");
                vList2.Add("Wood Fireplace");
                vList2.Add("Gas Fireplace");
                vList2.Add("Shared Jacuzzi");
                vList2.Add("Jacuzzi - Outside");
                vList2.Add("Ski In Ski Out");
                vList2.Add("Seaside");
                vList2.Add("Beach Front");

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    if (vList2.Contains(dt.Rows[x]["Amenity"].ToString()))
                    {
                        string amn = dt.Rows[x]["Amenity"].ToString().Trim();
                        if (amn == "Seaside" || amn == "Lake Front" || amn == "River Front" || amn == "Beach Front" || amn == "Ski In Ski Out" || amn == "Pet Friendly" || amn == "Private Swimming Pool" || amn == "Wood Fireplace" || amn == "Internet Access")
                        {
                            if (lblAmenities.Text == "")
                                lblAmenities.Text +=  dt.Rows[x]["Amenity"].ToString();
                            else
                                lblAmenities.Text += ", " + dt.Rows[x]["Amenity"].ToString();
                        }
                    }
                }
                HtmlGenericControl li = (HtmlGenericControl)e.Row.FindControl("liAmenity");
                if (lblAmenities.Text == "")
                    li.Visible = false;

                //long descriptions
                Label lblDesc = (Label)e.Row.FindControl("lblDesc");
                if (lblDesc != null)
                {
                    if (lblDesc.Text.Length > 320)
                    {
                        //int x = 130;
                        //while (lblDesc.Text.Substring(x, 1) != " ")
                        //{
                        //    x++;
                        //}
                        //lblDesc.Text = lblDesc.Text.Remove(x);
                        //lblDesc.Text += "...<a href=\"";
                        //string temp = CommonFunctions.GetSiteAddress() + "/" + country +
                        //    "/" + stateprovince + "/" + city + "/" + Vid + "/default.aspx\">more</a>";
                        //temp = temp.ToLower();
                        //temp = temp.Replace(' ', '_');
                        //lblDesc.Text += temp;
                        lblDesc.Text = lblDesc.Text.Remove(157);
                        lblDesc.Text += "...";
                    }
                }

                dt = VADBCommander.PropertyInd(Vid.ToString());
                Label lblPic = (Label)e.Row.FindControl("lblPicName");
                if ((lblPic != null) && (dt.Rows.Count > 0))
                {
                    lblPic.Text = dt.Rows[0]["name2"].ToString();
                    //if (lblPic.Text.Length > 30)
                    //    lblPic.Text = lblPic.Text.Remove(30);
                }


                //Label lblPN = (Label)e.Row.FindControl("lblPNRates");
                //if (lblPN != null)
                //{
                //    if (dt.Rows[0]["minNightRate"].ToString() != "")
                //    {
                //        lblPN.Text = dt.Rows[0]["minNightRate"].ToString() + "-" + dt.Rows[0]["HiNightRate"].ToString() + " " +
                //            dt.Rows[0]["minRateCurrency"].ToString() + " Per Night";
                //    }
                //}

                Label lblPN = (Label)e.Row.FindControl("lblPNRates");
                Label lblPNCurrency = (Label)e.Row.FindControl("lblPNRatesCurrency");
                Label lblPNBasis = (Label)e.Row.FindControl("lblPNRatesBasis");
                //Label lblPNMinimumNights = (Label)e.Row.FindControl("lblMinimumNights");
                if (lblPN != null)
                {
                    if (dt.Rows[0]["minNightRate"].ToString() != "")
                    {
                        lblPN.Text = dt.Rows[0]["minNightRate"].ToString() + "-" + dt.Rows[0]["HiNightRate"].ToString();
                        lblPNCurrency.Text = dt.Rows[0]["minRateCurrency"].ToString();
                        lblPNBasis.Text = " Per Night";

                        //lblPNMinimumNights.Text = "";
                        //lblPNMinimumNights.Text = " Night Minimum";
                    }
                    //if (dt.Rows[0]["minNightRate"].ToString() != "")
                    //{
                    //    lblPN.Text = dt.Rows[0]["minNightRate"].ToString() + "-" + dt.Rows[0]["HiNightRate"].ToString() + " " +
                    //        dt.Rows[0]["minRateCurrency"].ToString() + " Per Night";
                    //}
                }

            }
            catch (Exception ex) { lblInfo22.Text = ex.Message; }
            finally { obj.CloseConnection(); }
        }
    }
    protected void State_datagrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        State_datagrid.PageIndex = e.NewPageIndex;
        State_datagrid.DataSource = (DataTable)Session["dt"];
        State_datagrid.DataBind();
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
        Response.Status = "301 Moved Permanently";
        string newURL = CommonFunctions.GetSiteAddress() + "/" + country + "/" + stateprovince +
            "/Page" + (State_datagrid.PageIndex + 1).ToString() + ".aspx";
        newURL = newURL.Replace(" ", "_").ToLower();
        Response.AddHeader("Location", newURL);
        Response.End();
    }
    protected void State_datagrid_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {
        State_datagrid.PageIndex = e.NewPageIndex;
        Session["curPage"] = e.NewPageIndex;
        State_datagrid.DataSource = (DataTable)Session["dt"];
        State_datagrid.DataBind();
    }

    protected void rdoTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
        //recalculate #totals for other rdo's
        if (Session["dt"] != null)
        {
            DataTable dtCategories = new DataTable();
            DataFunctions obj = new DataFunctions();
            DataTable dt = (DataTable)Session["dt"];

            State_datagrid.DataSource = dt;
            State_datagrid.DataBind();

            dtCategories = dt;
            string[] vTypeSelect;
            vTypeSelect = rdoTypes.SelectedItem.Text.Split('(');
            vTypeSelect[0] = vTypeSelect[0].Replace("&nbsp;", " ");

            if (vTypeSelect[0].Trim() != "Display All")
            {
                dtCategories.DefaultView.RowFilter = "Category = '" + vTypeSelect[0].Trim() + "'";
            }

            //
            Session["dtRecalc"] = dtCategories.DefaultView.ToTable();
            //numbedrooms rdo
            dtCategories = obj.FindNumBedrooms(dtCategories.DefaultView.ToTable());


            rdoBedrooms.Items[0].Text = "0-2 Bedrooms (" + dtCategories.Rows[0]["count"].ToString() + ")  ";
            rdoBedrooms.Items[1].Text = "3-4 Bedrooms (" + dtCategories.Rows[1]["count"].ToString() + ")  ";
            rdoBedrooms.Items[2].Text = "5+ Bedrooms (" + dtCategories.Rows[2]["count"].ToString() + ")  ";
            int vBedCount = 0;
            foreach (DataRow row in dtCategories.Rows)
            {
                vBedCount += Convert.ToInt32(row["count"]);
            }
            rdoBedrooms.Items[3].Text = "Display All (" + vBedCount.ToString() + ") ";
            //rdoBedrooms.DataBind();            

            //amenities rdo
            int[] i = new int[4];
            i = FindNumAmenities(dt.DefaultView.ToTable());

            rdoFilter.Items[0].Text = "Hot Tub (" + i[0].ToString() + ") ";
            rdoFilter.Items[1].Text = "Internet (" + i[1].ToString() + ") ";
            rdoFilter.Items[2].Text = "Pets (" + i[2].ToString() + ") ";
            rdoFilter.Items[3].Text = "Pool (" + i[3].ToString() + ") ";
            rdoFilter.Items[4].Text = "Display All (" + dt.DefaultView.ToTable().Rows.Count.ToString() + ") ";
            //rdoFilter.DataBind();           
        }
        rdoBedrooms.SelectedIndex = 3;

        rdoFilter.SelectedIndex = 4;

    }
    protected void rdoBedrooms_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtBedrooms = new DataTable();
        DataTable dt = new DataTable();
        //if (rdoBedrooms.SelectedIndex != 3)
        //{
        //recalculate #totals for other rdo's

        if (Session["dtRecalc"] != null)  //top filter selected changed
        {

            if (Session["dt"] != null)
            {
                State_datagrid.DataSource = (DataTable)Session["dt"];
                State_datagrid.DataBind();
            }

            dt = (DataTable)Session["dtRecalc"];
            //sort out bedrooms selected
            if (rdoBedrooms.SelectedIndex != 3)
            {
                dtBedrooms = dt.DefaultView.ToTable();

                if (rdoBedrooms.SelectedIndex == 0)
                    dtBedrooms.DefaultView.RowFilter = "NumBedrooms < 3";
                else if (rdoBedrooms.SelectedIndex == 1)
                    dtBedrooms.DefaultView.RowFilter = "NumBedrooms > 2 AND NumBedrooms < 5";
                else if (rdoBedrooms.SelectedIndex == 2)
                    dtBedrooms.DefaultView.RowFilter = "NumBedrooms > 4";
            }
            else
                dtBedrooms = dt.DefaultView.ToTable();

            //amenities rdo
            int[] i = new int[4];

            i = FindNumAmenities(dtBedrooms.DefaultView.ToTable());

            rdoFilter.Items[0].Text = "Hot Tub (" + i[0].ToString() + ") ";
            rdoFilter.Items[1].Text = "Internet (" + i[1].ToString() + ") ";
            rdoFilter.Items[2].Text = "Pets (" + i[2].ToString() + ") ";
            rdoFilter.Items[3].Text = "Pool (" + i[3].ToString() + ") ";
            rdoFilter.Items[4].Text = "Display All (" + dtBedrooms.DefaultView.ToTable().Rows.Count.ToString() + ") ";

        }
        else
        {
            State_datagrid.DataSource = (DataTable)Session["dt"];
            State_datagrid.DataBind();

            dt = (DataTable)Session["dt"];

            if (rdoBedrooms.SelectedIndex != 3)
            {
                dtBedrooms = dt.DefaultView.ToTable();

                if (rdoBedrooms.SelectedIndex == 0)
                    dtBedrooms.DefaultView.RowFilter = "NumBedrooms < 3";
                else if (rdoBedrooms.SelectedIndex == 1)
                    dtBedrooms.DefaultView.RowFilter = "NumBedrooms > 2 AND NumBedrooms < 5";
                else if (rdoBedrooms.SelectedIndex == 2)
                    dtBedrooms.DefaultView.RowFilter = "NumBedrooms > 4";
            }
            else
                dtBedrooms = dt.DefaultView.ToTable();

            //amenities rdo
            int[] i = new int[4];

            i = FindNumAmenities(dtBedrooms.DefaultView.ToTable());

            rdoFilter.Items[0].Text = "Hot Tub (" + i[0].ToString() + ") ";
            rdoFilter.Items[1].Text = "Internet (" + i[1].ToString() + ") ";
            rdoFilter.Items[2].Text = "Pets (" + i[2].ToString() + ") ";
            rdoFilter.Items[3].Text = "Pool (" + i[3].ToString() + ") ";
            rdoFilter.Items[4].Text = "Display All (" + dtBedrooms.DefaultView.ToTable().Rows.Count.ToString() + ") ";

        }
        rdoFilter.SelectedIndex = 4;
        //rdoFilter.DataBind();
    }
    protected void rdoTypes_DataBound(object sender, EventArgs e)
    {
        //for (int i = 0; i < rdoTypes.Items.Count; i++)
        //{
        //    rdoTypes.Items[i].Text = rdoTypes.Items[i].Text.Replace(" ", "_");
        //}
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string strCityText = txtCityText.Text.Replace(Environment.NewLine, "<br />");
        DataTable dt = VADBCommander.CityTextByCountyInd(countyID.ToString());
        try
        {
            if (dt.Rows.Count > 0)
            {
                VADBCommander.CityTextByCountyEdit(countyID.ToString(), strCityText);
            }
            else
            {
                VADBCommander.CityTextByCountyAdd(countyID.ToString(), strCityText);
            }
            lblInfo.Text = "Data saved.";
        }
        catch (Exception ex)
        {
            lblInfo.Text = ex.Message;
        }
    }
    protected void btnSubmit2_Click(object sender, EventArgs e)
    {
        string strCityText2 = txtCityText2.Text.Replace(Environment.NewLine, "<br />");
        
        DBConnection obj = new DBConnection();
        DataTable dt = VADBCommander.CityTextByCountyInd(countyID.ToString());
        
        try
        {
            if (dt.Rows.Count > 0)
            {
                VADBCommander.CityText2ByCountyEdit(countyID.ToString(), strCityText2);
            }
            else
            {
                VADBCommander.CityText2ByCountyAdd(countyID.ToString(), strCityText2);
                
            }
            lblInfo2.Text = "Data saved.";

        }
        catch (Exception ex)
        {
            lblInfo2.Text = ex.Message;
        }
        lblInfo2.ForeColor = System.Drawing.Color.Red;
    }
}
