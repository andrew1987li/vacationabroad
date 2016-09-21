<%@ Page Language="C#" MasterPageFile="~/userowner/MasterPage.master" AutoEventWireup="true" CodeFile="Listings.aspx.cs" Inherits="userowner_Listing" %>

<asp:Content ID="cont_listing" runat="server" ContentPlaceHolderID="bodycontent">
    <div class="containter">
        <div class="row">
            <div class ="col-sm-2 col-sm-offset-5 listingpadding">
                <div class ="row">
                    <strong>
                    <a href='<%= CommonFunctions.PrepareURL ("OwnerInformation.aspx?UserID=" + userid.ToString ()) %>'>
							Contact Details
					</a>
                    </strong>
                </div>
                <div class ="row">
                    <strong>
						<a href='<%= CommonFunctions.PrepareURL ("AccountInformation.aspx?UserID=" + userid.ToString ()) %>'>
							Email / Password
						</a>
					</strong>
                </div>
                <div class ="row">
					<strong>
						<a href='<%= CommonFunctions.PrepareURL ("ViewInvoices.aspx?UserID=" + userid.ToString ()) %>'>
							View Invoices
						</a>
					</strong>
                </div>
            </div>
            
        </div>
        <div class="row textcenter">
            Welcome <%=(userinfo.firstname+ " "+userinfo.lastname) %> !
        </div>

        <div class="row top_formrow">
            <div id="exTab3">	
                    <ul  class="nav nav-tabs" role="tablist">
		                <li class="active lblFor">
                            <a  href="#1b" role="tab" data-toggle="tab">Property Owner</a>
		                </li>
		                <li class="lblFor">
                            <a href="#2b" role="tab" data-toggle="tab">Traveler</a>
		                </li>
	                 </ul>

                    <div class="tab-content clearfix">
			            <div class="tab-pane active tabback" id="1b">
                            <div class="row">
                           <div class="col-sm-4">
                            <div class="row textcenter">
                                 Current Request for a Quote
                            </div>
                            <div class="row tablepanel">
                                <table class="table table-striped">
                                    <thead>
                                        <tr>
                                            <th></th>
                                            <th>Date of Arrival</th>
                                            <th>Link To Response</th>
                                        </tr>
                                     </thead>
                                    <tbody>
                                        <% if (inquiry_set.Tables.Count > 0)
                                                
                                            { %>
                                        <%  int count = inquiry_set.Tables[0].Rows.Count;
                                            for (int index =0;index<count; index++  )
                                            { %>
                                              <tr>
                                                <td>Property<%=inquiry_set.Tables[0].Rows[index]["PropertyID"] %></td>
                                                <td><%=inquiry_set.Tables[0].Rows[index]["ArrivalDate"] %></td>
                                                <td>
                                                <%   int replied=0;
                                                     Int32.TryParse(inquiry_set.Tables[0].Rows[index]["IfReplied"].ToString(), out replied);
                                                    if ( replied != 1)
                                                    {%><a href="/userowner/TravelerResponse.aspx?quoteid=<%=inquiry_set.Tables[0].Rows[index]["ID"] %>">Respond</a>
                                                  <%}else { %>
                                                     Responded
                                                    <%} %>
                                                </td>
                                              </tr>
                                        <%} %>
                                        <%} %>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="row textcenter">
                                 Current Quote Submitted
                            </div>
                            <div class="row tablepanel">
                                <table class="table table-striped">
                                    <thead>
                                        <tr>
                                            <th></th>
                                            <th>Date Submitted Quote</th>
                                            <th>Link To Quote</th>
                                        </tr>
                                     </thead>
                                    <tbody>
                                       <% if (owner_response_set.Tables.Count > 0)
                                                
                                            { %>
                                        <%  int count = owner_response_set.Tables[0].Rows.Count;
                                            for (int index =0;index<count; index++  )
                                            { %>
                                              <tr>
                                                <td>Property<%=owner_response_set.Tables[0].Rows[index]["PropertyID"] %></td>
                                                <td><%=owner_response_set.Tables[0].Rows[index]["DateReplied"] %></td>
                                                <td>
                                                <%   int replied=0;
                                                     Int32.TryParse(owner_response_set.Tables[0].Rows[index]["IsQuoted"].ToString(), out replied);
                                                    if ( replied != 1)
                                                    {%><a>Quote</a> 
                                                  <%}else { %>
                                                     Quoted
                                                    <%} %>
                                                </td>
                                              </tr>
                                        <%} %>
                                        <%} %>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="row textcenter">
                                 Quote Accepted
                            </div>
                            <div class="row tablepanel">
                                <table class="table table-striped">
                                    <thead>
                                        <tr>
                                            <th></th>
                                            <th>Date of Arrival</th>
                                            <th>Link To Booking</th>
                                        </tr>
                                     </thead>
                                    <tbody>
                                      <tr>
                                        <td>John</td>
                                        <td>Doe</td>
                                        <td>john@example.com</td>
                                      </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>  
                            </div>
            
 

			            </div>
			            <div class="tab-pane tabback" id="2b">
                            <div class="row">
                          <div class="col-sm-4">
                            <div class="row textcenter">
                                 Current Request for a Quote
                            </div>
                            <div class="row tablepanel">
                                <table class="table table-striped">
                                    <thead>
                                        <tr>
                                            <th></th>
                                            <th>Date of Arrival</th>
                                            <th>Response Status</th>
                                        </tr>
                                     </thead>
                                    <tbody>
                                       <% if (traveler_inquery_set.Tables.Count > 0)
                                                
                                            { %>
                                        <%  int count = traveler_inquery_set.Tables[0].Rows.Count;
                                            for (int index =0;index<count; index++  )
                                            { %>
                                              <tr>
                                                <td>Property<%=traveler_inquery_set.Tables[0].Rows[index]["PropertyID"] %></td>
                                                <td><%=traveler_inquery_set.Tables[0].Rows[index]["ArrivalDate"] %></td>
                                                <td>
                                                <%   int replied=0;
                                                     Int32.TryParse(traveler_inquery_set.Tables[0].Rows[index]["IfReplied"].ToString(), out replied);
                                                    if ( replied != 1)
                                                    {%>Not Responded
                                                  <%}else { %>
                                                     Responded
                                                    <%} %>
                                                </td>
                                              </tr>
                                        <%} %>
                                        <%} %>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="row textcenter">
                                 Current Quote Submitted
                            </div>
                            <div class="row tablepanel">
                                <table class="table table-striped">
                                    <thead>
                                        <tr>
                                            <th></th>
                                            <th>Date Submitted Quote</th>
                                            <th>Link To Quote</th>
                                        </tr>
                                     </thead>
                                    <tbody>
                                       <% if (traveler_response_set.Tables.Count > 0)
                                                
                                            { %>
                                        <%  int count = traveler_response_set.Tables[0].Rows.Count;
                                            for (int index =0;index<count; index++  )
                                            { %>
                                              <tr>
                                                <td>Property<%=traveler_response_set.Tables[0].Rows[index]["PropertyID"] %></td>
                                                <td><%=traveler_response_set.Tables[0].Rows[index]["DateReplied"] %></td>
                                                <td>
                                                <%   int replied=0;
                                                     Int32.TryParse(traveler_response_set.Tables[0].Rows[index]["IsQuoted"].ToString(), out replied);
                                                    if ( replied != 1)
                                                    {%><a href="/userowner/QuoteResponse.aspx?respid=<%=traveler_response_set.Tables[0].Rows[index]["ID"] %>">Quote</a>
                                                  <%}else { %>
                                                     Quoted
                                                    <%} %>
                                                </td>
                                              </tr>
                                        <%} %>
                                        <%} %>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="row textcenter">
                                 Quote Accepted
                            </div>
                            <div class="row tablepanel">
                                <table class="table table-striped">
                                    <thead>
                                        <tr>
                                            <th></th>
                                            <th>Date of Arrival</th>
                                            <th>Link To Booking</th>
                                        </tr>
                                     </thead>
                                    <tbody>
                                      <tr>
                                        <td>John</td>
                                        <td>Doe</td>
                                        <td>johnss@example.com</td>
                                      </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>    
                            </div>
           
			            </div>
                  </div>

            </div>
        </div>

        <div class="row top_formrow">
            <div class="row textcenter">
                MY PROPERTIES
            </div>
            <form id="listbuttons" runat="server">
                <div class="row text-center">
                    <asp:Button ID="ListProperty" CssClass="formcontrolmargin btn btn-primary" runat="server" Text="ListProperty" OnClick="ListProperty_Click" /><asp:Button ID="ListTour"  OnClick="ListTour_Click" CssClass="formcontrolmargin btn btn-primary" runat="server" Text="List a Tour" />
                </div>
                <div class="row text-center">
                    <asp:Button ID="OurCommission"  CssClass="formcontrolmargin btn btn-primary" OnClick="OurCommision_Click"  runat="server" Text="OurCommission %" />
                </div>

                <div class="row formcontrolmargin">
                    <table class="table formtable">
                        <thead>
                            <tr>
                                <th>Number</th>
                                <th>Name</th>
                                <th></th>
                            </tr>
                            </thead>
                        <tbody>
                            <% if (property_set.Tables.Count > 0)
                                {
                                    int count = property_set.Tables[0].Rows.Count;
                                    int prop_id = 0;
                                    for (int index = 0; index < count; index++)
                                    {
                                        prop_id = Convert.ToInt32( property_set.Tables[0].Rows[index]["ID"]);
                                     %>
                            <tr>
                            <td><a href="/ViewProperty.aspx"> Property<%=prop_id %></a></td>
                            <td><% =property_set.Tables[0].Rows[index]["Name"] %></td>
                            <td>
                                <span class="pull-right">
                                    <asp:Button ID="bt_payment" OnCommand="bt_payment_Command" CssClass="formcommadbt btn btn-primary" runat="server" Text="Payment" CommandArgument="<%=prop_id %>" />
                                    <asp:Button ID="bt_edittxt" OnCommand="bt_edittxt_Command" CssClass="formcommadbt btn btn-primary" runat="server" Text="EditText" CommandArgument="%=prop_id %>"/>
                                    <asp:Button ID="bt_editphoto" OnCommand="bt_editphoto_Command" CssClass="formcommadbt btn btn-primary" runat="server" Text="Edit Photo" CommandArgument="%=prop_id %>" />
                                    <asp:Button ID="bt_calendar" OnCommand="bt_calendar_Command" CssClass="formcommadbt btn btn-primary" runat="server" Text="Calendar" CommandArgument="%=prop_id %>" />
                                </span>
                            </td>
                            </tr>
                            <%}
                               }%>
                        </tbody>
                    </table>

                </div>
            </form>



        </div>
         
    </div>
</asp:Content>