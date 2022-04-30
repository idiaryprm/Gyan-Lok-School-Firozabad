<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="NewsPaperTransact.aspx.vb" Inherits="iDiary_V3.NewPaperTransact" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Heading" runat="server">
    News Paper Entry
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="col_3" style="margin-top: 20px;" id="panelEnquiryNo" runat="server">
        <div class="col-md-3 widget widget1" style="width: 100%">
            <div class="r3_counter_box">
                <table class="table">
                    <tr>
                        <td width="18%" valign="top" align="left">
                            <asp:CheckBoxList ID="cboNewsPapers" runat="server" Width="228px">
                            </asp:CheckBoxList>
                        </td>
                        <td width="2%" valign="top" align="left">&nbsp;</td>

                        <td width="41%" align="left" valign="top" style="font-weight: bold">

                            <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="True" Text="Select All/ None" />
                            <br />
                            <br />
                            Date<br />
                            <br />
                            <asp:TextBox ID="txtDate" runat="server" CssClass="textbox"></asp:TextBox>
                            <asp:CalendarExtender ID="txtDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDate"></asp:CalendarExtender>
                            &nbsp;&nbsp;
                <br />
                            <br />
                            <br />
                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" />
                            &nbsp;&nbsp;
                            <asp:Button ID="btnRemove" runat="server" Text="Remove" CssClass="btn btn-primary" />
                            &nbsp;&nbsp;
                            
                <br />
                            <br />
                            <asp:Label ID="lblStatus" runat="server"></asp:Label>

                            <br />
                            <asp:TextBox ID="txtNewsPaperID" runat="server" CssClass="textbox" Visible="False"></asp:TextBox>

                            <br />

                        </td>
                        <td width="39%" align="right" valign="top" style="font-weight: bold">
                            <asp:ListBox ID="ListPaperID" runat="server" Visible="False"></asp:ListBox>
                            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                        </td>

                    </tr>
                </table>
            </div>
        </div>
        <div class="clearfix"></div>
    </div>

</asp:Content>
