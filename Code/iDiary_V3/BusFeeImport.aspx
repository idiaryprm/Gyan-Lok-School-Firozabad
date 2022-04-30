<%@ Page Language="VB" MasterPageFile="~/MasterPage.Master" AutoEventWireup="false" Inherits="iDiary_V3.ImportBusFee" title="Untitled Page" Codebehind="BusFeeImport.aspx.vb" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Heading" Runat="Server">
    Bus    Fee Import Wizard   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td width="10"></td>
            <td align="left" style="width: 118px; "></td>
            <td align="left" style="width: 193px; ">
                <asp:DropDownList ID="cboClass" runat="server" class="form-control1"
                    AutoPostBack="True" Visible="False">
                </asp:DropDownList>
                </td>
            <td align="left" style="width: 123px; "></td>
            <td align="left" style="width: 258px; ">
                <b>
                <asp:DropDownList ID="cboSection" runat="server" class="form-control1" Visible="False">
                </asp:DropDownList>
                </b>
                <asp:DropDownList ID="cboStatus" runat="server" class="form-control1" Visible="False">
                </asp:DropDownList>
                </td>
            <td align="left" style="width: 82px; "></td>
            <td align="left" style="width: 102px; ">
                <asp:DropDownList ID="cboTerm" runat="server" class="form-control1" Visible="False">
                </asp:DropDownList>
            </td>
            <td align="left">
                </td>
        </tr>
        <tr>
            <td width="10">&nbsp;</td>
            <td align="left" style="width: 118px; ">&nbsp;</td>
            <td align="left" style="width: 193px; ">
                &nbsp;</td>
            <td align="left" style="width: 123px; ">&nbsp;</td>
            <td align="left" style="width: 258px; ">
                &nbsp;</td>
            <td align="left" style="width: 82px; ">&nbsp;</td>
            <td align="left" style="width: 102px; ">
                &nbsp;</td>
            <td align="left">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="10" style="height: 21px"></td>
            <td align="left" style="width: 118px; height: 21px;">
                <asp:Label ID="lblFile" runat="server" style="font-weight: 700" Text="Select File"></asp:Label>
            </td>
            <td align="left" style="height: 21px; width: 193px;">
                <asp:FileUpload ID="myFile" runat="server" Width="236px" BorderWidth="1px" 
                    BorderColor="Black" />
            </td>
            <td align="left" style="height: 21px">
                </td>
            <td align="left" style="height: 21px; font-weight: 700;" colspan="3">
                <asp:Button ID="btnCheckExcel" runat="server" Text="Validate Bus Fee" Width="120px" />
            &nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnImportFee" runat="server" Text="Import Fee" Width="120px" Enabled="False" />
            </td>
            <td align="char" style="height: 21px">
                </td>
        </tr>
        <tr>
            <td width="10" style="height: 26px"></td>
            <td align="left" colspan="6" style="height: 26px">
                <asp:Label ID="lblStatus" runat="server" Font-Bold="True" ForeColor="Navy"></asp:Label>
            </td>
            <td align="left" style="height: 26px"></td>
        </tr>
        <tr>
            <td width="10" style="height: 26px">&nbsp;</td>
            <td align="left" colspan="6" style="height: 26px">
                <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4"  Height="16px" Width="100%">
                    <RowStyle BackColor="White" ForeColor="#330099" />
                    <Columns>
                        <asp:BoundField DataField="SNo" HeaderText="SNo" SortExpression="SNo" />
                        <asp:BoundField DataField="Dated" HeaderText="Dated" SortExpression="Dated" />
                        <asp:BoundField DataField="Details" HeaderText="Details" SortExpression="Details" />
                        <asp:BoundField DataField="Amount" HeaderText="Amount" SortExpression="Amount" />
                    </Columns>
                    <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                    <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                </asp:GridView>
            </td>
            <td align="left" style="height: 26px">&nbsp;</td>
        </tr>
        <tr>
            <td width="10">&nbsp;</td>
            <td align="left" colspan="7" style="margin-left: 40px">
                <asp:Button ID="btnPrint" runat="server" Text="Print" Width="82px" />
            &nbsp;&nbsp;
                <asp:Button ID="btnPrintExcel" runat="server" Text="Export to Excel" Width="125px" />
            </td>
        </tr>
        <tr>
            <td width="10">&nbsp;</td>
            <td align="left" colspan="7" style="margin-left: 40px">
                <asp:ListBox ID="LstWrongEntry" runat="server" Height="69px" style="color: #FF0000; font-weight: 700" Width="915px" Visible="False"></asp:ListBox>
            </td>
        </tr>
        <tr>
            <td width="10">&nbsp;</td>
            <td align="left" colspan="7" style="margin-left: 40px">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="10">&nbsp;</td>
            <td align="left" colspan="7" style="margin-left: 40px">
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT [SNo], [Dated], [Details], [Amount] FROM [tmpFeeReject] where Type=2 order by SNo">
                </asp:SqlDataSource>
                <asp:TextBox ID="txtFileName" runat="server" Visible="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td width="10">&nbsp;</td>
            <td align="left" colspan="7" style="margin-left: 40px">
                &nbsp;</td>
        </tr>
    </table>

</asp:Content>

