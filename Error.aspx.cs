using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Error : System.Web.UI.Page
{
    public string error_string="";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["error"] != null) error_string = Request.QueryString["error"];
    }
}