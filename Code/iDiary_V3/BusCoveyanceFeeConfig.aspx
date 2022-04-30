<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/BusMaster.master" CodeBehind="BusCoveyanceFeeConfig.aspx.vb" Inherits="iDiary_V3.ConveyanceCategoryConfig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BusMasterContents" runat="server">
    
    <table Class="tables">
        <%--<tr>
            <td width="25%" style="height: 34px">Term No</td>
            <td width="30%" style="height: 34px">
                <asp:DropDownList ID="cboTermNo" runat="server" CssClass="Dropdown"
                    AutoPostBack="True">
                </asp:DropDownList>
                </td>
            <td width="45%" style="height: 34px"><asp:Label ID="lblTerm" runat="server" Font-Bold="True"></asp:Label></td>
        </tr>--%>

        <tr>
            <td width="25%" style="height: 42px">Conveyance Head</td>
            <td></td>
            <td width="30%" style="height: 42px">
                <asp:DropDownList ID="cboConveyaceTypes" runat="server" CssClass="Dropdown" AutoPostBack="True">
                </asp:DropDownList>
                </td>
            
        </tr>
       
       
        <tr>
            <td width="25%" colspan="2">&nbsp;</td>
            
                <asp:Label ID="lblStatus" runat="server" Font-Bold="True" ForeColor="Navy" Text=""></asp:Label>
               
            <td width="45%">&nbsp;</td>
        </tr>
        
       
    </table>
     <asp:Table ID="myTable" runat="server" Width="100%" GridLines="Horizontal" >
                </asp:Table>
    <br />
     <asp:Button ID="btnSave" runat="server" Text="Save" class="btn btn-primary" />
</asp:Content>
