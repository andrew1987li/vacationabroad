using System;
using System.Web.UI;
using System.Xml;
using oAuthExample;

public partial class TwitterAuthentication : Page
{
    string url = "";
    string xml = "";
    public string name = "";
    public string username = "";
    public string profileImage = "";
    public string followersCount = "";
    public string noOfTweets = "";
    public string recentTweet = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        GetUserDetailsFromTwitter();
    }

    private void GetUserDetailsFromTwitter()
    {
        if(Request["oauth_token"]!=null & Request["oauth_verifier"]!=null)
        {
            imgTwitter.Visible = false;
            tbleTwitInfo.Visible = true;
            var oAuth = new oAuthTwitter();
            //Get the access token and secret.
            oAuth.AccessTokenGet(Request["oauth_token"], Request["oauth_verifier"]);
            if (oAuth.TokenSecret.Length > 0)
            {
                //We now have the credentials, so make a call to the Twitter API.
                url = "http://twitter.com/account/verify_credentials.xml";
                xml = oAuth.oAuthWebRequest(oAuthTwitter.Method.GET, url, String.Empty);
                XmlDocument xmldoc=new XmlDocument();
                xmldoc.LoadXml(xml);
                XmlNodeList xmlList = xmldoc.SelectNodes("/user");
                foreach (XmlNode node in xmlList)
                {
                   name = node["name"].InnerText;
                   username = node["screen_name"].InnerText;
                   profileImage = node["profile_image_url"].InnerText;
                   followersCount = node["followers_count"].InnerText;
                   noOfTweets = node["statuses_count"].InnerText;
                   recentTweet = node["status"]["text"].InnerText;
                }
            }
        }
    }

    protected void imgTwitter_Click(object sender, ImageClickEventArgs e)
    {
       
        var oAuth = new oAuthTwitter();

        if (Request["oauth_token"] == null)
        {
            //Redirect the user to Twitter for authorization.
            //Using oauth_callback for local testing.
            oAuth.CallBackUrl = "http://69.89.14.163:86/TwitterAuthentication.aspx";
            Response.Redirect(oAuth.AuthorizationLinkGet());
        }
        else
        {
            GetUserDetailsFromTwitter();
        }
    }
}