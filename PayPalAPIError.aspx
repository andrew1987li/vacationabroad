<%@ Page CodeBehind="PayPalAPIError.aspx.cs" Language="c#" AutoEventWireup="false"  %>

<html>
	<head>
		<title>PayPal SDK - TransactionSearch</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		
	</head>
	<body>
		<form id="TransSearch" method="post" runat="server">
			<P>
				<table class="api" id="Table1">
					<TR>
						<TD class="field"></TD>
						<TD><%=Request.QueryString.Get("ErrorCode")%></TD>
					</TR>
					<TR>
						<TD class="field"></TD>
						<TD><%=Request.QueryString.Get("Desc")%></TD>
					</TR>
										<TR>
						<TD class="field"></TD>
						<TD><%=Request.QueryString.Get("Desc2")%></TD>
					</TR>

				</TABLE>
			</P>
			<P>&nbsp;</P>
			<P>
				
			</P>
		</form>
		
	</body>
</html>
