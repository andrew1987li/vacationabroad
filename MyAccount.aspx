<%@ page language="C#" masterpagefile="~/MasterPageNoCss.master" autoeventwireup="true" CodeFile="~/MyAccount.aspx.cs" inherits="MyAccount" title="My Account" enableEventValidation="false" %>
<asp:Content ID="Content" ContentPlaceHolderID="Content" Runat="Server">
	<% if (AuthenticationManager.IfAdmin && BackLink.Visible) { %>
	<table cellspacing="0" cellpadding="0" width="250" align="center" bgcolor="#F5EDE3" border="2">
		<tr>
			<td align="center">
				<strong>
					<asp:HyperLink ID="BackLink" runat="server" NavigateUrl="OwnersList.aspx">
						Return to Owners list
					</asp:HyperLink>
				</strong>
			</td>
		</tr>
	</table>
	<br />
	<% } %>
	<div align="center">
		<strong>
			<asp:Label ID="WelcomeLabel" runat="server" Height="16px" Width="100%">
				Welcome
			</asp:Label>
		</strong>
	</div>
	<br />
	<table cellspacing="0" cellpadding="0" width="250" align="center" bgcolor="#F5EDE3" border="2">
		<tr>
			<td>
				<div align="center">
					<strong>
						<asp:Label ID="UserIDLabel" runat="server">
						</asp:Label>
					</strong>
				</div>
				<div align="center">
					<strong>
						<a href='<%= CommonFunctions.PrepareURL ("OwnerInformation.aspx?UserID=" + userid.ToString ()) %>'>
							Contact Details
						</a>
					</strong>
				</div>
				<div align="center">
					<strong>
						<a href='<%= CommonFunctions.PrepareURL ("AccountInformation.aspx?UserID=" + userid.ToString ()) %>'>
							Email / Password
						</a>
					</strong>
				</div>
				<% if (ifinvoices) { %>
				<div align="center">
					<strong>
						<a href='<%= CommonFunctions.PrepareURL ("ViewInvoices.aspx?UserID=" + userid.ToString ()) %>'>
							View Invoices
						</a>
					</strong>
				</div>
				<% } %>
			</td>
		</tr>
	</table>
	<br />
	<% if (MessageLabel.Visible) { %>
	<div align="center">
		<asp:Label ID="MessageLabel" runat="server" Text="" Visible="False"></asp:Label><br />
	</div>
	<% } %>
	<% if (MessageLink.Visible) { %>
	<div align="center">
		<asp:HyperLink ID="MessageLink" runat="server" Visible="False">Click here to return to Item you were viewing</asp:HyperLink><br />
	</div>
	<% } %>
	<br />
	<div align="center">
		<asp:Button ID="NewProperty" runat="server" Text="List A Property" Width="150px" OnClick="NewProperty_Click" />&nbsp;
		<asp:Button ID="EditListings" runat="server" OnClick="EditListings_Click" Text="Your Listings" Width="150px" />
	</div>
	<div align="center">
		<asp:Button ID="Agent" runat="server" OnClick="Agent_Click" Text="Become An Agent" Width="150px" />
	</div>
	<br />
	<% if (ifauctionswon) { %>
	<div align="center">
		<a href='<%= CommonFunctions.PrepareURL ("AuctionsWonList.aspx?UserID=" + userid.ToString (), "*User* Account") %>'>
			Auction Items Won (Add a Review)
		</a>
	</div>
	<% } %>
	<% if (ifwatchitems && (AuthenticationManager.UserID == userid)) { %>
	<div align="center">
		<a href='<%= CommonFunctions.PrepareURL ("WatchList.aspx", "*User* Account") %>'>
			Watch Items
		</a>
	</div>
	<% } %>
	<% if (ifreviews) { %>
	<div align="center">
		<a href='<%= CommonFunctions.PrepareURL ("UserReviews.aspx?UserID=" + userid.ToString (), "*User* Account") %>'>
			Reviews of My Auctions
		</a>
	</div>
	<% } %>
	<% if (MainDataSet.Tables["Auctions"].Rows.Count > 0) { %>
	<asp:Repeater ID="FreeTrialList" runat="server" DataMember="Auctions" DataSource="<%# MainDataSet %>">
		<HeaderTemplate>
			<table border="0" bordercolor="#ffffff" cellpadding="0" cellspacing="1" width="85%" bgColor="#ffffff"
					align="center">
				<tr bgcolor="#cc9933">
					<td width="11%" align="center">
					</td>
					<td width="16%" align="center">
						<strong><font face="Arial, Helvetica, sans-serif" color="#ffffff" size="2">
							Item Title
						</font></strong>
					</td>
					<td width="13%" align="center">
						<strong><font face="Arial, Helvetica, sans-serif" color="#ffffff" size="2">
							Date Ending
						</font></strong>
					</td>
					<td width="13%" align="center">
						<strong><font face="Arial, Helvetica, sans-serif" color="#ffffff" size="2">
							Current Bid
						</font></strong>
					</td>
					<td width="22%" align="center">
						<strong><font face="Arial, Helvetica, sans-serif" color="#ffffff" size="2">
							# of Bids
						</font></strong>
					</td>
					<td width="25%" align="center">
						<strong><font face="Arial, Helvetica, sans-serif" color="#ffffff" size="2">
							Time Remaining
						</font></strong>
					</td>
				</tr>
		</HeaderTemplate>
		<ItemTemplate>
			<tr>
				<td width="11%" align="center">
					<%# CommonFunctions.ShowAuctionPhoto (((System.Data.DataRowView)Container.DataItem).Row, "*User* Account")%>
				</td>
				<td width="16%" align="center">
					<strong><font face="Arial, Helvetica, sans-serif" size="2">
						<a href='<%# CommonFunctions.PrepareURL (DataBinder.Eval(Container.DataItem, "ID", "{0:d}") + "/default.aspx", "*User* Account") %> '>
							<%# DataBinder.Eval(Container.DataItem, "Title", "{0}") %>
						</a>
					</font></strong>
				</td>
				<td width="13%" align="center">
					<strong><font face="Arial, Helvetica, sans-serif" size="2">
						<%# DataBinder.Eval(Container.DataItem, "AuctionEnd", "{0:d}") %>
					</font></strong>
				</td>
				<td width="13%" align="center">
					<strong><font face="Arial, Helvetica, sans-serif" size="2">
						<%# DataBinder.Eval(Container.DataItem, "BidAmount", "{0}") %>
					</font></strong>
				</td>
				<td width="22%" align="center">
					<strong><font face="Arial, Helvetica, sans-serif" size="2">
						<%# DataBinder.Eval(Container.DataItem, "BidsNumber", "{0}") %>
					</font></strong>
				</td>
				<td width="25%" align="center">
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
  
	<script language=javascript type=text/javascript>
	<!--

	function AskDelete(propertyid)
	{
		if (window.confirm ("Are you sure you want to do this?"))
			window.location.href="DeleteProperty.aspx?PropertyID=" + propertyid + "&BackLink=MyAccount.aspx%3F<%# System.Web.HttpUtility.UrlEncode (Request.QueryString.ToString ()) %>"
	}

	// -->
	</script>


</asp:Content>
