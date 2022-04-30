<%@ Page Language="VB" MasterPageFile="~/AdminMaster.master" AutoEventWireup="false" Inherits="iDiary_V3.ClassTeacherAssignment" title="Untitled Page" Codebehind="ClassTeacherAssignment.aspx.vb" %>

 
<asp:Content ID="Content1" ContentPlaceHolderID="AdminMasterContents" Runat="Server">
    <%--<br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />--%>
    <strong>Class Teacher Assignment</strong>
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
            <td colspan="2">

                <asp:DropDownList ID="cboSchoolName" runat="server" AutoPostBack="true" CssClass="Dropdown" Width="300px">
                </asp:DropDownList>

            </td>
            <td style="width: 4%">&nbsp;</td>
            <td style="width: 15%">

                &nbsp;</td>
            <td style="width: 23%">

                &nbsp;</td>
            <td style="width: 10%">

                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 15%">

                <strong><span style="font-weight: normal">Emp Name</span></strong></td>
            <td style="width: 23%">

                <asp:TextBox ID="txtName" runat="server" CssClass="textbox"></asp:TextBox>

            </td>
            <td style="width: 10%">

                <asp:Button ID="btnNameSearch" runat="server" Text=">>" CssClass="btn btn-sm btn-primary" />

            </td>
            <td style="width: 4%"></td>
            <td style="width: 15%">

                <b><span style="font-weight: normal">Emp Code</span></b></td>
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
            <td>

                &nbsp;</td>
            <td>

                &nbsp;</td>
            <td>

                <b><span style="font-weight: normal">Section</span></b></td>
            <td style="width: 23%">

                <b>
                    <asp:DropDownList ID="cboSection" runat="server"
                        AutoPostBack="false" CssClass="Dropdown">
                    </asp:DropDownList>
                </b>

            </td>
            <td style="width: 10%">

                <asp:Label ID="lblCSSID" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblEmpID" runat="server" Visible="False"></asp:Label>

            </td>
        </tr>
        <tr>
            <td style="width: 15%">

                &nbsp;</td>
            <td style="width: 23%">

                <b>
                    <asp:Button ID="btnSave" runat="server" Text="Save / Update" CssClass="btn btn-primary" />
                </b>

            </td>
            <td style="width: 10%">&nbsp;</td>
            <td style="width: 4%">&nbsp;</td>
            <td style="width: 15%">&nbsp;</td>
            <td style="width: 23%">

                <b>
                    <asp:DropDownList ID="cboSubSection" runat="server"
                        AutoPostBack="True" CssClass="Dropdown" Visible="False">
                    </asp:DropDownList>
                </b>

            </td>
            <td style="width: 10%">

                <asp:Label ID="lblUserID" runat="server" Visible="False"></asp:Label>
                </td>
        </tr>
        <tr>
            <td colspan="3">

                <b>
                    <asp:Label ID="lblStatus" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                </b>

            </td>
            <td style="width: 4%">&nbsp;</td>
            <td colspan="3">

                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="7">

                 <asp:GridView ID="gvClassTeacher" runat="server" CssClass="Grid" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" AllowPaging="false" Width="100%" >
                  
                    <Columns>
                        <%--<asp:CommandField ShowSelectButton="True" />--%>
                        <asp:BoundField DataField="ClassName" HeaderText="Class" SortExpression="ClassName" />
                        <asp:BoundField DataField="SecName" HeaderText="Section" SortExpression="SecName" />
                        <asp:BoundField DataField="EmpCode" HeaderText="Emp Code" SortExpression="EmpCode" />
                        <asp:BoundField DataField="EmpName" HeaderText="Class Teacher" SortExpression="EmpName" />
                       
                    </Columns>
              
                   
                  
               
                   
                </asp:GridView>
                 <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT distinct ClassName,SecName,EmpCode,EmpName,Displayorder, SecDisplayOrder FROM [vw_ClassTeacher] where CSSID=0 order by Displayorder, SecDisplayOrder">
                </asp:SqlDataSource>
            </td>
        </tr>
    </table>        
    

</asp:Content>

