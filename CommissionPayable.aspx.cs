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

public partial class CommissionPayable : AdminPage
{
	//protected SqlConnection Connection = new SqlConnection (ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
	protected SqlDataAdapter MainAdapter;
	protected DataSet MainDataSet = new DataSet ();

	protected void Page_Load (object sender, EventArgs e)
	{
		//CommonFunctions.Connection.Open ();

		MainAdapter = CommonFunctions.PrepareAdapter (CommonFunctions.GetConnection(), "SELECT *," +
			" (SELECT SUM(PaidAmount) FROM Commissions WHERE Commissions.AgentID = Users.ID) AS AmountPaid," +
			" (SELECT SUM(Commissions.PaymentAmount) - SUM(Commissions.PaidAmount) FROM Commissions INNER JOIN Transactions" +
			" ON Commissions.TransactionID = Transactions.ID WHERE (Commissions.AgentID = Users.ID)) AS AmountDueAuctions," +
			" (SELECT SUM(Commissions.PaymentAmount) - SUM(Commissions.PaidAmount) FROM Commissions INNER JOIN Invoices" +
			" ON Commissions.InvoiceID = Invoices.ID WHERE (Commissions.AgentID = Users.ID)) AS AmountDueAnnual," +
			" (SELECT MAX(DatePaid) FROM Commissions WHERE Commissions.AgentID = Users.ID) AS LastPaymentDate" +
			" FROM Users WHERE IfAgent = 1");

		//lock (CommonFunctions.Connection)
			MainAdapter.Fill (MainDataSet, "Users");

		DataBind ();
	}
}
