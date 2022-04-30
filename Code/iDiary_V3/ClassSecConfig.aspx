<%@ Page Language="VB" MasterPageFile="~/StudentMaster.master" AutoEventWireup="false" Inherits="iDiary_V3.ClassSecConfig" title="Untitled Page" Codebehind="ClassSecConfig.aspx.vb" %>

<asp:Content ID="Content2" ContentPlaceHolderID="StudentMasterContents" runat="server">
<%--    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />--%>
    <table class="table">
        <tr>
            <td valign="top" style="width: 28%">
                <asp:ListBox ID="lstBranch" runat="server" Height="319px" Width="300px" 
                    AutoPostBack="True"></asp:ListBox>
            </td>
            <td width="60%" valign="top" align="left">
                <b>School Name<br />
                <asp:DropDownList ID="cboSchoolName" runat="server" AutoPostBack="true" CssClass="Dropdown" Width="300px">
                </asp:DropDownList>
                <br />
                Class Name<br />
            <asp:DropDownList ID="cboClass" runat="server" CssClass="Dropdown" >
            </asp:DropDownList>
                <br />
                Section<br />
            <asp:DropDownList ID="cboSection" runat="server" CssClass="Dropdown" >
            </asp:DropDownList>
                <br />
                Sub-Section<br />
            <asp:DropDownList ID="cboSubSection" runat="server" CssClass="Dropdown" >
            </asp:DropDownList>
                <br />
                Fee Group<br />
            <asp:DropDownList ID="cboFeeGroup" runat="server" CssClass="Dropdown">
            </asp:DropDownList>
                <br />
                </b>
                <asp:Label ID="lblStatus" runat="server" Font-Bold="True" ForeColor="Navy" Text=""></asp:Label>
                <br />
                <asp:TextBox ID="txtID" runat="server" Width="47px" Visible="False"></asp:TextBox>
                <br />
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" />
&nbsp;<asp:Button ID="btnRemove" runat="server" Text="Remove" CssClass="btn btn-primary" Visible="false" />
            
&nbsp;<asp:Button ID="btnNew" runat="server" Text="New" CssClass="btn btn-primary" />
                
                <br />
            </td>
        </tr>
    </table>

</asp:Content>

