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
    public EmailResponseInfo email_resp;
    public string[] currency_type = { "dollar", "euro" };

    public int respid = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!AuthenticationManager.IfAuthenticated || !User.Identity.IsAuthenticated)
        {
            FormsAuthentication.SignOut();
        }
        
        Int32.TryParse(Request.QueryString["respid"], out respid);

        email_resp = BookResponseEmail.getResponseInfo(respid);
        if (email_resp.ID == 0) Response.Redirect("/Error.aspx?error=Wrong Response number");

        inquiryinfo = BookDBProvider.getQuoteInfo(email_resp.QuoteID);

        countryinfo = BookDBProvider.getCountryInfo(inquiryinfo.PropertyID);

    }

 
    protected void SendQuote_Click(object sender, EventArgs e)
    {
       /* BookDBProvider.addEmailResponse(userid, inquiryinfo.UserID, quoteid, Convert.ToDecimal(rates.Text),
            Convert.ToDecimal(totalsum.InnerText), Convert.ToDecimal(cleaningfee.Text), Convert.ToDecimal(secdeposit.Text),
            Convert.ToDecimal(loadingtaxval.InnerText), Convert.ToDecimal(balance.Text), Convert.ToDecimal(cancel30.Text),
            Convert.ToDecimal(cancel60.Text), Convert.ToDecimal(cancel90.Text),DateTime.Now, Convert.ToInt32(validnumber.Value), Convert.ToInt32(currency.SelectedValue));

        BookDBProvider.updateEmailQuoteState(quoteid);

        Response.Redirect("/userowner/listings.aspx");
        */
    }
}