<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.master" CodeBehind="Index.aspx.vb" Inherits="iDiary_V3.Index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="heading" runat="server">
    Home
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <%-- <br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/>
    <br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/>--%>

     <div class="col_3" style="margin-top: 20px;" >
        <div class="col-md-3 widget widget1" style="width: 100%">
            <div class="r3_counter_box">
                <table class="table">
                      <tr>
            <td class="auto-style4" colspan="2"><strong>
                <asp:ImageButton ID="ImageButton1" runat="server" Height="20px" ImageUrl="~/Images/Android.png" Width="44px" Visible="False" />
                </strong>
               
            </td> 
            <td width="40%" rowspan="2">
              

      
    <div class="">

            </td> 
        </tr>

        <tr>
            <td width="30%" class="auto-style2">This product is licensed to:
                <br /><br />
                <asp:Label ID="lblSchoolName" runat="server" ForeColor="Navy"></asp:Label>
                <br />
                <br />
                Powered By : <br /><br />
        
                <asp:Label ID="lblManfName" runat="server" ForeColor="Green"></asp:Label>
                <br />
                <asp:Label ID="lblManfAddress1" runat="server" ForeColor="Green"></asp:Label>
                <br />
                <asp:Label ID="lblManfAddress2" runat="server" ForeColor="Green"></asp:Label>
                <br />
                <asp:Label ID="lblManfCity" runat="server" ForeColor="Green"></asp:Label>
                <br /><br />
                Email :
                <asp:Label ID="lblManfEmail" runat="server" ForeColor="Green"></asp:Label>
                <br /> 
                Phone:
                <asp:Label ID="lblManfPhone" runat="server" ForeColor="Green"></asp:Label>
                &nbsp;<br />
                Website:
                <asp:Label ID="lblManfURL" runat="server" ForeColor="Green"></asp:Label>
                <br />
            </td> 

            <td valign="top" class="auto-style3" background="images1/govind.JPG">
                </td> 
        </tr>
        <%--<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" 
            SelectCommand="Select RegNo, SName, ClassName, SecName, DOB, MobNo From vw_Student Where ASID<0">
        </asp:SqlDataSource>--%>
                      <tr>
                          <%--<td>
                <asp:Button ID="btnSend" runat="server"  Text="Send Messages" class="btn btn-primary" />
                
                
                
                
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                     DataSourceID="SqlDataSource1" CssClass="Grid" Width="400px" Visible="false">
                   
                    <Columns>
                        <asp:BoundField DataField="SName" HeaderText="Name" SortExpression="SName" />
                        <asp:BoundField DataField="ClassName" HeaderText="Class" 
                            SortExpression="ClassName" />
                        <asp:BoundField DataField="SecName" HeaderText="Sec" SortExpression="SecName" />
                    </Columns>
                    
                </asp:GridView>
                          </td>--%>
                      </tr>
                </table>

            </div>
        </div>
        <div class="clearfix"></div>
    </div>

  </asp:Content>
<asp:Content ID="Content3" runat="server" contentplaceholderid="head">
    <style type="text/css">
        .auto-style2 {
            height: 316px;
        }
        .auto-style3 {
            width: 50%;
            height: 316px;
        }
        .auto-style4 {
            height: 140px;
        }
    </style>
</asp:Content>
