<%@ Page Title="Academic Session" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="AcademicSession.aspx.vb" Inherits="iDiary_V3.AcademicSession" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Heading" runat="server">

    Choose Academic Session
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
        <div class="col_3" style="margin-top: 20px;" id="panelEnquiryNo" runat="server">
        <div class="col-md-3 widget widget1" style="width: 100%">
            <div class="r3_counter_box">
    <table class="table">
    
        <tr>
            <td align="left" valign="middle">
                <br />
                <asp:DropDownList ID="cboSession" runat="server" CssClass="Dropdown"></asp:DropDownList>
                <br />
                <br />
                <asp:Button ID="btnNext" runat="server" Text="Next"  class="btn btn-primary" />
            </td>
        </tr> 
    </table>
               

 </div>
        </div>
        <div class="clearfix"></div>
    </div>
     
</asp:Content>
