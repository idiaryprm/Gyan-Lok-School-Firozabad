<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="BusFeeDeposit.aspx.vb" Inherits="iDiary_V3.BusFeeDeposit" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Heading" Runat="Server">
    Bus Fee Deposit 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="col_3" style="margin-top: 20px;" id="panelEnquiryNo" runat="server">
        <div class="col-md-3 widget widget1" style="width: 100%">
            <div class="r3_counter_box">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>

    <table class="table">
        <tr>
            <td class="td_width_16" colspan="7">
                <asp:Panel ID="Panel1" runat="server">
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
                                    </asp:Panel>
            </td>
            <td class="auto-style9">
                &nbsp;</td>
        </tr>
        <tr>
           
            <td class="td_width_16">Search By Name </td>
            <td class="auto-style7">
                <asp:TextBox ID="txtSearchName" runat="server" CssClass="textbox"></asp:TextBox>
            </td>
            <td class="td_width_4">
                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-sm btn-primary" Text="&gt;&gt;" />
            </td>
              <td class="td_width_16">
                Admn / Reg No.</td>
            <td class="auto-style7">
                <asp:TextBox ID="txtRegNo" runat="server" CssClass="textbox"></asp:TextBox>
            </td>
            <td class="td_width_4"><asp:Button ID="btnregsearch" runat="server" CssClass="btn btn-sm btn-primary" Text="&gt;&gt;" /></td>
            <td class="td_width_16">Fee Book No </td>
            <td class="auto-style7">
                <asp:TextBox ID="txtFeeBookNo" runat="server" CssClass="textbox"></asp:TextBox>
            </td>
            <td class="td_width_4">
                <asp:Button ID="btnNext" runat="server" CssClass="btn btn-sm btn-primary" Text="&gt;&gt;" />
            </td>
            
        </tr>
        <tr>
            
            <td class="td_width_16">
                Student Name</td>
            <td class="auto-style7">
                <asp:TextBox ID="txtSName" runat="server" ReadOnly="True" CssClass="textbox"></asp:TextBox>
            </td>
            <td class="td_width_16">&nbsp;</td>
            <td class="td_width_16">
                Father Name</td>
            <td class="auto-style9">
                <asp:TextBox ID="txtFName" runat="server" ReadOnly="True" CssClass="textbox"></asp:TextBox>
            </td>
            <td class="td_width_4">
                &nbsp;</td>
             <td class="td_width_16">
                Class</td>
            <td class="auto-style6">
                <asp:TextBox ID="txtClass" runat="server" ReadOnly="True" Width="95px" CssClass="textbox"></asp:TextBox>
                <asp:TextBox ID="txtSec" runat="server" CssClass="textbox" ReadOnly="True" Width="49px"></asp:TextBox>
            </td>
            <td class="td_width_16">&nbsp;</td>
        </tr>
        <tr>
           
            <td class="td_width_16">
                Deposit Date</td>
            <td class="auto-style7">
                <asp:TextBox ID="txtDepositDate" runat="server" CssClass="textbox"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender1dd" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDepositDate">
                      </asp:CalendarExtender>
            </td>
            <td class="td_width_16">&nbsp;</td>
            <td class="td_width_16">
                Term / Installment No</td>
            <td class="auto-style9">

                        <asp:CheckBoxList ID="chkTermList" runat="server"
                                        OnSelectedIndexChanged="CheckBoxList1_SelectedIndexChanged"
                                        AutoPostBack="True" CellPadding="2" CellSpacing="2" RepeatColumns="6" Width="175px">
                                    </asp:CheckBoxList>   

                        
            </td>
            <td class="td_width_4">
                &nbsp;</td>
            <td class="td_width_16">Pick Up Point</td>
            <td class="auto-style6"><asp:Label ID="lblpickuppoint" runat="server" Font-Bold="True" ForeColor="Navy"></asp:Label></td>

            <td class="td_width_16">
                <%--<asp:TextBox ID="txtConfirm" runat="server" CssClass="textbox" width="20px"></asp:TextBox>--%>
            </td>

        </tr>
        <tr>
            <td class="td_width_16">
                Actual Amount</td>
            <td class="auto-style6">
                <asp:Label ID="lblActualAmt" runat="server" Font-Bold="True" ForeColor="Navy"></asp:Label>
            </td>
            <td class="td_width_16">&nbsp;</td>
            <td class="td_width_16">
                Deposite Amount</td>
            <td class="auto-style7">
                <asp:TextBox ID="txtDepositeAmt" runat="server" TextMode="Number" CssClass="textbox"></asp:TextBox>
            </td>
            <td class="td_width_16">&nbsp;</td>
            <td class="td_width_16">
                Depsoit Mode</td>
            <td class="auto-style9">
                <asp:DropDownList ID="cboMode" runat="server" AutoPostBack="True" CssClass="Dropdown">
                </asp:DropDownList>
            </td>
            <td class="td_width_4">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="td_width_16">Concession</td>
            <td class="auto-style6">
                <asp:TextBox ID="txtConcession" runat="server" TextMode="Number" CssClass="textbox"></asp:TextBox>
            </td>
            <td class="td_width_16">&nbsp;</td>
            <td class="td_width_16">Fine</td>
            <td class="auto-style7">
                <asp:TextBox ID="txtBusFine" runat="server" TextMode="Number" CssClass="textbox"></asp:TextBox>
            </td>
            <td class="td_width_16">&nbsp;</td>
            <td class="td_width_16">Cheque No</td>
            <td class="auto-style9">
                <asp:TextBox ID="txtChequeNo" runat="server" CssClass="textbox" Enabled="False"></asp:TextBox>
            </td>
            <td class="td_width_4">&nbsp;</td>
        </tr>
        <tr>
            <td class="td_width_16">Cheque Bank</td>
            <td class="auto-style6">
                <asp:TextBox ID="txtChequeBank" runat="server" CssClass="textbox" Enabled="False"></asp:TextBox>
            </td>
            <td class="td_width_16">&nbsp;</td>
            <td class="td_width_16">Cheque Date</td>
            <td class="auto-style7">
                <asp:TextBox ID="txtChequeDate" runat="server" CssClass="textbox" Enabled="False"></asp:TextBox>
                <asp:CalendarExtender ID="txtDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="txtChequeDate" />
            </td>
            <td class="td_width_16">&nbsp;</td>
            <td class="td_width_16">Remark</td>
            <td class="auto-style9">
                 <asp:TextBox ID="txtModeRemark" runat="server" CssClass="textbox"></asp:TextBox>
            </td>
            <td class="td_width_4">&nbsp;</td>
        </tr>
        <tr>
            <%--<td class="td_width_16">
                Previous History&nbsp;&nbsp; </td>
            <td class="td_width_16">
                <asp:DropDownList ID="cboHistory" runat="server" AutoPostBack="True" Width="130px">
                </asp:DropDownList>
            </td>
            <td class="td_width_16">
                <asp:Button ID="btnCompleteHistory" runat="server" BackColor="#FFCC66" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" Font-Size="X-Small" Height="21px" Text="Complete History" Visible="False" Width="116px" />
            </td>--%>
            <td class="td_width_16">
                <asp:CheckBox ID="chkFeeRcpt" runat="server" Text="Print Fee Receipt" />
            </td>
            <td class="auto-style6">
                &nbsp;</td>
            <td class="td_width_16">&nbsp;</td>
            <td class="td_width_16">
                <asp:CheckBox ID="chkPast" runat="server" AutoPostBack="True" Text="Consider Past Dues" Visible="False" />
            </td>
            <td class="auto-style7">
                &nbsp;</td>
            <td></td>
            <td>
                <asp:CheckBox ID="chkMultipleEntry" runat="server" AutoPostBack="True" Text="Multiple Entry" Visible="true" />
            </td>
        </tr>
        <tr>
            <td class="td_width_16" colspan="5">
                <asp:Label ID="lblStatus" runat="server" Font-Bold="True" ForeColor="Navy"></asp:Label>
            </td>
            <td class="td_width_16">
                &nbsp;</td>
            <td class="td_width_16">
                &nbsp;</td>
            <td class="auto-style9">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="td_width_16" colspan="4" valign="top">
                <asp:Button ID="btnSave" runat="server" Text="  Save  " CssClass="btn btn-sm btn-primary" />
            &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnRemove" runat="server" Text="Remove" CssClass="btn btn-sm btn-primary" Visible="False" />
                &nbsp;&nbsp;
                <asp:Button ID="btnSlip" runat="server" CssClass="btn btn-sm btn-primary" Text="Generate Slip" Visible="False" />
            </td>
            <td class="auto-style7">
                <asp:TextBox ID="txtSID" runat="server" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" Enabled="False" Visible="False" Width="126px"></asp:TextBox>
            </td>
            <td class="td_width_16">&nbsp;</td>
            <td class="td_width_16">&nbsp;</td>
            <td class="auto-style9">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style2" colspan="4" valign="top">Deposit Amount</td>
            <td class="auto-style2" colspan="3">Configured Amount</td>
            <td class="auto-style9">&nbsp;</td>
        </tr>
        <tr>
            <td class="td_width_16" colspan="4" style="text-align:center">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" AutoGenerateSelectButton="True" CssClass="Grid" DataKeyNames="BusFeeID" DataSourceID="SqlDataSource2" Width="98%">
                    <Columns>
                        <asp:BoundField DataField="DepositeDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Date" HtmlEncode="False" SortExpression="DepositeDate">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="TermNo" HeaderText="Term">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="DepositeAmt" HeaderText="Amount" SortExpression="DepositeAmt">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Concession" HeaderText="Concession" SortExpression="ConcessionAmount">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="FineAmt" HeaderText="FineAmt" SortExpression="Fine">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        
                    </Columns>
                    <HeaderStyle HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#4DE427" />
                </asp:GridView>
            </td>
            <td class="td_width_16" colspan="3">
                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CssClass="Grid" DataKeyNames="BusTermID" DataSourceID="SqlDataSource3" Width="98%">
                    <Columns>
                        <asp:BoundField DataField="BusTermID" HeaderText="TermID" Visible="False">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="BusTermNo" HeaderText="TermNo" SortExpression="BusTermNo">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataFormatString="{0:dd/MM/yyyy}" HeaderText="Due Date">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Amount">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                    </Columns>
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:GridView>
            </td>
            <td class="auto-style9">&nbsp;</td>
        </tr>
        <tr>
            <td class="td_width_16" colspan="7" >
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="10pt" Width="98%" Visible="true" ZoomPercent="90">
                                                </rsweb:ReportViewer>
            </td>
        </tr>
        <tr>
            <td class="td_width_16" colspan="7">
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT RegNo, FeeBookNo, SName, ClassName, SecName, FName, MName, ASID FROM vw_Student WHERE SID=0">
                   
                </asp:SqlDataSource>
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT BusFeeID,DepositeDate,DepositeAmt,FineAmt,Concession,BusTermNo from vw_BusFee where SID=0"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT BusTermID,BusTermNo,BusTermName FROM BusTermMaster where BusTermID=0"></asp:SqlDataSource>
                <asp:TextBox ID="txtFeeDepositID" runat="server" BorderStyle="Solid" BorderWidth="1px" ReadOnly="True" Visible="False" Width="22px"></asp:TextBox>
               
                <asp:DropDownList ID="cboTermID" runat="server" Visible="False">
                </asp:DropDownList>
               
            </td>
            <td class="auto-style9">
                &nbsp;</td>
        </tr>
    </table>
    </ContentTemplate>
</asp:UpdatePanel>
     </div>
        </div>
        <div class="clearfix"></div>
    </div>  
  
</asp:Content>

<asp:Content ID="Content3" runat="server" contentplaceholderid="head">
    <style type="text/css">
        .auto-style2 {
            font-weight: bold;
            text-align: center;
        }
        .auto-style6 {
            width: 43px;
        }
        .auto-style7 {
            width: 158px;
        }
        .auto-style8 {
            width: 77px;
        }
        .auto-style9 {
            width: 30px;
        }
    </style>
</asp:Content>


