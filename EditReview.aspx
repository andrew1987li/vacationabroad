<%@ Page Language="C#" MasterPageFile="~/MasterPageNoCss.master" AutoEventWireup="true" CodeFile="EditReview.aspx.cs" Inherits="EditReview" Title="Add / Edit Review" %>
<asp:Content ID="Content" ContentPlaceHolderID="Content" Runat="Server">
	<div align="center">
		<strong><font color="#cc9933" size="4">Add Your Review</font></strong>
	</div>
	<br /><br />
	<div align="center">
		<font size="3"><strong>
			<%# MainDataSet.Tables["Auctions"].Rows[0]["Title"] %>
		</strong></font>
	</div>
	<table border="0" width="100%">
		<tr>
			<td height="15">
			</td>
			<td height="15" align="center">
				<u><strong><font size="3">Date Ending</font></strong></u>
			</td>
			<td height="15" align="center">
				<u><strong><font size="3">Ending Price</font></strong></u>
			</td>
			<td height="15" align="center">
				<u><strong><font size="3">User Id</font></strong></u>
			</td>
			<td height="15" align="center">
				<u><strong><font size="3">Transaction ID</font></strong></u>
			</td>
		</tr>
		<tr>
			<td height="15" align="center">
				<%# CommonFunctions.ShowAuctionPhoto (MainDataSet.Tables["Auctions"].Rows[0], "Edit Review")%>
			</td>
			<td height="15" align="center">
				<%# MainDataSet.Tables["Auctions"].Rows[0]["AuctionEnd"] %>
			</td>
			<td height="15" align="center">
				<%# MainDataSet.Tables["Auctions"].Rows[0]["BidAmount"] %>
			</td>
			<td height="19" align="center">
				<a href='<%# CommonFunctions.PrepareURL ("ViewUser.aspx?UserID=" + MainDataSet.Tables["Auctions"].Rows[0]["UserID"].ToString (), "Edit Review") %>'>
					<%# MainDataSet.Tables["Auctions"].Rows[0]["Username"] %>
				</a>
			</td>
			<td height="19" align="center">
				<a href='<%# CommonFunctions.PrepareURL (MainDataSet.Tables["Auctions"].Rows[0]["ID"].ToString () + "/default.aspx", "Edit Review") %>'>
					<%# MainDataSet.Tables["Auctions"].Rows[0]["ID"] %>
				</a>
			</td>
		</tr>
	</table>
	<table border="0" width="100%">
		<tr bgcolor="#cc9933">
			<td align="center">
				<strong><font color="white">Title</font></strong>
			</td>
			<td width="10%" align="center">
				<font size="3" color="white">*</font>
			</td>
			<td width="10%" align="center">
				<font size="3" color="white">**</font>
			</td>
			<td width="10%" align="center">
				<font size="3" color="white">***</font>
			</td>
			<td width="10%" align="center">
				<font size="3" color="white">****</font>
			</td>
		</tr>
		<tr bgcolor="#ffffff">
			<td align="center">
				Auction Transaction
			</td>
			<td align="center">
				<asp:RadioButton ID="AuctionMark1" runat="server" GroupName="Auction" />
			</td>
			<td align="center">
				<asp:RadioButton ID="AuctionMark2" runat="server" GroupName="Auction" />
			</td>
			<td align="center">
				<asp:RadioButton ID="AuctionMark3" runat="server" GroupName="Auction" />
			</td>
			<td align="center">
				<asp:RadioButton ID="AuctionMark4" runat="server" GroupName="Auction" />
			</td>
		</tr>
		<tr bgcolor="#ffffff">
			<td align="center">
				Accurately Represented
			</td>
			<td align="center">
				<asp:RadioButton ID="AccuratelyRepresentedMark1" runat="server" GroupName="AccuratelyRepresented" />
			</td>
			<td align="center">
				<asp:RadioButton ID="AccuratelyRepresentedMark2" runat="server" GroupName="AccuratelyRepresented" />
			</td>
			<td align="center">
				<asp:RadioButton ID="AccuratelyRepresentedMark3" runat="server" GroupName="AccuratelyRepresented" />
			</td>
			<td align="center">
				<asp:RadioButton ID="AccuratelyRepresentedMark4" runat="server" GroupName="AccuratelyRepresented" />
			</td>
		</tr>
		<tr bgcolor="#ffffff">
			<td align="center">
				Customer Service
			</td>
			<td align="center">
				<asp:RadioButton ID="CustomerServiceMark1" runat="server" GroupName="CustomerService" />
			</td>
			<td align="center">
				<asp:RadioButton ID="CustomerServiceMark2" runat="server" GroupName="CustomerService" />
			</td>
			<td align="center">
				<asp:RadioButton ID="CustomerServiceMark3" runat="server" GroupName="CustomerService" />
			</td>
			<td align="center">
				<asp:RadioButton ID="CustomerServiceMark4" runat="server" GroupName="CustomerService" />
			</td>
		</tr>
		<tr bgcolor="#ffffff">
			<td align="center">
				Cleanliness
			</td>
			<td align="center">
				<asp:RadioButton ID="CleanlinessMark1" runat="server" GroupName="Cleanliness" />
			</td>
			<td align="center">
				<asp:RadioButton ID="CleanlinessMark2" runat="server" GroupName="Cleanliness" />
			</td>
			<td align="center">
				<asp:RadioButton ID="CleanlinessMark3" runat="server" GroupName="Cleanliness" />
			</td>
			<td align="center">
				<asp:RadioButton ID="CleanlinessMark4" runat="server" GroupName="Cleanliness" />
			</td>
		</tr>
		<tr bgcolor="#ffffff">
			<td align="center">
				Good Value
			</td>
			<td align="center">
				<asp:RadioButton ID="GoodValueMark1" runat="server" GroupName="GoodValue" />
			</td>
			<td align="center">
				<asp:RadioButton ID="GoodValueMark2" runat="server" GroupName="GoodValue" />
			</td>
			<td align="center">
				<asp:RadioButton ID="GoodValueMark3" runat="server" GroupName="GoodValue" />
			</td>
			<td align="center">
				<asp:RadioButton ID="GoodValueMark4" runat="server" GroupName="GoodValue" />
			</td>
		</tr>
	</table>
	<table border="0" width="100%" bgcolor="#cfdfef">
		<tr>
			<td>
				<strong>Add your comments</strong><br />
				<asp:TextBox ID="Comments" runat="server" Height="80px" TextMode="MultiLine" Width="99%" />
			</td>
		</tr>
	</table>
	<asp:Button ID="SaveButton" runat="server" OnClick="SaveButton_Click" Text="Save" />
	<asp:Button ID="CancelButton" runat="server" OnClick="CancelButton_Click" Text="Cancel" />
</asp:Content>

