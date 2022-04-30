<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.master" CodeBehind="ChangePassword.aspx.vb" Inherits="iDiary_V3.ChangePassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            height: 28px;
            width: 31%;
        }
        .auto-style2 {
            height: 28px;
            width: 21%;
        }
        .auto-style3 {
            height: 28px;
            width: 48%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Heading" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <%-- <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />--%>
     <div class="col_3" style="margin-top: 20px;" id="panelEnquiryNo" runat="server">
        <div class="col-md-3 widget widget1" style="width: 100%">
            <div class="r3_counter_box">
     <table class="table">
        <tr>
            <td class="auto-style2">Emp Name</td>
            <td width="10%" style="height: 28px"><asp:TextBox ID="txtempname" BackColor="Yellow"   CssClass="textbox" runat="server" ReadOnly="true" ></asp:TextBox></td>
            <td class="auto-style2">User Name</td>
            <td class="auto-style1">
                <asp:TextBox ID="txtusername" runat="server" CssClass="textbox" BackColor="Yellow"  ReadOnly="true" ></asp:TextBox>
            </td>
            <td class="auto-style3">Mob No</td>
            <td width="10%"  style="height: 28px">
                <asp:TextBox ID="txtmobno" runat="server" CssClass="textbox" BackColor="Yellow" ReadOnly="true" ></asp:TextBox>
            </td>
           
        </tr>
         <tr>
            <td class="auto-style2">Login ID</td>
            <td width="10%" style="height: 28px"><asp:TextBox ID="txtloginid"  CssClass="textbox" BackColor="Yellow" runat="server" ReadOnly="true" ></asp:TextBox></td>
            <td class="auto-style2"> Designation         
            </td>
            
            <td class="auto-style1">
                <asp:TextBox ID="txtdesignaion"  CssClass="textbox" BackColor="Yellow" runat="server" ReadOnly="true" ></asp:TextBox>
            </td>
              <td class="auto-style3">Dept
                
            </td>
            <td style="height: 28px; width: 24%;">   
                <asp:TextBox ID="txtdept"  CssClass="textbox" BackColor="Yellow" runat="server" ReadOnly="true" ></asp:TextBox>       
            </td>
        </tr>
         <tr>
            <td class="auto-style2"></td>
            <td width="10%" style="height: 28px"></td>
            <td class="auto-style2">         
            </td>
            
            <td class="auto-style1">
                
            </td>
              <td class="auto-style3">
                
            </td>
            <td style="height: 28px; width: 24%;">   
                     
            </td>
        </tr>
         <tr>
            <td class="auto-style2">
                <asp:Label ID="Label1" runat="server" Text="Old Password" Font-Bold="true" ></asp:Label>&nbsp;<span style="color: #CC0000">*</span></td>
            <td width="10%" style="height: 28px"><asp:TextBox ID="txtoldpassword" TextMode="Password"   CssClass="textbox" runat="server"></asp:TextBox></td>
            <td class="auto-style2">         
            </td>
            
            <td class="auto-style1">
                
            </td>
              <td class="auto-style3">
                <asp:TextBox ID="txtuserid"  CssClass="textbox" runat="server" ReadOnly="true" Visible="false"  ></asp:TextBox>
            </td>
            <td style="height: 28px; width: 24%;">   
                      <asp:TextBox ID="txtempoldpassword"  CssClass="textbox" runat="server" ReadOnly="true" Visible="false"  ></asp:TextBox>
            </td>
        </tr>
         <tr>
            <td class="auto-style2"> <asp:Label ID="Label2" runat="server" Text="New Password" Font-Bold="true" ></asp:Label>&nbsp;<span style="color: #CC0000">*</span></td>
            <td width="10%" style="height: 28px"><asp:TextBox ID="txtnewpassword" TextMode="Password"   CssClass="textbox" runat="server" ></asp:TextBox></td>
            <td class="auto-style2">       
            </td>
            
            <td class="auto-style1">
               
            </td>
              <td class="auto-style3">
                
            </td>
            <td style="height: 28px; width: 24%;">   
                     
            </td>
        </tr>
         <tr>
            <td class="auto-style2"> <asp:Label ID="Label3" runat="server" Text="Confirm Password" Font-Bold="true" ></asp:Label>&nbsp;<span style="color: #CC0000">*</span></td>
            <td width="10%" style="height: 28px"><asp:TextBox ID="txtconfirmpassword" TextMode="Password"   CssClass="textbox" runat="server" ></asp:TextBox></td>
            <td class="auto-style2">       
            </td>
            
            <td class="auto-style1">
                <asp:Button ID="btnsave" runat="server" Text="Save" CssClass="btn btn-sm btn-primary" />
            </td>
              <td class="auto-style3">
                
            </td>
            <td style="height: 28px; width: 24%;">   
                     
            </td>
        </tr>
        </table>
                 </div>
        </div>
        <div class="clearfix"></div>
    </div>
</asp:Content>
