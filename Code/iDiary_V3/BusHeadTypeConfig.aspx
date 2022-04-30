<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/BusMaster.master" CodeBehind="BusHeadTypeConfig.aspx.vb" Inherits="iDiary_V3.ConveyanceCategoryConfig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BusMasterContents" runat="server">
    
    <table Class="tables">
        <tr>
            <td width="25%" style="height: 34px">Term No</td>
            <td width="30%" style="height: 34px">
                <asp:DropDownList ID="cboTermNo" runat="server" CssClass="Dropdown"
                    AutoPostBack="True">
                </asp:DropDownList>
                </td>
            <td width="45%" style="height: 34px"><asp:Label ID="lblTerm" runat="server" Font-Bold="True"></asp:Label></td>
        </tr>

        <tr>
            <td width="25%" style="height: 42px">Conveyance Fee Type</td>
            <td width="30%" style="height: 42px">
                <asp:DropDownList ID="cboConveyaceTypes" runat="server" CssClass="Dropdown" AutoPostBack="True">
                </asp:DropDownList>
                </td>
            <td width="45%" style="height: 42px">
                <asp:TextBox ID="txtID" runat="server" Visible="False"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td width="25%">Fee Amount</td>
            <td width="30%">
                <asp:TextBox ID="txtAmount" runat="server" CssClass="textbox"></asp:TextBox>
                </td>
            <td width="45%">&nbsp;</td>
        </tr>
        <tr>
            <td width="25%">&nbsp;</td>
            <td width="30%">
                &nbsp;</td>
            <td width="45%">&nbsp;</td>
        </tr>
        <tr>
            <td width="25%">&nbsp;</td>
            <td width="30%">
                <asp:Label ID="lblStatus" runat="server" Font-Bold="True" ForeColor="Navy" Text=""></asp:Label>
                </td>
            <td width="45%">&nbsp;</td>
        </tr>
        <tr>
            <td width="25%">&nbsp;</td>
            <td width="30%">
                &nbsp;</td>
            <td width="45%">&nbsp;</td>
        </tr>
        <tr>
            <td width="25%">&nbsp;</td>
            <td width="30%">
                <asp:Button ID="btnSave" runat="server" Text="Save" class="btn btn-primary" />
                </td>
            <td width="45%">&nbsp;</td>
        </tr>
    </table>
     <asp:Table ID="myTable" runat="server" Width="100%" >
                </asp:Table>
</asp:Content>
