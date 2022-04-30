<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/BusMaster.master" CodeBehind="BusFeeDueConfig.aspx.vb" Inherits="iDiary_V3.BusFeeDueConfig" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BusMasterContents" runat="server">
       <table>
                <tr>
            <td class="auto-style7" style="height: 35px; width: 279px" >Term No</td>
            <td style="height: 35px" >
                <asp:DropDownList ID="cboTermNo" runat="server" CssClass="Dropdown"  
                    AutoPostBack="True">
                </asp:DropDownList>
                </td>
            <td class="auto-style7" style="height: 35px" >
                <asp:Label ID="lblTerm" runat="server" Font-Bold="True"></asp:Label>
                </td>
        </tr>

        

        <tr>
            <td class="auto-style8" style="width: 279px">Date (Fine Impose Start)</td>
            <td>
                <asp:TextBox ID="txtDepositDate" runat="server" CssClass="textbox"></asp:TextBox>
                
                <asp:CalendarExtender ID="txtDepositDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDepositDate">
                </asp:CalendarExtender>
                
                </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style9" style="height: 39px; width: 279px">Late Fee Amount</td>
            <td style="height: 39px" >
                <asp:TextBox ID="txtAmount" runat="server" TextMode="Number" CssClass="textbox"></asp:TextBox>
                </td>
            <td style="height: 39px" >
                </td>
        </tr>
        <tr>
            <td class="auto-style5" style="width: 279px; height: 38px">Processing Method</td>
            <td style="height: 38px">
                <asp:RadioButton ID="optFixed" runat="server" GroupName="R1" Text="Fixed" Checked="True" />
            &nbsp;
                <asp:RadioButton ID="optMonthly" runat="server" GroupName="R1" Text="Monthly" />
&nbsp;&nbsp;
                <asp:RadioButton ID="optDaily" runat="server" GroupName="R1" Text="Daily" />
            &nbsp;</td>
            <td class="auto-style5" style="height: 38px">
                </td>
        </tr>
        <tr>
            <td colspan="2" class="auto-style10" style="height: 36px">
                <asp:Label ID="lblStatus" runat="server" Font-Bold="True" ForeColor="Navy" Text="" style="color: #FF3300"></asp:Label>
                </td>
            <td class="auto-style10" style="height: 36px">
                </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" />
                &nbsp;<asp:Button ID="btnNew" runat="server" Text="New" CssClass="btn btn-primary" />
                &nbsp;<asp:Button ID="btnRemove" runat="server" Text="Remove" CssClass="btn btn-primary" Visible="False" />
                </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" CssClass="Grid" Width="99%" DataKeyNames="BusDueConfigID">
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" />
                        <asp:BoundField DataField="LastDate" HeaderText="Last Date" SortExpression="LastDate" DataFormatString="{0:dd/MM/yyyy}">
                        <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                       
                        <asp:BoundField DataField="LateFeeAmount" HeaderText="Late Fee Amount" SortExpression="LateFeeAmount">
                        <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <%--<asp:BoundField DataField="BusDueConfigID" SortExpression="BusDueConfigID" ReadOnly="True">
                                               </asp:BoundField>--%>
                    </Columns>
                                    </asp:GridView>
                </td>
        </tr>
        <tr>
            <td  colspan="2">
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT [LastDate], [LateFeeAmount], [BusDueConfigID]FROM [BusFeeDueConfig] WHERE ([TermNo] = 0)">
            
                </asp:SqlDataSource>
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <asp:TextBox ID="txtID" runat="server" Width="27px" Visible="False"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
     
</asp:Content>



