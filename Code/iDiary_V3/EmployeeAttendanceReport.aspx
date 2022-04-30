<%@ Page Language="VB" MasterPageFile="~/PayrollTransaction.master" AutoEventWireup="false" Inherits="iDiary_V3.EmployeeAttendanceReport" title="Untitled Page" Codebehind="EmployeeAttendanceReport.aspx.vb" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CertificateContent" Runat="Server">
  <%--  <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />--%>

    <table class="table">
        <tr>
            <td style="text-decoration: underline;" colspan="2">
                </td>
            <td style="width: 15%"></td>
            <td style="width: 15%"></td>
            <td style="width: 15%"></td>
            <td style="width: 15%"></td>
            <td style="width: 10%"></td>

        </tr>
        <tr>
            <td style="width: 15%">
                <b>Category</b></td>
            <td style="width: 15%">

                <asp:DropDownList ID="cboEmpCat" runat="server" Width="120px">
                </asp:DropDownList>

            </td>
            <td style="width: 15%">
                <strong>Department</strong></td>
            <td style="width: 15%">
                <asp:DropDownList ID="cboDepartment" runat="server" Width="120px">
                </asp:DropDownList>
            </td>
            <td style="width: 15%">
                <b>Status</b></td>
            <td style="width: 15%">
                <asp:DropDownList ID="cboStatus" runat="server" Width="120px">
                </asp:DropDownList>
            </td>
            <td style="width: 10%">&nbsp;</td>

        </tr>
        <tr>
            <td style="width: 15%">
                <strong>Date From</strong></td>
            <td style="width: 15%">

                <asp:TextBox ID="txtDateFrom" runat="server" Width="110px"></asp:TextBox>
                <asp:CalendarExtender ID="txtDateFrom_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDateFrom"></asp:CalendarExtender>

            </td>
            <td style="width: 15%">
                <b>&nbsp;Date To</b></td>
            <td style="width: 15%">
                <asp:TextBox ID="txtDateTo" runat="server" Width="110px"></asp:TextBox>
                <asp:CalendarExtender ID="txtDateTo_CalendarExtender0" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDateTo"></asp:CalendarExtender>
            </td>
            <td style="width: 15%">
                <b>Report Type</b></td>
            <td style="width: 15%">
                <asp:DropDownList ID="cboReportType" runat="server" Width="120px">
                    <asp:ListItem>Attendance Summary DayWise</asp:ListItem>
                    <asp:ListItem>Attendance Roster</asp:ListItem>
                    <asp:ListItem>Attendance In/Out Summary</asp:ListItem>
                    <asp:ListItem>Attendance Summary</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style="width: 10%">&nbsp;</td>

        </tr>
        <tr>
            <td style="width: 15%">
                &nbsp;</td>
            <td colspan="5">

                <asp:Label ID="lblStatus" runat="server" ForeColor="Navy"
                    Style="font-weight: 700"></asp:Label>
            </td>
            <td style="width: 10%">&nbsp;</td>

        </tr>
        <tr>
            <td colspan="2">

                <asp:Button ID="btnReport" runat="server"  Text="Generate Report" CssClass="btn btn-primary"/>

            </td>
            <td style="width: 15%">&nbsp;</td>
            <td colspan="2">

                <asp:Button ID="btnReportSummary" runat="server" Style="margin-left: 0px" Text="Generate Summary Report"  Visible="False" />

                <asp:Button ID="btnDetailReport" runat="server"  Text="Generate Detailed Report" CssClass="btn btn-primary" Visible="False"/>

            </td>
            <td style="width: 15%">&nbsp;</td>
            <td style="width: 10%">&nbsp;</td>

        </tr>
        <tr>
            <td colspan="6">
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%">
                </rsweb:ReportViewer>
            </td>
           <td style="width: 10%">&nbsp;</td>

        </tr>
        <tr>
            <td colspan="2">
                &nbsp;</td>
            <td style="width: 15%">&nbsp;</td>
            <td style="width: 15%">&nbsp;</td>
            <td style="width: 15%">
                &nbsp;</td>
            <td style="width: 15%">&nbsp;</td>
            <td style="width: 10%">&nbsp;</td>

        </tr>
        <tr>
            <td style="width: 15%">
                <strong>
                    <asp:Button ID="btnPrint" runat="server" Text="Print" Width="82px" Visible="False" />
                </strong>
            </td>
            <td style="width: 15%">

                <strong>
                    <asp:Button ID="btnExcel" runat="server" Text="Export to Excel" Width="125px" Visible="False" />
                </strong>
            </td>
            <td style="width: 15%">&nbsp;</td>
            <td style="width: 15%">&nbsp;</td>
            <td style="width: 15%">
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            </td>
            <td style="width: 15%">&nbsp;</td>
            <td style="width: 10%">&nbsp;</td>

        </tr>
        <tr>
            <td style="width: 15%">&nbsp;</td>
            <td style="width: 15%">&nbsp;</td>
            <td style="width: 15%">&nbsp;</td>
            <td style="width: 15%">&nbsp;</td>
            <td style="width: 15%">&nbsp;</td>
            <td style="width: 15%">&nbsp;</td>
            <td style="width: 10%">&nbsp;</td>

        </tr>
        <tr>
            <td style="width: 15%">&nbsp;</td>
            <td style="width: 15%">&nbsp;</td>
            <td style="width: 15%">&nbsp;</td>
            <td style="width: 15%">&nbsp;</td>
            <td style="width: 15%">&nbsp;</td>
            <td style="width: 15%">&nbsp;</td>
            <td style="width: 10%">&nbsp;</td>

        </tr>
    </table>
  
</asp:Content>

