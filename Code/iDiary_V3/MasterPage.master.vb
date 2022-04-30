Imports iDiary_V3.iDiary.CLS_idiary
Public Class MasterPage
    Inherits System.Web.UI.MasterPage
    Public Shared var1 As Integer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            imgPhoto.ImageUrl = "~/EmpPhotos/" & Request.Cookies("loginID").Value & ".jpg"
        Catch ex As Exception
            imgPhoto.ImageUrl = "~/images/EmpDummy.jpg"
        End Try

        var1 = Session("ActiveTab")
        'lblTime.Text = Now.ToString("dd-MM-yyyy hh:mm:ss tt")
        Dim UID As String = ""

        Try
            UID = Request.Cookies("UID").Value
            btnASName.Text = "Academic Session : " & Request.Cookies("ASName").Value
            Dim INDIAN_ZONE As TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time")
            Dim indianTime As DateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE)
            'btnUserName.Text = "Welcome " & Request.Cookies("UserName").Value & ", " & indianTime.ToString("dddd, MMM dd, yyyy, h:mm tt")
            '& "-" & DateTime.Now.ToString("h:mm:ss tt")
        Catch ex As Exception
            Request.Cookies("ASID").Value = Nothing
            Request.Cookies("ASName").Value = Nothing
            Request.Cookies("UID").Value = Nothing
            Response.Redirect("Login.aspx")
        End Try
        If Request.Cookies("SchoolIDs").Value = "1" Or Request.Cookies("SchoolIDs").Value = "1,2,3" Then
            lblSchoolName.Text = "GYAN LOK INTER COLLEGE"
        End If
        If Request.Cookies("SchoolIDs").Value = "2" Then
            lblSchoolName.Text = "VISHWA BHARTI INTERNATIONAL SCHOOL"
        End If
        If Request.Cookies("SchoolIDs").Value = "3" Then
            lblSchoolName.Text = "GYAN SAROVAR INTER COLLEGE"
        End If
       
        'lblSchoolName.Text = FindSchoolDetails1(1)
        'lblSchoolName.Text = "GYAN LOK INTER COLLEGE"
    End Sub
    Public Function getActiveTab(ByVal tabindex As Integer)
        If tabindex = var1 Then
            Return "sideli active "
        Else
            Return "sideli"
        End If
    End Function
    Public Function getPermissionTab(ByVal tabVal As String)
        If Request.Cookies("UType").Value.ToString.Contains("Admin") Or Request.Cookies("UType").Value.ToString.Contains(tabVal) Then
            Return "nav nav-second-level"
        Else
            Return "nav nav-second-level hide"
        End If
    End Function
    Protected Sub btnASName_Click(sender As Object, e As EventArgs) Handles btnASName.Click
        Response.Redirect("AcademicSession.aspx")
    End Sub

    Protected Sub btnAdmin_Click(sender As Object, e As EventArgs) Handles btnAdmin.Click
        Response.Redirect("AdminHome.aspx")
    End Sub
End Class