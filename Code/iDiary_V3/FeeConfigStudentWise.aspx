<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/FeeMasterPage.master" CodeBehind="FeeConfigStudentWise.aspx.vb" Inherits="iDiary_V3.FeeConfigStudentWise" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FeeMasterContents" runat="server">
    <%--<br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />--%>
    <table class="table">
        <tr>
            <td width="20%">Admin/Reg No.</td>
            <td style="width: 20%">
                <asp:TextBox ID="txtAdminNo" runat="server" CssClass="textbox"></asp:TextBox>
            </td>
            <td style="width: 14%">
                <asp:Button ID="btnFind" runat="server" Text="&gt;&gt;"
                    CssClass="btn btn-sm btn-primary" />
            </td>
            <td style="width: 26%">Fee Book&nbsp; No</td>
            <td style="width: 24%">
                <asp:TextBox ID="txtFeeBookNo" runat="server" CssClass="textbox"></asp:TextBox>
            </td>
            <td style="width: 24%">
                <asp:Button ID="btnFindFeeBook" runat="server" Text="&gt;&gt;"
                    CssClass="btn btn-sm btn-primary" />
            </td>
            <td width="30%">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="7">
                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" AutoGenerateSelectButton="True" CssClass="Grid" DataSourceID="SqlDataSource2" Width="98%">
                    <Columns>
                        <asp:BoundField DataField="RegNo" HeaderText="Reg No" SortExpression="RegNo" />
                        <asp:BoundField DataField="SName" HeaderText="Name" SortExpression="SName" />
                        <asp:BoundField DataField="ClassName" HeaderText="Class" SortExpression="ClassName" />
                        <asp:BoundField DataField="SecName" HeaderText="Sec" SortExpression="SecName" />
                        <asp:BoundField DataField="FName" HeaderText="Father Name" SortExpression="FName" />
                        <asp:BoundField DataField="MName" HeaderText="Mother Name" SortExpression="MName" />
                        <asp:BoundField DataField="AdmissionDate" DataFormatString="{0:d}" HeaderText="Admission Date" HtmlEncode="False" SortExpression="AdmissionDate" />
                        <asp:BoundField DataField="DOB" DataFormatString="{0:d}" HeaderText="Date of Birth" HtmlEncode="False" SortExpression="DOB" />
                    </Columns>

                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td width="20%">Student Name</td>
            <td style="width: 20%">
                <asp:TextBox ID="txtSName" runat="server" CssClass="textbox"></asp:TextBox>
            </td>
            <td style="width: 14%">
                <asp:Button ID="btnNameSearch" runat="server" Text="&gt;&gt;"
                    CssClass="btn btn-sm btn-primary" />
            </td>
            <td style="width: 26%">Father Name</td>
            <td style="width: 24%">
                <asp:TextBox ID="txtFName" runat="server" CssClass="textbox" ReadOnly="True"></asp:TextBox>
            </td>
            <td style="width: 24%">&nbsp;</td>
            <td width="30%">&nbsp;</td>
        </tr>
        <tr>
            <td width="20%">Admission Date</td>
            <td style="margin-left: 40px; width: 20%;">
                <asp:TextBox ID="txtAdmissionDate" runat="server" ReadOnly="True"
                    CssClass="textbox"></asp:TextBox>
            </td>
            <td style="margin-left: 40px; width: 14%;">&nbsp;</td>
            <td style="width: 26%">Date of Birth</td>
            <td style="width: 24%">
                <asp:TextBox ID="txtDOB" runat="server" ReadOnly="True"
                    CssClass="textbox"></asp:TextBox>
            </td>
            <td style="width: 24%">&nbsp;</td>
            <td width="30%">&nbsp;</td>
        </tr>
        <tr>
            <td width="20%">Class - Section</td>
            <td style="margin-left: 40px; width: 20%;">
                <asp:TextBox ID="txtClass" runat="server" Width="92px" ReadOnly="True"
                    CssClass="textbox"></asp:TextBox>
                &nbsp;
                <asp:TextBox ID="txtSec" runat="server" Width="40px" ReadOnly="True"
                    CssClass="textbox"></asp:TextBox>
            </td>
            <td style="margin-left: 40px; width: 14%;">&nbsp;</td>
            <td style="width: 26%">&nbsp;</td>
            <td style="width: 24%">&nbsp;</td>
            <td style="width: 24%">&nbsp;</td>
            <td width="30%">&nbsp;</td>
        </tr>
        <tr>
            <td align="left" colspan="7" style="text-align: center" valign="top">
                <asp:Table ID="myTable" runat="server" Width="98%">
                </asp:Table>
            </td>
        </tr>
        <tr>
            <td valign="top" align="left" style="width: 26%">
                <asp:Button ID="btnSave" runat="server" Text="Save" Width="100px" CssClass="btn btn-primary" />
           
            </td>
            <td valign="top" align="left" width="15%" colspan="5">
                <asp:Button ID="btnRemove" runat="server" Text="Remove" Width="100px" CssClass="btn btn-primary" />
                &nbsp;&nbsp;
                <asp:Label ID="lblStatus" runat="server" Font-Bold="True" ForeColor="Navy" Text="" style="color: #FF0000"></asp:Label>
            </td>
            <td></td>
        </tr>
        <tr>
            <td valign="middle" align="left" colspan="2" height="30">
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT RegNo, SName, ClassName, SecName, FName, MName,AdmissionDate,DOB FROM vw_Student WHERE [SName] Like '%SearchByName%' or @SearchByName is null">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="txtSName" Name="SearchByName" PropertyName="Text" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:TextBox ID="txtMName" runat="server" CssClass="textbox" Visible="false" ReadOnly="True"></asp:TextBox>
                <asp:TextBox ID="txtFeeGroupID" runat="server" BorderWidth="1px"
                    Width="55px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="txtMyTable" runat="server" BorderWidth="1px"
                    Width="55px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="txtConfigType" runat="server" BorderWidth="1px"
                    Width="55px" Visible="False"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="txtSID" runat="server" BorderWidth="1px"
                    Width="55px" Visible="False"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="txtAdmissionFeeID" runat="server" BorderStyle="Solid" BorderWidth="1px" ReadOnly="True" Visible="False" Width="22px"></asp:TextBox>
                <asp:TextBox ID="txtLateFeeID" runat="server" BorderStyle="Solid" BorderWidth="1px" ReadOnly="True" Visible="False" Width="22px"></asp:TextBox>
                <asp:TextBox ID="txtTutionFeeID" runat="server" BorderStyle="Solid" BorderWidth="1px" Height="16px" ReadOnly="True" Visible="False" Width="22px"></asp:TextBox>
                <asp:TextBox ID="txtConveyanceFeeID" runat="server" BorderStyle="Solid" BorderWidth="1px" ReadOnly="True" Visible="False" Width="22px"></asp:TextBox>
            </td>
        </tr>
    </table>
</asp:Content>