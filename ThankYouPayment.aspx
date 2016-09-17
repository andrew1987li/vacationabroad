<%@ Page Language="C#" MasterPageFile="~/MasterPageNoCss.master" AutoEventWireup="true" CodeFile="ThankYouPayment.aspx.cs" Inherits="ThankYouPayment" Title="Thank you" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
	<div align="center">
		<blockquote>
			<p>
				<font color="#cc0000" size="5">Thank You For Your payment!</font>
			</p>
		</blockquote>
		<blockquote>
			<p>
				<font size="3">Your payment is being processed. Your listing will be converted from the free trial
					to annual fee as soon as processing is completed. </font>
			</p>
		</blockquote>
	</div>
	<table width="250" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#e4e4af" id="Table3">
		<tr>
			<td>
				<div align="center">
					<strong>
						<a href='<%= CommonFunctions.PrepareURL ("MyAccount.aspx") %>'>
							Return to Your Account to review the property you just listed and to make any modifications
						</a>
					</strong>
				</div>
			</td>
		</tr>
	</table>
</asp:Content>
