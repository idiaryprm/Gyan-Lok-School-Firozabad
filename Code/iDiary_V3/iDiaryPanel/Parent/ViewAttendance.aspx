<%@ Page Language="VB" MasterPageFile="~/iDiaryPanel/Parent/ParentMaster.master" AutoEventWireup="false" Inherits="iDiary_V3.Parent_ViewAttendance" title="Untitled Page" Codebehind="ViewAttendance.aspx.vb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Heading" runat="server">
    Attendance
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%--<div class="cntbox">
	    <h2><span>Attendance</span></h2>
    </div>--%>
     <div class="col_3" style="margin-top: 20px;" id="panelEnquiryNo" runat="server">
        <div class="col-md-3 widget widget1" style="width: 100%">
            <div class="r3_counter_box">
                <table border="0" cellpadding="1" cellspacing="1" width="440px">
        <tr>
            <td style="width: 60px"><b>Month</b></td>
            <td style="width: 100px">
                <asp:DropDownList ID="cboMonth" runat="server" Width="80px">
                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem Value="Jan">Jan</asp:ListItem>
                    <asp:ListItem Value="Feb"></asp:ListItem>
                    <asp:ListItem Value="Mar"></asp:ListItem>
                    <asp:ListItem Value="Apr"></asp:ListItem>
                    <asp:ListItem Value="May"></asp:ListItem>
                    <asp:ListItem Value="Jun"></asp:ListItem>
                    <asp:ListItem Value="Jul"></asp:ListItem>
                    <asp:ListItem Value="Aug"></asp:ListItem>
                    <asp:ListItem Value="Sep"></asp:ListItem>
                    <asp:ListItem Value="Oct"></asp:ListItem>
                    <asp:ListItem Value="Nov"></asp:ListItem>
                    <asp:ListItem Value="Dec"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style="width: 60px"><b>Year</b></td>
            <td style="width: 100px">
                <asp:TextBox ID="txtYear" runat="server" BorderWidth="1px" Width="80px"></asp:TextBox>
            </td>
            <td align="left">
                <asp:LinkButton CssClass="circular" ID="btnShow" runat="server">Show</asp:LinkButton>
            </td>
        </tr>
    </table>
    <br />
    <asp:Table ID="myTable" runat="server" Width="440px" GridLines="Both"></asp:Table>

    <br />
    <asp:Label ID="lblStatus" runat="server" Font-Bold="True" ForeColor="Navy"></asp:Label>
    

            </div>
        </div>
        <div class="clearfix"></div>
    </div>
</asp:Content>

