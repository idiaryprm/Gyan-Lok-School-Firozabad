<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="LibraryIssueReturnReport.aspx.vb" Inherits="iDiary_V3.LibraryIssueReturnReport" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Heading" runat="server">
    Library Issue / Return Transaction Report
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="col_3" style="margin-top: 20px;" id="panelEnquiryNo" runat="server">
        <div class="col-md-3 widget widget1" style="width: 100%">
            <div class="r3_counter_box">
                <table class="table">
                    <tr>
                        <td style="width: 176px"><strong>
                            <h3>Select Category</h3>
                        </strong></td>
                        <td style="width: 197px">

                            <asp:RadioButton ID="rbBook" runat="server" Checked="True" Font-Bold="True" GroupName="SeachCategory" Text="Book" />

                        </td>
                        <td style="width: 75px">&nbsp;</td>
                        <td style="width: 192px">

                            <asp:RadioButton ID="rbMagazine" runat="server" Font-Bold="True" GroupName="SeachCategory" Text="Magazine" />
                        </td>
                        <td style="width: 52px">&nbsp;</td>
                        <td style="width: 161px">
                            <asp:RadioButton ID="rbDVD" runat="server" Font-Bold="True" GroupName="SeachCategory" Text="DVD/CD" />
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 176px">From: </td>
                        <td style="width: 197px">

                            <asp:TextBox ID="txtDateFrom" runat="server" CssClass="textbox"></asp:TextBox>
                            <asp:CalendarExtender ID="txtDateFrom_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDateFrom"></asp:CalendarExtender>

                        </td>
                        <td style="width: 75px">To: </td>
                        <td style="width: 192px">

                            <asp:TextBox ID="txtDateTo" runat="server" CssClass="textbox"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDateTo"></asp:CalendarExtender>
                        </td>
                        <td style="width: 52px">Type</td>
                        <td style="width: 161px">
                            <asp:DropDownList ID="cboType" runat="server" class="form-control1" CssClass="Dropdown">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem>Issue</asp:ListItem>
                                <asp:ListItem>Return</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="btnGenerate" runat="server" Text="Generate Report" Width="159px" CssClass="btn btn-primary"/>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 176px">&nbsp;</td>
                        <td style="width: 197px">&nbsp;</td>
                        <td style="width: 75px">&nbsp;</td>
                        <td style="width: 192px">&nbsp;</td>
                        <td colspan="3">&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="7">
                            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="931px">
                                <LocalReport ReportEmbeddedResource="iDiary_V3.rptLibraryTransaction.rdlc" ReportPath="rptLibraryTransaction.rdlc">
                                    <DataSources>
                                        <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="DataSet1" />
                                        <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="DataSet2" />
                                    </DataSources>
                                </LocalReport>
                            </rsweb:ReportViewer>
                            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData" TypeName="iDiaryDataSetTableAdapters.rptLibraryTransactionTableAdapter"></asp:ObjectDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="7">
                            <asp:Label ID="lblStatus" runat="server" Font-Bold="True" ForeColor="Navy"></asp:Label>
                            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="clearfix"></div>
    </div>
       

</asp:Content>
