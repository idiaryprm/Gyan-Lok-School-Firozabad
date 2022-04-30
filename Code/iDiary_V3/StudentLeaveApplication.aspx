<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.master" EnableEventValidation="false"  CodeBehind="StudentLeaveApplication.aspx.vb" Inherits="iDiary_V3.StudentLeaveApplication" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Heading" runat="server">
    Student Leave Application
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
  
     --%>
    <div class="col_3" style="margin-top: 20px;" id="panelEnquiryNo" runat="server">
        <div class="col-md-3 widget widget1" style="width: 100%">
            <div class="r3_counter_box">
    <table class="table">
        <tr>
            <td class="auto-style2" colspan="4">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" AutoGenerateSelectButton="True" CssClass="Grid" DataSourceID="SqlDataSource1" Width="98%">
                    <Columns>
                        <asp:BoundField DataField="RegNo" HeaderText="Reg No" SortExpression="RegNo" />
                        <asp:BoundField DataField="SName" HeaderText="Name" SortExpression="SName" />
                        <asp:BoundField DataField="ClassName" HeaderText="Class" SortExpression="ClassName" />
                        <asp:BoundField DataField="SecName" HeaderText="Sec" SortExpression="SecName" />
                        <asp:BoundField DataField="FName" HeaderText="Father Name" SortExpression="FName" />
                        <asp:BoundField DataField="MName" HeaderText="Mother Name" SortExpression="MName" />
                        <asp:BoundField DataField="AdmissionDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Admission Date" HtmlEncode="False" SortExpression="AdmissionDate" />
                        <asp:BoundField DataField="DOB" DataFormatString="{0:d}" HeaderText="Date of Birth" HtmlEncode="False" SortExpression="DOB" />
                    </Columns>
                    
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td class="auto-style2">Admin/Reg No.</td>
            <td width="20%">
                <asp:TextBox ID="txtAdminNo" runat="server" CssClass="textbox"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnFind" runat="server" Text="&gt;&gt;" 
                    CssClass="btn btn-primary" />
            </td>
            <td class="auto-style4">Name of the Student</td>
            <td width="10%">
                <asp:TextBox ID="txtSName" runat="server" CssClass="textbox"></asp:TextBox>
            &nbsp;&nbsp;
                <asp:Button ID="btnNameSearch" runat="server" Text="&gt;&gt;" 
                    CssClass="btn btn-primary" />
            </td>
        </tr>
        <tr>
            <td class="auto-style2">Father Name</td>
            <td width="20%">
                <asp:TextBox ID="txtFName" runat="server" CssClass="textbox" ReadOnly="True" 
                    ></asp:TextBox>
            </td>
            <td class="auto-style4">Mother Name</td>
            <td width="10%">
                <asp:TextBox ID="txtMName" runat="server" CssClass="textbox" ReadOnly="True" 
                   ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style2">Mother Name</td>
            <td width="20%">
                <asp:TextBox ID="txtAdmissionDate" runat="server"  ReadOnly="True" 
                    CssClass="textbox"></asp:TextBox>
            </td>
            <td class="auto-style4">Date of Birth</td>
            <td width="10%">
                <asp:TextBox ID="txtDOB" runat="server" ReadOnly="True" 
                   CssClass="textbox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style2">Class - Section</td>
            <td width="20%">
                <asp:TextBox ID="txtClass" runat="server" Width="92px" ReadOnly="True" 
                 CssClass="textbox"></asp:TextBox>
                &nbsp;
                <asp:TextBox ID="txtSec" runat="server" Width="69px" ReadOnly="True" 
                 CssClass="textbox"></asp:TextBox>
            </td>
            <td class="auto-style4">
                <asp:TextBox ID="txtDOBInWords" runat="server" BorderWidth="1px" 
                    Width="55px" Visible="False"></asp:TextBox>
            </td>
            <td width="10%">
                <asp:Button ID="btnGenerate" runat="server" Text="OK" 
                    CssClass="btn btn-primary" Enabled="false" Visible="false"  />
            </td>
        </tr>
        <tr>
            <td class="auto-style2">Leave Date From</td>
            <td width="30%">
                            <asp:TextBox ID="txtFrom" CssClass="textbox" runat="server"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="txtFrom_CalendarExtender" Format="dd/MM/yyyy" runat="server" TargetControlID="txtFrom"></ajaxToolkit:CalendarExtender>
            </td>
            <td rowspan="4" class="auto-style4">&nbsp;</td>
            
            <td rowspan="4"><asp:Image ID="imgPhoto" CssClass="textbox" runat="server" Height="155px" Width="149px" onerror="imgError(this);"/></td>
            
        </tr>
        <tr>
            <td width="15%">Leave Date To</td>
            <td width="30%">
                            <asp:TextBox ID="txtTo" CssClass="textbox" runat="server"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="txtTo_CalendarExtender" Format="dd/MM/yyyy" runat="server" TargetControlID="txtTo"></ajaxToolkit:CalendarExtender>
                        </td>
        </tr>
        <tr>
            <td class="auto-style2">Reason For Leave</td>
            <td width="30%" style="margin-left: 40px">
                            <asp:TextBox ID="txtReason" runat="server" Width="355px" CssClass="textbox"></asp:TextBox>
                            </td>
           
        </tr>
        <tr>
             <td width="15%">Remarks (Optional)</td>
            <td width="30%">
                            <asp:TextBox ID="txtMessage" runat="server" Height="78px" Width="356px" CssClass="textbox" TextMode="MultiLine"></asp:TextBox>
                        </td>
        </tr>
        <tr>
            <td class="auto-style2">Attachments</td>
            <td width="30%" style="margin-left: 40px">
                <asp:FileUpload ID="myfile" runat="server" CssClass="FileUpload"  />&nbsp;<asp:ImageButton ID="ImageButton1" ImageUrl="~/images/downloadicon.png" OnClick="downloadnotes_Click" runat="server" />
            </td>
            <td class="auto-style4">
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT RegNo, SName, ClassName, SecName, FName, MName,AdmissionDate,DOB FROM vw_Student WHERE [SName] Like '%SearchByName%' or @SearchByName is null">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="txtSName" Name="SearchByName" PropertyName="Text" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
            
            <td width="30%">
                <asp:TextBox ID="txtRollNo" runat="server" BorderWidth="1px" 
                    Width="55px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="txtSID" runat="server" BorderWidth="1px" 
                    Width="55px" Visible="False"></asp:TextBox><asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <asp:TextBox ID="txtGender" runat="server" BorderWidth="1px" 
                    Width="55px" Visible="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style2"><asp:Button ID="btnsave" runat="server" Text="Save" CssClass="btn btn-primary" />&nbsp; <asp:Button ID="btnremove" runat="server" Text="Remove" CssClass="btn btn-primary" /></td>
            <td width="30%" style="margin-left: 40px">
                </td>
            <td class="auto-style4"> </td>
            <td width="30%"></td>
        </tr>
        <tr>
            <td colspan="4">
                 <div id="gvDiv" style="width: 1000px; max-height: 1000px; overflow-x: scroll; text-align: center;">

                <asp:GridView ID="GridView2" runat="server" DataKeyNames="slID,FilePath"  CssClass="Grid" Width="98%" AutoGenerateColumns="False" AutoGenerateDeleteButton="false" OnRowDeleting="GridView2_RowDeleting">
                 
                    <Columns>
                        <%--<asp:CommandField ShowDeleteButton="True" ItemStyle-Width="20px">
<ItemStyle Width="30px"></ItemStyle>
                        </asp:CommandField>--%>
                        <asp:TemplateField HeaderText="Select">
     <ItemTemplate>
       <asp:LinkButton ID="LinkButton1" 

         CommandArgument='<%# Eval("slID") %>' 

         CommandName="Delete" runat="server">
         Delete</asp:LinkButton>
     </ItemTemplate>
   </asp:TemplateField>
                        <asp:BoundField DataField="slID" ItemStyle-Width="10px" HeaderText="ID" SortExpression="slID" InsertVisible="False" ReadOnly="True"  >
<ItemStyle Width="10px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="LeaveDate" ItemStyle-Width="40px" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Date" SortExpression="LeaveDate" >
<ItemStyle Width="50px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="Reason" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"  ItemStyle-Width="800px" HeaderText="Reason" SortExpression="Reason" >
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
<ItemStyle Width="600px" HorizontalAlign="left" VerticalAlign="Middle"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="Message" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"  ItemStyle-Width="800px" HeaderText="Message" SortExpression="Message" >
                                       <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
<ItemStyle Width="600px" HorizontalAlign="left" VerticalAlign="Middle"></ItemStyle>
                        </asp:BoundField>
               
                        <asp:TemplateField ItemStyle-Width="60px">
                                                 <ItemTemplate>
                                                    <asp:ImageButton ImageUrl="~/images/downloadicon.png"  RowIndex='<%# Container.DataItemIndex%>'   ID="imagebutton2" OnClick="downloadnotes_Click"  runat="server" />
                                                </ItemTemplate>
                                                 <ItemStyle Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                        
                       <%-- <asp:ImageField DataImageUrlField="NoteDocPath" NullImageUrl="~/images/images.png"  HeaderText="Notes Name"></asp:ImageField>--%>
                       <%-- <asp:HyperLinkField DataTextField="NoteDocPath" DataNavigateUrlFields="NoteDocPath" Target="_blank"   DataNavigateUrlFormatString="" HeaderText="Notes Name" SortExpression="NoteDocPath" />--%>
                    </Columns>
                   
                </asp:GridView>
                     </div>
                <asp:SqlDataSource ID="StudentNotepad" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT slID,LeaveDate,Reason,Message,FilePath,Sid,EntryDate FROM StudentLeave WHERE SID=0"></asp:SqlDataSource>

            </td>
        </tr>
        <tr>
            <td class="auto-style2"><asp:Button ID="btnprint" runat="server" Text="Print" CssClass="btn btn-primary" Width="54px"></asp:Button>&nbsp;<asp:Button ID="btnexporttoexel" runat="server" Text="Export To Excel" CssClass="btn btn-primary" Width="145px"></asp:Button></td>
        </tr>



    </table>

                 </div>
        </div>
        <div class="clearfix"></div>
    </div>
    <%--</div>--%>
</asp:Content>
<asp:Content ID="Content3" runat="server" contentplaceholderid="head">
    <style type="text/css">
        .auto-style1 {
            height: 39px;
        }
        .auto-style2 {
        }
        .auto-style3 {
            height: 39px;
            width: 18%;
        }
        .auto-style4 {
            width: 21%;
        }
        .auto-style5 {
            height: 39px;
            width: 21%;
        }
    </style>
</asp:Content>

