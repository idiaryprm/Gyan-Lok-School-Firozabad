<%@ Page Title="Vaccination Master" Language="vb" AutoEventWireup="false" MasterPageFile="~/StudentMaster.master" CodeBehind="VaccinationMaster.aspx.vb" Inherits="iDiary_V3.VaccinationMaster" %>
<asp:Content ID="Content2" ContentPlaceHolderID="StudentMasterContents" runat="server">
            <table width="100%" border="0" cellpadding="2" cellspacing="2">
        <tr>
            <td width="40%" valign="top">
                <asp:ListBox ID="lstMasters" runat="server" Height="252px" Width="300px" 
                    AutoPostBack="True"></asp:ListBox>
            </td>
            <td width="60%" valign="top" align="left">
                <b>Vaccination Code</b>
                <br />
                <asp:TextBox ID="txtCode" runat="server" CssClass="textbox"></asp:TextBox>
                <br />
                <strong>Vaccination Name<br />
                <asp:TextBox ID="txtName" runat="server" CssClass="textbox"></asp:TextBox>
                <br />
                Vaccination Type<br />
                </strong>
                <asp:DropDownList ID="cboVacType" runat="server" CssClass="Dropdown">
                    <asp:ListItem> </asp:ListItem>
                    <asp:ListItem>Vaccine</asp:ListItem>
                    <asp:ListItem>Booster Dose</asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:Label ID="lblStatus" runat="server" Font-Bold="True" ForeColor="Navy" Text=""></asp:Label>
                <br />
                <br />
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="hvr-glow" />
&nbsp;
                <asp:Button ID="btnNew" runat="server" Text="New" CssClass="hvr-glow" />
&nbsp;
                <asp:Button ID="btnRemove" runat="server" Text="Remove" visible="false" CssClass="hvr-glow" />
                <br />
                <asp:TextBox ID="txtID" runat="server" ReadOnly="True" Visible="False" 
                    Width="74px"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp;&nbsp;
                </td>
        </tr>
    </table>

</asp:Content>
