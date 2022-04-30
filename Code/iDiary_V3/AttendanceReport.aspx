<%@ Page Language="VB" MasterPageFile="~/StudentReport.master" AutoEventWireup="false" Inherits="iDiary_V3.Admin_AttendanceReport" title="Untitled Page" Codebehind="AttendanceReport.aspx.vb" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SubHeading" Runat="Server">
    Student Attendance Report 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ReportContent" Runat="Server">
   <%-- <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
        <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
        <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
        <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />--%>
    <table class="table">
        <tr>
            <td colspan="4">

                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

                <table border="0" cellpadding="1" cellspacing="1" width="100%">
                    <tr>
                        <td style="font-size: 14px; width: 190px;">
                            <b>School</b></td>
                        <td colspan="2">
                            <asp:DropDownList ID="cboSchoolName" runat="server" CssClass="Dropdown" Width="300px" AutoPostBack="true"></asp:DropDownList>
                        </td>

                    </tr>
                    <tr>
                        <td style="font-size: 14px; width:190px;">
                            <b>Class</b></td>
                        <td style="text-decoration: underline; font-size: 14px; width: 178px;">
                            <asp:DropDownList ID="cboClass" runat="server" CssClass="Dropdown"
                                AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 408px">
                            <b>Section</b></td>
                        <td style="width: 11%">
                            <asp:DropDownList ID="cboSection" runat="server" CssClass="Dropdown">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-size: 14px; width: 190px;">
                            <b>From Date</b></td>
                        <td style="font-size: 14px; width: 178px;">
                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="textbox"></asp:TextBox>
                            <asp:CalendarExtender ID="txtDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFromDate"></asp:CalendarExtender>
                            <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999" MaskType="Date" TargetControlID="txtFromDate" PromptCharacter="_"></asp:MaskedEditExtender>
                        </td>
                        <td style="width: 408px; height: 20px"></td>
                        <td style="width: 11%; height: 20px"></td>
                    </tr>
                    <tr>
                        <td style="font-size: 14px; width: 190px;">
                            <b>To Date</b></td>
                        <td style="font-size: 14px; width: 178px;">
                            <asp:TextBox ID="txtToDate" runat="server" CssClass="textbox"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtToDate"></asp:CalendarExtender>
                            <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999" MaskType="Date" TargetControlID="txtToDate" PromptCharacter="_"></asp:MaskedEditExtender>
                        </td>
                        <td style="width: 408px; height: 20px"></td>
                        <td style="width: 11%; height: 20px">
                            <asp:Button ID="btnReport" runat="server" CssClass="btn btn-primary" Width="140px" Text="Generate Report" />
                        </td>
                    </tr>
                    <tr>
                        <td style="font-size: 14px; height: 20px; width: 190px;">
                            <asp:Button ID="btnRoster" runat="server" CssClass="btn btn-primary" Width="140px" Text="Attendance Roster" />
                        </td>
                        <td style="text-decoration: underline; font-size: 14px; height: 20px; width: 178px;">
                            <asp:DropDownList ID="cboShift" runat="server" CssClass="Dropdown" Visible="false">
                                <asp:ListItem>Morning</asp:ListItem>
                                <asp:ListItem>Evening</asp:ListItem>
                            </asp:DropDownList>
                        </td>

                        <td style="width: 408px">&nbsp;</td>
                        <td style="width: 11%">
                            <asp:Button ID="btnSummaryReport" runat="server" CssClass="btn btn-primary" Width="140px" Text="Summary Report" />
                        </td>
                    </tr>
                </table>

            </td>

        </tr>

        <tr>
            <td style="font-size: 14px;" colspan="4">

                <asp:GridView ID="gvAttendance" runat="server" OnDataBound="gvAttendance_DataBound" AutoGenerateColumns="False" CssClass="Grid" DataSourceID="SqlDataSource1" Width="712px">
                    <Columns>
                        <asp:BoundField DataField="RegNo" HeaderText="RegNo" SortExpression="RegNo"></asp:BoundField>
                        <asp:BoundField DataField="SNAme" HeaderText="Student" SortExpression="SNAme"></asp:BoundField>
                        <asp:BoundField DataField="IsPresentM" HeaderText="Absent" SortExpression="IsPresentM"></asp:BoundField>
                        <asp:BoundField DataField="MobNo" HeaderText="Mobile No" SortExpression="MobNo"></asp:BoundField>
                        <asp:BoundField DataField="ClassName" HeaderText="Class" SortExpression="ClassName"></asp:BoundField>
                        <asp:BoundField DataField="SecName" HeaderText="Section" SortExpression="SecName"></asp:BoundField>
                        <asp:BoundField DataField="AttDate" HeaderText="Attendance Date" DataFormatString="{0:dd/MM/yyyy}" SortExpression="AttDate"></asp:BoundField>
                    </Columns>

                </asp:GridView>
                <strong>
                    <br />
                    <asp:Label ID="lblAbsent" runat="server"></asp:Label>
                    <br />
                    <asp:Label ID="lblPresent" runat="server"></asp:Label>
                    <br />
                </strong>
                <asp:Label ID="lblStatus" runat="server" ForeColor="Navy"
                    Style="font-weight: 700"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="font-size: 14px;">
                <strong>
                    <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="btn btn-primary" Visible="False" />
                </strong>
            </td>
            <td style="font-size: 14px;">
                <strong>
                    <asp:Button ID="btnExcel" runat="server" Text="Export to Excel" CssClass="btn btn-primary" Visible="False" />
                </strong>
            </td>
            <td width="20%" style="width: 0%">&nbsp;</td>
            <td width="20%" style="width: 10%">&nbsp;</td>
        </tr>
        <tr>
            <td style="font-size: 14px;">&nbsp;</td>
            <td style="font-size: 14px;">&nbsp;</td>
            <td width="20%" style="width: 0%">&nbsp;</td>
            <td width="20%" style="width: 10%">&nbsp;</td>
        </tr>
        <tr>
            <td style="font-size: 14px;" colspan="4">
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="246px" Width="700px">
                </rsweb:ReportViewer>
            </td>
        </tr>
        <tr>
            <td style="width: 8%">&nbsp;</td>
            <td style="width: 2%"></td>
            <td width="20%" colspan="2">
                <asp:ListBox ID="lstSID" runat="server" Height="32px" Visible="False"></asp:ListBox>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="Select RegNo,SNAme, IsPresentM,IsPresentE, MobNo From vw_Attendance"></asp:SqlDataSource>
            </td>
        </tr>
    </table>
</asp:Content>

