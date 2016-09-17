<%@ Page Language="C#" MasterPageFile="~/MasterPageNoCss.master" AutoEventWireup="true" CodeFile="AgentClients.aspx.cs" Inherits="AgentClients" Title="My Clients" %>
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
				Welcome - My Clients
			</asp:Label>
		</strong>
	</div>
	<asp:Repeater ID="Repeater1" runat="server" DataMember="Users" DataSource="<%# MainDataSet %>">
		<HeaderTemplate>
			<table border="0" width="100%">
				<tr>
					<td align="center">
						Company Name
					</td>
					<td align="center">
						Administrative Contact Name
					</td>
					<td align="center">
						Telephone
					</td>
					<td align="center">
						Email Contact
					</td>
				</tr>
		</HeaderTemplate>
		<ItemTemplate>
			<tr>
				<td align="center">
					<%# DataBinder.Eval(Container.DataItem, "CompanyName", "{0}") %>
				</td>
				<td align="center">
					<%# DataBinder.Eval(Container.DataItem, "FirstName", "{0}") %>
					<%# DataBinder.Eval(Container.DataItem, "LastName", "{0}") %>
				</td>
				<td align="center">
					<%# DataBinder.Eval(Container.DataItem, "PrimaryTelephone", "{0}") %>
				</td>
				<td align="center">
					<%# DataBinder.Eval(Container.DataItem, "Email", "{0}") %>
				</td>
			</tr>
		</ItemTemplate>
		<FooterTemplate>
			</table>
		</FooterTemplate>
	</asp:Repeater>
</asp:Content>