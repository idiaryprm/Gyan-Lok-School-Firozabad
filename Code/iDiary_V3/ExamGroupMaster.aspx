<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ExamAdminMasterPage.Master" CodeBehind="ExamGroupMaster.aspx.vb" Inherits="iDiary_V3.ExamGroupMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SubHeading" runat="server">
    Exam Groups 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
  <%--  <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br />--%>

     <table class="table">
            <tr>
                <td width="20">&nbsp;</td>
                <td width="400">
                    <asp:ListBox ID="lstMaster" runat="server" Height="300px" Width="300px"  CssClass="list"
                        AutoPostBack="True"></asp:ListBox>
                </td>
                <td valign="top" style="width: 601px">
                    <b>School Name<br />
                        <asp:DropDownList ID="cboSchoolName" runat="server" CssClass="Dropdown" Width="300px" AutoPostBack="true" ></asp:DropDownList>
                    <br />
                    <br />
                    Exam Class Group Name</b>
                    <br />
                    <asp:TextBox ID="txtName" runat="server" CssClass="textbox"></asp:TextBox>
                    <br />
                    <br />
                    <strong>Display Order</strong><br />
                    <asp:TextBox ID="txtDisplayOrder" runat="server" CssClass="textbox" TextMode="Number"></asp:TextBox>
                    <br />
                    <br />
                    <br />
                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnNew" runat="server"  Text="New" 
                        CssClass="btn btn-primary" />
            
                    &nbsp;&nbsp; <asp:Button ID="btnRemove" runat="server" Text="Remove" Visible="false" CssClass="btn btn-primary" />
                    <br />
                    <br />
                    <asp:TextBox ID="txtID" runat="server" Width="183px" Visible="False"></asp:TextBox>
                </td>
                <td width="20">&nbsp;</td>
            </tr>
        </table>
</asp:Content>
