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

public partial class UserReviews : CommonPage
{
	protected string username;

	//protected SqlConnection Connection = new SqlConnection (ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
	protected SqlDataAdapter AuctionsAdapter;
	protected DataSet MainDataSet = new DataSet ();

	protected void Page_Load (object sender, EventArgs e)
    {
		if (userid == -1)
			Response.Redirect (CommonFunctions.PrepareURL ("InternalError.aspx"), true);

		//CommonFunctions.Connection.Open ();

        using(SqlConnection connection = CommonFunctions.GetConnection()) {
            connection.Open();
            username = CommonFunctions.GetUsername(connection, userid);
            connection.Close();
        }

        AuctionsAdapter = CommonFunctions.PrepareAdapter(CommonFunctions.GetConnection(), "SELECT Auctions.* " +
			"FROM Auctions INNER JOIN Properties ON Auctions.PropertyID = Properties.ID " +
			"WHERE (Properties.UserID = @UserID) AND (ReviewDate IS NOT NULL) AND (AuctionEnd <= GETDATE ())" +
			" AND (HighestBidderID IS NOT NULL)", SqlDbType.Int);

		AuctionsAdapter.SelectCommand.Parameters["@UserID"].Value = userid;

		//lock (CommonFunctions.Connection)
			AuctionsAdapter.Fill (MainDataSet, "Reviews");

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
