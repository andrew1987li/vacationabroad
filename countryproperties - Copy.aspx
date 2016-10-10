<%@ Page Language="C#" MasterPageFile="~/MasterPageNoCss.master" AutoEventWireup="true"
    CodeFile="countryproperties - Copy.aspx.cs" Inherits="allPropertiesList" EnableEventValidation="false" %>

<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="Server">
    <asp:Label ID="Label2" runat="server" Visible="false" Text="%country% vacation Rentals, %country% Holiday Rentals, %country% Rental Accommodations"></asp:Label>
    <asp:Label ID="Title" runat="server" Visible="false" Text="Vacation Properties in %country%"></asp:Label>
    <asp:Label ID="Keywords" runat="server" Visible="false" Text="%country% apartments, %country% villas, %country% hotels, %country% B&Bs."></asp:Label>
    <asp:Label ID="Description" runat="server" Visible="false" Text="%country%  Find and Book : Apartments, Villas, Hotels and B&Bs"></asp:Label>
    <asp:TextBox runat="server" ID="txtCityVal" value="" Style="display: none;"></asp:TextBox>
    <asp:TextBox runat="server" ID="txtCityVal2" value="" Style="display: none;"></asp:TextBox>
    <input type="hidden" name="step1radio" value="" />
    <input type="hidden" name="step2radio" value="" />
    <input type="hidden" name="step3radio" value="" />

    <script type="text/javascript">
        $(document).ready(function () {
            var href = $(".lower").attr("href");
            href = href.toLowerCase().split(" ").join("_");
            $(".lower").attr("href", href);

            var hrefs = $(".lowers").attr("href");
            hrefs = hrefs.toLowerCase().split(" ").join("_");
            $(".lowers").attr("href", hrefs);

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
            $("input[name='ctl00$Content$rdoTypes']").click(function () {
                value = $(this).val();
                $('input[name="step1radio"]').val(value);
                $('input[name="step2radio"]').val("");
                $('input[name="step3radio"]').val("");
            });

            $("input[name='ctl00$Content$rdoBedrooms']").click(function () {
                value = $(this).val();
                $('input[name="step2radio"]').val(value);
                value = $('input[name="ctl00$Content$rdoTypes"]:checked').val();
                $('input[name="step1radio"]').val(value);
                $('input[name="step3radio"]').val("");
            });

            $("input[name='ctl00$Content$btnFilter']").click(function () {
                value = $('input[name="ctl00$Content$rdoFilter"]:checked').val();
                $('input[name="step3radio"]').val(value);
                value = $('input[name="ctl00$Content$rdoBedrooms"]:checked').val();
                $('input[name="step2radio"]').val(value);
                value = $('input[name="ctl00$Content$rdoTypes"]:checked').val();
                $('input[name="step1radio"]').val(value);
            });
        });
    </script>
    <style type="text/css">    
            .pg-normal {
                color: #6699FF;
                font-weight: normal;
                font-size: 14px;
                text-decoration: none;    
                cursor: pointer;    
            }
            .pg-selected {
                color: #6699FF;
                font-size: 14px;
                font-weight: bold;        
                text-decoration: underline;
                cursor: pointer;
            }
            .listingPagesContainer a {
    float: left;
    font-size: 1.3em;
    color: #6699FF;
}

    .listingPagesContainer a:visited {
        color: #6699FF;
    }

    .listingPagesContainer a:hover {
        color: #6699FF;
    }

            a:link {
    color: #6699FF;
}
            a:hover {
    color: #6699FF;
}a:visited {
    color: #6699FF;
    text-decoration:underline;
}
        </style>

    <span id="test234" runat="server" style="display: none"></span>
    <div align="center" style="position: relative; top: -80px">
        <table>
            <tr>
                <td>
                    <div  class="listingPagesContainer">
                        <asp:HyperLink ID="hyplnkCountryBackLink" runat="server"><h2><asp:Literal ID="ltrCountryBackText" runat="server"></asp:Literal></h2></asp:HyperLink>
                        <div class="clear"></div>
                        <h1 class="listingPagesH1Color H1CityText" style="text-align: center;position: relative; top: -5px" >
                            <asp:Literal ID="ltrH11" runat="server"></asp:Literal>
                            <br />
                        </h1>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <table style="width: 900px;" border="0">
                            <tr>
                                <td>
                                    <div style='padding-left: 10px; padding-right: 10px; padding-bottom: 10px;'>
                                            <div id="filerMain" style="float: left; width: 900px; font-family: Arial; color: #9b7f59; font-size: 12px; border: 2px solid #ACA593;">
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td style="color: #9b7f59 !important;font-size: 12px !important;text-align: center !important;vertical-align: top !important;width: 55px !important;">
                                                            Step 1:
                                                        </td>
                                                        <td style="vertical-align:top;">
                                                            <asp:RadioButtonList ID="rdoBedrooms" runat="server" RepeatDirection="Horizontal" RepeatColumns="6"
                                                                                 RepeatLayout="Flow" AutoPostBack="True" OnSelectedIndexChanged="rdoBedrooms_SelectedIndexChanged"/>
                                                        </td>
                                                    </tr>
                                                    <tr style="height:27px;">
                                                        <td style="color: #9b7f59 !important;font-size: 12px !important;text-align: center !important;vertical-align: top !important;width: 55px !important;">
                                                            Step 2:
                                                        </td>
                                                        <td>
                                                            <asp:RadioButtonList ID="rdoFilter" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                                <asp:ListItem>Sleeps 1-4</asp:ListItem>
                                                                <asp:ListItem>Sleeps 5-8</asp:ListItem>
                                                                <asp:ListItem>Sleeps 9+</asp:ListItem>
                                                                <asp:ListItem Selected="True">Display All</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </td>
                                                        <td rowspan="2" style="width: 117px !important;"><div style="margin-left: -50px">
                                                            <asp:Button ID="btnFilter" runat="server" Text="Search" Style="width: 117px !important;white-space: normal;white-space: normal;border-radius: 1em;
                                                                    color: white;font-family: arial; font-size: 12px;background: #154890;cursor:pointer;font-weight: bold;height: 26px;right: 6px;top: -22px;
                                                                    
                                                                    width: 120px;box-shadow: 2px 2px 6px #154890;border: 1px solid #154890;"
                                                                OnClick="btnFilter_Click" CausesValidation="False"
                                                                OnClientClick="$('#ctl00_Content_selectedRdoTypes').val($('.test').find('input:checked')[0].value)" />
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                     </div>
                                </td>
                            </tr>
                            <tr style="margin-top: 10px">
                                    <td colspan="5">
                                        <div class="PurpleTable" id="allcountryproperties" runat="server"/>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            
        </table>
        <asp:Label ID="lblInfo22" runat="server" ForeColor="Red"></asp:Label>
        <p align="left">
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
                    target="_blankre">
                    <img class="statcounter"
                        src="http://c.statcounter.com/3341533/0/ebe10c56/1/"
                        alt="web counter"></a>
            </div>
        </noscript>

            <script type="text/javascript" src="../scripts/pager.js"></script>	
            <script type="text/javascript"><!--
        var pager = new Pager('results', 10); 
        pager.init(); 
        pager.showPageNav('pager', 'pageNavPosition'); 
        pager.showPage(1);
    </script>

            <script type="text/javascript">
                try {
                    var pageTracker = _gat._getTracker("UA-1499424-2");
                    pageTracker._trackPageview();
                } catch (err) { }</script>

        </p>
    </div>
</asp:Content>
