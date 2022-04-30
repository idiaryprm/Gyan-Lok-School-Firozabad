
<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="FeeReport.aspx.vb" EnableEventValidation="false"  Inherits="iDiary_V3.FeeCollectionReport" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Heading" runat="server">
    Fee Report  
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <%-- <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
   --%>  <div class="col_3" style="margin-top: 20px;" id="panelEnquiryNo" runat="server">
        <div class="col-md-3 widget widget1" style="width: 100%">
            <div class="r3_counter_box">
                  <table class="table">
        <tr>
            <td class="auto-style10">
                <asp:RadioButton ID="rbCalss" GroupName="MainFilter" Text="School Name" runat="server" Checked="True" />
                </td>
            <td class="auto-style8">
                <asp:DropDownList ID="cboSchoolName" runat="server" AutoPostBack="True" CssClass="Dropdown">
                </asp:DropDownList>
            </td>
            <td class="auto-style3">&nbsp;</td>
            <td class="auto-style11">
                Class</td>
            <td style="text-align: left; " class="auto-style12">
                <asp:DropDownList ID="cboClass" runat="server" AutoPostBack="True" 
                    CssClass="Dropdown">
                </asp:DropDownList>
            </td>
            <td style="text-align: left;" class="auto-style13">
                Section</td>
            <td>
                <asp:DropDownList ID="cboSection" runat="server" CssClass="Dropdown">
                </asp:DropDownList>
            </td>
        </tr>

        <tr>
            <td class="auto-style10">
                <asp:RadioButton ID="rbRegNo" GroupName="MainFilter" Text="Reg No" runat="server" />
                </td>
            <td class="auto-style8">
               <asp:TextBox ID="txtregistrationno" runat="server" CssClass="textbox" ></asp:TextBox>
            </td>
            <td class="auto-style3">&nbsp;</td>
            <td class="auto-style11">
                Status</td>
            <td style="text-align: left; " class="auto-style12">
                <asp:DropDownList ID="cboStatus" runat="server" CssClass="Dropdown">
                </asp:DropDownList>
            </td>
            <td style="text-align: left;" class="auto-style13">
                <asp:CheckBox ID="chkdepositemode" Text="Payment Mode"  runat="server" />
            </td>
            <td>
               <asp:DropDownList ID="cboMode" runat="server" CssClass="Dropdown" DataSourceID="SqlDataSource2" DataTextField="PMName" DataValueField="PMName">
                                    </asp:DropDownList>
                </td>
        </tr>

        <tr>
            <td class="auto-style10">
                &nbsp;</td>
            <td class="auto-style8">
                &nbsp;</td>
            <td class="auto-style3">&nbsp;</td>
            <td class="auto-style11">
                <asp:CheckBox ID="chkBank" Text="School Bank"  runat="server" />
            </td>
            <td style="text-align: left; " class="auto-style12">
                <asp:DropDownList ID="cboBank" runat="server" AutoPostBack="true" CssClass="Dropdown">
                </asp:DropDownList>
            </td>
            <td style="text-align: left;" class="auto-style13">
                <asp:CheckBox ID="chkBranch" Text="Branch"  runat="server" />
            </td>
            <td>
                <asp:DropDownList ID="cboBranch" runat="server" AutoPostBack="true" CssClass="Dropdown">
                </asp:DropDownList>
                </td>
        </tr>

        <tr>
            <td class="auto-style10">
                <asp:RadioButton ID="rbTerm" GroupName="Filter" Text="Term" runat="server" AutoPostBack="True" />
                </td>
            <td class="auto-style8">
                <asp:CheckBox ID="chkAllTerm" runat="server" AutoPostBack="True" Text="Check All" Enabled="False" />
            </td>
            <td class="auto-style3">&nbsp;</td>
            <td style="height: 28px; " colspan="3">
                <asp:CheckBoxList ID="chkTerm"   RepeatColumns="6" RepeatDirection="Horizontal" runat="server" Width="100%" Enabled="False">
                </asp:CheckBoxList>
            </td>
            <td>
                &nbsp;</td>
        </tr>


        <tr>
            <td class="auto-style10">
                <asp:RadioButton ID="rbDate" GroupName="Filter" Text="Date From" AutoPostBack="true" runat="server" Checked="True" />
            </td>
            <td class="auto-style8">
                <asp:TextBox ID="txtFrom" runat="server" CssClass="textbox"></asp:TextBox>
                <asp:CalendarExtender ID="txtFrom_CalendarExtender" runat="server" TargetControlID="txtFrom" Format="dd/MM/yyyy">
                </asp:CalendarExtender>
                <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999" MaskType="Date" TargetControlID="txtFrom" PromptCharacter="_"> </asp:MaskedEditExtender>
            </td>
            <td class="auto-style3">&nbsp;</td>
            <td class="auto-style11">
                <asp:Label ID="lblDateTo" runat="server" Text="To"></asp:Label>
            </td>
            <td class="auto-style12">
                <asp:TextBox ID="txtTo" runat="server" CssClass="textbox"></asp:TextBox>
                <asp:CalendarExtender ID="txtTo_CalendarExtender" runat="server" TargetControlID="txtTo" DefaultView="Days" Format="dd/MM/yyyy">
                </asp:CalendarExtender>
                <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999" MaskType="Date" TargetControlID="txtTo" PromptCharacter="_"> </asp:MaskedEditExtender>
            </td>
            <td align="center" style="text-align: left;" class="auto-style14">
                <asp:Label ID="lblReportType" runat="server" Text="Report Type"></asp:Label>
            </td>
            <td>
               <asp:DropDownList ID="cboReportType" runat="server" CssClass="Dropdown">
                    <asp:ListItem Selected="True" Value="1">Student wise (Fee Collection Register)</asp:ListItem>
                    <asp:ListItem Value="2">Class-wise(Class-wise Collection Summary)</asp:ListItem>
                    <asp:ListItem Value="3">Term-wise (Term-wise Collection Report)</asp:ListItem>
                    <asp:ListItem Value="4">Head-wise (Detailed Head-wise Collection Report)</asp:ListItem>
                   <asp:ListItem Value="5">Head-wise (Summerized Head-wise Collection Report)</asp:ListItem>
                    <asp:ListItem Value="6">Concession Report (Summarized Concession Report)</asp:ListItem>
                    <asp:ListItem Value="7">Head-wise Concession (Detailed Concession Report)</asp:ListItem>
                    <asp:ListItem Value="8">Demand Register</asp:ListItem>
                    <asp:ListItem Value="9">Fee Dues (Dues Report)</asp:ListItem>
                                       <asp:ListItem Value="10">Head-wise Dues (Head-wise Dues Report)</asp:ListItem>
                    <%--<asp:ListItem Value="10">Due Circulars</asp:ListItem>--%>
                    <asp:ListItem Value="11">Class List (Class-wise Fee Group Listing)</asp:ListItem>
                    <asp:ListItem Value="12">Fee Label</asp:ListItem>
                   <asp:ListItem Value="13">Annual Fee Dues</asp:ListItem>
                                    </asp:DropDownList>
                </td>
        </tr>


        <tr>
            <td class="auto-style10">
                <b>
                <asp:CheckBox ID="chkCheckAll" runat="server" AutoPostBack="True" Text="Check All" />
                    <br />
                    <asp:CheckBox ID="chkselectallstudent" runat="server" AutoPostBack="True" Text="All Student" Checked="true" Visible="false"    />
                </b>
            </td>
            <td class="auto-style7" colspan="4">
                 
                &nbsp;
                <asp:Label ID="lblStatus" runat="server" Font-Bold="True" Font-Italic="False" ForeColor="Navy" style="color: #FF0000"></asp:Label>
            </td>
            <td align="center" style="text-align: left;" class="auto-style14">
                &nbsp;</td>
            <td>
                <asp:Button ID="btnReport" runat="server" Text="Generate Report" CssClass="btn btn-primary" />
            </td>
        </tr>


        <tr>
            <td class="auto-style7" colspan="7">
                <div id="gvDiv" style="width: 984px; max-height: 300px; overflow-y: scroll; text-align: center;">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"  CssClass="Grid" DataKeyNames="SID"  ShowFooter="True" Width="99%">
                    <Columns>
                            <%--<asp:BoundField DataField="RegNo" HeaderText="Reg No" SortExpression="RegNo" />--%>
                           <%-- <asp:BoundField DataField="Fine" HeaderText="Fine" SortExpression="Fine" />--%>
                             <%--<asp:BoundField DataField="CollegeNote" HeaderText="CollegeNote" SortExpression="CollegeNote" />--%>
                         <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelect" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                            <asp:BoundField DataField="SID" HeaderText="SID" SortExpression="SID" Visible="False" />
                        <asp:TemplateField HeaderText="S. No.">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1%>
                            </ItemTemplate>
                            <ItemStyle Width="50px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="RegNo" HeaderText="Reg. No" SortExpression="RegNo" />
                        <asp:BoundField DataField="RegNo" HeaderText="Adm. No" SortExpression="RegNo" />
                        <asp:BoundField DataField="SName" HeaderText="Student" SortExpression="SName" />
                        <asp:BoundField DataField="FName" HeaderText="Father" SortExpression="FName" />
                        <asp:BoundField DataField="ClassName" HeaderText="Class" SortExpression="ClassName" />
                        <asp:BoundField DataField="SecName" HeaderText="Section" SortExpression="SecName" />
                        
                        <asp:BoundField DataField="ConfigAmount" HeaderText="Config" SortExpression="ConfigAmount" />
                        <asp:BoundField DataField="DepositAmount" HeaderText="Deposit" SortExpression="DepositAmount" />
                        <asp:BoundField DataField="ConcessionAmount" HeaderText="Concession" SortExpression="ConcessionAmount" />
                        <asp:BoundField DataField="DueAmount" HeaderText="Due" SortExpression="DueAmount" />
<%--                        <asp:BoundField DataField="BusFeeDueAmount" HeaderText="BusFeeDue" SortExpression="BusFeeDueAmount" />--%>
                        <asp:BoundField DataField="MobNo" HeaderText="Mobile" SortExpression="MobNo" />
                       
                    </Columns>
                </asp:GridView>
                    </div>
                
            </td>
        </tr>


        <tr>
            <td class="auto-style7">
                <asp:Label ID="lblFeeMsg" runat="server" Text="Due Message"></asp:Label>
            </td>
            <td class="auto-style7" colspan="4">
                <asp:TextBox ID="txtMessage" runat="server" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" Height="81px" TextMode="MultiLine" Visible="False" Width="510px"></asp:TextBox>
            </td>
            <td class="auto-style7" colspan="2">
               <asp:Button ID="btnSendSMS" runat="server" Text="Send SMS" CssClass="btn btn-primary" />
            &nbsp;
               <asp:Button ID="btnDueCirculars" runat="server" Text="Due Circulars" CssClass="btn btn-primary" />
            &nbsp;<asp:Button ID="btnExcel" runat="server" Text="Export to Excel" CssClass="btn btn-primary" />
                
            </td>
        </tr>


        <tr>
            <td class="auto-style7" colspan="7">
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="92%">
                    <LocalReport ReportEmbeddedResource="rptFeeCollection.rdlc" ReportPath="rptFeeCollection.rdlc">
                        <DataSources>
                            <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="dsFeeCollection" />
                        </DataSources>
                    </LocalReport>
                </rsweb:ReportViewer>
            </td>
        </tr>


        <tr>
            <td class="auto-style7" colspan="7">
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
                <asp:TextBox ID="txtAdmissionFeeID" runat="server" BorderStyle="Solid" BorderWidth="1px" ReadOnly="True" Visible="False" Width="22px"></asp:TextBox>
                <asp:TextBox ID="txtLateFeeID" runat="server" BorderStyle="Solid" BorderWidth="1px" ReadOnly="True" Visible="False" Width="22px"></asp:TextBox>
                <asp:TextBox ID="txtTutionFeeID" runat="server" BorderStyle="Solid" BorderWidth="1px" Height="16px" ReadOnly="True" Visible="False" Width="22px"></asp:TextBox>
                <asp:TextBox ID="txtConveyanceFeeID" runat="server" BorderStyle="Solid" BorderWidth="1px" ReadOnly="True" Visible="False" Width="22px"></asp:TextBox>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT * FROM vw_Student where SID<0"></asp:SqlDataSource>
               <asp:Button ID="btnFeeDues" runat="server" Text="Get Fee Dues" CssClass="btn btn-primary" Visible="False" />
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT [PMName] FROM [PaymentModes] order by DisplayOrder"></asp:SqlDataSource>
            </td>
        </tr>


  
        </table>
            </div>
        </div>
        <div class="clearfix"></div>
    </div>

</asp:Content>

<asp:Content ID="Content3" runat="server" contentplaceholderid="head">
    <style type="text/css">
        .auto-style3 {
            height: 28px;
            width: 53px;
        }
        .auto-style7 {
            height: 28px;
        }
        .auto-style8 {
            height: 28px;
            width: 12%;
        }
        .auto-style10 {
            height: 28px;
            width: 115px;
        }
        .auto-style11 {
            height: 28px;
            width: 127px;
        }
        .auto-style12 {
            height: 28px;
            width: 191px;
        }
        .auto-style13 {
            width: 129px;
        }
        .auto-style14 {
            height: 28px;
            width: 129px;
        }
    </style>
</asp:Content>


