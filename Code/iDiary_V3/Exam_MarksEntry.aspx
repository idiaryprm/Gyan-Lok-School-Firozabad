<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.master" enableEventValidation="false" CodeBehind="Exam_MarksEntry.aspx.vb" Inherits="iDiary_V3.Exam_MarksEntry" %>
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
  
   <%-- <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />--%>

<div class="col_3" style="margin-top: 20px;" id="panelEnquiryNo" runat="server">
 
    <div class="col-md-3 widget widget1" style="width: 100%">
            <div class="r3_counter_box">

                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>

                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                            
                         <table class="table">
                            <tr>
                                <td style="width: 15%">School Name</td>
                                <td colspan="3"><asp:DropDownList ID="cboSchoolName" runat="server" CssClass="Dropdown" Width="300px" AutoPostBack="true" ></asp:DropDownList>
</td>
                               
                                <td style="width: 15%"><asp:Label ID="lblSchoolID" runat="server" Visible="False"></asp:Label></td>
                                <td style="width: 15%">&nbsp;</td>
                             
                            </tr>
                            <tr>
                                <td style="width: 15%">Exam Group&nbsp;</td>
                                <td style="width: 15%">
                                    <asp:DropDownList ID="cboExamGroup" runat="server" AutoPostBack="True" CssClass="Dropdown">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 15%">Subject Group</td>
                                <td style="width: 15%">
                                    <asp:DropDownList ID="cboSubjectGroup" runat="server" AutoPostBack="True" CssClass="Dropdown">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 15%">
                                    Subject Sub Group</td>
                                <td style="width: 15%">
                                    <asp:DropDownList ID="cboSubSubjectGroup" runat="server" AutoPostBack="True" CssClass="Dropdown">
                                    </asp:DropDownList>
                                </td>
                               
                            </tr>
                            <tr>
                                <td style="width: 15%">Class</td>
                                <td style="width: 15%">
                                    <asp:DropDownList ID="cboClass" runat="server" AutoPostBack="True" CssClass="Dropdown">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 15%">Section</td>
                                <td style="width: 15%"><b>
                                    <asp:DropDownList ID="cboSection" runat="server" AutoPostBack="True" CssClass="Dropdown">
                                    </asp:DropDownList>
                                </b></td>
                                <td style="width: 15%">Subject</td>
                                <td>
                                    <asp:DropDownList ID="cboSubject" runat="server" AutoPostBack="True" CssClass="Dropdown">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 15%">Term</td>
                                <td style="width: 15%">
                                    <asp:DropDownList ID="cboTerm" runat="server" AutoPostBack="True" CssClass="Dropdown">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 15%">Sub-Term</td>
                                <td style="width: 15%">
                                    <asp:DropDownList ID="cboMinorTerm" runat="server" CssClass="Dropdown">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 15%">Status</td>
                                <td style="width: 15%"><b>
                                    <asp:DropDownList ID="cboStatus" runat="server" CssClass="Dropdown">
                                    </asp:DropDownList>
                                </b></td>
                                
                            </tr>
                           
                            <tr>
                                <td style="width: 15%">
                                    <asp:Label ID="lblMaxMarks" runat="server">Max Marks</asp:Label>
                                </td>
                                <td style="width: 15%">
                                    <asp:TextBox ID="txtMaxMarks" runat="server" CssClass="textbox" Enabled="false"></asp:TextBox>
                                </td>
                                <td style="width: 15%">
                                    <asp:Button ID="btnChange" runat="server" CssClass="btn btn-primary" Text="Change" />
                                </td>
                                <td colspan="2">
                                    <asp:Label ID="lblStatus" runat="server" Font-Bold="True" ForeColor="Navy" style="color: #FF3300"></asp:Label>
                                </td>
                                <td style="width: 15%">
                                    <asp:Button ID="btnNext" runat="server" CssClass="btn btn-primary" Text="Next" Width="90px" />
                                </td>
                                
                            </tr>
                            <tr>
                                <td colspan="6">
                                    <div id="gvDiv" style="width: 100%; max-height: 350px; overflow-y: scroll;">
                                        <asp:GridView ID="gvmarks" runat="server" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="SID" DataSourceID="GVCreateMarksEntry" CssClass="Grid" GridLines="Horizontal" ShowFooter="True" Visible="False" Width="95%">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr. No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSerial" runat="server"></asp:Label>
                                                        <%--<%#Container.DataItemIndex+1 %>--%>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="RegNo" HeaderText="Reg. No." SortExpression="RegNo">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ClassRollno" HeaderText="Class Roll No" SortExpression="ClassRollno">
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="SName" HeaderText="Student Name" SortExpression="SName" HeaderStyle-HorizontalAlign="Center">
                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="Max Marks">
                                                    <ItemTemplate>
                                                        <asp:label ID="lblMaxM" runat="server" CssClass="textbox"  Width="100px" />
                                                    </ItemTemplate>
                                                    <ItemStyle Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Marks">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtMarks" runat="server" CssClass="textbox" Width="120px" />
                                                        <asp:DropDownList ID="cboRemark" runat="server" CssClass="Dropdown" Visible="false">
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Grades">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgrades" runat="server" CssClass="textbox" Width="120px" />

                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                            </Columns>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <SelectedRowStyle BackColor="#4DE427" />
                                            <FooterStyle BackColor="#ccff99" Font-Bold="True" />
                                        </asp:GridView>
                                        </div>
                                    <asp:SqlDataSource ID="GVCreateMarksEntry" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT [SID], [RegNo], [ClassRollno], [SName] FROM [vw_Student]"></asp:SqlDataSource>

                                </td>
                            </tr>
                            <tr>
                                <td colspan="6">
                                    
                                    <asp:Table ID="myTable" runat="server" CssClass="myTable" GridLines="Both" Width="100%" Enabled="false" Visible="false" >
                                    </asp:Table>
                                </td>
                                
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btnProceed" runat="server" CssClass="btn btn-primary" Text="Proceed" Width="130px" />
                                </td>
                                <td>
                                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Proceed &amp; Save " Visible="False" Width="160px" />
                                </td>
                                <td><asp:Button ID="btnDelete" runat="server" CssClass="btn btn-primary" Text="Remove " TabIndex="3" Visible="False" Width="160px" /></td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                               
                            </tr>
                            <tr>
                                <td>
                                    <asp:DropDownList ID="cboRemark" runat="server" Visible="False">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:Label ID="lblInputType" runat="server" Visible="False"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblMappedSID" runat="server" Visible="False"></asp:Label>
                                    <asp:Label ID="lblActivitySubID" runat="server" Visible="False"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblGrpID" runat="server" Visible="False"></asp:Label>
                                    <asp:Label ID="lblATT" runat="server" Visible="False"></asp:Label>
                                     <asp:Label ID="lblIsHealthType" runat="server" Visible="False"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblOutputType" runat="server" Visible="False"></asp:Label>
                                </td>
                                <td>
                                    <asp:SqlDataSource ID="sdsGrade" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT [UpperValue], [LowerValue], [Grade] FROM [vw_ExamGradeMapping] Where GradeID=0"></asp:SqlDataSource>
                                </td>
                                
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:GridView ID="gvGrade" runat="server" AutoGenerateColumns="False" DataSourceID="sdsGrade" Visible="False">
                                        <Columns>
                                            <asp:BoundField DataField="UpperValue" HeaderText="UpperValue" SortExpression="UpperValue" />
                                            <asp:BoundField DataField="LowerValue" HeaderText="LowerValue" SortExpression="LowerValue" />
                                            <asp:BoundField DataField="Grade" HeaderText="Grade" SortExpression="Grade" />
                                        </Columns>
                                    </asp:GridView>
                                </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                               
                            </tr>
                        </table>
                </ContentTemplate>
                </asp:UpdatePanel>      
                

                
                          
            </div>
        </div>
        <div class="clearfix"></div>
    </div>
              
</asp:Content>


