<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="iDiary_V3.Login1" %>
<%@ Register assembly="WebControlCaptcha" namespace="WebControlCaptcha" tagprefix="cc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml"><head><meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1" />
<title>Login Form</title>
<link href="css/loginStyle.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .auto-style1 {
            height: 97px;
        }
    </style>
    </head>
<body>
    <noscript>
    <style type="text/css">
        .pagecontainer {
            display: none;
        }
    </style>
    <div >
        <br />
        <hr />
        <b style="color:darkmagenta;">
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
              You don't have javascript enabled. Please Enable it to continue.
        </b>
    
    </div>
</noscript>
    <form id="Form1" runat="server">
<table width="100%" border="0" class="pagecontainer" cellspacing="0" cellpadding="0">
  <tr>
    <td width="100%" height="10" colspan="2" bgcolor="37C464"> </td>
  </tr>
  <tr>
    <td colspan="2" align="center" valign="top"><table width="1000" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td height="305" align="center" valign="top" class="top-sadho">
            
            <table width="975" border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td width="975" height="361" valign="top" class="bg"><table width="966" border="0" cellspacing="0" cellpadding="0">
              <tr>
                <td style="text-align:center"><br />
                        <img src="images/Loginlogo.png" alt=""   /><br />
                    <br />
                </td>
                <td style="text-align:center">
                        <img src="images/line.png" alt="" /></td>
              </tr>
              <tr>
                <td height="180" align="center" valign="middle" colspan="2"><table width="945" border="0" cellspacing="0" cellpadding="0">

                  <tr>
                    <td width="548"><center>
                        <br />
                        </center></td>
                    <td width="397"><table width="386" border="0" cellpadding="0" cellspacing="0">
                      <tr>
                        <td height="362" align="center" valign="middle" background="images/sing-bg.jpg"><table width="329" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                              <td><img src="images/sing-line.jpg" alt="" width="329" height="41" /></td>
                            </tr>

                            <tr>
                              <td height="33" class="text">Sign in using your registered account</td>
                            </tr>
                            
                            <tr>
                              <td><table border="0" cellspacing="0" cellpadding="0">
                                  <tr>
                                    <td height="39" background="images/filed-bg1.jpg">
                                        <table width="313" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                          <td width="51">&nbsp;</td>
                                          <td width="262"><asp:TextBox CssClass="text-filed" ID="txtUserName" Width="80%" runat="server" Text="Username" ></asp:TextBox>
</td>
                                        </tr>
                                    </table></td>
                                  </tr>
                              </table></td>
                            </tr>
                            <tr>
                              <td>&nbsp;</td>
                            </tr>
                            <tr>
                              <td><table border="0" cellspacing="0" cellpadding="0">
                                  <tr>
                                    <td height="39" background="images/filed-bg2.jpg"><table width="313" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                          <td width="49">&nbsp;</td>
                                          <td width="264"><asp:TextBox CssClass="text-filed" ID="txtPassword" Width="80%" runat="server" TextMode="Password" text="Password"></asp:TextBox></td>
                                        </tr>
                                    </table></td>
                                  </tr>
                              </table></td>
                            </tr>
                            <tr>
                              <td class="auto-style1">
                                  <cc1:CaptchaControl ID="ccLogin" runat="server" CaptchaWidth="200" LayoutStyle="Vertical" Text="Enter the code shown : " Width="100%" CaptchaMaxTimeout="300" />
                                </td>
                            </tr>
                            <tr>
                             <td style="text-align:center;">
                                                                
                           <asp:ValidationSummary ID="ValidationSummary1" ShowSummary="false" ShowMessageBox="true" runat="server" Font-Size="Small" />
                                   <asp:ImageButton ID="btnSignIn"  runat="server" ValidationGroup="Submit"  ImageUrl="~/images/singup-button.jpg"  width="124" height="39" border="0" /></td>
                            </tr>
                             <tr>
                             <td style="text-align:center;font-weight:bold;color:green;height:39px;">

                               <%--<a href="ForgotPassword.aspx" style="font-weight:bold;color:green;">Forgot Password ?</a> | <a href="Support.aspx" style="font-weight:bold;color:green;">Support - Help</a>--%>
                             </td>
                            </tr>
                            
                        </table></td>
                      </tr>
                    </table></td>
                  </tr>
                  <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                  </tr>
                </table></td>
              </tr>
              <tr>
                <td align="center" colspan="2">
                  
                    <table width="949" border="0" cellspacing="0" cellpadding="0">
                  <tr>
                    <td bgcolor="#37C464" class="heading1">&nbsp;</td>
                  </tr>
                  <tr>
                    <td align="center" valign="top">
                        <br />
                         
                      </td>
                  </tr>
                  
                </table></td>
              </tr>
              
            </table></td>
          </tr>
          
        </table>

        </td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td height="44" colspan="2" align="center" valign="top" bgcolor="2c2c2c"><table width="841" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td align="center">&nbsp;</td>
      </tr>
      <tr>
        <td align="center" class="text1"><table width="350" border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td width="180" style="text-align:right !important;">Designed &amp; Developed By : </td>
            <td width="170" style="text-align:left !important;">&nbsp; i-Diary IT Solutions Pvt. Ltd.</td>
          </tr>
        </table>&nbsp;</td>
      </tr>
      <tr>
        <td align="center">&nbsp;</td>
      </tr>
    </table></td>
  </tr>
</table>

<!-- Box Start-->
<!-- Text Under Box -->
        </form> 
</body></html>