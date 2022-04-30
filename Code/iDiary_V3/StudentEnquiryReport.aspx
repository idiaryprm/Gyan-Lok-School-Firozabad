<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" EnableEventValidation="false" CodeBehind="StudentEnquiryReport.aspx.vb" Inherits="iDiary_V3.StudentEnquiryReport" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Heading" runat="server">
    Admission Report 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
--%>
    <div class="col_3" style="margin-top: 20px;" id="panelEnquiryNo" runat="server">
        <div class="col-md-3 widget widget1" style="width: 100%">
            <div class="r3_counter_box">
                  <table class="table">
        <tr>
            <td style="width: 23%">
                <asp:RadioButton ID="optMonthwise" runat="server" Text="Month-wise" 
                    AutoPostBack="True" GroupName="G1" />
            </td>
            
            <td style="width: 25%">
                <asp:DropDownList ID="cboMonth" runat="server" CssClass="Dropdown">
                </asp:DropDownList>
            </td>
            <td class="auto-style3">
                Year
            </td>
            <td width="20%">
                <asp:TextBox ID="txtYear" runat="server" CssClass="textbox"></asp:TextBox>
            </td>
            <td width="20%">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 23%">
                <asp:RadioButton ID="optDaywise" runat="server" Text="Day-wise Details" 
                    AutoPostBack="True" GroupName="G1" />
            </td>
         
            <td style="width: 25%">
                <asp:TextBox ID="txtDate" runat="server" CssClass="textbox"></asp:TextBox>
                <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999" MaskType="Date" TargetControlID="txtDate" PromptCharacter="_"> </asp:MaskedEditExtender>
                <asp:CalendarExtender ID="txtDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDate">
                </asp:CalendarExtender>
            </td>
            <td class="auto-style3">
                <asp:RadioButton ID="optStatuswise" runat="server" Text=" Status wise" 
                    AutoPostBack="True" GroupName="G1" />
            </td>
            <td width="20%">
                <asp:DropDownList ID="cboStatus" runat="server" CssClass="Dropdown">
                </asp:DropDownList>
            </td>
            <td width="20%">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 23%; height: 23px;">
                <asp:RadioButton ID="optEnquiryType" runat="server" Text="Enquiry Type wise" 
                    AutoPostBack="True" GroupName="G1" />
            </td>
            
            <td style="height: 23px">
                <asp:DropDownList ID="cboEnquiryType" runat="server" CssClass="Dropdown">
                </asp:DropDownList>
            </td>
            <td class="auto-style4">
                <asp:RadioButton ID="optClassWise" runat="server" Text="Class wise" 
                    AutoPostBack="True" GroupName="G1" />
            </td>
            <td style="height: 23px">
                <asp:DropDownList ID="cboClass" runat="server" CssClass="Dropdown">
                </asp:DropDownList>
            </td>
            <td style="height: 23px">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="height: 23px;" colspan="2">
                <asp:Label ID="lblStatus" runat="server" Font-Bold="True"></asp:Label>
            </td>
            
            <td class="auto-style4">
                &nbsp;</td>
            <td style="height: 23px">
                <asp:Button ID="btnView" runat="server" Text="Search" class="btn btn-primary" Width="120px" />
                            </td>
            <td style="height: 23px">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="5">
            <div id="gvDiv" style="width: 99%; max-height: 500px; overflow-y: scroll; text-align: center;">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" DataKeyNames="EnquiryID,StatusName, FeeBookNo, RegNo,IsAdminFeeDeposit,IsIcardAssigned" CssClass="Grid" Width="1035px">
                    
                    <Columns>
                        <%--<asp:CommandField ShowSelectButton="True" />--%>
                        <asp:TemplateField HeaderText="Select">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelect" runat="server" Visible="true"  />
                                                          </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="EnquiryNo" HeaderText="Form No." SortExpression="EnquiryNo" />
                        <asp:BoundField DataField="Sname" HeaderText="Student Name" SortExpression="Sname" />
                        <asp:BoundField DataField="Fname" HeaderText="Father Name" SortExpression="Fname" />
                        <asp:BoundField DataField="ClassName" HeaderText="Class Name" SortExpression="ClassName" />
                        <asp:BoundField DataField="MobNo" HeaderText="Mobile No" SortExpression="MobNo" />
                        <%--<asp:BoundField DataField="address" HeaderText="Address" SortExpression="address" />--%>
                        <asp:BoundField DataField="enquiryYear" HeaderText="Admission Date" SortExpression="enquiryYear" DataFormatString="{0:dd/MM/yyyy}" />
                         <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <%--<asp:CheckBox ID="chkSelect" runat="server" Visible="true"  />--%>
                                <asp:DropDownList ID="ddlStatus" Width="110px" CssClass="Dropdown" runat="server" >
                               
                                    <asp:ListItem>Open</asp:ListItem>
                                    <asp:ListItem>Called</asp:ListItem>
                                    <asp:ListItem>Waiting</asp:ListItem>
                                    <asp:ListItem>Selected</asp:ListItem>
                                    <asp:ListItem>Rejected</asp:ListItem>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Fee Book No">
                            <ItemTemplate>
                                <asp:textBox ID="txtFeeBookNo" runat="server" Width="90px" Visible="true"  />
                                                          </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Reg No">
                            <ItemTemplate>
                                <asp:textBox ID="txtRegNo" runat="server" Width="90px" Visible="true"  />
                                                          </ItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:TemplateField HeaderText="Fee Book Isseued">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkFeeSelect" runat="server" Visible="true"  />
                                                          </ItemTemplate>
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="I-Card">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkICard" runat="server" Visible="true"  />
                                                          </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Admission Fee">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkAdmissionFee" runat="server" Visible="true"  />
                                                          </ItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:TemplateField HeaderText="Admission">
                            <ItemTemplate>
                                <asp:Button ID="btnAdmission" runat="server" Text=">>" class="btn btn-primary" Visible="true"  />
                                                          </ItemTemplate>
                        </asp:TemplateField>--%>
                    </Columns>
                                      
                    <SelectedRowStyle BackColor="YellowGreen" Font-Bold="True" ForeColor="#333333" />
                   
                </asp:GridView>
            </div>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" 
                    SelectCommand="SELECT * FROM [vw_StudentEnquiry]"></asp:SqlDataSource>
               <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
            </td>
        </tr>
        <tr>
            <td colspan="5">
                <asp:Button ID="btnSave" runat="server" Text="Save" Visible="false" class="btn btn-primary" />
            &nbsp;
                <asp:Button ID="btnPrint" runat="server" Text="Print" Visible="False" class="btn btn-primary"/>
                &nbsp;
                <asp:Button ID="btnExcel" runat="server" Text="Export to Excel" Visible="False" class="btn btn-primary" />
            &nbsp;
                <asp:Button ID="btnPrintForm" runat="server" Text="Print Call Letter" class="btn btn-primary" Visible="False" />
                &nbsp; <asp:Button ID="btnFeeLabel" runat="server" Text="Print Fee Label" class="btn btn-primary" Visible="False" />
            </td>
        </tr>
        <tr>
            <td colspan="5">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="5">
                <asp:Panel ID="Panel1" runat="server">
                    <table class="table">
                        <tr>
                            <td>Date</td>
                            <td>
                                <asp:TextBox ID="txtSmsDate" runat="server" CssClass="textbox"></asp:TextBox>
                                <asp:CalendarExtender ID="txtDate0_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="txtSmsDate">
                                </asp:CalendarExtender>
                            </td>
                            <td>Time</td>
                            <td>
                                <asp:TextBox ID="txtTime" runat="server" CssClass="textbox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Message</td>
                            <td colspan="3">
                                <asp:TextBox ID="txtMessage" runat="server" CssClass="textbox" TextMode="MultiLine" Width="651px" height="54px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnSendSMS" runat="server" Text="Send SMS" class="btn btn-primary"/>
                            </td>
                        </tr>
                    </table>
                    
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="5">
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="100%" Visible="False">
                    <LocalReport ReportEmbeddedResource="rptFeeCollection.rdlc" ReportPath="rptFeeCollection.rdlc">
                        <DataSources>
                            <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="dsFeeCollection" />
                        </DataSources>
                    </LocalReport>
                </rsweb:ReportViewer>
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
            width: 18%;
        }
        .auto-style4 {
            height: 23px;
            width: 18%;
        }
        </style>
</asp:Content>

