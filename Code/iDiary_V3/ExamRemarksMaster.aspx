<%@ Page Title="Remarks Master" Language="vb" AutoEventWireup="false" MasterPageFile="~/ExamAdminMasterPage.master" CodeBehind="ExamRemarksMaster.aspx.vb" Inherits="iDiary_V3.ExamRemarksMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SubHeading" Runat="Server">
    Remarks Master 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMain" Runat="Server">
   <%--  <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />--%>

   <table class="table">
        <tr>
            <td style="width:15%; height: 25px;">

                <strong>Subject</strong></td>
             <td style="width:25%; height: 25px;">

                <b>
                <asp:DropDownList ID="cboSubject" runat="server" AutoPostBack="True" CssClass="Dropdown">
                </asp:DropDownList>
                 </b>

            </td>
             <td style="width:15%; height: 25px;">

                 <strong>Remark</strong></td>
             <td colspan="2" rowspan="3">

                 <strong>
                <asp:TextBox ID="txtName" runat="server" Width="100%" Height="100%" TextMode="MultiLine" CssClass="textbox"></asp:TextBox>
                
            </td>
             <td style="width:5%; height: 25px;">

            </td>
        </tr>
        <tr>
            <td style="width:15%; height: 11px;">

                <strong>
                <asp:Label ID="lblGrade" runat="server" style="font-weight: 700" Text="Grade :"></asp:Label>
                
            </td>
             <td style="width:25%; height: 11px;">

                 <strong>
                 <asp:TextBox ID="txtGrade" runat="server" CssClass="textbox"></asp:TextBox>
                
            </td>
             <td style="width:15%; height: 11px;">

            </td>
             <td style="width:5%; height: 11px;">

            </td>
        </tr>
        <tr>
            <td style="width:15%">

                <strong>
                <asp:Label ID="lblApplicable" runat="server" style="font-weight: 700" Text="Applicable for :"></asp:Label>
                
            </td>
             <td colspan="2">

                 <strong>
                <asp:CheckBoxList ID="cblClasses" runat="server" DataSourceID="sdsClass" DataTextField="ClassName" DataValueField="ClassID" RepeatColumns="4" RepeatDirection="Horizontal" Width="100%">
                </asp:CheckBoxList>
                </td>
             <td style="width:5%">

                 &nbsp;</td>
        </tr>
        <tr>
            <td style="width:15%">

                &nbsp;</td>
             <td colspan="2">

                 <strong>
                     <asp:CheckBox ID="cbAll" Text="Check/Unckeck All" runat="server" AutoPostBack="True" style="color: #800000" />
                            
            </td>
             <td colspan="3" style="color: #990000">

                 <strong>use &lt;*&gt; for replacing name <br />and &lt;**&gt; for replacing his/her.</strong></td>
        </tr>
        <tr>
            <td style="width:15%">

                &nbsp;</td>
             <td colspan="2">

                 <strong>
                <asp:Label ID="lblStatus" runat="server" Font-Bold="True" ForeColor="Navy" Text="" style="color: #FF3300"></asp:Label>
                
            </td>
             <td colspan="3" style="color: #990000">

                 &nbsp;</td>
        </tr>
        <tr>
            <td style="width:15%">

                &nbsp;</td>
             <td style="width:25%">

                 <strong>
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnNew" runat="server" Text="New" CssClass="btn btn-primary" />

            </td>
             <td style="width:15%">

                 &nbsp;</td>
             <td style="width:25%">

                 &nbsp;</td>
             <td style="width:15%">

                 &nbsp;</td>
             <td style="width:5%">

                 &nbsp;</td>
        </tr>
        <tr>
            <td colspan="6">

                <asp:GridView ID="gvMapping" runat="server" DataKeyNames="SubjectID" AutoGenerateColumns="False" CssClass="Grid" DataSourceID="sdsGrade" Width="700px">
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" />
                        <asp:BoundField DataField="Subject" HeaderText="Subject" SortExpression="Subject" />
                        <asp:BoundField DataField="Remark" HeaderText="Remark" SortExpression="Remark" />
                        <asp:BoundField DataField="Grade" HeaderText="Grade" SortExpression="Grade" />
                        <asp:BoundField DataField="Applicable Class" HeaderText="Applicable Class" ReadOnly="True" SortExpression="Applicable Class" />
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="sdsGrade" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="
SELECT SubjectName as Subject,remarkname as Remark,Grade,SubjectID,
     STUFF(
         (SELECT DISTINCT ',' + classname
          FROM vw_examremarksmaster
          WHERE SubjectName = a.SubjectName AND remarkname = a.remarkname 
          FOR XML PATH (''))
          , 1, 1, '')  AS [Applicable Class]
FROM vw_examremarksmaster AS a 
                    Where Subjectid=0
GROUP BY remarkname, SubjectName,Grade,SubjectID
"></asp:SqlDataSource>

            </td>
        </tr>
        <tr>
            <td style="width:15%">

                &nbsp;</td>
             <td style="width:25%">

                 &nbsp;</td>
             <td style="width:15%">

                 <strong>
                <asp:SqlDataSource ID="sdsClass" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT [ClassName], [ClassID] FROM [Classes] Where classid=0 ORDER BY [DisplayOrder]"></asp:SqlDataSource>
                
            </td>
             <td style="width:25%">

                 <strong>
                <asp:Label ID="lblDisplayType" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblEntryType" runat="server" Visible="False"></asp:Label>
                     <asp:Label ID="lblRemark" runat="server" Visible="False"></asp:Label>
                     <asp:Label ID="lblRemarkID" runat="server" Visible="False"></asp:Label>
                
            </td>
             <td style="width:15%">

                 <strong>
                <asp:TextBox ID="txtID" runat="server" ReadOnly="True" Visible="False" 
                    Width="74px"></asp:TextBox>
            </td>
             <td style="width:5%">

                 &nbsp;</td>
        </tr>
        <tr>
            <td style="width:15%">

                &nbsp;</td>
             <td style="width:25%">

                 &nbsp;</td>
             <td style="width:15%">

                 &nbsp;</td>
             <td style="width:25%">

                 &nbsp;</td>
             <td style="width:15%">

                 &nbsp;</td>
             <td style="width:5%">

                 &nbsp;</td>
        </tr>
       </table>
 
</asp:Content>
