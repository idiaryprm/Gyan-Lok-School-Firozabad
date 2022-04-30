<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/IssueBookMaster.Master" CodeBehind="IssueBook.aspx.vb" Inherits="iDiary_V3.IssueBook" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SubHeading" runat="server">
    Issue/Return Books
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="LibraryMasterContents" runat="server">
        <table class="table">
        <tr>
            <td width="15%" valign="top" align="left">Accession No.</td>
            <td width="30%" valign="top" align="left">
                <asp:TextBox ID="txtBookAccNo" runat="server" CssClass="textbox"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnNext" runat="server" Text="Next" class="btn btn-sm btn-primary" />
            </td>
            <td valign="top" align="left" style="width: 8%">&nbsp;</td>
            <td valign="top" align="left" style="color: RED; width: 22%;">
                <asp:TextBox ID="txtAccNo" runat="server" CssClass="textbox" Visible="False"></asp:TextBox></td>
            <td width="30%" valign="top" align="left">&nbsp;</td>
        </tr>
        

        <tr>
            <td width="15%" valign="top" align="left">Title</td>
            <td width="30%" valign="top" align="left" style="margin-left: 40px">
                <asp:TextBox ID="txtTitle" runat="server" CssClass="textbox"></asp:TextBox>
            </td>
            <td valign="top" align="left" style="width: 8%">&nbsp;</td>
            <td valign="top" align="left" style="color: RED; width: 22%;">Publisher</td>
            <td width="30%" valign="top" align="left">
                <asp:TextBox ID="txtPub" runat="server" CssClass="textbox" Enabled="False" ></asp:TextBox>
                                        </td>
        </tr>
        

        <tr>
            <td width="15%" valign="top" align="left">Authors</td>
            <td width="30%" valign="top" align="left" style="margin-left: 80px" rowspan="3">
                <asp:TextBox ID="txtAuthor" runat="server" Width="280px" Enabled="False" 
                    Height="34px" CssClass="textbox"></asp:TextBox>
            </td>
            <td valign="top" align="left" style="width: 8%">&nbsp;</td>
            <td valign="top" align="left" style="width: 22%">Book Category</td>
            <td width="30%" valign="top" align="left">
                <asp:TextBox ID="txtBookCat" runat="server"  Enabled="False" CssClass="textbox"></asp:TextBox>
            </td>
        </tr>
        

        <tr>
            <td width="15%" valign="top" align="left">&nbsp;</td>
            <td valign="top" align="left" style="width: 8%">&nbsp;</td>
            <td valign="top" align="left" style="width: 22%">&nbsp;</td>
            <td width="30%" valign="top" align="left">
                &nbsp;</td>
        </tr>
        

        <tr>
            <td width="15%" valign="top" align="left">&nbsp;</td>
            <td valign="top" align="left" style="width: 8%">&nbsp;</td>
            <td valign="top" align="left" style="width: 22%">&nbsp;</td>
            <td width="30%" valign="top" align="left">
                &nbsp;</td>
        </tr>
        

        <tr>
            <td width="15%" valign="top" align="left">
                <asp:Label ID="lblCode" runat="server"></asp:Label>
            </td>
            <td width="30%" valign="top" align="left" style="margin-left: 40px">
                <asp:TextBox ID="txtRegNo" runat="server" CssClass="textbox"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnRegNoNext" runat="server" Text="Next" class="btn btn-sm btn-primary" />
                &nbsp;&nbsp;
                <asp:TextBox ID="txtSID" runat="server" CssClass="textbox" Visible="False"></asp:TextBox>
            </td>
            <td valign="top" align="left" style="width: 8%">&nbsp;</td>
            <td valign="top" align="left" style="width: 22%">&nbsp;</td>
            <td width="30%" valign="top" align="left">
                &nbsp;</td>
        </tr>
        

        <tr>
            <td width="15%" valign="top" align="left">Name</td>
            <td width="30%" valign="top" align="left" style="margin-left: 40px">
                <asp:TextBox ID="txtStudentName" runat="server" CssClass="textbox"></asp:TextBox>
            &nbsp;&nbsp; <asp:Button ID="btnSearch" runat="server" Text="Next" class="btn btn-sm btn-primary"/>
            </td>
            <td valign="top" align="left" style="width: 8%">&nbsp;</td>
            <td valign="top" align="left" style="width: 22%">Issue Date</td>
            <td width="30%" valign="top" align="left">
                <asp:Label ID="lblIssueDate" runat="server" Text="Label"></asp:Label>
                                </td>
        </tr>
        

        <tr>
            <td width="15%" valign="top" align="left" colspan="2" style="width: 45%">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="2" DataSourceID="SqlDataSourceStudent" ForeColor="Black" GridLines="Horizontal" Height="75px" Width="400px">
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
                <asp:SqlDataSource ID="SqlDataSourceStudent" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT [RegNo], [SName], [ClassName], [SecName] FROM [vw_Student]"></asp:SqlDataSource>
            </td>
            <td valign="top" align="left" style="width: 8%">&nbsp;</td>
            <td valign="top" align="left" style="width: 22%">Expected Return Date</td>
            <td width="30%" valign="top"><asp:TextBox ID="txtDDEDOR" runat="server" Width="33px" CssClass="textbox"></asp:TextBox>&nbsp;/
                <asp:TextBox ID="txtMMEDOR" runat="server" Width="33px" CssClass="textbox"></asp:TextBox>&nbsp;/
                <asp:TextBox ID="txtYYEDOR" runat="server" Width="61px" CssClass="textbox"></asp:TextBox>
            </td>
        </tr>
        

        <tr>
            <td width="15%" valign="top" align="left">
                <asp:Label ID="lblCourseDept" runat="server"></asp:Label>
            </td>
            <td width="30%" valign="top" align="left" style="margin-left: 40px">
                <asp:TextBox ID="txtClassSection" runat="server" Enabled="False" CssClass="textbox" Width="282px"></asp:TextBox>
            </td>
            <td valign="top" align="left" style="width: 8%">&nbsp;</td>
            <td valign="top" align="left" style="width: 22%">Actual Return Date</td>
            <td width="30%" valign="top" align="left">
                <asp:Label ID="lblActualReturnDate" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        

        <tr>
            <td width="15%" valign="top" align="left">
                <asp:CheckBox ID="chkFine" runat="server" Font-Bold="True" ForeColor="Red" Text="Paid Fine" Visible="False" />
            </td>
            <td valign="top" align="left" style="margin-left: 40px; " colspan="2">
             
                <asp:Label ID="lblFine" runat="server" Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label>
                <asp:Label ID="lblClass" runat="server" Visible="False"></asp:Label>
            </td>
            <td width="15%" valign="top" align="left" colspan="2" rowspan="3" style="width: 45%">
                <asp:ListBox ID="lstBooks" runat="server" Width="284px" Visible="False"></asp:ListBox>
            </td>
        </tr>
        

        <tr>
            <td width="15%" valign="top" align="left">&nbsp;</td>
            <td width="30%" valign="top" align="left" style="margin-left: 40px">
             
                <asp:Button ID="btnIssue" runat="server" Text="Issue" class="btn btn-primary" />
&nbsp;
                <asp:Button ID="btnReturn" runat="server" Text="Return" class="btn btn-primary" />
&nbsp;
                <asp:Button ID="btnBack" runat="server" Text="Back" class="btn btn-primary"/>
            </td>
            <td valign="top" align="left" style="width: 8%">&nbsp;</td>
        </tr>
        

        <tr>
            <td width="15%" valign="top" align="left">&nbsp;</td>
            <td width="30%" valign="top" align="left" style="margin-left: 40px">
                &nbsp;</td>
            <td valign="top" align="left" style="width: 8%">&nbsp;</td>
        </tr>
        

        <tr>
            <td width="15%" valign="top" align="left">
                <asp:TextBox ID="txtBookTransactID" runat="server" Width="44px" Visible="False"></asp:TextBox>
            </td>
            <td width="30%" valign="top" align="left" style="margin-left: 40px">
                <asp:Label ID="lblStatus" runat="server" Font-Bold="True" Font-Size="Medium" 
                    ForeColor="Navy"></asp:Label>
            </td>
            <td valign="top" align="left" style="width: 8%">&nbsp;</td>
            <td valign="top" align="left" style="width: 22%">
                &nbsp;</td>
            <td width="30%" valign="top" align="left">&nbsp;</td>
        </tr>
        
        </table>
</asp:Content>
