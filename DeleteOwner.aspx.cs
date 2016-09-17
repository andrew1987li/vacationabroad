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

namespace Vacations
{
	public partial class DeleteOwner : AdminPage
	{
		//protected System.Data.SqlClient.SqlConnection Connection;
		protected System.Data.SqlClient.SqlDataAdapter PhotosAdapter;
		protected System.Data.SqlClient.SqlCommand sqlSelectCommand2;
		protected Vacations.PropertiesDataset PropertiesSet;
		protected Vacations.PhotosDataset PhotosSet;
		protected System.Data.SqlClient.SqlDataAdapter PropertiesAdapter;
		protected System.Data.SqlClient.SqlDataAdapter UserAdapter;
		protected System.Data.SqlClient.SqlCommand sqlSelectCommand3;
		protected System.Data.SqlClient.SqlCommand sqlSelectCommand1;
		protected Vacations.UserDataset UserSet;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			System.Data.SqlClient.SqlCommandBuilder builder = new System.Data.SqlClient.SqlCommandBuilder (UserAdapter);

			//string connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

			//CommonFunctions.Connection.ConnectionString = connectionstring;

			UserAdapter.SelectCommand.Parameters["@UserID"].Value = userid;

			if (CommonFunctions.SyncFill (UserAdapter, UserSet) > 0)
			{
				PropertiesAdapter.SelectCommand.Parameters["@UserID"].Value = userid;

				//lock (CommonFunctions.Connection)
					PropertiesAdapter.Fill (PropertiesSet);

				foreach (System.Data.DataRow datarow in PropertiesSet.Tables["Properties"].Rows)
				{
					int propertyid = (int)datarow["ID"];

					PhotosAdapter.SelectCommand.Parameters["@PropertyID"].Value = propertyid;

					//lock (CommonFunctions.Connection)
						PhotosAdapter.Fill (PhotosSet);

					foreach (System.Data.DataRow datarow2 in PhotosSet.Tables["PropertyPhotos"].Rows)
						if (System.IO.File.Exists (Request.PhysicalApplicationPath + System.Configuration.ConfigurationManager.AppSettings["ImagesSubfolderPath"] + datarow2["FileName"]))
							System.IO.File.Delete (Request.PhysicalApplicationPath + System.Configuration.ConfigurationManager.AppSettings["ImagesSubfolderPath"] + datarow2["FileName"]);
				}

				UserSet.Tables["Users"].Rows[0].Delete ();

				//lock (CommonFunctions.Connection)
					UserAdapter.Update (UserSet);
			}

			Response.Redirect (backlinkurl);
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
			this.sqlSelectCommand2 = new System.Data.SqlClient.SqlCommand();
			this.PropertiesSet = new Vacations.PropertiesDataset();
			this.PhotosSet = new Vacations.PhotosDataset();
			this.PropertiesAdapter = new System.Data.SqlClient.SqlDataAdapter();
			this.UserAdapter = new System.Data.SqlClient.SqlDataAdapter();
			this.sqlSelectCommand3 = new System.Data.SqlClient.SqlCommand();
			this.UserSet = new Vacations.UserDataset();
			this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
			((System.ComponentModel.ISupportInitialize)(this.PropertiesSet)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.PhotosSet)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.UserSet)).BeginInit();
			// 
			// CommonFunctions.Connection
			// 
			//CommonFunctions.Connection.ConnectionString = "workstation id=MAIN;packet size=4096;integrated security=SSPI;data source=MAIN;pe" +
				//"rsist security info=False;initial catalog=Vacations";
			// 
			// PhotosAdapter
			// 
			this.PhotosAdapter.SelectCommand = this.sqlSelectCommand2;
			this.PhotosAdapter.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																									new System.Data.Common.DataTableMapping("Table", "PropertyPhotos", new System.Data.Common.DataColumnMapping[] {
																																																					  new System.Data.Common.DataColumnMapping("ID", "ID"),
																																																					  new System.Data.Common.DataColumnMapping("PropertyID", "PropertyID"),
																																																					  new System.Data.Common.DataColumnMapping("FileName", "FileName"),
																																																					  new System.Data.Common.DataColumnMapping("Width", "Width"),
																																																					  new System.Data.Common.DataColumnMapping("Height", "Height"),
																																																					  new System.Data.Common.DataColumnMapping("OrderNumber", "OrderNumber")})});
			// 
			// sqlSelectCommand2
			// 
			this.sqlSelectCommand2.CommandText = "SELECT ID, PropertyID, FileName, Width, Height, OrderNumber FROM PropertyPhotos W" +
				"HERE (PropertyID = @PropertyID)";
			this.sqlSelectCommand2.Connection = CommonFunctions.GetConnection();
			this.sqlSelectCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PropertyID", System.Data.SqlDbType.Int, 4, "PropertyID"));
			// 
			// PropertiesSet
			// 
			this.PropertiesSet.DataSetName = "PropertiesDataset";
			this.PropertiesSet.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// PhotosSet
			// 
			this.PhotosSet.DataSetName = "PhotosDataset";
			this.PhotosSet.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// PropertiesAdapter
			// 
			this.PropertiesAdapter.SelectCommand = this.sqlSelectCommand1;
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
			// UserAdapter
			// 
			this.UserAdapter.SelectCommand = this.sqlSelectCommand3;
			this.UserAdapter.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																								  new System.Data.Common.DataTableMapping("Table", "Users", new System.Data.Common.DataColumnMapping[] {
																																																		   new System.Data.Common.DataColumnMapping("ID", "ID"),
																																																		   new System.Data.Common.DataColumnMapping("Username", "Username"),
																																																		   new System.Data.Common.DataColumnMapping("PasswordSalt", "PasswordSalt"),
																																																		   new System.Data.Common.DataColumnMapping("Repeats", "Repeats"),
																																																		   new System.Data.Common.DataColumnMapping("PasswordHash", "PasswordHash"),
																																																		   new System.Data.Common.DataColumnMapping("Email", "Email"),
																																																		   new System.Data.Common.DataColumnMapping("FirstName", "FirstName"),
																																																		   new System.Data.Common.DataColumnMapping("LastName", "LastName"),
																																																		   new System.Data.Common.DataColumnMapping("Address", "Address"),
																																																		   new System.Data.Common.DataColumnMapping("City", "City"),
																																																		   new System.Data.Common.DataColumnMapping("State", "State"),
																																																		   new System.Data.Common.DataColumnMapping("Zip", "Zip"),
																																																		   new System.Data.Common.DataColumnMapping("Country", "Country"),
																																																		   new System.Data.Common.DataColumnMapping("PrimaryTelephone", "PrimaryTelephone"),
																																																		   new System.Data.Common.DataColumnMapping("EveningTelephone", "EveningTelephone"),
																																																		   new System.Data.Common.DataColumnMapping("DaytimeTelephone", "DaytimeTelephone"),
																																																		   new System.Data.Common.DataColumnMapping("MobileTelephone", "MobileTelephone"),
																																																		   new System.Data.Common.DataColumnMapping("Website", "Website"),
																																																		   new System.Data.Common.DataColumnMapping("IfAdmin", "IfAdmin"),
																																																		   new System.Data.Common.DataColumnMapping("CompanyName", "CompanyName"),
																																																		   new System.Data.Common.DataColumnMapping("Registered", "Registered"),
																																																		   new System.Data.Common.DataColumnMapping("IfPayTravelAgents", "IfPayTravelAgents")})});
			// 
			// sqlSelectCommand3
			// 
			this.sqlSelectCommand3.CommandText = @"SELECT ID, Username, PasswordSalt, Repeats, PasswordHash, Email, FirstName, LastName, Address, City, State, Zip, Country, PrimaryTelephone, EveningTelephone, DaytimeTelephone, MobileTelephone, Website, IfAdmin, CompanyName, Registered, IfPayTravelAgents FROM Users WHERE (ID = @UserID)";
			this.sqlSelectCommand3.Connection = CommonFunctions.GetConnection();
			this.sqlSelectCommand3.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserID", System.Data.SqlDbType.Int, 4, "ID"));
			// 
			// UserSet
			// 
			this.UserSet.DataSetName = "UserDataset";
			this.UserSet.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// sqlSelectCommand1
			// 
			this.sqlSelectCommand1.CommandText = @"SELECT ID, UserID, Name, TypeID, Address, IfShowAddress, NumBedrooms, NumBaths, NumSleeps, MinimumNightlyRentalID, NumTVs, NumVCRs, NumCDPlayers, Description, Amenities, LocalAttractions, Rates, CancellationPolicy, DepositRequired, IfMoreThan7PhotosAllowed, IfApproved, CityID, IfFinished, DateAdded, DateStartViewed, VirtualTour, RatesTable, PricesCurrency, CheckIn, CheckOut, LodgingTax, TaxIncluded, DateAvailable, IfDiscounted, IfLastMinuteCancellations, LastMinuteComments, HomeExchangeCityID1, HomeExchangeCityID2, HomeExchangeCityID3 FROM Properties WHERE (UserID = @UserID)";
			this.sqlSelectCommand1.Connection = CommonFunctions.GetConnection();
			this.sqlSelectCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserID", System.Data.SqlDbType.Int, 4, "UserID"));
			((System.ComponentModel.ISupportInitialize)(this.PropertiesSet)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.PhotosSet)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.UserSet)).EndInit();

		}
		#endregion
	}
}
