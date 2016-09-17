<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageNoCss.master" AutoEventWireup="true" CodeFile="RegionTextEdit.aspx.cs" Inherits="RegionTextEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
    <style type="text/css">

    #ctl00_Content_gv td input {
        width: 400px;
    }
    #ctl00_Content_gv td  {
        padding: 10px;
    }
    #ctl00_Content_gv th  {
        padding-right: 10px;
    }
    .HeaderText {
        display: block;
font-size: 1.5em;
-webkit-margin-before: 0.83em;
-webkit-margin-after: 0.83em;
-webkit-margin-start: 0px;
-webkit-margin-end: 0px;
font-weight: bold;
        padding-right: 10px;
    }
    .TASizer {
        width: 480px;
height: 114px;
    }

</style>
    <asp:ScriptManager runat="server" ID="smMain"></asp:ScriptManager>
    <asp:UpdatePanel runat="server">
     <ContentTemplate>
         <h2 style="font-weight: bold;font-size: 22px;padding-top: 10px;padding-bottom: 5px;">Edit Front Page Region Text</h2>
         <p>This is the text that shows up on the home page</p>
         <div align="right"><a href="Administration.aspx">Back to main administrative area</a></div>
        <asp:GridView ID="gv" Runat="server" Width="600"
             DataKeyNames="RegionTextID" AutoGenerateColumns="False" AllowPaging="False"  BorderWidth="1px" BackColor="White"
            CellPadding="10" BorderStyle="None" BorderColor="#3366CC" OnRowCancelingEdit="gv_RowCancelingEdit" OnRowEditing="gv_RowEditing" OnRowUpdating="gv_RowUpdating">
            <Columns>
                <asp:BoundField DataField="RegionTextID" HeaderText="ID #" ReadOnly="True" /> 
                <asp:BoundField ReadOnly="True" InsertVisible="False" HeaderText="Region" DataField="Region"></asp:BoundField>
                <%--<asp:BoundField DataField="RegionTextValue" HeaderText="Text"></asp:BoundField>--%>
                <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Value" ItemStyle-Wrap="true">
                 <ItemTemplate>
        <asp:Label ID="lbl" runat="server" Text='<%# Eval("RegionTextValue")%>'></asp:Label>
    </ItemTemplate>
    <EditItemTemplate>
        <asp:TextBox ID="txt" runat="server" Text='<%# Eval("RegionTextValue")%>' CssClass="TASizer" TextMode="MultiLine"></asp:TextBox>
    </EditItemTemplate> 
                    </asp:TemplateField>
                <asp:CommandField ShowEditButton="True"></asp:CommandField>
            </Columns>
            <SelectedRowStyle ForeColor="#CCFF99" Font-Bold="True" 
                BackColor="#009999"></SelectedRowStyle>
            <RowStyle ForeColor="#003399" BackColor="White"></RowStyle>
            <AlternatingRowStyle BackColor="#f7e6b2"></AlternatingRowStyle>
            <HeaderStyle CssClass="HeaderText"></HeaderStyle>
        </asp:GridView>

    </ContentTemplate>
         </asp:UpdatePanel>
    <br/><br/>
    <div align="right"><a href="Administration.aspx">Back to main administrative area</a></div>
</asp:Content>

