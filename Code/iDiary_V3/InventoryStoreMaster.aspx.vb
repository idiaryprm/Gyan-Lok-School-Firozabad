Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary

Public Class InventoryStoreMaster
    Inherits System.Web.UI.Page
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Request.Cookies("UType").Value.ToString.Contains("Accounts") Or Request.Cookies("UType").Value.ToString.Contains("Admin") Then
            'Allow
        Else
            Response.Redirect("../AccessDenied.aspx", False)
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            InitControls()
        End If
        If Request.Cookies("UType").Value.ToString.Contains("Accounts-1") = False And Request.Cookies("UType").Value.ToString.Contains("Admin-1") = False Then
            btnSave.Enabled = False
            btnRemove.Enabled = False
        End If
    End Sub

    Private Sub InitControls()
        LoadMasterInfo(57, lstMaster)
        '    Dim ObjLib As New clsLibrary
        '  ObjLib.LoadVenderAsList(lstVenders)
        txtName.Text = ""
        'Dim rv As Integer = ObjLib.GetNewPublisherID()
        'txtVenderID.Text = rv
        lblStatus.Text = ""
        txtName.Focus()
        '   ObjLib = Nothing
    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click

        txtName.Text = ""

    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Trim(txtName.Text) = "" Then
            lblStatus.Text = "Store Name is Empty..."
            txtName.Focus()
            Exit Sub
        End If

       
       
       

        Dim sqlstr As String = ""
        
        Dim FinalMessage As String = ""

        Try
            sqlstr = "Insert into inventoryStoreMaster Values('" & txtName.Text & "')"
            
            
            ExecuteQuery_Update(SqlStr)
            FinalMessage = "Store : " & txtName.Text & " successfully added..."
        Catch ex As Exception
            If ex.Message.Contains("duplicate") Then
                sqlstr = "Update inventoryStoreMaster Set storeName='" & txtName.Text & "' Where storeID=" & Val(txtID.Text)
                
                
                ExecuteQuery_Update(SqlStr)
                FinalMessage = "Store successfully updated..."
            End If
        End Try

        
        

        InitControls()
        lblStatus.Text = FinalMessage
    End Sub


    Protected Sub lstPub_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstMaster.SelectedIndexChanged
        Dim ObjLib As New iDiary.clsLibrary
        txtName.Text = lstMaster.Text
        txtID.Text = FindMasterID(57, lstMaster.Text)
        lblStatus.Text = ""
        ObjLib = Nothing
    End Sub

    Protected Sub btnRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        If Trim(txtName.Text) = "" Then
            lblStatus.Text = "Select a Store to remove..."
            txtName.Focus()
            Exit Sub
        End If

       
       
       

        Dim sqlStr As String = ""
        

        sqlStr = "Delete From inventoryStoreMaster Where storeID=" & Val(txtID.Text)
        
        
        Try
            ExecuteQuery_Update(SqlStr)
        Catch ex As Exception
            lblStatus.Text = "Unable to remove selected Store..."
        End Try

        
        

        Dim TempName As String = txtName.Text

        InitControls()

        lblStatus.Text = "Store : " & TempName & " removed successfully..."
    End Sub

End Class