<%@ page language="C#" masterpagefile="~/MasterPageNoCss.master" autoeventwireup="true" CodeFile="~/InvoiceRegister.aspx.cs" inherits="InvoiceRegister" title="Invoice Register" enableEventValidation="false" %>
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
							&nbsp;&nbsp;
							<a href='<%= CommonFunctions.PrepareURL ("InvoiceRegister.aspx?Year=2007", "Administration") %>'>
								Invoice Register 2007
							</a>
						
						</td>
					</tr>
				</table>
			</td>
        </tr>
    </table>
    <br />
	<asp:SqlDataSource ID="SqlDataSource"
		SelectCommand="SELECT 0 AS IfAuction, ID, InvoiceDate, InvoiceAmount, PropertyID, PaymentDate, PaymentAmount, RenewalDate, InvoiceAmount - ISNULL (PaymentAmount, 0) AS Balance FROM Invoices WHERE (Year(InvoiceDate) = @Year) AND (InvoiceAmount > 0) UNION SELECT 1 AS IfAuction, Transactions.ID, InvoiceDate, InvoiceAmount, PropertyID, PaymentDate, PaymentAmount, NULL, InvoiceAmount - ISNULL (PaymentAmount, 0) AS Balance FROM Transactions INNER JOIN Auctions ON Transactions.AuctionID = Auctions.ID WHERE (Year(InvoiceDate) = @Year) AND (InvoiceAmount > 0)"
		UpdateCommand="IF @IfAuction = 0 UPDATE Invoices SET InvoiceDate = @InvoiceDate, InvoiceAmount = CONVERT(money, @InvoiceAmount), PaymentDate = @PaymentDate, PaymentAmount = CONVERT(money, @PaymentAmount), RenewalDate = @RenewalDate WHERE ID = @ID ELSE UPDATE Transactions SET InvoiceDate = @InvoiceDate, InvoiceAmount = CONVERT(money, @InvoiceAmount), PaymentDate = @PaymentDate, PaymentAmount = CONVERT(money, @PaymentAmount) WHERE ID = @ID"
		runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>">
		<SelectParameters>
			<asp:QueryStringParameter DefaultValue="0" Name="Year" QueryStringField="Year" Type="Int32" />
		</SelectParameters>
		<UpdateParameters>
			<asp:Parameter Name="IfAuction"  />
			<asp:Parameter Name="InvoiceDate" />
			<asp:Parameter Name="InvoiceAmount" />
			<asp:Parameter Name="PaymentDate" />
			<asp:Parameter Name="PaymentAmount" />
			<asp:Parameter Name="RenewalDate" />
			<asp:Parameter Name="ID" />
		</UpdateParameters>
	</asp:SqlDataSource>
	<asp:GridView ID="InvoicesGrid" runat="server" AllowSorting="True" 
        AutoGenerateColumns="False" DataKeyNames="ID,IfAuction"
			GridLines="None" HorizontalAlign="Right" Width="100%" CellPadding="0" 
        BorderColor="White" BorderWidth="3px" DataSourceID="SqlDataSource" 
        onrowdatabound="InvoicesGrid_RowDataBound">
		<Columns>
			<asp:BoundField ConvertEmptyStringToNull="False" DataField="ID" DataFormatString="{0}" HeaderText="Inv #"
					ReadOnly="True" SortExpression="ID" HtmlEncode="False" ApplyFormatInEditMode="True">
				<ItemStyle HorizontalAlign="Right" />
			</asp:BoundField>
			<asp:BoundField DataField="InvoiceDate" DataFormatString="{0:d}" HeaderText="Inv Date"
					SortExpression="InvoiceDate" HtmlEncode="False" ApplyFormatInEditMode="True">
				<ControlStyle Width="65px" />
				<ItemStyle HorizontalAlign="Right" />
			</asp:BoundField>
			<asp:BoundField DataField="InvoiceAmount" DataFormatString="{0:c}" HeaderText="Inv Amt"
					SortExpression="InvoiceAmount" HtmlEncode="False" ApplyFormatInEditMode="True">
				<ControlStyle Width="60px" />
				<ItemStyle HorizontalAlign="Right" />
			</asp:BoundField>
			<asp:TemplateField HeaderText="Property #" SortExpression="PropertyID">
                <ItemTemplate>
                    <asp:HyperLink ID="hlkProperty" runat="server" 
                        Text='<%# Eval("PropertyID", "{0}") %>'></asp:HyperLink>
                </ItemTemplate>
                <HeaderStyle Font-Bold="True" />
                <ItemStyle BackColor="#E4E4AF" HorizontalAlign="Right" />
            </asp:TemplateField>
			<asp:BoundField DataField="PaymentDate" DataFormatString="{0:d}" HeaderText="Pymt Date"
					SortExpression="PaymentDate" HtmlEncode="False" ApplyFormatInEditMode="True">
				<ControlStyle Width="65px" />
				<ItemStyle HorizontalAlign="Right" />
			</asp:BoundField>
			<asp:BoundField DataField="PaymentAmount" DataFormatString="{0:c}" HeaderText="Pymt Amt"
					SortExpression="PaymentAmount" HtmlEncode="False" ApplyFormatInEditMode="True">
				<ControlStyle Width="60px" />
				<ItemStyle HorizontalAlign="Right" />
			</asp:BoundField>
			<asp:BoundField DataField="RenewalDate" DataFormatString="{0:d}" HeaderText="Renewal Date"
					SortExpression="RenewalDate" HtmlEncode="False" ApplyFormatInEditMode="True">
				<ControlStyle Width="65px" />
				<ItemStyle HorizontalAlign="Right" />
			</asp:BoundField>
			<asp:BoundField DataField="Balance" DataFormatString="{0:c}" HeaderText="Balance" ReadOnly="True"
					SortExpression="Balance" HtmlEncode="False" ApplyFormatInEditMode="True">
				<ItemStyle HorizontalAlign="Right" />
			</asp:BoundField>
			<asp:CommandField ShowEditButton="True">
				<ItemStyle HorizontalAlign="Right" />
			</asp:CommandField>
		</Columns>
	</asp:GridView>
</asp:Content>

