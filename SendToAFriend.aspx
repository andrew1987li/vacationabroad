<%@ Page Language="C#" MasterPageFile="~/MasterPageNoCss.master" AutoEventWireup="true" CodeFile="SendToAFriend.aspx.cs" Inherits="SendToAFriend" Title="Send to a Friend" %>
<asp:Content ID="Content" ContentPlaceHolderID="Content" Runat="Server">
	<% if (BackLink.Visible) { %>
    <table bgcolor="#e4e4af" cellspacing="0" cellpadding="0" width="350" align="center" border="2">
        <tr>
            <td>
                <div align="center">
                    <strong>
                        <asp:HyperLink ID="BackLink" runat="server" NavigateUrl="default.aspx">
							Return to property page
                        </asp:HyperLink>
                    </strong>
                </div>
            </td>
        </tr>
    </table>
	<br />
	<% } %>
    From this form you can send a link to this property to a friend of yours.
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td>
                <asp:Label ID="Label2" runat="server" Height="16px">Your E-mail Address:</asp:Label>
            </td>
            <td>
                <asp:TextBox ID="SenderEmail" runat="server" Width="300px" />
                <asp:RequiredFieldValidator ID="Requiredfieldvalidator2" runat="server" ControlToValidate="SenderEmail"
                    ErrorMessage="Please enter e-mail address" Display="Dynamic" />
                <asp:RegularExpressionValidator ID="Regularexpressionvalidator1" runat="server" ControlToValidate="SenderEmail"
                    ErrorMessage="Invalid email address entered" ValidationExpression="^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"
                    Display="Dynamic" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Height="16px">Your Name:</asp:Label>
            </td>
            <td>
                <asp:TextBox ID="SenderName" runat="server" Width="300px" />
                <asp:RequiredFieldValidator ID="Requiredfieldvalidator1" runat="server" ControlToValidate="SenderName"
                    ErrorMessage="Please enter your name" Display="Dynamic" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label4" runat="server" Height="16px">Friend's E-mail Address:</asp:Label>
            </td>
            <td>
                <asp:TextBox ID="FriendEmail" runat="server" Width="300px" />
                <asp:RequiredFieldValidator ID="Requiredfieldvalidator5" runat="server" ControlToValidate="FriendEmail"
                    ErrorMessage="Please enter e-mail address" Display="Dynamic" />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="FriendEmail"
                    ErrorMessage="Invalid email address entered" ValidationExpression="^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"
                    Display="Dynamic" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="SubmitButton" runat="server" Height="24px" Width="112px" Text="Submit"
                    CausesValidation="False" OnClick="SubmitButton_Click" />
            </td>
        </tr>
    </table>
    
    <noscript>
        <img src="http://www.watchwise.net/cgi-watchwise/monitor.cgi?<%= CommonFunctions.GetSiteAddress ().ToLower () %>:all"
            width="1" height="1">
    </noscript>
    
    <script language="javascript">
        document.write('<img src="http://www.watchwise.net/cgi-watchwise/monitorwise.cgi?URL=<%= CommonFunctions.GetSiteAddress ().ToLower () %>:all&LINK=',escape(document.referrer),'" height=1 width=1>')
    </script>
</asp:Content>
