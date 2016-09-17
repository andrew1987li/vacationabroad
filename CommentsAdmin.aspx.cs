using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class CommentsAdmin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DBConnection obj = new DBConnection();
        try
        {
            //DataTable dt = obj.spSelectAllComments();
            //if (dt.Rows.Count > 0)
            //{
            //    grdComments.DataSource = dt;
            //    grdComments.DataBind();
            //}
        }
        catch (Exception ex) { lblInfo.Text = ex.Message; }
        finally { obj.CloseConnection(); }
    }
    protected void grdComments_DataBound(object sender, EventArgs e)
    {
        DBConnection obj = new DBConnection();
        DataTable dt = new DataTable();
        dt = obj.spSelectAllComments();

        foreach (GridViewRow row in grdComments.Rows)
        {
            HyperLink lnk = (HyperLink)row.FindControl("hlkProp");
            if (lnk != null)
            {
                int vID = Convert.ToInt32(lnk.Text);
                lnk.Target = "_blank";

                dt.DefaultView.RowFilter = "propid = " + vID;

                if (dt.DefaultView.ToTable().Rows.Count > 0)
                {
                    string url = CommonFunctions.GetSiteAddress() + "/" + dt.DefaultView.ToTable().Rows[0]["country"].ToString() + "/" +
                                    dt.DefaultView.ToTable().Rows[0]["stateprovince"].ToString() + "/" + dt.DefaultView.ToTable().Rows[0]["city"].ToString() + "/" +
                                    vID.ToString() + "/default.aspx";
                    lnk.NavigateUrl = url.ToLower().Replace(' ', '_');
                }
            }
            Label lbl = (Label)row.FindControl("Label1");
            if (lbl != null)
                lbl.Text = HttpUtility.HtmlEncode(lbl.Text);
        }
    }
}
