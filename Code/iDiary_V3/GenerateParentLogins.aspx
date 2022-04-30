<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/AdminMaster.master" CodeBehind="GenerateParentLogins.aspx.vb" Inherits="iDiary_V3.GenerateParentLogins" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminMasterContents" runat="server">
    
   <%-- <br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/>
    <br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/>--%>


    <table class="table" >
         <tr>

             <td style="width:96px">School Name</td>
             <td style="width:170px">
                <asp:DropDownList ID="cboSchoolName" runat="server" AutoPostBack="true" CssClass="Dropdown" Width="300px">
                </asp:DropDownList>
             </td>
             <td style="width:104px">&nbsp;</td>
             <td style="width:175px">
                 &nbsp;</td>
             <td style="width:133px">
                 &nbsp;</td>
             <td style="width:140px">
                 &nbsp;</td>
         </tr>
         <tr>

             <td style="width:96px">Class</td>
             <td style="width:170px">
                <asp:DropDownList ID="cboClass" runat="server" CssClass="Dropdown" AutoPostBack="True">
                </asp:DropDownList>
             </td>
             <td style="width:104px">Section</td>
             <td style="width:175px">
                <asp:DropDownList ID="cboSection" runat="server" CssClass="Dropdown" AutoPostBack="True">
                </asp:DropDownList>
             </td>
             <td style="width:133px">
                 &nbsp;</td>
             <td style="width:140px">
                 &nbsp;</td>
         </tr>
         <tr>

             <td style="width:96px">&nbsp;</td>
             <td style="width:140px">
                 <asp:Button ID="btnShow" runat="server" Text="Show Login" CssClass="btn btn-primary"/>
             </td>
             <td style="width:104px">&nbsp;</td>
             <td style="width:175px">
                 <asp:Button ID="btnGenerate" runat="server" Text="Generate Login" CssClass="btn btn-primary"/>
             </td>
             <td style="width:133px">&nbsp;</td>
             <td style="width:140px">&nbsp;</td>
         </tr>
         <tr>

             <td colspan="6">
                 <asp:GridView ID="gvLogin" runat="server" AutoGenerateColumns="False" CellPadding="4" DataSourceID="SqlDataSourceStudents" GridLines="Horizontal" CssClass="Grid" Width="100%">
                     <Columns>
                         <asp:BoundField DataField="RegNo" HeaderText="Admission No." SortExpression="RegNo" />
                         <asp:BoundField DataField="SName" HeaderText="Student Name" SortExpression="SName" />
                         <asp:BoundField DataField="FName" HeaderText="Father Name" SortExpression="FName" />
                         <asp:BoundField DataField="MName" HeaderText="Mother Name" SortExpression="MName" />
                         <asp:BoundField DataField="ParentLoginName" HeaderText="Login Name" SortExpression="ParentLoginName" />
                     </Columns>
                     
                 </asp:GridView>
                 <br />
                 <asp:SqlDataSource ID="SqlDataSourceStudents" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT [RegNo], [SName], [FName], [MName], [ParentLoginName] FROM [vw_parent_login] WHERE (([ClassName] = @ClassName) AND ([SecName] = @SecName)) AND ([ASID] = @ASID)">
                     <SelectParameters>
                         <asp:ControlParameter ControlID="cboClass" Name="ClassName" PropertyName="SelectedValue" Type="String" />
                         <asp:ControlParameter ControlID="cboSection" Name="SecName" PropertyName="SelectedValue" Type="String" />
                         <asp:CookieParameter CookieName="ASID" Name="ASID" Type="Int32" />
                     </SelectParameters>
                 </asp:SqlDataSource>
             </td>
         </tr>
         <tr>

             <td style="width:96px">
                 <asp:Label ID="lblCount" runat="server" style="font-weight: 700"></asp:Label>
             </td>
             <td style="width:140px">&nbsp;</td>
             <td style="width:104px">&nbsp;</td>
             <td style="width:175px">&nbsp;</td>
             <td style="width:133px">&nbsp;</td>
             <td style="width:140px">&nbsp;</td>
         </tr>
         <tr>

             <td style="width:96px">&nbsp;</td>
             <td style="width:140px">&nbsp;</td>
             <td style="width:104px">&nbsp;</td>
             <td style="width:175px">&nbsp;</td>
             <td style="width:133px">&nbsp;</td>
             <td style="width:140px">&nbsp;</td>
         </tr>
    </table>
</asp:Content>
