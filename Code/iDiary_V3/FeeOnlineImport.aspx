<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="FeeOnlineImport.aspx.vb" Inherits="iDiary_V3.FeeOnlineImport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Heading" runat="server">
    Verify Online Transactions 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" cellpadding="2" cellspacing="2" border="0">
        <tr>
            <td style="width: 17%"><b>Payment Status</b></td>
            <td style="width: 25%">
                <asp:DropDownList ID="cboPaymentStatus" runat="server" Width="166px">
                    <asp:ListItem Value="Pending">Pending</asp:ListItem>
                    <asp:ListItem>Verified</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style="width: 12%">
                <asp:Button ID="btnShow" runat="server" Text="Show Report" Width="123px" />
            </td>

            <td style="width: 27%">
                &nbsp;</td>

            <td style="width: 12%">
                &nbsp;</td>

            <td width="60%">
                &nbsp;</td>

            <td width="60%">
                &nbsp;</td>

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
               
                <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False" BackColor="White"  DataKeyNames="FDOID" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" Height="16px" Width="100%">
                    <RowStyle BackColor="White" ForeColor="#330099" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelect" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="FDOID" 
                            HeaderText="FFDOID" Visible="False" SortExpression="FDOID">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="RegNo" HeaderText="Reg No" SortExpression="RegNo" />
                        <asp:BoundField DataField="FeeBookNo" HeaderText="Fee Book No" SortExpression="FeeBookNo" />
                        <asp:BoundField DataField="SName" HeaderText="Student" SortExpression="SName" />
                        <asp:BoundField DataField="ClassName" HeaderText="Class" SortExpression="ClassName" />
                        <asp:BoundField DataField="SecName" HeaderText="Sec" SortExpression="SecName" />
                         <asp:BoundField DataField="TermNo" 
                            HeaderText="TermNo" SortExpression="TermNo">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                         <asp:BoundField DataField="TransactionID" 
                            HeaderText="TransactionID" SortExpression="TransactionID">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PaymentDate" DataFormatString="{0:dd/MM/yyyy hh:mm}" 
                            HeaderText="PaymentDate" HtmlEncode="False" SortExpression="PaymentDate">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="Amount" HeaderText="Amount" 
                            SortExpression="Amount">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PaymentResponse" HeaderText="Response" 
                            SortExpression="PaymentResponse">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                    </Columns>
                    <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                    <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                </asp:GridView>
                <br />
                <asp:Button ID="btnAdd" runat="server" Text="Verify Tarnsactions" Width="200px" />
            
    

                <br />
                <asp:TextBox ID="txtAdmissionFeeID" runat="server" BorderStyle="Solid" BorderWidth="1px" ReadOnly="True" Visible="False" Width="22px"></asp:TextBox>
                <asp:TextBox ID="txtLateFeeID" runat="server" BorderStyle="Solid" BorderWidth="1px" ReadOnly="True" Visible="False" Width="22px"></asp:TextBox>
                <asp:TextBox ID="txtTutionFeeID" runat="server" BorderStyle="Solid" BorderWidth="1px" Height="16px" ReadOnly="True" Visible="False" Width="22px"></asp:TextBox>
                <asp:TextBox ID="txtConveyanceFeeID" runat="server" BorderStyle="Solid" BorderWidth="1px" ReadOnly="True" Visible="False" Width="22px"></asp:TextBox>
            
    

                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT FDOID,RegNo,FeeBookNo,SName,FName,ClassName,SecName,TermNo,TransactionID,PaymentDate, Amount,PaymentResponse FROM [vw_FeeDepositOnline] where ASID=0 Order by PaymentDate DESC">
                   
                </asp:SqlDataSource>
    
            </td>

        </tr>
    </table>
</asp:Content>
