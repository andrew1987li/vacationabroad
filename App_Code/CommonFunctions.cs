using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using System.Net.Mail;

public class CommonFunctions
{
    public static SqlConnection GetConnection()
    {
        SqlConnection retVal = null;
        try
        {
            retVal = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        }
        catch (Exception e)
        {

        }

        return retVal;
    }

	public static SqlConnection Connection = new SqlConnection (ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
    
  public static bool  sendEmail(string name , string email)
    {
        Regex regex = new Regex("^\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$");

        // SmtpClient smtpclient = new SmtpClient(ConfigurationManager.AppSettings["SMTPServer"],
        //   int.Parse(ConfigurationManager.AppSettings["SMTPPort"]));
        SmtpClient smtpclient = new SmtpClient("mail.vacations-abroad.com", 25);

        //MailMessage message = new MailMessage (IfShowContactInfo () ?
        //    ContactEmail.Text : "ar@" + CommonFunctions.GetDomainName (), (string)PropertiesFullSet.Tables["Properties"].Rows[0]["Email"]);
        // MailMessage message = new MailMessage("prop@vacations-abroad.com", (string)PropertiesFullSet.Tables["Properties"].Rows[0]["Email"]);


        string emailbody = "New owner registered at " + CommonFunctions.GetSiteName() + ".\n\n" +
            "Owner details:\n" +
            "Login name:" + name + "\n" +
            "Email address:" + email + "\n";

        // MailMessage message = new MailMessage(regex.Match(EmailAddress.Text).Success ?
        //   EmailAddress.Text : "admin@" + CommonFunctions.GetDomainName(), ConfigurationManager.AppSettings["NewOwnerEmail"]);
        MailMessage message = new MailMessage("prop@vacations-abroad.com", ConfigurationManager.AppSettings["NewOwnerEmail"]);
        message.Subject = "New owner registered in the system";
        message.Body = emailbody;
        message.IsBodyHtml = false;

        message.Body = message.Body.Replace("\r", "").Replace("\n", Environment.NewLine);
        message.Headers["Content-Type"] = "text/plain; charset = \"iso-8859-1\"";

        smtpclient.Credentials = new System.Net.NetworkCredential("noreply@vacations-abroad.com", System.Configuration.ConfigurationManager.AppSettings["smtpCredential"].ToString());
        smtpclient.UseDefaultCredentials = false;


        if (regex.Match(message.To.ToString()).Success)
        {
            try
            {
                smtpclient.Send(message);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        return true;
    }
      
    //---------------------
    //PrepareAdapter is called often, simply to get a select statment sent to the db quickly.
	public static SqlDataAdapter PrepareAdapter (SqlConnection Connection, string SelectCommand, params object[] Parameters)
	{
		SqlCommand DBcommand = new SqlCommand (SelectCommand, Connection);
        // Looks like this loop goes though the SelectCommand String, looking for multiple @ (paramenter) characters, to then divide it up.
        int atindex = SelectCommand.IndexOf('@');
		for (int i = 0; atindex != -1; i++)
			if ((i < Parameters.GetLength (0)) && (Parameters[i] is SqlDbType))
			{
				int j;
				for (j = 1; (atindex + j < SelectCommand.Length) &&
					(char.IsLetterOrDigit (SelectCommand[atindex + j]) || (SelectCommand[atindex + j] == '_')); j++);
				string name = SelectCommand.Substring (atindex, j);

				if (!DBcommand.Parameters.Contains (name))
					if ((i + 1 < Parameters.GetLength (0)) && (Parameters[i + 1] is int))
					{
						DBcommand.Parameters.Add (name, (SqlDbType)Parameters[i], (int)Parameters[i + 1]);
						i++;
					}
					else
						DBcommand.Parameters.Add (name, (SqlDbType)Parameters[i]);
				else
					i--;

				atindex = SelectCommand.IndexOf ('@', atindex + 1);
			}
			else
			{
				int j;
				for (j = 1; (atindex + j < SelectCommand.Length) &&
					(char.IsLetterOrDigit (SelectCommand[atindex + j]) || (SelectCommand[atindex + j] == '_')); j++)
					;
				string name = SelectCommand.Substring (atindex, j);

				if (!DBcommand.Parameters.Contains (name))
					throw new ApplicationException ("Please pass parameter type to CommonFunctions.PrepareAdapter");

				atindex = SelectCommand.IndexOf ('@', atindex + 1);
			}

        SqlDataAdapter adapter;
        lock (Connection)
			adapter = new SqlDataAdapter (DBcommand);

		SqlCommandBuilder builder;
		lock (Connection)
			builder = new SqlCommandBuilder (adapter);

		try
		{
			lock (Connection)
				adapter.UpdateCommand = builder.GetUpdateCommand (true);
		}
		catch (Exception)
		{
		}

		try
		{
			lock (Connection)
				adapter.InsertCommand = builder.GetInsertCommand (true);
		}
		catch (Exception)
		{
		}

		try
		{
			lock (Connection)
				adapter.DeleteCommand = builder.GetDeleteCommand (true);
		}
		catch (Exception)
		{
		}

		return adapter;
	}

	public static string PrepareURL (string URL)
	{
        if (HttpContext.Current == null)
            return "/" + URL;
        else
        {
            if (!URL.Contains("Property"))
                URL = URL.ToLower();
            URL = URL.Replace(' ', '_');
            //return (HttpContext.Current.Request.ApplicationPath.EndsWith("/") ?
            //    "http://204.12.125.187/" : "http://204.12.125.187" + "/") + URL;
            return (HttpContext.Current.Request.ApplicationPath.EndsWith("/") ?
                HttpContext.Current.Request.ApplicationPath : HttpContext.Current.Request.ApplicationPath + "/") + URL;

        }
        }


    public  static  string PrepareUrlCheckForNull(string strurl, string strLinkText)
    {
        string strFinal = string.Empty;
        if (!string.IsNullOrEmpty(strurl))
        {
            strFinal = "<a href="+strurl+" target=\"_blank\">"+strLinkText+"</a>";
        }
        return strFinal;
    }
	public static string PrepareURL (string URL, string BackLinkText)
	{
		if (URL.IndexOf ('?') > 0)
			return (HttpContext.Current.Request.ApplicationPath.EndsWith ("/") ?
				HttpContext.Current.Request.ApplicationPath : HttpContext.Current.Request.ApplicationPath + "/") + URL +
				"&BackLink=" + HttpUtility.UrlEncode (HttpContext.Current.Request.Url.ToString ()) + "&BackLinkText=" +
				HttpUtility.UrlEncode ("Return to " + BackLinkText);
		else
			return (HttpContext.Current.Request.ApplicationPath.EndsWith ("/") ?
				HttpContext.Current.Request.ApplicationPath : HttpContext.Current.Request.ApplicationPath + "/") + URL +
				"?BackLink=" + HttpUtility.UrlEncode (HttpContext.Current.Request.Url.ToString ()) + "&BackLinkText=" +
				HttpUtility.UrlEncode ("Return to " + BackLinkText);
	}

	public static void ReadRowValues (DataRow Row, ControlCollection Controls)
	{
		foreach (Control control in Controls)
			if ((control.ID != null) && Row.Table.Columns.Contains (control.ID))
			{
				if (control is TextBox)
					((TextBox)control).Text = DecodeInput (Row[control.ID].ToString ());
				else if (control is DropDownList)
					((DropDownList)control).SelectedValue = Row[control.ID].ToString ();
				else if ((control is RadioButton) && (Row.Table.Columns[control.ID].DataType == typeof (bool)))
				{
					((RadioButton)control).Checked = (Row[control.ID] is bool) && (bool)Row[control.ID];
					foreach (Control control2 in ((RadioButton)control).Parent.Controls)
						if ((control2 is RadioButton) && (control2.ID != control.ID) &&
								(((RadioButton)control2).GroupName == ((RadioButton)control).GroupName))
							((RadioButton)control2).Checked = !((RadioButton)control).Checked;
				}
				else if (control is CheckBox)
					((CheckBox)control).Checked = (Row[control.ID] is bool) && (bool)Row[control.ID];
				else if (control is Calendar)
					if (Row[control.ID] is DateTime)
					{
						((Calendar)control).SelectedDate = (DateTime)Row[control.ID];
						((Calendar)control).VisibleDate = (DateTime)Row[control.ID];
					}
			}
			else if ((control.ID != null) && (control is DropDownList) && (Row.Table.Columns.Contains (control.ID + "ID")))
				try
				{
					((DropDownList)control).SelectedValue = Row[control.ID + "ID"].ToString ();
				}
				catch (Exception)
				{
				}
			else if (control.Controls.Count > 0)
				ReadRowValues (Row, control.Controls);
	}

	public static void WriteRowValues (DataRow Row, ControlCollection Controls)
	{
		foreach (Control control in Controls)
			if (control.Visible && (control.ID != null) && Row.Table.Columns.Contains (control.ID))
			{
				if ((control is TextBox) && !((TextBox)control).ReadOnly && ((TextBox)control).Enabled)
					if (Row.Table.Columns[control.ID].DataType == typeof (string))
					{
						string temp = EncodeInput (((TextBox)control).Text);
						if ((Row.Table.Columns[control.ID].MaxLength > 0) &&
								(temp.Length > Row.Table.Columns[control.ID].MaxLength))
							Row[control.ID] = temp.Substring (0, Row.Table.Columns[control.ID].MaxLength);
						else
							Row[control.ID] = temp;
					}
					else
						try
						{
							Row[control.ID] = Convert.ChangeType (((TextBox)control).Text,
								Row.Table.Columns[control.ID].DataType);
						}
						catch (Exception)
						{
						}
				else if ((control is DropDownList) && ((DropDownList)control).Enabled)
					if (Row.Table.Columns[control.ID].DataType == typeof (string))
					{
						string temp = EncodeInput (((DropDownList)control).SelectedValue);
						if ((Row.Table.Columns[control.ID].MaxLength > 0) &&
								(temp.Length > Row.Table.Columns[control.ID].MaxLength))
							Row[control.ID] = temp.Substring (0, Row.Table.Columns[control.ID].MaxLength);
						else
							Row[control.ID] = temp;
					}
					else
						try
						{
							Row[control.ID] = Convert.ChangeType (((DropDownList)control).SelectedValue,
								Row.Table.Columns[control.ID].DataType);
						}
						catch (Exception)
						{
						}
				else if ((control is RadioButton) && ((RadioButton)control).Enabled &&
						(Row.Table.Columns[control.ID].DataType == typeof (bool)))
					Row[control.ID] = ((RadioButton)control).Checked;
				else if ((control is CheckBox) && ((CheckBox)control).Enabled)
					Row[control.ID] = ((CheckBox)control).Checked;
				else if ((control is Calendar) && ((Calendar)control).Enabled)
					Row[control.ID] = ((Calendar)control).SelectedDate;
			}
			else if (control.Visible && (control.ID != null) && (control is DropDownList) &&
					((DropDownList)control).Enabled && (Row.Table.Columns.Contains (control.ID + "ID")))
				try
				{
					Row[control.ID + "ID"] = Convert.ToInt32 (((DropDownList)control).SelectedValue);
				}
				catch (Exception)
				{
				}
			else if (control.Controls.Count > 0)
				WriteRowValues (Row, control.Controls);
	}

	public static string EncodeInput (string input)
	{
		string retval;

		retval = System.Web.HttpUtility.HtmlEncode (input);
		retval = retval.Replace ("\r\n", "<br />");
		retval = retval.Replace ("\n", "<br />");

		return retval;
	}

	public static string DecodeInput (string input)
	{
		string retval;

		retval = input.Replace ("<br />", "\r\n");
		retval = System.Web.HttpUtility.HtmlDecode (retval);

		return retval;
	}

	public static void ReadCheckBoxListValues (DataTable Table, CheckBoxList CheckBoxList, string RelatedID)
	{
		foreach (ListItem item in CheckBoxList.Items)
		{
			item.Selected = false;
			foreach (DataRow datarow in Table.Rows)
				if ((int)datarow[RelatedID] == Convert.ToInt32 (item.Value))
				{
					item.Selected = true;
					break;
				}
		}
	}

	public static void WriteCheckBoxListValues (CheckBoxList CheckBoxList, DataTable Table, string RelatedID,
		string MainID, int MainIDValue)
	{
		foreach (ListItem item in CheckBoxList.Items)
			if (item.Selected)
			{
				bool iffound = false;
				foreach (DataRow datarow in Table.Rows)
					if (datarow.RowState != DataRowState.Deleted)
						if (((int)datarow[RelatedID] == Convert.ToInt32 (item.Value)) &&
							((int)datarow[MainID] == MainIDValue))
						{
							iffound = true;
							break;
						}

				if (!iffound)
				{
					DataRow newrow = Table.NewRow ();

					newrow[MainID] = MainIDValue;
					newrow[RelatedID] = Convert.ToInt32 (item.Value);

					Table.Rows.Add (newrow);
				}
			}
			else
				foreach (DataRow datarow in new Snapshot (Table.Rows))
					if (datarow.RowState != DataRowState.Deleted)
						if (((int)datarow[RelatedID] == Convert.ToInt32 (item.Value)) &&
								((int)datarow[MainID] == MainIDValue))
							datarow.Delete ();
	}

	public static string GetSiteAddress ()
	{
        //return "http://204.12.125.187";
        return "http://www." + ConfigurationManager.AppSettings["DomainName"];
/*
		if (HttpContext.Current == null)
			return "http://www.travels-abroad.com";
		else if (HttpContext.Current.Request.Url.ToString ().Length >
				HttpUtility.UrlDecode (HttpContext.Current.Request.RawUrl).Length)
			return HttpContext.Current.Request.Url.ToString ().Substring (0,
				HttpContext.Current.Request.Url.ToString ().Length -
				HttpUtility.UrlDecode (HttpContext.Current.Request.RawUrl).Length);
		else
			return "";
*/
	}

	public static string GetDomainName ()
	{
		return GetSiteAddress ().Replace ("www.", "").Replace ("http://", "").Replace ("https://", "");
	}

	public static string GetSiteName ()
	{
		string retval = GetDomainName ();

		if (char.IsLetter (retval[0]))
			retval = char.ToUpper (retval[0]) + retval.Substring (1);

		for (int i = 0; i < retval.Length; i++)
			if (char.IsLetter (retval[i]) && (i != 0) && !char.IsLetter (retval[i]))
				retval = retval.Substring (0, i) + char.IsUpper (retval[i]) +
					((i < retval.Length) ? retval.Substring (i) : "");

		return retval.Replace (".Com", ".com");
	}

	public static string GetUsername (SqlConnection Connection, int UserID)
	{
		SqlCommand GetUserID = new SqlCommand ("SELECT Username FROM Users WHERE ID = @UserID", Connection);
		GetUserID.Parameters.Add ("@UserID", SqlDbType.Int);
		GetUserID.Parameters["@UserID"].Value = UserID;

        	bool connectionClosed = (Connection.State == ConnectionState.Closed);
            
        	if(connectionClosed)
            		Connection.Open();

		object useridstr = GetUserID.ExecuteScalar ();
		if (useridstr is string)
			return (string)useridstr;
		else
			return "";

        	if(connectionClosed)
            		Connection.Close();
	}

	public static bool IfEvenRow (DataRowView rowview)
	{
		int i = 0;
		foreach (DataRowView curview in rowview.DataView)
		{
			if ((int)rowview.Row["ID"] == (int)curview.Row["ID"])
				break;
			i++;
		}

		return (i % 2) == 0;
	}

	public static bool IfLastRow (DataRowView rowview)
	{
		bool iffound = false;
		foreach (DataRowView curview in rowview.DataView)
			if (!iffound)
			{
				if ((int)rowview.Row["ID"] == (int)curview.Row["ID"])
					iffound = true;
			}
			else
				return false;

		return iffound;
	}

	public static string ShowAuctionPhoto (DataRow DataRow, string BackLinkText)
	{
		if ((DataRow["FileName"] is string) && (DataRow["Width"] is int) && (DataRow["Height"] is int))
			return "<a href='" + CommonFunctions.PrepareURL (DataRow["ID"].ToString () + "/default.aspx", BackLinkText) + "'>" +
				"<img src='" + ConfigurationManager.AppSettings["ImagesVirtualLocation"] + (string)DataRow["FileName"] + "' " +
				"width='" + CalculatePhotoWidth ((int)DataRow["Width"], (int)DataRow["Height"], 100) + "' " +
				"height='" + CalculatePhotoHeight ((int)DataRow["Width"], (int)DataRow["Height"], 100) + "' /></a>";
		else
			return "";
	}

    public static string ShowPropertyPhoto(DataRow DataRow, string BackLinkText)
    {
        if ((DataRow["FileName"] is string) && (DataRow["Width"] is int) && (DataRow["Height"] is int))
        {
            string urlgen = String.Format("<a href='{0}'><img src='{1}{2}' width='{3}' height='{4}' /></a>", CommonFunctions.PrepareURL(DataRow["Country"].ToString().Replace(" ", "_") + "/" + DataRow["StateProvince"].ToString().Replace(" ", "_") + "/" + DataRow["City"].ToString().Replace(" ", "_") + "/" + DataRow["ID"].ToString() + "/default.aspx", BackLinkText), ConfigurationManager.AppSettings["ImagesVirtualLocation"], (string)DataRow["FileName"], CommonFunctions.CalculatePhotoWidth((int)DataRow["Width"], (int)DataRow["Height"], 100), CommonFunctions.CalculatePhotoHeight((int)DataRow["Width"], (int)DataRow["Height"], 100));
            return urlgen.ToLower() ;
        }
        else
            return "";
    }
    public static string ShowPropertyPhotoWithoutBacklink(DataRow DataRow)
    {
        if ((DataRow["FileName"] is string) && (DataRow["Width"] is int) && (DataRow["Height"] is int))
        {
            string urlgen = String.Format("<a href='{0}'><img src='{1}{2}' width='{3}' height='{4}' /></a>", CommonFunctions.PrepareURL(DataRow["Country"].ToString().Replace(" ", "_") + "/" + DataRow["StateProvince"].ToString().Replace(" ", "_") + "/" + DataRow["City"].ToString().Replace(" ", "_") + "/" + DataRow["ID"].ToString() + "/default.aspx"), ConfigurationManager.AppSettings["ImagesVirtualLocation"], (string)DataRow["FileName"], CommonFunctions.CalculatePhotoWidth((int)DataRow["Width"], (int)DataRow["Height"], 100), CommonFunctions.CalculatePhotoHeight((int)DataRow["Width"], (int)DataRow["Height"], 100));
            return urlgen.ToLower();
        }
        else
            return "";
    }
    public static string ShowPropertyPhotoWithoutBacklinkTravel(DataRow DataRow)
    {
        if ((DataRow["FileName"] is string) && (DataRow["Width"] is int) && (DataRow["Height"] is int))
        {
            string urlgen = String.Format("<a href='{0}'><img src='{1}{2}' width='{3}' height='{4}' alt='{5}' /></a>", CommonFunctions.PrepareURL(DataRow["Country"].ToString().Replace(" ", "_") + "/" + DataRow["StateProvince"].ToString().Replace(" ", "_") + "/" + DataRow["City"].ToString().Replace(" ", "_") + "/" + DataRow["ID"].ToString() + "/default.aspx"), ConfigurationManager.AppSettings["ImagesVirtualLocation"], (string)DataRow["FileName"], CommonFunctions.CalculatePhotoWidth((int)DataRow["Width"], (int)DataRow["Height"], 100), CommonFunctions.CalculatePhotoHeight((int)DataRow["Width"], (int)DataRow["Height"], 100), "Travel Agent " + DataRow["Type"].ToString() + " in " + DataRow["City"].ToString() + ", " + DataRow["Country"].ToString());
            return urlgen.ToLower();
        }
        else
            return "";
    }
	public static int CalculatePhotoWidth (int OriginalWidth, int OriginalHeight, int Limit)
	{
		if (OriginalWidth > OriginalHeight)
			return Limit;
		else
			return (int)(((double)OriginalWidth / (double)OriginalHeight) * Limit);
	}

	public static int CalculatePhotoHeight (int OriginalWidth, int OriginalHeight, int Limit)
	{
		if (OriginalWidth > OriginalHeight)
			return (int)(((double)OriginalHeight / (double)OriginalWidth) * Limit);
		else
			return Limit;
	}

	public static int SyncFill (SqlDataAdapter Adapter, DataSet Dataset)
	{
		int retval;

		lock (Connection)
			retval = Adapter.Fill (Dataset);

		return retval;
	}

	public static int SyncFill (SqlDataAdapter Adapter, DataSet Dataset, string Table)
	{
		int retval;

		lock (Connection)
			retval = Adapter.Fill (Dataset, Table);

		return retval;
	}


}
