using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Configuration;
using System.Net.Mail;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class ApplyPayment : AdminPage
{
	protected System.Data.SqlClient.SqlDataAdapter InvoicesAdapter;
	protected Vacations.InvoicesDataset InvoicesSet;
	protected System.Data.SqlClient.SqlCommand sqlSelectCommand1;
	protected System.Data.SqlClient.SqlDataAdapter PropertiesAdapter;
	protected Vacations.PropertiesFullDataset PropertiesFullSet;
	protected System.Data.SqlClient.SqlCommand sqlSelectCommand2;
	//protected System.Data.SqlClient.SqlConnection Connection;

	protected void Page_Load(object sender, System.EventArgs e)
	{
		System.Data.SqlClient.SqlCommandBuilder builder = new System.Data.SqlClient.SqlCommandBuilder (InvoicesAdapter);

		//string connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

		//CommonFunctions.Connection.ConnectionString = connectionstring;

		//if (CommonFunctions.Connection.State == ConnectionState.Closed)
			//CommonFunctions.Connection.Open ();

		if (!IsPostBack)
		{
			if (Request.Params["InvoiceID"] != null)
			{
				int invoiceid = Convert.ToInt32 (Request.Params["InvoiceID"]);

				LoadInvoice (invoiceid);
			}

			DataBind ();
		}

		//CommonFunctions.Connection.Close ();
	}

	private void LoadInvoice (int invoiceid)
	{
		InvoicesAdapter.SelectCommand.Parameters["@InvoiceID"].Value = invoiceid;

		InvoicesSet.Clear ();

		//lock (CommonFunctions.Connection)
			if (InvoicesAdapter.Fill (InvoicesSet) == 0)
			{
				NotFound.Visible = true;
				return;
			}

            using(SqlConnection connection = CommonFunctions.GetConnection()) {
                connection.Open();
                SqlCommand getname = new SqlCommand("SELECT FirstName + ' ' + LastName AS Name FROM Users" +
                    " INNER JOIN Properties ON Users.ID = Properties.UserID" +
                    " INNER JOIN Invoices ON Properties.ID = Invoices.PropertyID WHERE (Invoices.ID = @InvoiceID)", connection);
                getname.Parameters.Add("@InvoiceID", SqlDbType.Int, 4, "InvoiceID");
                getname.Parameters["@InvoiceID"].Value = invoiceid.ToString();

                object result = getname.ExecuteScalar();
                if(result is string)
                    OwnerName.Text = (string)result;

                PropertyNumber.Text = ((int)InvoicesSet.Tables["Invoices"].Rows[0]["PropertyID"]).ToString();
                InvoiceNumber.Text = ((int)InvoicesSet.Tables["Invoices"].Rows[0]["ID"]).ToString();
                if(InvoicesSet.Tables["Invoices"].Rows[0]["InvoiceDate"] is DateTime) {
                    InvoiceDay.SelectedValue = ((DateTime)InvoicesSet.Tables["Invoices"].Rows[0]["InvoiceDate"]).Day.ToString();
                    InvoiceMonth.SelectedValue = ((DateTime)InvoicesSet.Tables["Invoices"].Rows[0]["InvoiceDate"]).Month.ToString();
                    InvoiceYear.SelectedValue = ((DateTime)InvoicesSet.Tables["Invoices"].Rows[0]["InvoiceDate"]).Year.ToString();
                }
                if(InvoicesSet.Tables["Invoices"].Rows[0]["InvoiceAmount"] is decimal)
                    InvoiceAmount.Text = ((decimal)InvoicesSet.Tables["Invoices"].Rows[0]["InvoiceAmount"]).ToString();
                if(InvoicesSet.Tables["Invoices"].Rows[0]["PaymentDate"] is DateTime) {
                    PaymentDay.SelectedValue = ((DateTime)InvoicesSet.Tables["Invoices"].Rows[0]["PaymentDate"]).Day.ToString();
                    PaymentMonth.SelectedValue = ((DateTime)InvoicesSet.Tables["Invoices"].Rows[0]["PaymentDate"]).Month.ToString();
                    PaymentYear.SelectedValue = ((DateTime)InvoicesSet.Tables["Invoices"].Rows[0]["PaymentDate"]).Year.ToString();
                }
                else {
                    PaymentDay.SelectedValue = DateTime.Now.Day.ToString();
                    PaymentMonth.SelectedValue = DateTime.Now.Month.ToString();
                    PaymentYear.SelectedValue = DateTime.Now.Year.ToString();
                }
                if(InvoicesSet.Tables["Invoices"].Rows[0]["PaymentAmount"] is decimal)
                    PaymentAmount.Text = ((decimal)InvoicesSet.Tables["Invoices"].Rows[0]["PaymentAmount"]).ToString();
                if(InvoicesSet.Tables["Invoices"].Rows[0]["PaymentType"] is string)
                    PaymentType.SelectedValue = (string)InvoicesSet.Tables["Invoices"].Rows[0]["PaymentType"];
                if((InvoicesSet.Tables["Invoices"].Rows[0]["InvoiceAmount"] is decimal) &&
                        (InvoicesSet.Tables["Invoices"].Rows[0]["PaymentAmount"] is decimal))
                    Balance.Text = ((decimal)InvoicesSet.Tables["Invoices"].Rows[0]["InvoiceAmount"] -
                        (decimal)InvoicesSet.Tables["Invoices"].Rows[0]["PaymentAmount"]).ToString();
                connection.Close();
            }
	}

	#region Web Form Designer generated code
	override protected void OnInit(EventArgs e)
	{
		//
		// CODEGEN: This call is required by the ASP.NET Web Form Designer.
		//
		InitializeComponent();
		base.OnInit(e);
	}
	
	/// <summary>
	/// Required method for Designer support - do not modify
	/// the contents of this method with the code editor.
	/// </summary>
	private void InitializeComponent()
	{    
		//CommonFunctions.Connection = new System.Data.SqlClient.SqlConnection();
		this.InvoicesAdapter = new System.Data.SqlClient.SqlDataAdapter();
		this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
		this.InvoicesSet = new Vacations.InvoicesDataset();
		this.PropertiesAdapter = new System.Data.SqlClient.SqlDataAdapter();
		this.PropertiesFullSet = new Vacations.PropertiesFullDataset();
		this.sqlSelectCommand2 = new System.Data.SqlClient.SqlCommand();
		((System.ComponentModel.ISupportInitialize)(this.InvoicesSet)).BeginInit();
		((System.ComponentModel.ISupportInitialize)(this.PropertiesFullSet)).BeginInit();
		// 
		// CommonFunctions.Connection
		// 
		//CommonFunctions.Connection.ConnectionString = "workstation id=MAIN;packet size=4096;integrated security=SSPI;data source=MAIN;pe" +
			//"rsist security info=False;initial catalog=Vacations";
		// 
		// InvoicesAdapter
		// 
		this.InvoicesAdapter.SelectCommand = this.sqlSelectCommand1;
		this.InvoicesAdapter.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																								  new System.Data.Common.DataTableMapping("Table", "Invoices", new System.Data.Common.DataColumnMapping[] {
																																																			  new System.Data.Common.DataColumnMapping("ID", "ID"),
																																																			  new System.Data.Common.DataColumnMapping("PropertyID", "PropertyID"),
																																																			  new System.Data.Common.DataColumnMapping("InvoiceDate", "InvoiceDate"),
																																																			  new System.Data.Common.DataColumnMapping("InvoiceAmount", "InvoiceAmount"),
																																																			  new System.Data.Common.DataColumnMapping("PaymentDate", "PaymentDate"),
																																																			  new System.Data.Common.DataColumnMapping("PaymentAmount", "PaymentAmount"),
																																																			  new System.Data.Common.DataColumnMapping("PaymentType", "PaymentType"),
																																																			  new System.Data.Common.DataColumnMapping("Balance", "Balance")})});
		// 
		// sqlSelectCommand1
		// 
		this.sqlSelectCommand1.CommandText = "SELECT ID, PropertyID, InvoiceDate, InvoiceAmount, PaymentDate, PaymentAmount, PaymentType FROM Invoices WHERE (ID = @InvoiceID)";
		this.sqlSelectCommand1.Connection = CommonFunctions.GetConnection();
		this.sqlSelectCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InvoiceID", System.Data.SqlDbType.Int, 4, "ID"));
		// 
		// InvoicesSet
		// 
		this.InvoicesSet.DataSetName = "InvoicesDataset";
		this.InvoicesSet.Locale = new System.Globalization.CultureInfo("en-US");
		// 
		// PropertiesAdapter
		// 
		this.PropertiesAdapter.SelectCommand = this.sqlSelectCommand2;
		this.PropertiesAdapter.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																									new System.Data.Common.DataTableMapping("Table", "Properties", new System.Data.Common.DataColumnMapping[] {
																																																				  new System.Data.Common.DataColumnMapping("MinimumNightlyRental", "MinimumNightlyRental"),
																																																				  new System.Data.Common.DataColumnMapping("Type", "Type"),
																																																				  new System.Data.Common.DataColumnMapping("FirstName", "FirstName"),
																																																				  new System.Data.Common.DataColumnMapping("LastName", "LastName"),
																																																				  new System.Data.Common.DataColumnMapping("PrimaryTelephone", "PrimaryTelephone"),
																																																				  new System.Data.Common.DataColumnMapping("OwnerCountry", "OwnerCountry"),
																																																				  new System.Data.Common.DataColumnMapping("Email", "Email"),
																																																				  new System.Data.Common.DataColumnMapping("Username", "Username"),
																																																				  new System.Data.Common.DataColumnMapping("OwnerAddress", "OwnerAddress"),
																																																				  new System.Data.Common.DataColumnMapping("EveningTelephone", "EveningTelephone"),
																																																				  new System.Data.Common.DataColumnMapping("DaytimeTelephone", "DaytimeTelephone"),
																																																				  new System.Data.Common.DataColumnMapping("MobileTelephone", "MobileTelephone"),
																																																				  new System.Data.Common.DataColumnMapping("Website", "Website"),
																																																				  new System.Data.Common.DataColumnMapping("OwnerCity", "OwnerCity"),
																																																				  new System.Data.Common.DataColumnMapping("OwnerState", "OwnerState"),
																																																				  new System.Data.Common.DataColumnMapping("OwnerZip", "OwnerZip"),
																																																				  new System.Data.Common.DataColumnMapping("Registered", "Registered"),
																																																				  new System.Data.Common.DataColumnMapping("IfPayTravelAgents", "IfPayTravelAgents"),
																																																				  new System.Data.Common.DataColumnMapping("City", "City"),
																																																				  new System.Data.Common.DataColumnMapping("StateProvince", "StateProvince"),
																																																				  new System.Data.Common.DataColumnMapping("Country", "Country"),
																																																				  new System.Data.Common.DataColumnMapping("Region", "Region"),
																																																				  new System.Data.Common.DataColumnMapping("Smoking", "Smoking"),
																																																				  new System.Data.Common.DataColumnMapping("PetFriendly", "PetFriendly"),
																																																				  new System.Data.Common.DataColumnMapping("ID", "ID"),
																																																				  new System.Data.Common.DataColumnMapping("UserID", "UserID"),
																																																				  new System.Data.Common.DataColumnMapping("Name", "Name"),
																																																				  new System.Data.Common.DataColumnMapping("TypeID", "TypeID"),
																																																				  new System.Data.Common.DataColumnMapping("Address", "Address"),
																																																				  new System.Data.Common.DataColumnMapping("CityID", "CityID"),
																																																				  new System.Data.Common.DataColumnMapping("IfShowAddress", "IfShowAddress"),
																																																				  new System.Data.Common.DataColumnMapping("NumBedrooms", "NumBedrooms"),
																																																				  new System.Data.Common.DataColumnMapping("NumBaths", "NumBaths"),
																																																				  new System.Data.Common.DataColumnMapping("NumSleeps", "NumSleeps"),
																																																				  new System.Data.Common.DataColumnMapping("MinimumNightlyRentalID", "MinimumNightlyRentalID"),
																																																				  new System.Data.Common.DataColumnMapping("NumTVs", "NumTVs"),
																																																				  new System.Data.Common.DataColumnMapping("NumVCRs", "NumVCRs"),
																																																				  new System.Data.Common.DataColumnMapping("NumCDPlayers", "NumCDPlayers"),
																																																				  new System.Data.Common.DataColumnMapping("Description", "Description"),
																																																				  new System.Data.Common.DataColumnMapping("Amenities", "Amenities"),
																																																				  new System.Data.Common.DataColumnMapping("LocalAttractions", "LocalAttractions"),
																																																				  new System.Data.Common.DataColumnMapping("Rates", "Rates"),
																																																				  new System.Data.Common.DataColumnMapping("CancellationPolicy", "CancellationPolicy"),
																																																				  new System.Data.Common.DataColumnMapping("DepositRequired", "DepositRequired"),
																																																				  new System.Data.Common.DataColumnMapping("IfMoreThan7PhotosAllowed", "IfMoreThan7PhotosAllowed"),
																																																				  new System.Data.Common.DataColumnMapping("IfFinished", "IfFinished"),
																																																				  new System.Data.Common.DataColumnMapping("IfApproved", "IfApproved"),
																																																				  new System.Data.Common.DataColumnMapping("IfPaid", "IfPaid"),
																																																				  new System.Data.Common.DataColumnMapping("DateAdded", "DateAdded"),
																																																				  new System.Data.Common.DataColumnMapping("DateStartViewed", "DateStartViewed"),
																																																				  new System.Data.Common.DataColumnMapping("VirtualTour", "VirtualTour"),
																																																				  new System.Data.Common.DataColumnMapping("RatesTable", "RatesTable"),
																																																				  new System.Data.Common.DataColumnMapping("PricesCurrency", "PricesCurrency"),
																																																				  new System.Data.Common.DataColumnMapping("CheckIn", "CheckIn"),
																																																				  new System.Data.Common.DataColumnMapping("CheckOut", "CheckOut"),
																																																				  new System.Data.Common.DataColumnMapping("LodgingTax", "LodgingTax"),
																																																				  new System.Data.Common.DataColumnMapping("TaxIncluded", "TaxIncluded"),
																																																				  new System.Data.Common.DataColumnMapping("DateAvailable", "DateAvailable"),
																																																				  new System.Data.Common.DataColumnMapping("IfDiscounted", "IfDiscounted"),
																																																				  new System.Data.Common.DataColumnMapping("IfLastMinuteCancellations", "IfLastMinuteCancellations"),
																																																				  new System.Data.Common.DataColumnMapping("LastMinuteComments", "LastMinuteComments"),
																																																				  new System.Data.Common.DataColumnMapping("HomeExchangeCityID1", "HomeExchangeCityID1"),
																																																				  new System.Data.Common.DataColumnMapping("HomeExchangeCityID2", "HomeExchangeCityID2"),
																																																				  new System.Data.Common.DataColumnMapping("HomeExchangeCityID3", "HomeExchangeCityID3")})});
		// 
		// PropertiesFullSet
		// 
		this.PropertiesFullSet.DataSetName = "PropertiesFullDataset";
		this.PropertiesFullSet.Locale = new System.Globalization.CultureInfo("en-US");
		// 
		// sqlSelectCommand2
		// 
		this.sqlSelectCommand2.CommandText = "SELECT MinimumNightlyRentalTypes.Name AS MinimumNightlyRental, PropertyTypes.Name" +
			" AS Type, Users.FirstName, Users.LastName, Users.PrimaryTelephone, Users.Country" +
			" AS OwnerCountry, Users.Email, Users.Username, Users.Address AS OwnerAddress, Us" +
			"ers.EveningTelephone, Users.DaytimeTelephone, Users.MobileTelephone, Users.Websi" +
			"te, Users.City AS OwnerCity, Users.State AS OwnerState, Users.Zip AS OwnerZip, U" +
			"sers.Registered, Users.IfPayTravelAgents, Cities.City, StateProvinces.StateProvi" +
			"nce, Countries.Country, Regions.Region, CASE WHEN EXISTS (SELECT * FROM Properti" +
			"esAmenities INNER JOIN Amenities ON PropertiesAmenities.AmenityID = Amenities.ID" +
			" WHERE (PropertiesAmenities.PropertyID = Properties.ID) AND (Amenities.Amenity =" +
			" \'Smoking Permitted\')) THEN \'Yes\' ELSE \'No\' END AS Smoking, CASE WHEN EXISTS (SE" +
			"LECT * FROM PropertiesAmenities INNER JOIN Amenities ON PropertiesAmenities.Amen" +
			"ityID = Amenities.ID WHERE (PropertiesAmenities.PropertyID = Properties.ID) AND " +
			"(Amenities.Amenity = \'Pet Friendly\')) THEN \'Yes\' ELSE \'No\' END AS PetFriendly, P" +
			"roperties.ID, Properties.UserID, Properties.Name, Properties.TypeID, Properties." +
			"Address, Properties.CityID, Properties.IfShowAddress, Properties.NumBedrooms, Pr" +
			"operties.NumBaths, Properties.NumSleeps, Properties.MinimumNightlyRentalID, Prop" +
			"erties.NumTVs, Properties.NumVCRs, Properties.NumCDPlayers, Properties.IfMoreThan7Photos" +
			"Allowed, Properties.IfFinished, Properties.IfApproved, CASE WHEN EXISTS (SELECT " +
			"* FROM Invoices WHERE (Invoices.PropertyID = Properties.ID) AND (PaymentAmount >= InvoiceAmount) AND (GETDATE() <= RenewalDate)) THEN 1" +
			" ELSE 0 END AS IfPaid, Properties.DateAdded, Properties.DateStartViewed, Propert" +
			"ies.VirtualTour, Properties.RatesTable, Properties.PricesCurrency, Properties.Ch" +
			"eckIn, Properties.CheckOut, Properties.LodgingTax, Properties.TaxIncluded, Prope" +
			"rties.DateAvailable, Properties.IfDiscounted, Properties.IfLastMinuteCancellatio" +
			"ns, Properties.HomeExchangeCityID1, Properties.Ho" +
			"meExchangeCityID2, Properties.HomeExchangeCityID3 FROM Properties INNER JOIN Cit" +
			"ies ON Properties.CityID = Cities.ID INNER JOIN StateProvinces ON StateProvinces" +
			".ID = Cities.StateProvinceID INNER JOIN Countries ON StateProvinces.CountryID = " +
			"Countries.ID INNER JOIN Regions ON Countries.RegionID = Regions.ID INNER JOIN Us" +
			"ers ON Properties.UserID = Users.ID LEFT OUTER JOIN MinimumNightlyRentalTypes ON" +
			" Properties.MinimumNightlyRentalID = MinimumNightlyRentalTypes.ID LEFT OUTER JOI" +
			"N PropertyTypes ON Properties.TypeID = PropertyTypes.ID WHERE (Properties.ID = @" +
			"PropertyID)";
		this.sqlSelectCommand2.Connection = CommonFunctions.GetConnection();
		this.sqlSelectCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PropertyID", System.Data.SqlDbType.Int, 4, "ID"));
		((System.ComponentModel.ISupportInitialize)(this.InvoicesSet)).EndInit();
		((System.ComponentModel.ISupportInitialize)(this.PropertiesFullSet)).EndInit();

	}
	#endregion

	protected void ChooseInvoice_Click(object sender, System.EventArgs e)
	{
		NotFound.Visible = false;

		EnterInvoiceNumber.Validate ();
		ValidInvoiceNumber.Validate ();

		if (!EnterInvoiceNumber.IsValid || !ValidInvoiceNumber.IsValid)
			return;

		int invoiceid = Convert.ToInt32 (ChooseInvoiceNumber.Text);

		string querystring = Request.QueryString.ToString ();
		int pos = querystring.IndexOf ("InvoiceID=") + "InvoiceID=".Length;
		int pos2 = querystring.IndexOf ("&", pos);
		if (pos2 != -1)
			querystring = querystring.Substring (0, pos) + invoiceid.ToString () + querystring.Substring (pos2);
		else
			querystring = querystring.Substring (0, pos) + invoiceid.ToString ();

		Response.Redirect (CommonFunctions.PrepareURL ("ApplyPayment.aspx?" + querystring));
	}

	protected void ChooseProperty_Click(object sender, System.EventArgs e)
	{
		NotFound.Visible = false;

		EnterPropertyNumber.Validate ();
		ValidPropertyNumber.Validate ();

		if (!EnterPropertyNumber.IsValid || !ValidPropertyNumber.IsValid)
			return;

		int propertyid = Convert.ToInt32 (ChoosePropertyNumber.Text);
        object result = null;
        using(SqlConnection connection = CommonFunctions.GetConnection()) {
            connection.Open();
            SqlCommand getid = new SqlCommand("SELECT TOP 1 ID FROM Invoices WHERE (PropertyID=@PropertyID) ORDER BY InvoiceDate DESC", connection);
            getid.Parameters.Add("@PropertyID", SqlDbType.Int, 4, "PropertyID");
            getid.Parameters["@PropertyID"].Value = propertyid;

            result = getid.ExecuteScalar();
            connection.Close();
        }
		if (result is int)
		{
			string querystring = Request.QueryString.ToString ();
			int pos = querystring.IndexOf ("InvoiceID=") + "InvoiceID=".Length;
			int pos2 = querystring.IndexOf ("&", pos);
			if (pos2 != -1)
				querystring = querystring.Substring (0, pos) + ((int)result).ToString () + querystring.Substring (pos2);
			else
				querystring = querystring.Substring (0, pos) + ((int)result).ToString ();

			Response.Redirect (CommonFunctions.PrepareURL ("ApplyPayment.aspx?" + querystring));
		}
		else
			NotFound.Visible = true;
	}

	protected void SubmitButton_Click(object sender, System.EventArgs e)
	{
		NotFound.Visible = false;

		ValidPaymentAmount.Validate ();

		if (!ValidPaymentAmount.IsValid)
			return;

		if (Request.Params["InvoiceID"] != null)
		{
			int invoiceid = Convert.ToInt32 (Request.Params["InvoiceID"]);

			InvoicesAdapter.SelectCommand.Parameters["@InvoiceID"].Value = invoiceid;

			InvoicesSet.Clear ();
			int fillres;
			//lock (CommonFunctions.Connection)
				fillres = InvoicesAdapter.Fill (InvoicesSet);
			if (fillres > 0)
			{
				try
				{
					InvoicesSet.Tables["Invoices"].Rows[0]["InvoiceDate"] = new DateTime (Convert.ToInt32 (InvoiceYear.SelectedValue),
						Convert.ToInt32 (InvoiceMonth.SelectedValue), Convert.ToInt32 (InvoiceDay.SelectedValue));
				}
				catch (Exception)
				{
				}

				try
				{
					InvoicesSet.Tables["Invoices"].Rows[0]["PaymentDate"] = new DateTime (Convert.ToInt32 (PaymentYear.SelectedValue),
						Convert.ToInt32 (PaymentMonth.SelectedValue), Convert.ToInt32 (PaymentDay.SelectedValue));
				}
				catch (Exception)
				{
				}

				if ((InvoicesSet.Tables["Invoices"].Rows[0]["InvoiceDate"] is DateTime) &&
						(InvoicesSet.Tables["Invoices"].Rows[0]["PaymentDate"] is DateTime) &&
						((DateTime)InvoicesSet.Tables["Invoices"].Rows[0]["PaymentDate"] >
						((DateTime)InvoicesSet.Tables["Invoices"].Rows[0]["InvoiceDate"]).AddDays (15)))
					InvoicesSet.Tables["Invoices"].Rows[0]["InvoiceDate"] =
						InvoicesSet.Tables["Invoices"].Rows[0]["PaymentDate"];

				try
				{
					InvoicesSet.Tables["Invoices"].Rows[0]["PaymentAmount"] = Convert.ToDecimal (PaymentAmount.Text);
				}
				catch (Exception)
				{
				}

				try
				{
					InvoicesSet.Tables["Invoices"].Rows[0]["PaymentType"] = PaymentType.SelectedValue;
				}
				catch (Exception)
				{
				}

				//lock (CommonFunctions.Connection)
					InvoicesAdapter.Update (InvoicesSet);

				PropertiesAdapter.SelectCommand.Parameters["@PropertyID"].Value =
					InvoicesSet.Tables["Invoices"].Rows[0]["PropertyID"];

				PropertiesFullSet.Clear ();
				if ((CommonFunctions.SyncFill (PropertiesAdapter, PropertiesFullSet) > 0) &&
					!(PropertiesFullSet.Tables["Properties"].Rows[0]["Email"] is DBNull))
				{
					string emailbody;

					emailbody = "A payment was just applied! Thank you for making payment on your property<br />\n" +
						"Invoice No: " + ((int)InvoicesSet.Tables["Invoices"].Rows[0]["ID"]).ToString () + "<br />\n" +
						"Payment Amount: $" + ((decimal)InvoicesSet.Tables["Invoices"].Rows[0]["PaymentAmount"]).ToString () + "<br />\n";

					SmtpClient smtpclient = new SmtpClient (ConfigurationManager.AppSettings["SMTPServer"],
						int.Parse (ConfigurationManager.AppSettings["SMTPPort"]));

					MailMessage message = new MailMessage ("noreply@" + CommonFunctions.GetDomainName (),
						(string)PropertiesFullSet.Tables["Properties"].Rows[0]["Email"]);
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
				}

				Response.Redirect (backlinkurl);
			}
		}
	}

	protected void PaymentAmount_TextChanged(object sender, System.EventArgs e)
	{
		try
		{
			Balance.Text = (Convert.ToDecimal (InvoiceAmount.Text) - Convert.ToDecimal (PaymentAmount.Text)).ToString ();
		}
		catch (Exception)
		{
		}
	}
}
