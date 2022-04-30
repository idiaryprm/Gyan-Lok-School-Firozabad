Imports iDiary_V3.iDiary.CLS_idiary
Imports iDiary_V3.iDiary.CLS_iDiary_Exam
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.IO
Imports Microsoft.Reporting.WebForms

Public Class ExamAdmitCard
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try
            If Request.Cookies("UType").Value.ToString.Contains("Admin") Or Request.Cookies("UType").Value.ToString.Contains("Student") Then
                'Allow
            Else
                Response.Redirect("/./AccessDenied.aspx", False)
            End If
        Catch ex As Exception
            Response.Redirect("~/Login.aspx")
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("ActiveTab") = 7
        If (Request.Cookies("UType").Value.ToString.Contains("Admin-1") = False And Request.Cookies("UType").Value.ToString.Contains("Student-1") = False) Then
            btnAsignNo.Enabled = False
        End If
        If IsPostBack = False Then
            LoadMasterInfo(71, cboSchoolName, Request.Cookies("SchoolIDs").Value)

            LoadMasterInfo(2, cboClass, cboSchoolName.Text)
            cboClass.Text = ""
            cboSection.Items.Clear()
            btnSave.Visible = False
            cboSection.Text = ""
            cboClass.Focus()
        Else
        End If
    End Sub


    Protected Sub cboClass_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboClass.SelectedIndexChanged
        LoadClassSection(cboClass.Text, cboSection)
        Dim schoolid = FindMasterID(71, cboSchoolName.Text)
        lblExamGrpID.Text = FindMasterID(114, cboClass.SelectedItem.Text, schoolid)
        'LoadSubjectGroupExamTermsMajor(cboExamType, 1, lblExamGrpID.Text)
        LoadExamTerms(cboExamType, lblExamGrpID.Text, 0)
    End Sub

    'Changes By Abhinav
    Public Sub LoadExamTerms(ByRef cblTerm As DropDownList, grpID As Integer, type As Integer)
        'type 0 = major, type 1 = minor, type 2 = both
        cblTerm.Items.Clear()
        Dim sqlStr As String = "Select ExamTermID,ExamTermName From ExamTermMaster Where IsMinor=1"
        sqlStr &= " order by displayorder"
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            cblTerm.Items.Add(New ListItem(myReader(1), myReader(0)))
        End While
        myReader.Close()
    End Sub

    Private Function GetSID(ByVal myAdminNo As String) As Integer
        Dim myConnStr As String = System.Configuration.ConfigurationManager.ConnectionStrings("iDiaryConnectionString").ToString
        Dim myConn As New System.Data.SqlClient.SqlConnection(myConnStr)
        myConn.Open()

        Dim sqlStr As String = "Select Max(SID) From Student Where RegNo='" & myAdminNo & "' AND ASID=" & Request.Cookies("ASID").Value
        Dim myCommand As New SqlCommand(sqlStr, myConn)
        Dim rv As Integer = 0
        rv = myCommand.ExecuteScalar
        myCommand.Dispose()
        myConn.Dispose()
        Return rv
    End Function

    Protected Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        If cboClass.Text = "" Then
            lblStatus.Text = "Invalid Class..."
            cboClass.Focus()
            Exit Sub
        End If

        If cboSection.Text = "" Then
            lblStatus.Text = "Invalid Section..."
            cboSection.Focus()
            Exit Sub
        End If

        If cboExamType.Text = "" Then
            cboExamType.Text = "Invalid Exam Group..."
            cboExamType.Focus()
            Exit Sub
        End If
        Dim schoolid = FindMasterID(71, cboSchoolName.Text)
        lblStatus.Text = ""
        Dim sqlStr As String = ""
        sqlStr = "select * from vw_ExamAdmitCard where ClassName='" & cboClass.Text & "' and SecName='" & cboSection.Text & "' and ExamTermID=" & lblExamGrpID.Text & " and SchoolID='" & schoolid & "' and ASID=" & Request.Cookies("ASID").Value & " AND Status=1"
        PrepareReport(sqlStr, "rptExamAdmitCard.rdlc", "Exam Date Sheet For Class :" & cboClass.SelectedItem.Text & "-" & cboSection.SelectedItem.Text)
    End Sub

    Private Sub PrepareReport(ByVal sqlString As String, ByVal reportName As String, MyHeader As String, Optional rptParam As String = "")

        Dim sqlStr As String = ""
        Dim SignLogo = ""
        Dim schoolid = FindMasterID(71, cboSchoolName.Text)
        Dim imagepath As String = ""
        If schoolid = 1 Then
            imagepath = "~/Images1/Capture1.png"
            SignLogo = "~/Images1/sign.jpg"

        ElseIf schoolid = 2 Then
            imagepath = "~/Images1/VBISLogo.png"
            SignLogo = "~/Images1/SIGN VBIS.jpg"
        Else

            imagepath = "~/Images/GSICLogo.png"
            SignLogo = "~/Images1/SIGN GSIC.jpg"
        End If
        '................................................vikash..........17/06/2016.........................................
        sqlStr = "Select * from Params"
        Dim SchoolName As String = ""
        Dim SchoolAddress As String = ""
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)

        While myReader.Read
            SchoolName = myReader("SchoolName")
            SchoolAddress = myReader("SchoolDetails")
        End While
        myReader.Close()
        SchoolName = FindSchoolDetails1(schoolid, 0)
        SchoolAddress = FindSchoolDetails1(schoolid, 1)
        '.........................................................................................................................
        Dim termname As String = cboExamType.SelectedItem.Text
        If termname = "I UNIT TEST (30)" Then
            termname = "I UNIT TEST"
        ElseIf termname = "II UNIT TEST (30)" Then
            termname = "II UNIT TEST"
        ElseIf termname = "II UNIT TEST (20)" Then
            termname = "II UNIT TEST"
        ElseIf termname = "I UNIT TEST(20)" Then
            termname = "I UNIT TEST"
        ElseIf termname = "HALF YEARLY (70)" Then
            termname = "HALF YEARLY"
        ElseIf termname = "HALF YEARLY (80)" Then
            termname = "HALF YEARLY"
        ElseIf termname = "ANNUAL EXAM (80)" Then
            termname = "ANNUAL EXAM"
        ElseIf termname = "ANNUAL EXAM (70)" Then
            termname = "ANNUAL EXAM"
        End If

        Dim ClassHeader As String = termname
        Dim ds As New DataSet
        ds = ExecuteQuery_DataSet(sqlString, "tbl")

        Dim rds As ReportDataSource = New ReportDataSource()
        rds.Name = "DataSet1" ' Change to what you will be using when creating an objectdatasource
        rds.Value = ds.Tables(0)

        Dim rptReport As String = "Exam_Reports/"
        With ReportViewer1   ' Name of the report control on the form
            .Reset()
            .ProcessingMode = ProcessingMode.Local
            .LocalReport.DataSources.Clear()
            .Visible = True
            .LocalReport.ReportPath = rptReport & reportName
            .LocalReport.DataSources.Add(rds)
            .LocalReport.EnableExternalImages = True
        End With
        Dim params(4) As Microsoft.Reporting.WebForms.ReportParameter
        params(0) = New Microsoft.Reporting.WebForms.ReportParameter("SchoolName", SchoolName, True)
        params(1) = New Microsoft.Reporting.WebForms.ReportParameter("SchoolAddress", SchoolAddress, True)
        params(2) = New Microsoft.Reporting.WebForms.ReportParameter("ClassHeader", ClassHeader & " (" & Request.Cookies("ASName").Value.ToString() & ")", True)
        params(3) = New Microsoft.Reporting.WebForms.ReportParameter("ImagePath", Server.MapPath(imagepath), Visible)
        params(4) = New Microsoft.Reporting.WebForms.ReportParameter("SignLogo", Server.MapPath(SignLogo), Visible)
        'params(4) = New Microsoft.Reporting.WebForms.ReportParameter("ImagePath", Server.MapPath("~/images/SchoolLogo.png"), Visible)
        'params(5) = New Microsoft.Reporting.WebForms.ReportParameter("ImageSign", Server.MapPath("~/images/signature.png"), Visible)
        'params(6) = New Microsoft.Reporting.WebForms.ReportParameter("UserName", Request.Cookies("UID").Value, Visible)
        'params(7) = New Microsoft.Reporting.WebForms.ReportParameter("rptParam", rptParam, Visible)
        Me.ReportViewer1.LocalReport.SetParameters(params)

        ReportViewer1.Visible = True
        ReportViewer1.LocalReport.Refresh()


    End Sub



    Public Function ExecuteQuery_DataSet(ByVal strQuery As String, ByVal cTableName As String) As DataSet
        Dim myConnStr As String = System.Configuration.ConfigurationManager.ConnectionStrings("iDiaryConnectionString").ToString
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

    Protected Sub btnprint_Click(sender As Object, e As EventArgs) Handles btnprint.Click
        btnprint.Attributes.Add("onClick", "print();")
    End Sub
    Protected Sub cboSchool_SelectedIndexChanged(sender As Object, e As EventArgs)
        LoadMasterInfo(2, cboClass, cboSchoolName.Text)
        cboClass.Text = ""
        cboSection.Items.Clear()

        cboSchoolName.Focus()


    End Sub
End Class