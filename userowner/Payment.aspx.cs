using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class userowner_Payment : CommonPage
{
    public InquiryInfo inquiryinfo;
    public CountryInfo countryinfo;
    public EmailResponseInfo email_resp;
    public UserInfo owner_info;
    public PropertyInform prop_info;
    public string[] currency_type = { "USD", "EUR", "GBP", "YEN", "CAD" };

    public int respid = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        NameValueCollection nvc = Request.Form;
        // string userName, password;
        if (!string.IsNullOrEmpty(nvc["respid"]))
        {
            respid = Convert.ToInt32(nvc["respid"]);
            resp_id.Value = respid.ToString();
        }
        else if (resp_id.Value != null && resp_id.Value!="")
        {
            respid = Convert.ToInt32(resp_id.Value);
        }
        else Response.Redirect("/Error.aspx?error=Wrong Request for payment"); ;  //Not post or Wrong respid
        //Get the inquiry info.
        email_resp = BookResponseEmail.getResponseInfo(respid);
        if (email_resp.ID == 0 || email_resp.IsValid < 1) Response.Redirect("/Error.aspx?error=Wrong Response number or not valid");

        inquiryinfo = BookDBProvider.getQuoteInfo(email_resp.QuoteID);
        countryinfo = BookDBProvider.getCountryInfo(inquiryinfo.PropertyID);
        owner_info = BookDBProvider.getUserInfo(inquiryinfo.PropertyOwnerID);
        prop_info = BookDBProvider.getPropertyInfo(inquiryinfo.PropertyID);
    }

    protected void payment_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(paytype.SelectedValue) == 0)
        {
            PaywithPaypal();
        }
    }

    protected void PaywithPaypal()
    {

        /*
         * 
    URL is the URL to work with, depending on whether sandbox or a real PayPal account should be used
    cmd is a command that is sent to PayPal
    business is the seller's e-mail
    item_name is the item name -- i.e. what buyer pays for -- that will be shown to user;
    amount is the payment amount
    no_shipping is a parameter that determines whether the delivery address should be requested
    return_url is the URL that the buyer will be redirected to when payment is successfully performed
    rm is a parameter that determines the way in which information about a successfully finished transaction will be sent to the script specified in the return parameter
    notify_url is the URL PayPal will send information about transaction (IPN) to
    cancel_url is the URL that the buyer is redirected to when he cancels payment
    currency_code is the currency code
    request_id is an identifier of payment request

         * */


        string redirecturl = "";

        //Mention URL to redirect content to paypal site
        redirecturl += "https://www.paypal.com/cgi-bin/webscr?cmd=_xclick&business=" +
                       ConfigurationManager.AppSettings["PaypalEmail"].ToString();

        //Product Name
        redirecturl += String.Format("&item_name=Property{0} in {1},{2},{3}", inquiryinfo.PropertyID, countryinfo.city, countryinfo.state, countryinfo.country);
        //item_number
        redirecturl += "&item_number=" + inquiryinfo.id;
        //Product Name
        redirecturl += "&amount=" + (email_resp.Balance + email_resp.Sum);

        //Shipping charges if any
        redirecturl += "&no_shipping=1";

        redirecturl += "&rm=2";
        //Currency code 
        redirecturl += "&currency_code=" + "USD";

        //Success return page url
        redirecturl += "&return=" +
          ConfigurationManager.AppSettings["SuccessURL"].ToString();

        //Failed return page url
        redirecturl += "&cancel_return=" +
          ConfigurationManager.AppSettings["FailedURL"].ToString();

        redirecturl += "&notify_url=" +
          ConfigurationManager.AppSettings["IPNURL"].ToString();

        Response.Redirect(redirecturl);
    }

    protected void PaywithCredit()
    {

    }
}