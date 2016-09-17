using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Configuration;
using System.Net.Mail;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class SendEmail : CommonPage
{
    protected Vacations.PhotosDataset PhotosSet;
    protected Vacations.PropertiesFullDataset PropertiesFullSet;
    protected System.Data.SqlClient.SqlDataAdapter PropertiesAdapter;
    protected System.Data.SqlClient.SqlDataAdapter PhotosAdapter;
    protected System.Data.SqlClient.SqlCommand sqlSelectCommand2;
    protected System.Data.SqlClient.SqlDataAdapter EmailsAdapter;
    protected Vacations.EmailsDataset EmailsSet;
    protected System.Data.SqlClient.SqlCommand sqlSelectCommand3;
    protected System.Data.SqlClient.SqlCommand sqlSelectCommand1;
    //protected System.Data.SqlClient.SqlConnection Connection;

    protected void Page_Load(object sender, System.EventArgs e)
    {
        System.Data.SqlClient.SqlCommandBuilder builder = new System.Data.SqlClient.SqlCommandBuilder(EmailsAdapter);

        PropertiesAdapter.SelectCommand.Parameters["@PropertyID"].Value = propertyid;
        PhotosAdapter.SelectCommand.Parameters["@PropertyID"].Value = propertyid;

        //lock (CommonFunctions.Connection)
        if (PropertiesAdapter.Fill(PropertiesFullSet) < 1)
            Response.Redirect(backlinkurl);
        //lock (CommonFunctions.Connection)
        PhotosAdapter.Fill(PhotosSet);

        if (!(bool)PropertiesFullSet.Tables["Properties"].Rows[0]["IfApproved"] &&
                (!AuthenticationManager.IfAuthenticated ||
                ((AuthenticationManager.UserID != (int)PropertiesFullSet.Tables["Properties"].Rows[0]["UserID"]) &&
                !AuthenticationManager.IfAdmin)))
            Response.Redirect(backlinkurl, true);

        BackLink.NavigateUrl = CommonFunctions.PrepareURL("ViewProperty.aspx?PropertyID=" + propertyid.ToString(),
            backlinktext);

        if (!IsPostBack)
            DataBind();

        //new link menu
        DBConnection obj = new DBConnection();
        try
        {
            string vNum = propertyid.ToString();

            //get city, state, country using property number

            DataTable dt = VADBCommander.CityStatePropertyInd(vNum);
            if (dt.Rows.Count > 0)
            {
                string vCity = dt.Rows[0]["city"].ToString();
                vNum = dt.Rows[0]["state"].ToString();

                dt = VADBCommander.StateProvinceNamedInd(vNum); 
                string vState = dt.Rows[0]["state"].ToString();
                vNum = dt.Rows[0]["country"].ToString();

                dt = VADBCommander.CountryInd(vNum);
                string vCountry = dt.Rows[0]["country"].ToString();

                lnkCountry.Text = vCountry;
                lnkCountry.NavigateUrl = CommonFunctions.PrepareURL(vCountry + "/default.aspx");

                lnkState.Text = vState;
                lnkState.NavigateUrl = CommonFunctions.PrepareURL(vCountry + "/" + vState + "/default.aspx");

                lnkCity.Text = vCity;
                lnkCity.NavigateUrl = CommonFunctions.PrepareURL(vCountry + "/" + vState + "/" + vCity + "/default.aspx");

                lnkProperty.Text = "Property #" + propertyid.ToString();
                lnkProperty.NavigateUrl = CommonFunctions.PrepareURL(vCountry + "/" + vState + "/" + vCity + "/" + propertyid + "/default.aspx");

                Session["emailReturnURL"] = CommonFunctions.PrepareURL(vCountry + "/" + vState + "/" + vCity + "/default.aspx");                
            }


        }
        catch (Exception ex) { Response.Write("Error with response"); }
        finally { obj.CloseConnection(); }
    }

    public bool IfShowContactInfo()
    {
        return ((int)PropertiesFullSet.Tables["Properties"].Rows[0]["IfPaid"] == 1) ||
            ((DateTime.Now - (DateTime)PropertiesFullSet.Tables["Properties"].Rows[0]["DateStartViewed"]).TotalDays <= 120);
    }

    public bool EmailPresent()
    {
        System.Text.RegularExpressions.Regex regex =
            new System.Text.RegularExpressions.Regex("^\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$");

        return (PropertiesFullSet.Tables["Properties"].Rows[0]["Email"] is string) &&
            regex.Match((string)PropertiesFullSet.Tables["Properties"].Rows[0]["Email"]).Success;
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
        this.PhotosSet = new Vacations.PhotosDataset();
        this.PropertiesFullSet = new Vacations.PropertiesFullDataset();
        this.PropertiesAdapter = new System.Data.SqlClient.SqlDataAdapter();
        this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
        this.PhotosAdapter = new System.Data.SqlClient.SqlDataAdapter();
        this.sqlSelectCommand2 = new System.Data.SqlClient.SqlCommand();
        this.EmailsAdapter = new System.Data.SqlClient.SqlDataAdapter();
        this.sqlSelectCommand3 = new System.Data.SqlClient.SqlCommand();
        this.EmailsSet = new Vacations.EmailsDataset();
        ((System.ComponentModel.ISupportInitialize)(this.PhotosSet)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.PropertiesFullSet)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.EmailsSet)).BeginInit();
        // 
        // CommonFunctions.Connection
        // 
        //CommonFunctions.Connection.ConnectionString = "workstation id=MAIN;packet size=4096;integrated security=SSPI;data source=MAIN;pe" +
        //"rsist security info=False;initial catalog=Vacations";
        // 
        // PhotosSet
        // 
        this.PhotosSet.DataSetName = "PhotosDataset";
        this.PhotosSet.Locale = new System.Globalization.CultureInfo("en-US");
        // 
        // PropertiesFullSet
        // 
        this.PropertiesFullSet.DataSetName = "PropertiesFullDataset";
        this.PropertiesFullSet.Locale = new System.Globalization.CultureInfo("en-US");
        // 
        // PropertiesAdapter
        // 
        this.PropertiesAdapter.SelectCommand = this.sqlSelectCommand1;
        this.PropertiesAdapter.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																									new System.Data.Common.DataTableMapping("Table", "Properties", new System.Data.Common.DataColumnMapping[] {
																																																				  new System.Data.Common.DataColumnMapping("MinimumNightlyRental", "MinimumNightlyRental"),
																																																				  new System.Data.Common.DataColumnMapping("Type", "Type"),
																																																				  new System.Data.Common.DataColumnMapping("FirstName", "FirstName"),
																																																				  new System.Data.Common.DataColumnMapping("LastName", "LastName"),
																																																				  new System.Data.Common.DataColumnMapping("PrimaryTelephone", "PrimaryTelephone"),
																																																				  new System.Data.Common.DataColumnMapping("OwnerCountry", "OwnerCountry"),
																																																				  new System.Data.Common.DataColumnMapping("Email", "Email"),
																																																				  new System.Data.Common.DataColumnMapping("Username", "Username"),
																																																				  new System.Data.Common.DataColumnMapping("OwnerAddress", "OwnerAddress"),
																																																				  new System.Data.Common.DataColumnMapping("EveningTelephone", "EveningTelephone"),
																																																				  new System.Data.Common.DataColumnMapping("DaytimeTelephone", "DaytimeTelephone"),
																																																				  new System.Data.Common.DataColumnMapping("MobileTelephone", "MobileTelephone"),
																																																				  new System.Data.Common.DataColumnMapping("Website", "Website"),
																																																				  new System.Data.Common.DataColumnMapping("OwnerCity", "OwnerCity"),
																																																				  new System.Data.Common.DataColumnMapping("OwnerState", "OwnerState"),
																																																				  new System.Data.Common.DataColumnMapping("OwnerZip", "OwnerZip"),
																																																				  new System.Data.Common.DataColumnMapping("Registered", "Registered"),
																																																				  new System.Data.Common.DataColumnMapping("IfPayTravelAgents", "IfPayTravelAgents"),
																																																				  new System.Data.Common.DataColumnMapping("City", "City"),
																																																				  new System.Data.Common.DataColumnMapping("StateProvince", "StateProvince"),
																																																				  new System.Data.Common.DataColumnMapping("Country", "Country"),
																																																				  new System.Data.Common.DataColumnMapping("Region", "Region"),
																																																				  new System.Data.Common.DataColumnMapping("Smoking", "Smoking"),
																																																				  new System.Data.Common.DataColumnMapping("PetFriendly", "PetFriendly"),
																																																				  new System.Data.Common.DataColumnMapping("ID", "ID"),
																																																				  new System.Data.Common.DataColumnMapping("UserID", "UserID"),
																																																				  new System.Data.Common.DataColumnMapping("Name", "Name"),
																																																				  new System.Data.Common.DataColumnMapping("TypeID", "TypeID"),
																																																				  new System.Data.Common.DataColumnMapping("Address", "Address"),
																																																				  new System.Data.Common.DataColumnMapping("CityID", "CityID"),
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
																																																				  new System.Data.Common.DataColumnMapping("IfFinished", "IfFinished"),
																																																				  new System.Data.Common.DataColumnMapping("IfApproved", "IfApproved"),
																																																				  new System.Data.Common.DataColumnMapping("IfPaid", "IfPaid"),
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
																																																				  new System.Data.Common.DataColumnMapping("HomeExchangeCityID1", "HomeExchangeCityID1"),
																																																				  new System.Data.Common.DataColumnMapping("HomeExchangeCityID2", "HomeExchangeCityID2"),
																																																				  new System.Data.Common.DataColumnMapping("HomeExchangeCityID3", "HomeExchangeCityID3")})});
        // 
        // sqlSelectCommand1
        // 
        this.sqlSelectCommand1.CommandText = "SELECT MinimumNightlyRentalTypes.Name AS MinimumNightlyRental, PropertyTypes.Name" +
            " AS Type, Users.FirstName, Users.LastName, Users.PrimaryTelephone, Users.Country" +
            " AS OwnerCountry, Users.Email, Users.Username, Users.Address AS OwnerAddress, Us" +
            "ers.EveningTelephone, Users.DaytimeTelephone, Users.MobileTelephone, Users.Websi" +
            "te, Users.City AS OwnerCity, Users.State AS OwnerState, Users.Zip AS OwnerZip, U" +
            "sers.Registered, Users.IfPayTravelAgents, Users.TouristBoard, Cities.City, StateProvinces.StateProvi" +
            "nce, Countries.Country, Regions.Region, CASE WHEN EXISTS (SELECT * FROM Properti" +
            "esAmenities INNER JOIN Amenities ON PropertiesAmenities.AmenityID = Amenities.ID" +
            " WHERE (PropertiesAmenities.PropertyID = Properties.ID) AND (Amenities.Amenity =" +
            " \'Smoking Permitted\')) THEN \'Yes\' ELSE \'No\' END AS Smoking, CASE WHEN EXISTS (SE" +
            "LECT * FROM PropertiesAmenities INNER JOIN Amenities ON PropertiesAmenities.Amen" +
            "ityID = Amenities.ID WHERE (PropertiesAmenities.PropertyID = Properties.ID) AND " +
            "(Amenities.Amenity = \'Pet Friendly\')) THEN \'Yes\' ELSE \'No\' END AS PetFriendly, P" +
            "roperties.ID, Properties.UserID, Properties.Name, Properties.TypeID, Properties." +
            "Address, Properties.CityID, Properties.IfShowAddress, Properties.NumBedrooms, Pr" +
            "operties.NumBaths, Properties.NumSleeps, Properties.MinimumNightlyRentalID, Prop" +
            "erties.NumTVs, Properties.NumVCRs, Properties.NumCDPlayers, Properties.IfMoreTha" +
            "n7PhotosAllowed, Properties.IfFinished, Properties.IfApproved, CASE WHEN EXISTS" +
            " (SELECT * FROM Invoices WHERE (Invoices.PropertyID = Properties.ID) AND (invoices.invoiceamount>0) AND" +
            " (PaymentAmount >= InvoiceAmount) AND (GETDATE() <= RenewalDate)) THEN 1 ELSE 0 END" +
            " AS IfPaid, Properties.DateAdded, Properties.DateStartViewed," +
            " Properties.VirtualTour, Properties.RatesTable, Properties.PricesCurrency, Prope" +
            "rties.CheckIn, Properties.CheckOut, Properties.LodgingTax, Properties.TaxInclude" +
            "d, Properties.DateAvailable, Properties.IfDiscounted, Properties.IfLastMinuteCan" +
            "cellations, Properties.HomeExchangeCityID1, Properties.HomeExchangeCityID2, Prop" +
            "erties.HomeExchangeCityID3 FROM Properties INNER JOIN Cities ON Properties.CityI" +
            "D = Cities.ID INNER JOIN StateProvinces ON StateProvinces.ID = Cities.StateProvi" +
            "nceID INNER JOIN Countries ON StateProvinces.CountryID = Countries.ID INNER JOIN" +
            " Regions ON Countries.RegionID = Regions.ID INNER JOIN Users ON Properties.UserI" +
            "D = Users.ID LEFT OUTER JOIN MinimumNightlyRentalTypes ON Properties.MinimumNigh" +
            "tlyRentalID = MinimumNightlyRentalTypes.ID LEFT OUTER JOIN PropertyTypes ON Prop" +
            "erties.TypeID = PropertyTypes.ID WHERE (Properties.IfFinished = 1) AND (Properti" +
            "es.IfApproved = 1) AND (Properties.ID = @PropertyID)";
        this.sqlSelectCommand1.Connection = CommonFunctions.GetConnection();
        this.sqlSelectCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PropertyID", System.Data.SqlDbType.Int, 4, "ID"));
        // 
        // PhotosAdapter
        // 
        this.PhotosAdapter.SelectCommand = this.sqlSelectCommand2;
        this.PhotosAdapter.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																								new System.Data.Common.DataTableMapping("Table", "PropertyPhotos", new System.Data.Common.DataColumnMapping[] {
																																																				  new System.Data.Common.DataColumnMapping("ID", "ID"),
																																																				  new System.Data.Common.DataColumnMapping("PropertyID", "PropertyID"),
																																																				  new System.Data.Common.DataColumnMapping("FileName", "FileName"),
																																																				  new System.Data.Common.DataColumnMapping("OrderNumber", "OrderNumber"),
																																																				  new System.Data.Common.DataColumnMapping("Width", "Width"),
																																																				  new System.Data.Common.DataColumnMapping("Height", "Height")})});
        // 
        // sqlSelectCommand2
        // 
        this.sqlSelectCommand2.CommandText = "SELECT TOP 1 ID, PropertyID, FileName, OrderNumber, Width, Height FROM PropertyPh" +
            "otos WHERE (PropertyID = @PropertyID) ORDER BY OrderNumber";
        this.sqlSelectCommand2.Connection = CommonFunctions.GetConnection();
        this.sqlSelectCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PropertyID", System.Data.SqlDbType.Int, 4, "PropertyID"));
        // 
        // EmailsAdapter
        // 
        this.EmailsAdapter.SelectCommand = this.sqlSelectCommand3;
        this.EmailsAdapter.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																								new System.Data.Common.DataTableMapping("Table", "Emails", new System.Data.Common.DataColumnMapping[] {
																																																		  new System.Data.Common.DataColumnMapping("ID", "ID"),
																																																		  new System.Data.Common.DataColumnMapping("PropertyID", "PropertyID"),
																																																		  new System.Data.Common.DataColumnMapping("DateTime", "DateTime"),
																																																		  new System.Data.Common.DataColumnMapping("ContactName", "ContactName"),
																																																		  new System.Data.Common.DataColumnMapping("ContactEmail", "ContactEmail"),
																																																		  new System.Data.Common.DataColumnMapping("ContactTelephone", "ContactTelephone"),
																																																		  new System.Data.Common.DataColumnMapping("ArrivalDate", "ArrivalDate"),
																																																		  new System.Data.Common.DataColumnMapping("DepartureDate", "DepartureDate"),
																																																		  new System.Data.Common.DataColumnMapping("Nights", "Nights"),
																																																		  new System.Data.Common.DataColumnMapping("Adults", "Adults"),
																																																		  new System.Data.Common.DataColumnMapping("Children", "Children"),
																																																		  new System.Data.Common.DataColumnMapping("Telephone", "Telephone"),
																																																		  new System.Data.Common.DataColumnMapping("Telephone2", "Telephone2"),
																																																		  new System.Data.Common.DataColumnMapping("Notes", "Notes"),
																																																		  new System.Data.Common.DataColumnMapping("Email", "Email"),
																																																		  new System.Data.Common.DataColumnMapping("IfCustom", "IfCustom")})});
        // 
        // sqlSelectCommand3
        // 
        this.sqlSelectCommand3.CommandText = "SELECT ID, PropertyID, DateTime, ContactName, ContactEmail, ContactTelephone, Arr" +
            "ivalDate, DepartureDate, Nights, Adults, Children, Telephone, Telephone2, Notes," +
            " Email, IfCustom FROM Emails";
        this.sqlSelectCommand3.Connection = CommonFunctions.GetConnection();
        // 
        // EmailsSet
        // 
        this.EmailsSet.DataSetName = "EmailsDataset";
        this.EmailsSet.Locale = new System.Globalization.CultureInfo("en-US");
        ((System.ComponentModel.ISupportInitialize)(this.PhotosSet)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.PropertiesFullSet)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.EmailsSet)).EndInit();

    }
    #endregion

    protected void SubmitButton_Click(object sender, System.EventArgs e)
    {        
        string emailtoowner = "";
        string emailtosender = "";
        string details = "";
string vSubSend = "";
        string vSubject = CommonFunctions.GetSiteAddress() +
            CommonFunctions.PrepareURL(((string)PropertiesFullSet.Tables["Properties"].Rows[0]["Country"]).Replace(" ", "_").ToLower() + "/" +
            ((string)PropertiesFullSet.Tables["Properties"].Rows[0]["StateProvince"]).Replace(" ", "_").ToLower() + "/" +
            ((string)PropertiesFullSet.Tables["Properties"].Rows[0]["City"]).Replace(" ", "_").ToLower() + "/" +
            ((int)PropertiesFullSet.Tables["Properties"].Rows[0]["ID"]).ToString() + "/default.aspx");

        if (IfShowContactInfo())
        {
            details = "Inquiry data:<br />" +
                "Arrival date: " + ArrivalDay.Items[ArrivalDay.SelectedIndex].Value + " " +
                ArrivalMonth.Items[ArrivalMonth.SelectedIndex].Value + " " +
                ArrivalYear.Items[ArrivalYear.SelectedIndex].Value + " " + "<br />";

            if (HowManyNights.Text.Length > 0)
                details += "Length of stay: " + HowManyNights.Text + "<br />";
            if (HowManyAdults.Text.Length > 0)
                details += "Number of adults: " + HowManyAdults.Text + "<br />";
            if (HowManyChildren.Text.Length > 0)
                details += "Number of children: " + HowManyChildren.Text + "<br />";
            if (ContactTelephone.Text.Length > 0)
                details += "Contact telephone: " + ContactTelephone.Text + "<br />";
            if (Telephone.Text.Length > 0)
                details += "Telephone: " + Telephone.Text + "<br />";
            if (Telephone2.Text.Length > 0)
                details += "Telephone 2: " + Telephone2.Text + "<br />";
            if (Comments.Text.Length > 0)
                details += "Comments: " + Comments.Text + "<br />";            

            string vSub1 = "Your property \"" + (string)PropertiesFullSet.Tables["Properties"].Rows[0]["Name"] + "\" (" +
                ((int)PropertiesFullSet.Tables["Properties"].Rows[0]["NumBedrooms"]).ToString() + " Bedroom " +
                (string)PropertiesFullSet.Tables["Properties"].Rows[0]["Type"] + " in " +
                (string)PropertiesFullSet.Tables["Properties"].Rows[0]["City"] + " " +
                (string)PropertiesFullSet.Tables["Properties"].Rows[0]["StateProvince"] + " " +
                (string)PropertiesFullSet.Tables["Properties"].Rows[0]["Country"] + ")<br />" +
                //"listed on <a href='" + CommonFunctions.GetSiteAddress() +
                //"'>" + CommonFunctions.GetSiteName() + "</a>" +
                "Property URL:  <a href='" + vSubject + "'>" + vSubject + "</a><br />" +
                "Has been inquired about by " + ContactName.Text + " (" + ContactEmail.Text;

            if (ContactTelephone.Text.Length > 0)
                vSub1 += ", contact telephone: " + ContactTelephone.Text;

            vSub1 += ").<br />" + details;

            //FOR SENDER
            emailtosender = "Thank you for visiting \"" + CommonFunctions.GetSiteName() + "\". We want to provide you with as much information" +
                " as possible concerning the owner.<br />" +
                "Their property number is \"" + propertyid.ToString() + "\"<br />" +
                "Name of Property Owner: " + PropertiesFullSet.Tables["Properties"].Rows[0]["FirstName"].ToString() +
                " " + PropertiesFullSet.Tables["Properties"].Rows[0]["LastName"].ToString() + "<br />" +
                ((PropertiesFullSet.Tables["Properties"].Rows[0]["PrimaryTelephone"].ToString().Length > 0) ?
                "Property Owner Telephone: " + PropertiesFullSet.Tables["Properties"].Rows[0]["PrimaryTelephone"].ToString() + "<br />" :
                (PropertiesFullSet.Tables["Properties"].Rows[0]["DaytimeTelephone"].ToString().Length > 0) ?
                "Property Owner Telephone: " + PropertiesFullSet.Tables["Properties"].Rows[0]["DaytimeTelephone"].ToString() + "<br />" :
                (PropertiesFullSet.Tables["Properties"].Rows[0]["EveningTelephone"].ToString().Length > 0) ?
                "Property Owner Telephone: " + PropertiesFullSet.Tables["Properties"].Rows[0]["EveningTelephone"].ToString() + "<br />" :
                (PropertiesFullSet.Tables["Properties"].Rows[0]["MobileTelephone"].ToString().Length > 0) ?
                "Property Owner Telephone: " + PropertiesFullSet.Tables["Properties"].Rows[0]["MobileTelephone"].ToString() + "<br />" : "") +
                ((PropertiesFullSet.Tables["Properties"].Rows[0]["TouristBoard"].ToString().Length > 0) ?
                "They are member of \"Chamber of Commerce\": " +
                PropertiesFullSet.Tables["Properties"].Rows[0]["TouristBoard"].ToString() + "<br />" : "") +
                "Below is the email details that were transmitted to the owner:<br /><br />" +
                details;
            //FOR SENDER

            emailtoowner += "Dear " + (string)PropertiesFullSet.Tables["Properties"].Rows[0]["FirstName"] + "!<br /><br />" +
                "This is an inquiry for your property on <b>Vacations-Abroad.com<b><br/> Send your response <a href='mailto:" + ContactEmail.Text + "?subject=" +
                vSubject + "'>" + ContactEmail.Text + "</a>.<br/>";

            emailtoowner += vSub1;
        }
        else
        {
            details = "Inquiry data:<br />" +
                "Arrival date: " + ArrivalDay.Items[ArrivalDay.SelectedIndex].Value + " " +
                ArrivalMonth.Items[ArrivalMonth.SelectedIndex].Value + " " +
                ArrivalYear.Items[ArrivalYear.SelectedIndex].Value + " " + "<br />";

            if (HowManyNights.Text.Length > 0)
                details += "Length of stay: " + HowManyNights.Text + "<br />";
            if (HowManyAdults.Text.Length > 0)
                details += "Number of adults: " + HowManyAdults.Text + "<br />";
            if (HowManyChildren.Text.Length > 0)
                details += "Number of children: " + HowManyChildren.Text + "<br />";
            if (ContactTelephone.Text.Length > 0)
                details += "Contact telephone: " + ContactTelephone.Text + "<br />";
            if (Telephone.Text.Length > 0)
                details += "Telephone: " + Telephone.Text + "<br />";
            if (Telephone2.Text.Length > 0)
                details += "Telephone 2: " + Telephone2.Text + "<br />";
            if (Comments.Text.Length > 0)
                details += "Comments: " + Comments.Text + "<br />";


            emailtoowner = "Dear " + (string)PropertiesFullSet.Tables["Properties"].Rows[0]["FirstName"] + "!<br /><br />" +
                "<font color=\"red\">This is a complimentary inquiry from Vacations-Abroad.com.  " +
                " Our current rate for advertising on our website is: $" +
                System.Configuration.ConfigurationManager.AppSettings["AnnualListingFee"].ToString() + " USD annually.</font><br />" +
                "To respond to this inquiry click here: <a href='mailto:" + ContactEmail.Text + "?subject=" +
                vSubject + "'>" + ContactEmail.Text + "</a>.<br/>" +
                "You have received an inquiry through the <a href='" + CommonFunctions.GetSiteAddress() +
                "'>" + CommonFunctions.GetSiteName() + "</a> website for property \"" +
                (string)PropertiesFullSet.Tables["Properties"].Rows[0]["Name"] + "\" (" +
                ((int)PropertiesFullSet.Tables["Properties"].Rows[0]["NumBedrooms"]).ToString() + " Bedroom " +
                (string)PropertiesFullSet.Tables["Properties"].Rows[0]["Type"] + " in " +
                (string)PropertiesFullSet.Tables["Properties"].Rows[0]["City"] + " " +
                (string)PropertiesFullSet.Tables["Properties"].Rows[0]["StateProvince"] + " " +
                (string)PropertiesFullSet.Tables["Properties"].Rows[0]["Country"] + ").<br />" +
                "This is the URL:  <a href='" + vSubject + "'>" + vSubject + "</a><br />" +
                "Has been inquired about by " + ContactName.Text + " (" + ContactEmail.Text;
            
            if (ContactTelephone.Text.Length > 0)
                emailtoowner += ", contact telephone: " + ContactTelephone.Text;

            emailtoowner += ").<br />" +
                details + "<br />" +
                "<font color=\"red\"> We will call this week to discuss.</font>";
            /*
                        emailtosender = "Thank you for visiting \"" + CommonFunctions.GetSiteName () + "\". We want to provide you with as much information" +
                            " as possible concerning the owner.<br />" +
                            "Their property number is \"" + propertyid.ToString () + "\"<br />" +
                            "Name of Property Owner: " + PropertiesFullSet.Tables["Properties"].Rows[0]["FirstName"].ToString () +
                            " " + PropertiesFullSet.Tables["Properties"].Rows[0]["LastName"].ToString () + "<br />" +
                            "Below is the email details that were transmitted to the owner:<br /><br />" +
                            details;
            */
        }

        emailtoowner = emailtoowner.Replace("\r", "").Replace("\n", Environment.NewLine);
        emailtosender = emailtosender.Replace("\r", "").Replace("\n", Environment.NewLine);

        DataRow newrow = EmailsSet.Tables["Emails"].NewRow();

        newrow["PropertyID"] = propertyid;
        newrow["DateTime"] = DateTime.Now;
        newrow["ContactName"] = First(ContactName.Text, 100);
        newrow["ContactEmail"] = First(ContactEmail.Text, 100);
        newrow["ContactTelephone"] = First(ContactTelephone.Text, 100);
        newrow["ArrivalDate"] = First(ArrivalDay.Items[ArrivalDay.SelectedIndex].Value + " " + ArrivalMonth.Items[ArrivalMonth.SelectedIndex].Value + " " + ArrivalYear.Items[ArrivalYear.SelectedIndex].Value, 100);
        newrow["Nights"] = First(HowManyNights.Text, 100);
        newrow["Adults"] = First(HowManyAdults.Text, 100);
        newrow["Children"] = First(HowManyChildren.Text, 100);
        newrow["Telephone"] = First(Telephone.Text, 100);
        newrow["Telephone2"] = First(Telephone2.Text, 100);
        newrow["Notes"] = First(Comments.Text, 2000);
        newrow["Email"] = First(emailtoowner, 6000);
        newrow["IfCustom"] = false;

        EmailsSet.Tables["Emails"].Rows.Add(newrow);
        //lock (CommonFunctions.Connection)
        EmailsAdapter.Update(EmailsSet);

        System.Text.RegularExpressions.Regex regex =
            new System.Text.RegularExpressions.Regex("^\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$");
        // beginning of actual email sending part

        //SmtpClient smtpclient = new SmtpClient (ConfigurationManager.AppSettings["SMTPServer"],
        //    int.Parse (ConfigurationManager.AppSettings["SMTPPort"]));
        SmtpClient smtpclient = new SmtpClient("mail.vacations-abroad.com", 25);

        //MailMessage message = new MailMessage (IfShowContactInfo () ?
        //    ContactEmail.Text : "ar@" + CommonFunctions.GetDomainName (), (string)PropertiesFullSet.Tables["Properties"].Rows[0]["Email"]);
        MailMessage message = new MailMessage("ar@vacations-abroad.com", (string)PropertiesFullSet.Tables["Properties"].Rows[0]["Email"]);        

        message.Subject = CommonFunctions.GetSiteAddress() +
            CommonFunctions.PrepareURL(((string)PropertiesFullSet.Tables["Properties"].Rows[0]["Country"]).Replace(" ", "_").ToLower() + "/" +
            ((string)PropertiesFullSet.Tables["Properties"].Rows[0]["StateProvince"]).Replace(" ", "_").ToLower() + "/" +
            ((string)PropertiesFullSet.Tables["Properties"].Rows[0]["City"]).Replace(" ", "_").ToLower() + "/" +
            ((int)PropertiesFullSet.Tables["Properties"].Rows[0]["ID"]).ToString() + "/default.aspx");
       
        message.Subject = "Vacations-Abroad.com Reservation Inquiry Property #" + propertyid.ToString();  //use property # in subject
        message.Body = emailtoowner;
        message.IsBodyHtml = true;

        // message.Headers["Content-Type"] = "text/plain; charset = \"iso-8859-1\"";
        // Added below to deal with Credential problem of Smarter Mail, on 4/5/08 --LMG

        smtpclient.Credentials = new System.Net.NetworkCredential("noreply@vacations-abroad.com", "napoleon");
        smtpclient.UseDefaultCredentials = false;


        if (regex.Match(message.To.ToString()).Success)
        {
            try
            {
                smtpclient.Send(message);
            }
            catch (Exception ex)
            {

            }
        }

        //TO SENDER
        if (regex.Match(ContactEmail.Text).Success && (emailtosender.Length > 0))
        {
            MailMessage message2 = new MailMessage("noreply@" + CommonFunctions.GetDomainName(), ContactEmail.Text);
            message2.Subject = CommonFunctions.GetSiteAddress() +
                CommonFunctions.PrepareURL(((string)PropertiesFullSet.Tables["Properties"].Rows[0]["Country"]).Replace(" ", "_").ToLower() + "/" +
                ((string)PropertiesFullSet.Tables["Properties"].Rows[0]["StateProvince"]).Replace(" ", "_").ToLower() + "/" +
                ((string)PropertiesFullSet.Tables["Properties"].Rows[0]["City"]).Replace(" ", "_").ToLower() + "/" +
                ((int)PropertiesFullSet.Tables["Properties"].Rows[0]["ID"]).ToString() + "/default.aspx");
            message2.Body = emailtosender;
            message2.IsBodyHtml = message.IsBodyHtml;
            //message2.Headers["Content-Type"] = message.Headers["Content-Type"];
            
            message2.From = new MailAddress("noreply@vacations-abroad.com");
            smtpclient.Send(message2);
        }

        //SUBSCRIBED TO VA
        if (regex.Match(System.Configuration.ConfigurationManager.AppSettings["InquiryEmail"]).Success)
        {
            MailMessage message2 = new MailMessage(IfShowContactInfo() ? ContactEmail.Text : "ar@" +
                CommonFunctions.GetDomainName(), System.Configuration.ConfigurationManager.AppSettings["InquiryEmail"]);

            message2.Subject = "Vacations-Abroad.com Reservation Inquiry Property #" + propertyid.ToString();  //use property # in subject
            message2.Body = emailtoowner;
            message2.IsBodyHtml = message.IsBodyHtml;
            //message2.Headers["Content-Type"] = message.Headers["Content-Type"];
            message2.From = new MailAddress("noreply@vacations-abroad.com");

            smtpclient.Send(message2);
        }

        //SUBSCRIBED TO VA
        if (regex.Match(System.Configuration.ConfigurationManager.AppSettings["InquiryEmail2"]).Success)
        {
            MailMessage message2 = new MailMessage(IfShowContactInfo() ? ContactEmail.Text : "ar@" +
                CommonFunctions.GetDomainName(), System.Configuration.ConfigurationManager.AppSettings["InquiryEmail2"]);

            message2.Subject = "Reservation Inquiry Property #" + propertyid.ToString();  //use property # in subject
            message2.Body = emailtoowner;
            message2.IsBodyHtml = message.IsBodyHtml;
            //message2.Headers["Content-Type"] = message.Headers["Content-Type"];
            message2.From = new MailAddress("noreply@vacations-abroad.com");

            smtpclient.Send(message2);
        }

        //**add info to new email table .. table has name, email, phone, phone2, arrivalDate, propertyNumber, transDate
        DBConnection obj = new DBConnection();
        try
        {
            DateTime dTime = new DateTime();
            string tempDate = ArrivalMonth.SelectedItem.Text + "/" + ArrivalDay.SelectedItem.Text + "/" +
                ArrivalYear.SelectedItem.Text;

            dTime = Convert.ToDateTime(tempDate);

            obj.spInsertEmailCampaign(ContactName.Text, ContactEmail.Text, ContactTelephone.Text, Telephone.Text,
                dTime, propertyid, DateTime.Now);
        }
        catch (Exception ex) { lblInfo.Text = ex.Message; }
        finally { obj.CloseConnection(); }
        //**add info to new email table

        backlinkurl = Session["emailReturnURL"].ToString();
        Response.Redirect(backlinkurl);
    }

    private string First(string str, int numchars)
    {
        if (str.Length <= numchars)
            return str;
        else
            return str.Substring(0, numchars);
    }

    protected void ClearButton_Click(object sender, System.EventArgs e)
    {
        Response.Redirect(CommonFunctions.PrepareURL("SendEmail.aspx?" + Request.QueryString.ToString()));
    }
    public string GetTitle()
    {
        string titlereplacement = PropertiesFullSet.Tables["Properties"].Rows[0]["City"].ToString() + " Property " + propertyid.ToString() + " Holiday Accommodation and Vacation Homes";

        return titlereplacement;
    }
}
