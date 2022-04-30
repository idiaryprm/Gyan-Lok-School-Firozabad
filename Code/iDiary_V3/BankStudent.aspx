<%--<%@ Page Title="" Language="VB" MasterPageFile="~/StudentMaster.master" AutoEventWireup="false" CodeFile="BankStudent.aspx.vb" Inherits="BankStudent" %>--%>
<%@ Page Title="" Language="VB" MasterPageFile="~/StudentMaster.master" AutoEventWireup="false" Inherits="iDiary_V3.BankStudent" Codebehind="BankStudent.aspx.vb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="StudentMasterContents" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 748px">
            <tr>
                <td width="20">&nbsp;</td>
                <td width="400">
                    <asp:ListBox ID="lstBank" runat="server" Height="300px" Width="300px" 
                        AutoPostBack="True"></asp:ListBox>
                </td>
                <td width="520" valign="top">
                    <b>Bank Name</b>
                    <br />
                    <asp:TextBox ID="txtBankName" runat="server" Width="183px" BorderStyle="Solid" BorderColor="Gray" BorderWidth="1px"></asp:TextBox>
                    &nbsp;
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="txtBankName" ErrorMessage="Bank Name Required..."></asp:RequiredFieldValidator>
                    <br />
                    <asp:CheckBox ID="chkDefault" runat="server" style="font-weight: 700" Text="Set As Default" />
                    <br />
                    <br />
                    <asp:Button ID="btnSave" runat="server" Text="Save" Width="75px" 
                        BorderStyle="Solid" BorderColor="Gray" BorderWidth="1px" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnNew" runat="server" BorderStyle="Solid" Text="New" 
                        Width="75px" BorderColor="Gray" BorderWidth="1px" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnRemove" runat="server" Text="Remove" Width="75px" 
                        BorderStyle="Solid" BorderColor="Gray" BorderWidth="1px" />
                    <br />
                    <br />
                    <asp:TextBox ID="txtID" runat="server" Width="183px" Visible="False"></asp:TextBox>
                </td>
                <td width="20">&nbsp;</td>
            </tr>
        </table>

</asp:Content>

