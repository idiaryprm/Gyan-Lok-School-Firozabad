<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/PayrollMaster.master" CodeBehind="Department_Payroll.aspx.vb" Inherits="iDiary_V3.Department_Payroll" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SubHeading" runat="server">
    Department Master
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CertificateContent" runat="server">
    <table class="table">
        <tr>
            <td width="40%" valign="top">
                <asp:ListBox ID="lstMasters" runat="server" Height="225px" Width="300px" CssClass="list"
                    AutoPostBack="True"></asp:ListBox>
            </td>
            <td width="60%" valign="top" align="left">
                <b>Department Name</b>
                <br />
                <asp:TextBox ID="txtName" runat="server" Width="155px" CssClass="textbox"></asp:TextBox>
                <br />
                <br />
                <asp:CheckBox ID="chkDefault" runat="server" Style="font-weight: 700" Text="Set As Default" />
                <br />
                <br />
                <asp:Button ID="btnSave" runat="server" Text="Save" Width="65px"  CssClass="btn btn-primary" />
&nbsp;
                <asp:Button ID="btnNew" runat="server" Text="New" Width="65px"  CssClass="btn btn-primary" />
&nbsp;
                <asp:Button ID="btnRemove" runat="server" Text="Remove" Width="65px"  CssClass="btn btn-primary"  Visible="false"/>
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
