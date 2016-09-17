using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;

public partial class viewCalendar : CommonPage
{
    public string city = "";
    public string state = "";
    public string country = "";

    public string prevPage1 = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        
        
        HtmlHead head = Page.Header;

        HtmlMeta keywords = new HtmlMeta();
        HtmlMeta robots = new HtmlMeta();

        robots.Content = "NOINDEX, NOFOLLOW";
        //head.Title = GetTitle();
        //Session.Clear();
        if (!IsPostBack)
        {
            //if (Request.UrlReferrer != null)
            //{
            //    string prevPage = Request.UrlReferrer.ToString();

            //    Session["prevCal"] = prevPage;
            //    prevPage1 = prevPage;
            //}
        }
        FillCalendar();
        MakeLinksText();
        //DataBind();
    }
    private void MakeLinksText()
    {
        int Vid = Convert.ToInt32(Request.QueryString["propertyID"]);
        DBConnection obj = new DBConnection();
        try
        {
            DataTable dt = VADBCommander.CityStateCountryByProperty(Vid.ToString());
            if (dt.Rows.Count > 0)
            {
                city = dt.Rows[0]["City"].ToString();
                state = dt.Rows[0]["State"].ToString();
                country = dt.Rows[0]["Country"].ToString();

                string temp = CommonFunctions.GetSiteAddress() + "/" + dt.Rows[0]["Country"].ToString() + "/" + dt.Rows[0]["State"].ToString() + "/" + dt.Rows[0]["City"].ToString() + "/default.aspx";
                temp = temp.ToLower();
                temp = temp.Replace(" ", "_");

                hlkCity.Text = city;
                hlkCity.NavigateUrl = temp;

                temp = CommonFunctions.GetSiteAddress() + "/" + dt.Rows[0]["Country"].ToString() + "/" + dt.Rows[0]["State"].ToString() + "/default.aspx";
                temp = temp.ToLower();
                temp = temp.Replace(" ", "_");
                hlkState.Text = state;
                hlkState.NavigateUrl = temp;

                temp = CommonFunctions.GetSiteAddress() + "/" + dt.Rows[0]["Country"].ToString() +  "/default.aspx";
                temp = temp.ToLower();
                temp = temp.Replace(" ", "_");
                hlkCountry.Text = country;
                hlkCountry.NavigateUrl = temp;


                temp = CommonFunctions.GetSiteAddress() + "/" + dt.Rows[0]["Country"].ToString() + "/" + dt.Rows[0]["State"].ToString() + "/" + dt.Rows[0]["City"].ToString() + "/" + Vid.ToString()  + "/default.aspx";
                temp = temp.ToLower();
                temp = temp.Replace(" ", "_");
                hlkProperty.Text = "Property #" + Vid.ToString();
                hlkProperty.NavigateUrl = temp;

                Session["calCity"] = dt.Rows[0]["City"].ToString();
            }
        }
        catch (Exception ex) { lblInfo.Text = ex.Message; }
        finally { obj.CloseConnection(); }
        Page.Header.Controls.Add(new LiteralControl("<link href='/css/StyleSheetBig4.css' rel='stylesheet' type='text/css'></script>"));

    }
    private void FillCalendar()
    {
        DataTable dt = new DataTable();
        DBConnection obj = new DBConnection();

        if (Request.QueryString["propertyID"] != null)
        {
            int Vid = Convert.ToInt32(Request.QueryString["propertyID"]);

            dt =VADBCommander.PropertyAvailDatesByProperty(Vid.ToString());
            if (dt.Rows.Count > 0)
            {
                //place into list for dayrender
                List<DateTime> lst = new List<DateTime>();
                
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    lst.Add(Convert.ToDateTime(dt.Rows[i]["PropertyDates"]));
                    Calendar1.SelectedDates.Add(Convert.ToDateTime(dt.Rows[i]["PropertyDates"]));
                }

                Calendar1.TodaysDate = DateTime.Today;

                Session["PlaceCal"] = true;
            }
            else
            {
                Session.Remove("PlaceCal");
            }
        }

        obj.CloseConnection();
    }
    protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
    {
        e.Cell.Text = e.Day.DayNumberText;
        if (e.Day.IsOtherMonth == true)
        {
            e.Cell.Text = "";
            //e.Cell.Visible = false;
            e.Cell.BackColor = System.Drawing.Color.White;
        }
    }
    public string GetTitle()
    {
        int Vid = Convert.ToInt32(Request.QueryString["propertyID"]);
        string titlereplacement = city + " Property " + Vid.ToString() + " Calendar - Vacation Rentals";

        return titlereplacement;
    }
}

