<%@ Page Language="C#" AutoEventWireup="true" CodeFile="lmgmonitor.aspx.cs" Inherits="Default2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>VA Exception monitor</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:herefordpiesConnectionString1 %>"
            SelectCommand="SELECT top 20 [Date], [Message], [Source], [StackTrace], [RequestURL], [RequestReferer], [UserAgent], [UserIP] FROM [Exceptions] ORDER BY [Date] DESC">
        </asp:SqlDataSource>
    
    </div>
        <asp:DataList ID="DataList1" runat="server" CellPadding="4" DataSourceID="SqlDataSource1"
            ForeColor="#333333">
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
            <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <ItemTemplate>
                Date:
                <asp:Label ID="DateLabel" runat="server" Text='<%# Eval("Date") %>'></asp:Label><br />
                Message:
                <asp:Label ID="MessageLabel" runat="server" Text='<%# Eval("Message") %>'></asp:Label><br />
                Source:
                <asp:Label ID="SourceLabel" runat="server" Text='<%# Eval("Source") %>'></asp:Label><br />
                StackTrace:
                <asp:Label ID="StackTraceLabel" runat="server" Text='<%# Eval("StackTrace") %>'>
                </asp:Label><br />
                RequestURL:
                <asp:Label ID="RequestURLLabel" runat="server" Text='<%# Eval("RequestURL") %>'>
                </asp:Label><br />
                RequestReferer:
                <asp:Label ID="RequestRefererLabel" runat="server" Text='<%# Eval("RequestReferer") %>'>
                </asp:Label><br />
                UserAgent:
                <asp:Label ID="UserAgentLabel" runat="server" Text='<%# Eval("UserAgent") %>'></asp:Label><br />
                UserIP:
                <asp:Label ID="UserIPLabel" runat="server" Text='<%# Eval("UserIP") %>'></asp:Label><br />
                <br />
            </ItemTemplate>
        </asp:DataList>
    </form>
</body>
</html>
