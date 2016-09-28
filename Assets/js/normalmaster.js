var redirect_links = [["/applications.aspx", "/accounts/login.aspx"], ["/rentalguarantte.aspx"], ["/aboutus.aspx", "/press/AboutLindaKJenkins.pdf"]
    , ["/presscoverage.aspx", "/pressreleases.aspx"], ["/Contacts.aspx", "http://blog2.vacations-abroad.com", "http://madmimi.com/signups/121428/join", "https://plus.google.com/+Vacations-abroad/posts", "https://twitter.com/vacationsabroad","https://www.facebook.com/VacationsAbroad"]];

function onclickevent_footerment(menuindex, itemindex) {
    //alert(menuindex + "   " + itemindex);
    window.location.href = redirect_links[menuindex][itemindex];
}

function getmainmenu(cid) {
    $.ajax({
        type: "POST",
        url: "/AjaxHelper.aspx/GetStateList",
        data: '{id:'+cid+'}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: processTopMenuData,
        failure: function (response) {
            alert(response.d);
        }
    });



}

var call_cid = 0;
var callcountry = "";
var data_arr = [];

$(document).ready(function() {
    //mmitem
    $(".mmitem").mouseover(function () {

        var cid = this.id;
        $(this).parent().parent().find(".itemselected").removeClass("itemselected");
        $("#" + cid).addClass("itemselected");
        var sp_str = cid.split("_");
        call_cid = sp_str[1];
        callcountry = $("#" + cid).text();
        getmainmenu(call_cid);
    });
    $(".dropbtn").mouseover(function () {
        $(".statelists").empty();
        $(".allprop").attr("href", "#");
        $(".itemselected").removeClass("itemselected");
        var cid = $(this).parent().find("div div ul .mmitem").first().attr("id");
        $("#" + cid).addClass("itemselected");
        var sp_str = cid.split("_");
        call_cid = sp_str[1];
        callcountry = $("#" + cid).text();
        getmainmenu(call_cid);
    });

});

function processTopMenuData(response){
    var statelist = response.d;
    if (call_cid != statelist.countryid) return;
    var states = statelist.statelist;
    $(".statelists").empty();
    for (var i = 0; i < states.length; i++) {
        var link = "http://69.89.14.163:86/"+callcountry+"/"+states[i].name+"/default.aspx";
        $(".statelists").append('<li><a href="' + link + '">' + states[i].name + '</a></li>');
        $(".allprop").attr("href", "http://69.89.14.163:86/" + callcountry + "/countryproperties.aspx");
    }
}

