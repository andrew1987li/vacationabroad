<%@ Page Language="C#" MasterPageFile="~/MasterPageNoCss7.master" AutoEventWireup="true"
    CodeFile="Application7.aspx.cs" Inherits="Application7" Title="" %>

<%@ OutputCache Duration="600" VaryByParam="*" %>
<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="Server">
    <style type="text/css">
        .advertise-con
        {
            font-size: 14px;
            font-weight: bold;
            height: 413px;
            line-height: 25px;
            margin-left: 156px;
            margin-top: 30px;
            width: 680px;
            text-align: justify;
            color: #494949;
        }
        
        .advertise-con .adver-line
        {
            color: #6582A2;
            float: right;
            font-family: tahoma;
            font-size: 24px;
            position: absolute;
            right: 534px;
            top: 166px;
        }
        
        .advertise-con ol
        {
            list-style: disc outside none;
            color: #266ecc;
        }
        
        .advertise-con ol span
        {
            color: #6c6c6c;
        }
        
        .advertise-con .prop-con
        {
            border: 3px solid #EE9D23;
            color: #266ECC;
            margin: 41px 195px 0;
            padding: 5px 0;
        }
        
        .advertise-con .prop-con a
        {
            color: #266ecc !important;
            text-decoration: none !important;
            font-size: 17px;
        }
    </style>
    <div class="advertise-con">
        <div class="adver-line">
            Advertise With Us Now</div>
        <ol>
            <li><span>Annual Fee Listing $50 usd-</span></li>
            <li><span>No Set Up Cost - annual fee of $50usd.</span></li>
            <li><span>No Commission Fees</span></li>
            <li><span>7 Photos per listing</span></li>
            <li><span>Link to your private website</span></li>
            <li><span>Link for virtual tours</span></li>
            <li><span>Email storage - You can view all the emails you have received through the
                Vacations-Abroad.com website.</span></li>
            <li><span>Allows Multiple Telephones for Contact</span></li>
            <li><span>Update your listing from any computer</span></li>
            <li><span>Page Views Statistics</span></li>
            <li><span>Your email cannot be accessed by spammers</li>
            <li><span>You will receive emails which state all pertinent details concerning the inquiry.
            </span></li>
        </ol>
        <div align="center">
            <div class="prop-con">
                <strong><a href="FindOwner.aspx">Click here to List Your Property</a> </strong>
            </div>
        </div>
    </div>
    <br />
    <!-- Start of StatCounter Code for Default Guide -->
    <script type="text/javascript">
        var sc_project = 7164674;
        var sc_invisible = 1;
        var sc_security = "7447e973"; 
    </script>
    <script type="text/javascript" src="http://www.statcounter.com/counter/counter.js"></script>
    <noscript>
        <div class="statcounter">
            <a title="joomla
visitors" href="http://statcounter.com/joomla/" target="_blank">
                <img class="statcounter" src="http://c.statcounter.com/7164674/0/7447e973/1/" alt="joomla visitors"></a></div>
    </noscript>
    <!-- End of StatCounter Code for Default Guide -->
</asp:Content>
