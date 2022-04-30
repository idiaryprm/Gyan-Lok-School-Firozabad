<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="PettyCashStatement.aspx.vb" Inherits="iDiary_V3.PettyCashStatement" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Heading" runat="server">
    Petty Cash Statements
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table border="0" width="100%" cellpadding="2" cellspacing="2">
        <tr>
            <td width="15%">Transaction Type</td>
            <td width="15%">
                <asp:DropDownList ID="cboTrans" runat="server" Width="120px">
                </asp:DropDownList>
            </td>
            <td width="15%">From Date</td>
            <td width="15%">
                <asp:TextBox ID="txtFrom" runat="server" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px" Width="102px"></asp:TextBox>
                <asp:CalendarExtender ID="txtFrom_CalendarExtender" runat="server"  Format="dd/MM/yyyy" TargetControlID="txtFrom">
                </asp:CalendarExtender>
                </td>
            <td width="15%">To Date</td>
            <td width="15%">
                <asp:TextBox ID="txtTo" runat="server" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px" Width="102px"></asp:TextBox>
                <asp:CalendarExtender ID="txtTo_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="txtTo">
                </asp:CalendarExtender>
                </td>
            <td width="10%">
                <asp:Button ID="btnView" runat="server" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px" Text="View" Width="70px" style="height: 24px" />
            </td>
        </tr>
        <tr>
            <td width="15%">&nbsp;</td>
            <td width="15%">&nbsp;</td>
            <td width="15%">&nbsp;</td>
            <td width="15%">&nbsp;</td>
            <td width="15%">&nbsp;</td>
            <td width="15%">&nbsp;</td>
            <td width="10%">&nbsp;</td>
        </tr>
        <tr>
            <td colspan="7">
                <asp:Label ID="lblStatus" runat="server" Font-Bold="True" ForeColor="Navy"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="7" align="center">
            <div id="gvDiv" align="left">

                <asp:Label ID="lblSchoolName" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
                <br />
                <asp:Label ID="lblTitle" runat="server" Font-Bold="True" Font-Size="Small"></asp:Label>
                <br />
                <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" Width="100%" DataSourceID="SqlDataSource1" ShowFooter="True">
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
                <br />
                <asp:Literal ID="LitBalance" runat="server"></asp:Literal>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT * FROM [rptPettyCashStatements]"></asp:SqlDataSource>
            </div>
            </td>
        </tr>
        <tr>
            <td width="15%" colspan="6">
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            </td>
            <td width="10%">&nbsp;</td>
        </tr>
        <tr>
            <td colspan="7">
                <asp:Button ID="btnPrint" runat="server" Text="Print" Width="76px" />
                &nbsp;&nbsp;
                <asp:Button ID="btnExcel" runat="server" Text="Export to Excel" Width="125px" />
                </td>
        </tr>
    </table>
</asp:Content>
