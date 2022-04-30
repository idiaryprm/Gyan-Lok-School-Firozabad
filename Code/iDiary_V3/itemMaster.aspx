<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Stock.master" CodeBehind="itemMaster.aspx.vb" Inherits="iDiary_V3.itemMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="StockMasterContents" runat="server">
    <%-- <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />--%>
     <table class="table">
        <tr>
            <td width="40%" valign="top">
               <asp:ListBox ID="lstMaster" runat="server" AutoPostBack="True" Height="275px" 
                    Width="346px"></asp:ListBox>
            </td>
            <td width="60%" valign="top" align="left">
                 <table style="width:90%;padding :1px;">
                    <tr>
                        <td style="width:40%">

                Item Name
                
                        </td>
                        <td style="width:60%">

                <asp:TextBox ID="txtName" runat="server" CssClass="textbox" ></asp:TextBox>

                        </td>
                    </tr>
                    <tr>
                        <td style="width:40%">

                Item Specs
                
                        </td>
                        <td style="width:60%">

                <asp:TextBox ID="txtSpecs" runat="server" CssClass="textbox" ></asp:TextBox>

                        </td>
                    </tr>
                    <tr>
                        <td style="width:40%">

                            Item Unit</td>
                        <td style="width:60%">

                            <asp:DropDownList ID="cboUnit" runat="server" CssClass="Dropdown">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:40%">

                            Item Type</td>
                        <td style="width:60%">

                            <asp:DropDownList ID="cboitemType" runat="server" CssClass="Dropdown">
                                <asp:ListItem>Consumable</asp:ListItem>
                                <asp:ListItem>Non-Consumable</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:40%">

                            </td>
                        <td style="width:60%">

                <asp:TextBox ID="txtopeningStock" runat="server" CssClass="textbox"  Visible="False">0</asp:TextBox>

                        </td>
                    </tr>
                     <tr>
                         <td colspan="2"> <asp:Label ID="lblStatus" runat="server" ForeColor="Red" ></asp:Label></td>
                     </tr>
                    <tr>
                        <td style="width:40%">

                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" /> &nbsp; &nbsp;<asp:Button ID="btnRemove" runat="server" Text="Remove" CssClass="btn btn-primary" /></td>
                        <td style="width:60%">

                           <asp:Button ID="btnNew" runat="server" Text="New" CssClass="btn btn-primary" /></td>
                    </tr>
                </table>
               
            
                <br />
                <asp:TextBox ID="txtID" runat="server" CssClass="textbox"  Visible="False"></asp:TextBox>
            
                <br />
            
            </td>
           
            
        </tr>
    </table>
</asp:Content>
