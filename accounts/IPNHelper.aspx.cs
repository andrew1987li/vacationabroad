using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class accounts_IPNHelper : System.Web.UI.Page
{
    public InquiryInfo inquiryinfo;
    public CountryInfo countryinfo;
    public EmailResponseInfo email_resp;
    public UserInfo owner_info,traveler_info;
    public PropertyInform prop_info;
    public Transaction_Item transitem;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Response.Write("Wrong request");
            return;
        }


        string requestUriString = "https://www.paypal.com/cgi-bin/webscr";

        HttpWebRequest request =
               (HttpWebRequest)WebRequest.Create(requestUriString);

        string strFormValues = Encoding.ASCII.GetString(
    this.Request.BinaryRead(this.Request.ContentLength));
        // Set values for the request back
        request.Method = "POST";
        request.ContentType = "application/x-www-form-urlencoded";
        string obj2 = strFormValues + "&cmd=_notify-validate";
        request.ContentLength = obj2.Length;

       

        // Write the request back IPN strings
        StreamWriter writer =
            new StreamWriter(request.GetRequestStream(), Encoding.ASCII);
        writer.Write(RuntimeHelpers.GetObjectValue(obj2));
        writer.Close();

        //send the request, read the response
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        Stream responseStream = response.GetResponseStream();
        Encoding encoding = Encoding.GetEncoding("utf-8");
        StreamReader reader = new StreamReader(responseStream, encoding);
        string resp = reader.ReadToEnd();

        saveLog();

        if(resp== "VERIFIED")
        {
            if(transitem.business == ConfigurationManager.AppSettings["PaypalEmail"].ToString() && transitem.txn_type!= "reversal")
            {
                if ((transitem.mc_gross == (email_resp.Balance + email_resp.Sum)) && transitem.payment_status == "Completed" && transitem.mc_currency =="USD")
                {
                    PaymentHelper.addPaymentHistory(transitem, inquiryinfo);


                    BookResponseEmail.updateEmailResponseState(transitem.item_number);

                    string msg = "The traveler has paied for the property" + inquiryinfo.PropertyID;

                    BookResponseEmail.sendEmail(owner_info.email, msg, "Transaction Notification");

                    msg = "You have paied for the property" + inquiryinfo.PropertyID;
                    BookResponseEmail.sendEmail(traveler_info.email, msg, "Transaction Notification");

                    msg = "The traveler has paid for the Property" + inquiryinfo.PropertyID;
                    BookResponseEmail.sendEmail("prop@vacations-abroad.com", msg, "Transaction Notification");

                }

            }
        }
        else
        {

        }

    }
    protected void saveLog()
    {
        transitem = new Transaction_Item();

        PropertyInfo[] props = transitem.GetType().GetProperties();

        foreach(PropertyInfo prop in props)
        {
            prop.SetValue(transitem, Convert.ChangeType(Request[prop.Name], prop.PropertyType), null);
        }
        
/*
        int item_number = Convert.ToInt32(Request["item_number"]);
        decimal mc_gross = Convert.ToDecimal(Request["mc_gross"]);
        decimal mc_fee = Convert.ToDecimal(Request["mc_fee"]);
        string txn_id = Request["txn_id"];
        string paydate = Request["payment_date"];
        string business = Request["business"];
        string payer_email = Request["payer_email"];
        string payer_id = Request["payer_id"];
        string mc_currency = Request["mc_currency"];
        string txn_type = Request["txn_type"];
        string payment_status = Request["payment_status"];
        string payment_type = Request["payment_type"];
        string pending_reason = Request["pending_reason"];
        string item_name = Request["item_name"];

    */

        email_resp = BookResponseEmail.getResponseInfo(transitem.item_number); //respid
       // if (email_resp.ID == 0 || email_resp.IsValid < 1) Response.Redirect("/Error.aspx?error=Wrong Response number or not valid");

        inquiryinfo = BookDBProvider.getQuoteInfo(email_resp.QuoteID);
        countryinfo = BookDBProvider.getCountryInfo(inquiryinfo.PropertyID);
        owner_info = BookDBProvider.getUserInfo(inquiryinfo.PropertyOwnerID);
        traveler_info = BookDBProvider.getUserInfo(inquiryinfo.UserID);
        prop_info = BookDBProvider.getPropertyInfo(inquiryinfo.PropertyID);

        PaymentHelper.addPaymentLog(transitem);

    }
}
