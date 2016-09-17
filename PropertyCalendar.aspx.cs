using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default3 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblHeaderMsg.Text = "Click on dates to reserve/unreserve for property " + Request.QueryString["PropertyID"].ToString();

        if (!IsPostBack)
        {
            Session.Remove("DatesTaken");
            Session.Remove("DatesRemove");
            Session.Remove("curDate");

            DBConnection obj = new DBConnection();
            List<DateTime> DatesTaken = new List<DateTime>();

            DataTable dt = new DataTable();
            dt = VADBCommander.PropertyAvailDatesByProperty(Request.QueryString["PropertyID"].ToString()); 

            foreach (DataRow row in dt.Rows)
            {
                DatesTaken.Add(Convert.ToDateTime(row["PropertyDates"].ToString()));
            }
            Session["DatesTaken"] = DatesTaken;
            if (DatesTaken.Count > 0)
                chkDisplay.Checked = true;

            dt = VADBCommander.CityStatePropertyInd(Request.QueryString["PropertyID"].ToString());

            lblCityPage.Text = "Your calendar will be displayed next to your listing on the " + dt.Rows[0]["City"].ToString() + " page.";            
            
            string stateID = dt.Rows[0]["stateID"].ToString();
            string cityName = dt.Rows[0]["City"].ToString();

            dt = VADBCommander.StateCountryList(stateID);

            //lnkCityPage.PostBackUrl = dt.Rows[0]["country"].ToString() + "/" + dt.Rows[0]["state"].ToString() + "/" + cityName + "/default.aspx";
            lnkCity.NavigateUrl = dt.Rows[0]["country"].ToString() + "/" + dt.Rows[0]["state"].ToString() + "/" + cityName + "/default.aspx";
            lnkCity.Text = "View calendar on " + cityName + " page.";
            lblHeaderMsg.Text = "Calendar is displayed on Actual " + cityName + " webpage.";
        }
        else
        {
            
        }
        if (Session["curDate"] != null)
                Calendar1.TodaysDate = Convert.ToDateTime(Session["curDate"]);

        if (Session["DatesDiscard"] == null)
        {
            List<DateTime> DatesDiscard = new List<DateTime>();
            Session["DatesDiscard"] = DatesDiscard;
        }
        
    }
    
        protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
    {        
        // Change the background color of the days in the month to yellow.         

        //if (!e.Day.IsOtherMonth && !e.Day.IsSelected)
        //    e.Cell.BackColor = System.Drawing.Color.AliceBlue;        
        
        // Display vacation dates in yellow boxes with purple borders.         
        //Style vacationStyle = new Style();        
        //vacationStyle.BackColor = System.Drawing.Color.Gray;        
        //vacationStyle.ForeColor = System.Drawing.Color.LightGray;        
        //vacationStyle.BorderWidth = 1;        
        //vacationStyle.Font.Strikeout = true;        
        
        // Display weekend dates in green boxes.         
        //Style weekendStyle = new Style();        
        //weekendStyle.BackColor = System.Drawing.Color.Gray;
        //if ((e.Day.Date >= new DateTime(2009, 10, 01)) && (e.Day.Date <= new DateTime(2009, 10, 5)))        
        //{            // Apply the vacation style to the vacation dates.             
        //    e.Cell.ApplyStyle(vacationStyle);            
        //    //e.Cell.Enabled = false;            
        //    e.Cell.Text = e.Day.DayNumberText;        
        //}        
        //else if (e.Day.IsWeekend)        
        //{            // Apply the weekend style to the weekend dates.             
        //    e.Cell.ApplyStyle(weekendStyle);            
        //    e.Cell.Enabled = false;            
        //    e.Cell.Text = e.Day.DayNumberText;        
        //}
        //DateTime myAppointment = new DateTime(2009, 10, 10);


        List<DateTime> DatesTaken = (List<DateTime>)Session["DatesTaken"];
        if (DatesTaken.Contains(e.Day.Date))
        {
            e.Cell.BackColor = System.Drawing.Color.Red;
            e.Cell.ForeColor = System.Drawing.Color.White;
            //e.Cell.Text = e.Day.DayNumberText;
        }

        //if (e.Day.Date == myAppointment)
        //{
        //    //e.Day.IsSelectable = false;            
        //    //e.Cell.Controls.Add(new LiteralControl("My Appointment")); 
        //    e.Cell.BackColor = System.Drawing.Color.Red;
        //    e.Cell.ForeColor = System.Drawing.Color.White;
        //    e.Cell.Text = e.Day.DayNumberText;
        //}
        //        else        
        //{            e.Day.IsSelectable = true;        } 

        if (e.Day.IsOtherMonth == true)
        {

            e.Cell.Text = "";
            e.Cell.BackColor = System.Drawing.Color.White;

        }  

    }
        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            List<DateTime> DatesTaken = (List<DateTime>)Session["DatesTaken"];
            Session["curDate"] = Calendar1.SelectedDate;

            if (chkDisplay.Checked == true)
            {                
                DBConnection obj = new DBConnection();

                //if list contains reservation..remove by removing from list and db
                if (DatesTaken.Contains(Calendar1.SelectedDate))
                {
                    DatesTaken.Remove(Calendar1.SelectedDate);

                    VADBCommander.PropertyAvailDatesDelete(Request.QueryString["PropertyID"].ToString(), Calendar1.SelectedDate.ToString("yyyyMMdd"));

                    Calendar1.SelectedDayStyle.BackColor = System.Drawing.Color.AliceBlue;
                    Calendar1.SelectedDayStyle.ForeColor = System.Drawing.Color.Black;
                }
                else //if not in reserve list..add to it and to db
                {
                    DatesTaken.Add(Calendar1.SelectedDate);
                    VADBCommander.PropertyAvailDateAdd(Calendar1.SelectedDate.ToString("yyyyMMdd"), Request.QueryString["PropertyID"].ToString());
                }

            }
            else
            { //outside checkbox
                if (DatesTaken.Contains(Calendar1.SelectedDate))
                { //to temp delete
                    DatesTaken.Remove(Calendar1.SelectedDate);
                    List<DateTime> DatesRemove = new List<DateTime>();
                    if (Session["DatesRemove"] != null)
                        DatesRemove = (List<DateTime>)Session["DatesRemove"];                    

                        DatesRemove.Add(Calendar1.SelectedDate);
                        Session["DatesRemove"] = DatesRemove;
                }
                else
                {  //to temp add
                    DatesTaken.Add(Calendar1.SelectedDate);
                    //lblTest.Text += "date added";
                }

            }
            Session["DatesTaken"] = DatesTaken;
            Calendar1.SelectedDates.Clear();
        }
        protected void chkDisplay_CheckedChanged(object sender, EventArgs e)
        {
            //upon check true, save list values to db if not exists
            if (chkDisplay.Checked == true)
            {
                DBConnection obj = new DBConnection();
                List<DateTime> DatesTaken = (List<DateTime>)Session["DatesTaken"];
                
                for (int i = 0; i < DatesTaken.Count; i++)
                {                   
                    DataTable dt = new DataTable();
                    dt = VADBCommander.PropertyAvailByDate(Request.QueryString["PropertyID"].ToString(), DatesTaken[i].ToString("MM/dd/yyyy"));
                    if (dt.Rows.Count < 1)
                    {
                        VADBCommander.PropertyAvailDateAdd(DatesTaken[i].ToString("MM/dd/yyyy"), Request.QueryString["PropertyID"].ToString());
                        //lblTest.Text += "insert value " + DatesTaken[i].ToString("MM/dd/yyyy") + "; ";
                        //lblTest.Text += "iteration#" + i + ";";
                    }
                }
                //lblTest.Text += "New Dates Saved";
                //****************this part deletes from db if user deletes from calendar before checking box
                if (Session["DatesRemove"] != null)
                {
                    List<DateTime> DatesRemove = (List<DateTime>)Session["DatesRemove"];

                    for (int x = 0; x < DatesRemove.Count; x++)
                    {
                        try
                        {
                            VADBCommander.PropertyAvailDatesDelete(Request.QueryString["PropertyID"].ToString(), DatesRemove[x].ToString("MM/dd/yyyy"));

                        }
                        catch (Exception ex) { lblTest.Text = ex.Message; }
                    }
                }
                
            }
        }
}

