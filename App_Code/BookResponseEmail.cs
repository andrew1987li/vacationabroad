using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;


public class EmailResponseInfo
{         //UserID, TravelerID, QuoteID, NightRate, [Sum], CleaningFee, SecurityDeposit
          //,LoadingTax, Balance, Cancel30,Cancel60, Cancel90, DateReplied,IsValid, CurrencyType
    public int ID { get; set; }
    public int UserID { get; set; }
    public int TravelerID { get; set; }
    public int QuoteID { get; set; }
    public decimal NightRate { get; set; }
    public decimal Sum { get; set; }
    public decimal CleaningFee { get; set; }
    public decimal SecurityDeposit { get; set; }
    public decimal LoadingTax { get; set; }
    public decimal Balance { get; set; }
    public decimal Cancel30 { get; set; }
    public decimal Cancel60 { get; set; }
    public decimal Cancel90 { get; set; }
    public DateTime DateReplied { get; set; }

    public int IsValid { get; set; }
    public int CurrencyType { get; set; }
    public decimal LoadingTaxRate { get; set; }
    public EmailResponseInfo()
    {
        ID = 0;
    }
}

/// <summary>
/// Summary description for BookResponseEmail
/// </summary>
public class BookResponseEmail
{
    public static string connString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
    public BookResponseEmail()
    {
 
    }

    public static bool sendEmail(string dest_email, string msg,string subject) 
    {
        Regex regex = new Regex("^\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$");
        //   int.Parse(ConfigurationManager.AppSettings["SMTPPort"]));
        SmtpClient smtpclient = new SmtpClient("mail.vacations-abroad.com", 25);

        MailMessage message = new MailMessage("noreply@vacations-abroad.com", dest_email);
        message.Subject = subject;
        message.Body = msg;
        message.IsBodyHtml = true;

        message.Body = message.Body.Replace("\r", "").Replace("\n", Environment.NewLine);
        // message.Headers["Content-Type"] = "text/plain; charset = \"iso-8859-1\"";

        smtpclient.Credentials = new System.Net.NetworkCredential("noreply@vacations-abroad.com", System.Configuration.ConfigurationManager.AppSettings["smtpCredential"].ToString());
        //smtpclient.UseDefaultCredentials = false;


        try
        {
            smtpclient.Send(message);
        }
        catch (Exception ex)
        {
            //throw ex;
            return false;
        }
        return true;
    }

    public static object getValue(object par)
    {
        if (par == null)
        {
            return DBNull.Value;
        }
        return par;
    }

    public static bool updateEmailResponseState(int respid)
    {
        int rows = 0;
        try
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("update EmailResponse set IsQuoted =1 where ID=@id and IsValid>0", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = getValue(respid);

                     rows = cmd.ExecuteNonQuery();

                    con.Close();
                }
            }

        }
        catch (Exception ex)
        {
            // throw ex;
            return false;
        }

        return (rows > 0) ? true : false;
    }

    public static EmailResponseInfo getResponseInfo(int id)
    {
        EmailResponseInfo prop_info = new EmailResponseInfo();
        try
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("select * from EmailResponse where ID=@id", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;



                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            if (reader.Read())
                            {
                                //SentTime,ContactorName,ContactorEmail,ArrivalDate,DepartDate,Adults,Children,Telephone,UserID,PropertyID,PropertyOwnerID,Nights,IfReplied
                                PropertyInfo[] propertys = prop_info.GetType().GetProperties();
                                foreach(PropertyInfo info in propertys)
                                {
                                    info.SetValue(prop_info, Convert.ChangeType(reader[info.Name], info.PropertyType), null);
                                        //propertyInfo.SetValue(ship, Convert.ChangeType(value, propertyInfo.PropertyType), null);
                                }
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
        return prop_info;


    }


    public static DataSet getResponseInfoSet(int user_id, int type)
    {//0:owner  1://traveler
         //  SqlDataAdapter adapter = new SqlDataAdapter(queryString, connection);
         //UserID, TravelerID, QuoteID, NightRate, [Sum], CleaningFee, SecurityDeposit
	//,LoadingTax, Balance, Cancel30,Cancel60, Cancel90, DateReplied,IsValid, CurrencyType
        DataSet inquiry_set = new DataSet();
        //  adapter.Fill(customers, "Customers");
        try
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter())
                {
                    con.Open();
                    string sql;
                    if (type == 0) sql = "select em.*,EmailQuote.PropertyID from EmailQuote join   (select * from EmailResponse where UserID=@id) em on EmailQuote.ID=em.QuoteID";
                    else sql = "select em.*,EmailQuote.PropertyID from EmailQuote join   (select * from EmailResponse where TravelerID=@id) em on EmailQuote.ID=em.QuoteID";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = user_id;

                    adapter.SelectCommand = cmd;

                    adapter.Fill(inquiry_set, "InquiryList");

                    con.Close();

                }
            }
        }
        catch (Exception ex)
        {

        }

        return inquiry_set;
    }
}