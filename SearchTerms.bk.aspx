<%--<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SearchTerms.aspx.cs" Inherits="SearchTerms" Title="Search Terms" %>--%>
<%@ Page Language="C#" MasterPageFile="~/masterpage/NormalMaster.master" AutoEventWireup="true" CodeFile="SearchTerms.bk.aspx.cs" Inherits="SearchTerms" Title="Search Terms" EnableEventValidation="False" %>

<asp:Content ID="links" ContentPlaceHolderID="links" runat="server">
    <link href="/Assets/css/search.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content" ContentPlaceHolderID="bodycontent" Runat="server">
    <div class="internalpagewidth">
  <asp:Label id="lbl1" runat="server" style="display:none"></asp:Label>

        <div class="newline" style="display:none;">
		Enter Key Words<asp:TextBox ID="RefineTerms" runat="server" />
		<asp:Button ID="SearchButton" runat="server" Text="Search" /><br />
		<asp:RadioButton ID="OnlyAuction" runat="server" Text="Search Auction Items" GroupName="1" Visible="false"/>
		<asp:RadioButton ID="OnlyNonAuction" runat="server" Text="Vacation Properties" GroupName="1" Visible="false" />
		<asp:RadioButton ID="Both" runat="server" Text="Vacation Properties" GroupName="1" Visible="false" /><br />
        </div>

    
	<div align="center">

	</div>
	<br />
	<% if (MainDataSet.Tables["Auctions"].Rows.Count > 0) { %>
        <div class="newline">
	        <div class="centered">
		        <strong>Auction Items:</strong>
		        Sort in order of
		        <asp:DropDownList ID="SortOrder" runat="server" Width="202px"
				        AutoPostBack="True">
			        <asp:ListItem Value="1">Least Time Remaining - First</asp:ListItem>
			        <asp:ListItem Value="2">Most Time Remaining - First</asp:ListItem>
			        <asp:ListItem Value="3">Lowest Current Bid</asp:ListItem>
			        <asp:ListItem Value="4">Highest Current Bid</asp:ListItem>
		        </asp:DropDownList>
	        </div>
        </div>

	<br />
	<div class="newline">
		<strong>Auction Items</strong>
	</div>
        <div class="newline">
	<asp:Repeater ID="Repeater2" runat="server" DataMember="Auctions" DataSource="<%# MainDataSet %>">
		<HeaderTemplate>
			<table bordercolor="#ffffff" cellspacing="1" cellpadding="0" width="100%" align="center" bgcolor="#ffffff"
					border="0">
				<tr bgcolor="#cc9933">
					<td align="center" width="69" bgcolor="#ffffff">
					</td>
					<td align="center">
						<font color="#ffffff" size="2"><strong>Title</strong></font>
					</td>
					<td align="center" width="64">
						<font color="#ffffff" size="2"><strong>Country</strong></font>
					</td>
					<td align="center" width="132">
						<font color="#ffffff" size="2"><strong>State/Province</strong></font>
					</td>
					<td align="center" width="55">
						<font color="#ffffff" size="2"><strong>City</strong></font>
					</td>
					<td align="center" width="59">
						<font color="#ffffff" size="2"><strong>Min Bid</strong></font>
					</td>
					<td align="center" width="58">
						<font color="#ffffff" size="2"><strong>Current Bid</strong></font>
					</td>
					<td align="center" width="109">
						<font color="#ffffff" size="2"><strong>Auction Ending</strong></font>
					</td>
					<td align="center">
						<font color="#ffffff" size="2"><strong>Time Remaining</strong></font>
					</td>
				</tr>
		</HeaderTemplate>
		<ItemTemplate>
			<tr bgcolor="#cfdfef">
				<td bgcolor="#ffffff" align="center">
					<%# CommonFunctions.ShowAuctionPhoto (((System.Data.DataRowView)Container.DataItem).Row, "Search Terms")%>
				</td>
				<td bgcolor="#e4e4af" align="center">
					<font size="2">
						<a href='<%# CommonFunctions.PrepareURL (DataBinder.Eval(Container.DataItem, "ID", "{0}") + "/default.aspx", "Search Terms") %>'>
							<%# DataBinder.Eval(Container.DataItem, "Title", "{0}") %>
						</a>
					</font>
				</td>
				<td align="center">
					<font size="2"><%# DataBinder.Eval(Container.DataItem, "Country", "{0}") %></font>
				</td>
				<td align="center">
					<font size="2"><%# DataBinder.Eval(Container.DataItem, "StateProvince", "{0}") %></font>
				</td>
				<td align="center">
					<font size="2"><%# DataBinder.Eval(Container.DataItem, "City", "{0}") %></font>
				</td>
				<td align="center">
					<font size="2"><%# DataBinder.Eval(Container.DataItem, "MinimumBid", "{0}") %></font>
				</td>
				<td align="center">
					<font size="2"><%# DataBinder.Eval(Container.DataItem, "BidAmount", "{0}") %></font>
				</td>
				<td align="center">
					<font size="2"><%# DataBinder.Eval(Container.DataItem, "AuctionEnd", "{0:d}") %></font>
				</td>
				<td align="center">
					<font size="2">
						<%# ((DateTime)((System.Data.DataRowView)Container.DataItem).Row["AuctionEnd"] - DateTime.Now).Minutes%> Minutes,
						<%# ((DateTime)((System.Data.DataRowView)Container.DataItem).Row["AuctionEnd"] - DateTime.Now).Hours%> Hours,
						<%# ((DateTime)((System.Data.DataRowView)Container.DataItem).Row["AuctionEnd"] - DateTime.Now).Days % 7%> Days,
						<%# ((DateTime)((System.Data.DataRowView)Container.DataItem).Row["AuctionEnd"] - DateTime.Now).Days / 7%> Weeks
					</font>
				</td>
			</tr>
		</ItemTemplate>
		<FooterTemplate>
			</table>
		</FooterTemplate>
	</asp:Repeater>
        </div>

	<% } %>
	<% if (MainDataSet.Tables["Properties"].Rows.Count > 0) { %>
	<div class="centered">
		<strong>Vacation Property Results For Keyword "<%=terms %>"</strong>
	</div>
    <div class="newline centered">
	<asp:Repeater ID="Repeater1" runat="server" DataMember="Properties" DataSource="<%# MainDataSet %>">
		<HeaderTemplate>
			<table cellspacing="1" cellpadding="0" width="750" align="center" border="0" bgcolor="#333366">
				<tr bgcolor="#333366">
					<td bgcolor="#ffffff" align="center">
					</td>
					<td bgcolor="#333366" align="center">
						<font color="#ffffff" size="2"><strong>Name</strong></font>
					</td>
					<td bgcolor="#333366" align="center">
						<font color="#ffffff" size="2"><strong>Country</strong></font>
					</td>
					<td bgcolor="#333366" align="center">
						<font color="#ffffff" size="2"><strong>State/Province</strong></font>
					</td>
					<td bgcolor="#333366" align="center">
						<font color="#ffffff" size="2"><strong>City</strong></font>
					</td>
					<td bgcolor="#333366" align="center">
						<font color="#ffffff" size="2"><strong>Prop Type</strong></font>
					</td>
					<td bgcolor="#333366" align="center">
						<font color="#ffffff" size="2"><strong>Bedrms</strong></font>
					</td>
					<td bgcolor="#333366" align="center">
						<font color="#ffffff" size="2"><strong>Sleeps</strong></font>
					</td>
					<td bgcolor="#333366" align="center">
						<font color="#ffffff" size="2"><strong>Pool</strong></font>
					</td>
				</tr>
		</HeaderTemplate>
		<ItemTemplate>
			<tr bgcolor="#ffffff">
				<td bgcolor="#ffffff" align="center">
					<%# CommonFunctions.ShowPropertyPhotoWithoutBacklink (((System.Data.DataRowView)Container.DataItem).Row)%>
				</td>
				<td bgcolor="#ede9ed" align="center">
<%--					<a href='<%# CommonFunctions.PrepareURL ("ViewProperty.aspx?PropertyID=" + DataBinder.Eval(Container.DataItem, "ID", "{0:d}"), "Watch List") %>'>
						<%# DataBinder.Eval(Container.DataItem, "Name", "{0}") %>
					</a>
--%>                
                    <a href='<%# CommonFunctions.PrepareURL (DataBinder.Eval(Container.DataItem, "Country", "{0}").Replace (" ", "_").ToLower () + "/" + DataBinder.Eval(Container.DataItem, "StateProvince", "{0}").Replace (" ", "_").ToLower () + "/" + DataBinder.Eval(Container.DataItem, "City", "{0}").Replace (" ", "_").ToLower () + "/" + DataBinder.Eval(Container.DataItem, "ID", "{0:d}") + "/default.aspx") %>'>
                        <%# DataBinder.Eval(Container.DataItem, "Name", "{0}") %>
                    </a>
				</td>
				<td align="center">
					<%# DataBinder.Eval(Container.DataItem, "Country", "{0}") %>
				</td>
				<td align="center">
					<%# DataBinder.Eval(Container.DataItem, "StateProvince", "{0}") %>
				</td>
				<td align="center">
					<%# DataBinder.Eval(Container.DataItem, "City", "{0}") %>
				</td>
				<td align="center">
					<%# DataBinder.Eval(Container.DataItem, "Type", "{0}") %>
				</td>
				<td align="center">
					<%# DataBinder.Eval(Container.DataItem, "NumBedrooms", "{0}") %>
				</td>
				<td align="center">
					<%# DataBinder.Eval(Container.DataItem, "NumSleeps", "{0}") %>
				</td>
				<td align="center">
					<%# DataBinder.Eval(Container.DataItem, "Pool", "{0}") %>
				</td>
			</tr>
		</ItemTemplate>
		<FooterTemplate>
			</table>
		</FooterTemplate>
	</asp:Repeater>
    </div>

	<% } %>
	<div align="center">
		<% if (numitems > 25) for (int i = 0; i <= numitems / 25; i++) { %>
			<a href='<%= CommonFunctions.PrepareURL ("SearchTerms.aspx?SearchTerms=" + System.Web.HttpUtility.UrlEncode (terms) + "&SortOrder=" + sorder.ToString () + "&StartPos=" + (i * 25).ToString () + "&Only=" + ifonly.ToString (), backlinktext) %>'>
				<%= (i + 1).ToString () %>
			</a><%= (i < numitems / 25 - 1) ? "," : ""%>
		<% } %>
	</div>
    </div>
  
    <script src="/Assets/js/search.js"></script>
</asp:Content>