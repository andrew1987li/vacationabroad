using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for AjaxProvider
/// </summary>
public class AjaxProvider
{
    public static string connString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
    public AjaxProvider()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static DataSet getCountryInfoSet(int regionid)
    {
                  DataSet inquiry_set = new DataSet();
                //  adapter.Fill(customers, "Customers");
                try
                {
                    using (SqlConnection con = new SqlConnection(connString))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter())
                        {
                            con.Open();
                            SqlCommand cmd = new SqlCommand("uspGetCountryList", con);
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add("@RegionID", SqlDbType.Int).Value = regionid;

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

    public static AjaxCountryList getCountryInfo(int regionid)
    {
        AjaxCountryList ajaxlist = new AjaxCountryList();
        ajaxlist.regionid = regionid;
        //uspGetCountryInfo  @PropID
        List<Ajaxcountryinfo> list = new List<Ajaxcountryinfo>();
        try
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("uspGetCountryList", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@RegionID", SqlDbType.Int).Value = regionid;



                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Ajaxcountryinfo tmp = new Ajaxcountryinfo();
                            tmp.id = Convert.ToInt32(reader[0]);
                            tmp.name = reader[1].ToString();
                            list.Add(tmp);
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
        ajaxlist.statelist = list;
        return ajaxlist;
    }
    public static AjaxStateList getSateInfo(int countryid)
    {
        AjaxStateList ajaxlist = new AjaxStateList();
        ajaxlist.countryid = countryid;
        //uspGetCountryInfo  @PropID
        List<AjaxStateInfo> list = new List<AjaxStateInfo>();
        try
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("uspGetStateList", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = countryid;


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            AjaxStateInfo tmp = new AjaxStateInfo();
                            tmp.id = Convert.ToInt32(reader[0]);
                            tmp.name = reader[1].ToString();
                            list.Add(tmp);
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
        ajaxlist.statelist = list;
        return ajaxlist;
    }
}


