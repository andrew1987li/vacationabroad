

var sc_project = 3341533;
var sc_invisible = 1;
var sc_security = "ebe10c56";

var pager = new Pager('results', 10);
pager.init();
pager.showPageNav('pager', 'pageNavPosition');
pager.showPage(1);


try {
    var pageTracker = _gat._getTracker("UA-1499424-2");
    pageTracker._trackPageview();
} catch (err) { }

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