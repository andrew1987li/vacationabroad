var cur_page = 0;
var max_page = 1;
var refresh_flag = 0;

$(document).ready(function () {
    console.log("ready");
    refreshprop();
});

function refreshprop() {
   var cont= ' <div class="loading-bro">  <h1>Searching...</h1>  <svg id="load" x="0px" y="0px" viewBox="0 0 150 150"> <circle id="loading-inner" cx="75" cy="75" r="60"/></svg>  </div>';
   $('.pcontent').empty().append(cont);
   refresh_flag = 1;
    cur_page = 0;
    callProplistfunction(cur_page);
}

function callProplistfunction(pagenum) {
    var keyword = $('#strkeyword').val();
    var roomnums = $('input[name=roomnums]:checked').val();
    var amenitytype = $('input[name=amenitytype]:checked').val();
    var pricesort = $('input[name=pricesort]:checked').val();
    var proptype = $('input[name=proptype]:checked').val();
    getpropertylist(keyword, proptype, amenitytype, roomnums, pricesort, pagenum);
}

function getpropertylist(keyword, proptype, amenitytype, roomnum, sorttype, pagenum) {
    console.log("call ajax");
   // cur_page = 0;
    //string keyword, int proptype, int amenitytype, int roomnum
    $.ajax({
        type: "POST",
        url: "/AjaxHelper.aspx/GetPropertyListKeyword",
        data: '{keyword:"' + keyword + '",proptype:' + proptype + ',amenitytype:' + amenitytype + ',roomnum:' + roomnum +',sorttype:'+sorttype +',pagenum:'+pagenum+'}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: processPropertyData,
        failure: function (response) {
            console.log(response.d);
        }
    });
}



function processPropertyData(response) {
   // console.log(response.d);
    var ajaxproplist = response.d.propertyList;
    var allnums = response.d.allnums;
    //console.log(allnums);
    // console.log(ajaxproplist);
    if(refresh_flag==1)addPagination(allnums);
    refresh_flag = 0;
    if (allnums != 0) displayContent(ajaxproplist);
    else {
        $('.pcontent').empty().append('<div class="newrow centered">No results</div>')
    }
}

var min_rentaltypes = ["None","2 Nights", "3 Nights", "1 Week", "2 Weeks", "Monthly", "1 Night"];
//Category
//var prop_typeval = [17, 4, 1, 2, 9, 15, 16, 5, 11, 13, 0];
var prop_typeval = [2,5,11];

function displayContent(proplist) {
    $('.pcontent').empty();
    var count = proplist.length;
    for (i = 0; i < count; i++) {
        var prop = proplist[i];
        var propname = 'Sleeps ' + prop.detail.NumSleeps ;
        var rates = 'Rates: ' + prop.detail.MinNightRate + '-' + prop.detail.HiNightRate + '  ' + prop.detail.MinRateCurrency ;
        var amenity = "Amenity:  ";
        var am_count = prop.amenity.length;
        var href = "/" + prop.detail.Country + "/" + prop.detail.StateProvince + "/" + prop.detail.City + "/" + prop.detail.ID + "/default.aspx";
        var ahref = "/" + prop.detail.Country + "/" + prop.detail.StateProvince + "/" + prop.detail.City + "/default.aspx";
        var chref = "/" + prop.detail.Country + "/" + prop.detail.StateProvince + "/default.aspx";
        var alt = (prop_typeval.indexOf(prop.detail.Category) == -1) ? prop.detail.City + " " + prop.detail.NumBedrooms +" bedroom Vacation Rentals" : prop.detail.City + " " + prop.detail.NumBedrooms+" bedroom Boutique Hotels";
        //console.log(am_count);
        /*for (j = 0; j < am_count; j++) {
            amenity += (prop.amenity[j].Amenity + ', ');
        }
        amenity = amenity.substring(0, amenity.length - 2);
        */
        var item_cont=ont='<div class="newitem centered"> \
                        <div class="newrow"> <a href="' + ahref.toLowerCase().replace(" ","_") + '" class="wlocation">' +
                             prop.detail.City + '</a>,<a href="' + chref.toLowerCase().replace(" ", "_") + '" class="wlocation">' + prop.detail.StateProvince +
                        '</a></div>\
                       <div class="newrow">\
                           <a href="' + href.toLowerCase().replace(" ", "_") + '"> <img title="'+alt+'" alt="'+alt+'" class="imgstyle" src="images/' + prop.detail.FileName + '" /> </a>\
                        </div>\
                        <div class="comments">\
                        <div class="newrow"><span class="clocation">' +
                            prop.detail.CategoryTypes +
                        '</span>\
                        <span class="clocation">'
                            +propname+
                        '</span>\
                          <span class="clocation">' +
                           rates+
                    '</span> </div>\
                        </div>\
                             </div>';
        $('.pcontent').append(item_cont);
    }
}
var min_groupnum = 0, max_group = 0, cpagenums=0;

function showPagination(cur_group) {
    max_group = Math.min(max_page, cur_group + 10);
    min_groupnum = cur_group;

    $('#paging').empty();
    $('#paging').append('<li><a onclick="backpage()">«</a></li>');
    for (i = cur_group; i < max_group; i++) {
        $('#paging').append('<li><a onclick="getpage(' + i + ')" id="page' + i + '">' + (i + 1) + '</a></li>');
    }
    $('#paging').append('<li><a onclick="nextpage()">»</a></li>');
    $('#page' + cur_page).addClass("curpage");
}

function addPagination(allnums) {
    max_page = Math.ceil(allnums / 12);
    console.log(max_page + '  ' + allnums);
    min_groupnum = cur_page;
    showPagination(cur_page);
    //curpage
}

function callPropAjax() {
    var cont= ' <div class="loading-bro">  <h1>Searching...</h1>  <svg id="load" x="0px" y="0px" viewBox="0 0 150 150"> <circle id="loading-inner" cx="75" cy="75" r="60"/></svg>  </div>';
    $('.pcontent').empty().append(cont);
    $('.pagination').find(".curpage").removeClass("curpage");
    $('#page' + cur_page).addClass("curpage");
    //return;
    callProplistfunction(cur_page);
}

function getpage(pagenum) {
    if (cur_page == pagenum) return;
    cur_page = pagenum;
    console.log(min_groupnum + ' ' + max_group + ' ' + cur_page);
    if (cur_page == min_groupnum && min_groupnum != 0) {

        showPagination(Math.max(cur_page - 4,0));
    }
    else if (cur_page == (max_group - 1) && cur_page != (max_page - 1)) showPagination(cur_page - 4);
    callPropAjax();
}
function changedPage() {
    if (cur_page == min_groupnum && min_groupnum != 0) {

        showPagination(Math.max(cur_page - 4, 0));
    }
    else if (cur_page == (max_group - 1) && cur_page != (max_page - 1)) showPagination(cur_page - 4);
    callPropAjax();
}


function backpage() {
    if (cur_page > 0) cur_page--;
    else return;
    changedPage();
}

function nextpage() {
    if ((cur_page + 1) < max_page) cur_page++;
    else return;
    changedPage();
}