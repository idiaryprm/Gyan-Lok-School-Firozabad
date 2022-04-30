<%@ Page Language="VB" MasterPageFile="~/IdiaryPanel/Parent/ParentMaster.master" AutoEventWireup="false" Inherits="iDiary_V3.Parent_SentMessages" title="Untitled Page" Codebehind="SentMessages.aspx.vb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Heading" runat="server">
    Sent Message
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <div class="col_3" style="margin-top: 20px;" id="panelEnquiryNo" runat="server">
        <div class="col-md-3 widget widget1" style="width: 100%">
            <div class="r3_counter_box">
                <table class="table">
                    <tr>
                        <td width="440px">
                            <asp:DataList ID="DataList1" runat="server" DataSourceID="SqlDataSourceSENT"
                                BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                                CellPadding="4" ForeColor="Black" GridLines="Vertical" Width="440px">
                                <FooterStyle BackColor="#CCCC99" />
                                <AlternatingItemStyle BackColor="White" />
                                <ItemStyle BackColor="#F7F7DE" />
                                <ItemTemplate>
                                    SentDate:
                        <asp:Label ID="SentDateLabel" runat="server" Text='<%# Eval("SentDate", "{0:dd/MM/yyyy}")%>' />
                                    <br />
                                    SentTo:
                        <asp:Label ID="SentToLabel" runat="server" Text='<%# Eval("SentTo") %>' />
                                    <br />
                                    Subject:
                        <asp:Label ID="SubjectLabel" runat="server" Text='<%# Eval("Subject") %>' />
                                    <br />
                                    SentMessage:
                        <asp:Label ID="SentMessageLabel" runat="server" Text='<%# Eval("SentMessage") %>' />
                                    <br />
                                    <br />
                                </ItemTemplate>
                                <SelectedItemStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                            </asp:DataList>

                            <asp:SqlDataSource ID="SqlDataSourceSENT" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT [SentDate], [SentTo], [Subject], [SentMessage] FROM [Messages] WHERE ([SID] = @SID) Order By SentDate">
                                <SelectParameters>
                                    <asp:SessionParameter Name="SID" SessionField="SID" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </td>
                    </tr>

                    <tr>
                        <td width="440px">&nbsp;</td>
                    </tr>

                    <tr>
                        <td width="440px">&nbsp;</td>
                    </tr>

                </table>
            </div>
        </div>
        <div class="clearfix"></div>
    </div>
      
</asp:Content>

