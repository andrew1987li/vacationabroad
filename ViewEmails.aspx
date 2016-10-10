<%@ Page Language="C#" MasterPageFile="~/masterpage/NormalMaster.master" AutoEventWireup="true" CodeFile="ViewEmails.aspx.cs" Inherits="ViewEmails" Title="Vacations-Abroad.com: Vacation Rentals Holiday Rentals Vacations Holidays Cottages Cabins Homes Condos USA Mexico Caribbean Bahamas Canada France Italy Greece Spain England Brazil Costa Rica Scotland Portugal" %>


<asp:Content ID="head" ContentPlaceHolderID="head" runat="server">
    View Email
</asp:Content>
<asp:Content ID="links" ContentPlaceHolderID="links" runat="server">
    <link href="/Assets/css/viewemail.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="bodycontent" Runat="Server">
    <div class="internalpagewidth">
        <div class="newline">
<div align="left">
	<% if (BackLink.Visible) { %>
	<table bgcolor="#e4e4af" cellspacing="0" cellpadding="0" width="250" align="center" border="2">
		<tr>
			<td>
				<div align="center">
					<strong>
						<asp:hyperlink id="BackLink" runat="server" NavigateUrl="MyAccount.aspx">
							Return to My Account page
						</asp:hyperlink>
					</strong>
				</div>
			</td>
		</tr>
	</table>
	<br />
	<% } %>
	<% if (ifallow) { %>
	<asp:repeater id="Repeater1" runat="server" DataMember="Emails" DataSource="<%# EmailsSet %>">
		<HeaderTemplate>
			<table border="1" width="100%" cellpadding="0" cellspacing="0" bordercolor="#ffffff"
					bgcolor="#ffffff" align="center">
		</HeaderTemplate>
		<ItemTemplate>
				<tr>
				<td>
									<br />
					Date / Time Sent:
					<%# DataBinder.Eval(Container.DataItem, "DateTime", "{0}") %>
					<br />
					<%# GetText ((System.Data.DataRowView)Container.DataItem) %>
					</td>
				</tr>
		</ItemTemplate>
		<FooterTemplate>
			</table>
		</FooterTemplate>
	</asp:repeater>
	<% } %>
	<% if (AllowedWarning.Visible) { %>
	<br />
	<div align="center">
		<asp:Label ID="AllowedWarning" runat="server" Text="Label" ForeColor="Red" Font-Bold="true">You can review emails only for paid properties.</asp:Label>
	</div>
	<% } %>

	<noscript>
		<img src="http://www.watchwise.net/cgi-watchwise/monitor.cgi?<%= CommonFunctions.GetSiteAddress ().ToLower () %>:all"
			width="1" height="1" />
	</noscript>


	</div>
        </div>
    </div>

    <script src="/Assets/js/viewemail.js"></script>
</asp:Content>
