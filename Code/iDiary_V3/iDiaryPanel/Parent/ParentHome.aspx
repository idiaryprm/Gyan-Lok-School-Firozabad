<%@ Page Language="VB" MasterPageFile="~/IdiaryPanel/Parent/ParentMaster.master" AutoEventWireup="false" Inherits="iDiary_V3.Parent_ParentHome" title="Parent Dashboad" Codebehind="ParentHome.aspx.vb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Heading" runat="server">
    Dashboard
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 
    <br />
    <script type="text/javascript">
        function imgError(image) {
            image.onerror = "";
            image.src = "../../images/StudentDummy.jpg";
            return true;
        }
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="col_3" style="width:66.5%; float:left; margin-right:0px">
        <div class="col-md-3 widget widget1" style="width:48.5%">
            <div class="r3_counter_box">
                <i class="pull-left fa fa-list-alt icon-rounded"></i>
                <div>
                    <h3>Circulars</h3>
                    <a href="ViewCirculars.aspx"><asp:Label ID="lblUnreadCircular" runat="server" Text=""></asp:Label></a>
                    <br />
                    <br />
                    <br />
                </div> 
            </div>
        </div>
         <div class="col-md-3 widget widget1" style="width:48.5%">
            <div class="r3_counter_box">
                <div style="width:32%; float:left">  <span class="fa-stack fa-lg">
                <i class="pull-left fa fa-circle-o fa-stack-2x user1 icon-rounded"></i>
                <i class="fa fa-font fa-stack-1x" style="color:white"></i></span></div>
               
                <%--<i class="pull-left fa fa-font user1 icon-rounded"></i>--%>
                <div style="margin-left:10%">
                    <h3>Assignments</h3>
                    <a href="ViewAssignments.aspx"><asp:Label ID="lblUnreadAssignments" runat="server" Text=""></asp:Label></a>
                    <br />
                    <br />
                    <br />
                </div>
            </div>
        </div>
        
        <div class="col-md-3 widget widget1" style="width:48.5%;margin-top: 3%;">
            <div class="r3_counter_box">
                <i class="pull-left fa fa-calendar user2 icon-rounded"></i>
                <div>
                    <h3>Upcoming Activities</h3>
                    <a href="UpcomingActivity.aspx"><asp:Label ID="lblActivities" runat="server" Text=""></asp:Label></a>
                    <br />
                    <br />
                    <br />
                    <br />
                </div>
            </div>
        </div>
         <div class="col-md-3 widget widget1" style="width:48.5%;margin-top: 3%;">
            <div class="r3_counter_box">
                <i class="pull-left fa fa-inr dollar1 icon-rounded"></i>
                <div >
                    <h3>Fee Details</h3>
                    <a href="FeeHistory.aspx"><asp:Label ID="lblFee" runat="server" Text=""></asp:Label></a>
                    <br />
                    <br />
                    <br />
                </div>
            </div>
        </div>

        <div class="col-md-3 widget widget1" style="width:48.5%;margin-top: 3%;">
            <div class="r3_counter_box">
                <i class="pull-left fa fa-thumbs-up icon-rounded"></i>
                <div>
                    <h3>Attendance</h3>
                    <a href="ViewAttendance.aspx"><asp:Label ID="lblAttendance" runat="server" Text=""></asp:Label></a>
                    <br />
                    <br />
                    <br />
                </div>
            </div>
        </div>

         <div class="col-md-3 widget widget1" style="width:48.5%;margin-top: 3%;">
            <div class="r3_counter_box">
                <i class="pull-left fa fa-pencil user1 icon-rounded"></i>
                <div >
                    <h3>Apply Leave</h3>
                    <a href="ApplyLeave.aspx">Click Here !</a>
                    <br />
                    <br />
                    <br />
                    <br />
                </div>
            </div>
        </div>
        <div class="col-md-3 widget widget1" style="width:98.5%;margin-top: 3%;">
            <div class="r3_counter_box">
                <i class="pull-left fa fa-calendar dollar1 icon-rounded"></i>
                <div>
                    <h3>Academic Calendar</h3>
                    <table style="width:80%;float:right">
                        <tr>
                            <td style="width: 110px"><asp:Label ID="lblDateFrom" runat="server" Text=""></asp:Label></td>
                           <%-- <td style="width: 101px"><a href="ViewAcademicCalender.aspx"><asp:Label ID="lblDateTo" runat="server" Text=""></asp:Label></a></td>--%>
                            <%--<td><a href="ViewAcademicCalender.aspx"><asp:Label ID="lblACal" runat="server" Text=""></asp:Label></a></td>--%>
                            
                        </tr>
                        <tr>
                            
                           <%-- <td></td>
                            <td></td>--%>
                            <td style="text-align:right;vertical-align:bottom;"><a href="ViewAcademicCalender.aspx">View more...</a></td>
                        </tr>
                    </table>
                </div>
                <br /><br /><br /><br />
            </div>
        </div>
        <div class="clearfix"></div>
    </div>
    
    <div class="content_bottom">

        <div class="col-md-4 span_4">
            <div class="col_2">
                <div style="text-align: center">
                    <asp:Image ID="imgStudent" runat="server" Height="190px" Width="180px" CssClass="textbox" onerror="imgError(this);"/>
                    
                    <br />
                    
                    <br />
                    <h4 style="color: black; font-size: 1.25em; margin: 0px 0px 0px 0px">
                        <asp:Label ID="lblSname" runat="server" Text=""></asp:Label></h4>
                    <h4 style="color: black; font-size: 1em; margin: 4px 0px 0px 0px">
                        <asp:Label ID="lblFname" runat="server" Text=""></asp:Label></h4>

                </div>

                    
                <div style="margin-top:15px">
                        <table class="table" style="font-size:large;font-weight:600">
                        <tr>
                            <td class="one" style="width: 95px">Reg No </td>
                            <td class="text-center" style="width: 30px">:</td>
                            <td><asp:Label ID="lblRegNo" runat="server" Text=""></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="one" style="width: 95px">Class</td>
                            <td class="text-center" style="width: 30px">:</td>
                            <td>
                    
                    <asp:Label ID="lblClass" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="one" style="width: 95px">Phone No.</td>
                            <td class="text-center" style="width: 30px">:</td>
                            <td>
                        <asp:Label ID="lblMob" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="one" style="width: 95px">Address</td>
                            <td class="text-center" style="width: 30px">:</td>
                            <td>
                        <asp:Label ID="lblAddress" runat="server" Text=""></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="one" style="width: 95px">Email</td>
                            <td class="text-center" style="width: 30px">&nbsp;: </td>
                            <td>
                        <asp:Label ID="lblEmail" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                            <tr>
                            <td class="one" style="width: 95px">Date Of Birth</td>
                            <td class="text-center" style="width: 30px">&nbsp;: </td>
                            <td><asp:Label ID="lblDOB" runat="server" Text=""></asp:Label>
                                </td>
                        </tr>
                        <tr>
                            <td class="one" style="width: 95px">&nbsp;</td>
                            <td class="text-center" style="width: 30px">&nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="one" colspan="3"><asp:FileUpload ID="myFile" runat="server" CssClass="FileUpload" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-primary" Text="Upload" /></td>
                        </tr>
                    </table>
                    
                    <div style="margin-top:32%"></div>
                </div>
            </div>
        </div>
        <div class="clearfix"></div>
    </div>
    <div class="copy">
       <div style="margin-left:15px;text-align:left">
           <h3 class="text-center">Thought Of The Day</h3>
           <p class="text-center">
               <strong><span style="font-size: large">"</span></strong><asp:Label ID="lblThought" runat="server" Text="" Font-Size="Large" style="font-weight: 700"></asp:Label><strong><span style="font-size: large">"</span></strong></p>
       </div>
    </div>
    <br />
</asp:Content>

