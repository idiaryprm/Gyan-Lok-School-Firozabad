<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/FeeMasterPage.master" CodeBehind="FeeTypeConfig.aspx.vb" Inherits="iDiary_V3.FeeTypeConfig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FeeMasterContents" runat="server">
   
    <table class="table">
        <tr>
            <td><b>Select Fee Type to be treated as:</b></td>
        </tr>
        <tr>
            <td>
                <b>Admission Fee</b><br />
                <asp:DropDownList ID="cboAddmissionFee" runat="server" CssClass="Dropdown">
                </asp:DropDownList>
                <br />
                <b>Tution Fee<br />
                    <asp:DropDownList ID="cboTutionFee" runat="server" CssClass="Dropdown">
                    </asp:DropDownList>
                </b>
                <br />
                <b>Late Fee<br />
                    <asp:DropDownList ID="cboLateFee" runat="server" CssClass="Dropdown">
                    </asp:DropDownList>
                    <br />
                    Conveyance Fee<br />
                    <asp:DropDownList ID="cboConveyanceFee" runat="server" CssClass="Dropdown">
                    </asp:DropDownList>
                    <br />
                Arrear Fee<br />
                    <asp:DropDownList ID="cboArrear" runat="server" CssClass="Dropdown">
                    </asp:DropDownList>
                    <br />
                    Excess Fee<br />
                    <asp:DropDownList ID="cboExcessFeeID" runat="server" CssClass="Dropdown">
                    </asp:DropDownList>
                    <br />
                </b>
                <asp:Label ID="lblStatus" runat="server" Style="font-weight: 700; color: #FF0000"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" />
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
    </table>

</asp:Content>
