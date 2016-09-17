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

public partial class ViewAuction : CommonPage
{
	protected bool ifended;
	protected bool ifpaid;

	//protected SqlConnection Connection = new SqlConnection (ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
	protected SqlDataAdapter AuctionsAdapter;
	protected SqlDataAdapter PropertiesAdapter;
	protected SqlDataAdapter PhotosAdapter;
	protected SqlDataAdapter AmenitiesAdapter;
	protected SqlDataAdapter QuestionsAdapter;
	protected SqlDataAdapter PaymentMethodsAdapter;
	protected DataSet MainDataSet = new DataSet ();

	protected void Page_Load(object sender, EventArgs e)
    {
		if (auctionid == -1)
			Response.Redirect (CommonFunctions.PrepareURL ("InternalError.aspx"), true);

        PropertiesAdapter = CommonFunctions.PrepareAdapter(CommonFunctions.GetConnection(), "SELECT Auctions.ID AS AuctionID, Auctions.Title," +
			" Auctions.MinimumBid, Auctions.AuctionStart, Auctions.AuctionEnd, Auctions.RentalStart, Auctions.RentalEnd," +
			" Auctions.BidAmount, Auctions.IfBasedAvailability, Auctions.PaymentTerms," +
			" CASE WHEN EXISTS (SELECT * FROM Transactions WHERE (AuctionID = Auctions.ID) AND (IfListingFee = 1) AND" +
			" (PaymentAmount >= InvoiceAmount)) THEN 1 ELSE 0 END AS IfAuctionPaid, Properties.ID AS PropertyID," +
			" Properties.Name, Properties.Address, Properties.IfShowAddress, Properties.NumBedrooms, Properties.NumBaths," +
			" Properties.NumSleeps, Properties.NumTVs, Properties.NumVCRs, Properties.NumCDPlayers," +
			" Properties.Description, Properties.Amenities, Properties.IfMoreThan7PhotosAllowed, Properties.LodgingTax," +
			" Properties.TaxIncluded, RentalLengths.RentalLength, CASE WHEN EXISTS (SELECT * FROM Invoices " +
			"WHERE (PropertyID = Properties.ID) AND (PaymentAmount >= InvoiceAmount) AND" +
			" (GETDATE() <= Invoices.RenewalDate)) THEN 1 ELSE 0 END AS IfAnnualFee," +
			" MinimumNightlyRentalTypes.Name AS MinimumNightlyRental, PropertyTypes.Name AS Type," +
			" Countries.Country, StateProvinces.StateProvince, Cities.City, Users.ID AS UserID, Users.Registered," +
			" ISNULL(Users.UserID, Users.Username) AS Username " +
			"FROM Auctions INNER JOIN Properties ON Auctions.PropertyID = Properties.ID" +
			" INNER JOIN Cities ON Properties.CityID = Cities.ID" +
			" INNER JOIN StateProvinces ON StateProvinces.ID = Cities.StateProvinceID" +
			" INNER JOIN Countries ON StateProvinces.CountryID = Countries.ID" +
			" INNER JOIN Regions ON Countries.RegionID = Regions.ID INNER JOIN Users ON Properties.UserID = Users.ID" +
			" LEFT OUTER JOIN MinimumNightlyRentalTypes ON Properties.MinimumNightlyRentalID = MinimumNightlyRentalTypes.ID" +
			" LEFT OUTER JOIN PropertyTypes ON Properties.TypeID = PropertyTypes.ID" +
			" LEFT OUTER JOIN RentalLengths ON Auctions.RentalLengthID = RentalLengths.ID " +
			"WHERE (Auctions.ID = @AuctionID)", SqlDbType.Int);

		PropertiesAdapter.SelectCommand.Parameters["@AuctionID"].Value = auctionid;

		//lock (CommonFunctions.Connection)
			if (PropertiesAdapter.Fill (MainDataSet, "Properties") == 0)
				Response.Redirect (CommonFunctions.PrepareURL ("InternalError.aspx"), true);

		propertyid = (int)MainDataSet.Tables["Properties"].Rows[0]["PropertyID"];

		if ((MainDataSet.Tables["Properties"].Rows[0]["IfMoreThan7PhotosAllowed"] is bool) &&
				(bool)MainDataSet.Tables["Properties"].Rows[0]["IfMoreThan7PhotosAllowed"])
            PhotosAdapter = CommonFunctions.PrepareAdapter(CommonFunctions.GetConnection(), "SELECT * FROM PropertyPhotos " +
				"WHERE (PropertyID = @PropertyID) ORDER BY OrderNumber", SqlDbType.Int);
		else
            PhotosAdapter = CommonFunctions.PrepareAdapter(CommonFunctions.GetConnection(), "SELECT TOP 7 * FROM PropertyPhotos " +
				"WHERE (PropertyID = @PropertyID) ORDER BY OrderNumber", SqlDbType.Int);

        AmenitiesAdapter = CommonFunctions.PrepareAdapter(CommonFunctions.GetConnection(), "SELECT * " +
			"FROM Amenities INNER JOIN PropertiesAmenities ON Amenities.ID = PropertiesAmenities.AmenityID " +
			"WHERE (PropertiesAmenities.PropertyID = @PropertyID) AND (Amenities.Amenity <> 'TV') AND" +
			" (Amenities.Amenity <> 'VCR') AND (Amenities.Amenity <> 'CD Player')", SqlDbType.Int);

        QuestionsAdapter = CommonFunctions.PrepareAdapter(CommonFunctions.GetConnection(), "SELECT * FROM AuctionQuestions " +
			"WHERE AuctionID = @AuctionID", SqlDbType.Int);

        PaymentMethodsAdapter = CommonFunctions.PrepareAdapter(CommonFunctions.GetConnection(), "SELECT PaymentMethods.ID," +
			" PaymentMethods.PaymentMethod FROM PaymentMethods INNER JOIN PropertiesPaymentMethods ON" +
			" PaymentMethods.ID = PropertiesPaymentMethods.PaymentMethodID " +
			"WHERE (PropertiesPaymentMethods.PropertyID = @PropertyID)", SqlDbType.Int);

		PhotosAdapter.SelectCommand.Parameters["@PropertyID"].Value = propertyid;
		AmenitiesAdapter.SelectCommand.Parameters["@PropertyID"].Value = propertyid;
		QuestionsAdapter.SelectCommand.Parameters["@AuctionID"].Value = auctionid;
		PaymentMethodsAdapter.SelectCommand.Parameters["@PropertyID"].Value = propertyid;

		//lock (CommonFunctions.Connection)
			PhotosAdapter.Fill (MainDataSet, "Photos");
		//lock (CommonFunctions.Connection)
			AmenitiesAdapter.Fill (MainDataSet, "Amenities");
		//lock (CommonFunctions.Connection)
			QuestionsAdapter.Fill (MainDataSet, "AuctionQuestions");
		//lock (CommonFunctions.Connection)
			PaymentMethodsAdapter.Fill (MainDataSet, "PaymentMethods");

		ifended = (MainDataSet.Tables["Properties"].Rows[0]["AuctionEnd"] is DateTime) &&
			((DateTime)MainDataSet.Tables["Properties"].Rows[0]["AuctionEnd"] < DateTime.Now);
		ifpaid = (MainDataSet.Tables["Properties"].Rows[0]["IfAuctionPaid"] is int) &&
			((int)MainDataSet.Tables["Properties"].Rows[0]["IfAuctionPaid"] == 1);

		DataTable firstphoto = MainDataSet.Tables["Photos"].Clone ();
		firstphoto.TableName = "FirstPhoto";
		MainDataSet.Tables.Add (firstphoto);

		if (MainDataSet.Tables["Photos"].Rows.Count > 0)
		{
			DataRow photocopy = MainDataSet.Tables["FirstPhoto"].NewRow ();

			photocopy["ID"] = MainDataSet.Tables["Photos"].Rows[0]["ID"];
			photocopy["FileName"] = MainDataSet.Tables["Photos"].Rows[0]["FileName"];
			photocopy["Width"] = MainDataSet.Tables["Photos"].Rows[0]["Width"];
			photocopy["Height"] = MainDataSet.Tables["Photos"].Rows[0]["Height"];

			MainDataSet.Tables["FirstPhoto"].Rows.Add (photocopy);

			MainDataSet.Tables["Photos"].Rows.RemoveAt (0);
		}

		DataBind ();

		HtmlHead head = Page.Header;

		HtmlMeta description = new HtmlMeta ();

		description.Name = "description";
		description.Content = GetTitle ();

		head.Controls.Add (description);

		if (Request.QueryString["PlaceBid"] != null)
		{
			BidAmount.Text = Request.QueryString["PlaceBid"];
			BidNowButton_Click (sender, e);
		}
	}

	public string GetTitle ()
	{
		return (string)MainDataSet.Tables["Properties"].Rows[0]["Title"];
	}

	public bool IfAmenityPresent (string Amenity)
	{
		foreach (DataRow datarow in MainDataSet.Tables["Amenities"].Rows)
			if (datarow.RowState != DataRowState.Deleted)
				if (string.Compare ((string)datarow["Amenity"], Amenity, true) == 0)
					return true;

		return false;
	}

	public bool PaymentMethodsPresent ()
	{
		return (MainDataSet.Tables["PaymentMethods"].Rows.Count > 0);
	}

	public bool LodgingTaxPresent ()
	{
		return (MainDataSet.Tables["Properties"].Rows[0]["LodgingTax"] is string);
	}

	public bool PicturesTooWide (DataRowView CurrentPhoto, int MaxWidth)
	{
		bool ifnext = false;
		int sumlen = 0;

		foreach (DataRowView rowview in CurrentPhoto.DataView)
		{
			sumlen += (int)rowview["Width"];

			if (sumlen > MaxWidth)
				sumlen = 0;

			if (ifnext)
				return (sumlen == 0);

			if ((int)rowview.Row["ID"] == (int)CurrentPhoto.Row["ID"])
			{
				ifnext = true;
				if (sumlen == 0)
					return true;
			}
		}

		return true;
	}

	protected void BidNowButton_Click (object sender, EventArgs e)
	{
		int bidamount = 0;

		if (!ifpaid)
			return;

		try
		{
			bidamount = System.Convert.ToInt32 (BidAmount.Text);
		}
		catch (Exception)
		{
		}

		BidNotAccepted.Visible = false;

		if ((int)MainDataSet.Tables["Properties"].Rows[0]["UserID"] == AuthenticationManager.UserID)
		{
			BidNotAccepted.Text = "You can't bid on your own auction";
			BidNotAccepted.Visible = true;
			return;
		}

		if (((MainDataSet.Tables["Properties"].Rows[0]["MinimumBid"] is int) &&
			(bidamount < (int)MainDataSet.Tables["Properties"].Rows[0]["MinimumBid"])) ||
			((MainDataSet.Tables["Properties"].Rows[0]["BidAmount"] is int) &&
			(bidamount < (int)MainDataSet.Tables["Properties"].Rows[0]["BidAmount"])))
		{
			BidNotAccepted.Text = "Your bid was not accepted because it is less than the Current Bid or Minimum Bid.";
			BidNotAccepted.Visible = true;
			return;
		}

		if (ifended)
		{
			BidNotAccepted.Text = "Auction has already ended.";
			BidNotAccepted.Visible = true;
			return;
		}

		if (!AuthenticationManager.IfAuthenticated)
			Response.Redirect (CommonFunctions.PrepareURL ("FindOwner.aspx?BackLink=" +
				HttpUtility.UrlEncode (Request.Url.ToString () + "&PlaceBid=" + bidamount.ToString ())), true);

        AuctionsAdapter = CommonFunctions.PrepareAdapter(CommonFunctions.GetConnection(), "SELECT * FROM Auctions WHERE ID = @AuctionID",
			SqlDbType.Int);

		AuctionsAdapter.SelectCommand.Parameters["@AuctionID"].Value = auctionid;

		//lock (CommonFunctions.Connection)
			if (AuctionsAdapter.Fill (MainDataSet, "Auctions") < 0)
				Response.Redirect (CommonFunctions.PrepareURL ("InternalError.aspx"), true);

		MainDataSet.Tables["Auctions"].Rows[0]["HighestBidderID"] = AuthenticationManager.UserID;
		MainDataSet.Tables["Auctions"].Rows[0]["BidAmount"] = bidamount;
		MainDataSet.Tables["Auctions"].Rows[0]["BidDate"] = DateTime.Now;
		if (MainDataSet.Tables["Auctions"].Rows[0]["BidsNumber"] is int)
			MainDataSet.Tables["Auctions"].Rows[0]["BidsNumber"] =
				(int)MainDataSet.Tables["Auctions"].Rows[0]["BidsNumber"] + 1;
		else
			MainDataSet.Tables["Auctions"].Rows[0]["BidsNumber"] = 1;

		//lock (CommonFunctions.Connection)
			AuctionsAdapter.Update (MainDataSet, "Auctions");

		Session["Message"] = "BidSuccess";
		Response.Redirect (CommonFunctions.PrepareURL ("MyAccount.aspx?AuctionID=" + auctionid.ToString ()));
	}

	protected void DeleteButton_Click (object sender, EventArgs e)
	{
		Response.Redirect (CommonFunctions.PrepareURL ("DeleteAuction.aspx?AuctionID=" + auctionid.ToString () +
			"&BackLink=" + HttpUtility.UrlEncode (backlinkurl)));
	}

	protected void SendEmailButton_Click (object sender, EventArgs e)
	{
		Response.Redirect (CommonFunctions.PrepareURL ("SendCustomEmail.aspx?PropertyID=" + propertyid.ToString (),
			"Auction Item"));
	}
}
