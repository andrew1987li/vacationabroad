using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class ViewTour : System.Web.UI.Page
{
    public string tourName = "";
    public string tourName1 = "";
    public string tourAddress = "";
    public string tourTime = "";
    public string tourType = "";
    public string city = "";
    public string state = "";
    public string country = "";
    public string tourDesc = "";
    public string lowRate = "";
    public string tourCompany = "";
    public string tourContact = "";
    public string tourWebsite = "";
    public string tourPriPhone = "";
    public string tourMobile = "";
    public string tourID = "";
    public string abbr = "";
    public string cityLink = "";
    public string stateLink = "";
    public string countryLink = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        DBConnection obj = new DBConnection();
        string query = "";
        DataTable dt = new DataTable();

        try
        {
            if (Request.QueryString["tourID"] != null)
            {
                tourID = Request.QueryString["tourID"].ToString();


                dt = VADBCommander.TourInfoInd(tourID);
                if (dt.Rows.Count > 0)
                {
                    city = dt.Rows[0]["city"].ToString();
                    state = dt.Rows[0]["stateprovince"].ToString();
                    country = dt.Rows[0]["country"].ToString();
                    tourName = dt.Rows[0]["name"].ToString();
                    tourName1 = dt.Rows[0]["name2"].ToString();
                    tourMobile = dt.Rows[0]["mobile2"].ToString();
                    tourPriPhone = dt.Rows[0]["priphone2"].ToString();
                    lowRate = dt.Rows[0]["lowrate"].ToString();
                    tourTime = dt.Rows[0]["starthour"].ToString() + ":" + dt.Rows[0]["startminute"].ToString() +
                        " " + dt.Rows[0]["ampm"].ToString();                
                    tourWebsite = dt.Rows[0]["website2"].ToString();
                    tourAddress = dt.Rows[0]["address"].ToString();
                    tourCompany = dt.Rows[0]["companyname2"].ToString();
                    tourContact = dt.Rows[0]["contactname"].ToString();
                    tourDesc = dt.Rows[0]["description"].ToString();
                   

                    //if ((bool)dt.Rows[0]["mon"] == true)
                      //  lblDays.Text += "Monday, ";
                    //if ((bool)dt.Rows[0]["tue"] == true)
                    //    lblDays.Text += "Tuesday, ";
                    //if ((bool)dt.Rows[0]["wed"] == true)
                      //  lblDays.Text += "Wednesday, ";
                    //if ((bool)dt.Rows[0]["thu"] == true)
                      //  lblDays.Text += "Thursday, ";
                    //if ((bool)dt.Rows[0]["fri"] == true)
                      //  lblDays.Text += "Friday, ";
                    //if((bool)dt.Rows[0]["sat"] == true)
                      //  lblDays.Text += "Saturday, ";
                    //if ((bool)dt.Rows[0]["sun"] == true)
                      //  lblDays.Text += "Sunday, ";

//                    lblDays.Text = lblDays.Text.Remove(lblDays.Text.Length - 2, 2);

                    imgTour.ImageUrl = CommonFunctions.GetSiteAddress() + "/images/" + dt.Rows[0]["photoimage"].ToString();

                    cityLink = CommonFunctions.PrepareURL(country + "/" + state + "/" + city + "/default.aspx").ToLower().Replace(" ", "_");
                    stateLink = CommonFunctions.PrepareURL(country + "/" + state + "/default.aspx").ToLower().Replace(" ", "_");
                    countryLink = CommonFunctions.PrepareURL(country + "/default.aspx").ToLower().Replace(" ", "_");

                    hlkSite.Text = tourWebsite;
                    if(!tourWebsite.Contains("http://"))
                        tourWebsite = "http://" + tourWebsite;
                    hlkSite.NavigateUrl = tourWebsite;
                    hlkSite.Target = "_blank";

                    DataBind();
                }
            }
            Page.Header.Controls.Add(new LiteralControl("<link href='/css/StyleSheetBig4.css' rel='stylesheet' type='text/css'></script>"));

        }
        catch (Exception ex) { lblError.Text = ex.ToString(); }
        finally { obj.CloseConnection(); }
    }
    public string GetTitle()
    {
        string vValue = city + " Tours at Vacations-Abroad.com";
        return vValue;
    }
}
