<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/PayrollTransaction.master" CodeBehind="EmpConfigSalaryHeads.aspx.vb" Inherits="iDiary_V3.ConfigSalaryHeads" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SubHeading" runat="server">
    Configure Salary Heads (Earning / Deduction)
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CertificateContent" runat="server">
    <table class="table">
        <tr>
            <td width="30%" valign="top">
                <asp:ListBox ID="lstHeads" runat="server" Height="277px" Width="217px"
                    AutoPostBack="True"></asp:ListBox>
            </td>

            <td width="60%" valign="top" style="padding: 1px">
                <span style="text-decoration: underline; font-weight: bold">Percentage (%) for 
                Calculation</span><br />
                <br />
                <asp:TextBox ID="txtAmount" runat="server" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                %<br />
                <br />
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary"/>
                <br />
                <br />
                <span style="text-decoration: underline">Note</span>: If Paramater Calculation on % basis then this value will be used.<br />
                <br />
                <asp:Label ID="lblStatus" runat="server" Font-Bold="True" ForeColor="Navy"
                    Text="Label"></asp:Label>
            </td>

        </tr>
    </table>


</asp:Content>
