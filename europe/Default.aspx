<%@ Page Language="C#" MasterPageFile="/masterpage/NormalMaster.master" AutoEventWireup="true"
    CodeFile="default.aspx.cs" Inherits="Default" Title="<%# GetTitle () %>" EnableEventValidation="False"
    EnableViewState="false" %>

<asp:Content ID="links" ContentPlaceHolderID="links" runat="server">
    <link href="/Assets/css/africa.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="bodycontent" runat="Server">
    <div class="internalpagewidth">
  <div class="upperheader">
        <h1>Europe Vacations</h1>
    </div>
    <%--Main div--%>

             <div class="vbrblue">
                European Vacations
            </div>
   
                <div>
            <%--VBR--%>
            <div class="newline">
           <div class="clear">
                <%--content text--%>
             <div class="col-7">
                    <p> Europe vacations are always the vacation of a lifetime, regardless of whether you are setting foot on the continent for the first time or have been there 100 times before. The world's second smallest continent is also the world's top tourist destination, attracting more than 480 million international visitors per year or almost half of the global market. Stretching from the Atlantic to Asia and from the Arctic to Africa, the European landscape is replete with stunning beaches, awe-inspiring mountains, lush green fields, historical cities, and most importantly, a rich and diverse, and beautifully preserved cultural heritage.  Planning your next Europe vacation is easy with Vacations-Abroad.com because we have Europe vacation rentals, B&Bs and hotels located in almost every country.
                            </p>
                            <br/>
                            <p>
                                You can find Europe vacation rentals located along the beaches of the Mediterranean to the French Riviera, from the magical ski-slopes of Chamonix French Alps and Zermatt Switzerland to Ibiza in the west and Italian Alps in the south, all the way to Islands of Mallorca and Balearic Islands.  Europe is blessed with so much historical and natural beauty that one might not be able to see it all even if multiple family vacations were spent visiting all the countries that make up this region. The main attraction are the hundreds of historical and ancient villages and cities that are surrounded by culture, monuments, and castles. Explore the different countries and you'll know why Europe vacations are the most popular in the world.
                            </p>

             </div>
             <div class="col-3" >
              <img src="/images/europevacations.jpg" height="190" width="300" alt="The whitewashed streets of Lisbon Portugal"/>
                            <img src="../Images/likeusonfacebook.jpg" />
                              <div id="fb-root"></div>

                        <div class="fb-like" style="float:left;padding-left:5px" data-href="https://www.facebook.com/VacationsAbroad" data-width="75" data-layout="standard" data-action="like" data-show-faces="true" data-share="false"></div>
             </div>

                    
            </div>
            </div>

            <%--content VBR--%>
 
            <div class="newline">
                        <ul class="TripleListMain">
                            <li><a class="greytext" href="/austria/default.aspx"><b>Austria</b></a></li>
                            <li><a class="greytext" href="/belgium/default.aspx"><b>Belgium</b></a></li>
                            <li><a class="greytext" href="/bulgaria/default.aspx"><b>Bulgaria</b></a></li>
                            <li><a class="greytext" href="/croatia/default.aspx"><b>Croatia</b></a></li>
                            <li><a class="greytext" href="/cyprus/default.aspx"><b>Cyprus</b></a></li>
                            <li><a class="greytext" href="/czech_republic/default.aspx"><b>Czech Republic</b></a></li>
                            <li><a class="greytext" href="/england/default.aspx"><b>England</b></a></li>
                            <li><a class="greytext" href="/estonia/default.aspx"><b>Estonia</b></a></li>
                            <li><a class="greytext" href="/france/default.aspx"><b>France</b></a></li>
                            <li><a class="greytext" href="/germany/default.aspx"><b>Germany</b></a></li>
                            <li><a class="greytext" href="/greece/default.aspx"><b>Greece</b></a></li>
                            <li><a class="greytext" href="/hungary/default.aspx"><b>Hungary</b></a></li>
                            <li><a class="greytext" href="/ireland/default.aspx"><b>Ireland</b></a></li>
                            <li><a class="greytext" href="/italy/default.aspx"><b>Italy</b></a></li>
                            <li><a class="greytext" href="/latvia/default.aspx"><b>Latvia</b></a></li>
                            <li><a class="greytext" href="/lithuania/default.aspx"><b>Lithuania</b></a></li>
                            <li><a class="greytext" href="/malta/default.aspx"><b>Malta</b></a></li>
                            <li><a class="greytext" href="/montenegro/default.aspx"><b>Montenegro</b></a></li>
                            <li><a class="greytext" href="/netherlands/default.aspx"><b>Netherlands</b></a></li>
                            <li><a class="greytext" href="/norway/default.aspx"><b>Norway</b></a></li>
                            <li><a class="greytext" href="/poland/default.aspx"><b>Poland</b></a></li>
                            <li><a class="greytext" href="/portugal/default.aspx"><b>Portugal</b></a></li>
                            <li><a class="greytext" href="/romania/default.aspx"><b>Romania</b></a></li>
                            <li><a class="greytext" href="/scotland/default.aspx"><b>Scotland</b></a></li>
                            <li><a class="greytext" href="/spain/default.aspx"><b>Spain</b></a></li>
                            <li><a class="greytext" href="/switzerland/default.aspx"><b>Switzerland</b></a></li>
                            <li><a class="greytext" href="/turkey/default.aspx"><b>Turkey </b></a></li>
                            <li><a class="greytext" href="/wales/default.aspx"><b>Wales</b></a></li>


                        </ul>

            </div>

            <asp:Repeater ID="Repeater1" runat="server" DataMember="Countries" DataSource="<%# CountriesStates %>" Visible="false">
                <HeaderTemplate>
                    <table width="97%" cellpadding="0" cellspacing="2" border="0"
                        bgcolor="#ffffff" align="center">
                        <tr>
                            <td colspan="25">
                                <h1>
                                    <p align="center">
                                        <font size="3" color="#cc3333" face="Arial">
                                            <%= TableTitle ()%>
                                        </font>
                                </h1>

                            </td>
                        </tr>
                        <tr bgcolor="#ffffff">
                            <td></td>
                            <td></td>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td valign="top">
                            
                                <div style="width: 130px; text-align: left;">
                                    <font size="2"><a href='<%# CommonFunctions.PrepareURL (((string)DataBinder.Eval(Container.DataItem, "Country", "{0}")).Replace (" ", "_").ToLower () + "/default.aspx") %>'>
                                        <b><%# DataBinder.Eval(Container.DataItem, "Country", "{0}") %></b>
                                    </a></font>
                                </div>
                            
                        </td>
                        <td>
                            <div style="text-align: left;">
                                <asp:Repeater ID="Repeater2" runat="server" DataSource='<%# ((System.Data.DataRowView)Container.DataItem).CreateChildView("CountriesStates")%>'>
                                    <HeaderTemplate>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <font size="2"><a href='<%# CommonFunctions.PrepareURL (((string)DataBinder.Eval(Container.DataItem, "Country", "{0}")).Replace (" ", "_").ToLower () + "/" + ((string)DataBinder.Eval(Container.DataItem, "StateProvince", "{0}")).Replace (" ", "_").ToLower () + "/default.aspx") %>'>
                                            <b><%# DataBinder.Eval(Container.DataItem, "StateProvince", "{0}") %></a></font>
                                        <%# !CommonFunctions.IfLastRow ((System.Data.DataRowView)Container.DataItem) ? "," : "" %>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </div>
                            <br />
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    <tr>
                        <td colspan="35">
                            <p align="center">
                                <font size="2" face="Arial">
                                    <%= TableTitle ()%>
                                </font>
                            </p>
                        </td>
                    </tr>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
            <div class="lineblue">
            </div>
            <div class="clear">
            </div>
        </div>
    <div>

        <%--2nd part begins--%>
        <%--VBR--%>
        <div class="newline">
             <div class="vbrgreen">
                Europe Beach Vacations
            </div>
        </div>
        <div class="newline">

            <div class="clear">
                <div class="col-3">
                        <img src="/images/europebeachvacations.jpg" height="233" width="300"  alt="A historic chateau along a river in northern France"/>
                </div>
                          
                <div class="col-r7">
                <p>
                                Europe beach vacations are crowded with tourists from every country during summer time. Warm beaches, turquoise waters, unique scenery, and quaint fishing villages are just some of the reasons why Europe beach vacations are the dreams of families and romantic couples around the world. The classy resorts of French Riviera, the absolutely splendid Italian Riviera, breathtaking coastline of Portugal, the dramatic beaches of the Islands of Mallorca and Balearic Islands, and the world-famous Cornwall Coast in England are among the most visited destinations in Europe. Open borders and an efficient infrastructure make the beaches of Europe or Europe’s famous cities easily accessible.  Many families and romantic couples consider Europe beach vacations as part of their traditional summer vacation. And the remaining population would not consider that their life is complete until they have experienced those one of a kind Europe beach vacations.
                            </p>
                            <br/>
                </div>
                           
            </div>
        </div>
        <div class="newline">

           

                <div style="display:inline-block;text-align:center; width: 413px;">
                    <h3><a class="greytext" href="/spain/balearic_islands/ibiza/default.aspx"><b>Ibiza Spain</b></a></h3>
                </div>
                <div style="display:inline-block;text-align:center">
                    <h3><a class="greytext" href="/spain/balearic_islands/mallorca/default.aspx"><b>Mallorca Spain</b></a></h3>
                </div>
                <div style="display:inline-block;text-align:center; width: 413px;">
                    <h3><a class="greytext" href="/greece/cyclades_islands/santorini/default.aspx"><b>Santorini Greece</b></a></h3>
                </div>
                <div style="display:inline-block;text-align:center">
                    <h3><a class="greytext" href="/cyprus/paphos/coral_bay/default.aspx"><b>Coral Bay Cyprus</b></a></h3>
                </div>

        </div>
        <div class="linegreen">
        </div>
        <%--3rdpart--%>
                    <div class="vbrblue">
                Europe Ski Vacations
            </div>
        <div class="newline">
            <%--VBR--%>

            <%--content VBR--%>
            <div class="clear">
                <div class="col-7">
                 <p> Europe ski vacations can be an addictive affair, even if you are not a skier. The majestic snow covered mountains and bewitching winter scenery coupled along with well-developed tourist facilities make Europe ski vacations a favorite among snow lovers around the globe. There are literally hundreds of Europe ski resorts studded like white pearls across France, Switzerland, Germany, Italy, and the rest of Europe. Many of them were developed as resorts more than a hundred years ago and hold the best skiing facilities and infrastructure in the world.
                            </p>
                            <br/>
                            <p>
                                Be it the idyllic slopes and warm après ski  night life of Zermatt Switzerland, the famous ski areas of Chamonix in the French Alps with their stunning views of Mont Blanc, or the challenging mountains and unspoiled wilderness of the Italian Alps, you'll enjoy skiing there like in no other part of the world. Snug and cozy Europe ski accommodations play a big role in making your European ski vacations an experience you'd like to repeat every year.
                            </p><br/>
                </div>
                <div class="col-3">
                    <img src="/images/europeskivacations.jpg" height="233" width="300"  alt="The death defying view in Chamonix France"/>
                </div>
              </div>
            <div class="newline">
               

                    <div style="display:inline-block;text-align:center; width: 413px;">
                        <h3><a class="greytext" href="/france/rhone_alps/chamonix/default.aspx"><b>Chamonix France Ski Vacations</b></a></h3>
                    </div>
                    <div style="display:inline-block;text-align:center;"">
                        <h3><a class="greytext" href="/france/rhone_alps/meribel/default.aspx"><b>Meribel France Ski Vacations</b></a></h3>
                    </div>
                    <div style="display:inline-block;text-align:center; width: 413px;">
                        <h3><a class="greytext" href="/switzerland/valais/zermatt/default.aspx"><b>Zermatt Switzerland Ski Vacations</b></a></h3>
                    </div>
                    <div style="display:inline-block;text-align:center;">
                        <h3><a class="greytext" href="/austria/tyrol/innsbruck/default.aspx"><b>Innsbruck Austria Ski Vacations</b></a></h3>
                    </div>

            </div>

        </div>
                    <div class="lineblue">
            </div>
            <div class="clear">
            </div>
        <%--4th part begins--%>
        <div class="newline">
                <div class="vbrgreen">
                     Europe City Vacations 
                </div>
       </div>
        <div class="newline">
            <%--VBR--%>
            <div class="clear">
                <div class="col-3">
                     <img src="/images/europecityvacations.jpg" height="245px" width="300px" alt="The rooftops of Rome Italy"/>
                </div>
                               
                <div class="col-r7">
                         <p>
                                    Europe vacations can conjure up images of cobblestoned tree lined streets with romantic and historic buildings and mystical rivers flowing underneath their ancient bridges. Immaculately maintained and culturally preserved cities such as Paris France, Edinburgh Scotland, Rome Italy, Prague Czech Republic, Copenhagen Denmark, Florence Italy, and London England attract tourists numbering into hundreds of millions each year. Europe city vacations are what many people look forward to their whole lives. Others, who are more fortunate to be able to vacation at some of the magical European cities, want their vacations to last forever.
                                </p>
                                <br/>
                                <p>
                                    The borderless continent showcases a collage of culture and history. Travel for a few hours, and you find yourself in another city with a different language and heritage. Vacations to Europe can range Scotland to Spain and from Poland to Portugal as Europe is studded with historically rich cities, preserved with their entire rustic splendor. Going to a city like Paris, Florence or Rome can feel like traveling back in time by hundreds of years, while enjoying staying in Europe vacation rentals.
                                </p><br/>
                </div>
                               
              </div>
        </div>
            <div class="newline">

                    <div style="display:inline-block;text-align:center; width: 413px;">
                        <h3><a class="greytext" href="/greece/attica/athens/default.aspx"><b>Athens Greece</b></a></h3>
                    </div>
                    <div  style="display:inline-block;text-align:center;">
                        <h3><a class="greytext" href="/france/ile_de_france/paris/default.aspx"><b>Paris France</b></a></h3>
                    </div>
                    <div  style="display:inline-block;text-align:center; width: 413px;">
                        <h3><a class="greytext" href="/england/london/default.aspx"><b>London England</b></a></h3>
                    </div>
                    <div  style="display:inline-block;text-align:center;">
                        <h3><a class="greytext" href="/italy/tuscany/florence/default.aspx"><b>Florence Italy</b></a></h3>
                    </div>
			        <div  style="display:inline-block;text-align:center; width: 413px;">
                        <h3><a class="greytext" href="/italy/lazio/rome/default.aspx"><b>Rome Italy</b></a></h3>
                    </div>
                    <div  style="display:inline-block;text-align:center;">
                        <h3><a class="greytext" href="/spain/catalonia/barcelona/default.aspx"><b>Barcelona Spain</b></a></h3>
                    </div>

            </div>
            <div class="linegreen">
            </div>
        </div>
    </div>

    <%-- <style>
        .PDescText
        {
            color: #202027;
            text-align: left;
            padding-top: 4px;
            padding-bottom: 4px;
        }
    </style>--%>
    <%--header--%>
  
    <%-- <div class="tapsection" style="border: 1px;">
        <div class="section" style="position: absolute;top: 86px;left: 72px;">
            <h1 style="width: 230px; text-align: center; font-family: 'Trebuchet MS', Arial, Helvetica, sans-serif; font-size: 21px; color: white; font-weight: bold; text-shadow: #005784 2px 2px;">
               Explore the world <br>in our vacation rentals, vacation apartments, vacation homes, cottages, B&Bs & Hotels                <%--Vacation Apartments and Holiday Rentals in the US and Abroad--%>
    <%--            </h1>

        </div>
</div>--%>
   
    <!-- Start of StatCounter Code for Default Guide -->


    <script type="text/javascript" src="http://www.statcounter.com/counter/counter.js"></script>
    <noscript>
        <div class="statcounter">
            <a title="site stats" href="http://statcounter.com/" target="_blank">
                <img class="statcounter" src="http://c.statcounter.com/3336280/0/510252c5/1/" alt="site stats"></a>
        </div>
    </noscript>
    <!-- End of StatCounter Code for Default Guide -->
   <%-- <script type="text/javascript">

        var _gaq = _gaq || [];
        _gaq.push(['_setAccount', 'UA-1499424-2']);
        _gaq.push(['_setDomainName', 'vacations-abroad.com']);
        _gaq.push(['_setAllowLinker', true]);
        _gaq.push(['_trackPageview']);

        (function () {
            var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
            ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
            var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
        })();

    </script>--%>
    <script src="/Assets/js/europe.js"></script>
</asp:Content>
