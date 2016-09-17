using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

public partial class PropertyReview : System.Web.UI.Page
{
    public int propNum = 0;
    public int stateprovinceid = 0;
    public int cityID = 0;
    public int countyID = 0;
    public string county = "";
    public string city = "";
    public string state = "";
    public string country = "";

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
                            if (dt.Rows[0]["name2"] != DBNull.Value)                            
                                lblTitle.Text += dt.Rows[0]["name2"].ToString();
                            else
                                lblTitle.Text = dt.Rows[0]["name"].ToString();

                            lblAddress.Text = dt.Rows[0]["address"].ToString();

                            //hlkCity.Text = dt.Rows[0]["city"].ToString();
                            //hlkState.Text = dt.Rows[0]["stateprovince"].ToString();
                            //hlkCountry.Text = dt.Rows[0]["country"].ToString();

                            stateprovinceid = Convert.ToInt32(dt.Rows[0]["stateprovinceid"].ToString());

                            city = dt.Rows[0]["city"].ToString();
                            state = dt.Rows[0]["stateprovince"].ToString();
                            country = dt.Rows[0]["country"].ToString();
                            cityID = Convert.ToInt32(dt.Rows[0]["cityid"]);
                            if(dt.Rows[0]["countyid"] != DBNull.Value)
                            countyID = Convert.ToInt32(dt.Rows[0]["countyid"]);

                            if (dt.Rows[0]["county"] != DBNull.Value)
                                county = dt.Rows[0]["county"].ToString();

                            string url = CommonFunctions.GetSiteAddress() + "/" + dt.Rows[0]["country"].ToString() + "/" +
                                dt.Rows[0]["stateprovince"].ToString() + "/" + dt.Rows[0]["city"].ToString() + "/" +
                                Request.QueryString["propID"].ToString() + "/default.aspx";
                            Session["commentsRedirect"] = url;

                            hlkPropNum.Text = "Return to Property #" + Request.QueryString["propID"].ToString();
                            hlkPropNum.NavigateUrl = url.Replace(' ', '_').ToLower();

                            url = CommonFunctions.GetSiteAddress() + "/" + dt.Rows[0]["country"].ToString() + "/" +
                                dt.Rows[0]["stateprovince"].ToString() + "/" + dt.Rows[0]["city"].ToString() +
                                "/default.aspx";
                            //hlkCity.NavigateUrl = url.Replace(' ', '_').ToLower();

                            url = CommonFunctions.GetSiteAddress() + "/" + dt.Rows[0]["country"].ToString() + "/" +
                                dt.Rows[0]["stateprovince"].ToString() +
                                "/default.aspx";
                            //hlkState.NavigateUrl = url.Replace(' ', '_').ToLower();

                            url = CommonFunctions.GetSiteAddress() + "/" + dt.Rows[0]["country"].ToString() +
                                "/default.aspx";
                            //hlkCountry.NavigateUrl = url.Replace(' ', '_').ToLower();
                            Page.Header.Controls.Add(new LiteralControl("<link href='/css/StyleSheetBig4.css' rel='stylesheet' type='text/css'></script>"));

                            DataBind();
                        }
                    }
                }
            }
            catch (Exception ex) { lblInfo.Text = ex.Message; }
            finally { obj.CloseConnection(); }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            DBConnection obj = new DBConnection();
            try
            {
                if (Request.QueryString["propID"] != null)
                {
                    if (obj.IsNumeric(Request.QueryString["propID"].ToString()))
                    {
                        DateTime dTime = new DateTime();
                        if ((ddlMonth.SelectedIndex != 0) && (ddlYear.SelectedIndex != 0))
                            dTime = Convert.ToDateTime(ddlMonth.SelectedValue + "/01/" +
                                ddlYear.SelectedValue);
                        else
                            dTime = DateTime.Now;

                        int rating = 0;
                        if (rbnOne.Checked == true)
                            rating = 1;
                        else if (rbnTwo.Checked == true)
                            rating = 2;
                        else if (rbnThree.Checked == true)
                            rating = 3;
                        else if (rbnFour.Checked == true)
                            rating = 4;

                        //filter input
                        bool blnAllow = true;

                        if ((txtFName.Text.Contains("http")) || (txtFName.Text.Contains("www")) || (txtFName.Text.Contains("href")))
                            blnAllow = false;
                        if ((txtLName.Text.Contains("http")) || (txtLName.Text.Contains("www")) || (txtLName.Text.Contains("href")))
                            blnAllow = false;
                        if ((txtComments.Text.Contains("http")) || (txtComments.Text.Contains("www")) || (txtComments.Text.Contains("href")))
                            blnAllow = false;

                        if (blnAllow == true)
                        {
                            obj.spInsertComment(Convert.ToInt32(Request.QueryString["propID"]), txtFName.Text, txtLName.Text,
                                txtComments.Text, dTime, false, rating, DateTime.Today, txtPhone.Text);

                            if (Session["commentsRedirect"] != null)
                            {
                                Response.Redirect(Session["commentsRedirect"].ToString().Replace(' ', '_').ToLower());
                                Session.Remove("commentsRedirect");
                            }
                        }
                        else
                            lblInfo.Text = "No HTML markup allowed.  Please use regular text.";
                    }
                }
            }
            catch (Exception ex) { lblInfo.Text = ex.Message; }
            finally { obj.CloseConnection(); }
        }
    }
    public string GetTitle()
    {
        string vTitle = "";
        if ((propNum != null) && (propNum != 0))
        {
            vTitle = "Post Review for Vacation Rental Property #" + propNum.ToString() + ", " + city +
                ", " + country;
        }
        else
            vTitle = "Post Review";

        return vTitle;
    }
   
}
