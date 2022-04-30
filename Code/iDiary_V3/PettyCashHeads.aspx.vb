Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary

Public Class PettyCashHeads
    Inherits System.Web.UI.Page
    Dim isInventory As Integer = 0

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try
            If Request.Cookies("UType").Value.ToString.Contains("Petty Cash") Or Request.Cookies("UType").Value.ToString.Contains("Admin") Then
                'Allow
            Else
                Response.Redirect("/./AccessDenied.aspx", False)
            End If
        Catch ex As Exception
            response.redirect("~/Login.aspx")
        End Try
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("ActiveTab") = 8
        Response.Cookies("ActiveTab").Value = 8
        Response.Cookies("ActiveTab").Expires = DateTime.Now.AddHours(1)
        If IsPostBack = False Then InitControls()
        If Request.Cookies("UType").Value.ToString.Contains("Petty Cash-1") = False And Request.Cookies("UType").Value.ToString.Contains("Admin-1") = False Then
            btnSave.Enabled = False
            btnRemove.Enabled = False
        End If
    End Sub

    Private Sub InitControls()
        chbInventory.Checked = False
        LoadMasterInfo(17, cboTransType)
        LoadMasterInfo(70, cboClassGroup)
        cboTransType.SelectedIndex = 1
        LoadPettyCashHeads(cboTransType.Text, lstMasters)
        txtName.Text = ""
        txtID.Text = ""
        lblStatus.Text = ""
        txtOPBal.Text = ""
        txtName.Focus()
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        'If cboTransType.Text = "" Then
        '    lblStatus.Text = "Wrong Transaction Type!"
        '    cboTransType.Focus()
        '    Exit Sub
        'End If

        If Trim(txtName.Text) = "" Then
            lblStatus.Text = "Please Enter Petty Cash Head Name!"
            txtName.Focus()
            Exit Sub
        End If
        If IsNumeric(txtDisplayOrder.Text) = False Then
            lblStatus.Text = "Please Enter Valid Dispaly Order!"
            txtDisplayOrder.Focus()
            Exit Sub
        End If
        If txtID.Text = "" Then
            If CheckHeadName() > 0 Then
                lblStatus.Text = "Ledger Allready Exists!!!"
                txtName.Focus()
                Exit Sub
            End If
        End If
        lblStatus.Text = ""
        Dim TransTypeID As Integer = FindMasterID(17, cboTransType.Text)
        Dim ClassGroupID As Integer = FindMasterID(70, cboClassGroup.Text)
       
       
       
        Dim sqlStr As String = ""
        

        If chbInventory.Checked = True Then
            isInventory = 1
        Else
            isInventory = 0
        End If
        If txtID.Text = "" Then
            'Insert

            sqlStr = "Insert into PettyCashHeadMaster(PCHeadName,TransTypeID,IsInventory,OPBal,OpBalDRCR,ClassGroupID,DisplayOrder) Values('" & txtName.Text & "'," & TransTypeID & "," & isInventory & "," & Val(txtOPBal.Text) & ",'" & cboDRCR.Text & "','" & ClassGroupID & "','" & Val(txtDisplayOrder.Text) & "')"
            lblStatus.Text = "Petty Cash head has been saved!!!"
        Else
            'Update
            sqlStr = "Update PettyCashHeadMaster Set PCHeadName='" & txtName.Text & "',IsInventory='" & isInventory & "',OPBal=" & Val(txtOPBal.Text) & ",OpBalDRCR='" & cboDRCR.Text & "',ClassGroupID=" & ClassGroupID & ",DisplayOrder='" & Val(txtDisplayOrder.Text) & "'  Where PCHeadID=" & Val(txtID.Text)
            lblStatus.Text = "Ledger has been updated!!!"
        End If
        
        
        ExecuteQuery_Update(SqlStr)

        
        

        RetainInput()
    End Sub

    Private Sub RetainInput()
        Dim TempTransName As String = cboTransType.Text
        'InitControls()
        'cboTransType.Text = TempTransName
        LoadPettyCashHeads(cboTransType.Text, lstMasters)
        cboClassGroup.SelectedIndex = 0
        txtName.Text = ""
        txtDisplayOrder.Text = "0"
        txtName.Focus()
    End Sub

    Protected Sub lstMasters_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstMasters.SelectedIndexChanged
       
       
       

        Dim sqlStr As String = "Select * From vwPettyCashHead Where PCHeadName='" & lstMasters.Text & "' AND TransTypeName='" & cboTransType.Text & "'"
        
        
        
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            txtID.Text = myReader("PCHeadID")
            txtName.Text = myReader("PCHeadName")
            Try
                isInventory = myReader("IsInventory")
            Catch ex As Exception

            End Try
            Try
                txtOPBal.Text = myReader("OPBal")
            Catch ex As Exception

            End Try
            Try
                cboDRCR.Text = myReader("OPBalDRCR")
            Catch ex As Exception

            End Try
            Try
                txtDisplayOrder.Text = myReader("DisplayOrder")
            Catch ex As Exception

            End Try
            Try
                cboClassGroup.Text = myReader("ClassGroupName")
            Catch ex As Exception
                cboClassGroup.SelectedIndex = 0
            End Try
        End While
        myReader.Close()
        
        
        If isInventory = 1 Then
            chbInventory.Checked = True
        Else
            chbInventory.Checked = False
        End If
    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        InitControls()
    End Sub
    Private Function CheckHeadName() As Integer
       
       
       
        Dim rv As Integer = 0
        Dim sqlStr As String = "Select Count(*) From PettyCashHeadMaster Where PCHeadName='" & txtName.Text & "'"
        
        
        
        rv = ExecuteQuery_ExecuteScalar(SqlStr)
        
        
        Return rv
    End Function
    Protected Sub btnRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.Click
       
       
       

        Dim sqlStr As String = "Delete From PettyCashHeadMaster Where PCHeadID=" & Val(txtID.Text)
        
        
        
        ExecuteQuery_Update(SqlStr)
        
        
        RetainInput()
    End Sub


    Protected Sub cboTransType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboTransType.SelectedIndexChanged
        LoadPettyCashHeads(cboTransType.Text, lstMasters)
        cboTransType.Focus()
    End Sub
End Class