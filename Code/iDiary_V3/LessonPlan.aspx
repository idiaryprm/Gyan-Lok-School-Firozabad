<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/TeacherMasterPage.master" CodeBehind="LessonPlan.aspx.vb" Inherits="iDiary_V3.LessonPlan" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TeacherContents" runat="server">
     <table border="0" cellpadding="2" cellspacing="2" width="100%">
         <tr>
             <td width="16%" colspan="2" style="width: 32%; text-decoration: underline">

                 <strong>Lesson Plan</strong></td>
             <td width="16%">

             </td>
             <td width="16%">

             </td>
             <td width="16%">

             </td>
             <td width="20%">

             </td>
         </tr>
         <tr>
             <td width="16%">

                 Class</td>
             <td width="16%">

                 <asp:DropDownList ID="cboClass" runat="server" AutoPostBack="True" Width="110px">
                 </asp:DropDownList>

             </td>
             <td width="16%">

                 &nbsp;&nbsp;

                 Section</td>
             <td width="16%">

                 <asp:DropDownList ID="cboSection" runat="server" Width="110px">
                 </asp:DropDownList>

             </td>
             <td width="16%">

                 &nbsp;

                 Teacher</td>
             <td width="20%">

                 <asp:DropDownList ID="cboTeacher" runat="server" Width="110px">
                 </asp:DropDownList>

             </td>
         </tr>
         <tr>
             <td width="16%">

                 Date</td>
             <td width="16%">

                 <asp:TextBox ID="txtDate" runat="server" Width="100px"></asp:TextBox>

             </td>
             <td width="16%">

             &nbsp; Subject</td>
             <td width="16%">

                 <asp:DropDownList ID="cboSubject" runat="server" Width="110px">
                 </asp:DropDownList>

             </td>
             <td width="16%">

                 &nbsp;</td>
             <td width="20%">

                 &nbsp;</td>
         </tr>
         <tr>
             <td width="16%" valign="top" >

                 Lesson Plan</td>
             <td colspan="5" rowspan="2" valign="top" >

                 <asp:TextBox ID="txtPlan" runat="server" Width="467px" Height="79px" TextMode="MultiLine"></asp:TextBox>

             </td>
         </tr>
         <tr>
             <td width="16%">

                 &nbsp;</td>
         </tr>
         <tr>
             <td width="16%">

                 &nbsp;</td>
             <td width="16%">

                 &nbsp;</td>
             <td width="16%">

                 &nbsp;</td>
             <td width="16%">

                 &nbsp;</td>
             <td width="16%">

                 &nbsp;</td>
             <td width="20%">

                 &nbsp;</td>
         </tr>
         <tr>
             <td width="16%">

                 &nbsp;</td>
             <td width="16%">

                 &nbsp;
                 <asp:Button ID="txtSave" runat="server" BorderStyle="Solid" BorderWidth="1px" Text="Save" Width="70px" style="height: 24px" />
             </td>
             <td width="16%">

                 <asp:Button ID="txtEdit" runat="server" BorderStyle="Solid" BorderWidth="1px" Text="Edit" Width="70px" />
             </td>
             <td width="16%">

                 <asp:Button ID="txtDelete" runat="server" BorderStyle="Solid" BorderWidth="1px" Text="Delete" Width="70px" />
             </td>
             <td width="16%">

                 &nbsp;</td>
             <td width="20%">

                 &nbsp;</td>
         </tr>
         <tr>
             <td width="16%">

                 &nbsp;</td>
             <td width="16%">

                 &nbsp;</td>
             <td width="16%">

                 &nbsp;</td>
             <td width="16%">

                 &nbsp;</td>
             <td width="16%">

                 &nbsp;</td>
             <td width="20%">

                 &nbsp;</td>
         </tr>
         <tr>
             <td width="16%">

                 &nbsp;</td>
             <td width="16%">

                 &nbsp;</td>
             <td width="16%">

                 &nbsp;</td>
             <td width="16%">

                 &nbsp;</td>
             <td width="16%">

                 &nbsp;</td>
             <td width="20%">

                 &nbsp;</td>
         </tr>
         </table>
</asp:Content>
