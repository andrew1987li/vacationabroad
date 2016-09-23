<%@ Page Language="C#"  MasterPageFile="~/userowner/MasterPage.master" AutoEventWireup="true" CodeFile="QuoteResponse.aspx.cs" Inherits="userowner_TravelerResponse" %>

<asp:Content ID="content" ContentPlaceHolderID="bodycontent" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-offset-3 col-md-6">
                <div id="resp_form" class="formborder" runat="server">
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
                              <label id="rates" class="normalval"><%=BookDBProvider.DoFormat(email_resp.NightRate) %></label>
                          </div>
                        </div>
                        <div class="col-md-4">
                          <div class="form-group">
                            <label for="currency" class="normaltxt">Select Currency </label>
                            <label id="currency" class="normalval"><%=currency_type[email_resp.CurrencyType] %></label>
                          </div>
                        </div>
                    </div>

                    <div class="row top_formrow">
                        <div class="col-md-4 col-md-offset-4">
                            <label class="normaltxt">Total Due to Reserve</label>
                        </div>
                        <div class="col-md-4">
                            <label class="normalval" id="totalsum" runat="server"> <%=BookDBProvider.DoFormat(email_resp.Sum) %></label>
                        </div>
                    </div>

                    <div class="row top_formrow">
                        <div class="col-md-4">
                            <label class="normaltxt">Cleaning Fee</label>
                        </div>
                        <div class="col-md-4 col-md-offset-4">
                           <label class="normalval" id="Label1" runat="server"> <%=BookDBProvider.DoFormat(email_resp.CleaningFee) %></label>

                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-4">
                            <label class="normaltxt">Security Deposit</label>
                        </div>
                        <div class="col-md-4 col-md-offset-4">
                          <label class="normalval" id="Label2" runat="server"> <%=BookDBProvider.DoFormat(email_resp.SecurityDeposit) %></label>
                         </div>
                    </div>

                    <div class="row">
                        <div class="col-md-4">
                            <label class="normaltxt">Lodging Tax</label>
                        </div>
                        <div class="col-md-4">
                               <label class="normalval" id="Label3" runat="server"> <%=BookDBProvider.DoFormat(email_resp.LoadingTaxRate) %>%</label>
                        </div>
                        <div class="col-md-4">
                            <label class="normaltxt" id="loadingtaxval" runat="server"><%=BookDBProvider.DoFormat(email_resp.LoadingTax) %></label>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-4 col-md-offset-4">
                            <label class="normaltxt">Balance Due Upon Arrival</label>
                        </div>
                        <div class="col-md-4">
                            <label class="normalval" id="Label4" runat="server"> <%=BookDBProvider.DoFormat(email_resp.Balance) %></label>
                        </div>
                    </div>

                    <div class="row top_formrow">
                        <label class="normaltxt">Cancellation policy</label>
                    </div>

                    <div class="row">
                        <div class="col-md-3"><label class="normaltxt">90 days prior to arrival</label></div>
                        <div class="col-md-3"> <label class="normalval" id="Label5" runat="server"> <%=BookDBProvider.DoFormat(email_resp.Cancel90) %>%</label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3"><label class="normaltxt">60 days prior to arrival</label></div>
                        <div class="col-md-3"> <label class="normalval" id="Label6" runat="server"> <%=BookDBProvider.DoFormat(email_resp.Cancel60) %>%</label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3"><label class="normaltxt">30 days prior to arrival</label></div>
                        <div class="col-md-3"> <label class="normalval" id="Label7" runat="server"> <%=BookDBProvider.DoFormat(email_resp.Cancel30) %>%</label>
                        </div>
                    </div>

                    <%
                        DateTime cur = DateTime.Now;
                         %>

                    <div class="row top_formrow">
                         This offer is valid until 5:00 pm
                        30 days prior to renter’s arrival; the funds are transferred to the property owner.
                       <div class="row text-center">
                           <input id="chk_agree" type="checkbox" runat="server" />agree to all the terms specified above. 
                           <div class="row">
                                <asp:Label ID="errormsg" runat="server" Text="" ForeColor="Red"></asp:Label>
                           </div>
                       </div>
                       <div class="row text-center top_formrow">
                            <asp:Button ID="SendQuote" CssClass="btn btn-primary" runat="server" Text="Reserve this Property" OnClick="SendQuote_Click" />
                    <asp:HiddenField ID="resp_number" runat="server" />  
                       </div>
                                                   
                    </div>


                </div>
            </div>
        </div>
    </div>
</asp:Content>
