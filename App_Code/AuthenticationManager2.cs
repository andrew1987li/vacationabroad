using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Security.Cryptography;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public static class AuthenticationManager2
{
	#region Public Properties

	public static bool IfAuthenticated
	{
		get //gets session value for if authenticated
		{
            LoginInfo info = new LoginInfo();
            if (HttpContext.Current.Session["loginInfo"] != null)
            {                
                info = (LoginInfo)HttpContext.Current.Session["loginInfo"];                
            }
            return info.Authenticated;
			//return HttpContext.Current.Session["Authenticated"] != null;
		}
	}

	public static int UserID
	{
		get //gets session value for userid
		{
            if (!IfAuthenticated)
                return -1;
            else
            {
                LoginInfo info = new LoginInfo();
                if (HttpContext.Current.Session["loginInfo"] != null)
                {
                    info = (LoginInfo)HttpContext.Current.Session["loginInfo"];
                }
                return info.UserID;
            }
				//return (int)HttpContext.Current.Session["UserID"];
		}
	}

	public static string Username
	{
		get //gets session value for username
		{
            if (!IfAuthenticated)
                return "";
            else
            {
                LoginInfo info = new LoginInfo();
                if (HttpContext.Current.Session["loginInfo"] != null)
                {
                    info = (LoginInfo)HttpContext.Current.Session["loginInfo"];
                }
                return info.Username;
            }
				//return HttpContext.Current.Session["Username"].ToString ();
		}
	}

	public static bool IfAdmin
	{
		get //gets session value for if an administrator
		{
            if (!IfAuthenticated)
                return false;
            else
            {
                LoginInfo info = new LoginInfo();
                if (HttpContext.Current.Session["loginInfo"] != null)
                {
                    info = (LoginInfo)HttpContext.Current.Session["loginInfo"];
                }
                return info.IfAdmin;
            }
				//return (bool)HttpContext.Current.Session["IfAdministrator"];
		}
	}

	public static bool IfAgent
	{
		get //gets session value for if an agent
		{
            if (!IfAuthenticated)
                return false;
            else
            {
                LoginInfo info = new LoginInfo();
                if (HttpContext.Current.Session["loginInfo"] != null)
                {
                    info = (LoginInfo)HttpContext.Current.Session["loginInfo"];
                }
                return info.IfAgent;
            }
			//return (bool)HttpContext.Current.Session["IfAgent"];
		}
	}

	#endregion

	#region Public Methods
    public static string Test(string x)
    {
        return "Test";
    }
	public static bool Authenticate (string Username, string Password)
	{
        bool retVal = true;
        LoginInfo obj = new LoginInfo();
        using(SqlConnection connection = CommonFunctions.GetConnection()) {
            connection.Open();
            SqlDataAdapter GetHashAdapter = CommonFunctions.PrepareAdapter(connection,
                "SELECT * FROM Users WHERE Username = @Username",
                SqlDbType.NVarChar, 30);
            DataSet SaltSet = new DataSet();

            GetHashAdapter.SelectCommand.Parameters["@Username"].Value = Username;

            //lock(CommonFunctions.Connection)
                if(GetHashAdapter.Fill(SaltSet, "Users") < 1)
                    retVal = false;

            if(retVal) {
                byte[] salt = (byte[])SaltSet.Tables["Users"].Rows[0]["PasswordSalt"];
                int repeats = (int)SaltSet.Tables["Users"].Rows[0]["Repeats"];
                byte[] correcthash = (byte[])SaltSet.Tables["Users"].Rows[0]["PasswordHash"];

                byte[] pwdhash = HashPassword(Password, salt, repeats);

                if(pwdhash.GetLength(0) != correcthash.GetLength(0))
                    retVal = false;

                if(retVal) {
                    for(int i = 0; (i < pwdhash.GetLength(0)) && (i < correcthash.GetLength(0)); i++)
                        if(pwdhash[i] != correcthash[i])
                            retVal = false;

                    if(retVal) {
                        //sets session authenticated to true
                        //HttpContext.Current.Session["Authenticated"] = true;
                        obj.Authenticated = true;
                        //sets session userid to true
                        //HttpContext.Current.Session["UserID"] = (int)SaltSet.Tables["Users"].Rows[0]["ID"];
                        obj.UserID = (int)SaltSet.Tables["Users"].Rows[0]["ID"];
                        //sets session username
                        if(SaltSet.Tables["Users"].Rows[0]["UserID"].ToString().Length > 0)
                            obj.Username = SaltSet.Tables["Users"].Rows[0]["UserID"].ToString();
                        else
                            obj.Username = SaltSet.Tables["Users"].Rows[0]["Username"].ToString();
                        //sets session if admin
                        obj.IfAdmin = (SaltSet.Tables["Users"].Rows[0]["IfAdmin"] is bool) &&
                            ((bool)SaltSet.Tables["Users"].Rows[0]["IfAdmin"] == true);
                        //sets session if agent
                        obj.IfAgent = (SaltSet.Tables["Users"].Rows[0]["IfAgent"] is bool) &&
                            ((bool)SaltSet.Tables["Users"].Rows[0]["IfAgent"] == true);

                        HttpContext.Current.Session["loginInfo"] = obj;
                    }
                }
            }
            connection.Close();
        }
		return retVal;
	}

	public static void Logout ()
	{ //removes login session info
        HttpContext.Current.Session.Remove("loginInfo");
        //HttpContext.Current.Session["Authenticated"] = null;
        //HttpContext.Current.Session["UserID"] = null;
        //HttpContext.Current.Session["IfAdministrator"] = null;
        //HttpContext.Current.Session["IfAgent"] = null;
	}

	public static byte[] GenerateSalt ()
	{
		RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider ();

		byte[] buffer = new byte[16];

		rng.GetBytes (buffer);

		return buffer;
	}

	public static int GenerateRepeats ()
	{
		RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider ();

		byte[] buffer = new byte[2];

		rng.GetNonZeroBytes (buffer);

		return ((int)(buffer[0]) * 256 + (int)(buffer[1])) % 32768;
	}

	public static byte[] HashPassword (string Password, byte[] Salt, int Repeats)
	{
		string entropy = ConfigurationManager.AppSettings["Entropy"];
		byte[] entropybytes = System.Text.Encoding.ASCII.GetBytes (entropy);
		byte[] saltentropy = new byte[Salt.Length + entropybytes.Length];

		for (int i = 0; i < Salt.Length; i++)
			saltentropy[i] = Salt[i];
		for (int i = 0; i < entropybytes.Length; i++)
			saltentropy[Salt.Length + i] = entropybytes[i];

		PasswordDeriveBytes bytes = new PasswordDeriveBytes (Password, saltentropy, "SHA512", Repeats);

		return bytes.GetBytes (64);
	}

	#endregion
}
