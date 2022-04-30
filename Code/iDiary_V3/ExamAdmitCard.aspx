<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.master" CodeBehind="ExamAdmitCard.aspx.vb" Inherits="iDiary_V3.ExamAdmitCard" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Heading" runat="server">
    Exam Admit Card
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
         function print() {
             var printFriendly = document.getElementById("gvDiv")
             var printWin = window.open("about:blank", "Voucher", "menubar=no;status=no;toolbar=no;");
             printWin.document.write("<html><head><title>Student Roll No. Report</title></head><body>" + printFriendly.innerHTML + "</body></html>");
             printWin.document.close();
             printWin.window.print();
             printWin.close();
         }

    </script>
    
     <div class="col_3" style="margin-top: 20px;" id="panelEnquiryNo" runat="server">
        <div class="col-md-3 widget widget1" style="width: 100%">
            <div class="r3_counter_box">
                <table class="table">
                    <tr>
                     <td class="auto-style9">School</td>
                        <td class="auto-style6">
                            <asp:DropDownList ID="cboSchoolName" OnSelectedIndexChanged ="cboSchool_SelectedIndexChanged"   runat="server" CssClass="Dropdown"
                                AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                        <td class="auto-style9">Class</td>
                        <td class="auto-style6">
                            <asp:DropDownList ID="cboClass" runat="server" CssClass="Dropdown"
                                AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                        <td class="auto-style11">Section</td>
                        <td class="auto-style5">
                            <asp:DropDownList ID="cboSection" runat="server" CssClass="Dropdown">
                            </asp:DropDownList>
                        </td>
                        <td class="auto-style10">ExamType</td>
                        <td>
                            <asp:DropDownList ID="cboExamType" runat="server" CssClass="Dropdown">
                            </asp:DropDownList>
                        </td>
                      
                        <td>
                             <asp:Button ID="btnShow" runat="server" CssClass="btn btn-primary" Height="35px" Text="Show" />
                        </td>
                      
                    </tr>
                    
                    <tr>

                        <td colspan="7">
                            <div id="gvDiv" style=" max-height: 400px; overflow-y: scroll; text-align: center;">
                                <asp:Label ID="lblClassName" runat="server" Text="" style="font-weight: 700"></asp:Label>
                              
                           
                                </div>
                            
                        </td>
                    </tr>
                  <tr>

                        <td colspan="6">
                              <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Height="35px" Text="Save"  />
                            <asp:Label ID="lblStatus" runat="server" Font-Bold="True" ForeColor="Navy"></asp:Label>
                        </td>

                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="6">
                             <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="96%">
                </rsweb:ReportViewer>
                        </td>
                    </tr>
                  <tr>

                        <td class="auto-style9">
                            &nbsp;<asp:Label ID="lblAssignRollNo" runat="server" Text="Assignment Type" Visible="False"></asp:Label>
                            <asp:Label runat="server" ID="lblExamGrpID" Visible="false" ></asp:Label>
                        </td>

                        <td class="auto-style6">
                            <asp:DropDownList ID="cboassignrollno" runat="server" CssClass="Dropdown" Visible="False">
                                <asp:ListItem>Manually</asp:ListItem>
                                <asp:ListItem>Alphabetically</asp:ListItem>
                                <asp:ListItem>Gender wise</asp:ListItem>
                            </asp:DropDownList>
                        </td>

                        <td class="auto-style11">
                             <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager></td>

                        <td>
                           <%-- <asp:Button ID="btnassign" runat="server" CssClass="btn btn-primary" Text="Save" Visible="False" />--%>
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
        .auto-style5 {
            width: 337px;
        }
        .auto-style6 {
            width: 34px;
        }
        .auto-style9 {
            width: 517px;
        }
        .auto-style10 {
            width: 641px;
        }
        .auto-style11 {
            width: 536px;
        }
    </style>
</asp:Content>
