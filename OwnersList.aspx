<%@ Page Language="C#" MasterPageFile="~/MasterPageNoCss.master" AutoEventWireup="true" CodeFile="OwnersList.aspx.cs" Inherits="OwnersList" Title="Owners List" %>
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
        border="0">
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
    <asp:Repeater ID="Repeater1" runat="server" DataSource="<%# UserSet %>" DataMember="Users">
        <HeaderTemplate>
            <table bordercolor="#ffffff" border="1" cellspacing="0" cellpadding="0" width="100%">
                <tr>
                    <td bordercolor="#ffffff" colspan="100" align="center">
                        <b>
                            <br />
                            Owners registered in the system:
                            <br />
                            <br />
                        </b>
                    </td>
                </tr>
                <tr bordercolor="#ffffff">
                    <td>
                        <b>Login Name</b></td>
                    <td>
                        <b>First Name</b></td>
                    <td>
                        <b>Last Name</b></td>
                    <td>
                        <b>Primary Telephone</b></td>
                    <td>
                        <b>Evening Telephone</b></td>
                    <td>
                        <b>Daytime Telephone</b></td>
                    <td>
                        <b>Mobile Telephone</b></td>
                </tr>
                <tr bordercolor="#ffffff">
                    <td>
                        <b>Address</b></td>
                    <td>
                        <b>City</b></td>
                    <td>
                        <b>State/Province</b></td>
                    <td>
                        <b>Zip</b></td>
                    <td>
                        <b>Country</b></td>
                    <td>
                        <b>Email Address</b></td>
                    <td>
                        <b>Website</b></td>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td colspan="7" height="7">
                </td>
            </tr>
            <tr>
                <td>
                    <%# DataBinder.Eval(Container.DataItem, "UserName", "{0}") %>
                </td>
                <td>
                    <%# DataBinder.Eval(Container.DataItem, "FirstName", "{0}") %>
                </td>
                <td>
                    <%# DataBinder.Eval(Container.DataItem, "LastName", "{0}") %>
                </td>
                <td>
                    <%# DataBinder.Eval(Container.DataItem, "PrimaryTelephone", "{0}") %>
                </td>
                <td>
                    <%# DataBinder.Eval(Container.DataItem, "EveningTelephone", "{0}") %>
                </td>
                <td>
                    <%# DataBinder.Eval(Container.DataItem, "DaytimeTelephone", "{0}") %>
                </td>
                <td>
                    <%# DataBinder.Eval(Container.DataItem, "MobileTelephone", "{0}") %>
                </td>
                <td rowspan="2" bordercolor="#ffffff">
                    <button onclick='window.location.href=&quot;<%# CommonFunctions.PrepareURL ("MyAccount.aspx?UserID=" + DataBinder.Eval(Container.DataItem, "ID", "{0:d}"), "Owners List") %>&quot;'>
                        Edit
                    </button>
                </td>
                <td rowspan="2" bordercolor="#ffffff">
                    <button onclick='AskDelete (&quot;<%# CommonFunctions.PrepareURL ("DeleteOwner.aspx?UserID=" + DataBinder.Eval(Container.DataItem, "ID", "{0:d}"), "Owners List") %>&quot;);'>
                        Delete
                    </button>
                </td>
                <td rowspan="2" bordercolor="#ffffff">
                    <button onclick='window.location.href=&quot;<%# CommonFunctions.PrepareURL ("SendCustomEmail.aspx?Username=" + DataBinder.Eval(Container.DataItem, "Username", "{0:d}"), "Owners List") %>&quot;'>
                        Send Email
                    </button>
                </td>
            </tr>
            <tr>
                <td>
                    <%# DataBinder.Eval(Container.DataItem, "Address", "{0}") %>
                </td>
                <td>
                    <%# DataBinder.Eval(Container.DataItem, "City", "{0}") %>
                </td>
                <td>
                    <%# DataBinder.Eval(Container.DataItem, "State", "{0}") %>
                </td>
                <td>
                    <%# DataBinder.Eval(Container.DataItem, "Zip", "{0}") %>
                </td>
                <td>
                    <%# DataBinder.Eval(Container.DataItem, "Country", "{0}") %>
                </td>
                <td>
                    <%# DataBinder.Eval(Container.DataItem, "Email", "{0}") %>
                </td>
                <td>
                    <%# DataBinder.Eval(Container.DataItem, "Website", "{0}") %>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>

    <script language="javascript" type="text/javascript">
    <!--

    function AskDelete (link)
    {
	if (window.confirm ("Are you sure you want to do this?"))
		window.location.href = link;
    }

    // -->
    </script>

    <noscript>
        <img src="http://www.watchwise.net/cgi-watchwise/monitor.cgi?<%= CommonFunctions.GetSiteAddress ().ToLower () %>:owners" width="1" height="1">
    </noscript>

    <script language="javascript">
	    document.write('<img src="http://www.watchwise.net/cgi-watchwise/monitorwise.cgi?URL=<%= CommonFunctions.GetSiteAddress ().ToLower () %>:owners&LINK=',escape(document.referrer),'" height=1 width=1>')
    </script>
</asp:Content>
