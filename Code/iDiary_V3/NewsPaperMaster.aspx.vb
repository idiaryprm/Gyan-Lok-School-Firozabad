Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Public Class NewsPaperMaster
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Request.Cookies("UType").Value.ToString.Contains("Library") Or Request.Cookies("UType").Value.ToString.Contains("Admin") Then
            'Allow
        Else
            Response.Redirect("../AccessDenied.aspx", False)
        End If
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            InitControls()
        End If
        If Request.Cookies("UType").Value.ToString.Contains("Library-1") = False And Request.Cookies("UType").Value.ToString.Contains("Admin-1") = False Then
            btnSave.Enabled = False
            btnRemove.Enabled = False
        End If
    End Sub

    Private Sub InitControls()
        Dim ObjLib As New iDiary.clsLibrary
        ObjLib.LoadNewsPaperAsList(lstNewsPapers)
        ObjLib.LoadFrequencyAsDropDown(cboFrequency)
        txtNewsPaperName.Text = ""
        'Dim rv As Integer = ObjLib.GetNewRackID()
        'txtRackID.Text = rv
        lblStatus.Text = ""
        txtNewsPaperName.Focus()
        ObjLib = Nothing
    End Sub

    'Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
    '    Dim ObjLib As New clsLibrary
    '    txtNewsPaperName.Text = ""
    '    Dim rv As Integer = ObjLib.GetNewRackID()
    '    txtRackID.Text = rv
    '    objlib = Nothing
    'End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Trim(txtNewsPaperName.Text) = "" Then
            lblStatus.Text = "News Paper Name is Empty..."
            txtNewsPaperName.Focus()
            Exit Sub
        End If
        If Trim(cboFrequency.Text) = "" Then
            lblStatus.Text = "News Paper frequency is Empty..."
            cboFrequency.Focus()
            Exit Sub
        End If

        Dim sqlstr As String = ""
        
        Dim FinalMessage As String = ""
        Dim FrequencyID As Integer = 0
        Dim ObjLib As New iDiary.clsLibrary
        FrequencyID = ObjLib.GetFrequencyID(cboFrequency.Text)
        If txtNewsPaperID.Text = "" Then
            sqlstr = "Insert into NewsPaperMaster Values('" & txtNewsPaperName.Text & "'," & FrequencyID & ")"
            
            
            ExecuteQuery_Update(SqlStr)
            FinalMessage = "News Paper: " & txtNewsPaperName.Text & " successfully added..."
        Else
            sqlstr = "Update NewsPaperMaster Set NewsPaperName='" & txtNewsPaperName.Text & "',Frequency=" & FrequencyID & " Where NewsPaperID=" & Val(txtNewsPaperID.Text)
            
            
            ExecuteQuery_Update(SqlStr)
            FinalMessage = "News Paper successfully updated..."
        End If

        'Catch ex As Exception
        '    If ex.Message.Contains("duplicate") Then

        '    End If
        'End Try

        
        

        InitControls()
        lblStatus.Text = FinalMessage
    End Sub

    Protected Sub lstPub_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstNewsPapers.SelectedIndexChanged
        Dim ObjLib As New iDiary.clsLibrary
        txtNewsPaperName.Text = lstNewsPapers.Text
        txtNewsPaperID.Text = ObjLib.GetNewsPaperID(lstNewsPapers.Text)
        cboFrequency.Text = ObjLib.GetNewsPaperFrequency(lstNewsPapers.Text)
        lblStatus.Text = ""
        ObjLib = Nothing
    End Sub

    Protected Sub btnRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        If Trim(txtNewsPaperName.Text) = "" Then
            lblStatus.Text = "Select a News Paper Name to remove..."
            txtNewsPaperName.Focus()
            Exit Sub
        End If

       
       
       

        Dim sqlStr As String = ""
        

        sqlStr = "Delete From NewsPaperMaster Where NewsPaperID=" & Val(txtNewsPaperID.Text)
        
        
        ExecuteQuery_Update(SqlStr)

        
        

        Dim TempName As String = txtNewsPaperName.Text

        InitControls()

        lblStatus.Text = "News Paper: " & TempName & " removed successfully..."
    End Sub

    Protected Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        InitControls()
        txtNewsPaperID.Text = ""
    End Sub
End Class