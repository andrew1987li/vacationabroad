<%@ Page Title="<%# GetTitle () %>" Language="C#" MasterPageFile="~/MasterPageNoCss.master" AutoEventWireup="true" CodeFile="TourTerms.aspx.cs" Inherits="TourTerms" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
    <asp:Label ID="Description" runat="server" Visible="false" Text="Add a %city% tour to our %city% tour directory. Vacations-Abroad.com a vacation rental directory"></asp:Label>
<br /><br />
<div style="text-align:left; width:75%;">
  <table style="width: 100%" cellspacing="0">
  <tr>
                                    <td valign="bottom" width="20%" style="background-color: #ede9ed; border: solid 1px #999999;
                                        padding: 3px 3px 3px 3px;">
                                        <div id="divTab1" runat="server" align="center" class="tourTabs2" width="100%">
                                            <a href='<%= CommonFunctions.PrepareURL (country.Replace (" ", "_").ToLower () + "/" + state.Replace (" ", "_").ToLower () + "/" + city.Replace(" ", "_").ToLower() + "/default.aspx") %>'>
                                                Vacation Rentals </a>
                                        </div>
                                    </td>
                                     <td valign="bottom" width="20%" style="background-color: #ede9ed; border: solid 1px #999999;
                                        padding: 3px 3px 3px 3px;">
                                        <div id="divTab2" runat="server" align="center" class="tourTabs2" width="100%">
                                             <a href='<%= CommonFunctions.PrepareURL (country.Replace (" ", "_").ToLower () + "/" + state.Replace (" ", "_").ToLower () + "/" + city.Replace(" ", "_").ToLower() + "_tours/default.aspx") %>'>
                                                Tours </a>                                            
                                        </div>
                                    </td>
                                     <td valign="bottom" width="20%" style="border-left: solid 1px #999999; border-top: solid 1px #999999;
                                        border-right: solid 1px #999999;">
                                        <div id="div1" runat="server" align="center" class="tourTabs2" width="100%">
                                            Add Tour
                                        </div>
                                    </td>
                                    <td valign="bottom" width="20%" style="background-color: #ede9ed; border: solid 1px #999999;
                                        padding: 3px 3px 3px 3px;">
                                        <div id="div2" runat="server" align="center" class="tourTabs2" width="100%">
                                           <a href='<%# CommonFunctions.PrepareURL (country.Replace (" ", "_").ToLower () + "/" + state.Replace (" ", "_").ToLower () + "/add_" + city.Replace(" ", "_").ToLower() + "_property/default.aspx") %>'>Add Property </a>
                                        </div>
                                    </td>
                                    <td style="width: 20%; border-bottom: solid 1px #999999; color: White;">
                                        v
                                    </td>
                                </tr>
   <tr>
                                    <td width="100%" colspan="6" style="border-bottom: solid 1px #999999; border-left: solid 1px #999999;
                                        border-right: solid 1px #999999;">
    <br />
<center><h2>Terms for Advertising:<br />  <%# city %> sightseeing tours<br /> <%# city %> escorted tours<br /> <%# city %> private tours</h2></center>
<div style="border:solid 1px #cc6600; padding:5px 5px 5px 5px;">
1) Each tour added must be owned and managed by a local registered business.<br />

</div>
 <asp:CheckBox ID="chkAgree" runat="server" 
        Text="I agree to terms of Vacations-Abroad.com" />
<br /><br />
   
<b>Step 1:</b>  Agree to terms above.<br />
<b>Step 2:</b>  Create an account.<br />
<b>Step 3:</b>  Then add details and photo regarding your tour.<br />
    <asp:Button ID="btnSubmit" runat="server" Text="Create An Account" 
        onclick="btnSubmit_Click" />

</td></tr></table></div>
<asp:Label ID="lblInfo" runat="server" ForeColor="Red"></asp:Label>
</asp:Content>

