<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="Promotion.aspx.vb" Inherits="iDiary_V3.Promotion" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Heading" runat="server">
    Promotion Wizard
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type = "text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to save data?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
    <%--<br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />--%>
    <div class="col_3" style="margin-top: 20px;" id="panelEnquiryNo" runat="server">
        <div class="col-md-3 widget widget1" style="width: 100%">
            <div class="r3_counter_box">
    <table class="table">
    <tr>
        <td style="width: 32%" valign="top">
            <strong>Current / Previous Academic Session<br />
            <asp:DropDownList ID="cboCurrentAcademicSession" runat="server" Width="150px" AutoPostBack="True">
            </asp:DropDownList>
            <br />
            School Name<br />
            <asp:DropDownList ID="cboSchoolNameCurrent" runat="server" CssClass="Dropdown" Width="300px" AutoPostBack="true" ></asp:DropDownList>
            <br />
            Current / Previous Class<br />
            <asp:DropDownList ID="cboCurrentClass" runat="server" AutoPostBack="True" 
                Width="150px">
            </asp:DropDownList>
            <br />
            <br />
            Current / Previous Section<br />
            <asp:DropDownList ID="cboCurrentSection" runat="server" 
                Width="150px" AutoPostBack="True">
            </asp:DropDownList>
            <br />
            <br />
            Current / Previous Sub-Section<br />
            <asp:DropDownList ID="cboCurrentSubSection" runat="server" 
                Width="150px">
            </asp:DropDownList>
            <br />
            <br />
            Status<br />
            <asp:DropDownList ID="cboStatus" runat="server" Width="148px">
            </asp:DropDownList>
            <br />
            <br />
            <asp:Button ID="btnList" runat="server"  Text="Show Students" class="btn btn-primary"  />
            <br />
            <br />
            <asp:CheckBox ID="chkSelectAll" runat="server" AutoPostBack="True" 
                Text="Select All" />
            &nbsp;&nbsp;&nbsp;
            <asp:CheckBox ID="chkNone" runat="server" AutoPostBack="True" 
                Text="Uncheck All" />
            <br />
            <br />
            <br />
            <asp:Button ID="btnListPromotion" runat="server" class="btn btn-primary" Text="Student Promotion List" />
            <br />
            <br />
            </strong>
        </td>
        <td style="width: 36%" valign="top">
            <asp:CheckBoxList ID="chkStudList" runat="server" Height="367px" Width="336px">
            </asp:CheckBoxList>
        </td>
        <td valign="top" width="60%">
            <strong>Next Academic Session<br />
            <asp:DropDownList ID="cboNextAcademicSession" runat="server" Width="150px">
            </asp:DropDownList>
            <br />
            School Name<br />
            <asp:DropDownList ID="cboSchoolNameNext" runat="server" CssClass="Dropdown" Width="300px" AutoPostBack="true" ></asp:DropDownList>
            <br />
            Next Class</strong> <strong>
            <br />
            <asp:DropDownList ID="cboNextClass" runat="server" AutoPostBack="True" 
                Width="150px">
            </asp:DropDownList>
            <br />
            <br />
            Next Section<br />
            <asp:DropDownList ID="cboNextSection" runat="server" Width="150px" AutoPostBack="True">
            </asp:DropDownList>
            <br />
            <br />
            Next Sub-Section<br />
            <asp:DropDownList ID="cboNextSubSection" runat="server" Width="150px">
            </asp:DropDownList>
            <br />
            <br />
            <br />
                        <asp:Button ID="btnPromote" runat="server"  Text = "Promote" class="btn btn-primary"/>
            <br />
            <br />
            <asp:Label ID="lblStatus" runat="server" Font-Bold="True" ForeColor="Navy" 
                Text="Label"></asp:Label>
            </strong>
        </td>
    </tr>
    <tr>
        <td valign="top" colspan="3">
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="80%">
            </rsweb:ReportViewer>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
        </td>
    </tr>
</table>
                 </div>
        </div>
        <div class="clearfix"></div>
    </div>
</asp:Content>
