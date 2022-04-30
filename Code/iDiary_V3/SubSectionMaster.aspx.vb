Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary

Public Class SubSectionMaster
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try
            If Request.Cookies("UType").Value.ToString.Contains("Student") Or Request.Cookies("UType").Value.ToString.Contains("Admin") Then
                'Allow
            Else
                Response.Redirect("/./AccessDenied.aspx", False)
            End If
        Catch ex As Exception
            Response.Redirect("~/Login.aspx")
        End Try
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then InitControls()
    End Sub

    Private Sub InitControls()

        txtID.Text = ""
        'LoadMasterInfo(2, cboClassName)
        lblStatus.Text = ""
        'lstMasters.Items.Clear()
        txtName.Text = ""
        txtDisplayOrder.Text = ""
        LoadMasterInfo(62, lstMasters)
        'LoadClassSection(lstMasters)
        txtName.Focus()
    End Sub

    'Private Sub LoadData()
    '   
    '   
    '   

    '    Dim sqlstr As String = "Select SecName From vw_Class_Section Where ClassName='" & cboClassName.Text & "' Order By SecName"
    '    
    '    
    '    
    '    Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
    '    lstMasters.Items.Clear()
    '    While myReader.Read
    '        lstMasters.Items.Add(myReader("SecName"))
    '    End While
    '    myReader.Close()
    '    
    '    
    'End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If txtName.Text.Length <= 0 Then
            lblStatus.Text = "Please Enter Sub-Section!"
            txtName.Focus()
            Exit Sub
        End If
        If txtID.Text = "" Then
            If CheckDoubleEntry(62, txtName.Text) > 0 Then
                lblStatus.Text = "Same Sub Section allready Exist..."
                txtName.Focus()
                Exit Sub
            End If
        End If
        If Trim(txtDisplayOrder.Text) = "" Or IsNumeric(txtDisplayOrder.Text) = False Then
            lblStatus.Text = "Invalid Display Order No..."
            txtDisplayOrder.Focus()
            Exit Sub
        End If

        'If IsNumeric(txtDisplayOrder.Text) = False Then
        '    lblStatus.Text = "Please Enter Order!"
        '    txtDisplayOrder.Focus()
        '    Exit Sub
        'End If

        'Dim ClassID As Integer = FindMasterID(2, cboClassName.Text)

        Dim sqlStr As String = ""
       
       
       

        

        If txtID.Text = "" Then

            'Insert Query
            sqlStr = "Insert into SubSecMaster Values('" & txtName.Text & "'," & txtDisplayOrder.Text & ")"
        Else
            'Update Query
            sqlStr = "Update SubSecMaster Set SubSecName='" & txtName.Text & "',DisplayOrder=" & txtDisplayOrder.Text & " Where SubSecID=" & Val(txtID.Text)
        End If
        
        
        ExecuteQuery_Update(SqlStr)

        
        

        'Dim TempCourseName As String = cboClassName.Text
        InitControls()
        'cboClassName.Text = TempCourseName

    End Sub
    
    Protected Sub lstMasters_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstMasters.SelectedIndexChanged

        Dim sqlStr As String = "Select * From SubSecMaster Where SubSecName='" & lstMasters.Text & "'"
       
       
       

        
        
        
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            txtID.Text = myReader("SubSecID")
            txtName.Text = myReader("SubSecName")
            txtDisplayOrder.Text = myReader("DisplayOrder")
        End While
        myReader.Close()
        
        

    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        InitControls()
    End Sub

    Protected Sub btnRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.Click
       
       
       

        Dim sqlStr As String = "Delete From SubSecMaster Where SubSecID=" & Val(txtID.Text)
        
        
        
        ExecuteQuery_Update(SqlStr)
        
        

        'Dim TempCourseName As String = cboClassName.Text
        InitControls()
        'cboClassName.Text = TempCourseName
        'LoadClassSection(lstMasters)
    End Sub

    'Protected Sub cboClassName_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboClassName.SelectedIndexChanged
    '    LoadClassSection(cboClassName.Text, lstMasters)
    '    cboClassName.Focus()
    'End Sub

End Class