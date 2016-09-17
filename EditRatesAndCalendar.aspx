<%@ Page Language="C#" MasterPageFile="~/MasterPageNoCss.master" AutoEventWireup="true" CodeFile="~/EditRatesAndCalendar.aspx.cs" Inherits="EditRatesAndCalendar" Title="Edit Rates And Calendar" ValidateRequest="false" EnableEventValidation="false" %>

<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="Server">
    <script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?sensor=false"></script>
    <script type="text/javascript">

        var country, state, city, pinCode;
        function createCORSRequest(method, url) {
            var xhr = new XMLHttpRequest();

            if ("withCredentials" in xhr) {
                // XHR for Chrome/Firefox/Opera/Safari.
                xhr.open(method, url, true);

            } else if (typeof XDomainRequest != "undefined") {
                // XDomainRequest for IE.
                xhr = new XDomainRequest();
                xhr.open(method, url);

            } else {
                // CORS not supported.
                xhr = null;
            }
            return xhr;
        }

    </script>

    <div id="divJS" runat="server">
    </div>
    <div class="left">
        <% if (BackLink.Visible)
           { %>
        <table bgcolor="#e4e4af" cellspacing="0" cellpadding="0" width="250" align="center"
            border="2">
            <tr>
                <td>
                    <div align="center">
                        <strong>
                            <asp:HyperLink ID="BackLink" runat="server" NavigateUrl="Listings.aspx">
							Return to My Account page
                            </asp:HyperLink>
                        </strong>
                    </div>
                </td>
            </tr>
        </table>
        <asp:Label ID="lblInfo" runat="server" ForeColor="Red" EnableViewState="False"></asp:Label>
        <br />
        <% } %>
        <br />
        <br />
        <br />
        <div class="frmEditProp">
            
            <table border="2" cellspacing="0" bordercolor="#6699cc" bgcolor="#ececd9">
                <tr>
                    <td style="vertical-align:top; background-color:#fff;">
                        <font size="4"><b>Rates:
					<br />
                    </b></font>
                        <br />
                        <font size="2">In order for your listing to be saved, you must click on the button "Next
					Step" at the bottom of the page. </font>
                    </td>
                    <td align="left">These rates are to provide visitors a general idea of price range for your property and are displayed on the city pages.<br />
                        <div style="text-align: left">
                            <hr size="1" width="75%" />
                        </div>
                        Lowest Nightly Rate&nbsp;&nbsp;
                <asp:TextBox ID="txtReqLoRate" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;<asp:DropDownList ID="ddlCurrencies"
                    runat="server" AppendDataBoundItems="True">
                    <asp:ListItem>Currency</asp:ListItem>
                </asp:DropDownList>
                        <asp:Label ID="lblCurrReq" runat="server" Text="(Required)" ForeColor="Red"></asp:Label><br />
                        Highest Nightly Rate&nbsp;&nbsp;<asp:TextBox ID="txtReqHiRate" runat="server"></asp:TextBox>
                        <asp:RangeValidator ID="RangeValidator1" runat="server"
                            ErrorMessage="Please enter integer amount" ControlToValidate="txtReqLoRate"
                            Display="Dynamic" MaximumValue="100000" MinimumValue="0" Type="Integer"
                            SetFocusOnError="True">Please enter integer amount</asp:RangeValidator>
                        <asp:RangeValidator ID="RangeValidator2" runat="server"
                            ErrorMessage="RangeValidator" ControlToValidate="txtReqHiRate"
                            Display="Dynamic" MaximumValue="100000" MinimumValue="0" SetFocusOnError="True"
                            Type="Integer">Please enter integer amount</asp:RangeValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
                            ErrorMessage="Please enter minimum nightly rate"
                            ControlToValidate="txtReqLoRate" Display="Dynamic" SetFocusOnError="True">Please enter minimum nightly rate</asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server"
                            ErrorMessage="Please select a currency" ControlToValidate="ddlCurrencies"
                            Text="Please select a currency" InitialValue="Currency" Display="Dynamic"
                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server"
                            ErrorMessage="Please enter maximum nightly rate" ControlToValidate="txtReqHiRate"
                            Display="Dynamic" SetFocusOnError="True">Please enter maximum nightly rate</asp:RequiredFieldValidator><br />
                        <hr size="1" width="75%" />

                        This field displays rates on your individual property page
				<asp:TextBox ID="Rates" TabIndex="8" runat="server" Text='<%# DataBinder.Eval(PropertiesSet, "Tables[Properties].DefaultView.[0].Rates", "{0}") %>'
                    Width="600px" Height="100px" TextMode="MultiLine" /><br />

                        You are allowed to enter text in the above box and / or use the table below.
                
				<br />
                        <br />

                        <asp:DataGrid ID="RatesList" runat="server" DataKeyField="ID" DataMember="Rates"
                            DataSource='<%# RatesSet %>' AutoGenerateColumns="False" ShowFooter="True">
                            <Columns>
                                <asp:TemplateColumn HeaderText="Start Date - mo/da/year">
                                    <ItemTemplate>
                                        <asp:TextBox ID="StartDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "StartDate", "{0:d}") %>' />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="AddStartDate" runat="Server" style="width:98%;" />
                                    </FooterTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="End Date - mo/da/year">
                                    <ItemTemplate>
                                        <asp:TextBox ID="EndDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "EndDate", "{0:d}") %>' />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="AddEndDate" runat="Server" style="width:98%;" />
                                    </FooterTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Nightly">
                                    <ItemTemplate>
                                        <asp:TextBox ID="Nightly" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Nightly", "{0:G4}") %>' />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="AddNightly" runat="Server" style="width:98%;" />
                                    </FooterTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Weekly">
                                    <ItemTemplate>
                                        <asp:TextBox ID="Weekly" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Weekly", "{0:G4}") %>' />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="AddWeekly" runat="Server" style="width:98%;" />
                                    </FooterTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Monthly">
                                    <ItemTemplate>
                                        <asp:TextBox ID="Monthly" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Monthly", "{0:G4}") %>' />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="AddMonthly" runat="Server" style="width:98%;" />
                                    </FooterTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="">
                                    <ItemTemplate>
                                        <asp:Button CommandName="Delete" Text="Delete" ID="DeleteButton" runat="server" />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Button CommandName="Insert" Text="Add" ID="AddButton" runat="server" />
                                    </FooterTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                        </asp:DataGrid>
                        <hr>
                        <br />
                        Prices are quoted in
				<asp:TextBox ID="PricesCurrency" TabIndex="8" runat="server" MaxLength="50" Text='<%# DataBinder.Eval(PropertiesSet, "Tables[Properties].DefaultView.[0].PricesCurrency", "{0}") %>'
                    Width="100px" />
                        <asp:RegularExpressionValidator ID="Regularexpressionvalidator12" runat="server"
                            Display="Dynamic" ControlToValidate="PricesCurrency" ErrorMessage="Too long prices currency entered"
                            ValidationExpression="^[\s\S]{1,50}$" />
                        <br />
                        Check In:
				<asp:DropDownList ID="CheckIn" runat="server" Width="200px" SelectedValue='<%# DataBinder.Eval(PropertiesSet, "Tables[Properties].DefaultView.[0].CheckIn", "{0}") %>'
                    Height="24px">
                    <asp:ListItem Value="Check with Owner">Check with Owner</asp:ListItem>
                    <asp:ListItem Value="1 AM">1 AM</asp:ListItem>
                    <asp:ListItem Value="2 AM">2 AM</asp:ListItem>
                    <asp:ListItem Value="3 AM">3 AM</asp:ListItem>
                    <asp:ListItem Value="4 AM">4 AM</asp:ListItem>
                    <asp:ListItem Value="5 AM">5 AM</asp:ListItem>
                    <asp:ListItem Value="6 AM">6 AM</asp:ListItem>
                    <asp:ListItem Value="7 AM">7 AM</asp:ListItem>
                    <asp:ListItem Value="8 AM">8 AM</asp:ListItem>
                    <asp:ListItem Value="9 AM">9 AM</asp:ListItem>
                    <asp:ListItem Value="10 AM">10 AM</asp:ListItem>
                    <asp:ListItem Value="11 AM">11 AM</asp:ListItem>
                    <asp:ListItem Value="12 PM">12 PM</asp:ListItem>
                    <asp:ListItem Value="1 PM">1 PM</asp:ListItem>
                    <asp:ListItem Value="2 PM">2 PM</asp:ListItem>
                    <asp:ListItem Value="3 PM">3 PM</asp:ListItem>
                    <asp:ListItem Value="4 PM">4 PM</asp:ListItem>
                    <asp:ListItem Value="5 PM">5 PM</asp:ListItem>
                    <asp:ListItem Value="6 PM">6 PM</asp:ListItem>
                    <asp:ListItem Value="7 PM">7 PM</asp:ListItem>
                    <asp:ListItem Value="8 PM">8 PM</asp:ListItem>
                    <asp:ListItem Value="9 PM">9 PM</asp:ListItem>
                    <asp:ListItem Value="10 PM">10 PM</asp:ListItem>
                    <asp:ListItem Value="11 PM">11 PM</asp:ListItem>
                    <asp:ListItem Value="12 AM">12 AM</asp:ListItem>
                </asp:DropDownList>
                        <asp:RegularExpressionValidator ID="Regularexpressionvalidator11" runat="server"
                            Display="Dynamic" ControlToValidate="CheckIn" ErrorMessage="Too long check in value entered"
                            ValidationExpression="^[\s\S]{1,50}$" />
                        <br />
                        Check Out:
				<asp:DropDownList ID="CheckOut" runat="server" Width="200px" SelectedValue='<%# DataBinder.Eval(PropertiesSet, "Tables[Properties].DefaultView.[0].CheckOut", "{0}") %>'
                    Height="24px">
                    <asp:ListItem Value="Check with Owner">Check with Owner</asp:ListItem>
                    <asp:ListItem Value="1 AM">1 AM</asp:ListItem>
                    <asp:ListItem Value="2 AM">2 AM</asp:ListItem>
                    <asp:ListItem Value="3 AM">3 AM</asp:ListItem>
                    <asp:ListItem Value="4 AM">4 AM</asp:ListItem>
                    <asp:ListItem Value="5 AM">5 AM</asp:ListItem>
                    <asp:ListItem Value="6 AM">6 AM</asp:ListItem>
                    <asp:ListItem Value="7 AM">7 AM</asp:ListItem>
                    <asp:ListItem Value="8 AM">8 AM</asp:ListItem>
                    <asp:ListItem Value="9 AM">9 AM</asp:ListItem>
                    <asp:ListItem Value="10 AM">10 AM</asp:ListItem>
                    <asp:ListItem Value="11 AM">11 AM</asp:ListItem>
                    <asp:ListItem Value="12 PM">12 PM</asp:ListItem>
                    <asp:ListItem Value="1 PM">1 PM</asp:ListItem>
                    <asp:ListItem Value="2 PM">2 PM</asp:ListItem>
                    <asp:ListItem Value="3 PM">3 PM</asp:ListItem>
                    <asp:ListItem Value="4 PM">4 PM</asp:ListItem>
                    <asp:ListItem Value="5 PM">5 PM</asp:ListItem>
                    <asp:ListItem Value="6 PM">6 PM</asp:ListItem>
                    <asp:ListItem Value="7 PM">7 PM</asp:ListItem>
                    <asp:ListItem Value="8 PM">8 PM</asp:ListItem>
                    <asp:ListItem Value="9 PM">9 PM</asp:ListItem>
                    <asp:ListItem Value="10 PM">10 PM</asp:ListItem>
                    <asp:ListItem Value="11 PM">11 PM</asp:ListItem>
                    <asp:ListItem Value="12 AM">12 AM</asp:ListItem>
                </asp:DropDownList>
                        <asp:RegularExpressionValidator ID="Regularexpressionvalidator10" runat="server"
                            Display="Dynamic" ControlToValidate="CheckOut" ErrorMessage="Too long check out value entered"
                            ValidationExpression="^[\s\S]{1,50}$" />
                        <br />
                        Payment Methods:
				<asp:CheckBoxList ID="PaymentMethodsList" runat="server" DataTextFormatString="{0}"
                    DataValueField="ID" DataTextField="PaymentMethod" DataMember="PaymentMethods"
                    DataSource='<%# PaymentMethodsSet %>' RepeatColumns="1" RepeatDirection="Horizontal">
                </asp:CheckBoxList>
                        Lodging Tax:
				<asp:TextBox ID="LodgingTax" TabIndex="8" runat="server" MaxLength="300" Text='<%# DataBinder.Eval(PropertiesSet, "Tables[Properties].DefaultView.[0].LodgingTax", "{0}") %>'
                    Width="100px" />
                        <asp:RegularExpressionValidator ID="Regularexpressionvalidator9" runat="server" Display="Dynamic"
                            ControlToValidate="LodgingTax" ErrorMessage="Too long lodging tax entered" ValidationExpression="^[\s\S]{1,300}$" />
                        <br />
                        Tax Included in Rates above
				<asp:RadioButton ID="TaxIncluded" runat="server" Width="50px" Checked='<%# (PropertiesSet.Tables["Properties"].Rows[0]["TaxIncluded"] is bool) ? (bool)PropertiesSet.Tables["Properties"].Rows[0]["TaxIncluded"] : false %>'
                    Height="24px" Text="Yes" GroupName="0"></asp:RadioButton>
                        <asp:RadioButton ID="TaxNotIncluded" runat="server" Width="50px" Checked='<%# (PropertiesSet.Tables["Properties"].Rows[0]["TaxIncluded"] is bool) ? !(bool)PropertiesSet.Tables["Properties"].Rows[0]["TaxIncluded"] : false %>'
                            Height="24px" Text="No" GroupName="0"></asp:RadioButton>
                    </td>
                </tr>
                <tr>
                    <td valign="top" bgcolor="#996600">
                        <asp:Button ID="SubmitButton" TabIndex="20" runat="server" Text="NEXT STEP" Width="135px"
                            OnClick="SubmitButton_Click" OnClientClick="return validateForm();"
                            CausesValidation="False" />
                    </td>
                    <td valign="top">
                        <asp:Button ID="CancelButton" TabIndex="21" runat="server" Text="Cancel" Width="96px"
                            CausesValidation="False" OnClick="CancelButton_Click" />
                        <br />
                        <font size="-1"><font color="#ff0000">(Fields marked required must have a value for
					your listing to be submitted.
					<br />
                        After clicking "Next Step" you should be redirected to the upload photo page.<br />
                        If this does not happen, then one of the required fields maybe the problem.<br />
                        It could be an invalid character in the town name. Examples of invalid characters
					are ó ú, Ñ, é ,. It could be a space at the end of the town name.) </font></font>
                    </td>
                </tr>
            </table>
            </div>
        <div id="divPriTypes" runat="server">
        </div>
        <div id="divCounties" runat="server">
        </div>
        <script language="javascript" type="text/javascript">
            <%= DropDownScript() %>
	    
	    
        </script>
        <script language="javascript" type="text/javascript">
	<!--
    function ProcessValidators() {
        var i, val;
        for (i = 0; i < Page_Validators.length; i++) {
            val = Page_Validators[i];
            if (typeof (val.evaluationfunction) == "function") {
                if (eval("val.evaluationfunction == RequiredFieldValidatorEvaluateIsValid;"))
                    eval("val.evaluationfunction = RequiredFieldAlertValidate;");
                else if (eval("val.evaluationfunction == RegularExpressionValidatorEvaluateIsValid;"))
                    eval("val.evaluationfunction = RegularExpressionAlertValidate;");
            }
        }
    }

    function RequiredFieldAlertValidate(val) {
        var result;
        result = RequiredFieldValidatorEvaluateIsValid(val)
        if (!result)
            window.alert(val.errormessage);
        return result;
    }

    function RegularExpressionAlertValidate(val) {
        var result;
        result = RegularExpressionValidatorEvaluateIsValid(val)
        if (!result)
            window.alert(val.errormessage);
        return result;
    }
    // -->
        </script>





    </div>
</asp:Content>
