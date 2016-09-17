<%@ Page Language="C#" MasterPageFile="~/MasterPageNoCss.master" AutoEventWireup="true" CodeFile="CommissionPayable.aspx.cs" Inherits="CommissionPayable" Title="Commission Payable" %>
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
	<div align="center">
		Commission Payable
	</div>
	<br />
	<asp:Repeater ID="Repeater1" runat="server" DataMember="Users" DataSource="<%# MainDataSet %>">
		<HeaderTemplate>
			<table border="0" width="100%">
				<tr>
					<td align="center">
						Agent Name
					</td>
					<td align="center">
						Total Amt Paid
					</td>
					<td align="center">
						Amt Due - Auctions
					</td>
					<td align="center">
						Amt Due - Annual
					</td>
					<td align="center">
						Date Last Payment
					</td>
				</tr>
		</HeaderTemplate>
		<ItemTemplate>
			<tr>
				<td align="center">
					<a href='<%# CommonFunctions.PrepareURL ("AgentCommissionPayable.aspx?UserID=" + DataBinder.Eval(Container.DataItem, "ID", "{0}"), "Commission Payable") %>'>
						<%# DataBinder.Eval(Container.DataItem, "LastName", "{0}") %>,
						<%# DataBinder.Eval(Container.DataItem, "FirstName", "{0}") %>
					</a>
				</td>
				<td align="center">
					<%# DataBinder.Eval (Container.DataItem, "AmountPaid", "{0:c}")%>
				</td>
				<td align="center">
					<%# DataBinder.Eval (Container.DataItem, "AmountDueAuctions", "{0:c}")%>
				</td>
				<td align="center">
					<%# DataBinder.Eval (Container.DataItem, "AmountDueAnnual", "{0:c}") %>
				</td>
				<td align="center">
					<%# DataBinder.Eval (Container.DataItem, "LastPaymentDate", "{0:d}") %>
				</td>
			</tr>
		</ItemTemplate>
		<FooterTemplate>
			</table>
		</FooterTemplate>
	</asp:Repeater>
</asp:Content>

