<%@ Page Title="Student Search Wizard" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="StudentSearch.aspx.vb" Inherits="iDiary_V3.StudentSearch" EnableEventValidation="false" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Heading" runat="server">
    Student Search Wizard
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%-- <br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />--%>

     <div class="col_3" style="margin-top: 20px;">
        	<div class="col-md-3 widget widget1" style="width:100%">
                <div class="r3_counter_box">
                    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table class="table">
                                <tr>
                                    <td>
                                        School Name</td>
                                    <td colspan="2" >
                                        <asp:DropDownList ID="cboSchoolName" runat="server" AutoPostBack="true" CssClass="Dropdown" Width="300px">
                                        </asp:DropDownList>
                                    </td>

                                    <td >
                                        &nbsp;</td>

                                    <td >
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>

                                    
                                </tr>

                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkByName" runat="server" Text="Student Name" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtByName" runat="server" CssClass="textbox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkbyCode" runat="server" Text="Fee Book No" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtByCode" runat="server" CssClass="textbox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkByReg" runat="server" Text="Admission No." />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtByReg" runat="server" CssClass="textbox"></asp:TextBox>
                                    </td>
                                </tr>

                                <tr>
                                    <td >
                                        <asp:CheckBox ID="chkClass" runat="server" Text="Class" />
                                    </td>
                                    <td >
                                        <asp:DropDownList ID="cboClass" runat="server" AutoPostBack="True" CssClass="Dropdown">
                                        </asp:DropDownList>
                                    </td>

                                    <td >
                                        <asp:CheckBox ID="chkSection" runat="server" Text="Section" />
                                    </td>
                                    <td >
                                        <asp:DropDownList ID="cboSection" runat="server" AutoPostBack="True" CssClass="Dropdown">
                                        </asp:DropDownList>
                                    </td>

                                    <td >
                                        <asp:CheckBox ID="chkSubSection" runat="server" Text="Sub Section" />
                                    </td>
                                    <td >
                                        <asp:DropDownList ID="cboSubSection" runat="server" CssClass="Dropdown">
                                        </asp:DropDownList>
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkFName" runat="server" Text="Father Name" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFName" runat="server" CssClass="textbox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkHouse" runat="server" Text="House" />
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="cboHouse" runat="server" CssClass="Dropdown">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkReligion" runat="server" Text="Religion" />
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="cboReligion" runat="server" CssClass="Dropdown">
                                        </asp:DropDownList>
                                    </td>
                                </tr>

                                <tr>
                                    <td >
                                        <asp:CheckBox ID="chkCaste" runat="server" Text="Caste" />
                                    </td>
                                    <td >
                                        <asp:DropDownList ID="cboCaste" runat="server" CssClass="Dropdown">
                                        </asp:DropDownList>
                                    </td>

                                    <td >
                                        <asp:CheckBox ID="chkGender" runat="server" Text="Gender" />
                                    </td>
                                    <td >
                                        <asp:DropDownList ID="cboGender" runat="server" CssClass="Dropdown">
                                            <asp:ListItem>Male</asp:ListItem>
                                            <asp:ListItem>Female</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>

                                    <td >
                                        <asp:CheckBox ID="chkStatus" runat="server" Text="Status" />
                                    </td>
                                    <td >
                                        <asp:DropDownList ID="cboStatus" runat="server" CssClass="Dropdown">
                                        </asp:DropDownList>
                                    </td>

                                    
                                </tr>

                                <tr>
                                    <td >
                                        <asp:CheckBox ID="chkFDept" runat="server" Text="Father's Department" />
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="cboFDept" runat="server" CssClass="Dropdown">
                                        </asp:DropDownList>
                                    </td>

                                    <td>
                                        <asp:CheckBox ID="chkCategory" runat="server" Text="Category" />
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="cboCategory" runat="server" CssClass="Dropdown">
                                        </asp:DropDownList>
                                    </td>

                                    <td>
                                        <asp:CheckBox ID="chkmobno" runat="server" Text="Mobile No" />
                                    </td>
                                    <td >
                                        <asp:TextBox ID="txtmobno" runat="server" CssClass="textbox"></asp:TextBox>
                                    </td>

                                </tr>

                                <tr>
                                    <td >
                                        <asp:CheckBox ID="chkAdminDate" runat="server" Text="Admission Date From" />
                                    </td>
                                    <td >
                                        <asp:TextBox ID="txtAdminDateFrom" runat="server" CssClass="textbox"></asp:TextBox>
                                        <asp:CalendarExtender ID="txtAdminDateFrom_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="txtAdminDateFrom" />
                                        <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999" MaskType="Date" TargetControlID="txtAdminDateFrom" PromptCharacter="_"> </asp:MaskedEditExtender>
                                    </td>

                                    <td >
                                        <asp:Label ID="lblADate" runat="server" Text="Admission Date To"></asp:Label>
                                        </td>
                                    <td >
                                        <asp:TextBox ID="txtAdminDateTo" runat="server" CssClass="textbox"></asp:TextBox>
                                        <asp:CalendarExtender ID="txtAdminDateTo_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="txtAdminDateTo" />
                                        <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999" MaskType="Date" TargetControlID="txtAdminDateTo" PromptCharacter="_"> </asp:MaskedEditExtender>
                                    </td>

                                    <td ><asp:CheckBox ID="chkFatherOccupation" runat="server" Text="Father Occupation" /></td>
                                    <td >
                                        <asp:DropDownList ID="cboFatherOccupation" runat="server" CssClass="Dropdown">
                                        </asp:DropDownList>
                                    </td>

                                    
                                </tr>

                                <tr>
                                    <td>&nbsp;</td>
                                    <td >&nbsp;</td>

                                    <td >&nbsp;</td>
                                    <td > &nbsp;</td>

                                    <td >&nbsp;</td>
                                    <td style="float:right">
                                        <asp:Button ID="btnSearch" runat="server" class="btn btn-primary" Text="Search" Width="100px" /></td>

                                   </tr>
                            </table>
                        </ContentTemplate>
                        <Triggers>

                            <asp:PostBackTrigger ControlID="btnSearch" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
        	</div>
        	<div class="clearfix"> </div>
      </div>
    
    
    <div class="col_3" style="margin-top: 20px;">
        <div class="col-md-3 widget widget1" style="width: 100%">
            <div class="r3_counter_box">

                <table class="table">
                    <tr>
                        <td>
                            <asp:Panel runat="server" ID="panelSearcCrit">
                                <table width="100%" cellpadding="1" cellspacing="1" border="0" class="Table_Font">
                                    <tr>
                                        <td colspan="4"><h3>Choose Display Criteria :</h3></td>

                                    </tr>

                                    <tr>
                                        <td >
                                            <asp:CheckBox ID="chkSchoolNameC" runat="server" Checked="True" Text="School Name" />
                                        </td>
                                        <td >
                                            <asp:CheckBox ID="chkAdmnNoC" runat="server" Checked="True" Text="Admission No." />
                                        </td>

                                        <td >
                                            <asp:CheckBox ID="chkSNameC" runat="server" Checked="True" Text="Student Name" />
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chkFNameC" runat="server" Checked="True" Text="Father Name" />
                                        </td>

                                        <td >
                                            <asp:CheckBox ID="chkMNameC" runat="server" Checked="True" Text="Mother Name" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                        <td >
                                            <asp:CheckBox ID="chkClassC" runat="server" Checked="True" Text="Class" />
                                        </td>

                                        
                                    </tr>

                                    <tr>
                                        <td >
                                            <asp:CheckBox ID="chkSectionC" runat="server" Checked="True" Text="Section" />
                                        </td>
                                        <td >
                                            <asp:CheckBox ID="chkFBnoC" runat="server" Checked="True" Text="Fee Book No." />
                                        </td>

                                        <td >
                                            <asp:CheckBox ID="chkDobC" runat="server" Checked="True" Text="Date of Birth" />
                                        </td>
                                        <td >
                                            <asp:CheckBox ID="chkAddressC" runat="server" Checked="True" Text="Address" />
                                        </td>

                                        <td >
                                            <asp:CheckBox ID="chkPhoneC" runat="server" Checked="True" Text="Phone" />
                                        </td>
                                        <td >
                                            <asp:CheckBox ID="chkMobileC" runat="server" Checked="True" Text="Mobile" />
                                        </td>

                                        
                                    </tr>

                                    <tr>
                                        <td >
                                            <asp:CheckBox ID="chkAdmissionDateC" runat="server" Checked="True" Text="Admission Date" />
                                        </td>
                                        <td >
                                            <asp:CheckBox ID="chkRollNo" runat="server" Checked="True" Text="Roll No." />
                                        </td>

                                        <td>

                                           
                                            <asp:CheckBox ID="chkCategoryC" runat="server" Checked="True" Text="Category" />

                                           
                                        </td>

                                        <td>
                                            <asp:CheckBox ID="chkReligionC" runat="server" Checked="True" Text="Religion" />
                                        </td>

                                        <td > <asp:CheckBox ID="chkhouseC" runat="server" Checked="True" Text="HouseName" />
</td>
                                        <td >&nbsp;</td>

                                        
                                    </tr>


                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblStatus" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                        </td>

                        <td width="10%" align="right">&nbsp;</td>
                    </tr>
                    <tr>
            <td colspan="2" style="height: 472px" valign="top">
                <div id="gvDiv" style="width: 1000px; max-height: 1000px; overflow-x: scroll; text-align: center;">
                    <center>
                         <asp:Label ID="lblTotalRecords" runat="server" Style="font-weight: 700"></asp:Label>
                        <br />
                        <asp:Label ID="lblSchoolName" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
                        <br />
                        <asp:Label ID="lblTitle" runat="server" Font-Bold="True" Font-Size="Small"></asp:Label>
                        <br />
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                            CssClass="Grid" DataSourceID="SqlDataSource1" Width="98%">

                            <Columns>
                                <asp:CommandField ShowSelectButton="True" />
                                <asp:BoundField DataField="ClassRollNo" HeaderText="Roll No" SortExpression="ClassRollNo" />
                                <asp:BoundField DataField="RegNo" HeaderText="Admn No." SortExpression="RegNo" />
                                <asp:BoundField DataField="SName" HeaderText="Name" SortExpression="SName" />
                                <asp:BoundField DataField="FName" HeaderText="Father Name"
                                    SortExpression="FName" />
                                <asp:BoundField DataField="MName" HeaderText="Mother Name"
                                    SortExpression="MName" />
                                <asp:BoundField DataField="SchoolName" HeaderText="School Name"
                                    SortExpression="SchoolName" />
                                <asp:BoundField DataField="ClassName" HeaderText="Class"
                                    SortExpression="ClassName" />
                                <asp:BoundField DataField="SecName" HeaderText="Section"
                                    SortExpression="SecName" />
                                <asp:BoundField DataField="FeeBookNo" HeaderText="Fee Book No."
                                    SortExpression="FeeBookNo" />
                                <asp:BoundField DataField="CategoryName" HeaderText="Category"
                                    SortExpression="CategoryName" />
                                <asp:BoundField DataField="RelName" HeaderText="Religion"
                                    SortExpression="RelName" />
                                <asp:BoundField DataField="CasteName" HeaderText="Caste"
                                    SortExpression="CasteName" />
                                 <asp:BoundField DataField="Gender" HeaderText="Gender"
                                    SortExpression="Gender" />
                                <asp:BoundField DataField="HouseName" HeaderText="HouseName"
                                    SortExpression="HouseName" />
                                <asp:BoundField DataField="DOB" DataFormatString="{0:dd/MM/yyyy}"
                                    HeaderText="Date of Birth" HtmlEncode="False" SortExpression="DOB" />
                                <asp:BoundField DataField="FatherAddress" HeaderText="Address"
                                    SortExpression="Address" />
                                <asp:BoundField DataField="PhoneResd" HeaderText="Phone" SortExpression="PhoneResd" />
                                <asp:BoundField DataField="MobNo" HeaderText="Mobile" SortExpression="MobNo" />
                                <asp:BoundField DataField="AdmissionDate" HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Admission Date" SortExpression="AdmissionDate" />
                            </Columns>


                        </asp:GridView>

                        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False"
                            DataSourceID="SqlDataSource2" Width="400px" CssClass="Grid">

                            <Columns>
                                <asp:BoundField DataField="ClassRollNo" HeaderText="Roll No." SortExpression="ClassRollNo">
                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="RegNo" HeaderText="Admn No." SortExpression="RegNo">
                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SName" HeaderText="Name of the Student"
                                    SortExpression="SName">
                                    <ItemStyle HorizontalAlign="Center" Width="200px" />
                                </asp:BoundField>

                            </Columns>


                        </asp:GridView>
                    </center>
                </div>
                <br />
                <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="btn btn-primary" />
                &nbsp;&nbsp;
                <asp:Button ID="btnExcel" runat="server" Text="Export to Excel" CssClass="btn btn-primary" />
                <asp:SqlDataSource ID="SqlDataSource1" runat="server"
                    ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>"
                    SelectCommand="Select RegNo,ClassRollNo, SName, FName, MName, ClassName, SecName, FeeBookNo, FatherAddress, PhoneResd, DOB, MobNo, AdmissionDate,CategoryName,CASE WHEN Gender = 0 THEN 'Male' ELSE 'Female' END AS Gender,SchoolName, CasteName From vw_Student where sid<0"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server"
                    ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>"
                    SelectCommand="SELECT [Regno],[classrollno], [SName] FROM vw_Student where sid<0"></asp:SqlDataSource>
                
            </td>
        </tr>
                </table>

            </div>
        </div>
        <div class="clearfix"></div>
    </div>
   
</asp:Content>
<asp:Content ID="Content3" runat="server" contentplaceholderid="head">
</asp:Content>

