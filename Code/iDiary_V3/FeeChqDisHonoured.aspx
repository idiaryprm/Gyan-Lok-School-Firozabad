<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="FeeChqDisHonoured.aspx.vb" Inherits="iDiary_V3.FeeChqDisHonoured" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Heading" runat="server">
    Cheque
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    <div class="col_3" style="margin-top: 20px;" id="panelEnquiryNo" runat="server">
        <div class="col-md-3 widget widget1" style="width: 100%">
            <div class="r3_counter_box">
                 <table class="table">
        <tr>
            <td  class="td_width_16">Enter Cheque No</td>
            <td class="td_width_16" style="width: 226px">
                <asp:TextBox ID="txtSearchCheque" runat="server" CssClass="textbox"></asp:TextBox>
            &nbsp; <asp:Button ID="btnNext" runat="server" Text="&gt;&gt;" CssClass="btn btn-sm btn-primary" />
            </td>
            <td class="td_width_16"> Cheque Amount</td>
            <td class="td_width_16">
                <asp:TextBox ID="txtChequeAmount" runat="server" CssClass="textbox" ReadOnly="True"></asp:TextBox>
            </td>
            <td class="td_width_16">Cheque Date
            </td>
            <td class="td_width_16">
                <asp:TextBox ID="txtDepositDate" runat="server" CssClass="textbox" ReadOnly="True"></asp:TextBox>
            </td>
            <td class="td_width_4"></td>
        </tr>
      
        <tr>
            <td  class="td_width_16">Student Name</td>
            <td class="td_width_16" style="width: 226px">
                <asp:TextBox ID="txtSName" runat="server" CssClass="textbox" ReadOnly="True"></asp:TextBox>
            </td>
            <td class="td_width_16"> Fee Book No</td>
            <td class="td_width_16">
                <asp:TextBox ID="txtFeeBookNo" runat="server" CssClass="textbox" ReadOnly="True"></asp:TextBox>
            </td>
            <td class="td_width_16">Father Name</td>
            <td class="td_width_16">
                <asp:TextBox ID="txtFName" runat="server" CssClass="textbox" ReadOnly="True"></asp:TextBox>
            </td>
            <td class="td_width_4">&nbsp;</td>
        </tr>
      
        <tr>
            <td  class="td_width_16">Class</td>
            <td class="td_width_16" style="width: 226px">
                <asp:TextBox ID="txtClass" runat="server" CssClass="textbox" ReadOnly="True"></asp:TextBox>
            </td>
            <td class="td_width_16"> Section</td>
            <td class="td_width_16">
                <asp:TextBox ID="txtSection" runat="server" CssClass="textbox" ReadOnly="True"></asp:TextBox>
            </td>
            <td class="td_width_16">Cheque Bank</td>
            <td class="td_width_16">
                <asp:TextBox ID="txtChequeBank" runat="server" CssClass="textbox" ReadOnly="True"></asp:TextBox>
            </td>
            <td class="td_width_4">&nbsp;</td>
        </tr>
      
        <tr>
            <td  class="td_width_16" style="height: 28px">
    
                    Is Dishonoured</td>
            <td class="td_width_16" style="height: 28px; width: 226px;">
                <asp:DropDownList ID="cboChequeDishonour"  runat="server" CssClass="Dropdown">
                    <asp:ListItem>Yes</asp:ListItem>
                    <asp:ListItem>No</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="td_width_16" style="height: 28px"> Dishonoured Date</td>
            <td class="td_width_16" style="height: 28px">
                <asp:TextBox ID="txtDishonouredDate" runat="server" CssClass="textbox"></asp:TextBox>
                <asp:CalendarExtender ID="txtDepositDate0_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDishonouredDate">
                </asp:CalendarExtender>
            </td>
            <td class="td_width_16" style="height: 28px">&nbsp;</td>
            <td class="td_width_16" style="height: 28px">
                <asp:Button ID="btnSave" runat="server" Width="100px" CssClass="btn btn-primary" Text="Save" />
            </td>
            <td class="td_width_4" style="height: 28px"></td>
        </tr>
      
        <tr>
            <td  class="td_width_16" colspan="2">
                <asp:Label ID="lblStatus" runat="server" Font-Bold="True" ForeColor="Navy" style="color: #FF0000" Text=""></asp:Label>
                </td>
            <td class="td_width_16"> &nbsp;</td>
            <td class="td_width_16">
                <asp:TextBox ID="txtID" runat="server" CssClass="textbox" ReadOnly="True" Visible="False"></asp:TextBox>
            </td>
            <td class="td_width_16">&nbsp;</td>
            <td class="td_width_16">&nbsp;</td>
            <td class="td_width_4">&nbsp;</td>
        </tr>
      
        <tr>
            <td  class="td_width_16">&nbsp;</td>
            <td class="td_width_16" style="width: 226px">
                &nbsp;</td>
            <td class="td_width_16" colspan="2"> 
                 <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                </td>
            <td class="td_width_16">&nbsp;</td>
            <td class="td_width_16">&nbsp;</td>
            <td class="td_width_4">&nbsp;</td>
        </tr>
      
        <tr>
            <td  class="td_width_16">&nbsp;</td>
            <td class="td_width_16" style="width: 226px">
                &nbsp;</td>
            <td class="td_width_16"> &nbsp;</td>
            <td class="td_width_16">&nbsp;</td>
            <td class="td_width_16">&nbsp;</td>
            <td class="td_width_16">&nbsp;</td>
            <td class="td_width_4">&nbsp;</td>
        </tr>
      
        <tr>
            <td  class="td_width_16">&nbsp;</td>
            <td class="td_width_16" style="width: 226px">
                &nbsp;</td>
            <td class="td_width_16"> &nbsp;</td>
            <td class="td_width_16">&nbsp;</td>
            <td class="td_width_16">&nbsp;</td>
            <td class="td_width_16">&nbsp;</td>
            <td class="td_width_4">&nbsp;</td>
        </tr>
      
    </table>
            </div>
        </div>
        <div class="clearfix"></div>
    </div>   
</asp:Content>