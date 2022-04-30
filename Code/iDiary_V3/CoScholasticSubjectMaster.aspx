<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ExamAdminMasterPage.master" CodeBehind="CoScholasticSubjectMaster.aspx.vb" Inherits="iDiary_V3.CoScholasticSubjectMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SubHeading" Runat="Server">
    Co-Scholastic Subjects
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMain" Runat="Server">
         <table width="100%" border="0" cellpadding="2" cellspacing="2">
        <tr>
            <td width="40%" valign="top">
                <asp:ListBox ID="lstMasters" runat="server" Height="250px" Width="300px" 
                    AutoPostBack="True"></asp:ListBox>
            </td>
            <td width="60%" valign="top" align="left">
                <b><asp:Label ID="lblName" runat="server" Text="Label"></asp:Label></b>
                
                <br />
                <asp:TextBox ID="txtName" runat="server" Width="155px" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                <br />
                <br />
                <strong>Subject Code<br />
                <asp:TextBox ID="txtCode" runat="server" Width="155px" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                <br />
                </strong>
                <br />
&nbsp;<asp:CheckBox ID="chkAll" runat="server" AutoPostBack="True" Text="Select All/ None" style="font-weight: 700" />
                <br />
                <asp:CheckBoxList ID="chkGroups" runat="server" Width="228px">
                </asp:CheckBoxList>
                <br />
                <asp:Button ID="btnSave" runat="server" Text="Save" Width="65px" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" />
&nbsp;
                <asp:Button ID="btnNew" runat="server" Text="New" Width="65px" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" />
&nbsp;
                <asp:Button ID="btnRemove" runat="server" Text="Remove" Width="65px" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" />
                <br />
                <asp:TextBox ID="txtID" runat="server" ReadOnly="True" Visible="False" 
                    Width="74px"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp;<br />
                <asp:ListBox ID="ListPaperID" runat="server" Visible="False" Height="55px"></asp:ListBox>
                &nbsp;
                &nbsp;&nbsp;&nbsp;&nbsp;
                </td>
        </tr>
    </table>

</asp:Content>
