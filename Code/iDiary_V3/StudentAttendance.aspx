<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" Inherits="iDiary_V3.StudentAttendance" title="Untitled Page" Codebehind="StudentAttendance.aspx.vb" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Heading" runat="server">
    Student Attendance Wizard 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    <script language="javascript" type="text/javascript">
        function CountSMSCharacter() {
            var inputElems = document.getElementsByTagName("chkSelect"), labelField = document.getElementById("<%=lblCount.ClientID%>"),
            count = 0;
            
            for (var i = 0; i < gvStudent.Rows.Count - 1; i++) {
                if (inputElems[i].type === "checkbox" && inputElems[i].checked === false) {
                    count++;
                    labelField.innerHTML = "Total Selection " + count
                }
            }
        }
    </script>
   <%-- <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    
    <br /><br /><br /><br /><br /><br /><br /><br />

    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />--%>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
 
   <div class="col_3" style="margin-top: 20px;" id="panelEnquiryNo" runat="server">
        <div class="col-md-3 widget widget1" style="width: 100%">
            <div class="r3_counter_box">
                 <table class="table">
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <table border="0" cellpadding="1" cellspacing="1" width="100%">
                            <tr>
                                <td style="width: 8%">
                                    <b>Attendance Date</b></td>
                                <td style="width: 14%">
                                    <asp:TextBox ID="txtDate" runat="server" CssClass="textbox"></asp:TextBox>
                                    <asp:CalendarExtender ID="txtDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDate"></asp:CalendarExtender>
                               <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999" MaskType="Date" TargetControlID="txtDate" PromptCharacter="_"> </asp:MaskedEditExtender>
                                     </td>
                                <td width="20%">
                                    <b>Student List</b></td>
                            </tr>
                            <tr>
                                <td style="width: 8%">
                                    <strong>School Name</strong></td>
                                <td style="width: 14%">
                                    <asp:DropDownList ID="cboSchoolName" OnSelectedIndexChanged ="cboSchoolName_SelectedIndexChanged"   runat="server" AutoPostBack="true" CssClass="Dropdown" Width="300px">
                                    </asp:DropDownList>
                                </td>
                                <td width="20%" rowspan="6" valign="top" align="left">
                                    <div style="overflow-y: scroll; max-height: 400px">
                                        <asp:GridView ID="gvStudent" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" CssClass="Grid" DataKeyNames="SID" DataSourceID="SqlDataSource1" Height="16px" Visible="False" Width="100%">
                                            <RowStyle BackColor="White" ForeColor="#330099" />
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkSelect" runat="server" OnCheckedChanged="chkSelect_CheckedChanged" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ClassRollNo" HeaderText="RollNo" SortExpression="ClassRollNo" />
                                                <asp:BoundField DataField="SName" HeaderText="Student Name" SortExpression="SName" />
                                                <asp:BoundField DataField="FName" HeaderText="Father's Name" SortExpression="FName" />
                                                <asp:BoundField DataField="ClassName" HeaderText="Class" SortExpression="ClassName" />
                                                <asp:BoundField DataField="SecName" HeaderText="Sec" SortExpression="SecName" />
                                                <asp:BoundField DataField="MobNo" HeaderText="Mob.No" SortExpression="MobNo" />
                                            </Columns>
                                            <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                                            <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                                            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                                            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                                        </asp:GridView>

                                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT [SName],[classrollno], [FName], [ClassName], [SecName], [MobNo],SID FROM [vw_Student] WHERE SID is NULL"></asp:SqlDataSource>
                                   
                                         </div>
                                    
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 8%">
                                    <b>Class</b></td>
                                <td style="width: 14%">
                                    <asp:DropDownList ID="cboClass" runat="server" CssClass="Dropdown"
                                        AutoPostBack="True">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 8%"><b>Section</b></td>
                                <td style="width: 14%">
                                    <asp:DropDownList ID="cboSection" runat="server" AutoPostBack="True" CssClass="Dropdown">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                             <tr>
                                <td style="width: 8%"><b>Message Body</b></td>
                                <td style="width: 14%">
                                     <asp:TextBox ID="txtMessage" CssClass="textbox" runat="server" onkeyup="CountSMSCharacter();" Height="104px" 
                    TextMode="MultiLine" Width="75%" BorderWidth="1px"></asp:TextBox>
                                    <h6  style="color:Red;">  * is Represented for a Student Name </h6>
                                </td>
                                 
                                
                            </tr>
                           <%-- <tr>
                             <td>  
                                 <asp:Label ID="Label1" runat="server" Text=" * is Represented for a Registration Number " style="color:Red; text-align:center; width:100% ;"></asp:Label>
                               </td> 
                                  </tr>--%>
                              <tr>
                                <td style="width: 8%"><b>SMS Sender</b></td>
                                <td style="width: 14%">
                                     <asp:TextBox ID="txtSenderID" CssClass="textbox" Enabled ="false"  runat="server" BorderWidth="1px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 8%">
                                   <%-- <strong>Shift</strong></td>--%>
                                <td style="width: 14%">
                                    <asp:DropDownList ID="cboShift" runat="server" Enabled="false"  CssClass="Dropdown" Visible="false">
                                        <asp:ListItem>Morning</asp:ListItem>
                                        <asp:ListItem>Evening</asp:ListItem>
                                    </asp:DropDownList>
                                    <br />
                                    <asp:CheckBox ID="chkAll" runat="server" Text="Check / Uncheck All Students"
                                        AutoPostBack="True" />
                                    <br />
                                    <asp:CheckBox ID="chkSMS" runat="server" BorderWidth="0px"
                                        Text="Send Absent SMS" />
                                    <br />
                                    <asp:CheckBox ID="chkEmail" runat="server" BorderStyle="Solid"
                                        BorderWidth="0px" Text="Send Absent Email" Visible="False" />
                                    <br />
                                    <br />
                                    <asp:Label ID="lblCount" runat="server" ForeColor="Navy" Style="font-weight: 700"></asp:Label>
                                    <br />
                                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" />
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top;" colspan="2">
                                    <br />
                                    <br />
                                    &nbsp;
                <asp:Label ID="lblStatus" runat="server" ForeColor="Navy"
                    Style="font-weight: 700"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 8%">&nbsp;</td>
                                <td style="width: 14%">&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 8%">&nbsp;</td>
                                <td style="width: 14%">&nbsp;</td>
                                <td width="20%">
                                    <asp:ListBox ID="lstSID" runat="server" Height="32px" Visible="False"></asp:ListBox>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSave" />
                       <%-- <asp:AsyncPostBackTrigger ControlID="gvStudent"  EventName="OnCheckedChanged" />--%>
                    </Triggers>

                </asp:UpdatePanel>

            </td>
        </tr>
    </table>

            </div>
        </div>
        <div class="clearfix"></div>
    </div>
    
</asp:Content>

<asp:Content ID="Content3" runat="server" contentplaceholderid="head">
</asp:Content>


