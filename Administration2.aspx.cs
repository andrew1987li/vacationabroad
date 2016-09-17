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

public partial class Administration : AdminPage
{
    protected System.Data.SqlClient.SqlDataAdapter GetUserNameAdapter;
    protected Vacations.GetUserNameDataset GetUserNameSet;
    protected System.Data.SqlClient.SqlDataAdapter PropertiesAdapter;
    protected Vacations.PropertiesDataset PropertiesSet;
    protected System.Data.SqlClient.SqlCommand sqlSelectCommand2;
    protected System.Data.SqlClient.SqlCommand sqlSelectCommand1;
    protected System.Data.SqlClient.SqlCommand sqlSelectCommand3;
    //protected System.Data.SqlClient.SqlConnection Connection;

    protected void Page_Load(object sender, System.EventArgs e)
    {
        userid = AuthenticationManager.UserID;

        //string connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        //CommonFunctions.Connection.ConnectionString = connectionstring;
        using (SqlConnection connection = CommonFunctions.GetConnection())
        {
            connection.Open();
            GetUserNameAdapter.SelectCommand.Parameters["@UserID"].Value = userid;

            //lock(CommonFunctions.Connection)
            GetUserNameAdapter.Fill(GetUserNameSet);

            //lock(CommonFunctions.Connection)
            PropertiesAdapter.Fill(PropertiesSet);

            DataBind();
            connection.Close();
        }
        WelcomeLabel.Text = "Welcome ";
        if ((GetUserNameSet.Tables["Users"].Rows[0]["FirstName"] is string) &&
            (((string)GetUserNameSet.Tables["Users"].Rows[0]["FirstName"]).Length > 0))
            WelcomeLabel.Text += (string)GetUserNameSet.Tables["Users"].Rows[0]["FirstName"] + " ";
        if ((GetUserNameSet.Tables["Users"].Rows[0]["LastName"] is string) &&
            (((string)GetUserNameSet.Tables["Users"].Rows[0]["LastName"]).Length > 0))
            WelcomeLabel.Text += (string)GetUserNameSet.Tables["Users"].Rows[0]["LastName"];
        if ((GetUserNameSet.Tables["Users"].Rows[0]["CompanyName"] is string) &&
            (((string)GetUserNameSet.Tables["Users"].Rows[0]["CompanyName"]).Length > 0))
            WelcomeLabel.Text += " from " + (string)GetUserNameSet.Tables["Users"].Rows[0]["CompanyName"];

        OwnerInformationLink.NavigateUrl = CommonFunctions.PrepareURL("OwnerInformation.aspx?UserID=" + userid.ToString(), "Administration");
    
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
        this.GetUserNameAdapter = new System.Data.SqlClient.SqlDataAdapter();
        this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
        this.GetUserNameSet = new Vacations.GetUserNameDataset();
        this.PropertiesAdapter = new System.Data.SqlClient.SqlDataAdapter();
        this.sqlSelectCommand2 = new System.Data.SqlClient.SqlCommand();
        this.PropertiesSet = new Vacations.PropertiesDataset();
        this.sqlSelectCommand3 = new System.Data.SqlClient.SqlCommand();
        ((System.ComponentModel.ISupportInitialize)(this.GetUserNameSet)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.PropertiesSet)).BeginInit();
        // 
        // CommonFunctions.Connection
        // 
        //CommonFunctions.Connection.ConnectionString = "workstation id=MAIN;packet size=4096;integrated security=SSPI;data source=MAIN;pe" +
        //"rsist security info=False;initial catalog=Vacations";
        // 
        // GetUserNameAdapter
        // 
        this.GetUserNameAdapter.SelectCommand = this.sqlSelectCommand1;
        this.GetUserNameAdapter.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																									 new System.Data.Common.DataTableMapping("Table", "Users", new System.Data.Common.DataColumnMapping[] {
																																																			  new System.Data.Common.DataColumnMapping("ID", "ID"),
																																																			  new System.Data.Common.DataColumnMapping("FirstName", "FirstName"),
																																																			  new System.Data.Common.DataColumnMapping("LastName", "LastName"),
																																																			  new System.Data.Common.DataColumnMapping("CompanyName", "CompanyName")})});
        // 
        // sqlSelectCommand1
        // 
        this.sqlSelectCommand1.CommandText = "SELECT ID, FirstName, LastName, ISNULL(CompanyName, \'\') AS CompanyName FROM Users" +
            " WHERE (ID = @UserID)";
        this.sqlSelectCommand1.Connection = CommonFunctions.GetConnection();
        this.sqlSelectCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserID", System.Data.SqlDbType.Int, 4, "ID"));
        // 
        // GetUserNameSet
        // 
        this.GetUserNameSet.DataSetName = "GetUserNameDataset";
        this.GetUserNameSet.Locale = new System.Globalization.CultureInfo("en-US");
        // 
        // PropertiesAdapter
        // 
        this.PropertiesAdapter.SelectCommand = this.sqlSelectCommand3;
        this.PropertiesAdapter.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																									new System.Data.Common.DataTableMapping("Table", "Properties", new System.Data.Common.DataColumnMapping[] {
																																																				  new System.Data.Common.DataColumnMapping("ID", "ID"),
																																																				  new System.Data.Common.DataColumnMapping("UserID", "UserID"),
																																																				  new System.Data.Common.DataColumnMapping("Name", "Name"),
																																																				  new System.Data.Common.DataColumnMapping("TypeID", "TypeID"),
																																																				  new System.Data.Common.DataColumnMapping("Address", "Address"),
																																																				  new System.Data.Common.DataColumnMapping("IfShowAddress", "IfShowAddress"),
																																																				  new System.Data.Common.DataColumnMapping("NumBedrooms", "NumBedrooms"),
																																																				  new System.Data.Common.DataColumnMapping("NumBaths", "NumBaths"),
																																																				  new System.Data.Common.DataColumnMapping("NumSleeps", "NumSleeps"),
																																																				  new System.Data.Common.DataColumnMapping("MinimumNightlyRentalID", "MinimumNightlyRentalID"),
																																																				  new System.Data.Common.DataColumnMapping("NumTVs", "NumTVs"),
																																																				  new System.Data.Common.DataColumnMapping("NumVCRs", "NumVCRs"),
																																																				  new System.Data.Common.DataColumnMapping("NumCDPlayers", "NumCDPlayers"),
																																																				  new System.Data.Common.DataColumnMapping("Description", "Description"),
																																																				  new System.Data.Common.DataColumnMapping("Amenities", "Amenities"),
																																																				  new System.Data.Common.DataColumnMapping("LocalAttractions", "LocalAttractions"),
																																																				  new System.Data.Common.DataColumnMapping("Rates", "Rates"),
																																																				  new System.Data.Common.DataColumnMapping("CancellationPolicy", "CancellationPolicy"),
																																																				  new System.Data.Common.DataColumnMapping("DepositRequired", "DepositRequired"),
																																																				  new System.Data.Common.DataColumnMapping("IfMoreThan7PhotosAllowed", "IfMoreThan7PhotosAllowed"),
																																																				  new System.Data.Common.DataColumnMapping("IfApproved", "IfApproved"),
																																																				  new System.Data.Common.DataColumnMapping("CityID", "CityID"),
																																																				  new System.Data.Common.DataColumnMapping("IfFinished", "IfFinished"),
																																																				  new System.Data.Common.DataColumnMapping("DateAdded", "DateAdded"),
																																																				  new System.Data.Common.DataColumnMapping("DateStartViewed", "DateStartViewed"),
																																																				  new System.Data.Common.DataColumnMapping("VirtualTour", "VirtualTour"),
																																																				  new System.Data.Common.DataColumnMapping("RatesTable", "RatesTable"),
																																																				  new System.Data.Common.DataColumnMapping("PricesCurrency", "PricesCurrency"),
																																																				  new System.Data.Common.DataColumnMapping("CheckIn", "CheckIn"),
																																																				  new System.Data.Common.DataColumnMapping("CheckOut", "CheckOut"),
																																																				  new System.Data.Common.DataColumnMapping("LodgingTax", "LodgingTax"),
																																																				  new System.Data.Common.DataColumnMapping("TaxIncluded", "TaxIncluded"),
																																																				  new System.Data.Common.DataColumnMapping("DateAvailable", "DateAvailable"),
																																																				  new System.Data.Common.DataColumnMapping("IfDiscounted", "IfDiscounted"),
																																																				  new System.Data.Common.DataColumnMapping("IfLastMinuteCancellations", "IfLastMinuteCancellations"),
																																																				  new System.Data.Common.DataColumnMapping("LastMinuteComments", "LastMinuteComments"),
																																																				  new System.Data.Common.DataColumnMapping("HomeExchangeCityID1", "HomeExchangeCityID1"),
																																																				  new System.Data.Common.DataColumnMapping("HomeExchangeCityID2", "HomeExchangeCityID2"),
																																																				  new System.Data.Common.DataColumnMapping("HomeExchangeCityID3", "HomeExchangeCityID3")})});
        // 
        // PropertiesSet
        // 
        this.PropertiesSet.DataSetName = "PropertiesDataset";
        this.PropertiesSet.Locale = new System.Globalization.CultureInfo("en-US");
        // 
        // sqlSelectCommand3
        // 
        this.sqlSelectCommand3.CommandText = @"SELECT ID, UserID, Name, TypeID, Address, IfShowAddress, NumBedrooms, NumBaths, NumSleeps, MinimumNightlyRentalID, NumTVs, NumVCRs, NumCDPlayers, Description, Amenities, LocalAttractions, Rates, CancellationPolicy, DepositRequired, IfMoreThan7PhotosAllowed, IfApproved, CityID, IfFinished, DateAdded, DateStartViewed, VirtualTour, RatesTable, PricesCurrency, CheckIn, CheckOut, LodgingTax, TaxIncluded, DateAvailable, IfDiscounted, IfLastMinuteCancellations, LastMinuteComments, HomeExchangeCityID1, HomeExchangeCityID2, HomeExchangeCityID3 FROM Properties WHERE (IfFinished = 1) AND (IfApproved = 0)";
        this.sqlSelectCommand3.Connection = CommonFunctions.GetConnection();
        ((System.ComponentModel.ISupportInitialize)(this.GetUserNameSet)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.PropertiesSet)).EndInit();

    }
    #endregion

    protected void ViewProperty_Click(object sender, System.EventArgs e)
    {
        int propertyid;

        try
        {
            propertyid = Convert.ToInt32(PropertyNumber.Text);
        }
        catch (Exception)
        {
            return;
        }

        Response.Redirect(CommonFunctions.PrepareURL("ViewProperty.aspx?UserID=" + AuthenticationManager.UserID.ToString() + "&PropertyID=" + propertyid.ToString(), "Administration"), true);
    }

    protected void EditTextButton_Click(object sender, System.EventArgs e)
    {
        int propertyid;

        try
        {
            propertyid = Convert.ToInt32(PropertyNumber.Text);
        }
        catch (Exception)
        {
            return;
        }

        Response.Redirect(CommonFunctions.PrepareURL("EditProperty.aspx?UserID=" + AuthenticationManager.UserID.ToString() + "&PropertyID=" + propertyid.ToString(), "Administration"), true);
    }

    protected void EditPhotosButton_Click(object sender, System.EventArgs e)
    {
        int propertyid;

        try
        {
            propertyid = Convert.ToInt32(PropertyNumber.Text);
        }
        catch (Exception)
        {
            return;
        }

        Response.Redirect(CommonFunctions.PrepareURL("PropertyPhotos.aspx?UserID=" + AuthenticationManager.UserID.ToString() + "&PropertyID=" + propertyid.ToString(), "Administration"), true);
    }
    /*
        private void DeleteButton_Click(object sender, System.EventArgs e)
        {
            int propertyid;

            try
            {
                propertyid = Convert.ToInt32 (PropertyNumber.Text);
            }
            catch (Exception)
            {
                return;
            }

            Response.Redirect (CommonFunctions.PrepareURL ("DeleteProperty.aspx?UserID=" + AuthenticationManager.UserID.ToString () + "&PropertyID=" + propertyid.ToString (), "Administration"), true);
        }
    */
    protected void EmailsButton_Click(object sender, System.EventArgs e)
    {
        int propertyid;

        try
        {
            propertyid = Convert.ToInt32(PropertyNumber.Text);
        }
        catch (Exception)
        {
            return;
        }

        Response.Redirect(CommonFunctions.PrepareURL("ViewEmails.aspx?UserID=" + AuthenticationManager.UserID.ToString() + "&PropertyID=" + propertyid.ToString(), "Administration"), true);
    }

    protected void OwnerButton_Click(object sender, System.EventArgs e)
    {
        object userid = null;
        using (SqlConnection connection = CommonFunctions.GetConnection())
        {
            connection.Open();
            SqlCommand getuserid = new SqlCommand("SELECT UserID FROM Properties WHERE ID = @PropertyID", connection);
            getuserid.Parameters.Add("@PropertyID", SqlDbType.Int, 4, "PropertyID");
            int propertyid;

            try
            {
                propertyid = Convert.ToInt32(PropertyNumber.Text);
            }
            catch (Exception)
            {
                return;
            }

            //if (CommonFunctions.Connection.State == System.Data.ConnectionState.Closed)
            //CommonFunctions.Connection.Open ();

            getuserid.Parameters["@PropertyID"].Value = propertyid;
            userid = getuserid.ExecuteScalar();
            connection.Close();
        }
        //CommonFunctions.Connection.Close ();

        if (userid is int)
            Response.Redirect(CommonFunctions.PrepareURL("MyAccount.aspx?UserID=" + ((int)userid).ToString(), "Administration"), true);
    }

    protected void ChangeDate_Click(object sender, System.EventArgs e)
    {
        using (SqlConnection connection = CommonFunctions.GetConnection())
        {
            SqlCommand updatecommand = new SqlCommand("UPDATE Properties SET DateAdded = @DateAdded WHERE ID = @PropertyID", connection);
            updatecommand.Parameters.Add("@DateAdded", SqlDbType.SmallDateTime, 4, "DateAdded");
            updatecommand.Parameters.Add("@PropertyID", SqlDbType.Int, 4, "PropertyID");
            int propertyid;
            DateTime date;

            DateUpdateError.Visible = false;
            UserIDError.Visible = false;
            PropertyIDError.Visible = false;

            try
            {
                propertyid = Convert.ToInt32(PropertyNumber.Text);
            }
            catch (Exception)
            {
                DateUpdateError.Visible = true;
                DateUpdateError.Text = "Please enter valid property ID";
                return;
            }

            try
            {
                date = Convert.ToDateTime(NewDate.Text);
            }
            catch (Exception)
            {
                DateUpdateError.Visible = true;
                DateUpdateError.Text = "Please enter valid new date";
                return;
            }

            //if (CommonFunctions.Connection.State == System.Data.ConnectionState.Closed)
            //CommonFunctions.Connection.Open ();

            updatecommand.Parameters["@DateAdded"].Value = date;
            updatecommand.Parameters["@PropertyID"].Value = propertyid;
            updatecommand.ExecuteNonQuery();

            PropertyNumber.Text = "";
            NewDate.Text = "";

            //CommonFunctions.Connection.Close ();
            connection.Close();
        }
    }

    protected void ChangeUserDate_Click(object sender, EventArgs e)
    {
        using (SqlConnection connection = CommonFunctions.GetConnection())
        {
            connection.Open();
            SqlCommand updatecommand = new SqlCommand("UPDATE Users SET DateCreated = @DateCreated WHERE ID = @UserID",
                connection);
            updatecommand.Parameters.Add("@DateCreated", SqlDbType.SmallDateTime, 4, "DateCreated");
            updatecommand.Parameters.Add("@UserID", SqlDbType.Int, 4, "UserID");

            int userid;
            DateTime date;

            DateUpdateError.Visible = false;
            UserIDError.Visible = false;
            PropertyIDError.Visible = false;

            try
            {
                userid = Convert.ToInt32(UserID.Text);
            }
            catch (Exception)
            {
                UserIDError.Visible = true;
                UserIDError.Text = "Please enter valid user ID";
                return;
            }

            try
            {
                date = Convert.ToDateTime(NewUserDate.Text);
            }
            catch (Exception)
            {
                UserIDError.Visible = true;
                UserIDError.Text = "Please enter valid new date";
                return;
            }

            //if (CommonFunctions.Connection.State == System.Data.ConnectionState.Closed)
            //CommonFunctions.Connection.Open ();

            updatecommand.Parameters["@DateCreated"].Value = date;
            updatecommand.Parameters["@UserID"].Value = userid;
            updatecommand.ExecuteNonQuery();

            UserID.Text = "";
            NewUserDate.Text = "";
            connection.Close();
        }
    }

    protected void CreateInvoice_Click(object sender, EventArgs e)
    {
        string strPropertyID = PropertyID.Text;
        string strInvoiceAmount = InvoiceAmount.Text;
        int iQuantity;
        decimal decQuantity;
        bool isProperPropertyID = int.TryParse(strPropertyID, out iQuantity);
        bool isProperInvoiceAmount = decimal.TryParse(strInvoiceAmount, out decQuantity);


        DateUpdateError.Visible = false;
        UserIDError.Visible = false;
        PropertyIDError.Visible = false;

        if (isProperPropertyID && isProperInvoiceAmount)
        {
            VADBCommander.InvoiceAdd(strPropertyID, strInvoiceAmount);
            PropertyID.Text = "";
            InvoiceAmount.Text = "";
        }
        else if (!isProperPropertyID)
        {
            PropertyIDError.Visible = true;
            PropertyIDError.Text = "Please enter valid property ID";
        }
        else if (!isProperInvoiceAmount)
        {
            PropertyIDError.Visible = true;
            PropertyIDError.Text = "Please enter valid invoice amount";
        }
    }
    protected void BtnFindEmail_Click(object sender, EventArgs e)
    {
        object FindID = null;
        string emailadrtxt = EmailAddressSearch.Text.ToString();
        //Response.Write(emailadrtxt);
        using (SqlConnection connection = CommonFunctions.GetConnection())
        {
            connection.Open();
            using (SqlCommand getuserid = new SqlCommand("SELECT ID FROM Users WHERE Email = @Emailadr", connection))
            {
                //getuserid.Parameters.Add("@Emailadr", SqlDbType.Int, 4, "PropertyID");
                getuserid.Parameters.Add("@Emailadr", SqlDbType.NVarChar);
                getuserid.Parameters["@Emailadr"].Value = emailadrtxt;

                FindID = getuserid.ExecuteScalar();

                // useridtext.Text = reader["ID"].ToString();

            }

            connection.Close();
        }


        if (FindID is int)
            Response.Redirect(CommonFunctions.PrepareURL("MyAccount.aspx?UserID=" + ((int)FindID).ToString(), "Administration"), true);
        //EmailAdrError.Text = FindID.ToString();
        else
        {
            EmailAdrError.Text = "Email address not found.";
            EmailAdrError.Visible = true;
        }

        ;

    }
}
