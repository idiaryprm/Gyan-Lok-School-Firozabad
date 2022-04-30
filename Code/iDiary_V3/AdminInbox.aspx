<%@ Page Language="VB" MasterPageFile="~/AdminMaster.master" AutoEventWireup="false" Inherits="iDiary_V3.Admin_Admininbox" title="Untitled Page" Codebehind="AdminInbox.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AdminMasterContents" Runat="Server">
  
    <br />
    <table border="0" width="100%" cellpadding="2" cellspacing="2">
        <tr>
            <td  valign="top">
                <asp:Button ID="btnRead" runat="server" Text="Mark as Read" CssClass="hvr-glow" />

                &nbsp;<asp:Button ID="btnDelete" runat="server" Text="Delete Selected" CssClass="hvr-glow" />
                <br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSourceMSG" CssClass="Grid" Width="846px">
       
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:CheckBox ID="chkSelect" runat="server"  />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="RegNo" HeaderText="Reg No" SortExpression="RegNo" />
            <asp:BoundField DataField="Subject" HeaderText="Subject" SortExpression="Subject" />
            <asp:BoundField DataField="Body" HeaderText="Body" SortExpression="Body" />
            <asp:BoundField DataField="sentDate" HeaderText="Date" SortExpression="sentDate" DataFormatString="{0:dd/MM/yyyy}" />
            <asp:BoundField DataField="sentTime" HeaderText="Time" SortExpression="sentTime" DataFormatString="{0:hh:mm tt}" />
            <asp:BoundField DataField="msgID" SortExpression="msgID" ShowHeader="False" >
             <ControlStyle Width="2px" />
            <FooterStyle Width="2px" />
            <HeaderStyle Width="2px" />
            <ItemStyle Width="2px" />
            </asp:BoundField>
        </Columns>
      
    </asp:GridView>
            </td>
        </tr>
        </table>
    <asp:SqlDataSource ID="SqlDataSourceMSG" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT sentDate, sentTime, Subject, Body, isRead, SentFromSID, RegNo,msgID FROM vw_msgSentFromParent order by sentDate, sentTime"></asp:SqlDataSource>
    </asp:Content>

