using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;

/// <summary>
/// Summary description for SearchProvider
/// </summary>
public class SearchProvider
{
    public static string connString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
    public SearchProvider()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static int getNumbersOf(string keyword, int proptype, int amenitytype, int roomnum)
    {
        int ret = 0;
        try
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                     /*   @keyword nvarchar(200) ='',
                    @proptype int= 0,
                    @roomnum int= 0,
                    @amenityid int= 0
                    */
                    con.Open();
                    SqlCommand cmd = new SqlCommand("uspGetPropertiesNumsWithKeyword", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@keyword", SqlDbType.NVarChar, 200).Value = keyword;
                    cmd.Parameters.Add("@proptype", SqlDbType.Int).Value = proptype;
                    cmd.Parameters.Add("@roomnum", SqlDbType.Int).Value = roomnum;
                    cmd.Parameters.Add("@amenityid", SqlDbType.Int).Value = amenitytype;

 

                    SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Int32.TryParse(reader[0].ToString(), out ret);
                    break;
                }
                    
                    con.Close();

            }
        }
        catch (Exception ex)
        {

        }

        return ret;
    }
    //AjaxPropListSet
    public static AjaxPropListSet getAjaxPropListSet(string keyword, int proptype, int amenitytype, int roomnum,int sorttype, int pagenum)
    {
        AjaxPropListSet prop_set = new AjaxPropListSet();
        prop_set.allnums = getNumbersOf(keyword, proptype, amenitytype, roomnum);
        List<PropertyAmenityInfo> propertyList =new  List<PropertyAmenityInfo>();

        List<PropertyDetailInfo> prop_detail = new List<PropertyDetailInfo>();
        
        
        //  adapter.Fill(customers, "Customers");
        try
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                    /*   @keyword nvarchar(200) ='',
                    @proptype int= 0,
                    @roomnum int= 0,
                    @amenityid int= 0
                    */
                    con.Open();
                    SqlCommand cmd = new SqlCommand("uspGetPropertiesWithKeyword", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@keyword", SqlDbType.NVarChar, 200).Value = keyword;
                    cmd.Parameters.Add("@proptype", SqlDbType.Int).Value = proptype;
                    cmd.Parameters.Add("@roomnum", SqlDbType.Int).Value = roomnum;
                    cmd.Parameters.Add("@amenityid", SqlDbType.Int).Value = amenitytype;
                    cmd.Parameters.Add("@pagenum", SqlDbType.Int).Value = pagenum;
                    cmd.Parameters.Add("@ratesort", SqlDbType.Int).Value = sorttype;
                //   @pagenum int =0,
                //@ratesort int= 0
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    PropertyDetailInfo tmp = new PropertyDetailInfo();
                    PropertyInfo[] props = tmp.GetType().GetProperties();
                    foreach(PropertyInfo prop_info in props)
                    {
                        prop_info.SetValue(tmp, Convert.ChangeType(reader[prop_info.Name], prop_info.PropertyType), null);

                    }
                    prop_detail.Add(tmp);
                }

                con.Close();
              
            }
        }
        catch (Exception ex)
        {

        }

        foreach(PropertyDetailInfo propinfo in prop_detail)
        {
            PropertyAmenityInfo propamenity = new PropertyAmenityInfo();
            propamenity.detail = propinfo;
            try
            {
                using (SqlConnection con = new SqlConnection(connString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("uspGetAmenity", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@propid", SqlDbType.Int).Value = propinfo.ID;

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        AmenityInfo tmp = new AmenityInfo();
                        tmp.ID = Convert.ToInt32(reader["ID"]);
                        tmp.Amenity = reader["Amenity"].ToString();
                        propamenity.amenity.Add(tmp);
                    }

                    con.Close();

                }
            }
            catch (Exception ex)
            {

            }
            propertyList.Add(propamenity);
        }
        prop_set.propertyList = propertyList;
        return prop_set;
    }
    public static DataSet getPropertyAjaxListInfoSet(string keyword, int proptype, int amenitytype, int roomnum)
    {
        DataSet inquiry_set = new DataSet();
        //  adapter.Fill(customers, "Customers");
        try
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter())
                {
                    /*   @keyword nvarchar(200) ='',
                    @proptype int= 0,
                    @roomnum int= 0,
                    @amenityid int= 0
                    */
                    con.Open();
                    SqlCommand cmd = new SqlCommand("uspGetPropertiesWithKeyword", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@keyword", SqlDbType.NVarChar, 200).Value = keyword;
                    cmd.Parameters.Add("@proptype", SqlDbType.Int).Value = proptype;
                    cmd.Parameters.Add("@roomnum", SqlDbType.Int).Value = roomnum;
                    cmd.Parameters.Add("@amenityid", SqlDbType.Int).Value = amenitytype;

                    adapter.SelectCommand = cmd;

                    adapter.Fill(inquiry_set, "PropertyList");

                    con.Close();

                }
            }
        }
        catch (Exception ex)
        {

        }

        return inquiry_set;
    }
    public static DataSet getPropertyListInfoSet(string keyword, int proptype, int amenitytype, int roomnum)
    {
        DataSet inquiry_set = new DataSet();
        //  adapter.Fill(customers, "Customers");
        try
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter())
                {
                    /*   @keyword nvarchar(200) ='',
                    @proptype int= 0,
                    @roomnum int= 0,
                    @amenityid int= 0
                    */
                    con.Open();
                    SqlCommand cmd = new SqlCommand("uspGetPropertiesWithKeyword", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@keyword", SqlDbType.NVarChar,200).Value = keyword;
                    cmd.Parameters.Add("@proptype", SqlDbType.Int).Value = proptype;
                    cmd.Parameters.Add("@roomnum", SqlDbType.Int).Value = roomnum;
                    cmd.Parameters.Add("@amenityid", SqlDbType.Int).Value = amenitytype;

                    adapter.SelectCommand = cmd;

                    adapter.Fill(inquiry_set, "PropertyList");

                    con.Close();

                }
            }
        }
        catch (Exception ex)
        {

        }

        return inquiry_set;
    }

    public static DataSet getPropertyTypeListSet(string keyword)
    {
        DataSet inquiry_set = new DataSet();
        //  adapter.Fill(customers, "Customers");
        try
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter())
                {
                    /*   @keyword nvarchar(200) ='',
                    @proptype int= 0,
                    @roomnum int= 0,
                    @amenityid int= 0
                    */
                    con.Open();
                    SqlCommand cmd = new SqlCommand("uspGetPropertiesTypesWithKeyword", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@keyword", SqlDbType.NVarChar, 200).Value = keyword;
 
                    adapter.SelectCommand = cmd;

                    adapter.Fill(inquiry_set, "PropertyList");

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