<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/TeacherMasterPage.master" CodeBehind="TeacherInbox.aspx.vb" Inherits="iDiary_V3.TeacherInbox" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TeacherContents" runat="server">
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <table class="table">
        <tr>
            <td style="height: 348px;" valign="top">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSourceMSG" CellPadding="4" DataKeyNames="msgID" GridLines="None" AllowPaging="True" Width="100%" CssClass="Grid">
        <Columns>
            <asp:CommandField ShowSelectButton="True" />
            <asp:BoundField DataField="msgID" HeaderText="msgID" SortExpression="msgID" ReadOnly="True" />
            <asp:BoundField DataField="RegNo" HeaderText="RegNo" SortExpression="RegNo" />
            <asp:BoundField DataField="SName" HeaderText="Student Name" SortExpression="SName" />
            <asp:BoundField DataField="CssName" HeaderText="Class" SortExpression="CssName" />
            <asp:BoundField DataField="sentDate" HeaderText="Sent Date" SortExpression="sentDate" DataFormatString="{0:dd/MM/yyyy}" />
            <asp:BoundField DataField="sentTime" SortExpression="Sent Time" HeaderText="sentTime" DataFormatString="{0:hh:mm tt}">
            <ItemStyle HorizontalAlign="Left" />
            </asp:BoundField>
        </Columns>
        
    </asp:GridView>
                <br />
                <asp:Button ID="btnDelete" runat="server" Text="Delete Selected" Visible="False" CssClass="btn btn-primary"/>
                &nbsp;<br />
            </td>
            <td style="height: 348px;" valign="top" class="text-center">
                <br />
                <asp:Label ID="lblSubject" runat="server" style="font-weight: 700; text-align: right;"></asp:Label>
                <br />
                <asp:Label ID="txtMessage" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width:500px">
&nbsp;&nbsp;&nbsp;
                </td>
            <td style="width:100px" valign="top" >&nbsp;</td>
        </tr>
    </table>
    <asp:SqlDataSource ID="SqlDataSourceMSG" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT msgID, [RegNo], [SName], CssName, [sentDate], [sentTime] FROM [vw_msgSentFromParent] WHERE ([EmpCode] = @EmpCode) order by sentDate Desc ">
        <SelectParameters>
            <asp:CookieParameter CookieName="UID" Name="EmpCode" Type="String" />
        </SelectParameters>
        </asp:SqlDataSource>
    <br />
    <br />
</asp:Content>
