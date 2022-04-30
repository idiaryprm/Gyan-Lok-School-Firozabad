<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="BookMaster.aspx.vb" Inherits="iDiary_V3.BookMaster" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Heading" runat="server">
    Add / Modify Book Entries
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <link href="css/jquery-ui.css" rel="stylesheet" />
    <script src="js/jquery-ui.js"></script>

    <script>
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 46 || charCode > 57)) {
                alert("Numeric Only")
                return false;
            }
            return true;
        }
    </script>

  <script>
      $(function () {
          $("#accordion").accordion({
              heightStyle: "content"


          });
      });
  </script>
    
    <div class="col_3" style="margin-top: 20px;" id="panelEnquiryNo" runat="server">
        <div class="col-md-3 widget widget1" style="width: 100%">
            <div class="r3_counter_box">
              <%--  <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />--%>
                <table class="table">

                    <tr>
                        <td valign="top" align="left" style="width: 12%">Accession No<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                        </td>
                        <td valign="top" align="left" style="width: 23%">
                            <asp:TextBox ID="txtBookType" runat="server" CssClass="textbox" Enabled="False" ReadOnly="True" Width="16px">B</asp:TextBox>
                            <asp:TextBox ID="txtAccNo" runat="server" CssClass="textbox" Style="width: 80px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnNext" runat="server" Text=">>" CssClass="btn btn-sm btn-primary" />
                        </td>
                        <td valign="top" align="left" style="width: 11%">Book Code No.</td>
                        <td valign="top" align="left" style="width: 17%">
                            <asp:TextBox ID="txtBookCodeNo" runat="server" CssClass="textbox" Width="150px"></asp:TextBox>
                           
                        </td>
                        <td valign="top" align="left" style="width: 14%">Author Name(s)
                <asp:ImageButton ID="btnRefreshAuthors" runat="server" ImageUrl="~/Images/Refresh.jpg" />
                        </td>
                        <td width="30%" valign="top" align="left">
                            <asp:DropDownList ID="cboAuthor" runat="server" CssClass="Dropdown" Width="135px">
                            </asp:DropDownList>
                            &nbsp;<asp:Button ID="btnAddAuthor" runat="server" Text="+" CssClass="btn btn-sm btn-primary" Width="35px" />
                            &nbsp;
                <asp:Button ID="btnRemoveAuthor" runat="server"  Text="X" CssClass="btn btn-sm btn-primary" Width="35px" />
                        </td>
                    </tr>


                    <tr>
                        <td valign="top" align="left" style="width: 12%">Title</td>
                        <td valign="top" align="left" style="width: 23%">
                            <asp:TextBox ID="txtTitle" runat="server" CssClass="textbox"></asp:TextBox>
                        </td>
                        <td valign="top" align="left" style="width: 11%">Publisher
                        </td>
                        <td valign="top" align="left" style="width: 17%">
                            <asp:DropDownList ID="cboPub" runat="server" CssClass="Dropdown">
                            </asp:DropDownList>
                            <asp:ImageButton ID="btnRefreshPublisher" runat="server"
                                ImageUrl="~/Images/Refresh.jpg" Style="height: 14px" />
                        </td>
                        <td valign="top" align="left" colspan="2" rowspan="5" style="text-align: right">&nbsp;<asp:ListBox ID="lstAuthors" runat="server" CssClass="list" Height="184px" Width="235px"></asp:ListBox>
                        </td>
                    </tr>


                    <tr>
                        <td valign="top" align="left" style="width: 12%">Book Category
                        </td>
                        <td valign="top" align="left" style="margin-left: 40px; width: 23%;">
                            <asp:DropDownList ID="cboBookCat" runat="server" CssClass="Dropdown">
                            </asp:DropDownList>
                            &nbsp;
                <asp:ImageButton ID="btnRefreshBookCategory" runat="server"
                    ImageUrl="~/Images/Refresh.jpg" Style="height: 14px" />
                        </td>
                        <td valign="top" align="left" style="width: 11%">No. of Books</td>
                        <td valign="top" align="left" style="width: 17%">
                            <asp:TextBox ID="txtNoOfBooks" runat="server" CssClass="textbox" onkeypress="return isNumber(event)"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td valign="top" align="left" style="width: 12%">Rack No
                        </td>
                        <td valign="top" align="left" style="margin-left: 40px; width: 23%;">
                            <asp:DropDownList ID="cboRack" runat="server" CssClass="Dropdown">
                            </asp:DropDownList>
                            &nbsp;
                <asp:ImageButton ID="btnRefreshRack" runat="server"
                    ImageUrl="~/Images/Refresh.jpg" Style="height: 14px" />
                        </td>
                        <td valign="top" align="left" style="width: 11%">No. of Pages</td>
                        <td valign="top" align="left" style="width: 17%">
                            <asp:TextBox ID="txtPages" runat="server" CssClass="textbox" onkeypress="return isNumber(event)"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td valign="top" align="left" style="width: 12%">Status
                        </td>
                        <td valign="top" align="left" style="margin-left: 40px; width: 23%;">
                            <asp:DropDownList ID="cboStatus" runat="server" CssClass="Dropdown">
                            </asp:DropDownList>
                            &nbsp;
                <asp:ImageButton ID="btnRefreshStatus" runat="server"
                    ImageUrl="~/Images/Refresh.jpg" Style="height: 14px; width: 17px" />
                        </td>
                        <td valign="top" align="left" style="width: 11%">Edition</td>
                        <td valign="top" align="left" style="width: 17%">
                            <asp:TextBox ID="txtEdition" runat="server" CssClass="textbox"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td valign="top" align="left" style="width: 12%">Is Issuable ?</td>
                        <td valign="top" align="left" style="margin-left: 40px; width: 23%;">

                            <asp:DropDownList ID="cboIssued" runat="server" CssClass="Dropdown">
                                <asp:ListItem>No</asp:ListItem>
                                <asp:ListItem>Yes</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td valign="top" align="left" style="width: 11%">&nbsp;</td>
                        <td valign="top" align="left" style="width: 17%">&nbsp;</td>
                    </tr>

                </table>
                <div id="accordion" style="width: 100%">

                    <h3><a href="">Bill Details</a></h3>
                    <div>
                        <table style="width: 100%; " class="table">

                            <tr>

                                <td class="auto-style1">Vender Name</td>
                                <td class="td_width_16" style="width: 209px">
                                    <asp:DropDownList ID="cboVender" runat="server" CssClass="Dropdown">
                                    </asp:DropDownList>
                                    <asp:ImageButton ID="btnRefreshPublisher0" runat="server"
                                        ImageUrl="~/Images/Refresh.jpg" Style="height: 14px" />
                                </td>
                                <td class="td_width_16" style="width: 127px">Bill No</td>
                                <td class="td_width_16" valign="top" style="width: 193px">

                                    <asp:TextBox ID="txtBillNo" runat="server" CssClass="textbox"></asp:TextBox>

                                </td>
                                <td class="td_width_4" style="width: 207px">Bill Date</td>
                                <td>
                                    <asp:TextBox ID="txtBillDate" runat="server" CssClass="textbox"></asp:TextBox>

                                    <asp:CalendarExtender ID="txtBillDate_CalendarExtender" runat="server" BehaviorID="txtBillDate_CalendarExtender" Format="dd-MM-yyyy" TargetControlID="txtBillDate">
                                    </asp:CalendarExtender>

                                    <%--<asp:CalendarExtender ID="txtBillDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="txtBillDate"></asp:CalendarExtender>--%>
                                </td>
                            </tr>

                            <tr>
                                <td class="auto-style1">Price Per Book (Rs.)</td>
                                <td class="td_width_16" style="width: 209px">

                                    <asp:TextBox ID="txtPrice" runat="server" CssClass="textbox" onkeypress="return isNumber(event)"></asp:TextBox>

                                </td>
                                <td class="td_width_16" style="width: 127px">
                                    <asp:CheckBox ID="chkDVD" runat="server" AutoPostBack="True" Text="Contain DVD/CD" />
                                </td>
                                <td class="td_width_16" style="width: 193px">
                                    <asp:Label ID="lblDVDTitle" runat="server" Text="Title CD/DVD" Visible="False"></asp:Label>
                                </td>
                                <td class="td_width_16" style="width: 207px">
                                    &nbsp;<asp:CheckBox ID="chkDVDTitle" runat="server" AutoPostBack="True" Text="Same as Book" Visible="False" />
                                </td>
                                <td class="td_width_16" valign="top">

                                    <asp:TextBox ID="txtDVDTitle" runat="server" CssClass="textbox" Visible="False"></asp:TextBox>

                                </td>
                            </tr>

                            <tr>
                                <td class="auto-style1">Remarks</td>
                                <td class="td_width_16" colspan="3" rowspan="2">

                                    <asp:TextBox ID="txtRemark" runat="server" CssClass="textbox" Height="49px" Width="491px" TextMode="MultiLine"></asp:TextBox>

                                </td>
                                <td class="td_width_16" style="width: 207px">ISSN/ISBN</td>
                                <td class="td_width_16" valign="top">
                                    <asp:TextBox ID="txtISSN" runat="server" CssClass="textbox"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td class="auto-style1">&nbsp;</td>
                                <td class="td_width_16" colspan="2">
                                    <asp:CheckBox ID="chkReference" runat="server" AutoPostBack="True" Text="Under Reference Category" />
                                </td>
                            </tr>
                        </table>
                    </div>

                </div>
                <br />
                <table class="table">
                    <tr>
                        <td colspan="2" valign="top" align="left"><asp:Button ID="btnSave" runat="server" Text="Save" Class="btn btn-primary" />
                            &nbsp;
                <asp:Button ID="btnRemove" runat="server" Text="Remove" Class="btn btn-primary"  />
                            &nbsp;
                <asp:Button ID="btnBack" runat="server" Text="Back" Class="btn btn-primary" /></td>
                        
                        <td valign="top" align="left" colspan="2">
                            <asp:Label ID="lblStatus" runat="server" Font-Bold="True" Font-Size="Medium"
                                ForeColor="Navy"></asp:Label>
                        </td>
                        <td width="30%" valign="top" align="left">
                            <asp:TextBox ID="txtDVDAccNo" runat="server" CssClass="textbox" Visible="False"></asp:TextBox>
                                    <asp:TextBox ID="txtDateNow" runat="server" CssClass="textbox" Visible="False"></asp:TextBox>
                                    <asp:CalendarExtender ID="txtDateNow_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDateNow"></asp:CalendarExtender>
                                </td>
                    </tr>


                    <tr>
                        <td width="15%" valign="top" align="left">&nbsp;</td>
                        <td valign="top" align="left" style="margin-left: 40px; width: 31%;">

                            &nbsp;</td>
                        <td valign="top" align="left" style="width: 9%">&nbsp;</td>
                        <td width="15%" valign="top" align="left">
                            <asp:Label ID="lblLastAccNo" runat="server" Font-Bold="True"
                                Text="lblLastAccNo" Visible="False"></asp:Label>
                        </td>
                        <td width="30%" valign="top" align="left">&nbsp;</td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="clearfix"></div>
    </div>
</asp:Content>
<asp:Content ID="Content3" runat="server" contentplaceholderid="head">
    <style type="text/css">
        .auto-style1 {
            width: 206px;
        }
    </style>
</asp:Content>

