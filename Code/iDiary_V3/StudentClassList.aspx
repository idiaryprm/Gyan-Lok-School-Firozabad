<%@ Page Title="Student Class List" Language="vb" AutoEventWireup="false" MasterPageFile="~/StudentReport.Master" CodeBehind="StudentClassList.aspx.vb" Inherits="iDiary_V3.StudentClassList" EnableEventValidation="false" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SubHeading" Runat="Server">
    <p>
        Student Class List</p>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ReportContent" Runat="Server">
   <%--<br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />--%>
    <table class="table">
        <tr>
            <td colspan="6">
            <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table width="100%" cellpadding="1" cellspacing="1" border="0" class="Table_Font">

                        <tr>
                            <td height="25" style="width: 15%">
                                School Name</td>
                            <td width="15%" colspan="2">
                                <asp:DropDownList ID="cboSchoolName" runat="server" AutoPostBack="true" CssClass="Dropdown" Width="300px">
                                </asp:DropDownList>
                            </td>

                            <td width="15%">
                                &nbsp;</td>

                            <td style="width: 14%">
                                &nbsp;</td>

                            <td width="15%">
                                &nbsp;</td>

                            <td width="10%" align="right">&nbsp;</td>
                        </tr>

                        <tr>
                            <td height="25" style="width: 15%">
                                <asp:CheckBox ID="chkClass" runat="server" Text="Class" />
                            </td>
                            <td width="15%">
                                <asp:DropDownList ID="cboClass" runat="server" AutoPostBack="True" CssClass="Dropdown" style="width:100px">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 15%">
                                <asp:CheckBox ID="chkSection" runat="server" Text="Section" />
                            </td>
                            <td width="15%">
                                <asp:DropDownList ID="cboSection" runat="server" CssClass="Dropdown" style="width:100px">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 14%">
                                <asp:CheckBox ID="chkHouse" runat="server" Text="House" />
                            </td>
                            <td width="15%">
                                <asp:DropDownList ID="cboHouse" runat="server" CssClass="Dropdown" style="width:100px">
                                </asp:DropDownList>
                            </td>
                            <td align="right" width="10%">&nbsp;</td>
                        </tr>

                        <tr>
                            <td height="25" style="width: 15%">
                                <asp:CheckBox ID="chkGender" runat="server" Text="Gender" />
                            </td>
                            <td width="15%">
                                <asp:DropDownList ID="cboGender" runat="server" CssClass="Dropdown" style="width:100px">
                                    <asp:ListItem>Male</asp:ListItem>
                                    <asp:ListItem>Female</asp:ListItem>
                                </asp:DropDownList>
                            </td>

                            <td style="width: 15%">
                                <asp:CheckBox ID="chkCategory" runat="server" Text="Category" />
                            </td>
                            <td width="15%">
                                <asp:DropDownList ID="cboCategory" runat="server" CssClass="Dropdown" style="width:100px">
                                </asp:DropDownList>
                            </td>

                            <td style="width: 14%">
                                <asp:CheckBox ID="chkStatus" runat="server" Text="Status" />
                            </td>
                            <td width="15%">
                                <asp:DropDownList ID="cboStatus" runat="server" CssClass="Dropdown" style="width:100px">
                                </asp:DropDownList>
                            </td>

                            <td width="10%" align="right">&nbsp;</td>
                        </tr>

                        <tr>
                            <td height="25" style="width: 15%">&nbsp;</td>
                            <td width="15%">&nbsp;</td>

                            <td style="width: 15%">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
                            <td width="15%">&nbsp;</td>

                            <td style="width: 14%">&nbsp;</td>
                            <td width="15%" style="margin-left: 40px">
                                <asp:Button ID="btnSearch" runat="server"  Text="Search" CssClass="btn btn-primary" />
                            </td>

                            <td width="10%" align="Left">&nbsp;</td>
                        </tr>
                    </table>
                </ContentTemplate>
                 <Triggers>

        <asp:PostBackTrigger ControlID="btnSearch" />
    </Triggers>
            </asp:UpdatePanel>
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
                <div id="gvDiv" style="width: 90%; max-height: 1000px; overflow-x: scroll; text-align: left;">
                    <center>
                        <asp:Label ID="lblSchoolName" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
                        <br />
                        <asp:Label ID="lblTitle" runat="server" Font-Bold="True" Font-Size="Small"></asp:Label>
                        <br />

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
                    <asp:Label ID="lblCount" runat="server" Text=""></asp:Label>
                </div>
                <br />
                <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="btn btn-primary" />
                &nbsp;&nbsp;
                <asp:Button ID="btnExcel" runat="server" Text="Export to Excel" CssClass="btn btn-primary" />
                <asp:SqlDataSource ID="SqlDataSource1" runat="server"
                    ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>"
                    SelectCommand="Select RegNo,ClassRollNo, SName, FName, MName, ClassName, SecName, FeeBookNo, FatherAddress, PhoneResd, DOB, MobNo, AdmissionDate,CategoryName From vw_Student where sid<0"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server"
                    ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>"
                    SelectCommand="SELECT [Regno],[classrollno], [SName] FROM vw_Student"></asp:SqlDataSource>
                
            </td>
        </tr>

        <tr>
            <td colspan="2">&nbsp;
                <br />
            </td>
        </tr>

        <tr>
            <td colspan="2">&nbsp;</td>
        </tr>

    </table>
</asp:Content>
