<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/StudentMaster.master" CodeBehind="Capture.aspx.vb" Inherits="iDiary_V3.Capture" %>
<asp:Content ID="Content2" ContentPlaceHolderID="StudentMasterContents" runat="server">
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
         <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src='<%=ResolveUrl("~/Webcam_Plugin/jquery.webcam.js") %>' type="text/javascript"></script>
    <script type="text/javascript">
        var pageUrl = '<%=ResolveUrl("~/Capture.aspx") %>';
        $(function () {
            jQuery("#webcam").webcam({
                width: 320,
                height: 240,
                mode: "save",
                swffile: '<%=ResolveUrl("~/Webcam_Plugin/jscam.swf") %>',
                debug: function (type, status) {
                    $('#camStatus').append(type + ": " + status + '<br /><br />');
                },
                onSave: function (data) {
                    $.ajax({
                        type: "POST",
                        url: pageUrl + "/GetCapturedImage",
                        data: '',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (r) {
                            $("[id*=imgCapture]").css("visibility", "visible");
                            $("[id*=imgCapture]").attr("src", r.d);
                        },
                        failure: function (response) {
                            alert(response.d);
                        }
                    });
                },
                onCapture: function () {
                    webcam.save(pageUrl);
                }
            });
        });
        function Capture() {
            webcam.capture();
            return false;
        }
    </script>
    
    <table border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td align="center">
                <u>Live Camera</u>
            </td>
            <td>
            </td>
            <td align="center">
                <u>Captured Picture</u>
            </td>
        </tr>
        <tr>
            <td>
                <div id="webcam">
                </div>
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:Image ID="imgCapture" runat="server" Style="visibility: hidden; width: 320px;
                    height: 240px" />
            </td>
        </tr>
    </table>
    <br />
    <asp:Button ID="btnCapture" Text="Capture" runat="server" OnClientClick="return Capture();" />
    <br />
    <span id="camStatus"></span>
    
      <table class="table">
        <tr>
            <td class="auto-style8" colspan="8">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" AutoGenerateSelectButton="True" CssClass="Grid" DataSourceID="SqlDataSource1" Width="98%">
                    <Columns>
                        <asp:BoundField DataField="RegNo" HeaderText="Reg No" SortExpression="RegNo" />
                        <asp:BoundField DataField="SName" HeaderText="Name" SortExpression="SName" />
                        <asp:BoundField DataField="ClassName" HeaderText="Class" SortExpression="ClassName" />
                        <asp:BoundField DataField="SecName" HeaderText="Sec" SortExpression="SecName" />
                        <asp:BoundField DataField="FName" HeaderText="Father Name" SortExpression="FName" />
                        <asp:BoundField DataField="MName" HeaderText="Mother Name" SortExpression="MName" />
                        <asp:BoundField DataField="AdmissionDate" DataFormatString="{0:d}" HeaderText="Admission Date" HtmlEncode="False" SortExpression="AdmissionDate" />
                        <asp:BoundField DataField="DOB" DataFormatString="{0:d}" HeaderText="Date of Birth" HtmlEncode="False" SortExpression="DOB" />
                    </Columns>
                    
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td class="auto-style8">Admin/Reg No.</td>
            <td class="auto-style2">
                <asp:TextBox ID="txtAdminNo" runat="server" CssClass="textbox"></asp:TextBox>
            </td>
            <td class="auto-style10">
                <asp:Button ID="btnFind" runat="server" Text="&gt;&gt;" 
                    CssClass="btn btn-primary" />
            </td>
            <td class="auto-style3">&nbsp;Student Name</td>
            <td class="auto-style9">
                <asp:TextBox ID="txtSName" runat="server" CssClass="textbox"></asp:TextBox>
            </td>
            <td>
                <asp:Button ID="btnNameSearch" runat="server" Text="&gt;&gt;" 
                    CssClass="btn btn-primary" />
            </td>
            <td class="auto-style5">Father Name</td>
              <td><asp:TextBox ID="txtFName" runat="server" CssClass="textbox" ReadOnly="True" 
                    ></asp:TextBox></td>
        </tr>
        <tr>
            <td class="auto-style8">Mother Name</td>
            <td class="auto-style2"><asp:TextBox ID="txtMName" runat="server" CssClass="textbox" ReadOnly="True" 
                   ></asp:TextBox>
                
            </td>
            <td class="auto-style10">&nbsp;</td>
            <td class="auto-style3">Admission Date</td>
            <td class="auto-style9"> <asp:TextBox ID="txtAdmissionDate" runat="server"  ReadOnly="True" 
                    CssClass="textbox"></asp:TextBox>
                
            </td>
            <td> &nbsp;</td>
             <td class="auto-style5">Date of Birth</td>
              <td> <asp:TextBox ID="txtDOB" runat="server" ReadOnly="True" 
                   CssClass="textbox"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="auto-style8">Class - Section</td>
            <td style="margin-left: 40px" class="auto-style2">
                <asp:TextBox ID="txtClass" runat="server" Width="92px" ReadOnly="True" 
                 CssClass="textbox"></asp:TextBox>
                &nbsp;
                <asp:TextBox ID="txtSec" runat="server" Width="40px" ReadOnly="True" 
                 CssClass="textbox"></asp:TextBox>
            </td>
            <td style="margin-left: 40px" class="auto-style10">
                &nbsp;</td>
            <td class="auto-style3">
                <asp:TextBox ID="txtDOBInWords" runat="server" BorderWidth="1px" 
                    Width="55px" Visible="False"></asp:TextBox>
            </td>
            <td class="auto-style9">
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT RegNo, SName, ClassName, SecName, FName, MName,AdmissionDate,DOB FROM vw_Student WHERE [SName] Like '%SearchByName%' or @SearchByName is null">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="txtSName" Name="SearchByName" PropertyName="Text" />
                    </SelectParameters>
                </asp:SqlDataSource>    
            <asp:TextBox ID="txtGender" runat="server" BorderWidth="1px" 
                    Width="55px" Visible="False"></asp:TextBox></td>
            <td>
                &nbsp;</td>
             <td class="auto-style5"><asp:TextBox ID="txtRollNo" runat="server" BorderWidth="1px" 
                    Width="55px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="txtSID" runat="server" BorderWidth="1px" 
                    Width="55px" Visible="False"></asp:TextBox></td>
              <td><asp:Button ID="btnGenerate" runat="server" Text="Generate Certificate" 
                    CssClass="btn btn-primary" /></td>
        </tr>

        <tr>
            <td class="auto-style8">&nbsp;</td>
            <td style="margin-left: 40px" class="auto-style2">
                &nbsp;</td>
            <td style="margin-left: 40px" class="auto-style10">
                &nbsp;</td>
            <td class="auto-style3">
                &nbsp;</td>
            <td class="auto-style9">&nbsp;</td>
            <td>&nbsp;</td>
             <td class="auto-style5"></td>
              <td></td>
        </tr>


       


    </table>

</asp:Content>
