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

public partial class MasterPage : System.Web.UI.MasterPage
{

    protected void Page_Load(object sender, EventArgs e)
    {
        //---------------------end North America
        string strUrl = Request.Url.ToString().ToLower();
        //

        string strPage = Page.AppRelativeVirtualPath;
        bool ShowRightSide = strPage == "~/default.aspx" ? true : false;

        /*
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
        */
    }
    
    protected void SearchByKeyWords_Click(object sender, EventArgs e)
    {
        if (tbKeyWords.Text.Length > 0)
        {
            Response.Redirect(CommonFunctions.PrepareURL("SearchTerms.aspx?SearchTerms=" + HttpUtility.UrlEncode(tbKeyWords.Text)), true);
        }

    }


}
