<%@ Page Title="<%# GetTitle () %>" Language="C#" MasterPageFile="~/MasterPageNoCss.master" AutoEventWireup="true" CodeFile="PropertyTerms.aspx.cs" Inherits="TourTerms" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
    <asp:Label ID="Description" runat="server" Visible="false" Text="Advertise a %city% vacation property or %city% holiday property."></asp:Label>
    <br /><br />
<div style="text-align:left; width:75%; a">
  <table style="width: 100%" cellspacing="0">
  <tr>
                                    <td valign="bottom" width="20%" style="background-color: #ede9ed; border: solid 1px #999999;
                                        padding: 3px 3px 3px 3px;">
                                        <div id="divTab1" runat="server" align="center" class="tourTabs2" width="100%">
                                            <a href='<%= CommonFunctions.PrepareURL (country.Replace (" ", "_").ToLower () + "/" + state.Replace (" ", "_").ToLower () + "/" + city.Replace(" ", "_").ToLower() + "/default.aspx") %>'>
                                               <%# city %> Vacation Rentals </a>
                                        </div>
                                    </td>
                                     <td valign="bottom" width="20%" style="background-color: #ede9ed; border: solid 1px #999999;
                                        padding: 3px 3px 3px 3px;">
                                        <div id="divTab2" runat="server" align="center" class="tourTabs2" width="100%">
                                             <a href='<%= CommonFunctions.PrepareURL (country.Replace (" ", "_").ToLower () + "/" + state.Replace (" ", "_").ToLower () + "/" + city.Replace(" ", "_").ToLower() + "_tours/default.aspx") %>'>
                                                Tours</a>                                            
                                        </div>
                                    </td>
                                      <td valign="bottom" width="20%" style="background-color: #ede9ed; border: solid 1px #999999;
                                        padding: 3px 3px 3px 3px;">
                                        <div id="div1" runat="server" align="center" class="tourTabs2" width="100%">
                                           <a href='<%# CommonFunctions.PrepareURL (country.Replace (" ", "_").ToLower () + "/" + state.Replace (" ", "_").ToLower () + "/add_" + city.Replace(" ", "_").ToLower() + "_tour/default.aspx") %>'>Add Tour </a>
                                        </div>
                                    </td>
                                    <td valign="bottom" width="20%" style="border-left: solid 1px #999999; border-top: solid 1px #999999;
                                        border-right: solid 1px #999999;">
                                        <div id="div2" runat="server" align="center" class="tourTabs2" width="100%">
                                            Add Property
                                        </div>
                                    </td>
                                    <td style="width: 20%; border-bottom: solid 1px #999999; color: White;">
                                        v
                                    </td>
                                </tr>
   <tr>
                                    <td width="100%" colspan="5" style="border-bottom: solid 1px #999999; border-left: solid 1px #999999;
                                        border-right: solid 1px #999999;">
    <br />
<center><h2>Terms for Advertising<br /> 
Advertise <%# city %> vacation property<br />
Advertise <%# city %> holiday property<br /></h2></center>
<div style="border:solid 1px #cc6600; padding:5px 5px 5px 5px;">
1) Each property added must be owned and managed by a local registered business.<br />
2) Our annual fee is $50 USD.<br />
3) Your property must be listed in appropiate city.<br />
</div>
 <asp:CheckBox ID="chkAgree" runat="server" 
        Text="I agree to terms of Vacations-Abroad.com" />
<br /><br />
   
<b>Step 1:</b>  Agree to terms above.<br />
<b>Step 2:</b>  Create an account.<br />
<b>Step 3:</b>  Then add details and photo regarding your property.<br />
    <asp:Button ID="btnSubmit" runat="server" Text="Create An Account" 
        onclick="btnSubmit_Click" />
</td> </tr></table></div>    
<tr valign="top" bordercolor="#ffffff" bgcolor="#ffffff">
                        <td colspan="5">
                                    
                                            <li><font size="2">No Set Up Cost - except annual fee of $50usd</font></li>
                                            <li><font size="2">No Commission Fees</font></li>
                                            <li><font size="2">7 Photos per listing</font></li>
                                            <li><font size="2">Link to your private website</font></li>
                                            <li><font size="2">Link for virtual tours</font></li>
                                            <li><font size="2">Email storage - You can view all the emails you have recieved through
                                                the Vacations-Abroad.com website.</font></li>
                                            <li><font size="2">Allows Multiple Telephones for Contact</font></li>
                                            <li><font size="2">Update your listing from any computer</font></li>
                                            <li><font size="2">Page Views Statistics</font></li>
                                            <li><font size="2">Your email cannot be accessed by spammers</font></li>
                                            <li><font size="2">Searchable Database by Location and Attractions</font></li>
                                            <li><font size="2">You will receive emails which state all pertinent details concerning
                                                the inquiry.
					    <li><font size="2"><a href="http://www.alexa.com/siteinfo/vacations-abroad.com" target="_blank">Check our Alexa ranking profile - page opens in new window.</a>
</font></li>
                                            
                                        </ol>
                                    </ol>
                                </dl>
                           </p> </blockquote>
                            <a name="List"></a>
                            <table width="800" border="1" align="center" cellpadding="2" cellspacing="0" bordercolor="#AABBAA"
                                id="Table3">
                                <tr>
                                    <td width="800" height="0" align="center" bgcolor="#ffffff">
                                        <div align="left">
                                            <font size="2"><strong>I have known Linda for several years both personally and professionally. She has a real passion for her business. She has been a real asset to my vacation rental company. I look forward to continuinig our business relationship for many years. She's the BEST!" 
<br/>Dena Tuten - Realty Associates 
<br/>St. Augustine, FL <br/> </a>
                                            </strong></font>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                            <blockquote>
                               <table width="800" border="1" align="center" cellpadding="0" cellspacing="0" bordercolor="#AABBAA"
                                id="Table3">
                                <tr>
                                    <td width="800" height="0" align="center" bgcolor="#ffffff">
                                        <div align="center">
                                            <font size="2"><strong>Vacations abroad has been one of my top producers since I signed on with them some 3 or more years ago.  Their fee is very reasonable, and the results, most of which are sincere apartment seeking travelers who are looking for good value abroad, are many and measurable.

             I've pretty much given up on some of the more expensive sites that claim higher listing in search engines, Vacations Abroad gives me as much business as they ever did - - at a much lower price - thanks Linda!"
 
             <br/> Aloha and Paalam - Tito Gray Gleason 
<br/>condo owner in Manila
<br/> </a>
                                            </strong></font>
                                        </div>
                                    </td>
                                </tr>
                            </table>

<table width="800" border="1" align="center" cellpadding="0" cellspacing="0" bordercolor="#AABBAA"
                                id="Table3">
                                <tr>
                                    <td width="800" height="0" align="center" bgcolor="#ffffff">
                                        <div align="center">
                                            <font size="2"><strong>Google Analytics-Visitor Traffic
<br/>January 2011 - 22,404<br/> 
February 2011 - 18,709<br/>March 2011 - 20,718<br/>April 2011 - 21,497<br/>May 2011 - 23,937<br/>June 2011 - 23,725<br/>July 2011- 19,990<br/>
</a>
                                            </strong></font>
                                        </div>
                                    </td>
                                </tr>
                            </table>
<table width="800" border="1" align="center" cellpadding="0" cellspacing="0" bordercolor="#AABBAA"
                                id="Table3">
                                <tr>
                                    <td width="800" height="0" align="center" bgcolor="#ffffff">
                                        <div align="center">
                                            <font size="2"><strong>Traffic Demographics
<br/>
USA 40%,  
Canada 10%, 
England (UK) 10%, 
Australia 5%
<br/>
</a>
                                            </strong></font>
                                        </div>
                                    </td>
                                </tr>
                            </table></blockquote>
                            <p>
                                <font size="2"><a name="compare"></a></font>
                            </p>
                            
                            <div align="center">
                                <p>
                                    &nbsp;</p>
                            </div>
                        </td>
                    </tr>
<asp:Label ID="lblInfo" runat="server" ForeColor="Red"></asp:Label><!-- Start of StatCounter Code for Default Guide -->
<script type="text/javascript">
    var sc_project = 7164674;
    var sc_invisible = 1;
    var sc_security = "7447e973"; 
</script>
<script type="text/javascript"
src="http://www.statcounter.com/counter/counter.js"></script>
<noscript><div class="statcounter"><a title="joomla
visitors" href="http://statcounter.com/joomla/"
target="_blank"><img class="statcounter"
src="http://c.statcounter.com/7164674/0/7447e973/1/"
alt="joomla visitors"></a></div></noscript>
<!-- End of StatCounter Code for Default Guide -->
</asp:Content>

