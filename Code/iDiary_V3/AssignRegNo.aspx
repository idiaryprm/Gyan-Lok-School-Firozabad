<%@ Page Title="Assign Reg No" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.master" EnableEventValidation="false" CodeBehind="AssignRegNo.aspx.vb" Inherits="iDiary_V3.AssignRegNo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Heading" runat="server">
    Student Master
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
   <%-- <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /> 
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /> 
    <br />
             --%>                   
                          

   <div class="col_3" style="margin-top: 20px;" id="panelEnquiryNo" runat="server">
        <div class="col-md-3 widget widget1" style="width: 100%">
            <div class="r3_counter_box">

    
    
   
  
                <table class="table">
                    <tr>

                        <td style="width: 232px">School Name</td>
                        <td colspan="3">
                           <asp:DropDownList ID="cboSchoolName" runat="server" CssClass="Dropdown" Width="300px" AutoPostBack="true" ></asp:DropDownList></td>
                        <td class="auto-style1">
                            &nbsp;</td>
                        <td style="width: 232px">Class</td>
                        <td style="width: 149px">
                            <asp:DropDownList ID="cboClass" runat="server" CssClass="Dropdown"
                                AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 257px">
                            &nbsp;</td>
                    </tr>
                    <tr>

                        <td style="width: 232px">Section</td>
                        <td style="width: 149px">
                            <asp:DropDownList ID="cboSection" runat="server" CssClass="Dropdown">
                            </asp:DropDownList>
                        </td>
                        <td>&nbsp;</td>
                        <td style="width: 257px">
                            &nbsp;</td>
                        <td class="auto-style1">
                            &nbsp;</td>
                        <td style="width: 232px">Status</td>
                        <td style="width: 149px">
                            <asp:DropDownList ID="cboStatus" runat="server" CssClass="Dropdown">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 257px">
                            <asp:Button ID="btnShow" runat="server" CssClass="btn btn-primary" Text="Next" />
                        </td>
                    </tr>
                    <tr>

                        
                        <td colspan="4">
                            <asp:Label ID="lblStatus" runat="server" Font-Bold="True" ForeColor="Navy" style="color: #FF0000"></asp:Label>
                        </td>
                        
                        <td class="auto-style1"></td>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>

                        <td colspan="8">
                           <div id="gvDiv">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="SID,RegNo" CssClass="Grid" GridLines="Horizontal" DataSourceID="SqlDataSource1" Width="100%" Visible="False" >
                    
                    <Columns>
                       <%-- <asp:TemplateField HeaderText="Select">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelect" runat="server" Visible="true"  />
                                                          </ItemTemplate>
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="S. No.">
         <ItemTemplate>
               <%# Container.DataItemIndex + 1 %>
         </ItemTemplate>
          <ItemStyle Width="50px" />
     </asp:TemplateField>
                         <%--<asp:BoundField DataField="EnquiryNo" HeaderText="Form No." SortExpression="EnquiryNo" />--%>
                        <asp:BoundField DataField="Sname" HeaderText="Student Name" SortExpression="Sname" />
                        <asp:BoundField DataField="Fname" HeaderText="Father Name" SortExpression="Fname" />
                        <asp:TemplateField HeaderText="Reg No">
                            <ItemTemplate>
                                <asp:textBox ID="txtRegNo" runat="server" Width="90px" Visible="true"  />
                                                          </ItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:BoundField DataField="RegNo" HeaderText="RegNo" SortExpression="RegNo" />--%>
                        <asp:BoundField DataField="FeeBookNo" HeaderText="FeeBook No" SortExpression="FeeBookNo" />
                        <%--<asp:BoundField DataField="ClassName" HeaderText="Class Name" SortExpression="ClassName" />--%>
                        <asp:BoundField DataField="MobNo" HeaderText="Mobile No" SortExpression="MobNo" />
                        <%--<asp:BoundField DataField="address" HeaderText="Address" SortExpression="address" />--%>
                        <asp:BoundField DataField="AdmissionDate" HeaderText="Admission Date" SortExpression="AdmissionDate" DataFormatString="{0:dd/MM/yyyy}" />
                      

                        <%-- <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Section"  ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:DropDownlist ID="cboSec" runat="server" AutoPostBack="true" CssClass="Dropdown" OnTextChanged="cboSec_SelectedIndexChanged" RowIndex='<%# Container.DisplayIndex %>' Width="60px"></asp:DropDownlist>
                            </ItemTemplate>
                        </asp:TemplateField>
                      
                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Sub Section"  ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:DropDownlist ID="cboSubSec" runat="server" CssClass="Dropdown" RowIndex='<%# Container.DisplayIndex %>' Width="100px"></asp:DropDownlist>
                            </ItemTemplate>
                        </asp:TemplateField>--%>

                    </Columns>
                    <HeaderStyle HorizontalAlign="Center" />
                    <%--<SelectedRowStyle BackColor="#4DE427" />
                    <FooterStyle BackColor="#ccff99" Font-Bold="True" />--%>
                </asp:GridView>
                                </div>
                        </td>
                    </tr>
                    <tr>

                        <td colspan="5" style="height: 38px">
                            
                            <asp:Button ID="btnAsignNo" runat="server" CssClass="btn btn-primary" Text="Save" />
                            &nbsp;
                            
                            <asp:Button ID="btnprint" runat="server" CssClass="btn btn-primary" Text="Print" />&nbsp;&nbsp;<asp:Button ID="btnexporttoexcel" runat="server" CssClass="btn btn-primary" Text="Export To Excel" />&nbsp;&nbsp;&nbsp;</td>
                        <td style="width: 140px; height: 38px;"></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>

                        <td colspan="5">
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" 
                    SelectCommand="SELECT * FROM [vw_Student] where SID=0"></asp:SqlDataSource>
                        </td>
                        <td style="width: 140px">&nbsp;</td>
                        <td></td>
                        <td></td>
                    </tr>
                </table>
    </div>
        </div>
        <div class="clearfix"></div>
    </div>
   
</asp:Content>
<asp:Content ID="Content3" runat="server" contentplaceholderid="head">
    <style type="text/css">
        .auto-style1 {
            width: 68px;
        }
    </style>
</asp:Content>

