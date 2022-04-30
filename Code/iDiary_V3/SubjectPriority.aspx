<%@ Page Language="VB" MasterPageFile="~/ExamAdminMasterPage.master" AutoEventWireup="false" Inherits="iDiary_V3.SubjectPriority" Codebehind="SubjectPriority.aspx.vb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SubHeading" Runat="Server">
    Subject Priority
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMain" Runat="Server">
    <table width="100%">
        <tr>
            <td style="width: 3%" valign="top" align="left">
                &nbsp;</td>
            <td style="width: 20%" valign="top" align="left">
                <b>Class<br />
                <asp:DropDownList ID="cboClass" runat="server" AutoPostBack="True" 
                    Width="212px">
                </asp:DropDownList>
                <br />
                <br />
                Section<br />
                <asp:DropDownList ID="cboSection" runat="server" AutoPostBack="True" 
                    Width="212px">
                </asp:DropDownList>
                <br />
                <br />
                Sub-Section<br />
                <asp:DropDownList ID="cboSubSection" runat="server" AutoPostBack="True" 
                    Width="212px">
                </asp:DropDownList>
                <br />
                <br />
                Subject List<br />
                </b>
                <asp:ListBox ID="lstSubjects" runat="server" Height="179px" Width="312px">
                </asp:ListBox>
                <br />
                <br />
                <asp:Label ID="lblStatus" runat="server" Font-Bold="True" ForeColor="Navy"></asp:Label>
                <br />
                <br />
                <asp:Button ID="btnSave" runat="server" Height="29px" style="margin-top: 0px" 
                    Text="Save" Width="70px" />
            </td>
            <td style="width: 39%" valign="top" align="left">
                <b>
                <br />
                </b>
                <br />
                <br />
                &nbsp;&nbsp;&nbsp;&nbsp;
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <asp:ImageButton ID="btnUp" runat="server" Height="45px" 
                    ImageUrl="~/images/UpArrow.jpg" Width="45px" />
                <br />
                <br />
                <asp:ImageButton ID="btnDown" runat="server" Height="45px" 
                    ImageUrl="~/images/DownArrow.jpg" Width="45px" />
                <br />
            </td>
        </tr>
    </table>
</asp:Content>




