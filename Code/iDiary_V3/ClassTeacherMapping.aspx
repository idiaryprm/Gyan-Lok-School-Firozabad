<%@ Page Language="VB" MasterPageFile="~/AdminMaster.master" AutoEventWireup="false" Inherits="iDiary_V3.Admin_ClassTeacherMapping" title="Untitled Page" Codebehind="ClassTeacherMapping.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AdminMasterContents" Runat="Server">
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <table width="100%">
        <tr>
            <td style="width: 274px; text-decoration: underline; font-size: 14px;" valign="top"><strong>CLASS - TEACHER ASSIGNMENT</strong></td>
            <td valign="top" style="width: 482px">
                &nbsp;</td>
            <td valign="top">&nbsp;</td>
        </tr>
        <tr>
            <td style="width: 274px" valign="top"><b>
                <br />
                Class </b><br />
                <asp:DropDownList ID="cboClass" runat="server" Width="222px" 
                    AutoPostBack="True">
                </asp:DropDownList>
                <br />
                <b>Section</b><br />
                <asp:DropDownList ID="cboSection" runat="server" Width="222px" 
                    AutoPostBack="True">
                </asp:DropDownList>
                <b>
                <br />
                Sub-Section<br />
                <asp:DropDownList ID="cboSubSection" runat="server" Width="222px" 
                    AutoPostBack="True">
                </asp:DropDownList>
                </b>
                <br />
                <br />
                <b>Teachers</b><br />
                <asp:ListBox ID="lstTeachers" runat="server" Height="241px" Width="223px">
                </asp:ListBox>
                <asp:Button ID="btnAdd" runat="server" Text="&gt;&gt;" Width="40px" />
                <br />
                <br />
                <br />
                <asp:Button ID="btnSave" runat="server" Text="Save" Width="79px" />
                        </td>
            <td valign="top" style="width: 482px">
                <span style="text-decoration: underline"><b>Teachers List</b></span><br />
                <asp:ListBox ID="lstMappedTeachers" runat="server" Height="255px" Width="290px">
                </asp:ListBox>
                <asp:Button ID="btnRemove" runat="server" Text="Remove" Width="60px" />
                <br />
                <br />
                <asp:Button ID="btnClassTeacher" runat="server" Text="Choose Class Teacher" Width="290px" 
                    BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" />
                <br />
                <asp:DropDownList ID="cboClassTeacher" runat="server" Width="290px" Enabled="False">
                </asp:DropDownList>
            </td>
            <td valign="top"><br />
            </td>
        </tr>
    </table>
</asp:Content>

