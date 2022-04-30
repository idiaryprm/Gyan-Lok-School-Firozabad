<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/StudentReport.master" CodeBehind="StudentStrength.aspx.vb" Inherits="iDiary_V3.StudentStrength" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<%--<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="SubHeading" Runat="Server">
    Student Attendance Report 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ReportContent" Runat="Server">
      <%--<br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />   --%> 
    <table class="table">
        <tr>
            <td style="width: 28%">
                <strong>School</strong></td>
            <td colspan="2">
                <asp:DropDownList ID="cboSchoolName" runat="server" AutoPostBack="true" CssClass="Dropdown" Width="300px">
                </asp:DropDownList>
            </td>
            <td width="15%">
                <b>Class</b></td>
            <td width="15%">
                <asp:DropDownList ID="cboClass" runat="server" AutoPostBack="True" 
                    CssClass="Dropdown">
                </asp:DropDownList>
            </td>
            <td width="15%">
                &nbsp;</td>
            <td width="10%">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 28%">
                <b>Section</b></td>
            <td width="15%">
                <asp:DropDownList ID="cboSection" runat="server" CssClass="Dropdown">
                </asp:DropDownList>
            </td>
            <td width="15%">
                &nbsp;</td>
            <td width="15%">
                <b>Status</b></td>
            <td width="15%">
                <asp:DropDownList ID="cboStatus" runat="server" CssClass="Dropdown">
                </asp:DropDownList>
            </td>
            <td width="15%">
                <asp:Button ID="btnShow" runat="server" text="Generate Report" CssClass="btn btn-primary" />
            </td>
            <td width="10%">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="20%" colspan="6">
                <asp:Label ID="lblStatus" runat="server" ForeColor="Navy"></asp:Label>
            </td>
            <td width="5%">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="20%" colspan="7" style="width: 25%">
                    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%">
                        
</rsweb:ReportViewer>
            </td>
        </tr>
        <tr>
            <td colspan="7" style="margin-left: 80px">
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
            </td>
        </tr>
        </table>

</asp:Content>
