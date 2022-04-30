<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="EmployeeMaster.aspx.vb" Inherits="iDiary_V3.EmployeeMaster" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Heading" runat="server">
    Employee Master
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />--%>
      <link href="css/jquery-ui.css" rel="stylesheet" />
    <script src="js/jquery-ui.js"></script>
    <script>
        function ValidateEmail(mail) {
            if (/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test(myForm.emailAddr.value)) {
                return (true)
            }
            alert("You have entered an invalid email address!")
            return (false)
        }
    </script>
     <script>
         function isNumber(evt) {
             evt = (evt) ? evt : window.event;
             var charCode = (evt.which) ? evt.which : evt.keyCode;
             if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                 return false;
             }
             return true;
         }
    </script>
  <script>
      $(function () {
          $("#accordion").accordion({
              heightStyle: "content"


          });
      });
  </script>
    <div class="col_3" style="margin-top: 20px;" id="panelEnquiryNo" runat="server">
        <div class="col-md-3 widget widget1" style="width: 100%">
            <div class="r3_counter_box">
                <table class="table">
                    <tr>
                        <td colspan="7">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" AutoGenerateSelectButton="True" BackColor="White" BorderColor="#CC9966" BorderStyle="None" CellPadding="4" DataSourceID="SqlDataSource1" Width="97%" CssClass="Grid">
                                <Columns>
                                    <asp:BoundField DataField="EmpCode" HeaderText="Code" SortExpression="EmpCode" />
                                    <asp:BoundField DataField="EmpName" HeaderText="EmpName" SortExpression="EmpName" />
                                    <asp:BoundField DataField="SchoolName" HeaderText="School" SortExpression="SchoolName" />
                                    <asp:BoundField DataField="DeptName" HeaderText="Department" SortExpression="DeptName" />
                                    <asp:BoundField DataField="DesgName" HeaderText="Designation" SortExpression="DesgName" />
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
                        </td>

                    </tr>
                    <tr>
                        <td style="width: 13%">School Name</td>
                        <td colspan="3">
                            <asp:DropDownList ID="cboSchoolName" runat="server" AutoPostBack="false" CssClass="Dropdown" Width="300px">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 16%">
                            &nbsp;</td>
                        <td align="center" style="text-align: left" class="auto-style2">
                            &nbsp;</td>
                        <td width="17%" align="right" rowspan="5" style="text-align: center; vertical-align: top">

                            <asp:Image ID="Image1" CssClass="textbox" runat="server" Height="134px" Width="129px" ImageUrl="~/Images/EmpDummy.jpg" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 13%">Name<span style="color: #CC0000">*</span><asp:TextBox ID="txtEmpID" runat="server" Width="23px" Visible="False"></asp:TextBox>
                        </td>
                        <td class="auto-style2">
                            <asp:TextBox ID="txtName" runat="server" CssClass="textbox"></asp:TextBox>
                        </td>
                        <td class="auto-style1">
                <asp:Button ID="btnNameSearch" runat="server" Text=">>" CssClass="btn btn-sm btn-primary" />
                        </td>
                        <td style="width: 13%">Employee Code<span style="color: #CC0000">*</span></td>
                        <td style="width: 16%">
                            <asp:TextBox ID="txtEmpCode" runat="server" CssClass="textbox"></asp:TextBox>&nbsp;
                        </td>
                        <td align="center" style="text-align: left" class="auto-style2">
                            <asp:Button ID="btnNext" runat="server" Text=">>" CssClass="btn btn-sm btn-primary" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Photo</td>
                    </tr>
                    <tr>
                        <td style="width: 13%">Biometric Code</td>
                        <td class="auto-style2">
                            <asp:TextBox ID="txtBiometricCode" runat="server" CssClass="textbox"></asp:TextBox>
                        </td>
                        <td class="auto-style1">
                            &nbsp;</td>
                        <td style="width: 13%">Date of Birth</td>
                        <td style="width: 16%">

                            <asp:TextBox ID="txtDOB" runat="server" CssClass="textbox"></asp:TextBox>
                            <asp:CalendarExtender ID="txtDOB_CalendarExtender" runat="server" TargetControlID="txtDOB" Format="dd/MM/yyyy"></asp:CalendarExtender>
                            <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999" MaskType="Date" TargetControlID="txtDOB" PromptCharacter="_"> </asp:MaskedEditExtender>
                        </td>
                        <td align="center" style="text-align: left" class="auto-style2">&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 13%">Mother Name</td>
                        <td class="auto-style2">
                            <asp:TextBox ID="txtMName" runat="server" CssClass="textbox"></asp:TextBox>
                        </td>
                        <td class="auto-style1">
                            &nbsp;</td>
                        <td style="width: 13%">Father Name</td>
                        <td style="width: 16%">
                            <asp:TextBox ID="txtFName" runat="server" CssClass="textbox"></asp:TextBox>
                        </td>
                        <td align="center" style="text-align: left" class="auto-style2">&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 13%">Gender</td>
                        <td class="auto-style2">
                            <asp:DropDownList ID="cboGender" runat="server" CssClass="Dropdown">
                                <asp:ListItem>Male</asp:ListItem>
                                <asp:ListItem>Female</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="auto-style1">
                            &nbsp;</td>
                        <td style="width: 13%">Marital Status</td>
                        <td style="width: 16%">

                            <asp:DropDownList ID="cboMaritalStatus" runat="server" CssClass="Dropdown"></asp:DropDownList>
                        </td>
                        <td align="center" style="text-align: left" class="auto-style2">&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 13%">Caste</td>
                        <td class="auto-style2">

                            <asp:DropDownList ID="cboCaste" runat="server" CssClass="Dropdown"></asp:DropDownList>
                        </td>
                        <td class="auto-style1">

                            &nbsp;</td>
                        <td style="width: 13%">Religion</td>
                        <td style="width: 16%">

                            <asp:DropDownList ID="cboReligion" runat="server" CssClass="Dropdown"></asp:DropDownList>
                        </td>
                        <td align="center" style="text-align: left" class="auto-style2">&nbsp;</td>
                        <td>
                            <asp:FileUpload ID="myFile" runat="server" CssClass="FileUpload" TabIndex="-1" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 13%">Qualification</td>
                        <td class="auto-style2">
                            <asp:DropDownList ID="cboQualification" runat="server" CssClass="Dropdown"></asp:DropDownList>
                        </td>
                        <td class="auto-style1">
                            &nbsp;</td>
                        <td>Department</td>
                        <td>
                            <asp:DropDownList ID="cboDepartment" runat="server" CssClass="Dropdown"></asp:DropDownList>
                        </td>
                        <td align="center" style="text-align: left" class="auto-style2">
                            <asp:TextBox ID="txtCommPinCode" runat="server" CssClass="textbox" Visible="False"></asp:TextBox>
                        </td>
                        <td style="text-align: left">
                            <asp:Button ID="btnUpload" runat="server" Text="Upload" CssClass="btn btn-primary" TabIndex="-1" Width="100px" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 13%">Designation</td>
                        <td class="auto-style2">
                            <asp:DropDownList ID="cboDesignation" runat="server" CssClass="Dropdown"></asp:DropDownList></td>
                        <td class="auto-style1">
                            &nbsp;</td>
                        <td>Employee Category</td>
                        <td>
                            <asp:DropDownList ID="cboEmpCat" runat="server" CssClass="Dropdown"></asp:DropDownList></td>
                        <td align="center" style="text-align: left" class="auto-style2">Employee Type</td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="cboEmpType" runat="server" CssClass="Dropdown">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="7">

                            <div id="accordion" style="width: 100%">


                                <h3><a href="">Personal Details</a></h3>
                                <div>
                                    <table style="width: 100%">


                                        <tr>

                                            <td colspan="7">


                                                <table width="100%">

                                                    <tr>


                                                        <td style="width: 137px">Address</td>
                                                        <td class="td_width_16" colspan="3" rowspan="2">
                                                            <asp:TextBox ID="txtPerAdd" runat="server" CssClass="textbox" Height="40px" Width="517px" TextMode="MultiLine"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 172px">State</td>
                                                        <td>
                                                            <asp:DropDownList ID="cboPerState" runat="server" CssClass="Dropdown" AutoPostBack="true" ></asp:DropDownList>
                                                        </td>
                                                        
                                                    </tr>


                                                    <tr>


                                                        <td style="width: 137px">&nbsp;</td>
                                                        <td style="width: 172px">City / District</td>
                                                        <td>
                                                             <asp:DropDownList ID="cbocity" runat="server" CssClass="Dropdown" ></asp:DropDownList>
                                                            <asp:TextBox ID="txtPerCity" runat="server" CssClass="textbox" Visible="false" ></asp:TextBox>
                                                        </td>
                                                    </tr>


                                                    <tr>


                                                        <td style="width: 137px">Pincode</td>
                                                        <td class="td_width_16" style="width: 215px">
                                                            <asp:TextBox ID="txtPerPinCode" runat="server" CssClass="textbox"  onkeypress="return isNumber(event)"></asp:TextBox>
                                                        </td>
                                                        <td class="td_width_16" style="width: 149px">Mobile<span style="color: #CC0000">*</span></td>
                                                        <td class="td_width_4" style="width: 192px">
                                                            <asp:TextBox ID="txtMob" runat="server" CssClass="textbox"  onkeypress="return isNumber(event)"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 172px">Phone</td>
                                                        <td>
                                                            <asp:TextBox ID="txtPhone" runat="server" CssClass="textbox"  onkeypress="return isNumber(event)"></asp:TextBox>
                                                        </td>
                                                    </tr>


                                                    <tr>


                                                        <td style="width: 137px">Email</td>
                                                        <td class="td_width_16" style="width: 215px">
                                                            <asp:TextBox ID="txtEmail" runat="server" CssClass="textbox"></asp:TextBox>
                                                            
                                                        </td>
                                                        <td class="td_width_16" style="width: 149px">Webpage</td>
                                                        <td class="td_width_4" style="width: 192px">
                                                            <asp:TextBox ID="txtwebPage" runat="server" CssClass="textbox"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 172px">Nationality</td>
                                                        <td>
                                                            <asp:DropDownList ID="cboNationality" runat="server" CssClass="Dropdown"></asp:DropDownList>
                                                        </td>
                                                    </tr>

                                                    <tr>


                                                        <td style="width: 137px">Comm. Address</td>
                                                        <td class="td_width_16" colspan="3" rowspan="2">
                                                            <asp:TextBox ID="txtCommAdd" runat="server"
                                                                Height="40px" TextMode="MultiLine" Width="517px" CssClass="textbox"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 172px">City / District</td>
                                                        <td>
                                                            <asp:TextBox ID="txtCommCity" runat="server" CssClass="textbox"></asp:TextBox>
                                                        </td>
                                                    </tr>


                                                    <tr>


                                                        <td style="width: 137px">&nbsp;</td>
                                                        <td style="width: 172px">State</td>
                                                        <td>
                                                            <asp:DropDownList ID="cboCommState" runat="server" CssClass="Dropdown"></asp:DropDownList>
                                                        </td>
                                                    </tr>

                                                    <tr>


                                                        <td style="width: 137px">Aadhar No</td>
                                                        <td class="td_width_16" style="width: 215px">
                                                            <asp:TextBox ID="txtAadharNo" runat="server" CssClass="textbox"></asp:TextBox>
                                                        </td>
                                                        <td class="td_width_16" style="width: 149px">&nbsp;</td>
                                                        <td class="td_width_4" style="width: 192px">&nbsp;</td>
                                                        <td style="width: 172px">&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                </table>

                                            </td>
                                        </tr>

                                    </table>
                                </div>
                                <h3><a href="">Accounts Details</a> </h3>
                                <div>
                                    <table style="width: 100%">
                                        <tr>
                                            <td class="td_width_16" style="width: 13%">Date of Joining</td>
                                            <td style="width: 219px">
                                                <asp:TextBox ID="txtDOJ" runat="server" CssClass="textbox"></asp:TextBox>
                                                <asp:CalendarExtender ID="txtDOJ_CalendarExtender" runat="server" TargetControlID="txtDOJ" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                            <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999" MaskType="Date" TargetControlID="txtDOJ" PromptCharacter="_"> </asp:MaskedEditExtender>
                                            </td>
                                            <td style="width: 147px">Date of Increment</td>
                                            <td style="width: 194px">
                                                <asp:TextBox ID="txtDOI" runat="server" CssClass="textbox"></asp:TextBox>
                                                <asp:CalendarExtender ID="txtDOI_CalendarExtender" runat="server" TargetControlID="txtDOI" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                            <asp:MaskedEditExtender ID="MaskedEditExtender3" runat="server" Mask="99/99/9999" MaskType="Date" TargetControlID="txtDOI" PromptCharacter="_"> </asp:MaskedEditExtender>
                                            </td>
                                            <td style="width: 169px">Pay Scale</td>
                                            <td>
                                                <asp:DropDownList ID="cboGrade" runat="server" CssClass="Dropdown"></asp:DropDownList>
                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 13%">Grade Pay</td>
                                            <td style="width: 219px">

                                                <asp:DropDownList ID="cboGradePay" runat="server" CssClass="Dropdown"></asp:DropDownList>
                                            </td>
                                            <td style="width: 147px">Basic Pay</td>
                                            <td style="width: 194px">
                                                <asp:TextBox ID="txtBasicPay" runat="server" CssClass="textbox"></asp:TextBox>
                                            </td>
                                            <td class="td_width_4" style="width: 169px">Bank</td>
                                            <td class="td_width_4">
                                                <asp:DropDownList ID="cboBank" runat="server" CssClass="Dropdown"></asp:DropDownList>
                                            </td>
                                            <td class="td_width_4">&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 13%">A/C No.</td>
                                            <td style="width: 219px">
                                                <asp:TextBox ID="txtAccNo" runat="server" CssClass="textbox"></asp:TextBox>
                                            </td>
                                            <td style="width: 147px">PAN</td>
                                            <td class="td_width_16" style="width: 194px">
                                                <asp:TextBox ID="txtPAN" runat="server" CssClass="textbox"></asp:TextBox>
                                            </td>
                                            <td class="td_width_4" style="width: 169px">File No.</td>
                                            <td class="td_width_4">
                                                <asp:TextBox ID="txtFileNo" runat="server" CssClass="textbox"></asp:TextBox>
                                            </td>
                                            <td class="td_width_4">&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 13%">PF No.</td>
                                            <td style="width: 219px">
                                                <asp:TextBox ID="txtPFNo" runat="server" CssClass="textbox"></asp:TextBox>
                                            </td>
                                            <td style="width: 147px">&nbsp;</td>
                                            <td class="td_width_16" style="width: 194px">&nbsp;</td>
                                            <td class="td_width_4" style="width: 169px">&nbsp;</td>
                                            <td class="td_width_4">&nbsp;</td>
                                            <td class="td_width_4">&nbsp;</td>
                                        </tr>
                                    </table>
                                </div>

                            </div>
                        </td>

                        <tr>
                            <td style="width: 13%; height: 33px;">Remarks</td>
                            <td style="height: 33px;" colspan="4">
                                <asp:TextBox ID="txtRemarks" TextMode="MultiLine" Width="520px" runat="server" CssClass="textbox" Height="33px"></asp:TextBox>
                            </td>
                            <td class="auto-style3">Status</td>
                            <td width="17%" align="right" style="text-align: left; height: 33px;">
                                <asp:DropDownList ID="cboStatus" runat="server" CssClass="Dropdown"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td colspan="7">
                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" Width="100px"/>&nbsp;&nbsp;
                <asp:Button ID="btnNew" runat="server" Text="New" CssClass="btn btn-primary" Width="100px" />&nbsp;&nbsp;
                <asp:Button ID="btnDelete" runat="server" Text="Delete" Width="100px" CssClass="btn btn-primary" />&nbsp;&nbsp;
                                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT [EmpCode], [EmpName], [DeptName], [DesgName],SchoolName FROM [vw_Employees] Where EmpName Like '%@EmpName@%'">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="txtName" DefaultValue="" Name="EmpName" PropertyName="Text" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
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
        .auto-style1 {
            width: 3%;
        }
        .auto-style2 {
            width: 13%;
        }
        .auto-style3 {
            height: 33px;
            width: 13%;
        }
    </style>
</asp:Content>

