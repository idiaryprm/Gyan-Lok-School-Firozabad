Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Imports iDiary_V3.iDiaryV3_Inventory

Public Class InventoryVendors
    Inherits System.Web.UI.Page
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
       
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("ActiveTab") = 12
        If IsPostBack = False Then
            InitControls()
        End If
    End Sub

    Private Sub InitControls()
        LoadInventoryInfo(3, lstVenders)
        txtVenderName.Text = ""
        lblStatus.Text = ""
        txtVenderName.Focus()
    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        txtVenderName.Text = ""
        txtVenderID.Text = ""
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Trim(txtVenderName.Text) = "" Then
            lblStatus.Text = "Vendor Name is Empty..."
            txtVenderName.Focus()
            Exit Sub
        End If
        Dim multipleRecord As Integer = CheckInventoryDuplicateEntry(3, txtVenderName.Text)
        If multipleRecord > 0 Then
            lblStatus.Text = "Vender already exist......."
            Exit Sub
        End If
        Dim myConnStr As String = System.Configuration.ConfigurationManager.ConnectionStrings("iDiaryConnectionString").ToString
        Dim myConn As New System.Data.SqlClient.SqlConnection(myConnStr)
        myConn.Open()

        Dim sqlstr As String = ""
        Dim myCommand As New SqlCommand
        Dim FinalMessage As String = ""
        If txtVenderID.Text = "" Then
            sqlstr = "Insert into VendorInventory Values('" & txtVenderName.Text & "')"
            myCommand.CommandText = sqlstr
            myCommand.Connection = myConn
            myCommand.ExecuteNonQuery()
            FinalMessage = "Vendor: " & txtVenderName.Text & " successfully added..."
        Else
            sqlstr = "Update VendorInventory Set VendorName='" & txtVenderName.Text & "' Where VendorID=" & Val(txtVenderID.Text)
            myCommand.CommandText = sqlstr
            myCommand.Connection = myConn
            myCommand.ExecuteNonQuery()
            FinalMessage = "Vendor Name successfully updated..."
        End If
        myCommand.Dispose()
        myConn.Close()
        InitControls()
        lblStatus.Text = FinalMessage
    End Sub


    Protected Sub lstPub_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstVenders.SelectedIndexChanged
        txtVenderName.Text = lstVenders.Text
        txtVenderID.Text = GetVendorID(lstVenders.Text)
        lblStatus.Text = ""
    End Sub

    Protected Sub btnRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        If Trim(txtVenderName.Text) = "" Then
            lblStatus.Text = "Select a Vendor to remove..."
            txtVenderName.Focus()
            Exit Sub
        End If

        Dim myConnStr As String = System.Configuration.ConfigurationManager.ConnectionStrings("iDiaryConnectionString").ToString
        Dim myConn As New System.Data.SqlClient.SqlConnection(myConnStr)
        myConn.Open()

        Dim sqlStr As String = ""
        Dim myCommand As New SqlCommand

        sqlStr = "Delete From VendorInventory Where VendorID=" & Val(txtVenderID.Text)
        myCommand.CommandText = sqlStr
        myCommand.Connection = myConn
        Try
            myCommand.ExecuteNonQuery()
            Dim TempName As String = txtVenderName.Text
            lblStatus.Text = "Vendor: " & TempName & " removed successfully..."
        Catch ex As Exception
            lblStatus.Text = "Unable to remove selected Vendor..."
        End Try
        myCommand.Dispose()
        myConn.Dispose()
        InitControls()
    End Sub

End Class