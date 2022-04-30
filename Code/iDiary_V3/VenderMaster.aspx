<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="LibraryMasterPage.Master" CodeBehind="VenderMaster.aspx.vb" Inherits="iDiary_V3.VenderMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SubHeading" runat="server">
    Vendor Master  
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
                <asp:ListBox ID="lstVenders" runat="server" AutoPostBack="True" Height="275px" 
                    Width="264px"></asp:ListBox>
            </td>
            <td width="2%" valign="top" align="left">&nbsp;</td>
                
            <td width="41%" align="left" valign="top" style="font-weight: bold">
            
                Vendor Name
                <br />
                <br />
                <asp:TextBox ID="txtVenderName" runat="server" CssClass="textbox" ></asp:TextBox>
                &nbsp;&nbsp;
                <br />
                <br />
                <asp:Button ID="btnSave" runat="server" Text="Save" Class="btn btn-primary" />
&nbsp;&nbsp;
                <asp:Button ID="btnRemove" runat="server" Text="Remove" Class="btn btn-primary" Visible="false"/>
&nbsp;&nbsp;
                            
                <asp:Button ID="btnNew" runat="server" Text="New" Class="btn btn-primary"/>
                            
                <br />
                <br />
                <asp:Label ID="lblStatus" runat="server"></asp:Label>
            
                <br />
                <asp:TextBox ID="txtVenderID" runat="server" Width="60px" Visible="False"></asp:TextBox>
            
                <br />
            
            </td>
            <td width="39%" align="right" valign="top" style="font-weight: bold">
            &nbsp;</td>
            
        </tr>
    </table>

</asp:Content>
