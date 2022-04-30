<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.master" CodeBehind="StockRegister.aspx.vb" Inherits="iDiary_V3.StockRegister"  EnableEventValidation="false" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />--%>
     <div class="col_3" style="margin-top: 20px;" id="panelEnquiryNo" runat="server">
        <div class="col-md-3 widget widget1" style="width: 100%">
            <div class="r3_counter_box">
     <table class="table" >
         
     <tr>
         <td class="auto-style1">

             Date From
             
             &nbsp;&nbsp;
                
         </td>
         <td class="auto-style2">

                <asp:TextBox ID="txtDateFrom" runat="server" CssClass="textbox" ></asp:TextBox>

                            <asp:CalendarExtender ID="txtDateFrom_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDateFrom" TodaysDateFormat="dd/MM/yyyy">
                            </asp:CalendarExtender>

         </td>
         <td class="auto-style3">

             To</td>
         <td>

                <asp:TextBox ID="txtDateTo" runat="server" CssClass="textbox" ></asp:TextBox>

                            <asp:CalendarExtender ID="txtDateTo_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDateTo" TodaysDateFormat="dd/MM/yyyy">
                            </asp:CalendarExtender>

         </td>
     </tr>
         
     <tr>
         <td class="auto-style1">

             Item</td>
         <td class="auto-style2">

                            <asp:DropDownList ID="cboItem" runat="server" CssClass="Dropdown"  EnableTheming="True">
                            </asp:DropDownList>

         </td>
         <td class="auto-style3">

                            <asp:Label ID="lblLedger" runat="server" Text="Label"></asp:Label>

         </td>
         <td>

                            <asp:DropDownList ID="cboLedger" runat="server" CssClass="Dropdown" EnableTheming="True">
                            </asp:DropDownList>

         </td>
     </tr>
         
     <tr>
         <td class="auto-style1">

              <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" />

         </td>
         <td class="auto-style2">

                &nbsp;</td>
         <td class="auto-style3">

             &nbsp;</td>
         <td>

             <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT * FROM vw_stock where stockid=0"></asp:SqlDataSource>

         </td>
     </tr>
         
     <tr>
         <td colspan="4">

             <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataKeyNames="StockID" DataSourceID="SqlDataSource1" CssClass="Grid"  Width="800px">
                 <Columns>
                     <asp:TemplateField HeaderText="S. No.">
         <ItemTemplate>
               <%# Container.DataItemIndex + 1 %>
         </ItemTemplate>
          <ItemStyle Width="50px" />
     </asp:TemplateField>
                       <asp:BoundField DataField="StockDate" HeaderText="Date" SortExpression="StockDate" DataFormatString="{0:dd/MM/yyyy}" />
                     <asp:BoundField DataField="itemName" HeaderText="Item Name" SortExpression="itemName" />
                                          <asp:BoundField DataField="quantity" HeaderText="Quantity" SortExpression="quantity" />
                     <asp:BoundField DataField="UnitName" HeaderText="Unit" SortExpression="UnitName" />
                     <asp:BoundField DataField="VendorName" HeaderText="Vendor" SortExpression="VendorName" />
                     <asp:BoundField DataField="LedgerName" HeaderText="Ledger" SortExpression="LedgerName" />
                     <asp:ButtonField ButtonType="Button" Text="Edit" />
                 </Columns>
                
             </asp:GridView>

                   <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

                <asp:Button ID="btnExcel" runat="server" Text="Export to Excel" CssClass="btn btn-primary" Visible="False" />

         </td>
     </tr>
     </table> 
      </div>
        </div>
        <div class="clearfix"></div>
    </div>
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="head">
    <style type="text/css">
        .auto-style1 {
            width: 148px;
        }
        .auto-style2 {
            width: 283px;
        }
        .auto-style3 {
            width: 116px;
        }
    </style>
</asp:Content>

