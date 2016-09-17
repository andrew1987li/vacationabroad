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

    private const string STR_SELECTCitiesFROMCitiesWHERECitiesStateProvinceID = "SELECT Cities.* FROM Cities WHERE (Cities.StateProvinceID = @StateProvinceID) AND EXISTS ( SELECT * FROM Properties WHERE (Properties.IfFinished = 1) AND (Properties.IfApproved = 1) AND (Properties.CityID = Cities.ID)  AND NOT EXISTS (SELECT * FROM Auctions WHERE PropertyID = Properties.ID)) ORDER BY City";

    public string region;
    public string country;
    public string stateprovince;
    public string cities;

    private int regionid = -1;
    private int countryid = -1;
    public int cityID = -1;
    public string city = "";
    public int stateID = -1;
    public int tourID = -1;
    public string tourType = "";
    public string tourLength = "";
    public string tourTime = "";

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

        if (Request.QueryString["cityID"] != null)
            try
            {
                cityID = Convert.ToInt32(Request.QueryString["cityID"]);
            }
            catch (Exception)
            {
            }

        //GET REGION, COUNTRY, STATE
        try
        {


            dt = VADBCommander.CityTourList(cityID.ToString());
            if (dt.Rows.Count > 0)
            {
                region = dt.Rows[0]["region"].ToString();
                country = dt.Rows[0]["country"].ToString();
                stateprovince = dt.Rows[0]["stateprovince"].ToString();
                city = dt.Rows[0]["city"].ToString();
                stateID = Convert.ToInt32(dt.Rows[0]["StateID"]);
                countryid = Convert.ToInt32(dt.Rows[0]["countryid"]);
            }            
        }
        catch (Exception ex) { lblInfo22.Text = ex.ToString(); }
        finally { obj.CloseConnection(); }
       
        CitiesAdapter = CommonFunctions.PrepareAdapter(CommonFunctions.GetConnection(), String.Format(STR_SELECTCitiesFROMCitiesWHERECitiesStateProvinceID), SqlDbType.Int);

        string STR_SELECTPropertiesInfo = "select Cities.City, Tours.*, StateProvinces.StateProvince, Countries.Country, Countries.id as countryid,  "+
"Regions.Region, StateProvinces.id as StateID from Cities inner join  "+
"StateProvinces on Cities.stateprovinceid=StateProvinces.id  "+
"inner join Countries on Countries.id=StateProvinces.Countryid  "+
"inner join Regions on Regions.id=Countries.Regionid "+
"inner join Tours on Tours.CityID=Cities.ID " +
"where cities.id=@CityID";
        
        PropertiesAdapter = CommonFunctions.PrepareAdapter(CommonFunctions.GetConnection(), String.Format(STR_SELECTPropertiesInfo), SqlDbType.Int);

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

        LocationAdapter = CommonFunctions.PrepareAdapter(CommonFunctions.GetConnection(), "select Cities.City, Cities.ID as CityID, StateProvinces.StateProvince, Countries.Country, Countries.id as countryid, " +
"Regions.Region, StateProvinces.id as StateID from Cities inner join " +
"StateProvinces on Cities.stateprovinceid=StateProvinces.id " +
"inner join Countries on Countries.id=StateProvinces.Countryid " +
"inner join Regions on Regions.id=Countries.Regionid " +
"where cities.id=@CityID",
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
            string vText = "Vacations-abroad.com is a directory of " + city + " vacation rentals and privately owned " +
                   city + " holiday accommodation. Our holiday rentals include vacation homes, holiday villas, vacation condos, holiday " +
                           "apartments, holiday cottages, vacation cabins, B&Bs, Hotels, Resorts, Guesthouses in " +
                           city + " " + stateprovince;
           
            List<string> vList = new List<string>();
            DataTable dt1 = new DataTable();
            DataTable dtCategories = new DataTable();
            DBConnection objR = new DBConnection();
            try
            {
                DataFunctions objF = new DataFunctions();
                dt1 = VADBCommander.ListApprovedToursByCity(cityID.ToString());
                Session["dt"] = dt1;
                State_datagrid.DataSource = dt1;
                State_datagrid.DataBind();
                             

                //add cities to right column  
               
                SqlDataReader reader;
                DataTable dtR = new DataTable();
                
                string vCountyID = "";

               
                    //if county assoc
                    dtR = VADBCommander.CountyListByCityID(cityID.ToString());

                    if (dtR.Rows.Count > 0)
                    {
                        vCountyID = dtR.Rows[0]["countyid"].ToString();

                        rtCounties.InnerHtml = dtR.Rows[0]["county"].ToString() + " Cities";
                        rtLowerHd.InnerHtml = stateprovince + " Counties";


                        dtR = VADBCommander.CityListByCountyID(vCountyID);
                        foreach (DataRow row in dtR.Rows)
                        {
                            string temp = CommonFunctions.GetSiteAddress() + "/" + country +
                                  "/" + stateprovince + "/" + row["city"].ToString() + "/default.aspx";
                            temp = temp.ToLower();
                            temp = temp.Replace(' ', '_');

                            divCitiesRt.InnerHtml += "<a href=\"" + temp + "\">" + row["city"].ToString().Replace(" ", "&nbsp;") + "</a>, ";
                        }
                        divCitiesRt.InnerHtml = divCitiesRt.InnerHtml.Remove(divCitiesRt.InnerHtml.Length - 2, 2);

                        dtR = objR.spGetRightSideCounties(stateID);

                        if (dtR.Rows.Count > 0)
                        {

                            DataTable dtTooltip = VADBCommander.CityAndCountiesByStateID(stateID.ToString());

                            foreach (DataRow row in dtR.Rows)
                            {
                                string temp = CommonFunctions.GetSiteAddress() + "/" +
                                      stateprovince + "/Holiday-Rentals/" + row["county"].ToString() + "-Vacation_Rentals/default.aspx";
                                temp = temp.ToLower();
                                temp = temp.Replace(' ', '_');
                                //county tooltip
                                dtTooltip.DefaultView.RowFilter = "CountyName='" + row["county"].ToString() + "'";
                                if (dtTooltip.DefaultView.ToTable().Rows.Count > 0)
                                {
                                    rtLower.InnerHtml += "<a onmouseover=\"Tip('";
                                    foreach (DataRow rowCnty in dtTooltip.DefaultView.ToTable().Rows)
                                    {
                                        rtLower.InnerHtml += "<a href=\\'" + CommonFunctions.GetSiteAddress().ToLower() +
                                            "/" + country.ToLower().Replace(" ", "_") + "/" +
                                            stateprovince.ToLower().Replace(" ", "_") + "/" + rowCnty["city"].ToString().ToLower().Replace(" ", "_") + "/default.aspx\\' target=\\'_self\\'>" +
                                            rowCnty["city"].ToString() + "</a>, ";
                                    }
                                    rtLower.InnerHtml = rtLower.InnerHtml.Remove(rtLower.InnerHtml.Length - 2, 2);
                                    rtLower.InnerHtml += "', WIDTH, 150, SHADOW, false, OPACITY, 90, BGCOLOR, '#ede9ed', BORDERCOLOR, '#ede9ed', FONTCOLOR, '#474747', CLICKSTICKY, true, CLICKCLOSE, true, FONTSIZE, '12px', FONTFACE, 'Arial', CLOSEBTN, false, STICKY, true, OFFSETX, 10, PADDING, 5, OFFSETY, 0)\"";

                                    rtLower.InnerHtml += " <a href=\"" + temp + "\">" + row["county"].ToString().Replace(" ", "&nbsp;") + "</a>, ";
                                    //county tooltip
                                }
                            }
                            rtLower.InnerHtml = rtLower.InnerHtml.Remove(rtLower.InnerHtml.Length - 2, 2);

                        }
                    }
                    else
                    {
                        //if county assoc

                        rtCounties.InnerHtml = stateprovince + " Cities";
                        rtLowerHd.InnerHtml = country + " States";
                        
                        
                        reader = objR.ExecuteRecordSetArtificial("SELECT Cities.* FROM Cities WHERE (Cities.StateProvinceID = " + stateID + ") AND EXISTS ( SELECT * FROM Properties WHERE (Properties.IfFinished = 1) AND (Properties.IfApproved = 1) AND (Properties.CityID = Cities.ID)  AND NOT EXISTS (SELECT * FROM Auctions WHERE PropertyID = Properties.ID)) ORDER BY City");
                        while (reader.Read())
                        {
                            if (reader["City"] is string)
                            {
                                string temp = CommonFunctions.GetSiteAddress() + "/" + country +
                                       "/" + stateprovince + "/" + reader["city"].ToString() + "/default.aspx";
                                temp = temp.ToLower();
                                temp = temp.Replace(' ', '_');

                                divCitiesRt.InnerHtml += "<a href=\"" + temp + "\">" + reader["city"].ToString().Replace(" ", "&nbsp;") + "</a>, ";
                            }
                        }
                        reader.Close();
                        divCitiesRt.InnerHtml = divCitiesRt.InnerHtml.Remove(divCitiesRt.InnerHtml.Length - 2, 2);

                        DataTable dtCountries = objR.spStateProvByCountries(countryid);
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
                        if (rtLower.InnerHtml.Length > 2)
                            rtLower.InnerHtml = rtLower.InnerHtml.Remove(rtLower.InnerHtml.Length - 2, 2);
                    }

            }
            catch (Exception ex) { lblInfo22.Text = ex.Message + ":22"; }
        }
        //FillCitiesColumn();
        Session["city"] = city;
        Session["state"] = stateprovince;
        Session["country"] = country;
        ((System.Web.UI.WebControls.Image)Master.FindControl("Logo")).AlternateText = "Vacations-Abroad.com";
        Page.Header.Controls.Add(new LiteralControl("<link href='/css/StyleSheetBig4.css' rel='stylesheet' type='text/css'></script>"));
        
    }
    private void FillCitiesColumn()
    {        
    }    
    public string GetTitle()
    {
        return Title.Text.Replace("%country%", country).Replace("%stateprovince%", stateprovince).
            Replace("%city%", city);   
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
    protected void State_datagrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        DataTable dt = new DataTable();
        DBConnection obj = new DBConnection();
        if (e.Row.RowIndex != -1)
        {
            try
            {
                int Vid = Convert.ToInt32(State_datagrid.DataKeys[e.Row.RowIndex][0]);
                dt = VADBCommander.TourInfoByTourID(Vid.ToString());
                if (dt.Rows.Count > 0)
                {
                    Label lblPic = (Label)e.Row.FindControl("lblPicName");
                    if ((lblPic != null) && (dt.Rows.Count > 0))
                    {
                        lblPic.Text = dt.Rows[0]["compname2"].ToString();
                        if (lblPic.Text.Length > 30)
                            lblPic.Text = lblPic.Text.Remove(30);
                    }               
                }
               
            }
            catch (Exception ex) { lblInfo22.Text = ex.ToString(); }
            finally { obj.CloseConnection(); }
        }
    }   
           
}
