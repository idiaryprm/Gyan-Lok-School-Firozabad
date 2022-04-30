<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/FeeMasterPage.master" CodeBehind="StudentFatherPromotion.aspx.vb" Inherits="iDiary_V3.StudentFatherPromotion" %>
<%--<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="FeeMasterContents" runat="server">
        <table width="100%">
            <tr>
                <td colspan="3">
                 <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" AutoGenerateSelectButton="True" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataSourceID="SqlDataSource2" Font-Names="Garamond" Font-Size="10pt" ShowHeader="False" Width="100%">
                     <Columns>
                         <asp:BoundField DataField="RegNo" HeaderText="RegNo" SortExpression="RegNo" />
                         <asp:BoundField DataField="SName" HeaderText="SName" SortExpression="SName" />
                         <asp:BoundField DataField="ClassName" HeaderText="ClassName" SortExpression="ClassName" />
                         <asp:BoundField DataField="SecName" HeaderText="SecName" SortExpression="SecName" />
                     </Columns>
                     <FooterStyle BackColor="White" ForeColor="#000066" />
                     <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                     <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                     <RowStyle ForeColor="#000066" />
                     <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                     <SortedAscendingCellStyle BackColor="#F1F1F1" />
                     <SortedAscendingHeaderStyle BackColor="#007DBB" />
                     <SortedDescendingCellStyle BackColor="#CAC9C9" />
                     <SortedDescendingHeaderStyle BackColor="#00547E" />
                 </asp:GridView>
                </td>
                            </tr>

        <tr>
            <td valign="top" align="left" style="width: 28%"><b>Reg/Admin No.</b></td>
            <td valign="top" align="left" style="width: 31%">
                <asp:TextBox ID="txtRegno" runat="server" Width="128px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
            &nbsp;
                <asp:ImageButton ID="btnNext" runat="server" 
                    ImageUrl="~/images/next.png" ImageAlign="AbsMiddle" />
            </td>
            <td width="80%" valign="top" align="left" rowspan="14">
                <br />
                <br />
                <br />
                </td>
        </tr>
        <tr>
            <td valign="top" align="left" style="width: 28%"><b>Name</b></td>
            <td valign="top" align="left" style="width: 31%">
                <asp:TextBox ID="txtName" runat="server" BorderColor="Black" BorderWidth="1px" CssClass="TextBoxFont" Width="128px"></asp:TextBox>
                &nbsp; <asp:ImageButton ID="btnNameSearch" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/images/next.png" style="height: 19px" />
            </td>
        </tr>
        <tr>
            <td valign="top" align="left" style="width: 28%">Name</td>
            <td valign="top" align="left" style="width: 31%">
                 <asp:Label ID="lblSName" runat="server" Font-Size="Small" ForeColor="Black" style="font-weight: 700"></asp:Label>
            </td>
        </tr>
        <tr>
            <td valign="top" align="left" style="width: 28%">Father</td>
            <td valign="top" align="left" style="width: 31%">
                 <asp:Label ID="lblFather" runat="server" Font-Size="Small" ForeColor="Black" style="font-weight: 700"></asp:Label>
            </td>
        </tr>
        <tr>
            <td valign="top" align="left" style="width: 28%">Class-Sec</td>
            <td valign="top" align="left" style="width: 31%">
                 <asp:Label ID="lblClass" runat="server" Font-Size="Small" ForeColor="Black" style="font-weight: 700"></asp:Label>
            </td>
        </tr>
        <tr>
            <td valign="top" align="left" colspan="2" style="text-align: center">
                &nbsp;</td>
        </tr>
        <tr>
            <td valign="top" align="left" style="width: 28%"><strong>Old Rank</strong></td>
            <td valign="top" align="left" style="width: 31%">
                <asp:DropDownList ID="cboOldArmyCat" runat="server" Width="160px"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td valign="top" align="left" style="width: 28%"><strong>Promoted Rank</strong></td>
            <td valign="top" align="left" style="width: 31%">
                <asp:DropDownList ID="cboNewArmyCat" runat="server" Width="160px"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td valign="top" align="left" style="width: 28%"><strong>Date of Promotion</strong></td>
            <td valign="top" align="left" style="width: 31%">
                <asp:TextBox ID="txtDOP" runat="server" placeholder="dd/mm/yyyy" Width="154px" BorderWidth="1px" 
                    BorderColor="Black" CssClass="TextBoxFont" AutoPostBack="True"></asp:TextBox>
                <%--<asp:CalendarExtender ID="txtDOP_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDOP">
                </asp:CalendarExtender>--%>
                </td>
        </tr>
        <tr>
            <td valign="top" align="left" style="width: 28%"><b>Term From Rank Change</b></td>
            <td valign="top" align="left" style="width: 31%">
                <asp:DropDownList ID="cboTermNo" runat="server" Width="92px" 
                    AutoPostBack="True">
                </asp:DropDownList>
                &nbsp;<asp:Label ID="lblTerm" runat="server"></asp:Label>
                </td>
        </tr>
        <tr>
            <td valign="top" align="left" height="30" style="width: 28%">
                &nbsp;</td>
            <td valign="top" align="left" style="width: 31%">
                <asp:Label ID="lblStatus" runat="server" Font-Bold="True" ForeColor="Navy" Text=""></asp:Label>
                </td>
        </tr>
        <tr>
            <td valign="top" align="left" height="30" style="width: 28%">
                &nbsp;</td>
            <td valign="top" align="left" style="width: 31%">
                <asp:Button ID="btnSave" runat="server" Text="Save" Width="67px" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" />
                &nbsp;&nbsp;
                </td>
        </tr>
        <tr>
            <td valign="middle" align="left" colspan="2" height="30">
                <asp:TextBox ID="txtID" runat="server" Visible="False" Width="65px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td valign="middle" align="left" colspan="2" height="30">
                <asp:DropDownList ID="cboFeeGroup" runat="server" Width="130px" 
                    AutoPostBack="True" Visible="False">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td valign="top" align="left" colspan="2">
                <asp:DropDownList ID="cboCategoryArmy" runat="server" Width="130px" 
                    AutoPostBack="True" Visible="False">
                </asp:DropDownList>
            </td>
            <td width="60%" valign="top" align="left" rowspan="4">
                &nbsp;</td>
        </tr>
        <tr>
            <td valign="top" align="left" colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td valign="top" align="left" colspan="2">

        <asp:TextBox ID="txtSID" runat="server" Visible="False" Width="36px"></asp:TextBox>

        <asp:TextBox ID="txtFeeGroupID" runat="server" Visible="False" Width="36px"></asp:TextBox>

        <asp:TextBox ID="txtCategoryArmyID" runat="server" Visible="False" Width="36px"></asp:TextBox>

                </td>
        </tr>
        <tr>
            <td valign="top" align="left" colspan="2">
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT RegNo, SName, ClassName, SecName FROM vw_Student WHERE (ASID = @ASID) and [SName] Like '%SearchByName%' or @SearchByName is null">
                    <SelectParameters>
                        <asp:SessionParameter Name="ASID" SessionField="ASID" />
                        <asp:ControlParameter ControlID="txtName" Name="SearchByName" PropertyName="Text" />
                    </SelectParameters>
                </asp:SqlDataSource>
                </td>
        </tr>
    </table>
</asp:Content>
