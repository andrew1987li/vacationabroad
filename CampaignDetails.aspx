<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageNoCss.master" AutoEventWireup="true" CodeFile="CampaignDetails.aspx.cs" Inherits="CampaignDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<center>
 <table bgcolor="#e4e4af" cellspacing="0" cellpadding="0" width="300" align="center"
        border="2">
        <tr>
            <td>
                <div align="center">
                    <strong>
                        <asp:HyperLink ID="lnkReturn" runat="server" NavigateUrl="EmailCampaign.aspx">
									    Return to Email Overview
                        </asp:HyperLink>
                    </strong>
                </div>
            </td>
        </tr>
    </table><br />
    <asp:GridView ID="grdDetails" runat="server" Width="90%" 
        AutoGenerateColumns="False"  
        ondatabound="grdDetails_DataBound" DataKeyNames="id">
        <Columns>
            <asp:TemplateField HeaderText="#">
                <ItemTemplate>
                   <%# Container.DataItemIndex + 1 %>.
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="name" HeaderText="Name" />
            <asp:BoundField DataField="email" HeaderText="Email" />
            <asp:BoundField DataField="phone" HeaderText="Phone" />
            <asp:BoundField DataField="phone2" HeaderText="Phone 2" />
            <asp:BoundField DataField="arrivalDate" HeaderText="Arrival Date" />
            <asp:TemplateField HeaderText="Property #">
                <ItemTemplate>
                    <asp:HyperLink ID="hlkPropNum" runat="server" 
                        Text='<%# Bind("propertyNumber") %>'></asp:HyperLink>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("propertyNumber") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="transDate" HeaderText="Transaction Date" />
            <asp:TemplateField HeaderText="Winner">
                <ItemTemplate>
                    <asp:CheckBox ID="chkWinner" runat="server" OnCheckedChanged="chkWinnerChanged" Checked='<%# Bind("winner") %>' AutoPostBack="True" />                    
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("winner") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="id" Visible="False">
                <ItemTemplate>
                    <asp:Label ID="lblID" runat="server" Text='<%# Bind("id") %>' Visible="False"></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("id") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:Label ID="lblInfo" runat="server" ForeColor="Red"></asp:Label>
   
    </center>
</asp:Content>

