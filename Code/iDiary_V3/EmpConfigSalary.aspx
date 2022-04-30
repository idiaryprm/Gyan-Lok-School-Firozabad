<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/PayrollTransaction.master" CodeBehind="EmpConfigSalary.aspx.vb" Inherits="iDiary_V3.ConfigEmpSalary" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SubHeading" runat="server">
    Configure Employee Salary
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CertificateContent" runat="server">
    <table class="table">
        <tr>
            <td valign="top" style="width: 33%">
                <strong>Employee Category<br />
                    <asp:DropDownList ID="cboEmpCat" runat="server" Width="150px" AutoPostBack="True">
                    </asp:DropDownList>
                    <br />
                    <br />
                    Employee Names</strong><br />
                <asp:ListBox ID="lstEmployees" runat="server" Height="227px" Width="217px" AutoPostBack="True"></asp:ListBox>
            </td>

            <td valign="top" style="width: 35%">
                <strong>List of Salary Heads</strong><br />
                <asp:ListBox ID="lstSalaryHeads" runat="server" Height="277px" Width="214px"
                    AutoPostBack="True"></asp:ListBox>
            </td>

            <td width="20%" valign="top" style="padding: 1px">
                <u>Calculation Criteria:<br />
                </u>
                <br />
                <asp:RadioButton ID="optFixed" runat="server" Text="Fixed Amount"
                    GroupName="MyOption" />
                <br />
                <asp:TextBox ID="txtAmount" runat="server" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                <br />
                <br />
                <asp:RadioButton ID="optPercent" runat="server" Text="On Percent Basis"
                    GroupName="MyOption" />
                <br />
                <br />
                <br />
                <br />
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" />
                <br />
                <br />
                <asp:Label ID="lblStatus" runat="server" Font-Bold="True" ForeColor="Navy"
                    Text="Label"></asp:Label>
            </td>

        </tr>
    </table>

</asp:Content>
