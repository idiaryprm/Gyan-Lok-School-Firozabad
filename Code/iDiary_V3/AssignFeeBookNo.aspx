<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/FeeMasterPage.master" EnableEventValidation="false" CodeBehind="AssignFeeBookNo.aspx.vb" Inherits="iDiary_V3.AssignFeeBookNo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FeeMasterContents" runat="server">
    <script type="text/javascript">
        function print() {
            var printFriendly = document.getElementById("gvDiv")
            var printWin = window.open("about:blank", "Voucher", "menubar=no;status=no;toolbar=no;");
            printWin.document.write("<html><head><title>Student Fee Book No. Report</title></head><body>" + printFriendly.innerHTML + "</body></html>");
            printWin.document.close();
            printWin.window.print();
            printWin.close();
        }

    </script>
<%--    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />  --%> 

   
    
    
   
  
                <table class="table">
                    <tr>

                        <td style="width: 232px">School Name</td>
                        <td colspan="2">
                            <asp:DropDownList ID="cboSchoolName" runat="server" CssClass="Dropdown" Width="300px" AutoPostBack="true" ></asp:DropDownList>
                        </td>
                        <td style="width: 257px">
                            &nbsp;</td>
                        <td style="width: 94px">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td style="width: 140px">
                            &nbsp;</td>
                    </tr>
                    <tr>

                        <td style="width: 232px">Class</td>
                        <td style="width: 149px">
                            <asp:DropDownList ID="cboClass" runat="server" CssClass="Dropdown"
                                AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 140px">&nbsp;</td>
                        <td style="width: 257px">
                            Section</td>
                        <td style="width: 94px">
                            <asp:DropDownList ID="cboSection" runat="server" CssClass="Dropdown">
                            </asp:DropDownList>
                        </td>
                        <td>
                            &nbsp;</td>
                        <td style="width: 140px">
                            &nbsp;</td>
                    </tr>
                    <tr>

                        <td style="width: 232px">Status</td>
                        <td style="width: 149px">
                            <asp:DropDownList ID="cboStatus" runat="server" CssClass="Dropdown">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 140px">
                            <asp:Button ID="btnShow" runat="server" CssClass="btn btn-primary" Height="30px" Text="Next" />
                        </td>
                        <td style="width: 257px">
                            <asp:Label ID="lblFeeBookSeries" runat="server" Text="Series Starts From" Visible="False"></asp:Label>
                        </td>
                        <td style="width: 94px">
                            <asp:TextBox ID="txtFeeBooknoStart" runat="server" CssClass="textbox" Visible="False"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="btnAsignNoSeries" runat="server" CssClass="btn btn-primary" Text="Next" Height="30px" Visible="False" />
                        </td>
                        <td style="width: 140px">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="7">
                            <asp:Label ID="lblStatus" runat="server" Font-Bold="True" ForeColor="Navy" style="color: #FF0000"></asp:Label>
                        </td>
                    </tr>
                    <tr>

                        <td colspan="7">
                           <div id="gvDiv">
                            <asp:Table ID="myTable" runat="server" CellPadding="0" CellSpacing="0"
                                Width="100%" BorderColor="Black" BorderWidth="1px" GridLines="Both">
                            </asp:Table>
                                </div>
                        </td>
                    </tr>
                    <tr>

                        <td colspan="5" style="height: 38px">
                            
                            <asp:Button ID="btnAsignNo" runat="server" CssClass="btn btn-primary" Text="Save" />
                            &nbsp;
                            
                            <asp:Button ID="btnprint" runat="server" CssClass="btn btn-primary" Text="Print" />&nbsp;&nbsp;<asp:Button ID="btnexporttoexcel" runat="server" CssClass="btn btn-primary" Text="Export To Excel" />&nbsp;&nbsp;&nbsp;</td>
                        <td style="width: 140px; height: 38px;"></td>
                    </tr>
                    <tr>

                        <td colspan="5">
                            <asp:SqlDataSource ID="SqlDataSourceStudents" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT [RegNo], [SName], [ClassRollno], [ClassName], [SecName] FROM [vw_Student] WHERE ([ASID] = @ASID) AND ([STATUSNAME]= @STATUSNAME)" UpdateCommand="UPDATE Student SET ClassRollno = @ClassRollno WHERE (RegNo = @regno)">
                                <SelectParameters>
                                    <asp:SessionParameter Name="ASID" SessionField="ASID" Type="Int32" />
                                    <asp:ControlParameter ControlID="cboStatus" Name="STATUSNAME" PropertyName="SelectedValue" />
                                </SelectParameters>
                                <UpdateParameters>
                                    <asp:Parameter Name="ClassRollno" />
                                    <asp:Parameter Name="regno" />

                                </UpdateParameters>
                            </asp:SqlDataSource>
                        </td>
                        <td style="width: 140px">&nbsp;</td>
                    </tr>
                </table>
    
   
</asp:Content>
