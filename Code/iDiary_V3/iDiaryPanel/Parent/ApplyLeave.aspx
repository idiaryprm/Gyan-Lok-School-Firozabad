<%@ Page UnobtrusiveValidationMode="none" Language="VB" MasterPageFile="~/iDiaryPanel/Parent/ParentMaster.master" AutoEventWireup="false" Inherits="iDiary_V3.Parent_ApplyLeave" title="Untitled Page" Codebehind="ApplyLeave.aspx.vb" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Heading" runat="server">
    Apply Leave Online
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="col_3" style="margin-top: 20px;" id="panelEnquiryNo" runat="server">
        <div class="col-md-3 widget widget1" style="width: 100%">
            <div class="r3_counter_box">
                <table class="table">
                    <tr>
                        <td width="50px"><b>From:</b></td>
                        <td width="100px">
                            <asp:TextBox ID="txtFrom" CssClass="textbox" runat="server"></asp:TextBox>
                            <asp:CalendarExtender ID="txtFrom_CalendarExtender" Format="dd/MM/yyyy" runat="server" TargetControlID="txtFrom"></asp:CalendarExtender>
                        </td>
                        <td width="50px"><b>To:</b></td>
                        <td width="100px">
                            <asp:TextBox ID="txtTo" CssClass="textbox" runat="server"></asp:TextBox>
                            <asp:CalendarExtender ID="txtTo_CalendarExtender" Format="dd/MM/yyyy" runat="server" TargetControlID="txtTo"></asp:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <b>Reason</b>
                            <br />
                            <asp:TextBox ID="txtReason" runat="server" Width="250px" CssClass="textbox"></asp:TextBox>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                ControlToValidate="txtReason" ErrorMessage="RequiredFieldValidator">Reason Required...</asp:RequiredFieldValidator>
                        </td>
                    </tr>

                    <tr>
                        <td colspan="4">
                            <b>Message (Optional)</b>
                            <br />
                            <asp:TextBox ID="txtMessage" runat="server" Height="200px" Width="440px" CssClass="textbox" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td colspan="4">
                            <br />
                            <asp:Label ID="lblStatus" runat="server" Font-Bold="True" ForeColor="Navy"></asp:Label>
                            <br />
                            <br />
                            <asp:Button ID="btnApply" runat="server" Text="Apply Leave" class="btn btn-primary"/>
                            <br />
                            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                        </td>
                    </tr>


                </table>
            </div>
        </div>
        <div class="clearfix"></div>
    </div>
     
    

</asp:Content>

