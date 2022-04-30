Imports iDiary_V3.iDiary.CLS_idiary
Imports System.Data.SqlClient
Imports Microsoft.Reporting.WebForms
Imports iDiary_V3.iDiary.iDiary_TT

Public Class TTGenerate
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("ActiveTab") = 5
    End Sub

    Protected Sub btnGenerate_Click(sender As Object, e As EventArgs) Handles btnGenerate.Click
        Dim ASID As Integer = Request.Cookies("ASID").Value
        Dim CSSID As Integer = 0
        Dim SubjectID As Integer = 0, tmpSubID As Integer = 0, EmpID As Integer = 0, loopCount As Integer = 0
        Dim maxContinuousPeriods As Integer = 0
        Dim sqlStr As String = ""
        Dim lstPeriods As List(Of Integer) = getList("Select ttPeriodID from TTPeriodMaster")
        Dim lstDays As List(Of Integer) = getList("Select ttDayID from TTDayMaster")
        Dim lstCSSID As List(Of Integer) = getList("select * from vw_ClassStudent where classname='I' and SecName < 'E'")
        Dim lstSubjects As List(Of Integer)
        For c = 0 To lstCSSID.Count - 1
            CSSID = lstCSSID.Item(c)
            loopCount = 0
            lstSubjects = getSubjectWiseList(CSSID)
            For p = 0 To lstPeriods.Count - 1
                For d = 0 To lstDays.Count - 1
TTinsert:
                    EmpID = getEmpIDfromSubject(CSSID, lstSubjects(0))
                    If EmpID > 0 And (checkPreviousPeriodConstrain(CSSID, lstSubjects(0), lstPeriods.Item(p), lstDays.Item(d), getSubjectContinuousCount(CSSID, lstSubjects(0))) = 1) Then

                        If (checkEmpAvailability(lstPeriods.Item(p), lstDays.Item(d), EmpID) = 1) Then
                            sqlStr = "Insert into TTGenerate(CSSID,dayID,periodID,SubjectID,EmpID,ASID) Values(" & CSSID & "," & lstDays.Item(d) & "," & lstPeriods.Item(p) & "," & lstSubjects(0) & "," & EmpID & "," & ASID & ")"
                            ExecuteQuery_Update(sqlStr)
                            lstSubjects.RemoveAt(0)
                        Else
                            If loopCount > lstSubjects.Count Then Exit For
                            tmpSubID = lstSubjects(0)
                            lstSubjects.RemoveAt(0)
                            lstSubjects.Add(tmpSubID)
                            loopCount += 1
                            GoTo TTinsert
                        End If
                    Else
                        tmpSubID = lstSubjects(0)
                        lstSubjects.RemoveAt(0)
                        lstSubjects.Add(tmpSubID)

                        GoTo TTinsert
                    End If

                Next
                If loopCount > lstSubjects.Count Then Exit For
            Next
        Next

    End Sub
   
    Private Function getSubjectWiseList(ByVal CSSID As Integer) As List(Of Integer)
        Dim sqlStr As String = ""
        Dim lstType As New List(Of Integer)
        Dim totalWeek As Integer = 0
        sqlStr = "Select SubjectID,totalperiodWeek from vw_TT1 Where CSSID=" & CSSID & " Order by SubjectID, TTWeightage Desc"
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            totalWeek = myReader(1)
            For t = 1 To totalWeek
                lstType.Add(myReader(0))
            Next

        End While
        Return lstType
    End Function

    Private Function getList(ByVal sqlStr As String) As List(Of Integer)
        Dim lstType As New List(Of Integer)
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            lstType.Add(myReader(0))
        End While
        Return lstType
    End Function

    Private Sub PrepareReport(ByVal sqlStr As String)
        Dim ds As New DataSet
        ds = ExecuteQuery_DataSet(sqlStr, "tbl")

        Dim rds As ReportDataSource = New ReportDataSource()
        rds.Name = "DataSet1" ' Change to what you will be using when creating an objectdatasource
        rds.Value = ds.Tables(0)
        With ReportViewer1   ' Name of the report control on the form
            .Reset()
            .ProcessingMode = ProcessingMode.Local
            .LocalReport.DataSources.Clear()
            .Visible = True
            .LocalReport.ReportPath = "rptTT.rdlc"
            .LocalReport.DataSources.Add(rds)
        End With

        'Dim params(4) As Microsoft.Reporting.WebForms.ReportParameter
        'params(0) = New Microsoft.Reporting.WebForms.ReportParameter("reportHeader", rptHeader, True)
        'params(1) = New Microsoft.Reporting.WebForms.ReportParameter("reportContent", rptContent, True)
        'params(2) = New Microsoft.Reporting.WebForms.ReportParameter("reportParam", rptParam, True)
        'params(3) = New Microsoft.Reporting.WebForms.ReportParameter("termNo", termNo, True)
        'params(4) = New Microsoft.Reporting.WebForms.ReportParameter("TermIndex", cboTerm.SelectedIndex, True)
        'Me.ReportViewer1.LocalReport.SetParameters(params)

        ReportViewer1.Visible = True
        ReportViewer1.LocalReport.Refresh()
    End Sub

    Protected Sub btnView_Click(sender As Object, e As EventArgs) Handles btnView.Click
        PrepareReport("select * from vw_TTgen")
    End Sub
End Class