Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary

Partial Class Parent_ViewSyllabus
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadSyllabusRelatedInformation()
    End Sub

    Private Sub LoadSyllabusRelatedInformation()

        'Find Class/Sec ID of Student mentioned in Session("SID")
        Dim sqlStr As String = ""

        sqlStr = "Select Max(CSSID) From vw_Student Where SID=" & Request.Cookies("SID").Value

        Dim ClassSecID As Integer = 0
        ClassSecID = ExecuteQuery_ExecuteScalar(sqlStr)

        'As Per Retrieved ClassSecID, Load Syllabus

        sqlStr = "Select * From Syllabus Where CSSID=" & ClassSecID
       
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)

        myTable.Rows.Clear()
        'Add Header Row
        Dim TR0 As New TableRow

        Dim TD01 As New TableCell
        TD01.Width = 100
        TD01.HorizontalAlign = HorizontalAlign.Center
        TD01.BackColor = Drawing.ColorTranslator.FromHtml("#37C464")
        TD01.Text = "Date"
        TD01.ForeColor = Drawing.Color.White
        TD01.Font.Bold = True
        TR0.Cells.Add(TD01)

        Dim TD02 As New TableCell
        TD02.Width = 340
        TD02.HorizontalAlign = HorizontalAlign.Center
        TD02.BackColor = Drawing.ColorTranslator.FromHtml("#37C464")
        TD02.Font.Bold = True
        TD02.ForeColor = Drawing.Color.White
        TD02.Text = "Details"
        TR0.Cells.Add(TD02)

        myTable.Rows.Add(TR0)

        While myReader.Read

            Dim Tr As New TableRow

            Dim TD1 As New TableCell
            TD1.Text = myReader("UploadDate")
            TD1.HorizontalAlign = HorizontalAlign.Center
            Tr.Cells.Add(TD1)

            Dim TD2 As New TableCell
            TD2.HorizontalAlign = HorizontalAlign.Center
            Dim myFileName As String = myReader("FileName")
            myFileName = myFileName.Replace(" ", "%20")
            TD2.Text = "<A class=""circular"" Href=/./Syllabus/" & myFileName & ">" & myReader("Title") & "</A>"
            Tr.Cells.Add(TD2)

            myTable.Rows.Add(Tr)

        End While
        myReader.Close()
    End Sub
End Class
