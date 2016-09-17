<%@ Page Language="C#" MasterPageFile="~/MasterPageNoCss.master" AutoEventWireup="true"
    CodeFile="~/CountiesList.aspx.cs" Inherits="StateProvinceList" Title="<%# GetTitle () %>"
    EnableEventValidation="false" %>

<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="Server">
    <asp:Label ID="Title" runat="server" Visible="false" Text="%county% Regional Vacation Rentals, B&Bs Cottages, Villas | Vacations Abroad"></asp:Label>
    <asp:Label ID="Keywords" runat="server" Visible="false" Text="%county% vacation rentals, %county% holiday rentals, %county% lodging, %county% accommodation, %county% villas, %county% apartments, %county% condos, %county% vacation homes, nightly rentals, weekly rentals, short term rentals "></asp:Label>
    <asp:Label ID="Description" runat="server" Visible="false" Text="Consider staying in one of our %county% regional vacation rentals for your next %county% holiday rental. Check out our directory of %county% % vacation rentals, villas, apartments and B&Bs."></asp:Label>
    <div align="center">
        <script type="text/javascript">
            $(document).ready(function () {
                var href = $(".lower").attr("href");
                href = href.toLowerCase().split(" ").join("_");
                $(".lower").attr("href", href);

                var hrefs = $(".lowers").attr("href");
                hrefs = hrefs.toLowerCase().split(" ").join("_");
                $(".lowers").attr("href", hrefs);
            });
        </script>
        <table class="StateCityTable listingContainerMain">
            <tr class="StateTable1Row1">
                <td colspan="100" class="StateTable1TD1">
                    <div class="listingPagesH1Container">
                        <h1 class="listingPagesH1Color">
                            <%# county.Replace("Province of ","") %> Region Vacation Rentals
                            <%--& holiday accommodations
                            <%# stateprovince %>
                            <%# country %>--%>
                        </h1>
                    </div>
                    <%--padding 305 center--%>
                    <script type="text/javascript" src="http://s7.addthis.com/js/250/addthis_widget.js#pub=natasha499"></script>
                    <div style="margin: 8px;">
                        <% if (AuthenticationManager.IfAuthenticated && AuthenticationManager.IfAdmin)
                           { %>
                        <asp:TextBox ID="txtCityText" runat="server" Rows="7" TextMode="MultiLine" Width="600px"></asp:TextBox><br />
                        <asp:Button ID="btnSubmit" runat="server" Text="Save Text" OnClick="btnSubmit_Click" />
                        <br />
                        <asp:Label ID="Label2" runat="server" EnableViewState="False" ForeColor="Red"></asp:Label>
                        <% }
                           else
                           {%>
                        <div class="LightText">
                            <asp:Label ID="lblcityInfo" runat="server"></asp:Label>
                        </div>
                        <% } %>
                    </div>
                </td>
            </tr>
            <tr class="VacationAndSubscribeTabs">
                <td>



                    <span>
                        <div id="divTab1" runat="server" class="tourTabs2 vacationsAndSubsTabs" style="font-size: 1.5em; font-weight: normal !important; float: right; text-align: right; display: none;">
                            <a class="subscribeLink" style="color: #a0522d;" rel="nofollow" href="http://eepurl.com/vac0P">Subscribe to
                                    our newsletter</a>
                        </div>
                    </span>
                </td>
            </tr>

            <tr>




                <td class="StateTable1TD2">
                    <asp:Label ID="lblInfo" runat="server" ForeColor="Red"></asp:Label>
                    <asp:SqlDataSource ID="Sql_properties" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                        SelectCommand="GetPropertiesFromCountyID" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:QueryStringParameter DefaultValue="" Name="CountyID" QueryStringField="CountyID"
                                Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <%--right cities column edit--%>
                    <div style="width: 100%">
                        <div id="breadcrumb">
                            <div class="breadcrumb2" style="position: relative; width: 600px; font-size: 10pt; font-weight: bold;">
                                <h2>
                                    <label style="color: #71a3af;">
                                        Properties in:
                                    </label>
                                    <a href="http://www.vacations-abroad.com/<%# country %>/default.aspx">
                                        <span style="font-style: normal;" class="tdNoSleeps"><%# country %></span>
                                    </a>,
                                            <a href="http://www.vacations-abroad.com/<%# country %>/<%# stateprovince %>/default.aspx">
                                                <span style="font-style: normal;" class="tdNoSleeps"><%# stateprovince %></span>
                                            </a>
                                </h2>
                            </div>
                        </div>
                        <div style="float: left; width: 82%">
                            <div class="lftProperties" style="border: none;">
                                <div class="PurpleTable">
                                    <%--<div id="filterBtn" style="float: right; width: 18%; text-align: center;">
                                    
                                </div>--%>
                                    <div id="filerMain" class="stepsContainer" style="float: left; width: 110%">
                                        <table style="padding: 0x; margin: 0px; border: 0px; width: 100%;" cellspacing="0">
                                            <tr class="step1Bg">
                                                <td class="flrLeft steps1-3 step1Bg" style="padding-top: 2px !important;">Step 1:
                                                </td>
                                                <td class="flr1" valign="top">
                                                    <asp:RadioButtonList ID="rdoTypes" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow"
                                                        AutoPostBack="True" OnSelectedIndexChanged="rdoTypes_SelectedIndexChanged" CellPadding="0"
                                                        CellSpacing="0" Height="14px">
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                        </table>
                                        <table style="padding: 0x; margin: 0px; border: 0px; width: 100%; margin-top: -2px; background-color: #e9e3d5;" cellspacing="0">
                                            <tr class="step2-3Height">
                                                <td class="flrLeft steps1-3 step2Bg">Step 2:
                                                </td>
                                                <td class="flr2 align-top step2Bg" style="width: 489px;">
                                                    <asp:RadioButtonList ID="rdoBedrooms" runat="server" RepeatDirection="Horizontal"
                                                        RepeatLayout="Flow" AutoPostBack="True" OnSelectedIndexChanged="rdoBedrooms_SelectedIndexChanged">
                                                        <asp:ListItem>0-2 Bedrooms</asp:ListItem>
                                                        <asp:ListItem>3-4 Bedrooms</asp:ListItem>
                                                        <asp:ListItem>5+ Bedrooms</asp:ListItem>
                                                        <asp:ListItem Selected="True">Display All</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                                <td rowspan="2" class="showMePropContainer">
                                                    <asp:Button ID="btnFilter" runat="server" Text="Search" OnClick="btnFilter_Click"
                                                        CssClass="WrapButtonText showMePropBox"
                                                        CausesValidation="False" />
                                                </td>
                                            </tr>
                                            <tr class="step3Bg step2-3Height">
                                                <td class="flrLeft steps1-3 step3Bg">Step 3:
                                                </td>
                                                <td class="flr3 step2DescWidth">
                                                    <asp:RadioButtonList ID="rdoFilter" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                        <asp:ListItem>Hot Tub</asp:ListItem>
                                                        <asp:ListItem>Internet</asp:ListItem>
                                                        <asp:ListItem>Pets</asp:ListItem>
                                                        <asp:ListItem>Pool</asp:ListItem>
                                                        <asp:ListItem Selected="True">Display All</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>

                                <div style="float: left; width: 100%">

                                    <asp:GridView ID="State_datagrid" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"
                                        GridLines="None" OnRowDataBound="State_datagrid_RowDataBound" CellPadding="1"
                                        ShowHeader="false" Width="120%" OnPageIndexChanging="State_datagrid_PageIndexChanging"
                                        OnPageIndexChanged="State_datagrid_PageIndexChanged" CssClass="propertiesRowsContainer">
                                        <FooterStyle CssClass="StateFooter" />
                                        <RowStyle CssClass="StateRow tableMainContainer" />
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <table style="border-spacing: 0px ! important;" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td valign="top" style="vertical-align: top; text-align: center; width: 110px" rowspan="2">
                                                                <a class="linkImgClickToView" href="<%# CommonFunctions.PrepareURL (((string)DataBinder.Eval(Container.DataItem, "Country", "{0}")).Replace (" ", "_").ToLower () + "/" + ((string)DataBinder.Eval(Container.DataItem, "StateProvince", "{0}")).Replace (" ", "_").ToLower () + "/" + ((string)DataBinder.Eval(Container.DataItem, "City", "{0}")).Replace (" ", "_").ToLower () + "/" + DataBinder.Eval(Container.DataItem, "ID", "{0}") + "/default.aspx") %>">
                                                                    <img src='<%# DataBinder.Eval(Container.DataItem, "PhotoImage", "http://www.vacations-abroad.com/images/TH{0}") %>'
                                                                        class="grdImg propImg propertyImg" width="140" height="115" border="2" alt="<%# DataBinder.Eval(Container.DataItem,"City","{0} ") %> <%# DataBinder.Eval(Container.DataItem,"category","{0} ") %> " />
                                                                    <div class="divClickToView clickToViewDiv grdImg">
                                                                        <p style="margin-top: 20px;">
                                                                            Click
                                                                        <br />
                                                                            To
                                                                        <br />
                                                                            View
                                                                        </p>
                                                                    </div>
                                                                </a>
                                                                <p class="namebelowthumbnail"><%# county %></p>
                                                            </td>
                                                            <%--start of edit--%>
                                                            <td valign="top" class="propertyDetails" style="text-align: left; width: 540px;">
                                                                <div style="min-height: 140px; float: left; width: 100%" class="propLink">
                                                                    <table width="100%" class="propLink" cellspacing="0" cellpadding="0">
                                                                        <tr>
                                                                            <td>
                                                                                <ul>
                                                                                    <li>
                                                                                        <h3 class="categoryTitle H3CityText">
                                                                                            <a href="<%# CommonFunctions.PrepareURL (((string)DataBinder.Eval(Container.DataItem, "Country", "{0}")).Replace (" ", "_").ToLower () + "/" + ((string)DataBinder.Eval(Container.DataItem, "StateProvince", "{0}")).Replace (" ", "_").ToLower () + "/" + ((string)DataBinder.Eval(Container.DataItem, "City", "{0}")).Replace (" ", "_").ToLower () + "/" + DataBinder.Eval(Container.DataItem, "ID", "{0}") + "/default.aspx") %>">
                                                                                                <asp:Label ID="Label2" class="tdNoSleeps" runat="server" Text='<%# String.Format("{0}{1}{2}{3}",Eval("city"),"&nbsp;",Eval("category"), ":&nbsp;")%>'></asp:Label>
                                                                                                <span class="tdNoSleeps">
                                                                                                    <asp:Label ID="BedNum" runat="server"><%# Eval("NumBedrooms")%></asp:Label>
                                                                                                    <asp:Label ID="Label4" runat="server">Bedroom</asp:Label>
                                                                                                    <asp:Label ID="BathNum" runat="server" Text='<%# Bind("NumBaths") %> '></asp:Label>
                                                                                                    <asp:Label ID="Label3" runat="server">BA Sleeps</asp:Label>
                                                                                                    <asp:Label ID="Label1" runat="server"><%# Eval("NumSleeps")%></asp:Label>
                                                                                                </span>
                                                                                            </a>
                                                                                        </h3>
                                                                                    </li>
                                                                                    <%--<li>
                                                                                    <center>
                                                                                        <asp:Label ID="lblPNRates" runat="server" CssClass="tdRentalRates"></asp:Label>
                                                                                        <asp:Label ID="BedNum" runat="server" CssClass="tdRentalRates" Text='<%# Bind("NumBedrooms") %>'></asp:Label>
                                                                                        <asp:Label ID="Label3" runat="server" CssClass="tdRentalRates" Text="Bedroom Sleeps"></asp:Label>
                                                                                        <asp:Label ID="Label4" runat="server" CssClass="tdRentalRates" Text='<%# Bind("NumSleeps") %>'></asp:Label>
                                                                                    </center>
                                                                                </li>--%>
                                                                                    <li>
                                                                                        <span class="tdRentalRates CityText">



                                                                                            <asp:Label ID="lblPNRatesCaption" class="tdRentalRatesBlue" runat="server">Rates:&nbsp;</asp:Label>
                                                                                            <asp:Label ID="lblPNRates" runat="server"></asp:Label>
                                                                                            <asp:Label ID="lblPNRatesCurrency" class="tdRentalRatesBlue" runat="server"></asp:Label>
                                                                                            <asp:Label ID="lblPNRatesBasis" runat="server"></asp:Label>
                                                                                            <asp:Label ID="lblMinimumNights" class="tdRentalRatesBlue" Style="text-transform: capitalize;" runat="server" Text='<%# Bind("MinimumNightlyRental") %>'></asp:Label>
                                                                                            <asp:Label ID="Label6" class="tdRentalRatesBlue" runat="server">Minimum</asp:Label>

                                                                                            <%--<%# PropertiesFullSet.Tables["Properties"].Rows[0]["MinimumNightlyRental"] %>--%>

                                                                                            <%--<asp:Label ID="lblseparator" runat="server" Text=' - '></asp:Label>-
                                                                                            --%>
                                                                                        </span>
                                                                                    </li>




                                                                                    <li>
                                                                                        <center class="tdNoSleeps">
                                                                                            <%--                                                                                        <asp:Label ID="BedNum" runat="server" Text='<%# Bind("NumBedrooms") %>'></asp:Label>
                                                                                        <asp:Label ID="Label3" runat="server" Text="Bedroom Sleeps"></asp:Label>
                                                                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("NumSleeps") %> - <%# Eval("Name","{0} ")  %>'></asp:Label>
                                                                                            --%>
                                                                                        </center>
                                                                                    </li>
                                                                                    <li id="liAmenity" class="amenities H4CityText" runat="server">
                                                                                        <h4>
                                                                                            <asp:Label ID="lblAmenitiesCaption" CssClass="amenitiesCaption" runat="server">Amenities: </asp:Label>
                                                                                            <asp:Label ID="lblAmenities" CssClass="amenities" runat="server"></asp:Label>
                                                                                        </h4>
                                                                                    </li>

                                                                                </ul>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                    <%--middle of edit--%>
                                                                </div>
                                                                <div style="float: right;">
                                                                    <center>
                                                                        <br />
                                                                        <div id="divContact">
                                                                            <%-- <a href="<%# CommonFunctions.PrepareURL("Property" + DataBinder.Eval(Container.DataItem, "ID", "{0}") + "/default.aspx") %>">
                                                                            <b>Contact Owner</b></a>--%>
                                                                        </div>
                                                                        <div id="divDetails">
                                                                            <%--<a href="<%# CommonFunctions.PrepareURL (((string)DataBinder.Eval(Container.DataItem, "Country", "{0}")).Replace (" ", "_").ToLower () + "/" + ((string)DataBinder.Eval(Container.DataItem, "StateProvince", "{0}")).Replace (" ", "_").ToLower () + "/" + ((string)DataBinder.Eval(Container.DataItem, "City", "{0}")).Replace (" ", "_").ToLower () + "/" + DataBinder.Eval(Container.DataItem, "ID", "{0}") + "/default.aspx") %>">
                                                                            <b>Details</b> </a>--%>
                                                                        </div>
                                                                        <div id="divCalendar" runat="server">
                                                                            <%-- <a href="<%# CommonFunctions.PrepareURL(((string)DataBinder.Eval(Container.DataItem, "Country", "{0}")).Replace (" ", "_").ToLower () + "/" + DataBinder.Eval(Container.DataItem, "ID", "{0}") + "/calendar.aspx") %>">
                                                                            <b>Calendar</b></a>--%>
                                                                        </div>
                                                                        <div>
                                                                            <%--<a href="<%# CommonFunctions.PrepareURL(DataBinder.Eval(Container.DataItem, "ID", "{0}") + "/newreview.aspx") %>">
                                                                            <b>Write Review</b></a>--%>
                                                                        </div>
                                                                        <div id="divWrite" runat="server">
                                                                            <%-- <a style="color: White;" rel="nofollow" href="<%# CommonFunctions.PrepareURL(DataBinder.Eval(Container.DataItem, "ID", "{0}") + "/reviews.aspx") %>">
                                                                            <b>Reviews</b></a>--%>
                                                                        </div>
                                                                    </center>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <%--end of edits--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="NumBedrooms" HeaderText="Sort rentals by Bedrooms" SortExpression="NumBedrooms"
                                                Visible="False" />
                                            <asp:ImageField ControlStyle-Width="500px" ControlStyle-Height="500px" DataImageUrlField="PhotoImage"
                                                DataAlternateTextField="Type" DataImageUrlFormatString="http://www.vacations-abroad.com/images/{0}"
                                                Visible="False">
                                                <ControlStyle Height="500px" Width="500px"></ControlStyle>
                                            </asp:ImageField>
                                            <asp:TemplateField HeaderText="Amenities" Visible="False">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("NumBedrooms") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    Sleeps
                                                <asp:Label ID="num_sleeps" runat="server" Text='<%# Eval("NumSleeps", "{0}") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField></asp:TemplateField>
                                        </Columns>
                                        <PagerStyle CssClass="StatePager" BackColor="#ede9ed" ForeColor="Black" />
                                        <EmptyDataTemplate>
                                            <br />
                                            <br />
                                            <center>
                                                No Propeties Found</center>
                                            <br />
                                        </EmptyDataTemplate>
                                        <SelectedRowStyle CssClass="StateSelected" />
                                        <HeaderStyle CssClass="StateHeader" />
                                        <EditRowStyle CssClass="StateEdit" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <div style="float: left; width: 18%">
                            <script async src="//pagead2.googlesyndication.com/pagead/js/adsbygoogle.js"></script>
                            <!-- Country Ads -->
                            <ins class="adsbygoogle"
                                style="display: inline-block; width: 160px; height: 600px"
                                data-ad-client="ca-pub-0264789273185284"
                                data-ad-slot="9499072355"></ins>
                            <script>
                                (adsbygoogle = window.adsbygoogle || []).push({});
                            </script>
                        </div>

                    </div>
                    <%--end right column edit--%>
                    <div class="OrangeText" style="text-align: left;">
                        <br />
                    </div>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <% if (AuthenticationManager.IfAuthenticated && AuthenticationManager.IfAdmin)
                       { %>
                    <center>
                        <asp:TextBox ID="txtCityText2" runat="server" Rows="7" TextMode="MultiLine" Width="600px"></asp:TextBox></center>
                    <br />
                    <center>
                        <asp:Button ID="btnSubmit2" runat="server" Text="Save Text" OnClick="btnSubmit2_Click" /></center>
                    <br />
                    <% } %>
                    <asp:Label ID="lblInfo2" runat="server" EnableViewState="False" CssClass="LightText"></asp:Label>
                    <br />
                    <asp:Label ID="lblInfo22" runat="server" ForeColor="Red"></asp:Label>
                    <br />
                </td>
            </tr>
        </table>
        <div class="listingContainerMain" style="border: none; box-shadow: none; margin-left: 0px !important;">
            <div class="rtHeader rightSideHeaders" id="rtHd3" runat="server" style="width: 917px; text-align: left; background-color: transparent !important; font-size: 10pt; font-weight: bold;">
                <%# county.Replace("Province of ","") %>
                                    Cities
            </div>

            <div id="divCitiesRt" runat="server" style="border: none; box-shadow: none; margin-left: 0px !important; font-size: 10pt; font-weight: bold; color: #2f6547;">
            </div>
        </div>
        <div class="listingContainerMain" style="border: none; box-shadow: none; margin-left: 0px !important;">

            <div class="rtHeader rightSideHeaders" id="Div5" runat="server" style="width: 917px; text-align: left; background-color: transparent !important; font-size: 10pt; font-weight: bold;">
                <%# stateprovince %>
                                    Regions
            </div>

            <div id="divCountiesRt" runat="server" style="border: none; box-shadow: none; margin-left: 0px !important; font-size: 10pt; font-weight: bold; color: #2f6547;">
            </div>

        </div>
        <div class="listingContainerMain" style="border: none; box-shadow: none; margin-left: 0px !important; ">
            <div class="rtHeader rightSideHeaders" id="Div3" runat="server" style="width: 917px; text-align: left; background-color: transparent !important; font-size: 10pt; font-weight: bold;">
                <%# country %>
                                    States
            </div>

            <div id="divStates" runat="server" style="border: none; box-shadow: none; margin-left: 0px !important; font-size: 10pt; font-weight: bold; color: #2f6547;">
            </div>
        </div>
    </div>
    <div>

        <p align="left" style="padding-top: 15px">
            <!-- Start of StatCounter Code -->
            We hope you enjoyed planning your
        <%# county %>
 Regional holiday rental our directory of
        <%# county %> Regional
        holiday accommodation
        </p>
        <br />
        <br />
        <br />
        <script type="text/javascript">
            sc_project = 3345780;
            sc_invisible = 1;
            sc_partition = 36;
            sc_security = "c7e8957f";
        </script>
        <script type="text/javascript">

            $(".propImg").mouseenter(function () {
                $(this).hide();
                $(this).siblings($('.clickToViewDiv')).show();
            });
            $('.clickToViewDiv').mouseleave(function () {
                $(".propImg").show();
                $(".propImg").siblings($('.clickToViewDiv')).hide();

            });
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
            } catch (err) { }</script>
    </div>
</asp:Content>
