<%@ Page Title="Student Siblings" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="StudentSiblings.aspx.vb" Inherits="iDiary_V3.StudentSiblings" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Heading" runat="server">
    Manage Siblings 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="css/jquery-ui.css" rel="stylesheet" />
    <%--<script src="js/jquery-1.10.2.js"></script>--%>
    <script src="js/jquery-ui.js"></script>
  <script>
      $(function () {
          $("#accordion").accordion({
              heightStyle: "content"
              

          });
      });
  </script>
   <%-- <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />--%>
    <div class="col_3" style="margin-top: 20px;" id="panelEnquiryNo" runat="server">
        <div class="col-md-3 widget widget1" style="width: 100%">
            <div class="r3_counter_box">
                <table class="table">
                    <tr>
                        <td><h3>Siblings for :</h3></td>
                    </tr>
                    <tr>
                        <td>Student Name</td>
                        <td>
                            <asp:TextBox ID="txtSName" runat="server" CssClass="textbox"></asp:TextBox>

                            &nbsp;
                    <asp:Button ID="btnSName" runat="server" CssClass="btn btn-sm btn-primary" Text="&gt;&gt;" />
                        </td>
                        <td>Reg/Admin/SR No</td>
                        <td>
                            <asp:TextBox ID="txtSAdmission" runat="server" CssClass="textbox"></asp:TextBox>

                            &nbsp;
                    <asp:Button ID="btnSAdmission" runat="server" CssClass="btn btn-sm btn-primary" Text="&gt;&gt;" />
                        </td>
                    </tr>
                    <tr>
                        <td>School Name</td>
                        <td colspan="2">
                            <asp:TextBox ID="txtSchoolName" runat="server" Width="300px" CssClass="textbox"></asp:TextBox>

                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>Class</td>
                        <td>
                            <asp:TextBox ID="txtClass" runat="server" CssClass="textbox"></asp:TextBox>

                        </td>
                        <td>Father Name</td>
                        <td>
                            <asp:TextBox ID="txtFName" runat="server" CssClass="textbox"></asp:TextBox>

                            &nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="txtStudentID" runat="server"
                    Visible="False" Width="28px"></asp:TextBox>

                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" AutoGenerateSelectButton="True" DataKeyNames="StudentID" CssClass="Grid"
                                CellPadding="2" DataSourceID="SqlDataSource2" Width="98%" Font-Names="Garamond" Font-Size="10pt">
                                <Columns>
                                    <asp:BoundField DataField="RegNo" HeaderText="RegNo" SortExpression="RegNo" />
                                    <asp:BoundField DataField="SName" HeaderText="SName" SortExpression="SName" />
                                    <asp:BoundField DataField="SchoolName" HeaderText="School" SortExpression="SchoolName" />
                                         <asp:BoundField DataField="ClassName" HeaderText="ClassName" SortExpression="ClassName" />
                                    <asp:BoundField DataField="SecName" HeaderText="SecName" SortExpression="SecName" />
                                    <asp:BoundField DataField="FName" HeaderText="FName" SortExpression="FName" />
                                </Columns>

                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td><h3>Add Siblings :</h3></td>
                        
                    </tr>
                    <tr>
                        <td>Student Name</td>
                        <td>
                            <asp:TextBox ID="txtSbName" runat="server" CssClass="textbox"></asp:TextBox>

                            &nbsp;
                    <asp:Button ID="btnSbName" runat="server" CssClass="btn btn-sm btn-primary" Text="&gt;&gt;" />
                        </td>
                        <td>Reg/Admin/SR No</td>
                        <td>
                            <asp:TextBox ID="txtSbAdmission" runat="server" CssClass="textbox"></asp:TextBox>

                            &nbsp;
                    <asp:Button ID="btnSbAdm" runat="server" CssClass="btn btn-sm btn-primary" Text="&gt;&gt;" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:GridView ID="gvStudent" runat="server" AutoGenerateColumns="False" AutoGenerateSelectButton="True" CssClass="Grid" DataSourceID="SqlDataSourcesiblings" ShowHeader="true" Width="95%">
                                <Columns>
                                    <asp:BoundField DataField="StudentID" HeaderText="ID" SortExpression="StudentID" />
                                    <asp:BoundField DataField="RegNo" HeaderText="RegNo" SortExpression="RegNo" />
                                    <asp:BoundField DataField="SName" HeaderText="Name" SortExpression="SName" />
                                         <asp:BoundField DataField="SchoolName" HeaderText="School" SortExpression="SchoolName" />
                                    <asp:BoundField DataField="ClassName" HeaderText="Class" SortExpression="ClassName" />
                                    <asp:BoundField DataField="SecName" HeaderText="Sec" SortExpression="SecName" />
                                    <asp:BoundField DataField="FName" HeaderText="Father" SortExpression="FName" />
                                    <asp:BoundField DataField="MobNo" HeaderText="Mobile" SortExpression="MobNo" />

                                </Columns>

                            </asp:GridView>
                            <asp:SqlDataSource ID="SqlDataSourcesiblings" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="sELECT StudentID,RegNo, SName, ClassName, SecName,FName,MobNo,SchoolName FROM vw_Student where StudentID<0"></asp:SqlDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:Label ID="lblSiblings" runat="server" Style="font-weight: 700" Text="Sibling List"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:GridView ID="gvSibling" runat="server" AutoGenerateColumns="False" CssClass="Grid" ShowHeader="true" Width="95%" AutoGenerateDeleteButton="True">
                                <Columns>
                                    <asp:BoundField DataField="StudentID" HeaderText="ID" SortExpression="StudentID" />
                                    <asp:BoundField DataField="RegNo" HeaderText="RegNo" SortExpression="RegNo" />
                                    <asp:BoundField DataField="SName" HeaderText="Name" SortExpression="SName" />
                                    <asp:BoundField DataField="SchoolName" HeaderText="School" SortExpression="SchoolName" />
                                                        <asp:BoundField DataField="ClassName" HeaderText="Class" SortExpression="ClassName" />
                                    <asp:BoundField DataField="SecName" HeaderText="Sec" SortExpression="SecName" />
                                    <asp:BoundField DataField="FName" HeaderText="Father" SortExpression="FName" />
                                    <asp:BoundField DataField="MobNo" HeaderText="Mobile" SortExpression="MobNo" />

                                </Columns>

                            </asp:GridView>
                            <asp:SqlDataSource ID="SqlDataSourcesiblingstmp" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="sELECT StudentID,RegNo, SName, ClassName, SecName,FName,MobNo,SchoolName FROM vw_Student where StudentID<0"></asp:SqlDataSource>

                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT RegNo, SName, ClassName, SecName,FName,studentid,SchoolName FROM vw_Student WHERE SID=0"></asp:SqlDataSource>

                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:Label ID="lblStatus" runat="server" Style="font-weight: 700; color: #FF0000"></asp:Label>

                        </td>
                    </tr>
                    <tr>
                        <td><asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" />&nbsp;&nbsp;
                            <asp:Button ID="btnNew" runat="server" Text="New" CssClass="btn btn-primary"  /></td>
                        
                    </tr>
                </table>
            </div>
        </div>
        <div class="clearfix"></div>
    </div>
     
       
                
       

          
                
              
    
</asp:Content>
