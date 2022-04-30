Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary

Public Class ExamAdminHome
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            lblSchoolType.Text = ExecuteQuery_ExecuteScalar("Select SchoolType from Params")
        Catch ex As Exception

        End Try
        'school type =1 >> CBSE
        'school type =2 >> ISCE
        If lblSchoolType.Text = "1" Then
            btnProcess.Visible = True
            btnActivity.Visible = True
            btnMarksInitialize.Visible = True
        Else
            btnProcess.Visible = False
            btnActivity.Visible = False
            btnMarksInitialize.Visible = False
        End If
    End Sub

    Protected Sub btnProcess_Click(sender As Object, e As EventArgs) Handles btnProcess.Click
        Response.Redirect("ExamMarksProcessing.aspx")
    End Sub
   
    Protected Sub btnActivity_Click(sender As Object, e As EventArgs) Handles btnActivity.Click
        Response.Redirect("ExamActivityMapping.aspx")
    End Sub
    Private Sub configMarksTable()
        Dim sqlStr As String = ""
        Dim lstMajorTerms As New List(Of String)
        'get list of terms
        sqlStr = "select Distinct ExamTermMajor,DisplayOrderMaj  from vw_ExamTerms order by DisplayOrderMaj"
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            lstMajorTerms.Add(myReader(0))
        End While
        myReader.Close()

        'get list of Aggregation
        sqlStr = "select AggregationName from ExamTermAggregation order by DisplayOrder"
        myReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            lstMajorTerms.Add(myReader(0))
        End While
        myReader.Close()
        'drop table
        sqlStr = "Drop Table ExamMarks"
        ExecuteQuery_Update(sqlStr)

        'create default table schema
        sqlStr = "CREATE TABLE [dbo].[ExamMarks](" & _
                 "[marksEntry] [int] IDENTITY(1,1) NOT NULL, " & _
                 "[SID] [int] NULL," & _
                 "[SubjectID] [int] NULL, " & _
                 "CONSTRAINT [PK_ExamMarks] PRIMARY KEY CLUSTERED " & _
                 "( [marksEntry] Asc " & _
                 ")WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] ) ON [PRIMARY] "
        ExecuteQuery_Update(sqlStr)

        'add configuration
        sqlStr = "Alter Table ExamMarks Add  "
        For i = 0 To lstMajorTerms.Count - 1
            sqlStr += " [" & lstMajorTerms.Item(i) & "] nvarchar(50) ,"
        Next
        sqlStr += " OverAll [int]"

        ExecuteQuery_Update(sqlStr)

    End Sub

    Private Sub btnMarksInitialize_Click(sender As Object, e As EventArgs) Handles btnMarksInitialize.Click
        configMarksTable()
    End Sub
End Class