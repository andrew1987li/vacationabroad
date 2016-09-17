<%@ Page Language="C#" MasterPageFile="~/MasterPageNoCss.master" AutoEventWireup="true" CodeFile="FBAcoountInformation.aspx.cs" Inherits="FBAcoountInformation" Title="FBAccount Information" %>


<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="Server">
    <% if (BackLink.Visible)
       { %>
    <table bgcolor="#e4e4af" cellspacing="0" cellpadding="0" width="250" align="center" border="2">
        <tr>
            <td>
                <div align="center">
                    <strong>
                        <asp:HyperLink ID="BackLink" runat="server" NavigateUrl="MyAccount.aspx">
							Return to My Account page
                        </asp:HyperLink>
                    </strong>
                </div>
            </td>
        </tr>
    </table>
    
    <% } %>
    <asp:Label ID="UsernameWrongWarning" runat="server" ForeColor="Red" Visible="False" Width="100%">
		Incorrect login name or password. Please check whether Caps Lock is pressed and retry entering password,
		if it fails again contact support. If you have lost your password see the section below.
    </asp:Label>
    <table cellspacing="1" cellpadding="1" bgcolor="#cfdfef" border="0" width="100%">  <br />  <br />  <br />  <br />
        <tr valign="top" align="left">
            <td style="height: 26px; width: 140px;">
                Login name:
            </td>
            <td style="height: 26px; width: 13px;">
                <font color="red">* </font>
            </td>
            <td bgcolor="#ffffff" style="height: 26px">
                <asp:TextBox ID="Username" Width="216" runat="server" ReadOnly="True" Text='<%# MainDataSet.Tables["Users"].Rows[0]["Username"] %>' />
                <asp:RequiredFieldValidator ID="UsernameRequired" runat="server" ControlToValidate="Username" Display="Dynamic" ErrorMessage="Please enter login name" Enabled="False" />
                <asp:RegularExpressionValidator ID="UsernameInvalid" runat="server" ControlToValidate="Username" Display="Dynamic" ErrorMessage="Too long login name entered" ValidationExpression="^.{1,30}$"
                    Enabled="False" />
                <asp:Label ID="DuplicateUsername" runat="server" ForeColor="Red" Visible="False">
					Duplicate login name/email, please select another one
                </asp:Label>
            </td>
        </tr>
        <tr valign="top" align="left">
            <td style="width: 140px">
                Email Address:
            </td>
            <td style="width: 13px">
                <font color="red">*</font>
            </td>
            <td bgcolor="#ffffff">
                <asp:TextBox ID="EmailAddress" Width="304" runat="server"
                    Text='<%# MainDataSet.Tables["Users"].Rows[0]["Email"] %>' />
                <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="EmailAddress" Display="Dynamic" ErrorMessage="Please enter e-mail address"/>
                <asp:RegularExpressionValidator ID="EmailInvalid" runat="server" ControlToValidate="EmailAddress" Display="Dynamic" ErrorMessage="Invalid e-mail address entered" ValidationExpression="^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$" />
                <asp:RegularExpressionValidator ID="EmailTooLong" runat="server" ControlToValidate="EmailAddress" Display="Dynamic" ErrorMessage="Too long email address entered" ValidationExpression="^.{1,80}$" />
                <br />
                This is the default email for all functions associated with this account.
            </td>
        </tr>
       
        <tr valign="top" align="left">
          
            <td bgcolor="#ffffff">
                <asp:TextBox ID="NewPassword" Width="216" runat="server" Visible="false" />
               
            </td>
        </tr>
        <tr valign="top" align="left">
         
            <td bgcolor="#ffffff">
                <asp:TextBox ID="NewPassword2" Width="216" runat="server" Visible="false" />
                <asp:CompareValidator ID="PasswordsSame" runat="server" ControlToCompare="NewPassword"
                    ControlToValidate="NewPassword2" Display="Dynamic" ErrorMessage="Passwords must be the same" />
            </td>
        </tr>
        <tr valign="top" align="left">
            <td style="width: 140px">
                User ID:
            </td>
            <td style="width: 13px">
            </td>
            <td bgcolor="#ffffff">
                <asp:TextBox ID="UserID" Width="216" runat="server"
                    Text='<%# MainDataSet.Tables["Users"].Rows[0]["UserID"] %>' />
                Create an ID different from you Login Name.
				<font color="red">
                    <asp:RegularExpressionValidator ID="UserIDValid" runat="server" ControlToValidate="UserID" ErrorMessage="Max 7 Characters. No spaces and must contain alphanumeric only." ValidationExpression="^[a-zA-Z0-9]{1,7}$" Display="Dynamic" />
                </font>
            </td>
        </tr>
        <tr valign="top" align="left">
            <td style="width: 140px">
                &nbsp;
            </td>
            <td style="width: 13px">
            </td>
            <td bgcolor="#ffffff">
                <asp:Button ID="SubmitButton" runat="server" Text="Submit" OnClick="SubmitButton_Click" Width="96px" /> &nbsp; 
                <asp:Button ID="CancelButton" runat="server" Text="Cancel" CausesValidation="False" OnClick="CancelButton_Click" Width="96px" />
            </td>
        </tr>
    </table>
    <asp:hiddenfield id="TwiID"
              value="" 
              runat="server"/>
</asp:Content>
