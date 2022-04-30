<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ExamAdminMasterPage.master" CodeBehind="ExamConfiguration.aspx.vb" Inherits="iDiary_V3.ExamConfiguration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SubHeading" Runat="Server">
    Subject Configuration
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMain" Runat="Server">
   <%-- <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />--%>

    <table class="table" >
        <tr>
            <td style="width:28%">
                <b>Activity Type Subject Group</b></td>
            <td style="width:40%">
                <asp:DropDownList ID="cboActivityGroup" runat="server" CssClass="Dropdown">
                </asp:DropDownList>
                </td>
            <td style="width:20%"></td>
        </tr>
         <tr>
            <td style="width:28%">
                <strong>Health Status Type Subject Group</strong></td>
            <td style="width:40%"><strong>
                <asp:DropDownList ID="cboHealthGroup" runat="server" CssClass="Dropdown">
                </asp:DropDownList>
                </strong>
                </td>
            <td style="width:20%"></td>
        </tr>
         <tr>
            <td style="width:28%">
                <b>Marks Entry Allowed</b></td>
            <td style="width:40%"><b>
                    <asp:DropDownList ID="cboMarksEntryAllowed" runat="server" CssClass="Dropdown">
                        <asp:ListItem Value="-1">--select option--</asp:ListItem>
                        <asp:ListItem Value="1">Yes</asp:ListItem>
                        <asp:ListItem Value="0">No</asp:ListItem>
                    </asp:DropDownList>
                </b>
                </td>
            <td style="width:20%"></td>
        </tr>
         <tr>
            <td style="width:28%"><b>Marks Entry Permission Applicable</b></td>
            <td style="width:40%"><b>
                    <asp:DropDownList ID="cboMarksEntryPermApplicable" runat="server" CssClass="Dropdown">
                        <asp:ListItem Value="-1">--select option--</asp:ListItem>
                        <asp:ListItem Value="1">Yes</asp:ListItem>
                        <asp:ListItem Value="0">No</asp:ListItem>
                    </asp:DropDownList>
                </b>
                </td>
            <td style="width:20%"></td>
        </tr>
         <tr>
            <td style="width:28%"><b>Processing Allowed</b></td>
            <td style="width:40%"><b>
                    <asp:DropDownList ID="cboProcessingAllowed" runat="server" CssClass="Dropdown">
                        <asp:ListItem Value="-1">--select option--</asp:ListItem>
                        <asp:ListItem Value="1">Yes</asp:ListItem>
                        <asp:ListItem Value="0">No</asp:ListItem>
                    </asp:DropDownList>
                </b>
                </td>
            <td style="width:20%">&nbsp;</td>
        </tr>
         <tr>
            <td style="width:28%">&nbsp;</td>
            <td style="width:40%">
                <asp:Label ID="lblStatus" runat="server" Style="font-weight: 700; color: #FF0000"></asp:Label>
             </td>
            <td style="width:20%">&nbsp;</td>
        </tr>
         <tr>
            <td style="width:28%">&nbsp;</td>
            <td style="width:40%">
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" />
             </td>
            <td style="width:20%">&nbsp;</td>
        </tr>
    </table>
  
</asp:Content>
