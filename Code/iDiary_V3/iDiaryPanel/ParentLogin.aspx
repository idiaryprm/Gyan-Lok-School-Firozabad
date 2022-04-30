<%@ Page Language="VB" AutoEventWireup="false" Inherits="iDiary_V3.Parent_ParentLogin" Codebehind="ParentLogin.aspx.vb" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml"><head><meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1">

<meta http-equiv="X-UA-Compatible" content="IE=9">
<title>Login Form</title>
<link href="/./css/loginStyle.css" rel="stylesheet" type="text/css">
</head>

<body>
    <form id="Form1" runat="server">
<table width="100%" border="0." cellspacing="0" cellpadding="0">
  <tr>
    <td width="100%" height="10" colspan="2" bgcolor="37C464"> </td>
  </tr>
  <tr>
    <td colspan="2" align="center" valign="top"><table width="1000" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td height="305" align="center" valign="top" class="top-sadho"><table width="975" border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td width="975" height="361" valign="top" class="bg"><table width="966" border="0" cellspacing="0" cellpadding="0">
              <tr>
                <td><table width="949" border="0" cellspacing="0" cellpadding="0">
                  <tr>
                    <td width="275" height="125" align="center">&nbsp;</td>
                    <td width="674" align="center" valign="middle">
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                      </td>
                  </tr>
                </table></td>
              </tr>
              <tr>
                <td height="180" align="center" valign="middle"><table width="945" border="0" cellspacing="0" cellpadding="0">

                  <tr>
                    <td width="548"><center><img src="image/logo.png" alt="" width="233" height="111" /><br />
                        <br />
                        <img src="image/line.png" alt="" width="465" style="height: 64px" /></center></td>
                    <td width="397"><table width="386" border="0" cellpadding="0" cellspacing="0">
                      <tr>
                        <td height="284" align="center" valign="middle" background="image/sing-bg.jpg"><table width="329" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                              <td><img src="image/sing-line.jpg" alt="" width="329" height="41" /></td>
                            </tr>

                            <tr>
                              <td height="33" class="text">Sign in using your registered account:s</td>
                            </tr>
                            
                            <tr>
                              <td><table border="0" cellspacing="0" cellpadding="0">
                                  <tr>
                                    <td height="39" background="image/filed-bg1.jpg"><table width="313" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                          <td width="51">&nbsp;</td>
                                          <td width="262"><asp:TextBox CssClass="text-filed" ID="txtUserName" runat="server" Text="Username" ></asp:TextBox>
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
                                    <td height="39" background="image/filed-bg2.jpg"><table width="313" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                          <td width="49">&nbsp;</td>
                                          <td width="264"><asp:TextBox CssClass="text-filed" ID="txtPassword" runat="server" TextMode="Password" text="Password"></asp:TextBox></td>
                                        </tr>
                                    </table></td>
                                  </tr>
                              </table></td>
                            </tr>
                            <tr>
                              <td>&nbsp;</td>
                            </tr>
                            <tr>
                              <td><table width="316" border="0" cellspacing="0" cellpadding="0">
                                  <tr>
                                    <td><span class="text">
                                      <input id="Field2" name="Field2" type="checkbox" class="field checkbox" value="First Choice" tabindex="4" onchange="handleInput(this);" />
                                      <label class="choice" for="Field2">Keep me signed in</label>
                                    </span> </td>
                                    <td><a href="#"><asp:ImageButton ID="btnSignIn" runat="server" ImageUrl="image/singup-button.jpg"  width="104" height="39" border="0" /></a></td>
                                  </tr>
                              </table></td>
                                
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
                <td align="center"><table width="949" border="0" cellspacing="0" cellpadding="0">
                  <tr>
                    <td bgcolor="#37C464" class="heading1">School Management System Core Features</td>
                  </tr>
                  <tr>
                    <td align="center" valign="top"><table width="930" border="0" cellspacing="0" cellpadding="0">
                      <tr>
                        <td height="10" colspan="3" align="left" valign="top"></td>
                        </tr>
                      <tr>
                        <td width="330" height="53" align="left" valign="top"><table width="287" border="0" cellspacing="0" cellpadding="0">
                          <tr>
                            <td width="287" class="heading-more"><a href="#">Enquiry Management</a></td>
                          </tr>
                          <tr>
                            <td class="heading-more"><a href="#">Admission Management</a></td>
                          </tr>
                          <tr>
                            <td class="heading-more"><a href="#">Student information Management</a></td>
                          </tr>
                          <tr>
                            <td class="heading-more"><a href="#">Fee Management</a></td>
                          </tr>
                          <tr>
                            <td class="heading-more"><a href="#">Library Management</a></td>
                          </tr>
                          <tr>
                            <td class="heading-more"><a href="#">Examination Management</a></td>
                          </tr>
                          <tr>
                            <td>&nbsp;</td>
                          </tr>
                        </table></td>
                        <td width="302" align="left" valign="top"><table width="272" border="0" cellspacing="0" cellpadding="0">
                          <tr>
                            <td class="heading-more"><a href="#">Financial Management</a></td>
                          </tr>
                          <tr>
                            <td class="heading-more"><a href="#">HR & Payroll Management</a></td>
                          </tr>
                          <tr>
                            <td class="heading-more"><a href="#">Inventory Management</a></td>
                          </tr>
                          <tr>
                            <td class="heading-more"><a href="#">Transport Management</a></td>
                          </tr>
                          <tr>
                            <td class="heading-more"><a href="#">Attendance Management</a></td>
                          </tr>
                          <tr>
                            <td class="heading-more"><a href="#">Time Table Management</a></td>
                          </tr>
                          <tr>
                            <td>&nbsp;</td>
                          </tr>
                        </table></td>
                        <td width="298" align="left" valign="top"><table width="272" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                              <td class="heading-more"><a href="#">Teacher’s Diary</a></td>
                            </tr>
                            <tr>
                              <td class="heading-more"><a href="#">Document Management System</a></td>
                            </tr>
                            <tr>
                              <td class="heading-more"><a href="#">Health Record Management</a></td>
                            </tr>
                            <tr>
                              <td class="heading-more"><a href="#">Principal Dashboard</a></td>
                            </tr>
                            <tr>
                              <td class="heading-more"><a href="#">Parents Dashboard</a></td>
                            </tr>
                            <tr>
                              <td class="heading-more"><a href="#">Mobile Apps</td>
                            </tr>
                            <tr>
                              <td>&nbsp;</td>
                            </tr>
                          </table>                          </td>
                      </tr>
                    </table></td>
                  </tr>
                  
                </table></td>
              </tr>
              
            </table></td>
          </tr>
          
        </table></td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td height="44" colspan="2" align="center" valign="top" bgcolor="2c2c2c"><table width="841" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td align="center">&nbsp;</td>
      </tr>
      <tr>
        <td align="center" class="text1"><table width="333" border="0" cellspacing="0" cellpadding="0" style="height: 18px">
          <tr>
            <td style="width: 338px">&nbsp;<strong>iDiary IT Solutions Pvt. Ltd</strong></td>
          </tr>
        </table></td>
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
