<%@ Page Language="VB" MasterPageFile="~/IdiaryPanel/Parent/ParentMaster.master" AutoEventWireup="false" Inherits="iDiary_V3.Parent_SubmitThought" title="Untitled Page" Codebehind="SubmitThought.aspx.vb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Heading" runat="server">
    Submit Thoughts
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="col_3" style="margin-top: 20px;" id="panelEnquiryNo" runat="server">
        <div class="col-md-3 widget widget1" style="width: 100%">
            <div class="r3_counter_box">
                <table class="table">
                    <tr>
                        <td>Your Thought</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtThought" runat="server" Width="440px" Height="100px"
                                TextMode="MultiLine"></asp:TextBox>
                            
                        </td>
                    </tr>
                    <tr>
                        <td>
                            
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" class="btn btn-primary"/></td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="clearfix"></div>
    </div>
</asp:Content>

