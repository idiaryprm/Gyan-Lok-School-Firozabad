Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Public Class VenderMaster
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
        ObjLib.LoadVenderAsList(lstVenders)
        txtVenderName.Text = ""
        'Dim rv As Integer = ObjLib.GetNewPublisherID()
        'txtVenderID.Text = rv
        lblStatus.Text = ""
        txtVenderName.Focus()
        ObjLib = Nothing
    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        ' Dim ObjLib As New iDiary.CLS_idiaryLibrary
        txtVenderName.Text = ""
        'Dim rv As Integer = ObjLib.GetNewPublisherID()
        'txtVenderID.Text = rv
        'ObjLib = Nothing
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim VenderName As String = lstVenders.Text
        If Trim(txtVenderName.Text) = "" Then
            lblStatus.Text = "Vender Name is Empty..."
            txtVenderName.Focus()
            Exit Sub
        End If





        Dim sqlstr As String = ""

        Dim FinalMessage As String = ""
        If VenderName = "" Then
            Try
                sqlstr = "Insert into VenderMaster(VenderName) Values('" & txtVenderName.Text & "')"
                ExecuteQuery_Update(sqlstr)
                FinalMessage = "Vender: " & txtVenderName.Text & " successfully added..."
            Catch ex As Exception
               
            End Try
        Else
            Try
                sqlstr = "Update VenderMaster Set VenderName='" & txtVenderName.Text & "' Where VenderID=" & Val(txtVenderID.Text)
                ExecuteQuery_Update(sqlstr)
                FinalMessage = "Publisher Name successfully updated..."
            Catch ex As Exception
               
            End Try
        End If

        




        InitControls()
        lblStatus.Text = FinalMessage
    End Sub


    Protected Sub lstPub_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstVenders.SelectedIndexChanged
        Dim ObjLib As New iDiary.clsLibrary
        txtVenderName.Text = lstVenders.Text
        txtVenderID.Text = ObjLib.GetVenderID(lstVenders.Text)
        lblStatus.Text = ""
        ObjLib = Nothing
    End Sub

    Protected Sub btnRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        If Trim(txtVenderName.Text) = "" Then
            lblStatus.Text = "Select a Vender to remove..."
            txtVenderName.Focus()
            Exit Sub
        End If

       
       
       

        Dim sqlStr As String = ""
        

        sqlStr = "Delete From VenderMaster Where VenderID=" & Val(txtVenderID.Text)
        
        
        Try
            ExecuteQuery_Update(SqlStr)

            
            

            Dim TempName As String = txtVenderName.Text

            InitControls()

            lblStatus.Text = "Vender: " & TempName & " removed successfully..."
        Catch ex As Exception
            lblStatus.Text = "Unable to remove selected Vender..."
        End Try

    End Sub

End Class