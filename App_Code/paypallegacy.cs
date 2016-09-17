using System;
using System.Data;
using System.Configuration;
using System.Web;
using System;
using System.Data;
using System.Configuration;
using System.ComponentModel;
using System.Drawing;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using com.paypal.sdk.core;
using com.paypal.sdk.util;

/// <summary>
/// This is to enable the old version to work with the Paypal named value pair, & the newer API, with hopefully only minor changes to the original code.
/// </summary>
public class PayPalFunctions
{
    public static bool PerformPayment(string CreditCardType, string CreditCardNumber, string CVV2, string ExpMonth, string ExpYear,
        string FirstName, string LastName, string Address1, string Address2, string City, string State, string Zip,
        string Country, string CountryCode, decimal Amount, out string Errors)
   

        {
            // LMG - maybe this should be set somewhere else??  4/28
            SetProfile.SessionProfile = SetProfile.CreateAPIProfile(Constants.API_USERNAME, Constants.API_PASSWORD, Constants.API_SIGNATURE, "", "");

            com.paypal.sdk.services.NVPCallerServices caller = PayPalAPI.PayPalAPIInitialize();
            NVPCodec encoder = new NVPCodec();
            encoder["METHOD"] = "DoDirectPayment";
            // encoder["PAYMENTACTION"] = this.Request.QueryString[Constants.PAYMENT_TYPE_PARAM];
            encoder["PAYMENTACTION"] = "Sale";
            encoder["AMT"] = Amount.ToString();
            //encoder["AMT"] = ".01";
            encoder["CREDITCARDTYPE"] = CreditCardType;
            encoder["ACCT"] = CreditCardNumber;
            encoder["EXPDATE"] = ExpMonth + ExpYear;
            encoder["CVV2"] = CVV2;
            encoder["FIRSTNAME"] = FirstName;
            encoder["LASTNAME"] = LastName;
            encoder["STREET"] = Address1;
            encoder["CITY"] = City;
            encoder["STATE"] = State;
            encoder["ZIP"] = Zip;
            encoder["COUNTRYCODE"] = CountryCode;
            encoder["CURRENCYCODE"] = "USD";

            string pStrrequestforNvp = encoder.Encode();
            // Actually Process the Card and put response etc. into pStresponsenvp
            string pStresponsenvp = caller.Call(pStrrequestforNvp);

            NVPCodec decoder = new NVPCodec();
            // 
            decoder.Decode(pStresponsenvp);
            // 
            string strAck = decoder["ACK"];
            Errors = "";
            if (strAck != null && (strAck == "Success" || strAck == "SuccessWithWarning"))
            {
                // Card passes, YES  (should we do a result=yes?  (note: result = bool)
                string pStrResQue = "TRANSACTIONID=" + decoder["TRANSACTIONID"] + "&" +
                    "AMT=" + decoder["AMT"] + "&" +
                    "AVSCODE=" + decoder["AVSCODE"] + "&" +
                    "CVV2MATCH=" + decoder["CVV2MATCH"];
                return (true);
//                Response.Redirect("paymentreceipt.aspx?" + pStrResQue);
            }
            else
            {
                // Card does not pass, or Paypal has an error.
                string pStrError = "ErrorCode=" + decoder["L_ERRORCODE0"] + "&" +
                    "Desc=" + decoder["L_SHORTMESSAGE0"] + "&" +
                    "Desc2=" + decoder["L_LONGMESSAGE0"];
                Errors = pStrError;
                return (false);
  //              Response.Redirect("PayPalAPIError.aspx?" + pStrError);
            }

        }
    
}
