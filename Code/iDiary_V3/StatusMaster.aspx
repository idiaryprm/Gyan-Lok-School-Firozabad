<%@ Page Title="Status Master" Language="vb" AutoEventWireup="false" MasterPageFile="~/StudentMaster.master" CodeBehind="StatusMaster.aspx.vb" Inherits="iDiary_V3.StatusMaster" %>
<asp:Content ID="Content2" ContentPlaceHolderID="StudentMasterContents" runat="server">
        <table class="table">
        <tr>
            <td valign="top" width="40%">
                <asp:ListBox ID="lstMasters" runat="server" CssClass="textbox" AutoPostBack="True" Height="254px" 
                    Width="300px"></asp:ListBox>
            </td>
            <td align="left" valign="top" width="60%">
                <b>Status Name</b>
                <br />
                <asp:TextBox ID="txtName" runat="server" CssClass="textbox"></asp:TextBox>
                <br />
                <br />
                <asp:CheckBox ID="chkDefault" runat="server" style="font-weight: 700" Text="Set As Default" />
                <br />
                <br />
                <asp:Label ID="lblStatus" runat="server" Font-Bold="True" ForeColor="Navy" 
                    Text=""></asp:Label>
                <br />
                <br />
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" />
                &nbsp;
                <asp:Button ID="btnNew" runat="server" Text="New" CssClass="btn btn-primary" />
                &nbsp;
                <asp:Button ID="btnRemove" runat="server" Text="Remove" CssClass="btn btn-primary" Visible="False" />
                <br />
                <asp:TextBox ID="txtID" runat="server" ReadOnly="True" Visible="False" 
                    Width="74px"></asp:TextBox>
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>
