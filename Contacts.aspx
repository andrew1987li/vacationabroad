<%@ Page Title="Vacations-Abroad.com Contact Information" Language="C#" MasterPageFile="~/masterpage/NormalMaster.master"
    AutoEventWireup="true" CodeFile="Contacts.aspx.cs" Inherits="Contacts" %>
<asp:Content ID="head" ContentPlaceHolderID="head" runat="server">
    Vacations-Abroad.com Contact Information
</asp:Content>
<asp:Content ID="links" ContentPlaceHolderID="links" runat="server">
    <link href="/Assets/css/staticspage.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="bodycontent" runat="Server">
    <div class="internalpagewidth">
        <div>
            <div class="apphead" style="margin-bottom: 50px;">
                <h1>Contact Us</h1>
            </div>
            <strong></strong>
        </div>
        <div class="appcont" >
            <div align="center">
            </div>
            <div align="left">
                <p align="center">
                    <strong>Office Hours<a
                        name="hours"></a><br />
                        Monday-Friday 8AM-8PM<br />
                            Saturday 9AM-1PM<br />
                            <br />
                            <br />
                            The time now is<br />
                        </strong>
                    <span id="curtime"></span>
                </p>
                <p align="center">
                    <br />
                    <br />
                    <strong>Mailing Address<a
                        name="address"></a>
                    
                        <br />
                        Suite G 284, 5805 State Bridge Rd.<br />
                            Johns Creek, GA 30097</strong><br />
                    <br />
                    <strong>Telephone</strong><a name="telephone"></a><br />
                    770-687-6889<br />
                    <br />
                    <strong>Our Email</font></strong><br />
                   webmaster (at) vacations-abroad (dot) com
                </p>

                <br />
                <br />
                <br />
                <p align="center">
                    <strong>Please contact us with any questions you might have regarding a property or their owner that is listed on Vacations-Abroad.com.
                        <br />
                    </strong>
                    <br />
                    <br />
                    <br />
                </p>
            </div>
        </div>
    </div>
    <script src="Assets/js/contacts.js"></script>
</asp:Content>
