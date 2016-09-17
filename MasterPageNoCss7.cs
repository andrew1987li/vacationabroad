using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Web.SessionState;

public partial class MasterPage7 : System.Web.UI.MasterPage
{

    protected void Page_Load(object sender, EventArgs e)
    {
        //LogInLink.NavigateUrl = CommonFunctions.PrepareURL("Login.aspx");
        //CreateAccountLink.NavigateUrl = CommonFunctions.PrepareURL("FindOwner.aspx");
        //LogOutLink.NavigateUrl = CommonFunctions.PrepareURL("Logout.aspx");
        //UserIDLink.NavigateUrl = CommonFunctions.PrepareURL("Listings.aspx");
        //OwnersLinkLink.NavigateUrl = CommonFunctions.PrepareURL("OwnersList.aspx");
        //OutStandingInvoicesLink.NavigateUrl = CommonFunctions.PrepareURL("OutstandingInvoices.aspx");
        //AdminLink.NavigateUrl = CommonFunctions.PrepareURL("Administration.aspx");

        //Logo.ImageUrl = CommonFunctions.PrepareURL("images/logo.jpg");
        //MainLogo.ImageUrl = CommonFunctions.PrepareURL("images/main.jpg");

        DBConnection obj = new DBConnection();
        DataTable dtCountryList = new DataTable();

        DataTable dtStateList = new DataTable();

        StringBuilder sbNorthAmerica = new StringBuilder();
        //----------------------------North America
        //north amer

        StringBuilder sb = new StringBuilder();
        sb.AppendLine("<ul class=\"TripleListMain\">");
        sb.AppendLine("<li><a class=\"mainPgCountry\" href=\"/canada/default.aspx\"><b>Canada</b></a></li>");
        sb.AppendLine("<li><a class=\"mainPgCountry\" href=\"/mexico/default.aspx\"><b>Mexico</b></a></li>");
        sb.AppendLine("<li><a class=\"mainPgCountry\" href=\"/usa/default.aspx\"><b>USA</b></a></li>");
        sb.AppendLine("<li><a class=\"mainPgCountry\" href=\"/bahamas/default.aspx\"><b>Bahamas</b></a></li>");
        sb.AppendLine("<li><a class=\"mainPgCountry\" href=\"/barbados/default.aspx\"><b>Barbados</b></a></li>");
        sb.AppendLine("<li><a class=\"mainPgCountry\" href=\"/belize/default.aspx\"><b>Belize</b></a></li>");
        sb.AppendLine("<li><a class=\"mainPgCountry\" href=\"/costa_rica/default.aspx\"><b>Costa Rica</b></a></li>");
        sb.AppendLine("<li><a class=\"mainPgCountry\" href=\"/dominica/default.aspx\"><b>Dominica</b></a></li>");
        sb.AppendLine("<li><a class=\"mainPgCountry\" href=\"/dominican_republic/default.aspx\"><b>Dominican Republic</b></a></li>");
        sb.AppendLine("<li><a class=\"mainPgCountry\" href=\"/french_caribbean/default.aspx\"><b>French Caribbean</b></a></li>");
        sb.AppendLine("<li><a class=\"mainPgCountry\" href=\"/guatemala/default.aspx\"><b>Guatemala</b></a></li>");
        sb.AppendLine("<li><a class=\"mainPgCountry\" href=\"/jamaica/default.aspx\"><b>Jamaica</b></a></li>");
        sb.AppendLine("<li><a class=\"mainPgCountry\" href=\"/netherlands_caribbean/default.aspx\"><b>Netherlands Caribbean</b></a></li>");
        sb.AppendLine("<li><a class=\"mainPgCountry\" href=\"/nicaragua/default.aspx\"><b>Nicaragua</b></a></li>");
        sb.AppendLine("<li><a class=\"mainPgCountry\" href=\"/panama/default.aspx\"><b>Panama</b></a></li>");
        sb.AppendLine("<li><a class=\"mainPgCountry\" href=\"/saint_kitts_and_nevis/default.aspx\"><b>Saint Kitts and Nevis</b></a></li>");
        sb.AppendLine("<li><a class=\"mainPgCountry\" href=\"/st_lucia/default.aspx\"><b>St Lucia</b></a></li>");
        sb.AppendLine("<li><a class=\"mainPgCountry\" href=\"/the_grenadines/default.aspx\"><b>The Grenadines</b></a></li>");
        sb.AppendLine("<li><a class=\"mainPgCountry\" href=\"/trinidad_and_tobago/default.aspx\"><b>Trinidad and Tobago</b></a></li>");
        sb.AppendLine("<li><a class=\"mainPgCountry\" href=\"/uk_caribbean/default.aspx\"><b>UK Caribbean</b></a></li>");
        sb.AppendLine("<li><a class=\"mainPgCountry\" href=\"/us_caribbean/default.aspx\"><b>US Caribbean</b></a></li>");
        sb.AppendLine("</ul>");


        divnAmerica.InnerHtml = sb.ToString();// GenerateCountryLinks("8");
        divSouthAmerica.InnerHtml = GenerateCountryLinks("9");
        divAfrica.InnerHtml = GenerateCountryLinks("1");
        divAsia.InnerHtml = GenerateCountryLinks("2");
        divEurope.InnerHtml = GenerateCountryLinks("6");
        divOceania.InnerHtml = GenerateCountryLinks("3");

        //        ID	Region
        //1	Africa
        //2	Asia
        //3	Oceania
        //4	Caribbean
        //5	Central America
        //6	Europe
        //7	Middle East


        //---------------------end North America
        string strUrl = Request.Url.ToString().ToLower();
        //

        string strPage = Page.AppRelativeVirtualPath;
        bool ShowRightSide = strPage == "~/default.aspx" ? true : false;


        if (ShowRightSide)
        {
            dvRightFloat.Visible = true;
            dvLft.Attributes.Add("style", "padding-top: 83px;");
        }
        else
        {
            dvRightFloat.Visible = false;
            dvLft.Attributes.Add("style", "padding-top: 83px;width:876px;");
        }

    }
    public string GenerateCountryLinks(string regionID)
    {
        DataTable dtCountryList = new DataTable();
        DataTable dtStateList = new DataTable();
        StringBuilder sb = new StringBuilder();
        //----------------------------North America
        //north amer
        dtCountryList = VADBCommander.CountiesByRegionList(regionID);
        //dtCountryList = VADBCommander.CountriesByRegionList(regionID);
        if (dtCountryList.Rows.Count > 0)
        {
            sb.AppendLine("<ul class=\"TripleListMain\">");
            foreach (DataRow row in dtCountryList.Rows)
            {
                //we list the country first
                sb.AppendLine("<li><a class=\"mainPgCountry\" href=\"" + CommonFunctions.PrepareURL(row["country"].ToString().ToLower().Replace(" ", "_") + "/default.aspx") + "\"><b>" + row["country"].ToString() + "</b></a></li>");
                bool stateDown = false;

                //we grab all of the states for that country
                dtStateList = VADBCommander.StateProvinceByCountryList(row["id"].ToString());

                if (dtStateList.Rows.Count > 0)
                {

                    /*
                    sbNorthAmerica.AppendLine("<ul class=\"links fltlft\">");
                    foreach (DataRow rowTemp in dtStateList.Rows)
                    {
                        DataTable dtCityList = VADBCommander.CityStateCountryRegionList(rowTemp["id"].ToString());
                        //add cities to lower
                        if (dtCityList.Rows.Count > 0)
                        {
                            //sbNorthAmerica.AppendLine("<li><a class=\"mainState\" href=\"" + CommonFunctions.PrepareURL(row["country"].ToString().ToLower().Replace(" ", "_").Replace(" ", "_").ToLower() + "/" + rowTemp["stateprovince"].ToString().Replace(" ", "_").ToLower() + "/default.aspx") + "\">" + rowTemp["stateprovince"].ToString().Replace(" ", "&nbsp;").Replace("-", "&#8209;") + "</a>");
                            
                            //sbNorthAmerica.AppendLine(":  ");
                            //foreach (DataRow row1 in dtCityList.Rows)
                            //{
                            //    sbNorthAmerica.AppendLine("<a class=\"mainCity\" href=\"" + CommonFunctions.PrepareURL(row["country"].ToString().ToLower().Replace(" ", "_") + "/" + rowTemp["stateProvince"].ToString().Replace(" ", "_").ToLower() + "/" + row1["city"].ToString().ToLower().Replace(" ", "_") + "/default.aspx") + "\" class=\"mainCity\">" + row1["city"].ToString().Replace(" ", "&nbsp;").Replace("-", "&#8209;") + "</a>, ");
                            //} 
                            sbNorthAmerica.AppendLine("</li>");
                        }
                        else
                        {
                            sbNorthAmerica.AppendLine("<li><a class=\"mainState\" href=\"" + CommonFunctions.PrepareURL(row["country"].ToString().ToLower().Replace(" ", "_").Replace(" ", "_").ToLower() + "/" + rowTemp["stateprovince"].ToString().Replace(" ", "_").ToLower() + "/default.aspx") + "\">" + rowTemp["stateprovince"].ToString().Replace(" ", "&nbsp;").Replace("-", "&#8209;") + "</a></li>");
                        }
                    }
                    sbNorthAmerica.AppendLine("</ul>");
                      */
                }
            }
            sb.AppendLine("</ul>");
        }
        return sb.ToString();

    }

    protected void SearchByKeyWords_Click(object sender, EventArgs e)
    {
        if (tbKeyWords.Text.Length > 0)
        {
            Response.Redirect(CommonFunctions.PrepareURL("SearchTerms.aspx?SearchTerms=" + HttpUtility.UrlEncode(tbKeyWords.Text)), true);
        }

    }

}
