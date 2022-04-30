<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/PayrollMaster.master" CodeBehind="LeaveMaster.aspx.vb" Inherits="iDiary_V3.LeaveMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SubHeading" runat="server">
    Leave Type Master
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CertificateContent" runat="server">
         <table class="table">
        <tr>
            <td width="40%" valign="top">
                <asp:ListBox ID="lstMasters" runat="server" Height="225px" Width="300px" CssClass="list"
                    AutoPostBack="True"></asp:ListBox>
            </td>
            <td width="60%" valign="top" align="left">
                Leave Name
                <br />
                <asp:TextBox ID="txtName" runat="server" Width="155px" CssClass="textbox"></asp:TextBox>
                <br />
                <br />
                Maximum Limit<br />
                <asp:TextBox ID="txtMaxLimit" runat="server" Width="155px" CssClass="textbox"></asp:TextBox>
                <br />
                <br />
                <asp:CheckBox ID="chkCarryForward" runat="server" Text="Carry Forward" />
                <br />
                <br />
                <asp:CheckBox ID="chkSalaryDeduct" runat="server" Text="Salary Deduct Applicable" />
                <br />
                <br />
                Applicable For
                <asp:DropDownList ID="cboEmpType" runat="server" CssClass="Dropdown">
                </asp:DropDownList>
                <br />
                <br />
                <asp:Button ID="btnSave" runat="server" Text="Save" Width="65px" CssClass="btn btn-primary" />
&nbsp;
                <asp:Button ID="btnNew" runat="server" Text="New" Width="65px" CssClass="btn btn-primary" />
&nbsp;
                <asp:Button ID="btnRemove" runat="server" Text="Remove" Width="65px" CssClass="btn btn-primary" />
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
