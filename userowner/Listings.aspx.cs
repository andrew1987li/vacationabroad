using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class userowner_Listing : CommonPage
{
    public UserInfo userinfo;
    public DataSet inquiry_set, traveler_inquery_set;
    public DataSet property_set;
    public DataSet owner_response_set, traveler_response_set;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!AuthenticationManager.IfAuthenticated) FormsAuthentication.SignOut();

        userinfo = BookDBProvider.getUserInfo(userid);

        inquiry_set = BookDBProvider.getInquiryInfoSet(userid,0);
        traveler_inquery_set = BookDBProvider.getInquiryInfoSet(userid, 1);

        property_set = BookDBProvider.getPropertySet(userid);
        
        owner_response_set = BookResponseEmail.getResponseInfoSet(userid, 0); //0:User=> owner
        traveler_response_set = BookResponseEmail.getResponseInfoSet(userid, 1);
    }

    protected void ListProperty_Click(object sender, EventArgs e)
    {
        Response.Redirect(CommonFunctions.PrepareURL("EditProperty.aspx?UserID=" + userid.ToString(), "*User* Listings"));
    }
    protected void ListTour_Click(object sender, EventArgs e)
    {
        Response.Redirect(CommonFunctions.PrepareURL("EditTour.aspx?UserID=" + userid.ToString(), "*User* Listings"));
    }

    protected void OurCommision_Click(object sender, EventArgs e)
    {
        Response.Redirect(CommonFunctions.PrepareURL("AgentAccount.aspx?UserID=" + userid.ToString(), "*User* Account"));
    }

    protected void bt_payment_Command(object sender, CommandEventArgs e)
    {
        //CommonFunctions.PrepareURL ("MakePayment.aspx?UserID=" + userid.ToString () + "&PropertyID=" + DataBinder.Eval(Container.DataItem, "ID", "{0:d}") + "&InvoiceID=-1", "*User* Listings"
        Button btn = (Button)(sender);
        string yourValue = btn.CommandArgument;
    }

    protected void bt_edittxt_Command(object sender, CommandEventArgs e)
    {
        //CommonFunctions.PrepareURL ("EditProperty.aspx?UserID=" + userid.ToString () + "&PropertyID=" + DataBinder.Eval(Container.DataItem, "ID", "{0:d}"), "*User* Listings")
    }

    protected void bt_editphoto_Command(object sender, CommandEventArgs e)
    {
        // CommonFunctions.PrepareURL ("PropertyPhotos.aspx?UserID=" + userid.ToString () + "&PropertyID=" + DataBinder.Eval(Container.DataItem, "ID", "{0:d}"), "*User* Listings") 
    }

    protected void bt_calendar_Command(object sender, CommandEventArgs e)
    {
        //CommonFunctions.PrepareURL ("PropertyCalendar.aspx?UserID=" + userid.ToString () + "&PropertyID=" + DataBinder.Eval(Container.DataItem, "ID", "{0:d}"), "*User* Listings")
    }
}