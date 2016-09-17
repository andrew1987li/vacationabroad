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
        //As long as there is a match for each email in the users table, leave
        //...but if a row has no match in users(edited by owner), remove from warning table, remove before displaying
        //if old doesn't exist in users table but does exist in emails table, update emails table w/good email & remove
        //from warning table
        DBConnection obj = new DBConnection();
        string query = "";
        try
        {
            DataTable dt = VADBCommander.OwnerWarningList();
            if (dt.Rows.Count > 0)
            {
                for (int i = dt.Rows.Count - 1; i >= 0; i--)
                {
                    DataTable dt2 = VADBCommander.UserInd(dt.Rows[i]["ownerID"].ToString());
                    if (dt2.Rows.Count > 0)
                    {
                        //if (dt2.Rows[0]["email"].ToString() != dt.Rows[i]["email"].ToString())
                        //{
                        //    query = "select * from newsletterEmails where email='" + dt.Rows[i]["email"].ToString() + "'";
                        //    DataTable dt3 = obj.G --deleted --etDataSet(query);
                        //    if (dt3.Rows.Count > 0)
                        //    {
                        //        string ID2 = dt3.Rows[0]["id"].ToString();                        
                        
                        //    }
                        //}
                    }
                }

                grdComments.DataSource = dt;
                grdComments.DataBind();
            }
        }
        catch (Exception ex) { lblInfo.Text = ex.Message; }
        finally { obj.CloseConnection(); }
    }
    protected void grdComments_DataBound(object sender, EventArgs e)
    {
       
        DBConnection obj = new DBConnection();
        DataTable dt = new DataTable();
        try
        {
            dt = obj.spSelectAllComments();

            foreach (GridViewRow row in grdComments.Rows)
            {
                HyperLink lnk = (HyperLink)row.FindControl("hlkOwner");
                if (lnk != null)
                {
                    int vID = Convert.ToInt32(lnk.Text);
                    lnk.Target = "_blank";

                    //dt.DefaultView.RowFilter = "propid = " + vID;

                    //if (dt.DefaultView.ToTable().Rows.Count > 0)
                    //{
                    string url = CommonFunctions.GetSiteAddress() + "/ownerinformation.aspx?userid=" +
                        vID.ToString();
                    lnk.NavigateUrl = url.ToLower().Replace(' ', '_');
                    lnk.Text = "Owner";
                    //    }
                }
                //Label lbl = (Label)row.FindControl("Label1");
                //if (lbl != null)
                //    lbl.Text = HttpUtility.HtmlEncode(lbl.Text);
            }
        }
        catch (Exception ex) { lblInfo.Text = ex.Message; }
        finally { obj.CloseConnection(); }
    }
}
