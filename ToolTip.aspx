<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageNoCss.master" AutoEventWireup="true" CodeFile="ToolTip.aspx.cs" Inherits="ToolTip" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
$("#stay-target-1").ezpz_tooltip({
	contentPosition: 'belowStatic',
	stayOnContent: true,
	offset: 0
});

<div class="demo">
	<h2>Stay on Content</h2>
	<p>A static tooltip that stays visible when mouse is over the target and the content.</p>
	<div class="stay-tooltip-target tooltip-target" id="stay-target-1">Stay on Content ToolTip</div>
	<div class="stay-tooltip-content tooltip-content" id="stay-content-1">
		<p>
			Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas sit amet enim...<br />
			<a href="javascript:">You can reach down and click this</a>
		</p>
	</div>
</div>

</asp:Content>

