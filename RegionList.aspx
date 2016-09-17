<%--<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="RegionList.aspx.cs" Inherits="RegionList" Title="<%# GetTitle () %>" %>--%>

<%@ Page Language="C#" EnableViewState="false" MasterPageFile="~/MasterPageNoCss.master" AutoEventWireup="true" CodeFile="RegionList.aspx.cs" Inherits="RegionList" Title="<%# GetTitle () %>" %>

<%@ OutputCache Duration="600" VaryByParam="*" %>

<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="Server">
    <div style="height: 413px;">
    </div>
    <asp:Repeater ID="Repeater1" runat="server" DataMember="Countries" DataSource="<%# CountriesStates %>" Visible="false">
        <HeaderTemplate>
            <table width="97%" cellpadding="0" cellspacing="2" border="0"
                bgcolor="#ffffff" align="center">
                <tr>
                    <td colspan="25">
                        <h1>
                            <p align="center">
                                <font size="3" color="#cc3333" face="Arial">
                                    <%= TableTitle ()%>
                                </font>
                        </h1>

                    </td>
                </tr>
                <tr bgcolor="#ffffff">
                    <td></td>
                    <td></td>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td valign="top">
                    <h2><div style="width: 130px; text-align: left;">
                        <font size="2"><a href='<%# CommonFunctions.PrepareURL (((string)DataBinder.Eval(Container.DataItem, "Country", "{0}")).Replace (" ", "_").ToLower () + "/default.aspx") %>'>
                            <b><%# DataBinder.Eval(Container.DataItem, "Country", "{0}") %></b>
                        </a></font>
                    </div></h2>
                </td>
                <td>
                    <div style="text-align: left;">
                        <asp:Repeater ID="Repeater2" runat="server" DataSource='<%# ((System.Data.DataRowView)Container.DataItem).CreateChildView("CountriesStates")%>'>
                            <HeaderTemplate>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <font size="2"><a href='<%# CommonFunctions.PrepareURL (((string)DataBinder.Eval(Container.DataItem, "Country", "{0}")).Replace (" ", "_").ToLower () + "/" + ((string)DataBinder.Eval(Container.DataItem, "StateProvince", "{0}")).Replace (" ", "_").ToLower () + "/default.aspx") %>'>
                                    <b><%# DataBinder.Eval(Container.DataItem, "StateProvince", "{0}") %></a></font>
                                <%# !CommonFunctions.IfLastRow ((System.Data.DataRowView)Container.DataItem) ? "," : "" %>
                            </ItemTemplate>
                            <FooterTemplate>
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>
                    <br />
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            <tr>
                <td colspan="35">
                    <p align="center">
                        <font size="2" face="Arial">
                            <%= TableTitle ()%>
                        </font>
                    </p>
                </td>
            </tr>
            </table>
        </FooterTemplate>
    </asp:Repeater>
    <br />


    <!-- Start of StatCounter Code -->

    <script type="text/javascript">
        var sc_project = 3812793;
        var sc_invisible = 1;
        var sc_partition = 34;
        var sc_click_stat = 1;
        var sc_security = "e3bbd0d0";
        $(document).ready(function () {
            if ($("input[id$='hdnRC']").val() != '') {
                var regionCode = $("input[id$='hdnRC']").val();
                $("div.tab_container div.i" + regionCode).fadeIn();
            }
        });
    </script>

    <script type="text/javascript" src="http://www.statcounter.com/counter/counter_xhtml.js"></script>

    <noscript>
        <div class="statcounter">
            <a href="http://www.statcounter.com/" target="_blank">
                <img class="statcounter" src="http://c.statcounter.com/3812793/0/e3bbd0d0/1/" alt="web statistic"></a>
        </div>
    </noscript>
    <!-- End of StatCounter Code -->
    <asp:HiddenField runat="server" ID="hdnRC" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TabContainer" runat="server">
    <!--BOF tab_container-->
    <div class="tab_container">
        <!--BOF tab1-->
        <div id="tab1" class="tab_content">
        </div>
        <!--EOF tab1-->
        <div id="conNorthAmerica" runat="server" visible="false">
            <!--BOF tabNorthAmerica-->
            <div style="display: none;" id="tabNorthAmerica" class="tab_content i8" title="North America Vacations">
                <div class="tapsectionNorthAmerica">
                    <div class="section2">
                        <h1 class="BlueHeader">
                            North America Vacations
                        </h1>
                        <div style="border: solid 0px #ececec; padding: 2px;">
                            <div id="divnAmerica" runat="server">
                            </div>
                        </div>
                    </div>
                </div>
                <div class="cont_rht fltrht dvRight" runat="server" id="Div6">
                    <div class="rhtsection">
                        <h2>
                            Maui Hawaii</h2>
                        <a href="http://www.vacations-abroad.com/usa/hawaii/maui/default.aspx">
                            <img src="/images/RightSideNorthAmerica01.jpg" /></a>
                    </div>
                    <div class="rhtsection">
                        <h2>
                            Mt Tremblant Canada</h2>
                        <a href="http://www.vacations-abroad.com/canada/quebec/mont_tremblant/default.aspx">
                            <img src="/images/RightSideNorthAmerica02.jpg" /></a>
                    </div>
                    <div class="rhtsection">
                        <h2>
                            Whistler Canada</h2>
                        <a href="http://www.vacations-abroad.com/usa/hawaii/maui/default.aspx">
                            <img src="/images/RightSideNorthAmerica03.jpg" /></a>
                    </div>
                </div>
            </div>
            <!--EOF tabNorthAmerica-->
        </div>
        <div id="conSouthAmerica" runat="server" visible="false">
            <!--BOF tabSouthAmerica-->
            <div style="display: none;" id="tabSouthAmerica" class="tab_content i9" title="South America Vacations">
                <div class="tapsectionSouthAmerica">
                    <div class="section2">
                        <h1 class="BlueHeader">
                            South America Vacations
                        </h1>
                        <div style="border: solid 0px #ececec; padding: 2px;">
                            <div id="divSouthAmerica" runat="server">
                            </div>
                        </div>
                    </div>
                </div>
                <div class="cont_rht fltrht dvRight" runat="server" id="Div5">
                    <div class="rhtsection">
                        <h2>
                            Buenos Aires Argentina</h2>
                        <a href="http://www.vacations-abroad.com/argentina/buenos_aires/default.aspx">
                            <img src="/images/RightSideSouthAmerica01.jpg" /></a>
                    </div>
                    <div class="rhtsection">
                        <h2>
                            Lima Peru</h2>
                        <a href="http://www.vacations-abroad.com/peru/lima/default.aspx">
                            <img src="/images/RightSideSouthAmerica02.jpg" /></a>
                    </div>
                    <div class="rhtsection">
                        <h2>
                            Rio de Janerio Brazil</h2>
                        <a href="http://www.vacations-abroad.com/brazil/rio_de_janeiro/default.aspx">
                            <img src="/images/RightSideSouthAmerica03.jpg" /></a>
                    </div>
                </div>
            </div>
            <!--EOF tabSouthAmerica-->
        </div>
        <div id="conEurope" runat="server" visible="false">
            <!--BOF tabEurope-->
            <div style="display: none;" id="tabEurope" class="tab_content i6" title="Europe Vacations">
                <div class="tapsectionEurope">
                    <div class="section2">
                        <h1 class="BlueHeader">
                            Europe Vacations
                        </h1>
                        <div style="border: solid 0px #ececec; padding: 2px;">
                            <div id="divEurope" runat="server">
                            </div>
                        </div>
                    </div>
                </div>
                <div class="cont_rht fltrht dvRight" runat="server" id="Div4">
                    <div class="rhtsection">
                        <h2>
                            Algarve Portugal</h2>
                        <a href="http://www.vacations-abroad.com/portugal/algarve/default.aspx">
                            <img src="/images/RightSideEurope01.jpg" width="181" height="132" alt=""></a>
                    </div>
                    <div class="rhtsection">
                        <h2>
                            Chamonix France</h2>
                        <a href="http://www.vacations-abroad.com/france/rhone_alps/chamonix/default.aspx">
                            <img src="/images/RightSideEurope02.jpg" width="181" height="132" alt=""></a>
                    </div>
                    <div class="rhtsection">
                        <h2>
                            Zermatt Switzerland</h2>
                        <a href="http://www.vacations-abroad.com/switzerland/valais/zermatt/default.aspx">
                            <img src="/images/RightSideEurope03.jpg" width="181" height="130" alt=""></a>
                    </div>
                </div>
            </div>
            <!--EOF tabEurope-->
        </div>
        <div id="conAsia" runat="server" visible="false">
            <!--BOF tabAsia-->
            <div style="display: none;" id="tabAsia" class="tab_content i2" title="Asia Vacations">
                <div class="tapsectionAsia">
                    <div class="section2">
                        <h1 class="BlueHeader">
                            Asia
                            <br />
                            Vacations
                        </h1>
                        <div style="border: solid 0px #ececec; padding: 2px;">
                            <div id="divAsia" runat="server">
                            </div>
                        </div>
                    </div>
                </div>
                <div class="cont_rht fltrht dvRight" runat="server" id="Div3">
                    <div class="rhtsection">
                        <h2>
                            Koh Chang Thailand</h2>
                        <a href="http://www.vacations-abroad.com/thailand/trat/koh_chang/default.aspx">
                            <img src="/images/RightSideAsia01.jpg" /></a>
                    </div>
                    <div class="rhtsection">
                        <h2>
                            Manilla Philippines</h2>
                        <a href="http://www.vacations-abroad.com/philippines/metro_manila/default.aspx">
                            <img src="/images/RightSideAsia02.jpg" /></a>
                    </div>
                    <div class="rhtsection">
                        <h2>
                            Kalutara Sri Lanka</h2>
                        <a href="http://www.vacations-abroad.com/sri_lanka/western_province/kalutara/default.aspx">
                            <img src="/images/RightSideAsia03.jpg" /></a>
                    </div>
                </div>
            </div>
            <!--EOF tabAsia-->
        </div>
        <div id="conAfrica" runat="server" visible="false">
            <!--BOF tabAfrica-->
            <div style="display: none;" id="tabAfrica" class="tab_content i1" title="Africa Vacations">
                <div class="tapsectionAfrica">
                    <div class="section2">
                        <h1 class="BlueHeader">
                            Africa Vacations
                        </h1>
                        <div style="border: solid 0px #ececec; padding: 2px;">
                            <div id="divAfrica" runat="server">
                            </div>
                        </div>
                    </div>
                </div>
                <div class="cont_rht fltrht dvRight" runat="server" id="Div2">
                    <div class="rhtsection">
                        <h2>
                            South Africa</h2>
                        <a href="http://www.vacations-abroad.com/south_africa/cape_province/default.aspx">
                            <img src="/images/RightSideAfrica02.jpg" /></a>
                    </div>
                    <div class="rhtsection">
                        <h2>
                            Diani Beach Kenya</h2>
                        <a href="http://www.vacations-abroad.com/kenya/coast/diani_beach/default.aspx">
                            <img src="/images/RightSideAfrica03.jpg" /></a>
                    </div>
                    <div class="rhtsection">
                        <h2>
                            Marrakesh Morocco</h2>
                        <a href="http://www.vacations-abroad.com/morocco/marrakech/default.aspx">
                            <img src="/images/RightSideAfrica01.jpg" /></a>
                    </div>
                </div>
            </div>
            <!--EOF tabAfrica-->
        </div>
        <div id="conOceania" runat="server" visible="false">
            <!--BOF tabOceania-->
            <div style="display: none;" id="tabOceania" class="tab_content i3" title="Oceania Vacations">
                <div class="tapsectionOceania">
                    <div class="section2">
                        <h1 class="BlueHeader">
                            Oceania Vacations
                        </h1>
                        <div style="border: solid 0px #ececec; padding: 2px;">
                            <div id="divOceania" runat="server">
                            </div>
                        </div>
                    </div>
                </div>
                <div class="cont_rht fltrht dvRight" runat="server" id="Div1">
                    <div class="rhtsection">
                        <h2>
                            Cairns Australia</h2>
                        <a href="http://www.vacations-abroad.com/australia/queensland/cairns/default.aspx">
                            <img src="/images/RightSideOceania01.jpg" /></a>
                    </div>
                    <div class="rhtsection">
                        <h2>
                            Auckland New Zealand</h2>
                        <a href="http://www.vacations-abroad.com/new_zealand/auckland/default.aspx">
                            <img src="/images/RightSideOceania02.jpg" /></a>
                    </div>
                    <div class="rhtsection">
                        <h2>
                            Sydney Australia</h2>
                        <a rel="nofollow" href="http://www.vacations-abroad.com/australia/new_south_wales/sydney/default.aspx">
                            <img src="/images/RightSideOceania03.jpg" /></a>
                    </div>
                </div>
            </div>
            <!--EOF tabOceania-->
        </div>
    </div>
    <!--EOF tab_container-->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="DefaultPageFeaturedCitiesContainer"
    runat="server">
    <div style="height: 511px;">
    </div>
</asp:Content>