Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary

Public Class BookCategoryMaster
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Request.Cookies("UType").Value.ToString.Contains("Library") Or Request.Cookies("UType").Value.ToString.Contains("Admin") Then
            'Allow
        Else
            Response.Redirect("../AccessDenied.aspx", False)
        End If
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            InitControls()
        End If
        If Request.Cookies("UType").Value.ToString.Contains("Library-1") = False And Request.Cookies("UType").Value.ToString.Contains("Admin-1") = False Then
            btnSave.Enabled = False
            btnRemove.Enabled = False
        End If
    End Sub

    Private Sub InitControls()
        Dim ObjLib As New iDiary.clsLibrary
        ObjLib.LoadBookCatAsList(lstBookCat)
        txtBookCatName.Text = ""
        Dim rv As Integer = ObjLib.GetNewBookCatID()
        txtID.Text = rv
        lblStatus.Text = ""
        txtBookCatName.Focus()
        ObjLib = Nothing
    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Dim ObjLib As New iDiary.clsLibrary
        txtBookCatName.Text = ""
        Dim rv As Integer = ObjLib.GetNewBookCatID()
        txtID.Text = rv
        ObjLib = Nothing
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim BookCategory As String = lstBookCat.Text
        If Trim(txtBookCatName.Text) = "" Then
            lblStatus.Text = "Book Category Name is Empty..."
            txtBookCatName.Focus()
            Exit Sub
        End If

        Dim sqlstr As String = ""

        Dim FinalMessage As String = ""
        If BookCategory = "" Then
            Try
                sqlstr = "Insert into BookCategory(BookCatName) Values('" & txtBookCatName.Text & "')"
                ExecuteQuery_Update(sqlstr)
                FinalMessage = "Book Category: " & txtBookCatName.Text & " successfully added..."
            Catch ex As Exception
               
            End Try

        Else
            Try
                sqlstr = "Update BookCategory Set BookCatName='" & txtBookCatName.Text & "' Where BookCatID=" & Val(txtID.Text)
                ExecuteQuery_Update(sqlstr)
                FinalMessage = "Book Category Name successfully updated..."

            Catch ex As Exception

            End Try

        End If

        InitControls()
        lblStatus.Text = FinalMessage
    End Sub

    Protected Sub lstBookCat_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstBookCat.SelectedIndexChanged
        Dim ObjLib As New iDiary.clsLibrary
        txtBookCatName.Text = lstBookCat.Text
        txtID.Text = ObjLib.GetBookCatID(lstBookCat.Text)
        lblStatus.Text = ""
        ObjLib = Nothing
    End Sub

    Protected Sub btnRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        If Trim(txtBookCatName.Text) = "" Then
            lblStatus.Text = "Select a Book Category Name to remove..."
            txtBookCatName.Focus()
            Exit Sub
        End If

       
       
       

        Dim sqlStr As String = ""
        

        sqlStr = "Delete From BookCategory Where BookCatID=" & Val(txtID.Text)
        
        
        Try
            ExecuteQuery_Update(SqlStr)

            
            

            Dim TempName As String = txtBookCatName.Text

            InitControls()

            lblStatus.Text = "Book Category: " & TempName & " removed successfully..."
        Catch ex As Exception
            lblStatus.Text = "Unable to remove the selected Book Category"
        End Try
       
    End Sub

End Class