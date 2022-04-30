<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="LibraryMasterPage.Master" CodeBehind="BookCategoryMaster.aspx.vb" Inherits="iDiary_V3.BookCategoryMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SubHeading" runat="server">
    Book Category Master
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="LibraryMasterContents" runat="server">
   <%-- <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />--%>
        <table class="table">
        <tr>
            <td width="18%" valign="top" align="left">
                <asp:ListBox ID="lstBookCat" runat="server" AutoPostBack="True" Height="275px" 
                    Width="264px"></asp:ListBox>
            </td>
            <td width="2%" valign="top" align="left">&nbsp;</td>
                
            <td width="41%" align="left" valign="top" style="font-weight: bold">
            
                Book Category Name
                <br />
                <br />
                <asp:TextBox ID="txtBookCatName" runat="server" CssClass="textbox" Width="205px" ></asp:TextBox>
                
                
                <br />
                <br />
                <asp:LinkButton ID="btnNew" runat="server" Class="btn btn-primary">New</asp:LinkButton>
                &nbsp;&nbsp;
                <asp:Button ID="btnSave" runat="server" Text="Save" Class="btn btn-primary"/>
&nbsp;&nbsp;
                <asp:Button ID="btnRemove" runat="server" Text="Remove" Class="btn btn-primary" Visible="false"/>
&nbsp;&nbsp;
                            
                <br />
                <br />
                <asp:Label ID="lblStatus" runat="server" Width="210px"></asp:Label>
            
                <br />
                <asp:TextBox ID="txtID" runat="server" Width="60px" Visible="False"></asp:TextBox>
            
                <br />
            
            </td>
            <td width="39%" align="right" valign="top" style="font-weight: bold">
            &nbsp;</td>
            
        </tr>
    </table>

</asp:Content>
