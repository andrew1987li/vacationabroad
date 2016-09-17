<%@ Page Title="Newsletter List" Language="C#" MasterPageFile="~/MasterPageNoCss.master" AutoEventWireup="true" CodeFile="NewsletterList.aspx.cs" Inherits="NewsletterList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
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
					<tr><td align="left"><a href="Currency.aspx">Currencies</a></td>
					<td colspan="3" align="left"><a href="PropertyTypes.aspx">Property Types</a>&nbsp;&nbsp;
					<a href="EmailCampaign.aspx">Email Campaign</a>&nbsp;&nbsp;<a href="CommentsAdmin.aspx">Comments</a>&nbsp;&nbsp;
					<a href="NewsletterList.aspx">Newsletters</a>
					</td></tr>
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
<div>
        <h3>
            Newsletter List</h3>
    <asp:Button ID="Button1" runat="server" Text="Create New" onclick="Button1_Click" /><br />
        <asp:GridView ID="grdNews" runat="server" AutoGenerateColumns="False" 
            DataKeyNames="id" onselectedindexchanged="grdNews_SelectedIndexChanged" 
            onrowdeleting="grdNews_RowDeleting" onrowdatabound="grdNews_RowDataBound">
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" 
                            CommandName="Delete" onclientclick="return confirm('Are you sure?');" 
                            Text="Delete"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="dateDep" HeaderText="Date" />
                <asp:BoundField DataField="title" HeaderText="Title" />
                <asp:TemplateField HeaderText="Deployed">
                    <ItemTemplate>
                        <asp:Label ID="lblDeployed" runat="server" Text='<%# Bind("deployed") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("deployed") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Emails Sent">
                    <ItemTemplate>
                        <asp:Label ID="lblEmailID" runat="server" Text='<%# Bind("curEmailID") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("curEmailID") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Deploy Complete">
                    <ItemTemplate>
                        <asp:Label ID="lblComplete" runat="server" Text='<%# Bind("deployComplete") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("deployComplete") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <br />
        <asp:Label ID="lblError" runat="server"></asp:Label>
    </div>
</asp:Content>

