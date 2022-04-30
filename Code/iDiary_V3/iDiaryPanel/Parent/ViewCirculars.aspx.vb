Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary

Partial Class Parent_ViewCirculars
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadCirculars()
    End Sub

    Private Sub LoadCirculars()

        Dim sqlStr As String = ""

        sqlStr = "Select * From Circulars "
        
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        Dim myRowNo As Integer = 1

        myTable.Rows.Clear()
        'Add Header Row
        Dim TR0 As New TableRow

        Dim TD01 As New TableCell
        TD01.Width = 100
        TD01.HorizontalAlign = HorizontalAlign.Center
        TD01.BackColor = Drawing.ColorTranslator.FromHtml("#37C464")
        TD01.Text = "Date"
        TD01.Font.Bold = True
        TD01.ForeColor = Drawing.Color.White
        TR0.Cells.Add(TD01)

        Dim TD02 As New TableCell
        TD02.Width = 340
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

            Dim Tr As New TableRow

            Dim TD2 As New TableCell
            TD2.Text = myReader("UploadDate")
            TD2.Width = 150
            TD2.HorizontalAlign = HorizontalAlign.Center
            Tr.Cells.Add(TD2)

            'Dim TD3 As New TableCell
            'Dim myFileName As String = myReader("FileName")
            'myFileName = myFileName.Replace(" ", "%20")
            'TD3.Text = "<A class=""circular"" Href=/./Circulars/" & myFileName & ">" & myReader("Title") & "</A>"
            'TD3.Width = 300
            'TD3.HorizontalAlign = HorizontalAlign.Center
            'Tr.Cells.Add(TD3)

            Dim TD3 As New TableCell
            Dim myFileName As String = myReader("FileName")
            myFileName = myFileName.Replace(" ", "%20")
            TD3.Text = myReader("Title")
            TD3.Width = 700
            Tr.Cells.Add(TD3)

            Dim TD4 As New TableCell
            Dim btnDetails As New Button()
            btnDetails.ID = myReader("CircularID")
            btnDetails.CssClass = "btn btn-primary"
            btnDetails.Text = "Download"
            AddHandler btnDetails.Click, AddressOf btnDetails_Click
            Dim FileDownload As String = ""
            TD4.Controls.Add(btnDetails)
            FileDownload = myReader("FileName")
            FileDownload = FileDownload.Replace(" ", "%20")
            TD4.Width = 240
            TD4.Height = 40
            TD4.HorizontalAlign = HorizontalAlign.Center
            Tr.Cells.Add(TD4)

            myTable.Rows.Add(Tr)
            myRowNo += 1
        End While
        myReader.Close()

    End Sub

    Private Sub btnDetails_Click(sender As Object, e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        Dim FileName As String = btn.ID
        Dim sqlstr As String = "Select FileName from Circulars where CircularID=" & FileName
        Dim rv As String = ExecuteQuery_ExecuteScalar(sqlstr)
        Response.Redirect("/./Circulars/" & rv)
    End Sub
End Class
