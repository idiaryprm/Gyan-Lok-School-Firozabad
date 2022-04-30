<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="LibraryBookHistory.aspx.vb" Inherits="iDiary_V3.LibraryBookHistory" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Heading" runat="server">
    Library Issue / Return History
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <table width="100%" cellpadding="2" cellspacing="2" border="0">
        <tr>
            <td style="width: 176px">&nbsp;</td>
            <td style="width: 197px">

                &nbsp;</td>
            <td style="width: 75px">&nbsp;</td>
            <td style="width: 192px">

                &nbsp;</td>
            <td style="width: 52px">
                &nbsp;</td>
            <td style="width: 161px">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 176px"><strong>Acc. No.</strong></td>
            <td style="width: 197px">

                <asp:TextBox ID="txtBookAcc" runat="server" Width="140px"></asp:TextBox>

            </td>
            <td style="width: 75px">
                <asp:Button ID="btnFind" runat="server" BorderStyle="Solid" BorderWidth="1px" Text="Find" Width="61px" />
            </td>
            <td style="width: 192px">

                &nbsp;</td>
            <td style="width: 52px">
                &nbsp;</td>
            <td style="width: 161px">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 176px">
                <strong>Issued to</strong></td>
            <td colspan="3">
                <asp:Label ID="lblIssuedTo" runat="server" BorderStyle="None"></asp:Label>
            </td>
            <td colspan="3">&nbsp;</td>
        </tr>
            <tr>
                <td colspan="7">
                    <h3 style="color: #990000">
                  <b>Library : Student History</b></h3>
&nbsp;<div style="height:200px;overflow:scroll ">
                        <asp:GridView ID="gvStudent" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4" GridLines="Horizontal">
                            <Columns>
                                <asp:BoundField DataField="BookTitle" HeaderText="BookTitle" SortExpression="BookTitle" />
                                <asp:BoundField DataField="RegNo" HeaderText="RegNo" SortExpression="RegNo" />
                                <asp:BoundField DataField="SName" HeaderText="Student Name" SortExpression="SName" />
                                <asp:BoundField DataField="ClassName" HeaderText="Class" SortExpression="ClassName" />
                                <asp:BoundField DataField="SecName" HeaderText="Section" SortExpression="SecName" />
                                <asp:BoundField DataField="IssueDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="IssueDate" SortExpression="IssueDate" />
                                <asp:BoundField DataField="ExpectedReturnDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Expected Return Date" SortExpression="ExpectedReturnDate" />
                                <asp:BoundField DataField="ActualReturnDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Actual Return Date" SortExpression="ActualReturnDate" />
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
                        <asp:SqlDataSource ID="SqlDataSourceStudent" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT [AccNo], [IssueDate], [ExpectedReturnDate], [ActualReturnDate], [SName], [ClassName], [SecName], [RegNo], [BookTitle], [BookAccNo] FROM [vwBookTransactStudent]"></asp:SqlDataSource>
                    </div>
                </td>
            </tr>
        <tr>
            <td colspan="7">
                <h3 style="color: #800000">
                <b>Library : Teacher History</b></h3>
&nbsp;<div style="height:200px;overflow:scroll ">
                <asp:GridView ID="gvTeacher" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4" GridLines="Horizontal" >
                    <Columns>
                        <asp:BoundField DataField="BookTitle" HeaderText="Book Title" SortExpression="BookTitle" />
                        <asp:BoundField DataField="EmpCode" HeaderText="Employee Code" SortExpression="EmpCode" />
                        <asp:BoundField DataField="EmpName" HeaderText="Employee Name" SortExpression="EmpName" />
                        <asp:BoundField DataField="IssueDate" HeaderText="Issue Date" SortExpression="IssueDate" DataFormatString="{0:dd/MM/yyyy}" />
                       <asp:BoundField DataField="ExpectedReturnDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Expected Return Date" SortExpression="ExpectedReturnDate" />
                        <asp:BoundField DataField="ActualReturnDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Actual Return Date" SortExpression="ActualReturnDate" />
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
                <asp:SqlDataSource ID="SqlDataSourceTeaacher" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT [BookAccNo], [BookTitle], [EmpCode], [EmpName], [IssueDate], [ExpectedReturnDate], [ActualReturnDate] FROM [vwBookTransactEmployee]"></asp:SqlDataSource>
                     </div> 
            </td>
        </tr>
        <tr>
            <td colspan="7">
                <asp:Label ID="lblStatus" runat="server" Font-Bold="True" ForeColor="Navy"></asp:Label>
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <asp:Label ID="lblAccNo" runat="server" Visible="False"></asp:Label>
            </td>
        </tr>
    </table>

</asp:Content>
