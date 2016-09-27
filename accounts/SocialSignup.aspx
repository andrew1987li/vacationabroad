<%@ Page Language="C#" MasterPageFile="~/accounts/MasterPage.master" AutoEventWireup="true" CodeFile="SocialSignup.aspx.cs" Inherits="accounts_SocialSignup" %>

<asp:Content runat="server" ID="LoginContent" ContentPlaceHolderID="bodycontent">
       
        <div id="">	
               <div class="row">
                   <div class="col-md-offset-3 col-md-6">
                        <h3 class="formpaddingcont">
                            Register with Social Netwrok----
                        </h3>
                         <div class="row">
                            <div class="form-group formpaddingcont col-md-9">
                                <label for="Email">Email</label>
                                <asp:TextBox ID="Email" runat="server" class="form-control"></asp:TextBox><asp:CustomValidator ID="CustomValidator1" OnServerValidate="EmailValidate" Display="Dynamic" runat="server" ForeColor="Red" ErrorMessage="The user used this email is existed" ControlToValidate="Email"></asp:CustomValidator><asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="Email" Display="Dynamic" ForeColor="Red" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorMessage="Email Format Error"></asp:RegularExpressionValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ForeColor="Red" ControlToValidate="Email" ErrorMessage="Required"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group formpaddingcont col-md-9">
                                <label for="LoginName">Login Name</label>
                                <asp:TextBox ID="LoginName" runat="server" class="form-control"></asp:TextBox><asp:CustomValidator ID="CustomValidator2" OnServerValidate="UsernameValidate" runat="server" ControlToValidate="LoginName" ForeColor="Red" ErrorMessage="User Name duplicated" Display="Dynamic"></asp:CustomValidator><asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" Display="Dynamic" ForeColor="Red" ControlToValidate="LoginName" ValidationExpression="[0-9a-zA-Z_]+" ErrorMessage="Allowed letters, number, _"></asp:RegularExpressionValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ForeColor="Red" Display="Dynamic" ControlToValidate="LoginName" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                <asp:HiddenField ID="hidden_id" runat="server" />
                                <asp:HiddenField ID="acctype" runat="server" />
                            </div>

                        </div>
                       <div class ="row">
                               <div class="col-md-offset-2">
                                    <input id="showproperty" name="showproperty" type="checkbox" checked=""  runat="server"/>
                                    <label for="showproperty" class="lblCheck">
                                        I want to list a property!
                                    </label>
                                </div>
 
                       </div>

                       <div  class="row">
                            <asp:Button ID="bt_register" runat="server" class="btnLogin" Text="Register" OnClick="bt_register_Click" />
                       </div>
                       
                   </div>
               </div>
    </div>






</asp:Content>
