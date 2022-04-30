<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="Dashboard.aspx.vb" Inherits="iDiary_V3.Dashboard" %>

<%@ Import Namespace="System.Data" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" runat="server" contentplaceholderid="Heading">
    Dashboard
    <div style="float:right;"><a onclick="reloadPage();" style="text-decoration: none !important;color:white;cursor:pointer "><i class="fa fa-refresh"></i>&nbsp;&nbsp;Refresh</a></div>
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="ContentPlaceHolder1">
    <br />
<script type="text/javascript">
    var idleInterval = setInterval("reloadPage()", 150000);
    function reloadPage() {
        location.reload();
    }
</script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
                            </asp:ScriptManager>
     <div class="col_3" >
        <div class="col-md-3 widget widget1">
            <div class="r3_counter_box">
                <i class="pull-left fa fa-user user fa-2x icon-rounded"></i>
                <div class="stats">
                    <h5><strong>Student</strong></h5>
                </div>
                Strength : <asp:Label ID="lblStudentStrength" runat="server" Text=""></asp:Label>
                <div><i class="fa fa-male" style="width:12px;height:0px;line-height:0px;text-align:left"></i>:<asp:Label ID="lblMaleStudent" runat="server" Text=""></asp:Label>&nbsp;|&nbsp;<i class="fa fa-female" style="width:12px;height:0px;line-height:0px;text-align:left"></i>:<asp:Label ID="lblFemaleStudent" runat="server" Text=""></asp:Label> </div>
                 <div style="text-align:right "><a href="StudentStrength.aspx">View More...</a></div> 
                <hr />
                <div style="text-align:center">
                    <div>
                        <h5><strong>Attendence :</strong></h5>
                    </div>
                    <div>P : <%: ViewStudentsPresent("1")%> &nbsp;&nbsp;| &nbsp;&nbsp; A : <%: ViewStudentsPresent("0")%></div>
                                    
                      <div>Pending : <%: ViewPendingAtt()%></div>
                 </div> 
                 <div style="text-align:right "><a href="AttendanceReport.aspx">View More...</a></div> 
            </div>
        </div>
        <div class="col-md-3 widget widget1">
            <div class="r3_counter_box">
                <i class="pull-left fa fa-user user1 fa-2x icon-rounded"></i>
                <div class="stats">
                    <h5><strong>Employee</strong></h5>
                </div>
                Strength : <asp:Label ID="lblEmployeeStrength" runat="server" Text=""></asp:Label><br />
                <div>
                    <i class="fa fa-male" style="width:12px;height:0px;line-height:0px;text-align:left"></i> :
                    <asp:Label ID="lblMaleEmployee" runat="server" Text=""></asp:Label>
                    &nbsp;&nbsp;|&nbsp;&nbsp; <i class="fa fa-female" style="width:12px;height:0px;line-height:0px;text-align:left"></i> :
                    <asp:Label ID="lblFemaleEmployee" runat="server" Text=""></asp:Label>
                </div>
                <div></div>
                <div style="text-align:right "><a href="EmployeeSearch.aspx">View More...</a></div> 
                <hr />
                <div style="text-align:center">
                <div>
                    <h5><strong>Attendence :</strong></h5>
                </div>
                <div>P : <%: ViewEmployeePresent("1")%> &nbsp;&nbsp;| &nbsp;&nbsp; A : <%: ViewEmployeePresent("0")%> </div>

                <div>Pending : <%: ViewPendingAttEmp()%></div>
                    </div>
                <div style="text-align:right "><a href="EmployeeAttendanceReport.aspx">View More...</a></div> 
            </div>
        </div>
        <div class="col-md-3 widget widget1">
            <div class="r3_counter_box">
                <i class="pull-left fa fa-inr user2 fa-2x icon-rounded"></i>
                <div >
                    <h5><strong>Fees</strong></h5>
                    <asp:Label ID="lblFeeDate" runat="server" Text="" Font-Size="Smaller" Font-Bold="true"></asp:Label>
                    <br />
                    <div style="text-align:right "><a href="FeeReport.aspx">View More...</a></div> 
                </div>
                <hr />
                <div style="text-align:center">
                 <h5><strong>Fees Details</strong></h5>
                    Month : <i class="fa fa-inr" style="width:12px;height:0px;line-height:0px;text-align:left"></i><%: ViewFeesMonthly()%> <br /> Session Total : <i class="fa fa-inr" style="width:12px;height:0px;line-height:0px;text-align:left"></i><%: ViewFees()%><br />
                    
                    </div>
                <div style="text-align:right "><a href="FeeReport.aspx">View More...</a></div> 
            </div>
        </div>
        <div class="col-md-3 widget">
            <div class="r3_counter_box">
                <i class="pull-left fa fa-book dollar1 fa-2x icon-rounded"></i>
                <div class="stats">
                    <h5><strong>Library</strong></h5>
                  <%--  <a href=""><%: ViewBooks()%></a>--%>
                    Total : <%: ViewBooks()%><br />
                    Issued : <%: ViewBooksIssued()%>
                </div>
                 <div style="text-align:right "><a href="LibrarySearch.aspx">View More...</a></div> 
                <hr />
                <div style="text-align:center "><h5><strong>Issued To </strong></h5>
                    Teacher : <%: ViewBooksIssuedTeacher()%> | Students : <%: ViewBooksIssuedStudent()%> </div>
                <br />
                 <div style="text-align:right "><a href="LibraryIssueReturnReport.aspx">View More...</a></div> 
            </div>
        </div>
        <div class="clearfix"></div>
    </div>

    <div class="content_bottom">
        <div class="col-md-8 span_3">
            <div class="bs-example1" data-example-id="contextual-table">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <i class="fa fa-bar-chart-o fa-fw"></i>Details Panel
                            <div class="pull-right">
                                <div class="btn-group">
                                    <asp:DropDownList ID="cboEvent" runat="server" Width="70px" Style="margin-right: 10px" AutoPostBack="True" CssClass="Dropdown">
                                    </asp:DropDownList>

                                    <asp:DropDownList ID="cboTime" runat="server" Width="70px" AutoPostBack="True" CssClass="Dropdown">
                                        <asp:ListItem>Today</asp:ListItem>
                                        <asp:ListItem>Weekly</asp:ListItem>
                                        <asp:ListItem>Monthly</asp:ListItem>
                                        <asp:ListItem>Yearly</asp:ListItem>
                                    </asp:DropDownList>

                                </div>
                            </div>
                            </div>
                            <!-- /.panel-heading -->
                            <div class="panel-body" style="overflow: scroll; height: 500px;">
                                <div class="row">
                                    <div width="95%">
                                        <div class="table-responsive" onload="alternate('myTableDiv')">
                                            <table class="table table-bordered table-hover table-striped" id="myTableDiv">
                                                <thead>
                                                    <tr style="text-align: center;">
                                                        <th>Date</th>
                                                        <th>Event Type</th>
                                                        <th>Details</th>
 
                                                        <th>User</th>

                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <% Dim DS As New Dataset %>
                                                    <% DS.Merge(CType(EventLog(), Data.DataSet).Copy ) %>
                                                    <% For i = 0 To DS.Tables(0).Rows.Count - 1%>

                                                    <tr class="alt" 
                                                        <% If(DS.Tables(0).Rows(i).Item("EventType").ToString.Contains("INSERT"))%>
                                                        style="background-color:#C4FCC1"
                                                        <% ElseIf (DS.Tables(0).Rows(i).Item("EventType").ToString.Contains("DELETE"))%>
                                                        style="background-color:#FCE2DC"
                                                        <% ElseIf (DS.Tables(0).Rows(i).Item("EventType").ToString.Contains("UPDATE"))%>
                                                        style="background-color:#FAF7AE"
                                                        <%End If%>
                                                        >                                                        
                                                        <td class="MyClass"><%: CType(DS.Tables(0).Rows(i).Item("logTime"), Date).ToString("dd/MM/yyyy")%></td>
                                                        <td class="MyClass">
                                                            <%: DS.Tables(0).Rows(i).Item("EventType").ToString()%>
                                                        </td >
                                                        <td class="MyClass"><%: DS.Tables(0).Rows(i).Item("Details").ToString()%></td>

                                                        <td class="MyClass"><%: DS.Tables(0).Rows(i).Item("UserName").ToString()%></td>
                                                        
                                                    </tr>

                                                    <% Next%>
                                                </tbody>

                                            </table>
                                        </div>
                                        <!-- /.table-responsive -->
                                    </div>
                                    <!-- /.col-lg-4 (nested) -->
                                    <div class="col-lg-8">
                                        <div id="morris-bar-chart"></div>
                                    </div>
                                    <!-- /.col-lg-8 (nested) -->
                                </div>
                                <!-- /.row -->
                            </div>
                            <!-- /.panel-body -->
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="col-md-4 span_4">
            <div class="col_2">
                <div class="box_1">
                   <div class="panel-heading" style="margin-top:-20px; text-align:center ">
                <h4 class="panel-title"><i class="fa fa-bell fa-fw"></i>Notifications Panel</h4>
            </div>
                    <%--<h4>&nbsp;&nbsp;&nbsp;<i class="fa fa-bell fa-fw"></i>Notifications Panel</h4>--%>
                </div>
                <div class="panel-body" style="overflow: hidden">
                    <div class="list-group">
                        <a href="StudentAnalysis.aspx" class="list-group-item" style="text-align: center">
                            <font size="3"> Student Analysis&nbsp; </font><i class="fa fa-forward fa-2x"></i>
                        </a>
                        <a href="#" class="list-group-item" style="text-align: center">
                            <i class="fa fa-graduation-cap fa-2x"></i><font size="3"> New Admission </font>
                        </a>
                        <a href="#" class="list-group-item">
                            <i class="fa fa-graduation-cap"></i>Today
                                    <span class="pull-right text-muted small"><em><%: NewAdmissionToday()%></em>
                                    </span>
                        </a>
                        <a href="#" class="list-group-item">
                            <i class="fa fa-graduation-cap"></i>Month
                                    <span class="pull-right text-muted small"><em><%: NewAdmission()%></em>
                                    </span>
                        </a>
                        <a href="#" class="list-group-item">
                            <i class="fa fa-graduation-cap"></i>Session
                                    <span class="pull-right text-muted small"><em><%: NewAdmissionSession()%></em>
                                    </span>
                        </a>
                        <a href="#" class="list-group-item" style="text-align: center">
                            <i class="fa fa-file fa-2x"></i><font size="3">  TC </font>
                        </a>
                        <a href="#" class="list-group-item">
                            <i class="fa fa-file"></i>Today
                                    <span class="pull-right text-muted small"><em><%: ViewTCToday()%></em>
                                    </span>
                        </a>
                        <a href="#" class="list-group-item">
                            <i class="fa fa-file"></i>Month
                                    <span class="pull-right text-muted small"><em><%: ViewTC()%></em>
                                    </span>
                        </a>
                        <a href="#" class="list-group-item">
                            <i class="fa fa-file"></i>Session
                                    <span class="pull-right text-muted small"><em><%: ViewTCSession()%></em>
                                    </span>
                        </a>
                        <a href="#" class="list-group-item" style="text-align: center">
                            <i class="fa fa-book fa-2x"></i><font size="3"> New Book </font>

                        </a>
                        <a href="#" class="list-group-item">
                            <i class="fa fa-book"></i>Monthly
                                    <span class="pull-right text-muted small"><em><%: NewBooks()%></em>
                                    </span>
                        </a>
                        
                        <div style="height:16px"></div>
                    </div>
                    <!-- /.list-group -->

                </div>
            </div>

        </div>
        <div class="clearfix"></div>
    </div>
</asp:Content>

