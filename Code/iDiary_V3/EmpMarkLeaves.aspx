<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/PayrollTransaction.master" CodeBehind="EmpMarkLeaves.aspx.vb" Inherits="iDiary_V3.MarkLeaves" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SubHeading" runat="server">
    Mark Leave Records
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CertificateContent" runat="server">
    <table class="table">
        <tr>
            <td width="25%">Employee Code</td>
            <td width="25%">
                <asp:TextBox ID="txtEmpCode" runat="server" CssClass="textbox"></asp:TextBox>
            </td>
            <td width="25%">
                <asp:Button ID="btnShow" runat="server" Text="&gt;&gt;" CssClass="btn btn-sm btn-primary" />
            </td>
            <td width="25%">&nbsp;</td>
        </tr>
        <tr>
            <td width="25%">Employee Category</td>
            <td width="25%">
                <asp:DropDownList ID="cboEmpCat" runat="server" CssClass="Dropdown" AutoPostBack="True"></asp:DropDownList>
            </td>
            <td width="25%">Employee Name</td>
            <td width="25%">
                <asp:DropDownList ID="cboEmpName" runat="server" CssClass="Dropdown" AutoPostBack="True"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td width="25%">Department</td>
            <td width="25%">
                <asp:TextBox ID="txtDeptName" runat="server" CssClass="textbox" ReadOnly="True"></asp:TextBox>
            </td>
            <td width="25%">Mobile</td>
            <td width="25%">
                <asp:TextBox ID="txtMobile" runat="server" CssClass="textbox" ReadOnly="True"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td width="25%">Designation</td>
            <td width="25%">
                <asp:TextBox ID="txtDesgName" runat="server" CssClass="textbox" ReadOnly="True"></asp:TextBox>
            </td>
            <td width="25%">Date of Joining</td>
            <td width="25%">
                <asp:TextBox ID="txtDOJ" runat="server" CssClass="textbox" ReadOnly="True"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td width="25%">Leave Type</td>
            <td width="25%">
                <asp:DropDownList ID="cboLeaveType" runat="server" CssClass="Dropdown" AutoPostBack="True"></asp:DropDownList>
            </td>
            <td width="25%">Balance Leaves</td>
            <td width="25%">
                <asp:TextBox ID="txtBalanceLeaves" runat="server" CssClass="textbox" ReadOnly="True"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td width="25%">From </td>
            <td width="25%">
                <asp:TextBox ID="txtFrom" runat="server" CssClass="textbox"></asp:TextBox>
                <asp:CalendarExtender ID="txtFrom_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFrom"></asp:CalendarExtender>
            </td>
            <td width="25%">To</td>
            <td width="25%">
                <asp:TextBox ID="txtTo" runat="server" CssClass="textbox"></asp:TextBox>
                <asp:CalendarExtender ID="txtTo_CalendarExtender" runat="server" TargetControlID="txtTo" Format="dd/MM/yyyy"></asp:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td width="25%" colspan="2" style="width: 50%">
                <asp:Label ID="lblStatus" runat="server" Style="font-weight: 700; color: #FF0000"></asp:Label>
            </td>
            <td width="25%">&nbsp;</td>
            <td width="25%">&nbsp;</td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Button ID="btnSave" runat="server" Text="Save" Width="100px" CssClass="btn btn-primary" />
            </td>
            <td width="25%">
                <asp:TextBox ID="txtEmpID" runat="server" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px" Width="29px" Visible="False"></asp:TextBox>
            </td>
            <td width="25%">&nbsp;</td>
            <td width="25%">&nbsp;</td>
        </tr>
        <tr>
            <td width="25%">&nbsp;</td>
            <td width="25%">&nbsp;</td>
            <td width="25%">&nbsp;</td>
            <td width="25%">&nbsp;</td>
        </tr>
        <tr>
            <td width="25%">
                <strong>Pending Leaves</strong></td>
            <td width="25%">
                <strong>Leave History</strong></td>
            <td width="25%">&nbsp;</td>
            <td width="25%">&nbsp;</td>
        </tr>
        <tr>
            <td style="width: 0%" valign="Top">
                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CssClass="Grid" DataSourceID="SqlDataSource1" Width="90%">
                    <Columns>
                        <asp:BoundField DataField="AttDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Pending Leave Dates" HtmlEncode="False" SortExpression="AttDate" />
                    </Columns>

                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT  [AttDate] FROM [vw_Employee_Attendance] Where EmpID<0"></asp:SqlDataSource>
            </td>
            <td colspan="3" style="width: 50%" valign="Top">
                <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" CssClass="Grid" DataSourceID="SqlDataSource2" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="AttDate" HeaderText="Leave Date" SortExpression="AttDate" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField DataField="LeaveName" HeaderText="Leave Type" SortExpression="LeaveName" />
                        <asp:BoundField DataField="Remark" HeaderText="Remark" SortExpression="Remark" />
                    </Columns>

                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT [AttDate], [LeaveName], [Remark] FROM [vw_Employee_Attendance] Where EmpID<0 Order By AttDate DESC"></asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td width="25%">&nbsp;</td>
            <td width="25%">&nbsp;</td>
            <td width="25%">&nbsp;</td>
            <td width="25%">&nbsp;</td>
        </tr>
        <tr>
            <td width="25%" colspan="2" style="width: 50%">
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            </td>
            <td width="25%">&nbsp;</td>
            <td width="25%">&nbsp;</td>
        </tr>
    </table>
</asp:Content>
