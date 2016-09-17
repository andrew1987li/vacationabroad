using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class treeview : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            DataTable dtRegion = new DataTable();  //load all regions sorted
            DBConnection obj1 = new DBConnection();
            dtRegion = VADBCommander.RegionList();

            foreach (DataRow row in dtRegion.Rows)
            {
                TreeNode trnRegion = new TreeNode();
                trnRegion.Text = row["region"].ToString();  //add each region node
                trnRegion.Value = "region" + row["id"].ToString();
                
                trnRegion.Collapse();
                TreeView1.Nodes.Add(trnRegion);
            }
            obj1.CloseConnection();
        }
    }
    protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    {
        //foreach (TreeNode node in TreeView1.Nodes)
        //{
        //    if (node.Selected == false)
        //        node.Collapse();
        //}
        DBConnection obj = new DBConnection();
       
            if (TreeView1.SelectedNode.Value.Contains("region"))
            {

                string IDreg = TreeView1.SelectedNode.Value.Substring(6);
                DataTable dtRegion = new DataTable();
                dtRegion =VADBCommander.CountriesByRegionList(IDreg);

                foreach (DataRow row1 in dtRegion.Rows)
                {
                    if(TreeView1.SelectedNode.ChildNodes.Count < dtRegion.Rows.Count)
                    TreeView1.SelectedNode.ChildNodes.Add(new TreeNode(row1["country"].ToString(), "country" + row1["id"].ToString()));
                }
                
            }
            
                if (TreeView1.SelectedNode.Value.Contains("country"))
                {
                    string IDcon = TreeView1.SelectedNode.Value.Substring(7);
                    DataTable dtCountry = new DataTable();
                    dtCountry = VADBCommander.StateProvinceByCountrySimpleList(IDcon);

                    foreach (DataRow row2 in dtCountry.Rows)
                    {
                        if (TreeView1.SelectedNode.ChildNodes.Count < dtCountry.Rows.Count)
                            TreeView1.SelectedNode.ChildNodes.Add(new TreeNode(row2["stateprovince"].ToString(), "state" + row2["id"].ToString()));
                    }
                    TreeView1.SelectedNode.ChildNodes.Add(new TreeNode("test"));
               
                }
                if (TreeView1.SelectedNode.Value.Contains("state"))
                {
                    string IDstate = TreeView1.SelectedNode.Value.Substring(5);
                    DataTable dtState = new DataTable();
                    dtState = VADBCommander.CityByStateList(IDstate);

                    foreach (DataRow row3 in dtState.Rows)
                    {
                        string vState = TreeView1.SelectedNode.Parent.Text;
                        string vCountry = TreeView1.SelectedNode.Parent.Parent.Text;
                        string vCity = TreeView1.SelectedNode.Text;

                        if (TreeView1.SelectedNode.ChildNodes.Count < dtState.Rows.Count)
                            TreeView1.SelectedNode.ChildNodes.Add(new TreeNode(row3["city"].ToString(), "city" + row3["id"].ToString(),"",
                                "http://www.vacations-abroad.com/" + vCountry + "/" + vState + "/" + vCity + "/default.aspx", ""));
                        
                    }
                    Response.Write(TreeView1.SelectedNode.Text + TreeView1.SelectedNode.ChildNodes.Count.ToString());
                }
            if (TreeView1.SelectedNode.Expanded == false)
                TreeView1.SelectedNode.Expand();
            else
                if (TreeView1.SelectedNode.Expanded == true)
                    TreeView1.SelectedNode.Collapse();

            TreeView1.SelectedNode.ExpandAll();
            TreeView1.SelectedNode.Selected = false;
        
          
        obj.CloseConnection();
    }
}
