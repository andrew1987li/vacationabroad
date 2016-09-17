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
public partial class _Maps : CommonPage
{
    private const string STR_SELECTCitiesFROMCitiesWHERECitiesCountryId = "SELECT     CityLatLong.*" +
 " FROM         Cities INNER JOIN " +
  " StateProvinces ON Cities.StateProvinceID = StateProvinces.ID INNER JOIN "+
                      " Countries ON StateProvinces.CountryID = Countries.ID INNER JOIN "+
                      "CityLatLong ON Cities.City = CityLatLong.City AND Countries.Country = CityLatLong.Country AND StateProvinces.StateProvince = CityLatLong.StateProvince " +
"where Countries.ID=@CountryID AND EXISTS ( SELECT * FROM Properties WHERE (Properties.IfFinished = 1) AND (Properties.IfApproved = 1) AND (Properties.CityID = Cities.ID)  AND NOT EXISTS (SELECT * FROM Auctions WHERE PropertyID = Properties.ID))";

    private const string STR_SELECTCitiesFROMCitiesWHERECitiesStateProvinceID = "SELECT     CityLatLong.*" +
 " FROM         Cities INNER JOIN " +
  " StateProvinces ON Cities.StateProvinceID = StateProvinces.ID INNER JOIN " +
                      " Countries ON StateProvinces.CountryID = Countries.ID INNER JOIN " +
                      "CityLatLong ON Cities.City = CityLatLong.City AND Countries.Country = CityLatLong.Country AND StateProvinces.StateProvince = CityLatLong.StateProvince " +
"where StateProvinces.ID=@StateId AND EXISTS ( SELECT * FROM Properties WHERE (Properties.IfFinished = 1) AND (Properties.IfApproved = 1) AND (Properties.CityID = Cities.ID)  AND NOT EXISTS (SELECT * FROM Auctions WHERE PropertyID = Properties.ID))";

    protected SqlDataAdapter CitiesAdapter;               
    protected void Page_Load(object sender, EventArgs e)
    {
    //        SqlConnection connection = CommonFunctions.GetConnection();
    //        SqlDataAdapter GetIDsAdapter = CommonFunctions.PrepareAdapter(connection,
    //                       "select * from countries where Country=@Country",
    //                       SqlDbType.NVarChar, 300, SqlDbType.NVarChar, 300, SqlDbType.NVarChar, 300, SqlDbType.NVarChar, 300,
    //                       SqlDbType.Int);

    //        DataSet MainDataSet = new DataSet();
    //       string url = HttpContext.Current.Request.UrlReferrer.Host;
    //        string[] spliturl = url.Split('/');
    //     //   GetIDsAdapter.SelectCommand.Parameters["@Region"].Value = "";
    //                GetIDsAdapter.SelectCommand.Parameters["@Country"].Value = spliturl[1];
    //                //GetIDsAdapter.SelectCommand.Parameters["@StateProvince"].Value = "";
    //                //GetIDsAdapter.SelectCommand.Parameters["@City"].Value = "";
    //                //GetIDsAdapter.SelectCommand.Parameters["@PropertyID"].Value = -1;

    //                //lock(CommonFunctions.Connection)
    //                GetIDsAdapter.Fill(MainDataSet);
        ClientScriptManager cs = Page.ClientScript;
        string url = Request.Url.AbsoluteUri;
        string[] token = url.Split('/');

        //cs.RegisterStartupScript(Page.GetType(), "JSON", "alert(" + token.Length + ");", true);
        if (Convert.ToInt32(Request.QueryString["StateProvinceID"]) != null && Convert.ToInt32(Request.QueryString["StateProvinceID"]) > 0)
        {
            SqlConnection con = CommonFunctions.GetConnection();
            CitiesAdapter = new SqlDataAdapter(STR_SELECTCitiesFROMCitiesWHERECitiesStateProvinceID, con);//CommonFunctions.PrepareAdapter(CommonFunctions.GetConnection(), String.Format(STR_SELECTCitiesFROMCitiesWHERECitiesStateProvinceID), SqlDbType.Int);
            CitiesAdapter.SelectCommand.Parameters.Add("@StateId", SqlDbType.Int);
            CitiesAdapter.SelectCommand.Parameters["@StateId"].Value = Convert.ToInt32(Request.QueryString["StateProvinceID"]);


            //cs.RegisterStartupScript(Page.GetType(), "JSON", "alert(" + Convert.ToInt32(Request.QueryString["StateProvinceID"]) + ");", true);

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
                    string temp = "/" + dr["Country"].ToString().ToLower().Replace(" ", "_") +
                     "/" + dr["StateProvince"].ToString().ToLower().Replace(" ", "_") + "/" + dr["City"].ToString().ToLower().Replace(" ", "_") + "/default.aspx";
                    e1.URL = temp;
                    eList.Add(e1);
                }
                catch { }
            }
            // Response.Write(CitiesAdapter.SelectCommand.CommandText);
            string ans = JsonConvert.SerializeObject(eList, Formatting.Indented);



            // ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(Page.GetType(), "JSON", "initialize(" + ans + ");", true);
        }
        else
            if (token.Length == 4)
            {
                SqlConnection con = CommonFunctions.GetConnection();
                CitiesAdapter = new SqlDataAdapter(STR_SELECTCitiesFROMCitiesWHERECitiesCountryId, con);//CommonFunctions.PrepareAdapter(CommonFunctions.GetConnection(), String.Format(STR_SELECTCitiesFROMCitiesWHERECitiesStateProvinceID), SqlDbType.Int);
                CitiesAdapter.SelectCommand.Parameters.Add("@CountryID", SqlDbType.Int);
                CitiesAdapter.SelectCommand.Parameters["@CountryID"].Value = Convert.ToInt32(Request.QueryString["CountryID"]);

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
                        string temp = "/" + dr["Country"].ToString().ToLower().Replace(" ", "_") +
                         "/" + dr["StateProvince"].ToString().ToLower().Replace(" ", "_") + "/" + dr["City"].ToString().ToLower().Replace(" ", "_") + "/default.aspx";
                        e1.URL = temp;
                        eList.Add(e1);
                    }
                    catch { }
                }
                // Response.Write(CitiesAdapter.SelectCommand.CommandText);
                string ans = JsonConvert.SerializeObject(eList, Formatting.Indented);




                cs.RegisterStartupScript(Page.GetType(), "JSON", "initialize(" + ans + ");", true);
            }
        
    }
}

