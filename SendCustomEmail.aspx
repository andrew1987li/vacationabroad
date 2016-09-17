<%@ Page Language="C#" MasterPageFile="~/MasterPageNoCss.master" AutoEventWireup="true" CodeFile="SendCustomEmail.aspx.cs" Inherits="SendCustomEmail" Title="Send Email" %>
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
	<% if (BackLink.Visible) { %>
    <table bgcolor="#e4e4af" cellspacing="0" cellpadding="0" width="250" align="center" border="2">
        <tr>
            <td>
                <div align="center">
                    <strong>
                        <asp:HyperLink ID="BackLink" runat="server" NavigateUrl="Administration.aspx">
							Return to administration page
                        </asp:HyperLink>
                    </strong>
                </div>
            </td>
        </tr>
    </table>
	<br />
	<% } %>
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
    <br />
    <div align="left" style="padding-left:10px">
    <table border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td>
                <asp:RadioButton ID="SendProperty" runat="server" Width="312px" Height="24px" Text="Send e-mail to owner of property with number"
                    GroupName="0"></asp:RadioButton>
                <asp:TextBox ID="PropertyNumber" runat="server" Width="128px" />
                <asp:RequiredFieldValidator ID="PropertyNumberRequired" runat="server" Height="16px"
                    ErrorMessage="Please enter property number" ControlToValidate="PropertyNumber"
                    Display="Dynamic" />
                <asp:RegularExpressionValidator ID="PropertyNumberValid" runat="server" ErrorMessage="Invalid number entered"
                    ControlToValidate="PropertyNumber" Display="Dynamic" ValidationExpression="^[0-9 \.\-\(\)]{1,300}$" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:RadioButton ID="SendOwner" runat="server" Width="352px" Height="24px" Text="Send e-mail to all properties of owner with username"
                    GroupName="0"></asp:RadioButton>
                <asp:TextBox ID="OwnerUsername" runat="server" Width="128px" />
                <asp:RequiredFieldValidator ID="OwnerUsernameRequired" runat="server" Height="16px"
                    ErrorMessage="Please enter owner username" ControlToValidate="OwnerUsername"
                    Display="Dynamic" />
                <asp:RegularExpressionValidator ID="OwnerUsernameValid" runat="server" ErrorMessage="Invalid username entered"
                    ControlToValidate="OwnerUsername" Display="Dynamic" ValidationExpression="^[\s\S]{1,30}$" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:RadioButton ID="SendTrial" runat="server" Width="224px" Height="24px" Text="Send e-mail to all free trial listings"
                    GroupName="0"></asp:RadioButton>
            </td>
        </tr>
        <tr>
            <td>
                <asp:RadioButton ID="SendAnnual" runat="server" Width="232px" Height="24px" Text="Send e-mail to all annual fee listings"
                    GroupName="0"></asp:RadioButton>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="EmailBody" runat="server" Width="568px" Height="176px" TextMode="MultiLine" />
                <asp:RequiredFieldValidator ID="EmailRequired" runat="server" Height="16px" ErrorMessage="Please enter e-mail body"
                    ControlToValidate="EmailBody" Display="Dynamic" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="SendButton" runat="server" Width="128px" CausesValidation="False"
                    Text="Send" OnClick="SendButton_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="EmailsSent" runat="server" Height="24px" Text="X e-mails sent" Visible="False"></asp:Label>
            </td>
        </tr>
    </table>
    </div>
   

</asp:Content>
