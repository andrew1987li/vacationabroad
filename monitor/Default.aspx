<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="monitor_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        &nbsp;<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:herefordpiesConnectionString1 %>"
            DeleteCommand="DELETE FROM [Exceptions] WHERE [ID] = @ID" InsertCommand="INSERT INTO [Exceptions] ([Date], [Message], [Source], [StackTrace], [RequestURL], [RequestReferer], [UserAgent], [UserIP]) VALUES (@Date, @Message, @Source, @StackTrace, @RequestURL, @RequestReferer, @UserAgent, @UserIP)"
            ProviderName="<%$ ConnectionStrings:herefordpiesConnectionString1.ProviderName %>"
            SelectCommand="SELECT [ID], [Date], [Message], [Source], [StackTrace], [RequestURL], [RequestReferer], [UserAgent], [UserIP] FROM [Exceptions]"
            UpdateCommand="UPDATE [Exceptions] SET [Date] = @Date, [Message] = @Message, [Source] = @Source, [StackTrace] = @StackTrace, [RequestURL] = @RequestURL, [RequestReferer] = @RequestReferer, [UserAgent] = @UserAgent, [UserIP] = @UserIP WHERE [ID] = @ID">
            <InsertParameters>
                <asp:Parameter Name="Date" Type="DateTime" />
                <asp:Parameter Name="Message" Type="String" />
                <asp:Parameter Name="Source" Type="String" />
                <asp:Parameter Name="StackTrace" Type="String" />
                <asp:Parameter Name="RequestURL" Type="String" />
                <asp:Parameter Name="RequestReferer" Type="String" />
                <asp:Parameter Name="UserAgent" Type="String" />
                <asp:Parameter Name="UserIP" Type="String" />
            </InsertParameters>
            <UpdateParameters>
                <asp:Parameter Name="Date" Type="DateTime" />
                <asp:Parameter Name="Message" Type="String" />
                <asp:Parameter Name="Source" Type="String" />
                <asp:Parameter Name="StackTrace" Type="String" />
                <asp:Parameter Name="RequestURL" Type="String" />
                <asp:Parameter Name="RequestReferer" Type="String" />
                <asp:Parameter Name="UserAgent" Type="String" />
                <asp:Parameter Name="UserIP" Type="String" />
                <asp:Parameter Name="ID" Type="Int32" />
            </UpdateParameters>
            <DeleteParameters>
                <asp:Parameter Name="ID" Type="Int32" />
            </DeleteParameters>
        </asp:SqlDataSource>
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            DataKeyNames="ID" DataSourceID="SqlDataSource2" EmptyDataText="There are no data records to display.">
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" SortExpression="ID" />
                <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" />
                <asp:BoundField DataField="Message" HeaderText="Message" SortExpression="Message" />
                <asp:BoundField DataField="Source" HeaderText="Source" SortExpression="Source" />
                <asp:BoundField DataField="StackTrace" HeaderText="StackTrace" SortExpression="StackTrace" />
                <asp:BoundField DataField="RequestURL" HeaderText="RequestURL" SortExpression="RequestURL" />
                <asp:BoundField DataField="RequestReferer" HeaderText="RequestReferer" SortExpression="RequestReferer" />
                <asp:BoundField DataField="UserAgent" HeaderText="UserAgent" SortExpression="UserAgent" />
                <asp:BoundField DataField="UserIP" HeaderText="UserIP" SortExpression="UserIP" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:herefordpiesConnectionString1 %>"
            DeleteCommand="DELETE FROM [Exceptions] WHERE [ID] = @ID" InsertCommand="INSERT INTO [Exceptions] ([Date], [Message], [Source], [StackTrace], [RequestURL], [RequestReferer], [UserAgent], [UserIP]) VALUES (@Date, @Message, @Source, @StackTrace, @RequestURL, @RequestReferer, @UserAgent, @UserIP)"
            ProviderName="<%$ ConnectionStrings:herefordpiesConnectionString1.ProviderName %>"
            SelectCommand="SELECT [ID], [Date], [Message], [Source], [StackTrace], [RequestURL], [RequestReferer], [UserAgent], [UserIP] FROM [Exceptions] ORDER BY [Date] DESC"
            UpdateCommand="UPDATE [Exceptions] SET [Date] = @Date, [Message] = @Message, [Source] = @Source, [StackTrace] = @StackTrace, [RequestURL] = @RequestURL, [RequestReferer] = @RequestReferer, [UserAgent] = @UserAgent, [UserIP] = @UserIP WHERE [ID] = @ID">
            <DeleteParameters>
                <asp:Parameter Name="ID" Type="Int32" />
            </DeleteParameters>
            <UpdateParameters>
                <asp:Parameter Name="Date" Type="DateTime" />
                <asp:Parameter Name="Message" Type="String" />
                <asp:Parameter Name="Source" Type="String" />
                <asp:Parameter Name="StackTrace" Type="String" />
                <asp:Parameter Name="RequestURL" Type="String" />
                <asp:Parameter Name="RequestReferer" Type="String" />
                <asp:Parameter Name="UserAgent" Type="String" />
                <asp:Parameter Name="UserIP" Type="String" />
                <asp:Parameter Name="ID" Type="Int32" />
            </UpdateParameters>
            <InsertParameters>
                <asp:Parameter Name="Date" Type="DateTime" />
                <asp:Parameter Name="Message" Type="String" />
                <asp:Parameter Name="Source" Type="String" />
                <asp:Parameter Name="StackTrace" Type="String" />
                <asp:Parameter Name="RequestURL" Type="String" />
                <asp:Parameter Name="RequestReferer" Type="String" />
                <asp:Parameter Name="UserAgent" Type="String" />
                <asp:Parameter Name="UserIP" Type="String" />
            </InsertParameters>
        </asp:SqlDataSource>
    
    </div>
    </form>
</body>
</html>
