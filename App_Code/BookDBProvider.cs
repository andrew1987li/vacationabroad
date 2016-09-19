using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;

//Version 1.0
/*
 * For booking, saves info to the db(SentEmail)
   Return dbset for the userid to request the quote.
     
     */

public class UserInfo
{
    public int id { get; set; }
    public string name { get; set; }
    public string email { get; set; }
    public string lastname { get; set; }

    public UserInfo()
    {
        id = 0; name = "";
        email = ""; lastname = "";
    }
}

public class PropertyInform
{
    public int id { get; set; }
    public string name { get; set; }

    public int userid { get; set; }

}

public class BookDBProvider
{
    public static string connString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
    public BookDBProvider()
    {

    }

    public static PropertyInfo getPropertyInfo(int propid)
    {
        PropertyInfo propinfo = new PropertyInfo();
        try
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("select Name,UserID from Properties  where ID=@id", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = propid;



                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            if (reader.Read())
                            {
                                //more code
                                propinfo.id = propid;
                                propinfo.name = reader[0].ToString();
                                propinfo.userid = Convert.ToInt32(reader[1]);
                            }
                        }

                    }


                    con.Close();

                }
            }

        }
        catch (Exception ex)
        {
            // throw ex;
            // return 0;
        }
        return propinfo;


    }
    public static UserInfo getUserInfo(int user_id) {
        UserInfo userinfo = new UserInfo();
        try
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("select Username,Email,LastName from Users  where ID=@id", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = user_id;



                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            if (reader.Read())
                            {
                                //more code
                                userinfo.id = user_id;
                                userinfo.name = reader[0].ToString();
                                userinfo.email = reader[1].ToString();
                                userinfo.lastname = reader[2].ToString();
                                
                            }
                        }

                    }


                    con.Close();

                }
            }

        }
        catch (Exception ex)
        {
            // throw ex;
            // return 0;
        }
        return userinfo;


    }
    public static object getValue(object par)
    {
        if(par == null)
        {
            return DBNull.Value;
        }
        return par;
    }

    public static bool sendEmailToOwner(string ownername,string owneremail, string name, string email, string arrive,int nights, int adults, int child, string comment, string telephone, string propname)
    {
        Regex regex = new Regex("^\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$");

        // SmtpClient smtpclient = new SmtpClient(ConfigurationManager.AppSettings["SMTPServer"],
        //   int.Parse(ConfigurationManager.AppSettings["SMTPPort"]));
        SmtpClient smtpclient = new SmtpClient("mail.vacations-abroad.com", 25);

        //MailMessage message = new MailMessage (IfShowContactInfo () ?
        //    ContactEmail.Text : "ar@" + CommonFunctions.GetDomainName (), (string)PropertiesFullSet.Tables["Properties"].Rows[0]["Email"]);
        // MailMessage message = new MailMessage("prop@vacations-abroad.com", (string)PropertiesFullSet.Tables["Properties"].Rows[0]["Email"]);

        string mailbody = "Dear {0}!<br> This is an inquiry for your property {1} on vocation-abroad.com.<br>" +
            "Deatiled inquiry:<br>"
            + "UserName:{2}<br> UserEmail:{3} <br> ArrivalDate:{4} <br> Nights:{5}<br> Adults:{6} Children:{7}<br>Telephone:{8} Comment:{9}<br>";

        string emailbody = String.Format(mailbody, ownername, propname, name,email, arrive, nights,adults, child,telephone,comment);

        // MailMessage message = new MailMessage(regex.Match(EmailAddress.Text).Success ?
        //   EmailAddress.Text : "admin@" + CommonFunctions.GetDomainName(), ConfigurationManager.AppSettings["NewOwnerEmail"]);
        MailMessage message = new MailMessage("prop@vacations-abroad.com", owneremail);
        message.Subject = "New Inquiry received.";
        message.Body = emailbody;
        message.IsBodyHtml = true;

        message.Body = message.Body.Replace("\r", "").Replace("\n", Environment.NewLine);
        //message.Headers["Content-Type"] = "text/plain; charset = \"iso-8859-1\"";

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

    public static bool sendEmailToAdmin(string ownername, string owneremail, string name, string email, string arrive, int nights, int adults, int child, string comment, string telephone, string propname)
    {
        Regex regex = new Regex("^\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$");

        // SmtpClient smtpclient = new SmtpClient(ConfigurationManager.AppSettings["SMTPServer"],
        //   int.Parse(ConfigurationManager.AppSettings["SMTPPort"]));
        SmtpClient smtpclient = new SmtpClient("mail.vacations-abroad.com", 25);

        //MailMessage message = new MailMessage (IfShowContactInfo () ?
        //    ContactEmail.Text : "ar@" + CommonFunctions.GetDomainName (), (string)PropertiesFullSet.Tables["Properties"].Rows[0]["Email"]);
        // MailMessage message = new MailMessage("prop@vacations-abroad.com", (string)PropertiesFullSet.Tables["Properties"].Rows[0]["Email"]);

        string mailbody = "Dear {0}!<br> This is an inquiry for your property {1} on vocation-abroad.com.<br>" +
            "Deatiled inquiry:<br>"
            + "UserName:{2}<br> UserEmail:{3} <br> ArrivalDate:{4} <br> Nights:{5}<br> Adults:{6} Children:{7}<br>Telephone:{8} Comment:{9}<br>"
            +"<br>Property OwnerName:{10}<br> Email:{11}";

        string emailbody = String.Format(mailbody, "Admin", propname, name, email, arrive, nights, adults, child, telephone, comment,ownername, owneremail);

        // MailMessage message = new MailMessage(regex.Match(EmailAddress.Text).Success ?
        //   EmailAddress.Text : "admin@" + CommonFunctions.GetDomainName(), ConfigurationManager.AppSettings["NewOwnerEmail"]);
        MailMessage message = new MailMessage("prop@vacations-abroad.com", "prop@vacations-abroad.com");
        message.Subject = "New Inquiry received.";
        message.Body = emailbody;
        message.IsBodyHtml = true;

        message.Body = message.Body.Replace("\r", "").Replace("\n", Environment.NewLine);
        //message.Headers["Content-Type"] = "text/plain; charset = \"iso-8859-1\"";

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

    public static bool sendEmailToTraveler(string ownername, string owneremail, string name, string email, string arrive, int nights, int adults, int child, string comment, string telephone, string propname)
    {
        Regex regex = new Regex("^\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$");

        // SmtpClient smtpclient = new SmtpClient(ConfigurationManager.AppSettings["SMTPServer"],
        //   int.Parse(ConfigurationManager.AppSettings["SMTPPort"]));
        SmtpClient smtpclient = new SmtpClient("mail.vacations-abroad.com", 25);

        //MailMessage message = new MailMessage (IfShowContactInfo () ?
        //    ContactEmail.Text : "ar@" + CommonFunctions.GetDomainName (), (string)PropertiesFullSet.Tables["Properties"].Rows[0]["Email"]);
        // MailMessage message = new MailMessage("prop@vacations-abroad.com", (string)PropertiesFullSet.Tables["Properties"].Rows[0]["Email"]);

        string mailbody = "Dear {0}! Thanks for visiting our vocation abroad.<br> This is an inquiry for the property {1} on vocation-abroad.com.<br>" +
            "Deatiled inquiry:<br>"
            + "UserName:{2}<br> UserEmail:{3} <br> ArrivalDate:{4} <br> Nights:{5}<br> Adults:{6} Children:{7}<br>Telephone:{8} Comment:{9}<br>";

        string emailbody = String.Format(mailbody, ownername, propname, name, email, arrive, nights, adults, child, telephone, comment);

        // MailMessage message = new MailMessage(regex.Match(EmailAddress.Text).Success ?
        //   EmailAddress.Text : "admin@" + CommonFunctions.GetDomainName(), ConfigurationManager.AppSettings["NewOwnerEmail"]);
        MailMessage message = new MailMessage("prop@vacations-abroad.com", owneremail);
        message.Subject = "New Inquiry received.";
        message.Body = emailbody;
        message.IsBodyHtml = true;

        message.Body = message.Body.Replace("\r", "").Replace("\n", Environment.NewLine);
        //message.Headers["Content-Type"] = "text/plain; charset = \"iso-8859-1\"";

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

    public static int getUsrIDbyProperty(int propid)
    {
        int ret_val = 0;
        try
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("select UserID from Properties  where ID=@id", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = propid;


                    
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            if (reader.Read())
                            {
                                //more code
                                ret_val =Convert.ToInt32( reader[0]);
                            }
                        }
                        
                    }


                    con.Close();

                }
            }

        }
        catch (Exception ex)
        {
            // throw ex;
           // return 0;
        }
        return ret_val;
    }

    public static bool addEmailQuote(string name, string email, string arrive, int adults, int child, string comment, string telephone, int userid, int propid, int ownerid, int nights)
    {
        try
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("dbo.uspAddEmailQuote", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@SentTime", SqlDbType.DateTime).Value = DateTime.Now;
                    cmd.Parameters.Add("@ContactorName", SqlDbType.NVarChar, 50).Value = getValue(name) ;
                    cmd.Parameters.Add("@ContactorEmail", SqlDbType.NVarChar, 300).Value = getValue(email);
                    cmd.Parameters.Add("@ArrivalDate", SqlDbType.DateTime).Value = getValue(arrive);
                    cmd.Parameters.Add("@Adults", SqlDbType.Int).Value = getValue(adults);
                    cmd.Parameters.Add("@Children", SqlDbType.Int).Value =getValue( child);
                    cmd.Parameters.Add("@Comment", SqlDbType.NVarChar, 500).Value = getValue(comment);
                    cmd.Parameters.Add("@Phone", SqlDbType.NVarChar,50).Value = getValue(telephone);
                    cmd.Parameters.Add("@UserID", SqlDbType.BigInt).Value = getValue(userid);
                    cmd.Parameters.Add("@PropertyID", SqlDbType.Int).Value = getValue(propid);
                    cmd.Parameters.Add("@PropertyOwnerID", SqlDbType.Int).Value = getValue(ownerid);
                    cmd.Parameters.Add("@Nights", SqlDbType.Int).Value = getValue(nights);

                    int rows = cmd.ExecuteNonQuery();

                    /*
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            if (reader.Read())
                            {
                                //more code
                            }
                        }
                    }*/


                    con.Close();
                }
            }

        }
        catch(Exception ex)
        {
           // throw ex;
            return false;
        }


        return true;
    }


}