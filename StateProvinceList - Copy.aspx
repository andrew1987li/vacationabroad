<%@ Page Language="C#" MasterPageFile="~/MasterPageNoCss.master"
    AutoEventWireup="true" CodeFile="~/StateProvinceList - Copy.aspx.cs" Inherits="StateProvinceList"
    Title="<%# GetTitle () %>" EnableEventValidation="false" %>

<%--<%@ OutputCache Duration="600" VaryByParam="*" %>--%>
<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="Server">
    <asp:Label ID="Title" runat="server" Visible="false" Text="%stateprovince% %country% Vacation Rentals, %stateprovince% Vacation Home Rentals, %stateprovince% Holiday Accommodation"></asp:Label>
    <asp:Label ID="Keywords" runat="server" Visible="false" Text="%stateprovince% vacation rentals, %stateprovince% Hotels, %stateprovince% Cottages, %stateprovince% B&Bs, %stateprovince% villas , "></asp:Label>
    <asp:Label ID="Description" runat="server" Visible="false" Text="A great selection of unique %stateprovince% vacation rentals and boutique %stateprovince% properties for your next adventure in %stateprovince% ."></asp:Label>
    <asp:TextBox runat="server" ID="txtCityVal" value="" Style="display: none;"></asp:TextBox>
    <asp:TextBox runat="server" ID="txtCityVal2" value="" Style="display: none;"></asp:TextBox>
    <%--    <asp:TextBox runat="server" ID ="txtCityVal"  value="" Style="display:none;" ></asp:TextBox>
    --%>
    
    <script type="text/javascript">
        $(document).ready(function () {
            var href = $(".lower").attr("href");
            href = href.toLowerCase().split(" ").join("_");
            $(".lower").attr("href", href);

            var btnname = "input[name='ctl00$Content$btnSubmit']";
            var txtname = "input[name='ctl00$Content$txtCityText']";
            $(btnname).live('click', function () {
                var textcityvalue = $('#ctl00_Content_txtCityText').val().toString();
                $('#ctl00_Content_txtCityVal').val(textcityvalue);
                console.log($('#ctl00_Content_txtCityVal').val());
            });

            var btnname2 = "input[name='ctl00$Content$btnSubmit2']";
            var txtname2 = "input[name='ctl00$Content$txtCityText2']";
            $(btnname2).live('click', function () {
                var textcityvalue2 = $('#ctl00_Content_txtCityText2').val().toString();
                $('#ctl00_Content_txtCityVal2').val(textcityvalue2);
                console.log($('#ctl00_Content_txtCityVal2').val());
            });

            var value = "";

        });
    </script>
    <div align="center">
        <table class="StateCityTable listingContainerMain">
            <tr class="StateTable1Row1">
                <td class="StateTable1TD1">
                    <div class="listingPagesH1Container">
                        <asp:HyperLink ID="hyplnkBackLink" runat="server">
                                <asp:Literal ID="ltrBackText" runat="server"></asp:Literal>
                        </asp:HyperLink>
                        <div class="clear"></div>
                        <h1 class="listingPagesH1Color H1CityText" style="text-align: center">
                            <asp:Literal ID="ltrH1" runat="server"></asp:Literal>
                            <br />
                        </h1>
                    </div>
                    <%--<div class="LightText">
                        <div id="breadcrumb">
                            <div class="breadcrumb2" style="position: relative; font-size: 10pt; font-weight: bold;">
                                <h2>
                                    <asp:Literal ID="lbltText" runat="server"></asp:Literal>
                                </h2>
                            </div>
                        </div>

                    </div>--%>
                    <asp:Label ID="Label1" runat="server" EnableViewState="False" ForeColor="Red" Style="display: none"></asp:Label>

                    <div>
                        <% if (AuthenticationManager.IfAuthenticated && AuthenticationManager.IfAdmin)
                           { %>
                        <asp:TextBox ID="txtCityText" runat="server" Rows="7" TextMode="MultiLine" Width="600px"></asp:TextBox><br />
                        <asp:Button ID="btnSubmit" runat="server" Text="Save Text" OnClick="btnSubmit_Click" />
                        <br />
                        <asp:Label ID="Label9" runat="server" EnableViewState="False" ForeColor="Red"></asp:Label>
                        <% }
                           else
                           {%>
                        <div class="LightText" id="divHide123" runat="server">
                            <asp:Label ID="lblcityInfo" runat="server"></asp:Label>
                        </div>


                        <% } %>

                        <div class="countrymap" id="googlemap" runat="server">
                        </div>
                    </div>
                </td>
            </tr>
            <tr style="display: none;">
                <td class="StateTable1TD2">
                    <asp:Label ID="lblInfo" runat="server" ForeColor="Red"></asp:Label>
                    <asp:SqlDataSource ID="Sql_properties" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                        SelectCommand="GetPropertiesFromStateID2" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:QueryStringParameter DefaultValue="25" Name="StateProvinceID" QueryStringField="StateProvinceID"
                                Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <%--right cities column edit--%>

                </td>
            </tr>
        </table>
        <div class="heding_box" style="text-align: center">

            <h2>
                <asp:Literal ID="ltrHeading" runat="server"></asp:Literal></h2>

        </div>

        <div class="country_box">
            <ul id="ulManiGrid" runat="server">
            </ul>
        </div>
        <div class="custm-content">

            <div class="row">
                <%--<div class="col-50">
     
     	<div class="sub-title blue">
        
        	<asp:Literal ID="lrtPropertyHeading" runat="server"></asp:Literal>
        
        </div>
        
        <div class="properties_box">
            
            	<ul>
                    <asp:Repeater ID="dtlStates" runat="server" OnItemDataBound="dtlStates_ItemDataBound1" OnItemCommand="dtlStates_ItemCommand1">
                                    <ItemTemplate>
                                        <li>
                                            <asp:Label ID="lblCat" runat="server" Text='<%# Eval("category")%>' Visible="false"></asp:Label>
                                                    <asp:HyperLink ID="hCategory" runat="server" cssClass="HyperLinkHover">
                                                          <%=stateprovince %>&nbsp;<%#Eval("category") %>s (<%#Eval("Count") %>)
                                                           &nbsp;
                                                    </asp:HyperLink>
                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                
                </ul>
            
            </div>
     
     </div>--%>
                <div class="col-100">

                    <div class="sub-title test orange" visible="true" id="OrangeTitle" runat="server">
                        <h2>Things to see and do in
                        <asp:Literal ID="ltrStateThing" runat="server"></asp:Literal></h2>

                    </div>

                    <% if (AuthenticationManager.IfAuthenticated && AuthenticationManager.IfAdmin)
               { %>
                    <asp:TextBox ID="txtCityText2" runat="server" Rows="7" TextMode="MultiLine" Width="600px"></asp:TextBox><br />
                    <center>
                        <asp:Button ID="btnSubmit2" runat="server" Text="Save Text" OnClick="btnSubmit2_Click" /></center>
                    <br />
                    <% } %>
                    <p>
                        <asp:Label ID="lblInfo2" runat="server" EnableViewState="False" CssClass="LightTextLower"></asp:Label></p>

                </div>
            </div>

        </div>

        <div class="country_list_box">

            <ul>
                <li>
                    <div id="rtHd3" runat="server" style="display: inline"></div>
                </li>
                <asp:Literal ID="rtLow3" runat="server"></asp:Literal>
            </ul>
            <br />
            <br />
            <ul>
                <li>
                    <div id="rtCountiesHd" runat="server" style="display: inline;"></div>
                </li>
                <asp:Literal ID="divCitiesRt" runat="server"></asp:Literal>
            </ul>

        </div>
    </div>
    <%--end right column edit--%>
    <div class="OrangeText" style="text-align: left; float: left;">
        <br />
    </div>

    <asp:Label ID="lblInfo22" runat="server" ForeColor="Red" Style="display: none"></asp:Label>
    <!-- Start of StatCounter Code -->
    <script type="text/javascript">
        sc_project = 3345780;
        sc_invisible = 1;
        sc_partition = 36;
        sc_security = "c7e8957f";
    </script>

    <script type="text/javascript" src="http://www.statcounter.com/counter/counter.js"></script>
    <noscript>
        <div class="statcounter">
            <a title="web counter" href="http://www.statcounter.com/free_hit_counter.html" target="_blank">
                <img class="statcounter" src="http://c37.statcounter.com/3345780/0/c7e8957f/1/" alt="web counter"></a>
        </div>
    </noscript>
    <!-- End of StatCounter Code -->
    <!-- End counter code -->
    <script type="text/javascript">
        var gaJsHost = (("https:" == document.location.protocol) ? "https://ssl." : "http://www.");
        document.write(unescape("%3Cscript src='" + gaJsHost + "google-analytics.com/ga.js' type='text/javascript'%3E%3C/script%3E"));
    </script>
    <script type="text/javascript">
        try {
            var pageTracker = _gat._getTracker("UA-1499424-2");
            pageTracker._trackPageview();
        } catch (err) { }
    </script>
</asp:Content>
