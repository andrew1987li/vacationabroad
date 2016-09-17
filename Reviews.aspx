<%@ Page Title="<%# GetTitle () %>" Language="C#" MasterPageFile="~/MasterPageNoCss.master" AutoEventWireup="true"
    CodeFile="Reviews.aspx.cs" Inherits="PropertyReviewRead" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <br />
    <center>
        <div style="width: 98%; min-height: 400px">
            <div style="float: left; text-align: left;">
                <asp:Label ID="lblTitle" runat="server" Text="Review for " Font-Bold="True"></asp:Label><br />
                <asp:Label ID="lblAddress" runat="server"></asp:Label>
            </div>
            <div style="float: right;">
               
                <asp:HyperLink ID="hlkCity" runat="server"></asp:HyperLink>,
                <asp:HyperLink ID="hlkPropNum" runat="server"></asp:HyperLink>
            </div>
            <br />
            <br />
            <div id="divLftContent" style="width: 35%; float: left;">
                <div style="float: left;">
                    <asp:Image ID="imgProperty" runat="server" /></div>
            </div>
            <div id="divRightContent" style="width: 65%; float: right;">
            <%--<div style="width:19%; float:right;">
            <div class="rtOuter" style="width:100%; text-align:left;">
                                        <div class="rtHeader">
                                            <%# stateprovince %>
                                            Cities</div>
                                        <div id="divCitiesRt" runat="server" class="rtText">
                                        </div>
                                    </div>
            </div>--%>
            <div style="width:100%;">
                <asp:Repeater ID="rptReviews" runat="server" 
                    onitemdatabound="rptReviews_ItemDataBound">
                <HeaderTemplate>
                <table width="98%" border="0" cellpadding="0" cellspacing="0">
                </HeaderTemplate>
                    <ItemTemplate>
                    <tr>
                    <td align="left" style="border-bottom: solid 1px #474747; width:40%">
                    By: <%# DataBinder.Eval(Container.DataItem,"FirstName") %> <%# DataBinder.Eval(Container.DataItem,"LastName") %>
                    
                        <%--<div id="divStars" runat="server" style="float:left;">
                        </div>--%>
                    </td>
                    <td align="left" style="border-bottom: solid 1px #000000; width:25%;">
                    Rating <asp:Label ID="lblStars" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "rating") %>'></asp:Label>

                    </td>
                    <td style="border-bottom: solid 1px #000000; width:35%" align="right">
                    Trip Occurred: <%# DataBinder.Eval(Container.DataItem,"arrivalDate",  "{0:MMM yyyy}") %>
                    </td>
                    </tr>
                    <tr>
                    <td colspan="3" align="left">
                   
                    <%# HttpUtility.HtmlEncode(DataBinder.Eval(Container.DataItem,"Comments").ToString()) %>
                    <br /> 
                    Posted on: <%# DataBinder.Eval(Container.DataItem, "dateEntered", "{0:MMM dd yyyy}")%>
                    <br /><br />
                    </td>
                    </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                    </table>
                    </FooterTemplate>
                </asp:Repeater>  
                </div>              
            </div>
            <asp:Label ID="lblInfo" runat="server" ForeColor="Red"></asp:Label>
        </div>
    </center>
</asp:Content>
