<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/AdminMaster.master" CodeBehind="ParamsMaster.aspx.vb" Inherits="iDiary_V3.ParamsMaster" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminMasterContents" runat="server">
    
       <table class="table">
        <tr>
            <td style="height: 30px; text-decoration: underline; font-size: 14px;" colspan="2">
                <strong>Param Master<asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                </strong></td>
            <td style="width: 17%; height: 30px">
                &nbsp;</td>
            <td rowspan="10" valign="top" width="65%">               
            </td>
        </tr>
        <tr>
            <td style="width: 22%; height: 30px">
                School Name</td>
            <td style="width: 50%; height: 30px">
                <asp:TextBox ID="txtschoolname" runat="server" BorderStyle="Solid" 
                    BorderWidth="1px" Width="172px"></asp:TextBox>
            &nbsp;
            </td>
            <td style="width: 17%; height: 30px">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 22%; height: 44px">
                School Details</td>
            <td style="width: 50%; height: 44px">
                <asp:TextBox ID="txtschooldetails" runat="server" BorderStyle="Solid" 
                    BorderWidth="1px" Width="172px"></asp:TextBox>
            </td>
            <td style="width: 17%; height: 44px">
                </td>
        </tr>
        <tr>
            <td style="width: 22%; height: 30px">
                SMS Facility</td>
            <td style="width: 50%; height: 30px">
               <asp:DropDownList ID="cbosmsfacility" runat="server" CssClass="Dropdown">
                   <asp:ListItem>Yes</asp:ListItem>
                   <asp:ListItem>No</asp:ListItem>
               </asp:DropDownList>
            </td>
            <td style="width: 17%; height: 30px">
            </td>
        </tr>
        <tr>
            <td style="width: 22%; height: 30px">
                SR / Admin No Same or Not</td>
            
            <td style="width: 50%; height: 30px">
                <asp:DropDownList ID="cbosrno" runat="server" CssClass="Dropdown">
                   <asp:ListItem>Yes</asp:ListItem>
                   <asp:ListItem>No</asp:ListItem>
               </asp:DropDownList>
            </td>
            <td style="width: 17%; height: 30px">
            </td>
        </tr>
            <tr>
            <td style="width: 22%; height: 44px">
                URL Keywords</td>
            <td style="width: 50%; height: 44px">
                <asp:TextBox ID="txturlkeywords" runat="server" BorderStyle="Solid" 
                    BorderWidth="1px" Width="172px"></asp:TextBox>
            </td>
            <td style="width: 17%; height: 44px">
                </td>
        </tr>
            <tr>
            <td style="width: 22%; height: 44px">
                Age Calculate on</td>
            <td style="width: 50%; height: 44px">
                <asp:TextBox ID="txtageon" runat="server" BorderStyle="Solid" 
                    BorderWidth="1px" Width="172px"></asp:TextBox>

                <ajaxToolkit:CalendarExtender ID="txtageon_CalendarExtender" runat="server" TargetControlID="txtageon" Format="dd/MM/yyyy" />

            </td>
            <td style="width: 17%; height: 44px">
                </td>
        </tr>
            <tr>
            <td style="width: 22%; height: 30px">
                On Line Entry Allowed</td>
            
            <td style="width: 50%; height: 30px">
                <asp:DropDownList ID="cboonlineentryallowed" runat="server" CssClass="Dropdown">
                   <asp:ListItem>Yes</asp:ListItem>
                   <asp:ListItem>No</asp:ListItem>
               </asp:DropDownList>
            </td>
            <td style="width: 17%; height: 30px">
            </td>
        </tr>
        <tr>
            <td style="width: 22%; height: 30px">
                </td>
            <td style="width: 50%; height: 30px">
            &nbsp;<asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary"/></td>
            <td style="width: 17%; height: 30px">
            </td>
        </tr>
           
        <tr>
            <td style="width: 22%">
                &nbsp;</td>
            <td style="width: 50%">
                
                &nbsp;
                
            </td>
            <td style="width: 17%">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 22%; height: 53px;">
            </td>
            <td style="width: 50%; height: 53px;">
                <asp:Label ID="lblStatus" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
            </td>
            <td style="width: 17%; height: 53px;">
            </td>
        </tr>
        <tr>
            <td style="width: 22%">
                &nbsp;</td>
            <td style="width: 50%">
                &nbsp;</td>
            <td style="width: 17%">
                &nbsp;</td>
            <td width="65%">
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>
