Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Public Class AcademicSession
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            LoadMasterInfo(1, cboSession)   ''Load Academic Sessions
            cboSession.Focus()
        End If
        'If Request.Cookies("UType").Value.ToString.Contains("Admin-1") = False Then
        '    btnNext.Enabled = False
        'End If
    End Sub

    Protected Sub btnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNext.Click
        Dim myID As Integer = 0
        myID = FindMasterID(1, cboSession.Text)

        If myID <= 0 Then
            'do nothing
        Else
            Dim cookieASName As New HttpCookie("ASName")
            cookieASName.Value = cboSession.Text
            Response.Cookies.Add(cookieASName)
            Dim cookieASID As New HttpCookie("ASID")
            cookieASID.Value = myID
            Response.Cookies.Add(cookieASID)
            SaveLastUsedASID(myID, Request.Cookies("UserID").Value)
            If Request.Cookies("UType").Value.ToString.Contains("Admin") = True Then
                Response.Redirect("~/Dashboard.aspx")
            Else
                Response.Redirect("~/Index.aspx")
            End If
        End If
    End Sub

End Class