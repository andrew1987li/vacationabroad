<%@ Page Language="C#" MasterPageFile="~/MasterPageNoCss.master" AutoEventWireup="True" Inherits="TravelAgents" Title="Travel Agent - Vacation rentals that pay a travel agent commission" EnableEventValidation="False" CodeFile="TravelAgents.aspx.cs" %>

<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="Server">
    <div align="center">
    <div class="apphead">
    <h1 class="listingPagesH1Color H1CityText">Travel Agents 
                            Vacation Rentals<br>
        </h1>
        </div>
        </div>
    <div class="Left listingPagesH1Container">
        
    </div>
    <div style="padding-top: 50px"></div>
    
    <asp:Repeater ID="Repeater1" runat="server" DataMember="Properties" DataSource="<%# PropertiesFullSet %>">
        <HeaderTemplate>
            <table cellspacing="1" cellpadding="0" width="750" align="center" border="0" bgcolor="#333366">
                <tr bgcolor="#333366">
                    <td bgcolor="#ffffff" align="center"></td>

                    <td bgcolor="#333366" align="center">
                        <font color="#ffffff" size="2"><strong>Country</strong></font>
                    </td>
                    <td bgcolor="#333366" align="center">
                        <font color="#ffffff" size="2"><strong>State/Province</strong></font>
                    </td>
                    <td bgcolor="#333366" align="center">
                        <font color="#ffffff" size="2"><strong>City</strong></font>
                    </td>
                    <td bgcolor="#333366" align="center">
                        <font color="#ffffff" size="2"><strong>Prop Type</strong></font>
                    </td>
                    <td bgcolor="#333366" align="center">
                        <font color="#ffffff" size="2"><strong>Bedrms</strong></font>
                    </td>
                    <td bgcolor="#333366" align="center">
                        <font color="#ffffff" size="2"><strong>Sleeps</strong></font>
                    </td>
                    <td bgcolor="#333366" align="center">
                        <font color="#ffffff" size="2"><strong>Pool</strong></font>
                    </td>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr bgcolor="#ffffff">
                <td bgcolor="#ffffff" align="center" style="padding: 3px">
                    <%# CommonFunctions.ShowPropertyPhotoWithoutBacklinkTravel (((System.Data.DataRowView)Container.DataItem).Row)%>
                </td>

                <td align="center" style="vertical-align: middle">
                    <%# DataBinder.Eval(Container.DataItem, "Country", "{0}") %>
                </td>
                <td align="center" style="vertical-align: middle">
                    <%# DataBinder.Eval(Container.DataItem, "StateProvince", "{0}") %>
                </td>
                <td align="center" style="vertical-align: middle">
                    <%# DataBinder.Eval(Container.DataItem, "City", "{0}") %>
                </td>
                <td align="center" style="vertical-align: middle">
                    <a href='<%# CommonFunctions.PrepareURL (DataBinder.Eval(Container.DataItem, "Country", "{0}").Replace (" ", "_").ToLower () + "/" + DataBinder.Eval(Container.DataItem, "StateProvince", "{0}").Replace (" ", "_").ToLower () + "/" + DataBinder.Eval(Container.DataItem, "City", "{0}").Replace (" ", "_").ToLower () + "/" + DataBinder.Eval(Container.DataItem, "ID", "{0:d}") + "/default.aspx") %>'>
                        <%# DataBinder.Eval(Container.DataItem, "Type", "{0}") %>
                    </a>
                </td>
                <td align="center" style="vertical-align: middle">
                    <%# DataBinder.Eval(Container.DataItem, "NumBedrooms", "{0}") %>
                </td>
                <td align="center" style="vertical-align: middle">
                    <%# DataBinder.Eval(Container.DataItem, "NumSleeps", "{0}") %>
                </td>
                <td align="center" style="vertical-align: middle">
                    <%# DataBinder.Eval(Container.DataItem, "Pool", "{0}") %>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>

    <!-- Start of StatCounter Code -->
    <script type="text/javascript">
        sc_project = 3650571;
        sc_invisible = 1;
        sc_partition = 43;
        sc_security = "16687e98";
    </script>

    <script type="text/javascript" src="http://www.statcounter.com/counter/counter_xhtml.js"></script>
    <noscript>
        <div class="statcounter">
            <a href="http://www.statcounter.com/" target="_blank">
                <img class="statcounter" src="http://c44.statcounter.com/3650571/0/16687e98/1/" alt="free web tracker"></a>
        </div>
    </noscript>
    <!-- End of StatCounter Code -->
    <!-- Start Quantcast tag -->
    <script type="text/javascript" src="http://edge.quantserve.com/quant.js"></script>
    <script type="text/javascript">_qacct = "p-a9k_t7603DATw"; quantserve();</script>
    <noscript>
        <a href="http://www.quantcast.com/p-a9k_t7603DATw" target="_blank">
            <img src="http://pixel.quantserve.com/pixel/p-a9k_t7603DATw.gif" style="display: none;" border="0" height="1" width="1" alt="Quantcast" /></a>
    </noscript>
    <!-- End Quantcast tag -->
</asp:Content>
