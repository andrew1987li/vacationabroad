
var country, state, city, pinCode;
function createCORSRequest(method, url) {
    var xhr = new XMLHttpRequest();

    if ("withCredentials" in xhr) {
        // XHR for Chrome/Firefox/Opera/Safari.
        xhr.open(method, url, true);

    } else if (typeof XDomainRequest != "undefined") {
        // XDomainRequest for IE.
        xhr = new XDomainRequest();
        xhr.open(method, url);

    } else {
        // CORS not supported.
        xhr = null;
    }
    return xhr;
}
function getLocationDetails() {

    var States = document.getElementById('<%= hdnState.ClientID %>').value;
    var Countrys = document.getElementById('<%= hdnCountry.ClientID %>').value;
    latitude1 = document.getElementById("Latitude").value;
    longitude1 = document.getElementById("Longitude").value;
    var url = "http://maps.googleapis.com/maps/api/geocode/json?latlng=" +
            latitude1 + "," + longitude1 + "&sensor=false";
    var xhr = createCORSRequest('POST', url);
    if (!xhr) {
        alert('CORS not supported');
    }

    xhr.onload = function () {

        var data = JSON.parse(xhr.responseText);

        if (data.results.length > 0) {
            var locationDetails;
            var EntCity = document.getElementById('<%= CityNew.ClientID %>').value;

            if (EntCity == '' || EntCity == 'undefined') {
                var hdcty = document.getElementById('city');
                EntCity = hdcty.options[hdcty.selectedIndex].text;
                document.getElementById('<%= hdcity.ClientID %>').value = EntCity;
            }
            var isvalid = "false";
            for (var i = 0; i < data.results.length; i++) {
                locationDetails = data.results[i].formatted_address.toLowerCase();
                if (parseInt(locationDetails.indexOf(EntCity.toLowerCase().replace(/ /g, ''))) >= 0) {
                    isvalid = "true";
                    break;
                }
            }
            if (isvalid == "false") {
                document.getElementById('<%= CityNew.ClientID %>').value = '';
                alert("No location available for provided details.");
                document.getElementById('<%= CityNew.ClientID %>').focus();
            } else {
                document.getElementById('<%= hdnLatitude.ClientID %>').value = latitude1;
                document.getElementById('<%= hdnLongitude.ClientID %>').value = longitude1;
            }

        }
        else {
            alert("No location available for provided details.");
        }

    };

    xhr.onerror = function () {
        alert('Woops, there was an error making the request.');

    };
    xhr.send();

}


function GetLocation() {
    var hdcounty = document.getElementById('country');
    var hdState = document.getElementById('state');
    var hdcnty = document.getElementById('<%= hdnCountry.ClientID %>').value;
    var hdsts = document.getElementById('<%= hdnState.ClientID %>').value;
    if (hdcnty == '' && hdsts == '') {
        document.getElementById('<%= hdnCountry.ClientID %>').value = hdcounty.options[hdcounty.selectedIndex].text;
        document.getElementById('<%= hdnState.ClientID %>').value = hdState.options[hdState.selectedIndex].text;
    }

    var geocoder = new google.maps.Geocoder();
    var address = document.getElementById('<%= CityNew.ClientID %>').value;

    if (address == '' || address == 'undefined' || address == 'Other (please specify)') {
        var hdcty = document.getElementById('city');
        address = hdcty.options[hdcty.selectedIndex].text;
        document.getElementById('<%= hdcity.ClientID %>').value = address;
    }

    if (address != '' && address != 'undefined' && address != 'Other (please specify)') {
        var country = document.getElementById('<%= hdnCountry.ClientID %>').value;
        var state = document.getElementById('<%= hdnState.ClientID %>').value;
        address = address + ', ' + state + ', ' + country;
        geocoder.geocode({ 'address': address }, function (results, status) {
            if (status == google.maps.GeocoderStatus.OK) {
                var latitude = results[0].geometry.location.lat();
                var longitude = results[0].geometry.location.lng();
                document.getElementById("Latitude").value = latitude;
                document.getElementById("Longitude").value = longitude;
                if (document.getElementById('<%= CityNew.ClientID %>').value != '' && document.getElementById('<%= CityNew.ClientID %>').value != 'undefined') {
                    getLocationDetails();
                } else {
                    document.getElementById('<%= hdnLatitude.ClientID %>').value = latitude;
                    document.getElementById('<%= hdnLongitude.ClientID %>').value = longitude;
                }
            } else {
                alert("Request failed.");
            }
        });
    }
}
function GetText(element, option) {
    var text = element.options[element.selectedIndex].text;
    if (option == 'Country') {
        document.getElementById('<%= hdnCountry.ClientID %>').value = text;
    }
    else if (option == 'State') {
        document.getElementById('<%= hdnState.ClientID %>').value = text;
    }
}

function ProcessValidators() {
    var i, val;
    for (i = 0; i < Page_Validators.length; i++) {
        val = Page_Validators[i];
        if (typeof (val.evaluationfunction) == "function") {
            if (eval("val.evaluationfunction == RequiredFieldValidatorEvaluateIsValid;"))
                eval("val.evaluationfunction = RequiredFieldAlertValidate;");
            else if (eval("val.evaluationfunction == RegularExpressionValidatorEvaluateIsValid;"))
                eval("val.evaluationfunction = RegularExpressionAlertValidate;");
        }
    }
}

function RequiredFieldAlertValidate(val) {
    var result;
    result = RequiredFieldValidatorEvaluateIsValid(val)
    if (!result)
        window.alert(val.errormessage);
    return result;
}

function RegularExpressionAlertValidate(val) {
    var result;
    result = RegularExpressionValidatorEvaluateIsValid(val)
    if (!result)
        window.alert(val.errormessage);
    return result;
}


InitializeDropdowns();