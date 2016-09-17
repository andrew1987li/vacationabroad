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

public partial class AgentAgents : ClosedPage
{
	//protected SqlConnection Connection = new SqlConnection (ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
	protected SqlDataAdapter MainAdapter;
	protected DataSet MainDataSet = new DataSet ();

	protected void Page_Load (object sender, EventArgs e)
	{
		//CommonFunctions.Connection.Open ();
        using(SqlConnection connection = CommonFunctions.GetConnection()) {
            connection.Open();
            SqlCommand GetUserName = new SqlCommand("SELECT ISNULL(FirstName, '') + ' ' + ISNULL(LastName, '') + ' ' +" +
                " ISNULL(CompanyName, '') AS FullName FROM Users WHERE ID = @UserID", connection);
            GetUserName.Parameters.Add("@UserID", SqlDbType.Int);
            GetUserName.Parameters["@UserID"].Value = userid;

            object fullname = GetUserName.ExecuteScalar();
            if(fullname is string)
                WelcomeLabel.Text = "Welcome " + (string)fullname + " - Commission From My Agents";

            MainAdapter = CommonFunctions.PrepareAdapter(connection, "SELECT C1.*, Invoices.InvoiceDate," +
                " Invoices.InvoiceAmount, ISNULL(U2.UserID, U2.Username) AS Username, U2.FirstName, U2.LastName " +
                "FROM Commissions AS C1 INNER JOIN Commissions AS C2 ON C1.CommissionID = C2.ID" +
                " INNER JOIN Invoices ON C2.InvoiceID = Invoices.ID" +
                " INNER JOIN Properties ON Invoices.PropertyID = Properties.ID" +
                " INNER JOIN Users AS U1 ON Properties.UserID = U1.ID " +
                " INNER JOIN Users AS U2 ON U1.ReferredByID = U2.ID " +
                "WHERE (C1.AgentID = @UserID) " +
                "UNION SELECT C1.*, Transactions.InvoiceDate, Transactions.InvoiceAmount," +
                " ISNULL(U2.UserID, U2.Username) AS Username, U2.FirstName, U2.LastName " +
                "FROM Commissions AS C1 INNER JOIN Commissions AS C2 ON C1.CommissionID = C2.ID" +
                " INNER JOIN Transactions ON C2.TransactionID = Transactions.ID" +
                " INNER JOIN Auctions ON Transactions.AuctionID = Auctions.ID" +
                " INNER JOIN Properties ON Auctions.PropertyID = Properties.ID" +
                " INNER JOIN Users AS U1 ON Properties.UserID = U1.ID " +
                " INNER JOIN Users AS U2 ON U1.ReferredByID = U2.ID " +
                "WHERE (C1.AgentID = @UserID)", SqlDbType.Int);

            MainAdapter.SelectCommand.Parameters["@UserID"].Value = userid;

            //lock(CommonFunctions.Connection)
                MainAdapter.Fill(MainDataSet, "Commissions");

            DataBind();
            connection.Close();
        }
		//CommonFunctions.Connection.Close ();
	}
}
