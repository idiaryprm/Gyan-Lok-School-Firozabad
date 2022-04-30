<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/AdminMaster.Master" CodeBehind="BackupRecovery.aspx.vb" Inherits="iDiary_V3.BackupRecovery" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminMasterContents" runat="Server">
    <table class="table">
        <tr>
            <td style="height: 18px; width: 350px">&nbsp;</td>
            <td style="height: 18px; width: 509px">
                <asp:RadioButton ID="rbBackup" runat="server" AutoPostBack="True" Checked="True" Font-Bold="True" Font-Size="12pt" ForeColor="Blue" GroupName="Backup" Text="Database Backup" />
            </td>
            <td style="height: 18px; width: 651px">
                <asp:Button ID="btnBackup" runat="server" Text="Click Here for Backup" class="btn btn-primary" />
            </td>
        </tr>
        <tr>
            <td style="width: 350px; height: 10px;"></td>
            <td style="width: 509px; height: 10px;"></td>
            <td style="width: 651px; height: 10px;"></td>
        </tr>

        <tr>
            <td style="width: 350px">&nbsp;</td>
            <td style="width: 509px">
                <asp:RadioButton ID="rbRecovery" runat="server" AutoPostBack="True" Font-Bold="True" Font-Size="12pt" ForeColor="Blue" GroupName="Backup" Text="Restore Database" Visible="False" />
            </td>
            <td style="width: 651px">
                <asp:FileUpload ID="myFile" runat="server" Width="240px" CssClass="textbox" Visible="False" />
            </td>
        </tr>
        <tr>
            <td style="width: 350px">&nbsp;</td>
            <td style="width: 509px">

                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            </td>
            <td style="width: 651px">
                <asp:Button ID="ButtonRecovery" runat="server" Text="Click Here for Recover Database"
                    class="btn btn-primary" Visible="False" />
            </td>
        </tr>

    </table>
</asp:Content>