<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/FeeMasterPage.master" CodeBehind="FeeDueConfig.aspx.vb" Inherits="iDiary_V3.FeeDueConfig" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FeeMasterContents" runat="server">
    <%--<br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br />--%>
    <table Class="table">
        <tr>
            <td width="25%" style="height: 20px">Fee Group</td>
            <td style="width: 26%; height: 20px;">
                <asp:DropDownList ID="cboFeeGroup" runat="server" CssClass="Dropdown" 
                    AutoPostBack="True">
                </asp:DropDownList>
                </td>
            <td style="height: 20px; width: 23%;">
                Installment No</td>
            <td width="45%" style="height: 20px">
                <asp:DropDownList ID="cboTermNo" runat="server" CssClass="Dropdown" 
                    AutoPostBack="True">
                </asp:DropDownList>
            </td>
        </tr>

        

        <tr>
            <td width="25%">Fine Applicable Date From </td>
            <td style="width: 26%">
                <asp:TextBox ID="txtDepositDate" runat="server" CssClass="textbox"></asp:TextBox>
                 <asp:CalendarExtender ID="txtDepositDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDepositDate">
                </asp:CalendarExtender>
                <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999" MaskType="Date" TargetControlID="txtDepositDate" PromptCharacter="_"> </asp:MaskedEditExtender>
                </td>
            <td style="width: 23%">
                &nbsp;</td>
            <td width="45%">
                <asp:Label ID="lblTerm" runat="server" Font-Bold="True"></asp:Label>
            </td>
        </tr>

        

        <tr>
            <td width="25%">Late Fee Amount</td>
            <td style="width: 26%">
                <asp:TextBox ID="txtAmount" runat="server" CssClass="textbox"></asp:TextBox>
                </td>
            <td style="width: 23%">Processing Method</td>
            <td width="45%">
                <asp:RadioButton ID="optFixed" runat="server" GroupName="R1" Text="Fixed" />
                <asp:RadioButton ID="optMonthly" runat="server" GroupName="R1" Text="Monthly" />
                <asp:RadioButton ID="optDaily" runat="server" GroupName="R1" Text="Daily" />
            </td>
        </tr>
        <tr>
            <td width="25%" colspan="3">&nbsp;&nbsp;<asp:Label ID="lblStatus" runat="server" Font-Bold="True" ForeColor="Navy" Text="" style="color: #FF3300"></asp:Label>
            </td>
            <td width="45%"><asp:TextBox ID="txtID" runat="server" Width="27px" Visible="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td width="25%" align="right">
                &nbsp;</td>
            <td colspan="2">
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" />
                &nbsp;<asp:Button ID="btnNew" runat="server" Text="New" CssClass="btn btn-primary" />
                &nbsp;
                <asp:Button ID="btnRemove" runat="server" Text="Remove" CssClass="btn btn-primary" />
                 &nbsp;
                <asp:Button ID="btnimportPrevDue" runat="server" Text="Import Previous Due" CssClass="btn btn-primary" />
                </td>
            <td width="45%">
                &nbsp;</td>
        </tr>
        <tr>
            <td  colspan="4">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"  DataSourceID="SqlDataSource1" width="80%" CssClass="Grid" DataKeyNames="DueConfigID">
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" />
                        <asp:BoundField DataField="LastDate" HeaderText="Last Date" SortExpression="LastDate" DataFormatString="{0:dd/MM/yyyy}">
                        <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                      
                        <asp:BoundField DataField="LateFeeAmount" HeaderText="Late Fee Amount" SortExpression="LateFeeAmount">
                        <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <%--<asp:BoundField DataField="DueConfigID" SortExpression="DueConfigID" ReadOnly="True">
                        
                        </asp:BoundField>--%>
                    </Columns>
                  
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT [LastDate],  [LateFeeAmount], [DueConfigID] FROM [vwFeeDueConfig] WHERE [DueConfigID]<0">
                    
                </asp:SqlDataSource></td>
        </tr>
    </table>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
</asp:Content>
