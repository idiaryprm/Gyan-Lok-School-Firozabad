<%@ Page Language="VB" MasterPageFile="~/IdiaryPanel/Parent/ParentMaster.master" AutoEventWireup="false" Inherits="iDiary_V3.Parent_ChangeProfile" title="Untitled Page" Codebehind="ChangeProfile.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="cntbox">
	    <h2><span>Change Profile</span></h2>
    </div>
    
    <div class="mrt30">
        <table border="0" cellpadding="2" cellspacing="3" width="440px">
            <tr>
                <td width="100">Address</td>
                <td width="340">
                    <asp:TextBox ID="txtAddress" runat="server" Width="340" TextMode="MultiLine"></asp:TextBox>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="txtAddress" ErrorMessage="Invalid Address"></asp:RequiredFieldValidator>
                </td>

            </tr>

            <tr>
                <td width="100">Father Name</td>
                <td width="340"><asp:TextBox ID="txtFName" runat="server" Width="340"></asp:TextBox>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ControlToValidate="txtFName" ErrorMessage="Invalid Father Name"></asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td width="100">Mobile No</td>
                <td width="340"><asp:TextBox ID="txtMobNo" runat="server" Width="340px"></asp:TextBox>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                        ControlToValidate="txtMobNo" ErrorMessage="Invalid Mobile Number"></asp:RequiredFieldValidator>
                </td>
                </td>
            </tr>
            <tr>
                <td width="100">Email</td>
                <td width="340"><asp:TextBox ID="txtEmail" runat="server" Width="340"></asp:TextBox>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                        ControlToValidate="txtEmail" ErrorMessage="RequiredFieldValidator" 
                        SetFocusOnError="True">Invalid Email</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                        ControlToValidate="txtEmail" ErrorMessage="RegularExpressionValidator" 
                        SetFocusOnError="True" 
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">Invalid 
                    Email</asp:RegularExpressionValidator>
                </td>
            </tr>

            <tr>
                <td></td>
                <td>
                    <br />
                    <asp:Label ID="lblStatus" runat="server"></asp:Label>
                    <br />
                    <br />
                    <asp:Button ID="btnSave" runat="server" Text="Update Records" /> 
                </td>
            </tr>

        </table>
    </div>
</asp:Content>

