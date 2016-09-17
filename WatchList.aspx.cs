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

public partial class WatchList : ClosedPage
{
	//protected SqlConnection Connection = new SqlConnection (ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
	protected SqlDataAdapter UsersAdapter;
	protected SqlDataAdapter AuctionsAdapter;
	protected SqlDataAdapter PropertiesAdapter;
	protected DataSet MainDataSet = new DataSet ();

	protected void Page_Load (object sender, EventArgs e)
    {
		userid = AuthenticationManager.UserID;

		//CommonFunctions.Connection.Open ();

        object fullname = null;
        using(SqlConnection connection = CommonFunctions.GetConnection()) {
            connection.Open();
            SqlCommand GetUserName = new SqlCommand
                ("SELECT ISNULL(FirstName, '') + ' ' + ISNULL(LastName, '') + ' ' + ISNULL(CompanyName, '') AS FullName " +
                "FROM Users WHERE ID = @UserID", connection);
            GetUserName.Parameters.Add("@UserID", SqlDbType.Int);
            GetUserName.Parameters["@UserID"].Value = userid;

            fullname = GetUserName.ExecuteScalar();
            connection.Close();
        }
		if (fullname is string)
			WelcomeLabel.Text = "Welcome " + (string)fullname;

        UsersAdapter = CommonFunctions.PrepareAdapter(CommonFunctions.GetConnection(), "SELECT * FROM Users WHERE (ID = @UserID)",
			SqlDbType.Int);

        AuctionsAdapter = CommonFunctions.PrepareAdapter(CommonFunctions.GetConnection(), "SELECT TOP 30 WatchListItems.ID AS WatchListItemID, Auctions.*," +
			" (SELECT TOP 1 FileName FROM PropertyPhotos WHERE PropertyPhotos.PropertyID = Properties.ID ORDER BY OrderNumber) AS FileName," +
			" (SELECT TOP 1 Width FROM PropertyPhotos WHERE PropertyPhotos.PropertyID = Properties.ID ORDER BY OrderNumber) AS Width," +
			" (SELECT TOP 1 Height FROM PropertyPhotos WHERE PropertyPhotos.PropertyID = Properties.ID ORDER BY OrderNumber) AS Height " +
			"FROM Auctions INNER JOIN Properties ON Auctions.PropertyID = Properties.ID" +
			" INNER JOIN WatchListItems ON Properties.ID = WatchListItems.PropertyID " +
			"WHERE (WatchListItems.UserID = @UserID)", SqlDbType.Int);

        PropertiesAdapter = CommonFunctions.PrepareAdapter(CommonFunctions.GetConnection(), "SELECT TOP 30 WatchListItems.ID AS WatchListItemID, Properties.*," +
			" (SELECT TOP 1 FileName FROM PropertyPhotos WHERE PropertyPhotos.PropertyID = Properties.ID ORDER BY OrderNumber) AS FileName," +
			" (SELECT TOP 1 Width FROM PropertyPhotos WHERE PropertyPhotos.PropertyID = Properties.ID ORDER BY OrderNumber) AS Width," +
			" (SELECT TOP 1 Height FROM PropertyPhotos WHERE PropertyPhotos.PropertyID = Properties.ID ORDER BY OrderNumber) AS Height, " +
			" Regions.Region, Countries.Country, StateProvinces.StateProvince, Cities.City," +
			" PropertyTypes.Name AS Type " +
			"FROM Properties INNER JOIN Cities ON Properties.CityID = Cities.ID" +
			" INNER JOIN StateProvinces ON StateProvinces.ID = Cities.StateProvinceID" +
			" INNER JOIN Countries ON StateProvinces.CountryID = Countries.ID" +
			" INNER JOIN Regions ON Countries.RegionID = Regions.ID" +
			" INNER JOIN WatchListItems ON Properties.ID = WatchListItems.PropertyID" +
			" LEFT OUTER JOIN PropertyTypes ON Properties.TypeID = PropertyTypes.ID " +
			"WHERE (WatchListItems.UserID = @UserID) AND NOT EXISTS" +
			" (SELECT * FROM Auctions WHERE PropertyID = Properties.ID)",
			SqlDbType.Int);

		UsersAdapter.SelectCommand.Parameters["@UserID"].Value = userid;
		AuctionsAdapter.SelectCommand.Parameters["@UserID"].Value = userid;
		PropertiesAdapter.SelectCommand.Parameters["@UserID"].Value = userid;

		//lock (CommonFunctions.Connection)
			if (UsersAdapter.Fill (MainDataSet, "Users") == 0)
				Response.Redirect (CommonFunctions.PrepareURL ("InternalError.aspx"), true);

		//lock (CommonFunctions.Connection)
			AuctionsAdapter.Fill (MainDataSet, "Auctions");
		//lock (CommonFunctions.Connection)
			PropertiesAdapter.Fill (MainDataSet, "Properties");

		if (!IsPostBack)
			DataBind ();
	}

	protected void DeleteItems_Click (object source, EventArgs e)
	{
		SqlDataAdapter WatchListAdapter;

        WatchListAdapter = CommonFunctions.PrepareAdapter(CommonFunctions.GetConnection(), "SELECT * FROM WatchListItems WHERE UserID = @UserID",
			SqlDbType.Int);
		WatchListAdapter.SelectCommand.Parameters["@UserID"].Value = userid;

		//lock (CommonFunctions.Connection)
			WatchListAdapter.Fill (MainDataSet, "WatchListItems");

		foreach (DataRow row in new Snapshot (MainDataSet.Tables["WatchListItems"].Rows))
			if ((Request.Params["Delete" + row["ID"].ToString ()] != null) &&
					(Request.Params["Delete" + row["ID"].ToString ()] == "on"))
				row.Delete ();

		//lock (CommonFunctions.Connection)
			WatchListAdapter.Update (MainDataSet, "WatchListItems");

		MainDataSet.Tables["Auctions"].Clear ();
		MainDataSet.Tables["Properties"].Clear ();

		//lock (CommonFunctions.Connection)
			AuctionsAdapter.Fill (MainDataSet, "Auctions");
		//lock (CommonFunctions.Connection)
			PropertiesAdapter.Fill (MainDataSet, "Properties");

		DataBind ();
	}
}
