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

public partial class MyAccount : ClosedPage
{
	protected bool ifagent;
	protected bool ifinvoices;
	protected bool ifauctionswon;
	protected bool ifwatchitems;
	protected bool ifreviews;
	protected string useridstring;

	//protected SqlConnection Connection = new SqlConnection (ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
	protected SqlDataAdapter MainAdapter;
	protected DataSet MainDataSet = new DataSet ();

	protected void Page_Load(object sender, System.EventArgs e)
	{
		//CommonFunctions.Connection.Open ();

		BackLink.Visible = AuthenticationManager.IfAdmin;

        using(SqlConnection connection = CommonFunctions.GetConnection()) {
            connection.Open();
            SqlCommand GetUserName = new SqlCommand
                ("SELECT ISNULL(FirstName, '') + ' ' + ISNULL(LastName, '') + ' ' + ISNULL(CompanyName, '') AS FullName " +
                "FROM Users WHERE ID = @UserID", connection);
            GetUserName.Parameters.Add("@UserID", SqlDbType.Int);
            GetUserName.Parameters["@UserID"].Value = userid;

            object fullname = GetUserName.ExecuteScalar();
            if(fullname is string)
                WelcomeLabel.Text = "Welcome " + (string)fullname;

            UserIDLabel.Text = CommonFunctions.GetUsername(connection, userid);

            SqlCommand GetIfAgent = new SqlCommand("SELECT IfAgent FROM Users WHERE ID = @UserID", connection);
            GetIfAgent.Parameters.Add("@UserID", SqlDbType.Int);
            GetIfAgent.Parameters["@UserID"].Value = userid;

            object ifagentresult = GetIfAgent.ExecuteScalar();
            ifagent = (ifagentresult is bool) && (bool)ifagentresult;

            if(ifagent)
                Agent.Text = "Agent Account";
            else
                Agent.Text = "Become An Agent";

            SqlCommand GetIfExistInvoices = new SqlCommand("SELECT COUNT (*) FROM Invoices INNER JOIN Properties ON" +
                " Invoices.PropertyID = Properties.ID WHERE Properties.UserID = @UserID", connection);
            GetIfExistInvoices.Parameters.Add("@UserID", SqlDbType.Int);
            GetIfExistInvoices.Parameters["@UserID"].Value = userid;

            object ifinvoicesresult = GetIfExistInvoices.ExecuteScalar();
            ifinvoices = (ifinvoicesresult is int) && ((int)ifinvoicesresult > 0);

            SqlCommand GetNumAuctionsWon = new SqlCommand("SELECT COUNT(*) FROM Auctions " +
                "WHERE (HighestBidderID = @UserID) AND (AuctionEnd <= GETDATE())", connection);
            GetNumAuctionsWon.Parameters.Add("@UserID", SqlDbType.Int);
            GetNumAuctionsWon.Parameters["@UserID"].Value = userid;

            object numauctionswonresult = GetNumAuctionsWon.ExecuteScalar();
            ifauctionswon = (numauctionswonresult is int) && ((int)numauctionswonresult > 0);

            SqlCommand GetNumWatchItems = new SqlCommand("SELECT COUNT(*) FROM WatchListItems WHERE UserID = @UserID",
                connection);
            GetNumWatchItems.Parameters.Add("@UserID", SqlDbType.Int);
            GetNumWatchItems.Parameters["@UserID"].Value = userid;

            object numwatchitemsresult = GetNumWatchItems.ExecuteScalar();
            ifwatchitems = (numwatchitemsresult is int) && ((int)numwatchitemsresult > 0);

            SqlCommand GetNumReviews = new SqlCommand("SELECT COUNT(*) FROM Auctions INNER JOIN Properties" +
                " ON Auctions.PropertyID = Properties.ID WHERE (UserID = @UserID) AND (ReviewDate IS NOT NULL)", connection);
            GetNumReviews.Parameters.Add("@UserID", SqlDbType.Int);
            GetNumReviews.Parameters["@UserID"].Value = userid;

            object numreviewsresult = GetNumReviews.ExecuteScalar();
            ifreviews = (numreviewsresult is int) && ((int)numreviewsresult > 0);

            MainAdapter = CommonFunctions.PrepareAdapter(connection, "SELECT Auctions.*," +
                " (SELECT TOP 1 FileName FROM PropertyPhotos WHERE PropertyPhotos.PropertyID = Properties.ID ORDER BY OrderNumber) AS FileName," +
                " (SELECT TOP 1 Width FROM PropertyPhotos WHERE PropertyPhotos.PropertyID = Properties.ID ORDER BY OrderNumber) AS Width," +
                " (SELECT TOP 1 Height FROM PropertyPhotos WHERE PropertyPhotos.PropertyID = Properties.ID ORDER BY OrderNumber) AS Height " +
                "FROM Auctions INNER JOIN Properties ON Auctions.PropertyID = Properties.ID " +
                "WHERE ((UserID = @UserID) OR (HighestBidderID = @UserID)) AND (AuctionEnd > GETDATE ())" +
                " AND EXISTS (SELECT * FROM Transactions WHERE (AuctionID = Auctions.ID) AND (IfListingFee = 1)" +
                " AND (PaymentAmount >= InvoiceAmount))", SqlDbType.Int);

            MainAdapter.SelectCommand.Parameters["@UserID"].Value = userid;
            //lock(CommonFunctions.Connection)
                MainAdapter.Fill(MainDataSet, "Auctions");

            if((Session["Message"] is string) && (auctionid != -1))
                if(string.Compare((string)Session["Message"], "BidSuccess", true) == 0) {
                    MessageLabel.Visible = true;
                    MessageLink.Visible = true;
                    MessageLabel.Text = "You have successfully Bid on an Auction Item";
                    MessageLink.NavigateUrl = CommonFunctions.PrepareURL(auctionid.ToString() + "/default.aspx",
                        "*User* Account");
                    Session["Message"] = null;
                }
                else if(string.Compare((string)Session["Message"], "AddedToWatchList", true) == 0) {
                    MessageLabel.Visible = true;
                    MessageLink.Visible = true;
                    MessageLabel.Text = "The Item is has been added to your Watch List";
                    MessageLink.NavigateUrl = CommonFunctions.PrepareURL(auctionid.ToString() + "/default.aspx",
                        "*User* Account");
                    Session["Message"] = null;
                }
                else if(string.Compare((string)Session["Message"], "AlreadyOnWatchList", true) == 0) {
                    MessageLabel.Visible = true;
                    MessageLink.Visible = true;
                    MessageLabel.Text = "The Item is already on your Watch List";
                    MessageLink.NavigateUrl = CommonFunctions.PrepareURL(auctionid.ToString() + "/default.aspx",
                        "*User* Account");
                    Session["Message"] = null;
                }

            if(!IsPostBack)
                DataBind();

            connection.Close();
        }
	}

	protected void NewProperty_Click(object sender, System.EventArgs e)
	{
		Response.Redirect (CommonFunctions.PrepareURL ("EditProperty.aspx?UserID=" + userid.ToString (), "*User* Account"));
	}

	protected void NewAuction_Click (object sender, EventArgs e)
	{
		Response.Redirect (CommonFunctions.PrepareURL ("EditAuction.aspx?UserID=" + userid.ToString (), "*User* Account"));
	}

	protected void EditListings_Click (object sender, EventArgs e)
	{
		Response.Redirect (CommonFunctions.PrepareURL ("Listings.aspx?UserID=" + userid.ToString (), "*User* Account"));
	}

	protected void Agent_Click (object sender, EventArgs e)
	{
		Response.Redirect (CommonFunctions.PrepareURL ("AgentAccount.aspx?UserID=" + userid.ToString (), "*User* Account"));
	}
}
