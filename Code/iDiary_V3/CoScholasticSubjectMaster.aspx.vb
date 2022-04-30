Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary

Public Class CoScholasticSubjectMaster
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try
            If Request.Cookies("UType").Value.ToString.Contains("Student") Or Request.Cookies("UType").Value.ToString.Contains("Payroll") Or Request.Cookies("UType").Value.ToString.Contains("Admin") Then
                'Allow
            Else
                Response.Redirect("AccessDenied.aspx")
            End If
        Catch ex As Exception
            Response.Redirect("Login.aspx")
        End Try
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then InitControls()
    End Sub

    Private Sub InitControls()
        txtName.Text = ""
        txtID.Text = ""
        If Request.QueryString("type") = 1 Then
            lblName.Text = "Scholastic Area:B"
            LoadMasterInfo(49, lstMasters)
        ElseIf Request.QueryString("type") = 2 Then
            lblName.Text = "Co-Scholastic Area"
            LoadMasterInfo(50, lstMasters)
        ElseIf Request.QueryString("type") = 3 Then
            lblName.Text = "Co-Scholastic Activity"
            LoadMasterInfo(51, lstMasters)
        ElseIf Request.QueryString("type") = 4 Then
            lblName.Text = "Helth & Physical Education Activity"
            LoadMasterInfo(52, lstMasters)
        End If
        LoadGroups()
        txtName.Focus()
    End Sub
    Private Sub LoadGroups()

       
        
       

        

        Dim sqlStr As String = "Select ClassGroupID,ClassGroupName From ClassGroups"
        
        
        chkGroups.Items.Clear()
        ListPaperID.Items.Clear()

        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            ListPaperID.Items.Add(myReader("ClassGroupID"))
            chkGroups.Items.Add(myReader("ClassGroupName"))
        End While
        myReader.Close()

        
        

    End Sub
    Private Function IsCodeExists() As Boolean

       
        
       

        

        Dim sqlStr As String = "Select SubCode from SubjectCoScholasticMaster Where SubCode='" & txtCode.Text & "'"
        
        

        Dim Subcode As String = ""
        Try
            Subcode = ExecuteQuery_ExecuteScalar(SqlStr)
        Catch ex As Exception

        End Try

        
        
        If Subcode = "" Then
            Return False
        Else
            Return True
        End If
    End Function
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If txtName.Text.Length <= 0 Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Wrong Input');", True)
            txtName.Focus()
            Exit Sub
        End If
        If txtCode.Text.Length <= 0 Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Code is required');", True)
            txtName.Focus()
            Exit Sub
        End If
        If IsCodeExists() And txtID.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Code already Exists');", True)
            txtName.Focus()
            Exit Sub
        End If
        If Val(txtCode.Text) > 40 Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Code should be below 40');", True)
            txtName.Focus()
            Exit Sub
        End If

       
       
       
        Dim sqlStr As String = ""
        


        If txtID.Text = "" Then
            'Insert
            sqlStr = "Insert into SubjectCoScholasticMaster Values('" & txtName.Text & "', " & Request.QueryString("type") & ",'" & txtCode.Text & "')"
            
            
            ExecuteQuery_Update(SqlStr)
            '   insertSyncLog(sqlStr, "I", Request.Cookies("UserID").Value)
        Else
            'Update
            sqlStr = "delete from SubjectCoScholasticGroupMapping Where CoscholaticSubjectID=" & Val(txtID.Text)
            
            
            ExecuteQuery_Update(SqlStr)
            '       insertSyncLog(sqlStr, "D", Request.Cookies("UserID").Value)
            sqlStr = "Update SubjectCoScholasticMaster Set SubCode='" & txtCode.Text & "', CoScholasticSubName='" & txtName.Text & "' Where CoScholasticSubID=" & Val(txtID.Text)
            
            
            ExecuteQuery_Update(SqlStr)
            '      insertSyncLog(sqlStr, "U", Request.Cookies("UserID").Value)

        End If

        Dim i As Integer = 0

        For i = 0 To chkGroups.Items.Count - 1

            Dim GroupID As Integer = Val(ListPaperID.Items(i).Text)
            'If Request.QueryString("type") = 1 Then
            txtID.Text = FindMasterID(49, txtName.Text)
            'ElseIf Request.QueryString("type") = 2 Then
            '    txtID.Text = FindMasterID(44, txtName.Text)
            'ElseIf Request.QueryString("type") = 3 Then
            '    txtID.Text = FindMasterID(45, txtName.Text)
            'End If
            If chkGroups.Items(i).Selected = True Then


                sqlStr = "Insert into SubjectCoScholasticGroupMapping Values (" & _
                Val(txtID.Text) & "," & GroupID & ")"

                
                
                ExecuteQuery_Update(SqlStr)
                '    insertSyncLog(sqlStr, "I", Request.Cookies("UserID").Value)
            End If

        Next
        
        
        InitControls()
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Record Saved Successfully...');", True)
    End Sub

    Protected Sub lstMasters_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstMasters.SelectedIndexChanged
        'If Request.QueryString("type") = 1 Then
        '    txtID.Text = FindMasterID(43, lstMasters.Text)
        'ElseIf Request.QueryString("type") = 2 Then
        '    txtID.Text = FindMasterID(44, lstMasters.Text)
        'ElseIf Request.QueryString("type") = 3 Then
        '    txtID.Text = FindMasterID(45, lstMasters.Text)
        'End If
        txtID.Text = FindMasterID(49, lstMasters.Text)
        txtName.Text = lstMasters.Text
        txtCode.Text = getSubCode()
        CheckGroups()
    End Sub
    Private Sub CheckGroups()
        For i = 0 To chkGroups.Items.Count - 1
            chkGroups.Items(i).Selected = False
        Next
        Dim sqlCon As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("iDiaryConnectionString").ConnectionString)
        Dim sqlCmd As New SqlCommand("SELECT ClassGroupName from vw_CoScholasticMapping where CoScholasticSubName ='" & txtName.Text & "'", sqlCon)
        Dim dataReader As SqlDataReader
        sqlCon.Open()
        dataReader = sqlCmd.ExecuteReader()
        While dataReader.Read
            For i = 0 To chkGroups.Items.Count - 1
                If chkGroups.Items(i).Text = dataReader(0) Then
                    chkGroups.Items(i).Selected = True
                End If
            Next
        End While
        dataReader.Close()
        sqlCon.Close()
    End Sub
    Private Function getSubCode()
        Dim sqlCon As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("iDiaryConnectionString").ConnectionString)
        Dim sqlCmd As New SqlCommand("SELECT SubCode from SubjectCoScholasticMaster where CoScholasticSubName ='" & txtName.Text & "'", sqlCon)
        '   Dim dataReader As SqlDataReader
        Dim subCode As String = ""
        sqlCon.Open()
        Try
            subCode = sqlCmd.ExecuteScalar
        Catch ex As Exception

        End Try
        'dataReader = sqlCmd.ExecuteReader()

        'dataReader.Close()
        sqlCon.Dispose()
        sqlCon.Close()
        Return subCode
    End Function
    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        InitControls()
    End Sub

    Protected Sub btnRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.Click
       
       
       

        Dim sqlStr As String = "Delete From SubjectCoScholasticMaster Where CoScholasticSubID=" & Val(txtID.Text)
        
        
        
        ExecuteQuery_Update(SqlStr)
        '     insertSyncLog(sqlStr, "D", Request.Cookies("UserID").Value)
        
        
        InitControls()
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Record Removed Successfully...');", True)
    End Sub

    Protected Sub chkAll_CheckedChanged(sender As Object, e As EventArgs) Handles chkAll.CheckedChanged
        Dim i As Integer = 0
        For i = 0 To chkGroups.Items.Count - 1
            If chkAll.Checked = True Then
                chkGroups.Items(i).Selected = True
            Else
                chkGroups.Items(i).Selected = False
            End If
        Next
    End Sub
End Class