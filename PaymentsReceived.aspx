<%@ Page Language="C#" MasterPageFile="~/MasterPageNoCss.master" AutoEventWireup="true" CodeFile="PaymentsReceived.aspx.cs" Inherits="PaymentsReceived" Title="Payments Received" %>
<asp:Content ID="Content" ContentPlaceHolderID="Content" Runat="Server">
    <table bgcolor="#e4e4af" cellspacing="0" cellpadding="0" width="100%" align="center" border="2">
        <tr>
            <td colspan="100" align="center">
                <strong>
                    <asp:HyperLink ID="Hyperlink7" runat="server" NavigateUrl="Administration.aspx">
						Main Administration page
                    </asp:HyperLink>
                </strong>
            </td>
        </tr>
        <tr>
            <td align="center">
                <strong>
                    <asp:HyperLink ID="Hyperlink2" runat="server" NavigateUrl="OwnersList.aspx">
						Owners
                    </asp:HyperLink>
                </strong>
            </td>
            <td align="center">
                <strong>
                    <asp:HyperLink ID="Hyperlink3" runat="server" NavigateUrl="Locations.aspx">
						Locations
                    </asp:HyperLink>
                </strong>
            </td>
            <td align="center">
                <strong>
                    <asp:HyperLink ID="Hyperlink4" runat="server" NavigateUrl="SendCustomEmail.aspx">
						Send Email
                    </asp:HyperLink>
                </strong>
            </td>
            <td align="center">
                <strong>
                    <asp:HyperLink ID="Hyperlink1" runat="server" NavigateUrl="OutstandingInvoices.aspx">
						Outstanding Invoices
                    </asp:HyperLink>
                </strong>
            </td>
            <td align="center">
                <strong>
                    <asp:HyperLink ID="Hyperlink5" runat="server" NavigateUrl="FreeTrial.aspx">
						Free Trial Properties
                    </asp:HyperLink>
                </strong>
            </td>
            <td align="center">
                <strong>
                    <asp:HyperLink ID="Hyperlink6" runat="server" NavigateUrl="PaymentsReceived.aspx">
						Payments Received
                    </asp:HyperLink>
                </strong>
            </td>
        </tr>
        <tr>
			<td colspan="100" align="center">
				<table width="100%" border="0">
					<tr>
						<td colspan="3" align="left">
							<a href='<%= CommonFunctions.PrepareURL ("CommissionPayable.aspx", "Administration") %>'>
								Commission Payable
							</a>
						</td>
						<td colspan="3" align="right">
							<a href='<%= CommonFunctions.PrepareURL ("InvoiceRegister.aspx?Year=2004", "Administration") %>'>
								Invoice Register 2004
							</a>
							&nbsp;&nbsp;
							<a href='<%= CommonFunctions.PrepareURL ("InvoiceRegister.aspx?Year=2005", "Administration") %>'>
								Invoice Register 2005
							</a>
							&nbsp;&nbsp;
							<a href='<%= CommonFunctions.PrepareURL ("InvoiceRegister.aspx?Year=2006", "Administration") %>'>
								Invoice Register 2006
							</a>
						</td>
					</tr>
				</table>
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
    <asp:Repeater ID="Repeater1" runat="server" DataMember="Invoices" DataSource="<%# MainDataSet %>">
        <HeaderTemplate>
            <table bordercolor="#ffffff" border="1" cellspacing="0" cellpadding="0" width="100%">
                <tr>
                    <td colspan="100" align="center">
                        <b>
                            <br />
                            Payment received:
                            <br />
                            <br />
                        </b>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b><a href='PaymentsReceived.aspx?SortOrder=1&<%# GetQueryStringWithoutSortOrder () %>'>
                            Invoice Number </a></b>
                    </td>
                    <td>
                        <b><a href='PaymentsReceived.aspx?SortOrder=2&<%# GetQueryStringWithoutSortOrder () %>'>
                            Invoice Date </a></b>
                    </td>
                    <td>
                        <b><a href='PaymentsReceived.aspx?SortOrder=3&<%# GetQueryStringWithoutSortOrder () %>'>
                            Invoice Amount </a></b>
                    </td>
                    <td>
                        <b><a href='PaymentsReceived.aspx?SortOrder=4&<%# GetQueryStringWithoutSortOrder () %>'>
                            Auction / Property Number </a></b>
                    </td>
                    <td>
                        <b><a href='PaymentsReceived.aspx?SortOrder=5&<%# GetQueryStringWithoutSortOrder () %>'>
                            Payment Date </a></b>
                    </td>
                    <td>
                        <b><a href='PaymentsReceived.aspx?SortOrder=6&<%# GetQueryStringWithoutSortOrder () %>'>
                            Payment Amount </a></b>
                    </td>
                    <td>
                        <b><a href='PaymentsReceived.aspx?SortOrder=7&<%# GetQueryStringWithoutSortOrder () %>'>
                            Payment Method </a></b>
                    </td>
                    <td>
                        <b><a href='PaymentsReceived.aspx?SortOrder=8&<%# GetQueryStringWithoutSortOrder () %>'>
                            Balance </a></b>
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
                    <a href='<%# (((System.Data.DataRowView)Container.DataItem).Row["IfAuction"] is int) && ((int)((System.Data.DataRowView)Container.DataItem).Row["IfAuction"] == 1) ? CommonFunctions.PrepareURL (DataBinder.Eval(Container.DataItem, "ID", "{0:d}") + "/default.aspx", "Payments Received") : CommonFunctions.PrepareURL ("ViewProperty.aspx?PropertyID=" + DataBinder.Eval(Container.DataItem, "ID", "{0:d}"), "Payments Received") %>'>
                        <%# DataBinder.Eval (Container.DataItem, "ID", "{0}")%>
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
                    <%# DataBinder.Eval(Container.DataItem, "Balance", "{0:c}") %>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            <tr>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    Total:
                </td>
                <td>
                </td>
                <td>
                    <%# SumInvoiceAmounts () %>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                    <%# SumPaymentAmounts () %>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            </table>
        </FooterTemplate>
    </asp:Repeater>

    <noscript>
        <img src="http://www.watchwise.net/cgi-watchwise/monitor.cgi?<%= CommonFunctions.GetSiteAddress ().ToLower () %>:all" width="1" height="1">
    </noscript>

    <script language="javascript">
        document.write('<img src="http://www.watchwise.net/cgi-watchwise/monitorwise.cgi?URL=<%= CommonFunctions.GetSiteAddress ().ToLower () %>:all&LINK=',escape(document.referrer),'" height=1 width=1>')
    </script>
</asp:Content>
