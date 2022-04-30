<%@ Page Language="VB" MasterPageFile="~/ExamAdminMasterPage.master" AutoEventWireup="false" Inherits="iDiary_V3.SubjectMapping" title="Untitled Page" Codebehind="SubjectMapping.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SubHeading" Runat="Server">
    Subject Mapping
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMain" Runat="Server">
    <table width="100%">
        <tr>
            <td style="width: 30%" valign="top" align="left">
                <b>Subject List<br />
                </b>
                <asp:ListBox ID="lstSelected" runat="server" Height="428px" Width="266px" AutoPostBack="True">
                </asp:ListBox>
            </td>
            <td style="width: 39%" valign="top" align="left">
                <b>Class<br />
                </b>
                <asp:DropDownList ID="cboClass" runat="server" 
                    AutoPostBack="True" CssClass="Dropdown">
                </asp:DropDownList>
                <br />
                <br />
                <b>Section<br />
                <asp:DropDownList ID="cboSection" runat="server" 
                    AutoPostBack="True" CssClass="Dropdown">
                </asp:DropDownList>
                <br />
                <br />
                Sub Section<br />
                <asp:DropDownList ID="cboSubSection" runat="server" 
                    AutoPostBack="True" CssClass="Dropdown">
                </asp:DropDownList>
                <br />
                </b>
                <br />
                <b>Subject Selection<br />
                </b>
                <asp:DropDownList ID="cboSubjects" runat="server" CssClass="Dropdown">
                </asp:DropDownList>
                &nbsp;&nbsp;&nbsp;&nbsp;
                <br />
                <br />
                <asp:CheckBox ID="cbExam" runat="server" Checked="True" Font-Bold="True" Font-Underline="True" Text="Applicable in Exams" />
                <br />
                <asp:CheckBox ID="cbTimetable" runat="server" Checked="True" Font-Bold="True" Font-Underline="True" Text="Applicable in TimeTable" />
                <br />
                <strong>TT Weightage
                <br />
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
                <br />
                </strong>
                <br />
                <asp:Button ID="btnAdd" runat="server" Text="Add" Width="70px" />
&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnRemove" runat="server" Text="Remove" Width="70px" />
                &nbsp;&nbsp;
                <br />
                <br />
                <asp:Label ID="lblStatus" runat="server" Font-Bold="True" ForeColor="Navy"></asp:Label>
            </td>
        </tr>
        </table>
</asp:Content>



