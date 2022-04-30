<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="StockSummary.aspx.vb" Inherits="iDiary_V3.StockSummary" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    <div class="col_3" style="margin-top: 20px;" id="panelEnquiryNo" runat="server">
        <div class="col-md-3 widget widget1" style="width: 100%">
            <div class="r3_counter_box">
     <table width="100%" cellpadding="2" cellspacing="2" border="0">
         
     <tr>
         <td style="width:98%">

             <asp:GridView ID="GridView1" runat="server" Visible="False">
                 <Columns>
                     <asp:BoundField />
                     <asp:TemplateField>
                         <ItemTemplate>
                             <asp:DataList ID="DataList1" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal">
                                 <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                 <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                                 <SelectedItemStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                             </asp:DataList>
                         </ItemTemplate>
                     </asp:TemplateField>
                 </Columns>
             </asp:GridView>
             <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CellPadding="4" DataSourceID="SqlDataSource1" CssClass="Grid" Width="840px">
                 <AlternatingRowStyle BackColor="White" />
                 <Columns>
                     <asp:BoundField DataField="SrNo" HeaderText="S. No." SortExpression="SrNo" />
                     <asp:BoundField DataField="itemName" HeaderText="Item Name" SortExpression="itemName" />            
                     <asp:BoundField DataField="qty" HeaderText="Quantity" SortExpression="qty" />
                     <asp:BoundField DataField="UnitName" HeaderText="Unit" SortExpression="UnitName" />
                 </Columns>
             </asp:GridView>
              <br />
              <asp:Button ID="btnRefresh" runat="server" Text="Refresh" CssClass="btn btn-primary" />
             <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT * FROM [tmpStock]"></asp:SqlDataSource>
             <asp:Table ID="myTable" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CellPadding="0" CellSpacing="0" GridLines="Both" Visible="False" Width="100%">
             </asp:Table>

         </td>
     </tr>
     </table> 
                </div>
        </div>
        <div class="clearfix"></div>
    </div>
</asp:Content>
