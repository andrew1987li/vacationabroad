//live
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

//public partial class newCityList : System.Web.UI.Page
public partial class newCityList : CommonPage
{

    protected Vacations.PropertiesDataset PropertiesSet;
    public string region;
    public string country;
    public string stateprovince;
    public string city;
    public string pageTitle;
    public string Property;
    private const string STR_SELECTCitiesFROMCitiesWHERECitiesStateProvinceID = "SELECT Cities.* FROM Cities WHERE (Cities.StateProvinceID = @StateProvinceID) AND EXISTS ( SELECT * FROM Properties WHERE (Properties.IfFinished = 1) AND (Properties.IfApproved = 1) AND (Properties.CityID = Cities.ID)  AND NOT EXISTS (SELECT * FROM Auctions WHERE PropertyID = Properties.ID)) ORDER BY City";

    private int regionid = -1;
    private int countryid = -1;
    private int stateprovinceid = -1;
    public int cityid = -1;

    public CountryInfoWithCityID countryinfo;

    public int[] bedroominfo = new int[4];
    public int[] amenity_id = { 8, 33, 1, 11, 0 };
    public int[] amenity_nums = new int[5];

    // public string[] str_propcate = { "Chalet", "Apartment", "Villa", "Hotel", "Cottage", "Boat", "Castle", "B&B", "Guesthouse", "Farmhouse", "Display All" };
    // public int[] prop_typeval = { 17, 4, 1, 2, 9, 15, 16, 5, 11, 13, 0 };
    public string[] str_propcate = { "Vacation Rentals", "Hotels", "Display All" };
    public int[] prop_typeval = { 1, 2, 0 };
    public int[] prop_nums = new int[3];
    //live
    protected void Page_Load(object sender, System.EventArgs e)
    {
		//Response.Clear();
        //Response.StatusCode = 404;
        //Response.End(); 
        //HttpResponse.RemoveOutputCacheItem("/CityList.aspx");
        //CommonFunctions.Connection.Open ();
        
           
            if ((Request.Params["CityID"] != null) && (Request.Params["CityID"].Length > 0))
                try
                {
                    cityid = Convert.ToInt32(Request.Params["CityID"]);
                }
                catch(Exception)
                {
                }
            //lblcity.Text = cityid.ToString();
            //cityid = 3031;
            if(cityid == -1)
                Response.Redirect(CommonFunctions.PrepareURL("InternalError.aspx"));
  

                // propertyset = SearchProvider.getPropertyListInfoSet(strkeyword, 0, 0, 0);
                // propertylist.DataSource = propertyset;
                // propertylist.DataBind();
                // propertytypes = SearchProvider.getPropertyTypeListSet(strkeyword);
                for (int i = 0; i < 4; i++)
                    bedroominfo[i] = SearchProvider.getNumbersOfCityID(cityid, 0, 0, i);
                for (int i = 0; i < 5; i++)
                    amenity_nums[i] = SearchProvider.getNumbersOfCityID(cityid, 0, amenity_id[i], 0);

                for (int i = 0; i < 3; i++)
                {
                    prop_nums[i] = SearchProvider.getNumbersOfCityID(cityid, prop_typeval[i], 0, 0);
                }
        // ajax_proplist = SearchProvider.getAjaxPropListSet(strkeyword, 0, 0, 0, 0, 0);

        countryinfo = SearchProvider.getCountryInfoCityID(cityid);

        hyperRegion.NavigateUrl="/"+countryinfo.Region.ToLower().Replace(" ", "_") + "/default.aspx";
        hyplnkCountryBackLink.NavigateUrl= "/" + countryinfo.Region.ToLower().Replace(" ", "_") +"/"+countryinfo.Country.ToLower().Replace(" ", "_")+ "/default.aspx";
        hyplnkStateBackLink.NavigateUrl = "/" + countryinfo.Region.ToLower().Replace(" ", "_") + "/" + countryinfo.Country.ToLower().Replace(" ", "_") + "/" + countryinfo.StateProvince.ToLower().Replace(" ", "_") + "/default.aspx";

        ltrH11.Text = countryinfo.City + " Vacations";
        lblcity.Text = countryinfo.CityText;

        if (!IsPostBack) { 
        txtCityText.Text= countryinfo.CityText;
        txtCityText2.Text = countryinfo.CityText2;
        CityParam.Value = cityid.ToString();
        }
        // Response.Write(cityid);
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


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        SearchProvider.setCityText(Int32.Parse(CityParam.Value), 0, txtCityText.Text);
    }

    protected void btnSubmit2_Click(object sender, EventArgs e)
    {
          // Response.Write(CityParam.Value);
        if ( SearchProvider.setCityText(Int32.Parse(CityParam.Value), 1, txtCityText2.Text))
        {
            Response.Write("Ok");

        }else
        {
            Response.Write("Not good");
        }
    }
}
