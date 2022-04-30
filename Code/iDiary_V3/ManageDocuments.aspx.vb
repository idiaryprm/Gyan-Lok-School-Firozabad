Imports System.IO
Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary

Public Class ManageDocuments
    Inherits System.Web.UI.Page
    Dim a As Integer = 0
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try
            a = a + 1
            If Request.Cookies("UType").Value.ToString.Contains("e-Docs") Or Request.Cookies("UType").Value.ToString.Contains("Admin") Then
                'Allow
                a = a + 1

            Else
                a += 1
                Response.Redirect("/./AccessDenied.aspx", False)
                a += 1
            End If
        Catch ex As Exception
            a += 1
            Response.Redirect("~/Login.aspx")
            a += 1
        End Try
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then InitControls()
        If Request.Cookies("UType").Value.ToString.Contains("e-Docs-1") = False And Request.Cookies("UType").Value.ToString.Contains("Admin-1") = False Then
            btnSave.Enabled = False
            btnRemove.Enabled = False
        End If
    End Sub

    Private Sub InitControls()
        txtDocID.text = ""
        txtFileNo.Text = ""
        txtDD.Text = Today.Day
        txtMM.Text = Today.Month
        txtYY.Text = Today.Year
        txtSubject.Text = ""
        txtContent.Text = ""
        txtFileName.Text = ""
        lblStatus.Text = ""

        btnOpen.Enabled = False
        FileUpload1.Enabled = True

        txtFileNo.Focus()
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        If txtFileNo.Text = "" Then
            lblStatus.Text = "File Number is Blank..."
            txtFileNo.Focus()
            Exit Sub
        End If
        If txtSubject.Text = "" Then
            lblStatus.Text = "Subject is Blank..."
            txtSubject.Focus()
            Exit Sub
        End If

       
       
       

        Dim sqlStr As String = ""
        

        'Get New File Number
        Dim DocID As Integer = 0
        Dim myFileName As String = ""

        If txtDocID.Text = "" Then

            sqlStr = "Select Max(DocID) From Documents"
            
            
            Try
                DocID = ExecuteQuery_ExecuteScalar(SqlStr)
            Catch ex As Exception
                DocID = 0
            End Try
            DocID += 1

        Else

            DocID = txtDocID.Text

        End If

        If FileUpload1.FileName <> "" And txtFileName.Text <> "" Then
            myFileName = DocID & "_" & FileUpload1.FileName
            File.Delete(Server.MapPath("Documents") & "/" & txtFileName.Text)
        ElseIf FileUpload1.FileName = "" And txtFileName.Text <> "" Then
            myFileName = txtFileName.Text
        ElseIf FileUpload1.FileName <> "" And txtFileName.Text = "" Then
            myFileName = DocID & "_" & FileUpload1.FileName
        ElseIf FileUpload1.FileName = "" And txtFileName.Text = "" Then
            myFileName = ""
        End If

        If txtDocID.Text = "" Then

            sqlStr = "Insert into Documents Values(" & _
            DocID & "," & _
            "'" & txtFileNo.Text & "'," & _
            "'" & Val(txtMM.Text) & "/" & Val(txtDD.Text) & "/" & Val(txtYY.Text) & "'," & _
            "'" & txtSubject.Text & "'," & _
            "'" & txtContent.Text & "'," & _
            "'" & myFileName & "')"

        Else

            sqlStr = "Update Documents Set " & _
            "FileNo='" & txtFileNo.Text & "'," & _
            "FileDate='" & Val(txtMM.Text) & "/" & Val(txtDD.Text) & "/" & Val(txtYY.Text) & "'," & _
            "FileSubject='" & txtSubject.Text & "'," & _
            "FileContents='" & txtContent.Text & "'," & _
            "FilePath='" & myFileName & "' Where DocID=" & Val(txtDocID.Text)

        End If

        
        
        ExecuteQuery_Update(SqlStr)

        Dim fp1 As String = FileUpload1.PostedFile.FileName
        If fp1.ToString() <> "" Then
            Dim fn1 As String = fp1.Substring(fp1.LastIndexOf("\\") + 1)
            Dim sp1 As String = ""
            sp1 = Server.MapPath("Documents")
            If sp1.EndsWith("\\") = False Then
                sp1 += "\"
            End If

            sp1 &= DocID & "_" & FileUpload1.FileName
            FileUpload1.PostedFile.SaveAs(sp1)
        End If

        
        

        InitControls()

        lblStatus.Text = "File Details saved successfully..."
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSearch.Click

        Dim TempFileNo As String = txtFileNo.Text
        InitControls()
        txtFileNo.Text = TempFileNo

        Dim myCount As Integer = 0

       
       
       

        Dim sqlStr As String = ""
        

        sqlStr = "Select * From Documents Where FileNo Like '%" & txtFileNo.Text & "%'"
        
        
        Dim DocReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While DocReader.Read
            txtDocID.Text = DocReader("DocID")
            txtFileNo.Text = DocReader("FileNo")
            txtDD.Text = CDate(DocReader("FileDate")).Day
            txtMM.Text = CDate(DocReader("FileDate")).Month
            txtYY.Text = CDate(DocReader("FileDate")).Year
            txtSubject.Text = DocReader("FileSubject")
            txtContent.Text = DocReader("FileContents")
            txtFileName.Text = DocReader("FilePath")
            btnOpen.Enabled = True
            myCount += 1
        End While
        DocReader.Close()
        
        

        If myCount <= 0 Then
            lblStatus.Text = "Invalid File Number..."
        End If
        txtFileNo.Focus()

    End Sub


    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        InitControls()
    End Sub

    Protected Sub btnOpen_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOpen.Click
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Redirect", "window.open('Documents/" & txtFileName.Text & "');", True)
        txtFileNo.Focus()
    End Sub

    Protected Sub btnRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.Click

        If txtDocID.Text = "" Then
            lblStatus.Text = "Invalid Input..."
            txtFileNo.Focus()
            Exit Sub
        End If

       
       
       

        Dim sqlStr As String = ""
        

        sqlStr = "Delete From Documents Where DocID=" & Val(txtDocID.Text)
        
        
        ExecuteQuery_Update(SqlStr)
        Try
            File.Delete(Server.MapPath("Documents") & "/" & txtFileName.Text)

        Catch ex As Exception
            lblStatus.Text = "File not deleted."
        End Try
       
        
        

        InitControls()

        lblStatus.Text = "File removed successfully..."

    End Sub

End Class