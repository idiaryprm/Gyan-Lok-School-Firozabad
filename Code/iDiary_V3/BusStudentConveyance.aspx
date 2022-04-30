<%@ Page Title="Student Transportation" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="BusStudentConveyance.aspx.vb" Inherits="iDiary_V3.BusStudentConveyence" %>
<asp:Content ID="Content2" ContentPlaceHolderID="Heading" runat="server">
    Student Transportation
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />--%>
     <div class="col_3" style="margin-top: 20px;" id="panelEnquiryNo" runat="server">
        <div class="col-md-3 widget widget1" style="width: 100%">
            <div class="r3_counter_box">
    <table class="table">
         <tr>
             <td style="height: 22px;" colspan="7">

                    <asp:GridView ID="gvSearch" runat="server" AutoGenerateColumns="False" DataKeyNames="SID" Width="90%" DataSourceID="SqlDataSourceSearch" CssClass="Grid">
                
                        <Columns>
                            <asp:CommandField ShowSelectButton="True" />
                            <asp:BoundField DataField="SID" HeaderText="SID" ReadOnly="True" SortExpression="SID" />
                            <asp:BoundField DataField="RegNo" HeaderText="RegNo" SortExpression="RegNo" />
                            <asp:BoundField DataField="SName" HeaderText="SName" SortExpression="SName" />
                            <asp:BoundField DataField="ClassName" HeaderText="ClassName" SortExpression="ClassName" />
                            <asp:BoundField DataField="SecName" HeaderText="SecName" SortExpression="SecName" />
                            <asp:BoundField DataField="FName" HeaderText="FName" SortExpression="FName" />
                        </Columns>
                       
                    </asp:GridView>
             </td>

         </tr>
         <tr>
             <td style="width:15%; height: 22px;">

                 Admission No.</td>
             <td style="width:15%; height: 22px;">

                <asp:TextBox ID="txtRegNo" runat="server" CssClass="textbox"></asp:TextBox>

             </td>
             <td style="width:15%; height: 22px;">
                 <asp:Button ID="btnNextAdminNo" runat="server" CssClass="btn btn-sm btn-primary" Text="&gt;&gt;" />
         

             </td>
             <td style="width:15%; height: 22px;">

                 Student Name</td>
             <td style="width:15%; height: 22px;">

                <asp:TextBox ID="txtName" runat="server" CssClass="textbox"></asp:TextBox>

                 </td>

             <td style="width:15%; height: 22px;">

                 <asp:Button ID="btnNextStudent" runat="server" CssClass="btn btn-sm btn-primary" Text="&gt;&gt;" />

             </td>

             <td style="width:10%; height: 22px;">

                <asp:TextBox ID="txtSID" runat="server" Width="40px" BorderWidth="1px" 
                    BorderColor="Black" Visible="False"></asp:TextBox>

             </td>

         </tr>
         <tr>
             <td style="width:15%; height: 20px;">

                 Class-Sec</td>
             <td style="width:15%; height: 20px;">

                <asp:TextBox ID="txtClass" runat="server" CssClass="textbox" Width="90px" ReadOnly="True"></asp:TextBox>  <asp:TextBox ID="txtSection" runat="server" CssClass="textbox" Width="50px" ReadOnly="True"></asp:TextBox>

             </td>
             <td style="width:15%; height: 20px;">

                 &nbsp;</td>
             <td style="width:15%; height: 20px;">

                 Bus Service

                 </td>
             <td style="width:15%; height: 20px;">

                 <asp:DropDownList ID="cboBusService" runat="server"  CssClass="Dropdown" AutoPostBack="True">
                     <asp:ListItem></asp:ListItem>
                     <asp:ListItem>No</asp:ListItem>
                     <asp:ListItem>Yes</asp:ListItem>
                 </asp:DropDownList>

                 </td>

             <td style="width:15%; height: 20px;">

                

                 &nbsp;</td>

             <td style="width:10%; height: 20px;">

                 &nbsp;</td>

         </tr>
         <tr>
             <td style="width:15%">

                 Term No<td style="width:15%">

               <asp:DropDownList ID="cboTermNo" runat="server"  CssClass="Dropdown" 
                    AutoPostBack="True">
                </asp:DropDownList>

             </td>
             <td style="width:15%">

                 &nbsp;

                                 

                 <asp:Label ID="lblTerm" runat="server" Font-Bold="True"></asp:Label>

                

                 </td>
             <td style="width:15%">

                 <asp:Label ID="lblMode" runat="server" Text="Mode"></asp:Label>

             </td>
             <td style="width:15%">

                 <asp:DropDownList ID="cboModeConvey" runat="server"  CssClass="Dropdown">
                     <asp:ListItem></asp:ListItem>
                     <asp:ListItem>No</asp:ListItem>
                     <asp:ListItem>Yes</asp:ListItem>
                 </asp:DropDownList>

             </td>

             <td style="width:15%">

                

                 &nbsp;</td>

             <td style="width:10%">

                                 <asp:Button ID="btnSavetransport" runat="server" BorderColor="Black" BorderWidth="1px" Text="Save" Width="60px" />

             </td>

         </tr>
         <tr>
             <td colspan="4" >

                <asp:Label ID="lblStatus" runat="server" Font-Bold="True" ForeColor="Navy" Text="" style="color: #FF3300"></asp:Label>
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
                     <table border="0" cellpadding="0" cellspacing="0" >
                         <tr>
                             <td style="height: 22px;">Location</td>
                             <td class="auto-style1">
                                 <asp:DropDownList ID="cboBusLocation" runat="server" AutoPostBack="true"  CssClass="Dropdown">
                                     <asp:ListItem></asp:ListItem>
                                     <asp:ListItem>No</asp:ListItem>
                                     <asp:ListItem>Yes</asp:ListItem>
                                 </asp:DropDownList>
                             </td>
                             <td class="auto-style2">
                                 <asp:Label ID="lblConveyance" runat="server" Text="Conveyance Head" ></asp:Label>
                             </td>
                             <td style="height: 22px;">
                                 <asp:DropDownList ID="cboConvaeyanceHead" runat="server" CssClass="Dropdown" AutoPostBack="True">
                                     <asp:ListItem></asp:ListItem>
                                     <asp:ListItem>No</asp:ListItem>
                                     <asp:ListItem>Yes</asp:ListItem>
                                 </asp:DropDownList>
                             </td>
                             <td>Amount</td>
                             <td>
                                 <asp:TextBox ID="txtAmount" runat="server" CssClass="textbox" ReadOnly="True" Width="90px"></asp:TextBox>
                             </td>
                             <td style="height: 22px;">
                                 <asp:Label ID="lblBus" runat="server" Text="Bus"></asp:Label>
                                 &nbsp; </td>
                             <td style="height: 22px;">
                                 <asp:DropDownList ID="cboBus" runat="server" CssClass="Dropdown" >
                                 </asp:DropDownList>
                             </td>
                         </tr>
                         <tr>
                             <td colspan="7">
                                 <asp:SqlDataSource ID="SqlDataSourceSearch" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT [SID], [RegNo], [SName], [ClassName], [SecName], [FName] FROM [vw_Student] WHERE [SID] = 0">
                                     
                                 </asp:SqlDataSource>
                             </td>
                         </tr>
                         </table>

                 </asp:Panel>
                <br />
             <asp:Button ID="btnSave" runat="server" BorderColor="Black" BorderWidth="1px" Text="Save"  class="btn btn-primary"/>
                                 &nbsp;<asp:Button ID="btnNew" runat="server" Text="New"  class="btn btn-primary" />
                                 &nbsp;<asp:Button ID="btnDelete" runat="server" Text="Delete"  class="btn btn-primary" Visible="False" />

                   </div>
        </div>
        <div class="clearfix"></div>
    </div>
</asp:Content>


