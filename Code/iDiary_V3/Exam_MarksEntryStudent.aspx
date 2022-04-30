<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.master" enableEventValidation="false" CodeBehind="Exam_MarksEntryStudent.aspx.vb" Inherits="iDiary_V3.Exam_MarksEntryStudent" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Heading" runat="server">
    Exam Marks Entry
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      
   <script type="text/javascript">
       function tabE(obj, e) {
           var e = (typeof event != 'undefined') ? window.event : e;// IE : Moz 
           if (e.keyCode == 13) {
               var ele = document.forms[0].elements;
               for (var i = 0; i < ele.length; i++) {
                   var q = (i == ele.length - 1) ? 0 : i + 1;// if last element : if any other 
                   if (obj == ele[i]) { ele[q].focus(); break }
               }
               return false;
           }
       }
</script> 

      <script type="text/javascript">
          function tabEnter(obj, e) {
              var e = (typeof event != 'undefined') ? window.event : e;// IE : Moz 
              if (e.keyCode == 13) {
                  var ele = document.forms[0].elements;
                  for (var i = 0; i < ele.length; i++) {
                      var q = (i == ele.length - 1) ? 0 : i + 2;// if last element : if any other 
                      if (obj == ele[i]) { ele[q].focus(); break }
                  }
                  return false;
              }
          }
</script> 
  
<div class="col_3" style="margin-top: 20px;" id="panelEnquiryNo" runat="server">
 
    <div class="col-md-3 widget widget1" style="width: 100%">
            <div class="r3_counter_box">

                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>

                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                       <%--  <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
                        <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
                        <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
                        <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />--%>

                         <table class="table">
                         <tr>
                             <td colspan="7">
                                 <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" AutoGenerateSelectButton="True" CssClass="Grid" DataSourceID="SqlDataSource1" Width="99%">
                                     <Columns>
                                         <asp:BoundField DataField="RegNo" HeaderText="Reg. No" SortExpression="RegNo" />
                                         <asp:BoundField DataField="SName" HeaderText="Name" SortExpression="SName" />
                                         <asp:BoundField DataField="ClassName" HeaderText="Class" SortExpression="ClassName" />
                                         <asp:BoundField DataField="SecName" HeaderText="Sec" SortExpression="SecName" />
                                         <asp:BoundField DataField="ClassRollNo" HeaderText="Roll No" SortExpression="ClassRollNo" />
                                         <asp:BoundField DataField="FName" HeaderText="Father Name" SortExpression="FName" />
                                         <asp:BoundField DataField="MName" HeaderText="Mother Name" SortExpression="MName" />
                                     </Columns>
                                 </asp:GridView>
                                 <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT regno, SName, ClassName, SecName,classrollNo, FName, MName,AdmissionDate,DOB FROM vw_Student WHERE [SName] Like '%SearchByName%' or @SearchByName is null">
                                     <SelectParameters>
                                         <asp:ControlParameter ControlID="txtSName" Name="SearchByName" PropertyName="Text" />
                                     </SelectParameters>
                                 </asp:SqlDataSource>
                             </td>
                         </tr>  
                             <tr>
                             <td style="width:15%">Student Name</td>
                             <td style="width:20%">
                                 <asp:TextBox ID="txtSName" runat="server" CssClass="textbox"></asp:TextBox>
                                 </td>
                             <td style="width:10%">
                                 <asp:Button ID="btnSName" runat="server" CssClass="btn btn-primary"  Text="&gt;&gt;" Height="25px" />
                                 </td>
                             <td style="width:15%">Reg/Admn No</td>
                             <td style="width:20%">
                                 <asp:TextBox ID="txtRegNo" runat="server" CssClass="textbox"></asp:TextBox>
                                 </td>
                             <td style="width:10%">
                                 <asp:Button ID="btnRegNo" runat="server" CssClass="btn btn-primary" Text="&gt;&gt;" Height="25px" />
                                 </td>
                             <td style="width:10%"></td>
                         </tr>  
                             <tr>
                             <td style="width:15%">Roll No.</td>
                             <td style="width:20%">
                                 <asp:TextBox ID="txtRollNo" runat="server" CssClass="textbox" ReadOnly="True"></asp:TextBox>
                                 </td>
                             <td style="width:10%"></td>
                             <td style="width:15%">Class - Section</td>
                             <td style="width:20%">
                                 <asp:TextBox ID="txtClass" runat="server" CssClass="textbox" ReadOnly="True"></asp:TextBox>
                                 </td>
                             <td style="width:10%"></td>
                             <td style="width:10%"></td>
                         </tr>  
                             <tr>
                             <td style="width:15%">Father Name</td>
                             <td style="width:20%">
                                 <asp:TextBox ID="txtFName" runat="server" CssClass="textbox" ReadOnly="True"></asp:TextBox>
                                 </td>
                             <td style="width:10%"></td>
                             <td style="width:15%">Mother Name</td>
                             <td style="width:20%">
                                 <asp:TextBox ID="txtMName" runat="server" CssClass="textbox" ReadOnly="True"></asp:TextBox>
                                 </td>
                             <td style="width:10%"></td>
                             <td style="width:10%"></td>
                         </tr>  
                             <tr>
                             <td style="width:15%">Subject Group</td>
                             <td style="width:20%">
                                 <asp:DropDownList ID="cboSubjectGroup" runat="server" AutoPostBack="True" CssClass="Dropdown">
                                 </asp:DropDownList>
                                 </td>
                             <td style="width:10%"></td>
                             <td style="width:15%">Subject Sub Group</td>
                             <td style="width:20%">
                                 <asp:DropDownList ID="cboSubSubjectGroup" runat="server" AutoPostBack="True" CssClass="Dropdown">
                                 </asp:DropDownList>
                                 </td>
                             <td style="width:10%"></td>
                             <td style="width:10%"></td>
                         </tr>  
                             <tr>
                             <td style="width:15%">Term</td>
                             <td style="width:20%">
                                 <asp:DropDownList ID="cboTerm" runat="server" AutoPostBack="True" CssClass="Dropdown">
                                 </asp:DropDownList>
                                 </td>
                             <td style="width:10%"></td>
                             <td style="width:15%">Sub-Term</td>
                             <td style="width:20%">
                                 <asp:DropDownList ID="cboMinorTerm" runat="server" CssClass="Dropdown">
                                 </asp:DropDownList>
                                 </td>
                             <td style="width:10%">
                                 <asp:Button ID="btnNext" runat="server" CssClass="btn btn-primary" Text="Next" Width="90px" />
                                 </td>
                             <td style="width:10%"></td>
                         </tr>  
                             <tr>
                                 <td style="width:15%">
                                     <asp:Label ID="lblMaxMarks" runat="server">Max Marks/Grade</asp:Label>
                                 </td>
                                 <td style="width:20%">
                                     <asp:TextBox ID="txtMaxMarks" runat="server" CssClass="textbox" Enabled="true"></asp:TextBox>
                                 </td>
                                 <td style="width:10%">
                                     <asp:Button ID="btnChange" runat="server" CssClass="btn btn-primary" Text="Change" />
                                 </td>
                                 <td colspan="2">
                                     <asp:Label ID="lblStatus" runat="server" Font-Bold="True" ForeColor="Navy" style="color: #FF3300"></asp:Label>
                                 </td>
                                 <td style="width:10%">&nbsp;</td>
                                 <td style="width:10%">&nbsp;</td>
                             </tr>
                             <tr>
                             <td colspan="3">
                                    <asp:Label ID="lblSID" runat="server" Visible="False"></asp:Label>
                                    <asp:Label ID="lblClassId" runat="server" Visible="False"></asp:Label>
                                    <asp:Label ID="lblSecID" runat="server" Visible="False"></asp:Label>
                                    <asp:Label ID="lblGrpID" runat="server" Visible="False"></asp:Label>
                             </td>
                             <td style="width:15%"></td>
                             <td style="width:20%"></td>
                             <td style="width:10%"></td>
                             <td style="width:10%"></td>
                         </tr>  
                             <tr>
                             <td style="width:15%"></td>
                             <td style="width:20%"></td>
                             <td style="width:10%"></td>
                             <td style="width:15%"></td>
                             <td style="width:20%"></td>
                             <td style="width:10%"></td>
                             <td style="width:10%"></td>
                         </tr>  
                             <tr>
                             <td colspan="6">
                                 <div id="gvDiv" style="width: 100%; max-height: 350px; overflow-y: scroll;">
                                        <asp:GridView ID="gvmarks" runat="server" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="SubjectID,DisplayType,EntryType" DataSourceID="GVCreateMarksEntry" CssClass="Grid" GridLines="Horizontal" ShowFooter="True" Visible="False" Width="95%">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr. No.">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="SubjectCode" HeaderText="Subject Code" SortExpression="SubjectCode">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundField>
                                               <asp:BoundField DataField="SubjectName" HeaderText="Subject Name" SortExpression="SubjectName" HeaderStyle-HorizontalAlign="Center">
                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="Max Marks">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="lblMaxM" runat="server" Text="" Width="100px" />
                                                    </ItemTemplate>
                                                    <ItemStyle Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Marks/Grade">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtMarks" runat="server" CssClass="textbox" Width="120px" />
                                                        <asp:DropDownList ID="cboRemark" runat="server" CssClass="Dropdown" Visible="true">
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Grades">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgrades" runat="server" CssClass="textbox" Width="120px" Enabled="false"  />

                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                            </Columns>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <SelectedRowStyle BackColor="#4DE427" />
                                            <FooterStyle BackColor="#ccff99" Font-Bold="True" />
                                        </asp:GridView>
                                        </div>
                                    <asp:SqlDataSource ID="GVCreateMarksEntry" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT [SubjectId],DisplayType,EntryType, [SubjectCode], [SubjectName] FROM [vw_ExamSubjectMapping]"></asp:SqlDataSource>

                             </td>
                             <td style="width:10%"></td>
                         </tr>    
                               <tr>
                             <td style="width:15%"></td>
                             <td style="width:20%"></td>
                             <td style="width:10%"></td>
                             <td style="width:15%"></td>
                             <td style="width:20%"></td>
                             <td style="width:10%"></td>
                             <td style="width:10%"></td>
                         </tr>  
                             <tr>
                             <td style="width:15%">
                                 <asp:Button ID="btnProceed" runat="server" CssClass="btn btn-primary" Text="Proceed" Width="130px" />
                                 </td>
                             <td style="width:20%">
                                 <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Proceed &amp; Save " Visible="False" Width="160px" />
                                 </td>
                             <td style="width:10%"></td>
                             <td style="width:15%"></td>
                             <td style="width:20%"></td>
                             <td style="width:10%"></td>
                             <td style="width:10%"></td>
                         </tr>    
                             <tr>
                                 <td style="width:15%">
                                     <asp:DropDownList ID="cboRemark" runat="server" Visible="False">
                                     </asp:DropDownList>
                                 </td>
                                 <td style="width:20%">
                                     <asp:Label ID="lblInputType" runat="server" Visible="False"></asp:Label>
                                 </td>
                                 <td style="width:10%">
                                     <asp:Label ID="lblMappedSID" runat="server" Visible="False"></asp:Label>
                                 </td>
                                 <td style="width:15%">
                                     <asp:Label ID="lblATT" runat="server" Visible="False"></asp:Label>
                                 </td>
                                 <td style="width:20%">
                                     <asp:Label ID="lblOutputType" runat="server" Visible="False"></asp:Label>
                                 </td>
                                 <td style="width:10%">
                                     <asp:GridView ID="gvGrade" runat="server" AutoGenerateColumns="False" DataSourceID="sdsGrade" Visible="False">
                                         <Columns>
                                             <asp:BoundField DataField="UpperValue" HeaderText="UpperValue" SortExpression="UpperValue" />
                                             <asp:BoundField DataField="LowerValue" HeaderText="LowerValue" SortExpression="LowerValue" />
                                             <asp:BoundField DataField="Grade" HeaderText="Grade" SortExpression="Grade" />
                                         </Columns>
                                     </asp:GridView>
                                     <asp:SqlDataSource ID="sdsGrade" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT [UpperValue], [LowerValue], [Grade] FROM [vw_ExamGradeMapping] Where GradeID=0"></asp:SqlDataSource>
                                 </td>
                                 <td style="width:10%">&nbsp;</td>
                             </tr>
                         </table>
                      
                         </ContentTemplate>
                </asp:UpdatePanel>
                

                
                          
            </div>
        </div>
        <div class="clearfix"></div>
    </div>
              
</asp:Content>


