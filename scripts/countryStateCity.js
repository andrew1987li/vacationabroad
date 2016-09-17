function setCountries(){if(initialregion!=undefined){rgSel=document.getElementById('region');if(initialregion!=-1){rgSel.value=initialregion;}
changeSelect('country',countryregions,countrystrs,rgSel.value,countryids);initialregion=-1;setStates();}}
function setStates(){cntrySel=document.getElementById('country');if(initialcountry!=-1){cntrySel.value=initialcountry;}
changeSelect('state',provincecountries,provincestrs,cntrySel.value,provinceids);initialcountry=-1;setCities();}
function setCities(){stateSel=document.getElementById('state');citySel=document.getElementById('city');if(initialstateprovince!=-1){stateSel.value=initialstateprovince;}
changeSelect('city',cityprovinces,citystrs,stateSel.value,cityids);if(initialcity!=-1)
{citySel.value=initialcity;}
initialstateprovince=-1;initialcity=-1;}
function changeSelect(fieldID,newOptions,newValues,vID,newIDs){selectField=document.getElementById(fieldID);selectField.options.length=0;for(i=0;i<newOptions.length;i++){if(newOptions[i]==vID)
selectField.options[selectField.length]=new Option(newValues[i],newIDs[i]);}
if(defaultpage!=true){if(fieldID=='city'){var option2=document.createElement("option");selectField.options.add(option2);option2.text="Other (please specify)";option2.value=0;editCities();}}}
function editCities(){var city=document.getElementById('city');var citynew=document.getElementById("ctl00_Content_CityNew");if(citynew!=null){if(city.value==0){citynew.style.visibility="visible";citynew.value="";}
else{citynew.style.visibility="hidden";citynew.value=city.text;}}}
function addLoadEvent(func){var oldonload=window.onload;if(typeof window.onload!='function'){window.onload=func;}else{window.onload=function(){if(oldonload){oldonload();}
func();}}}
addLoadEvent(function(){setCountries();});function InitializeDropdowns(){var i;var proptype=document.getElementById("PropertyType");for(i=0;i<numproptypes;i++){var option=document.createElement("option");proptype.options.add(option);option.name=proptypeids[i];option.text=proptypestrs[i];option.value=proptypeids[i];}
if(proptype.options.length>0){PropertyTypeChanged(0);if(initialproptype!=-1)
for(i=0;i<proptype.options.length;i++)
if(proptype.options[i].value==initialproptype){proptype.value=initialproptype;PropertyTypeChanged(i);break;}}}
function PropertyTypeChanged(selectedindex){var proptype=document.getElementById("PropertyType");var proptypenew=document.getElementById("PropertyTypeNew");if(proptype.options[selectedindex].value==0){proptypenew.style.visibility="visible";proptypenew.value="";}
else{proptypenew.style.visibility="hidden";proptypenew.value=proptype.text;}}