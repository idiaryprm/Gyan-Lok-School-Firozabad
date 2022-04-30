Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Partial Class ClassSecConfig
    Inherits System.Web.UI.Page
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        Try

            If Request.Cookies("UType").Value.ToString.Contains("Student") Or Request.Cookies("UType").Value.ToString.Contains("Admin") Then
                'Allow
            Else
                Response.Redirect("AccessDenied.aspx")
            End If

        Catch ex As Exception

            If ex.Message.Contains("Object reference not set to an instance of an object") Then
                Response.Redirect("LoginFaculty.aspx")
            End If

        End Try

    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            InitControls()
        End If
    End Sub

    Private Sub InitControls()
        LoadMasterInfo(71, cboSchoolName, Request.Cookies("SchoolIDs").Value)
        'LoadMasterInfo(2, cboClass, cboSchoolName.Text)
        LoadClass()
        LoadMasterInfo(3, cboSection)
        LoadMasterInfo(62, cboSubSection)
        LoadMasterInfo(60, cboFeeGroup)
        LoadMasterInfo(63, lstBranch, cboSchoolName.Text)
        'LoadBranchs()
        'txtName.Text = ""
        txtID.Text = ""
        'txttmpID.Text = ""
        lblStatus.Text = ""
        cboClass.Focus()
    End Sub
    Private Sub LoadClass()
        Dim sqlstr As String = ""

        sqlstr = "Select ClassName From Classes order by DisplayOrder"

        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)
        cboClass.Items.Clear()
        cboClass.Items.Add("")
        While myReader.Read
            cboClass.Items.Add(myReader("ClassName"))
        End While
        myReader.Close()

    End Sub
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If cboSchoolName.Text = "" Then
            lblStatus.Text = "Please Select School Name!"
            cboSchoolName.Focus()
            Exit Sub
        End If
        If cboClass.Text = "" Then
            lblStatus.Text = "Please Select Class!"
            cboClass.Focus()
            Exit Sub
        End If
        If cboSection.Text = "" Then
            lblStatus.Text = "Please Select Section!"
            cboSection.Focus()
            Exit Sub
        End If
        If cboFeeGroup.Text = "" Then
            lblStatus.Text = "Please Select Fee Group!"
            cboSection.Focus()
            Exit Sub
        End If
        'If txtName.Text.Length <= 0 Then
        '    lblStatus.Text = "Please Enter Batch Name!"
        '    txtName.Focus()
        '    Exit Sub
        'End If

        Dim sqlStr As String = ""

        'Dim CourseID As Integer = GetMasterItemID(cboCourse.Text, 1)
        Dim SchoolID As Integer = FindMasterID(71, cboSchoolName.Text)
        Dim ClassId As Integer = FindMasterID(2, cboClass.Text)
        Dim SecId As Integer = FindMasterID(3, cboSection.Text)
        Dim SubSecId As Integer = FindMasterID(62, cboSubSection.Text)
        Dim FeeGroupId As Integer = FindMasterID(60, cboFeeGroup.Text)
        If CheckConfig(SchoolID, ClassId, SecId, SubSecId) = True And txtID.Text = "" Then
            lblStatus.Text = "Configuration Allready Exist!"
            'cboSection.Focus()
            Exit Sub
        End If

       
        Dim CSSName As String = cboClass.Text & " - " & cboSection.Text
        If cboSubSection.Text <> "" Then
            CSSName += " - " & cboSubSection.Text
        End If

        If txtID.Text = "" Then
            If SubSecId > 0 Then
                sqlStr = "Insert into ClassStudent(SchoolID,ClassId,SecId,SubSecId,CSSName,FeeGroupID) Values(" & SchoolID & "," & ClassId & "," & SecId & "," & SubSecId & "," & "'" & CSSName & "'," & FeeGroupId & ")"
            Else
                sqlStr = "Insert into ClassStudent(SchoolID,ClassId,SecId,CSSName,FeeGroupID) Values(" & SchoolID & "," & ClassId & "," & SecId & "," & "'" & CSSName & "'," & FeeGroupId & ")"
            End If
         ExecuteQuery_Update(sqlStr)
        Else
            sqlStr = "Update ClassStudent Set SchoolID='" & SchoolID & "', ClassId='" & ClassId & "',SecId='" & SecId & "',CSSName='" & CSSName & "',FeeGroupID=" & FeeGroupId
            If SubSecId <> 0 Then
                sqlStr += ",SubSecId='" & SubSecId & "'"
            Else
                sqlStr += ",SubSecId=NULL"
            End If
            sqlStr += " Where CSSID = " & Val(txtID.Text)
           ExecuteQuery_Update(sqlStr)
        End If

        txtID.Text = ""
        Dim tmpSchoolName As String = cboSchoolName.Text
        InitControls()
        cboSchoolName.Text = tmpSchoolName
        LoadMasterInfo(63, lstBranch, cboSchoolName.Text)
        'LoadData()
    End Sub


    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        InitControls()
        cboClass.Focus()
    End Sub

    Protected Sub btnRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        Dim sqlStr As String = "Delete From ClassStudent Where CSSID=" & Val(txtID.Text)
      ExecuteQuery_Update(sqlStr)
        InitControls()
        txtID.Text = ""
    End Sub

    Protected Sub lstBranch_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstBranch.SelectedIndexChanged
        Dim sqlStr As String = "Select * From vw_ClassStudent Where CSSName='" & lstBranch.Text & "' and SchoolName='" & cboSchoolName.Text & "'"
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            txtID.Text = myReader("CSSID")
            cboClass.Text = myReader("ClassName")
            cboSection.Text = myReader("SecName")
            Try
                cboSubSection.Text = myReader("SubSecName")
            Catch ex As Exception

            End Try

            cboFeeGroup.Text = myReader("FeeGroupName")
            'txtName.Text = myReader("BatchName")
        End While
        myReader.Close()
    End Sub
    Private Function CheckConfig(SchoolID As Integer, ClassID As Integer, SecID As Integer, SubSecID As Integer)
        Dim sqlStr As String = "Select count(*) From ClassStudent Where SchoolID='" & SchoolID & "' and CLassID='" & ClassID & "' and SecID='" & SecID & "'"
        If SubSecID <> 0 Then
            sqlStr += " and SubSecID='" & SubSecID & "'"
        End If
        Dim rv As Integer = 0
        Try
            rv = ExecuteQuery_ExecuteScalar(sqlStr)
        Catch ex As Exception

        End Try

        If rv = 0 Then
            Return False
        Else
            Return True
        End If
    End Function

    Protected Sub cboSchoolName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSchoolName.SelectedIndexChanged
        LoadMasterInfo(63, lstBranch, cboSchoolName.Text)
    End Sub
End Class
