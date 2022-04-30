<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/PayrollTransaction.master" CodeBehind="EmpCreditLeaves.aspx.vb" Inherits="iDiary_V3.CreditLeaves" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SubHeading" runat="server">
    Credit Leaves into Employee's Account
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CertificateContent" runat="server">
    <table class="table">
        <tr>
            <td width="25%">Employee Category</td>
            <td width="25%">
                <asp:DropDownList ID="cboEmpCat" runat="server" CssClass="Dropdown" AutoPostBack="True"></asp:DropDownList>
            </td>
            <td width="25%">
                Employee Type</td>
            <td width="25%">
                <asp:DropDownList ID="cboEmpType" runat="server" CssClass="Dropdown">
                </asp:DropDownList>
            </td>
            <td width="25%">
                <asp:Button ID="btnShow" runat="server" Text=">>" CssClass="btn btn-sm btn-primary" />
            </td>
        </tr>
        <tr>
            <td colspan="5">&nbsp;</td>
        </tr>
        <tr>
            <td colspan="5">
                  <div style="overflow-y:scroll; max-height:400px">
                <asp:GridView ID="GridView1" runat="server" CssClass="Grid" DataSourceID="SqlDataSource1" GridLines="Vertical" Width="672px">
                    
                </asp:GridView>
                      </div>
            </td>
        </tr>
        <tr>
            <td colspan="5">
                <asp:Button ID="btnCredit" runat="server" Text="Credit Leaves" CssClass="btn btn-primary" Width="100px" />
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT * FROM [rptCreditLeaves]"></asp:SqlDataSource>
            </td>
        </tr>
    </table>


</asp:Content>
