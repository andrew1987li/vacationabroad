using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;

public partial class RegionList : CommonPage
{
	public string region;

	private int regionid;

	protected System.Data.SqlClient.SqlDataAdapter GetStateProvinces;
	protected System.Data.SqlClient.SqlDataAdapter GetCountries;
	protected System.Data.SqlClient.SqlCommand sqlSelectCommand2;
	protected Vacations.CountriesStates CountriesStates;
	protected System.Data.SqlClient.SqlCommand sqlSelectCommand1;
	//protected System.Data.SqlClient.SqlConnection Connection;

	protected void Page_Load(object sender, System.EventArgs e)
	{
		//string connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

		//CommonFunctions.Connection.ConnectionString = connectionstring;

		if (Request.Params["RegionID"] != null)
		{
			regionid = Convert.ToInt32 (Request.Params["RegionID"]);

            hdnRC.Value = regionid.ToString();

            switch (regionid)
            {
                case 8:
                    conNorthAmerica.Visible = true;
                    break;
                case 9:
                    conSouthAmerica.Visible = true;
                    break;
                case 6:
                    conEurope.Visible = true;
                    break;
                case 2:
                    conAsia.Visible = true;
                    break;
                case 1:
                    conAfrica.Visible = true;
                    break;
                case 3:
                    conOceania.Visible = true;
                    break;
            }

            object result = null;
            using(SqlConnection connection = CommonFunctions.GetConnection()) {
                connection.Open();

                SqlCommand getregion = new SqlCommand("SELECT Region FROM Regions WHERE ID=@RegionID",
                    connection);
                getregion.Parameters.Add("@RegionID", System.Data.SqlDbType.Int, 4);
                getregion.Parameters["@RegionID"].Value = regionid;

                //if (CommonFunctions.Connection.State == ConnectionState.Closed)
                //CommonFunctions.Connection.Open ();

                result = getregion.ExecuteScalar();
                connection.Close();
            }

			if (result is string)
				region = (string)result;
			else
			{
				Response.Redirect (CommonFunctions.PrepareURL ("default.aspx"));
				return;
			}
		}
		else
		{
			Response.Redirect (CommonFunctions.PrepareURL ("default.aspx"));
			return;
		}

		GetCountries.SelectCommand.Parameters["@RegionID"].Value = regionid;
		GetStateProvinces.SelectCommand.Parameters["@RegionID"].Value = regionid;

		//lock (CommonFunctions.Connection)
			GetCountries.Fill (CountriesStates);
		//lock (CommonFunctions.Connection)
			GetStateProvinces.Fill (CountriesStates);
	
		CountriesStates.Relations.Add ("CountriesStates", CountriesStates.Tables["Countries"].Columns["ID"],
			CountriesStates.Tables["StateProvinces"].Columns["CountryID"]);

		HtmlHead head = Page.Header;

		HtmlMeta keywords = new HtmlMeta ();

		keywords.Name = "keywords";
		keywords.Content = region + " vacation rentals " + region + " Villas " + region + " Homes " + region + " Houses " + region + " B&B " + region + " Cabins " + region + " Condos";
		head.Controls.Add (keywords);

		HtmlMeta description = new HtmlMeta ();
   
		description.Name = "description";
        description.Content = " Consider staying in a " + region + " vacation rental or a " + region + " holiday rental. Check our directory of " + region + " accommodations and book a " + region + " vacation homes, holiday homes, vacation villas, holiday villas, vacation apartments, holiday apartments, vacation condos, holiday condos, vacation cabins, holiday cabins, vacation cottages, holiday cottages, vacation resorts, holiday resorts.";
		head.Controls.Add (description);
        Page.Header.Controls.Add(new LiteralControl("<link href='/css/StyleSheetBig4.css' rel='stylesheet' type='text/css'></script>"));

		DataBind ();

        initPageLoad();
	}

    public void initPageLoad()
    {
        //LogInLink.NavigateUrl = CommonFunctions.PrepareURL("Login.aspx");
        //CreateAccountLink.NavigateUrl = CommonFunctions.PrepareURL("FindOwner.aspx");
        //LogOutLink.NavigateUrl = CommonFunctions.PrepareURL("Logout.aspx");
        //UserIDLink.NavigateUrl = CommonFunctions.PrepareURL("Listings.aspx");
        //OwnersLinkLink.NavigateUrl = CommonFunctions.PrepareURL("OwnersList.aspx");
        //OutStandingInvoicesLink.NavigateUrl = CommonFunctions.PrepareURL("OutstandingInvoices.aspx");
        //AdminLink.NavigateUrl = CommonFunctions.PrepareURL("Administration.aspx");

        //Logo.ImageUrl = CommonFunctions.PrepareURL("images/logo.jpg");
        //MainLogo.ImageUrl = CommonFunctions.PrepareURL("images/main.jpg");

        DBConnection obj = new DBConnection();
        DataTable dtCountryList = new DataTable();

        DataTable dtStateList = new DataTable();

        StringBuilder sbNorthAmerica = new StringBuilder();
        //----------------------------North America
        //north amer

        StringBuilder sb = new StringBuilder();
        sb.AppendLine("<ul class=\"TripleListMain\">");
        sb.AppendLine("<li><a class=\"mainPgCountry\" href=\"/canada/default.aspx\"><b>Canada</b></a></li>");
        sb.AppendLine("<li><a class=\"mainPgCountry\" href=\"/mexico/default.aspx\"><b>Mexico</b></a></li>");
        sb.AppendLine("<li><a class=\"mainPgCountry\" href=\"/usa/default.aspx\"><b>USA</b></a></li>");
        sb.AppendLine("<li><a class=\"mainPgCountry\" href=\"/bahamas/default.aspx\"><b>Bahamas</b></a></li>");
        sb.AppendLine("<li><a class=\"mainPgCountry\" href=\"/barbados/default.aspx\"><b>Barbados</b></a></li>");
        sb.AppendLine("<li><a class=\"mainPgCountry\" href=\"/belize/default.aspx\"><b>Belize</b></a></li>");
        sb.AppendLine("<li><a class=\"mainPgCountry\" href=\"/costa_rica/default.aspx\"><b>Costa Rica</b></a></li>");
        sb.AppendLine("<li><a class=\"mainPgCountry\" href=\"/dominica/default.aspx\"><b>Dominica</b></a></li>");
        sb.AppendLine("<li><a class=\"mainPgCountry\" href=\"/dominican_republic/default.aspx\"><b>Dominican Republic</b></a></li>");
        sb.AppendLine("<li><a class=\"mainPgCountry\" href=\"/french_caribbean/default.aspx\"><b>French Caribbean</b></a></li>");
        sb.AppendLine("<li><a class=\"mainPgCountry\" href=\"/guatemala/default.aspx\"><b>Guatemala</b></a></li>");
        sb.AppendLine("<li><a class=\"mainPgCountry\" href=\"/jamaica/default.aspx\"><b>Jamaica</b></a></li>");
        sb.AppendLine("<li><a class=\"mainPgCountry\" href=\"/netherlands_caribbean/default.aspx\"><b>Netherlands Caribbean</b></a></li>");
        sb.AppendLine("<li><a class=\"mainPgCountry\" href=\"/nicaragua/default.aspx\"><b>Nicaragua</b></a></li>");
        sb.AppendLine("<li><a class=\"mainPgCountry\" href=\"/panama/default.aspx\"><b>Panama</b></a></li>");
        sb.AppendLine("<li><a class=\"mainPgCountry\" href=\"/saint_kitts_and_nevis/default.aspx\"><b>Saint Kitts and Nevis</b></a></li>");
        sb.AppendLine("<li><a class=\"mainPgCountry\" href=\"/st_lucia/default.aspx\"><b>St Lucia</b></a></li>");
        sb.AppendLine("<li><a class=\"mainPgCountry\" href=\"/the_grenadines/default.aspx\"><b>The Grenadines</b></a></li>");
        sb.AppendLine("<li><a class=\"mainPgCountry\" href=\"/trinidad_and_tobago/default.aspx\"><b>Trinidad and Tobago</b></a></li>");
        sb.AppendLine("<li><a class=\"mainPgCountry\" href=\"/uk_caribbean/default.aspx\"><b>UK Caribbean</b></a></li>");
        sb.AppendLine("<li><a class=\"mainPgCountry\" href=\"/us_caribbean/default.aspx\"><b>US Caribbean</b></a></li>");
        sb.AppendLine("</ul>");


        divnAmerica.InnerHtml = sb.ToString();// GenerateCountryLinks("8");
        divSouthAmerica.InnerHtml = GenerateCountryLinks("9");
        divAfrica.InnerHtml = GenerateCountryLinks("1");
        divAsia.InnerHtml = GenerateCountryLinks("2");
        divEurope.InnerHtml = GenerateCountryLinks("6");
        divOceania.InnerHtml = GenerateCountryLinks("3");

        //        ID	Region
        //1	Africa
        //2	Asia
        //3	Oceania
        //4	Caribbean
        //5	Central America
        //6	Europe
        //7	Middle East
    }

    public string GenerateCountryLinks(string regionID)
    {
        DataTable dtCountryList = new DataTable();
        DataTable dtStateList = new DataTable();
        StringBuilder sb = new StringBuilder();
        //----------------------------North America
        //north amer
        dtCountryList = VADBCommander.CountiesByRegionList(regionID);
        //dtCountryList = VADBCommander.CountriesByRegionList(regionID);
        if (dtCountryList.Rows.Count > 0)
        {
            sb.AppendLine("<ul class=\"TripleListMain\">");
            foreach (DataRow row in dtCountryList.Rows)
            {
                //we list the country first
                sb.AppendLine("<li><a class=\"mainPgCountry\" href=\"" + CommonFunctions.PrepareURL(row["country"].ToString().ToLower().Replace(" ", "_") + "/default.aspx") + "\"><b>" + row["country"].ToString() + "</b></a></li>");
                bool stateDown = false;

                //we grab all of the states for that country
                dtStateList = VADBCommander.StateProvinceByCountryList(row["id"].ToString());

                if (dtStateList.Rows.Count > 0)
                {

                    /*
                    sbNorthAmerica.AppendLine("<ul class=\"links fltlft\">");
                    foreach (DataRow rowTemp in dtStateList.Rows)
                    {
                        DataTable dtCityList = VADBCommander.CityStateCountryRegionList(rowTemp["id"].ToString());
                        //add cities to lower
                        if (dtCityList.Rows.Count > 0)
                        {
                            //sbNorthAmerica.AppendLine("<li><a class=\"mainState\" href=\"" + CommonFunctions.PrepareURL(row["country"].ToString().ToLower().Replace(" ", "_").Replace(" ", "_").ToLower() + "/" + rowTemp["stateprovince"].ToString().Replace(" ", "_").ToLower() + "/default.aspx") + "\">" + rowTemp["stateprovince"].ToString().Replace(" ", "&nbsp;").Replace("-", "&#8209;") + "</a>");
                            
                            //sbNorthAmerica.AppendLine(":  ");
                            //foreach (DataRow row1 in dtCityList.Rows)
                            //{
                            //    sbNorthAmerica.AppendLine("<a class=\"mainCity\" href=\"" + CommonFunctions.PrepareURL(row["country"].ToString().ToLower().Replace(" ", "_") + "/" + rowTemp["stateProvince"].ToString().Replace(" ", "_").ToLower() + "/" + row1["city"].ToString().ToLower().Replace(" ", "_") + "/default.aspx") + "\" class=\"mainCity\">" + row1["city"].ToString().Replace(" ", "&nbsp;").Replace("-", "&#8209;") + "</a>, ");
                            //} 
                            sbNorthAmerica.AppendLine("</li>");
                        }
                        else
                        {
                            sbNorthAmerica.AppendLine("<li><a class=\"mainState\" href=\"" + CommonFunctions.PrepareURL(row["country"].ToString().ToLower().Replace(" ", "_").Replace(" ", "_").ToLower() + "/" + rowTemp["stateprovince"].ToString().Replace(" ", "_").ToLower() + "/default.aspx") + "\">" + rowTemp["stateprovince"].ToString().Replace(" ", "&nbsp;").Replace("-", "&#8209;") + "</a></li>");
                        }
                    }
                    sbNorthAmerica.AppendLine("</ul>");
                      */
                }
            }
            sb.AppendLine("</ul>");
        }
        return sb.ToString();

    }

	public string GetTitle ()
	{
        //return region + " Vacation Rentals " + region + " Vacation Apartments " + region +
        //    " Holiday Apartments ";

        return  region +  " Vacation Rentals, " + region + " Holiday Rentals, " + region + " Vacation Apartments, and Holiday Villas";
	;
	}

	public string TableTitle ()
	{
		return  region +  " vacation rentals, holiday apartments and villas" ;
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
		this.GetCountries = new System.Data.SqlClient.SqlDataAdapter();
		this.sqlSelectCommand2 = new System.Data.SqlClient.SqlCommand();
		this.CountriesStates = new Vacations.CountriesStates();
		this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
		((System.ComponentModel.ISupportInitialize)(this.CountriesStates)).BeginInit();
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
																																																					  new System.Data.Common.DataColumnMapping("StateProvince", "StateProvince"),
																																																					  new System.Data.Common.DataColumnMapping("Country", "Country")})});
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
		//this.sqlSelectCommand2.CommandText = @"SELECT ID, RegionID, Country FROM Countries WHERE (RegionID = @RegionID) AND EXISTS (SELECT * FROM (Properties INNER JOIN Cities ON Properties.CityID = Cities.ID) INNER JOIN StateProvinces ON Cities.StateProvinceID = StateProvinces.ID WHERE (StateProvinces.CountryID = Countries.ID) AND (Properties.IfFinished = 1) AND (Properties.IfApproved = 1) AND NOT EXISTS (SELECT * FROM Auctions WHERE PropertyID = Properties.ID)) ORDER BY Country";
        this.sqlSelectCommand2.CommandText = @"SELECT ID, RegionID, Country FROM Countries WHERE (RegionID = @RegionID) AND EXISTS (SELECT * FROM (Properties INNER JOIN Cities ON Properties.CityID = Cities.ID) INNER JOIN StateProvinces ON Cities.StateProvinceID = StateProvinces.ID WHERE (StateProvinces.CountryID = Countries.ID) AND (Properties.IfFinished = 1) AND (Properties.IfApproved = 1) ) ORDER BY Country";
        this.sqlSelectCommand2.Connection = CommonFunctions.GetConnection();
		this.sqlSelectCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RegionID", System.Data.SqlDbType.Int, 4, "RegionID"));
		// 
		// CountriesStates
		// 
		this.CountriesStates.DataSetName = "CountriesStates";
		this.CountriesStates.Locale = new System.Globalization.CultureInfo("en-US");
		// 
		// sqlSelectCommand1
		// 
		//this.sqlSelectCommand1.CommandText = @"SELECT StateProvinces.ID, StateProvinces.CountryID, StateProvinces.StateProvince, Countries.Country FROM StateProvinces INNER JOIN Countries ON StateProvinces.CountryID = Countries.ID WHERE (Countries.RegionID = @RegionID) AND EXISTS (SELECT * FROM Properties INNER JOIN Cities ON Properties.CityID = Cities.ID WHERE (Cities.StateProvinceID = StateProvinces.ID) AND (Properties.IfFinished = 1) AND (Properties.IfApproved = 1) AND NOT EXISTS (SELECT * FROM Auctions WHERE PropertyID = Properties.ID)) ORDER BY StateProvinces.StateProvince";
        this.sqlSelectCommand1.CommandText = @"SELECT StateProvinces.ID, StateProvinces.CountryID, StateProvinces.StateProvince, Countries.Country FROM StateProvinces INNER JOIN Countries ON StateProvinces.CountryID = Countries.ID WHERE (Countries.RegionID = @RegionID) AND EXISTS (SELECT * FROM Properties INNER JOIN Cities ON Properties.CityID = Cities.ID WHERE (Cities.StateProvinceID = StateProvinces.ID) AND (Properties.IfFinished = 1) AND (Properties.IfApproved = 1)) ORDER BY StateProvinces.StateProvince";
        this.sqlSelectCommand1.Connection = CommonFunctions.GetConnection();
		this.sqlSelectCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RegionID", System.Data.SqlDbType.Int, 4, "RegionID"));
		((System.ComponentModel.ISupportInitialize)(this.CountriesStates)).EndInit();

	}
	#endregion
}
