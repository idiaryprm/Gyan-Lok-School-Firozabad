<%@ Page Title="" Language="VB" MasterPageFile="~/PayrollMaster.master" AutoEventWireup="false" Inherits="iDiary_V3.Bank" Codebehind="Bank.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SubHeading" Runat="Server">
    Bank
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CertificateContent" Runat="Server">
    <table class="table">
            <tr>
                <td width="20">&nbsp;</td>
                <td width="400">
                    <asp:ListBox ID="lstBank" runat="server" Height="300px" Width="300px"  CssClass="list"
                        AutoPostBack="True"></asp:ListBox>
                </td>
                <td valign="top" style="width: 601px">
                    <b>Bank Name</b>
                    <br />
                    <asp:TextBox ID="txtBankName" runat="server" CssClass="textbox"></asp:TextBox>
                    <br />
                    <br />
                    <asp:CheckBox ID="chkDefault" runat="server" style="font-weight: 700" Text="Set As Default" />
                    <br />
                    <br />
                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnNew" runat="server"  Text="New" 
                        CssClass="btn btn-primary" />
            
                    &nbsp;<asp:Button ID="btnRemove" runat="server" Text="Remove" Visible="false" CssClass="btn btn-primary" />
                    <br />
                    <br />
                    <asp:TextBox ID="txtID" runat="server" Width="183px" Visible="False"></asp:TextBox>
                </td>
                <td width="20">&nbsp;</td>
            </tr>
        </table>
</asp:Content>

