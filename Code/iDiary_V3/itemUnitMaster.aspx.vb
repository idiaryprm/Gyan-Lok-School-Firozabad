Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Imports iDiary_V3.iDiaryV3_Inventory

Public Class itemUnitMaster
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
        LoadInventoryInfo(2, lstMaster)
        txtName.Text = ""
        lblStatus.Text = ""
        txtName.Focus()
    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        txtName.Text = ""
        txtID.Text = ""
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Trim(txtName.Text) = "" Then
            lblStatus.Text = "Item Unit Name is Empty..."
            txtName.Focus()
            Exit Sub
        End If
        Dim multipleRecord As Integer = CheckInventoryDuplicateEntry(2, txtName.Text)
        If multipleRecord > 0 Then
            lblStatus.Text = "item unit already exist......."
            Exit Sub
        End If
        Dim myConnStr As String = System.Configuration.ConfigurationManager.ConnectionStrings("iDiaryConnectionString").ToString
        Dim myConn As New System.Data.SqlClient.SqlConnection(myConnStr)
        myConn.Open()

        Dim sqlstr As String = ""
        Dim myCommand As New SqlCommand
        Dim FinalMessage As String = ""
        If txtID.Text = "" Then
            sqlstr = "Insert into itemUnitMaster Values('" & txtName.Text & "')"
            myCommand.CommandText = sqlstr
            myCommand.Connection = myConn
            myCommand.ExecuteNonQuery()
            FinalMessage = "Item Unit : " & txtName.Text & " successfully added..."
        Else
            sqlstr = "Update itemUnitMaster Set unitName='" & txtName.Text & "' Where iuID=" & Val(txtID.Text)
            myCommand.CommandText = sqlstr
            myCommand.Connection = myConn
            myCommand.ExecuteNonQuery()
            FinalMessage = "Item Unit successfully updated..."
        End If
        myCommand.Dispose()
        myConn.Close()
        InitControls()
        lblStatus.Text = FinalMessage
    End Sub


    Protected Sub lstPub_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstMaster.SelectedIndexChanged
        txtName.Text = lstMaster.Text
        txtID.Text = GetItemUnitID(lstMaster.Text)
        lblStatus.Text = ""
    End Sub

    Protected Sub btnRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        If Trim(txtName.Text) = "" Then
            lblStatus.Text = "Select a Unit to remove..."
            txtName.Focus()
            Exit Sub
        End If

        Dim myConnStr As String = System.Configuration.ConfigurationManager.ConnectionStrings("iDiaryConnectionString").ToString
        Dim myConn As New System.Data.SqlClient.SqlConnection(myConnStr)
        myConn.Open()

        Dim sqlStr As String = ""
        Dim myCommand As New SqlCommand

        sqlStr = "Delete From itemUnitMaster Where iuID=" & Val(txtID.Text)
        myCommand.CommandText = sqlStr
        myCommand.Connection = myConn
        Try
            myCommand.ExecuteNonQuery()
            Dim TempName As String = txtName.Text
            lblStatus.Text = "Item Unit : " & TempName & " removed successfully..."
        Catch ex As Exception
            lblStatus.Text = "Unable to remove selected Unit..."
        End Try
        myCommand.Dispose()
        myConn.Dispose()
        InitControls()
    End Sub

End Class