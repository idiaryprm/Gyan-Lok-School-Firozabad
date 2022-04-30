<%@ Page Language="VB" MasterPageFile="~/AdminMaster.master" AutoEventWireup="false" Inherits="iDiary_V3.Admin_Updates" title="Untitled Page" Codebehind="UpcomingActivities.aspx.vb" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminMasterContents" Runat="Server">
    <table class="table">
        <tr>
            <td style="text-decoration: underline; font-size: 14px;" colspan="2">
        <strong>UPCOMING ACTIVITIES</strong></td>
        </tr>
        <tr>
            <td style="width: 17%">
                Activity Date</td>
            <td style="width: 50%">
                <asp:TextBox ID="txtDate" runat="server" BorderStyle="Solid" BorderWidth="1px" 
                    Width="110px"></asp:TextBox>
                <asp:CalendarExtender ID="txtDate_CalendarExtender" runat="server" Format="dd/MM/yyyy"  TargetControlID="txtDate">
                </asp:CalendarExtender>

            </td>
        </tr>
        <tr>
            <td style="width: 17%">
                Activity Details</td>
            <td style="width: 50%">
                <asp:TextBox ID="txtActivityDetails" runat="server" BorderWidth="1px" 
                    Width="300px" Height="85px" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 17%">
                Upload File</td>
            <td style="width: 50%" valign="middle">
                <asp:FileUpload ID="myFile" runat="server" BorderWidth="1px" Width="300px" />
            </td>
        </tr>
        <tr>
            <td style="width: 17%">
                &nbsp;</td>
            <td style="width: 50%">
                <asp:Label ID="lblStatus" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:GridView ID="gvActivities" runat="server" AutoGenerateColumns="False" DataKeyNames="UPID" DataSourceID="SqlDataSourceActivities" CellPadding="4" GridLines="None" AllowPaging="True" PageSize="8" CssClass="Grid" Width="537px">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:CommandField ShowDeleteButton="True" />
                        <asp:BoundField DataField="ActivityDate" HeaderText="Activity Date" SortExpression="ActivityDate" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField DataField="Detail" HeaderText="Details" SortExpression="Detail" />
                        <asp:BoundField DataField="Path" HeaderText="Path" SortExpression="Path" />
                    </Columns>
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSourceActivities" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT [UPID], [ActivityDate], [Detail], [Path] FROM [UpcomingActivities] ORDER BY [ActivityDate] DESC" DeleteCommand="DELETE FROM [UpcomingActivities] WHERE [UPID] = @UPID" InsertCommand="INSERT INTO [UpcomingActivities] ([ActivityDate], [Detail], [Path]) VALUES (@ActivityDate, @Detail, @Path)" UpdateCommand="UPDATE [UpcomingActivities] SET [ActivityDate] = @ActivityDate, [Detail] = @Detail, [Path] = @Path WHERE [UPID] = @UPID">
                    <DeleteParameters>
                        <asp:Parameter Name="UPID" Type="Int32" />
                    </DeleteParameters>
                    <InsertParameters>
                        <asp:Parameter Name="ActivityDate" DbType="Date" />
                        <asp:Parameter Name="Detail" Type="String" />
                        <asp:Parameter Name="Path" Type="String" />
                    </InsertParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="ActivityDate" DbType="Date" />
                        <asp:Parameter Name="Detail" Type="String" />
                        <asp:Parameter Name="Path" Type="String" />
                        <asp:Parameter Name="UPID" Type="Int32" />
                    </UpdateParameters>
                </asp:SqlDataSource>

            </td>
        </tr>
        <tr>
            <td style="width: 17%">
                &nbsp;</td>
            <td style="width: 50%">
                <asp:Button ID="btnSave" runat="server" Text="Save Information" CssClass="btn btn-primary"/>
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            </td>
        </tr>
        <tr>
            <td style="width: 17%">
                &nbsp;</td>
            <td style="width: 50%">
                &nbsp;</td>
        </tr>
        </table>
</asp:Content>

