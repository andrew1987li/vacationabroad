

var sc_project = 3336280;
var sc_invisible = 1;
var sc_security = "510252c5";


function Validate() {
    var vValue = true;
    rb1 = document.getElementById('ctl00_Content_rbnOne');
    rb2 = document.getElementById('ctl00_Content_rbnTwo');
    rb3 = document.getElementById('ctl00_Content_rbnThree');
    rb4 = document.getElementById('ctl00_Content_rbnFour');
    vFName = document.getElementById('ctl00_Content_txtFName');
    vLName = document.getElementById('ctl00_Content_txtLName');
    vComments = document.getElementById('ctl00_Content_txtComments');
    vMonth = document.getElementById('ctl00_Content_ddlMonth');
    vYear = document.getElementById('ctl00_Content_ddlYear');

    if (vFName.value == "") {
        alert('Please enter first name.');
        vValue = false;
    }
    if ((vLName.value == '') || (vLName.value == null)) {
        alert('Please enter last name.');
        vValue = false;
    }
    if (vMonth.value == 0) {
        alert('Please specify month.');
        vValue = false;
    }
    if (vYear.value == 0) {
        alert('Please specify year.');
        vValue = false;
    }
    if (vComments.value == "") {
        alert('Please enter comments.');
        vValue = false;
    }
    if ((rb1.checked == false) && (rb2.checked == false) && (rb3.checked == false) && (rb4.checked == false)) {
        alert('Please enter star rating.');
        vValue = false;
    }
    //        else {
    //            return true;
    //        }
    return vValue;
}