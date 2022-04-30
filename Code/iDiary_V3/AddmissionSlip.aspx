<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AddmissionSlip.aspx.vb" Inherits="iDiary_V3.AddmissionSlip" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            width: 475px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <br />
        <table class="auto-style1">
            <tr>
                <td class="auto-style2">ADMISSION SLIP</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2"><rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" Height="535px" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="913px">
            <LocalReport ReportEmbeddedResource="iDiary_V3.rptAddmissionSlip.rdlc" ReportPath="rptAddmissionSlip.rdlc">
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="DataSet1" />
                </DataSources>
            </LocalReport>
        </rsweb:ReportViewer></td>
                <td><rsweb:ReportViewer ID="ReportViewer2" runat="server" Font-Names="Verdana" Font-Size="8pt" Height="450px" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Visible="False" Width="164px">
            <LocalReport ReportEmbeddedResource="iDiary_V3.rptBonafied_Cert.rdlc" ReportPath="rptAddmissionSlip.rdlc">
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="DataSet1" />
                </DataSources>
            </LocalReport>
        </rsweb:ReportViewer></td>
            </tr>
        </table>
        
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData" TypeName="iDiaryDataSetTableAdapters.vw_StudentTableAdapter"></asp:ObjectDataSource>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    </div>
    </form>
</body>
</html>
