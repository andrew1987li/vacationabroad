using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class userowner_TravelerResponse : CommonPage
{
    public InquiryInfo inquiryinfo;
    public CountryInfo countryinfo;
    public int quoteid = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!AuthenticationManager.IfAuthenticated || !User.Identity.IsAuthenticated)
        {
            FormsAuthentication.SignOut();
        }
        
        Int32.TryParse(Request.QueryString["quoteid"], out quoteid);

        inquiryinfo = BookDBProvider.getQuoteInfo(quoteid);

        if (inquiryinfo.PropertyID == 0) Response.Redirect("/Error.aspx?error=Wrong Inquiry number");

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
        BookDBProvider.addEmailResponse(userid, inquiryinfo.UserID, quoteid, Convert.ToDecimal(rates.Text),
            Convert.ToDecimal(totalsum.InnerText), Convert.ToDecimal(cleaningfee.Text), Convert.ToDecimal(secdeposit.Text),
            Convert.ToDecimal(loadingtaxval.InnerText), Convert.ToDecimal(balance.Text), Convert.ToDecimal(cancel30.Text),
            Convert.ToDecimal(cancel60.Text), Convert.ToDecimal(cancel90.Text),DateTime.Now, Convert.ToInt32(validnumber.Value), Convert.ToInt32(currency.SelectedValue),
            Convert.ToDecimal(loadingtax.Text));

        BookDBProvider.updateEmailQuoteState(quoteid);

        Response.Redirect("/userowner/listings.aspx");
    }
}