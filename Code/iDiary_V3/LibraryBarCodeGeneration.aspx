<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="LibraryBarCodeGeneration.aspx.vb" Inherits="iDiary_V3.LibraryBarCodeGeneration" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Heading" runat="server">
    Bar Code Generation
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col_3" style="margin-top: 20px;" id="panelEnquiryNo" runat="server">
        <div class="col-md-3 widget widget1" style="width: 100%">
            <div class="r3_counter_box">
                <table class="table">
                    <tr>
                        <td style="width: 176px"><strong>
                            <h5 style="margin: 0px 0px 0px 0px">Select Category</h5>
                        </strong></td>
                        <td style="width: 143px">

                            <asp:RadioButton ID="rbBook" runat="server" Checked="True" Font-Bold="True" GroupName="SeachCategory" Text="Book" />

                        </td>
                        <td style="width: 146px">

                            <asp:RadioButton ID="rbMagazine" runat="server" Font-Bold="True" GroupName="SeachCategory" Text="Magazine" />
                        </td>
                        <td style="width: 192px">

                            <asp:RadioButton ID="rbDVD" runat="server" Font-Bold="True" GroupName="SeachCategory" Text="DVD/CD" />
                        </td>
                        <td style="width: 52px">&nbsp;</td>
                        <td style="width: 161px">&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 15%">
                            <asp:RadioButton ID="optRange" runat="server" Text="Range-wise"
                                GroupName="BarType" />
                        </td>
                        <td style="width: 143px">Start Accession Number</td>
                        <td style="width: 146px">
                            <asp:TextBox ID="txtStartNo" runat="server" CssClass="textbox"></asp:TextBox></td>
                        <td style="width: 25%">Last Accession Number</td>
                        <td width="15%">
                            <asp:TextBox ID="txtLastNo" runat="server" CssClass="textbox"></asp:TextBox></td>
                        <td width="5%">&nbsp;</td>
                    </tr>

                    <tr>
                        <td style="width: 15%; height: 24px;">
                            <asp:RadioButton ID="optInd" runat="server" Text="Individual"
                                GroupName="BarType" />
                        </td>
                        <td style="height: 24px;" colspan="2">
                            <asp:TextBox ID="txtBarCodes" runat="server" Width="292px" CssClass="textbox"></asp:TextBox>
                        </td>
                        <td width="45%" colspan="3" style="height: 24px">Mention barcodes seperated by comma.
                        </td>
                    </tr>


                    <tr>
                        <td style="width: 15%">&nbsp;</td>
                        <td colspan="2">&nbsp;</td>
                        <td width="45%" colspan="3">&nbsp;</td>
                    </tr>


                    <tr>
                        <td style="width: 15%">&nbsp;</td>
                        <td colspan="2">
                            <asp:Button ID="btnGenerate" runat="server" Text="Generate" CssClass="btn btn-primary" />
                        </td>
                        <td width="45%" colspan="3">
                            <asp:ScriptManager ID="ScriptManager1" runat="server">
                            </asp:ScriptManager>
                        </td>
                    </tr>


                    <tr>
                        <td style="width: 15%">&nbsp;</td>
                        <td colspan="5">
                            <asp:Label ID="lblStatus" runat="server" Font-Bold="True" ForeColor="Navy"></asp:Label>
                        </td>
                    </tr>

                </table>
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="100%" Visible="False">
                    <LocalReport ReportEmbeddedResource="rptLibraryBarCode.rdlc" ReportPath="rptLibraryBarCode.rdlc">
                        <DataSources>
                            <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="DataSet1" />
                        </DataSources>
                    </LocalReport>
                </rsweb:ReportViewer>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData" TypeName="iDiary_V3.iDiaryDataSetTableAdapters.rptLibraryBarCodesTableAdapter"></asp:ObjectDataSource>
                <br />
            </div>
        </div>
        <div class="clearfix"></div>
    </div>       
</asp:Content>
