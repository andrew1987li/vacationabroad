<%@ Page Language="C#" MasterPageFile="~/MasterPageNoCss.master" AutoEventWireup="true" CodeFile="paymentreceipt.aspx.cs" Inherits="paymentreceipt" Title="Untitled Page" %>

<%@ Register Src="paymentreceipt.ascx" TagName="paymentreceipt" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
    <uc1:paymentreceipt ID="Paymentreceipt1" runat="server" />
</asp:Content>

