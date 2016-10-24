<%@ Page Language="C#" MasterPageFile="~/masterpage/NormalMaster.master" AutoEventWireup="true"
    CodeFile="CityList.aspx.cs" Inherits="newCityList" EnableEventValidation="false" %>
<asp:Content ID="head" ContentPlaceHolderID="head" runat="server">
    <%=countryinfo.City %> Vacation Rentals And Boutique Hotels | Vacation abroad
</asp:Content>


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
                        <asp:HyperLink ID="hyperRegion" runat="server"><h3 class="backitem"><%=countryinfo.Region %><<</h3></asp:HyperLink>
                        <asp:HyperLink ID="hyplnkCountryBackLink" runat="server"><h3 class="backitem"><%=countryinfo.Country %><<</h3></asp:HyperLink>
                        <asp:HyperLink ID="hyplnkStateBackLink" runat="server"><h3 class="backitem"><%=countryinfo.StateProvince %><<</h3></asp:HyperLink>
                        <div class="clear"></div>

                    </div>
                <div class="text-center">
                        <h1 class="H1CityText">
                            <%--<%= city %> Vacation Rentals--%>
                            <asp:Literal ID="ltrH11"  runat="server"></asp:Literal>
                            <br />
                        </h1>
                </div>

            </div>
            <div class="row">
                 <div class="newline citytext">
                        <asp:Label runat="server" ID="lblcity"  ></asp:Label>
                  </div>

                <input type="hidden"  id="cityid" value="<%=cityid %>"  />
                <input type="hidden"  id="CityParam" name="CityParam"  runat="server" />
                    <%--padding 305 center--%>
                    <div class="row">
                        <% if (AuthenticationManager.IfAuthenticated && AuthenticationManager.IfAdmin)
                           { %>
                        <asp:TextBox ID="txtCityText" runat="server" Rows="7" TextMode="MultiLine" Width="600px"></asp:TextBox><br />
                        <asp:Button ID="btnSubmit" runat="server" Text="Save Text" OnClick="btnSubmit_Click" />
                        <br />
                        <asp:Label ID="lblInfo" runat="server" EnableViewState="False" ForeColor="Red"></asp:Label>
                        <% }%>
                         
                    </div>

            <div class="newline" >
                        <div class="heding_box">
                                <h2>
                                    <%=countryinfo.City %> Vacation Rentals and <%=countryinfo.City %> Hotels
                                </h2>
                            </div>
                        
            </div>

          <div class="row">
        
       <div class="newline borerstep">

            <div class="stepfont">
                <div class="col-1">
                     <label> Step 1: </label>
                </div>
                 <div class="col-5">
                       <% 
//"City" vacation Rentals (count) "City" Hotesl (count)
                           for (int i = 0; i < 2; i++) {%>
                        <input type="radio" name="proptype" value="<%=prop_typeval[i]%>" /><%=countryinfo.City %> <%=str_propcate[i] %> (<%=prop_nums[i] %>)
                    <%} %>
                    <input type="radio" name="proptype" value="<%=prop_typeval[2]%>" checked="checked" /> <%=str_propcate[2] %> (<%=prop_nums[2] %>)
       
                 </div>
           
            </div>
            <div class="stepfont">
                <div class="col-1">
                    <label> Step 2: </label> 
                </div>
                <div class="col-5"><input type="radio" id="roomnums" name="roomnums" value="1" /> 0-2 Bedrooms (<%=bedroominfo[1] %>)
                <input type="radio"  name="roomnums" value="2" /> 3-4 Bedrooms (<%=bedroominfo[2] %>)
                <input type="radio"  name="roomnums" value="3" /> 5+ Bedrooms (<%=bedroominfo[3] %>)
                <input type="radio"  name="roomnums" value="0" checked="checked" /> Display All (<%=bedroominfo[0] %>)</div>
            </div>
            <div class="stepfont">
                <div class="col-1">
                        <label> Step 3: </label>
                </div>
                <div class="col-5">
                <input type="radio" name="amenitytype" value="8" /> Hot Tub (<%=amenity_nums[0] %>)
                <input type="radio" name="amenitytype" value="33" /> Internet (<%=amenity_nums[1] %>)
                <input type="radio" name="amenitytype" value="1" /> Pets (<%=amenity_nums[2] %>)
                <input type="radio" name="amenitytype" value="11" /> Pool (<%=amenity_nums[3] %>)
                <input type="radio" name="amenitytype" value="0" checked="checked" /> Display All (<%=amenity_nums[4] %>)
                </div> 

            </div>
            <div class="stepfont">
                <div class="col-1">
                    <label> Step 4: </label>
                </div>
                <div class="col-5">
                 <input type="radio" name="pricesort" value="1" checked="checked" /> From high to low for the price
                <input type="radio" name="pricesort" value="2" /> From low to high for the price
                <input type="radio" name="pricesort" value="0"  /> No sorting
                </div>
                <div class="col-2">
                    <input type="button" id="refresh" class="btnsigns" value="Search"  onclick="refreshprop()" />
                </div>

            </div>

        </div>
        <div class=" newline">
            <div class="pcontent">

                
            </div>
        </div>
        <div class="newline">
            <div class="pagination" id="paging">

            </div>
        </div>
    
  
  
        </div>
    <% if (countryinfo.CityText2!="")
{ %>
                 <div class="com_box">
                                <h3>
                                    <%=countryinfo.City %> Vacations: Things to see while on vacation in <%=countryinfo.City %>,<%=countryinfo.StateProvince %>
                                </h3><br />
                     <label>
                         <%=countryinfo.CityText2 %>
                     </label>
                   </div> 

            <% } %>
               </div>


 
                                               <div>

                                                                <br />
                                                                <% if (AuthenticationManager.IfAuthenticated && AuthenticationManager.IfAdmin)
                                                                   { %>
                                                                <center>
                                                                        <asp:TextBox ID="txtCityText2" runat="server" Rows="7" TextMode="MultiLine" Width="600px"></asp:TextBox>
                                                                        <br />
                                                                        <asp:Button ID="btnSubmit2" runat="server" Text="Save Text" OnClick="btnSubmit2_Click" /></center>
                                                                <br />
                                                                <% } %>
                                                    </div>
                                                <div style="float: left; width: 18%">

                                                    <script async src="//pagead2.googlesyndication.com/pagead/js/adsbygoogle.js"></script>

                                                    <!-- Country Ads -->
                                                    <ins class="adsbygoogle" style="display: inline-block; width: 160px; height: 10px"
                                                        data-ad-client="ca-pub-0264789273185284" data-ad-slot="9499072355"></ins>

                                                    <script>
                                                        (adsbygoogle = window.adsbygoogle || []).push({});
                                                    </script>

                                                </div>
                    <div class="OrangeText" style="text-align: left; float: left;">
                        <br />
                    </div>
        <asp:Label ID="lblInfo22" runat="server" ForeColor="Red"></asp:Label>
        <div style="display:none;">

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
 
   
    
    <script src="/Assets/js/citylist.js"></script>
</asp:Content>
