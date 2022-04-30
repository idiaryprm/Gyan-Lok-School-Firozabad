Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Imports iDiary_V3.iDiary.CLS_iDiary_Exam

Public Class ExamRemarksMaster
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try
            If Request.Cookies("UType").Value.ToString.Contains("Exam") Or Request.Cookies("UType").Value.ToString.Contains("Admin") Then
                'Allow
            Else
                Response.Redirect("AccessDenied.aspx")
            End If
        Catch ex As Exception
            Response.Redirect("Login.aspx")
        End Try
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then InitControls()
    End Sub

    Private Sub InitControls()
        lblRemark.Text = ""
        txtName.Text = ""
        txtID.Text = ""
        ' LoadMasterInfo(4, lstMasters)
        lblStatus.Text = ""
        ' chkDefault.Checked = False
        lblApplicable.Visible = False
        cboSubject.Focus()
        LoadRemarkSubjects(cboSubject)
        cbAll.Checked = False
    End Sub
    Private Sub LoadRemark()
        gvMapping.Visible = True
        Dim sqlstr As String = ""
        sqlstr = "SELECT SubjectName as Subject,remarkname as Remark,Grade,SubjectID," & _
            " STUFF( " & _
            "  (SELECT DISTINCT ',' + classname " & _
            "  FROM vw_examremarksmaster " & _
            "  WHERE SubjectName = Remarks.SubjectName And remarkname = Remarks.remarkname " & _
            "   FOR XML PATH ('')) , 1, 1, '')  AS [Applicable Class]" & _
            " FROM vw_examremarksmaster AS Remarks where SubjectID='" & cboSubject.SelectedItem.Value & "'" & _
            " GROUP BY remarkname, SubjectName,Grade,SubjectID Order By grade,RemarkName"

        sdsGrade.SelectCommand = sqlstr
        gvMapping.DataBind()

    End Sub
   
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        lblStatus.Text = ""
        If cboSubject.Text = "" Then
            lblStatus.Text = "Please Select Subject!"
            cboSubject.Focus()
            Exit Sub
        End If
        If Trim(txtGrade.Text) = "" Then
            lblStatus.Text = "Please Enter Grade!"
            txtGrade.Focus()
            Exit Sub
        End If
        If Trim(txtName.Text) = "" Then
            lblStatus.Text = "Please Enter Remarks!"
            txtName.Focus()
            Exit Sub
        End If

        Dim counter = 0
        For i = 0 To cblClasses.Items.Count - 1
            If cblClasses.Items(i).Selected = True Then
                counter = 1
                Exit For
            End If
        Next
        If counter = 0 Then
            lblStatus.Text = "Please Select atleast one Class !"
            cblClasses.Focus()
            Exit Sub
        End If

        Dim sqlStr As String = ""
        Dim UserID As Integer = Request.Cookies("UserID").Value
        Dim ASID As Integer = Request.Cookies("ASID").Value

        'If Trim(lblRemark.Text) = "" Then

        'End If

        sqlStr = "Select count(RemarkName) From ExamRemarksMaster Where SubjectID=" & cboSubject.Text & " AND grade='" & UCase(Trim(txtGrade.Text)) & "' AND  ASID=" & ASID
        If ExecuteQuery_ExecuteScalar(sqlStr) > 0 Then
            lblStatus.Text = "Remark for the same grade already exist."
            Exit Sub
        End If

        sqlStr = "Delete from ExamRemarksMaster Where grade='" & UCase(Trim(txtGrade.Text)) & "' AND SubjectID=" & cboSubject.Text & " AND ASID=" & ASID
        ExecuteQuery_Update(sqlStr)

        'Insert
        For i = 0 To cblClasses.Items.Count - 1
            If cblClasses.Items(i).Selected = True Then
                sqlStr = "Insert into ExamRemarksMaster(RemarkName,SubjectID,ClassID,grade,ASID,createdBy) Values(" & _
            "'" & SQLFixup(txtName.Text) & "','" & cboSubject.SelectedItem.Value & "','" & cblClasses.Items(i).Value & "','" & UCase(Trim(txtGrade.Text)) & "','" & ASID & "','" & UserID & "')"
                ExecuteQuery_Update(sqlStr)
            End If
        Next

        LoadRemark()
        txtName.Text = ""
        txtGrade.Text = ""
        lblApplicable.Visible = False
        CheckAll(False)
        cbAll.Checked = False

    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        txtName.Text = ""
        txtGrade.Text = ""
        lblRemark.Text = ""
        LoadMappedClasses()
        '  lstMasters.SelectedIndex = -1
        LoadRemark()
        ' gvMapping.Visible = False
    End Sub

   
    Protected Sub cboType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSubject.SelectedIndexChanged
        lblStatus.Text = ""
        cboSubject.Focus()
        LoadMappedClasses()
        LoadRemark()
        gvMapping.Visible = True
        lblApplicable.Visible = True
    End Sub
    Private Sub LoadMappedClasses()
        Dim sqlStr As String = ""
        sqlStr = "select Distinct classname,ClassID from vw_ExamSubjectMapping where (entrytype=2 or displayType=2) and subjectid=" & Val(cboSubject.SelectedItem.Value) & " Order by ClassID"
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        cblClasses.Items.Clear()
        While myReader.Read
            cblClasses.Items.Add(New ListItem(myReader(0), myReader(1)))
        End While
        myReader.Close()
    End Sub

    Protected Sub gvMapping_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvMapping.SelectedIndexChanged

        Dim lstClasses As String = gvMapping.SelectedRow.Cells(4).Text
        cboSubject.ClearSelection()
        cboSubject.Items.FindByValue(gvMapping.SelectedDataKey(0)).Selected = True

        txtName.Text = Server.HtmlDecode(gvMapping.SelectedRow.Cells(2).Text)
        lblRemark.Text = txtName.Text
        txtGrade.Text = gvMapping.SelectedRow.Cells(3).Text
        lblApplicable.Visible = True
        Dim ar() As String = lstClasses.Split(",")
        lstClasses = ""
        For i = 0 To ar.Length - 1
            lstClasses += ":" & ar(i) & ":,"
        Next

        For i = 0 To cblClasses.Items.Count - 1
            If lstClasses.Contains(":" & cblClasses.Items(i).Text & ":") = True Then
                cblClasses.Items(i).Selected = True
            Else
                cblClasses.Items(i).Selected = False
            End If
        Next
    End Sub

    Protected Sub cbAll_CheckedChanged(sender As Object, e As EventArgs) Handles cbAll.CheckedChanged
        CheckAll(cbAll.Checked)
    End Sub
    Private Sub CheckAll(ByVal isChecked As Boolean)
        For i = 0 To cblClasses.Items.Count - 1
            cblClasses.Items(i).Selected = isChecked
        Next
    End Sub
End Class