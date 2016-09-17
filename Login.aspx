<%--<%@ Page Language="C#" MasterPageFile="~/MasterPage10.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="LoginPage" Title="Vacations-Abroad.Com Login" %>--%>
<%@ Page Language="C#" MasterPageFile="~/MasterPageNoCss.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="LoginPage" Title="Vacations-Abroad.Com Login" %>
<asp:Content ID="Content" ContentPlaceHolderID="Content" Runat="Server">
    <table bgcolor="#e4e4af" cellspacing="0" cellpadding="0" width="250" align="center" border="2">
        <tr>
            <td>
                <div align="center">
                    <strong>
                        <asp:HyperLink ID="NewLink" runat="server" NavigateUrl="AccountInformation.aspx">
							New to the site? Register here.
                        </asp:HyperLink>
                    </strong>
                </div>
            </td>
        </tr>
    </table>
	<br />
    <asp:Label ID="UsernameWrongWarning" runat="server" Width="100%" Visible="False"
        ForeColor="Red">Incorrect login name or password. Please check whether Caps Lock is pressed and retry entering password, if it fails again contact support. If you have lost your password see the section below.</asp:Label>
    <table align="center">
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server">Login name</asp:Label>
            </td>
            <td>
                <asp:TextBox ID="Username" TabIndex="2" runat="server" Width="152px" />
                <asp:RequiredFieldValidator ID="EnterUsername" runat="server" ErrorMessage="RequiredFieldValidator"
                    ControlToValidate="Username" Display="Dynamic">Please enter login name</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="CheckUsername" runat="server" ValidationExpression="^[\s\S]{1,30}$"
                    ErrorMessage="Invalid login name entered" ControlToValidate="Username" Display="Dynamic" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label4" runat="server">Password</asp:Label>
            </td>
            <td>
                <asp:TextBox ID="Password" TabIndex="3" runat="server" Width="152px" 
                    TextMode="Password" />
                <asp:RequiredFieldValidator ID="EnterPassword" runat="server" ErrorMessage="RequiredFieldValidator"
                    ControlToValidate="Password" Display="Dynamic">Please enter password</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="CheckPassword" runat="server" ValidationExpression="^[\s\S]{1,30}$"
                    ErrorMessage="Invalid password entered" ControlToValidate="Password" Display="Dynamic" />
            </td>
        </tr>
    </table>
    <p align="center">
        When you login, be sure and click on the "LOGIN" Button. If you need help, contact us at 1-770-687-6889


    </p>
    <p align="center">
        <asp:Button ID="LoginButton" runat="server" Height="24px" Width="120px"
            Text="Login" CausesValidation="False" OnClick="LoginButton_Click" 
            TabIndex="4" UseSubmitBehavior="true" />
    </p>
     <p align="center">
        Use Another Service to Log in


    </p>
    <p align="center">
        <asp:Button ID="fb_loginButton" runat="server" Height="24px" Width="120px"
            Text="Facebook Login" CausesValidation="False" OnClick="fbLogin"
            TabIndex="4" UseSubmitBehavior="true" />
         <asp:Button ID="Twitter_btn" runat="server" Height="24px" Width="120px"
            Text="Twitter Login" CausesValidation="False" OnClick="twitter_login"
            TabIndex="4" UseSubmitBehavior="true" />

    </p>
    <br />
    <font color="#FF3300">
        <asp:Label ID="Label5" runat="server" Width="100%">Forgot your login name or password? Enter your email address here, password will be reset and sent to you. We're not storing passwords so we can't send you your existing password.</asp:Label>
    </font>
    <p align="center">
        <table>
            <tr>
                <td>
                    <font color="#FF3300">
                        <asp:Label ID="Label6" runat="server">Email address</asp:Label>
                    </font>
                </td>
                <td>
                    <asp:TextBox ID="EmailAddress" TabIndex="5" runat="server" Width="304px" 
                        MaxLength="80" />
                    <asp:RequiredFieldValidator ID="EnterEmail" runat="server" ErrorMessage="RequiredFieldValidator"
                        ControlToValidate="EmailAddress" Display="Dynamic">Please enter email</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="CheckEmail" runat="server" ValidationExpression="^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"
                        ErrorMessage="Invalid email address entered" ControlToValidate="EmailAddress"
                        Display="Dynamic" />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ValidationExpression="^[\s\S]{1,80}$"
                        ErrorMessage="Too long email address entered" ControlToValidate="EmailAddress"
                        Display="Dynamic" />
                </td>
            </tr>
        </table>
    </p>
    <div align="center">
        <asp:Label ID="InvalidEmail" runat="server" Visible="False" ForeColor="Red">Incorrect email address</asp:Label>
    </div>
    <div align="center">
        <asp:Label ID="Emailed" runat="server" Visible="False">Your password was reset, new password generated and sent to you</asp:Label>
    </div>
    <p align="center">
        <asp:Button ID="SendPassword" TabIndex="6" runat="server" Height="24px" Width="120px"
            Text="Send password" CausesValidation="False" 
            OnClick="SendPassword_Click" />
    </p>

</asp:Content>
