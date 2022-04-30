<%@ Page Title="Student Master" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="StudentMaster.aspx.vb" Inherits="iDiary_V3.StudentMaster" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Heading" runat="server">
    Student Master
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <%--  <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
   --%> 

    <link href="css/jquery-ui.css" rel="stylesheet" />
    <script src="js/jquery-ui.js"></script>
    <script type="text/javascript">
        function imgError(image) {
            image.onerror = "";
            image.src = "../../images/StudentDummy.jpg";
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
                        <td class="td_width_16" colspan="5">
                            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" AutoGenerateSelectButton="True" CssClass="Grid"
                                CellPadding="2" DataSourceID="SqlDataSource2" ShowHeader="False" Width="98%" Font-Names="Garamond" Font-Size="10pt">
                                <Columns>
                                    <asp:BoundField DataField="RegNo" HeaderText="RegNo" SortExpression="RegNo" />
                                    <asp:BoundField DataField="SName" HeaderText="SName" SortExpression="SName" />
                                    <asp:BoundField DataField="ClassName" HeaderText="ClassName" SortExpression="ClassName" />
                                    <asp:BoundField DataField="SecName" HeaderText="SecName" SortExpression="SecName" />
                                </Columns>

                            </asp:GridView>
                            <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
                        </td>
                    </tr>

                    <tr>
                       
                        <td class="auto-style1">School Name</td>
                        <td class="auto-style2" colspan="2">
                            <asp:DropDownList ID="cboSchoolName" OnSelectedIndexChanged ="cboSchoolName_SelectedIndexChanged"   runat="server" CssClass="Dropdown" Width="300px" AutoPostBack="true" ></asp:DropDownList>
                        </td>
                        <td class="td_width_16" align="left" valign="top" rowspan="7">
                            <asp:Image ID="imgPhoto" CssClass="textbox" runat="server" Height="155px" Width="149px" onerror="imgError(this);"/>
                             <br />
                            <br />
                             <asp:FileUpload ID="myFile" runat="server" CssClass="FileUpload" />
                            &nbsp;
                            <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-primary" Text="Upload" />
                        </td>
                    </tr>

                    <tr>
                       
                        <td class="auto-style1">Admin/Sr/Reg No.<span style="color: #CC0000">*</span></td>
                        <td class="auto-style2">
                            <asp:panel runat="server" DefaultButton="btnFind">
                            <asp:TextBox ID="txtSRNo" runat="server" CssClass="textbox"></asp:TextBox>
                            &nbsp;<asp:Button ID="btnFind" runat="server" CssClass="btn btn-sm btn-primary" Text="&gt;&gt;" />
                        </asp:panel>
                                </td>
                        <td class="td_width_16" style="width: 141px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Photo</td>
                    </tr>

                    <tr>
                        <td class="auto-style1">Admission Date<span style="color: #CC0000" __designer:mapid="1c9">*</span></td>
                        <td class="auto-style2">
                                                                    <asp:TextBox ID="txtAdmissionDate" runat="server" CssClass="textbox" placeholder="dd/mm/yyyy"></asp:TextBox>
                                                                    <asp:CalendarExtender ID="txtAdmissionDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="txtAdmissionDate" />
                        <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999" MaskType="Date" TargetControlID="txtAdmissionDate" PromptCharacter="_"> </asp:MaskedEditExtender>
                       <%-- <asp:MaskedEditValidator ID="MV_Date" runat="server" ControlToValidate="txtAdmissionDate"
            ControlExtender="TextBox1_MaskedEditExtender" Mask="99/99/9999" MaskType="Date"
           />--%>
                        </td>
                       
                        <td class="td_width_16" style="width: 141px">&nbsp;</td>
                    </tr>
                    <tr>
                         <td class="auto-style1">Student Name<span style="color: #CC0000">*</span></td>
                        <td class="td_width_16" style="width: 226px">

                            <asp:TextBox ID="txtName" runat="server"
                                CssClass="textbox"></asp:TextBox>
                            <asp:AutoCompleteExtender ID="txtName_AutoCompleteExtender" runat="server" MinimumPrefixLength="1" ServiceMethod="ShowNames" TargetControlID="txtName" CompletionSetCount="1" CompletionInterval="100" EnableCaching="False" ServicePath="~">
                            </asp:AutoCompleteExtender>
                            &nbsp;<asp:Button ID="btnNameSearch" runat="server" CssClass="btn btn-sm btn-primary" Text="&gt;&gt;" />
                        </td>
                         <td>&nbsp;</td>
                    </tr>
                    <tr>
                         <td class="auto-style1">Gender<span style="color: #CC0000" __designer:mapid="2e8">*</span></td>
                        <td class="td_width_16" style="width: 226px">

                            <asp:DropDownList ID="cboGender" runat="server" CssClass="Dropdown">
                                <asp:ListItem>Male</asp:ListItem>
                                <asp:ListItem>Female</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                         <td>&nbsp;</td>
                    </tr>
                    <tr>
                         <td class="auto-style1">Father Name<span style="color: #CC0000">*</span></td>
                        <td class="td_width_16" style="width: 226px">
                            <asp:TextBox ID="txtFathername" runat="server" CssClass="textbox"></asp:TextBox>
                        </td>
                         <td></td>
                    </tr>
                    <tr>
                         <td class="auto-style1">Mother Name<span style="color: #CC0000">*</span></td>
                        <td class="td_width_16">
                            <asp:TextBox ID="txtMotherName" runat="server" CssClass="textbox"></asp:TextBox>
                        </td>
                         <td></td>
                    </tr>
                    <tr>
                         <td class="auto-style1">Date of Birth<span style="color: #CC0000">*</span></td>
                                            <td class="td_width_16" style="width: 207px">
                                                <asp:TextBox ID="txtDOB" runat="server" placeholder="dd/mm/yyyy"
                                                    CssClass="textbox" AutoPostBack="True"></asp:TextBox>
                                                <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999" MaskType="Date" TargetControlID="txtDOB" PromptCharacter="_"> </asp:MaskedEditExtender>
                                                <asp:CalendarExtender ID="txtDOB_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDOB"></asp:CalendarExtender>
                                                <br />
                             <asp:Label ID="lblAgeOn" runat="server" Style="font-weight: 700"></asp:Label>
                                                                
                                                                    <asp:Label ID="lblAge" runat="server"></asp:Label>
                                                               </td>
                        
                                            <td class="td_width_16" style="width: 207px">
                                                Mobile No<span style="color: #CC0000">*</span></td>
                        
                         <td>
                            <asp:TextBox ID="txtMobile" runat="server" CssClass="textbox"></asp:TextBox>
                         </td>
                        
                    </tr>
                    <tr>
                        <td class="auto-style1">Address<span style="color: #CC0000">*</span></td>
                        <td class="td_width_16" rowspan="4">
                            <asp:TextBox ID="txtAddressFather" runat="server" Width="307px" CssClass="textbox" Height="100px" TextMode="MultiLine"></asp:TextBox>
                        </td>
                        <td class="td_width_16">
                            State<span style="color: #CC0000">*</span>&nbsp;&nbsp;</td>
                        <td class="td_width_16">
                            <asp:DropDownList ID="cboState" runat="server" CssClass="Dropdown" AutoPostBack="true" ></asp:DropDownList>
                            <asp:ImageButton ID="ImageButton19" runat="server"
                                ImageUrl="~/images/Refresh.jpg" />
                        </td>
                    </tr>
                    <tr>
                         <td class="auto-style1">&nbsp;</td>
                        <td class="td_width_16">
                            PinCode</td>
                        <td class="td_width_16">
                            <asp:TextBox ID="txtPincode" runat="server" CssClass="textbox" TextMode="Number"></asp:TextBox>
                         </td>
                        <td class="td_width_16">
                            &nbsp;</td>
                    </tr>
                    <tr>
                         <td class="auto-style3"></td>
                        <td class="auto-style4">
                            District</td>
                        <td class="auto-style4">
                            <asp:DropDownList ID="cbocity"  runat="server" CssClass="Dropdown" ></asp:DropDownList>
                            <asp:TextBox ID="txtDistrict" runat="server" CssClass="textbox" Visible="false" ></asp:TextBox>
                         </td>
                        <td class="auto-style4">
                            </td>
                    </tr>
                      <tr>
                         <td class="auto-style3"></td>
                        <td class="auto-style4">
                           Religion<span style="color: #CC0000">*</span> </td>
                        <td class="auto-style4">
                              <asp:DropDownList ID="cboRel" runat="server" CssClass="Dropdown">
                                                                    </asp:DropDownList>
                         </td>
                        <td class="auto-style4">
                            </td>
                    </tr>
                    <tr>
                        <td class="auto-style1">Class<span style="color: #CC0000" __designer:mapid="257">*</span>
                                                                </td>
                        <td class="td_width_16">
                                                                    <asp:DropDownList ID="cboClass" runat="server" AutoPostBack="True"
                                                                        CssClass="Dropdown">
                                                                    </asp:DropDownList>
                                                                    <asp:ImageButton ID="ImageButton1" runat="server"
                                                                        ImageUrl="~/images/Refresh.jpg" Height="16px" Visible="False" />
                        </td>
                        <td>Section<span style="color: #CC0000" __designer:mapid="25c">*
                                                                </td>
                        <td>
                                                                    <asp:DropDownList ID="cboSection" runat="server" CssClass="Dropdown" AutoPostBack="True"></asp:DropDownList>
                                                                    <span style="color: #CC0000" __designer:mapid="260">
                                                                        <asp:ImageButton ID="ImageButton2" runat="server"
                                                                            ImageUrl="~/images/Refresh.jpg" Height="16px" Visible="False" />
                                                                </td>
                    </tr>
                    <tr>
                        <td class="auto-style1">Sub-Section</td>
                        <td class="td_width_16" style="width: 210px">
                                                                    <asp:DropDownList ID="cboSubSection" runat="server" CssClass="Dropdown"></asp:DropDownList>
                                                                    <asp:ImageButton ID="ImageButton24" runat="server"
                                                                        ImageUrl="~/images/Refresh.jpg" Style="height: 14px" />
                        </td>
                        <td>Strength</td>
                        <td>
                                                <asp:Label ID="lblSecStrength" runat="server" Style="font-weight: 700"></asp:Label>
                                            </td>
                    </tr>
                    <tr>
                        <td class="auto-style1">Status<span style="color: #CC0000">*</span></td>
                        <td class="td_width_16" style="width: 210px">
                                                <asp:DropDownList ID="cboStatus" runat="server" CssClass="Dropdown"></asp:DropDownList>
                                                <asp:ImageButton ID="ImageButton18" runat="server"
                                                    ImageUrl="~/images/Refresh.jpg" Style="height: 14px" />
                        </td>
                        <td>
                            Board Registration Number</td>
                        <td>
                            <asp:TextBox ID="txtboardRollNO" runat="server" CssClass="textbox"></asp:TextBox>
                            </td>
                    </tr>
                    <tr>
                        <td class="auto-style1">&nbsp;</td>
                        <td class="td_width_16" style="width: 210px">
                                                &nbsp;</td>
                        <td>
                            <asp:Button ID="btnSave" runat="server"  Text="Submit For Authorize" CssClass="btn btn-primary" />
                            </td>
                        <td>
                                                &nbsp;</td>
                    </tr>
                    </table>

                <table class="table">
                    <tr>
                        <td colspan="6">
                             <div id="accordion" style="width: 100%">

                                <h3><a href="">Other Details</a></h3>
                                <div>
                                    <table style="width: 100%; font-size:1.25em; " class="table">

                                        <tr>

                                            <td colspan="8">

                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                                        <table class="table">
                                                            <tr>
                                                                <td>Last Admission No</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtLastSR" runat="server" CssClass="textbox" ReadOnly="True"></asp:TextBox>
                                                                </td>
                                                                <td>Form No</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtFormNo" runat="server" CssClass="textbox"></asp:TextBox>
                                                                </td>
                                                                <td>Class Roll No</td>
                                                                <td><asp:TextBox ID="txtClassRollNo" runat="server"
                                                                        CssClass="textbox"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td>Fee Book No</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtFeeBookNo" runat="server" CssClass="textbox"></asp:TextBox>
                                                                </td>
                                                                <td>Admission Receipt No.</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtReceiptNo" runat="server" CssClass="textbox"></asp:TextBox>
                                                                    <asp:TextBox ID="txtSID" runat="server" Visible="False" Width="28px"></asp:TextBox>
                                                                    <asp:TextBox ID="txtStudentID" runat="server" Visible="False" Width="28px"></asp:TextBox>
                                                                </td>
                                                                <td> </td>
                                                                <td>
                                                                    <asp:DropDownList ID="cboRel1" Visible ="false"  runat="server" CssClass="Dropdown">
                                                                    </asp:DropDownList>
                                                                    <asp:ImageButton ID="ImageButton3" runat="server" Visible ="false"  ImageUrl="~/images/Refresh.jpg" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Caste<span style="color: #CC0000">*</span> </td>
                                                                <td>
                                                                    <asp:DropDownList ID="cboCaste" runat="server" CssClass="Dropdown">
                                                                    </asp:DropDownList>
                                                                    <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/images/Refresh.jpg" />
                                                                </td>
                                                                <td>Category<span style="color: #CC0000">*</span></td>
                                                                <td>
                                                                    <asp:DropDownList ID="cboCategory" runat="server" CssClass="Dropdown" >
                                                                    </asp:DropDownList>
                                                                    <asp:ImageButton ID="ImageButton17" runat="server" ImageUrl="~/images/Refresh.jpg" Style="width: 17px" />
                                                                </td>
                                                                <td>Blood Group<span style="color: #CC0000">*</span></td>
                                                                <td>
                                                                    <asp:DropDownList ID="cboBloodGroup" runat="server" CssClass="Dropdown">
                                                                    </asp:DropDownList>
                                                                    <asp:ImageButton ID="ImageButton25" runat="server" ImageUrl="~/images/Refresh.jpg" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Mother Tongue<span style="color: #CC0000">*</span></td>
                                                                <td>
                                                                    <asp:DropDownList ID="cboMotherTongue" runat="server" CssClass="Dropdown">
                                                                    </asp:DropDownList>
                                                                    <span style="color: #CC0000">
                                                                    <asp:ImageButton ID="ImageButton21" runat="server" ImageUrl="~/images/Refresh.jpg" Width="17px" />
                                                                    </span></td>
                                                                <td>Phone(Residence)</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtPhoneResd" runat="server" CssClass="textbox"></asp:TextBox>
                                                                </td>
                                                                <td>Aadhar No.</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtAadharNo" runat="server" CssClass="textbox"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Any Siblings<span style="color: #CC0000">*</span></td>
                                                                <td>
                                                                    <asp:DropDownList ID="cboOnlyChild" runat="server" CssClass="Dropdown">
                                                                        <asp:ListItem>No</asp:ListItem>
                                                                        <asp:ListItem>Yes</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td>House<span style="color: #CC0000">*</span> </td>
                                                                <td>
                                                                    <asp:DropDownList ID="cboHouse" runat="server" CssClass="Dropdown">
                                                                    </asp:DropDownList>
                                                                    <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/images/Refresh.jpg" />
                                                                </td>
                                                                <td>Nationality </td>
                                                                <td>
                                                                    <asp:DropDownList ID="cboNationality" runat="server" CssClass="Dropdown">
                                                                    </asp:DropDownList>
                                                                    <span style="color: #CC0000">
                                                                    <asp:ImageButton ID="ImageButton26" runat="server" ImageUrl="~/images/Refresh.jpg" Width="17px" />
                                                                    </span></td>
                                                            </tr>
                                                            <tr style="visibility:hidden">
                                                                
                                                                <td>&nbsp;</td>
                                                                <td>&nbsp;</td>
                                                                <td>&nbsp;</td>
                                                                <td>&nbsp;</td>
                                                            </tr>
                                                        </table>
                                                        
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>

                                       
                                        <tr>
                                            <td colspan="6">
                                                <asp:GridView ID="gvSb" runat="server" AutoGenerateColumns="False" CssClass="Grid" DataSourceID="SqlDataSourcesiblings" ShowHeader="true" Width="98%">
                                                    <Columns>
                                                        <asp:BoundField DataField="RegNo" HeaderText="Reg No" SortExpression="RegNo" />
                                                        <asp:BoundField DataField="SName" HeaderText="Name" SortExpression="SName" />
                                                        <asp:BoundField DataField="ClassName" HeaderText="Class" SortExpression="ClassName" />
                                                        <asp:BoundField DataField="SecName" HeaderText="Sec" SortExpression="SecName" />
                                                        <asp:BoundField DataField="FName" HeaderText="Father" SortExpression="FName" />
                                                        <asp:BoundField DataField="MobNo" HeaderText="Mobile" SortExpression="MobNo" />
                                                        <%--<asp:BoundField DataField="CSSID" HeaderText="CSSID" SortExpression="CSSID" />--%>
                                                    </Columns>

                                                </asp:GridView>
                                                <asp:SqlDataSource ID="SqlDataSourcesiblings" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="sELECT RegNo, SName, ClassName, SecName,FName,MobNo FROM vw_Student where SID<0"></asp:SqlDataSource>

                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>
                                    </table>
                                </div>
                                <h3><a href="">Father Details</a></h3>
                                <div>
                                    <table style="width: 100%;font-size:1.25em;" class="table">
                                        <tr>
                                            <td class="td_width_16" style="width: 15%">Occupation<span style="color: #CC0000" __designer:mapid="21">*</span></td>
                                            <td style="width: 210px">
                                                <asp:DropDownList ID="cboFOcc" runat="server" CssClass="Dropdown"></asp:DropDownList>
                                                <asp:ImageButton ID="ImageButton9" runat="server"
                                                    ImageUrl="~/images/Refresh.jpg" />
                                            </td>
                                            <td style="width: 150px">Dept/Business<span style="color: #CC0000" __designer:mapid="21">*</span></td>
                                            <td style="width: 200px">
                                                <asp:DropDownList ID="cboFDept" runat="server" CssClass="Dropdown"></asp:DropDownList>
                                                <span style="color: #CC0000" __designer:mapid="21">
                                                    <asp:ImageButton ID="ImageButton10" runat="server"
                                                        ImageUrl="~/images/Refresh.jpg" Style="height: 14px" />
                                                </span>
                                            </td>
                                            <td>Designation<span style="color: #CC0000" __designer:mapid="21">*</span></td>
                                            <td>
                                                <asp:DropDownList ID="cboFPost" runat="server" CssClass="Dropdown"></asp:DropDownList>
                                                
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="ImageButton11" runat="server"
                                                    ImageUrl="~/images/Refresh.jpg" Style="height: 14px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 15%">Employee No</td>
                                            <td style="width: 210px">
                                                <asp:TextBox ID="txtFEmpNo" runat="server"
                                                    CssClass="textbox"></asp:TextBox>
                                            </td>
                                            <td style="width: 150px">Phone(Office)</td>
                                            <td style="width: 200px">
                                                <asp:TextBox ID="txtPhoneOffice" runat="server" CssClass="textbox"></asp:TextBox>
                                            </td>
                                            <td class="td_width_4">Email</td>
                                            <td class="td_width_4">
                                                <asp:TextBox ID="txtEmail" runat="server" CssClass="textbox"></asp:TextBox>
                                            </td>
                                            <td class="td_width_4">&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 15%">Annual Income</td>
                                            <td style="width: 210px">
                                                <asp:TextBox ID="txtFIncome" runat="server"
                                                    CssClass="textbox"></asp:TextBox>
                                            </td>
                                            <td style="width: 150px">&nbsp;</td>
                                            <td class="td_width_16" style="width: 200px">&nbsp;</td>
                                            <td class="td_width_4">&nbsp;</td>
                                            <td class="td_width_4">&nbsp;</td>
                                            <td class="td_width_4">&nbsp;</td>
                                        </tr>
                                    </table>
                                </div>

                                <h3><a href="">Mother Details</a></h3>
                                <div>
                                    <table class="table" style="width:100%;font-size:1.25em;">
                                        <tr>
                                            <td style="width: 15%">Occupation<span style="color: #CC0000" __designer:mapid="21">*</span></td>
                                            <td style="width: 210px">
                                                <asp:DropDownList ID="cboMOcc" runat="server" CssClass="Dropdown"></asp:DropDownList>
                                                <asp:ImageButton ID="ImageButton13" runat="server"
                                                    ImageUrl="~/images/Refresh.jpg" Style="width: 17px" />
                                            </td>
                                            <td style="width: 150px">Dept/Business<span style="color: #CC0000" __designer:mapid="21">*</span></td>
                                            <td class="td_width_16" style="width: 200px">
                                                <asp:DropDownList ID="cboMDept" runat="server" CssClass="Dropdown"></asp:DropDownList>
                                                <asp:ImageButton ID="ImageButton14" runat="server"
                                                    ImageUrl="~/images/Refresh.jpg" />
                                            </td>
                                            <td class="td_width_4">Designation<span style="color: #CC0000" __designer:mapid="21">*</span></td>
                                            <td class="td_width_4">
                                                <asp:DropDownList ID="cboMPost" runat="server" CssClass="Dropdown"></asp:DropDownList>
                                                 
                                            </td>
                                            <td>
<asp:ImageButton ID="ImageButton15" runat="server"
                                                    ImageUrl="~/images/Refresh.jpg" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 15%">Employee No</td>
                                            <td style="width: 210px">
                                                <asp:TextBox ID="txtMEmpNo" runat="server"
                                                    CssClass="textbox"></asp:TextBox>
                                            </td>
                                            <td style="width: 150px">Phone(Office)</td>
                                            <td class="td_width_16" style="width: 200px">
                                                <asp:TextBox ID="txtPhoneOfficeM" runat="server" CssClass="textbox"></asp:TextBox>
                                            </td>
                                            <td class="td_width_4">Email</td>
                                            <td class="td_width_4">
                                                <asp:TextBox ID="txtEmailM" runat="server" CssClass="textbox"></asp:TextBox>
                                            </td>
                                            <td class="td_width_4">&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 15%">Annual Income</td>
                                            <td style="width: 210px">
                                                <asp:TextBox ID="txtMIncome" runat="server"
                                                    CssClass="textbox"></asp:TextBox>
                                            </td>
                                            <td style="width: 150px">&nbsp;</td>
                                            <td class="td_width_16" style="width: 200px">&nbsp;</td>
                                            <td class="td_width_4">&nbsp;</td>
                                            <td class="td_width_4">&nbsp;</td>
                                            <td class="td_width_4">&nbsp;</td>
                                        </tr>
                                    </table>
                                </div>

                                <h3><a href="">Guardian Details</a></h3>
                                <div>
                                    <table class="table" style="width:100%;font-size:1.25em;">
                                        <tr>
                                            <td class="td_width_16" style="width: 15%; height: 18px;">Name</td>
                                            <td class="td_width_16" style="height: 18px; width: 225px;">
                                                <asp:TextBox ID="txtGuardianName" runat="server" CssClass="textbox"></asp:TextBox>
                                            </td>
                                            <td style="height: 18px; width: 150px;">Relation</td>
                                            <td class="td_width_16" style="height: 18px; width: 241px;">
                                                <asp:TextBox ID="txtGuardianRelation" runat="server" CssClass="textbox"></asp:TextBox>
                                            </td>
                                            <td class="td_width_4" style="height: 18px; width:150px">Contact No</td>
                                            <td class="td_width_4" style="height: 18px">
                                                <asp:TextBox ID="txtMobileGuardian" runat="server"
                                                    CssClass="textbox"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_width_16" style="width: 15%; height: 18px;">Address</td>
                                            <td class="td_width_16" style="height: 18px" colspan="3">
                                                <asp:TextBox ID="txtAddressGuardian" runat="server" Width="488px" CssClass="textbox" Height="51px" TextMode="MultiLine"></asp:TextBox>
                                            </td>
                                            <td class="td_width_4" style="height: 18px">&nbsp;</td>
                                            <td class="td_width_4" style="height: 18px">&nbsp;</td>
                                        </tr>
                                    </table>
                                </div>


                                <h3><a href="">Last School Details</a></h3>
                                <div>
                                    <table class="table" style="width:100%;font-size:1.25em;">
                                        <tr>
                                            <td class="td_width_16" style="width: 16%; height: 27px;">Last School Name</td>
                                            <td class="td_width_16" style="height: 27px; width: 222px;">
                                                <asp:TextBox ID="txtLastSchool" runat="server" CssClass="textbox"></asp:TextBox>
                                            </td>
                                            <td style="height: 27px; width: 151px;">Address Of School</td>
                                            <td class="td_width_16" colspan="3" style="height: 27px">
                                                <asp:TextBox ID="txtLastSchoolAddress" runat="server" CssClass="textbox" Width="446px"></asp:TextBox>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td class="td_width_16" style="width: 16%; height: 27px;">Board</td>
                                            <td class="td_width_16" style="height: 27px; width: 222px;">
                                                <asp:DropDownList ID="cboLastboard" runat="server" CssClass="Dropdown" >
                                                </asp:DropDownList>
                                            </td>
                                            <td style="height: 27px; width: 151px;">% Last Exam</td>
                                            <td class="td_width_16" style="height: 27px">
                                                <asp:TextBox ID="txtLastSchoolPercentage" runat="server" CssClass="textbox"></asp:TextBox>
                                            </td>
                                            <td class="td_width_4" style="height: 27px"></td>
                                            <td class="td_width_4" style="height: 27px">&nbsp;</td>
                                        </tr>
                                    </table>
                                </div>

                                
                            </div>
                        </td>
                    </tr>
                </table>
                
                <table border="0" cellpadding="1" cellspacing="2" class="Table_Font" style="width: 100%">


                    

                    <tr>
                        <td class="td_width_16" colspan="6">
                            <asp:CheckBox ID="chkAddmissionSlip" Text="Print Admission Slip" runat="server" Visible="false"/>
                            &nbsp;&nbsp;
                <asp:Button ID="btnRemove" runat="server" Width="100px" Text="Remove" CssClass="btn btn-primary" Visible="False" />
                            &nbsp;&nbsp;
                <asp:Button ID="btnNew" runat="server" Width="100px" Text="New" CssClass="btn btn-primary" Visible="False" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnAdmissionSlip" runat="server" Text="Print Admission Slip" CssClass="btn btn-primary" Width="180px" Visible="False" />
                            &nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnBonafide" runat="server" Text="Print Bonafide Certificate" CssClass="btn btn-primary" Width="180px" Visible="False" />
                        </td>
                    </tr>


                    <tr>
                        <td class="td_width_16" colspan="6">

                            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" Height="308px" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="699px" Visible="False">
                                <LocalReport ReportEmbeddedResource="iDiary_v2.rptAddmissionSlip.rdlc">
                                    <DataSources>
                                        <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="DataSet1" />
                                    </DataSources>
                                </LocalReport>
                            </rsweb:ReportViewer>
                            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData" TypeName="iDiaryDataSetTableAdapters.vw_StudentTableAdapter"></asp:ObjectDataSource>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server"
                                ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>"
                                DeleteCommand="DELETE FROM [TempSiblings] WHERE [SibID] = @SibID"
                                SelectCommand="SELECT [SibName], [SibClass], [SibSec], [SibID] FROM [TempSiblings]" InsertCommand="INSERT INTO [TempSiblings] ([SibName], [SibClass], [SibSec]) VALUES (@SibName, @SibClass, @SibSec)" UpdateCommand="UPDATE [TempSiblings] SET [SibName] = @SibName, [SibClass] = @SibClass, [SibSec] = @SibSec WHERE [SibID] = @SibID">
                                <DeleteParameters>
                                    <asp:Parameter Name="SibID" Type="Int32" />
                                </DeleteParameters>
                                <InsertParameters>
                                    <asp:Parameter Name="SibName" Type="String" />
                                    <asp:Parameter Name="SibClass" Type="String" />
                                    <asp:Parameter Name="SibSec" Type="String" />
                                </InsertParameters>
                                <UpdateParameters>
                                    <asp:Parameter Name="SibName" Type="String" />
                                    <asp:Parameter Name="SibClass" Type="String" />
                                    <asp:Parameter Name="SibSec" Type="String" />
                                    <asp:Parameter Name="SibID" Type="Int32" />
                                </UpdateParameters>
                            </asp:SqlDataSource>

                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT RegNo, SName, ClassName, SecName FROM vw_Student WHERE SID<0"></asp:SqlDataSource>
                        </td>
                    </tr>

                </table>
            </div>
              <asp:TextBox ID="txtSenderID" CssClass="textbox" Enabled ="false" Visible ="false"   runat="server"  Height="154px" 
                     Width="95%" BorderWidth="1px"></asp:TextBox>
        </div>
        <div class="clearfix"></div>
    </div>
</asp:Content>
<asp:Content ID="Content3" runat="server" contentplaceholderid="head">
   
    <style type="text/css">
        .auto-style1 {
            width: 228px;
        }
        .auto-style2 {
        }
        .auto-style3 {
            width: 228px;
            height: 38px;
        }
        .auto-style4 {
            height: 38px;
        }
    </style>
   
</asp:Content>

