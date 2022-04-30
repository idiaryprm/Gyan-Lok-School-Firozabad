<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="PettyCashTransactions.aspx.vb" Inherits="iDiary_V3.PettyCashTransactions" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Heading" runat="server">

    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
&nbsp;  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <%--<br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />vv
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
         <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />  
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /> --%>
    <div class="col_3" style="margin-top: 20px;" id="panelEnquiryNo" runat="server">
        <div class="col-md-3 widget widget1" style="width: 100%">
            <div class="r3_counter_box">

                <table class="table">
        <tr>
            <td colspan="9">
                                        <asp:GridView ID="gvSearch" runat="server"
                                            AutoGenerateColumns="False" AutoGenerateSelectButton="True"
                                            DataSourceID="SqlDataSource1" CssClass="Grid" Width="98%">

                                            <Columns>
                                                <asp:BoundField DataField="RegNo" HeaderText="Reg No" SortExpression="RegNo" />
                                                <asp:BoundField DataField="FeeBookNo" HeaderText="Fee Book No."
                                                    SortExpression="FeeBookNo" />
                                                <asp:BoundField DataField="SName" HeaderText="Student Name"
                                                    SortExpression="SName" />
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
                                    </td>
        </tr>

        <tr>
            <td>Fee Book No</td>
            <td class="auto-style12">
                <asp:TextBox ID="txtFeeBookNo" runat="server" CssClass="textbox"></asp:TextBox>
            </td>
            
            <td class="auto-style18">
                                    <asp:Button ID="btnFeeBookNext" runat="server" CssClass="btn btn-sm btn-primary" Text="&gt;&gt;" />
                                </td>
            
            <td class="auto-style5">
                Reg No</td>
            
            <td class="auto-style14">
                <asp:TextBox ID="txtRegNO" runat="server" CssClass="textbox"></asp:TextBox>
            </td>
            
            <td class="auto-style4">
                                    <asp:Button ID="btnNextRegNo" runat="server" CssClass="btn btn-sm btn-primary" Text="&gt;&gt;" />
                                </td>
            
            <td class="auto-style11">
                Student Name</td>
            
            <td class="auto-style16">
                <asp:TextBox ID="txtSName" runat="server" CssClass="textbox"></asp:TextBox>
            </td>
            
            <td class="auto-style4">
                                    <asp:Button ID="btnSNameNext" runat="server" CssClass="btn btn-sm btn-primary" Text="&gt;&gt;" />
            </td>
        </tr>

        <tr>
            <td class="auto-style2">Class/Sec</td>
            <td class="auto-style12">
                                    <asp:TextBox ID="txtClass" runat="server" CssClass="textbox" ReadOnly="true"></asp:TextBox>
                                    </td>
            
            <td class="auto-style18">
                &nbsp;</td>
            
            <td class="auto-style5">
                Father</td>
            
            <td class="auto-style14">
                                    <asp:TextBox ID="txtFName" runat="server" CssClass="textbox" ReadOnly="true"></asp:TextBox>
                                </td>
            
            <td class="auto-style4">
                &nbsp;</td>
            
            <td class="auto-style3">
                Vr_ No<asp:Label ID="lblVr_No" runat="server" Visible="False"></asp:Label>
            </td>
            
            <td class="auto-style16">
                <asp:TextBox ID="txtVRNo" runat="server" CssClass="textbox"></asp:TextBox>
            </td>
            
            <td class="auto-style4">
                                    <asp:Button ID="btnNext" runat="server" CssClass="btn btn-sm btn-primary" Text="&gt;&gt;" />
                                </td>
        </tr>

        <tr>
            <td class="auto-style2">
                Petty Cash Head</td>
            <td class="auto-style12">
                <asp:DropDownList ID="cboPettyCashHead" runat="server" CssClass="Dropdown" AutoPostBack="True">
                                </asp:DropDownList></td>
           
            <td class="auto-style18">
                &nbsp;</td>
           
            <td class="auto-style5">
                Deposit Date</td>
           
            <td class="auto-style14"  >
                <asp:TextBox ID="txtVRDT" runat="server" AutoPostBack="True" CssClass="textbox"></asp:TextBox>
                <asp:CalendarExtender ID="txtVRDT_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="txtVRDT">
                </asp:CalendarExtender>
            </td>
           
            <td class="auto-style4"  >
                &nbsp;</td>
           
            <td class="auto-style3"  >
                Amount</td>
           
            <td class="auto-style16"  >
                <asp:TextBox ID="txtAmount" runat="server" CssClass="textbox" TextMode="Number"></asp:TextBox>
            </td>
           
            <td class="auto-style4"  >
                &nbsp;</td>
        </tr>

        <tr>
            <td class="auto-style2">Remarks</td>
            <td class="auto-style1" colspan="5">
                                    <asp:TextBox ID="txtRemarks" runat="server" CssClass="textbox" Height="50px" TextMode="MultiLine" Width="499px"></asp:TextBox>
                                    </td>
            <td class="auto-style3" colspan="3">
                <asp:Label ID="lblSchoolName" runat="server" Font-Bold="True" ForeColor="Navy" style="color: #000000"></asp:Label>
                <br />
                <asp:CheckBox ID="chkFeeRcpt" runat="server" Checked="True" Text="Print Fee Receipt" />
                <br />
                <asp:Label ID="lblStatus" runat="server" Font-Bold="True" ForeColor="Navy" style="color: #FF0000"></asp:Label>
            </td>
        </tr>

      

        <tr>
            <td colspan="4">
                <asp:Button ID="btnSave" runat="server" Width="70px" CssClass="btn btn-sm btn-primary" Text="Save" />
                &nbsp;<asp:Button ID="btnDelete" runat="server" CssClass="btn btn-sm btn-primary" Text="Delete" Width="70px" Visible="False" />
                &nbsp;<asp:Button ID="btnNew" runat="server" CssClass="btn btn-sm btn-primary" Text="New" Width="70px" />
            &nbsp;<asp:Button ID="btnSlip" runat="server" CssClass="btn btn-sm btn-primary" Text="Generate Slip" Width="100px" Visible="False" />
                &nbsp;<asp:Button ID="btnprint" runat="server" CssClass="btn btn-sm btn-primary" OnClientClick="myFunction()" Text="Print" Width="70px" Visible="false" />
            
            </td>
            <td colspan="5" rowspan="2">
                                                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" AutoGenerateSelectButton="True" CssClass="Grid" DataKeyNames="TransID" DataSourceID="SqlDataSource2" Width="521px">
                                                                <Columns>
                                                                    <asp:BoundField DataField="SchoolName" HeaderText="School Account" SortExpression="SchoolName">
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Vr_No" HeaderText="Vr_No" SortExpression="Vr_No">
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Vr_Dt" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Date" HtmlEncode="False" SortExpression="DepositDate">
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="PCHeadName" HeaderText="Head" SortExpression="PCHeadName">
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="TransAmount" HeaderText="Amount" SortExpression="DepositAmount">
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    </asp:BoundField>
                                                                    
                                                                </Columns>
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <SelectedRowStyle BackColor="#4DE427" />
                                                            </asp:GridView>
                                                        </td>
        </tr>

      

        <tr>
            <td class="auto-style2">
                <asp:TextBox ID="txtSID" runat="server" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px" Width="39px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="txtIDOther" runat="server" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px" Width="39px" Visible="False"></asp:TextBox>
                </td>
            <td class="auto-style12">
                <asp:TextBox ID="txtID" runat="server" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px" Width="39px" Visible="False"></asp:TextBox>
                </td>
            <td class="auto-style18">
                <asp:TextBox ID="txtClassGroupID" runat="server" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px" Width="39px" Visible="False"></asp:TextBox>
                </td>
            <td class="auto-style5">
                &nbsp;</td>
        </tr>

        <tr>
            <%--<td colspan="5">
                <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" Width="100%" AutoGenerateColumns="False" DataKeyNames="TransID" DataSourceID="SqlDataSource1">
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" />
                        <asp:BoundField DataField="TransID" HeaderText="ID" ReadOnly="True" SortExpression="TransID">
                        <ItemStyle Width="50px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="TransDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Date" HtmlEncode="False" ReadOnly="True" SortExpression="TransDate">
                        <ItemStyle Width="80px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="TransTypeName" HeaderText="Transaction Type" ReadOnly="True" SortExpression="TransTypeName" />
                        <asp:BoundField DataField="PCHeadName" HeaderText="Head Name" ReadOnly="True" SortExpression="PCHeadName" />
                        <asp:BoundField DataField="NameOf" HeaderText="In Favour of" SortExpression="NameOf" />
                        <asp:BoundField DataField="AccountDetails" HeaderText="Account Details" SortExpression="AccountDetails" />
                        <asp:BoundField DataField="TransAmount" HeaderText="Amount" SortExpression="TransAmount" />
                        <asp:BoundField DataField="TransRemark" HeaderText="Remark" SortExpression="TransRemark" />
                    </Columns>
                    <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                    <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                    <RowStyle BackColor="White" ForeColor="#330099" />
                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                    <SortedAscendingCellStyle BackColor="#FEFCEB" />
                    <SortedAscendingHeaderStyle BackColor="#AF0101" />
                    <SortedDescendingCellStyle BackColor="#F6F0C0" />
                    <SortedDescendingHeaderStyle BackColor="#7E0000" />
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT [TransID], [TransDate], [TransTypeName], [PCHeadName], [NameOf],[TransAmount],[AccountDetails], [TransRemark] FROM [vwPettyCashTransaction] Where TransTypeName=@TransName AND PCHeadName=@HeadName">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="cboTrans" Name="TransName" PropertyName="text" />
                        <asp:ControlParameter ControlID="cboHead" Name="HeadName" PropertyName="Text" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>--%>
        </tr>

        <tr>
            <td colspan="9">
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="272px" Width="100%" Font-Names="Verdana" Font-Size="9pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" CssClass="aspNetDisabled" Visible="False">
                    <LocalReport ReportEmbeddedResource="iDiary_V3.rptPettyCash.rdlc" ReportPath="rptPettyCash.rdlc">
                    </LocalReport>
                </rsweb:ReportViewer>
                                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT * from vwPettyCashTransaction where SID=0"></asp:SqlDataSource>
                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT RegNo, FeeBookNo, SName, ClassName, SecName, FName, MName, ASID FROM vw_Student WHERE SID&lt;0"></asp:SqlDataSource>
            </td>
        </tr>
<%--<tr>
    <td><button onclick="myFunction()">Click me</button></td>
    
</tr>
     --%>   </table>
                </div>
        </div>
        <div class="clearfix"></div>
    </div>
</asp:Content>





<asp:Content ID="Content3" runat="server" contentplaceholderid="head">
    <style type="text/css">
        .auto-style1 {
        }
        .auto-style2 {
            width: 12%;
        }
        .auto-style3 {
        }
        .auto-style4 {
            width: 51px;
        }
        .auto-style5 {
            width: 127px;
        }
        .auto-style11 {
            width: 133px;
            }
        .auto-style12 {
            width: 13%;
        }
        .auto-style14 {
            width: 99px;
        }
        .auto-style16 {
            width: 125px;
        }
        .auto-style18 {
            width: 73px;
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






