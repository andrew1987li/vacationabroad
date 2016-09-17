using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Web.SessionState;

public partial class DropDownMenu : System.Web.UI.UserControl
{
    protected Vacations.RegionsDataset RegionsSet;
    protected Vacations.StateProvincesDataset StateProvincesSet;
    protected Vacations.AmenitiesDataset AmenitiesSet;
    protected System.Data.SqlClient.SqlDataAdapter CountriesAdapter;
    protected System.Data.SqlClient.SqlDataAdapter AmenitiesAdapter;
    protected System.Data.SqlClient.SqlDataAdapter RegionsAdapter;
    protected System.Data.SqlClient.SqlDataAdapter AttractionsAdapter;
    protected Vacations.CitiesDataset CitiesSet;
    protected Vacations.CountriesDataset CountriesSet;
    protected System.Data.SqlClient.SqlDataAdapter StateProvincesAdapter;
    protected Vacations.AttractionsDataset AttractionsSet;
    protected System.Data.SqlClient.SqlDataAdapter CitiesAdapter;
    protected System.Data.SqlClient.SqlCommand sqlInsertCommand1;
    protected System.Data.SqlClient.SqlCommand sqlSelectCommand7;
    protected System.Web.UI.WebControls.Label Label2;
    protected System.Web.UI.WebControls.Label Label3;
    protected System.Web.UI.WebControls.Label Label4;
    protected System.Data.SqlClient.SqlCommand sqlSelectCommand1;
    protected System.Data.SqlClient.SqlCommand sqlInsertCommand4;
    protected System.Data.SqlClient.SqlCommand sqlSelectCommand3;
    protected System.Data.SqlClient.SqlCommand sqlInsertCommand5;
    protected System.Web.UI.WebControls.RequiredFieldValidator CityRequired;
    protected System.Data.SqlClient.SqlCommand sqlSelectCommand2;
    protected System.Data.SqlClient.SqlCommand sqlInsertCommand3;
    protected System.Data.SqlClient.SqlCommand sqlSelectCommand5;
    protected System.Data.SqlClient.SqlCommand sqlInsertCommand6;
    protected System.Web.UI.WebControls.HyperLink Hyperlink1;
    protected Vacations.PropertiesFullDataset PropertiesFullSet;
    protected System.Data.SqlClient.SqlDataAdapter PropertiesAdapter;
    protected Vacations.CountriesDataset CountriesSet2;
    protected System.Data.SqlClient.SqlCommand sqlSelectCommand4;
    protected System.Data.SqlClient.SqlCommand sqlSelectCommand6;

    private string regions = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        RegionsAdapter.Fill(RegionsSet);
        CountriesAdapter.Fill(CountriesSet);
        StateProvincesAdapter.Fill(StateProvincesSet);
        CitiesAdapter.Fill(CitiesSet);

        divInclude.InnerHtml = "<script type=\"text/javascript\" language=\"javascript\">";
        divInclude.InnerHtml += DropDownScript();
        divInclude.InnerHtml += "</script>";
    }

    public string DropDownScript()
    {
        StringBuilder script = new StringBuilder();

        script.Append("var numregions = " + RegionsSet.Tables["Regions"].Rows.Count.ToString() + ";\n");
        script.Append("var regionids = new Array (");
        foreach (DataRow datarow in RegionsSet.Tables["Regions"].Rows)
        {
            script.Append(datarow["ID"].ToString());
            script.Append(", ");
        }
        if (RegionsSet.Tables["Regions"].Rows.Count > 0)
            script.Remove(script.Length - 2, 2);
        script.Append(");\n");
        script.Append("var regionstrs = new Array (");
        foreach (DataRow datarow in RegionsSet.Tables["Regions"].Rows)
        {
            script.Append("\"");
            script.Append(datarow["Region"].ToString());
            script.Append("\", ");
        }
        if (RegionsSet.Tables["Regions"].Rows.Count > 0)
            script.Remove(script.Length - 2, 2);
        script.Append(");\n");

        script.Append("var numcountries = " + CountriesSet.Tables["Countries"].Rows.Count.ToString() + ";\n");
        script.Append("var countryids = new Array (");
        foreach (DataRow datarow in CountriesSet.Tables["Countries"].Rows)
        {
            script.Append(datarow["ID"].ToString());
            script.Append(", ");
        }
        if (CountriesSet.Tables["Countries"].Rows.Count > 0)
            script.Remove(script.Length - 2, 2);
        script.Append(");\n");
        script.Append("var countryregions = new Array (");
        foreach (DataRow datarow in CountriesSet.Tables["Countries"].Rows)
        {
            if (datarow["RegionID"] is DBNull)
                script.Append("0");
            else
                script.Append(datarow["RegionID"].ToString());
            script.Append(", ");
        }
        if (CountriesSet.Tables["Countries"].Rows.Count > 0)
            script.Remove(script.Length - 2, 2);
        script.Append(");\n");
        script.Append("var countrystrs = new Array (");
        foreach (DataRow datarow in CountriesSet.Tables["Countries"].Rows)
        {
            script.Append("\"");
            script.Append(datarow["Country"].ToString());
            script.Append("\", ");
        }
        if (CountriesSet.Tables["Countries"].Rows.Count > 0)
            script.Remove(script.Length - 2, 2);
        script.Append(");\n");

        script.Append("var numprovinces = " + StateProvincesSet.Tables["StateProvinces"].Rows.Count.ToString() + ";\n");
        script.Append("var provinceids = new Array (");
        foreach (DataRow datarow in StateProvincesSet.Tables["StateProvinces"].Rows)
        {
            script.Append(datarow["ID"].ToString());
            script.Append(", ");
        }
        if (StateProvincesSet.Tables["StateProvinces"].Rows.Count > 0)
            script.Remove(script.Length - 2, 2);
        script.Append(");\n");
        script.Append("var provincecountries = new Array (");
        foreach (DataRow datarow in StateProvincesSet.Tables["StateProvinces"].Rows)
        {
            if (datarow["CountryID"] is DBNull)
                script.Append("0");
            else
                script.Append(datarow["CountryID"].ToString());
            script.Append(", ");
        }
        if (StateProvincesSet.Tables["StateProvinces"].Rows.Count > 0)
            script.Remove(script.Length - 2, 2);
        script.Append(");\n");
        script.Append("var provincestrs = new Array (");
        foreach (DataRow datarow in StateProvincesSet.Tables["StateProvinces"].Rows)
        {
            script.Append("\"");
            script.Append(datarow["StateProvince"].ToString());
            script.Append("\", ");
        }
        if (StateProvincesSet.Tables["StateProvinces"].Rows.Count > 0)
            script.Remove(script.Length - 2, 2);
        script.Append(");\n");

        script.Append("var numcities = " + CitiesSet.Tables["Cities"].Rows.Count.ToString() + ";\n");
        script.Append("var cityids = new Array (");
        foreach (DataRow datarow in CitiesSet.Tables["Cities"].Rows)
        {
            script.Append(datarow["ID"].ToString());
            script.Append(", ");
        }
        if (CitiesSet.Tables["Cities"].Rows.Count > 0)
            script.Remove(script.Length - 2, 2);
        script.Append(");\n");
        script.Append("var cityprovinces = new Array (");
        foreach (DataRow datarow in CitiesSet.Tables["Cities"].Rows)
        {
            if (datarow["StateProvinceID"] is DBNull)
                script.Append("0");
            else
                script.Append(datarow["StateProvinceID"].ToString());
            script.Append(", ");
        }
        if (CitiesSet.Tables["Cities"].Rows.Count > 0)
            script.Remove(script.Length - 2, 2);
        script.Append(");\n");
        script.Append("var citystrs = new Array (");
        foreach (DataRow datarow in CitiesSet.Tables["Cities"].Rows)
        {
            script.Append("\"");
            script.Append(datarow["City"].ToString());
            script.Append("\", ");
        }
        if (CitiesSet.Tables["Cities"].Rows.Count > 0)
            script.Remove(script.Length - 2, 2);
        script.Append(");\n");

        return script.ToString();

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
        this.RegionsSet = new Vacations.RegionsDataset();
        this.StateProvincesSet = new Vacations.StateProvincesDataset();
        this.CountriesAdapter = new System.Data.SqlClient.SqlDataAdapter();
        this.sqlSelectCommand4 = new System.Data.SqlClient.SqlCommand();
        this.sqlInsertCommand5 = new System.Data.SqlClient.SqlCommand();
        this.sqlSelectCommand3 = new System.Data.SqlClient.SqlCommand();
        this.RegionsAdapter = new System.Data.SqlClient.SqlDataAdapter();
        this.sqlInsertCommand6 = new System.Data.SqlClient.SqlCommand();
        this.sqlSelectCommand5 = new System.Data.SqlClient.SqlCommand();
        this.sqlInsertCommand4 = new System.Data.SqlClient.SqlCommand();
        this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
        this.CitiesSet = new Vacations.CitiesDataset();
        this.CountriesSet = new Vacations.CountriesDataset();
        this.StateProvincesAdapter = new System.Data.SqlClient.SqlDataAdapter();
        this.sqlInsertCommand3 = new System.Data.SqlClient.SqlCommand();
        this.sqlSelectCommand2 = new System.Data.SqlClient.SqlCommand();
        this.CitiesAdapter = new System.Data.SqlClient.SqlDataAdapter();
        this.sqlInsertCommand1 = new System.Data.SqlClient.SqlCommand();
        this.sqlSelectCommand7 = new System.Data.SqlClient.SqlCommand();
        this.PropertiesFullSet = new Vacations.PropertiesFullDataset();
        this.PropertiesAdapter = new System.Data.SqlClient.SqlDataAdapter();
        this.sqlSelectCommand6 = new System.Data.SqlClient.SqlCommand();
        this.CountriesSet2 = new Vacations.CountriesDataset();
        ((System.ComponentModel.ISupportInitialize)(this.RegionsSet)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.StateProvincesSet)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.CitiesSet)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.CountriesSet)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.PropertiesFullSet)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.CountriesSet2)).BeginInit();

        this.RegionsSet.DataSetName = "RegionsDataset";
        this.RegionsSet.Locale = new System.Globalization.CultureInfo("en-US");
        // 
        // StateProvincesSet
        // 
        this.StateProvincesSet.DataSetName = "StateProvincesDataset";
        this.StateProvincesSet.Locale = new System.Globalization.CultureInfo("en-US");

        // CountriesAdapter
        // 
        this.CountriesAdapter.SelectCommand = this.sqlSelectCommand4;
        this.CountriesAdapter.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																								   new System.Data.Common.DataTableMapping("Table", "Countries", new System.Data.Common.DataColumnMapping[] {
																																																				new System.Data.Common.DataColumnMapping("ID", "ID"),
																																																				new System.Data.Common.DataColumnMapping("RegionID", "RegionID"),
																																																				new System.Data.Common.DataColumnMapping("Country", "Country")})});
        // 
        // sqlSelectCommand4
        // 
        this.sqlSelectCommand4.CommandText = @"SELECT DISTINCT Countries.ID, Countries.RegionID, Countries.Country FROM Countries INNER JOIN StateProvinces ON Countries.ID = StateProvinces.CountryID INNER JOIN Cities ON StateProvinces.ID = Cities.StateProvinceID INNER JOIN Properties ON Cities.ID = Properties.CityID WHERE (Properties.IfFinished = 1) AND (Properties.IfApproved = 1) ORDER BY Countries.Country";
        this.sqlSelectCommand4.Connection = CommonFunctions.GetConnection();

        // RegionsAdapter
        // 
        this.RegionsAdapter.InsertCommand = this.sqlInsertCommand6;
        this.RegionsAdapter.SelectCommand = this.sqlSelectCommand5;
        this.RegionsAdapter.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																								 new System.Data.Common.DataTableMapping("Table", "Regions", new System.Data.Common.DataColumnMapping[] {
																																																			new System.Data.Common.DataColumnMapping("ID", "ID"),
																																																			new System.Data.Common.DataColumnMapping("Region", "Region")})});
        // 
        // sqlInsertCommand6
        // 
        this.sqlInsertCommand6.CommandText = "INSERT INTO Regions(Region) VALUES (@Region)";
        this.sqlInsertCommand6.Connection = CommonFunctions.GetConnection();
        this.sqlInsertCommand6.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Region", System.Data.SqlDbType.NVarChar, 300, "Region"));
        // 
        // sqlSelectCommand5
        // 
        this.sqlSelectCommand5.CommandText = @"SELECT DISTINCT Regions.ID, Regions.Region FROM Regions INNER JOIN Countries ON Regions.ID = Countries.RegionID INNER JOIN StateProvinces ON Countries.ID = StateProvinces.CountryID INNER JOIN Cities ON StateProvinces.ID = Cities.StateProvinceID INNER JOIN Properties ON Cities.ID = Properties.CityID WHERE (Properties.IfFinished = 1) AND (Properties.IfApproved = 1) ORDER BY Region";
        this.sqlSelectCommand5.Connection = CommonFunctions.GetConnection();

        // CitiesSet
        // 
        this.CitiesSet.DataSetName = "CitiesDataset";
        this.CitiesSet.Locale = new System.Globalization.CultureInfo("en-US");
        // 
        // CountriesSet
        // 
        this.CountriesSet.DataSetName = "CountriesDataset";
        this.CountriesSet.Locale = new System.Globalization.CultureInfo("en-US");
        // 
        // StateProvincesAdapter
        // 
        this.StateProvincesAdapter.InsertCommand = this.sqlInsertCommand3;
        this.StateProvincesAdapter.SelectCommand = this.sqlSelectCommand2;
        this.StateProvincesAdapter.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																										new System.Data.Common.DataTableMapping("Table", "StateProvinces", new System.Data.Common.DataColumnMapping[] {
																																																						  new System.Data.Common.DataColumnMapping("ID", "ID"),
																																																						  new System.Data.Common.DataColumnMapping("CountryID", "CountryID"),
																																																						  new System.Data.Common.DataColumnMapping("StateProvince", "StateProvince")})});
        // 
        // sqlInsertCommand3
        // 
        this.sqlInsertCommand3.CommandText = "INSERT INTO StateProvinces(CountryID, StateProvince) VALUES (@CountryID, @StatePr" +
            "ovince)";
        this.sqlInsertCommand3.Connection = CommonFunctions.GetConnection();
        this.sqlInsertCommand3.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CountryID", System.Data.SqlDbType.Int, 4, "CountryID"));
        this.sqlInsertCommand3.Parameters.Add(new System.Data.SqlClient.SqlParameter("@StateProvince", System.Data.SqlDbType.NVarChar, 300, "StateProvince"));
        // 
        // sqlSelectCommand2
        // 
        this.sqlSelectCommand2.CommandText = @"SELECT DISTINCT StateProvinces.ID, StateProvinces.CountryID, StateProvinces.StateProvince FROM StateProvinces INNER JOIN Cities ON StateProvinces.ID = Cities.StateProvinceID INNER JOIN Properties ON Cities.ID = Properties.CityID WHERE (Properties.IfFinished = 1) AND (Properties.IfApproved = 1) UNION SELECT -1, -1, ' Include All' ORDER BY StateProvince";
        this.sqlSelectCommand2.Connection = CommonFunctions.GetConnection();

        //// CitiesAdapter
        //// 
        this.CitiesAdapter.InsertCommand = this.sqlInsertCommand1;
        this.CitiesAdapter.SelectCommand = this.sqlSelectCommand7;
        this.CitiesAdapter.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																								new System.Data.Common.DataTableMapping("Table", "Cities", new System.Data.Common.DataColumnMapping[] {
																																																		  new System.Data.Common.DataColumnMapping("ID", "ID"),
																																																		  new System.Data.Common.DataColumnMapping("StateProvinceID", "StateProvinceID"),
																																																		  new System.Data.Common.DataColumnMapping("City", "City")})});
        // 
        // sqlInsertCommand1
        // 
        this.sqlInsertCommand1.CommandText = "INSERT INTO Cities(StateProvinceID, City) VALUES (@StateProvinceID, @City)";
        this.sqlInsertCommand1.Connection = CommonFunctions.GetConnection();
        this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@StateProvinceID", System.Data.SqlDbType.Int, 4, "StateProvinceID"));
        this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@City", System.Data.SqlDbType.NVarChar, 300, "City"));
        // 
        // sqlSelectCommand7
        // 
        this.sqlSelectCommand7.CommandText = @"SELECT DISTINCT Cities.ID, Cities.StateProvinceID, Cities.City FROM Cities INNER JOIN Properties ON Cities.ID=Properties.CityID WHERE (Properties.IfFinished = 1) AND (Properties.IfApproved = 1) UNION SELECT -1, -1, ' Include All' ORDER BY City";
        this.sqlSelectCommand7.Connection = CommonFunctions.GetConnection();

        this.sqlSelectCommand6.Connection = CommonFunctions.GetConnection();
        // 
        // CountriesSet2
        // 
        this.CountriesSet2.DataSetName = "CountriesDataset";
        this.CountriesSet2.Locale = new System.Globalization.CultureInfo("en-US");
        ((System.ComponentModel.ISupportInitialize)(this.RegionsSet)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.StateProvincesSet)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.CitiesSet)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.CountriesSet)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.CountriesSet2)).EndInit();

    }
    #endregion
    
}
