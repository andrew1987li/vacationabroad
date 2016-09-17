<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageNoCss.master" ValidateRequest="false" AutoEventWireup="true" CodeFile="PropertyTypes.aspx.cs" Inherits="PropertyTypes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<div align="center">
<table width="70%">
<tr>
<td>
Old Property Type<br />
    <asp:DropDownList ID="ddlOld" runat="server" Width="200px" DataTextField="name" 
        DataValueField="name" 
        ondatabound="ddlOld_DataBound" AutoPostBack="True" 
        onselectedindexchanged="ddlOld_SelectedIndexChanged">
        
    </asp:DropDownList>
    
</td>
<td>
Associate Primary Type<br />
    <asp:DropDownList ID="ddlPrimary" runat="server" Width="200px" 
        DataTextField="categorytypes" DataValueField="id" AutoPostBack="True" 
        ondatabound="ddlPrimary_DataBound" 
        onselectedindexchanged="ddlPrimary_SelectedIndexChanged">       
    </asp:DropDownList>
</td>
<td>
Old Types Associated<br />
    <asp:DropDownList ID="ddlAssoc" runat="server" Width="350px" 
        DataTextField="type" DataValueField="id">
    </asp:DropDownList>
</td>
</tr>
<tr>
<td valign="top">
Edit Old Property<br />
    <asp:TextBox ID="txtOldRename" runat="server" Width="200px"></asp:TextBox><br />
    <asp:Button ID="btnOldRename" runat="server" Text="Rename" 
        onclick="btnOldRename_Click" Width="90px" /><br />
        <asp:Button ID="btnOldDelete" runat="server" Text="Delete" 
        onclientclick="javascript:return confirm('Are you sure you want to delete this record?');" 
        Width="90px" onclick="btnOldDelete_Click" />
</td>
<td valign="top">
Edit Primary Type<br />
    <asp:DropDownList ID="ddlPrimaryEdit" runat="server" Width="200px" 
        DataTextField="categorytypes" DataValueField="id">
    </asp:DropDownList><br />
    <asp:TextBox ID="txtPrimaryEdit" runat="server" Width="200px"></asp:TextBox>
    <br />
    <asp:Button ID="btnPrimaryEdit" runat="server" Text="Rename" Width="90px" 
        onclick="btnPrimaryEdit_Click" />
    <br />
    <asp:Button ID="btnPrimaryDelete" runat="server" Text="Delete" 
        onclientclick="javascript:return confirm('Are you sure you want to delete this record?');" 
        Width="90px" onclick="btnPrimaryDelete_Click" style="height: 26px" />
        <br />
    <asp:Button ID="btnPrimaryAdd" runat="server" Text="Add" 
        onclick="btnPrimaryAdd_Click" Width="90px" />
</td>
<td valign="top">
    <asp:Button ID="btnRemoveAssoc" runat="server" Text="Remove Association" 
        onclick="btnRemoveAssoc_Click" /><br /><br />
    <asp:TextBox ID="txtAssoc" runat="server" Width="200px"></asp:TextBox>
   <br />
    <asp:Button ID="btnAssocRename" runat="server" Text="Rename" Width="90px" 
        onclick="btnAssocRename_Click" />
    <br />
    <asp:Button ID="btnAssocDelete" runat="server" Text="Delete" Width="90px" 
    onclientclick="javascript:return confirm('Are you sure you want to delete this record?');" 
        onclick="btnAssocDelete_Click" />
    <br />
    <asp:Button ID="btnAssocAdd" runat="server" Text="Add" 
        onclick="btnAssocAdd_Click" Width="90px" />
</td>
</tr>
<tr>
<td>
    
</td>
<td>
</td>
<td></td>
</tr>
</table>
    <asp:Label ID="lblInfo" runat="server" ForeColor="Red" EnableViewState="False"></asp:Label>
    </div>
</asp:Content>

