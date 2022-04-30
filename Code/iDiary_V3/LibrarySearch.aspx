<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="LibrarySearch.aspx.vb" Inherits="iDiary_V3.LibrarySearch" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Heading" runat="server">
    Library Search Wizard
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="col_3" style="margin-top: 20px;" id="panelEnquiryNo" runat="server">
        <div class="col-md-3 widget widget1" style="width: 100%">
            <div class="r3_counter_box">
                <table class="table">
                    <tr>
                        <td width="15%" colspan="2" style="width: 30%">
                            <strong><h3>Select Search Category</h3></strong></td>

                        <td width="15%">
                            <asp:RadioButton ID="rbBook" runat="server" Checked="True" Font-Bold="True" GroupName="SeachCategory" Text="Book" />
                        </td>
                        <td width="15%">
                            <asp:RadioButton ID="rbMagazine" runat="server" Font-Bold="True" GroupName="SeachCategory" Text="Magazine" />
                        </td>

                        <td width="15%">
                            <asp:RadioButton ID="rbDVD" runat="server" Font-Bold="True" GroupName="SeachCategory" Text="DVD/CD" />
                        </td>
                        <td width="15%">&nbsp;</td>

                        <td width="10%" align="right">&nbsp;</td>
                    </tr>

                    <tr>
                        <td width="15%">
                            <asp:CheckBox ID="chkAccNo" runat="server" Text="Accession No" />
                        </td>
                        <td width="15%">
                            <asp:TextBox ID="txtAccessionNo" runat="server" CssClass="textbox"></asp:TextBox>
                        </td>

                        <td width="15%">
                            <asp:CheckBox ID="chkTitle" runat="server" Text="Book Title" />
                        </td>
                        <td width="15%">
                            <asp:TextBox ID="txtTitle" runat="server" CssClass="textbox"></asp:TextBox>
                        </td>

                        <td width="15%">
                            <asp:CheckBox ID="chkCodeNo" runat="server" Text="Book Code No" />
                        </td>
                        <td width="15%">
                            <asp:TextBox ID="txtCodeNo" runat="server" CssClass="textbox"></asp:TextBox>
                        </td>

                        <td width="10%" align="right">&nbsp;</td>
                    </tr>

                    <tr>
                        <td width="15%">
                            <asp:CheckBox ID="chkAuthor" runat="server" Text="Author Name" />
                        </td>
                        <td width="15%">
                            <asp:TextBox ID="txtAuthor" runat="server" CssClass="textbox"></asp:TextBox>
                        </td>

                        <td width="15%">
                            <asp:CheckBox ID="chkPublisher" runat="server" Text="Publisher" />
                        </td>
                        <td width="15%">
                            <asp:TextBox ID="txtPublisher" runat="server" CssClass="textbox"></asp:TextBox>
                        </td>

                        <td width="15%">
                            <asp:CheckBox ID="chkBookCategory" runat="server" Text="Book Category" />
                        </td>
                        <td width="15%">
                            <asp:DropDownList ID="cboBookCategory" runat="server" AutoPostBack="True" CssClass="Dropdown">
                            </asp:DropDownList>
                        </td>

                        <td width="10%" align="right">&nbsp;</td>
                    </tr>

                    <tr>
                        <td width="15%">
                            <asp:CheckBox ID="chkIssued" runat="server" Text="Issued" />
                        </td>
                        <td width="15%">
                            <asp:DropDownList ID="cboIssued" runat="server" CssClass="Dropdown">
                                <asp:ListItem>No</asp:ListItem>
                                <asp:ListItem>Yes</asp:ListItem>
                            </asp:DropDownList>
                        </td>

                        <td width="15%">
                            <asp:CheckBox ID="chkstatus" runat="server" Text="Status" />
                        </td>
                        <td width="15%">
                            <asp:DropDownList ID="cboStatus" runat="server" CssClass="Dropdown">
                            </asp:DropDownList>
                        </td>

                        <td width="15%">&nbsp;
                <asp:Button ID="btnFind" runat="server" Text="Find" CssClass="btn btn-primary" />
                        </td>
                        <td width="15%"></td>

                        <td width="10%" align="right">&nbsp;</td>
                    </tr>


                    <tr>
                        <td width="15%"></td>
                        <td width="15%">&nbsp;</td>

                        <td width="15%">&nbsp;</td>
                        <td width="15%">&nbsp;</td>

                        <td width="15%">&nbsp;</td>
                        <td width="15%">&nbsp;</td>

                        <td width="10%" align="right">&nbsp;</td>
                    </tr>


                    <tr>
                        <td colspan="7">
                            <br />
                            <asp:Label ID="lblStatus" runat="server" ForeColor="#3333CC" style="font-weight: 700"></asp:Label>
                            <br />
                        </td>
                    </tr>


                    <tr>
                        <td colspan="7">
                            
                            <div id="gvDiv" style="overflow-x:scroll;height:500px">
                                <asp:Label ID="lblTitle" runat="server" Font-Bold="True" Font-Size="Small"></asp:Label>
                                <asp:GridView ID="GridView1" runat="server" CellPadding="4" CssClass="Grid"
                                    Width="100%" AutoGenerateColumns="False" >
                                    <RowStyle BackColor="White" ForeColor="#330099" />
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                Sr. No.
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblSRNO" runat="server"
                                                    Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="AccNo" HeaderText="AccNo" SortExpression="AccNo"></asp:BoundField>
                                        <asp:BoundField DataField="BookTitle" HeaderText="Title" SortExpression="BookTitle"></asp:BoundField>
                                        <asp:BoundField DataField="BookCodeNo" HeaderText="CodeNo" SortExpression="BookCodeNo"></asp:BoundField>
                                        <asp:BoundField DataField="Authors" HeaderText="Authors"
                                            SortExpression="Authors"></asp:BoundField>
                                        <asp:BoundField DataField="PubName" HeaderText="Publisher"
                                            SortExpression="PubName"></asp:BoundField>
                                        <asp:BoundField DataField="Issued" HeaderText="Issued" SortExpression="Issued" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Button ID="btnDetails" runat="server" Text="Details" BorderStyle="Solid" CommandName="details" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" BorderWidth="1px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                                    <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                                    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                                </asp:GridView>
                                <asp:SqlDataSource ID="SqlDataSource1" runat="server"
                                    ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>"
                                    SelectCommand="SELECT [BookAccNo], [BookTitle], [BookCodeNo], [AuthorName], [PubName], [Issued] FROM [vw_BookMaster] where BookAccNo < 0"></asp:SqlDataSource>
                                
                                <br />
                            </div>
                            <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="btn btn-primary" />
                        </td>
                    </tr>


                </table>
            </div>
        </div>
        <div class="clearfix"></div>
    </div>

</asp:Content>
