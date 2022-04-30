<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/AdminMaster.master" CodeBehind="SMSConfiguration.aspx.vb" Inherits="iDiary_V3.SMSConfiguration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminMasterContents" runat="server">
    
       <table class="table">
        <tr>
            <td style="height: 30px; text-decoration: underline; font-size: 14px;" colspan="2">
                <strong>SMS Configuration</strong></td>
            <td style="width: 17%; height: 30px">
                &nbsp;</td>
            <td rowspan="10" valign="top" width="65%">               
            </td>
        </tr>
        <tr>
            <td style="width: 17%; height: 30px">
                SMS URL</td>
            <td style="width: 50%; height: 30px">
                <asp:TextBox ID="txtsmsurl" runat="server" BorderStyle="Solid" 
                    BorderWidth="1px" Width="172px"></asp:TextBox>
            &nbsp;
            </td>
            <td style="width: 17%; height: 30px">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 17%; height: 30px">
                SMS Port</td>
            <td style="width: 50%; height: 30px">
                <asp:TextBox ID="txtsmsport" runat="server" BorderStyle="Solid" 
                    BorderWidth="1px" Width="172px"></asp:TextBox>
            </td>
            <td style="width: 17%; height: 30px">
                </td>
        </tr>
        <tr>
            <td style="width: 17%; height: 30px">
                SMS User</td>
            <td style="width: 50%; height: 30px">
                <asp:TextBox ID="txtsmsuser" runat="server" BorderStyle="Solid" 
                    BorderWidth="1px" Width="172px"></asp:TextBox>
            </td>
            <td style="width: 17%; height: 30px">
            </td>
        </tr>
        <tr>
            <td style="width: 17%; height: 30px">
                SMS Password</td>
            <td style="width: 50%; height: 30px">
                <asp:TextBox ID="txtsmspassword" runat="server" BorderStyle="Solid" BorderWidth="1px"  Width="172px"></asp:TextBox>
            </td>
            <td style="width: 17%; height: 30px">
            </td>
        </tr>



             <tr>
            <td style="width: 17%; height: 30px">
               School</td>
            <td style="width: 50%; height: 30px">
               <asp:DropDownList ID="cboSchool" runat="server"  CssClass="Dropdown" BorderStyle="Solid" BorderWidth="1px"  Width="172px">
               </asp:DropDownList>
            </td>
            <td style="width: 17%; height: 30px">
                </td>
        </tr>






        <tr>
            <td style="width: 17%; height: 30px">
                SMS Sender</td>
            <td style="width: 50%; height: 30px">
                <asp:TextBox ID="txtsmssender" runat="server" style="text-transform:uppercase" BorderStyle="Solid"
                    BorderWidth="1px" Width="172px"></asp:TextBox>
            &nbsp;<asp:Button ID="btnSave" runat="server" Text="+" CssClass="btn btn-primary"/>&nbsp; <asp:Button ID="btnremove" runat="server" Text="X" CssClass="btn btn-primary"/>&nbsp;<asp:Button ID="btnnew" runat="server" Text="New" CssClass="btn btn-primary" /></td>
            <td style="width: 17%; height: 30px">
            </td>
        </tr>

           

           <tr>
               <td style="width: 17%; height: 30px">
                </td>
            <td style="width: 50%; height: 30px">
                <asp:CheckBox ID="chkisdefault" runat="server" Text="Set As Default" AutoPostBack="true"  />
                </td>
            <td style="width: 17%; height: 30px">
            </td>
           </tr>


        <tr>
            <td style="width: 17%">
                &nbsp;</td>
            <td style="width: 100%">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" DataSourceID="SMSSenderDatasource" CssClass="Grid" Width="250px">
                   
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" />
                         <asp:TemplateField HeaderText="Sr. No.">
        <ItemTemplate>
             <%#Container.DataItemIndex+1 %>
        </ItemTemplate>
          <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
    </asp:TemplateField>
                        <asp:BoundField DataField="SMSSender" ItemStyle-Width ="250px" HeaderText="SMS Sender" SortExpression="SMSSender" />

                            <asp:BoundField DataField="SchoolName"  ItemStyle-Width ="350px" HeaderText="School Name" SortExpression="SchoolName" />
                       
                    </Columns>
                   
                </asp:GridView>
                <asp:SqlDataSource ID="SMSSenderDatasource" runat="server" ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" SelectCommand="SELECT [SMSSender],SchoolName FROM [vw_SMSSender]"></asp:SqlDataSource>
                </td>
            <td style="width: 17%">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 17%">
                &nbsp;</td>
            <td style="width: 50%">
                
                &nbsp;
                
            </td>
            <td style="width: 17%">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 17%; height: 53px;">
            </td>
            <td style="width: 50%; height: 53px;">
                <asp:Label ID="lblStatus" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
            </td>
            <td style="width: 17%; height: 53px;">
            </td>
        </tr>
        <tr>
            <td style="width: 17%">
                &nbsp;</td>
            <td style="width: 50%">
                &nbsp;</td>
            <td style="width: 17%">
                &nbsp;</td>
            <td width="65%">
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>
