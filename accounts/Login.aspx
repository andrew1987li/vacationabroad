<%@ Page Language="C#" MasterPageFile="~/masterpage/NormalMaster.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="accounts_Login" %>

<asp:Content ID="head" ContentPlaceHolderID="head" runat="server">
    Sign in
</asp:Content>
<asp:Content ID="links" ContentPlaceHolderID="links" runat="server">
    <link href="/Assets/css/customs.css" rel="stylesheet" />
</asp:Content>

<asp:Content runat="server" ID="LoginContent" ContentPlaceHolderID="bodycontent">
   <div class="row formmargin">
    <div class="col-sm-6 col-sm-offset-3">	
        <ul  class="nav nav-tabs" role="tablist">
            <% if (logtype == 0)
                { %>
		    <li class="active lblFor">
                <a  href="#2b" role="tab" data-toggle="tab"><i class="fa fa-user" aria-hidden="true"></i> Register</a>
		    </li>
		    <li class="lblFor"><a href="#1b" role="tab" data-toggle="tab"><i class="fa fa-sign-in" aria-hidden="true"></i> Sign In</a>
		    </li>
            <%}
            else
            { %>
		    <li class="lblFor">
                <a  href="#2b" role="tab" data-toggle="tab"><i class="fa fa-user" aria-hidden="true"></i> Register</a>
		    </li>
		    <li class="active lblFor"><a href="#1b" role="tab" data-toggle="tab"><i class="fa fa-sign-in" aria-hidden="true"></i> Sign In</a>
		    </li>
            <%} %>
	     </ul>

        <div class="tab-content clearfix">
			<div class="tab-pane  <%=(logtype!=0)?"active":"" %> tabback" id="1b">
                <div class="row">
                   <div class="col-sm-offset-1 col-sm-10 ">

                    <div class="row">
                        <div class ="col-sm-12">
                             <button type="button" class="btnLogins" runat="server" onServerClick="btn_signinfacebook_Click" validationgroup="facebook"> <i class="fa fa-facebook-f socialchar"></i>  &nbsp;Login with Facebook</button>
                        </div>
                        
                    </div>
   

                    <div class="row" >
                        <div class="col-sm-12">
                            <button type="button" class="btnLogins" runat="server" onServerClick="btn_signintwitter_Click" validationgroup="twitter"> <i class="fa fa-twitter socialchar"></i>   &nbsp;Login with Twitter</button>
                        </div>
                        
                    </div>

               
                    <h3 class="formpaddingcont lblOr">
                        Or---
                    </h3>
                       

                    <div class="form-group formpaddingcont">
                        <label for="usrname" class="lblFor">Email or Username</label>
                        <asp:TextBox ID="usrname" runat="server" class="form-control"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator4" ValidationGroup="signining" runat="server" ControlToValidate="usrname" Display="Dynamic" ValidationExpression="(^[0-9a-zA-Z_]+$|^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$)" ErrorMessage="Please type username or email"></asp:RegularExpressionValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="usrname" ErrorMessage="Required" Display="Dynamic" ForeColor="Red" ValidationGroup="signining"></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group formpaddingcont">
                        <label for="pwd" class="lblFor">Password</label>
                        <asp:TextBox ID="pwd" runat="server" class="form-control" TextMode="Password"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="signining" runat="server" ForeColor="Red" Display="Dynamic" ControlToValidate="pwd" ErrorMessage="Required"></asp:RequiredFieldValidator>
                    </div>
                    <asp:Button ID="btn_signin" ValidationGroup="signining" runat="server" class="btnLogin" Text="Sign In" OnClick="btn_signin_Click" />
                </div>
                </div>
 

			</div>
			<div class="tab-pane <%=(logtype==0)?"active":"" %> tabback" id="2b">
               <div class="row">
                   <div class="col-sm-offset-1 col-sm-10 ">
                       <div class="row">
                        <div class="col-sm-12">
                                <button type="button" class="btnLogins" runat="server" validationgroup="facebookup" onserverclick="btn_signupfacebook_Click"> <i class="fa fa-facebook-f socialchar"></i>  &nbsp;Register with Facebook</button>
                        </div>
                       </div>

                        <div class="row">
                        <div class="col-sm-12" >
                            <button type="button" class="btnLogins" runat="server" validationgroup="twitterup" onserverclick="btn_signuptwitter_Click"> <i class="fa fa-twitter socialchar"></i>  &nbsp;Register with Twitter</button>
                        </div>
                        </div>




                        <h3 class="formpaddingcont lblOr">
                            Or---
                        </h3>
                        <div class ="row">
                            <div class="form-group formpaddingcont col-sm-6">
                                <label for="usrname" class="lblFor">Last Name</label>
                                <asp:TextBox ID="reg_lastname" runat="server" class="form-control"></asp:TextBox><asp:RegularExpressionValidator ID="LastNameValid" runat="server" ControlToValidate="reg_lastname" ValidationExpression="[a-zA-Z]+" display="Dynamic" ForeColor="Red" ErrorMessage="Only English letters are allowed"></asp:RegularExpressionValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="reg_lastname" runat="server" ForeColor="Red" display="Dynamic" ErrorMessage="Last Name Required"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group formpaddingcont col-sm-6">
                                <label for="usrname"  class="lblFor">First Name</label>
                                <asp:TextBox ID="reg_firstname" runat="server" class="form-control"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="reg_firstname" ForeColor="Red" Display="Dynamic"  ErrorMessage="Required"></asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Only English letters are allowed" Display="Dynamic" ControlToValidate="reg_firstname" ForeColor="Red" ValidationExpression="[a-zA-Z]+"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div class="row">
                                                        <div class="form-group formpaddingcont col-sm-9">
                                <label for="Email"  class="lblFor">Email</label>
                                <asp:TextBox ID="Email" runat="server" class="form-control"></asp:TextBox><asp:CustomValidator ID="CustomValidator1" OnServerValidate="EmailValidate" Display="Dynamic" runat="server" ForeColor="Red" ErrorMessage="The user used this email is existed" ControlToValidate="Email"></asp:CustomValidator><asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="Email" Display="Dynamic" ForeColor="Red" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorMessage="Email Format Error"></asp:RegularExpressionValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ForeColor="Red" ControlToValidate="Email" ErrorMessage="Required"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group formpaddingcont col-sm-9">
                                <label for="Password"  class="lblFor">Password</label>
                                <asp:TextBox ID="Password" runat="server" class="form-control" TextMode="Password"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator4" Display="Dynamic" ForeColor="Red" ControlToValidate="Password" runat="server" ErrorMessage="Password Required"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group formpaddingcont col-sm-9">
                                <label for="ConfirmPwd"  class="lblFor">Confirm Password</label>
                                <asp:TextBox ID="ConfirmPwd" runat="server" class="form-control" TextMode="Password"></asp:TextBox><asp:CompareValidator ID="CompareValidator1" runat="server" Display="Dynamic" ForeColor="Red" ErrorMessage="Confirm Password doesn't match" ControlToValidate="Password" ControlToCompare="ConfirmPwd"></asp:CompareValidator>
                            </div>
                            <div class="form-group formpaddingcont col-sm-9">
                                <label for="LoginName"  class="lblFor">Login Name</label>
                                <asp:TextBox ID="LoginName" runat="server" class="form-control"></asp:TextBox><asp:CustomValidator ID="CustomValidator2" OnServerValidate="UsernameValidate" runat="server" ControlToValidate="LoginName" ForeColor="Red" ErrorMessage="User Name duplicated" Display="Dynamic"></asp:CustomValidator><asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" Display="Dynamic" ForeColor="Red" ControlToValidate="LoginName" ValidationExpression="[0-9a-zA-Z_]+" ErrorMessage="Allowed letters, number, _"></asp:RegularExpressionValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ForeColor="Red" Display="Dynamic" ControlToValidate="LoginName" ErrorMessage="Required"></asp:RequiredFieldValidator>
                            </div>

                        </div>

                       <div  class="col-sm-12">
                            <asp:Button ID="bt_register" runat="server" class="btnLogin" Text="Register" OnClick="bt_register_Click" />
                       </div>
                       
                   </div>
               </div>
			</div>
      </div>

    </div>
   </div>
      






</asp:Content>
