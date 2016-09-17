using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ToolTip : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Header.Controls.Add(new LiteralControl("<script src='http://vacations-abroad.com/scripts/jquery.ezpz_tooltip.js' type='text/javascript'></script>"));
    }
}
