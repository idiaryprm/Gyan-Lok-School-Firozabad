<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ExamAdminMasterPage.Master" CodeBehind="ExamAdminHome.aspx.vb" Inherits="iDiary_V3.ExamAdminHome" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SubHeading" runat="server">
    Exam Admin Home
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    
    <br />
    <asp:Button ID="btnMarksInitialize" runat="server" Text="Marks initialization" CssClass="btn btn-primary" />
    <br />
    <br />
    <asp:Button ID="btnProcess" runat="server" Text="Process Marks" CssClass="btn btn-primary" />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="btnActivity" runat="server" Text="Activity Mapping" CssClass="btn btn-primary" />
    <br />
    <asp:Label ID="lblSchoolType" runat="server" Visible="false"  Text=""></asp:Label>
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
                                  
    <br />
    <br /><br />
    <br />
    
    <br />
    <br />
    <br />
</asp:Content>
