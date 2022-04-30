<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/FeeMasterPage.master" CodeBehind="FeeTypes.aspx.vb" Inherits="iDiary_V3.FeeTypes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FeeMasterContents" runat="server">
    <table class="table">
        <tr>
            <td width="40%" valign="top">
                <asp:ListBox ID="lstMasters" runat="server" Height="297px" Width="278px"
                    AutoPostBack="True"></asp:ListBox>
            </td>
            <td width="60%" valign="top" align="left">
                <b>Fee Type / Head Name</b>
                <br />
                <asp:TextBox ID="txtName" runat="server" CssClass="textbox"></asp:TextBox>
                <br />
                <br />
                <asp:CheckBox ID="chkDuePart" runat="server" Font-Bold="True"
                    Text="Part of Due Process" />
                &nbsp;<br />
                <asp:CheckBox ID="chkConcession" runat="server" Font-Bold="True"
                    Text="Concession Applicable" />
                <br />
                <br />
                <b>Display Order</b><br />
                <strong>
                    <asp:TextBox ID="txtDispalyOrder" runat="server" CssClass="textbox"></asp:TextBox>
                </strong>
                <br />
                <asp:Label ID="lblStatus" runat="server" Font-Bold="True" ForeColor="Navy" Text=""></asp:Label>
                <br />
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" />
                &nbsp;
                <asp:Button ID="btnNew" runat="server" Text="New" CssClass="btn btn-primary" />
                &nbsp;
                <asp:Button ID="btnRemove" runat="server" Text="Remove" Visible="false" CssClass="btn btn-primary" />
                <br />
                <asp:TextBox ID="txtID" runat="server" ReadOnly="True" Visible="False"
                    Width="74px"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp;&nbsp;
                <strong>
                    <asp:TextBox ID="txtTallyHeadName" runat="server" CssClass="textbox" Visible="False"></asp:TextBox>
                </strong>
                <asp:CheckBox ID="chkAreearTYpe" runat="server" Font-Bold="True"
                    Text="Arrear Type" Visible="False" />
            </td>
        </tr>
    </table>

</asp:Content>
