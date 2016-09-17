<%@ Control Language="C#" AutoEventWireup="true" CodeFile="paymentreceipt.ascx.cs" Inherits="paymentreceipt" %>
<br>
			<center>
				<b>Do Direct Payment</b>
				<br>
				<br>
				<b>Thank you for your payment!</b><br>
				<br>
				<table width="400">
					<tr>
						<td>Transaction ID:</td>
						<td><%=Request.QueryString.Get("TRANSACTIONID")%></td>
					</tr>
					<tr>
						<td>Amount:</td>
						<td>USD <%=Request.QueryString.Get("AMT")%></td>
					</tr>
					<tr>
						<td>AVS:</td>
						<td><%=Request.QueryString.Get("AVSCODE")%></td>
					</tr>
					<tr>
						<td>CVV2:</td>
						<td><%=Request.QueryString.Get("CVV2MATCH")%></td>
					</tr>
				</table>
			</center>