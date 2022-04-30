<%@ Page Language="VB" MasterPageFile="~/iDiaryPanel/Parent/ParentMaster.master" AutoEventWireup="false" Inherits="iDiary_V3.Parent_FeeHistory" title="Untitled Page" Codebehind="FeeHistory.aspx.vb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Heading" runat="server">
    Fee Configuration
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="col_3" style="margin-top: 20px;" id="panelEnquiryNo" runat="server">
        <div class="col-md-3 widget widget1" style="width: 100%">
            <div class="r3_counter_box">
                <table class="table">
                    <tr>
                        <td style="width: 98px"><font style="font-size:1.3em; font-weight:700">Session</font></td>
                        <td style="width: 219px">
                            <asp:DropDownList ID="cboSession" runat="server" Width="200px"></asp:DropDownList></td>
                        <td style="width: 10px">&nbsp;</td>
                        <td style="width: 100px">
                            <asp:Button ID="btnGenerate" runat="server" Text="Show" CssClass="btn btn-sm btn-primary" />
                        </td>
                        <td align="left"><asp:Label ID="lblStatus" runat="server" Font-Bold="True" ForeColor="Navy"></asp:Label></td>
                    </tr>
                   
                </table>

                 
                 <font style="font-size:1.3em; font-weight:700">Payment Details</font>
                <br />
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="Grid" DataKeyNames="FeeDepositID" DataSourceID="SqlDataSource2" Width="98%" AutoGenerateSelectButton="True">
                    <Columns>
                        <asp:BoundField DataField="DepositDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Date" HtmlEncode="False" SortExpression="DepositDate">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Term">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="DepositAmount" HeaderText="Amount" SortExpression="DepositAmount">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ConcessionAmount" HeaderText="Concession" SortExpression="ConcessionAmount">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                    </Columns>
                    <HeaderStyle HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#4DE427" />
                </asp:GridView>
                
                <div id="gvdiv" runat="server">
                    <font style="font-size:1.3em; font-weight:700">Configuration</font>
                    <br />
                     <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CssClass="Grid" DataKeyNames="TermID" DataSourceID="SqlDataSource3" ShowFooter="True" Width="98%" >
                    <Columns>
                        <asp:BoundField DataField="TermID" HeaderText="TermID" Visible="False">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="TermNo" HeaderText="TermNo" SortExpression="TermNo">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>

                        </asp:BoundField>
                        <asp:BoundField DataFormatString="{0:dd/MM/yyyy}" HeaderText="Due Date">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Config">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Deposit Date">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Deposit">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Concession">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Excess">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Due">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                        </asp:BoundField>
                    </Columns>
                    <FooterStyle HorizontalAlign="Center" BackColor="#ccff99" Font-Bold="True" />
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:GridView>
                </div>
               

                <br />

                <asp:TextBox ID="txtSID" runat="server" Visible="False" Width="36px"></asp:TextBox>

                <asp:TextBox ID="txtFeeGroupID" runat="server" Visible="False" Width="36px"></asp:TextBox>

                <asp:TextBox ID="txtConfigType" runat="server" Visible="False" Width="36px"></asp:TextBox>

                <asp:TextBox ID="txtCategoryArmyID" runat="server" Visible="False" Width="36px"></asp:TextBox>

                <asp:TextBox ID="txtClassName" runat="server" Visible="False" Width="36px"></asp:TextBox>

                <asp:TextBox ID="txtSecName" runat="server" Visible="False" Width="36px"></asp:TextBox>

                <asp:TextBox ID="txtASID" runat="server" Visible="False" Width="36px"></asp:TextBox>

                <asp:TextBox ID="txtAdmissionFeeID" runat="server" Visible="False" Width="36px"></asp:TextBox>

                <asp:TextBox ID="txtAdminFlag" runat="server" Visible="False" Width="36px"></asp:TextBox>

                <asp:TextBox ID="txtAdminFeeApplicable" runat="server" Visible="False" Width="37px"></asp:TextBox>

                <asp:TextBox ID="txtExcessFeeID" runat="server" ReadOnly="True" Visible="False" Width="22px"></asp:TextBox>

                <br />

                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT [FeeDepositID],[DepositDate], Sum([FeeDepositAmount]) as DepositAmount,Sum([ConcessionAmount]) as ConcessionAmount from vw_FeeDeposit where SID<0 group by [FeeDepositID],[DepositDate] order by [DepositDate] DESC"></asp:SqlDataSource>

                <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT TermID,[TermNo],TermName FROM TermMaster where FeeGroupID=0"></asp:SqlDataSource>
            </div>
        </div>
        <div class="clearfix"></div>
    </div>
</asp:Content>