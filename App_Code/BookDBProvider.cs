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

public class CountryInfo
{
    public string country { get; set; }
    public string state { get; set; }
    public string city { get; set; }
    public CountryInfo()
    {
        country = "";
        state = "";
        city = "";
    }
}
public class InquiryInfo
{
    public int id { get; set; }
    public DateTime SentTime { get; set; }
    public string ContactorName { get; set; }
    public string ContactorEmail { get; set; }
    public string ArrivalDate { get; set; }
    public int Adults { get; set; }
    public int Children { get; set; }
    public string Telephone { get; set; }
    public int UserID { get; set; }
    public int PropertyID { get; set; }
    public int Nights { get; set; }

    public int PropertyOwnerID { get; set; }
       //,[DepartDate]

}

public class UserInfo
{
    public int id { get; set; }
    public string name { get; set; }
    public string email { get; set; }
    public string lastname { get; set; }
    public string firstname { get; set; }
    public string PrimaryTelephone { get; set; }

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

    public static CountryInfo getCountryInfo(int prop_id)
    {
        //uspGetCountryInfo  @PropID
        CountryInfo country_info = new CountryInfo();
        try
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("uspGetCountryInfo", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@PropID", SqlDbType.Int).Value = prop_id;



                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            if (reader.Read())
                            {
                                //more code
                                country_info.country = reader[0].ToString();
                                country_info.state = reader[1].ToString();
                                country_info.city = reader[2].ToString();
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
        return country_info;
    }

    public static DataSet getPropertySet(int ownerid)
    {
        /*
   [ID]    ,[UserID]     ,[Name]     ,[TypeID]    ,[Address]    ,[IfShowAddress]     ,[NumBedrooms]    ,[NumBaths]     ,[NumSleeps]     ,[MinimumNightlyRentalID]     ,[NumTVs]
      ,[NumVCRs]      ,[NumCDPlayers]      ,[Description]      ,[Amenities]      ,[LocalAttractions]      ,[Rates]      ,[CancellationPolicy]
      ,[DepositRequired]      ,[IfMoreThan7PhotosAllowed]      ,[IfApproved]      ,[CityID]      ,[IfFinished]      ,[VirtualTour]
      ,[RatesTable]      ,[PricesCurrency]      ,[CheckIn]      ,[CheckOut]      ,[LodgingTax]      ,[TaxIncluded]      ,[DateAdded]
      ,[DateStartViewed]      ,[DateAvailable]      ,[IfDiscounted]      ,[IfLastMinuteCancellations]      ,[LastMinuteComments]
      ,[HomeExchangeCityID1]      ,[HomeExchangeCityID2]      ,[HomeExchangeCityID3]      ,[HomeExchangeComments]      ,[IfFreeTrialExpirationSent]
      ,[Name2]      ,[MinNightRate]      ,[MinRateCurrency]      ,[HiNightRate]      ,[PriType]
  */

        //  SqlDataAdapter adapter = new SqlDataAdapter(queryString, connection);

        DataSet inquiry_set = new DataSet();
        //  adapter.Fill(customers, "Customers");
        try
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter())
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("select * from Properties em where UserID=@id", con);
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = ownerid;

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


    public static DataSet getInquiryInfoSet(int userid, int type)
    {
        /*
  * [ID]
       ,[SentTime]
       ,[ContactorName]
       ,[ContactorEmail]
       ,[ArrivalDate]
       ,[DepartDate]
       ,[Adults]
       ,[Children]
       ,[Comment]
       ,[Telephone]
       ,[UserID]
       ,[PropertyID]
       ,[PropertyOwnerID]
       ,[Nights]
       IfReplied
  */

        //  SqlDataAdapter adapter = new SqlDataAdapter(queryString, connection);

        DataSet inquiry_set = new DataSet();
      //  adapter.Fill(customers, "Customers");
        try
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter())
                {
                    con.Open();
                    string sql = "";
                    if (type == 0) sql = "select * from EmailQuote em where PropertyOwnerID=@id";
                    else if (type == 1) sql = "select * from EmailQuote em where UserID=@id";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = userid;

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


    // PHas to add the parameter PropertyOwnerID=userid
    public static InquiryInfo getQuoteInfo(int quoteid)
    {
        InquiryInfo propinfo = new InquiryInfo();
        try
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("select * from EmailQuote where ID=@id", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = quoteid;



                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            if (reader.Read())
                            {
                                //SentTime,ContactorName,ContactorEmail,ArrivalDate,DepartDate,Adults,Children,Telephone,UserID,PropertyID,PropertyOwnerID,Nights,IfReplied
                                propinfo.id = quoteid;
                                propinfo.ContactorName = reader["ContactorName"].ToString();
                                propinfo.ContactorEmail = reader["ContactorEmail"].ToString();
                                propinfo.Adults = Convert.ToInt32(reader["Adults"]);
                                propinfo.UserID = Convert.ToInt32(reader["UserID"]);
                                propinfo.PropertyID = Convert.ToInt32(reader["PropertyID"]);
                                propinfo.Nights = Convert.ToInt32(reader["Nights"]);
                                propinfo.ArrivalDate = Convert.ToDateTime(reader["ArrivalDate"]).ToString("yyyy-MM-dd");
                                propinfo.PropertyOwnerID= Convert.ToInt32(reader["PropertyOwnerID"]);
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

    public static string DoFormat(decimal myNumber)
    {
        var s = string.Format("{0:0.00}", myNumber);

        if (s.EndsWith("00"))
        {
            return ((int)myNumber).ToString();
        }
        else
        {
            return s;
        }
    }

    public static PropertyInform getPropertyInfo(int propid)
     {
         PropertyInform propinfo = new PropertyInform();
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
                 using (SqlCommand cmd = new SqlCommand("select Username,Email,LastName,FirstName,PrimaryTelephone from Users  where ID=@id", con))
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
                                 userinfo.firstname = reader[3].ToString();
                                userinfo.PrimaryTelephone = reader[4].ToString();
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
         MailMessage message = new MailMessage("noreply@vacations-abroad.com", owneremail);
         message.Subject = "New Inquiry received.";
         message.Body = emailbody;
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

         string emailbody = String.Format(mailbody, "Linda", propname, name, email, arrive, nights, adults, child, telephone, comment,ownername, owneremail);

         // MailMessage message = new MailMessage(regex.Match(EmailAddress.Text).Success ?
         //   EmailAddress.Text : "admin@" + CommonFunctions.GetDomainName(), ConfigurationManager.AppSettings["NewOwnerEmail"]);
         MailMessage message = new MailMessage("noreply@vacations-abroad.com", "prop@vacations-abroad.com");
         message.Subject = "New Inquiry received.";
         message.Body = emailbody;
         message.IsBodyHtml = true;

         message.Body = message.Body.Replace("\r", "").Replace("\n", Environment.NewLine);
         //message.Headers["Content-Type"] = "text/plain; charset = \"iso-8859-1\"";

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

     public static bool sendEmailToTraveler(string ownername, string owneremail, string name, string email, string arrive, int nights, int adults, int child, string comment, string telephone, string propname)
     {
         Regex regex = new Regex("^\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$");

         // SmtpClient smtpclient = new SmtpClient(ConfigurationManager.AppSettings["SMTPServer"],
         //   int.Parse(ConfigurationManager.AppSettings["SMTPPort"]));
          SmtpClient smtpclient = new SmtpClient("mail.vacations-abroad.com", 25);

          //MailMessage message = new MailMessage (IfShowContactInfo () ?
          //    ContactEmail.Text : "ar@" + CommonFunctions.GetDomainName (), (string)PropertiesFullSet.Tables["Properties"].Rows[0]["Email"]);
          // MailMessage message = new MailMessage("prop@vacations-abroad.com", (string)PropertiesFullSet.Tables["Properties"].Rows[0]["Email"]);

          string mailbody = "Dear {0}! <br>Thanks for visiting our vocation abroad.<br> This is an inquiry for the property {1} on vocation-abroad.com.<br>" +
              "Deatiled inquiry:<br>"
              + "UserName:{2}<br> UserEmail:{3} <br> ArrivalDate:{4} <br> Nights:{5}<br> Adults:{6} Children:{7}<br>Telephone:{8} Comment:{9}<br>";

          string emailbody = String.Format(mailbody, ownername, propname, name, email, arrive, nights, adults, child, telephone, comment);

         // MailMessage message = new MailMessage(regex.Match(EmailAddress.Text).Success ?
         //   EmailAddress.Text : "admin@" + CommonFunctions.GetDomainName(), ConfigurationManager.AppSettings["NewOwnerEmail"]);
         // MailMessage message = new MailMessage(new MailAddress("noreply@vacations-abroad.com"), new MailAddress( owneremail));
         MailMessage message = new MailMessage("noreply@vacations-abroad.com", owneremail);
         message.Subject = "You've sent the inquiry on vacation abroad";
          message.Body = emailbody;
          message.IsBodyHtml = true;

          message.Body = message.Body.Replace("\r", "").Replace("\n", Environment.NewLine);
          //message.Headers["Content-Type"] = "text/plain; charset = \"iso-8859-1\"";

          smtpclient.Credentials = new System.Net.NetworkCredential("noreply@vacations-abroad.com", System.Configuration.ConfigurationManager.AppSettings["smtpCredential"].ToString());
         //smtpclient.UseDefaultCredentials = false;

         try
         {
             smtpclient.Send(message);
         }
         catch (Exception ex)
         {
             //  throw ex;
             return false;
         }
         /*SmtpClient smtpclient = new SmtpClient("mail.vacations-abroad.com", 25);

         MailMessage message = new MailMessage(new MailAddress("noreply@" + CommonFunctions.GetDomainName()), new MailAddress( owneremail));
         message.Subject = "test";
         message.Body = "dear , this is the test";
         message.IsBodyHtml = false;

         message.Body = message.Body.Replace("\r", "").Replace("\n", Environment.NewLine);
         message.Headers["Content-Type"] = "text/plain; charset = \"iso-8859-1\"";
         smtpclient.Credentials = new System.Net.NetworkCredential("noreply@vacations-abroad.com", System.Configuration.ConfigurationManager.AppSettings["smtpCredential"].ToString());
         smtpclient.Send(message);*/
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

    public static bool updateEmailQuoteState(int quoteid)
    {
        try
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("update EmailQuote set IfReplied =1 where ID=@id", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = getValue(quoteid);

                    int rows = cmd.ExecuteNonQuery();

                    con.Close();
                }
            }

        }
        catch (Exception ex)
        {
            // throw ex;
            return false;
        }


        return true;
    }


    public static bool addEmailResponse(int userid, int travelerid, int quoteid, decimal nightrate, decimal sum, decimal cleanfee, decimal securitydep,
        decimal loadingtax, decimal balance, decimal can30, decimal can60, decimal can90, DateTime datereplied, int validdays, int currencytype, decimal taxrate)
    {
        //@UserID, @TravelerID, @QuoteID, @NightRate, @Sum, @CleaningFee, @SecurityDeposit
	//,@LoadingTax, @Balance, @Cancel30,@Cancel60, @Cancel90, @DateReplied,@IsValid
        try
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("dbo.uspAddEmailResponse", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@UserID", SqlDbType.BigInt).Value = userid;
                    cmd.Parameters.Add("@TravelerID", SqlDbType.BigInt).Value = travelerid;
                    cmd.Parameters.Add("@QuoteID", SqlDbType.BigInt).Value = getValue(quoteid);
                    cmd.Parameters.Add("@NightRate", SqlDbType.Decimal).Value = getValue(nightrate);
                    cmd.Parameters.Add("@Sum", SqlDbType.Decimal).Value = getValue(sum);
                    cmd.Parameters.Add("@CleaningFee", SqlDbType.Decimal).Value = getValue(cleanfee);
                    cmd.Parameters.Add("@SecurityDeposit", SqlDbType.Decimal).Value = getValue(securitydep);
                    cmd.Parameters.Add("@LoadingTax", SqlDbType.Decimal).Value = getValue(loadingtax);
                    cmd.Parameters.Add("@Balance", SqlDbType.Decimal).Value = getValue(balance);
                    cmd.Parameters.Add("@Cancel30", SqlDbType.Decimal).Value = getValue(can30);
                    cmd.Parameters.Add("@Cancel60", SqlDbType.Decimal).Value = getValue(can60);
                    cmd.Parameters.Add("@Cancel90", SqlDbType.Decimal).Value = getValue(can90);
                    cmd.Parameters.Add("@DateReplied", SqlDbType.DateTime).Value = getValue(datereplied);
                    cmd.Parameters.Add("@IsValid", SqlDbType.Int).Value = getValue(validdays);
                    cmd.Parameters.Add("@CurrencyType", SqlDbType.Int).Value = getValue(currencytype);
                    cmd.Parameters.Add("@LoadingTaxRate", SqlDbType.Int).Value = getValue(taxrate);
                    int rows = cmd.ExecuteNonQuery();

                    con.Close();
                }
            }

        }
        catch (Exception ex)
        {
            // throw ex;
            return false;
        }


        return true;
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