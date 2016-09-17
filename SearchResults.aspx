<%@ Page Language="C#" MasterPageFile="~/MasterPageNoCss.master" AutoEventWireup="true" CodeFile="SearchResults.aspx.cs" Inherits="SearchResults" Title="Vacations-Abroad.com Vacation Rentals Holiday Rentals" %>
<asp:Content ID="Content" ContentPlaceHolderID="Content" Runat="Server">
    <table cellspacing="1" cellpadding="0" width="100%" align="center" border="0">
        <tr>
            <td valign="top" width="25%" bgcolor="#333366">
                <font color="#ffffff">
                    <div align="center">
                        <strong>
                            <asp:Label ID="Label2" runat="server" Height="8px">Refine Your Search</asp:Label>
                        </strong>
                    </div>
                    <div align="center">
                        <strong>
                            <asp:Label ID="SearchedLocationLabel" Visible="False" runat="server">Searched Location:</asp:Label>
                            <br />
                            <asp:Label ID="SearchedLocation" Visible="False" runat="server"></asp:Label>
                            <br />
                            <asp:Label ID="SearchedAttractionsLabel" Visible="False" runat="server">Searched Attractions:</asp:Label>
                            <br />
                            <asp:Label ID="SearchedAttractions" Visible="False" runat="server"></asp:Label>
                        </strong>
                    </div>
                    <div align="center">
                        <strong>
                            <asp:Label ID="Label1" runat="server">Location:</asp:Label>
                        </strong>
                    </div>
                    <br />
                    <div align="center">
                        <asp:DropDownList ID="Region" runat="server" Width="100px" Height="24px" DataTextFormatString="{0}"
                            DataValueField="ID" DataTextField="Region" DataMember="Regions" DataSource="<%# RegionsSet %>"
                            AutoPostBack="True" OnSelectedIndexChanged="Region_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                    <br />
                    <div align="center">
                        <asp:DropDownList ID="Country" runat="server" Width="100px" Height="24px" DataTextFormatString="{0}"
                            DataValueField="ID" DataTextField="Country" DataMember="Countries" DataSource="<%# CountriesSet %>"
                            AutoPostBack="True" OnSelectedIndexChanged="Country_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                    <br />
                    <div align="center">
                        <asp:DropDownList ID="StateProvince" runat="server" Width="100px" Height="24px" DataTextFormatString="{0}"
                            DataValueField="ID" DataTextField="StateProvince" DataMember="StateProvinces"
                            DataSource="<%# StateProvincesSet %>" AutoPostBack="True" OnSelectedIndexChanged="StateProvince_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                    <br />
                    <div align="center">
                        <asp:DropDownList ID="City" runat="server" Width="100px" Height="24px" DataTextFormatString="{0}"
                            DataValueField="ID" DataTextField="City" DataMember="Cities" DataSource="<%# CitiesSet %>">
                        </asp:DropDownList>
                    </div>
                    <br />
                    <div align="center">
                        <asp:Button ID="FindLocation" runat="server" Width="70px" Text="Find" OnClick="FindLocation_Click">
                        </asp:Button>
                    </div>
                    <br />
                </font>
            </td>
            <td width="75%" valign="top">
                <asp:Repeater ID="Repeater1" runat="server" DataMember="Properties" DataSource="<%# PropertiesFullSet %>">
                    <HeaderTemplate>
                        <table border="0" width="100%" cellpadding="0" cellspacing="1"
                            bgcolor="#333366" align="center">
                            <tr bgcolor="#333366">
                                <td>
                                    <font color="#ffffff">Name </font>
                                </td>
                                <td>
                                    <font color="#ffffff">City </font>
                                </td>
                                <td>
                                    <font color="#ffffff">State </font>
                                </td>
                                <td>
                                    <font color="#ffffff">Country </font>
                                </td>
                                <td>
                                    <font color="#ffffff">Type </font>
                                </td>
                                <td>
                                    <font color="#ffffff">Bd </font>
                                </td>
                                <td>
                                    <font color="#ffffff">Ba </font>
                                </td>
                                <td>
                                    <font color="#ffffff">Sleeps </font>
                                </td>
                                <td>
                                    <font color="#ffffff">Smoking </font>
                                </td>
                                <td>
                                    <font color="#ffffff">Pets </font>
                                </td>
                                <td>
                                    <font color="#ffffff">Min Rental </font>
                                </td>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr bgcolor="#ffffff">
                            <td bgcolor="#ffffff">
                            	<a href='<%# CommonFunctions.PrepareURL (DataBinder.Eval(Container.DataItem, "Country", "{0}").Replace (" ", "_").ToLower () + "/" + DataBinder.Eval(Container.DataItem, "StateProvince", "{0}").Replace (" ", "_").ToLower () + "/" + DataBinder.Eval(Container.DataItem, "City", "{0}").Replace (" ", "_").ToLower () + "/" + DataBinder.Eval(Container.DataItem, "ID", "{0:d}") + "/default.aspx") %>'>
                                    <%# DataBinder.Eval(Container.DataItem, "Name", "{0}") %>
                                </a>
                            </td>
                            <td>
                                <%# DataBinder.Eval(Container.DataItem, "City", "{0}") %>
                            </td>
                            <td>
                                <%# DataBinder.Eval(Container.DataItem, "StateProvince", "{0}") %>
                            </td>
                            <td>
                                <%# DataBinder.Eval(Container.DataItem, "Country", "{0}") %>
                            </td>
                            <td>
                                <%# DataBinder.Eval(Container.DataItem, "Type", "{0}") %>
                            </td>
                            <td>
                                <%# DataBinder.Eval(Container.DataItem, "NumBedrooms", "{0}") %>
                            </td>
                            <td>
                                <%# DataBinder.Eval(Container.DataItem, "NumBaths", "{0}") %>
                            </td>
                            <td>
                                <%# DataBinder.Eval(Container.DataItem, "NumSleeps", "{0}") %>
                            </td>
                            <td>
                                <%# DataBinder.Eval(Container.DataItem, "Smoking", "{0}") %>
                            </td>
                            <td>
                                <%# DataBinder.Eval(Container.DataItem, "PetFriendly", "{0}") %>
                            </td>
                            <td>
                                <%# DataBinder.Eval(Container.DataItem, "MinimumNightlyRental", "{0}") %>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </td>
        </tr>
    </table>
    
   
</asp:Content>
