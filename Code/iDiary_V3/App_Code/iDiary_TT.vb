Imports System.Data.SqlClient
Imports System.Data
Imports System.Collections.Generic
Imports System.Web
Imports System.Web.UI.Control
Imports Microsoft.visualbasic
Imports iDiary_V3.iDiary.CLS_idiary

Namespace iDiary
    Public Class iDiary_TT
        Public Shared Function getTTTeacherWeekLoad(ByVal EmpID As Integer) As String
            Dim sqlStr As String = ""
            Dim rv As String = ""
            sqlStr = "Select Count(empID) from vw_TTGen Where EmpID=" & EmpID
            rv = ExecuteQuery_ExecuteScalar(sqlStr)
            Return rv
        End Function
        Public Shared Function getTTTeacherSubject(ByVal EmpID As Integer, ByVal DayID As Integer, ByVal PeriodID As Integer) As String
            Dim sqlStr As String = ""
            Dim rv As String = ""
            sqlStr = "Select SubjectName from vw_TTGen Where EmpID=" & EmpID & " AND DayID=" & DayID & " AND PeriodID=" & PeriodID
            rv = ExecuteQuery_ExecuteScalar(sqlStr)
            Return rv
        End Function
        Public Shared Function getTTTeacherClass(ByVal EmpID As Integer, ByVal DayID As Integer, ByVal PeriodID As Integer) As String
            Dim sqlStr As String = ""
            Dim rv As String = ""
            sqlStr = "Select CSSNAme from vw_TTGen Where EmpID=" & EmpID & " AND DayID=" & DayID & " AND PeriodID=" & PeriodID
            rv = ExecuteQuery_ExecuteScalar(sqlStr)
            Return rv
        End Function
        Public Shared Function checkEmpAvailability(ByVal periodID As Integer, ByVal dayID As Integer, ByVal empID As Integer) As Integer
            Dim rv As Integer = 0
            Dim sqlStr As String = "Select count(empID) from vw_TTgen where periodID=" & periodID & " AND dayID = " & dayID & " AND empID =" & empID
            rv = ExecuteQuery_ExecuteScalar(sqlStr)
            If rv > 0 Then
                Return 0
            Else
                Return 1  '
            End If
        End Function

        Public Shared Function getEmpIDfromSubject(ByVal cssID As Integer, ByVal subjectID As Integer) As Integer
            Dim rv As Integer = 0
            Dim sqlStr As String = "select Max(empid)  from  vw_UserSubjectPermission where cssID=" & cssID & " AND subjectID= " & subjectID
            Try
                rv = ExecuteQuery_ExecuteScalar(sqlStr)
            Catch ex As Exception
                rv = 0
            End Try
            Return rv
        End Function

        Public Shared Function getTTSubject(ByVal CSSID As Integer, ByVal DayID As Integer, ByVal PeriodID As Integer) As String
            Dim sqlStr As String = ""
            Dim rv As String = ""
            sqlStr = "Select SubjectName from vw_TTGen Where CSSID=" & CSSID & " AND DayID=" & DayID & " AND PeriodID=" & PeriodID
            rv = ExecuteQuery_ExecuteScalar(sqlStr)
            Return rv
        End Function
        Public Shared Function getTTSubjectTeacher(ByVal CSSID As Integer, ByVal DayID As Integer, ByVal PeriodID As Integer) As String
            Dim sqlStr As String = ""
            Dim rv As String = ""
            sqlStr = "Select EmpName from vw_TTGen Where CSSID=" & CSSID & " AND DayID=" & DayID & " AND PeriodID=" & PeriodID
            rv = ExecuteQuery_ExecuteScalar(sqlStr)
            Return rv
        End Function
        Public Shared Function getSubjectContinuousCount(ByVal CSSID As Integer, ByVal SubID As Integer) As Integer
            Dim sqlStr As String = ""
            Dim rv As Integer = 0
            sqlStr = "Select maxContinuousPeriods from TTSubjectConfig Where CSSID=" & CSSID & " AND SubjectID=" & SubID
            rv = ExecuteQuery_ExecuteScalar(sqlStr)
            Return rv
        End Function

        Public Shared Function checkPreviousPeriodConstrain(ByVal cssID As Integer, ByVal SubID As Integer, ByVal periodID As Integer, ByVal dayID As Integer, continuousSubs As Integer) As Integer
            Dim rv As Integer = 0
            Dim sqlStr As String = ""
            Dim preSubID As Integer = 0
            For i = 1 To continuousSubs
                sqlStr = "Select SubjectID from TTGenerate Where CSSID=" & cssID & " AND dayID=" & dayID & " AND periodID=" & periodID - i
                preSubID = ExecuteQuery_ExecuteScalar(sqlStr)
                If preSubID = SubID Then
                    rv += 1
                End If
            Next
            If rv >= continuousSubs Then
                Return 0
            Else
                Return 1
            End If
            Return rv
        End Function
    End Class
End Namespace

