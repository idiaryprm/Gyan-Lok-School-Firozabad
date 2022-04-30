<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="Error.aspx.vb" Inherits="iDiary_V3._Error" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Heading" runat="server">Error Occured
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td align="center" >

                <asp:ImageMap ID="ImageMap1" runat="server" HotSpotMode="PostBack" ImageUrl="~/Images/error.png">
                    <asp:RectangleHotSpot Bottom="320" HotSpotMode="Navigate" Left="66" NavigateUrl="~/Index.aspx" Right="370" Top="280" />
                </asp:ImageMap>

            </td>
        </tr>

    </table>
</asp:Content>
