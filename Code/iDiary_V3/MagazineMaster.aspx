<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="MagazineMaster.aspx.vb" Inherits="iDiary_V3.MagazineMaster" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Heading" runat="server">
    Add / Modify Magazine Entries  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col_3" style="margin-top: 20px;" id="panelEnquiryNo" runat="server">
        <div class="col-md-3 widget widget1" style="width: 100%">
            <div class="r3_counter_box">
                <table class="table">

                    <tr>
                        <td>Accession No</td>
                        <td class="auto-style3">
                            <asp:TextBox ID="txtAccNo" runat="server" Width="96px" CssClass="textbox"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnNext" runat="server" Text="Next" Width="54px" CssClass="btn btn-sm btn-primary" />
                        </td>
                        <td class="auto-style1">Title</td>
                        <td class="auto-style2">
                            <asp:TextBox ID="txtTitle" runat="server" CssClass="textbox"></asp:TextBox>
                        </td>
                        <td style="margin-left: 10px">No. of Magazines</td>
                        <td>
                            <asp:TextBox ID="txtNoOfBooks" runat="server" CssClass="textbox"></asp:TextBox></td>
                    </tr>

                    <tr>
                        <td>Publisher<asp:ImageButton ID="btnRefreshPublisher" runat="server"
                            ImageUrl="~/Images/Refresh.jpg" Style="height: 14px" />
                        </td>
                        <td class="auto-style3" >
                            <asp:DropDownList ID="cboPub" runat="server" CssClass="Dropdown">
                            </asp:DropDownList>
                        </td>
                        <td class="auto-style1">Category<asp:ImageButton ID="btnRefreshBookCategory" runat="server"
                            ImageUrl="~/Images/Refresh.jpg" Style="height: 14px" />
                        </td>
                        <td class="auto-style2" >
                            <asp:DropDownList ID="cboBookCat" runat="server" CssClass="Dropdown">
                            </asp:DropDownList>
                        </td>
                        <td style="margin-left: 10px">Code No.</td>
                        <td>
                            <asp:TextBox ID="txtBookCodeNo" runat="server" CssClass="textbox"></asp:TextBox></td>
                    </tr>


                    <tr>
                        <td>No. of Pages</td>
                        <td class="auto-style3">
                            <asp:TextBox ID="txtPages" runat="server" CssClass="textbox"></asp:TextBox>
                        </td>
                         <td class="auto-style1">Frequency</td>
                        <td class="auto-style2">
                            <asp:DropDownList ID="cboFrequency" runat="server" CssClass="Dropdown">
                            </asp:DropDownList>
                        </td>
                        <td style="margin-left: 10px">Volume</td>
                        <td >
                            <asp:TextBox ID="txtVolume" runat="server" Width="148px" CssClass="textbox"></asp:TextBox>
                        </td>
                    </tr>


                    <tr>
                        
                        <td>Issue</td>
                        <td class="auto-style3">
                            <asp:TextBox ID="txtIssue" runat="server" CssClass="textbox"></asp:TextBox>
                        </td>
                         <td class="auto-style1">Month</td>
                        <td class="auto-style2" >
                            <asp:DropDownList ID="cboMonth" runat="server" CssClass="Dropdown"></asp:DropDownList>
                        </td>

                        <td style="margin-left: 10px">Year</td>
                        <td>
                            <asp:DropDownList ID="cboYear" runat="server" CssClass="Dropdown">
                            </asp:DropDownList>
                        </td>
                    </tr>


                    <tr>
                        <td>ISSN/ISBN</td>
                        <td class="auto-style3" >
                            <asp:TextBox ID="txtISSN" runat="server" CssClass="textbox"></asp:TextBox>
                        </td>
                        
                        <td class="auto-style1">Status
                <asp:ImageButton ID="btnRefreshStatus" runat="server"
                    ImageUrl="~/Images/Refresh.jpg" Style="height: 14px; width: 17px" />
                        </td>
                        <td class="auto-style2">
                            <asp:DropDownList ID="cboStatus" runat="server" CssClass="Dropdown">
                            </asp:DropDownList>
                        </td>
                        <td style="margin-left: 10px">Rack No
                <asp:ImageButton ID="btnRefreshRack" runat="server"
                    ImageUrl="~/Images/Refresh.jpg" Style="height: 14px" />
                        </td>
                        <td >
                            <asp:DropDownList ID="cboRack" runat="server" CssClass="Dropdown">
                            </asp:DropDownList>
                        </td>
                    </tr>

                    <tr>
                        <td>Magazine Issued?</td>
                        <td class="auto-style3">

                            <asp:DropDownList ID="cboIssued" runat="server" CssClass="Dropdown">
                                <asp:ListItem>No</asp:ListItem>
                                <asp:ListItem>Yes</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>

                    <tr>
                        <td>&nbsp;</td>

                        <td class="auto-style3">&nbsp;</td>

                        <td class="auto-style1">&nbsp;</td>

                        <td class="auto-style2">&nbsp;</td>
                        <td>&nbsp;</td>

                        <td>&nbsp;</td>
                    </tr>


                    <tr>
                        <td><b>Bill Details</b></td>

                        <td class="auto-style3">&nbsp;</td>
                        <td class="auto-style1">&nbsp;</td>

                        <td class="auto-style2">&nbsp;</td>
                        <td>&nbsp;</td>

                        <td>&nbsp;</td>
                    </tr>


                    <tr>
                        <td>Vender Name<asp:ImageButton ID="btnRefreshPublisher0" runat="server"
                            ImageUrl="~/Images/Refresh.jpg" Style="height: 14px" />
                        </td>
                        <td class="auto-style3" >
                            <asp:DropDownList ID="cboVender" runat="server" CssClass="Dropdown">
                            </asp:DropDownList>
                        </td>
                        
                        <td class="auto-style1">Bill No</td>

                        <td class="auto-style2">

                            <asp:TextBox ID="txtBillNo" runat="server" CssClass="textbox"></asp:TextBox>

                        </td>
                        <td style="height: 24px">Bill Date</td>
                        <td >
                            <asp:TextBox ID="txtBillDate" runat="server"  CssClass="textbox"></asp:TextBox>
                            <asp:CalendarExtender ID="txtBillDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="txtBillDate"></asp:CalendarExtender>
                        </td>
                    </tr>


                    <tr>
                                                
                        <td style="height: 24px">Price (IN RS)</td>

                        <td valign="top" class="auto-style4">

                            <asp:TextBox ID="txtPrice" runat="server" CssClass="textbox"></asp:TextBox>

                        </td>
                        <td class="auto-style1">Contain DVD/CD</td>
                        <td valign="top" align="left" class="auto-style2">
                            <asp:CheckBox ID="chkDVD" runat="server" AutoPostBack="True" />
                        </td>
                        <td>
                            <asp:Label ID="lblDVDTitle" runat="server" Text="Title CD/DVD" Visible="False"></asp:Label>
                            
                        </td>
                        <td>
                            
                            <asp:CheckBox ID="chkDVDTitle" runat="server" AutoPostBack="True" Text="Same as Magazine" Visible="False" />
                            <br />
                            <asp:TextBox ID="txtDVDTitle" runat="server" Width="135px" CssClass="textbox" Visible="False"></asp:TextBox>
                            
                        </td>
                    </tr>



                    <tr>
                        <td>Remarks</td>
                        <td  rowspan="2" class="auto-style3">
                            <asp:TextBox ID="txtRemark" runat="server" Width="224px" Height="47px" CssClass="textbox"></asp:TextBox>
                        </td>

                        <td class="auto-style1">&nbsp;</td>
                        <td class="auto-style2">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>


                    <tr>
                        <td>&nbsp;</td>
                        <td valign="top" align="left" style="margin-left: 40px; " class="auto-style1">&nbsp;</td>
                        <td valign="top" align="left" class="auto-style2">
                            <asp:TextBox ID="txtDateNow" runat="server" Width="100px" CssClass="textbox" Visible="False"></asp:TextBox>
                            <asp:CalendarExtender ID="txtDateNow_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDateNow"></asp:CalendarExtender>
                        </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>


                    <tr>
                        <td>&nbsp;</td>
                        <td class="auto-style3" >
                            <br />
                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" />
                            &nbsp;
                <asp:Button ID="btnRemove" runat="server" Text="Remove" CssClass="btn btn-primary" />
                            &nbsp;
                <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-primary" />
                        </td>

                        <td class="auto-style1">
                            <asp:TextBox ID="txtDVDAccNo" runat="server" CssClass="textbox" Visible="False"></asp:TextBox></td>
                        <td class="auto-style2">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>


                    <tr>
                        <td>&nbsp;</td>
                        <td class="auto-style3" >&nbsp;</td>

                        <td class="auto-style1">&nbsp;</td>
                        <td class="auto-style2">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>


                    <tr>
                        <td>&nbsp;</td>
                        <td class="auto-style3" >
                            <asp:Label ID="lblStatus" runat="server" Font-Bold="True" Font-Size="Medium"
                                ForeColor="Navy"></asp:Label>
                            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                        </td>

                        <td class="auto-style1">
                            <asp:Label ID="lblLastAccNo" runat="server" Font-Bold="True"
                                Text="lblLastAccNo" Visible="False"></asp:Label>
                        </td>
                        <td class="auto-style2">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
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
            width: 17%;
        }
        .auto-style2 {
            width: 18%;
        }
        .auto-style3 {
            width: 278px;
        }
        .auto-style4 {
            height: 24px;
            width: 278px;
        }
    </style>
</asp:Content>

