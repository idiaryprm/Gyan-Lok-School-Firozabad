<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="PettyCashJournalEntry.aspx.vb" Inherits="iDiary_V3.PettyCashJournalEntry" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Heading" runat="server">
       Journal Voucher
&nbsp;  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" cellpadding="2" cellspacing="2" border="0">
        <tr>
            <td style="width: 24%">Vr_ No</td>
            <td style="width: 30%">
                <asp:TextBox ID="txtVRNo" runat="server" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px" Width="148px"></asp:TextBox>
            &nbsp;<asp:ImageButton ID="btnNext" runat="server" 
                    ImageUrl="~/images/next.png" ImageAlign="AbsMiddle" />
            </td>
            
            <td valign="top" style="width: 42%">
                VR_DT</td>
            
            <td width="65%" valign="top">
                <asp:TextBox ID="txtVRDT" runat="server" AutoPostBack="True" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px" Width="148px" Height="16px"></asp:TextBox>
                <asp:CalendarExtender ID="txtVRDT_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="txtVRDT">
                </asp:CalendarExtender>
            </td>
        </tr>

        <tr>
            <td style="width: 24%">
                Payment Head</td>
            <td style="width: 30%">
                <asp:DropDownList ID="cboHeadPV" runat="server" class="form-control1">
                </asp:DropDownList>
            </td>
           
            <td valign="top" style="width: 42%">
                Receipt Head</td>
           
            <td width="65%" valign="top">
                <asp:DropDownList ID="cboHeadRV" runat="server" class="form-control1">
                </asp:DropDownList>
            </td>
        </tr>

        <tr>
            <td style="width: 24%">
                ChqNo</td>
            <td style="width: 30%">
                <asp:TextBox ID="txtChqNo" runat="server" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px" Width="148px"></asp:TextBox>
            </td>
            <td valign="top" style="width: 42%">
                Amount</td>
            <td width="65%" valign="top">
                <asp:TextBox ID="txtAmount" runat="server" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px" Width="148px"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td style="width: 24%">
                <asp:Label ID="lblPaidReceived" runat="server"></asp:Label>
            </td>
            <td style="width: 30%">
                <asp:TextBox ID="txtPaidReciveDetail" runat="server" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px" Width="249px"></asp:TextBox>
            </td>
            <td valign="top" style="width: 42%">
                On What Account</td>
            <td width="65%" valign="top">
                <asp:TextBox ID="txtOnWhatAccount" runat="server" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px" Width="249px"></asp:TextBox>
            </td>
        </tr>

      

        <tr>
            <td colspan="2">
                <asp:Label ID="lblStatus" runat="server" Font-Bold="True" ForeColor="Navy" style="color: #FF0000"></asp:Label>
            </td>
            <td valign="top" style="width: 42%">
                &nbsp;</td>
            <td width="65%" valign="top">
                &nbsp;</td>
        </tr>

      

        <tr>
            <td style="width: 24%">
                <asp:TextBox ID="txtID" runat="server" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px" Width="39px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="txtIDOther" runat="server" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px" Width="39px" Visible="False"></asp:TextBox>
                              </td>
            <td style="width: 30%" valign="top" >
                <asp:Button ID="btnSave" runat="server" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px" Text="Save" Width="71px" />
                &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnSlip" runat="server" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px" Text="Generate Slip" Width="135px" Visible="False" />
                &nbsp;<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            </td>
            <td valign="top" style="width: 42%">
                &nbsp;</td>
            <td width="65%" valign="top">
                &nbsp;</td>
        </tr>

        <tr>
            <td style="width: 24%">
                &nbsp;</td>
            <td style="width: 30%" valign="top" >
                &nbsp;</td>
            <td valign="top" style="width: 42%">
                &nbsp;</td>
            <td width="65%" valign="top">
                &nbsp;</td>
        </tr>

        <tr>
            <td style="width: 24%">
                &nbsp;</td>
            <td style="width: 30%" valign="top" >
                &nbsp;</td>
            <td valign="top" style="width: 42%">
                &nbsp;</td>
            <td width="65%" valign="top">
                &nbsp;</td>
        </tr>

        <tr>
            <td style="width: 24%">
                &nbsp;</td>
            <td style="width: 30%" valign="top" >
                &nbsp;</td>
            <td valign="top" style="width: 42%">
                &nbsp;</td>
            <td width="65%" valign="top">
                &nbsp;</td>
        </tr>

        <tr>
            <td style="width: 24%">
                &nbsp;</td>
            <td style="width: 30%" valign="top" >
                &nbsp;</td>
            <td valign="top" style="width: 42%">
                &nbsp;</td>
            <td width="65%" valign="top">
                &nbsp;</td>
        </tr>

        <tr>
            <td style="width: 24%">
                &nbsp;</td>
            <td style="width: 30%" valign="top" >
                &nbsp;</td>
            <td valign="top" style="width: 42%">
                &nbsp;</td>
            <td width="65%" valign="top">
                &nbsp;</td>
        </tr>

        <tr>
            <td style="width: 24%">
                &nbsp;</td>
            <td style="width: 30%" valign="top" >
                &nbsp;</td>
            <td valign="top" style="width: 42%">
                &nbsp;</td>
            <td width="65%" valign="top">
                &nbsp;</td>
        </tr>

        <tr>
            <%--<td colspan="5">
                <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" Width="100%" AutoGenerateColumns="False" DataKeyNames="TransID" DataSourceID="SqlDataSource1">
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" />
                        <asp:BoundField DataField="TransID" HeaderText="ID" ReadOnly="True" SortExpression="TransID">
                        <ItemStyle Width="50px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="TransDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Date" HtmlEncode="False" ReadOnly="True" SortExpression="TransDate">
                        <ItemStyle Width="80px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="TransTypeName" HeaderText="Transaction Type" ReadOnly="True" SortExpression="TransTypeName" />
                        <asp:BoundField DataField="PCHeadName" HeaderText="Head Name" ReadOnly="True" SortExpression="PCHeadName" />
                        <asp:BoundField DataField="NameOf" HeaderText="In Favour of" SortExpression="NameOf" />
                        <asp:BoundField DataField="AccountDetails" HeaderText="Account Details" SortExpression="AccountDetails" />
                        <asp:BoundField DataField="TransAmount" HeaderText="Amount" SortExpression="TransAmount" />
                        <asp:BoundField DataField="TransRemark" HeaderText="Remark" SortExpression="TransRemark" />
                    </Columns>
                    <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                    <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                    <RowStyle BackColor="White" ForeColor="#330099" />
                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                    <SortedAscendingCellStyle BackColor="#FEFCEB" />
                    <SortedAscendingHeaderStyle BackColor="#AF0101" />
                    <SortedDescendingCellStyle BackColor="#F6F0C0" />
                    <SortedDescendingHeaderStyle BackColor="#7E0000" />
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT [TransID], [TransDate], [TransTypeName], [PCHeadName], [NameOf],[TransAmount],[AccountDetails], [TransRemark] FROM [vwPettyCashTransaction] Where TransTypeName=@TransName AND PCHeadName=@HeadName">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="cboTrans" Name="TransName" PropertyName="text" />
                        <asp:ControlParameter ControlID="cboHead" Name="HeadName" PropertyName="Text" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>--%>
        </tr>

        <tr>
            <td colspan="5">
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="272px" Width="100%" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" CssClass="aspNetDisabled">
                    <LocalReport ReportEmbeddedResource="iDiary_V3.rptPettyCash.rdlc" ReportPath="rptPettyCash.rdlc">
                    </LocalReport>
                </rsweb:ReportViewer>
            </td>
        </tr>

        <tr>
            <td colspan="3">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td style="width: 75%">
                &nbsp;</td>
        </tr>

    </table>
</asp:Content>
