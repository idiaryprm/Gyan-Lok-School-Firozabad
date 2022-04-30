<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/PayrollTransaction.master" CodeBehind="EmpPendingLeaves.aspx.vb" Inherits="iDiary_V3.PendingLeaves" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SubHeading" runat="server">
    Pending Leaves
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CertificateContent" runat="server">
    <table class="table">
        <tr>
            <td width="100%">
                <div style="overflow-y:scroll; max-height:400px">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="Grid" DataSourceID="SqlDataSource1" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="EmpCode" HeaderText="Employee Code" SortExpression="EmpCode" />
                        <asp:BoundField DataField="EmpName" HeaderText="Name" SortExpression="EmpName" />
                        <asp:BoundField DataField="DesgName" HeaderText="Designation" SortExpression="DesgName" />
                        <asp:BoundField DataField="DeptName" HeaderText="Department" SortExpression="DeptName" />
                        <asp:BoundField DataField="AttDate" HeaderText="Date" SortExpression="AttDate" DataFormatString="{0:dd/MM/yyyy}" />
                        <%--<asp:BoundField DataField="Att" HeaderText="Attendance" SortExpression="Att" />--%>
                    </Columns>
                   
                </asp:GridView>
                    </div>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT [EmpID], [EmpCode], [EmpName], [DesgName], [DeptName], [AttDate],[Att] FROM EmpID<0"></asp:SqlDataSource>
                <br />
                <asp:Button ID="btnRefresh" runat="server" CssClass="btn btn-primary" Text="Refresh" />

            </td>
        </tr>
    </table>
</asp:Content>
