<%@ Page Title="Conveyance Mode Master" Language="vb" AutoEventWireup="false" MasterPageFile="~/BusMaster.master" CodeBehind="BusconveyanceModeMaster.aspx.vb" Inherits="iDiary_V3.conveyanceModeMaster" %>
<asp:Content ID="Content2" ContentPlaceHolderID="BusMasterContents" runat="server">
       
    <table class="table">
        <tr>
            <td width="40%" valign="top">
                <asp:ListBox ID="lstMasters" runat="server" Height="225px" Width="300px" 
                    AutoPostBack="True"></asp:ListBox>
            </td>
            <td width="60%" valign="top" align="left">
                <b>Conveyance Mode</b>
                <br />
                <asp:TextBox ID="txtName" runat="server" CssClass="textbox"></asp:TextBox>
                <br />
                                <br />
                <b>Display Order</b><br />
                <asp:TextBox ID="txtDispOrder" runat="server" TextMode="Number" CssClass="textbox"></asp:TextBox>
                <br />
                <br />
                <asp:CheckBox ID="chkDefault" runat="server" style="font-weight: 700" Text="Set As Default" />
                <br />
                <br />
                <asp:Label ID="lblStatus" runat="server" Font-Bold="True" ForeColor="Navy" Text="" style="color: #FF3300"></asp:Label>
                <br />
                <br />
                <asp:Button ID="btnSave" runat="server" Text="Save" class="btn btn-primary" />
&nbsp;
                <asp:Button ID="btnNew" runat="server" Text="New" class="btn btn-primary"  />
&nbsp;
                <asp:Button ID="btnRemove" runat="server" Text="Remove" class="btn btn-primary" Visible="false" />
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
