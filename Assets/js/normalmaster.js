var redirect_links = [["/applications.aspx", "/accounts/login.aspx?type=0"], ["/rentalguarantee.aspx"], ["/aboutus.aspx", "/press/AboutLindaKJenkins.pdf"]
    , ["/presscoverage.aspx", "/pressreleases.aspx"], ["/Contacts.aspx", "http://blog2.vacations-abroad.com", "http://madmimi.com/signups/121428/join", "https://plus.google.com/+Vacations-abroad/posts", "https://twitter.com/vacationsabroad", "https://www.facebook.com/VacationsAbroad"]];

function onclickevent_footerment(menuindex, itemindex) {
    //alert(menuindex + "   " + itemindex);
    window.location.href = redirect_links[menuindex][itemindex];
}

function getcountrylist(item) {

    //console.log(item.id);
    var rid = item.id.split("_")[1];
    if (menuitem[rid] != 0) {
        dropdownbtn(item);
        return;
    }
    console.log("countrylist" + rid);
    call_rid = rid;
    $.ajax({
        type: "POST",
        url: "/AjaxHelper.aspx/GetCountryList",
        data: '{id:' + rid + '}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: processTopCountryData,
        failure: function (response) {
            alert(response.d);
        }
    });
}


function processTopCountryData(response) {
    var statelist = response.d;
    //console.log(statelist);
    if (call_rid != statelist.regionid) return;
    var states = statelist.statelist;
    //$(".ajcountry").empty();
    for (var i = 0; i < states.length; i++) {
        var id = "item" + call_rid + '_' + states[i].id;
        var href = "/" + states[i].name + "/default.aspx";
        var item = ' <li ><a href="'+href+'" class="mmitem" onmouseover="callstateslist(\''+id+'\')" id="' + id + '">' + states[i].name + '</a></li>';
        $("#ajcountry"+call_rid).append(item);
        // $(".statelists").append('<li><a>' + states[i].name + '</a></li>');
    }
    menuitem[call_rid] = 1;
    dropdownbtn("#reg_" + call_rid);
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
var call_rid = 0;
var callcountry = "";
var data_arr = [];
var menuitem = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0];

function callstateslist(cid) {

    $(".itemselected").removeClass("itemselected");
    $("#" + cid).parent().parent().find(".itemselected").removeClass("itemselected");
    $("#" + cid).addClass("itemselected");
    var sp_str = cid.split("_");
    call_cid = sp_str[1];
    callcountry = $("#" + cid).text();
    getmainmenu(call_cid);
}

function dropdownbtn(item){
     $(".statelists").empty();
    $(".allprop").attr("href", "#");
    $(".itemselected").removeClass("itemselected");
    var cid = $(item).parent().find("div div ul .mmitem").first().attr("id");
    $("#" + cid).addClass("itemselected");
    var sp_str = cid.split("_");
    call_cid = sp_str[1];
    callcountry = $("#" + cid).text();
    getmainmenu(call_cid);
}


$(document).ready(function() {
    //mmitem
  /*  $(".mmitem").mouseover(function () {

        callstateslist();
    });
    */
    /*
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
       
    }); */



});

function processTopMenuData(response){
    var statelist = response.d;
    if (call_cid != statelist.countryid) return;
    var states = statelist.statelist;
    $(".statelists").empty();
    for (var i = 0; i < states.length; i++) {
        var link = "http://69.89.14.163:86/" + callcountry.toLowerCase().replace(" ", "_") + "/" + states[i].name.toLowerCase().replace(" ", "_") + "/default.aspx";
         $(".statelists").append('<li><a href="' + link + '">' + states[i].name + '</a></li>');
       // $(".statelists").append('<li><a>' + states[i].name + '</a></li>');
         $(".allprop").attr("href", "http://69.89.14.163:86/" + callcountry.toLowerCase().replace(" ", "_") + "/countryproperties.aspx");
         $(".allprop").text("View all " +callcountry+ " properties");
    }
}

