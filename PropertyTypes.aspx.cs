using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class PropertyTypes : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DBConnection obj = new DBConnection();
            try
            {
                //**ddlOld value is string-name, ddlPrimary value is int-id
                DataTable dt = obj.spGetCategories();
                ddlPrimary.DataSource = dt;
                ddlPrimary.DataBind();

                ddlPrimaryEdit.DataSource = dt;
                ddlPrimaryEdit.DataBind();

                ddlOld.DataSource = obj.spGetPropertyTypes();
                ddlOld.DataBind();
                
            }
            catch (Exception ex) { lblInfo.Text = "load " + ex.Message; }
            finally { obj.CloseConnection(); }
        }
        if ((ddlOld.SelectedIndex == 0) && (ddlPrimary.SelectedIndex == 0))
            ddlAssoc.Items.Clear();
    }
    protected void btnOldRename_Click(object sender, EventArgs e)
    {
        DBConnection obj = new DBConnection();
        try
        {
            obj.spUpdatePropertyType(txtOldRename.Text, ddlOld.SelectedValue);

            DataTable dt = new DataTable();
            ddlOld.Items.Clear();
            ddlOld.DataSource = obj.spGetPropertyTypes();
            ddlOld.DataBind();
        }
        catch (Exception ex) { lblInfo.Text = "btnOldRen " + ex.Message; }
        finally { obj.CloseConnection(); }
    }
    protected void ddlOld_DataBound(object sender, EventArgs e)
    {
        ddlOld.Items.Insert(0, new ListItem("Select Old Type", "0"));
    }
    protected void btnOldDelete_Click(object sender, EventArgs e)
    {
        if (ddlOld.SelectedIndex != 0)
        {
            DBConnection obj = new DBConnection();

            bool vExists = obj.spSelectPropertyExistsWType(ddlOld.SelectedItem.Text);

            if (vExists == false)
            {                
                try
                {
                    obj.spDeletePropertyType(ddlOld.SelectedValue);

                    DataTable dt = new DataTable();
                    ddlOld.Items.Clear();
                    ddlOld.DataSource = obj.spGetPropertyTypes();
                    ddlOld.DataBind();
                }
                catch (Exception ex) { lblInfo.Text = "btnOldDel " + ex.Message; }
                finally { obj.CloseConnection(); }
            }
            else
                lblInfo.Text = "Property exists for this type.";
        }
    }
    protected void btnPrimaryEdit_Click(object sender, EventArgs e)
    {
        DBConnection obj = new DBConnection();
        try
        {
            obj.spUpdateCategory(ddlPrimaryEdit.SelectedValue, txtPrimaryEdit.Text);

            DataTable dt = obj.spGetCategories();
            ddlPrimary.Items.Clear();
            ddlPrimary.DataSource = dt;
            ddlPrimary.DataBind();

            ddlPrimaryEdit.Items.Clear();
            ddlPrimaryEdit.DataSource = dt;
            ddlPrimaryEdit.DataBind();
        }
        catch (Exception ex) { lblInfo.Text = "btnPriEd " + ex.Message; }
        finally { obj.CloseConnection(); }
    }
    protected void btnPrimaryAdd_Click(object sender, EventArgs e)
    {  //add to propertycategories table
        DBConnection obj = new DBConnection();
        try
        {
            obj.spInsertCategory(txtPrimaryEdit.Text);

            DataTable dt = obj.spGetCategories();
            ddlPrimary.Items.Clear();
            ddlPrimary.DataSource = dt;
            ddlPrimary.DataBind();

            ddlPrimaryEdit.Items.Clear();
            ddlPrimaryEdit.DataSource = dt;
            ddlPrimaryEdit.DataBind();
        }
        catch (Exception ex) { lblInfo.Text = "btnPriAdd " + ex.Message; }
        finally { obj.CloseConnection(); }
    }
    protected void btnPrimaryDelete_Click(object sender, EventArgs e)
    {
        //see if properties exist, delete primary from primary table and delete related values for property table(TODO)
        DBConnection obj = new DBConnection();
        try
        {
            bool vExists = obj.spSelectPropertyExistsWPriID(Convert.ToInt32(ddlPrimaryEdit.SelectedValue));

            if (vExists == false)
            {
                obj.spDeleteCategory(Convert.ToInt32(ddlPrimaryEdit.SelectedValue));

                DataTable dt = obj.spGetCategories();
                ddlPrimary.Items.Clear();
                ddlPrimary.DataSource = dt;
                ddlPrimary.DataBind();

                ddlPrimaryEdit.Items.Clear();
                ddlPrimaryEdit.DataSource = dt;
                ddlPrimaryEdit.DataBind();
            }
            else
                lblInfo.Text = "Property exists for this primary type.";
        }
        catch (Exception ex) { lblInfo.Text = "btnPriDel " + ex.Message; }
        finally { obj.CloseConnection(); }
    }
    protected void ddlPrimary_DataBound(object sender, EventArgs e)
    {
        if(ddlPrimary.Items[0].Text != "Select Primary")
        ddlPrimary.Items.Insert(0, new ListItem("Select Primary", "0"));
    }
    protected void ddlPrimary_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPrimary.SelectedIndex != 0)
        {
            if (ddlOld.SelectedIndex == 0)
            {
                //old column with selected index 0..display assoc types
                DBConnection obj = new DBConnection();
                try
                {
                    ddlAssoc.Items.Clear();
                    ddlAssoc.DataSource = obj.spSelectPropertyAssoc(Convert.ToInt32(ddlPrimary.SelectedValue));
                    ddlAssoc.DataBind();
                    
                }
                catch (Exception ex) { lblInfo.Text = "ddlPriSel1 " + ex.Message; }
                finally { obj.CloseConnection(); }
            }
            else
            {
                //use category column in propertytypes table to associate primary category
                DBConnection obj = new DBConnection();
                try
                {
                    obj.spUpdatePropertyAssoc(ddlOld.SelectedItem.Text, Convert.ToInt32(ddlPrimary.SelectedValue)); 
                  
                    ddlOld.Items.Clear();
                    ddlOld.DataSource = obj.spGetPropertyTypes();
                    ddlOld.DataBind();

                    ddlPrimary.SelectedIndex = 0;
                    ddlPrimary.DataBind();
                }
                catch (Exception ex) { lblInfo.Text = "ddlPriSel2 " +ex.Message; }
                finally { obj.CloseConnection(); }
            }
        }
    }
    protected void btnRemoveAssoc_Click(object sender, EventArgs e)
    {
        //set category to null in propertytypes table where name=ddlAssoc value
        DBConnection obj = new DBConnection();
        try
        {
            obj.spDeletePropertyAssoc(ddlAssoc.SelectedValue);

            DataTable dt = obj.spSelectPropertyAssoc(Convert.ToInt32(ddlPrimary.SelectedValue));
            ddlAssoc.Items.Clear();
            ddlAssoc.DataSource = dt;
            ddlAssoc.DataBind();

            ddlOld.Items.Clear();
            ddlOld.DataSource = obj.spGetPropertyTypes();
            ddlOld.DataBind();
            
        }
        catch (Exception ex) { lblInfo.Text = "btnReAssoc " + ex.Message; }
        finally { obj.CloseConnection(); }
    }
    protected void ddlOld_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPrimary.SelectedIndex != 0)
        {
            //make assoc if primary ddl selected
            DBConnection obj = new DBConnection();
            try
            {
                obj.spUpdatePropertyAssoc(ddlOld.SelectedItem.Text, Convert.ToInt32(ddlPrimary.SelectedValue));

                ddlOld.Items.Clear();
                ddlOld.DataSource = obj.spGetPropertyTypes();
                
                ddlOld.DataBind();
ddlOld.SelectedIndex = 0;

                ddlAssoc.Items.Clear();
                ddlAssoc.DataSource = obj.spSelectPropertyAssoc(Convert.ToInt32(ddlPrimary.SelectedValue));
                ddlAssoc.DataBind();
            }
            catch (Exception ex) { lblInfo.Text = "ddlOldSel " + ex.Message; }
            finally { obj.CloseConnection(); }
        }
    }
    protected void btnAssocAdd_Click(object sender, EventArgs e)
    {
        if (ddlPrimary.SelectedIndex == 0)
        {
            lblInfo.Text = "Please select primary category";
        }
        else
        {
            if (txtAssoc.Text != "")
            {
                //see if type exists
                //insert new property type into properties table with ddlPrimary's value to assoc column
                DBConnection obj = new DBConnection();
                try
                {
                    bool vExists = obj.spSelectPropertyTypeExists(txtAssoc.Text);

                    if (vExists == false)
                    {
                        obj.spInsertPropertyType(txtAssoc.Text, Convert.ToInt32(ddlPrimary.SelectedValue));
                        txtAssoc.Text = "";

                        //reload assoc ddl                               
                        ddlAssoc.Items.Clear();
                        ddlAssoc.DataSource = obj.spSelectPropertyAssoc(Convert.ToInt32(ddlPrimary.SelectedValue));
                        ddlAssoc.DataBind();
                    }
                    else
                        lblInfo.Text = "Property Type Already Exists";
                }
                catch (Exception ex) { lblInfo.Text = "ddlAssocAdd " + ex.Message; }
                finally { obj.CloseConnection(); }
            }
        }
    }
    protected void btnAssocRename_Click(object sender, EventArgs e)
    {
        //rename [name] in propertytypes table
        DBConnection obj = new DBConnection();
        try
        {
            obj.spUpdatePropertyType(txtAssoc.Text, ddlAssoc.SelectedItem.Text);

            //reload assoc ddl                               
            ddlAssoc.Items.Clear();
            ddlAssoc.DataSource = obj.spSelectPropertyAssoc(Convert.ToInt32(ddlPrimary.SelectedValue));
            ddlAssoc.DataBind();
        }
        catch (Exception ex) { lblInfo.Text = "btnAssocRen " + ex.Message; }
        finally { obj.CloseConnection(); }
    }
    protected void btnAssocDelete_Click(object sender, EventArgs e)
    {
        //copy this function to other deletes...see if property exists for type, alert if so
        DBConnection obj = new DBConnection();
        try
        {
            bool vExists = obj.spSelectPropertyExistsWType(ddlAssoc.SelectedItem.Text);

            if (vExists == false)
            {
                obj.spDeletePropertyType(ddlAssoc.SelectedItem.Text);

                //reload assoc ddl                               
                ddlAssoc.Items.Clear();
                ddlAssoc.DataSource = obj.spSelectPropertyAssoc(Convert.ToInt32(ddlPrimary.SelectedValue));
                ddlAssoc.DataBind();
            }
            else
                lblInfo.Text = "Property exists for this type.";
        }
        catch (Exception ex) { lblInfo.Text = ddlAssoc.SelectedValue + ex.Message; }
        finally { obj.CloseConnection(); }
    }
}
