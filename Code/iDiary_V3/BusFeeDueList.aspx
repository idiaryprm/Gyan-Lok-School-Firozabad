<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="BusFeeDueList.aspx.vb" Inherits="iDiary_V3.BusFeeDueList" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Heading" runat="server">
    Bus Fee Due List
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <%--<br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
        <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
        <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
        <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />--%>
    <div class="col_3" style="margin-top: 20px;" id="panelEnquiryNo" runat="server">
        <div class="col-md-3 widget widget1" style="width: 100%">
            <div class="r3_counter_box">
                <table style="width:100%">
        <tr>
            <td class="auto-style3">
                School Name</td>
            <td class="auto-style3">
                 <asp:DropDownList ID="cboSchoolName" runat="server" CssClass="Dropdown" Width="300px" AutoPostBack="true" ></asp:DropDownList></td>
            <td class="auto-style4">
                Class</td>
            <td class="auto-style5">
                <asp:DropDownList ID="cboClass" runat="server" CssClass="Dropdown" AutoPostBack="True" 
                    >
                </asp:DropDownList>
            </td>
            <td class="auto-style3">
                &nbsp;</td>
            <td class="auto-style3">
                Section</td>
            <td class="auto-style3">
                <asp:DropDownList ID="cboSection" runat="server" CssClass="Dropdown">
                </asp:DropDownList>
                </td>
        </tr>
        <tr>
            <td>
                Status</td>
            <td>
                <asp:DropDownList ID="cboStatus" runat="server" CssClass="Dropdown">
                </asp:DropDownList>
            </td>
            <td class="auto-style1">
                Upto Term</td>
            <td class="auto-style2">
                <asp:DropDownList ID="cboTerm" runat="server" CssClass="Dropdown">
                   <%-- <asp:ListItem>April - June (Term-1)April - June (Term-1)</asp:ListItem>
                    <asp:ListItem>July - September (Term-2)July - September (Term-2)</asp:ListItem>
                    <asp:ListItem>October - December (Term-3)October - December (Term-3)</asp:ListItem>
                    <asp:ListItem>January - March (Term-4)January - March (Term-4)</asp:ListItem>
                    <asp:ListItem>Full YearFull Year</asp:ListItem>
                    <asp:ListItem Selected="True">-</asp:ListItem>--%>
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;</td>
            <td>
                <asp:Button ID="btnViewSummaryList" runat="server" 
                    Text="Generate Report" CssClass="btn btn-primary" />
            </td>
            <td width="5%">
                &nbsp;</td>
        </tr>
     
        <tr>
            <td colspan="6">
            &nbsp;<asp:Label ID="lblStatus" runat="server" ForeColor="Navy"></asp:Label>
                &nbsp;
                <asp:Button ID="btnViewDetails" runat="server" style="margin-top: 0px" 
                    Text="View Detailed Report" CssClass="btn btn-primary" Visible="False" />
            &nbsp;&nbsp;
                <asp:Button ID="btnPrint" runat="server" style="margin-top: 0px; " 
                    Text="Print" CssClass="btn btn-primary" Visible="False" />
            &nbsp;&nbsp;
                <asp:Button ID="btnExcel" runat="server" Text="Export to Excel" CssClass="btn btn-primary" Visible="False" />
            &nbsp;&nbsp;
                <asp:Button ID="btnSendSMS" runat="server" Text="Send SMS" 
                    CssClass="btn btn-primary" Visible="False" />
            </td>
            <td >
                &nbsp;</td>
        </tr>
  
        <tr>
            <td colspan="6">
                &nbsp;</td>
            <td >
                &nbsp;</td>
        </tr>
  
        <tr>
            <td colspan="6">
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="83%" ProcessingMode="Remote" Visible="False">
                   
                </rsweb:ReportViewer></td>
            <td >
                &nbsp;</td>
        </tr>
  
        <tr>
            <td colspan="7" style="margin-left: 80px" align="center">
                <div id="gvDiv">
                    <asp:Label ID="lblSchoolName" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
                    <br />
                    <asp:Label ID="lblTitle" runat="server" Font-Bold="True" Font-Size="Small"></asp:Label>
                    <br />
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="Grid"  Width="100%">
                        <Columns>
                            <asp:BoundField DataField="SID" HeaderText="SID" SortExpression="SID" Visible="False" />
                            <asp:BoundField DataField="RegNo" HeaderText="Reg No" SortExpression="RegNo" />
                            <asp:BoundField DataField="FeeBookNo" HeaderText="Computer Code" SortExpression="FeeBookNo" />
                            <asp:BoundField DataField="SName" HeaderText="Student Name" SortExpression="SName" />
                            <asp:BoundField DataField="FName" HeaderText="Father's Name" SortExpression="FName" />
                            <asp:BoundField DataField="ClassName" HeaderText="Class" SortExpression="ClassName" />
                            <asp:BoundField DataField="SecName" HeaderText="Section" SortExpression="SecName" />
                            <asp:BoundField DataField="FeeDepositedAmount" HeaderText="FeeDepositedAmount" SortExpression="FeeDepositedAmount" Visible="False" />
                            <asp:BoundField DataField="FeeConfigAmount" HeaderText="FeeConfigAmount" SortExpression="FeeConfigAmount" Visible="False" />
                            <asp:BoundField DataField="FeeDueAmount" HeaderText="FeeDueAmount" SortExpression="FeeDueAmount" />
                            <asp:BoundField DataField="CollegeNote" HeaderText="CollegeNote" SortExpression="CollegeNote" />
                        </Columns>
                        
                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT * FROM [rptFeeDue]"></asp:SqlDataSource>
                    <br />
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="7" style="margin-left: 80px">
                  <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="iDiary_V3.iDiaryDataSetTableAdapters.rptFeeCollectionTableAdapter">
                    <InsertParameters>
                        <asp:Parameter Name="FeeDepositDate" Type="DateTime" />
                        <asp:Parameter Name="SID" Type="Int32" />
                        <asp:Parameter Name="FeeBookNo" Type="String" />
                        <asp:Parameter Name="SName" Type="String" />
                        <asp:Parameter Name="ClassName" Type="String" />
                        <asp:Parameter Name="SecName" Type="String" />
                        <asp:Parameter Name="FeeAmount" Type="Double" />
                    </InsertParameters>
                </asp:ObjectDataSource>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT * FROM vw_Student where SID<0"></asp:SqlDataSource>
            <asp:DropDownList ID="cboTermID" runat="server" Visible="False">
                                    </asp:DropDownList></td>
        </tr>
        </table>
                </div>
        </div>
        <div class="clearfix"></div>
    </div>  
</asp:Content>
<asp:Content ID="Content3" runat="server" contentplaceholderid="head">
    <style type="text/css">
        .auto-style1 {
            width: 127px;
        }
        .auto-style2 {
            width: 262px;
        }
        .auto-style3 {
            height: 40px;
        }
        .auto-style4 {
            width: 127px;
            height: 40px;
        }
        .auto-style5 {
            width: 262px;
            height: 40px;
        }
    </style>
</asp:Content>

