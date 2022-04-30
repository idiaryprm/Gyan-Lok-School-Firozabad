<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/PayrollTransaction.master" CodeBehind="EmpProcessSalary.aspx.vb" Inherits="iDiary_V3.ProcessSalary" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SubHeading" runat="server">
    Process Monthly Salary
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CertificateContent" runat="server">
     <table class="table">
        <tr>
            <td width="10%">Month</td>
            <td width="20%">
                <asp:DropDownList ID="cboMonth" runat="server" CssClass="Dropdown"></asp:DropDownList>
            </td>
            <td width="10%">Year</td>
            <td width="20%">
                <asp:DropDownList ID="cboYear" runat="server" CssClass="Dropdown">
                </asp:DropDownList>
            </td>
            <td width="40%">
                <asp:Button ID="btnProcess" runat="server" Text="Process Salary" 
                    CssClass="btn btn-primary" />
            </td>
        </tr>

        <tr>
            <td colspan="5">
            </td>
        </tr>
        
        <tr>
            <td colspan="5">
                <asp:Label ID="lblStatus" runat="server" Text="Label" Font-Bold="True" 
                    ForeColor="Navy"></asp:Label>
                <br />
                <br />
                <asp:Button ID="btnPayBill" runat="server" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px" Text="Generate Pay Bill" />
            </td>
        </tr>
    </table>
   

</asp:Content>
