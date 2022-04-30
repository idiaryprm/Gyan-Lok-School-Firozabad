<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.master" CodeBehind="AssignRollNo.aspx.vb" Inherits="iDiary_V3.AssignRollNo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Heading" runat="server">
    Roll No. Assignment 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  
    <script type="text/javascript">
        function tabE(obj, e) {
            var e = (typeof event != 'undefined') ? window.event : e;// IE : Moz 
            if (e.keyCode == 13) {
                var ele = document.forms[0].elements;
                for (var i = 0; i < ele.length; i++) {
                    var q = (i == ele.length - 1) ? 0 : i + 1;// if last element : if any other 
                    if (obj == ele[i]) { ele[q].focus(); break }
                }
                return false;
            }
        }
</script> 
     <script type="text/javascript">
      function print()
    {
          var printFriendly = document.getElementById("gvDiv")
    var printWin = window.open("about:blank","Voucher","menubar=no;status=no;toolbar=no;");
    printWin.document.write("<html><head><title>Student Roll No. Report</title></head><body>" + printFriendly.innerHTML + "</body></html>");
    printWin.document.close();
    printWin.window.print();   
    printWin.close();
    }
   
    </script>
     <%--   <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /> 
        <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />  --%> 
     <div class="col_3" style="margin-top: 20px;" id="panelEnquiryNo" runat="server">
        <div class="col-md-3 widget widget1" style="width: 100%">
            <div class="r3_counter_box">
                <table class="table">
                    <tr>
                        <td class="auto-style14">School Name</td>
                        <td class="auto-style6" colspan="2">
                             <asp:DropDownList ID="cboSchoolName" runat="server" CssClass="Dropdown" Width="300px" AutoPostBack="true" ></asp:DropDownList></td>
                        <td class="auto-style15">
                            &nbsp;</td>
                        <td class="auto-style10">Class</td>
                        <td>
                            <asp:DropDownList ID="cboClass" runat="server" CssClass="Dropdown"
                                AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                      
                        <td>
                             &nbsp;</td>
                      
                    </tr>
                    
                    <tr>
                        <td class="auto-style14">Section</td>
                        <td class="auto-style16">
                            <asp:DropDownList ID="cboSection" runat="server" CssClass="Dropdown">
                            </asp:DropDownList>
                        </td>
                        <td class="auto-style17">&nbsp;</td>
                        <td class="auto-style15">
                            &nbsp;</td>
                        <td class="auto-style10">Status</td>
                        <td>
                            <asp:DropDownList ID="cboStatus" runat="server" CssClass="Dropdown">
                            </asp:DropDownList>
                        </td>
                      
                        <td>
                             <asp:Button ID="btnShow" runat="server" CssClass="btn btn-primary" Height="35px" Text="Next" />
                        </td>
                      
                    </tr>
                    
                    <tr>

                        <td colspan="7">
                            <div id="gvDiv" style=" max-height: 400px; overflow-y: scroll; text-align: center;">
                                <asp:Label ID="lblClassName" runat="server" Text="" style="font-weight: 700"></asp:Label>
                            <asp:Table ID="myTable" runat="server" CellPadding="0" CellSpacing="0"
                                Width="100%" BorderColor="Black" BorderWidth="1px" GridLines="Both">

                            </asp:Table>
                                </div>
                        </td>
                    </tr>
                  <tr>

                        <td colspan="6">
                            <asp:Label ID="lblStatus" runat="server" Font-Bold="True" ForeColor="Navy"></asp:Label>
                        </td>

                        <td>
                            &nbsp;</td>
                    </tr>
                  <tr>

                        <td class="auto-style14">
                            &nbsp;<asp:Label ID="lblAssignRollNo" runat="server" Text="Assignment Type" Visible="False"></asp:Label>
                        </td>

                        <td class="auto-style16">
                            <asp:DropDownList ID="cboassignrollno" runat="server" CssClass="Dropdown" Visible="False">
                                <asp:ListItem>Manually</asp:ListItem>
                                <asp:ListItem>Alphabetically</asp:ListItem>
                                <asp:ListItem>Gender wise</asp:ListItem>
                            </asp:DropDownList>
                        </td>

                        <td class="auto-style17">
                            &nbsp;</td>

                        <td class="auto-style15">
                            <asp:Button ID="btnassign" runat="server" CssClass="btn btn-primary" Text="Save" Visible="False" />
                            &nbsp; <asp:Button ID="btnprint" runat="server" CssClass="btn btn-primary" Text="Print" Visible="False" />
                        </td>

                        <td class="auto-style10">
                            &nbsp;</td>

                        <td>
                            &nbsp;</td>

                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>

                        <td colspan="5" style="height: 38px">
                            <asp:Button ID="btnAsignNo" runat="server" CssClass="btn btn-primary" Text="Assign Roll No" Enabled="false" OnClientClick="print()" Visible="false"  />
                            &nbsp;&nbsp;&nbsp;<asp:Button ID="btnAsignNoAlphab" runat="server" CssClass="btn btn-primary" Text="Assign Roll No Alphabetically" Enabled="false" Visible="false" />
                        </td>
                        <td class="auto-style4">
                             &nbsp;</td>
                        <td style="width: 140px; height: 38px;">
                             &nbsp;</td>
                    </tr>
                    <tr>

                        <td colspan="5">
                            <asp:SqlDataSource ID="SqlDataSourceStudents" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT [RegNo], [SName], [ClassRollno], [ClassName], [SecName] FROM [vw_Student] WHERE ([ASID] = @ASID) AND ([STATUSNAME]= @STATUSNAME)" UpdateCommand="UPDATE Student SET ClassRollno = @ClassRollno WHERE (RegNo = @regno)">
                                <SelectParameters>
                                    <asp:SessionParameter Name="ASID" SessionField="ASID" Type="Int32" />
                                    <asp:ControlParameter ControlID="cboStatus" Name="STATUSNAME" PropertyName="SelectedValue" />
                                </SelectParameters>
                                <UpdateParameters>
                                    <asp:Parameter Name="ClassRollno" />
                                    <asp:Parameter Name="regno" />

                                </UpdateParameters>
                            </asp:SqlDataSource>
                             <asp:Button ID="btnAssignRollAlGen" runat="server" CssClass="btn btn-primary" Text="Assign Roll No Gender wise" Enabled="false" Visible="false" />
                        </td>
                        <td>&nbsp;</td>
                        <td style="width: 140px">&nbsp;</td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="clearfix"></div>
    </div>
</asp:Content>
<asp:Content ID="Content3" runat="server" contentplaceholderid="head">
    <style type="text/css">
        .auto-style4 {
            height: 38px;
        }
        .auto-style6 {
        }
        .auto-style10 {
            width: 384px;
        }
        .auto-style14 {
            width: 342px
        }
        .auto-style15 {
            width: 261px
        }
        .auto-style16 {
            width: 77px;
        }
        .auto-style17 {
            width: 146px;
        }
    </style>
</asp:Content>

