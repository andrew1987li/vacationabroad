using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class accounts_SocialSignup : CommonPage
{
    public string cs = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        HttpCookie myCookie = Request.Cookies["fbinfo"];
        if (myCookie != null)
        {
            string smail = Request.Cookies["fbinfo"]["Email"];
            string faceBookUserId = Request.Cookies["fbinfo"]["faceBookUserId"];
            string Tuserid = Request.Cookies["fbinfo"]["username"];
            hidden_id.Value = faceBookUserId;
            acctype.Value = Request.Cookies["fbinfo"]["type"];
            LoginName.Text = "usr" + Tuserid;
            Email.Text = smail;
 
            //TUserID.Text = TwiID.Value;
            //NewPasswordRequired.Enabled = false;
            Response.Cookies["fbinfo"].Expires = DateTime.Now;


        }
    }

    protected void bt_register_Click(object sender ,  EventArgs e)
    {
        SocialUser social = new SocialUser();
        social.email = Email.Text;
        social.username = LoginName.Text;
        social.id = hidden_id.Value;
        InsertNewUser(social);
    }

    protected bool InsertNewUser(SocialUser social)
    {
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
                byte[] pwdhash = AuthenticationManager.HashPassword(social.id, salt, repeats);

                string sqlQuery = "select * from Users where 0 = 1";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlQuery, connection);
                DataSet MainDataSet = new DataSet();
                dataAdapter.Fill(MainDataSet, "Users");

                DataRow newuser = MainDataSet.Tables["Users"].NewRow();

                newuser["ID"] = newid;
                newuser["Username"] = social.username;
                newuser["PasswordSalt"] = salt;
                newuser["Repeats"] = repeats;
                newuser["PasswordHash"] = pwdhash;
                newuser["Email"] = social.email;
                newuser["IfAdmin"] = 0;

                newuser["UserID"] = social.id;
                newuser["AdministrativeEmail"] = newuser["Email"];
                newuser["IfAgent"] = 0;
                newuser["ReservationEmail"] = newuser["Email"];
                newuser["DateCreated"] = DateTime.Now;

                newuser["FirstName"] = "";
                newuser["LastName"] = "";
                //new part
                newuser["dateModified"] = DateTime.Today.ToString();

                int type = (acctype.Value == "1") ? 1 : 2;
                newuser["AccountType"] = type;  //0:email 1: facebook 2:twitter
                bool bl_show = showproperty.Checked;
                newuser["Listing"] = (bl_show) ? 1 : 0;

                MainDataSet.Tables["Users"].Rows.Add(newuser);

                new SqlCommandBuilder(dataAdapter);
                int rows = dataAdapter.Update(MainDataSet, "Users");

                if (rows < 1) return false;
 
                CommonFunctions.sendEmail(social.username, social.email);

                //                if (regex.Match(message.To.ToString()).Success)
                //                  smtpclient.Send(message);

                //CommonFunctions.Connection.Close ();

                connection.Close();
                if (AuthenticationManager.Login(social.email, social.id, type) != "")
                {
                    FormsAuthentication.RedirectFromLoginPage(LoginName.Text, false);
                }
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }

        return true;
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
}