using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class userowner_Listing : CommonPage
{
    public UserInfo userinfo;
    public List<PropertyInform> list_inquiry= ;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!AuthenticationManager.IfAuthenticated) FormsAuthentication.SignOut();

        userinfo = BookDBProvider.getUserInfo(userid);
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