<%@ Page Language="C#" MasterPageFile="~/userowner/MasterPage.master" AutoEventWireup="true" CodeFile="Payment.aspx.cs" Inherits="userowner_Payment" %>

<asp:Content ID="body" ContentPlaceHolderID="bodycontent" runat="server" >
    <div class="container">
        <div class="row">
            <div class="col-sm-6">
                <label>Vocation Abroad</label>
            </div>
            <div class="col-sm-6">
                <div class="row">
                <span class="pull-right"><label>Payment for reservation</label>         </span>
                 </div>
                <div class="row text-right">
                       <label>Current Date:<%=DateTime.Now.ToString("yyyy-MM-dd") %></label>
                 </div>
            </div>
        </div>

        <div class="row">
            <div class="row">
                You have agreed the following terms.
            </div>
            <div class="row">
                <div class="col-sm-6  col-sm-6 borderpane ">
                    <div class="col-sm-1">
                        To:
                    </div>
                    <div class="col-sm-5 text-left">
                        <ul>
                            <li><%=owner_info.firstname+" "+owner_info.lastname %></li>
                            <li>Email:<%=owner_info.email %></li>
                            <li>Phone Number:<%=owner_info.PrimaryTelephone %></li>
                        </ul>
                    </div>
                </div>

            </div>
             <div class="row top_formrow">
                <table class="table table-bordered">
                  <thead>
                    <tr class="tablerow">
                      <th>Property Location</th>
                      <th>Property #</th>
                      <th>Our Commission(%)</th>
                      <th>Our Commission</th>
                    </tr>
                  </thead>
                  <tbody>
                    <tr>
                      <th scope="row"><%=countryinfo.city %>,<%=countryinfo.state %>,<%=countryinfo.country %></th>
                      <td><%=inquiryinfo.PropertyID %></td>
                      <td>10</td>
                      <td>$<%=BookDBProvider.DoFormat(email_resp.Sum*Convert.ToDecimal(0.1)) %></td>
                    </tr>
                   </tbody>
                </table>
            </div>
            <div class="row">
                Arrival Date:<%=inquiryinfo.ArrivalDate %>
            </div>
            <div class="row">
                <table class="table table-bordered">
                  <thead>
                    <tr class="tablerow">
                      <th># of Nights</th>
                      <th>Description</th>
                      <th>Nightly Rate</th>
                      <th>Total Rental</th>
                    </tr>
                  </thead>
                  <tbody>
                    <tr>
                      <th scope="row"><%=inquiryinfo.Nights %></th>
                      <td><%=prop_info.name %></td>
                      <td><%=BookDBProvider.DoFormat(email_resp.NightRate) %></td>
                      <td>$<%=BookDBProvider.DoFormat(email_resp.Sum) %></td>
                    </tr>
                   </tbody>
                </table>
            </div>
            <div class="row text-right  top_formrow">
               <div class="row">
                    Amount Due to Reserve Property: $<%=BookDBProvider.DoFormat(email_resp.Sum) %>
                </div>
                <div class="row">
                    Security Deposit:$<%=BookDBProvider.DoFormat(email_resp.SecurityDeposit) %>
                </div>
                <div class="row">
                    Lodaing Tax: <input type="text" readonly="true" class="smallwidth" value="<%=BookDBProvider.DoFormat(email_resp.LoadingTaxRate) %>%" />  <input type="text" readonly="true" class="smallwidth" value="$<%=BookDBProvider.DoFormat(email_resp.LoadingTax) %>" />
                </div>
                <div class="row">
                    Cleaning Fee:$<%=BookDBProvider.DoFormat(email_resp.CleaningFee) %>
                </div>
                <div class="row">
                    Amount Due to Owner upon Arrival:$<%=BookDBProvider.DoFormat(email_resp.Balance) %>
                </div>
                <div class="row top_formrow">
                    Total Rental Price:$<%=BookDBProvider.DoFormat(email_resp.Balance+email_resp.Sum) %>
                </div>
            </div>
 
        </div>

        <div class="row">
            <div class="col-sm-4 col-sm-offset-4 listingpadding text-center">
                <div id="pay_form">
                    <div class="row">
                        <asp:Label ID="Label1" runat="server" Text="Payment Method"></asp:Label>
                       <asp:DropDownList ID="paytype" runat="server">
                           <asp:ListItem Text="PayPal" Value="0" Selected="True"></asp:ListItem>
                           <asp:ListItem Text="Credit" Value="1"></asp:ListItem>
                       </asp:DropDownList>
                    </div>
                    <div class="row">
                        <asp:Button ID="payment" OnClick="payment_Click" CssClass="btn btn-primary top_formrow" runat="server" Text="Pay To Reserve" />
                    </div>
                            <asp:HiddenField ID="resp_id" runat="server" />
                </div>
            </div>
        </div>
    

    </div>
</asp:Content>

