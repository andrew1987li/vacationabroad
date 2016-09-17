<%@ page language="C#" masterpagefile="~/MasterPageNoCss.master" autoeventwireup="true" CodeFile="~/Administration2.aspx.cs" inherits="Administration" title="Administration" enableeventvalidation="false" %>
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
							&nbsp;&nbsp;<a href='<%= CommonFunctions.PrepareURL ("InvoiceRegister.aspx?Year=2007", "Administration") %>'>

                                                                                              Invoice Register 2007

                                                                                  </a>
&nbsp;&nbsp;<a href='<%= CommonFunctions.PrepareURL ("InvoiceRegister.aspx?Year=2008", "Administration") %>'>

                                                                                              Invoice Register 2008

                                                                                  </a>
&nbsp;&nbsp;
							<a href='<%= CommonFunctions.PrepareURL ("InvoiceRegister.aspx?Year=2009", "Administration") %>'>
								Invoice Register 2009
							</a>
<a href='<%= CommonFunctions.PrepareURL ("InvoiceRegister.aspx?Year=2010", "Administration") %>'>
								Invoice Register 2010
							</a>
						</td>
					</tr>
				</table>
			</td>
        </tr>
    </table>
    <p align="center">
        <asp:Label ID="WelcomeLabel" runat="server" Height="16px" Width="100%">
					    Welcome
        </asp:Label>
    </p>
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
    <p align="center">
        <b>
            <asp:Label ID="Label2" runat="server" Height="16px" Width="100%">
						    Modify property by property number:
            </asp:Label>
        </b>
    </p>
    
    
    
    <div align="center">
        <table border="0" cellspacing="0" cellpadding="0" bordercolor="#ccccff">
            <tr>
                <td bordercolor="#ffffff" style="height: 24px">
                    <asp:TextBox ID="PropertyNumber" runat="server" Width="96px" />
                </td>
                <td bordercolor="#ffffff" style="height: 24px">
                    <asp:Button ID="ViewProperty" runat="server" Text="View" Width="96px" OnClick="ViewProperty_Click">
                    </asp:Button>
                </td>
                <td bordercolor="#ffffff" style="height: 24px">
                    <asp:Button ID="EditTextButton" runat="server" Text="Edit Text" Width="96px" OnClick="EditTextButton_Click">
                    </asp:Button>
                </td>
                <td bordercolor="#ffffff" style="height: 24px">
                    <asp:Button ID="EditPhotosButton" runat="server" Text="Edit Photos" Width="96px"
                        OnClick="EditPhotosButton_Click" />
                </td>
                <td bordercolor="#ffffff" style="height: 24px">
                    <button onclick='AskDelete ("DeleteProperty.aspx?PropertyID=" + ctl00_Content_PropertyNumber.value + "&BackLink=Administration.aspx%3F<%# System.Web.HttpUtility.UrlEncode (Request.QueryString.ToString ()) %>&BackLinkText=Return+to+Administration+page");' style="width:96px">
                        Delete</button>
                </td>
                <td bordercolor="#ffffff" style="height: 24px">
                    <asp:Button ID="EmailsButton" runat="server" Text="Emails" Width="96px" OnClick="EmailsButton_Click">
                    </asp:Button>
                </td>
                <td bordercolor="#ffffff" style="height: 24px">
                    <asp:Button ID="OwnerButton" runat="server" Text="Owner" Width="96px" OnClick="OwnerButton_Click">
                    </asp:Button>
                </td>
            </tr>
        </table>
		&nbsp;
        <br />
        <table border="0" cellspacing="0" cellpadding="0" bordercolor="#ccccff">
            <tr>
                <td bordercolor="#ffffff">
                    <asp:Button ID="ChangeDate" runat="server" Text="Change property creation date to:" Width="216px" OnClick="ChangeDate_Click">
                    </asp:Button>
                </td>
                <td bordercolor="#ffffff">
                    <asp:TextBox ID="NewDate" runat="server" Width="96px" />
                </td>
            </tr>
        </table>
        <asp:Label ID="DateUpdateError" runat="server" Visible="False" ForeColor="Red">Incorrect property number</asp:Label><br />
		<asp:Label ID="Label1" runat="server" Text="User ID:"></asp:Label>
		<asp:TextBox ID="UserID" runat="server" Width="59px" />
		<asp:Button ID="ChangeUserDate" runat="server" OnClick="ChangeUserDate_Click" Text="Change user creation date to:"
			Width="184px" />
		<asp:TextBox ID="NewUserDate" runat="server" Width="79px" /><br />
		&nbsp;
		<asp:Label ID="UserIDError" runat="server" ForeColor="Red" Visible="False">Incorrect user ID</asp:Label><br />
		<br />
		<asp:Label ID="Label3" runat="server" Text="Property ID:"></asp:Label>
		<asp:TextBox ID="PropertyID" runat="server" Width="96px" />
		<asp:Label ID="Label4" runat="server" Text="Invoice Amount:"></asp:Label>
		<asp:TextBox ID="InvoiceAmount" runat="server" Width="96px" />
		<asp:Button ID="CreateInvoice" runat="server" OnClick="CreateInvoice_Click" Text="Create Invoice"
			Width="126px" />
        <br />
        <br />
		<asp:Label ID="PropertyIDError" runat="server" ForeColor="Red" Visible="False">Incorrect property number</asp:Label>
        <br />
        <br />
        Email Address:
        <asp:TextBox ID="EmailAddressSearch" runat="server"></asp:TextBox>
        <asp:Button ID="BtnFindEmail" runat="server" onclick="BtnFindEmail_Click" 
            Text="Find Email address" />
        <br />
        
        <asp:Label ID="EmailAdrError" runat="server" ForeColor="Red" Visible="False"/>
</div>
    
    
    <asp:Repeater ID="Repeater1" runat="server" DataMember="Properties" DataSource="<%# PropertiesSet %>">
        <HeaderTemplate>
            <table bordercolor="#ffffff" border="1" cellspacing="0" cellpadding="0" width="100%">
                <tr>
                    <td bordercolor="#ffffff" colspan="100" align="center">
                        <b>
                            <br />
                            Properties awaiting approval:
                            <br />
                            <br />
                        </b>
                    </td>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td width="100%" bgcolor="#e4e4af">
                    <a href='<%# CommonFunctions.PrepareURL ("ViewProperty.aspx?UserID=" + DataBinder.Eval(Container.DataItem, "UserID", "{0:d}") + "&PropertyID=" + DataBinder.Eval(Container.DataItem, "ID", "{0:d}"), "Administration") %>'>
                        <%# DataBinder.Eval(Container.DataItem, "Name", "{0}") %>
                    </a>
                </td>
                <td bordercolor="#ffffff">
                    <input type="button" value="Approve" onclick='window.location.href=&quot;<%# CommonFunctions.PrepareURL ("ApproveProperty.aspx?UserID=" + DataBinder.Eval(Container.DataItem, "UserID", "{0:d}") + "&PropertyID=" + DataBinder.Eval(Container.DataItem, "ID", "{0:d}"), "") %>&quot;'>
                </td>
                <td bordercolor="#ffffff">
                    <input type="button" value="Edit" onclick='window.location.href=&quot;<%# CommonFunctions.PrepareURL ("EditProperty.aspx?UserID=" + DataBinder.Eval(Container.DataItem, "UserID", "{0:d}") + "&PropertyID=" + DataBinder.Eval(Container.DataItem, "ID", "{0:d}"), "Administration") %>&quot;'>
                </td>
                <td bordercolor="#ffffff">
                    <input type="button" value="Delete" onclick='window.location.href=&quot;<%# CommonFunctions.PrepareURL ("DeleteProperty.aspx?UserID=" + DataBinder.Eval(Container.DataItem, "UserID", "{0:d}") + "&PropertyID=" + DataBinder.Eval(Container.DataItem, "ID", "{0:d}"), "") %>&quot;'>
                </td>
            </tr>
            <tr>
                <td>
                    <%# DataBinder.Eval(Container.DataItem, "Description", "{0}") %>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table></FooterTemplate>
    </asp:Repeater>
    
     <script language="javascript" type="text/javascript">
    <!--

	function AskDelete (link)
	{
		if (window.confirm ("Are you sure you want to do this?"))
			window.location.href = link;
	}

   
    </script>
</asp:Content>
