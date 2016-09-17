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
        LogInLink.NavigateUrl = CommonFunctions.PrepareURL("Login.aspx");
        CreateAccountLink.NavigateUrl = CommonFunctions.PrepareURL("FindOwner.aspx");
        LogOutLink.NavigateUrl = CommonFunctions.PrepareURL("Logout.aspx");
        UserIDLink.NavigateUrl = CommonFunctions.PrepareURL("Listings.aspx");
        OwnersLinkLink.NavigateUrl = CommonFunctions.PrepareURL("OwnersList.aspx");
        OutStandingInvoicesLink.NavigateUrl = CommonFunctions.PrepareURL("OutstandingInvoices.aspx");
        AdminLink.NavigateUrl = CommonFunctions.PrepareURL("Administration.aspx");       

        Logo.ImageUrl = CommonFunctions.PrepareURL("images/logo.jpg");
        //MainLogo.ImageUrl = CommonFunctions.PrepareURL("images/main.jpg");
    }

    protected void SearchByKeyWords_Click(object sender, EventArgs e)
    {
        if (KeyWords.Text.Length > 0)
            Response.Redirect(CommonFunctions.PrepareURL("SearchTerms.aspx?SearchTerms=" +
                HttpUtility.UrlEncode(KeyWords.Text)), true);
    }
    
}
