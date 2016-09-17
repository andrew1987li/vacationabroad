using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.OleDb;
using System.Data.SqlClient;

public partial class NewsletterList : System.Web.UI.Page
{
    public bool gComplete = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        DBConnection obj = new DBConnection();
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();

        try
        {
            dt = VADBCommander.NewsLetterOrderByIDList();
            grdNews.DataSource = dt;
            grdNews.DataBind();

        }
        catch (Exception ex) { lblError.Text = ex.Message; }
        obj.CloseConnection();
    }
    protected void grdNews_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["id"] = grdNews.SelectedValue.ToString();
        Response.Redirect("Newsletter.aspx?id=" + grdNews.SelectedValue.ToString());
    }
    protected void grdNews_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (!IsPostBack)
        {
            DBConnection obj = new DBConnection();

            try
            {
                VADBCommander.NewsletterDelete(grdNews.DataKeys[e.RowIndex].Value.ToString());
                DataTable dt5 = VADBCommander.NewsletterList();;
                grdNews.DataSource = dt5;
                grdNews.DataBind();
            }
            catch (Exception ex) { Response.Write(ex.Message); }
            obj.CloseConnection();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("Newsletter.aspx");
    }
    protected void grdNews_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Label lblEmailID = (Label)e.Row.FindControl("lblEmailID");
        Label lblDeployed = (Label)e.Row.FindControl("lblDeployed");
        Label lblComplete = (Label)e.Row.FindControl("lblComplete");

        string vID = "";
        if (lblDeployed != null)
        {
            if (lblDeployed.Text == "Yes")
            {
                DBConnection obj = new DBConnection();
                DataTable dt = new DataTable();
                if(lblEmailID != null)
                    vID = lblEmailID.Text;
                string complete = "";
                //find index of ID out of total for query of possible emails
                try
                {
                    dt = VADBCommander.NewsLetterEmailOptOutIsNullList();

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (dt.Rows[i]["id"].ToString() == vID)
                                lblEmailID.Text = dt.Rows[i]["numeral"].ToString();
                        }
                        complete = lblEmailID.Text;

                        if (lblComplete != null)
                            if (lblComplete.Text == "True")
                                complete = dt.Rows.Count.ToString();

                      lblEmailID.Text = complete + "/" + dt.Rows.Count.ToString();
                        
                    }
                }
                catch (Exception ex) { lblError.Text = ex.Message; }
                finally { obj.CloseConnection(); }
            }
        }
    }
}
