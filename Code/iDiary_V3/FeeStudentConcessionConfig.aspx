<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/FeeMasterPage.master" CodeBehind="FeeStudentConcessionConfig.aspx.vb" Inherits="iDiary_V3.FeeStudentConcessionConfig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FeeMasterContents" runat="server">
    <%--<br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />--%>   
     <table class="table">
             <tr>
            <td width="20%">Reg No.</td>
            <td style="width: 20%">
                <asp:TextBox ID="txtRegNo" runat="server" CssClass="textbox"></asp:TextBox>
                 </td>
            <td style="width: 14%">
                <asp:Button ID="btnFindRegNo" runat="server" Text="&gt;&gt;" 
                    CssClass="btn btn-sm btn-primary" />
            </td>
            <td style="width: 26%">Fee Book No.</td>
            <td style="width: 24%">
                <asp:TextBox ID="txtFeeBookNo" runat="server" CssClass="textbox"></asp:TextBox>
                 </td>
            <td width="30%">
                <asp:Button ID="btnFind" runat="server" Text="&gt;&gt;" 
                    CssClass="btn btn-sm btn-primary" />
            </td>
        </tr>
        <tr>
            <td colspan="6">
                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" AutoGenerateSelectButton="True" CssClass="Grid" DataSourceID="SqlDataSource2" Width="98%">
                    <Columns>
                        <asp:BoundField DataField="FeeBookno" HeaderText="Fee Book No" SortExpression="FeeBookno" />
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
                <asp:TextBox ID="txtFName" runat="server" CssClass="textbox" ReadOnly="True" 
                    ></asp:TextBox>
            </td>
            <td width="30%">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="20%">Admission Date</td>
            <td style="margin-left: 40px; width: 20%;">
                <asp:TextBox ID="txtAdmissionDate" runat="server"  ReadOnly="True" 
                    CssClass="textbox"></asp:TextBox>
            </td>
            <td style="margin-left: 40px; width: 14%;">
                &nbsp;</td>
            <td style="width: 26%">Date of Birth</td>
            <td style="width: 24%">
                <asp:TextBox ID="txtDOB" runat="server" ReadOnly="True" 
                   CssClass="textbox"></asp:TextBox>
            </td>
            <td width="30%">
                &nbsp;</td>
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
            <td style="margin-left: 40px; width: 14%;">
                &nbsp;</td>
            <td style="width: 26%">
                Concession Head</td>
            <td style="width: 24%">
                <asp:DropDownList ID="cboConcessionType" runat="server"  CssClass="Dropdown"></asp:DropDownList>
            </td>
            <td width="30%">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="20%">
                <asp:TextBox ID="txtMyTable" runat="server" BorderWidth="1px" 
                    Width="55px" Visible="False"></asp:TextBox>
            </td>
            <td style="margin-left: 40px; width: 20%;">
                <asp:TextBox ID="txtSID" runat="server" BorderWidth="1px" 
                    Width="55px" Visible="False"></asp:TextBox>
            </td>
            <td style="margin-left: 40px; width: 14%;">
                &nbsp;</td>
            <td style="width: 26%">
                <asp:TextBox ID="txtFeeGroupID" runat="server" BorderWidth="1px" 
                    Width="55px" Visible="False"></asp:TextBox>
            </td>
            <td style="width: 24%">
                <asp:Button ID="btnNext" runat="server" width="120px" Text="Next" 
                    CssClass="btn btn-primary" />
            </td>
            <td width="30%">
                &nbsp;</td>
        </tr>
        <tr>
             <td align="left" colspan="6" style="text-align: center" valign="top">
                    <asp:Table ID="myTable" runat="server" Width="100%" GridLines="Horizontal">
                    </asp:Table>
                </td>
        </tr>
        <tr>
            <td valign="top" align="left" style="width: 26%">
                <asp:Button ID="btnSave" runat="server" Text="Save" Width="100px" CssClass="btn btn-primary"/>
                
                </td>
            <td valign="top" align="left" colspan="4">
                <asp:Button ID="btnRemove" runat="server" Text="Remove" Width="100px" CssClass="btn btn-primary"/>
                <asp:Label ID="lblStatus" runat="server" Font-Bold="True" ForeColor="Navy" Text="" style="color: #FF0000"></asp:Label>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT FeeBookno, SName, ClassName, SecName, FName, MName,AdmissionDate,DOB FROM vw_Student WHERE [SName] Like '%SearchByName%' or @SearchByName is null">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="txtSName" Name="SearchByName" PropertyName="Text" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:TextBox ID="txtAdmissionFeeID" runat="server" BorderStyle="Solid" BorderWidth="1px" ReadOnly="True" Visible="False" Width="22px"></asp:TextBox>
                <asp:TextBox ID="txtLateFeeID" runat="server" BorderStyle="Solid" BorderWidth="1px" ReadOnly="True" Visible="False" Width="22px"></asp:TextBox>
                <asp:TextBox ID="txtTutionFeeID" runat="server" BorderStyle="Solid" BorderWidth="1px" Height="16px" ReadOnly="True" Visible="False" Width="22px"></asp:TextBox>
                <asp:TextBox ID="txtConveyanceFeeID" runat="server" BorderStyle="Solid" BorderWidth="1px" ReadOnly="True" Visible="False" Width="22px"></asp:TextBox>
                <asp:TextBox ID="txtMName" runat="server" CssClass="textbox" ReadOnly="True" Visible="False" 
                   ></asp:TextBox>
                <asp:TextBox ID="txtConfigType" runat="server" Visible="False" Width="37px"></asp:TextBox>
            </td>
            <td valign="top" align="left" width="15%">
                &nbsp;</td>
        </tr>
        </table>
</asp:Content>
