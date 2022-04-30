<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="PettyStatement.aspx.vb" Inherits="iDiary_V3.PettyStatement" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Heading" runat="server">
    Ledger Statement
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table border="0" width="100%">
        <tr>
            <td style="width: 19%">
                Head</td>
            <td style="width: 25%">
                <asp:DropDownList ID="cboHead" runat="server" class="form-control1">
                </asp:DropDownList>
                </td>
            <td style="width: 10%">
                &nbsp;</td>
            <td width="20%">
                <asp:RadioButton ID="rbPayment" runat="server" Checked="True" GroupName="PV" Text="Payment" />
&nbsp;
                <asp:RadioButton ID="rbReceive" runat="server" GroupName="PV" Text="Receipt" />
            </td>
            <td width="20%">
                &nbsp;</td>
        </tr>
      <%--  <tr>
            <td style="width: 23%">
                <asp:RadioButton ID="optMonthwise" runat="server" Text="Month-wise" 
                    AutoPostBack="True" GroupName="G1" />
            </td>
            <td width="10%">
                Month
            </td>
            <td style="width: 25%">
                <asp:DropDownList ID="cboMonth" runat="server" Width="124px">
                </asp:DropDownList>
            </td>
            <td style="width: 10%">
                Year
            </td>
            <td width="20%">
                <asp:TextBox ID="txtYear" runat="server" BorderWidth="1px"></asp:TextBox>
            </td>
            <td width="20%">
                &nbsp;</td>
        </tr>--%>
        <tr>
            <td style="height: 24px; width: 19%;">
                Date From</td>
            <td style="width: 25%; height: 24px;">
                <asp:TextBox ID="txtFrom" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" placeholder="dd/mm/yyyy" Width="120px"></asp:TextBox>
                <asp:CalendarExtender ID="txtFrom_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFrom">
                </asp:CalendarExtender>
            </td>
            <td style="width: 10%; height: 24px;">
                Date To</td>
            <td width="20%" style="height: 24px">
                <asp:TextBox ID="txtTo" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" placeholder="dd/mm/yyyy" Width="120px"></asp:TextBox>
                <asp:CalendarExtender ID="txtTo_CalendarExtender" runat="server" DefaultView="Days" Format="dd/MM/yyyy" TargetControlID="txtTo">
                </asp:CalendarExtender>
            </td>
            <td width="20%" style="height: 24px">
                </td>
        </tr>
<%--        <tr>
            <td style="width: 23%">
                <asp:RadioButton ID="optDaywise" runat="server" Text="Day-wise with Student Details" 
                    AutoPostBack="True" GroupName="G1" />
            </td>
            <td width="10%">
                Date</td>
            <td style="width: 25%">
                <asp:TextBox ID="txtDate" runat="server" BorderWidth="1px"></asp:TextBox>
                <asp:CalendarExtender ID="txtDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDate">
                </asp:CalendarExtender>
            </td>
            <td style="width: 10%">
                &nbsp;</td>
            <td width="20%">
                &nbsp;</td>
            <td width="20%">
                &nbsp;</td>
        </tr>--%>
        <tr>
            <td colspan="3">
                <asp:Label ID="lblStatus" runat="server" Font-Bold="True"></asp:Label>
                <asp:Button ID="btnView" runat="server" Text="View" Width="76px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Visible="False" />
            </td>
            <td width="20%">
                <asp:Button ID="btnDetail" runat="server" Text="Generate Report" Width="129px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
            </td>
            <td width="20%">
                <asp:Button ID="btnBalanceSheet" runat="server" Text="Balance Sheet" Width="129px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
            </td>
        </tr>
        <tr>
            <td colspan="5">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="5">
            <div id="gvDiv" align="center">
                <asp:Label ID="lblSchoolName" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
                <br />
                <asp:Label ID="lblTitle" runat="server" Font-Bold="True" Font-Size="Small"></asp:Label>
                <br />
            </div>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" 
                    SelectCommand="SELECT * FROM [rptHeadwise]"></asp:SqlDataSource>
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="921px">
                </rsweb:ReportViewer>
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            </td>
        </tr>
        <tr>
            <td colspan="5">
                <asp:Button ID="btnPrint" runat="server" Text="Print" Width="76px" />
                &nbsp;&nbsp;
                <asp:Button ID="btnExcel" runat="server" Text="Export to Excel" Width="125px" />
                <br />
            </td>
        </tr>
    </table>
</asp:Content>
