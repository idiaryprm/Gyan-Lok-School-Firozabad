<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/LibraryMasterPage.master" CodeBehind="LibraryMembership.aspx.vb" Inherits="iDiary_V3.LibraryMembership" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SubHeading" runat="server">
    Library Membership Management
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="LibraryMasterContents" runat="server">
    <table style="width:100%">
        <tr>
            <td style="width:15%">

                <strong>Membership Type</strong></td>
             <td colspan="2">

                 <asp:RadioButtonList ID="rblMembership" runat="server" AutoPostBack="True" RepeatDirection="Horizontal" style="font-weight: 700" Width="200px">
                     <asp:ListItem>Student</asp:ListItem>
                     <asp:ListItem>Employee</asp:ListItem>
                 </asp:RadioButtonList>

            </td>
             <td style="width:25%">

            </td>
             <td style="width:10%">

            </td>
        </tr>
         <tr>
            <td style="width:15%">

                <asp:Label ID="lblName" runat="server" Text="Student Name"></asp:Label>
             </td>
             <td style="width:25%">

                 <asp:TextBox ID="txtName" runat="server" BorderStyle="Solid" BorderWidth="1px" Width="160px"></asp:TextBox>

            </td>
             <td style="width:25%">

                 <asp:ImageButton ID="btnSearch" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/images/next.png" />

            </td>
             <td style="width:25%">

            </td>
              <td style="width:10%">

            </td>
        </tr>
         <tr>
            <td style="width:15%">

                <asp:Label ID="lblregCode" runat="server" Text="Admn. No."></asp:Label>
             </td>
             <td style="width:25%">

                 <asp:TextBox ID="txtRegCode" runat="server" BorderStyle="Solid" BorderWidth="1px" Width="160px"></asp:TextBox>

            </td>
             <td style="width:25%">

                 &nbsp;</td>
             <td style="width:25%">

                 &nbsp;</td>
              <td style="width:10%">

                  &nbsp;</td>
        </tr>
         <tr>
            <td style="width:15%">

            </td>
             <td colspan="3">

                <asp:GridView ID="gvStudent" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="2" DataSourceID="SqlDataSourceStudent" ForeColor="Black" GridLines="Horizontal" Height="75px" Width="227px">
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" />
                        <asp:BoundField DataField="RegNo" HeaderText="Reg No" SortExpression="RegNo" />
                        <asp:BoundField DataField="SName" HeaderText="Name" SortExpression="SName" />
                        <asp:BoundField DataField="ClassName" HeaderText="Class" SortExpression="ClassName" />
                        <asp:BoundField DataField="SecName" HeaderText="Section" SortExpression="SecName" />
                    </Columns>
                    <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                    <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                    <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F7F7F7" />
                    <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                    <SortedDescendingCellStyle BackColor="#E5E5E5" />
                    <SortedDescendingHeaderStyle BackColor="#242121" />
                </asp:GridView>
                <asp:GridView ID="gvEmployee" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="2" DataSourceID="SqlDataSourceEmployee" ForeColor="Black" GridLines="Horizontal" Height="75px" Width="275px">
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" />
                        <asp:BoundField DataField="EmpCode" HeaderText="Employee Code" SortExpression="EmpCode" />
                        <asp:BoundField DataField="EmpName" HeaderText="Employee Name" SortExpression="EmpName" />
                        <asp:BoundField DataField="DeptName" HeaderText="Department" SortExpression="DeptName" />
                        <asp:BoundField DataField="DesgName" HeaderText="Designation" SortExpression="DesgName" />
                    </Columns>
                    <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                    <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                    <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F7F7F7" />
                    <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                    <SortedDescendingCellStyle BackColor="#E5E5E5" />
                    <SortedDescendingHeaderStyle BackColor="#242121" />
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSourceEmployee" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT [EmpCode], [EmpName], [DeptName], [DesgName] FROM [vw_Employees]"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSourceStudent" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT [RegNo], [SName], [ClassName], [SecName] FROM [vw_Student]"></asp:SqlDataSource>

            </td>
              <td style="width:10%">

            </td>
        </tr>
         <tr>
            <td style="width:15%">

                Membership Status</td>
             <td style="width:25%">

                 <asp:DropDownList ID="cboMembership" runat="server" Width="160px">
                 </asp:DropDownList>

            </td>
             <td style="width:25%">

                <asp:Button ID="btnUpdate" runat="server" Text="Save / Update" Width="140px" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px" />

            </td>
             <td style="width:25%">

            </td>
              <td style="width:10%">

            </td>
        </tr>

         <tr>
            <td style="width:15%">

                &nbsp;</td>
             <td style="width:25%">

                 &nbsp;</td>
             <td style="width:25%">

                 &nbsp;</td>
             <td style="width:25%">

                 &nbsp;</td>
              <td style="width:10%">

                  &nbsp;</td>
        </tr>

         <tr>
            <td style="width:15%">

                &nbsp;</td>
             <td style="width:25%">

                 &nbsp;</td>
             <td style="width:25%">

                 &nbsp;</td>
             <td style="width:25%">

                 &nbsp;</td>
              <td style="width:10%">

                  &nbsp;</td>
        </tr>

    </table>
</asp:Content>
