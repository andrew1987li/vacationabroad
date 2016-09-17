using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class NewsletterUnsubscribe : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["email"] != null)
        {
            //lblInfo.Text = Request.QueryString["email"].ToString();
            DBConnection obj = new DBConnection();
            try
            {
                VADBCommander.NewsletterEmailDeleteByEmail(Request.QueryString["email"].ToString());
                VADBCommander.OwnerWarningDeleteByEmail(Request.QueryString["email"].ToString());
                Response.Redirect(CommonFunctions.PrepareURL("OwnerEmailAdmin.aspx"));
            }
            catch (Exception ex) { lblInfo.Text = ex.Message; }
            finally { obj.CloseConnection(); }
        }
    }
}
