<%@ Page Language="C#" MasterPageFile="~/MasterPageNoCss.master" AutoEventWireup="true" CodeFile="AgentAgents.aspx.cs" Inherits="AgentAgents" Title="Commission From My Agents" %>
<asp:Content ID="Content" ContentPlaceHolderID="Content" Runat="Server">
	<% if (BackLink.Visible) { %>
	<table cellspacing="0" cellpadding="0" width="250" align="center" bgcolor="#e4e4af" border="2">
		<tr>
			<td>
				<div align="center">
					<strong>
						<asp:HyperLink ID="BackLink" runat="server" NavigateUrl="OwnersList.aspx">
							Return to Owners list
						</asp:HyperLink>
					</strong>
				</div>
			</td>
		</tr>
	</table>
	<br />
	<% } %>
	<div align="center">
		<strong>
			<asp:Label ID="WelcomeLabel" runat="server" Height="16px" Width="100%">
				Welcome - Commission From My Agents
			</asp:Label>
		</strong>
	</div>
	<asp:Repeater ID="Repeater1" runat="server" DataMember="Commissions" DataSource="<%# MainDataSet %>">
		<HeaderTemplate>
			<table border="0" width="100%">
				<tr>
					<td align="center">
						Agent Name
					</td>
					<td align="center">
						Administrative Contact Name
					</td>
					<td align="center">
						Transaction Date
					</td>
					<td align="center">
						Invoice Amt
					</td>
					<td align="center">
						Agent Fee
					</td>
					<td align="center">
						Date Paid
					</td>
				</tr>
		</HeaderTemplate>
		<ItemTemplate>
			<tr>
				<td align="center">
					<%# DataBinder.Eval(Container.DataItem, "Username", "{0}") %>
				</td>
				<td align="center">
					<%# DataBinder.Eval(Container.DataItem, "FirstName", "{0}") %>
					<%# DataBinder.Eval(Container.DataItem, "LastName", "{0}") %>
				</td>
				<td align="center">
					<%# DataBinder.Eval(Container.DataItem, "InvoiceDate", "{0:d}") %>
				</td>
				<td align="center">
					<%# DataBinder.Eval(Container.DataItem, "InvoiceAmount", "{0:c}") %>
				</td>
				<td align="center">
					<%# DataBinder.Eval(Container.DataItem, "PaymentAmount", "{0:c}") %>
				</td>
				<td align="center">
					<%# DataBinder.Eval (Container.DataItem, "DatePaid", "{0:d}")%>
				</td>
			</tr>
		</ItemTemplate>
		<FooterTemplate>
			</table>
		</FooterTemplate>
	</asp:Repeater>
</asp:Content>

