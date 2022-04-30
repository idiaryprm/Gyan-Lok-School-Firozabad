Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Imports iDiary_V3.iDiary_Fee.CLS_iDiary_Fee
Imports Microsoft.Reporting.WebForms

Public Class Cert_Fee
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cookies("ActiveTab").Value = 6
        Response.Cookies("ActiveTab").Expires = DateTime.Now.AddHours(1)
        Session("ActiveTab") = 6
        If IsPostBack = False Then
            InitControls()
        End If
    End Sub

    Private Sub InitControls()
        txtAdminNo.Text = ""
        LoadMasterInfo(11, cboFeeType)
        txtSName.Text = ""
        txtFName.Text = ""
        txtClass.Text = ""
        txtSchoolName.Text = ""
        ReportViewer1.Visible = False
        lblHeaderImg.Text = ""
        txtFeeBookNo.Focus()
    End Sub

    Protected Sub btnFind_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFind.Click

        If txtFeeBookNo.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Fee Book No is required...');", True)
            txtFeeBookNo.Focus()
            Exit Sub
        End If

        FillStudentDetail(txtFeeBookNo.Text, 1)
        txtFeeBookNo.Focus()
    End Sub
    Private Sub FillStudentDetail(feebookNo As String, Type As Integer)
        Dim sqlStr As String = ""

        sqlStr = "Select SchoolHeaderImage,FeeBookNo,regno, SName, FName, MName, AdmissionDate, DOB, ClassName,SecName,Gender,ClassRollNo,SID,SchoolName From vw_Student Where ASID=" & Request.Cookies("ASID").Value
        If Type = 1 Then
            sqlStr += " and Feebookno='" & feebookNo & "'"
        Else
            sqlStr += " and Regno='" & feebookNo & "'"
        End If


        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)

        While myReader.Read
            lblHeaderImg.Text = myReader("SchoolHeaderImage").ToString
            txtSID.Text = myReader("SID")
            txtFeeBookNo.Text = myReader("FeeBookNo")
            txtAdminNo.Text = myReader("regno")
            txtGender.Text = myReader("Gender")
            txtSName.Text = myReader("SName")
            txtFName.Text = myReader("FName")
            txtMName.Text = myReader("MName")
            txtClass.Text = myReader("ClassName") & "-" & myReader("SecName")
            Try
                txtSchoolName.Text = myReader("SchoolName")
            Catch ex As Exception

            End Try
        End While
        myReader.Close()
    End Sub
    Protected Sub btnGenerate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerate.Click

        If CheckBoxList1.SelectedValue = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please select a Fee Type...');", True)
            cboFeeType.Focus()
            Exit Sub
        End If
        Dim sqlStr As String = ""

        Dim TotalFeeAmount As Double = 0
        Dim SessionStart As String = Request.Cookies("ASName").Value.ToString.Substring(0, Request.Cookies("ASName").Value.ToString.IndexOf("-"))
        Dim SessionEnd As String = Request.Cookies("ASName").Value.ToString.Substring(Request.Cookies("ASName").Value.ToString.IndexOf("-") + 1, Request.Cookies("ASName").Value.ToString.Length - Request.Cookies("ASName").Value.ToString.IndexOf("-") - 1)
        Select Case SessionEnd.Length
            Case 2 : SessionEnd = "20" & SessionEnd
        End Select

        sqlStr = "Select Sum(FeeDepositAmount) From vw_FeeDeposit Where " & _
            " SID=" & txtSID.Text & " and feetypeid<>1 AND ("
        For j = 0 To CheckBoxList1.Items.Count - 1
            If CheckBoxList1.Items(j).Selected = True Then
                sqlStr += " FeeTypeName='" & CheckBoxList1.Items(j).ToString & "' or "

            End If
        Next
        sqlStr += ")"
        sqlStr = sqlStr.Remove(sqlStr.Length - 4, 2)

        sqlStr += " group by RegNo"
        Try
            TotalFeeAmount = ExecuteQuery_ExecuteScalar(sqlStr)
        Catch ex As Exception
            TotalFeeAmount = -1
        End Try

        sqlStr = ""
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


        Dim Matter1 As String = "", Matter2 As String = ""
        ' If txtGender.Text = "0" Then
        '     Matter1 = "This is to certify that " & txtSName.Text & " S/O Mr. " & _
        'txtFName.Text & " & Mrs. " & txtMName.Text & " is a bonafide student of " & SchoolName & " " & Address & ", and his admission No. is " & txtAdminNo.Text & ". " & _
        '"He has paid " & TotalFeeAmount & "/- (" & GetNumberAsWords(TotalFeeAmount) & ") as " & cboFeeType.Text & " for the academic year ( " & SessionStart & "-" & SessionEnd & ")"
        ' Else
        '     Matter1 = "This is to certify that Miss " & txtSName.Text & " D/O Mr. " & _
        '  txtFName.Text & " & Mrs. " & txtMName.Text & " is a bonafide student of " & SchoolName & " " & Address & ", and her admission No. is " & txtAdminNo.Text & ". " & _
        '  "She has paid " & TotalFeeAmount & "/- (" & GetNumberAsWords(TotalFeeAmount) & ") as " & cboFeeType.Text & " for the academic year ( " & SessionStart & "-" & SessionEnd & ")"

        ' End If

        Dim sql As String = "Select * From vw_FeeDeposit Where  SID=" & txtSID.Text & " AND ("
        For j = 0 To CheckBoxList1.Items.Count - 1
            If CheckBoxList1.Items(j).Selected = True Then
                sql += " FeeTypeName='" & CheckBoxList1.Items(j).ToString & "' or "

            End If
        Next
        sql += ")"
        sql = sql.Remove(sql.Length - 4, 2)

        'Dim Sql As String = "Select * from vw_Student where SID=" & txtSID.Text
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
            .LocalReport.ReportPath = "Report/rptFeeCert.rdlc"
            .LocalReport.DataSources.Add(rds)
            .LocalReport.EnableExternalImages = True
            End With

        Dim params(4) As Microsoft.Reporting.WebForms.ReportParameter
        params(0) = New Microsoft.Reporting.WebForms.ReportParameter("headerPath", Server.MapPath(lblHeaderImg.Text), Visible)
        params(1) = New Microsoft.Reporting.WebForms.ReportParameter("DOBHindi", "", Visible)
        params(2) = New Microsoft.Reporting.WebForms.ReportParameter("Conduct", "", Visible)
        params(3) = New Microsoft.Reporting.WebForms.ReportParameter("Amount", TotalFeeAmount, Visible)
        params(4) = New Microsoft.Reporting.WebForms.ReportParameter("AmountinWords", GetNumberAsWords(TotalFeeAmount), Visible)

        Me.ReportViewer1.LocalReport.SetParameters(params)
        ReportViewer1.Visible = True

        ReportViewer1.LocalReport.Refresh()

        SaveCertificateDetails(5)
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
        SqlDataSource1.SelectCommand = "SELECT FeeBookNo, SName, ClassName, SecName, FName, MName, AdmissionDate, DOB,SchoolName FROM vw_Student WHERE ASID = " & Request.Cookies("ASID").Value & " and SName Like '%" & txtSName.Text & "%'"
        GridView1.DataBind()
    End Sub
    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged
        txtAdminNo.Text = GridView1.SelectedRow.Cells(1).Text
        FillStudentDetail(txtAdminNo.Text, 1)
        GridView1.Visible = False
    End Sub

    Protected Sub btnRegNo_Click(sender As Object, e As EventArgs) Handles btnRegNo.Click
        If txtAdminNo.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Reg No is required...');", True)
            txtAdminNo.Focus()
            Exit Sub
        End If

        FillStudentDetail(txtAdminNo.Text, 2)
        txtAdminNo.Focus()
    End Sub
End Class