<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.master" CodeBehind="Inventory.aspx.vb" Inherits="iDiary_V3.Inventory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <%-- <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />--%>
     <div class="col_3" style="margin-top: 20px;" id="panelEnquiryNo" runat="server">
        <div class="col-md-3 widget widget1" style="width: 100%">
            <div class="r3_counter_box">
    <table class="table">
        <tr>
            <td colspan="2">
                 <asp:Label ID="lblStock" runat="server" style="font-weight: 700; font-size: medium"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="40%" valign="top">
                <asp:ListBox ID="lstMaster" runat="server" CssClass="textbox"  Height="242px" Width="300px" 
                    AutoPostBack="True"></asp:ListBox>
            </td>
            <td width="60%" valign="top" align="left">
                   <table style="width:99%; padding :1px;">
                    <tr>
                        <td style="width:29%">

                Item Name
                
                        </td>
                        <td style="width:60%">

                <asp:TextBox ID="txtName" runat="server" CssClass="textbox"  Enabled="False"></asp:TextBox>

                        </td>
                    </tr>
                    <tr>
                        <td style="width:29%">

                Item Specs
                
                        </td>
                        <td style="width:60%">

                <asp:TextBox ID="txtSpecs" runat="server" CssClass="textbox"  ReadOnly="True"></asp:TextBox>

                        </td>
                    </tr>
                    <tr>
                        <td style="width:29%">

                            Item Unit</td>
                        <td style="width:60%">

                            <asp:DropDownList ID="cboUnit" runat="server" CssClass="Dropdown"  Enabled="False">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:29%">

                            Item Type</td>
                        <td style="width:60%">

                            <asp:DropDownList ID="cboitemType" runat="server" CssClass="Dropdown" Enabled="False">
                                <asp:ListItem>Consumable</asp:ListItem>
                                <asp:ListItem>Non-Consumable</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:29%">

                            Item Quantity</td>
                        <td style="width:60%">

                <asp:TextBox ID="txtQty" runat="server" CssClass="textbox" TextMode="Number"  ></asp:TextBox>

                        </td>
                    </tr>
                    <tr>
                        <td style="width:29%">

                            Date</td>
                        <td style="width:60%">

                <asp:TextBox ID="txtStockDate" runat="server" CssClass="textbox" ></asp:TextBox>

                        </td>
                    </tr>
                    <tr>
                        <td style="width:29%">

                            <asp:Label ID="lblUnitCost" runat="server" Text="Unit Cost"></asp:Label>
                        </td>
                        <td style="width:60%">

                <asp:TextBox ID="txtUnitCost" runat="server" CssClass="textbox" ></asp:TextBox>

                        </td>
                    </tr>
                    <tr>
                        <td style="width:29%">

                            <asp:Label ID="lblLedger" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td style="width:60%">

                          

                            <asp:DropDownList ID="cboLedger" runat="server" CssClass="Dropdown" EnableTheming="True">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:29%">

                            Remarks</td>
                        <td style="width:60%">

                <asp:TextBox ID="txtRemarks" style="width: 299px; height: 90px;" runat="server" CssClass="textbox"  TextMode="MultiLine" Rows="20" Columns="5"></asp:TextBox>

                        </td>
                    </tr>
                </table>
                &nbsp;<br />
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" />
&nbsp;&nbsp;
                <asp:Button ID="btnRemove" runat="server" Text="Remove" CssClass="btn btn-primary" Visible="False" />
&nbsp;&nbsp;
                            
                <asp:Button ID="btnNew" runat="server" Text="New" CssClass="btn btn-primary" Visible="False" />
                            
                <br />
                <br />
                <asp:Label ID="lblStatus" runat="server"></asp:Label>
            
                <br />
                <asp:TextBox ID="txtID" runat="server" CssClass="textbox"  Visible="False"></asp:TextBox>
            
                <asp:TextBox ID="txtStockID" runat="server" CssClass="textbox"  Visible="False"></asp:TextBox>
            
                <br />
               
                </td>
        </tr>
    </table>
                  </div>
        </div>
        <div class="clearfix"></div>
    </div>
</asp:Content>
