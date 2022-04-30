<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="NewsPaperReport.aspx.vb" Inherits="iDiary_V3.NewsPaperReport" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Heading" runat="server">
    News Paper Report 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="col_3" style="margin-top: 20px;" id="panelEnquiryNo" runat="server">
        <div class="col-md-3 widget widget1" style="width: 100%">
            <div class="r3_counter_box">
                <table class="table">
        <tr>
            <td height="25" style="width: 17%">
                Date&nbsp;From</td>
            <td width="15%">
                <asp:TextBox ID="txtDateFrom" runat="server" CssClass="textbox" ></asp:TextBox>
                <asp:CalendarExtender ID="txtDateFrom_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDateFrom">
                </asp:CalendarExtender>
            </td>

            <td style="width: 18%">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                Date To</td>
            <td width="15%">
                <asp:TextBox ID="txtDateTo" runat="server" CssClass="textbox"></asp:TextBox>
                <asp:CalendarExtender ID="txtDateTo_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDateTo">
                </asp:CalendarExtender>
            </td>

            <td width="15%">
                <asp:Button ID="btnFind" runat="server" Text="Next" CssClass="btn btn-primary"/>
            </td>
            <td width="15%" style="margin-left: 40px">
                &nbsp;</td>
           
            <td width="10%" align="Left">
                &nbsp;</td>
        </tr>
        <tr>
            <td height="25" style="width: 17%">
                &nbsp;</td>
            <td width="15%">
                &nbsp;</td>

            <td style="width: 18%">
                &nbsp;</td>
            <td width="15%">
                &nbsp;</td>

            <td width="15%">
                &nbsp;</td>
            <td width="15%" style="margin-left: 40px">
                &nbsp;</td>
           
            <td width="10%" align="Left">
                &nbsp;</td>
        </tr>
        <tr>
            <td height="25" colspan="4" valign="top">
                <asp:GridView ID="gvAttendance" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4" DataSourceID="SqlDataSourceAttendance" GridLines="Horizontal" Width="603px">
                    <Columns>
                        <asp:BoundField DataField="NewsPaperName" HeaderText="News Paper" SortExpression="NewsPaperName" />
                        <asp:BoundField DataField="DateIn" HeaderText="Date" SortExpression="DateIn" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField DataField="Present" HeaderText="Present/Absent" SortExpression="Present" />
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
            </td>

            <td width="15%" valign="top" colspan="2" style="width: 30%">
                <strong>
                <br />
                <asp:Label ID="lblPresent" runat="server"></asp:Label>
                <br />
                <br />
                <asp:Label ID="lblAbsent" runat="server"></asp:Label>
                </strong></td>
           
            <td width="10%" align="Left" valign="top">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="height: 25px;" colspan="7">
                <asp:SqlDataSource ID="SqlDataSourceAttendance" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT [NewsPaperName], [DateIn], [Present] FROM [vwNewsPaper]"></asp:SqlDataSource>
               <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            </td>
        </tr>
        <tr>
            <td height="25" style="width: 17%">
                &nbsp;</td>
            <td width="15%">
                &nbsp;</td>

            <td style="width: 18%">
                &nbsp;</td>
            <td width="15%">
                &nbsp;</td>

            <td width="15%">
                &nbsp;</td>
            <td width="15%">
                &nbsp;</td>
           
            <td width="10%" align="right">
                &nbsp;</td>
        </tr>
    </table>
            </div>
        </div>
        <div class="clearfix"></div>
    </div>
</asp:Content>

