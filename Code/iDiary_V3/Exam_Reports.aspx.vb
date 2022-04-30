Imports iDiary_V3.iDiary.CLS_idiary
Imports iDiary_V3.iDiary.CLS_iDiary_Exam
Imports Microsoft.Reporting.WebForms
Imports System.Web.Util
Imports System.Data.SqlClient

Public Class Exam_Reports
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then InitControls()
    End Sub

    Private Sub InitControls()
        'LoadMasterInfo(2, cboClass)
        'LoadMasterInfo(10, cboStatus)
        'LoadSubjectGroups(cboSubjectGroup, 0)
        '
        'cboSection.Items.Clear()
        'cboSubSection.Items.Clear()

        LoadMasterInfo(71, cboSchoolName, Request.Cookies("SchoolIDs").Value)
        lblSchoolID.Text = getSchoolID(cboSchoolName.SelectedItem.Text)
        LoadMasterInfo(101, cboExamGroup, cboSchoolName.SelectedItem.Text)
        lblGrpID.Text = FindMasterID(101, cboExamGroup.Text)
        rblReportOrient.SelectedIndex = 0
        'rblReportOrient.Visible = False

        lblGrpID.Text = FindMasterID(101, cboExamGroup.Text)
        ' Literal1.Text = "Marks Entry : " & Request.QueryString("grpSubject")
        cboSection.Items.Clear()
        '  LoadMasterInfo(18, cboTerm)
        'cboSubject.Items.Clear()
        LoadMasterInfo(10, cboStatus)
        cboExamGroup.Focus()
    End Sub

    Protected Sub cboSection_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSection.SelectedIndexChanged
        'LoadClassSubSection(cboClass.Text, cboSection.Text, cboSubSection)
        'LoadMasterInfo(104, cboTerm)
        'LoadMasterInfo(105, cbosubterm)
        cboSection.Focus()
    End Sub

    Protected Sub cboClass_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboClass.SelectedIndexChanged
        LoadClassSection("", cboClass.SelectedItem.Text, cboSection)
        cboSection.Items.Add("ALL")
        cboClass.Focus()
    End Sub

    Protected Sub btnGenerate_Click(sender As Object, e As EventArgs) Handles btnGenerate.Click

        If Trim(cboExamGroup.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please select Exam Group..');", True)
            lblmsg.Text = "Please select Exam Group.."
            cboExamGroup.Focus()
            Exit Sub
        End If
        If Trim(cboClass.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please select Class..');", True)
            lblmsg.Text = "Please select Class.."
            cboClass.Focus()
            Exit Sub
        End If
        If Trim(cboSection.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please select Section..');", True)
            lblmsg.Text = "Please select Section.."
            cboSection.Focus()
            Exit Sub
        End If
        If Trim(cboSubjectGroup.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please select Subject Group..');", True)
            lblmsg.Text = "Please select Subject Group.."
            cboSubjectGroup.Focus()
            Exit Sub
        End If
        If cboSubSubjectGroup.Enabled = True And Trim(cboSubjectGroup.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please select Sub Subject Group..');", True)
            lblmsg.Text = "Please select Sub Subject Group.."
            cboSubSubjectGroup.Focus()
            Exit Sub
        End If


        If Trim(cboTerm.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please select Term..');", True)
            lblmsg.Text = "Please select Term.."
            cboTerm.Focus()
            Exit Sub
        End If
        If cbosubterm.Enabled = True And Trim(cbosubterm.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please select Minor Term..');", True)
            lblmsg.Text = "Please select Minor Term.."
            cbosubterm.Focus()
            Exit Sub
        End If
        If Trim(cboStatus.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please select Status..');", True)
            lblmsg.Text = "Please select Status.."
            cboStatus.Focus()
            Exit Sub
        End If
        Dim sqlStr As String = ""
        lblmsg.Text = ""
        sqlStr = "select Count(*) from vw_examsubjectmapping where (isProcessed =0 or isProcessed is null) AND ClassName='" & cboClass.SelectedItem.Text & "' AND SecName='" & cboSection.SelectedItem.Text & "' AND ASID=" & Request.Cookies("ASID").Value
        Dim isProcessed As Integer = 0
        Try
            isProcessed = ExecuteQuery_ExecuteScalar(sqlStr)
        Catch ex As Exception

        End Try
        If isProcessed > 0 Then
            lblmsg.Text = "Marks for selected Class-Section has been changed. Please process marks to generate report."
        End If
        Dim ReportHeader As String = "", rdlcName As String = ""
        Select Case cboListType.Text
            Case "Marks Entry Slip"
                'Dim sqlStr As String = "SELECT * from vw_ExamSubjectMapping where ClassName='" & cboClass.Text & "' AND SecName='" & cboSection.Text & "' AND ASID=" & Request.Cookies("ASID").Value
                sqlStr = "SELECT * from vw_ExamSubjectMapping join vw_Student on vw_ExamSubjectMapping.ClassName= vw_Student.classname and vw_ExamSubjectMapping.secName= vw_Student.secname where vw_Student.SchoolName= '" & cboSchoolName.SelectedItem.Text & "' and vw_ExamSubjectMapping.ClassName='" & cboClass.SelectedItem.Text & "' AND vw_Student.StatusName='" & cboStatus.Text & "' AND vw_Student.ASID=" & Request.Cookies("ASID").Value & "  AND vw_ExamSubjectMapping.ASID=" & Request.Cookies("ASID").Value
                If cboSection.SelectedItem.Text <> "ALL" Then
                    sqlStr += " AND vw_ExamSubjectMapping.SecName='" & cboSection.Text & "'"
                End If
                If cboSubjectGroup.SelectedItem.Text <> "ALL" Then
                    sqlStr += " and (vw_ExamSubjectMapping.MajorGroupID='" & cboSubjectGroup.Text & "' or  vw_ExamSubjectMapping.SubGroupName='" & cboSubjectGroup.SelectedItem.Text & "')"
                End If
                If cboSubSubjectGroup.SelectedItem.Text <> "ALL" Then
                    sqlStr += " and vw_ExamSubjectMapping.SubGroupName='" & cboSubSubjectGroup.SelectedItem.Text & "'"
                End If

                ReportHeader = cboClass.SelectedItem.Text
                If cboSection.SelectedItem.Text = "ALL" Then
                Else
                    ReportHeader += "-" & cboSection.SelectedItem.Text
                End If

                ReportHeader += "; Term: " & cboTerm.SelectedItem.Text
                Try
                    ReportHeader += "-" & cbosubterm.SelectedItem.Text
                Catch ex As Exception

                End Try
                ReportHeader += "; Subject Group: " & cboSubjectGroup.SelectedItem.Text & ""
                'If cboSubSubjectGroup.SelectedItem.Text <> "ALL" Then
                '    rdlcName = "rptBlankSheetSubjects.rdlc"
                'Else
                '    rdlcName = "rptBlankSheet.rdlc"
                'End If

                If rblReportOrient.SelectedIndex = 1 Then
                    rdlcName = "rptBlankSheetSubjects.rdlc"
                Else
                    rdlcName = "rptBlankSheet.rdlc"
                End If
                PrepareReport(sqlStr, rdlcName, "Marks Entry Slip Class: " & ReportHeader)

            Case "Marks Entry Slip(Term Wise)"
                sqlStr = "Select * from vw_Student where SchoolName= '" & cboSchoolName.SelectedItem.Text & "' and ASID=" & Request.Cookies("ASID").Value & " AND ClassName='" & cboClass.SelectedItem.Text & "' AND  StatusName='" & cboStatus.SelectedItem.Text & "'"
                If UCase(cboSection.Text) <> "ALL" Then
                    sqlStr += " AND SecName='" & cboSection.SelectedItem.Text & "'"
                End If
                rdlcName = "rptBlankSheetTerms.rdlc"
                PrepareReport(sqlStr, rdlcName, "Marks Entry Slip")

            Case "Marks Enty Status"
                sqlStr = "SELECT * from vw_ExamSubjectMapping where SchoolName= '" & cboSchoolName.SelectedItem.Text & "' and ASID=" & Request.Cookies("ASID").Value & " AND ClassName='" & cboClass.SelectedItem.Text & "' "
                If cboSection.SelectedItem.Text <> "ALL" Then
                    sqlstr += " AND SecName='" & cboSection.Text & "'"
                End If
                If cboSubjectGroup.SelectedItem.Text <> "ALL" Then
                    sqlstr += " and (vw_ExamSubjectMapping.MajorGroupID='" & cboSubjectGroup.Text & "' or  vw_ExamSubjectMapping.SubGroupName='" & cboSubjectGroup.SelectedItem.Text & "')"
                End If
                If cboSubSubjectGroup.SelectedItem.Text <> "ALL" Then
                    sqlstr += " and vw_ExamSubjectMapping.SubGroupName='" & cboSubSubjectGroup.SelectedItem.Text & "'"
                End If

                rdlcName = "rptMarksEntryStatus.rdlc"
                PrepareReport(sqlstr, rdlcName, "Marks Entry Status")

            Case "Subject wise Marks Entry Slip"
                'Dim sqlStr As String = "SELECT * from vw_ExamSubjectMapping where ClassName='" & cboClass.Text & "' AND SecName='" & cboSection.Text & "' AND ASID=" & Request.Cookies("ASID").Value
                sqlStr = "SELECT * from vw_ExamSubjectMapping join vw_Student on vw_ExamSubjectMapping.ClassName= vw_Student.classname and vw_ExamSubjectMapping.secName= vw_Student.secname where vw_Student.SchoolName= '" & cboSchoolName.SelectedItem.Text & "' and vw_ExamSubjectMapping.ClassName='" & cboClass.SelectedItem.Text & "' AND vw_Student.StatusName='" & cboStatus.Text & "' AND vw_Student.ASID=" & Request.Cookies("ASID").Value & "  AND vw_ExamSubjectMapping.ASID=" & Request.Cookies("ASID").Value
                If cboSection.SelectedItem.Text <> "ALL" Then
                    sqlStr += " AND vw_ExamSubjectMapping.SecName='" & cboSection.Text & "'"
                End If
                If cboSubjectGroup.SelectedItem.Text <> "ALL" Then
                    sqlStr += " and (vw_ExamSubjectMapping.MajorGroupID='" & cboSubjectGroup.Text & "' or  vw_ExamSubjectMapping.SubGroupName='" & cboSubjectGroup.SelectedItem.Text & "')"
                End If
                If cboSubSubjectGroup.SelectedItem.Text <> "ALL" Then
                    sqlStr += " and vw_ExamSubjectMapping.SubGroupName='" & cboSubSubjectGroup.SelectedItem.Text & "'"
                End If

                ReportHeader = cboClass.SelectedItem.Text
                If cboSection.SelectedItem.Text = "ALL" Then
                Else
                    ReportHeader += "-" & cboSection.SelectedItem.Text
                End If

                ReportHeader += " Term: " & cboTerm.SelectedItem.Text
                Try
                    ReportHeader += " " & cbosubterm.SelectedItem.Text
                Catch ex As Exception

                End Try
                rdlcName = "rptBlankSheet.rdlc"
                PrepareReport(sqlStr, rdlcName, "Marks Entry Slip Class: " & ReportHeader)
            Case "Term Wise Subject List"
                sqlStr = "Select * from vw_Exam_Report WHere SchoolName= '" & cboSchoolName.SelectedItem.Text & "' and StatusName='" & cboStatus.Text & "' AND ClassName='" & cboClass.SelectedItem.Text & "' AND SecName='" & cboSection.SelectedItem.Text & "' AND  ASID=" & Request.Cookies("ASID").Value & " AND SubjectID='" & cboSubjects.SelectedItem.Value & "' AND MajorTermID='" & cboTerm.SelectedItem.Value & "'"
                rdlcName = "rptTermWiseSubjectList.rdlc"
                ReportHeader = "MARKS LIST FOR THE YEAR : " & Request.Cookies("ASName").Value
                PrepareReport(sqlStr, rdlcName, ReportHeader)

            Case "Marks Enty Status"
                sqlStr = "SELECT * from vw_ExamSubjectMapping where SchoolName= '" & cboSchoolName.SelectedItem.Text & "' and ASID=" & Request.Cookies("ASID").Value & " AND ClassName='" & cboClass.SelectedItem.Text & "' "
                If cboSection.Text = "ALL" Then
                Else
                    sqlStr += " AND SecName='" & cboSection.Text & "'"
                End If
                If cboSubjectGroup.SelectedItem.Text <> "ALL" Then
                    sqlStr += " and (vw_ExamSubjectMapping.MajorGroupID='" & cboSubjectGroup.Text & "' or  vw_ExamSubjectMapping.SubGroupName='" & cboSubjectGroup.SelectedItem.Text & "')"
                End If
                If cboSubSubjectGroup.SelectedItem.Text <> "ALL" Then
                    sqlStr += " and vw_ExamSubjectMapping.SubGroupName='" & cboSubSubjectGroup.SelectedItem.Text & "'"
                End If
                ReportHeader = "Marks Entry Status report for Term : " & cboTerm.SelectedItem.Text & "-" & cbosubterm.SelectedItem.Text
                rdlcName = "rptMarksEntryStatus.rdlc"
                PrepareReport(sqlStr, rdlcName, ReportHeader)

            Case "Consolidate Sheet"
                Dim rptParam As String = ""
                sqlStr = "Select max(termWeightage) from ExamTermConfig WHere SchoolName= '" & cboSchoolName.SelectedItem.Text & "' and ExamGroupID=" & lblGrpID.Text & " AND ExamMajorTermID=" & cboTerm.SelectedItem.Value & " AND SubGrpID=" & cboSubjectGroup.SelectedItem.Value
                Try
                    rptParam = cboTerm.SelectedItem.Text & "/" & ExecuteQuery_ExecuteScalar(sqlStr)
                Catch ex As Exception

                End Try
                sqlStr = "Select [" & cboTerm.SelectedItem.Text & "] as FA1,SID,regno,ClassRollNo,SName,ClassName,SecName,SubjectName,SubjectID,Priority from vw_ExamMarks WHere StatusName='" & cboStatus.Text & "' AND ClassName='" & cboClass.SelectedItem.Text & "' AND SecName='" & cboSection.SelectedItem.Text & "' AND  ASID=" & Request.Cookies("ASID").Value
                If cboSubjectGroup.SelectedItem.Text = "ALL" Then
                Else
                    sqlStr += " AND SubGrpID=" & cboSubjectGroup.SelectedItem.Value
                End If
                If cboTerm.SelectedItem.Text.Contains("FA") = True Then
                    rdlcName = "rptTabulationSheetFA.rdlc"
                Else
                    rdlcName = "rptTabulationSheetSA.rdlc"
                End If

                ReportHeader = "Consolidate Sheet : " & Request.Cookies("ASName").Value
                PrepareReport(sqlStr, rdlcName, ReportHeader, rptParam)

            Case "Summative Assessment"
                If cboTerm.SelectedItem.Text.Contains("SA") = False Then
                    lblmsg.Text = "report applicable only for SAs."
                    Exit Sub
                End If
                Dim rptParam As String = ""
                If cboTerm.SelectedItem.Text = "SA1" Then
                    rptParam = "I"
                    sqlStr = "Select FA1,FA2,SumFA,SA1,Term1,"
                Else
                    rptParam = "II"
                    sqlStr = "Select FA3 as FA1,FA4 as FA2,SA2 as SA1,Term2 as Term1,"
                End If
                sqlStr += "SID,regno,ClassRollNo,SName,ClassName,SecName,SubjectName,SubjectID from vw_ExamMarks WHere SchoolName= '" & cboSchoolName.SelectedItem.Text & "' and StatusName='" & cboStatus.Text & "' AND ClassName='" & cboClass.SelectedItem.Text & "' AND SecName='" & cboSection.SelectedItem.Text & "' AND  ASID=" & Request.Cookies("ASID").Value
                If cboSubjectGroup.SelectedItem.Text = "ALL" Then
                Else
                    sqlStr += " AND SubGrpID=" & cboSubjectGroup.SelectedItem.Value
                End If
                rdlcName = "rptSummativeAssessment.rdlc"
                ReportHeader = "SUMMATIVE ASSESSMENT -  " & rptParam & "  SESSION : " & Request.Cookies("ASName").Value
                PrepareReport(sqlStr, rdlcName, ReportHeader, rptParam)

            Case "Final Summative Assessment"
                If cboTerm.SelectedItem.Text.Contains("SA") = False Then
                    lblmsg.Text = "report applicable only for SAs."
                    Exit Sub
                End If
                Dim rptParam As String = "Final Term"
               
                sqlStr = "Select *  from vw_ExamMarks WHere SchoolName= '" & cboSchoolName.SelectedItem.Text & "' and StatusName='" & cboStatus.Text & "' AND ClassName='" & cboClass.SelectedItem.Text & "' AND SecName='" & cboSection.SelectedItem.Text & "' AND  ASID=" & Request.Cookies("ASID").Value
                If cboSubjectGroup.SelectedItem.Text = "ALL" Then
                Else
                    sqlStr += " AND SubGrpID=" & cboSubjectGroup.SelectedItem.Value
                End If
                rdlcName = "rptSummativeAssessmentFinal.rdlc"
                ReportHeader = "SUMMATIVE ASSESSMENT -  " & rptParam & "  SESSION : " & Request.Cookies("ASName").Value
                PrepareReport(sqlStr, rdlcName, ReportHeader, rptParam)

            Case "Summary Sheet (Scored Marks)", "Summary Sheet (Weighted Marks)"
                Dim MajorTermID As String = ""
                For i = 0 To cboTerm.SelectedIndex
                    MajorTermID += FindMasterID(102, cboTerm.Items(i).Text) & ","
                Next
                If MajorTermID <> "" Then
                    MajorTermID = MajorTermID.Substring(0, MajorTermID.Length - 1)
                End If
                sqlStr = "SELECT * from vw_Exam_Report where SchoolName= '" & cboSchoolName.SelectedItem.Text & "' and ClassName='" & cboClass.SelectedItem.Text & "' AND ASID=" & Request.Cookies("ASID").Value & " AND SubjectMappingASID=" & Request.Cookies("ASID").Value
                If cboSection.SelectedItem.Text <> "ALL" Then
                    sqlStr += " AND SecName='" & cboSection.Text & "'"
                End If
                If cbosubterm.Items.Count = 0 And cbosubterm.Enabled = False Then
                Else
                    If cbosubterm.SelectedItem.Text <> "ALL" Then
                        sqlStr += " AND MinorTermName='" & cbosubterm.SelectedItem.Text & "'"
                    End If
                End If

                If cboSubjectGroup.SelectedItem.Text <> "ALL" Then
                    sqlStr += " and (SubGroupName='" & cboSubjectGroup.SelectedItem.Text & "' or MajorGroupID='" & cboSubjectGroup.Text & "')"
                End If
                If cboSubSubjectGroup.SelectedItem.Text <> "ALL" Then
                    sqlStr += " and SubGroupName='" & cboSubSubjectGroup.SelectedItem.Text & "'"
                End If
                sqlStr += " AND StatusName='" & cboStatus.Text & "'  AND MajorTermID in (" & MajorTermID & ") order by DisplayOrder"
                ReportHeader = cboClass.SelectedItem.Text
                If cboSection.SelectedItem.Text = "ALL" Then
                Else
                    ReportHeader += "-" & cboSection.SelectedItem.Text
                End If
                If cboListType.Text = "Summary Sheet (Weighted Marks)" Then
                    rdlcName = "rptSummarySheet_Wt.rdlc"
                Else
                    rdlcName = "rptSummarySheet.rdlc"
                End If
                PrepareReport(sqlStr, rdlcName, "Summary Sheet Class: " & ReportHeader)

            Case "Subject Mapping List"
                sqlStr = "SELECT * from vw_ExamSubjectMapping where SchoolName= '" & cboSchoolName.SelectedItem.Text & "' and ClassName='" & cboClass.SelectedItem.Text & "'  AND ASID=" & Request.Cookies("ASID").Value
                If cboSection.SelectedItem.Text <> "ALL" Then
                    sqlStr += " AND vw_ExamSubjectMapping.SecName='" & cboSection.Text & "'"
                End If
                If cboSubjectGroup.SelectedItem.Text <> "ALL" Then
                    sqlStr += " and (vw_ExamSubjectMapping.MajorGroupID='" & cboSubjectGroup.Text & "' or  vw_ExamSubjectMapping.SubGroupName='" & cboSubjectGroup.SelectedItem.Text & "')"
                End If
                If cboSubSubjectGroup.SelectedItem.Text <> "ALL" Then
                    sqlStr += " and vw_ExamSubjectMapping.SubGroupName='" & cboSubSubjectGroup.SelectedItem.Text & "'"
                End If

                ReportHeader = cboClass.SelectedItem.Text
                If cboSection.SelectedItem.Text = "ALL" Then
                Else
                    ReportHeader += "-" & cboSection.SelectedItem.Text
                End If

                ReportHeader += " Term: " & cboTerm.SelectedItem.Text
                Try
                    ReportHeader += " " & cbosubterm.SelectedItem.Text
                Catch ex As Exception

                End Try
                rdlcName = "rptSubjectMappingList.rdlc"
                PrepareReport(sqlStr, rdlcName, "Subject Mapping List: " & ReportHeader)

            Case "Exam Term Mapping List"
                Dim MajorGroupId As Integer = FindMasterID(101, cboExamGroup.Text)
                sqlStr = "SELECT * from vw_ExamTerms where SchoolName= '" & cboSchoolName.SelectedItem.Text & "' and examGroupID='" & MajorGroupId & "'"

                If cboSubSubjectGroup.SelectedItem.Text <> "ALL" Then
                    sqlStr += " and vw_ExamTerms.SubGroupName='" & cboSubSubjectGroup.SelectedItem.Text & "'"
                End If


                Try
                    ReportHeader += cboExamGroup.SelectedItem.Text
                Catch ex As Exception

                End Try
                rdlcName = "rptExamTermMappingList.rdlc"
                PrepareReport(sqlStr, rdlcName, ReportHeader)

            Case "User Permission Mapping List"
                sqlStr = "Select * from vw_UserSubjectPermission Where ASID=" & Request.Cookies("ASID").Value
                PrepareReport(sqlStr, "rptUserPermissions.rdlc", "User Permission Sheet ")

            Case "PromotionList"
                lblmsg.Text = ""
                sqlStr = "SELECT * from vw_Exam_Report where SchoolName= '" & cboSchoolName.SelectedItem.Text & "' and ClassName='" & cboClass.SelectedItem.Text & "' AND SecName='" & cboSection.Text & "'"
                PrepareReport(sqlStr, "rptPromotionList.rdlc", "Summary Sheet :" & cboClass.SelectedItem.Text & "-" & cboSection.SelectedItem.Text)

                'Case "Report Card"

                '    sqlStr = "SELECT * from vw_ExamMarks where ClassName='" & cboClass.SelectedItem.Text & "' AND SecName='" & cboSection.SelectedItem.Text & "' AND StatusName='" & cboStatus.Text & "'"
                '    Try
                '        rdlcName = ExecuteQuery_ExecuteScalar("Select top(1) rdlcName from ExamReportName Where ExamGroupID=" & lblGrpID.Text)
                '        PrepareReport(sqlStr, rdlcName, "Summary Sheet :" & cboClass.SelectedItem.Text & "-" & cboSection.SelectedItem.Text)
                '    Catch ex As Exception

                '    End Try


        End Select

    End Sub


    Private Sub PrepareReport(ByVal sqlString As String, ByVal reportName As String, MyHeader As String, Optional rptParam As String = "")

        Dim ds As New DataSet
        ds = ExecuteQuery_DataSet(sqlString, "tbl")
        Select Case cboListType.Text
            Case "Term Wise Subject List"
                ds = ProcessDSforTerm(ds, cboTerm.SelectedItem.Value, cboSubjects.SelectedItem.Value)
            Case "Marks Enty Status"
                ds = DatasetEntryStatus(ds, cboTerm.SelectedItem.Value, cbosubterm.SelectedItem.Value)
        End Select

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
        Dim SchoolName As String = cboSchoolName.SelectedItem.Value
        Dim SchoolID As Int16 = FindMasterID(71, SchoolName)
        Dim SchoolAddress As String = FindSchoolDetails1(1, SchoolID)
        Dim ASName As String = Request.Cookies("ASName").Value
        Dim params(7) As Microsoft.Reporting.WebForms.ReportParameter
        params(0) = New Microsoft.Reporting.WebForms.ReportParameter("SchoolName", SchoolName, True)
        params(1) = New Microsoft.Reporting.WebForms.ReportParameter("SchoolAddress", SchoolAddress, True)
        params(2) = New Microsoft.Reporting.WebForms.ReportParameter("ASName", ASName, True)
        params(3) = New Microsoft.Reporting.WebForms.ReportParameter("MyHeader", MyHeader, True)
        params(4) = New Microsoft.Reporting.WebForms.ReportParameter("ImagePath", Server.MapPath("~/images/SchoolLogo.png"), Visible)
        params(5) = New Microsoft.Reporting.WebForms.ReportParameter("ImageSign", Server.MapPath("~/images/signature.png"), Visible)
        params(6) = New Microsoft.Reporting.WebForms.ReportParameter("UserName", Request.Cookies("UID").Value, Visible)
        params(7) = New Microsoft.Reporting.WebForms.ReportParameter("rptParam", rptParam, Visible)
        Me.ReportViewer1.LocalReport.SetParameters(params)

        ReportViewer1.Visible = True
        ReportViewer1.LocalReport.Refresh()


    End Sub

    Private Function DatasetEntryStatus(ByVal ds As DataSet, MajorTermID As Integer, MinorTermID As Integer) As DataSet
        Dim nDs As DataSet = ds
        Dim Entry As DataColumn
        Dim ASID As Integer = Request.Cookies("ASID").Value
        Entry = New DataColumn("Entry", GetType(String))
        nDs.Tables(0).Columns.Add(Entry)
        For Each Row As DataRow In nDs.Tables(0).Rows
            Dim sqlstr As String = "Select Count(*) from vw_Exam_Report Where ASID=" & ASID & " AND ClassName='" & Row("ClassName") & "' And SecName='" & Row("SecName") & "' AND SubjectID=" & Row("SubjectID") & " AND MajorTermID='" & MajorTermID & "' AND MinorTermID='" & MinorTermID & "' and SchoolName='" & cboSchoolName.SelectedItem.Text & "'"
            Dim rv As Integer = ExecuteQuery_ExecuteScalar(sqlstr)
            If rv > 0 Then
                Row("Entry") = "Yes"
            Else
                Row("Entry") = "No"
            End If
        Next
        Return ds
    End Function

    Private Function ProcessDSforTerm(ByVal ds As DataSet, majorTermID As Integer, Optional SubjectID As Integer = 0) As DataSet
        ds.Tables(0).Columns.Add("OverAll", GetType(String))
        Dim preSID As Integer = 0, SID As Integer = 0
        Dim OverAll As String = ""
        For Each Row As DataRow In ds.Tables(0).Rows
            SID = Row("SID")
            ' SubjectID = Row("SubjectID")
            If preSID <> SID Then
                OverAll = getOverAllScore(SID, majorTermID, SubjectID)
            End If
            Row("OverAll") = OverAll
            preSID = SID
        Next
        Return ds
    End Function
    Private Function getOverAllScore(SID As Integer, MajorTermID As Integer, SubJectID As Integer) As String
        Dim rv As String = ""
        Dim sqlstr As String = ""
        'If SubJectID = 0 Then
        '    sqlstr = "SELECT SUM(CAST(obtM as float)) as obtM,SUM(CAST(maxM as float)) as maxM from vw_exam_report WHERE ISNUMERIC(obtM)=1 and PartOfCalculation=1 AND SID=" & SID & " AND MajorTermID=" & MajorTermID & " AND SubjectID=" & SubJectID
        'End If
        'for numeric entries
        sqlstr = "SELECT SUM(CAST(obtM as float)) as obtM,SUM(CAST(maxM as float)) as maxM from vw_exam_report WHERE ISNUMERIC(obtM)=1 and PartOfCalculation=1 AND SID=" & SID & " AND MajorTermID=" & MajorTermID & " AND SubjectID=" & SubJectID
        Dim obtM As Double = 0, maxM As Double = 0
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)
        While myReader.Read
            Try
                obtM = myReader("obtM")
            Catch ex As Exception

            End Try
            Try
                maxM = myReader("maxM")
            Catch ex As Exception

            End Try

        End While
        myReader.Close()
        'for Non-numeric entries
        sqlstr = "SELECT  obtM, maxM from vw_exam_report WHERE ISNUMERIC(obtM)=0 and PartOfCalculation=1 AND SID=" & SID & " AND MajorTermID=" & MajorTermID & " AND SubjectID=" & SubJectID

        myReader = ExecuteQuery_ExecuteReader(sqlstr)
        While myReader.Read
            'skip obtM
            'Try
            '    obtM += myReader("obtM")
            'Catch ex As Exception

            'End Try
            Try
                maxM += myReader("maxM")
            Catch ex As Exception
                maxM += 0
            End Try

        End While
        myReader.Close()
        rv = obtM & "/" & maxM & "/" & getGrade(obtM, maxM)

        Return rv
    End Function
    Protected Sub cboStudentAttendance_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboStudentAttendance.SelectedIndexChanged
        LoadTotalAttendance(cboClass.Text, cboSection.Text, cboStudentAttendance)
        cboStudentAttendance.Focus()
    End Sub

    Public Shared Function LoadTotalAttendance(ByVal ClassName As String, ByVal SectionName As String, ByRef mycbo As DropDownList) As Integer
        Dim sqlstr As String = "Select Count(RegNo) , RegNo From vw_Attendance where ClassName='" & ClassName & "' AND SecName='" & SectionName & "' AND IsPresentM='1'  group by RegNo Order By RegNo"

        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)
        mycbo.Items.Clear()
        mycbo.Items.Add("")
        While myReader.Read
            mycbo.Items.Add(myReader(0))
        End While
        myReader.Close()

        Return 0

    End Function

    Protected Sub cbosubterm_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbosubterm.SelectedIndexChanged

    End Sub

    Protected Sub cboExamGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboExamGroup.SelectedIndexChanged
        lblGrpID.Text = FindMasterID(101, cboExamGroup.Text)
        LoadSubjectGroups(cboSubjectGroup, 0, lblGrpID.Text)
        cboSubjectGroup.Items.Add("ALL")

        LoadClasses(cboClass, lblGrpID.Text, lblSchoolID.Text)
        cboExamGroup.Focus()
        cboSubjectGroup.Enabled = True
        cboSubSubjectGroup.Enabled = True
    End Sub

    Protected Sub cboSubjectGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSubjectGroup.SelectedIndexChanged
        cboSubSubjectGroup.Items.Clear()
        If cboSubjectGroup.SelectedItem.Text = "ALL" Then
            cboSubSubjectGroup.Items.Add("ALL")
        Else
            Try
                LoadMinorGroups(cboSubSubjectGroup, cboSubjectGroup.SelectedItem.Value)
                cboSubSubjectGroup.Items.Add("ALL")
            Catch ex As Exception

            End Try
        End If

        Dim subGrpID As Integer = 0
        Try
            subGrpID = cboSubjectGroup.SelectedItem.Value
        Catch ex As Exception

        End Try

        Try
            cboSubSubjectGroup.SelectedIndex = 1

        Catch ex As Exception

        End Try
        If cboSubSubjectGroup.Items.Count > 2 Then
            cboSubSubjectGroup.Enabled = True
            LoadSubjectClassWise(cboSubjects, cboClass.SelectedItem.Text, cboSection.SelectedItem.Text, Request.Cookies("ASID").Value, cboSubSubjectGroup.SelectedItem.Value)
        Else
            cboSubSubjectGroup.Enabled = False
            LoadSubjectClassWise(cboSubjects, cboClass.SelectedItem.Text, cboSection.SelectedItem.Text, Request.Cookies("ASID").Value, subGrpID)
        End If

        'If cboSubSubjectGroup.Text = "" Then
        '    Try
        '        subGrpID = cboSubjectGroup.SelectedItem.Value
        '    Catch ex As Exception

        '    End Try
        'Else
        '    Try
        '        subGrpID = cboSubSubjectGroup.SelectedItem.Value
        '    Catch ex As Exception

        '    End Try
        'End If
        LoadSubjectGroupExamTermsMajor(cboTerm, Val(subGrpID), lblGrpID.Text)

        If subGrpID = 0 Then
            cbosubterm.Items.Clear()
            cbosubterm.Enabled = False
        Else
            Try
                LoadSubjectGroupExamTermsMinor(cbosubterm, cboTerm.SelectedItem.Value, lblGrpID.Text, cboSubjectGroup.SelectedItem.Value)
                cbosubterm.Items.Add("ALL")
            Catch ex As Exception

            End Try
            cbosubterm.Enabled = True
        End If
        If cbosubterm.Items.Count > 1 Then
            cbosubterm.Enabled = True
        Else
            cbosubterm.Enabled = False
        End If

        sdsGrade.SelectCommand = "SELECT [UpperValue], [LowerValue], [Grade] FROM [vw_ExamGradeMapping] where ExamgroupID=" & lblGrpID.Text & " and SubGrpID=" & subGrpID & " order by DisplayOrder  "
        gvGrade.DataBind()

        cboSubjectGroup.Focus()
    End Sub

    Protected Sub cboSubSubjectGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSubSubjectGroup.SelectedIndexChanged
        'LoadSubjectGroupExamTermsMajor(cboTerm, cboSubSubjectGroup.SelectedItem.Value, lblGrpID.Text)
        'LoadSubjectGroupExamTermsMinor(cbosubterm, cboSubjectGroup.SelectedItem.Value, lblGrpID.Text, cboSubjectGroup.SelectedItem.Value)
        Try
            LoadSubjectClassWise(cboSubjects, cboClass.SelectedItem.Text, cboSection.SelectedItem.Text, Request.Cookies("ASID").Value, cboSubSubjectGroup.SelectedItem.Value)
        Catch ex As Exception

        End Try
        cboSubSubjectGroup.Focus()
    End Sub

    Protected Sub cboTerm_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboTerm.SelectedIndexChanged
        LoadSubjectGroupExamTermsMinor(cbosubterm, cboTerm.SelectedItem.Value, lblGrpID.Text, Val(cboSubjectGroup.SelectedItem.Value))
        cbosubterm.Items.Add("ALL")
        If cbosubterm.Items.Count > 1 Then
            cbosubterm.Enabled = True
        Else
            cbosubterm.Enabled = False
        End If
        cboTerm.Focus()
    End Sub
    Private Function getGrade(obtM As String, maxM As String) As String
        If maxM = 0 Then Return obtM
        If Not IsNumeric(obtM) Then
            Return obtM
        End If
        Dim marks As Double = 0
        Try
            marks = Math.Round(100 * CDbl(obtM) / CDbl(maxM), 2, MidpointRounding.AwayFromZero)
        Catch ex As Exception

        End Try
        Dim rv As String = "", UValue As Double = 0, LValue As Double = 0
        For i = 0 To gvGrade.Rows.Count - 1
            UValue = Val(gvGrade.Rows(i).Cells(0).Text)
            LValue = Val(gvGrade.Rows(i).Cells(1).Text)
            If marks >= LValue And marks <= UValue Then
                rv = gvGrade.Rows(i).Cells(2).Text
                Exit For
            End If
        Next

        Return rv
    End Function

    Protected Sub cboSchoolName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSchoolName.SelectedIndexChanged
        LoadMasterInfo(101, cboExamGroup, cboSchoolName.SelectedItem.Text)
        lblSchoolID.Text = getSchoolID(cboSchoolName.SelectedItem.Text)
        lblGrpID.Text = FindMasterID(101, cboExamGroup.Text)

    End Sub

   

   

End Class