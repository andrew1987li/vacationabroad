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

public partial class UserProperties : CommonPage
{
	protected string username;

	//protected SqlConnection Connection = new SqlConnection (ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
	protected SqlDataAdapter PropertiesAdapter;
	protected SqlDataAdapter AmenitiesAdapter;
	protected DataSet MainDataSet = new DataSet ();
	
	protected void Page_Load (object sender, EventArgs e)
    {
		if (userid == -1)
			Response.Redirect (CommonFunctions.PrepareURL ("InternalError.aspx"), true);

		//CommonFunctions.Connection.Open ();

        username = CommonFunctions.GetUsername(CommonFunctions.GetConnection(), userid);

        PropertiesAdapter = CommonFunctions.PrepareAdapter(CommonFunctions.GetConnection(), "SELECT Properties.*," +
			" CASE WHEN EXISTS (SELECT * FROM PropertiesAmenities INNER JOIN Amenities ON" +
			" PropertiesAmenities.AmenityID = Amenities.ID WHERE (PropertiesAmenities.PropertyID = Properties.ID)" +
			" AND ((Amenities.Amenity = 'Private Swimming Pool') OR (Amenities.Amenity = 'Shared Swimming Pool')))" +
			" THEN 'Yes' ELSE 'No' END AS Pool, Regions.Region, Countries.Country, StateProvinces.StateProvince," +
			" Cities.City, MinimumNightlyRentalTypes.Name as MinimumNightlyRental, PropertyTypes.Name AS Type " +
			"FROM Properties INNER JOIN Cities ON Properties.CityID = Cities.ID" +
			" INNER JOIN StateProvinces ON StateProvinces.ID = Cities.StateProvinceID" +
			" INNER JOIN Countries ON StateProvinces.CountryID = Countries.ID" +
			" INNER JOIN Regions ON Countries.RegionID = Regions.ID" +
			" LEFT OUTER JOIN MinimumNightlyRentalTypes ON Properties.MinimumNightlyRentalID = MinimumNightlyRentalTypes.ID" +
			" LEFT OUTER JOIN PropertyTypes ON Properties.TypeID = PropertyTypes.ID " +
			"WHERE (Properties.IfFinished = 1) AND (Properties.IfApproved = 1) AND (UserID = @UserID)" +
			" AND NOT EXISTS (SELECT * FROM Auctions WHERE PropertyID = Properties.ID)",
			SqlDbType.Int);

        AmenitiesAdapter = CommonFunctions.PrepareAdapter(CommonFunctions.GetConnection(), "SELECT Amenities.ID, Amenity," +
			" PropertiesAmenities.PropertyID " +
			"FROM Amenities INNER JOIN PropertiesAmenities ON Amenities.ID = PropertiesAmenities.AmenityID" +
			" INNER JOIN Properties ON PropertiesAmenities.PropertyID = Properties.ID WHERE (UserID = @UserID)" +
			" AND NOT EXISTS (SELECT * FROM Auctions WHERE PropertyID = Properties.ID)",
			SqlDbType.Int);

		PropertiesAdapter.SelectCommand.Parameters["@UserID"].Value = userid;
		AmenitiesAdapter.SelectCommand.Parameters["@UserID"].Value = userid;

		//lock (CommonFunctions.Connection)
			PropertiesAdapter.Fill (MainDataSet, "Properties");
		//lock (CommonFunctions.Connection)
			AmenitiesAdapter.Fill (MainDataSet, "Amenities");

		MainDataSet.Relations.Add ("PropertiesAmenities", MainDataSet.Tables["Properties"].Columns["ID"],
			MainDataSet.Tables["Amenities"].Columns["PropertyID"]);

		DataBind ();
	}
}
