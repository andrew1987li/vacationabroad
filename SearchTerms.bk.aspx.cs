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

public partial class SearchTerms : CommonPage
{
	protected int sorder = 1;
	protected int startpos = 0;
	protected int numitems = 0;
	public string terms = "";
	protected int ifonly = 0;

	//protected SqlConnection Connection = new SqlConnection (ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
	protected SqlDataAdapter AuctionsAdapter;
	protected SqlDataAdapter PropertiesAdapter;
	protected DataSet MainDataSet = new DataSet ();



	protected void Page_Load (object sender, EventArgs e)
    {
		if ((Request.Params["SortOrder"] != null) && (Request.Params["SortOrder"].Length > 0))
			try
			{
				sorder = Convert.ToInt32 (Request.Params["SortOrder"]);
			}
			catch (Exception)
			{
			}

		if (IsPostBack)
			try
			{
				sorder = Convert.ToInt32 (SortOrder.SelectedValue);
			}
			catch (Exception)
			{
			}
		else
			SortOrder.SelectedValue = sorder.ToString ();

		if ((Request.Params["StartPos"] != null) && (Request.Params["StartPos"].Length > 0))
			try
			{
				startpos = Convert.ToInt32 (Request.Params["StartPos"]);
			}
			catch (Exception)
			{
			}

		if ((Request.Params["Only"] != null) && (Request.Params["Only"].Length > 0))
			try
			{
				ifonly = Convert.ToInt32 (Request.Params["Only"]);
			}
			catch (Exception)
			{
			}

		if (IsPostBack)
			if (OnlyAuction.Checked)
				ifonly = 1;
			else if (OnlyNonAuction.Checked)
				ifonly = 2;
			else
				ifonly = 3;
		else
		{
			Both.Checked = (ifonly == 0);
			OnlyAuction.Checked = (ifonly == 1);
			OnlyNonAuction.Checked = (ifonly == 2);
		}

		if ((Request.Params["SearchTerms"] != null) && (Request.Params["SearchTerms"].Length > 0))
			terms = Request.Params["SearchTerms"];

        if (IsPostBack)
        {
            terms = RefineTerms.Text;
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "key", "alert(" + terms + ")", true);
            //ClientScript.RegisterClientScriptBlock(this.GetType(),"Key","alert("+terms+")",true);
        }
        else
        {
            RefineTerms.Text = terms;
        }
		string auctionscriteria = "";
		string propertiescriteria = "";
		string[] words = terms.Split (' ');
		foreach (string word in words)
		{
			auctionscriteria += " AND ((Auctions.Title LIKE '%" + word + "%') OR (Properties.Name2 LIKE '%" + word +
				"%') OR (Properties.Description LIKE '%" + word + "%') OR (Properties.LocalAttractions LIKE '%" + word + "%') OR (Properties.Rates LIKE '%" + word +
				"%') OR (Cities.City LIKE '%" + word + "%') OR (StateProvinces.StateProvince LIKE '%" + word +
				"%') OR EXISTS (SELECT * FROM PropertiesAmenities INNER JOIN Amenities" +
				" ON PropertiesAmenities.AmenityID = Amenities.ID WHERE (Amenities.Amenity LIKE '%" + word +
				"%') AND (PropertiesAmenities.PropertyID = Properties.ID)))";
            propertiescriteria += " AND IfFinished = 1 AND ((Cities.City LIKE '%" + word +
                "%') OR (Properties.Name2 LIKE '%" + word + "%') OR (Countries.Country LIKE '%" + word + "%')  OR (StateProvinces.StateProvince LIKE '%" + word + "%'))";// OR EXISTS (SELECT * " +
				//"FROM PropertiesAmenities INNER JOIN Amenities ON PropertiesAmenities.AmenityID = Amenities.ID " +
				//"WHERE (Amenities.Amenity LIKE '%" + word + "%') AND (PropertiesAmenities.PropertyID = Properties.ID)))";
		}

		string orderby = "";
		switch (sorder)
		{
			case 1:
				orderby = "AuctionEnd";
				break;
			case 2:
				orderby = "AuctionEnd DESC";
				break;
			case 3:
				orderby = "BidAmount";
				break;
			case 4:
				orderby = "BidAmount DESC";
				break;
			default:
				Response.Redirect (CommonFunctions.PrepareURL ("InternalError.aspx"), true);
				break;
		}

		//CommonFunctions.Connection.Open ();

        AuctionsAdapter = CommonFunctions.PrepareAdapter(CommonFunctions.GetConnection(), "SELECT TOP " + (startpos + 25).ToString() +
			" Auctions.*, Countries.Country, StateProvinces.StateProvince, Cities.City, " +
			" (SELECT TOP 1 FileName FROM PropertyPhotos INNER JOIN Properties ON" +
			" PropertyPhotos.PropertyID = Properties.ID WHERE Properties.ID = Auctions.PropertyID ORDER BY OrderNumber) AS FileName," +
			" (SELECT TOP 1 Width FROM PropertyPhotos INNER JOIN Properties ON" +
			" PropertyPhotos.PropertyID = Properties.ID WHERE Properties.ID = Auctions.PropertyID ORDER BY OrderNumber) AS Width," +
			" (SELECT TOP 1 Height FROM PropertyPhotos INNER JOIN Properties ON" +
			" PropertyPhotos.PropertyID = Properties.ID WHERE Properties.ID = Auctions.PropertyID ORDER BY OrderNumber) AS Height " +
			"FROM Auctions INNER JOIN Properties ON Auctions.PropertyID = Properties.ID" +
			" INNER JOIN Cities ON Properties.CityID = Cities.ID" +
			" INNER JOIN StateProvinces ON StateProvinces.ID = Cities.StateProvinceID" +
			" INNER JOIN Countries ON StateProvinces.CountryID = Countries.ID" +
			" INNER JOIN Regions ON Countries.RegionID = Regions.ID " +
			"WHERE (GETDATE() < AuctionEnd) AND EXISTS (SELECT * FROM Transactions WHERE (AuctionID = Auctions.ID)" +
			" AND (IfListingFee = 1) AND (PaymentAmount >= InvoiceAmount))" + auctionscriteria + " ORDER BY " + orderby);

		//lock (CommonFunctions.Connection)
			if (ifonly != 2)
				AuctionsAdapter.Fill (MainDataSet, "Auctions");
			else
				AuctionsAdapter.FillSchema (MainDataSet, SchemaType.Source, "Auctions");

            PropertiesAdapter = CommonFunctions.PrepareAdapter(CommonFunctions.GetConnection(), "SELECT Properties.*," +
			" (SELECT TOP 1 FileName FROM PropertyPhotos WHERE PropertyPhotos.PropertyID = Properties.ID ORDER BY OrderNumber) AS FileName," +
			" (SELECT TOP 1 Width FROM PropertyPhotos WHERE PropertyPhotos.PropertyID = Properties.ID ORDER BY OrderNumber) AS Width," +
			" (SELECT TOP 1 Height FROM PropertyPhotos WHERE PropertyPhotos.PropertyID = Properties.ID ORDER BY OrderNumber) AS Height, " +
			" Regions.Region, Countries.Country, StateProvinces.StateProvince, Cities.City," +
			" PropertyTypes.Name AS Type, CASE WHEN EXISTS (SELECT * FROM PropertiesAmenities INNER JOIN Amenities" +
			" ON PropertiesAmenities.AmenityID = Amenities.ID WHERE (PropertiesAmenities.PropertyID = Properties.ID)" +
			" AND ((Amenities.Amenity = 'Private Swimming Pool') OR (Amenities.Amenity = 'Shared Swimming Pool')))" +
			" THEN 'Yes' ELSE 'No' END AS Pool " +
			"FROM Properties INNER JOIN Cities ON Properties.CityID = Cities.ID" +
			" INNER JOIN StateProvinces ON StateProvinces.ID = Cities.StateProvinceID" +
			" INNER JOIN Countries ON StateProvinces.CountryID = Countries.ID" +
			" INNER JOIN Regions ON Countries.RegionID = Regions.ID" +                        
			" LEFT OUTER JOIN PropertyTypes ON Properties.TypeID = PropertyTypes.ID " +
            " INNER JOIN PropertyCategories ON PropertyCategories.ID = PropertyTypes.Category " +
			"WHERE NOT EXISTS (SELECT * FROM Auctions WHERE PropertyID = Properties.ID)" + propertiescriteria,
			SqlDbType.Int);

            lbl1.Text = PropertiesAdapter.SelectCommand.CommandText;
		//lock (CommonFunctions.Connection)
			if ((ifonly != 1) && (MainDataSet.Tables["Auctions"].Rows.Count < startpos + 25))
				PropertiesAdapter.Fill (MainDataSet, "Properties");
			else
				PropertiesAdapter.FillSchema (MainDataSet, SchemaType.Source, "Properties");

		int numdeleted = 0;
		foreach (DataRow datarow in new Snapshot (MainDataSet.Tables["Auctions"].Rows))
			if (numdeleted < startpos)
			{
				MainDataSet.Tables["Auctions"].Rows.Remove (datarow);
				numdeleted++;
			}

		if (MainDataSet.Tables["Auctions"].Rows.Count == 0)
		{
			int numdeleted2 = 0;
			foreach (DataRow datarow in new Snapshot (MainDataSet.Tables["Properties"].Rows))
				if (numdeleted2 < startpos - numdeleted)
				{
					MainDataSet.Tables["Properties"].Rows.Remove (datarow);
					numdeleted++;
				}
		}

        using(SqlConnection connection = CommonFunctions.GetConnection()) {
            connection.Open();
            SqlCommand getnumauctions = new System.Data.SqlClient.SqlCommand("SELECT COUNT(*) " +
                "FROM Auctions INNER JOIN Properties ON Auctions.PropertyID = Properties.ID" +
                " INNER JOIN Cities ON Properties.CityID = Cities.ID" +
                " INNER JOIN StateProvinces ON StateProvinces.ID = Cities.StateProvinceID" +
                " INNER JOIN Countries ON StateProvinces.CountryID = Countries.ID" +
                " INNER JOIN Regions ON Countries.RegionID = Regions.ID " +
                "WHERE (GETDATE() < AuctionEnd) AND EXISTS (SELECT * FROM Transactions WHERE (AuctionID = Auctions.ID)" +
                " AND (IfListingFee = 1) AND (PaymentAmount >= InvoiceAmount))" + auctionscriteria, connection);

            object numauctionsresult = getnumauctions.ExecuteScalar();

            SqlCommand getnumproperties = new System.Data.SqlClient.SqlCommand("SELECT COUNT(*) " +
                "FROM Properties INNER JOIN Cities ON Properties.CityID = Cities.ID" +
                " INNER JOIN StateProvinces ON StateProvinces.ID = Cities.StateProvinceID" +
                " INNER JOIN Countries ON StateProvinces.CountryID = Countries.ID" +
                " INNER JOIN Regions ON Countries.RegionID = Regions.ID" +
                " LEFT OUTER JOIN PropertyTypes ON Properties.TypeID = PropertyTypes.ID " +
                "WHERE NOT EXISTS (SELECT * FROM Auctions WHERE PropertyID = Properties.ID)" +
                " AND EXISTS (SELECT * FROM Invoices WHERE (PropertyID = Properties.ID) AND (PaymentAmount >= InvoiceAmount)" +
                " AND (GETDATE() <= Invoices.RenewalDate))" + propertiescriteria, connection);

            object numpropertiesresult = getnumproperties.ExecuteScalar();

            if((numauctionsresult is int) && (numpropertiesresult is int)) {
                if(ifonly != 1)
                    numitems += (int)numpropertiesresult;
                if(ifonly != 2)
                    numitems += (int)numauctionsresult;
            }
           // Page.Header.Controls.Add(new LiteralControl("<link href='/css/StyleSheetBig4.css' rel='stylesheet' type='text/css'></script>"));

            DataBind();
            connection.Close();
        }
	}
}
