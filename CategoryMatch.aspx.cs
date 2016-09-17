using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class CategoryMatch : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        FillDropDown();
    }
    private void FillDropDown()
    {
        DBConnection obj = new DBConnection();
        DataTable dt = new DataTable();

        try
        {
            dt = obj.spGetCategories();
            ddlCategories.DataSource = dt;
            ddlCategories.DataBind();
        }
        catch (Exception ex) { lblInfo.Text = ex.Message; }
        finally { obj.CloseConnection(); }
    }
}
