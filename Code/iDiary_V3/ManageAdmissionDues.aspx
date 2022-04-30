<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="ManageAdmissionDues.aspx.vb" Inherits="iDiary_V3.ManageAdmissionDues" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Heading" runat="server">
    Manage New Admission 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" cellpadding="2" cellspacing="2" border="0">
        <tr>
            <td style="width: 12%"><strong>Class</strong></td>
            <td style="width: 25%">
                <asp:DropDownList ID="cboClass" runat="server" Width="150px" 
                    AutoPostBack="True">
                </asp:DropDownList>
            </td>
            <td style="width: 12%">
                <strong>Section</strong></td>

            <td style="width: 27%">
                <b>
                <asp:DropDownList ID="cboSection" runat="server" Width="150px" 
                    AutoPostBack="True">
                </asp:DropDownList>
                </b>
            </td>

            <td style="width: 12%">
                <strong>Status</strong></td>

            <td width="60%">
                <asp:DropDownList ID="cboStatus" runat="server" Width="150px">
                </asp:DropDownList>
            </td>

            <td width="60%">
                <asp:Button ID="btnShow" runat="server" Text="Show Students" Width="123px" />
            </td>

        </tr>
        <tr>
            <td colspan="4">
                <asp:Label ID="lblStatus" runat="server" Font-Bold="True" ForeColor="Red" style="color: #FF0000"></asp:Label>
            </td>
            <td style="width: 12%">
                &nbsp;</td>

            <td width="60%">
                &nbsp;</td>

            <td width="60%">
                &nbsp;</td>

        </tr>
        <tr>
            <td colspan="7">
                <b>
                <asp:CheckBox ID="chkCheckAll" runat="server" AutoPostBack="True" Text="Check All" />
                </b>
                <br />
                <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" Height="16px" Width="100%">
                    <RowStyle BackColor="White" ForeColor="#330099" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelect" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="SID" HeaderText="SID" SortExpression="SID" Visible="False" />
                        <asp:BoundField DataField="RegNo" HeaderText="Reg No" SortExpression="RegNo" />
                        <asp:BoundField DataField="FeeBookNo" HeaderText="Fee Book No" SortExpression="FeeBookNo" />
                        <asp:BoundField DataField="SName" HeaderText="Name of the Student" SortExpression="SName" />
                        <asp:BoundField DataField="FName" HeaderText="Father Name" SortExpression="FName" />
                    </Columns>
                    <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                    <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                </asp:GridView>
                <br />
                <asp:Button ID="btnAdd" runat="server" Text="Save New Admissions" Width="200px" />
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" ProviderName="<%$ ConnectionStrings:iDiaryConnectionString.ProviderName %>" SelectCommand="SELECT SID, RegNo, SName, FName, FeeBookNo FROM vw_Student">
                    <%--<SelectParameters>
                        <asp:ControlParameter ControlID="cboASession" Name="ASName" PropertyName="Text" Type="String" />
                    </SelectParameters>--%>
                </asp:SqlDataSource>
            </td>

        </tr>
    </table>
</asp:Content>
