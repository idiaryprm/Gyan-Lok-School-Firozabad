<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/PayrollTransaction.master" CodeBehind="EmpSalaryHeadStatement.aspx.vb" Inherits="iDiary_V3.SalaryHeadStatement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SubHeading" runat="server">
    Salary Head Statement
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CertificateContent" runat="server">
  
    <table class="table">
        <tr>
            <td style="width: 16%">Salary Head</td>
            <td width="20%">
                <asp:DropDownList ID="cboHead" runat="server" Width="144px"></asp:DropDownList></td>
            <td width="10%">Month</td>
            <td width="15%">
                <asp:DropDownList ID="cboMonth" runat="server" Width="100px">
                </asp:DropDownList>
            </td>
            <td width="10%">Year</td>
            <td width="10%">
                <asp:DropDownList ID="cboYear" runat="server" Width="90px">
                </asp:DropDownList>
            </td>
            <td width="15%" align="right">
                <asp:Button ID="btnGenerate" runat="server" Text="Generate" CssClass="btn btn-primary"  />
            </td>
        </tr>

        <tr>
            <td style="width: 14%">&nbsp;</td>
            <td width="20%">&nbsp;</td>
            <td width="10%">&nbsp;</td>
            <td width="15%">&nbsp;</td>
            <td width="10%">&nbsp;</td>
            <td width="10%">&nbsp;</td>
            <td width="15%" align="right">&nbsp;</td>
        </tr>

        <tr>
            <td colspan="7" style="height: 190px" align="Top">
                <div id="gvDiv">
                    <asp:Label ID="lblHeader" runat="server" Font-Bold="True" ForeColor="Navy" Text="Bank Statement"></asp:Label>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataSourceID="SqlDataSource1" Width="50%">
                        <Columns>
                            <asp:BoundField DataField="EmpName" HeaderText="EmpName" SortExpression="EmpName" />
                            <asp:BoundField DataField="HeadAmount" HeaderText="HeadAmount" SortExpression="HeadAmount" />
                        </Columns>
                        <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                        <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                        <RowStyle BackColor="White" ForeColor="#330099" />
                        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                        <SortedAscendingCellStyle BackColor="#FEFCEB" />
                        <SortedAscendingHeaderStyle BackColor="#AF0101" />
                        <SortedDescendingCellStyle BackColor="#F6F0C0" />
                        <SortedDescendingHeaderStyle BackColor="#7E0000" />
                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT [EmpName], [HeadAmount] FROM [rptHeadStatement]"></asp:SqlDataSource>
                </div>
            </td>
        </tr>

        <tr>
            <td colspan="7" style="height: 20px">
                <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="btn btn-primary"  />
                &nbsp;&nbsp;
                <asp:Button ID="btnExcel" runat="server" Text="Export to Excel" CssClass="btn btn-primary"  />
            </td>
        </tr>

    </table>

</asp:Content>
