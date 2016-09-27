using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using ASPSnippets.FaceBookAPI;
using System.Web.Script.Serialization;
using System.Net;
using System.IO;
using ASPSnippets.TwitterAPI;
using Twitterizer;


public partial class accounts_Login : CommonPage
{
    public string cs = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (User!=null && User.Identity.IsAuthenticated)
        {
            Response.Redirect("/userowner/listings.aspx");
        }

        FaceBookConnect.API_Key = ConfigurationManager.AppSettings["FacebookAppId"];
        FaceBookConnect.API_Secret = ConfigurationManager.AppSettings["FacebookAppSecret"];
        if (!IsPostBack)
        {
            if (Request.QueryString["error"] == "access_denied")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('User has denied access.')", true);
                return;
            }

            string code = Request.QueryString["code"];
            if (!string.IsNullOrEmpty(code))
            {
                SocialUser faceBookUser = GetFacebookUserData(code);
                string usrname;

                if ((usrname =AuthenticationManager.Login(faceBookUser.email, faceBookUser.id, 1)) != "")
                {
                    try
                    {

                    }catch
                    {

                    }finally
                    {

                        /*  FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
      usrname, false, 30
      );
                          string encTicket = FormsAuthentication.Encrypt(ticket);
                          Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
                          Response.Redirect(FormsAuthentication.GetRedirectUrl(usrname, false));*/
                        // FormsAuthentication.RedirectFromLoginPage(usrname, false);
                        FormsAuthentication.SetAuthCookie(usrname, false);
                        if (backlinkpassed) Response.Redirect("http://" + Request.ServerVariables["SERVER_NAME"] + ":" + Request.ServerVariables["SERVER_PORT"] + backlinkurl);
                        if (backlinkpassed) Response.Redirect("http://" + Request.ServerVariables["SERVER_NAME"] + ":" + Request.ServerVariables["SERVER_PORT"] + backlinkurl);
                        else if (AuthenticationManager.IfAdmin)
                            Response.Redirect(CommonFunctions.PrepareURL("Administration.aspx"));
                        else
                            Response.Redirect("/userowner/listings.aspx");
                        //  if (backlinkpassed) Response.Redirect(CommonFunctions.PrepareURL(backlinkurl));
                        //  else Response.Redirect(CommonFunctions.PrepareURL("myaccount.aspx"));
                        /*  if (backlinkpassed) Response.Redirect(CommonFunctions.PrepareURL(backlinkurl));
                          Response.Redirect(CommonFunctions.PrepareURL("Listings.aspx?UserID=" +
                                  AuthenticationManager.UserID.ToString()));
                                  */
                    }
 
                }
                else
                {
                    HttpCookie fbinfo = new HttpCookie("fbinfo");
                    fbinfo.Values.Add("Email", faceBookUser.email);
                    fbinfo.Values.Add("faceBookUserId", faceBookUser.id);
                    fbinfo.Values.Add("username", faceBookUser.username);
                    fbinfo.Values.Add("type", "1");
                    //fbinfo["UserName"] = data1.UserName;
                    //fbinfo["Email"] = data1.email;
                    //fbinfo["faceBookUserId"] = data1.id;
                    // fbinfo.Expires = DateTime.Now.AddDays(1);
                    Response.Cookies.Add(fbinfo);
                    Response.Redirect("SocialSignup.aspx");


                }
            }
            if (TwitterConnect.IsAuthorized)
            {
                    Uri theRealURL = new Uri(HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.RawUrl);

                    string requestToken = HttpUtility.ParseQueryString(theRealURL.Query).Get("oauth_token");
                    string accessVerifier = HttpUtility.ParseQueryString(theRealURL.Query).Get("oauth_verifier");
                    // string requestToken = Request["oauth_token"];
                    //string accessVerifier = Request["oauth_verifier"];

                    // string consumerKey = ConfigurationManager.AppSettings["CtuSySDU4l4VVuWG7CRHva81N"];
                    // string consumerSecret = ConfigurationManager.AppSettings["srDIZEDCWvXf4CoIGjPornn5MGW5YCk1lul1ZNftWnXmk4sE34"];
                    int opt = -1;
                string signname = "";
                try
                    {
                        OAuthTokenResponse accessTokenResponse
                                = OAuthUtility.GetAccessToken("CtuSySDU4l4VVuWG7CRHva81N", "srDIZEDCWvXf4CoIGjPornn5MGW5YCk1lul1ZNftWnXmk4sE34", requestToken, accessVerifier);

                        string accessToken = accessTokenResponse.Token;
                        string accessTokenSecret = accessTokenResponse.TokenSecret;

                        string username = accessTokenResponse.ScreenName;
                        string userId = "" + accessTokenResponse.UserId;


                         
                        if ( (signname= AuthenticationManager.Login(username, userId,2))!="")
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
                            fbinfo.Values.Add("type", "2");
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
                         Response.Redirect("/accounts/Login.aspx?" + ex.Message + ex.InnerException);
                    }
                    finally
                    {
                        if (opt == 1) Response.Redirect("SocialSignup.aspx");
                        else if (opt == 0)
                        {
                        //string userData = "ApplicationSpecific data";
                        /*  FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                              signname, false, 30
                              );
                          string encTicket = FormsAuthentication.Encrypt(ticket);
                          Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
                          Response.Redirect(FormsAuthentication.GetRedirectUrl(signname, false));*/
                        //FormsAuthentication.RedirectFromLoginPage(signname, false);
                        FormsAuthentication.SetAuthCookie(signname, false);
                        //Response.Write(backlinkurl);
                        if (backlinkpassed) Response.Redirect("http://" + Request.ServerVariables["SERVER_NAME"] + ":" + Request.ServerVariables["SERVER_PORT"]+ backlinkurl);
                        else if (AuthenticationManager.IfAdmin)
                            Response.Redirect(CommonFunctions.PrepareURL("Administration.aspx"));
                        else
                            Response.Redirect("/userowner/listings.aspx");
                        /* if (AuthenticationManager.IfAdmin)
                              Response.Redirect(CommonFunctions.PrepareURL("Administration.aspx"));
                          else
                              Response.Redirect(CommonFunctions.PrepareURL("Listings.aspx?UserID=" +
                                  AuthenticationManager.UserID.ToString()));*/

                    }
                    }

                }



        }
    
    }

    protected SocialUser GetFacebookUserData(string code)
    {
        string abs_uri = Request.Url.AbsoluteUri;
        abs_uri = abs_uri.Substring(0, abs_uri.IndexOf("code=")-1);

        // Exchange the code for an access token
       // Uri targetUri = new Uri("https://graph.facebook.com/oauth/access_token?client_id=" + ConfigurationManager.AppSettings["FacebookAppId"] + "&client_secret=" + ConfigurationManager.AppSettings["FacebookAppSecret"] + "&redirect_uri=http://" + Request.ServerVariables["SERVER_NAME"] + ":" + Request.ServerVariables["SERVER_PORT"] + "/accounts/login.aspx&code=" + code);
       // Uri targetUri = new Uri("https://graph.facebook.com/oauth/access_token?client_id=" + ConfigurationManager.AppSettings["FacebookAppId"] + "&client_secret=" + ConfigurationManager.AppSettings["FacebookAppSecret"] + "&redirect_uri=http://" + Request.ServerVariables["SERVER_NAME"] + ":" + Request.ServerVariables["SERVER_PORT"] + Request.RawUrl+ "&code=" + code);
        Uri targetUri = new Uri("https://graph.facebook.com/oauth/access_token?client_id=" + ConfigurationManager.AppSettings["FacebookAppId"] + "&client_secret=" + ConfigurationManager.AppSettings["FacebookAppSecret"] + "&redirect_uri="+ Server.UrlEncode( abs_uri) + "&code=" + code);
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
        SocialUser converted = sr.Deserialize<SocialUser>(jsondata);

        // Write the user data to a List
        //List<FaceBookUser> currentUser = new List<FaceBookUser>();
        //currentUser.Add(converted);

        // Return the current Facebook user
        return converted;
    }
 
    protected void btn_signinfacebook_Click(object sender, EventArgs e)
    {
        FaceBookConnect.Authorize("email", Request.Url.AbsoluteUri);
       // FaceBookConnect.Authorize("email", Request.Url.AbsoluteUri.Split('?')[0]);
    }

    protected void btn_signintwitter_Click(object sender, EventArgs e)
    {
        
        if (!TwitterConnect.IsAuthorized)
        {
            /*
            TwitterConnect twitter = new TwitterConnect();
            twitter.Authorize(Request.Url.AbsoluteUri.Split('?')[0]);
            */
            OAuthTokenResponse otr = OAuthUtility.GetRequestToken("CtuSySDU4l4VVuWG7CRHva81N", "srDIZEDCWvXf4CoIGjPornn5MGW5YCk1lul1ZNftWnXmk4sE34",
                             "http://" + Request.ServerVariables["SERVER_NAME"] + ":" + Request.ServerVariables["SERVER_PORT"] + Request.RawUrl);
            Uri uri = OAuthUtility.BuildAuthorizationUri(otr.Token);
            Response.Redirect(uri.AbsoluteUri);
        }

    }

    protected void btn_signupfacebook_Click(object sender, EventArgs e)
    {

        FaceBookConnect.Authorize("email", Request.Url.AbsoluteUri.Split('?')[0]);

    }

    protected void btn_signuptwitter_Click(object sender, EventArgs e)
    {

        if (!TwitterConnect.IsAuthorized)
        {
            /*
            TwitterConnect twitter = new TwitterConnect();
            twitter.Authorize(Request.Url.AbsoluteUri.Split('?')[0]);
            */
            OAuthTokenResponse otr = OAuthUtility.GetRequestToken("CtuSySDU4l4VVuWG7CRHva81N", "srDIZEDCWvXf4CoIGjPornn5MGW5YCk1lul1ZNftWnXmk4sE34",
                             "http://" + Request.ServerVariables["SERVER_NAME"] + ":" + Request.ServerVariables["SERVER_PORT"] +  Request.RawUrl);
            Uri uri = OAuthUtility.BuildAuthorizationUri(otr.Token);
            Response.Redirect(uri.AbsoluteUri);
        }


    }
    protected void btn_signin_Click(object sender, EventArgs e)
    {
        if (!IsValid) return;

        string username = "";
        if ((username = AuthenticationManager.Login(usrname.Text, pwd.Text, 0)) != ""){
            FormsAuthentication.SetAuthCookie(username, false);
            if (backlinkpassed) Response.Redirect("http://" + Request.ServerVariables["SERVER_NAME"] + ":" + Request.ServerVariables["SERVER_PORT"] + backlinkurl);
            else if (AuthenticationManager.IfAdmin)
                Response.Redirect(CommonFunctions.PrepareURL("Administration.aspx"));
            else
                Response.Redirect(CommonFunctions.PrepareURL("Listings.aspx?UserID=" +
                    AuthenticationManager.UserID.ToString()));
        }

    }

    protected void bt_register_Click(object sender, EventArgs e)
    {
        if (!IsValid)
            return;

        try
        {
            int newid;
            using (SqlConnection connection = CommonFunctions.GetConnection())
            {
                connection.Open();
                //lock(CommonFunctions.Connection)
                SqlCommand getmaxid = new System.Data.SqlClient.SqlCommand("SELECT MAX(ID) FROM Users", connection);

                object maxid = getmaxid.ExecuteScalar();

                if (maxid is int)
                    newid = (int)maxid + 1;
                else
                    newid = 1;

                byte[] salt = AuthenticationManager.GenerateSalt();
                int repeats = AuthenticationManager.GenerateRepeats();
                byte[] pwdhash = AuthenticationManager.HashPassword(Password.Text, salt, repeats);

                string sqlQuery = "select * from Users where 0 = 1";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlQuery, connection);
                DataSet MainDataSet = new DataSet();
                dataAdapter.Fill(MainDataSet, "Users");

                DataRow newuser = MainDataSet.Tables["Users"].NewRow();

                newuser["ID"] = newid;
                newuser["Username"] = LoginName.Text;
                newuser["PasswordSalt"] = salt;
                newuser["Repeats"] = repeats;
                newuser["PasswordHash"] = pwdhash;
                newuser["Email"] = Email.Text;
                newuser["IfAdmin"] = 0;

                newuser["UserID"] = newuser["Username"];
                newuser["AdministrativeEmail"] = newuser["Email"];
                newuser["IfAgent"] = 0;
                newuser["ReservationEmail"] = newuser["Email"];
                newuser["DateCreated"] = DateTime.Now;

                newuser["FirstName"] = "";
                newuser["LastName"] = "";
                //new part
                newuser["dateModified"] = DateTime.Today.ToString();
                newuser["AccountType"] = 0;  //0:email 1: facebook 2:twitter
                bool bl_show = false;// showproperty.Checked;
                newuser["Listing"] = (bl_show) ? 1 : 0;
                MainDataSet.Tables["Users"].Rows.Add(newuser);

                new SqlCommandBuilder(dataAdapter);
                int rows = dataAdapter.Update(MainDataSet,"Users");

                if (rows < 1) return;

                CommonFunctions.sendEmail(LoginName.Text, Email.Text);

                //                if (regex.Match(message.To.ToString()).Success)
                //                  smtpclient.Send(message);

                //CommonFunctions.Connection.Close ();
                connection.Close();
                if(AuthenticationManager.Login(LoginName.Text, Password.Text, 0)!="")
                {
                    FormsAuthentication.RedirectFromLoginPage(LoginName.Text, false);
                }
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        //CommonFunctions.Connection.Open ();

        Response.Redirect(backlinkurl);

    }

    protected void UsernameValidate(object source, ServerValidateEventArgs args)
    {
        int count = 0;
        try
        {

            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();

                string sql = String.Format("select count(*) from Users where Username=@email");

                using (SqlCommand command = new SqlCommand(sql, con))
                {
                    SqlParameter email = new SqlParameter("@email", args.Value);
                    command.Parameters.Add(email);

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        count = Convert.ToInt32(reader[0]);
                    }
                }




                con.Close();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

        args.IsValid = (count == 0);
    }

    protected void EmailValidate(object source, ServerValidateEventArgs args)
    {
        int count = 0;
        try
        {

            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();

                string sql = String.Format("select count(*) from Users where (AccountType < 1 or AccountType is null) and Email=@email");

                using(SqlCommand command = new SqlCommand(sql, con))
                {
                    SqlParameter email = new SqlParameter("@email", args.Value);
                    command.Parameters.Add(email);

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        count = Convert.ToInt32(reader[0]);
                    }
                }




                con.Close();
            }
        }
        catch(Exception ex)
        {
            throw ex;
        }

        args.IsValid = (count == 0);
    }
}