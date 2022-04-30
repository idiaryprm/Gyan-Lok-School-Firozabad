<%@ Page Title="Transfer Certificate" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="Cert_TC.aspx.vb" Inherits="iDiary_V3.Cert_TC1" %>
<%@ Register Assembly="AjaxControlToolKit" Namespace="AjaxControlToolkit"  TagPrefix="asp" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Heading" Runat="Server">
    Transfer Certificate
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     
    <style type="text/css">
       
        .auto-style1 {
            width: 337px;
        }
        .auto-style2 {
            height: 26px;
            width: 337px;
        }
        .auto-style3 {
            width: 11px;
        }
        .auto-style4 {
            height: 26px;
            width: 11px;
        }
        .auto-style5 {
            width: 362px;
        }
        .auto-style6 {
            height: 26px;
            width: 362px;
        }
    </style>
    <div class="col_3" style="margin-top: 20px;" id="panelEnquiryNo" runat="server">
        <div class="col-md-3 widget widget1" style="width: 100%">
            <div class="r3_counter_box">
    <div style="text-transform:uppercase">
    
    <table class="table">
        <tr>
            <td class="auto-style1">
                Tc No
                <asp:Label ID="tcStatus" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="Red"></asp:Label>
            </td>
            <td class="auto-style3">
                <asp:TextBox ID="txtSno" runat="server" CssClass="textbox"></asp:TextBox>
            </td>

            <td>
                <asp:Button ID="btnSnoNext" runat="server" CssClass="btn btn-sm btn-primary" Text="&gt;&gt;" />
            </td>

            <td colspan="2" style="text-align: left">
                <asp:DropDownList ID="cboSchoolName" runat="server" AutoPostBack="true" CssClass="Dropdown" Width="300px">
                </asp:DropDownList>
            </td>

            <td style="width: 6%">
                &nbsp;</td>

        </tr>
        <tr>
            <td class="auto-style1">
                <asp:CheckBox ID="chkWholeClass" runat="server" AutoPostBack="True" ForeColor="Navy" Text="Whole Class" />
            </td>
            <td class="auto-style3">
                <asp:DropDownList ID="cboClass" runat="server" CssClass="Dropdown"
                    AutoPostBack="True" Visible="False"></asp:DropDownList>
            </td>

            <td>
                &nbsp;</td>

            <td class="auto-style5">
                <asp:Label ID="lblSection" runat="server" Text="Section" Visible="False"></asp:Label>
            </td>

            <td style="width: 6%">
                <asp:DropDownList ID="cboSection" runat="server" CssClass="Dropdown" Visible="False"></asp:DropDownList>
            </td>

            <td style="width: 6%">
                &nbsp;</td>

        </tr>
        <tr>
            <td class="auto-style1">
                Admin No / SR No</td>
            <td class="auto-style3">
                <asp:TextBox ID="txtSRNo" runat="server" CssClass="textbox"></asp:TextBox>
            </td>

            <td>
                <asp:Button ID="btnSRNoNext" runat="server" CssClass="btn btn-sm btn-primary" Text="&gt;&gt;" />
            </td>

            <td class="auto-style5">
                Student Name</td>

            <td style="width: 6%">
                <asp:TextBox ID="txtName" runat="server" CssClass="textbox"></asp:TextBox>
            </td>

            <td style="width: 6%">
                <asp:Button ID="btnNameNext" runat="server" Text=">>" CssClass="btn btn-sm btn-primary" />
            </td>

        </tr>
        <tr>
            <td colspan="5">
                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" AutoGenerateSelectButton="True" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"  DataSourceID="SqlDataSource2" ShowHeader="False" CssClass="Grid" width="98%">
                        <Columns>
                            <asp:BoundField DataField="RegNo" HeaderText="RegNo" SortExpression="RegNo" />
                            <asp:BoundField DataField="SName" HeaderText="SName" SortExpression="SName" />
                            <asp:BoundField DataField="ClassName" HeaderText="ClassName" SortExpression="ClassName" />
                            <asp:BoundField DataField="SecName" HeaderText="SecName" SortExpression="SecName" />
                        </Columns>
                       
                    </asp:GridView>
            </td>

            <td>
                    &nbsp;</td>

        </tr>
        <tr>
            <td class="auto-style1">Father Name</td>
            <td class="auto-style3">
                <asp:TextBox ID="txtFName" runat="server"  ReadOnly="True" 
                    CssClass="textbox" Enabled="False"></asp:TextBox>
                
            </td>

            <td>
                &nbsp;</td>

            <td class="auto-style5">
                Mother Name</td>

            <td style="width: 6%">
                <asp:TextBox ID="txtMName" runat="server"  ReadOnly="True" 
                    CssClass="textbox" Enabled="False"></asp:TextBox>
                
            </td>

            <td style="width: 6%">
                &nbsp;</td>

        </tr>
        <tr>
            <td class="auto-style2">DOB</td>
            <td style="margin-left: 120px; " class="auto-style4">
                <asp:TextBox ID="txtDOB" runat="server" CssClass="textbox" ReadOnly="True" 
                     Enabled="False"></asp:TextBox>
               
                <asp:CalendarExtender ID="txtDOB_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDOB">
                </asp:CalendarExtender>
                <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999" MaskType="Date" TargetControlID="txtDOB" PromptCharacter="_"> </asp:MaskedEditExtender>
               
            </td>

            <td style="margin-left: 120px; height: 26px; ">
                &nbsp;</td>

            <td style="margin-left: 120px; " class="auto-style6">
                Class/Section</td>

            <td style="margin-left: 120px; width: 6%; height: 26px;">
                <asp:TextBox ID="txtClassSection" runat="server" ReadOnly="True" 
                    CssClass="textbox" Enabled="False"></asp:TextBox>
                
            </td>

            <td style="margin-left: 120px; width: 6%; height: 26px;">
                &nbsp;</td>

        </tr>
        <tr>
            <td class="auto-style2">Admission Date</td>
            <td style="margin-left: 120px; " class="auto-style4">
                <asp:TextBox ID="txtDateAdmission" runat="server" CssClass="textbox"></asp:TextBox>
                
                <asp:CalendarExtender ID="txt3" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDateAdmission">
                </asp:CalendarExtender>
               
            </td>

            <td style="margin-left: 120px; height: 26px; ">
                &nbsp;</td>

            <td style="margin-left: 120px; " class="auto-style6">
                <%--Category--%>Category</td>

            <td style="margin-left: 120px; width: 6%; height: 26px;">
                <%--<asp:TextBox ID="txtCategory" runat="server" CssClass="textbox"></asp:TextBox>
                
                <asp:CalendarExtender ID="txtCategory_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="txtCategory">
                </asp:CalendarExtender>--%>
               
                <asp:TextBox ID="txtCategory" runat="server" ReadOnly="True" 
                    CssClass="textbox" Enabled="False"></asp:TextBox>
                
            </td>

            <td style="margin-left: 120px; width: 6%; height: 26px;">
                &nbsp;</td>

        </tr>
        <tr>
            <td class="auto-style1">Whether S.C./S.T.</td>
            <td style="margin-left: 40px; " class="auto-style3">
                <asp:DropDownList ID="ddlSCST" runat="server" CssClass="Dropdown">
                    <asp:ListItem>Yes</asp:ListItem>
                    <asp:ListItem>No</asp:ListItem>
                    <asp:ListItem Value="NA"></asp:ListItem>
                </asp:DropDownList>
                
            </td>

            <td style="margin-left: 40px; ">
                &nbsp;</td>

            <td style="margin-left: 40px; " class="auto-style5">
                Last Class Studied</td>

            <td style="margin-left: 40px; width: 6%;">
                <asp:DropDownList ID="cboLastClass" runat="server" CssClass="Dropdown" >
                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem>NA</asp:ListItem>
                    <asp:ListItem>Studying</asp:ListItem>
                     <asp:ListItem>L.K.G.</asp:ListItem>
                    <asp:ListItem>U.K.G.</asp:ListItem>
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

            <td style="margin-left: 40px; width: 6%;">
                &nbsp;</td>

        </tr>
        <tr>
            <td valign="top" class="auto-style1">
                Last Exam taken</td>
            <td style="margin-left: 40px; " class="auto-style3">
                <asp:DropDownList ID="cboLastExamTaken" runat="server" CssClass="Dropdown" AutoPostBack="True" >
                    <asp:ListItem>NA</asp:ListItem>
                    <asp:ListItem>HalfYearly</asp:ListItem>
                    <asp:ListItem>Annual</asp:ListItem>
                    <asp:ListItem>Term I</asp:ListItem>
                        <asp:ListItem>Term II</asp:ListItem>
                    <asp:ListItem>AISSCE</asp:ListItem>
                </asp:DropDownList>
                
            </td>

            <td style="margin-left: 40px; ">
                &nbsp;</td>

            <td style="margin-left: 40px; " class="auto-style5">
                Result</td>

            <td style="margin-left: 40px; width: 6%;">
                                <asp:DropDownList ID="cboLastClassResult" runat="server" CssClass="Dropdown" >
                                    <asp:ListItem></asp:ListItem>
                                    <asp:ListItem>NA</asp:ListItem>
                                    <asp:ListItem>Pass</asp:ListItem>
                                    <asp:ListItem>Fail</asp:ListItem>
                                    <asp:ListItem>Studying</asp:ListItem>
                                </asp:DropDownList>
                
            </td>

            <td style="margin-left: 40px; width: 6%;">
                                &nbsp;</td>

        </tr>
    
        <asp:Panel ID="Panel1" runat="server">
                <tr>
            <td valign="top" class="auto-style1">
                Whether Failed, if so once/twice in the class</td>
            <td style="margin-left: 40px; " class="auto-style3">
                <asp:TextBox ID="txtFailed" runat="server" CssClass="textbox"></asp:TextBox>
                
            </td>

            <td style="margin-left: 40px; ">
                &nbsp;</td>

            <td style="margin-left: 40px; " class="auto-style5">
                Subjects Studied</td>

            <td style="margin-left: 40px; width: 6%;">
                <asp:TextBox ID="txtSubjects" runat="server" CssClass="textbox"  Height="50px" TextMode="MultiLine"></asp:TextBox>
                
            </td>

            <td style="margin-left: 40px; width: 6%;">
                                &nbsp;</td>

        </tr>
        <tr>
            <td valign="top" class="auto-style1">
                Whether qualified for 
                <br />
                promotion of higher class</td>
            <td valign="top" class="auto-style3" style="vertical-align:middle; ">
                <asp:DropDownList ID="ddlPromoted" runat="server" CssClass="Dropdown" AutoPostBack="True">
                    <asp:ListItem>NA</asp:ListItem>
                    <asp:ListItem>Yes</asp:ListItem>
                    <asp:ListItem >No</asp:ListItem>
                </asp:DropDownList>
                
            </td>
            <td valign="top" class="td_width_10" style="vertical-align:middle; ">
                &nbsp;</td>
            <td style="margin-left: 40px; " class="auto-style5">
                
            &nbsp;to Class</td>

            <td style="margin-left: 40px; width: 6%;">
                <asp:DropDownList ID="cboToClass" runat="server" CssClass="Dropdown">
                    <asp:ListItem>
                    </asp:ListItem>
                    <asp:ListItem>L.K.G.</asp:ListItem>
                    <asp:ListItem>U.K.G.</asp:ListItem>
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

            <td style="margin-left: 40px; width: 6%;">
                &nbsp;</td>

        </tr>
        <tr>
            <td valign="top" class="auto-style1">
                Fee Paid upto </td>
            <td style="margin-left: 40px; " class="auto-style3">
                                <asp:TextBox ID="cboMonth" runat="server" CssClass="textbox"></asp:TextBox>
                
            </td>

            <td style="margin-left: 40px; ">
                                &nbsp;</td>

            <td style="margin-left: 40px; " class="auto-style5">
                Fee Concession availed</td>

            <td style="margin-left: 40px; width: 6%;">
                <asp:TextBox ID="txtFeeCon" runat="server" CssClass="textbox"></asp:TextBox>
                
            </td>

            <td style="margin-left: 40px; width: 6%;">
                &nbsp;</td>

        </tr>
       
        <tr>
            <td valign="top" class="auto-style1">
                No. of Working Days</td>
            <td style="margin-left: 40px; " class="auto-style3">
                <asp:TextBox ID="txtWorkDays" runat="server" CssClass="textbox"></asp:TextBox>
                
            </td>

            <td style="margin-left: 40px; ">
                &nbsp;</td>

            <td style="margin-left: 40px; " class="auto-style5">
                No. of days the pupil attended</td>

            <td style="margin-left: 40px; width: 6%;">
                <asp:TextBox ID="txtSchoolDays" runat="server" CssClass="textbox"></asp:TextBox>
                
            </td>

            <td style="margin-left: 40px; width: 6%;">
                &nbsp;</td>

        </tr>
      
      
        <tr>
              <td style="margin-left: 40px; " class="auto-style5">
                Extra-Corricular</td>

            <td style="margin-left: 40px; width: 6%;">
                <asp:TextBox ID="txtExtraCorr" runat="server" CssClass="textbox"></asp:TextBox>
                
            </td>
            

            <td style="margin-left: 40px; ">
                &nbsp;</td>

            <td style="margin-left: 40px; " class="auto-style5">
                Reason for leaving the School</td>

            <td style="margin-left: 40px; width: 6%;">
                <asp:DropDownList ID="cboreason" runat="server" CssClass="Dropdown">
                    <asp:ListItem>On Parent Request</asp:ListItem>
                    <asp:ListItem>Last Class X Exam</asp:ListItem>
                    <asp:ListItem>Change of School</asp:ListItem>
                    <asp:ListItem>Passout Class XII</asp:ListItem>
                    <asp:ListItem>Father Posting</asp:ListItem>
                    <asp:ListItem>Mother Posting</asp:ListItem>
                    <asp:ListItem>To Take Another Stream</asp:ListItem>
                    <asp:ListItem>Transfer</asp:ListItem>
                    <asp:ListItem>Transfer to Pension Establishment</asp:ListItem>
                </asp:DropDownList>
                
            </td>

            <td style="margin-left: 40px; width: 6%;">
                &nbsp;</td>

        </tr>
         <tr>
            <td valign="top" class="auto-style1">
                Remark<b style="color:red">*</b></td>
            <td style="margin-left: 40px; " colspan="2">
                <asp:TextBox ID="txtremark" runat="server" CssClass="textbox" Height="87px" TextMode="MultiLine" ></asp:TextBox>
                
            </td>


            <td style="margin-left: 40px; vertical-align:top" class="auto-style5">
                Cancel Tc</td>

            <td style="margin-left: 40px; vertical-align:top; text-align: left;" colspan="2">
                <asp:DropDownList ID="chkCancel" runat="server" CssClass="Dropdown" Height="26px">
                    <asp:ListItem>Yes</asp:ListItem>
                    <asp:ListItem>No</asp:ListItem>
                </asp:DropDownList>
                
            </td>

        </tr>
       
            </asp:Panel>
          <tr>
            <td valign="top" class="auto-style1">
                NCC/Scout etc.</td>
            <td style="margin-left: 40px; " class="auto-style3">
                <asp:TextBox ID="txtNCC" runat="server" CssClass="textbox" ></asp:TextBox>
                
            </td>

               <td style="margin-left: 40px; width: 6%;">
                &nbsp;</td>
          <td valign="top" class="auto-style1">
                Date Certificate Issue</td>
            <td style="margin-left: 40px; " class="auto-style3">
                <asp:TextBox ID="txtIssuedDate" runat="server" ReadOnly="True" 
                    CssClass="textbox"></asp:TextBox>
                   <asp:CalendarExtender ID="txt2" runat="server" Format="dd/MM/yyyy" TargetControlID="txtIssuedDate">
                </asp:CalendarExtender>
                <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999" MaskType="Date" TargetControlID="txtIssuedDate" PromptCharacter="_"> </asp:MaskedEditExtender>
            </td>

           

        </tr>
          <tr>
            <td valign="top" class="auto-style1">
                Conduct<b style="color:red">*</b>
                <asp:ImageButton ID="ImageButton1" runat="server" 
                    ImageUrl="~/images/Refresh.jpg" style="width: 17px" Visible="False" />
            </td>
            <td style="margin-left: 40px; " class="auto-style3">
                <asp:DropDownList ID="cboCharacter" runat="server" CssClass="Dropdown">
                </asp:DropDownList>
                
            </td>
              <td style="margin-left: 40px; width: 6%;">
                &nbsp;</td>
            <td style="margin-left: 40px; " class="auto-style5">
                Date Application for Certificate <b style="color:red">*</b></td>

            <td style="margin-left: 40px; width: 6%;">
                <asp:TextBox ID="txtApplicationDate" runat="server" CssClass="textbox"></asp:TextBox>
                <asp:CalendarExtender ID="txt1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtApplicationDate">
                </asp:CalendarExtender>
                <asp:MaskedEditExtender ID="MaskedEditExtender3" runat="server" Mask="99/99/9999" MaskType="Date" TargetControlID="txtApplicationDate" PromptCharacter="_"> </asp:MaskedEditExtender>
                
            </td>

            

        </tr>
        <tr>
            <td colspan="5">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="TCDetailsID" class="Grid">
                    <Columns>
                        <asp:BoundField DataField="TCDetailsID" HeaderText="TCDetailsID" ReadOnly="True" SortExpression="TCDetailsID" Visible="false"></asp:BoundField>
                        <asp:BoundField DataField="ClassName" HeaderText="ClassName" SortExpression="ClassName"></asp:BoundField>
                        <asp:BoundField DataField="ASession" HeaderText="ASession" SortExpression="ASession"></asp:BoundField>
                        <asp:TemplateField HeaderText="Date of Admission">
                            <ItemTemplate>
                                <asp:TextBox ID="txtPromotionDate" runat="server" width="110px"></asp:TextBox>
                                <asp:CalendarExtender ID="txt2" runat="server" Format="dd/MM/yyyy" TargetControlID="txtPromotionDate"></asp:CalendarExtender>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date of promotion">
                            <ItemTemplate>
                                <asp:TextBox ID="txtEndDate" runat="server" width="110px"></asp:TextBox>
                                <asp:CalendarExtender ID="txt3" runat="server" Format="dd/MM/yyyy" TargetControlID="txtEndDate"></asp:CalendarExtender>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Work">
                            <ItemTemplate>
                                <asp:TextBox ID="txtRemark" runat="server"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Conduct Concession">
                            <ItemTemplate>
                                <asp:DropDownList ID="cboConduct" runat="server"></asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date Of Removal">
                            <ItemTemplate>
                                <asp:TextBox ID="txtRemoval" runat="server" width="110px"></asp:TextBox>
                                <asp:CalendarExtender ID="txt4" runat="server" Format="dd/MM/yyyy" TargetControlID="txtRemoval"></asp:CalendarExtender>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Reason of Leaving">
                            <ItemTemplate>
                                <asp:DropDownList ID="cboRemoval" runat="server">
                                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem>On Parent Request</asp:ListItem>
                    <asp:ListItem>Last Class X Exam</asp:ListItem>
                    <asp:ListItem>Change of School</asp:ListItem>
                    <asp:ListItem>Passout Class XII</asp:ListItem>
                    <asp:ListItem>Father Posting</asp:ListItem>
                    <asp:ListItem>Mother Posting</asp:ListItem>
                    <asp:ListItem>To Take Another Stream</asp:ListItem>
                    <asp:ListItem>Transfer</asp:ListItem>
                    <asp:ListItem>Transfer to Pension Establishment</asp:ListItem>
                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <%--<asp:SqlDataSource runat="server" ID="SqlDataSource1" ConnectionString='<%$ ConnectionStrings:iDiaryConnectionString %>' SelectCommand="SELECT [TCDetailsID], [ClassName], [Conduct], [Remark], [PromotionDate], [EndDate], [ASession] FROM [vw_Student] where SID < 0"></asp:SqlDataSource>--%>
            </td>
        </tr>
        <tr>
            <td>Class</td>
            <td><asp:DropDownList ID="cboClasses" runat="server" CssClass="Dropdown"></asp:DropDownList></td>
            <td></td>
            <td>Conduct</td>
            <td><asp:DropDownList ID="cboConduct" runat="server" CssClass="Dropdown">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td>Work</td>
            <td><asp:TextBox ID="txtWork" runat="server" CssClass="textbox"></asp:TextBox>
            </td>
            <td></td>
            <td>Admission Date
            </td>
            <td> <asp:TextBox ID="txtProDate" runat="server" CssClass="textbox"></asp:TextBox>
                <asp:CalendarExtender ID="txtProDate_CalendarExtender" Format="dd/MM/yyyy"  runat="server" TargetControlID="txtProDate" />
            </td>
        </tr>
        <tr>
            <td> Promotion Date
             </td>
            <td><asp:TextBox ID="txtEndDate1" runat="server" CssClass="textbox"></asp:TextBox>
                <asp:CalendarExtender ID="txtEndDate1_CalendarExtender" Format="dd/MM/yyyy" runat="server" TargetControlID="txtEndDate1" />
            </td>
            <td></td>
            <td>
                 Session 
            </td>
            <td>
                <asp:TextBox ID="txtSession" runat="server" CssClass="textbox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Date of Removal</td>
            <td><asp:TextBox ID="txtDateOfRemoval" runat="server" CssClass="textbox"></asp:TextBox>
                <asp:CalendarExtender ID="txtDateOfRemoval_CalendarExtender" Format="dd/MM/yyyy"  runat="server" TargetControlID="txtDateOfRemoval"></asp:CalendarExtender>
            </td>
            <td></td>
            <td>Reason of Removal</td>
            <td>
                <asp:DropDownList ID="cboRemoval" runat="server">
                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem>On Parent Request</asp:ListItem>
                    <asp:ListItem>Last Class X Exam</asp:ListItem>
                    <asp:ListItem>Change of School</asp:ListItem>
                    <asp:ListItem>Passout Class XII</asp:ListItem>
                    <asp:ListItem>Father Posting</asp:ListItem>
                    <asp:ListItem>Mother Posting</asp:ListItem>
                    <asp:ListItem>To Take Another Stream</asp:ListItem>
                    <asp:ListItem>Transfer</asp:ListItem>
                    <asp:ListItem>Transfer to Pension Establishment</asp:ListItem>
                </asp:DropDownList></td>
            
            
        </tr>
        <tr>
            <td><asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn btn-primary" /></td>
        </tr>
         <tr>
            <td class="auto-style1">
                <asp:Label ID="lblStatus" runat="server" Font-Bold="True" ForeColor="Navy"></asp:Label>
            </td>
            <td style="margin-left: 40px; " colspan="5">
                <asp:Button ID="btnGenerate" runat="server"  Text="Generate TC" 
                    CssClass="btn btn-primary" />
                &nbsp;&nbsp; <asp:Button ID="btnUpdate" runat="server"  Text="Update TC"  Visible="false" 
                    CssClass="btn btn-primary" />
                &nbsp;&nbsp; <asp:Button ID="btnNew" runat="server"  Text="New" 
                    CssClass="btn btn-primary" />
            &nbsp;
                <asp:Button ID="btnCancel" runat="server"  Text="Cancel TC" 
                    CssClass="btn btn-primary" Visible="False" />
            </td>

            <td style="margin-left: 40px; ">
                &nbsp;</td>
             
        </tr>
        <tr>
            <td class="auto-style1">
                <asp:TextBox ID="txtSID" runat="server" Visible="False" CssClass="textbox"></asp:TextBox>
            </td>
            <td class="auto-style3">
            &nbsp;<asp:TextBox ID="txtClassName" runat="server" Visible="False" CssClass="textbox" Width="10px"></asp:TextBox>
                <asp:TextBox ID="txtSecName" runat="server" Visible="False"  CssClass="textbox" Width="10px"></asp:TextBox>
                <asp:TextBox ID="txtCSSID" runat="server" Visible="False" CssClass="textbox" Width="10px"></asp:TextBox>
                <asp:Label ID="lblCount" runat="server" Text="" Visible="false"></asp:Label>
                &nbsp;&nbsp;&nbsp;
                </td>
            <td colspan="2">
                &nbsp;</td>
            <td class="auto-style5">
                &nbsp;</td>
            <td style="width: 6%">
                &nbsp;</td>

            <td style="width: 6%">
                &nbsp;</td>

        </tr>
        <tr>
            
            <td colspan="5">
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT RegNo, SName, ClassName, SecName FROM vw_Student WHERE SID<0">
                   
                </asp:SqlDataSource>
              
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" AsyncRendering="false"  Font-Names="mangal" Font-Size="8pt" WaitMessageFont-Names="mangal" WaitMessageFont-Size="14pt" Height="800px" Width="754px" style="margin-left: 0px; margin-right: 107px;">
                    <LocalReport ReportEmbeddedResource="iDiary_V3.rptTC.rdlc">
                        <DataSources>
                            <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="DataSet1" />
                        </DataSources>
                    </LocalReport>
                </rsweb:ReportViewer>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData" TypeName="dbidiaryKhairDataSetTableAdapters.vw_StudentTCTableAdapter"></asp:ObjectDataSource>
            </td>

            <td>
                &nbsp;</td>

        </tr>
    </table>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    </div>
                 </div>
        </div>
        <div class="clearfix"></div>
    </div>

    <asp:Label ID="lblPromotionDate" runat="server" Text=""></asp:Label>
</asp:Content>