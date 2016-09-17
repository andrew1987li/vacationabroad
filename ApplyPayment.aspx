<%@ Page Language="C#" MasterPageFile="~/MasterPageNoCss.master" AutoEventWireup="true" CodeFile="ApplyPayment.aspx.cs" Inherits="ApplyPayment" Title="Apply Payment" %>
<asp:Content ID="Content" ContentPlaceHolderID="Content" Runat="Server">
    <table bgcolor="#e4e4af" cellspacing="0" cellpadding="0" width="100%" align="center"
        border="2">
        <tr>
            <td colspan="100">
                <div align="center">
                    <strong>
                        <asp:HyperLink ID="Hyperlink7" runat="server" NavigateUrl="Administration.aspx">
							Main Administration page
                        </asp:HyperLink>
                    </strong>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div align="center">
                    <strong>
                        <asp:HyperLink ID="Hyperlink2" runat="server" NavigateUrl="OwnersList.aspx">
							Owners
                        </asp:HyperLink>
                    </strong>
                </div>
            </td>
            <td>
                <div align="center">
                    <strong>
                        <asp:HyperLink ID="Hyperlink3" runat="server" NavigateUrl="Locations.aspx">
							Locations
                        </asp:HyperLink>
                    </strong>
                </div>
            </td>
            <td>
                <div align="center">
                    <strong>
                        <asp:HyperLink ID="Hyperlink4" runat="server" NavigateUrl="SendCustomEmail.aspx">
							Send Email
                        </asp:HyperLink>
                    </strong>
                </div>
            </td>
            <td>
                <div align="center">
                    <strong>
                        <asp:HyperLink ID="Hyperlink1" runat="server" NavigateUrl="OutstandingInvoices.aspx">
							Outstanding Invoices
                        </asp:HyperLink>
                    </strong>
                </div>
            </td>
            <td>
                <div align="center">
                    <strong>
                        <asp:HyperLink ID="Hyperlink5" runat="server" NavigateUrl="FreeTrial.aspx">
							Free Trial Properties
                        </asp:HyperLink>
                    </strong>
                </div>
            </td>
            <td>
                <div align="center">
                    <strong>
                        <asp:HyperLink ID="Hyperlink6" runat="server" NavigateUrl="PaymentsReceived.aspx">
							Payments Received
                        </asp:HyperLink>
                    </strong>
                </div>
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
    <table bgcolor="#e4e4af" cellspacing="0" cellpadding="0" width="350" align="center"
        border="2">
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
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Height="16px">Enter invoice number to choose another invoice:</asp:Label>
            </td>
            <td>
                <asp:TextBox ID="ChooseInvoiceNumber" runat="server" Width="104px" />
                <asp:RequiredFieldValidator ID="EnterInvoiceNumber" runat="server" Display="Dynamic"
					ErrorMessage="Please enter invoice number" ControlToValidate="ChooseInvoiceNumber" />
                <asp:RegularExpressionValidator ID="ValidInvoiceNumber" runat="server" Display="Dynamic"
                    ErrorMessage="Invalid number entered" ControlToValidate="ChooseInvoiceNumber"
                    ValidationExpression="^[0-9 \.\-\(\)]{1,300}$" />
                <asp:Button ID="ChooseInvoice" runat="server" Width="112px" Height="24px" Text="Choose invoice"
                    CausesValidation="False" OnClick="ChooseInvoice_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label2" runat="server" Height="16px">Enter property number to choose invoice for that property:</asp:Label>
            </td>
            <td>
                <asp:TextBox ID="ChoosePropertyNumber" runat="server" Width="104px" />
                <asp:RequiredFieldValidator ID="EnterPropertyNumber" runat="server" ErrorMessage="Please enter property number"
                    ControlToValidate="ChoosePropertyNumber" Display="Dynamic" />
                <asp:RegularExpressionValidator ID="ValidPropertyNumber" runat="server" ValidationExpression="^[0-9 \.\-\(\)]{1,300}$"
                    ErrorMessage="Invalid number entered" ControlToValidate="ChoosePropertyNumber"
                    Display="Dynamic" />
                <asp:Button ID="ChooseProperty" runat="server" Width="112px" Text="Choose invoice"
                    CausesValidation="False" OnClick="ChooseProperty_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="100">
                <asp:Label ID="NotFound" runat="server" Height="16px" ForeColor="Red" Visible="False">No invoices found</asp:Label>
            </td>
        </tr>
    </table>
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td width="20%">
                <asp:Label ID="Label3" runat="server" Height="16px">Owner Name</asp:Label>
            </td>
            <td>
                <asp:TextBox ID="OwnerName" runat="server" Width="176px" ReadOnly="True" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label4" runat="server" Height="16px">Property Number</asp:Label>
            </td>
            <td>
                <asp:TextBox ID="PropertyNumber" runat="server" Width="120px" ReadOnly="True" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label5" runat="server" Height="16px">Invoice Number</asp:Label>
            </td>
            <td>
                <asp:TextBox ID="InvoiceNumber" runat="server" Width="120px" ReadOnly="True" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label6" runat="server" Height="16px">Invoice Date</asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="InvoiceDay" runat="server" Height="16px" Width="48px" Enabled="False">
                    <asp:ListItem Value="1">1</asp:ListItem>
                    <asp:ListItem Value="2">2</asp:ListItem>
                    <asp:ListItem Value="3">3</asp:ListItem>
                    <asp:ListItem Value="4">4</asp:ListItem>
                    <asp:ListItem Value="5">5</asp:ListItem>
                    <asp:ListItem Value="6">6</asp:ListItem>
                    <asp:ListItem Value="7">7</asp:ListItem>
                    <asp:ListItem Value="8">8</asp:ListItem>
                    <asp:ListItem Value="9">9</asp:ListItem>
                    <asp:ListItem Value="10">10</asp:ListItem>
                    <asp:ListItem Value="11">11</asp:ListItem>
                    <asp:ListItem Value="12">12</asp:ListItem>
                    <asp:ListItem Value="13">13</asp:ListItem>
                    <asp:ListItem Value="14">14</asp:ListItem>
                    <asp:ListItem Value="15">15</asp:ListItem>
                    <asp:ListItem Value="16">16</asp:ListItem>
                    <asp:ListItem Value="17">17</asp:ListItem>
                    <asp:ListItem Value="18">18</asp:ListItem>
                    <asp:ListItem Value="19">19</asp:ListItem>
                    <asp:ListItem Value="20">20</asp:ListItem>
                    <asp:ListItem Value="21">21</asp:ListItem>
                    <asp:ListItem Value="22">22</asp:ListItem>
                    <asp:ListItem Value="23">23</asp:ListItem>
                    <asp:ListItem Value="24">24</asp:ListItem>
                    <asp:ListItem Value="25">25</asp:ListItem>
                    <asp:ListItem Value="26">26</asp:ListItem>
                    <asp:ListItem Value="27">27</asp:ListItem>
                    <asp:ListItem Value="28">28</asp:ListItem>
                    <asp:ListItem Value="29">29</asp:ListItem>
                    <asp:ListItem Value="30">30</asp:ListItem>
                    <asp:ListItem Value="31">31</asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList ID="InvoiceMonth" runat="server" Height="24px" Width="88px" Enabled="False">
                    <asp:ListItem Value="1">January</asp:ListItem>
                    <asp:ListItem Value="2">February</asp:ListItem>
                    <asp:ListItem Value="3">March</asp:ListItem>
                    <asp:ListItem Value="4">April</asp:ListItem>
                    <asp:ListItem Value="5">May</asp:ListItem>
                    <asp:ListItem Value="6">June</asp:ListItem>
                    <asp:ListItem Value="7">July</asp:ListItem>
                    <asp:ListItem Value="8">August</asp:ListItem>
                    <asp:ListItem Value="9">September</asp:ListItem>
                    <asp:ListItem Value="10">October</asp:ListItem>
                    <asp:ListItem Value="11">November</asp:ListItem>
                    <asp:ListItem Value="12">December</asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList ID="InvoiceYear" runat="server" Height="16px" Width="72px" Enabled="False">
                    <asp:ListItem Value="2004">2004</asp:ListItem>
                    <asp:ListItem Value="2005">2005</asp:ListItem>
                    <asp:ListItem Value="2006">2006</asp:ListItem>
                    <asp:ListItem Value="2007">2007</asp:ListItem>
                    <asp:ListItem Value="2008">2008</asp:ListItem>
                    <asp:ListItem Value="2009">2009</asp:ListItem>
                    <asp:ListItem Value="2010">2010</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label7" runat="server" Height="16px">Invoice Amount</asp:Label>
            </td>
            <td>
                <asp:TextBox ID="InvoiceAmount" runat="server" Width="120px" ReadOnly="True" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label8" runat="server" Height="16px">Payment Date</asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="PaymentDay" runat="server" Height="16px" Width="48px">
                    <asp:ListItem Value="1">1</asp:ListItem>
                    <asp:ListItem Value="2">2</asp:ListItem>
                    <asp:ListItem Value="3">3</asp:ListItem>
                    <asp:ListItem Value="4">4</asp:ListItem>
                    <asp:ListItem Value="5">5</asp:ListItem>
                    <asp:ListItem Value="6">6</asp:ListItem>
                    <asp:ListItem Value="7">7</asp:ListItem>
                    <asp:ListItem Value="8">8</asp:ListItem>
                    <asp:ListItem Value="9">9</asp:ListItem>
                    <asp:ListItem Value="10">10</asp:ListItem>
                    <asp:ListItem Value="11">11</asp:ListItem>
                    <asp:ListItem Value="12">12</asp:ListItem>
                    <asp:ListItem Value="13">13</asp:ListItem>
                    <asp:ListItem Value="14">14</asp:ListItem>
                    <asp:ListItem Value="15">15</asp:ListItem>
                    <asp:ListItem Value="16">16</asp:ListItem>
                    <asp:ListItem Value="17">17</asp:ListItem>
                    <asp:ListItem Value="18">18</asp:ListItem>
                    <asp:ListItem Value="19">19</asp:ListItem>
                    <asp:ListItem Value="20">20</asp:ListItem>
                    <asp:ListItem Value="21">21</asp:ListItem>
                    <asp:ListItem Value="22">22</asp:ListItem>
                    <asp:ListItem Value="23">23</asp:ListItem>
                    <asp:ListItem Value="24">24</asp:ListItem>
                    <asp:ListItem Value="25">25</asp:ListItem>
                    <asp:ListItem Value="26">26</asp:ListItem>
                    <asp:ListItem Value="27">27</asp:ListItem>
                    <asp:ListItem Value="28">28</asp:ListItem>
                    <asp:ListItem Value="29">29</asp:ListItem>
                    <asp:ListItem Value="30">30</asp:ListItem>
                    <asp:ListItem Value="31">31</asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList ID="PaymentMonth" runat="server" Height="24px" Width="88px">
                    <asp:ListItem Value="1">January</asp:ListItem>
                    <asp:ListItem Value="2">February</asp:ListItem>
                    <asp:ListItem Value="3">March</asp:ListItem>
                    <asp:ListItem Value="4">April</asp:ListItem>
                    <asp:ListItem Value="5">May</asp:ListItem>
                    <asp:ListItem Value="6">June</asp:ListItem>
                    <asp:ListItem Value="7">July</asp:ListItem>
                    <asp:ListItem Value="8">August</asp:ListItem>
                    <asp:ListItem Value="9">September</asp:ListItem>
                    <asp:ListItem Value="10">October</asp:ListItem>
                    <asp:ListItem Value="11">November</asp:ListItem>
                    <asp:ListItem Value="12">December</asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList ID="PaymentYear" runat="server" Height="16px" Width="72px">
                    <asp:ListItem Value="2004">2004</asp:ListItem>
                    <asp:ListItem Value="2005">2005</asp:ListItem>
                    <asp:ListItem Value="2006">2006</asp:ListItem>
                    <asp:ListItem Value="2007">2007</asp:ListItem>
                    <asp:ListItem Value="2008">2008</asp:ListItem>
                    <asp:ListItem Value="2009">2009</asp:ListItem>
                    <asp:ListItem Value="2010">2010</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label9" runat="server" Height="16px">Payment Amount</asp:Label>
            </td>
            <td>
                <asp:TextBox ID="PaymentAmount" runat="server" Width="120px" AutoPostBack="True"
                    OnTextChanged="PaymentAmount_TextChanged" />
                <asp:RegularExpressionValidator ID="ValidPaymentAmount" runat="server" Display="Dynamic"
                    ControlToValidate="PaymentAmount" ErrorMessage="Invalid number entered" ValidationExpression="^[0-9 \.\-\(\)]{1,300}$" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label10" runat="server" Height="16px">Payment Method</asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="PaymentType" runat="server" Height="24px" Width="120px">
                    <asp:ListItem Value="Propay">Propay</asp:ListItem>
                    <asp:ListItem Value="Paypal">Paypal</asp:ListItem>
                    <asp:ListItem Value="Checks">Checks</asp:ListItem>
                    <asp:ListItem Value="Cash">Cash</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label11" runat="server" Height="16px">Balance</asp:Label>
            </td>
            <td>
                <asp:TextBox ID="Balance" runat="server" Width="120px" ReadOnly="True" />
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
