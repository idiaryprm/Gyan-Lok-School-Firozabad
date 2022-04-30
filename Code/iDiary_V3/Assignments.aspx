<%@ Page Language="VB" MasterPageFile="~/TeacherMasterPage.master" AutoEventWireup="false" Inherits="iDiary_V3.Admin_Assignments" title="Untitled Page" Codebehind="Assignments.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TeacherContents" runat="Server">
   <%--    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /> 
        <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />  --%>
    <table class="table">
        <tr>
            <td style="width: 17%; text-decoration: underline; font-size: 14px;">
                <strong>ASSIGNMENTS</strong></td>
            <td style="width: 50%">
                <asp:DropDownList ID="cboSchoolName" runat="server" AutoPostBack="true" CssClass="Dropdown" Width="300px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 17%">Class</td>
            <td style="width: 50%">
                <asp:DropDownList ID="cboClass" runat="server" CssClass="Dropdown" AutoPostBack="True">
                </asp:DropDownList>
                <asp:TextBox ID="txtAssignID" runat="server" Height="16px" Visible="False" Width="51px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 17%">Section</td>
            <td style="width: 50%">
                <asp:DropDownList ID="cboSection" runat="server" CssClass="Dropdown" AutoPostBack="True">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 17%">Sub-Section</td>
            <td style="width: 50%">
                <asp:DropDownList ID="cboSubSection" runat="server" CssClass="Dropdown">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 17%">Assignment Details</td>
            <td style="width: 50%">
                <asp:TextBox ID="txtAssignmentDetails" runat="server" CssClass="textbox"
                    Width="300px" Height="60px" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 17%">Upload File</td>
            <td style="width: 50%" valign="middle">
                <asp:FileUpload ID="myFile" runat="server" CssClass="textbox" Width="300px" Height="30px" />
            </td>
        </tr>
        <tr>
            <td style="width: 17%">&nbsp;</td>
            <td style="width: 50%">
                <asp:Label ID="lblStatus" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 17%">&nbsp;</td>
            <td style="width: 50%">
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" />
                &nbsp;&nbsp;
                <asp:Button ID="btnDelete" runat="server" Text="Remove" CssClass="btn btn-primary" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <div style="height:500px;overflow-y:scroll">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" DataSourceID="SqlDataSource1" CssClass="Grid" Width="100%" PageSize="8">

                    <Columns>
                        <asp:CommandField ShowSelectButton="True" />
                        <asp:BoundField DataField="AssID" HeaderText="Assign ID" SortExpression="AssID" />
                        <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
                        <asp:BoundField DataField="FileName" HeaderText="File Name" SortExpression="FileName" />
                        <asp:BoundField DataField="UploadDate" HeaderText="Upload Date" SortExpression="UploadDate" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField DataField="CSSName" HeaderText="Class" SortExpression="CSSName" />
                    </Columns>
                </asp:GridView>
                    </div>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="Select Distinct CSSName, Title, FileName, UploadDate,AssID  from vw_Assignments "></asp:SqlDataSource>
            </td>
        </tr>
    </table>
</asp:Content>

