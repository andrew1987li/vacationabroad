<%@ Reference Page="~/CityList.aspx" %>
<%@ Reference Page="~/CountryList.aspx" %>
<%@ Reference Page="~/RegionList.aspx" %>
<%@ Page Language="C#" MasterPageFile="~/MasterPageNoCss.master" AutoEventWireup="true" CodeFile="Locations.aspx.cs" Inherits="Locations" Title="Locations Administration" %>
<asp:Content ID="Content" ContentPlaceHolderID="Content" Runat="Server">
    <table bgcolor="#e4e4af" cellspacing="0" cellpadding="0" width="100%" align="center" border="2">
        <tr>
            <td colspan="100" align="center">
                <strong>
                    <asp:HyperLink ID="Hyperlink7" runat="server" NavigateUrl="Administration.aspx">
						Main Administration page
                    </asp:HyperLink>
                </strong>
            </td>
        </tr>
        <tr>
            <td align="center">
                <strong>
                    <asp:HyperLink ID="Hyperlink2" runat="server" NavigateUrl="OwnersList.aspx">
						Owners
                    </asp:HyperLink>
                </strong>
            </td>
            <td align="center">
                <strong>
                    <asp:HyperLink ID="Hyperlink3" runat="server" NavigateUrl="Locations.aspx">
						Locations
                    </asp:HyperLink>
                </strong>
            </td>
            <td align="center">
                <strong>
                    <asp:HyperLink ID="Hyperlink4" runat="server" NavigateUrl="SendCustomEmail.aspx">
						Send Email
                    </asp:HyperLink>
                </strong>
            </td>
            <td align="center">
                <strong>
                    <asp:HyperLink ID="Hyperlink1" runat="server" NavigateUrl="OutstandingInvoices.aspx">
						Outstanding Invoices
                    </asp:HyperLink>
                </strong>
            </td>
            <td align="center">
                <strong>
                    <asp:HyperLink ID="Hyperlink5" runat="server" NavigateUrl="FreeTrial.aspx">
						Free Trial Properties
                    </asp:HyperLink>
                </strong>
            </td>
            <td align="center">
                <strong>
                    <asp:HyperLink ID="Hyperlink6" runat="server" NavigateUrl="PaymentsReceived.aspx">
						Payments Received
                    </asp:HyperLink>
                </strong>
            </td>
        </tr>
        <tr>
			<td colspan="100" align="center">
				<table width="100%" border="0">
					<tr>
						<td colspan="3" align="left">
							<a href='<%= CommonFunctions.PrepareURL ("CommissionPayable.aspx", "Administration") %>'>
								Commission Payable
							</a>
						</td>
						<td colspan="3" align="right">
							<a href='<%= CommonFunctions.PrepareURL ("InvoiceRegister.aspx?Year=2004", "Administration") %>'>
								Invoice Register 2004
							</a>
							&nbsp;&nbsp;
							<a href='<%= CommonFunctions.PrepareURL ("InvoiceRegister.aspx?Year=2005", "Administration") %>'>
								Invoice Register 2005
							</a>
							&nbsp;&nbsp;
							<a href='<%= CommonFunctions.PrepareURL ("InvoiceRegister.aspx?Year=2006", "Administration") %>'>
								Invoice Register 2006
							</a>
						</td>
					</tr>
				</table>
			</td>
        </tr>
    </table>
    <br />
    <table bgcolor="#e4e4af" cellspacing="0" cellpadding="0" width="300" align="center"
        border="2">
        <tr>
            <td>
                <div align="center">
                    <strong>
                        <asp:HyperLink ID="OwnerInformationLink" runat="server" NavigateUrl="OwnerInformation.aspx">
						    Update my personal information
                        </asp:HyperLink>
                    </strong>
                </div>
            </td>
        </tr>
    </table>
    <br />
    <div align="center">
        <font color="red"><strong>Warning: deleting a region will delete all countries, states/provinces
            and cities assigned to it. Deleting a country will delete all states/provinces and
            cities assigned to it. Deleting a state/province will delete all cities assigned
            to it. </strong></font>
    </div>
    <table width="100%" border="0" cellspacing="4" cellpadding="2" bordercolor="#ffffff">
        <tr>
            <td align="center">
                <strong>Regions </strong>
            </td>
            <td align="center">
                <strong>Countries </strong>
            </td>
            <td align="center">
                <strong>States / Provinces </strong>
            </td>
            <td align="center">
                <strong>Cities </strong>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:DropDownList ID="RegionList" runat="server" AutoPostBack="True" DataSource="<%# RegionsSet %>"
                    DataMember="Regions" DataTextField="Region" DataValueField="ID" DataTextFormatString="{0}"
                    OnSelectedIndexChanged="RegionList_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td align="center">
                <asp:DropDownList ID="CountryList" runat="server" AutoPostBack="True" DataSource="<%# CountriesSet %>"
                    DataMember="Countries" DataTextField="Country" DataValueField="ID" DataTextFormatString="{0}"
                    OnSelectedIndexChanged="CountryList_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td align="center">
                <asp:DropDownList ID="StateList" runat="server" AutoPostBack="True" DataSource="<%# StateProvincesSet %>"
                    DataMember="StateProvinces" DataTextField="StateProvince" DataValueField="ID"
                    DataTextFormatString="{0}" OnSelectedIndexChanged="StateList_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td align="center">
                <asp:DropDownList ID="CityList" runat="server" AutoPostBack="True" DataSource="<%# CitiesSet %>"
                    DataMember="Cities" DataTextField="City" DataValueField="ID" DataTextFormatString="{0}"
                    OnSelectedIndexChanged="CityList_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:TextBox ID="RegionName" runat="server" />
            </td>
            <td align="center">
                <asp:TextBox ID="CountryName" runat="server" />
            </td>
            <td align="center">
                <asp:TextBox ID="StateName" runat="server" />
            </td>
            <td align="center">
                <asp:TextBox ID="CityName" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Button ID="RegionRename" runat="server" Text="Rename" CausesValidation="False"
                    OnClick="RegionRename_Click" />
            </td>
            <td align="center">
                <asp:Button ID="CountryRename" runat="server" Text="Rename" CausesValidation="False"
                    OnClick="CountryRename_Click" />
            </td>
            <td align="center">
                <asp:Button ID="StateRename" runat="server" Text="Rename" CausesValidation="False"
                    OnClick="StateRename_Click" />
            </td>
            <td align="center">
                <asp:Button ID="CityRename" runat="server" Text="Rename" CausesValidation="False"
                    OnClick="CityRename_Click" />
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Button ID="RegionDelete" runat="server" Text="Delete" CausesValidation="False"
                    OnClick="RegionDelete_Click" />
            </td>
            <td align="center">
                <asp:Button ID="CountryDelete" runat="server" Text="Delete" CausesValidation="False"
                    OnClick="CountryDelete_Click" />
            </td>
            <td align="center">
                <asp:Button ID="StateDelete" runat="server" Text="Delete" CausesValidation="False"
                    OnClick="StateDelete_Click" />
            </td>
            <td align="center">
                <asp:Button ID="CityDelete" runat="server" Text="Delete" CausesValidation="False"
                    OnClick="CityDelete_Click" />
            </td>
        </tr>
        <tr>
            <td align="center">
            </td>
            <td align="center">
                <asp:DropDownList ID="RegionList2" runat="server" AutoPostBack="True" DataSource="<%# RegionsSet %>"
                    DataMember="Regions" DataTextField="Region" DataValueField="ID" DataTextFormatString="{0}"
                    OnSelectedIndexChanged="RegionList2_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td align="center">
                <asp:DropDownList ID="CountryList2" runat="server" AutoPostBack="True" DataSource="<%# CountriesSet %>"
                    DataMember="Countries" DataTextField="Country" DataValueField="ID" DataTextFormatString="{0}"
                    OnSelectedIndexChanged="CountryList2_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td align="center">
                <asp:DropDownList ID="StateList2" runat="server" AutoPostBack="True" DataSource="<%# StateProvincesSet %>"
                    DataMember="StateProvinces" DataTextField="StateProvince" DataValueField="ID"
                    DataTextFormatString="{0}" OnSelectedIndexChanged="StateList2_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:TextBox ID="NewRegion" runat="server" />
                <br />
                <asp:RequiredFieldValidator ID="EnterRegion" runat="server" ErrorMessage="Please enter region name"
                    ControlToValidate="NewRegion" Display="Dynamic" />
                <asp:RegularExpressionValidator ID="InvalidRegion" runat="server" Display="Dynamic"
                    ControlToValidate="NewRegion" ErrorMessage="Invalid region name entered" ValidationExpression="^[a-zA-Z0-9\-]([a-zA-Z0-9\- ]){1,50}[a-zA-Z0-9\-]$" />
            </td>
            <td align="center">
                <asp:TextBox ID="NewCountry" runat="server" />
                <br />
                <asp:RequiredFieldValidator ID="EnterCountry" runat="server" ErrorMessage="Please enter country name"
                    ControlToValidate="NewCountry" Display="Dynamic" />
                <asp:RegularExpressionValidator ID="InvalidCountry" runat="server" Display="Dynamic"
                    ControlToValidate="NewCountry" ErrorMessage="Invalid country name entered" ValidationExpression="^[a-zA-Z0-9\-]([a-zA-Z0-9\- ]){1,50}[a-zA-Z0-9\-]$" />
            </td>
            <td align="center">
                <asp:TextBox ID="NewState" runat="server" />
                <br />
                <asp:RequiredFieldValidator ID="EnterState" runat="server" ErrorMessage="Please enter state name"
                    ControlToValidate="NewState" Display="Dynamic" />
                <asp:RegularExpressionValidator ID="InvalidState" runat="server" Display="Dynamic"
                    ControlToValidate="NewState" ErrorMessage="Invalid state/province name entered"
                    ValidationExpression="^[a-zA-Z0-9\-]([a-zA-Z0-9\- ]){1,50}[a-zA-Z0-9\-]$" />
            </td>
            <td align="center">
                <asp:TextBox ID="NewCity" runat="server" />
                <br />
                <asp:RequiredFieldValidator ID="EnterCity" runat="server" ErrorMessage="Please enter city name"
                    ControlToValidate="NewCity" Display="Dynamic" />
                <asp:RegularExpressionValidator ID="InvalidCity" runat="server" Display="Dynamic"
                    ControlToValidate="NewCity" ErrorMessage="Invalid city name entered" ValidationExpression="^[a-zA-Z0-9\-]([a-zA-Z0-9\- ]){1,50}[a-zA-Z0-9\-]$" />
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Button ID="RegionAdd" runat="server" Text="Add" CausesValidation="False" OnClick="RegionAdd_Click">
                </asp:Button>
            </td>
            <td align="center">
                <asp:Button ID="CountryAdd" runat="server" Text="Add" CausesValidation="False" OnClick="CountryAdd_Click">
                </asp:Button>
            </td>
            <td align="center">
                <asp:Button ID="StateAdd" runat="server" Text="Add" CausesValidation="False" OnClick="StateAdd_Click">
                </asp:Button>
            </td>
            <td align="center">
                <asp:Button ID="CityAdd" runat="server" Text="Add" CausesValidation="False" OnClick="CityAdd_Click">
                </asp:Button>
            </td>
        </tr>
        <tr>
            <td align="center">
            </td>
            <td align="center">
                <asp:TextBox ID="Country_Title_OverRide" runat="server" MaxLength="200"></asp:TextBox></td>
            <td align="center">
                <asp:TextBox ID="State_Title_override" runat="server" MaxLength="200"></asp:TextBox></td>
            <td align="center">
                <asp:TextBox ID="City_Title_Override" runat="server" MaxLength="200"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="center">
            </td>
            <td align="center">
                <asp:Button ID="BtnChangeCountryTitle" runat="server" CausesValidation="False" Text="Change Country Title" OnClick="BtnChangeCountryTitle_Click" /></td>
            <td align="center"><asp:Button ID="BtnChangeStateTitle" runat="server" CausesValidation="False" Text="Change State Title" OnClick="BtnChangeStateTitle_Click" /></td>
            <td align="center">
                <asp:Button ID="BtnChangeCityTitle" runat="server" OnClick="BtnChangeCityTitle_Click" CausesValidation="False"
                    Text="Change City Title" /></td>
        </tr>
        <tr>
            <td align="center">
            </td>
            <td align="center">
                <asp:TextBox ID="Country_description_override" runat="server" MaxLength="300" TextMode="MultiLine"
                    ToolTip="Enter the meta tag descriptoin text.  Note you are limited to 254 characters"></asp:TextBox></td>
            <td align="center">
                <asp:TextBox ID="State_Description_override" runat="server" MaxLength="254" TextMode="MultiLine"
                    ToolTip="Enter the meta tag descriptoin text.  Note you are limited to 254 characters"></asp:TextBox></td>
            <td align="center">
                <asp:TextBox ID="City_Description_override" runat="server" MaxLength="254" TextMode="MultiLine"
                    ToolTip="Enter the meta tag descriptoin text.  Note you are limited to 254 characters"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="center">
            </td>
            <td align="center">
                <asp:Button ID="BtnChangeCountryDescription" runat="server" CausesValidation="False"
                    OnClick="BtnChangeCountryDescription_Click" Text="Change Description" /></td>
            <td align="center">
                <asp:Button ID="BtnChangeStateDescription" runat="server" CausesValidation="False"
                    OnClick="BtnChangeStateDescription_Click" Text="Change Description" ToolTip="Press here to change the State Decription Metatag" /></td>
            <td align="center">
                <asp:Button ID="BtnChangeCityDescription" runat="server" CausesValidation="False"
                    OnClick="BtnChangeCityDescription_Click" Text="Change Description" ToolTip="Click here to change the CITY description meta tag" /></td>
        </tr>
    </table>

    <noscript>
        <img src="http://www.watchwise.net/cgi-watchwise/monitor.cgi?<%= CommonFunctions.GetSiteAddress ().ToLower () %>:all"
            width="1" height="1">
    </noscript>

    <script language="javascript">
        document.write('<img src="http://www.watchwise.net/cgi-watchwise/monitorwise.cgi?URL=<%= CommonFunctions.GetSiteAddress ().ToLower () %>:all&LINK=',escape(document.referrer),'" height=1 width=1>')
    </script>
</asp:Content>
