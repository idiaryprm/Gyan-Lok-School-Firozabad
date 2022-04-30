Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Public Class BackupRecovery
    Inherits System.Web.UI.Page
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try
            If Request.Cookies("UType").Value.ToString.Contains("Admin") Then
                'Allow
            Else
                Response.Redirect("~/AccessDenied.aspx")
            End If
        Catch ex As Exception
            Response.Redirect("~/Login.aspx")
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' If IsPostBack = False Then InitControls()
    End Sub

    Protected Sub btnBackup_Click(sender As Object, e As EventArgs) Handles btnBackup.Click
        Dim sqlstr As String = "SELECT DB_NAME()"
        Dim dbName As String = ExecuteQuery_ExecuteScalar(sqlstr)


        Dim BackupName As String = "Backup\" & dbName & "_" & Now.Day.ToString & "_" & Now.Month.ToString & "_" & Now.Year.ToString & "_" & Now.Hour.ToString & "_" & Now.Minute.ToString & "_" & Now.Second.ToString & ".bak"
        Dim filename As String = Server.MapPath(BackupName)

        sqlstr = "backup database " & dbName & " to disk='" & filename & "'"
        ExecuteQuery_Update(sqlstr)
        Response.Redirect(BackupName)
    End Sub

    Protected Sub ButtonRecovery_Click(sender As Object, e As EventArgs) Handles ButtonRecovery.Click
        Dim filename As String = myFile.PostedFile.FileName

        If filename.ToString() <> "" Then
            Dim sp1 As String = ""
            sp1 = Server.MapPath("Backup")
            If sp1.EndsWith("\\") = False Then
                sp1 += "\"
            End If
            Dim BackupName As String = "BackupRecover" & Now.Day.ToString & "_" & Now.Month.ToString & "_" & Now.Year.ToString & "_" & Now.Hour.ToString & "_" & Now.Minute.ToString & "_" & Now.Second.ToString & ".zip"
            sp1 &= BackupName
            myFile.PostedFile.SaveAs(sp1)

            'Server.MapPath("Backup\" & myFile.PostedFile.FileName))
            '"D:\Backup_" + Now.Day.ToString + "_" + Now.Month.ToString + "_" + Now.Year.ToString + ".Bak"

            'myCommand.CommandText = "alter database dbKsj set single_user with rollback immediate"
            '
            'ExecuteQuery_Update(SqlStr)
            Dim sqlstr As String = "RESTORE database iDiaryV3_School FROM disk='" & sp1 & "';"
            
            ExecuteQuery_Update(SqlStr)

            
            
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Database has been Restored...');", True)
        Else
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Select Backup file...');", True)
            myFile.Focus()
        End If



        'Dim fp1 As String = myFile.PostedFile.FileName
        'If fp1.ToString() <> "" Then
        '    Dim fn1 As String = fp1.Substring(fp1.LastIndexOf("\\") + 1)
        '    Dim sp1 As String = ""
        '    sp1 = Server.MapPath("Backup")
        '    If sp1.EndsWith("\\") = False Then
        '        sp1 += "\"
        '    End If

        '    sp1 &= txtSRNo.Text & "_" & Request.Cookies("ASID").Value & ".jpg"
        '    myFile.PostedFile.SaveAs(sp1)
        '    imgPhoto.ImageUrl = "~\Photos\" & txtSRNo.Text & "_" & Request.Cookies("ASID").Value & ".jpg"
        'End If
        'txtFeeBookNo.Focus()
    End Sub

    Protected Sub rbBackup_CheckedChanged(sender As Object, e As EventArgs) Handles rbBackup.CheckedChanged
        If rbBackup.Checked = True Then
            btnBackup.Visible = True
            ButtonRecovery.Visible = False
            myFile.Visible = False
        Else
            btnBackup.Visible = False
            ButtonRecovery.Visible = True
            myFile.Visible = True
        End If
    End Sub

    Protected Sub rbRecovery_CheckedChanged(sender As Object, e As EventArgs) Handles rbRecovery.CheckedChanged
        If rbBackup.Checked = True Then
            btnBackup.Visible = True
            ButtonRecovery.Visible = False
            myFile.Visible = False
        Else
            btnBackup.Visible = False
            ButtonRecovery.Visible = True
            myFile.Visible = True
        End If
    End Sub
End Class