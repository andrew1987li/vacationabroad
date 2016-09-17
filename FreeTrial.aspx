<%@ Page Language="C#" MasterPageFile="~/MasterPageNoCss.master" AutoEventWireup="true" CodeFile="FreeTrial.aspx.cs" Inherits="FreeTrial" Title="Free Trial Properties" %>
<asp:Content ID="Content" ContentPlaceHolderID="Content" Runat="Server">
    <table bgcolor="#e4e4af" cellspacing="0" cellpadding="0" width="100%" align="center" border="2">
        <tr>
            <td colspan="100" align="center">
                <strong>
                    <asp:HyperLink ID="Hyperlink7" runat="server" NavigateUrl="Administration.aspx">
						Main Administration page
                    </asp:HyperLink>
                </strong>
            </td>
        </tr>
        <tr>
            <td align="center">
                <strong>
                    <asp:HyperLink ID="Hyperlink2" runat="server" NavigateUrl="OwnersList.aspx">
						Owners
                    </asp:HyperLink>
                </strong>
            </td>
            <td align="center">
                <strong>
                    <asp:HyperLink ID="Hyperlink3" runat="server" NavigateUrl="Locations.aspx">
						Locations
                    </asp:HyperLink>
                </strong>
            </td>
            <td align="center">
                <strong>
                    <asp:HyperLink ID="Hyperlink4" runat="server" NavigateUrl="SendCustomEmail.aspx">
						Send Email
                    </asp:HyperLink>
                </strong>
            </td>
            <td align="center">
                <strong>
                    <asp:HyperLink ID="Hyperlink1" runat="server" NavigateUrl="OutstandingInvoices.aspx">
						Outstanding Invoices
                    </asp:HyperLink>
                </strong>
            </td>
            <td align="center">
                <strong>
                    <asp:HyperLink ID="Hyperlink5" runat="server" NavigateUrl="FreeTrial.aspx">
						Free Trial Properties
                    </asp:HyperLink>
                </strong>
            </td>
            <td align="center">
                <strong>
                    <asp:HyperLink ID="Hyperlink6" runat="server" NavigateUrl="PaymentsReceived.aspx">
						Payments Received
                    </asp:HyperLink>
                </strong>
            </td>
        </tr>
        <tr>
			<td colspan="100" align="center">
				<table width="100%" border="0">
					<tr>
						<td colspan="3" align="left">
							<a href='<%= CommonFunctions.PrepareURL ("CommissionPayable.aspx", "Administration") %>'>
								Commission Payable
							</a>
						</td>
						<td colspan="3" align="right">
							<a href='<%= CommonFunctions.PrepareURL ("InvoiceRegister.aspx?Year=2004", "Administration") %>'>
								Invoice Register 2004
							</a>
							&nbsp;&nbsp;
							<a href='<%= CommonFunctions.PrepareURL ("InvoiceRegister.aspx?Year=2005", "Administration") %>'>
								Invoice Register 2005
							</a>
							&nbsp;&nbsp;
							<a href='<%= CommonFunctions.PrepareURL ("InvoiceRegister.aspx?Year=2006", "Administration") %>'>
								Invoice Register 2006
							</a>
						</td>
					</tr>
				</table>
			</td>
        </tr>
    </table>
    <br />
    <table bgcolor="#e4e4af" cellspacing="0" cellpadding="0" width="300" align="center"
        border="2">
        <tr>
            <td>
                <div align="center">
                    <strong>
                        <asp:HyperLink ID="OwnerInformationLink" runat="server" NavigateUrl="OwnerInformation.aspx">
							Update my personal information
                        </asp:HyperLink>
                    </strong>
                </div>
            </td>
        </tr>
    </table>
    <asp:Repeater ID="Repeater1" runat="server" DataMember="Properties" DataSource="<%# PropertiesSet %>">
        <HeaderTemplate>
            <table bordercolor="#ffffff" border="1" cellspacing="0" cellpadding="0" width="100%">
                <tr>
                    <td colspan="100" align="center">
                        <b>
                            <br />
                            Free trial properties:
                            <br />
                            <br />
                        </b>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b><a href='FreeTrial.aspx?SortOrder=1&<%# GetQueryStringWithoutSortOrder () %>'>Sign
                            Up </a></b>
                    </td>
                    <td>
                        <b><a href='FreeTrial.aspx?SortOrder=2&<%# GetQueryStringWithoutSortOrder () %>'>Property
                            Number </a></b>
                    </td>
                    <td>
                        <b><a href='FreeTrial.aspx?SortOrder=3&<%# GetQueryStringWithoutSortOrder () %>'>Country
                        </a></b>
                    </td>
                    <td>
                        <b><a href='FreeTrial.aspx?SortOrder=4&<%# GetQueryStringWithoutSortOrder () %>'>State
                        </a></b>
                    </td>
                    <td>
                        <b><a href='FreeTrial.aspx?SortOrder=5&<%# GetQueryStringWithoutSortOrder () %>'>First
                            Name </a></b>
                    </td>
                    <td>
                        <b><a href='FreeTrial.aspx?SortOrder=6&<%# GetQueryStringWithoutSortOrder () %>'>Last
                            Name </a></b>
                    </td>
                    <td>
                        <b><a href='FreeTrial.aspx?SortOrder=7&<%# GetQueryStringWithoutSortOrder () %>'>Owner
                            Country </a></b>
                    </td>
                    <td>
                        <b><a href='FreeTrial.aspx?SortOrder=8&<%# GetQueryStringWithoutSortOrder () %>'>Primary
                            Telephone </a></b>
                    </td>
                    <td>
                        <b><a href='FreeTrial.aspx?SortOrder=9&<%# GetQueryStringWithoutSortOrder () %>'>Daytime
                            Telephone </a></b>
                    </td>
                    <td>
                        <b><a href='FreeTrial.aspx?SortOrder=10&<%# GetQueryStringWithoutSortOrder () %>'>Evening
                            Telephone </a></b>
                    </td>
                    <td>
                        <b><a href='FreeTrial.aspx?SortOrder=11&<%# GetQueryStringWithoutSortOrder () %>'>Mobile
                            Telephone </a></b>
                    </td>
                    <td>
                        <b><a href='FreeTrial.aspx?SortOrder=12&<%# GetQueryStringWithoutSortOrder () %>'>Owner
                            Email </a></b>
                    </td>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <%# DataBinder.Eval(Container.DataItem, "DateAdded", "{0}") %>
                </td>
                <td width="100%" bgcolor="#e4e4af">
                    <a href='<%# CommonFunctions.PrepareURL ("ViewProperty.aspx?UserID=" + DataBinder.Eval(Container.DataItem, "UserID", "{0:d}") + "&PropertyID=" + DataBinder.Eval(Container.DataItem, "ID", "{0:d}"), "Outstanding Invoices") %>'>
                        <%# DataBinder.Eval(Container.DataItem, "ID", "{0}") %>
                    </a>
                </td>
                <td>
                    <%# DataBinder.Eval(Container.DataItem, "Country", "{0}") %>
                </td>
                <td>
                    <%# DataBinder.Eval(Container.DataItem, "StateProvince", "{0}") %>
                </td>
                <td>
                    <%# DataBinder.Eval(Container.DataItem, "FirstName", "{0}") %>
                </td>
                <td>
                    <%# DataBinder.Eval(Container.DataItem, "LastName", "{0}") %>
                </td>
                <td>
                    <%# DataBinder.Eval(Container.DataItem, "OwnerCountry", "{0}") %>
                </td>
                <td>
                    <%# DataBinder.Eval(Container.DataItem, "PrimaryTelephone", "{0}") %>
                </td>
                <td>
                    <%# DataBinder.Eval(Container.DataItem, "DaytimeTelephone", "{0}") %>
                </td>
                <td>
                    <%# DataBinder.Eval(Container.DataItem, "EveningTelephone", "{0}") %>
                </td>
                <td>
                    <%# DataBinder.Eval(Container.DataItem, "MobileTelephone", "{0}") %>
                </td>
                <td>
                    <a href='mailto:<%# DataBinder.Eval(Container.DataItem, "Email", "{0}") %>'>
                        <%# DataBinder.Eval(Container.DataItem, "Email", "{0}") %>
                    </a>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>

    <noscript>
        <img src="http://www.watchwise.net/cgi-watchwise/monitor.cgi?<%= CommonFunctions.GetSiteAddress ().ToLower () %>:all"
            width="1" height="1">
    </noscript>

    <script language="javascript">
        document.write('<img src="http://www.watchwise.net/cgi-watchwise/monitorwise.cgi?URL=<%= CommonFunctions.GetSiteAddress ().ToLower () %>:all&LINK=',escape(document.referrer),'" height=1 width=1>')
    </script>
</asp:Content>
