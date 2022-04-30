Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Imports iDiary_V3.iDiaryV3_Inventory

Public Class itemMaster
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try
            If Request.Cookies("UType").Value.ToString.Contains("Inventory") Or Request.Cookies("UType").Value.ToString.Contains("Admin") Then
                'Allow
            Else
                Response.Redirect("/./AccessDenied.aspx", False)
            End If
        Catch ex As Exception
            Response.Redirect("~/Login.aspx")
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("ActiveTab") = 12
        If IsPostBack = False Then
            InitControls()
        End If
    End Sub

    Private Sub InitControls()
        LoadInventoryInfo(1, lstMaster)
        LoadInventoryInfo(1, cboUnit)
        txtName.Text = ""
        txtopeningStock.Text = ""
        txtSpecs.Text = ""
        lblStatus.Text = ""
        txtName.Focus()
    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        txtName.Text = ""
        txtopeningStock.Text = ""
        txtSpecs.Text = ""
        lblStatus.Text = ""
        txtID.Text = ""
        txtName.Focus()

    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Trim(txtName.Text) = "" Then
            lblStatus.Text = "Item Name is Empty..."
            txtName.Focus()
            Exit Sub
        End If
        If Trim(cboUnit.Text) = "" Then
            lblStatus.Text = "Please select Item Unit to continue..."
            txtName.Focus()
            Exit Sub
        End If
        Dim multipleRecord As Integer = CheckInventoryDuplicateEntry(1, txtName.Text)
        If multipleRecord > 0 Then
            lblStatus.Text = "item already exist......."
            Exit Sub
        End If
        Dim myConnStr As String = System.Configuration.ConfigurationManager.ConnectionStrings("iDiaryConnectionString").ToString
        Dim myConn As New System.Data.SqlClient.SqlConnection(myConnStr)
        myConn.Open()

        Dim sqlstr As String = ""
        Dim myCommand As New SqlCommand
        Dim FinalMessage As String = ""
        Dim unitID = GetItemUnitID(cboUnit.Text)
        If txtID.Text = "" Then
            sqlstr = "Insert into itemMaster(itemName,itemSpecs,unitID,itemType,openStock) "
            sqlstr &= "  Values('" & txtName.Text & "','" & txtSpecs.Text & "','" & unitID & "','" & cboitemType.Text & "','" & txtopeningStock.Text & "')"
            myCommand.CommandText = sqlstr
            myCommand.Connection = myConn
            myCommand.ExecuteNonQuery()
            FinalMessage = "Item : " & txtName.Text & " successfully added..."
        Else
            sqlstr = "Update itemMaster Set itemName='" & txtName.Text & "',itemSpecs='" & txtSpecs.Text & "',unitID='" & unitID & "',itemType='" & cboitemType.Text & "',openStock='" & txtopeningStock.Text & "'  Where itemID=" & Val(txtID.Text)
            myCommand.CommandText = sqlstr
            myCommand.Connection = myConn
            myCommand.ExecuteNonQuery()
            FinalMessage = "Item successfully updated..."
        End If
        myCommand.Dispose()
        myConn.Close()

        InitControls()
        lblStatus.Text = FinalMessage
    End Sub


    Protected Sub lstPub_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstMaster.SelectedIndexChanged
        txtName.Text = lstMaster.Text
        txtID.Text = GetItemID(lstMaster.Text)
        Dim sqlStr As String = ""
        sqlStr = "select * from itemMaster where itemID=" & Val(txtID.Text)
        Dim myreader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myreader.Read
            txtSpecs.Text = myreader("itemSpecs")
            cboUnit.Text = GetItemUnitName(myreader("unitID"))
            cboitemType.Text = myreader("itemType")
        End While
        myreader.Close()
        lblStatus.Text = ""
    End Sub

    Protected Sub btnRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        If Trim(txtName.Text) = "" Then
            lblStatus.Text = "Select an Item to remove..."
            txtName.Focus()
            Exit Sub
        End If

        Dim myConnStr As String = System.Configuration.ConfigurationManager.ConnectionStrings("iDiaryConnectionString").ToString
        Dim myConn As New System.Data.SqlClient.SqlConnection(myConnStr)
        myConn.Open()

        Dim sqlStr As String = ""
        Dim myCommand As New SqlCommand

        sqlStr = "Delete From itemMaster Where itemID=" & Val(txtID.Text)
        myCommand.CommandText = sqlStr
        myCommand.Connection = myConn
        Try
            myCommand.ExecuteNonQuery()
            Dim TempName As String = txtName.Text
            lblStatus.Text = "Item : " & TempName & " removed successfully..."
        Catch ex As Exception
            lblStatus.Text = "Unable to remove selected Item..."
        End Try
        myCommand.Dispose()
        myConn.Dispose()
        InitControls()
    End Sub

End Class