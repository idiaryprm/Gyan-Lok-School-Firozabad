﻿<%@ Page Language="VB" MasterPageFile="~/iDiaryPanel/Parent/ParentMaster.master" AutoEventWireup="false" Inherits="iDiary_V3.Parent_ViewAssignments" title="Untitled Page" Codebehind="ViewAssignments.aspx.vb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Heading" runat="server">
    Assignments
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="col_3" style="margin-top: 20px;" id="panelEnquiryNo" runat="server">
        <div class="col-md-3 widget widget1" style="width: 100%">
            <div class="r3_counter_box">
                <div style="height:1000px;overflow-y:scroll">
                <asp:Table ID="myTable" runat="server" Width="880px" BorderColor="Blue"
                    BorderStyle="Solid" BorderWidth="1px" CellPadding="6" CellSpacing="6"
                    GridLines="Both">
                </asp:Table>
                    </div>
            </div>
        </div>
        <div class="clearfix"></div>
    </div>
    <asp:Button ID="Button1" runat="server" Text="Button" />
</asp:Content>
                        