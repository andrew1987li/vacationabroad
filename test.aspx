<%@ Page Language="C#" AutoEventWireup="true" CodeFile="test.aspx.cs" Inherits="test" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="countryStateCity.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
<fieldset style="width: 230px;">
<legend><strong>Make your selection</strong></legend>
<%--<p>--%>
<%--<form name="test" method="POST" action="processingpage.php">--%>
<table>
<tr>
<td style="text-align: left;">Region:</td>
<td style="text-align: left;">
<select name="region" id="region" onchange="setCountries();">
  <option value="1">Africa</option>
  <option value="2">Asia</option> 
  <option value="4">Caribbean</option>
  <option value="5">Central America</option>
  <option value="6">Europe</option>
  <option value="7">Middle East</option> 
  <option value="8">North America</option>
  <option value="3">Oceania</option>   
  <option value="9">South America</option>   
</select>
</td>
</tr>
<tr>
<td style="text-align: left;">Country:</td>
<td style="text-align: left;">
<select name="country" id="country" onchange="setStates();">
   <option value="">Please select a Region</option>
</select>
</td>
</tr><tr>
<td style="text-align: left;">State:</td>
<td style="text-align: left;">
<select name="state" id="state" onchange="setCities();">
  <option value="">Please select a Region</option>
</select>
</td>
</tr><tr>
<td style="text-align: left;">City:</td>
<td style="text-align: left;">
<select name="city"  id="city">
  <option value="">Please select a Region</option>
</select>
</td>
</tr>
</table>
<%--</form>--%>
</fieldset>

    </div>
    </form>
</body>
</html>
