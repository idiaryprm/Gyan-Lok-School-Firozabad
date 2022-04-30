<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="ManageDocuments.aspx.vb" Inherits="iDiary_V3.ManageDocuments" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Heading" runat="server">
    Document & Content Management
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <table border="0" width="100%" cellpadding="1" cellspacing="1">
        <tr>
            <td width="15%">Document Number</td>
            <td width="20%">
                <asp:TextBox ID="txtFileNo" runat="server" Width="117px" BorderColor="Navy" 
                    BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
            &nbsp;&nbsp;
                <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/images/search.png" />
            </td>
            <td width="15%">Document Date</td>
            <td width="20%">
                <asp:TextBox ID="txtDD" runat="server" Width="32px" BorderColor="Navy" 
                    BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                &nbsp;/ <asp:TextBox ID="txtMM" runat="server" Width="35px" BorderColor="Navy" 
                    BorderStyle="Solid" BorderWidth="1px"></asp:TextBox> 
                &nbsp;/ <asp:TextBox ID="txtYY" runat="server" Width="52px" BorderColor="Navy" 
                    BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
            </td>
            <td width="30%" rowspan="7">
                <img alt="" src="images/documentmanagement.jpg" 
                    style="width: 271px; height: 264px" /></td>
        </tr>
        <tr>
            <td width="15%">Subject</td>
            <td colspan="3" style="width: 40%">
                <asp:TextBox ID="txtSubject" runat="server" Width="498px" BorderColor="Navy" 
                    BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td width="15%">Contents Details</td>
            <td colspan="3" style="width: 40%">
                <asp:TextBox ID="txtContent" runat="server" Width="498px" Height="130px" 
                    TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td width="15%">Browse Document</td>
            <td colspan="3" style="width: 40%">

                <asp:FileUpload ID="FileUpload1" runat="server" BorderColor="Navy" 
                    BorderStyle="Solid" BorderWidth="1px" />
            &nbsp;&nbsp;

                <asp:Button ID="btnOpen" runat="server" Text="Open File" Width="81px" 
                    BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" Height="20px"  />
&nbsp;<asp:TextBox ID="txtFileName" runat="server" Width="53px" BorderColor="Navy" 
                    BorderStyle="Solid" BorderWidth="1px" Height="16px" Visible="False"></asp:TextBox>
            &nbsp;<asp:TextBox ID="txtDocID" runat="server" Width="33px" BorderColor="Navy" 
                    BorderStyle="Solid" BorderWidth="1px" Height="16px" Visible="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td width="15%">&nbsp;</td>
            <td colspan="3" style="width: 40%">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="15%">&nbsp;</td>
            <td colspan="3" style="width: 40%">
                <asp:Label ID="lblStatus" runat="server" Font-Bold="True" ForeColor="Navy" 
                    Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="15%">&nbsp;</td>
            <td colspan="3" style="width: 40%">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="15%">&nbsp;</td>
            <td colspan="3" style="width: 40%">
                <asp:Button ID="btnNew" runat="server" Text="New" Width="104px" 
                    BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" />
&nbsp;&nbsp;
                <asp:Button ID="btnSave" runat="server" Text="Save" Width="104px" 
                    BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" />
&nbsp;&nbsp;
                <asp:Button ID="btnRemove" runat="server" Text="Remove" Width="104px" 
                    BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" />
            </td>
            <td width="30%">&nbsp;</td>
        </tr>
    </table>

</asp:Content>
