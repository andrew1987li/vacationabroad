using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public class CommonPage : Page
{
	protected int userid = -1;
	protected int propertyid = -1;
	protected int auctionid = -1;
	protected string backlinkurl = CommonFunctions.PrepareURL ("default.aspx");
	protected string backlinktext = "Return to previous page";
	protected bool backlinkpassed = false;

	protected override void OnPreLoad (EventArgs e)
	{
		base.OnPreLoad (e);
         if ((Request.Params["UserID"] != null) && (Request.Params["UserID"].Length > 0) )
			try
			{
                userid = Convert.ToInt32 (Request.Params["UserID"]);
               // Response.Write("Uid exe" + userid);
            }
			catch (Exception)
			{
			}
		else
			userid = AuthenticationManager.UserID;

           if (Request.Params["BackLink"] != null)
		{
			backlinkpassed = true;

			backlinkurl = Request.Params["BackLink"];

			if (Request.Params["BackLinkText"] != null)
				backlinktext = Request.Params["BackLinkText"];
			else if (userid != -1)
				backlinktext = "Return to previous page";
		}
        else if (Request.Params["ReturnUrl"] != null)
        {
            backlinkpassed = true;

            backlinkurl = Request.Params["ReturnUrl"];

        }
		else if (userid != -1)
		{
			backlinkurl = CommonFunctions.PrepareURL ("MyAccount.aspx?UserID=" + userid.ToString ());
			backlinktext = "Return to *User* Account page";
		}
		backlinktext = System.Web.HttpUtility.UrlDecode (backlinktext);

		if (userid == AuthenticationManager.UserID)
			backlinktext = backlinktext.Replace ("*User*", "My");
		else
			backlinktext = backlinktext.Replace ("*User*", "User");

		SetBackLink (Controls, backlinkurl, backlinktext);

		if ((Request.Params["PropertyID"] != null) && (Request.Params["PropertyID"].Length > 0))
			try
			{
				propertyid = Convert.ToInt32 (Request.Params["PropertyID"]);
			}
			catch (Exception)
			{
			}

		if ((Request.Params["AuctionID"] != null) && (Request.Params["AuctionID"].Length > 0))
			try
			{
				auctionid = Convert.ToInt32 (Request.Params["AuctionID"]);
			}
			catch (Exception)
			{
			}
	}

	private bool SetBackLink (ControlCollection Controls, string URL, string Text)
	{
		foreach (Control control in Controls)
			if ((control is HyperLink) && (string.Compare (control.ID, "backlink", true) == 0))
			{
				((HyperLink)control).Text = backlinktext;
				((HyperLink)control).NavigateUrl = backlinkurl;
				((HyperLink)control).Visible = backlinkpassed;
				return true;
			}
			else if (control.Controls.Count > 0)
				if (SetBackLink (control.Controls, URL, Text))
					return true;

		return false;
	}
}
