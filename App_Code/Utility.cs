using System;                                                               
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Net;
using System.Net.Sockets;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;
using System.Net.Mail;
using System.Collections.Generic;
using System.Reflection;

/// <summary>
/// The usual DTF collection of useful functions.
/// </summary>
public class Utility
{
    public static string ProperDater(string strVal)
    {
        string str = strVal;
        try
        {
            if (Convert.ToDateTime(strVal).Year == 1900)
            {
                str = "";

            }

        }
        catch (Exception ex)
        {
            str = "";
        }
        return str;

    }

    public static string GVDVFinder(GridViewRow row, DetailsView dv, string FieldName)
    {
        string str = "";
        TextBox tb = new TextBox();
        string strFullFieldName = "tb" + FieldName;
        if (dv == null)
        {
            tb = (TextBox)row.FindControl(strFullFieldName);
            str = tb.Text.Trim();
            //tb.Text = "";
        }
        else
        {
            tb = (TextBox)dv.FindControl(strFullFieldName);
            str = tb.Text.Trim();
            tb.Text = "";
        }
        return str;
    }

    public static List<Object> PropLooper(Object o, DataSet ds)
    {
        //this returns a list of whatever object we send, with a matched dataset

        List<object> lst = new List<object>();
        if (ds.Tables[0].Rows.Count != 0)
        {
            object oNew = new object();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                //oNew = new Holder.IncentiveSubHead();
                Type tNew = o.GetType();
                oNew = Activator.CreateInstance(tNew);
                foreach (PropertyInfo pi in o.GetType().GetProperties())
                {
                    pi.SetValue(oNew, row[pi.Name].ToString(), null);
                }
                lst.Add(oNew);
            }
        }
        return lst;
    }
    public static bool DSHasLanguage(string LanguageID, DataSet ds)
    {
        bool bl = false;
        if (ds.Tables[0].Rows.Count != 0)
        {
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                if (row["LanguageID"].ToString() == LanguageID)
                {
                    bl = true;
                    break;
                }
            }
        }
        return bl;
    }
    public static string TextBreak(string str, int iBreak)
    {
        //first we look for a break, then we split it
        char[] sep = { ' ' };
        string[] strList = str.Split(sep);
        //now we have to go through everything, text block to text block
        StringBuilder sb = new StringBuilder();
        int iCount = 0;

        string strHold = "";
        foreach (string strInd in strList)
        {
            strHold = strInd;
            int iFirst = strHold.IndexOf(" ");
            if (strHold.Length <= iBreak)
            {
                strHold = strHold + " ";
            }
            else
            {
                iCount = strHold.Length;
                for (int i = 0; i < iCount; )
                {
                    i = i + iBreak;
                    if (i < strHold.Length)
                    {
                        strHold = strHold.Insert(i, " ");
                    }
                    else
                    {
                        strHold = strHold + " ";
                    }
                    iCount = iCount + 1;
                }
            }
            sb.Append(strHold);
        }
        return sb.ToString();
    }
    public static string TextBreak(string str, int iBreak, string BreakerText)
    {
        //first we look for a break, then we split it
        char[] sep = { ' ' };
        string[] strList = str.Split(sep);
        //now we have to go through everything, text block to text block
        StringBuilder sb = new StringBuilder();
        int iCount = 0;

        string strHold = "";
        foreach (string strInd in strList)
        {
            strHold = strInd;
            if (strHold.Length < iBreak)
            {
                strHold = strHold + " ";
            }
            else
            {
                iCount = strHold.Length;
                for (int i = 0; i < iCount; )
                {
                    i = i + iBreak;
                    if (i < strHold.Length)
                    {
                        strHold = strHold.Insert(i, "- ");
                        iCount = iCount + 2;
                    }
                    else
                    {
                        strHold = strHold + " ";
                        iCount = iCount + 1;
                    }
                }
            }
            sb.Append(strHold);
        }
        return sb.ToString();
    }

    public static string TBreaker(string str, int iBreak)
    {
        int iCount = iBreak;
        while (iCount < str.Length)
        {
            str = str.Insert(iCount, "- ");
            iCount = iCount + iBreak + 2;
        }
        return str;
    }
    public static string GetEMEAUserID()
    {
        //do something here
        return "3";
    }
    public static string RollImageNamer(string strFileName)
    {
        strFileName = strFileName.Replace(".", "Roll.");
        return strFileName;
    }
    public static string CleanFileName(string str)
    {
        str = str.Replace(" ", "");
        str = str.Replace("?", "");
        str = str.Replace("\\", "");
        str = str.Replace("/", "");
        str = str.Replace("'", "");
        str = str.Replace("<", "");
        str = str.Replace(">", "");
        str = str.Replace(".", "");
        str = str.Replace(",", "");
        str = str.Replace("(", "");
        str = str.Replace(")", "");
        str = str.Replace("&", "");
        str = str.Replace("^", "");
        str = str.Replace("@", "");
        str = str.Replace("#", "");
        str = str.Replace("!", "");
        str = str.Replace("%", "");
        str = str.Replace("*", "");
        str = str.Replace("+", "");
        str = str.Replace("=", "");
        str = str.Replace("{", "");
        str = str.Replace("[", "");
        str = str.Replace("}", "");
        str = str.Replace("]", "");
        str = str.Replace("|", "");
        str = str.Replace(":", "");
        str = str.Replace(";", "");
        str = str.Replace("\"", "");
        str = str.Replace("'", "");
        return str;
    }
    public static string ProperPhoneFormat(string PhoneNumber)
    {
        double iPhone = Convert.ToDouble(PhoneNumber);
        string strPhoneNumber = iPhone.ToString("(###) ### - ####");
        return strPhoneNumber;
    }



    public static string FormatCurrency(string str)
    {
        string strFinal = "";
        double dblTotalPrice = 0;
        try
        {
            dblTotalPrice = Convert.ToDouble(str);
            strFinal = String.Format("{0:c}", dblTotalPrice);
        }
        catch
        {
            strFinal = String.Format("{0:c}", dblTotalPrice);
        }
        return strFinal;
    }
    public static string GetContactID()
    {
        //let's get our cookie
        //grab the cookie			
        HttpCookie cookie = HttpContext.Current.Request.Cookies["LoginValue"];
        //let's get the cookie value
        string strCookieVal = cookie.Value.Trim().ToString();
        DataSet dsLoginInfo = Utility.dsGrab("LoginAdminCheck", "IDGuid", strCookieVal);
        string strContactID = dsLoginInfo.Tables[0].Rows[0]["ContactID"].ToString();
        return strContactID;
    }
    public static string CheckAuth(string UserName, string PassWord)
    {
        string strUserName = UserName.ToString().Trim();
        string strPassWord = PassWord.ToString().Trim();
        string strNewGuid = "";
        //string strFirstName="";

        string[,] strValueList = { { "UserName", strUserName }, { "PassWord", strPassWord } };
        DataSet dsCheckAuth = Utility.DataCommandDS("ClientLoginCheck", strValueList);

        //let's get our cookie value
        DateTime dtDate = DateTime.Now.AddDays(2);
        //dtDate=dtDate.AddDays(2);
        string strDate = dtDate.ToString();
        string strResponse;

        //we check to see if it is empty
        if (dsCheckAuth.Tables[0].Rows.Count != 0)
        {
            strNewGuid = dsCheckAuth.Tables[0].Rows[0]["GuidValue"].ToString().Trim();

            if (strNewGuid != "ImproperLogin")
            {
                HttpContext.Current.Response.Cookies["LoginValue"].Value = strNewGuid;
                HttpContext.Current.Response.Cookies["LoginValue"].Expires = dtDate;
                HttpContext.Current.Response.Redirect("default.aspx");
                strResponse = "It worked";
            }
            else
            {
                strResponse = "Your information was not correct.";
            }

        }
        else
        {
            strResponse = "Your information was not correct.";
        }
        return strResponse;
    }

    public static SqlConnection dbConn()
    {
        //string strConn = System.Configuration.ConfigurationManager.AppSettings["dbConn"].ToString();
        SqlConnection conn = new SqlConnection();
        string strCompName = HttpContext.Current.Request.ServerVariables["SERVER_NAME"];
        string strProperPath = "";
        conn = new SqlConnection(ConfigurationManager.ConnectionStrings["herefordpiesConnectionString1"].ConnectionString);
        return conn;
    }
    public static DataSet dsGrab(string StoredProcName)
    {
        //lets get our variables
        string strStoredProcName = StoredProcName;

        SqlCommand GrabGenericSP = new System.Data.SqlClient.SqlCommand();
        GrabGenericSP.CommandText = strStoredProcName;
        GrabGenericSP.CommandType = System.Data.CommandType.StoredProcedure;
        GrabGenericSP.Connection = Utility.dbConn();

        SqlDataAdapter GrabGenericAdapter = new SqlDataAdapter(GrabGenericSP);
        //instantiate the dataset
        DataSet dsGrabGeneric = new DataSet();
        //fill the dataset
        GrabGenericAdapter.Fill(dsGrabGeneric);
        return dsGrabGeneric;
    }




    public static DataSet dsGrab(string StoredProcName, string ParamOneName, string ParamOneValue)
    {
        //lets get our variables
        string strStoredProcName = StoredProcName;

        SqlCommand GrabGenericSP = new System.Data.SqlClient.SqlCommand();
        GrabGenericSP.CommandText = strStoredProcName;
        GrabGenericSP.CommandType = System.Data.CommandType.StoredProcedure;
        GrabGenericSP.Connection = Utility.dbConn();
        GrabGenericSP.Parameters.Add(new System.Data.SqlClient.SqlParameter("@" + ParamOneName, ParamOneValue));

        SqlDataAdapter GrabGenericAdapter = new SqlDataAdapter(GrabGenericSP);
        //instantiate the dataset
        DataSet dsGrabGeneric = new DataSet();
        //fill the dataset
        GrabGenericAdapter.Fill(dsGrabGeneric);
        return dsGrabGeneric;
    }

    public static DataSet dsGrab(string StoredProcName, string ParamOneName, string ParamOneValue, string ParamTwoName, string ParamTwoValue)
    {
        //lets get our variables
        string strStoredProcName = StoredProcName;

        SqlCommand GrabGenericSP = new System.Data.SqlClient.SqlCommand();
        GrabGenericSP.CommandText = strStoredProcName;
        GrabGenericSP.CommandType = System.Data.CommandType.StoredProcedure;
        GrabGenericSP.Connection = Utility.dbConn();
        GrabGenericSP.Parameters.Add(new System.Data.SqlClient.SqlParameter("@" + ParamOneName, ParamOneValue));
        GrabGenericSP.Parameters.Add(new System.Data.SqlClient.SqlParameter("@" + ParamTwoName, ParamTwoValue));

        SqlDataAdapter GrabGenericAdapter = new SqlDataAdapter(GrabGenericSP);
        //instantiate the dataset
        DataSet dsGrabGeneric = new DataSet();
        //fill the dataset
        GrabGenericAdapter.Fill(dsGrabGeneric);
        return dsGrabGeneric;
    }

    public static DataSet dsGrab(string StoredProcName, string ParamOneName, string ParamOneValue, string ParamTwoName, string ParamTwoValue, string ParamThreeName, string ParamThreeValue)
    {
        //lets get our variables
        string strStoredProcName = StoredProcName;

        SqlCommand GrabGenericSP = new System.Data.SqlClient.SqlCommand();
        GrabGenericSP.CommandText = strStoredProcName;
        GrabGenericSP.CommandType = System.Data.CommandType.StoredProcedure;
        GrabGenericSP.Connection = Utility.dbConn();
        GrabGenericSP.Parameters.Add(new System.Data.SqlClient.SqlParameter("@" + ParamOneName, ParamOneValue));
        GrabGenericSP.Parameters.Add(new System.Data.SqlClient.SqlParameter("@" + ParamTwoName, ParamTwoValue));
        GrabGenericSP.Parameters.Add(new System.Data.SqlClient.SqlParameter("@" + ParamThreeName, ParamThreeValue));

        SqlDataAdapter GrabGenericAdapter = new SqlDataAdapter(GrabGenericSP);
        //instantiate the dataset
        DataSet dsGrabGeneric = new DataSet();
        //fill the dataset
        GrabGenericAdapter.Fill(dsGrabGeneric);
        return dsGrabGeneric;
    }
    public static DataSet dsGrab(string StoredProcName, string ParamOneName, string ParamOneValue, string ParamTwoName, string ParamTwoValue, string ParamThreeName, string ParamThreeValue, string ParamFourName, string ParamFourValue)
    {
        //lets get our variables
        string strStoredProcName = StoredProcName;

        SqlCommand GrabGenericSP = new System.Data.SqlClient.SqlCommand();
        GrabGenericSP.CommandText = strStoredProcName;
        GrabGenericSP.CommandType = System.Data.CommandType.StoredProcedure;
        GrabGenericSP.Connection = Utility.dbConn();
        GrabGenericSP.Parameters.Add(new System.Data.SqlClient.SqlParameter("@" + ParamOneName, ParamOneValue));
        GrabGenericSP.Parameters.Add(new System.Data.SqlClient.SqlParameter("@" + ParamTwoName, ParamTwoValue));
        GrabGenericSP.Parameters.Add(new System.Data.SqlClient.SqlParameter("@" + ParamThreeName, ParamThreeValue));
        GrabGenericSP.Parameters.Add(new System.Data.SqlClient.SqlParameter("@" + ParamFourName, ParamFourValue));


        SqlDataAdapter GrabGenericAdapter = new SqlDataAdapter(GrabGenericSP);
        //instantiate the dataset
        DataSet dsGrabGeneric = new DataSet();
        //fill the dataset
        GrabGenericAdapter.Fill(dsGrabGeneric);
        return dsGrabGeneric;
    }
    public static DataSet dsGrab(string StoredProcName, string ParamOneName, string ParamOneValue, string ParamTwoName, string ParamTwoValue, string ParamThreeName, string ParamThreeValue, string ParamFourName, string ParamFourValue, string ParamFiveName, string ParamFiveValue)
    {
        //lets get our variables
        string strStoredProcName = StoredProcName;

        SqlCommand GrabGenericSP = new System.Data.SqlClient.SqlCommand();
        GrabGenericSP.CommandText = strStoredProcName;
        GrabGenericSP.CommandType = System.Data.CommandType.StoredProcedure;
        GrabGenericSP.Connection = Utility.dbConn();
        GrabGenericSP.Parameters.Add(new System.Data.SqlClient.SqlParameter("@" + ParamOneName, ParamOneValue));
        GrabGenericSP.Parameters.Add(new System.Data.SqlClient.SqlParameter("@" + ParamTwoName, ParamTwoValue));
        GrabGenericSP.Parameters.Add(new System.Data.SqlClient.SqlParameter("@" + ParamThreeName, ParamThreeValue));
        GrabGenericSP.Parameters.Add(new System.Data.SqlClient.SqlParameter("@" + ParamFourName, ParamFourValue));
        GrabGenericSP.Parameters.Add(new System.Data.SqlClient.SqlParameter("@" + ParamFiveName, ParamFiveValue));

        SqlDataAdapter GrabGenericAdapter = new SqlDataAdapter(GrabGenericSP);
        //instantiate the dataset
        DataSet dsGrabGeneric = new DataSet();
        //fill the dataset
        GrabGenericAdapter.Fill(dsGrabGeneric);
        return dsGrabGeneric;
    }
    public static DataSet dsGrab(string StoredProcName, string ParamOneName, string ParamOneValue, string ParamTwoName, string ParamTwoValue, string ParamThreeName, string ParamThreeValue, string ParamFourName, string ParamFourValue, string ParamFiveName, string ParamFiveValue, string ParamSixName, string ParamSixValue)
    {
        //lets get our variables
        string strStoredProcName = StoredProcName;

        SqlCommand GrabGenericSP = new System.Data.SqlClient.SqlCommand();
        GrabGenericSP.CommandText = strStoredProcName;
        GrabGenericSP.CommandType = System.Data.CommandType.StoredProcedure;
        GrabGenericSP.Connection = Utility.dbConn();
        GrabGenericSP.Parameters.Add(new System.Data.SqlClient.SqlParameter("@" + ParamOneName, ParamOneValue));
        GrabGenericSP.Parameters.Add(new System.Data.SqlClient.SqlParameter("@" + ParamTwoName, ParamTwoValue));
        GrabGenericSP.Parameters.Add(new System.Data.SqlClient.SqlParameter("@" + ParamThreeName, ParamThreeValue));
        GrabGenericSP.Parameters.Add(new System.Data.SqlClient.SqlParameter("@" + ParamFourName, ParamFourValue));
        GrabGenericSP.Parameters.Add(new System.Data.SqlClient.SqlParameter("@" + ParamFiveName, ParamFiveValue));
        GrabGenericSP.Parameters.Add(new System.Data.SqlClient.SqlParameter("@" + ParamSixName, ParamSixValue));

        SqlDataAdapter GrabGenericAdapter = new SqlDataAdapter(GrabGenericSP);
        //instantiate the dataset
        DataSet dsGrabGeneric = new DataSet();
        //fill the dataset
        GrabGenericAdapter.Fill(dsGrabGeneric);
        return dsGrabGeneric;
    }
    public static void DataCommand(string storedProcName, List<SqlParameter> ValueList)
    {
        using (SqlConnection connection = Utility.dbConn())
        {
            List<string> lstContactList = new List<string>();
            string strStoredProcName = storedProcName.Trim();
            //ok, this assumes that 
            //  1) order is not relevent, 
            //  2) everything is converted to a string for simplicity, (since the sp doesn't care)
            //  3) the SP already exists 

            //the stucture of the array is 
            //string[,] siblings = { {"@ParamName_1", "Value_1"}, {"@ParamName_2", "Value_2"} }; 
            //so the parameter name is always first, and the value is always second.			
            SqlCommand GenericDataCommandSP = new System.Data.SqlClient.SqlCommand();
            GenericDataCommandSP.CommandText = strStoredProcName;
            GenericDataCommandSP.CommandType = System.Data.CommandType.StoredProcedure;
            GenericDataCommandSP.Connection = connection;

            foreach (SqlParameter sp in ValueList)
            {
                GenericDataCommandSP.Parameters.Add(sp);

            }
            //now we add our parameters

            GenericDataCommandSP.Connection.Open();
            GenericDataCommandSP.ExecuteNonQuery();
            GenericDataCommandSP.Connection.Close();
        }

    }
    public static DataSet DataCommandDS(string StoredProcName, List<SqlParameter> ValueList)
    {
        string strStoredProcName = StoredProcName.Trim().ToString();
        //ok, this assumes that 
        //  1) order is not relevent, 
        //  2) everything is converted to a string for simplicity, (since the sp doesn't care)
        //  3) the SP already exists 

        //the stucture of the array is 
        //string[,] siblings = { {"@ParamName_1", "Value_1"}, {"@ParamName_2", "Value_2"} }; 
        //so the parameter name is always first, and the value is always second.			
        SqlCommand GenericDataCommandSP = new System.Data.SqlClient.SqlCommand();
        GenericDataCommandSP.CommandText = strStoredProcName;
        GenericDataCommandSP.CommandType = System.Data.CommandType.StoredProcedure;
        GenericDataCommandSP.Connection = Utility.dbConn();

        //now we add our parameters
        foreach (SqlParameter sp in ValueList)
        {
            GenericDataCommandSP.Parameters.Add(sp);

        }


        SqlDataAdapter DataCommandDSAdapter = new SqlDataAdapter(GenericDataCommandSP);
        //instantiate the dataset
        DataSet dsDataCommandDS = new DataSet();
        //fill the dataset
        DataCommandDSAdapter.Fill(dsDataCommandDS);
        return dsDataCommandDS;
    }
    public static void DataCommand(string storedProcName, string[,] valueList)
    {
        using (SqlConnection connection = Utility.dbConn())
        {
            string strStoredProcName = storedProcName.Trim().ToString();
            //ok, this assumes that 
            //  1) order is not relevent, 
            //  2) everything is converted to a string for simplicity, (since the sp doesn't care)
            //  3) the SP already exists 

            //the stucture of the array is 
            //string[,] siblings = { {"@ParamName_1", "Value_1"}, {"@ParamName_2", "Value_2"} }; 
            //so the parameter name is always first, and the value is always second.			
            SqlCommand GenericDataCommandSP = new System.Data.SqlClient.SqlCommand();
            GenericDataCommandSP.CommandText = strStoredProcName;
            GenericDataCommandSP.CommandType = System.Data.CommandType.StoredProcedure;
            GenericDataCommandSP.Connection = connection;

            //now we add our parameters
            for (int row = 0; row < valueList.GetLength(0); row++)
            {
                GenericDataCommandSP.Parameters.Add("@" + valueList[row, 0], valueList[row, 1]);
            }
            GenericDataCommandSP.Connection.Open();
            GenericDataCommandSP.ExecuteNonQuery();
            GenericDataCommandSP.Connection.Close();
        }
        
    }
    public static DataSet DataCommandDS(string StoredProcName, string[,] ValueList)
    {
        string strStoredProcName = StoredProcName.Trim().ToString();
        //ok, this assumes that 
        //  1) order is not relevent, 
        //  2) everything is converted to a string for simplicity, (since the sp doesn't care)
        //  3) the SP already exists 

        //the stucture of the array is 
        //string[,] siblings = { {"@ParamName_1", "Value_1"}, {"@ParamName_2", "Value_2"} }; 
        //so the parameter name is always first, and the value is always second.			
        SqlCommand GenericDataCommandSP = new System.Data.SqlClient.SqlCommand();
        GenericDataCommandSP.CommandText = strStoredProcName;
        GenericDataCommandSP.CommandType = System.Data.CommandType.StoredProcedure;
        GenericDataCommandSP.Connection = Utility.dbConn();

        //now we add our parameters
        for (int row = 0; row < ValueList.GetLength(0); row++)
        {
            GenericDataCommandSP.Parameters.Add("@" + ValueList[row, 0], ValueList[row, 1]);
        }


        SqlDataAdapter DataCommandDSAdapter = new SqlDataAdapter(GenericDataCommandSP);
        //instantiate the dataset
        DataSet dsDataCommandDS = new DataSet();
        //fill the dataset
        DataCommandDSAdapter.Fill(dsDataCommandDS);
        return dsDataCommandDS;
    }
    public static string ProperPathPrefix()
    {
        //this just return the file path to the rootof the web site
        string strCompName = HttpContext.Current.Request.ServerVariables["SERVER_NAME"];
        string strProperPath = "";
        if (strCompName.ToUpper().Contains("LOCALHOST"))
        {
            strProperPath = @"D:\Creative Plumbing\Cerebrate\AXTRAWebForms\EventWizard\Uploads\";
            strProperPath = @"D:\Creative Plumbing\Cerebrate\AXTRAWebForms\MarketingPromo\MarketingPromo\Uploads\";
            //strProperPath = @"C:\DTF\Tympani\MarketingPromo\MarketingPromo\\Uploads\";

        }
        else if (strCompName.ToUpper().Contains("TEST.AXTRAWEB"))
        {
            strProperPath = @"C:\inetpub\wwwroot\WebSites\AxtraWeb\Web Tier\Inetpub\ax-deploy-web\wizard\MP\Uploads\";
        }
        else
        {
            strProperPath = @"E:\websites\AxtraWeb\Web Tier\Inetpub\ax-deploy-web\wizard\MarketingPromo\Uploads\";
        }
        return strProperPath;
    }
    public static string ProperPathPrefixSeparate()
    {
        //string strProperPath = @"E:\websites\AxtraWeb\Web Tier\Inetpub\ax-deploy-web\cms400\custom\2.0\images\";
        string strProperPath = @"C:\inetpub\wwwroot\WebSites\AxtraWeb\Web Tier\Inetpub\ax-deploy-web\cms400\custom\2.0\images\";
        return strProperPath;
    }


    public static string CorrectMailServer()
    {
        string strCompName = HttpContext.Current.Request.ServerVariables["SERVER_NAME"];
        strCompName = strCompName.ToLower();
        string strMailServer = "";
        if (strCompName == "localhost")
        {
            strMailServer = "smtp.comcast.net";
        }
        if (strCompName == "creativeplumbing.gotdns.com")
        {
            strMailServer = "smtp.comcast.net";
        }
        else
        {
            //strMailServer="";
            strMailServer = "mailhub.registeredsite.com";
        }
        return strMailServer;
    }
    public static string GetCurrentPage()
    {
        string strCurPage = HttpContext.Current.Request.ServerVariables["SCRIPT_NAME"].ToString().Trim();
        int intCurPageLenth = strCurPage.Length;
        int intSlashMark = strCurPage.LastIndexOf("/") + 1;
        strCurPage = strCurPage.Remove(0, intSlashMark);
        return strCurPage;
    }
    public static void RepDSNullCheck(DataSet DS, System.Web.UI.WebControls.Repeater Rep, System.Web.UI.WebControls.Label Lbl, string ErrorText)
    {
        if (DS.Tables[0].Rows.Count != 0)
        {
            Rep.DataSource = DS;
            Rep.DataBind();
            Rep.Visible = true;
            Lbl.Visible = false;
        }
        else
        {
            Rep.Visible = false;
            Lbl.Visible = true;
            Lbl.Text = ErrorText;
        }
    }
    public static void RBLDSNullCheck(DataSet DS, System.Web.UI.WebControls.RadioButtonList RBL, string DataField, string ValueField)
    {
        if (DS.Tables[0].Rows.Count != 0)
        {
            RBL.Items.Clear();
            RBL.ClearSelection();
            RBL.DataTextField = DataField;
            RBL.DataValueField = ValueField;
            RBL.DataSource = DS;
            RBL.DataBind();
            RBL.Visible = true;

        }
        else
        {
            RBL.Visible = false;
        }
    }

    public static void RBLDSNullCheck(DataSet DS, System.Web.UI.WebControls.RadioButtonList RBL, string DataField, string ValueField, string SelectedID)
    {
        if (DS.Tables[0].Rows.Count != 0)
        {
            RBL.Items.Clear();
            RBL.ClearSelection();
            RBL.DataTextField = DataField;
            RBL.DataValueField = ValueField;
            RBL.DataSource = DS;
            RBL.DataBind();
            RBL.Visible = true;
            RBL.SelectedIndex = Convert.ToInt32(RBL.Items.IndexOf(RBL.Items.FindByValue(SelectedID)).ToString());
        }
        else
        {
            RBL.Visible = false;
        }
    }
    public static void DDArranger(DataSet DS, System.Web.UI.WebControls.DropDownList DD, string DataField, string ValueField)
    {
        DD.Items.Clear();
        DD.ClearSelection();
        DD.DataTextField = ValueField;
        DD.DataValueField = DataField;
        DD.DataSource = DS;
        DD.DataBind();
    }
    public static void DDArranger(DataSet DS, System.Web.UI.WebControls.DropDownList DD, string DataField, string ValueField, string SelectedID)
    {


        DD.Items.Clear();
        DD.ClearSelection();
        DD.DataTextField = ValueField;
        DD.DataValueField = DataField;
        DD.DataSource = DS;
        DD.DataBind();
        DD.SelectedIndex = Convert.ToInt32(DD.Items.IndexOf(DD.Items.FindByValue(SelectedID)).ToString());


    }
    public static void DDArranger(DataSet DS, System.Web.UI.WebControls.DropDownList DD, string DataField, string ValueField, string InitialText, string InitialValue)
    {
        DD.Items.Clear();
        DD.ClearSelection();
        DD.DataTextField = ValueField;
        DD.DataValueField = DataField;
        DD.DataSource = DS;
        DD.DataBind();
        ListItem LI = new ListItem(InitialText, InitialValue);
        DD.Items.Insert(0, LI);
        DD.SelectedIndex = Convert.ToInt32(DD.Items.IndexOf(DD.Items.FindByValue(InitialValue)).ToString());

    }
    public static void DDArranger(DataSet DS, System.Web.UI.WebControls.DropDownList DD, string DataField, string ValueField, string InitialText, string InitialValue, string SelectedID)
    {
        DD.Items.Clear();
        DD.ClearSelection();
        DD.DataTextField = ValueField;
        DD.DataValueField = DataField;
        DD.DataSource = DS;
        DD.DataBind();
        ListItem LI = new ListItem(InitialText, InitialValue);
        DD.Items.Insert(0, LI);
        DD.SelectedIndex = Convert.ToInt32(DD.Items.IndexOf(DD.Items.FindByValue(SelectedID)).ToString());
    }
    public static void DGDSNullCheck(DataSet DS, System.Web.UI.WebControls.DataGrid DG, System.Web.UI.WebControls.Label Lbl, string ErrorText)
    {
        if (DS.Tables[0].Rows.Count != 0)
        {
            DG.DataSource = DS;
            DG.DataBind();
            DG.Visible = true;
            Lbl.Visible = false;
        }
        else
        {
            DG.Visible = false;
            Lbl.Visible = true;
            Lbl.Text = ErrorText;
        }
    }
    public static void GVDSNullCheck(DataSet DS, System.Web.UI.WebControls.GridView GV, System.Web.UI.WebControls.Label Lbl, string ErrorText)
    {
        if (DS.Tables[0].Rows.Count != 0)
        {
            GV.DataSource = DS;
            GV.DataBind();
            GV.Visible = true;
            Lbl.Visible = false;
        }
        else
        {
            GV.Visible = false;
            Lbl.Visible = true;
            Lbl.Text = ErrorText;
        }
    }
    public static void SendMail(string strTo, string strSubject, string strMessage)
    {
        MailMessage message = new MailMessage();
        message.From = new MailAddress("noreply@vacations-abroad.com");
        message.To.Add(new MailAddress(strTo));
        message.Subject = strSubject;
        message.Body = strMessage;
        message.IsBodyHtml = true;
        SmtpClient client = new SmtpClient();
        client.Host = "204.12.125.187";
        client.Port = 25;
        client.Credentials = new System.Net.NetworkCredential("noreply@vacations-abroad.com", System.Configuration.ConfigurationManager.AppSettings["smtpCredential"].ToString());
        client.UseDefaultCredentials = false;
        client.Send(message);
    }
    public static void SendMail(string strTo, string strSubject, string strMessage, string strAttachment)
    {
        MailMessage message = new MailMessage();
        message.From = new MailAddress("noreply@vacations-abroad.com");
        message.To.Add(new MailAddress(strTo));
        message.Subject = strSubject;
        message.Body = strMessage;
        message.IsBodyHtml = true;
        SmtpClient client = new SmtpClient();
        client.Host = "204.12.125.187";
        client.Port = 25;
        client.Send(message);
    }
    public static void ConfirmBox(System.Web.UI.WebControls.Button ConButtonName, string MessageText)
    {
        ConButtonName.Attributes.Add("onclick", "return confirm('" + MessageText + "');");
    }
    public static void ButtonDecorate(System.Web.UI.WebControls.Button btn)
    {
        btn.Attributes.Add("onmouseover", "this.className='ButtonClassOver'");
        btn.Attributes.Add("onmouseout", "this.className='ButtonClassOut'");
        btn.CssClass = "ButtonClassOut";
    }
    public static string TextChop(int MaxNumbers, string TextToChop)
    {
        int iLength = TextToChop.Length;
        string strNewString = "";
        if (iLength < MaxNumbers)
        {
            //do nothing
            strNewString = TextToChop;
        }
        else
        {
            strNewString = TextToChop.Remove(MaxNumbers, iLength - MaxNumbers);
        }

        return strNewString;
    }
    public static string[] SplitFileName(string strImageName)
    {
        string[] splitFileName = strImageName.Split('.');
        int intFileName = splitFileName.GetLowerBound(0);
        int intFileExtension = splitFileName.GetUpperBound(0);

        //the first one is the file name, the second is the extnsion
        string[] strSplitFileName = new string[3];
        strSplitFileName[1] = splitFileName[intFileName].ToString();
        strSplitFileName[2] = splitFileName[intFileExtension].ToString();
        return strSplitFileName;
    }
    public static string DateConverter(string OrigDate)
    {
        string strDate = "";
        DateTime dtDate = Convert.ToDateTime(OrigDate);
        strDate += dtDate.Day;
        strDate += " ";
        strDate += dtDate.ToString("MMMM");
        strDate += " ";
        strDate += dtDate.Year;
        return strDate;
    }
    public static string FirstOfMonth(string Date)
    {
        DateTime dt = Convert.ToDateTime(Date);
        string strMonth = dt.Month.ToString();
        string strYear = dt.Year.ToString();
        string strFinalDate = strMonth + "/1/" + strYear;
        return strFinalDate;
    }
    public static string GetIPAddess()
    {
        string strInfo = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        return strInfo;
    }
    public static void DeleteFile(string FileLocation)
    {
        if (File.Exists(FileLocation))
        {
            while (true)
            {
                try
                {
                    File.Delete(FileLocation);
                    break;
                }
                catch
                {
                    System.Threading.Thread.Sleep(10);
                }
            }
        }

    }
    public static string GetCurrentFolder()
    {
        string strCurPage = HttpContext.Current.Request.ServerVariables["SCRIPT_NAME"].ToString().Trim();
        int iURL = strCurPage.Length;
        strCurPage = strCurPage.Replace("http://", "");

        string[] strCP;
        char[] SplitterMain = { '/' };
        strCP = strCurPage.Split(SplitterMain);
        //string strTransNumInd=strROMain[1];
        string strFolder = "";

        for (int x = 0; x < strCP.Length - 1; x++)
        {
            strFolder = (strCP[x]);
        }
        strFolder = strFolder.Trim();
        strFolder = strFolder.ToLower();
        if (strFolder == "")
        {
            strFolder = "";
        }
        return strFolder;
    }
    /*
    public static string FormatCurrency(string Price)
    {
        double dblTotalPrice = Convert.ToDouble(Price);
        string strTotalPrice = String.Format("{0:c}", dblTotalPrice);
        return strTotalPrice;
    }
     */

    public static string FormatCurrency(string Price, bool ShowFree)
    {
        double dblTotalPrice = Convert.ToDouble(Price);
        string strTotalPrice = String.Format("{0:c}", dblTotalPrice);
        if (ShowFree = true)
        {
            strTotalPrice = "Free!";
        }
        else
        {
            strTotalPrice = strTotalPrice;
        }
        return strTotalPrice;
    }
    public static bool IsSecure()
    {
        bool blIsSecure = false;
        string strSecureStatus = HttpContext.Current.Request.ServerVariables["HTTPS"].ToString().ToUpper();
        if (strSecureStatus == "ON")
        {
            blIsSecure = true;
        }
        else
        {
            blIsSecure = false;
        }
        return blIsSecure;
    }
    public static void ToSecurePage()
    {
        string strSecureStatus = HttpContext.Current.Request.ServerVariables["HTTPS"].ToString().ToUpper();
        string strURL = "https://" + HttpContext.Current.Request.ServerVariables["SERVER_NAME"].ToString() + HttpContext.Current.Request.ServerVariables["URL"].ToString();
        if (Utility.IsSecure() == false)
        {
            HttpContext.Current.Response.Redirect(strURL);
        }
    }
    public static string[] ProperFileName(string strFileName)
    {
        string strUploadedFileName = strFileName;
        string strCleanFileName;
        string[] splitSafeFileName;
        string strSafeFileName = "";
        string strSafeFileExtension = "";
        string strFinalFileName;
        string strStatusMessage = "Success";
        string strStatusDescription = "";
        int intSafeFileName;
        int intSafeFileExtension;
        string[] splitUploadedFilename = strUploadedFileName.Split('\\');
        int intUploadedFileName = splitUploadedFilename.GetUpperBound(0);

        strCleanFileName = splitUploadedFilename[intUploadedFileName];
        //now we replace anything illegal in the file name

        strCleanFileName = strCleanFileName.Replace(" ", "");
        strCleanFileName = strCleanFileName.Replace("?", "");
        //strCleanFileName=strUploadedFileName.Replace("/","");
        strCleanFileName = strCleanFileName.Replace("\\", "");
        strCleanFileName = strCleanFileName.Replace("'", "");

        //now we make sure it has a file extension on it.
        splitSafeFileName = strCleanFileName.Split('.');
        intSafeFileName = splitSafeFileName.GetLowerBound(0);
        intSafeFileExtension = splitSafeFileName.GetUpperBound(0);

        strSafeFileName = splitSafeFileName[intSafeFileName].ToString();
        strSafeFileExtension = splitSafeFileName[intSafeFileExtension].ToString();

        string strSafeTotalFileName = strSafeFileName + "." + strSafeFileExtension;

        //first we make sure it has an etenstion at all
        if (strSafeFileName == strSafeFileExtension)
        {
            strStatusMessage = "NoExtension";
            strStatusDescription = "There was not an extension of the file name, so the file was not saved on the server.  Legal file extensions are .gif,.jpg,.jpeg,.tif,.psd,.ai and .eps.  Please add a proper file extension and try again.";
        }
        else
        {
            //now we convert everything to lower case to keep things tidy
            strSafeFileExtension = strSafeFileExtension.ToLower();
            //OK, now we just have to make sure that it has a proper extension
            if (strSafeFileExtension == "jpg")
            {
                strStatusMessage = "Success";
                strStatusDescription = "You have successfully uploaded <i>" + strSafeTotalFileName + "</i> to the server.";
            }
            else if (strSafeFileExtension == "jpeg")
            {
                strStatusMessage = "Success";
                strStatusDescription = "You have successfully uploaded <i>" + strSafeTotalFileName + "</i> to the server.";
                strSafeFileExtension = "jpg";
            }
            else if (strSafeFileExtension == "gif")
            {
                strStatusMessage = "Success";
                strStatusDescription = "You have successfully uploaded <i>" + strSafeTotalFileName + "</i> to the server.";
            }
            else if (strSafeFileExtension == "psd")
            {
                strStatusMessage = "Success";
                strStatusDescription = "You have successfully uploaded <i>" + strSafeTotalFileName + "</i> to the server.";
            }
            else if (strSafeFileExtension == "tif")
            {
                strStatusMessage = "Success";
                strStatusDescription = "You have successfully uploaded <i>" + strSafeTotalFileName + "</i> to the server.";
            }
            else if (strSafeFileExtension == "tiff")
            {
                strStatusMessage = "Success";
                strStatusDescription = "You have successfully uploaded <i>" + strSafeTotalFileName + "</i> to the server.";
                strSafeFileExtension = "tif";
            }
            else if (strSafeFileExtension == "eps")
            {
                strStatusMessage = "Success";
                strStatusDescription = "You have successfully uploaded <i>" + strSafeTotalFileName + "</i> to the server.";
            }
            else if (strSafeFileExtension == "ai")
            {
                strStatusMessage = "Success";
                strStatusDescription = "You have successfully uploaded <i>" + strSafeTotalFileName + "</i> to the server.";
            }
            else
            {
                strStatusMessage = "IllegalFileName";
                strStatusDescription = "The file had an illegal extension on the file name, specifically <b>" + strSafeFileExtension + "</b>.  The file was not saved on the server.  Please change the file extension and try again.";
            }
        }

        strFinalFileName = strSafeFileName + "." + strSafeFileExtension;
        string[] strProperFileName = new string[4];
        strProperFileName[1] = strFinalFileName;
        strProperFileName[2] = strStatusMessage;
        strProperFileName[3] = strStatusDescription;
        return strProperFileName;
    }
    public static string CheckImageOverWrite(string strPath, string strImageName)
    {
        //----------------------------start new stuff
        string filename;
        //lets get the filename and extension in case we have to rename it
        string[] strSplitFileName = new string[3];
        bool blGoodFileName = false;
        strSplitFileName = Utility.SplitFileName(strImageName);
        //path to an image/* type of file
        filename = strPath + strImageName;
        //create instance of Bitmap class around specified image file
        string strFinalSafeFileName = "";
        int intVerNum = 1;
        string strVerNum = "";
        string strHoldingFileName = "";
        if (File.Exists(filename) == false)
        {
            //the file doesn't exist, so keep the orignial file name
            strFinalSafeFileName = strImageName;
        }
        else
        {
            //since a file already exists with that name, lets rename what we are saving, remember to add the dot
            //lets loop through all the possibilities here, and when we get 
            //to a free file name, we have the nave the name we want to use

            while (blGoodFileName == false)
            {
                //first we have to assign our string variables and what not
                if (intVerNum < 10)
                {
                    strVerNum = "00" + intVerNum.ToString();
                }
                else if (intVerNum < 100)
                {
                    strVerNum = "0" + intVerNum.ToString();
                }
                else
                {
                    strVerNum = intVerNum.ToString();
                }
                //we get our new holding file name
                strHoldingFileName = strPath + strSplitFileName[1] + "_" + strVerNum + "." + strSplitFileName[2];
                //now we find and acceptable file name
                if (File.Exists(strHoldingFileName))
                {
                    //it's already there then we can't use the file name, so we do nothing
                }
                else
                {
                    //it is not there, then we can use it, so assign that baby
                    strFinalSafeFileName = strSplitFileName[1] + "_" + strVerNum + "." + strSplitFileName[2];
                    blGoodFileName = true;
                }
                intVerNum++;

            }

        }
        //cleanup
        return strFinalSafeFileName;
    }
    public static string Upload(HttpPostedFile UploadedFile, string Path)
    {
        string strPath = Path.ToString().Trim();
        string strOrigFileName = UploadedFile.FileName.ToString();

        //let's get the proper file name
        string[] strProperFileName = Utility.ProperFileName(strOrigFileName);
        string strCleanFileName = strProperFileName[1];
        string strFinalFileName = Utility.CheckImageOverWrite(strPath, strCleanFileName);
        //let's save it			
        UploadedFile.SaveAs(strPath + strFinalFileName);
        return strFinalFileName;
    }
    public static string Upload(HttpPostedFile UploadedFile, string Path, string DesiredFileName)
    {
        string strPath = Path.ToString().Trim();
        string strOrigFileName = UploadedFile.FileName.ToString();

        //let's get the proper file name, all this is the normal file name plus the new parameter of desired file name
        string[] strProperFileName = Utility.ProperFileName(DesiredFileName);
        string strCleanFileName = strProperFileName[1];
        string strFinalFileName = Utility.CheckImageOverWrite(strPath, strCleanFileName);
        //let's save it			
        UploadedFile.SaveAs(strPath + strFinalFileName);
        return strFinalFileName;
    }
    public static string GetFileSize(string Path, string FileName)
    {
        string strPath = Path;
        string strFileName = FileName;
        FileInfo flMainFile = new FileInfo(strPath + strFileName);
        int intFileLength = Convert.ToInt32(flMainFile.Length);
        string strFileLength = intFileLength.ToString();
        return strFileLength;
    }
    public static string FileSizeDresser(int FileSize)
    {
        int intFileSize = Convert.ToInt32(FileSize);
        intFileSize = intFileSize / 1024;
        string strFileSize = intFileSize.ToString("#,##0.;($#,##0.00);Zero");
        strFileSize += " KB";
        return strFileSize;
    }
    public static FileInfo[] DirectoryFileList(string DirectoryPath)
    {
        string strDirectoryPath = DirectoryPath.ToString().Trim();
        DirectoryInfo diDirectory = new DirectoryInfo(strDirectoryPath);
        FileInfo[] arrFileInfo = diDirectory.GetFiles();
        return arrFileInfo;
    }
    public static void DirectoryCreate(string DirectoryPath, string DirectoryName)
    {
        string strDirectoryPath = DirectoryPath.ToString().Trim();
        string strDirectoryName = DirectoryName.ToString().Trim();
        DirectoryInfo diDirectory = new DirectoryInfo(strDirectoryPath + strDirectoryName);
        diDirectory.Create();
        //let's create our subdirectories
        diDirectory.CreateSubdirectory("Uploads");
        diDirectory.CreateSubdirectory("Downloads");
    }
    public static string TimeOfDay()
    {
        int intHour = Convert.ToInt32(DateTime.Now.Hour);
        string strTimeOfDay = "";
        if (2 <= intHour && intHour < 12)
        {
            strTimeOfDay = "morning";
        }
        else if (12 <= intHour && intHour < 20)
        {
            strTimeOfDay = "afternoon";
        }
        else if (20 <= intHour && intHour < 24)
        {
            strTimeOfDay = "evening";
        }
        else if (0 <= intHour && intHour <= 2)
        {
            strTimeOfDay = "evening";
        }
        return strTimeOfDay;
    }
    public static void ImageResizeAspectRatio(string OrigPath, string ImageFileName, string NewPath, int MaxDim)
    {
        //read in our current variables
        string strImageFileName = ImageFileName;
        int intMaxDim = MaxDim;

        //ofay - here are the local paths, remember to uncommentt these out later
        string strOrigPath = OrigPath;
        string strNewPath = NewPath;

        //'Read in the image filename to create a thumbnail of
        string imageUrl = strOrigPath + strImageFileName;

        //'Get the image.    
        System.Drawing.Image fullSizeImg;
        fullSizeImg = System.Drawing.Image.FromFile(imageUrl);

        //now we set it to save at the correct size
        int intImgWidth = Convert.ToInt32(fullSizeImg.Width);
        int intImgHeight = Convert.ToInt32(fullSizeImg.Height);
        //now we see see wheter it it long or tall
        bool blIsTall = false;
        bool blIsWide = false;
        if (intImgHeight > intImgWidth)
        {
            blIsTall = true;
        }
        else if (intImgHeight < intImgWidth)
        {
            blIsWide = true;
        }
        else
        {
            //if neighter of them are true, then they are the same dimenstions, i.e a square
        }
        //ok, let's get our aspect ratio

        double dblAspectRatio;
        decimal decAspectRatio;
        int intNewWidth;
        int intNewHeight;
        if (blIsTall == true)
        {
            dblAspectRatio = Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(intMaxDim) / Convert.ToDouble(intImgHeight)));
            decAspectRatio = Convert.ToDecimal(Convert.ToDecimal(Convert.ToDecimal(intMaxDim) / Convert.ToDecimal(intImgHeight)));
            //let's get everything
            intNewHeight = intMaxDim;
            intNewWidth = Convert.ToInt32(Convert.ToDouble(intImgWidth) * dblAspectRatio);
            intNewWidth = Convert.ToInt32(Convert.ToDecimal(intImgWidth) * decAspectRatio);
        }
        else if (blIsWide == true)
        {
            dblAspectRatio = Convert.ToDouble(Convert.ToDouble(intMaxDim) / Convert.ToDouble(intImgWidth));
            decAspectRatio = Convert.ToDecimal(Convert.ToDecimal(Convert.ToDecimal(intMaxDim) / Convert.ToDecimal(intImgWidth)));
            intNewWidth = intMaxDim;
            intNewHeight = Convert.ToInt32(intImgHeight * dblAspectRatio);
            intNewHeight = Convert.ToInt32(Convert.ToDecimal(intImgHeight) * decAspectRatio);
        }
        else
        {
            intNewHeight = intMaxDim;
            intNewWidth = intMaxDim;
        }
        //let's try shrinking it down incrementally
        fullSizeImg = FixedSize(fullSizeImg, intNewWidth, intNewHeight);


        //fullSizeImg=fullSizeImg.GetThumbnailImage(intNewWidth,intNewHeight, null,new IntPtr());


        //now, lets make sure we're not over writing anything
        string strSafeFinalFileName;
        strSafeFinalFileName = CheckImageOverWrite(strNewPath, strImageFileName);
        //save it	
        fullSizeImg.Save(strNewPath + strSafeFinalFileName, ImageFormat.Jpeg);
    }
    public static void ImageMoveSafely(string OrigPath, string NewPath, string ImageFileName)
    {
        //read in our current variables
        string strImageFileName = ImageFileName;
        //'Read in the image filename to create a thumbnail of
        string imageUrl = OrigPath + strImageFileName;

        //'Get the image.    
        System.Drawing.Image fullSizeImg;
        fullSizeImg = System.Drawing.Image.FromFile(imageUrl);

        //now, lets make sure we're not over writing anything
        string strSafeFinalFileName;
        strSafeFinalFileName = CheckImageOverWrite(NewPath, strImageFileName);
        //save it	
        fullSizeImg.Save(NewPath + strSafeFinalFileName, ImageFormat.Jpeg);
    }
    public static string FileExtension(string strImageName)
    {
        string[] splitFileName = strImageName.Split('.');
        int intFileName = splitFileName.GetLowerBound(0);
        int intFileExtension = splitFileName.GetUpperBound(0);

        //the first one is the file name, the second is the extnsion
        string[] strSplitFileName = new string[3];
        strSplitFileName[1] = splitFileName[intFileName].ToString();
        strSplitFileName[2] = splitFileName[intFileExtension].ToString();
        return strSplitFileName[2].ToLower();
    }
    public static void DLDSNullCheck(DataSet DS, System.Web.UI.WebControls.DataList DL, System.Web.UI.WebControls.Label Lbl, string ErrorText)
    {
        if (DS.Tables[0].Rows.Count != 0)
        {
            DL.DataSource = DS;
            DL.DataBind();
            DL.Visible = true;
            Lbl.Visible = false;
        }
        else
        {
            DL.Visible = false;
            Lbl.Visible = true;
            Lbl.Text = ErrorText;
        }
    }
    public static System.Drawing.Image FixedSize(System.Drawing.Image imgPhoto, int Width, int Height)
    {
        int sourceWidth = imgPhoto.Width;
        int sourceHeight = imgPhoto.Height;
        int sourceX = 0;
        int sourceY = 0;
        int destX = 0;
        int destY = 0;

        float nPercent = 0;
        float nPercentW = 0;
        float nPercentH = 0;

        nPercentW = ((float)Width / (float)sourceWidth);
        nPercentH = ((float)Height / (float)sourceHeight);
        if (nPercentH < nPercentW)
        {
            nPercent = nPercentH;
            destX = System.Convert.ToInt16((Width - (sourceWidth * nPercent)) / 2);
        }
        else
        {
            nPercent = nPercentW;
            destY = System.Convert.ToInt16((Height - (sourceHeight * nPercent)) / 2);
        }

        int destWidth = (int)(sourceWidth * nPercent);
        int destHeight = (int)(sourceHeight * nPercent);

        Bitmap bmPhoto = new Bitmap(Width, Height, PixelFormat.Format24bppRgb);
        bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

        Graphics grPhoto = Graphics.FromImage(bmPhoto);
        grPhoto.Clear(Color.Black);
        grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;

        grPhoto.DrawImage(imgPhoto, new Rectangle(destX, destY, destWidth, destHeight),
            new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight), GraphicsUnit.Pixel);

        grPhoto.Dispose();
        return bmPhoto;
    }
    public static string JavaScriptDeleteText()
    {
        string str = "";
        str += "<script language=\"javascript\" type=\"text/javascript\">";
        str += "alert('You are not authorized to delete this record from the database \\n You will now be sent to the previous page.');";
        str += "javascript:history.back()";
        str += "</script>";
        return str;
    }
    public static string CBLValueGetter(System.Web.UI.WebControls.CheckBoxList list)
    {
        string strValue = "";
        for (int counter = 0; counter < list.Items.Count; counter++)
        {
            if (list.Items[counter].Selected)
            {
                strValue += list.Items[counter].Value + ",";
            }
        }
        strValue = strValue.Remove(strValue.Length - 1, 1);
        return strValue; ;
    }
    public static void CBLArranger(DataSet DS, System.Web.UI.WebControls.CheckBoxList CBL, string DataField, string ValueField)
    {
        CBL.Items.Clear();
        CBL.ClearSelection();
        CBL.DataTextField = ValueField;
        CBL.DataValueField = DataField;
        CBL.DataSource = DS;
        CBL.DataBind();
    }
    public static void CBLArranger(DataSet DS, System.Web.UI.WebControls.CheckBoxList CBL, string DataField, string ValueField, DataSet DSExisting)
    {
        CBL.Items.Clear();
        CBL.ClearSelection();
        CBL.DataTextField = ValueField;
        CBL.DataValueField = DataField;
        CBL.DataSource = DS;
        CBL.DataBind();
        foreach (DataRow row in DSExisting.Tables[0].Rows)
        {
            ListItem currentCheckBox = CBL.Items.FindByValue(row["ID"].ToString());
            if (currentCheckBox != null)
            {
                currentCheckBox.Selected = true;
            }
        }
    }
    public static void CBLArranger(DataSet DS, System.Web.UI.WebControls.CheckBoxList CBL, string DataField, string ValueField, DataSet DSExisting, string MatchIDField)
    {
        CBL.Items.Clear();
        CBL.ClearSelection();
        CBL.DataTextField = ValueField;
        CBL.DataValueField = DataField;
        CBL.DataSource = DS;
        CBL.DataBind();
        foreach (DataRow row in DSExisting.Tables[0].Rows)
        {
            ListItem currentCheckBox = CBL.Items.FindByValue(row[MatchIDField].ToString());
            if (currentCheckBox != null)
            {
                currentCheckBox.Selected = true;
            }
        }
    }
    public static string CBRBChecker(string FieldName, string Warning, int NumValues)
    {
        int i = 0;
        string strChecker = "";
        strChecker += "if(";

        while (i < NumValues)
        {
            strChecker += "!document.getElementById('" + FieldName + "_" + i.ToString() + "').checked  && ";
            i++;
        }
        //now we yank the final &&
        strChecker = strChecker.Remove(strChecker.Length - 3, 3);

        strChecker += "){";
        strChecker += System.Environment.NewLine;
        strChecker += "missinginfo += \"\\n     -  " + Warning + "\"";
        strChecker += System.Environment.NewLine;
        strChecker += "}";
        return strChecker;
    }
    public static bool isValidEmailDomain(string inputEmail)
    {
        bool blGoodEmail = true;
        //first we check to see if there is an @ sign
        int iCount = inputEmail.Length;
        int iShort = inputEmail.Replace("@", "").Length;
        //if the two above ints match, there is no @ sign, therefore it is not valid
        if (iCount != iShort)
        {
            string[] host = inputEmail.Split('@');
            string hostName = host[1];
            Socket socket;
            try
            {
                IPHostEntry entry = Dns.GetHostByName(hostName);
                IPEndPoint endPoint = new IPEndPoint(entry.AddressList[0], 25);
                socket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(endPoint);
                //Yippee - the email domain exists!
                blGoodEmail = true;
            }
            catch (SocketException se)
            {
                //Oops - the email domain is not valid!
                blGoodEmail = false;
            }
        }
        else
        {
            blGoodEmail = false;
        }
        return blGoodEmail;
    }
    public static string CreateOptGroup(DataSet DBInfo, string DDName, string OptGroupID, string OptGroupValue, string OptID, string OptValue, string SelectedValue)
    {
        //lets snag all the variables we need
        DataSet dsDBInfo = DBInfo;
        string strOptGroupDD = "";
        string strDDName = DDName;
        string strOptGroupID = OptGroupID;
        string strOptGroupValue = OptGroupValue;
        string strOptID = OptID;
        string strOptValue = OptValue;
        string strOptGroupValueHolder = "";
        string strSelectedValue = "";
        string strSelectedOption = "";

        //let's deal with nulls on the selected value
        if (SelectedValue == null)
        {
            strSelectedValue = "";
        }
        else
        {
            strSelectedValue = SelectedValue.ToString().Trim();
        }

        strOptGroupDD = "<select name=" + strDDName + " runat=server>\n";
        foreach (DataTable table in dsDBInfo.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                if (strOptGroupValueHolder == "")
                {
                    strOptGroupValueHolder = row[strOptGroupValue].ToString();
                    strOptGroupDD += "<OPTGROUP LABEL=\"" + strOptGroupValueHolder + "\">\n";
                }
                else if (strOptGroupValueHolder != row[strOptGroupValue].ToString())
                {
                    strOptGroupValueHolder = row[strOptGroupValue].ToString().Trim();
                    strOptGroupDD += "</OPTGROUP>";
                    strOptGroupDD += "<OPTGROUP LABEL=\"" + strOptGroupValueHolder + "\">\n";
                }
                else
                {
                    //do nothing
                }
                //now we have to check to see if this person (or whatever is selected
                if (strSelectedValue != "")
                {
                    //now we have to check to see if this particluar value should be selected
                    if (strSelectedValue == row[strOptID].ToString().Trim())
                    {
                        strSelectedOption = "Selected";
                    }
                    else
                    {
                        strSelectedOption = "";
                    }
                }
                else
                {
                    strSelectedOption = "";
                }

                strOptGroupDD += "<option " + strSelectedOption + " value=" + row[strOptID].ToString().Trim() + ">" + row[strOptValue].ToString().Trim() + "</option>\n\n";
            }
        }
        //this is the end of the foreach statement
        strOptGroupDD += "</OPTGROUP>\n";
        strOptGroupDD += "</SELECT>\n";
        return strOptGroupDD;
    }
    public static string UploadSwap(HttpPostedFile UploadedFile, string Path, string OrigFileName)
    {
        string strFinalFileName = "";
        if (UploadedFile != null && UploadedFile.ContentLength != 0)
        {
            if (OrigFileName != "")
            {
                Utility.DeleteFile(Path + OrigFileName);
            }
            strFinalFileName = Utility.Upload(UploadedFile, Path);
        }
        else
        {
            strFinalFileName = OrigFileName;
        }
        return strFinalFileName;
    }
    public static string GetCurrentPageWithQS()
    {
        string strCurPage = HttpContext.Current.Request.ServerVariables["SCRIPT_NAME"].ToString().Trim();
        int intCurPageLenth = strCurPage.Length;
        int intSlashMark = strCurPage.LastIndexOf("/") + 1;
        strCurPage = strCurPage.Remove(0, intSlashMark);
        string strQS = HttpContext.Current.Request.ServerVariables["QUERY_STRING"].ToString();
        if (strQS != "")
        {
            strCurPage = strCurPage + "?" + strQS;
        }

        return strCurPage;
    }
    public static void ImageResizeFixedWidth(string OrigPath, string ImageFileName, string NewPath, int MaxWidth)
    {
        //read in our current variables
        string strImageFileName = ImageFileName;
        int intMaxWidth = MaxWidth;

        //ofay - here are the local paths, remember to uncommentt these out later
        string strOrigPath = OrigPath;
        string strNewPath = NewPath;
        double dblAspectRatio;
        decimal decAspectRatio;
        int intNewWidth;
        int intNewHeight;

        //'Read in the image filename to create a thumbnail of
        string imageUrl = strOrigPath + strImageFileName;

        //'Get the image.    
        System.Drawing.Image fullSizeImg;
        fullSizeImg = System.Drawing.Image.FromFile(imageUrl);

        //now we set it to save at the correct size
        int intImgWidth = Convert.ToInt32(fullSizeImg.Width);
        int intImgHeight = Convert.ToInt32(fullSizeImg.Height);
        //now we see see wheter it it long or tall
        bool blIsTall = false;
        bool blIsWide = false;
        blIsWide = true;
        dblAspectRatio = Convert.ToDouble(Convert.ToDouble(intMaxWidth) / Convert.ToDouble(intImgWidth));
        decAspectRatio = Convert.ToDecimal(Convert.ToDecimal(Convert.ToDecimal(intMaxWidth) / Convert.ToDecimal(intImgWidth)));
        intNewWidth = intMaxWidth;
        intNewHeight = Convert.ToInt32(intImgHeight * dblAspectRatio);
        intNewHeight = Convert.ToInt32(Convert.ToDecimal(intImgHeight) * decAspectRatio);
        fullSizeImg = FixedSize(fullSizeImg, intNewWidth, intNewHeight);
        string strSafeFinalFileName = "";
        strSafeFinalFileName = CheckImageOverWrite(strNewPath, strImageFileName);
        //save it	
        fullSizeImg.Save(strNewPath + strSafeFinalFileName, ImageFormat.Jpeg);
    }
    public static string DayOfWeek(string OrigDate)
    {
        string strDate = "";
        DateTime dtDate = Convert.ToDateTime(OrigDate);
        strDate = dtDate.ToString("dddd");
        return strDate;
    }
    public static string MonthOfYear(string OrigDate)
    {
        string strDate = "";
        DateTime dtDate = Convert.ToDateTime(OrigDate);
        strDate = dtDate.ToString("MMMM");
        return strDate;
    }
    public static void WriteTextFile(string Path, string FileName, string Text)
    {
        TextWriter tw = new StreamWriter(Path + FileName);
        // write a line of text to the file
        tw.WriteLine(Text);
        // close the stream
        tw.Close();
        tw.Dispose();

    }
    public static string CBLValueGet(System.Web.UI.WebControls.CheckBoxList list)
    {
        string strValue = "";
        for (int counter = 0; counter < list.Items.Count; counter++)
        {
            if (list.Items[counter].Selected)
            {
                strValue += list.Items[counter].Value + ",";
            }
        }
        if (strValue.Length - 1 < 0)
        {
            strValue = "";
        }
        else
        {
            strValue = strValue.Remove(strValue.Length - 1, 1);
        }

        return strValue;
    }
    public static string FormatNumber(string Num)
    {
        double dbl = Convert.ToDouble(Num);
        string str = String.Format("{0:0,0}", dbl);
        return str;
    }
    public static string CheckBoxStringVal(CheckBox cb)
    {
        string str = "True";
        if (cb.Checked)
        {
            str = "True";
        }
        else
        {
            str = "False";
        }
        return str;
    }
    public static bool ConvertStringToBool(string str)
    {
        bool bl = false;
        if (str.ToUpper() == "TRUE")
        {
            bl = true;
        }
        return bl;
    }
    public static void CopyAndRenameFile(string OldPath, string OldFileName, string NewPath, string NewFileName)
    {
        if (File.Exists(OldPath + OldFileName))
        {
            File.Copy(OldPath + OldFileName, NewPath + NewFileName, true);
        }
    }
    public static void SetPValue(object o, string propertyName, object newValue)
    {
        PropertyInfo pi;
        pi = o.GetType().GetProperty(propertyName);
        if (pi == null)
            throw new Exception("No Property [" +
                propertyName + "] in Object [" +
                o.GetType().ToString() + "]");
        if (!pi.CanWrite)
            throw new Exception("Property [" +
                propertyName + "] in Object [" +
                o.GetType().ToString() +
                "] does not allow writes");
        pi.SetValue(o, newValue, null);
    }
    public static string GetPValue(object o, string propertyName)
    {
        PropertyInfo pi;
        pi = o.GetType().GetProperty(propertyName);
        string str = "";
        if (pi.GetValue(o, null) != null)
        {
            str = pi.GetValue(o, null).ToString();
        }
        return str;
    }

    public static string CookieNullReader(HttpCookie ck, string strDefaultValue)
    {
        string str = "";
        if (ck != null && ck.Value.Length != 0)
        {
            str = ck.Value.ToString();
        }
        else
        {
            str = strDefaultValue;
        }
        return str;
    }


}