<%@ Page Language="C#" MasterPageFile="~/MasterPageNoCss.master" AutoEventWireup="true" CodeFile="AgentCommissionPayable.aspx.cs" Inherits="AgentCommissionPayable" Title="Commission Payable by Agent" %>
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
		Commission Payable to
		<%# MainDataSet.Tables["Users"].Rows[0]["FirstName"] %>
		<%# MainDataSet.Tables["Users"].Rows[0]["LastName"] %><br />
		<%# MainDataSet.Tables["Users"].Rows[0]["PrimaryTelephone"] %><br />
		<%# MainDataSet.Tables["Users"].Rows[0]["Email"] %>
	</div>
	<br />
	<asp:Repeater ID="Repeater1" runat="server" DataMember="Commissions" DataSource="<%# MainDataSet %>">
		<HeaderTemplate>
			<table border="0" width="100%">
				<tr>
					<td align="center">
						<strong>
							Transaction No
						</strong>
					</td>
					<td align="center">
						<strong>
							Transaction Date
						</strong>
					</td>
					<td align="center">
						<strong>
							Amount $
						</strong>
					</td>
					<td align="center">
						<strong>
							Transaction Type
						</strong>
					</td>
					<td align="center">
						<strong>
							Balance Due
						</strong>
					</td>
					<td align="center">
						<strong>
							Date Paid
						</strong>
					</td>
					<td align="center">
						<strong>
							Paid?
						</strong>
					</td>
				</tr>
		</HeaderTemplate>
		<ItemTemplate>
			<tr>
				<td align="center">
					<%# DataBinder.Eval (Container.DataItem, "TransactionNumber", "{0}")%>
				</td>
				<td align="center">
					<%# DataBinder.Eval (Container.DataItem, "InvoiceDate", "{0:d}") %>
				</td>
				<td align="center">
					<%# DataBinder.Eval (Container.DataItem, "PaymentAmount", "{0:c}")%>
				</td>
				<td align="center">
					<%# DataBinder.Eval (Container.DataItem, "TransactionType", "{0}") %>
				</td>
				<td align="center">
					<%# DataBinder.Eval (Container.DataItem, "BalanceDue", "{0:c}") %>
				</td>
				<td align="center">
					<%# DataBinder.Eval (Container.DataItem, "DatePaid", "{0:d}") %>
				</td>
				<td align="center">
					<input type="checkbox"
						name='Paid<%# DataBinder.Eval(Container.DataItem, "ID", "{0}") %>'
						<%# (((System.Data.DataRowView)Container.DataItem).Row["DatePaid"] is DBNull) ? "" : "checked disabled" %> />
				</td>
			</tr>
		</ItemTemplate>
		<FooterTemplate>
			</table>
		</FooterTemplate>
	</asp:Repeater>
	<div align="right">
		<asp:Button ID="SubmitButton" runat="server" OnClick="SubmitButton_Click" Text="Submit" />
	</div>
</asp:Content>