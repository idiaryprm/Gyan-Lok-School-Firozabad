<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/AdminMaster.master" CodeBehind="MessageTemplateMaster.aspx.vb" Inherits="iDiary_V3.MessageTemplate" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminMasterContents" runat="server">
    <%--<br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />--%>
     <strong>Message Templates</strong>
    <table class="table">
        <tr>
            <td width="30%">Code<br />
                <br />
            </td>
            <td >
                <asp:TextBox ID="txtAPIName" runat="server" CssClass="textbox"></asp:TextBox>
               
            </td>
        </tr>

        <tr>
            <td width="30%">Details</td>
            <td >
                <asp:TextBox ID="txturl" runat="server" CssClass="textbox" TextMode="MultiLine"  Width="500px" Height="60px"></asp:TextBox>
                
            </td>
        </tr>
         <tr>
            <td width="30%"> <asp:CheckBox ID="chkDefault" runat="server" style="font-weight: 700" Text="Set As Default" Visible="False" /></td>
            <td >
                <span style="color: #CC0000">*</span><span style="color:blue">Please Donot change <*>.</span>
                
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
            <td colspan="2">
                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" />
                &nbsp;
                <asp:Button ID="btnRemove" runat="server" Visible="false" CssClass="btn btn-primary" Text="Remove" />
                &nbsp;
                <asp:Button ID="btnNew" runat="server" CssClass="btn btn-primary" Text="New" />
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <asp:SqlDataSource ID="SqlDataSourceAcademicCal" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT * FROM [MessageTemplates]">
                </asp:SqlDataSource>
            </td>
        </tr>

        <tr>
            <td width="30%" colspan="2" style="height:500px; overflow-y:scroll">
                <asp:GridView ID="gvSMSAPI" runat="server" CssClass="Grid" AutoGenerateColumns="False" DataKeyNames="MessageTemplateID" DataSourceID="SqlDataSourceAcademicCal" AllowPaging="True" PageSize="8" Width="100%" >
                  
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" HeaderStyle-Width="40px" />
                        <asp:BoundField DataField="MessageTemplateID" HeaderText="ID" SortExpression="MessageTemplateID" InsertVisible="False" HeaderStyle-Width="40px" ReadOnly="True" />
                        <asp:BoundField DataField="MessageSubject" HeaderText="Subject" SortExpression="MessageSubject" HeaderStyle-Width="150px" />
                        <asp:BoundField DataField="MessageTemplateDesc" HeaderText="Message" SortExpression="MessageTemplateDesc" HeaderStyle-Width="400px"/>
                    </Columns>
              
                   
                    <PagerStyle HorizontalAlign="Center" />
                  
               
                   
                </asp:GridView>
            </td>
        </tr>

        </table>
</asp:Content>
