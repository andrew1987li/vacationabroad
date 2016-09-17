using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Moneybook : System.Web.UI.Page
{
    public int vAmount = 125;
    protected void Page_Load(object sender, EventArgs e)
    {
        string vValue = "<form action=\"https://www.moneybookers.com/app/payment.pl\" method=\"post\" target=\"_blank\">"+
//"<input type=\"hidden\" name=\"pay_to_email\" value=\"ar@vacations-abroad.com\"/>" +
//"<input type=\"hidden\" name=\"status_url\" value=\"ar@vacations-abroad.com\"/> " +
//"<input type=\"hidden\" name=\"language\" value=\"EN\"/>"+
//"<input type=\"hidden\" name=\"amount\" value=\"1\"/>"+
//"<input type=\"hidden\" name=\"currency\" value=\"USD\"/>"+
//"<input type=\"hidden\" name=\"detail1_description\" value=\"Reservation\"/>"+
//"<input type=\"hidden\" name=\"detail1_text\" value=\"Reservation for J. Jones\"/>"+
//"<input type=\"submit\" value=\"Pay!\"/>" +
"</form> ";

        divContent.InnerHtml = vValue;
        DataBind();
    }
}
