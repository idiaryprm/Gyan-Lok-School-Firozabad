<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/PayrollTransaction.master" CodeBehind="EmpGeneratePayBill.aspx.vb" Inherits="iDiary_V3.GeneratePayBill" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SubHeading" runat="server">
    Generate Pay Bill
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CertificateContent" runat="server">
     <table class="table">
        <tr>
            <td width="10%">Month</td>
            <td width="15%">
                <asp:DropDownList ID="cboMonth" runat="server" CssClass="Dropdown"></asp:DropDownList>
            </td>
            <td width="10%">Year</td>
            <td width="15%">
                <asp:DropDownList ID="cboYear" runat="server" CssClass="Dropdown">
                </asp:DropDownList>
            </td>
            <td width="17%">Category</td>
            <td width="18%" style="margin-left: 200px">
                <asp:DropDownList ID="cboEmpCat" runat="server" CssClass="Dropdown">
                </asp:DropDownList>
            </td>
            <td width="15%" style="margin-left: 40px">
                <asp:Button ID="btnPayBill" runat="server" Text="Generate Pay Bill" 
                    CssClass="btn btn-primary" />
            </td>
        </tr>
        <tr>
            <td width="10%">&nbsp;</td>
            <td width="15%">
                &nbsp;</td>
            <td width="10%">&nbsp;</td>
            <td width="15%">
                &nbsp;</td>
            <td width="17%">&nbsp;</td>
            <td width="18%" style="margin-left: 200px">
                &nbsp;</td>
            <td width="15%" style="margin-left: 40px">
                &nbsp;</td>
        </tr>
    </table>
    
    <table width="100%" cellpadding="2" cellspacing="2" border="0">
        <tr>
            <td width="100%">
                <div id="gvDiv" align="center">
                <asp:GridView ID="GridView1" runat="server" CssClass="Grid" Width="100%" DataSourceID="SqlDataSource1">
                   
                </asp:GridView>
                </div>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT * FROM [rptPayBill]"></asp:SqlDataSource>
                <br />
                <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="btn btn-primary" />
                &nbsp;&nbsp;
                <asp:Button ID="btnExcel" runat="server" Text="Export to Excel" CssClass="btn btn-primary" />
            </td>
        </tr>
    </table>    

</asp:Content>
