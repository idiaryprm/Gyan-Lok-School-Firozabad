<%@ Page Title="Character Certificate" Language="vb" AutoEventWireup="false" MasterPageFile="~/Certificates.Master" CodeBehind="Cert_Char.aspx.vb" Inherits="iDiary_V3.Cert_Char" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SubHeading" Runat="Server">
    Character Certificate 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="CertificateContent" Runat="Server">
    <table border="0" cellpadding="2" cellspacing="2" width="100%">
        <tr>
            <td colspan="4">
                    <asp:GridView ID="GridView2" runat="server" width="98%" AutoGenerateColumns="False" AutoGenerateSelectButton="True"  DataSourceID="SqlDataSource2" ShowHeader="False" CssClass="Grid">
                        <Columns>
                            <asp:BoundField DataField="RegNo" HeaderText="RegNo" SortExpression="RegNo" />
                            <asp:BoundField DataField="SName" HeaderText="SName" SortExpression="SName" />
                            <asp:BoundField DataField="ClassName" HeaderText="ClassName" SortExpression="ClassName" />
                            <asp:BoundField DataField="SecName" HeaderText="SecName" SortExpression="SecName" />
                        </Columns>
                       
                    </asp:GridView>
            </td>

            

        </tr>
        <tr>
            <td style="width: 5%">
                CC No</td>
            <td style="width: 6%">
                <asp:TextBox ID="txtSno" runat="server" CssClass="textbox"></asp:TextBox>
                &nbsp;&nbsp;
                    
                <asp:Button ID="btnNextCC" runat="server"  Text=">>" 
                    CssClass="hvr-glow" />
            </td>

            

            <td style="width: 3%">
                <asp:TextBox ID="txtStudentID" runat="server" CssClass="textbox" Visible="False"></asp:TextBox>
            </td>

            

            <td style="width: 6%">
                &nbsp;</td>

            

        </tr>
        <tr>
            <td style="width: 5%">
                Admin No / Reg No</td>
            <td style="width: 6%">
                <asp:TextBox ID="txtSRNo" runat="server" CssClass="textbox"></asp:TextBox>
                            &nbsp;&nbsp;
                            <asp:Button ID="btnNextSRNO" runat="server"  Text=">>" 
                    CssClass="hvr-glow" />
            </td>

            <td style="width: 3%">
                Student Name</td>

            <td style="width: 6%">
                <asp:TextBox ID="txtName" runat="server" CssClass="textbox"></asp:TextBox>
            &nbsp;<asp:Button ID="btnNextName" runat="server"  Text=">>" 
                    CssClass="hvr-glow" />
            </td>

        </tr>
        <tr>
            <td style="width: 5%">Father Name</td>
            <td style="width: 6%">
                <asp:TextBox ID="txtFName" runat="server"  ReadOnly="True" 
                    CssClass="textbox"></asp:TextBox>
            </td>

            <td style="width: 3%">
                Mother Name</td>

            <td style="width: 6%">
                <asp:TextBox ID="txtMName" runat="server" ReadOnly="True" 
                    CssClass="textbox"></asp:TextBox>
            </td>

        </tr>
        <tr>
            <td style="width: 5%">DOB</td>
            <td style="margin-left: 40px; width: 6%;">
                <asp:TextBox ID="txtDOB" runat="server" ReadOnly="True" 
                  CssClass="textbox"></asp:TextBox>
             
            </td>

            <td style="margin-left: 40px; width: 3%;">
                Left Date</td>

            <td style="margin-left: 40px; width: 6%;">
                <asp:TextBox ID="txtDateDropOut" runat="server" 
                    CssClass="textbox"></asp:TextBox>
                <asp:CalendarExtender ID="txtDateDropOut_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDateDropOut">
                </asp:CalendarExtender>
            </td>

        </tr>
        <tr>
            <td valign="top">
                Last Class Details</td>

            <td valign="top">
                <asp:DropDownList ID="cboClass" runat="server" CssClass="Dropdown" 
                ></asp:DropDownList>
            </td>

            <td valign="top" style="width: 3%">
                Roll No</td>

            <td valign="top">
                <asp:TextBox ID="txtRollNo" runat="server" CssClass="textbox"></asp:TextBox>
            </td>

        </tr>
        <tr>
            <td style="width: 5%" valign="top">
                Division</td>
            <td style="margin-left: 40px; width: 6%;">
                <asp:DropDownList ID="cboLastClassDivision" runat="server" CssClass="Dropdown">
                </asp:DropDownList>
            </td>

            <td style="margin-left: 40px; width: 3%;">
                Result</td>

            <td style="margin-left: 40px; width: 6%;">
                                <asp:DropDownList ID="cboLastClassResult" runat="server" CssClass="Dropdown">
                                </asp:DropDownList>
            </td>

        </tr>
        <tr>
            <td style="width: 5%" valign="top">
                <asp:HyperLink ID="HyperLink1" runat="server" ForeColor="Navy" 
                    NavigateUrl="CharacterMaster.aspx">Character</asp:HyperLink>
&nbsp;<asp:ImageButton ID="ImageButton1" runat="server" 
                    ImageUrl="~/images/Refresh.jpg" style="width: 17px"  />
            </td>
            <td style="margin-left: 40px; width: 6%;">
                <asp:DropDownList ID="cboCharacter" runat="server" CssClass="Dropdown">
                </asp:DropDownList>
            </td>

            <td style="margin-left: 40px; width: 3%;">
                <asp:Button ID="btnGenerate" runat="server" Text="Generate Report" 
                   CssClass="hvr-glow" />
                &nbsp;&nbsp;
            </td>

            <td style="margin-left: 40px; ">
                <asp:Button ID="btnNew" runat="server"  Text="New" 
                    CssClass="hvr-glow" />
            </td>

        </tr>
        <tr>
            <td style="width: 5%">
                <asp:Label ID="lblStatus" runat="server" Font-Bold="True" ForeColor="Navy"></asp:Label>
            </td>
            <td style="margin-left: 40px; " colspan="2">
                &nbsp;&nbsp;&nbsp;&nbsp; 
                <asp:Label ID="lblCharacterPrintDate" runat="server" Text="Certificate Date" Visible="False"></asp:Label>
            </td>

            <td style="margin-left: 40px; width: 6%;">
                <asp:TextBox ID="txtPrintDate" runat="server" BorderWidth="1px" ReadOnly="True" 
                    CssClass="textbox" Visible="False"></asp:TextBox>
                <asp:CalendarExtender ID="txtPrintDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="txtPrintDate">
                </asp:CalendarExtender>
                </td>

        </tr>
        <tr>
            <td colspan="4"><rsweb:ReportViewer ID="ReportViewer1" Width="98%" runat="server">
                </rsweb:ReportViewer>
            </td>

        </tr>
        <tr>
            <td colspan="3">
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT RegNo, SName, ClassName, SecName FROM vw_Student WHERE SID<0">
                   
                </asp:SqlDataSource>
               <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            </td>

            <td>
                &nbsp;</td>

        </tr>
    </table>

</asp:Content>


