<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/AdminMaster.master" CodeBehind="ViewLeaveRecords.aspx.vb" Inherits="iDiary_V3.ViewLeaveRecords" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminMasterContents" runat="server">
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <table width="100%" cellpadding="1" cellspacing="1" border="0" class="Table_Font">
        <tr>
            <td height="25" style="width: 17%">
                <asp:CheckBox ID="chkByAdminNo" runat="server" Text="Admission No." />
            </td>
            <td width="15%">
                <asp:TextBox ID="txtByAdminNo" runat="server" Width="130px" BorderWidth="1px"></asp:TextBox>
            </td>

            <td style="width: 18%">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:CheckBox ID="chkClass" runat="server" Text="Class" />
            </td>
            <td width="15%">
                <asp:DropDownList ID="cboClass" runat="server" Width="130px" 
                    AutoPostBack="True">
                </asp:DropDownList>
            </td>

            <td width="15%">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:CheckBox ID="chkSection" runat="server" Text="Section" />
            </td>
            <td width="15%">
                <asp:DropDownList ID="cboSection" runat="server" Width="130px">
                </asp:DropDownList>
            </td>
           
            <td width="10%" align="right">
                &nbsp;</td>
        </tr>
        <tr>
            <td height="25" style="width: 17%">
                <asp:CheckBox ID="chkAttendanceDate" runat="server" Text="Attendance Date" />
            &nbsp;From</td>
            <td width="15%">
                <asp:TextBox ID="txtDateFrom" runat="server" Width="130px" BorderWidth="1px" Height="20px"></asp:TextBox>
            </td>

            <td style="width: 18%">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                Attendance Date To</td>
            <td width="15%">
                <asp:TextBox ID="txtDateTo" runat="server" Width="130px" BorderWidth="1px"></asp:TextBox>
            </td>

            <td width="15%">
                &nbsp;</td>
            <td width="15%" style="margin-left: 40px">
                <asp:ImageButton ID="btnFind" runat="server" ImageUrl="~/images/search.png" 
                    style="height: 19px" />
            </td>
           
            <td width="10%" align="Left">
                &nbsp;</td>
        </tr>
        <tr>
            <td height="25" style="width: 17%">
                &nbsp;</td>
            <td width="15%">
                &nbsp;</td>

            <td style="width: 18%">
                &nbsp;</td>
            <td width="15%">
                &nbsp;</td>

            <td width="15%">
                &nbsp;</td>
            <td width="15%" style="margin-left: 40px">
                &nbsp;</td>
           
            <td width="10%" align="Left">
                &nbsp;</td>
        </tr>
        <tr>
            <td height="25" colspan="4" valign="top">
                <asp:GridView ID="gvAttendance" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4" DataSourceID="SqlDataSourceAttendance" GridLines="Horizontal">
                    <Columns>
                        <asp:BoundField DataField="RegNo" HeaderText="Admission No" SortExpression="RegNo" />
                        <asp:BoundField DataField="ClassRollno" HeaderText="Rollno" SortExpression="ClassRollno" />
                        <asp:BoundField DataField="SName" HeaderText="Student Name" SortExpression="SName" />
                        <asp:BoundField DataField="ClassName" HeaderText="Class" SortExpression="ClassName" />
                        <asp:BoundField DataField="SecName" HeaderText="Section" SortExpression="SecName" />
                        <asp:BoundField DataField="AttDate" HeaderText="Attendance Date" SortExpression="AttDate" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField DataField="IsPresent" HeaderText="Status" SortExpression="IsPresent" />
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

            <td width="15%" valign="top" colspan="2" style="width: 30%">
                <strong>
                <br />
                <asp:Label ID="lblPresent" runat="server"></asp:Label>
                <br />
                <br />
                <asp:Label ID="lblAbsent" runat="server"></asp:Label>
                </strong></td>
           
            <td width="10%" align="Left" valign="top">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="height: 25px;" colspan="7">
                <asp:SqlDataSource ID="SqlDataSourceAttendance" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT [RegNo], [ClassRollno], [SName], [ClassName], [SecName], [AttDate], [IsPresent] FROM [vw_Student_Attendance]"></asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td height="25" style="width: 17%">
                &nbsp;</td>
            <td width="15%">
                &nbsp;</td>

            <td style="width: 18%">
                &nbsp;</td>
            <td width="15%">
                &nbsp;</td>

            <td width="15%">
                &nbsp;</td>
            <td width="15%">
                &nbsp;</td>
           
            <td width="10%" align="right">
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>
