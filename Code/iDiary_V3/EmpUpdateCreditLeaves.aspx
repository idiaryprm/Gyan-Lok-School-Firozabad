<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/PayrollTransaction.master" CodeBehind="EmpUpdateCreditLeaves.aspx.vb" Inherits="iDiary_V3.updateCreditLeaves" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SubHeading" runat="server">
    Update Leave count 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CertificateContent" runat="server">
    <table class="table">
        <tr>
            <td width="20%">

                Employee Code</td>
            <td width="20%">
                
                <asp:TextBox ID="txtEmpCode" runat="server" CssClass="textbox"></asp:TextBox>
                
            </td>
            <td width="20%">

                <asp:Button ID="btnShow" runat="server" Text=">>" 
                    CssClass="btn btn-primary" /></td>
            <td width="20%">

                &nbsp;</td>
            <td width="20%">
               
                &nbsp;</td>
        </tr>
        <tr>
            <td width="20%">

                Leave Type</td>
            <td width="20%">
                
                <asp:DropDownList ID="cboLeaveType" runat="server" AutoPostBack="True" CssClass="Dropdown">
                </asp:DropDownList>
                
            </td>
            <td colspan="3" style="width: 40%">

                <asp:Label ID="lblEmp" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="20%">

                Max Leaves</td>
            <td width="20%">
                
                <asp:TextBox ID="txtLeaveCount" runat="server" CssClass="textbox"></asp:TextBox></td>
            <td width="20%">

                <asp:Label ID="lblEmpID" runat="server" Visible="False"></asp:Label>
            </td>
            <td width="20%">

                &nbsp;</td>
            <td width="20%">
               
                &nbsp;</td>
        </tr>
           <tr>
            <td width="20%">

                &nbsp;</td>
            <td width="20%">
                
                &nbsp;</td>
            <td width="20%">

                &nbsp;</td>
            <td width="20%">

                &nbsp;</td>
            <td width="20%">
               
                &nbsp;</td>
        </tr>
           <tr>
            <td width="20%">

                &nbsp;</td>
            <td width="20%">
                
                <asp:Button ID="btnSave" runat="server" Text="Save" Width="100px" 
                    CssClass="btn btn-primary" /></td>
            <td width="20%">

                &nbsp;</td>
            <td width="20%">

                &nbsp;</td>
            <td width="20%">
               
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>
