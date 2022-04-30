<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="TTConfig.aspx.vb" Inherits="iDiary_V3.TTConfig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Heading" runat="server">
    Time Table Configuration
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col_3" style="margin-top: 20px;" id="panelEnquiryNo" runat="server">
        <div class="col-md-3 widget widget1" style="width: 100%">
            <div class="r3_counter_box">
                <asp:Panel ID="PanelSubject" runat="server">
                    <table class="table">
                        <tr>
                            <td class="auto-style1"><strong>Class</strong></td>
                            <td class="auto-style6">
                                <asp:DropDownList ID="cboClass" runat="server" AutoPostBack="True"
                                    CssClass="Dropdown">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 10%"><strong>Section</strong></td>
                            <td class="auto-style5">
                                <asp:DropDownList ID="cboSection" runat="server" CssClass="Dropdown" AutoPostBack="True"></asp:DropDownList>
                            </td>
                            <td class="auto-style6"><strong>Subject</strong></td>
                            <td class="auto-style4">
                                <asp:DropDownList ID="cboSubject" runat="server" CssClass="Dropdown">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 10%">
                                <asp:Button ID="btnNext" runat="server" CssClass="btn btn-sm btn-primary" Text="&gt;&gt;" />
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style1">Total Periods in Week</td>
                            <td class="auto-style6">
                                <asp:TextBox ID="txtTotalPeriodsWeek" TextMode="Number"  runat="server"
                                    CssClass="textbox"></asp:TextBox>
                            </td>
                            <td style="width: 10%">Weightage</td>
                            <td class="auto-style5">
                                <asp:DropDownList ID="cboWeightage" runat="server" CssClass="Dropdown">
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>9</asp:ListItem>
                                    <asp:ListItem>8</asp:ListItem>
                                    <asp:ListItem>7</asp:ListItem>
                                    <asp:ListItem>6</asp:ListItem>
                                    <asp:ListItem>5</asp:ListItem>
                                    <asp:ListItem>4</asp:ListItem>
                                    <asp:ListItem>3</asp:ListItem>
                                    <asp:ListItem>2</asp:ListItem>
                                    <asp:ListItem>1</asp:ListItem>
                                    <asp:ListItem>0</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="auto-style6">Continuous Periods Allowed</td>
                            <td class="auto-style4">
                                <asp:TextBox ID="txtContinPeriods" TextMode="Number" runat="server"
                                    CssClass="textbox"></asp:TextBox>
                            </td>
                            <td style="width: 10%">&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="auto-style1">Max Periods in Day</td>
                            <td class="auto-style6">
                                <asp:TextBox ID="txtTotalPeriodsDay" TextMode="Number"   runat="server"
                                    CssClass="textbox"></asp:TextBox>
                            </td>
                            <td style="width: 10%">&nbsp;</td>
                            <td class="auto-style5">
                                <asp:Button ID="btnSaveSub" runat="server" CssClass="btn btn-primary" Text="Save" />
                            </td>
                            <td class="auto-style6">&nbsp;</td>
                            <td class="auto-style4">&nbsp;</td>
                            <td style="width: 10%">&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="auto-style1">&nbsp;</td>
                            <td class="auto-style6">&nbsp;</td>
                            <td style="width: 10%">&nbsp;</td>
                            <td class="auto-style5">&nbsp;</td>
                            <td class="auto-style6">&nbsp;</td>
                            <td class="auto-style4">&nbsp;</td>
                            <td style="width: 10%">&nbsp;</td>
                    </table>
                </asp:Panel>
                <asp:Panel ID="PanelEmployee" runat="server">
                    <table class="table">
                        <tr>
                            <td class="auto-style15">Name</td>
                            <td class="auto-style14">
                                <asp:TextBox ID="txtName" runat="server" CssClass="textbox"></asp:TextBox>
                            </td>
                            <td class="auto-style5">
                                <asp:Button ID="btnNameSearch" runat="server" Text=">>" CssClass="btn btn-primary" />
                            </td>
                            <td colspan="4" align="left" valign="top" rowspan="2">
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" AutoGenerateSelectButton="True" BackColor="White" BorderColor="#CC9966" BorderStyle="None" CellPadding="4" DataSourceID="SqlDataSource1" ShowHeader="False" Width="97%" CssClass="Grid">
                                    <Columns>
                                        <asp:BoundField DataField="EmpCode" HeaderText="EmpCode" SortExpression="EmpCode" />
                                        <asp:BoundField DataField="EmpName" HeaderText="EmpName" SortExpression="EmpName" />
                                        <asp:BoundField DataField="DeptName" HeaderText="DeptName" SortExpression="DeptName" />
                                        <asp:BoundField DataField="DesgName" HeaderText="DesgName" SortExpression="DesgName" />
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
                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT [EmpCode], [EmpName], [DeptName], [DesgName] FROM [vw_Employees] Where EmpName Like '%@EmpName@%'">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="txtName" DefaultValue="" Name="EmpName" PropertyName="Text" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style15">Emp Code</td>
                            <td class="auto-style14">
                                <asp:TextBox ID="txtEmpCode" runat="server" CssClass="textbox"></asp:TextBox>
                            </td>
                            <td class="auto-style5">
                                <asp:Button ID="btnEmpNext" runat="server" Text=">>" CssClass="btn btn-primary" />
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style15">Department</td>
                            <td class="auto-style14">
                                <asp:DropDownList ID="cboDepartment" runat="server" CssClass="Dropdown"></asp:DropDownList></td>
                            <td class="auto-style5">Designation</td>
                            <td class="auto-style13">
                                <asp:DropDownList ID="cboDesignation" runat="server" CssClass="Dropdown"></asp:DropDownList><asp:TextBox ID="txtEmpID" runat="server" Width="23px" Visible="False"></asp:TextBox>
                            </td>
                            <td class="auto-style16">Max consecutive Periods</td>
                            <td style="width: 20%">
                                <asp:TextBox ID="txtEmpConsePeriods" runat="server" CssClass="textbox"></asp:TextBox>
                            </td>
                            <td style="width: 10%">&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="auto-style15">Max Day Load</td>
                            <td class="auto-style14">
                                <asp:TextBox ID="txtEmpDayLoad" runat="server" CssClass="textbox"></asp:TextBox>
                            </td>
                            <td class="auto-style5">Max Week Load</td>
                            <td class="auto-style13">
                                <asp:TextBox ID="txtEmpWeekLoad" runat="server" CssClass="textbox"></asp:TextBox>
                            </td>
                            <td class="auto-style16">
                                <asp:Button ID="btnSaveEmp" runat="server" CssClass="btn btn-primary" Text="Save" />
                            </td>
                            <td style="width: 20%">&nbsp;</td>
                            <td style="width: 10%">&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="auto-style15">&nbsp;</td>
                            <td class="auto-style14">&nbsp;</td>
                            <td class="auto-style5">&nbsp;</td>
                            <td class="auto-style13">&nbsp;</td>
                            <td class="auto-style16">&nbsp;</td>
                            <td style="width: 20%">&nbsp;</td>
                            <td style="width: 10%">&nbsp;</td>
                    </table>
                </asp:Panel>

            </div>
        </div>
        <div class="clearfix"></div>
    </div>
</asp:Content>
<asp:Content ID="Content3" runat="server" contentplaceholderid="head">
    <style type="text/css">
        .auto-style1 {
            width: 13%;
        }
        .auto-style4 {
            width: 15%;
        }
        .auto-style5 {
            width: 16%;
        }
        .auto-style6 {
            width: 17%;
        }
        .auto-style12 {
            width: 23%;
        }
        .auto-style13 {
            width: 21%;
        }
        .auto-style14 {
            width: 22%;
        }
        .auto-style15 {
            width: 14%;
        }
        .auto-style16 {
            width: 20%;
        }
    </style>
</asp:Content>

