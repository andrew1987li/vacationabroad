using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;

public partial class DrawingResults : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //first print year as heading, and month, name, and Prop#(w/link) as tables
        //**Select only winners
        DBConnection obj = new DBConnection();
        try
        {
            DataTable dtYears = new DataTable();
            dtYears = obj.spSelectCampaignWinners();
            List<int> lstYears = new List<int>();

            //make a list of years
            foreach (DataRow row1 in dtYears.Rows)
            {
                DateTime tempYear = Convert.ToDateTime(row1["transDate"]);
                int Year = tempYear.Year;
                if (!lstYears.Contains(Year))
                {
                    lstYears.Add(Year);
                }
            }
            DataTable dtDisplay = new DataTable();
            dtDisplay.Columns.Add("Month");
            dtDisplay.Columns.Add("Name");
            dtDisplay.Columns.Add("Property#");

            for (int i = 0; i < lstYears.Count; i++)
            {
                divContent.InnerHtml += "Year " + lstYears[i].ToString() + "<br />";
                dtDisplay.Rows.Clear();
                foreach (DataRow row in dtYears.Rows)
                {
                    if(Convert.ToDateTime(row["transDate"]).Year == lstYears[i])
                    {
                        DataRow rowNew = dtDisplay.NewRow();
                        rowNew["Month"] = Convert.ToDateTime(row["transDate"]).ToString("MMMM");
                        rowNew["Name"] = row["name"].ToString();

                        DataTable dt = obj.spSelectLocationInfoByPropID(Convert.ToInt32(row["propertyNumber"]));
                        string vText = CommonFunctions.GetSiteAddress() + "/" + dt.Rows[0]["country"].ToString() + "/" +
                            dt.Rows[0]["stateprovince"].ToString() + "/" + dt.Rows[0]["city"].ToString() + "/" +
                            row["propertyNumber"].ToString() + "/default.aspx";
                        vText = vText.Replace(" ", "_").ToLower();

                        rowNew["Property#"] = "<a href=\"" + vText + "\">" + row["propertyNumber"].ToString() +
                            "</a>";

                        dtDisplay.Rows.Add(rowNew);
                    }
                }
                divContent.InnerHtml += "<table class=\"DrawingTable\">";
                divContent.InnerHtml += "<tr><td width=\"20%\">MONTH</td><td width=\"30%\">NAME</td><td width=\"30%\">PROPERTY#</td></tr>";
                foreach (DataRow row1 in dtDisplay.Rows)
                {
                    divContent.InnerHtml += "<tr><td style=\"text-align:left\">" + row1["month"].ToString() + "</td><td style=\"text-align:left\">" +
                        row1["Name"].ToString() + "</td><td>" + row1["Property#"].ToString() + "</td></tr>";
                }
                divContent.InnerHtml += "</table><br/>";
            }
        }
        catch (Exception ex) { lblInfo.Text = ex.Message; }
        finally { obj.CloseConnection(); }
        Page.Header.Controls.Add(new LiteralControl("<link href='css/StyleSheetBig4.css' rel='stylesheet' type='text/css' />"));

    }
}
