<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/BusMaster.master" CodeBehind="BusConveyanceMaster.aspx.vb" Inherits="iDiary_V3.StudentConveyence" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BusMasterContents" runat="server">
     
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
        <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
        <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
        <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    
    <table class="table">
         <tr>
             <td style="width:15%; height: 22px;">

                 Admission No.</td>
             <td style="width:15%; height: 22px;">

                <asp:TextBox ID="txtRegNo" runat="server" Width="110px" BorderWidth="1px" 
                    BorderColor="Black"></asp:TextBox>

             </td>
             <td style="width:15%; height: 22px;">

                 <asp:ImageButton ID="btnNext" runat="server" 
                    ImageUrl="~/images/next.png" ImageAlign="AbsMiddle" Height="19px" />

             </td>
             <td style="width:15%; height: 22px;">

                 &nbsp;</td>
             <td style="width:15%; height: 22px;">

                 </td>

             <td style="width:15%; height: 22px;">

                 &nbsp;</td>

             <td style="width:10%; height: 22px;">

                <asp:TextBox ID="txtSID" runat="server" Width="40px" BorderWidth="1px" 
                    BorderColor="Black" Visible="False"></asp:TextBox>

             </td>

         </tr>
         <tr>
             <td style="width:15%">

                 Student Name</td>
             <td style="width:15%">

                <asp:TextBox ID="txtName" runat="server" Width="110px" BorderWidth="1px" 
                    BorderColor="Black"></asp:TextBox>

             </td>
             <td style="width:15%">

                 <asp:ImageButton ID="btnNext0" runat="server" 
                    ImageUrl="~/images/next.png" ImageAlign="AbsMiddle" />

             </td>
             <td style="width:15%; height: 22px;">

                 &nbsp;</td>
             <td style="width:15%">

                 &nbsp;</td>

             <td style="width:15%">

                 &nbsp;</td>

             <td style="width:10%">

                 &nbsp;</td>

         </tr>
         <tr>
             <td colspan="7"  valign="top">

                    <asp:GridView ID="gvSearch" runat="server" AutoGenerateColumns="False" DataKeyNames="SID" DataSourceID="SqlDataSourceSearch" Width="449px" CellPadding="4" ForeColor="#333333" GridLines="None">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:CommandField ShowSelectButton="True" />
                            <asp:BoundField DataField="SID" HeaderText="SID" ReadOnly="True" SortExpression="SID" />
                            <asp:BoundField DataField="RegNo" HeaderText="RegNo" SortExpression="RegNo" />
                            <asp:BoundField DataField="SName" HeaderText="SName" SortExpression="SName" />
                            <asp:BoundField DataField="ClassName" HeaderText="ClassName" SortExpression="ClassName" />
                            <asp:BoundField DataField="SecName" HeaderText="SecName" SortExpression="SecName" />
                            <asp:BoundField DataField="FName" HeaderText="FName" SortExpression="FName" />
                        </Columns>
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>
             </td>

         </tr>
         <tr>
             <td style="width:15%; height: 20px;">

                 Class</td>
             <td style="width:15%; height: 20px;">

                <asp:TextBox ID="txtClass" runat="server" Width="110px" BorderWidth="1px" 
                    BorderColor="Black" ReadOnly="True"></asp:TextBox>

             </td>
             <td style="width:15%; height: 20px;">

                 &nbsp;

                 Section</td>
             <td style="width:15%; height: 20px;">

                <asp:TextBox ID="txtSection" runat="server" Width="110px" BorderWidth="1px" 
                    BorderColor="Black" ReadOnly="True"></asp:TextBox>

             </td>
             <td style="width:15%; height: 20px;">

                 Term No</td>

             <td style="width:15%; height: 20px;">

                <asp:DropDownList ID="cboTermNo" runat="server" Width="130px" 
                    AutoPostBack="True" Height="22px">
                </asp:DropDownList>

                 </td>

             <td style="width:10%; height: 20px;">

                 <asp:Label ID="lblTerm" runat="server" Font-Bold="True"></asp:Label>

                 </td>

         </tr>
         <tr>
             <td style="width:15%">

                 &nbsp;Bus Service
             <td style="width:15%">

                 <asp:DropDownList ID="cboBusService" runat="server" Width="110px" AutoPostBack="True">
                     <asp:ListItem></asp:ListItem>
                     <asp:ListItem>No</asp:ListItem>
                     <asp:ListItem>Yes</asp:ListItem>
                 </asp:DropDownList>

             </td>
             <td style="width:15%">

                 &nbsp;

                 <asp:Label ID="lblMode" runat="server" Text="Mode"></asp:Label>

             </td>
             <td style="width:15%">

                 <asp:DropDownList ID="cboModeConvey" runat="server" Width="110px">
                     <asp:ListItem></asp:ListItem>
                     <asp:ListItem>No</asp:ListItem>
                     <asp:ListItem>Yes</asp:ListItem>
                 </asp:DropDownList>

             </td>
             <td style="width:15%">

                                 &nbsp;</td>

             <td style="width:15%">

                                 <asp:Button ID="btnSavetransport" runat="server" BorderColor="Black" BorderWidth="1px" Text="Save" Width="60px" />

             </td>

             <td style="width:10%">

                 &nbsp;</td>

         </tr>
         <tr>
             <td colspan="4" >

                <asp:Label ID="lblStatus" runat="server" Font-Bold="True" ForeColor="Navy" Text=""></asp:Label>
                </td>
             <td style="width:15%">

                 &nbsp;</td>

             <td style="width:15%">

                 &nbsp;</td>

             <td style="width:10%">

                 &nbsp;</td>

         </tr>
         </table>

                 <asp:Panel ID="PanelBus" runat="server">
                     <table border="0" cellpadding="0" cellspacing="0" width="100%" >
                         <tr>
                             <td style="height: 22px;">Location/Amount</td>
                             <td style="height: 22px;">
                                 <asp:DropDownList ID="cboBusLocation" runat="server" Width="234px">
                                     <asp:ListItem></asp:ListItem>
                                     <asp:ListItem>No</asp:ListItem>
                                     <asp:ListItem>Yes</asp:ListItem>
                                 </asp:DropDownList>
                             </td>
                             <td style="height: 22px;">
                                 <asp:Label ID="lblRoute" runat="server" Text="Route" Visible="False"></asp:Label>
                             </td>
                             <td style="height: 22px;">
                                 <asp:DropDownList ID="cboBusRoute" runat="server" Width="110px" AutoPostBack="True" Visible="False">
                                     <asp:ListItem></asp:ListItem>
                                     <asp:ListItem>No</asp:ListItem>
                                     <asp:ListItem>Yes</asp:ListItem>
                                 </asp:DropDownList>
                             </td>
                             <td style="height: 22px;">
                                 <asp:Label ID="lblBus" runat="server" Text="Bus" Visible="False"></asp:Label>
                                 &nbsp; </td>
                             <td style="height: 22px;">
                                 <asp:DropDownList ID="cboBus" runat="server" Width="110px" Visible="False">
                                 </asp:DropDownList>
                             </td>
                             <td style="height: 22px;"></td>
                         </tr>
                         <tr>
                             <td style="height: 22px; ">&nbsp;</td>
                             <td style="height: 22px;">
                                 <asp:TextBox ID="txtsiblingName" runat="server" BorderColor="Black" BorderWidth="1px" Visible="False" Width="110px"></asp:TextBox>
                             </td>
                             <td style="height: 22px;">&nbsp;</td>
                             <td style="height: 22px;">
                                 <asp:DropDownList ID="cboSiblingClass" runat="server" Visible="False" Width="110px">
                                     <asp:ListItem></asp:ListItem>
                                     <asp:ListItem>No</asp:ListItem>
                                     <asp:ListItem>Yes</asp:ListItem>
                                 </asp:DropDownList>
                             </td>
                             <td style="height: 22px;">&nbsp;</td>
                             <td style="height: 22px;">
                                 <asp:DropDownList ID="cboSiblingSection" runat="server" Visible="False" Width="110px">
                                     <asp:ListItem></asp:ListItem>
                                     <asp:ListItem>No</asp:ListItem>
                                     <asp:ListItem>Yes</asp:ListItem>
                                 </asp:DropDownList>
                             </td>
                             <td style="height: 22px;">
                                 <asp:Button ID="btnAddSibling" runat="server" BorderColor="Black" BorderWidth="1px" Text="&gt;&gt;" Visible="False" Width="34px" />
                             </td>
                         </tr>
                         <tr>
                             <td colspan="7" style="height: 22px;">
                                 <br />
                                 <asp:GridView ID="gvSiblings" runat="server" AutoGenerateColumns="False" AutoGenerateDeleteButton="True" CellPadding="4" DataKeyNames="SibID" DataSourceID="SqlDataSource1" ForeColor="#333333" GridLines="None" Width="369px">
                                     <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                     <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                     <Columns>
                                         <asp:BoundField DataField="SibID" HeaderText="SibID" InsertVisible="False" ReadOnly="True" SortExpression="SibID" Visible="False" />
                                         <asp:BoundField DataField="SibName" HeaderText="SibName" SortExpression="SibName" />
                                         <asp:BoundField DataField="SibClass" HeaderText="SibClass" SortExpression="SibClass" />
                                         <asp:BoundField DataField="SibSec" HeaderText="SibSec" SortExpression="SibSec" />
                                     </Columns>
                                     <EditRowStyle BackColor="#999999" />
                                     <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                     <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                     <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                     <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                     <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                     <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                     <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                     <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                 </asp:GridView>
                             </td>
                         </tr>
                         <tr>
                             <td colspan="7">
                                 <asp:SqlDataSource ID="SqlDataSourceSearch" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT [SID], [RegNo], [SName], [ClassName], [SecName], [FName] FROM [vw_Student] WHERE (([ASID] = @ASID) AND ([SName] LIKE '%' + @SName + '%'))">
                                     <SelectParameters>
                                         <asp:SessionParameter Name="ASID" SessionField="ASID" Type="Int32" />
                                         <asp:ControlParameter ControlID="txtName" Name="SName" PropertyName="Text" Type="String" />
                                     </SelectParameters>
                                 </asp:SqlDataSource>
                                 <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" DeleteCommand="DELETE FROM [TempSiblings] WHERE [SibID] = @SibID" InsertCommand="INSERT INTO [TempSiblings] ([SibName], [SibClass], [SibSec]) VALUES (@SibName, @SibClass, @SibSec)" SelectCommand="SELECT [SibName], [SibClass], [SibSec], [SibID] FROM [TempSiblings]" UpdateCommand="UPDATE [TempSiblings] SET [SibName] = @SibName, [SibClass] = @SibClass, [SibSec] = @SibSec WHERE [SibID] = @SibID">
                                     <DeleteParameters>
                                         <asp:Parameter Name="SibID" Type="Int32" />
                                     </DeleteParameters>
                                     <InsertParameters>
                                         <asp:Parameter Name="SibName" Type="String" />
                                         <asp:Parameter Name="SibClass" Type="String" />
                                         <asp:Parameter Name="SibSec" Type="String" />
                                     </InsertParameters>
                                     <UpdateParameters>
                                         <asp:Parameter Name="SibName" Type="String" />
                                         <asp:Parameter Name="SibClass" Type="String" />
                                         <asp:Parameter Name="SibSec" Type="String" />
                                         <asp:Parameter Name="SibID" Type="Int32" />
                                     </UpdateParameters>
                                 </asp:SqlDataSource>
                             </td>
                         </tr>
                         </table>

                 </asp:Panel>
             <asp:Button ID="btnSave" runat="server" BorderColor="Black" BorderWidth="1px" Text="Save" Width="60px" />
                                 &nbsp;<asp:Button ID="btnNew" runat="server" BorderColor="Black" BorderWidth="1px" Text="New" Width="60px" />
                                 &nbsp;<asp:Button ID="btnDelete" runat="server" BorderColor="Black" BorderWidth="1px" Text="Delete" Width="60px" Visible="False" />
</asp:Content>
