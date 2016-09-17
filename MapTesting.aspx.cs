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
using System.Collections.Generic;
using Newtonsoft.Json;


public partial class MapTesting : System.Web.UI.Page
{
    private const string STR_SELECTCitiesFROMCitiesWHERECitiesStateProvinceID = "SELECT     CityLatLong.*" +
" FROM         Cities INNER JOIN " +
 " StateProvinces ON Cities.StateProvinceID = StateProvinces.ID INNER JOIN " +
                     " Countries ON StateProvinces.CountryID = Countries.ID INNER JOIN " +
                     "CityLatLong ON Cities.City = CityLatLong.City AND Countries.Country = CityLatLong.Country AND StateProvinces.StateProvince = CityLatLong.StateProvince " +
"where 1=1 AND EXISTS ( SELECT * FROM Properties WHERE (Properties.IfFinished = 1) AND (Properties.IfApproved = 1) AND (Properties.CityID = Cities.ID)  AND NOT EXISTS (SELECT * FROM Auctions WHERE PropertyID = Properties.ID))";
    protected SqlDataAdapter CitiesAdapter; 
    
    protected void Page_Load(object sender, EventArgs e)
    {

        SqlConnection con = CommonFunctions.GetConnection();
        CitiesAdapter = new SqlDataAdapter(STR_SELECTCitiesFROMCitiesWHERECitiesStateProvinceID, con);//CommonFunctions.PrepareAdapter(CommonFunctions.GetConnection(), String.Format(STR_SELECTCitiesFROMCitiesWHERECitiesStateProvinceID), SqlDbType.Int);
       

        DataTable dt = new DataTable();
        CitiesAdapter.Fill(dt);
        List<Location> eList = new List<Location>();
        string maxLat = "";
        string maxLong = "";
        foreach (DataRow dr in dt.Rows)
        {
            try
            {
                Location e1 = new Location();
                e1.title = dr["City"].ToString();
                e1.lat = Convert.ToDouble(dr["Latitude"]);
                e1.lng = Convert.ToDouble(dr["Longitude"]); ;
                e1.description = dr["City"].ToString();
                string temp = CommonFunctions.GetSiteAddress() + "/" + dr["Country"].ToString().ToLower().Replace(" ", "_") +
                 "/" + dr["StateProvince"].ToString().ToLower().Replace(" ", "_") + "/" + dr["City"].ToString().ToLower().Replace(" ", "_") + "/default.aspx";
                e1.URL = temp;
                eList.Add(e1);
            }
            catch { }
        }
        // Response.Write(CitiesAdapter.SelectCommand.CommandText);
        string ans = JsonConvert.SerializeObject(eList, Formatting.Indented);



        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(Page.GetType(), "JSON", "initialize(" + ans + ");", true);

    }
}
public class Location
{
    public string title;
    public double lat;
    public double lng;
    public string description;
    public string URL;
}