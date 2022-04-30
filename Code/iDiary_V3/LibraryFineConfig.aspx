<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/LibraryMasterPage.Master" CodeBehind="LibraryFineConfig.aspx.vb" Inherits="iDiary_V3.LibraryFineConfig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SubHeading" runat="server">
    News paper Master
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="LibraryMasterContents" runat="server">
   <%-- <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />--%>
    
    <table class="table">
        <tr>
            <td width="18%" valign="top" align="left">
                <strong>Category</strong><br />
                <br />
                <asp:DropDownList ID="cboCategory" runat="server" AutoPostBack="True" Width="200px" CssClass="Dropdown">
                    <asp:ListItem>Student</asp:ListItem>
                    <asp:ListItem>Teacher</asp:ListItem>
                </asp:DropDownList>
                <br />
                <br />
                <strong>Class<br />
                <br />
                <asp:DropDownList ID="cboClass" runat="server" Width="200px" AutoPostBack="True" CssClass="Dropdown">
                    <asp:ListItem>Student</asp:ListItem>
                    <asp:ListItem>Teacher</asp:ListItem>
                </asp:DropDownList>
                <br />
                </strong>
            </td>
            <td width="2%" valign="top" align="left">&nbsp;</td>
                
            <td width="41%" align="left" valign="top" style="font-weight: bold">
            
                Max Books Limit<br />
                <br />
                <asp:TextBox ID="txtBookLimit" runat="server" CssClass="textbox"></asp:TextBox>
                &nbsp;&nbsp;
                <br />
                <br />
                No. Of Days Limit<br />
                <br />
                <asp:TextBox ID="txtDayLimit" runat="server"  CssClass="textbox"></asp:TextBox>
                <br />
                <br />
                Fine Per Book Per Day<br />
                <br />
                <asp:TextBox ID="txtAmountFine" runat="server" CssClass="textbox"></asp:TextBox>
                <br />
                <br />
                <asp:Button ID="btnSave" runat="server" Text="Save" class="btn btn-primary"/>
&nbsp;&nbsp;
                            
                <br />
                <br />
                <asp:Label ID="lblStatus" runat="server"></asp:Label>
            
                <br />
                <asp:TextBox ID="txtCategoryID" runat="server" Width="60px" Visible="False"></asp:TextBox>
            
                <br />
            
            </td>
            <td width="39%" align="right" valign="top" style="font-weight: bold">
            &nbsp;</td>
            
        </tr>
    </table>

</asp:Content>
