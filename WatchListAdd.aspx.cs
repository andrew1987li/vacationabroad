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

public partial class WatchListAdd : ClosedPage
{
	//protected SqlConnection Connection = new SqlConnection (ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
	protected SqlDataAdapter MainAdapter;
	protected DataSet MainDataSet = new DataSet ();

	protected void Page_Load (object sender, EventArgs e)
    {
		if ((auctionid == -1) && (propertyid == -1))
			Response.Redirect (CommonFunctions.PrepareURL ("InternalError.aspx"), true);

		//CommonFunctions.Connection.Open ();

		if (propertyid == -1)
		{
            object propertyidresult = null;
            using(SqlConnection connection = CommonFunctions.GetConnection()) {
                connection.Open();
                SqlCommand GetPropertyID = new SqlCommand("SELECT PropertyID FROM Auctions WHERE (ID = @AuctionID)" +
                    " AND EXISTS (SELECT * FROM Transactions WHERE (AuctionID = Auctions.ID) AND (IfListingFee = 1)" +
                    " AND (PaymentAmount >= InvoiceAmount))", connection);
                GetPropertyID.Parameters.Add("@AuctionID", SqlDbType.Int);
                GetPropertyID.Parameters["@AuctionID"].Value = auctionid;

                propertyidresult = GetPropertyID.ExecuteScalar();
                connection.Close();
            }

			if (!(propertyidresult is int))
				Response.Redirect (CommonFunctions.PrepareURL ("InternalError.aspx"), true);

			propertyid = (int)propertyidresult;
		}


        MainAdapter = CommonFunctions.PrepareAdapter(CommonFunctions.GetConnection(), "SELECT * FROM WatchListItems " +
			"WHERE (UserID = @UserID) AND (PropertyID = @PropertyID)", SqlDbType.Int, SqlDbType.Int);

		MainAdapter.SelectCommand.Parameters["@UserID"].Value = AuthenticationManager.UserID;
		MainAdapter.SelectCommand.Parameters["@PropertyID"].Value = propertyid;

		if (CommonFunctions.SyncFill (MainAdapter, MainDataSet, "WatchListItems") == 0)
		{
			DataRow newrow = MainDataSet.Tables["WatchListItems"].NewRow ();

			newrow["UserID"] = AuthenticationManager.UserID;
			newrow["PropertyID"] = propertyid;

			MainDataSet.Tables["WatchListItems"].Rows.Add (newrow);

			//lock (CommonFunctions.Connection)
				MainAdapter.Update (MainDataSet, "WatchListItems");

			Session["Message"] = "AddedToWatchList";
			Response.Redirect (CommonFunctions.PrepareURL ("MyAccount.aspx?AuctionID=" + auctionid.ToString ()));
		}
		else
		{
			Session["Message"] = "AlreadyOnWatchList";
			Response.Redirect (CommonFunctions.PrepareURL ("MyAccount.aspx?AuctionID=" + auctionid.ToString ()));
		}
	}
}
