using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class PropertyReviewRead : System.Web.UI.Page
{
    public int propNum = 0;
    public string city = "";
    public string stateprovince = "";
    public string country = "";
    public int stateprovinceid = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DBConnection obj = new DBConnection();
            try
            {
                if (Request.QueryString["propID"] != null)
                {
                    if (obj.IsNumeric(Request.QueryString["propID"]))
                    {
                        DataTable dt = obj.spSelectSingleProperty(Convert.ToInt32(Request.QueryString["propID"]));

                        if (dt.Rows.Count > 0)
                        {
                            propNum = Convert.ToInt32(Request.QueryString["propID"]);
                            imgProperty.ImageUrl = CommonFunctions.GetSiteAddress() + "/images/" +
                                dt.Rows[0]["photoImage"].ToString();
                            if (dt.Rows[0]["name2"] != null)
                                lblTitle.Text += dt.Rows[0]["name2"].ToString();
                            else
                                lblTitle.Text = "";

                            lblAddress.Text = dt.Rows[0]["address"].ToString();

                            hlkCity.Text = dt.Rows[0]["city"].ToString() + " Vacation Rentals";

                            //hlkState.Text = dt.Rows[0]["stateprovince"].ToString() + "Vacation Rentals";
                            //hlkCountry.Text = dt.Rows[0]["country"].ToString() + "Vacation Rentals";

                            city = dt.Rows[0]["city"].ToString();
                            stateprovince = dt.Rows[0]["stateprovince"].ToString();
                            country = dt.Rows[0]["country"].ToString();
                            stateprovinceid = Convert.ToInt32(dt.Rows[0]["stateprovinceid"].ToString());

                            string url = CommonFunctions.GetSiteAddress() + "/" + dt.Rows[0]["country"].ToString() + "/" +
                                dt.Rows[0]["stateprovince"].ToString() + "/" + dt.Rows[0]["city"].ToString() + "/" +
                                Request.QueryString["propID"].ToString() + "/default.aspx";
                            Session["commentsRedirect"] = url;

                            hlkPropNum.Text = "Property #" + Request.QueryString["propID"].ToString();
                            hlkPropNum.NavigateUrl = url.Replace(' ', '_').ToLower();

                            url = CommonFunctions.GetSiteAddress() + "/" + dt.Rows[0]["country"].ToString() + "/" +
                                dt.Rows[0]["stateprovince"].ToString() + "/" + dt.Rows[0]["city"].ToString() +
                                "/default.aspx";
                            hlkCity.NavigateUrl = url.Replace(' ', '_').ToLower();

                            //url = CommonFunctions.GetSiteAddress() + "/" + dt.Rows[0]["country"].ToString() + "/" +
                            //    dt.Rows[0]["stateprovince"].ToString() +
                            //    "/default.aspx";
                            //hlkState.NavigateUrl = url.Replace(' ', '_').ToLower();

                            //url = CommonFunctions.GetSiteAddress() + "/" + dt.Rows[0]["country"].ToString() +
                            //    "/default.aspx";
                            //hlkCountry.NavigateUrl = url.Replace(' ', '_').ToLower();
                            PopulateRepeater();
                            Page.Header.Controls.Add(new LiteralControl("<link href='/css/StyleSheetBig4.css' rel='stylesheet' type='text/css'></script>"));

                            DataBind();
                        }
                    }
                }
                else
                    lblInfo.Text = "empty";
            }
            catch (Exception ex) { lblInfo.Text = ex.Message + "82"; }
            finally { obj.CloseConnection(); }
        }
    }
    
    protected void PopulateRepeater()
    {
        DBConnection obj = new DBConnection();
        try
        {
            DataTable dt = new DataTable();
            dt = obj.spSelectCommentExist(propNum);
            if (dt.Rows.Count > 0)
            {
                rptReviews.DataSource = dt;
                rptReviews.DataBind();
            }
        }
        catch (Exception ex) { lblInfo.Text = ex.Message + "127"; }
        finally { obj.CloseConnection(); }
    }
    public string GetTitle()
    {
        string vTitle = "Reviews for Vacation Rentals Property #" + propNum.ToString();

        return vTitle;
    }
    protected void rptReviews_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DBConnection obj = new DBConnection();
        try
        {
            Label lbl = (Label)e.Item.FindControl("lblStars");
            if (lbl != null)
            {
                int rating = Convert.ToInt32(lbl.Text);

                if (rating == 1)
                    lbl.Text = "<img src=\"/images/star2.gif\" />";
                else if (rating == 2)
                    lbl.Text = "<img src=\"/images/star2.gif\" /><img src=\"/images/star2.gif\" />";
                else if (rating == 3)
                    lbl.Text = "<img src=\"/images/star2.gif\" /><img src=\"/images/star2.gif\" /><img src=\"/images/star2.gif\" />";
                else if (rating == 4)
                    lbl.Text = "<img src=\"/images/star2.gif\" /><img src=\"/images/star2.gif\" /><img src=\"/images/star2.gif\" /><img src=\"/images/star2.gif\" />";
            }
            }
        catch (Exception ex) { lblInfo.Text = ex.Message + "153"; }
        finally { obj.CloseConnection(); }
    }
}
