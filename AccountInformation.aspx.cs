using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class AccountInformation : CommonPage
{
    protected bool ifadd;

    //protected SqlConnection Connection = new SqlConnection (ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
    protected SqlDataAdapter MainAdapter;
    protected DataSet MainDataSet = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (userid != -1)
        {
            if (!AuthenticationManager.IfAuthenticated)
            {
                Response.Redirect(CommonFunctions.PrepareURL("Login.aspx?BackLink=" + HttpUtility.UrlEncode(Request.Url.ToString())), true);
            }

            if ((userid != AuthenticationManager.UserID) && !AuthenticationManager.IfAdmin)
            {
                Response.Redirect(CommonFunctions.PrepareURL("Login.aspx?BackLink=" + HttpUtility.UrlEncode(Request.Url.ToString())), true);
            }
        }

        ifadd = (userid == -1);

        //CommonFunctions.Connection.Open ();

        using (SqlConnection connection = CommonFunctions.GetConnection())
        {
            connection.Open();
            MainAdapter = CommonFunctions.PrepareAdapter(connection, "SELECT * FROM Users WHERE ID = @UserID", SqlDbType.Int);

            Username.ReadOnly = !ifadd;
            UsernameRequired.Enabled = ifadd;
            UsernameInvalid.Enabled = ifadd;
            OldPasswordLabel.Visible = !AuthenticationManager.IfAdmin && !ifadd;
            OldPasswordAsterisk.Visible = !AuthenticationManager.IfAdmin && !ifadd;
            OldPassword.Visible = !AuthenticationManager.IfAdmin && !ifadd;
            OldPasswordRequired.Enabled = !AuthenticationManager.IfAdmin && !ifadd;
            OldPasswordInvalid.Enabled = !AuthenticationManager.IfAdmin && !ifadd;
            NewPasswordAsterisk.Visible = ifadd;
            NewPassword2Asterisk.Visible = ifadd;
            NewPasswordRequired.Enabled = ifadd;

            if (!ifadd)
            {
                MainAdapter.SelectCommand.Parameters["@UserID"].Value = userid;
                //lock(CommonFunctions.Connection)
                if (MainAdapter.Fill(MainDataSet, "Users") == 0)
                {
                    //CommonFunctions.Connection.Close ();
                    Response.Redirect(CommonFunctions.PrepareURL("InternalError.aspx"), true);
                }

                if (!IsPostBack)
                {
                    Username.Text = MainDataSet.Tables["Users"].Rows[0]["Username"].ToString();
                    EmailAddress.Text = MainDataSet.Tables["Users"].Rows[0]["Email"].ToString();
                    UserID.Text = MainDataSet.Tables["Users"].Rows[0]["UserID"].ToString();
                }
            }
            connection.Close();
        }
        Page.Header.Controls.Add(new LiteralControl("<link href='/css/StyleSheetBig4.css' rel='stylesheet' type='text/css' />"));


    }

    protected void SubmitButton_Click(object sender, EventArgs e)
    {
        if (!IsValid)
            return;

        if (ifadd)
        {
            //CommonFunctions.Connection.Open ();
            int newid;
            using (SqlConnection connection = CommonFunctions.GetConnection())
            {
                connection.Open();
                //lock(CommonFunctions.Connection)
                MainAdapter = CommonFunctions.PrepareAdapter(connection, "SELECT * FROM Users",
                    SqlDbType.Int);

                MainAdapter.FillSchema(MainDataSet, SchemaType.Mapped, "Users");

                DuplicateUsername.Visible = false;

                SqlCommand getUsername = new SqlCommand("SELECT count(ID) FROM Users WHERE Username = '" + Username.Text + "' OR " +
                    "email='" + EmailAddress.Text + "'", connection);
                object idCount = getUsername.ExecuteScalar();

                if (idCount != null && int.Parse(idCount.ToString()) > 0)
                {
                    DuplicateUsername.Visible = true;
                    return;
                }

                SqlCommand getmaxid = new System.Data.SqlClient.SqlCommand("SELECT MAX(ID) FROM Users", connection);

                object maxid = getmaxid.ExecuteScalar();

                if (maxid is int)
                    newid = (int)maxid + 1;
                else
                    newid = 1;

                byte[] salt = AuthenticationManager.GenerateSalt();
                int repeats = AuthenticationManager.GenerateRepeats();
                byte[] pwdhash = AuthenticationManager.HashPassword(NewPassword.Text, salt, repeats);

                DataRow newuser = MainDataSet.Tables["Users"].NewRow();

                newuser["ID"] = newid;
                newuser["Username"] = Username.Text;
                newuser["PasswordSalt"] = salt;
                newuser["Repeats"] = repeats;
                newuser["PasswordHash"] = pwdhash;
                newuser["Email"] = EmailAddress.Text;
                newuser["IfAdmin"] = 0;
                if (UserID.Text.Length > 0)
                    newuser["UserID"] = UserID.Text;
                else
                    newuser["UserID"] = newuser["Username"];
                newuser["AdministrativeEmail"] = newuser["Email"];
                newuser["IfAgent"] = 0;
                newuser["ReservationEmail"] = newuser["Email"];
                newuser["DateCreated"] = DateTime.Now;

                newuser["FirstName"] = "";
                newuser["LastName"] = "";
                //new part
                newuser["dateModified"] = DateTime.Today.ToString();

                MainDataSet.Tables["Users"].Rows.Add(newuser);

                try
                {
                    //lock(CommonFunctions.Connection)
                    MainAdapter.Update(MainDataSet, "Users");
                }
                catch (Exception exc)
                {
                    //CommonFunctions.Connection.Close ();
                    if (exc.Message.IndexOf("Cannot insert duplicate key") >= 0)
                    {
                        DuplicateUsername.Visible = true;
                        return;
                    }
                    else
                        throw;
                }

                AuthenticationManager.Authenticate(Username.Text, NewPassword.Text);

                Regex regex = new Regex("^\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$");

                // SmtpClient smtpclient = new SmtpClient(ConfigurationManager.AppSettings["SMTPServer"],
                //   int.Parse(ConfigurationManager.AppSettings["SMTPPort"]));
                SmtpClient smtpclient = new SmtpClient("mail.vacations-abroad.com", 25);

                //MailMessage message = new MailMessage (IfShowContactInfo () ?
                //    ContactEmail.Text : "ar@" + CommonFunctions.GetDomainName (), (string)PropertiesFullSet.Tables["Properties"].Rows[0]["Email"]);
               // MailMessage message = new MailMessage("prop@vacations-abroad.com", (string)PropertiesFullSet.Tables["Properties"].Rows[0]["Email"]);


                string emailbody = "New owner registered at " + CommonFunctions.GetSiteName() + ".\n\n" +
                    "Owner details:\n" +
                    "Login name:" + Username.Text + "\n" +
                    "Email address:" + EmailAddress.Text + "\n" +
                    (UserID.Text.Length > 0 ? "User ID:" + UserID.Text + "\n" : "");

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

                    }
                }

//                if (regex.Match(message.To.ToString()).Success)
  //                  smtpclient.Send(message);

                //CommonFunctions.Connection.Close ();
                connection.Close();
            }

            if (!DuplicateUsername.Visible)
                //Response.Redirect (CommonFunctions.PrepareURL ("MyAccount.aspx?UserID=" + newid.ToString ()));
                Response.Redirect(CommonFunctions.PrepareURL("OwnerInformation.aspx?UserID=" + newid.ToString()));
        }
        else
        {
            UsernameWrongWarning.Visible = false;

            using (SqlConnection connection = CommonFunctions.GetConnection())
            {
                connection.Open();

                if (!AuthenticationManager.IfAdmin)
                    if (!AuthenticationManager.Authenticate((string)MainDataSet.Tables["Users"].Rows[0]["Username"],
                        OldPassword.Text))
                    {
                        UsernameWrongWarning.Visible = true;
                        return;
                    }

                //CommonFunctions.Connection.Open ();

                MainDataSet.Tables["Users"].Rows[0]["Email"] = EmailAddress.Text;
                if (UserID.Text.Length > 0)
                    MainDataSet.Tables["Users"].Rows[0]["UserID"] = UserID.Text;
                else
                    MainDataSet.Tables["Users"].Rows[0]["UserID"] = MainDataSet.Tables["Users"].Rows[0]["Username"];

                if (NewPassword.Text.Length > 0)
                {
                    byte[] salt = AuthenticationManager.GenerateSalt();
                    int repeats = AuthenticationManager.GenerateRepeats();
                    byte[] passwordhash = AuthenticationManager.HashPassword(NewPassword.Text, salt, repeats);

                    MainDataSet.Tables["Users"].Rows[0]["PasswordSalt"] = salt;
                    MainDataSet.Tables["Users"].Rows[0]["Repeats"] = repeats;
                    MainDataSet.Tables["Users"].Rows[0]["PasswordHash"] = passwordhash;
                    MainDataSet.Tables["Users"].Rows[0]["dateModified"] = DateTime.Today.ToString();
                }


                MainAdapter = CommonFunctions.PrepareAdapter(connection, "SELECT * FROM Users WHERE ID = @UserID",SqlDbType.Int);

                //lock(CommonFunctions.Connection)
                MainAdapter.Update(MainDataSet, "Users");

                connection.Close();
            }
            //CommonFunctions.Connection.Close ();
        }

        Response.Redirect(backlinkurl);
    }

    protected void CancelButton_Click(object sender, EventArgs e)
    {
        Response.Redirect(backlinkurl);
    }
}
