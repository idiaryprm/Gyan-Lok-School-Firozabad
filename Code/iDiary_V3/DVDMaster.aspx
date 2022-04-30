<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="DVDMaster.aspx.vb" Inherits="iDiary_V3.DVDMaster" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Heading" runat="server">
    Add / Modify DVD/CD Entries  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col_3" style="margin-top: 20px;" id="panelEnquiryNo" runat="server">
        <div class="col-md-3 widget widget1" style="width: 100%">
            <div class="r3_counter_box">
                <table class="table">

                    <tr>
                        <td width="15%" valign="top" align="left">Accession No</td>
                        <td width="30%" valign="top" align="left">
                            <asp:TextBox ID="txtAccNo" runat="server" CssClass="textbox"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnNext" runat="server" Text="Next" Width="54px" class="btn btn-sm btn-primary" />
                        </td>
                        <td valign="top" align="left" class="auto-style1">&nbsp;</td>
                        <td valign="top" align="left" class="auto-style3">No. of DVD/CD</td>
                        <td width="30%" valign="top" align="left">
                            <asp:TextBox ID="txtNoOfBooks" runat="server" CssClass="textbox"></asp:TextBox></td>
                    </tr>


                    <tr>
                        <td width="15%" valign="top" align="left">Title</td>
                        <td width="30%" valign="top" align="left">
                            <asp:TextBox ID="txtTitle" runat="server" CssClass="textbox"></asp:TextBox>
                        </td>
                        <td valign="top" align="left" class="auto-style1">&nbsp;</td>
                        <td valign="top" align="left" class="auto-style3">Code No.</td>
                        <td width="30%" valign="top" align="left">
                            <asp:TextBox ID="txtBookCodeNo" runat="server" CssClass="textbox"></asp:TextBox></td>
                    </tr>


                    <tr>
                        <td width="15%" valign="top" align="left">Category<asp:ImageButton ID="btnRefreshBookCategory" runat="server"
                            ImageUrl="~/Images/Refresh.jpg" Style="height: 14px" />
                        </td>
                        <td width="30%" valign="top" align="left" style="margin-left: 40px">
                            <asp:DropDownList ID="cboBookCat" runat="server"  CssClass="Dropdown">
                            </asp:DropDownList>
                        </td>
                        <td valign="top" align="left" class="auto-style1">&nbsp;</td>
                        <td valign="top" align="left" class="auto-style3">Frequency</td>
                        <td width="30%" valign="top" align="left">
                            <asp:DropDownList ID="cboFrequency" runat="server" CssClass="Dropdown">
                            </asp:DropDownList>
                        </td>
                    </tr>


                    <tr>
                        <td width="15%" valign="top" align="left">Rack No
                <asp:ImageButton ID="btnRefreshRack" runat="server"
                    ImageUrl="~/Images/Refresh.jpg" Style="height: 14px" />
                        </td>
                        <td width="30%" valign="top" align="left" style="margin-left: 40px">
                            <asp:DropDownList ID="cboRack" runat="server" CssClass="Dropdown">
                            </asp:DropDownList>
                        </td>
                        <td valign="top" align="left" class="auto-style1"></td>
                        <td valign="top" align="left" class="auto-style3">Status
                <asp:ImageButton ID="btnRefreshStatus" runat="server"
                    ImageUrl="~/Images/Refresh.jpg" Style="height: 14px; width: 17px" />
                        </td>
                        <td width="30%" valign="top" align="left">
                            <asp:DropDownList ID="cboStatus" runat="server" CssClass="Dropdown">
                            </asp:DropDownList>
                        </td>
                    </tr>

                    <tr>
                        <td width="15%" valign="top" align="left">&nbsp;</td>
                        <td width="30%" valign="top" align="left" style="margin-left: 40px">&nbsp;</td>
                        <td valign="top" align="left" class="auto-style1">&nbsp;</td>
                        <td valign="top" align="left" class="auto-style3">&nbsp;Issued?</td>
                        <td width="30%" valign="top" align="left">

                            <asp:DropDownList ID="cboIssued" runat="server" CssClass="Dropdown">
                                <asp:ListItem>No</asp:ListItem>
                                <asp:ListItem>Yes</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>


                    <tr>
                        <td width="15%" valign="top" align="left"><b>Bill Details</b></td>
                        <td valign="top" align="left" style="width: 19%">&nbsp;</td>
                        <td valign="top" align="left" class="auto-style1">&nbsp;</td>
                        <td valign="top" align="left" class="auto-style3">&nbsp;</td>

                        <td>&nbsp;</td>
                    </tr>


                    <tr>
                        <td width="15%" valign="top" align="left">Vender Name<asp:ImageButton ID="btnRefreshPublisher0" runat="server"
                            ImageUrl="~/Images/Refresh.jpg" Style="height: 14px" />
                        </td>
                        <td valign="top" align="left" style="width: 19%">
                            <asp:DropDownList ID="cboVender" runat="server" CssClass="Dropdown">
                            </asp:DropDownList>
                        </td>
                        <td valign="top" align="left" class="auto-style1">&nbsp;</td>
                        <td valign="top" align="left" class="auto-style3">Bill No</td>

                        <td>

                            <asp:TextBox ID="txtBillNo" runat="server" CssClass="textbox"></asp:TextBox>

                        </td>
                    </tr>


                    <tr>
                        <td width="15%" valign="top" align="left" style="height: 24px">Bill Date</td>
                        <td valign="top" align="left" style="width: 19%; height: 24px;">
                            <asp:TextBox ID="txtBillDate" runat="server" CssClass="textbox"></asp:TextBox>
                            <asp:CalendarExtender ID="txtBillDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="txtBillDate"></asp:CalendarExtender>
                        </td>
                        <td valign="top" align="left" class="auto-style2"></td>
                        <td valign="top" align="left" class="auto-style4">Price (IN RS)</td>

                        <td valign="top" style="height: 24px">

                            <asp:TextBox ID="txtPrice" runat="server" CssClass="textbox"></asp:TextBox>

                        </td>
                    </tr>


                    <tr>
                        <td width="15%" valign="top" align="left">Remarks</td>
                        <td width="30%" valign="top" align="left" style="margin-left: 40px" rowspan="2">
                            <asp:TextBox ID="txtRemark" runat="server" CssClass="textbox" TextMode="MultiLine" Height="70" Width="216"></asp:TextBox>
                        </td>
                        <td valign="top" align="left" class="auto-style1">&nbsp;</td>
                        <td valign="top" align="left" class="auto-style3">&nbsp;</td>
                        <td width="30%" valign="top" align="left">&nbsp;</td>
                    </tr>


                    <tr>
                        <td width="15%" valign="top" align="left">&nbsp;</td>
                        <td valign="top" align="left" style="margin-left: 40px; " class="auto-style1">&nbsp;</td>
                        <td valign="top" align="left" class="auto-style3">
                            <asp:TextBox ID="txtDateNow" runat="server" Width="100px" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px" Visible="False"></asp:TextBox>
                            <asp:CalendarExtender ID="txtDateNow_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDateNow"></asp:CalendarExtender>
                        </td>
                        <td width="15%" valign="top" align="left">&nbsp;</td>
                        <td width="30%" valign="top" align="left">&nbsp;</td>
                    </tr>


                    <tr>
                        <td width="15%" valign="top" align="left">&nbsp;</td>
                        <td width="30%" valign="top" align="left" style="margin-left: 40px">

                            <asp:Button ID="btnSave" runat="server" Text="Save" class="btn btn-primary"  />
                            &nbsp;
                <asp:Button ID="btnRemove" runat="server" Text="Remove"  class="btn btn-primary"  />
                            &nbsp;
                <asp:Button ID="btnBack" runat="server" Text="Back" class="btn btn-primary"  />
                        </td>
                        <td valign="top" align="left" class="auto-style1">&nbsp;</td>
                        <td valign="top" align="left" class="auto-style3">&nbsp;</td>
                        <td width="30%" valign="top" align="left">&nbsp;</td>
                    </tr>


                    <tr>
                        <td width="15%" valign="top" align="left">&nbsp;</td>
                        <td width="30%" valign="top" align="left" style="margin-left: 40px">&nbsp;</td>
                        <td valign="top" align="left" class="auto-style1">&nbsp;</td>
                        <td valign="top" align="left" class="auto-style3">&nbsp;</td>
                        <td width="30%" valign="top" align="left">&nbsp;</td>
                    </tr>


                    <tr>
                        <td width="15%" valign="top" align="left">&nbsp;</td>
                        <td width="30%" valign="top" align="left" style="margin-left: 40px">
                            <asp:Label ID="lblStatus" runat="server" Font-Bold="True" Font-Size="Medium"
                                ForeColor="Navy"></asp:Label>
                            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                        </td>
                        <td valign="top" align="left" class="auto-style1">&nbsp;</td>
                        <td valign="top" align="left" class="auto-style3">
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
            width: 9%;
        }
        .auto-style2 {
            height: 24px;
            width: 9%;
        }
        .auto-style3 {
            width: 21%;
        }
        .auto-style4 {
            height: 24px;
            width: 21%;
        }
    </style>
</asp:Content>

