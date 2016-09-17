<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageNoCss.master" ValidateRequest="false" AutoEventWireup="true" CodeFile="Newsletter.aspx.cs" Inherits="Newsletter" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>

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
					<a href="NewsletterList.aspx">Newsletters</a>&nbsp;&nbsp;<a href="TourLinks.aspx">Tour Links</a>
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
<div align="left">
<h3>Newsletters</h3>
 
        <asp:Label ID="lblError" runat="server" EnableViewState="False" ForeColor="Red"></asp:Label><br />
    Title&nbsp;&nbsp;&nbsp;&nbsp; <asp:TextBox ID="txtTitle" runat="server" 
            Width="421px"></asp:TextBox>
           
        <br />
        From Email&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtFrom" runat="server" Width="421px"></asp:TextBox>
        <br />
        From Name&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtFromName" runat="server" Width="421px"></asp:TextBox><br />
        <asp:FileUpload ID="FileUpload1" runat="server" Visible="False" /><br />
        <asp:Button ID="btnAttach" runat="server" Text="Attach File" 
            onclick="btnAttach_Click" Visible="False" /><br />
       
        <FCKeditorV2:FCKeditor ID="Editor" runat="server" Height="500px" Width="100%" BasePath="FCKeditor/">
        </FCKeditorV2:FCKeditor>
        <br />
        <asp:Button ID="btnSubmit" runat="server" Text="Save Newsletter" 
            onclick="btnSubmit_Click" EnableViewState="False" />
            
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnDeploy" runat="server" Text="Deploy Newsletter" 
            Enabled="False" onclick="btnDeploy_Click" EnableViewState="False" 
            onclientclick="return confirm('Are you sure?');" />        
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnPreview" runat="server" Text="Preview" 
            EnableViewState="False" onclick="btnPreview_Click" Width="100px" />  <br />
            <hr size="50%" />
            Deploy Test to Specified Email Address:
        <asp:TextBox ID="txtTest" runat="server" Width="250px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnTest" runat="server" Text="Deploy Test" Width="100px" 
            onclick="btnTest_Click" />
        
    </div>
</asp:Content>

