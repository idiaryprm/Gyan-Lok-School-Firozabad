<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/AdminMaster.Master" CodeBehind="CreateUsers.aspx.vb" Inherits="iDiary_V3.CreateUsers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AdminMasterContents" runat="server">
<%--     <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />--%>
        <table class="table">
        <tr>
            <td style="height: 30px; text-decoration: underline; font-size: 14px;" colspan="2">
                <strong>USER MANAGEMENT</strong></td>
            <td style="width: 17%; height: 30px">
                &nbsp;</td>
            <td rowspan="10" valign="top" width="65%">
                <br />
                <asp:Table ID="myTable" runat="server" CellPadding="0" CellSpacing="0" 
                    Width="99%" BorderColor="Black" BorderWidth="1px" GridLines="Both">
                </asp:Table>
                <br />
                <br />
                <asp:TreeView ID="TreeView2" runat="server" ShowCheckBoxes="All" ShowLines="True" BorderStyle="Solid" BorderWidth="1px" ExpandDepth="0" Font-Names="Tahoma" ForeColor="Black" Width="233px" Visible="False">
                    <Nodes>
                        <asp:TreeNode Text="Enquiry" Value="Enquiry">
                            <asp:TreeNode Text="Entry" Value="Entry"></asp:TreeNode>
                            <asp:TreeNode Text="Processing" Value="Processing"></asp:TreeNode>
                        </asp:TreeNode>
                        <asp:TreeNode Text="Student" Value="Student">
                            <asp:TreeNode Text="Entry" Value="Entry"></asp:TreeNode>
                            <asp:TreeNode Text="Search" Value="Search"></asp:TreeNode>
                            <asp:TreeNode Text="Alter" Value="Alter"></asp:TreeNode>
                            <asp:TreeNode Text="Certificate generation" Value="Certificate generation"></asp:TreeNode>
                            <asp:TreeNode Text="Promotion" Value="Promotion"></asp:TreeNode>
                            <asp:TreeNode Text="Master Records" Value="Master Records"></asp:TreeNode>
                        </asp:TreeNode>
                        <asp:TreeNode Text="Transportation" Value="Transportation">
                            <asp:TreeNode Text="Assignment" Value="Assignments"></asp:TreeNode>
                            <asp:TreeNode Text="Master Records" Value="Master Records"></asp:TreeNode>
                        </asp:TreeNode>
                        <asp:TreeNode Text="Fee" Value="Fee">
                            <asp:TreeNode Text="Deposit" Value="Deposit"></asp:TreeNode>
                            <asp:TreeNode Text="Alter" Value="Alter"></asp:TreeNode>
                            <asp:TreeNode Text="Reports" Value="Reports"></asp:TreeNode>
                            <asp:TreeNode Text="Master Records" Value="Master Records"></asp:TreeNode>
                        </asp:TreeNode>
                        <asp:TreeNode Text="Library" Value="Library">
                            <asp:TreeNode Text="Entry" Value="Entry"></asp:TreeNode>
                            <asp:TreeNode Text="Issue/Return/Search" Value="Issue/Return/Search"></asp:TreeNode>
                            <asp:TreeNode Text="Transactions/Reports" Value="Transactions/Reports"></asp:TreeNode>
                            <asp:TreeNode Text="Master Records" Value="Master Records"></asp:TreeNode>
                        </asp:TreeNode>
                        <asp:TreeNode Text="HR/PayRoll" Value="HR/PayRoll">
                            <asp:TreeNode Text="Add/Alter" Value="Add/Alter"></asp:TreeNode>
                            <asp:TreeNode Text="Master Records" Value="Master Records"></asp:TreeNode>
                            <asp:TreeNode Text="Transactions" Value="Transactions"></asp:TreeNode>
                            <asp:TreeNode Text="Reports" Value="Reports"></asp:TreeNode>
                        </asp:TreeNode>
                        <asp:TreeNode Text="e-Docs" Value="e-Docs">
                            <asp:TreeNode Text="Manage" Value="Manage"></asp:TreeNode>
                            <asp:TreeNode Text="Search" Value="Search"></asp:TreeNode>
                        </asp:TreeNode>
                        <asp:TreeNode Text="PettyCash" Value="PettyCash">
                            <asp:TreeNode Text="View" Value="View"></asp:TreeNode>
                            <asp:TreeNode Text="Add/Alter" Value="Add/Alter"></asp:TreeNode>
                        </asp:TreeNode>
                        <asp:TreeNode Text="TeacherPanel" Value="TeacherPanel">
                            <asp:TreeNode Text="View" Value="View"></asp:TreeNode>
                            <asp:TreeNode Text="Add/Alter" Value="Add/Alter"></asp:TreeNode>
                        </asp:TreeNode>
                    </Nodes>
                </asp:TreeView>
            </td>
        </tr>
        <tr>
            <td style="width: 17%; height: 30px">
                Choose Employee</td>
            <td style="width: 37%; height: 30px">
                <asp:TextBox ID="txtEmpName" runat="server" BorderStyle="Solid" 
                    BorderWidth="1px" Width="172px"></asp:TextBox>
            &nbsp;<asp:Button ID="btnNameSearch" runat="server" Text="Go" class="btn btn-sm btn-primary" />
            </td>
            <td style="width: 17%; height: 30px">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="height: 15px" colspan="3">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" AutoGenerateSelectButton="True" DataKeyNames="EmpID" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataSourceID="SqlDataSource1" ShowHeader="False" Width="100%">
                    <Columns>
                        
                          <asp:BoundField DataField="EmpCode" HeaderText="EmpCode" SortExpression="EmpCode" />
                        <asp:BoundField DataField="EmpName" HeaderText="EmpName" SortExpression="EmpName" />
                        <asp:BoundField DataField="SchoolName" HeaderText="School" SortExpression="SchoolName" />
                          <asp:BoundField DataField="DeptName" HeaderText="DeptName" SortExpression="DeptName" />
                        <asp:BoundField DataField="DesgName" HeaderText="DesgName" SortExpression="DesgName" />
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
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT SchoolName,[EmpCode], [EmpName], [DeptName], [DesgName],EMPID FROM [vw_Employees] Where EmpName Like '%@EmpName@%'">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="txtEmpName" DefaultValue="" Name="EmpName" PropertyName="Text" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td style="width: 17%; height: 44px">
                Login ID</td>
            <td style="width: 37%; height: 44px">
                <asp:TextBox ID="txtLoginID" runat="server" BorderStyle="Solid" 
                    BorderWidth="1px" Width="172px"></asp:TextBox>
                &nbsp;<asp:Button ID="btnShow" runat="server" Text="Show" class="btn btn-sm btn-primary" />
            </td>
            <td style="width: 17%; height: 44px">
                Permissions</td>
        </tr>
        <tr>
            <td style="width: 17%; height: 30px">
                Name of the User</td>
            <td style="width: 37%; height: 30px">
                <asp:TextBox ID="txtUserName" runat="server" BorderStyle="Solid" 
                    BorderWidth="1px" Width="172px" Enabled="False"></asp:TextBox>
            </td>
            <td style="width: 17%; height: 30px">
                <asp:Label ID="lblEmpID" runat="server" Visible="False" Width="20px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 17%; height: 30px">
                Password</td>
            <td style="width: 37%; height: 30px">
                <asp:TextBox ID="txtPass" runat="server" BorderStyle="Solid" BorderWidth="1px" 
                    TextMode="Password" Width="172px"></asp:TextBox>
            </td>
            <td style="width: 17%; height: 30px">
            </td>
        </tr>
        <tr>
            <td style="width: 17%; height: 30px">
                Retype Password</td>
            <td style="width: 37%; height: 30px">
                <asp:TextBox ID="txtRePass" runat="server" BorderStyle="Solid" 
                    BorderWidth="1px" TextMode="Password" Width="172px"></asp:TextBox>
            </td>
            <td style="width: 17%; height: 30px">
            </td>
        </tr>
        <tr>
            <td style="width: 17%">
                School Permission</td>
            <td colspan="2">
                <asp:CheckBoxList ID="chkSchool" runat="server" RepeatColumns="1" width="230px">
                </asp:CheckBoxList>
                </td>
        </tr>
        <tr>
            <td style="width: 17%">
                &nbsp;</td>
            <td style="width: 37%">
                <asp:Button ID="btnSave" runat="server" Text="Create" Width="74px" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px" Visible="False" />
                &nbsp;
                <asp:Button ID="btnUpdate" runat="server" Text="Update" Width="74px" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px" Visible="False" />
            </td>
            <td style="width: 17%">
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT UserID,LoginID,LoginPass,[EmpCode], [EmpName], [DeptName], [DesgName],SchoolName,vw_employees.empid FROM vw_Employees inner join Users on vw_Employees.EmpID=Users.EmpID order by vw_Employees.EmpName"></asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td style="width: 17%; height: 7px;">
            </td>
            <td style="width: 37%; height: 7px;">
                <asp:Label ID="lblStatus" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
            </td>
            <td style="width: 17%; height: 7px;">
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" AutoGenerateSelectButton="True" BackColor="White" BorderColor="#CC9966" DataKeyNames="EmpID" BorderStyle="None" CellPadding="4" CssClass="Grid" DataSourceID="SqlDataSource2" Width="97%">
                    <Columns>
                        <asp:BoundField DataField="EmpCode" HeaderText="Code" SortExpression="EmpCode" />
                        <asp:BoundField DataField="EmpName" HeaderText="EmpName" SortExpression="EmpName" />
                                         <asp:BoundField DataField="SchoolName" HeaderText="School" SortExpression="SchoolName" />
                        <asp:BoundField DataField="DeptName" HeaderText="Department" SortExpression="DeptName" />
                        <asp:BoundField DataField="DesgName" HeaderText="Designation" SortExpression="DesgName" />
                               <asp:BoundField DataField="LoginID" HeaderText="Login ID" SortExpression="LoginID" />
                         <asp:BoundField DataField="LoginPass" HeaderText="Password" SortExpression="LoginPass" />
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
            </td>
        </tr>
    </table>

</asp:Content>
