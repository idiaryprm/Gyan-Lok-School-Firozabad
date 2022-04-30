<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/StudentMaster.master" CodeBehind="ClassMaster.aspx.vb" Inherits="iDiary_V3.ClassMaster" %>
<asp:Content ID="Content2" ContentPlaceHolderID="StudentMasterContents" runat="server">
   <%-- <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />--%>
        <table border="0" width="100%">
        <tr>
            <td width="18%" valign="top" align="left">
                <asp:ListBox ID="lstClasses" runat="server" AutoPostBack="True" Height="329px" 
                    Width="192px"></asp:ListBox>
            </td>
            <td valign="middle" align="center" style="width: 9%">
                <asp:ImageButton ID="btnUp" runat="server" Height="30px" 
                    ImageUrl="~/images/UpArrow.jpg" Width="30px" />
                <br />
                <br />
                <asp:ImageButton ID="btnDown" runat="server" Height="30px" 
                    ImageUrl="~/images/DownArrow.jpg" Width="30px" />
            </td>
                
            <td width="41%" align="left" valign="top" style="font-weight: bold">
            
                Class Name&nbsp; 
                <br />
                <asp:TextBox ID="txtClassName" runat="server" CssClass="textbox"></asp:TextBox>
                &nbsp;&nbsp;
                <br />
                Display Order<br />
                <asp:TextBox ID="txtOrderNo" runat="server" CssClass="textbox">0</asp:TextBox>
                <br />
                Next Promoted Class<br />
                <asp:TextBox ID="txtNextClass" runat="server" CssClass="textbox" ToolTip="This Class Name will be used in Report Card for giving next class."></asp:TextBox>
                <br />
                <br />
                <asp:DropDownList ID="cboExamGroup" runat="server" Visible="false" Enabled="false"  CssClass="Dropdown">
                </asp:DropDownList>
                <br />
                <br />
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" />
&nbsp;&nbsp;
                <asp:Button ID="btnRemove" runat="server" Text="Remove" Visible="false" CssClass="btn btn-primary" />
            
                &nbsp;<asp:Button ID="btnNew" runat="server" Text="New" CssClass="btn btn-primary" />
            
                <br />
                <asp:Label ID="lblStatus" runat="server" Font-Bold="True" ForeColor="Navy"></asp:Label>
            
                <br />
                <asp:TextBox ID="txtClassID" runat="server" Width="58px" Visible="False" BorderWidth="1px"></asp:TextBox>
            
                <br />
            
            </td>
            <td width="39%" align="right" valign="top" style="font-weight: bold">
            <img src="Images/ClassRoomMaster.jpg" style="width: 230px; height: 174px;" />
            </td>
            
        </tr>
    </table>


</asp:Content>
