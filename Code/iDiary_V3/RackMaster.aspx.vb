Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Public Class RackMaster
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
        ObjLib.LoadRackAsList(lstRacks)
        txtRackName.Text = ""
        Dim rv As Integer = ObjLib.GetNewRackID()
        txtRackID.Text = rv
        lblStatus.Text = ""
        txtRackName.Focus()
        ObjLib = Nothing
    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Dim ObjLib As New iDiary.clsLibrary
        txtRackName.Text = ""
        Dim rv As Integer = ObjLib.GetNewRackID()
        txtRackID.Text = rv
        objlib = Nothing
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim RackName As String = lstRacks.Text
        If Trim(txtRackName.Text) = "" Then
            lblStatus.Text = "Rack Name is Empty..."
            txtRackName.Focus()
            Exit Sub
        End If


        Dim sqlstr As String = ""

        Dim FinalMessage As String = ""
        If RackName = "" Then
            Try
                sqlstr = "Insert into Racks(RackName) Values('" & txtRackName.Text & "')"
                ExecuteQuery_Update(sqlstr)
                FinalMessage = "Rack: " & txtRackName.Text & " successfully added..."
            Catch ex As Exception
                
            End Try
        Else
            Try
                sqlstr = "Update Racks Set RackName='" & txtRackName.Text & "' Where RackID=" & Val(txtRackID.Text)
                ExecuteQuery_Update(sqlstr)
                FinalMessage = "Rack Name successfully updated..."
            Catch ex As Exception
              
            End Try
        End If

       




        InitControls()
        lblStatus.Text = FinalMessage
    End Sub

    Protected Sub lstPub_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstRacks.SelectedIndexChanged
        Dim ObjLib As New iDiary.clsLibrary
        txtRackName.Text = lstRacks.Text
        txtRackID.Text = ObjLib.GetRackID(lstRacks.Text)
        lblStatus.Text = ""
        ObjLib = Nothing
    End Sub

    Protected Sub btnRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        If Trim(txtRackName.Text) = "" Then
            lblStatus.Text = "Select a Rack Name to remove..."
            txtRackName.Focus()
            Exit Sub
        End If

       
       
       

        Dim sqlStr As String = ""
        

        sqlStr = "Delete From Racks Where RackID=" & Val(txtRackID.Text)
        
        
        Try
            ExecuteQuery_Update(SqlStr)

            
            

            Dim TempName As String = txtRackName.Text

            InitControls()

            lblStatus.Text = "Rack: " & TempName & " removed successfully..."
        Catch ex As Exception
            lblStatus.Text = "Unable to Remove The Selected Rack"
        End Try
        
    End Sub
End Class