<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ExamAdminMasterPage.Master" CodeBehind="ExamSubjectGroupmaster.aspx.vb" Inherits="iDiary_V3.ExamSubGroupmaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SubHeading" runat="server">Subject Group Master
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
   <%-- <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
  --%>
    <table class="table">
        <tr>
            <td style="width:40%; " rowspan="16">

                    <strong>Major Groups<br />

                     <asp:DropDownList ID="cboMajors" runat="server" Width="200px"   CssClass="Dropdown" AutoPostBack="True">
                               
                            </asp:DropDownList>

                    <br />

                    <br />
                    Minor Groups</strong><br />

                    <asp:ListBox ID="lstSubGrp" runat="server" Height="255px" Width="200px"  CssClass="list"
                        AutoPostBack="True"></asp:ListBox>

            </td>
            <td style="width:25%; ">

                    <strong>Subject Group Name</strong></td>
            <td style="width:35%; ">

                    <asp:TextBox ID="txtSubGrpName" runat="server" CssClass="textbox"></asp:TextBox>

            </td>
        </tr>
         <tr>
            <td style="width:25%">

                    <strong>Group Type</strong></td>
            <td style="width:35%">

                     <asp:DropDownList ID="cboGroupType" runat="server" CssClass="Dropdown" AutoPostBack="True">
                                <asp:ListItem>Major</asp:ListItem>
                                <asp:ListItem>Minor</asp:ListItem>
                            </asp:DropDownList>

            </td>
        </tr>
         <tr>
            <td style="width:25%">

                    <strong>Display Order</strong></td>
            <td style="width:35%">

                <strong>
                    <asp:TextBox ID="txtDisplayOrder" runat="server" CssClass="textbox" TextMode="Number">0</asp:TextBox>
                    </strong>

            </td>
        </tr>
         <tr>
            <td style="width:25%">

                    <strong>Part of Calculation</strong></td>
            <td style="width:35%">

                     <asp:DropDownList ID="cboPartOfCalculation" runat="server" CssClass="Dropdown" AutoPostBack="True">
                                <asp:ListItem>Yes</asp:ListItem>
                                <asp:ListItem>No</asp:ListItem>
                            </asp:DropDownList>

            </td>
        </tr>
         <tr>
            <td style="width:25%">

                    <strong>Is Attendance Type</strong></td>
            <td style="width:35%">

                     <asp:DropDownList ID="cboIsAttendanceType" runat="server" CssClass="Dropdown">
                                <asp:ListItem>Yes</asp:ListItem>
                                <asp:ListItem>No</asp:ListItem>
                            </asp:DropDownList>

            </td>
        </tr>
         <tr>
            <td style="width:25%">

                <asp:Label ID="lblExamGroups" runat="server" style="font-weight: 700" Text="Major Groups"></asp:Label>
             </td>
            <td style="width:35%">

                    <strong>
                     <asp:DropDownList ID="cboMajorGroups" runat="server" CssClass="Dropdown" AutoPostBack="True">
                               
                            </asp:DropDownList>
                    </strong>

              </td>
        </tr>
          <tr>
            <td style="width:25%">

                    <strong>
                    Class Groups</strong></td>
            <td style="width:35%">
                 <asp:CheckBoxList ID="cblExamGroups" runat="server" DataSourceID="sdsExamGroups" DataTextField="ExamGroupName" DataValueField="examGroupID">
                    </asp:CheckBoxList>
                    <strong>
                   
                   
                        <br />
                    <asp:CheckBox ID="cbAll" runat="server" AutoPostBack="True"  Font-Bold="True"  Text="Check/Uncheck All" ForeColor="#660033" Font-Size="Medium" />
                 </strong>
              </td>
        </tr>
          <tr>
            <td style="width:25%">

                    &nbsp;</td>
            <td style="width:35%">

                    &nbsp;</td>
        </tr>
          <tr>
            <td colspan="2">

                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnNew" runat="server"  Text="New" 
                        CssClass="btn btn-primary" />
            
                &nbsp;&nbsp;&nbsp;&nbsp;
            
                <asp:Button ID="btnRemove" runat="server" Text="Remove" Visible="false" CssClass="btn btn-primary" />

            </td>
        </tr>
        <tr>
            <td style="width:25%">

            </td>
            <td style="width:35%">

              </td>
        </tr>
        <tr>
            <td style="width:25%">

            </td>
            <td style="width:35%">

              </td>
        </tr>
        <tr>
            <td style="width:25%">

            </td>
            <td style="width:35%">

              </td>
        </tr>
        <tr>
            <td style="width:25%">

            </td>
            <td style="width:35%">

              </td>
        </tr>
        <tr>
            <td style="width:25%">

            </td>
            <td style="width:35%">

              </td>
        </tr>
        <tr>
            <td style="width:25%">

            </td>
            <td style="width:35%">

              </td>
        </tr>
        <tr>
            <td style="width:25%">

            </td>
            <td style="width:35%">

              </td>
        </tr>
        <tr>
            <td style="width:40%">

                    <strong>
                    <asp:SqlDataSource ID="sdsExamGroups" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT [ExamGroupName], [examGroupID] FROM [ExamGroups] ORDER BY [DisplayOrder]"></asp:SqlDataSource>
                    <asp:TextBox ID="txtID" runat="server" Width="25px" Visible="False"></asp:TextBox>
                    </strong>

                </td>
            <td style="width:25%">

                    &nbsp;</td>
            <td style="width:35%">

              </td>
        </tr>
    </table>
    
</asp:Content>
