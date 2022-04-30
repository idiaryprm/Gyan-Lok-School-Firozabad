<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/FeeMasterPage.master" CodeBehind="FeeTermMaster.aspx.vb" Inherits="iDiary_V3.FeeTermMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FeeMasterContents" runat="server">
    <%--<br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />--%>
    <table class="table">
        <tr>
            <td style="width: 279px">
                <asp:ListBox ID="lstTerm" runat="server" Height="294px" Width="278px" AutoPostBack="True"></asp:ListBox>
            </td>
            <td style="width: 18px">&nbsp;</td>
            <td valign="top" style="width: 226px"><strong>Fee Group Name<br />
                <asp:DropDownList ID="cboGroup" runat="server" CssClass="Dropdown" AutoPostBack="True">
                </asp:DropDownList>
                <br />
                Installment Number<br />
                <asp:DropDownList ID="cboTermNo" runat="server" CssClass="Dropdown" Enabled="False">
                    <asp:ListItem>I</asp:ListItem>
                    <asp:ListItem>II</asp:ListItem>
                    <asp:ListItem>III</asp:ListItem>
                    <asp:ListItem>IV</asp:ListItem>
                    <asp:ListItem>V</asp:ListItem>
                    <asp:ListItem>VI</asp:ListItem>
                    <asp:ListItem>VII</asp:ListItem>
                    <asp:ListItem>VIII</asp:ListItem>
                    <asp:ListItem>IX</asp:ListItem>
                    <asp:ListItem>X</asp:ListItem>
                    <asp:ListItem>XI</asp:ListItem>
                    <asp:ListItem>XII</asp:ListItem>
                </asp:DropDownList>
                
                <br />
                Installment Name<br />
                <asp:TextBox ID="txtTermName" runat="server" CssClass="textbox" Enabled="False"></asp:TextBox>
                <br />
                Display Order<br />
                <asp:TextBox ID="txtDispOrder" runat="server" CssClass="textbox"></asp:TextBox>
                <br />
                <asp:Label ID="lblStatus" runat="server" Font-Bold="True" ForeColor="Navy" Text="" style="color: #FF0000"></asp:Label>
                <br />
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" />
                &nbsp;
                <asp:Button ID="btnNew" runat="server" Text="New" CssClass="btn btn-primary" />
                &nbsp;
                <asp:Button ID="btnRemove" runat="server" Text="Remove" CssClass="btn btn-primary" Visible="false" />
                <br />
                <asp:TextBox ID="txtId" runat="server" Width="49px" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" Visible="False"></asp:TextBox>
            </strong></td>
            <td valign="top">
                <table>
                    <tr>
                        <td style="height: 29px"><b>Select Months</b></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBoxList ID="chkMonth" runat="server" CellPadding="2" CellSpacing="2" Width="220px" RepeatColumns="3" RepeatDirection="Horizontal">
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                </table>

            </td>
        </tr>
    </table>
</asp:Content>
