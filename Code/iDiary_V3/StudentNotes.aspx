<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.master" EnableEventValidation="false"  CodeBehind="StudentNotes.aspx.vb" Inherits="iDiary_V3.StudentNotes" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Heading" runat="server">
    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />--%>
  
     <div class="col_3" style="margin-top: 20px;" id="panelEnquiryNo" runat="server">
        <div class="col-md-3 widget widget1" style="width: 100%">
            <div class="r3_counter_box">
    <table class="table">
        <tr>
            <td class="auto-style2">Admin/Reg No.</td>
            <td class="auto-style5">
                <asp:TextBox ID="txtAdminNo" runat="server" CssClass="textbox"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnFind" runat="server" Text="&gt;&gt;" 
                    CssClass="btn btn-primary" />
            </td>
            <td class="auto-style4">Name of the Student</td>
            <td class="auto-style4">
                <asp:TextBox ID="txtSName" runat="server" CssClass="textbox"></asp:TextBox>
            </td>
            <td width="10%">
            &nbsp;&nbsp;
                <asp:Button ID="btnNameSearch" runat="server" Text="&gt;&gt;" 
                    CssClass="btn btn-primary" />
            </td>
        </tr>
        <tr>
            <td colspan="5">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" AutoGenerateSelectButton="True" CssClass="Grid" DataSourceID="SqlDataSource1" Width="98%">
                    <Columns>
                        <asp:BoundField DataField="RegNo" HeaderText="Reg No" SortExpression="RegNo" />
                        <asp:BoundField DataField="SName" HeaderText="Name" SortExpression="SName" />
                               <asp:BoundField DataField="SchoolName" HeaderText="School" SortExpression="SchoolName" />
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
            <td class="auto-style2">School Name</td>
            <td width="30%" colspan="2">
                <asp:TextBox ID="txtSchoolName" runat="server" CssClass="textbox" Width="248px" ReadOnly="True" 
                    ></asp:TextBox>
            </td>
            <td>&nbsp;</td>
            
            <td rowspan="4"> <asp:Image ID="imgPhoto" CssClass="textbox" runat="server" Height="155px" Width="149px" onerror="imgError(this);"/></td>
            
        </tr>
        <tr>
            <td width="15%">Father Name</td>
            <td class="auto-style5">
                <asp:TextBox ID="txtFName" runat="server" CssClass="textbox" ReadOnly="True" 
                    ></asp:TextBox>
            </td>
            <td>Mother Name</td>
            <td>
                <asp:TextBox ID="txtMName" runat="server" CssClass="textbox" ReadOnly="True" 
                   ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style2">Admission Date</td>
            <td style="margin-left: 40px" class="auto-style5">
                <asp:TextBox ID="txtAdmissionDate" runat="server"  ReadOnly="True" 
                    CssClass="textbox"></asp:TextBox>
            </td>
           
            <td>Date of Birth</td>
            <td>
                <asp:TextBox ID="txtDOB" runat="server" ReadOnly="True" 
                   CssClass="textbox"></asp:TextBox>
            </td>
           
        </tr>
        <tr>
             <td width="15%">Class - Section</td>
            <td class="auto-style5">
                <asp:TextBox ID="txtClass" runat="server" Width="70px" ReadOnly="True" 
                 CssClass="textbox"></asp:TextBox>
                &nbsp;<asp:TextBox ID="txtSec" runat="server" Width="40px" ReadOnly="True" 
                 CssClass="textbox"></asp:TextBox>
            </td>
             <td>Date</td>
             <td><asp:TextBox ID="txtdate" runat="server" 
                   CssClass="textbox"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="txtdate_CalendarExtender" runat="server" TargetControlID="txtdate" Format="dd/MM/yyyy" />
                 <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999" MaskType="Date" TargetControlID="txtdate" PromptCharacter="_"> </ajaxToolKit:MaskedEditExtender>
               </td>
        </tr>
        <tr>

             <td class="auto-style2">Comments</td>
            <td style="margin-left: 40px" class="auto-style5">
                <asp:TextBox ID="txtcomments" runat="server" Height="66px" TextMode="MultiLine" Width="256px"></asp:TextBox> </td>
            <td class="auto-style4"></td>
            <td class="auto-style4">
                <asp:TextBox ID="txtDOBInWords" runat="server" BorderWidth="1px" 
                    Width="55px" Visible="False"></asp:TextBox>
             </td>
            <td width="30%">
                <asp:Button ID="btnGenerate" runat="server" Text="OK" 
                    CssClass="btn btn-primary" Enabled="false" Visible="false"  />
             </td>
        </tr>
        <tr>
            <td class="auto-style2">Attachments</td>
            <td style="margin-left: 40px" class="auto-style5">
                <asp:FileUpload ID="myfile" runat="server" CssClass="FileUpload"  />&nbsp;<asp:ImageButton ID="ImageButton1" ImageUrl="~/images/downloadicon.png" OnClick="downloadnotes_Click" runat="server" />
            </td>
            <td class="auto-style4" colspan="2">
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT RegNo, SName, ClassName, SecName, FName, MName,AdmissionDate,DOB,SchoolName FROM vw_Student WHERE [SName] Like '%SearchByName%' or @SearchByName is null">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="txtSName" Name="SearchByName" PropertyName="Text" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:TextBox ID="txtGender" runat="server" BorderWidth="1px" 
                    Width="55px" Visible="False"></asp:TextBox>
            </td>
            
            <td width="30%">
                <asp:TextBox ID="txtRollNo" runat="server" BorderWidth="1px" 
                    Width="55px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="txtSID" runat="server" BorderWidth="1px" 
                    Width="55px" Visible="False"></asp:TextBox><asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            </td>
        </tr>
        <tr>
            <td class="auto-style2"><asp:Button ID="btnsave" runat="server" Text="Save" CssClass="btn btn-primary" />&nbsp; <asp:Button ID="btnremove" runat="server" Text="Remove" CssClass="btn btn-primary" /></td>
            <td style="margin-left: 40px" class="auto-style5">
                </td>
            <td class="auto-style4"> </td>
            <td class="auto-style4"> &nbsp;</td>
            <td width="30%"></td>
        </tr>
        <tr>
            <td colspan="7">
                 <div id="gvDiv" style="width: 1000px; max-height: 1000px; overflow-x: scroll; text-align: center;">

                <asp:GridView ID="GridView2" runat="server" DataSourceID="StudentNotepad" DataKeyNames="StudentNotesID,NoteDocPath"  CssClass="Grid" Width="98%" AutoGenerateColumns="False">
                 
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" ItemStyle-Width="20px">
<ItemStyle Width="30px"></ItemStyle>
                        </asp:CommandField>
                        <asp:BoundField DataField="StudentNotesID" ItemStyle-Width="10px" HeaderText="StudentNotesID" SortExpression="StudentNotesID" InsertVisible="False" ReadOnly="True"  >
<ItemStyle Width="10px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="EntryDate" ItemStyle-Width="40px" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Date" SortExpression="EntryDate" >
<ItemStyle Width="50px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="Comments" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"  ItemStyle-Width="800px" HeaderText="Comments" SortExpression="Comments" >
               
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
<ItemStyle Width="600px" HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
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
                <asp:SqlDataSource ID="StudentNotepad" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT [StudentNotesID], [EntryDate], [Comments], [NoteDocPath] FROM [StudentNotes]"></asp:SqlDataSource>

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
        .auto-style2 {
            width: 18%;
        }
        .auto-style4 {
        }
        .auto-style5 {
            width: 15%;
        }
    </style>
</asp:Content>

