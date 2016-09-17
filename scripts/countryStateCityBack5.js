/* This script and many more are available free online at
The JavaScript Source!! http://javascript.internet.com
Created by: Michael J. Damato | http://developing.damato.net/ */

function setCountries() {
    if (typeof (initialregion) == "undefined") { }
    else {
        rgSel = document.getElementById('region');
        
        if (initialregion != -1) {
            rgSel.value = initialregion;
        }
        initialregion = -1;
        changeSelect('country', countryregions, countrystrs, rgSel.value, countryids); //send state, country-state, state names, countryid, stateID
        
        setStates();
    }
}

function setStates() {
    cntrySel = document.getElementById('country');
    if (initialcountry != -1) {
        cntrySel.value = initialcountry;

    }
    initialcountry = -1;
    changeSelect('state', provincecountries, provincestrs, cntrySel.value, provinceids); //send state, country-state, state names, countryid, stateID
    //initialcountry = -1;
    //    setCities();
    stateSel = document.getElementById('state');
    if (initialstateprovince != -1) {
        stateSel.value = initialstateprovince;
       // initialstateprovince = -1;
    }
    setCounty();
    if (initialcountry != -1) {
        cntrySel.value = initialcountry;

    }
}

function setCities() {
    stateSel = document.getElementById('state');
    citySel = document.getElementById('city');
    countySel = document.getElementById('county');

    if (countySel.options.length == 0)
        changeSelect('city', cityprovinces, citystrs, stateSel.value, cityids); //send city, state-city, city name, stateid, cityids
    else
        changeSelect('city', cityCounty, citystrs, countySel.value, cityids); //send city, state-city, city name, stateid, cityids

    if (initialcity != -1) {
        citySel.value = initialcity;
        //initialcity = -1;
    }
    //    initialstateprovince = -1;
    //    initialcity = -1;
}

function changeSelect(fieldID, newOptions, newValues, vID, newIDs) {
    selectField = document.getElementById(fieldID);
    selectField.options.length = 0;
    for (i = 0; i < newOptions.length; i++) {
        if (newOptions[i] == vID)
            selectField.options[selectField.length] = new Option(newValues[i], newIDs[i]);

    }
    if (fieldID == 'city') {
        var option2 = document.createElement("option");

        selectField.options.add(option2);

        option2.text = "Other (please specify)";
        option2.value = 0;
        editCities();
    }
}
function editCities() {
    var city = document.getElementById('city');
    var citynew = document.getElementsByName("ctl00$Content$CityNew")[0];
    if (city.value == 0) {
        citynew.style.visibility = "visible";
        citynew.value = "";
    }
    else {
        citynew.style.visibility = "hidden";
        citynew.value = city.text;
    }
}
// Multiple onload function created by: Simon Willison
// http://simonwillison.net/2004/May/26/addLoadEvent/
function addLoadEvent(func) {
    var oldonload = window.onload;
    if (typeof window.onload != 'function') {
        window.onload = func;
    } else {
        window.onload = function () {
            if (oldonload) {
                oldonload();
            }
            func();
        }
    }
}

addLoadEvent(function () {
    //setCountries();
});

function setPriTypes() {
    selectField = document.getElementById("PrimaryType");
    selectField.options.length = 0;

    var option2 = document.createElement("option");

    selectField.options.add(option2);

    option2.text = "Please select primary type";
    option2.value = -1;

    for (i = 0; i < priTypeIDs.length; i++) {
        selectField.options[selectField.length] = new Option(priTypes[i], priTypeIDs[i]);
    }

    if (initialproptype != -1) {
        for (z = 0; z < proptypeids.length; z++) {
            if (proptypeids[z] == initialproptype)
                selectField.value = Priproptypeids[z];
        }
    }

    setPropertyDropDown();
}
function setPropertyDropDown() {

    staticField = document.getElementById("PrimaryType");
    selectField = document.getElementById("PropertyType");
    selectField.options.length = 0;

    if (initialproptype != -1) {
        var vUsed = new Array();
        for (i = 0; i < proptypeids.length; i++) { //cycles through property types
            if (Priproptypeids[i] == staticField.value) {
                //if doesn't exist in list already
                for (n = -1; n < vUsed.length; n++) {
                    if (vUsed[n] != proptypestrs[i]) {
                    if(initialproptype == proptypeids[i]) {
                        selectField.options[selectField.length] = new Option(proptypestrs[i], proptypeids[i]);
                        vUsed[n] = proptypestrs[i];
                    } //initialproptype == proptypeids[i]
                    } //if vUsed
                } //for n=0
            } // if priprop
        }
        selectField.options[selectField.length] = new Option("Be creative and create a unique property type in the field below",
        0);
        if (initialproptype != -1) {
            for (i = 0; i < selectField.options.length; i++) {
                if (selectField.options[i].text == initialpropstring) {
                    selectField.value = selectField.options[i].value;

                } //if
            } //for i
        } //if initialprop

        if (selectField.options.length > 0) {
            PropertyTypeChanged(0);
        }
    } //edit function
    else {
        var option2 = document.createElement("option");

        selectField.options.add(option2);

        option2.text = "Be creative and create a unique property type in the field below";
        option2.value = 0;

        PropertyTypeChanged(0);
    }
}
function InitializeDropdowns() {
    setCountries();
    setPriTypes();
}
function PropertyTypeChanged(selectedindex) {
    var proptype = document.getElementById("PropertyType");
    var proptypenew = document.getElementById("PropertyTypeNew");

    if (proptype.options[selectedindex].value == 0) {
        proptypenew.style.visibility = "visible";
        proptypenew.value = "";
    }
    else {
        proptypenew.style.visibility = "hidden";
        proptypenew.value = proptype.text;
    }
}

function setCounty() {
    stateSel = document.getElementById('state');
    var lbl = document.getElementById("ctl00_Content_lblCounty");


    var blExist = false;

    for (i = 0; i < countiesstates.length; i++) {
        if (countiesstates[i] == stateSel.value) {
            blExist = true;
        }
    }

    changeSelect('county', countiesstates, counties, stateSel.value, countyids);
    countySel = document.getElementById('county');
    if (initialcounty != -1) {
        countySel.value = initialcounty;
        //initialcounty = -1;
    }

    if (blExist == false) {  //find county and hide it
        countySel.style.visibility = "hidden";
        //lbl.style.visibility = "hidden";
    }
    else {
        countySel.style.visibility = "visible";
        //lbl.style.visibility = "visible";
    }

    setCities();
}

function validateForm() {
    staticField = document.getElementById("PrimaryType");
    selectField = document.getElementById("PropertyType");
    ratesText = document.getElementById("ctl00$Content$Rates");
    propName = document.getElementById("ctl00$Content$txtPropName");
    propName1 = document.getElementById("ctl00$Content$PropertyName");
    address = document.getElementById("ctl00$Content$AddressLocation");
    loRate = document.getElementById("ctl00$Content$txtReqLoRate");
    hiRate = document.getElementById("ctl00$Content$txtReqHiRate");
    ddlCurrency = document.getElementById("ctl00$Content$ddlCurrencies");
    numBedrooms = document.getElementById("ctl00$Content$NumBedrooms");
    numBaths = document.getElementById("ctl00$Content$NumBaths");
    numSleeps = document.getElementById("ctl00$Content$NumSleeps");
    numTVs = document.getElementById("ctl00$Content$NumTVs");
    numVCRs = document.getElementById("ctl00$Content$NumVCRs");
    numCDs = document.getElementById("ctl00$Content$NumCDPlayers");

    var vReturn;

    if (staticField.value == -1) {
        alert("Please select primary property type.");
        //        document.forms.PrimaryType.focus();
        vReturn = false;
    }

    if (selectField.value == -1) {
        alert("Please select secondary property type.");
        vReturn = false;
    }

    if ((typeof (vRates) == "undefined") && (ratesText.value == "")) {
        alert("Please enter rate information.");
        vReturn = false;
    }
    if (propName.value == "") {
        alert("Please enter property name.");
        vReturn = false;
    }
    if (propName1.value == "") {
        alert("Please enter property description.");
        vReturn = false;
    }
    if (address.value == "") {
        alert("Please enter address.");
        vReturn = false;
    }
    if (loRate.value == "") {
        alert("Please enter minimum nightly rate.");
        vReturn = false;
    }
    if (hiRate.value == "") {
        alert("Please enter maximum nightly rate.");
        vReturn = false;
    }
    if (ddlCurrency.value == "Currency") {
        alert("Please select rate currency.");
        vReturn = false;
    }
    if (isNaN(loRate.value)) {
        alert("Please enter valid integer number for minimum rate.");
        vReturn = false;
    }
    if (isNaN(hiRate.value)) {
        alert("Please enter valid integer number for maximum rate.");
        vReturn = false;
    }
    if (numBedrooms.value == "") {
        alert("Please enter number of bedrooms.");
        vReturn = false;
    }
    if (isNaN(numBedrooms.value)) {
        alert("Please enter valid integer number for number of bedrooms.");
        vReturn = false;
    }
    if (numBaths.value == "") {
        alert("Please enter number of baths.");
        vReturn = false;
    }
    if (isNaN(numBaths.value)) {
        alert("Please enter valid integer number for number of baths.");
        vReturn = false;
    }
    if (numSleeps.value == "") {
        alert("Please enter sleeps number.");
        vReturn = false;
    }
    if (isNaN(numSleeps.value)) {
        alert("Please enter valid integer sleeps number.");
        vReturn = false;
    }
    if (numTVs.value == "") {
        alert("Please enter number of TVs.");
        vReturn = false;
    }
    if (isNaN(numTVs.value)) {
        alert("Please enter valid integer number of TVs.");
        vReturn = false;
    }
    if (numVCRs.value == "") {
        alert("Please enter number of VCRs.");
        vReturn = false;
    }
    if (isNaN(numVCRs.value)) {
        alert("Please enter valid integer number of VCRs.");
        vReturn = false;
    }
    if (numCDs.value == "") {
        alert("Please enter number of CDs.");
        vReturn = false;
    }
    if (isNaN(numCDs.value)) {
        alert("Please enter valid integer number of CDs.");
        vReturn = false;
    }
    return vReturn;
}

