<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="SendEmail.aspx.cs" Inherits="userowner_SendEmail" %>


<script runat="server">
    protected void Button1_Click(object sender, EventArgs e)
    {
        TextBox1.Text = "ajax";
    }
</script>


 <div style="height:400px;">
     This is the test page.... Only for naviation.....
     Menu style has to be modified...
 </div>
<form id="www" method="post" runat="server">
    <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="Button1" EventName="click" />
        </Triggers>
       <ContentTemplate>
           <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
       </ContentTemplate>
    </asp:UpdatePanel>

</form>