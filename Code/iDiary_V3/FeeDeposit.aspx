<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="FeeDeposit.aspx.vb" Inherits="iDiary_V3.FeeDeposit" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Heading" Runat="Server">
  
  
    <%--<script language="javascript" type="text/javascript">


function ShowTotal() {
    debugger;
    var total = document.getElementById('<%=myTable.ClientID%>');
    var x = 0;
    var ar = new Array();
    var textBox = new Array(); // to store the textbox objects
    var oInputs = new Array();

    oInputs = total.getElementsByTagName('input') // store collection of all <input/> elements
    for (i = 0; i < oInputs.length; i++) { // loop through and find <input type="text"/>
        if (oInputs[i].type == 'text') {
            textBox.push(oInputs[i]); // found one - store it in the oTextBoxes array
        }
        //alert(i);
    }


    for (i = 0; i < textBox.length; i++) { // Loop through the stored and output the value
        if (textBox[i].value != "") {
            x = x + parseInt(textBox[i].value);
        }
    }
        //alert(x);
    
    document.getElementById('txtTotal').value = x;
    document.getElementById('test').value = x;
}

</script>--%>
    Fee Deposit Section
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <%--   <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />--%>

     <div class="col_3" style="margin-top: 20px;" id="panelEnquiryNo" runat="server">
        <div class="col-md-3 widget widget1" style="width: 100%">
            <div class="r3_counter_box">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>

                        <table class="table" style="width:1000px">
                            <tr>
                                <td colspan="9" valign="top">
                                    <asp:Panel ID="Panel1" runat="server">
                                        <asp:GridView ID="gvSearch" runat="server" AutoGenerateColumns="False" AutoGenerateSelectButton="True" DataSourceID="SqlDataSource1" CssClass="Grid" Width="98%">
                                            <Columns>
                                                <asp:BoundField DataField="RegNo" HeaderText="Reg No" SortExpression="RegNo" />
                                                <asp:BoundField DataField="FeeBookNo" HeaderText="Fee Book No."
                                                    SortExpression="FeeBookNo" />
                                                <asp:BoundField DataField="SName" HeaderText="Student Name"
                                                    SortExpression="SName" />
                                                <asp:BoundField DataField="SchoolName" HeaderText="School"
                                                    SortExpression="SchoolName" />
                                                <asp:BoundField DataField="ClassName" HeaderText="Class"
                                                    SortExpression="ClassName" />
                                                <asp:BoundField DataField="SecName" HeaderText="Section"
                                                    SortExpression="SecName" />
                                                <asp:BoundField DataField="FName" HeaderText="Father Name"
                                                    SortExpression="FName" />
                                                <asp:BoundField DataField="MName" HeaderText="Mother Name"
                                                    SortExpression="MName" />
                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style1">Fee Book No</td>
                                <td style="text-align:left" class="auto-style2">
                                    <asp:TextBox ID="txtFeeBookNo" runat="server" CssClass="textbox"></asp:TextBox>
                                 </td>
                                <td class="auto-style15" style="text-align:left">
                                    <asp:Button ID="btnNext" runat="server" CssClass="btn btn-sm btn-primary" Text="&gt;&gt;" />
                                </td>
                                <td class="auto-style28">Student Name</td>
                                <td class="auto-style27">
                                    <asp:TextBox ID="txtSName" runat="server" CssClass="textbox"></asp:TextBox>
                                    </td>
                                <td>
                                    <asp:Button ID="btnNextName" runat="server" CssClass="btn btn-sm btn-primary" Text="&gt;&gt;" />
                                </td>
                                <td class="auto-style32">Adm. No.</td>
                                <td>
                                    <asp:TextBox ID="txtRegNo" runat="server" CssClass="textbox"></asp:TextBox>
                                    </td>
                               
                                <td>
                                    <asp:Button ID="btnNextRegNo" runat="server" CssClass="btn btn-sm btn-primary" Text="&gt;&gt;" />
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style1">Father Name</td>
                                <td class="auto-style2">
                                    <asp:TextBox ID="txtFName" runat="server" CssClass="textbox" ReadOnly="true"></asp:TextBox>
                                </td>
                                <td class="auto-style15">&nbsp;</td>
                                <td class="auto-style28">School</td>
                                <td class="auto-style27">
                                    <asp:TextBox ID="txtSchooName" runat="server" CssClass="textbox" ReadOnly="true"></asp:TextBox>
                                </td>
                                <td>&nbsp;</td>
                                <td class="auto-style32">Class</td>
                                <td>
                                    <asp:TextBox ID="txtClass" runat="server" CssClass="textbox" ReadOnly="true" Width="90px"></asp:TextBox>
                                    &nbsp;/&nbsp;<asp:TextBox ID="txtSec" runat="server" CssClass="textbox" ReadOnly="True" Width="57px"></asp:TextBox>
                                </td>
                                
                                <td>&nbsp;</td>
                                
                            </tr>
                            <tr>
                                <td class="auto-style1">School Bank</td>
                                <td class="auto-style2">
                                    <asp:DropDownList ID="cboBank" runat="server" AutoPostBack="true" CssClass="Dropdown">
                                    </asp:DropDownList>
                                </td>
                                <td class="auto-style15">&nbsp;</td>
                                <td class="auto-style28">Branch</td>
                                <td class="auto-style27">
                                    <asp:DropDownList ID="cboBranch" runat="server" AutoPostBack="true" CssClass="Dropdown">
                                    </asp:DropDownList>
                                </td>
                                <td>&nbsp;</td>
                                <td class="auto-style32">Deposit Date</td>
                                <td>
                                    <asp:TextBox ID="txtDepositDate" runat="server" CssClass="textbox"></asp:TextBox>
                                    <asp:CalendarExtender ID="txtDepositDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDepositDate" />
                                    <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999" MaskType="Date" TargetControlID="txtDepositDate" PromptCharacter="_"> </asp:MaskedEditExtender>
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="auto-style1">Deposit Mode</td>
                                <td class="auto-style2">
                                    <asp:DropDownList ID="cboMode" runat="server" AutoPostBack="True" CssClass="Dropdown">
                                    </asp:DropDownList>
                                </td>
                                <td class="auto-style15">&nbsp;</td>
                                <td class="auto-style28">Chq/DD No</td>
                                <td class="auto-style27">
                                    <asp:TextBox ID="txtChequeNo" runat="server" CssClass="textbox"></asp:TextBox>
                                </td>
                                <td>&nbsp;</td>
                            
                                <td class="auto-style32">Chq/DD Date</td>
                                <td>
                                    <asp:TextBox ID="txtChequeDate" runat="server" CssClass="textbox"></asp:TextBox>
                                    <asp:CalendarExtender ID="txtDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="txtChequeDate" />
                                </td>
                                <td>&nbsp;</td>
                            
                            </tr>
                            <tr>
                                <td class="auto-style1">Chq/DD Bank</td>
                                <td class="auto-style2">
                                    <asp:TextBox ID="txtChequeBank" runat="server" CssClass="textbox"></asp:TextBox>
                                </td>
                                <td class="auto-style15">
                                    <asp:CheckBox ID="chkAllTerm" runat="server" AutoPostBack="True" Text="Check All" />
                                </td>
                                <td class="auto-style28">Installment</td>
                                <td class="auto-style19" colspan="4">
                                    <asp:CheckBoxList ID="chkTermList" runat="server" AutoPostBack="True" CellPadding="1" CellSpacing="1" OnSelectedIndexChanged="CheckBoxList1_SelectedIndexChanged" RepeatColumns="6" Width="487px">
                                    </asp:CheckBoxList>
                                </td>
                                <td>&nbsp;</td>
                            
                            </tr>
                            <tr>
                                <td class="auto-style1">Remark</td>
                                <td class="auto-style11" colspan="3">
                                    <asp:TextBox ID="txtModeRemark" runat="server" CssClass="textbox" Width="300px"></asp:TextBox>
                                </td>
                                <td class="auto-style11">
                                    <asp:CheckBox ID="chkMultipleEntry" runat="server" Text="Multiple Entry" />
                                </td>
                                <td>&nbsp;</td>
                                <td class="auto-style26" colspan="2">
                                    <asp:Label ID="lblFeeGroupName" runat="server" Style="font-weight: 700; color: #000000"></asp:Label>
                                </td>
                                <td>&nbsp;</td>
                                
                            </tr>
                            <tr>
                                <td class="auto-style17" colspan="4">
                                    <asp:Label ID="lblStatus" runat="server" Font-Bold="True" ForeColor="Navy" Style="color: #FF0000" Text=""></asp:Label>
                                </td>
                                <td class="auto-style11">&nbsp;</td>
                                <td>&nbsp;</td>
                                <td class="auto-style26" colspan="2">&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="auto-style17" colspan="9">
                                    <table>
                                        <tr>
                                            <td rowspan="4" style="vertical-align:top">
                                                <div style="min-width:500px">
                                                <asp:GridView ID="GvMyTable" runat="server" AutoGenerateColumns="False" CssClass="Grid" DataKeyNames="FeeTypeID" DataSourceID="SqlDataSourceGvMyTable" GridLines="Horizontal" ShowFooter="True" Visible="False" Width="540px">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="true" OnCheckedChanged="chkSelect_CheckedChanged" width="15px" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="FeeTypeName" HeaderStyle-HorizontalAlign="Center" HeaderText="Fee Type" HtmlEncode="False" SortExpression="FeeTypeName">
                                                        <ItemStyle HorizontalAlign="left" Width="35%" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Actual">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblActualAmount" runat="server" Text="" Width="100px" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                     <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Deposited">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtDepositAmount" runat="server" AutoPostBack="true" CssClass="textbox" Enabled="false" OnTextChanged="txtDepositAmount_TextChanged" RowIndex='<%# Container.DisplayIndex %>' Text="0" TextMode="Number" Width="120px" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Concession">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtActualConcession" runat="server" AutoPostBack="true" CssClass="textbox" Enabled="false" OnTextChanged="txtActualConcession_TextChanged" RowIndex='<%# Container.DisplayIndex %>' Text="0" TextMode="Number" Width="120px" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <SelectedRowStyle BackColor="#4DE427" />
                                                    <FooterStyle BackColor="#ccff99" Font-Bold="True" />
                                                </asp:GridView>
</div>
                                            </td>
                                            
                                            <td rowspan="4" style="vertical-align:top">
                                                <table>
                                                    <tr>
                                                        <td><b>
                                                            <asp:Label ID="lblDepositAmount" runat="server" Text="Deposit Amount" Visible="False"></asp:Label>
                                                            </b></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" AutoGenerateSelectButton="True" CssClass="Grid" DataKeyNames="FeeDepositID" DataSourceID="SqlDataSource2" Width="469px">
                                                                <Columns>
                                                                    <asp:BoundField DataField="DepositDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Date" HtmlEncode="False" SortExpression="DepositDate">
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField HeaderText="Term">
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="DepositAmount" HeaderText="Amount" SortExpression="DepositAmount">
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="ConcessionAmount" HeaderText="Concession" SortExpression="ConcessionAmount">
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    </asp:BoundField>
                                                                </Columns>
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <SelectedRowStyle BackColor="#4DE427" />
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td><b>
                                                            <asp:Label ID="lblConfiguredAmount" runat="server" style="text-align: left" Text="Configured Amount" Visible="False"></asp:Label>
                                                            </b></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CssClass="Grid" DataKeyNames="TermID" DataSourceID="SqlDataSource3" ShowFooter="True" Width="435px">
                                                                <Columns>
                                                                    <asp:BoundField DataField="TermID" HeaderText="TermID" Visible="False">
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="TermNo" HeaderText="TermNo" SortExpression="TermNo">
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataFormatString="{0:dd/MM/yyyy}" HeaderText="DueDate">
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField HeaderText="Config">
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    </asp:BoundField>
                                                                    <%-- <asp:BoundField HeaderText="Deposit Date">
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:BoundField>--%>
                                                                    <asp:BoundField HeaderText="Fine">
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    </asp:BoundField>
                                                                                                                                        <asp:BoundField HeaderText="Deposit">
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField HeaderText="Concession">
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField HeaderText="Excess">
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField HeaderText="Due">
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    </asp:BoundField>
                                                                </Columns>
                                                                <FooterStyle BackColor="#ccff99" Font-Bold="True" HorizontalAlign="Center" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style3">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style3">&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style3">
                                                &nbsp;</td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top" colspan="9">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td>&nbsp;<asp:CheckBox ID="chkFeeRcpt" runat="server" Checked="True" Text="Print Fee Receipt" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-sm btn-primary" Width="75px" Text="Save" />
                                                &nbsp;<asp:Button ID="btnRemove" runat="server" CssClass="btn btn-sm btn-primary" Width="75px" Text="Remove" />
                                                &nbsp;<asp:Button ID="btnSlip" runat="server" CssClass="btn btn-sm btn-primary" Text="Generate Slip" />
                                                &nbsp;<asp:Button ID="btnCompleteHistory" runat="server" CssClass="btn btn-sm btn-primary" Text="History" Visible="False" />
                                                &nbsp;<asp:Button ID="btnprint" runat="server" CssClass="btn btn-sm btn-primary" OnClientClick="myFunction()" Text="Print" Visible="false" Width="70px" />
                                                <asp:Label ID="lblFeeDue" runat="server" Style="font-weight: 700; color: #000099"></asp:Label>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="10pt" Width="92%" Visible="true" ZoomPercent="90">
                                                </rsweb:ReportViewer>
                                            </td>
                                        </tr>
                                    </table>

                                </td>
                              
                            </tr>
                            <tr>
                                <td colspan="4" valign="top">&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                    <asp:Label ID="lblFeeDue0" runat="server"></asp:Label>
                                    <asp:TextBox ID="txtSID" runat="server" BorderStyle="Solid" BorderWidth="1px" ReadOnly="True" Visible="False" Width="22px"></asp:TextBox>
                                    <asp:TextBox ID="txtAdmissionDate" runat="server" BorderStyle="Solid" BorderWidth="1px" ReadOnly="True" Visible="False" Width="22px"></asp:TextBox>
                                    <asp:TextBox ID="txtAdmissionFeeID" runat="server" BorderStyle="Solid" BorderWidth="1px" ReadOnly="True" Visible="False" Width="22px"></asp:TextBox>
                                    <asp:TextBox ID="txtLateFeeID" runat="server" BorderStyle="Solid" BorderWidth="1px" ReadOnly="True" Visible="False" Width="22px"></asp:TextBox>
                                    <asp:TextBox ID="txtArrearFeeID" runat="server" BorderStyle="Solid" BorderWidth="1px" ReadOnly="True" Visible="False" Width="22px"></asp:TextBox>
                                    <asp:TextBox ID="txtExcessFeeID" runat="server" BorderStyle="Solid" BorderWidth="1px" ReadOnly="True" Visible="False" Width="22px"></asp:TextBox>
                                    <asp:TextBox ID="txtTutionFeeID" runat="server" BorderStyle="Solid" BorderWidth="1px" Height="16px" ReadOnly="True" Visible="False" Width="22px"></asp:TextBox>
                                    <asp:TextBox ID="txtConveyanceFeeID" runat="server" BorderStyle="Solid" BorderWidth="1px" ReadOnly="True" Visible="False" Width="22px"></asp:TextBox>
                                    <asp:TextBox ID="txtClassGroup" runat="server" BorderStyle="Solid" BorderWidth="1px" ReadOnly="True" Visible="False" Width="22px"></asp:TextBox>
                                    <br />
                                    <asp:SqlDataSource ID="SqlDataSourceGvMyTable" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="Select FeeTypeID, FeeTypeName From FeeTypes order by FeeOrder"></asp:SqlDataSource>
                                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT [FeeDepositID],[DepositDate], Sum([FeeDepositAmount]) as DepositAmount,Sum([ConcessionAmount]) as ConcessionAmount from vw_FeeDeposit where SID&lt;0 group by [FeeDepositID],[DepositDate] order by [DepositDate] DESC"></asp:SqlDataSource>
                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT RegNo, FeeBookNo, SName, ClassName, SecName, FName, MName, ASID, SchoolName FROM vw_Student WHERE SID=0"></asp:SqlDataSource>
                                    <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT TermID,[TermNo],TermName FROM TermMaster where FeeGroupID=0"></asp:SqlDataSource>
                                </td>
                                <td class="auto-style27">
                                    <asp:TextBox ID="txtFeeDepositID" runat="server" BorderStyle="Solid" BorderWidth="1px" ReadOnly="True" Visible="False" Width="22px"></asp:TextBox>
                                    <asp:TextBox ID="txtFeeGroupID" runat="server" BorderStyle="Solid" BorderWidth="1px" ReadOnly="True" Visible="False" Width="22px"></asp:TextBox>
                                    <asp:CheckBox ID="chkPast" runat="server" AutoPostBack="True" Text="Consider Past Dues" Visible="False" />
                                    <br />
                                    <asp:TextBox ID="txtAdminFeeApplicable" runat="server" Visible="False" Width="37px"></asp:TextBox>
                                    <asp:TextBox ID="txtConfigType" runat="server" Visible="False" Width="37px"></asp:TextBox>
                                    <asp:DropDownList ID="cboTermID" runat="server" Visible="False">
                                    </asp:DropDownList>
                                    <br />
                                    <asp:TextBox ID="txtChallanNo" runat="server" CssClass="textbox" Enabled="False" Visible="false"></asp:TextBox>
                                    <asp:Button ID="btnNextChallan" runat="server" CssClass="btn btn-sm btn-primary" Enabled="False" Text="&gt;&gt;" Visible="false" />
                                </td>
                                <td>&nbsp;</td>
                                <td class="auto-style32">&nbsp;</td>
                                <td>
                                    &nbsp;</td>
                               
                                <td>&nbsp;</td>
                               
                            </tr>
                        </table>
                    </ContentTemplate>
             <%--        <Triggers>

                         <asp:AsyncPostBackTrigger ControlID="btnExcel" />
 </Triggers>--%>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="clearfix"></div>
    </div>  
</asp:Content>

<asp:Content ID="Content3" runat="server" contentplaceholderid="head">
 
    <style type="text/css">
        .auto-style1 {
            width: 109px;
        }
        .auto-style2 {
            width: 135px;
        }
        .auto-style3 {
            width: 440px;
        }
    </style>
 <script type="text/javascript">
     function printReport(report_ID) {
         var rv1 = $('#' + report_ID);
         var iDoc = rv1.parents('html');

         // Reading the report styles
         var styles = iDoc.find("head style[id$='ReportControl_styles']").html();
         if ((styles == undefined) || (styles == '')) {
             iDoc.find('head script').each(function () {
                 var cnt = $(this).html();
                 var p1 = cnt.indexOf('ReportStyles":"');
                 if (p1 > 0) {
                     p1 += 15;
                     var p2 = cnt.indexOf('"', p1);
                     styles = cnt.substr(p1, p2 - p1);
                 }
             });
         }
         if (styles == '') { alert("Cannot generate styles, Displaying without styles.."); }
         styles = '<style type="text/css">' + styles + "</style>";

         // Reading the report html
         var table = rv1.find("div[id$='_oReportDiv']");
         if (table == undefined) {
             alert("Report source not found.");
             return;
         }

         // Generating a copy of the report in a new window
         var docType = '<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01//EN" "http://www.w3.org/TR/html4/loose.dtd">';
         var docCnt = styles + table.parent().html();
         var docHead = '<head><title></title><style>body{margin:0;padding:0;font-size:10px;}</style></head>';
         var winAttr = "location=yes, statusbar=no, directories=no, menubar=no, titlebar=no, toolbar=no, dependent=no, width=720, height=600, resizable=yes, screenX=200, screenY=200, personalbar=no, scrollbars=yes";;
         var newWin = window.open("", "_blank", winAttr);
         writeDoc = newWin.document;
         writeDoc.open();
         writeDoc.write('<html>' + docHead + '<body  onload="window.print();">' + docCnt + '</body></html>');
         writeDoc.close();

         // The print event will fire as soon as the window loads
         newWin.focus();
         // uncomment to autoclose the preview window when printing is confirmed or canceled.
         // newWin.close();
     };
     function myFunction() {

         //alert('1');
         printReport('ctl00_ContentPlaceHolder1_ReportViewer1_ReportViewer');
     }
    </script>
</asp:Content>











