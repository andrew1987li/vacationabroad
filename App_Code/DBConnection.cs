using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


/// <summary>
/// Summary description for DBConnection
/// </summary>
public class DBConnection
{


    public SqlConnection con = new SqlConnection();
    public SqlCommand cmd = new SqlCommand();
    public SqlDataAdapter adap = new SqlDataAdapter();
    public SqlDataReader reader;

    private string v_connect;


    public string DBConnectionString
    {
        get
        {
            v_connect = ConfigurationManager.ConnectionStrings["herefordpiesConnectionString1"].ConnectionString;
            return v_connect;
        }
        set
        {
            v_connect = value;
        }
    }
    public DBConnection()
    {
        //con.ConnectionString = "Data Source=.\\SqlEXPRESS;AttachDbFilename=|DataDirectory|\\products.mdf;Integrated Security=True;User Instance=True";

        //con.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;


        con.ConnectionString = ConfigurationManager.ConnectionStrings["herefordpiesConnectionString1"].ConnectionString;
        con.Open();
    }

    public void ExecuteSqlArtificial(string query)
    {

        cmd = new SqlCommand(query, con);
        cmd.ExecuteNonQuery();
    }

    public void ExecuteSqlArtificial(string query, SqlTransaction transaction)
    {
        cmd = new SqlCommand(query, con);
        cmd.Transaction = transaction;
        cmd.ExecuteNonQuery();
    }

    public void ExecuteArtificialNonQuery(string query)
    {
        SqlCommand cmd = new SqlCommand(query);
        cmd.Connection = con;
        cmd.ExecuteNonQuery();
    }

    public void CloseConnection()
    {
        con.Close();
    }

    public SqlDataReader ExecuteRecordSetArtificial(string query)
    {
        cmd = new SqlCommand(query, con);
        reader = cmd.ExecuteReader();
        return reader;
    }

    public SqlDataReader ExecuteRecordSetArtificial(string query, SqlTransaction transaction)
    {
        cmd = new SqlCommand(query, con);
        cmd.Transaction = transaction;
        reader = cmd.ExecuteReader();
        return reader;
    }

    public DataTable GetDataSetArtificial(string query)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        adap = new SqlDataAdapter(query, con);
        adap.Fill(ds);
        return ds.Tables[0];
    }
    public object ExecuteScalarArtificial(string query)
    {
        cmd = new SqlCommand(query, con);

        return cmd.ExecuteScalar();
    }

    public string ClearString(string str)
    {
        str = str.Replace("'", "''");
        return str.Trim().ToString();
    }
    //returns single value
    public string GetSingleArtificial(string v_field, string v_value, string v_table)
    {
        SqlDataReader reader;
        string query = string.Empty;

        query = "select " + v_field + " from " + v_table + " where email = '" + v_value + "'";
        try
        {
            reader = ExecuteRecordSetArtificial(query);
            if (reader.HasRows)
            {

                return reader[v_field].ToString();
            }
            else
            {


            }
        }
        catch (Exception error)
        {

        }
        con.Close();
        return "";

    }

    //checks if value exists
    public bool SingleValueExistsArtificial(string v_value, string v_table, string v_field)
    {
        SqlDataReader reader;
        string query = string.Empty;

        query = "select " + v_field + " from " + v_table + " where email = '" + v_value + "'";
        try
        {
            reader = ExecuteRecordSetArtificial(query);
            if (reader.HasRows)
            {
                CloseConnection();
                return true;
            }
            else
            {
                CloseConnection();
                return false;
            }
        }
        catch (Exception error)
        {
            Console.Write(error);
        }
        con.Close();
        return new bool();

    }//singlevalueexists
    public DataTable CountryPropertiesSP(int countryID)
    {
        DataTable dt = new DataTable();
        try
        {
            SqlCommand command = new SqlCommand("GetPropertiesFromCountryID", con);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            command.Parameters.Add("@CountryID", SqlDbType.Int).Value = countryID;
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            dt = ds.Tables[0];

        }
        catch (Exception ex) { throw ex; }
        finally { CloseConnection(); }
        return dt;
    }
    public void spInsertCurrencies(string abbr, string text)
    {
        try
        {
            SqlCommand command = new SqlCommand("InsertCurrencies", con);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            command.Parameters.Add("@desc", SqlDbType.VarChar).Value = text;
            command.Parameters.Add("@abbr", SqlDbType.NChar).Value = abbr;

            command.ExecuteNonQuery();
        }
        catch (Exception ex) { throw ex; }
        finally { CloseConnection(); }
    }
    public void spUpdateCurrencies(int countryID, string abbr, string text)
    {
        try
        {
            SqlCommand command = new SqlCommand("UpdateCurrencies", con);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            command.Parameters.Add("@ID", SqlDbType.Int).Value = countryID;
            command.Parameters.Add("@desc", SqlDbType.VarChar).Value = text;
            command.Parameters.Add("@abbr", SqlDbType.NChar).Value = abbr;

            command.ExecuteNonQuery();           
        }
        catch (Exception ex) { throw ex; }
        finally { CloseConnection(); }       
    }
    public DataTable spCurrencyList()
    {
        DataTable dt = new DataTable();
        try
        {
            SqlCommand command = new SqlCommand("GetCurrencies", con);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            dt = ds.Tables[0];
        }
        catch (Exception ex) { throw ex; }
        finally { CloseConnection(); }
        return dt;
    }
    public DataTable spVast()
    {
        DataTable dt = new DataTable();
        try
        {
            SqlCommand command = new SqlCommand("Vast", con);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            dt = ds.Tables[0];
        }
        catch (Exception ex) { throw ex; }
        finally { CloseConnection(); }
        return dt;
    }
    public DataTable spLocationInfo(int CityID)
    {
        DataTable dt = new DataTable();
        try
        {
            SqlCommand command = new SqlCommand("GetLocationInformationByCity", con);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            command.Parameters.Add("@state", SqlDbType.Int).Value = CityID;
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            dt = ds.Tables[0];

        }
        catch (Exception ex) { throw ex; }
        finally { CloseConnection(); }
        return dt;
    }
    public DataTable spGetPropertiesByCityID(int vID)
    {
        DataTable dt = new DataTable();
        try
        {
            SqlCommand command = new SqlCommand("GetPropertiesFromCityIDplay", con);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            command.Parameters.Add("@CityID", SqlDbType.Int).Value = vID;
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            dt = ds.Tables[0];

        }
        catch (Exception ex) { throw ex; }
        finally { CloseConnection(); }
        return dt;
    }
    public DataTable spGetPropertiesByStateID(int vID)
    {
        DataTable dt = new DataTable();
        try
        {
            SqlCommand command = new SqlCommand("GetPropertiesFromStateID", con);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            command.Parameters.Add("@StateProvinceID", SqlDbType.Int).Value = vID;
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            dt = ds.Tables[0];

        }
        catch (Exception ex) { throw ex; }
        finally { CloseConnection(); }
        return dt;
    }
    public DataTable spGetPropertiesByCountiesID(int vID)
    {
        DataTable dt = new DataTable();
        try
        {
            SqlCommand command = new SqlCommand("GetPropertiesFromCountiesID", con);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            command.Parameters.Add("@CountiesID", SqlDbType.Int).Value = vID;
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            dt = ds.Tables[0];

        }
        catch (Exception ex) { throw ex; }
        finally { CloseConnection(); }
        return dt;
    }
    public DataTable spGetRightSideCounties(int stateID)
    {
        DataTable dt = new DataTable();
        try
        {
            SqlCommand command = new SqlCommand("GetRightSideCounties", con);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            command.Parameters.Add("@stateID", SqlDbType.Int).Value = stateID;
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            dt = ds.Tables[0];

        }
        catch (Exception ex) { throw ex; }
        finally { CloseConnection(); }
        return dt;
    }
    public DataTable spCountriesByRegions(int regID)
    {
        DataTable dt = new DataTable();
        try
        {
            SqlCommand command = new SqlCommand("GetCountriesByRegion", con);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            command.Parameters.Add("@RegionID", SqlDbType.Int).Value = regID;
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            dt = ds.Tables[0];

        }
        catch (Exception ex) { throw ex; }
        finally { CloseConnection(); }
        return dt;
    }
    public DataTable spStateProvByCountries(int countryID)
    {
        DataTable dt = new DataTable();
        try
        {
            SqlCommand command = new SqlCommand("GetStateProvByCountry", con);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            command.Parameters.Add("@CountryID", SqlDbType.Int).Value = countryID;
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            dt = ds.Tables[0];

        }
        catch (Exception ex) { throw ex; }
        finally { CloseConnection(); }
        return dt;
    }
    public DataTable spPropertiesByCounty(string countyID)
    {
        DataTable dt = new DataTable();
        try
        {
            SqlCommand command = new SqlCommand("GetPropertiesFromCountyID", con);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            command.Parameters.Add("@CountyID", SqlDbType.NVarChar).Value = countyID;
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            dt = ds.Tables[0];

        }
        catch (Exception ex) { throw ex; }
        finally { CloseConnection(); }
        return dt;
    }
    public DataTable spGetCategories()
    {
        DataTable dt = new DataTable();
        try
        {
            SqlCommand command = new SqlCommand("GetPropertyCategory", con);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            dt = ds.Tables[0];

        }
        catch (Exception ex) { throw ex; }
        finally { CloseConnection(); }
        return dt;
    }
    public DataTable spGetPropertyTypes()
    {
        DataTable dt = new DataTable();
        try
        {
            SqlCommand command = new SqlCommand("GetPropertyTypes", con);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            dt = ds.Tables[0];

        }
        catch (Exception ex) { throw ex; }
        finally { CloseConnection(); }
        return dt;
    }

    public void spUpdatePropertyType(string vRename, string vID)
    {
        DataTable dt = new DataTable();
        try
        {
            SqlCommand command = new SqlCommand("UpdatePropertyType", con);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            command.Parameters.Add("@rename", SqlDbType.NVarChar).Value = vRename;
            command.Parameters.Add("@id", SqlDbType.NVarChar).Value = vID;
            command.ExecuteNonQuery();

        }
        catch (Exception ex) { throw ex; }
        finally { CloseConnection(); }        
    }
    public void spDeletePropertyType(string vID)
    {       
        try
        {
            SqlCommand command = new SqlCommand("DeletePropertyType", con);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(command);            
            command.Parameters.Add("@id", SqlDbType.NVarChar).Value = vID;
            command.ExecuteNonQuery();

        }
        catch (Exception ex) { throw ex; }
        finally { CloseConnection(); }
    }
    public void spUpdateCategory(string vID, string vValue)
    {
        try
        {
            SqlCommand command = new SqlCommand("UpdatePropertyCategory", con);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            command.Parameters.Add("@id", SqlDbType.Int).Value = vID;
            command.Parameters.Add("@newCategory", SqlDbType.NVarChar).Value = vValue;

            command.ExecuteNonQuery();

        }
        catch (Exception ex) { throw ex; }
        finally { CloseConnection(); }
    }
    public void spInsertCategory(string vValue)
    {
        try
        {
            SqlCommand command = new SqlCommand("InsertPropertyCategory", con);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            command.Parameters.Add("@newCategory", SqlDbType.NVarChar).Value = vValue;

            command.ExecuteNonQuery();

        }
        catch (Exception ex) { throw ex; }
        finally { CloseConnection(); }
    }
    public void spDeleteCategory(int vID)
    {
        try
        {
            SqlCommand command = new SqlCommand("DeletePropertyCategory", con);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            command.Parameters.Add("@id", SqlDbType.NVarChar).Value = vID;

            command.ExecuteNonQuery();

        }
        catch (Exception ex) { throw ex; }
        finally { CloseConnection(); }
    }
    public void spUpdatePropertyAssoc(string vType, int vCategory)
    {
        try
        {
            SqlCommand command = new SqlCommand("UpdatePropertyTypeAssoc", con);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            command.Parameters.Add("@type", SqlDbType.NVarChar).Value = vType;
            command.Parameters.Add("@category", SqlDbType.Int).Value = vCategory;

            command.ExecuteNonQuery();

        }
        catch (Exception ex) { throw ex; }
        finally { CloseConnection(); }
    }
    public DataTable spSelectPropertyAssoc(int vID)
    {
        DataTable dt = new DataTable();
        try
        {            
            SqlCommand command = new SqlCommand("SelectPropertyTypeAssoc", con);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            command.Parameters.Add("@id", SqlDbType.Int).Value = vID;

            DataSet ds = new DataSet();
            adapter.Fill(ds);
            dt = ds.Tables[0];

        }
        catch (Exception ex) { throw ex; }
        finally { CloseConnection(); }
        return dt;
    }
    public void spDeletePropertyAssoc(string vID)
    {
        try
        {
            SqlCommand command = new SqlCommand("DeletePropertyTypeAssoc", con);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            command.Parameters.Add("@id", SqlDbType.NVarChar).Value = vID;

            command.ExecuteNonQuery();

        }
        catch (Exception ex) { throw ex; }
        finally { CloseConnection(); }
    }
    public bool spSelectPropertyTypeExists(string vName)
    {
        bool vExists = false;
        try
        {
            DataTable dt = new DataTable();
            SqlCommand command = new SqlCommand("GetPropertyTypeExists", con);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(command);            
            command.Parameters.Add("@name", SqlDbType.NVarChar).Value = vName;

            DataSet ds = new DataSet();
            adapter.Fill(ds);
            dt = ds.Tables[0];

            if (dt.Rows.Count > 0)
                vExists = true;
            else
                vExists = false;
        }
        catch (Exception ex) { throw ex; }
        finally { }
        return vExists;
    }
    public bool spSelectPropertyExistsWType(string vType)
    {
        bool vExists = false;
        try
        {
            DataTable dt = new DataTable();
            SqlCommand command = new SqlCommand("GetIfPropertyExistsWTypeID", con);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            command.Parameters.Add("@type", SqlDbType.NVarChar).Value = vType;

            DataSet ds = new DataSet();
            adapter.Fill(ds);
            dt = ds.Tables[0];

            if (dt.Rows.Count > 0)
                vExists = true;
            else
                vExists = false;
        }
        catch (Exception ex) { throw ex; }
        finally { }
        return vExists;
    }
    public bool spSelectPropertyExistsWPriID(int vID)
    {
        bool vExists = false;
        try
        {
            DataTable dt = new DataTable();
            SqlCommand command = new SqlCommand("GetIfPropertyExistsWPriID", con);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            command.Parameters.Add("@id", SqlDbType.Int).Value = vID;

            DataSet ds = new DataSet();
            adapter.Fill(ds);
            dt = ds.Tables[0];

            if (dt.Rows.Count > 0)
                vExists = true;
            else
                vExists = false;
        }
        catch (Exception ex) { throw ex; }
        finally { }
        return vExists;
    }
    public void spInsertPropertyType(string vType, int vCategoryID)
    {
        try
        {
            SqlCommand command = new SqlCommand("GetMaxPropertyTypeID", con);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            int vID = (int)command.ExecuteScalar();
            vID = vID + 1;

            command = new SqlCommand("InsertPropertyTypeWCategory", con);
            command.CommandType = CommandType.StoredProcedure;
            adapter = new SqlDataAdapter(command);
            command.Parameters.Add("@propertyType", SqlDbType.NVarChar).Value = vType;
            command.Parameters.Add("@categoryID", SqlDbType.Int).Value = vCategoryID;
            command.Parameters.Add("@ID", SqlDbType.Int).Value = vID;

            command.ExecuteNonQuery();

        }
        catch (Exception ex) { throw ex; }
        finally { CloseConnection(); }
    }
    public bool spSelectCountyCityExists(int vCountyID)
    {
        bool vExists = false;
        try
        {
            DataTable dt = new DataTable();
            SqlCommand command = new SqlCommand("GetCountyCityExists", con);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            command.Parameters.Add("@countyID", SqlDbType.Int).Value = vCountyID;

            DataSet ds = new DataSet();
            adapter.Fill(ds);
            dt = ds.Tables[0];

            if (dt.Rows.Count > 0)
                vExists = true;
            else
                vExists = false;
        }
        catch (Exception ex) { throw ex; }
        finally { }
        return vExists;
    }
    public bool spSelectCityPropertyExists(int vCityID)
    {
        bool vExists = false;
        try
        {
            DataTable dt = new DataTable();
            SqlCommand command = new SqlCommand("GetCityPropertyExists", con);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            command.Parameters.Add("@cityID", SqlDbType.Int).Value = vCityID;

            DataSet ds = new DataSet();
            adapter.Fill(ds);
            dt = ds.Tables[0];

            if (dt.Rows.Count > 0)
                vExists = true;
            else
                vExists = false;
        }
        catch (Exception ex) { throw ex; }
        finally { }
        return vExists;
    }
    public void spInsertEmailCampaign(string vName, string vEmail, string vPhone, string vPhone2, DateTime vArrivalDate, int vPropertyNum, DateTime vTransDate)
    {
        try
        {
            SqlCommand command = new SqlCommand("InsertEmailCampaign", con);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
           
            command.Parameters.Add("@name", SqlDbType.NVarChar).Value = vName;
            command.Parameters.Add("@email", SqlDbType.NVarChar).Value = vEmail;
            command.Parameters.Add("@phone", SqlDbType.NVarChar).Value = vPhone;
            command.Parameters.Add("@phone2", SqlDbType.NVarChar).Value = vPhone2;
            command.Parameters.Add("@arrivalDate", SqlDbType.DateTime).Value = vArrivalDate;
            command.Parameters.Add("@propertyNumber", SqlDbType.Int).Value = vPropertyNum;
            command.Parameters.Add("@transDate", SqlDbType.DateTime).Value = vTransDate;

            command.ExecuteNonQuery();

        }
        catch (Exception ex) { throw ex; }
        finally { CloseConnection(); }
    }
    public DataTable spSelectCampaignDates()
    {
        DataTable dt = new DataTable();
        try
        {
            SqlCommand command = new SqlCommand("SelectEmailCampaignDates", con);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(command);

            DataSet ds = new DataSet();
            adapter.Fill(ds);
            dt = ds.Tables[0];

        }
        catch (Exception ex) { throw ex; }
        finally { CloseConnection(); }
        return dt;
    }
    public DataTable spSelectCampaignWinners()
    {
        DataTable dt = new DataTable();
        try
        {
            SqlCommand command = new SqlCommand("SelectEmailCampaignWinners", con);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(command);

            DataSet ds = new DataSet();
            adapter.Fill(ds);
            dt = ds.Tables[0];

        }
        catch (Exception ex) { throw ex; }
        finally { CloseConnection(); }
        return dt;
    }
    public DataTable spSelectCampaignDetail(int month, int year)
    {
        DataTable dt = new DataTable();
        try
        {
            SqlCommand command = new SqlCommand("SelectEmailCampaignDetail", con);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            command.Parameters.Add("@Month", SqlDbType.Int).Value = month;
            command.Parameters.Add("@Year", SqlDbType.Int).Value = year;

            DataSet ds = new DataSet();
            adapter.Fill(ds);
            dt = ds.Tables[0];

        }
        catch (Exception ex) { throw ex; }
        finally { CloseConnection(); }
        return dt;
    }
    public DataTable spSelectLocationInfoByPropID(int ID)
    {
        DataTable dt = new DataTable();
        try
        {
            SqlCommand command = new SqlCommand("GetLocationInformationByPropID", con);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            command.Parameters.Add("@PropID", SqlDbType.Int).Value = ID;

            DataSet ds = new DataSet();
            adapter.Fill(ds);
            dt = ds.Tables[0];

        }
        catch (Exception ex) { throw ex; }
        finally { CloseConnection(); }
        return dt;
    }
    public void spUpdateCampaignDetail(int ID, bool vResult)
    {
        try
        {
            SqlCommand command = new SqlCommand("UpdateEmailCampaignDetail", con);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            command.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
            command.Parameters.Add("@value", SqlDbType.Bit).Value = vResult;

            command.ExecuteNonQuery();

        }
        catch (Exception ex) { throw ex; }
        finally { CloseConnection(); }
    }
    public DataTable spSelectCampaignSummary(int vYear)
    {
        DataTable dt = new DataTable();
        try
        {
            SqlCommand command = new SqlCommand("SelectCampaignSummary", con);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            command.Parameters.Add("@year", SqlDbType.Int).Value = vYear;

            DataSet ds = new DataSet();
            adapter.Fill(ds);
            dt = ds.Tables[0];

        }
        catch (Exception ex) { throw ex; }
        finally { CloseConnection(); }
        return dt;
    }
    public DataTable spSelectSingleProperty(int ID)
    {
        DataTable dt = new DataTable();
        try
        {
            SqlCommand command = new SqlCommand("GetPropertySingle", con);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            command.Parameters.Add("@ID", SqlDbType.Int).Value = ID;

            DataSet ds = new DataSet();
            adapter.Fill(ds);
            dt = ds.Tables[0];

        }
        catch (Exception ex) { throw ex; }
        finally { CloseConnection(); }
        return dt;
    }
    public void spInsertComment(int propID, string firstName, string lastName, string comments, DateTime arrivalDate, bool reservation, int rating, DateTime today, string phone)
    {
        try
        {
            SqlCommand command = new SqlCommand("InsertComments", con);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            command.Parameters.Add("@propID", SqlDbType.Int).Value = propID;
            command.Parameters.Add("@firstname", SqlDbType.NVarChar).Value = firstName;
            command.Parameters.Add("@lastname", SqlDbType.NVarChar).Value = lastName;
            command.Parameters.Add("@comments", SqlDbType.NVarChar).Value = comments;
            command.Parameters.Add("@arrivalDate", SqlDbType.DateTime).Value = arrivalDate;
            command.Parameters.Add("@Reservation", SqlDbType.Bit).Value = reservation;
            command.Parameters.Add("@Rating", SqlDbType.Int).Value = rating;
            command.Parameters.Add("@DateEntered", SqlDbType.DateTime).Value = today;
            command.Parameters.Add("@Phone", SqlDbType.NVarChar).Value = phone;

            command.ExecuteNonQuery();

        }
        catch (Exception ex) { throw ex; }
        finally { CloseConnection(); }
    }
    public DataTable spSelectCommentExist(int propID)
    {
        DataTable dt = new DataTable();
        try
        {
            SqlCommand command = new SqlCommand("SelectCommentsExist", con);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            command.Parameters.Add("@propID", SqlDbType.Int).Value = propID;

            DataSet ds = new DataSet();
            adapter.Fill(ds);
            dt = ds.Tables[0];

        }
        catch (Exception ex) { throw ex; }
        finally { CloseConnection(); }
        return dt;
    }
    public DataTable spSelectAllComments()
    {
        DataTable dt = new DataTable();
        try
        {
            SqlCommand command = new SqlCommand("SelectAllComments", con);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(command);

            DataSet ds = new DataSet();
            adapter.Fill(ds);
            dt = ds.Tables[0];

        }
        catch (Exception ex) { throw ex; }
        finally { CloseConnection(); }
        return dt;
    }
    public bool IsNumeric(string vValue)
    {
        try
        {
            Convert.ToInt32(vValue);
            return true;
        }
        catch
        {
            return false;
        }

    }
    //public void spInsertTour(int userID, string name, string name2, int pCity)
    public void spInsertTour(int typeID, int userID, string name, string name2, string address, string description, string rates, int cityID, DateTime dateAdd, string companyName, string contactName, string website, string phone, string mobile, string startHr, string startMin, string amPm, bool mon, bool tue, bool wed, bool thu, bool fri, bool sat, bool sun)
    {
        try
        {
            SqlCommand command = new SqlCommand("InsertTour", con);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            command.Parameters.Add("@typeID", SqlDbType.Int).Value = DBNull.Value;
            command.Parameters.Add("@userID", SqlDbType.Int).Value = userID;
            command.Parameters.Add("@address", SqlDbType.NVarChar).Value = DBNull.Value;
            command.Parameters.Add("@name", SqlDbType.NVarChar).Value = name;
            command.Parameters.Add("@name2", SqlDbType.NVarChar).Value = name2;
            command.Parameters.Add("@description", SqlDbType.NVarChar).Value = DBNull.Value;
            //command.Parameters.Add("@cityID", SqlDbType.Int).Value = pCity;
            command.Parameters.Add("@cityID", SqlDbType.Int).Value = cityID;
            command.Parameters.Add("@dateAdded", SqlDbType.DateTime).Value = DBNull.Value;
            //command.Parameters.Add("@companyname", SqlDbType.NVarChar).Value = companyName;
            //command.Parameters.Add("@contactname", SqlDbType.NVarChar).Value = contactName;
            //command.Parameters.Add("@website", SqlDbType.NVarChar).Value = website;
            //command.Parameters.Add("@telephone", SqlDbType.NVarChar).Value = phone;
            //command.Parameters.Add("@mobile", SqlDbType.NVarChar).Value = mobile;
            command.Parameters.Add("@startHour", SqlDbType.NVarChar).Value = DBNull.Value;
            command.Parameters.Add("@startMinute", SqlDbType.NVarChar).Value = DBNull.Value;
            command.Parameters.Add("@amPm", SqlDbType.NVarChar).Value = DBNull.Value;
            command.Parameters.Add("@mon", SqlDbType.Bit).Value = DBNull.Value;
            command.Parameters.Add("@tue", SqlDbType.Bit).Value = DBNull.Value;
            command.Parameters.Add("@wed", SqlDbType.Bit).Value = DBNull.Value;
            command.Parameters.Add("@thu", SqlDbType.Bit).Value = DBNull.Value;
            command.Parameters.Add("@fri", SqlDbType.Bit).Value = DBNull.Value;
            command.Parameters.Add("@sat", SqlDbType.Bit).Value = DBNull.Value;
            command.Parameters.Add("@sun", SqlDbType.Bit).Value = DBNull.Value;
            command.Parameters.Add("@lowrate", SqlDbType.Int).Value = DBNull.Value;
            command.Parameters.Add("@currencyID", SqlDbType.Int).Value = DBNull.Value;
            command.Parameters.Add("@escorted", SqlDbType.NVarChar).Value = DBNull.Value;
            command.Parameters.Add("@tourLengthHr", SqlDbType.Int).Value = DBNull.Value;
            command.Parameters.Add("@tourLengthMin", SqlDbType.Int).Value = DBNull.Value;

            command.ExecuteNonQuery();

        }
        catch (Exception ex) { throw ex; }
        finally { CloseConnection(); }
    }
}