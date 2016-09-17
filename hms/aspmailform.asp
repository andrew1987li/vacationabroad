<%
Set Mailer = Server.CreateObject("SMTPsvg.Mailer")
Mailer.FromName = "website"
Mailer.FromAddress= "user@yourdomain.com"
Mailer.RemoteHost = "localhost"
Mailer.AddRecipient "Name", "name@yourdomain.com"
Mailer.AddExtraHeader "X-MimeOLE:Produced yourdomain.com"
Mailer.Subject = "Form Submission"

strMsgHeader = "Form Information Follows: " & vbCrLf
for i = 1 to Request.Form.Count
  strMsgInfo = strMsgInfo & Request.Form.Key(i) & " - " & Request.Form.Item(i) & vbCrLf
next
strMsgFooter = vbCrLf & "End of form information"
Mailer.BodyText = strMsgHeader & strMsgInfo & strMsgFooter
if Mailer.SendMail then
  Response.Write "Form information submitted..."
else
  Response.Write "Mail send failure. Error was " & Mailer.Response
end if
set Mailer = Nothing
%>