<%@ Page Title="Transport Master" Language="vb" AutoEventWireup="false" MasterPageFile="~/StudentMaster.master" CodeBehind="TransportMaster.aspx.vb" Inherits="iDiary_V3.TransportMaster" %>
<asp:Content ID="Content2" ContentPlaceHolderID="StudentMasterContents" runat="server">
        <table width="100%" border="0" cellpadding="2" cellspacing="2">
        <tr>
            <td width="40%" valign="top">
                <asp:ListBox ID="lstMasters" runat="server" Height="225px" Width="300px" 
                    AutoPostBack="True"></asp:ListBox>
            </td>
            <td width="60%" valign="top" align="left">
                <b>Transport Name</b>
                <br />
                <asp:TextBox ID="txtName" runat="server" Width="155px" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                <br />
                <br />
                <asp:CheckBox ID="chkDefault" runat="server" style="font-weight: 700" Text="Set As Default" />
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
