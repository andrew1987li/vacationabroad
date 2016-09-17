<%@ Page Title="<%# GetTitle () %>" Language="C#" MasterPageFile="~/MasterPageNoCss.master" AutoEventWireup="true"
    CodeFile="ViewTour.aspx.cs" Inherits="ViewTour" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
    <div align="left" style="width:75%">
        <h1>
            <%# tourType %> Tours in <%# city %>, <%# state %></h1>
       <a href="<%# countryLink %>"><%# country %> vacation rentals</a>, 
       <a href="<%# stateLink %>"><%# state %> vacation rentals</a>, 
       <a href="<%# cityLink %>"><%# city %> vacation rentals</a>
      

       <br /> <br />
        <div align="center">
            <asp:Image ID="imgTour" runat="server" />
            <br />
            <table style="width:100%; text-align:left;">
            <tr><td width="25%"><b>Tour Name:</b></td>
            <td width="75%"><%# tourName %></td>
            </tr>
            <tr><td valign="top"><b>Tour Description:</b></td>
            <td><%# tourDesc %></td>
            </tr>
            <tr><td><b>Starting Location:</b></td>
            <td><%# tourAddress %></td>
            </tr>
            <tr><td><b>Tour Starting Time:</b></td>
            <td><%# tourTime %></td>
            </tr>
            <tr><td><b>Tour Days:</b></td>
            <td><asp:Label ID="lblDays" runat="server"></asp:Label></td>
            </tr>
            <tr><td><b>Tour Type:</b></td>
            <td><%# tourType %></td>
            </tr>   
             <tr><td><b>Tour Starting Rate:</b></td>
            <td><%# lowRate %> <%# abbr %></td>
            </tr>          
            </table>
            <br />
            <table style="border:solid 2px #CC6600; width:100%; text-align:left;">
            <tr>
            <td width="25%">
            Company Name
            </td>
            <td width="75%">
            <b><%# tourCompany %></b>
            </td>
            </tr>
            <tr>
            <td>Contact Name</td>
            <td><b><%# tourContact %></b></td>
            </tr>
            <tr>
            <td style="height: 17px">Website</td>
            <td style="height: 17px"><b>
                <asp:HyperLink ID="hlkSite" runat="server"></asp:HyperLink></b></td>
            </tr>
            <tr>
            <td>Primary Telephone</td>
            <td><b><%# tourPriPhone %></b></td>
            </tr>
            <tr>
            <td>Mobile Phone</td>
            <td><b><%# tourMobile %></b></td>
            </tr>
            </table>
        </div>
    </div>
</asp:Content>
