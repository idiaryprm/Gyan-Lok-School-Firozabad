<%@ Page Language="VB" MasterPageFile="~/AdminMaster.master" AutoEventWireup="false" Inherits="iDiary_V3.TeacherSubjectMap" title="Untitled Page" Codebehind="TeacherSubjectMap.aspx.vb" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AdminMasterContents" Runat="Server">
    
   <table width="100%" cellpadding="2" cellspacing="2" border="0" style="height: 41px">
        <tr>
            <td valign="top" style="height: 6px;">
                <strong>Select Teacher</strong></td>
            <td valign="top" style="height: 6px;">
                <asp:DropDownList ID="cboEmpName" runat="server" Width="208px" AutoPostBack="True">
                </asp:DropDownList>
                </td>
            <td valign="top" style="width: 20%; " rowspan="4">
                &nbsp;</td>


        </tr>
        <tr>
            <td valign="top" style="width: 22%; height: 21px;">
                <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="True" Text="Select All/ None" />
                <br />
                <br />
                <asp:Label ID="lblStatus" runat="server" Font-Bold="True" ForeColor="Navy" Text=""></asp:Label>
                </td>
            <td valign="top" style="height: 21px;">
                <div style="overflow:scroll; height:300px">
                <asp:CheckBoxList ID="chkSubject" runat="server">
                </asp:CheckBoxList>

                
                    </div>
                </td>


        </tr>
        <tr>
            <td valign="top" style="width: 22%; height: 21px;">
                &nbsp;</td>
            <td valign="top" style="height: 21px;">
                 <asp:Button ID="btnSave" runat="server" Text="Save" Width="100px" />
                &nbsp;</td>


        </tr>
        <tr>
            <td valign="top" style="width: 22%; height: 21px;">
                &nbsp;</td>
            <td valign="top" style="height: 21px;">
               
            </td>


        </tr>
    </table>
</asp:Content>

