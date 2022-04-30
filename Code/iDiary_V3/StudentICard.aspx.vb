Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Imports Microsoft.Reporting.WebForms

Public Class StudentICard
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        Try

            If Request.Cookies("UType").Value.ToString.Contains("Student") Or Request.Cookies("UType").Value.ToString.Contains("Admin") Then
                'Allow
            Else

                ' tmp += tmp
                Response.Redirect("/./AccessDenied.aspx")
                Exit Sub
            End If

        Catch ex As Exception

            If ex.Message.Contains("Object reference not set to an instance of an object") Then
                '  tmp += tmp
                Response.Redirect("~/Login.aspx")
                Exit Sub
            End If

        End Try

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cookies("ActiveTab").Value = 2
        Response.Cookies("ActiveTab").Expires = DateTime.Now.AddHours(1)
        Session("ActiveTab") = 2
        If IsPostBack = False Then
            InitControls()
        End If
    End Sub

    Private Sub InitControls()
        ReportViewer1.Visible = False
        ReportViewer1.LocalReport.EnableExternalImages = True
        optSpecific.Checked = True
        LoadMasterInfo(71, cboSchoolName, Request.Cookies("SchoolIDs").Value)
        LoadMasterInfo(2, cboClass, cboSchoolName.Text)
        cboSection.Items.Clear()
    End Sub

    Protected Sub cboClass_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboClass.SelectedIndexChanged
        LoadClassSection(cboSchoolName.Text, cboClass.Text, cboSection)
    End Sub

    Protected Sub btnGenerate_Click(sender As Object, e As EventArgs) Handles btnGenerate.Click
        If optClass.Checked = True Then
            If Trim(cboSchoolName.Text) = "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('School Name is required...');", True)
                cboSchoolName.Focus()
                Exit Sub
            End If
            If Trim(cboClass.Text) = "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Class is required...');", True)
                cboClass.Focus()
                Exit Sub
            End If
            If Trim(cboSection.Text) = "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Section is required...');", True)
                cboSection.Focus()
                Exit Sub
            End If
        End If
        Dim sqlStr As String = ""
        sqlStr &= "Select RegNo, SName, FNAme, FatherAddress, MobNo, ClassName, SecName,PhotoPath,SchoolName  From vw_Student Where ASID=" & Request.Cookies("ASID").Value
        If optSpecific.Checked = True Then
            sqlStr &= " AND  RegNo='" & txtRegNo.Text & "' AND SchoolName='" & cboSchoolName.Text & "'"
        ElseIf optClass.Checked = True Then
            sqlStr &= " and SchoolName='" & cboSchoolName.Text & "' AND  ClassName='" & cboClass.Text & "' AND SecName='" & cboSection.Text & "'"
        ElseIf optAll.Checked = True Then
            'Do Nothing (All Student under Academic Session)
        End If
        Dim ds As New DataSet
        ds = ExecuteQuery_DataSet(sqlStr, "tbl")
        If optSpecific.Checked = True Then
            If ds.Tables(0).Rows.Count = 0 Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Reg. No...');", True)
                txtRegNo.Focus()
                Exit Sub
            End If
        End If
        Dim rds As ReportDataSource = New ReportDataSource()
        rds.Name = "DataSet1" ' Change to what you will be using when creating an objectdatasource
        rds.Value = ds.Tables(0)
        With ReportViewer1   ' Name of the report control on the form
            .Reset()
            .ProcessingMode = ProcessingMode.Local
            .LocalReport.DataSources.Clear()
            .Visible = True
            .LocalReport.ReportPath = "Report/rptStudentICard.rdlc"
            .LocalReport.EnableExternalImages = True
            .LocalReport.DataSources.Add(rds)
        End With

        Dim params(1) As Microsoft.Reporting.WebForms.ReportParameter
        Dim path As String = Server.MapPath("~")
        params(0) = New Microsoft.Reporting.WebForms.ReportParameter("paraPATH", path, Visible)
        params(1) = New Microsoft.Reporting.WebForms.ReportParameter("SchoolName", cboSchoolName.Text, True)
        Me.ReportViewer1.LocalReport.SetParameters(params)
        ReportViewer1.Visible = True
        ReportViewer1.LocalReport.Refresh()
    End Sub

    Protected Sub optSpecific_CheckedChanged(sender As Object, e As EventArgs) Handles optSpecific.CheckedChanged
        EnableDisable(1)
    End Sub

    Private Sub EnableDisable(ByVal myType As Integer)
        If myType = 1 Then    'Specific
            PanelSpecific.Enabled = True
            PanelClass.Enabled = False
        ElseIf myType = 2 Then  'Class-wise
            PanelSpecific.Enabled = False
            PanelClass.Enabled = True
        ElseIf myType = 3 Then    'All
            PanelSpecific.Enabled = False
            PanelClass.Enabled = False
        End If
    End Sub

    Protected Sub optClass_CheckedChanged(sender As Object, e As EventArgs) Handles optClass.CheckedChanged
        EnableDisable(2)
    End Sub

    Protected Sub optAll_CheckedChanged(sender As Object, e As EventArgs) Handles optAll.CheckedChanged
        EnableDisable(3)
    End Sub

    Protected Sub cboSchoolName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSchoolName.SelectedIndexChanged
        LoadMasterInfo(2, cboClass, cboSchoolName.Text)
        cboSchoolName.Focus()
    End Sub
End Class