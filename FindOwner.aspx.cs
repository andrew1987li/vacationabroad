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

public partial class FindOwner : CommonPage
{
	//protected System.Data.SqlClient.SqlConnection Connection;
	protected System.Data.SqlClient.SqlDataAdapter GetOwner;
	protected Vacations.UserDataset UsersSet;
	protected System.Data.SqlClient.SqlCommand sqlSelectCommand1;

	protected void Page_Load(object sender, System.EventArgs e)
	{
		//string connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

		//CommonFunctions.Connection.ConnectionString = connectionstring;

		//if (CommonFunctions.Connection.State == System.Data.ConnectionState.Closed)
			//CommonFunctions.Connection.Open ();

		if (Request.QueryString.ToString ().Length > 0)
			UserFound.NavigateUrl = "Login.aspx?" + Request.QueryString.ToString ();

		UserFound.Visible = false;
		UserNotFound.Visible = false;
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
		this.GetOwner = new System.Data.SqlClient.SqlDataAdapter();
		this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
		this.UsersSet = new Vacations.UserDataset();
		((System.ComponentModel.ISupportInitialize)(this.UsersSet)).BeginInit();
		// 
		// CommonFunctions.Connection
		// 
		//CommonFunctions.Connection.ConnectionString = "workstation id=MAIN;packet size=4096;integrated security=SSPI;data source=MAIN;pe" +
			//"rsist security info=False;initial catalog=Vacations";
		// 
		// GetOwner
		// 
		this.GetOwner.SelectCommand = this.sqlSelectCommand1;
		this.GetOwner.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
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
		this.sqlSelectCommand1.CommandText = @"SELECT ID, Username, PasswordSalt, Repeats, PasswordHash, Email, FirstName, LastName, Address, City, State, Zip, Country, PrimaryTelephone, EveningTelephone, DaytimeTelephone, MobileTelephone, Website, IfAdmin, CompanyName, Registered, IfPayTravelAgents FROM Users WHERE (Email = @Email)";
		this.sqlSelectCommand1.Connection = CommonFunctions.GetConnection();
		this.sqlSelectCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Email", System.Data.SqlDbType.NVarChar, 300, "Email"));
		// 
		// UsersSet
		// 
		this.UsersSet.DataSetName = "UserDataset";
		this.UsersSet.Locale = new System.Globalization.CultureInfo("en-US");
		((System.ComponentModel.ISupportInitialize)(this.UsersSet)).EndInit();

	}
	#endregion

	protected void CheckEmail_Click(object sender, System.EventArgs e)
	{
		//if (CommonFunctions.Connection.State == System.Data.ConnectionState.Closed)
			//CommonFunctions.Connection.Open ();

		GetOwner.SelectCommand.Parameters["@Email"].Value = EmailAddress.Text;
		//lock (CommonFunctions.Connection)
			if (GetOwner.Fill (UsersSet) > 0)
				UserFound.Visible = true;
			else
				UserNotFound.Visible = true;
	}

	private void NewOwner_Click(object sender, System.EventArgs e)
	{
		Response.Redirect (CommonFunctions.PrepareURL ("AccountInformation.aspx"));
	}
}
