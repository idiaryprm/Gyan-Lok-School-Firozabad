Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary

Public Class ChangePassword
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            InitControls()
        Else
        End If
    End Sub

    Private Sub InitControls()
        txtuserid.Text = Request.Cookies("UserID").Value
        Dim sqlStr As String = ""
        Dim myCount As Integer = 0
        sqlStr = "Select UserName,LoginID,EmpName,DesgName,DeptName,Mob,LoginPass From Users Left OUTER JOIN vw_Employees on Users.EmpID=vw_Employees.EmpID Where UserID='" & txtuserid.Text & "'"
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            Try
                txtusername.Text = myReader("UserName")
            Catch ex As Exception

            End Try
            Try
                txtloginid.Text = myReader("LoginID")
            Catch ex As Exception

            End Try
            Try
                txtempname.Text = myReader("EmpName")
            Catch ex As Exception

            End Try
            Try
                txtdesignaion.Text = myReader("DesgName")
            Catch ex As Exception

            End Try
            Try
                txtdept.Text = myReader("DeptName")
            Catch ex As Exception

            End Try
            Try
                txtmobno.Text = myReader("Mob")
            Catch ex As Exception

            End Try
            Try
                txtempoldpassword.Text = myReader("LoginPass")
            Catch ex As Exception

            End Try
        End While
        myReader.Close()
        txtoldpassword.Focus()
    End Sub

    Protected Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        If txtoldpassword.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Old Password is Required...');", True)
            txtoldpassword.Focus()
            txtoldpassword.Text = ""
            txtnewpassword.Text = ""
            txtconfirmpassword.Text = ""
            Exit Sub
        End If
        If txtoldpassword.Text <> txtempoldpassword.Text Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Old Password did not match...');", True)
            txtoldpassword.Focus()
            txtoldpassword.Text = ""
            txtnewpassword.Text = ""
            txtconfirmpassword.Text = ""
            Exit Sub
        End If
        If txtnewpassword.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Enter New Password...');", True)
            txtoldpassword.Text = ""
            txtnewpassword.Text = ""
            txtconfirmpassword.Text = ""
            txtnewpassword.Focus()
            Exit Sub
        End If
        If txtnewpassword.Text <> txtconfirmpassword.Text Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Password mismatch...');", True)
            txtoldpassword.Text = ""
            txtnewpassword.Text = ""
            txtconfirmpassword.Text = ""
            txtnewpassword.Focus()
            Exit Sub
        End If
        Dim sqlStr As String = ""
        sqlStr = "update Users set LoginPass='" & txtnewpassword.Text & "' where UserID='" & txtuserid.Text & "'"
        ExecuteQuery_Update(sqlStr)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Password updated Successfully...');", True)
        txtnewpassword.Text = ""
        txtconfirmpassword.Text = ""
    End Sub
End Class