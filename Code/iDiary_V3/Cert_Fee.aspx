<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.master" CodeBehind="Cert_Fee.aspx.vb" Inherits="iDiary_V3.Cert_Fee" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Heading" runat="server">
    Tution Fee Certificate
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
            <td colspan="9">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" AutoGenerateSelectButton="True" CssClass="Grid" DataSourceID="SqlDataSource1" Width="99%">
                    <Columns>
                        <asp:BoundField DataField="FeeBookNo" HeaderText="Fee Boo No" SortExpression="FeeBookNo" />
                        <asp:BoundField DataField="SName" HeaderText="Name" SortExpression="SName" />
                        <asp:BoundField DataField="SchoolName" HeaderText="School" SortExpression="ClassName" />
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
            <td>Fee Book No</td>
            <td class="auto-style3">
                <asp:TextBox ID="txtFeeBookNo" runat="server" CssClass="textbox"></asp:TextBox>

            </td>
            <td>
    
                <asp:Button ID="btnFind" runat="server" Text=">>"
                    CssClass="btn btn-sm btn-primary" />

            </td>
            <td>Student Name</td>
            <td class="auto-style2">
                <asp:TextBox ID="txtSName" runat="server" CssClass="textbox"></asp:TextBox>
            </td>
            <td>
                <asp:Button ID="btnNameSearch" runat="server" Text="&gt;&gt;"
                    CssClass="btn btn-sm btn-primary" />
            </td>
            <td>Adm. No.</td>
            <td class="auto-style7"><asp:TextBox ID="txtAdminNo" runat="server" CssClass="textbox"></asp:TextBox></td>
            <td>
    
                <asp:Button ID="btnRegNo" runat="server" Text=">>"
                    CssClass="btn btn-sm btn-primary" />

            </td>
        </tr>
        <tr>
            <td class="auto-style4">Father Name</td>
            <td class="auto-style5">
                <asp:TextBox ID="txtFName" runat="server" ReadOnly="True"
                    CssClass="textbox"></asp:TextBox>
            </td>
            <td class="auto-style4">
                </td>
            <td class="auto-style4">Mother Name</td>
            <td class="auto-style6">
                <asp:TextBox ID="txtMName" runat="server" ReadOnly="True"
                    CssClass="textbox"></asp:TextBox>
            </td>
            <td class="auto-style4">
                </td>
             <td class="auto-style4">&nbsp;</td>
            <td class="auto-style8"> &nbsp;</td>
            <td class="auto-style4"> </td>
        </tr>

        <tr>
            <td class="auto-style4">School Name</td>
            <td class="auto-style5" colspan="2">
                <asp:TextBox ID="txtSchoolName" runat="server" Width="200px" ReadOnly="True"
                    CssClass="textbox"></asp:TextBox>
            </td>
            <td class="auto-style4">Class - Section</td>
            <td class="auto-style6">
                <asp:TextBox ID="txtClass" runat="server" ReadOnly="True"
                    CssClass="textbox"></asp:TextBox>
            </td>
            <td class="auto-style4">
                &nbsp;</td>
             <td class="auto-style4">&nbsp;</td>
            <td class="auto-style8"> &nbsp;</td>
            <td class="auto-style4"> &nbsp;</td>
        </tr>

           <tr>
            <td class="auto-style9">Fee Type</td>
            <td style="margin-left: 40px" colspan="6" class="auto-style9">
                <asp:CheckBoxList ID="CheckBoxList1" runat="server"  RepeatColumns="4" RepeatDirection="Horizontal" DataSourceID="SqlDataSource2" DataTextField="FeeTypeName" DataValueField="FeeTypeName" Width="703px">
                
                </asp:CheckBoxList>
                </td>
                
         
            <td style="margin-left: 40px" colspan="2" class="auto-style9">
                <asp:Button ID="btnGenerate" runat="server" Text="Generate Certificate"
                    CssClass="btn btn-primary" Width="160px" />
                </td>
                
         
        </tr>

        <tr>
            <td><asp:DropDownList ID="cboFeeType" runat="server" class="form-control1" Width="76%" Visible="false" Enabled="false" >
                </asp:DropDownList></td>
            <td class="auto-style3">&nbsp;</td>
            <td>&nbsp;</td>
            <td><asp:TextBox ID="txtDOBInWords" runat="server"
                    CssClass="textbox" Visible="False" Width="10px"></asp:TextBox></td>
            <td class="auto-style2">
                <asp:TextBox ID="txtGender" runat="server"
                    CssClass="textbox" Visible="False" Width="10px"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
             <td><asp:TextBox ID="txtSID" runat="server"
                    CssClass="textbox" Visible="False" Width="10px"></asp:TextBox></td>
            <td class="auto-style1" colspan="2">&nbsp;</td>
        </tr>


        <tr>
            <td colspan="9">
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="100%">
                    <LocalReport ReportEmbeddedResource="rptFeeCertificate.rdlc" ReportPath="rptFeeCertificate.rdlc">
                    </LocalReport>
                </rsweb:ReportViewer>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT [FeeTypeName] FROM [FeeTypes]"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT FeeBookNo, SName, ClassName, SecName, FName, MName,AdmissionDate,DOB FROM vw_Student WHERE [SName] Like '%SearchByName%' or @SearchByName is null">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="txtSName" Name="SearchByName" PropertyName="Text" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <br />
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
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
        }
        .auto-style2 {
            width: 132px;
        }
        .auto-style3 {
            width: 141px;
        }
        .auto-style4 {
            height: 42px;
        }
        .auto-style5 {
            height: 42px;
        }
        .auto-style6 {
            width: 132px;
            height: 42px;
        }
        .auto-style7 {
            width: 91px;
        }
        .auto-style8 {
            width: 91px;
            height: 42px;
        }
        .auto-style9 {
            height: 32px;
        }
    </style>
</asp:Content>

