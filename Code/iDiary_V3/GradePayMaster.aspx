<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/PayrollMaster.master" CodeBehind="GradePayMaster.aspx.vb" Inherits="iDiary_V3.GradePayMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SubHeading" runat="server">
    Grade Pay
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CertificateContent" runat="server">
    <table class="table">
        <tr>
            <td width="20">&nbsp;</td>
            <td width="400">
                <asp:ListBox ID="lstAGP" runat="server" Height="300px" Width="300px" CssClass="list"
                    AutoPostBack="True"></asp:ListBox>
            </td>
            <td width="520" valign="top">
                <b>Grade Pay Name</b>
                <br />
                <asp:TextBox ID="txtAGPName" runat="server" Width="183px" CssClass="textbox"></asp:TextBox>
                <br />
                <br />
                <asp:CheckBox ID="chkDefault" runat="server" Style="font-weight: 700" Text="Set As Default" />

                <br />
                <br />
                <asp:Button ID="btnSave" runat="server" Text="Save" Width="75px"
                    CssClass="btn btn-primary" />
                &nbsp;&nbsp;
                    <asp:Button ID="btnNew" runat="server" Text="New"
                        Width="75px" CssClass="btn btn-primary" />
                &nbsp;&nbsp;
                    <asp:Button ID="btnRemove" runat="server" Text="Remove" Width="75px" Visible="false"
                        CssClass="btn btn-primary" />
                <br />
                <br />
                <br />
                <asp:TextBox ID="txtID" runat="server" Width="183px" Visible="False"></asp:TextBox>
            </td>
            <td width="20">&nbsp;</td>
        </tr>
    </table>

</asp:Content>
