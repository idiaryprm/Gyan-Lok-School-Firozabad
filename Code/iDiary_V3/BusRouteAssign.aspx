<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/BusMaster.master" CodeBehind="BusRouteAssign.aspx.vb" Inherits="iDiary_V3.BusRouteAssign" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BusMasterContents" runat="server">
    <table cellpadding="2" cellspacing="2" border="0" width="600px">
        <tr>
            <td style="height :22px; text-decoration: underline;" colspan="2">

                <strong>Assign Bus Route</strong></td>
            <td style="width:120px; height :22px">

            </td>
            <td style="width:120px; height :22px">

            </td>
            <td style="width:120px; height :22px">

            </td>
        </tr>

        <tr>
            <td style="width:120px; height :22px">

                Choose Bus</td>
            <td style="width:120px; height :22px">

                <asp:DropDownList ID="cboBus" runat="server" Width="100px">
                </asp:DropDownList>
            </td>
            <td style="width:120px; height :22px">

                Choose Route</td>
            <td style="width:120px; height :22px">

                <asp:DropDownList ID="cboRoute" runat="server" Width="100px">
                </asp:DropDownList>
            </td>
            <td style="width:120px; height :22px">

                <asp:Button ID="btnAssign" runat="server" Text="Assign" />

            </td>
        </tr>

        <tr>
            <td style="width:120px; height :22px">

                &nbsp;</td>
            <td style="width:120px; height :22px">

                &nbsp;</td>
            <td style="width:120px; height :22px">

                &nbsp;</td>
            <td style="width:120px; height :22px">

                &nbsp;</td>
            <td style="width:120px; height :22px">

            </td>
        </tr>

        <tr>
            <td style="height :22px" colspan="5">

                <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical">
                    <AlternatingRowStyle BackColor="White" />
                    <FooterStyle BackColor="#CCCC99" />
                    <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                    <RowStyle BackColor="#F7F7DE" />
                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#FBFBF2" />
                    <SortedAscendingHeaderStyle BackColor="#848384" />
                    <SortedDescendingCellStyle BackColor="#EAEAD3" />
                    <SortedDescendingHeaderStyle BackColor="#575357" />
                </asp:GridView>
            </td>
        </tr>

        <tr>
            <td style="width:120px; height :22px">

                &nbsp;</td>
            <td style="width:120px; height :22px">

                &nbsp;</td>
            <td style="width:120px; height :22px">

                &nbsp;</td>
            <td style="width:120px; height :22px">

                &nbsp;</td>
            <td style="width:120px; height :22px">

                &nbsp;</td>
        </tr>

    </table>
</asp:Content>
