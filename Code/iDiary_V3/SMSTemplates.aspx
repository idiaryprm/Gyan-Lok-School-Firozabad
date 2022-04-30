<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/AdminMaster.master" CodeBehind="SMSTemplates.aspx.vb" Inherits="iDiary_V3.SMSTemplates" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminMasterContents" runat="server">
           <table class="table">
        <tr>
            <td valign="top" width="40%" style="height: 25px">
                <strong>SMS Template Management</strong></td>
            <td align="left" valign="top" width="60%" style="height: 25px">
            </td>
        </tr>
        <tr>
            <td valign="top" width="40%">
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:ListBox ID="lstMasters" runat="server" AutoPostBack="True" Height="233px" CssClass="list"
                            Width="290px"></asp:ListBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td valign="top"  style="margin-left:20px;">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <b>SMS Template Code<br />
                            <asp:TextBox runat="server" ID="txtCode" CssClass="textbox"></asp:TextBox>
                            <asp:TextBox ID="txtID" runat="server" ReadOnly="True" Visible="false" 
                    Width="74px"></asp:TextBox>
                            <br />
                            <br />
                            Message</b><br />
                        <asp:TextBox ID="txtMessage" runat="server" Width="420px"
                            MaxLength="160" Height="120px" TextMode="MultiLine" CssClass="textbox"></asp:TextBox>
                        <br />
                        <br />
                        <br />
                        <asp:Label ID="lblStatus" runat="server" Font-Bold="True" ForeColor="Navy"
                            Text=""></asp:Label>
                        <br />
                        <br />
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:Button ID="btnSave" runat="server" Text="Save" class="btn btn-primary" />
                &nbsp;
                <asp:Button ID="btnNew" runat="server" Text="New" class="btn btn-primary" />
                &nbsp;
                <asp:Button ID="btnRemove" runat="server" Text="Remove" class="btn btn-primary"/>
                <br />
                <br />
                
                &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
    </table>


</asp:Content>
