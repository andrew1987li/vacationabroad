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

public partial class ViewUser : CommonPage
{
	//protected SqlConnection Connection = new SqlConnection (ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
	protected SqlDataAdapter UsersAdapter;
	protected SqlDataAdapter AuctionsAdapter;
	protected DataSet MainDataSet = new DataSet ();

	public bool ifproperties = false;
	public bool ifreviews = false;

	protected void Page_Load (object sender, EventArgs e)
    {
		//CommonFunctions.Connection.Open ();

        UsersAdapter = CommonFunctions.PrepareAdapter(CommonFunctions.GetConnection(), "SELECT * FROM Users WHERE (ID = @UserID)",
			SqlDbType.Int);

        AuctionsAdapter = CommonFunctions.PrepareAdapter(CommonFunctions.GetConnection(), "SELECT Auctions.*," +
			" (SELECT TOP 1 FileName FROM PropertyPhotos WHERE PropertyPhotos.PropertyID = Properties.ID ORDER BY OrderNumber) AS FileName," +
			" (SELECT TOP 1 Width FROM PropertyPhotos WHERE PropertyPhotos.PropertyID = Properties.ID ORDER BY OrderNumber) AS Width," +
			" (SELECT TOP 1 Height FROM PropertyPhotos WHERE PropertyPhotos.PropertyID = Properties.ID ORDER BY OrderNumber) AS Height " +
			"FROM Auctions INNER JOIN Properties ON Auctions.PropertyID = Properties.ID " +
			"WHERE (UserID = @UserID) AND (AuctionEnd > GETDATE ())" +
			" AND EXISTS (SELECT * FROM Transactions WHERE (AuctionID = Auctions.ID) AND (IfListingFee = 1)" +
			" AND (PaymentAmount >= InvoiceAmount))", SqlDbType.Int);

		UsersAdapter.SelectCommand.Parameters["@UserID"].Value = userid;
		AuctionsAdapter.SelectCommand.Parameters["@UserID"].Value = userid;

		//lock (CommonFunctions.Connection)
			if (UsersAdapter.Fill (MainDataSet, "Users") == 0)
				Response.Redirect (CommonFunctions.PrepareURL ("InternalError.aspx"), true);

		//lock (CommonFunctions.Connection)
			AuctionsAdapter.Fill (MainDataSet, "Auctions");

            object numpropsresult = null;
            using(SqlConnection connection = CommonFunctions.GetConnection()) {
                connection.Open();
                SqlCommand GetNumProperties = new SqlCommand("SELECT COUNT(*) FROM Properties " +
                    "WHERE (Properties.IfFinished = 1) AND (Properties.IfApproved = 1) AND (UserID = @UserID)" +
                    " AND NOT EXISTS (SELECT * FROM Auctions WHERE PropertyID = Properties.ID)", connection);
                GetNumProperties.Parameters.Add("@UserID", SqlDbType.Int);
                GetNumProperties.Parameters["@UserID"].Value = userid;

                numpropsresult = GetNumProperties.ExecuteScalar();
                connection.Close();
            }
		ifproperties = (numpropsresult is int) && ((int)numpropsresult > 0);

        object numreviewsresult = null;
        using(SqlConnection connection = CommonFunctions.GetConnection()) {
            connection.Open();
            SqlCommand GetNumReviews = new SqlCommand("SELECT COUNT(*) FROM Auctions INNER JOIN Properties" +
                " ON Auctions.PropertyID = Properties.ID WHERE (UserID = @UserID) AND (ReviewDate IS NOT NULL)", connection);
            GetNumReviews.Parameters.Add("@UserID", SqlDbType.Int);
            GetNumReviews.Parameters["@UserID"].Value = userid;

            numreviewsresult = GetNumReviews.ExecuteScalar();
            connection.Close();
        }
		ifreviews = (numreviewsresult is int) && ((int)numreviewsresult > 0);

		DataBind ();
	}
}
