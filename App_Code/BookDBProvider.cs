using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

//Version 1.0
/*
 * For booking, saves info to the db(SentEmail)
   Return dbset for the userid to request the quote.
     
     */
public class BookDBProvider
{
    public static string connString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
    public BookDBProvider()
    {

    }

    public static object getValue(object par)
    {
        if(par == null)
        {
            return DBNull.Value;
        }
        return par;
    }

    public static bool addEmailQuote(string name, string email, string arrive, int adults, int child, string comment, string telephone, int userid, int ownerid, int propid)
    {
        try
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("uspAddEmailQuote", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@SentTime", SqlDbType.DateTime).Value = DateTime.Now;
                    cmd.Parameters.Add("@ContactorName", SqlDbType.NVarChar, 50).Value = getValue(name) ;
                    cmd.Parameters.Add("@ContactorEmail", SqlDbType.NVarChar, 300).Value = getValue(email);
                    cmd.Parameters.Add("@ArrivalDate", SqlDbType.DateTime).Value = arrive;
                    cmd.Parameters.Add("@Adults", SqlDbType.Int).Value = adults;
                    cmd.Parameters.Add("@Children", SqlDbType.Int).Value = child;
                    cmd.Parameters.Add("@Comment", SqlDbType.NVarChar, 500).Value = comment;
                    cmd.Parameters.Add("@Telephone", SqlDbType.NVarChar,50).Value = telephone;
                    cmd.Parameters.Add("@UserID", SqlDbType.BigInt).Value = userid;
                    cmd.Parameters.Add("@OwnerID", SqlDbType.BigInt).Value = ownerid;
                    cmd.Parameters.Add("@PropertyID", SqlDbType.Int).Value = propid;

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
                }
            }

        }
        catch
        {
            return false;
        }


        return true;
    }


}