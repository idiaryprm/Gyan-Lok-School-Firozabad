Imports System.Data.SqlClient

Partial Class Parent_ChangeProfile
    Inherits System.Web.UI.Page

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim sqlStr As String = ""
        Dim myConnStr As String = System.Configuration.ConfigurationManager.ConnectionStrings("iDiaryConnectionString").ToString
        Dim myConn As New SqlConnection(myConnStr)
        myConn.Open()

        Dim myCommand As New SqlCommand

        sqlStr = "Update Student Set TempAddress='" & txtAddress.Text & "',FName='" & txtFName.Text & "',MobNo='" & txtMobNo.Text & "',Email='" & txtEmail.Text & "' Where SID=" & Session("SID")

        myCommand.CommandText = sqlStr
        myCommand.Connection = myConn
        myCommand.ExecuteNonQuery()

        myCommand.Dispose()
        myConn.Dispose()

        InitControls()
        lblStatus.Text = "Information updated successfully..."
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            InitControls()
        End If
    End Sub

    Private Sub InitControls()
        Dim sqlStr As String = ""
        Dim myConnStr As String = System.Configuration.ConfigurationManager.ConnectionStrings("iDiaryConnectionString").ToString
        Dim myConn As New SqlConnection(myConnStr)
        myConn.Open()

        Dim myCommand As New SqlCommand

        sqlStr = "Select FName, MobNo, Email, TempAddress From Student Where SID='" & Session("SID") & "'"

        myCommand.CommandText = sqlStr
        myCommand.Connection = myConn
        Dim myReader As SqlDataReader = myCommand.ExecuteReader
        While myReader.Read
            txtAddress.Text = myReader("TempAddress")
            txtFName.Text = myReader("FName")
            txtMobNo.Text = myReader("MobNo")
            txtEmail.Text = myReader("Email")
        End While
        myReader.Close()

        myCommand.Dispose()
        myConn.Dispose()

    End Sub
End Class
