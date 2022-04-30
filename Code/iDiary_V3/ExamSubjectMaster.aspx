<%@ Page Language="VB" MasterPageFile="~/ExamAdminMasterPage.master" AutoEventWireup="false" Inherits="iDiary_V3.ExamSubjectMaster" title="Untitled Page" Codebehind="ExamSubjectMaster.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SubHeading" Runat="Server">
    Subject Master
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMain" Runat="Server">
  
     <table class="table" >
        <tr>
            <td style="width:44%" rowspan="2">

                <asp:ListBox ID="lstSubjects" runat="server" AutoPostBack="True"  Height="367px" Width="300px"  CssClass="list"></asp:ListBox>

            </td>
             <td style="vertical-align:top;" rowspan="2" colspan="2">

                <strong style="font-size: medium">

                 <strong>Subject Major Group</strong><br />

                <asp:DropDownList ID="cboSubJectGroupMajor" runat="server" TabIndex="1" Width="172px" CssClass="Dropdown" AutoPostBack="True">
                </asp:DropDownList>

                 <br />
                 <br />
                 Subject Minor Group<br />

                <asp:DropDownList ID="cboSubJectGroupMinor" runat="server" TabIndex="1" Width="172px" CssClass="Dropdown" AutoPostBack="True">
                </asp:DropDownList>

                 <br />
                 <br />
                 Subject Name</strong><br />
                <asp:TextBox ID="txtSubjectName" runat="server" Width="175px" TabIndex="1" CssClass="textbox"></asp:TextBox>
                 <br />
                 <br />

                <strong style="font-size: medium">Subject Code<br />
                 <asp:TextBox ID="txtSubjectCode" runat="server" Width="175px" CssClass="textbox" TabIndex="1"></asp:TextBox>
                 </strong>
                  <br />
                <br />
                <asp:Label ID="lblStatus" runat="server" ForeColor="Navy" style="font-weight: 700; color: #FF3300"></asp:Label>
              
                <br />
                 <br />
                 <asp:Button ID="btnSave" runat="server" Text="Save" 
                   TabIndex="11" CssClass="btn btn-primary" />
&nbsp;&nbsp;
                <asp:Button ID="btnNew" runat="server" Text="New" 
                   TabIndex="12" CssClass="btn btn-primary" />
&nbsp;&nbsp;
                
                            
                <asp:Button ID="btnRemove" runat="server" Text="Remove" Visible="false" 
                    TabIndex="12" CssClass="btn btn-primary" />
            
               
            
                <br />
                <asp:TextBox ID="txtSubjectID" runat="server" Width="60px" Visible="False" 
                    AutoPostBack="True"></asp:TextBox>
            
                 <br />
                &nbsp;</td>
           
            
        </tr>
        <tr>
            <td style="width:35%">

                &nbsp;</td>
          
        </tr>
        <tr>
            <td style="width:44%">

                <asp:Label ID="lblHelp" runat="server" BackColor="#00FF99" BorderColor="Navy" 
                    Font-Bold="True" ForeColor="Navy"></asp:Label>
            </td>
             <td style="width:34%">

                 &nbsp;</td>
            <td style="width:35%">

                &nbsp;</td>
            
       
        </tr>
    </table>
    
</asp:Content>

