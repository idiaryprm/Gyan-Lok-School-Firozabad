<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/FeeMasterPage.master" EnableEventValidation="true"  CodeBehind="FeeConfig.aspx.vb" Inherits="iDiary_V3.FeeConfig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FeeMasterContents" runat="server">
<asp:HiddenField ID="hfGridHtml" runat="server" />

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<script type="text/javascript">
    $(function () {
        $("[id*=btnExport]").click(function () {
            $("[id*=hfGridHtml]").val($("#divExport").html());
        });
    });
</script>

   <%-- <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />--%>
  
    <table class="table">
        <tr>
            <td valign="top" align="left" style="height: 27px; width: 26%;"><strong>Class Fee Group</strong></td>
            <td valign="top" align="left" width="15%" style="height: 27px">
                <asp:DropDownList ID="cboFeeGroup" runat="server" CssClass="Dropdown"
                    AutoPostBack="True">
                </asp:DropDownList>
            </td>
            <td valign="top" align="left" style="height: 27px; width: 26%;">&nbsp;</td>
            <td valign="top" align="left" style="height: 27px">&nbsp;</td>
        </tr>
        <tr>
            <td align="left" colspan="4" style="text-align: center" valign="top">
              <div runat ="server" id="divExport">
                <asp:Table ID="myTable"  runat="server" Width="100%">
                </asp:Table>
                 </div>
            </td>
        </tr>
        <tr>
            <td valign="top" align="left" style="width: 26%">
                <asp:Button ID="btnSave" runat="server" Text="Save" Width="100px" CssClass="btn btn-primary" />
                &nbsp;
                <asp:Button ID="btnImportFee" runat="server" Text="Import Previous Fees" Width="168px" CssClass="btn btn-primary" />
            </td>
             <td valign="top" align="left" width="15%">
                  <asp:Button ID="btnExport" runat="server" OnClick ="ExportToExcel"   Text="Export To Excel" Width="100px" CssClass="btn btn-primary" />
            </td>
            <td valign="top" align="left" width="15%" colspan="2">
                <asp:Label ID="lblStatus" runat="server" Font-Bold="True" ForeColor="Navy" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td valign="top" align="left" style="width: 26%">&nbsp;</td>
            <td valign="top" align="left" width="15%" colspan="3">
                <asp:TextBox ID="txtAdmissionFeeID" runat="server" BorderStyle="Solid" BorderWidth="1px" ReadOnly="True" Visible="False" Width="22px"></asp:TextBox>
                <asp:TextBox ID="txtLateFeeID" runat="server" BorderStyle="Solid" BorderWidth="1px" ReadOnly="True" Visible="False" Width="22px"></asp:TextBox>
                <asp:TextBox ID="txtTutionFeeID" runat="server" BorderStyle="Solid" BorderWidth="1px" Height="16px" ReadOnly="True" Visible="False" Width="22px"></asp:TextBox>
                <asp:TextBox ID="txtConveyanceFeeID" runat="server" BorderStyle="Solid" BorderWidth="1px" ReadOnly="True" Visible="False" Width="22px"></asp:TextBox>
            </td>
        </tr>
    </table>
   
</asp:Content>
