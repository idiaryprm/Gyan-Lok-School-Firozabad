Imports System.Data.SqlClient

Partial Class Parent_ViewClassmates
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadClassMates()
    End Sub

    Private Sub LoadClassMates()

        'Find Class/Sec ID of Student mentioned in Session("SID")

        Dim myConnStr As String = System.Configuration.ConfigurationManager.ConnectionStrings("iDiaryConnectionString").ToString
        Dim myConn As New SqlConnection(myConnStr)
        myConn.Open()

        Dim myCommand As New SqlCommand
        Dim sqlStr As String = ""

        sqlStr = "Select Max(ClassSecID) From vwStudent Where SchoolID=" & Session("SchoolID") & " AND SID=" & Session("SID")
        myCommand.CommandText = sqlStr
        myCommand.Connection = myConn

        Dim ClassSecID As Integer = 0
        ClassSecID = myCommand.ExecuteScalar

        'As Per Retrieved ClassSecID, Load Syllabus

        sqlStr = "Select StudentName, PhotoPath, SID From Students Where SchoolID=" & Session("SchoolID") & " AND ClassSecID=" & ClassSecID
        myCommand.CommandText = sqlStr
        myCommand.Connection = myConn

        Dim myReader As SqlDataReader = myCommand.ExecuteReader

        Dim StudentCount As Integer = 1
        Dim lstName As New ListBox
        Dim lstPhotoPath As New ListBox

        lstName.Items.Clear()
        lstPhotoPath.Items.Clear()

        While myReader.Read
            If StudentCount <= 4 Then
                lstName.Items.Add(myReader("StudentName"))
                lstPhotoPath.Items.Add(myReader("PhotoPath"))
            End If

            If StudentCount = 4 Then
                Dim Tr As New TableRow

                Dim TD1 As New TableCell
                TD1.HorizontalAlign = HorizontalAlign.Center
                TD1.Text = "<img src=../Photos/" & lstPhotoPath.Items(0).Text & " width=95 height=110 border=1 /> <BR />" & lstName.Items(0).Text
                Tr.Cells.Add(TD1)

                Dim TD2 As New TableCell
                TD2.HorizontalAlign = HorizontalAlign.Center
                TD2.Text = "<img src=../Photos/" & lstPhotoPath.Items(1).Text & " width=95 height=110 border=1 /> <BR />" & lstName.Items(1).Text
                Tr.Cells.Add(TD2)

                Dim TD3 As New TableCell
                TD3.HorizontalAlign = HorizontalAlign.Center
                TD3.Text = "<img src=../Photos/" & lstPhotoPath.Items(2).Text & " width=95 height=110 border=1 /> <BR />" & lstName.Items(2).Text
                Tr.Cells.Add(TD3)

                Dim TD4 As New TableCell
                TD4.HorizontalAlign = HorizontalAlign.Center
                TD4.Text = "<img src=../Photos/" & lstPhotoPath.Items(3).Text & " width=95 height=110 border=1 /> <BR />" & lstName.Items(3).Text
                Tr.Cells.Add(TD4)

                myTable.Rows.Add(Tr)

                StudentCount = 1
                lstName.Items.Clear()
                lstPhotoPath.Items.Clear()
            Else
                StudentCount += 1
            End If

        End While
        myReader.Close()

        Dim Tr1 As New TableRow
        Dim i As Integer = 0
        For i = 0 To lstName.Items.Count - 1

            Dim TD11 As New TableCell
            TD11.HorizontalAlign = HorizontalAlign.Center
            TD11.Text = "<img src=../Photos/" & lstPhotoPath.Items(i).Text & " width=95 height=110 border=1 /> <BR />" & lstName.Items(i).Text
            Tr1.Cells.Add(TD11)
        Next

        myTable.Rows.Add(Tr1)
        myCommand.Dispose()
        myConn.Dispose()

    End Sub
End Class
