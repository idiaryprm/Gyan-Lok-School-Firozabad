<%@ Page Language="VB" MasterPageFile="~/AdminMaster.master" AutoEventWireup="false" Inherits="iDiary_V3.Admin_Syllabus" title="Untitled Page" Codebehind="Syllabus.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AdminMasterContents" runat="Server">
    <table class="table">
        <tr>
            <td width="20%" style="text-decoration: underline; font-size: 14px"><strong>SYLLABUS</strong></td>
            <td style="width: 50%">&nbsp;</td>
        </tr>
        <tr>
            <td width="20%">Class</td>
            <td style="width: 50%">
                <asp:DropDownList ID="cboClass" runat="server" Width="214px" AutoPostBack="True" CssClass="Dropdown">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td width="20%">Section</td>
            <td style="width: 50%">
                <asp:DropDownList ID="cboSection" runat="server" Width="214px" CssClass="Dropdown">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td width="20%">Syllabus for</td>
            <td style="width: 50%">
                <asp:TextBox ID="txtSyllabusFor" runat="server" CssClass="textbox"  Width="214px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td width="20%">Upload File</td>
            <td style="width: 50%" valign="middle">
                <asp:FileUpload ID="myFile" runat="server" BorderWidth="1px" Width="222px" />
            </td>
        </tr>
        <tr>
            <td width="20%">&nbsp;</td>
            <td style="width: 50%">&nbsp;</td>
        </tr>
        <tr>
            <td width="20%">&nbsp;</td>
            <td style="width: 50%">
                <asp:Button ID="btnSave" runat="server" Text="Save Information" CssClass="btn btn-primary" />
                <asp:Label ID="lblStatus" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="20%">&nbsp;</td>
            <td style="width: 50%">
                <asp:SqlDataSource ID="SqlDataSourceSyllabus" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT [SyllabusID], [Title], [FileName], [UploadDate], CSSName FROM [vw_Syllabus] order by [UploadDate] desc"></asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td colspan="3" style="height:500px;overflow-y:scroll">
                 <asp:GridView ID="gvSyllabus" runat="server" AutoGenerateColumns="False" DataKeyNames="SyllabusID" DataSourceID="SqlDataSourceSyllabus" Width="100%" CssClass="Grid">
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
                <asp:BoundField DataField="SyllabusID" HeaderText="Syllabus ID" ReadOnly="True" SortExpression="SyllabusID" />
                <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
                <asp:BoundField DataField="FileName" HeaderText="File Name" SortExpression="FileName" />
                <asp:BoundField DataField="UploadDate" HeaderText="Upload Date" SortExpression="UploadDate" DataFormatString="{0: dd/MM/yyyy}" />
                <asp:BoundField DataField="CSSName" HeaderText="Class Name" SortExpression="CSSName" />
            </Columns>
        </asp:GridView>
            </td>
        </tr>
    </table>
   
       
       
        <br />
        <asp:Button ID="btnRemove" runat="server" Text="Remove Selected item" CssClass="btn btn-primary" />
     <asp:TextBox ID="txtSyllID" runat="server" CssClass="textbox"  Width="34px" Visible="false"></asp:TextBox>
</asp:Content>

