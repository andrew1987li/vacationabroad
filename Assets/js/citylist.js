var gaJsHost = (("https:" == document.location.protocol) ? "https://ssl." : "http://www.");
document.write(unescape("%3Cscript src='" + gaJsHost + "google-analytics.com/ga.js' type='text/javascript'%3E%3C/script%3E"));



    try {
        var pageTracker = _gat._getTracker("UA-1499424-2");
pageTracker._trackPageview();
    } catch (err) { }


    sc_project = 3345790;
    sc_invisible = 1;
    sc_partition = 36;
    sc_security = "b7bf8208";


    var cur_page = 0;
    var max_page = 1;
    var refresh_flag = 0;

    $(document).ready(function () {
        console.log("ready");
        refreshprop();
    });

    function refreshprop() {
      //  return;
        var cont= ' <div class="loading-bro">  <h1>Searching...</h1>  <svg id="load" x="0px" y="0px" viewBox="0 0 150 150"> <circle id="loading-inner" cx="75" cy="75" r="60"/></svg>  </div>';
        $('.pcontent').empty().append(cont);
        refresh_flag = 1;
        cur_page = 0;
        callProplistfunction(cur_page);
    }

    function callProplistfunction(pagenum) {
        var cityid = $('#cityid').val();
        var roomnums = $('input[name=roomnums]:checked').val();
        var amenitytype = $('input[name=amenitytype]:checked').val();
        var pricesort = $('input[name=pricesort]:checked').val();
        var proptype = $('input[name=proptype]:checked').val();
        getpropertylist(cityid, proptype, amenitytype, roomnums, pricesort, pagenum);
    }

    function getpropertylist(cityid, proptype, amenitytype, roomnum, sorttype, pagenum) {
        console.log("call ajax");
        // cur_page = 0;
        //string keyword, int proptype, int amenitytype, int roomnum
        $.ajax({
            type: "POST",
            url: "/AjaxHelper.aspx/GetPropertyListCityID",
            data: '{cityid:"' + cityid + '",proptype:' + proptype + ',amenitytype:' + amenitytype + ',roomnum:' + roomnum + ',sorttype:' + sorttype + ',pagenum:' + pagenum + '}',
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
        //"2 Nights","3 Nights", "1 Week", "2 Weeks", "Monthly", "1 Night"
    //Category
    //var prop_typeval = [17, 4, 1, 2, 9, 15, 16, 5, 11, 13, 0];
    var prop_typeval = [8, 2, 5, 16, 11, 24, 2, 19, 22, 12];

    function displayContent(proplist) {
        $('.pcontent').empty();
        var count = proplist.length;
        for (i = 0; i < count; i++) {
            var prop = proplist[i];
            // Sixth Arrondissement Hotel 21 Bedroom 21 BA Sleeps 42
            var propname = prop.detail.PropertyName + ' ' + prop.detail.NumBedrooms + ' Bedroom ' + prop.detail.NumBaths + ' BA Sleeps ' + prop.detail.NumSleeps;
            // Rates:  79-169 EUR Per Night 2 nights Minimum 
            var rates = 'Rates: ' + prop.detail.MinNightRate + '-' + prop.detail.HiNightRate + '  ' + prop.detail.MinRateCurrency + ' Per Night ' + min_rentaltypes[prop.detail.MinimumNightlyRentalID];
            var amenity = "Amenity:  ";
            var am_count = prop.amenity.length;
            var href = "/" + prop.detail.Country + "/" + prop.detail.StateProvince + "/" + prop.detail.City + "/" + prop.detail.ID + "/default.aspx";
            var ahref = "/" + prop.detail.Country + "/" + prop.detail.StateProvince + "/" + prop.detail.City + "/default.aspx";
            var chref = "/" + prop.detail.Country + "/" + prop.detail.StateProvince + "/default.aspx";
            // var alt = (prop_typeval.indexOf(prop.detail.Category) == -1) ? prop.detail.City + " " + prop.detail.NumBedrooms +" bedroom Vacation Rentals" : prop.detail.City + " " + prop.detail.NumBedrooms+" bedroom Boutique Hotels";
            //var alt = (prop_typeval.indexOf(prop.detail.Category) == -1) ?"Rentals" : "Hotel";
            //console.log(am_count);
            var alt = (prop_typeval.indexOf(prop.detail.Category) == -1) ? prop.detail.City + " " + prop.detail.NumBedrooms + " bedroom Vacation Rental" : prop.detail.City + " " + prop.detail.NumBedrooms + " bedroom Hotel";
            for (j = 0; j < am_count; j++) {
                amenity += (prop.amenity[j].Amenity + ', ');
            }
            amenity = amenity.substring(0, amenity.length - 2);
            
            var item_cont='<div class="newrow">\
                 <div class="col-2">\
                     <div class="drop-shadow effect4">\
                       <a href="' + href + '"> <img title="' + alt + '" alt="' + alt + '" src="/images/' + prop.detail.FileName + '"/></a>\
                     </div>\
                     <div class="newrow">\
                         <label class="imgtitle">'+
                                prop.detail.Name2 +
                        ' </label>\
                     </div>\
                 </div>          \
                <div class="col-6">\
                    <div class="explaination">\
                        <div class="ex_con1">\
                            <a href="'+href+'"> ' + propname + '</a>\
                        </div>\
                        <div class="ex_con2">'+rates+
                       ' </div>\
                        <div class="ex_con2">'+
                        amenity +
                       ' </div>\
                        <div class="ex_con3">'
                        +prop.detail.Name+
                       ' </div>\
                    </div>\
                </div>\
                </div>';
            $('.pcontent').append(item_cont);
        }
    }
    var min_groupnum = 0, max_group = 0, cpagenums=0;

    function showPagination(cur_group) {
        max_group = Math.min(max_page, cur_group + 10);
        min_groupnum = cur_group;

        /* $('#paging').empty();
         $('#paging').append('<li><a onclick="backpage()">«</a></li>');
         for (i = cur_group; i < max_group; i++) {
             $('#paging').append('<li><a onclick="getpage(' + i + ')" id="page' + i + '">' + (i + 1) + '</a></li>');
         }
         $('#paging').append('<li><a onclick="nextpage()">»</a></li>');
         $('#page' + cur_page).addClass("curpage");
         */
        $('#paging').empty();
        $('#paging').append('<span class="pg-normal" onclick="backpage()">«Prev</span>');
        for (i = cur_group; i < max_page; i++) {
            $('#paging').append('|<span class="pg-normal" onclick="getpage(' + i + ')" id="page' + i + '">' + (i + 1) + '</span>');
        }
        $('#paging').append('|<span class="pg-normal"  onclick="nextpage()"> Next»</span>');
        $('#page' + cur_page).addClass("pg-selected");
    }

    function addPagination(allnums) {
        max_page = Math.ceil(allnums / 20);
        console.log(max_page + '  ' + allnums);
        min_groupnum = cur_page;
        showPagination(cur_page);
        //curpage
    }

    function callPropAjax() {
        var cont= ' <div class="loading-bro">  <h1>Searching...</h1>  <svg id="load" x="0px" y="0px" viewBox="0 0 150 150"> <circle id="loading-inner" cx="75" cy="75" r="60"/></svg>  </div>';
        $('.pcontent').empty().append(cont);
        // $('.pagination').find(".curpage").removeClass("curpage");
        // $('#page' + cur_page).addClass("curpage");
        $('.pagination').find(".pg-selected").removeClass("pg-selected");
        $('#page' + cur_page).addClass("pg-selected");
        //return;
        callProplistfunction(cur_page);
    }

    function getpage(pagenum) {
        if (cur_page == pagenum) return;
        cur_page = pagenum;
        console.log(min_groupnum + ' ' + max_group + ' ' + cur_page);
        /* if (cur_page == min_groupnum && min_groupnum != 0) {
     
             showPagination(Math.max(cur_page - 4,0));
         }
         else if (cur_page == (max_group - 1) && cur_page != (max_page - 1)) showPagination(cur_page - 4);
         */
    
        //callPropAjax();
        changedPage();
    }
    function changedPage() {
        /*  if (cur_page == min_groupnum && min_groupnum != 0) {
      
              
          }
          else if (cur_page == (max_group - 1) && cur_page != (max_page - 1)) showPagination(cur_page - 4);
          */
        // showPagination(cur_page);
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