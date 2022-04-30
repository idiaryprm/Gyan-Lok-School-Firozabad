<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/BusMaster.master" CodeBehind="BusTermMaster.aspx.vb" Inherits="iDiary_V3.BusTermMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BusMasterContents" runat="server">
    <script>
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 46 || charCode > 57)) {
                return false;
            }
            return true;
        }
    </script>
    <table class="table">
    <tr>
        <td class="modal-sm" style="width: 315px"><asp:ListBox ID="lstTerm" runat="server" Height="233px" Width="283px" AutoPostBack="True"></asp:ListBox></td>
        <td style="width: 177px"><strong>Bus Term Name</strong><br />
            <asp:TextBox ID="txtTermName" runat="server" CssClass="textbox"></asp:TextBox>
            <br />
            <br />
            <strong>Bus Term No</strong><br />
            <asp:TextBox ID="txtTermNo" runat="server" CssClass="textbox"></asp:TextBox>
            <br />
            <br />
                <strong>Display Order  </strong><br />
                <asp:TextBox ID="txtDispOrder" runat="server" TextMode="Number" CssClass="textbox"></asp:TextBox>
          
                <br />
                 <br />
            <asp:Label ID="lblStatus" runat="server" style="font-weight: 700; color: #FF3300;" Text="" ></asp:Label>
            <br />
            <br />
          
            <asp:Button ID="btnSave" runat="server" Text="Save" class="btn btn-primary"/>
&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnNew" runat="server" Text="New" class="btn btn-primary"/>
           
            <asp:TextBox ID="txtID" runat="server" CssClass="textbox" Width="53px"></asp:TextBox>
           </td>
        <td>
            
            <asp:CheckBox ID="chkDefault" runat="server" Text="Is Default" Visible="false"/>
        </td>
        <td>&nbsp;</td>
    </tr>
</table>
</asp:Content>
