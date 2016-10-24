<%--<%@ Page Language="C#" MasterPageFile="~/MasterPPropertiesFullSetageNoCss.master" AutoEventWireup="true" CodeFile="~/viewproperty.aspx.cs" Inherits="ViewProperty" Title="<%# GetTitle () %>" EnableEventValidation="false" %>--%>
<%--EnableViewState="false"--%>

<%@ Page Language="C#" MasterPageFile="~/masterpage/NormalMaster.master" AutoEventWireup="true" CodeFile="~/viewproperty.aspx.cs" Inherits="ViewProperty" Title="<%# GetTitle () %>" EnableEventValidation="false" %>

<%@ Register TagPrefix="recaptcha" Namespace="Recaptcha" Assembly="Recaptcha" %>

<asp:Content ID="head" ContentPlaceHolderID="head" runat="server">
    <%=city %> <%# PropertiesFullSet.Tables["Properties"].Rows[0]["NumBedrooms"] %> Bedroom <%# PropertiesFullSet.Tables["Properties"].Rows[0]["Type"] %> # <%=propertyid %> | Vacations Abroad
</asp:Content>

<asp:Content ID="links" ContentPlaceHolderID="links" runat="server">
    <link href="/Assets/css/viewproperty.css" rel="stylesheet" />
        <link rel="stylesheet" type="text/css" href="/css/jcarousel.css"/>
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="bodycontent" runat="Server">
    <div class="row">
     <div class="internalpagewidth">
          <script type="text/javascript" src="/js/responsiveCarousel.min.js"></script>



    <script type="text/javascript" src="/js/jquery.jcarousel.min.js"></script>
    <script type="text/javascript" src="/js/jcarousel.basic.js"></script>

    <asp:Label ID="Title" runat="server" Visible="false" Text="%city% %bedroom% Bedroom %type% # %propid% | Vacations Abroad"></asp:Label>
    <asp:Label ID="Keywords" runat="server" Visible="false" Text="%city% %bedroom% Bedroom %type%, %city% %stateprovince% %type% rental, %city% %country% %type% "></asp:Label>
    <asp:Label ID="Description" runat="server" Visible="false" Text="Kickback and relax in this %city% %type% in %stateprovince% %country% from Vacations-Abroad"></asp:Label>
    <asp:Label ID="Alt" runat="server" Visible="false" Text="%city% %bedroom% Bedroom %type%"></asp:Label>
    <%--content and city cells--%>
    <%--<div class="rtOuter">
        <div class="rtHeader" id="rtHead" runat="server">
           </div>
        <div id="divCitiesRt" runat="server" class="rtText2">
        </div>
    </div>--%>
<!-- The Modal -->
    <div id="inqureform" class="modalform">

      <!-- Modal content -->
      <div id="modal_contents" class="modal_contents">
          <div class="dlghead">
                <span class="mclose">x</span>
          </div>
        
<div class="centered">
                <table>
                    <tr>
                        <td align="center" >
                            <% if (EmailPresent())
                               { %>
                            <div class="inquriyform">
                          <table >
                                <tr align="center">
                                    <td style="width: 100px" align="left">
                                        <asp:Label ID="Label1" runat="server">Your name:</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="Label21" runat="server" ForeColor="Red">*</asp:Label>
                                    </td>
                                    <td style="width: 175px;" align="right">
                                        <asp:TextBox ID="ContactName" runat="server" Width="170px" MaxLength="300" />
                                        <asp:RequiredFieldValidator ID="EnterName" runat="server" ErrorMessage="Please enter contact name"
                                            ControlToValidate="ContactName" Display="Dynamic" />
                                        <asp:RegularExpressionValidator ID="CheckName" runat="server" ValidationExpression="^[a-zA-Z0-9 \.\-\(\)]{1,300}$"
                                            ErrorMessage="Invalid contact name entered" ControlToValidate="ContactName" Display="Dynamic" />
                                    </td>
                                </tr>
                                <tr align="center">
                                    <td align="left">
                                        <asp:Label ID="Label2" runat="server">And email:</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="Label12" runat="server" ForeColor="Red">*</asp:Label>
                                    </td>
                                    <td align="right" style="width: 175px; margin-left: -5px">
                                        <asp:TextBox ID="ContactEmail" runat="server" Width="170px" MaxLength="175" />
                                        <asp:RequiredFieldValidator ID="EnterEmail" runat="server" ErrorMessage="Please enter email"
                                            ControlToValidate="ContactEmail" Display="Dynamic" />
                                        <asp:RegularExpressionValidator ID="CheckEmailLength" runat="server" ValidationExpression="^[\s\S]{1,80}$"
                                            ErrorMessage="Too long email address entered" ControlToValidate="ContactEmail"
                                            Display="Dynamic" />
                                        <asp:RegularExpressionValidator ID="CheckEmail" runat="server" ValidationExpression="^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"
                                            ErrorMessage="Invalid email" ControlToValidate="ContactEmail"
                                            Display="Dynamic" />
                                    </td>
                                </tr>
                                <tr align="center">
                                    <td align="left">
                                        <asp:Label ID="Label3" runat="server">Telephone:</asp:Label>
                                    </td>
                                    <td></td>
                                    <td colspan="2" align="right" style="width: 175px">
                                        <asp:TextBox ID="ContactTelephone" runat="server" Width="170px" MaxLength="300" />
                                        <asp:RegularExpressionValidator ID="CheckTelephone" runat="server" ValidationExpression="^[a-zA-Z0-9 \.\-\(\)]{1,50}$"
                                            ErrorMessage="Invalid telephone entered" ControlToValidate="ContactTelephone"
                                            Display="Dynamic" />
                                    </td>
                                </tr>
                                <tr align="center">
                                    <td align="left">
                                        <asp:Label ID="Label4" runat="server">Arrival:</asp:Label>
                                    </td>
                                    <td></td>
                                    <td align="right" style="width: 172px">
                                        <asp:DropDownList ID="ArrivalDay" runat="server" Width="47px" Height="24px">
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
                                        <asp:DropDownList ID="ArrivalMonth" runat="server" Width="53px" Height="24px">
                                            <asp:ListItem Value="January">Jan</asp:ListItem>
                                            <asp:ListItem Value="February">Feb</asp:ListItem>
                                            <asp:ListItem Value="March">Mar</asp:ListItem>
                                            <asp:ListItem Value="April">Apr</asp:ListItem>
                                            <asp:ListItem Value="May">May</asp:ListItem>
                                            <asp:ListItem Value="June">Jun</asp:ListItem>
                                            <asp:ListItem Value="July">Jul</asp:ListItem>
                                            <asp:ListItem Value="August">Aug</asp:ListItem>
                                            <asp:ListItem Value="September">Sep</asp:ListItem>
                                            <asp:ListItem Value="October">Oct</asp:ListItem>
                                            <asp:ListItem Value="November">Nov</asp:ListItem>
                                            <asp:ListItem Value="December">Dec</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ArrivalYear" runat="server" Width="62px" Height="24px">

                                            <asp:ListItem Value="2016">2016</asp:ListItem>
                                            <asp:ListItem Value="2017">2017</asp:ListItem>

                                        </asp:DropDownList>
                                        <asp:RegularExpressionValidator ID="Regularexpressionvalidator7" runat="server" ValidationExpression="^[0-9]{1,2}$"
                                            ErrorMessage="Invalid day entered" ControlToValidate="ArrivalDay" Display="Dynamic" />
                                        <asp:RegularExpressionValidator ID="Regularexpressionvalidator8" runat="server" ValidationExpression="^[a-zA-Z]{1,20}$"
                                            ErrorMessage="Invalid month entered" ControlToValidate="ArrivalMonth" Display="Dynamic" />
                                        <asp:RegularExpressionValidator ID="Regularexpressionvalidator9" runat="server" ValidationExpression="^[0-9]{4}$"
                                            ErrorMessage="Invalid year entered" ControlToValidate="ArrivalYear" Display="Dynamic" />
                                    </td>
                                </tr>
                                <tr align="center">
                                    <td align="left">
                                        <asp:Label ID="Label6" runat="server"># nights:</asp:Label>
                                    </td>
                                    <td></td>
                                    <td align="right" style="width: 175px">
                                        <asp:TextBox ID="HowManyNights" runat="server" Width="170px" MaxLength="200" />
                                        <asp:RegularExpressionValidator ID="Regularexpressionvalidator1" runat="server" ValidationExpression="^[0-9 \.\-\(\)]{1,300}$"
                                            ErrorMessage="Invalid number entered" ControlToValidate="HowManyNights" Display="Dynamic" />
                                    </td>
                                </tr>
                                <tr align="center">
                                    <td align="left">
                                        <asp:Label ID="Label7" runat="server"># Adults:</asp:Label>
                                    </td>
                                    <td></td>
                                    <td colspan="2" align="right" style="width: 175px">
                                        <asp:TextBox ID="HowManyAdults" runat="server" Width="46px" MaxLength="300" />
                                        <asp:RegularExpressionValidator ID="Regularexpressionvalidator2" runat="server" ValidationExpression="^[0-9 \.\-\(\)]{1,300}$"
                                            ErrorMessage="Invalid number entered" ControlToValidate="HowManyAdults" Display="Dynamic" />
                                        &nbsp;#Children:&nbsp;
                                            <asp:TextBox ID="HowManyChildren"  runat="server" Width="46px" MaxLength="300" />
                                        <asp:RegularExpressionValidator ID="Regularexpressionvalidator3" runat="server" ValidationExpression="^[0-9 \.\-\(\)]{1,300}$"
                                            ErrorMessage="Invalid number entered" ControlToValidate="HowManyChildren" Display="Dynamic" />
                                    </td>
                                </tr>
                                <tr align="center">
                                    <td style="vertical-align: top;" align="left">
                                        <asp:Label ID="Label11" runat="server">Additional comments:</asp:Label>
                                    </td>
                                    <td></td>
                                    <td align="right" style="width: 175px">
                                        <asp:TextBox ID="Comments" runat="server" MaxLength="4000" Width="170px" TextMode="MultiLine"
                                            Rows="2" />
                                        <asp:RegularExpressionValidator ID="Regularexpressionvalidator6" runat="server" ValidationExpression="^[\s\S]{1,4000}$"
                                            ErrorMessage="Invalid comments entered" ControlToValidate="Comments" Display="Dynamic" />
                                    </td>
                                </tr>
                                <tr align="center">
                                    <td></td>

                                    <td>
                                        <asp:Label ID="lblmsg2" runat="server" Font-Bold="True" ForeColor="Red" Text=""></asp:Label>
                                        <%--                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1"  ControlToValidate="txtimgcode" runat="server" ErrorMessage="Image Code Cannot be Empty !"></asp:RequiredFieldValidator>
                                        --%>
                                            

                                    </td>
                                </tr>
                                <tr align="center">
                                    <td style="height: 30px;" colspan="3" align="center">
                                        <asp:Button ID="SubmitButton" runat="server" Width="90%" Height="30px" BackColor="#6699ff" class="btnBookNow" Text="Request a Quote"
                                            OnClick="SubmitButton_Click" />
                                        <asp:Label ID="lblMsg" Style="color: Red; font-weight: bold;" runat="server"></asp:Label>
                                    </td>
                                </tr>
								<tr align="center">
                                    <td style="height: 30px;" colspan="3" align="center">
                                        <button class="btnBookNow" style="background-color:#154890;height:30px;width:90%;">Add To Favorites</button>
                                    </td>
                                </tr>
                            </table>
                            </div>
  
                            <% }
                               else
                               { %>
                            <div class="Center">
                                This owner didn't enter an e-mail into the system so there is no way to send an
                                    e-mail to him.
                            </div>
                            <% } %>
                            <%--actual form--%>
                            <%--right side--%>
                        </td>
                    </tr>
                </table>
                </div>
      </div>

    </div>

     <asp:Label ID="lblTest" runat="server" Style="display: none"></asp:Label>
    <div class="row">
                    <div class="listingPagesH1Container">   
                         <asp:HyperLink ID="hyplnkRegionBackLink" runat="server">
                            <h3>
                                <asp:Literal ID="ltrRegionBackText" runat="server"></asp:Literal></h3>
                        </asp:HyperLink>
                        <asp:HyperLink ID="hyplnkCountryBackLink" runat="server">
                            <h3>
                                <asp:Literal ID="ltrCountryBackText" runat="server"></asp:Literal></h3>
                        </asp:HyperLink>
                        <asp:HyperLink ID="hyplnkStateBackLink" runat="server">
                            <h3>
                                <asp:Literal ID="ltrStateBackText" runat="server"></asp:Literal></h3>
                        </asp:HyperLink>
                        <asp:HyperLink ID="hyplnkCityBackLink" runat="server">
                            <h3>
                                <asp:Literal ID="ltrCityBackText" runat="server"></asp:Literal></h3>
                        </asp:HyperLink>

                    </div>
    </div>
     <div class="row">
                    <div class="text-center">
                        <h1><span class="H1CityText">
                            <%# PropertiesFullSet.Tables["Properties"].Rows[0]["City"] %> <%# PropertiesFullSet.Tables["Properties"].Rows[0]["Type"] %> </span></h1>
                        <br />

                       
                    </div>
     </div>
     <div class="row">
         <div class="text-center">
                    <div class="wrapper">
                        <div class="jcarousel-wrapper" style="margin-bottom: 100px; margin-top: -30px">
                            <div class="jcarousel" id="">
                                <ul>
                                    <%if (PhotosSet.Tables["PropertyPhotos"].Rows.Count > 0)
                                      { %>
                                    <li>
                                        <div class='drop-shadow effect4'>
                                        <img  alt='<%# PropertiesFullSet.Tables["Properties"].Rows[0]["City"].ToString() +" "+ PropertiesFullSet.Tables["Properties"].Rows[0]["Type"].ToString() %>' title='<%# PropertiesFullSet.Tables["Properties"].Rows[0]["City"].ToString() +" "+ PropertiesFullSet.Tables["Properties"].Rows[0]["Type"].ToString() %>'
                                            src='<%# ConfigurationManager.AppSettings["ImagesVirtualLocation"] + ((PhotosSet.Tables["PropertyPhotos"].Rows.Count > 0) ? 
                                    PhotosSet.Tables["PropertyPhotos"].Rows[0]["FileName"].ToString () : "") %>'
                                            width='<%# (PhotosSet.Tables["PropertyPhotos"].Rows.Count > 0) ? PhotosSet.Tables["PropertyPhotos"].Rows[0]["Width"].ToString () : "0" %>'
                                            height='<%# (PhotosSet.Tables["PropertyPhotos"].Rows.Count > 0) ? PhotosSet.Tables["PropertyPhotos"].Rows[0]["Height"].ToString () : "0" %>' /></div></li>
                                    <%} %>
                                    <%if (PhotosSet.Tables["PropertyPhotos"].Rows.Count > 1)
                                      { %>
                                    <li>
                                        <div class='drop-shadow effect4'>
                                        <img  alt='<%# PropertiesFullSet.Tables["Properties"].Rows[0]["City"].ToString() +" "+ PropertiesFullSet.Tables["Properties"].Rows[0]["Type"].ToString() %>' title='<%# PropertiesFullSet.Tables["Properties"].Rows[0]["City"].ToString() +" "+ PropertiesFullSet.Tables["Properties"].Rows[0]["Type"].ToString() %>' src='<%# ConfigurationManager.AppSettings["ImagesVirtualLocation"] + ((PhotosSet.Tables["PropertyPhotos"].Rows.Count > 1) ? 
                                    PhotosSet.Tables["PropertyPhotos"].Rows[1]["FileName"].ToString () : "") %>'
                                            width='<%# (PhotosSet.Tables["PropertyPhotos"].Rows.Count > 1) ? PhotosSet.Tables["PropertyPhotos"].Rows[1]["Width"].ToString () : "0" %>'
                                            height='<%# (PhotosSet.Tables["PropertyPhotos"].Rows.Count > 1) ? PhotosSet.Tables["PropertyPhotos"].Rows[1]["Height"].ToString () : "0" %>' /></div></li>
                                    <%} %>
                                    <%if (PhotosSet.Tables["PropertyPhotos"].Rows.Count > 2)
                                      { %>
                                    <li>
                                        <div class='drop-shadow effect4'>
                                        <img  alt='<%# PropertiesFullSet.Tables["Properties"].Rows[0]["City"].ToString() +" "+ PropertiesFullSet.Tables["Properties"].Rows[0]["Type"].ToString() %>' title='<%# PropertiesFullSet.Tables["Properties"].Rows[0]["City"].ToString() +" "+ PropertiesFullSet.Tables["Properties"].Rows[0]["Type"].ToString() %>' src='<%# ConfigurationManager.AppSettings["ImagesVirtualLocation"] + ((PhotosSet.Tables["PropertyPhotos"].Rows.Count > 2) ? 
                                    PhotosSet.Tables["PropertyPhotos"].Rows[2]["FileName"].ToString () : "") %>'
                                            width='<%# (PhotosSet.Tables["PropertyPhotos"].Rows.Count > 2) ? PhotosSet.Tables["PropertyPhotos"].Rows[2]["Width"].ToString () : "0" %>'
                                            height='<%# (PhotosSet.Tables["PropertyPhotos"].Rows.Count > 2) ? PhotosSet.Tables["PropertyPhotos"].Rows[2]["Height"].ToString () : "0" %>' /></div></li>
                                    <%} %>
                                    <%if (PhotosSet.Tables["PropertyPhotos"].Rows.Count > 3)
                                      { %>
                                    <li>
                                        <div class='drop-shadow effect4'>
                                        <img  alt='<%# PropertiesFullSet.Tables["Properties"].Rows[0]["City"].ToString() +" "+ PropertiesFullSet.Tables["Properties"].Rows[0]["Type"].ToString() %>' title='<%# PropertiesFullSet.Tables["Properties"].Rows[0]["City"].ToString() +" "+ PropertiesFullSet.Tables["Properties"].Rows[0]["Type"].ToString() %>' src='<%# ConfigurationManager.AppSettings["ImagesVirtualLocation"] + ((PhotosSet.Tables["PropertyPhotos"].Rows.Count > 3) ? 
                                    PhotosSet.Tables["PropertyPhotos"].Rows[3]["FileName"].ToString () : "") %>'
                                            width='<%# (PhotosSet.Tables["PropertyPhotos"].Rows.Count > 3) ? PhotosSet.Tables["PropertyPhotos"].Rows[3]["Width"].ToString () : "0" %>'
                                            height='<%# (PhotosSet.Tables["PropertyPhotos"].Rows.Count > 3) ? PhotosSet.Tables["PropertyPhotos"].Rows[3]["Height"].ToString () : "0" %>' /></div></li>
                                    <%} %>
                                    <%if (PhotosSet.Tables["PropertyPhotos"].Rows.Count > 4)
                                      { %>
                                    <li>
                                        <div class='drop-shadow effect4'>
                                        <img alt='<%# PropertiesFullSet.Tables["Properties"].Rows[0]["City"].ToString() +" "+ PropertiesFullSet.Tables["Properties"].Rows[0]["Type"].ToString() %>' title='<%# PropertiesFullSet.Tables["Properties"].Rows[0]["City"].ToString() +" "+ PropertiesFullSet.Tables["Properties"].Rows[0]["Type"].ToString() %>' src='<%# ConfigurationManager.AppSettings["ImagesVirtualLocation"] + ((PhotosSet.Tables["PropertyPhotos"].Rows.Count > 4) ? 
                                    PhotosSet.Tables["PropertyPhotos"].Rows[4]["FileName"].ToString () : "") %>'
                                            width='<%# (PhotosSet.Tables["PropertyPhotos"].Rows.Count > 4) ? PhotosSet.Tables["PropertyPhotos"].Rows[4]["Width"].ToString () : "0" %>'
                                            height='<%# (PhotosSet.Tables["PropertyPhotos"].Rows.Count > 4) ? PhotosSet.Tables["PropertyPhotos"].Rows[4]["Height"].ToString () : "0" %>' /></div></li>
                                    <%} %>
                                    <%if (PhotosSet.Tables["PropertyPhotos"].Rows.Count > 5)
                                      { %>
                                    <li>
                                        <div class='drop-shadow effect4'>
                                        <img  alt='<%# PropertiesFullSet.Tables["Properties"].Rows[0]["City"].ToString() +" "+ PropertiesFullSet.Tables["Properties"].Rows[0]["Type"].ToString() %>' title='<%# PropertiesFullSet.Tables["Properties"].Rows[0]["City"].ToString() +" "+ PropertiesFullSet.Tables["Properties"].Rows[0]["Type"].ToString() %>' src='<%# ConfigurationManager.AppSettings["ImagesVirtualLocation"] + ((PhotosSet.Tables["PropertyPhotos"].Rows.Count > 5) ? 
                                    PhotosSet.Tables["PropertyPhotos"].Rows[5]["FileName"].ToString () : "") %>'
                                            width='<%# (PhotosSet.Tables["PropertyPhotos"].Rows.Count > 5) ? PhotosSet.Tables["PropertyPhotos"].Rows[5]["Width"].ToString () : "0" %>'
                                            height='<%# (PhotosSet.Tables["PropertyPhotos"].Rows.Count > 5) ? PhotosSet.Tables["PropertyPhotos"].Rows[5]["Height"].ToString () : "0" %>' /></div></li>
                                    <%} %>
                                    <%if (PhotosSet.Tables["PropertyPhotos"].Rows.Count > 6)
                                      { %>
                                    <li style='<%# PhotosSet.Tables["PropertyPhotos"].Rows.Count > 6 ? "visibility: visible": "visibility: collapse"  %>'>
                                        <div class='drop-shadow effect4'>
                                        <img  alt='<%# PropertiesFullSet.Tables["Properties"].Rows[0]["City"].ToString() +" "+ PropertiesFullSet.Tables["Properties"].Rows[0]["Type"].ToString() %>' title='<%# PropertiesFullSet.Tables["Properties"].Rows[0]["City"].ToString() +" "+ PropertiesFullSet.Tables["Properties"].Rows[0]["Type"].ToString() %>' src='<%# ConfigurationManager.AppSettings["ImagesVirtualLocation"] + ((PhotosSet.Tables["PropertyPhotos"].Rows.Count > 6) ? 
                                    PhotosSet.Tables["PropertyPhotos"].Rows[6]["FileName"].ToString () : "") %>'
                                            width='<%# (PhotosSet.Tables["PropertyPhotos"].Rows.Count > 6) ? PhotosSet.Tables["PropertyPhotos"].Rows[6]["Width"].ToString () : "0" %>'
                                            height='<%# (PhotosSet.Tables["PropertyPhotos"].Rows.Count > 6) ? PhotosSet.Tables["PropertyPhotos"].Rows[6]["Height"].ToString () : "0" %>' /></div></li>
                                    <%} %>
                                    <%if (PhotosSet.Tables["PropertyPhotos"].Rows.Count > 7)
                                      { %>

                                    <li style='<%# PhotosSet.Tables["PropertyPhotos"].Rows.Count > 7 ? "visibility: visible": "visibility: collapse" %>'>
                                        <div class='drop-shadow effect4'>
                                        <img  alt='<%# PropertiesFullSet.Tables["Properties"].Rows[0]["City"].ToString() +" "+ PropertiesFullSet.Tables["Properties"].Rows[0]["Type"].ToString() %>' title='<%# PropertiesFullSet.Tables["Properties"].Rows[0]["City"].ToString() +" "+ PropertiesFullSet.Tables["Properties"].Rows[0]["Type"].ToString() %>' src='<%# ConfigurationManager.AppSettings["ImagesVirtualLocation"] + ((PhotosSet.Tables["PropertyPhotos"].Rows.Count > 7) ? 
                                    PhotosSet.Tables["PropertyPhotos"].Rows[7]["FileName"].ToString () : "") %>'
                                            width='<%# (PhotosSet.Tables["PropertyPhotos"].Rows.Count > 7) ? PhotosSet.Tables["PropertyPhotos"].Rows[7]["Width"].ToString () : "0" %>'
                                            height='<%# (PhotosSet.Tables["PropertyPhotos"].Rows.Count > 7) ? PhotosSet.Tables["PropertyPhotos"].Rows[7]["Height"].ToString () : "0" %>' /></div></li>
                                    <%} %>
                                    <%if (PhotosSet.Tables["PropertyPhotos"].Rows.Count > 8)
                                      { %>
                                    <li style='<%# PhotosSet.Tables["PropertyPhotos"].Rows.Count > 8 ? "visibility: visible": "visibility: collapse" %>'>
                                        <div class='drop-shadow effect4'>
                                        <img  alt='<%# PropertiesFullSet.Tables["Properties"].Rows[0]["City"].ToString() +" "+ PropertiesFullSet.Tables["Properties"].Rows[0]["Type"].ToString() %>' title='<%# PropertiesFullSet.Tables["Properties"].Rows[0]["City"].ToString() +" "+ PropertiesFullSet.Tables["Properties"].Rows[0]["Type"].ToString() %>' src='<%# ConfigurationManager.AppSettings["ImagesVirtualLocation"] + ((PhotosSet.Tables["PropertyPhotos"].Rows.Count > 8) ? 
                                    PhotosSet.Tables["PropertyPhotos"].Rows[8]["FileName"].ToString () : "") %>'
                                            width='<%# (PhotosSet.Tables["PropertyPhotos"].Rows.Count > 8) ? PhotosSet.Tables["PropertyPhotos"].Rows[8]["Width"].ToString () : "0" %>'
                                            height='<%# (PhotosSet.Tables["PropertyPhotos"].Rows.Count > 8) ? PhotosSet.Tables["PropertyPhotos"].Rows[8]["Height"].ToString () : "0" %>' /></div></li>
                                    <%} %>
                                </ul>
                            </div>
                            
                            <a href="#" class="jcarousel-control-prev" style="color: white">&lsaquo;</a>
                            <a href="#" class="jcarousel-control-next" style="color: white">&rsaquo;</a>
                        </div>

                    </div>
         </div>
     </div>
    <div class="row">

        <div class="TitleFont">
            <h2 class="ViewPropertyPageFonts">
                <%# PropertiesFullSet.Tables["Properties"].Rows[0]["City"] %>
                <%# PropertiesFullSet.Tables["Properties"].Rows[0]["NumBedrooms"] %> Bedroom <%# PropertiesFullSet.Tables["Properties"].Rows[0]["Type"] %>
                , Sleeps <%# PropertiesFullSet.Tables["Properties"].Rows[0]["NumSleeps"] %>
                , Minimum nightly rental - <%# PropertiesFullSet.Tables["Properties"].Rows[0]["MinimumNightlyRental"] %>.
                <br />
                <%# ((int)PropertiesFullSet.Tables["Properties"].Rows[0]["IfPaid"] == 1) && (bool)PropertiesFullSet.Tables["Properties"].Rows[0]["IfShowAddress"] ? "Address: " + PropertiesFullSet.Tables["Properties"].Rows[0]["Address"] : "" %>
            </h2>
            <div class="textfont">
                <%# PropertiesFullSet.Tables["Properties"].Rows[0]["NumTVs"] %>
                TVs,
                <%# PropertiesFullSet.Tables["Properties"].Rows[0]["NumVCRs"] %>
                VCRs,
                <%# PropertiesFullSet.Tables["Properties"].Rows[0]["NumCDPlayers"] %>
                CD Players<%# (AmenitiesSet.Tables["Amenities"].Rows.Count > 0) ? "," : "." %>
                <asp:Repeater ID="Repeater9" runat="server" DataMember="Amenities" DataSource="<%# AmenitiesSet %>">
                    <HeaderTemplate>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Amenity", "{0}") %><%# !CommonFunctions.IfLastRow ((System.Data.DataRowView)Container.DataItem) ? "," : "." %>
                    </ItemTemplate>
                    <FooterTemplate>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
        </div>

          <div id="tabs" >
            <ul class="tabs">
                <li data-tab="tabs-1" class="current">Amenities</li>
                <li data-tab="tabs-5">Attractions</li>
                <li data-tab="tabs-2">Rates</li>
                <li data-tab="tabs-6">Calendar</li>
                <li data-tab="tabs-4">Reviews</li>
                <li data-tab="tabs-3">Inquire</li>

            </ul>
            <div id="tabs-1"  class="tab-content current textfont">

                <%= PropertiesFullSet.Tables["Properties"].Rows[0]["Description"] %><br />
                <%= PropertiesFullSet.Tables["Properties"].Rows[0]["Amenities"] %>
           <div class ="centered" style="margin-top:30px;">
                        <table class="PropTable10">
                            <tr>
                                <asp:Repeater ID="Repeater5" runat="server" DataMember="RoomInfo" DataSource="<%# RoomsFurnitureSet %>">
                                    <HeaderTemplate>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <td style="font-size: 14px; background-color: #ff6414; color: #f5ede3;">
                                            <%# DataBinder.Eval(Container.DataItem, "RoomTitle", "{0}") %>
                                        </td>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </tr>
                            <tr>
                                <asp:Repeater ID="Repeater6" runat="server" DataMember="RoomInfo" DataSource="<%# RoomsFurnitureSet %>">
                                    <HeaderTemplate>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <td class="PropTable10B ViewPropertyPageFonts">
                                            <asp:Repeater ID="Repeater7" runat="server" DataMember="FurnitureItems" DataSource='<%# ((System.Data.DataRowView)Container.DataItem).CreateChildView("RoomsFurniture") %>'>
                                                <HeaderTemplate>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "FurnitureItem", "{0}") %><%# !CommonFunctions.IfLastRow ((System.Data.DataRowView)Container.DataItem) ? "," : "." %>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                        </td>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </tr>
                        </table>
           </div>
            </div>

            <div id="tabs-2"  class="tab-content">
                <div align="center" class="contentfont" style="color: #343d6c;">
                    <table class="PropTable12">
                        <tr>
                            <td class="Center">
                                <a name="Rates"></a>

                            </td>
                        </tr>
                    </table>
                    <%-- <div class="PropertiesFont3">
                            
                        </div>--%>
                    
                        <div class="contentfont">
                            <%# PropertiesFullSet.Tables["Properties"].Rows[0]["Rates"] %><br />
                        </div>
                        <% if (RatesSet.Tables["Rates"].Rows.Count > 0)
                           { %>
                        <asp:Repeater ID="Repeater8" runat="server" DataMember="Rates" DataSource="<%# RatesSet %>">
                            <HeaderTemplate>
                                <table class="PropTable14">
                                    <tr>
                                        <td class="Center">Start Mo/Da/Yr
                                        </td>
                                        <td class="Center">End Mo/Da/Yr
                                        </td>
                                        <td class="Center">Nightly
                                        </td>
                                        <td class="Center">Weekly
                                        </td>
                                        <td class="Center">Monthly
                                        </td>
                                    </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td class="Center">
                                        <%# DataBinder.Eval(Container.DataItem, "StartDate", "{0:d}") %>
                                    </td>
                                    <td class="Center">
                                        <%# DataBinder.Eval(Container.DataItem, "EndDate", "{0:d}") %>
                                    </td>
                                    <td class="Center">
                                        <%# DataBinder.Eval(Container.DataItem, "Nightly", "{0:0}") %>
                                    </td>
                                    <td class="Center">
                                        <%# DataBinder.Eval(Container.DataItem, "Weekly", "{0:0}") %>
                                    </td>
                                    <td class="Center">
                                        <%# DataBinder.Eval(Container.DataItem, "Monthly", "{0:0}") %>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                        <% } %>
                    
                    <div class="contentfont">
                        Pricing for  <%# PropertiesFullSet.Tables["Properties"].Rows[0]["Type"] %> in <%# PropertiesFullSet.Tables["Properties"].Rows[0]["City"] %> <%# PropertiesFullSet.Tables["Properties"].Rows[0]["Country"] %> are quoted in
                            <%# PropertiesFullSet.Tables["Properties"].Rows[0]["PricesCurrency"] %><br />
                        <br />
                        <br />
                        <br />
                        <br />

                    </div>
                    <div class="text-left" style="font-size: 11pt; font-family: Arial; color: #000;">
                        Check in time <%# PropertiesFullSet.Tables["Properties"].Rows[0]["CheckIn"] %><br />
                        Check out time   <%# PropertiesFullSet.Tables["Properties"].Rows[0]["CheckOut"] %><br />
                        Payment methods accepted:
                            <% if (PaymentMethodsPresent())
                               { %>
                        <asp:Repeater ID="Repeater4" runat="server" DataMember="PaymentMethods" DataSource="<%# PaymentMethodsSet %>">
                            <HeaderTemplate>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "PaymentMethod", "{0}") %><%# !CommonFunctions.IfLastRow ((System.Data.DataRowView)Container.DataItem) ? "," : "." %>
                            </ItemTemplate>
                            <FooterTemplate>
                            </FooterTemplate>
                        </asp:Repeater>
                        <% }
                               else
                               { %>
                            Check with Property Owner
                            <% } %>
                        <br />
                        <% if (LodgingTaxPresent())
                           { %>
                            Property Lodging Tax :
                            <%# PropertiesFullSet.Tables["Properties"].Rows[0]["LodgingTax"] %><br />
                        <%# (PropertiesFullSet.Tables["Properties"].Rows[0]["TaxIncluded"] is bool) && (bool)PropertiesFullSet.Tables["Properties"].Rows[0]["TaxIncluded"] ? "Tax included in rates above" : "Tax not included in rates above" %><br />
                        <% } %>
                        <b>Cancellation Policy:</b>
                        <%# PropertiesFullSet.Tables["Properties"].Rows[0]["CancellationPolicy"] %><br />
                        <b>Deposit Required for <%# PropertiesFullSet.Tables["Properties"].Rows[0]["Type"] %>
                          
                            :</b>
                        <%# PropertiesFullSet.Tables["Properties"].Rows[0]["DepositRequired"] %><br />
                    </div>
                </div>

            </div>
            <div id="tabs-6"  class="tab-content">
                      <div class="centered">
                         <h3> Property Availability Calendar</h3>
                    <table style="margin-top: 10px">
                    <tr>
                        <td width="25%">
                            <asp:Calendar ID="Calendar1" runat="server" OnDayRender="Calendar1_DayRender"
                                SelectionMode="Day" BorderColor="#000000" Width="175px"
                                Font-Names="Arial" Font-Size="14px">
                                <NextPrevStyle ForeColor="Black" />
                                <TitleStyle BackColor="#ffffff" ForeColor="Black" />
                            </asp:Calendar>
                        </td>
                        <td width="25%">
                            <asp:Calendar ID="Calendar2" runat="server" OnDayRender="Calendar1_DayRender"
                                SelectionMode="Day" BorderColor="#000000" Width="175px" ShowNextPrevMonth="False"
                                Font-Names="Arial" Font-Size="14px">
                                <NextPrevStyle ForeColor="Black" />
                                <TitleStyle BackColor="#ffffff" ForeColor="Black" />
                            </asp:Calendar>
                        </td>
                        <td width="25%">
                            <asp:Calendar ID="Calendar3" runat="server" OnDayRender="Calendar1_DayRender"
                                SelectionMode="Day" BorderColor="#000000" Width="175px" ShowNextPrevMonth="False"
                                Font-Names="Arial" Font-Size="14px">
                                <NextPrevStyle ForeColor="Black" />
                                <TitleStyle BackColor="#ffffff" ForeColor="Black" />
                            </asp:Calendar>
                        </td>

                        <td width="25%">
                            <asp:Calendar ID="Calendar4" runat="server" OnDayRender="Calendar1_DayRender"
                                SelectionMode="Day" BorderColor="#000000" Width="175px" ShowNextPrevMonth="False"
                                Font-Names="Arial" Font-Size="14px">
                                <NextPrevStyle ForeColor="Black" />
                                <TitleStyle BackColor="#ffffff" ForeColor="Black" />
                            </asp:Calendar>
                        </td>

                    </tr>
                    <tr>
                        <td width="25%">
                            <asp:Calendar ID="Calendar5" runat="server" OnDayRender="Calendar1_DayRender"
                                SelectionMode="Day" BorderColor="#000000" Width="175px"
                                Font-Names="Arial" Font-Size="14px">
                                <NextPrevStyle ForeColor="Black" />
                                <TitleStyle BackColor="#ffffff" ForeColor="Black" />
                            </asp:Calendar>
                        </td>
                        <td width="25%">
                            <asp:Calendar ID="Calendar6" runat="server" OnDayRender="Calendar1_DayRender"
                                SelectionMode="Day" BorderColor="#000000" Width="175px" ShowNextPrevMonth="False"
                                Font-Names="Arial" Font-Size="14px">
                                <NextPrevStyle ForeColor="Black" />
                                <TitleStyle BackColor="#ffffff" ForeColor="Black" />
                            </asp:Calendar>
                        </td>
                        <td width="25%">
                            <asp:Calendar ID="Calendar7" runat="server" OnDayRender="Calendar1_DayRender"
                                SelectionMode="Day" BorderColor="#000000" Width="175px" ShowNextPrevMonth="False"
                                Font-Names="Arial" Font-Size="14px">
                                <NextPrevStyle ForeColor="Black" />
                                <TitleStyle BackColor="#ffffff" ForeColor="Black" />
                            </asp:Calendar>
                        </td>
                        <td width="25%">
                            <asp:Calendar ID="Calendar8" runat="server" OnDayRender="Calendar1_DayRender"
                                SelectionMode="Day" BorderColor="#000000" Width="175px" ShowNextPrevMonth="False"
                                Font-Names="Arial" Font-Size="14px">
                                <NextPrevStyle ForeColor="Black" />
                                <TitleStyle BackColor="#ffffff" ForeColor="Black" />
                            </asp:Calendar>
                        </td>

                    </tr>
                    <tr>
                        <td width="25%">
                            <asp:Calendar ID="Calendar9" runat="server" OnDayRender="Calendar1_DayRender"
                                SelectionMode="Day" BorderColor="#000000" Width="175px"
                                Font-Names="Arial" Font-Size="14px">
                                <NextPrevStyle ForeColor="Black" />
                                <TitleStyle BackColor="#ffffff" ForeColor="Black" />
                            </asp:Calendar>
                        </td>
                        <td width="25%">
                            <asp:Calendar ID="Calendar10" runat="server" OnDayRender="Calendar1_DayRender"
                                SelectionMode="Day" BorderColor="#000000" Width="175px" ShowNextPrevMonth="False"
                                Font-Names="Arial" Font-Size="14px">
                                <NextPrevStyle ForeColor="Black" />
                                <TitleStyle BackColor="#ffffff" ForeColor="Black" />
                            </asp:Calendar>
                        </td>
                        <td width="25%">
                            <asp:Calendar ID="Calendar11" runat="server" OnDayRender="Calendar1_DayRender"
                                SelectionMode="Day" BorderColor="#000000" Width="175px" ShowNextPrevMonth="False"
                                Font-Names="Arial" Font-Size="14px">
                                <NextPrevStyle ForeColor="Black" />
                                <TitleStyle BackColor="#ffffff" ForeColor="Black" />
                            </asp:Calendar>
                        </td>

                        <td width="25%">
                            <asp:Calendar ID="Calendar12" runat="server" OnDayRender="Calendar1_DayRender"
                                SelectionMode="Day" BorderColor="#000000" Width="175px"
                                Font-Names="Arial" Font-Size="14px">
                                <NextPrevStyle ForeColor="Black" />
                                <TitleStyle BackColor="#ffffff" ForeColor="Black" />
                            </asp:Calendar>
                        </td>

                    </tr>
                </table>
                </div>
            </div>
            <div id="tabs-3"  class="tab-content">
                
            </div>
            <div id="tabs-4"  class="tab-content">
                <%--reviews--%>
                <div id="divReviews" runat="server" class="contentfont" style="width: 100%;">
                    <table width="100%">
                        <tr>
                            <td style="width: 50%;">
                                 <% DateTime dt;
                                     DateTime.TryParse(PropertiesFullSet.Tables["Properties"].Rows[0]["DateCreated"].ToString(),out dt);
                            %>
                                <label>Member Since:<%= dt.ToString("MMM yyyy") %></label>
                            </td>
                            <td style="width: 50%;">
                                <a rel="nofollow" href='WriteReview.aspx?PropID=<%# propertyid %>'>Write a Review</a>
                            </td>
                        </tr>
                    </table>
                </div>
                <%--reviews--%>
            </div>
            <div id="tabs-5"  class="tab-content text-center">
                <div align="center" style="color: #1D2D33;">
                    <table class="NonTable contentfont">
                        <tr>
                            <td align="center" style="color: #000072;">
                                <a name="Attractions"></a>
                                <label class="colorOnHover"><%# PropertiesFullSet.Tables["Properties"].Rows[0]["City"] %> <%# PropertiesFullSet.Tables["Properties"].Rows[0]["Country"] %> Local Attractions</label>
                            </td>
                        </tr>
                    </table>
                    <%--attractions--%>
                    <%if (PropertiesFullSet.Tables["Properties"].Rows[0]["LocalAttractions"].ToString().Length > 0)
                      {%>
                    <div align="left" class="ViewPropertyPageFonts textfont">
                        <%# PropertiesFullSet.Tables["Properties"].Rows[0]["LocalAttractions"]%>
                    </div>
                    <%} %>
                    <%--attractions--%>
                    <div style="width: 70%; margin-top: 20px; margin-bottom: 20px">
                        <asp:Repeater ID="Repeater2" runat="server" DataMember="Attractions" DataSource="<%# AttractionsDistancesSet %>">
                            <HeaderTemplate>
                                <table class="proptable">
                                    <tr>
                                        <td align="center" colspan="4" style="background-color: #ff6600; color: White; font-size: 12pt;">
                                            <%# PropertiesFullSet.Tables["Properties"].Rows[0]["City"] %> Local Attractions
                                                
                                        </td>
                                    </tr>
                                    <tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <td >
                                    <%# DataBinder.Eval(Container.DataItem, "Attraction", "{0}") %>
                                </td>
                                <td >
                                    <%# DataBinder.Eval(Container.DataItem, "Distance", "{0}") %>
                                </td>
                                <%# IfEvenRow ((System.Data.DataRowView)Container.DataItem) ? "</tr><tr>" : "" %>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tr> </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>

            <asp:Label ID="lblInfo" runat="server" ForeColor="Red"></asp:Label>
            <div class="TitleFont" style="display:none">
                <div class="rtHeader rightSideHeaders" id="rtHd3" runat="server" style="width: 917px; text-align: left; background-color: transparent !important; font-size: 10pt; font-weight: bold;">
                </div>
                <div id="rtLow3" runat="server" style="border: none; box-shadow: none; margin-left: 0px !important; font-size: 10pt; font-weight: bold; color: #2f6547;">
                </div>
            </div>

        </div>
        <br />
     </div>

    </div>
    </div>

        <!-- Start of StatCounter Code -->


 
        <script type="text/javascript" src="http://www.statcounter.com/counter/counter.js"></script>

        <noscript>
            <div class="statcounter">
                <a title="free hit counters" href="http://www.statcounter.com/free_hit_counter.html"
                    target="_blank">
                    <img class="statcounter" src="http://c42.statcounter.com/3614019/0/5d0ed9a7/1/" alt="free hit counters" /></a>
            </div>
        </noscript>
        <!-- End of StatCounter Code -->
        <!-- End counter code -->

  

        <asp:Label ID="devnote" runat="server" Text="" BackColor="White" ForeColor="White"
            Visible="false" />
        <script src="/Assets/js/viewproperty.js"></script>
</asp:Content>
