Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Public Class AdminEntry
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then cboAllow.SelectedIndex = getEntryStatus()
    End Sub

    Protected Sub btnAllow_Click(sender As Object, e As EventArgs) Handles btnAllow.Click
        Dim sqlStr As String = ""
        
        Dim status As Integer = 0
        '0---------not allowed
        '1---------Allowed

        sqlStr = "Update PARAMS set OnlineEntryAllowed='" & cboAllow.SelectedIndex & "'"

        
        
        status = ExecuteQuery_Update(sqlStr)

        
        
    End Sub
    Private Function getEntryStatus() As Integer
        Dim sqlStr As String = ""
        
        Dim status As Integer = 0
        '0---------not allowed
        '1---------Allowed
       
        sqlStr = "select OnlineEntryAllowed from PARAMS"
        Try
            
            
            status = ExecuteQuery_ExecuteScalar(SqlStr)
        Catch ex As Exception

        End Try
        
        
        Return status
    End Function

End Class