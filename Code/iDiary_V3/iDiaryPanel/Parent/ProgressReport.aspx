<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/iDiaryPanel/Parent/ParentMaster.master" CodeBehind="ProgressReport.aspx.vb" Inherits="iDiary_V3.ProgressReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Heading" runat="server">
    Progress Report
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="col_3" style="margin-top: 20px;" id="panelEnquiryNo" runat="server">
        <div class="col-md-3 widget widget1" style="width: 100%">
            <div class="r3_counter_box">
                <p>
                    Choose Term&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:DropDownList ID="DropDownList1" runat="server" Width="50px">
            <asp:ListItem>1</asp:ListItem>
            <asp:ListItem>2</asp:ListItem>
        </asp:DropDownList>
                </p>
                <p>
                    <asp:Button ID="btnReport" runat="server" Text="Report Card" class="btn btn-primary"/>
                </p>
            </div>
        </div>
        <div class="clearfix"></div>
    </div>
    
</asp:Content>
