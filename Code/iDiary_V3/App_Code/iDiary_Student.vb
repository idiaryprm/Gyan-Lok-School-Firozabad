Imports System.Data.SqlClient
Imports System.Data
Imports System.Collections.Generic
Imports System.Web
Imports System.Web.UI.Control
Imports iDiary_V3.iDiary.CLS_idiary
Namespace iDiary_Student

    Public Class CLS_iDiary_Student

        '---Get Default Student Status ID---
        Public Shared Function GetStudentDefaultStatusID() As Integer

            Dim StatusID As Integer = 0

            Dim sqlStr As String = ""
            

            sqlStr = "Select DefaultStudentStatus From Params"
            
            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            While myReader.Read
                Try
                    StatusID = myReader(0)
                Catch ex As Exception
                    StatusID = 0
                End Try

            End While
            myReader.Close()

            Return StatusID

        End Function

        Public Shared Function AddSibling(ByVal SibName As String, ByVal SibClass As String, ByVal SibSec As String) As Integer

            Dim sqlStr As String = "Insert into TempSiblings Values(" & _
            "'" & SibName & "'," & _
            "'" & SibClass & "'," & _
            "'" & SibSec & "')"

            ExecuteQuery_Update(SqlStr)

            Return 0
        End Function

        Public Shared Function ClearTempSiblingTable() As Integer

           
            Return 0
        End Function

        Public Shared Function FindSID(ByVal AdminNo As String, ByVal ASID As Integer) As Integer

            Dim sqlStr As String = "Select Max(SID) From Student Where RegNo='" & AdminNo & "' AND ASID=" & ASID
            
            
            Dim rv As Integer = 0
            Try
                rv = ExecuteQuery_ExecuteScalar(SqlStr)
            Catch ex As Exception
                rv = 0
            End Try

            Return rv

        End Function

        Public Shared Function CheckSRExist(ByVal SRNo As String, ByVal ASID As Integer, Optional SID As Integer = 0) As Boolean

            Dim sqlStr As String = "Select Count(RegNo) From vw_Student Where RegNo='" & SRNo & "' AND ASID=" & ASID
            If SID > 0 Then
                sqlStr += " and SID<>" & SID
            End If

            Dim rv As Integer = ExecuteQuery_ExecuteScalar(SqlStr)

            If rv <= 0 Then
                Return False
            Else
                Return True
            End If

        End Function

        Public Shared Function CheckFeeBookNoExist(ByVal FeeBookNo As String, ByVal ASID As Integer, Optional SID As Integer = 0) As Boolean

            Dim sqlStr As String = "Select Count(FeeBookNo) From vw_Student Where FeeBookNo='" & FeeBookNo & "' AND ASID=" & ASID
            If SID > 0 Then
                sqlStr += " and SID<>" & SID
            End If

            Dim rv As Integer = ExecuteQuery_ExecuteScalar(SqlStr)

            If rv <= 0 Then
                Return False
            Else
                Return True
            End If

        End Function

        Public Shared Function CheckFormNoExist(ByVal FormNo As String, ByVal ASID As Integer) As Boolean

            Dim sqlStr As String = "Select Count(FormNo) From Student Where FormNo='" & FormNo & "' AND ASID=" & ASID

            Dim rv As Integer = ExecuteQuery_ExecuteScalar(SqlStr)

            If rv <= 0 Then
                Return False
            Else
                Return True
            End If

        End Function


        Public Shared Function AddBusSibling(ByVal SibName As String, ByVal SibClass As String, ByVal SibSec As String) As Integer

            Dim sqlStr As String = "Insert into TempBusSiblings Values(" & _
            "'" & SibName & "'," & _
            "'" & SibClass & "'," & _
            "'" & SibSec & "')"

            ExecuteQuery_Update(SqlStr)

            Return 0
        End Function

        Public Shared Function ClearTempBusSiblingTable() As Integer

            Dim sqlStr As String = "Delete From TempBusSiblings"
            
            ExecuteQuery_Update(SqlStr)

            Return 0
        End Function

        Public Shared Function InsertSiblings(ByVal TmpSID As String, ByVal SID As String) As Integer

            Dim sqlStr As String = "Insert into Siblings Values(" & _
              "'" & SID & "'," & _
              "'" & TmpSID & "')"

            ExecuteQuery_Update(SqlStr)

            Return 0
        End Function

        Public Shared Function GetSectionStrength(SchoolName As String, ByVal className As String, ByVal ASID As Integer) As String

            Dim rv As String = ""
            Dim sqlStr As String = " Select SecName,Count(SID) From vw_Student " & _
                " Where SchoolName='" & SchoolName & "' and ClassName='" & className & "' and " & _
                             " StatusName='Active' and " & _
                " ASID=" & ASID & " group by SecName"
            '" SecName='" & SecName & "' and " & _



            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            While myReader.Read
                Try
                    rv += myReader(0) & " - " & myReader(1) & ",  "
                Catch ex As Exception

                End Try

            End While
            myReader.Close()

            Return rv
        End Function

    End Class

End Namespace