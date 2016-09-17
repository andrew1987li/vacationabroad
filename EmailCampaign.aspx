<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageNoCss.master" AutoEventWireup="true" CodeFile="EmailCampaign.aspx.cs" Inherits="EmailCampaign" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
   <center>
        Email Inquiries<br /><br />
         <table bgcolor="#e4e4af" cellspacing="0" cellpadding="0" width="300" align="center"
        border="2">
        <tr>
            <td>
                <div align="center">
                    <strong>
                        <asp:HyperLink ID="lnkReturn" runat="server" NavigateUrl="Administration.aspx">
									    Return to Administration
                        </asp:HyperLink>
                    </strong>
                </div>
            </td>
        </tr>
    </table>
        <%--cycle through all dates in email campaign table and add months to row columns--%>
            <asp:GridView ID="grdSummary" Width="85%" runat="server" 
            AutoGenerateColumns="False" ondatabound="grdSummary_DataBound">            
                <Columns>
                    <asp:TemplateField HeaderText="Year">
                        <ItemTemplate>                                                 
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("year") %>' CssClass="campaignRow"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("year") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Jan">
                        <ItemTemplate>
                        <a href="<%# CommonFunctions.PrepareURL ("CampaignDetails.aspx?year=" + DataBinder.Eval(Container.DataItem, "year", "{0}") + "&month=" + DataBinder.Eval(Container.DataItem, "jan", "{0}")) %>">
                           <%# DataBinder.Eval(Container.DataItem, "jan", "{0}")%></a>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("jan") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Feb">
                        <ItemTemplate>
                        <a href="<%# CommonFunctions.PrepareURL ("CampaignDetails.aspx?year=" + DataBinder.Eval(Container.DataItem, "year", "{0}") + "&month=" + DataBinder.Eval(Container.DataItem, "feb", "{0}")) %>">
                           <%# DataBinder.Eval(Container.DataItem, "feb", "{0}")%></a>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("feb") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Mar">
                        <ItemTemplate>
                            <a href="<%# CommonFunctions.PrepareURL ("CampaignDetails.aspx?year=" + DataBinder.Eval(Container.DataItem, "year", "{0}") + "&month=" + DataBinder.Eval(Container.DataItem, "mar", "{0}")) %>">
                           <%# DataBinder.Eval(Container.DataItem, "mar", "{0}")%></a>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("mar") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Apr">
                        <ItemTemplate>
                             <a href="<%# CommonFunctions.PrepareURL ("CampaignDetails.aspx?year=" + DataBinder.Eval(Container.DataItem, "year", "{0}") + "&month=" + DataBinder.Eval(Container.DataItem, "apr", "{0}")) %>">
                           <%# DataBinder.Eval(Container.DataItem, "apr", "{0}")%></a>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("apr") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="May">
                        <ItemTemplate>
                           <a href="<%# CommonFunctions.PrepareURL ("CampaignDetails.aspx?year=" + DataBinder.Eval(Container.DataItem, "year", "{0}") + "&month=" + DataBinder.Eval(Container.DataItem, "may", "{0}")) %>">
                           <%# DataBinder.Eval(Container.DataItem, "may", "{0}")%></a>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("may") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Jun">
                        <ItemTemplate>
                           <a href="<%# CommonFunctions.PrepareURL ("CampaignDetails.aspx?year=" + DataBinder.Eval(Container.DataItem, "year", "{0}") + "&month=" + DataBinder.Eval(Container.DataItem, "jun", "{0}")) %>">
                           <%# DataBinder.Eval(Container.DataItem, "jun", "{0}")%></a>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox7" runat="server" Text='<%# Bind("jun") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Jul">
                        <ItemTemplate>
                            <a href="<%# CommonFunctions.PrepareURL ("CampaignDetails.aspx?year=" + DataBinder.Eval(Container.DataItem, "year", "{0}") + "&month=" + DataBinder.Eval(Container.DataItem, "jul", "{0}")) %>">
                           <%# DataBinder.Eval(Container.DataItem, "jul", "{0}")%></a>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox8" runat="server" Text='<%# Bind("jul") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Aug">
                        <ItemTemplate>
                            <a href="<%# CommonFunctions.PrepareURL ("CampaignDetails.aspx?year=" + DataBinder.Eval(Container.DataItem, "year", "{0}") + "&month=" + DataBinder.Eval(Container.DataItem, "aug", "{0}")) %>">
                           <%# DataBinder.Eval(Container.DataItem, "aug", "{0}")%></a>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox9" runat="server" Text='<%# Bind("aug") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sep">
                        <ItemTemplate>
                            <a href="<%# CommonFunctions.PrepareURL ("CampaignDetails.aspx?year=" + DataBinder.Eval(Container.DataItem, "year", "{0}") + "&month=" + DataBinder.Eval(Container.DataItem, "sep", "{0}")) %>">
                           <%# DataBinder.Eval(Container.DataItem, "sep", "{0}")%></a>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox11" runat="server" Text='<%# Bind("sep") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Oct">
                        <ItemTemplate>
                            <a href="<%# CommonFunctions.PrepareURL ("CampaignDetails.aspx?year=" + DataBinder.Eval(Container.DataItem, "year", "{0}") + "&month=" + DataBinder.Eval(Container.DataItem, "oct", "{0}")) %>">
                           <%# DataBinder.Eval(Container.DataItem, "oct", "{0}")%></a>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox10" runat="server" Text='<%# Bind("oct") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nov">
                        <ItemTemplate>
                            <a href="<%# CommonFunctions.PrepareURL ("CampaignDetails.aspx?year=" + DataBinder.Eval(Container.DataItem, "year", "{0}") + "&month=" + DataBinder.Eval(Container.DataItem, "nov", "{0}")) %>">
                           <%# DataBinder.Eval(Container.DataItem, "nov", "{0}")%></a>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox12" runat="server" Text='<%# Bind("nov") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Dec">
                        <ItemTemplate>
                            <a href="<%# CommonFunctions.PrepareURL ("CampaignDetails.aspx?year=" + DataBinder.Eval(Container.DataItem, "year", "{0}") + "&month=" + DataBinder.Eval(Container.DataItem, "dec", "{0}")) %>">
                           <%# DataBinder.Eval(Container.DataItem, "dec", "{0}")%></a>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox13" runat="server" Text='<%# Bind("dec") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <div>
                <br />
                <br />
                <h3>
                    IP Program Progress</h3>
                <asp:Label ID="lblDisplay" runat="server"></asp:Label>
            </div>
     
    <asp:Label ID="lblInfo" runat="server" ForeColor="Red"></asp:Label>
   </center>
   <br /><br />
</asp:Content>

