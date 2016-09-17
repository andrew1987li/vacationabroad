using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Configuration;
using System.Net.Mail;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using ASPSnippets.FaceBookAPI;
using System.Web.Script.Serialization;
using System.Net;
using System.IO;
using System.Collections.Generic;
using ASPSnippets.TwitterAPI;
using Twitterizer;
using System.Data.SqlClient;

public partial class LoginPage : CommonPage
{
	//protected System.Data.SqlClient.SqlConnection Connection;
	protected System.Data.SqlClient.SqlDataAdapter ResetPasswordAdapter;
	protected System.Data.SqlClient.SqlDataAdapter GetSaltAdapter;
	protected System.Data.SqlClient.SqlDataAdapter CheckPasswordAdapter;
	protected System.Data.SqlClient.SqlCommand sqlSelectCommand1;
	protected System.Data.SqlClient.SqlCommand sqlSelectCommand2;
	protected Vacations.CheckPasswordDataset CheckPasswordSet;
	protected Vacations.ResetPasswordDataset ResetPasswordSet;
	protected Vacations.GetSaltDataset GetSaltSet;
	protected System.Data.SqlClient.SqlCommand sqlSelectCommand3;

	private string backlink;

    protected void fbLogin(object sender, EventArgs e)
    {
        FaceBookConnect.Authorize("email", Request.Url.AbsoluteUri.Split('?')[0]);
        string a = Request.QueryString["code"];
        
    }
    protected void twitter_login(object sender, EventArgs e)
    {
        HttpCookie twi_cookie = Request.Cookies["twidata"];
        if (twi_cookie != null) return;

        if (!TwitterConnect.IsAuthorized)
        {
            /*
            TwitterConnect twitter = new TwitterConnect();
            twitter.Authorize(Request.Url.AbsoluteUri.Split('?')[0]);
            */
            OAuthTokenResponse otr = OAuthUtility.GetRequestToken("CtuSySDU4l4VVuWG7CRHva81N", "srDIZEDCWvXf4CoIGjPornn5MGW5YCk1lul1ZNftWnXmk4sE34",
                             "http://69.89.14.163:86/login.aspx");
            Uri uri = OAuthUtility.BuildAuthorizationUri(otr.Token);
            Response.Redirect(uri.AbsoluteUri);
        }
        
        
       // OAuthTokens tokens = new OAuthTokens();
       // tokens.ConsumerKey = "CdptkvcJvxoQGqOMEqSoLGlkS"; //<-- replace with yours
       // tokens.ConsumerSecret = "nQNgNu1ikIFdsLkSZ5VZuEIf9uNQtj9TdnIX2Ro6irlh6HR5i0";//<-- replace with yours
       // tokens.AccessToken = "392617652-TH3yp5IYprUPx4W1UgARHY8wnmbbvMHRku8TZkAG";//<-- replace with yours
       // tokens.AccessTokenSecret = "webI4UCzJA9FBmDtrCCBD8bSV2MLOW5dZ2A6NraAkSMgH";//<-- replace with yours
    
      //  TwitterUser user = new TwitterUser();
        
        
        

        ////TwitterResponse<TwitterUser> users = TwitterUser.Show();
        //Response.Write(res.UserId.ToString());
        ////Response.Write(res.ScreenName.ToString());
        //Response.Write(res.VerificationString.ToString());
        //Response.Write(res.Token.ToString());
        //Response.Write(res.TokenSecret.ToString());

        //USER TIMELINE (ALL TWEETS)
        //UserTimelineOptions userOptions = new UserTimelineOptions();
        //userOptions.APIBaseAddress = "https://api.twitter.com/1.1/"; // <-- needed for API 1.1
        //userOptions.Count = 20;
        //userOptions.UseSSL = true; // <-- needed for API 1.1
        //userOptions.ScreenName = "shekhar gangwar";//<-- replace with yours

        //TwitterResponse<TwitterStatusCollection> timeline = TwitterTimeline.UserTimeline(tokens, userOptions);
        //    Label4.Text = timeline.Content.ToString();

 

        

    }

    protected System.Data.SqlClient.SqlDataAdapter MainAdapter;
    protected DataSet MainDataSet = new DataSet();

    protected void Page_Load(object sender, System.EventArgs e)
	{

        if (backlink != null)
            Response.Redirect(backlink);
        else if (AuthenticationManager.IfAdmin)
            Response.Redirect(CommonFunctions.PrepareURL("Administration.aspx"));
        else if(AuthenticationManager.IfAuthenticated)
            Response.Redirect(CommonFunctions.PrepareURL("Listings.aspx?UserID=" +
                AuthenticationManager.UserID.ToString()));



        //code for facebook Login

        FaceBookConnect.API_Key = ConfigurationManager.AppSettings["FacebookAppId"];
        FaceBookConnect.API_Secret = ConfigurationManager.AppSettings["FacebookAppSecret"];
        TwitterConnect.API_Key = ConfigurationManager.AppSettings["TwitterAppId"];
        TwitterConnect.API_Secret = ConfigurationManager.AppSettings["TwitterAppSecret"];
        if (!IsPostBack)
        {
            if (Request.QueryString["error"] == "access_denied")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('User has denied access.')", true);
                return;
            }
            HttpCookie myCookie = Request.Cookies["fbdata"];
            if (myCookie != null)
            {
                string code = myCookie.Value;
                Response.Cookies["fbdata"].Expires = DateTime.Now;
                FaceBookUser data1=GetFacebookUserData(code);
                if (!string.IsNullOrEmpty(code))
                {



                    bool result = check_credentials(data1.email, data1.id);
                    if (result == true)
                    {
                        if (backlink != null)
                            Response.Redirect(backlink);
                        else if (AuthenticationManager.IfAdmin)
                            Response.Redirect(CommonFunctions.PrepareURL("Administration.aspx"));
                        else
                            Response.Redirect(CommonFunctions.PrepareURL("Listings.aspx?UserID=" +
                                AuthenticationManager.UserID.ToString()));
                    }
                    else
                    {
                        HttpCookie fbinfo = new HttpCookie("fbinfo");
                        fbinfo.Values.Add("Email", data1.email);
                        fbinfo.Values.Add("faceBookUserId", data1.id);
                        fbinfo.Values.Add("username", data1.username);
                        //fbinfo["UserName"] = data1.UserName;
                        //fbinfo["Email"] = data1.email;
                        //fbinfo["faceBookUserId"] = data1.id;
                        // fbinfo.Expires = DateTime.Now.AddDays(1);
                        Response.Cookies.Add(fbinfo);
                        Response.Redirect("FBAcoountInformation.aspx");
                    }
                }
            }
            if (TwitterConnect.IsAuthorized)
            {
                HttpCookie twi_cookie = Request.Cookies["twidata"];

                if (twi_cookie != null)
                {
                    string email = twi_cookie.Values.Get("email");
                    string id = twi_cookie.Values.Get("eid");

                    Twitter_btn.Text = "logined";
                }
                else
                {
                    Uri theRealURL = new Uri(HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.RawUrl);

                    string requestToken = HttpUtility.ParseQueryString(theRealURL.Query).Get("oauth_token");
                    string accessVerifier = HttpUtility.ParseQueryString(theRealURL.Query).Get("oauth_verifier");
                    // string requestToken = Request["oauth_token"];
                    //string accessVerifier = Request["oauth_verifier"];

                    // string consumerKey = ConfigurationManager.AppSettings["CtuSySDU4l4VVuWG7CRHva81N"];
                    // string consumerSecret = ConfigurationManager.AppSettings["srDIZEDCWvXf4CoIGjPornn5MGW5YCk1lul1ZNftWnXmk4sE34"];
                    int opt = -1;
                    try
                    {
                        OAuthTokenResponse accessTokenResponse
                                = OAuthUtility.GetAccessToken("CtuSySDU4l4VVuWG7CRHva81N", "srDIZEDCWvXf4CoIGjPornn5MGW5YCk1lul1ZNftWnXmk4sE34", requestToken, accessVerifier);

                        string accessToken = accessTokenResponse.Token;
                        string accessTokenSecret = accessTokenResponse.TokenSecret;

                        string username = accessTokenResponse.ScreenName;
                        string userId = "" + accessTokenResponse.UserId;

     

                        if (Authenticate(userId))
                        {
                            opt = 0;
                        }
                        else
                        {
                            //Labelttt.Text = username + "  " + userId;
                            HttpCookie fbinfo = new HttpCookie("fbinfo");
                            fbinfo.Values.Add("Email", "");
                            fbinfo.Values.Add("faceBookUserId", userId);
                            fbinfo.Values.Add("username", username);
                            //fbinfo["UserName"] = data1.UserName;
                            //fbinfo["Email"] = data1.email;
                            //fbinfo["faceBookUserId"] = data1.id;
                            // fbinfo.Expires = DateTime.Now.AddDays(1);
                            Response.Cookies.Add(fbinfo);
                            opt = 1;
                        }

                        


                    }
                    catch (Exception ex)
                    {
                        Response.Redirect("Login.aspx?" + ex.Message + ex.InnerException);
                    }
                    finally
                    {
                        if(opt == 1)Response.Redirect("FBAcoountInformation.aspx");
                        else if(opt == 0)
                        {
                            if (backlink != null)
                                Response.Redirect(backlink);
                            else if (AuthenticationManager.IfAdmin)
                                Response.Redirect(CommonFunctions.PrepareURL("Administration.aspx"));
                            else
                                Response.Redirect(CommonFunctions.PrepareURL("Listings.aspx?UserID=" +
                                    AuthenticationManager.UserID.ToString()));
                        }
                    }

                }



                /*OAuthTokenResponse res = Twitterizer.OAuthUtility.GetRequestToken("CtuSySDU4l4VVuWG7CRHva81N", "srDIZEDCWvXf4CoIGjPornn5MGW5YCk1lul1ZNftWnXmk4sE34", "http://69.89.14.163:86/login.aspx");
                TwitterConnect twitter = new TwitterConnect();
                Response.Write(twitter.OAuthToken);
                twitter.OAuthToken = res.Token;
                twitter.OAuthTokenSecret = res.TokenSecret;
                Response.Write(TwitterConnect.IsDenied);
                */
                //LoggedIn User Twitter Profile Details
                //DataTable dt = twitter.FetchProfile();
                //Response.Write(dt.Rows.Count);

                //bool result = check_credentials(dt.Rows[0]["email"].ToString(), dt.Rows[0]["Id"].ToString());
                //Response.Write(result);
                //if (result == true)
                //{
                //    if (backlink != null)
                //        Response.Redirect(backlink);
                //    else if (AuthenticationManager.IfAdmin)
                //        Response.Redirect(CommonFunctions.PrepareURL("Administration.aspx"));
                //    else
                //        Response.Redirect(CommonFunctions.PrepareURL("Listings.aspx?UserID=" +
                //            AuthenticationManager.UserID.ToString()));
                //}
                //else
                //{
                //    HttpCookie fbinfo = new HttpCookie("fbinfo");
                //    fbinfo.Values.Add("Email", dt.Rows[0]["email"].ToString());
                //    fbinfo.Values.Add("faceBookUserId", dt.Rows[0]["Id"].ToString());
                //    //fbinfo["UserName"] = data1.UserName;
                //    //fbinfo["Email"] = data1.email;
                //    //fbinfo["faceBookUserId"] = data1.id;
                //    // fbinfo.Expires = DateTime.Now.AddDays(1);
                //    Response.Cookies.Add(fbinfo);
                //    Response.Redirect("FBAcoountInformation.aspx");
                //}
            }
            else
            {
                Response.Write("NULL");
            }
           
        }

        string connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        CommonFunctions.Connection.ConnectionString = connectionstring;

        System.Data.SqlClient.SqlCommandBuilder builder = new System.Data.SqlClient.SqlCommandBuilder(ResetPasswordAdapter);

        backlink = Request["BackLink"];
        Page.Form.DefaultButton = LoginButton.UniqueID;
        Page.Header.Controls.Add(new LiteralControl("<link href='http://vacations-abroad.com/css/StyleSheet.css' rel='stylesheet' type='text/css' />"));

	}

    public static bool Authenticate(string userID)
    {
        bool retVal = true;
        using (SqlConnection connection = CommonFunctions.GetConnection())
        {
            connection.Open();
            SqlDataAdapter GetHashAdapter = CommonFunctions.PrepareAdapter(connection,
                "SELECT * FROM Users WHERE TwiID = @Username",
                SqlDbType.NVarChar, 30);
            DataSet SaltSet = new DataSet();

            GetHashAdapter.SelectCommand.Parameters["@Username"].Value = userID;

            //lock(CommonFunctions.Connection)
            if (GetHashAdapter.Fill(SaltSet, "Users") < 1)
                retVal = false;


                if (retVal)
                {

                        HttpContext.Current.Session["Authenticated"] = true;
                        HttpContext.Current.Session["UserID"] = (int)SaltSet.Tables["Users"].Rows[0]["ID"];
                        if (SaltSet.Tables["Users"].Rows[0]["UserID"].ToString().Length > 0)
                            HttpContext.Current.Session["Username"] = SaltSet.Tables["Users"].Rows[0]["UserID"].ToString();
                        else
                            HttpContext.Current.Session["Username"] = SaltSet.Tables["Users"].Rows[0]["Username"].ToString();
                        HttpContext.Current.Session["IfAdministrator"] = (SaltSet.Tables["Users"].Rows[0]["IfAdmin"] is bool) &&
                            ((bool)SaltSet.Tables["Users"].Rows[0]["IfAdmin"] == true);
                        HttpContext.Current.Session["IfAgent"] = (SaltSet.Tables["Users"].Rows[0]["IfAgent"] is bool) &&
                            ((bool)SaltSet.Tables["Users"].Rows[0]["IfAgent"] == true);

                }
  
            connection.Close();
        }
        return retVal;
    }


    protected FaceBookUser GetFacebookUserData(string code)
    {
        // Exchange the code for an access token
        Uri targetUri = new Uri("https://graph.facebook.com/oauth/access_token?client_id=" + ConfigurationManager.AppSettings["FacebookAppId"] + "&client_secret=" + ConfigurationManager.AppSettings["FacebookAppSecret"] + "&redirect_uri=http://" + Request.ServerVariables["SERVER_NAME"] + ":" + Request.ServerVariables["SERVER_PORT"] + "/login.aspx&code=" + code);
        HttpWebRequest at = (HttpWebRequest)HttpWebRequest.Create(targetUri);

        System.IO.StreamReader str = new System.IO.StreamReader(at.GetResponse().GetResponseStream());
        string token = str.ReadToEnd().ToString().Replace("access_token=", "");

        // Split the access token and expiration from the single string
        string[] combined = token.Split('&');
        string accessToken = combined[0];

        // Exchange the code for an extended access token
        Uri eatTargetUri = new Uri("https://graph.facebook.com/oauth/access_token?grant_type=fb_exchange_token&client_id=" + ConfigurationManager.AppSettings["FacebookAppId"] + "&client_secret=" + ConfigurationManager.AppSettings["FacebookAppSecret"] + "&fb_exchange_token=" + accessToken);
        HttpWebRequest eat = (HttpWebRequest)HttpWebRequest.Create(eatTargetUri);

        StreamReader eatStr = new StreamReader(eat.GetResponse().GetResponseStream());
        string eatToken = eatStr.ReadToEnd().ToString().Replace("access_token=", "");

        // Split the access token and expiration from the single string
        string[] eatWords = eatToken.Split('&');
        string extendedAccessToken = eatWords[0];

        // Request the Facebook user information
        Uri targetUserUri = new Uri("https://graph.facebook.com/me?fields=first_name,last_name,email,gender,locale,link&access_token=" + accessToken);
        HttpWebRequest user = (HttpWebRequest)HttpWebRequest.Create(targetUserUri);

        // Read the returned JSON object response
        StreamReader userInfo = new StreamReader(user.GetResponse().GetResponseStream());
        string jsonResponse = string.Empty;
        jsonResponse = userInfo.ReadToEnd();

        // Deserialize and convert the JSON object to the Facebook.User object type
        JavaScriptSerializer sr = new JavaScriptSerializer();
        string jsondata = jsonResponse;
        FaceBookUser converted = sr.Deserialize<FaceBookUser>(jsondata);

        // Write the user data to a List
        //List<FaceBookUser> currentUser = new List<FaceBookUser>();
        //currentUser.Add(converted);

        // Return the current Facebook user
        return converted;
    }

	#region Web Form Designer generated code
	override protected void OnInit(EventArgs e)
	{
		//
		// CODEGEN: This call is required by the ASP.NET Web Form Designer.
		//
		InitializeComponent();
		base.OnInit(e);
	}
	
	/// <summary>
	/// Required method for Designer support - do not modify
	/// the contents of this method with the code editor.
	/// </summary>
	private void InitializeComponent()
	{    
		//CommonFunctions.Connection = new System.Data.SqlClient.SqlConnection();
		this.ResetPasswordAdapter = new System.Data.SqlClient.SqlDataAdapter();
		this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
		this.GetSaltAdapter = new System.Data.SqlClient.SqlDataAdapter();
		this.sqlSelectCommand2 = new System.Data.SqlClient.SqlCommand();
		this.CheckPasswordAdapter = new System.Data.SqlClient.SqlDataAdapter();
		this.sqlSelectCommand3 = new System.Data.SqlClient.SqlCommand();
		this.CheckPasswordSet = new Vacations.CheckPasswordDataset();
		this.ResetPasswordSet = new Vacations.ResetPasswordDataset();
		this.GetSaltSet = new Vacations.GetSaltDataset();
		((System.ComponentModel.ISupportInitialize)(this.CheckPasswordSet)).BeginInit();
		((System.ComponentModel.ISupportInitialize)(this.ResetPasswordSet)).BeginInit();
		((System.ComponentModel.ISupportInitialize)(this.GetSaltSet)).BeginInit();
		// 
		// CommonFunctions.Connection
		// 
		//CommonFunctions.Connection.ConnectionString = "workstation id=MAIN;packet size=4096;integrated security=SSPI;data source=MAIN;pe" +
			//"rsist security info=False;initial catalog=Vacations";
		// 
		// ResetPasswordAdapter
		// 
		this.ResetPasswordAdapter.SelectCommand = this.sqlSelectCommand1;
		this.ResetPasswordAdapter.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																									   new System.Data.Common.DataTableMapping("Table", "Users", new System.Data.Common.DataColumnMapping[] {
																																																				new System.Data.Common.DataColumnMapping("ID", "ID"),
																																																				new System.Data.Common.DataColumnMapping("Username", "Username"),
																																																				new System.Data.Common.DataColumnMapping("PasswordHash", "PasswordHash"),
																																																				new System.Data.Common.DataColumnMapping("Repeats", "Repeats"),
																																																				new System.Data.Common.DataColumnMapping("PasswordSalt", "PasswordSalt"),
																																																				new System.Data.Common.DataColumnMapping("Email", "Email"),
																																																				new System.Data.Common.DataColumnMapping("FirstName", "FirstName"),
																																																				new System.Data.Common.DataColumnMapping("LastName", "LastName")})});
		// 
		// sqlSelectCommand1
		// 
		this.sqlSelectCommand1.CommandText = "SELECT ID, Username, PasswordHash, Repeats, PasswordSalt, Email, FirstName, LastN" +
			"ame FROM Users WHERE (Email = @Email)";
		this.sqlSelectCommand1.Connection = CommonFunctions.GetConnection();
		this.sqlSelectCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Email", System.Data.SqlDbType.NVarChar, 300, "Email"));
		// 
		// GetSaltAdapter
		// 
		this.GetSaltAdapter.SelectCommand = this.sqlSelectCommand2;
		this.GetSaltAdapter.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																								 new System.Data.Common.DataTableMapping("Table", "Users", new System.Data.Common.DataColumnMapping[] {
																																																		  new System.Data.Common.DataColumnMapping("Username", "Username"),
																																																		  new System.Data.Common.DataColumnMapping("PasswordSalt", "PasswordSalt"),
																																																		  new System.Data.Common.DataColumnMapping("Repeats", "Repeats")})});
		// 
		// sqlSelectCommand2
		// 
		this.sqlSelectCommand2.CommandText = "SELECT Username, PasswordSalt, Repeats FROM Users WHERE (Username = @Username)";
		this.sqlSelectCommand2.Connection = CommonFunctions.GetConnection();
		this.sqlSelectCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Username", System.Data.SqlDbType.NVarChar, 30, "Username"));
		// 
		// CheckPasswordAdapter
		// 
		this.CheckPasswordAdapter.SelectCommand = this.sqlSelectCommand3;
		this.CheckPasswordAdapter.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																									   new System.Data.Common.DataTableMapping("Table", "Users", new System.Data.Common.DataColumnMapping[] {
																																																				new System.Data.Common.DataColumnMapping("ID", "ID"),
																																																				new System.Data.Common.DataColumnMapping("Username", "Username"),
																																																				new System.Data.Common.DataColumnMapping("PasswordHash", "PasswordHash"),
																																																				new System.Data.Common.DataColumnMapping("IfAdmin", "IfAdmin")})});
		// 
		// sqlSelectCommand3
		// 
		this.sqlSelectCommand3.CommandText = "SELECT ID, Username, PasswordHash, IfAdmin FROM Users WHERE (Username = @Username" +
			") AND (PasswordHash = @PasswordHash)";
		this.sqlSelectCommand3.Connection = CommonFunctions.GetConnection();
		this.sqlSelectCommand3.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Username", System.Data.SqlDbType.NVarChar, 30, "Username"));
		this.sqlSelectCommand3.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PasswordHash", System.Data.SqlDbType.VarBinary, 64, "PasswordHash"));
		// 
		// CheckPasswordSet
		// 
		this.CheckPasswordSet.DataSetName = "CheckPasswordDataset";
		this.CheckPasswordSet.Locale = new System.Globalization.CultureInfo("en-US");
		// 
		// ResetPasswordSet
		// 
		this.ResetPasswordSet.DataSetName = "ResetPasswordDataset";
		this.ResetPasswordSet.Locale = new System.Globalization.CultureInfo("en-US");
		// 
		// GetSaltSet
		// 
		this.GetSaltSet.DataSetName = "GetSaltDataset";
		this.GetSaltSet.Locale = new System.Globalization.CultureInfo("en-US");
		((System.ComponentModel.ISupportInitialize)(this.CheckPasswordSet)).EndInit();
		((System.ComponentModel.ISupportInitialize)(this.ResetPasswordSet)).EndInit();
		((System.ComponentModel.ISupportInitialize)(this.GetSaltSet)).EndInit();

	}
	#endregion

	protected void LoginButton_Click(object sender, System.EventArgs e)
	{
		EnterUsername.Validate ();
		CheckUsername.Validate ();
		EnterPassword.Validate ();
		CheckPassword.Validate ();

		if (!EnterUsername.IsValid || !CheckUsername.IsValid || !EnterPassword.IsValid || !CheckPassword.IsValid)
			return;

		UsernameWrongWarning.Visible = false;

		if (!AuthenticationManager.Authenticate (Username.Text, Password.Text))
		{
			UsernameWrongWarning.Visible = true;
			return;
		}

		if (backlink != null)
			Response.Redirect (backlink);
		else if (AuthenticationManager.IfAdmin)
			Response.Redirect (CommonFunctions.PrepareURL ("Administration.aspx"));
		else
			Response.Redirect (CommonFunctions.PrepareURL ("Listings.aspx?UserID=" +
				AuthenticationManager.UserID.ToString ()));
        //Response.Redirect(CommonFunctions.PrepareURL("MyAccount.aspx?UserID=" +
        //        AuthenticationManager.UserID.ToString()));
	}

    protected bool check_credentials(string email,string password)
    {
        EnterUsername.Validate();
        CheckUsername.Validate();
        EnterPassword.Validate();
        CheckPassword.Validate();

        
        if (!AuthenticationManager.Authenticate(email, password))
        {
            UsernameWrongWarning.Visible = true;
            return false;
        }
        else
        {
            return true;
        }
            
    } 

	protected void SendPassword_Click(object sender, System.EventArgs e)
	{
		System.Security.Cryptography.RNGCryptoServiceProvider rng =
			new System.Security.Cryptography.RNGCryptoServiceProvider ();
		string newpassword;
		byte[] rndbytes = new byte[9];
		string pwdsymbols = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
		int i;

		EnterEmail.Validate ();
		CheckEmail.Validate ();

		if (!EnterEmail.IsValid || !CheckEmail.IsValid)
			return;

		InvalidEmail.Visible = false;
		Emailed.Visible = false;

		ResetPasswordAdapter.SelectCommand.Parameters["@Email"].Value = EmailAddress.Text;
		//lock (CommonFunctions.Connection)
			if (ResetPasswordAdapter.Fill (ResetPasswordSet) < 1)
			{
				InvalidEmail.Visible = true;
				return;
			}

		newpassword = "";
		rng.GetBytes (rndbytes);
		for (i = 0; i < rndbytes.Length; i++)
			newpassword += pwdsymbols.Substring (rndbytes[i] % pwdsymbols.Length, 1);

		byte[] salt = AuthenticationManager.GenerateSalt ();
		int repeats = AuthenticationManager.GenerateRepeats ();
		byte[] passwordhash = AuthenticationManager.HashPassword (newpassword, salt, repeats);

		ResetPasswordSet.Tables["Users"].Rows[0]["PasswordSalt"] = salt;
		ResetPasswordSet.Tables["Users"].Rows[0]["Repeats"] = repeats;
		ResetPasswordSet.Tables["Users"].Rows[0]["PasswordHash"] = passwordhash;

		//lock (CommonFunctions.Connection)
			ResetPasswordAdapter.Update (ResetPasswordSet);

		SmtpClient smtpclient = new SmtpClient (ConfigurationManager.AppSettings["SMTPServer"],
			int.Parse (ConfigurationManager.AppSettings["SMTPPort"]));

		MailMessage message = new MailMessage ("noreply@" + CommonFunctions.GetDomainName (), EmailAddress.Text);
		message.Subject = "Your " + CommonFunctions.GetSiteName () + " Password";
		message.Body = "Dear " + ResetPasswordSet.Tables["Users"].Rows[0]["FirstName"] + " " +
			ResetPasswordSet.Tables["Users"].Rows[0]["LastName"] + "!\n\n" +
			"Your " + CommonFunctions.GetSiteName () + " password has been reset according to" +
			" your request on the " + CommonFunctions.GetSiteName () + " web site (" + CommonFunctions.GetSiteAddress () + ").\n\n" +
			"Your " + CommonFunctions.GetSiteName () + " login name is: " + ResetPasswordSet.Tables["Users"].Rows[0]["Username"] + " \n" +
			"Here is your new " + CommonFunctions.GetSiteName () + " password: " + newpassword;
		message.IsBodyHtml = false;

		message.Body = message.Body.Replace("\r", "").Replace("\n", Environment.NewLine);
		message.Headers["Content-Type"] = "text/plain; charset = \"iso-8859-1\"";

        // Added below to deal with Credential problem of Smarter Mail, on 4/08 --LMG

        smtpclient.Credentials = new System.Net.NetworkCredential("noreply@vacations-abroad.com",
                           System.Configuration.ConfigurationManager.AppSettings["smtpCredential"].ToString());
        smtpclient.UseDefaultCredentials = false;

		smtpclient.Send (message);

		Emailed.Visible = true;
        SendPassword.Visible = false;
	}

    public class FaceBookUser
    {

        public string first_name;
            public string gender;
            public string id;
            public string last_name;
            public string link;
            public string locale;
            public string username;
            public string email;
    }
}
