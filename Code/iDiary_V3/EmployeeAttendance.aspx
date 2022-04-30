<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/PayrollTransaction.master" CodeBehind="EmployeeAttendance.aspx.vb" Inherits="iDiary_V3.EmployeeAttendance" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SubHeading" runat="server">
    Mark Daily Attendance
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CertificateContent" runat="server">
    <table class="table">
        <tr>
            <td width="20%">Employee Category</td>
            <td style="width: 27%">
                <asp:DropDownList ID="cboEmpCat" runat="server" CssClass="Dropdown"></asp:DropDownList>
            </td>
            <td width="20%">Attendance Date</td>
            <td width="20%">
                <asp:TextBox ID="txtAttDate" runat="server" CssClass="textbox"></asp:TextBox>
                <asp:CalendarExtender ID="txtAttDate_CalendarExtender" runat="server" TargetControlID="txtAttDate" Format="dd/MM/yyyy"></asp:CalendarExtender>
            </td>
            <td width="20%">&nbsp;</td>
        </tr>
        <tr>
            <td width="20%">Status</td>
            <td style="width: 27%">
                <asp:DropDownList ID="cboStatus" runat="server" CssClass="Dropdown"></asp:DropDownList>
            </td>
            <td width="20%">&nbsp;</td>
            <td width="20%">
                <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn btn-primary" Width="100px" />
            </td>
            <td width="20%">&nbsp;</td>
        </tr>
        <tr>
            <td width="20%" colspan="2">
                <asp:Label ID="lblStatus" runat="server" Style="font-weight: 700; color: #FF0000"></asp:Label>
            </td>
            <td width="20%">&nbsp;</td>
            <td width="20%">&nbsp;</td>
            <td width="20%" style="margin-left: 40px">&nbsp;</td>
        </tr>
        <tr>
            <td colspan="5">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="Grid" DataKeyNames="EmpID" DataSourceID="SqlDataSource1" Width="99%" Visible="False">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelect" runat="server" Visible="false" />
                                <asp:DropDownList ID="ddlAtt" Width="50px" CssClass="Dropdown" runat="server">

                                    <asp:ListItem>Pr</asp:ListItem>
                                    <asp:ListItem>Ab</asp:ListItem>
                                    <asp:ListItem>Hd</asp:ListItem>
                                    <asp:ListItem>Lt</asp:ListItem>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="EmpID" HeaderText="EmpID" ReadOnly="True" SortExpression="EmpID" />
                        <asp:BoundField DataField="EmpCode" HeaderText="EmpCode" SortExpression="EmpCode" />
                        <asp:BoundField DataField="EmpName" HeaderText="EmpName" SortExpression="EmpName" />
                        <asp:BoundField DataField="DesgName" HeaderText="DesgName" SortExpression="DesgName" />
                        <asp:BoundField DataField="DeptName" HeaderText="DeptName" SortExpression="DeptName" />
                    </Columns>

                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT [EmpID], [EmpCode], [EmpName], [DesgName], [DeptName] FROM [vw_Employees] Where EmpCatName=@EmpCatName AND StatusName=@StatusName Order By EmpName">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="cboEmpCat" Name="EmpCatName" PropertyName="Text" />
                        <asp:ControlParameter ControlID="cboStatus" Name="StatusName" PropertyName="Text" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td width="20%">
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" Width="100px" />
            </td>
            <td style="width: 27%">&nbsp;</td>
            <td width="20%">&nbsp;</td>
            <td width="20%">&nbsp;</td>
            <td width="20%">&nbsp;</td>
        </tr>
        <tr>
            <td colspan="5">
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            </td>
        </tr>
    </table>
    
</asp:Content>
