Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Public Class BookStatusMaster
    Inherits System.Web.UI.Page

    Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Response.Redirect("Index.aspx")
    End Sub

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
        ObjLib.LoadBookStatusAsList(lstBookStatus)
        txtBookStatusName.Text = ""
        Dim rv As Integer = ObjLib.GetNewBookStatusID()
        txtBookStatusID.Text = rv
        lblStatus.Text = ""
        ObjLib = Nothing
        txtBookStatusName.Focus()
    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Dim ObjLib As New iDiary.clsLibrary
        txtBookStatusName.Text = ""
        Dim rv As Integer = ObjLib.GetNewBookStatusID()
        txtBookStatusID.Text = rv
        ObjLib = Nothing
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Trim(txtBookStatusName.Text) = "" Then
            lblStatus.Text = "Book Status Name is Empty..."
            txtBookStatusName.Focus()
            Exit Sub
        End If

       
       
       

        Dim sqlstr As String = ""
        
        Dim FinalMessage As String = ""

        Try
            sqlstr = "Insert into BookStatus Values(" & Val(txtBookStatusID.Text) & ",'" & txtBookStatusName.Text & "')"
            
            
            ExecuteQuery_Update(SqlStr)
            FinalMessage = "Book Status: " & txtBookStatusName.Text & " successfully added..."
        Catch ex As Exception
            If ex.Message.Contains("duplicate") Then
                sqlstr = "Update BookStatus Set BookStatusName='" & txtBookStatusName.Text & "' Where BookStatusID=" & Val(txtBookStatusID.Text)
                
                
                ExecuteQuery_Update(SqlStr)
                FinalMessage = "Book Status Name successfully updated..."
            End If
        End Try

        
        

        InitControls()
        lblStatus.Text = FinalMessage
    End Sub


    Protected Sub lstBookStatus_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstBookStatus.SelectedIndexChanged
        Dim ObjLib As New iDiary.clsLibrary
        txtBookStatusName.Text = lstBookStatus.Text
        txtBookStatusID.Text = ObjLib.GetBookStatusID(lstBookStatus.Text)
        lblStatus.Text = ""
        ObjLib = Nothing
    End Sub

    Protected Sub btnRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        If Trim(txtBookStatusName.Text) = "" Then
            lblStatus.Text = "Select a Book Status to remove..."
            txtBookStatusName.Focus()
            Exit Sub
        End If

       
       
       

        Dim sqlStr As String = ""
        

        sqlStr = "Delete From BookStatus Where BookStatusID=" & Val(txtBookStatusID.Text)
        
        
        ExecuteQuery_Update(SqlStr)

        
        

        Dim TempName As String = txtBookStatusName.Text

        InitControls()

        lblStatus.Text = "Book Status: " & TempName & " removed successfully..."
    End Sub

End Class