<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="AdminEntry.aspx.vb" Inherits="iDiary_V3.AdminEntry" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Heading" runat="server">Admin Entry
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    Allow Online Entry
    
       <br /><br />
    <asp:DropDownList ID="cboAllow" CssClass="Dropdown" runat="server">
        <asp:ListItem>NO</asp:ListItem>
        <asp:ListItem>YES</asp:ListItem>
    </asp:DropDownList>
<br /><br />
  
   

    <asp:Button ID="btnAllow" runat="server" CssClass="hvr-glow" Text="Save" />


</asp:Content>
