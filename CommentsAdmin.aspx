<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageNoCss.master" AutoEventWireup="true"
    CodeFile="CommentsAdmin.aspx.cs" Inherits="CommentsAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <asp:SqlDataSource ID="SqlDataSource1" SelectCommandType="StoredProcedure" SelectCommand="SelectAllComments"
     
        UpdateCommand="update comments SET comments=@comments, firstName=@firstName, lastName=@lastName where id=@ID"
        DeleteCommand="delete from comments where id=@id"
        runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>">
        <UpdateParameters>
            <asp:Parameter Name="Comments" />
            <asp:Parameter Name="FirstName" />
            <asp:Parameter Name="LastName" />
            <asp:Parameter Name="ID" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <asp:GridView ID="grdComments" runat="server" DataSourceID="SqlDataSource1" AutoGenerateColumns="False"
        Width="97%" DataKeyNames="id" ondatabound="grdComments_DataBound">
        <Columns>
            <asp:TemplateField HeaderText="Comments">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Rows="10" Text='<%# Bind("comments") %>'
                        TextMode="MultiLine" Width="90%"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("comments") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Property#">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("propID") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                   
                    <asp:HyperLink ID="hlkProp" runat="server" Text='<%# Bind("propID") %>'></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="firstName" HeaderText="First Name" />
            <asp:BoundField DataField="lastName" HeaderText="Last Name" />
            <asp:BoundField DataField="arrivalDate" HeaderText="Arrival Date" />
             <asp:BoundField DataField="dateEntered" HeaderText="Date Posted" />
             <asp:BoundField DataField="phone" HeaderText="Phone" />
            <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
        </Columns>
    </asp:GridView>
    <asp:Label ID="lblInfo" runat="server" ForeColor="Red"></asp:Label>
</asp:Content>
