<%@ Page Title="Vendors" Language="vb" AutoEventWireup="false" MasterPageFile="~/Stock.Master" CodeBehind="InventoryLedger.aspx.vb" Inherits="iDiary_V3.InventoryLedger" %>
<asp:Content ID="Content1" ContentPlaceHolderID="StockMasterContents" runat="server">
      <%--<br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />--%>
    <table class="table">
        <tr>
            <td width="40%" valign="top">
                <asp:ListBox ID="lstLedgers" runat="server" CssClass="textbox"  Height="242px" Width="300px" 
                    AutoPostBack="True"></asp:ListBox>
            </td>
            <td width="60%" valign="top" align="left">
                  Ledger Name
                <br />
                <br />
                <asp:TextBox ID="txtLedgerName" runat="server" CssClass="textbox" ></asp:TextBox>
                &nbsp;&nbsp;
                <br />
                <br />
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" />
&nbsp;&nbsp;
                <asp:Button ID="btnRemove" runat="server" Text="Remove" CssClass="btn btn-primary" Visible="False" />
&nbsp;&nbsp;
                            
                <asp:Button ID="btnNew" runat="server" Text="New" CssClass="btn btn-primary" />
                            
                <br />
                <br />
                <asp:Label ID="lblStatus" runat="server" ForeColor="Red" ></asp:Label>
            
                <br />
                <asp:TextBox ID="txtLedgerID" runat="server" CssClass="textbox"  Visible="False"></asp:TextBox>
            
                <br />
            
            </td>
            <td width="39%" align="right" valign="top" style="font-weight: bold">
            &nbsp;
                </td>
        </tr>
    </table>
</asp:Content>
