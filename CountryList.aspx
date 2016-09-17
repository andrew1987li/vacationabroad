<%@ Page Language="C#" MasterPageFile="~/MasterPageNoCss.master" AutoEventWireup="true"
    CodeFile="~/countrylist.aspx.cs" Inherits="CountryList"
    EnableEventValidation="false" EnableViewState="true" %>

<%@ OutputCache Duration="600" VaryByParam="*" %>
<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="Server">
    <div>
        <asp:Label ID="Title" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="Keywords" runat="server" Visible="false" Text="%stateprovince% vacation rentals, %stateprovince% Hotels, %stateprovince% Cottages, %stateprovince% B&Bs, %stateprovince% villas , "></asp:Label>
        <asp:Label ID="Description" runat="server" Visible="false" Text="Relax and unwind in our %stateprovince% vacation rentals, B&Bs and boutique hotels in %country% "></asp:Label>
        <asp:TextBox runat="server" ID="txtCityVal" value="" Style="display: none;"></asp:TextBox>
        <asp:TextBox runat="server" ID="txtCityVal2" value="" Style="display: none;"></asp:TextBox>
        <%--    <asp:TextBox runat="server" ID ="txtCityVal"  value="" Style="display:none;" ></asp:TextBox>
        --%>
        <script type="text/javascript">
            function initialize(markers) {
                var mapOptions = {
                    center: new google.maps.LatLng(25, 80),
                    zoom: 5,
                    mapTypeId: google.maps.MapTypeId.ROADMAP
                    //  marker:true
                };
                var infoWindow = new google.maps.InfoWindow();
                var map = new google.maps.Map(document.getElementById("map_canvas"), mapOptions);
                for (i = 0; i < markers.length; i++) {
                    var data = markers[i]
                    var myLatlng = new google.maps.LatLng(data.lat, data.lng);
                    var marker = new google.maps.Marker({
                        position: myLatlng,
                        map: map,
                        title: data.title
                    });
                    (function (marker, data) {

                        // Attaching a click event to the current marker
                        google.maps.event.addListener(marker, "click", function (e) {
                            infoWindow.setContent(data.description);
                            infoWindow.open(map, marker);
                        });
                    })(marker, data);
                }
            }
        </script>
        <script type="text/javascript">
            $(document).ready(function () {
                var href = $(".lower").attr("href");
                href = href.toLowerCase().split(" ").join("_");
                $(".lower").attr("href", href);

                var btnname = "input[name='ctl00$Content$btnSubmit']";
                var txtname = "input[name='ctl00$Content$txtCountryText']";
                $(btnname).live('click', function () {
                    var textcityvalue = $('#ctl00_Content_txtCountryText').val().toString();
                    $('#ctl00_Content_txtCityVal').val(textcityvalue);
                    console.log($('#ctl00_Content_txtCityVal').val());
                });

                var btnname2 = "input[name='ctl00$Content$btnSubmit2']";
                var txtname2 = "input[name='ctl00$Content$txtCountryText2']";
                $(btnname2).live('click', function () {
                    var textcityvalue2 = $('#ctl00_Content_txtCountryText2').val().toString();
                    $('#ctl00_Content_txtCityVal2').val(textcityvalue2);
                    console.log($('#ctl00_Content_txtCityVal2').val());
                });

                var value = "";

            });
        </script>
        
    <style type="text/css">
            .listingPagesContainer a {
    float: left;
    font-size: 1.3em;
    color: #6699FF;
}

    .listingPagesContainer a:visited {
        color: #6699FF;
    }

    .listingPagesContainer a:hover {
        color: #6699FF;
    }

            a:link {
    color: #6699FF;
}
            a:hover {
    color: #6699FF;
}a:visited {
    color: #6699FF;
    text-decoration:underline;
}
        </style>
        

        <div align="center">
        <table class="StateCityTable listingContainerMain">
            <tr class="StateTable1Row1">
                <td class="StateTable1TD1">
                    <div class="listingPagesH1Container">
                        <asp:HyperLink ID="hyplnkBackLink" runat="server"><h3><asp:Literal ID="ltrBackText" runat="server"></asp:Literal></h3></asp:HyperLink>
                        <div class="clear"></div>
                        <h1 class="listingPagesH1Color H1CityText">
                            <%--<%= city %> Vacation Rentals--%>
                            <asp:Literal ID="ltrH11" runat="server"></asp:Literal>
                            <br />
                        </h1>
                    </div>
                        <asp:Label ID="Label3" runat="server" EnableViewState="False" ForeColor="Red" Style="display: none"></asp:Label>
                        <div>
                            <% if (AuthenticationManager.IfAuthenticated && AuthenticationManager.IfAdmin)
                           { %>
                            <asp:TextBox ID="txtCountryText" runat="server" Rows="7" TextMode="MultiLine" Width="600px"></asp:TextBox><br />
                            <asp:Button ID="btnSubmit" runat="server" Text="Save Text" OnClick="btnSubmit_Click" />
                            <br />
                            <asp:Label ID="Label1" runat="server" EnableViewState="False" ForeColor="Red"></asp:Label>
                            <% }
                           else
                           {%>
                            <div class="LightText" id="divHide123" runat="server" visible="true">
                                <asp:Label ID="lblCountryInfo" runat="server"></asp:Label>
                            </div>
                            <div class="countrymap" id="googleCountrymap" runat="server">
                            </div>

                            <% } %>
                        </div>
                    </td>
                </tr>
                <tr style="display:none;">
                    <td class="StateTable1TD2">
                        <asp:Label ID="lblInfo" runat="server" ForeColor="Red"></asp:Label>
                        <asp:SqlDataSource ID="Sql_properties" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                            SelectCommand="GetPropertiesFromStateID2" SelectCommandType="StoredProcedure">
                            <SelectParameters>
                                <asp:QueryStringParameter DefaultValue="25" Name="StateProvinceID" QueryStringField="StateProvinceID"
                                    Type="Int32" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                        <div style="width: 100%;">
                            <div style="float: right; margin-top: -23px;">

                                <div id="divTab1" runat="server" class="tourTabs2 vacationsAndSubsTabs" style="display: none; height: 10px !important; margin-bottom: -33px; margin-top: 15px; font-style: inherit; font-size: 1.5em;">
                                    <a class="subscribeLink" style="color: #a0522d;" rel="nofollow" href="http://eepurl.com/vac0P">Subscribe to
                                                     newsletter</a>
                                </div>


                                <br />

                            </div>
                        </div>
                        <div class="lftPropertiesTbl" style="width: 637px !important;">
                            <table style="width: 637px;" cellspacing="0" border="0">
                                <tr class="VacationAndSubscribeTabs">
                                    <td>

                                        <span>
                                            <div id="div1" runat="server" class="tourTabs2 vacationsAndSubsTabs" style="font-size: 1.5em; font-weight: normal !important; float: right; text-align: right; display: none;">
                                                <a class="subscribeLink" style="color: #a0522d;" rel="nofollow" href="http://eepurl.com/vac0P">Subscribe to our newsletter</a>
                                            </div>
                                        </span>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="properties">
                                            <div class="property"></div>
                                            <div class="ttsee"></div>
                                        </div>
                                    </td>
                                </tr>
                            </table>

                        </div>
                    </td>
                </tr>
            </table>

            
            <table class="heding_box" align="left">
<tr><td>
                <h2><asp:Literal ID="ltrHeading" runat="server"></asp:Literal></h2>
                </td>
    <td><div  style="font-size: 1.3em;
    color: #6699FF; font-family: Arial, Helvetica, sans-serif; float: inherit">
        <asp:HyperLink ID="hyplnkAllProps" Text="AllProperties" runat="server"><h3><asp:Literal ID="ltrAllProps" runat="server"></asp:Literal></h3></asp:HyperLink>
            </div></td></tr>
            </table>
                        
            
            <div class="country_box">
                <ul id="Statesul" runat="server">
                    <%--<asp:Repeater ID="rptrStates" runat="server">
                        <ItemTemplate>
                            <li>
                                <h3>Dorset</h3>
                                <a href="#">
                                    <img src="http://www.vacations-abroad.com/images/THproperty00008527photo0005.JPG" alt="" /></a>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>--%>
                </ul>
            </div>

            <div class="custm-content">

                <div class="row">
                    <%--<div class="col-50">

                        <div class="sub-title blue">
                            <asp:Literal ID="ltrH12" runat="server"></asp:Literal>
        
                        </div>

                        <div class="properties_box">

                            <ul>
                                <asp:Repeater ID="dtlStates" runat="server" OnItemDataBound="dtlStates_ItemDataBound1" OnItemCommand="dtlStates_ItemCommand1">
                                    <ItemTemplate>
                                        <li>
                                            <asp:Label ID="lblCat" runat="server" Text='<%# Eval("category")%>' Visible="false"></asp:Label>
                                            <asp:HyperLink ID="hCategory" runat="server" CssClass="HyperLinkHover">
                                                          <%=country %>&nbsp;<%#Eval("category") %>s (<%#Eval("Count") %>)
                                                           &nbsp;
                                            </asp:HyperLink>
                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <asp:DataList ID="dtlStates1" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" Width="100%" OnItemCommand="dtlStates_ItemCommand" OnItemDataBound="dtlStates_ItemDataBound" CellPadding="3" CellSpacing="2">
                                    <ItemTemplate>
                                    </ItemTemplate>
                                </asp:DataList>

                            </ul>

                        </div>

                    </div>--%>
                    <div class="col-100">

                        <div class="sub-title orange" id="OrangeTitle" runat="server">
                            Things to see and do in <asp:Literal ID="ltrCountryThing" runat ="server"></asp:Literal>
                        </div>
                         <% if (AuthenticationManager.IfAuthenticated && AuthenticationManager.IfAdmin)
               { %>
                    <asp:TextBox ID="txtCountryText2" runat="server" Rows="7" TextMode="MultiLine" Width="600px"></asp:TextBox><br />
                    <center>
                        <asp:Button ID="btnSubmit2" runat="server" Text="Save Text" OnClick="btnSubmit2_Click" /></center>
                    <br />
                    <% } %>
                    <p><asp:Label ID="lblInfo2" runat="server" EnableViewState="False" CssClass="LightTextLower"></asp:Label>
                        </p>

                    </div>
                </div>

            </div>

            <div class="country_list_box">

                <ul>
                    <li><div id="rtHd3" runat="server" style="display:inline;"></div></li>
                    <asp:Literal id="rtLow3" runat="server">
                            </asp:Literal>

                </ul>

            </div>

        </div>
        <%--end right column edit--%>
        <%--<div class="OrangeText" style="text-align: left; float: left;">
            <br />
        </div>--%>
        <%--<table>
            <tr>
                <td colspan="2" align="left">
                    <br />
                   
                </td>
            </tr>
            <tr>
                <td id="StateTable1TD3">
                    <p align="left">
                        <br />
                        <br />
                        <br />
                        <div class="listingContainerMain" style="border: none; box-shadow: none; margin-left: 0px !important;">
                            
                            </div>
                            
                        <asp:Label ID="lblBr" runat="server" Text="<br/>"></asp:Label>
                        <div class="listingContainerMain" id="rtCountyOut" runat="server" style="border: none; box-shadow: none; margin-left: 0px !important;">
                            <div class="rtHeader rtHeader rightSideHeaders" id="rtCountiesHd" runat="server" style="width: 917px; text-align: left; background-color: transparent !important; font-size: 10pt; font-weight: bold;">
                            </div>
                            <div id="divCitiesRt" runat="server" class="rtText" style="border: none; width: 917px; box-shadow: none; margin-left: 0px !important; font-size: 10pt; font-weight: bold; color: #2f6547;">
                            </div>

                            <br />
                            <div class="rtOuterCnty" style="display: none;">
                                <div class="rtHeader rtHeader rightSideHeaders" id="rtLowerHd" runat="server">
                                </div>
                                <div id="rtLower" runat="server" class="rtText">
                                </div>
                            </div>

                        </div>

                        
                </td>
            </tr>
        </table>--%>
        <asp:Label ID="lblInfo22" runat="server" ForeColor="Red" Style="display: none"></asp:Label>
        <!-- Start of StatCounter Code for Default Guide -->
        <script type="text/javascript">
            var sc_project = 3341533;
            var sc_invisible = 1;
            var sc_security = "ebe10c56";
        </script>
        <script type="text/javascript"
            src="http://www.statcounter.com/counter/counter.js"></script>
        <noscript>
            <div class="statcounter">
                <a title="web counter"
                    href="http://statcounter.com/free-hit-counter/"
                    target="_blank">
                    <img class="statcounter"
                        src="http://c.statcounter.com/3341533/0/ebe10c56/1/"
                        alt="web counter"></a>
            </div>
        </noscript>
        <!-- End of StatCounter Code for Default Guide -->
        <!-- End counter code -->
        <script type="text/javascript">
            var gaJsHost = (("https:" == document.location.protocol) ? "https://ssl." : "http://www.");
            document.write(unescape("%3Cscript src='" + gaJsHost + "google-analytics.com/ga.js' type='text/javascript'%3E%3C/script%3E"));
        </script>
        <script type="text/javascript">
            try {
                var pageTracker = _gat._getTracker("UA-1499424-2");
                pageTracker._trackPageview();
            } catch (err) { }</script>
    </div>
</asp:Content>

