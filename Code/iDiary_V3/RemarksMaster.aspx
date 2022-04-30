<%@ Page Title="House Master" Language="vb" AutoEventWireup="false" MasterPageFile="~/ExamAdminMasterPage.master" CodeBehind="RemarksMaster.aspx.vb" Inherits="iDiary_V3.RemarksMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SubHeading" Runat="Server">
    Remarks Master 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMain" Runat="Server">
    <table width="100%" cellpadding="2" cellspacing="2" border="0">
        <tr>
            <td width="40%" valign="top">
                <strong>Select Type</strong></td>
            <td width="60%" valign="top" align="left">
                <asp:DropDownList ID="cboType" runat="server" AutoPostBack="True" Width="140px">
                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem>Remarks</asp:ListItem>
                    <asp:ListItem>Participation</asp:ListItem>
                </asp:DropDownList>
                </td>
        </tr>
        <tr>
            <td width="40%" valign="top">
                <asp:ListBox ID="lstMasters" runat="server" Height="225px" Width="300px" 
                    AutoPostBack="True"></asp:ListBox>
            </td>
            <td width="60%" valign="top" align="left">
                <b>Remark Name</b>
                <br />
                <asp:TextBox ID="txtName" runat="server" Width="245px" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" Height="62px" TextMode="MultiLine"></asp:TextBox>
                <br />
                <br />
                <br />
                <br />
                <asp:Label ID="lblStatus" runat="server" Font-Bold="True" ForeColor="Navy" Text=""></asp:Label>
                <br />
                <br />
                <asp:Button ID="btnSave" runat="server" Text="Save" Width="65px" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" />
&nbsp;
                <asp:Button ID="btnNew" runat="server" Text="New" Width="65px" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" />
&nbsp;
                <asp:Button ID="btnRemove" runat="server" Text="Remove" Width="65px" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" />
                <br /><br />
                <asp:TextBox ID="txtID" runat="server" ReadOnly="True" Visible="False" 
                    Width="74px"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp;&nbsp;
                </td>
        </tr>
    </table>

</asp:Content>
