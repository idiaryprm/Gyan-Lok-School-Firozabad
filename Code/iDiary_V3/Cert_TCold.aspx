<%@ Page Title="Transfer Certificate" Language="vb" AutoEventWireup="false" MasterPageFile="~/Certificates.Master" CodeBehind="Cert_TCold.aspx.vb" Inherits="iDiary_V3.Cert_TCold" %>
<%@ Register Assembly="AjaxControlToolKit" Namespace="AjaxControlToolkit"  TagPrefix="asp" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SubHeading" Runat="Server">
    Transfer Certificate
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="CertificateContent" Runat="Server">
    <div>
    
    <table border="0" cellpadding="2" cellspacing="2" style="width: 99%">
        <tr>
            <td style="width: 127px">
                Choose Academic Year</td>
            <td colspan="2">
                <asp:DropDownList ID="cboSession" runat="server" class="form-control1"></asp:DropDownList>
            </td>

            <td style="width: 6%">
                &nbsp;</td>

        </tr>
        <tr>
            <td style="width: 127px">
                Student Name</td>
            <td colspan="2">
                <asp:TextBox ID="txtSName" runat="server" BorderWidth="1px" Width="140px"></asp:TextBox>
            &nbsp;&nbsp; <asp:ImageButton ID="btnNameNext" runat="server" 
                    ImageUrl="~/images/next.png" ImageAlign="AbsMiddle" style="margin-top: 0px" />
            </td>

            <td style="width: 6%">
                &nbsp;</td>

        </tr>
        <tr>
            <td colspan="4">
                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" AutoGenerateSelectButton="True" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataSourceID="SqlDataSource2" ShowHeader="False" Width="100%" Font-Names="Garamond" Font-Size="10pt">
                        <Columns>
                            <asp:BoundField DataField="TCNo" HeaderText="TCNo" SortExpression="TCNo"></asp:BoundField>
                            <asp:BoundField DataField="SName" HeaderText="SName" SortExpression="SName" />
                            <asp:BoundField DataField="MName" HeaderText="MName" SortExpression="MName" />
                            <asp:BoundField DataField="FName" HeaderText="FName" SortExpression="FName" />
                        </Columns>
                        <FooterStyle BackColor="White" ForeColor="#000066" />
                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                        <RowStyle ForeColor="#000066" />
                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                    </asp:GridView>
            </td>

        </tr>
        <tr>
            <td style="width: 127px">
                TC No</td>
            <td style="width: 197px">
                <asp:TextBox ID="txtSno" runat="server" BorderWidth="1px" Width="98px"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:ImageButton ID="btnSnoNext" runat="server" 
                    ImageUrl="~/images/next.png" ImageAlign="AbsMiddle" />
            </td>

            <td style="width: 174px">
                &nbsp;</td>

            <td style="width: 6%">
                &nbsp;</td>

        </tr>
        <tr>
            <td style="width: 127px">
                <asp:CheckBox ID="chkWholeClass" runat="server" AutoPostBack="True" ForeColor="Navy" Text="Whole Class" Enabled="False" />
            </td>
            <td style="width: 197px">
                <asp:DropDownList ID="cboClass" runat="server" Width="105px" 
                    AutoPostBack="True" Visible="False"></asp:DropDownList>
            </td>

            <td style="width: 174px">
                <asp:Label ID="lblSection" runat="server" Text="Section" Visible="False"></asp:Label>
            </td>

            <td style="width: 6%">
                <asp:DropDownList ID="cboSection" runat="server" Width="105px" Visible="False"></asp:DropDownList>
            </td>

        </tr>
        <tr>
            <td style="width: 127px">
                Admin No / SR No</td>
            <td style="width: 197px">
                <asp:TextBox ID="txtSRNo" runat="server" BorderWidth="1px" Width="98px"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;&nbsp; <asp:ImageButton ID="btnSRNoNext" runat="server" 
                    ImageUrl="~/images/next.png" ImageAlign="AbsMiddle" style="height: 19px" Visible="False" />
            </td>

            <td style="width: 174px">
                Student Name</td>

            <td style="width: 6%">
                <asp:TextBox ID="txtName" runat="server" BorderWidth="1px" Width="98px"></asp:TextBox>
            &nbsp;&nbsp;&nbsp; 
            </td>

        </tr>
        <tr>
            <td colspan="4">
                    &nbsp;</td>

        </tr>
        <tr>
            <td style="width: 127px">Father Name</td>
            <td style="width: 197px">
                <asp:TextBox ID="txtFName" runat="server" BorderWidth="1px" ReadOnly="True" 
                    Width="170px" BackColor="White" Enabled="False"></asp:TextBox>
                
            </td>

            <td style="width: 174px">
                Mother Name</td>

            <td style="width: 6%">
                <asp:TextBox ID="txtMName" runat="server" BorderWidth="1px" ReadOnly="True" 
                    Width="170px" BackColor="White" Enabled="False"></asp:TextBox>
                
            </td>

        </tr>
        <tr>
            <td style="width: 127px; height: 26px;" class="td_width_10">DOB</td>
            <td style="margin-left: 120px; height: 26px; width: 197px;">
                <asp:TextBox ID="txtDOB" runat="server" BorderWidth="1px" ReadOnly="True" 
                    Width="170px" BackColor="White" Enabled="False"></asp:TextBox>
               
                <asp:CalendarExtender ID="txtDOB_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDOB">
                </asp:CalendarExtender>
               
            </td>

            <td style="margin-left: 120px; height: 26px; width: 174px;">
                Class/Section</td>

            <td style="margin-left: 120px; width: 6%; height: 26px;">
                <asp:TextBox ID="txtClassSection" runat="server" BorderWidth="1px" ReadOnly="True" 
                    Width="170px" BackColor="White" Enabled="False"></asp:TextBox>
                
            </td>

        </tr>
        <tr>
            <td style="height: 26px;" class="td_width_10" colspan="3">Whether the student belongs to Schedule Caste or Schedule Tribe or OBC</td>

            <td style="margin-left: 120px; width: 6%; height: 26px;">
                <asp:TextBox ID="txtCategory" runat="server" BorderWidth="1px" ReadOnly="True" 
                    Width="170px" BackColor="White" Enabled="False"></asp:TextBox>
                
            </td>

        </tr>
        <tr>
            <td style="width: 127px">Admission Date</td>
            <td style="margin-left: 40px; width: 197px;">
                <asp:TextBox ID="txtDateAdmission" runat="server" BorderWidth="1px" 
                    Width="120px" BackColor="White"></asp:TextBox>
                
                <asp:CalendarExtender ID="txtDateAdmission_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDateAdmission">
                </asp:CalendarExtender>
               
            </td>

            <td style="margin-left: 40px; width: 174px;">
                Last Class Studied</td>

            <td style="margin-left: 40px; width: 6%;">
                <asp:DropDownList ID="cboLastClass" runat="server" Width="175px">
                    <asp:ListItem>Studying</asp:ListItem>
                    <asp:ListItem>I</asp:ListItem>
                    <asp:ListItem>II</asp:ListItem>
                    <asp:ListItem>III</asp:ListItem>
                    <asp:ListItem>IV</asp:ListItem>
                    <asp:ListItem>V</asp:ListItem>
                    <asp:ListItem>VI</asp:ListItem>
                    <asp:ListItem>VII</asp:ListItem>
                    <asp:ListItem>VIII</asp:ListItem>
                    <asp:ListItem>IX</asp:ListItem>
                    <asp:ListItem>X</asp:ListItem>
                    <asp:ListItem>XI</asp:ListItem>
                    <asp:ListItem>XII</asp:ListItem>
                </asp:DropDownList>
                
            </td>

        </tr>
        <tr>
            <td style="width: 127px" valign="top">
                Last Exam taken</td>
            <td style="margin-left: 40px; width: 197px;">
                <asp:DropDownList ID="cboLastExamTaken" runat="server" Width="128px">
                    <asp:ListItem>FA 1</asp:ListItem>
                    <asp:ListItem>FA 2
                    </asp:ListItem>
                    <asp:ListItem>SA 1</asp:ListItem>
                    <asp:ListItem>FA 3</asp:ListItem>
                    <asp:ListItem>FA 4</asp:ListItem>
                    <asp:ListItem>SA 2</asp:ListItem>
                    <asp:ListItem>UT1</asp:ListItem>
                    <asp:ListItem>HY</asp:ListItem>
                    <asp:ListItem>UT2</asp:ListItem>
                </asp:DropDownList>
                
            </td>

            <td style="margin-left: 40px; width: 174px;">
                Result</td>

            <td style="margin-left: 40px; width: 6%;">
                                <asp:DropDownList ID="cboLastClassResult" runat="server" Width="175px">
                                    <asp:ListItem>Passed</asp:ListItem>
                                    <asp:ListItem>Failed</asp:ListItem>
                                    <asp:ListItem>Studying</asp:ListItem>
                                </asp:DropDownList>
                
            </td>

        </tr>
        <tr>
            <td valign="top" class="td_width_10" colspan="2">
                Whether Failed, if so once/twice in the class</td>
            <td style="margin-left: 40px; width: 174px;">
                &nbsp;</td>

            <td style="margin-left: 40px; width: 6%;">
                <asp:TextBox ID="txtFailed" runat="server" BorderWidth="1px" 
                    Width="170px" BackColor="White"></asp:TextBox>
                
            </td>

        </tr>
        <tr>
            <td style="width: 127px" valign="top">
                Subjects Studied</td>
            <td style="margin-left: 40px; " colspan="3">
                <asp:TextBox ID="txtSubjects" runat="server" BorderWidth="1px" 
                    Width="510px" BackColor="White" Height="18px"></asp:TextBox>
                
            </td>

        </tr>
        <tr>
            <td valign="top" class="td_width_10" colspan="2">
                Whether qualified for promotion of higher class</td>
            <td style="margin-left: 40px; width: 174px;">
                <asp:DropDownList ID="ddlPromoted" runat="server">
                    <asp:ListItem>Yes</asp:ListItem>
                    <asp:ListItem>No</asp:ListItem>
                    <asp:ListItem Value="NA"></asp:ListItem>
                </asp:DropDownList>
                
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                to Class</td>

            <td style="margin-left: 40px; width: 6%;">
                &nbsp;<asp:DropDownList ID="cboLastClass0" runat="server" Width="100px">
                    <asp:ListItem>Studying</asp:ListItem>
                    <asp:ListItem>Detained</asp:ListItem>
                    <asp:ListItem>Compartment</asp:ListItem>
                    <asp:ListItem>Promoted</asp:ListItem>
                    <asp:ListItem>Appeared in X NIOS Exam</asp:ListItem>
                    <asp:ListItem>Passed in XII NIOS
                    </asp:ListItem>
                    <asp:ListItem>Passed in XI NIOS</asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList ID="cboLastClass1" runat="server" Width="60px">
                    <asp:ListItem>
                    </asp:ListItem>
                    <asp:ListItem>I</asp:ListItem>
                    <asp:ListItem>II</asp:ListItem>
                    <asp:ListItem>III</asp:ListItem>
                    <asp:ListItem>IV</asp:ListItem>
                    <asp:ListItem>V</asp:ListItem>
                    <asp:ListItem>VI</asp:ListItem>
                    <asp:ListItem>VII</asp:ListItem>
                    <asp:ListItem>VIII</asp:ListItem>
                    <asp:ListItem>IX</asp:ListItem>
                    <asp:ListItem>X</asp:ListItem>
                    <asp:ListItem>XI</asp:ListItem>
                    <asp:ListItem>XII</asp:ListItem>
                </asp:DropDownList>
                
            </td>

        </tr>
        <tr>
            <td style="width: 127px" valign="top">
                Fee Paid upto Month</td>
            <td style="margin-left: 40px; width: 197px;">
                                <asp:DropDownList ID="cboMonth" runat="server" Width="180px">
                                    <asp:ListItem>January</asp:ListItem>
                                    <asp:ListItem>February</asp:ListItem>
                                    <asp:ListItem>March</asp:ListItem>
                                    <asp:ListItem>April</asp:ListItem>
                                    <asp:ListItem>May</asp:ListItem>
                                    <asp:ListItem>June</asp:ListItem>
                                    <asp:ListItem>July</asp:ListItem>
                                    <asp:ListItem>August</asp:ListItem>
                                    <asp:ListItem>September</asp:ListItem>
                                    <asp:ListItem>October</asp:ListItem>
                                    <asp:ListItem>November</asp:ListItem>
                                    <asp:ListItem>December</asp:ListItem>
                                    <asp:ListItem></asp:ListItem>
                                </asp:DropDownList>
                
            </td>

            <td style="margin-left: 40px; width: 174px;">
                Fee Concession availed</td>

            <td style="margin-left: 40px; width: 6%;">
                <asp:TextBox ID="txtFeeCon" runat="server" BorderWidth="1px" 
                    Width="170px" BackColor="White"></asp:TextBox>
                
            </td>

        </tr>
        <tr>
            <td style="width: 127px" valign="top">
                Left Date</td>
            <td style="margin-left: 40px; width: 197px;">
                <asp:TextBox ID="txtDateDropOut" runat="server" BorderWidth="1px" 
                    Width="170px" BackColor="White"></asp:TextBox>
                <asp:CalendarExtender ID="txtDateDropOut_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDateDropOut">
                </asp:CalendarExtender>
            </td>

            <td style="margin-left: 40px; width: 174px;">
                Reason for leaving the School</td>

            <td style="margin-left: 40px; width: 6%;">
                <asp:DropDownList ID="cboreason" runat="server" Width="175px">
                    <asp:ListItem>On Parent Request</asp:ListItem>
                    <asp:ListItem>Last Class X Exam</asp:ListItem>
                    <asp:ListItem>Change of School</asp:ListItem>
                    <asp:ListItem>Passed XII Class</asp:ListItem>
                    <asp:ListItem>Father Posting</asp:ListItem>
                    <asp:ListItem>Mother Posting</asp:ListItem>
                    <asp:ListItem>To Take Another Stream</asp:ListItem>
                    <asp:ListItem>Transfer</asp:ListItem>
                    <asp:ListItem>Transfer to Pension Establishment</asp:ListItem>
                </asp:DropDownList>
            </td>

        </tr>
        <tr>
            <td style="width: 127px" valign="top">
                No. of Working Days</td>
            <td style="margin-left: 40px; width: 197px;">
                <asp:TextBox ID="txtWorkDays" runat="server" BorderWidth="1px" 
                    Width="170px" BackColor="White"></asp:TextBox>
                
            </td>

            <td style="margin-left: 40px; width: 174px;">
                No. of days the pupil attended</td>

            <td style="margin-left: 40px; width: 6%;">
                <asp:TextBox ID="txtSchoolDays" runat="server" BorderWidth="1px" 
                    Width="170px" BackColor="White"></asp:TextBox>
                
            </td>

        </tr>
        <tr>
            <td style="width: 127px" valign="top">
                NCC/Scout etc.</td>
            <td style="margin-left: 40px; width: 197px;">
                <asp:TextBox ID="txtNCC" runat="server" BorderWidth="1px" 
                    Width="170px" BackColor="White"></asp:TextBox>
                
            </td>

            <td style="margin-left: 40px; width: 174px;">
                Extra-Corricular</td>

            <td style="margin-left: 40px; width: 6%;">
                <asp:TextBox ID="txtExtraCorr" runat="server" BorderWidth="1px" 
                    Width="170px" BackColor="White"></asp:TextBox>
                
            </td>

        </tr>
        <tr>
            <td style="width: 127px" valign="top">
                <asp:HyperLink ID="HyperLink1" runat="server" ForeColor="Navy" 
                    NavigateUrl="CharacterMaster.aspx">Conduct</asp:HyperLink>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;<asp:ImageButton ID="ImageButton1" runat="server" 
                    ImageUrl="~/images/Refresh.jpg" style="width: 17px" Visible="False" />
            </td>
            <td style="margin-left: 40px; width: 197px;">
                <asp:DropDownList ID="cboCharacter" runat="server" BackColor="White" 
                    Width="170px">
                </asp:DropDownList>
            </td>

            <td style="margin-left: 40px; width: 174px;">
                Date of application for Certificate</td>

            <td style="margin-left: 40px; width: 6%;">
                <asp:TextBox ID="txtApplyDate" runat="server" BorderWidth="1px" 
                    Width="170px" BackColor="White"></asp:TextBox>
                <asp:CalendarExtender ID="txtPromotionDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="txtApplyDate">
                </asp:CalendarExtender>
            </td>

        </tr>
        <tr>
            <td style="width: 127px" valign="top">
                Date of Issue of Certificate</td>
            <td style="margin-left: 40px; width: 197px;">
                <asp:TextBox ID="txtPrintDate" runat="server" BorderWidth="1px" ReadOnly="True" 
                    Width="170px" BackColor="White" Visible="False"></asp:TextBox>
                   <asp:CalendarExtender ID="txtPrintDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="txtPrintDate">
                </asp:CalendarExtender>
            </td>

            <td style="margin-left: 40px; width: 174px;">
                &nbsp;</td>

            <td style="margin-left: 40px; width: 6%;">
                &nbsp;</td>

        </tr>
        <tr>
            <td style="width: 127px" valign="top">
                Remarks</td>
            <td style="margin-left: 40px; width: 197px;">
                <asp:TextBox ID="txtremark" runat="server" BorderWidth="1px" 
                    Width="170px" BackColor="White" Height="49px" TextMode="MultiLine"></asp:TextBox>
                
            </td>

            <td style="margin-left: 40px; width: 174px;">
                &nbsp;</td>

            <td style="margin-left: 40px; width: 6%;">
                &nbsp;</td>

        </tr>
        <tr>
            <td style="width: 127px">
                &nbsp;</td>
            <td style="margin-left: 40px; width: 197px;">
                &nbsp;</td>

            <td style="margin-left: 40px; width: 174px;">
                &nbsp;</td>

            <td style="margin-left: 40px; width: 6%;">
                &nbsp;</td>

        </tr>
        <tr>
            <td style="width: 127px">
                <asp:Label ID="lblStatus" runat="server" Font-Bold="True" ForeColor="Navy"></asp:Label>
            </td>
            <td style="margin-left: 40px; width: 197px;">
                &nbsp;&nbsp; <asp:Button ID="btnUpdate" runat="server" BorderWidth="1px" Text="Update TC" 
                    Width="80px" Font-Bold="True" BorderColor="Black" />
            </td>

            <td style="margin-left: 40px; " colspan="2">
                &nbsp;&nbsp; <asp:Button ID="btnNew" runat="server" BorderWidth="1px" Text="New" 
                    Width="48px" Font-Bold="True" BorderColor="Black" />
            &nbsp;
                <asp:Button ID="btnCancel" runat="server" BorderWidth="1px" Text="Cancel TC" 
                    Width="126px" Font-Bold="True" BorderColor="Black" />
                <asp:Button ID="btnGenerate" runat="server" BorderWidth="1px" Text="Generate TC" 
                    Width="100px" Font-Bold="True" BorderColor="Black" Visible="False" />
                </td>

        </tr>
        <tr>
            <td style="width: 127px">
                <asp:TextBox ID="txtSID" runat="server" Visible="False" Width="92px"></asp:TextBox>
            </td>
            <td style="width: 197px">
            &nbsp;<asp:TextBox ID="txtClassName" runat="server" Visible="False" Width="10px"></asp:TextBox>
                <asp:TextBox ID="txtSecName" runat="server" Visible="False" Width="10px"></asp:TextBox>
                <asp:TextBox ID="txtCSSID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;
                </td>
            <td style="width: 174px">
                &nbsp;</td>
            <td style="width: 6%">
                &nbsp;</td>

        </tr>
        <tr>
            <td colspan="4">
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT TCNo,  SName, MName,FName FROM oldTCData WHERE (ASID = @ASID) and [SName] Like '%SearchByName%' or @SearchByName is null">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="cboSession" Name="ASID" PropertyName="SelectedValue" />
                        <asp:ControlParameter ControlID="txtSName" Name="SearchByName" PropertyName="Text" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" AsyncRendering="false"  Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Height="800px" Width="689px">
                    <LocalReport ReportEmbeddedResource="iDiary_V3.rptTC.rdlc">
                        <DataSources>
                            <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="DataSet1" />
                        </DataSources>
                    </LocalReport>
                </rsweb:ReportViewer>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData" TypeName="dbidiaryKhairDataSetTableAdapters.vw_StudentTCTableAdapter"></asp:ObjectDataSource>
            </td>

        </tr>
    </table>

    </div>
</asp:Content>