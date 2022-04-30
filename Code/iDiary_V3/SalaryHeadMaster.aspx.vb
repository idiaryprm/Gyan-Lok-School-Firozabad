Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary

Public Class SalaryHeadMaster
    Inherits System.Web.UI.Page


    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try
            If Request.Cookies("UType").Value.ToString.Contains("Admin") Or Request.Cookies("UType").Value.ToString.Contains("Student") Or Request.Cookies("UType").Value.ToString.Contains("Payroll") Then
                'Allow
            Else
                Response.Redirect("/./AccessDenied.aspx", False)
            End If
        Catch ex As Exception
            response.redirect("~/Login.aspx")
        End Try
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim sqlStr As String = "", FinalMessage = ""

       
        
       
        Dim HeadType As Integer = Request.QueryString("HeadType")
        
        Dim IsDefault As Integer = 0
        If chkDefault.Checked = True Then
            IsDefault = 1
            sqlStr = "Update SalaryHeadMaster Set IsDefault=0 where HeadType=" & HeadType
            
            
            ExecuteQuery_Update(SqlStr)
        End If

        If Val(txtID.Text) = 0 Then    'Insert Command
            sqlStr = "Insert into SalaryHeadMaster Values('" & txtHeadName.Text & "','" & HeadType & "','" & Request.Cookies("UserID").Value & "','" & IsDefault & "')"
            FinalMessage = "New Head Information added successfully..."
        Else    'Update Command
            sqlStr = "Update SalaryHeadMaster Set HeadName='" & txtHeadName.Text & "',CreatedBy='" & Request.Cookies("UserID").Value & "',IsDefault=" & IsDefault & " Where HeadID=" & Val(txtID.Text)
            FinalMessage = "Head Information updated successfully..."
        End If

        
        
        ExecuteQuery_Update(SqlStr)

        
        

        InitControls()
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('" & FinalMessage & "');", True)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            InitControls()
        End If
    End Sub

    Private Sub InitControls()

        Dim HeadType As Integer = Request.QueryString("HeadType")

        If HeadType = 1 Then
            Literal1.Text = "Salary Earnings Master"
            LoadMasterInfo(23, lstHead)
        End If
        If HeadType = 2 Then
            Literal1.Text = "Salary Deductions Master"
            LoadMasterInfo(24, lstHead)
        End If

        txtID.Text = 0
        txtHeadName.Text = ""
        btnRemove.Visible = False
        txtHeadName.Focus()

    End Sub

    Protected Sub lstHead_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstHead.SelectedIndexChanged

        Dim HeadType As Integer = Request.QueryString("HeadType")

        If HeadType = 1 Then
            txtID.Text = FindMasterID(23, lstHead.Text)
        End If
        If HeadType = 2 Then
            txtID.Text = FindMasterID(24, lstHead.Text)
        End If

        txtHeadName.Text = lstHead.Text
       
       
       

        Dim sqlStr As String = "Select * From SalaryHeadMaster Where HeadID='" & txtID.Text & "'"
        
        
        
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
        
        

    End Sub

    Protected Sub btnRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        Dim sqlStr As String = ""
        Dim FinalMessage = ""

        If Val(txtID.Text) = 0 Then    'Error
            FinalMessage = "Select a Status to remove..."
        Else
            sqlStr = "Delete SalaryHeadMaster Where HeadID=" & Val(txtID.Text)
            FinalMessage = "Status removed successfully..."
        End If

       
        
       

        
        
        
        ExecuteQuery_Update(SqlStr)

        
        

        InitControls()
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('" & FinalMessage & "');", True)

    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        InitControls()
    End Sub
End Class