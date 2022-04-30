<%--<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="TeacherNotes.aspx.vb" Inherits="iDiary_V3.TeacherNotes" %>--%>
<%@ Page Title="Teacher Notes" Language="VB" MasterPageFile="~/TeacherMasterPage.Master" AutoEventWireup="false" CodeBehind="TeacherNotes.aspx.vb" Inherits="iDiary_V3.TeacherNotes" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content2" ContentPlaceHolderID="TeacherContents" runat="server">
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <div>
    
    <div>
        <table border="0" cellpadding="2" cellspacing="2" width="100%">
        <tr>
            <td style="height: 7px;" align="right" colspan="2">

                <asp:TextBox ID="txtNoteID" runat="server" Visible="False" Height="17px" Width="54px"></asp:TextBox>

                </td>
            
        </tr>
        <tr>
            <td style="width: 86px" align="left">

                Teacher Name</td>
            <td style="width: 265px" align="left">

                <asp:DropDownList ID="DDLTeacherName" runat="server" Width="148px">
                </asp:DropDownList>

            </td>
            
        <tr>
            <td style="width: 86px; height: 26px;" align="left">

                Date</td>
            <td style="width: 265px; height: 26px;" align="left">





                
                
               
                
                <table style="width: 85%">
                    <tr>
                        <td>
                            <asp:TextBox ID="txtDate" runat="server" Width="140px"></asp:TextBox>
                
               
                
                <asp:CalendarExtender ID="txtDate_CalendarExtender" runat="server" TargetControlID="txtDate" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy">
                </asp:CalendarExtender>
                        </td>
                        <td>

                <asp:Button ID="btnShow" runat="server" Text="Show" Width="80px" />

               
                
                        </td>
                    </tr>
                </table>





                
                
               
                
            </td>
            
        </tr>
        
        <tr>
            <td style="width: 86px" align="left">

                Description</td>
            <td style="width: 265px" align="left">

                <asp:TextBox ID="txtDescription" runat="server" Height="66px" TextMode="MultiLine" Width="256px"></asp:TextBox>

            </td>
            
        </tr>
        <tr>
            <td align="center" colspan="2">

                <table style="width: 36%">
                    <tr>
                        <td>

                <asp:Button ID="btnSubmit" runat="server" Text="Save" Width="80px" />

                        </td>
                        <td>

                <asp:Button ID="btnRemove" runat="server" Text="Remove" Width="80px" />

                        </td>
                    </tr>
                </table>

            </td>
            
        </tr>
        <tr>
            <td align="center" colspan="2">

                &nbsp;</td>
            
        </tr>
        <tr>
            <td align="left" colspan="2">

            <asp:GridView ID="DBGrid" runat="server" 
            CellPadding="4" Font-Size="X-Small" GridLines="None" 
            HorizontalAlign="Center" Width="100%" AutoGenerateSelectButton="True" ForeColor="#333333" AutoGenerateColumns="False" DataKeyNames="EmpID,TNID">
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" 
                Height="30px" />
            <AlternatingRowStyle BackColor="White" VerticalAlign="Top" ForeColor="#284775" />
                <Columns>
                    <asp:BoundField DataField="TNID" HeaderText="ID" SortExpression="TNID" ReadOnly="True" />
                    <asp:BoundField DataField="EmpID" HeaderText="EmpID" SortExpression="EmpID" >
                    </asp:BoundField>
                    <asp:BoundField DataField="EmpName" HeaderText="Teacher Name" SortExpression="EmpName" />
                    <asp:BoundField DataField="TNDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Date" SortExpression="TNDate" />
                    <asp:BoundField DataField="TNDesc" HeaderText="Decs" SortExpression="TNDesc" />
                </Columns>
            <EditRowStyle Font-Size="Small" BackColor="#999999" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
        </asp:GridView>   
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT * FROM [vw_TeacherNote]"></asp:SqlDataSource>
               <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            </td>
            
        </tr>
    </table>
    </div>
    
    </div>
    
</asp:Content>
