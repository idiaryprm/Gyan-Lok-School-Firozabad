<%@ Page Title="Academic Session" Language="vb" AutoEventWireup="false" MasterPageFile="~/Masterpage.master" CodeBehind="EmpAcademicSession.aspx.vb" Inherits="iDiary_V3.EmpAcademicSession" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Heading" runat="server">
    Financial Year
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <div class="col_3" style="margin-top: 20px;" id="panelEnquiryNo" runat="server">
        <div class="col-md-3 widget widget1" style="width: 100%">
            <div class="r3_counter_box">
                <table class="table">

                    <tr>
                        <td align="left" valign="middle">
                            <b>Select Financial Year</b><br />
                            <asp:DropDownList ID="cboSession" runat="server" class="Dropdown"></asp:DropDownList>
                            <br />
                            <br />
                            <asp:Button ID="btnNext" runat="server" Text="Next" CssClass="btn btn-primary" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="clearfix"></div>
    </div>

</asp:Content>
