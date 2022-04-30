<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="LibraryMasterPage.Master" CodeBehind="PublisherMaster.aspx.vb" Inherits="iDiary_V3.PublisherMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SubHeading" runat="server">
    Publisher Master
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="LibraryMasterContents" runat="server">
     <%-- <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />--%>
        <table class="table">
        <tr>
            <td width="18%" valign="top" align="left">
                <asp:ListBox ID="lstPubs" runat="server" AutoPostBack="True" Height="275px" 
                    Width="264px"></asp:ListBox>
            </td>
            <td width="2%" valign="top" align="left">&nbsp;</td>
                
            <td width="41%" align="left" valign="top" style="font-weight: bold">
            
                Publisher Name
                <br />
                <br />
                <asp:TextBox ID="txtPubName" runat="server" CssClass="textbox" Width="241px" ></asp:TextBox>
                &nbsp;&nbsp;
                
                <br />
                <br />
                <asp:Button ID="btnNew" runat="server" Text="New" class="btn btn-sm btn-primary"/>&nbsp;&nbsp;
                <asp:Button ID="btnSave" runat="server" Text="Save" class="btn btn-primary"/>
&nbsp;&nbsp;
                <asp:Button ID="btnRemove" runat="server" Text="Remove" class="btn btn-primary" Visible="false"/>
&nbsp;&nbsp;
                            
                <br />
                <br />
                <asp:Label ID="lblStatus" runat="server"></asp:Label>
            
                <br />
                <asp:TextBox ID="txtPubID" runat="server" Width="248px" Visible="False" Height="16px"></asp:TextBox>
            
                <br />
            
            </td>
            <td width="39%" align="right" valign="top" style="font-weight: bold">
            &nbsp;</td>
            
        </tr>
    </table>

</asp:Content>
