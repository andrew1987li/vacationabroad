using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public class ClosedPageFindOwner : CommonPage
{
	protected override void OnPreLoad (EventArgs e)
	{
		base.OnPreLoad (e);

		if (!AuthenticationManager.IfAuthenticated)
			Response.Redirect (CommonFunctions.PrepareURL ("FindOwner.aspx?BackLink=" +
				HttpUtility.UrlEncode (Request.Url.ToString ())), true);

		if ((userid != AuthenticationManager.UserID) && !AuthenticationManager.IfAdmin)
			Response.Redirect (CommonFunctions.PrepareURL ("FindOwner.aspx?BackLink=" +
				HttpUtility.UrlEncode (Request.Url.ToString ())), true);
	}
}
