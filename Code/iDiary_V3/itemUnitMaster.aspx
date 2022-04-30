<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Stock.master" CodeBehind="itemUnitMaster.aspx.vb" Inherits="iDiary_V3.itemUnitMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="StockMasterContents" runat="server">
   <%-- <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />--%>

     <table class="table">
        <tr>
            <td width="40%" valign="top">
                <asp:ListBox ID="lstMaster" runat="server" AutoPostBack="True" Height="275px" 
                    Width="252px"></asp:ListBox>
            </td>
            <td width="60%" valign="top" align="left">
                 Item Unit Name
                <br />
                <br />
                <asp:TextBox ID="txtName" runat="server" CssClass="textbox" ></asp:TextBox>
                &nbsp;&nbsp;
                <br />
                <br />
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" />
&nbsp;&nbsp;
                <asp:Button ID="btnRemove" runat="server" Text="Remove" CssClass="btn btn-primary" />
&nbsp;&nbsp;
                            
                <asp:Button ID="btnNew" runat="server" Text="New" CssClass="btn btn-primary" />
                            
                <br />
                <br />
                <asp:Label ID="lblStatus" runat="server" ForeColor="Red" ></asp:Label>
            
                <br />
                <asp:TextBox ID="txtID" runat="server" Width="60px" Visible="False"></asp:TextBox>
            
                <br />
                </td>
        </tr>
    </table>
</asp:Content>
