<%@ Page Language="C#" MasterPageFile="~/masterpage/NormalMaster.master" AutoEventWireup="true"
    CodeFile="OwnerInformation.aspx.cs" Inherits="OwnerInformation" Title="Owner Personal Information" %>
<asp:Content ID="links" ContentPlaceHolderID="links" runat="server">
    <link href="/Assets/css/ownerinfo.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="bodycontent" runat="Server">
    <div class="internalpagewidth">
<div class="left">
    <% if (BackLink.Visible)
       { %>
    <table bgcolor="#F5EDE3" cellspacing="0" cellpadding="0" width="250" align="center"
        border="2">
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
    <br />
    <% } %>
    <table>
        <tr>
            <td colspan="100" style="text-align:center;">
                <strong>Administrative Contact Details</strong>
            </td>
        </tr>
        <tr>
            <td width="30%">
                Administrative Email Address:
            </td>
            <td width="8">
                <font color="red">*</font>
            </td>
            <td>
                <asp:TextBox ID="EmailAddress" TabIndex="7" runat="server" Width="304px" MaxLength="80" />
                <asp:RequiredFieldValidator ID="Requiredfieldvalidator5" runat="server" ControlToValidate="EmailAddress"
                    ErrorMessage="Please enter e-mail address" Display="Dynamic" />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="EmailAddress"
                    ErrorMessage="Invalid email address entered" ValidationExpression="^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"
                    Display="Dynamic" />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="EmailAddress"
                    ErrorMessage="Too long email address entered" ValidationExpression="^.{1,80}$"
                    Display="Dynamic" />
            </td>
        </tr>
        <tr>
            <td width="30%">
                First Name:
            </td>
            <td width="8">
                <font color="red">*</font>
            </td>
            <td>
                <asp:TextBox ID="FirstName" TabIndex="10" runat="server" Width="216px" MaxLength="30" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="FirstName"
                    ErrorMessage="Please enter first name" Display="Dynamic" />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator19" runat="server"
                    ControlToValidate="FirstName" ErrorMessage="Too long first name entered" ValidationExpression="^.{1,30}$"
                    Display="Dynamic" />&nbsp;
            </td>
        </tr>
        <tr>
            <td width="30%">
                Last Name:
            </td>
            <td width="8">
                <font color="red">*</font>
            </td>
            <td>
                <asp:TextBox ID="LastName" TabIndex="11" runat="server" Width="216px" MaxLength="30" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="LastName"
                    ErrorMessage="Please enter last name" Display="Dynamic" />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator20" runat="server"
                    ControlToValidate="LastName" ErrorMessage="Too long last name entered" ValidationExpression="^.{1,30}$"
                    Display="Dynamic" />&nbsp;
            </td>
        </tr>
        <tr>
            <td width="30%">
                Company Name:
            </td>
            <td width="8">
            </td>
            <td>
                <asp:TextBox ID="CompanyName" TabIndex="11" runat="server" Width="216px" MaxLength="30" />
                <asp:RegularExpressionValidator ID="Regularexpressionvalidator17" runat="server"
                    ControlToValidate="CompanyName" ErrorMessage="Too long company name entered"
                    ValidationExpression="^.{1,30}$" Display="Dynamic" />
            </td>
        </tr>
        <tr>
            <td width="30%">
                Administrative Address:
            </td>
            <td width="8">
            <font color="red">*</font>
            </td>
            <td>
                <asp:TextBox ID="Address" TabIndex="12" runat="server" Width="304px" MaxLength="300" />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="Address"
                    ErrorMessage="Too long address entered" ValidationExpression="^.{1,300}$" Display="Dynamic" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="Address" Text="Address Required"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="30%">
                City:
            </td>
            <td width="8">
            <font color="red">*</font>
            </td>
            <td>
                <asp:TextBox ID="City" TabIndex="14" runat="server" Width="224px" MaxLength="300" />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="City"
                    ErrorMessage="Too long city entered" ValidationExpression="^.{1,300}$" Display="Dynamic" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="City" Text="City Required"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="30%">
                State / Province:
            </td>
            <td width="8">
            <font color="red">*</font>
            </td>
            <td>
                <asp:TextBox ID="State" TabIndex="15" runat="server" Width="224px" MaxLength="300" />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server"
                    ControlToValidate="State" ErrorMessage="Too long state entered" ValidationExpression="^.{1,300}$"
                    Display="Dynamic" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                    ErrorMessage="State Required" ControlToValidate="State"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="30%">
                Zip / Postal Code:
            </td>
            <td width="8">
            <font color="red">*</font>
            </td>
            <td>
                <asp:TextBox ID="Zip" TabIndex="16" runat="server" Width="112px" MaxLength="300" />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server"
                    ControlToValidate="Zip" ErrorMessage="Too long zip code entered" ValidationExpression="^.{1,300}$"
                    Display="Dynamic" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="Zip" Text="Zip Required"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="30%">
                Country:
            </td>
            <td width="8">
                <font color="red">*</font>
            <td>
                <asp:DropDownList ID="ddlCountries" runat="server" AppendDataBoundItems="True" 
                    DataTextField="country" DataValueField="country">
                    <asp:ListItem Selected="True" Value="0">Select your country</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                    ControlToValidate="ddlCountries" ErrorMessage="Please enter country" 
                    InitialValue="0">Please enter country</asp:RequiredFieldValidator>
                <%--<asp:TextBox ID="Country" TabIndex="17" runat="server" Width="224px" MaxLength="300" />
				<asp:RequiredFieldValidator ID="CountryRequired" runat="server" ControlToValidate="Country"
					ErrorMessage="Country must be entered by agents" Display="Dynamic" />
				<asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server" ControlToValidate="Country"
					ErrorMessage="Too long country entered" ValidationExpression="^.{1,300}$" Display="Dynamic" />--%>
            </td>
        </tr>
        <tr>
            <td width="30%">
                Primary Telephone:
            </td>
            <td width="8">
            <font color="red">*</font>
            </td>
            <td>
                <asp:TextBox ID="PrimaryTelephone" TabIndex="14" runat="server" Width="224px" MaxLength="300" />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="PrimaryTelephone"
                    ErrorMessage="Too long primary telephone entered" ValidationExpression="^.{1,300}$"
                    Display="Dynamic" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="PrimaryTelephone"
                    Display="Dynamic" ErrorMessage="Primary Telephone # Needed" SetFocusOnError="True">Primary Telephone # Needed</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="30%">
                Evening Telephone:
            </td>
            <td width="8">
            </td>
            <td>
                <asp:TextBox ID="EveningTelephone" TabIndex="14" runat="server" Width="224px" MaxLength="300" />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator15" runat="server"
                    ControlToValidate="EveningTelephone" ErrorMessage="Too long evening telephone entered"
                    ValidationExpression="^.{1,300}$" Display="Dynamic" />
            </td>
        </tr>
        <tr>
            <td width="30%">
                Daytime Telephone:
            </td>
            <td width="8">
            </td>
            <td>
                <asp:TextBox ID="DaytimeTelephone" TabIndex="14" runat="server" Width="224px" MaxLength="300" />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator16" runat="server"
                    ControlToValidate="DaytimeTelephone" ErrorMessage="Too long daytime telephone entered"
                    ValidationExpression="^.{1,300}$" Display="Dynamic" />
            </td>
        </tr>
        <tr>
            <td width="30%">
                Mobile Telephone:
            </td>
            <td width="8">
            </td>
            <td>
                <asp:TextBox ID="MobileTelephone" TabIndex="14" runat="server" Width="224px" MaxLength="300" />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server"
                    ControlToValidate="MobileTelephone" ErrorMessage="Too long mobile telephone entered"
                    ValidationExpression="^.{1,300}$" Display="Dynamic" />
            </td>
        </tr>
        <tr>
            <td width="30%">
                Web site:
            </td>
            <td width="8">
            </td>
            <td>
                <asp:TextBox ID="Website" TabIndex="14" runat="server" Width="304px" MaxLength="300" />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server"
                    ControlToValidate="Website" ErrorMessage="Too long web site entered" ValidationExpression="^.{1,300}$"
                    Display="Dynamic" />
            </td>
        </tr>
        <tr>
            <td width="30%">
                <strong>Specify Name</strong> of Tourist Board or Chamber of Commerce in which you
                are a member:
            </td>
            <td width="8">
            </td>
            <td>
                <asp:TextBox ID="Registered" TabIndex="14" runat="server" Width="224px" MaxLength="300" />
                <asp:RegularExpressionValidator ID="Regularexpressionvalidator18" runat="server"
                    ControlToValidate="Registered" ErrorMessage="Too long registered with name entered"
                    ValidationExpression="^.{1,300}$" Display="Dynamic" />
            </td>
        </tr>
        <tr>
            <td colspan="100">
                <asp:CheckBox ID="PayTravelAgents" runat="server" Text="<strong>Yes</strong> - We Pay Travel Agents Commission" />
            </td>
        </tr>
        <tr>
            <td width="30%">
                <strong>Referral</strong> - Did someone refer you to our site? First select the
                appropriate country of the agent, then a list agents in that region will be displayed.
            </td>
            <td width="8">
            </td>
            <td>
                <asp:DropDownList ID="AgentCountries" runat="server" Width="98px" AutoPostBack="True"
                    DataMember="Countries" DataValueField="Country" DataTextField="Country" DataTextFormatString="{0}"
                    OnSelectedIndexChanged="AgentCountries_SelectedIndexChanged">
                </asp:DropDownList>
                <br />
                <asp:DropDownList ID="Agents" runat="server" Width="276px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr bgcolor="#999966" height="20">
            <td width="30%">
            </td>
            <td width="8">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td colspan="100">
                <strong>Reservation Contact Details</strong><br />
                The following data will only be used when you list a property and want a different
                email for Reservation Inquiries
            </td>
        </tr>
        <tr>
            <td colspan="100" style="height: 22px">
                Administrative and Reservation Contact Details are the same?
                <asp:RadioButton ID="ReservationSame" runat="server" Text="Yes" OnCheckedChanged="ReservationSame_CheckedChanged"
                    GroupName="1" AutoPostBack="True" />
                <asp:RadioButton ID="ReservationNotSame" runat="server" Text="No" OnCheckedChanged="ReservationNotSame_CheckedChanged"
                    GroupName="1" AutoPostBack="True" />
            </td>
        </tr>
        <% if (!ReservationSame.Checked)
           { %>
        <tr>
            <td width="30%">
                Reservation Email Address:
            </td>
            <td width="8">
            </td>
            <td>
                <asp:TextBox ID="ReservationEmail" TabIndex="7" runat="server" Width="304px" MaxLength="80" />
                <asp:RequiredFieldValidator ID="ReservationEmailRequired" runat="server" ControlToValidate="ReservationEmail"
                    ErrorMessage="Please enter e-mail address" Display="Dynamic" />
                <asp:RegularExpressionValidator ID="ReservationEmailInvalid" runat="server" ControlToValidate="ReservationEmail"
                    ErrorMessage="Invalid email address entered" ValidationExpression="^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"
                    Display="Dynamic" />
                <asp:RegularExpressionValidator ID="ReservationEmailTooLong" runat="server" ControlToValidate="ReservationEmail"
                    ErrorMessage="Too long email address entered" ValidationExpression="^.{1,80}$"
                    Display="Dynamic" />
            </td>
        </tr>
        <tr>
            <td width="30%">
                First Name:
            </td>
            <td width="8">
            </td>
            <td>
                <asp:TextBox ID="ReservationFirstName" TabIndex="10" runat="server" Width="216px"
                    MaxLength="30" />
                <asp:RequiredFieldValidator ID="ReservationFirstNameRequired" runat="server" ControlToValidate="ReservationFirstName"
                    ErrorMessage="Please enter first name" Display="Dynamic" />
                <asp:RegularExpressionValidator ID="ReservationFirstNameTooLong" runat="server" ControlToValidate="ReservationFirstName"
                    ErrorMessage="Too long first name entered" ValidationExpression="^.{1,30}$" Display="Dynamic" />&nbsp;
            </td>
        </tr>
        <tr>
            <td width="30%">
                Last Name:
            </td>
            <td width="8">
            </td>
            <td>
                <asp:TextBox ID="ReservationLastName" TabIndex="11" runat="server" Width="216px"
                    MaxLength="30" />
                <asp:RequiredFieldValidator ID="ReservationLastNameRequired" runat="server" ControlToValidate="ReservationLastName"
                    ErrorMessage="Please enter last name" Display="Dynamic" />
                <asp:RegularExpressionValidator ID="ReservationLastNameTooLong" runat="server" ControlToValidate="ReservationLastName"
                    ErrorMessage="Too long last name entered" ValidationExpression="^.{1,30}$" Display="Dynamic" />&nbsp;
            </td>
        </tr>
        <% } %>
        <tr bgcolor="#999966" height="20">
            <td width="30%">
            </td>
            <td width="8">
            </td>
            <td style="height: 10px">
            </td>
        </tr>
        <% if (IfAdmin.Visible)
           { %>
        <tr>
            <td width="30%">
                <asp:Label ID="IfAdminLabel" runat="server">Site administrator:</asp:Label>
            </td>
            <td width="8">
            </td>
            <td>
                <asp:DropDownList ID="IfAdmin" runat="server" Width="64px" Height="24px">
                    <asp:ListItem Value="True">Yes</asp:ListItem>
                    <asp:ListItem Value="False" Selected="True">No</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <% } %>
        <tr>
            <td width="30%">
                <font color="red">*</font>Required field
            </td>
            <td width="8">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td width="30%">
                <asp:Button ID="SubmitButton" TabIndex="20" runat="server" Width="96px" Text="Submit"
                    OnClick="SubmitButton_Click"></asp:Button>
            </td>
            <td width="8">
            </td>
            <td>
                <asp:Button ID="CancelButton" Width="96px" runat="server" Text="Cancel" CausesValidation="False"
                    OnClick="CancelButton_Click" />
            </td>
        </tr>
    </table>
    <asp:Label ID="lblInfo" runat="server" ForeColor="Red"></asp:Label>
    <noscript>
        <img src="http://www.watchwise.net/cgi-watchwise/monitor.cgi?<%= CommonFunctions.GetSiteAddress ().ToLower () %>:all"
            width="1" height="1">
    </noscript>


</div>
    </div>

    <script src="/Assets/js/ownerinfo.js"></script>
</asp:Content>
