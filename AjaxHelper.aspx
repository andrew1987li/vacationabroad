<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AjaxHelper.aspx.cs" Inherits="AjaxHelper" %>
<%@ Import Namespace="System.Web.Services" %>

<!DOCTYPE html>
<script runat="server">

    [WebMethod]
    // Get session state value.
    public static AjaxCountryList GetCountryList(int id) {
        return AjaxProvider.getCountryInfo(id);
    }
    [WebMethod]
    // Get session state value.
    public static AjaxStateList GetStateList(int id) {
        return AjaxProvider.getSateInfo(id);
    }

    [WebMethod]
    // Get session state value.AjaxPropListSet
    public static AjaxPropListSet GetPropertyListKeyword(string keyword, int proptype, int amenitytype, int roomnum, int sorttype, int pagenum ) {
         return SearchProvider.getAjaxPropListSet(keyword, proptype, amenitytype, roomnum,sorttype,pagenum);
        // AjaxPropListSet ajx=SearchProvider.getAjaxPropListSet(keyword, proptype, amenitytype, roomnum,sorttype,pagenum);
       // return keyword + proptype + amenitytype + roomnum+sorttype+pagenum;
       // return keyword +ajx.allnums;

    }

   [WebMethod]
    // Get session state value.AjaxPropListSet
    public static AjaxPropListSet GetPropertyListCityID(int cityid, int proptype, int amenitytype, int roomnum, int sorttype, int pagenum) {
         return SearchProvider.getAjaxPropListSetWithCityID(cityid, proptype, amenitytype, roomnum,sorttype,pagenum);
        // AjaxPropListSet ajx=SearchProvider.getAjaxPropListSet(keyword, proptype, amenitytype, roomnum,sorttype,pagenum);
       // return keyword + proptype + amenitytype + roomnum+sorttype+pagenum;
       // return keyword +ajx.allnums;

    }

</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head >
    <title></title>
</head>
<body>

</body>
</html>
