<%@ Page Language="C#" MasterPageFile="~/masterpage/NormalMaster.master" AutoEventWireup="true" CodeFile="MakePayment.aspx.cs" Inherits="MakePayment" Title="Make Payment" %>

<asp:Content ID="head" ContentPlaceHolderID="head" runat="server">
    Make Payment
</asp:Content>
<asp:Content ID="links" ContentPlaceHolderID="links" runat="server">
    <link href="/Assets/css/makepayment.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content" ContentPlaceHolderID="bodycontent" Runat="Server">
    <div class="internalpagewidth">
        <div class=" newline">
    <div class="left" style="padding-left:10px; padding-right:10px;">
	<% if (BackLink.Visible) { %>
    <table bgcolor="#F5EDE3" cellspacing="0" cellpadding="0" width="350" align="center" border="2">
        <tr>
            <td>
                <div align="center">
                    <strong>
                        <asp:HyperLink ID="BackLink" runat="server" NavigateUrl="ViewInvoices.aspx">
							Return to invoices page
                        </asp:HyperLink>
                    </strong>
                </div>
            </td>
        </tr>
    </table>
	<br />
	<% } %>
    Thank you for listing your property. To make payment with a credit card; please
    select the button below. If you find shopping cart difficult to use; we can process your
    credit card through another company, or you can send payment by Paypal to 
        ar@vacations-abroad.com. <br /> <br />We only accept Visa, MasterCard, or Diners Club</b><br />
	<br />
	<asp:Label ID="WrongPaymentInformation" runat="server" ForeColor="Red" Text="Error processing payment:"
		Visible="False"></asp:Label><br />
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
		<% if (auctionid == -1) { %>
        <tr>
            <td>
                <asp:Label ID="Label4" runat="server" Height="16px">Property Number</asp:Label>
            </td>
            <td>
				<%= (propertyid != -1) ? propertyid.ToString () : "" %>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label5" runat="server" Height="16px">Invoice Number</asp:Label>
            </td>
            <td>
				<%= (invoiceid != -1) ? invoiceid.ToString () : "" %>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label6" runat="server" Height="16px">Invoice Date</asp:Label>
            </td>
            <td>
				<%= (invoiceid != -1) ? ((DateTime)MainDataSet.Tables["Invoices"].Rows[0]["InvoiceDate"]).ToLongDateString () : DateTime.Now.ToLongDateString () %>
            </td>
        </tr>
        <% } else { %>
        <tr>
            <td style="height: 19px">
                <asp:Label ID="Label8" runat="server" Height="16px">Auction Number</asp:Label>
            </td>
            <td style="height: 19px">
				<%= (auctionid != -1) ? auctionid.ToString () : "" %>
            </td>
        </tr>
        <% } %>
        <tr>
            <td style="height: 19px">
                <asp:Label ID="Label7" runat="server" Height="16px">Invoice Amount</asp:Label>
            </td>
            <td style="height: 19px">
				<%//LMG: problem here, "System.FormatException: Input string was not in a correct format.": (auctionid != -1) ? 5.ToString ("c") : ((invoiceid != -1) ? ((decimal)MainDataSet.Tables["Invoices"].Rows[0]["InvoiceAmount"]).ToString ("c") : int.Parse (System.Configuration.ConfigurationManager.AppSettings["AnnualListingFee"]).ToString ("c")) %>
                $50 USD
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Height="16px">Credit Card Type</asp:Label>
            </td>
            <td>
                <asp:DropDownList Runat="server" ID="CreditCardType" AutoPostBack="True" OnSelectedIndexChanged="CreditCardType_SelectedIndexChanged">
								<asp:ListItem Selected="True" value="Visa">Visa</asp:ListItem>
								<asp:ListItem value="MasterCard">MasterCard</asp:ListItem>
								<asp:ListItem value="Discover">Discover</asp:ListItem>
								<asp:ListItem value="Amex">American Express</asp:ListItem>
							</asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="height: 24px">
                <asp:Label ID="Label2" runat="server" Height="16px">Credit Card Number</asp:Label>
            </td>
            <td style="height: 24px">
                <asp:TextBox ID="CreditCardNumber" runat="server" Width="200px" MaxLength="19" />
				<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="CreditCardNumber"
					ErrorMessage="RequiredFieldValidator" Display="Dynamic">Please enter credit card number</asp:RequiredFieldValidator>
				<asp:RegularExpressionValidator ID="CCValid" runat="server" ControlToValidate="CreditCardNumber"
					ErrorMessage="RegularExpressionValidator" Display="Dynamic"
					ValidationExpression="^\d{4}[ -]{0,1}\d{4}[ -]{0,1}\d{4}[ -]{0,1}\d{4}$">Credit card number invalid</asp:RegularExpressionValidator></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label11" runat="server" Height="16px">Credit Card CVV2 Code</asp:Label>
            </td>
            <td>
                <asp:TextBox ID="CVV2" runat="server" Width="50px" MaxLength="3" />
				<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="CVV2"
					Display="Dynamic" ErrorMessage="RequiredFieldValidator">Please enter credit card CVV2 code</asp:RequiredFieldValidator>
				<asp:RegularExpressionValidator ID="CVV2Valid" runat="server" ControlToValidate="CVV2"
					ErrorMessage="RegularExpressionValidator" ValidationExpression="^\d{3}$">CVV2 code invalid</asp:RegularExpressionValidator></td>
        </tr>
        <tr>
            <td style="height: 43px">
                <asp:Label ID="Label3" runat="server" Height="16px">Credit Card Expiration</asp:Label>
            </td>
            <td style="height: 43px">
                    <select id="ExpirationMonth" name="ExpirationMonth" runat="server">
								<option value="01" selected>01</option>
								<option value="02">02</option>
								<option value="03">03</option>
								<option value="04">04</option>
								<option value="05">05</option>
								<option value="06">06</option>
								<option value="07">07</option>
								<option value="08">08</option>
								<option value="09">09</option>
								<option value="10">10</option>
								<option value="11">11</option>
								<option value="12">12</option>
							</select>
                / 
                							<select id="ExpirationYear" name="ExpirationYear" runat="server">

								
								<option value="2015">2015</option>
								<option value="2016">2016</option>
								<option value="2017">2017</option>
								<option value="2018">2018</option>
								<option value="2019">2019</option>
								<option value="2020">2020</option>
								<option value="2021">2021</option>
    								<option value="2022">2022</option>
							</select>
				<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ExpirationMonth"
					ErrorMessage="RequiredFieldValidator" Display="Dynamic">Please enter expiration month</asp:RequiredFieldValidator>
				<asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="ExpirationMonth"
					ErrorMessage="RegularExpressionValidator" Display="Dynamic"
					ValidationExpression="^\d{2}$">Expiration month invalid</asp:RegularExpressionValidator><td>
                        &nbsp; &nbsp;
				</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label9" runat="server" Height="16px">First Name On Card</asp:Label>
            </td>
            <td>
                <asp:TextBox ID="CreditCardFirstName" runat="server" Width="200px" MaxLength="50" />
				<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="CreditCardFirstName"
					ErrorMessage="RequiredFieldValidator" Display="Dynamic">Please enter first name on card</asp:RequiredFieldValidator>
				<asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="CreditCardFirstName"
					ErrorMessage="RegularExpressionValidator" ValidationExpression="^[a-zA-Z #\-\,\.]{1,50}$">First name invalid</asp:RegularExpressionValidator></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label10" runat="server" Height="16px">Last Name On Card</asp:Label>
            </td>
            <td>
                <asp:TextBox ID="CreditCardLastName" runat="server" Width="200px" MaxLength="50" />
				<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="CreditCardLastName"
					ErrorMessage="RequiredFieldValidator" Display="Dynamic">Please enter last name on card</asp:RequiredFieldValidator>
				<asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="CreditCardLastName"
					ErrorMessage="RegularExpressionValidator" ValidationExpression="^[a-zA-Z #\-\,\.]{1,50}$">Last name invalid</asp:RegularExpressionValidator></td>
        </tr>
        <tr>
            <td colspan="100">
                <asp:Label ID="Label18" runat="server" Height="16px">Billing address:</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label12" runat="server" Height="16px">Address 1</asp:Label>
            </td>
            <td>
                <asp:TextBox ID="Address1" runat="server" Width="200px" MaxLength="300" />
				<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="Address1"
					Display="Dynamic" ErrorMessage="RequiredFieldValidator">Please enter address 1</asp:RequiredFieldValidator>
				<asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="Address1"
					ErrorMessage="RegularExpressionValidator" ValidationExpression="^[a-zA-Z0-9 #\'\-\,\.]{1,300}$">Address 1 invalid or too long</asp:RegularExpressionValidator>
			</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label13" runat="server"  >Address 2</asp:Label>
            </td>
            <td>
                <asp:TextBox ID="Address2" runat="server" Width="200px" MaxLength="300" />
				<asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="Address2"
					ErrorMessage="RegularExpressionValidator" ValidationExpression="^[a-zA-Z0-9 #\'\-\,\.]{1,300}$">Address 2 invalid or too long</asp:RegularExpressionValidator>
			</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label14" runat="server" >City</asp:Label>
            </td>
            <td>
                <asp:TextBox ID="City" runat="server" Width="200px" MaxLength="50" />
				<asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="City"
					ErrorMessage="RequiredFieldValidator" Display="Dynamic">Please enter city</asp:RequiredFieldValidator>
				<asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="City"
					ErrorMessage="RegularExpressionValidator" ValidationExpression="^[a-zA-Z0-9 #\'\-\,\. ]{1,50}$">City invalid</asp:RegularExpressionValidator>
			</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label15" runat="server"  >State / Province</asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="State" runat="server" Width="206px">
					<asp:ListItem Value=Null>Please select state</asp:ListItem>
					<asp:ListItem Value="AL">Alabama</asp:ListItem>
					<asp:ListItem Value="AK">Alaska</asp:ListItem>
					<asp:ListItem Value="AZ">Arizona</asp:ListItem>
					<asp:ListItem Value="AR">Arkansas</asp:ListItem>
					<asp:ListItem Value="CA">California</asp:ListItem>
					<asp:ListItem Value="CO">Colorado</asp:ListItem>
					<asp:ListItem Value="CT">Connecticut</asp:ListItem>
					<asp:ListItem Value="DE">Delaware</asp:ListItem>
					<asp:ListItem Value="FL">Florida</asp:ListItem>
					<asp:ListItem Value="GA">Georgia</asp:ListItem>
					<asp:ListItem Value="HI">Hawaii</asp:ListItem>
					<asp:ListItem Value="ID">Idaho</asp:ListItem>
					<asp:ListItem Value="IL">Illinois</asp:ListItem>
					<asp:ListItem Value="IN">Indiana</asp:ListItem>
					<asp:ListItem Value="IA">Iowa</asp:ListItem>
					<asp:ListItem Value="KS">Kansas</asp:ListItem>
					<asp:ListItem Value="KY">Kentucky</asp:ListItem>
					<asp:ListItem Value="LA">Louisiana</asp:ListItem>
					<asp:ListItem Value="ME">Maine</asp:ListItem>
					<asp:ListItem Value="MD">Maryland</asp:ListItem>
					<asp:ListItem Value="MA">Massachusetts</asp:ListItem>
					<asp:ListItem Value="MI">Michigan</asp:ListItem>
					<asp:ListItem Value="MN">Minnesota</asp:ListItem>
					<asp:ListItem Value="MS">Mississipi</asp:ListItem>
					<asp:ListItem Value="MO">Missouri</asp:ListItem>
					<asp:ListItem Value="MT">Montana</asp:ListItem>
					<asp:ListItem Value="NE">Nebraska</asp:ListItem>
					<asp:ListItem Value="NV">Nevada</asp:ListItem>
					<asp:ListItem Value="NH">New Hampshire</asp:ListItem>
					<asp:ListItem Value="NJ">New Jersey</asp:ListItem>
					<asp:ListItem Value="NM">New Mexico</asp:ListItem>
					<asp:ListItem Value="NY">New York</asp:ListItem>
					<asp:ListItem Value="NC">North Carolina</asp:ListItem>
					<asp:ListItem Value="ND">North Dakota</asp:ListItem>
					<asp:ListItem Value="OH">Ohio</asp:ListItem>
					<asp:ListItem Value="OK">Oklahoma</asp:ListItem>
					<asp:ListItem Value="OR">Oregon</asp:ListItem>
					<asp:ListItem Value="PA">Pennsylvania</asp:ListItem>
					<asp:ListItem Value="RI">Rhode Island</asp:ListItem>
					<asp:ListItem Value="SC">South Carolina</asp:ListItem>
					<asp:ListItem Value="SD">South Dakota</asp:ListItem>
					<asp:ListItem Value="SW">Sweden</asp:ListItem>
					<asp:ListItem Value="TN">Tennessee</asp:ListItem>
					<asp:ListItem Value="TX">Texas</asp:ListItem>
					<asp:ListItem Value="UT">Utah</asp:ListItem>
					<asp:ListItem Value="VT">Vermont</asp:ListItem>
					<asp:ListItem Value="VA">Virginia</asp:ListItem>
					<asp:ListItem Value="WA">Washington</asp:ListItem>
					<asp:ListItem Value="DC">Washington D.C.</asp:ListItem>
					<asp:ListItem Value="WV">West Virginia</asp:ListItem>
					<asp:ListItem Value="WI">Wisconsin</asp:ListItem>
					<asp:ListItem Value="WY">Wyoming</asp:ListItem>
                </asp:DropDownList>
				<asp:RequiredFieldValidator ID="StateRequired" runat="server" ControlToValidate="State"
					ErrorMessage="RequiredFieldValidator" Display="Dynamic">Please enter state/province</asp:RequiredFieldValidator>
                <asp:TextBox ID="Province" runat="server" Width="200px" MaxLength="50" Visible="False" />
				<asp:RequiredFieldValidator ID="ProvinceRequired" runat="server" ControlToValidate="Province"
					ErrorMessage="RequiredFieldValidator" Display="Dynamic">Please enter state/province</asp:RequiredFieldValidator>
				<asp:RegularExpressionValidator ID="ProvinceValid" runat="server" ControlToValidate="Province"
					ErrorMessage="RegularExpressionValidator" ValidationExpression="^[a-zA-Z0-9 #\'\-\,\.]{1,50}$">State/province invalid or too long</asp:RegularExpressionValidator>
			</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label16" runat="server" >Zip / Postal Code</asp:Label>
            </td>
            <td>
                <asp:TextBox ID="Zip" runat="server" Width="200px" MaxLength="6" />&nbsp;
				<asp:RegularExpressionValidator ID="ZipValid" runat="server" ControlToValidate="Zip"
					ErrorMessage="RegularExpressionValidator" ValidationExpression="^[a-zA-Z0-9]{3,6}$">Zip / postal code invalid</asp:RegularExpressionValidator>
			</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label17" runat="server"  >Country</asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="Country" runat="server"  Width="206px" AutoPostBack="True" OnSelectedIndexChanged="Country_SelectedIndexChanged">
					<asp:ListItem Value="US">United States</asp:ListItem>
					<asp:ListItem Value="UM">United States Minor Outlying Islands</asp:ListItem>
					<asp:ListItem Value="AF">Afghanistan</asp:ListItem>
					<asp:ListItem Value="AL">Albania</asp:ListItem>
					<asp:ListItem Value="DZ">Algeria</asp:ListItem>
					<asp:ListItem Value="AS">American Samoa</asp:ListItem>
					<asp:ListItem Value="AD">Andorra</asp:ListItem>
					<asp:ListItem Value="AO">Angola</asp:ListItem>
					<asp:ListItem Value="AI">Anguilla</asp:ListItem>
					<asp:ListItem Value="AQ">Antarctica</asp:ListItem>
					<asp:ListItem Value="AG">Antigua and Barbuda</asp:ListItem>
					<asp:ListItem Value="AR">Argentina</asp:ListItem>
					<asp:ListItem Value="AM">Armenia</asp:ListItem>
					<asp:ListItem Value="AW">Aruba</asp:ListItem>
					<asp:ListItem Value="AU">Australia</asp:ListItem>
					<asp:ListItem Value="AT">Austria</asp:ListItem>
					<asp:ListItem Value="AZ">Azerbaijan</asp:ListItem>
					<asp:ListItem Value="BS">Bahamas</asp:ListItem>
					<asp:ListItem Value="BH">Bahrain</asp:ListItem>
					<asp:ListItem Value="BD">Bangladesh</asp:ListItem>
					<asp:ListItem Value="BB">Barbados</asp:ListItem>
					<asp:ListItem Value="BY">Belarus</asp:ListItem>
					<asp:ListItem Value="BE">Belgium</asp:ListItem>
					<asp:ListItem Value="BZ">Beliz </asp:ListItem>
					<asp:ListItem Value="BJ">Benin</asp:ListItem>
					<asp:ListItem Value="BM">Bermuda</asp:ListItem>
					<asp:ListItem Value="BT">Bhutan</asp:ListItem>
					<asp:ListItem Value="BO">Bolivia</asp:ListItem>
					<asp:ListItem Value="BA">Bosnia and Herzegowina</asp:ListItem>
					<asp:ListItem Value="BW">Botswana</asp:ListItem>
					<asp:ListItem Value="BV">Bouvet Island</asp:ListItem>
					<asp:ListItem Value="BR">Brazil</asp:ListItem>
					<asp:ListItem Value="IO">British Indian Ocean Territory</asp:ListItem>
					<asp:ListItem Value="BN">Brunei Darussalam</asp:ListItem>
					<asp:ListItem Value="BG">Bulgaria</asp:ListItem>
					<asp:ListItem Value="BI">Burundi</asp:ListItem>
					<asp:ListItem Value="BF">Burkina Faso</asp:ListItem>
					<asp:ListItem Value="KH">Cambodia</asp:ListItem>
					<asp:ListItem Value="CM">Cameroon</asp:ListItem>
					<asp:ListItem Value="CA">Canada</asp:ListItem>
					<asp:ListItem Value="CV">Cape Verde</asp:ListItem>
					<asp:ListItem Value="KY">Cayman Islands</asp:ListItem>
					<asp:ListItem Value="CF">Central African Republic</asp:ListItem>
					<asp:ListItem Value="TD">Chad</asp:ListItem>
					<asp:ListItem Value="CL">Chile</asp:ListItem>
					<asp:ListItem Value="CN">China</asp:ListItem>
					<asp:ListItem Value="CX">Christmas Island</asp:ListItem>
					<asp:ListItem Value="CC">Cocos (Keeling) Islands</asp:ListItem>
					<asp:ListItem Value="CO">Colombia</asp:ListItem>
					<asp:ListItem Value="KM">Comoros</asp:ListItem>
					<asp:ListItem Value="CD">Congo</asp:ListItem>
					<asp:ListItem Value="CG">Congo</asp:ListItem>
					<asp:ListItem Value="CK">Cook Islands</asp:ListItem>
					<asp:ListItem Value="CR">Costa Rica</asp:ListItem>
					<asp:ListItem Value="CI">Cote D'Ivoire</asp:ListItem>
					<asp:ListItem Value="HR">Croatia</asp:ListItem>
					<asp:ListItem Value="CU">Cuba</asp:ListItem>
					<asp:ListItem Value="CZ">Czech Republic</asp:ListItem>
					<asp:ListItem Value="CY">Cyprus</asp:ListItem>
					<asp:ListItem Value="KP">Democratic People's Republic of Korea</asp:ListItem>
					<asp:ListItem Value="DK">Denmark</asp:ListItem>
					<asp:ListItem Value="DJ">Djibouti</asp:ListItem>
					<asp:ListItem Value="DM">Dominica</asp:ListItem>
					<asp:ListItem Value="DO">Dominican Republic</asp:ListItem>
					<asp:ListItem Value="TP">East Timor</asp:ListItem>
					<asp:ListItem Value="EC">Ecuador</asp:ListItem>
					<asp:ListItem Value="SV">El Salvador</asp:ListItem>
					<asp:ListItem Value="EG">Egypt</asp:ListItem>
					<asp:ListItem Value="GQ">Equatorial Guinea</asp:ListItem>
					<asp:ListItem Value="ER">Eritrea</asp:ListItem>
					<asp:ListItem Value="EE">Estonia</asp:ListItem>
					<asp:ListItem Value="ET">Ethiopia</asp:ListItem>
					<asp:ListItem Value="FK">Falkland Islands (Malvinas)</asp:ListItem>
					<asp:ListItem Value="FO">Faroe Islands</asp:ListItem>
					<asp:ListItem Value="FJ">Fiji</asp:ListItem>
					<asp:ListItem Value="FI">Finland</asp:ListItem>
					<asp:ListItem Value="FR">France</asp:ListItem>
					<asp:ListItem Value="FX">France, Metropolitan</asp:ListItem>
					<asp:ListItem Value="GF">French Guiana</asp:ListItem>
					<asp:ListItem Value="PF">French Polynesia</asp:ListItem>
					<asp:ListItem Value="TF">French Southern Territories</asp:ListItem>
					<asp:ListItem Value="GA">Gabon</asp:ListItem>
					<asp:ListItem Value="GM">Gambia</asp:ListItem>
					<asp:ListItem Value="GE">Georgia</asp:ListItem>
					<asp:ListItem Value="DE">Germany</asp:ListItem>
					<asp:ListItem Value="GH">Ghana</asp:ListItem>
					<asp:ListItem Value="GI">Gibraltar</asp:ListItem>
					<asp:ListItem Value="GR">Greece</asp:ListItem>
					<asp:ListItem Value="GL">Greenland</asp:ListItem>
					<asp:ListItem Value="GD">Grenada</asp:ListItem>
					<asp:ListItem Value="GP">Guadeloupe</asp:ListItem>
					<asp:ListItem Value="GT">Guatemala</asp:ListItem>
					<asp:ListItem Value="GU">Guam</asp:ListItem>
					<asp:ListItem Value="GN">Guinea</asp:ListItem>
					<asp:ListItem Value="GW">Guinea-Bissau</asp:ListItem>
					<asp:ListItem Value="GY">Guyana</asp:ListItem>
					<asp:ListItem Value="HT">Haiti</asp:ListItem>
					<asp:ListItem Value="HM">Heard and Mc Donald Islands</asp:ListItem>
					<asp:ListItem Value="SH">St. Helena</asp:ListItem>
					<asp:ListItem Value="HN">Honduras</asp:ListItem>
					<asp:ListItem Value="HK">Hong Kong</asp:ListItem>
					<asp:ListItem Value="VA">Holy See (Vatican City State)</asp:ListItem>
					<asp:ListItem Value="HU">Hungary</asp:ListItem>
					<asp:ListItem Value="IS">Iceland</asp:ListItem>
					<asp:ListItem Value="IN">India</asp:ListItem>
					<asp:ListItem Value="ID">Indonesia</asp:ListItem>
					<asp:ListItem Value="IR">Iran</asp:ListItem>
					<asp:ListItem Value="IQ">Iraq</asp:ListItem>
					<asp:ListItem Value="IE">Ireland</asp:ListItem>
					<asp:ListItem Value="IL">Israel</asp:ListItem>
					<asp:ListItem Value="IT">Italy</asp:ListItem>
					<asp:ListItem Value="JM">Jamaica</asp:ListItem>
					<asp:ListItem Value="JP">Japan</asp:ListItem>
					<asp:ListItem Value="JO">Jordan</asp:ListItem>
					<asp:ListItem Value="KE">Kenya</asp:ListItem>
					<asp:ListItem Value="KI">Kiribati</asp:ListItem>
					<asp:ListItem Value="KG">Kyrgyzstan</asp:ListItem>
					<asp:ListItem Value="KW">Kuwait</asp:ListItem>
					<asp:ListItem Value="KZ">Kazakhstan</asp:ListItem>
					<asp:ListItem Value="LA">Lao People's Democratic Republic</asp:ListItem>
					<asp:ListItem Value="LV">Latvia</asp:ListItem>
					<asp:ListItem Value="LB">Lebanon</asp:ListItem>
					<asp:ListItem Value="LS">Lesotho</asp:ListItem>
					<asp:ListItem Value="LR">Liberia</asp:ListItem>
					<asp:ListItem Value="LY">Libyan Arab Jamahiriya</asp:ListItem>
					<asp:ListItem Value="LI">Liechtenstein</asp:ListItem>
					<asp:ListItem Value="LT">Lithuania</asp:ListItem>
					<asp:ListItem Value="LU">Luxembourg</asp:ListItem>
					<asp:ListItem Value="MO">Macau</asp:ListItem>
					<asp:ListItem Value="MK">Macedonia</asp:ListItem>
					<asp:ListItem Value="MG">Madagascar</asp:ListItem>
					<asp:ListItem Value="MW">Malawi</asp:ListItem>
					<asp:ListItem Value="MY">Malaysia</asp:ListItem>
					<asp:ListItem Value="MV">Maldives</asp:ListItem>
					<asp:ListItem Value="ML">Mali</asp:ListItem>
					<asp:ListItem Value="MT">Malta</asp:ListItem>
					<asp:ListItem Value="MH">Marshall Islands</asp:ListItem>
					<asp:ListItem Value="MQ">Martinique</asp:ListItem>
					<asp:ListItem Value="MR">Mauritania</asp:ListItem>
					<asp:ListItem Value="MU">Mauritius</asp:ListItem>
					<asp:ListItem Value="YT">Mayotte</asp:ListItem>
					<asp:ListItem Value="MX">Mexico</asp:ListItem>
					<asp:ListItem Value="FM">Micronesia, Federated States of</asp:ListItem>
					<asp:ListItem Value="MD">Moldova</asp:ListItem>
					<asp:ListItem Value="MC">Monaco</asp:ListItem>
					<asp:ListItem Value="MN">Mongolia</asp:ListItem>
					<asp:ListItem Value="MS">Montserrat</asp:ListItem>
					<asp:ListItem Value="MA">Morocco</asp:ListItem>
					<asp:ListItem Value="MZ">Mozambique</asp:ListItem>
					<asp:ListItem Value="MM">Myanmar</asp:ListItem>
					<asp:ListItem Value="NA">Namibia</asp:ListItem>
					<asp:ListItem Value="NR">Nauru</asp:ListItem>
					<asp:ListItem Value="NP">Nepal</asp:ListItem>
					<asp:ListItem Value="NL">Netherlands</asp:ListItem>
					<asp:ListItem Value="AN">Netherlands Antilles</asp:ListItem>
					<asp:ListItem Value="NC">New Caledonia</asp:ListItem>
					<asp:ListItem Value="NZ">New Zealand</asp:ListItem>
					<asp:ListItem Value="NI">Nicaragua</asp:ListItem>
					<asp:ListItem Value="NE">Niger</asp:ListItem>
					<asp:ListItem Value="NG">Nigeria</asp:ListItem>
					<asp:ListItem Value="NU">Niue</asp:ListItem>
					<asp:ListItem Value="NF">Norfolk Island</asp:ListItem>
					<asp:ListItem Value="MP">Northern Mariana Islands</asp:ListItem>
					<asp:ListItem Value="NO">Norway</asp:ListItem>
					<asp:ListItem Value="OM">Oman</asp:ListItem>
					<asp:ListItem Value="PK">Pakistan</asp:ListItem>
					<asp:ListItem Value="PW">Palau</asp:ListItem>
					<asp:ListItem Value="PA">Panama</asp:ListItem>
					<asp:ListItem Value="PG">Papua New Guinea</asp:ListItem>
					<asp:ListItem Value="PY">Paraguay</asp:ListItem>
					<asp:ListItem Value="PE">Peru</asp:ListItem>
					<asp:ListItem Value="PH">Philippines</asp:ListItem>
					<asp:ListItem Value="PM">St. Pierre and Miquelon</asp:ListItem>
					<asp:ListItem Value="PN">Pitcairn</asp:ListItem>
					<asp:ListItem Value="PL">Poland</asp:ListItem>
					<asp:ListItem Value="PT">Portugal</asp:ListItem>
					<asp:ListItem Value="PR">Puerto Rico</asp:ListItem>
					<asp:ListItem Value="QA">Qatar</asp:ListItem>
					<asp:ListItem Value="KR">Republic of Korea</asp:ListItem>
					<asp:ListItem Value="RE">Reunion</asp:ListItem>
					<asp:ListItem Value="RO">Romania</asp:ListItem>
					<asp:ListItem Value="RU">Russian Federation</asp:ListItem>
					<asp:ListItem Value="RW">Rwanda</asp:ListItem>
					<asp:ListItem Value="KN">Saint Kitts and Nevis</asp:ListItem>
					<asp:ListItem Value="LC">Saint Lucia</asp:ListItem>
					<asp:ListItem Value="VC">Saint Vincent and The Grenadines</asp:ListItem>
					<asp:ListItem Value="WS">Samoa</asp:ListItem>
					<asp:ListItem Value="SM">San Marino</asp:ListItem>
					<asp:ListItem Value="ST">Sao Tome and Principe</asp:ListItem>
					<asp:ListItem Value="SA">Saudi Arabia</asp:ListItem>
					<asp:ListItem Value="SN">Senegal</asp:ListItem>
					<asp:ListItem Value="SC">Seychelles</asp:ListItem>
					<asp:ListItem Value="SL">Sierra Leone</asp:ListItem>
					<asp:ListItem Value="SG">Singapore</asp:ListItem>
					<asp:ListItem Value="SK">Slovakia (Slovak Republic)</asp:ListItem>
					<asp:ListItem Value="SI">Slovenia</asp:ListItem>
					<asp:ListItem Value="SB">Solomon Islands</asp:ListItem>
					<asp:ListItem Value="SO">Somalia</asp:ListItem>
					<asp:ListItem Value="ZA">South Africa</asp:ListItem>
					<asp:ListItem Value="GS">South Georgia and the South Sandwich Islands</asp:ListItem>
					<asp:ListItem Value="ES">Spain</asp:ListItem>
					<asp:ListItem Value="LK">Sri Lanka</asp:ListItem>
					<asp:ListItem Value="SD">Sudan</asp:ListItem>
					<asp:ListItem Value="SR">Suriname</asp:ListItem>
					<asp:ListItem Value="SJ">Svalbard and Jan Mayen Islands</asp:ListItem>
					<asp:ListItem Value="SZ">Swaziland</asp:ListItem>
					<asp:ListItem Value="SE">Sweden</asp:ListItem>
					<asp:ListItem Value="CH">Switzerland</asp:ListItem>
					<asp:ListItem Value="SY">Syrian Arab Republic</asp:ListItem>
					<asp:ListItem Value="TW">Taiwan, Province of China</asp:ListItem>
					<asp:ListItem Value="TJ">Tajikistan</asp:ListItem>
					<asp:ListItem Value="TZ">Tanzania</asp:ListItem>
					<asp:ListItem Value="TH">Thailand</asp:ListItem>
					<asp:ListItem Value="TG">Togo</asp:ListItem>
					<asp:ListItem Value="TK">Tokelau</asp:ListItem>
					<asp:ListItem Value="TO">Tonga</asp:ListItem>
					<asp:ListItem Value="TT">Trinidad and Tobago</asp:ListItem>
					<asp:ListItem Value="TN">Tunisia</asp:ListItem>
					<asp:ListItem Value="TR">Turkey</asp:ListItem>
					<asp:ListItem Value="TM">Turkmenistan</asp:ListItem>
					<asp:ListItem Value="TC">Turks and Caicos Islands</asp:ListItem>
					<asp:ListItem Value="TV">Tuvalu</asp:ListItem>
					<asp:ListItem Value="UG">Uganda</asp:ListItem>
					<asp:ListItem Value="UA">Ukraine</asp:ListItem>
					<asp:ListItem Value="AE">United Arab Emirates</asp:ListItem>
					<asp:ListItem Value="GB">United Kingdom</asp:ListItem>
					<asp:ListItem Value="UY">Uruguay</asp:ListItem>
					<asp:ListItem Value="UZ">Uzbekistan</asp:ListItem>
					<asp:ListItem Value="VU">Vanuatu</asp:ListItem>
					<asp:ListItem Value="VE">Venezuela</asp:ListItem>
					<asp:ListItem Value="VN">Viet Nam</asp:ListItem>
					<asp:ListItem Value="VG">Virgin Islands (British)</asp:ListItem>
					<asp:ListItem Value="VI">Virgin Islands (US)</asp:ListItem>
					<asp:ListItem Value="WF">Wallis and Futuna Islands</asp:ListItem>
					<asp:ListItem Value="EH">Western Sahara</asp:ListItem>
					<asp:ListItem Value="YE">Yemen</asp:ListItem>
					<asp:ListItem Value="YU">Yugoslavia</asp:ListItem>
					<asp:ListItem Value="ZM">Zambia</asp:ListItem>
					<asp:ListItem Value="ZW">Zimbabwe</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="SubmitButton" runat="server" Height="24px" Width="112px" Text="Submit" OnClick="SubmitButton_Click" />
            </td>
            <td>
                <asp:Button ID="CancelButton" runat="server" Height="24px" Width="112px" Text="Cancel"
                    CausesValidation="False" OnClick="CancelButton_Click" />
            </td>
        </tr>
    </table>
    <br />
    <div align="center">
        <b>Sending a check? Mail to</b><br />
        <%= CommonFunctions.GetSiteName () %><br />
        Suite G 284, 5805 State Bridge Road<br />
        Johns Creek, GA 30097<br />
	770-687-6889<br />
    </div>
</div>
        </div>
    </div>

    <script src="/Assets/js/makepayment.js"></script>
</asp:Content>
