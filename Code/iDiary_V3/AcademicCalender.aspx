<%@ Page Language="VB" MasterPageFile="~/AdminMaster.master" AutoEventWireup="false" Inherits="iDiary_V3.Admin_AcademicCalender" title="Untitled Page" Codebehind="AcademicCalender.aspx.vb" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AdminMasterContents" Runat="Server">
    <strong>ACADEMIC CALENDER</strong>
    <table class="table">
        <tr>
            <td width="30%">Date From<br />
                <br />
            </td>
            <td >
                
                <asp:TextBox ID="txtDate" runat="server" CssClass="textbox"></asp:TextBox>
               
                <ajaxToolkit:CalendarExtender ID="txtDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDate" />
               
            </td>
        </tr>

        <tr>
            <td width="30%">Date To</td>
            <td >
                <asp:TextBox ID="txtDateTo" runat="server" CssClass="textbox"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="txtDateTo_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDateTo" />
            </td>
        </tr>

        <tr>
            <td width="30%">Description</td>
            <td >
                <asp:TextBox ID="txtDesc" runat="server" CssClass="textbox" 
                    Height="61px" TextMode="MultiLine" Width="300px" ></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td style="width: 0%">
                <asp:Label ID="lblStatus" runat="server" Font-Bold="True" ForeColor="Navy"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtID" runat="server" Height="16px" Visible="False" Width="37px"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td width="30%">&nbsp;</td>
            <td></td>
        </tr>

        <tr>
            <td width="30%">
                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" />
                &nbsp;&nbsp;
                <asp:Button ID="btnRemove" runat="server" CssClass="btn btn-primary" Text="Remove" />
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <asp:SqlDataSource ID="SqlDataSourceAcademicCal" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT * FROM [AcademicCalender]">
                </asp:SqlDataSource>
            </td>
            <td></td>
        </tr>

        <tr>
            <td width="30%" colspan="2" style="height:500px; overflow-y:scroll">
                <asp:GridView ID="gvAcademicCal" runat="server" CssClass="Grid" AutoGenerateColumns="False" DataKeyNames="ACID" DataSourceID="SqlDataSourceAcademicCal" AllowPaging="True" PageSize="8" Width="100%" >
                  
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" />
                        <asp:BoundField DataField="ACID" HeaderText="ID" SortExpression="ACID" InsertVisible="False" ReadOnly="True" />
                        <asp:BoundField DataField="ACDate" HeaderText="Date From" SortExpression="ACDate" DataFormatString="{0: dd/MM/yyyy}" />
                        <asp:BoundField DataField="ACDateTo" HeaderText="Date To" SortExpression="ACDateTo" DataFormatString="{0: dd/MM/yyyy}"/>
                        <asp:BoundField DataField="ACDetails" HeaderText="Details" SortExpression="ACDetails" />
                    </Columns>
              
                   
                    <PagerStyle HorizontalAlign="Center" />
                  
               
                   
                </asp:GridView>
            </td>
        </tr>

        </table>
</asp:Content>

