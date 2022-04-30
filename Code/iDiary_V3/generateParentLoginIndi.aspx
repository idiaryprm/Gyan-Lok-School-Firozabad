<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/AdminMaster.master" CodeBehind="generateParentLoginIndi.aspx.vb" Inherits="iDiary_V3.generateParentLoginIndi" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminMasterContents" runat="server">
    <table class="table">
         <tr>

             <td colspan="2"><strong>Parent Login Individual<br />
                 </strong></td>
             <td style="width:140px">
                 &nbsp;</td>
             <td style="width:140px">
                 &nbsp;</td>
         </tr>
         <tr>

             <td>Student Admn. No.</td>
             <td>
                 <asp:TextBox ID="txtAdmnNo" runat="server" CssClass="textbox"></asp:TextBox>
             </td>
             <td style="width:140px">
                 <asp:Button ID="btnShow" runat="server" Text="Show Login" CssClass="btn btn-primary" />
             </td>
             <td style="width:140px">
                 <asp:Button ID="btnGenerate" runat="server" CssClass="btn btn-primary" Text="Reset Password" />
             </td>
         </tr>
         <tr>

             <td style="width:140px">&nbsp;</td>
             <td style="width:140px">&nbsp;</td>
             <td style="width:140px">
                 &nbsp;</td>
             <td style="width:140px">
                 &nbsp;</td>
         </tr>
         <tr>

             <td colspan="4">
                 <asp:GridView ID="gvLogin" runat="server" AutoGenerateColumns="False" CellPadding="4" DataSourceID="SqlDataSourceStudents" GridLines="Horizontal" CssClass="Grid" Width="100%">
                     <Columns>
                         <asp:BoundField DataField="RegNo" HeaderText="Addmission No." SortExpression="RegNo" />
                         <asp:BoundField DataField="SName" HeaderText="Student Name" SortExpression="SName" />
                         <asp:BoundField DataField="FName" HeaderText="Father Name" SortExpression="FName" />
                         <asp:BoundField DataField="MName" HeaderText="Mother Name" SortExpression="MName" />
                         <asp:BoundField DataField="ParentLoginName" HeaderText="Login Name" SortExpression="ParentLoginName" />
                     </Columns>
                 </asp:GridView>
                 <br />
                 <asp:SqlDataSource ID="SqlDataSourceStudents" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT [RegNo], [SName], [FName], [MName], [ParentLoginName] FROM [vw_parent_login] WHERE (([regno] = @regNO) AND ([ASID] = @ASID))">
                     <SelectParameters>
                         <asp:ControlParameter ControlID="txtAdmnNo" Name="regNO" PropertyName="Text" />
                         <asp:CookieParameter CookieName="ASID" Name="ASID" />
                     </SelectParameters>
                 </asp:SqlDataSource>
             </td>
         </tr>
        
    </table>
</asp:Content>
