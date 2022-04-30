<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="ExamActivityMapping.aspx.vb" Inherits="iDiary_V3.ExamActivityMapping" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Heading" runat="server">
    Exam Reports
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">   
    <div class="col_3" style="margin-top: 20px;" id="panelEnquiryNo" runat="server">
   
     <div class="col-md-3 widget widget1" style="width: 100%">
            <div class="r3_counter_box">

                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>

                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>

                         </ContentTemplate>
                </asp:UpdatePanel>
            <%--    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
                <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
                <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />--%>
    
                <table class="table">
        <tr>
            
                    <td class="auto-style1">Class</td>
                    <td class="auto-style4">
                        <asp:DropDownList ID="cboClass" runat="server" AutoPostBack="true" CssClass="Dropdown">
                        </asp:DropDownList>
                    </td>
                    <td class="auto-style3">Section</td>
                    <td>
                        <asp:DropDownList ID="cboSection" runat="server" CssClass="Dropdown">
                        </asp:DropDownList>
                    </td>
                    <td class="auto-style6">&nbsp;</td>
                    <td>
                        &nbsp;</td>
               
        </tr>
        <tr>
            
                    <td class="auto-style1">Subject Group</td>
                    <td class="auto-style4">
                        <b>
                <asp:DropDownList ID="cboMinorSubjectGroups" runat="server" CssClass="Dropdown">
                </asp:DropDownList>
                </b>
                    </td>
                    <td class="auto-style3">Status</td>
                    <td>
                        <b>
                <asp:DropDownList ID="cboStatus" runat="server" CssClass="Dropdown">
                </asp:DropDownList>
                </b>
                    </td>
                    <td class="auto-style6">
                <asp:Button ID="btnShow" runat="server" cssclass="btn btn-primary" Text="Show" />
                    </td>
                    <td>
                        &nbsp;</td>
               
        </tr>
      
        <tr>
            
                    <td class="auto-style1">Set Activity</td>
                    <td class="auto-style4">
                        <b>
                <asp:DropDownList ID="cboActivity" runat="server" CssClass="Dropdown">
                </asp:DropDownList>
                </b>
                    </td>
                    <td class="auto-style3">for </td>
                    <td>
                        <b>
                <asp:DropDownList ID="cboAct" runat="server" CssClass="Dropdown">
                    <asp:ListItem>Activity-1</asp:ListItem>
                    <asp:ListItem>Activity-2</asp:ListItem>
                </asp:DropDownList>
                &nbsp;&nbsp;&nbsp;
                </b>
                    </td>
                    <td class="auto-style6">
                        <b>
                <asp:Button ID="btnSet" runat="server" cssclass="btn btn-primary" Text="&gt;&gt;" />
                </b>
                    </td>
                    <td>
                        &nbsp;</td>
               
        </tr>
      
        <tr>
            <td class="auto-style1" colspan="2">
                <asp:Label ID="lblmsg" runat="server" Font-Bold="true" Font-Size="18px" ForeColor="Red" style="font-size: small"></asp:Label>
                <asp:Label ID="lblActivityGrpID" runat="server" Visible="False"></asp:Label>
            </td>
            <td class="auto-style3">
                        &nbsp;</td>
            <td>
                &nbsp;</td>
            
            <td class="auto-style6">
                &nbsp;</td>
            
            <td class="auto-style6">
                &nbsp;</td>
            
        </tr>
          <tr>
            
                    <td class="auto-style1" colspan="5">
                         <div id="gvDiv" style="width: 100%;max-height: 350px; overflow-y: scroll;">
                                        <asp:GridView ID="gvmarks" runat="server" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="SID" DataSourceID="GVCreateMarksEntry" CssClass="Grid" GridLines="Horizontal" ShowFooter="True" Visible="False" Width="99%">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr. No.">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
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
                                              <asp:TemplateField HeaderText="Activity-1">
                                                    <ItemTemplate>
                                                       <asp:DropDownList ID="cboActivityOne" runat="server" Width="220px" CssClass="Dropdown" Visible="true">
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Activity-2">
                                                    <ItemTemplate>
                                                       <asp:DropDownList ID="cboActivityTwo" runat="server" Width="220px" CssClass="Dropdown" Visible="true">
                                                        </asp:DropDownList>
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

                         <br />
                         <asp:Button ID="btnSave" runat="server" cssclass="btn btn-primary" Text="Save" />

                    </td>
                    <td>
                        &nbsp;</td>
               
        </tr>
    </table>
     

            </div>
        </div>
        <div class="clearfix"></div>
    </div>

   
</asp:Content>
<asp:Content ID="Content3" runat="server" contentplaceholderid="head">
    <style type="text/css">
    .auto-style1 {
    }
    .auto-style3 {
        width: 152px;
    }
    .auto-style4 {
        width: 20%;
    }
    .auto-style6 {
        }
</style>
</asp:Content>

