<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ExamAdminMasterPage.Master" CodeBehind="ExamGroupConfig.aspx.vb" Inherits="iDiary_V3.ExamGroupConfig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SubHeading" runat="server">
    Exam Group Configuration
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
   
    <table class="table">

        <tr>
            <td style="width: 30%">

                School Name
            </td>
            <td style="width: 70%">
                
<asp:DropDownList ID="cboSchoolName" runat="server" CssClass="Dropdown" Width="300px" AutoPostBack="true" ></asp:DropDownList>
                <asp:Label ID="lblSchoolID" runat="server" Visible="False"></asp:Label>

            </td>

        </tr>

        <tr>
            <td style="width: 30%">
                    <b>Exam Class Group Name</b>
                    </td>
            <td style="width: 70%">
                <asp:DropDownList ID="cboExamGroup"  AutoPostBack="true"  runat="server" CssClass="Dropdown">
                </asp:DropDownList>
            </td>

        </tr>
         <tr>
            <td style="width: 30%">
                    <strong>Mapped Classes&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</strong></td>
            <td style="width: 70%">
                    <asp:CheckBoxList ID="cblClasses" runat="server" Width="400px" RepeatColumns="3" RepeatDirection="Horizontal">
                    </asp:CheckBoxList>
                    <br />
                <asp:CheckBox ID="cbAll" Text="Check/Unckeck All" runat="server" AutoPostBack="True" />
                </td>

        </tr>
         <tr>
            <td style="width: 30%">
                    <asp:TextBox ID="txtID" runat="server" Width="19px" Visible="False"></asp:TextBox>
                </td>
            <td style="width: 70%">
                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" />
                    </td>

        </tr>
         <tr>
            <td style="width: 30%"></td>
            <td style="width: 70%"></td>

        </tr>

    </table>
  <%--   <table class="table">
        
            <tr>
                <td width="10%>&nbsp;</td>
                <td width="30%">
                    <b>Exam Class Group Name</b>
                    </td>
                <td valign="top" style="width:50%">
                    <strong>Mapped Classes&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </strong>
                     
                    &nbsp;</td>
                <td width="10%">&nbsp;</td>
            </tr>
        
            <tr>
                <td width="10%>&nbsp;</td>
                <td width="30%">
                    <asp:ListBox ID="lstMaster" runat="server" Height="300px" Width="300px"  CssClass="list"
                        AutoPostBack="True"></asp:ListBox>
                </td>
                <td valign="top" style="width:50%">
                    <br />
                    &nbsp;&nbsp;
                                
                    &nbsp;<br />
                    <br />
                </td>
                <td width="10%">&nbsp;</td>
            </tr>
        </table>--%>
</asp:Content>
