Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary

Partial Class Admin_Assignments
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then InitControls()
    End Sub

    Private Sub InitControls()
        'LoadClassSections()
        txtAssignmentDetails.Text = ""
        myFile.Controls.Clear()
        lblStatus.Text = ""
        LoadMasterInfo(71, cboSchoolName, Request.Cookies("SchoolIDs").Value)
        LoadMasterInfo(2, cboClass, cboSchoolName.Text)
        cboClass.Focus()
        GridBind()
        cboSection.Text = ""
        btnDelete.Visible = False

    End Sub
    Public Sub GridBind()
        SqlDataSource1.SelectCommand = "Select Distinct CSSName, Title, FileName, UploadDate,AssID from vw_Assignments where SchoolName='" & cboSchoolName.Text & "'"
        GridView1.DataBind()
    End Sub
    Private Sub UploadAssignmentFile(ByVal fileAppend As String)
        Dim fp1 As String = fileAppend & myFile.PostedFile.FileName
        If fp1.ToString() <> "" Then
            Dim fn1 As String = fp1.Substring(fp1.LastIndexOf("\\") + 1)
            Dim sp1 As String = ""
            sp1 = Server.MapPath("~/Assignments")
            If sp1.EndsWith("\\") = False Then
                sp1 += "\"
            End If

            sp1 += fp1
            myFile.PostedFile.SaveAs(sp1)
        End If

    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If cboSchoolName.Text = "" Then
            lblStatus.Text = "Please choose a School Name..."
            cboSchoolName.Focus()
            Exit Sub
        End If
        If cboClass.Text = "" Then
            lblStatus.Text = "Please choose a class..."
            cboClass.Focus()
            Exit Sub
        End If
        If cboSection.Text = "" Then
            lblStatus.Text = "Please choose a section..."
            cboSection.Focus()
            Exit Sub
        End If
        If txtAssignmentDetails.Text.Length <= 0 Then
            lblStatus.Text = "Please provide proper details..."
            txtAssignmentDetails.Focus()
            Exit Sub
        End If

        If myFile.PostedFile.FileName.ToString.Length <= 0 Then
            lblStatus.Text = "Please select a file..."
            myFile.Focus()
            Exit Sub
        End If

        SaveAssignmentToDB()

        Dim TempClass As String = cboClass.Text
        Dim TempAssignmentTitle As String = txtAssignmentDetails.Text

        InitControls()
        lblStatus.Text = "Assignment uploaded for Class: " & TempClass & " with details (" & TempAssignmentTitle & ")"

        GridView1.DataBind()
    End Sub

    Private Sub SaveAssignmentToDB()
        Dim CSSID As Integer = FindCSSID(cboSchoolName.Text, cboClass.Text, cboSection.Text, cboSubSection.Text)
        Dim sqlStr As String = ""

        Dim AppendTitle As String = DateTime.Now.ToString("ddMMyyyyhhmmss")
        If cboSection.Text = "ALL" Then
            Dim lstCSSID As New List(Of Integer)
            sqlStr = "Select distinct CSSID from vw_classstudent where classname='" & cboClass.Text & "' and SchoolName='" & cboSchoolName.Text & "'"
            
            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            While myReader.Read
                lstCSSID.Add(myReader("CSSID"))
            End While
            myReader.Close()

            For i = 0 To lstCSSID.Count - 1
                sqlStr = "Insert into Assignments (CSSID, Title, FileName, UploadDate, IsRead) Values (" & _
           lstCSSID.Item(i) & "," & _
           "'" & txtAssignmentDetails.Text & "'," & _
           "'" & AppendTitle & myFile.PostedFile.FileName & "'," & _
           "'" & Now.Date.Year & "/" & Now.Date.Month & "/" & Now.Date.Day & "',0" & _
           ")"
                
                UploadAssignmentFile(AppendTitle)
                ExecuteQuery_Update(SqlStr)
            Next
        ElseIf txtAssignID.Text <> Nothing Then
            sqlStr = "Update Assignments Set Title='" & txtAssignmentDetails.Text & "', FileName='" & AppendTitle & myFile.PostedFile.FileName & _
                "', UploadDate='" & Now.Date.Year & "/" & Now.Date.Month & "/" & Now.Date.Day & "',IsRead=0" & _
                ",CSSID='" & CSSID & "' Where AssID=" & txtAssignID.Text
            UploadAssignmentFile(AppendTitle)
            ExecuteQuery_Update(sqlStr)
        Else
            sqlStr = "Insert into Assignments (CSSID, Title, FileName, UploadDate, IsRead) Values (" & _
            CSSID & "," & _
            "'" & txtAssignmentDetails.Text & "'," & _
            "'" & AppendTitle & myFile.PostedFile.FileName & "'," & _
            "'" & Now.Date.Year & "/" & Now.Date.Month & "/" & Now.Date.Day & "',0" & _
            ")"
            UploadAssignmentFile(AppendTitle)
            ExecuteQuery_Update(sqlStr)
        End If
    End Sub

    'Protected Sub cboClass_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboClass.SelectedIndexChanged
    '    LoadClassSection(cboClass.Text, cboSection)
    'End Sub

    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged
        txtAssignID.Text = GridView1.SelectedRow.Cells(1).Text
        txtAssignmentDetails.Text = GridView1.SelectedRow.Cells(2).Text
        Dim CssName As String = GridView1.SelectedRow.Cells(5).Text
        GetCSSID(CssName)
    End Sub
    Public Sub GetCSSID(ByVal CssName As String)
        Dim sqlStr As String = "Select * From vw_ClassStudent Where CssName='" & CssName & "' "
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            cboClass.Text = myReader("ClassName")
            LoadClassSection(cboSchoolName.Text, cboClass.Text, cboSection)
            cboSection.Items.Add("ALL")
            cboSection.Text = myReader("SecName")
            Try
                LoadClassSubSection(cboSchoolName.Text, cboClass.Text, cboSection.Text, cboSubSection)
                cboSubSection.Text = myReader("SubSecName")
            Catch ex As Exception

            End Try
        End While
        myReader.Close()
        btnDelete.Visible = True
    End Sub

    Protected Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
       
        Dim sqlStr As String = ""
        sqlStr = "Delete from Assignments Where AssID=" & txtAssignID.Text

        ExecuteQuery_Update(SqlStr)
        GridView1.DataBind()
        InitControls()
    End Sub

    Protected Sub cboSection_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSection.SelectedIndexChanged
        If cboSection.Text = "All" Then
            SqlDataSource1.SelectCommand = "Select Distinct CSSName, Title, FileName, UploadDate,AssID  from vw_Assignments where ClassName='" & cboClass.Text & "' and SchoolName='" & cboSchoolName.Text & "'"
        Else
            SqlDataSource1.SelectCommand = "Select Distinct CSSName, Title, FileName, UploadDate,AssID  from vw_Assignments where ClassName='" & cboClass.Text & "' and SchoolName='" & cboSchoolName.Text & "' and SecName='" & cboSection.Text & "'"
        End If
        GridView1.DataBind()
        LoadClassSubSection(cboSchoolName.Text, cboClass.Text, cboSection.Text, cboSubSection)
    End Sub

    Protected Sub cboClass_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboClass.SelectedIndexChanged
        LoadClassSection(cboSchoolName.Text, cboClass.Text, cboSection)
        cboSection.Items.Add("ALL")
    End Sub

    Protected Sub cboSchoolName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSchoolName.SelectedIndexChanged
        LoadMasterInfo(2, cboClass, cboSchoolName.Text)
        cboSchoolName.Focus()
    End Sub
End Class
