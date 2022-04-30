<%@ Page Language="VB" MasterPageFile="~/AdminMaster.master" AutoEventWireup="false" Inherits="iDiary_V3.Admin_AdminHome" title="Untitled Page" Codebehind="AdminHome.aspx.vb" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminMasterContents" Runat="Server">
        <style type="text/css" media="screen" >
        .accordionContent {
            background-color: #D3DEEF;
            border-color: -moz-use-text-color #98AA00 #98AA00;
            border-right: 1px dashed #98AA00;
            border-style: none dashed dashed;
            border-width: medium 1px 1px;
            padding: 10px 5px 5px;
            width: 280px;
        }
 
        .accordionHeaderSelected {
            background-color: #980000;
            border: 1px solid #990000;
            color: white;
            cursor: pointer;
            font-family: Arial,Sans-Serif;
            font-size: 12px;
            font-weight: bold;
            margin-top: 5px;
            padding: 5px;
            width: 280px;
        }
 
        .accordionHeader {
            background-color: #980000;
            border: 1px solid #990000;
            color: white;
            cursor: pointer;
            font-family: Arial,Sans-Serif;
            font-size: 12px;
            font-weight: bold;
            margin-top: 5px;
            padding: 5px;
            width: 280px;
        }
 
        .href {
            color: White;
            font-weight: bold;
            text-decoration: none;
        }
         .auto-style1 {
             color: #CC6600;
         }
         .auto-style2 {
             color: #CC6600;
             height: 20px;
         }
         .auto-style4 {
             color: #CC6600;
             height: 24px;
             width: 60%;
         }
         </style>
    

    <br />
    <br />

                <%--<asp:Accordion ID="AccordianMain" runat="server" SelectedIndex="0" HeaderCssClass="accordionHeader"
                HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent" FadeTransitions="true" SuppressHeaderPostbacks="true" TransitionDuration="250" FramesPerSecond="40" RequireOpenedPane="false" AutoSize="None">
        <Panes>
            <asp:AccordionPane ID="Acc1" runat="server" >
                <Header >
                  
                    <asp:Label ID="lblStudentStrength"  runat="server"  Font-Bold="True" Font-Size="Large" style="font-size: medium" ></asp:Label>
                </Header>
                <Content>
                    <asp:Label ID="lblStudentDetail" Text="Student" runat="server" Font-Size="Medium" style="font-size: medium"></asp:Label><br />
                    <asp:Label ID="lblStudentDetail1" Text="Student" runat="server"   Font-Size="Medium" style="font-size: medium"></asp:Label><br />
                    <asp:Label ID="lblStudentDetail2" Text="Student" runat="server"   Font-Size="Medium" style="font-size: medium"></asp:Label><br />
                    <asp:Label ID="lblStudentDetail3" Text="Student" runat="server"   Font-Size="Medium" style="font-size: medium"></asp:Label><br />
                </Content>
            </asp:AccordionPane>
            
            <asp:AccordionPane ID="Acc2" runat="server"  >
                <Header>
                    <asp:Label ID="lblFee" Text="fee"  runat="server"  Font-Bold="True" Font-Size="Large" style="font-size: medium" ></asp:Label>
                </Header>
                <Content>
                    <asp:Label ID="lblFeeDetail" Text="fee"  runat="server"   Font-Size="Medium" style="font-size: medium"></asp:Label><br />

                     <asp:Label ID="lblFeeDetail1" Text="fee"  runat="server"   Font-Size="Medium" style="font-size: medium"></asp:Label><br />
                </Content>
            </asp:AccordionPane>
            <asp:AccordionPane ID="Acc3" runat="server"  >
                <Header>
                    <asp:Label ID="lblEmployee" text="Employee" runat="server"  Font-Bold="True" Font-Size="Large" style="font-size: medium"></asp:Label>
                </Header>
                <Content>
                    <asp:Label ID="lblEmployeeDetail" text="Employee" runat="server"   Font-Size="Medium" style="font-size: medium"></asp:Label><br />

                    <asp:Label ID="lblEmployeeDetail1" text="Employee" runat="server"   Font-Size="Medium"  style="font-size: medium"></asp:Label><br />
                </Content>
            </asp:AccordionPane>
             <asp:AccordionPane ID="Acc4" runat="server"  >
                <Header>
                    <asp:Label ID="lblPettyCash" text="Employee" runat="server"  Font-Bold="True" Font-Size="Large" style="font-size: medium"></asp:Label>
                </Header>
                <Content>
                    <asp:Label ID="lblPettyCash1" text="Employee" runat="server"   Font-Size="Medium" style="font-size: medium"></asp:Label><br />

                    <asp:Label ID="lblPettyCash2" text="Employee" runat="server"   Font-Size="Medium"  style="font-size: medium"></asp:Label><br />
                </Content>
            </asp:AccordionPane>

        </Panes>

    </asp:Accordion>--%>
                 
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <br />
    <br />
</asp:Content>

