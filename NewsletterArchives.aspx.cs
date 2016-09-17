using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class NewsletterArchives : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DBConnection obj = new DBConnection();
        DataTable dt = new DataTable();
        string query = "";

        try
        {
            if (Request.QueryString["item"] == null)
            {
                hlkReturn.Visible = false;

                dt = VADBCommander.NewsLettersByDeployedList();
                if (dt.Rows.Count > 0)
                {
                    grdNewsletter.DataSource = dt;
                    grdNewsletter.DataBind();
                }
                else
                    divGrid.InnerHtml = "No Data to Display";
            }
            else
            {
                grdNewsletter.Visible = false;
                dt = VADBCommander.NewsLetterByID(Request.QueryString["item"].ToString());
                divContent.InnerHtml = dt.Rows[0]["content"].ToString();
            }
            Page.Header.Controls.Add(new LiteralControl("<link href='/css/StyleSheetBig4.css' rel='stylesheet' type='text/css'></script>"));

        }
        catch (Exception ex) { lblInfo.Text = ex.Message; }
        finally { obj.CloseConnection(); }
    }
}
