Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary

Partial Class Parent_ViewAttendance
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = True Then InitControls()
    End Sub

    Private Sub InitControls()
        If IsPostBack = False Then
            txtYear.Text = Now.Year
            myTable.Rows.Clear()
            lblStatus.Text = ""

            cboMonth.Focus()
        End If
    End Sub

    Protected Sub btnShow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnShow.Click

        Dim sqlStr As String = "Select AttDate, IsPresentM from Attendance Where SID=" & Request.Cookies("SID").Value & " AND "

        Select Case cboMonth.SelectedIndex
            Case 1 : sqlStr &= " AttDate Between '01/01/" & txtYear.Text & "' and '01/31/" & txtYear.Text & "'"
            Case 2 : sqlStr &= " AttDate >='02/01/" & txtYear.Text & "' and AttDate < '03/01/" & txtYear.Text & "'"
            Case 3 : sqlStr &= " AttDate Between '03/01/" & txtYear.Text & "' and '03/31/" & txtYear.Text & "'"
            Case 4 : sqlStr &= " AttDate Between '04/01/" & txtYear.Text & "' and '04/30/" & txtYear.Text & "'"
            Case 5 : sqlStr &= " AttDate Between '05/01/" & txtYear.Text & "' and '05/31/" & txtYear.Text & "'"
            Case 6 : sqlStr &= " AttDate Between '06/01/" & txtYear.Text & "' and '06/30/" & txtYear.Text & "'"
            Case 7 : sqlStr &= " AttDate Between '07/01/" & txtYear.Text & "' and '07/31/" & txtYear.Text & "'"
            Case 8 : sqlStr &= " AttDate Between '08/01/" & txtYear.Text & "' and '08/31/" & txtYear.Text & "'"
            Case 9 : sqlStr &= " AttDate Between '09/01/" & txtYear.Text & "' and '09/30/" & txtYear.Text & "'"
            Case 10 : sqlStr &= " AttDate Between '10/01/" & txtYear.Text & "' and '10/31/" & txtYear.Text & "'"
            Case 11 : sqlStr &= " AttDate Between '11/01/" & txtYear.Text & "' and '11/30/" & txtYear.Text & "'"
            Case 12 : sqlStr &= " AttDate Between '12/01/" & txtYear.Text & "' and '12/31/" & txtYear.Text & "'"
        End Select

        sqlStr &= " Order by AttDate"

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
        TD02.ForeColor = Drawing.Color.White
        TD02.Font.Bold = True
        TD02.Text = "Status"
        TR0.Cells.Add(TD02)

        myTable.Rows.Add(TR0)

        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            Dim myDate As String = CDate(myReader("AttDate")).Day & "/" & CDate(myReader("AttDate")).Month & "/" & CDate(myReader("AttDate")).Year
            Dim myStatus As String = ""
            If myReader("IsPresentM") = 0 Then myStatus = "Absent"
            If myReader("IsPresentM") = 1 Then myStatus = "Present"
            If myReader("IsPresentM") = 2 Then myStatus = "On Leave"

            Dim Tr As New TableRow

            Dim TD1 As New TableCell
            TD1.HorizontalAlign = HorizontalAlign.Center
            TD1.Text = myDate
            Tr.Cells.Add(TD1)

            Dim TD2 As New TableCell
            TD2.HorizontalAlign = HorizontalAlign.Center
            TD2.Text = myStatus
            Tr.Cells.Add(TD2)

            myTable.Rows.Add(Tr)
        End While
        myReader.Close()
    End Sub
End Class
