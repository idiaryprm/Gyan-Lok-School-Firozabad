Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
'Imports iDiary_V3.iDiary_Student.CLS_iDiary_Student
Imports iDiary_V3.iDiary_Date.CLS_iDiary_Date
Imports Microsoft.Reporting.WebForms

Partial Class Cert_TC1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then InitControls()
    End Sub
    Private Sub InitControls()
        Session("ActiveTab") = 6
        ReportViewer1.Visible = False
        'txtSno.Text = Now.Date.Year & "/" & FindNextSno(Now.Date.Year, 2)
        txtSno.Text = FindNextSno(Now.Date.Year, 2) & "/" & Now.Date.Year
        'cboClass.Items.Clear()
        LoadMasterInfo(71, cboSchoolName, Request.Cookies("SchoolIDs").Value)
        chkWholeClass.Checked = False
        LoadMasterInfo(2, cboClass, cboSchoolName.Text)
        LoadMasterInfo(2, cboClasses, cboSchoolName.Text)
        'LoadMasterInfo(2, cboClass, cboSchoolName.Text)
        txtSRNo.Text = ""
        txtDOB.Text = ""
        txtName.Text = ""
        txtFName.Text = ""
        txtMName.Text = ""
        cboLastExamTaken.SelectedIndex = 0
        cboLastClassResult.SelectedIndex = 0
        cboLastClassResult.Enabled = False
        txtSubjects.Text = ""
        txtFailed.Text = ""
        txtClassSection.Text = ""
        txtDateAdmission.Text = Now.Date.ToString("dd/MM/yyyy")
        ddlPromoted.SelectedIndex = 0
        txtSID.Text = ""
        txtApplicationDate.Text = Now.Date.ToString("dd/MM/yyyy")
        ' txtLeaveReason.Text = ""
        LoadMasterInfo(15, cboCharacter)
        LoadMasterInfo(15, cboConduct)
        'LoadMasterInfo(40, cboLastClassResult)
        txtremark.Text = ""

        'txtDateDropOut.Text = ""
        txtWorkDays.Text = ""
        cboToClass.SelectedIndex = 0
        cboToClass.Enabled = False
        cboMonth.Text = ""
        txtFeeCon.Text = ""
        cboreason.SelectedIndex = 0
        txtSchoolDays.Text = ""
        txtNCC.Text = ""
        'txtCategory.Text = ""
        cboCharacter.SelectedIndex = 0
        'LoadMasterInfo(39, cboLastClassDivision)
        'LoadMasterInfo(40, cboLastClassResult)
        txtIssuedDate.Text = Now.Date.ToString("dd/MM/yyyy")
        'txtBookNo.Focus()
        txtSRNo.Focus()
        'LoadMasterInfo(1, cboSession)   ''Load Academic Sessions
        'cboSession.Focus()
        txtExtraCorr.Text = ""
        chkCancel.Text = "No"
        chkCancel.Enabled = False
        If Request.QueryString("TCNo") = "" Then

        Else
            Try
                PrepareReport(Request.QueryString("TCNo"), 1)
                txtSno.Text = Request.QueryString("TCNo")
                GetTCDetails()
            Catch ex As Exception

            End Try
        End If
        Dim sqlstr As String = ""
        sqlstr = "Select * from Params"
        Dim SchoolName As String = ""
        Dim Address As String = ""
        Dim SchoolType As Integer = 0
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)
        Dim ReportPath As String = ""
        While myReader.Read
            SchoolName = myReader("SchoolName")
            Address = myReader("SchoolDetails")
            SchoolType = myReader("SchoolType")
        End While
        myReader.Close()
        If SchoolType = 1 Then
            Panel1.Visible = True
        Else
            Panel1.Visible = False

        End If
        cboCharacter.SelectedIndex = 1
        RandomNo = getRandomNo()
    End Sub
    Protected Sub btnGenerate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        'If Trim(cboSession.Text) = "" Then
        '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please choose Academic Year first...');", True)
        '    cboSession.Focus()
        '    Exit Sub
        'End If
        If chkWholeClass.Checked = False Then
            If Trim(txtSRNo.Text = "") Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Enter Student Admn No. to continue...');", True)
                txtSno.Focus()
                Exit Sub
            End If
            If Trim(txtName.Text = "") Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Enter Student Name to continue...');", True)
                txtName.Focus()
                Exit Sub
            End If
        End If

        If Trim(txtApplicationDate.Text = "") Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Enter Promotion date to continue...');", True)
            txtApplicationDate.Focus()
            Exit Sub
        End If
        'If Trim(txtLeaveReason.Text = "") Then
        '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert(Enter School leaving reason to continue...');", True)
        '    txtLeaveReason.Focus()
        '    Exit Sub
        'End If
        If Trim(cboCharacter.Text = "") Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Choose Conduct to continue...');", True)
            cboCharacter.Focus()
            Exit Sub
        End If
        If CheckTC() = True And chkWholeClass.Checked = False Then
            lblStatus.Text = "TC is Already Exists..."
            txtSno.Focus()
            Exit Sub
        End If

       
       
       

        Dim sqlstr As String = ""

        Dim myCommand1 As New SqlCommand
        Dim ASID As String = Request.Cookies("ASID").Value
        sqlstr = "Select * From vw_Student Where ClassName=N'" & cboClass.Text & "' And SecName = N'" & cboSection.Text & "' AND ASID=" & ASID
        Dim ds As DataSet = ExecuteQuery_DataSet(sqlstr, "student")
        Dim CharacterID As Integer = FindMasterID(15, cboCharacter.Text)
        Dim LastClassResultID As Integer = FindMasterID(40, cboLastClassResult.Text)
        Dim TCno As String = ""
        Dim DOBYear As String = ""
        Dim DOBInWords As String = ""

        '      myCommand1.Dispose()
        '   
        If chkWholeClass.Checked = True Then

            'Dim myReader As SqlDataReader = myCommand1.ExecuteReader

            For i = 0 To ds.Tables(0).Rows.Count - 1
                Dim a As Date = ds.Tables(0).Rows(i).Item("DOB").ToString
                'TCno = Now.Date.Year & "/" & FindNextSno(Now.Date.Year, 2)
                TCno = FindNextSno(Now.Date.Year, 2) & "/" & Now.Date.Year

                DOBInWords = ConvertDateToWords(a.ToString("dd/MM/yyyy").Substring(0, 2), a.ToString("dd/MM/yyyy").Substring(3, 2), a.ToString("dd/MM/yyyy").Substring(6, 4))

                sqlstr = "Insert into TC (TCNo, SID, DOBInWords, admissionDate, admissionClass, lastClass,lastExamTaken, lastExamResult, Failed, subjectsStudied, promoted, promotedClass, feeMonth, "
                sqlstr += " feeConcession,workDays, presentDays, NCC, corricular, conduct, dateOFApplication, dateOfIssue,Reason, remarks,isIssued,IsCanceled) Values("
                sqlstr += "'" & SQLFixup(TCno) & "',"
                sqlstr += "'" & ds.Tables(0).Rows(i).Item("SID") & "',"
                sqlstr += "'" & DOBInWords & "',"
                sqlstr += "'" & txtDateAdmission.Text.Substring(6, 4) & "/" & txtDateAdmission.Text.Substring(3, 2) & "/" & txtDateAdmission.Text.Substring(0, 2) & "',"
                sqlstr += "' ',"
                sqlstr += "'" & cboLastClass.Text & "',"
                sqlstr += "'" & cboLastExamTaken.Text & "',"
                sqlstr += "'" & cboLastClassResult.Text & "',"
                sqlstr += "'" & SQLFixup(txtFailed.Text) & "',"
                sqlstr += "'" & SQLFixup(txtSubjects.Text) & "',"
                sqlstr += "'" & ddlPromoted.Text & "',"
                sqlstr += "'" & cboToClass.Text & "',"
                sqlstr += "'" & cboMonth.Text & "',"
                sqlstr += "'" & SQLFixup(txtFeeCon.Text) & "',"
                sqlstr += "'" & SQLFixup(txtWorkDays.Text) & "',"
                sqlstr += "'" & SQLFixup(txtSchoolDays.Text) & "',"
                sqlstr += "'" & SQLFixup(txtNCC.Text) & "',"
                sqlstr += "'" & SQLFixup(txtExtraCorr.Text) & "',"
                sqlstr += "'" & cboCharacter.Text & "',"
                sqlstr += "'" & txtApplicationDate.Text.Substring(6, 4) & "/" & txtApplicationDate.Text.Substring(3, 2) & "/" & txtApplicationDate.Text.Substring(0, 2) & "',"
                sqlstr += "'" & txtIssuedDate.Text.Substring(6, 4) & "/" & txtIssuedDate.Text.Substring(3, 2) & "/" & txtIssuedDate.Text.Substring(0, 2) & "',"
                sqlstr += "'" & cboreason.Text & "',"
                sqlstr += "N'" & SQLFixup(txtremark.Text) & "',1,0)"
                ExecuteQuery_Update(sqlstr)

                GetStudentID("0")

            Next
            PrepareReport(TCno, 2)
        Else
            'TCno = Now.Date.Year & "/" & FindNextSno(Now.Date.Year, 2)
            TCno = FindNextSno(Now.Date.Year, 2) & "/" & Now.Date.Year
            DOBInWords = ConvertDateToWords(txtDOB.Text.Substring(0, 2), txtDOB.Text.Substring(3, 2), txtDOB.Text.Substring(6, 4))

            sqlstr = "Insert into TC (TCNo, SID, DOBInWords, studentCategory ,admissionDate, admissionClass, lastClass,lastExamTaken, lastExamResult, Failed, subjectsStudied, promoted, promotedClass, feeMonth, "
            sqlstr += " feeConcession,workDays, presentDays, NCC, corricular, conduct, dateOFApplication, dateOfIssue,Reason, remarks,isIssued,IsCanceled) Values("
            sqlstr += "'" & SQLFixup(TCno) & "',"
            sqlstr += "" & SQLFixup(txtSID.Text) & ","
            sqlstr += "'" & DOBInWords & "',"
            sqlstr += "'" & SQLFixup(txtCategory.Text) & "',"
            sqlstr += "'" & txtDateAdmission.Text.Substring(6, 4) & "/" & txtDateAdmission.Text.Substring(3, 2) & "/" & txtDateAdmission.Text.Substring(0, 2) & "',"
            sqlstr += "'" & ddlSCST.Text & "',"
            sqlstr += "'" & cboLastClass.Text & "',"
            sqlstr += "'" & cboLastExamTaken.Text & "',"
            sqlstr += "'" & cboLastClassResult.Text & "',"
            sqlstr += "'" & SQLFixup(txtFailed.Text) & "',"
            sqlstr += "'" & SQLFixup(txtSubjects.Text) & "',"
            sqlstr += "'" & ddlPromoted.Text & "',"
            sqlstr += "'" & cboToClass.Text & "',"
            sqlstr += "'" & cboMonth.Text & "',"
            sqlstr += "'" & SQLFixup(txtFeeCon.Text) & "',"
            sqlstr += "'" & SQLFixup(txtWorkDays.Text) & "',"
            sqlstr += "'" & SQLFixup(txtSchoolDays.Text) & "',"
            sqlstr += "'" & SQLFixup(txtNCC.Text) & "',"
            sqlstr += "'" & SQLFixup(txtExtraCorr.Text) & "',"
            sqlstr += "'" & cboCharacter.Text & "',"
            sqlstr += "'" & txtApplicationDate.Text.Substring(6, 4) & "/" & txtApplicationDate.Text.Substring(3, 2) & "/" & txtApplicationDate.Text.Substring(0, 2) & "',"
            sqlstr += "'" & txtIssuedDate.Text.Substring(6, 4) & "/" & txtIssuedDate.Text.Substring(3, 2) & "/" & txtIssuedDate.Text.Substring(0, 2) & "',"
            sqlstr += "'" & cboreason.Text & "',"
            sqlstr += "N'" & SQLFixup(txtremark.Text) & "',1,0)"
            ExecuteQuery_Update(sqlstr)

            Dim TCID As String = ExecuteQuery_ExecuteScalar("Select MAX(TCID) From vw_StudentTC where ASID='" & Request.Cookies("ASID").Value & "'")

            UpdateGridData(TCID)

            ''Insert Data Student Details In TC Details 
            'For gv As Integer = 0 To GridView1.Rows.Count - 1
            '    Dim txtPromotionDate As TextBox = DirectCast(GridView1.Rows(gv).FindControl("txtPromotionDate"), TextBox)
            '    Dim txtEndDate As TextBox = DirectCast(GridView1.Rows(gv).FindControl("txtEndDate"), TextBox)
            '    Dim cboConduct As DropDownList = DirectCast(GridView1.Rows(gv).FindControl("cboConduct"), DropDownList)
            '    Dim txtRemark As TextBox = DirectCast(GridView1.Rows(gv).FindControl("txtRemark"), TextBox)
            '    Dim SID As Integer = GridView1.DataKeys(gv).Value
            '    Dim PromotionDate As Date = DateTime.Now.Date
            '    Dim EndDate As Date = DateTime.Now.Date
            '    Try
            '        PromotionDate = txtPromotionDate.Text.Substring(6, 4) & "/" & txtPromotionDate.Text.Substring(3, 2) & "/" & txtPromotionDate.Text.Substring(0, 2)
            '    Catch ex As Exception

            '    End Try
            '    Try
            '        EndDate = txtEndDate.Text.Substring(6, 4) & "/" & txtEndDate.Text.Substring(3, 2) & "/" & txtEndDate.Text.Substring(0, 2)
            '    Catch ex As Exception

            '    End Try

            '    sqlstr = "Insert into TCDetails (SID, Conduct, Remark, PromotionDate, EndDate,TCID) Values(" & SID & ",'" & cboConduct.SelectedItem.Text & "','" & txtRemark.Text & "','" & PromotionDate.ToString("yyyy/MM/dd") & "', '" & EndDate.AddDays(-1).ToString("yyyy/MM/dd") & "'," & TCID & ")"
            '    ExecuteQuery_Update(sqlstr)
            'Next

            GetStudentID("0")
            PrepareReport(TCno, 1)
        End If
        If chkWholeClass.Checked = False Then
            UpdateRollNo(txtSID.Text, txtCSSID.Text)
        Else


        End If
    End Sub

    Private Sub GetStudentID(ByVal status As String)
        Dim ASID As String = Request.Cookies("ASID").Value

        Dim sqlstr As String = ""


        sqlstr = "Select * from vw_Student where Regno='" & txtSRNo.Text & "' AND ASID=" & ASID



        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        Dim StudentID As String = Nothing
        While myReader.Read
            StudentID = myReader("StudentID")
        End While
        myReader.Close()


        If status = "0" Then
            sqlstr = "Update StudentBasicInfo Set "
            sqlstr += "StatusID=1002"
            sqlstr += " Where StudentID='" & StudentID & "'"
        Else
            sqlstr = "Update StudentBasicInfo Set "
            sqlstr += "StatusID=1"
            sqlstr += " Where StudentID='" & StudentID & "'"
        End If
        ExecuteQuery_Update(sqlstr)
    End Sub
    Private Sub UpdateRollNo(Sid As Integer, CSSID As Integer, Optional StatusID As Integer = 1)
        Dim sqlStr As String = ""




        Dim sidRollNo As Integer = 0
        Dim maxRollNo As Integer = 0
        sqlStr = "Select ClassRollno from vw_Student where statusID=" & StatusID & " AND sid=" & Sid


        Try
            sidRollNo = ExecuteQuery_ExecuteScalar(sqlStr)
        Catch ex As Exception

        End Try

        sqlStr = "Select max(convert(int,classrollno)) as classrollno from vw_Student where statusID=" & StatusID & " AND CSSID=" & CSSID


        Try
            maxRollNo = ExecuteQuery_ExecuteScalar(sqlStr)
        Catch ex As Exception

        End Try

        For i = sidRollNo To maxRollNo
            sqlStr = "UPDATE Student set classrollno='" & i & "' Where ClassRollNo='" & i + 1 & "' AND CSSID=" & CSSID


            ExecuteQuery_Update(SqlStr)
        Next



    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        InitControls()
    End Sub

    Public Function checkClassEntry()
        Dim rv As Integer = 0
        Dim sqlstr As String = ""
        sqlstr = "Select Count(*) from TCDetails where Regno='" + txtSRNo.Text + "'"
        rv = ExecuteQuery_ExecuteScalar(sqlstr)
        Return rv
    End Function

    Public Function doEntry(TCID As String)
        Dim ClassList() As Integer = {6, 7, 8, 9, 10, 11, 12}
        Dim rv As Integer = 0
        Dim sqlstr As String = ""
        'for class 
        For i As Integer = 0 To ClassList.Count - 1
1:
            sqlstr = "Select Count(*) from TCDetails where ClassName='" & ClassList(i) & "' and Regno='" & txtSRNo.Text & "' and TempID='" & RandomNo & "'"
            rv = ExecuteQuery_ExecuteScalar(sqlstr)
            If rv <> 3 Then
                sqlstr = "Insert into TCDetails (TempID,ClassName,Regno,TCID) Values('" & RandomNo & "','" & ClassList(i) & "','" & txtSRNo.Text & "','" & TCID & "')"
                ExecuteQuery_Update(sqlstr)
                GoTo 1
            End If
        Next

        Return rv
    End Function

    Private Sub PrepareReport(ByVal TCNo As String, type As Integer)
        Dim ASID As String = Request.Cookies("ASID").Value
        'Dim P1 As String = "", P2 As String = "", P3 As String = ""
        'Dim P4 As String = "", P5 As String = "", P6 As String = ""
        Dim sql As String = ""
        '................................................vikash..........17/06/2016.........................................
        Dim sqlstr As String = ""
        sqlstr = "Select * from Params"
        Dim SchoolName As String = ""
        Dim Address As String = ""
        Dim SchoolType As Integer = 0
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)
        Dim ReportPath As String = ""
        While myReader.Read
            SchoolName = myReader("SchoolName")
            Address = myReader("SchoolDetails")
            SchoolType = myReader("SchoolType")
        End While
        myReader.Close()
        sql = "Select SchoolHeaderImage from vw_StudentTC where TCNo = '" & TCNo & "' order by SID"
        Dim SchoolImage = ""
        myReader = ExecuteQuery_ExecuteReader(sql)
        While myReader.Read
            SchoolImage = myReader(0)
        End While
        myReader.Close()
        If SchoolType = 1 Then
            ReportPath = "Report\rptTC.rdlc"
            If type = 1 Then
                sql = "Select * from vw_StudentTC where TCNo = '" & TCNo & "' order by SID"
            ElseIf type = 2 Then
                sql = "Select * From vw_StudentTC Where ClassName=N'" & cboClass.Text & "' And SecName = N'" & cboSection.Text & "' AND ASID=" & ASID & "  order by SID"
            End If

        Else
            ReportPath = "Report\rptTCICSE.rdlc"
            If type = 1 Then
                sql = "Select SName,admissionDate as DOA,ClassName as ClassSecName,dateofIssue as LeftDate,conduct as CharName,ClassName as PromotedToClass,FName,DOB,TCNo,RegNo as AdminNo from vw_StudentTC where TCNo = '" & TCNo & "' order by SID"
            ElseIf type = 2 Then
                sql = "Select  SName,admissionDate as DOA,ClassName as ClassSecName,dateofIssue as LeftDate,conduct as CharName,ClassName as PromotedToClass,FName,DOB,TCNo,RegNo as AdminNo From vw_StudentTC Where ClassName=N'" & cboClass.Text & "' And SecName = N'" & cboSection.Text & "' AND ASID=" & ASID & "  order by SID"
            End If
        End If
        '.........................................................................................................................

        Dim ds As New DataSet
        ds = ExecuteQuery_DataSet(sql, "tbl")

        'Check data entered of each class exists or not 
        'Set Count to 21 because of TC contains 7 classes, so its count is 21
        If Request.QueryString("TCNo") = "" Then
            If checkClassEntry() <> 21 Then
                doEntry(ds.Tables(0).Rows(0)("TCID"))
            End If
        End If
        
        'Get TC Details
        sqlstr = "Select * from TCDetails where TCID =" & ds.Tables(0).Rows(0)("TCID")
        Dim TCDetailsds As DataSet = ExecuteQuery_DataSet(sqlstr, "tbl")


        Dim rds As ReportDataSource = New ReportDataSource()
        rds.Name = "rdOldData" ' Change to what you will be using when creating an objectdatasource
        rds.Value = ds.Tables(0)

        Dim rdOldData As New ReportDataSource("DataSet1", TCDetailsds.Tables(0))

        With ReportViewer1   ' Name of the report control on the form
            .Reset()
            .ProcessingMode = ProcessingMode.Local
            .LocalReport.DataSources.Clear()
            .Visible = True
            .LocalReport.ReportPath = ReportPath
            .LocalReport.DataSources.Add(rds)
            .LocalReport.DataSources.Add(rdOldData)
            .LocalReport.EnableExternalImages = True


            'If you have any parameters you can pass them here
            Dim rptParameters As New List(Of ReportParameter)()
            'With rptParameters
            '    .Add(New ReportParameter("Title",
            '      "First Parameter"))
            '    '.Add(New ReportParameter("Title",
            '    '    String.Format("{0} Grid For Period {1} To {2}", gridType, FormatDateTime(startDate, DateFormat.ShortDate),
            '    '        FormatDateTime(endDate, DateFormat.ShortDate))))
            '    '.Add(New ReportParameter("SubTitle", String.Format("Program: {0}", ucProgram.Text)))
            '    '.Add(New ReportParameter("StartDate", startDate))
            '    '.Add(New ReportParameter("EndDate", endDate))
            'End With
            '.LocalReport.SetParameters(rptParameters)

        End With
        ReportViewer1.Visible = True
        ReportViewer1.LocalReport.Refresh()
        'P1 = UCase(txtSName.Text)

        'P2 = "son of " & txtFName.Text & " was admitted into this School on " & txtAdmissionDate.Text

        'P3 = "and left on " & txtLeftDD.Text & "/" & txtLeftMM.Text & "/" & txtLeftYY.Text & _
        '" with a " & UCase(cboCharacter.Text) & " character."

        'P4 = "He was then studying in the class " & txtClass.Text & "."

        'P5 = "His date of birth, according to the Admission Register is (in figures) " & txtDOB.Text & " (in words) " & txtDOBInWords.Text & "."

        'P6 = cboPromotedToClass.Text

        Dim params(2) As Microsoft.Reporting.WebForms.ReportParameter
        params(0) = New Microsoft.Reporting.WebForms.ReportParameter("headerPath", Server.MapPath(SchoolImage), Visible)
        params(1) = New Microsoft.Reporting.WebForms.ReportParameter("SchoolName", SchoolName, True)
        params(2) = New Microsoft.Reporting.WebForms.ReportParameter("SchoolAddress", Address, True)
        'params(3) = New Microsoft.Reporting.WebForms.ReportParameter("DOBWords", lblDOB.Text, True)
        'params(4) = New Microsoft.Reporting.WebForms.ReportParameter("P4", P4, True)
        'params(5) = New Microsoft.Reporting.WebForms.ReportParameter("P5", P5, True)
        Me.ReportViewer1.LocalReport.SetParameters(params)
        ReportViewer1.Visible = True
        ReportViewer1.LocalReport.Refresh()

        'ReportViewer1.LocalReport.Refresh()

    End Sub
    Protected Sub GridView2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView2.SelectedIndexChanged
        ShowStudentRecord(2, GridView2.SelectedRow.Cells(2).Text)
        GridView2.Visible = False
    End Sub
    Private Sub ShowStudentRecord(ByVal SearchType As Integer, SearchVal As String)
        InitControls()
        Dim ASID As String = Request.Cookies("ASID").Value

        Dim sqlstr As String = ""


        '------Load Personal Information--------
        If SearchType = 1 Then
            sqlstr = "Select * From vw_Student Where RegNo=N'" & SearchVal & "' AND ASID=" & ASID
        ElseIf SearchType = 2 Then
            sqlstr = "Select * From vw_Student Where SName=N'" & SearchVal & "' AND ASID=" & ASID
        End If



        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)

        While myReader.Read
            txtSRNo.Text = myReader("RegNo")
            txtName.Text = myReader("SName")
            Dim a As Date = myReader("DOB").ToString
            txtDOB.Text = a.ToString("dd/MM/yyyy")
            Try
                a = myReader("PromotionDate")
                lblPromotionDate.Text = a.ToString("dd/MM/yyyy")
            Catch ex As Exception

            End Try
            
            txtFName.Text = myReader("FName")
            txtMName.Text = myReader("MName")
            txtClassSection.Text = myReader("ClassName") & "/" & myReader("SecName")
            txtClassName.Text = myReader("ClassName")
            txtSecName.Text = myReader("SecName")
            txtSID.Text = myReader("SID")
            a = myReader("AdmissionDate")
            txtDateAdmission.Text = a.ToString("dd/MM/yyyy")
            txtCSSID.Text = myReader("CSSID")
            txtCategory.Text = myReader("CategoryName")
            Try
                cboSchoolName.Text = myReader("SchoolName")
            Catch ex As Exception

            End Try

            'txtCategory.Text = myReader("CategoryName")
        End While
        myReader.Close()

        If txtSRNo.Text = "" Then

        Else
            'Insert Data into TC Details
            sqlstr = "SELECT [SID], [RegNo], [ClassName], [PromotionDate], [AdmissionDate], [ASName] FROM [vw_Student] where Regno = '" & txtSRNo.Text & "'"
            myReader = ExecuteQuery_ExecuteReader(sqlstr)
            While (myReader.Read())
                sqlstr = "insert into TCDetails (Regno, ClassName, TempID, ASession) Values('" & myReader("Regno").ToString() & "', '" & myReader("ClassName").ToString() & "', '" & RandomNo & "','" & myReader("ASName") & "')"
                ExecuteQuery_Update(sqlstr)
            End While
            myReader.Close()

            Bindgridview()

            'For i As Integer = 0 To GridView1.Rows.Count - 1
            '    sqlstr = "insert into TCDetails (SID, TempID) Values('" & GridView1.DataKeys(i).Values(0) & "', '" & RandomNo & "')"
            '    ExecuteQuery_Update(sqlstr)
            'Next
        End If
        

        'sqlstr = "select SubjectName from vwsubjectmapping where classname='" & txtClassName.Text & "' AND secname='" & txtSecName.Text & "'"
        '
        '
        ''Dim MyReader As SqlDataReader = myCommand1.ExecuteReader
        'myReader = ExecuteQuery_ExecuteReader(sqlStr)
        'Dim count As Integer = 1
        'Dim subjects As String = ""
        'While myReader.Read
        '    subjects += count & ". " & myReader(0) & "    "
        '    count += 1
        'End While
        'myReader.Close()

        'txtSubjects.Text = subjects
    End Sub
    Protected Sub chkWholeClass_CheckedChanged(sender As Object, e As EventArgs) Handles chkWholeClass.CheckedChanged
        If chkWholeClass.Checked = True Then
            cboClass.Visible = True
            lblSection.Visible = True
            cboSection.Visible = True
            btnSRNoNext.Enabled = False
            btnNameNext.Enabled = False
            txtSRNo.Enabled = False
            txtName.Enabled = False
            cboClass.Focus()
        Else
            cboClass.Visible = False
            lblSection.Visible = False
            cboSection.Visible = False
            btnSRNoNext.Enabled = True
            btnNameNext.Enabled = True
            txtSRNo.Enabled = True
            txtName.Enabled = True
            txtSRNo.Focus()
        End If
    End Sub

    Protected Sub cboClass_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboClass.SelectedIndexChanged
        LoadClassSection(cboSchoolName.Text, cboClass.Text, cboSection)
        cboSection.Focus()
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click



        If Trim(txtSno.Text = "") Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert(Enter TC No. to cancel...');", True)
            txtSno.Focus()
            Exit Sub
        End If


        Dim sqlstr As String = "update TC set isIssued=0 where TCNo='" & txtSno.Text & "'"




        ExecuteQuery_Update(SqlStr)




    End Sub
    Private Sub GetTCDetails()




        Dim sqlstr As String = ""

        sqlstr = "Select * From vw_StudentTC Where TCNo='" & txtSno.Text & "'"




        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)

        While myReader.Read
            chkCancel.Enabled = True
            Dim Canceled As String = myReader("IsCanceled")
            If Canceled = "1" Then
                chkCancel.Text = "Yes"
            Else
                chkCancel.Text = "No"
            End If
            Try
                cboSchoolName.Text = myReader("SchoolName")
            Catch ex As Exception

            End Try
            txtCategory.Text = myReader("CategoryName")
            txtSRNo.Text = myReader("RegNo")
            txtSID.Text = myReader("SID")
            txtName.Text = myReader("SName")
            txtFName.Text = myReader("FName")
            txtMName.Text = myReader("MName")
            Dim a As Date = myReader("DOB").ToString
            txtDOB.Text = a.ToString("dd/MM/yyyy")
            txtClassSection.Text = myReader("ClassName") & "/" & myReader("SecName")
            a = myReader("AdmissionDate")
            txtDateAdmission.Text = a.ToString("dd/MM/yyyy")
            Try
                cboLastClass.SelectedItem.Text = myReader("lastClass")
            Catch ex As Exception

            End Try

            cboLastExamTaken.Text = myReader("lastExamTaken")
            Try
                cboLastClassResult.Text = myReader("lastExamResult")
            Catch ex As Exception

            End Try

            Try
                txtFailed.Text = myReader("Failed")
            Catch ex As Exception

            End Try
            txtSubjects.Text = myReader("subjectsStudied")
            Try
                ddlPromoted.SelectedItem.Text = myReader("promoted")
            Catch ex As Exception

            End Try

            Dim PromotedClass As String = myReader("promotedClass")
            Dim PClass As String() = PromotedClass.Split("-")
            Try
                cboToClass.Text = PromotedClass
            Catch ex As Exception

            End Try
            cboMonth.Text = myReader("feeMonth")
            txtFeeCon.Text = myReader("feeConcession")
            cboreason.Text = myReader("Reason")
            txtWorkDays.Text = myReader("workDays")
            txtSchoolDays.Text = myReader("presentDays")
            txtNCC.Text = myReader("NCC")
            txtExtraCorr.Text = myReader("corricular")
            cboCharacter.Text = (myReader("conduct"))
            a = myReader("dateOFApplication")
            txtApplicationDate.Text = a.ToString("dd/MM/yyyy")
            Try
                txtremark.Text = myReader("remarks")
            Catch ex As Exception

            End Try
        End While

        myReader.Close()


    End Sub
    Private Function CheckTC()

        Dim sqlStr As String = "Select Count(*) From TC Where SID='" & txtSID.Text & "'"

        Dim rv As Integer = ExecuteQuery_ExecuteScalar(SqlStr)


        If rv > 0 Then
            Return True
        Else
            Return False
        End If


        Return rv
    End Function

    Protected Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        'If Trim(cboSession.Text) = "" Then
        '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert(Please choose Academic Year first...');", True)
        '    cboSession.Focus()
        '    Exit Sub
        'End If
        Dim Canceled As String = Nothing
        If chkCancel.SelectedIndex = 0 Then
            Canceled = "1"
            GetStudentID(Canceled)
        Else
            Canceled = "0"
        End If


        Dim sqlstr As String = ""
        Dim DOBInWords As String = ""
        DOBInWords = ConvertDateToWords(txtDOB.Text.Substring(0, 2), txtDOB.Text.Substring(3, 2), txtDOB.Text.Substring(6, 4))

        sqlstr = "Update TC SET DOBInWords='" & DOBInWords & "',"
        sqlstr += "studentCategory='" & ddlSCST.Text & "',"
        sqlstr += "admissionDate='" & txtDateAdmission.Text.Substring(6, 4) & "/" & txtDateAdmission.Text.Substring(3, 2) & "/" & txtDateAdmission.Text.Substring(0, 2) & "',"
        'sqlstr += "admissionClass='" & txtClass.Text & "',"
        sqlstr += "lastClass='" & cboLastClass.Text & "',"
        sqlstr += "lastExamTaken='" & cboLastExamTaken.Text & "',"
        sqlstr += "lastExamResult='" & cboLastClassResult.Text & "',"
        sqlstr += "Failed='" & txtFailed.Text & "',"
        sqlstr += "subjectsStudied='" & txtSubjects.Text & "',"
        sqlstr += "promoted='" & ddlPromoted.Text & "',"
        sqlstr += "promotedClass='" & cboToClass.Text & "',"
        sqlstr += "feeMonth='" & cboMonth.Text & "',"
        sqlstr += "feeConcession='" & txtFeeCon.Text & "',"
        sqlstr += "workDays='" & txtWorkDays.Text & "',"
        sqlstr += "presentDays='" & txtSchoolDays.Text & "',"
        sqlstr += "NCC='" & txtNCC.Text & "',"
        sqlstr += "corricular='" & txtExtraCorr.Text & "',"
        sqlstr += "conduct='" & cboCharacter.Text & "',"
        sqlstr += "dateOFApplication='" & txtApplicationDate.Text.Substring(6, 4) & "/" & txtApplicationDate.Text.Substring(3, 2) & "/" & txtApplicationDate.Text.Substring(0, 2) & "',"
        sqlstr += "dateOfIssue='" & txtIssuedDate.Text.Substring(6, 4) & "/" & txtIssuedDate.Text.Substring(3, 2) & "/" & txtIssuedDate.Text.Substring(0, 2) & "',"
        sqlstr += "Reason='" & cboreason.Text & "',"
        sqlstr += "remarks='" & txtremark.Text & "',"
        sqlstr += "IsCanceled='" & Canceled & "'"
        sqlstr += " Where TCno='" & txtSno.Text & "' AND SID=" & txtSID.Text

        ExecuteQuery_Update(sqlstr)

        ''Insert Data Student Details In TC Details 
        'For gv As Integer = 0 To GridView1.Rows.Count - 1
        '    Dim cboConduct As DropDownList = DirectCast(GridView1.Rows(gv).FindControl("cboConduct"), DropDownList)
        '    Dim txtRemark As TextBox = DirectCast(GridView1.Rows(gv).FindControl("txtRemark"), TextBox)
        '    Dim SID As Integer = GridView1.DataKeys(gv).Value
        '    Dim PromotionDate As Date = DateTime.Now.Date
        '    Dim EndDate As Date = DateTime.Now.Date
        '    Try
        '        If gv = GridView1.Rows.Count - 1 Then
        '            EndDate = txtApplicationDate.Text
        '        Else
        '            EndDate = GridView1.DataKeys(gv).Value(1)
        '        End If

        '    Catch ex As Exception

        '    End Try
        '    Try
        '        PromotionDate = GridView1.Rows(gv).Cells(3).Text
        '    Catch ex As Exception
        '        'If Promotion Date is null then Assigning Admission Date
        '        'PromotionDate = GridView1.DataKeys(gv).Values(1)
        '        If gv = 0 Then
        '            PromotionDate = txtDateAdmission.Text
        '            EndDate = lblPromotionDate.Text
        '        End If
        '    End Try

        '    sqlstr = "Insert into TCDetails (SID, Conduct, Remark, PromotionDate, EndDate) Values(" & SID & ",'" & cboConduct.SelectedItem.Text & "','" & txtRemark.Text & "','" & PromotionDate.ToString("yyyy/MM/dd") & "', '" & EndDate.AddDays(-1).ToString("yyyy/MM/dd") & "')"
        '    ExecuteQuery_Update(sqlstr)
        'Next

        PrepareReport(txtSno.Text, 1)
    End Sub

    Protected Sub btnSRNoNext_Click(sender As Object, e As EventArgs) Handles btnSRNoNext.Click

        ShowStudentRecord(1, txtSRNo.Text)
        txtApplicationDate.Focus()
    End Sub

    Protected Sub btnSnoNext_Click(sender As Object, e As EventArgs) Handles btnSnoNext.Click
        'If Trim(cboSession.Text) = "" Then
        '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please choose Academic Year first...');", True)
        '    cboSession.Focus()
        '    Exit Sub
        'End If
        If CheckCertificateSno(txtSno.Text, 3) = False Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Tc. No Does Not Exists...');", True)
            txtSno.Text = ""
            txtSno.Focus()
            InitControls()
            Exit Sub
        Else
            PrepareReport(txtSno.Text, 1)
            GetTCDetails()
            chkCancel.Enabled = True
        End If
    End Sub

    Protected Sub btnNameNext_Click1(sender As Object, e As EventArgs) Handles btnNameNext.Click
        'If cboSession.Text = "" Then
        '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please choose Academic Year first...');", True)
        '    cboSession.Focus()
        '    Exit Sub
        'End If
        Dim ASID As String = Request.Cookies("ASID").Value
        SqlDataSource2.SelectCommand = "SELECT RegNo, SName, ClassName, SecName FROM vw_Student WHERE ASID = " & ASID & " AND SName Like '" & txtName.Text & "%' "
        GridView2.DataBind()
        GridView2.Visible = True

    End Sub

    Protected Sub ddlPromoted_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPromoted.SelectedIndexChanged
        If ddlPromoted.SelectedItem.Text = "Yes" Then
            cboToClass.Enabled = True
        Else
            cboToClass.Enabled = False
        End If

    End Sub

    Protected Sub cboLastExamTaken_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboLastExamTaken.SelectedIndexChanged
        If cboLastExamTaken.SelectedItem.Text = "NA" Then
            cboLastClassResult.Enabled = False
        Else
            cboLastClassResult.Enabled = True
        End If
    End Sub

    Protected Sub cboSchoolName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSchoolName.SelectedIndexChanged
        LoadMasterInfo(2, cboClass, cboSchoolName.Text)
        'cboClass.Items.Add("ALL")
        cboSchoolName.Focus()
    End Sub

    Dim ds As New DataTable

    Protected Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            Dim cboConduct As DropDownList = CType(e.Row.FindControl("cboConduct"), DropDownList)
            LoadMasterInfo(15, cboConduct)

            Dim txtPromotionDate As TextBox = CType(e.Row.FindControl("txtPromotionDate"), TextBox)
            Dim txtEndDate As TextBox = CType(e.Row.FindControl("txtEndDate"), TextBox)
            Dim txtRemark As TextBox = CType(e.Row.FindControl("txtRemark"), TextBox)
            Dim txtRemoval As TextBox = CType(e.Row.FindControl("txtRemoval"), TextBox)
            Dim cboRemoval As DropDownList = CType(e.Row.FindControl("cboRemoval"), DropDownList)

            Dim ASName As TableCell = e.Row.Cells(2)

            Dim TCDetailsID As String = GridView1.DataKeys(e.Row.RowIndex).Value
            Dim sqlstr As String = "Select Conduct, Remark,PromotionDate,Enddate,ASession,DateOfRemoval from TCDetails where TempID='" & RandomNo & "' and TCDetailsID='" & TCDetailsID & "'"
            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)
            While myReader.Read()
                Try
                    txtPromotionDate.Text = Convert.ToDateTime(myReader("PromotionDate")).ToString("dd/MM/yyyy")
                Catch ex As Exception

                End Try
                Try
                    txtEndDate.Text = Convert.ToDateTime(myReader("Enddate")).ToString("dd/MM/yyyy")
                Catch ex As Exception

                End Try
                txtRemark.Text = myReader("Remark").ToString()
                cboConduct.ClearSelection()
                Try
                    cboConduct.Items.FindByText(myReader("Conduct").ToString()).Selected = True
                Catch ex As Exception

                End Try
                ASName.Text = myReader("ASession").ToString()
                Try
                    txtRemoval.Text = Convert.ToDateTime(myReader("DateOfRemoval")).ToString("dd/MM/yyyy")
                Catch ex As Exception

                End Try
                cboRemoval.ClearSelection()
                Try
                    cboRemoval.Items.FindByText(myReader("cboRemoval").ToString()).Selected = True
                Catch ex As Exception

                End Try
            End While
            myReader.Close()
        End If
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        'If GridView1.Rows.Count > 0 Then
        '    lblCount.Text = "1"
        'End If
        'If (lblCount.Text = "0") Then
        '    'Add Columns
        '    ds.Columns.Add("SID", GetType(String))
        '    ds.Columns.Add("RegNo", GetType(String))
        '    ds.Columns.Add("ClassName", GetType(String))

        '    'Add New Rows
        '    Dim newRow1 As DataRow = ds.NewRow()
        '    newRow1("SID") = ""
        '    newRow1("RegNo") = ""
        '    newRow1("ClassName") = ""

        '    ds.Rows.Add(newRow1)
        '    GridView1.DataSource = ds
        '    GridView1.DataBind()
        '    lblCount.Text = "1"
        '    ViewState("tbl") = ds

        'Else

        '    ds = TryCast(ViewState("tbl"), DataTable)
        '    'Check Duplicate Qualification
        '    For i As Integer = 0 To ds.Rows.Count - 1
        '        If ds.Rows(i)("ClassName") Then

        '        End If
        '    Next
        '    'Add New Rows
        '    Dim newRow1 As DataRow = ds.NewRow()
        '    newRow1("SID") = ""
        '    newRow1("RegNo") = ""
        '    newRow1("ClassName") = ""

        '    ds.Rows.Add(newRow1)
        '    GridView1.DataSource = ds
        '    GridView1.DataBind()

        'End If
        If txtSno.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Enter Registration No. to continue...');", True)
            txtSno.Focus()
            Exit Sub
        End If

        Dim sqlstr As String = ""
        'sqlstr = "Delete from TCDetails where TempID='" & RandomNo & "'"
        'ExecuteQuery_Update(sqlstr)
        Dim PromotionDate As Date = DateTime.Now.Date
        Dim EndDate As Date = DateTime.Now.Date
        Dim RemovalDate As Date = DateTime.Now.Date
        Try
            PromotionDate = txtProDate.Text.Substring(6, 4) & "/" & txtProDate.Text.Substring(3, 2) & "/" & txtProDate.Text.Substring(0, 2)
        Catch ex As Exception

        End Try
        Try
            EndDate = txtEndDate1.Text.Substring(6, 4) & "/" & txtEndDate1.Text.Substring(3, 2) & "/" & txtEndDate1.Text.Substring(0, 2)
        Catch ex As Exception

        End Try
        Try
            RemovalDate = txtDateOfRemoval.Text.Substring(6, 4) & "/" & txtDateOfRemoval.Text.Substring(3, 2) & "/" & txtDateOfRemoval.Text.Substring(0, 2)
        Catch ex As Exception

        End Try

        sqlstr = "Insert into TCDetails (Regno,Conduct,Remark,PromotionDate,EndDate,ASession,TempID,ClassName,DateOfRemoval,ReasonOfRemoval) Values('" & txtSRNo.Text & "'," & _
        "'" & cboConduct.SelectedItem.Text & "'," & _
        "'" & txtWork.Text & "'," & _
        "'" & PromotionDate.ToString("yyyy/MM/dd") & "'," & _
        "'" & EndDate.ToString("yyyy/MM/dd") & "'," & _
        "'" & txtSession.Text & "'," & _
        "'" & RandomNo & "'," & _
        "'" & cboClasses.SelectedItem.Text & "',"
        If (txtDateOfRemoval.Text = "") Then
            sqlstr &= "NULL,"
        Else
            sqlstr &= "'" & RemovalDate.ToString("yyyy/MM/dd") & "',"
        End If
        sqlstr &= "'" & cboRemoval.SelectedItem.Text & "')"

        ExecuteQuery_Update(sqlstr)
        'TCID Pending Update At Last
        'UpdateGridData()
        Bindgridview()
    End Sub

    Public Sub UpdateGridData(Optional TCID As String = "")
        Dim sqlstr As String = ""
        sqlstr = "Delete from TCDetails where TempID='" & RandomNo & "'"
        ExecuteQuery_Update(sqlstr)
        For i As Integer = 0 To GridView1.Rows.Count - 1
            Dim ClassName As String = GridView1.Rows(i).Cells(1).Text
            Dim ASSession As String = GridView1.Rows(i).Cells(2).Text
            Dim txtPromotionDate As TextBox = DirectCast(GridView1.Rows(i).FindControl("txtPromotionDate"), TextBox)
            Dim txtEndDate As TextBox = DirectCast(GridView1.Rows(i).FindControl("txtEndDate"), TextBox)
            Dim cboConduct As DropDownList = DirectCast(GridView1.Rows(i).FindControl("cboConduct"), DropDownList)
            Dim txtRemark As TextBox = DirectCast(GridView1.Rows(i).FindControl("txtRemark"), TextBox)
            Dim txtRemoval As TextBox = DirectCast(GridView1.Rows(i).FindControl("txtRemoval"), TextBox)
            Dim cboRemoval As DropDownList = DirectCast(GridView1.Rows(i).FindControl("cboRemoval"), DropDownList)

            Dim PromotionDate As Date = DateTime.Now.Date
            Dim EndDate As Date = DateTime.Now.Date
            Dim RemovalDate As Date = DateTime.Now.Date
            Try
                PromotionDate = txtPromotionDate.Text.Substring(6, 4) & "/" & txtPromotionDate.Text.Substring(3, 2) & "/" & txtPromotionDate.Text.Substring(0, 2)
            Catch ex As Exception

            End Try
            Try
                EndDate = txtEndDate.Text.Substring(6, 4) & "/" & txtEndDate.Text.Substring(3, 2) & "/" & txtEndDate.Text.Substring(0, 2)
            Catch ex As Exception

            End Try
            Try
                RemovalDate = txtRemoval.Text.Substring(6, 4) & "/" & txtRemoval.Text.Substring(3, 2) & "/" & txtRemoval.Text.Substring(0, 2)
            Catch ex As Exception

            End Try

            sqlstr = "Insert into TCDetails (Regno,ClassName,Conduct,Remark,PromotionDate,EndDate,TCID,ASession,TempID,DateOfRemoval,ReasonOfRemoval) Values('" & txtSRNo.Text & "'," & _
                "'" & ClassName & "'," & _
                "'" & cboConduct.SelectedItem.Text & "'," & _
        "'" & txtRemark.Text & "'," & _
        "'" & PromotionDate.ToString("yyyy/MM/dd") & "'," & _
        "'" & EndDate.ToString("yyyy/MM/dd") & "'," & _
        "'" & TCID & "'," & _
        "'" & ASSession & "'," & _
        "'" & RandomNo & "',"
            If (txtRemoval.Text = "") Then
                sqlstr &= "NULL,"
            Else

                sqlstr &= "'" & RemovalDate.ToString("yyyy/MM/dd") & "',"
            End If

            sqlstr &= "'" & cboRemoval.SelectedItem.Text & "')"

            ExecuteQuery_Update(sqlstr)
        Next
    End Sub

    Public Sub Bindgridview()
        Dim sqlstr As String = "SELECT [TCDetailsID], [ClassName], [Conduct], [Remark], [PromotionDate], [EndDate], [ASession], [DateOfRemoval], [ReasonOfRemoval] from TCDetails where Regno = '" & txtSRNo.Text & "' and TempID='" & RandomNo & "'"
        Dim ds As DataSet = ExecuteQuery_DataSet(sqlstr, "tbl")
        GridView1.DataSource = ds.Tables(0)
        GridView1.DataBind()
    End Sub

    Public Shared RandomNo As String = ""

    Public Function getRandomNo() As String
        Dim i As String = ""
        Dim g As Guid = Guid.NewGuid()
        Dim random As String = g.ToString()
        Dim s As String = random.Replace("-", "")
        i = s.Substring(0, 6)
        Return i
    End Function
End Class
