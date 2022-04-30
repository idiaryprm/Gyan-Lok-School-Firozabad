<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="testPAGE.aspx.vb" Inherits="iDiary_V3.testPAGE" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
<table width="600" border="0" align="center" cellpadding="5" cellspacing="1" bgcolor="#cccccc">
<tr> 
<td width="100" align="right" bgcolor="#eeeeee" class="header1"> Renaming a File:</td>
<td align="center" bgcolor="#FFFFFF">
Old File File:<asp:TextBox ID="txtOldFile" runat="server"></asp:TextBox> <br />
New File Name:<asp:TextBox ID="txtNewFile" runat="server" Width="126px"></asp:TextBox><br />
<asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" /><br />&nbsp;
<asp:label ID="lblStatus" runat="server"></asp:label></td>
</tr>
    <tr>
        <td>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1">
                <Columns>
                    <asp:BoundField DataField="RegNo" HeaderText="RegNo" SortExpression="RegNo" />
                    <asp:BoundField DataField="FName" HeaderText="FName" SortExpression="FName" />
                    <asp:BoundField DataField="ParentLoginName" HeaderText="ParentLoginName" SortExpression="ParentLoginName" />
                    <asp:BoundField DataField="ParentPassword" HeaderText="ParentPassword" SortExpression="ParentPassword" />
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT [RegNo], [FName], [ParentLoginName], [ParentPassword] FROM [vw_Parent_Login] WHERE ([ASID] = @ASID)">
                <SelectParameters>
                    <asp:CookieParameter CookieName="ASID" DefaultValue="0" Name="ASID" Type="Int32" />
                </SelectParameters>
            </asp:SqlDataSource>
            <asp:Button ID="Button7" runat="server" Text="test Log" />

            <asp:Button ID="Button8" runat="server" Text="procedure" />
        </td>
        <td>

            <asp:Button ID="Button5" runat="server" Text="Library" />

        </td>
    </tr>
    <tr>
        <td>

            <asp:Button ID="Button2" runat="server" Text="write to file" />
        </td>
        <td>

            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>

            <asp:Button ID="Button3" runat="server" Text="fire query on tbl" />

            <asp:Button ID="Button6" runat="server" Text="insert" Width="52px" />

        </td>
    </tr>
    <tr>
        <td>

            <asp:Button ID="Button1" runat="server" Text="Button" Width="52px" />

        </td>
        <td>

&nbsp;<asp:TextBox ID="txtInput" runat="server" Height="18px" Width="76px"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;

<asp:label ID="lblMsg" runat="server"></asp:label>

        </td>
    </tr>
    <tr>
        <td>

            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
        </td>
        <td>

            <asp:ListBox ID="ListBox1" runat="server" Height="122px" Width="130px"></asp:ListBox>
            <asp:Button ID="Button4" runat="server" Text="Button" />
            <rsweb:ReportViewer ID="ReportViewer1" runat="server">
            </rsweb:ReportViewer>
        </td>
    </tr>
</table>
        
                        
    </div>
    </form>
</body>
</html>
