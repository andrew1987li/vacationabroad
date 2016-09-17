<%@ Page Language="C#" MasterPageFile="~/MasterPageNoCss.master" AutoEventWireup="true"
    CodeFile="~/SendEmail.aspx.cs" Inherits="SendEmail" Title="<%# GetTitle () %>"
    EnableEventValidation="false" Debug="true" %>

<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="Server">
    <div style="font-family: Arial; font-size: 13px;">
        <% if (BackLink.Visible)
           { %>
        <table class="SndEmlTable">
            <tr>
                <td>
                    <div class="Center">
                        <asp:HyperLink ID="BackLink" runat="server" NavigateUrl="default.aspx">
							Return to property page
                        </asp:HyperLink>
                    </div>
                    
                </td>
            </tr>
        </table>
        <% } %>
        <table class="SndEmlTable10">
            <tr valign="top" style="height: 230px;">
                <td height="230" class="Center" width='<%# (PhotosSet.Tables["PropertyPhotos"].Rows.Count > 0) ? PhotosSet.Tables["PropertyPhotos"].Rows[0]["Width"] : "0" %>'>
                    <% if (PhotosSet.Tables["PropertyPhotos"].Rows.Count > 0)
                       { %>
                    <img alt='<%# PropertiesFullSet.Tables["Properties"].Rows[0]["city"] %> <%# PropertiesFullSet.Tables["Properties"].Rows[0]["type"] %>'
                        src='<%# ConfigurationManager.AppSettings["ImagesVirtualLocation"] + ((PhotosSet.Tables["PropertyPhotos"].Rows.Count > 0) ? PhotosSet.Tables["PropertyPhotos"].Rows[0]["FileName"] : "") %>'
                        width="280" height="230" border="2" />
                    <% } %>
                </td>
                <td width="100%" valign="top" style="height: 230px;">
                    <%--<div style="width: 100%">
                        <div style="width: 25%; float: left;">
                            <asp:HyperLink ID="lnkCountry" runat="server"></asp:HyperLink>
                        </div>
                        <div style="width: 25%; float: left;">
                            <asp:HyperLink ID="lnkState" runat="server"></asp:HyperLink>
                        </div>
                        <div style="width: 25%; float: left;">
                            <asp:HyperLink ID="lnkCity" runat="server"></asp:HyperLink>
                        </div>
                        <div style="width: 100%; text-align:center;">
                            <asp:HyperLink ID="lnkProperty" runat="server"></asp:HyperLink>
                        </div>
                    </div>
                    <br />--%>
                    <%-- <div class="SndEmlFont">
                                   
                                </div>--%>
                    <table class="SndEmlTable2">
                        <tr>
                            <td class="SndEmlRed">
                                <asp:HyperLink ID="lnkProperty" runat="server"></asp:HyperLink><br />
                                <div class="PropertyTable5"> Vacations-Abroad.com will guarantee the validity of this vacation rental.<br /> However, you must inform us prior to sending your deposit to the owner <br /> 
                                Send details including dates, property number, property location and owner information to webmaster@vacations-abroad.com:  <br />                           <a href='<%# CommonFunctions.PrepareURL (((string)PropertiesFullSet.Tables["Properties"].Rows[0]["Country"]).Replace (" ", "_").ToLower () + "/default.aspx") %>'>
                                <%# PropertiesFullSet.Tables["Properties"].Rows[0]["Country"] %>
                                Vacation Rentals</a>, <a href='<%# CommonFunctions.PrepareURL (((string)PropertiesFullSet.Tables["Properties"].Rows[0]["Country"]).Replace (" ", "_").ToLower () + "/" + ((string)PropertiesFullSet.Tables["Properties"].Rows[0]["StateProvince"]).Replace (" ", "_").ToLower () + "/default.aspx") %>'>
                                    <%# PropertiesFullSet.Tables["Properties"].Rows[0]["StateProvince"] %>
                                    Vacation Rentals</a>, or <a href='<%# CommonFunctions.PrepareURL (((string)PropertiesFullSet.Tables["Properties"].Rows[0]["Country"]).Replace (" ", "_").ToLower () + "/" + ((string)PropertiesFullSet.Tables["Properties"].Rows[0]["StateProvince"]).Replace (" ", "_").ToLower () + "/" + ((string)PropertiesFullSet.Tables["Properties"].Rows[0]["City"]).Replace (" ", "_").ToLower () + "/default.aspx") %>'>
                                        <%# PropertiesFullSet.Tables["Properties"].Rows[0]["City"] %>
                                        Vacation Rentals </a>
                        </div>
                               
                            </td>
                        </tr>
            <% if (IfShowContactInfo())
               { %>
            <tr>
                <td>
                    <b>Primary Phone:
                    <%# PropertiesFullSet.Tables["Properties"].Rows[0]["PrimaryTelephone"] %></b> (Please mention Vacations-Abroad.com)
                </td>
            </tr>
            <tr>
                <td>
                    <b>Daytime Phone:
                    <%# PropertiesFullSet.Tables["Properties"].Rows[0]["DaytimeTelephone"] %></b>
                </td>
            </tr>
            <tr>
                <td>
                   <b> Cell Phone:
                    <%# PropertiesFullSet.Tables["Properties"].Rows[0]["MobileTelephone"] %></b>
                </td>
            </tr>
            <tr>
                <td>
                    Owner Country:
                    <%# PropertiesFullSet.Tables["Properties"].Rows[0]["OwnerCountry"] %>
                </td>
            </tr>
            <tr>
                <td>
                    Owner Name:
                    <%# PropertiesFullSet.Tables["Properties"].Rows[0]["FirstName"] %>
                    <%# PropertiesFullSet.Tables["Properties"].Rows[0]["LastName"] %>
                </td>
            </tr>
            <tr>
                <td>
                    Owner Virtual Tour: <a href='<%# PropertiesFullSet.Tables["Properties"].Rows[0]["VirtualTour"] %>'
                        target="_blank">
                        <%# PropertiesFullSet.Tables["Properties"].Rows[0]["VirtualTour"] %>
                    </a>
                </td>
            </tr>
            <tr>
                <td>
                    Owner Website: <a href='<%# PropertiesFullSet.Tables["Properties"].Rows[0]["Website"] %>'
                        target="_blank">
                        <%# PropertiesFullSet.Tables["Properties"].Rows[0]["Website"] %>
                    </a>
                </td>
            </tr>
            <% }
               else
               { %>
               <tr>
                <td><br />
                   <b> Primary Phone:
                    <%# PropertiesFullSet.Tables["Properties"].Rows[0]["PrimaryTelephone"] %> </b> <b> Please mention Vacations-Abroad.com</b>
                </td>
            </tr>
            <tr>
                <td>
                  <b>  Daytime Phone:
                    <%# PropertiesFullSet.Tables["Properties"].Rows[0]["DaytimeTelephone"] %> 
                </td>
            </tr>
            <tr>
                <td>
                  <b>  Cell Phone:
                   <%# PropertiesFullSet.Tables["Properties"].Rows[0]["MobileTelephone"] %> 
                </td>
            </tr>
            <tr>
                <td>
                    Owner Country:
                    <%# PropertiesFullSet.Tables["Properties"].Rows[0]["OwnerCountry"] %>
                </td>
            </tr>
            <tr>
                <td>
                    Owner Name:
                    <%# PropertiesFullSet.Tables["Properties"].Rows[0]["FirstName"] %>
                    <%# PropertiesFullSet.Tables["Properties"].Rows[0]["LastName"] %>
                </td>
            </tr>
            <tr>
                <td>
                    Owner Virtual Tour: <a href='<%# PropertiesFullSet.Tables["Properties"].Rows[0]["VirtualTour"] %>'
                        target="_blank">
                        <%# PropertiesFullSet.Tables["Properties"].Rows[0]["VirtualTour"] %>
                    </a>
                </td>
            </tr>
            <tr>
                <td>
                    Owner Website: <a href='<%# PropertiesFullSet.Tables["Properties"].Rows[0]["Website"] %>'
                        target="_blank">
                        <%# PropertiesFullSet.Tables["Properties"].Rows[0]["Website"] %>
                    </a>
                </td>
            </tr>
            <tr>
                <td style="text-align:center;">
                    To contact
                    <%# PropertiesFullSet.Tables["Properties"].Rows[0]["city"] %>
                    <%# PropertiesFullSet.Tables["Properties"].Rows[0]["type"] %>
                    owner please send an email.
                </td>
            </tr>
            <% } %>
            <tr>
                <td class="SndEmlRed">
                    To Send Email, Use This Form
                </td>
            </tr>
        </table>
        </td> </tr> </table>
        <% if (EmailPresent())
           { %>
        <table class="SndEmlTable3">
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server">Your name:</asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label21" runat="server" ForeColor="Red">*</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="ContactName" runat="server" Width="160px" MaxLength="300" />
                    <asp:RequiredFieldValidator ID="EnterName" runat="server" ErrorMessage="Please enter contact name"
                        ControlToValidate="ContactName" Display="Dynamic" />
                    <asp:RegularExpressionValidator ID="CheckName" runat="server" ValidationExpression="^[a-zA-Z0-9 \.\-\(\)]{1,300}$"
                        ErrorMessage="Invalid contact name entered" ControlToValidate="ContactName" Display="Dynamic" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server">Your e-mail:</asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label12" runat="server" ForeColor="Red">*</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="ContactEmail" runat="server" Width="288px" MaxLength="300" />
                    <asp:RequiredFieldValidator ID="EnterEmail" runat="server" ErrorMessage="Please enter email"
                        ControlToValidate="ContactEmail" Display="Dynamic" />
                    <asp:RegularExpressionValidator ID="CheckEmailLength" runat="server" ValidationExpression="^[\s\S]{1,80}$"
                        ErrorMessage="Too long email address entered" ControlToValidate="ContactEmail"
                        Display="Dynamic" />
                    <asp:RegularExpressionValidator ID="CheckEmail" runat="server" ValidationExpression="^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"
                        ErrorMessage="Invalid email address entered" ControlToValidate="ContactEmail"
                        Display="Dynamic" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server">Your telephone:</asp:Label>
                </td>
                <td>
                </td>
                <td>
                    <asp:TextBox ID="ContactTelephone" runat="server" Width="160px" MaxLength="300" />
                    <asp:RegularExpressionValidator ID="CheckTelephone" runat="server" ValidationExpression="^[a-zA-Z0-9 \.\-\(\)]{1,300}$"
                        ErrorMessage="Invalid telephone entered" ControlToValidate="ContactTelephone"
                        Display="Dynamic" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label4" runat="server">Arrival date:</asp:Label>
                </td>
                <td>
                </td>
                <td>
                    <asp:DropDownList ID="ArrivalDay" runat="server" Width="48px" Height="24px">
                        <asp:ListItem Value="1">1</asp:ListItem>
                        <asp:ListItem Value="2">2</asp:ListItem>
                        <asp:ListItem Value="3">3</asp:ListItem>
                        <asp:ListItem Value="4">4</asp:ListItem>
                        <asp:ListItem Value="5">5</asp:ListItem>
                        <asp:ListItem Value="6">6</asp:ListItem>
                        <asp:ListItem Value="7">7</asp:ListItem>
                        <asp:ListItem Value="8">8</asp:ListItem>
                        <asp:ListItem Value="9">9</asp:ListItem>
                        <asp:ListItem Value="10">10</asp:ListItem>
                        <asp:ListItem Value="11">11</asp:ListItem>
                        <asp:ListItem Value="12">12</asp:ListItem>
                        <asp:ListItem Value="13">13</asp:ListItem>
                        <asp:ListItem Value="14">14</asp:ListItem>
                        <asp:ListItem Value="15">15</asp:ListItem>
                        <asp:ListItem Value="16">16</asp:ListItem>
                        <asp:ListItem Value="17">17</asp:ListItem>
                        <asp:ListItem Value="18">18</asp:ListItem>
                        <asp:ListItem Value="19">19</asp:ListItem>
                        <asp:ListItem Value="20">20</asp:ListItem>
                        <asp:ListItem Value="21">21</asp:ListItem>
                        <asp:ListItem Value="22">22</asp:ListItem>
                        <asp:ListItem Value="23">23</asp:ListItem>
                        <asp:ListItem Value="24">24</asp:ListItem>
                        <asp:ListItem Value="25">25</asp:ListItem>
                        <asp:ListItem Value="26">26</asp:ListItem>
                        <asp:ListItem Value="27">27</asp:ListItem>
                        <asp:ListItem Value="28">28</asp:ListItem>
                        <asp:ListItem Value="29">29</asp:ListItem>
                        <asp:ListItem Value="30">30</asp:ListItem>
                        <asp:ListItem Value="31">31</asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="ArrivalMonth" runat="server" Width="88px" Height="24px">
                        <asp:ListItem Value="January">January</asp:ListItem>
                        <asp:ListItem Value="February">February</asp:ListItem>
                        <asp:ListItem Value="March">March</asp:ListItem>
                        <asp:ListItem Value="April">April</asp:ListItem>
                        <asp:ListItem Value="May">May</asp:ListItem>
                        <asp:ListItem Value="June">June</asp:ListItem>
                        <asp:ListItem Value="July">July</asp:ListItem>
                        <asp:ListItem Value="August">August</asp:ListItem>
                        <asp:ListItem Value="September">September</asp:ListItem>
                        <asp:ListItem Value="October">October</asp:ListItem>
                        <asp:ListItem Value="November">November</asp:ListItem>
                        <asp:ListItem Value="December">December</asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="ArrivalYear" runat="server" Width="72px" Height="24px">
                        <asp:ListItem Value="2010">2010</asp:ListItem>
                        <asp:ListItem Value="2011">2011</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RegularExpressionValidator ID="Regularexpressionvalidator7" runat="server" ValidationExpression="^[0-9]{1,2}$"
                        ErrorMessage="Invalid day entered" ControlToValidate="ArrivalDay" Display="Dynamic" />
                    <asp:RegularExpressionValidator ID="Regularexpressionvalidator8" runat="server" ValidationExpression="^[a-zA-Z]{1,20}$"
                        ErrorMessage="Invalid month entered" ControlToValidate="ArrivalMonth" Display="Dynamic" />
                    <asp:RegularExpressionValidator ID="Regularexpressionvalidator9" runat="server" ValidationExpression="^[0-9]{4}$"
                        ErrorMessage="Invalid year entered" ControlToValidate="ArrivalYear" Display="Dynamic" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label6" runat="server">How many nights:</asp:Label>
                </td>
                <td>
                </td>
                <td>
                    <asp:TextBox ID="HowManyNights" runat="server" Width="160px" MaxLength="300" />
                    <asp:RegularExpressionValidator ID="Regularexpressionvalidator1" runat="server" ValidationExpression="^[0-9 \.\-\(\)]{1,300}$"
                        ErrorMessage="Invalid number entered" ControlToValidate="HowManyNights" Display="Dynamic" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label7" runat="server">How many adults:</asp:Label>
                </td>
                <td>
                </td>
                <td>
                    <asp:TextBox ID="HowManyAdults" runat="server" Width="160px" MaxLength="300" />
                    <asp:RegularExpressionValidator ID="Regularexpressionvalidator2" runat="server" ValidationExpression="^[0-9 \.\-\(\)]{1,300}$"
                        ErrorMessage="Invalid number entered" ControlToValidate="HowManyAdults" Display="Dynamic" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label8" runat="server">How many children:</asp:Label>
                </td>
                <td>
                </td>
                <td>
                    <asp:TextBox ID="HowManyChildren" runat="server" Width="160px" MaxLength="300" />
                    <asp:RegularExpressionValidator ID="Regularexpressionvalidator3" runat="server" ValidationExpression="^[0-9 \.\-\(\)]{1,300}$"
                        ErrorMessage="Invalid number entered" ControlToValidate="HowManyChildren" Display="Dynamic" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label9" runat="server">Telephone:</asp:Label>
                </td>
                <td>
                </td>
                <td>
                    <asp:TextBox ID="Telephone" runat="server" Width="160px" MaxLength="300" />
                    <asp:RegularExpressionValidator ID="Regularexpressionvalidator4" runat="server" ValidationExpression="^[0-9 \.\-\(\)]{1,300}$"
                        ErrorMessage="Invalid telephone entered" ControlToValidate="Telephone" Display="Dynamic" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label10" runat="server">Telephone 2:</asp:Label>
                </td>
                <td>
                </td>
                <td>
                    <asp:TextBox ID="Telephone2" runat="server" Width="160px" MaxLength="300" />
                    <asp:RegularExpressionValidator ID="Regularexpressionvalidator5" runat="server" ValidationExpression="^[0-9 \.\-\(\)]{1,300}$"
                        ErrorMessage="Invalid telephone 2 entered" ControlToValidate="Telephone2" Display="Dynamic" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label11" runat="server">Additional comments:</asp:Label>
                </td>
                <td>
                </td>
                <td>
                    <asp:TextBox ID="Comments" runat="server" Width="392px" Height="64px" MaxLength="4000"
                        TextMode="MultiLine" />
                    <asp:RegularExpressionValidator ID="Regularexpressionvalidator6" runat="server" ValidationExpression="^[\s\S]{1,4000}$"
                        ErrorMessage="Invalid comments entered" ControlToValidate="Comments" Display="Dynamic" />
                </td>
            </tr>
            <tr>
                <td style="height: 26px">
                    <asp:Button ID="SubmitButton" runat="server" Width="112px" Height="24px" Text="Submit"
                        OnClick="SubmitButton_Click" />
                </td>
                <td style="height: 26px">
                </td>
                <td style="height: 26px">
                    <asp:Button ID="ClearButton" runat="server" Width="112px" Height="24px" Text="Clear"
                        OnClick="ClearButton_Click" />
                </td>
            </tr>
        </table>
        <% }
           else
           { %>
        <div class="Center">
            This owner didn't enter an e-mail into the system so there is no way to send an
            e-mail to him.
        </div>
        <% } %>
        <!-- Start of StatCounter Code -->

        <script type="text/javascript">
            sc_project = 3690279;
            sc_invisible = 1;
            sc_partition = 44;
            sc_security = "84557ce7"; 
        </script>

        <script type="text/javascript" src="http://www.statcounter.com/counter/counter_xhtml.js"></script>

        <noscript>
            <div class="statcounter">
                <a href="http://www.statcounter.com/" target="_blank">
                    <img class="statcounter" src="http://c45.statcounter.com/3690279/0/84557ce7/1/" alt="counter easy hit" /></a></div>
        </noscript>
        <!-- End of StatCounter Code -->
    </div>
</asp:Content>
