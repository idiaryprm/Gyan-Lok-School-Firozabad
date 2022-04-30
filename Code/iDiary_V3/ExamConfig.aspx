<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ExamAdminMasterPage.Master" CodeBehind="ExamConfig.aspx.vb" Inherits="iDiary_V3.ExamConfig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SubHeading" runat="server">
    Exam Configuration
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <table class="table">

        <tr>
            <td colspan="2">

                <strong>Please Select the Exam Terms for locking/Freezing&nbsp; the marks Entry.</strong></td>
        </tr>
        <tr>
            <td style="width: 30%; vertical-align: top;" rowspan="2">

                <strong>Exam Terms</strong><asp:CheckBoxList ID="cblExamTerms" runat="server" DataSourceID="sdsExamTerm" DataTextField="ExamTermName" DataValueField="ExamTermID">
                </asp:CheckBoxList>
                <asp:SqlDataSource ID="sdsExamTerm" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT [ExamTermID], [ExamTermName] FROM [ExamTermMaster] WHERE ExamTermID <0"></asp:SqlDataSource>

            </td>
            <td style="vertical-align: Top;">

                <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="True" Text="Select All" />
                <br />

                <br />
            </td>
        </tr>
        <tr>
            <td style="vertical-align: bottom;">

                <asp:Button ID="btnSave" runat="server" Text="Save" />

            </td>
        </tr>
    </table>
    <table style="float:left; width: 64%; visibility:hidden" >
        <tr>
            <td colspan="2"></td>
           
        </tr>
        <tr>
            <td colspan="2">

            <strong>Please Select the Exam Terms for locking/Freezing&nbsp;for the marks Entry for Group 5 And Attendance, health, etc for All Groups.</strong></td>
        </tr>
        <tr>
            <td rowspan="2">

            <strong>Exam Terms<br />
                </strong><asp:CheckBoxList ID="cblTermsGrp5" runat="server" >
            </asp:CheckBoxList>
            </td>
            <td>

            <asp:CheckBox ID="chkAllTermGrp5" runat="server" AutoPostBack="True" Text="Select All"/>
            </td>
            
        </tr>
        <tr>
            <td>

            <asp:Button ID="btnProceed" runat="server" Text="Save" />

            </td>
            
        </tr>
    </table> 
    </asp:Content>
