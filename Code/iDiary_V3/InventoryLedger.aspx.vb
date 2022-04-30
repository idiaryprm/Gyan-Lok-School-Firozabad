Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Imports iDiary_V3.iDiaryV3_Inventory

Public Class InventoryLedger
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
        LoadInventoryInfo(4, lstLedgers)
        txtLedgerName.Text = ""
        txtLedgerID.Text = ""
        lblStatus.Text = ""
        txtLedgerName.Focus()
    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        txtLedgerName.Text = ""
        txtLedgerID.Text = ""
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Trim(txtLedgerName.Text) = "" Then
            lblStatus.Text = "Ledger Name is Empty..."
            txtLedgerName.Focus()
            Exit Sub
        End If
        Dim multipleRecord As Integer = CheckInventoryDuplicateEntry(4, txtLedgerName.Text)
        If multipleRecord > 0 Then
            lblStatus.Text = "Ledger already exist......."
            Exit Sub
        End If
        Dim myConnStr As String = System.Configuration.ConfigurationManager.ConnectionStrings("iDiaryConnectionString").ToString
        Dim myConn As New System.Data.SqlClient.SqlConnection(myConnStr)
        myConn.Open()

        Dim sqlstr As String = ""
        Dim myCommand As New SqlCommand
        Dim FinalMessage As String = ""

        If txtLedgerID.Text = "" Then
            sqlstr = "Insert into InventoryLedger Values('" & txtLedgerName.Text & "')"
            FinalMessage = "Ledger: " & txtLedgerName.Text & " successfully Saved..."
        Else
            sqlstr = "Update InventoryLedger Set LedgerName='" & txtLedgerName.Text & "' Where LedgerID=" & Val(txtLedgerID.Text)
            FinalMessage = "Ledger: " & txtLedgerName.Text & " successfully Updated..."
        End If
        myCommand.CommandText = sqlstr
        myCommand.Connection = myConn
        myCommand.ExecuteNonQuery()

        myCommand.Dispose()
        myConn.Close()

        InitControls()
        lblStatus.Text = FinalMessage
    End Sub


    Protected Sub lstPub_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstLedgers.SelectedIndexChanged
        txtLedgerName.Text = lstLedgers.Text
        txtLedgerID.Text = GetLedgerID(lstLedgers.Text)
        lblStatus.Text = ""
    End Sub

    Protected Sub btnRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        If Trim(txtLedgerName.Text) = "" Then
            lblStatus.Text = "Select a Ledger to remove..."
            txtLedgerName.Focus()
            Exit Sub
        End If
        Dim myConnStr As String = System.Configuration.ConfigurationManager.ConnectionStrings("iDiaryConnectionString").ToString
        Dim myConn As New System.Data.SqlClient.SqlConnection(myConnStr)
        myConn.Open()
        Dim sqlStr As String = ""
        Dim myCommand As New SqlCommand
        sqlStr = "Delete From InventoryLedger Where LedgerID=" & Val(txtLedgerID.Text)
        myCommand.CommandText = sqlStr
        myCommand.Connection = myConn
        Try
            myCommand.ExecuteNonQuery()
            Dim TempName As String = txtLedgerName.Text
            lblStatus.Text = "Ledger: " & TempName & " removed successfully..."
        Catch ex As Exception
            lblStatus.Text = "Unable to remove selected Ledger..."
        End Try
        myCommand.Dispose()
        myConn.Dispose()
        InitControls()
    End Sub
    Private Function CheckDuplicate(LedgerName As String, LedgerID As String) As Integer
        Dim myConnStr As String = System.Configuration.ConfigurationManager.ConnectionStrings("iDiaryConnectionString").ToString
        Dim myConn As New System.Data.SqlClient.SqlConnection(myConnStr)
        myConn.Open()

        Dim sqlStr As String = ""
        Dim myCommand As New SqlCommand
        Dim rv As Integer = 0
        sqlStr = "Select Count(*) From InventoryLedger Where LedgerName='" & LedgerName & "'"
        If LedgerID <> "" Then
            sqlStr = " and LedgerID<>" & LedgerID
        End If
        myCommand.CommandText = sqlStr
        myCommand.Connection = myConn
        Try
            rv = myCommand.ExecuteScalar()
        Catch ex As Exception
            'lblStatus.Text = "Unable to remove selected Ledger..."
        End Try

        myCommand.Dispose()
        myConn.Dispose()
        Return rv
    End Function
End Class