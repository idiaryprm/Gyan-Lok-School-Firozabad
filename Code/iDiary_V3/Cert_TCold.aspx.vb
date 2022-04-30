Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
'Imports iDiary_V3.iDiary_Student.CLS_iDiary_Student
Imports iDiary_V3.iDiary_Date.CLS_iDiary_Date
Imports Microsoft.Reporting.WebForms

Partial Class Cert_TCold
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then InitControls()
    End Sub
    Private Sub LoadSession(ByRef mycbo As DropDownList)
       
       
       

        Dim sqlStr As String = "Select Distinct ASID From oldtcdata order by ASID"
        
        
        
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        mycbo.Items.Clear()

        While myReader.Read
            mycbo.Items.Add(myReader(0))
        End While
        myReader.Close()
        
        
    End Sub
    Private Sub InitControls()
        ReportViewer1.Visible = False
        txtSno.Text = Now.Date.Year & "/" & FindNextSno(Now.Date.Year, 2)
        'cboClass.Items.Clear()
        chkWholeClass.Checked = False
        LoadMasterInfo(2, cboClass)
        txtSRNo.Text = ""
        txtDateDropOut.Text = Now.Date.ToString("dd/MM/yyyy")
        txtDOB.Text = ""
        txtName.Text = ""
        txtFName.Text = ""
        txtMName.Text = ""
        cboLastExamTaken.SelectedIndex = 0
        cboLastClassResult.SelectedIndex = 0
        txtSubjects.Text = ""
        txtFailed.Text = ""
        txtClassSection.Text = ""
        txtDateAdmission.Text = ""
        ddlPromoted.SelectedIndex = 0
        cboLastClass0.SelectedIndex = 0
        txtSID.Text = ""
        txtApplyDate.Text = Now.Date.ToString("dd/MM/yyyy")
        ' txtLeaveReason.Text = ""
        LoadMasterInfo(15, cboCharacter)
        'LoadMasterInfo(40, cboLastClassResult)
        txtremark.Text = ""
        'txtDateDropOut.Text = ""
        txtWorkDays.Text = ""
        cboLastClass1.SelectedIndex = 0
        cboMonth.SelectedIndex = 0
        txtFeeCon.Text = ""
        cboreason.SelectedIndex = 0
        txtSchoolDays.Text = ""
        txtNCC.Text = ""
        cboCharacter.SelectedIndex = 0
        'LoadMasterInfo(39, cboLastClassDivision)
        'LoadMasterInfo(40, cboLastClassResult)
        txtPrintDate.Text = Now.Date.ToString("dd/MM/yyyy")
        'txtBookNo.Focus()
        txtSRNo.Focus()
        LoadSession(cboSession)   ''Load Academic Sessions
        cboSession.Focus()
        txtExtraCorr.Text = "Taken Part in Co-Curricular Activities."
    End Sub

    Protected Sub btnGenerate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        'If Trim(cboSession.Text) = "" Then
        '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert(Please choose Academic Year first...');", True)
        '    cboSession.Focus()
        '    Exit Sub
        'End If
        'If chkWholeClass.Checked = False Then
        '    If Trim(txtSRNo.Text = "") Then
        '        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert(Enter Student Admn No. to continue...');", True)
        '        txtSno.Focus()
        '        Exit Sub
        '    End If
        '    If Trim(txtName.Text = "") Then
        '        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert(Enter Student Name to continue...');", True)
        '        txtName.Focus()
        '        Exit Sub
        '    End If
        'End If
        'If Trim(txtDateDropOut.Text = "") Then
        '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert(Enter School leaving date to continue...');", True)
        '    txtDateDropOut.Focus()
        '    Exit Sub
        'End If

        'If Trim(txtApplyDate.Text = "") Then
        '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert(Enter Promotion date to continue...');", True)
        '    txtApplyDate.Focus()
        '    Exit Sub
        'End If
        'If Trim(txtLeaveReason.Text = "") Then
        '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert(Enter School leaving reason to continue...');", True)
        '    txtLeaveReason.Focus()
        '    Exit Sub
        'End If
        'If Trim(cboCharacter.Text = "") Then
        '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert(Choose Conduct to continue...');", True)
        '    cboCharacter.Focus()
        '    Exit Sub
        'End If
        'If CheckTC() = True Then
        '    lblStatus.Text = "TC is Already Exists..."
        '    txtSno.Focus()
        '    Exit Sub
        'End If

        'Dim myConnStr As String = System.Configuration.ConfigurationManager.ConnectionStrings("iDiaryConnectionString").ToString
        'Dim myConn As New System.Data.SqlClient.SqlConnection(myConnStr)
        'myConn.Open()

        'Dim sqlstr As String = ""

        'Dim myCommand1 As New SqlCommand
        'Dim ASID As String = FindMasterID(1, cboSession.Text)
        'sqlstr = "Select * From vw_Student Where ClassName=N'" & cboClass.Text & "' And SecName = N'" & cboSection.Text & "' AND ASID=" & ASID
        'Dim ds As DataSet = ExecuteQuery_DataSet(sqlstr, "student")
        'Dim CharacterID As Integer = FindMasterID(15, cboCharacter.Text)
        'Dim LastClassResultID As Integer = FindMasterID(40, cboLastClassResult.Text)
        'Dim TCno As String = ""
        'Dim DOBYear As String = ""
        'Dim DOBInWords As String = ""

        'myCommand1.Dispose()
        '
        'If chkWholeClass.Checked = True Then

        '    Dim myReader As SqlDataReader = myCommand1.ExecuteReader

        '    For i = 0 To ds.Tables(0).Rows.Count - 1
        '        Dim a As Date = ds.Tables(0).Rows(i).Item("DOB").ToString
        '        TCno = Now.Date.Year & "/" & FindNextSno(Now.Date.Year, 2)

        '        DOBInWords = ConvertDateToWords(a.ToString("dd/MM/yyyy").Substring(0, 2), a.ToString("dd/MM/yyyy").Substring(3, 2), a.ToString("dd/MM/yyyy").Substring(6, 4))
        '        sqlstr = "Insert into TC (TCNo, SID, DOBInWords, admissionDate, admissionClass, lastClass,lastClassTaken, lastExamResult, Failed, subjectsStudied, promoted, feeMonth, "
        '        sqlstr += " feeConcession,workDays, presentDays, NCC, corricular, conduct, dateOFApplication, dateOfIssue, leavingReason, remarks,isIssued) Values("
        '        sqlstr += "'" & TCno & "',"
        '        sqlstr += "" & ds.Tables(0).Rows(i).Item("SID") & ","
        '        sqlstr += "'" & DOBInWords & "',"
        '        sqlstr += "'" & txtDateAdmission.Text & "',"
        '        sqlstr += "'',"
        '        sqlstr += "'" & cboLastClass.Text & "',"
        '        sqlstr += "'" & cboLastExamTaken.Text & "',"
        '        sqlstr += "'" & cboLastClassResult.Text & "',"
        '        sqlstr += "'" & txtFailed.Text & "',"
        '        sqlstr += "'" & txtSubjects.Text & "',"
        '        sqlstr += "'" & ddlPromoted.Text & "',"
        '        sqlstr += "'" & cboMonth.Text & "',"
        '        sqlstr += "'" & txtFeeCon.Text & "',"
        '        sqlstr += "'" & txtWorkDays.Text & "',"
        '        sqlstr += "'" & txtSchoolDays.Text & "',"
        '        sqlstr += "'" & txtNCC.Text & "',"
        '        sqlstr += "'" & txtExtraCorr.Text & "',"
        '        sqlstr += "'" & cboCharacter.Text & "',"
        '        sqlstr += "'" & txtApplyDate.Text & "',"
        '        sqlstr += "'" & txtPrintDate.Text & "',"
        '        sqlstr += "'" & cboreason.Text & "',"
        '        sqlstr += "N'" & txtremark.Text & "',1)"
        '        UpdateQuery(sqlstr)

        '        sqlstr = "Update Student Set "
        '        sqlstr += "StatusID=" & 5 & ""
        '        sqlstr += " Where RegNo='" & ds.Tables(0).Rows(i).Item("RegNo") & "' AND ASID=" & ASID
        '        UpdateQuery(sqlstr)
        '        PrepareReport(TCno, 2)
        '    Next
        'Else
        '    TCno = Now.Date.Year & "/" & FindNextSno(Now.Date.Year, 2)
        '    DOBInWords = ConvertDateToWords(txtDOB.Text.Substring(0, 2), txtDOB.Text.Substring(3, 2), txtDOB.Text.Substring(6, 4))

        '    sqlstr = "Insert into TC (TCNo, SID, DOBInWords,studentCategory, admissionDate, admissionClass, lastClass,lastClassTaken, lastExamResult, Failed, subjectsStudied, promoted,promotedClass, feeMonth, "
        '    sqlstr += " feeConcession,workDays, presentDays, NCC, corricular, conduct, dateOFApplication, dateOfIssue, leavingReason, remarks,isIssued) Values("
        '    sqlstr += "'" & TCno & "',"
        '    sqlstr += "" & txtSID.Text & ","
        '    sqlstr += "'" & DOBInWords & "',"
        '    sqlstr += "'" & txtCategory.Text & "',"
        '    sqlstr += "'" & txtDateAdmission.Text & "',"
        '    sqlstr += "'',"
        '    sqlstr += "'" & cboLastClass.Text & "',"
        '    sqlstr += "'" & cboLastExamTaken.Text & "',"
        '    sqlstr += "'" & cboLastClassResult.Text & "',"
        '    sqlstr += "'" & txtFailed.Text & "',"
        '    sqlstr += "'" & txtSubjects.Text & "',"
        '    sqlstr += "'" & ddlPromoted.Text & "',"
        '    sqlstr += "'" & cboLastClass0.Text & "-" & cboLastClass1.Text & "',"
        '    sqlstr += "'" & cboMonth.Text & "',"
        '    sqlstr += "'" & txtFeeCon.Text & "',"
        '    sqlstr += "'" & txtWorkDays.Text & "',"
        '    sqlstr += "'" & txtSchoolDays.Text & "',"
        '    sqlstr += "'" & txtNCC.Text & "',"
        '    sqlstr += "'" & txtExtraCorr.Text & "',"
        '    sqlstr += "'" & cboCharacter.Text & "',"
        '    sqlstr += "'" & txtDateDropOut.Text & "',"
        '    sqlstr += "'" & txtPrintDate.Text & "',"
        '    sqlstr += "'" & cboreason.Text & "',"
        '    sqlstr += "N'" & txtremark.Text & "',1)"
        '    UpdateQuery(sqlstr)
        '    sqlstr = "Update Student Set "
        '    sqlstr += "StatusID=" & 5 & ""
        '    sqlstr += " Where RegNo='" & txtSRNo.Text & "' AND ASID=" & ASID
        '    UpdateQuery(sqlstr)
        '    sqlstr = "Select MAX(CharCertificateID) From CharacterCertificateStudent"
        '    
        '    
        '    Dim CID = ExecuteQuery_ExecuteScalar(SqlStr)

        '    PrepareReport(TCno, 1)
        'End If
        'UpdateRollNo(txtSID.Text, txtCSSID.Text)
    End Sub
    Private Sub UpdateRollNo(Sid As Integer, CSSID As Integer, Optional StatusID As Integer = 1)
        Dim sqlStr As String = ""
       
       
       
        
        Dim sidRollNo As Integer = 0
        Dim maxRollNo As Integer = 0
        sqlStr = "Select ClassRollno from vw_Student where statusID=" & StatusID & " AND sid=" & Sid
        
        
        Try
            sidRollNo = ExecuteQuery_ExecuteScalar(SqlStr)()
        Catch ex As Exception

        End Try

        sqlStr = "Select max(convert(int,classrollno)) as classrollno from vw_Student where statusID=" & StatusID & " AND CSSID=" & CSSID
        
        
        Try
            maxRollNo = ExecuteQuery_ExecuteScalar(SqlStr)()
        Catch ex As Exception

        End Try

        For i = sidRollNo To maxRollNo
            sqlStr = "UPDATE Student set classrollno='" & i & "' Where ClassRollNo='" & i + 1 & "' AND CSSID=" & CSSID
            
            
            ExecuteQuery_Update(SqlStr)
        Next

        
        
    End Sub

    Private Sub UpdateQuery(Str As String)
       
       
       
        
        myCommand.CommandText = Str
        
        ExecuteQuery_Update(SqlStr)
        
    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        InitControls()
    End Sub
    Private Sub PrepareReport(ByVal TCNo As String, type As Integer)

        Dim ASID As String = FindMasterID(1, cboSession.Text)
        'Dim P1 As String = "", P2 As String = "", P3 As String = ""
        'Dim P4 As String = "", P5 As String = "", P6 As String = ""
        Dim sql As String = ""
        If type = 1 Then
            sql = "Select * from oldtcdata where TCNo = '" & TCNo & "' order by SID"
        ElseIf type = 2 Then
            sql = "Select * From oldtcdata Where ClassName=N'" & cboClass.Text & "' And SecName = N'" & cboSection.Text & "' AND ASID=" & ASID & "  order by SID"
        End If
        Dim ds As New DataSet
        ds = ExecuteQuery_DataSet(sql, "tbl")
        Dim rds As ReportDataSource = New ReportDataSource()
        rds.Name = "DataSet1" ' Change to what you will be using when creating an objectdatasource
        rds.Value = ds.Tables(0)
        With ReportViewer1   ' Name of the report control on the form
            .Reset()
            .ProcessingMode = ProcessingMode.Local
            .LocalReport.DataSources.Clear()
            .Visible = True
            .LocalReport.ReportPath = "rptTC.rdlc"
            .LocalReport.DataSources.Add(rds)


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

        'Dim params(5) As Microsoft.Reporting.WebForms.ReportParameter
        'params(0) = New Microsoft.Reporting.WebForms.ReportParameter("TCNo", TCNo, True)
        'params(1) = New Microsoft.Reporting.WebForms.ReportParameter("P1", P1, True)
        'params(2) = New Microsoft.Reporting.WebForms.ReportParameter("P2", P2, True)
        'params(3) = New Microsoft.Reporting.WebForms.ReportParameter("P3", P3, True)
        'params(4) = New Microsoft.Reporting.WebForms.ReportParameter("P4", P4, True)
        'params(5) = New Microsoft.Reporting.WebForms.ReportParameter("P5", P5, True)
        'Me.ReportViewer1.LocalReport.SetParameters(params)


        'ReportViewer1.LocalReport.Refresh()

    End Sub
    Public Function ExecuteQuery_DataSet(ByVal strQuery As String, ByVal cTableName As String) As DataSet
       
        Dim con As New System.Data.SqlClient.SqlConnection(myConnStr)
        Dim SqlCmd = New SqlCommand(strQuery, con)

        Dim da As New SqlDataAdapter()
        da.SelectCommand = SqlCmd
        If con.State <> ConnectionState.Open Then
            con.Open()
        End If
        Dim ds As New DataSet()
        Try
            da.Fill(ds, cTableName)
        Catch ex As Exception
            'HttpContext.Current.Response.Write(" Error Web Msql Error ExecuteQuery : " );
            Throw (ex)
        Finally
            SqlCmd.Connection.Close()
            SqlCmd.Dispose()
            con.Close()
        End Try
        Return ds
    End Function
    Protected Sub btnNameNext_Click(sender As Object, e As ImageClickEventArgs) Handles btnNameNext.Click
        If Trim(cboSession.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert(Please choose Academic Year first...');", True)
            cboSession.Focus()
            Exit Sub
        End If
        Dim ASID As String = cboSession.Text
        GridView2.Visible = True
        SqlDataSource2.SelectCommand = "SELECT Tcno,SName, MName,FName FROM oldTCData WHERE ASID = " & ASID & " AND SName Like N'%" & txtSName.Text & "%'"

        GridView2.DataBind()
    End Sub

    Protected Sub GridView2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView2.SelectedIndexChanged
        txtSno.Text = GridView2.SelectedRow.Cells(1).Text
        GetTCDetails()
        GridView2.SelectedIndex = -1
        GridView2.Visible = False
        '  ShowStudentRecord(2, GridView2.SelectedRow.Cells(1).Text)
    End Sub
    Private Sub ShowStudentRecord(ByVal SearchType As Integer, SearchVal As String)
        Dim ASID As String = FindMasterID(1, cboSession.Text)
       
       
       

        Dim sqlstr As String = ""
        

        '------Load Personal Information--------
        If SearchType = 1 Then
            sqlstr = "Select * From oldTCData Where RegNo=N'" & SearchVal & "' AND ASID=" & ASID
        ElseIf SearchType = 2 Then
            sqlstr = "Select * From oldTCData Where SName=N'" & SearchVal & "' AND ASID=" & ASID
        End If
        
        

        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)

        While myReader.Read
            txtSRNo.Text = myReader("RegNo")
            txtName.Text = myReader("SName")
            Dim a As Date = myReader("DOB").ToString
            txtDOB.Text = a.ToString("dd/MM/yyyy")
            txtFName.Text = myReader("FName")
            txtMName.Text = myReader("MName")
            txtClassSection.Text = myReader("ClassName") & "/" & myReader("SecName")
            txtClassName.Text = myReader("ClassName")
            txtSecName.Text = myReader("SecName")
            txtSID.Text = myReader("SID")
            a = myReader("AdmissionYear")
            txtDateAdmission.Text = a.ToString("dd/MM/yyyy")
            txtCSSID.Text = myReader("CSSID")
            'txtCategory.Text = myReader("studentCategory")
        End While
        myReader.Close()

        sqlstr = "select SubjectName from vwsubjectmapping where classname='" & txtClassName.Text & "' AND secname='" & txtSecName.Text & "'"
        
        
        'Dim MyReader As SqlDataReader = myCommand1.ExecuteReader
        myReader = ExecuteQuery_ExecuteReader(sqlStr)
        Dim count As Integer = 1
        Dim subjects As String = ""
        While myReader.Read
            subjects += count & ". " & myReader(0) & "    "
            count += 1
        End While
        myReader.Close()
        
        txtSubjects.Text = subjects
        txtDateDropOut.Focus()
    End Sub
    Protected Sub btnSRNoNext_Click(sender As Object, e As ImageClickEventArgs) Handles btnSRNoNext.Click
        If Trim(cboSession.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert(Please choose Academic Year first...');", True)
            cboSession.Focus()
            Exit Sub
        End If
        ShowStudentRecord(1, txtSRNo.Text)
        txtApplyDate.Focus()
    End Sub
    Protected Sub btnSnoNext_Click(sender As Object, e As ImageClickEventArgs) Handles btnSnoNext.Click
        If Trim(cboSession.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert(Please choose Academic Year first...');", True)
            cboSession.Focus()
            Exit Sub
        End If
        If CheckCertificateSno(txtSno.Text, 2) = False Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert(TC No. doesn't Exists...');", True)
            txtSno.Text = ""
            txtSno.Focus()
            InitControls()
            Exit Sub
        Else
            PrepareReport(txtSno.Text, 1)
            GetTCDetails()
        End If
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
        LoadClassSection(cboClass.Text, cboSection)
        cboSection.Focus()
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
       
       
       
        If Trim(txtSno.Text = "") Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert(Enter TC No. to cancel...');", True)
            txtSno.Focus()
            Exit Sub
        End If


        Dim sqlstr As String = "update oldtcdata set isIssued=0 where TCNo='" & txtSno.Text & "'"
        

        
        
        ExecuteQuery_Update(SqlStr)

        
        

    End Sub
    Private Sub GetTCDetails()
       
       
       

        Dim sqlstr As String = ""
        
        sqlstr = "Select * From oldtcdata Where TCNo='" & txtSno.Text & "'"

        
        

        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)

        While myReader.Read
            txtSRNo.Text = myReader("RegNo")
            txtSID.Text = myReader("SID")
            txtName.Text = myReader("SName")
            txtFName.Text = myReader("FName")
            txtMName.Text = myReader("MName")
            Dim a As Date = myReader("DOB").ToString
            txtDOB.Text = a.ToString("dd/MM/yyyy")
            txtClassSection.Text = myReader("ClassName") & "/" & myReader("SecName")
            a = myReader("AdmissionYear")
            txtDateAdmission.Text = a.ToString("dd/MM/yyyy")
            'cboLastClass.Text = myReader("lastClass")
            cboLastExamTaken.Text = myReader("lastClassTaken")
            cboLastClassResult.Text = myReader("lastExamResult")
            Try
                txtFailed.Text = myReader("Failed")
            Catch ex As Exception

            End Try
            txtSubjects.Text = myReader("subjectsStudied")
            ddlPromoted.Text = myReader("promoted")
            Dim PromotedClass As String = myReader("promotedClass")
            Dim PClass As String() = PromotedClass.Split("-")
            cboLastClass0.Text = PClass(0)
            cboLastClass1.Text = PClass(1)
            cboMonth.Text = myReader("feeMonth")
            txtFeeCon.Text = myReader("feeConcession")
            txtDateDropOut.Text = myReader("dateOfIssue")
            cboreason.Text = myReader("leavingReason")
            txtWorkDays.Text = myReader("workDays")
            txtSchoolDays.Text = myReader("presentDays")
            txtNCC.Text = myReader("NCC")
            txtExtraCorr.Text = myReader("corricular")
            cboCharacter.Text = myReader("conduct")
            txtApplyDate.Text = myReader("dateOFApplication")
            Try
                txtremark.Text = myReader("remarks")
            Catch ex As Exception

            End Try
        End While

        myReader.Close()
        

    End Sub
    Private Function CheckTC()
       
       
       

        Dim sqlStr As String = "Select Count(*) From oldtcdata Where SID='" & txtSID.Text & "'"
        
        
        
        Dim rv As Integer = ExecuteQuery_ExecuteScalar(SqlStr)
        
        
        If rv > 0 Then
            Return True
        Else
            Return False
        End If
        
        
        Return rv
    End Function

    Protected Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        If Trim(cboSession.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert(Please choose Academic Year first...');", True)
            cboSession.Focus()
            Exit Sub
        End If

       
       
       

        Dim sqlstr As String = ""
        Dim DOBInWords As String = ""
        DOBInWords = ConvertDateToWords(txtDOB.Text.Substring(0, 2), txtDOB.Text.Substring(3, 2), txtDOB.Text.Substring(6, 4))
        
        sqlstr = "Update oldtcdata SET DOBInWords='" & DOBInWords & "',"
        sqlstr += "studentCategory='" & txtCategory.Text & "',"
        sqlstr += "admissionDate='" & txtDateAdmission.Text & "',"
        'sqlstr += "admissionClass='" & txtClass.Text & "',"
        sqlstr += "lastClass='" & cboLastClass.Text & "',"
        sqlstr += "lastClassTaken='" & cboLastExamTaken.Text & "',"
        sqlstr += "lastExamResult='" & cboLastClassResult.Text & "',"
        sqlstr += "Failed='" & txtFailed.Text & "',"
        sqlstr += "subjectsStudied='" & txtSubjects.Text & "',"
        sqlstr += "promoted='" & ddlPromoted.Text & "',"
        sqlstr += "promotedClass='" & cboLastClass0.Text & "-" & cboLastClass1.Text & "',"
        sqlstr += "feeMonth='" & cboMonth.Text & "',"
        sqlstr += "feeConcession='" & txtFeeCon.Text & "',"
        sqlstr += "workDays='" & txtWorkDays.Text & "',"
        sqlstr += "presentDays='" & txtSchoolDays.Text & "',"
        sqlstr += "NCC='" & txtNCC.Text & "',"
        sqlstr += "corricular='" & txtExtraCorr.Text & "',"
        sqlstr += "conduct='" & cboCharacter.Text & "',"
        sqlstr += "dateOFApplication='" & txtDateDropOut.Text & "',"
        sqlstr += "dateOfIssue='" & txtPrintDate.Text & "',"
        sqlstr += "leavingReason='" & cboreason.Text & "',"
        sqlstr += "remarks='" & txtremark.Text & "'"
        sqlstr += " Where TCno='" & txtSno.Text & "' AND SID=" & txtSID.Text

        UpdateQuery(sqlstr)
        PrepareReport(txtSno.Text, 1)
    End Sub
End Class
