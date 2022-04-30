<%@ Page Language="VB" MasterPageFile="~/ExamAdminMasterPage.master" AutoEventWireup="false" Inherits="iDiary_V3.ExamSubjectPriority" Codebehind="ExamSubjectPriority.aspx.vb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SubHeading" Runat="Server">
    Subject Priority
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMain" Runat="Server">
  
       <table width="100%" cellpadding="2">
        <tr>
            <td style="width: 3%" valign="top" align="left">
                &nbsp;</td>
            <td style="width: 20%" valign="top" align="left">
                <b>Class<br />
                <asp:DropDownList ID="cboClass" runat="server" AutoPostBack="True" Width="220px"
                    CssClass="Dropdown" >
                </asp:DropDownList>
                <br />
                <br />
                Subject Group<br />
                <asp:DropDownList ID="cboSubjectGroup" runat="server" AutoPostBack="True" Width="220px"
                    CssClass="Dropdown" >
                </asp:DropDownList>
                <br />
                <br />
                Subject List<br />
                </b>
                <asp:ListBox ID="lstSubjects" runat="server" Height="300px" Width="220px">
                </asp:ListBox>
                <br />
                <br />
                <asp:Label ID="lblStatus" runat="server" Font-Bold="True" ForeColor="Navy"></asp:Label>
                <br />
                <br />
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" />
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




