<%@ Page Language="C#" MasterPageFile="~/MasterPageNoCss.master" AutoEventWireup="true" CodeFile="AgentAccount.aspx.cs" Inherits="AgentAccount" Title="Agent Account" %>

<asp:Content ID="Content" ContentPlaceHolderID="Content" Runat="Server">
	<% if (BackLink.Visible) { %>
	<table cellspacing="0" cellpadding="0" width="250" align="center" bgcolor="#e4e4af" border="2">
		<tr>
			<td>
				<div align="center">
					<strong>
						<asp:HyperLink ID="BackLink" runat="server" NavigateUrl="OwnersList.aspx">
							Return to Owners list
						</asp:HyperLink>
					</strong>
				</div>
			</td>
		</tr>
	</table>
	<br />
	<% } %>
	<div align="center">
		<strong>
			<asp:Label ID="WelcomeLabel" runat="server" Height="16px" Width="100%">
				Welcome
			</asp:Label>
		</strong>
	</div>
	<% if (ifclients) { %>
	<div align="center">
		<a href='<%= CommonFunctions.PrepareURL ("AgentClients.aspx?UserID=" + userid.ToString (), "Agent Account") %>'>
			My Clients
		</a>
	</div>
	<% } %>
	<% if (ifcommissions) { %>
	<div align="center">
		<a href='<%= CommonFunctions.PrepareURL ("AgentCommissions.aspx?UserID" + userid.ToString (), "Agent Account") %>'>
			My Commissions
		</a>
	</div>
	<% } %>
	<% if (ifagents) { %>
	<div align="center">
		<a href='<%= CommonFunctions.PrepareURL ("AgentAgents.aspx?UserID=" + userid.ToString (), "Agent Account") %>'>
			My Agents
		</a>
	</div>
	<% } %>
	<br />
	<table border="0" width="100%">
		<tr>
			<td width="4%">
			</td>
			<td width="92%">
                <div id="id_2">
<p class="p9 ft7">Our Commission Percentage</p>
<p class="p10 ft9"><span class="ft8"> </span>7% charged to the property owner / manager on gross sales</p><br />
<p class="p9 ft7">Our Cancellation Policy</p>
<p class="p12 ft12"><span class="ft10"></span><span class="ft11">60 or more days prior to </span><nobr>arrival—100%</nobr> refunded</p>
<p class="p13 ft12"><span class="ft10"></span><nobr><span class="ft11">50-60</span></nobr> days prior to arrival— 90% refunded</p>
<p class="p14 ft12"><span class="ft10"></span><nobr><span class="ft11">40-50</span></nobr> days prior to <nobr>arrival—80%</nobr> refunded</p>
<p class="p14 ft12"><span class="ft8"></span><nobr><span class="ft11">30-40</span></nobr> days prior to arrival— 70% refunded</p>
<p class="p14 ft12"><span class="ft8"></span><nobr><span class="ft11">20-30</span></nobr> days prior to arrival— 60% refunded</p>
<p class="p13 ft12"><span class="ft8"></span><nobr><span class="ft11">14-20</span></nobr> days prior to arrival— 50% refunded</p>
<p class="p15 ft14"><span class="ft10"></span><nobr><span class="ft13">0-14</span></nobr> days prior to <nobr>arrival—No</nobr> refund unless property can rebook</p><br />
<p class="p9 ft7">What Is Not Included</p>
<p class="p12 ft12"><span class="ft10"></span><span class="ft11">We do not collect security deposits</span></p>
<p class="p13 ft12"><span class="ft10"></span><span class="ft11">We do not collect local sales tax</span></p><br />
<p class="p9 ft7">When You Receive Your Funds</p>
<p class="p18 ft14"><span class="ft10"></span><span class="ft13">Your funds will be displayed immediately in your online account</span></p>
<p class="p19 ft14"><span class="ft10"></span><span class="ft13">The funds will be transferred to your bank </span><nobr>1-2</nobr> days after the arrival of the guest.</p>
</div>

                <%--<h2>Our Commission Percentage</h2>
                <ul>
                    <li>
                        7% charged to the property owner / manager on gross sales
                    </li>
                </ul>
                <h2>Our Cancellation Policy</h2>
                <ul>
                    <li>
                        60 or more days prior to arrival—100% refunded
                    </li>
                    <li>
                        50-60 days prior to arrival— 90% refunded
                    </li>
                    <li>
                        40-50 days prior to arrival—80% refunded
                    </li>
                    <li>
                        30-40 days prior to arrival— 70% refunded
                    </li>
                    <li>
                        20-30 days prior to arrival— 60% refunded
                    </li>
                    <li>
                        14-20 days prior to arrival— 50% refunded
                    </li>
                    <li>
                        0-14 days prior to arrival—No refund unless property can rebook 
                    </li>
                </ul>
                <h2>What Is Not Included</h2>
                <ul>
                    <li>
                        We do not collect security deposits
                    </li>
                    <li>
                        We do not collect local sales tax
                    </li>
                </ul>
                <h2>When You Receive Your Funds</h2>
                <ul>
                    <li>
                        Your funds will be displayed immediately in your online account 
                    </li>
                    <li>
                        The funds will be transferred to your bank 1-2 days after the arrival of the guest.
                    </li>
                </ul>--%>
				<%--We are very excited to be working with you as an agent. We will pay you a commission on each transaction
				that is completed by your client for as long as they remain on the website.<br /><br />
				We currently are working only with people who have paypal accounts. The rationale for this is that the
				cost of sending funds to international agents through the banking system is exorbitant.<br /><br />
				We will be making a monthly payment for all commission earned the previous month.<br /><br />
				A list of countries in which Paypal will accept payments:<br />
				Australia, Argentina, Austria, Belgium, Brazil, Canada, Chile, China, Denmark, Ecuador, France, Finland,
				Germany, Greece, Hong Kong, India, Italy, Ireland, Jamaica, Japan, Mexico, New Zealand, Netherlands,
				Norway, Portugal, Singapore, South Korea, Spain, Sweden, Switzerland, Taiwan, United Kingdom,
				United States, Uruguay<br />
				Additional terms can be found on the Paypal website:
				<a href="http://www.paypal.com/cgi-bin/webscr?cmd=_display-approved-signup-countries-outside">
					click here to view all terms
				</a><br /><br />
				When a client lists a property on the website, they will be asked who referred them.
				They will then be prompted to select from our list of agents. This list will be based on an agent's
				last name. Once a client is identified to a specific agent, the agent will receive commissioin fees
				each time a transaction is completed with that entity. If the client advertises 10 properties, the agent
				will receive fees calculated on those ten properties for as long as those properties are listed on the
				website.<br /><br />
				Commission Fee <%= ConfigurationManager.AppSettings["AgentCommission"]%> of Fees Collected.<br /><br />
				Any new agents that are recruited by you; you will receive an additional
				<%= ConfigurationManager.AppSettings["SubAgentCommission"]%> commission of fees generated in
				their account.<br /><br />
				<asp:CheckBox ID="AgentCheckBox" runat="server" Text="Yes, I wish to become an agent of <%= CommonFunctions.GetSiteName () %>" /><br />
				<br />
				To become an Agent of <%= CommonFunctions.GetSiteName () %> we will require that you provide additional information.<br />
				<br />
				<asp:Button ID="SubmitButton" runat="server" OnClick="SubmitButton_Click" Text="Add Additional Info" />--%>

			</td>
			<td width="4%">
			</td>
		</tr>
	</table>
  
	<script language=javascript type=text/javascript>
	<!--

	function AskDelete(propertyid)
	{
		if (window.confirm ("Are you sure you want to do this?"))
			window.location.href="DeleteProperty.aspx?PropertyID=" + propertyid + "&BackLink=MyAccount.aspx%3F<%# System.Web.HttpUtility.UrlEncode (Request.QueryString.ToString ()) %>"
	}

	// -->
	</script>

	<noscript>
		<img src="http://www.watchwise.net/cgi-watchwise/monitor.cgi?<%= CommonFunctions.GetSiteAddress ().ToLower () %>:myaccount" width="1" height="1">
	</noscript>

	<script language="javascript">
		document.write('<img src="http://www.watchwise.net/cgi-watchwise/monitorwise.cgi?URL=<%= CommonFunctions.GetSiteAddress ().ToLower () %>:myaccount&LINK=',escape(document.referrer),'" height=1 width=1>')
	</script>

</asp:Content>
