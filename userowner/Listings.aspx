<%@ Page Language="C#" MasterPageFile="~/MasterPageNoCss.master" AutoEventWireup="true" CodeFile="Listings.aspx.cs"
	Inherits="Listings" Title="Listings" %>
<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="Server">
    <div>
	<% if (BackLink.Visible) { %>
	<table cellspacing="0" cellpadding="0" width="250" align="center" bgcolor="#F5EDE3" border="2">
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
			<asp:Label ID="Label1" runat="server" Height="16px" Width="100%">
				Welcome
			</asp:Label>
		</strong>
	</div>
	<br />
	<table cellspacing="0" cellpadding="0" width="250" align="center" bgcolor="#F5EDE3" border="2">
		<tr>
			<td>
				<div align="center">
					<strong>
						<asp:Label ID="UserIDLabel" runat="server">
						</asp:Label>
					</strong>
				</div>
				<div align="center">
					<strong>
						<a href='<%= CommonFunctions.PrepareURL ("OwnerInformation.aspx?UserID=" + userid.ToString ()) %>'>
							Contact Details
						</a>
					</strong>
				</div>
				<div align="center">
					<strong>
						<a href='<%= CommonFunctions.PrepareURL ("AccountInformation.aspx?UserID=" + userid.ToString ()) %>'>
							Email / Password
						</a>
					</strong>
				</div>
				<% if (ifinvoices) { %>
				<div align="center">
					<strong>
						<a href='<%= CommonFunctions.PrepareURL ("ViewInvoices.aspx?UserID=" + userid.ToString ()) %>'>
							View Invoices
						</a>
					</strong>
				</div>
				<% } %>
			</td>
		</tr>
	</table>
	<br />
	<div align="center">
		<strong>
			<asp:Label ID="WelcomeLabel" runat="server" Height="16px" Width="100%">
				Welcome </asp:Label>-<br /> List Your vacation rental, boutique hotel or B&B.</strong> <br /><br />Follow the steps in "List A Property", where you will add the text and the photos for your property.<br /> We review all new listings for accuracy and verify that the owners are legitimate.<br /><br />All properties will default to the 7% commission payment schedule listed below in "Our Commission %". <br />If you do not want to pay the Commission Percentage, you will be given the opportunity to pay $50 annual fee, after your property has been approved.<br /> Original text will speed the listing of your property. <br />
		<br /><br /></strong>
	</div>
	<br />
	<div align="center">
		<asp:Button ID="NewProperty" runat="server" Text="List A Property" Width="150px" OnClick="NewProperty_Click" />
        <asp:Button ID="btnTour" runat="server" Text="List A Tour"  Width="150px" 
            onclick="btnTour_Click"/>
		<asp:Button ID="NewAuction" runat="server" Text="List An Auction" Width="150px" 
            OnClick="NewAuction_Click" Visible="False" />
            <br />
            <div align="center">
		<asp:Button ID="Agent" runat="server" OnClick="Agent_Click" Text="Our Commission %" Width="150px" />
        <asp:Button ID="facebook_btn" runat="server" OnClick="fbLogin" Text="Facebook Connect" Width="150px" />
         <asp:Button ID="Twitter_btn" runat="server" OnClick="Agent_Click" Text="Twitter Connect" Width="150px" />
	</div>
	</div>
	<% if (MainDataSet.Tables["FreeTrialProperties"].Rows.Count > 0) { %>
	<asp:Repeater ID="FreeTrialList" runat="server" DataMember="FreeTrialProperties" DataSource="<%# MainDataSet %>">
		<HeaderTemplate>
			<div style="text-align:center">
				<strong>
					<br />
					Your Unpaid Listings
					<br />
				</strong>
			</div>
			<table border="0" bordercolor="#ccccff" cellpadding="0" cellspacing="0" width="100%">
				<tr>
					<td bgcolor="#F5EDE3" width="15%">
						Number
					</td>
					<td bgcolor="#F5EDE3" width="100%">
						Name
					</td>
				</tr>
		</HeaderTemplate>
		<ItemTemplate>
			<tr>
				<td bgcolor="#F5EDE3" width="10%">
					<a href='<%# CommonFunctions.PrepareURL ("ViewProperty.aspx?UserID=" + userid.ToString () + "&PropertyID=" + DataBinder.Eval(Container.DataItem, "ID", "{0:d}"), "*User* Listings") %>'>
						<%# DataBinder.Eval(Container.DataItem, "ID", "{0}") %>
					</a>
				</td>
				<td bgcolor="#F5EDE3" width="100%">
					<a href='<%# CommonFunctions.PrepareURL ("ViewProperty.aspx?UserID=" + userid.ToString () + "&PropertyID=" + DataBinder.Eval(Container.DataItem, "ID", "{0:d}"), "*User* Listings") %>'>
						<%# DataBinder.Eval(Container.DataItem, "Name", "{0}") %>
					</a>
				</td>
				<%# PrintPublishButton ((System.Data.DataRowView)Container.DataItem) %>
				<td bordercolor="#ffffff">
					<input onclick="window.location.href=&quot;<%# CommonFunctions.PrepareURL ("MakePayment.aspx?UserID=" + userid.ToString () + "&PropertyID=" + DataBinder.Eval(Container.DataItem, "ID", "{0:d}") + "&InvoiceID=-1", "*User* Listings") %>&quot;;"
						style="width: 65px" type="button" value="Payment">
				</td>
				<td bordercolor="#ffffff">
					<input onclick="window.location.href=&quot;<%# CommonFunctions.PrepareURL ("EditProperty.aspx?UserID=" + userid.ToString () + "&PropertyID=" + DataBinder.Eval(Container.DataItem, "ID", "{0:d}"), "*User* Listings") %>&quot;;"
						style="width: 65px" type="button" value="Edit Text">
				</td>
				<td bordercolor="#ffffff">
					<input onclick="window.location.href=&quot;<%# CommonFunctions.PrepareURL ("PropertyPhotos.aspx?UserID=" + userid.ToString () + "&PropertyID=" + DataBinder.Eval(Container.DataItem, "ID", "{0:d}"), "*User* Listings") %>&quot;;"
						style="width: 75px" type="button" value="Edit Photos">
				</td>
				<td bordercolor="#ffffff">
					<input onclick="window.location.href=&quot;<%# CommonFunctions.PrepareURL ("PropertyCalendar.aspx?UserID=" + userid.ToString () + "&PropertyID=" + DataBinder.Eval(Container.DataItem, "ID", "{0:d}"), "*User* Listings") %>&quot;;"
						style="width: 75px" type="button" value="Calendar">
				</td>
				
<%--				<td bordercolor="#ffffff">
					<input onclick="window.location.href=&quot;<%# CommonFunctions.PrepareURL ("EditHomeExchanges.aspx?UserID=" + userid.ToString () + "&PropertyID=" + DataBinder.Eval(Container.DataItem, "ID", "{0:d}"), "*User* Listings") %>&quot;;"
						style="width: 115px" type="button" value="Home Exchanges">
				</td>
--%>				
				<td style="border-color:Red;">
					<input onclick="AskDeleteProperty('<%# DataBinder.Eval(Container.DataItem, "ID", "{0:d}") %>');" style="width:55px" type="button" value="Delete">
				</td>
				<td bordercolor="#ffffff">
					<input onclick="window.location.href=&quot;<%# CommonFunctions.PrepareURL ("ViewEmails.aspx?UserID=" + userid.ToString () + "&PropertyID=" + DataBinder.Eval(Container.DataItem, "ID", "{0:d}"), "*User* Listings") %>&quot;;"
						style="width:50px" type="button" value="Emails">
				</td>
			</tr>
		</ItemTemplate>
		<FooterTemplate>
			</table>
		</FooterTemplate>
	</asp:Repeater>
	<% } %>
	
		
	
	<% if (MainDataSet.Tables["AnnualFeeProperties"].Rows.Count > 0) { %>
	<asp:Repeater ID="AnnualFeeList" runat="server" DataMember="AnnualFeeProperties" DataSource="<%# MainDataSet %>">
		<HeaderTemplate>
			<div align="center">
				<strong>
					<br />
					Your Annual Fee Listings
					<br />
				</strong>
			</div>
			<table border="0" bordercolor="#ccccff" cellpadding="0" cellspacing="0" width="100%">
				<tr>
					<td bgcolor="#F5EDE3" width="15%">
						Number
					</td>
					<td bgcolor="#F5EDE3" width="100%">
						Name
					</td>
				</tr>
		</HeaderTemplate>
		<ItemTemplate>
			<tr>
				<td bgcolor="#F5EDE3" width="10%">
					<a href='<%# CommonFunctions.PrepareURL ("ViewProperty.aspx?UserID=" + userid.ToString () + "&PropertyID=" + DataBinder.Eval(Container.DataItem, "ID", "{0:d}"), "*User* Listings") %>'>
						<%# DataBinder.Eval(Container.DataItem, "ID", "{0}") %>
					</a>
				</td>
				<td bgcolor="#F5EDE3" width="100%">
					<a href='<%# CommonFunctions.PrepareURL ("ViewProperty.aspx?UserID=" + userid.ToString () + "&PropertyID=" + DataBinder.Eval(Container.DataItem, "ID", "{0:d}"), "*User* Listings") %>'>
						<%# DataBinder.Eval(Container.DataItem, "Name", "{0}") %>
					</a>
				</td>
				<%# PrintPublishButton ((System.Data.DataRowView)Container.DataItem) %>
				<%# PrintPropertyPaymentButton ((System.Data.DataRowView)Container.DataItem) %>
				<td bordercolor="#ffffff">
					<input onclick="window.location.href=&quot;<%# CommonFunctions.PrepareURL ("EditProperty.aspx?UserID=" + userid.ToString () + "&PropertyID=" + DataBinder.Eval(Container.DataItem, "ID", "{0:d}"), "*User* Listings") %>&quot;;"
						style="width: 65px" type="button" value="Edit Text">
				</td>
				<td bordercolor="#ffffff">
					<input onclick="window.location.href=&quot;<%# CommonFunctions.PrepareURL ("PropertyPhotos.aspx?UserID=" + userid.ToString () + "&PropertyID=" + DataBinder.Eval(Container.DataItem, "ID", "{0:d}"), "*User* Listings") %>&quot;;"
						style="width: 70px" type="button" value="Edit Photos">
				</td>
<%--				<td bordercolor="#ffffff">
					<input onclick="window.location.href=&quot;<%# CommonFunctions.PrepareURL ("EditHomeExchanges.aspx?UserID=" + userid.ToString () + "&PropertyID=" + DataBinder.Eval(Container.DataItem, "ID", "{0:d}"), "*User* Listings") %>&quot;;"
						style="width: 115px" type="button" value="Home Exchanges">
				</td>
--%>				<%# PrintCalendarButton ((System.Data.DataRowView)Container.DataItem) %>
				<td bordercolor="#ffffff">
					<button onclick="AskDeleteProperty ('<%# DataBinder.Eval(Container.DataItem, "ID", "{0:d}") %>');" value="Delete" style="width:55px">
						Delete
					</button>
				</td>
				<td bordercolor="#ffffff">
					<input onclick="window.location.href=&quot;<%# CommonFunctions.PrepareURL ("ViewEmails.aspx?UserID=" + userid.ToString () + "&PropertyID=" + DataBinder.Eval(Container.DataItem, "ID", "{0:d}"), "*User* Listings") %>&quot;;"
						style="width: 50px" type="button" value="Emails">
				</td>
			</tr>
		</ItemTemplate>
		<FooterTemplate>
			</table>
		</FooterTemplate>
	</asp:Repeater>
	<% } %>
	<% if (MainDataSet.Tables["Auctions"].Rows.Count > 0) { %>
	<asp:Repeater ID="AuctionList" runat="server" DataMember="Auctions" DataSource="<%# MainDataSet %>">
		<HeaderTemplate>
			<div align="center">
				<strong>
					<br />
					Your Auction Listings
					<br />
				</strong>
			</div>
			<table border="0" bordercolor="#ccccff" cellpadding="0" cellspacing="0" width="100%">
				<tr>
					<td bgcolor="#F5EDE3" width="15%">
						Number
					</td>
					<td bgcolor="#F5EDE3" width="100%">
						Name
					</td>
				</tr>
		</HeaderTemplate>
		<ItemTemplate>
			<tr>
				<td bgcolor="#F5EDE3" width="10%">
					<a href='<%# CommonFunctions.PrepareURL (DataBinder.Eval(Container.DataItem, "ID", "{0:d}") + "/default.aspx", "*User* Listings") %>'>
						<%# DataBinder.Eval(Container.DataItem, "ID", "{0}") %>
					</a>
				</td>
				<td bgcolor="#F5EDE3" width="100%">
					<a href='<%# CommonFunctions.PrepareURL (DataBinder.Eval(Container.DataItem, "ID", "{0:d}") + "/default.aspx", "*User* Listings") %>'>
						<%# DataBinder.Eval(Container.DataItem, "Name", "{0}") %>
					</a>
				</td>
				<%# PrintAuctionPaymentButton ((System.Data.DataRowView)Container.DataItem) %>
				<td bordercolor="#ffffff">
					<input onclick="window.location.href=&quot;<%# CommonFunctions.PrepareURL ("EditAuction.aspx?UserID=" + userid.ToString () + "&AuctionID=" + DataBinder.Eval(Container.DataItem, "ID", "{0:d}"), "*User* Listings") %>&quot;;"
						style="width: 100px" type="button" value='<%# (((System.Data.DataRowView)Container.DataItem).Row["AuctionEnd"] is DateTime) && ((DateTime)((System.Data.DataRowView)Container.DataItem).Row["AuctionEnd"] < DateTime.Now) && (((System.Data.DataRowView)Container.DataItem).Row["HighestBidderID"] is DBNull) ? "Relist Auction" : "Edit Text" %>'>
				</td>
				<td bordercolor="#ffffff">
					<input onclick="window.location.href=&quot;<%# CommonFunctions.PrepareURL ("PropertyPhotos.aspx?UserID=" + userid.ToString () + "&PropertyID=" + DataBinder.Eval(Container.DataItem, "PropertyID", "{0:d}"), "*User* Listings") %>&quot;;"
						style="width: 70px" type="button" value="Edit Photos">
				</td>
				<td bordercolor="#ffffff">
					<button onclick="AskDeleteAuction ('<%# DataBinder.Eval(Container.DataItem, "ID", "{0:d}") %>');" style="width:55px">
						Delete
					</button>
				</td>
				<td bordercolor="#ffffff">
					<input onclick="window.location.href=&quot;<%# CommonFunctions.PrepareURL ("ViewEmails.aspx?UserID=" + userid.ToString () + "&PropertyID=" + DataBinder.Eval(Container.DataItem, "PropertyID", "{0:d}"), "*User* Listings") %>&quot;;"
						style="width: 50px" type="button" value="Emails">
				</td>
			</tr>
		</ItemTemplate>
		<FooterTemplate>
			</table>
		</FooterTemplate>
	</asp:Repeater>
	<% } %>
<%--<br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
Vacations-Abroad.com--%>
<asp:Repeater ID="rptTours" runat="server" OnItemCommand="Action" DataMember="id">
		<HeaderTemplate>
			<div style="text-align:center">
				<strong>
					<br />
					Your Tours
					<br />
				</strong>
			</div>
			<table border="0" cellpadding="0" cellspacing="0" width="100%">
				<tr>
					<td bgcolor="#F5EDE3" width="15%">
						Number
					</td>
					<td bgcolor="#F5EDE3" width="20%">
						Name
					</td>
					
				</tr>
		</HeaderTemplate>
		<ItemTemplate>
			<tr>
				<td bgcolor="#F5EDE3" width="15%">
					<%--<a href='<%# CommonFunctions.PrepareURL ("ViewProperty.aspx?UserID=" + userid.ToString () + "&PropertyID=" + DataBinder.Eval(Container.DataItem, "ID", "{0:d}"), "*User* Listings") %>'>--%>
						<%# DataBinder.Eval(Container.DataItem, "ID", "{0}") %>
					<%--</a>--%>
				</td>
				<td bgcolor="#F5EDE3" width="20%">
					<%--<a href='<%# CommonFunctions.PrepareURL ("ViewProperty.aspx?UserID=" + userid.ToString () + "&PropertyID=" + DataBinder.Eval(Container.DataItem, "ID", "{0:d}"), "*User* Listings") %>'>--%>
						<%# DataBinder.Eval(Container.DataItem, "Name", "{0}") %>
					<%--</a>--%>
				</td>				
				
				<td>
					<input onclick="window.location.href=&quot;<%# CommonFunctions.PrepareURL ("EditTour.aspx?tourID=" + DataBinder.Eval(Container.DataItem, "ID", "{0:d}"), "*User* Listings") %>&quot;;"
						style="width: 120px" type="button" value="Edit Text">
				</td>
				<td>
					<input onclick="window.location.href=&quot;<%# CommonFunctions.PrepareURL ("tourPhotos.aspx?tourID=" + DataBinder.Eval(Container.DataItem, "ID", "{0:d}"), "*User* Listings") %>&quot;;"
						style="width: 120px" type="button" value="Edit Photos">
				</td>
				<td bordercolor="#ffffff">
					<input onclick="window.location.href=&quot;<%# CommonFunctions.PrepareURL ("tourLinkEdit.aspx?tourID=" + DataBinder.Eval(Container.DataItem, "ID", "{0:d}"), "*User* Listings") %>&quot;;"
						style="width: 120px" type="button" value="Edit Link Text">
				</td>
							
				<td style="border-color:Red;">
                    <input onclick="window.location.href=&quot;<%# CommonFunctions.PrepareURL ("Listings2.aspx?action=rem&tourID=" + DataBinder.Eval(Container.DataItem, "ID", "{0:d}"), "*User* Listings") %>&quot;;"
						style="width: 120px" type="button" value="Delete">
				</td>
				
			</tr>
		</ItemTemplate>
		<FooterTemplate>
			</table>
		</FooterTemplate>
	</asp:Repeater>

	<script language="javascript" type="text/javascript">
	<!--
	    function AskDeleteTour(tourid) {
	        if (window.confirm("Are you sure you want to do this?"))
	            window.location.href = "Listings2.aspx?action=rem&tourID=" + tourid;
	    }
	function AskDeleteProperty (propertyid)
	{
		if (window.confirm ("Are you sure you want to do this?"))
			window.location.href="DeleteProperty.aspx?PropertyID=" + propertyid + "&BackLink=<%= System.Web.HttpUtility.UrlEncode (Request.Url.ToString ()) %>";
	}

	function AskDeleteAuction (auctionid)
	{
		if (window.confirm ("Are you sure you want to do this?"))
			window.location.href="DeleteAuction.aspx?AuctionID=" + auctionid + "&BackLink=<%= System.Web.HttpUtility.UrlEncode (Request.Url.ToString ()) %>";
	}

	// -->
	</script>

        <asp:Label ID="lblError" runat="server"></asp:Label>
</div>
</asp:Content>
