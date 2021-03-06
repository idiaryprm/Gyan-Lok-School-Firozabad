<%@ Page Title="Bus Route Master" Language="vb" AutoEventWireup="false" MasterPageFile="~/BusMaster.master" CodeBehind="BusRouteMaster.aspx.vb" Inherits="iDiary_V3.BusRouteMaster" %>
<asp:Content ID="Content2" ContentPlaceHolderID="BusMasterContents" runat="server">
        <table width="100%" cellpadding="2" cellspacing="2" border="0">
        <tr>
            <td width="40%" valign="top">
                <asp:ListBox ID="lstMasters" runat="server" Height="225px" Width="300px" 
                    AutoPostBack="True"></asp:ListBox>
            </td>
            <td width="60%" valign="top" align="left">
                <b>Bus Route Name</b>
                <br />
                <asp:TextBox ID="txtName" runat="server" Width="155px" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                <br />
                <br />
                <asp:TextBox ID="txtDetails" runat="server" Width="155px" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" Height="65px" TextMode="MultiLine"></asp:TextBox>
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
