Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary

Partial Class Parent_ViewAcademicCalender
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadAcademicCalender()
    End Sub

    Private Sub LoadAcademicCalender()
        Dim sqlStr As String = ""
        sqlStr = "Select * From AcademicCalender"

        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)

        myTable.Rows.Clear()
        'Add Header Row
        Dim TR0 As New TableRow

        Dim TD01 As New TableCell
        TD01.Width = 150
        TD01.HorizontalAlign = HorizontalAlign.Center
        TD01.BackColor = Drawing.ColorTranslator.FromHtml("#37C464")
        TD01.Text = "Date From"
        TD01.ForeColor = Drawing.Color.White
        TD01.Font.Bold = True
        TR0.Cells.Add(TD01)

        Dim TD02 As New TableCell
        TD02.Width = 150
        TD02.HorizontalAlign = HorizontalAlign.Center
        TD02.BackColor = Drawing.ColorTranslator.FromHtml("#37C464")
        TD02.Font.Bold = True
        TD02.Text = "Date To"
        TD02.ForeColor = Drawing.Color.White
        TR0.Cells.Add(TD02)

        Dim TD03 As New TableCell
        TD03.Width = 500
        TD03.HorizontalAlign = HorizontalAlign.Center
        TD03.BackColor = Drawing.ColorTranslator.FromHtml("#37C464")
        TD03.Font.Bold = True
        TD03.Text = "Event Details"
        TD03.ForeColor = Drawing.Color.White
        TR0.Cells.Add(TD03)

        myTable.Rows.Add(TR0)

        While myReader.Read

            Dim Tr As New TableRow

            Dim TD1 As New TableCell
            TD1.Text = myReader("ACDate")
            TD1.HorizontalAlign = HorizontalAlign.Center
            Tr.Cells.Add(TD1)

            Dim TD2 As New TableCell
            TD2.Text = myReader("ACDateTo")
            TD2.HorizontalAlign = HorizontalAlign.Center
            Tr.Cells.Add(TD2)

            Dim TD3 As New TableCell
            TD3.Text = myReader("ACDetails")
            TD3.HorizontalAlign = HorizontalAlign.Center
            Tr.Cells.Add(TD3)

            myTable.Rows.Add(Tr)

        End While
        myReader.Close()

    End Sub
End Class
