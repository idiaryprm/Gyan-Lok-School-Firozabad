<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.master" CodeBehind="StudentEnquiry.aspx.vb" Inherits="iDiary_V3.StudentEnquiry" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Heading" runat="server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <script>
         function isNumber(evt) {
             evt = (evt) ? evt : window.event;
             var charCode = (evt.which) ? evt.which : evt.keyCode;
             if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                 return false;
             }
             return true;
         }
    </script>
    <script type="text/javascript">
        function PaymentModeChange(value) {

            if (value.includes("Cash")==true) {
                document.getElementById('ContentPlaceHolder1_txtChqNo').disabled = true;
                document.getElementById('ContentPlaceHolder1_txtChqDate').disabled = true;
                document.getElementById('ContentPlaceHolder1_txtBankName').disabled = true;
                document.getElementById('ContentPlaceHolder1_txtBranchName').disabled = true;
            }
            else {
                document.getElementById('ContentPlaceHolder1_txtChqNo').disabled = false;
                document.getElementById('ContentPlaceHolder1_txtChqDate').disabled = false;
                document.getElementById('ContentPlaceHolder1_txtBankName').disabled = false;
                document.getElementById('ContentPlaceHolder1_txtBranchName').disabled = false;
            }

        }
</script>
     <link href="css/jquery-ui.css" rel="stylesheet" />
    <script src="js/jquery-ui.js"></script>
    <script>
        $(function () {
            $("#accordion")
                            .accordion({
                                collapsible: true,
                                active: false,
                               
                                active: -1,
                            
                                heightStyle: "content"
                            })
                            .show();
        });
  </script>
    <%--<br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
     <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
     <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />--%>
    <%--<div class="col_3" style="margin-top: 20px;" id="panelEnquiryNo" runat="server">
        <div class="col-md-3 widget widget1" style="width: 100%">
            <div class="r3_counter_box">
                <table class="table">
                    <tr>
                        <td class="auto-style1">Form No</td>
                        <td>
                            <asp:TextBox ID="txtEnquiryNo" runat="server" CssClass="textbox"></asp:TextBox>&nbsp;&nbsp;
                           </td>
                        <td>
                            
                        </td>
                        
                    </tr>
                </table>

            </div>
        </div>
        <div class="clearfix"></div>
    </div>--%>

    <div class="col_3" style="margin-top: 20px;" id="PanelEnquiry" runat="server">
        <div class="col-md-3 widget widget1" style="width: 100%">
            <div class="r3_counter_box">
                    <table class="table">
                        <tr>
                            <td>School Name</td>
                            <td colspan="2">
                                <asp:DropDownList ID="cboSchoolName" runat="server" AutoPostBack="true" CssClass="Dropdown" Width="300px">
                                </asp:DropDownList>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>&nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>Form No<span style="color: #CC0000">*</span></td>
                            <td>
                                <asp:TextBox ID="txtFormNo" runat="server" CssClass="textbox" Width="134px"></asp:TextBox> <asp:Button ID="btnEnquiry" runat="server" Text=">>" class="btn btn-sm btn-primary"/></td>
                            <td>Student Name<span style="color: #CC0000">*</span></td>
                            <td>
                                <asp:TextBox ID="txtSname" runat="server" CssClass="textbox"></asp:TextBox></td>
                            <td>Gender</td>
                            <td>
                            <asp:DropDownList ID="cboGender" runat="server" CssClass="Dropdown">
                                <asp:ListItem>Male</asp:ListItem>
                                <asp:ListItem>Female</asp:ListItem>
                            </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>Father Name<span style="color: #CC0000">*</span></td>
                            <td>
                                <asp:TextBox ID="txtFname" runat="server" CssClass="textbox"></asp:TextBox></td>
                            <td>Mother Name</td>
                            <td>
                                <asp:TextBox ID="txtMname" runat="server" CssClass="textbox"></asp:TextBox></td>
                            <td>Date of Birth<span style="color: #CC0000">*</span></td>
                            <td>
                                <asp:TextBox ID="txtDoB" runat="server" CssClass="textbox" AutoPostBack="true" ></asp:TextBox>
                                <asp:CalendarExtender ID="txtDOB_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDOB"></asp:CalendarExtender>
                                <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999" MaskType="Date" TargetControlID="txtDOB" PromptCharacter="_"> </asp:MaskedEditExtender>
                            </td>
                        </tr>
                        <tr>
                            <td>Admission for Class<span style="color: #CC0000">*</span></td>

                            <td>
                                <asp:DropDownList ID="cboClass" runat="server" CssClass="Dropdown">
                                </asp:DropDownList>
                            </td>
                            <td>Email ID</td>

                            <td>
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="textbox"></asp:TextBox>
                            </td>

                            <td>
                                Mobile No.<span style="color: #CC0000">*</span></td>

                            <td>

                                <asp:TextBox ID="txtMobNo" runat="server" CssClass="textbox" onkeypress="return isNumber(event)"></asp:TextBox>
                            </td>

                        </tr>
                        <tr>
                            <td>Enquiry Type<span style="color: #CC0000">*</span></td>

                            <td>
                                <asp:DropDownList ID="cboCategory" runat="server" CssClass="Dropdown">
                                </asp:DropDownList>
                            </td>
                            <td>Status<span style="color: #CC0000">*</span></td>

                            <td>

                                <asp:DropDownList ID="cboStatus" runat="server" CssClass="Dropdown" AutoPostBack="false">
                                </asp:DropDownList>
                            </td>

                            <td colspan="2">
                                <asp:Label ID="lblAgeOn" runat="server" Style="font-weight: 700"></asp:Label>
                                             <asp:Label ID="lblAge" runat="server"></asp:Label></td>


                        </tr>

                        <tr>
                            <td>Address</td>

                            <td colspan="3">
                                <asp:TextBox ID="txtAddress"  runat="server" CssClass="textbox" TextMode="MultiLine"  Width="518px" height="44px"></asp:TextBox>
                            </td>

                            <td>
                                Registration Date</td>

                            <td>

                <asp:TextBox ID="txtRegDate" runat="server" placeholder="dd/MM/yyyy" Width="163px" Height="25px" CssClass="textbox" TextMode="Date"></asp:TextBox>
                                <asp:MaskedEditExtender ID="MaskedEditExtender4" runat="server" Mask="99/99/9999" MaskType="Date" TargetControlID="txtRegDate" PromptCharacter="_"> </asp:MaskedEditExtender>
                            </td>
                        </tr>
                        
                        <tr>
                            <td>Remark</td>

                            <td colspan="3">
                                <asp:TextBox ID="txtEnquiry" runat="server" CssClass="textbox" TextMode="MultiLine" Width="518px" height="44px"></asp:TextBox>
                            </td>

                          
                        </tr>
                        <tr>
                            <td colspan="6">

                                  <div id="accordion" style="width: 100%">
    <h3><a href="">Payment Details</a></h3>
                                <div  style="padding:1em;padding-bottom:0px">       
     <table class="table">
        <tr>
            <td>Payment Mode <span style="color:red">*</span></td>
            <td>
                <asp:DropDownList ID="cboPaymentMode" Width="163px" runat="server" onchange="PaymentModeChange(this.value);"  CssClass="Dropdown"></asp:DropDownList>
            </td>
         
            <td>Amount <span style="color:red">*</span></td>
            <td>
                <asp:TextBox ID="txtAmount" runat="server"  CssClass="textbox"></asp:TextBox>
                </td>
            <td>Receipt No <span style="color:red">*</span></td>
             <td>
                <asp:TextBox ID="txtReceiptNo" runat="server"  CssClass="textbox"></asp:TextBox>
                </td>

            </tr>
         <tr>
             <td>Chq/DD/Transaction No</td>
           <td>
                <asp:TextBox ID="txtChqNo" runat="server" CssClass="textbox"></asp:TextBox></td>
            <td>Chq/DD Date</td>
             <td>
                <asp:TextBox ID="txtChqDate" runat="server" placeholder="dd/MM/yyyy" Width="163px" Height="25px" CssClass="textbox" TextMode="Date"></asp:TextBox>
                 <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999" MaskType="Date" TargetControlID="txtChqDate" PromptCharacter="_"> </asp:MaskedEditExtender>
                </td>
             <td>Bank Name</td>
             <td>
                <asp:TextBox ID="txtBankName" runat="server" CssClass="textbox"></asp:TextBox>
             </td>
        </tr>
         <tr>
            <td>Branch Name</td>
            <td>
                 <asp:TextBox ID="txtBranchName" runat="server" CssClass="textbox"></asp:TextBox>
         </td>
            <td>Remarks</td>
            <td colspan="3">
                <asp:TextBox ID="txtPaymentRemarks" runat="server" TextMode="MultiLine" Width="95.2%"  CssClass="textbox"></asp:TextBox>
                </td>
            

            </tr>
 

      

        </table>
                                    </div>

           </div>

                            </td>
                        </tr>


                        <tr>
                           <td>
<asp:CheckBox ID="chkprint" runat ="server" Checked="true" Text ="Print Receipt"  /></td>
                         
                              <td>

                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" class="btn btn-primary"/>
                            </td>

                            <td>

                                <asp:Button ID="btnUpdate" runat="server" Text="Update" class="btn btn-primary"/></td>
                            <td>
 <asp:TextBox ID="txtID" runat="server" CssClass="textbox" Visible="false"></asp:TextBox>
                               </td> 
                        </tr>
                        </table>
              
            </div>
        </div>
        <div class="clearfix"></div>
    </div>


       



    <div class="col_3" style="margin-top: 20px;" id="Panel1" runat="server">
        <div class="col-md-3 widget widget1" style="width: 100%">
            <div class="r3_counter_box">
                    <asp:CheckBox ID="CheckBoxNotification" runat="server" Text="Send Notification" AutoPostBack="True" />
                    <table class="table">
                        <tr>
                            <td>Date</td>
                            <td class="auto-style1">
                                <asp:TextBox ID="txtDate" runat="server" CssClass="textbox"></asp:TextBox>
                                <asp:CalendarExtender ID="txtDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDate"></asp:CalendarExtender>
                                <asp:MaskedEditExtender ID="MaskedEditExtender3" runat="server" Mask="99/99/9999" MaskType="Date" TargetControlID="txtDate" PromptCharacter="_"> </asp:MaskedEditExtender>

                            </td>
                            <td>Time</td>
                            <td>
                                <asp:TextBox ID="txtTime" runat="server" CssClass="textbox"></asp:TextBox>

                            </td>
                        </tr>
                       
                        <tr>
                            <td>Message</td>
                            <td colspan="3">
                                <asp:TextBox ID="txtMessage" runat="server" CssClass="textbox" TextMode="MultiLine" Width="553px" height="54px"></asp:TextBox>

                            </td>
                        </tr>
                       
                        <tr>
                            <td>
                                <asp:Button ID="btnSendSMS" runat="server" Text="Send SMS" class="btn btn-primary"/>
                            </td>
                        </tr>
                    </table>
            </div>
        </div>
        <div class="clearfix"></div>
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
    </div>
</asp:Content>
<asp:Content ID="Content3" runat="server" contentplaceholderid="head">
    <style type="text/css">
        .auto-style1 {
            width: 310px;
        }
    </style>
</asp:Content>

