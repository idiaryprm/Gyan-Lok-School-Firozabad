Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Public Class SchoolInfo
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try
            If Request.Cookies("UType").Value.ToString.Contains("Admin") Then
                'Allow
            Else
                Response.Redirect("/./AccessDenied.aspx", False)
            End If
        Catch ex As Exception
            response.redirect("~/Login.aspx")
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            InitControls()
        End If
    End Sub

    Private Sub InitControls()
        LoadSchoolInformation()
    End Sub

    Private Sub LoadSchoolInformation()

        Dim sqlStr As String = ""
       
       
       

        

        sqlStr = "Select SchoolName, SchoolDetails From Params"
        
        

        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            txtSchoolName.Text = myReader(0)
            txtSchoolDetails.Text = myReader(1)
        End While
        myReader.Close()

        imgLogo.ImageUrl = "Images/logo.png"
        
        



    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

    End Sub
End Class