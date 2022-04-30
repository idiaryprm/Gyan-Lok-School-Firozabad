Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary

Public Class CityMaster
    Inherits System.Web.UI.Page
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try
            If Request.Cookies("UType").Value.ToString.Contains("Student") Or Request.Cookies("UType").Value.ToString.Contains("Admin") Then
                'Allow
            Else
                Response.Redirect("/./AccessDenied.aspx", False)
            End If
        Catch ex As Exception
            Response.Redirect("~/Login.aspx")
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then InitControls()
    End Sub
    Private Sub InitControls()
        txtName.Text = ""
        txtID.Text = ""
        LoadMasterInfo(35, cbostate)
        cbostate.Text = FindDefault(35)
        Dim StateID As Integer = FindMasterID(35, cbostate.Text)
        LoadCity(StateID, lstMasters)
        lblStatus.Text = ""
        chkDefault.Checked = False
        cbostate.Focus()
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If txtName.Text.Length <= 0 Then
            lblStatus.Text = "Wrong Input!"
            txtName.Focus()
            Exit Sub
        End If
        If txtID.Text = "" Then
            If CheckDoubleEntry(70, txtName.Text) > 0 Then
                lblStatus.Text = "Same City allready Exist..."
                txtName.Focus()
                Exit Sub
            End If
        End If


        Dim StateID As Integer = FindMasterID(35, cbostate.Text)
        Dim sqlStr As String = ""

        Dim IsDefault As Integer = 0
        If chkDefault.Checked = True Then
            IsDefault = 1
            sqlStr = "Update CityMaster Set IsDefault=0"


            ExecuteQuery_Update(sqlStr)
        End If
        If txtID.Text = "" Then
            'Insert
            sqlStr = "Insert into CityMaster(CityName,StateID,IsDefault,IsDeleted) Values('" & txtName.Text & "'," & StateID & "," & IsDefault & ",0)"
        Else
            'Update
            sqlStr = "Update CityMaster Set CityName='" & txtName.Text & "', IsDefault=" & IsDefault & " Where CityID=" & Val(txtID.Text)
        End If


        ExecuteQuery_Update(sqlStr)



        InitControls()
    End Sub

    Protected Sub lstMasters_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstMasters.SelectedIndexChanged




        Dim sqlStr As String = "Select * From CityMaster Where CityName='" & lstMasters.Text & "' And IsDeleted=0"



        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            txtID.Text = myReader("CityID")
            txtName.Text = myReader("CityName")
            Try
                If myReader("IsDefault") = "1" Then
                    chkDefault.Checked = True
                Else
                    chkDefault.Checked = False
                End If
            Catch ex As Exception
                chkDefault.Checked = False
            End Try
        End While
        myReader.Close()


    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        InitControls()
    End Sub

    Protected Sub btnRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.Click




        Dim sqlStr As String = "update CityMaster set IsDeleted=1  Where CityID=" & Val(txtID.Text)



        ExecuteQuery_Update(sqlStr)


        InitControls()
    End Sub

    Protected Sub cbostate_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbostate.SelectedIndexChanged
        Dim StateID As Integer = FindMasterID(35, cbostate.Text)
        LoadCity(StateID, lstMasters)

    End Sub
End Class