<%@ Page Language="VB" MasterPageFile="~/AdminMaster.master" AutoEventWireup="false" Inherits="iDiary_V3.Admin_StudentLeaveReport" title="Untitled Page" Codebehind="StudentLeaveReport.aspx.vb" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminMasterContents" Runat="Server">
    
    <table class="table">
        <tr>
            <td style="text-decoration: underline; font-size: 14px;" colspan="2">
                <strong>STUDENT&#39;S LEAVE REPORT</strong></td>
            <td style="text-decoration: underline; font-size: 14px; width: 106px;">
                </td>
            <td style="text-decoration: underline; font-size: 14px;">
                </td>
            <td style="text-decoration: underline; font-size: 14px;">
                </td>
            <td style="text-decoration: underline; font-size: 14px;">
                </td>
        </tr>
        <tr>
            <td style="text-decoration: underline; font-size: 14px; width: 50px;">
                <b>Date</b></td>
            <td>
                <asp:TextBox ID="txtDate" runat="server" BorderStyle="Solid" BorderWidth="1px" 
                    Width="115px"></asp:TextBox>
            </td>
            <td style="width: 106px">
                <b>Class</b></td>
            <td>
                <asp:DropDownList ID="cboClass" runat="server" Width="120px" 
                    AutoPostBack="True">
                </asp:DropDownList>
            </td>
            <td>
                <b>Section</b></td>
            <td>
                <asp:DropDownList ID="cboSection" runat="server" Width="120px" 
                    AutoPostBack="True">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="lblStatus" runat="server" ForeColor="Navy" 
                    style="font-weight: 700"></asp:Label>
            </td>
            <td style="width: 106px">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td valign="top" colspan="2">
                <asp:Button ID="btnReport" runat="server" Text="Generate Report" CssClass="btn btn-primary" />
                </td>
            <td valign="top" style="width: 106px">
                &nbsp;</td>
            <td valign="top">
                &nbsp;</td>
            <td valign="top">
                &nbsp;</td>
            <td valign="top">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="6">
                <asp:GridView ID="gvAttendance" runat="server" AutoGenerateColumns="False" BackColor="White"  CellPadding="1" GridLines="Horizontal" DataSourceID="SqlDataSource1" CssClass="Grid" Width="100%">
                                        <Columns>
                                            <asp:BoundField DataField="RegNo" HeaderText="RegNo" >
                                            <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="SName" HeaderText="Student" >
                                            <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="fromDate" HeaderText="From" DataFormatString="{0:dd/MM/yyyy}" >
                                            <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ToDate" HeaderText="To" DataFormatString="{0:dd/MM/yyyy}" >
                                            <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Reason" HeaderText="Reason" />
                                            <asp:BoundField DataField="Message" HeaderText="Message" />
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
                </td>
        </tr>
        <tr>
            <td colspan="2">
                <strong>
                <asp:Label ID="lblAbsent" runat="server"></asp:Label>
                </strong>
            </td>
            <td style="width: 106px">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="2">
                <strong>
                <asp:Label ID="lblPresent" runat="server"></asp:Label>
                </strong>
            </td>
            <td style="width: 106px">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;</td>
            <td style="width: 106px">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 50px">
                <strong>
                <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="btn btn-primary" Visible="False" />
                </strong>
            </td>
            <td>
                <strong>
                <asp:Button ID="btnExcel" runat="server" Text="Export to Excel" CssClass="btn btn-primary" Visible="False" />
                </strong>
            </td>
            <td style="width: 106px">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 50px">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td style="width: 106px">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="6">
                <asp:ListBox ID="lstSID" runat="server" Height="32px" Visible="False">
                </asp:ListBox>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="Select RegNo,SNAme,FromDate,ToDate,Message,Reason From vw_StudentLeave"></asp:SqlDataSource>
              <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <br />
                <asp:CalendarExtender ID="txtDate_CalendarExtender" runat="server" Format="dd/MM/yyyy"  TargetControlID="txtDate">
</asp:CalendarExtender>
            </td>
        </tr>
    </table>
</asp:Content>

