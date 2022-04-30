<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/FeeMasterPage.master" CodeBehind="ClassTermConfig.aspx.vb" Inherits="iDiary_V3.ClassTermConfig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FeeMasterContents" runat="server">
    <table style="width: 100%">
        <tr>
            <td style="width: 116px">Select Class</td>
            <td style="width: 173px">
                <asp:DropDownList ID="cboClass" runat="server" Height="19px" Width="136px" AutoPostBack="True">
                </asp:DropDownList>
            </td>
            <td colspan="2">
                Select Section</td>
            <td>
                <asp:DropDownList ID="cboSection" runat="server" Height="19px" Width="136px" AutoPostBack="True">
                </asp:DropDownList>
                <asp:TextBox ID="txtID" runat="server" Height="16px" Visible="False" Width="55px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 116px; height: 44px;" valign="top">Select Term<br />
            </td>
            <td colspan="2" valign="top" style="height: 44px">
                <asp:RadioButton ID="rdoSelectAll" runat="server" AutoPostBack="True" GroupName="Select" Text="Select All" />
&nbsp;
                <asp:RadioButton ID="rdoDeSelectAll" runat="server" AutoPostBack="True" GroupName="Select" Text="Select None" />
                <br />
                <asp:CheckBoxList ID="chkTermNo" runat="server" Height="23px" Width="136px">
                </asp:CheckBoxList>
            </td>
            <td style="width: 128px; height: 44px;">
                </td>
            <td style="height: 44px">
                </td>
        </tr>
        <tr>
            <td style="width: 116px; height: 7px"></td>
            <td style="height: 7px" colspan="4"></td>
        </tr>
        <tr>
            <td colspan="5" style="height: 27px">
                <asp:Label ID="lblStatus" runat="server" ForeColor="#003399" style="font-weight: 700"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 116px">
                <asp:Button ID="btnSave" runat="server" Text="Save" Width="67px" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" />
                </td>
            <td colspan="4">&nbsp;</td>
        </tr>
    </table>
    <table width="100%" cellpadding="3" cellspacing="3" border="0">
    </table>
</asp:Content>
