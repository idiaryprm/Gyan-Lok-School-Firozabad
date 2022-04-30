<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="LibraryMasterPage.Master" CodeBehind="BookStatusMaster.aspx.vb" Inherits="iDiary_V3.BookStatusMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SubHeading" runat="server">
    Book Status Master
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="LibraryMasterContents" runat="server">
        <table border="0" width="100%">
        <tr>
            <td width="18%" valign="top" align="left">
                <asp:ListBox ID="lstBookStatus" runat="server" AutoPostBack="True" Height="275px" 
                    Width="264px"></asp:ListBox>
            </td>
            <td width="2%" valign="top" align="left">&nbsp;</td>
                
            <td width="41%" align="left" valign="top" style="font-weight: bold">
            
                Book Status Name
                <br />
                <br />
                <asp:TextBox ID="txtBookStatusName" runat="server" Width="175px" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                &nbsp;&nbsp;
                <asp:LinkButton ID="btnNew" runat="server" ForeColor="#000066">New</asp:LinkButton>
                <br />
                <br />
                <asp:Button ID="btnSave" runat="server" Text="Save" Width="70px" 
                    style="height: 26px" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px" />
&nbsp;&nbsp;
                <asp:Button ID="btnRemove" runat="server" Text="Remove" Width="70px" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px" />
&nbsp;&nbsp;
                <asp:Button ID="btnBack" runat="server" Text="Back" Width="70px" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px" />
            
                <br />
                <br />
                <asp:Label ID="lblStatus" runat="server"></asp:Label>
            
                <br />
                <asp:TextBox ID="txtBookStatusID" runat="server" Width="60px" Visible="False"></asp:TextBox>
            
                <br />
            
            </td>
            <td width="39%" align="right" valign="top" style="font-weight: bold">
            &nbsp;</td>
            
        </tr>
    </table>

</asp:Content>
