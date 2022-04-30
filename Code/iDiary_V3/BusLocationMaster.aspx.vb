Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary

Public Class LocationMaster
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
       
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then InitControls()
    End Sub

    Private Sub InitControls()
        txtName.Text = ""
        txtID.Text = ""

        LoadMasterInfo(69, cboConveyaceTypes)
        'cboConveyaceTypes.Text = FindDefault(69)
        'LoadMasterInfo(45, cboBus)
        'cboBus.Text = FindDefault(45)
        'Dim ConveyanceTypeID As Integer = FindMasterID(69, cboConveyaceTypes.Text)
        'LoadLocation(lstMasters, ConveyanceTypeID)
        lblStatus.Text = ""
        cboConveyaceTypes.Focus()
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If cboConveyaceTypes.Text = "" Then
            lblStatus.Text = "Please Select Conveyance Head!"
            cboConveyaceTypes.Focus()
            Exit Sub
        End If
        If txtName.Text.Length <= 0 Then
            lblStatus.Text = "Please Enter Location!"
            txtName.Focus()
            Exit Sub
        End If
        Dim ConveyanceTypeID As Integer = FindMasterID(69, cboConveyaceTypes.Text)
        'Dim BusID As Integer = FindMasterID(45, cboConveyaceTypes.Text)
        Dim sqlStr As String = ""

        If txtID.Text = "" Then
            'Insert
            sqlStr = "Insert into LocationMaster(locationName,ConveyanceTypeID) Values('" & txtName.Text & "','" & ConveyanceTypeID & "')"
        Else
            'Update
            sqlStr = "Update LocationMaster Set LocationName='" & txtName.Text & "',ConveyanceTypeID='" & ConveyanceTypeID & "' Where LocationID=" & Val(txtID.Text)
        End If
        ExecuteQuery_Update(sqlStr)
        txtName.Text = ""
        LoadLocation(lstMasters, ConveyanceTypeID)
        txtName.Focus()
        'InitControls()
    End Sub

    Protected Sub lstMasters_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstMasters.SelectedIndexChanged
       
        Dim sqlStr As String = "Select * From LocationMaster Where LocationName='" & lstMasters.Text & "'"
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            txtID.Text = myReader("LocationID")
            txtName.Text = myReader("LocationName")
            cboConveyaceTypes.Text = FindMasterName(69, myReader("ConveyanceTypeID"))
            'cboBus.Text = FindMasterName(45, myReader("BusID"))
        End While
        myReader.Close()
    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        InitControls()
    End Sub

    Protected Sub btnRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        Dim sqlStr As String = "Delete From LocationMaster Where LocationID=" & Val(txtID.Text)
        ExecuteQuery_Update(sqlStr)
        InitControls()
    End Sub

    Protected Sub cboConveyaceTypes_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboConveyaceTypes.SelectedIndexChanged
        Dim ConveyanceTypeID As Integer = FindMasterID(69, cboConveyaceTypes.Text)
        LoadLocation(lstMasters, ConveyanceTypeID)
    End Sub
    Public Shared Function LoadLocation(ByRef myLst As ListBox, ConveyanceTypeID As Integer) As Integer
        Dim sqlStr As String = ""
        sqlStr = "Select LocationName From LocationMaster where ConveyanceTypeID=" & ConveyanceTypeID
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        myLst.Items.Clear()
        While myReader.Read
            myLst.Items.Add(myReader(0))
        End While
        myReader.Close()

        Return 0

    End Function
End Class