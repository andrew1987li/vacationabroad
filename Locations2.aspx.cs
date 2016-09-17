using System;
using System.Collections;
using System.Collections.Generic;
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
    protected Vacations.CountiesDataSet CountiesSet;
    protected System.Data.SqlClient.SqlDataAdapter CountiesAdapter;
    protected Vacations.StateProvincesDataset StateProvincesSet;
    protected System.Data.SqlClient.SqlCommand sqlSelectCommand3;
    protected System.Data.SqlClient.SqlCommand sqlInsertCommand1;
    protected System.Data.SqlClient.SqlCommand sqlSelectCommand2;
    protected System.Data.SqlClient.SqlCommand sqlInsertCommand2;
    protected System.Data.SqlClient.SqlCommand sqlSelectCommand1;
    protected System.Data.SqlClient.SqlCommand sqlInsertCommand3;
    protected System.Data.SqlClient.SqlCommand sqlInsertCommandCnty;
    protected System.Data.SqlClient.SqlCommand sqlSelectCommandCnty;

    protected void Page_Load(object sender, System.EventArgs e)
    {
        System.Data.SqlClient.SqlCommandBuilder builder = new System.Data.SqlClient.SqlCommandBuilder(RegionsAdapter);
        System.Data.SqlClient.SqlCommandBuilder builder2 = new System.Data.SqlClient.SqlCommandBuilder(CountriesAdapter);
        System.Data.SqlClient.SqlCommandBuilder builder3 = new System.Data.SqlClient.SqlCommandBuilder(StateProvincesAdapter);
        System.Data.SqlClient.SqlCommandBuilder builder4 = new System.Data.SqlClient.SqlCommandBuilder(CitiesAdapter);
        System.Data.SqlClient.SqlCommandBuilder builder5 = new System.Data.SqlClient.SqlCommandBuilder(CountiesAdapter);

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

        if (!IsPostBack)
            FillCities();



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
        this.CountiesAdapter = new System.Data.SqlClient.SqlDataAdapter();
        this.CountiesSet = new Vacations.CountiesDataSet();
        this.sqlInsertCommandCnty = new System.Data.SqlClient.SqlCommand();
        this.sqlSelectCommandCnty = new System.Data.SqlClient.SqlCommand();
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
        ((System.ComponentModel.ISupportInitialize)(this.CountiesSet)).BeginInit();
        // 
        // CommonFunctions.Connection
        // 
        //CommonFunctions.Connection.ConnectionString = "workstation id=MAIN;packet size=4096;integrated security=SSPI;data source=MAIN;pe" +
        //"rsist security info=False;initial catalog=Vacations";
        // 
        // CitiesAdapter
        // 
        //********************
        this.CountiesAdapter.InsertCommand = this.sqlInsertCommandCnty;
        this.CountiesAdapter.SelectCommand = this.sqlSelectCommandCnty;
        this.CountiesAdapter.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
		new System.Data.Common.DataTableMapping("Table", 
            "Counties", 
            new System.Data.Common.DataColumnMapping[]
{
            new System.Data.Common.DataColumnMapping("ID", "ID"), 
            new System.Data.Common.DataColumnMapping("StateID", "StateID"), 
            new System.Data.Common.DataColumnMapping("County", "County"),
            new System.Data.Common.DataColumnMapping("titleoverride", "titleoverride"),
            new System.Data.Common.DataColumnMapping("descriptionoverride", "descriptionoverride") 
        }
            )});
        // 
        // sqlInsertCommand county
        // 
        this.sqlInsertCommandCnty.CommandText = "INSERT INTO Counties(CityID, County) VALUES (@CityID, @County)";
        this.sqlInsertCommandCnty.Connection = CommonFunctions.GetConnection();
        this.sqlInsertCommandCnty.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CityID", System.Data.SqlDbType.Int, 4, "CityID"));
        this.sqlInsertCommandCnty.Parameters.Add(new System.Data.SqlClient.SqlParameter("@County", System.Data.SqlDbType.NVarChar, 300, "County"));

        // 
        // sqlSelectCommand county
        // 
        this.sqlSelectCommandCnty.CommandText = "select distinct counties.County as County from stateprovinces inner join cities on cities.stateprovinceid=stateprovinces.id inner join counties on counties.cityid=cities.id where stateprovinces.id=@StateID";
        this.sqlSelectCommandCnty.Connection = CommonFunctions.GetConnection();
        this.sqlSelectCommandCnty.Parameters.Add(new System.Data.SqlClient.SqlParameter("@StateID", System.Data.SqlDbType.Variant));
        // 
        // 
        //********************
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
        // CountiesSet
        // 
        this.CountiesSet.DataSetName = "CountiesDataset";
        this.CountiesSet.Locale = new System.Globalization.CultureInfo("en-US");
        ((System.ComponentModel.ISupportInitialize)(this.RegionsSet)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.CountriesSet)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.CitiesSet)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.StateProvincesSet)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.CountiesSet)).EndInit();
    }
    #endregion
    public void FillCities()
    {

        DBConnection obj = new DBConnection();
        DataTable dt = new DataTable();
        try
        {
            //refill full city list without cities associated with counties
            dt = VADBCommander.CityListByStateProvinceID(StateList.SelectedValue);
            if (dt.Rows.Count > 0)
            {
                CityList.DataSource = dt;
                CityList.DataBind();
            }
            else
            {
                CityList.Items.Clear();
                CityList.DataBind();
            }

            if (dt.Rows.Count > 0)
            {
                if (CityList.SelectedIndex != -1)
                    CityName.Text = CityList.SelectedItem.Text;

                City_Description_override.Text = dt.Rows[0]["descriptionoverride"].ToString();
                City_Title_Override.Text = dt.Rows[0]["titleoverride"].ToString();
            }
            else
            {
                City_Description_override.Text = "";
                City_Title_Override.Text = "";
                CityName.Text = "";
            }
        }
        catch (Exception ex) { lblInfo.Text = ex.Message; }
        finally { obj.CloseConnection(); }
    }
    public void ClearCountyInfo()
    {
        CountyName.Text = "";
        NewCounty.Text = "";
        County_Description_override.Text = "";
        County_Title_override.Text = "";
    }
    public void ClearCountyRel()
    {
        txtCouCtyRename.Text = "";
        txtCouCtyAdd.Text = "";
        txtCouCity_Title.Text = "";
        txtChangeCouCtyDesc.Text = "";
    }
    public void BindCountyRel()
    {
        DBConnection obj = new DBConnection();
        DataTable dt = new DataTable();
        try
        {
            dt =VADBCommander.CitiesByCountyList(CountryList.SelectedValue);

            if (dt.Rows.Count > 0)
            {
                CountyCityList.DataSource = dt;
                CountyCityList.DataTextField = "city";
                CountyCityList.DataValueField = "id";

                CountyCityList.DataBind();
                txtCouCity_Title.Text = dt.Rows[0]["titleoverride"].ToString();
                txtChangeCouCtyDesc.Text = dt.Rows[0]["descriptionoverride"].ToString();
            }
            else
            {
                CountyCityList.Items.Clear();
                CountyCityList.DataBind();

                txtCouCity_Title.Text = "";
                txtChangeCouCtyDesc.Text = "";
            }
        }
        catch (Exception ex) { lblInfo.Text = ex.Message; }
        finally { obj.CloseConnection(); }
    }
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
        CountyCityList.Items.Clear();
        CountryList.DataBind();
        CountyList.Items.Clear();
        CountryList2.DataBind();
        CityList.Items.Clear();
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
        CountyCityList.Items.Clear();
        CountyList.Items.Clear();
        CityList.Items.Clear();
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
        CountyCityList.Items.Clear();
        CountyList.Items.Clear();
        CityList.Items.Clear();
        CitiesSet.Clear();
        ClearCountyInfo();
        ClearCountyRel();
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
        DBConnection obj = new DBConnection();
        DataTable dt = new DataTable();


        try
        {
            if (StateList.SelectedIndex != -1)
            {
                dt = VADBCommander.CountiesByStateList(StateList.SelectedValue);
                if (dt.Rows.Count > 0)
                {
                    CountyList.DataSource = dt;
                    CountyList.DataTextField = "countyname";
                    CountyList.DataValueField = "id";
                    CountyList.DataBind();
                }
            }
        }
        catch (Exception ex) { lblInfo.Text = ex.Message + " 440"; }
        finally { obj.CloseConnection(); }
        CountyCityList.Items.Clear();
        FillCities();
    }

    protected void CityList_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        if (CityList.SelectedIndex > 0)
        {
            if (CityList.SelectedIndex != -1)
            {
                CityName.Text = CityList.SelectedItem.Text;
                ClearCountyInfo();
                ClearCountyRel();
            }
        }
    }

    protected void CountyList_SelectedIndexChanged(object sender, EventArgs e)
    {
        DBConnection obj = new DBConnection();
        DataTable dt = new DataTable();
        ClearCountyRel();

        string query = "";
        try
        {

            //*****associate selected city in full city list to selected county...refill full cities and proceed BindCountyRel
            if (CityList.SelectedValue != "")
            {
                if (CountyList.SelectedIndex != 0)
                {
                    dt = VADBCommander.CountiesByCityList(CityList.SelectedValue.ToString());

                    if (dt.Rows.Count == 0)
                    {
                        VADBCommander.CountyAdd(CityList.SelectedValue, CountyList.SelectedItem.Text, CountyList.SelectedValue);
                        FillCities();
                    }
                    else
                    {
                        VADBCommander.CountyEditByCityID(CountyList.SelectedItem.Text, CountyList.SelectedValue, CityList.SelectedValue);
                        FillCities();
                    }
                }
            }
            BindCountyRel();


            if (CountyList.SelectedIndex != -1)
            {

                dt = VADBCommander.CountyNameInd(CountyList.SelectedValue.ToString());
                if (dt.Rows.Count > 0)
                {
                    County_Title_override.Text = dt.Rows[0]["title"].ToString();
                    County_Description_override.Text = dt.Rows[0]["description"].ToString();
                }
                else
                {
                    County_Title_override.Text = "";
                    County_Description_override.Text = "";
                }

            }

            //refill full city list without cities associated with counties
            dt = VADBCommander.CityListByStateProvinceID(StateList.SelectedValue);
            if (dt.Rows.Count > 0)
            {
                CityList.DataSource = dt;
                CityList.DataBind();
            }
            else
            {
                CityList.Items.Clear();
                CityList.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblInfo.Text += ex.Message + ":525";
        }
        finally { obj.CloseConnection(); }
        //CountyList.SelectedIndex = 0;

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

        DBConnection obj = new DBConnection();
        DataTable dt = new DataTable();
        try
        {
            VADBCommander.CityEditByID(CityName.Text, CityList.SelectedValue);


            dt = VADBCommander.CityByStateList(StateList.SelectedValue);
            if (dt.Rows.Count > 0)
            {
                CityList.DataSource = dt;
                CityList.DataBind();
            }
        }
        catch (Exception ex) { lblInfo.Text = ex.Message; }
        finally { obj.CloseConnection(); }
    }

    protected void CountyRename_Click(object sender, EventArgs e)
    {
        DBConnection obj = new DBConnection();
        
        DataTable dt = new DataTable();

        try
        {
            VADBCommander.CountyNamesEdit(CountyName.Text, CountyList.SelectedValue);
            VADBCommander.CountyEditByCountyID(CountyName.Text, CountyList.SelectedValue);
            dt = VADBCommander.CountiesByStateList(StateList.SelectedValue);
            CountyList.DataSource = dt;
            CountyList.DataTextField = "countyname";
            CountyList.DataValueField = "id";
            CountyList.DataBind();
        }
        catch (Exception ex) { lblInfo.Text = ex.Message; }
        finally { obj.CloseConnection(); }
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
        //add function to other city delete button...see if property exists in city first
        DBConnection obj = new DBConnection();
        DataTable dt = new DataTable();

        try
        {
            bool vExists = obj.spSelectCityPropertyExists(Convert.ToInt32(CityList.SelectedValue));

            if (vExists == false)
            {                
                VADBCommander.CityDelete(CityList.SelectedValue);
                VADBCommander.CountyDeleteByCityID(CityList.SelectedValue);

                dt = VADBCommander.CityListByStateProvinceID(StateList.SelectedValue);
                if (dt.Rows.Count > 0)
                {

                    CityList.DataSource = dt;
                    CityList.DataBind();

                    CityName.Text = CityList.SelectedItem.Text;
                }
                else
                {
                    CityList.Items.Clear();
                    CityList.DataBind();
                }
            }
            else
                lblInfo.Text = "Property exists for city.";
        }
        catch (Exception ex) { lblInfo.Text = ex.Message; }
        finally { obj.CloseConnection(); }
    }

    protected void CountyDelete_Click(object sender, EventArgs e)
    {
        //see if city exist for county...delete if not
        DBConnection obj = new DBConnection();
        string query = "";
        DataTable dt = new DataTable();
        try
        {
            bool vExists = obj.spSelectCountyCityExists(Convert.ToInt32(CountyList.SelectedValue));

            if (vExists == false)
            {
                VADBCommander.CountyNameDelete(CountyList.SelectedValue);
                VADBCommander.CountyDelete(CountyList.SelectedValue);
                dt = VADBCommander.CountiesByStateList(StateList.SelectedValue);
                CountyList.DataSource = dt;
                CountyList.DataTextField = "countyname";
                CountyList.DataValueField = "id";
                CountyList.DataBind();
            }
            else
                lblInfo.Text = "City exists for county";
        }
        catch (Exception ex) { lblInfo.Text = ex.Message; }
        finally { obj.CloseConnection(); }
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

    protected void CityList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (DataRow datarow in CountiesSet.Tables["Counties"].Rows)
            if (datarow.RowState != DataRowState.Deleted)
                if ((int)datarow["ID"] == Convert.ToInt32(CountyList.SelectedValue))
                {
                    datarow["CityID"] = Convert.ToInt32(CityList2.SelectedValue);
                    break;
                }

        //lock (CommonFunctions.Connection)
        CountiesAdapter.Update(CountiesSet);

        Finish();
    }

    protected void RegionAdd_Click(object sender, System.EventArgs e)
    {
        //EnterRegion.Validate();
        InvalidRegion.Validate();

        if (!InvalidRegion.IsValid)
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
        //EnterCountry.Validate();
        InvalidCountry.Validate();

        if (!InvalidCountry.IsValid)
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
        //EnterState.Validate();
        InvalidState.Validate();

        if (!InvalidState.IsValid)
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
        DBConnection obj = new DBConnection();
        DataTable dt = new DataTable();
        try
        {

            //if (dt.Rows.Count == 0)
            //{
            VADBCommander.CityAdd(NewCity.Text, StateList.SelectedValue);

            dt = VADBCommander.CityListByStateProvinceID(StateList.SelectedValue);
            if (dt.Rows.Count > 0)
            {
                CityList.DataSource = dt;
                CityList.DataBind();
            }
            //}
        }
        catch (Exception ex) { lblInfo.Text = ex.Message; }
        finally { obj.CloseConnection(); }
    }

    protected void CountyAdd_Click(object sender, EventArgs e)
    {
        //EnterCounty.Validate();
        InvalidCounty.Validate();

        if (!InvalidCounty.IsValid)
            return;
        DBConnection obj = new DBConnection();
        try
        {
            DataTable dt = VADBCommander.CountiesByStateAndName(NewCountry.Text, StateList.SelectedValue);
            if (dt.Rows.Count == 0)
            {
                VADBCommander.CountyNameAdd(NewCounty.Text.Trim(), StateList.SelectedValue);
                dt = VADBCommander.CountiesByStateList(StateList.SelectedValue); ;
                CountyList.DataSource = dt;
                CountyList.DataTextField = "countyname";
                CountyList.DataValueField = "id";
                CountyList.DataBind();
            }
            else
                lblInfo.Text = "County already exists for state or province";

        }
        catch (Exception ex)
        {
            lblInfo.Text = ex.Message + "585";
        }
    }

    private void Finish()
    {
        NewRegion.Text = "";
        NewCountry.Text = "";
        NewState.Text = "";
        NewCity.Text = "";
        NewCounty.Text = "";
        RegionName.Text = "";
        CountryName.Text = "";
        StateName.Text = "";
        CityName.Text = "";
        CountyName.Text = "";
        int selectedregion = 0;
        int selectedcountry = 0;
        int selectedstate = 0;
        int selectedcity = 0;
        int selectedcounty = 0;

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
        try
        {
            selectedcounty = Convert.ToInt32(CountyList.SelectedValue);
        }
        catch (Exception ex)
        {
            lblInfo.Text = ex.Message + " 988";
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
        if (selectedcounty != 0)
        {
            try
            {
                CountyList.SelectedValue = selectedcounty.ToString();
                CountyList_SelectedIndexChanged(CountyList, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                lblInfo.Text = ex.Message + " 1049";
            }
        }
    }

    protected void BtnChangeCountyTitle_Click(object sender, EventArgs e)
    {
        if (County_Title_override.Text.Length < 1)
            County_Title_override.Text = String.Empty;

        DBConnection obj = new DBConnection();
        try
        {
            VADBCommander.CountyNameEdit(County_Title_override.Text, CountyList.SelectedValue);
        }
        catch (Exception ex) { lblInfo.Text = ex.Message + " 1065"; }
        finally { obj.CloseConnection(); }

        Finish();
    }

    protected void BtnChangeCityTitle_Click(object sender, EventArgs e)
    {
        DBConnection obj = new DBConnection();
        try
        {
            VADBCommander.CityEditByName(City_Title_Override.Text, CityList.SelectedValue);
        }
        catch (Exception ex) { lblInfo.Text = ex.Message; }
        finally { obj.CloseConnection(); }
    }
    protected void BtnChangeStateTitle_Click(object sender, EventArgs e)
    {
        if (State_Title_override.Text.Length < 1)
            State_Title_override.Text = String.Empty;

        foreach (DataRow datarow in StateProvincesSet.Tables["StateProvinces"].Rows)
            if (datarow.RowState != DataRowState.Deleted)
                if ((int)datarow["ID"] == Convert.ToInt32(StateList.SelectedValue))
                {
                    datarow["titleoverride"] = State_Title_override.Text;
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
        DBConnection obj = new DBConnection();

        try
        {
            VADBCommander.CityEdit(City_Description_override.Text, CityList.SelectedValue);
        }
        catch (Exception ex) { lblInfo.Text = ex.Message; }
        finally { obj.CloseConnection(); }

    }

    protected void BtnChangeCountyDescription_Click(object sender, EventArgs e)
    {
        if (County_Description_override.Text.Length < 1)
            County_Description_override.Text = String.Empty;

        DBConnection obj = new DBConnection();
        try
        {
            VADBCommander.CountyDescriptionEditByID(County_Description_override.Text, CountyList.SelectedValue);
        }
        catch (Exception ex) { lblInfo.Text = ex.Message; }
        finally { obj.CloseConnection(); }

        Finish();
    }

    protected void CountyList2_SelectedIndexChanged(object sender, EventArgs e)
    {

    }




    protected void CountyList_DataBound(object sender, EventArgs e)
    {
        CountyList.Items.Insert(0, new ListItem("Select county", "0"));
    }
    protected void CityList_DataBound(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (CityList.SelectedIndex != -1)
                CityName.Text = CityList.SelectedItem.Text;
        }
    }
    protected void btnCountyCityRem_Click(object sender, EventArgs e)
    {
        DBConnection obj = new DBConnection();
        DataTable dt = new DataTable();
        string vID = "";
        try
        {
            dt = VADBCommander.CityByNameAndState(CountyCityList.SelectedItem.Text, StateList.SelectedValue);
            if (dt.Rows.Count > 0)
            {
                vID = dt.Rows[0]["id"].ToString();
            }

            VADBCommander.CountyDeleteByCityAndCountyID(vID, CountyList.SelectedValue.ToString());

            BindCountyRel();

            //refill full city list without cities associated with counties
            dt = VADBCommander.CityListByStateProvinceID(StateList.SelectedValue);
            if (dt.Rows.Count > 0)
            {
                CityList.DataSource = dt;
                CityList.DataBind();
            }
        }
        catch (Exception ex) { lblInfo.Text = ex.Message; }
        finally { obj.CloseConnection(); }
    }
    protected void btnCity_Click(object sender, EventArgs e)
    {
        //rename city in cities table
        DBConnection obj = new DBConnection();
        DataTable dt = new DataTable();
        try
        {
            VADBCommander.CityEditByID(txtCouCtyRename.Text, CountyCityList.SelectedValue);
            BindCountyRel();
        }
        catch (Exception ex) { lblInfo.Text = ex.Message; }
        finally { obj.CloseConnection(); }
    }
    protected void btnCouCtyDelete_Click(object sender, EventArgs e)
    {
        //see if properties exists for city...delete city in cities table
        DBConnection obj = new DBConnection();
        DataTable dt = new DataTable();
        try
        {
            bool vExists = obj.spSelectCityPropertyExists(Convert.ToInt32(CountyCityList.SelectedValue));

            if (vExists == false)
            {
                VADBCommander.CityDelete(CountyCityList.SelectedValue);
                VADBCommander.CountyDeleteByCityID(CityList.SelectedValue);
                BindCountyRel();
            }
            else
                lblInfo.Text = "Property exists for city.";
        }
        catch (Exception ex) { lblInfo.Text = ex.Message; }
        finally { obj.CloseConnection(); }
    }
    protected void btnChangeCouCtyTitle_Click(object sender, EventArgs e)
    {
        //update city title in cities table
        DBConnection obj = new DBConnection();

        try
        {
            VADBCommander.CityEditByName(txtCouCtyAdd.Text, CountyCityList.SelectedValue);
        }
        catch (Exception ex) { lblInfo.Text = ex.Message; }
        finally { obj.CloseConnection(); }
    }
    protected void btnCouCtyChangeDesc_Click(object sender, EventArgs e)
    {
        //update desc in cities
        DBConnection obj = new DBConnection();

        try
        {
            VADBCommander.CityEdit(txtChangeCouCtyDesc.Text, CountyCityList.SelectedValue);
        }
        catch (Exception ex) { lblInfo.Text = ex.Message; }
        finally { obj.CloseConnection(); }
    }
    protected void CountyCityList_DataBound(object sender, EventArgs e)
    {
        //set title and desc boxes
        DBConnection obj = new DBConnection();
        DataTable dt = new DataTable();

        try
        {
            

        }
        catch (Exception ex) { lblInfo.Text = ex.Message; }
        finally { obj.CloseConnection(); }
    }
    protected void StateList_DataBound(object sender, EventArgs e)
    {

    }
    protected void btnCityCouAdd_Click(object sender, EventArgs e)
    {
        DBConnection obj = new DBConnection();
        DataTable dt = new DataTable();
        string query = "";
        try
        {
            VADBCommander.CityAdd(txtCouCtyAdd.Text, StateList.SelectedValue);

            dt = VADBCommander.CitiesByName(txtCouCtyAdd.Text);
            if (dt.Rows.Count > 0)
            {

                VADBCommander.CountyAdd(dt.Rows[0]["id"].ToString(), CountyList.SelectedItem.Text, CountyList.SelectedValue);
            }
            BindCountyRel();
            //}
            //else
            //    lblInfo.Text = "City already exists";
        }
        catch (Exception ex) { lblInfo.Text = ex.Message; }
        finally { obj.CloseConnection(); }
    }
}
