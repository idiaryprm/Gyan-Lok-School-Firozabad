
<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.master" CodeBehind="TimeTableToday.aspx.vb" Inherits="iDiary_V3.TimeTableToday" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Heading" runat="server">
    &nbsp;Time Table  
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    <table cellspacing="0" style="width: 100%; height: 22px">
        <tr>
            <td>&nbsp;</td>
            <td><strong>Class</strong> </td>
            <td>
                <asp:DropDownList ID="cboClass" runat="server" AutoPostBack="true" Width="110px" >
                </asp:DropDownList>
            </td>
            <td><strong>Section</strong></td>
            <td>    
                <asp:DropDownList ID="cboSection" runat="server" AutoPostBack="True" Width="110px">
                </asp:DropDownList>
            </td>
            <td>    <strong>Day</strong>
            <td>        
                <asp:DropDownList ID="cboDays" runat="server" AutoPostBack="true" Width="110px" >
                     
                    <asp:ListItem></asp:ListItem>
                     
                    <asp:ListItem>Monday</asp:ListItem>
                    <asp:ListItem>Tuesday</asp:ListItem>
                    <asp:ListItem>Wednesday</asp:ListItem>
                    <asp:ListItem>Thursday</asp:ListItem>
                    <asp:ListItem>Friday</asp:ListItem>
                    <asp:ListItem>Saturday</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>    </td>
            <td style="width: 97px">    </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>
                &nbsp;</td>
            <td>&nbsp;</td>
            <td>    
                &nbsp;</td>
            <td>    &nbsp;<td>        
                &nbsp;</td>
            <td>    &nbsp;</td>
            <td style="width: 97px">    &nbsp;</td>
        </tr>
        <tr>
           
            <td colspan="9">    
                <asp:GridView ID="gvTimeTable" runat="server" AutoGenerateColumns="False" DataKeyNames="ttID" DataSourceID="SqlDataSourceTimeTable" BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4" GridLines="Horizontal" Width="909px">
                    <Columns>
                        <asp:BoundField DataField="DayID" HeaderText="Period/Day" SortExpression="DayID" ControlStyle-Font-Bold="true" ItemStyle-Font-Bold="true"  />
                       <%-- <asp:BoundField DataField="Per1Sub" HeaderText="Per1Sub" SortExpression="Per1Sub" />
                        <asp:BoundField DataField="Per1Teacher" HeaderText="Per1Teacher" SortExpression="Per1Teacher" />
                        <asp:BoundField DataField="Per2Sub" HeaderText="Per2Sub" SortExpression="Per2Sub" />
                        <asp:BoundField DataField="Per2Teacher" HeaderText="Per2Teacher" SortExpression="Per2Teacher" />
                        <asp:BoundField DataField="Per3Sub" HeaderText="Per3Sub" SortExpression="Per3Sub" />
                        <asp:BoundField DataField="Per3Teacher" HeaderText="Per3Teacher" SortExpression="Per3Teacher" />
                        <asp:BoundField DataField="Per4Sub" HeaderText="Per4Sub" SortExpression="Per4Sub" />
                        <asp:BoundField DataField="Per4Teacher" HeaderText="Per4Teacher" SortExpression="Per4Teacher" />
                        <asp:BoundField DataField="Per5Sub" HeaderText="Per5Sub" SortExpression="Per5Sub" />
                        <asp:BoundField DataField="Per5Teacher" HeaderText="Per5Teacher" SortExpression="Per5Teacher" />
                        <asp:BoundField DataField="Per6Sub" HeaderText="Per6Sub" SortExpression="Per6Sub" />
                        <asp:BoundField DataField="Per6Teacher" HeaderText="Per6Teacher" SortExpression="Per6Teacher" />
                        <asp:BoundField DataField="Per7Sub" HeaderText="Per7Sub" SortExpression="Per7Sub" />
                        <asp:BoundField DataField="Per7Teacher" HeaderText="Per7Teacher" SortExpression="Per7Teacher" />
                        <asp:BoundField DataField="Per8Sub" HeaderText="Per8Sub" SortExpression="Per8Sub" />
                        <asp:BoundField DataField="Per8Teacher" HeaderText="Per8Teacher" SortExpression="Per8Teacher" />--%>
                        <asp:TemplateField HeaderText="Period I">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" style="font-weight: 700" Text='<%# Eval("Per1Sub") %>' ></asp:Label>
                                <br />
                                <asp:Label ID="Label2" runat="server" style="font-size: x-small" Text='<%# Eval("Per1Teacher") %>' ForeColor="#9999FF"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Period II">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" style="font-weight: 700" Text='<%# Eval("Per2Sub")%>' ></asp:Label>
                                <br />
                                <asp:Label ID="Label2" runat="server" style="font-size: x-small" Text='<%# Eval("Per2Teacher")%>' ForeColor="#9999FF"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Period III">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" style="font-weight: 700" Text='<%# Eval("Per3Sub")%>' ></asp:Label>
                                <br />
                                <asp:Label ID="Label2" runat="server" style="font-size: x-small" Text='<%# Eval("Per3Teacher")%>' ForeColor="#9999FF"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Period IV">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" style="font-weight: 700" Text='<%# Eval("Per4Sub")%>' ></asp:Label>
                                <br />
                                <asp:Label ID="Label2" runat="server" style="font-size: x-small" Text='<%# Eval("Per4Teacher")%>' ForeColor="#9999FF"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Period V">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" style="font-weight: 700" Text='<%# Eval("Per5Sub")%>' ></asp:Label>
                                <br />
                                <asp:Label ID="Label2" runat="server" style="font-size: x-small" Text='<%# Eval("Per5Teacher")%>' ForeColor="#9999FF"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Period VI">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" style="font-weight: 700" Text='<%# Eval("Per6Sub")%>' ></asp:Label>
                                <br />
                                <asp:Label ID="Label2" runat="server" style="font-size: x-small" Text='<%# Eval("Per6Teacher")%>' ForeColor="#9999FF"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Period VII">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" style="font-weight: 700" Text='<%# Eval("Per7Sub")%>' ></asp:Label>
                                <br />
                                <asp:Label ID="Label2" runat="server" style="font-size: x-small" Text='<%# Eval("Per7Teacher")%>' ForeColor="#9999FF"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Period VIII">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" style="font-weight: 700" Text='<%# Eval("Per8Sub")%>' ></asp:Label>
                                <br />
                                <asp:Label ID="Label2" runat="server" style="font-size: x-small" Text='<%# Eval("Per8Teacher") %>' ForeColor="#9999FF"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="White" ForeColor="#333333" />
                    <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="White" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F7F7F7" />
                    <SortedAscendingHeaderStyle BackColor="#487575" />
                    <SortedDescendingCellStyle BackColor="#E5E5E5" />
                    <SortedDescendingHeaderStyle BackColor="#275353" />
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSourceTimeTable" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT * FROM [TimeTable] ORDER BY [DayID]"></asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td class="auto-style1" style="height: 21px; font-weight: bold" colspan="3">    &nbsp;</td>
            <td class="auto-style1" style="height: 21px; font-weight: bold" colspan="3">    &nbsp;</td>
            <td class="auto-style1" style="height: 21px; font-weight: bold" colspan="3">    &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style1" style="height: 21px; font-weight: bold" colspan="3">    Absentee List</td>
            <td class="auto-style1" style="height: 21px; font-weight: bold" colspan="3">    
                <asp:Label ID="lblSchedule" runat="server" style="font-weight: 700" Text="Schedule"></asp:Label>
            </td>
            <td class="auto-style1" style="height: 21px; font-weight: bold" colspan="3">    
                <asp:Button ID="btnSubstitute" runat="server" BorderStyle="None" EnableTheming="True" Height="16px" Text="get Substitute" />
            </td>
        </tr>
        <tr>
            <td class="auto-style1" style="height: 21px; font-weight: bold; vertical-align:top " colspan="3">    
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSourceEMP" CellPadding="4" ForeColor="Black" GridLines="Vertical" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="EmpName" HeaderText="EmpName" SortExpression="EmpName" />
                        <asp:BoundField DataField="Mob" HeaderText="Mob" SortExpression="Mob" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnSchedule" runat="server" Text="get Schedule" CausesValidation="false" CommandName="getSchedule" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" BorderStyle="None" style="color: #993300; background-color: #FFFFFF" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#CCCC99" />
                    <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                    <RowStyle BackColor="#F7F7DE" />
                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#FBFBF2" />
                    <SortedAscendingHeaderStyle BackColor="#848384" />
                    <SortedDescendingCellStyle BackColor="#EAEAD3" />
                    <SortedDescendingHeaderStyle BackColor="#575357" />
                </asp:GridView>
                </td>
            <td class="auto-style1" style="height: 21px; font-weight: bold; vertical-align :top" colspan="3">    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSourceTT" CellPadding="4" ForeColor="Black" GridLines="Vertical" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="ClassName" HeaderText="ClassName" SortExpression="ClassName" />
                        <asp:BoundField DataField="SecName" HeaderText="SecName" SortExpression="SecName" />
                        <asp:BoundField DataField="periodID" HeaderText="periodID" SortExpression="periodID" />
                        <asp:BoundField DataField="SubjectName" HeaderText="SubjectName" SortExpression="SubjectName" />
                         <asp:TemplateField>
                            <ItemTemplate>
                                <asp:DropDownList ID="cboTeachers" runat="server">
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#CCCC99" />
                    <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                    <RowStyle BackColor="#F7F7DE" />
                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#FBFBF2" />
                    <SortedAscendingHeaderStyle BackColor="#848384" />
                    <SortedDescendingCellStyle BackColor="#EAEAD3" />
                    <SortedDescendingHeaderStyle BackColor="#575357" />
                </asp:GridView>
                </td>
            <td class="auto-style1" style="height: 21px; font-weight: bold;vertical-align:top " colspan="3">    
                <strong>
                <asp:GridView ID="GridView4" runat="server" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" GridLines="Vertical" ForeColor="Black">
                    <AlternatingRowStyle BackColor="White" />
                    <FooterStyle BackColor="#CCCC99" />
                    <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                    <RowStyle BackColor="#F7F7DE" />
                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#FBFBF2" />
                    <SortedAscendingHeaderStyle BackColor="#848384" />
                    <SortedDescendingCellStyle BackColor="#EAEAD3" />
                    <SortedDescendingHeaderStyle BackColor="#575357" />
                   
                </asp:GridView>
                </strong></td>
        </tr>
        <tr>
            <td class="auto-style1" style="height: 21px; font-weight: bold; vertical-align:top " colspan="9">    
                <br />
                <asp:GridView ID="GridView5" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSourceSubst">
                    <Columns>
                        <asp:BoundField DataField="periodID" HeaderText="periodID" SortExpression="periodID" />
                        <asp:BoundField DataField="EmpName" HeaderText="EmpName" SortExpression="EmpName" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:DropDownList ID="cboTeachers" runat="server">
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <br />
                <br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <br />
                <strong>
                <br />
                </strong>
                </td>
        </tr>
        <tr>
            <td class="auto-style1" style="height: 21px; font-weight: bold">    &nbsp;</td>
            <td class="auto-style1" style="height: 21px; font-weight: bold">    
                &nbsp;</td>
            <td style="height: 21px">    
                &nbsp;</td>
            <td style="height: 21px">    &nbsp;</td>
            <td style="height: 21px">    &nbsp;</td>
            <td style="height: 21px">    &nbsp;</td>
            <td style="height: 21px">    &nbsp;</td>
            <td style="height: 21px">    &nbsp;</td>
            <td style="height: 21px; width: 97px;">    &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style1" style="height: 21px; font-weight: bold;  vertical-align:top " colspan="9">    
                <asp:SqlDataSource ID="SqlDataSourceEMP" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT [AttDate], [EmpID], [Att], [EmpName], [Mob] FROM [vw_Employee_Attendance]"></asp:SqlDataSource>
                <br />
                <br />
                Subject List<asp:SqlDataSource ID="SqlDataSourceTT" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT [ClassName], [SecName], [periodID], [SubjectName], [EmpName], [Att], [AttDate] FROM [vw_Time_table]"></asp:SqlDataSource>
                <br />
                Substitute<br />
                <asp:SqlDataSource ID="SqlDataSourceSubst" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT [periodID], [SubjectName], [EmpName], [SubjectCode], [ClassName], [SecName] FROM [vw_Time_table]"></asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td class="auto-style1" style="height: 21px; font-weight: bold">    &nbsp;</td>
            <td class="auto-style1" style="height: 21px; font-weight: bold">    &nbsp;</td>
            <td style="height: 21px">    &nbsp;</td>
            <td style="height: 21px">    &nbsp;</td>
            <td style="height: 21px">    &nbsp;</td>
            <td style="height: 21px">    &nbsp;</td>
            <td style="height: 21px">    &nbsp;</td>
            <td style="height: 21px">    &nbsp;</td>
            <td style="height: 21px; width: 97px;">    &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style1" style="height: 21px; font-weight: bold">    &nbsp;</td>
            <td class="auto-style1" style="height: 21px; font-weight: bold">    &nbsp;</td>
            <td style="height: 21px">    &nbsp;</td>
            <td style="height: 21px">    &nbsp;</td>
            <td style="height: 21px">    &nbsp;</td>
            <td style="height: 21px">    &nbsp;</td>
            <td style="height: 21px">    &nbsp;</td>
            <td style="height: 21px">    &nbsp;</td>
            <td style="height: 21px; width: 97px;">    &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style1" style="height: 21px; font-weight: bold">    &nbsp;</td>
            <td class="auto-style1" style="height: 21px; font-weight: bold">    &nbsp;</td>
            <td style="height: 21px">    &nbsp;</td>
            <td style="height: 21px">    &nbsp;</td>
            <td style="height: 21px">    &nbsp;</td>
            <td style="height: 21px">    &nbsp;</td>
            <td style="height: 21px">    &nbsp;</td>
            <td style="height: 21px">    &nbsp;</td>
            <td style="height: 21px; width: 97px;">    &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style1" style="height: 21px; font-weight: bold">    &nbsp;</td>
            <td class="auto-style1" style="height: 21px; font-weight: bold">    &nbsp;</td>
            <td style="height: 21px">    &nbsp;</td>
            <td style="height: 21px">    &nbsp;</td>
            <td style="height: 21px">    &nbsp;</td>
            <td style="height: 21px">    &nbsp;</td>
            <td style="height: 21px">    &nbsp;</td>
            <td style="height: 21px">    &nbsp;</td>
            <td style="height: 21px; width: 97px;">    &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style1" style="height: 21px; font-weight: bold">    &nbsp;</td>
            <td class="auto-style1" style="height: 21px; font-weight: bold">    &nbsp;</td>
            <td style="height: 21px">    &nbsp;</td>
            <td style="height: 21px">    &nbsp;</td>
            <td style="height: 21px">    &nbsp;</td>
            <td style="height: 21px">    &nbsp;</td>
            <td style="height: 21px">    &nbsp;</td>
            <td style="height: 21px">    &nbsp;</td>
            <td style="height: 21px; width: 97px;">    &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style1" style="height: 21px; font-weight: bold">    &nbsp;</td>
            <td class="auto-style1" style="height: 21px; font-weight: bold">    &nbsp;</td>
            <td style="height: 21px">    &nbsp;</td>
            <td style="height: 21px">    &nbsp;</td>
            <td style="height: 21px">    &nbsp;</td>
            <td style="height: 21px">    &nbsp;</td>
            <td style="height: 21px">    &nbsp;</td>
            <td style="height: 21px">    &nbsp;</td>
            <td style="height: 21px; width: 97px;">    &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style1" style="height: 21px; font-weight: bold">    &nbsp;</td>
            <td class="auto-style1" style="height: 21px; font-weight: bold">    &nbsp;</td>
            <td style="height: 21px">    &nbsp;</td>
            <td style="height: 21px">    &nbsp;</td>
            <td style="height: 21px">    &nbsp;</td>
            <td style="height: 21px">    &nbsp;</td>
            <td style="height: 21px">    &nbsp;</td>
            <td style="height: 21px">    &nbsp;</td>
            <td style="height: 21px; width: 97px;">    &nbsp;</td>
        </tr>
        <tr>
            <td>    &nbsp;</td>
            <td>    &nbsp;</td>
            <td>    </td>
            <td>    </td>
            <td>    </td>
            <td>    </td>
            <td>    </td>
            <td>    </td>
            <td style="width: 97px">    </td>
        </tr>
        <tr>
            <td>    &nbsp;</td>
            <td>    &nbsp;</td>
            <td>    &nbsp;</td>
            <td>    &nbsp;</td>
            <td>    &nbsp;</td>
            <td>    &nbsp;</td>
            <td>    &nbsp;</td>
            <td>    &nbsp;</td>
            <td style="width: 97px">    &nbsp;</td>
        </tr>
       
    </table>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    </asp:UpdatePanel>
</asp:Content>
