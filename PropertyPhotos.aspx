<%@ Page Language="C#" MasterPageFile="~/masterpage/NormalMaster.master" AutoEventWireup="true" CodeFile="PropertyPhotos.aspx.cs" Inherits="PropertyPhotos" Title="Property Photos" %>
<asp:Content ID="head" ContentPlaceHolderID="head" runat="server">
    Property Photos
</asp:Content>
<asp:Content ID="links" ContentPlaceHolderID="links" runat="server">
    <link href="/Assets/css/photoproperty.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content" ContentPlaceHolderID="bodycontent" Runat="Server">
    <div class="internalpagewidth">
        <div class="newline centered">
	<% if (BackLink.Visible) { %>
    <table bgcolor="#e4e4af" cellspacing="0" cellpadding="0" width="250" align="center"
        border="2">
        <tr>
            <td>
                <div align="center">
                    <strong>
                        <asp:HyperLink ID="BackLink" runat="server" NavigateUrl="MyAccount.aspx">
							Return to My Account page
                        </asp:HyperLink>
                    </strong>
                </div>
            </td>
        </tr>
    </table>
	<br />
	<% } %>
	<div align="left" style="padding-left:10px">
	<asp:Label ID="MoreThan7PhotosWarning" runat="server" Height="40px" Width="100%">
		Please note you can’t upload more than 7 photos since you were not authorized by administration to do that. If you previously uploaded more than 7 photos and later you were unauthorized only first 7 photos will be displayed on the property page.
	</asp:Label><br />
	<br />
	<asp:Label ID="Label1" runat="server" Height="64px" Width="100%">
		Please note all uploaded photos will be resized so that largest side becomes 320 pixels. Please note quality may degrade severely during the resize. Since quality resizing includes some very advanced algorithms not present on this website, you may increase quality by resizing the pictures manually to the correct size with specialized graphics tools like Adobe Photoshop before uploading them.
	</asp:Label><br />
	<asp:Repeater ID="Repeater1" runat="server" DataSource="<%# PhotosSet %>" DataMember="PropertyPhotos">
		<HeaderTemplate>
			<table border="0" cellspacing="0" cellpadding="0" bordercolor="#ccccff" width="100%">
		</HeaderTemplate>
		<ItemTemplate>
			<tr>
				<td width="100%">
				<%--	<img src='<%# ConfigurationManager.AppSettings["ImagesVirtualLocation"] + DataBinder.Eval(Container.DataItem, "FileName", "{0}") %>'
						width='<%# DataBinder.Eval(Container.DataItem, "Width", "{0:d}") %>' height='<%# DataBinder.Eval(Container.DataItem, "Height", "{0:d}") %>'
						alt='Property Photo'>
						changed by LMG 4/2/08--%>
                 <img src='<%# "images/" + DataBinder.Eval(Container.DataItem, "FileName", "{0}") %>'
						width='<%# DataBinder.Eval(Container.DataItem, "Width", "{0:d}") %>' height='<%# DataBinder.Eval(Container.DataItem, "Height", "{0:d}") %>'
						alt='Property Photo' id='unknown'>
                </td>
				<td bordercolor="#ffffff">
					<input type="button" value="Up" onclick='window.location.href=&quot;<%# CommonFunctions.PrepareURL ("PhotoMove.aspx?Direction=Up&UserID=" + userid.ToString () + "&PhotoID=" + DataBinder.Eval(Container.DataItem, "ID", "{0:d}"), "") %>&quot;'>
				</td>
				<td bordercolor="#ffffff">
					<input type="button" value="Down" onclick='window.location.href=&quot;<%# CommonFunctions.PrepareURL ("PhotoMove.aspx?Direction=Down&UserID=" + userid.ToString () + "&PhotoID=" + DataBinder.Eval(Container.DataItem, "ID", "{0:d}"), "") %>&quot;'>
				</td>
				<td bordercolor="#ffffff">
					<input type="button" value="Delete" onclick='window.location.href=&quot;<%# CommonFunctions.PrepareURL ("DeletePhoto.aspx?UserID=" + userid.ToString () + "&PhotoID=" + DataBinder.Eval(Container.DataItem, "ID", "{0:d}"), "") %>&quot;'>
				</td>
			</tr>
		</ItemTemplate>
		<FooterTemplate>
			</table>
		</FooterTemplate>
	</asp:Repeater>
	<br />
	<input id="PreviewButton" type="button" value="Preview" onclick="window.open('<%= GetPreviewLink () %>', 'Preview', 'toolbar=1,resizable=1,scrollbars=1')" style="width: 96px" /><br />
	<br />
	<asp:Button ID="FinishButton" TabIndex="20" runat="server" Text="Finish" Width="96px" OnClick="FinishButton_Click" />
	<% if (allowupload) { %>
	<br />
	<br />
	You can upload your photos from your local hard drive here:
	<br />
	<asp:FileUpload ID="FileUpload" runat="server" Width="359px" />
	<br />
	<asp:Button ID="UploadFromFile" runat="server" Text="Upload" OnClick="UploadFromFile_Click" Width="96px" /><br />
	<br />
	You can upload your photos from a web location here:
	<br />
	<asp:TextBox ID="WebLocation" runat="server" Width="348px" />
	<br />
	<asp:Button ID="UploadFromWeb" runat="server" Text="Upload" OnClick="UploadFromWeb_Click" Width="96px" />
    <% } %>
        <asp:Label ID="lblInfo" runat="server" ForeColor="Red"></asp:Label>
    </div>
        </div>
    </div>

    <noscript>
        <img src="http://www.watchwise.net/cgi-watchwise/monitor.cgi?<%= CommonFunctions.GetSiteAddress ().ToLower () %>:all" width="1" height="1"><br />
	</noscript>

    <script src="/Assets/js/photoproperty.js"></script>
</asp:Content>
