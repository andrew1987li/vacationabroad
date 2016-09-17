using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class OwnersList2 : AdminPage
{    
    protected void Page_Load(object sender, EventArgs e)
    {
        userid = AuthenticationManager.UserID;
        if (userid != 1)
            Response.Redirect("login.aspx");

        //if (grdOwners.Rows.Count < 1)
        //    divResultsDisplay.Visible = false;
        //else
        //    divResultsDisplay.Visible = true;


        BodyTag.Attributes["background"] = CommonFunctions.PrepareURL("images/background.gif");

        LogInLink.NavigateUrl = CommonFunctions.PrepareURL("Login.aspx");
        CreateAccountLink.NavigateUrl = CommonFunctions.PrepareURL("FindOwner.aspx");
        LogOutLink.NavigateUrl = CommonFunctions.PrepareURL("Logout.aspx");
        UserIDLink.NavigateUrl = CommonFunctions.PrepareURL("MyAccount.aspx");
        OwnersLinkLink.NavigateUrl = CommonFunctions.PrepareURL("OwnersList.aspx");
        OutStandingInvoicesLink.NavigateUrl = CommonFunctions.PrepareURL("OutstandingInvoices.aspx");
        AdminLink.NavigateUrl = CommonFunctions.PrepareURL("Administration.aspx");

        Logo.ImageUrl = CommonFunctions.PrepareURL("images/logo.gif");
        MainLogo.ImageUrl = CommonFunctions.PrepareURL("images/main.jpg");
    }
    protected void vUnload(object sender, EventArgs e)
    {
        
    }
    protected void lnkAB_Click(object sender, EventArgs e)
    {
        char[] charAry = { 'A', 'B' };
        DataTable dt = GetData(charAry);
        grdOwners.DataSource = dt;
        Session["table1"] = dt;
        grdOwners.DataBind();
    }
    protected void lnkCD_Click(object sender, EventArgs e)
    {
        char[] charAry = { 'C', 'D' };
        DataTable dt = GetData(charAry);
        grdOwners.DataSource = dt;
        Session["table2"] = dt;
        grdOwners.DataBind();
    }
    protected void lnkEG_Click(object sender, EventArgs e)
    {
        char[] charAry = { 'E', 'G' };
        DataTable dt = GetData(charAry);
        grdOwners.DataSource = dt;
        Session["table3"] = dt;
        grdOwners.DataBind();
    }
    protected void lnkHI_Click(object sender, EventArgs e)
    {
        char[] charAry = { 'H', 'I' };
        DataTable dt = GetData(charAry);
        grdOwners.DataSource = dt;
        Session["table4"] = dt;
        grdOwners.DataBind();
    }
    protected void lnkJL_Click(object sender, EventArgs e)
    {
        char[] charAry = { 'J', 'L' };
        DataTable dt = GetData(charAry);
        grdOwners.DataSource = dt;
        Session["table5"] = dt;
        grdOwners.DataBind();
    }
    protected void lnkMO_Click(object sender, EventArgs e)
    {
        char[] charAry = { 'M', 'O' };
        DataTable dt = GetData(charAry);
        grdOwners.DataSource = dt;
        Session["table6"] = dt;
        grdOwners.DataBind();
    }
    protected void lnkPR_Click(object sender, EventArgs e)
    {
        char[] charAry = { 'P', 'R' };
        DataTable dt = GetData(charAry);
        grdOwners.DataSource = dt;
        Session["table7"] = dt;
        grdOwners.DataBind();
    }
    protected void lnkST_Click(object sender, EventArgs e)
    {
        char[] charAry = { 'S', 'T' };
        DataTable dt = GetData(charAry);
        grdOwners.DataSource = dt;
        Session["table8"] = dt;
        grdOwners.DataBind();
    }
    protected void lnkUZ_Click(object sender, EventArgs e)
    {
        char[] charAry = { 'U', 'Z' };
        DataTable dt = GetData(charAry);
        grdOwners.DataSource = dt;
        Session["table9"] = dt;
        grdOwners.DataBind();
    }


    private DataTable GetData(char[] queryItem)
    {
        DataTable dt = new DataTable();
        
            SqlDataAdapter MainAdapter;
        DataSet MainDataSet = new DataSet();
            
            MainAdapter = CommonFunctions.PrepareAdapter(CommonFunctions.GetConnection(), "SELECT " +
                "ID, Email, FirstName, LastName, City, State, Country, PrimaryTelephone, EveningTelephone, MobileTelephone, Website, " +
                "CompanyName FROM Users_Backup WHERE (ID <> 1) AND " +
                "lastname like '[" + queryItem[0] + "-" + queryItem[1] + "]%' order by lastname",
                SqlDbType.Int);

            MainAdapter.Fill(MainDataSet, "Users_Backup");
            dt = MainDataSet.Tables[0];    
        
        return dt;
    }
    
    protected void SearchByKeyWords_Click(object sender, EventArgs e)
    {
        if (KeyWords.Text.Length > 0)
            Response.Redirect(CommonFunctions.PrepareURL("SearchTerms.aspx?SearchTerms=" +
                HttpUtility.UrlEncode(KeyWords.Text)), true);
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {                    
        DBConnection obj = new DBConnection();
        foreach (GridViewRow row in grdOwners.Rows)
        {

            Label lblID = (Label)row.FindControl("lblID");
            TextBox txtEmail = (TextBox)row.FindControl("txtEmail");
            TextBox txtfName = (TextBox)row.FindControl("txtFirstName");
            TextBox txtlName = (TextBox)row.FindControl("txtLastName");
            TextBox txtCity = (TextBox)row.FindControl("txtCity");
            TextBox txtState = (TextBox)row.FindControl("txtState");
            TextBox txtPriPhone = (TextBox)row.FindControl("txtPrimaryTelephone");
            TextBox txtMobile = (TextBox)row.FindControl("txtMobileTelephone");
            TextBox txtEvePhone = (TextBox)row.FindControl("txtEveningPhone");
            TextBox txtWebsite = (TextBox)row.FindControl("txtWebsite");
            TextBox txtCompany = (TextBox)row.FindControl("txtCompany");

            string vID = string.Empty;
            if (lblID != null)
                vID = lblID.Text;
            string vEmail = string.Empty;
            if (txtEmail != null)
                vEmail = txtEmail.Text;
            string vfName = string.Empty;
            if (txtfName != null)
                vfName = txtfName.Text;
            string vlName = string.Empty;
            if (txtlName != null)
                vlName = txtlName.Text;
            string vCity = string.Empty;
            if (txtCity != null)
                vCity = txtCity.Text;
            string vState = string.Empty;
            if (txtState != null)
                vState = txtState.Text;
            string vPPhone = string.Empty;
            if (txtPriPhone != null)
                vPPhone = txtPriPhone.Text;
            string vMobile = string.Empty;
            if (txtMobile != null)
                vMobile = txtMobile.Text;
            string vEPhone = string.Empty;
            if (txtEvePhone != null)
                vEPhone = txtEvePhone.Text;
            string vWebsite = string.Empty;
            if (txtWebsite != null)
                vWebsite = txtWebsite.Text;
            string vCompany = string.Empty;
            if (txtCompany != null)
                vCompany = txtCompany.Text;

            try
            {
                //VADBCommander.UserBackupEdit(vCompany, vfName, vlName, vCity, vState, vPPhone, vEPhone, vMobile, vWebsite, vCompany, lblID.Text);
            }
            catch (Exception ex) { lblError.Text += ex.Message; }

        }


                obj.CloseConnection();
        lblError.Text += "Data Saved";
    }
}
