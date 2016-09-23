<%@ Page Language="C#"  MasterPageFile="~/userowner/MasterPage.master" AutoEventWireup="true" CodeFile="TravelerResponse.aspx.cs" Inherits="userowner_TravelerResponse" %>

<asp:Content ID="content" ContentPlaceHolderID="bodycontent" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-offset-3 col-md-6">
                <div id="resp_form" class="formborder">
                    <div class="row">
                        <div id="headertitle" class="col-md-6">Property <%=inquiryinfo.PropertyID %> in <%=countryinfo.city %>, <%=countryinfo.state %>,<%=countryinfo.country %></div>
                        <div id="headerlast" class="col-md-6"><span class="pull-right">Owner Response</span></div>
                    </div>

                    <div class="row">
                        <div class="col-md-4">
                          <div class="form-group">
                            <label for="arrivaldate" class="normaltxt">Arrival Date</label>
                            <label id="arrivaldate" class="normalval"><%=inquiryinfo.ArrivalDate %> </label>
                          </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-4">
                          <div class="form-group">
                            <label for="nights" class="normaltxt"># of Nights Requested</label>
                            <label id="nights" class="normalval"><%=inquiryinfo.Nights %></label>
                          </div>
                        </div>
                        <div class="col-md-4">
                          <div class="form-group">
                            <label for="rates" class="normaltxt">Price Quote Nightly Rates</label>
                              <asp:TextBox ID="rates" ClientIDMode="Static" CssClass="normalval" runat="server" OnTextChanged="rates_TextChanged" AutoPostBack="true"></asp:TextBox>
                              <asp:RequiredFieldValidator ID="req_rate" ControlToValidate="rates" runat="server" Display="Dynamic" ErrorMessage="Required"></asp:RequiredFieldValidator>
                              <asp:RegularExpressionValidator ID="reg_rate" runat="server" ControlToValidate="rates" Display="Dynamic" ValidationExpression="\d+(\.\d+)?" ErrorMessage="Only Number allowed"></asp:RegularExpressionValidator>
                          </div>
                        </div>
                        <div class="col-md-4">
                          <div class="form-group">
                            <label for="currency" class="normaltxt">Select Currency </label>
                             <asp:DropDownList ID="currency" runat="server" CssClass="form-control" ClientIDMode="Static">
                                 <asp:ListItem Text="dollar" Value="0" Selected="True">
                                  </asp:ListItem>
                                 <asp:ListItem Text="euro" Value="1">
                                  </asp:ListItem>
                             </asp:DropDownList>
                          </div>
                        </div>
                    </div>

                    <div class="row top_formrow">
                        <div class="col-md-4 col-md-offset-4">
                            <label class="normaltxt">Total Due to Reserve</label>
                        </div>
                        <div class="col-md-4">
                            <label class="normalval" id="totalsum" runat="server"> Sum of Above</label>
                        </div>
                    </div>

                    <div class="row top_formrow">
                        <div class="col-md-4">
                            <label class="normaltxt">Cleaning Fee</label>
                        </div>
                        <div class="col-md-4 col-md-offset-4">
                            <asp:TextBox ID="cleaningfee" runat="server" CssClass="form-control" ClientIDMode="Static"  AutoPostBack="true" OnTextChanged="rates_TextChanged"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="req_cleanfee" ControlToValidate="cleaningfee" runat="server" Display="Dynamic" ErrorMessage="Required"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="reg_cleanfee" runat="server" ControlToValidate="cleaningfee" Display="Dynamic" ValidationExpression="\d+(\.\d+)?" ErrorMessage="Only Number allowed"></asp:RegularExpressionValidator>

                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-4">
                            <label class="normaltxt">Security Deposit</label>
                        </div>
                        <div class="col-md-4 col-md-offset-4">
                            <asp:TextBox ID="secdeposit" runat="server" CssClass="form-control" ClientIDMode="Static" AutoPostBack="true" OnTextChanged="rates_TextChanged"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="req_secd" ControlToValidate="secdeposit" runat="server" Display="Dynamic" ErrorMessage="Required"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="reg_secd" runat="server" ControlToValidate="secdeposit" Display="Dynamic" ValidationExpression="\d+(\.\d+)?" ErrorMessage="Only Number allowed"></asp:RegularExpressionValidator>

                         </div>
                    </div>

                    <div class="row">
                        <div class="col-md-4">
                            <label class="normaltxt">Lodging Tax</label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="loadingtax" runat="server" CssClass="form-control" ClientIDMode="Static" AutoPostBack="true" OnTextChanged="rates_TextChanged"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="req_load" ControlToValidate="loadingtax" runat="server" Display="Dynamic" ErrorMessage="Required"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="reg_load" runat="server" ControlToValidate="loadingtax" Display="Dynamic" ValidationExpression="\d+(\.\d+)?" ErrorMessage="Only Number allowed"></asp:RegularExpressionValidator>

                        </div>
                        <div class="col-md-4">
                            <label class="normaltxt" id="loadingtaxval" runat="server">(Rate % x Price Quote)</label>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-4 col-md-offset-4">
                            <label class="normaltxt">Balance Due Upon Arrival</label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox CssClass="normalval" ID="balance" runat="server" ClientIDMode="Static" ReadOnly="true">Cleaning Fee + Sec Deposit + Lodging Tax</asp:TextBox>
                        </div>
                    </div>

                    <div class="row top_formrow">
                        <label class="normaltxt">Cancellation policy</label>
                    </div>

                    <div class="row">
                        <div class="col-md-3"><label class="normaltxt">90 days prior to arrival</label></div>
                        <div class="col-md-3"> <asp:TextBox ID="cancel90" CssClass="normalval" ClientIDMode="Static" runat="server"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="req_90" ControlToValidate="cancel90" runat="server" Display="Dynamic" ErrorMessage="Required"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="reg_90" runat="server" ControlToValidate="cancel90" Display="Dynamic" ValidationExpression="\d+(\.\d+)?" ErrorMessage="Only Number allowed"></asp:RegularExpressionValidator>

                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3"><label class="normaltxt">60 days prior to arrival</label></div>
                        <div class="col-md-3"> <asp:TextBox ID="cancel60" CssClass="normalval" ClientIDMode="Static" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="req_60" ControlToValidate="cancel60" runat="server" Display="Dynamic" ErrorMessage="Required"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="reg_60" runat="server" ControlToValidate="cancel60" Display="Dynamic" ValidationExpression="\d+(\.\d+)?" ErrorMessage="Only Number allowed"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3"><label class="normaltxt">30 days prior to arrival</label></div>
                        <div class="col-md-3"> <asp:TextBox ID="cancel30" CssClass="normalval" ClientIDMode="Static" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="req_30" ControlToValidate="cancel30" runat="server" Display="Dynamic" ErrorMessage="Required"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="reg_30" runat="server" ControlToValidate="cancel30" Display="Dynamic" ValidationExpression="\d+(\.\d+)?" ErrorMessage="Only Number allowed"></asp:RegularExpressionValidator>

                        </div>
                    </div>

                    <%
                        DateTime cur = DateTime.Now;
                         %>

                    <div class="row top_formrow">
                        This offer is valid for <input type="text"  id="validnumber" name="validnumber" runat="server" class="normalval smallwidth" /> days from <%=cur.ToString("MM/dd/yyyy") %>.<br />
                        30 days prior to renter’s arrival; the funds are transferred to the property owner.
                            <asp:RequiredFieldValidator ID="req_val" ControlToValidate="validnumber" runat="server" Display="Dynamic" ErrorMessage="Required"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="reg_val" runat="server" ControlToValidate="validnumber" Display="Dynamic" ValidationExpression="\d+" ErrorMessage="Only Number allowed"></asp:RegularExpressionValidator>

                    </div>

                    <asp:Button ID="SendQuote" CssClass="btn btn-primary" runat="server" Text="Send Quote to Traveler" OnClick="SendQuote_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
