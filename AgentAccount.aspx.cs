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

public partial class AgentAccount : ClosedPage
{
	protected bool ifagent;
	protected bool ifclients;
	protected bool ifcommissions;
	protected bool ifagents;

	//protected SqlConnection Connection = new SqlConnection (ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
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
                WelcomeLabel.Text = "Welcome " + (string)fullname;

            SqlCommand GetIfAgent = new SqlCommand("SELECT IfAgent FROM Users WHERE ID = @UserID", connection);
            GetIfAgent.Parameters.Add("@UserID", SqlDbType.Int);
            GetIfAgent.Parameters["@UserID"].Value = userid;

            object ifagentresult = GetIfAgent.ExecuteScalar();
            ifagent = (ifagentresult is bool) && (bool)ifagentresult;

            SqlCommand GetNumClients = new SqlCommand("SELECT COUNT(*) FROM Users WHERE ReferredByID = @UserID", connection);
            GetNumClients.Parameters.Add("@UserID", SqlDbType.Int);
            GetNumClients.Parameters["@UserID"].Value = userid;

            object numclients = GetNumClients.ExecuteScalar();
            ifclients = (numclients is int) && ((int)numclients > 0);

            SqlCommand GetNumCommissions1 = new SqlCommand("SELECT COUNT(*) " +
                "FROM Commissions INNER JOIN Invoices ON Commissions.InvoiceID = Invoices.ID" +
                " INNER JOIN Properties ON Invoices.PropertyID = Properties.ID" +
                " INNER JOIN Users ON Properties.UserID = Users.ID " +
                "WHERE (AgentID = @UserID) AND (CommissionID IS NULL)", connection);
            GetNumCommissions1.Parameters.Add("@UserID", SqlDbType.Int);
            GetNumCommissions1.Parameters["@UserID"].Value = userid;

            SqlCommand GetNumCommissions2 = new SqlCommand("SELECT COUNT(*) " +
                "FROM Commissions INNER JOIN Transactions ON Commissions.TransactionID = Transactions.ID" +
                " INNER JOIN Auctions ON Transactions.AuctionID = Auctions.ID" +
                " INNER JOIN Properties ON Auctions.PropertyID = Properties.ID" +
                " INNER JOIN Users ON Properties.UserID = Users.ID " +
                "WHERE (AgentID = @UserID) AND (CommissionID IS NULL)", connection);
            GetNumCommissions2.Parameters.Add("@UserID", SqlDbType.Int);
            GetNumCommissions2.Parameters["@UserID"].Value = userid;

            object numcommissions1 = GetNumCommissions1.ExecuteScalar();
            object numcommissions2 = GetNumCommissions2.ExecuteScalar();
            ifcommissions = (numcommissions1 is int) && (numcommissions2 is int) &&
                ((int)numcommissions1 + (int)numcommissions2 > 0);

            SqlCommand GetNumAgents1 = new SqlCommand("SELECT COUNT(*) " +
                "FROM Commissions AS C1 INNER JOIN Commissions AS C2 ON C1.CommissionID = C2.ID" +
                " INNER JOIN Invoices ON C2.InvoiceID = Invoices.ID" +
                " INNER JOIN Properties ON Invoices.PropertyID = Properties.ID" +
                " INNER JOIN Users ON Properties.UserID = Users.ID " +
                "WHERE (C1.AgentID = @UserID)", connection);
            GetNumAgents1.Parameters.Add("@UserID", SqlDbType.Int);
            GetNumAgents1.Parameters["@UserID"].Value = userid;

            SqlCommand GetNumAgents2 = new SqlCommand("SELECT COUNT(*) " +
                "FROM Commissions AS C1 INNER JOIN Commissions AS C2 ON C1.CommissionID = C2.ID" +
                " INNER JOIN Transactions ON C2.TransactionID = Transactions.ID" +
                " INNER JOIN Auctions ON Transactions.AuctionID = Auctions.ID" +
                " INNER JOIN Properties ON Auctions.PropertyID = Properties.ID" +
                " INNER JOIN Users ON Properties.UserID = Users.ID " +
                "WHERE (C1.AgentID = @UserID)", connection);
            GetNumAgents2.Parameters.Add("@UserID", SqlDbType.Int);
            GetNumAgents2.Parameters["@UserID"].Value = userid;

            object numagents1 = GetNumAgents1.ExecuteScalar();
            object numagents2 = GetNumAgents2.ExecuteScalar();
            //ifagents = (numagents1 is int) && (numagents2 is int) && ((int)numagents1 + (int)numagents2 > 0);

            //if(ifagent) {
            //    AgentCheckBox.Text = "No, I do not wish to be an agent of " + CommonFunctions.GetSiteName();
            //    SubmitButton.Text = "Make changes";
            //}
            //else {
            //    AgentCheckBox.Text = "Yes, I wish to become an agent of " + CommonFunctions.GetSiteName();
            //    SubmitButton.Text = "Add Additional Info";
            //}
            connection.Close();
        }
		//CommonFunctions.Connection.Close ();
	}

    //protected void SubmitButton_Click (object sender, EventArgs e)
    //{
    //    if (!AgentCheckBox.Checked)
    //        return;

    //    if (ifagent)
    //    {
    //        using(SqlConnection connection = CommonFunctions.GetConnection()) {
    //            connection.Open();
    //            SqlCommand ClearReferredBy = new SqlCommand("UPDATE Users SET ReferredByID = NULL WHERE" +
    //                " ReferredByID = @UserID", connection);
    //            ClearReferredBy.Parameters.Add("@UserID", SqlDbType.Int);
    //            ClearReferredBy.Parameters["@UserID"].Value = userid;

    //            ClearReferredBy.ExecuteNonQuery();

    //            SqlCommand ClearAgent = new SqlCommand("UPDATE Users SET IfAgent = 0 WHERE ID = @UserID", connection);
    //            ClearAgent.Parameters.Add("@UserID", SqlDbType.Int);
    //            ClearAgent.Parameters["@UserID"].Value = userid;

    //            ClearAgent.ExecuteNonQuery();
    //            connection.Close();
    //        }

    //        Response.Redirect (backlinkurl);
    //    }
    //    else
    //    {
    //        using(SqlConnection connection = CommonFunctions.GetConnection()) {
    //            connection.Open();
    //            SqlCommand SetAgent = new SqlCommand("UPDATE Users SET IfAgent = 1 WHERE ID = @UserID", connection);
    //            SetAgent.Parameters.Add("@UserID", SqlDbType.Int);
    //            SetAgent.Parameters["@UserID"].Value = userid;

    //            SetAgent.ExecuteNonQuery();
    //            connection.Close();
    //        }

    //        Response.Redirect (CommonFunctions.PrepareURL ("OwnerInformation.aspx?UserID=" + userid.ToString (),
    //            "Agent Account"));
    //    }
    //}
}
