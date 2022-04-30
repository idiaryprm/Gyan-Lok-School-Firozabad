<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.master" CodeBehind="TimeTable.aspx.vb" Inherits="iDiary_V3.TimeTable" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Heading" runat="server">
    Generate
    Time Table 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    <table cellspacing="0" style="width: 100%; height: 22px">
        <tr>
            <td><strong>Class</strong> </td>
            <td>
                <asp:DropDownList ID="cboClass" runat="server" AutoPostBack="true" Width="100px" >
                </asp:DropDownList>
            </td>
            <td>
                <strong>Section</strong></td>
            <td>    
                <asp:DropDownList ID="cboSection" runat="server" AutoPostBack="True" Width="100px">
                </asp:DropDownList>
            </td>
            <td>    
                <strong>Day</strong>
            </td>
            <td>            
                <asp:DropDownList ID="cboDays" runat="server" AutoPostBack="true" Width="100px" >
                     
                    <asp:ListItem></asp:ListItem>
                     
                    <asp:ListItem>Monday</asp:ListItem>
                    <asp:ListItem>Tuesday</asp:ListItem>
                    <asp:ListItem>Wednesday</asp:ListItem>
                    <asp:ListItem>Thursday</asp:ListItem>
                    <asp:ListItem>Friday</asp:ListItem>
                    <asp:ListItem>Saturday</asp:ListItem>
                </asp:DropDownList>
            <td>        
                &nbsp;</td>
            <td>    </td>
            <td>    </td>
        </tr>
        <tr>
            <td>    &nbsp;</td>
            <td>    &nbsp;</td>
            <td>    
                &nbsp;</td>
            <td>    </td>
            <td>    </td>
            <td>    </td>
            <td>    </td>
            <td>    &nbsp;</td>
            <td>    </td>
        </tr>
        <tr>
            <td class="auto-style1" style="height: 21px; font-weight: bold">    Periods&gt;</td>
            <td class="auto-style1" style="height: 21px; font-weight: bold">    <strong>&nbsp;First</strong></td>
            <td style="height: 21px">    <b>&nbsp;Second</b></td>
            <td style="height: 21px">    <b>&nbsp;Third</b></td>
            <td style="height: 21px">    <b>&nbsp;Fourth</b></td>
            <td style="height: 21px">    <b>&nbsp;Fifth</b></td>
            <td style="height: 21px">    <b>&nbsp;Sixth</b></td>
            <td style="height: 21px">    <b>&nbsp;Seventh</b></td>
            <td style="height: 21px">    <b>&nbsp;Eighth</b></td>
        </tr>
        <tr>
            <td style="vertical-align :top; font-size: 13px;">    
                <strong><span style="font-size: 14px">Subject<br />
                <br />
                Teacher</span></strong></td>
            <td>    
                <asp:DropDownList ID="cboPeriod1" runat="server" AutoPostBack="True" Width="100px">
                     
                
                </asp:DropDownList>
                <br />
                <br />
                <asp:DropDownList ID="cboTeacher1" runat="server" Width="100px">
                   
                </asp:DropDownList>
            </td>
            <td>    
                <asp:DropDownList ID="cboPeriod2" runat="server" AutoPostBack="True" Width="100px">
                 
                </asp:DropDownList>
                <br />
                <br />
                <asp:DropDownList ID="cboTeacher2" runat="server" Width="100px">
                    
                   
                </asp:DropDownList>
            </td>
            <td>    
                <asp:DropDownList ID="cboPeriod3" runat="server" AutoPostBack="True" Width="100px">
                    
                   
                </asp:DropDownList>
                <br />
                <br />
                <asp:DropDownList ID="cboTeacher3" runat="server" Width="100px">
                   
                   
                </asp:DropDownList>
            </td>
            <td>    
                <asp:DropDownList ID="cboPeriod4" runat="server" AutoPostBack="True" Width="100px">
                     
                  
                </asp:DropDownList>
                <br />
                <br />
                <asp:DropDownList ID="cboTeacher4" runat="server" Width="100px">
                     
                  
                </asp:DropDownList>
            </td>
            <td>    
                <asp:DropDownList ID="cboPeriod5" runat="server" AutoPostBack="True" Width="100px">
                     

                </asp:DropDownList>
                <br />
                <br />
                <asp:DropDownList ID="cboTeacher5" runat="server" Width="100px">
                 
                </asp:DropDownList>
            </td>
            <td>    
                <asp:DropDownList ID="cboPeriod6" runat="server" AutoPostBack="True" Width="100px">
                     
                   
                </asp:DropDownList>
                <br />
                <br />
                <asp:DropDownList ID="cboTeacher6" runat="server" Width="100px">
                     
                
                </asp:DropDownList>
            </td>
            <td>    
                <asp:DropDownList ID="cboPeriod7" runat="server" AutoPostBack="True" Width="100px">
                     
                   
                </asp:DropDownList>
                <br />
                <br />
                <asp:DropDownList ID="cboTeacher7" runat="server" Width="100px">
                  
                </asp:DropDownList>
            </td>
            <td>    
                <asp:DropDownList ID="cboPeriod8" runat="server" AutoPostBack="True" Width="100px">
                   
                </asp:DropDownList>
                <br />
                <br />
                <asp:DropDownList ID="cboTeacher8" runat="server" Width="100px">
                   
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>    
                 &nbsp;</td>
           
            <td>    
                 &nbsp;</td>
           
            <td colspan="7">    
                &nbsp;</td>
        </tr>
        <tr>
            <td>    &nbsp;</td>
            <td>        
                 <asp:Button ID="btnAdd" runat="server" Text="Save" />
               </td>
            <td>        
                <asp:Label ID="lblStatus" runat="server" BorderStyle="None" style="color: #CC3300"></asp:Label>
            </td>
            <td>    </td>
            <td>    </td>
            <td>    </td>
            <td>    </td>
            <td>    </td>
            <td>    </td>
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
            <td>    &nbsp;</td>
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
    </table>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    </asp:UpdatePanel>
</asp:Content>
