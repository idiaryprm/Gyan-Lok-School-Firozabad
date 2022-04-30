Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Imports Microsoft.Reporting.WebForms

Public Class StudentStrength
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
                Response.Redirect("Login.aspx")
            End If

        End Try

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("ActiveTab") = 2
        If IsPostBack = False Then
            InitControls()
        End If
    End Sub

    Private Sub InitControls()
        LoadMasterInfo(71, cboSchoolName, Request.Cookies("SchoolIDs").Value)
        LoadMasterInfo(2, cboClass, cboSchoolName.Text)
        cboClass.Items.Add("ALL")
        cboSection.Items.Clear()
        LoadMasterInfo(10, cboStatus)


        cboClass.Focus()

    End Sub

    Protected Sub cboClass_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboClass.SelectedIndexChanged
        LoadClassSection(cboSchoolName.Text, cboClass.Text, cboSection)
        cboSection.Items.Add("ALL")
        cboClass.Focus()
    End Sub

    Protected Sub btnShow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnShow.Click
        If cboSchoolName.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid School Name...');", True)
            cboSchoolName.Focus()
            Exit Sub
        End If
        If cboClass.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Class...');", True)
            cboClass.Focus()
            Exit Sub
        End If
        If cboSection.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Section...');", True)
            cboSection.Focus()
            Exit Sub
        End If
        If cboStatus.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Status...');", True)
            cboStatus.Focus()
            Exit Sub
        End If
        lblStatus.Text = ""

        CategoryWiseStrengthReport()

    End Sub

  
    Private Sub CategoryWiseStrengthReport()
        Dim sqlStr As String = ""
        '................................................vikash..........17/06/2016.........................................
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
        Dim myConnStr As String = System.Configuration.ConfigurationManager.ConnectionStrings("iDiaryConnectionString").ToString
        Dim myConn As New System.Data.SqlClient.SqlConnection(myConnStr)
        myConn.Open()

        Dim myCommand As New System.Data.SqlClient.SqlCommand
        Dim reportType As Integer = 0
        Try
            reportType = Request.QueryString("type")
        Catch ex As Exception

        End Try
        Dim rptName As String = ""
        Dim ClassHeader As String = "Session: " & Request.Cookies("ASName").Value

        If reportType = 4 Then
            sqlStr = "Select * from vw_Student where StatusName='" & cboStatus.Text & "' and SchoolName='" & cboSchoolName.Text & "' and ASID=" & Request.Cookies("ASID").Value
            If cboClass.Text = "ALL" And cboSection.Text = "ALL" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Cannot Select All...');", True)
                Exit Sub
            ElseIf cboClass.Text <> "ALL" And cboSection.Text = "ALL" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Cannot Select All...');", True)
                Exit Sub
            ElseIf cboClass.Text = "ALL" And cboSection.Text <> "ALL" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Cannot Select All...');", True)
                Exit Sub
            ElseIf cboClass.Text <> "ALL" And cboSection.Text <> "ALL" Then
                sqlStr &= " and ClassName='" & cboClass.Text & "' AND SecName='" & cboSection.Text & "'"
            End If
            rptName = "rptFeeNew.rdlc"
        Else
            sqlStr = " Select Count(SID) as StudentCount, ClassName, SecName, CASE WHEN Gender = 0 THEN 'Male' ELSE 'Female' END AS Gender From vw_Student where StatusName='" & cboStatus.Text & "' and SchoolName='" & cboSchoolName.Text & "' and ASID=" & Request.Cookies("ASID").Value
            If cboClass.Text = "ALL" And cboSection.Text = "ALL" Then   ''ALL-ALL (Allowed)
                'No Filter
            ElseIf cboClass.Text <> "ALL" And cboSection.Text = "ALL" Then     ''LKG-ALL(Allowed)
                sqlStr &= " and ClassName='" & cboClass.Text & "'"
            ElseIf cboClass.Text = "ALL" And cboSection.Text <> "ALL" Then     ''ALL-B(Allowed)
                sqlStr &= " and SecName='" & cboSection.Text & "'"
            ElseIf cboClass.Text <> "ALL" And cboSection.Text <> "ALL" Then     ''XXX-XXX(Allowed)
                sqlStr &= " and ClassName='" & cboClass.Text & "' AND SecName='" & cboSection.Text & "'"
            End If
            sqlStr &= " group by DisplayOrder,ClassName, SecName, Gender  Order By DisplayOrder, ClassName, SecName, Gender"



            If reportType = 2 Then
                sqlStr = "Select * from vw_Student where SchoolName='" & cboSchoolName.Text & "' and StatusName='" & cboStatus.Text & "' AND ASID=" & Request.Cookies("ASID").Value
                If cboClass.Text = "ALL" And cboSection.Text = "ALL" Then   ''ALL-ALL (Allowed)
                    'No Filter
                ElseIf cboClass.Text <> "ALL" And cboSection.Text = "ALL" Then     ''LKG-ALL(Allowed)
                    sqlStr &= " and ClassName='" & cboClass.Text & "'"
                ElseIf cboClass.Text = "ALL" And cboSection.Text <> "ALL" Then     ''ALL-B(Allowed)
                    sqlStr &= " and SecName='" & cboSection.Text & "'"
                ElseIf cboClass.Text <> "ALL" And cboSection.Text <> "ALL" Then     ''XXX-XXX(Allowed)
                    sqlStr &= " and ClassName='" & cboClass.Text & "' AND SecName='" & cboSection.Text & "'"
                End If
                rptName = "rptMarksEntry.rdlc"
                ClassHeader = "Marks Entry List for Class : " & cboClass.Text & "-" & cboSection.Text & vbNewLine & " Academic Session: " & Request.Cookies("ASName").Value
            ElseIf reportType = 3 Then
                sqlStr = "Select * from vw_Student where SchoolName='" & cboSchoolName.Text & "' and StatusName='" & cboStatus.Text & "' AND ASID=" & Request.Cookies("ASID").Value
                If cboClass.Text = "ALL" And cboSection.Text = "ALL" Then   ''ALL-ALL (Allowed)
                    'No Filter
                ElseIf cboClass.Text <> "ALL" And cboSection.Text = "ALL" Then     ''LKG-ALL(Allowed)
                    sqlStr &= " and ClassName='" & cboClass.Text & "'"
                ElseIf cboClass.Text = "ALL" And cboSection.Text <> "ALL" Then     ''ALL-B(Allowed)
                    sqlStr &= " and SecName='" & cboSection.Text & "'"
                ElseIf cboClass.Text <> "ALL" And cboSection.Text <> "ALL" Then     ''XXX-XXX(Allowed)
                    sqlStr &= " and ClassName='" & cboClass.Text & "' AND SecName='" & cboSection.Text & "'"
                End If
                rptName = "rptStudentVerification.rdlc"
                ClassHeader = "Session: " & Request.Cookies("ASName").Value
            Else
                rptName = "rptStudentStrength.rdlc"
                ClassHeader = "Session: " & Request.Cookies("ASName").Value
            End If
        End If
        

        Dim ds As New DataSet
        ds = ExecuteQuery_DataSet(sqlStr, "tbl")
        Dim rds As ReportDataSource = New ReportDataSource()
        rds.Name = "DataSet1" ' Change to what you will be using when creating an objectdatasource
        rds.Value = ds.Tables(0)
        With ReportViewer1   ' Name of the report control on the form
            .Reset()
            .ProcessingMode = ProcessingMode.Local
            .LocalReport.DataSources.Clear()
            .Visible = True
            .LocalReport.ReportPath = "Report\" & rptName
            .LocalReport.DataSources.Add(rds)
        End With


        Dim params(5) As Microsoft.Reporting.WebForms.ReportParameter
        params(0) = New Microsoft.Reporting.WebForms.ReportParameter("ASName", ClassHeader, True)
        params(1) = New Microsoft.Reporting.WebForms.ReportParameter("ClassName", cboClass.Text, True)
        params(2) = New Microsoft.Reporting.WebForms.ReportParameter("SecName", cboSection.Text, True)
        params(3) = New Microsoft.Reporting.WebForms.ReportParameter("ClassHeader", ClassHeader, True)
        params(4) = New Microsoft.Reporting.WebForms.ReportParameter("SchoolName", cboSchoolName.Text, True)
        params(5) = New Microsoft.Reporting.WebForms.ReportParameter("SchoolAddress", Address, True)
        Me.ReportViewer1.LocalReport.SetParameters(params)


        ReportViewer1.Visible = True
        ReportViewer1.LocalReport.Refresh()



    End Sub
    
    Protected Sub cboSchoolName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSchoolName.SelectedIndexChanged
        LoadMasterInfo(2, cboClass, cboSchoolName.Text)
        cboClass.Items.Add("ALL")
        cboSection.Items.Clear()
        cboSchoolName.Focus()
    End Sub
End Class