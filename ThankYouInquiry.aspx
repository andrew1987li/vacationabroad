<%@ Page Language="C#" MasterPageFile="~/MasterPageNoCss.master" AutoEventWireup="true" CodeFile="~/ThankYouInquiry.aspx.cs" Inherits="ThankYouInquiry" Title="Thank You!" EnableEventValidation="false" %>

<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="Server">
    <div id="thank-you-inquiry" style="max-height:555px;min-height:555px;">
        <div class="message" style="min-height:450px!important;">
            <span style="">Thank you! Your email is on its way to the owner.</span>
            <br />
            <asp:Image ID="Image1"
                runat="server"
                AlternateText="Life is a Journey Image"
		Width="525px"
		Height="375px"
                ImageUrl="/images2/linda_map-small.jpg" />
        </div>
        <div class="message msg2" style="min-height:350px!important;">
            <span style="">And now we are sending you to the city of your choice<br> so you
                can review additional vacation properties.
            </span>
        </div>
        <br />
        <br />
        <!-- Google Code for Vacations Abroad Conversion Page -->
        <script type="text/javascript">
            /* <![CDATA[ */
            var google_conversion_id = 1020514987;
            var google_conversion_language = "en";
            var google_conversion_format = "1";
            var google_conversion_color = "ffffff";
            var google_conversion_label = "YgGRCJXBmwcQq6XP5gM";
            var google_conversion_value = 0;
            /* ]]> */
        </script>
        <script type="text/javascript" src="//www.googleadservices.com/pagead/conversion.js">
        </script>
        <noscript>
            <div style="display: inline;">
                <img height="1" width="1" style="border-style: none;"
                    alt="" src="//www.googleadservices.com/pagead/conversion/1020514987/?value=0&amp;label=YgGRCJXBmwcQq6XP5gM&amp;guid=ON&amp;script=0" />
            </div>
        </noscript>
        <asp:HiddenField ID="hdnRTC" runat="server" />
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            // if the query string value is available, just do a redirect after specified number of seconds.
            var redirectTo = $("input[id$='hdnRTC']").val();
            if (redirectTo != "") {
                setTimeout(function () {
                    document.location = redirectTo;
                }, 8000)
            }
            function GetParameterValues(param) {
                var url = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
                for (var i = 0; i < url.length; i++) {
                    var urlparam = url[i].split('=');
                    if (urlparam[0] == param) {
                        return urlparam[1];
                    }
                }
            }
            var redirect = GetParameterValues('redirect');
            if (redirect == undefined || redirect == "") {
                setTimeout(function () {
		document.location.href = "http://www.vacations-abroad.com";
		}, 8000);
            }
        });
    </script>
</asp:Content>
