<%@ Page Language="VB" MasterPageFile="~/ExamAdminMasterPage.master" AutoEventWireup="false" Inherits="iDiary_V3.UserPermissions" title="Untitled Page" Codebehind="UserPermissions.aspx.vb" %>

 
<asp:Content ID="Content1" ContentPlaceHolderID="SubHeading" Runat="Server">
    Administrator - Permission Management
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" Runat="Server">
            <table width="100%" cellpadding="1" cellspacing="1">
            <tr>
                <td style="width: 1%; height: 30px" align="Left">&nbsp;</td>
                <td style="width: 12%; height: 30px" align="Left"><b>Login ID</b></td>
                <td style="width: 12%; height: 30px" align="Left">
                <asp:DropDownList ID="cbologinID" runat="server" Width="172px" 
                    AutoPostBack="True" CssClass="Dropdown">
                </asp:DropDownList>
                &nbsp;</td>
                <td style="width: 17%; " align="Left" valign="top">
                    <strong>&nbsp; User Name</strong></td>
                <td style="width: 17%; " align="Left" valign="top">
                    <asp:TextBox ID="txtUName" runat="server" BorderStyle="Solid" BorderWidth="1px" Width="165px" CssClass="textbox"></asp:TextBox>
                </td>
            </tr>        
            <tr>
                <td style="width: 1%; height: 30px">&nbsp;</td>
                <td style="width: 12%; height: 30px" align="Left"><b>Class</b></td>
                <td style="width: 12%; height: 30px" align="Left">
                <asp:DropDownList ID="cboClass" runat="server" Width="172px" 
                    AutoPostBack="True" CssClass="Dropdown">
                </asp:DropDownList>
                </td>
                <td style="width: 17%; " align="Left" valign="top" colspan="2">
                    <asp:CheckBox ID="chkClassTeacher" runat="server" style="font-weight: 700; color: #660033" Text=" Is a Class Teacher ?" />
                </td>
            </tr>        
            <tr>
                <td style="width: 1%; height: 30px">&nbsp;</td>
                <td style="width: 12%; height: 30px" align="Left"><b>Section</b></td>
                <td style="width: 12%; height: 30px" align="Left">
                <b>
                <asp:DropDownList ID="cboSection" runat="server" Width="172px" 
                    AutoPostBack="True" CssClass="Dropdown">
                </asp:DropDownList>
                </b>
                </td>
                <td style="width: 17%; " align="Left" valign="top" colspan="2" rowspan="5">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" DataSourceID="SqlDataSource1" ForeColor="#333333" GridLines="None">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:CommandField ShowSelectButton="True" />
                            <asp:BoundField DataField="ClassName" HeaderText="Class" SortExpression="ClassName" />
                            <asp:BoundField DataField="SecName" HeaderText="Section" SortExpression="SecName" />
                            <asp:BoundField DataField="SubjectName" HeaderText="Subject Name" SortExpression="SubjectName" />
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
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT [ClassName], [SecName], [SubjectCode], [SubjectName] FROM [vw_UserSubjectPermission] WHERE ([LoginID] = @LoginID)">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="cbologinID" Name="LoginID" PropertyName="SelectedValue" Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>        
            <tr>
                <td style="width: 1%; ">&nbsp;</td>
                <td align="Left" colspan="2" rowspan="3">
                    <asp:GridView ID="gvSubjectPermission" runat="server" AutoGenerateColumns="False" DataKeyNames="SubjectID" CssClass="Grid" DataSourceID="SqlDataSource2">
                        <Columns>
                            <asp:BoundField DataField="ClassName" HeaderText="Class" SortExpression="ClassName" />
                            <asp:BoundField DataField="SecName" HeaderText="Section" SortExpression="SecName" />
                            <asp:BoundField DataField="SubjectName" HeaderText="Subject" SortExpression="SubjectName" />
                            <asp:TemplateField HeaderText="Teaches">
                                <ItemTemplate>
                                    <asp:CheckBox ID="cbTeaches" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Permission Applicable">
                                <ItemTemplate>
                                    <asp:CheckBox ID="cbPermission" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT EntryType as [IsClassTeacher],EntryType as [IsPermissionApplicable], [ClassName], [SecName], [SubjectName],[SubjectID] FROM [vw_SubjectMapping]"></asp:SqlDataSource>
                    <b>
                    <asp:Button ID="btnAddSubject0" runat="server" Text="Save / Update" CssClass="hvr-glow" />
                    <br />
                    <asp:Label ID="lblStatus" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                </b>
                </td>
            </tr>        
            <tr>
                <td style="width: 1%; height: 30px">&nbsp;</td>
            </tr>        
            <tr>
                <td style="width: 1%; height: 30px">&nbsp;</td>
            </tr>        
            <tr>
                <td style="width: 1%; height: 30px">&nbsp;</td>
                <td style="width: 12%; height: 30px" align="Left">
                <asp:Label ID="lblUserID" runat="server" Visible="False" Width="20px"></asp:Label>
                </td>
                <td style="width: 12%; height: 30px" align="Left">
                    &nbsp;</td>
            </tr>        
            </table>


</asp:Content>

