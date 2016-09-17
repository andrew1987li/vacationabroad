using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Net.Mail;
using System.Collections.Generic;

public partial class PropSched : CommonPage
{


    protected void Page_Load(object sender, System.EventArgs e)
    {
       // if (propertyid == -1)
            //Response.Redirect(CommonFunctions.PrepareURL("InternalError.aspx"), true);
if(Request.QueryString["PropertyID"] != null)
PopulateCal();
}


#region Calendar Section
private void PopulateCal()
{
DBConnection obj = new DBConnection();
            List<DateTime> DatesTaken = new List<DateTime>();

            DataTable dt = new DataTable();
            dt = VADBCommander.PropertyAvailDatesByProperty(Request.QueryString["PropertyID"].ToString());

            foreach (DataRow row in dt.Rows)
            {
                DatesTaken.Add(Convert.ToDateTime(row["PropertyDates"].ToString()));
            }
Session["DatesTaken"] = DatesTaken;

Calendar2.SelectedDate = DateTime.Now.AddMonths(1);
                Calendar2.VisibleDate = Calendar2.SelectedDate;

Calendar3.SelectedDate = DateTime.Now.AddMonths(2);
                Calendar3.VisibleDate = Calendar3.SelectedDate;
Calendar4.SelectedDate = DateTime.Now.AddMonths(3);
                Calendar4.VisibleDate = Calendar4.SelectedDate;

Calendar5.SelectedDate = DateTime.Now.AddMonths(4);
                Calendar5.VisibleDate = Calendar5.SelectedDate;
Calendar6.SelectedDate = DateTime.Now.AddMonths(5);
                Calendar6.VisibleDate = Calendar6.SelectedDate;
Calendar7.SelectedDate = DateTime.Now.AddMonths(6);
                Calendar7.VisibleDate = Calendar7.SelectedDate;
Calendar8.SelectedDate = DateTime.Now.AddMonths(7);
                Calendar8.VisibleDate = Calendar8.SelectedDate;

Calendar9.SelectedDate = DateTime.Now.AddMonths(8);
                Calendar9.VisibleDate = Calendar9.SelectedDate;
Calendar10.SelectedDate = DateTime.Now.AddMonths(9);
                Calendar10.VisibleDate = Calendar10.SelectedDate;
Calendar11.SelectedDate = DateTime.Now.AddMonths(10);
                Calendar11.VisibleDate = Calendar11.SelectedDate;
Calendar12.SelectedDate = DateTime.Now.AddMonths(11);
                Calendar12.VisibleDate = Calendar12.SelectedDate;
}
protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
    {                
        List<DateTime> DatesTaken = (List<DateTime>)Session["DatesTaken"];
        if (DatesTaken.Contains(e.Day.Date))
        {
            e.Cell.BackColor = System.Drawing.Color.Red;
            e.Cell.ForeColor = System.Drawing.Color.White;            
        }
       
        if (e.Day.IsOtherMonth == true)
        {

            e.Cell.Text = "";
            e.Cell.BackColor = System.Drawing.Color.White;
        }  
e.Cell.Text = e.Day.DayNumberText;

    }
#endregion
}