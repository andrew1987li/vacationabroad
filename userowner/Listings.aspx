<%@ Page Language="C#" EnableEventValidation="false" MasterPageFile="~/masterpage/NormalMaster.master" AutoEventWireup="true" CodeFile="Listings.aspx.cs" Inherits="userowner_Listing" %>
<asp:Content ID="title" runat="server" ContentPlaceHolderID="head">Listings</asp:Content>
<asp:Content ID="links" runat="server" ContentPlaceHolderID="links">
    <link href="/Assets/css/listings.css" rel="stylesheet" />
    <link href="/Assets/css/response.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="cont_listing" runat="server" ContentPlaceHolderID="bodycontent">
       <div class="row">
	<% if (BackLink.Visible) { %>
	<div class="text-center">
					<strong>
						<asp:HyperLink ID="BackLink" runat="server" NavigateUrl="OwnersList.aspx">
							Return to Owners list
						</asp:HyperLink>
					</strong>
	</div>
	<br />
	<% } %>

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

        <div class="newline top_formrow">
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
                           <div class="col-md-4 col-sm-6">
                            <div class="newline textcenter">
                                 Current Request for a Quote
                            </div>
                            <div class="tablepanel">
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
                                            {
                                                string arive_date = Convert.ToDateTime(inquiry_set.Tables[0].Rows[index]["ArrivalDate"]).ToString("yyyy-MM-dd");
                                                 %>
                                              <tr>
                                                <td>Property<%=inquiry_set.Tables[0].Rows[index]["PropertyID"] %></td>
                                                <td><%=arive_date %></td>
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
                        <div class="col-md-4 col-sm-6">
                            <div class="newline textcenter">
                                 Current Quote Submitted
                            </div>
                            <div class="tablepanel">
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
                                                    int validdays = 0;
                                                    Int32.TryParse(owner_response_set.Tables[0].Rows[index]["IsQuoted"].ToString(), out replied);
                                                    Int32.TryParse(owner_response_set.Tables[0].Rows[index]["IsValid"].ToString(), out validdays);
                                                    if (replied == 1)
                                                    {%><a>Quoted</a> 
                                                  <%}
                                                    else if (validdays == 0)
                                                    { %>
                                                     Not Valid
                                                    <%}
                                                    else
                                                    { %>
                                                    Not Reserved
                                                    <%} %>
                                                </td>
                                              </tr>
                                        <%} %>
                                        <%} %>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <div class="col-md-4 col-sm-6">
                            <div class="newline textcenter">
                                 Quote Accepted
                            </div>
                            <div class="tablepanel">
                                <table class="table table-striped">
                                    <thead>
                                        <tr>
                                            <th></th>
                                            <th>Date of Arrival</th>
                                            <th>Link To Booking</th>
                                        </tr>
                                     </thead>
                                    <tbody>
                                        <% if (owner_book_set.Tables.Count > 0)
                                                
                                            { %>
                                        <%  int count = owner_book_set.Tables[0].Rows.Count;
                                            for (int index =0;index<count; index++  )
                                            { %>
                                              <tr>
                                                <td>Property<%=owner_book_set.Tables[0].Rows[index]["propertyid"] %></td>
                                                <td><%=Convert.ToDateTime(owner_book_set.Tables[0].Rows[index]["arrivedate"]).ToString("yyyy-MM-dd") %></td>
                                                <td>
                                                <%   int replied=0;
                                                    Int32.TryParse(owner_book_set.Tables[0].Rows[index]["isconfirmed"].ToString(), out replied);
                                                   
                                                    if (replied == 1)
                                                    {%><a>Booked</a> 
                                                  <%}else {%>
                                                    
                                                     <a>Booking</a>
                                                    <%} %>
                                                </td>
                                              </tr>
                                        <%} %>
                                        <%} %>
                                    </tbody>
                                </table>
                            </div>
                        </div>  
                            </div>
                            <div class="clear"></div>
                          <div class="newline top_formrow">
                                    <div class="row textcenter">
                                        MY PROPERTIES
                                    </div>
                                    
                                        <div class="row text-center normalmargin">
                                            <asp:Button ID="Button1" CssClass="formcontrolmargin btn btn-primary" runat="server" Text="List A Property" OnClick="ListProperty_Click" /><asp:Button ID="Button2"  OnClick="ListTour_Click" CssClass="formcontrolmargin btn btn-primary" runat="server" Text="List A Tour" />
                                        </div>
                                        <div class="row text-center">
                                            <asp:Button ID="Button3"  CssClass="formcontrolmargin btn btn-primary" OnClick="OurCommision_Click"  runat="server" Text="Our Commission %" />
                                        </div>

                                        <div class="row formcontrolmargin">
                                            <table class="table formtable">
                                                <thead>
                                                    <tr>
                                                        <th>Property</th>
                                                        <th>Name</th>
                                                        <th class="btgroupcontainer"></th>
                                                    </tr>
                                                    </thead>
                                                <tbody>
                                                    <%  
                                                        if (property_set.Tables.Count > 0)
                                                        {
                                                            int count = property_set.Tables[0].Rows.Count;

                                                             %>
                                                    <asp:Repeater runat="server" id="propertylist">
                                                       <ItemTemplate>
                                                    <tr>
                                                    <td><a href="<%# CommonFunctions.PrepareURL ("ViewProperty.aspx?UserID=" + userid.ToString () + "&PropertyID=" + Eval("ID"), "*User* Listings") %>"> <%#Eval("ID") %></a></td>
                                                    <td><%#Eval("Name") %></td>
                                                    <td class="btgroupcontainer">
                                                        <div class="buttongroup">
                                                            <asp:Button ID="Button8" OnCommand="bt_delete_Command" CssClass="formcommadbt" runat="server" Text="Delete" OnClientClick="return confirm('Are you certain you want to delete this property?');" CommandArgument='<%#Eval("ID") %>'/>
                                                            <asp:Button ID="Button5" OnCommand="bt_edittxt_Command" CssClass="formcommadbt" runat="server" Text="Edit Text" CommandArgument='<%#Eval("ID") %>'/>
                                                            <asp:Button ID="Button6" OnCommand="bt_editphoto_Command" CssClass="formcommadbt" runat="server" Text="Edit Photo" CommandArgument='<%#Eval("ID") %>' />
                                                            <asp:Button ID="Button7" OnCommand="bt_calendar_Command" CssClass="formcommadbt" runat="server" Text="Calendar" CommandArgument='<%#Eval("ID") %>' />
                                                            <asp:Button ID="Button4" OnCommand="bt_payment_Command" CssClass="formcommadbt" runat="server" Text="Payment" CommandArgument='<%#Eval("ID") %>' />



                                                        </div>
                                                                 
                                                            
                                                            
                                                    </td>
                                                    </tr>
                                                           </ItemTemplate>
                                                    </asp:Repeater>
                                                    <%
                                                       }%>
                                                </tbody>
                                            </table>

                                        </div>
                                    



                                </div>
                        
            
            

			            </div>
			            <div class="tab-pane tabback" id="2b">
                            <div class="row">
                          <div class="col-md-4 col-sm-6">
                            <div class="newline textcenter">
                                 Current Request for a Quote
                            </div>
                            <div class="newline tablepanel">
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
                                            {  string arive_date = Convert.ToDateTime(traveler_inquery_set.Tables[0].Rows[index]["ArrivalDate"]).ToString("yyyy-MM-dd");
                                                %>
                                              <tr>
                                                <td>Property<%=traveler_inquery_set.Tables[0].Rows[index]["PropertyID"] %></td>
                                                <td><%=arive_date %></td>
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
                        <div class="col-md-4 col-sm-6">
                            <div class="newline textcenter">
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
                                                    int validdays = 0;
                                                    Int32.TryParse(traveler_response_set.Tables[0].Rows[index]["IsQuoted"].ToString(), out replied);
                                                    Int32.TryParse(traveler_response_set.Tables[0].Rows[index]["IsValid"].ToString(), out validdays);
                                                    if (replied != 1 && validdays > 0)
                                                    {%><a href="/userowner/QuoteResponse.aspx?respid=<%=traveler_response_set.Tables[0].Rows[index]["ID"] %>">Quote</a>
                                                  <%}
                                                    else if (replied != 1 && validdays == 0)
                                                    { %>
                                                     Not Valid
                                                    <%}
                                                        else
                                                        { %>
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
                        <div class="col-md-4 col-sm-6">
                            <div class="newline textcenter">
                                 Quote Accepted
                            </div>
                            <div class="newline tablepanel">
                                <table class="table table-striped">
                                    <thead>
                                        <tr>
                                            <th></th>
                                            <th>Date of Arrival</th>
                                            <th>State To Booking</th>
                                        </tr>
                                     </thead>
                                    <tbody>
                                      <% if (traveler_book_set.Tables.Count > 0)
                                                
                                            { %>
                                        <%  int count = traveler_book_set.Tables[0].Rows.Count;
                                            for (int index =0;index<count; index++  )
                                            { %>
                                              <tr>
                                                <td>Property<%=traveler_book_set.Tables[0].Rows[index]["propertyid"] %></td>
                                                <td><%=Convert.ToDateTime(traveler_book_set.Tables[0].Rows[index]["arrivedate"]).ToString("yyyy-MM-dd") %></td>
                                                <td>
                                                <%   int replied=0;
                                                    Int32.TryParse(traveler_book_set.Tables[0].Rows[index]["isconfirmed"].ToString(), out replied);
                                                   
                                                    if (replied == 1)
                                                    {%><a>Booked</a> 
                                                  <%}else {%>
                                                    
                                                     Not confirmed
                                                    <%} %>
                                                </td>
                                              </tr>
                                        <%} %>
                                        <%} %>
                                    </tbody>
                                </table>
                            </div>
                        </div>    
                            </div>
           
			            </div>
                  </div>

            </div>
        </div>
        </div>
 

         
    <script src="/Assets/js/listings.js"></script>
</asp:Content>