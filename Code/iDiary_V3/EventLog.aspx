<%@ Page Language="VB" MasterPageFile="~/AdminMaster.master" AutoEventWireup="false" Inherits="iDiary_V3.Admin_EventLog" title="Untitled Page" Codebehind="EventLog.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AdminMasterContents" Runat="Server">
   
    <table class="table">
        <tr>
            <td colspan="3">
    <asp:BulletedList ID="BulletedListLOG" runat="server" BackColor="#FFFFCC" BorderColor="#660033" BorderStyle="Solid" BorderWidth="1px" BulletImageUrl="~/Images/arrow.gif" BulletStyle="CustomImage" Font-Bold="False" Font-Size="Small" ForeColor="Black" Height="16px" Width="718px" Visible="False">
    </asp:BulletedList>
            </td>
        </tr>
        <tr>
            <td style="width: 148px"><strong>Select Event Type</strong><asp:CheckBoxList ID="chkEventType" runat="server" Width="149px">
                </asp:CheckBoxList>
            </td>
            <td style="width: 173px">
            
                <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="True" Text="Select All/ None" style="font-weight: 700" />
                </td>
            <td>
                <asp:Button ID="btnFind" runat="server" Text=">>"  CssClass="btn btn-sm btn-primary"/>
            </td>
        </tr>
        <tr>
            <td colspan="3">
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" DataSourceID="SqlDataSourcelog" GridLines="Horizontal" PageSize="12" Width="740px" Visible="False" CssClass="Grid">
        <Columns>
            <asp:BoundField DataField="logTime" HeaderText="logTime" SortExpression="logTime">
            </asp:BoundField>
            <asp:BoundField DataField="EventType" HeaderText="EventType" SortExpression="EventType">
            </asp:BoundField>
            <asp:BoundField DataField="Details" HeaderText="Details" SortExpression="Details">
            </asp:BoundField>
            
            <asp:BoundField DataField="UserName" HeaderText="UserName" SortExpression="UserName">
            </asp:BoundField>
        </Columns>
        <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
        <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
        <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
        <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F7F7F7" />
        <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
        <SortedDescendingCellStyle BackColor="#E5E5E5" />
        <SortedDescendingHeaderStyle BackColor="#242121" />
    </asp:GridView>
            </td>
        </tr>
    </table>
    <br />
    <asp:SqlDataSource ID="SqlDataSourcelog" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT [logTime], [EventType], [Details], [UserName] FROM [vw_Event_log]"></asp:SqlDataSource>
    <br />
    <br />
    <br />
</asp:Content>

