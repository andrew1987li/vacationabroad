<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageNoCss.master" AutoEventWireup="true" CodeFile="PropertyCalendar.aspx.cs" Inherits="Default3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
    <div align="center" style="font-family:Arial; font-size:14px;">
<%--<input onclick="window.location.href=&quot;<%# CommonFunctions.PrepareURL ("Listings.aspx?UserID=" + Request.QueryString["UserID"].ToString()) %>&quot;;"" 
style="width: 100px" type="button" value="Previous Page"/>--%>

<% if (BackLink.Visible) { %>
    <table bgcolor="#e4e4af" cellspacing="0" cellpadding="0" width="350" align="center" border="2">
        <tr>
            <td>
                <div align="center">
                    <strong>
                        <asp:HyperLink ID="BackLink" runat="server" NavigateUrl="Listings.aspx">
							Return to listings page
                        </asp:HyperLink>
                    </strong>
                </div>
            </td>
        </tr>
    </table>
    <% } %>

<br />
<br />
    <asp:Label ID="lblHeaderMsg" runat="server"></asp:Label>
    <br />
    <asp:CheckBox ID="chkDisplay" runat="server" Font-Bold="True" ForeColor="Red" 
        oncheckedchanged="chkDisplay_CheckedChanged" Text="Display to Website" 
        AutoPostBack="True" />
    <br />
    <asp:Label ID="lblTest" runat="server" ForeColor="Red"></asp:Label>
    <br />
<asp:Calendar ID="Calendar1" runat="server" OnDayRender="Calendar1_DayRender" 
        onselectionchanged="Calendar1_SelectionChanged" SelectionMode="Day" 
        Font-Names="Arial" Font-Size="14px">
    <NextPrevStyle ForeColor="Black" />
    <TitleStyle BackColor="White" ForeColor="Black" />
    </asp:Calendar>
        <br />
        <asp:Label ID="Label1" runat="server" BackColor="Red" ForeColor="Red" 
        Text="Red"></asp:Label>&nbsp;Dates in RED have reservations
        <br />
        <br />
        <asp:Label ID="lblCityPage" runat="server" Text="Calendar will be displayed on the city page."></asp:Label>
        <br />
        <% if (BackLink.Visible) { %>
    
                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="Listings.aspx">
							Return to your listings
                        </asp:HyperLink>                    
    <% } %>
        <br />
        
        <asp:HyperLink ID="lnkCity" runat="server">HyperLink</asp:HyperLink>
        <br />
        <br />
</div>
</asp:Content>

