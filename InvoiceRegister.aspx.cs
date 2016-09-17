using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class InvoiceRegister : AdminPage
{
	protected int year = -1;

	protected void Page_Load (object sender, EventArgs e)
    {
		if ((Request.Params["Year"] == null) || (Request.Params["Year"].Length == 0))
			Response.Redirect (CommonFunctions.PrepareURL ("InternalError.aspx"));

		try
		{
			year = Convert.ToInt32 (Request.Params["Year"]);
		}
		catch (Exception)
		{
		}

		if (year < 2000)
			Response.Redirect (CommonFunctions.PrepareURL ("InternalError.aspx"));

		if (!IsPostBack)
			DataBind ();
	}
    protected void InvoicesGrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex != -1)
        {
            DBConnection obj = new DBConnection();
            try
            {
                HyperLink lnkProperty = (HyperLink)e.Row.FindControl("hlkProperty");
                if (lnkProperty != null)
                {
                    string vNum = lnkProperty.Text;

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

                        lnkProperty.NavigateUrl = CommonFunctions.PrepareURL(vCountry + "/" + vState + "/" + vCity + "/" + lnkProperty.Text + "/default.aspx");
                    }
                    
                }
            }
            catch (Exception ex) { Response.Write("Error with response"); }
            finally { obj.CloseConnection(); }
        }
    }
}
