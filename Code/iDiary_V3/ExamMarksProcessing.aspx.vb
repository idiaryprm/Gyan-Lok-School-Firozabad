Imports iDiary_V3.iDiary.CLS_idiary
Imports iDiary_V3.iDiary.CLS_iDiary_Exam
Imports Microsoft.Reporting.WebForms
Imports System.Web.Util
Imports System.Data.SqlClient

Public Class ExamMarksProcessing
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then InitControls()
    End Sub

    Private Sub InitControls()
        LoadMasterInfo(101, cboExamGroup)
        lblGrpID.Text = FindMasterID(101, cboExamGroup.Text)

        cboExamGroup.Focus()
    End Sub

    Protected Sub btnProcess_Click(sender As Object, e As EventArgs) Handles btnProcess.Click

        If Trim(cboExamGroup.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please select Exam Group..');", True)
            lblmsg.Text = "Please select Exam Group.."
            cboExamGroup.Focus()
            Exit Sub
        End If

        Dim sqlCSSID As String = "Update examsubjectmapping Set isProcessed=1 Where CSSID IN ("
        For i = 0 To cblClasses.Items.Count - 1
            If cblClasses.Items(i).Selected = True Then
                processMarks(lblGrpID.Text, cblClasses.Items(i).Value)
                processTotalMarks(lblGrpID.Text, cblClasses.Items(i).Value)
                sqlCSSID += cblClasses.Items(i).Value & ","
            End If
        Next
        sqlCSSID = sqlCSSID.Substring(0, sqlCSSID.Length - 1)
        sqlCSSID += ")"
        ExecuteQuery_Update(sqlCSSID)
        lblmsg.Text = "Marks for Selected Classes Processed/Freezed successfully."

    End Sub

    Private Sub processMarks(ExamGroupID As Integer, CSSID As Integer)
        Dim sqlStr As String = "", sqlUpdate As String = "", lstMajorTerms As New List(Of String)
        sqlStr = "select Distinct ExamTermMajor,DisplayOrderMaj from vw_ExamTerms where ExamGroupID=" & ExamGroupID & " order by DisplayOrderMaj"
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            lstMajorTerms.Add(myReader("ExamTermMajor"))
        End While
        myReader.Close()
        Dim SubGrpID As Integer = 0
        sqlStr = "Select SID,SubjectID,MajorTermName,obtM,maxM,grade,PartofCalculation,IsAttendanceType,SubGrpID,ExamGroupID from vw_exam_report Where CSSID=" & CSSID & "  order by SID,SubjectID,MajorTermName "
        Dim ds As DataSet = ExecuteQuery_DataSet(sqlStr, "t1")
        Dim SID As Integer = 0, nextSID As Integer = 0, SubjectID As Integer = 0, nextSubjectID As Integer = 0
        Dim cnt As Integer = 0, TermName As String = "", nextTermName As String = ""
        Dim obtM As Double = 0, maxM As Double = 0
        Dim sqlString As String = ""
        Dim partOfCalculation As Integer = 0, IsAttendanceType As Integer = 0

        'grade mapping
        sqlString = "Select top 1 SubgrpId from examsubjectgroupmaster Where ExamGroupID like '%:" & lblGrpID.Text & ":%' AND PartOfCalculation=1"
        SubGrpID = ExecuteQuery_ExecuteScalar(sqlString)
        sdsGrade.SelectCommand = "SELECT [UpperValue], [LowerValue], [Grade],[GradePoint] FROM [vw_ExamGradeMapping] Where SubGrpID=" & SubGrpID & " AND EGroupID=" & ExamGroupID & " order by DisplayOrder"
        gvGrade.DataBind()
        ''
        sqlString = ""
        For i = 0 To ds.Tables(0).Rows.Count - 1
            Dim row As DataRow = ds.Tables(0).Rows(i)
            cnt = 0
            SID = row("SID")
            SubjectID = row("SubjectID")
            TermName = row("MajorTermName")
            partOfCalculation = row("PartofCalculation")
            IsAttendanceType = row("IsAttendanceType")
            '  If nextSubjectID = 0 Or (SubjectID <> nextSubjectID) Then
            sqlStr = "Select Count(SID) from  ExamMarks Where SID=" & SID & " And SubjectID=" & SubjectID
            Try
                cnt = ExecuteQuery_ExecuteScalar(sqlStr)
            Catch ex As Exception
                cnt = 0
            End Try
            If cnt = 0 Then
                sqlStr = "Insert into ExamMarks (SID,SubjectID) Values(" & SID & "," & SubjectID & ")"
                ExecuteQuery_Update(sqlStr)
            End If

            'If nextSubjectID = 0 Or (SubjectID <> nextSubjectID) Then

            'End If
            Try
                nextSID = ds.Tables(0).Rows(i + 1)("SID")
                nextSubjectID = ds.Tables(0).Rows(i + 1)("SubjectID")
                nextTermName = ds.Tables(0).Rows(i + 1)("MajorTermName")
            Catch ex As Exception
                nextSID = 0
                nextSubjectID = 0
                nextTermName = ""
            End Try
            '    End If

            'If TermName <> nextTermName Then
            '    obtM = 0
            '    maxM = 0
            'Else

            'End If


            obtM += Val(row("obtM"))
            maxM += Val(row("maxM"))

            If nextTermName = "" Or (TermName <> nextTermName) Or (SubjectID <> nextSubjectID) Then
                If partOfCalculation = 1 Then
                    If SubGrpID <> row("SubGrpID") Then
                        'subGrpID = row("SubGrpID")
                        'sdsGrade.SelectCommand = "SELECT [UpperValue], [LowerValue], [Grade],[GradePoint] FROM [vw_ExamGradeMapping] Where SubGrpID=" & subGrpID & " AND EGroupID=" & row("ExamGroupID") & " order by DisplayOrder"
                        'gvGrade.DataBind()
                    End If
                    sqlString += TermName & "='" & obtM & "/" & maxM & "/" & getGrade(obtM, maxM) & "',"
                Else
                    If IsAttendanceType = 1 Then
                        sqlString += TermName & "='" & obtM & "/" & maxM & "/" & row("grade") & "',"
                    Else
                        sqlString += TermName & "='" & row("obtM") & "',"
                    End If
                End If


                obtM = 0
                maxM = 0
            End If

            If nextSubjectID = 0 Or (SubjectID <> nextSubjectID) Then
                sqlString = sqlString.Substring(0, sqlString.Length - 1)
                sqlUpdate = "Update ExamMarks Set " & sqlString & " Where SID=" & SID & " And SubjectID=" & SubjectID
                ExecuteQuery_Update(sqlUpdate)
                sqlString = ""
            End If

        Next

    End Sub

    Private Sub processTotalMarks(ExamGroupID As Integer, CSSID As Integer)
        Dim subGrpID As Integer = 0
        Dim sqlStr As String = "", sqlUpdate As String = "", ddlMajorTerms As New DropDownList
        sqlStr = "select Distinct ExamTermMajor,termWeightage ,DisplayOrderMaj  from vw_ExamTerms where partofCalculation=1 AND ExamGroupID=" & ExamGroupID & " order by DisplayOrderMaj"
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            ddlMajorTerms.Items.Add(New ListItem(myReader(0), myReader(1)))
        End While
        myReader.Close()

        'get list of Aggregation
        Dim lstAggregations As New List(Of ListItem)
        sqlStr = "select AggregationName,AggregationRule from ExamTermAggregation order by DisplayOrder"
        myReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            lstAggregations.Add(New ListItem(myReader(0), myReader(1)))
        End While
        myReader.Close()

        sqlStr = "select SID,subjectID,"
        For i = 0 To ddlMajorTerms.Items.Count - 1
            sqlStr += ddlMajorTerms.Items(i).Text & ","
        Next
        sqlStr += "ExamGroupID,SubGrpID from vw_ExamMarks  Where partOfCalculation=1 and CSSID=" & CSSID & "  order by SID,SubjectID"
        Dim ds As DataSet = ExecuteQuery_DataSet(sqlStr, "t1")
        Dim SID As Integer = 0, SubjectID As Integer = 0
        Dim cnt As Integer = 0
        Dim obtM As Double = 0, maxM As Double = 0, myMarks As String = "", termWt As Integer = 0, obtTotal As Double = 0, maxTotal As Double = 0
        Dim sqlString As String = ""
        Dim partOfCalculation As Integer = 0
        For i = 0 To ds.Tables(0).Rows.Count - 1
            obtM = 0
            maxM = 0
            Dim obtTerm As Double = 0, maxTerm As Double = 0
            Dim row As DataRow = ds.Tables(0).Rows(i)
            If subGrpID <> row("SubGrpID") Then
                'subGrpID = row("SubGrpID")
                'sdsGrade.SelectCommand = "SELECT [UpperValue], [LowerValue], [Grade],[GradePoint] FROM [vw_ExamGradeMapping] Where SubGrpID=" & subGrpID & " AND EGroupID=" & row("ExamGroupID") & " order by DisplayOrder"
                'gvGrade.DataBind()
            End If
            SID = row("SID")
            SubjectID = row("SubjectID")
            sqlStr = "Update ExamMarks Set "
            For ct = 0 To lstAggregations.Count - 1
                obtTotal = 0
                maxTotal = 0
                Dim termAr() As String = lstAggregations.Item(ct).Value.Split("+")
                For t = 0 To termAr.Length - 1
                    Try
                        myMarks = row(Trim(termAr(t)))
                        termWt = ddlMajorTerms.Items.FindByText(termAr(t)).Value
                        Try
                            obtM = myMarks.Split("/")(0)
                            maxM = myMarks.Split("/")(1)
                        Catch ex As Exception
                            obtM = 0
                            maxM = 0
                        End Try
                        If maxM > 0 Then

                            obtTotal += Math.Round(termWt * obtM / maxM, 2, MidpointRounding.AwayFromZero)
                            maxTotal += termWt
                        End If
                    Catch ex As Exception

                    End Try

                Next
                sqlStr += lstAggregations.Item(ct).Text & "='" & obtTotal & "/" & maxTotal & "/" & getGrade(obtTotal, maxTotal) & "/" & getGradePoint(obtTotal, maxTotal) & "',"
            Next
            sqlStr = sqlStr.Substring(0, sqlStr.Length - 1)
            sqlStr += " Where SID=" & SID & " AND SubjectID=" & SubjectID
            ExecuteQuery_Update(sqlStr)

        Next

    End Sub
    Private Function getGradePoint(obtM As String, maxM As String) As String
        If maxM = 0 Then Return obtM
        If Not IsNumeric(obtM) Then
            Return obtM
        End If
        Dim marks As Double = 0
        Try
            marks = Math.Round(100 * (CDbl(obtM) / CDbl(maxM)), 1, MidpointRounding.AwayFromZero)
        Catch ex As Exception

        End Try
        Dim rv As String = "", UValue As Double = 0, LValue As Double = 0
        For i = 0 To gvGrade.Rows.Count - 1
            UValue = Val(gvGrade.Rows(i).Cells(0).Text)
            LValue = Val(gvGrade.Rows(i).Cells(1).Text)
            If marks >= LValue And marks <= UValue Then
                rv = gvGrade.Rows(i).Cells(3).Text
                Exit For
            End If
        Next

        Return rv
    End Function
    Private Function getGrade(obtM As String, maxM As String) As String
        If maxM = 0 Then Return obtM
        If Not IsNumeric(obtM) Then
            Return obtM
        End If
        Dim marks As Double = 0
        Try
            marks = Math.Round(100 * (CDbl(obtM) / CDbl(maxM)), 2, MidpointRounding.AwayFromZero)
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
    Protected Sub cboExamGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboExamGroup.SelectedIndexChanged
        lblGrpID.Text = FindMasterID(101, cboExamGroup.Text)
        cboExamGroup.Focus()

        sdsCSSID.SelectCommand = "SELECT CASE WHEN SubSecName is NULL THEN Concat(ClassName,'-',SecName) ELSE Concat(ClassName,'-',SecName,'-',SubSecName ) END AS CSSNAME,CSSID FROM [vw_ClassStudent] Where ExamGroupID=" & lblGrpID.Text & " AND CSSID in (select distinct CSSID from examsubjectmapping where (isProcessed =0 or isProcessed is null) AND ASID=" & Request.Cookies("ASID").Value & ") Order By ClassDisplayOrder,DisplayOrder"
        cblClasses.DataBind()
    End Sub

    Protected Sub cbAll_CheckedChanged(sender As Object, e As EventArgs) Handles cbAll.CheckedChanged
        CheckAll(cbAll.Checked)
    End Sub
    Private Sub CheckAll(ByVal isChecked As Boolean)
        For i = 0 To cblClasses.Items.Count - 1
            cblClasses.Items(i).Selected = isChecked
        Next
    End Sub

End Class