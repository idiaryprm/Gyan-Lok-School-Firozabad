Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Imports iDiary_V3.iDiary.CLS_iDiary_Exam
Imports System.Drawing
Imports Microsoft.Reporting.WebForms


Public Class Exam_ReportCard
    Inherits System.Web.UI.Page


    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try

            If Request.Cookies("UType").Value.ToString.Contains("Exam") Or Request.Cookies("UType").Value.ToString.Contains("Admin") Then
                'Allow
            Else
                Response.Redirect("AccessDenied.aspx")
            End If

        Catch ex As Exception

            If ex.Message.Contains("Object reference not set to an instance of an object") Then
                Response.Redirect("Login.aspx")
            End If

        End Try

    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' lblGrpID.Text = Request.QueryString("grpID").ToString
        Session("ActiveTab") = 7
        If IsPostBack = False Then
            InitControls()
        Else

        End If
    End Sub

    Private Sub InitControls()

        LoadMasterInfo(71, cboSchoolName, Request.Cookies("SchoolIDs").Value)
        lblSchoolID.Text = getSchoolID(cboSchoolName.SelectedItem.Text)
        LoadMasterInfo(101, cboExamGroup, cboSchoolName.SelectedItem.Text)
        lblGrpID.Text = FindMasterID(101, cboExamGroup.Text)

        cboExamGroup.Focus()
        cboClass.Items.Clear()
        cboSection.Items.Clear()
        cboTerm.Items.Clear()
        LoadMasterInfo(10, cboStatus)
        txtReportDate.Text = Now.Date.ToString("dd-MM-yyyy")
    End Sub

    Protected Sub cboClass_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboClass.SelectedIndexChanged
        LoadClassSections(cboClass.SelectedItem.Text, cboSection)
        cboClass.Focus()
    End Sub

    Protected Sub btnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        If Trim(cboExamGroup.Text) = "" Then
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Error", "alert('Please Select Exam Group to continue...');", True)
            cboExamGroup.Focus()
            Exit Sub
        End If
        If Trim(cboClass.Text) = "" Then
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Error", "alert('Please Select Class to continue...');", True)
            cboClass.Focus()
            Exit Sub
        End If
        If Trim(cboSection.Text) = "" Then
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Error", "alert('Please Select Section to continue...');", True)
            cboClass.Focus()
            Exit Sub
        End If
        If Trim(cboTerm.Text) = "" Then
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Error", "alert('Please Select Term to continue...');", True)
            cboTerm.Focus()
            Exit Sub
        End If
        If Trim(cboStatus.Text) = "" Then
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Error", "alert('Please Select Status to continue...');", True)
            cboStatus.Focus()
            Exit Sub
        End If
        Dim tmpDate As Date = Now.Date
        If Trim(txtReportDate.Text) = "" Then
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Error", "alert('Reportcard generation date is required...');", True)
            txtReportDate.Focus()
            Exit Sub
        End If
        Dim opendate As Date = Now.Date
        'If Trim(txtopenDate.Text) = "" Then
        '    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Error", "alert('School Re-open date is required...');", True)
        '    txtopenDate.Focus()
        '    Exit Sub
        'End If
        Try
            tmpDate = CDate(txtReportDate.Text.Substring(6, 4) & "/" & txtReportDate.Text.Substring(3, 2) & "/" & txtReportDate.Text.Substring(0, 2))
            opendate = CDate(txtopenDate.Text.Substring(6, 4) & "/" & txtopenDate.Text.Substring(3, 2) & "/" & txtopenDate.Text.Substring(0, 2))
        Catch ex As Exception
            'ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Error", "alert('Invalid Date...');", True)
            'txtReportDate.Focus()
            'Exit Sub
        End Try
        Dim sqlStr As String = "", rdlcName As String = ""
        Try
            sqlStr = "Select top(1) rdlcName from ExamReportName Where ExamGroupID=" & lblGrpID.Text & " ANd MajorTermID=" & cboTerm.SelectedItem.Value & " AND ASID=" & Request.Cookies("ASID").Value
            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            While myReader.Read
                rdlcName = myReader("rdlcName")
            End While
            myReader.Close()
        Catch ex As Exception

        End Try
        If rdlcName = "" Then
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Error", "alert('No Reportcard Configured for selected Term...');", True)
            cboTerm.Focus()
            Exit Sub
        End If
        Try
            rdlcName = ExecuteQuery_ExecuteScalar(sqlStr)
            sqlStr = "SELECT * from vw_Exam_Report where ClassName='" & cboClass.SelectedItem.Text & "' AND SecName='" & cboSection.SelectedItem.Text & "' AND StatusName='" & cboStatus.Text & "' AND SubjectMappingASID='" & Request.Cookies("ASID").Value & "' AND ASID=" & Request.Cookies("ASID").Value & " AND SCHOOLNAME='" & cboSchoolName.SelectedItem.Text & "'"
            Dim MajorTermID As String = ""
            For i = 0 To cboTerm.SelectedIndex
                MajorTermID += cboTerm.Items(i).Value & ","
            Next
            If MajorTermID <> "" Then
                MajorTermID = MajorTermID.Substring(0, MajorTermID.Length - 1)
                sqlStr += " AND MajorTermID in(" & MajorTermID & ")"
            End If
            If chkRegno.Checked = True Then
                sqlStr &= " and Regno='" & txtRegno.Text & "'"
            End If
        Catch ex As Exception

        End Try
        PrepareReport(sqlStr, rdlcName, "Report Card :" & cboClass.SelectedItem.Text & "-" & cboSection.SelectedItem.Text, "", tmpDate.ToString("dd-MM-yyyy"))
        sqlStr = "Update ExamReportName Set reportDate='" & tmpDate.ToString("yyyy-MM-dd") & "' Where ExamGroupID=" & lblGrpID.Text & " ANd MajorTermID=" & cboTerm.SelectedItem.Value & " AND ASID=" & Request.Cookies("ASID").Value
        ExecuteQuery_Update(sqlStr)

    End Sub

    Private Sub PrepareReport(ByVal sqlString As String, ByVal reportName As String, MyHeader As String, rptParam As String, rptDate As String)

        Dim ds As New DataSet, lstDIubjects As New List(Of Integer), sqlStr As String = ""

        ds = ExecuteQuery_DataSet(sqlString, "tbl")
        sqlStr = "select Distinct SubjectID from vw_ExamSubjectMapping where ASID=" & Request.Cookies("ASID").Value & " AND ClassName='" & cboClass.SelectedItem.Text & "' and SecName='" & cboSection.SelectedItem.Text & "' and displaytype=2"
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            lstDIubjects.Add(myReader(0))
        End While
        If lstDIubjects.Count > 0 Then
            ds = ProcessDSforIndicator(ds, lstDIubjects, cboTerm.SelectedItem.Text, cboClass.SelectedItem.Value)
        End If
        Dim rds As ReportDataSource = New ReportDataSource()
        rds.Name = "DataSet1" ' Change to what you will be using when creating an objectdatasource
        rds.Value = ds.Tables(0)

        Dim rptReport As String = "Exam_Reports/"
        With ReportViewer1   ' Name of the report control on the form
            .Reset()
            .ProcessingMode = ProcessingMode.Local
            .LocalReport.DataSources.Clear()
            .Visible = True
            .LocalReport.ReportPath = rptReport & reportName
            .LocalReport.DataSources.Add(rds)
            .LocalReport.EnableExternalImages = True
        End With
        'Dim classGroupID As Integer = 0
        'Try
        '    classGroupID = ExecuteQuery_ExecuteScalar("Select top 1 ClassGroupID from Classes Where ClassName='" & cboClass.SelectedItem.Text & "'")
        'Catch ex As Exception
        '    classGroupID = 0
        'End Try
        Dim schooldate As String = txtopenDate.Text
        Dim SchoolName As String = cboSchoolName.SelectedItem.Text
        Dim SchoolID As Int16 = FindMasterID(71, SchoolName)
        Dim SchoolAddress As String = FindSchoolDetails1(SchoolID, 1)
        Dim ASName As String = Request.Cookies("ASName").Value
        Dim params(7) As Microsoft.Reporting.WebForms.ReportParameter
        params(0) = New Microsoft.Reporting.WebForms.ReportParameter("SchoolName", SchoolName, True)
        params(1) = New Microsoft.Reporting.WebForms.ReportParameter("SchoolAddress", SchoolAddress, True)
        params(2) = New Microsoft.Reporting.WebForms.ReportParameter("ASName", ASName, True)
        params(3) = New Microsoft.Reporting.WebForms.ReportParameter("MyHeader", MyHeader, True)
        params(4) = New Microsoft.Reporting.WebForms.ReportParameter("ImagePath", Server.MapPath("~/"), Visible)
        params(5) = New Microsoft.Reporting.WebForms.ReportParameter("ImageSign", Server.MapPath("~/images/signature.png"), Visible)
        params(6) = New Microsoft.Reporting.WebForms.ReportParameter("rptDate", txtReportDate.Text, Visible)
        params(7) = New Microsoft.Reporting.WebForms.ReportParameter("Scldate", txtopenDate.Text, Visible)

        Me.ReportViewer1.LocalReport.SetParameters(params)

        ReportViewer1.Visible = True
        ReportViewer1.LocalReport.Refresh()
    End Sub

    Private Function ProcessDSforIndicator(ByVal ds As DataSet, lstDIsubjects As List(Of Integer), termName As String, classID As Integer) As DataSet
        Dim SID As Integer = 0, SubjectID As Integer = 0, grade As String = "", DesIndicator As String = "", asid As Integer = Request.Cookies("ASID").Value, SName As String = "", Gender As Integer = 0, proNoun As String = "", inputType As Integer = 0

        For Each Row As DataRow In ds.Tables(0).Rows
            SID = Row("SID")
            SubjectID = Row("SubjectID")
            SName = Row("SName")
            inputType = Row("EntryType")
            If lstDIsubjects.Contains(SubjectID) Then
                Try
                    'change on 20170905
                    'grade = Row(termName)
                    grade = Row("grade")
                Catch ex As Exception
                    Continue For
                End Try
                If inputType = 1 Then
                    Try
                        Gender = Row("Gender")
                        If Gender = 0 Then
                            proNoun = "his"
                        Else
                            proNoun = "her"
                        End If
                    Catch ex As Exception
                        Gender = 0
                    End Try
                    DesIndicator = getMappedDescIndicator(classID, SubjectID, grade, asid)
                    Try
                        DesIndicator = DesIndicator.Replace("<*>", SName)
                    Catch ex As Exception
                        DesIndicator = "--"
                    End Try
                    Try
                        DesIndicator = DesIndicator.Replace("<**>", proNoun)
                    Catch ex As Exception
                        '  DesIndicator = "--"
                    End Try
                    'change on 20170905
                    ''   Row("FinalTerm") = DesIndicator
                    Row("grade") = DesIndicator
                ElseIf inputType = 2 Then
                    Try
                        grade = grade.Split("/")(0)
                    Catch ex As Exception

                    End Try
                    DesIndicator = getRemark(grade)
                    'change on 20170905
                    'Row("FinalTerm") = DesIndicator
                    Row("grade") = DesIndicator
                End If


            Else
                'do nothing
            End If
        Next
        Return ds
    End Function


    'Private Function ProcessDSforIndicator(ByVal ds As DataSet, lstDIsubjects As List(Of Integer), termName As String, classID As Integer) As DataSet
    '    Dim SID As Integer = 0, SubjectID As Integer = 0, grade As String = "", DesIndicator As String = "", asid As Integer = Request.Cookies("ASID").Value, SName As String = "", Gender As Integer = 0, proNoun As String = "", inputType As Integer = 0

    '    'configured for static term SA1
    '    'termName = "SA1"
    '    For Each Row As DataRow In ds.Tables(0).Rows
    '        SID = Row("SID")
    '        SubjectID = Row("SubjectID")
    '        SName = Row("SName")
    '        inputType = Row("EntryType")
    '        If lstDIsubjects.Contains(SubjectID) Then
    '            Try
    '                grade = Row(termName)
    '            Catch ex As Exception
    '                Continue For
    '            End Try
    '            If inputType = 1 Then
    '                Try
    '                    Gender = Row("Gender")
    '                    If Gender = 0 Then
    '                        proNoun = "his"
    '                    Else
    '                        proNoun = "her"
    '                    End If
    '                Catch ex As Exception
    '                    Gender = 0
    '                End Try
    '                DesIndicator = getMappedDescIndicator(classID, SubjectID, grade, asid)
    '                Try
    '                    DesIndicator = DesIndicator.Replace("<*>", SName)
    '                Catch ex As Exception
    '                    DesIndicator = "--"
    '                End Try
    '                Try
    '                    DesIndicator = DesIndicator.Replace("<**>", proNoun)
    '                Catch ex As Exception
    '                    '  DesIndicator = "--"
    '                End Try
    '                Row("FinalTerm") = DesIndicator
    '            ElseIf inputType = 2 Then
    '                Try
    '                    grade = grade.Split("/")(0)
    '                Catch ex As Exception

    '                End Try
    '                DesIndicator = getRemark(grade)
    '                'change on 20170905
    '                'Row("FinalTerm") = DesIndicator
    '                Row("grade") = DesIndicator
    '            End If


    '        Else
    '            'do nothing
    '        End If
    '    Next
    '    Return ds
    'End Function
  
    Public Shared Function getRemark(ByVal remarkID As Integer) As String
        Dim sqlStr As String = "Select top 1 RemarkName From ExamRemarksMaster Where RemarkID=" & remarkID
        Dim myDI As String = ""
        Try
            myDI = ExecuteQuery_ExecuteScalar(sqlStr)
        Catch ex As Exception
            myDI = "--"
        End Try

        Return myDI
    End Function
    'Private Function ProcessDSforIndicator(ByVal ds As DataSet, lstDIsubjects As List(Of Integer), termName As String, classID As Integer) As DataSet
    '    Dim SID As Integer = 0, SubjectID As Integer = 0, grade As String = "", DesIndicator As String = "", asid As Integer = Request.Cookies("ASID").Value, SName As String = "", Gender As Integer = 0, proNoun0 As String = "", proNoun As String = ""
    '    For Each Row As DataRow In ds.Tables(0).Rows
    '        SID = Row("SID")
    '        SubjectID = Row("SubjectID")
    '        SName = Row("SName")
    '        If lstDIsubjects.Contains(SubjectID) Then
    '            Try
    '                grade = Row(termName)
    '            Catch ex As Exception
    '                Continue For
    '            End Try
    '            Try
    '                Gender = Row("Gender")
    '                If Gender = 0 Then
    '                    proNoun0 = "He"
    '                    proNoun = "his"
    '                Else
    '                    proNoun0 = "She"
    '                    proNoun = "her"
    '                End If
    '            Catch ex As Exception
    '                gender = 0
    '            End Try
    '            DesIndicator = getMappedDescIndicator(classID, SubjectID, grade, asid)
    '            Try
    '                DesIndicator = DesIndicator.Replace("<*>", SName)
    '            Catch ex As Exception
    '                DesIndicator = "--"
    '            End Try
    '            Try
    '                DesIndicator = DesIndicator.Replace(". <**>", ". " & proNoun0)
    '            Catch ex As Exception
    '                '  DesIndicator = "--"
    '            End Try
    '            Try
    '                DesIndicator = DesIndicator.Replace(".<**>", "." & proNoun0)
    '            Catch ex As Exception
    '                '  DesIndicator = "--"
    '            End Try
    '            Try
    '                DesIndicator = DesIndicator.Replace("<**>", proNoun)
    '            Catch ex As Exception
    '                '  DesIndicator = "--"
    '            End Try
    '            Row("FinalTerm") = DesIndicator
    '        Else
    '            'do nothing
    '        End If
    '    Next
    '    Return ds
    'End Function
    Public Shared Function getMappedDescIndicator(ByVal classID As Integer, ByVal SubjectID As Integer, ByVal grade As String, ByVal ASID As Integer) As String
        Dim sqlStr As String = "Select top 1 RemarkName From ExamRemarksMaster Where ASID=" & ASID & " AND ClassID=" & classID & " AND SubjectID =" & SubjectID & " AND grade='" & grade & "'"
        Dim myDI As String = ""
        Try
            myDI = ExecuteQuery_ExecuteScalar(sqlStr)
        Catch ex As Exception
            myDI = "--"
        End Try

        Return myDI
    End Function
    Protected Sub cboExamGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboExamGroup.SelectedIndexChanged
        lblGrpID.Text = FindMasterID(101, cboExamGroup.Text)
        LoadClasses(cboClass, lblGrpID.Text, lblSchoolID.Text)
        LoadExamTermsByGroups(cboTerm, lblGrpID.Text, 0)
        'LoadExamTerms(cboTerm,0, lblGrpID.Text)
        cboExamGroup.Focus()
    End Sub

    Protected Sub cboSchoolName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSchoolName.SelectedIndexChanged
        LoadMasterInfo(101, cboExamGroup, cboSchoolName.SelectedItem.Text)
        lblSchoolID.Text = getSchoolID(cboSchoolName.SelectedItem.Text)
        lblGrpID.Text = FindMasterID(101, cboExamGroup.Text)

    End Sub
End Class