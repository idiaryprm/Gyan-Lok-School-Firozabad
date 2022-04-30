<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ExamMasterPage.Master" CodeBehind="SubjectMappingImport.aspx.vb" Inherits="iDiary_V3.SubjectMappingImport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SubHeading" runat="server">
    Import Wizard -> Subject-Class Mapping
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
        <table border="0" cellpadding="4" cellspacing="4" width="100%">
        <tr>
            <td width="15%">Source Class</td>
            <td width="20%">
                <asp:DropDownList ID="cboClassSource" runat="server" Width="135px" 
                    AutoPostBack="True">
                </asp:DropDownList>
                </td>
            <td width="15%">Source Section</td>
            <td width="20%">
                <b>
                <asp:DropDownList ID="cboSectionSource" runat="server" Width="135px">
                </asp:DropDownList>
                </b>
                </td>
            <td width="30%"></td>
        </tr>
        <tr>
            <td width="15%">Target Class</td>
            <td width="20%">
                <asp:DropDownList ID="cboClassTarget" runat="server" Width="135px" 
                    AutoPostBack="True">
                </asp:DropDownList>
                </td>
            <td width="15%">Target Section</td>
            <td width="20%">
                <b>
                <asp:DropDownList ID="cboSectionTarget" runat="server" Width="135px">
                </asp:DropDownList>
                </b>
                </td>
            <td width="30%"></td>

        </tr>
        <tr>
            <td width="15%">&nbsp;</td>
            <td width="20%">&nbsp;</td>
            <td width="15%">&nbsp;</td>
            <td width="20%">&nbsp;</td>
            <td width="30%">&nbsp;</td>

        </tr>
        <tr>
            <td width="15%">&nbsp;</td>
            <td width="20%">
                <asp:Button ID="btnImport" runat="server" Text="Import" Width="68px" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px" />
            </td>
            <td colspan="3" style="width: 45%">
                <asp:Label ID="lblStatus" runat="server" Font-Bold="True" ForeColor="Navy"></asp:Label>
            </td>

        </tr>
    </table>

</asp:Content>
