<%@ Page Language="C#" MasterPageFile="~/MasterPageNoCss.master" AutoEventWireup="true" CodeFile="ViewUser.aspx.cs" Inherits="ViewUser" Title="User Page" %>
<asp:Content ID="Content" ContentPlaceHolderID="Content" Runat="Server">
	<% if (BackLink.Visible) { %>
	<table cellspacing="0" cellpadding="0" width="250" align="center" bgcolor="#e4e4af" border="2">
		<tr>
			<td>
				<div align="center">
					<strong>
						<asp:HyperLink ID="BackLink" runat="server" NavigateUrl="default.aspx">
							Return to Home Page
						</asp:HyperLink>
					</strong>
				</div>
			</td>
		</tr>
	</table>
	<br />
	<% } %>
	<table bgcolor="#e4e4af" cellspacing="0" cellpadding="0" width="70%" align="center" border="0">
		<tr>
			<td align="center">
				<%# MainDataSet.Tables["Users"].Rows[0]["UserID"].ToString ().Length > 0 ? MainDataSet.Tables["Users"].Rows[0]["UserID"] : MainDataSet.Tables["Users"].Rows[0]["Username"]%><br />
				<%# (MainDataSet.Tables["Users"].Rows[0]["DateCreated"] is DateTime) ? "This User has been a customer since " + ((DateTime)MainDataSet.Tables["Users"].Rows[0]["DateCreated"]).ToLongDateString () : "" %>
			</td>
		</tr>
	</table>
	<br />
	<% if (ifproperties) { %>
	<div align="center">
		<a href='<%= CommonFunctions.PrepareURL ("UserProperties.aspx?UserID=" + userid.ToString (), "User Page") %>'>
			Current Property Listings
		</a>
	</div>
	<% } %>
	<% if (ifreviews) { %>
	<div align="center">
		<a href='<%= CommonFunctions.PrepareURL ("UserReviews.aspx?UserID=" + userid.ToString (), "User Page") %>'>
			Auction Reviews
		</a>
	</div>
	<br />
	<% } %>
	<% if (MainDataSet.Tables["Auctions"].Rows.Count > 0) { %>
	<asp:Repeater ID="FreeTrialList" runat="server" DataMember="Auctions" DataSource="<%# MainDataSet %>">
		<HeaderTemplate>
			<table border="0" bordercolor="#ffffff" cellpadding="0" cellspacing="1" width="85%" bgColor="#ffffff"
					align="center">
				<tr bgcolor="#cc9933">
					<td align="center">
					</td>
					<td align="center">
						<strong><font face="Arial, Helvetica, sans-serif" color="#ffffff" size="2">
							Item Title
						</font></strong>
					</td>
					<td align="center">
						<strong><font face="Arial, Helvetica, sans-serif" color="#ffffff" size="2">
							Date Ending
						</font></strong>
					</td>
					<td align="center">
						<strong><font face="Arial, Helvetica, sans-serif" color="#ffffff" size="2">
							Current Bid
						</font></strong>
					</td>
					<td align="center">
						<strong><font face="Arial, Helvetica, sans-serif" color="#ffffff" size="2">
							# of Bids
						</font></strong>
					</td>
					<td align="center">
						<strong><font face="Arial, Helvetica, sans-serif" color="#ffffff" size="2">
							Time Remaining
						</font></strong>
					</td>
				</tr>
		</HeaderTemplate>
		<ItemTemplate>
			<tr>
				<td align="center">
					<%# CommonFunctions.ShowAuctionPhoto (((System.Data.DataRowView)Container.DataItem).Row, "User Page")%>
				</td>
				<td align="center">
					<strong><font face="Arial, Helvetica, sans-serif" size="2">
						<a href='<%# CommonFunctions.PrepareURL (DataBinder.Eval(Container.DataItem, "ID", "{0:d}") + "/default.aspx", "User Page") %> '>
							<%# DataBinder.Eval(Container.DataItem, "Title", "{0}") %>
						</a>
					</font></strong>
				</td>
				<td align="center">
					<strong><font face="Arial, Helvetica, sans-serif" size="2">
						<%# DataBinder.Eval(Container.DataItem, "AuctionEnd", "{0:d}") %>
					</font></strong>
				</td>
				<td align="center">
					<strong><font face="Arial, Helvetica, sans-serif" size="2">
						<%# DataBinder.Eval(Container.DataItem, "BidAmount", "{0}") %>
					</font></strong>
				</td>
				<td align="center">
					<strong><font face="Arial, Helvetica, sans-serif" size="2">
						<%# DataBinder.Eval(Container.DataItem, "BidsNumber", "{0}") %>
					</font></strong>
				</td>
				<td align="center">
					<strong><font face="Arial, Helvetica, sans-serif" size="2">
						<%# ((DateTime)((System.Data.DataRowView)Container.DataItem).Row["AuctionEnd"] - DateTime.Now).Minutes %> Mins,
						<%# ((DateTime)((System.Data.DataRowView)Container.DataItem).Row["AuctionEnd"] - DateTime.Now).Hours%> Hours,
						<%# ((DateTime)((System.Data.DataRowView)Container.DataItem).Row["AuctionEnd"] - DateTime.Now).Days % 7%> Days,
						<%# ((DateTime)((System.Data.DataRowView)Container.DataItem).Row["AuctionEnd"] - DateTime.Now).Days / 7%> Weeks
					</font></strong>
				</td>
			</tr>
		</ItemTemplate>
		<FooterTemplate>
			</table>
		</FooterTemplate>
	</asp:Repeater>
	<% } %>
</asp:Content>

