<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="LibraryMasterPage.Master" CodeBehind="NewsPaperMaster.aspx.vb" Inherits="iDiary_V3.NewsPaperMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SubHeading" runat="server">
    News paper Master
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="LibraryMasterContents" runat="server">
   <%-- <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />--%>
    
    <table class="table">
        <tr>
            <td width="18%" valign="top" align="left">
                <asp:ListBox ID="lstNewsPapers" runat="server" AutoPostBack="True" Height="275px" 
                    Width="264px"></asp:ListBox>
            </td>
            <td width="2%" valign="top" align="left">&nbsp;</td>
                
            <td width="41%" align="left" valign="top" style="font-weight: bold">
            
                News Paper Name
                <br />
                <br />
                <asp:TextBox ID="txtNewsPaperName" runat="server" CssClass="textbox"></asp:TextBox>
                
                <br />
                <br />
                Frequency<br />
                <br />
                <asp:DropDownList ID="cboFrequency" runat="server" CssClass="Dropdown">
                </asp:DropDownList>
                <br />
                <br />
                <asp:LinkButton ID="btnNew" runat="server" class="btn btn-primary">New</asp:LinkButton>
                &nbsp;&nbsp;
                <asp:Button ID="btnSave" runat="server" Text="Save" class="btn btn-primary" />
&nbsp;&nbsp;
                <asp:Button ID="btnRemove" runat="server" Text="Remove" class="btn btn-primary" Visible="False"/>
&nbsp;&nbsp;
                            
                <br />
                <br />
                <asp:Label ID="lblStatus" runat="server" Width="210px"></asp:Label>
            
                <br />
                <asp:TextBox ID="txtNewsPaperID" runat="server" Width="60px" Visible="False"></asp:TextBox>
            
                <br />
            
            </td>
            <td width="39%" align="right" valign="top" style="font-weight: bold">
            &nbsp;</td>
            
        </tr>
    </table>

</asp:Content>
