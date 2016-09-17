using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

namespace Vacations
{
	public partial class UploadPhoto : ClosedPage
	{
		protected System.Data.SqlClient.SqlDataAdapter PhotosAdapter;
		protected Vacations.PhotosDataset PhotosSet;
		protected System.Data.SqlClient.SqlDataAdapter PropertiesAdapter;
		protected Vacations.PropertiesDataset PropertiesSet;
		protected System.Data.SqlClient.SqlCommand sqlSelectCommand1;
		protected System.Data.SqlClient.SqlCommand sqlSelectCommand2;
		//protected System.Data.SqlClient.SqlConnection Connection;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			System.Data.SqlClient.SqlCommandBuilder builder = new System.Data.SqlClient.SqlCommandBuilder (PhotosAdapter);

			if (propertyid != -1)
			{
				//string connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

				//CommonFunctions.Connection.ConnectionString = connectionstring;

				PropertiesAdapter.SelectCommand.Parameters["@PropertyID"].Value = propertyid;

				if (CommonFunctions.SyncFill (PropertiesAdapter, PropertiesSet) > 0)
				{
					int uid = (int)PropertiesSet.Tables["Properties"].Rows[0]["UserID"];

					if ((uid == AuthenticationManager.UserID) || AuthenticationManager.IfAdmin)
					{
						PhotosAdapter.SelectCommand.Parameters["@PropertyID"].Value = propertyid;

						//lock (CommonFunctions.Connection)
							PhotosAdapter.Fill (PhotosSet);

						if ((bool)PropertiesSet.Tables["Properties"].Rows[0]["IfMoreThan7PhotosAllowed"] ||
							(PhotosSet.Tables["PropertyPhotos"].Rows.Count < 7))
						{
							string filename;
							string destname;
							string extension;
							int i;

							System.IO.Stream photostream = null;
							string oldfilename = "";

							if (Request.Files.Count > 0)
							{
								photostream = Request.Files[0].InputStream;
								oldfilename = Request.Files[0].FileName;
							}
							else if (Request.Params["PhotoLocation"] != null)
							{
								System.Net.WebClient webclient = new System.Net.WebClient ();
								byte[] buffer = webclient.DownloadData (Request.Params["PhotoLocation"]);
								photostream = new System.IO.MemoryStream (buffer, 0, buffer.Length, false, true);
								oldfilename = Request.Params["PhotoLocation"];
							}
							else
								Response.Redirect (backlinkurl);

							System.Drawing.Image photo = System.Drawing.Image.FromStream (photostream, true, true);
							if ((photo.Width > 320) && ((double)photo.Width / (double)photo.Height >= 2.5))
							{
								if (photo.Width > 500)
								{
									double ratio = 500 / (double)photo.Width;

									System.Drawing.Bitmap oThumbNail = new System.Drawing.Bitmap ((int)(ratio * photo.Width),
										(int)(ratio * photo.Height));

									Graphics oGraphic =  Graphics.FromImage (oThumbNail);

									oGraphic.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality ;

									oGraphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality ;

									oGraphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic ;

									Rectangle oRectangle = new Rectangle (0, 0, (int)(ratio * photo.Width),
										(int)(ratio * photo.Height));

									oGraphic.DrawImage (photo, oRectangle);

									photo = oThumbNail;
								}
							}
							else if (Math.Max (photo.Width, photo.Height) > 320)
							{
								double ratio = 320 / (double)Math.Max (photo.Width, photo.Height);

								System.Drawing.Bitmap oThumbNail = new System.Drawing.Bitmap ((int)(ratio * photo.Width),
									(int)(ratio * photo.Height));

								Graphics oGraphic =  Graphics.FromImage (oThumbNail);

								oGraphic.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality ;

								oGraphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality ;

								oGraphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic ;

								Rectangle oRectangle = new Rectangle (0, 0, (int)(ratio * photo.Width),
									(int)(ratio * photo.Height));

								oGraphic.DrawImage (photo, oRectangle);

								photo = oThumbNail;
							}

							extension = oldfilename.Substring (oldfilename.LastIndexOf (".") + 1);

							if (!System.IO.Directory.Exists (Request.PhysicalApplicationPath +
									System.Configuration.ConfigurationManager.AppSettings["ImagesSubfolderPath"]))
								System.IO.Directory.CreateDirectory (Request.PhysicalApplicationPath +
									System.Configuration.ConfigurationManager.AppSettings["ImagesSubfolderPath"]);
							for (i = 0; (i < 10000) &&
								System.IO.File.Exists (Request.PhysicalApplicationPath +
									System.Configuration.ConfigurationManager.AppSettings["ImagesSubfolderPath"] + "property" +
									propertyid.ToString ("00000000") + "photo" + i.ToString ("0000") + "." + extension); i++);
							if (i > 10000)
							{
								//TODO: add error handling here
								Response.Redirect (backlinkurl);
							}

							destname = "property" + propertyid.ToString ("00000000") + "photo" + i.ToString ("0000") + "." + extension;
							filename = Request.PhysicalApplicationPath +
								System.Configuration.ConfigurationManager.AppSettings["ImagesSubfolderPath"] + destname;

							photo.Save (filename, System.Drawing.Imaging.ImageFormat.Jpeg);

							//if (CommonFunctions.Connection.State == System.Data.ConnectionState.Closed)
								//CommonFunctions.Connection.Open ();

                            
							object maxid;
							int newid;
							int newordernumber;

                            using(SqlConnection connection = CommonFunctions.GetConnection()) {
                                connection.Open();
                                System.Data.SqlClient.SqlCommand getmaxid =
                                    new System.Data.SqlClient.SqlCommand("SELECT MAX(ID) FROM PropertyPhotos", connection);
                                System.Data.SqlClient.SqlCommand getmaxordernumber =
                                    new System.Data.SqlClient.SqlCommand("SELECT MAX(OrderNumber) FROM PropertyPhotos", connection);


                                maxid = getmaxid.ExecuteScalar();
                                connection.Close();

							    if (maxid is DBNull)
								    newid = 1;
							    else
								    newid = (int)maxid + 1;

							    maxid = getmaxordernumber.ExecuteScalar ();
                            }
							if (maxid is DBNull)
								newordernumber = 1;
							else
								newordernumber = (int)maxid + 1;

							System.Data.DataRow newphoto = PhotosSet.Tables["PropertyPhotos"].NewRow ();

							newphoto["ID"] = newid;
							newphoto["PropertyID"] = propertyid;
							newphoto["FileName"] = destname;
							newphoto["Width"] = photo.Width;
							newphoto["Height"] = photo.Height;
							newphoto["OrderNumber"] = newordernumber;

							PhotosSet.Tables["PropertyPhotos"].Rows.Add (newphoto);

							//lock (CommonFunctions.Connection)
								PhotosAdapter.Update (PhotosSet);
						}
					}
				}
			}

			Response.Redirect (backlinkurl);
		}

		public bool DummyCallback ()
		{
			return false;
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
			this.PhotosAdapter = new System.Data.SqlClient.SqlDataAdapter();
			this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
			this.PhotosSet = new Vacations.PhotosDataset();
			this.PropertiesAdapter = new System.Data.SqlClient.SqlDataAdapter();
			this.PropertiesSet = new Vacations.PropertiesDataset();
			this.sqlSelectCommand2 = new System.Data.SqlClient.SqlCommand();
			((System.ComponentModel.ISupportInitialize)(this.PhotosSet)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.PropertiesSet)).BeginInit();
			// 
			// CommonFunctions.Connection
			// 
			//CommonFunctions.Connection.ConnectionString = "workstation id=MAIN;packet size=4096;integrated security=SSPI;data source=MAIN;pe" +
				//"rsist security info=False;initial catalog=Vacations";
			// 
			// PhotosAdapter
			// 
			this.PhotosAdapter.SelectCommand = this.sqlSelectCommand1;
			this.PhotosAdapter.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																									new System.Data.Common.DataTableMapping("Table", "PropertyPhotos", new System.Data.Common.DataColumnMapping[] {
																																																					  new System.Data.Common.DataColumnMapping("ID", "ID"),
																																																					  new System.Data.Common.DataColumnMapping("PropertyID", "PropertyID"),
																																																					  new System.Data.Common.DataColumnMapping("FileName", "FileName"),
																																																					  new System.Data.Common.DataColumnMapping("Width", "Width"),
																																																					  new System.Data.Common.DataColumnMapping("Height", "Height"),
																																																					  new System.Data.Common.DataColumnMapping("OrderNumber", "OrderNumber")})});
			// 
			// sqlSelectCommand1
			// 
			this.sqlSelectCommand1.CommandText = "SELECT ID, PropertyID, FileName, Width, Height, OrderNumber FROM PropertyPhotos W" +
				"HERE (PropertyID = @PropertyID)";
            this.sqlSelectCommand1.Connection = CommonFunctions.GetConnection();
			this.sqlSelectCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PropertyID", System.Data.SqlDbType.Int, 4, "PropertyID"));
			// 
			// PhotosSet
			// 
			this.PhotosSet.DataSetName = "PhotosDataset";
			this.PhotosSet.Locale = new System.Globalization.CultureInfo("en-US");
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
			// sqlSelectCommand2
			// 
			this.sqlSelectCommand2.CommandText = @"SELECT ID, UserID, Name, TypeID, Address, IfShowAddress, NumBedrooms, NumBaths, NumSleeps, MinimumNightlyRentalID, NumTVs, NumVCRs, NumCDPlayers, Description, Amenities, LocalAttractions, Rates, CancellationPolicy, DepositRequired, IfMoreThan7PhotosAllowed, IfApproved, CityID, IfFinished, DateAdded, DateStartViewed, VirtualTour, RatesTable, PricesCurrency, CheckIn, CheckOut, LodgingTax, TaxIncluded, DateAvailable, IfDiscounted, IfLastMinuteCancellations, LastMinuteComments, HomeExchangeCityID1, HomeExchangeCityID2, HomeExchangeCityID3 FROM Properties WHERE (ID = @PropertyID)";
            this.sqlSelectCommand2.Connection = CommonFunctions.GetConnection();
			this.sqlSelectCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PropertyID", System.Data.SqlDbType.Int, 4, "ID"));
			((System.ComponentModel.ISupportInitialize)(this.PhotosSet)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.PropertiesSet)).EndInit();

		}
		#endregion
	}
}
