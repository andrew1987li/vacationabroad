<%@ Page Language="C#" MasterPageFile="~/MasterPageNoCss.master" AutoEventWireup="true" CodeFile="AllProperties.aspx.cs" Inherits="AllProperties" Title="Vacation Cottages Chalets Condos Hotels Homes Apartments Villas Lodges Resorts" %>
<asp:Content ID="Content" ContentPlaceHolderID="Content" Runat="Server">
<%--	<% if (Request.Params["HomeExchanges"] != null) { %>
		<table align="center" borderColor=#e7dbf2 bgColor="#336699" width="100%">
			<tr align="center">
				<td align="center">
					<h4><font face="Book Antiqua" color="#ffffff">
						Home Exchanges
					</font></h4>
				</td>
			</tr>
		</table>
	<% } %>
--%>	<table width="100%" border="0">
		<tr>
			<td width="50%" vAlign=top>
				<asp:repeater id=Repeater1 runat="server" DataMember="Regions" DataSource="<%# LocationsSet1 %>">
					<HeaderTemplate>
					</HeaderTemplate>
					<ItemTemplate>
						<div align="center">
							<strong>
								<%# DataBinder.Eval(Container.DataItem, "Region", "{0}")%>
							</strong>
						</div>
						<asp:repeater id=Repeater2 runat="server" DataSource='<%# ((System.Data.DataRowView)Container.DataItem).CreateChildView("RegionsCountries")%>'>
							<HeaderTemplate>
							</HeaderTemplate>
							<ItemTemplate>
								<div align="center">
									<strong>
										<a href='<%# CommonFunctions.PrepareURL (((string)DataBinder.Eval(Container.DataItem, "Country", "{0}")).Replace (" ", "_").ToLower () + "/default.aspx") %>'>
<!--										<a href='<%# CommonFunctions.PrepareURL (((string)DataBinder.Eval(Container.DataItem, "Country", "{0}")).Replace (" ", "_").ToLower () + "/default.aspx" + ((Request.Params["HomeExchanges"] != null) ? "?HomeExchanges=1" : "")) %>'> -->
											<%# DataBinder.Eval(Container.DataItem, "Country", "{0}") %>
										</a>
									</strong>
								</div>
<%--								<% if (Request.Params["HomeExchanges"] == null) { %>
--%>									<asp:repeater id="Repeater3" runat="server" DataSource='<%# ((System.Data.DataRowView)Container.DataItem).CreateChildView("CountriesStateProvinces")%>'>
										<HeaderTemplate>
										</HeaderTemplate>
										<ItemTemplate>
											<a href='<%# CommonFunctions.PrepareURL (((string)DataBinder.Eval(((System.Data.DataRowView)Container.DataItem).Row.GetParentRow ("CountriesStateProvinces"), "Country", "{0}")).Replace (" ", "_").ToLower () + "/" + ((string)DataBinder.Eval(Container.DataItem, "StateProvince", "{0}")).Replace (" ", "_").ToLower () + "/default.aspx") %>'>
												<%# DataBinder.Eval(Container.DataItem, "StateProvince", "{0}") %>
											</a>
											<%# !CommonFunctions.IfLastRow ((System.Data.DataRowView)Container.DataItem) ? ":" : "" %>
										</ItemTemplate>
										<FooterTemplate>
										</FooterTemplate>
									</asp:repeater>
<%--								<% } %>
--%>							</ItemTemplate>
							<FooterTemplate>
							</FooterTemplate>
						</asp:repeater>
					</ItemTemplate>
					<FooterTemplate>
					</FooterTemplate>
				</asp:repeater>
			</td>
			<td width="50%" vAlign=top>
				<asp:repeater id="Repeater4" runat="server" DataMember="Regions" DataSource="<%# LocationsSet2 %>">
					<HeaderTemplate>
					</HeaderTemplate>
					<ItemTemplate>
						<div align="center">
							<strong>
								<%# DataBinder.Eval(Container.DataItem, "Region", "{0}")%>
							</strong>
						</div>
						<asp:repeater id="Repeater5" runat="server" DataSource='<%# ((System.Data.DataRowView)Container.DataItem).CreateChildView("RegionsCountries")%>'>
							<HeaderTemplate>
							</HeaderTemplate>
							<ItemTemplate>
								<div align="center">
									<strong>
										<a href='<%# CommonFunctions.PrepareURL (((string)DataBinder.Eval(Container.DataItem, "Country", "{0}")).Replace (" ", "_").ToLower () + "/default.aspx") %>'>
<!--										<a href='<%# CommonFunctions.PrepareURL (((string)DataBinder.Eval(Container.DataItem, "Country", "{0}")).Replace (" ", "_").ToLower () + "/default.aspx" + ((Request.Params["HomeExchanges"] != null) ? "?HomeExchanges=1" : "")) %>'> -->
											<%# DataBinder.Eval(Container.DataItem, "Country", "{0}") %>
										</a>
									</strong>
								</div>
								<!--<% if (Request.Params["HomeExchanges"] == null) { %> -->
									<asp:repeater id="Repeater6" runat="server" DataSource='<%# ((System.Data.DataRowView)Container.DataItem).CreateChildView("CountriesStateProvinces")%>'>
										<HeaderTemplate>
										</HeaderTemplate>
										<ItemTemplate>
											<a href='<%# CommonFunctions.PrepareURL (((string)DataBinder.Eval(((System.Data.DataRowView)Container.DataItem).Row.GetParentRow ("CountriesStateProvinces"), "Country", "{0}")).Replace (" ", "_").ToLower () + "/" + ((string)DataBinder.Eval(Container.DataItem, "StateProvince", "{0}")).Replace (" ", "_").ToLower () + "/default.aspx") %>'>
												<%# DataBinder.Eval(Container.DataItem, "StateProvince", "{0}") %>
											</a>
											<%# !CommonFunctions.IfLastRow ((System.Data.DataRowView)Container.DataItem) ? ":" : "" %>
										</ItemTemplate>
										<FooterTemplate>
										</FooterTemplate>
									</asp:repeater>
								<!--<% } %>-->
							</ItemTemplate>
							<FooterTemplate>
							</FooterTemplate>
						</asp:repeater>
					</ItemTemplate>
					<FooterTemplate>
					</FooterTemplate>
				</asp:repeater>
			</td>
		</tr>
	</table>
	
    <noscript><img src="http://www.watchwise.net/cgi-watchwise/monitor.cgi?<%= CommonFunctions.GetSiteAddress ().ToLower () %>:all"
            width="1" height="1">
    </noscript>
    
    <script language="javascript">
        document.write('<img src="http://www.watchwise.net/cgi-watchwise/monitorwise.cgi?URL=<%= CommonFunctions.GetSiteAddress ().ToLower () %>:all&LINK=',escape(document.referrer),'" height=1 width=1>')
    </script>
</asp:Content>
