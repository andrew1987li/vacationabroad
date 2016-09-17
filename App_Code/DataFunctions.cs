using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for DataFunctions
/// </summary>
public class DataFunctions
{
    public DataFunctions()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public DataTable PropertiesByCase(List<string> vCases, int id, string vType)
    {
        DataTable dt = new DataTable();
        string query = "";
        //query for pets only       

        DBConnection obj = new DBConnection();
        try
        {
            if (vType == "City")
            {
                dt = obj.spGetPropertiesByCityID(id);
            }
            else if (vType == "State")
            {
                dt = obj.spGetPropertiesByStateID(id);
            }
            else if (vType == "Counties")
            {
                dt = obj.spGetPropertiesByCountiesID(id);
            }
            else
            {
                dt = obj.CountryPropertiesSP(id);
            }

            //Filter out rows that don't contain filter list items
            DataTable dt2 = dt;
            if (vCases.Contains("PetFriendly"))
            {
                for (int i = dt.Rows.Count - 1; i > -1; i--)
                {
                    if (dt.Rows[i]["PetFriendly"].ToString() == "")
                        dt.Rows.RemoveAt(i);
                }
            }
            if (vCases.Contains("Pool"))
            {
                for (int i = dt.Rows.Count - 1; i > -1; i--)
                {
                    if ((dt.Rows[i]["SharedPool"].ToString() == "") && (dt.Rows[i]["PrivPool"].ToString() == ""))
                        dt.Rows.RemoveAt(i);
                }
            }
            if (vCases.Contains("InternetAccess"))
            {
                for (int i = dt.Rows.Count - 1; i > -1; i--)
                {
                    if (dt.Rows[i]["InternetAccess"].ToString() == "")
                        dt.Rows.RemoveAt(i);
                }
            }
            if (vCases.Contains("HotTub"))
            {
                for (int i = dt.Rows.Count - 1; i > -1; i--)
                {
                    if (dt.Rows[i]["HotTub"].ToString() == "")
                        dt.Rows.RemoveAt(i);
                }
            }
            //
        }
        catch (Exception ex) { throw ex; }
        finally { obj.CloseConnection(); }
        return dt;
    }
    public DataTable PropertiesByCounty(List<string> vCases, int id, string vType)
    {
        DataTable dt = new DataTable();
        string query = "";
        //query for pets only
        DBConnection obj = new DBConnection();
        try
        {
            if (vType == "Country")
            {
                if (vType == "State")
                {
                    dt = VADBCommander.PropertiesByCountyList(id.ToString());
                }
                else if (vType == "City")
                {
                    VADBCommander.PropertiesByCityList(id.ToString());
                }
            }
            else
            {
                dt = VADBCommander.PropertiesByCountryList(id.ToString());
            }


            DataTable dt2 = dt;
            if (vCases.Contains("PetFriendly"))
            {
                for (int i = dt.Rows.Count - 1; i > -1; i--)
                {
                    if (dt.Rows[i]["PetFriendly"].ToString() == "")
                        dt.Rows.RemoveAt(i);
                }
            }
            if (vCases.Contains("Pool"))
            {
                for (int i = dt.Rows.Count - 1; i > -1; i--)
                {
                    if ((dt.Rows[i]["SharedPool"].ToString() == "") && (dt.Rows[i]["PrivPool"].ToString() == ""))
                        dt.Rows.RemoveAt(i);
                }
            }
            if (vCases.Contains("InternetAccess"))
            {
                for (int i = dt.Rows.Count - 1; i > -1; i--)
                {
                    if (dt.Rows[i]["InternetAccess"].ToString() == "")
                        dt.Rows.RemoveAt(i);
                }
            }
            if (vCases.Contains("HotTub"))
            {
                for (int i = dt.Rows.Count - 1; i > -1; i--)
                {
                    if (dt.Rows[i]["HotTub"].ToString() == "")
                        dt.Rows.RemoveAt(i);
                }
            }

        }

        catch (Exception ex)
        {
            throw ex;
        }
        return dt;
    }
    public DataTable FindNumCategories(DataTable vdt)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("category");
        dt.Columns.Add("count",typeof(Int32));

        List<string> lstCategory = new List<string>();

        for (int x = 0; x < vdt.Rows.Count; x++)
        {
            //fill category column with new entries and keep track of count for each
            if (!lstCategory.Contains(vdt.Rows[x]["Category"].ToString()))
            {
                lstCategory.Add(vdt.Rows[x]["Category"].ToString());
                DataRow row = dt.NewRow();
                row["category"] = vdt.Rows[x]["Category"].ToString();
                row["count"] = "1";
                dt.Rows.Add(row);
            }
            else
            {
                //add to count for each
                foreach (DataRow row in dt.Rows)
                {
                    if (row["category"].ToString() == vdt.Rows[x]["Category"].ToString())
                    {
                        int vCount = Convert.ToInt32(row["count"].ToString());
                        vCount = vCount + 1;
                        row["count"] = vCount.ToString();
                    }
                }
            }
        }
        return dt;
    }
    public DataTable FindNumCategoriesTour(DataTable vdt)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("category");
        dt.Columns.Add("count");

        List<string> lstCategory = new List<string>();

        for (int x = 0; x < vdt.Rows.Count; x++)
        {
            //fill category column with new entries and keep track of count for each
            if (!lstCategory.Contains(vdt.Rows[x]["type"].ToString()))
            {
                lstCategory.Add(vdt.Rows[x]["type"].ToString());
                DataRow row = dt.NewRow();
                row["category"] = vdt.Rows[x]["type"].ToString();
                row["count"] = "1";
                dt.Rows.Add(row);
            }
            else
            {
                //add to count for each
                foreach (DataRow row in dt.Rows)
                {
                    if (row["category"].ToString() == vdt.Rows[x]["type"].ToString())
                    {
                        int vCount = Convert.ToInt32(row["count"].ToString());
                        vCount = vCount + 1;
                        row["count"] = vCount.ToString();
                    }
                }
            }
        }
        return dt;
    }
    public DataTable FindNumCategorieswithImage(DataTable Dti)
    {

        DataView dv1 = Dti.DefaultView;
        dv1.Sort = "MinNightRate desc";
        DataTable vdt = new DataTable();
        vdt = dv1.ToTable();
        DataTable dt = new DataTable();
        dt.Columns.Add("category");
        dt.Columns.Add("count", typeof(Int32));
        dt.Columns.Add("PhotoImage");
        List<string> lstCategory = new List<string>();

        for (int x = 0; x < vdt.Rows.Count; x++)
        {
            //fill category column with new entries and keep track of count for each
            if (!lstCategory.Contains(vdt.Rows[x]["Category"].ToString()))
            {
                lstCategory.Add(vdt.Rows[x]["Category"].ToString());
                DataRow row = dt.NewRow();
                row["category"] = vdt.Rows[x]["Category"].ToString();
                row["count"] = "1";
                row["PhotoImage"] = vdt.Rows[x]["PhotoImage"].ToString();
                dt.Rows.Add(row);
            }
            else
            {
                //add to count for each
                foreach (DataRow row in dt.Rows)
                {
                    if (row["category"].ToString() == vdt.Rows[x]["Category"].ToString())
                    {
                        int vCount = Convert.ToInt32(row["count"].ToString());
                        vCount = vCount + 1;
                        row["count"] = vCount.ToString();
                    }
                }
            }
        }

        // dt.DefaultView.Sort = "count desc";
        // dt = dt.DefaultView.ToTable();
        return dt;
    }

    public DataTable FindNumBedrooms(DataTable vdt)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("title");
        dt.Columns.Add("count");
        DataRow row1 = dt.NewRow();
        row1["title"] = "0-1";
        row1["count"] = 0;
        DataRow row2 = dt.NewRow();
        row2["title"] = "3-4";
        row2["count"] = 0;
        DataRow row3 = dt.NewRow();
        row3["title"] = "5+";
        row3["count"] = 0;
        dt.Rows.Add(row1);
        dt.Rows.Add(row2);
        dt.Rows.Add(row3);

        List<string> lstCategory = new List<string>();

        for (int x = 0; x < vdt.Rows.Count; x++)
        {
            //tally 3 rows...0-2, 3-4, 5+
            if (vdt.Rows[x]["numBedrooms"] != null)
            {
                if (Convert.ToInt32(vdt.Rows[x]["numBedrooms"]) < 3)
                    dt.Rows[0]["count"] = Convert.ToInt32(dt.Rows[0]["count"]) + 1;
                else if ((Convert.ToInt32(vdt.Rows[x]["numBedrooms"]) > 2) && (Convert.ToInt32(vdt.Rows[x]["numBedrooms"]) < 5))
                    dt.Rows[1]["count"] = Convert.ToInt32(dt.Rows[1]["count"]) + 1;
                else if (Convert.ToInt32(vdt.Rows[x]["numBedrooms"]) > 4)
                    dt.Rows[2]["count"] = Convert.ToInt32(dt.Rows[2]["count"]) + 1;
            }
        }
        return dt;
    }
    public DataTable FindNumBedroomsTour(DataTable vdt)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("title");
        dt.Columns.Add("count");
        DataRow row1 = dt.NewRow();
        row1["title"] = "Escorted";
        row1["count"] = 0;
        DataRow row2 = dt.NewRow();
        row2["title"] = "Unescorted";
        row2["count"] = 0;
        DataRow row3 = dt.NewRow();
        row3["title"] = "Private";
        row3["count"] = 0;

        dt.Rows.Add(row1);
        dt.Rows.Add(row2);
        dt.Rows.Add(row3);

        List<string> lstCategory = new List<string>();

        for (int x = 0; x < vdt.Rows.Count; x++)
        {
            //tally 3 rows...0-2, 3-4, 5+
            if (vdt.Rows[x]["escorted"] != DBNull.Value)
            {
                if (vdt.Rows[x]["escorted"].ToString() == "escorted")
                    dt.Rows[0]["count"] = Convert.ToInt32(dt.Rows[0]["count"]) + 1;
                else if (vdt.Rows[x]["escorted"].ToString() == "unescorted")
                    dt.Rows[1]["count"] = Convert.ToInt32(dt.Rows[1]["count"]) + 1;
                else if (vdt.Rows[x]["escorted"].ToString() == "private")
                    dt.Rows[2]["count"] = Convert.ToInt32(dt.Rows[2]["count"]) + 1;
            }
        }
        return dt;
    }
}
