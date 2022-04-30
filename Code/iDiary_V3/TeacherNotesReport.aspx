<%--<%@ Page Title="Teacher Notes" Language="VB" MasterPageFile="~/MasterPage.Master" AutoEventWireup="false" CodeBehind="TeacherNotesReport.aspx.vb" Inherits="iDiary_V3.TeacherNotesReport" %>--%>
<%@ Page Title="Teacher Notes" Language="VB" MasterPageFile="~/TeacherMasterPage.Master" AutoEventWireup="false" CodeBehind="TeacherNotesReport.aspx.vb" Inherits="iDiary_V3.TeacherNotesReport" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content2" ContentPlaceHolderID="TeacherContents" runat="server">
    <div>
    
        <table>
        <tr>
            <td align="left" colspan="2">

                <table style="width: 100%">
                    <tr>
                        <td>From</td>
                        <td>

                <asp:TextBox ID="txtDateFrom" runat="server" Width="140px"></asp:TextBox>

                            <asp:CalendarExtender ID="txtDateFrom_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDateFrom" TodaysDateFormat="dd/MM/yyyy">
                            </asp:CalendarExtender>

                        </td>
                        <td>To</td>
                        <td>

                <asp:TextBox ID="txtDateTo" runat="server" Width="140px"></asp:TextBox>

                            <asp:CalendarExtender ID="txtDateTo_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDateTo" TodaysDateFormat="dd/MM/yyyy">
                            </asp:CalendarExtender>

                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="width: 177px" align="left">

                &nbsp;</td>
            <td style="width: 265px" align="left">

                &nbsp;</td>
        </tr>
        
        <tr>
            <td style="width: 177px" align="left">

                Teacher Name</td>
            <td style="width: 265px" align="left">

                <asp:DropDownList ID="DDLTeacherName" runat="server" Width="200px">
                </asp:DropDownList>

            </td>
        </tr>
        
        <tr>
            <td align="center" colspan="2">

                &nbsp;</td>
        </tr>
        <tr>
            <td align="center" colspan="2">

                <asp:Button ID="btnShow" runat="server" Text="Show" Width="80px" />

            </td>
        </tr>
        <tr>
            <td align="left" colspan="2">

            <asp:GridView ID="DBGrid" runat="server" 
            CellPadding="4" Font-Size="X-Small" GridLines="None" 
            HorizontalAlign="Center" Width="100%" ForeColor="#333333" AutoGenerateColumns="False" DataKeyNames="TNID">
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" 
                Height="30px" />
            <AlternatingRowStyle BackColor="White" VerticalAlign="Top" ForeColor="#284775" />
                <Columns>
                    <asp:BoundField DataField="TNID" HeaderText="ID" SortExpression="TNID" ReadOnly="True" />
                    <asp:BoundField DataField="EmpName" HeaderText="Teacher Name" SortExpression="EmpName" />
                    <asp:BoundField DataField="TNDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Date" SortExpression="TNDate" />
                    <asp:BoundField DataField="TNDesc" HeaderText="Desc" SortExpression="TNDesc" />
                </Columns>
            <EditRowStyle Font-Size="Small" BackColor="#999999" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
        </asp:GridView>   
            </td>
        </tr>
        <tr>
            <td align="left" colspan="2">

               <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT [TNID], [TNDate], [TNDesc], [EmpName] FROM [vw_TeacherNote]"></asp:SqlDataSource>
            </td>
        </tr>
    </table>
    
    </div>
</asp:Content>
