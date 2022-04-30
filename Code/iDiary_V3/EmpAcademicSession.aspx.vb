Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Public Class EmpAcademicSession
    Inherits System.Web.UI.Page
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try
            If Request.Cookies("UType").Value.ToString.Contains("Admin") Or Request.Cookies("UType").Value.ToString.Contains("Payroll") Then
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
            LoadMasterInfo(33, cboSession)   ''Load Academic Sessions
            Try
                cboSession.Text = Request.Cookies("EmpASName").Value
            Catch ex As Exception

            End Try
            cboSession.Focus()
        End If
        If Request.Cookies("UType").Value.ToString.Contains("Admin-1") = True Or Request.Cookies("UType").Value.ToString.Contains("Payroll-1") = True Then
        Else
            btnNext.Enabled = False
        End If
    End Sub

    Protected Sub btnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNext.Click
        Dim myID As Integer = 0
        myID = FindMasterID(33, cboSession.Text)

        If myID <= 0 Then
            'do nothing
        Else
            Request.Cookies("EmpASName").Value = cboSession.Text
            Request.Cookies("EmpASID").Value = myID
            Dim sqlstr As String = "update Users set EmpASID='" & Request.Cookies("EmpASID").Value & "' where UserID='" & Request.Cookies("UserID").Value & "'"
            ExecuteQuery_Update(sqlstr)
            'SaveLastUsedASID(myID, cboSession.Text)
            Response.Redirect("~/EmpPayrollHome.aspx")
        End If
    End Sub

End Class