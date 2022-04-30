<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.master" enableEventValidation="false" CodeBehind="Exam_ReportCard.aspx.vb" Inherits="iDiary_V3.Exam_ReportCard" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Heading" runat="server">
    Report Card Generation
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<%--    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />--%>

    <div class="col_3" style="margin-top: 20px;" id="panelEnquiryNo" runat="server">
        <div class="col-md-3 widget widget1" style="width: 100%">
            <div class="r3_counter_box">
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
               

                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                          </ContentTemplate>
                </asp:UpdatePanel>

                        <table class="table">
                            <tr>
                                <td style="width: 15%">School Name</td>
                                <td colspan="6">
                                     <asp:DropDownList ID="cboSchoolName" runat="server" CssClass="Dropdown" Width="300px" AutoPostBack="true" ></asp:DropDownList>
                        <asp:Label ID="lblSchoolID" runat="server" Visible="False"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 15%"><b>Exam Group</b>      </td>
                                <td style="width: 15%">
                                    <asp:DropDownList ID="cboExamGroup" runat="server" AutoPostBack="True" CssClass="Dropdown">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 15%"><b>Class</b></td>
                                <td style="width: 15%">
                                    <asp:DropDownList ID="cboClass" runat="server" AutoPostBack="True" CssClass="Dropdown">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 15%">
                                    <strong>Section</strong></td>
                                <td style="width: 15%">
                                    <b>
                                        <asp:DropDownList ID="cboSection" runat="server"  CssClass="Dropdown">
                                        </asp:DropDownList>
                                    </b>
                                </td>
                                <td style="width: 10%"></td>
                            </tr>
                            <tr>
                                <td style="width: 15%"><b>Term</b></td>
                                <td style="width: 15%">
                                    <asp:DropDownList ID="cboTerm" runat="server"  CssClass="Dropdown">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 15%"><b>Status</b></td>
                                <td style="width: 15%"><b>
                                    <asp:DropDownList ID="cboStatus" runat="server" CssClass="Dropdown">
                                    </asp:DropDownList>
                                </b></td>
                                <td style="width: 15%"><strong>Issue Date</strong></td>
                                <td style="width: 15%">
                                    <asp:TextBox ID="txtReportDate" runat="server" CssClass="textbox"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender runat="server" BehaviorID="txtReportDate_CalendarExtender" Format="dd-MM-yyyy" TargetControlID="txtReportDate" ID="txtReportDate_CalendarExtender"></ajaxToolkit:CalendarExtender>
                                </td>
                                <td style="width: 10%"></td>
                            </tr>
                            <tr>
                                <td style="width: 15%">   <strong>School Re-open Date</strong></td>
                                <td style="width: 15%">
                                    <asp:TextBox ID="txtopenDate" runat="server" CssClass="textbox"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender runat="server" BehaviorID="txtopenDate_CalendarExtender" Format="dd-MM-yyyy" TargetControlID="txtopenDate" ID="txtopenDate_CalendarExtender"></ajaxToolkit:CalendarExtender>
                                </td>
                                <td style="width: 15%">  <asp:CheckBox ID="chkRegno" runat="server" Text="Reg No."/> </td>
                                <td style="width: 15%"> <asp:TextBox ID="txtRegno" runat="server" CssClass="textbox"></asp:TextBox>  </td>
                                <td style="width: 15%">   </td>
                                <td style="width: 15%">
                                       <asp:Button ID="btnGenerate" runat="server" CssClass="btn btn-primary" Text="Generate" />
                                       </td>
                                <td style="width: 10%">   </td>
                            </tr>
                        </table>
                        
                        <table class="table">
                            <tr>
                                <td style="width: 15%">   
                                    <asp:Label ID="lblGrpID" runat="server" Visible="False"></asp:Label>
                                </td>
                                <td style="width: 15%">   </td>
                                <td style="width: 15%">   </td>
                                <td style="width: 15%">   </td>
                                <td style="width: 15%">   </td>
                                <td style="width: 15%">   </td>
                                <td style="width: 10%">   </td>
                            </tr>
                            <tr>
                                <td colspan="7">   
                                    <rsweb:ReportViewer ID="ReportViewer1" Width="95%" runat="server">
                                    </rsweb:ReportViewer>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 15%">&nbsp;</td>
                                <td style="width: 15%">&nbsp;</td>
                                <td style="width: 15%">&nbsp;</td>
                                <td style="width: 15%">&nbsp;</td>
                                <td style="width: 15%">&nbsp;</td>
                                <td style="width: 15%">&nbsp;</td>
                                <td style="width: 10%">&nbsp;</td>
                            </tr>
                            </table> 

                  

            </div>
        </div>
        <div class="clearfix"></div>
    </div>
              
</asp:Content>
