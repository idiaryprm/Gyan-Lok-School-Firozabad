<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/FeeMasterPage.master" CodeBehind="FeeConcessionTypeMaster.aspx.vb" Inherits="iDiary_V3.FeeConcessionTypeMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FeeMasterContents" runat="server">
   <%-- <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />        --%>
    <table class="table">
        <tr>
            <td width="40%" valign="top">
                <asp:ListBox ID="lstMasters" runat="server" Height="268px" Width="278px" 
                    AutoPostBack="True"></asp:ListBox>
            </td>
            <td valign="top" align="left" style="width: 27%">
                <b>Concession Head Name</b>
                <br />
                <asp:TextBox ID="txtName" runat="server" CssClass="textbox"></asp:TextBox>
                <br />
                <strong>Concession Type</strong><br />
                <asp:DropDownList ID="cboConcessionType" runat="server" CssClass="Dropdown">
                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem>Percentage</asp:ListItem>
                    <asp:ListItem>Fixed Amount</asp:ListItem>
                </asp:DropDownList>
                <br />
                <strong>Concession % / Amount</strong><br />
                <asp:TextBox ID="txtAmount" runat="server" CssClass="textbox"></asp:TextBox>
                <br />
                <span style="color: #6600FF">* Concession is Monthly applicable for each Fee Head</span><br />
                <asp:Label ID="lblStatus" runat="server" Font-Bold="True" ForeColor="Navy" Text="" style="color: #FF3300"></asp:Label>
                <br />
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" />
&nbsp;
                <asp:Button ID="btnNew" runat="server" Text="New" CssClass="btn btn-primary" />
&nbsp;
                <asp:Button ID="btnRemove" runat="server" Text="Remove" Visible="false" CssClass="btn btn-primary" />
                <br />
                <asp:TextBox ID="txtID" runat="server" ReadOnly="True" Visible="False" 
                    Width="74px"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp;&nbsp;
                </td>
            <td width="60%" valign="top" align="left">
                <table>
                    <tr>
                        <td style="height: 29px">
                <strong>Concession for Fee Type</strong></td>
                    </tr>
                    <tr>
                        <td style="height: 29px">
                            <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="True" style="font-weight: 700" Text="Check All" />
                        </td>
                    </tr>
                    <tr>
                        <td>
  <asp:CheckBoxList ID="chkFeeType" runat="server"  width="230px" RepeatColumns="2" RepeatDirection="Horizontal">
                    </asp:CheckBoxList>
                        </td>
                    </tr>
                </table>
                  
                </td>
        </tr>
    </table>

</asp:Content>
