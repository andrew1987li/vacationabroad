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

public partial class AllProperties : CommonPage
{
	//protected System.Data.SqlClient.SqlConnection Connection;
	protected System.Data.SqlClient.SqlDataAdapter GetCountries;
	protected System.Data.SqlClient.SqlDataAdapter GetStateProvinces;
	protected System.Data.SqlClient.SqlDataAdapter GetRegions;
	protected System.Data.SqlClient.SqlCommand sqlSelectCommand3;
	protected System.Data.SqlClient.SqlCommand sqlSelectCommand2;
	protected System.Data.SqlClient.SqlCommand sqlSelectCommand1;
	protected System.Web.UI.WebControls.HyperLink Hyperlink1;
	protected Vacations.LocationsDataset LocationsSet2;
	protected Vacations.LocationsDataset LocationsSet1;

	protected void Page_Load(object sender, System.EventArgs e)
	{
		//string connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

		//CommonFunctions.Connection.ConnectionString = connectionstring;

        //if (Request.Params["HomeExchanges"] != null)
        //{
        //    GetRegions.SelectCommand.CommandText = GetRegions.SelectCommand.CommandText.Replace ("WHERE (Properties",
        //        "WHERE ((Properties.HomeExchangeCityID1 IS NOT NULL) OR (Properties.HomeExchangeCityID2 IS NOT NULL) OR (Properties.HomeExchangeCityID3 IS NOT NULL)) AND (Properties");
        //    GetCountries.SelectCommand.CommandText = GetCountries.SelectCommand.CommandText.Replace ("WHERE (Properties",
        //        "WHERE ((Properties.HomeExchangeCityID1 IS NOT NULL) OR (Properties.HomeExchangeCityID2 IS NOT NULL) OR (Properties.HomeExchangeCityID3 IS NOT NULL)) AND (Properties");
        //    GetStateProvinces.SelectCommand.CommandText = GetStateProvinces.SelectCommand.CommandText.Replace ("WHERE (Properties",
        //        "WHERE ((Properties.HomeExchangeCityID1 IS NOT NULL) OR (Properties.HomeExchangeCityID2 IS NOT NULL) OR (Properties.HomeExchangeCityID3 IS NOT NULL)) AND (Properties");
        //}

		//lock (CommonFunctions.Connection)
			GetRegions.Fill (LocationsSet1);
		//lock (CommonFunctions.Connection)
			GetCountries.Fill (LocationsSet1);
		//lock (CommonFunctions.Connection)
			GetStateProvinces.Fill (LocationsSet1);

		//lock (CommonFunctions.Connection)
			GetRegions.Fill (LocationsSet2);
		//lock (CommonFunctions.Connection)
			GetCountries.Fill (LocationsSet2);
		//lock (CommonFunctions.Connection)
			GetStateProvinces.Fill (LocationsSet2);

		int count = LocationsSet1.Tables["Regions"].Rows.Count;
		int count1 = count / 2;
		int count2 = count - count1;
		int i;
		
		for (i = count1; i < count; i++)
		{
			foreach (DataRow datarow in new Snapshot (LocationsSet1.Tables["Countries"].Rows))
				if ((datarow.RowState != DataRowState.Deleted) && (!(datarow["RegionID"] is int) ||
					((int)datarow["RegionID"] == (int)LocationsSet1.Tables["Regions"].Rows[i]["ID"])))
				{
					foreach (DataRow datarow2 in new Snapshot (LocationsSet1.Tables["StateProvinces"].Rows))
						if ((datarow2.RowState != DataRowState.Deleted) && (!(datarow2["CountryID"] is int) ||
								((int)datarow2["CountryID"] == (int)datarow["ID"])))
							datarow2.Delete ();
					datarow.Delete ();
				}
			LocationsSet1.Tables["Regions"].Rows[i].Delete ();
		}
		
		for (i = 0; i < count1; i++)
		{
			foreach (DataRow datarow in new Snapshot (LocationsSet2.Tables["Countries"].Rows))
				if ((datarow.RowState != DataRowState.Deleted) && (!(datarow["RegionID"] is int) ||
					((int)datarow["RegionID"] == (int)LocationsSet2.Tables["Regions"].Rows[i]["ID"])))
				{
					foreach (DataRow datarow2 in new Snapshot (LocationsSet2.Tables["StateProvinces"].Rows))
						if ((datarow2.RowState != DataRowState.Deleted) && (!(datarow2["CountryID"] is int) ||
								((int)datarow2["CountryID"] == (int)datarow["ID"])))
							datarow2.Delete ();
					datarow.Delete ();
				}
			LocationsSet2.Tables["Regions"].Rows[i].Delete ();
		}
		
		LocationsSet1.Relations.Add ("RegionsCountries", LocationsSet1.Tables["Regions"].Columns["ID"],
			LocationsSet1.Tables["Countries"].Columns["RegionID"]);
		LocationsSet1.Relations.Add ("CountriesStateProvinces", LocationsSet1.Tables["Countries"].Columns["ID"],
			LocationsSet1.Tables["StateProvinces"].Columns["CountryID"]);

		LocationsSet2.Relations.Add ("RegionsCountries", LocationsSet2.Tables["Regions"].Columns["ID"],
			LocationsSet2.Tables["Countries"].Columns["RegionID"]);
		LocationsSet2.Relations.Add ("CountriesStateProvinces", LocationsSet2.Tables["Countries"].Columns["ID"],
			LocationsSet2.Tables["StateProvinces"].Columns["CountryID"]);

		DataBind ();

		HtmlHead head = Page.Header;

		HtmlMeta description = new HtmlMeta ();

		description.Name = "description";
		description.Content = "Vacation Cottages Chalets Condos Hotels Homes Apartments Villas Lodges Resorts";

		head.Controls.Add (description);

		((System.Web.UI.WebControls.Image)Master.FindControl ("Logo")).AlternateText = "Vacation Cottages Chalets Condos" +
			" Apartments Villas Hotels Homes Lodges Resorts";
		((System.Web.UI.WebControls.Image)Master.FindControl ("MainLogo")).AlternateText = "Vacation Cottages Chalets" +
			" Condos Apartments Villas Hotels Homes Lodges Resorts";
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
		this.GetStateProvinces = new System.Data.SqlClient.SqlDataAdapter();
		this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
		this.GetCountries = new System.Data.SqlClient.SqlDataAdapter();
		this.sqlSelectCommand2 = new System.Data.SqlClient.SqlCommand();
		this.GetRegions = new System.Data.SqlClient.SqlDataAdapter();
		this.sqlSelectCommand3 = new System.Data.SqlClient.SqlCommand();
		this.LocationsSet1 = new Vacations.LocationsDataset();
		this.LocationsSet2 = new Vacations.LocationsDataset();
		((System.ComponentModel.ISupportInitialize)(this.LocationsSet1)).BeginInit();
		((System.ComponentModel.ISupportInitialize)(this.LocationsSet2)).BeginInit();
		// 
		// CommonFunctions.Connection
		// 
		//CommonFunctions.Connection.ConnectionString = "workstation id=MAIN;packet size=4096;integrated security=SSPI;data source=MAIN;pe" +
			//"rsist security info=False;initial catalog=Vacations";
		// 
		// GetStateProvinces
		// 
		this.GetStateProvinces.SelectCommand = this.sqlSelectCommand1;
		this.GetStateProvinces.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																									new System.Data.Common.DataTableMapping("Table", "StateProvinces", new System.Data.Common.DataColumnMapping[] {
																																																					  new System.Data.Common.DataColumnMapping("ID", "ID"),
																																																					  new System.Data.Common.DataColumnMapping("CountryID", "CountryID"),
																																																					  new System.Data.Common.DataColumnMapping("StateProvince", "StateProvince")})});
		// 
		// sqlSelectCommand1
		// 
		this.sqlSelectCommand1.CommandText = @"SELECT DISTINCT StateProvinces.ID, StateProvinces.CountryID, StateProvinces.StateProvince FROM StateProvinces INNER JOIN Cities ON Cities.StateProvinceID = StateProvinces.ID INNER JOIN Properties ON Properties.CityID = Cities.ID WHERE (Properties.IfFinished = 1) AND (Properties.IfApproved = 1) ORDER BY StateProvinces.StateProvince";
		this.sqlSelectCommand1.Connection = CommonFunctions.GetConnection();
		// 
		// GetCountries
		// 
		this.GetCountries.SelectCommand = this.sqlSelectCommand2;
		this.GetCountries.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																							   new System.Data.Common.DataTableMapping("Table", "Countries", new System.Data.Common.DataColumnMapping[] {
																																																			new System.Data.Common.DataColumnMapping("ID", "ID"),
																																																			new System.Data.Common.DataColumnMapping("RegionID", "RegionID"),
																																																			new System.Data.Common.DataColumnMapping("Country", "Country")})});
		// 
		// sqlSelectCommand2
		// 
		this.sqlSelectCommand2.CommandText = @"SELECT DISTINCT Countries.ID, Countries.RegionID, Countries.Country FROM Countries INNER JOIN StateProvinces ON StateProvinces.CountryID = Countries.ID INNER JOIN Cities ON Cities.StateProvinceID = StateProvinces.ID INNER JOIN Properties ON Properties.CityID = Cities.ID WHERE (Properties.IfFinished = 1) AND (Properties.IfApproved = 1) ORDER BY Countries.Country";
		this.sqlSelectCommand2.Connection = CommonFunctions.GetConnection();
		// 
		// GetRegions
		// 
		this.GetRegions.SelectCommand = this.sqlSelectCommand3;
		this.GetRegions.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																							 new System.Data.Common.DataTableMapping("Table", "Regions", new System.Data.Common.DataColumnMapping[] {
																																																		new System.Data.Common.DataColumnMapping("ID", "ID"),
																																																		new System.Data.Common.DataColumnMapping("Region", "Region")})});
		// 
		// sqlSelectCommand3
		// 
		this.sqlSelectCommand3.CommandText = @"SELECT DISTINCT Regions.ID, Regions.Region FROM Regions INNER JOIN Countries ON Regions.ID = Countries.RegionID INNER JOIN StateProvinces ON StateProvinces.CountryID = Countries.ID INNER JOIN Cities ON Cities.StateProvinceID = StateProvinces.ID INNER JOIN Properties ON Properties.CityID = Cities.ID WHERE (Properties.IfFinished = 1) AND (Properties.IfApproved = 1) ORDER BY Regions.Region";
		this.sqlSelectCommand3.Connection = CommonFunctions.GetConnection();
		// 
		// LocationsSet1
		// 
		this.LocationsSet1.DataSetName = "LocationsDataset";
		this.LocationsSet1.Locale = new System.Globalization.CultureInfo("en-US");
		// 
		// LocationsSet2
		// 
		this.LocationsSet2.DataSetName = "LocationsDataset";
		this.LocationsSet2.Locale = new System.Globalization.CultureInfo("en-US");
		((System.ComponentModel.ISupportInitialize)(this.LocationsSet1)).EndInit();
		((System.ComponentModel.ISupportInitialize)(this.LocationsSet2)).EndInit();

	}
	#endregion
}
