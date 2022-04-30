<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="StudentAnalysis.aspx.vb" Inherits="iDiary_V3.StudentAnalysis" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Heading" runat="server">
    Student Analysis
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        a {
            font-size:0.85em;
        }
    </style>
   <%-- <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />--%>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
                            </asp:ScriptManager>
    <link href="css/Graphs.css" rel="stylesheet" />
        <script src="js/index.js"></script>
   
    <div class="copy" style="margin-top: 2%; margin-bottom: 24px">
        <table style="width: 100%">
            <tr>
                <td>Admin/Sr/Reg No.</td>
                <td>
                    <asp:TextBox ID="txtSRNo" runat="server" CssClass="textbox"></asp:TextBox>
                    <asp:TextBox ID="txtID" runat="server" Height="16px" Visible="False" Width="25px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnFind" runat="server" class="btn btn-primary" Text="&gt;&gt;" />
                </td>
                <td>Student Name</td>
                <td>

                    <asp:TextBox ID="txtName" runat="server"
                        CssClass="textbox"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnNameSearch" runat="server" class="btn btn-primary" Text="&gt;&gt;" />
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" AutoGenerateSelectButton="True" CssClass="Grid"
                        CellPadding="2" DataSourceID="SqlDataSource2" ShowHeader="False" Width="98%" Font-Names="Garamond" Font-Size="10pt">
                        <Columns>
                            <asp:BoundField DataField="RegNo" HeaderText="RegNo" SortExpression="RegNo" />
                            <asp:BoundField DataField="SName" HeaderText="SName" SortExpression="SName" />
                            <asp:BoundField DataField="ClassName" HeaderText="ClassName" SortExpression="ClassName" />
                            <asp:BoundField DataField="SecName" HeaderText="SecName" SortExpression="SecName" />
                        </Columns>

                    </asp:GridView>
                </td>
            </tr>
        </table>
        <br />
    </div>
    
        
    <div class="content_bottom">
        <div class="col-md-8 span_3">
            <div class="bs-example1" data-example-id="contextual-table">
                <div class="panel-heading" style="margin-top: -40px">
                    <h4 class="panel-title"><i class="fa fa-bell fa-fw"></i>Student Attendance Details</h4>
                </div>
                <div id="contenitore">
                    <div class="left" style="margin-left: 40px">
                        <div id="grafico">

                            <div class="riga" style="top: 20%">
                                <div>24</div>
                            </div>
                            <div class="riga" style="top: 40%">
                                <div>18</div>
                            </div>
                            <div class="riga" style="top: 60%">
                                <div>12</div>
                            </div>
                            <div class="riga" style="top: 80%">
                                <div>6</div>
                            </div>
                            <div class="riga" style="top: 100%">
                                <div>0</div>
                            </div>

                            <div id="col0" style="left: 0; background-color: #37C464;" class="column"></div>
                            <div id="col1" style="left: 0; background-color: red;" class="column2"></div>
                            <div id="col2" style="left: 6%; background-color: #37C464;" class="column"></div>
                            <div id="col3" style="left: 6%; background-color: red;" class="column2"></div>
                            <div id="col4" style="left: 12%; background-color: #37C464;" class="column"></div>
                            <div id="col5" style="left: 12%; background-color: red;" class="column2"></div>
                            <div id="col6" style="left: 18%; background-color: #37C464;" class="column"></div>
                            <div id="col7" style="left: 18%; background-color: red;" class="column2"></div>
                            <div id="col8" style="left: 24%; background-color: #37C464;" class="column"></div>
                            <div id="col9" style="left: 24%; background-color: red;" class="column2"></div>
                            <div id="col10" style="left: 30%; background-color: #37C464;" class="column"></div>
                            <div id="col11" style="left: 30%; background-color: red;" class="column2"></div>
                            <div id="col12" style="left: 36%; background-color: #37C464;" class="column"></div>
                            <div id="col13" style="left: 36%; background-color: red;" class="column2"></div>
                            <div id="col14" style="left: 42%; background-color: #37C464;" class="column"></div>
                            <div id="col15" style="left: 42%; background-color: red;" class="column2"></div>
                            <div id="col16" style="left: 48%; background-color: #37C464;" class="column"></div>
                            <div id="col17" style="left: 48%; background-color: red;" class="column2"></div>
                            <div id="col18" style="left: 54%; background-color: #37C464;" class="column"></div>
                            <div id="col19" style="left: 54%; background-color: red;" class="column2"></div>
                            <div id="col20" style="left: 60%; background-color: #37C464;" class="column"></div>
                            <div id="col21" style="left: 60%; background-color: red;" class="column2"></div>
                            <div id="col22" style="left: 66%; background-color: #37C464;" class="column"></div>
                            <div id="col23" style="left: 66%; background-color: red;" class="column2"></div>

                            <div class="riga1" style="top: 100%; left: 2%">
                                <div>April</div>
                            </div>
                            <div class="riga1" style="top: 100%; left: 8%">
                                <div>May</div>
                            </div>
                            <div class="riga1" style="top: 100%; left: 14%">
                                <div>June</div>
                            </div>
                            <div class="riga1" style="top: 100%; left: 20%">
                                <div>July</div>
                            </div>
                            <div class="riga1" style="top: 100%; left: 26%">
                                <div>Aug</div>
                            </div>
                            <div class="riga1" style="top: 100%; left: 32%">
                                <div>Sept</div>
                            </div>
                            <div class="riga1" style="top: 100%; left: 38%">
                                <div>Oct</div>
                            </div>
                            <div class="riga1" style="top: 100%; left: 44%">
                                <div>Nov</div>
                            </div>
                            <div class="riga1" style="top: 100%; left: 50%">
                                <div>Dec</div>
                            </div>
                            <div class="riga1" style="top: 100%; left: 56%">
                                <div>Jan</div>
                            </div>
                            <div class="riga1" style="top: 100%; left: 62%">
                                <div>Feb</div>
                            </div>
                            <div class="riga1" style="top: 100%; left: 68%">
                                <div>March</div>
                            </div>
                        </div>
                    </div>
                    <div class="left" runat="server" id="MyDiv">

                        <table id="MyGraph" style="margin-top: 30px; margin-left: 30px">

                            <tr>
                                <td>For Total Days</td>
                                <td>
                                    <asp:Label ID="lblTM4" runat="server" Text=""></asp:Label></td>
                                <td style="background: #37C464; width: 70px"></td>
                            </tr>
                            <tr>
                                <td>For Days Present</td>
                                <td style="visibility: hidden">
                                    <asp:Label ID="lblM4" runat="server" Text=""></asp:Label></td>
                                <td style="background: red; width: 60px"></td>
                            </tr>
                            <tr>
                                <td>Green</td>
                                <td>
                                    <asp:Label ID="lblTM5" runat="server" Text=""></asp:Label></td>

                            </tr>
                            <tr>
                                <td>Red</td>
                                <td>
                                    <asp:Label ID="lblM5" runat="server" Text=""></asp:Label></td>

                            </tr>
                            <tr>
                                <td>Total Days</td>
                                <td style="visibility: hidden">
                                    <asp:Label ID="lblTM6" runat="server" Text=""></asp:Label></td>
                                <td style="background: #37C464; width: 20px"></td>
                            </tr>
                            <tr>
                                <td>Red</td>
                                <td>
                                    <asp:Label ID="lblM6" runat="server" Text=""></asp:Label></td>

                            </tr>
                            <tr>
                                <td>Green</td>
                                <td>
                                    <asp:Label ID="lblTM7" runat="server" Text=""></asp:Label></td>

                            </tr>
                            <tr>
                                <td>Red</td>
                                <td>
                                    <asp:Label ID="lblM7" runat="server" Text=""></asp:Label></td>

                            </tr>
                            <tr>
                                <td>Total Days</td>
                                <td style="visibility: hidden">
                                    <asp:Label ID="lblTM8" runat="server" Text=""></asp:Label></td>
                                <td style="background: #37C464; width: 20px"></td>
                            </tr>
                            <tr>
                                <td>Red</td>
                                <td>
                                    <asp:Label ID="lblM8" runat="server" Text=""></asp:Label></td>

                            </tr>
                            <tr>
                                <td>Green</td>
                                <td>
                                    <asp:Label ID="lblTM9" runat="server" Text=""></asp:Label></td>

                            </tr>
                            <tr>
                                <td>Red</td>
                                <td>
                                    <asp:Label ID="lblM9" runat="server" Text=""></asp:Label></td>

                            </tr>
                            <tr>
                                <td>Total Days</td>
                                <td style="visibility: hidden">
                                    <asp:Label ID="lblTM10" runat="server" Text=""></asp:Label></td>
                                <td style="background: #37C464; width: 20px"></td>
                            </tr>
                            <tr>
                                <td>Red</td>
                                <td>
                                    <asp:Label ID="lblM10" runat="server" Text=""></asp:Label></td>

                            </tr>
                            <tr>
                                <td>Green</td>
                                <td>
                                    <asp:Label ID="lblTM11" runat="server" Text=""></asp:Label></td>

                            </tr>
                            <tr>
                                <td>Red</td>
                                <td>
                                    <asp:Label ID="lblM11" runat="server" Text=""></asp:Label></td>

                            </tr>
                            <tr>
                                <td>Total Days</td>
                                <td style="visibility: hidden">
                                    <asp:Label ID="lblTM12" runat="server" Text=""></asp:Label></td>
                                <td style="background: #37C464; width: 20px"></td>
                            </tr>
                            <tr>
                                <td>Red</td>
                                <td>
                                    <asp:Label ID="lblM12" runat="server" Text=""></asp:Label></td>

                            </tr>
                            <tr>
                                <td>Green</td>
                                <td>
                                    <asp:Label ID="lblTM1" runat="server" Text=""></asp:Label></td>

                            </tr>
                            <tr>
                                <td>Red</td>
                                <td>
                                    <asp:Label ID="lblM1" runat="server" Text=""></asp:Label></td>

                            </tr>
                            <tr>
                                <td>Total Days</td>
                                <td style="visibility: hidden">
                                    <asp:Label ID="lblTM2" runat="server" Text=""></asp:Label></td>
                                <td style="background: #37C464; width: 20px"></td>
                            </tr>
                            <tr>
                                <td>Red</td>
                                <td>
                                    <asp:Label ID="lblM2" runat="server" Text=""></asp:Label></td>

                            </tr>
                            <tr>
                                <td>Green</td>
                                <td>
                                    <asp:Label ID="lblTM3" runat="server" Text=""></asp:Label></td>

                            </tr>
                            <tr>
                                <td>Red</td>
                                <td>
                                    <asp:Label ID="lblM3" runat="server" Text=""></asp:Label></td>

                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-4 span_4">
            <div class="col_2">
                <div class="box_1">
                    <div class="panel-heading" style="margin-top: -30px">
                        <h4 class="panel-title"><i class="fa fa-bell fa-fw"></i>Student Details</h4>
                    </div>
                    <div class="panel-body" style="overflow: hidden">
                        <div class="list-group">

                            <a href="#" class="list-group-item">Name :
                                    <span class="pull-right text-muted small"><em>
                                        <asp:Label ID="lblSName" runat="server" Text="" Style="font-weight: 700"></asp:Label></em>
                                    </span>
                            </a>
                            <a href="#" class="list-group-item">Class : 
                                    <span class="pull-right text-muted small"><em>
                                        <asp:Label ID="lblClassSec" runat="server" Text="" Style="font-weight: 700"></asp:Label></em>
                                    </span>

                            </a>
                            <%--<a href="#" class="list-group-item">Roll No : 
                                    <span class="pull-right text-muted small"><em>
                                        <asp:Label ID="lblRollNo" runat="server" Text="" Style="font-weight: 700"></asp:Label></em>
                                    </span>
                            </a>--%>
                            <a href="#" class="list-group-item">House : 
                                    <span class="pull-right text-muted small"><em>
                                        <asp:Label ID="lblHouse" runat="server" Text="" Style="font-weight: 700"></asp:Label></em>
                                    </span>
                            </a>
                            <a href="#" class="list-group-item">Father Name : 
                                    <span class="pull-right text-muted small"><em>
                                        <asp:Label ID="lblFatherName" runat="server" Text="" Style="font-weight: 700"></asp:Label></em>
                                    </span>
                            </a>
                         <%--   <a href="#" class="list-group-item">Mother Name :
                                    <span class="pull-right text-muted small"><em>
                                        <asp:Label ID="lblMotherName" runat="server" Text="" Style="font-weight: 700"></asp:Label></em>
                                    </span>
                            </a>--%>
                            <a href="#" class="list-group-item">Admission Date : 
                                    <span class="pull-right text-muted small"><em>
                                        <asp:Label ID="lblAdmissionDate" runat="server" Text="" Style="font-weight: 700"></asp:Label></em>
                                    </span>
                            </a>
                           <%-- <a href="#" class="list-group-item">Fee Group : 
                                    <span class="pull-right text-muted small"><em>
                                        <asp:Label ID="lblFeeGroup" runat="server" Text="" Style="font-weight: 700"></asp:Label></em>
                                    </span>
                            </a>--%>
                            <a href="#" class="list-group-item">Fee Book No : 
                                    <span class="pull-right text-muted small"><em>
                                        <asp:Label ID="lblFeeBookNo" runat="server" Text="" Style="font-weight: 700"> </asp:Label></em>
                                    </span>
                            </a>
                            <a href="#" class="list-group-item">Date of birth : 
                                    <span class="pull-right text-muted small"><em>
                                        <asp:Label ID="lblDob" runat="server" Text="" Style="font-weight: 700"></asp:Label></em>
                                    </span>
                            </a>
                            <a href="#" class="list-group-item" style="height: 73px">Address :
                                    <span class="pull-right text-muted small"><em>
                                        <asp:Label ID="lblAddress" runat="server" Text="" Style="font-weight: 700"></asp:Label></em>
                                    </span>
                            </a>
                            <a href="#" class="list-group-item">Mobile :
                                    <span class="pull-right text-muted small"><em>
                                        <asp:Label ID="lblMob" runat="server" Text="" Style="font-weight: 700"></asp:Label></em>
                                    </span>
                            </a>
                        </div>
                        <!-- /.list-group -->
                    </div>

                    <div class="clearfix"></div>
                </div>
            </div>

        </div>
        <div class="clearfix"></div>
    </div>

<div class="content_bottom">
    <div class="content_bottom" style="">
            <div class="col-md-6 span_3">
                <div class="bs-example1" data-example-id="contextual-table" style="margin-top:-5%">
                     <div id="contenitore2">
                <a href="#" class="list-group-item" style="text-align:center "><i class="fa fa-bell fa-fw"></i>Student Fees Details
                        </a>
                <div class="left" style="margin-left: 40px" runat="server" id="TermNo">
                    
                </div>
                <div class="left" runat="server" id="Division">
                   
                </div>
            </div>
                </div>
            </div>

            <div class="col-md-6 span_4">
                <div class="bs-example1" data-example-id="contextual-table" style="margin-top:-5%">
                     <div id="Div1">
                <a href="#" class="list-group-item" style="text-align:center "><i class="fa fa-bell fa-fw"></i>Student Notes
                        </a>
                         <br />
                      <asp:GridView ID="GridView1" runat="server" DataSourceID="StudentNotepad" DataKeyNames="StudentNotesID"  CssClass="Grid" Width="98%" AutoGenerateColumns="False">
                 
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" ItemStyle-Width="20px" Visible="false" >
<ItemStyle Width="30px"></ItemStyle>
                        </asp:CommandField>
                        <asp:BoundField DataField="StudentNotesID" Visible="false"  ItemStyle-Width="10px" HeaderText="StudentNotesID" SortExpression="StudentNotesID" InsertVisible="False" ReadOnly="True"  >
<ItemStyle Width="10px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="EntryDate" ItemStyle-Width="40px" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Date" SortExpression="EntryDate" >
<ItemStyle Width="50px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="Comments" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"  ItemStyle-Width="800px" HeaderText="Comments" SortExpression="Comments" >
               
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
<ItemStyle Width="600px" HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                        </asp:BoundField>
               
                        <asp:TemplateField ItemStyle-Width="60px">
                                                 <ItemTemplate>
                                                    <asp:ImageButton ImageUrl="~/images/downloadicon.png"  ID="imagebutton2"   runat="server" />
                                                </ItemTemplate>
                                                 <ItemStyle Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                        
                       <%-- <asp:ImageField DataImageUrlField="NoteDocPath" NullImageUrl="~/images/images.png"  HeaderText="Notes Name"></asp:ImageField>--%>
                       <%-- <asp:HyperLinkField DataTextField="NoteDocPath" DataNavigateUrlFields="NoteDocPath" Target="_blank"   DataNavigateUrlFormatString="" HeaderText="Notes Name" SortExpression="NoteDocPath" />--%>
                    </Columns>
                   
                </asp:GridView>
                <div class="left" style="margin-left: 40px" runat="server" id="Divstudentnotes">
                      <div class="left" runat="server" id="Div3">
                     </div>
                </div>
              
                <asp:SqlDataSource ID="StudentNotepad" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT [StudentNotesID], [EntryDate], [Comments], [NoteDocPath] FROM [StudentNotes]"></asp:SqlDataSource>

                </div>
            </div>
                </div>
            </div>

            <div class="clearfix"></div>
        </div>
   
    <!-- Graph Starts For Fees-->
         <div class="col-lg-8" style="margin-top:-39%">
           
            <!-- End Graph -->
        </div>
    <!-- End Fee Graph -->
     <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT RegNo, SName, ClassName, SecName FROM vw_Student WHERE SID<0">
                 
                </asp:SqlDataSource>
</asp:Content>
