<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="OwnersList2.aspx.cs" Inherits="OwnersList2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
	<title>Owners List</title>
</head>

<body runat="server" id="BodyTag" onunload="vUnload" bgcolor="#C1C4D7" text="#000000" vlink="#9C9C9C" alink="#828282" link="#3366CC" leftmargin="0" topmargin="0">
	<form id="MainForm" runat="server">
		<table bordercolor="#006699" bgcolor="#ffffff" cellspacing="1" cellpadding="" width="750" align="center" border="0">
			<tr bordercolor="#ffffff">
				<td colspan="5">
					<table border="0" width="100%">
						<tr>
							<td width="33%" align="left"><font size="2" face="Arial">
								Search for vacation rentals<br />
								<asp:TextBox ID="KeyWords" runat="server" />
								<asp:Button ID="SearchByKeyWords" runat="server" OnClick="SearchByKeyWords_Click"
									Text="Search" CausesValidation="False" />
							</font></td>
							<td width="33%" align="center">
								<a href='<%= CommonFunctions.PrepareURL ("") %>'>
									<asp:Image runat="server" ID="Logo" ImageAlign="middle"
										AlternateText='Vacations-Abroad.com' Width="215" Height="58"
										BorderWidth="0" ImageUrl='images2/logo.gif' />
								</a>
							</td>
							<td width="33%" align="center"><font size="2" face="Arial">
<br>

								<% if (!AuthenticationManager.IfAuthenticated) { %>
								<asp:HyperLink ID="LogInLink" runat="server" NavigateUrl="Login.aspx">Log In</asp:HyperLink><br />
								<asp:HyperLink ID="CreateAccountLink" runat="server" NavigateUrl="FindOwner.aspx">Create An Account</asp:HyperLink><br />
								<% } else { %>
								<asp:HyperLink ID="LogOutLink" runat="server" NavigateUrl="Logout.aspx">Log Out</asp:HyperLink> /
								<asp:HyperLink ID="UserIDLink" runat="server" NavigateUrl="MyAccount.aspx">User ID (<%= AuthenticationManager.Username %>)</asp:HyperLink><br />
								<% } %>
								<% if (AuthenticationManager.IfAuthenticated && AuthenticationManager.IfAdmin) { %>
								<asp:HyperLink ID="OwnersLinkLink" runat="server" NavigateUrl="OwnersList.aspx">Owners List</asp:HyperLink> /
								<asp:HyperLink ID="OutStandingInvoicesLink" runat="server" NavigateUrl="OutstandingInvoices.aspx">Outstanding Inv</asp:HyperLink> /
								<asp:HyperLink ID="AdminLink" runat="server" NavigateUrl="Administration.aspx">Admin</asp:HyperLink>
								<% } %>
							</font></td>

						</tr>


					</table>
				</td>
			</tr>
			<tr bordercolor="#ffffff">
				<td align="center" colspan="5">
					<a href='<%= CommonFunctions.PrepareURL ("") %>'>
						<asp:Image runat="server" ID="MainLogo" ImageAlign="middle" AlternateText="Main Logo"
							ImageUrl='images2/main2.jpg' Width="750" Height="140"
							BorderWidth="0" />
					</a>
				</td>
			</tr>
           		<tr border="0" bgcolor="#8AA37B">
				<td width="20%" style="height: 21px" align="center">
					<font face="Arial, serif" color="#cc9933" size="2">
					<a href='<%= CommonFunctions.PrepareURL ("") %>'>
					<font size="2" face="Arial" color="#ffffff">Vacation Rentals</font></a>
					
				</td>
				<td width="20%" border="0" style="height: 21px" align="center">
					<font face="Arial" color="#cc9933" size="2">
						<a href='<%= CommonFunctions.PrepareURL ("newsletter/index.htm") %>'>
							<font size="2" face="Arial" color="#ffffff">Newsletter
						</font></a>
					
				</td>
				<td width="20%" style="height: 21px" align="center">
					<font face="Arial" color="#cc9933" size="2">
						<a href='<%= CommonFunctions.PrepareURL ("photos/index.htm") %>'>
							<font size="2" face="Arial" color="#ffffff">Travel Photos
						</font></a>
					
				</td>
				<td width="20%" style="height: 21px" align="center">
					<font face="Arial" color="#cc9933" size="2">
						<a href="http://vacationsabroad.blogspot.com">
							<font size="2" face="Arial" color="#ffffff">Blog
						</font></a>
					
				</td>
				<td width="20%" style="height: 21px" align="center">
					<font face="Arial" color="#cc9933" size="2">
						<a href='<%= CommonFunctions.PrepareURL ("Applications.htm") %>'>
							<font size="2" face="Arial" color="#ffffff">List Your Rental
						</font></a>
					
				</td>


			</tr>
			
			
			<tr valign="top" bordercolor="#ffffff" bgcolor="#ffffff">
				<td colspan="5">
				<%--content--%>
				
				<div style="width:100%">
     <div style="width:50px; float:left;"><asp:LinkButton ID="lnkAB" runat="server" 
             onclick="lnkAB_Click">A-B</asp:LinkButton></div>
    <div style="width:50px; float:left;"><asp:LinkButton ID="lnkCI" runat="server" 
            onclick="lnkCD_Click">C-D</asp:LinkButton></div>
    <div style="width:50px; float:left;"><asp:LinkButton ID="lnkJN" runat="server" 
            onclick="lnkEG_Click">E-G</asp:LinkButton></div>
    <div style="width:50px; float:left;"><asp:LinkButton ID="lnkOR" runat="server" 
            onclick="lnkHI_Click">H-I</asp:LinkButton></div>
    <div style="width:50px; float:left;"><asp:LinkButton ID="lnkST" runat="server" 
            onclick="lnkJL_Click">J-L</asp:LinkButton></div>
    <div style="width:50px; float:left;"><asp:LinkButton ID="lnkUZ" runat="server" 
            onclick="lnkMO_Click">M-O</asp:LinkButton> </div>
             <div style="width:50px; float:left;"><asp:LinkButton ID="LinkButton1" runat="server" 
            onclick="lnkPR_Click">P-R</asp:LinkButton> </div>
    <div style="width:50px; float:left;"><asp:LinkButton ID="LinkButton2" runat="server" 
            onclick="lnkST_Click">S-T</asp:LinkButton> </div>
            <div style="width:50px; float:left;"><asp:LinkButton ID="LinkButton3" runat="server" 
            onclick="lnkUZ_Click">U-Z</asp:LinkButton> </div>
            
            
    </div>
                    <asp:Button ID="btnSubmit" runat="server" Text="Save All Results" 
                        onclick="btnSubmit_Click" />
    <br />   
                     <asp:Label ID="lblError" runat="server"></asp:Label>
    <%--<div id="divResultsDisplay" runat="server">
    Results per page:
        <asp:RadioButton ID="RadioButton1" runat="server" Text="50" />
        <asp:RadioButton ID="RadioButton2" runat="server" Text="100" />
    </div>--%>
    <br />
        <asp:GridView ID="grdOwners" runat="server" PageSize="50" 
            AutoGenerateColumns="False">
        <Columns>
            <asp:TemplateField Visible="False">
                <ItemTemplate>
                    <asp:Label ID="lblID" runat="server" Text='<%# Bind("id") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox13" runat="server" Text='<%# Bind("id") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Email">
                <ItemTemplate>
                    <asp:TextBox ID="txtEmail" runat="server" Text='<%# Bind("Email") %>'></asp:TextBox>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Email") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="First Name">
                <ItemTemplate>
                    <asp:TextBox ID="txtFirstName" runat="server" Text='<%# Bind("FirstName") %>'>
                        </asp:TextBox>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("FirstName") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Last Name">
                <ItemTemplate>
                    <asp:TextBox ID="txtLastName" runat="server" Text='<%# Bind("LastName") %>'></asp:TextBox>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("LastName") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="City">
                <ItemTemplate>
                    <asp:TextBox ID="txtCity" runat="server" Text='<%# Bind("City") %>'></asp:TextBox>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("City") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="State">
                <ItemTemplate>
                    <asp:TextBox ID="txtState" runat="server" Text='<%# Bind("State") %>'></asp:TextBox>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("State") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Country">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Country") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("Country") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Primary Telephone">
                <ItemTemplate>
                    <asp:TextBox ID="txtPrimaryTelephone" runat="server" 
                        Text='<%# Bind("primarytelephone") %>'></asp:TextBox>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox7" runat="server" 
                        Text='<%# Bind("primarytelephone") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Evening Telephone">
                <ItemTemplate>
                    <asp:TextBox ID="txtEveningTelephone" runat="server" 
                        Text='<%# Bind("eveningtelephone") %>'></asp:TextBox>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox8" runat="server" 
                        Text='<%# Bind("eveningtelephone") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Mobile Telephone">
                <ItemTemplate>
                    <asp:TextBox ID="txtMobileTelephone" runat="server" 
                        Text='<%# Bind("mobiletelephone") %>'></asp:TextBox>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox10" runat="server" 
                        Text='<%# Bind("mobiletelephone") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Website">
                <ItemTemplate>
                    <asp:TextBox ID="txtWebsite" runat="server" Text='<%# Bind("website") %>'></asp:TextBox>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox11" runat="server" Text='<%# Bind("website") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Company Name">
                <ItemTemplate>
                    <asp:TextBox ID="txtCompanyName" runat="server" 
                        Text='<%# Bind("companyname") %>'></asp:TextBox>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox12" runat="server" Text='<%# Bind("companyname") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>   
                    
                    <br />
                   
				<%--content--%>
					
				</td>
			</tr>
			
			<tr bordercolor="#ffffff">
				<td colspan="5">
					<font size="2" face="Arial" ><p align="center">
								<a href='<%= CommonFunctions.PrepareURL ("TravelAgents.aspx") %>'>Travel Agents</a>,
								<a href='<%= CommonFunctions.PrepareURL ("Disclaimer.htm") %>'>Disclaimer</a>,
								<a href='<%= CommonFunctions.PrepareURL ("FAQ.htm") %>'>FAQs</a>,
								<a href='<%= CommonFunctions.PrepareURL ("Contacts.htm") %>'>Contact</a>,
<a href='<%= CommonFunctions.PrepareURL ("sitemaps.html") %>'>Sitemaps</a>,
								<a href="http://resources.vacations-abroad.com/index.asp">Travel Links</a>,
								<br />
								<blockquote> 
								Copyright ©2000-2009 <%= CommonFunctions.GetSiteName () %> is a registered trademark - All rights reserved. 
							<br>Thank you for visiting Vacations-Abroad.com<br></font>
						</p>
					</div>
				</td>
			</tr>
		</table>
	</form></body>
<!-- start counter code -->
<script type="text/javascript" id="wa_u"></script>
<script type="text/javascript">//<![CDATA[
// Begin Variable Declarations
wa_account="899E9C9E8B9690918CD29E9D8D909E9B"; wa_location=31;
wa_pageName=location.pathname;  // you can customize the page name here
// End Variable Declarations
document.cookie='__support_check=1';wa_hp='http';
wa_rf=document.referrer;wa_sr=window.location.search;
wa_tz=new Date();if(location.href.substr(0,6).toLowerCase()=='https:')
wa_hp='https';wa_data='&an='+escape(navigator.appName)+ 
'&sr='+escape(wa_sr)+'&ck='+document.cookie.length+
'&rf='+escape(wa_rf)+'&sl='+escape(navigator.systemLanguage)+
'&av='+escape(navigator.appVersion)+'&l='+escape(navigator.language)+
'&pf='+escape(navigator.platform)+'&pg='+escape(wa_pageName);
wa_data=wa_data+'&cd='+
screen.colorDepth+'&rs='+escape(screen.width+ ' x '+screen.height)+
'&tz='+wa_tz.getTimezoneOffset()+'&je='+ navigator.javaEnabled();
wa_img=new Image();wa_img.src=wa_hp+'://counter.hitslink.com/statistics.asp'+
'?v=1&s='+wa_location+'&eacct='+wa_account+wa_data+'&tks='+wa_tz.getTime();
document.getElementById('wa_u').src=wa_hp+'://counter.hitslink.com/track.js';
 //]]>
</script>
<!-- End counter code -->

</html>

