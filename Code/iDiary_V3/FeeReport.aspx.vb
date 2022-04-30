Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Imports iDiary_V3.iDiary_Fee.CLS_iDiary_Fee
Imports Microsoft.Reporting.WebForms
Imports System.IO

Public Class FeeCollectionReport
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        Try

            If Request.Cookies("UType").Value.ToString.Contains("Fee") Or Request.Cookies("UType").Value.ToString.Contains("Admin") Then
                'Allow
            Else
                Response.Redirect("/./AccessDenied.aspx", False)
            End If

        Catch ex As Exception

            If ex.Message.Contains("Object reference not set to an instance of an object") Then
                Response.Redirect("~/Login.aspx")
            End If

        End Try

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("ActiveTab") = 3
        If IsPostBack = False Then
            InitControls()
            Dim FeeTypes() As String = GetFeeTypeConfigID().Split("$")
            txtAdmissionFeeID.Text = FeeTypes(0)
            txtLateFeeID.Text = FeeTypes(1)
            txtConveyanceFeeID.Text = FeeTypes(2)
            txtTutionFeeID.Text = FeeTypes(3)
        End If

    End Sub

    Private Sub InitControls()
        rbCalss.Checked = True
        rbTerm.Checked = False
        btnSendSMS.Visible = False
        btnFeeDues.Visible = False
        lblFeeMsg.Visible = False
        txtMessage.Visible = False
        btnExcel.Visible = False
        GridView1.Visible = False
        chkCheckAll.Visible = False
        btnDueCirculars.Visible = False
        'txtFrom.Visible = True
        'txtTo.Visible = True
        btnReport.Visible = True
        lblReportType.Visible = True
        lblDateTo.Visible = True
        ReportViewer1.Visible = False
        btnExcel.Visible = False

        LoadMasterInfo(71, cboSchoolName, Request.Cookies("SchoolIDs").Value)
        LoadMasterInfo(2, cboClass, cboSchoolName.Text)
        cboClass.Items.Add("ALL")
        chkTerm.Items.Clear()
        LoadFeeTerms(chkTerm, 0)
        LoadMasterInfo(10, cboStatus)
        LoadMasterInfo(72, cboBank)
        cboBranch.Items.Clear()
        txtMessage.Text = GetMessage("103-FeeSMS")
        Try
            cboStatus.Text = FindDefault(10)
        Catch ex As Exception

        End Try
        cboSchoolName.Items.Add("ALL")
        'cboClass.Items.Clear()
        cboSection.Items.Clear()
        'chkTerm.Items.Clear()
        txtFrom.Text = Now.Date.ToString("dd/MM/yyyy")
        txtTo.Text = Now.Date.ToString("dd/MM/yyyy")
        ReportViewer1.Visible = False
        cboSchoolName.Focus()
    End Sub

    Protected Sub cboClassGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSchoolName.SelectedIndexChanged
        LoadMasterInfo(2, cboClass, cboSchoolName.Text)
        cboClass.Items.Add("ALL")
        LoadFeeTerms(chkTerm, 0)
        cboSchoolName.Focus()
        'If cboClassGroup.Text = "" Then
        '    chkTerm.Items.Clear()
        'Else
        '    LoadFeeTerms(chkTerm, FeeGroupId)
        'End If
    End Sub

    Protected Sub cboClass_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboClass.SelectedIndexChanged
        LoadClassSection(cboSchoolName.Text, cboClass.Text, cboSection)
        cboSection.Items.Add("ALL")
        If cboClass.Text = "ALL" Then
            cboSection.Text = "ALL"
        End If
        cboClass.Focus()
    End Sub


    Protected Sub btnTermWise_Click(sender As Object, e As EventArgs) Handles btnReport.Click
        If rbCalss.Checked = True Then
            If cboSchoolName.Text = "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid School Name...');", True)
                lblStatus.Text = "Invalid School Name..."
                cboSchoolName.Focus()
                Exit Sub
            End If
            If cboClass.Text = "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Class...');", True)
                lblStatus.Text = "Invalid Class..."
                cboClass.Focus()
                Exit Sub
            End If
            If cboSection.Text = "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Section...');", True)
                lblStatus.Text = "Invalid Section..."
                cboSection.Focus()
                Exit Sub
            End If
            If cboClass.Text = "ALL" And cboSection.Text <> "ALL" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Section (Please Select --ALL--)');", True)
                lblStatus.Text = "Invalid Section (Please Select --ALL--)"
                cboSection.Focus()
                Exit Sub
            End If
        End If

        If rbRegNo.Checked = True Then
            If txtregistrationno.Text = "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Enter Registration No......');", True)
                lblStatus.Text = "Enter Registration No......"
                txtregistrationno.Focus()
                Exit Sub
            End If
        End If
        If chkdepositemode.Checked = True Then
            If cboMode.Text = "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please select Deposit Mode...');", True)
                lblStatus.Text = "Please select Deposit Mode..."
                cboMode.Focus()
                Exit Sub
            End If
        End If

        If cboStatus.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Status...');", True)
            lblStatus.Text = "Invalid Status..."
            cboStatus.Focus()
            Exit Sub
        End If

        If chkBank.Checked = True Then
            If cboBank.Text = "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please select Bank...');", True)
                lblStatus.Text = "Please select Bank..."
                cboBank.Focus()
                Exit Sub
            End If
        End If
        If chkBranch.Checked = True Then
            If cboBranch.Text = "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please select Branch...');", True)
                lblStatus.Text = "Please select Bank..."
                cboBranch.Focus()
                Exit Sub
            End If
        End If

        Dim chkTermList As String = "'"
        If cboReportType.SelectedValue <> 13 Then
            If rbTerm.Checked = True Then

                For i = 0 To chkTerm.Items.Count - 1

                    If chkTerm.Items(i).Selected = True Then
                        chkTermList += chkTerm.Items(i).ToString & "','"
                    End If
                Next

                If chkTermList = "'" And rbTerm.Checked = True Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Select at least one Fee Term...');", True)
                    lblStatus.Text = "Please Select at least one Fee Term..."
                    chkTerm.Focus()
                    Exit Sub
                End If
            End If
        End If

        If cboReportType.SelectedValue = 13 Then
            If cboReportType.SelectedValue = 13 Then
                For i = 0 To chkTerm.Items.Count - 1
                    chkTermList += chkTerm.Items(i).ToString & "','"
                Next
            End If
        End If
        If chkTermList <> "'" Then
            chkTermList = chkTermList.Substring(0, chkTermList.Length - 2)
        End If

        Dim FromDate1 As Date = Now.Date
        Dim ToDate1 As Date = Now.Date
        If rbDate.Checked = True Then
            Try
                FromDate1 = txtFrom.Text.Substring(6, 4) & "/" & txtFrom.Text.Substring(3, 2) & "/" & txtFrom.Text.Substring(0, 2)
            Catch ex As Exception
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Date From Please Check dd/mm/yyyy Format...');", True)
                txtFrom.Focus()
                Exit Sub
            End Try

            Try
                ToDate1 = txtTo.Text.Substring(6, 4) & "/" & txtTo.Text.Substring(3, 2) & "/" & txtTo.Text.Substring(0, 2)
            Catch ex As Exception
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Date To Please Check dd/mm/yyyy Format...');", True)
                txtTo.Focus()
                Exit Sub
            End Try
            If FromDate1 > ToDate1 Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('From Date can not greater than To Date...');", True)
                txtFrom.Focus()
                Exit Sub
            End If
        End If

        If txtFrom.Text = "" Or txtTo.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Dates...');", True)
            lblStatus.Text = "Invalid Dates..."
            'cboSection.Focus()
            Exit Sub
        End If
        Dim ReportType As Integer = cboReportType.SelectedValue
        If rbTerm.Checked = False And (ReportType = 8 Or ReportType = 9 Or ReportType = 10) Then
            lblStatus.Text = "Please Select Term Option For Required Reports..."
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Select Term Option For Required Reports...');", True)
            chkTerm.Focus()
            Exit Sub
        End If
        lblStatus.Text = ""
        chkCheckAll.Visible = False
        GridView1.Visible = False
        btnSendSMS.Visible = False
        btnDueCirculars.Visible = False
        lblFeeMsg.Visible = False
        txtMessage.Visible = False
        btnExcel.Visible = False
        chkselectallstudent.Checked = True
        If ReportType = 9 Or ReportType = 13 Then
            PrepareDuesGrid(chkTermList)
        ElseIf ReportType = 8 Or ReportType = 10 Then
            PrepareDuesReport(ReportType, FromDate1.ToString("yyyy/MM/dd"), ToDate1.ToString("yyyy/MM/dd"), chkTermList)
        Else
            PrepareReport(ReportType, FromDate1.ToString("yyyy/MM/dd"), ToDate1.ToString("yyyy/MM/dd"), chkTermList)
        End If
    End Sub
    Private Sub PrepareReport(Type As Integer, FromDate1 As String, ToDate1 As String, TermList As String)
        Dim sql As String = ""
        Dim i As Integer = 0
        Dim MyHeader As String = ""
        Dim ReportPath As String = "Report/"

        If Type = 1 Or Type = 2 Then 'Collection
            sql = "Select  DepositDate, SID, regno, SName, ClassName, SecName, Sum(FeeDepositAmount) as FeeDepositAmount,PMName,FeeDepositID,ChequeBank,ChequeNo,FeeBankName,FeeBankBranchName From vw_FeeDeposit "
        ElseIf Type = 3 Or Type = 4 Or Type = 5 Or Type = 7 Then 'Head Wise Collection/Concession 
            sql = "select * from vw_FeeDeposit "
        ElseIf Type = 6 Then ' Concession
            sql = "Select  DepositDate, SID, regno, SName, ClassName, SecName, Sum(ConcessionAmount)+Sum(abs(FeeDepositAmount)) as FeeDepositAmount,FeeDepositID,FeeBankName,FeeBankBranchName From vw_FeeDeposit "
        ElseIf Type = 11 Or Type = 12 Then 'Class List Fee LAble
            sql = "Select  * From vw_Student"
        End If

        sql += " where StatusName='" & cboStatus.Text & "' AND ASID=" & Request.Cookies("ASID").Value

        If Type < 11 Then
            sql += " and isDeposit=1 "
            If chkdepositemode.Checked = True Then
                sql += " And PMName='" & cboMode.Text & "'"
            End If
            If chkBank.Checked = True And cboBank.Text <> "ALL" Then
                sql += " And FeeBankName='" & cboBank.Text & "'"
            End If
            If chkBranch.Checked = True And cboBranch.Text <> "ALL" Then
                sql += " And FeeBankBranchName='" & cboBranch.Text & "'"
            End If
            If rbDate.Checked = True Then
                sql += " and DepositDate between '" & FromDate1 & "' and '" & ToDate1 & "'"
            Else
                sql += " and TermNo in (" & TermList & ")"
            End If
        End If
        If Type = 6 Then
            sql += " and (ConcessionAmount>0 or FeeDepositAmount<0) "
        End If

        If rbRegNo.Checked = True Then
            sql += "And RegNo='" & txtregistrationno.Text & "'"
        Else
            If cboSchoolName.Text <> "ALL" Then
                sql += " And SchoolName='" & cboSchoolName.Text & "' "
            End If
            If cboClass.Text <> "ALL" Then
                sql += " And ClassName='" & cboClass.Text & "' "
            End If
            If cboClass.Text <> "ALL" And cboSection.Text <> "ALL" Then
                sql += " And SecName='" & cboSection.Text & "' "
            ElseIf cboClass.Text <> "ALL" And cboSection.Text = "ALL" Then
                sql += " And SecID in (Select distinct SecID From vw_ClassStudent where ClassName='" & cboClass.Text & "')"
            End If
        End If

        If Type = 1 Or Type = 2 Or Type = 6 Then
            sql += " Group By DepositDate, [SID], regno, SName,FName, ClassName, SecName,DisplayOrder,PMName,FeeDepositID,ChequeBank,ChequeNo,FeeBankName,FeeBankBranchName order by DisplayOrder"
        ElseIf Type = 3 Or Type = 4 Or Type = 5 Or Type = 7 Then
            sql += " Order by DepositDate,FeeDepositID"
        ElseIf Type = 11 Or Type = 12 Then
            sql += " Order by DisplayOrder,SecNAme,SName"

        End If

        Dim ds As New DataSet
        ds = ExecuteQuery_DataSet(sql, "tbl")

        'Sno append
        If Type = 4 Or Type = 3 Or Type = 5 Or Type = 7 Then
            Dim FeeDepositeID As Integer = 0
            Dim OldFeeDepositeID As Integer = 0
            Dim SNo As Integer = 0
            For Each Row As DataRow In ds.Tables(0).Rows
                FeeDepositeID = Row("FeeDepositID")
                If FeeDepositeID <> OldFeeDepositeID Then
                    SNo += 1
                    OldFeeDepositeID = FeeDepositeID
                    If Type = 7 Then
                        If Row("FeeDepositAmount") < 0 Then
                            Row("ConcessionAmount") = Row("FeeDepositAmount") * -1
                        End If
                    End If
                End If

                Row("CMNO") = SNo
            Next
        End If

        Dim rds As ReportDataSource = New ReportDataSource()
        rds.Name = "DataSet1" ' Change to what you will be using when creating an objectdatasource
        rds.Value = ds.Tables(0)
        With ReportViewer1   ' Name of the report control on the form
            .Reset()
            .ProcessingMode = ProcessingMode.Local
            .LocalReport.DataSources.Clear()
            .Visible = True
            Select Case Type
                Case 1
                    MyHeader = "Student wise Fee Collection"
                    ReportPath += "rptFeeCollection.rdlc"
                Case 2
                    MyHeader = "Class wise Fee Collection"
                    ReportPath += "rptFeeSummary.rdlc"
                Case 3
                    MyHeader = "Term wise Fee Collection"
                    ReportPath += "rptFeeCollectionTermWise.rdlc"
                Case 4
                    MyHeader = "Head wise Fee Collection"
                    ReportPath += "rptFeeCollectionHeadWise.rdlc"

                Case 5
                    MyHeader = "Summerized Head wise Fee Collection"
                    ReportPath += "rptFeeCollectionHeadWiseSummerized.rdlc"
                Case 6
                    MyHeader = "Student wise Concession"
                    ReportPath += "rptFeeCollection.rdlc"
                Case 7
                    MyHeader = "Head Wise Concession"
                    ReportPath += "rptFeeConcessionHeadWise.rdlc"
                Case 11
                    MyHeader = "List Of Students Session :" & Request.Cookies("ASName").Value
                    ReportPath += "rptClassList.rdlc"
                Case 12
                    MyHeader = "List Of Students Session :" & Request.Cookies("ASName").Value
                    ReportPath += "rptStudentLabels.rdlc"
            End Select

            .LocalReport.ReportPath = ReportPath

            .LocalReport.DataSources.Add(rds)
        End With
        If rbDate.Checked = True Then
            MyHeader += ": " & txtFrom.Text & " - " & txtTo.Text
        Else
            MyHeader += " For Terms: " & TermList.Replace("'", "")
        End If
        If chkBank.Checked = True And cboBank.Text <> "ALL" Then
            MyHeader += " Bank:" & cboBank.Text
        End If
        If chkBank.Checked = True And cboBranch.Text <> "ALL" Then
            MyHeader += " Branch:" & cboBranch.Text
        End If
        If chkdepositemode.Checked = True Then
            MyHeader += " Mode:" & cboMode.SelectedItem.Text
        End If
        Dim params(1) As Microsoft.Reporting.WebForms.ReportParameter
        params(0) = New Microsoft.Reporting.WebForms.ReportParameter("param", MyHeader, Visible)
        params(1) = New Microsoft.Reporting.WebForms.ReportParameter("SchoolName", cboSchoolName.Text, True)
        Me.ReportViewer1.LocalReport.SetParameters(params)
        ReportViewer1.Visible = True

        ReportViewer1.LocalReport.Refresh()

    End Sub

    Private Sub PrepareDuesReport(Type As Integer, FromDate1 As String, ToDate1 As String, TermList As String)
        chkselectallstudent.Visible = True
        Dim sql As String = ""
        Dim i As Integer = 0
        Dim MyHeader As String = ""
        Dim ReportPath As String = "Report/"

        sql = "Select  *  From vw_Student"
        'If Type = 10 Then
        sql += "  cross join FeeTypes "
        'End If
        sql += " where StatusName='" & cboStatus.Text & "' AND ASID=" & Request.Cookies("ASID").Value

        If rbRegNo.Checked = True Then
            sql += "And RegNo='" & txtregistrationno.Text & "'"
        Else
            If cboSchoolName.Text <> "ALL" Then
                sql += " And SchoolName='" & cboSchoolName.Text & "' "
            End If
            If cboClass.Text <> "ALL" Then
                sql += " And ClassName='" & cboClass.Text & "' "
            End If
            If cboClass.Text <> "ALL" And cboSection.Text <> "ALL" Then
                sql += " And SecName='" & cboSection.Text & "' "
            ElseIf cboClass.Text <> "ALL" And cboSection.Text = "ALL" Then
                sql += " And SecID in (Select Distinct SecID From vw_ClassStudent where ClassName='" & cboClass.Text & "')"
            End If
        End If

        sql += " Order by DisplayOrder"
        Dim ds As New DataSet
        ds = ExecuteQuery_DataSet(sql, "tbl")

        If Type < 11 Then 'Other than Class List
            ds.Tables(0).Columns.Add("ConfigAmount", System.Type.GetType("System.Decimal"))
            ds.Tables(0).Columns.Add("DepositAmount", System.Type.GetType("System.Decimal"))
            ds.Tables(0).Columns.Add("ConcessionAmount", System.Type.GetType("System.Decimal"))
            ds.Tables(0).Columns.Add("DueAmount", System.Type.GetType("System.Decimal"))

            Dim ConfigAmt As Double = 0, DepositAmt As Double = 0, DueAmt As Double = 0, ConcessionAmt As Double = 0
            For Each Row As DataRow In ds.Tables(0).Rows
                Dim SID As Integer = Row("SID")
                Dim FeeConfig As Integer = 0
                Try
                    FeeConfig = Row("FeeConfigType")
                Catch ex As Exception

                End Try
                Dim AdmissionDate As Date = Row("AdmissionDate")
                Dim FeeGroupID As Integer = Row("FeeGroupID")
                Dim FeeTypeID As Integer = Row("FeeTypeID")
                Dim DepositAmttmp As String = ""
                If FeeConfig = 0 Then
                    If FeeTypeID = txtAdmissionFeeID.Text And AdmissionFeeApplicable(SID, Request.Cookies("ASID").Value) = False Then
                        ConfigAmt = 0
                    ElseIf FeeTypeID = txtLateFeeID.Text Then
                        If Type = 10 Then
                            ConfigAmt = GetLateFeeAmount(SID, FeeGroupID, AdmissionDate.ToString("yyyy/MM/dd"), TermList.Replace("'", ""), txtAdmissionFeeID.Text, txtLateFeeID.Text, Request.Cookies("ASID").Value, Val(FeeConfig))
                        End If
                    Else
                        ConfigAmt = GetFeeConfigForFeeHead(Request.Cookies("ASID").Value, FeeGroupID, FeeTypeID, GetMonthID(Val(FeeGroupID), 0, TermList))
                    End If
                Else
                    If FeeTypeID = txtLateFeeID.Text Then
                        If Type = 10 Then
                            ConfigAmt = GetLateFeeAmount(SID, FeeGroupID, AdmissionDate.ToString("yyyy/MM/dd"), TermList.Replace("'", ""), txtAdmissionFeeID.Text, txtLateFeeID.Text, Request.Cookies("ASID").Value, FeeConfig)
                        End If
                    Else
                        ConfigAmt = GetFeeConfigForFeeHead(Request.Cookies("ASID").Value, 0, FeeTypeID, GetMonthID(Val(FeeGroupID), 0, TermList), "", SID)
                    End If
                End If

                If Type = 10 Then 'Head wise Fee Due
                    DepositAmttmp = GetFeeDepositeForStudent(SID, FeeTypeID, TermList, "", "Yes")
                    DepositAmt = DepositAmttmp.Split("/")(0)
                    ConcessionAmt = DepositAmttmp.Split("/")(1)
                    DueAmt = ConfigAmt - (DepositAmt + ConcessionAmt)
                End If



                Row("ConfigAmount") = ConfigAmt
                Row("DepositAmount") = DepositAmt
                Row("ConcessionAmount") = ConcessionAmt
                'If Type = 6 Then 'Print Concession
                '    Row("DueAmount") = ConcessionAmt
                If Type = 8 Then 'Print Config Amt
                    Row("DueAmount") = ConfigAmt
                Else
                    Row("DueAmount") = DueAmt
                End If
            Next

        End If

        'If Type = 1 Or Type = 2 Or Type = 6 Then
        '    sql += " Group By DepositDate, [SID], FeeBookNo, SName,FName, ClassName, SecName,DisplayOrder,FeeDepositID order by DisplayOrder"
        'ElseIf Type = 3 Or Type = 4 Then
        '    sql += " Order by DepositDate,FeeDepositID"
        'End If

        Dim rds As ReportDataSource = New ReportDataSource()
        rds.Name = "DataSet1" ' Change to what you will be using when creating an objectdatasource
        rds.Value = ds.Tables(0)
        With ReportViewer1   ' Name of the report control on the form
            .Reset()
            .ProcessingMode = ProcessingMode.Local
            .LocalReport.DataSources.Clear()
            .Visible = True
            Select Case Type
                'Case 6
                '    MyHeader = "Head Wise Concession"
                '    ReportPath += "rptFeeDueHeadWise.rdlc"
                Case 8
                    MyHeader = "Demand Register"
                    ReportPath += "rptFeeDueHeadWise.rdlc"
                    'Case 8
                    '    MyHeader = "Fee Dues"
                    '    ReportPath += "rptFeeDue.rdlc"
                Case 10
                    MyHeader = "Head Wise Fee Dues"
                    ReportPath += "rptFeeDueHeadWise.rdlc"
                    'Case 10
                    '    MyHeader = "Reminder Letter"
                    '    ReportPath += "rptFeeReminder.rdlc"

            End Select

            .LocalReport.ReportPath = ReportPath

            .LocalReport.DataSources.Add(rds)
        End With
        If Type < 11 Then
            If rbDate.Checked = True Then
                MyHeader += ": " & txtFrom.Text & " - " & txtTo.Text
            Else
                MyHeader += " For Terms: " & TermList.Replace("'", "")
            End If
        End If
        Dim params(1) As Microsoft.Reporting.WebForms.ReportParameter
        params(0) = New Microsoft.Reporting.WebForms.ReportParameter("param", MyHeader, Visible)
        params(1) = New Microsoft.Reporting.WebForms.ReportParameter("SchoolName", FindSchoolDetails(1), Visible)
        Me.ReportViewer1.LocalReport.SetParameters(params)
        ReportViewer1.Visible = True

        ReportViewer1.LocalReport.Refresh()

    End Sub

    Protected Sub chkAllTerm_CheckedChanged(sender As Object, e As EventArgs) Handles chkAllTerm.CheckedChanged
        For i = 0 To chkTerm.Items.Count - 1
            If chkAllTerm.Checked = True Then
                chkTerm.Items(i).Selected = True
            Else
                chkTerm.Items(i).Selected = False
            End If
        Next
    End Sub
    Protected Sub rbDate_CheckedChanged(sender As Object, e As EventArgs) Handles rbDate.CheckedChanged
        If rbDate.Checked = True Then
            txtFrom.Enabled = True
            txtTo.Enabled = True
            chkTerm.Enabled = False
            chkAllTerm.Enabled = False
        Else
            txtFrom.Enabled = False
            txtTo.Enabled = False
            chkTerm.Enabled = True
            chkAllTerm.Enabled = True
        End If
    End Sub

    Protected Sub rbTerm_CheckedChanged(sender As Object, e As EventArgs) Handles rbTerm.CheckedChanged
        If rbTerm.Checked = True Then
            txtFrom.Enabled = False
            txtTo.Enabled = False
            chkTerm.Enabled = True
            chkAllTerm.Enabled = True
        Else
            txtFrom.Enabled = True
            txtTo.Enabled = True
            chkTerm.Enabled = False
            chkAllTerm.Enabled = False
        End If
    End Sub

    Protected Sub btnFeeDues_Click(sender As Object, e As EventArgs) Handles btnFeeDues.Click
        If cboSchoolName.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Class Group...');", True)
            lblStatus.Text = "Invalid Class Group..."
            cboSchoolName.Focus()
            Exit Sub
        End If
        If cboClass.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Class...');", True)
            lblStatus.Text = "Invalid Class..."
            cboClass.Focus()
            Exit Sub
        End If
        If cboSection.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Section...');", True)
            lblStatus.Text = "Invalid Section..."
            cboSection.Focus()
            Exit Sub
        End If
        'ALL-XXX (Not Allowed...)
        If cboClass.Text = "ALL" And cboSection.Text <> "ALL" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Section (Please Select --ALL--)');", True)
            lblStatus.Text = "Invalid Section (Please Select --ALL--)"
            cboSection.Focus()
            Exit Sub
        End If
        Dim chkTermList As String = "'"
        If rbTerm.Checked = True Then
            For i = 0 To chkTerm.Items.Count - 1
                If chkTerm.Items(i).Selected = True Then
                    chkTermList += chkTerm.Items(i).ToString & "','"
                End If
            Next
            If chkTermList = "'" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Select at least one Fee Term...');", True)
                lblStatus.Text = "Please Select at least one Fee Term..."
                chkTerm.Focus()
                Exit Sub
            End If
        End If

        If cboStatus.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Status...');", True)
            lblStatus.Text = "Invalid Status..."
            cboStatus.Focus()
            Exit Sub
        End If
        lblStatus.Text = ""
        chkTermList = chkTermList.Substring(0, chkTermList.Length - 2)
        PrepareDuesGrid(chkTermList)

    End Sub
    Private Sub PrepareDuesGrid(TermList As String)
        chkselectallstudent.Visible = True
        Dim sql As String = ""
        Dim i As Integer = 0


        sql = "Select  SID,regno,FeeBookNo,SName,FName,ClassName,SecName,MobNo,AdmissionDate,FeeGroupID,FeeConfigType  From vw_Student"
        sql += " where StatusName='" & cboStatus.Text & "' AND ASID=" & Request.Cookies("ASID").Value

        If rbRegNo.Checked = True Then
            sql += "And RegNo='" & txtregistrationno.Text & "'"
        Else
            If cboSchoolName.Text <> "ALL" Then
                sql += " And SchoolName='" & cboSchoolName.Text & "' "
            End If
            If cboClass.Text <> "ALL" Then
                sql += " And ClassName='" & cboClass.Text & "' "
            End If
            If cboClass.Text <> "ALL" And cboSection.Text <> "ALL" Then
                sql += " And SecName='" & cboSection.Text & "' "
            ElseIf cboClass.Text <> "ALL" And cboSection.Text = "ALL" Then
                sql += " And SecID in (Select Distinct SecID From vw_ClassStudent where ClassName='" & cboClass.Text & "')"
            End If
        End If
        sql += " Order by DisplayOrder"
        Dim ds As New DataSet
        ds = ExecuteQuery_DataSet(sql, "tbl")

        ds.Tables(0).Columns.Add("ConfigAmount", System.Type.GetType("System.Decimal"))
        ds.Tables(0).Columns.Add("DepositAmount", System.Type.GetType("System.Decimal"))
        ds.Tables(0).Columns.Add("ConcessionAmount", System.Type.GetType("System.Decimal"))
        ds.Tables(0).Columns.Add("DueAmount", System.Type.GetType("System.Decimal"))

        Dim ConfigAmt As Double = 0, DepositAmt As Double = 0, DueAmt As Double = 0, ConcessionAmt As Double = 0
        Dim nDS As New DataSet
        nDS = ds.Clone
        For Each Row As DataRow In ds.Tables(0).Rows
            Dim SID As Integer = Row("SID")
            Dim FeeConfig As Integer = 0
            Try
                FeeConfig = Row("FeeConfigType")
            Catch ex As Exception

            End Try
            Dim AdmissionDate As Date = Row("AdmissionDate")
            Dim FeeGroupID As Integer = Row("FeeGroupID")
            'Dim FeeTypeID As Integer = Row("FeeTypeID")
            Dim DepositAmttmp As String = ""
            If FeeConfig = 0 Then
                Dim LateFeeAmount As Double = GetLateFeeAmount(SID, FeeGroupID, AdmissionDate.ToString("yyyy/MM/dd"), TermList.Replace("'", ""), txtAdmissionFeeID.Text, txtLateFeeID.Text, Request.Cookies("ASID").Value, Val(FeeConfig))
                ConfigAmt = GetFeeConfigForFeeHead(Request.Cookies("ASID").Value, FeeGroupID, 0, GetMonthID(Val(FeeGroupID), 0, TermList))
                If AdmissionFeeApplicable(SID, Request.Cookies("ASID").Value) = False Then
                    ConfigAmt = ConfigAmt - GetFeeConfigForFeeHead(Request.Cookies("ASID").Value, FeeGroupID, txtAdmissionFeeID.Text, GetMonthID(Val(FeeGroupID), 0, TermList))
                End If
                ConfigAmt = ConfigAmt + LateFeeAmount
            Else
                Dim LateFeeAmount As Double = GetLateFeeAmount(SID, FeeGroupID, AdmissionDate.ToString("yyyy/MM/dd"), TermList.Replace("'", ""), txtAdmissionFeeID.Text, txtLateFeeID.Text, Request.Cookies("ASID").Value, FeeConfig)
                ConfigAmt = GetFeeConfigForFeeHead(Request.Cookies("ASID").Value, 0, 0, GetMonthID(Val(FeeGroupID), 0, TermList), "", SID)
                ConfigAmt = ConfigAmt + LateFeeAmount
            End If

            DepositAmttmp = GetFeeDepositeForStudent(SID, 0, TermList, "", "yes")
            DepositAmt = DepositAmttmp.Split("/")(0)
            ConcessionAmt = DepositAmttmp.Split("/")(1)
            DueAmt = ConfigAmt - (DepositAmt + ConcessionAmt)

            Row("ConfigAmount") = ConfigAmt
            Row("DepositAmount") = DepositAmt
            Row("ConcessionAmount") = ConcessionAmt
            Row("DueAmount") = DueAmt
            ' If DueAmt = 0 Then
            'Else
            nDS.Tables(0).Rows.Add(Row.ItemArray)
            'End If
        Next

        GridView1.DataSource = nDS
        GridView1.DataBind()

        If ds.Tables(0).Rows.Count > 0 Then
            GridView1.Visible = True
            btnExcel.Visible = True
            Dim MyHeader As String = "Reminder Letter"
            Dim ReportPath As String = "Report/rptFeeReminder.rdlc"
            Dim rds As ReportDataSource = New ReportDataSource()
            rds.Name = "DataSet1" ' Change to what you will be using when creating an objectdatasource
            rds.Value = ds.Tables(0)
            With ReportViewer1   ' Name of the report control on the form
                .Reset()
                .ProcessingMode = ProcessingMode.Local
                .LocalReport.DataSources.Clear()
                .Visible = False
                .LocalReport.ReportPath = ReportPath
                .LocalReport.DataSources.Add(rds)
            End With

            MyHeader += " For Terms: " & TermList.Replace("'", "")

            Dim params(1) As Microsoft.Reporting.WebForms.ReportParameter
            params(0) = New Microsoft.Reporting.WebForms.ReportParameter("param", MyHeader, Visible)
            params(1) = New Microsoft.Reporting.WebForms.ReportParameter("SchoolName", FindSchoolDetails(1), Visible)
            Me.ReportViewer1.LocalReport.SetParameters(params)
            ReportViewer1.Visible = True
            ReportViewer1.LocalReport.Refresh()

            If SMSFaciltyOpted() = 0 Then
                chkCheckAll.Visible = False
                btnSendSMS.Visible = False
                btnDueCirculars.Visible = False
                lblFeeMsg.Visible = False
                txtMessage.Visible = False
            Else
                chkCheckAll.Visible = True
                btnSendSMS.Visible = True
                'btnDueCirculars.Visible = True
                lblFeeMsg.Visible = True
                txtMessage.Visible = True
            End If
        Else
            GridView1.Visible = False
            chkCheckAll.Visible = False
            btnDueCirculars.Visible = False
            lblFeeMsg.Visible = False
            txtMessage.Visible = False
            btnSendSMS.Visible = False
        End If
        GetTotalAmount()
    End Sub
    'Private Sub PrepareDuesGrid(TermList As String)
    '    chkselectallstudent.Visible = True
    '    Dim sql As String = ""
    '    Dim i As Integer = 0


    '    sql = "Select  SID,regno,FeeBookNo,SName,FName,ClassName,SecName,MobNo,AdmissionDate,FeeGroupID,FeeConfigType  From vw_Student"
    '    sql += " where StatusName='" & cboStatus.Text & "' AND ASID=" & Request.Cookies("ASID").Value

    '    If rbRegNo.Checked = True Then
    '        sql += "And RegNo='" & txtregistrationno.Text & "'"
    '    Else
    '        If cboClassGroup.Text <> "ALL" Then
    '            sql += " And SchoolName='" & cboClassGroup.Text & "' "
    '        End If
    '        If cboClass.Text <> "ALL" Then
    '            sql += " And ClassName='" & cboClass.Text & "' "
    '        End If
    '        If cboClass.Text <> "ALL" And cboSection.Text <> "ALL" Then
    '            sql += " And SecName='" & cboSection.Text & "' "
    '        ElseIf cboClass.Text <> "ALL" And cboSection.Text = "ALL" Then
    '            sql += " And SecID in (Select Distinct SecID From vw_ClassStudent where ClassName='" & cboClass.Text & "')"
    '        End If
    '    End If
    '    sql += " Order by DisplayOrder"
    '    Dim ds As New DataSet
    '    ds = ExecuteQuery_DataSet(sql, "tbl")

    '    ds.Tables(0).Columns.Add("ConfigAmount", System.Type.GetType("System.Decimal"))
    '    ds.Tables(0).Columns.Add("DepositAmount", System.Type.GetType("System.Decimal"))
    '    ds.Tables(0).Columns.Add("ConcessionAmount", System.Type.GetType("System.Decimal"))
    '    ds.Tables(0).Columns.Add("DueAmount", System.Type.GetType("System.Decimal"))

    '    Dim ConfigAmt As Double = 0, DepositAmt As Double = 0, DueAmt As Double = 0, ConcessionAmt As Double = 0
    '    Dim nDS As New DataSet
    '    nDS = ds.Clone
    '    For Each Row As DataRow In ds.Tables(0).Rows
    '        Dim SID As Integer = Row("SID")
    '        Dim FeeConfig As Integer = 0
    '        Try
    '            FeeConfig = Row("FeeConfigType")
    '        Catch ex As Exception

    '        End Try
    '        Dim AdmissionDate As Date = Row("AdmissionDate")
    '        Dim FeeGroupID As Integer = Row("FeeGroupID")
    '        'Dim FeeTypeID As Integer = Row("FeeTypeID")
    '        Dim DepositAmttmp As String = ""
    '        If FeeConfig = 0 Then
    '            Dim LateFeeAmount As Double = GetLateFeeAmount(SID, FeeGroupID, AdmissionDate.ToString("yyyy/MM/dd"), TermList.Replace("'", ""), txtAdmissionFeeID.Text, txtLateFeeID.Text, Request.Cookies("ASID").Value, Val(FeeConfig))
    '            ConfigAmt = GetFeeConfigForFeeHead(Request.Cookies("ASID").Value, FeeGroupID, 0, GetMonthID(Val(FeeGroupID), 0, TermList))
    '            If AdmissionFeeApplicable(SID, Request.Cookies("ASID").Value) = False Then
    '                ConfigAmt = ConfigAmt - GetFeeConfigForFeeHead(Request.Cookies("ASID").Value, FeeGroupID, txtAdmissionFeeID.Text, GetMonthID(Val(FeeGroupID), 0, TermList))
    '            End If
    '            ConfigAmt = ConfigAmt + LateFeeAmount
    '        Else
    '            Dim LateFeeAmount As Double = GetLateFeeAmount(SID, FeeGroupID, AdmissionDate.ToString("yyyy/MM/dd"), TermList.Replace("'", ""), txtAdmissionFeeID.Text, txtLateFeeID.Text, Request.Cookies("ASID").Value, FeeConfig)
    '            ConfigAmt = GetFeeConfigForFeeHead(Request.Cookies("ASID").Value, 0, 0, GetMonthID(Val(FeeGroupID), 0, TermList), "", SID)
    '            ConfigAmt = ConfigAmt + LateFeeAmount
    '        End If

    '        DepositAmttmp = GetFeeDepositeForStudent(SID, 0, TermList)
    '        DepositAmt = DepositAmttmp.Split("/")(0)
    '        ConcessionAmt = DepositAmttmp.Split("/")(1)
    '        DueAmt = ConfigAmt - (DepositAmt + ConcessionAmt)

    '        Row("ConfigAmount") = ConfigAmt
    '        Row("DepositAmount") = DepositAmt
    '        Row("ConcessionAmount") = ConcessionAmt
    '        Row("DueAmount") = DueAmt
    '        ' If DueAmt = 0 Then
    '        'Else
    '        nDS.Tables(0).Rows.Add(Row.ItemArray)
    '        'End If
    '    Next

    '    GridView1.DataSource = nDS
    '    GridView1.DataBind()

    '    If ds.Tables(0).Rows.Count > 0 Then
    '        GridView1.Visible = True
    '        btnExcel.Visible = True
    '        Dim MyHeader As String = "Reminder Letter"
    '        Dim ReportPath As String = "Report/rptFeeReminder.rdlc"
    '        Dim rds As ReportDataSource = New ReportDataSource()
    '        rds.Name = "DataSet1" ' Change to what you will be using when creating an objectdatasource
    '        rds.Value = ds.Tables(0)
    '        With ReportViewer1   ' Name of the report control on the form
    '            .Reset()
    '            .ProcessingMode = ProcessingMode.Local
    '            .LocalReport.DataSources.Clear()
    '            .Visible = False
    '            .LocalReport.ReportPath = ReportPath
    '            .LocalReport.DataSources.Add(rds)
    '        End With

    '        MyHeader += " For Terms: " & TermList.Replace("'", "")

    '        Dim params(1) As Microsoft.Reporting.WebForms.ReportParameter
    '        params(0) = New Microsoft.Reporting.WebForms.ReportParameter("param", MyHeader, Visible)
    '        params(1) = New Microsoft.Reporting.WebForms.ReportParameter("SchoolName", FindSchoolDetails(1), Visible)
    '        Me.ReportViewer1.LocalReport.SetParameters(params)
    '        ReportViewer1.Visible = True
    '        ReportViewer1.LocalReport.Refresh()

    '        If SMSFaciltyOpted() = 0 Then
    '            chkCheckAll.Visible = False
    '            btnSendSMS.Visible = False
    '            btnDueCirculars.Visible = False
    '            lblFeeMsg.Visible = False
    '            txtMessage.Visible = False
    '        Else
    '            chkCheckAll.Visible = True
    '            btnSendSMS.Visible = True
    '            'btnDueCirculars.Visible = True
    '            lblFeeMsg.Visible = True
    '            txtMessage.Visible = True
    '        End If
    '    Else
    '        GridView1.Visible = False
    '        chkCheckAll.Visible = False
    '        btnDueCirculars.Visible = False
    '        lblFeeMsg.Visible = False
    '        txtMessage.Visible = False
    '        btnSendSMS.Visible = False
    '    End If
    '    GetTotalAmount()
    'End Sub

    Protected Sub chkCheckAll_CheckedChanged(sender As Object, e As EventArgs) Handles chkCheckAll.CheckedChanged
        Dim myVal As Boolean = False
        Dim i As Integer = 0, myCount As Integer = 0

        If chkCheckAll.Checked = True Then myVal = True

        For i = 0 To GridView1.Rows.Count - 1

            DirectCast(GridView1.Rows(i).FindControl("chkSelect"), CheckBox).Checked = myVal

            myCount += 1
        Next
    End Sub

    Protected Sub btnSendSMS_Click(sender As Object, e As EventArgs) Handles btnSendSMS.Click
        If txtMessage.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Enter Message...');", True)
            txtMessage.Focus()
            Exit Sub
        End If
        Dim Counter As Integer = 0
        For i = 0 To GridView1.Rows.Count - 1
            Dim chk As CheckBox = DirectCast(GridView1.Rows(i).FindControl("chkSelect"), CheckBox)
            If chk.Checked = True And GridView1.Rows(i).Visible = True Then
                Counter = 1
                Exit For
            End If
        Next
        If Counter = 0 Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Select atleast one Student for Send SMS...');", True)
            GridView1.Focus()
            Exit Sub
        End If

        Dim sqlStr As String = ""
        Dim MobNo As String = "", SName As String = "", myMessage As String = ""
        Dim SMSResponse As String = "", SenderID As String = ""

        sqlStr = "Select SMSSender From SMSSender where isdefault=1"
        Dim SMSReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While SMSReader.Read
            SenderID = SMSReader(0)
            Exit While
        End While
        SMSReader.Close()
        Dim Term As String = ""
        For j = 0 To chkTerm.Items.Count - 1
            If chkTerm.Items(j).Selected = True Then
                Term = chkTerm.Items(j).ToString
            End If
        Next
        For i = 0 To GridView1.Rows.Count - 1
            Dim chk As CheckBox = DirectCast(GridView1.Rows(i).FindControl("chkSelect"), CheckBox)
            If chk.Checked = True And GridView1.Rows(i).Visible = True Then
                'Dim SID As Integer = Convert.ToInt32(GridView1.Rows(i).Cells(0).Text)
                SName = GridView1.Rows(i).Cells(5).Text
                Dim ClassName As String = GridView1.Rows(i).Cells(7).Text & "-" & GridView1.Rows(i).Cells(8).Text
                Dim RegNo As String = GridView1.Rows(i).Cells(4).Text
                Dim DueAmount As Double = Convert.ToInt32(GridView1.Rows(i).Cells(12).Text)
                MobNo = GridView1.Rows(i).Cells(13).Text

                myMessage = SQLFixup(txtMessage.Text)
                myMessage = myMessage.Replace("<*>", SName)
                myMessage = myMessage.Replace("<**>", ClassName)
                myMessage = myMessage.Replace("<***>", RegNo)
                myMessage = myMessage.Replace("<****>", DueAmount)
                myMessage = myMessage.Replace("<*****>", Term)
                If DueAmount > 0 Then
                    SMSResponse = SendMySMS(SenderID, MobNo, myMessage)
                End If
            End If
        Next
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Defaulter Message has been send...');", True)
    End Sub


    Protected Sub btnDueCirculars_Click(sender As Object, e As EventArgs) Handles btnDueCirculars.Click
        ReportViewer1.Focus()
        txtAdmissionFeeID.Focus()
    End Sub

    Protected Sub btnExcel_Click(sender As Object, e As EventArgs) Handles btnExcel.Click
        Dim sw As New StringWriter()
        Dim hw As New System.Web.UI.HtmlTextWriter(sw)
        Dim frm As HtmlForm = New HtmlForm()

        Dim filename As String = "SearchResult_" + DateTime.Now.ToString() + ".xls"

        Page.Response.AddHeader("content-disposition", "attachment;filename=" + filename)
        Page.Response.ContentType = "application/vnd.ms-excel"
        Page.Response.Charset = ""
        Page.EnableViewState = False
        frm.Attributes("runat") = "server"
        Controls.Add(frm)

        frm.Controls.Add(GridView1)
        frm.RenderControl(hw)
        Response.Write(sw.ToString())
        Response.End()

    End Sub

    Protected Sub chkselectallstudent_CheckedChanged(sender As Object, e As EventArgs) Handles chkselectallstudent.CheckedChanged
        If chkselectallstudent.Checked = True Then
            Dim i As Integer = 1
            For Each gvr As GridViewRow In GridView1.Rows
                If gvr.Visible = False Then
                    gvr.Visible = True
                    gvr.Cells(2).Text = i
                    i = i + 1
                Else
                    gvr.Cells(2).Text = i
                    i = i + 1
                End If
            Next
        Else
            Dim i As Integer = 1
            For Each gvr As GridViewRow In GridView1.Rows

                If gvr.Cells(12).Text = 0 Then
                    gvr.Visible = False
                Else
                    gvr.Visible = True
                    gvr.Cells(2).Text = i
                    i = i + 1
                End If
            Next
        End If
        GetTotalAmount()
    End Sub
    Private Sub GetTotalAmount()
        Dim ConfigSum As Double = 0
        Dim DepositAmountSum As Double = 0
        Dim ConcessionAmountSum As Double = 0
        Dim DuesAmountSum As Double = 0

            For Each gvr As GridViewRow In GridView1.Rows
            If gvr.Visible = False Then
                'ConfigSum = ConfigSum + gvr.Cells(9).Text
                'DepositAmountSum = DepositAmountSum + gvr.Cells(10).Text
                'ConcessionAmountSum = ConcessionAmountSum + gvr.Cells(11).Text
                'DuesAmountSum = DuesAmountSum + gvr.Cells(12).Text


            Else
                ConfigSum = ConfigSum + gvr.Cells(9).Text
                DepositAmountSum = DepositAmountSum + gvr.Cells(10).Text
                ConcessionAmountSum = ConcessionAmountSum + gvr.Cells(11).Text
                DuesAmountSum = DuesAmountSum + gvr.Cells(12).Text

            End If
        Next
        Try
            GridView1.FooterRow.Cells(9).Text = FormatNumber(ConfigSum)
            GridView1.FooterRow.Cells(10).Text = FormatNumber(DepositAmountSum)
            GridView1.FooterRow.Cells(11).Text = FormatNumber(ConcessionAmountSum)
            GridView1.FooterRow.Cells(12).Text = FormatNumber(DuesAmountSum)
            GridView1.FooterRow.Cells(6).Text = "Total"
            GridView1.FooterRow.BackColor = Drawing.Color.Yellow
        Catch ex As Exception

        End Try
        


    End Sub

    Protected Sub cboBank_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboBank.SelectedIndexChanged
        Dim BankID As Integer = FindMasterID(72, cboBank.Text)
        LoadFeeBankBranch(BankID, cboBranch)
        cboBranch.Items.Add("ALL")
        cboBank.Focus()
    End Sub
End Class