using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class EmailCampaign : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        IPSection();
        if (!IsPostBack)
        {
            DBConnection obj = new DBConnection();
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("year");
                dt.Columns.Add("jan");
                dt.Columns.Add("feb");
                dt.Columns.Add("mar");
                dt.Columns.Add("apr");
                dt.Columns.Add("may");
                dt.Columns.Add("jun");
                dt.Columns.Add("jul");
                dt.Columns.Add("aug");
                dt.Columns.Add("sep");
                dt.Columns.Add("oct");
                dt.Columns.Add("nov");
                dt.Columns.Add("dec");

                DataTable dtDates = obj.spSelectCampaignDates();

                List<int> lstTemp = new List<int>();
                //make a row for each year in table
                foreach (DataRow row1 in dtDates.Rows)
                {
                    DateTime tempYear = Convert.ToDateTime(row1["transDate"]);
                    int Year = tempYear.Year;
                    if (!lstTemp.Contains(Year))
                    {
                        lstTemp.Add(Year);
                    }
                }

                for (int i = 0; i < lstTemp.Count; i++)
                {
                    DataRow row = dt.NewRow();
                    row["year"] = lstTemp[i].ToString();
                    dt.Rows.Add(row);
                }

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int x = 0; x < dtDates.Rows.Count; x++)
                    {
                        if (lstTemp[i] == Convert.ToDateTime(dtDates.Rows[x]["transDate"]).Year)
                        {
                            dt.Rows[i][Convert.ToDateTime(dtDates.Rows[x]["transDate"]).Month] = Convert.ToDateTime(dtDates.Rows[x]["transDate"]).ToString("MMMM");
                        }
                    }

                }
                //fill in months for table

                grdSummary.DataSource = dt;
                grdSummary.DataBind();
            }
            catch (Exception ex) { lblInfo.Text = ex.Message; }
            finally { obj.CloseConnection(); }
        }
    }
    protected void grdSummary_DataBound(object sender, EventArgs e)
    {

    }
    private void IPSection()
    {
        SqlConnection conn = new SqlConnection();
        try
        {
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["herefordpiesConnectionString1"].ConnectionString;
            conn.Open();
            SqlCommand cmd = new SqlCommand("[herefordpies_test].[accesspies].[getIPpercent]", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            if (ds.Tables.Count > 0)
            {
                string percent = "";
                string curProperty = "";

                percent = ds.Tables[0].Rows[0][0].ToString();
                curProperty = ds.Tables[1].Rows[0][0].ToString();

                if (percent != "100")
                    lblDisplay.Text = "The IP program is " + percent + "% complete and is currently on the user/email: " +
                        curProperty + ".";
                else
                    lblDisplay.Text = "The IP program is 100% complete for this week and will begin again on Saturday.";
            }

        }
        catch (Exception ex)
        {
            lblInfo.Text = ex.ToString();
        }
        finally
        {
            conn.Close();
        }
    }
}
