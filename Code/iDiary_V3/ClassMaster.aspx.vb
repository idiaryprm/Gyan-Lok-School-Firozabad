'..................................updated by vikash gupta on 27/06/2016................................

Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary

Public Class ClassMaster
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
        Session("ActiveTab") = 2
        Response.Cookies("ActiveTab").Value = 2
        If IsPostBack = False Then InitControls()
    End Sub

    Private Sub InitControls()
        LoadExamGroups()
        LoadMasterInfo(2, lstClasses)
        txtClassID.Text = ""
        txtClassName.Text = ""
        txtOrderNo.Text = ""
        txtNextClass.Text = ""
        lblStatus.Text = ""
        txtClassName.Focus()
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Trim(txtClassName.Text) = "" Then
            lblStatus.Text = "Class Name is Empty..."
            txtClassName.Focus()
            Exit Sub
        End If
        If txtClassID.Text = "" Then
            If CheckDoubleEntry(2, txtClassName.Text) > 0 Then
                lblStatus.Text = "Same Class allready Exist..."
                txtClassName.Focus()
                Exit Sub
            End If
        End If
        If Trim(txtOrderNo.Text) = "" Or IsNumeric(txtOrderNo.Text) = False Then
            lblStatus.Text = "Invalid Display Order No..."
            txtOrderNo.Focus()
            Exit Sub
        End If

        If txtNextClass.Text = "" Then
            lblStatus.Text = "Provide Next class to which student will be promoted..."
            txtNextClass.Focus()
            Exit Sub
        End If

        Dim sqlstr As String = ""


        If txtClassID.Text = "" Then
            sqlstr = "Insert into Classes(ClassName,DisplayOrder,NextClass) Values(" & "'" & txtClassName.Text & "'," & Val(txtOrderNo.Text) & ",'" & txtNextClass.Text & "')"
        Else
            sqlstr = "Update Classes Set ClassName='" & txtClassName.Text & "', DisplayOrder=" & Val(txtOrderNo.Text) & ", NextClass='" & txtNextClass.Text & "' Where ClassID=" & Val(txtClassID.Text)
        End If
        ExecuteQuery_Update(sqlStr)

        Dim TempClass As String = txtClassName.Text
        InitControls()
        lblStatus.Text = "Class: " & TempClass & " successfully added / updated..."
    End Sub

    Protected Sub lstClasses_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstClasses.SelectedIndexChanged
        LoadClassInformation()
    End Sub

    Protected Sub btnRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        If Trim(txtClassID.Text) = "" Then
            lblStatus.Text = "Select a Class Name to remove..."
            txtClassName.Focus()
            Exit Sub
        End If

        Dim sqlStr As String = ""

        sqlStr = "Delete From Classes Where ClassID=" & Val(txtClassID.Text)
        ExecuteQuery_Update(sqlStr)

        Dim TempClass As String = txtClassName.Text
        InitControls()
        lblStatus.Text = "Class: " & TempClass & " removed successfully..."
    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        InitControls()
    End Sub

    Protected Sub btnUp_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnUp.Click
        If lstClasses.SelectedIndex > 0 Then
            Dim myIndex As Integer = lstClasses.SelectedIndex
            Dim TempStr As String = lstClasses.Items(myIndex - 1).Text
            lstClasses.Items(myIndex - 1).Text = lstClasses.Items(myIndex).Text
            lstClasses.Items(myIndex).Text = TempStr
            lstClasses.Items(myIndex).Selected = False
            lstClasses.Items(myIndex - 1).Selected = True

            UpdateClassDisplayOrder(lstClasses)
            RetainInfoAfterDisplayOrderUpdate()

        End If
    End Sub

    Protected Sub btnDown_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnDown.Click
        If lstClasses.SelectedIndex < lstClasses.Items.Count - 1 Then
            Dim myIndex As Integer = lstClasses.SelectedIndex
            Dim TempStr As String = lstClasses.Items(myIndex + 1).Text
            lstClasses.Items(myIndex + 1).Text = lstClasses.Items(myIndex).Text
            lstClasses.Items(myIndex).Text = TempStr
            lstClasses.Items(myIndex).Selected = False
            lstClasses.Items(myIndex + 1).Selected = True

            UpdateClassDisplayOrder(lstClasses)
            RetainInfoAfterDisplayOrderUpdate()

        End If
    End Sub

    Private Sub RetainInfoAfterDisplayOrderUpdate()
        Dim TempClass As String = lstClasses.Items(lstClasses.SelectedIndex).Text
        InitControls()
        lstClasses.Text = TempClass
        LoadClassInformation()
    End Sub

    Private Sub LoadClassInformation()
        Dim NextClassID As Integer = 0

        Dim sqlstr As String = "Select * From Classes Where ClassName='" & lstClasses.Text & "'"
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)

        While myReader.Read
            txtClassID.Text = myReader("ClassID")
            txtClassName.Text = myReader("ClassName")
            txtOrderNo.Text = myReader("DisplayOrder")
            txtNextClass.Text = myReader("NextClass")
            Try
                cboExamGroup.Text = myReader("ExamGroupID")
            Catch ex As Exception

            End Try
        End While
        myReader.Close()

        '
        '

        lblStatus.Text = ""

    End Sub

    Private Sub LoadExamGroups()
       
       
       

        Dim sqlstr As String = "Select * From ExamGroups"

        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        cboExamGroup.Items.Clear()
        cboExamGroup.Items.Add("")
        While myReader.Read
            cboExamGroup.Items.Add(New ListItem(myReader("ExamGroupName"), myReader("ExamGroupID")))
        End While
        myReader.Close()
        lblStatus.Text = ""

    End Sub
    Private Function CheckDoubleEntry(p1 As Integer, p2 As String) As Integer
        Dim sqlStr As String = "Select Count(*) From Classes where ClassName='" & p2 & "'"
        Dim rv As Integer = ExecuteQuery_ExecuteScalar(sqlStr)
        Return rv
    End Function

End Class