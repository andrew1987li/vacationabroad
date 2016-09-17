using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class CampaignDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DBConnection obj = new DBConnection();
            //query by month and year
            try
            {
                if ((Request.QueryString["month"] != null) && (Request.QueryString["year"] != null))
                {
                    int year = Convert.ToInt32(Request.QueryString["year"]);
                    int month = 0;

                    switch (Request.QueryString["month"].ToString())
                    {
                        case "january":
                            month = 1;
                            break;
                        case "february":
                            month = 2;
                            break;
                        case "march":
                            month = 3;
                            break;
                        case "april":
                            month = 4;
                            break;
                        case "may":
                            month = 5;
                            break;
                        case "june":
                            month = 6;
                            break;
                        case "july":
                            month = 7;
                            break;
                        case "august":
                            month = 8;
                            break;
                        case "september":
                            month = 9;
                            break;
                        case "october":
                            month = 10;
                            break;
                        case "november":
                            month = 11;
                            break;
                        case "december":
                            month = 12;
                            break;
                        default:
                            Response.Write("No data.");
                            break;
                    }
                    DataTable dt = new DataTable();
                    dt = obj.spSelectCampaignDetail(month, year);
                    grdDetails.DataSource = dt;
                    grdDetails.DataBind();
                }
            }
            catch (Exception ex) { lblInfo.Text = ex.Message; }
            finally { obj.CloseConnection(); }
        }
    }
    protected void grdDetails_DataBound(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DBConnection obj = new DBConnection();
            try
            {
                foreach (GridViewRow row in grdDetails.Rows)
                {
                    HyperLink link = (HyperLink)row.FindControl("hlkPropNum");
                    int vPropNum = Convert.ToInt32(link.Text);
                    //get city, state, country by propID
                    DataTable dt = obj.spSelectLocationInfoByPropID(vPropNum);
                    //property may be deleted but still credit inquiry in drawing
                    if (dt.Rows.Count > 0)
                    {
                        string vText = CommonFunctions.GetSiteAddress() + "/" + dt.Rows[0]["country"].ToString() + "/" +
                            dt.Rows[0]["stateprovince"].ToString() + "/" + dt.Rows[0]["city"].ToString() + "/" +
                            vPropNum.ToString() + "/default.aspx";
                        vText = vText.Replace(" ", "_").ToLower();

                        link.NavigateUrl = vText;
                    }
                }
            }
            catch (Exception ex) { lblInfo.Text = ex.Message; }
            finally { obj.CloseConnection(); }
        }
    }

    protected void chkWinnerChanged(object sender, EventArgs e)
    {
        DBConnection obj = new DBConnection();
        try
        {
            CheckBox chkSample = sender as CheckBox;
            GridViewRow row = chkSample.NamingContainer as GridViewRow;
            Label lblId = row.FindControl("lblID") as Label;

            int id = Convert.ToInt32(lblId.Text);
            //update campaign table for selected user
            obj.spUpdateCampaignDetail(id, chkSample.Checked);
        }
        catch (Exception ex) { lblInfo.Text = ex.Message; }
        finally { obj.CloseConnection(); }
    }
}
