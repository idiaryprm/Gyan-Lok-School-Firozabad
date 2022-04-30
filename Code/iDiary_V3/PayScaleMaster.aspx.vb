Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary

Public Class PayScaleMaster
    Inherits System.Web.UI.Page


    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try
            If Request.Cookies("UType").Value.ToString.Contains("Admin") Or Request.Cookies("UType").Value.ToString.Contains("Payroll") Or Request.Cookies("UType").Value.ToString.Contains("Student") Then
                'Allow
            Else
                Response.Redirect("/./AccessDenied.aspx", False)
            End If
        Catch ex As Exception
            response.redirect("~/Login.aspx")
        End Try
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim sqlStr As String = ""
        Dim FinalMessage = ""
        If Val(txtID.Text) = 0 Then
            If CheckPayScale() = True Then
                FinalMessage = "Pay Scale is Already Exists..."
                txtPayScaleName.Focus()
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('" & FinalMessage & "');", True)
                Exit Sub
            End If
        End If
        
       
        
       
        
        Dim IsDefault As Integer = 0
        If chkDefault.Checked = True Then
            IsDefault = 1
            sqlStr = "Update PayScale Set IsDefault=0"
            
            
            ExecuteQuery_Update(SqlStr)
        End If

        If Val(txtID.Text) = 0 Then    'Insert Command
            sqlStr = "Insert into PayScale Values('" & txtPayScaleName.Text & "','" & Request.Cookies("UserID").Value & "','" & IsDefault & "')"
            FinalMessage = "New Pay Scale added successfully..."
        Else    'Update Command
            sqlStr = "Update PayScale Set PayScaleName='" & txtPayScaleName.Text & "',CreatedBy='" & Request.Cookies("UserID").Value & "',IsDefault=" & IsDefault & " Where PayScaleID=" & Val(txtID.Text)
            FinalMessage = "Pay Scale updated successfully..."
        End If

        
        
        ExecuteQuery_Update(SqlStr)

        
        

        InitControls()
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('" & FinalMessage & "');", True)

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("ActiveTab") = 4
        If IsPostBack = False Then InitControls()
    End Sub

    Private Sub InitControls()
        LoadMasterInfo(20, lstPayScale)
        txtID.Text = 0
        txtPayScaleName.Text = ""
        txtPayScaleName.Focus()
        chkDefault.Checked = False
        btnRemove.Visible = False
    End Sub

    Protected Sub lstPayScale_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstPayScale.SelectedIndexChanged
        txtID.Text = FindMasterID(20, lstPayScale.Text)
        txtPayScaleName.Text = lstPayScale.Text

       
       
       

        Dim sqlStr As String = "Select * From PayScale Where PayScaleName='" & lstPayScale.Text & "'"
        
        
        
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            Try
                If myReader("IsDefault") = "1" Then
                    chkDefault.Checked = True
                Else
                    chkDefault.Checked = False
                End If
            Catch ex As Exception
                chkDefault.Checked = False
            End Try
        End While
        myReader.Close()
        
        

        'btnRemove.Visible = True
    End Sub

    Protected Sub btnRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        Dim sqlStr As String = ""
        Dim FinalMessage = ""

        If Val(txtID.Text) = 0 Then    'Error
            FinalMessage = "Select a Pay Scale to remove..."
        Else
            sqlStr = "Delete PayScale Where PayScaleID=" & Val(txtID.Text)
            FinalMessage = "Pay Scale removed successfully..."
        End If

       
        
       

        
        
        
        ExecuteQuery_Update(SqlStr)

        
        

        InitControls()

        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('" & FinalMessage & "');", True)

    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        InitControls()
    End Sub
    Private Function CheckPayScale()
       
       
       

        Dim sqlStr As String = "Select Count(*) From PayScale Where PayScaleName='" & txtPayScaleName.Text & "'"
        
        
        
        Dim rv As Integer = ExecuteQuery_ExecuteScalar(SqlStr)
        
        
        If rv > 0 Then
            Return True
        Else
            Return False
        End If
        
        
        Return rv
    End Function
End Class