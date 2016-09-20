<%@ Page Language="C#" MasterPageFile="~/userowner/MasterPage.master" AutoEventWireup="true" CodeFile="Listing.aspx.cs" Inherits="userowner_Listing" %>

<asp:Content ID="cont_listing" runat="server" ContentPlaceHolderID="bodycontent">
    <div class="containter">
        <div class="row">
            <div class ="col-sm-4 col-sm-offset-4 formborder listingpadding">
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



    </div>
</asp:Content>