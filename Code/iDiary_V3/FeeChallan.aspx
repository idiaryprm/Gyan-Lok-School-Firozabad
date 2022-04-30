<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="FeeChallan.aspx.vb" Inherits="iDiary_V3.FeeChallan" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Heading" runat="server">
    <script language="javascript" type="text/javascript">


        function ShowTotal() {
            debugger;
            var total = document.getElementById('<%=myTable.ClientID%>');
    var x = 0;
    var ar = new Array();
    var textBox = new Array(); // to store the textbox objects
    var oInputs = new Array();

    oInputs = total.getElementsByTagName('input') // store collection of all <input/> elements
    for (i = 0; i < oInputs.length; i++) { // loop through and find <input type="text"/>
        if (oInputs[i].type == 'text') {
            textBox.push(oInputs[i]); // found one - store it in the oTextBoxes array
        }
        //alert(i);
    }


    for (i = 0; i < textBox.length; i++) { // Loop through the stored textboxes and output the value
        if (textBox[i].value != "") {
            x = x + parseInt(textBox[i].value);
        }
    }
    //alert(x);

    document.getElementById('txtTotal').value = x;
    document.getElementById('test').value = x;
}

</script>
    Fee Challan
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>   
     <table border="0">
        <tr>
            <td style="width: 11%; height: 26px;">
                &nbsp;</td>
            <td style="height: 26px;" colspan="6">
                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" AutoGenerateSelectButton="True" CssClass="Grid" DataSourceID="SqlDataSource2" ShowHeader="False" Width="98%">
                    <Columns>
                        <asp:BoundField DataField="RegNo" HeaderText="RegNo" SortExpression="RegNo" />
                        <asp:BoundField DataField="SName" HeaderText="SName" SortExpression="SName" />
                        <asp:BoundField DataField="ClassName" HeaderText="ClassName" SortExpression="ClassName" />
                        <asp:BoundField DataField="SecName" HeaderText="SecName" SortExpression="SecName" />
                    </Columns>
                   
                </asp:GridView>
            </td>
            <td style="height: 26px">
                &nbsp;</td>
        </tr>
         <tr>
             <td style="width: 11%; height: 26px;">
                 <asp:RadioButton ID="optIndivisual" runat="server" AutoPostBack="True" Checked="True" GroupName="G1" style="font-weight: 700" Text="Indivisual" />
             </td>
             <td style="width: 17%; height: 26px;">Reg/Admin No.</td>
             <td style="margin-left: 80px; width: 22%; height: 26px;">
                 <asp:TextBox ID="txtRegno" runat="server" AutoPostBack="True" CssClass="textbox"></asp:TextBox>
             </td>
             <td style="width: 104px; height: 26px;">
                 Name</td>
             <td style="width: 18%; height: 26px;">
                 <asp:TextBox ID="txtName" runat="server" CssClass="textbox" Width="100px"></asp:TextBox>
                             &nbsp; <asp:Button ID="btnNameSearch" runat="server" CssClass="hvr-glow" Text="&gt;&gt;" />
             </td>
             <td style="height: 26px; width: 14%;">Challan Date</td>
             <td style="height: 26px; width: 6%;">
                 <asp:TextBox ID="txtChallanDate" runat="server" CssClass="textbox"></asp:TextBox>
                 <asp:CalendarExtender ID="txtChallanDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="txtChallanDate">
                 </asp:CalendarExtender>
             </td>
             <td style="height: 26px"></td>
         </tr>
         <tr>
             <td style="width: 11%; height: 26px;">&nbsp;</td>
             <td style="width: 17%; height: 26px;">Name</td>
             <td style="margin-left: 80px; width: 22%; height: 26px;">
                 <asp:Label ID="lblSName" runat="server" Font-Size="Small" ForeColor="Black" style="font-weight: 700"></asp:Label>
             </td>
             <td style="width: 104px; height: 26px;">Father</td>
             <td style="width: 18%; height: 26px;">
                 <asp:Label ID="lblFather" runat="server" Font-Size="Small" ForeColor="Black" style="font-weight: 700"></asp:Label>
             </td>
             <td style="height: 26px; width: 14%;">Class</td>
             <td style="height: 26px; width: 6%;">
                 <asp:Label ID="lblClass" runat="server" Font-Size="Small" ForeColor="Black" style="font-weight: 700"></asp:Label>
             </td>
             <td style="height: 26px">&nbsp;</td>
         </tr>
        <tr>
            <td style="width: 11%; height: 26px;">
                <asp:RadioButton ID="optClassWise" runat="server" AutoPostBack="True" GroupName="G1" style="font-weight: 700" Text="Class Wise" />
            </td>
            <td style="width: 17%; height: 26px;">
                Class</td>
            <td style="margin-left: 80px; width: 22%; height: 26px;">
                <asp:DropDownList ID="cboClass" runat="server" AutoPostBack="True" CssClass="Dropdown">
                </asp:DropDownList>
            </td>
            <td style="width: 104px; height: 26px;">
                Section</td>
            <td style="width: 18%; height: 26px;">
                <asp:DropDownList ID="cboSection" runat="server" CssClass="Dropdown">
                </asp:DropDownList>
            </td>
            <td style="height: 26px; width: 14%;">
                Status</td>
            <td style="height: 26px; width: 6%;">
                <asp:DropDownList ID="cboStatus" runat="server" CssClass="Dropdown">
                </asp:DropDownList>
                </td>
            <td style="height: 26px">
                &nbsp;</td>
        </tr>
         <tr>
             <td style="width: 11%; height: 26px;">&nbsp;</td>
             <td style="width: 17%; height: 26px;">Fee Term</td>
             <td style="margin-left: 80px; width: 22%; height: 26px;">
                 <asp:DropDownList ID="cboTerm" runat="server" CssClass="Dropdown">
                     <asp:ListItem></asp:ListItem>
                     <asp:ListItem>I</asp:ListItem>
                     <asp:ListItem>II</asp:ListItem>
                     <asp:ListItem>III</asp:ListItem>
                     <asp:ListItem>IV</asp:ListItem>
                 </asp:DropDownList>
             </td>
             <td style="width: 104px; height: 26px;">BusTerm</td>
             <td style="width: 18%; height: 26px;">
                 <asp:DropDownList ID="cboBusTerm" runat="server" CssClass="Dropdown">
                    
                 </asp:DropDownList>
             </td>
             <td style="height: 26px; width: 14%;">&nbsp;</td>
             <td style="height: 26px; width: 6%;">
                 <asp:Button ID="btnRegNext" runat="server" CssClass="hvr-glow" Text="Next"/>
             </td>
             <td style="height: 26px">&nbsp;</td>
         </tr>
        <tr>
            <td style="width: 11%; ">
                &nbsp;</td>
            <td colspan="6">
                <asp:Label ID="lblStatus" runat="server" ForeColor="Navy" style="font-weight: 700; color: #FF3300;"></asp:Label>
            </td>
            <td width="5%">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="7">

        <asp:Table ID="myTable" runat="server" Width="100%" BorderStyle="Solid" 
    BorderWidth="1px">
            
        </asp:Table>
            </td>
            <td width="5%">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 11%">
                Total Amount</td>
            <td style="width: 17%">
                <input id="txtTotal" name="txtTotal" type="text" value="0" 
                        style="border: 1px solid navy; width: 92px" readonly align="Left" /></td>
            <td style="margin-left: 80px; width: 22%;">
                &nbsp;</td>
            <td style="width: 104px">
                &nbsp;</td>
            <td style="width: 18%">
                <asp:Button ID="btnViewChallan" runat="server" 
                    Text="View Challan" CssClass="hvr-glow"/>
            </td>
            <td style="width: 14%">
                &nbsp;</td>
            <td style="width: 6%">
                <asp:TextBox ID="txtDueDate" runat="server" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" Enabled="False" Visible="False" Width="136px"></asp:TextBox>
                <asp:CalendarExtender ID="txtDueDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDueDate">
                </asp:CalendarExtender>
            </td>
            <td width="5%">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 11%">
                &nbsp;</td>
            <td colspan="2">
                &nbsp;</td>
            <td style="width: 104px">
                &nbsp;</td>
            <td colspan="3">
                &nbsp;</td>
            <td width="5%">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="8">
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="100%">
                    <LocalReport ReportEmbeddedResource="rptFeeCollection.rdlc" ReportPath="rptFeeCollection.rdlc">
                        <DataSources>
                            <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="dsFeeCollection" />
                        </DataSources>
                    </LocalReport>
                </rsweb:ReportViewer>
                </td>
        </tr>
        <tr>
            <td style="height: 6px; width: 11%;">
                &nbsp;</td>
            <td style="height: 6px; width: 17%;">
                <asp:TextBox ID="txtAdmissionFeeID" runat="server" BorderStyle="Solid" BorderWidth="1px" ReadOnly="True" Visible="False" Width="22px"></asp:TextBox>
                <asp:TextBox ID="txtLateFeeID" runat="server" BorderStyle="Solid" BorderWidth="1px" ReadOnly="True" Visible="False" Width="22px"></asp:TextBox>
                <asp:TextBox ID="txtTutionFeeID" runat="server" BorderStyle="Solid" BorderWidth="1px" Height="16px" ReadOnly="True" Visible="False" Width="22px"></asp:TextBox>
                <asp:TextBox ID="txtConveyanceFeeID" runat="server" BorderStyle="Solid" BorderWidth="1px" ReadOnly="True" Visible="False" Width="22px"></asp:TextBox>
                <asp:TextBox ID="txtFeeConfigType" runat="server" BorderStyle="Solid" BorderWidth="1px" ReadOnly="True" Visible="False" Width="22px"></asp:TextBox>
                </td>
            <td style="height: 6px; width: 22%;">

        <asp:TextBox ID="txtSID" runat="server" Visible="False" Width="36px"></asp:TextBox>

        <asp:TextBox ID="txtFeeGroupID" runat="server" Visible="False" Width="36px"></asp:TextBox>

                <asp:TextBox ID="txtAdminDate" runat="server" Visible="False" Width="36px"></asp:TextBox>

                </td>
            <td style="height: 6px; " colspan="2">
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT RegNo, SName, ClassName, SecName FROM vw_Student WHERE (ASID = @ASID) and [SName] Like '%SearchByName%' or @SearchByName is null">
                    <SelectParameters>
                        <asp:SessionParameter Name="ASID" SessionField="ASID" />
                        <asp:ControlParameter ControlID="txtName" Name="SearchByName" PropertyName="Text" />
                    </SelectParameters>
                </asp:SqlDataSource>
                </td>
            <td style="height: 6px; " colspan="3">
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                </td>
        </tr>
        </table>
            </ContentTemplate>
         </asp:UpdatePanel>

</asp:Content>
