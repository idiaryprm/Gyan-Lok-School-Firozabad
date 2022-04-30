<%@ Page Language="VB" MasterPageFile="~/ExamAdminMasterPage.master" AutoEventWireup="false" Inherits="iDiary_V3.ExamUserPermissions" title="Untitled Page" Codebehind="ExamUserPermissions.aspx.vb" %>

 
<asp:Content ID="Content1" ContentPlaceHolderID="SubHeading" Runat="Server">
    Administrator - Permission Management
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" Runat="Server">
 <%--   <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
   --%>
     
    <table class="table">
        <tr>
            <td colspan="7">

                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="EmpID" AutoGenerateSelectButton="True" BackColor="White" BorderColor="#CC9966" BorderStyle="None" CellPadding="4" DataSourceID="sdsEmployee" Width="97%" CssClass="Grid">
                    <Columns>
                        <asp:BoundField DataField="EmpCode" HeaderText="Code" SortExpression="EmpCode" />
                        <asp:BoundField DataField="EmpName" HeaderText="EmpName" SortExpression="EmpName" />
                        <asp:BoundField DataField="DeptName" HeaderText="Department" SortExpression="DeptName" />
                        <asp:BoundField DataField="DesgName" HeaderText="Designation" SortExpression="DesgName" />
                    </Columns>
                    <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                    <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                    <RowStyle BackColor="White" ForeColor="#330099" />
                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                    <SortedAscendingCellStyle BackColor="#FEFCEB" />
                    <SortedAscendingHeaderStyle BackColor="#AF0101" />
                    <SortedDescendingCellStyle BackColor="#F6F0C0" />
                    <SortedDescendingHeaderStyle BackColor="#7E0000" />
                </asp:GridView>
                <asp:SqlDataSource ID="sdsEmployee" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT EmpID, [EmpCode], [EmpName], [DeptName], [DesgName] FROM [vw_Employees] Where EmpID=0"></asp:SqlDataSource>

            </td>
        </tr>
        <tr>
            <td style="width: 15%">

                School Name</td>
            <td colspan="4">

                <asp:DropDownList ID="cboSchoolName" runat="server" CssClass="Dropdown" Width="300px" AutoPostBack="true" ></asp:DropDownList>
</td>
            <td style="width: 23%">

                <asp:Label ID="lblSchoolID" runat="server" Visible="false"></asp:Label>
                </td>
            <td style="width: 10%">

                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 15%">

                <strong><span style="font-weight: normal">Employee Name</span></strong></td>
            <td style="width: 23%">

                <asp:TextBox ID="txtName" runat="server" CssClass="textbox"></asp:TextBox>

            </td>
            <td style="width: 10%">

                <asp:Button ID="btnNameSearch" runat="server" Text=">>" CssClass="btn btn-sm btn-primary" />

            </td>
            <td style="width: 4%"></td>
            <td style="width: 15%">

                <b><span style="font-weight: normal">Employe Code</span></b></td>
            <td style="width: 23%">

                <asp:TextBox ID="txtEmpCode" runat="server" CssClass="textbox"></asp:TextBox>

            </td>
            <td style="width: 10%">

                <asp:Button ID="btnEmpCode" runat="server" Text=">>" CssClass="btn btn-sm btn-primary" />

            </td>
        </tr>
        <tr>
            <td style="width: 15%">

                <strong><span style="font-weight: normal">Department</span></strong></td>
            <td style="width: 23%">

                <asp:TextBox ID="txtEmpDepartment" runat="server" CssClass="textbox"></asp:TextBox>

            </td>
            <td style="width: 10%"></td>
            <td style="width: 4%"></td>
            <td style="width: 15%">Designation</td>
            <td style="width: 23%">

                <asp:TextBox ID="txtEmpDesignation" runat="server" CssClass="textbox"></asp:TextBox>

            </td>
            <td style="width: 10%"></td>
        </tr>
        <tr>
            <td style="width: 15%">

                <b><span style="font-weight: normal">Class</span></b></td>
            <td style="width: 23%">

                <asp:DropDownList ID="cboClass" runat="server"
                    AutoPostBack="True" CssClass="Dropdown">
                </asp:DropDownList>

            </td>
            <td colspan="3">

                <asp:CheckBox ID="chkClassTeacher" runat="server" Style="font-weight: 700; color: #660033" Text=" Is a Class Teacher ?" />

            </td>
            <td style="width: 23%"></td>
            <td style="width: 10%">

                <asp:Label ID="lblCSSID" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblEmpID" runat="server" Visible="False"></asp:Label>

            </td>
        </tr>
        <tr>
            <td style="width: 15%">

                <b><span style="font-weight: normal">Section</span></b></td>
            <td style="width: 23%">

                <b>
                    <asp:DropDownList ID="cboSection" runat="server"
                        AutoPostBack="True" CssClass="Dropdown">
                    </asp:DropDownList>
                </b>

            </td>
            <td style="width: 10%">&nbsp;</td>
            <td style="width: 4%">&nbsp;</td>
            <td style="width: 15%">Sub - Section</td>
            <td style="width: 23%">

                <b>
                    <asp:DropDownList ID="cboSubSection" runat="server"
                        AutoPostBack="True" CssClass="Dropdown">
                    </asp:DropDownList>
                </b>

            </td>
            <td style="width: 10%">&nbsp;</td>
        </tr>
        <tr>
            <td style="width: 15%">Subject Group</td>
            <td style="width: 23%">

                <b>
                    <asp:DropDownList ID="cboSubjectGroup" runat="server"
                        AutoPostBack="True" CssClass="Dropdown">
                    </asp:DropDownList>
                </b>

            </td>
            <td style="width: 10%">

                <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn btn-sm btn-primary" />

            </td>
            <td style="width: 4%">&nbsp;</td>
            <td style="width: 15%">&nbsp;</td>
            <td style="width: 23%">&nbsp;</td>
            <td style="width: 10%">&nbsp;</td>
        </tr>
        <tr>
            <td style="width: 15%">&nbsp;</td>
            <td style="width: 23%">&nbsp;</td>
            <td style="width: 10%">&nbsp;</td>
            <td style="width: 4%">&nbsp;</td>
            <td style="width: 15%">&nbsp;</td>
            <td style="width: 23%">&nbsp;</td>
            <td style="width: 10%">&nbsp;</td>
        </tr>
        <tr>
            <td colspan="3">

                <asp:GridView ID="gvSubjectPermission" runat="server" AutoGenerateColumns="False" DataKeyNames="SubjectID" CssClass="Grid" DataSourceID="SqlDataSource2" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="ClassName" HeaderText="Class" SortExpression="ClassName" />
                        <asp:BoundField DataField="SecName" HeaderText="Section" SortExpression="SecName" />
                        <asp:BoundField DataField="SubjectName" HeaderText="Subject" SortExpression="SubjectName" />
                        <asp:TemplateField HeaderText="Teaches">
                            <ItemTemplate>
                                <asp:CheckBox ID="cbTeaches" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Marks Entry">
                            <ItemTemplate>
                                <asp:CheckBox ID="cbPermission" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT EntryType as [IsClassTeacher],EntryType as [IsPermissionApplicable], [ClassName], [SecName], [SubjectName],[SubjectID] FROM [vw_ExamSubjectMapping]"></asp:SqlDataSource>

            </td>
            <td style="width: 4%">&nbsp;</td>
            <td colspan="3">

                <asp:GridView ID="gvMappedSubjects" runat="server" AutoGenerateColumns="False" DataKeyNames="SubjectID" DataSourceID="SqlDataSource1" CssClass="Grid" Width="95%">
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" />
                        <asp:BoundField DataField="ClassName" HeaderText="Class" SortExpression="ClassName" />
                        <asp:BoundField DataField="SecName" HeaderText="Section" SortExpression="SecName" />
                        <asp:BoundField DataField="SubjectName" HeaderText="Subject Name" SortExpression="SubjectName" />
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT [ClassName], [SecName], [SubjectCode], [SubjectID], [SubjectName] FROM [vw_UserSubjectPermission] Where EmpID=0 "></asp:SqlDataSource>

            </td>
        </tr>
        <tr>
            <td colspan="3">

                <b>
                    <asp:Button ID="btnSave" runat="server" Text="Save / Update" CssClass="btn btn-primary" />
                </b>

            </td>
            <td style="width: 4%">&nbsp;</td>
            <td colspan="3">&nbsp;</td>
        </tr>
        <tr>
            <td colspan="3">

                <b>
                    <asp:Label ID="lblStatus" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                </b>

            </td>
            <td style="width: 4%">&nbsp;</td>
            <td colspan="3">

                <asp:Label ID="lblUserID" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblExamGroupID" runat="server" Visible="False"></asp:Label>

            </td>
        </tr>
    </table>        
    

</asp:Content>

