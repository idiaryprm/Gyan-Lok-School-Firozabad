<%@ Page Title="Location Master" Language="vb" AutoEventWireup="false" MasterPageFile="~/BusMaster.master" CodeBehind="BusLocationMaster.aspx.vb" Inherits="iDiary_V3.LocationMaster" %>
<asp:Content ID="Content2" ContentPlaceHolderID="BusMasterContents" runat="server">
    <table class="table">
        <tr>
            <td width="40%" valign="top">
                <asp:ListBox ID="lstMasters" runat="server" Height="225px" Width="300px" 
                    AutoPostBack="True"></asp:ListBox>
            </td>
            <td width="60%" valign="top" align="left">
                <b>Conveyance Head
                <br />
                <asp:DropDownList ID="cboConveyaceTypes" runat="server" CssClass="Dropdown" AutoPostBack="True">
                </asp:DropDownList>
                <br />
                <br />
                                 Location Name</b>
                <br />
                <asp:TextBox ID="txtName" runat="server" CssClass="textbox"></asp:TextBox>
                <br />
                <br />
                <asp:Label ID="lblStatus" runat="server" Font-Bold="True" ForeColor="Navy" Text="" style="color: #FF3300"></asp:Label>
                <br />
                <br />
                <asp:Button ID="btnSave" runat="server" Text="Save" class="btn btn-primary" />
&nbsp;
                <asp:Button ID="btnNew" runat="server" Text="New" class="btn btn-primary"/>
&nbsp;
                <asp:Button ID="btnRemove" runat="server" Text="Remove" class="btn btn-primary" Visible="False"/>
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
