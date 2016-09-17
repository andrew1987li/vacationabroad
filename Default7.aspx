<%@ Page Language="C#" MasterPageFile="~/MasterPageNoCss7.master" AutoEventWireup="true"
    CodeFile="Default7.aspx.cs" Inherits="Default7" Title="<%# GetTitle () %>" EnableEventValidation="False" %>
<%@ OutputCache Duration="600" VaryByParam="*" %>

<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="Server">
    <style>
        .PDescText {
            color: #202027;
            text-align: left;
            padding-top: 4px;
            padding-bottom: 4px;
        }
    </style>
    <div class="tapsection" style="border: 1px;">
        <div class="section" style="position: absolute;top: 86px;left: 72px;">
            <h1 style="width: 230px; text-align: center; font-family: 'Trebuchet MS', Arial, Helvetica, sans-serif; font-size: 21px; color: white; font-weight: bold; text-shadow: #005784 2px 2px;">
               Explore the world <br>in our vacation rentals, B&Bs & Hotels                <%--Vacation Apartments and Holiday Rentals in the US and Abroad--%>
            </h1>

        </div>
        <div style="position: absolute; top: 637px;left: -348px;margin-left: 50%;">
            <table id="MainTable4" class="MainTable4" style="width: 880px;">
                <tbody>
                    <tr class="TR3">
                        <td valign="middle" style="font-size: 18px; font-weight: bold; background-color: #000072;">
                            <style="display: block; font-size: 1.45em; -webkit-margin-before: 0.83em; -webkit-margin-after: 0.83em; -webkit-margin-start: 0px; -webkit-margin-end: 0px; font-weight: bold;">
                                Live like a local or a king if you prefer!
                                <%--Vacation Apartments and Holiday Villa Rentals--%>
                            </style>

                        </td>
                    </tr>
                </tbody>
            </table>

            <center>
                <div style="border: solid 1px #ececec; background-color: White; padding: 2px;">
                    <center>
                        <br />
                        <h2><a class="mainRegion" href="http://www.vacations-abroad.com/africa/default.aspx">AFRICA VACATION RENTALS</a></h2>
                        <p class="PDescText">
                            <asp:Literal ID="ltlAfrica" runat="server" />
                        </p>
                        <br />
                    </center>
                   <h3> <div id="ctl00_Content_divAfrica" align="left" style="color: #202027;">
                        <asp:Literal runat="server" ID="ltlAfricaList"></asp:Literal>


                    </div></h3>
                </div>
                <br>
                <div style="border: solid 1px #ececec; background-color: White; padding: 2px;">
                    <center>
                        <h2><a class="mainRegion" href="http://www.vacations-abroad.com/asia/default.aspx">ASIA VACATION RENTALS</a></h2></center>
                    <p class="PDescText">
                        <asp:Literal ID="ltlAsia" runat="server" />
                    </p>
                    <br />
                   <h3> <div id="ctl00_Content_divAsia" align="left" style="color: #202027;">
                        <asp:Literal runat="server" ID="ltlAsiaList"></asp:Literal>
                    </div></h3>
                </div>
                <br>
                <div style="border: solid 1px #ececec; width: 100%; background-color: White;">
                    <center>
                        <h2><a class="mainRegion" href="http://www.vacations-abroad.com/europe/default.aspx">EUROPE VACATION RENTALS</a></h2></center>
                    <p class="PDescText">
                        <asp:Literal ID="ltlEurope" runat="server" />
                    </p>
                    <br />
                   <h3> <div align="left" style="color: #202027;">
                        <asp:Literal runat="server" ID="ltlEuropeList"></asp:Literal>
                    </div></h3>
                    <br />
                </div>
                <div style="border: solid 1px #ececec; background-color: White; padding: 2px;">
                    <center>
                        <h2><a class="mainRegion" href="http://www.vacations-abroad.com/north_america/default.aspx">NORTH AMERICA VACATION RENTALS</a></h2></center>
                    <p class="PDescText">
                        <asp:Literal ID="ltlNorthAmerica" runat="server" />
                    </p>
                    <br />
                    <h3><div id="ctl00_Content_divnAmerica" align="left" style="color: #202027;">
                        <asp:Literal runat="server" ID="ltlNorthAmericaList"></asp:Literal>
                    </div></h3>
                </div>
                <br />
                <div style="border: solid 1px #ececec; background-color: White; padding: 2px;">
                    <center>
                        <h2><a class="mainRegion" href="http://www.vacations-abroad.com/oceania/default.aspx">OCEANIA VACATION RENTALS</a></h2></center>
                    <p class="PDescText">
                        <asp:Literal ID="ltlOceania" runat="server" />
                    </p>
                    <br />
                    <h3><div id="ctl00_Content_divOceania" align="left" style="color: #202027;;">
                        <asp:Literal runat="server" ID="ltlOceaniaList"></asp:Literal>
                    </div></h3>
                </div>
                
                <br>
                <div style="border: solid 1px #ececec; background-color: White; padding: 2px;">
                    <center>
                        <h2><a class="mainRegion" href="http://www.vacations-abroad.com/south_america/default.aspx">SOUTH AMERICA VACATION RENTALS</a></h2></center>
                    <p class="PDescText">
                        <asp:Literal ID="ltlSouthAmerica" runat="server" />
                    </p>
                    <br />
                    <h3><div id="ctl00_Content_divsAmerica" align="left" style="color: #202027;">
                        <asp:Literal runat="server" ID="ltlSouthAmericaList"></asp:Literal>
                    </div></h3>
                    <br />
                </div>
                
                <br>
            </center>
        </div>
    </div>

    <%--<asp:Label ID="Title" runat="server" Visible="false" Text="Vacation Rentals, Hotels and B&Bs in Asia, Africa, Europe, North and South America, Oceania"></asp:Label>--%>
    <asp:Label ID="Title" runat="server" Visible="false" Text="Vacation Rentals, Hotels and B&Bs in Asia, Africa, Europe, North and South America, Oceania"></asp:Label>
    <asp:Label ID="Keywords" runat="server" Visible="false" Text="Vacations-Abroad: Vacation rentals, holiday villa rentals, vacation condo rentals, Budget self catering apartment rentals by owner"></asp:Label>
    <%--<asp:Label ID="Description" runat="server" Visible="false" Text="Vacations-Abroad.Vacation rentals, Luxury Holiday Villas, Budget Self Catering Apartments by owner"></asp:Label>--%>
    <asp:Label ID="Description" runat="server" Visible="false" Text="Vacations-Abroad. Tropical Beach Vacations in Asia & Oceania, Cultural Holiday Accommodations in Europe & the Americas, Exotic Holiday Rentals in Africa"></asp:Label>





    <br />

    <asp:Label ID="lblInfo" runat="server" ForeColor="Red"></asp:Label>
    </div>
    
   






<!-- Start of StatCounter Code for Default Guide -->
<script type="text/javascript">
var sc_project=3336280; 
var sc_invisible=1; 
var sc_security="510252c5"; 
</script>
<script type="text/javascript"
src="http://www.statcounter.com/counter/counter.js"></script>
<noscript><div class="statcounter"><a title="site stats"
href="http://statcounter.com/" target="_blank"><img
class="statcounter"
src="http://c.statcounter.com/3336280/0/510252c5/1/"
alt="site stats"></a></div></noscript>
<!-- End of StatCounter Code for Default Guide -->
<script type="text/javascript">

  var _gaq = _gaq || [];
  _gaq.push(['_setAccount', 'UA-1499424-2']);
  _gaq.push(['_setDomainName', 'vacations-abroad.com']);
  _gaq.push(['_setAllowLinker', true]);
  _gaq.push(['_trackPageview']);

  (function() {
    var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
    ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
    var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
  })();

</script>

</asp:Content>
