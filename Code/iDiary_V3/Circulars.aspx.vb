Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Partial Class Admin_Circulars
    Inherits System.Web.UI.Page
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try
            If Request.Cookies("UType").Value.ToString.Contains("Admin") Then
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
        txtTitle.Text = ""
        myFile.Controls.Clear()
        lblStatus.Text = ""
        txtID.Text = ""
        txtTitle.Focus()
        btnRemove.Visible = False
    End Sub

    Private Sub UploadCircularFile()
        Dim fp1 As String = myFile.PostedFile.FileName
        If fp1.ToString() <> "" Then
            Dim fn1 As String = fp1.Substring(fp1.LastIndexOf("\\") + 1)
            Dim sp1 As String = ""
            sp1 = Server.MapPath("~/Circulars")
            If sp1.EndsWith("\\") = False Then
                sp1 += "\"
            End If

            sp1 += fp1
            myFile.PostedFile.SaveAs(sp1)
        End If

    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If txtTitle.Text.Length <= 0 Then
            lblStatus.Text = "Please provide proper title..."
            txtTitle.Focus()
            Exit Sub
        End If

        'If myFile.PostedFile.FileName.ToString.Length <= 0 Then
        '    lblStatus.Text = "Please select a file..."
        '    myFile.Focus()
        '    Exit Sub
        'End If

        SaveCircularToDB()

        InitControls()
        lblStatus.Text = "Circular uploaded successfully..."
        GridView1.DataBind()
    End Sub

    Private Sub SaveCircularToDB()
        UploadCircularFile()
        
        Dim sqlStr As String = ""
        If txtID.Text = "" Then
            sqlStr = "Insert into Circulars (Title, FileName, UploadDate, IsRead) Values (" & _
       "'" & txtTitle.Text & "'," & _
       "'" & myFile.PostedFile.FileName & "'," & _
       "'" & Now.Date.Month & "/" & Now.Date.Day & "/" & Now.Date.Year & "',0" & _
       ")"
        Else
            sqlStr = "Update Circulars Set Title='" & txtTitle.Text & "', FileName='" & myFile.PostedFile.FileName & _
                "', UploadDate='" & Now.Date.Month & "/" & Now.Date.Day & "/" & Now.Date.Year & "', IsRead=0 Where CircularID=" & txtID.Text
        End If
       
        ExecuteQuery_Update(sqlStr)
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged
        txtID.Text = GridView1.SelectedRow.Cells(1).Text
        txtTitle.Text = GridView1.SelectedRow.Cells(2).Text
        btnRemove.Visible = True
    End Sub

    Protected Sub btnRemove_Click(sender As Object, e As EventArgs) Handles btnRemove.Click
        Dim sqlStr As String = ""
        sqlStr = "Delete From Circulars Where CircularID='" & txtID.Text & "'"
        ExecuteQuery_Update(sqlStr)
        GridView1.DataBind()
        InitControls()
    End Sub
End Class
