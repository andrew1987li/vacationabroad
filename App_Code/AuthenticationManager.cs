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

public static class AuthenticationManager
{
	#region Public Properties

	public static bool IfAuthenticated
	{
		get
		{
			return HttpContext.Current.Session["Authenticated"] != null;
		}
	}

	public static int UserID
	{
		get
		{
			if (!IfAuthenticated)
				return -1;
			else
				return (int)HttpContext.Current.Session["UserID"];
		}
	}

	public static string Username
	{
		get
		{
			if (!IfAuthenticated)
				return "";
			else
				return HttpContext.Current.Session["Username"].ToString ();
		}
	}

	public static bool IfAdmin
	{
		get
		{
			if (!IfAuthenticated)
				return false;
			else
				return (bool)HttpContext.Current.Session["IfAdministrator"];
		}
	}

	public static bool IfAgent
	{
		get
		{
			if (!IfAuthenticated)
				return false;
			else
				return (bool)HttpContext.Current.Session["IfAgent"];
		}
	}

    #endregion

    #region Public Methods

    public static object GetDataValue(object value)
    {
        if (value == null)
        {
            return DBNull.Value;
        }

        return value;
    }



    public static string SocialLogin(string Username, string Password, int Acctype)
    {
        string usrname = "";

        using (SqlConnection connection = CommonFunctions.GetConnection())
        {
            connection.Open();
            SqlDataAdapter GetHashAdapter = new SqlDataAdapter("SELECT * FROM Users WHERE UserID = @UserID and AccountType=@type and AccountType>0", connection);

            DataSet SaltSet = new DataSet();

            GetHashAdapter.SelectCommand.Parameters.Add("@UserID", SqlDbType.NVarChar).Value = GetDataValue(Password);
            GetHashAdapter.SelectCommand.Parameters.Add("@type", SqlDbType.TinyInt).Value = GetDataValue(Acctype);

            //lock(CommonFunctions.Connection)
            if (GetHashAdapter.Fill(SaltSet, "Users") < 1)
                return "";

            if(SaltSet.Tables["Users"].Rows.Count > 0)
            {
                usrname = SaltSet.Tables["Users"].Rows[0]["Username"].ToString();
                HttpContext.Current.Session["Authenticated"] = true;
                HttpContext.Current.Session["UserID"] = (int)SaltSet.Tables["Users"].Rows[0]["ID"];
                HttpContext.Current.Session["Username"] = usrname;
                HttpContext.Current.Session["IfAdministrator"] = (SaltSet.Tables["Users"].Rows[0]["IfAdmin"] is bool) &&
                    ((bool)SaltSet.Tables["Users"].Rows[0]["IfAdmin"] == true);
                HttpContext.Current.Session["IfAgent"] = (SaltSet.Tables["Users"].Rows[0]["IfAgent"] is bool) &&
                    ((bool)SaltSet.Tables["Users"].Rows[0]["IfAgent"] == true);
            }
            connection.Close();
        }

        return usrname;
    }

    public static string Login(string Username, string Password, int Acctype)
    {
        bool retVal = true;
        string usrname = "";
        if(Acctype > 0)
        {
            usrname = SocialLogin(Username, Password, Acctype);
            return usrname;
        }
        using (SqlConnection connection = CommonFunctions.GetConnection())
        {
            connection.Open();
            SqlDataAdapter GetHashAdapter = new SqlDataAdapter("SELECT * FROM Users WHERE Username = @Username or Email=@Email", connection);

            DataSet SaltSet = new DataSet();

            GetHashAdapter.SelectCommand.Parameters.Add("@Username", SqlDbType.NVarChar).Value= GetDataValue(Username);
            GetHashAdapter.SelectCommand.Parameters.Add("@Email", SqlDbType.NVarChar).Value = GetDataValue(Username);

            //lock(CommonFunctions.Connection)
            if (GetHashAdapter.Fill(SaltSet, "Users") < 1)
                retVal = false;

            if (retVal)
            {
                byte[] salt = (byte[])SaltSet.Tables["Users"].Rows[0]["PasswordSalt"];
                int repeats = (int)SaltSet.Tables["Users"].Rows[0]["Repeats"];
                byte[] correcthash = (byte[])SaltSet.Tables["Users"].Rows[0]["PasswordHash"];

                byte[] pwdhash = HashPassword(Password, salt, repeats);

                if (pwdhash.GetLength(0) != correcthash.GetLength(0))
                    retVal = false;

                if (retVal)
                {
                    for (int i = 0; (i < pwdhash.GetLength(0)) && (i < correcthash.GetLength(0)); i++)
                        if (pwdhash[i] != correcthash[i])
                            retVal = false;

                    if (retVal)
                    {
                        usrname = SaltSet.Tables["Users"].Rows[0]["Username"].ToString();
                        HttpContext.Current.Session["Authenticated"] = true;
                        HttpContext.Current.Session["UserID"] = (int)SaltSet.Tables["Users"].Rows[0]["ID"];
                        HttpContext.Current.Session["Username"] = usrname;
                        HttpContext.Current.Session["IfAdministrator"] = (SaltSet.Tables["Users"].Rows[0]["IfAdmin"] is bool) &&
                            ((bool)SaltSet.Tables["Users"].Rows[0]["IfAdmin"] == true);
                        HttpContext.Current.Session["IfAgent"] = (SaltSet.Tables["Users"].Rows[0]["IfAgent"] is bool) &&
                            ((bool)SaltSet.Tables["Users"].Rows[0]["IfAgent"] == true);
                    }
                }
            }
            connection.Close();
        }

        return usrname;
    }
    public static bool Authenticate (string Username, string Password)
	{
        bool retVal = true;
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
                        HttpContext.Current.Session["Authenticated"] = true;
                        HttpContext.Current.Session["UserID"] = (int)SaltSet.Tables["Users"].Rows[0]["ID"];
                        if(SaltSet.Tables["Users"].Rows[0]["UserID"].ToString().Length > 0)
                            HttpContext.Current.Session["Username"] = SaltSet.Tables["Users"].Rows[0]["UserID"].ToString();
                        else
                            HttpContext.Current.Session["Username"] = SaltSet.Tables["Users"].Rows[0]["Username"].ToString();
                        HttpContext.Current.Session["IfAdministrator"] = (SaltSet.Tables["Users"].Rows[0]["IfAdmin"] is bool) &&
                            ((bool)SaltSet.Tables["Users"].Rows[0]["IfAdmin"] == true);
                        HttpContext.Current.Session["IfAgent"] = (SaltSet.Tables["Users"].Rows[0]["IfAgent"] is bool) &&
                            ((bool)SaltSet.Tables["Users"].Rows[0]["IfAgent"] == true);
                    }
                }
            }
            connection.Close();
        }
		return retVal;
	}

	public static void Logout ()
	{
        HttpContext.Current.Session["Authenticated"] = null;
        HttpContext.Current.Session["UserID"] = null;
        HttpContext.Current.Session["Username"] = null;
        HttpContext.Current.Session["IfAdministrator"] = null;
        HttpContext.Current.Session["IfAgent"] = null;
        FormsAuthentication.SignOut();
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
