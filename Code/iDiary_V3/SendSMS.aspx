<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/AdminMaster.Master" CodeBehind="SendSMS.aspx.vb" Inherits="iDiary_V3.SendSMS" %>

<asp:Content ID="Content2" ContentPlaceHolderID="AdminMasterContents" runat="server">
    
    <script language="javascript" type="text/javascript">
        function CountSMSCharacter() {
            var count = 0;
            var mgsLength = 0;

            var textField =
                document.getElementById("<%=txtMessage.ClientID%>");
    var labelField =
        document.getElementById("<%=lblSMSCount.ClientID%>");

    var length = textField.value.length;
    var setValue = count + length;

    if (setValue == 0) {
        mgsLength = 0;
        labelField.innerHTML =
            "Total characters " + setValue + "; No of SMS: " + mgsLength;
    }
    else {
        if (setValue <= 160) {
            mgsLength = 1;
            labelField.innerHTML =
                 "Total characters " + setValue + "; No of SMS: " + mgsLength;
        }
        else {
            var roundLength = setValue / 160;
            mgsLength = Math.ceil(roundLength);
            labelField.innerHTML =
                     "Total characters " + setValue + "; No of SMS: " + mgsLength;
        }
    }
}
    </script>
    <script>
        function colorDiv(){ 
            var div = document.getElementById( 'smsinfo' ); 
            var a = document.getElementById("<%= lblTotalSMS.ClientID()%>");
            a = a.value;
            if (a<5000)
            {
                div.style.backgroundColor='red';
            }
            else{
                div.style.backgroundColor='green';
            }
            document.getElementById("demo").innerHTML =selection;    
            }
</script>
    <%--<br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />--%>
     <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div id="smsinfo" onload="colorDiv()">
                            <asp:Label runat="server" ID="lblTotalSMS"></asp:Label>
                        </div>
    <table class="table">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <tr>
            <td style="width:20%"><strong>&nbsp;Send To</strong></td>
            <td style="width: 30%">
                <asp:RadioButtonList ID="rblSmsReceiver" runat="server" AutoPostBack="True" RepeatDirection="Horizontal" Width="250px">
                    <asp:ListItem>Student</asp:ListItem>
                    <asp:ListItem>Employee</asp:ListItem>
                    <asp:ListItem>Individual</asp:ListItem>
                </asp:RadioButtonList>

            </td>
              <td style="width:20%">&nbsp;</td>
              <td style="width:20%">
                  &nbsp;</td>
            <td style="width:10%"></td>
        </tr>
          <tr>
            <td colspan="4">
                <asp:Panel ID="PanelStudent" runat="server">
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 187px"><strong>School Name</strong></td>
                            <td colspan="3">
                                <asp:DropDownList ID="cboSchoolName" OnSelectedIndexChanged="cboSchoolName_SelectedIndexChanged"  runat="server" AutoPostBack="true" CssClass="Dropdown" Width="300px">
                                </asp:DropDownList>
                            </td>
                            <td>
                                &nbsp;</td>
                                                    </tr>
                        <tr>
                            <td style="width: 187px"><b>Class Name&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</b></td>
                            <td>
                                <asp:DropDownList ID="cboClass" runat="server" AutoPostBack="True" CssClass="Dropdown">
                                </asp:DropDownList>
                            </td>
                            <td><b>Section</b></td>
                            <td>
                                <asp:DropDownList ID="cboSection" runat="server" CssClass="Dropdown">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Button ID="btnStudent" runat="server" CssClass="btn btn-primary" Text="&gt;&gt;" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                 <asp:Panel ID="PanelEmployee" runat="server">
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 190px"><strong>Employee Category</strong></td>
                            <td>
                                <asp:DropDownList ID="cboEmpCat" runat="server" CssClass="Dropdown">
                                </asp:DropDownList>
                            </td>
                            <td><strong>Employee Status</strong></td>
                            <td>
                                <asp:DropDownList ID="cboEmpStatus" runat="server" CssClass="Dropdown">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Button ID="btnEmployee" runat="server" Text="&gt;&gt;" CssClass="btn btn-primary" />
                            </td>
                            
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="PanelIndividual" runat="server">
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 174px; vertical-align:top;"><strong>Enter Mobile No.<br /> <br />
                                <asp:Label ID="lblStatusMsg" runat="server" ForeColor="Navy" style="font-weight: 700" Width="140px">(Enter 10 digit mobile no.  one in a row)</asp:Label>
                                </strong></td>
                            <td style="width: 239px;vertical-align:top;">
                                <asp:TextBox ID="txtMobNo" runat="server" CssClass="textbox" Height="140px" TextMode="MultiLine" Width="248px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtMobNoInvalid" runat="server" CssClass="textbox" ForeColor="#FF3300" Height="140px" TextMode="MultiLine" Visible="False" Width="248px"></asp:TextBox>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
               <td style="width:10%"></td>
        </tr>
          
          <tr>
            <td style="width:20%" valign="top" rowspan="8">
                <b>Message Template<br />
                </b>
                <asp:DropDownList ID="cboMessageTemplate" CssClass="Dropdown" runat="server" 
                    AutoPostBack="True">
                </asp:DropDownList>
                <strong style="vertical-align: top">
                <br />
                <br />
                Message<br />
                </strong>
                <asp:TextBox ID="txtMessage" CssClass="textbox" runat="server" onkeyup="CountSMSCharacter();" Height="154px" 
                    TextMode="MultiLine" Width="95%" BorderWidth="1px"></asp:TextBox>
                <br />
                <asp:Label ID="lblSMSCount" runat="server" ForeColor="Navy" 
                    style="font-weight: 700"></asp:Label>
                <br />
                <asp:Label ID="lblStatus" runat="server" ForeColor="Navy" 
                    style="font-weight: 700"></asp:Label>
                <strong>
                <br />
                <br />
                Message Type<br />
                <asp:DropDownList ID="cboMessageType" runat="server"  CssClass="Dropdown">
                    <asp:ListItem>Simple</asp:ListItem>
                    <asp:ListItem>Unicode</asp:ListItem>
                </asp:DropDownList>
                <br />
                <br />
                SMS Sender<br />
                </strong>
                <asp:DropDownList ID="cboFrom" runat="server"  CssClass="Dropdown">
                </asp:DropDownList>
                <br />
                <br />
                <asp:Button ID="btnSend" runat="server" style="margin-left: 0px" Text="Send"  CssClass="btn btn-primary" />
                  
                <br />
                <asp:Label ID="lblCount" runat="server" ForeColor="Navy" 
                    style="font-weight: 700"></asp:Label>
                <br />
                </td>
              <td colspan="3" rowspan="9" valign="top">
               <table style="width:100%">
                   <tr>
                       <td style="width:5%">

                       </td>
                       <td style="width:95%;vertical-align:top;">
                <b><asp:CheckBox ID="chkCheckAll" runat="server" AutoPostBack="True" Text="Check All" /></b>
                           <div style="height:1000px;overflow-y:scroll">
                            <asp:GridView ID="gvStudent" runat="server" AutoGenerateColumns="False"  DataKeyNames="SID"
                    BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" 
                    CellPadding="4"  Width="100%" Height="16px" CssClass="Grid" DataSourceID="SqlDataSource1" Visible="False">
                    <RowStyle BackColor="White" ForeColor="#330099" />
                    <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                        <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="true"  OnCheckedChanged="chkSelect_CheckedChanged"  />
                        </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ClassRollNo" HeaderText="Roll No" SortExpression="ClassRollNo" />
                        <asp:BoundField DataField="SName" HeaderText="Student Name" SortExpression="SName" />
                        <asp:BoundField DataField="FName" HeaderText="Father's Name" SortExpression="FName" />
                        <asp:BoundField DataField="ClassName" HeaderText="Class" 
                            SortExpression="ClassName" />
                        <asp:BoundField DataField="SecName" HeaderText="Sec" 
                            SortExpression="SecName" />
                        <asp:BoundField DataField="MobNo" HeaderText="Mobile No" SortExpression="MobNo" />
                    </Columns>
                    <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                    <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                </asp:GridView>
                <asp:GridView ID="gvEmployee" runat="server" AutoGenerateColumns="False" DataKeyNames="EmpID"
                    BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" 
                    CellPadding="4"  Width="100%" Height="16px" CssClass="Grid" DataSourceID="SqlDataSource2" Visible="False">
                    <RowStyle BackColor="White" ForeColor="#330099" />
                    <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                        <asp:CheckBox ID="chkSelectEmp" runat="server" AutoPostBack="false" OnCheckedChanged="chkSelect_CheckedChanged"  />
                        </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="EmpName" HeaderText="Employee Name" SortExpression="EmpName" />
                        <asp:BoundField DataField="EmpCode" HeaderText="Code" SortExpression="EmpCode" />
                        <asp:BoundField DataField="DeptName" HeaderText="Depart." 
                            SortExpression="DeptName" />
                        <asp:BoundField DataField="DesgName" HeaderText="Desig." 
                            SortExpression="DesgName" />
                        <asp:BoundField DataField="Mob" HeaderText="Mobile No" SortExpression="Mob" />
                    </Columns>
                    <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                    <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                </asp:GridView>
                               </div>
                                        <br />
                                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                                            ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" 
                                            
                    
                    SelectCommand="SELECT [ClassRollNo],[SName], [FName], [ClassName], [SecName], [MobNo],SID FROM [vw_Student] WHERE SID is NULL" >
                                           
                                        </asp:SqlDataSource>
                                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                                            ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" 
                                            
                    
                    SelectCommand="SELECT [EmpName], [EmpCode], [DeptName], [DesgName], [Mob],EmpID FROM [vw_Employees] WHERE EmpID is NULL">
                                           
                                        </asp:SqlDataSource>
                       </td>
                   </tr>
               </table>
                </td>
               <td style="width:10%">
                   &nbsp;</td>
        </tr>
          
          <tr>
               <td style="width:10%">&nbsp;</td>
        </tr>
          
          <tr>
               <td style="width:10%">&nbsp;</td>
        </tr>
          
          <tr>
               <td style="width:10%">&nbsp;</td>
        </tr>
          
          <tr>
               <td style="width:10%">&nbsp;</td>
        </tr>
          
          <tr>
               <td style="width:10%">&nbsp;</td>
        </tr>
          
          <tr>
               <td style="width:10%">&nbsp;</td>
        </tr>
          
          <tr>
               <td style="width:10%; height: 174px;"></td>
        </tr>
          
          <tr>
            <td style="width:20%">
           &nbsp;</td>
               <td style="width:10%">&nbsp;</td>
        </tr>
          
    </table>
         </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSend" />
                    </Triggers>
                </asp:UpdatePanel>
</asp:Content>
