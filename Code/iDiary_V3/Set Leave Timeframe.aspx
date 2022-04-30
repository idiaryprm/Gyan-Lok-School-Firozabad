<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/PayrollMaster.master" CodeBehind="Set Leave Timeframe.aspx.vb" Inherits="iDiary_V3.Set_Leave_Timeframe" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SubHeading" runat="server">
    Define Duration for Leave Calculation
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CertificateContent" runat="server">
    <table width="100%" cellpadding="2" cellspacing="2" border="0">
        <tr>
            <td width="25%">Start Date</td>
            <td width="25%">
                <asp:TextBox ID="txtStart" runat="server" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                <asp:CalendarExtender ID="txtStart_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="txtStart">
                </asp:CalendarExtender>
            </td>
            <td width="50%">&nbsp;</td>
        </tr>
        <tr>
            <td width="25%">End Date</td>
            <td width="25%">
                <asp:TextBox ID="txtEnd" runat="server" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                <asp:CalendarExtender ID="txtEnd_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="txtEnd">
                </asp:CalendarExtender>
            </td>
            <td width="50%">&nbsp;</td>
        </tr>
        <tr>
            <td width="25%">&nbsp;</td>
            <td width="25%">
                &nbsp;</td>
            <td width="50%">&nbsp;</td>
        </tr>
        <tr>
            <td width="25%">&nbsp;</td>
            <td width="25%">
                <asp:Button ID="btnSave" runat="server" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px" Text="Save" Width="76px" />
            </td>
            <td width="50%">
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            </td>
        </tr>
    </table>
</asp:Content>
