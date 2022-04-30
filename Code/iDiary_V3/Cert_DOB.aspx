<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.master" CodeBehind="Cert_DOB.aspx.vb" Inherits="iDiary_V3.Cert_DOB" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Heading" runat="server">
    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <%--<br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />--%>
     <div class="col_3" style="margin-top: 20px;" id="panelEnquiryNo" runat="server">
        <div class="col-md-3 widget widget1" style="width: 100%">
            <div class="r3_counter_box">
    <table class="table">
        <tr>
            <td class="auto-style8" colspan="8">
                 <%--<div id="gvDiv" style="width: 1000px; max-height: 1000px; overflow-x: scroll; text-align: center;">--%>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" AutoGenerateSelectButton="True" CssClass="Grid" DataSourceID="SqlDataSource1" Width="1000px">
                    <Columns>
                        <asp:BoundField DataField="RegNo" HeaderText="Reg No" SortExpression="RegNo" />
                        <asp:BoundField DataField="SName" HeaderText="Name" SortExpression="SName" />
                        <asp:BoundField DataField="SchoolName" HeaderText="School" SortExpression="SchoolName" />
                          <asp:BoundField DataField="ClassName" HeaderText="Class" SortExpression="ClassName" />
                        <asp:BoundField DataField="SecName" HeaderText="Sec" SortExpression="SecName" />
                        <asp:BoundField DataField="FName" HeaderText="Father Name" SortExpression="FName" />
                        <asp:BoundField DataField="MName" HeaderText="Mother Name" SortExpression="MName" />
                        <asp:BoundField DataField="AdmissionDate" DataFormatString="{0:d}" HeaderText="Admission Date" HtmlEncode="False" SortExpression="AdmissionDate" />
                        <asp:BoundField DataField="DOB" DataFormatString="{0:d}" HeaderText="Date of Birth" HtmlEncode="False" SortExpression="DOB" />
                    </Columns>
                    
                </asp:GridView>
                     <%--</div>--%>
            </td>
        </tr>
        <tr>
            <td colspan="8">
                <asp:CheckBox ID="chkWholeClass" runat="server" Text="Whole Class" AutoPostBack="true" />
            </td>
        </tr>
        <asp:Panel ID="Panel1" runat="server" Visible="false">
        <tr>
            <td>School Name</td>
            <td><asp:DropDownList ID="cboSchoolName" runat="server" AutoPostBack="true"  CssClass="Dropdown"></asp:DropDownList></td>
            <td>Class</td>
            <td>
                <asp:DropDownList ID="cboClass" runat="server" AutoPostBack="true"  CssClass="Dropdown"></asp:DropDownList></td>
            <td>Section</td>
            <td>
                <asp:DropDownList ID="cboSection" runat="server" CssClass="Dropdown"></asp:DropDownList></td>
        </tr>
            </asp:Panel>
        <tr>
            <td class="auto-style4">Adm/Reg No.</td>
            <td class="auto-style2">
                <asp:TextBox ID="txtAdminNo" runat="server" CssClass="textbox"></asp:TextBox>
            </td>
            <td class="auto-style10">
                <asp:Button ID="btnFind" runat="server" Text="&gt;&gt;" 
                    CssClass="btn btn-primary" />
            </td>
            <td class="auto-style3">&nbsp;Student Name</td>
            <td class="auto-style7">
                <asp:TextBox ID="txtSName" runat="server" CssClass="textbox"></asp:TextBox>
            </td>
            <td>
                <asp:Button ID="btnNameSearch" runat="server" Text="&gt;&gt;" 
                    CssClass="btn btn-primary" />
            </td>
            <td class="auto-style5">&nbsp;</td>
              <td>
                  &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style4">School Name</td>
            <td class="auto-style2">
                <asp:TextBox ID="txtSchoolName" runat="server" ReadOnly="true" CssClass="textbox"></asp:TextBox>
            </td>
            <td class="auto-style10">
                &nbsp;</td>
            <td class="auto-style3">Father Name</td>
            <td class="auto-style7">
                <asp:TextBox ID="txtFName" runat="server" CssClass="textbox" ReadOnly="True" 
                    ></asp:TextBox>
                
            </td>
            <td>
                &nbsp;</td>
            <td class="auto-style5">&nbsp;</td>
              <td>
                  &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style4">Mother Name</td>
            <td class="auto-style2"> <asp:TextBox ID="txtMName" runat="server" CssClass="textbox" ReadOnly="True" 
                   ></asp:TextBox>
                
            </td>
            <td class="auto-style10">&nbsp;</td>
            <td class="auto-style3">Admission Date</td>
            <td class="auto-style7"> <asp:TextBox ID="txtAdmissionDate" runat="server"  ReadOnly="True" 
                    CssClass="textbox"></asp:TextBox>
                
            </td>
            <td> &nbsp;</td>
             <td class="auto-style5">&nbsp;</td>
              <td> &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style4">Date of Birth</td>
            <td style="margin-left: 40px" class="auto-style2">
                <asp:TextBox ID="txtDOB" runat="server" ReadOnly="True" 
                   CssClass="textbox"></asp:TextBox>
            </td>
            <td style="margin-left: 40px" class="auto-style10">
                &nbsp;</td>
            <td class="auto-style3">
                Class - Section</td>
            <td class="auto-style7">
                <asp:TextBox ID="txtClass" runat="server" Width="70px" ReadOnly="True" 
                 CssClass="textbox"></asp:TextBox>
                &nbsp;
                <asp:TextBox ID="txtSec" runat="server" Width="35px" ReadOnly="True" 
                 CssClass="textbox"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
             <td class="auto-style5">&nbsp;</td>
              <td>&nbsp;</td>
        </tr>

        <tr>
            <td class="auto-style4">
                <asp:Label ID="lblConduct" runat="server" Text="Conduct"></asp:Label>
            </td>
            <td style="margin-left: 40px" class="auto-style2">
                <asp:DropDownList ID="cboCharacter" runat="server" CssClass="Dropdown">
                </asp:DropDownList>
                
            </td>
            <td style="margin-left: 40px" class="auto-style10">
                &nbsp;</td>
            <td class="auto-style3">
                &nbsp;</td>
            <td class="auto-style6" colspan="2">
                <asp:Button ID="btnGenerate" runat="server" Text="Generate Certificate" 
                    CssClass="btn btn-primary" OnClick="btnGenerate_Click" />
            </td>
             <td class="auto-style5">&nbsp;</td>
              <td>&nbsp;</td>
        </tr>

        <tr>
            <td class="auto-style4"><asp:TextBox ID="txtRollNo" runat="server" BorderWidth="1px" 
                    Width="55px" Visible="False"></asp:TextBox>
                </td>
            <td style="margin-left: 40px" class="auto-style2">
                <asp:TextBox ID="txtSID" runat="server" BorderWidth="1px" 
                    Width="55px" Visible="False"></asp:TextBox></td>
            <td style="margin-left: 40px" class="auto-style10">
                &nbsp;</td>
            <td class="auto-style3">
                &nbsp;</td>
            <td class="auto-style7">    
            <asp:TextBox ID="txtGender" runat="server" BorderWidth="1px" 
                    Width="55px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="txtDOBInWords" runat="server" BorderWidth="1px" 
                    Width="55px" Visible="False"></asp:TextBox>
            </td>
            <td>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT RegNo, SName, ClassName, SecName, FName, MName,AdmissionDate,DOB,SchoolName FROM vw_Student WHERE [SName] Like '%SearchByName%' or @SearchByName is null">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="txtSName" Name="SearchByName" PropertyName="Text" />
                    </SelectParameters>
                </asp:SqlDataSource>    
            </td>
             <td class="auto-style5">    
                 &nbsp;</td>
              <td>
                  &nbsp;</td>
        </tr>


        <tr>
            <td class="auto-style1" colspan="8">
                <br />
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="1000px">
                </rsweb:ReportViewer>
            </td>
        </tr>


    </table>
    <asp:Label ID="lblHeaderImg" Text =""  runat ="server" Visible ="false"  ></asp:Label>
                </div>
        </div>
        <div class="clearfix"></div>
    </div>
</asp:Content>
<asp:Content ID="Content3" runat="server" contentplaceholderid="head">
    <style type="text/css">
        .auto-style1 {
            width: 212px;
        }
        .auto-style2 {
            width: 113px;
        }
        .auto-style3 {
            width: 142px;
        }
        .auto-style4 {
            width: 237px;
        }
        .auto-style5 {
            width: 132px;
        }
        .auto-style6 {
        }
        .auto-style7 {
            width: 145px;
        }
    </style>
</asp:Content>

