<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="BusStudentList.aspx.vb" Inherits="iDiary_V3.BusStudentList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Heading" runat="server">
    Bus Student List 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
         <div class="col_3" style="margin-top: 20px;" id="panelEnquiryNo" runat="server">
        <div class="col-md-3 widget widget1" style="width: 100%">
            <div class="r3_counter_box">
    <table border="0">
        <tr>
            <td style="width: 17%">
                Class</td>
            <td style="margin-left: 80px; width: 18%;">
                <asp:DropDownList ID="cboClass" runat="server" AutoPostBack="True" 
                    CssClass="Dropdown">
                </asp:DropDownList>
            </td>
            <td style="width: 17%">
                Section</td>
            <td style="width: 16%">
                <asp:DropDownList ID="cboSection" runat="server" CssClass="Dropdown">
                </asp:DropDownList>
            </td>
            <td style="width: 14%">
                Status</td>
            <td width="10%">
                <asp:DropDownList ID="cboStatus" runat="server" CssClass="Dropdown">
                </asp:DropDownList>
            </td>
            <td width="5%">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 17%">
                Term</td>
            <td style="width: 18%">
                <asp:DropDownList ID="cboTerm" runat="server" CssClass="Dropdown">
                    <%--<asp:ListItem>April - June (Term-1)April - June (Term-1)</asp:ListItem>
                    <asp:ListItem>July - September (Term-2)July - September (Term-2)</asp:ListItem>
                    <asp:ListItem>October - December (Term-3)October - December (Term-3)</asp:ListItem>
                    <asp:ListItem>January - March (Term-4)January - March (Term-4)</asp:ListItem>
                    <asp:ListItem>Full YearFull Year</asp:ListItem>
                    <asp:ListItem Selected="True">-</asp:ListItem>--%>
                </asp:DropDownList>
            </td>
            <td style="width: 17%">
                Bus</td>
            <td style="width: 16%">
                                 <asp:DropDownList ID="cboBus" runat="server" CssClass="Dropdown">
                                 </asp:DropDownList>
            </td>
            <td style="width: 14%">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td width="5%">
                &nbsp;</td>
        </tr>
            <tr>
            <td height="25" style="text-decoration: underline;" colspan="2">
                Choose Display Criteria :</td>

            <td style="width: 17%">
                &nbsp;</td>
            <td style="width: 16%">
                &nbsp;</td>

            <td style="width: 14%">
                &nbsp;</td>
            <td width="15%">
                &nbsp;</td>
           
            <td width="10%" align="right">
                &nbsp;</td>
        </tr>

        <tr>
            <td height="25" style="width: 17%">
                <asp:CheckBox ID="chkFBnoC" runat="server" Text="Fee Book No" Checked="True" />
            </td>
            <td style="width: 18%">
                <asp:CheckBox ID="chkSNameC" runat="server" Text="Student Name" Checked="True" />
            </td>

            <td style="width: 17%">
                <asp:CheckBox ID="chkFNameC" runat="server" Text="Father Name" Checked="True" />
            </td>
            <td style="width: 16%">
                <asp:CheckBox ID="chkGenderC" runat="server" Text="Gender" Checked="True" />
            </td>

            <td style="width: 14%">
                &nbsp;<asp:CheckBox ID="chkClassC" runat="server" Text="Class" Checked="True" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;</td>
            <td width="15%">
                <asp:CheckBox ID="chkSectionC" runat="server" Text="Section" Checked="True" />
            </td>
           
            <td width="10%" align="right">
                &nbsp;</td>
        </tr>

        <tr>
            <td style="width: 17%; height: 25px;">
                <asp:CheckBox ID="chkDobC" runat="server" Text="Date of Birth" Checked="True" />
            </td>
            <td style="height: 25px; width: 18%">
                <asp:CheckBox ID="chkAddressC" runat="server" Text="Address" Checked="True" />
            </td>

            <td style="width: 17%; height: 25px;">
                <asp:CheckBox ID="chkMobileC" runat="server" Text="Mobile" Checked="True" />
            </td>
            <td style="height: 25px; width: 16%">
                <asp:CheckBox ID="chkBusC" runat="server" Text="Bus" Checked="True" />
            </td>

            <td style="height: 25px; width: 14%">
            </td>
            <td width="15%" style="height: 25px">
            </td>
           
            <td width="10%" align="right" style="height: 25px">
                </td>
        </tr>

        <tr>
            <td colspan="3">
                <asp:Label ID="lblStatus" runat="server" ForeColor="Navy" style="font-weight: 700; color: #FF3300"></asp:Label>
            </td>
            <td style="width: 16%">
                &nbsp;</td>
            <td style="width: 14%">
                &nbsp;</td>
            <td width="10%">
                &nbsp;</td>
            <td width="5%">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="6">
                <asp:Button ID="btnViewSummaryList" runat="server" 
                    Text="Generate List" class="btn btn-primary" />
            &nbsp;&nbsp;
                <asp:Button ID="btnPrint" runat="server" style="margin-top: 0px; " 
                    Text="Print" class="btn btn-primary" />
            &nbsp;&nbsp;
                <asp:Button ID="btnExcel" runat="server" Text="Export to Excel" class="btn btn-primary" />
            </td>
            <td width="5%">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="height: 6px; width: 17%;">
                
                <asp:Label ID="lblTotalRecords" runat="server"></asp:Label>
                </td>
            <td style="height: 6px; width: 18%;">
                </td>
            <td style="height: 6px; width: 17%;">
                </td>
            <td style="height: 6px; width: 16%;">
                </td>
            <td style="height: 6px; width: 14%;">
                </td>
            <td width="10%" style="height: 6px">
                </td>
            <td width="5%" style="height: 6px">
                </td>
        </tr>
        <tr>
            <td colspan="7" style="margin-left: 80px" align="center">
                <div id="gvDiv">

                    <asp:Label ID="lblSchoolName" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
                    <br />
                    <asp:Label ID="lblTitle" runat="server" Font-Bold="True" Font-Size="Small"></asp:Label>
                    <br />
                    <div style="overflow:scroll;width:930px;">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataSourceID="SqlDataSource1" Width="100%">
                        <Columns>
                            <asp:BoundField DataField="SID" HeaderText="SID" SortExpression="SID" Visible="False" />
                                                        <asp:BoundField DataField="FeeBookNo" HeaderText="Computer Code" SortExpression="FeeBookNo" />
                            <asp:BoundField DataField="SName" HeaderText="Student Name" SortExpression="SName" />
                            <asp:BoundField DataField="FName" HeaderText="Father's Name" SortExpression="FName" />
                            <asp:BoundField DataField="Gender" HeaderText="Gender" SortExpression="Gender" />
                            <asp:BoundField DataField="ClassName" HeaderText="Class" SortExpression="ClassName" />
                            <asp:BoundField DataField="SecName" HeaderText="Section" SortExpression="SecName" />
                            
                            <asp:BoundField DataField="DOB" HeaderText="DOB" SortExpression="DOB" DataFormatString="{0:dd/MM/yyyy}"  />
                            <asp:BoundField DataField="TempAddress" HeaderText="Address" SortExpression="TempAddress" />
                            <asp:BoundField DataField="MobNo" HeaderText="Mobile" SortExpression="MobNo" />
                            <asp:BoundField DataField="BusName" HeaderText="Bus" SortExpression="BusName" />
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
                    </div>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="select SID,FeeBookNo, SName,FName,CASE WHEN Gender = 0 THEN 'Male' ELSE 'Female' END AS Gender,ClassName,SecName,DOB,TempAddress,MobNo,BusName From vw_StudentBusList where SID<0"></asp:SqlDataSource>
                    <br />
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="7" style="margin-left: 80px">
                &nbsp;</td>
        </tr>
        </table>
                 </div>
        </div>
        <div class="clearfix"></div>
    </div>
</asp:Content>
