Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary

Public Class Set_Leave_Timeframe
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try
            If Request.Cookies("UType").Value.ToString.Contains("Student") Or Request.Cookies("UType").Value.ToString.Contains("Payroll") Or Request.Cookies("UType").Value.ToString.Contains("Admin") Then
                'Allow
            Else
                Response.Redirect("/./AccessDenied.aspx", False)
            End If
        Catch ex As Exception
            response.redirect("~/Login.aspx")
        End Try
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then InitControls()
    End Sub

    Private Sub InitControls()
        Dim sqlStr As String = "Select LeaveStartDate, LeaveEndDate From Params"

       
       
       

        
        
        

        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            Try
                txtStart.Text = myReader("LeaveStartDate")
            Catch ex As Exception
                txtStart.Text = ""
            End Try

            Try
                txtEnd.Text = myReader("LeaveEndDate")
            Catch ex As Exception
                txtEnd.Text = ""
            End Try
        End While
        myReader.Close()
        
        
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        Dim sqlStr As String = ""

       
       
       

        sqlStr = "Update Params Set " & _
            "LeaveStartDate='" & CDate(txtStart.Text).Month & "/" & CDate(txtStart.Text).Day & "/" & CDate(txtStart.Text).Year & "'," & _
            "LeaveEndDate='" & CDate(txtEnd.Text).Month & "/" & CDate(txtEnd.Text).Day & "/" & CDate(txtEnd.Text).Year & "'"

        
        
        

        ExecuteQuery_Update(SqlStr)

        
        

        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Information updated Successfully...');", True)

        InitControls()
    End Sub
End Class