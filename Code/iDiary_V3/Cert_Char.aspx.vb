Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Imports Microsoft.Reporting.WebForms

Partial Class Cert_Char
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then InitControls()
    End Sub
    Private Sub InitControls()
        ReportViewer1.Visible = False
        GridView2.Visible = False
        txtSno.Text = Now.Date.Year & "/" & FindNextSno(Now.Date.Year, 1)
        txtSRNo.Text = ""
        txtDateDropOut.Text = Now.Date.ToString("dd/MM/yyyy")
        txtDOB.Text = ""
        txtName.Text = ""
        txtFName.Text = ""
        txtMName.Text = ""
        txtStudentID.Text = ""
        LoadMasterInfo(2, cboClass)
        txtRollNo.Text = ""
        LoadMasterInfo(15, cboCharacter)
        LoadMasterInfo(53, cboLastClassDivision)
        LoadMasterInfo(54, cboLastClassResult)
        txtPrintDate.Text = Now.Date.ToString("dd/MM/yyyy")
        'txtBookNo.Focus()
        txtSRNo.Focus()
    End Sub
    Protected Sub btnGenerate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerate.Click

        If Trim(txtSRNo.Text = "") Then
            'Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert(विध्यार्थी की पंजीकरण संख्या अंकित करें...');", True)
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Reg. No...');", True)
            txtSno.Focus()
            Exit Sub
        End If
        If Trim(txtName.Text = "") Then
            'Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert(विध्यार्थी का नाम अंकित करें...');", True)
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Student Name...');", True)
            txtName.Focus()
            Exit Sub
        End If
        If Trim(txtDateDropOut.Text = "") Then
            'Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert(विध्यालय छोड़ने की दिनांक अंकित करें...');", True)
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid School leave Date...');", True)
            txtDateDropOut.Focus()
            Exit Sub
        End If
        If Trim(cboClass.Text = "") Then
            'Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert(अंतिम कक्षा का नाम अंकित करें...');", True)
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Last Studied Class...');", True)
            cboClass.Focus()
            Exit Sub
        End If
        If Trim(txtRollNo.Text = "") Then
            'Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert(अंतिम कक्षा का अनुक्रमांक अंकित करें...');", True)
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert(Invalid Roll No...');", True)
            txtRollNo.Focus()
            Exit Sub
        End If
        If Trim(cboLastClassDivision.Text = "") Then
            'Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert(अंतिम कक्षा की श्रेणी अंकित करें...');", True)
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Division...');", True)
            cboLastClassDivision.Focus()
            Exit Sub
        End If
        If Trim(cboLastClassResult.Text = "") Then
            'Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert(अंतिम कक्षा का परीक्षाफल अंकित करें...');", True)
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invaild Last Class Result...');", True)
            cboLastClassResult.Focus()
            Exit Sub
        End If
        Dim CCno As String = Now.Date.Year & "/" & FindNextSno(Now.Date.Year, 1)
        Dim ClassID As Integer = FindMasterID(2, cboClass.Text)
        Dim LastClassDivisionID As Integer = FindMasterID(53, cboLastClassDivision.Text)
        Dim LastClassResultID As Integer = FindMasterID(54, cboLastClassResult.Text)

        Dim sqlstr As String = ""

        Dim DOBYear As String = ""
        If txtDOB.Text.Substring(6, 2) = "20" Then
            DOBYear += "Two thousands " & GetHindiNo(txtDOB.Text.Substring(8, 2))
        Else
            DOBYear += "Niteen hundred " & GetHindiNo(txtDOB.Text.Substring(8, 2))
        End If
        Dim DateInHindi As String = GetHindiNo(txtDOB.Text.Substring(0, 2)) & " " & GetMonth(txtDOB.Text.Substring(3, 2)) & " Year " & DOBYear
        sqlstr = "Insert into CharacterCertificateStudent (SNo,StudentID,PrintDate,CharacterType,DOBInHindi,DateDropOut,ClassDropOut,ClassDropOutRollNo,DivisionDropOut,ResultTypeDropout) Values("
        'sqlstr += "" & txtBookNo.Text & ","
        'sqlstr += "" & 0 & ","
        'sqlstr += "'" & txtPrintDate.Text.Substring(6, 4) & "',"
        sqlstr += "'" & CCno & "',"
        sqlstr += "" & txtStudentID.Text & ","
        sqlstr += "'" & txtPrintDate.Text.Substring(6, 4) & "/" & txtPrintDate.Text.Substring(3, 2) & "/" & txtPrintDate.Text.Substring(0, 2) & "',"
        sqlstr += "" & 1 & ","
        sqlstr += "N'" & DateInHindi & "',"
        sqlstr += "'" & txtDateDropOut.Text.Substring(6, 4) & "/" & txtDateDropOut.Text.Substring(3, 2) & "/" & txtDateDropOut.Text.Substring(0, 2) & "',"
        sqlstr += "" & ClassID & ","
        sqlstr += "'" & txtRollNo.Text & "',"
        sqlstr += "" & LastClassDivisionID & ","
        sqlstr += "" & LastClassResultID & ")"
       ExecuteQuery_Update(sqlstr)
       
        PrepareReport(CCno)
    End Sub
    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        InitControls()
    End Sub
    Private Sub PrepareReport(ByVal CID As String)

        Dim P1 As String = "", P2 As String = "", P3 As String = ""
        Dim P4 As String = "", P5 As String = "", P6 As String = ""
        Dim sql As String = ""
       
        sql = "Select * from vw_StudentCharacterCertificate where Sno = '" & CID & "'"
       
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
            .LocalReport.ReportPath = "Report/rptCharacter.rdlc"
            .LocalReport.DataSources.Add(rds)
        End With
        ReportViewer1.Visible = True
        ReportViewer1.LocalReport.Refresh()
  

    End Sub
    Protected Sub GridView2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView2.SelectedIndexChanged
        ShowStudentRecord(2, GridView2.SelectedRow.Cells(2).Text)
        GridView2.Visible = False
    End Sub
    Private Sub ShowStudentRecord(ByVal SearchType As Integer, SearchVal As String)
        txtStudentID.Text = ""
        Dim sqlstr As String = ""
       
        '------Load Personal Information--------
        If SearchType = 1 Then
            sqlstr = "Select * From vw_Student Where RegNo=N'" & SearchVal & "' AND ASID=" & Request.Cookies("ASID").Value
        ElseIf SearchType = 2 Then
            sqlstr = "Select * From vw_Student Where SName=N'" & SearchVal & "' AND ASID=" & Request.Cookies("ASID").Value
        End If
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)
        While myReader.Read
            txtSRNo.Text = myReader("RegNo")
            txtName.Text = myReader("SName")
            Dim a As Date = myReader("DOB").ToString
            txtDOB.Text = a.ToString("dd/MM/yyyy")
            txtFName.Text = myReader("FName")
            txtMName.Text = myReader("MName")
            txtStudentID.Text = myReader("SID")
        End While
        myReader.Close()
        txtDateDropOut.Focus()
    End Sub
    Protected Sub btnNextCC_Click(sender As Object, e As EventArgs) Handles btnNextCC.Click
        If CheckCertificateSno(txtSno.Text, 1) = False Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert(यह क्रम संख्या डेटाबेस में उपस्थित नहीं है कृपया बदलें...');", True)
            txtSno.Text = ""
            txtSno.Focus()
            Exit Sub
        Else
            PrepareReport(txtSno.Text)
        End If
    End Sub

    Protected Sub btnNextName_Click(sender As Object, e As EventArgs) Handles btnNextName.Click
        SqlDataSource2.SelectCommand = "SELECT RegNo, SName, ClassName, SecName FROM vw_Student WHERE ASID = " & Request.Cookies("ASID").Value & " AND SName Like N'%" & txtName.Text & "%'"
        GridView2.DataBind()
        GridView2.Visible = True
    End Sub

    Protected Sub btnNextSRNO_Click(sender As Object, e As EventArgs) Handles btnNextSRNO.Click
        ShowStudentRecord(1, txtSRNo.Text)
        If Trim(txtStudentID.Text = "") Then
            'Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert(विध्यालय छोड़ने की दिनांक अंकित करें...');", True)
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Admin/Reg No...');", True)
            txtSRNo.Focus()
            Exit Sub
        End If
        txtDateDropOut.Focus()
    End Sub
End Class
