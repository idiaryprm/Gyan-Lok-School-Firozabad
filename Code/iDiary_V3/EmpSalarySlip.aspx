<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/PayrollTransaction.master" CodeBehind="EmpSalarySlip.aspx.vb" Inherits="iDiary_V3.SalarySlip" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SubHeading" runat="server">
    Generate Salary Slip
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CertificateContent" runat="server">
    <table class="table">
        <tr>
            <td width="30%" valign="top">
                <asp:RadioButton ID="optSpecific" runat="server" GroupName="R1" Text="Employee-wise" Font-Bold="True" Checked="True" Visible="False" />
                <br />
                Employee Name
                <br />
                <asp:DropDownList ID="cboEmpName" runat="server" CssClass="Dropdown"></asp:DropDownList>
                <br /><br />
                <asp:RadioButton ID="optAll" runat="server" GroupName="R1" Text="All Employees" Font-Bold="True" Visible="False" />
                <br /><br />
                <strong>Month
                </strong>
                <br />
                <asp:DropDownList ID="cboMonth" runat="server" CssClass="Dropdown"></asp:DropDownList>
                <br /><br />
                <strong>Year</strong>
                <br />
                <asp:DropDownList ID="cboYear" runat="server" CssClass="Dropdown"></asp:DropDownList>
                <br />
                <br />
                <br />
                <asp:Button ID="btnGenerate" runat="server" Text="Generate" CssClass="btn btn-primary" />
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
            </td>

            <td colspan="70%">
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                    <LocalReport ReportEmbeddedResource="rptSalarySlip.rdlc" ReportPath="Report/rptSalarySlip.rdlc">
                        <DataSources>
                            <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="DataSet1" />
                            <rsweb:ReportDataSource DataSourceId="ObjectDataSource2" Name="DataSet2" />
                            <rsweb:ReportDataSource DataSourceId="ObjectDataSource3" Name="DataSet3" />
                        </DataSources>
                    </LocalReport>
                </rsweb:ReportViewer>
                <asp:ObjectDataSource ID="ObjectDataSource3" runat="server" SelectMethod="GetData" TypeName="iDiary_V3.iDiaryDataSetTableAdapters.rptSalarySlipDeductionPartTableAdapter"></asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="GetData" TypeName="iDiary_V3.iDiaryDataSetTableAdapters.rptSalarySlipEarningPartTableAdapter"></asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData" TypeName="iDiary_V3.iDiaryDataSetTableAdapters.rptSalarySlipHeadPartTableAdapter"></asp:ObjectDataSource>
            </td>


        </tr>
    </table>

</asp:Content>
