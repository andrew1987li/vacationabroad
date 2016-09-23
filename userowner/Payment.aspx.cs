using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
    public string[] currency_type = { "dollar", "euro" };

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
        else if (resp_id.Value != null)
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

    }
}