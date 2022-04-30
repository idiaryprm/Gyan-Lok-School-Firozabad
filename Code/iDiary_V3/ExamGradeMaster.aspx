<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ExamAdminMasterPage.master" CodeBehind="ExamGradeMaster.aspx.vb" Inherits="iDiary_V3.ExamGradeMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SubHeading" runat="server">
        <script type = "text/javascript">
            function Confirm() {
                var confirm_value = document.createElement("INPUT");
                confirm_value.type = "hidden";
                confirm_value.name = "confirm_value";
                if (confirm("Do you want to save data?")) {
                    confirm_value.value = "Yes";
                } else {
                    confirm_value.value = "No";
                }
                document.forms[0].appendChild(confirm_value);
            }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    
    <table class="table">
        <tr>
            <td style="width:30%">
                 <asp:ListBox ID="lstGrades" runat="server" Height="267px" Width="278px" AutoPostBack="True">
        <asp:ListItem></asp:ListItem>
    </asp:ListBox>
             </td>
            <td style="width:70%">
                <b>Enter Grade Name</b><br />
                <asp:TextBox ID="txtgradename" runat="server" CssClass="textbox"></asp:TextBox>
                <br />  
                <br />
                <b>Enter No. Of Grade Scale</b><strong><br />
                    <asp:TextBox ID="txtnoofgrade" runat="server" CssClass="textbox" TextMode="Number"></asp:TextBox>
                    <br />
                </strong>
                <br />
                 <asp:Button ID="btnProceed" runat="server" Text="Proceed" CssClass="btn btn-primary" /> &nbsp;
    <asp:Button ID="btnNew" runat="server" Text="New" CssClass="btn btn-primary" />
                <br />
                <asp:Label ID="lblGrade" runat="server" style="font-weight: 700; color: #CC0000" Text="Grade Range is 0-100."></asp:Label>
                <br />

                <asp:GridView ID="gvGrade" runat="server" AutoGenerateColumns="False" DataKeyNames="DisplayOrder" CssClass="Grid" GridLines="Horizontal" Width="100%" >
                    <Columns>
                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Sr. No."  ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblSR" runat="server" Text="" Width="40px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                         <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Lower Value"  ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:TextBox ID="txtLValue" runat="server" AutoPostBack="true" TextMode="Number"  CssClass="textbox" OnTextChanged="txtLValue_TextChanged" RowIndex='<%# Container.DisplayIndex %>' Width="80px"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                      
                         <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Upper Value"  ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:TextBox ID="txtUValue" runat="server" TextMode="Number" CssClass="textbox" Width="80px"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                         <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Grade"  ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:TextBox ID="txtGrade" runat="server" CssClass="textbox" Width="80px"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                         <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Grade Point"  ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:TextBox ID="txtGradePoint" runat="server" TextMode="Number" CssClass="textbox" Width="80px"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                    <HeaderStyle HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#4DE427" />
                    <FooterStyle BackColor="#ccff99" Font-Bold="True" />
                </asp:GridView>
                <br />
                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" OnClientClick = "Confirm()" />
                <asp:Label ID="lblGradeID" runat="server" Text="" Visible="false" ></asp:Label>
            </td>
        </tr>

    </table>
</asp:Content>
