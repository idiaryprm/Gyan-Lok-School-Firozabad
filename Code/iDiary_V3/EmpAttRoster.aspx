<%@ Page Language="VB" MasterPageFile="~/PayrollTransaction.master" AutoEventWireup="false" Inherits="iDiary_V3.EmpAttRoster" title="Untitled Page" Codebehind="EmpAttRoster.aspx.vb" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CertificateContent" Runat="Server">
    <div class="panel-heading" style="margin-top: -40px">
        <h4 class="panel-title">Attendance Report</h4>
    </div> 
    <br /> 
    <table>
        <tr>
            <td >
                <strong>Date From</strong></td>
            <td >

                <asp:TextBox ID="txtDateFrom" runat="server" CssClass="textbox"></asp:TextBox>
                <asp:CalendarExtender ID="txtDateFrom_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDateFrom"></asp:CalendarExtender>

            </td>
            <td >
                <b>&nbsp;Date To</b></td>
            <td >
                <asp:TextBox ID="txtDateTo" runat="server" CssClass="textbox"></asp:TextBox>
                <asp:CalendarExtender ID="txtDateTo_CalendarExtender0" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDateTo"></asp:CalendarExtender>
            </td>
            <td >
                <b>&nbsp;</b></td>
            <td >&nbsp;</td>
            <td style="width: 10%">&nbsp;</td>

        </tr>
        <tr>
            <td >
                <b>Category</b></td>
            <td >

                <asp:DropDownList ID="cboEmpCat" runat="server" Width="120px">
                </asp:DropDownList>

            </td>
            <td >
                <b>Status</b></td>
            <td >
                <asp:DropDownList ID="cboStatus" runat="server" Width="120px">
                </asp:DropDownList>
            </td>
            <td >

                <asp:Button ID="btnReport" runat="server" Text="Generate Report" class="btn btn-primary" />

            </td>
            <td >&nbsp;</td>
            <td style="width: 10%">&nbsp;</td>

        </tr>
        <tr>
            <td colspan="5">
                <asp:Label ID="lblStatus" runat="server" ForeColor="Navy"
                    Style="font-weight: 700"></asp:Label>
            </td>
            <td >&nbsp;</td>
            <td style="width: 10%">&nbsp;</td>

        </tr>
        <tr>
            <td colspan="5">&nbsp;</td>
            <td >&nbsp;</td>
            <td style="width: 10%">&nbsp;</td>

        </tr>
        <tr>
            <td colspan="7">
                <asp:GridView ID="gvAttendance" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4" GridLines="Horizontal" Width="483px" DataSourceID="SqlDataSource1">
                    <Columns>
                        <asp:BoundField DataField="EmpCode" HeaderText="Code" SortExpression="EmpCode"></asp:BoundField>
                        <asp:BoundField DataField="EmpName" HeaderText="Name" SortExpression="EmpName"></asp:BoundField>
                    </Columns>
                    <FooterStyle BackColor="White" ForeColor="#333333" />
                    <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="White" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F7F7F7" />
                    <SortedAscendingHeaderStyle BackColor="#487575" />
                    <SortedDescendingCellStyle BackColor="#E5E5E5" />
                    <SortedDescendingHeaderStyle BackColor="#275353" />
                </asp:GridView>
                <br />
            </td>

        </tr>
        <tr>
            <td colspan="2">
                <strong>
                    <asp:Label ID="lblPresent" runat="server"></asp:Label>
                </strong>
            </td>
            <td style="width: 15%">&nbsp;</td>
            <td style="width: 15%">&nbsp;</td>
            <td style="width: 15%">&nbsp;</td>
            <td style="width: 15%">&nbsp;</td>
            <td style="width: 10%">&nbsp;</td>

        </tr>
        <tr>
            <td colspan="2">
                <strong>
                    <asp:Label ID="lblAbsent" runat="server"></asp:Label>
                </strong>
            </td>
            <td style="width: 15%">&nbsp;</td>
            <td style="width: 15%">&nbsp;</td>
            <td style="width: 15%">
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT [EmpCode], [EmpName], [AttDate], [Att], [LeaveName] FROM [vw_employee_Attendance]"></asp:SqlDataSource>
            </td>
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

