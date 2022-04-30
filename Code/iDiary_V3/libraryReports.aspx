<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/IssueBookMaster.master" CodeBehind="libraryReports.aspx.vb" Inherits="iDiary_V3.libraryReports" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SubHeading" runat="server">
    Library Reports
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="LibraryMasterContents" runat="server">
    
     <table Class="table">
         <tr>
            <td style="width: 531px"><h3>Transaction report Of :</h3> </td>
               
             <td style="width: 88px"></td>
             <td></td>
          </tr>
        
         <tr>
             <td style="width: 531px"><asp:RadioButtonList ID="rblreport" runat="server" AutoPostBack="True" RepeatDirection="Horizontal" Width="383px">
                         <asp:ListItem>Student Wise</asp:ListItem>
                         <asp:ListItem>Teacher Wise</asp:ListItem>
                         <asp:ListItem>Class Wise</asp:ListItem>
                     </asp:RadioButtonList></td>
             <td style="width: 88px">Status</td>
             <td><asp:DropDownList ID="cboBookStatus" runat="server" CssClass="Dropdown">
                             <asp:ListItem>Issued</asp:ListItem>
                             <asp:ListItem>Returned</asp:ListItem>
                         </asp:DropDownList></td>
         </tr>
        
         <tr>
             <td style="width: 531px; height: 31px"><asp:Label ID="lblName" runat="server" Text="Student Admn No"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                   <asp:DropDownList ID="cboClass" runat="server" Visible="False" AutoPostBack="True" CssClass="Dropdown">
                </asp:DropDownList>
                <asp:TextBox ID="txtName" runat="server" CssClass="textbox"></asp:TextBox>
             </td>
             <td style="text-align:left; width: 88px; height: 31px;">
                   <asp:Label ID="lblSection" runat="server" Text="Section" Visible="False"></asp:Label>
             </td>
             
             <td style="height: 31px">
                 <asp:DropDownList ID="cboSection" runat="server" Visible="False" CssClass="Dropdown">
                </asp:DropDownList>
               
             &nbsp;&nbsp;&nbsp;&nbsp;
                  <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn btn-sm btn-primary" />
               
             </td>
         </tr>
        
         <tr>
            <td colspan="3" >
               

                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" GridLines="Horizontal" Width="100%" CssClass="Grid">
                    <Columns>
                        <asp:TemplateField HeaderText="Sr No" HeaderStyle-Width="5%" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField> 
                        <asp:BoundField DataField="SName" HeaderText="Name" SortExpression="SName" />
                        <asp:BoundField DataField="ClassName" HeaderText="Class" SortExpression="ClassName" />
                        <asp:BoundField DataField="SecName" HeaderText="Section" SortExpression="SecName" />
                        <asp:BoundField DataField="AccNo" HeaderText="Acc. No" SortExpression="BookAccNo" />
                        <asp:BoundField DataField="BookTitle" HeaderText="Title" SortExpression="BookTitle" />
                        <asp:BoundField DataField="IssueDate" HeaderText="Issue Date" SortExpression="IssueDate" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField DataField="ActualReturnDate" HeaderText="Return Date" SortExpression="ActualReturnDate" DataFormatString="{0:dd/MM/yyyy}" />
                         <asp:BoundField DataField="Fine" HeaderText="Fine" SortExpression="Fine" />
                    </Columns>
                    <FooterStyle BackColor="White" ForeColor="#333333" />
                    <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="White" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F7F7F7" />
                    <SortedAscendingHeaderStyle BackColor="#487575" />
                    <SortedDescendingCellStyle BackColor="#E5E5E5" />
                    <SortedDescendingHeaderStyle BackColor="#275353" />
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT [SName], [ClassName], [SecName], [AccNo], [BookTitle], [IssueDate], [ActualReturnDate],[Fine] FROM [vwBookTransactStudent] WHERE ([RegNo] = @RegNo)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="txtName" DefaultValue="&quot;&quot;" Name="RegNo" PropertyName="Text" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CellPadding="4"  GridLines="Horizontal" CssClass="Grid" Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderText="Sr No" HeaderStyle-Width="5%" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField> 
                        <asp:BoundField DataField="EmpName" HeaderText="Name" SortExpression="EmpName" />
                        <asp:BoundField DataField="DeptName" HeaderText="Department" SortExpression="DeptName" />
                        <asp:BoundField DataField="DesgName" HeaderText="Designation" SortExpression="DesgName" />
                        <asp:BoundField DataField="AccNo" HeaderText="Book Acc No" SortExpression="BookAccNo" />
                        <asp:BoundField DataField="BookTitle" HeaderText="Book Title" SortExpression="BookTitle" />
                        <asp:BoundField DataField="IssueDate" HeaderText="Issue Date" SortExpression="IssueDate" />
                    </Columns>
                    <FooterStyle BackColor="White" ForeColor="#333333" />
                    <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="White" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F7F7F7" />
                    <SortedAscendingHeaderStyle BackColor="#487575" />
                    <SortedDescendingCellStyle BackColor="#E5E5E5" />
                    <SortedDescendingHeaderStyle BackColor="#275353" />
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSourceTeacher" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT [EmpName], [DeptName], [DesgName], [AccNo], [BookTitle], [IssueDate],[Fine] FROM [vwBookTransactEmployee]"></asp:SqlDataSource>
        </tr>
        
         </table>
</asp:Content>
