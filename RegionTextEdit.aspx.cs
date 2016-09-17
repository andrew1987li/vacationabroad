using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RegionTextEdit :  AdminPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int userid = AuthenticationManager.UserID;
        if (!IsPostBack)
        {
            PopGV();
        }
    }
    public void PopGV()
    {
        gv.DataSource = Utility.dsGrab("RegionTextList");
        gv.DataBind();
        
    }
    protected void gvRegionText_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView gv = (GridView)sender;
        DataView dv = gv.DataSource as DataView;
        DataTable dataTable = dv.Table;

        PopGV();
        
        
    }


    protected void gvRegionText_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView gv = (GridView)sender;
        DataView dv = gv.DataSource as DataView;
        DataTable dataTable = dv.Table;
        gv.DataSource = Utility.dsGrab("RegionTextList");
        gv.EditIndex = e.NewEditIndex;
        gv.DataBind();
    }
//-------------------------------------------------------
    protected void gv_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gv.EditIndex = -1;
        PopGV();

    }
    protected void gv_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gv.EditIndex = e.NewEditIndex;

        //((TextBox) gv.Rows[e.NewEditIndex].Cells[2].Controls[0]).TextMode = TextBoxMode.MultiLine;

        PopGV();
    }
    protected void gv_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string strID = gv.Rows[e.RowIndex].Cells[0].Text;
        //string strEditText = ((TextBox) gv.Rows[e.RowIndex].Cells[2].Controls[0]).Text;
        string strEditText = ((TextBox)gv.Rows[e.RowIndex].FindControl("txt")).Text; 
        VADBCommander.RegionTextEdit(strID, strEditText);
        gv.EditIndex = -1;
        PopGV();


    }
}