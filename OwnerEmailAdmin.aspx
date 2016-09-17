<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageNoCss.master" AutoEventWireup="true"
    CodeFile="OwnerEmailAdmin.aspx.cs" Inherits="CommentsAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <%--<asp:SqlDataSource ID="SqlDataSource1" SelectCommandType="StoredProcedure" SelectCommand="SelectAllWarningEmails"
     
        UpdateCommand="update warningemails SET email=@email id=@ID"
        DeleteCommand="delete from comments where id=@id"
        runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>">
        <UpdateParameters>
            <asp:Parameter Name="email" />                        
            <asp:Parameter Name="ID" />
        </UpdateParameters>
    </asp:SqlDataSource>--%>
    <div align="center">
    <asp:GridView ID="grdComments" runat="server" AutoGenerateColumns="False"
        Width="85%" DataKeyNames="id" ondatabound="grdComments_DataBound">
        <Columns>            
            <asp:TemplateField HeaderText="Email">
                <ItemTemplate>
                <div align="left">
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("email") %>'></asp:Label>
                    </div>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("email") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="dateBounce" HeaderText="Bounce Date & Time" />          
            <%--<asp:CommandField ShowDeleteButton="True" />--%><asp:BoundField 
                DataField="ownerID" HeaderText="OwnerID" />
            <asp:TemplateField HeaderText="Unsubscribe">
                <ItemTemplate>
                   
         <input type="button" value="Unsubscribe" onclick='window.location.href=&quot;<%# CommonFunctions.PrepareURL ("NewsletterUnsubscribe.aspx?email=" + DataBinder.Eval(Container.DataItem, "email", "{0}")) %>&quot;'>

                </ItemTemplate>
                <EditItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" />
                </EditItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    </div>
    <asp:Label ID="lblInfo" runat="server" ForeColor="Red"></asp:Label>
</asp:Content>
