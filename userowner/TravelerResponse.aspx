<%@ Page Language="C#"  MasterPageFile="~/userowner/MasterPage.master" AutoEventWireup="true" CodeFile="TravelerResponse.aspx.cs" Inherits="userowner_TravelerResponse" %>

<asp:Content ID="content" ContentPlaceHolderID="bodycontent" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-sm-offset-3 col-sm-6">
                <form id="resp_form" class="formborder" runat="server">
                    <div class="row">
                        <div id="headertitle" class="col-sm-6">Property #xxxx in City, State</div>
                        <div id="headerlast" class="col-sm-6"><span class="pull-right">Owner Response</span></div>
                    </div>

                    <div class="row">
                        <div class="col-sm-4">
                          <div class="form-group">
                            <label for="arrivaldate" class="normaltxt">Arrival Date</label>
                            <label id="arrivaldate" class="normalval">Month Day, Year </label>
                          </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-sm-4">
                          <div class="form-group">
                            <label for="nights" class="normaltxt"># of Nights Requested</label>
                            <label id="nights" class="normalval"># of nights (taken from inquiry)</label>
                          </div>
                        </div>
                        <div class="col-sm-4">
                          <div class="form-group">
                            <label for="rates" class="normaltxt">Price Quote Nightly Rates</label>
                              <asp:TextBox ID="rates" ClientIDMode="Static" CssClass="normalval" runat="server"></asp:TextBox>
                          </div>
                        </div>
                        <div class="col-sm-4">
                          <div class="form-group">
                            <label for="currency" class="normaltxt">Select Currency </label>
                             <asp:DropDownList ID="currency" runat="server" CssClass="form-control" ClientIDMode="Static">
                                 <asp:ListItem Text="dollar" Value="0">
                                  </asp:ListItem>
                                 <asp:ListItem Text="euro" Value="1">
                                  </asp:ListItem>
                             </asp:DropDownList>
                          </div>
                        </div>
                    </div>

                    <div class="row top_formrow">
                        <div class="col-sm-4 col-sm-offset-4">
                            <label class="normaltxt">Total Due to Reserve</label>
                        </div>
                        <div class="col-sm-4">
                            <label class="normalval"> Sum of Above</label>
                        </div>
                    </div>

                    <div class="row top_formrow">
                        <div class="col-sm-4">
                            <label class="normaltxt">Cleaning Fee</label>
                        </div>
                        <div class="col-sm-4 col-sm-offset-4">
                            <asp:TextBox ID="cleaningfee" runat="server" ClientIDMode="Static"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-sm-4">
                            <label class="normaltxt">Security Deposit</label>
                        </div>
                        <div class="col-sm-4 col-sm-offset-4">
                            <asp:TextBox ID="secdeposit" runat="server" ClientIDMode="Static"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-sm-4">
                            <label class="normaltxt">Lodging Tax</label>
                        </div>
                        <div class="col-sm-4">
                            <asp:TextBox ID="loadingtax" runat="server" ClientIDMode="Static"></asp:TextBox>
                        </div>
                        <div class="col-sm-4">
                            <label class="normaltxt">(Rate % x Price Quote)</label>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-sm-4 col-sm-offset-4">
                            <label class="normaltxt">Balance Due Upon Arrival</label>
                        </div>
                        <div class="col-sm-4">
                            <asp:TextBox CssClass="normalval" ID="balance" runat="server" ClientIDMode="Static" ReadOnly="true">Cleaning Fee + Sec Deposit + Lodging Tax</asp:TextBox>
                        </div>
                    </div>

                    <div class="row top_formrow">
                        <label class="normaltxt">Cancellation policy</label>
                    </div>

                    <div class="row">
                        <div class="col-sm-3"><label class="normaltxt">90 days prior to arrival</label></div>
                        <div class="col-sm-3"> <asp:TextBox ID="cancel90" CssClass="normalval" ClientIDMode="Static" runat="server"></asp:TextBox></div>
                    </div>
                    <div class="row">
                        <div class="col-sm-3"><label class="normaltxt">60 days prior to arrival</label></div>
                        <div class="col-sm-3"> <asp:TextBox ID="cancel60" CssClass="normalval" ClientIDMode="Static" runat="server"></asp:TextBox></div>
                    </div>
                    <div class="row">
                        <div class="col-sm-3"><label class="normaltxt">30 days prior to arrival</label></div>
                        <div class="col-sm-3"> <asp:TextBox ID="cancel30" CssClass="normalval" ClientIDMode="Static" runat="server"></asp:TextBox></div>
                    </div>

                    <%
                        DateTime cur = DateTime.Now;
                         %>

                    <div class="row top_formrow">
                        This offer is valid for <input type="text"  id="validnumber" name="validnumber" runat="server" class="normalval" /> days from <%=cur.ToString("MM/dd/yyyy") %>.<br />
                        30 days prior to renter’s arrival; the funds are transferred to the property owner.
                    </div>

                    <asp:Button ID="SendQuote" CssClass="btn btn-primary" runat="server" Text="Send Quote to Traveler" />
                </form>
            </div>
        </div>
    </div>
</asp:Content>
