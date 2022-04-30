<%@ Page Language="VB" MasterPageFile="~/ExamAdminMasterPage.master" AutoEventWireup="false" Inherits="iDiary_V3.SubjectMaster" title="Untitled Page" Codebehind="SubjectMaster.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SubHeading" Runat="Server">
    Subject Master
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMain" Runat="Server">
    <table border="0" width="100%">
        <tr>
            <td width="18%" valign="top" align="left">
                <asp:ListBox ID="lstSubjects" runat="server" AutoPostBack="True" Height="273px" 
                    Width="264px"></asp:ListBox>
            </td>
            <td valign="top" align="left" style="width: 4%">&nbsp;</td>
                
            <td align="left" valign="top" style="font-weight: bold; width: 37%;">
            
                Subject Code<br />
                <asp:TextBox ID="txtSubjectCode" runat="server" Width="175px"></asp:TextBox>
                <br />
                <br />
            
                Subject Type (Grade/Marks)
                <asp:ImageButton ID="btnHelpSubjectType" runat="server" Height="19px" 
                    ImageUrl="~/Images/Helpicon.jpg" TabIndex="4" Width="16px" />
                <br />
                <asp:DropDownList ID="cboGradeMarks" runat="server" AutoPostBack="True" 
                    Width="175px" TabIndex="3">
                </asp:DropDownList>
                <br />
                <br />
                Entry Type (Grade / Marks)
                <asp:ImageButton ID="btnHelpEntryType" runat="server" Height="19px" 
                    ImageUrl="~/Images/Helpicon.jpg" TabIndex="8" Width="16px" />
                <br />
                <asp:DropDownList ID="cboGradeMarksEntry" runat="server" AutoPostBack="True" 
                    Width="175px" TabIndex="7">
                </asp:DropDownList>
                <br />
                <br />
                <br />
                <br />
                <asp:Button ID="btnSave" runat="server" Text="Save" Width="70px" 
                    style="height: 26px" TabIndex="11" />
&nbsp;&nbsp;
                <asp:Button ID="btnRemove" runat="server" Text="Remove" Width="70px" 
                    TabIndex="12" />
            
                <br />
                <br />
                <asp:Label ID="lblStatus" runat="server" ForeColor="Navy"></asp:Label>
            
                <br />
                <asp:TextBox ID="txtSubjectID" runat="server" Width="60px" Visible="False" 
                    AutoPostBack="True"></asp:TextBox>
            
                <br />
            
            </td>
            
            <td width="41%" align="left" valign="top" style="font-weight: bold">
            
                Subject Name<br />
                <asp:TextBox ID="txtSubjectName" runat="server" Width="175px" TabIndex="1"></asp:TextBox>
                &nbsp;<asp:ImageButton ID="btnNew" runat="server" 
                    ImageUrl="~/Images/NewIcon.jpg" style="height: 18px" TabIndex="2" />
                <br />
                <br />
                Final Max Marks<asp:ImageButton ID="btnHelpSubjectTypeMarks" runat="server" 
                    Height="19px" ImageUrl="~/Images/Helpicon.jpg" TabIndex="6" Width="16px" />
                <br />
                <asp:TextBox ID="txtMaxMarks" runat="server" Width="96px" TabIndex="5"></asp:TextBox>
                <br />
                <br />
                Max Marks for Entry<asp:ImageButton ID="btnHelpMaxMarksEntry" runat="server" 
                    Height="19px" ImageUrl="~/Images/Helpicon.jpg" TabIndex="10" Width="16px" />
                <br />
                <asp:TextBox ID="txtMaxMarksEntry" runat="server" Width="96px" TabIndex="9"></asp:TextBox>
                <br />
                <br />
            
            </td>
            
        </tr>
        <tr>
            <td width="18%" valign="top" align="left" colspan="4" style="width: 59%">
                <asp:Label ID="lblHelp" runat="server" BackColor="#00FF99" BorderColor="Navy" 
                    Font-Bold="True" ForeColor="Navy"></asp:Label>
            </td>
            
        </tr>
    </table>
</asp:Content>

