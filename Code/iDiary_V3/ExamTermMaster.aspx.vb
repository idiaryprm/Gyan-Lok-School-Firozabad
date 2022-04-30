Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary

Partial Class ExamTermMaster
    Inherits System.Web.UI.Page


    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try
            If Request.Cookies("UType").Value.ToString.Contains("Admin") Then
                'Allow
            Else
                Response.Redirect("AccessDenied.aspx")
            End If
        Catch ex As Exception
            Response.Redirect("Login.aspx")
        End Try
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        If Trim(txtName.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Enter Term Name...');", True)
            txtName.Focus()
            Exit Sub
        End If
        If IsNumeric(txtDisplayOrder.Text) = False Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Display Order...');", True)
            txtDisplayOrder.Focus()
            Exit Sub
        End If

        Dim sqlStr As String = ""
        Dim FinalMessage = ""
        If Val(txtID.Text) = 0 Then
            If CheckDoubleEntry(102, txtName.Text) <> 0 Then
                FinalMessage = "Term Already Exists..."
                txtName.Focus()
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('" & FinalMessage & "');", True)
                Exit Sub
            End If
        End If


        If Val(txtID.Text) = 0 Then    'Insert Command
            sqlStr = "Insert into ExamTermMaster Values('" & txtName.Text & "','" & txtDisplayOrder.Text & "','" & cboTermType.SelectedIndex & "','" & Request.Cookies("USerID").Value & "')"
            FinalMessage = "New Exam Term added successfully..."
        Else    'Update Command
            sqlStr = "Update ExamTermMaster Set ExamTermName='" & txtName.Text & "',DisplayOrder='" & txtDisplayOrder.Text & "', isMinor='" & cboTermType.SelectedIndex & "' Where ExamTermID=" & Val(txtID.Text)
            FinalMessage = "Exam Term updated successfully..."
        End If


        ExecuteQuery_Update(sqlStr)

        InitControls()
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('" & FinalMessage & "');", True)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then InitControls()
    End Sub

    Private Sub InitControls()
        LoadMasterInfo(102, lstMaster)
        txtID.Text = 0
        txtName.Text = ""
        txtName.Focus()
        txtDisplayOrder.Text = 0
        btnRemove.Visible = False
    End Sub

    Protected Sub lstMaster_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstMaster.SelectedIndexChanged

        Dim sqlStr As String = "Select * From ExamTermMaster Where ExamTermName='" & lstMaster.Text & "'"

        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            txtID.Text = myReader("ExamTermID")
            txtName.Text = myReader("ExamTermName")
            txtDisplayOrder.Text = myReader("DisplayOrder")
            If myReader("isMinor") = 0 Then
                cboTermType.SelectedIndex = 0
            Else
                cboTermType.SelectedIndex = 1
            End If
        End While
        myReader.Close()

    End Sub

    Protected Sub btnRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        Dim sqlStr As String = ""
        Dim FinalMessage = ""

        If Val(txtID.Text) = 0 Then    'Error
            FinalMessage = "Select a Exam Term to remove..."
        Else
            sqlStr = "Delete ExamTermMaster Where ExamTermID=" & Val(txtID.Text)
        End If

        'ExecuteQuery_Update(sqlStr)
        'FinalMessage = "Exam Group removed successfully..."
        InitControls()
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('" & FinalMessage & "');", True)

    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        InitControls()
    End Sub

End Class
