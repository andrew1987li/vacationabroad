using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public class AdminPage : ClosedPage
{
	protected override void OnPreLoad (EventArgs e)
	{
		base.OnPreLoad (e);

		if (!AuthenticationManager.IfAdmin)
			Response.Redirect (CommonFunctions.PrepareURL ("Login.aspx?BackLink=" +
				HttpUtility.UrlEncode (Request.Url.ToString ())), true);
	}
}
