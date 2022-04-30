Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary

Partial Class Admin_Syllabus
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
        LoadMasterInfo(2, cboClass)
        txtSyllabusFor.Text = ""
        myFile.Controls.Clear()
        lblStatus.Text = ""
        cboClass.Focus()
        cboSection.Text = ""
        txtSyllID.Text = ""
    End Sub

    Private Sub UploadSyllabusFile()
        Dim fp1 As String = myFile.PostedFile.FileName
        If fp1.ToString() <> "" Then
            Dim fn1 As String = fp1.Substring(fp1.LastIndexOf("\\") + 1)
            Dim sp1 As String = ""
            sp1 = Server.MapPath("~/Syllabus")
            If sp1.EndsWith("\\") = False Then
                sp1 += "\"
            End If

            sp1 += fp1
            myFile.PostedFile.SaveAs(sp1)
        End If

    End Sub


    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If txtSyllabusFor.Text.Length <= 0 Then
            lblStatus.Text = "Please provide proper details..."
            txtSyllabusFor.Focus()
            Exit Sub
        End If

        If myFile.PostedFile.FileName.ToString.Length <= 0 Then
            lblStatus.Text = "Please select a file..."
            myFile.Focus()
            Exit Sub
        End If

        SaveSyllabusToDB()

        Dim TempClass As String = cboClass.Text
        Dim TempSyllabusFor As String = txtSyllabusFor.Text

        InitControls()
        lblStatus.Text = "Syllabus uploaded for Class: " & TempClass & " with details (" & TempSyllabusFor & ")"
        gvSyllabus.DataBind()
    End Sub

    Private Sub SaveSyllabusToDB()
        Dim CSSID As Integer = FindCSSID("", cboClass.Text, cboSection.Text, "")
        Dim sqlStr As String = ""
        If cboSection.Text = "ALL" Then
            Dim lstCSSID As New List(Of Integer)
            sqlStr = "Select distinct CSSID from vw_classstudent where classname='" & cboClass.Text & "'"

            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            While myReader.Read
                lstCSSID.Add(myReader("CSSID"))
            End While
            myReader.Close()

            For i = 0 To lstCSSID.Count - 1
                sqlStr = "Insert into Syllabus (CSSID, Title, FileName, UploadDate) Values (" & _
          lstCSSID.Item(i) & "," & _
          "'" & txtSyllabusFor.Text & "'," & _
          "'" & myFile.PostedFile.FileName & "'," & _
          "'" & Now.Date.Year & "/" & Now.Date.Month & "/" & Now.Date.Day & "'" & _
          ")"

                ExecuteQuery_Update(sqlStr)
                UploadSyllabusFile()
            Next
        ElseIf txtSyllID.Text <> Nothing Then
            sqlStr = "Update Syllabus Set CSSID=" & CSSID & ", FileName='" & myFile.PostedFile.FileName & _
                "', Title='" & txtSyllabusFor.Text & "', UploadDate='" & Now.Date.Year & "/" & Now.Date.Month & "/" & Now.Date.Day & _
                "' Where SyllabusID=" & txtSyllID.Text
            ExecuteQuery_Update(sqlStr)
            UploadSyllabusFile()
        Else
            sqlStr = "Insert into Syllabus (CSSID, Title, FileName, UploadDate) Values (" & _
        CSSID & "," & _
        "'" & txtSyllabusFor.Text & "'," & _
        "'" & myFile.PostedFile.FileName & "'," & _
        "'" & Now.Date.Year & "/" & Now.Date.Month & "/" & Now.Date.Day & "'" & _
        ")"

            ExecuteQuery_Update(sqlStr)
            UploadSyllabusFile()
        End If
    End Sub

    Protected Sub cboClass_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboClass.SelectedIndexChanged
        LoadClassSection("", cboClass.Text, cboSection)
        cboSection.Items.Add("ALL")
    End Sub

    Protected Sub btnRemove_Click(sender As Object, e As EventArgs) Handles btnRemove.Click
        If gvSyllabus.SelectedIndex = -1 Then
            lblStatus.Text = "Please select a row to delete."
            Exit Sub
        End If

        Dim sqlStr As String = ""

        sqlStr = "Delete From Syllabus Where SyllabusID='" & gvSyllabus.SelectedRow.Cells(1).Text & "'"

        ExecuteQuery_Update(SqlStr)

        gvSyllabus.DataBind()
        gvSyllabus.SelectedIndex = -1
        InitControls()
    End Sub

    Protected Sub gvSyllabus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvSyllabus.SelectedIndexChanged
        txtSyllID.Text = gvSyllabus.SelectedRow.Cells(1).Text
        txtSyllabusFor.Text = gvSyllabus.SelectedRow.Cells(2).Text
        Dim CssName As String = gvSyllabus.SelectedRow.Cells(5).Text
        GetCSSID(CssName)
    End Sub
    Public Sub GetCSSID(ByVal CssName As String)
        Dim sqlStr As String = "Select * From vw_ClassStudent Where CssName='" & CssName & "' "
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            cboClass.Text = myReader("ClassName")
            LoadClassSection("", cboClass.Text, cboSection)
            cboSection.Items.Add("ALL")
            cboSection.Text = myReader("SecName")
        End While
        myReader.Close()
        btnRemove.Visible = True
    End Sub
End Class
