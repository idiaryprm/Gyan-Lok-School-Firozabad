<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/PayrollTransaction.master" CodeBehind="EmpBankStatement.aspx.vb" Inherits="iDiary_V3.BankStatement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SubHeading" runat="server">
    Bank Statement
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CertificateContent" runat="server">
    <table class="table">
        <tr>
            <td width="15%">Bank</td>
            <td width="20%">
                <asp:DropDownList ID="cboBank" runat="server" CssClass="Dropdown"></asp:DropDownList></td>
            <td width="10%">Month</td>
            <td width="15%">
                <asp:DropDownList ID="cboMonth" runat="server" CssClass="Dropdown">
                </asp:DropDownList>
            </td>
            <td width="10%">Year</td>
            <td width="10%">
                <asp:DropDownList ID="cboYear" runat="server" CssClass="Dropdown">
                </asp:DropDownList>
            </td>
            <td width="20%" align="right">
                <asp:Button ID="btnGenerate" runat="server" Text="Generate" CssClass="btn btn-primary" />
            </td>
        </tr>

        <tr>
            <td width="15%">&nbsp;</td>
            <td width="20%">&nbsp;</td>
            <td width="10%">&nbsp;</td>
            <td width="15%">&nbsp;</td>
            <td width="10%">&nbsp;</td>
            <td width="10%">&nbsp;</td>
            <td width="20%" align="right">&nbsp;</td>
        </tr>

        <tr>
            <td colspan="7">
                <div id="gvDiv">
                    <asp:Label ID="lblHeader" runat="server" Font-Bold="True" ForeColor="Navy" Text="Bank Statement"></asp:Label>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="Grid" DataSourceID="SqlDataSource1" Width="60%">
                        <Columns>
                            <asp:BoundField DataField="AccNo" HeaderText="Account No" SortExpression="AccNo" />
                            <asp:BoundField DataField="EmpName" HeaderText="Employee Name" SortExpression="EmpName" />
                            <asp:BoundField DataField="NetPay" HeaderText="Net Pay" SortExpression="NetPay" />
                        </Columns>

                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT [AccNo], [EmpName], [NetPay] FROM [rptBankStatement]"></asp:SqlDataSource>
                </div>
            </td>
        </tr>

        <tr>
            <td colspan="7">
                <asp:Button ID="btnPrint" runat="server" Text="Print" Width="82px" />
                &nbsp;&nbsp;
                <asp:Button ID="btnExcel" runat="server" Text="Export to Excel" Width="125px" />
            </td>
        </tr>

    </table>
    

</asp:Content>
