<%@ Page Language="C#" MasterPageFile="~/MasterPageNoCss.master" AutoEventWireup="true"
    CodeFile="~/Country.aspx.cs" Inherits="Country"
    EnableEventValidation="false"  %>

<%--<%@ OutputCache Duration="600" VaryByParam="*" %>--%>
<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="Server">
    <asp:Label ID="Title" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="Keywords" runat="server" Visible="false" Text="%stateprovince% vacation rentals, %stateprovince% Hotels, %stateprovince% Cottages, %stateprovince% B&Bs, %stateprovince% villas , "></asp:Label>
    <asp:Label ID="Description" runat="server" Visible="false" Text="Relax and unwind in our %stateprovince% vacation rentals, B&Bs and boutique hotels in %country% "></asp:Label>
    <asp:TextBox runat="server" ID="txtCityVal" value="" Style="display: none;"></asp:TextBox>
    <asp:TextBox runat="server" ID="txtCityVal2" value="" Style="display: none;"></asp:TextBox>
    <%--    <asp:TextBox runat="server" ID ="txtCityVal"  value="" Style="display:none;" ></asp:TextBox>
    --%>
    <asp:HiddenField ID="cityList" runat="server"></asp:HiddenField>

    <input type="hidden" name="step1radio" value="" />
    <input type="hidden" name="step2radio" value="" />
    <input type="hidden" name="step3radio" value="" />
    <style>
        #map-canvas {
            height: 800px;
            width: 800px;
            margin-top: 25px;
        }

        hr {
            color: #738ea7;
        }

        .box {
            width: 170px;
            border: 2px solid #D9E7F1;
            height: 130px;
            margin: 3px;
        }
        
    </style>
    <script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>
    <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyC6v5-2uaq_wusHDktM9ILcqIrlPtnZgEk&sensor=false">
    </script>
    <script type="text/javascript">

        function initialize(markers) {


            var bounds = new google.maps.LatLngBounds();
            var mapOptions = {
                center: new google.maps.LatLng(markers[0].lat, markers[0].lng),
                styles: [{ "featureType": "landscape", "stylers": [{ "hue": "#F1FF00" }, { "saturation": -27.4 }, { "lightness": 9.4 }, { "gamma": 1 }] }, { "featureType": "road.highway", "stylers": [{ "hue": "#0099FF" }, { "saturation": -20 }, { "lightness": 36.4 }, { "gamma": 1 }] }, { "featureType": "road.arterial", "stylers": [{ "hue": "#00FF4F" }, { "saturation": 0 }, { "lightness": 0 }, { "gamma": 1 }] }, { "featureType": "road.local", "stylers": [{ "hue": "#FFB300" }, { "saturation": -38 }, { "lightness": 11.2 }, { "gamma": 1 }] }, { "featureType": "water", "stylers": [{ "hue": "#00B6FF" }, { "saturation": 4.2 }, { "lightness": -63.4 }, { "gamma": 1 }] }, { "featureType": "poi", "stylers": [{ "hue": "#9FFF00" }, { "saturation": 0 }, { "lightness": 0 }, { "gamma": 1 }] }],
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
                bounds.extend(marker.position);
                (function (marker, data) {

                    // Attaching a click event to the current marker
                    google.maps.event.addListener(marker, "click", function (e) {
                        window.open(data.URL);
                        // infoWindow.setContent(data.description);
                        //infoWindow.open(map, marker);
                    });
                })(marker, data);
            }
            map.fitBounds(bounds);
        }
    </script>
    


    <div align="center">
        <table class="StateCityTable listingContainerMain">
            <tr class="StateTable1Row1">
                <td>
                    <div style="position:relative;top:-80px;">
                        <h1 class="CountryListH1">
                            <asp:Literal ID="ltrH11" runat="server"></asp:Literal>
                            <br />
                        </h1>
                    </div>

                    <asp:Label ID="Label3" runat="server" EnableViewState="False" ForeColor="Red" Style="display: none"></asp:Label>
                    <%--<div>
                        
                        <div class="ContryTextbox" id="divHide123"  visible="true">
                            <asp:Label ID="lblCountryInfo" runat="server"></asp:Label>
                        </div>
                    </div>--%>
                    <div>
                        <% if (AuthenticationManager.IfAuthenticated && AuthenticationManager.IfAdmin)
                           { %>
                        <center><asp:Label ID="lblInfo" runat="server" ForeColor="Red"></asp:Label></center>
                        <asp:TextBox ID="txtCountryText" runat="server" Rows="7" TextMode="MultiLine" Width="600px"></asp:TextBox><br />
                        <asp:Button ID="btnSubmit" runat="server" Text="Save Text" OnClick="btnSubmit_Click" />
                        <br />
                        <asp:Label ID="Label1" runat="server" EnableViewState="False" ForeColor="Red"></asp:Label>
                        <% }
                           else
                           {%>
                        <div class="ContryTextbox" id="divHide123" runat="server" visible="true">
                            <asp:Label ID="lblCountryInfo" runat="server"></asp:Label>
                        </div>
                        <% } %>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="StateTable1TD2">
                    
                    <asp:SqlDataSource ID="Sql_properties" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                        SelectCommand="GetPropertiesFromStateID2" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:QueryStringParameter DefaultValue="25" Name="StateProvinceID" QueryStringField="StateProvinceID"
                                Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <%--right cities column edit--%>
                    <div style="width: 100%;">
                        <div style="float: right; margin-top: -23px;">
                        </div>
                    </div>
                    <hr />
                    <div class="lftPropertiesTbl" style="width: 637px !important;">
                        <table style="width: 637px;" cellspacing="0" border="0">
                            <tr class="VacationAndSubscribeTabs">
                                <td></td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="Left" style="padding-bottom: 10px;">
                                        <h1 class="CountryListH1">
                                            <%--<%= city %> Vacation Rentals--%>
                                            <asp:Literal ID="ltrH12" runat="server"></asp:Literal>

                                        </h1>
                                        <br />
                                        <span style="color: #E28C33 !important; font-size: 14px ; font-weight: bold;">Regions :</span>
                                        <asp:Literal ID="lbltText" runat="server" EnableViewState="false"></asp:Literal>
                                        <br />
                                    </div>

                                </td>
                            </tr>

                            <tr>
                                <td colspan="5">
                                    <div class="PurpleTable">

                                        <asp:Label ID="test123" runat="server" Style="display: none"></asp:Label>

                                        <br />
                                        <div id="map_canvas" style="float: left; width: 933px; height: 485px;margin-top:25px;"></div>

                                        <div style="clear: both;"></div>
                                        <div id="filerMain" runat="server">
                                            <br />
                                            <br />


                                            <asp:DataList ID="dtlStates" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" Width="100%" OnItemCommand="dtlStates_ItemCommand" OnItemDataBound="dtlStates_ItemDataBound" CellPadding="3" CellSpacing="2">
                                                <ItemTemplate>
                                                    <div class="box">
                                                        <asp:Label ID="lblCat" runat="server" Text='<%# Eval("category")%>' Visible="false"></asp:Label>
                                                        <asp:Image ID="imgProp" CssClass="grdImg propImg  propertyImg" Height="110px" Width="160px"
                                                            Style="border-width: 1px;" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "PhotoImage", "http://www.vacations-abroad.com/images/TH{0}").Trim() %>'
                                                            runat="server" />
                                                        <asp:HyperLink ID="hCategory" runat="server" CssClass="HyperLinkHover"><span class="CountryInternalLink" style="font-weight:bold;font-style:normal;font-size:12px">
                                                          <%=country %>&nbsp;<%#Eval("category") %>s (<%#Eval("Count") %>)
                                                           &nbsp;
                                                            </span>
                                                        </asp:HyperLink>


                                                    </div>
                                                </ItemTemplate>
                                            </asp:DataList>
                                        </div>
                                    </div>

                    </div>
                </td>
            </tr>
        </table>
    </div>
    <%--end right column edit--%>
    <div class="OrangeText" style="text-align: left; float: left;">
        <br />
    </div>
    </td> </tr>
    <tr>
        <td colspan="2" align="left">
            <div id="counrtyregions" runat="server" visible="false">
                <h2 class="CountryListH1"><%=country %> Regions</h2>
            </div>
            <br />
             <% if (AuthenticationManager.IfAuthenticated && AuthenticationManager.IfAdmin)
               { %>
            <asp:Label ID="lblerrormsg" runat="server" EnableViewState="False" ForeColor="Red" style="text-align:center;"></asp:Label>
            <br />
            <asp:TextBox ID="txtCountryText2" runat="server" Rows="7" TextMode="MultiLine" Width="600px"></asp:TextBox><br />
            <center>
                <asp:Button ID="btnSubmit2" runat="server" Text="Save Text" OnClick="btnSubmit2_Click" /></center>
            <br />
            <% } else { %>
            <asp:Label ID="lblInfo2" runat="server" EnableViewState="False" CssClass="ContryTextbox"></asp:Label>
            <%} %>

           
        </td>
    </tr>
    <tr>
        <td id="StateTable1TD3">
            <p align="left">

                <br />
                <br />
                <%--<div class="listingContainerMain" style="border: none; box-shadow: none; margin-left: 0px !important; border: 2px solid #ACA593;">--%>
                <div id="rtHd3" runat="server" style="width: 200px; text-align: left; color: #E28C33 !important; font-size: 14px; font-weight: bold;">
                </div>
                <div id="rtLow3" runat="server" class="Left" enableviewstate="false">
                </div>
                <%--</div>--%>
                <asp:Label ID="lblBr" runat="server" Text="<br/>"></asp:Label>
                <div class="listingContainerMain" id="rtCountyOut" runat="server" style="border: none; box-shadow: none; margin-left: 0px !important; border: 2px solid #ACA593;">
                    <div class="rtHeader rtHeader rightSideHeaders" id="rtCountiesHd" runat="server" style="width: 917px; text-align: left; background-color: #e9e3d5 !important; font-size: 10pt; font-weight: bold;">
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

                <asp:Label ID="lblInfo22" runat="server" ForeColor="Red" Style="display: none"></asp:Label>
        </td>
    </tr>
    </table>
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

</asp:Content>

