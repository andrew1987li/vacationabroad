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
    public DataSet inquiry_set;
    public DataSet property_set;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!AuthenticationManager.IfAuthenticated) FormsAuthentication.SignOut();

        userinfo = BookDBProvider.getUserInfo(userid);

        inquiry_set = BookDBProvider.getInquiryInfoSet(userid);
        property_set = BookDBProvider.getPropertySet(userid);
    }

    protected void ListProperty_Click(object sender, EventArgs e)
    {

    }
    protected void ListTour_Click(object sender, EventArgs e)
    {

    }

    protected void OurCommision_Click(object sender, EventArgs e)
    {

    }

    protected void bt_payment_Command(object sender, CommandEventArgs e)
    {

    }

    protected void bt_edittxt_Command(object sender, CommandEventArgs e)
    {

    }

    protected void bt_editphoto_Command(object sender, CommandEventArgs e)
    {

    }

    protected void bt_calendar_Command(object sender, CommandEventArgs e)
    {

    }
}