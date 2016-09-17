<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TwitterAuthentication.aspx.cs" Inherits="TwitterAuthentication" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1">
<title>Twitter Login Authentication for Website in asp.net</title>
</head>
<body>
<form id="form1" runat="server">
<asp:ImageButton ID="imgTwitter" runat="server" ImageUrl="~/TwitterSigning.png" 
onclick="imgTwitter_Click" />
<table id="tbleTwitInfo" runat="server" border="1" cellpadding="4" cellspacing="0" visible="false">
<tr>
<td colspan="2"><b>Twitter User Profile</b></td>
</tr>
<tr>
<td><b>UserName:</b></td>
<td><%=username%></td>
</tr>
<tr>
<td><b>Full Name:</b></td>
<td><%=name%></td>
</tr>
<tr>
<td><b>Profile Image:</b></td>
<td><img src="<%=profileImage%>" /></td>
</tr>
<tr>
<td><b>Twitter Followers:</b></td>
<td><%=followersCount%></td>
</tr>
<tr>
<td><b>Number Of Tweets:</b></td>
<td><%=noOfTweets%></td>
</tr>
<tr>
<td><b>Recent Tweet:</b></td>
<td><%=recentTweet%></td>
</tr>
</table>
</form>
</body>
</html>