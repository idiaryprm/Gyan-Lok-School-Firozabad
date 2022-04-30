<%@ Page Language="VB" UnobtrusiveValidationMode="None" MasterPageFile="~/iDiaryPanel/Parent/ParentMaster.master" AutoEventWireup="false" Inherits="iDiary_V3.Parent_ViewMessages" title="Untitled Page" Codebehind="ViewMessages.aspx.vb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Heading" runat="server">
    Messages
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <div class="col_3" style="margin-top: 20px;" id="panelEnquiryNo" runat="server">
        <div class="col-md-3 widget widget1" style="width: 100%">
            <div class="r3_counter_box">
                <table border="0" width="100%" cellpadding="1" cellspacing="1">
                    <tr>
                        <td width="100%">
                            <asp:Button ID="btnRead" runat="server" Text="Mark as Read" class="btn btn-primary" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnDelete" runat="server" Text="Delete Selected" class="btn btn-primary"  />
                            <br />
                            <br />
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSourceMSG" CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="msgID" Width="733px">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelect" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="SenderName" SortExpression="SenderName" HeaderText="SenderName"></asp:BoundField>
                                    <asp:BoundField DataField="Subject" HeaderText="Subject" SortExpression="Subject" />
                                    <asp:BoundField DataField="Body" HeaderText="Body" SortExpression="Body" />

                                    <asp:BoundField DataField="sentDate" HeaderText="sentDate" SortExpression="sentDate" DataFormatString="{0:dd/MM/yyyy}" />
                                    <asp:BoundField DataField="sentTime" HeaderText="sentTime" SortExpression="sentTime" DataFormatString="{0:hh:mm tt}" />
                                    <asp:BoundField DataField="msgID" SortExpression="msgID" ShowHeader="False">
                                        <ControlStyle Width="2px" />
                                        <FooterStyle Width="2px" />
                                        <HeaderStyle Width="2px" />
                                        <ItemStyle Width="2px" />
                                    </asp:BoundField>
                                </Columns>
                                <EditRowStyle BackColor="#999999" />
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                            </asp:GridView>

                            <asp:SqlDataSource ID="SqlDataSourceMSG" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT msgID, sentDate, sentTime, Subject, Body, isRead, SenderName FROM vw_msgSentFromAdmin">
                            </asp:SqlDataSource>
                        </td>
                    </tr>
                </table> 
            </div>
        </div>
        <div class="clearfix"></div>
    </div>
    </asp:Content>