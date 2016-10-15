using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class masterpage_NoramlMaster : System.Web.UI.MasterPage
{
    public DataSet[] countrylist= new DataSet[8];
    public string []CountryName = {"Africa", "Asia", "Europe", "Middle East", "North America", "Oceania", "South America"};
    public int[] regionid = { 1, 2, 6, 7, 8, 3, 9 };

    public string strkeyword
    {
        get
        {
            return tbKeyWords.Value;
        }
        set
        {
            tbKeyWords.Value = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
     /*   for(int i=0;i<7; i++)
        {
            countrylist[i] = AjaxProvider.getCountryInfoSet(regionid[i]);
        }
        */
    }

    protected void searchbt_ServerClick(object sender, EventArgs e)
    {
        if (tbKeyWords.Value.Length > 0)
        {
            Response.Redirect(CommonFunctions.PrepareURL("SearchTerms.aspx?SearchTerms=" + HttpUtility.UrlEncode(tbKeyWords.Value)), true);
        }
    }

    protected void SigninClick(object sender, EventArgs e)
    {
        Response.Redirect("/accounts/login.aspx?type=1"); 
    }
}
