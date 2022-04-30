<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="StudentEnquiryCompare.aspx.vb" Inherits="iDiary_V3.StudentEnquiryCompare" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Heading" runat="server">
    Admission Report 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <%--  <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />--%>

    <div class="col_3" style="margin-top: 20px;" id="panelEnquiryNo" runat="server">
        <div class="col-md-3 widget widget1" style="width: 100%">
            <div class="r3_counter_box">
                  <table class="table">
        <tr>
            <td style="width: 23%">
                Academic Session</td>
            
            <td colspan="4">
                <asp:CheckBoxList ID="cblAcademicSession" Width="300px" runat="server" RepeatColumns="3" RepeatDirection="Horizontal">
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr>
            <td style="width: 23%">
                Comparison Factor(greater Than)</td>
         
            <td style="width: 25%">
                <asp:TextBox ID="txtComFactor" runat="server" TextMode="Number">1</asp:TextBox>
            </td>
            <td class="auto-style3">
                <asp:Button ID="btnCompare" runat="server" Text="Compare" CssClass="btn btn-primary" />
            </td>
            <td width="20%">
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                &nbsp;</td>
            <td width="20%">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 23%; height: 23px;">
                &nbsp;</td>
            
            <td style="height: 23px">
                &nbsp;</td>
            <td class="auto-style4">
                &nbsp;</td>
            <td style="height: 23px">
                &nbsp;</td>
            <td style="height: 23px">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="5">
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="80%" Visible="False">
                    <LocalReport ReportEmbeddedResource="rptFeeCollection.rdlc" ReportPath="rptFeeCollection.rdlc">
                        <DataSources>
                            <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="dsFeeCollection" />
                        </DataSources>
                    </LocalReport>
                </rsweb:ReportViewer>
            </td>
        </tr>
        </table>
                </div>
        </div>
        <div class="clearfix"></div>
    </div>
  
</asp:Content>
<asp:Content ID="Content3" runat="server" contentplaceholderid="head">
    <style type="text/css">
        .auto-style3 {
            width: 18%;
        }
        .auto-style4 {
            height: 23px;
            width: 18%;
        }
        </style>
</asp:Content>

