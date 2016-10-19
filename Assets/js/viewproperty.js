var RecaptchaOptions = {
    theme: 'white'
};

var sc_project = 3614019;
var sc_invisible = 1;
var sc_partition = 41;
var sc_security = "5d0ed9a7";
      var gaJsHost = (("https:" == document.location.protocol) ? "https://ssl." : "http://www.");
document.write(unescape("%3Cscript src='" + gaJsHost + "google-analytics.com/ga.js' type='text/javascript'%3E%3C/script%3E"));



    try {
        var pageTracker = _gat._getTracker("UA-1499424-2");
pageTracker._trackPageview();
    } catch (err) { }

    $(document).ready(function () {
        $(".grdImg2").each(function (index) {
            var height = $(this).height;
            var width = $(this).width();
            if (height == 0 || width == 0) {
                $(this).hide();
            }
        });
    })
    var $element;
    $(document).ready(function () {
        $element = $('#modal_contents').bind('webkitAnimationEnd', function () {
            this.style.webkitAnimationName = '';
        });

        $('ul.tabs li').click(function () {
            var tab_id = $(this).attr('data-tab');

            $('ul.tabs li').removeClass('current');
            $('.tab-content').removeClass('current');

            $(this).addClass('current');
            $("#" + tab_id).addClass('current');
            if (tab_id == "tabs-3") {
                console.log("tst");


                
                $element.css('webkitAnimationName', 'animatetop');
                    // you'll probably want to preventDefault here.
                
                $('#inqureform').show();
            }
        });

        $('.mclose').click(function () {
            $('#inqureform').hide();
        })

    })

    $(window).click(function (event) {
        console.log("windows click");
        // $('#inqureform').hide();
        if (event.target.id == "inqureform") {
            $('#inqureform').hide();
        }
    })