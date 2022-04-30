<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="BusDetails.aspx.vb" Inherits="iDiary_V3.BusDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Heading" runat="server">
    Manage Bus Details
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <table border="0" width="100%">
        <tr>
            <td width="18%" valign="top" align="left">
                Registration No:
            </td>
            <td width="2%" valign="top" align="left">&nbsp;</td>
                
            <td width="28%" align="left" valign="top" style="font-weight: bold">
                <asp:TextBox ID="txtRegNo" runat="server" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
            &nbsp;<asp:Button ID="btnGO" runat="server" Text="GO" Width="70px" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px" Font-Size="X-Small" Height="20px" />
                <asp:TextBox ID="txtSID" runat="server" Visible="False"></asp:TextBox>
            </td>
            <td width="2%" valign="top" align="left">&nbsp;</td>
            <td width="50%" align="left" valign="top" style="font-weight: bold"></td>
        </tr>

        <tr>
            <td width="18%" valign="top" align="left">
                Student Name:
            </td>
            <td width="2%" valign="top" align="left">&nbsp;</td>
                
            <td width="28%" align="left" valign="top" style="font-weight: bold">
                <asp:TextBox ID="txtName" runat="server" ReadOnly="true" Width="195px" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
            </td>
            <td width="2%" valign="top" align="left">&nbsp;</td>
            <td width="50%" align="left" valign="top" style="font-weight: bold"></td>
        </tr>

        <tr>
            <td width="18%" valign="top" align="left">
                Class /Section:
            </td>
            <td width="2%" valign="top" align="left">&nbsp;</td>
                
            <td width="28%" align="left" valign="top" style="font-weight: bold">
                <asp:TextBox ID="txtClass" runat="server" Width="195px" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
            </td>
            <td width="2%" valign="top" align="left">&nbsp;</td>
            <td width="50%" align="left" valign="top" style="font-weight: bold"></td>
        </tr>

        <tr>
            <td width="18%" valign="top" align="left">
                Bus Name:
            </td>
            <td width="2%" valign="top" align="left">&nbsp;</td>
                
            <td width="28%" align="left" valign="top" style="font-weight: bold">
                <asp:DropDownList ID="lstBuses" runat="server" AutoPostBack="True"></asp:DropDownList>
            </td>
            <td width="2%" valign="top" align="left">&nbsp;</td>
            <td width="50%" align="left" valign="top" style="font-weight: bold">&nbsp;</td>
        </tr>

        <tr>
            <td width="18%" valign="top" align="left">
                Trip No:
            </td>
            <td width="2%" valign="top" align="left">&nbsp;</td>
                
            <td width="28%" align="left" valign="top" style="font-weight: bold">
                <asp:DropDownList ID="lstTripNo" runat="server"></asp:DropDownList>
            </td>
            <td width="2%" valign="top" align="left">&nbsp;</td>
            <td width="50%" align="left" valign="top" style="font-weight: bold">&nbsp;</td>
        </tr>

        <tr>
            <td width="18%" valign="top" align="left">
                &nbsp;
            </td>
            <td width="2%" valign="top" align="left">&nbsp;</td>
                
            <td width="80%" align="left" valign="top" style="font-weight: bold">
                <asp:Button ID="btnSave" runat="server" Text="Save" Width="70px" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px" />
                &nbsp;&nbsp;
                <asp:Button ID="btnBack" runat="server" Text="Back" Width="70px" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px" />
            </td>
        </tr>
            
        <tr>
            <td width="18%" valign="top" align="left">
                &nbsp;</td>
            <td width="2%" valign="top" align="left">&nbsp;</td>
                
            <td width="80%" align="left" valign="top" style="font-weight: bold">
                &nbsp;</td>
        </tr>
            
        <tr>
            <td width="18%" valign="top" align="left">
                &nbsp;</td>
            <td width="2%" valign="top" align="left">&nbsp;</td>
                
            <td width="80%" align="left" valign="top" style="font-weight: bold">
                <asp:Label ID="lblStatus" runat="server"></asp:Label></td>
        </tr>
            
    </table>

</asp:Content>
