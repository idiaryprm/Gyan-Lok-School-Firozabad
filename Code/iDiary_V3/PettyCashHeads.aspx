<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="PettyCashHeads.aspx.vb" Inherits="iDiary_V3.PettyCashHeads" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Heading" runat="server">
    Petty Cash Head Master 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
         <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />  
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />--%> 
    <div class="col_3" style="margin-top: 20px;" id="panelEnquiryNo" runat="server">
        <div class="col-md-3 widget widget1" style="width: 100%">
            <div class="r3_counter_box">

                <table class="table">
        <tr>
            <td width="40%" valign="top">
                <asp:ListBox ID="lstMasters" runat="server" Height="301px" Width="300px" 
                    AutoPostBack="True"></asp:ListBox>
            </td>
            <td width="60%" valign="top" align="left">
            
                <asp:DropDownList ID="cboTransType" runat="server" Width="165px" Visible="false" AutoPostBack="True">
                </asp:DropDownList>
                
                <br />
                <b>Head Name</b> <b>
                <br />
                </b>
                <asp:TextBox ID="txtName" runat="server" CssClass="textbox"></asp:TextBox>
                <br />
                <b>
                <br />
                Display Order<br />
                    <asp:TextBox ID="txtDisplayOrder" runat="server" CssClass="textbox" TextMode="Number" ></asp:TextBox>
                <asp:TextBox ID="txtOPBal" runat="server" CssClass="textbox" Visible="false"></asp:TextBox>
                <br />
                <br />
                Class Groups<br />
            
                <asp:DropDownList ID="cboClassGroup" runat="server" Width="165px" CssClass="Dropdown">
                </asp:DropDownList>
                
                <br />
                <asp:DropDownList ID="cboDRCR" runat="server" Width="165px" Visible="False">
                    <asp:ListItem>DR</asp:ListItem>
                    <asp:ListItem>CR</asp:ListItem>
                </asp:DropDownList>
                </b>
                <br />
                
                <span class="auto-style1">* Pease Keep Class Group Blank for Petty Cash Heads which are associated with Student Present Class<br />
               
                </span>
               
               
                <asp:CheckBox ID="chbInventory" runat="server" style="font-weight: 700" Text=" Is Inventory Type  " Visible="False" />
                
                <asp:Label ID="lblStatus" runat="server" Font-Bold="True" ForeColor="Navy" Text="" style="color: #FF3300"></asp:Label>
                <br />
                <br />
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" />
&nbsp;
                <asp:Button ID="btnNew" runat="server" Text="New" CssClass="btn btn-primary" />
&nbsp;
                <asp:Button ID="btnRemove" runat="server" Text="Remove" CssClass="btn btn-primary" Visible="false" />
                <br /><br />
                <asp:TextBox ID="txtID" runat="server" ReadOnly="True" Visible="False" 
                    Width="74px"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp;&nbsp;
                </td>
        </tr>
    </table>
                 </div>
        </div>
        <div class="clearfix"></div>
    </div>
</asp:Content>
<asp:Content ID="Content3" runat="server" contentplaceholderid="head">
    <style type="text/css">
        .auto-style1 {
            color: #0066FF;
        }
    </style>
</asp:Content>

