<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPageNoCss.master"
    CodeFile="ThankYou.aspx.cs" Inherits="ThankYou" %>

<%@ OutputCache Duration="600" VaryByParam="*" %>
<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="Server">
    <div class="advertise-con" style="min-height:560px;">
        
        <blockquote>
            <div align="center">
                <p>
                    <font color="#cc0000" size="4" face="Arial">Thank You for listing your property on Vacations-Abroad.com</font></p><br />
                <p>
                    <font color="#cc0000" size="3" face="Arial">ALL INQUIRIES will come from ar@vacations-abroad.com</font><font
                        color="#cc0000" size="3" face="Arial"><br>
                        
                        Create a rule in your email program to make sure that your inquires do not go into
                        the Spam box.</font>
                </p>
            </div>
        </blockquote><br />
        <div align="center">
            <div class="prop-con" style="margin:0px;">
                <strong>
                    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="lnkCalendar_Click">Click here to edit the reservation calendar for this property.</asp:LinkButton>
                </strong>
            </div>
        </div>
        
        
            <li><span>Your current listing has not been approved for inclusion into our website
                YET. Once it has been approved, we will notify you via email.</span></li><br />
            <li><span>Our Site Has 2 Payment Options</span></li><br />
	    <span> (1) By default all listings will have a 7% commission deducted from all bookings.</br>
	    <span> (2) Should you prefer to pay the annual fee of $50, then click on the link below; "Your Account" and make a payment.<br /> <br /> 
           <li><span>Please make sure that the text for your property is totally original and is not duplicated.</span></li>
            <li><span>We review all new listing.<br />
                <li><span>We want to insure that we only have reputable properties on our website, so please allow us 2 days for inclusion in our site.</span></li>

        
        <div align="center" class="prop-con">
            <strong><a href="MyAccount.aspx">Your Account </a></strong>
        </div>
    </div>
</asp:Content>
