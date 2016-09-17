using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Net.Mail;
using System.Collections.Generic;

public partial class ThankYouInquiry : CommonPage
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (Request.QueryString["redirect"] != null)
            hdnRTC.Value = Request.QueryString["redirect"].ToString();
        Page.Header.Controls.Add(new LiteralControl("<link href='/css/StyleSheetBig4.css' rel='stylesheet' type='text/css'></script>"));
    }
}
