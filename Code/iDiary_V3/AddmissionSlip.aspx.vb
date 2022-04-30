Imports Microsoft.Reporting.WebForms
Imports System.Data.SqlClient
Imports iDiary_V3.iDiary_Date.CLS_iDiary_Date
Imports iDiary_V3.iDiary.CLS_idiary

Public Class AddmissionSlip
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim SID As String = Request.QueryString("SID")
            loadReport(SID)
            '   loadReportBonafide(SID)

        End If
    End Sub

    Private Sub loadReport(SID As Integer)
        Dim Sql As String = "Select * from vw_Student where SID=" & SID
        Dim ds As New DataSet
        ds = ExecuteQuery_DataSet(Sql, "tbl")
        Dim rds As ReportDataSource = New ReportDataSource()

        rds.Name = "DataSet1" ' Change to what you will be using when creating an objectdatasource
        '    End If

        rds.Value = ds.Tables(0)
        With ReportViewer1   ' Name of the report control on the form
            .Reset()
            .ProcessingMode = ProcessingMode.Local
            .LocalReport.DataSources.Clear()
            .Visible = True
            .LocalReport.ReportPath = "Report/rptAddmissionSlip.rdlc"
            .LocalReport.DataSources.Add(rds)
        End With
        Dim params(1) As Microsoft.Reporting.WebForms.ReportParameter
        params(0) = New Microsoft.Reporting.WebForms.ReportParameter("param", "param", Visible)
        params(1) = New Microsoft.Reporting.WebForms.ReportParameter("SchoolName", FindSchoolDetails(1), Visible)

        Me.ReportViewer1.LocalReport.SetParameters(params)
        ReportViewer1.Visible = True
        ReportViewer1.LocalReport.Refresh()
    End Sub

    'Private Sub loadReportBonafide(SID As Integer)
    '   
    '   
    '   

    '    Dim sqlStr As String = "Select DOB From Student Where SID='" & SID & "'"
    '    (sqlStr, myConn)
    '    Dim rv As Date = Now.Date
    '    rv = ExecuteQuery_ExecuteScalar(SqlStr)
    '    
    '    
    '    Dim param As String = ConvertDateToWords(rv.Day.ToString("00"), rv.Month.ToString("00"), rv.Year.ToString("0000"))
    '    Dim Sql As String = "Select * from vw_Student where SID=" & SID
    '    Dim ds As New DataSet
    '    ds = ExecuteQuery_DataSet(Sql, "tbl")
    '    Dim rds As ReportDataSource = New ReportDataSource()

    '    rds.Name = "DataSet1" ' Change to what you will be using when creating an objectdatasource
    '    '    End If

    '    rds.Value = ds.Tables(0)
    '    With ReportViewer2   ' Name of the report control on the form
    '        .Reset()
    '        .ProcessingMode = ProcessingMode.Local
    '        .LocalReport.DataSources.Clear()
    '        .Visible = True
    '        .LocalReport.ReportPath = "rptBonafied_Cert.rdlc"
    '        .LocalReport.DataSources.Add(rds)
    '    End With
    '    Dim params(0) As Microsoft.Reporting.WebForms.ReportParameter
    '    params(0) = New Microsoft.Reporting.WebForms.ReportParameter("param", param, Visible)


    '    Me.ReportViewer2.LocalReport.SetParameters(params)
    '    ReportViewer2.Visible = True

    '    ReportViewer2.LocalReport.Refresh()

    'End Sub

   
End Class