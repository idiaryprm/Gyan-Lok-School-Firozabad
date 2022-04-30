
Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Imports iDiary_V3.iDiary_Student.CLS_iDiary_Student
Imports Microsoft.Reporting.WebForms

Public Class Student_Subsec
    Inherits System.Web.UI.Page


    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try
            If Request.Cookies("UType").Value.ToString.Contains("Student") Or Request.Cookies("UType").Value.ToString.Contains("Exam") Or Request.Cookies("UType").Value.ToString.Contains("Admin") Then
                'Allow
            Else
                Response.Redirect("/./AccessDenied.aspx", False)
            End If
        Catch ex As Exception
            Response.Redirect("~/Logout.aspx")
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cookies("ActiveTab").Value = 2
        Response.Cookies("ActiveTab").Expires = DateTime.Now.AddHours(1)
        Session("ActiveTab") = 2
        If IsPostBack = False Then InitControls()
        If Request.Cookies("UType").Value.ToString.Contains("Student-1") = False And Request.Cookies("UType").Value.ToString.Contains("Admin-1") = False Then
            btnPromote.Enabled = False
        End If
    End Sub

    Private Sub InitControls()

        'LoadMasterInfo(1, cboCurrentAcademicSession)
        'cboCurrentAcademicSession.Text = Request.Cookies("ASName").Value
        'LoadMasterInfo(1, cboNextAcademicSession)
        'LoadNextAcademicSession()
        LoadMasterInfo(71, cboSchoolNameCurrent, Request.Cookies("SchoolIDs").Value)
        LoadMasterInfo(2, cboCurrentClass, cboSchoolNameCurrent.Text)
        LoadMasterInfo(71, cboSchoolNameNext, Request.Cookies("SchoolIDs").Value)
        LoadMasterInfo(2, cboNextClass, cboSchoolNameNext.Text)

        cboCurrentSection.Items.Clear()
        cboNextSection.Items.Clear()

        LoadMasterInfo(10, cboStatus)

        chkStudList.Items.Clear()
        lblStatus.Text = ""
        'cboCurrentAcademicSession.Focus()

    End Sub

    Protected Sub btnPromote_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPromote.Click
        'If cboCurrentClass.Text = cboNextClass.Text Then
        '    ' Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('You had selected same class for both the academic years!');", True)
        '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "CallMyFunction", "Confirm()", True)
        'End If
        'Dim confirmValue As String = Request.Form("confirm_value")
        'If confirmValue = "Yes" Then

        Dim CASID As Integer = Request.Cookies("ASID").Value
        Dim CurrentCSSID As Integer = FindCSSID(cboSchoolNameCurrent.Text, cboCurrentClass.Text, cboCurrentSection.Text, cboCurrentSubSection.Text)
        'Dim CSecID As Integer = 0
        'FindSectionID(cboCurrentClass.Text, cboCurrentSection.Text)
        Dim CStatusID As Integer = FindMasterID(10, cboStatus.Text)

        Dim NASID As Integer = Request.Cookies("ASID").Value
        Dim NewCSSID As Integer = FindCSSID(cboSchoolNameNext.Text, cboNextClass.Text, cboNextSection.Text, cboNextSubSection.Text)
        If NewCSSID = 0 Then
            lblStatus.Text = "Combination of Promoted School/Class/Sec/Sub-Section is not present, Please Manage it from Class Sec Config"
            cboNextClass.Focus()
            Exit Sub
        End If
        Dim NSecID As Integer = 0
        'FindSectionID(cboNextClass.Text, cboNextSection.Text)

        Dim sqlStr As String = ""
        Dim lstInsert As New ListBox
        Dim myCommand As New SqlCommand
        Dim RegNo As String = ""
        Dim i As Integer = 0
        Dim AllreadyExistRegNo As String = ""
        For i = 0 To chkStudList.Items.Count - 1

            If chkStudList.Items.Item(i).Selected = True Then

                Dim Loc1 As Integer = chkStudList.Items.Item(i).Text.IndexOf("(")
                RegNo = chkStudList.Items.Item(i).Text.Substring(0, Loc1)
                'If CheckSRExist(RegNo, NASID) = True Then
                '    AllreadyExistRegNo += RegNo & ","
                '    Continue For
                'End If

                'Find Old SID
                sqlStr = "Select MAX(SID) From Student Where RegNo='" & RegNo & "' AND ASID=" & CASID
                Dim OldSID As Integer = ExecuteQuery_ExecuteScalar(sqlStr)

                '-------Student--------
                'Copy Student Reocrd

                'sqlStr = "Insert into Student (StudentID,RegNo,ClassRollno,FeeBookNo,CSSID,HouseID,ASID,Promoted,PhotoPath,FeeConfigType,UserID) "
                'sqlStr += " SELECT StudentID,RegNo,ClassRollno,FeeBookNo,CSSID,HouseID,ASID,Promoted,PhotoPath,FeeConfigType,UserID FROM Student WHERE SID=" & OldSID

                'ExecuteQuery_Update(sqlStr)

                ''Get New SID
                'sqlStr = "Select MAX(SID) From Student"

                'Dim NewSID As Integer = ExecuteQuery_ExecuteScalar(sqlStr)

                'Update Class, Section and ASID
                sqlStr = "Update Student Set CSSID=" & NewCSSID & " Where SID=" & OldSID
                ExecuteQuery_Update(sqlStr)

                'Update Promoted Status of OldSID
                'sqlStr = "Update Student Set Promoted=2 Where SID=" & OldSID
                'ExecuteQuery_Update(sqlStr)

                'Save_Log("STUDENT PROMOTION", RegNo)
            End If
        Next
        myCommand.Dispose()

        Dim CClass As String = cboCurrentClass.Text & "-" & cboCurrentSection.Text
        Dim NClass As String = cboNextClass.Text & "-" & cboNextSection.Text & "-" & cboNextSubSection.Text
        'Dim TempCASName As String = cboCurrentAcademicSession.Text
        Dim TempCSchool As String = cboSchoolNameCurrent.Text
        Dim TempClassName As String = cboCurrentClass.Text
        Dim TempSecName As String = cboCurrentSection.Text
        Dim TempSubSecName As String = cboCurrentSubSection.Text
        Dim TempStatus As String = cboStatus.Text
        'Dim TempNextASName As String = cboNextAcademicSession.Text
        Dim TempNSchool As String = cboSchoolNameNext.Text
        InitControls()

        'cboCurrentAcademicSession.Text = TempCASName
        cboSchoolNameCurrent.Text = TempCSchool
        LoadMasterInfo(2, cboCurrentClass, cboSchoolNameCurrent.Text)
        cboCurrentClass.Text = TempClassName
        LoadClassSection(cboSchoolNameCurrent.Text, cboCurrentClass.Text, cboCurrentSection)
        cboCurrentSection.Text = TempSecName
        LoadClassSubSection(cboSchoolNameCurrent.Text, cboCurrentClass.Text, cboCurrentSection.Text, cboCurrentSubSection)
        cboCurrentSubSection.Text = TempSubSecName
        cboStatus.Text = TempStatus
        'cboNextAcademicSession.Text = TempNextASName
        cboSchoolNameNext.Text = TempNSchool
        LoadMasterInfo(2, cboNextClass, cboSchoolNameNext.Text)
        LoadStudents(1)

        lblStatus.Text = "Selected Students of Class " & CClass & " changed subsec to " & NClass & " successfully..."
        'If AllreadyExistRegNo <> "" Then
        '    lblStatus.Text += " But Reg No already Exist Please Check: " & AllreadyExistRegNo & " in Academic Session " & cboNextAcademicSession.Text
        'End If

        'cboCurrentAcademicSession.Focus()
        'Else
        'ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('Saved Successfully!')", True)
        'End If

    End Sub

    Protected Sub chkSelectAll_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkSelectAll.CheckedChanged
        chkNone.Checked = False
        Dim i As Integer = 0
        For i = 0 To chkStudList.Items.Count - 1
            chkStudList.Items.Item(i).Selected = True
        Next
    End Sub

    Protected Sub chkNone_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkNone.CheckedChanged
        chkSelectAll.Checked = False
        Dim i As Integer = 0
        For i = 0 To chkStudList.Items.Count - 1
            chkStudList.Items.Item(i).Selected = False
        Next
    End Sub

    Protected Sub cboCurrentClass_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCurrentClass.SelectedIndexChanged
        LoadClassSection(cboSchoolNameCurrent.Text, cboCurrentClass.Text, cboCurrentSection)
        cboCurrentClass.Focus()
    End Sub


    Protected Sub cboNextClass_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboNextClass.SelectedIndexChanged
        LoadClassSection(cboSchoolNameNext.Text, cboNextClass.Text, cboNextSection)
        cboNextClass.Focus()
    End Sub

    Private Sub LoadStudents(type As Integer)
        '1 for all , 2 for only regno
        Dim ASID As Integer = Request.Cookies("ASID").Value

        Dim sqlStr As String = "Select RegNo, SName From vw_Student Where " & _
        "ClassName='" & cboCurrentClass.Text & "' AND SecName='" & cboCurrentSection.Text & "' AND ASID=" & ASID & _
        " AND Promoted=0 AND StatusName='" & cboStatus.Text & "' "
        If cboCurrentSubSection.Text <> "" Then
            sqlStr += " and SubSecName='" & cboCurrentSubSection.Text & "'"
        End If
        sqlStr += " Order By SName"

        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        chkStudList.Items.Clear()
        While myReader.Read

            chkStudList.Items.Add(myReader(0) & "(" & myReader(1) & ")")

        End While
        myReader.Close()

    End Sub

    Protected Sub btnList_Click(sender As Object, e As EventArgs) Handles btnList.Click
        'If cboCurrentAcademicSession.Text = "" Then
        '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Select Current Academic Session.');", True)
        '    cboCurrentAcademicSession.Focus()
        '    Exit Sub
        'End If
        If cboCurrentClass.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Select Current Class.');", True)
            cboCurrentClass.Focus()
            Exit Sub
        End If
        If cboCurrentSection.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Select Current Section.');", True)
            cboCurrentSection.Focus()
            Exit Sub
        End If
        If cboStatus.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Select Student Status');", True)
            cboStatus.Focus()
            Exit Sub
        End If
        'Dim obj As New ReportCardClass
        'If Request.Cookies("UType").Value.ToString.Contains("Admin") Or obj.CheckClassTeacherPermission(cboCurrentClass.Text, cboCurrentSection.Text, Request.Cookies("UserID").Value) = True Then
        '    btnPromote.Enabled = True
        'Else
        '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('You are not authorized for this section.');", True)
        '    Exit Sub
        'End If
        LoadStudents(1)
    End Sub
    Protected Sub btnListPromotion_Click(sender As Object, e As EventArgs) Handles btnListPromotion.Click

        Dim lstRegno As New List(Of String)
        Dim sqlStr As String = "Select RegNo from vw_Student Where ASID=" & Request.Cookies("ASID").Value & " AND className='" & cboCurrentClass.Text & "' AND SecName='" & cboCurrentSection.Text & "' AND StatusName='" & cboStatus.Text & "'"

        Dim regReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While regReader.Read
            lstRegno.Add(regReader(0))
        End While
        regReader.Close()
        sqlStr = "Select Regno,SName,ClassName,SecName,ASNAME,ASID From vw_student WHERE SID<0"
        Dim ds As DataSet = ExecuteQuery_DataSet(sqlStr, "tbl")
        Dim regno As String = ""
        For i = 0 To lstRegno.Count - 1
            regno = lstRegno.Item(i).ToString

            sqlStr = "Select SName,ClassName,SEcName,ASNAME,ASID from vw_Student WHEre regno='" & regno & "'"

            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            While myReader.Read
                Dim newRow As DataRow = ds.Tables(0).NewRow()
                newRow("RegNo") = regno
                newRow("SName") = myReader("SNAME")
                newRow("ClassName") = myReader("ClassName")
                newRow("SEcName") = myReader("SEcName")
                newRow("ASNAME") = myReader("ASNAME")
                newRow("ASID") = myReader("ASID")

                ds.Tables(0).Rows.Add(newRow)
            End While
            myReader.Close()

        Next

        Dim rds As ReportDataSource = New ReportDataSource()
        rds.Name = "DataSet1" ' Change to what you will be using when creating an objectdatasource
        rds.Value = ds.Tables(0)
        With ReportViewer1   ' Name of the report control on the form
            .Reset()
            .ProcessingMode = ProcessingMode.Local
            .LocalReport.DataSources.Clear()
            .Visible = True
            .LocalReport.ReportPath = "Report/rptPromotionListNew.rdlc"
            .LocalReport.DataSources.Add(rds)
        End With
        Dim Param As String = "Promotion List for Class : " & cboCurrentClass.Text & " - " & cboCurrentSection.Text & " for the Session : " & Request.Cookies("ASID").Value
        Dim params(0) As Microsoft.Reporting.WebForms.ReportParameter
        params(0) = New Microsoft.Reporting.WebForms.ReportParameter("Param", Param, True)
        Me.ReportViewer1.LocalReport.SetParameters(params)
        ReportViewer1.Visible = True
        ReportViewer1.LocalReport.Refresh()
    End Sub

    'Protected Sub cboCurrentAcademicSession_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCurrentAcademicSession.SelectedIndexChanged
    '    LoadNextAcademicSession()
    'End Sub

    'Private Sub LoadNextAcademicSession()

    '    Dim ASID As Integer = FindMasterID(1, cboCurrentAcademicSession.Text)

    '    Dim sqlStr As String = "select top(1) ASNAME from academicsession where asid > " & ASID & " order by ASID asc"

    '    cboNextAcademicSession.Items.Clear()
    '    cboNextAcademicSession.Items.Add(ExecuteQuery_ExecuteScalar(sqlStr))

    'End Sub
    'Private Sub Save_Log(ByVal type As String, regNo As String)

    '    Dim log1 As String = ""
    '    Dim sqlStr As String = ""
    '    If (type.Contains("PROMOTION") = True) Then

    '        log1 = "Student RegNo : " & regNo & ", Promoted to Session : " & cboNextAcademicSession.Text & ", Class : " & cboNextClass.Text & ", Section : " & cboNextSection.Text & "    #######" & vbNewLine
    '        log1 &= " From Session : " & cboCurrentAcademicSession.Text & ", Class : " & cboCurrentClass.Text & ", Section : " & cboCurrentSection.Text & "    #######" & vbNewLine
    '    Else

    '    End If

    '    sqlStr = "Insert Into Event_log(logTime,EventType,Details,loginId,Visible) Values('" & System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "','" & type & "','" & log1 & "','" & Request.Cookies("UID").Value & "','1')"
    '    ExecuteQuery_Update(sqlStr)
    'End Sub

    Protected Sub cboCurrentSection_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCurrentSection.SelectedIndexChanged
        LoadClassSubSection(cboSchoolNameCurrent.Text, cboCurrentClass.Text, cboCurrentSection.Text, cboCurrentSubSection)
    End Sub

    Protected Sub cboNextSection_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboNextSection.SelectedIndexChanged
        LoadClassSubSection(cboSchoolNameNext.Text, cboNextClass.Text, cboNextSection.Text, cboNextSubSection)
    End Sub

    Protected Sub cboSchoolNameCurrent_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSchoolNameCurrent.SelectedIndexChanged
        LoadMasterInfo(2, cboCurrentClass, cboSchoolNameCurrent.Text)
        cboSchoolNameCurrent.Focus()
    End Sub

    Protected Sub cboSchoolNameNext_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSchoolNameNext.SelectedIndexChanged
        LoadMasterInfo(2, cboNextClass, cboSchoolNameNext.Text)
        cboSchoolNameNext.Focus()
    End Sub
End Class