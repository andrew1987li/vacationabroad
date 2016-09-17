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

public partial class Locations : AdminPage
{
    //protected System.Data.SqlClient.SqlConnection Connection;
    protected System.Data.SqlClient.SqlDataAdapter CitiesAdapter;
    protected System.Data.SqlClient.SqlDataAdapter RegionsAdapter;
    protected System.Data.SqlClient.SqlCommand sqlSelectCommand11;
    protected Vacations.RegionsDataset RegionsSet;
    protected System.Data.SqlClient.SqlDataAdapter StateProvincesAdapter;
    protected Vacations.CountriesDataset CountriesSet;
    protected System.Data.SqlClient.SqlDataAdapter CountriesAdapter;
    protected Vacations.CitiesDataset CitiesSet;
    protected Vacations.StateProvincesDataset StateProvincesSet;
    protected System.Data.SqlClient.SqlCommand sqlSelectCommand3;
    protected System.Data.SqlClient.SqlCommand sqlInsertCommand1;
    protected System.Data.SqlClient.SqlCommand sqlSelectCommand2;
    protected System.Data.SqlClient.SqlCommand sqlInsertCommand2;
    protected System.Data.SqlClient.SqlCommand sqlSelectCommand1;
    protected System.Data.SqlClient.SqlCommand sqlInsertCommand3;

    protected void Page_Load(object sender, System.EventArgs e)
    {
        System.Data.SqlClient.SqlCommandBuilder builder = new System.Data.SqlClient.SqlCommandBuilder(RegionsAdapter);
        System.Data.SqlClient.SqlCommandBuilder builder2 = new System.Data.SqlClient.SqlCommandBuilder(CountriesAdapter);
        System.Data.SqlClient.SqlCommandBuilder builder3 = new System.Data.SqlClient.SqlCommandBuilder(StateProvincesAdapter);
        System.Data.SqlClient.SqlCommandBuilder builder4 = new System.Data.SqlClient.SqlCommandBuilder(CitiesAdapter);

        userid = AuthenticationManager.UserID;

        //string connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        //CommonFunctions.Connection.ConnectionString = connectionstring;

        //lock (CommonFunctions.Connection)
        RegionsAdapter.Fill(RegionsSet);

        if (!IsPostBack)
        {
            DataBind();

            RegionList_SelectedIndexChanged(RegionList, EventArgs.Empty);
        }
        else
        {
            try
            {
                if (Convert.ToInt32(RegionList.SelectedValue) != 0)
                {
                    CountriesAdapter.SelectCommand.Parameters["@RegionID"].Value = Convert.ToInt32(RegionList.SelectedValue);
                    //lock (CommonFunctions.Connection)
                    CountriesAdapter.Fill(CountriesSet);
                }
            }
            catch (Exception)
            {
            }
            try
            {
                if (Convert.ToInt32(CountryList.SelectedValue) != 0)
                {
                    StateProvincesAdapter.SelectCommand.Parameters["@CountryID"].Value = Convert.ToInt32(CountryList.SelectedValue);
                    //lock (CommonFunctions.Connection)
                    StateProvincesAdapter.Fill(StateProvincesSet);
                }
            }
            catch (Exception)
            {
            }
            try
            {
                if (Convert.ToInt32(StateList.SelectedValue) != 0)
                {
                    CitiesAdapter.SelectCommand.Parameters["@StateProvinceID"].Value = Convert.ToInt32(StateList.SelectedValue);
                    //lock (CommonFunctions.Connection)
                    CitiesAdapter.Fill(CitiesSet);
                }
            }
            catch (Exception)
            {
            }
        }
        Page.Header.Controls.Add(new LiteralControl("<link href='/css/StyleSheetBig4.css' rel='stylesheet' type='text/css'></script>"));

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
        this.CitiesAdapter = new System.Data.SqlClient.SqlDataAdapter();
        this.sqlInsertCommand3 = new System.Data.SqlClient.SqlCommand();
        this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
        this.RegionsAdapter = new System.Data.SqlClient.SqlDataAdapter();
        this.sqlSelectCommand11 = new System.Data.SqlClient.SqlCommand();
        this.RegionsSet = new Vacations.RegionsDataset();
        this.StateProvincesAdapter = new System.Data.SqlClient.SqlDataAdapter();
        this.sqlInsertCommand2 = new System.Data.SqlClient.SqlCommand();
        this.sqlSelectCommand2 = new System.Data.SqlClient.SqlCommand();
        this.CountriesSet = new Vacations.CountriesDataset();
        this.CountriesAdapter = new System.Data.SqlClient.SqlDataAdapter();
        this.sqlInsertCommand1 = new System.Data.SqlClient.SqlCommand();
        this.sqlSelectCommand3 = new System.Data.SqlClient.SqlCommand();
        this.CitiesSet = new Vacations.CitiesDataset();
        this.StateProvincesSet = new Vacations.StateProvincesDataset();
        ((System.ComponentModel.ISupportInitialize)(this.RegionsSet)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.CountriesSet)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.CitiesSet)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.StateProvincesSet)).BeginInit();
        // 
        // CommonFunctions.Connection
        // 
        //CommonFunctions.Connection.ConnectionString = "workstation id=MAIN;packet size=4096;integrated security=SSPI;data source=MAIN;pe" +
        //"rsist security info=False;initial catalog=Vacations";
        // 
        // CitiesAdapter
        // 
        this.CitiesAdapter.InsertCommand = this.sqlInsertCommand3;
        this.CitiesAdapter.SelectCommand = this.sqlSelectCommand1;
        this.CitiesAdapter.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
		new System.Data.Common.DataTableMapping("Table", 
            "Cities", 
            new System.Data.Common.DataColumnMapping[]
{
            new System.Data.Common.DataColumnMapping("ID", "ID"), 
            new System.Data.Common.DataColumnMapping("StateProvinceID", "StateProvinceID"), 
            new System.Data.Common.DataColumnMapping("City", "City"),
            new System.Data.Common.DataColumnMapping("titleoverride", "titleoverride"),
            new System.Data.Common.DataColumnMapping("descriptionoverride", "descriptionoverride") 
        }
            )});
        // 
        // sqlInsertCommand3
        // 
        this.sqlInsertCommand3.CommandText = "INSERT INTO Cities(StateProvinceID, City) VALUES (@StateProvinceID, @City)";
        this.sqlInsertCommand3.Connection = CommonFunctions.GetConnection();
        this.sqlInsertCommand3.Parameters.Add(new System.Data.SqlClient.SqlParameter("@StateProvinceID", System.Data.SqlDbType.Int, 4, "StateProvinceID"));
        this.sqlInsertCommand3.Parameters.Add(new System.Data.SqlClient.SqlParameter("@City", System.Data.SqlDbType.NVarChar, 300, "City"));
        // 
        // sqlSelectCommand1
        // 
        this.sqlSelectCommand1.CommandText = "SELECT ID, StateProvinceID, City, titleoverride, descriptionoverride FROM Cities WHERE (@StateProvinceID = - 1) OR (@" +
            "StateProvinceID = StateProvinceID) ORDER BY City";
        this.sqlSelectCommand1.Connection = CommonFunctions.GetConnection();
        this.sqlSelectCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@StateProvinceID", System.Data.SqlDbType.Variant));
        // 
        // RegionsAdapter
        // 
        this.RegionsAdapter.SelectCommand = this.sqlSelectCommand11;
        this.RegionsAdapter.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																								 new System.Data.Common.DataTableMapping("Table", "Regions", new System.Data.Common.DataColumnMapping[] {
																																																			new System.Data.Common.DataColumnMapping("ID", "ID"),
																																																			new System.Data.Common.DataColumnMapping("Region", "Region")})});
        // 
        // sqlSelectCommand11
        // 
        this.sqlSelectCommand11.CommandText = "SELECT ID, Region FROM Regions ORDER BY Region";
        this.sqlSelectCommand11.Connection = CommonFunctions.GetConnection();
        // 
        // RegionsSet
        // 
        this.RegionsSet.DataSetName = "RegionsDataset";
        this.RegionsSet.Locale = new System.Globalization.CultureInfo("en-US");
        // 
        // StateProvincesAdapter
        // 
        this.StateProvincesAdapter.InsertCommand = this.sqlInsertCommand2;
        this.StateProvincesAdapter.SelectCommand = this.sqlSelectCommand2;
        this.StateProvincesAdapter.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] 
        {
          new System.Data.Common.DataTableMapping("Table", "StateProvinces", new System.Data.Common.DataColumnMapping[] 
            {
              new System.Data.Common.DataColumnMapping("ID", "ID"),
              new System.Data.Common.DataColumnMapping("CountryID", "CountryID"),
              new System.Data.Common.DataColumnMapping("StateProvince", "StateProvince"),
              new System.Data.Common.DataColumnMapping("titleoverride", "titleoverride"),
              new System.Data.Common.DataColumnMapping("descriptionoverride", "descriptionoverride")
            }
            )
        });
        // 
        // sqlInsertCommand2  - Insert for State Provinces 
        // 
        this.sqlInsertCommand2.CommandText = "INSERT INTO StateProvinces(CountryID, StateProvince) VALUES (@CountryID, @StateProvince)";
        this.sqlInsertCommand2.Connection = CommonFunctions.GetConnection();
        this.sqlInsertCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CountryID", System.Data.SqlDbType.Int, 4, "CountryID"));
        this.sqlInsertCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@StateProvince", System.Data.SqlDbType.NVarChar, 300, "StateProvince"));
        // 
        // sqlSelectCommand2 - should really be State Select Command
        // 
        this.sqlSelectCommand2.CommandText = "SELECT ID, CountryID, StateProvince, titleoverride, descriptionoverride FROM StateProvinces WHERE (@CountryID = - 1) " +
            "OR (@CountryID = CountryID) ORDER BY StateProvince";
        this.sqlSelectCommand2.Connection = CommonFunctions.GetConnection();
        this.sqlSelectCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CountryID", System.Data.SqlDbType.Variant));
        // 
        // CountriesSet
        // 
        this.CountriesSet.DataSetName = "CountriesDataset";
        this.CountriesSet.Locale = new System.Globalization.CultureInfo("en-US");
        // 
        // CountriesAdapter
        // 
        this.CountriesAdapter.InsertCommand = this.sqlInsertCommand1;
        this.CountriesAdapter.SelectCommand = this.sqlSelectCommand3;
        this.CountriesAdapter.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "Countries", new System.Data.Common.DataColumnMapping[] 
        {
			new System.Data.Common.DataColumnMapping("ID", "ID"),
			new System.Data.Common.DataColumnMapping("RegionID", "RegionID"),
			new System.Data.Common.DataColumnMapping("Country", "Country"),
            new System.Data.Common.DataColumnMapping("titleoverride", "titleoverride"),
            new System.Data.Common.DataColumnMapping("descriptionoverride", "descriptionoverride")
        })});
        // 
        // sqlInsertCommand1
        // 
        this.sqlInsertCommand1.CommandText = "INSERT INTO Countries(RegionID, Country) VALUES (@RegionID, @Country)";
        this.sqlInsertCommand1.Connection = CommonFunctions.GetConnection();
        this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RegionID", System.Data.SqlDbType.Int, 4, "RegionID"));
        this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Country", System.Data.SqlDbType.NVarChar, 300, "Country"));
        // 
        // sqlSelectCommand3  - For Country selection
        // 
        this.sqlSelectCommand3.CommandText = "SELECT ID, RegionID, Country, titleoverride, descriptionoverride FROM Countries WHERE (@RegionID = - 1) OR (@RegionID  = RegionID) ORDER BY Country";
        this.sqlSelectCommand3.Connection = CommonFunctions.GetConnection();
        this.sqlSelectCommand3.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RegionID", System.Data.SqlDbType.Variant));
        // 
        // CitiesSet
        // 
        this.CitiesSet.DataSetName = "CitiesDataset";
        this.CitiesSet.Locale = new System.Globalization.CultureInfo("en-US");
        // 
        // StateProvincesSet
        // 
        this.StateProvincesSet.DataSetName = "StateProvincesDataset";
        this.StateProvincesSet.Locale = new System.Globalization.CultureInfo("en-US");
        ((System.ComponentModel.ISupportInitialize)(this.RegionsSet)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.CountriesSet)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.CitiesSet)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.StateProvincesSet)).EndInit();

    }
    #endregion

    protected void RegionList_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        foreach (DataRow datarow in RegionsSet.Tables["Regions"].Rows)
            if (datarow.RowState != DataRowState.Deleted)
                if ((int)datarow["ID"] == Convert.ToInt32(RegionList.SelectedValue))
                {
                    RegionName.Text = (string)datarow["Region"];
                    break;
                }

        CountriesSet.Clear();
        try
        {
            if (Convert.ToInt32(RegionList.SelectedValue) != 0)
            {
                CountriesAdapter.SelectCommand.Parameters["@RegionID"].Value = Convert.ToInt32(RegionList.SelectedValue);
                //lock (CommonFunctions.Connection)
                CountriesAdapter.Fill(CountriesSet);
            }
        }
        catch (Exception)
        {
        }

        CountryList.DataBind();
        CountryList2.DataBind();
        CountryList_SelectedIndexChanged(CountryList, EventArgs.Empty);
    }

    protected void CountryList_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        foreach (DataRow datarow in CountriesSet.Tables["Countries"].Rows)
            if (datarow.RowState != DataRowState.Deleted)
                if ((int)datarow["ID"] == Convert.ToInt32(CountryList.SelectedValue))
                {
                    if (datarow["RegionID"] is int)
                        RegionList2.SelectedValue = ((int)datarow["RegionID"]).ToString();
                    CountryName.Text = (string)datarow["Country"];
                    if (datarow["titleoverride"] != null)
                         Country_Title_OverRide.Text = datarow["titleoverride"].ToString();
                     if (datarow["descriptionoverride"] != null)
                         Country_description_override.Text = datarow["descriptionoverride"].ToString();

                    break;
                }

        StateProvincesSet.Clear();
        try
        {
            if (Convert.ToInt32(CountryList.SelectedValue) != 0)
            {
                StateProvincesAdapter.SelectCommand.Parameters["@CountryID"].Value = Convert.ToInt32(CountryList.SelectedValue);
                //lock (CommonFunctions.Connection)
                StateProvincesAdapter.Fill(StateProvincesSet);
            }
        }
        catch (Exception)
        {
        }

        StateList.DataBind();
        StateList2.DataBind();
        StateList_SelectedIndexChanged(StateList, EventArgs.Empty);
    }

    protected void StateList_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        foreach (DataRow datarow in StateProvincesSet.Tables["StateProvinces"].Rows)
            if (datarow.RowState != DataRowState.Deleted)
                if ((int)datarow["ID"] == Convert.ToInt32(StateList.SelectedValue))
                {
                    if (datarow["CountryID"] is int)
                        CountryList2.SelectedValue = ((int)datarow["CountryID"]).ToString();
                    StateName.Text = (string)datarow["StateProvince"];
                    if (datarow["titleoverride"] != null)
                        State_Title_override.Text = datarow["titleoverride"].ToString();
                    if (datarow["descriptionoverride"] != null)
                       State_Description_override.Text = datarow["descriptionoverride"].ToString();
                    break;
                }

        CitiesSet.Clear();
        try
        {
            if (Convert.ToInt32(StateList.SelectedValue) != 0)
            {
                CitiesAdapter.SelectCommand.Parameters["@StateProvinceID"].Value = Convert.ToInt32(StateList.SelectedValue);
                //lock (CommonFunctions.Connection)
                CitiesAdapter.Fill(CitiesSet);
            }
        }
        catch (Exception)
        {
        }

        CityList.DataBind();
        CityList_SelectedIndexChanged(CityList, EventArgs.Empty);
    }

    protected void CityList_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        foreach (DataRow datarow in CitiesSet.Tables["Cities"].Rows)
            if (datarow.RowState != DataRowState.Deleted)
                if ((int)datarow["ID"] == Convert.ToInt32(CityList.SelectedValue))
                {
                    if (datarow["StateProvinceID"] is int)
                        StateList2.SelectedValue = ((int)datarow["StateProvinceID"]).ToString();
                    CityName.Text = (string)datarow["City"];
                    if ( datarow["titleoverride"]!=null)
                    City_Title_Override.Text = datarow["titleoverride"].ToString();
                if (datarow["descriptionoverride"] != null)
                    City_Description_override.Text = datarow["descriptionoverride"].ToString();
                    break;
                }
    }

    protected void RegionRename_Click(object sender, System.EventArgs e)
    {
        if (RegionName.Text.Length < 1)
            return;

        foreach (DataRow datarow in RegionsSet.Tables["Regions"].Rows)
            if (datarow.RowState != DataRowState.Deleted)
                if ((int)datarow["ID"] == Convert.ToInt32(RegionList.SelectedValue))
                {
                    datarow["Region"] = RegionName.Text;
                    break;
                }

        //lock (CommonFunctions.Connection)
        RegionsAdapter.Update(RegionsSet);

        Finish();
    }

    protected void CountryRename_Click(object sender, System.EventArgs e)
    {
        if (CountryName.Text.Length < 1)
            return;

        foreach (DataRow datarow in CountriesSet.Tables["Countries"].Rows)
            if (datarow.RowState != DataRowState.Deleted)
                if ((int)datarow["ID"] == Convert.ToInt32(CountryList.SelectedValue))
                {
                    datarow["Country"] = CountryName.Text;
                    break;
                }

        //lock (CommonFunctions.Connection)
        CountriesAdapter.Update(CountriesSet);

        Finish();
    }

    protected void StateRename_Click(object sender, System.EventArgs e)
    {
        if (StateName.Text.Length < 1)
            return;

        foreach (DataRow datarow in StateProvincesSet.Tables["StateProvinces"].Rows)
            if (datarow.RowState != DataRowState.Deleted)
                if ((int)datarow["ID"] == Convert.ToInt32(StateList.SelectedValue))
                {
                    datarow["StateProvince"] = StateName.Text;
                    break;
                }

        //lock (CommonFunctions.Connection)
        StateProvincesAdapter.Update(StateProvincesSet);

        Finish();
    }

    protected void CityRename_Click(object sender, System.EventArgs e)
    {
        if (CityName.Text.Length < 1)
            return;

        foreach (DataRow datarow in CitiesSet.Tables["Cities"].Rows)
            if (datarow.RowState != DataRowState.Deleted)
                if ((int)datarow["ID"] == Convert.ToInt32(CityList.SelectedValue))
                {
                    datarow["City"] = CityName.Text;
                    break;
                }

        //lock (CommonFunctions.Connection)
        CitiesAdapter.Update(CitiesSet);

        Finish();
    }

    protected void RegionDelete_Click(object sender, System.EventArgs e)
    {
        foreach (DataRow datarow in RegionsSet.Tables["Regions"].Rows)
            if (datarow.RowState != DataRowState.Deleted)
                if ((int)datarow["ID"] == Convert.ToInt32(RegionList.SelectedValue))
                {
                    datarow.Delete();
                    break;
                }

        //lock (CommonFunctions.Connection)
        RegionsAdapter.Update(RegionsSet);

        Finish();
    }

    protected void CountryDelete_Click(object sender, System.EventArgs e)
    {
        foreach (DataRow datarow in CountriesSet.Tables["Countries"].Rows)
            if (datarow.RowState != DataRowState.Deleted)
                if ((int)datarow["ID"] == Convert.ToInt32(CountryList.SelectedValue))
                {
                    datarow.Delete();
                    break;
                }

        //lock (CommonFunctions.Connection)
        CountriesAdapter.Update(CountriesSet);

        Finish();
    }

    protected void StateDelete_Click(object sender, System.EventArgs e)
    {
        foreach (DataRow datarow in StateProvincesSet.Tables["StateProvinces"].Rows)
            if (datarow.RowState != DataRowState.Deleted)
                if ((int)datarow["ID"] == Convert.ToInt32(StateList.SelectedValue))
                {
                    datarow.Delete();
                    break;
                }

        //lock (CommonFunctions.Connection)
        StateProvincesAdapter.Update(StateProvincesSet);

        Finish();
    }

    protected void CityDelete_Click(object sender, System.EventArgs e)
    {
        foreach (DataRow datarow in CitiesSet.Tables["Cities"].Rows)
            if (datarow.RowState != DataRowState.Deleted)
                if ((int)datarow["ID"] == Convert.ToInt32(CityList.SelectedValue))
                {
                    datarow.Delete();
                    break;
                }

        //lock (CommonFunctions.Connection)
        CitiesAdapter.Update(CitiesSet);

        Finish();
    }

    protected void RegionList2_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        foreach (DataRow datarow in CountriesSet.Tables["Countries"].Rows)
            if (datarow.RowState != DataRowState.Deleted)
                if ((int)datarow["ID"] == Convert.ToInt32(CountryList.SelectedValue))
                {
                    datarow["RegionID"] = Convert.ToInt32(RegionList2.SelectedValue);
                    break;
                }

        //lock (CommonFunctions.Connection)
        CountriesAdapter.Update(CountriesSet);

        Finish();
    }

    protected void CountryList2_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        foreach (DataRow datarow in StateProvincesSet.Tables["StateProvinces"].Rows)
            if (datarow.RowState != DataRowState.Deleted)
                if ((int)datarow["ID"] == Convert.ToInt32(StateList.SelectedValue))
                {
                    datarow["CountryID"] = Convert.ToInt32(CountryList2.SelectedValue);
                    break;
                }

        //lock (CommonFunctions.Connection)
        StateProvincesAdapter.Update(StateProvincesSet);

        Finish();
    }

    protected void StateList2_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        foreach (DataRow datarow in CitiesSet.Tables["Cities"].Rows)
            if (datarow.RowState != DataRowState.Deleted)
                if ((int)datarow["ID"] == Convert.ToInt32(CityList.SelectedValue))
                {
                    datarow["StateProvinceID"] = Convert.ToInt32(StateList2.SelectedValue);
                    break;
                }

        //lock (CommonFunctions.Connection)
        CitiesAdapter.Update(CitiesSet);

        Finish();
    }

    protected void RegionAdd_Click(object sender, System.EventArgs e)
    {
        EnterRegion.Validate();
        InvalidRegion.Validate();

        if (!EnterRegion.IsValid || !InvalidRegion.IsValid)
            return;

        DataRow newrow = RegionsSet.Tables["Regions"].NewRow();

        newrow["Region"] = NewRegion.Text;

        RegionsSet.Tables["Regions"].Rows.Add(newrow);

        //lock (CommonFunctions.Connection)
        RegionsAdapter.Update(RegionsSet);

        Finish();
    }

    protected void CountryAdd_Click(object sender, System.EventArgs e)
    {
        EnterCountry.Validate();
        InvalidCountry.Validate();

        if (!EnterCountry.IsValid || !InvalidCountry.IsValid)
            return;

        DataRow newrow = CountriesSet.Tables["Countries"].NewRow();

        newrow["Country"] = NewCountry.Text;
        try
        {
            newrow["RegionID"] = Convert.ToInt32(RegionList.SelectedValue);

            CountriesSet.Tables["Countries"].Rows.Add(newrow);

            //lock (CommonFunctions.Connection)
            CountriesAdapter.Update(CountriesSet);

            Finish();
        }
        catch (Exception)
        {
        }
    }

    protected void StateAdd_Click(object sender, System.EventArgs e)
    {
        EnterState.Validate();
        InvalidState.Validate();

        if (!EnterState.IsValid || !InvalidState.IsValid)
            return;

        DataRow newrow = StateProvincesSet.Tables["StateProvinces"].NewRow();

        newrow["StateProvince"] = NewState.Text;
        try
        {
            newrow["CountryID"] = Convert.ToInt32(CountryList.SelectedValue);

            StateProvincesSet.Tables["StateProvinces"].Rows.Add(newrow);

            //lock (CommonFunctions.Connection)
            StateProvincesAdapter.Update(StateProvincesSet);

            Finish();
        }
        catch (Exception)
        {
        }
    }

    protected void CityAdd_Click(object sender, System.EventArgs e)
    {
        EnterCity.Validate();
        InvalidCity.Validate();

        if (!EnterCity.IsValid || !InvalidCity.IsValid)
            return;

        DataRow newrow = CitiesSet.Tables["Cities"].NewRow();

        newrow["City"] = NewCity.Text;
        try
        {
            newrow["StateProvinceID"] = Convert.ToInt32(StateList.SelectedValue);

            CitiesSet.Tables["Cities"].Rows.Add(newrow);

            //lock (CommonFunctions.Connection)
            CitiesAdapter.Update(CitiesSet);

            Finish();
        }
        catch (Exception)
        {
        }
    }

    private void Finish()
    {
        NewRegion.Text = "";
        NewCountry.Text = "";
        NewState.Text = "";
        NewCity.Text = "";
        RegionName.Text = "";
        CountryName.Text = "";
        StateName.Text = "";
        CityName.Text = "";

        int selectedregion = 0;
        int selectedcountry = 0;
        int selectedstate = 0;
        int selectedcity = 0;

        try
        {
            selectedregion = Convert.ToInt32(RegionList.SelectedValue);
        }
        catch (Exception)
        {
        }
        try
        {
            selectedcountry = Convert.ToInt32(CountryList.SelectedValue);
        }
        catch (Exception)
        {
        }
        try
        {
            selectedstate = Convert.ToInt32(StateList.SelectedValue);
        }
        catch (Exception)
        {
        }
        try
        {
            selectedcity = Convert.ToInt32(CityList.SelectedValue);
        }
        catch (Exception)
        {
        }

        RegionsSet.Clear();
        //lock (CommonFunctions.Connection)
        RegionsAdapter.Fill(RegionsSet);

        DataBind();

        if (selectedregion != 0)
        {
            try
            {
                RegionList.SelectedValue = selectedregion.ToString();
            }
            catch (Exception)
            {
            }
        }
        RegionList_SelectedIndexChanged(RegionList, EventArgs.Empty);
        if (selectedcountry != 0)
        {
            try
            {
                CountryList.SelectedValue = selectedcountry.ToString();
                CountryList_SelectedIndexChanged(CountryList, EventArgs.Empty);
            }
            catch (Exception)
            {
            }
        }
        if (selectedstate != 0)
        {
            try
            {
                StateList.SelectedValue = selectedstate.ToString();
                StateList_SelectedIndexChanged(StateList, EventArgs.Empty);
            }
            catch (Exception)
            {
            }
        }
        if (selectedcity != 0)
        {
            try
            {
                CityList.SelectedValue = selectedcity.ToString();
                CityList_SelectedIndexChanged(CityList, EventArgs.Empty);
            }
            catch (Exception)
            {
            }
        }
    }
    protected void BtnChangeCityTitle_Click(object sender, EventArgs e)
    {
        if (City_Title_Override.Text.Length < 1)
            City_Title_Override.Text = String.Empty;

        foreach (DataRow datarow in CitiesSet.Tables["Cities"].Rows)
            if (datarow.RowState != DataRowState.Deleted)
                if ((int)datarow["ID"] == Convert.ToInt32(CityList.SelectedValue))
                {
                    datarow["titleoverride"] = City_Title_Override.Text;
                    break;
                }

        //lock (CommonFunctions.Connection)
        CitiesAdapter.Update(CitiesSet);

        Finish();
    }
     protected void BtnChangeStateTitle_Click(object sender, EventArgs e)
    {
        if (State_Title_override.Text.Length < 1)
            State_Title_override.Text = String.Empty;

        foreach (DataRow datarow in StateProvincesSet.Tables["StateProvinces"].Rows)
            if (datarow.RowState != DataRowState.Deleted)
                if ((int)datarow["ID"] == Convert.ToInt32(StateList.SelectedValue))
                {
                    datarow["titleoverride"] = State_Title_override.Text ;
                    break;
                }

        //lock (CommonFunctions.Connection)
        StateProvincesAdapter.Update(StateProvincesSet);
        

        Finish();

    }
    protected void BtnChangeCountryTitle_Click(object sender, EventArgs e)
    {
        if (Country_Title_OverRide.Text.Length < 1)
            Country_Title_OverRide.Text = String.Empty;

        foreach (DataRow datarow in CountriesSet.Tables["Countries"].Rows)
            if (datarow.RowState != DataRowState.Deleted)
                if ((int)datarow["ID"] == Convert.ToInt32(CountryList.SelectedValue))
                {
                    datarow["titleoverride"] = Country_Title_OverRide.Text;
                    break;
                }

        //lock (CommonFunctions.Connection)
        CountriesAdapter.Update(CountriesSet);

        Finish();
    }

    protected void BtnChangeCountryDescription_Click(object sender, EventArgs e)
    {
        if (Country_description_override.Text.Length < 1)
            Country_description_override.Text = String.Empty;

        foreach (DataRow datarow in CountriesSet.Tables["Countries"].Rows)
            if (datarow.RowState != DataRowState.Deleted)
                if ((int)datarow["ID"] == Convert.ToInt32(CountryList.SelectedValue))
                {
                    datarow["descriptionoverride"] = Country_description_override.Text;
                    break;
                }

        //lock (CommonFunctions.Connection)
        CountriesAdapter.Update(CountriesSet);

        Finish();
    }
    protected void BtnChangeStateDescription_Click(object sender, EventArgs e)
    { 
        if (State_Description_override.Text.Length < 1)
            State_Description_override.Text = String.Empty;

        foreach (DataRow datarow in StateProvincesSet.Tables["StateProvinces"].Rows)
            if (datarow.RowState != DataRowState.Deleted)
                if ((int)datarow["ID"] == Convert.ToInt32(StateList.SelectedValue))
                {
                    datarow["descriptionoverride"] = State_Description_override.Text;
                    break;
                }

        //lock (CommonFunctions.Connection)
        StateProvincesAdapter.Update(StateProvincesSet);


        Finish();
    }
    protected void BtnChangeCityDescription_Click(object sender, EventArgs e)
    { 
        if (City_Description_override.Text.Length < 1)
            City_Description_override.Text = String.Empty;

        foreach (DataRow datarow in CitiesSet.Tables["Cities"].Rows)
            if (datarow.RowState != DataRowState.Deleted)
                if ((int)datarow["ID"] == Convert.ToInt32(CityList.SelectedValue))
                {
                    datarow["descriptionoverride"] = City_Description_override.Text;
                    break;
                }

        //lock (CommonFunctions.Connection)
        CitiesAdapter.Update(CitiesSet);

        Finish();
    }
}
