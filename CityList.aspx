<%@ Page Language="C#" MasterPageFile="~/masterpage/NormalMaster.master" AutoEventWireup="true"
    CodeFile="CityList.aspx.cs" Inherits="newCityList" EnableEventValidation="false" %>

<asp:Content ID="links" ContentPlaceHolderID="links" runat="server">
    <link href="/Assets/css/citylist.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="bodycontent" runat="Server">
    <asp:Label ID="Label2" runat="server" Visible="false" Text="%city% %stateprovinc% vacation Rentals, %city% %country% Holiday Rentals, %city% Rental Accommodations"></asp:Label>
    <asp:Label ID="Title" runat="server" Visible="false" Text="%city% %country% Vacation Rentals, %city% Villas, %city% Condos, Apartments, Hotels, Cottages"></asp:Label>
    <asp:Label ID="Keywords" runat="server" Visible="false" Text="%city% vacation rentals, %city% vacations, %city% %stateprovince% vacation rentals,   villas, cottages, boutique hotels"></asp:Label>
    <asp:Label ID="Description" runat="server" Visible="false" Text="Enjoy %city% vacations while relaxing in %city% vacation rentals, boutique hotels direct from owner in %stateprovince% %country%"></asp:Label>
    <asp:TextBox runat="server" ID="txtCityVal" value="" Style="display: none;"></asp:TextBox>
    <asp:TextBox runat="server" ID="txtCityVal2" value="" Style="display: none;"></asp:TextBox>
    <input type="hidden" name="step1radio" value="" />
    <input type="hidden" name="step2radio" value="" />
    <input type="hidden" name="step3radio" value="" />

    <div class="row">
         <span id="test234" runat="server" style="display: none"></span>
        <div class="internalpagewidth">
            <div class="row">
                    <div class="text-left topMargin">
                        <asp:HyperLink ID="hyperRegion" runat="server"><h3 class="backitem"><asp:Literal ID="ltrRegion" runat="server"></asp:Literal></h3></asp:HyperLink>
                        <asp:HyperLink ID="hyplnkCountryBackLink" runat="server"><h3 class="backitem"><asp:Literal ID="ltrCountryBackText" runat="server"></asp:Literal></h3></asp:HyperLink>
                        <asp:HyperLink ID="hyplnkStateBackLink" runat="server"><h3 class="backitem"><asp:Literal ID="ltrStateBackText" runat="server"></asp:Literal></h3></asp:HyperLink>
                        <div class="clear"></div>

                    </div>
                <div class="text-center">
                        <h1 class="H1CityText">
                            <%--<%= city %> Vacation Rentals--%>
                            <asp:Literal ID="ltrH11" runat="server"></asp:Literal>
                            <br />
                        </h1>
                </div>

            </div>
            <div class="row">
                 <div class="row">
                        <asp:Label runat="server" ID="lblcity" Font-Size="Large"></asp:Label>
                  </div>


    
                    <%--padding 305 center--%>
                    <div class="row">
                        <% if (AuthenticationManager.IfAuthenticated && AuthenticationManager.IfAdmin)
                           { %>
                        <asp:TextBox ID="txtCityText" runat="server" Rows="7" TextMode="MultiLine" Width="600px"></asp:TextBox><br />
                        <asp:Button ID="btnSubmit" runat="server" Text="Save Text" OnClick="btnSubmit_Click" />
                        <br />
                        <asp:Label ID="lblInfo" runat="server" EnableViewState="False" ForeColor="Red"></asp:Label>
                        <% }
                           else
                           {%>
                                         
					<% if(string.IsNullOrEmpty(Convert.ToString(Request.QueryString["category"]))){ %> 
					        <div >
                                             <asp:Label ID="lblcityInfo" CssClass="txtalign" runat="server"></asp:Label>
                                 </div>
					<% } %>
                        <% } %>
                    </div>

                    

                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:herefordpiesConnectionString1 %>"
                        SelectCommand="GetPropertiesFromCityIDplay" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:QueryStringParameter DefaultValue="1849" Name="CityID" QueryStringField="CityID"
                                Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                          <div class="row">
                            <div id="divTab1" runat="server" class="tourTabs2 vacationsAndSubsTabs" style="display: none;
                                height: 10px !important; margin-bottom: -33px; margin-top: 15px; font-style: inherit;
                                font-size: 1.5em;">
                                <a class="subscribeLink" style="color: #a0522d;" rel="nofollow" href="http://eepurl.com/vac0P">
                                    Subscribe to newsletter</a>
                            </div>
                            <br />
                       
                            <div id="div1" runat="server" class="tourTabs2 vacationsAndSubsTabs" style="font-size: 1.5em;
                                font-weight: normal !important; float: right; text-align: right; display: none;">
                                <a class="subscribeLink" style="color: #a0522d;" rel="nofollow" href="http://eepurl.com/vac0P">
                                    Subscribe to newsletter </a>
                            </div>

                        </div>
                <div class="row">

                        <div style="padding-bottom: 10px;margin-left:20px;">
                            <div class="heding_box">
                                <h2 class="listingPagesH1Color">
                                    <%--<%= city %> Vacation Rentals--%>
                                    <asp:Literal ID="ltrH12" runat="server"></asp:Literal>
                                    <br />
                                </h2>
                            </div>
                        </div>
                </div>
                                                        <div id="breadcrumb" style="display:none;">
                                            <div class="breadcrumb2" style="position: relative; width: 600px; font-size: 10pt;
                                                font-weight: bold;">
                                                    <label id="lblBreadcrumb" runat="server" style="color: #71a3af;">
                                                        Vacation Rentals in:
                                                    </label>
                                                    <asp:Literal ID="lbltText" runat="server"></asp:Literal>
                                            </div>
                                        </div>
                <div class="row">
                                            <div id="Div2" style="float: left; width: 900px; border: 2px solid #c4d9e2" runat="server">
                                                <asp:DataList ID="dtlStates" runat="server" RepeatColumns="5" RepeatDirection="Horizontal"
                                                    Width="100%" OnItemCommand="dtlStates_ItemCommand" OnItemDataBound="dtlStates_ItemDataBound"
                                                    CellPadding="3" CellSpacing="2">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCat" runat="server" Text='<%# Eval("category")%>' Visible="false"></asp:Label>
                                                        <asp:HyperLink ID="hCategory" runat="server" cssClass="HyperLinkHover"><span class="tdNoSleeps" style="font-weight:normal;font-style:normal;font-size:12px">
                                                          <%=city %>&nbsp;<%#Eval("category")%>s (<%#Eval("count")%>)
                                                           &nbsp;
                                                        </span>
                                                        </asp:HyperLink>
                                                    </ItemTemplate>
                                                </asp:DataList>
                                            </div>
                                             <asp:RadioButtonList ID="rdoTypes" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow"
                                                                AutoPostBack="True" OnSelectedIndexChanged="rdoTypes_SelectedIndexChanged" CellPadding="0"
                                                                CellSpacing="0" Height="14px" ViewStateMode="Enabled" CssClass="test">
                                             </asp:RadioButtonList>
                </div>
               <div class="row">
        
                   <div class="tableborder">
                    <div class="row">
                        <div class="col-md-12 col-sm-12">
                 <input type="hidden" id="selectedRdoTypes" runat="server" />
                                        <label >Step 1:</label>

                                                            <asp:RadioButtonList ID="rdoBedrooms" runat="server" RepeatDirection="Horizontal"
                                                                RepeatLayout="Flow" AutoPostBack="True" OnSelectedIndexChanged="rdoBedrooms_SelectedIndexChanged">
                                                                <asp:ListItem>0-2 Bedrooms</asp:ListItem>
                                                                <asp:ListItem>3-4 Bedrooms</asp:ListItem>
                                                                <asp:ListItem>5+ Bedrooms</asp:ListItem>
                                                                <asp:ListItem Selected="True">Display</asp:ListItem>
                                                            </asp:RadioButtonList>
                        </div>
       
  
                    </div>
                    <div class="row">
                        <div class="col-md-8 col-sm-8">
                            <label >Step 2:</label>   

                                                            <asp:RadioButtonList ID="rdoFilter" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                                <asp:ListItem>Hot Tub</asp:ListItem>
                                                                <asp:ListItem>Internet</asp:ListItem>
                                                                <asp:ListItem>Pets</asp:ListItem>
                                                                <asp:ListItem>Pool</asp:ListItem>
                                                                <asp:ListItem Selected="True">Display All</asp:ListItem>
                                                            </asp:RadioButtonList>
                        </div>
  
                        <div class="col-md-2 col-sm-2 ">
                            <div class="text-right">
                                <asp:Button ID="btnFilter"  runat="server" Text="Search" 
                                                                OnClick="btnFilter_Click" CssClass="btnsigns" CausesValidation="False"
                                                                OnClientClick="$('#ctl00_Content_selectedRdoTypes').val($('.test').find('input:checked')[0].value)" />

                            </div>
                            
                        </div>
                    </div>
                   </div>

    
       
  
                                        </div>
                </div>


                <div class="row">
                                           <div style="float: left; width: 100%">
                                                <div style="float: left; width: 82%">
                                                    <asp:GridView ID="City_datagrid" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"
                                                        GridLines="None" OnRowDataBound="City_datagrid_RowDataBound" CellPadding="1" 
                                                        ShowHeader="false" Width="100%" OnPageIndexChanging="City_datagrid_PageIndexChanging"
                                                        OnPageIndexChanged="City_datagrid_PageIndexChanged"  >
                                                        <FooterStyle CssClass="StateFooter" />
                                                        <RowStyle CssClass="StateRow tableMainContainer" />
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <table>
                                                                        <asp:Panel ID="test" runat="server">
                                                                            <tr>
                                                                                <td align="center" colspan="2">
                                                                                    <h2 style="font-size: 14pt; color: #050505">
                                                                                        <b>
                                                                                        <% if (Request.QueryString["category"] != null){ %>
                                                                                            <asp:Label ID="Label1" runat="server"><%#Eval("City") %><sp/> <%#Eval("Category")%>s</asp:Label></b></h2>
                                                                                            <%} else{%>
                                                                                            <asp:Label ID="lblHeader" runat="server"><%#Eval("City") %><sp/> <%#Eval("Category")%>s</asp:Label></b></h2>
                                                                                       <%} %>
                                                                                        <asp:Label ID="lblCategoryNew" runat="server" Style="display: none" Text='<%#Eval("Category")%>'></asp:Label>
                                                                                       <asp:Label ID="lblSubCategory" runat="server" Style="display: none" Text='<%#Eval("SubCategory")%>'></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                        </asp:Panel>
                                                                        <tr>
                                                                            <td valign="top" class="propertyThumbnail" style="vertical-align: top; text-align: center;
                                                                                width: 110px" rowspan="1">
                                                                                <a href="<%# CommonFunctions.PrepareURL (((string)DataBinder.Eval(Container.DataItem, "Country", "{0}")).Replace (" ", "_").ToLower () + "/" + ((string)DataBinder.Eval(Container.DataItem, "StateProvince", "{0}")).Replace (" ", "_").ToLower () + "/" + ((string)DataBinder.Eval(Container.DataItem, "City", "{0}")).Replace (" ", "_").ToLower () + "/" + DataBinder.Eval(Container.DataItem, "ID", "{0}") + "/default.aspx") %>">
                                                                                    <asp:Image ID="imgProp" CssClass="imgstyle" Width="150px" Height="125px"
                                                                                        Style="border-width: 1px;" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "PhotoImage", "http://www.vacations-abroad.com/images/{0}").Trim() %>'
                                                                                        runat="server" alt='<%#String.Format("{0}{1}{2}{3}{4}",Eval("Type"),"","","","") %>' />
                                                                                    <%--runat  ="server" alt='<%#String.Format("{0}{1}{2}{3}{4}{5}","Boutique ", Eval("City")," Vacation Rentals, Boutique ",Eval("City")," ",Eval("Category")) %>' />--%>
                                                                                    
                                                                                </a>
                                                                                
                                                                         
                                                                                <p class="namebelowthumbnail" style="display: none;height:10px;">
                                                                                </p>
                                                                                
                                                                                <asp:Label ID="propname" runat="server" Text='<%#Eval("name2")%>'></asp:Label>
                                                                                
                                                                            </td>
                                                                            <%--start of edit--%>
                                                                            <td>
                                                                                <div >
                                                                                    <table>
                                                                                        <tr>
                                                                                            <td class="bottomline"  >
                                                                                                <div>
                                                                                                <ul class="proplist">
                                                                                                    <li><h3 style="margin-top:0px;padding:0px;"><span class="H3CityText"><a href="<%# CommonFunctions.PrepareURL (((string)DataBinder.Eval(Container.DataItem, "Country", "{0}")).Replace (" ", "_").ToLower () + "/" + ((string)DataBinder.Eval(Container.DataItem, "StateProvince", "{0}")).Replace (" ", "_").ToLower () + "/" + ((string)DataBinder.Eval(Container.DataItem, "City", "{0}")).Replace (" ", "_").ToLower () + "/" + DataBinder.Eval(Container.DataItem, "ID", "{0}") + "/default.aspx") %>">
                                                                                                        <asp:Label ID="Label4" class="tdNoSleepsBathNum" runat="server" Text='<%# String.Format("{0}{1}{2}{3}",Eval("type"),"","", "")%>'></asp:Label>
                                                                                                        <span style="padding:0px; margin:0px;">
                                                                                                            <asp:Label ID="Label5" runat="server"><%# Eval("NumBedrooms")%></asp:Label>
                                                                                                            <asp:Label ID="Label6" runat="server">Bedroom</asp:Label>
                                                                                                            <asp:Label ID="BathNum" runat="server" Text='<%# Bind("NumBaths") %> '></asp:Label>
                                                                                                            <asp:Label ID="Label7" runat="server">BA Sleeps</asp:Label>
                                                                                                            <asp:Label ID="Label8" runat="server"><%# Eval("NumSleeps")%></asp:Label>
                                                                                                        </span></a></span>
                                                                                                        </h3>
                                                                                                   </li>
                                                                                                    <li>
                                                                                                        <%--                                                                                            <center class="tdRentalRates H3CityText">
                                                                                                <asp:Label ID="lblPNRates" runat="server"></asp:Label>
                                                                                                <asp:Label ID="lblSeparator" runat="server" Text=' - '></asp:Label>
                                                                                                <asp:Label ID="BedNum" runat="server" Text='<%# Bind("NumBedrooms") %>'></asp:Label>
                                                                                                <asp:Label ID="Label3" runat="server" Text="Bedroom Sleeps"></asp:Label>
                                                                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("NumSleeps") %>'></asp:Label>
                                                                                                </center>
                                                                                                        --%>
                                                                                                        
                                                                                                        <span>
                                                                                                            <asp:Label ID="lblPNRatesCaption" class="tdRentalRatesBlue" runat="server">Rates:&nbsp;</asp:Label>
                                                                                                            <asp:Label ID="lblPNRates" CssClass="tdRentalRates"  runat="server"></asp:Label>
                                                                                                            <asp:Label ID="lblPNRatesCurrency" class="tdRentalRatesBlue" runat="server"></asp:Label>
                                                                                                            <asp:Label ID="lblPNRatesBasis" CssClass="tdRentalRates" runat="server"></asp:Label>
                                                                                                            <asp:Label ID="lblMinimumNights" class="tdRentalRatesBlue" Style="text-transform: capitalize;"
                                                                                                                runat="server" Text='<%# Bind("MinimumNightlyRental") %>'></asp:Label>
                                                                                                            <asp:Label ID="Label2" class="tdRentalRatesBlue" runat="server">Minimum</asp:Label>
                                                                                                            <%--<%# PropertiesFullSet.Tables["Properties"].Rows[0]["MinimumNightlyRental"] %>--%>
                                                                                                            <%--<asp:Label ID="lblseparator" runat="server" Text=' - '></asp:Label>--%>
                                                                                                        </span>
                                                                                                        
                                                                                                    </li>
                                                                                                    <li>
                                                                                                        <center class="tdNoSleeps">
                                                                                                            <%--                                                                                                        <asp:Label ID="BedNum" runat="server" Text='<%# Bind("NumBedrooms") %>'></asp:Label>
                                                                                                        <asp:Label runat="server" Text="Bedroom "></asp:Label>
                                                                                                        <asp:Label ID="BathNum" runat="server" Text='<%# Bind("NumBaths") %> '></asp:Label>
                                                                                                        <asp:Label runat="server" Text="BA Sleeps "></asp:Label>
                                                                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("NumSleeps") %>'></asp:Label>                                                                                                    
                                                                                                            --%>
                                                                                                            <%--                                                                                                    <asp:Label runat="server" Text='vacation rental in '></asp:Label>
                                                                                                        <asp:Label runat="server" Text='<%#Eval("city")%>'></asp:Label>
                                                                                                            --%>
                                                                                                        </center>
                                                                                                    </li>
                                                                                                    <li id="liAmenity"  runat="server">
                                                                                                        <asp:Label ID="lblAmenitiesCaption" CssClass="amenitiesCaption" runat="server">Amenities:</asp:Label>
                                                                                                        <h4 style="display:inline;"><asp:Label ID="lblAmenities" CssClass="amenities" runat="server"></asp:Label></h4>
                                                                                                    </li>
                                                                                                    <li class="liDesccity">
                                                                                                        <asp:Label ID="lblDesc" runat="server" Text='<%# Eval("Name","{0} ")  %>'></asp:Label>
                                                                                                    </li>
                                                                                                </ul>
                                                                                                </div>

                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                    <%--middle of edit--%>
                                                                                </div>
                                                                                <div style="float: right;">
                                                                                    <center>
                                                                                        <div id="divCalendar" runat="server">
                                                                                        </div>
                                                                                        <div id="divWrite" runat="server">
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
                                                        <PagerStyle CssClass="StatePager" BackColor="#CCAF00" ForeColor="Black" />
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
                                                    <table>
                                                        <tr>
                                                            <td align="left" style="float: left; width: 82%;">

                                                                <br />
                                                                <% if (AuthenticationManager.IfAuthenticated && AuthenticationManager.IfAdmin)
                                                                   { %>
                                                                <center>
                        <asp:TextBox ID="txtCityText2" runat="server" Rows="7" TextMode="MultiLine" Width="600px"></asp:TextBox>
                        <br />
                        <asp:Button ID="btnSubmit2" runat="server" Text="Save Text" OnClick="btnSubmit2_Click" /></center>
                                                                <br />
                                                                <% } %>

                                                                <% if (string.IsNullOrEmpty(Convert.ToString(Request.QueryString["category"])))
                                                                   { %>
                                                                <h2 style="font-size: 1.5em; color: #050505">
                                                                    <b>
                                                                        <span id="ctl00_Content_City_datagrid_ctl02_lblHeader"><asp:Label ID="lblBottom" runat="server"></asp:Label></span></b></h2>
                                                                <asp:Label ID="lblInfo2" runat="server" EnableViewState="False" CssClass="" style="font-size:14px;color:#050505"></asp:Label>
                                                                <% } %>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <div style="float: left; width: 18%">

                                                    <script async src="//pagead2.googlesyndication.com/pagead/js/adsbygoogle.js"></script>

                                                    <!-- Country Ads -->
                                                    <ins class="adsbygoogle" style="display: inline-block; width: 160px; height: 600px"
                                                        data-ad-client="ca-pub-0264789273185284" data-ad-slot="9499072355"></ins>

                                                    <script>
                                                        (adsbygoogle = window.adsbygoogle || []).push({});
                                                    </script>

                                                </div>
                                            </div>
                </div>


                    <div class="OrangeText" style="text-align: left; float: left;">
                        <br />
                    </div>
        <asp:Label ID="lblInfo22" runat="server" ForeColor="Red"></asp:Label>
        <div class="country_list_box">

                <ul class="proplists">
                    <li><div id="rtHd3" runat="server"  style="display:inline;"></div></li>
                    <asp:Literal id="rtLow3" runat="server">
                            </asp:Literal>

                </ul>

            </div>
        <p align="left">
            
       
  

            <script type="text/javascript" src="http://www.statcounter.com/counter/counter.js"></script>

            <noscript>
                <div class="statcounter">
                    <a title="web counter" href="http://www.statcounter.com/free_hit_counter.html" target="_blank">
                        <img class="statcounter" src="http://c37.statcounter.com/3345790/0/b7bf8208/1/" alt="web counter" /></a>
                </div>
           </noscript>

        </p>
    </div>

        </div>
    </div>

   
    
    <script src="/Assets/js/citylist.js"></script>
</asp:Content>
