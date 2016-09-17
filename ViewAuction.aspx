<%@ Page Language="C#" MasterPageFile="~/MasterPageNoCss.master" AutoEventWireup="true" CodeFile="ViewAuction.aspx.cs" Inherits="ViewAuction" Title="<%# GetTitle () %>" %>
<asp:Content ID="Content" ContentPlaceHolderID="Content" Runat="Server">
	<table border="0" width="100%">
		<tr>
			<td width="4%">
			</td>
			<td width="92%">
				<div align="center">
					<h3><font face="Book Antiqua">
						<%# MainDataSet.Tables["Properties"].Rows[0]["Name"] %>
					</font></h3>
				</div>
				<br />
				<% if (BackLink.Visible) { %>
				<div align="center">
					<table bgcolor="#e4e4af" cellspacing="0" cellpadding="0" width="80%" align="center" border="0">
						<tr>
							<td>
								<div align="center">
									<strong>
										<asp:HyperLink ID="BackLink" runat="server" NavigateUrl="default.aspx">
											Return to start page
										</asp:HyperLink>
									</strong>
								</div>
							</td>
						</tr>
					</table>
				</div>
				<br />
				<% } %>
				<% if (Request.QueryString.ToString ().IndexOf ("IfPopup") != -1) { %>
				<div align="center">
					<table bgcolor="#e4e4af" cellspacing="0" cellpadding="0" width="80%" align="center" border="0">
						<tr>
							<td>
								<div align="center">
									<strong>
										<a href="#" onclick="window.close ();">Close</a>
									</strong>
								</div>
							</td>
						</tr>
					</table>
				</div>
				<br />
				<% } %>
				<table align="center" border="0" width="100%">
					<tr>
						<td align="left">
							<a href='<%# CommonFunctions.PrepareURL ("ViewUser.aspx?UserID=" + MainDataSet.Tables["Properties"].Rows[0]["UserID"].ToString (), "Auction Item") %>'>
								Account <%# MainDataSet.Tables["Properties"].Rows[0]["Username"] %>
							</a>
						</td>
						<td align="right">
							Auction: <%# MainDataSet.Tables["Properties"].Rows[0]["AuctionID"] %>
							<%# (MainDataSet.Tables["Properties"].Rows[0]["Registered"] is string) && (((string)MainDataSet.Tables["Properties"].Rows[0]["Registered"]).Length > 0) ? "<br />A member of<br />" + (string)MainDataSet.Tables["Properties"].Rows[0]["Registered"] : "" %>
						</td>
					</tr>
				</table>
				<div align="center">
					<img src='<%# ConfigurationManager.AppSettings["ImagesVirtualLocation"] + ((MainDataSet.Tables["FirstPhoto"].Rows.Count > 0) ? MainDataSet.Tables["FirstPhoto"].Rows[0]["FileName"].ToString () : "") %>'
						width='<%# (MainDataSet.Tables["FirstPhoto"].Rows.Count > 0) ? MainDataSet.Tables["FirstPhoto"].Rows[0]["Width"].ToString () : "0" %>'
						height='<%# (MainDataSet.Tables["FirstPhoto"].Rows.Count > 0) ? MainDataSet.Tables["FirstPhoto"].Rows[0]["Height"].ToString () : "0" %>'
						alt='Vacation and Holiday Rentals' border='<%# (MainDataSet.Tables["FirstPhoto"].Rows.Count > 0) ? "1" : "0" %>'>
				</div>
				<div align="center">
					Auction Began <%# MainDataSet.Tables["Properties"].Rows[0]["AuctionStart"] %><br />
					Time Remaining for Auction:
					<% if (!ifended) { %>
					<%# ((DateTime)MainDataSet.Tables["Properties"].Rows[0]["AuctionEnd"] - DateTime.Now).Minutes %> Mins,
					<%# ((DateTime)MainDataSet.Tables["Properties"].Rows[0]["AuctionEnd"] - DateTime.Now).Hours%> Hours,
					<%# ((DateTime)MainDataSet.Tables["Properties"].Rows[0]["AuctionEnd"] - DateTime.Now).Days % 7%> Days,
					<%# ((DateTime)MainDataSet.Tables["Properties"].Rows[0]["AuctionEnd"] - DateTime.Now).Days / 7%> Weeks<br />
					<% } else { %>
					Auction Ended<br />
					<% } %>
					Length of Auction: <%# (int)((DateTime)MainDataSet.Tables["Properties"].Rows[0]["AuctionEnd"] - (DateTime)MainDataSet.Tables["Properties"].Rows[0]["AuctionStart"]).TotalDays %> Days<br />
					Item Location: <%# MainDataSet.Tables["Properties"].Rows[0]["Country"] %>,
					<%# MainDataSet.Tables["Properties"].Rows[0]["StateProvince"] %>,
					<%# MainDataSet.Tables["Properties"].Rows[0]["City"] %><br />
					Length of Rental: <%# MainDataSet.Tables["Properties"].Rows[0]["RentalLength"] %><br />
					Minimum Bid: <%# MainDataSet.Tables["Properties"].Rows[0]["MinimumBid"] %><br />
					<%# ifended ? "Winning Bid" : "Current Bid" %>:
					<%# (MainDataSet.Tables["Properties"].Rows[0]["BidAmount"] is DBNull) ? "None" : MainDataSet.Tables["Properties"].Rows[0]["BidAmount"].ToString () %><br />
					Dates Available
					<%# ((MainDataSet.Tables["Properties"].Rows[0]["IfBasedAvailability"] is bool) &&
						(bool)MainDataSet.Tables["Properties"].Rows[0]["IfBasedAvailability"]) ? " - Based on Availability" :
						"From " + ((MainDataSet.Tables["Properties"].Rows[0]["RentalStart"] is DateTime) ?
						((DateTime)MainDataSet.Tables["Properties"].Rows[0]["RentalStart"]).ToShortDateString () : "") +
						" to " + ((MainDataSet.Tables["Properties"].Rows[0]["RentalEnd"] is DateTime) ?
						((DateTime)MainDataSet.Tables["Properties"].Rows[0]["RentalEnd"]).ToShortDateString () : "") %>
				</div>
				<div align="center">
					<table bgcolor="#dbe7f2" cellspacing="0" cellpadding="0" width="600" align="center" border="0">
						<tr>
							<td align="center">
								<strong>
									<a href='#Photos'><font face="Book Antiqua" size=2>View More Photos</font></a>
								</strong>
							</td>
							<% if (!ifended && ifpaid) { %>
							<td align="center">
								<strong>
									<a href='<%# CommonFunctions.PrepareURL ("WatchListAdd.aspx?AuctionID=" + MainDataSet.Tables["Properties"].Rows[0]["AuctionID"].ToString (), "Auction Item") %>'>
										<font face="Book Antiqua" size=2>Add To Watch List</font>
									</a>
								</strong>
							</td>
							<td align="center">
								<strong>
									<a href='<%# CommonFunctions.PrepareURL ("BidNow.aspx?AuctionID=" + MainDataSet.Tables["Properties"].Rows[0]["AuctionID"].ToString (), "Auction Item") %>'>
										<font face="Book Antiqua" size=2>Bid Now</font>
									</a>
								</strong>
							</td>
							<td align="center">
								<strong>
									<a href='<%# CommonFunctions.PrepareURL ("AuctionSendEmail.aspx?AuctionID=" + MainDataSet.Tables["Properties"].Rows[0]["AuctionID"].ToString (), "Auction Item") %>'>
										<font face="Book Antiqua" size=2>Inquire About This Auction</font>
									</a>
								</strong>
							</td>
							<% } %>
						</tr>
					</table>
				</div>
				<div align="center">
					<p align="left">
						<font face="Book Antiqua" size="2">
							<%# MainDataSet.Tables["Properties"].Rows[0]["Name"] %> -
							<%# MainDataSet.Tables["Properties"].Rows[0]["City"] %>,
							<%# MainDataSet.Tables["Properties"].Rows[0]["StateProvince"] %>,
							<%# MainDataSet.Tables["Properties"].Rows[0]["Type"] %>
							<%# MainDataSet.Tables["Properties"].Rows[0]["NumBedrooms"] %> Bedrooms,
							<%# MainDataSet.Tables["Properties"].Rows[0]["NumBaths"] %> Baths,
							<%# MainDataSet.Tables["Properties"].Rows[0]["NumSleeps"] %> Sleeps, Minimum nightly rental -
							<%# MainDataSet.Tables["Properties"].Rows[0]["MinimumNightlyRental"] %>.<br />
							Includes:
							<%# MainDataSet.Tables["Properties"].Rows[0]["NumTVs"] %> TVs,
							<%# MainDataSet.Tables["Properties"].Rows[0]["NumVCRs"] %> VCRs,
							<%# MainDataSet.Tables["Properties"].Rows[0]["NumCDPlayers"] %> CD Players<%# (MainDataSet.Tables["Amenities"].Rows.Count > 0) ? "," : "." %>
							<asp:Repeater ID="Repeater2" runat="server" DataMember="Amenities" DataSource="<%# MainDataSet %>">
								<HeaderTemplate>
								</HeaderTemplate>
								<ItemTemplate>
									<%# DataBinder.Eval(Container.DataItem, "Amenity", "{0}") %><%# !CommonFunctions.IfLastRow ((System.Data.DataRowView)Container.DataItem) ? "," : "." %>
								</ItemTemplate>
								<FooterTemplate>
								</FooterTemplate>
							</asp:Repeater>
							<br />
							<%# ((int)MainDataSet.Tables["Properties"].Rows[0]["IfAnnualFee"] == 1) && (bool)MainDataSet.Tables["Properties"].Rows[0]["IfShowAddress"] ? "Address: " + MainDataSet.Tables["Properties"].Rows[0]["Address"] : "" %>
						</font>
					</p>
				</div>
				<% if (MainDataSet.Tables["AuctionQuestions"].Rows.Count > 0) { %>
				<asp:Repeater ID="Repeater3" runat="server" DataMember="AuctionQuestions" DataSource="<%# MainDataSet %>">
					<HeaderTemplate>
						<table cellspacing="0" cellpadding="1" width="75%" align="center" bgcolor="#dbe7f2" border="0">
							<tr>
								<td colspan="100" align="center">
									<font size="2"><strong>Questions regarding this auction item</strong></font>
								</td>
							</tr>
					</HeaderTemplate>
					<ItemTemplate>
							<tr>
								<td width="15%" align="left">
									<font size="2"><strong>
									<%# (((System.Data.DataRowView)Container.DataItem).Row["IfQuestion"] is bool) && (bool)((System.Data.DataRowView)Container.DataItem).Row["IfQuestion"] ? "Question:" : "Answer:" %>
									</strong></font>
								</td>
								<td align="left">
									<font size="2"><%# DataBinder.Eval(Container.DataItem, "Text", "{0}") %></font>
								</td>
							</tr>
					</ItemTemplate>
					<FooterTemplate>
						</table>
					</FooterTemplate>
				</asp:Repeater>
				<br />
				<% } %>
				<div align="center">
					<table bgcolor="#e4e4af" cellspacing="0" cellpadding="0" width="600" align="center" border="0">
						<tr>
							<td>
								<div align="center">
									<font face="Book Antiqua" size=3><strong>
										Vacation Rental Description
									</strong></font>
								</div>
							</td>
						</tr>
					</table>
				</div>
				<br />
				<div align="left">
					<p>
						<font face="Book Antiqua" size="2">
							<%# MainDataSet.Tables["Properties"].Rows[0]["Description"] %><br />
							<%# MainDataSet.Tables["Properties"].Rows[0]["Amenities"] %>
						</font>
					</p>
				</div>
				Payment methods accepted:
				<% if (PaymentMethodsPresent ()) { %>
				<asp:Repeater ID="Repeater4" runat="server" DataMember="PaymentMethods" DataSource="<%# MainDataSet %>">
					<HeaderTemplate>
					</HeaderTemplate>
					<ItemTemplate>
						<%# DataBinder.Eval(Container.DataItem, "PaymentMethod", "{0}") %><%# !CommonFunctions.IfLastRow ((System.Data.DataRowView)Container.DataItem) ? "," : "." %>
					</ItemTemplate>
					<FooterTemplate>
					</FooterTemplate>
				</asp:Repeater>
				<% } else { %>
				Check with Owner 
				<% } %>
				<br />
				<% if (LodgingTaxPresent ()) { %>
				Lodging Tax: <%# MainDataSet.Tables["Properties"].Rows[0]["LodgingTax"] %><br />
				<%# (MainDataSet.Tables["Properties"].Rows[0]["TaxIncluded"] is bool) && (bool)MainDataSet.Tables["Properties"].Rows[0]["TaxIncluded"] ? "Tax included in price" : "Tax not included in price"%><br />
				<% } %>
				<div align="left">
					<p>
						<font face="Book Antiqua" size="2">
							Payment Terms: <%# MainDataSet.Tables["Properties"].Rows[0]["PaymentTerms"] %><br />
						</font>
					</p>
				</div>
				<% if (!ifended && ifpaid) { %>
				<div align="center">
					<strong>Bid Now!</strong><br />
					Minimum Bid: <%# MainDataSet.Tables["Properties"].Rows[0]["MinimumBid"] %> USD<br />
					Current Bid: <%# MainDataSet.Tables["Properties"].Rows[0]["BidAmount"] %> USD<br />
					Your Bid: <asp:TextBox ID="BidAmount" runat="server" MaxLength="20" Width="67px" /> USD<br />
					<asp:RequiredFieldValidator ID="BidRequired" runat="server" ControlToValidate="BidAmount" Display="Dynamic"
						ErrorMessage="Please enter bid amount" />
					<asp:RegularExpressionValidator ID="BidInvalid" runat="server" ControlToValidate="BidAmount" Display="Dynamic"
						ErrorMessage="Whole Numbers, no decimals, no commas" ValidationExpression="^\d{1,20}$" /><br />
					<asp:Label ID="BidNotAccepted" runat="server" ForeColor="Red"
						Text="Your bid was not accepted because it is less than the Current Bid or Minimum Bid." Visible="False" /><br />
					<asp:Button ID="BidNowButton" runat="server" OnClick="BidNowButton_Click" Text="Bid Now" />
				</div>
				<% } %>
				<a name="#Photos"></a>
				<div align="center">
					<asp:Repeater ID="Repeater1" runat="server" DataMember="Photos" DataSource="<%# MainDataSet %>">
						<HeaderTemplate>
						</HeaderTemplate>
						<ItemTemplate>
							<img src='<%# ConfigurationManager.AppSettings["ImagesVirtualLocation"] + (string)DataBinder.Eval(Container.DataItem, "FileName", "{0:s}") %>'
								width='<%# DataBinder.Eval(Container.DataItem, "Width", "{0:d}") %>'
								height='<%# DataBinder.Eval(Container.DataItem, "Height", "{0:d}") %>'
								alt='Vacation and Holiday Rentals' border="1">
							<%# PicturesTooWide ((System.Data.DataRowView)Container.DataItem, 675) ? "<br />" : "" %>
						</ItemTemplate>
						<FooterTemplate>
						</FooterTemplate>
					</asp:Repeater>
				</div>
				<% if (AuthenticationManager.IfAuthenticated && AuthenticationManager.IfAdmin) { %>
				<div align="center">
					<asp:Button ID="DeleteButton" runat="server" Text="Delete" Width="112px" OnClick="DeleteButton_Click" /><br />
					<asp:Button ID="SendEmailButton" runat="server" Text="Send Email" Width="112px" OnClick="SendEmailButton_Click" /><br />
				</div>
				<% } %>
			</td>
			<td width="4%">
			</td>
		</tr>
	</table>
	
	<noscript>
		<img src="http://www.watchwise.net/cgi-watchwise/monitor.cgi?<%= CommonFunctions.GetSiteAddress ().ToLower () %>:all" width="1" height="1">
	</noscript>

	<script language="javascript">
		document.write('<img src="http://www.watchwise.net/cgi-watchwise/monitorwise.cgi?URL=<%= CommonFunctions.GetSiteAddress ().ToLower () %>:all&LINK=',escape(document.referrer),'" height=1 width=1>')
	</script>

</asp:Content>

