<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="SchoolInfo.aspx.vb" Inherits="iDiary_V3.SchoolInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Heading" runat="server">
    Manage School Information
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table border="0" cellpadding="2" cellspacing="2" width="100%">
        <tr>
            <td width="25%">School Name</td>
            <td width="45%">
                <asp:TextBox ID="txtSchoolName" runat="server" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px" Width="300px"></asp:TextBox>
            </td>
            <td width="30%">&nbsp;</td>
        </tr>
        <tr>
            <td width="25%">School Address</td>
            <td width="45%">
                <asp:TextBox ID="txtSchoolDetails" runat="server" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px" Width="300px"></asp:TextBox>
            </td>
            <td width="30%">&nbsp;</td>
        </tr>
        <tr>
            <td width="25%">Logo</td>
            <td width="45%">
                <asp:Image ID="imgLogo" runat="server" Height="96px" Width="89px" />
                <br />
                <br />
                <asp:FileUpload ID="FileUpload1" runat="server" />
            </td>
            <td width="30%">&nbsp;</td>
        </tr>
        <tr>
            <td width="25%">&nbsp;</td>
            <td width="45%">
                &nbsp;</td>
            <td width="30%">&nbsp;</td>
        </tr>
        <tr>
            <td width="25%">&nbsp;</td>
            <td width="45%">
                <asp:Button ID="btnSave" runat="server" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px" Text="Save" Width="87px" />
            </td>
            <td width="30%">&nbsp;</td>
        </tr>
    </table>
</asp:Content>
