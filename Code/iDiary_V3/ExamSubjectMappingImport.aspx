<%@ Page Language="VB" MasterPageFile="~/ExamAdminMasterPage.master" AutoEventWireup="false" Inherits="iDiary_V3.ExamSubjectMappingImport" title="Untitled Page" Codebehind="ExamSubjectMappingImport.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SubHeading" Runat="Server">
    Copy subject Mapping to Another Class-Section
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" Runat="Server">
    
    <table class="table" style="width:100%">
       
        <tr>
            
            <td style="font-size:14px;" colspan="2">

                <strong>Select Source </strong></td>
            <td style="width:15%">

            </td>
            <td style="width:15%">

            </td>
             <td style="width:15%">

            </td>
            <td style="width:25%">

            </td>
        </tr>
          <tr>
            
            <td style="font-size:14px;" >

                <strong>School Name</strong></td>
            <td colspan="3">
                <asp:DropDownList ID="cboSchoolName" runat="server" CssClass="Dropdown" Width="300px" AutoPostBack="true" ></asp:DropDownList>
                <asp:Label ID="lblSchoolID" runat="server" Visible="False"></asp:Label>
            </td>
           
            <td style="width:25%">

            </td>
        </tr>
         <tr>
           
            <td style="width:15%">

            <b>Class
            Group</b></td>
            <td style="width:15%">

            <asp:DropDownList ID="cboExamGroup" runat="server" AutoPostBack="True" CssClass="Dropdown">
            </asp:DropDownList>

            </td>
            <td style="width:15%">

            <strong>Class</strong></td>
            <td style="width:15%">

            <b>
                <asp:DropDownList ID="cboClassS" runat="server" 
                    AutoPostBack="True" CssClass="Dropdown">
                </asp:DropDownList>
            </b>
            
            </td>
             <td colspan="2" rowspan="12">

                <asp:ListBox ID="lstSelected" runat="server" Height="260px" Width="100%">
                </asp:ListBox>
            
            </td>
        </tr>
         <tr>
            <td style="width:15%">

            <strong>Section</strong></td>
            <td style="width:15%">

            <b><asp:DropDownList ID="cboSectionS" runat="server" 
                    AutoPostBack="True" CssClass="Dropdown">
                </asp:DropDownList>
            </b>
            
                   </td>
            <td style="width:15%">

            <strong>Sub-Section</strong></td>
            <td style="width:15%">

            <b><asp:DropDownList ID="cboSubSectionS" runat="server" 
                    AutoPostBack="True" CssClass="Dropdown">
                </asp:DropDownList>
            </b>
            
                   </td>
        </tr>
         <tr>
            <td style="width:15%">

            <b>
            <asp:Button ID="btnShow" runat="server" CssClass="btn btn-primary" Text="Show" />
            </b>
            
                      </td>
            <td colspan="3">

            <b>
                <asp:Label ID="lblStatus" runat="server" Font-Bold="True" ForeColor="Navy" style="color: #FF3300"></asp:Label>
            </b>
            
                   </td>
        </tr>
         <tr>
            <td style="font-size:14px;" colspan="4">

                      &nbsp;&nbsp;&nbsp;

            <asp:CheckBoxList ID="cblClasses" runat="server" DataSourceID="sdsCSSID" DataTextField="CSSName" DataValueField="CSSID" CellPadding="10" CellSpacing="2" RepeatColumns="3" RepeatDirection="Horizontal" Width="80%">
            </asp:CheckBoxList>

                      <br />
                      <asp:CheckBox ID="cbAll" runat="server" AutoPostBack="True" Font-Bold="True" Font-Size="Medium" ForeColor="#660033" Text="Check/Uncheck All" />

                      </td>
        </tr>
         <tr>
            <td style="width:15%">

            <b>
            <asp:Button ID="btnCopy" runat="server" CssClass="btn btn-primary" width="150px" Text="Import Subjects" />
            </b>
            
                      </td>
            <td colspan="3">

            <asp:SqlDataSource ID="sdsCSSID" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT [CSSName], [CSSID] FROM [vw_ClassStudent] Where cssid=0"></asp:SqlDataSource>

                      </td>
        </tr>
         <tr>
            <td style="width:15%">

            <b>
            <asp:Label ID="lblGrpID" runat="server" Visible="False"></asp:Label>
            </b>
            
                   </td>
            <td style="width:15%">

                      </td>
            <td style="width:15%">

                      </td>
            <td style="width:15%">

                      </td>
        </tr>
         <tr>
            <td style="width:15%">

                &nbsp;</td>
            <td style="width:15%">

                      </td>
            <td style="width:15%">

                      </td>
            <td style="width:15%">

                      </td>
        </tr>
         <tr>
            <td style="width:15%">

                      </td>
            <td style="width:15%">

                      </td>
            <td style="width:15%">

                      </td>
            <td style="width:15%">

                      </td>
        </tr>
         <tr>
            <td style="width:15%">

                      </td>
            <td style="width:15%">

                      </td>
            <td style="width:15%">

                      </td>
            <td style="width:15%">

                      </td>
        </tr>
            <tr>
            <td style="width:15%">

                   </td>
            <td style="width:15%">

                   </td>
            <td style="width:15%">

                   </td>
            <td style="width:15%">

                   </td>
        </tr>
            <tr>
            <td style="width:15%">

                   </td>
            <td style="width:15%">

                   </td>
            <td style="width:15%">

                   </td>
            <td style="width:15%">

                   </td>
        </tr>
            <tr>
            <td style="width:15%">

                      </td>
            <td style="width:15%">

                      </td>
            <td style="width:15%">

                      </td>
            <td style="width:15%">

                      </td>
        </tr>
           <tr>
            <td style="width:15%">

                      </td>
            <td style="width:15%">

                      </td>
            <td style="width:15%">

                      </td>
            <td style="width:15%">

                      </td>
        </tr>
           <tr>
            <td style="width:15%">

                      </td>
            <td style="width:15%">

                      </td>
            <td style="width:15%">

                      </td>
            <td style="width:15%">

                      </td>
        </tr>
         <tr>
            <td style="width:15%">

                   </td>
            <td style="width:15%">

                   </td>
            <td style="width:15%">

                   </td>
            <td style="width:15%">

                   </td>
            <td style="width:15%">

                   </td>
             <td style="width:25%">

                    </td>
        </tr>
    </table>
                
</asp:Content>

