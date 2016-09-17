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

public partial class AgentCommissionPayable : AdminPage
{
	//protected SqlConnection Connection = new SqlConnection (ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
	protected SqlDataAdapter AgentAdapter;
	protected SqlDataAdapter MainAdapter;
	protected SqlDataAdapter CommissionsAdapter;
	protected DataSet MainDataSet = new DataSet ();

	protected void Page_Load (object sender, EventArgs e)
	{
		//CommonFunctions.Connection.Open ();
        using(SqlConnection connection = CommonFunctions.GetConnection()) {
            connection.Open();
            AgentAdapter = CommonFunctions.PrepareAdapter(connection, "SELECT * FROM Users " +
                "WHERE (ID = @UserID) AND (IfAgent = 1)", SqlDbType.Int);

            MainAdapter = CommonFunctions.PrepareAdapter(connection, "SELECT Commissions.*, InvoiceID AS TransactionNumber," +
                " (SELECT TOP 1 InvoiceDate FROM Invoices WHERE ID = Commissions.InvoiceID) AS InvoiceDate," +
                " 'W' AS TransactionType, PaymentAmount - ISNULL (PaidAmount, 0) AS BalanceDue " +
                "FROM Commissions " +
                "WHERE (AgentID = @UserID) AND EXISTS (SELECT * FROM Invoices WHERE ID = Commissions.InvoiceID) " +
                "UNION SELECT Commissions.*, TransactionID AS TransactionNumber," +
                " (SELECT TOP 1 InvoiceDate FROM Transactions WHERE ID = Commissions.TransactionID) AS InvoiceDate," +
                " CASE WHEN EXISTS (SELECT TOP 1 InvoiceDate FROM Transactions WHERE (ID = Commissions.TransactionID) AND" +
                " (IfListingFee = 1)) THEN 'L' ELSE 'A' END AS TransactionType," +
                " PaymentAmount - ISNULL (PaidAmount, 0) AS BalanceDue " +
                "FROM Commissions " +
                "WHERE (AgentID = @UserID) AND EXISTS (SELECT * FROM Transactions WHERE ID = Commissions.TransactionID)",
                SqlDbType.Int);

            MainAdapter.UpdateCommand = new SqlCommand("UPDATE Commissions SET PaidAmount = @PaidAmount, DatePaid = @DatePaid " +
                "WHERE ID = @CommissionID", connection);
            MainAdapter.UpdateCommand.Parameters.Add("@PaidAmount", SqlDbType.Money, 8, "PaidAmount");
            MainAdapter.UpdateCommand.Parameters.Add("@DatePaid", SqlDbType.SmallDateTime, 4, "DatePaid");
            MainAdapter.UpdateCommand.Parameters.Add("@CommissionID", SqlDbType.Int, 4, "ID");

            AgentAdapter.SelectCommand.Parameters["@UserID"].Value = userid;
            MainAdapter.SelectCommand.Parameters["@UserID"].Value = userid;

            //lock(CommonFunctions.Connection)
                if(AgentAdapter.Fill(MainDataSet, "Users") == 0)
                    Response.Redirect(CommonFunctions.PrepareURL("InternalError.aspx"));
            //lock(CommonFunctions.Connection)
                MainAdapter.Fill(MainDataSet, "Commissions");

            DataBind();

            connection.Close();
        }
		//CommonFunctions.Connection.Close ();
	}

	protected void SubmitButton_Click (object sender, EventArgs e)
	{
		foreach (DataRow datarow in MainDataSet.Tables["Commissions"].Rows)
			if ((Request.Params["Paid" + datarow["ID"].ToString ()] != null) &&
				(Request.Params["Paid" + datarow["ID"].ToString ()] == "on") &&
				((datarow["DatePaid"] is DBNull) || (datarow["DatePaid"] == null)))
			{
				datarow["PaidAmount"] = datarow["PaymentAmount"];
				datarow["BalanceDue"] = 0;
				datarow["DatePaid"] = DateTime.Now;
			}

		//lock (CommonFunctions.Connection)
			MainAdapter.Update (MainDataSet, "Commissions");

		DataBind ();
	}
}
