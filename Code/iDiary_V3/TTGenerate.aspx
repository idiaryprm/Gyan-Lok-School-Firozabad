<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="TTGenerate.aspx.vb" Inherits="iDiary_V3.TTGenerate" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Heading" runat="server">
     Time Table Generation
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col_3" style="margin-top: 20px;" id="panelEnquiryNo" runat="server">
        <div class="col-md-3 widget widget1" style="width: 100%">
            <div class="r3_counter_box">
                <table class="table">
                    <tr>
                        <td style="width: 10%">
                            <asp:Button ID="btnGenerate" runat="server" Text="Generate" CssClass="btn btn-sm btn-primary"/>
                        </td>
                        <td style="width: 20%">&nbsp;</td>
                        <td style="width: 10%">
                            <asp:Button ID="btnView" runat="server" Text="View" CssClass="btn btn-sm btn-primary"/>
                        </td>
                        <td style="width: 20%">&nbsp;</td>
                        <td style="width: 10%">&nbsp;</td>
                        <td style="width: 20%">
                            <asp:ScriptManager ID="ScriptManager2" runat="server">
                            </asp:ScriptManager>
                        </td>
                        <td style="width: 10%">&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 10%">&nbsp;</td>
                        <td style="width: 20%">&nbsp;</td>
                        <td style="width: 10%">&nbsp;</td>
                        <td style="width: 20%">&nbsp;</td>
                        <td style="width: 10%">&nbsp;</td>
                        <td style="width: 20%">&nbsp;</td>
                        <td style="width: 10%">&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 10%">&nbsp;</td>
                        <td colspan="5" align="left">
                            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%">
                            </rsweb:ReportViewer>
                        </td>
                        <td style="width: 10%">&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 10%">&nbsp;</td>
                        <td style="width: 20%">&nbsp;</td>
                        <td style="width: 10%">&nbsp;</td>
                        <td style="width: 20%">&nbsp;</td>
                        <td style="width: 10%">&nbsp;</td>
                        <td style="width: 20%">&nbsp;</td>
                        <td style="width: 10%">&nbsp;</td>
                    </tr>
                </table>
                <br />
                <br />
                <br />
            </div>
        </div>
        <div class="clearfix"></div>
    </div>
</asp:Content>
