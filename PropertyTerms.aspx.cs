using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class TourTerms : System.Web.UI.Page
{
    public string country = "";
    public string state = "";
    public string city = "";
    public string county = "";

    protected void Page_Load(object sender, EventArgs e)
    {
            if (Session["country"] != null)
                country = Session["country"].ToString();
            if (Session["state"] != null)
                state = Session["state"].ToString();
            if (Session["county"] != null)
                county = Session["county"].ToString();
            if (Session["city"] != null)
                city = Session["city"].ToString();
        
        HtmlMeta description = new HtmlMeta();
        HtmlHead head = Page.Header;

        description.Name = "description";
        description.Content = Description.Text.Replace("%country%", country).
            Replace("%stateprovince%", state).Replace("%city%", city);

        head.Controls.Add(description); 

        DataBind();
        Page.Header.Controls.Add(new LiteralControl("<link href='/css/StyleSheetBig4.css' rel='stylesheet' type='text/css'></script>"));

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (chkAgree.Checked == false)
            lblInfo.Text = "Cannot create property.  Terms not checked.";
        else
        {
            Response.Redirect("accountinformation.aspx");
        }
    }
    public string GetTitle()
    {
        string vValue = "Add a " + city + " vacation property to our " + city + " vacation rental directory.";
        return vValue;
    }
}
