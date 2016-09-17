<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageNoCss.master" AutoEventWireup="true" CodeFile="CategoryMatch.aspx.cs" Inherits="CategoryMatch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<div class="Left">
    <asp:DropDownList ID="ddlCategories" runat="server" 
        DataTextField="CategoryTypes" DataValueField="id">
    </asp:DropDownList>
    <asp:TextBox ID="TextBox1" runat="server" Width="200px"></asp:TextBox>
    <asp:Button ID="btnAdd" runat="server" Text="ADD" />&nbsp;
    <asp:Button ID="btnEdit" runat="server" Text="EDIT" />&nbsp;
    <asp:Button ID="btnDelete" runat="server" Text="DELETE" 
        onclientclick="return confirm('You sure you want to delete item?');" />
</div>
    <asp:Label ID="lblInfo" runat="server" ForeColor="Red"></asp:Label>
</asp:Content>

