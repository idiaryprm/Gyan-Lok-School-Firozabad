<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ExamAdminMasterPage.Master" CodeBehind="ExamTermConfig.aspx.vb" Inherits="iDiary_V3.ExamTermConfig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SubHeading" runat="server">
    Exam Term Configuration
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
   
    <%--<br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />--%>
      <table class="table" style="width:100%;">
        
            <tr>
               
                <td style="width:21%; height: 39px;">
                    School Name</td>
                
                <td style="vertical-align: top; height: 39px;" colspan="2">
                    <asp:DropDownList ID="cboSchoolName" runat="server" AutoPostBack="true" CssClass="Dropdown" Width="300px">
                    </asp:DropDownList>
                </td>
               
                <td style="height: 39px;">
                    <asp:Label ID="lblSchoolID" runat="server" Visible="false"></asp:Label>
                </td>
               
                <td style="width:5%; height: 39px;">
                    &nbsp;</td>
            </tr>
        
            <tr>
               
                <td style="width:21%; height: 39px;">
                    Exam Group Name</td>
                
                <td style="width: 21%; vertical-align: top; height: 39px;">
                    <strong>
                    <asp:DropDownList ID="cboExamGroup" runat="server" CssClass="Dropdown" AutoPostBack="true"  Width="150px">
                    </asp:DropDownList>
                    </strong>
                </td>
               
                <td style="width: 22%; vertical-align: top; height: 39px;">
                    Exam Subject Groups</td>
               
                <td style="height: 39px;">
                    <strong>
                    <asp:DropDownList ID="cboExamSubjectGroup" runat="server" CssClass="Dropdown" Width="150px">
                    </asp:DropDownList>
                    </strong>
                </td>
               
                <td style="width:5%; height: 39px;">
                    <asp:Button ID="btnNext" runat="server" Text="Next" Width="80px" CssClass="btn btn-primary" />
                    </td>
            </tr>
        
            <tr>
               
                <td colspan="5">
                    <asp:GridView ID="gvTerms" runat="server" AutoGenerateColumns="False" DataKeyNames="ExamTermID" CssClass="Grid" DataSourceID="SqlDataSource1" Width="80%">
                        <Columns>
                            <asp:TemplateField HeaderText="Major Term" HeaderStyle-Width="120px">
                                <ItemTemplate>
                                    <asp:CheckBox ID="cbMajorTerm" runat="server" Text='<%# Eval("ExamTermName") %>' />
                                    <asp:Label ID="lblMajorTermID" runat="server" Visible="false" Text='<%# Eval("ExamTermID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Weightage" HeaderStyle-Width="120px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtWeightage" runat="server" Width="50px" BorderStyle="Solid" BorderWidth="1" TextMode="Number"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Minor Term" HeaderStyle-Width="400px">
                                <ItemTemplate>
                                    <asp:CheckBoxList ID="cblMinorTerm" runat="server" DataTextField="ExamTermName" DataValueField="ExamTermName" Width="100%" RepeatDirection="Horizontal" RepeatColumns="3" CellPadding="5" CellSpacing="2" BorderStyle="None">
                                    </asp:CheckBoxList>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
                
            </tr>
        
            <tr>
               
                <td colspan="5">
                    <asp:Button ID="btnSaveMinor" runat="server" Text="Save" Width="80px" CssClass="btn btn-primary" />
                </td>
                
            </tr>
        
            <tr>
               
                <td colspan="5">
                    <asp:Label ID="lblSelectedIDMajor" runat="server" Visible="False"></asp:Label>
                    <asp:TextBox ID="txtExmGrpID" runat="server" Width="19px" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtGrpID" runat="server" Width="19px" Visible="False"></asp:TextBox>
                    <strong>
                        <asp:Label ID="lblHead" runat="server" Text=""></asp:Label></strong>
                      <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT * FROM [ExamTermMaster] Where isMinor=-1 order by DisplayOrder"></asp:SqlDataSource>
                </td>
                
            </tr>
        </table>
</asp:Content>
