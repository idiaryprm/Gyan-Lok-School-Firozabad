<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="FeePaymentHistory.aspx.vb" Inherits="iDiary_V3.FeePaymentHistory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Heading" runat="server">
    Fee Payment History
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <table border="0" cellpadding="2" cellspacing="2" width="100%">
        <tr>
            <td style="width: 15%"><b>Admn No.</b></td>
            <td width="25%">
                
                <asp:TextBox ID="txtRegNo" runat="server" CssClass="textbox"></asp:TextBox>
                &nbsp;
                <asp:Button ID="btnAdminNoNext" runat="server" Text="&gt;&gt;" CssClass="hvr-glow" />
                
            </td>
            <td width="50%">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 15%"><b>FeeBook No</b></td>
            <td width="25%">
                
                <asp:TextBox ID="txtFeeBookNo" runat="server" CssClass="textbox"></asp:TextBox>
                &nbsp;
                <asp:Button ID="btnNext" runat="server" Text="&gt;&gt;" CssClass="hvr-glow" />
                
            </td>
            <td width="50%">
                
                <asp:Label ID="lblStatus" runat="server" Font-Bold="True" style="color: #FF0000"></asp:Label>
                
            </td>
        </tr>
    </table>
    
    <div id="gvDiv">
        <table border="0" width="100%">
            <tr>
                <td align="center">
                    <asp:Label ID="lblSchoolName" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
                    <br />
                    <asp:Label ID="lblTitle" runat="server" Font-Bold="True" Font-Size="Small"></asp:Label>
                                </td>
            </tr>
        </table>

        <table border="0" cellpadding="2" cellspacing="2" width="100%">
        <tr>
            <td style="width: 11%; height: 20px;">Admn No.</td>
            <td style="width: 11%; height: 20px;"><asp:Label ID="lblRegNo" runat="server" style="font-weight: bold"></asp:Label></td>
            <td width="15%" style="height: 20px">Fee Book No</td>
            <td style="width: 12%; height: 20px;"><asp:Label ID="lblFeeBookNo" runat="server" style="font-weight: bold"></asp:Label></td>
            <td width="15%" style="height: 20px">Class / Section</td>
            <td style="width: 121px; height: 20px;"><asp:Label ID="lblClass" runat="server" style="font-weight: bold"></asp:Label></td>
            <td width="15%" style="text-align: center; height: 20px;"><b>Fee Details</b></td>
        </tr>
        
        <tr>
            <td style="width: 11%">Student Name</td>
            <td style="width: 11%"><asp:Label ID="lblSName" runat="server" style="font-weight: bold"></asp:Label></td>
            <td width="15%">Father's Name</td>
            <td style="width: 12%"><asp:Label ID="lblFatherName" runat="server" style="font-weight: bold"></asp:Label></td>
            <td width="15%">&nbsp;</td>
            <td style="width: 121px">&nbsp;</td>
            <td width="30%" rowspan="1" valign="top" style="text-align: center">
                <asp:Label ID="lblTermNo" runat="server" style="font-weight: bold"></asp:Label>
                </td>
        </tr>
        
        <tr>
            <td colspan="6" valign="top">
                    <asp:GridView ID="GridView1" runat="server" DataKeyNames="FeeDepositID" AutoGenerateColumns="False" Width="98%" CssClass="Grid" DataSourceID="SqlDataSource1" >
                    <Columns>
                        <asp:BoundField DataField="DepositDate" DataFormatString="{0:dd/MM/yyyy}" 
                            HeaderText="DepositDate" HtmlEncode="False" SortExpression="DepositDate">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="TermNo" 
                            HeaderText="TermNo" SortExpression="TermNo">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="DepositAmount" HeaderText="DepositAmount" 
                            SortExpression="DepositAmount">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="DepositDetails" HeaderText="DepositDetails" 
                            SortExpression="DepositDetails">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:ButtonField ButtonType="Button" Text="Detail" />
                    </Columns>
                   
                </asp:GridView>
                    <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="hvr-glow" />
                
                &nbsp;&nbsp;
                <asp:Button ID="btnExcel" runat="server" Text="Export to Excel" CssClass="hvr-glow" />
                
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" 
                    SelectCommand="SELECT [FeeDepositID],[DepositDate], [TermNo], [DepositAmount], [DepositMode], [DepositDetails] FROM [rptFeeHistory]">
                </asp:SqlDataSource>
            
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT [FeeTypeName], [FeeDepositAmount] FROM vw_FeeDeposit where FeeDepositID=0">
                    
                </asp:SqlDataSource>
            
            </td>
            <td style="text-align: center" valign="top">
                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CssClass="Grid" DataSourceID="SqlDataSource2" Width="98%">
                    <Columns>
                       
                        <asp:BoundField DataField="FeeTypeName" HeaderText="Fee Head" 
                            SortExpression="FeeTypeName">
                        </asp:BoundField>
                        <asp:BoundField DataField="FeeDepositAmount" HeaderText="Amount" 
                            SortExpression="FeeDepositAmount">
                        </asp:BoundField>
                    </Columns>
                  
                </asp:GridView>
                </td>
        </tr>
        
    </table>
    
    </div>

</asp:Content>
