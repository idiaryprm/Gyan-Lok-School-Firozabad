<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.master" CodeBehind="Cert_TCList.aspx.vb" Inherits="iDiary_V3.Cert_TCList" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Heading" runat="server">
    TC List
    &amp; Search 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <%-- <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />--%>
    <div class="col_3" style="margin-top: 20px;" id="panelEnquiryNo" runat="server">
        <div class="col-md-3 widget widget1" style="width: 100%">
            <div class="r3_counter_box">
     <table class="table">
        <tr>
            <td style="width: 17%; height: 28px;">School Name</td>
            <td width="10%" style="height: 28px" colspan="2">
                <asp:DropDownList ID="cboSchoolName" runat="server" AutoPostBack="true" CssClass="Dropdown" Width="300px">
                </asp:DropDownList>
            </td>
            <td style="height: 28px; width: 11%;">
                &nbsp;</td>
            <td width="10%"  style="height: 28px">
                &nbsp;</td>
            <td width="50%" style="height: 28px">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 17%; height: 28px;">Admission No.</td>
            <td width="10%" style="height: 28px">
                <asp:TextBox ID="txtRegNo" runat="server" CssClass="textbox"></asp:TextBox>
            </td>
            <td style="height: 28px; width: 24%;">
                <asp:Button ID="btnAdnmSearch" runat="server" Text=">>" CssClass="btn btn-sm btn-primary" />
            </td>
            <td style="height: 28px; width: 11%;">
                Name</td>
            <td width="10%"  style="height: 28px">
                <asp:TextBox ID="txtName" runat="server" CssClass="textbox"></asp:TextBox>
            </td>
            <td width="50%" style="height: 28px">
                <asp:Button ID="btnNameSearch" runat="server" Text=">>" CssClass="btn btn-sm btn-primary" />
            </td>
        </tr>
        <tr>
            <td style="width: 17%; height: 28px;">Date From</td>
            <td width="10%" style="height: 28px">
                <asp:TextBox ID="txtDateFrom" runat="server" placeholder="dd/mm/yyyy" CssClass="textbox"></asp:TextBox>
                <asp:CalendarExtender ID="txtDateFrom_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDateFrom">
                </asp:CalendarExtender>
            </td>
            <td style="height: 28px; width: 24%;">&nbsp;</td>
            <td style="height: 28px; width: 11%;">
                Date To</td>
            <td width="10%"  style="height: 28px">
                <asp:TextBox ID="txtDateTo" runat="server" placeholder="dd/mm/yyyy" CssClass="textbox"></asp:TextBox>
                <asp:CalendarExtender ID="txtDateTo_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDateTo">
                </asp:CalendarExtender>
            </td>
            <td width="50%" style="height: 28px">
                <asp:Button ID="btnDateSearch" runat="server" Text=">>" CssClass="btn btn-sm btn-primary" />
            </td>
        </tr>
        <tr>
            <td style="width: 17%; height: 27px;">Class</td>
            <td width="10%" style="height: 27px">
                <asp:DropDownList ID="cboClass" runat="server" AutoPostBack="True" CssClass="Dropdown">
                </asp:DropDownList>
            </td>
            <td style="height: 27px; width: 24%;"></td>
            <td style="height: 27px; width: 11%;">
                Section</td>
            <td width="10%"  style="height: 27px">
                <asp:DropDownList ID="cboSection" runat="server" CssClass="Dropdown">
                </asp:DropDownList>
            </td>
            <td width="50%" style="height: 27px">
                <asp:Button ID="btnClassSearch" runat="server" Text=">>" CssClass="btn btn-sm btn-primary" />
            </td>
        </tr>
        <tr>
            <td style="height: 28px;" colspan="6">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"  DataSourceID="SqlDataSourceTC" CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr" Width="98%">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" />
                        <asp:BoundField DataField="TCNo" HeaderText="TC No" SortExpression="TCNo" />
                        <asp:BoundField DataField="RegNo" HeaderText="RegNo" SortExpression="RegNo" />
                        <asp:BoundField DataField="SName" HeaderText="Student Name" SortExpression="SName" />
                        <asp:BoundField DataField="FName" HeaderText="Father Name" SortExpression="FName" />
                        <asp:BoundField DataField="ClassName" HeaderText="Class Name" SortExpression="ClassName" />
                        <asp:BoundField DataField="SecName" HeaderText="Section" SortExpression="SecName" />
                        <asp:BoundField DataField="dateOfIssue" HeaderText="Date Of Issue" SortExpression="dateOfIssue" DataFormatString="{0:dd/MM/yyyy}" />
                     
                    </Columns>
                    
<PagerStyle CssClass="pgr"></PagerStyle>
                    
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSourceTC" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT [TCNo], [studentCategory], [RegNo], [SName], [FName], [ClassName], [SecName], [dateOfIssue] FROM [vw_StudentTC]"></asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td style="width: 17%">
                &nbsp;</td>
            <td width="10%">
                &nbsp;</td>
            <td style="width: 24%">&nbsp;</td>
            <td style="width: 11%">
                &nbsp;</td>
            <td width="10%" align="center">
                <asp:Button ID="Button1" runat="server" Text="Button" Visible="False" />
            </td>
            <td width="50%" align="center">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="6">
               <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                </td>
        </tr>
        </table>
                 </div>
        </div>
        <div class="clearfix"></div>
    </div>
</asp:Content>
