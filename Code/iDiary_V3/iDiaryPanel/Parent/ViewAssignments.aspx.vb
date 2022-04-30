Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary

Partial Class Parent_ViewAssignments
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadAssignments()
    End Sub

    Private Sub LoadAssignments()

        'Find Class/Sec ID of Student mentioned in Session("SID")

        Dim sqlStr As String = ""

        sqlStr = "Select CSSID From Student Where SID=" & Request.Cookies("SID").Value

        Dim CSSID As Integer = 0
        Dim myreader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myreader.Read
            CSSID = myreader(0)
        End While
        myreader.Close()

        'As Per Retrieved ClassSecID, Load Syllabus

        sqlStr = "Select * From Assignments Where CSSID=" & CSSID

        myreader = ExecuteQuery_ExecuteReader(sqlStr)

        myTable.Rows.Clear()
        'Add Header Row
        Dim TR0 As New TableRow

        Dim TD01 As New TableCell
        TD01.Width = 150
        TD01.HorizontalAlign = HorizontalAlign.Center
        TD01.BackColor = Drawing.ColorTranslator.FromHtml("#37C464")
        TD01.Text = "Date"
        TD01.Font.Bold = True
        TD01.ForeColor = Drawing.Color.White
        TR0.Cells.Add(TD01)

        Dim TD02 As New TableCell
        TD02.Width = 700
        TD02.HorizontalAlign = HorizontalAlign.Center
        TD02.BackColor = Drawing.ColorTranslator.FromHtml("#37C464")
        TD02.Font.Bold = True
        TD02.ForeColor = Drawing.Color.White
        TD02.Text = "Details"
        TR0.Cells.Add(TD02)

        Dim TD03 As New TableCell
        TD03.Width = 250
        TD03.HorizontalAlign = HorizontalAlign.Center
        TD03.BackColor = Drawing.ColorTranslator.FromHtml("#37C464")
        TD03.Font.Bold = True
        TD03.ForeColor = Drawing.Color.White
        TR0.Cells.Add(TD03)

        myTable.Rows.Add(TR0)

        While myReader.Read
            Dim Count As Integer = 1
            Dim Tr As New TableRow

            Dim TD1 As New TableCell
            TD1.Text = myReader("UploadDate")
            TD1.Width = 150
            Tr.Cells.Add(TD1)

            Dim TD2 As New TableCell
            Dim myFileName As String = myReader("FileName")
            myFileName = myFileName.Replace(" ", "%20")
            TD2.Text = myreader("Title")
            TD2.Width = 700
            Tr.Cells.Add(TD2)

            Dim TD3 As New TableCell
            Dim btnDetails As New Button()
            btnDetails.ID = myreader("ASSID")
            btnDetails.CssClass = "btn btn-primary"
            btnDetails.Text = "Download"
            AddHandler btnDetails.Click, AddressOf btnDetails_Click
            TD3.Controls.Add(btnDetails)
            Dim FileDownload As String = ""
            FileDownload = myreader("FileName")
            FileDownload = FileDownload.Replace(" ", "%20")
            TD3.Width = 240
            TD3.Height = 40
            TD3.HorizontalAlign = HorizontalAlign.Center
            Tr.Cells.Add(TD3)

            myTable.Rows.Add(Tr)

        End While
        myReader.Close()
    End Sub
    Private Sub btnDetails_Click(sender As Object, e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        Dim FileName As String = btn.ID
        Dim sqlstr As String = "Select FileName from Assignments where ASSID=" & FileName
        Dim rv As String = ExecuteQuery_ExecuteScalar(sqlstr)
        Response.Redirect("/./Assignments/" & rv)
    End Sub
End Class
