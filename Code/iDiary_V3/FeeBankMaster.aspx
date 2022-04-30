<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/FeeMasterPage.master" CodeBehind="FeeBankMaster.aspx.vb" Inherits="iDiary_V3.FeeBankMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FeeMasterContents" runat="server">
    <%--<br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />--%>
    <table class="table">
        <tr>
            <td width="40%" valign="top">
                <asp:ListBox ID="lstMasters" runat="server" Height="267px" Width="278px"
                    AutoPostBack="True"></asp:ListBox>
            </td>
            <td width="60%" valign="top" align="left">
                <b>Bank Name</b>
                <br />
                <asp:TextBox ID="txtName" runat="server" CssClass="textbox"></asp:TextBox>
                <br />
                <br />
                <b>Display Order</b><strong><br />
                    <asp:TextBox ID="txtDisplayOrder" runat="server" CssClass="textbox" TextMode="Number"></asp:TextBox>
                    <br />
                <br />
                </strong>
                <asp:CheckBox ID="chkDefault" runat="server" style="font-weight: 700" Text="Set As Default" />
                <strong>
                    <br />
                </strong>
                <br />
                <asp:Label ID="lblStatus" runat="server" Font-Bold="True" ForeColor="Navy" Text=""></asp:Label>
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
                &nbsp;&nbsp;&nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
    </table>

</asp:Content>
