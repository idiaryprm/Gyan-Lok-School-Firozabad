<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/iDiaryPanel/Parent/ParentMaster.master" CodeBehind="viewLeave.aspx.vb" Inherits="iDiary_V3.viewLeave" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <p style="font-size: 14px; text-decoration: underline">
        <strong>Student Leave History</strong></p>
    <table width="100%" cellpadding="2" cellspacing="2" border="0">
        <tr>
            <td style="width:140px">

                Choose From Date</td>
            <td style="width:140px">

                <asp:TextBox ID="txtFromDate" runat="server" Width="70px"></asp:TextBox>
                <asp:CalendarExtender ID="txtTo_CalendarExtender" Format="dd/MM/yyyy" runat="server" TargetControlID="txtFromDate">
                    </asp:CalendarExtender>

            </td>
            <td style="width:140px">

                To Date</td>
            <td style="width:140px">

                <asp:TextBox ID="txtToDate" runat="server" Width="70px"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" runat="server" TargetControlID="txtToDate">
                    </asp:CalendarExtender>

            </td>
        </tr>
        <tr>
            <td style="width:140px">

                &nbsp;&nbsp;<asp:Button ID="btnFilter" runat="server" BorderStyle="Solid" BorderWidth="1px" Text="Filter" />

            </td>
            <td style="width:140px">

                &nbsp;</td>
            <td style="width:140px">

                &nbsp;</td>
            <td style="width:140px">

                &nbsp;</td>
        </tr>
    </table>
    <p style="font-size: 10pt">
        <asp:GridView ID="GridView1" runat="server" CellPadding="4" GridLines="None" AutoGenerateColumns="False" DataSourceID="SqlDataSourceLeave" ForeColor="#333333" Width="237px" AllowPaging="True" PageSize="4">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:BoundField DataField="fromDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="From Date" SortExpression="fromDate" />
                <asp:BoundField DataField="toDate" HeaderText="To Date" SortExpression="toDate" DataFormatString="{0:dd/MM/yyyy}">
                </asp:BoundField>
                <asp:BoundField DataField="Reason" HeaderText="Reason" SortExpression="Reason" />
                <asp:BoundField DataField="Message" HeaderText="Message" SortExpression="Message" />
            </Columns>
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#E9E7E2" />
            <SortedAscendingHeaderStyle BackColor="#506C8C" />
            <SortedDescendingCellStyle BackColor="#FFFDF8" />
            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSourceLeave" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT [fromDate], [toDate], [Reason], [Message] FROM [StudentLeave] WHERE ([SID] = @SID)">
            <SelectParameters>
                <asp:SessionParameter Name="SID" SessionField="SID" Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource><asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    </p>
    

</asp:Content>
