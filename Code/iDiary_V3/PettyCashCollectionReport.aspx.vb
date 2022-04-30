Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Imports iDiary_V3.iDiary_Fee.CLS_iDiary_Fee
Imports Microsoft.Reporting.WebForms

Public Class PettyCashCollectionReport
    Inherits System.Web.UI.Page

    Protected Sub btnView_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnView.Click
        If chkregno.Checked = True Then
            If txtregistrationno.Text = "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Enter Registration No......');", True)
                lblStatus.Text = "Enter Registration No......"
                txtregistrationno.Focus()
                Exit Sub
            End If
        Else
            If cboClassGroup.Text = "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid School Account...');", True)
                lblStatus.Text = "Invalid School Account..."
                cboClassGroup.Focus()
                Exit Sub
            End If
            If cboClass.Text = "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Class...');", True)
                lblStatus.Text = "Invalid Class..."
                cboClass.Focus()
                Exit Sub
            End If
            If cboSection.Text = "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Section...');", True)
                lblStatus.Text = "Invalid Section..."
                cboSection.Focus()
                Exit Sub
            End If
            'ALL-XXX (Not Allowed...)
            If cboClass.Text = "ALL" And cboSection.Text <> "ALL" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Section (Please Select --ALL--)');", True)
                lblStatus.Text = "Invalid Section (Please Select --ALL--)"
                cboSection.Focus()
                Exit Sub
            End If
            If chkPettyCash.Checked = True And cboPettyCashHead.Text = "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Petty Cash Head...');", True)
                lblStatus.Text = "Invalid Petty Cash Head..."
                cboPettyCashHead.Focus()
                Exit Sub
            End If
            If cboStatus.Text = "" And chkStatus.Checked = True Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Status...');", True)
                lblStatus.Text = "Invalid Status..."
                cboStatus.Focus()
                Exit Sub
            End If
        End If


        If txtFrom.Text = "" Or txtTo.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Dates...');", True)
            lblStatus.Text = "Invalid Dates..."
            txtFrom.Focus()
            Exit Sub
        End If


        lblStatus.Text = ""
        Dim FromDate1 As Date = Now.Date
        Dim ToDate1 As Date = Now.Date
        Try
            FromDate1 = txtFrom.Text.Substring(6, 4) & "/" & txtFrom.Text.Substring(3, 2) & "/" & txtFrom.Text.Substring(0, 2)
            ToDate1 = txtTo.Text.Substring(6, 4) & "/" & txtTo.Text.Substring(3, 2) & "/" & txtTo.Text.Substring(0, 2)
        Catch ex As Exception
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Dates Please Check Dates dd/mm/yyyy Format...');", True)
            Exit Sub
        End Try
        'Process Report
        'ProcessFeeCollectionReport(txtFrom.Text.Substring(6, 4) & "/" & txtFrom.Text.Substring(3, 2) & "/" & txtFrom.Text.Substring(0, 2), txtTo.Text.Substring(3, 2) & "/" & txtTo.Text.Substring(0, 2) & "/" & txtTo.Text.Substring(6, 4), Request.Cookies("ASID").Value)
        'Dim FromDate1 As String = txtFrom.Text.Substring(6, 4) & "/" & txtFrom.Text.Substring(3, 2) & "/" & txtFrom.Text.Substring(0, 2)
        'Dim ToDate1 As String = txtTo.Text.Substring(6, 4) & "/" & txtTo.Text.Substring(3, 2) & "/" & txtTo.Text.Substring(0, 2)

        'Dim params(0) As Microsoft.Reporting.WebForms.ReportParameter

        'Me.ReportViewer1.LocalReport.SetParameters(params)
        PrepareReport(FromDate1.ToString("yyyy/MM/dd"), ToDate1.ToString("yyyy/MM/dd"))
        ReportViewer1.Visible = True
        ReportViewer1.LocalReport.Refresh()
    End Sub
    Private Sub PrepareReport(FromDate As String, ToDate As String)
        'Dim FromDate As String = txtFrom.Text.Substring(6, 4) & "/" & txtFrom.Text.Substring(3, 2) & "/" & txtFrom.Text.Substring(0, 2)
        'Dim ToDate As String = txtTo.Text.Substring(6, 2) & "/" & txtTo.Text.Substring(0, 2) & "/" & txtTo.Text.Substring(6, 4)
        Dim Sql As String = "Select * from vwPettyCashTransaction"
        Sql += " where vr_dt Between '" & FromDate & "' AND '" & ToDate & "' and IsDeleted=0"
        ' AND ASID=" & Request.Cookies("ASID").Value
        If chkStatus.Checked = True Then
            Sql += " and StatusName='" & cboStatus.Text & "'"
        End If
        If chkregno.Checked = True Then
            Sql += "And RegNo='" & txtregistrationno.Text & "'"
        Else
            If chkPettyCash.Checked = True Then
                Sql += "And PCHeadName='" & cboPettyCashHead.Text & "'"
            End If
            If cboClassGroup.Text <> "ALL" Then
                Sql += " And SchoolName='" & SQLFixup(cboClassGroup.Text) & "' "
                'and (tmpClassGroupName='" & cboClassGroup.Text & "' or tmpClassGroupName is null) "
            End If
            If cboClass.Text <> "ALL" Then
                Sql += " And ClassName='" & cboClass.Text & "' "
            End If
            If cboClass.Text <> "ALL" And cboSection.Text <> "ALL" Then
                Sql += " And SecName='" & cboSection.Text & "' "
            ElseIf cboClass.Text <> "ALL" And cboSection.Text = "ALL" Then
                Sql += " And SecID in (Select distinct SecID From vw_ClassStudent where ClassName='" & cboClass.Text & "')"
            End If
        End If
        If Request.Cookies("UType").Value.Contains("Admin") = False Then
            Sql += " And PettyCashUserID='" & Request.Cookies("UserID").Value & "'"
        End If


        Sql += " order by vr_dt,vr_NoNumeric"
        Dim ds As New DataSet
        ds = ExecuteQuery_DataSet(Sql, "tbl")
        Dim rds As ReportDataSource = New ReportDataSource()
        rds.Name = "DataSet1" ' Change to what you will be using when creating an objectdatasource
        rds.Value = ds.Tables(0)
        With ReportViewer1   ' Name of the report control on the form
            .Reset()
            .ProcessingMode = ProcessingMode.Local
            .LocalReport.DataSources.Clear()
            .Visible = True
            If cboReportType.SelectedIndex = 0 Then
                .LocalReport.ReportPath = "Report/rptPettyCashCollection.rdlc"
            ElseIf cboReportType.SelectedIndex = 1 Then
                .LocalReport.ReportPath = "Report/rptPettyCashUserWise.rdlc"
            ElseIf cboReportType.SelectedIndex = 2 Then
                .LocalReport.ReportPath = "Report/rptPettyCashRcpt.rdlc"
            Else
                .LocalReport.ReportPath = "Report/rptPettyCashHeadWiseCollection.rdlc"
            End If

            .LocalReport.DataSources.Add(rds)
        End With
        Dim ClassHeader As String = Request.Cookies("ASName").Value
        Dim params(0) As Microsoft.Reporting.WebForms.ReportParameter
        params(0) = New Microsoft.Reporting.WebForms.ReportParameter("MyHeader", "Cash Collection Report From " & txtFrom.Text & " - " & txtTo.Text, True)
        Me.ReportViewer1.LocalReport.SetParameters(params)
        ReportViewer1.Visible = True
        ReportViewer1.LocalReport.Refresh()
    End Sub
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try
            If Request.Cookies("UType").Value.ToString.Contains("Petty Cash-1") Or Request.Cookies("UType").Value.ToString.Contains("Admin-1") Then
                'Allow
            Else
                Response.Redirect("/./AccessDenied.aspx", False)
            End If
        Catch ex As Exception
            Response.Redirect("~/Login.aspx")
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("ActiveTab") = 8
        Response.Cookies("ActiveTab").Value = 8
        Response.Cookies("ActiveTab").Expires = DateTime.Now.AddHours(1)
        If IsPostBack = False Then InitControls()
    End Sub

    Private Sub InitControls()
        If chkregno.Checked = True Then
            txtregistrationno.Enabled = True
            txtregistrationno.Focus()
        Else
            txtregistrationno.Enabled = False
            txtregistrationno.Text = ""

        End If
        txtFrom.Text = Now.Date.ToString("dd/MM/yyyy")
        txtFrom_CalendarExtender.EnableViewState = True
        txtTo.Text = Now.Date.ToString("dd/MM/yyyy")
        txtTo_CalendarExtender.EnableViewState = True

        ReportViewer1.Visible = False

        LoadMasterInfo(72, cboClassGroup)
        cboClassGroup.Items.Add("ALL")
        'LoadMasterInfo(12, cboMode)
        LoadMasterInfo(10, cboStatus)
        LoadPettyCashHeads(cboPettyCashHead)
        Try
            cboStatus.Text = FindDefault(10)
        Catch ex As Exception

        End Try
        LoadMasterInfo(2, cboClass)
        cboClass.Items.Add("ALL")
        'cboClassGroup.Items.Add("ALL")
        'cboMode.Items.Add("ALL")
        'cboClass.Items.Clear()
        cboSection.Items.Clear()
        cboClassGroup.Focus()
    End Sub
    Public Shared Function LoadPettyCashHeads(cbo As DropDownList) As Integer





        Dim sqlstr As String = "Select PCHeadName From PettyCashHeadMaster Order By DisplayOrder"




        Dim myReader As System.Data.SqlClient.SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)
        cbo.Items.Clear()
        cbo.Items.Add("")
        While myReader.Read
            cbo.Items.Add(myReader(0))
        End While
        myReader.Close()




        Return 0

    End Function
    Protected Sub cboClassGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboClassGroup.SelectedIndexChanged
        'Dim FeeGroupId As Integer = FindMasterID(70, cboClassGroup.Text)
        'LoadClassByClassGroup(cboClass, FeeGroupId)
        'cboClass.Items.Add("ALL")
    End Sub

    Protected Sub cboClass_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboClass.SelectedIndexChanged
        LoadClassSection(cboClass.Text, cboSection)
        cboSection.Items.Add("ALL")
        If cboClass.Text = "ALL" Then
            cboSection.Text = "ALL"
        End If
    End Sub


    Protected Sub chkregno_CheckedChanged(sender As Object, e As EventArgs) Handles chkregno.CheckedChanged
        If chkregno.Checked = True Then
            txtregistrationno.Enabled = True
            txtregistrationno.Focus()
        Else
            txtregistrationno.Enabled = False
            txtregistrationno.Text = ""

        End If
    End Sub

    Protected Sub chkPettyCash_CheckedChanged(sender As Object, e As EventArgs) Handles chkPettyCash.CheckedChanged
        If chkPettyCash.Checked = True Then
            cboPettyCashHead.Enabled = True
        Else
            cboPettyCashHead.Enabled = False
        End If
    End Sub
End Class