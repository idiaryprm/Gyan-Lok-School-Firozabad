﻿Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary

Public Class BusRouteMaster
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try
            If Request.Cookies("UType").Value.ToString.Contains("Student") Or Request.Cookies("UType").Value.ToString.Contains("Admin") Then
                'Allow
            Else
                Response.Redirect("/./AccessDenied.aspx", False)
            End If
        Catch ex As Exception
            response.redirect("~/Login.aspx")
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then InitControls()
    End Sub

    Private Sub InitControls()
        txtName.Text = ""
        txtID.Text = ""
        LoadMasterInfo(39, lstMasters)
        lblStatus.Text = ""
        txtDetails.Text = ""
        txtName.Focus()
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If txtName.Text.Length <= 0 Then
            lblStatus.Text = "Wrong Input!"
            txtName.Focus()
            Exit Sub
        End If

       
       
       
        Dim sqlStr As String = ""
        

        If txtID.Text = "" Then
            'Insert
            sqlStr = "Insert into BusRouteMaster Values('" & txtName.Text & "','" & txtDetails.Text & "')"
        Else
            'Update
            sqlStr = "Update BusRouteMaster Set RouteName='" & txtName.Text & "', routeDetails='" & txtDetails.Text & "' Where RouteID=" & Val(txtID.Text)
        End If
        
        
        ExecuteQuery_Update(SqlStr)

        
        
        InitControls()
    End Sub

    Protected Sub lstMasters_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstMasters.SelectedIndexChanged
       
       
       

        Dim sqlStr As String = "Select * From BusRouteMaster Where RouteName='" & lstMasters.Text & "'"
        
        
        
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            txtID.Text = myReader("RouteID")
            txtName.Text = myReader("RouteName")
            txtDetails.Text = myReader("routeDetails")
        End While
        myReader.Close()
        
        
    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        InitControls()
    End Sub

    Protected Sub btnRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.Click
       
       
       

        Dim sqlStr As String = "Delete From BusRouteMaster Where RouteID=" & Val(txtID.Text)
        
        
        
        ExecuteQuery_Update(SqlStr)
        
        
        InitControls()
    End Sub
End Class