<%@ Page Language="C#" MasterPageFile="~/MasterPageNoCss.master" AutoEventWireup="true" CodeFile="UserProperties.aspx.cs" Inherits="UserProperties" Title="User Properties" %>
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
			Current Property Listings for
			<a href='<%# CommonFunctions.PrepareURL ("ViewUser.aspx?UserID=" + userid.ToString (), "User Properties") %>'>
				<%= username %>
			</a>
		</strong>
	</div>
	<asp:Repeater ID="Repeater1" runat="server" DataMember="Properties" DataSource="<%# MainDataSet %>">
		<HeaderTemplate>
			<table bordercolor="#ffffff" cellspacing="1" cellpadding="0" width="750" align="center" bgcolor="#cfdfef"
				border="0">
				<tr bgcolor="#cc9933">
					<td align="center">
						<font size="2" color="#ffffff"><strong>Title</strong></font>
					</td>
					<td width="83" bgcolor="#cc9933" align="center">
						<font size="2" color="#ffffff"><strong>Country</strong></font>
					</td>
					<td width="74" align="center">
						<font size="2" color="#ffffff"><strong>City</strong></font>
					</td>
					<td width="45" align="center">
						<font size="2" color="#ffffff"><strong>Type</strong></font>
					</td>
					<td width="37" align="center">
						<font size="2" color="#ffffff"><strong>Bdrms</strong></font>
					</td>
					<td width="48" align="center">
						<font size="2" color="#ffffff"><strong>Ba</strong></font>
					</td>
					<td width="47" align="center">
						<font size="2" color="#ffffff"><strong>Pool</strong></font>
					</td>
					<td width="226" align="center">
						<font size="2" color="#ffffff"><strong>Other</strong></font>
					</td>
				</tr>
		</HeaderTemplate>
		<ItemTemplate>
			<tr>
				<td bgcolor="#ffffff">
					<font size="2">
						<a href='<%# CommonFunctions.PrepareURL ("ViewProperty.aspx?PropertyID=" + DataBinder.Eval(Container.DataItem, "ID", "{0:d}"), "*User* Listings") %>'>
							<%# DataBinder.Eval(Container.DataItem, "Name", "{0}") %>
						</a>
					</font>
				</td>
				<td bgcolor="#ffffff" align="center">
					<font size="2"><%# DataBinder.Eval(Container.DataItem, "Country", "{0}") %></font>
				</td>
				<td bgcolor="#ffffff" align="center">
					<font size="2"><%# DataBinder.Eval(Container.DataItem, "City", "{0}") %></font>
				</td>
				<td bgcolor="#ffffff" align="center">
					<font size="2"><%# DataBinder.Eval(Container.DataItem, "Type", "{0}") %></font>
				</td>
				<td bgcolor="#ffffff" align="center">
					<font size="2"><%# DataBinder.Eval(Container.DataItem, "NumBedrooms", "{0}") %></font>
				</td>
				<td bgcolor="#ffffff" align="center">
					<font size="2"><%# DataBinder.Eval(Container.DataItem, "NumBaths", "{0}") %></font>
				</td>
				<td bgcolor="#ffffff" align="center">
					<font size="2"><%# DataBinder.Eval(Container.DataItem, "Pool", "{0}") %></font>
				</td>
				<td bgcolor="#ffffff" align="center">
					<font size="2">
						<asp:repeater id="Repeater2" runat="server" DataSource='<%# ((System.Data.DataRowView)Container.DataItem).CreateChildView("PropertiesAmenities") %>'><HeaderTemplate></HeaderTemplate><ItemTemplate><%# DataBinder.Eval(Container.DataItem, "Amenity", "{0}") %><%# !CommonFunctions.IfLastRow ((System.Data.DataRowView)Container.DataItem) ? "," : "." %></ItemTemplate><FooterTemplate></FooterTemplate></asp:Repeater>
					</font>
				</td>
			</tr>
		</ItemTemplate>
		<FooterTemplate>
			</table>
		</FooterTemplate>
	</asp:Repeater>
</asp:Content>