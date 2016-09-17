using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Currency : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillDropDown();
        }
    }
    private void FillDropDown()
    {
        DBConnection obj = new DBConnection();
        try
        {
            DataTable dt = obj.spCurrencyList();
            ddlCurrencies.DataSource = dt;
            Session["currDt"] = dt;
            ddlCurrencies.DataTextField = "abbr";
            ddlCurrencies.DataValueField = "id";
            ddlCurrencies.DataBind();

            txtEditText.Text = dt.Rows[0]["desc"].ToString();
            txtEditAbbr.Text = dt.Rows[0]["abbr"].ToString();
        }
        catch (Exception ex) { lblInfo.Text += ex.Message; }
    }
    protected void ddlCurrencies_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Session["currDt"] != null)
        {
            DataTable dt = (DataTable)Session["currDt"];
            int i = 0;
            bool found = false;

            while ((found == false) || (i < dt.Rows.Count))
            {
                if (dt.Rows[i]["abbr"].ToString() == ddlCurrencies.SelectedItem.Text)
                {
                    txtEditText.Text = dt.Rows[i]["desc"].ToString();
                    txtEditAbbr.Text = dt.Rows[i]["abbr"].ToString();                    
                    found = true;
                }
                i++;
            }            
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            DBConnection obj = new DBConnection();

            obj.spInsertCurrencies(txtAbbr.Text, txtDesc.Text);
            FillDropDown();
            lblInfo.Text = "Data added";
        }
        catch (Exception ex) { lblInfo.Text = ex.Message; }

    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        DBConnection obj = new DBConnection();
        try
        {
            obj.spUpdateCurrencies(Convert.ToInt32(ddlCurrencies.SelectedValue), txtEditAbbr.Text, txtEditText.Text);
            lblInfo.Text = "Data updated";
            FillDropDown();
        }
        catch (Exception ex) { lblInfo.Text = ex.Message; }
    }
}
