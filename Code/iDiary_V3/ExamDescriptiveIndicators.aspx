<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ExamAdminMasterPage.master" CodeBehind="ExamDescriptiveIndicators.aspx.vb" Inherits="iDiary_V3.ExamDescriptiveIndicators" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SubHeading" Runat="Server">
    Co-Scholastic Subjects
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMain" Runat="Server">
         <table width="100%" border="0" cellpadding="2" cellspacing="2">
        <tr>
            <td width="40%" valign="top">
                <asp:ListBox ID="lstMasters" runat="server" Height="263px" Width="300px" 
                    AutoPostBack="True"></asp:ListBox>
            </td>
            <td width="60%" valign="top" align="left">
                <strong>Subject Type<br />
                <b>
                <asp:DropDownList ID="cboSubjectType" runat="server" class="form-control1" AutoPostBack="True">
                </asp:DropDownList>
                <br />
                </b></strong><b>
                <br />
                Area/Activity
                <br />
                <asp:DropDownList ID="cboArea" runat="server" class="form-control1">
                </asp:DropDownList>
                <br />
                <br />
                Grade<br />
                <asp:DropDownList ID="cboGrade" runat="server" class="form-control1" AutoPostBack="True">
                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem>A</asp:ListItem>
                    <asp:ListItem>B</asp:ListItem>
                    <asp:ListItem>C</asp:ListItem>
                    <asp:ListItem>D</asp:ListItem>
                    <asp:ListItem>E</asp:ListItem>
                </asp:DropDownList>
                <br />
                <br />
                Descriptive Indicators</b><br />
                <asp:TextBox ID="txtName" runat="server" Width="200px" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" Height="45px" TextMode="MultiLine"></asp:TextBox>
                <br />
                <br />
                <br />
                <asp:Button ID="btnSave" runat="server" Text="Save" Width="65px" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" />
&nbsp;
                <asp:Button ID="btnNew" runat="server" Text="New" Width="65px" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" />
&nbsp;
                <asp:Button ID="btnRemove" runat="server" Text="Remove" Width="65px" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" />
                &nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="txtID" runat="server" ReadOnly="True" 
                    Width="74px"></asp:TextBox>
                <br /><br />
&nbsp;&nbsp;&nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp;&nbsp;
                </td>
        </tr>
    </table>

</asp:Content>
