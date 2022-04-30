<%@ Page Language="vb" AutoEventWireup="false" EnableEventValidation="false"  CodeBehind="TT1.aspx.vb" Inherits="iDiary_V3.TT1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
       <link href="zx/bootstrap.css" rel="stylesheet" type="text/css" />
    <script src="zx/jquery-1.9.1.min.js"></script>
    <script src="Zx/bootstrap.js" type="text/javascript"></script>
    <div>

        <asp:ScriptManager ID="ScriptManager1" runat="server" />

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">

            <ContentTemplate>

                <div>
                    <asp:Panel ID="PanelClass" runat="server">
                    <table border="0" cellpadding="1" cellspacing="1" width="100%">

                        <tr>
                            <td style="width: 15%">
                                <strong>Class</strong></td>
                            <td style="width: 15%">

                                <asp:DropDownList ID="cboClass" runat="server" AutoPostBack="True" CssClass="Dropdown">
                                </asp:DropDownList>

                            </td>
                            <td style="width: 15%">
                                <strong>Section</strong></td>
                            <td style="width: 15%">
                                <asp:DropDownList ID="cboSection" runat="server" CssClass="Dropdown">
                                </asp:DropDownList>
                            </td>
                              <td style="width: 15%">
                                <asp:Button ID="btnReport" runat="server" Style="margin-left: 0px" Text="Generate Report" CssClass="hvr-glow" />
                                
                            </td>
                              <td style="width: 15%">
                                  </td> 
                              <td style="width: 10%">
                                  <asp:Label ID="lblCssID" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="lblType" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="lblEmpID" runat="server" Visible="False"></asp:Label>
                                  </td> 

                        </tr>
                        </table>
                        </asp:Panel> 
                    <asp:Panel ID="PanelEmployee" runat="server">
                     <table border="0" cellpadding="1" cellspacing="1" width="100%">
                        <tr>
                            <td style="width: 15%"><strong>EmployeeCode</strong></td>
                            <td style="width: 15%">
                                <asp:TextBox ID="txtEmpCode" CssClass="textbox" runat="server"></asp:TextBox>
                            </td>
                            <td style="width: 15%">
                                <strong>Employee Name</strong></td>
                            <td style="width: 15%">
                                <asp:TextBox ID="txtEmpName" runat="server" CssClass="textbox" Enabled="False"></asp:TextBox>
                            </td>
                          
                             <td style="width: 15%">
                                  <asp:Button ID="btnEmp" runat="server" Text="Generate" CssClass="hvr-glow" />
                                  </td> 
                             <td style="width: 15%">
                                  </td> 
                             <td style="width: 10%">
                                  </td> 
                        </tr>
                         <tr>
                             <td style="width: 15%">
                                 <asp:Label ID="lblEmpLoad" runat="server" style="font-weight: 700"></asp:Label>
                             </td>
                             <td style="width: 15%">&nbsp;</td>
                             <td style="width: 15%">&nbsp;</td>
                             <td style="width: 15%">&nbsp;</td>
                             <td style="width: 15%">&nbsp;</td>
                             <td style="width: 15%">&nbsp;</td>
                             <td style="width: 10%">&nbsp;</td>
                         </tr>
                         </table> 
                        </asp:Panel> 
                          <table border="0" cellpadding="1" cellspacing="1" width="100%">
                        <tr>
                            <td style="width: 15%">&nbsp;</td>
                            <td colspan="4">
                                <asp:Label ID="lblStatus" runat="server" style="font-weight: 700"></asp:Label>
                            </td>
                            <td style="width: 15%">
                                <strong>
                                    <asp:Button ID="btnPrint" runat="server" Text="Print" Width="82px" Visible="False" />
                                </strong></td>
                            <td style="width: 10%">
                                <asp:Button ID="btnExcel" runat="server" Text="Export to Excel" Width="125px" Visible="False" />
                            </td>

                        </tr>
                    </table>
                    <div style="height: 500px; width: 100%; text-align:left; vertical-align:top; overflow: auto;">
                        <asp:GridView ID="GridView1" runat="server"
                            Width="940px" HorizontalAlign="Center"
                            OnRowCommand="GridView1_RowCommand"
                            AutoGenerateColumns="False"
                            DataKeyNames="ttDayID"
                            CssClass="table table-hover table-striped">

                            <Columns>
                                <asp:ButtonField CommandName="ColumnClick" Visible="false" />
                                <asp:BoundField DataField="ttDayID" HeaderText="ID" Visible="false" />
                                <asp:BoundField DataField="ttDayName" HeaderText="Day" />

                                <%-- <asp:BoundField DataField="AttDate" HeaderText="Date" />

	            <asp:BoundField DataField="Att" HeaderText="Att" />

	            <asp:BoundField DataField="LeaveName" HeaderText="LeaveName" />--%>
                            </Columns>

                        </asp:GridView>

                        <br />

                    </div>

                </div>

            </ContentTemplate>
            <%-- <Triggers>

	                           <asp:AsyncPostBackTrigger ControlID="btnExcel"  EventName="btnExcel_Click" />
                <asp:AsyncPostBackTrigger ControlID="btnPrint"  EventName="btnPrint_Click" /> 
	           </Triggers>--%>
        </asp:UpdatePanel>

        <asp:UpdateProgress ID="UpdateProgress1" runat="server">

            <ProgressTemplate>

                <img src="" alt="Loading.. Please wait!" />

            </ProgressTemplate>

        </asp:UpdateProgress>

        <div id="currentdetail" class="modal hide fade"
            tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
            aria-hidden="true">

            <div class="modal-header">

                <button type="button" class="close" data-dismiss="modal"
                    aria-hidden="true">
                    ×</button>

                <%--<h3 id="myModalLabel">Detailed View</h3>--%>
            </div>

            <div class="modal-body">

                <asp:UpdatePanel ID="UpdatePanel2" runat="server">

                    <ContentTemplate>

                                            <%--                    <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>--%>
                        <asp:Button ID="btnRelease" runat="server" Text="Free this Period" />
                        <asp:Label ID="lblDayID" runat="server"></asp:Label>
                                            &nbsp;&nbsp;&nbsp;
                                            <asp:Label ID="lblPeriodID" runat="server"></asp:Label>
                                            &nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lblRow" runat="server"></asp:Label>
                                            &nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lblColumn" runat="server"></asp:Label>
                        <h5>Selected Period :
                            <asp:Label ID="lblPeriod" runat="server" Text=""></asp:Label></h5>
                        <h5>Selected Day :
                            <asp:Label ID="lblDay" runat="server" Text=""></asp:Label></h5>
                        <h5>Subject :
                            <asp:Label ID="lblSubject" runat="server" Text=""></asp:Label></h5>
                        <h5>Teacher :
                            <asp:Label ID="lblTeacher" runat="server" text="">
                            </asp:Label></h5>
                                            <p>
                                                <asp:Button ID="btnGet" runat="server" Text="Get free Period" />
                                            </p>
                                            <p>
                                                <strong>Select&nbsp; Subject&nbsp;&nbsp;&nbsp;&nbsp; </strong>
                                                <asp:DropDownList ID="cboFreeSubject" CssClass="Dropdown" runat="server">
                                                </asp:DropDownList>
                                                &nbsp;&nbsp;&nbsp;
                                                <asp:Button ID="btnFill" runat="server" Text="Fill this Period" />
                                            </p>
                                            <p>
                                               
                                            </p>
                                            <p>
                                                &nbsp;</p>
                    </ContentTemplate>

                    <Triggers>

                        <asp:AsyncPostBackTrigger ControlID="GridView1" EventName="RowCommand" />
                        <asp:PostBackTrigger ControlID="btnFill" />
                        <asp:PostBackTrigger ControlID="btnRelease" />
                    </Triggers>

                </asp:UpdatePanel>

                <div class="modal-footer">

                    <%-- <button class="btn btn-info" data-dismiss="modal"

	                            aria-hidden="true">Close</button>--%>
                    <asp:Button ID="btnSave" class="btn btn-info" runat="server" Text="Save" />
                </div>

            </div>

        </div>

    </div>
    <asp:DropDownList ID="ddlDate" runat="server" Visible="False">
    </asp:DropDownList>
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    </div>
    </form>
</body>
</html>
