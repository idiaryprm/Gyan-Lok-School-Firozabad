<%@ Page Language="VB" MasterPageFile="~/AdminMaster.master" AutoEventWireup="false" Inherits="iDiary_V3.Admin_Circulars" title="Manage Circulars" Codebehind="Circulars.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AdminMasterContents" runat="Server">
    <table class="table">
        <tr>
            <td width="20%" style="font-size: 14px; text-decoration: underline">
                <strong>CIRCULARS</strong></td>
            <td style="width: 38%">&nbsp;</td>
            <td style="width: 50%">&nbsp;</td>
        </tr>
        <tr>
            <td width="20%">Title of Circular</td>
            <td style="width: 38%">
                <asp:TextBox ID="txtTitle" runat="server" CssClass="textbox" Width="250px"></asp:TextBox>
            </td>
            <td style="width: 50%" valign="top">
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT * FROM [Circulars] ORDER BY [UploadDate] DESC">
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td width="20%">Upload File</td>
            <td style="width: 38%" valign="middle">
                <asp:FileUpload ID="myFile" runat="server" CssClass="textbox" Width="250px" Height="30px" />
            </td>
            <td style="width: 50%" valign="top">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="20%">&nbsp;</td>
            <td style="width: 38%">
                <asp:TextBox ID="txtID" runat="server" Width="31px" Visible="False"></asp:TextBox>
            </td>
            <td style="width: 50%" valign="top">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="20%">&nbsp;</td>
            <td style="width: 38%">
                <asp:Label ID="lblStatus" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
            </td>
            <td style="width: 50%" valign="top">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="20%">&nbsp;</td>
            <td style="width: 38%">
                <asp:Button ID="btnSave" runat="server" Text="Save Information" CssClass="btn btn-primary" />
                &nbsp;&nbsp;&nbsp;<asp:Button ID="btnRemove" runat="server" Text="Remove" CssClass="btn btn-primary" />
            </td>
            <td style="width: 50%" valign="top">
                </td>
        </tr>
        <tr>
            <td width="20%">&nbsp;</td>
            <td style="width: 38%">&nbsp;</td>
            <td style="width: 50%" valign="top">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="20%" colspan="3" style="height:500px;overflow-y:scroll">
                <div style="height:500px;overflow-y:scroll">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="CircularID" DataSourceID="SqlDataSource1" CssClass="Grid" GridLines="None" Width="100%">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                       
                        <asp:CommandField ShowSelectButton="True">
                        <ItemStyle Width="80px" />
                        </asp:CommandField>
                        <asp:BoundField DataField="CircularID" HeaderText="Circular ID" SortExpression="CircularID" InsertVisible="False" ReadOnly="True" >
                        <ItemStyle Width="50px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" >
                        <ItemStyle Width="200px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="FileName" HeaderText="File Name" SortExpression="FileName" >
                        <ItemStyle Width="80px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="UploadDate" DataFormatString="{0: dd/MM/yyyy}" HeaderText="Upload Date" SortExpression="UploadDate">
                        <ItemStyle Width="80px" />
                        </asp:BoundField>
                    </Columns>
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                </asp:GridView>
                    </div>
                </td>
        </tr>
        </table>
</asp:Content>