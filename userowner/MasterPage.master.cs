using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class userowner_MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSearchByKeyWords_Click(object sender, EventArgs e)
    {
        if (tbKeyWords.Value.Length > 0)
        {
            Response.Redirect(CommonFunctions.PrepareURL("SearchTerms.aspx?SearchTerms=" + HttpUtility.UrlEncode(tbKeyWords.Value)), true);
        }
    }
}
