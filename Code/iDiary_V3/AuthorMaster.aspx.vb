Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary



Public Class AuthorMaster
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
        ObjLib.LoadAuthorListAsList(lstAuthor)
        txtAuthorName.Text = ""
        Dim rv As Integer = ObjLib.GetNewAuthorID()
        txtAuthorID.Text = rv
        lblStatus.Text = ""
        txtAuthorName.Focus()
        ObjLib = Nothing
    End Sub


    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Dim ObjLib As New iDiary.clsLibrary
        txtAuthorName.Text = ""
        Dim rv As Integer = ObjLib.GetNewAuthorID()
        txtAuthorID.Text = rv
        ObjLib = Nothing
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Trim(txtAuthorName.Text) = "" Then
            lblStatus.Text = "Author Name is Empty..."
            txtAuthorName.Focus()
            Exit Sub
        End If

       
       
       

        Dim sqlstr As String = ""
        
        Dim FinalMessage As String = ""

        Try
            sqlstr = "Insert into Authors Values(" & Val(txtAuthorID.Text) & ",'" & txtAuthorName.Text.Replace("'", "''") & "')"
            
            
            ExecuteQuery_Update(SqlStr)
            FinalMessage = "Author: " & txtAuthorName.Text & " successfully added..."
        Catch ex As Exception
            If ex.Message.Contains("duplicate") Then
                sqlstr = "Update Authors Set AuthorName='" & txtAuthorName.Text.Replace("'", "''") & "' Where AuthorID=" & Val(txtAuthorID.Text)
                
                
                ExecuteQuery_Update(SqlStr)
                FinalMessage = "Author Name successfully updated..."
            End If
        End Try

        
        

        InitControls()
        lblStatus.Text = FinalMessage
    End Sub

    Protected Sub lstAuthor_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstAuthor.SelectedIndexChanged
        Dim ObjLib As New iDiary.clsLibrary
        txtAuthorName.Text = lstAuthor.Text
        txtAuthorID.Text = ObjLib.GetAuthorID(lstAuthor.Text)
        lblStatus.Text = ""
        ObjLib = Nothing
    End Sub

    Protected Sub btnRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        If Trim(txtAuthorName.Text) = "" Then
            lblStatus.Text = "Select a Author to remove..."
            txtAuthorName.Focus()
            Exit Sub
        End If

       
       
       

        Dim sqlStr As String = ""
        

        sqlStr = "Delete From Authors Where AuthorID=" & Val(txtAuthorID.Text)
        
        
        Try
            ExecuteQuery_Update(SqlStr)


            
            

            Dim TempName As String = txtAuthorName.Text

            InitControls()

            lblStatus.Text = "Author: " & TempName & " removed successfully..."
        Catch ex As Exception
            lblStatus.Text = "This Author Cannot be Deleted..."
        End Try

    End Sub

End Class