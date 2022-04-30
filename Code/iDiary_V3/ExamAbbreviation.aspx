<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ExamAdminMasterPage.Master" CodeBehind="ExamAbbreviation.aspx.vb" Inherits="iDiary_V3.ExamAbbreviation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SubHeading" runat="server">
    Exam Abbreviation 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <table class="table">
        <tr>
            <td>Abbreviation Name</td>
            <td><asp:TextBox ID="txtAbbName" runat="server" CssClass="textbox"></asp:TextBox></td>
            <td>Abbreviation Type</td>
            <td><asp:DropDownList ID="cboAbbType" runat="server" CssClass="Dropdown" >
                <asp:ListItem>Consider Max Marks As 0</asp:ListItem>
                <asp:ListItem>Consider Obt Marks As 0</asp:ListItem>
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:GridView ID="GridView1" runat="server" Width="100%" CssClass="Grid" AutoGenerateColumns="False" DataKeyNames="AbbID" DataSourceID="SqlDataSource1">
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" />
                                               <asp:BoundField DataField="AbbName" HeaderText="Abbreviation Name" SortExpression="AbbName" />
                        <asp:BoundField DataField="AbbType" HeaderText="Abbreviation Type" SortExpression="AbbType" />
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT AbbID, AbbName, CASE WHEN AbbType = 0 THEN 'Consider Max Marks As 0' ELSE 'Consider Obt Marks As 0' END AS AbbType FROM [ExamAbbreviation]"></asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td><asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary"/>&nbsp;&nbsp;<asp:Button ID="btnNew" runat="server" Text="New" CssClass="btn btn-primary"/></td>
            <td></td>
            <td></td>
            <td><asp:TextBox ID="txtID" runat="server" CssClass="textbox" Visible="False"></asp:TextBox></td>
        </tr>
    </table>
</asp:Content>
