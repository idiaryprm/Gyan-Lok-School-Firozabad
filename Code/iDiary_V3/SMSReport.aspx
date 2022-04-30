<%@ Page Title="SMS Report" Language="VB" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="false" CodeBehind="SMSReport.aspx.vb" Inherits="iDiary_V3.SMSReport" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AdminMasterContents" runat="server">
    <div>
    
        <table style="width: 746px">
        <tr>
            <td align="left">

                <table style="width: 100%">
                    <tr>
                        <td>Date From</td>
                        <td style="width: 106px">

                <asp:TextBox ID="txtDateFrom" runat="server" Width="140px"></asp:TextBox>

                            <asp:CalendarExtender ID="txtDateFrom_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDateFrom" TodaysDateFormat="dd/MM/yyyy">
                            </asp:CalendarExtender>

                        </td>
                        <td>To</td>
                        <td style="width: 130px">

                <asp:TextBox ID="txtDateTo" runat="server" Width="140px"></asp:TextBox>

                            <asp:CalendarExtender ID="txtDateTo_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDateTo" TodaysDateFormat="dd/MM/yyyy">
                            </asp:CalendarExtender>

                        </td>
                        <td>Status</td>
                        <td>
                            <asp:DropDownList ID="cboStatus" runat="server" Width="120px">
                                <asp:ListItem>Delivered</asp:ListItem>
                                <asp:ListItem>UnDelivered</asp:ListItem>
                                <asp:ListItem>Missed</asp:ListItem>
                                <asp:ListItem>All</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>

                <asp:Button ID="btnShow" runat="server" Text="Show" Width="80px" />

                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center">
                <table style="width: 369px">
                    <tr>
                        <td>

                                                <asp:Label ID="lblDelivered" runat="server" Font-Bold="True" ForeColor="Blue"></asp:Label>

                                                </td>
                        <td>

                       

                            <asp:Label ID="lblUnDelivered" runat="server" Font-Bold="True" ForeColor="Blue"></asp:Label>

                       

                        </td>
                    </tr>
                </table>                
            </td>
        </tr>
        <tr>
            <td align="left">

          <asp:GridView ID="DBGrid" runat="server" AllowPaging="True" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" PageSize="50" Width="740px">
        <Columns>
            <asp:BoundField DataField="SMSDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Date" SortExpression="SMSDate">
            <ControlStyle Width="80px" />
            <ItemStyle Width="80px" />
            </asp:BoundField>
             <asp:BoundField DataField="MobileNo" HeaderText="MobileNo" SortExpression="MobileNo">
            <ControlStyle Width="80px" />
            <ItemStyle Width="80px" />
            </asp:BoundField>
             <asp:BoundField DataField="Message" HeaderText="Message" SortExpression="Message">
                <ControlStyle  Width="250px"/>
                <ItemStyle Width="250px" />
            </asp:BoundField>
            
            <asp:BoundField DataField="DLRStatusCode" HeaderText="DLR Code" SortExpression="DLRStatusCode">
            <ControlStyle Width="80px" />
            <ItemStyle Width="80px" />
            </asp:BoundField>
            <asp:BoundField DataField="Detail" HeaderText="DLR Status" SortExpression="Detail">
            <ControlStyle Width="150px" />
            <ItemStyle Width="150px" />
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
        <tr>
            <td align="left">

                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT [SMSDate], [MobileNo], [Message], [DLRStatusCode], [Detail] FROM vw_SMSDLR"></asp:SqlDataSource>
                <br />
            </td>
        </tr>
    </table>
    
    </div>
</asp:Content>
