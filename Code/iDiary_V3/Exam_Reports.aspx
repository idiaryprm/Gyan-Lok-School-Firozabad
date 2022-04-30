<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="Exam_Reports.aspx.vb" Inherits="iDiary_V3.Exam_Reports" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Heading" runat="server">
    Exam Reports
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">   
    <div class="col_3" style="margin-top: 20px;" id="panelEnquiryNo" runat="server">
  
     <div class="col-md-3 widget widget1" style="width: 100%">
            <div class="r3_counter_box">

                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>

                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>

                         </ContentTemplate>
                </asp:UpdatePanel>
               <%-- <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><   br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
                <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
                <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />--%>

    <table class="table">
        <tr>
            
                    <td class="auto-style1">School Name</td>
                    <td class="auto-style4" colspan="5">
                        <asp:DropDownList ID="cboSchoolName" runat="server" CssClass="Dropdown" Width="300px" AutoPostBack="true" ></asp:DropDownList>
                        <asp:Label ID="lblSchoolID" runat="server" Visible="False"></asp:Label>

                    </td>
               
        </tr>
        <tr>
            
                    <td class="auto-style1">Exam Group</td>
                    <td class="auto-style4">
                        <asp:DropDownList ID="cboExamGroup" runat="server" AutoPostBack="True" CssClass="Dropdown">
                        </asp:DropDownList>
                    </td>
                    <td class="auto-style3">Class</td>
                    <td>
                        <asp:DropDownList ID="cboClass" runat="server" AutoPostBack="true" CssClass="Dropdown">
                        </asp:DropDownList>
                    </td>
                    <td class="auto-style6">Section</td>
                    <td>
                        <asp:DropDownList ID="cboSection" runat="server" AutoPostBack="true" CssClass="Dropdown">
                        </asp:DropDownList>
                    </td>
               
        </tr>
        <tr>
            <td class="auto-style1">Subject Group</td>
            <td class="auto-style4">
                <asp:DropDownList ID="cboSubjectGroup" runat="server" AutoPostBack="True" CssClass="Dropdown">
                </asp:DropDownList>
            </td>
            <td class="auto-style3">Subject Sub Group</td>
            <td>
                <asp:DropDownList ID="cboSubSubjectGroup" runat="server" AutoPostBack="True" CssClass="Dropdown">
                </asp:DropDownList>
            </td>
            <td class="auto-style6">Subject</td>
            <td>
                <b>
                <asp:DropDownList ID="cboSubjects" runat="server" CssClass="Dropdown">
                </asp:DropDownList>
                </b>
            </td>
        </tr>
        <tr>
            <td class="auto-style1">Term</td>
            <td class="auto-style4">
                <asp:DropDownList ID="cboTerm" runat="server" AutoPostBack="True" CssClass="Dropdown">
                </asp:DropDownList>
            </td>
            <td class="auto-style3">Sub Term</td>
            <td>
                <asp:DropDownList ID="cbosubterm" runat="server" AutoPostBack="True" CssClass="Dropdown">
                </asp:DropDownList>
            </td>
            <td class="auto-style6">Status</td>
            <td>
                <b>
                <asp:DropDownList ID="cboStatus" runat="server" CssClass="Dropdown">
                </asp:DropDownList>
                </b>
            </td>
        </tr>
        <tr>
            <td class="auto-style1">Report Type</td>
            <td class="auto-style4">
                <asp:DropDownList ID="cboListType" runat="server" CssClass="Dropdown">
                    <asp:ListItem>Marks Entry Slip</asp:ListItem>
                    <asp:ListItem>Marks Entry Slip(Term Wise)</asp:ListItem>
                <asp:ListItem>Subject wise Marks Entry Slip</asp:ListItem>
                    <asp:ListItem>Summary Sheet (Scored Marks)</asp:ListItem>
                    <asp:ListItem>Summary Sheet (Weighted Marks)</asp:ListItem>
                      <asp:ListItem>Marks Enty Status</asp:ListItem>
                    <asp:ListItem>Term Wise Subject List</asp:ListItem>
                    <%--<asp:ListItem>Consolidate Sheet</asp:ListItem>--%>
                    <asp:ListItem>Summative Assessment</asp:ListItem>
                    <asp:ListItem>Final Summative Assessment</asp:ListItem>
                     <asp:ListItem>Subject Mapping List</asp:ListItem>
                     <asp:ListItem>Exam Term Mapping List</asp:ListItem>
                    <asp:ListItem>User Permission Mapping List</asp:ListItem>
                     <%--<asp:ListItem>Report Card CBSE</asp:ListItem>--%>
                </asp:DropDownList>
            </td>
            <td class="auto-style3" colspan="2">
                <asp:RadioButtonList ID="rblReportOrient" runat="server" RepeatDirection="Horizontal" Width="180px">
                    <asp:ListItem>Landscape</asp:ListItem>
                    <asp:ListItem>Portrait</asp:ListItem>
                </asp:RadioButtonList>
                (<span class="auto-style7">for entry sheet only</span>)</td>
            <td class="auto-style6">&nbsp;</td>
            <td>
                <asp:Button ID="btnGenerate" runat="server" cssclass="btn btn-primary" Text="Generate Report" />
            </td>
        </tr>
        <tr>
            <td class="auto-style1" colspan="3">
                <asp:Label ID="lblmsg" runat="server" Font-Bold="true" Font-Size="18px" ForeColor="Red" style="font-size: small"></asp:Label>
            </td>
                       <td>
                <asp:DropDownList ID="cboSubSection" runat="server" CssClass="Dropdown" Visible="False">
                </asp:DropDownList>
                 <asp:DropDownList ID="cboStudentAttendance" runat="server" Visible="false">
                </asp:DropDownList>
            </td>
            
            <td class="auto-style6">
                <asp:Label ID="lblGrpID" runat="server" Visible="False"></asp:Label>
            </td>
            
            <td class="auto-style6">
                &nbsp;</td>
            
        </tr>
        
    </table>
     

                 <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="96%">
                </rsweb:ReportViewer>
                          
                <br />
                <asp:SqlDataSource ID="sdsGrade" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT [UpperValue], [LowerValue], [Grade] FROM [vw_ExamGradeMapping] Where GradeID=0"></asp:SqlDataSource>
                <asp:GridView ID="gvGrade" runat="server" AutoGenerateColumns="False" DataSourceID="sdsGrade" Visible="False">
                    <Columns>
                        <asp:BoundField DataField="UpperValue" HeaderText="UpperValue" SortExpression="UpperValue" />
                        <asp:BoundField DataField="LowerValue" HeaderText="LowerValue" SortExpression="LowerValue" />
                        <asp:BoundField DataField="Grade" HeaderText="Grade" SortExpression="Grade" />
                    </Columns>
                </asp:GridView>
                          
            </div>
        </div>
        <div class="clearfix"></div>
    </div>

   
</asp:Content>
<asp:Content ID="Content3" runat="server" contentplaceholderid="head">
    <style type="text/css">
    .auto-style1 {
    }
    .auto-style3 {
        }
    .auto-style4 {
        }
    .auto-style6 {
        }
        .auto-style7 {
            color: #996633;
        }
    </style>
</asp:Content>

