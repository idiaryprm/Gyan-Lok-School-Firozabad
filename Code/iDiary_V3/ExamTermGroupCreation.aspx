<%@ Page Language="VB" MasterPageFile="~/ExamAdminMasterPage.master" AutoEventWireup="false" Inherits="iDiary_V3.ExamTermGroupCreation" title="Untitled Page" Codebehind="ExamTermGroupCreation.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SubHeading" Runat="Server">
    Subject Mapping
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMain" Runat="Server">
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
       <br /><br /><br /><br />
    <table class="table" >
        <tr>
            <td style="width: 97px">

                Exam Group</td>
            <td>

                 <asp:DropDownList ID="cboExamGroup" runat="server" AutoPostBack="True" CssClass="Dropdown" Width="150px">
                                    </asp:DropDownList>

            </td>
            <td>

                Class</td>
            <td>

                <asp:DropDownList ID="cboClass" runat="server" 
                    AutoPostBack="True" CssClass="Dropdown" Width="150px">
                </asp:DropDownList>

            </td>
            <td>

                Section</td>
             <td>

                <b>
                <asp:DropDownList ID="cboSection" runat="server" 
                    AutoPostBack="True" CssClass="Dropdown" Width="100px">
                </asp:DropDownList>
                </b>

            </td>
        </tr>

        <tr>
            <td style="width: 97px">

                Sub-Section</td>
            <td>

                <b>
                <asp:DropDownList ID="cboSubSection" runat="server" CssClass="Dropdown" Width="150px">
                </asp:DropDownList>
                </b>

            </td>
            <td>

                Subject Group</td>
            <td>

                <b>
                <asp:DropDownList ID="cboSubjectGroup" runat="server" CssClass="Dropdown" Width="150px">
                </asp:DropDownList>
                </b>

            </td>
            <td>

                &nbsp;</td>
             <td>

                <asp:Button ID="btnNext" runat="server" CssClass="btn btn-primary" Text="Next" />
            </td>
        </tr>

     

        <tr>
            <td colspan="6">

                <asp:Label ID="lblStatus" runat="server" Font-Bold="True" ForeColor="Navy" style="color: #FF3300"></asp:Label>
            </td>
        </tr>

        <tr>
            <td colspan="6">

                <asp:GridView ID="GvMyTable" runat="server" AutoGenerateColumns="False" CssClass="Grid" DataKeyNames="SubjectID" DataSourceID="sdsgvMytable" GridLines="Horizontal" Visible="true" Width="100%">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="true" OnCheckedChanged="chkSelect_CheckedChanged" RowIndex='<%# Container.DisplayIndex %>' Width="15px" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="SubjectName" HeaderStyle-HorizontalAlign="Center" HeaderText="Subject Name" HtmlEncode="False" SortExpression="SubjectName">
                            <ItemStyle HorizontalAlign="left" Width="25%" />
                        </asp:BoundField>
                           <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Display Type"  ItemStyle-HorizontalAlign="Center" >
                               <ItemTemplate>
                                   <asp:DropDownList ID="cboDisplayType" runat="server" CssClass="Dropdown" Width="70px" Enabled="false" >
                                       <asp:ListItem>Marks</asp:ListItem>
                                       <asp:ListItem>Grade</asp:ListItem>
                                       <asp:ListItem>Remarks</asp:ListItem>
                                   </asp:DropDownList>
                               </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Entry Type"  ItemStyle-HorizontalAlign="Center" >
                               <ItemTemplate>
                                   <asp:DropDownList ID="cboEntryType" runat="server" CssClass="Dropdown" Width="70px" Enabled="false" >
                                       <asp:ListItem>Marks</asp:ListItem>
                                       <asp:ListItem>Grade</asp:ListItem>
                                       <asp:ListItem>Remarks</asp:ListItem>
                                   </asp:DropDownList>
                               </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="In Exam" ItemStyle-HorizontalAlign="Center"  >
                            <ItemTemplate>
                                <asp:CheckBox ID="chkApplicableExam" runat="server" Enabled="false"  />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="In Time Table" ItemStyle-HorizontalAlign="Center" >
                            <ItemTemplate>
                                <asp:CheckBox ID="chkApplicableTT" runat="server" Enabled="false"    />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Time Table Wt."  ItemStyle-HorizontalAlign="Center" >
                            <ItemTemplate>
                                <asp:DropDownList ID="cboTTWeightage" runat="server" CssClass="Dropdown" Width="60px" Enabled="false" >
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>9</asp:ListItem>
                                    <asp:ListItem>8</asp:ListItem>
                                    <asp:ListItem>7</asp:ListItem>
                                    <asp:ListItem>6</asp:ListItem>
                                    <asp:ListItem>5</asp:ListItem>
                                    <asp:ListItem>4</asp:ListItem>
                                    <asp:ListItem>3</asp:ListItem>
                                    <asp:ListItem>2</asp:ListItem>
                                    <asp:ListItem>1</asp:ListItem>
                                    <asp:ListItem>0</asp:ListItem>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                    <HeaderStyle HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#4DE427" />
                    <FooterStyle BackColor="#ccff99" Font-Bold="True" />
                </asp:GridView>
                </td>
        </tr>

        <tr>
            <td style="width: 97px">

                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" />
            </td>
            <td>

                &nbsp;</td>
            <td>

                &nbsp;</td>
            <td>

                &nbsp;</td>
            <td>

                &nbsp;</td>
             <td>

                 &nbsp;</td>
        </tr>

        <tr>
            <td colspan="3">

                <asp:SqlDataSource ID="sdsgvMytable" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="Select SubjectName,SubjectID From vw_ExamSubjects where subjectid=0 order by SubjectName"></asp:SqlDataSource>

                <asp:Label ID="lblGrpID" runat="server" Visible="False"></asp:Label>
            </td>
            <td>

                &nbsp;</td>
            <td>

                &nbsp;</td>
             <td>

                 &nbsp;</td>
        </tr>

    </table>
</asp:Content>



