using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class PaymentsReceived : AdminPage
{
	//protected SqlConnection Connection = new SqlConnection (ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
	protected SqlDataAdapter MainAdapter;
	protected DataSet MainDataSet = new DataSet ();

	protected void Page_Load(object sender, System.EventArgs e)
	{
		userid = AuthenticationManager.UserID;

        MainAdapter = CommonFunctions.PrepareAdapter(CommonFunctions.GetConnection(), "SELECT 0 AS IfAuction, Invoices.ID AS InvoiceID," +
			" Invoices.InvoiceDate, Invoices.InvoiceAmount, Invoices.PaymentDate, Invoices.PaymentAmount," +
			" Invoices.PaymentType, Invoices.InvoiceAmount - ISNULL(Invoices.PaymentAmount, 0) AS Balance, Properties.ID " +
			"FROM Invoices INNER JOIN Properties ON Invoices.PropertyID = Properties.ID " +
			"WHERE (Invoices.PaymentAmount > 0) " +
			"UNION " +
			"SELECT 1 AS IfAuction, Transactions.ID AS InvoiceID," +
			" Transactions.InvoiceDate, Transactions.InvoiceAmount, Transactions.PaymentDate, Transactions.PaymentAmount," +
			" 'Credit card' AS PaymentType, Transactions.InvoiceAmount - ISNULL(Transactions.PaymentAmount, 0) AS Balance, Auctions.ID " +
			"FROM Transactions INNER JOIN Auctions ON Transactions.AuctionID = Auctions.ID " +
			"WHERE (Transactions.PaymentAmount > 0)");

		if (Request.Params["SortOrder"] != null)
		{
			string sortfield = "";

			switch (Request.Params["SortOrder"])
			{
				case "1":
					sortfield = "InvoiceID";
					break;
				case "2":
					sortfield = "InvoiceDate";
					break;
				case "3":
					sortfield = "InvoiceAmount";
					break;
				case "4":
					sortfield = "ID";
					break;
				case "5":
					sortfield = "PaymentDate";
					break;
				case "6":
					sortfield = "PaymentAmount";
					break;
				case "7":
					sortfield = "PaymentType";
					break;
				case "8":
					sortfield = "Balance";
					break;
			}

			if (sortfield.Length > 0)
				MainAdapter.SelectCommand.CommandText += " ORDER BY " + sortfield;
		}

		//lock (CommonFunctions.Connection)
			MainAdapter.Fill (MainDataSet, "Invoices");

		DataBind ();
	}

	public string GetQueryStringWithoutSortOrder ()
	{
		string querystring = Request.QueryString.ToString ();

		querystring = querystring.Replace ("&SortOrder=1", "");
		querystring = querystring.Replace ("&SortOrder=2", "");
		querystring = querystring.Replace ("&SortOrder=3", "");
		querystring = querystring.Replace ("&SortOrder=4", "");
		querystring = querystring.Replace ("&SortOrder=5", "");
		querystring = querystring.Replace ("&SortOrder=6", "");
		querystring = querystring.Replace ("&SortOrder=7", "");
		querystring = querystring.Replace ("&SortOrder=8", "");

		querystring = querystring.Replace ("SortOrder=1&", "");
		querystring = querystring.Replace ("SortOrder=2&", "");
		querystring = querystring.Replace ("SortOrder=3&", "");
		querystring = querystring.Replace ("SortOrder=4&", "");
		querystring = querystring.Replace ("SortOrder=5&", "");
		querystring = querystring.Replace ("SortOrder=6&", "");
		querystring = querystring.Replace ("SortOrder=7&", "");
		querystring = querystring.Replace ("SortOrder=8&", "");

		return querystring;
	}

	public string SumInvoiceAmounts ()
	{
		decimal sum = 0;

		foreach (DataRow datarow in MainDataSet.Tables["Invoices"].Rows)
			if (datarow.RowState != DataRowState.Deleted)
				if (datarow["InvoiceAmount"] is decimal)
					sum += (decimal)datarow["InvoiceAmount"];

		return sum.ToString ("c");
	}

	public string SumPaymentAmounts ()
	{
		decimal sum = 0;

		foreach (DataRow datarow in MainDataSet.Tables["Invoices"].Rows)
			if (datarow.RowState != DataRowState.Deleted)
				if (datarow["PaymentAmount"] is decimal)
					sum += (decimal)datarow["PaymentAmount"];

		return sum.ToString ("c");
	}
}
