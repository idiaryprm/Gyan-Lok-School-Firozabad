
<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="BusFeeCollectionReport.aspx.vb" Inherits="iDiary_V3.BusFeeCollectionReport" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Heading" runat="server">
    Bus Fee Collection Report 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <%-- <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
        <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
        <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
        <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />--%>
    <div class="col_3" style="margin-top: 20px;" id="panelEnquiryNo" runat="server">
        <div class="col-md-3 widget widget1" style="width: 100%">
            <div class="r3_counter_box">
    <table border="0">
        <tr>
            <td>School Name</td>
            <td colspan="3">
                <asp:DropDownList ID="cboSchoolName" runat="server" AutoPostBack="true" CssClass="Dropdown" Width="300px">
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>Class</td>
            <td>
                <asp:DropDownList ID="cboClass" runat="server" AutoPostBack="True" 
                    CssClass="Dropdown">
                </asp:DropDownList>
            </td>
            <td>Section</td>
            <td>
                <asp:DropDownList ID="cboSection" runat="server" CssClass="Dropdown">
                </asp:DropDownList>
            </td>
            <td>
                <asp:CheckBox ID="chkregno" Text="Reg. No" AutoPostBack="true"  runat="server" /></td>
            <td>
               <asp:TextBox ID="txtregistrationno" runat="server" CssClass="textbox" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Payment Mode</td>
            <td>
               <asp:DropDownList ID="cboMode" runat="server" CssClass="Dropdown">
                                    </asp:DropDownList>
                </td>
            <td>Status</td>
            <td>
                <asp:DropDownList ID="cboStatus" runat="server" CssClass="Dropdown">
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>From</td>
            <td>
                <asp:TextBox ID="txtFrom" runat="server" CssClass="textbox"></asp:TextBox>
                <asp:CalendarExtender ID="txtFrom_CalendarExtender" runat="server" TargetControlID="txtFrom" Format="dd/MM/yyyy">
                </asp:CalendarExtender>
            </td>
            <td>To</td>
            <td>
                <asp:TextBox ID="txtTo" runat="server" CssClass="textbox"></asp:TextBox>
                <asp:CalendarExtender ID="txtTo_CalendarExtender" runat="server" TargetControlID="txtTo" DefaultView="Days" Format="dd/MM/yyyy">
                </asp:CalendarExtender>
            </td>
            <td>
                &nbsp;</td>
            <td>
                <asp:Button ID="btnView" runat="server" Text="Generate Report" CssClass="btn btn-primary" />
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:Label ID="lblStatus" runat="server" Font-Bold="True" Font-Italic="False" ForeColor="Navy"></asp:Label>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td >
                
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            </td>
            
        </tr>
        <tr>
            <td colspan="6">
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="100%">
                    <LocalReport ReportEmbeddedResource="rptFeeCollection.rdlc" ReportPath="rptFeeCollection.rdlc">
                        <DataSources>
                            <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="dsFeeCollection" />
                        </DataSources>
                    </LocalReport>
                </rsweb:ReportViewer>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="iDiary_V3.iDiaryDataSetTableAdapters.rptFeeCollectionTableAdapter">
                    <InsertParameters>
                        <asp:Parameter Name="FeeDepositDate" Type="DateTime" />
                        <asp:Parameter Name="SID" Type="Int32" />
                        <asp:Parameter Name="FeeBookNo" Type="String" />
                        <asp:Parameter Name="SName" Type="String" />
                        <asp:Parameter Name="ClassName" Type="String" />
                        <asp:Parameter Name="SecName" Type="String" />
                        <asp:Parameter Name="FeeAmount" Type="Double" />
                    </InsertParameters>
                </asp:ObjectDataSource>
            </td>
        </tr>
        </table>
           </div>
        </div>
        <div class="clearfix"></div>
    </div>  
  
</asp:Content>

