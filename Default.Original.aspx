<%@ Page Language="C#" MasterPageFile="~/MasterPageNoCss.master" AutoEventWireup="true"
    CodeFile="Default.Original.aspx.cs" Inherits="DefaultOriginal" Title="<%# GetTitle () %>" EnableEventValidation="False" %>

<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="Server">
    <asp:Label ID="Title" runat="server" Visible="false" Text="Vacations-Abroad: Vacation Rentals, Budget Self Catering Apartments, Luxury Holiday Villas by owner"></asp:Label>
    <asp:Label ID="Keywords" runat="server" Visible="false" Text="Vacations-Abroad: Vacation rentals, holiday villa rentals, vacation condo rentals, Budget self catering apartment rentals by owner"></asp:Label>
    <asp:Label ID="Description" runat="server" Visible="false" Text="Vacations-Abroad.Vacation rentals, Luxury Holiday Villas, Budget Self Catering Apartments by owner"></asp:Label>

    <table id="MainTable3" border="0" align="center" cellpadding="1" cellspacing="0">
        
        <tr>
            <td class="TD3B" valign="top" colspan="2" style="background-image:url('Photo3.jpg');background-size: 100%;background-repeat:no-repeat;width:66%;
height:375px;background-position:center;color:#ffffff;font-weight:bold;">
<div style="float:left;width:49%;padding-left:4px;padding-top:50px;">
                Search for vacation rentals by<br />
                    Country - State or Province - City 
                <div id="TD3AA">
                    <select name="region" id="region" onchange="setCountries();" style="width: 137px">
                        <option value="1">Africa</option>
                        <option value="2">Asia</option>
                        <option value="4">Caribbean</option>
                        <option value="5">Central America</option>
                        <option value="6">Europe</option>
                        <option value="7">Middle East</option>
                        <option value="8">North America</option>
                        <option value="3">Oceania</option>
                        <option value="9">South America</option>
                    </select><br />
                    <select name="country" id="country" onchange="setStates();" style="width: 137px">
                        <option value="">Please select a Region</option>
                    </select>
                    <br />
                    <select name="state" id="state" onchange="setCities();" style="width: 137px">
                        <option value="">Please select a Region</option>
                    </select>
                    <br />
                    <select name="city" id="city" onchange="editCities();" style="width: 137px">
                        <option value="0">Please select a Region</option>
                    </select>
                    
                </div>
                <br />
                <asp:Button ID="FindByLocation" runat="server" Width="50px" Text="Find" CausesValidation="False"
                    OnClick="FindByLocation_Click" />
            </div>
<div style="float:left;width:49%;padding-left:4px;padding-top:50px;">
               Search by Vacation Rental Property Number
                <br />
                <asp:TextBox ID="PropertyNumber" runat="server" Width="50px" />
                <br />
                <asp:Button ID="FindByPropertyNumber" runat="server" Width="50px" Text="Find" CausesValidation="False"
                    OnClick="FindByPropertyNumber_Click" />
</div>
            </td>
            <td style="text-align: left; background-color:#8080cc; padding:0 0 0 0;width:33%;">
            <center><font color="#ffffff">New Holiday Rentals</font></center>
                <asp:Repeater ID="Repeater1" runat="server" DataMember="Properties" DataSource="<%# PropertiesFullSet %>">
                    <HeaderTemplate>
                        <table width="100%" style="background-color:White;" border="0" cellpadding="0" cellpadding="0">
                       
                        
                    </HeaderTemplate>
                    <SeparatorTemplate>
                    <tr><td style="width:100%; border-bottom: solid 10px #ffffff; height:10px; padding:1 1 1 1;">
                    <%--<hr size="1" width="100%" style="color:#ececec;" />--%>
                    </td></tr>
                    </SeparatorTemplate>
                    <ItemTemplate>
                        <tr>
                            <td style="width:100%; text-align:left;" valign="middle">
                            <table>
                            <tr><td>
                                <a href='<%# CommonFunctions.PrepareURL (((string)DataBinder.Eval(Container.DataItem, "Country", "{0}")).Replace (" ", "_").ToLower () + "/" + ((string)DataBinder.Eval(Container.DataItem, "StateProvince", "{0}")).Replace (" ", "_").ToLower () + "/" + ((string)DataBinder.Eval(Container.DataItem, "City", "{0}")).Replace (" ", "_").ToLower () + "/" + DataBinder.Eval(Container.DataItem, "ID", "{0}") + "/default.aspx") %>'>
                                    <img src='<%# DataBinder.Eval(Container.DataItem, "PhotoImage", "http://www.vacations-abroad.com/images/TH{0}") %>'
                                        class="grdImg" width="105" height="90" border="1" alt='<%# DataBinder.Eval(Container.DataItem, "Country", "{0}") %> vacation rental' />
                                </a>
                                </td>
                                <td>
                            
                                <a href='<%# CommonFunctions.PrepareURL (((string)DataBinder.Eval(Container.DataItem, "Country", "{0}")).Replace (" ", "_").ToLower () + "/" + ((string)DataBinder.Eval(Container.DataItem, "StateProvince", "{0}")).Replace (" ", "_").ToLower () + "/" + ((string)DataBinder.Eval(Container.DataItem, "City", "{0}")).Replace (" ", "_").ToLower () + "/" + DataBinder.Eval(Container.DataItem, "ID", "{0}") + "/default.aspx") %>'>
                                    <%# DataBinder.Eval(Container.DataItem, "City", "{0}") %> <%# DataBinder.Eval(Container.DataItem, "stateProvince", "{0}") %> Vacation Rentals
                                   
                                   <%-- <%# DataBinder.Eval(Container.DataItem, "CategoryTypes", "{0}")%>--%>
                                </a>
                            </td></tr></table>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </td>
        </tr>
    </table>
   
    <div align="center">
        <asp:RequiredFieldValidator ID="PropertyNumberRequired" runat="server" Height="16px"
            ErrorMessage="Please enter property number" ControlToValidate="PropertyNumber"
            Display="Dynamic" />
    </div>
    <div align="center">
        <asp:RegularExpressionValidator ID="PropertyNumberFormat" runat="server" Height="16px"
            ErrorMessage="Invalid property number entered" ControlToValidate="PropertyNumber"
            ValidationExpression="^\d+$" Display="Dynamic" />
    </div>
    
    <div align="center">
        <asp:Label ID="NothingFound" runat="server" Height="16px" Width="136px" ForeColor="Red"
            Visible="False">No results were found</asp:Label>
    </div>
    
    <table id="MainTable4" class="MainTable4">
        <tr class="TR3">
            <td valign="middle" style="font-size:12px; font-weight:bold; background-color:#000072;">
                
                    <h2>Vacation Apartments and Holiday Villa Rentals</h2>
               
            </td>
        </tr>
    </table>
    
    <center>
    
        <table width="100%" style="text-align: left; background-color: #ececec;">
            <tr>
                <td valign="top" width="20%">
                <div style="border:solid 1px #ececec; background-color:White; padding:2px;">
                    <center>
                       <a class="mainRegion" href="<%# CommonFunctions.PrepareURL("africa/default.aspx") %>">AFRICA HOLIDAY APARTMENTS</a>
                        </center>
                    <div id="divAfrica" runat="server">
                    </div>
                    </div>
                    <br />
                     <div style="border:solid 1px #ececec; background-color:White; padding:2px;">
                    <center>
                       <a class="mainRegion" href="<%# CommonFunctions.PrepareURL("asia/default.aspx") %>">ASIA VACATION APARTMENTS</a></center>
                    <div id="divAsia" runat="server">
                    </div>
                    </div>
                    <br />
                     <div style="border:solid 1px #ececec; background-color:White; padding:2px;">
                    <center>
                       <a class="mainRegion" href="<%# CommonFunctions.PrepareURL("oceania/default.aspx") %>">OCEANIA HOLIDAY APARTMENTS</a></center>
                    <div id="divOceania" runat="server">
                    </div>
                    </div>
                    <br />
                     <div style="border:solid 1px #ececec; background-color:White; padding:2px;">
                    <center>
                        <a class="mainRegion" href="<%# CommonFunctions.PrepareURL("south_america/default.aspx") %>">SOUTH AMERICA VACATION RENTALS</a></center>
                    <div id="divsAmerica" runat="server">
                    </div>
                    </div>
                </td>
                <td valign="top" width="20%">
                 <div style="border:solid 1px #ececec; background-color:White; padding:2px;">
                    <center>
                         <a class="mainRegion" href="<%# CommonFunctions.PrepareURL("caribbean/default.aspx") %>">CARIBBEAN VACATION APARTMENTS</a></center>
                    <div id="divCaribbean" runat="server"> 
                    </div>
                    </div>
                    <br />
                     <div style="border:solid 1px #ececec; background-color:White; padding:2px;">
                    <center>
                         <a class="mainRegion" href="<%# CommonFunctions.PrepareURL("central_america/default.aspx") %>">CENTRAL AMERICA HOLIDAY RENTALS</a></center>
                    <div id="divcAmerica" runat="server">
                    </div>
                    </div>
                </td>
                <td colspan="2" valign="top" width="40%">
                 <div style="border:solid 1px #ececec; width:100%; background-color:White;">
                 
                    <center>
                        <a class="mainRegion" href="<%# CommonFunctions.PrepareURL("europe/default.aspx") %>">EUROPE VACATION APARTMENTS</a></center>
                        <table style="width:100%">
                        <tr><td style="width:50%" valign="top">
                    <div id="divEurope" runat="server" style="width:100%;">                    
                    </div>
              </td>
                       <td style="width:50%" valign="top">        
                    <div id="divEurope2" runat="server" style="width:100%;">
                    </div>
                    </td></tr></table>
                    </div>
                </td>
                <td valign="top" width="20%">
                 <div style="border:solid 1px #ececec; background-color:White; padding:2px;">
                    <center>
                        <a class="mainRegion" href="<%# CommonFunctions.PrepareURL("middle_east/default.aspx") %>">MIDDLE EAST HOLIDAY RENTALS</a></center>
                    <div id="divmEast" runat="server">
                    </div>
                    </div>
                     
                    <br />
                    <div style="border:solid 1px #ececec; background-color:White; padding:2px;">
                    <center>
                        <a class="mainRegion" href="<%# CommonFunctions.PrepareURL("north_america/default.aspx") %>">NORTH AMERICA VACATION RENTALS</a></center>
                    <div id="divnAmerica" runat="server">
                    </div>
                    </div>
                </td>
            </tr>
        </table>
        <table id="Table1" class="MainTable4">
        <tr class="TR3">
            <td valign="middle" style="font-size:18px; font-weight:bold; background-color:#999999;">
                
                   
               
            </td>
        </tr>
    </table>
        <br />
       
    </center>
   

    <script type="text/javascript" language="javascript">
	    <%= DropDownScript () %>

        
    </script>

    
        <asp:Label ID="lblInfo" runat="server" ForeColor="Red"></asp:Label>
    </div>
 <%--<a href="http://www.vacations-abroad.com/stevemapua.htm">Seattle SEO</a><br />--%>
                                    
    <%--<!-- Start of StatCounter Code -->
    <script type="text/javascript">
        sc_project = 3336280;
        sc_invisible = 1;
        sc_partition = 36;
        sc_security = "510252c5"; 
    </script>
    <script type="text/javascript" src="http://www.statcounter.com/counter/counter.js"></script>

    <noscript>
        <div class="statcounter">
            <a title="hit counter" href="http://www.statcounter.com/free_hit_counter.html" target="_blank">
                <img class="statcounter" src="http://c37.statcounter.com/3336280/0/510252c5/1/" alt="hit counter" /></a></div>
    </noscript>--%>
    <!-- End of StatCounter Code -->

    <script type="text/javascript">
        var gaJsHost = (("https:" == document.location.protocol) ? "https://ssl." : "http://www.");
        document.write(unescape("%3Cscript src='" + gaJsHost + "google-analytics.com/ga.js' type='text/javascript'%3E%3C/script%3E"));
    </script>

    <%--<script type="text/javascript">
        try {
            var pageTracker = _gat._getTracker("UA-1499424-2");
            pageTracker._trackPageview();
        } catch (err) { }</script>

                          <script type="text/javascript">
                              google_ad_client = "pub-0264789273185284";
                              google_ad_width = 468;
                              google_ad_height = 60;
                              google_ad_format = "468x60_as";
                              google_ad_type = "text_image";
                              google_ad_channel = "";
                              google_color_border = "6699CC";
                              google_color_bg = "003366";
                              google_color_link = "FFFFFF";
                              google_color_text = "AECCEB";
                              google_color_url = "AECCEB";
                        </script>

                        <script type="text/javascript" src="http://pagead2.googlesyndication.com/pagead/show_ads.js">
                        </script>--%>
</asp:Content>
