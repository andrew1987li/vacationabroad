<%@ Page Title="Newsletter Archives" Language="C#" MasterPageFile="~/MasterPageNoCss.master"
    AutoEventWireup="true" CodeFile="NewsletterArchives.aspx.cs" Inherits="NewsletterArchives" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <center>
    <div style="padding-left: 5px; padding-right: 5px;">
        <div align="left">
            <asp:HyperLink ID="hlkReturn" runat="server" NavigateUrl="NewsletterArchives.aspx">Return to Newsletters</asp:HyperLink>
        </div>
        <div align="center" style="width: 85%; background-color: #999999; color: White; height: 20px;
            font-size: 17px;">
            <strong>Our Newsletter </strong>
        </div>
       
        <div id="divGrid" runat="server" style="width: 30%; border: solid 2px #999999; 
            padding: 3px;">
            <asp:GridView ID="grdNewsletter" BorderStyle="None" runat="server" AutoGenerateColumns="False"
                DataMember="id" GridLines="None">
                <Columns>
                    <asp:BoundField DataField="dateDep" DataFormatString="{0:MM/dd/yyyy}" 
                        ShowHeader="False" />
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                        <div align="left">
                         <b>   <asp:HyperLink ID="hlkItem" runat="server" NavigateUrl='<%# CommonFunctions.PrepareURL ("NewsletterArchives.aspx?item=" + ((string)DataBinder.Eval(Container.DataItem, "id", "{0}"))) %>'
                                Text='<%# Bind("title") %>'></asp:HyperLink></b>
                              </div>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("title") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <div id="divContent" style="padding-right: 5px; padding-left: 5px; text-align:left;" runat="server">
        </div>
        
        <div align="center"><b>
            <a href="Unsubscribe.aspx">Subscribe to our newsletter</a>&nbsp;&nbsp;&nbsp;&nbsp;<a
                href="Unsubscribe.aspx">Unsubscribe from our newsletter</a></div>
        <asp:Label ID="lblInfo" runat="server" ForeColor="Red"></asp:Label>
    </b></div>
    <br />
    <br />
    </center>
</asp:Content>
