<%@ Page UnobtrusiveValidationMode="none" Language="VB" MasterPageFile="~/IdiaryPanel/Parent/ParentMaster.master" AutoEventWireup="false" Inherits="iDiary_V3.Parent_ComposeMessage" title="Untitled Page" Codebehind="ComposeMessage.aspx.vb" %>
<%@ MasterType virtualpath="~/IdiaryPanel/Parent/ParentMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Heading" runat="server">
    Compose Message
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="col_3" style="margin-top: 20px;" id="panelEnquiryNo" runat="server">
        <div class="col-md-3 widget widget1" style="width: 100%">
            <div class="r3_counter_box">
                <table class="table">
                    <tr>
                        <td>
                            <b>To</b>
                            <br />
                            <asp:DropDownList ID="cboTo" runat="server" Width="150px">
                                <asp:ListItem>Class Teacher</asp:ListItem>
                                <asp:ListItem>Principal</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>Subject</b>
                            <br />
                            <asp:TextBox ID="txtSubject" runat="server" Width="250px"></asp:TextBox>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                ControlToValidate="txtSubject" ErrorMessage="RequiredFieldValidator">Invalid 
                    Subject...</asp:RequiredFieldValidator>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <b>Message</b>
                            <br />
                            <asp:TextBox ID="txtMessage" runat="server" Height="200px" Width="440px" TextMode="MultiLine"></asp:TextBox>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                ControlToValidate="txtMessage" ErrorMessage="Invalid Message...">Invalid 
                    Message</asp:RequiredFieldValidator>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <br />
                            <asp:Label ID="lblStatus" runat="server" Font-Bold="True" ForeColor="Navy"></asp:Label>
                            <br />
                            <br />
                            <asp:Button ID="btnSend" runat="server" Text="Send Message" class="btn btn-primary"/>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="clearfix"></div>
    </div>
</asp:Content>

