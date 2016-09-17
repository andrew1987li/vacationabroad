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

public partial class OwnersList : AdminPage
{
	//protected System.Data.SqlClient.SqlConnection Connection;
	protected Vacations.UserDataset UserSet;
	protected System.Data.SqlClient.SqlDataAdapter UserAdapter;
	protected System.Data.SqlClient.SqlCommand sqlSelectCommand1;

	protected void Page_Load(object sender, System.EventArgs e)
	{
		userid = AuthenticationManager.UserID;

		//string connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

		//CommonFunctions.Connection.ConnectionString = connectionstring;

		UserAdapter.SelectCommand.Parameters["@UserID"].Value = userid;

		//lock (CommonFunctions.Connection)
			UserAdapter.Fill (UserSet);

		DataBind ();

		OwnerInformationLink.NavigateUrl = CommonFunctions.PrepareURL ("OwnerInformation.aspx?UserID=" + userid.ToString (), "Owners List");
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
		this.UserSet = new Vacations.UserDataset();
		this.UserAdapter = new System.Data.SqlClient.SqlDataAdapter();
		this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
		((System.ComponentModel.ISupportInitialize)(this.UserSet)).BeginInit();
		// 
		// CommonFunctions.Connection
		// 
		//CommonFunctions.Connection.ConnectionString = "workstation id=MAIN;packet size=4096;integrated security=SSPI;data source=MAIN;pe" +
			//"rsist security info=False;initial catalog=Vacations";
		// 
		// UserSet
		// 
		this.UserSet.DataSetName = "UserDataset";
		this.UserSet.Locale = new System.Globalization.CultureInfo("en-US");
		// 
		// UserAdapter
		// 
		this.UserAdapter.SelectCommand = this.sqlSelectCommand1;
		this.UserAdapter.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																							  new System.Data.Common.DataTableMapping("Table", "Users", new System.Data.Common.DataColumnMapping[] {
																																																	   new System.Data.Common.DataColumnMapping("ID", "ID"),
																																																	   new System.Data.Common.DataColumnMapping("Username", "Username"),
																																																	   new System.Data.Common.DataColumnMapping("PasswordSalt", "PasswordSalt"),
																																																	   new System.Data.Common.DataColumnMapping("Repeats", "Repeats"),
																																																	   new System.Data.Common.DataColumnMapping("PasswordHash", "PasswordHash"),
																																																	   new System.Data.Common.DataColumnMapping("Email", "Email"),
																																																	   new System.Data.Common.DataColumnMapping("FirstName", "FirstName"),
																																																	   new System.Data.Common.DataColumnMapping("LastName", "LastName"),
																																																	   new System.Data.Common.DataColumnMapping("Address", "Address"),
																																																	   new System.Data.Common.DataColumnMapping("City", "City"),
																																																	   new System.Data.Common.DataColumnMapping("State", "State"),
																																																	   new System.Data.Common.DataColumnMapping("Zip", "Zip"),
																																																	   new System.Data.Common.DataColumnMapping("Country", "Country"),
																																																	   new System.Data.Common.DataColumnMapping("PrimaryTelephone", "PrimaryTelephone"),
																																																	   new System.Data.Common.DataColumnMapping("EveningTelephone", "EveningTelephone"),
																																																	   new System.Data.Common.DataColumnMapping("DaytimeTelephone", "DaytimeTelephone"),
																																																	   new System.Data.Common.DataColumnMapping("MobileTelephone", "MobileTelephone"),
																																																	   new System.Data.Common.DataColumnMapping("Website", "Website"),
																																																	   new System.Data.Common.DataColumnMapping("IfAdmin", "IfAdmin"),
																																																	   new System.Data.Common.DataColumnMapping("CompanyName", "CompanyName"),
																																																	   new System.Data.Common.DataColumnMapping("Registered", "Registered"),
																																																	   new System.Data.Common.DataColumnMapping("IfPayTravelAgents", "IfPayTravelAgents")})});
		// 
		// sqlSelectCommand1
		// 
		this.sqlSelectCommand1.CommandText = @"SELECT ID, Username, PasswordSalt, Repeats, PasswordHash, Email, FirstName, LastName, Address, City, State, Zip, Country, PrimaryTelephone, EveningTelephone, DaytimeTelephone, MobileTelephone, Website, IfAdmin, CompanyName, Registered, IfPayTravelAgents FROM Users WHERE (ID <> @UserID)";
        this.sqlSelectCommand1.Connection = CommonFunctions.GetConnection();
		this.sqlSelectCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserID", System.Data.SqlDbType.Int, 4, "ID"));
		((System.ComponentModel.ISupportInitialize)(this.UserSet)).EndInit();

	}
	#endregion
}
