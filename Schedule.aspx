<%@ Page Language="C#" MasterPageFile="~/MasterPageNoCss.master" AutoEventWireup="true"
    CodeFile="~/Schedule.aspx.cs" Inherits="PropSched" Title="Property Schedule"
    EnableEventValidation="false" %>

<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="Server">

<table width="100%"><tr>                           
           <td width="25%">             
<asp:Calendar ID="Calendar1" runat="server" OnDayRender="Calendar1_DayRender" 
        SelectionMode="Day" BorderColor="#000000" Width="175px"
        Font-Names="Arial" Font-Size="14px">
    <NextPrevStyle ForeColor="Black" />
    <TitleStyle BackColor="#ffffff" ForeColor="Black" />
    </asp:Calendar>
</td>
<td width="25%">             
<asp:Calendar ID="Calendar2" runat="server" OnDayRender="Calendar1_DayRender" 
        SelectionMode="Day" BorderColor="#000000" Width="175px" ShowNextPrevMonth="False"
        Font-Names="Arial" Font-Size="14px">
    <NextPrevStyle ForeColor="Black" />
    <TitleStyle BackColor="#ffffff" ForeColor="Black" />
    </asp:Calendar>
</td>
<td width="25%">             
<asp:Calendar ID="Calendar3" runat="server" OnDayRender="Calendar1_DayRender" 
        SelectionMode="Day" BorderColor="#000000" Width="175px" ShowNextPrevMonth="False"
        Font-Names="Arial" Font-Size="14px">
    <NextPrevStyle ForeColor="Black" />
    <TitleStyle BackColor="#ffffff" ForeColor="Black" />
    </asp:Calendar>
</td>

<td width="25%">             
<asp:Calendar ID="Calendar4" runat="server" OnDayRender="Calendar1_DayRender" 
        SelectionMode="Day" BorderColor="#000000" Width="175px" ShowNextPrevMonth="False"
        Font-Names="Arial" Font-Size="14px">
    <NextPrevStyle ForeColor="Black" />
    <TitleStyle BackColor="#ffffff" ForeColor="Black" />
    </asp:Calendar>
</td>
                               
</tr>

<tr>                           
           <td width="25%">             
<asp:Calendar ID="Calendar5" runat="server" OnDayRender="Calendar1_DayRender" 
        SelectionMode="Day" BorderColor="#000000" Width="175px"
        Font-Names="Arial" Font-Size="14px">
    <NextPrevStyle ForeColor="Black" />
    <TitleStyle BackColor="#ffffff" ForeColor="Black" />
    </asp:Calendar>
</td>
<td width="25%">             
<asp:Calendar ID="Calendar6" runat="server" OnDayRender="Calendar1_DayRender" 
        SelectionMode="Day" BorderColor="#000000" Width="175px" ShowNextPrevMonth="False"
        Font-Names="Arial" Font-Size="14px">
    <NextPrevStyle ForeColor="Black" />
    <TitleStyle BackColor="#ffffff" ForeColor="Black" />
    </asp:Calendar>
</td>
<td width="25%">             
<asp:Calendar ID="Calendar7" runat="server" OnDayRender="Calendar1_DayRender" 
        SelectionMode="Day" BorderColor="#000000" Width="175px" ShowNextPrevMonth="False"
        Font-Names="Arial" Font-Size="14px">
    <NextPrevStyle ForeColor="Black" />
    <TitleStyle BackColor="#ffffff" ForeColor="Black" />
    </asp:Calendar>
</td>

<td width="25%">             
<asp:Calendar ID="Calendar8" runat="server" OnDayRender="Calendar1_DayRender" 
        SelectionMode="Day" BorderColor="#000000" Width="175px" ShowNextPrevMonth="False"
        Font-Names="Arial" Font-Size="14px">
    <NextPrevStyle ForeColor="Black" />
    <TitleStyle BackColor="#ffffff" ForeColor="Black" />
    </asp:Calendar>
</td>
                               
</tr>

<tr>                           
           <td width="25%">             
<asp:Calendar ID="Calendar9" runat="server" OnDayRender="Calendar1_DayRender" 
        SelectionMode="Day" BorderColor="#000000" Width="175px"
        Font-Names="Arial" Font-Size="14px">
    <NextPrevStyle ForeColor="Black" />
    <TitleStyle BackColor="#ffffff" ForeColor="Black" />
    </asp:Calendar>
</td>
<td width="25%">             
<asp:Calendar ID="Calendar10" runat="server" OnDayRender="Calendar1_DayRender" 
        SelectionMode="Day" BorderColor="#000000" Width="175px" ShowNextPrevMonth="False"
        Font-Names="Arial" Font-Size="14px">
    <NextPrevStyle ForeColor="Black" />
    <TitleStyle BackColor="#ffffff" ForeColor="Black" />
    </asp:Calendar>
</td>
<td width="25%">             
<asp:Calendar ID="Calendar11" runat="server" OnDayRender="Calendar1_DayRender" 
        SelectionMode="Day" BorderColor="#000000" Width="175px" ShowNextPrevMonth="False"
        Font-Names="Arial" Font-Size="14px">
    <NextPrevStyle ForeColor="Black" />
    <TitleStyle BackColor="#ffffff" ForeColor="Black" />
    </asp:Calendar>
</td>

<td width="25%">             
<asp:Calendar ID="Calendar12" runat="server" OnDayRender="Calendar1_DayRender" 
        SelectionMode="Day" BorderColor="#000000" Width="175px"
        Font-Names="Arial" Font-Size="14px">
    <NextPrevStyle ForeColor="Black" />
    <TitleStyle BackColor="#ffffff" ForeColor="Black" />
    </asp:Calendar>
</td>
                               
</tr>





</table>


</asp:Content>