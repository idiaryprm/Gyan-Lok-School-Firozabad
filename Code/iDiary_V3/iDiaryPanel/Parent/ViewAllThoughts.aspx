<%@ Page Language="VB" MasterPageFile="~/iDiaryPanel/Parent/ParentMaster.master" AutoEventWireup="false" Inherits="iDiary_V3.Parent_ViewAllThoughts" title="Untitled Page" Codebehind="ViewAllThoughts.aspx.vb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Heading" runat="server">
    Thoughts
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <div class="col_3" style="margin-top: 20px;" id="panelEnquiryNo" runat="server">
        <div class="col-md-3 widget widget1" style="width: 100%">
            <div class="r3_counter_box">
                <asp:DataList ID="DataList1" runat="server" DataSourceID="SqlDataSource1" 
                    BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" 
                    CellPadding="4" ForeColor="Black" GridLines="Vertical" Width="440px">
                    <FooterStyle BackColor="#CCCC99" />
                    <AlternatingItemStyle BackColor="White" />
                    <ItemStyle BackColor="#F7F7DE" />
                    <SelectedItemStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                    <ItemTemplate>
                        Thought:
                        <asp:Label ID="ThoughtLabel" runat="server" Text='<%# Eval("Thought") %>' />
                        <br />
                        Submitted By:
                        <asp:Label ID="SubmitByLabel" runat="server" 
                            Text='<%# Eval("SubmitBy") %>' />
                        <br />
                        Class:
                        <asp:Label ID="ClassNameLabel" runat="server" 
                            Text='<%# Eval("CSSName") %>' />
                        <br />
                        Submited On:
                        <asp:Label ID="submitDateLabel" runat="server" 
                            Text='<%# Eval("submitDate", "{0:dd/MM/yyyy}") %>' />
                        <br />
                        <br />
                    </ItemTemplate>
                </asp:DataList>
                
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:iDiaryConnectionString %>" 
                    
                    SelectCommand="SELECT Thought, SName as SubmitBy, CSSName, submitDate FROM vw_Thoughts">
                </asp:SqlDataSource>
            </div>
        </div>
        <div class="clearfix"></div>
    </div>
</asp:Content>