<%@ Page Language="C#" MasterPageFile="~/MasterPageNoCss.master" AutoEventWireup="true"
    CodeFile="default.aspx.cs" Inherits="Default" Title="<%# GetTitle () %>" EnableEventValidation="False"
    EnableViewState="false" %>

<%@ OutputCache Duration="600" VaryByParam="*" %>
<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="Server">
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
    <div class="upperheader" style="width: 68.8%">
        <h1>Oceania Vacations</h1>
    </div>
    <%--Main div--%>
    <div>
        <div>
            <%--VBR--%>
            <div class="vbrblue">
                Oceania Destinations
            </div>
            <%--content VBR--%>
            <div class="contenttextleft">
                <%--content text--%>
                <table>
                    <tr>
                        <td class="content1 tbody">
                            <p>
                                Stunningly beautiful beaches, lush green forests, tranquilizing lagoons, and sumptuous cruises are just some of the attractions of Oceania vacations. From the underwater caves of the Cook Islands to the flower orchards of Fiji, and from the sublime seashores of Tahiti to the vibrant cities of Australia and New Zealand, the Oceania is studded with countless enchanting destinations that can take a lifetime to fully explore.
                            </p>
                            <br>
                            <p>
                                The Oceania region comprises of numerous tiny islands located in the South Pacific. The term is loosely used to include cities on the coasts of Australia and New Zealand as well as secluded, deep-sea islands such as Tahiti, the Cook Islands, Fiji, Tonga, and many other little-known and unbelievably charming destinations. Most of the popular Oceania islands sport international airports and can be reached by air from most parts of the world.
                            </p>
                            <br>
                            <p>Imagine living in an offshore hut, perched above water so clear that you could see colourful fishes and other sea creatures swimming over the white seabed. Picture yourself cruising to breathtakingly beautiful volcanic islands scattered amidst the sprawling blue sea like green emeralds. Or, you could relish the ideal combination of a refreshingly idyllic and a sensually exciting vacation at Oceania cities such as Melbourne, Perth, and Auckland. </p>

                        </td>
                        <%--image--%>
                        <td class="imgright" style="border: none; box-shadow: none">
                            <img src="/images/oceaniavacations.jpg" height="190" width="300" alt="">
                            <img src="../Images/likeusonfacebook.jpg" />
                            <div id="fb-root"></div>
                            <script>(function (d, s, id) {
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) return;
    js = d.createElement(s); js.id = id;
    js.src = "//connect.facebook.net/en_US/all.js#xfbml=1&appId=159812020755747";
    fjs.parentNode.insertBefore(js, fjs);
}(document, 'script', 'facebook-jssdk'));</script>
                            <div class="fb-like" style="float: left; padding-left: 5px" data-href="https://www.facebook.com/VacationsAbroad" data-width="75" data-layout="standard" data-action="like" data-show-faces="true" data-share="false"></div>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="greytext" style="background-color: white;">
                <div style="border: solid 0px #ececec; padding: 2px; background-color: white; width: 928px">
                    <div id="ctl00_TabContainer_divAfrica">
                        <ul class="TripleListMain">

                            <li style="width: 16%"><a class="mainPgCountry" href="http://www.vacations-abroad.com/australia/default.aspx"><b>Australia</b></a></li>
                            <li style="width: 16%"><a class="mainPgCountry" href="http://www.vacations-abroad.com/cook_islands/default.aspx"><b>Cook Islands</b></a></li>
                            <li style="width: 16%"><a class="mainPgCountry" href="http://www.vacations-abroad.com/fiji/default.aspx"><b>Fiji</b></a></li>
                            <li style="width: 16%"><a class="mainPgCountry" href="http://www.vacations-abroad.com/new_zealand/default.aspx"><b>New Zealand</b></a></li>
                            <li style="width: 16%"><a class="mainPgCountry" href="http://www.vacations-abroad.com/tahiti/default.aspx"><b>Tahiti</b></a></li>


                        </ul>
                    </div>
                </div>
         
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
                            <h2>
                                <div style="width: 130px; text-align: left;">
                                    <font size="2"><a href='<%# CommonFunctions.PrepareURL (((string)DataBinder.Eval(Container.DataItem, "Country", "{0}")).Replace (" ", "_").ToLower () + "/default.aspx") %>'>
                                        <b><%# DataBinder.Eval(Container.DataItem, "Country", "{0}") %></b>
                                    </a></font>
                                </div>
                            </h2>
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
        <%--2nd part begins--%>
        <%--VBR--%>
        <div style="float: left;">
            <div>
            <div class="vbrgreen">
                Oceania Beach Vacations
            </div>
            <div class="contenttextright">
                <table>
                    <tr>
                        <%--image--%>
                        <td class="imgleft">
                            <img src="/images/oceaniabeachvacations.jpg" height="233" width="300" alt="">
                        </td>
                        <%--content VBR--%>
                        <%--content text--%>
                        <td class="content2 tbody">
                            <p>
                                Oceania beach vacations take you to some of the most scenic seascapes of the world. Surrounded by the expansive South Pacific Ocean, the secluded Oceania beach resorts of Fiji, Tahiti and the Cook Islands are fringed with thick, towering palms and carpeted with white powdery sands. The sweeping views of the ocean, the lush green tropical hills and forests, and the stunning oceanic peaks protruding out of the sea can make most vacationers drunk on natural beauty.
                            </p>
                            <br>

                            <p>There are literally hundreds of beach resorts dotting the Oceania seascape like precious jewels. Located in Australia, New Zealand, Tahiti, Fiji, the Cook Islands, and other Oceania destinations, these exotic beaches are carved straight out of paradise and don’t leave much to imagination. Oceania beach vacations are the dream of most people because of the sheer beauty and tranquillity that inundates this region. Relax, rejuvenate, and reward yourself as you behold the best that nature has to offer.</p>
                            <br>
                            <p>Oceania beach vacations are equally relished by families, romantic couples and divers. Many beach resorts in Oceania allow social nudity, while others have topless or clothing-optional beaches. There's no dearth of family beaches in Oceania, where your kids can record their most precious childhood memories, and you and your spouse can rekindle the flame of love that might have mellowed down because of the stresses of everyday life. Romantic couples find these Oceania beach destinations electrifying for their romance, and adventurers discover the excitement to be just too much to handle.</p>


                        </td>
                    </tr>
                </table>
            </div>
       
        <div class="greytext">
            <p>Be it the nude beaches of Australia and New Zealand, the heavenly coasts of Tahiti and the Cook Islands, or the spellbinding seashores of Fiji, Oceania just has no parallel in terms of beauty, climate, and serenity. Most of the Oceania beach resorts offer a variety of tours, boat rides and activities such as scuba diving, snorkelling, jet-skiing, paragliding, and many others.</p>
            <%--<div style="border: solid 0px #ececec; padding: 2px; background-color: white; text-align: center; width: 800px">

                <div style="float: left; width: 413px;">
                    <h3><a class="mainPgCountry" href="http://www.vacations-abroad.com/spain/balearic_islands/ibiza/default.aspx"><b>Ibiza Spain Beach Vacations</b></a></h3>
                </div>
                <div>
                    <h3><a class="mainPgCountry" href="http://www.vacations-abroad.com/spain/balearic_islands/mallorca/default.aspx"><b>Mallorca Spain Beach Vacations</b></a></h3>
                </div>
                <div style="float: left; width: 413px;">
                    <h3><a class="mainPgCountry" href="http://www.vacations-abroad.com/greece/cyclades_islands/santorini/default.aspx"><b>Santorini Greece Beach Vacations</b></a></h3>
                </div>
                <div>
                    <h3><a class="mainPgCountry" href="http://www.vacations-abroad.com/cyprus/paphos/coral_bay/default.aspx"><b>Coral Bay Cyprus Beach Vacations</b></a></h3>
                </div>

            </div>--%>
        </div>
        <div class="linegreen">
        </div>
        </div>
        <%--3rdpart--%>
        <div>
            <%--VBR--%>
            <div class="vbrblue">
                Oceania Exotic Vacations
            </div>
            <%--content VBR--%>
            <div class="contenttextleft">
                <table>
                    <tr>
                        <%--content text--%>
                        <td class="content1 tbody">
                            <p>
                                Oceania destinations are awash with natural beauty and calmness. Millions of weary vacationers board their flights for Oceania destinations each year, looking to rejuvenate and be reborn before returning to their routines with a new gusto. Abundant scenic beauty, excellent facilities, and complete privacy make the South Pacific destinations rank among the best vacation retreats on the planet.
                            </p>
                            <br>
                            <p>
                                Oceania destinations spread out from the coasts of Australia and New Zealand right into the middle of the great Pacific. Oceania city destinations include Sydney, Melbourne, Perth, Auckland, Devonport, Christchurch, and other coastal cities of the two neighbouring countries. Exotic Oceania destinations comprise of a plethora of tiny islands including the Cook Islands, Fiji, Tahiti, and the French Polynesia. Most places, other than, perhaps, the nude beaches are suitable for family vacations.
                            </p>

                            <p>With the exhausting number Oceania destinations available and each one being more exquisite than the others in its own way, it may be hard to make a choice. Much would depend upon the purpose of your vacation. If you are tired of city life and looking for a private time with your family, perhaps the flower-filled tropical jungles and ethereal beaches of the Cook Islands, Tahiti and Fiji are just for you. And, if you want to enjoy the warmth of a slick city while being able to surf the waves and sprawl on white sands, perhaps you should consider Melbourne or Auckland.</p>


                        </td>
                        <%--image--%>
                        <td class="imgright">
                            <img src="/images/oceaniadestinations.jpg" height="233" width="300" alt="">
                        </td>
                    </tr>
                </table>
            </div>
            <div class="greytext">
                <p>The best time to travel to Oceania destinations is from October to March, which is the summer season in the Southern hemisphere. When most areas of Europe and America are buried under snow and the sky is covered by grey clouds, the sun is shining warm and bright every day in the bright-blue Oceania skies. Vacations-abroad.com offers the widest collection of plush and unique vacation rentals at the most attractive Oceania destinations.</p>
                <%--<div style="border: solid 0px #ececec; padding: 2px; background-color: white; text-align: center; width: 800px">

                    <div style="float: left; width: 413px;">
                        <h3><a class="mainPgCountry" href="http://www.vacations-abroad.com/france/rhone_alps/chamonix/default.aspx"><b>Chamonix France Ski Vacations</b></a></h3>
                    </div>
                    <div>
                        <h3><a class="mainPgCountry" href="http://www.vacations-abroad.com/france/rhone_alps/meribel/default.aspx"><b>Meribel France Ski Vacations</b></a></h3>
                    </div>
                    <div style="float: left; width: 413px;">
                        <h3><a class="mainPgCountry" href="http://www.vacations-abroad.com/switzerland/valais/zermatt/default.aspx"><b>Zermatt Switzerland Ski Vacations</b></a></h3>
                    </div>
                    <div>
                        <h3><a class="mainPgCountry" href="http://www.vacations-abroad.com/austria/tyrol/innsbruck/default.aspx"><b>Innsbruck Austria Ski Vacations</b></a></h3>
                    </div>

                </div>--%>
            </div>
            <div class="lineblue">
            </div>
            <div class="clear">
            </div>
        </div>
        <%--4th part begins--%>
        <div>
            <%--VBR--%>
            <div style="float: left;">
                <div class="vbrgreen">
                    Oceania Family Vacations
                </div>
                <div class="contenttextright">
                    <%--image--%>
                    <table>
                        <tr>
                            <td class="imgleft">
                                <img src="/images/oceaniaaccommodation.jpg" height="245px" width="300px" alt="">
                            </td>
                            <%--content VBR--%>
                            <%--content text--%>
                            <td class="content2 tbody">
                                <p>
                                    Finding the perfect Oceania accommodation is not a problem, thanks to the extensive property listings that are available on vacations-abroad.com. Vacation rentals are available to suit every need and budget. The available accommodations range from basic, but comfortable and compact country cottages to modern apartments, expansive villas, and resort-style residences. You can browse through the website to your destination and readily look at a variety of Oceania accommodations available for short and long term rental.
                                </p>
                                <br>
                                <p>
                                    We have taken great care to list only verified and reviewed Oceania accommodations on our website. Oceania city accommodations are situated close to city centres, restaurants and shopping, while beach accommodations are ideally placed at the most picturesque locations. Each property is unique and different from the rest, yet all of them are equipped with every modern luxury and comfort. Located at exclusive spots, these vacation rentals provide easy access to tourist attractions, beaches, and a plethora of exciting activities. Most of them feature their private swimming pools, bars, spas, and orchards to consummate your Oceania vacations.
                                </p>

                                <p>The apartments, B&Bs, cottages, resorts, and other Oceania accommodations are featured along with complete details about rooms, available facilities and rent. Each property is illustrated by multiple interior and exterior views, so that you can book the accommodation that suits your needs perfectly. Regardless of whether you are going to Oceania on a romantic, family, exotic, or adventure vacation, you'll find the ideal abode at vacations-abroad.com.</p>


                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="greytext">
                <p>Booking a splendid yet economical accommodation before departing on your journey to Oceania can save you from unnecessary hassle and expenses. Why take chances when you can look before you book? Feel free to click through to your destination and view the available vacation rentals.</p>
                <%--<div style="border: solid 0px #ececec; padding: 2px; background-color: white; text-align: center; width: 800px">

                    <div style="float: left; width: 413px;">
                        <h3><a class="mainPgCountry" href="http://www.vacations-abroad.com/greece/attica/athens/default.aspx"><b>Athens Greece Vacations</b></a></h3>
                    </div>
                    <div>
                        <h3><a class="mainPgCountry" href="http://www.vacations-abroad.com/spain/madrid/madrid/default.aspx"><b>Madrid Spain Vacations</b></a></h3>
                    </div>
                    <div style="float: left; width: 413px;">
                        <h3><a class="mainPgCountry" href="http://www.vacations-abroad.com/czech_republic/praha/prague/default.aspx"><b>Prague Czech Republic Vacations</b></a></h3>
                    </div>
                    <div>
                        <h3><a class="mainPgCountry" href="http://www.vacations-abroad.com/austria/vienna/vienna/default.aspx"><b>Vienna Austria Vacations</b></a></h3>
                    </div>

                </div>--%>
            </div>
            <div class="linegreen">
            </div>
        </div>
    </div>
    <%-- <div class="tapsection" style="border: 1px;">
        <div class="section" style="position: absolute;top: 86px;left: 72px;">
            <h1 style="width: 230px; text-align: center; font-family: 'Trebuchet MS', Arial, Helvetica, sans-serif; font-size: 21px; color: white; font-weight: bold; text-shadow: #005784 2px 2px;">
               Explore the world <br>in our vacation rentals, vacation apartments, vacation homes, cottages, B&Bs & Hotels                <%--Vacation Apartments and Holiday Rentals in the US and Abroad--%>
    <%--            </h1>

        </div>
</div>--%>

    <!-- Start of StatCounter Code for Default Guide -->
    <script type="text/javascript">
        var sc_project = 3336280;
        var sc_invisible = 1;
        var sc_security = "510252c5";
    </script>

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
    </div>
</asp:Content>
<%--<asp:Content ID="Content3" ContentPlaceHolderID="DefaultPageFeaturedCitiesContainer"
    runat="server">
    <div class="rhtsection frontSmallerImages">
                        <h2>
                            <a style="text-decoration: none;" href="http://www.vacations-abroad.com/italy/lazio/rome/default.aspx">
                                Rome Italy</a></h2>
                        <a style="text-decoration: none;" href="http://www.vacations-abroad.com/italy/lazio/rome/default.aspx">
                            <img src="/images/FrontImage1.jpg" width="181" height="132" alt=""></a>
                    </div>
                    <div class="rhtsection frontSmallerImages">
                        <h2>
                            <a style="text-decoration: none;" href="http://www.vacations-abroad.com/england/london/default.aspx">
                               London England</a></h2>
                        <a style="text-decoration: none;" href="http://www.vacations-abroad.com/england/london/default.aspx">
                            <img  src="/images/FrontImage2.jpg" width="181" height="132" alt=""></a>
                    </div>
                    <div class="rhtsection frontSmallerImages">
                        <h2>
                            <a style="text-decoration: none;" href="http://www.vacations-abroad.com/france/ile_de_france/paris/default.aspx">
                                Paris France</a></h2>
                        <a style="text-decoration: none;" href="http://www.vacations-abroad.com/france/ile_de_france/paris/default.aspx">
                            <img  src="/images/FrontImage3.jpg" width="181" height="130" alt=""></a>
                    </div>
</asp:Content>--%>