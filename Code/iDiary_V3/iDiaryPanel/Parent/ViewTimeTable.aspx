<%@ Page Language="VB" MasterPageFile="~/iDiaryPanel/Parent/ParentMaster.master" AutoEventWireup="false" Inherits="iDiary_V3.ViewTimeTable" title="Untitled Page" Codebehind="ViewTimeTable.aspx.vb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Heading" runat="server">
    Time Table
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="col_3" style="margin-top: 20px;" id="panelEnquiryNo" runat="server">
        <div class="col-md-3 widget widget1" style="width: 100%">
            <div class="r3_counter_box">
                <table border="1" cellpadding="0" cellspacing="1" width="640px">
                    <tr>
                        <td align="center" valign="middle" width="100px" height="30">&nbsp;</td>
                        <td align="center" valign="middle" width="100px"><b>Period-1</b></td>
                        <td align="center" valign="middle" width="100px"><b>Period-2</b></td>
                        <td align="center" valign="middle" width="100px"><b>Period-3</b></td>
                        <td align="center" valign="middle" width="100px"><b>Period-4</b></td>
                        <td align="center" valign="middle" width="100px"><b>Period-5</b></td>
                        <td align="center" valign="middle" width="100px"><b>Period-6</b></td>
                    </tr>

                    <tr>
                        <td align="center" valign="middle" width="100px" height="40"><b>Mon</b></td>
                        <td align="center" valign="middle">Hindi</td>
                        <td align="center" valign="middle">English</td>
                        <td align="center" valign="middle">Maths</td>
                        <td align="center" valign="middle">Art</td>
                        <td align="center" valign="middle">Music</td>
                        <td align="center" valign="middle">Craft</td>
                    </tr>

                    <tr>
                        <td align="center" valign="middle" width="100px" height="40"><b>Tue</b></td>
                        <td align="center" valign="middle">Hindi Grammer</td>
                        <td align="center" valign="middle">English Grammer</td>
                        <td align="center" valign="middle">Maths</td>
                        <td align="center" valign="middle">Art</td>
                        <td align="center" valign="middle">Music</td>
                        <td align="center" valign="middle">Craft</td>
                    </tr>

                    <tr>
                        <td align="center" valign="middle" width="100px" height="40"><b>Wed</b></td>
                        <td align="center" valign="middle">Hindi</td>
                        <td align="center" valign="middle">English Lit.</td>
                        <td align="center" valign="middle">Maths</td>
                        <td align="center" valign="middle">Computer</td>
                        <td align="center" valign="middle">GK</td>
                        <td align="center" valign="middle">Moral Sc.</td>
                    </tr>

                    <tr>
                        <td align="center" valign="middle" width="100px" height="40"><b>Thu</b></td>
                        <td align="center" valign="middle">Hindi</td>
                        <td align="center" valign="middle">English</td>
                        <td align="center" valign="middle">EVS</td>
                        <td align="center" valign="middle">Computer</td>
                        <td align="center" valign="middle">GK</td>
                        <td align="center" valign="middle">Moral Sc.</td>
                    </tr>

                    <tr>
                        <td align="center" valign="middle" width="100px" height="40"><b>Fri</b></td>
                        <td align="center" valign="middle">EVS</td>
                        <td align="center" valign="middle">English Lit.</td>
                        <td align="center" valign="middle">Maths</td>
                        <td align="center" valign="middle">Hindi</td>
                        <td align="center" valign="middle">Music</td>
                        <td align="center" valign="middle">Moral Sc.</td>
                    </tr>

                    <tr>
                        <td align="center" valign="middle" width="100px" height="40"><b>Sat</b></td>
                        <td align="center" valign="middle">Craft</td>
                        <td align="center" valign="middle">Music</td>
                        <td align="center" valign="middle">PT</td>
                        <td align="center" valign="middle">PT</td>
                        <td align="center" valign="middle">GK</td>
                        <td align="center" valign="middle">Moral Sc.</td>
                    </tr>

                </table>
                <br />
        <p>
        <a href="../TimeTable/myTT.PDF" class="circular">Download Time Table (In PDF Format)</a> 
        </p>
            </div>
        </div>
        <div class="clearfix"></div>
    </div>
</asp:Content>

