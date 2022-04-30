<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.master" CodeBehind="StudentEnquiryConfirm.aspx.vb" Inherits="iDiary_V3.StudentEnquiryConfirm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Heading" runat="server">
    Student Admission Confirm
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <%-- <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />--%>
     <div class="col_3" style="margin-top: 20px;" id="panelEnquiryNo" runat="server">
        <div class="col-md-3 widget widget1" style="width: 100%">
            <div class="r3_counter_box">
    <table class="table">
        <tr>
            <td class="auto-style4">
                 School Name</td>
            <td class="auto-style1" colspan="2">
                <asp:DropDownList ID="cboSchoolName" runat="server" AutoPostBack="true" CssClass="Dropdown" Width="300px">
                </asp:DropDownList>
            </td>
            <td class="auto-style3">
                 Class</td>
            <td class="auto-style5">
                <asp:DropDownList ID="cboClass" runat="server" CssClass="Dropdown" AutoPostBack="True">
                </asp:DropDownList>
            </td>
            <td class="auto-style3">
    <asp:Button ID="btnNext" runat="server" Text="Next" CssClass="btn btn-primary" />
                </td>
            <td style="width:70%">
                &nbsp;</td>
        </tr>

        <tr>
            <td colspan="7">
                <div id="gvDiv" style="width: 99%; max-height: 500px; overflow-y: scroll; text-align: center;">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="enquiryID,RegNo,Address,MNAme,Gender,DOB,EMail" CssClass="Grid" GridLines="Horizontal" DataSourceID="SqlDataSource1" Width="100%" Visible="False" >
                    
                    <Columns>
                        <asp:TemplateField HeaderText="Select">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelect" runat="server" Visible="true"  />
                                                          </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="S. No.">
         <ItemTemplate>
               <%# Container.DataItemIndex + 1 %>
         </ItemTemplate>
          <ItemStyle Width="50px" />
     </asp:TemplateField>
                         <asp:BoundField DataField="EnquiryNo" HeaderText="Form No." SortExpression="EnquiryNo" />
                        <asp:BoundField DataField="Sname" HeaderText="Student Name" SortExpression="Sname" />
                        <asp:BoundField DataField="Fname" HeaderText="Father Name" SortExpression="Fname" />
                        <asp:TemplateField HeaderText="Reg No">
                            <ItemTemplate>
                                <asp:textBox ID="txtRegNo" runat="server" Width="90px" Visible="true"  />
                                                          </ItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:BoundField DataField="RegNo" HeaderText="RegNo" SortExpression="RegNo" />--%>
                        <asp:BoundField DataField="FeeBookNo" HeaderText="FeeBook No" SortExpression="FeeBookNo" />
                        <%--<asp:BoundField DataField="ClassName" HeaderText="Class Name" SortExpression="ClassName" />--%>
                        <asp:BoundField DataField="MobNo" HeaderText="Mobile No" SortExpression="MobNo" />
                        <%--<asp:BoundField DataField="address" HeaderText="Address" SortExpression="address" />--%>
                        <asp:BoundField DataField="enquiryYear" HeaderText="Admission Date" SortExpression="enquiryYear" DataFormatString="{0:dd/MM/yyyy}" />
                      

                        <%-- <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Section"  ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:DropDownlist ID="cboSec" runat="server" AutoPostBack="true" CssClass="Dropdown" OnTextChanged="cboSec_SelectedIndexChanged" RowIndex='<%# Container.DisplayIndex %>' Width="60px"></asp:DropDownlist>
                            </ItemTemplate>
                        </asp:TemplateField>
                      
                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Sub Section"  ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:DropDownlist ID="cboSubSec" runat="server" CssClass="Dropdown" RowIndex='<%# Container.DisplayIndex %>' Width="100px"></asp:DropDownlist>
                            </ItemTemplate>
                        </asp:TemplateField>--%>

                    </Columns>
                    <HeaderStyle HorizontalAlign="Center" />
                    <%--<SelectedRowStyle BackColor="#4DE427" />
                    <FooterStyle BackColor="#ccff99" Font-Bold="True" />--%>
                </asp:GridView>
                    </div>
                </td>
        </tr>

        <tr>
            <td class="auto-style4">
                <asp:Label ID="lblSection" runat="server" Text="Section"></asp:Label>
            </td>
            <td class="auto-style1">
                <asp:DropDownList ID="cboTmpSec" runat="server" CssClass="Dropdown" AutoPostBack="True">
                </asp:DropDownList>
               </td>
            <td class="auto-style2">
                <asp:Label ID="lblSubSection" runat="server" Text="Sub Section"></asp:Label>
            </td>
            <td class="auto-style3">
                <asp:DropDownList ID="cboTmpSubSec" runat="server" CssClass="Dropdown">
                </asp:DropDownList>
                </td>
            <td class="auto-style5">
                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" OnClientClick = "Confirm()" Visible="False" />
            </td>
            <td class="auto-style3">
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" 
                    SelectCommand="SELECT enquiryID,[EnquiryNo], [Sname], [Fname], [MobNo], [ClassName], [address], [enquiryYear],regno,feebookno FROM [vw_StudentEnquiry] where enquiryID=0"></asp:SqlDataSource>
               </td>
            <td style="width:70%">
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
        .auto-style2 {
            width: 14%;
        }
        .auto-style3 {
            width: 21%;
        }
        .auto-style4 {
            width: 25%;
        }
        .auto-style5 {
            width: 213px;
        }
    </style>
</asp:Content>

