Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Imports iDiary_V3.iDiaryV3_Inventory

Public Class Inventory
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
        LoadInventoryInfo(1, cboUnit)
        Try
            txtStockID.Text = Request.QueryString("StockID")
            showStockDetails(txtStockID.Text)
        Catch ex As Exception
            txtStockID.Text = ""
        End Try
        If txtStockID.Text = "" Then
            txtStockDate.Text = Now.Date.ToString("dd/MM/yyyy")
            lblStock.Text = "Stock " & Request.QueryString("type").ToString
            If Request.QueryString("type").ToString = "OUT" Then
                lblUnitCost.Visible = False
                txtUnitCost.Visible = False
                lblLedger.Text = "Ledger"
                LoadInventoryInfo(2, cboLedger)
            Else
                lblLedger.Text = "Vendor"
                LoadInventoryInfo(3, cboLedger)
            End If
            LoadInventoryInfo(1, lstMaster)
            'LoadMasterInfo(56, cboUnit)
            '    Dim ObjLib As New clsLibrary
            '  ObjLib.LoadVenderAsList(lstVenders)
            txtName.Text = ""
            txtQty.Text = ""
            txtSpecs.Text = ""
            txtQty.Text = ""
            txtUnitCost.Text = ""
            txtRemarks.Text = ""
            'Dim rv As Integer = ObjLib.GetNewPublisherID()
            'txtVenderID.Text = rv
            lblStatus.Text = ""
        Else
            lstMaster.Enabled = False
            txtName.Enabled = False
            btnNew.Visible = False
        End If
        txtName.Focus()
        '   ObjLib = Nothing
    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        txtName.Text = ""
        txtQty.Text = ""
        txtSpecs.Text = ""
        lblStatus.Text = ""
        txtRemarks.Text = ""
        txtName.Focus()
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Trim(txtName.Text) = "" Then
            lblStatus.Text = "Item Name is Empty..."
            txtName.Focus()
            Exit Sub
        End If

        If Trim(txtSpecs.Text) = "" Then
            lblStatus.Text = "Item Specs is Empty..."
            txtSpecs.Focus()
            Exit Sub
        End If
        If lblLedger.Text <> "Ledger" Then
            If txtUnitCost.Text < 1 Then
                lblStatus.Text = "Item Unit is Invalid..."
                txtUnitCost.Focus()
                Exit Sub
            End If
        End If
        If txtQty.Text < 1 Then
            lblStatus.Text = "Item Quantity is Invalid..."
            txtQty.Focus()
            Exit Sub
        End If
        If cboLedger.Text = "" Then
            If lblLedger.Text = "Ledger" Then
                lblStatus.Text = "Please select Ledger to continue..."
            Else
                lblStatus.Text = "Please select Vendor to continue..."
            End If
            cboLedger.Focus()
            Exit Sub
        End If
        lblStatus.Text = ""
        'Dim StockDate As String = 
        Dim StockDate As Date = Now.Date
        Try
            StockDate = txtStockDate.Text.Substring(6, 4) & "/" & txtStockDate.Text.Substring(3, 2) & "/" & txtStockDate.Text.Substring(0, 2)
        Catch ex As Exception
            lblStatus.Text = "Please Enter Valid Date..."
            txtStockDate.Focus()
            Exit Sub
        End Try
        Dim myConnStr As String = System.Configuration.ConfigurationManager.ConnectionStrings("iDiaryConnectionString").ToString
        Dim myConn As New System.Data.SqlClient.SqlConnection(myConnStr)
        myConn.Open()

        Dim sqlstr As String = ""
        Dim myCommand As New SqlCommand
        Dim FinalMessage As String = ""
        Dim itemID = GetItemID(txtName.Text)
        Dim LedgerID As Integer = 0
        If lblLedger.Text = "Ledger" Then
            LedgerID = GetLedgerID(cboLedger.Text)
        Else
            LedgerID = GetVendorID(cboLedger.Text)
        End If
        Dim StockType As String = ""
        If txtStockID.Text = "" Then
            StockType = Request.QueryString("type").ToString
        End If

        Try
            If txtStockID.Text = "" Then
                If Request.QueryString("type").ToString = "OUT" Then
                    sqlstr = "Insert into Stock (stockDate,itemID,quantity,unitCost,stockType,LedgerID,Remarks,UserID)  Values('" & StockDate.ToString("yyyy/MM/dd") & "','" & itemID & "','" & Val(txtQty.Text) & "','" & Val(txtUnitCost.Text) & "','" & StockType & "','" & LedgerID & "','" & SQLFixup(txtRemarks.Text) & "','" & Session("UID") & "')"
                Else
                    sqlstr = "Insert into Stock (stockDate,itemID,quantity,unitCost,stockType,VendorID)  Values('" & StockDate.ToString("yyyy/MM/dd") & "','" & itemID & "','" & Val(txtQty.Text) & "','" & Val(txtUnitCost.Text) & "','" & StockType & "','" & LedgerID & "')"
                End If
            Else
                sqlstr = "Update Stock set stockDate='" & StockDate.ToString("yyyy/MM/dd") & "',UpdateDate='" & Now.Date.ToString("yyyy/MM/dd") & "',itemID='" & itemID & "',quantity='" & Val(txtQty.Text) & "',unitCost='" & Val(txtUnitCost.Text) & "',"
                If lblLedger.Text = "Ledger" Then
                    sqlstr += "LedgerID='" & LedgerID & "',"
                Else
                    sqlstr += "VendorID='" & LedgerID & "',"
                End If
                sqlstr += "Remarks='" & SQLFixup(txtRemarks.Text) & "',UpdateBy='" & Session("UID") & "' where StockID=" & txtStockID.Text
            End If
            'sqlstr = "Insert into Stock" & StockType & "(" & StockType & "Date,itemID,quantity,unitCost)  Values('" & txtStockDate.Text & "','" & itemID & "','" & txtQty.Text & "','" & txtUnitCost.Text & "')"
            
            myCommand.CommandText = sqlstr
            myCommand.Connection = myConn
            myCommand.ExecuteNonQuery()
            If txtStockID.Text <> "" Then
                FinalMessage = "Item Updated Successfully..."
            Else
                If StockType = "IN" Then
                    FinalMessage = "Item : " & txtName.Text & " successfully added to Inventory..."
                Else
                    FinalMessage = "Item : " & txtName.Text & " successfully removed from Inventory..."
                End If
            End If

        Catch ex As Exception

        End Try

        myCommand.Dispose()
        myConn.Close()
        If txtStockID.Text = "" Then
            InitControls()
        Else
            txtName.Text = ""
            txtQty.Text = ""
            txtSpecs.Text = ""
            lblStatus.Text = ""
            txtRemarks.Text = ""
            txtName.Focus()
        End If
        lblStatus.Text = FinalMessage
    End Sub

    Protected Sub lstPub_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstMaster.SelectedIndexChanged
        showItem(lstMaster.SelectedItem.Text)

    End Sub

    Public Sub showItem(item As String)
        Dim sqlStr As String = "Select top(1) itemName,itemSpecs,unitName,itemType from vw_itemMaster where itemName='" & item & "'"
        Dim myConnStr As String = System.Configuration.ConfigurationManager.ConnectionStrings("iDiaryConnectionString").ToString
        Dim myConn As New System.Data.SqlClient.SqlConnection(myConnStr)
        myConn.Open()

        Dim myCommand As New SqlCommand(sqlStr, myConn)
        Dim myReader As SqlDataReader = myCommand.ExecuteReader

        While myReader.Read
            txtName.Text = myReader("itemName")
            txtSpecs.Text = myReader("itemSpecs")
            cboUnit.Text = myReader("unitName")
            cboitemType.Text = myReader("itemType")
        End While
        myReader.Close()
        myCommand.Dispose()
        myConn.Dispose()
        lblStatus.Text = ""
    End Sub
    Public Sub showStockDetails(StockID As Integer)
        Dim sqlStr As String = "Select * from vw_Stock where StockID='" & StockID & "'"
        Dim myConnStr As String = System.Configuration.ConfigurationManager.ConnectionStrings("iDiaryConnectionString").ToString
        Dim myConn As New System.Data.SqlClient.SqlConnection(myConnStr)
        myConn.Open()

        Dim myCommand As New SqlCommand(sqlStr, myConn)
        Dim myReader As SqlDataReader = myCommand.ExecuteReader

        While myReader.Read
            txtName.Text = myReader("itemName")
            txtSpecs.Text = myReader("itemSpecs")
            Try
                cboUnit.Text = myReader("unitName")
            Catch ex As Exception

            End Try
            Try
                cboitemType.Text = myReader("itemType")
            Catch ex As Exception

            End Try
            Dim a As Date = myReader("StockDate")
            txtStockDate.Text = a.ToString("dd/MM/yyyy")
            txtQty.Text = myReader("quantity")
            If myReader("StockType") = "OUT" Then
                lblUnitCost.Visible = False
                txtUnitCost.Visible = False
                lblLedger.Text = "Ledger"
                LoadInventoryInfo(2, cboLedger)
                Try
                    cboLedger.Text = myReader("LedgerName")
                Catch ex As Exception

                End Try
            Else
                lblLedger.Text = "Vendor"
                LoadInventoryInfo(3, cboLedger)
                Try
                    cboLedger.Text = myReader("VendorName")
                Catch ex As Exception

                End Try
            End If
            lblStock.Text = "Stock " & myReader("StockType")
            txtUnitCost.Text = myReader("UnitCost")
            Try
                txtRemarks.Text = myReader("Remarks")
            Catch ex As Exception

            End Try
            btnRemove.Visible = True
        End While
        myReader.Close()
        myCommand.Dispose()
        myConn.Dispose()
        lblStatus.Text = ""
    End Sub
    Protected Sub btnRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        If Trim(txtStockID.Text) = "" Then
            lblStatus.Text = "Select an Item to remove..."
            txtName.Focus()
            Exit Sub
        End If

        Dim myConnStr As String = System.Configuration.ConfigurationManager.ConnectionStrings("iDiaryConnectionString").ToString
        Dim myConn As New System.Data.SqlClient.SqlConnection(myConnStr)
        myConn.Open()

        Dim sqlStr As String = ""
        Dim myCommand As New SqlCommand

        sqlStr = "Delete From stock Where stockID=" & Val(txtStockID.Text)
        myCommand.CommandText = sqlStr
        myCommand.Connection = myConn
        Try
            myCommand.ExecuteNonQuery()
        Catch ex As Exception
            lblStatus.Text = "Unable to remove selected Item..."
        End Try

        myCommand.Dispose()
        myConn.Dispose()

        Dim TempName As String = txtName.Text

        InitControls()

        lblStatus.Text = "Item : " & TempName & " removed successfully..."
        txtName.Text = ""
        txtStockID.Text = ""
    End Sub

End Class