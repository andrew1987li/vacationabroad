using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Net.Mail;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class MakePayment : ClosedPage
{
	protected int invoiceid = -1;
	protected int uid;

	//protected SqlConnection Connection = new SqlConnection (ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
	protected SqlDataAdapter InvoicesAdapter;
	protected SqlDataAdapter TransactionsAdapter;
	protected DataSet MainDataSet = new DataSet ();

	protected void Page_Load(object sender, System.EventArgs e)
	{
		SqlCommand getuserid;

		if ((Request.Params["InvoiceID"] != null) && (Request.Params["InvoiceID"].Length > 0))
			try
			{
				invoiceid = Convert.ToInt32 (Request.Params["InvoiceID"]);
			}
			catch (Exception)
			{
			}

		if ((invoiceid != -1) || (propertyid != -1))
			auctionid = -1;

		if ((invoiceid == -1) && (propertyid == -1) && (auctionid == -1))
			Response.Redirect (CommonFunctions.PrepareURL ("InternalError.aspx"), true);

		//CommonFunctions.Connection.Open ();

		if (auctionid == -1)
		{
            using(SqlConnection connection = CommonFunctions.GetConnection()) {
                connection.Open();
                InvoicesAdapter = CommonFunctions.PrepareAdapter(connection, "SELECT * FROM Invoices " +
                    "WHERE ID = @InvoiceID", SqlDbType.Int);

                if(invoiceid == -1) {
                    SqlCommand getinvoiceid = new SqlCommand("SELECT TOP 1 ID FROM Invoices " +
                        "WHERE (PropertyID = @PropertyID) AND (RenewalDate >= GETDATE()) " +
                        "ORDER BY InvoiceDate DESC", connection);
                    getinvoiceid.Parameters.Add("@PropertyID", SqlDbType.Int);
                    getinvoiceid.Parameters["@PropertyID"].Value = propertyid;

                    object idresult = getinvoiceid.ExecuteScalar();

                    if(idresult is int)
                        invoiceid = (int)idresult;
                }
                else if(propertyid == -1) {
                    SqlCommand getpropertyid = new SqlCommand("SELECT PropertyID FROM Invoices " +
                        "WHERE ID = @InvoiceID", connection);
                    getpropertyid.Parameters.Add("@InvoiceID", SqlDbType.Int);
                    getpropertyid.Parameters["@InvoiceID"].Value = invoiceid;

                    object idresult = getpropertyid.ExecuteScalar();

                    if(idresult is int)
                        propertyid = (int)idresult;
                }

                InvoicesAdapter.SelectCommand.Parameters["@InvoiceID"].Value = invoiceid;

                //lock(CommonFunctions.Connection)
                    InvoicesAdapter.FillSchema(MainDataSet, SchemaType.Source, "Invoices");

                //lock(CommonFunctions.Connection)
                    if((invoiceid != -1) && (InvoicesAdapter.Fill(MainDataSet, "Invoices") == 0))
                        Response.Redirect(CommonFunctions.PrepareURL("InternalError.aspx"), true);

                getuserid = new SqlCommand("SELECT UserID FROM Properties WHERE ID = @PropertyID", connection);
                getuserid.Parameters.Add("@PropertyID", SqlDbType.Int);
                getuserid.Parameters["@PropertyID"].Value = propertyid;

                object useridresult = getuserid.ExecuteScalar();

                if(!(useridresult is int) || (((int)useridresult != AuthenticationManager.UserID) &&
                        !AuthenticationManager.IfAdmin))
                    Response.Redirect(CommonFunctions.PrepareURL("Login.aspx?BackLink=" + HttpUtility.UrlEncode(Request.Url.ToString())));
                
                connection.Close();
            }
		}
		else
		{
            using(SqlConnection connection = CommonFunctions.GetConnection()) {
                connection.Open();
                TransactionsAdapter = CommonFunctions.PrepareAdapter(connection, "SELECT * FROM Transactions");

                //lock(CommonFunctions.Connection)
                    TransactionsAdapter.FillSchema(MainDataSet, SchemaType.Source, "Transactions");

                getuserid = new SqlCommand("SELECT UserID FROM Properties INNER JOIN Auctions" +
                    " ON Properties.ID = Auctions.PropertyID WHERE Auctions.ID = @AuctionID", connection);
                getuserid.Parameters.Add("@AuctionID", SqlDbType.Int);
                getuserid.Parameters["@AuctionID"].Value = auctionid;

                object useridresult = getuserid.ExecuteScalar();

                if(!(useridresult is int) || (((int)useridresult != AuthenticationManager.UserID) &&
                        !AuthenticationManager.IfAdmin))
                    Response.Redirect(CommonFunctions.PrepareURL("Login.aspx?BackLink=" + HttpUtility.UrlEncode(Request.Url.ToString())));

                connection.Close();
            }
		}

		CreditCardType_SelectedIndexChanged (sender, e);
		Country_SelectedIndexChanged (sender, e);

		if (!IsPostBack)
			DataBind ();
	}

	protected void CreditCardType_SelectedIndexChanged (object sender, EventArgs e)
	{
		if (CreditCardType.SelectedValue == "Amex")
		{
			CVV2Valid.ValidationExpression = @"^\d{4}$";
			CVV2.MaxLength = 4;
			CCValid.ValidationExpression = @"^\d{4}[ -]{0,1}\d{2}[ -]{0,1}\d{4}[ -]{0,1}\d{5}$";
			CreditCardNumber.MaxLength = 18;
		}
		else
		{
			CVV2Valid.ValidationExpression = @"^\d{3}$";
			CVV2.MaxLength = 3;
			CCValid.ValidationExpression = @"^\d{4}[ -]{0,1}\d{4}[ -]{0,1}\d{4}[ -]{0,1}\d{4}$";
			CreditCardNumber.MaxLength = 19;
		}
	}

	protected void Country_SelectedIndexChanged (object sender, EventArgs e)
	{
		if (Country.SelectedValue == "US")
		{
			State.Visible = true;
			StateRequired.Visible = true;
			Province.Visible = false;
			ProvinceRequired.Enabled = false;
			ProvinceValid.Enabled = false;
			ZipValid.ValidationExpression = @"^[a-zA-Z0-9]{5}$";
			Zip.MaxLength = 5;
		}
		else
		{
			State.Visible = false;
			StateRequired.Visible = false;
			Province.Visible = true;
			ProvinceRequired.Enabled = true;
			ProvinceValid.Enabled = true;
			ZipValid.ValidationExpression = @"^[a-zA-Z0-9]{3,6}$";
			Zip.MaxLength = 6;
		}
	}

	protected void SubmitButton_Click(object sender, System.EventArgs e)
	{
		decimal amount;
		int type;

		if (!IsValid)
			return;

		if (auctionid == -1)
			amount = Convert.ToDecimal (ConfigurationManager.AppSettings["AnnualListingFee"]);
		else
			amount = Convert.ToDecimal (ConfigurationManager.AppSettings["AuctionListingFee"]);

		WrongPaymentInformation.Visible = false;

	/*	if (CreditCardType.SelectedValue.Length > 1)
			Response.Redirect (CommonFunctions.PrepareURL ("InternalError.aspx"), true);

		if ((string.Compare (CreditCardType.SelectedValue, "1") < 0) ||
				(string.Compare (CreditCardType.SelectedValue, "4") > 0))
			Response.Redirect (CommonFunctions.PrepareURL ("InternalError.aspx"), true);

		//type = Convert.ToInt32 (CreditCardType.SelectedValue); 
        - no longer needed, LMG 5/2/08
*/
		string ccnumber = CreditCardNumber.Text.Replace (" ", "").Replace ("-", "");

		string errors;
		bool result;

        result = PayPalFunctions.PerformPayment(CreditCardType.SelectedValue, ccnumber, CVV2.Text, ExpirationMonth.Value,
			ExpirationYear.Value, CreditCardFirstName.Text, CreditCardLastName.Text, Address1.Text,
			Address2.Text, City.Text, (State.Visible ? State.SelectedValue : Province.Text), Zip.Text,
			Country.SelectedItem.Text, Country.SelectedValue, amount, out errors);

		if (!result)
		{
			WrongPaymentInformation.Visible = true;
			WrongPaymentInformation.Text = "Error processing payment:" + errors;
			return;
		}

        using(SqlConnection connection = CommonFunctions.GetConnection()) {
            connection.Open();

	    InvoicesAdapter = CommonFunctions.PrepareAdapter(connection, "SELECT * FROM Invoices", SqlDbType.Int);

            SqlDataAdapter CreditCardsAdapter = CommonFunctions.PrepareAdapter(connection,
                "SELECT TOP 1 * FROM CreditCards WHERE UserID = @UserID", SqlDbType.Int);

            CreditCardsAdapter.SelectCommand.Parameters["@UserID"].Value = userid;

            //lock (CommonFunctions.Connection)
            CreditCardsAdapter.Fill(MainDataSet, "CreditCards");

            if(MainDataSet.Tables["CreditCards"].Rows.Count > 0) {
                MainDataSet.Tables["CreditCards"].Rows[0]["Type"] = CreditCardType.SelectedValue;
                MainDataSet.Tables["CreditCards"].Rows[0]["Number"] = CreditCardNumber.Text;
                MainDataSet.Tables["CreditCards"].Rows[0]["CVV2"] = CVV2.Text;
                MainDataSet.Tables["CreditCards"].Rows[0]["ExpMonth"] = Convert.ToInt32(ExpirationMonth.Value);
                MainDataSet.Tables["CreditCards"].Rows[0]["ExpYear"] = Convert.ToInt32(ExpirationYear.Value);
                MainDataSet.Tables["CreditCards"].Rows[0]["FirstName"] = CreditCardFirstName.Text;
                MainDataSet.Tables["CreditCards"].Rows[0]["LastName"] = CreditCardLastName.Text;
                MainDataSet.Tables["CreditCards"].Rows[0]["Address1"] = Address1.Text;
                MainDataSet.Tables["CreditCards"].Rows[0]["Address2"] = Address2.Text;
                MainDataSet.Tables["CreditCards"].Rows[0]["City"] = City.Text;
                if(State.Visible)
                    MainDataSet.Tables["CreditCards"].Rows[0]["State"] = State.SelectedValue;
                else
                    MainDataSet.Tables["CreditCards"].Rows[0]["State"] = Province.Text;
                MainDataSet.Tables["CreditCards"].Rows[0]["Zip"] = Zip.Text;
                MainDataSet.Tables["CreditCards"].Rows[0]["Country"] = Country.SelectedItem.Text;
                MainDataSet.Tables["CreditCards"].Rows[0]["CountryCode"] = Country.SelectedValue;
            }
            else {
                DataRow newcreditcard = MainDataSet.Tables["CreditCards"].NewRow();

                newcreditcard["UserID"] = userid;
                newcreditcard["Type"] = CreditCardType.SelectedValue;
                newcreditcard["Number"] = CreditCardNumber.Text;
                newcreditcard["CVV2"] = CVV2.Text;
                newcreditcard["ExpMonth"] = Convert.ToInt32(ExpirationMonth.Value);
                newcreditcard["ExpYear"] = Convert.ToInt32(ExpirationYear.Value);
                newcreditcard["FirstName"] = CreditCardFirstName.Text;
                newcreditcard["LastName"] = CreditCardLastName.Text;
                newcreditcard["Address1"] = Address1.Text;
                newcreditcard["Address2"] = Address2.Text;
                newcreditcard["City"] = City.Text;
                if(State.Visible)
                    newcreditcard["State"] = State.SelectedValue;
                else
                    newcreditcard["State"] = Province.Text;
                newcreditcard["Zip"] = Zip.Text;
                newcreditcard["Country"] = Country.SelectedItem.Text;
                newcreditcard["CountryCode"] = Country.SelectedValue;

                MainDataSet.Tables["CreditCards"].Rows.Add(newcreditcard);
            }

            //lock (CommonFunctions.Connection)
            CreditCardsAdapter.Update(MainDataSet, "CreditCards");

            int transactionid = -1;

            SqlCommand getidentity = new SqlCommand("SELECT @@IDENTITY AS ID", connection);

            if(auctionid == -1)
                if(invoiceid != -1) {
                    MainDataSet.Tables["Invoices"].Rows[0]["PaymentDate"] = DateTime.Now.Date;
                    MainDataSet.Tables["Invoices"].Rows[0]["PaymentAmount"] = amount;
                    MainDataSet.Tables["Invoices"].Rows[0]["PaymentType"] = "Credit card";

                    //lock (CommonFunctions.Connection)
                    InvoicesAdapter.Update(MainDataSet, "Invoices");
                }
                else {
                    DataRow datarow = MainDataSet.Tables["Invoices"].NewRow();

                    datarow["PropertyID"] = propertyid;
                    datarow["InvoiceDate"] = DateTime.Now.Date;
                    datarow["RenewalDate"] = DateTime.Now.Date.AddYears(1);
                    datarow["InvoiceAmount"] = amount;
                    datarow["PaymentDate"] = DateTime.Now.Date;
                    datarow["PaymentAmount"] = amount;
                    datarow["PaymentType"] = "Credit card";

                    MainDataSet.Tables["Invoices"].Rows.Add(datarow);

                    //lock (CommonFunctions.Connection)
                    InvoicesAdapter.Update(MainDataSet, "Invoices");

                    object res = getidentity.ExecuteScalar();
                    if(res is decimal)
                        invoiceid = (int)(decimal)res;
                }
            else {
                DataRow datarow = MainDataSet.Tables["Transactions"].NewRow();

                datarow["AuctionID"] = auctionid;
                datarow["IfListingFee"] = 1;
                datarow["InvoiceDate"] = DateTime.Now.Date;
                datarow["InvoiceAmount"] = amount;
                datarow["PaymentDate"] = DateTime.Now.Date;
                datarow["PaymentAmount"] = amount;

                MainDataSet.Tables["Transactions"].Rows.Add(datarow);

                //lock(CommonFunctions.Connection)
                    TransactionsAdapter.Update(MainDataSet, "Transactions");

                object res = getidentity.ExecuteScalar();
                if(res is decimal)
                    transactionid = (int)(decimal)res;
            }

            SqlCommand getagentid = new SqlCommand("SELECT ReferredByID FROM Users WHERE ID = @UserID", connection);
            getagentid.Parameters.Add("@UserID", SqlDbType.Int);
            getagentid.Parameters["@UserID"].Value = userid;

            object agentidresult = getagentid.ExecuteScalar();

            if((agentidresult is int) && ((invoiceid != -1) || (transactionid != -1))) {
                double percentage = Convert.ToDouble(ConfigurationManager.AppSettings["AgentCommission"].Replace("%", "")) /
                    100;

                SqlCommand getmaxid = new SqlCommand("SELECT MAX(ID) FROM Commissions", connection);

                object maxid = getmaxid.ExecuteScalar();
                int newid;

                if(maxid is int)
                    newid = (int)maxid + 1;
                else
                    newid = 1;

                SqlDataAdapter CommissionsAdapter = CommonFunctions.PrepareAdapter(connection, "SELECT * FROM Commissions");

                //lock(CommonFunctions.Connection)
                    CommissionsAdapter.FillSchema(MainDataSet, SchemaType.Source, "Commissions");

                DataRow newcommission = MainDataSet.Tables["Commissions"].NewRow();

                newcommission["ID"] = newid;
                newcommission["AgentID"] = (int)agentidresult;
                if(invoiceid != -1)
                    newcommission["InvoiceID"] = invoiceid;
                else
                    newcommission["TransactionID"] = transactionid;
                newcommission["PaymentAmount"] = amount * (decimal)percentage;
                newcommission["PaidAmount"] = 0;
                newcommission["DateIssued"] = DateTime.Now;

                MainDataSet.Tables["Commissions"].Rows.Add(newcommission);

                //lock(CommonFunctions.Connection)
                    CommissionsAdapter.Update(MainDataSet, "Commissions");

                getagentid.Parameters["@UserID"].Value = (int)agentidresult;

                agentidresult = getagentid.ExecuteScalar();

                if(agentidresult is int) {
                    percentage = Convert.ToDouble(ConfigurationManager.AppSettings["SubAgentCommission"].Replace("%", "")) /
                        100;

                    maxid = getmaxid.ExecuteScalar();
                    int newid2;

                    if(maxid is int)
                        newid2 = (int)maxid + 1;
                    else
                        newid2 = 1;

                    newcommission = MainDataSet.Tables["Commissions"].NewRow();

                    newcommission["ID"] = newid2;
                    newcommission["AgentID"] = (int)agentidresult;
                    if(invoiceid != -1)
                        newcommission["InvoiceID"] = invoiceid;
                    else
                        newcommission["TransactionID"] = transactionid;
                    newcommission["PaymentAmount"] = amount * (decimal)percentage;
                    newcommission["DateIssued"] = DateTime.Now;
                    newcommission["PaidAmount"] = 0;
                    newcommission["CommissionID"] = newid;

                    MainDataSet.Tables["Commissions"].Rows.Add(newcommission);

                    //lock(CommonFunctions.Connection)
                        CommissionsAdapter.Update(MainDataSet, "Commissions");
                }
            }
            connection.Close();
        }
/*
		string emailbody;

		emailbody = "Hello!<br /><br />" +
			"The owner intends to pay invoice number " + invoiceid.ToString () + " issued to property number " +
			PropertyNumber.Text + " (invoice amount is $" + InvoiceAmount.Text + ").<br /><br />" +
			"Owner credit card info:<br />" +
			"Card type: " + CreditCardType.SelectedValue + "<br />" +
			"Card number: " + CreditCardNumber.Text + "<br />" +
			"Card expiration: " + ExpirationMonth.Text + "/" + ExpirationYear.Text + "<br />";

		SmtpClient smtpclient = new SmtpClient (ConfigurationManager.AppSettings["SMTPServer"],
			int.Parse (ConfigurationManager.AppSettings["SMTPPort"]));

		MailMessage message = new MailMessage ("admin@" + CommonFunctions.GetDomainName (),
			System.Configuration.ConfigurationManager.AppSettings["PaymentEmail"]);
		message.Subject = CommonFunctions.GetSiteAddress () +
			CommonFunctions.PrepareURL (((string)PropertiesFullSet.Tables["Properties"].Rows[0]["Country"]).Replace (" ", "_").ToLower () + "/" +
			((string)PropertiesFullSet.Tables["Properties"].Rows[0]["StateProvince"]).Replace (" ", "_").ToLower () + "/" +
			((string)PropertiesFullSet.Tables["Properties"].Rows[0]["City"]).Replace (" ", "_").ToLower () + "/" +
			((int)PropertiesFullSet.Tables["Properties"].Rows[0]["ID"]).ToString () + "/default.aspx");
		message.Body = emailbody;
		message.IsBodyHtml = true;

		message.Body = message.Body.Replace("\r", "").Replace("\n", Environment.NewLine);
		message.Headers["Content-Type"] = "text/plain; charset = \"iso-8859-1\"";

		smtpclient.Send (message);
*/
		Response.Redirect (CommonFunctions.PrepareURL ("ThankYouPayment.aspx"));
	}

	protected void CancelButton_Click (object sender, EventArgs e)
	{
		Response.Redirect (backlinkurl);
	}
}
