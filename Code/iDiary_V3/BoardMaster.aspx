﻿<%@ Page Title="Board Master" Language="vb" AutoEventWireup="false" MasterPageFile="~/StudentMaster.master" CodeBehind="BoardMaster.aspx.vb" Inherits="iDiary_V3.BoardMaster" %>
<asp:Content ID="Content2" ContentPlaceHolderID="StudentMasterContents" runat="server">
        <table class="table">
        <tr>
            <td width="40%" valign="top">
                <asp:ListBox ID="lstMasters" runat="server" Height="240px" Width="300px" 
                    AutoPostBack="True"></asp:ListBox>
            </td>
            <td width="60%" valign="top" align="left">
                <b>Board Name</b>
                <br />
                <asp:TextBox ID="txtName" runat="server" CssClass="textbox"></asp:TextBox>
                <br />
                <br />
                <asp:CheckBox ID="chkDefault" runat="server" style="font-weight: 700" Text="Set As Default" />
                <br />
                <br />
                <asp:Label ID="lblStatus" runat="server" Font-Bold="True" ForeColor="Navy" Text=""></asp:Label>
                <br />
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
                </td>
        </tr>
    </table>
</asp:Content>
