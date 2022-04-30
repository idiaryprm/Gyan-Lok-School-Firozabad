<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/BusMaster.master" CodeBehind="BusMasterEntry.aspx.vb" Inherits="iDiary_V3.BusMasterEntry" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BusMasterContents" runat="server">
     <table Class="table">
        <tr>
            <td width="40%" valign="top">
                <asp:ListBox ID="lstMasters" runat="server" Height="225px" Width="300px" 
                    AutoPostBack="True"></asp:ListBox>
            </td>
            <td width="60%" valign="top" align="left">
                <b>Bus Name</b>
                <br />
                <asp:TextBox ID="txtName" runat="server" cssClass="textbox"></asp:TextBox>
                <br />
                <br />
                <b>Bus Number</b>
                <br />
                <asp:TextBox ID="txtNumber" runat="server" cssClass="textbox"></asp:TextBox>
                <br />
                <br />
                <b>Bus Capacity</b><br />
                <asp:TextBox ID="txtCapacity" runat="server" TextMode="Number" cssClass="textbox"></asp:TextBox>
                <br />
                <br />
                <b>Driver Name</b>&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEmpName" runat="server" Text="" style="font-weight: 700"></asp:Label>&nbsp;,<asp:Label ID="lblEmpCode" runat="server" Text="" style="font-weight: 700"></asp:Label><br />
                <asp:TextBox ID="txtDName" runat="server" cssClass="textbox"></asp:TextBox>
                &nbsp;
                <asp:Button ID="btnNext" runat="server" Text=">>" class="btn btn-primary"/>
                <br />
                <br />
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" Width="381px">
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" />
                        <asp:BoundField DataField="EmpCode" HeaderText="EmpCode" SortExpression="EmpCode" />
                        <asp:BoundField DataField="EmpName" HeaderText="EmpName" SortExpression="EmpName" />
                        <asp:BoundField DataField="DeptName" HeaderText="DeptName" SortExpression="DeptName" />
                        <asp:BoundField DataField="DesgName" HeaderText="DesgName" SortExpression="DesgName" />
                        <asp:BoundField DataField="Mob" HeaderText="Mob" SortExpression="Mob" />
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT [EmpCode], [EmpName], [DeptName], [DesgName], [Mob] FROM [vw_Employees] Where EmpID<0 "></asp:SqlDataSource>
                <br />
                <asp:Label ID="lblStatus" runat="server" Font-Bold="True" ForeColor="Navy" Text="" style="color: #FF3300"></asp:Label>
                <br />
                               <asp:Button ID="btnSave" runat="server" Text="Save"  class="btn btn-primary"/>
&nbsp;
                <asp:Button ID="btnNew" runat="server" Text="New" class="btn btn-primary" />
&nbsp;
                <asp:Button ID="btnRemove" runat="server" Text="Remove" class="btn btn-primary" Visible="false"/>
                <br /><br />
                <asp:TextBox ID="txtID" runat="server" ReadOnly="True" Visible="False" 
                    Width="74px"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtEmpCode" runat="server" ReadOnly="True" Visible="False" 
                    Width="74px"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp;&nbsp;
                </td>
        </tr>
    </table>
</asp:Content>
