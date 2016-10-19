<%@ Page Language="C#" MasterPageFile="~/masterpage/NormalMaster.master" AutoEventWireup="true" CodeFile="SearchTerms.aspx.cs" Inherits="SearchTerms" %>

<asp:Content ID="head" ContentPlaceHolderID="head" runat="server">
    Search Terms
</asp:Content>
<asp:Content ID="links" ContentPlaceHolderID="links" runat="server">
    <link href="Assets/css/search.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="body" ContentPlaceHolderID="bodycontent" runat="server">
    <div class="internalpagewidth">
            <div class="newline centered">
                <h2 style="color:orangered">The results for keyword "<%=strkeyword %>"</h2>
            </div>
        <div class="newline borerstep">

            <div class="stepfont">
                <div class="col-1">
                     <label> Step 1: </label>
                </div>
                 <div class="col-6">
                       <% 
                           for (int i = 0; i < 10; i++)
                           {if (i == 6)
                               { %> 
                          <br />
                       <%} %>
                       <input type="radio" name="proptype" value="<%=prop_typeval[i]%>" /><%=str_propcate[i] %>(<%=prop_nums[i] %>)
                    <%} %>
                    <input type="radio" name="proptype" value="<%=prop_typeval[10]%>" checked="checked" /><%=str_propcate[10] %>(<%=prop_nums[10] %>)
       
                 </div>
           
            </div>
            <input type="hidden" id="strkeyword" value="<%=strkeyword %>" />
            <div class="stepfont">
                <div class="col-1">
                    <label> Step 2: </label> 
                </div>
                <div class="col-6"><input type="radio" id="roomnums" name="roomnums" value="1" /> 0-2 Bedrooms(<%=bedroominfo[1] %>)
                <input type="radio"  name="roomnums" value="2" /> 3-4 Bedrooms(<%=bedroominfo[2] %>)
                <input type="radio"  name="roomnums" value="3" /> 5+ Bedrooms(<%=bedroominfo[3] %>)
                <input type="radio"  name="roomnums" value="0" checked="checked" /> Display All(<%=bedroominfo[0] %>)</div>
            </div>
            <div class="stepfont">
                <div class="col-1">
                        <label> Step 3: </label>
                </div>
                <div class="col-6">
                <input type="radio" name="amenitytype" value="8" /> Hot Tub (<%=amenity_nums[0] %>)
                <input type="radio" name="amenitytype" value="33" /> Internet(<%=amenity_nums[1] %>)
                <input type="radio" name="amenitytype" value="1" /> Pets(<%=amenity_nums[2] %>)
                <input type="radio" name="amenitytype" value="11" /> Pool(<%=amenity_nums[3] %>)
                <input type="radio" name="amenitytype" value="0" checked="checked" /> Display All(<%=amenity_nums[4] %>)
                </div> 

            </div>
            <div class="stepfont">
                <div class="col-1">
                    <label> Step 4: </label>
                </div>
                <div class="col-6">
                 <input type="radio" name="pricesort" value="1" /> From high to low for the price
                <input type="radio" name="pricesort" value="2" /> From low to high for the price
                <input type="radio" name="pricesort" value="0" checked="checked" /> No sorting
                </div>
                <div class="col-2">
                    <input type="button" id="refresh" class="btnsigns" value="Refresh"  onclick="refreshprop()" />
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
    <script src="Assets/js/search.js"></script>
</asp:Content>