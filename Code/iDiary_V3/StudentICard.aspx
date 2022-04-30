<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="StudentICard.aspx.vb" Inherits="iDiary_V3.StudentICard" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Heading" runat="server">
    Student I-Card Generation
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />--%>
    <div class="col_3" style="margin-top: 20px;" id="panelEnquiryNo" runat="server">
        <div class="col-md-3 widget widget1" style="width: 100%">
            <div class="r3_counter_box">
                <table class="table">
                     <tr>
            <td width="16%">
                School Name</td>
            <td class="auto-style1" colspan="2">
                <asp:DropDownList ID="cboSchoolName" runat="server" AutoPostBack="true" CssClass="Dropdown" Width="300px" OnSelectedIndexChanged="cboSchoolName_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td width="16%" colspan="2" style="width: 32%">
                &nbsp;</td>
            <td width="16%">
                &nbsp;</td>
            <td width="4%">&nbsp;</td>
        </tr>
                     <tr>
            <td width="16%">
                <asp:RadioButton ID="optSpecific" runat="server" GroupName="G1" Text="Individual " />
            </td>
            <td class="auto-style1">
                <asp:Panel ID="PanelSpecific" runat="server" Width="130px">
                    <asp:TextBox ID="txtRegNo" runat="server" CssClass="textbox"></asp:TextBox>
                </asp:Panel>
            </td>
            <td width="16%">
                <asp:RadioButton ID="optClass" runat="server" GroupName="G1" Text="Class-wise" />
            </td>
            <td width="16%" colspan="2" style="width: 32%">
                <asp:Panel ID="PanelClass" runat="server" Width="280px">
                    <asp:DropDownList ID="cboClass" runat="server" Width="120px" AutoPostBack="True" 
                    CssClass="Dropdown">
                    </asp:DropDownList>
                    &nbsp;&nbsp;
                    <asp:DropDownList ID="cboSection" runat="server" Width="70px" CssClass="Dropdown">
                    </asp:DropDownList>
                </asp:Panel>
            </td>
            <td width="16%">
                <asp:RadioButton ID="optAll" runat="server" GroupName="G1" Text="All student" AutoPostBack="True" />
            </td>
            <td width="4%"></td>
        </tr>
        <tr>
            <td width="16%">
                &nbsp;</td>
            <td class="auto-style1">&nbsp;</td>
            <td width="16%">&nbsp;</td>
            <td width="16%">&nbsp;</td>
            <td width="16%">&nbsp;</td>
            <td width="16%">&nbsp;</td>
            <td width="4%">&nbsp;</td>
        </tr>
        <tr>
            <td width="16%">
                <asp:Button ID="btnGenerate" runat="server" CssClass="btn btn-primary" Text="Generate" OnClick="btnGenerate_Click" />
            </td>
            <td class="auto-style1">
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
            </td>
            <td width="16%">&nbsp;</td>
            <td width="16%">&nbsp;</td>
            <td width="16%">&nbsp;</td>
            <td width="16%">&nbsp;</td>
            <td width="4%">&nbsp;</td>
        </tr>
        <tr>
            <td width="16%">
                &nbsp;</td>
            <td class="auto-style1">&nbsp;</td>
            <td width="16%">&nbsp;</td>
            <td width="16%">&nbsp;</td>
            <td width="16%">&nbsp;</td>
            <td width="16%">&nbsp;</td>
            <td width="4%">&nbsp;</td>
        </tr>
        <tr>
            <td colspan="6">
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="1pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="8pt" Width="100%">
                    <LocalReport ReportEmbeddedResource="iDiary_V3.rptStudentICard.rdlc" ReportPath="rptStudentICard.rdlc">
                        <DataSources>
                            <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="DataSet1" />
                        </DataSources>
                    </LocalReport>
                </rsweb:ReportViewer>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData" TypeName="iDiary_V3.iDiaryDataSetTableAdapters.rptStudentICardTableAdapter" InsertMethod="Insert" OldValuesParameterFormatString="original_{0}">
                    <InsertParameters>
                        
                        <asp:Parameter Name="RegNo" Type="String"/>
                        <asp:Parameter Name="SName" Type="String"/>
                        <asp:Parameter Name="FName" Type="String"/>
                        <asp:Parameter Name="Photo" Type="Object"/>
                        <asp:Parameter Name="TAddress" Type="String"/>
                        <asp:Parameter Name="MobileNo" Type="String"/>
                        <asp:Parameter Name="ClassName" Type="String"/>
                        <asp:Parameter Name="SecName" Type="String"/>
                    </InsertParameters>
                </asp:ObjectDataSource>
            </td>
            <td width="4%">&nbsp;</td>
        </tr>
                </table>

            </div>
        </div>
        <div class="clearfix"></div>
    </div>
</asp:Content>
<asp:Content ID="Content3" runat="server" contentplaceholderid="head">
    <style type="text/css">
        .auto-style1 {
        }
    </style>
</asp:Content>

