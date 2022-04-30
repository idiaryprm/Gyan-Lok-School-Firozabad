Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary

Public Class DescriptiveIndicators
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
        LoadSubjectType()
        loadGrades()
        cboSubjectType.Focus()
    End Sub
    Private Sub LoadSubjectType()
        cboSubjectType.Items.Clear()
        cboSubjectType.Items.Add("")
        cboSubjectType.Items.Add("Scholastic:B")
        cboSubjectType.Items.Add("Co-Scholastic Area")
        cboSubjectType.Items.Add("Co-Scholastic Activity")
        cboSubjectType.Items.Add("Health & Physical Activity")
    End Sub
    Private Sub LoadAreaType()
        If cboSubjectType.Text = "Scholastic:B" Then
            LoadMasterInfo(49, cboArea)
        ElseIf cboSubjectType.Text = "Co-Scholastic Area" Then
            LoadMasterInfo(50, cboArea)
        ElseIf cboSubjectType.Text = "Co-Scholastic Activity" Then
            LoadMasterInfo(51, cboArea)
        ElseIf cboSubjectType.Text = "Health & Physical Activity" Then
            LoadMasterInfo(52, cboArea)
        End If
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If cboSubjectType.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Select Subject Type');", True)
            cboSubjectType.Focus()
            Exit Sub
        End If
        If cboArea.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Select Area/Activity');", True)
            cboArea.Focus()
            Exit Sub
        End If
        If txtName.Text.Length <= 0 Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please enter Descriptive Indicator');", True)
            txtName.Focus()
            Exit Sub
        End If
        Dim AreaId As Integer = FindMasterID(49, cboArea.Text)
       
       
       
        Dim sqlStr As String = ""
        

        If txtID.Text = "" Then
            'Insert
            sqlStr = "Insert into DescriptiveIndicators Values(" & AreaId & ", '" & txtName.Text & "','" & cboGrade.Text & "')"
            
            
            ExecuteQuery_Update(SqlStr)
            '   insertSyncLog(sqlStr, "I", Request.Cookies("UserID").Value)
        Else
            'Update
            sqlStr = "Update DescriptiveIndicators Set Grade='" & cboGrade.Text & "',  DescriptiveIndicator='" & txtName.Text & "', CoScholasticSubjectID=" & AreaId & "  Where DescIndicatorID=" & Val(txtID.Text)
            
            
            ExecuteQuery_Update(SqlStr)
            '    insertSyncLog(sqlStr, "U", Request.Cookies("UserID").Value)
        End If
        

        
        
        'InitControls()
        txtName.Text = ""
        txtID.Text = ""
        txtName.Focus()
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Record Saved Successfully...');", True)
        LoadDI()
    End Sub

    Protected Sub lstMasters_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstMasters.SelectedIndexChanged
        txtName.Text = lstMasters.Text
        '  cboGrade.Text = GetGrade()
        txtID.Text = FindDI_ID()
    End Sub
    Private Function GetGrade() As String
       
       
       

        Dim sqlStr As String = "Select Grade From DescriptiveIndicators Where DescriptiveIndicator='" & txtName.Text & "' AND CoScholasticSubjectID=" & FindMasterID(49, cboArea.Text)

        Dim rv As String = ""
        rv = ExecuteQuery_ExecuteScalar(SqlStr)
        
        
        Return rv
    End Function
    Private Function FindDI_ID() As Integer
       
       
       

        Dim sqlStr As String = "Select Max(DescIndicatorID) From DescriptiveIndicators Where DescriptiveIndicator='" & txtName.Text & "' AND CoScholasticSubjectID=" & FindMasterID(49, cboArea.Text)
        Dim rv As Integer = 0
        rv = ExecuteQuery_ExecuteScalar(SqlStr)
        
        
        Return rv
    End Function

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        'InitControls()
        loadGrades()
        txtName.Text = ""
    End Sub

    Protected Sub btnRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.Click
       
       
       

        Dim sqlStr As String = "Delete From DescriptiveIndicators Where DescIndicatorID=" & Val(txtID.Text)
        
        
        
        ExecuteQuery_Update(SqlStr)
        '    insertSyncLog(sqlStr, "D", Request.Cookies("UserID").Value)
        
        
        LoadDI()
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Record Removed Successfully...');", True)
    End Sub

    Protected Sub cboSubjectType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSubjectType.SelectedIndexChanged
        LoadAreaType()
    End Sub

    Protected Sub cboArea_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboArea.SelectedIndexChanged

    End Sub
    Private Sub loadGrades()
        cboGrade.Items.Clear()
        cboGrade.Items.Add("")
        cboGrade.Items.Add("A")
        cboGrade.Items.Add("B")
        cboGrade.Items.Add("C")
        cboGrade.Items.Add("D")
        cboGrade.Items.Add("E")
    End Sub
    Private Sub LoadDI()

        Dim sqlStr As String = ""

        sqlStr = "Select Grade,DescriptiveIndicator From DescriptiveIndicators where Grade='" & cboGrade.SelectedItem.Text & "' AND CoScholasticSubjectID=" & FindMasterID(49, cboArea.Text)
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        lstMasters.Items.Clear()
        While myReader.Read
            '  cboGrade.Text = myReader("Grade")
            lstMasters.Items.Add(myReader("DescriptiveIndicator"))
        End While
        myReader.Close()
        
        
    End Sub

    Protected Sub cboGrade_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboGrade.SelectedIndexChanged

        LoadDI()
    End Sub
End Class