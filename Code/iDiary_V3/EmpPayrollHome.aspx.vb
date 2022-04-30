Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Public Class EmpPayrollHome
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
        Session("ActiveTab") = 4
        'If IsPostBack = False Then
        '    LoadMasterInfo(1, cboSession)   ''Load Academic Sessions
        '    cboSession.Focus()
        'End If
        'If Request.Cookies("UType").Value.ToString.Contains("Admin-1") = False Then
        '    btnNext.Enabled = False
        'End If
    End Sub

    'Protected Sub btnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNext.Click
    '    Dim myID As Integer = 0
    '    myID = FindMasterID(1, cboSession.Text)

    '    If myID <= 0 Then
    '        'do nothing
    '    Else
    '        Request.Cookies("ASName").Value = cboSession.Text
    '        Request.Cookies("ASID").Value = myID
    '        'SaveLastUsedASID(myID, cboSession.Text)
    '        Response.Redirect("~/Index.aspx")
    '    End If
    'End Sub

End Class