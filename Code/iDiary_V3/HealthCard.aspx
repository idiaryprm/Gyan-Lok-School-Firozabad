<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="HealthCard.aspx.vb" MasterPageFile="~/MasterPage.Master" Inherits="iDiary_V3.HealthCard" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Heading" runat="server">
    Health Card  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <table width="100%" border="0" cellpadding="2" cellspacing="2" class="Table_Font ">
        <tr>
            
            <td class="td_width_16" >
                Admission No.<span style="color: #CC0000">*</span></td>
            <td class="td_width_16" >
                <asp:TextBox ID="txtSRNo" runat="server" Width="70px" BorderWidth="1px" 
                    BorderColor="Black" CssClass="TextBoxFont"></asp:TextBox>
                <asp:ImageButton ID="btnNext" runat="server" 
                    ImageUrl="~/images/next.png" ImageAlign="AbsMiddle" />

                <asp:TextBox ID="txtSID" runat="server" Visible="False" Width="15px"></asp:TextBox>

            </td>
            <td class="td_width_16"  >
                Student Name</td>
            <td class="td_width_16" valign="top"  >
                <asp:TextBox ID="txtName" runat="server" Width="125px" BorderWidth="1px" 
                    BorderColor="Black" CssClass="TextBoxFont"></asp:TextBox>
                </td>
            <td class="td_width_16" >
                <asp:ImageButton ID="btnNameSearch" runat="server" 
                    ImageUrl="~/images/next.png" ImageAlign="AbsMiddle" />
                </td>
            <td class="td_width_16"  >
                &nbsp;</td>
            <td class="td_width_4">
                &nbsp;</td>
        </tr>
        <tr>
            
            <td class="td_width_16" colspan="7">
                        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" AutoGenerateSelectButton="True" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataSourceID="SqlDataSource2" ShowHeader="False" Width="600px" Font-Names="Garamond" Font-Size="10pt" ForeColor="Black" GridLines="Horizontal">
                            <Columns>
                                <asp:BoundField DataField="RegNo" HeaderText="RegNo" SortExpression="RegNo" />
                                <asp:BoundField DataField="SName" HeaderText="SName" SortExpression="SName" />
                                <asp:BoundField DataField="ClassName" HeaderText="ClassName" SortExpression="ClassName" />
                                <asp:BoundField DataField="SecName" HeaderText="SecName" SortExpression="SecName" />
                            </Columns>
                            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                            <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                            <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#F7F7F7" />
                            <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                            <SortedDescendingCellStyle BackColor="#E5E5E5" />
                            <SortedDescendingHeaderStyle BackColor="#242121" />
                        </asp:GridView>
                    </td>
        </tr>
        <tr>
            
            <td class="td_width_16" >
                Father Name</td>
            <td class="td_width_16" >
                <asp:TextBox ID="txtFName" runat="server" Width="125px" BorderWidth="1px" 
                    BorderColor="Black" CssClass="TextBoxFont" ReadOnly="True"></asp:TextBox>
                </td>
            <td class="td_width_16"  >
                Mother Name</td>
            <td class="td_width_16" valign="top"  >
                <asp:TextBox ID="txtMName" runat="server" Width="125px" BorderWidth="1px" 
                    BorderColor="Black" CssClass="TextBoxFont" ReadOnly="True"></asp:TextBox>
                </td>
            <td class="td_width_16" >
                Father Address</td>
            <td class="td_width_16" colspan="2" rowspan="2" valign="top" >
                <asp:TextBox ID="txtFatherAddress" runat="server" Width="124px" BorderWidth="1px" 
                    BorderColor="Black" CssClass="TextBoxFont" Height="40px" TextMode="MultiLine" ReadOnly="True"></asp:TextBox>
                </td>
        </tr>
        <tr>
            
            <td class="td_width_16"  >
                Date Of Birth</td>
            <td class="td_width_16"  >
                <asp:TextBox ID="txtDOB" runat="server" Width="125px" BorderWidth="1px" 
                    BorderColor="Black" CssClass="TextBoxFont" ReadOnly="True"></asp:TextBox>
              
            </td>
            <td class="td_width_16" style="width: 94px; height: 26px;">
                Phone (Office)</td>
            <td class="td_width_16" valign="top" style="width: 106px; height: 26px;">
                <asp:TextBox ID="txtPhoneOffice" runat="server" Width="125px" BorderWidth="1px" 
                    BorderColor="Black" ReadOnly="True"></asp:TextBox>
            </td>
            <td class="td_width_16" style="height: 26px; width: 100px;">
                </td>
        </tr>
          <tr>
            <td class="td_width_16" style="height: 18px; width: 136px;">
                Phone (Resd)</td>
            <td class="td_width_16" style="height: 18px">
                <asp:TextBox ID="txtPhoneResd" runat="server" Width="125px" BorderWidth="1px" 
                    BorderColor="Black" ReadOnly="True"></asp:TextBox>
            </td>
            <td class="td_width_16" style="height: 18px; width: 94px;">
                Mobile No.</td>
            <td class="td_width_16" valign="top"  >
                <asp:TextBox ID="txtMobile" runat="server" Width="125px" BorderWidth="1px" 
                    BorderColor="Black" ReadOnly="True"></asp:TextBox>
            </td>
            <td class="td_width_16" style="height: 18px; width: 100px;">
                Blood Group</td>
            <td class="td_width_16">
                <asp:TextBox ID="txtBloodGroup" runat="server" Width="125px" BorderWidth="1px" 
                    BorderColor="Black" ReadOnly="True"></asp:TextBox>
              </td>
            <td class="td_width_4" style="height: 18px">
            </td>
          </tr>
          <tr>
            <td class="td_width_16" style="height: 18px; " colspan="2">
                <span lang="EN-US" style="font-size: small; mso-bidi-font-size: 10.0pt; line-height: 115%; font-family: &quot;Calibri&quot;,&quot;sans-serif&quot;; mso-ascii-theme-font: minor-latin; mso-fareast-font-family: &quot;Times New Roman&quot;; mso-fareast-theme-font: minor-fareast; mso-hansi-theme-font: minor-latin; mso-bidi-font-family: Mangal; mso-bidi-theme-font: minor-bidi; mso-ansi-language: EN-US; mso-fareast-language: EN-US; mso-bidi-language: HI; text-decoration: underline;">Student Vaccination Details</span></td>
            <td class="td_width_16" style="height: 18px; width: 94px;">
                &nbsp;</td>
            <td class="td_width_16" valign="top"  >
                &nbsp;</td>
            <td class="td_width_16" style="height: 18px; width: 100px;">
                &nbsp;</td>
            <td class="td_width_16">
                &nbsp;</td>
            <td class="td_width_4" style="height: 18px">
                &nbsp;</td>
          </tr>
        <tr>
            
            <td class="td_width_16" colspan="7">
                <asp:GridView ID="gvVaccination" runat="server" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="svID" DataSourceID="SqlDataSourceVacc" ForeColor="#333333" GridLines="None" Width="700px" AutoGenerateSelectButton="True">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:BoundField DataField="svID" HeaderText="svID" ReadOnly="True" SortExpression="svID" />
                        <asp:BoundField DataField="vacCode" HeaderText="Vaccine Code" SortExpression="vacCode" />
                        <asp:BoundField DataField="vacName" HeaderText="Vaccine Name" SortExpression="vacName" />
                        <asp:BoundField DataField="Performed_Date" HeaderText="Performed Date" SortExpression="Performed_Date" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField DataField="Due_Date" HeaderText="Due Date" SortExpression="Due_Date" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField DataField="Remarks" HeaderText="Remarks" SortExpression="Remarks" />
                    </Columns>
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            
            <td class="td_width_16" >
                Vaccinne Code</td>
            <td class="td_width_16" >
                <asp:DropDownList ID="cbovacCode" runat="server" class="form-control1">
                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem>Vaccine</asp:ListItem>
                    <asp:ListItem>Booster Dose</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="td_width_16"  >
                Due Date</td>
            <td class="td_width_16" valign="top"  >
                <asp:TextBox ID="txtDueDate" runat="server" Width="125px" BorderWidth="1px" 
                    BorderColor="Black"></asp:TextBox>
                <asp:CalendarExtender ID="txtDueDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDueDate">
                </asp:CalendarExtender>
            </td>
            <td class="td_width_16" >
                Performed Date</td>
            <td class="td_width_16"  >
                <asp:TextBox ID="txtPerformedDate" runat="server" Width="125px" BorderWidth="1px" 
                    BorderColor="Black" style="margin-left: 0px"></asp:TextBox>
                <asp:CalendarExtender ID="txtPerformedDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="txtPerformedDate">
                </asp:CalendarExtender>
            </td>
            <td class="td_width_4">

                <asp:TextBox ID="txtsvID" runat="server" Visible="False" Width="15px"></asp:TextBox>

            </td>
        </tr>
        <tr>
            
            <td class="td_width_16">
                Remarks</td>
            
            <td class="td_width_16" colspan="3">
                <asp:TextBox ID="txtremarks" runat="server" Width="410px" BorderWidth="1px" 
                    BorderColor="Black" Height="16px"></asp:TextBox>
            </td>
            <td class="td_width_16" >
                <asp:Button ID="btnSave" runat="server" Text="Save" Width="50px" 
                    BorderWidth="1px" BorderColor="Black" />
            </td>
            <td class="td_width_16"  >
                &nbsp;</td>
            <td class="td_width_4">
                &nbsp;</td>
        </tr>
        <tr>
            
            <td class="td_width_16" style="text-decoration: underline;" colspan="2">
                </td>
            <td class="td_width_16"  >
                &nbsp;</td>
            <td class="td_width_16" valign="top"  >
                &nbsp;</td>
            <td class="td_width_16" >
                &nbsp;</td>
            <td class="td_width_16"  >
                &nbsp;</td>
            <td class="td_width_4">
                &nbsp;</td>
        </tr>
        <tr>
            
            <td class="td_width_16" style="text-decoration: underline;" colspan="2">
                Health History</td>
            <td class="td_width_16"  >
                &nbsp;</td>
            <td class="td_width_16" valign="top"  >
                &nbsp;</td>
            <td class="td_width_16" >
                &nbsp;</td>
            <td class="td_width_16"  >
                &nbsp;</td>
            <td class="td_width_4">
                &nbsp;</td>
        </tr>
        <tr>
            
            <td class="td_width_16" colspan="7">
                <asp:GridView ID="gvHealthHistory" runat="server" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="hhID" DataSourceID="SqlDataSourceHealthHistory" ForeColor="#333333" GridLines="None" Width="700px" AutoGenerateSelectButton="True">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:BoundField DataField="hhID" HeaderText="hhID" ReadOnly="True" SortExpression="hhID" />
                        <asp:BoundField DataField="alrgName" HeaderText="Allergy Name" SortExpression="alrgName" />
                        <asp:BoundField DataField="SeverityName" HeaderText="Severity" SortExpression="SeverityName" />
                        <asp:BoundField DataField="MedicationTaken" HeaderText="MedicationTaken" SortExpression="MedicationTaken" />
                        <asp:BoundField DataField="What_Happened" HeaderText="What Happened" SortExpression="What_Happened" />
                        <asp:BoundField DataField="Happened_ON" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Happend on Date" SortExpression="Happened_ON" />
                    </Columns>
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            
            <td class="td_width_16" >
                Allergy Name</td>
            <td class="td_width_16" >
                <asp:DropDownList ID="cboAllergy" runat="server" class="form-control1">
                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem>Vaccine</asp:ListItem>
                    <asp:ListItem>Booster Dose</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="td_width_16"  >
                Severity</td>
            <td class="td_width_16" valign="top"  >
                <asp:DropDownList ID="cboSeverity" runat="server" class="form-control1">
                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem>Vaccine</asp:ListItem>
                    <asp:ListItem>Booster Dose</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="td_width_16" >
                Medication taken</td>
            <td class="td_width_16"  >
                <asp:TextBox ID="txtMedication" runat="server" Width="130px" BorderWidth="1px" 
                    BorderColor="Black"></asp:TextBox>
            </td>
            <td class="td_width_4">

                <asp:TextBox ID="txthhID" runat="server" Visible="False" Width="15px"></asp:TextBox>

            </td>
        </tr>
        <tr>
            
            <td class="td_width_16" >
                Effect</td>
            <td class="td_width_16" colspan="3">
                <asp:TextBox ID="txtEffect" runat="server" Width="410px" BorderWidth="1px" 
                    BorderColor="Black" Height="16px"></asp:TextBox>
            </td>
            <td class="td_width_16" >
                Happened On
            </td>
            <td class="td_width_16"  >
                <asp:TextBox ID="txtHappendON" runat="server" Width="130px" BorderWidth="1px" 
                    BorderColor="Black"></asp:TextBox>
                <asp:CalendarExtender ID="txtHappendON_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="txtHappendON">
                </asp:CalendarExtender>
            </td>
            <td class="td_width_4">
                <asp:Button ID="btnSaveHistory" runat="server" Text="Save" Width="50px" 
                    BorderWidth="1px" BorderColor="Black" />
            </td>
        </tr>
        <tr>
            
            <td class="td_width_16" >
                &nbsp;</td>
            <td class="td_width_16" colspan="3">
                &nbsp;</td>
            <td class="td_width_16" >
                &nbsp;</td>
            <td class="td_width_16"  >
                &nbsp;</td>
            <td class="td_width_4">
                &nbsp;</td>
        </tr>
        <tr>
            
            <td class="td_width_16" style="width: 110px; height: 40px;" valign="top" >
                Physical Activity Problem</td>
            <td class="td_width_16" style="width: 181px; height: 40px;" valign="top" >
                <asp:DropDownList ID="cboPhysicalActivity" runat="server" class="form-control1" valign="top" AutoPostBack="True" >
                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem>No</asp:ListItem>
                    <asp:ListItem>Yes</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="td_width_16" style="height: 40px;" valign="top" >
                Details</td>
            <td class="td_width_16" colspan="3" rowspan="2" valign="top" >
                <asp:TextBox ID="txtPhysicalActivity" runat="server" Width="377px" BorderWidth="1px" 
                    BorderColor="Black" TextMode="MultiLine" Height="39px"></asp:TextBox>
            </td>
            <td class="td_width_4" style="height: 40px" valign="top">
                <asp:Button ID="btnSaveHistory0" runat="server" Text="Save" Width="50px" 
                    BorderWidth="1px" BorderColor="Black" />
                </td>
        </tr>
        <tr>
            
            <td class="td_width_16" >
                &nbsp;</td>
            <td class="td_width_16" >
                &nbsp;</td>
            <td class="td_width_16"  >
                &nbsp;</td>
            <td class="td_width_4">
                &nbsp;</td>
        </tr>
        <tr>
            
            <td class="td_width_16" >
                &nbsp;</td>
            <td class="td_width_16" >
                &nbsp;</td>
            <td class="td_width_16"  >
                &nbsp;</td>
            <td class="td_width_16" valign="top"  >
                &nbsp;</td>
            <td class="td_width_16" >
                &nbsp;</td>
            <td class="td_width_16"  >
                &nbsp;</td>
            <td class="td_width_4">
                &nbsp;</td>
        </tr>
        <tr>
            
            <td class="td_width_16" >
                &nbsp;</td>
            <td class="td_width_16" >
                &nbsp;</td>
            <td class="td_width_16"  >
                &nbsp;</td>
            <td class="td_width_16" valign="top"  >
                &nbsp;</td>
            <td class="td_width_16" >
                &nbsp;</td>
            <td class="td_width_16"  >
                &nbsp;</td>
            <td class="td_width_4">
                &nbsp;</td>
        </tr>
        <tr>
            
            <td class="td_width_16" colspan="2">
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT RegNo, SName, ClassName, SecName FROM vw_Student WHERE (ASID = @ASID) and [SName] Like '%SearchByName%' or @SearchByName is null">
                    <SelectParameters>
                        <asp:SessionParameter Name="ASID" SessionField="ASID" />
                        <asp:ControlParameter ControlID="txtName" Name="SearchByName" PropertyName="Text" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSourceVacc" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT [svID], [vacCode], [vacName], [vacType], [Due_Date], [Performed_Date], [Remarks] FROM [vw_VaccinationDetails] WHERE ([SID] = @SID)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="txtSID" Name="SID" PropertyName="Text" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
                 <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <asp:SqlDataSource ID="SqlDataSourceHealthHistory" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT [hhID], [What_Happened], [SeverityID], [MedicationTaken], [SeverityName], [alrgName],[Happened_ON] FROM [vw_Student_Health_History] WHERE ([SID] = @SID)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="txtSID" Name="SID" PropertyName="Text" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
                </td>
            <td class="td_width_16"  >
                &nbsp;</td>
            <td class="td_width_16" valign="top"  >
                &nbsp;</td>
            <td class="td_width_16" >
                &nbsp;</td>
            <td class="td_width_16"  >
                &nbsp;</td>
            <td class="td_width_4">
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>
