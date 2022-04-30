<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="SearchDocument.aspx.vb" Inherits="iDiary_V3.SearchDocument" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Heading" runat="server">
    Search Documents
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <table width="100%" cellpadding="1" cellspacing="1" border="0">
        <tr>
            <td width="15%" height="25">
                <asp:CheckBox ID="chkFileNo" runat="server" Text="File No" />
            </td>
            <td style="width: 16%">
                <asp:TextBox ID="txtFileNo" runat="server" Width="130px" BorderWidth="1px"></asp:TextBox>
            </td>

            <td width="15%">
                <asp:CheckBox ID="chkSubject" runat="server" Text="Subject" />
            </td>
            <td class="td_width_16" style="width: 17%">
                <asp:TextBox ID="txtSubject" runat="server" Width="130px" BorderWidth="1px"></asp:TextBox>
            </td>

            <td width="15%">
                <asp:CheckBox ID="chkContents" runat="server" Text="Contents" />
            </td>
            <td width="15%">
                <asp:TextBox ID="txtContents" runat="server" Width="130px" BorderWidth="1px"></asp:TextBox>
            </td>
           
            <td width="10%" align="right">
                &nbsp;</td>
        </tr>

        <tr>
            <td width="15%" height="25">
                <asp:CheckBox ID="chkDocDate" runat="server" Text="Doc Date From" />
            </td>
            <td style="width: 16%">
                <asp:TextBox ID="txtDocDate_DD" runat="server" Width="25px" BorderWidth="1px"></asp:TextBox>&nbsp;/
                <asp:TextBox ID="txtDocDate_MM" runat="server" Width="25px" BorderWidth="1px"></asp:TextBox>&nbsp;/
                <asp:TextBox ID="txtDocDate_YY" runat="server" Width="45px" BorderWidth="1px"></asp:TextBox>
            </td>

            <td width="15%">
                Doc Date To</td>
            <td class="td_width_16" style="width: 17%">
                <asp:TextBox ID="txtDocDate_DDTo" runat="server" Width="25px" 
                    BorderWidth="1px"></asp:TextBox>&nbsp;/
                <asp:TextBox ID="txtDocDate_MMTo" runat="server" Width="25px" 
                    BorderWidth="1px"></asp:TextBox>&nbsp;/
                <asp:TextBox ID="txtDocDate_YYTo" runat="server" Width="45px" 
                    BorderWidth="1px"></asp:TextBox>
            </td>

            <td width="15%">
                &nbsp;</td>
            <td width="15%">
                <asp:ImageButton ID="btnFind" runat="server" ImageUrl="~/images/search.png" 
                    style="height: 19px" />
            </td>
           
            <td width="10%" align="right">
                &nbsp;</td>
        </tr>

        <tr>
            <td width="15%" height="25">
                <asp:Label ID="lblStatus" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
            </td>
            <td style="width: 16%">
                &nbsp;</td>

            <td width="15%">
                &nbsp;</td>
            <td class="td_width_16" style="width: 17%">
                &nbsp;</td>

            <td width="15%">
                &nbsp;</td>
            <td width="15%">
                &nbsp;</td>
           
            <td width="10%" align="right">
                &nbsp;</td>
        </tr>

        <tr>
            <td colspan="6">
                &nbsp;</td>
           
            <td width="10%" align="right">
                &nbsp;</td>
        </tr>

        <tr>
            <td colspan="7">
                <div id="gvDiv">
                    <asp:Label ID="lblSchoolName" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
                    <br />
                    <asp:Label ID="lblTitle" runat="server" Font-Bold="True" Font-Size="Small"></asp:Label>
                    <br /><br />
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" 
                    CellPadding="4" DataSourceID="SqlDataSource1" Width="100%" AllowSorting="True">
                    <RowStyle BackColor="White" ForeColor="#330099" />
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" />
                        <asp:BoundField DataField="FileNo" HeaderText="File No" 
                            SortExpression="FileNo" >
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="FileDate" HeaderText="Date" 
                            SortExpression="FileDate" DataFormatString="{0:d}" HtmlEncode="False" >
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle Width="80px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="FileSubject" HeaderText="Subject" 
                            SortExpression="FileSubject" >
                        <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="FileContents" HeaderText="Contents" 
                            SortExpression="FileContents" >
                        <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="FilePath" HeaderText="File Path" 
                            SortExpression="FilePath" >
                        <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                    </Columns>
                    
                    <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                    <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                </asp:GridView>
                
                <br />
                
                <asp:Label ID="lblTotalRecords" runat="server"></asp:Label>
                </div>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" 
                    SelectCommand="SELECT [FileNo], [FileDate], [FileSubject], [FileContents], [FilePath] FROM [rptDocumentSearch]">
                </asp:SqlDataSource>
                <br />
                <asp:Button ID="btnPrint" runat="server" Text="Print" Width="82px" />
            &nbsp;&nbsp;
                <asp:Button ID="btnExcel" runat="server" Text="Export to Excel" Width="125px" />
            </td>
        </tr>

        <tr>
            <td colspan="7">
                &nbsp;
                <br />
            </td>
        </tr>
        
        <tr>
            <td colspan="7">
                &nbsp;</td>
        </tr>
        
    </table>


</asp:Content>
