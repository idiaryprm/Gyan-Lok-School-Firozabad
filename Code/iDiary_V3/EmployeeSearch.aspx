<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" Inherits="iDiary_V3.EmployeeSearch" EnableEventValidation="false" Codebehind="EmployeeSearch.aspx.vb" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Heading" Runat="Server">
    Employee Search Wizard 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%--<br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />--%>
     <div class="col_3" style="margin-top: 20px;" id="panelEnquiryNo" runat="server">
        <div class="col-md-3 widget widget1" style="width: 100%">
            <div class="r3_counter_box">
                <table class="table">
                    <tr>
                        <td height="25" style="width: 18%">
                            <asp:CheckBox ID="chkByName" runat="server" Text="Emloyee Name" />
                        </td>
                        <td style="width: 17%">
                            <asp:TextBox ID="txtByName" runat="server" CssClass="textbox"></asp:TextBox>
                        </td>

                        <td width="15%">
                            <asp:CheckBox ID="chkbyDept" runat="server" Text="Department" />
                        </td>
                        <td style="width: 16%">
                            <asp:DropDownList ID="cboDepartment" runat="server" CssClass="Dropdown"></asp:DropDownList>
                        </td>

                        <td width="15%">
                            <asp:CheckBox ID="chkByDesg" runat="server" Text="Designation" />
                        </td>
                        <td width="15%">
                            <asp:DropDownList ID="cboDesignation" runat="server" CssClass="Dropdown"></asp:DropDownList>
                        </td>

                        <td width="10%" align="right">&nbsp;</td>
                    </tr>

                    <tr>
                        <td height="25" style="width: 18%">
                            <asp:CheckBox ID="chkQualification" runat="server" Text="Qualification" />
                        </td>
                        <td style="width: 17%">
                            <asp:DropDownList ID="cboQualification" runat="server" CssClass="Dropdown"></asp:DropDownList>
                        </td>

                        <td width="15%">
                            <asp:CheckBox ID="chkGender" runat="server" Text="Gender" />
                        </td>
                        <td style="width: 16%">
                            <asp:DropDownList ID="cboGender" runat="server" CssClass="Dropdown">
                                <asp:ListItem>Male</asp:ListItem>
                                <asp:ListItem>Female</asp:ListItem>
                            </asp:DropDownList>
                        </td>

                        <td width="15%">
                            <asp:CheckBox ID="chkReligion" runat="server" Text="Religion" />
                        </td>
                        <td width="15%">
                            <asp:DropDownList ID="cboReligion" runat="server" CssClass="Dropdown"></asp:DropDownList>
                        </td>

                        <td width="10%" align="right">&nbsp;</td>
                    </tr>

                    <tr>
                        <td height="25" style="width: 18%">
                            <asp:CheckBox ID="chkCaste" runat="server" Text="Caste" />
                        </td>
                        <td style="width: 17%">
                            <asp:DropDownList ID="cboCaste" runat="server" CssClass="Dropdown"></asp:DropDownList>
                        </td>

                        <td width="15%">
                            <asp:CheckBox ID="chkPayScale" runat="server" Text="Pay Scale" />
                        </td>
                        <td style="width: 16%">
                            <asp:DropDownList ID="cboPayScale" runat="server" CssClass="Dropdown"></asp:DropDownList>
                        </td>

                        <td width="15%">
                            <asp:CheckBox ID="chkGradePay" runat="server" Text="Grade Pay" />
                        </td>
                        <td width="15%">
                            <asp:DropDownList ID="cboGradePay" runat="server" CssClass="Dropdown"></asp:DropDownList>
                        </td>

                        <td width="10%" align="right">&nbsp;</td>
                    </tr>

                    <tr>
                        <td height="25" style="width: 18%">
                            <asp:CheckBox ID="chkCategory" runat="server" Text="Category" />
                        </td>
                        <td style="width: 17%">
                            <asp:DropDownList ID="cboCategory" runat="server" CssClass="Dropdown"></asp:DropDownList>
                        </td>

                        <td width="15%">
                            <asp:CheckBox ID="chkStatus" runat="server" Text="Status" />
                        </td>
                        <td style="width: 16%">
                            <asp:DropDownList ID="cboStatus" runat="server" CssClass="Dropdown"></asp:DropDownList>
                        </td>

                        <td width="15%">
                            <asp:CheckBox ID="chkType" runat="server" Text="Type" />
                        </td>
                        <td width="15%">
                            <asp:DropDownList ID="cboEmpType" runat="server" CssClass="Dropdown"></asp:DropDownList>
                        </td>

                        <td width="10%" align="Left">&nbsp;</td>
                    </tr>

                    <tr>
                        <td height="25" style="width: 18%">
                            <asp:CheckBox ID="chkDOJ" runat="server" Text="Date of joinning From" />
                        </td>
                        <td style="width: 17%">
                            <asp:TextBox ID="txtDOJFrom" runat="server" placeholder="dd/mm/yyyy" CssClass="textbox"></asp:TextBox>
                            <asp:CalendarExtender ID="txtDOI_CalendarExtender" runat="server" TargetControlID="txtDOJFrom" Format="dd/MM/yyyy"></asp:CalendarExtender>
                            <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999" MaskType="Date" TargetControlID="txtDOJFrom" PromptCharacter="_"> </asp:MaskedEditExtender>
                        </td>

                        <td width="15%">To</td>
                        <td style="width: 16%">
                            <asp:TextBox ID="txtDOJTO" runat="server" placeholder="dd/mm/yyyy" CssClass="textbox"></asp:TextBox>
                            <asp:CalendarExtender ID="cetxtDOIFrom" runat="server" TargetControlID="txtDOJTO" Format="dd/MM/yyyy"></asp:CalendarExtender>
                            <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999" MaskType="Date" TargetControlID="txtDOJTO" PromptCharacter="_"> </asp:MaskedEditExtender>
                        </td>

                        <td width="15%">&nbsp;</td>
                        <td width="15%">
                            <asp:Button ID="btnSearch" runat="server" Text="Search" Width="100px" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
                        </td>

                        <td width="10%" align="Left">&nbsp;</td>
                    </tr>

                    <tr>
                        <td height="25" colspan="2">
                            <asp:Label ID="lblStatus" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                        </td>

                        <td width="15%" colspan="2" style="text-align: center">

                            <asp:Label ID="lblTotalRecords" runat="server" Style="color: #009900"></asp:Label>
                        </td>

                        <td width="15%">&nbsp;</td>
                        <td width="15%">&nbsp;</td>

                        <td width="10%" align="right">&nbsp;</td>
                    </tr>

                    <tr>
                        <td colspan="7">
                           <div id="gvDiv" style="width: 1000px; max-height: 1000px; overflow-x: scroll; text-align: center;">
                                <asp:Label ID="lblSchoolName" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
                                <br />
                                <asp:Label ID="lblTitle" runat="server" Font-Bold="True" Font-Size="Small"></asp:Label>
                                <br />
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                                    CssClass="Grid" DataSourceID="SqlDataSource1" Width="100%">

                                    <Columns>
                                        <asp:TemplateField HeaderText="SN.">
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="EmpCode" HeaderText="Emp Code" SortExpression="EmpCode" />
                                        <asp:BoundField DataField="EmpName" HeaderText="Emp Name" SortExpression="EmpName" />
                                        <asp:BoundField DataField="DOB" DataFormatString="{0:dd/MM/yyyy}"
                                            HeaderText="Date of Birth" HtmlEncode="False" SortExpression="DOB" />
                                        <asp:BoundField DataField="DOJ" DataFormatString="{0:d/MM/yyyy}"
                                            HeaderText="Date of Join" HtmlEncode="False" SortExpression="DOJ" />
                                              <asp:BoundField DataField="SchoolName" HeaderText="School" SortExpression="SchoolName" />
                                        <asp:BoundField DataField="DeptName" HeaderText="Department"
                                            SortExpression="DeptName" />
                                        <asp:BoundField DataField="DesgName" HeaderText="Designation"
                                            SortExpression="DesgName" />
                                        <asp:BoundField DataField="QualName" HeaderText="Qualification"
                                            SortExpression="QualName" />
                                        <asp:BoundField DataField="Gender" HeaderText="Gender"
                                            SortExpression="Gender" />

                                        <asp:BoundField DataField="RelName" HeaderText="Religion"
                                            SortExpression="RelName" />
                                        <asp:BoundField DataField="CasteName" HeaderText="Caste" SortExpression="CasteName" />
                                        <asp:BoundField DataField="AccNo" HeaderText="AccNo" SortExpression="AccNo" />
                                        <asp:BoundField DataField="PAN" HeaderText="PAN" SortExpression="PAN" />
                                        <asp:BoundField DataField="PerAdd" HeaderText="Address" SortExpression="PerAdd" />
                                        <asp:BoundField DataField="Mob" HeaderText="Mobile" SortExpression="Mob" />
                                        <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                                    </Columns>

                                </asp:GridView>

                            </div>
                            <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="btn btn-primary" Width="100px" />
                            &nbsp;&nbsp;
                <asp:Button ID="btnExcel" runat="server" Text="Export to Excel" CssClass="btn btn-primary" Width="125px" />

                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="select EmpCode,EmpName,DOB,DOJ, DeptName, DesgName,QualName, CASE WHEN Gender = 0 THEN 'Male' ELSE 'Female' END AS Gender, RelName, CasteName,AccNo, PAN, PerAdd, Mob,Email From vw_Employees where EmpID=0"></asp:SqlDataSource>

                            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

                        </td>
                    </tr>

                </table>
            </div>
        </div>
        <div class="clearfix"></div>
    </div>
</asp:Content>

