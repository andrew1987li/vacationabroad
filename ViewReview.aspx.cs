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

public partial class ViewReview : CommonPage
{
	//protected SqlConnection Connection = new SqlConnection (ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
	protected SqlDataAdapter AuctionsAdapter;
	protected DataSet MainDataSet = new DataSet ();

	protected void Page_Load (object sender, EventArgs e)
    {
		if (auctionid == -1)
			Response.Redirect (CommonFunctions.PrepareURL ("InternalError.aspx"), true);

		//CommonFunctions.Connection.Open ();

        AuctionsAdapter = CommonFunctions.PrepareAdapter(CommonFunctions.GetConnection(), "SELECT *," +
			" (SELECT UserID FROM Properties WHERE Properties.ID = Auctions.PropertyID) AS UserID," +
			" (SELECT ISNULL(Users.UserID, Users.Username) FROM Users INNER JOIN Properties ON Users.ID = Properties.UserID" +
			" WHERE Properties.ID = Auctions.PropertyID) AS Username " +
			"FROM Auctions WHERE (ID = @AuctionID) AND (AuctionEnd <= GETDATE ()) AND (HighestBidderID IS NOT NULL)",
			SqlDbType.Int);

		AuctionsAdapter.SelectCommand.Parameters["@AuctionID"].Value = auctionid;

		//lock (CommonFunctions.Connection)
			AuctionsAdapter.Fill (MainDataSet, "Auctions");
/*
		if (((int)MainDataSet.Tables["Auctions"].Rows[0]["HighestBidderID"] != AuthenticationManager.UserID) &&
			((int)MainDataSet.Tables["Auctions"].Rows[0]["UserID"] != AuthenticationManager.UserID) &&
				!AuthenticationManager.IfAdmin)
			Response.Redirect (CommonFunctions.PrepareURL ("Login.aspx"), true);
*/
		DataBind ();
	}

	protected string DrawAsterisks (object Number)
	{
		if ((Number is int) && ((int)Number > 0))
			return new string ('*', (int)Number);
		else
			return "---";
	}
}
