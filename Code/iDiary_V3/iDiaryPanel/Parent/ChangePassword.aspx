<%@ Page Language="VB" MasterPageFile="~/IdiaryPanel/Parent/ParentMaster.master" AutoEventWireup="false" Inherits="iDiary_V3.Parent_ChangePassword" title="Untitled Page" Codebehind="ChangePassword.aspx.vb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Heading" runat="server">
    Change Password
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="col_3" style="margin-top: 20px;" id="panelEnquiryNo" runat="server">
        <div class="col-md-3 widget widget1" style="width: 100%">
            <div class="r3_counter_box">
                New Password<br />
                <asp:TextBox ID="txtNew" runat="server" BorderWidth="1px" Width="189px"></asp:TextBox>
                <br />
                <br />
                Retype New Password<br />
                <asp:TextBox ID="txtRe" runat="server" BorderWidth="1px" Width="189px"></asp:TextBox>
                <br />
                <br />
                <asp:Label ID="lblStatus" runat="server" Text="Label" Font-Bold="True"
                    ForeColor="Navy"></asp:Label>
                <br />
                <br />
                <asp:Button ID="btnChangePassword" runat="server" Text="Change Password" CssClass="btn btn-primary"  />
            </div>
        </div>
        <div class="clearfix"></div>
    </div>
</asp:Content>

