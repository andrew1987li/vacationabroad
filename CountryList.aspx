<%@ Page Language="C#" MasterPageFile="~/masterpage/NormalMaster.master" AutoEventWireup="true"
    CodeFile="~/countrylist.aspx.cs" Inherits="CountryList"
    EnableEventValidation="false" EnableViewState="true" %>
<asp:Content ID="links" ContentPlaceHolderID="links" runat="server">
    <link href="/Assets/css/countrylist.css" rel="stylesheet" />
</asp:Content>


<asp:Content ID="Content" ContentPlaceHolderID="bodycontent" runat="Server">
    <div class="row">
    <div class="internalpagewidth">

        <%--    <asp:TextBox runat="server" ID ="txtCityVal"  value="" Style="display:none;" ></asp:TextBox>
        --%>

        <div class="row text-left">
            <asp:HyperLink ID="hyplnkBackLink" runat="server" ><h3 class="backitem"><asp:Literal ID="ltrBackText" runat="server"></asp:Literal></h3></asp:HyperLink>
        </div>
        <div class="row text-center">
                <h1 class="H1CityText">
                    <%--<%= city %> Vacation Rentals--%>
                    <asp:Literal ID="ltrH11" runat="server"></asp:Literal>

                </h1>
        </div>
        

        <div class="row">
                <asp:Label ID="Label3" runat="server" EnableViewState="False" ForeColor="Red" Style="display: none"></asp:Label>
                <div class="row">
                    <% if (AuthenticationManager.IfAuthenticated && AuthenticationManager.IfAdmin)
                    { %>
                    <asp:TextBox ID="txtCountryText" runat="server" Rows="7" TextMode="MultiLine" Width="600px"></asp:TextBox><br />
                    <asp:Button ID="btnSubmit" runat="server" Text="Save Text" OnClick="btnSubmit_Click" />
                    <br />
                    <asp:Label ID="Label1" runat="server" EnableViewState="False" ForeColor="Red"></asp:Label>
                    <% }
                    else
                    {%>
                    <div class="col-md-6 col-sm-6">
                       <div runat="server" visible="true">
                            <asp:Label ID="lblCountryInfo" CssClass="txtalign" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="col-md-6 col-sm-6">
                     <div  id="googleCountrymap" runat="server">
                    </div>
                    </div>
 
 

                    <% } %>
                </div>
                <div class="row">
                    <asp:Label ID="lblInfo" runat="server" ForeColor="Red"></asp:Label>
                    <asp:SqlDataSource ID="Sql_properties" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                        SelectCommand="GetPropertiesFromStateID2" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:QueryStringParameter DefaultValue="25" Name="StateProvinceID" QueryStringField="StateProvinceID"
                                Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>

                   
                
                     <div class="row text-left">  
                
                             <div class="linkpadding">
                                 <h2 class="inlineblock"><asp:Literal ID="ltrHeading" runat="server"></asp:Literal></h2>
                                <asp:HyperLink ID="hyplnkAllProps"  Text="AllProperties" runat="server"><h3 class="inlineblock viewalllink"><asp:Literal ID="ltrAllProps" runat="server"></asp:Literal></h3></asp:HyperLink>
                            </div>  

                     </div>          
 
              <div class="row">
                  <div class="text-center">
                        <ul id="Statesul" class="stateful" runat="server">
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
              </div>
        
        <div class="row">
            <div class="contentpadding">
 

                            <div class="orangetxt" id="OrangeTitle" runat="server">
                                Things to see and do in <asp:Literal ID="ltrCountryThing" runat ="server"></asp:Literal>
                            </div>
                                <% if (AuthenticationManager.IfAuthenticated && AuthenticationManager.IfAdmin)
                    { %>
                        <asp:TextBox ID="txtCountryText2" runat="server" Rows="7" TextMode="MultiLine" Width="600px"></asp:TextBox><br />
                        <center>
                            <asp:Button ID="btnSubmit2" runat="server" Text="Save Text" OnClick="btnSubmit2_Click" /></center>
                        <br />
                        <% } %>
                        <p><asp:Label ID="lblInfo2" CssClass="contentstyle" runat="server" EnableViewState="False"></asp:Label>
                            </p>


  
            </div>
        </div>

        <div class="row contentpadding">
            

                <ul class="countrylist">
                    <li><div id="rtHd3" runat="server" style="display:inline;"></div></li>
                    <asp:Literal id="rtLow3" runat="server">
                            </asp:Literal>

                </ul>

   
        </div>
                      


        </div>



            


        </div>
            <asp:Label ID="lblInfo22" runat="server" ForeColor="Red" Style="display: none"></asp:Label>

    </div>
             <asp:Label ID="Title" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="Keywords" runat="server" Visible="false" Text="%stateprovince% vacation rentals, %stateprovince% Hotels, %stateprovince% Cottages, %stateprovince% B&Bs, %stateprovince% villas , "></asp:Label>
        <asp:Label ID="Description" runat="server" Visible="false" Text="Relax and unwind in our %stateprovince% vacation rentals, B&Bs and boutique hotels in %country% "></asp:Label>
        <asp:TextBox runat="server" ID="txtCityVal" value="" Style="display: none;"></asp:TextBox>
        <asp:TextBox runat="server" ID="txtCityVal2" value="" Style="display: none;"></asp:TextBox>
    </div>

        <script src="/Assets/js/countrylist.js"></script>
</asp:Content>

