<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="ExamMarksProcessing.aspx.vb" Inherits="iDiary_V3.ExamMarksProcessing" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Heading" runat="server">
    Exam marks Processing/Freezing
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">   
    <div class="col_3" style="margin-top: 20px;" id="panelEnquiryNo" runat="server">
    
         <div class="col-md-3 widget widget1" style="width: 100%">
            <div class="r3_counter_box">

                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>

               <%-- <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
                <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
                <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />--%>

    <table class="table">
        <tr>
            
                    <td class="auto-style1">Exam Group</td>
                    <td class="auto-style4">
                        <asp:DropDownList ID="cboExamGroup" runat="server" AutoPostBack="True" CssClass="Dropdown">
                        </asp:DropDownList>
                    </td>
                    <td class="auto-style3">&nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td class="auto-style6">&nbsp;</td>
                    <td>
                        &nbsp;</td>
               
        </tr>
        <tr>
            
                    <td class="auto-style1">Classes</td>
                    <td class="auto-style4" colspan="5">
                          <asp:CheckBoxList ID="cblClasses" runat="server" DataSourceID="sdsCSSID" DataTextField="CSSName" DataValueField="CSSID" CellPadding="10" CellSpacing="2" RepeatColumns="3" RepeatDirection="Horizontal" Width="80%">
            </asp:CheckBoxList>

                      <br />
                      <asp:CheckBox ID="cbAll" runat="server" AutoPostBack="True" Font-Bold="True" Font-Size="Medium" ForeColor="#660033" Text="Check/Uncheck All" />

                           <asp:SqlDataSource ID="sdsCSSID" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT [CSSName], [CSSID] FROM [vw_ExamSubjectMapping] Where cssid=0"></asp:SqlDataSource>
                    </td>
               
        </tr>
        <tr>
            <td class="auto-style1">
                <asp:Label ID="lblGrpID" runat="server" Visible="False"></asp:Label>
            </td>
            <td class="auto-style4" colspan="3">
                <asp:Button ID="btnProcess" runat="server" cssclass="btn btn-primary" Text="Process Marks" />
            </td>
            <td class="auto-style6">&nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style1" colspan="3">
                <asp:Label ID="lblmsg" runat="server" Font-Bold="true" Font-Size="18px" ForeColor="Red" style="font-size: small"></asp:Label>
                <br />
                                    <asp:GridView ID="gvGrade" runat="server" AutoGenerateColumns="False" DataSourceID="sdsGrade" Visible="False">
                                        <Columns>
                                            <asp:BoundField DataField="UpperValue" HeaderText="UpperValue" SortExpression="UpperValue" />
                                            <asp:BoundField DataField="LowerValue" HeaderText="LowerValue" SortExpression="LowerValue" />
                                            <asp:BoundField DataField="Grade" HeaderText="Grade" SortExpression="Grade" />
                                             <asp:BoundField DataField="GradePoint" HeaderText="GradePoint" SortExpression="GradePoint" />
                                        </Columns>
                                    </asp:GridView>
                                    <asp:SqlDataSource ID="sdsGrade" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT [UpperValue], [LowerValue], [Grade],[GradePoint] FROM [vw_ExamGradeMapping] Where GradeID=1 order by DisplayOrder"></asp:SqlDataSource>
            </td>
            <td>
                &nbsp;</td>
            
            <td class="auto-style6">
                &nbsp;</td>
            
            <td class="auto-style6">
                &nbsp;</td>
            
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
    .auto-style3 {
        width: 152px;
    }
    .auto-style4 {
        }
    .auto-style6 {
        }
</style>
</asp:Content>

