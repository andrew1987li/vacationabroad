<%@ Page Language="C#" AutoEventWireup="true" CodeFile="treeview.aspx.cs" Inherits="treeview" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>        
        
        <asp:TreeView ID="TreeView1" runat="server" 
            onselectednodechanged="TreeView1_SelectedNodeChanged" 
            ShowExpandCollapse="False">
            <Nodes>                
            </Nodes>
        </asp:TreeView>
    </div>
    </form>
</body>
</html>
