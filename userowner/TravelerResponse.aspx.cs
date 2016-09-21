using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class userowner_TravelerResponse : System.Web.UI.Page
{
    public InquiryInfo inquiryinfo;
    public CountryInfo countryinfo;
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!AuthenticationManager.IfAuthenticated || !User.Identity.IsAuthenticated)
        {
            FormsAuthentication.SignOut();
        }
        int quoteid=0;
        Int32.TryParse(Request.QueryString["quoteid"], out quoteid);
        inquiryinfo = BookDBProvider.getQuoteInfo(quoteid);

        countryinfo = BookDBProvider.getCountryInfo(inquiryinfo.PropertyID);

    }

    protected void rates_TextChanged(object sender, EventArgs e)
    {
        //Response.Write("rate changed");
              
        decimal rate_val = 0; Decimal.TryParse(rates.Text, out rate_val);
        decimal tax_val = 0; Decimal.TryParse(loadingtax.Text, out tax_val);
        decimal clean_val = 0; Decimal.TryParse(cleaningfee.Text, out clean_val);
        decimal sec_val = 0; Decimal.TryParse(secdeposit.Text, out sec_val);
        decimal loading_val = rate_val * tax_val;

        totalsum.InnerText = (rate_val * inquiryinfo.Nights).ToString();
        loadingtaxval.InnerText = loading_val.ToString();

        balance.Text = (clean_val + sec_val + loading_val).ToString();
    }



    protected void SendQuote_Click(object sender, EventArgs e)
    {

    }
}