<%@ Page Language="C#" MasterPageFile="~/MasterPageNoCss.master" AutoEventWireup="true" CodeFile="UserReviews.aspx.cs" Inherits="UserReviews" Title="User Auction Reviews" %>
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
			Auction Reviews for
			<a href='<%# CommonFunctions.PrepareURL ("ViewUser.aspx?UserID=" + userid.ToString (), "User Reviews") %>'>
				<%= username %>
			</a>
		</strong>
	</div>
	<asp:Repeater ID="Repeater1" runat="server" DataMember="Reviews" DataSource="<%# MainDataSet %>">
		<HeaderTemplate>
			<table bordercolor="#ffffff" cellspacing="1" cellpadding="0" width="100%" align="center" bgcolor="#ffffff"
					border="0">
				<tr bgcolor="#cc9933">
					<td align="center" nowrap>
						<font color="#ffffff" size="2"><strong>
							To view the comments about a specific auction click on link.
						</strong></font>
						<br />
						<font color="red" size="2">
							(this link will take you to a page where the comments can be viewed.)
						</font>
					</td>
					<td width="15%" align="center" nowrap>
						<font color="#ffffff" size="2"><strong>Auction Date</strong></font>
					</td>
					<td width="10%" align="center" nowrap>
						<font color="#ffffff" size="2"><strong>Auction</strong></font>
					</td>
					<td width="10%" align="center" nowrap>
						<font color="#ffffff" size="2"><strong>Accurately Represented</strong></font>
					</td>
					<td width="10%" align="center" nowrap>
						<font color="#ffffff" size="2"><strong>Customer Service</strong></font>
					</td>
					<td width="10%" align="center" nowrap>
						<font color="#ffffff" size="2"><strong>Cleanliness</strong></font>
					</td>
					<td width="10%" align="center" nowrap>
						<font color="#ffffff" size="2"><strong>Good Value</strong></font>
					</td>
				</tr>
		</HeaderTemplate>
		<ItemTemplate>
			<tr>
				<td bgcolor="#ffffff">
					<font size="2">
						<a href="#" onclick="window.open('<%# CommonFunctions.PrepareURL ("ViewReview.aspx?AuctionID=" + MainDataSet.Tables["Reviews"].Rows[0]["ID"].ToString (), "User Reviews") %>', 'Review', 'width=500,height=400,toolbar=0,resizable=0')">
							<%# DataBinder.Eval(Container.DataItem, "Title", "{0}") %>
						</a>
					</font>
				</td>
				<td width="15%" bgcolor="#ffffff">
					<%# DataBinder.Eval(Container.DataItem, "AuctionStart", "{0:D}") %>
				</td>
				<td bgcolor="#ff9900" align="center">
					<font size="2">
						<%# DrawAsterisks (((System.Data.DataRowView)Container.DataItem).Row["Auction"])%>
					</font>
				</td>
				<td bgcolor="#ffcc33" align="center">
					<font size="2">
						<%# DrawAsterisks (((System.Data.DataRowView)Container.DataItem).Row["AccuratelyRepresented"])%>
					</font>
				</td>
				<td bgcolor="#ffcc99" align="center">
					<font size="2">
						<%# DrawAsterisks (((System.Data.DataRowView)Container.DataItem).Row["CustomerService"])%>
					</font>
				</td>
				<td bgcolor="#cccc66" align="center">
					<font size="2">
						<%# DrawAsterisks (((System.Data.DataRowView)Container.DataItem).Row["Cleanliness"])%>
					</font>
				</td>
				<td bgcolor="#cfdfef" align="center">
					<font size="2">
						<%# DrawAsterisks (((System.Data.DataRowView)Container.DataItem).Row["GoodValue"])%>
					</font>
				</td>
			</tr>
		</ItemTemplate>
		<FooterTemplate>
			</table>
		</FooterTemplate>
	</asp:Repeater>
</asp:Content>