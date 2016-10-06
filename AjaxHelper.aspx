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

</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head >
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    tst
    </div>
    </form>
</body>
</html>
