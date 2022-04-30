<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/PayrollTransaction.master" CodeBehind="EmpImportAttendance.aspx.vb" Inherits="iDiary_V3.EmpImportAttendance" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SubHeading" runat="server">
    Import Employee Attendance
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CertificateContent" runat="server">
   
  <%--  <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />--%>

    <table class="table">
       
        <tr>
            <td style="width: 15%; height: 26px;">Choose File</td>
            <td style="width: 26%; height: 26px;">
                <asp:FileUpload ID="myFile" runat="server" CssClass="FileUpload" TabIndex="-1" AllowMultiple="True" />
            </td>
            <td style="width: 15%; height: 26px;">&nbsp;</td>
            <td style="width: 29%; height: 26px;">
                &nbsp;</td>
            <td style="width: 15%; height: 26px;">&nbsp;</td>
            <td style="width: 15%; height: 26px;">
                &nbsp;</td>
        </tr>
              <tr>
            <td style="width: 15%; height: 26px;">Emp Category</td>
            <td style="width: 26%; height: 26px;">
                <asp:DropDownList ID="cboEmpCat" runat="server" CssClass="Dropdown"></asp:DropDownList>
            </td>
            <td style="width: 15%; height: 26px;">Status</td>
            <td style="width: 29%; height: 26px;">
                <asp:DropDownList ID="cboStatus" runat="server" CssClass="Dropdown"></asp:DropDownList>
                </td>
            <td style="width: 15%; height: 26px;">&nbsp;</td>
            <td style="width: 15%; height: 26px;">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 15%">Date From</td>
            <td style="width: 26%">
                 <asp:TextBox ID="txtFromDate" runat="server" CssClass="textbox"></asp:TextBox>  
                 <ajaxToolkit:CalendarExtender ID="txtFromDate_CalendarExtender" runat="server" TargetControlID="txtFromDate" Format="dd/MM/yyyy">
                </ajaxToolkit:CalendarExtender>
            </td>
            <td style="width: 15%">Date To</td>
            <td style="width: 29%">
                 <asp:TextBox ID="txtToDate" runat="server" CssClass="textbox"></asp:TextBox>            
                <ajaxToolkit:CalendarExtender ID="txtToDate_CalendarExtender" runat="server" TargetControlID="txtToDate" Format="dd/MM/yyyy">
                </ajaxToolkit:CalendarExtender>
                            </td>
            <td style="width: 15%">
                &nbsp;</td>
            <td style="width: 15%"></td>
        </tr>
        <tr>
            <td style="width: 15%">Set In Time</td>
            <td style="width: 26%">
                <asp:Panel ID="PanelInTime" runat="server">
                    <asp:DropDownList ID="ddlInHH" Width="55px" CssClass="Dropdown" runat="server"></asp:DropDownList>:
                    <asp:DropDownList ID="ddlInMM" Width="55px" CssClass="Dropdown" runat="server"></asp:DropDownList>:
                    <asp:DropDownList ID="ddlInMer" Width="55px" CssClass="Dropdown" runat="server">
                        <asp:ListItem>AM</asp:ListItem>
                        <asp:ListItem>PM</asp:ListItem>
                    </asp:DropDownList>
                </asp:Panel>
            </td>
            <td style="width: 15%">Set Out Time</td>
            <td style="width: 29%">
                <asp:Panel ID="PanelOutTime" runat="server">
                     <asp:DropDownList ID="ddlOutHH" Width="55px" CssClass="Dropdown" runat="server"></asp:DropDownList>:
                    <asp:DropDownList ID="ddlOutMM" Width="55px" CssClass="Dropdown" runat="server"></asp:DropDownList>:
                    <asp:DropDownList ID="ddlOutMer" Width="55px" CssClass="Dropdown" runat="server">
                        <asp:ListItem>AM</asp:ListItem>
                        <asp:ListItem>PM</asp:ListItem>
                    </asp:DropDownList>
                </asp:Panel>
            </td>
            <td style="width: 15%">
                &nbsp;</td>
            <td style="width: 15%"></td>
        </tr>
          <tr>
            <td style="width: 15%">Absent SMS</td>
            <td colspan="3" style="vertical-align:top;" rowspan="2">
                <asp:TextBox ID="txtMessage" runat="server" CssClass="textbox"  TextMode="MultiLine" Width="364px" Height="43px"></asp:TextBox>
              </td>
            <td style="width: 15%">
                 <asp:CheckBox ID="ckbSMS" runat="server" style="color: #FF0000;" Text="Send SMS" />
              </td>
            <td style="width: 15%"></td>
        </tr>
          <tr>
            <td style="width: 15%">
                 &nbsp;&nbsp;
                 </td>
            <td style="width: 15%"></td>
            <td style="width: 15%"></td>
        </tr>
         <tr>
            <td style="width: 15%; height: 26px;">Late SMS</td>
            <td colspan="3" rowspan="2">
                <asp:TextBox ID="txtMessageLate" runat="server" CssClass="textbox"  TextMode="MultiLine" Width="364px" Height="43px"></asp:TextBox>
              </td>
            <td style="width: 15%; height: 26px;">
                <asp:Button ID="btnImport" runat="server" Text="Import" CssClass="btn btn-primary" Width="100px" />
             </td>
            <td style="width: 15%; height: 26px;">
                &nbsp;</td>
        </tr>
         <tr>
            <td style="width: 15%; height: 26px;">&nbsp;</td>
            <td style="width: 15%; height: 26px;">&nbsp;</td>
            <td style="width: 15%; height: 26px;">
                &nbsp;</td>
        </tr>
          <tr>
            <td colspan="4">
                 <asp:Label ID="lblStatus" runat="server" style="font-weight: 700; color: #FF0000"></asp:Label>
              </td>
            <td style="width: 15%">
                <asp:Button ID="btnProcess" runat="server" Text="Process" CssClass="btn btn-primary" Width="100px" Visible="False" />
              </td>
            <td style="width: 15%"></td>
        </tr>
        <tr>
            <td colspan="6">
                 <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            </td>
        </tr>
    </table>
</asp:Content>
