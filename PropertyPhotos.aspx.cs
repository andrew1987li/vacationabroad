using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class PropertyPhotos : ClosedPage
{
	protected bool morethan7allowed;
	protected bool allowupload;
	protected bool ifadd;
	protected bool ifauction;

	protected System.Data.SqlClient.SqlDataAdapter PropertiesAdapter;
	protected Vacations.PropertiesDataset PropertiesSet;
	//protected System.Data.SqlClient.SqlConnection Connection;
	protected System.Data.SqlClient.SqlDataAdapter PhotosAdapter;
	protected Vacations.PhotosDataset PhotosSet;
	protected System.Data.SqlClient.SqlCommand sqlSelectCommand2;
	protected System.Data.SqlClient.SqlCommand sqlSelectCommand1;

	protected void Page_Load(object sender, System.EventArgs e)
	{
		bool ifok;

		System.Data.SqlClient.SqlCommandBuilder builder = new System.Data.SqlClient.SqlCommandBuilder (PropertiesAdapter);
		System.Data.SqlClient.SqlCommandBuilder builder2 = new System.Data.SqlClient.SqlCommandBuilder (PhotosAdapter);

		//string connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

		//CommonFunctions.Connection.ConnectionString = connectionstring;
		//CommonFunctions.Connection.Open ();

		ifok = false;
		ifadd = false;
		morethan7allowed = false;
		allowupload = false;
		if (propertyid != -1)
		{
			PropertiesAdapter.SelectCommand.Parameters["@PropertyID"].Value = propertyid;

			if (CommonFunctions.SyncFill (PropertiesAdapter, PropertiesSet) > 0)
			{
				morethan7allowed = (bool)PropertiesSet.Tables["Properties"].Rows[0]["IfMoreThan7PhotosAllowed"];

				MoreThan7PhotosWarning.Visible = !morethan7allowed;
				ifadd = !(bool)PropertiesSet.Tables["Properties"].Rows[0]["IfFinished"];

				int uid = (int)PropertiesSet.Tables["Properties"].Rows[0]["UserID"];

				if ((uid == AuthenticationManager.UserID) || AuthenticationManager.IfAdmin)
				{
					PhotosAdapter.SelectCommand.Parameters["@PropertyID"].Value = propertyid;

					//lock (CommonFunctions.Connection)
						PhotosAdapter.Fill (PhotosSet);

					allowupload = morethan7allowed || (PhotosSet.Tables["PropertyPhotos"].Rows.Count < 7);

					if (!IsPostBack)
						DataBind ();

					ifok = true;
				}
			}
		}

        object auctionidresult = null;
        using(SqlConnection connection = CommonFunctions.GetConnection()) {
            connection.Open();
            SqlCommand GetAuctionID = new SqlCommand("SELECT ID FROM Auctions WHERE PropertyID = @PropertyID", connection);
            GetAuctionID.Parameters.Add("@PropertyID", SqlDbType.Int);
            GetAuctionID.Parameters["@PropertyID"].Value = propertyid;

            auctionidresult = GetAuctionID.ExecuteScalar();
            connection.Close();
        }

		ifauction = (auctionidresult is int);
		if (ifauction)
		{
			auctionid = (int)auctionidresult;

            object auctionpaidresult = null;
            using(SqlConnection connection = CommonFunctions.GetConnection()) {
                connection.Open();
                SqlCommand GetAuctionPaid = new SqlCommand("SELECT COUNT(*) FROM Transactions WHERE (AuctionID = @AuctionID)" +
                    " AND (PaymentAmount >= InvoiceAmount) AND (IfListingFee = 1)", connection);
                GetAuctionPaid.Parameters.Add("@AuctionID", SqlDbType.Int);
                GetAuctionPaid.Parameters["@AuctionID"].Value = auctionid;

                auctionpaidresult = GetAuctionPaid.ExecuteScalar();
                connection.Close();
            }

			ifadd = (auctionpaidresult is int) && ((int)auctionpaidresult == 0);

			allowupload = true;
		}

		if (ifadd)
			FinishButton.Text = "Next Step";
		else
			FinishButton.Text = "Finish";

		if (!ifok)
			Response.Redirect (CommonFunctions.PrepareURL ("InternalError.aspx"));
	}

	public string GetPreviewLink ()
	{
		if (ifauction)
			return CommonFunctions.PrepareURL (auctionid.ToString () + "/default.aspx?IfPopup");
		else
			return CommonFunctions.PrepareURL ("ViewProperty.aspx?PropertyID=" + propertyid.ToString () + "&IfPopup");
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
		this.PropertiesAdapter = new System.Data.SqlClient.SqlDataAdapter();
		this.PropertiesSet = new Vacations.PropertiesDataset();
		this.PhotosAdapter = new System.Data.SqlClient.SqlDataAdapter();
		this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
		this.PhotosSet = new Vacations.PhotosDataset();
		this.sqlSelectCommand2 = new System.Data.SqlClient.SqlCommand();
		((System.ComponentModel.ISupportInitialize)(this.PropertiesSet)).BeginInit();
		((System.ComponentModel.ISupportInitialize)(this.PhotosSet)).BeginInit();
		// 
		// CommonFunctions.Connection
		// 
		//CommonFunctions.Connection.ConnectionString = "workstation id=MAIN;packet size=4096;integrated security=SSPI;data source=MAIN;pe" +
			//"rsist security info=False;initial catalog=Vacations";
		// 
		// PropertiesAdapter
		// 
		this.PropertiesAdapter.SelectCommand = this.sqlSelectCommand2;
		this.PropertiesAdapter.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																									new System.Data.Common.DataTableMapping("Table", "Properties", new System.Data.Common.DataColumnMapping[] {
																																																				  new System.Data.Common.DataColumnMapping("ID", "ID"),
																																																				  new System.Data.Common.DataColumnMapping("UserID", "UserID"),
																																																				  new System.Data.Common.DataColumnMapping("Name", "Name"),
																																																				  new System.Data.Common.DataColumnMapping("TypeID", "TypeID"),
																																																				  new System.Data.Common.DataColumnMapping("Address", "Address"),
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
																																																				  new System.Data.Common.DataColumnMapping("IfApproved", "IfApproved"),
																																																				  new System.Data.Common.DataColumnMapping("CityID", "CityID"),
																																																				  new System.Data.Common.DataColumnMapping("IfFinished", "IfFinished"),
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
		// PropertiesSet
		// 
		this.PropertiesSet.DataSetName = "PropertiesDataset";
		this.PropertiesSet.Locale = new System.Globalization.CultureInfo("en-US");
		// 
		// PhotosAdapter
		// 
		this.PhotosAdapter.SelectCommand = this.sqlSelectCommand1;
		this.PhotosAdapter.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																								new System.Data.Common.DataTableMapping("Table", "PropertyPhotos", new System.Data.Common.DataColumnMapping[] {
																																																				  new System.Data.Common.DataColumnMapping("ID", "ID"),
																																																				  new System.Data.Common.DataColumnMapping("PropertyID", "PropertyID"),
																																																				  new System.Data.Common.DataColumnMapping("FileName", "FileName"),
																																																				  new System.Data.Common.DataColumnMapping("OrderNumber", "OrderNumber"),
																																																				  new System.Data.Common.DataColumnMapping("Width", "Width"),
																																																				  new System.Data.Common.DataColumnMapping("Height", "Height")})});
		// 
		// sqlSelectCommand1
		// 
		this.sqlSelectCommand1.CommandText = "SELECT ID, PropertyID, FileName, OrderNumber, Width, Height FROM PropertyPhotos W" +
			"HERE (PropertyID = @PropertyID) ORDER BY OrderNumber";
        this.sqlSelectCommand1.Connection = CommonFunctions.GetConnection();
		this.sqlSelectCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PropertyID", System.Data.SqlDbType.Int, 4, "PropertyID"));
		// 
		// PhotosSet
		// 
		this.PhotosSet.DataSetName = "PhotosDataset";
		this.PhotosSet.Locale = new System.Globalization.CultureInfo("en-US");
		// 
		// sqlSelectCommand2
		// 
		this.sqlSelectCommand2.CommandText = @"SELECT ID, UserID, Name, TypeID, Address, IfShowAddress, NumBedrooms, NumBaths, NumSleeps, MinimumNightlyRentalID, NumTVs, NumVCRs, NumCDPlayers, Description, Amenities, LocalAttractions, Rates, CancellationPolicy, DepositRequired, IfMoreThan7PhotosAllowed, IfApproved, CityID, IfFinished, DateAdded, DateStartViewed, VirtualTour, RatesTable, PricesCurrency, CheckIn, CheckOut, LodgingTax, TaxIncluded, DateAvailable, IfDiscounted, IfLastMinuteCancellations, LastMinuteComments, HomeExchangeCityID1, HomeExchangeCityID2, HomeExchangeCityID3 FROM Properties WHERE (ID = @PropertyID)";
        this.sqlSelectCommand2.Connection = CommonFunctions.GetConnection();
		this.sqlSelectCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PropertyID", System.Data.SqlDbType.Int, 4, "ID"));
		((System.ComponentModel.ISupportInitialize)(this.PropertiesSet)).EndInit();
		((System.ComponentModel.ISupportInitialize)(this.PhotosSet)).EndInit();

	}
	#endregion

	protected void FinishButton_Click(object sender, System.EventArgs e)
	{
		if (!IsValid)
			return;

		//if (CommonFunctions.Connection.State == System.Data.ConnectionState.Closed)
			//CommonFunctions.Connection.Open ();

		//lock (CommonFunctions.Connection)
			PropertiesAdapter.Update (PropertiesSet);

        //get top photo and make thumbnail if not already exist..don't forget to delete thumbnails when delete, names not in db
            DBConnection obj = new DBConnection();
            try
            {
                DataTable dt = VADBCommander.PropertyPhotoTopOne(propertyid.ToString());
                //create thumbnail for top photo or overwrite
                if (dt.Rows.Count > 0)
                {
                    File.Delete("C:\\Inetpub\\wwwroot\\vacations-abroad\\httpdocs\\images\\TH" + dt.Rows[0]["filename"].ToString());

                    //see if file exists
                    if (!File.Exists("C:\\Inetpub\\wwwroot\\vacations-abroad\\httpdocs\\images\\TH" + dt.Rows[0]["filename"].ToString()))
                    {
                        System.IO.FileStream streamFrom = new System.IO.FileStream("C:\\Inetpub\\wwwroot\\vacations-abroad\\httpdocs\\images\\" + dt.Rows[0]["filename"].ToString(), System.IO.FileMode.Open, System.IO.FileAccess.Read);
                        System.IO.FileStream streamTo = new FileStream("C:\\Inetpub\\wwwroot\\vacations-abroad\\httpdocs\\images\\TH" + dt.Rows[0]["filename"].ToString(), FileMode.Create, FileAccess.Write);

                        ResizeImage(streamFrom, streamTo);
                    }
                }
            }
            catch (Exception ex) { lblInfo.Text = ex.Message; }
                finally { obj.CloseConnection(); }
        //get top photo and make thumbnail if not already exist..don't forget to delete thumbnails when delete, names not in db

		if (ifadd)
			if (ifauction)
				Response.Redirect (CommonFunctions.PrepareURL ("MakePayment.aspx?UserID=" + userid.ToString () +
					"&AuctionID=" + auctionid.ToString (), backlinktext));
			else
				Response.Redirect (CommonFunctions.PrepareURL ("PublishProperty.aspx?UserID=" + userid.ToString () +
					"&PropertyID=" + propertyid.ToString (), backlinktext));
		else if (ifauction)
			Response.Redirect (CommonFunctions.PrepareURL ("MyAccount.aspx"));
		else
			Response.Redirect (backlinkurl);
	}
    public void ResizeImage(Stream fromStream, Stream toStream)
    {
        System.Drawing.Image image = System.Drawing.Image.FromStream(fromStream);
        int newWidth = 140;
        int newHeight = 115;
        Bitmap thumbnailBitmap = new Bitmap(newWidth, newHeight);
        Graphics thumbnailGraph = Graphics.FromImage(thumbnailBitmap);

        thumbnailGraph.CompositingQuality = CompositingQuality.HighQuality;
        thumbnailGraph.SmoothingMode = SmoothingMode.HighQuality;
        thumbnailGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;

        Rectangle imageRectangle = new Rectangle(0, 0, newWidth, newHeight);

        thumbnailGraph.DrawImage(image, imageRectangle);
        thumbnailBitmap.Save(toStream, image.RawFormat);

        thumbnailGraph.Dispose();
        thumbnailBitmap.Dispose();

        image.Dispose();
    }
	protected void UploadFromFile_Click (object sender, EventArgs e)
	{
		if (!allowupload)
			Response.Redirect (CommonFunctions.PrepareURL ("InternalError.aspx"));

		AddPhoto (FileUpload.FileContent, FileUpload.FileName);
	}

	protected void UploadFromWeb_Click (object sender, EventArgs e)
	{
		if (!allowupload)
			Response.Redirect (CommonFunctions.PrepareURL ("InternalError.aspx"));

		WebClient webclient = new WebClient ();
		byte[] buffer = webclient.DownloadData (WebLocation.Text);
		MemoryStream photostream = new MemoryStream (buffer, 0, buffer.Length, false, true);

		AddPhoto (photostream, WebLocation.Text);
	}

	protected void AddPhoto (Stream PhotoStream, string FileName)
	{
		string filename;
		string destname;
		string extension;
        string new_filename;
		int i;

		double ratio = 1;

		extension = FileName.Substring (FileName.LastIndexOf (".") + 1);

		if (!Directory.Exists (Request.PhysicalApplicationPath + ConfigurationManager.AppSettings["ImagesSubfolderPath"]))
			Directory.CreateDirectory (Request.PhysicalApplicationPath + ConfigurationManager.AppSettings["ImagesSubfolderPath"]);
		for (i = 0; (i < 10000) && File.Exists (Request.PhysicalApplicationPath +
			ConfigurationManager.AppSettings["ImagesSubfolderPath"] + "property" + propertyid.ToString ("00000000") +
			"photo" + i.ToString ("0000") + "." + extension); i++);
		if (i >= 10000)
		{
			//TODO: add error handling here
			Response.Redirect (backlinkurl);
		}

		destname = "property" + propertyid.ToString ("00000000") + "photo" + i.ToString ("0000") + "." + extension;
		filename = Request.PhysicalApplicationPath + ConfigurationManager.AppSettings["ImagesSubfolderPath"] + destname;
        new_filename = Request.PhysicalApplicationPath + ConfigurationManager.AppSettings["ImagesSubfolderPath"] + destname;

		System.Drawing.Image photo = System.Drawing.Image.FromStream (PhotoStream, true, true);

		if ((photo.Width > 320) && ((double)photo.Width / (double)photo.Height >= 2.5))
		{
			if (photo.Width > 500)
				ratio = 500 / (double)photo.Width;
		}
		else if (Math.Max (photo.Width, photo.Height) > 320)
			ratio = 320 / (double)Math.Max (photo.Width, photo.Height);

		if (ratio != 1)
		{
			Bitmap oThumbNail = new Bitmap ((int)(ratio * photo.Width), (int)(ratio * photo.Height));

			Graphics oGraphic = Graphics.FromImage (oThumbNail);

			oGraphic.CompositingQuality = CompositingQuality.HighQuality;
			oGraphic.SmoothingMode = SmoothingMode.HighQuality;
			oGraphic.InterpolationMode = InterpolationMode.HighQualityBicubic;

			Rectangle oRectangle = new Rectangle (0, 0, (int)(ratio * photo.Width),
				(int)(ratio * photo.Height));

			oGraphic.DrawImage (photo, oRectangle);

			photo = oThumbNail;
		}

		try
		{
			photo.Save (new_filename, ImageFormat.Jpeg);
		}
		catch (Exception ex)
		{
			Response.Write ("Error:"+ex.Message+"<br>"+new_filename);
			return;
		}

		//if (CommonFunctions.Connection.State == ConnectionState.Closed)
			//CommonFunctions.Connection.Open ();

		object maxid;
		int newid;
		int newordernumber;

        using(SqlConnection connection = CommonFunctions.GetConnection()) {
            connection.Open();
            SqlCommand getmaxid = new SqlCommand("SELECT MAX(ID) FROM PropertyPhotos", connection);
            SqlCommand getmaxordernumber = new SqlCommand("SELECT MAX(OrderNumber) FROM PropertyPhotos " +
                "WHERE PropertyID = @PropertyID", connection);
            getmaxordernumber.Parameters.Add("@PropertyID", SqlDbType.Int);
            getmaxordernumber.Parameters["@PropertyID"].Value = propertyid;


            maxid = getmaxid.ExecuteScalar();

		    if (maxid is DBNull)
			    newid = 1;
		    else
			    newid = (int)maxid + 1;

		    maxid = getmaxordernumber.ExecuteScalar ();
            connection.Close();
        }
		if (maxid is DBNull)
			newordernumber = 1;
		else
			newordernumber = (int)maxid + 1;

		DataRow newphoto = PhotosSet.Tables["PropertyPhotos"].NewRow ();

		newphoto["ID"] = newid;
		newphoto["PropertyID"] = propertyid;
		newphoto["FileName"] = destname;
		newphoto["Width"] = photo.Width;
		newphoto["Height"] = photo.Height;
		newphoto["OrderNumber"] = newordernumber;

		PhotosSet.Tables["PropertyPhotos"].Rows.Add (newphoto);

		//lock (CommonFunctions.Connection)
			PhotosAdapter.Update (PhotosSet);

		Response.Redirect (CommonFunctions.PrepareURL ("PropertyPhotos.aspx?" + Request.QueryString.ToString ()));
	}
}
