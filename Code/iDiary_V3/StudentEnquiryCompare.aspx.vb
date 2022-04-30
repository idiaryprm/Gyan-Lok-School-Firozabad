Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Imports iDiary_V3.AdmiDiary.CLS_idiary
Imports System.IO
Imports Microsoft.Reporting.WebForms

Public Class StudentEnquiryCompare
    Inherits System.Web.UI.Page

    Dim sqlstr As String = ""

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try
            If Request.Cookies("UType").Value.ToString.Contains("Student") Or Request.Cookies("UType").Value.ToString.Contains("Admin") Then
                'Allow
            Else
                Response.Redirect("/./AccessDenied.aspx", False)
            End If
        Catch ex As Exception
            Response.Redirect("~/Login.aspx")
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then

            InitControls()
        Else
          
        End If
        ''If Request.Cookies("UType").Value.ToString.Contains("Student-1") = False And Request.Cookies("UType").Value.ToString.Contains("Admin-1") = False Then
        ''    btnConverttoAdmission.Enabled = False
        ''End If
    End Sub

  
    Private Sub InitControls()
        LoadSession(cblAcademicSession)
    End Sub

    Private Sub LoadSession(ByRef myCbl As CheckBoxList)
        myCbl.Items.Clear()
        'Dim sqlStr As String = "Select ASName,ASID From AcademicSession "
        Dim sqlStr As String = "Select ASName,ASID From AcademicSession where ASID <> " & Request.Cookies("ASID").Value
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            myCbl.Items.Add(New ListItem(myReader(0), myReader(1)))
        End While
        myReader.Close()
    End Sub
    Protected Function FindDefault() As Integer
        Dim Asid As Integer = 0
        Dim sqlstr = "Select ASID from AcademicSession where IsDefault=1"
        Asid = ExecuteQuery_ExecuteScalar(sqlstr)
        Return Asid
    End Function
    Private Sub PrepareReport()
        Dim sql As String = ""
        Dim i As Integer = 0
        Dim MyHeader As String = ""
        Dim ReportPath As String = "Report/rptStudentEnquiryCompare.rdlc"
        Dim DefaultASID = FindDefault()
        If Request.Cookies("ASID").Value = DefaultASID Then
            sql = "Select Regno as SrNo,ASName,Regno ,SFirstName AS SName,MName,FName,DOB,Mobile,Address,ASID,0.0 as weightage,SchoolID From vw_Applicant Where Regno <> '' and ASID='" & DefaultASID & "'  and (SchoolID='2' or SchoolID='3') and ( ChallanDeposited=1 or RegNo in(Select RegNo from OnlinePaymentDetails ))"
            ' sql = "Select Regno as SrNo,ASName,Regno ,SFirstName AS SName,MName,FName,DOB,Mobile,Address,ASID,0.0 as weightage,SchoolID From vw_Applicant Where Regno <> '' and ASID='" & DefaultASID & "'  and (SchoolID='2' or SchoolID='3')"
        Else
            sql = "Select EnquiryNo as SrNo,ASName,EnquiryNo,SName,MName,FName,DOB,mobNo,Address,ClassName,ASID,0.0 as weightage From vw_StudentEnquiry where  ASID = " & Request.Cookies("ASID").Value
        End If


        Dim ds As New DataSet
        ds = ExecuteQuery_DataSetOnline(sql, "tbl")

        For i = 0 To cblAcademicSession.Items.Count - 1
            If cblAcademicSession.Items(i).Selected = True Then
                sql = "Select Regno as SrNo,ASName,Regno,SFirstName AS SName,MName,FName,DOB,Mobile,Address,ASID,0.0 as weightage From vw_Applicant Where Regno <> '' and ASID='" & cblAcademicSession.Items(i).Value & "'"
                '     sql = "Select EnquiryNo as SrNo, ASName,EnquiryNo,SName,MName,FName,DOB,mobNo,Address,ClassName,ASID,0.0 as weightage From vw_StudentEnquiry where ASID = " & cblAcademicSession.Items(i).Value
                Dim tDS As New DataSet
                tDS = ExecuteQuery_DataSet(sql, "tbl")
                ds = updateDS(ds, tDS)
            End If
        Next

        Dim rds As ReportDataSource = New ReportDataSource()
        rds.Name = "DataSet1" ' Change to what you will be using when creating an objectdatasource
        rds.Value = ds.Tables(0)
        With ReportViewer1   ' Name of the report control on the form
            .Reset()
            .ProcessingMode = ProcessingMode.Local
            .LocalReport.DataSources.Clear()
            .Visible = True
            .LocalReport.ReportPath = ReportPath
            .LocalReport.DataSources.Add(rds)
        End With
        MyHeader = "Student Enquiry Compare Sheet for Academic Year : " & Request.Cookies("ASName").Value
        Dim params(2) As Microsoft.Reporting.WebForms.ReportParameter
        params(0) = New Microsoft.Reporting.WebForms.ReportParameter("param", MyHeader, Visible)
        params(1) = New Microsoft.Reporting.WebForms.ReportParameter("SchoolName", FindSchoolDetails(1), Visible)
        params(2) = New Microsoft.Reporting.WebForms.ReportParameter("ASID", Request.Cookies("ASID").Value, Visible)
        Me.ReportViewer1.LocalReport.SetParameters(params)
        ReportViewer1.Visible = True
        ReportViewer1.LocalReport.Refresh()
    End Sub

    Private Function updateDS(ByVal ds As DataSet, ByVal tmpDS As DataSet) As DataSet
        Dim sqlStr As String = "", ASID As Integer = 0, dsASID As Integer = 0, SName As String = "", FName As String = "", MName As String, mobNo As String = "", address As String = "", DoB As Date = Now.Date
        Dim tSName As String = "", tFName As String = "", tMName As String, tmobNo As String = "", taddress As String = "", tDoB As Date = Now.Date, weightage As Double = 0
        Dim SrNo As String = "", nDS As New DataSet, ASName As String = "", enqNo As String = "", cnt As Integer = 0
        'nDS = ds.Copy
        Dim dt As New DataTable
        dt.Columns.Add("ASID", GetType(Integer))
        dt.Columns.Add("SrNo", GetType(String))
        dt.Columns.Add("SName", GetType(String))
        dt.Columns.Add("FName", GetType(String))
        dt.Columns.Add("MName", GetType(String))
        dt.Columns.Add("mobNo", GetType(String))
        dt.Columns.Add("address", GetType(String))
        dt.Columns.Add("DOB", GetType(Date))
        dt.Columns.Add("EnquiryNo", GetType(String))
        dt.Columns.Add("weightage", GetType(String))
        dt.Columns.Add("ASName", GetType(String))

        nDS.Tables.Add(dt)


        ASID = Request.Cookies("ASID").Value
        For Each Row As DataRow In ds.Tables(0).Rows
            dsASID = Row("ASID")
            If dsASID <> ASID Then Continue For
            SrNo = Row("SrNo")

            Try
                SName = Row("SName")
                FName = Row("FName")
            Catch ex As Exception

            End Try
            Try
                MName = Row("MName")
            Catch ex As Exception

            End Try
            Try
                mobNo = Row("mobNo")
            Catch ex As Exception

            End Try
            Try
                address = Row("address")
            Catch ex As Exception

            End Try
            Try
                DoB = Row("DOB")
            Catch ex As Exception

            End Try

            For Each tRow As DataRow In tmpDS.Tables(0).Rows
                weightage = 0
                Try
                    tSName = tRow("SName")
                    weightage += getCompare(SName, tSName, 0)
                    tFName = tRow("FName")
                    weightage += getCompare(FName, tFName, 0)
                Catch ex As Exception

                End Try
                Try
                    tMName = tRow("MName")
                    weightage += getCompare(MName, tMName, 0)
                Catch ex As Exception

                End Try
                Try
                    tmobNo = tRow("mobNo")
                    weightage += getCompare(mobNo, tmobNo, 0)
                Catch ex As Exception

                End Try
                Try
                    taddress = tRow("address")
                    weightage += getCompare(address, taddress, 0)
                Catch ex As Exception

                End Try
                Try
                    tDoB = tRow("DOB")
                    weightage += getCompare(DoB.ToString("yyyy-MM-dd"), tDoB.ToString("yyyy-MM-dd"), 1)
                Catch ex As Exception

                End Try
                If weightage <= Val(txtComFactor.Text) Then Continue For
                cnt += 1
                dt.Rows.Add(ASID, cnt, SName, FName, MName, mobNo, address, DoB, SrNo, "", "")
                'dt.Rows.Add(ASID, cnt, SName, FName, MName, mobNo, address, DoB, SrNo, "")
                Dim newRow As DataRow = nDS.Tables(0).NewRow()
                newRow("SrNo") = cnt
                newRow("EnquiryNo") = tRow("RegNo")
                newRow("SName") = tSName
                newRow("MName") = tMName
                newRow("FName") = tFName
                newRow("address") = taddress
                newRow("DOB") = tDoB
                newRow("mobno") = tmobNo
                newRow("weightage") = weightage
                newRow("ASID") = tRow("ASID")
                Try
                    ASName = cblAcademicSession.Items.FindByValue(tRow("ASID")).Text
                Catch ex As Exception

                End Try
                newRow("ASName") = ASName
                nDS.Tables(0).Rows.Add(newRow)
            Next
           
            'Dim dt As New DataTable
            'Dim colSrNo = New DataColumn("SrNo", GetType(String))
            'dt.Columns.Add(colSrNo)
            'Dim colEnquiryNo = New DataColumn("EnquiryNo", GetType(String))
            'dt.Columns.Add(colEnquiryNo)
            'Dim colSName = New DataColumn("SName", GetType(String))
            'dt.Columns.Add(colSName)
            'Dim colMName = New DataColumn("MName", GetType(String))
            'dt.Columns.Add(colMName)
            'Dim colFName = New DataColumn("FName", GetType(String))
            'dt.Columns.Add(colFName)
            'Dim colClassName = New DataColumn("ClassName", GetType(String))
            'dt.Columns.Add(colClassName)
            'Dim colDOB = New DataColumn("DOB", GetType(String))
            'dt.Columns.Add(colDOB)
            'Dim coladdress = New DataColumn("address", GetType(Int32))
            'dt.Columns.Add(coladdress)
            'Dim colSubCode = New DataColumn("SubCode", GetType(String))
            'dt.Columns.Add(colSubCode)
            'Dim colSubjectName = New DataColumn("SubjectName", GetType(String))
            'dt.Columns.Add(colSubjectName)
            'Dim colDisplayOrder = New DataColumn("DisplayOrder", GetType(String))
            'dt.Columns.Add(colDisplayOrder)
            'Dim colmyMarks = New DataColumn("myMarks", GetType(String))
            'dt.Columns.Add(colmyMarks)

            'Dim colOverAll = New DataColumn("OverAllMarks", GetType(String))
            'dt.Columns.Add(colOverAll)
            'Dim colRank = New DataColumn("OverAllSRank", GetType(String))
            'dt.Columns.Add(colRank)
            'nDS.Tables.Add(dt)
        Next
        Return nDS
    End Function

    Private Function getCompare(myVal As String, tmpVal As String, type As Integer) As Double
        'type   -> 0    -   Name
        'type   -> 1    -   Date in yyyy-MM-dd
        Dim rv As Double = 0, val1 As String = "", val2 As String = ""
        val1 = Trim(myVal)
        val2 = Trim(tmpVal)
        If val1 = "" And val2 = "" Then
            Return 0
        End If
        If type = 0 Then
            If val1 = val2 Then
                rv = 1
            Else
                Try
                    val1 = val1.Split(" ")(0)
                Catch ex As Exception

                End Try
                Try
                    val2 = val2.Split(" ")(0)
                Catch ex As Exception

                End Try
                If val1 = val2 Then
                    rv = 0.5
                Else
                    rv = 0
                End If
            End If
        ElseIf type = 1 Then
            If val1 = val2 Then
                rv = 1
            Else
                Try
                    val1 = val1.Substring(5, 5)
                Catch ex As Exception

                End Try
                Try
                    val2 = val2.Substring(5, 5)
                Catch ex As Exception

                End Try
                If val1 = val2 Then
                    rv = 0.5
                Else
                    rv = 0
                End If
            End If
        End If
        Return rv
    End Function

    Private Sub btnCompare_Click(sender As Object, e As EventArgs) Handles btnCompare.Click

        PrepareReport()
    End Sub
End Class