<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Stock.master" CodeBehind="InventoryStoreMaster.aspx.vb" Inherits="iDiary_V3.InventoryStoreMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="StockMasterContents" runat="server">
 <table border="0" width="100%">
        <tr>
            <td width="18%" valign="top" align="left">
                <asp:ListBox ID="lstMaster" runat="server" AutoPostBack="True" Height="275px" 
                    Width="346px"></asp:ListBox>
            </td>
            <td width="2%" valign="top" align="left">&nbsp;</td>
                
            <td width="41%" align="left" valign="top" style="font-weight: bold">
            
                Item Unit Name
                <br />
                <br />
                <asp:TextBox ID="txtName" runat="server" Width="175px" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                &nbsp;&nbsp;
                <br />
                <br />
                <asp:Button ID="btnSave" runat="server" Text="Save" Width="70px" 
                    style="height: 26px" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px" Height="25px" />
&nbsp;&nbsp;
                <asp:Button ID="btnRemove" runat="server" Text="Remove" Width="70px" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px" Height="25px" />
&nbsp;&nbsp;
                            
                <asp:Button ID="btnNew" runat="server" Text="New" Width="57px" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px" Height="25px" />
                            
                <br />
                <br />
                <asp:Label ID="lblStatus" runat="server"></asp:Label>
            
                <br />
                <asp:TextBox ID="txtID" runat="server" Width="60px" Visible="False"></asp:TextBox>
            
                <br />
            
            </td>
            <td width="39%" align="right" valign="top" style="font-weight: bold">
            &nbsp;</td>
            
        </tr>
    </table>

</asp:Content>
