Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Public Class PublisherMaster
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
        ObjLib.LoadPublisherAsList(lstPubs)
        txtPubName.Text = ""
        Dim rv As Integer = ObjLib.GetNewPublisherID()
        txtPubID.Text = rv
        lblStatus.Text = ""
        txtPubName.Focus()
        ObjLib = Nothing
    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Dim ObjLib As New iDiary.clsLibrary
        txtPubName.Text = ""
        Dim rv As Integer = ObjLib.GetNewPublisherID()
        txtPubID.Text = rv
        ObjLib = Nothing
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Trim(txtPubName.Text) = "" Then
            lblStatus.Text = "Publisher Name is Empty..."
            txtPubName.Focus()
            Exit Sub
        End If

       
       
       

        Dim sqlstr As String = ""
        
        Dim FinalMessage As String = ""

        Try
            sqlstr = "Insert into Publishers Values(" & Val(txtPubID.Text) & ",'" & txtPubName.Text & "')"
            
            
            ExecuteQuery_Update(SqlStr)
            FinalMessage = "Publisher: " & txtPubName.Text & " successfully added..."
        Catch ex As Exception
            If ex.Message.Contains("duplicate") Then
                sqlstr = "Update Publishers Set PubName='" & txtPubName.Text & "' Where PubID=" & Val(txtPubID.Text)
                
                
                ExecuteQuery_Update(SqlStr)
                FinalMessage = "Publisher Name successfully updated..."
            End If
        End Try

        
        

        InitControls()
        lblStatus.Text = FinalMessage
    End Sub


    Protected Sub lstPub_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstPubs.SelectedIndexChanged
        Dim ObjLib As New iDiary.clsLibrary
        txtPubName.Text = lstPubs.Text
        txtPubID.Text = ObjLib.GetPubID(lstPubs.Text)
        lblStatus.Text = ""
        ObjLib = Nothing
    End Sub

    Protected Sub btnRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        If Trim(txtPubName.Text) = "" Then
            lblStatus.Text = "Select a Publisher to remove..."
            txtPubName.Focus()
            Exit Sub
        End If

       
       
       

        Dim sqlStr As String = ""
        

        sqlStr = "Delete From Publishers Where PubID=" & Val(txtPubID.Text)
        
        
        Try
            ExecuteQuery_Update(SqlStr)

            
            

            Dim TempName As String = txtPubName.Text

            InitControls()

            lblStatus.Text = "Publisher: " & TempName & " removed successfully..."
        Catch ex As Exception
            lblStatus.Text = "Unable to remove selected publisher..."
        End Try

    End Sub

End Class