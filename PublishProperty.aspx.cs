using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Net;

public partial class PublishProperty : ClosedPage
{
	//protected System.Data.SqlClient.SqlConnection Connection;
	protected System.Data.SqlClient.SqlDataAdapter PropertiesAdapter;
	protected Vacations.PropertiesFullDataset PropertiesSet;
	protected System.Data.SqlClient.SqlCommand sqlSelectCommand1;

	protected void Page_Load(object sender, System.EventArgs e)
	{
		bool ifok;

		System.Data.SqlClient.SqlCommandBuilder builder = new System.Data.SqlClient.SqlCommandBuilder (PropertiesAdapter);

		//string connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

		//CommonFunctions.Connection.ConnectionString = connectionstring;

		ifok = false;
		if (propertyid != -1)
		{
			PropertiesAdapter.SelectCommand.Parameters["@PropertyID"].Value = propertyid;

			if (CommonFunctions.SyncFill (PropertiesAdapter, PropertiesSet) > 0)
			{
				int uid = (int)PropertiesSet.Tables["Properties"].Rows[0]["UserID"];

				if ((!(PropertiesSet.Tables["Properties"].Rows[0]["IfFinished"] is bool) ||
					!(bool)PropertiesSet.Tables["Properties"].Rows[0]["IfFinished"]) &&
					((uid == AuthenticationManager.UserID) || AuthenticationManager.IfAdmin))
				{
                    if (!IsPostBack)
                    {
                        DataBind();

                        //get top photo and make thumbnail if not already exist..don't forget to delete thumbnails when delete, names not in db
                        DBConnection obj = new DBConnection();
                        try
                        {
                            DataTable dt = VADBCommander.PropertyPhotoTopOne(propertyid.ToString());
                            //create thumbnail for top photo or overwrite
                            if (dt.Rows.Count > 0)
                            {
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
                    }

					ifok = true;
				}
			}
		}

		if (!ifok)
			Response.Redirect (CommonFunctions.PrepareURL ("InternalError.aspx"));
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
		this.PropertiesSet = new Vacations.PropertiesFullDataset();
		this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
		((System.ComponentModel.ISupportInitialize)(this.PropertiesSet)).BeginInit();
		// 
		// CommonFunctions.Connection
		// 
		//CommonFunctions.Connection.ConnectionString = "workstation id=MAIN;packet size=4096;integrated security=SSPI;data source=MAIN;pe" +
			//"rsist security info=False;initial catalog=Vacations";
		// 
		// PropertiesAdapter
		// 
		this.PropertiesAdapter.SelectCommand = this.sqlSelectCommand1;
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
																																																				  new System.Data.Common.DataColumnMapping("HomeExchangeCityID1", "HomeExchangeCityID1"),
																																																				  new System.Data.Common.DataColumnMapping("HomeExchangeCityID2", "HomeExchangeCityID2"),
																																																				  new System.Data.Common.DataColumnMapping("HomeExchangeCityID3", "HomeExchangeCityID3")})});
		// 
		// PropertiesSet
		// 
		this.PropertiesSet.DataSetName = "PropertiesFullDataset";
		this.PropertiesSet.Locale = new System.Globalization.CultureInfo("en-US");
		// 
		// sqlSelectCommand1
		// 
		this.sqlSelectCommand1.CommandText = "SELECT MinimumNightlyRentalTypes.Name AS MinimumNightlyRental, PropertyTypes.Name" +
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
			"ns, Properties.HomeExchangeCityID1, Properties.HomeExchangeCityID2, Properties.H" +
			"omeExchangeCityID3 FROM Properties INNER JOIN Cities ON Properties.CityID = Citi" +
			"es.ID INNER JOIN StateProvinces ON StateProvinces.ID = Cities.StateProvinceID IN" +
			"NER JOIN Countries ON StateProvinces.CountryID = Countries.ID INNER JOIN Regions" +
			" ON Countries.RegionID = Regions.ID INNER JOIN Users ON Properties.UserID = User" +
			"s.ID LEFT OUTER JOIN MinimumNightlyRentalTypes ON Properties.MinimumNightlyRenta" +
			"lID = MinimumNightlyRentalTypes.ID LEFT OUTER JOIN PropertyTypes ON Properties.T" +
			"ypeID = PropertyTypes.ID WHERE (Properties.ID = @PropertyID)";
        this.sqlSelectCommand1.Connection = CommonFunctions.GetConnection();
		this.sqlSelectCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PropertyID", System.Data.SqlDbType.Int, 4, "ID"));
		((System.ComponentModel.ISupportInitialize)(this.PropertiesSet)).EndInit();

	}
	#endregion

	protected void PublishButton_Click(object sender, System.EventArgs e)
	{
		if (!IsValid)
			return;

        using(SqlConnection connection = CommonFunctions.GetConnection()) {
            connection.Open();

            SqlCommand updatecommand = new SqlCommand("UPDATE Properties SET IfFinished = 1 WHERE ID = @PropertyID", connection);
            updatecommand.Parameters.Add("@PropertyID", SqlDbType.Int, 4, "PropertyID");

            //if (CommonFunctions.Connection.State == System.Data.ConnectionState.Closed)
            //CommonFunctions.Connection.Open ();

            updatecommand.Parameters["@PropertyID"].Value = propertyid;
            updatecommand.ExecuteNonQuery();

            connection.Close();
        }     

		Response.Redirect (CommonFunctions.PrepareURL ("ThankYou.aspx?PropertyID=" + propertyid));
	}

	protected void CancelButton_Click(object sender, System.EventArgs e)
	{
		Response.Redirect (backlinkurl);
	}
}
