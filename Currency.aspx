<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageNoCss.master" AutoEventWireup="true" CodeFile="Currency.aspx.cs" Inherits="Currency" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
    <div class="Left">
<table cellpadding="6">
<tr>
<td>
<b>Enter new currency</b>
</td>
<td>
3 Char Abbreviation <asp:TextBox ID="txtAbbr" runat="server"></asp:TextBox></td><td>Text<asp:TextBox ID="txtDesc" runat="server"></asp:TextBox>
</td>
</tr>
<tr><td colspan="3"><asp:Button ID="btnSubmit" runat="server" Text="Enter New" onclick="btnSubmit_Click" /></td></tr>
<tr>
<td>
<b>Edit Existing currency</b>
</td>
<td colspan="2">
<asp:DropDownList ID="ddlCurrencies" runat="server" 
        onselectedindexchanged="ddlCurrencies_SelectedIndexChanged" 
        AutoPostBack="True">
    </asp:DropDownList>
    <asp:TextBox ID="txtEditAbbr" runat="server" Width="50px"></asp:TextBox>
    <asp:TextBox ID="txtEditText" runat="server"></asp:TextBox>
</td>
</tr>
<tr>
<td colspan="3"><asp:Button ID="btnUpdate" runat="server" Text="Update" 
        onclick="btnUpdate_Click" /></td>
</tr>
</table>
<br />
    <asp:Label ID="lblInfo" runat="server" ForeColor="Red"></asp:Label>
</div>
</asp:Content>

