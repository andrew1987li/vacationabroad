<%@ Page Language="C#" MasterPageFile="~/MasterPageNoCss.master" AutoEventWireup="true" CodeFile="WatchList.aspx.cs" Inherits="WatchList" Title="Watch List" EnableEventValidation="false" %>
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
	<div align="center">
		<strong>
			<asp:Label ID="WelcomeLabel" runat="server" Height="16px" Width="100%">
				Welcome
			</asp:Label>
		</strong>
	</div>
	<br />
	<br />
	<% if (MainDataSet.Tables["Auctions"].Rows.Count > 0) { %>
	<div align="center">
		<strong>Auction Items</strong>
	</div>
	<br />
	<asp:Repeater ID="FreeTrialList" runat="server" DataMember="Auctions" DataSource="<%# MainDataSet %>">
		<HeaderTemplate>
			<table width="85%" border="0" align="center" cellpadding="0" cellspacing="1" bordercolor="#FFFFFF" bgcolor="#cfdfef">
				<tr bgcolor="#cc9933">
					<td align="center">
					</td>
					<td align="center">
						<font face="Arial, Helvetica, sans-serif" size="2" color="#ffffff"><strong>
							Item Title
						</strong></font>
					</td>
					<td align="center">
						<font face="Arial, Helvetica, sans-serif" size="2" color="#ffffff"><strong>
							Date Ending
						</strong></font>
					</td>
					<td align="center">
						<font face="Arial, Helvetica, sans-serif" size="2" color="#ffffff"><strong>
							Current Bid
						</strong></font>
					</td>
					<td align="center">
						<font face="Arial, Helvetica, sans-serif" size="2" color="#ffffff"><strong>
							No of Bids
						</strong></font>
					</td>
					<td align="center">
						<font face="Arial, Helvetica, sans-serif" size="2" color="#ffffff"><strong>
							Bid Now
						</strong></font>
					</td>
					<td align="center">
						<font face="Arial, Helvetica, sans-serif" size="2" color="#ffffff"><strong>
							Delete
						</strong></font>
					</td>
				</tr>
		</HeaderTemplate>
		<ItemTemplate>
			<tr bgcolor="#FFFFFF">
				<td align="center">
					<%# CommonFunctions.ShowAuctionPhoto (((System.Data.DataRowView)Container.DataItem).Row, "Watch List")%>
				</td>
				<td align="center">
					<a href='<%# CommonFunctions.PrepareURL (DataBinder.Eval(Container.DataItem, "ID", "{0:d}") + "/default.aspx", "Watch List") %> '>
						<%# DataBinder.Eval(Container.DataItem, "Title", "{0}") %>
					</a>
				</td>
				<td align="center">
					<%# DataBinder.Eval(Container.DataItem, "AuctionEnd", "{0:d}") %>
				</td>
				<td align="center">
					<%# DataBinder.Eval(Container.DataItem, "BidAmount", "{0}") %>
				</td>
				<td align="center">
					<%# DataBinder.Eval(Container.DataItem, "BidsNumber", "{0}") %>
				</td>
				<td align="center">
					<a href='<%# CommonFunctions.PrepareURL ("BidNow.aspx?AuctionID=" + DataBinder.Eval(Container.DataItem, "ID", "{0:d}"), "Watch List") %> '>
						<%# ((DateTime)((System.Data.DataRowView)Container.DataItem).Row["AuctionEnd"] > DateTime.Now) ? "Bid Now" : "" %>
					</a>
				</td>
				<td align="center">
					<input type="checkbox" name='Delete<%# DataBinder.Eval(Container.DataItem, "WatchListItemID", "{0}") %>'
						title="Delete this item?" />
				</td>
			</tr>
		</ItemTemplate>
		<FooterTemplate>
				<tr bgcolor="#FFFFFF" bordercolor="#FFFFFF">
					<td align="center">
					</td>
					<td align="center">
					</td>
					<td align="center">
					</td>
					<td align="center">
					</td>
					<td align="center">
					</td>
					<td align="center">
					</td>
					<td align="center">
						<asp:Button runat="server" ID="DeleteAuctionItems" Text="Delete items" OnClick="DeleteItems_Click" />
					</td>
				</tr>
			</table>
		</FooterTemplate>
	</asp:Repeater>
	<br />
	<% } %>
	<% if (MainDataSet.Tables["Properties"].Rows.Count > 0) { %>
	<div align="center">
		<strong>Non Auction Items</strong>
	</div>
	<br />
	<asp:Repeater ID="Repeater1" runat="server" DataMember="Properties" DataSource="<%# MainDataSet %>">
		<HeaderTemplate>
			<table width="85%" border="0" align="center" cellpadding="0" cellspacing="1" bordercolor="#FFFFFF" bgcolor="#cfdfef">
				<tr bgcolor="#cc9933">
					<td align="center">
					</td>
					<td align="center">
						<font face="Arial, Helvetica, sans-serif" size="2" color="#ffffff"><strong>Name</strong></font>
					</td>
					<td align="center">
						<font face="Arial, Helvetica, sans-serif" size="2" color="#ffffff"><strong>City</strong></font>
					</td>
					<td align="center">
						<font face="Arial, Helvetica, sans-serif" size="2" color="#ffffff"><strong>State</strong></font>
					</td>
					<td align="center">
						<font face="Arial, Helvetica, sans-serif" size="2" color="#ffffff"><strong>Country</strong></font>
					</td>
					<td align="center">
						<font face="Arial, Helvetica, sans-serif" size="2" color="#ffffff"><strong>Type</strong></font>
					</td>
					<td align="center">
						<font face="Arial, Helvetica, sans-serif" size="2" color="#ffffff"><strong>Bd</strong></font>
					</td>
					<td align="center">
						<font face="Arial, Helvetica, sans-serif" size="2" color="#ffffff"><strong>Sleeps</strong></font>
					</td>
					<td align="center">
						<font face="Arial, Helvetica, sans-serif" size="2" color="#ffffff"><strong>Delete</strong></font>
					</td>
				</tr>
		</HeaderTemplate>
		<ItemTemplate>
			<tr bgcolor="#FFFFFF">
				<td align="center">
					<%# CommonFunctions.ShowPropertyPhoto (((System.Data.DataRowView)Container.DataItem).Row, "Watch List")%>
				</td>
				<td bgcolor="#ffffff">
					<a href='<%# CommonFunctions.PrepareURL ("ViewProperty.aspx?PropertyID=" + DataBinder.Eval(Container.DataItem, "ID", "{0:d}"), "Watch List") %>'>
						<%# DataBinder.Eval(Container.DataItem, "Name", "{0}") %>
					</a>
				</td>
				<td bgcolor="#ffffff" align="center">
					<%# DataBinder.Eval(Container.DataItem, "City", "{0}") %>
				</td>
				<td bgcolor="#ffffff" align="center">
					<%# DataBinder.Eval(Container.DataItem, "StateProvince", "{0}") %>
				</td>
				<td bgcolor="#ffffff" align="center">
					<%# DataBinder.Eval(Container.DataItem, "Country", "{0}") %>
				</td>
				<td bgcolor="#ffffff" align="center">
					<%# DataBinder.Eval(Container.DataItem, "Type", "{0}") %>
				</td>
				<td bgcolor="#ffffff" align="center">
					<%# DataBinder.Eval(Container.DataItem, "NumBedrooms", "{0}") %>
				</td>
				<td bgcolor="#ffffff" align="center">
					<%# DataBinder.Eval(Container.DataItem, "NumSleeps", "{0}") %>
				</td>
				<td bgcolor="#ffffff" align="center">
					<input type="checkbox" name='Delete<%# DataBinder.Eval(Container.DataItem, "WatchListItemID", "{0}") %>'
						title="Delete this item?" />
				</td>
			</tr>
		</ItemTemplate>
		<FooterTemplate>
				<tr bgcolor="#FFFFFF" bordercolor="#FFFFFF">
					<td align="center">
					</td>
					<td bgcolor="#ffffff">
					</td>
					<td bgcolor="#ffffff" align="center">
					</td>
					<td bgcolor="#ffffff" align="center">
					</td>
					<td bgcolor="#ffffff" align="center">
					</td>
					<td bgcolor="#ffffff" align="center">
					</td>
					<td bgcolor="#ffffff" align="center">
					</td>
					<td bgcolor="#ffffff" align="center">
					</td>
					<td bgcolor="#ffffff" align="center">
						<asp:Button runat="server" ID="DeletePropertyItems" Text="Delete items" OnClick="DeleteItems_Click" />
					</td>
				</tr>
			</table>
		</FooterTemplate>
	</asp:Repeater>
	<% } %>
</asp:Content>

