<%@ Page Language="VB" MasterPageFile="~/iDiaryPanel/Parent/ParentMaster.master" AutoEventWireup="false" Inherits="iDiary_V3.Parent_ViewProgressReport" title="Untitled Page" Codebehind="ViewProgressReport.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="cntbox">
	    <h2><span>Progress Report</span></h2>
        
        <p><B>Term-1</B></p>
        
        <table border="1" width="440px" cellpadding=2 cellspacing=2>
            <tr>
                <td width="100" align="center"><B>Subject</B></td>
                <td width="40" align="center"><B>Grade</B></td>
                <td width="300" align="center"><B>Teacher Comments</B></td>
            </tr>
        
            <tr>
                <td width="100" align="center">English</td>
                <td width="40" align="center">B</td>
                <td width="300" align="center">
                        <ul class="mrt10">
                            <li>Poor Handwriting</li>
                            <li>Lot of Spelling Mistakes</li>    
                            <li>Please pay attention</li>    
    	                </ul>
                </td>
            </tr>

            <tr>
                <td width="100" align="center">Hindi</td>
                <td width="40" align="center">A+</td>
                <td width="300" align="center">
                        <ul class="mrt10">
                            <li>Excellent</li>
    	                </ul>
                </td>
            </tr>

            <tr>
                <td width="100" align="center">Mathematics</td>
                <td width="40" align="center">C</td>
                <td width="300" align="center">
                        <ul class="mrt10">
                            <li>Poor performance</li>
                            <li>Need extra efforts</li>
                            <li>Pay attention on tables.</li>
    	                </ul>
                </td>
            </tr>
            
            <tr>
                <td width="100" align="center">Art / Craft</td>
                <td width="40" align="center">A+</td>
                <td width="300" align="center">
                        <ul class="mrt10">
                            <li>Excellent</li>
                            <li>Very Creative</li>
    	                </ul>
                </td>
            </tr>
        </table>
        
    </div>
</asp:Content>

