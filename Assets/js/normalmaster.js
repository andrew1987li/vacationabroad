var redirect_links = [["/applications.aspx", "/accounts/login.aspx"], ["/rentalguarantte.aspx"], ["/aboutus.aspx", "/press/AboutLindaKJenkins.pdf"]
    , ["/presscoverage.aspx", "/pressreleases.aspx"], ["/Contacts.aspx", "http://blog2.vacations-abroad.com", "http://madmimi.com/signups/121428/join", "https://plus.google.com/+Vacations-abroad/posts", "https://twitter.com/vacationsabroad","https://www.facebook.com/VacationsAbroad"]];

function onclickevent_footerment(menuindex, itemindex) {
    //alert(menuindex + "   " + itemindex);
    window.location.href = redirect_links[menuindex][itemindex];
}