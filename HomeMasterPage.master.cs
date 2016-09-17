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

public partial class HomeMasterPage : System.Web.UI.MasterPage
{

    protected void Page_Load(object sender, EventArgs e)
    {
        //---------------------end North America
        string strUrl = Request.Url.ToString().ToLower();
        //

        string strPage = Page.AppRelativeVirtualPath;
        bool ShowRightSide = strPage == "~/default.aspx" ? true : false;
        searchwithoutnavigation.InnerHtml = GetTopMenu();

        if (ShowRightSide)
        {
            regionmenu.Visible = true;
	    dvRightFloat.Visible = true;
            dvLft.Attributes.Add("style", "padding-top: 83px;");
        }
        else
        {
            
			dvRightFloat.Visible = false;
            regionmenu.Visible = false;
            dvLft.Attributes.Add("style", "padding-top: 83px;width:882px;");
        }

    }

    private string GetTopMenu()
    {
        string query = " SELECT DISTINCT CO.Country,R.Region FROM Regions as R LEFT OUTER JOIN Countries as CO ON R.ID = CO.RegionId LEFT OUTER JOIN StateProvinces as S ON S.CountryId = Co.ID LEFT OUTER JOIN Cities as c ON c.StateProvinceID = S.ID"
                         + " LEFT OUTER JOIN Properties as P on P.[CityID] = c.Id"
                         + " where CO.Country <> '' and P.ID IS NOT NULL and ifApproved=1 and ifFinished=1"
                         + " GROUP BY R.Region,CO.Country"
                         + " order by R.Region,CO.Country";
        DBConnection obj3 = new DBConnection();

        DataTable dt = obj3.GetDataSetArtificial(query);

        string menu = string.Empty;
        string region = string.Empty;
        menu += "<ul class=\"whennotloggedin nav\">";

        foreach (DataRow dr in dt.Rows)
        {
            if (string.IsNullOrEmpty(region))
            {
                region = Convert.ToString(dr["Region"]);
                menu += "<li>";
                menu += "<a rel=\"nofollow\" href=\"#\">" + region + "</a>";
                menu += "<ul class=\"submenu\">";
            }

            if (region != Convert.ToString(dr["Region"]))
            {
                if (dr["Country"] != null)
                {
                    region = Convert.ToString(dr["Region"]);
                    menu += "</ul>";
                    menu += "</li>";
                    menu += "<li>";
                    menu += "<a rel=\"nofollow\" href=\"#\">" + region + "</a>";
                    menu += "<ul class=\"submenu\">";
                }
            }
            if (dr["Country"] != null)
            {
                string url = "/" + Convert.ToString(dr["Country"]).ToLower().Replace(" ", "_") + "/default.aspx";
                menu += "<li><a href='" + url + "'>" + Convert.ToString(dr["Country"]) + "</a></li>";
            }
        }

        menu += "</ul>";
        menu += "</li>";
        menu += "</ul>";
        return menu;
    }
    
    protected void SearchByKeyWords_Click(object sender, EventArgs e)
    {
        if (tbKeyWords.Text.Length > 0)
        {
            Response.Redirect(CommonFunctions.PrepareURL("SearchTerms.aspx?SearchTerms=" + HttpUtility.UrlEncode(tbKeyWords.Text)), true);
        }

    }

}
