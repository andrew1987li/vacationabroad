<%@ Page Language="C#" MasterPageFile="~/MasterPageNoCss.master" AutoEventWireup="true" CodeFile="ViewInvoices.aspx.cs" Inherits="ViewInvoices" Title="View Invoices" %>
<asp:Content ID="Content" ContentPlaceHolderID="Content" Runat="Server">
    <table bgcolor="#e4e4af" cellspacing="0" cellpadding="0" width="250" align="center"
        border="2">
        <tr>
            <td>
                <div align="center">
                    <strong>
                        <asp:HyperLink ID="Hyperlink1" runat="server" NavigateUrl="MyAccount.aspx">
							My Account
                        </asp:HyperLink>
                    </strong>
                </div>
            </td>
        </tr>
    </table>
    <br />
    <table bgcolor="#e4e4af" cellspacing="0" cellpadding="0" width="300" align="center"
        border="2">
        <tr>
            <td>
                <div align="center">
                    <strong>
                        <asp:HyperLink ID="OwnerInformationLink" runat="server" NavigateUrl="OwnerInformation.aspx">
							Update my personal information
                        </asp:HyperLink>
                    </strong>
                </div>
            </td>
        </tr>
    </table>
    <asp:Repeater ID="Repeater1" runat="server" DataMember="OutstandingInvoices" 
        DataSource="<%# MainDataSet %>" onitemcreated="Repeater1_ItemCreated">
        <HeaderTemplate>
            <table bordercolor="#ffffff" border="1" cellspacing="0" cellpadding="0" width="100%">
                <tr>
                    <td colspan="100" align="center">
                        <b>
                            <br />
                            Outstanding invoices:
                            <br />
                            <br />
                        </b>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>Invoice Number </b>
                    </td>
                    <td>
                        <b>Invoice Date </b>
                    </td>
                    <td>
                        <b>Invoice Amount </b>
                    </td>
                    <td>
                        <b>Property Number </b>
                    </td>
                    <td>
                    </td>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <%# DataBinder.Eval(Container.DataItem, "InvoiceID", "{0}") %>
                </td>
                <td>
                    <%# DataBinder.Eval(Container.DataItem, "InvoiceDate", "{0:d}") %>
                </td>
                <td>
                    <%# DataBinder.Eval(Container.DataItem, "InvoiceAmount", "{0:c}") %>
                </td>
                <td width="100%" bgcolor="#e4e4af">
                    <a href='<%# CommonFunctions.PrepareURL ("ViewProperty.aspx?UserID=" + DataBinder.Eval(Container.DataItem, "UserID", "{0:d}") + "&PropertyID=" + DataBinder.Eval(Container.DataItem, "ID", "{0:d}"), "Invoices") %>'>
                        <%# DataBinder.Eval(Container.DataItem, "ID", "{0}") %>
                    </a>
                </td>
                <td bordercolor="#ffffff">
                    <input type="button" value="Make Payment" onclick='window.location.href=&quot;<%# CommonFunctions.PrepareURL ("MakePayment.aspx?InvoiceID=" + DataBinder.Eval(Container.DataItem, "InvoiceID", "{0:d}") + "&PropertyID=" + DataBinder.Eval(Container.DataItem, "ID", "{0:d}"), "Invoices") %>&quot;'>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
    <asp:Repeater ID="Repeater2" runat="server" DataMember="PaidInvoices" DataSource="<%# MainDataSet %>">
        <HeaderTemplate>
            <table bordercolor="#ffffff" border="1" cellspacing="0" cellpadding="0" width="100%">
                <tr>
                    <td colspan="100" align="center">
                        <b>
                            <br />
                            Paid invoices:
                            <br />
                            <br />
                        </b>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>Invoice Number </b>
                    </td>
                    <td>
                        <b>Invoice Date </b>
                    </td>
                    <td>
                        <b>Invoice Amount </b>
                    </td>
                    <td>
                        <b>Auction / Property Number </b>
                    </td>
                    <td>
                        <b>Payment Date </b>
                    </td>
                    <td>
                        <b>Payment Amount </b>
                    </td>
                    <td>
                        <b>Payment Method </b>
                    </td>
                    <td>
                        <b>Renewal Date </b>
                    </td>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <%# DataBinder.Eval(Container.DataItem, "InvoiceID", "{0}") %>
                </td>
                <td>
                    <%# DataBinder.Eval(Container.DataItem, "InvoiceDate", "{0:d}") %>
                </td>
                <td>
                    <%# DataBinder.Eval(Container.DataItem, "InvoiceAmount", "{0:c}") %>
                </td>
                <td width="100%" bgcolor="#e4e4af">
                    <a href='<%# (((System.Data.DataRowView)Container.DataItem).Row["IfAuction"] is int) && ((int)((System.Data.DataRowView)Container.DataItem).Row["IfAuction"] == 1) ? CommonFunctions.PrepareURL (DataBinder.Eval(Container.DataItem, "ID", "{0:d}") + "/default.aspx", "Invoices") : CommonFunctions.PrepareURL ("ViewProperty.aspx?UserID=" + DataBinder.Eval(Container.DataItem, "UserID", "{0:d}") + "&PropertyID=" + DataBinder.Eval(Container.DataItem, "ID", "{0:d}"), "Invoices") %>'>
                        <%# DataBinder.Eval(Container.DataItem, "ID", "{0}") %>
                    </a>
                </td>
                <td>
                    <%# DataBinder.Eval(Container.DataItem, "PaymentDate", "{0:d}") %>
                </td>
                <td>
                    <%# DataBinder.Eval(Container.DataItem, "PaymentAmount", "{0:c}") %>
                </td>
                <td>
                    <%# DataBinder.Eval(Container.DataItem, "PaymentType", "{0}") %>
                </td>
                <td>
                    <%# DataBinder.Eval(Container.DataItem, "RenewalDate", "{0:d}") %>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>

	<noscript>
		<img src="http://www.watchwise.net/cgi-watchwise/monitor.cgi?<%= CommonFunctions.GetSiteAddress ().ToLower () %>:all"
			width="1" height="1">
	</noscript>

	<script language="Javascript">
		    document.write('<img src="http://www.watchwise.net/cgi-watchwise/monitorwise.cgi?URL=<%= CommonFunctions.GetSiteAddress ().ToLower () %>:all&LINK=',escape(document.referrer),'" height=1 width=1>')
	</script>
</asp:Content>
