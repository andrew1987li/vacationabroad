<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageNoCss.master" AutoEventWireup="true" CodeFile="Moneybook.aspx.cs" Inherits="Moneybook" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<div id="divContent" runat="server"></div>

<form action="https://www.moneybookers.com/app/payment.pl" method="post" target="_blank">
<input type="hidden" name="pay_to_email" value="ar@vacations-abroad.com"/>
<input type="hidden" name="status_url" value="ar@vacations-abroad.com"/> 
<input type="hidden" name="language" value="EN"/>
<input type="hidden" name="amount" value="<%# vAmount %>"/>
<input type="hidden" name="currency" value="USD"/>
<input type="hidden" name="detail1_description" value="Reservation"/>
<input type="hidden" name="detail1_text" value="Reservation for J. Jones"/>
<input type="submit" value="Pay!"/>
</form> 
</asp:Content>

