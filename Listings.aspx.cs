using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ASPSnippets.FaceBookAPI;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;

public partial class Listings : ClosedPage
{
	//protected SqlConnection Connection = new SqlConnection (ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
	protected SqlDataAdapter FreeTrialAdapter;
	protected SqlDataAdapter AnnualFeeAdapter;
	protected SqlDataAdapter AuctionAdapter;
	protected DataSet MainDataSet = new DataSet ();
    protected bool ifinvoices;

    protected void fbLogin(object sender, EventArgs e)
    {
        FaceBookConnect.Authorize("email", Request.Url.AbsoluteUri.Split('?')[0]);
        string a = Request.QueryString["code"];

    }

	protected void Page_Load (object sender, EventArgs e)
    {

       


		//CommonFunctions.Connection.Open ();

        using(SqlConnection connection = CommonFunctions.GetConnection()) {
            connection.Open();

            SqlCommand GetUserName = new SqlCommand
                ("SELECT ISNULL(FirstName, '') + ' ' + ISNULL(LastName, '') + ' ' + ISNULL(CompanyName, '') AS FullName " +
                "FROM Users WHERE ID = @UserID", connection);
            GetUserName.Parameters.Add("@UserID", SqlDbType.Int);
            GetUserName.Parameters["@UserID"].Value = userid;

            object fullname = GetUserName.ExecuteScalar();
            if(fullname is string)
                WelcomeLabel.Text += ' ' + (string)fullname;

            FreeTrialAdapter = CommonFunctions.PrepareAdapter(connection, "SELECT *," +
                " CASE WHEN EXISTS (SELECT * FROM Auctions WHERE (PropertyID = Properties.ID) AND ((AuctionEnd > GETDATE())" +
                " OR (HighestBidderID IS NULL))) THEN 1 ELSE 0 END AS IfListedAsAuction FROM Properties " +
                "WHERE (UserID = @UserID) AND ((IfFinished = 1) OR" +
                " NOT EXISTS (SELECT ID FROM Auctions WHERE PropertyID = Properties.ID)) AND" +
                " NOT EXISTS (SELECT * FROM Invoices WHERE (PropertyID = Properties.ID) AND" +
                " (PaymentAmount >= InvoiceAmount) AND (GETDATE() <= Invoices.RenewalDate))", SqlDbType.Int);

            AnnualFeeAdapter = CommonFunctions.PrepareAdapter(connection, "SELECT *," +
                " CASE WHEN EXISTS (SELECT * FROM Auctions WHERE (PropertyID = Properties.ID) AND ((AuctionEnd > GETDATE())" +
                " OR (HighestBidderID IS NULL))) THEN 1 ELSE 0 END AS IfListedAsAuction," +
                " CASE WHEN EXISTS (SELECT * FROM Invoices WHERE (PropertyID = Properties.ID) AND" +
                " (PaymentAmount < InvoiceAmount) AND (RenewalDate >= GETDATE()))" +
                " AND NOT EXISTS (SELECT * FROM Invoices WHERE (PropertyID = Properties.ID) AND" +
                " (PaymentAmount >= InvoiceAmount) AND (DATEDIFF(d, GETDATE(), RenewalDate) > 15)) THEN 1 ELSE 0 END AS NeedToPay " +
                "FROM Properties WHERE (UserID = @UserID)" +
                " AND ((IfFinished = 1) OR NOT EXISTS (SELECT ID FROM Auctions WHERE PropertyID = Properties.ID))" +
                " AND EXISTS (SELECT * FROM Invoices WHERE (PropertyID = Properties.ID) AND" +
                " (PaymentAmount >= InvoiceAmount) AND (GETDATE() <= Invoices.RenewalDate))", SqlDbType.Int);

            AuctionAdapter = CommonFunctions.PrepareAdapter(connection, "SELECT Auctions.*, Properties.ID AS PropertyID," +
                " Properties.IfFinished, CASE WHEN NOT EXISTS (SELECT * FROM Transactions WHERE (AuctionID = Auctions.ID) AND" +
                " (IfListingFee = 1) AND (PaymentAmount >= InvoiceAmount)) THEN 1 ELSE 0 END AS NeedToPay," +
                " Properties.Name AS Name " +
                "FROM Auctions INNER JOIN Properties ON Auctions.PropertyID = Properties.ID " +
                "WHERE (UserID = @UserID)", SqlDbType.Int);

            FreeTrialAdapter.SelectCommand.Parameters["@UserID"].Value = userid;
            AnnualFeeAdapter.SelectCommand.Parameters["@UserID"].Value = userid;
            AuctionAdapter.SelectCommand.Parameters["@UserID"].Value = userid;

            //lock (CommonFunctions.Connection)
            FreeTrialAdapter.Fill(MainDataSet, "FreeTrialProperties");
            //lock (CommonFunctions.Connection)
            AnnualFeeAdapter.Fill(MainDataSet, "AnnualFeeProperties");
            //lock (CommonFunctions.Connection)
            AuctionAdapter.Fill(MainDataSet, "Auctions");

            UserIDLabel.Text = CommonFunctions.GetUsername(connection, userid);

            SqlCommand GetIfExistInvoices = new SqlCommand("SELECT COUNT (*) FROM Invoices INNER JOIN Properties ON" +
                " Invoices.PropertyID = Properties.ID WHERE Properties.UserID = @UserID", connection);
            GetIfExistInvoices.Parameters.Add("@UserID", SqlDbType.Int);
            GetIfExistInvoices.Parameters["@UserID"].Value = userid;
            object ifinvoicesresult = GetIfExistInvoices.ExecuteScalar();
            ifinvoices = (ifinvoicesresult is int) && ((int)ifinvoicesresult > 0);
           
                string a = Convert.ToString(HttpContext.Current.Session["IsSocial"]);
                if (a == "True")
                {
                    facebook_btn.Visible = false;
                    Twitter_btn.Visible = false;
                }
            if(!IsPostBack)
                DataBind();

            connection.Close();
            FillTours();
            
        }

  
	}
    public void FillTours()
    {
        DBConnection obj = new DBConnection();
        DataTable dt = new DataTable();
        string query = "";
        try
        {
            if(Request.QueryString["action"] != null)
            {
                VADBCommander.TourDelete(Request.QueryString["tourID"].ToString());
            }

            dt = VADBCommander.ToursByUserID(userid.ToString());
            if (dt.Rows.Count > 0)
            {
                rptTours.DataSource = dt;
                rptTours.DataBind();
            }
            else
                rptTours.Visible = false;
        }
        catch (Exception ex) { lblError.Text = ex.Message; }
        finally { obj.CloseConnection(); }
    }
	public string PrintPublishButton (System.Data.DataRowView property)
	{
		if ((property.Row["IfFinished"] is bool) && (bool)property.Row["IfFinished"])
			return "<td bordercolor=\"#ffffff\"></td>";
		else
			return "<td bordercolor=\"#ffffff\">" +
					 "<input type=\"button\" value=\"Publish\" onclick=\"window.location.href='" +
					 CommonFunctions.PrepareURL ("PublishProperty.aspx?UserID=" + userid.ToString () + "&PropertyID=" +
					 ((int)property.Row["ID"]).ToString (), "*User* Listings") + "';\">" +
				   "</td>";
	}

	public string PrintPropertyPaymentButton (System.Data.DataRowView property)
	{
		if ((property.Row["NeedToPay"] is int) && ((int)property.Row["NeedToPay"] == 0))
			return "<td bordercolor=\"#ffffff\"></td>";
		else
			return "<td bordercolor=\"#ffffff\">" +
					 "<input type=\"button\" value=\"Payment\" style=\"width:65px\" onclick=\"window.location.href='" +
					 CommonFunctions.PrepareURL ("MakePayment.aspx?UserID=" + userid.ToString () + "&PropertyID=" + ((int)property.Row["ID"]).ToString (),
					 "*User* Listings") + "';\">" +
				   "</td>";
	}

	public string PrintCalendarButton (System.Data.DataRowView property)
	{
        //if ((property.Row["IfListedAsAuction"] is int) && ((int)property.Row["IfListedAsAuction"] == 1))
        //    return "<td bordercolor=\"#ffffff\"></td>";
        //else
        return "<td bordercolor=\"#ffffff\">" +
                 "<input type=\"button\" value=\"Calendar\" style=\"width:115px\" onclick=\"window.location.href='" +
                 "PropertyCalendar.aspx?PropertyId=" + ((int)property.Row["ID"]).ToString () + "&UserID=" + userid.ToString() + "';\">" +
                   "</td>";
	}

	public string PrintAuctionPaymentButton (System.Data.DataRowView auction)
	{
		if ((auction.Row["NeedToPay"] is int) && ((int)auction.Row["NeedToPay"] == 0))
			return "<td bordercolor=\"#ffffff\"></td>";
		else
			return "<td bordercolor=\"#ffffff\">" +
					 "<input type=\"button\" value=\"Payment\" style=\"width:65px\" onclick=\"window.location.href='" +
					 CommonFunctions.PrepareURL ("MakePayment.aspx?UserID=" + userid.ToString () + "&AuctionID=" + ((int)auction.Row["ID"]).ToString (),
					 "*User* Listings") + "';\">" +
				   "</td>";
	}

	protected void NewProperty_Click (object sender, EventArgs e)
	{
		Response.Redirect (CommonFunctions.PrepareURL ("EditProperty.aspx?UserID=" + userid.ToString (), "*User* Listings"));
	}

	protected void NewAuction_Click (object sender, EventArgs e)
	{
		Response.Redirect (CommonFunctions.PrepareURL ("EditAuction.aspx?UserID=" + userid.ToString (), "*User* Listings"));
	}
    protected void btnTour_Click(object sender, EventArgs e)
    {
        //Response.Redirect(CommonFunctions.PrepareURL("TourTerms.aspx?UserID=" + userid.ToString(), "*User* Listings"));
        Response.Redirect(CommonFunctions.PrepareURL("EditTour.aspx?UserID=" + userid.ToString(), "*User* Listings"));
    }
    protected void Action(object source, RepeaterCommandEventArgs e)
    {
        
        DataTable dt = new DataTable();
        DBConnection obj = new DBConnection();
        try
        {
            lblError.Text = "jkl";
            
        }
        catch (Exception ex) { lblError.Text = ex.Message; }
        finally { obj.CloseConnection(); }
    }
    protected void Agent_Click(object sender, EventArgs e)
    {
        Response.Redirect(CommonFunctions.PrepareURL("AgentAccount.aspx?UserID=" + userid.ToString(), "*User* Account"));
    }

 
 
}
