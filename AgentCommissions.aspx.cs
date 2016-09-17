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

public partial class AgentCommissions : ClosedPage
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
                WelcomeLabel.Text = "Welcome " + (string)fullname + " - My Commissions";

            MainAdapter = CommonFunctions.PrepareAdapter(connection, "SELECT Commissions.*, Invoices.InvoiceDate," +
                " Invoices.InvoiceAmount, Users.CompanyName, Users.FirstName, Users.LastName " +
                "FROM Commissions INNER JOIN Invoices ON Commissions.InvoiceID = Invoices.ID" +
                " INNER JOIN Properties ON Invoices.PropertyID = Properties.ID" +
                " INNER JOIN Users ON Properties.UserID = Users.ID " +
                "WHERE (AgentID = @UserID) AND (CommissionID IS NULL)" +
                "UNION SELECT Commissions.*, Transactions.InvoiceDate, Transactions.InvoiceAmount, Users.CompanyName," +
                " Users.FirstName, Users.LastName " +
                "FROM Commissions INNER JOIN Transactions ON Commissions.TransactionID = Transactions.ID" +
                " INNER JOIN Auctions ON Transactions.AuctionID = Auctions.ID" +
                " INNER JOIN Properties ON Auctions.PropertyID = Properties.ID" +
                " INNER JOIN Users ON Properties.UserID = Users.ID " +
                "WHERE (AgentID = @UserID) AND (CommissionID IS NULL)", SqlDbType.Int);

            MainAdapter.SelectCommand.Parameters["@UserID"].Value = userid;

            //lock(CommonFunctions.Connection)
                MainAdapter.Fill(MainDataSet, "Commissions");

            DataBind();
            connection.Close();
        }
		//CommonFunctions.Connection.Close ();
	}
}
