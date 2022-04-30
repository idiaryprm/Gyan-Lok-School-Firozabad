<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ExamAdminMasterPage.Master" CodeBehind="ExamGradeMapping.aspx.vb" Inherits="iDiary_V3.ExamGradeMapping" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SubHeading" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
   <%-- <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />--%>
    <table class="table">
        <tr>
            <td>School Name</td>
            <td colspan="3">
                <asp:DropDownList ID="cboSchoolName" runat="server" AutoPostBack="true" CssClass="Dropdown" Width="300px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>Exam Group</td>
            <td><asp:DropDownList ID="cboExamGroup" runat="server" AutoPostBack="True" CssClass="Dropdown" Width="150px">
                                    </asp:DropDownList></td>
            <td>Subject Type</td>
            <td> <asp:DropDownList ID="cboSubjectGroup" runat="server" AutoPostBack="True" CssClass="Dropdown" Width="150px">
                                    </asp:DropDownList></td>
        </tr>
        <tr>
            <td>Grade Point Name</td>
            <td><asp:DropDownList ID="cboGradePoint" runat="server" AutoPostBack="True" CssClass="Dropdown" Width="150px">
                                    </asp:DropDownList></td>
            <td colspan="2"><asp:Label ID="lblStatus" runat="server" Text="" style="font-weight: 700; color: #FF3300;"></asp:Label></td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT DISTINCT [GradeName], [ExamGroupName], [subGroupName] FROM [vw_ExamGradeMapping] WHERE GradeMapID < 0">
                   <%-- <SelectParameters>
                        <asp:ControlParameter ControlID="cboExamGroup" Name="ExamGroupName" PropertyName="SelectedValue" Type="String" />
                        <asp:ControlParameter ControlID="cboSubjectGroup" Name="subGroupName" PropertyName="SelectedValue" Type="String" />
                    </SelectParameters>--%>
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" Width="100%" CssClass="Grid">
                    <Columns>
                        <asp:BoundField DataField="DisplayOrder" HeaderText="Sr. No." SortExpression="DisplayOrder" />
                        <asp:BoundField DataField="UpperValue" HeaderText="Upper Value" SortExpression="UpperValue" />
                        <asp:BoundField DataField="LowerValue" HeaderText="Lower Value" SortExpression="LowerValue" />
                        <asp:BoundField DataField="Grade" HeaderText="Grade" SortExpression="Grade" />
                        <asp:BoundField DataField="GradePoint" HeaderText="Grade Point" SortExpression="GradePoint" />
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT DisplayOrder,[UpperValue], [LowerValue], [Grade], [GradePoint] FROM [ExamGradeDetails] Where GradeID &lt; 0"></asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td><asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary"/></td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>
