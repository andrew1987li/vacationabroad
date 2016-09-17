using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Unsubscribe : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["email"] != null)
        {
            txtEmail.Text = Request.QueryString["email"].ToString();
            lblTitle.Text = "Unsubscribe from List";
            btnAdd.Visible = false;
            //lblName.Visible = false;
            //txtName.Visible = false;
        }
        else
        {
            btnSubmit.Text = "Remove from List";
            lblTitle.Text = "Subscribe to List";
        }
        Page.Header.Controls.Add(new LiteralControl("<link href='/css/StyleSheetBig4.css' rel='stylesheet' type='text/css'></script>"));

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    { //Remove
        DBConnection obj = new DBConnection();
        DataTable dt = new DataTable();

        try
        {
            dt = VADBCommander.NewsletterEmailByEmail(txtEmail.Text);

            if (dt.Rows.Count > 0)
            {
                VADBCommander.NewsLetterEmailOptOut(txtEmail.Text);
                lblInfo.Text = "Email removed.";
            }
            else
            {
                lblInfo.Text = "Email not found";
            }
        }
        catch (Exception ex) { lblInfo.Text = ex.Message; }
        finally { obj.CloseConnection(); }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        DBConnection obj = new DBConnection();
        DataTable dt = new DataTable();
        try
        {
            dt = VADBCommander.NewsletterEmailByEmail(txtEmail.Text);

            if (dt.Rows.Count > 0)
                lblInfo.Text = "Email already exists";
            else
            {
                dt = VADBCommander.NewsLetterEmailBySingleEmail(txtEmail.Text);
                string query = "";
                if (dt.Rows.Count == 0)
                {
                    VADBCommander.NewsLetterEmailAdd(txtEmail.Text);
                }
                else
                {
                    VADBCommander.NewsLetterEmailSetOptOutNull(txtEmail.Text);
                }
                lblInfo.Text = "Email added";
            }


        }
        catch (Exception ex) { lblInfo.Text = ex.Message; }
        finally { obj.CloseConnection(); }

    }
}
