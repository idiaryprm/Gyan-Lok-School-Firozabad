Imports System.Data.SqlClient
Imports iDiary_V3.iDiary_Date.CLS_iDiary_Date
Imports iDiary_V3.iDiary.CLS_idiary
Imports Microsoft.Reporting.WebForms

Public Class Cert_DOB
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cookies("ActiveTab").Value = 6
        Response.Cookies("ActiveTab").Expires = DateTime.Now.AddHours(1)
        Session("ActiveTab") = 6
       
        If IsPostBack = False Then
            InitControls()
            'Session("ActiveTab") = 6
        End If
        If Request.QueryString("type") = "1" Then
            Literal1.Text = "DOB Certificate"
        ElseIf Request.QueryString("type") = "2" Then
            Literal1.Text = "Studying Certificate"
        ElseIf Request.QueryString("type") = "3" Then
            Literal1.Text = "Bonafide Certificate"
        ElseIf Request.QueryString("type") = "4" Then
            Literal1.Text = "Character Certificate"
            'LoadMasterInfo(15, cboCharacter)
            'cboCharacter.Visible = True
            'lblConduct.Visible = True
        Else
            Literal1.Text = "Provisional Certificate"
        End If
    End Sub

    Private Sub InitControls()
        If Request.QueryString("type") = "3" Or Request.QueryString("type") = "4" Then
            LoadMasterInfo(71, cboSchoolName, Request.Cookies("SchoolIDs").Value)
            LoadMasterInfo(2, cboClass, cboSchoolName.Text)
        End If
        If Request.QueryString("type") = "4" Then
            LoadMasterInfo(15, cboCharacter)
            cboCharacter.Visible = True
            lblConduct.Visible = True
        Else
            cboCharacter.Visible = False
            lblConduct.Visible = False
        End If
        txtSchoolName.Text = ""
        txtAdminNo.Text = ""
        txtSName.Text = ""
        txtFName.Text = ""
        txtMName.Text = ""
        txtAdmissionDate.Text = ""
        txtDOB.Text = ""
        txtRollNo.Text = ""
        txtClass.Text = ""
        txtSec.Text = ""
        txtSID.Text = ""
        txtGender.Text = ""
        'ReportViewer1.Visible = False

        txtAdminNo.Focus()
    End Sub

    Protected Sub btnFind_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFind.Click
        If txtAdminNo.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Registration No is required...');", True)
            txtAdminNo.Focus()
            Exit Sub
        End If
        FillStudentDetail(txtAdminNo.Text)
        txtSName.ReadOnly = True
        txtAdminNo.Focus()
    End Sub
    Private Sub FillStudentDetail(RegNo As String)
        Dim sqlStr As String = ""
        sqlStr = "Select SchoolHeaderImage,RegNo, SName, FName, MName, AdmissionDate, DOB, ClassName,SecName,Gender,ClassRollNo,SID,SchoolName From vw_Student Where RegNo='" & RegNo & "' AND ASID=" & Request.Cookies("ASID").Value
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)

        While myReader.Read
            lblHeaderImg.Text = myReader("SchoolHeaderImage").ToString
            txtSchoolName.Text = myReader("SchoolName")
            txtSName.Text = myReader("SName")
            txtFName.Text = myReader("FName")
            txtMName.Text = myReader("MName")
            Dim tmpDate As Date = Now.Date
            Try
                tmpDate = myReader("AdmissionDate")
            Catch ex As Exception

            End Try
            'Dim tmpDate As Date = myReader("AdmissionDate")
            'txtAdmissionDate.Text = a.Substring(3, 2) & "/" & a.Substring(0, 2) & "/" & a.Substring(6, 4)
            txtAdmissionDate.Text = tmpDate.ToString("dd/MM/yyyy")
            Try
                tmpDate = myReader("DOB")
            Catch ex As Exception

            End Try
            'txtDOB.Text = a.Substring(3, 2) & "/" & a.Substring(0, 2) & "/" & a.Substring(6, 4)
            txtDOB.Text = tmpDate.ToString("dd/MM/yyyy")
            txtClass.Text = myReader("ClassName")
            txtSec.Text = myReader("SecName")
            txtDOBInWords.Text = ConvertDateToWords(tmpDate.Day, tmpDate.Month, tmpDate.Year)
            txtGender.Text = myReader("Gender")
            txtSID.Text = myReader("SID")
            Try
                txtRollNo.Text = myReader("ClassRollNo")
            Catch ex As Exception

            End Try
        End While
        myReader.Close()
    End Sub
    Protected Sub btnGenerate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        If chkWholeClass.Checked = True Then
            
            If cboClass.Text = "" And cboSection.Text = "" Then
                'Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert(अंतिम कक्षा की श्रेणी अंकित करें...');", True)
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Class or Section...');", True)
                cboClass.Focus()
                Exit Sub
            End If
        Else
            If Trim(txtGender.Text = "") Then
                'Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert(अंतिम कक्षा की श्रेणी अंकित करें...');", True)
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Reg No...');", True)
                txtAdminNo.Focus()
                Exit Sub
            End If
            If cboCharacter.Text = "" And cboCharacter.Visible = True Then
                'Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert(अंतिम कक्षा की श्रेणी अंकित करें...');", True)
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Conduct...');", True)
                cboCharacter.Focus()
                Exit Sub
            End If
        End If
       


        Dim SessionStart As String = Request.Cookies("ASName").Value.ToString.Substring(0, Request.Cookies("ASName").Value.ToString.IndexOf("-"))
        Dim SessionEnd As String = Request.Cookies("ASName").Value.ToString.Substring(Request.Cookies("ASName").Value.ToString.IndexOf("-") + 1, Request.Cookies("ASName").Value.ToString.Length - Request.Cookies("ASName").Value.ToString.IndexOf("-") - 1)

        Dim myMonth As String = ""
        Select Case Now.Month
            Case 1 : myMonth = "January"
            Case 2 : myMonth = "February"
            Case 3 : myMonth = "March"
            Case 4 : myMonth = "April"
            Case 5 : myMonth = "May"
            Case 6 : myMonth = "June"
            Case 7 : myMonth = "July"
            Case 8 : myMonth = "August"
            Case 9 : myMonth = "September"
            Case 10 : myMonth = "October"
            Case 11 : myMonth = "November"
            Case 12 : myMonth = "December"
        End Select

        Dim Matter1 As String = "", Matter2 As String = "", Matter3 As String = ""
        Dim SRno As String = txtAdminNo.Text
        Dim AdmnDate As String = txtAdmissionDate.Text
        Dim reportPath As String = ""
        '................................................vikash..........17/06/2016.........................................
        Dim sqlstr As String = ""
        sqlstr = "Select * from Params"
        Dim SchoolName As String = ""
        Dim Address As String = ""
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)

        While myReader.Read
            SchoolName = myReader("SchoolName")
            Address = myReader("SchoolDetails")
        End While
        myReader.Close()
        '.........................................................................................................................



        If Request.QueryString("type") = "1" Then 'DOB Cert

            reportPath = "Report/rptDOB.rdlc"
            'If txtGender.Text = "0" Then
            '    Matter1 = "This is to certify that <b>" & txtSName.Text & "</b> son of  <b>" & _
            '           txtFName.Text & "</b> and <b>" & txtMName.Text & "</b> Admn. No. <b>" & txtAdminNo.Text & "</b> is a regular student of our school."
            '    '"from " & txtAdmissionDate.Text & " and is studying in Class " & txtClass.Text & "."
            '    Matter2 = "His date of birth as per our records is (in Figure) <b>" & txtDOB.Text & "</b>"
            'Else
            '    Matter1 = "This is to certify that <b>" & txtSName.Text & "</b> daughter of  <b>" & _
            '           txtFName.Text & "</b> and <b>" & txtMName.Text & "</b> Admn. No. <b>" & txtAdminNo.Text & "</b> is a regular student of our school."
            '    '"from " & txtAdmissionDate.Text & " and is studying in Class " & txtClass.Text & "."
            '    Matter2 = "Her date of birth as per our records is (in Figure) <b>" & txtDOB.Text & "</b>"
            'End If
            'Matter3 = "in words <b>" & txtDOBInWords.Text & "</b>"

        ElseIf Request.QueryString("type") = "2" Then 'Studying Cert

            SRno = ""
            AdmnDate = ""
            reportPath = "Report/rptCertStudying.rdlc"

            If txtGender.Text = "0" Then
                Matter1 = "This is to certify that <b>" & txtSName.Text & "</b> S/o  <b>" & _
                       txtFName.Text & "</b> is a bonafide student of " & SchoolName & ", " & Address & "."
                '"from " & txtAdmissionDate.Text & " and is studying in Class " & txtClass.Text & "."
                Matter2 = "He is studying in class <b>" & txtClass.Text & "</b> in academic year <b>" & SessionStart & "-" & SessionEnd & "</b>."
                Matter3 = "He bears a good moral character."
            Else
                Matter1 = "This is to certify that <b>" & txtSName.Text & "</b> D/o  <b>" & _
                       txtFName.Text & "</b> is a bonafide student of " & SchoolName & ", " & Address & "."
                '"from " & txtAdmissionDate.Text & " and is studying in Class " & txtClass.Text & "."
                Matter2 = "She is studying in class <b>" & txtClass.Text & "</b> in academic year <b>" & SessionStart & "-" & SessionEnd & "</b>."
                Matter3 = "She bears a good moral character."
            End If
            
        ElseIf Request.QueryString("type") = "3" Then 'Bonafide
            reportPath = "Report/rptBonafide.rdlc"

            'If txtClass.Text = "X" Then




            '    If txtGender.Text = "0" Then
            '        Matter1 = "This is to certify that <b>" & txtSName.Text & "</b> son of <b>" & _
            '               txtFName.Text & "</b> and <b>" & txtMName.Text & "</b> is a bonafide student of class <b>" & txtClass.Text & " - " & txtSec.Text & "</b> " & SchoolName & "."
            '        '"from " & txtAdmissionDate.Text & " and is studying in Class " & txtClass.Text & "."
            '        Matter2 = "His date of birth as per the school record is (in Figure) <b>" & txtDOB.Text & "</b>"
            '    Else
            '        Matter1 = "This is to certify that <b>" & txtSName.Text & "</b> daughter of <b>" & _
            '               txtFName.Text & "</b> and <b>" & txtMName.Text & "</b> is a bonafide student of class <b>" & txtClass.Text & " - " & txtSec.Text & "</b> " & SchoolName & "."
            '        '"from " & txtAdmissionDate.Text & " and is studying in Class " & txtClass.Text & "."
            '        Matter2 = "Her date of birth as per the school record is (in Figure) <b>" & txtDOB.Text & "</b>"
            '    End If
            '    Matter3 = "in words <b>(" & txtDOBInWords.Text & ")</b>"


            'Else

            '    SRno = ""
            '    AdmnDate = ""
            '    reportPath = "Report/rptCertStudying.rdlc"
            '    If txtGender.Text = "0" Then
            '        Matter1 = "This is to certify that <b>" & txtSName.Text & "</b> S/o <b>" & _
            '               txtFName.Text & "</b> is a bonafide student of " & SchoolName & ", " & Address & "."
            '        '"from " & txtAdmissionDate.Text & " and is studying in Class " & txtClass.Text & "."
            '        Matter2 = "He is studying in class <b>" & txtClass.Text & "</b> in academic year </b>" & SessionStart & "-" & SessionEnd & "</b>"
            '        Matter3 = "He bears a good moral character."
            '    Else
            '        Matter1 = "This is to certify that <b>" & txtSName.Text & "</b> D/o <b>" & _
            '               txtFName.Text & "</b> is a bonafide student of " & SchoolName & ", " & Address & "."
            '        '"from " & txtAdmissionDate.Text & " and is studying in Class " & txtClass.Text & "."
            '        Matter2 = "She is studying in class <b>" & txtClass.Text & "</b> in academic year <b>" & SessionStart & "-" & SessionEnd & "</b>"
            '        Matter3 = "She bears a good moral character."
            '    End If





            'End If

        ElseIf Request.QueryString("type") = 4 Then 'Charachter

            reportPath = "Report/rptCharacter.rdlc"
            'If txtGender.Text = "0" Then
            '    Matter1 = "This is to certify that <b>" & txtSName.Text & "</b> S/o <b>" & _
            '           txtFName.Text & "</b> is a bonafide student of " & SchoolName & ", " & Address & ". He is studying in class <b>" & txtClass.Text & "</b> in academic year </b>" & SessionStart & "-" & SessionEnd & "</b>"""
            '    '"from " & txtAdmissionDate.Text & " and is studying in Class " & txtClass.Text & "."
            '    Matter2 = "His date of birth in accordance with the school admission register is <b>" & txtDOB.Text & "</b>."
            '    Matter3 = "He bears a good moral character."
            'Else
            '    Matter1 = "This is to certify that <b>" & txtSName.Text & "</b> D/o <b>" & _
            '           txtFName.Text & "</b> is a bonafide student of " & SchoolName & ", " & Address & ". She is studying in class <b>" & txtClass.Text & "</b> in academic year <b>" & SessionStart & "-" & SessionEnd & "</b>"""
            '    '"from " & txtAdmissionDate.Text & " and is studying in Class " & txtClass.Text & "."
            '    Matter2 = "Her date of birth in accordance with the school admission register is <b>" & txtDOB.Text & "</b>."
            '    Matter3 = "She bears a good moral character."
            'End If


        Else 'Provisional


            reportPath = "Report/rptCertStudying.rdlc"
            If txtGender.Text = "0" Then
                Matter1 = "This is to certify that <b>" & txtSName.Text & "</b>, Roll No. <b>" & txtRollNo.Text & "</b>, son of <b>" & _
                       txtFName.Text & "</b> and <b>" & txtMName.Text & "</b> as declared successful in the <b>AISSCE</b> held in March/April in academic year <b>" & SessionStart & "-" & SessionEnd & "</b>"
                '"from " & txtAdmissionDate.Text & " and is studying in Class " & txtClass.Text & "."
                Matter2 = " "
                Matter3 = " "
            Else
                Matter1 = "This is to certify that <b>" & txtSName.Text & "</b>, Roll No. <b>" & txtRollNo.Text & "</b>, daughter of <b>" & _
                         txtFName.Text & "</b> and <b>" & txtMName.Text & "</b> as declared successful in the <b>AISSCE</b> held in March/April in academic year <b>" & SessionStart & "-" & SessionEnd & "</b>"
                '"from " & txtAdmissionDate.Text & " and is studying in Class " & txtClass.Text & "."
                Matter2 = " "
                Matter3 = " "
            End If
        End If

        Dim Sql As String = ""
        If chkWholeClass.Checked = True Then
            Sql = "Select * from vw_Student where CLassName='" & cboClass.Text & "' and SecName='" & cboSection.Text & "' and ASID='" & Request.Cookies("ASID").Value & "' and SchoolName='" & cboSchoolName.Text & "'"
        Else
            Sql = "Select * from vw_Student where SID=" & txtSID.Text
        End If

        Dim ds As New DataSet
        ds = ExecuteQuery_DataSet(Sql, "tbl")
        If (Request.QueryString("type") = "3" Or Request.QueryString("type") = "4") And chkWholeClass.Checked = True Then
            'ds.Tables(0).Columns.Add("DateOfBirth", GetType(String))
            For Each Row As DataRow In ds.Tables(0).Rows
                Row("SchoolHeaderImage") = Server.MapPath(Row("SchoolHeaderImage"))
                'Dim tmpDate As Date = Now.Date
                'tmpDate = Row("DOB")
                'Row("DateOfBirth") = ConvertDateToWords(tmpDate.Day, tmpDate.Month, tmpDate.Year)
            Next
        End If
        Dim rds As ReportDataSource = New ReportDataSource()
        rds.Name = "DataSet1" ' Change to what you will be using when creating an objectdatasource
        '    End If

        rds.Value = ds.Tables(0)
        With ReportViewer1   ' Name of the report control on the form
            .Reset()
            .ProcessingMode = ProcessingMode.Local
            .LocalReport.DataSources.Clear()
            .Visible = True
            .LocalReport.ReportPath = reportPath
            .LocalReport.DataSources.Add(rds)
            .LocalReport.EnableExternalImages = True
        End With

        'Matter3 = "The academic year being from April " & SessionStart & " - March " & SessionEnd
        If Request.QueryString("type") = 4 Then 'Charachter

            Dim params(3) As Microsoft.Reporting.WebForms.ReportParameter
            'params(0) = New Microsoft.Reporting.WebForms.ReportParameter("SRNo", UCase(SRno), Visible)
            'params(1) = New Microsoft.Reporting.WebForms.ReportParameter("Matter1", Matter1, Visible)
            'params(2) = New Microsoft.Reporting.WebForms.ReportParameter("Matter2", Matter2, Visible)
            'params(3) = New Microsoft.Reporting.WebForms.ReportParameter("Matter3", Matter3, Visible)
            'params(4) = New Microsoft.Reporting.WebForms.ReportParameter("AdmnDate", AdmnDate, Visible)
            params(0) = New Microsoft.Reporting.WebForms.ReportParameter("headerPath", Server.MapPath(lblHeaderImg.Text), Visible)
            'params(6) = New Microsoft.Reporting.WebForms.ReportParameter("CertName", UCase(Literal1.Text), Visible)
            params(1) = New Microsoft.Reporting.WebForms.ReportParameter("DOBHindi", txtDOBInWords.Text, Visible)
            params(2) = New Microsoft.Reporting.WebForms.ReportParameter("Conduct", cboCharacter.Text, Visible)
            params(3) = New Microsoft.Reporting.WebForms.ReportParameter("Enddate", "06/30/" & SessionStart, Visible)
            Me.ReportViewer1.LocalReport.SetParameters(params)

        Else
            Dim params(2) As Microsoft.Reporting.WebForms.ReportParameter
            'params(0) = New Microsoft.Reporting.WebForms.ReportParameter("SRNo", UCase(SRno), Visible)
            'params(1) = New Microsoft.Reporting.WebForms.ReportParameter("Matter1", Matter1, Visible)
            'params(2) = New Microsoft.Reporting.WebForms.ReportParameter("Matter2", Matter2, Visible)
            'params(3) = New Microsoft.Reporting.WebForms.ReportParameter("Matter3", Matter3, Visible)
            'params(4) = New Microsoft.Reporting.WebForms.ReportParameter("AdmnDate", AdmnDate, Visible)
            params(0) = New Microsoft.Reporting.WebForms.ReportParameter("headerPath", Server.MapPath(lblHeaderImg.Text), Visible)
            'params(6) = New Microsoft.Reporting.WebForms.ReportParameter("CertName", UCase(Literal1.Text), Visible)
            params(1) = New Microsoft.Reporting.WebForms.ReportParameter("DOBHindi", txtDOBInWords.Text, Visible)
            params(2) = New Microsoft.Reporting.WebForms.ReportParameter("Conduct", cboCharacter.Text, Visible)
            Me.ReportViewer1.LocalReport.SetParameters(params)
        End If

        ReportViewer1.Visible = True

        ReportViewer1.LocalReport.Refresh()

        SaveCertificateDetails(Request.QueryString("type"))
        InitControls()
        txtSName.ReadOnly = False
    End Sub

    Private Sub SaveCertificateDetails(type As Integer)
        Dim sqlStr As String = ""
        sqlStr = "Insert into StudentCertificate Values(" & _
            "'" & txtSID.Text & "'," & _
                       "'" & DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") & "'," & _
        "'" & type & "')"
        ExecuteQuery_Update(sqlStr)
    End Sub

    Protected Sub btnNameSearch_Click(sender As Object, e As EventArgs) Handles btnNameSearch.Click
        GridView1.Visible = True
        SqlDataSource1.SelectCommand = "SELECT RegNo, SName, ClassName, SecName, FName, MName, AdmissionDate, DOB, SchoolName FROM vw_Student WHERE ASID = " & Request.Cookies("ASID").Value & " and SName Like '%" & txtSName.Text & "%'"
        GridView1.DataBind()
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged
        txtAdminNo.Text = GridView1.SelectedRow.Cells(1).Text
        FillStudentDetail(txtAdminNo.Text)
        GridView1.Visible = False
        txtSName.ReadOnly = True
    End Sub

    Protected Sub chkWholeClass_CheckedChanged(sender As Object, e As EventArgs) Handles chkWholeClass.CheckedChanged
        If chkWholeClass.Checked = True Then
            Panel1.Visible = True
        Else
            Panel1.Visible = False
        End If
    End Sub

    Protected Sub cboClass_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboClass.SelectedIndexChanged
        LoadClassSection(cboSchoolName.Text, cboClass.Text, cboSection)
        cboClass.Focus()
    End Sub

    Protected Sub cboSchoolName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSchoolName.SelectedIndexChanged
        LoadMasterInfo(2, cboClass, cboSchoolName.Text)
    End Sub
End Class