Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary

Public Class SalarySlip
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then InitControls()
    End Sub

    Private Sub InitControls()
        cboEmpName.Items.Clear()
        LoadEmployees(0, "", cboEmpName)
        LoadMonths(cboMonth)
        LoadYears(cboYear)
        ReportViewer1.Visible = False
    End Sub

    Protected Sub btnGenerate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        If optSpecific.Checked And cboEmpName.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Select a employee to continue...');", True)
            cboEmpName.Focus()
            Exit Sub
        End If

        If cboMonth.SelectedIndex = 0 Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Select a month to continue...');", True)
            cboMonth.Focus()
            Exit Sub
        End If

        If cboYear.SelectedIndex = 0 Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Select a Year to continue...');", True)
            cboYear.Focus()
            Exit Sub
        End If

        Dim lstInsert As New ListBox
        Dim lstEarID As New ListBox
        Dim lstEarName As New ListBox
        Dim lstDedID As New ListBox
        Dim lstDedName As New ListBox

        Dim sqlStr As String = "", i As Integer = 0
        Dim EmpName As String = "", DeptName As String = "", DesgName As String = ""
        Dim GradeName As String = "", AGPName As String = "", AccNo As String = ""

        lstInsert.Items.Clear()
        lstEarID.Items.Clear()
        lstEarName.Items.Clear()
        lstDedID.Items.Clear()
        lstDedName.Items.Clear()

       
        
       

        

        sqlStr = "Delete From rptSalarySlipHeadPart"
        
        
        ExecuteQuery_Update(SqlStr)

        sqlStr = "Delete From rptSalarySlipEarningPart"
        
        
        ExecuteQuery_Update(SqlStr)

        sqlStr = "Delete From rptSalarySlipDeductionPart"
        
        
        ExecuteQuery_Update(SqlStr)

        Dim lstEmpID As New ListBox

        If optSpecific.Checked Then
            lstEmpID.Items.Add(FindEmployeeID(0, "", cboEmpName.Text))
        ElseIf optAll.Checked Then
            sqlStr = "Select EmpID From EmployeeMaster"
            
            
            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            While myReader.Read
                lstEmpID.Items.Add(myReader(0))
            End While
            myReader.Close()
        End If

        sqlStr = "Select HeadID, HeadName From SalaryHeadMaster Where HeadType=1  Order By HeadID, HeadName"
        
        
        Dim EarHeadReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While EarHeadReader.Read
            lstEarID.Items.Add(EarHeadReader(0))
            lstEarName.Items.Add(EarHeadReader(1))
        End While
        EarHeadReader.Close()

        sqlStr = "Select HeadID, HeadName From SalaryHeadMaster Where HeadType=2  Order By HeadID, HeadName"
        
        
        Dim DedHeadReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While DedHeadReader.Read
            lstDedID.Items.Add(DedHeadReader(0))
            lstDedName.Items.Add(DedHeadReader(1))
        End While
        DedHeadReader.Close()

        For i = 0 To lstEmpID.Items.Count - 1

            sqlStr = "Select EmpName, DeptName, DesgName, PayScaleName, AGPName, AccNo From vw_Employees Where EmpID=" & lstEmpID.Items(i).Text
            
            
            Dim EmpReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            While EmpReader.Read
                EmpName = EmpReader(0)
                DeptName = EmpReader(1)
                DesgName = EmpReader(2)
                GradeName = EmpReader(3)
                AGPName = EmpReader(4)
                AccNo = EmpReader(5)
            End While
            EmpReader.Close()

            Dim EHAmount As Double = 0, DHAmount = 0
            Dim Esum As Double = 0, DSum As Double = 0, NetAmt As Double = 0

            sqlStr = "Select HeadAmount From ProcessedSalary Where " & _
             " EmpID=" & lstEmpID.Items(i).Text & _
             " AND MonthID=" & cboMonth.SelectedIndex & _
             " AND YearID='" & cboYear.Text & "'" & _
             " AND HeadID=0"

            
            
            Dim EBasic As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            While EBasic.Read
                sqlStr = "Insert into rptSalarySlipEarningPart Values(" & _
                "'Basic'," & _
                EBasic(0) & ")"
                Esum += EBasic(0)
                lstInsert.Items.Add(sqlStr)
            End While
            EBasic.Close()

            Dim j As Integer = 0
            For j = 0 To lstEarID.Items.Count - 1
                sqlStr = "Select HeadAmount From ProcessedSalary Where " & _
                " EmpID=" & lstEmpID.Items(i).Text & _
                " AND MonthID=" & cboMonth.SelectedIndex & _
                " AND YearID='" & cboYear.Text & "'" & _
                " AND HeadID=" & lstEarID.Items.Item(j).Text

                
                
                Dim EHAReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
                While EHAReader.Read
                    EHAmount = EHAReader(0)
                    Esum += EHAmount
                End While
                EHAReader.Close()

                sqlStr = "Insert into rptSalarySlipEarningPart Values(" & _
                "'" & lstEarName.Items.Item(j).Text & "'," & _
                EHAmount & ")"

                lstInsert.Items.Add(sqlStr)
            Next

            For j = 0 To lstDedID.Items.Count - 1
                sqlStr = "Select HeadAmount From ProcessedSalary Where " & _
                " EmpID=" & lstEmpID.Items(i).Text & _
                " AND MonthID=" & cboMonth.SelectedIndex & _
                " AND YearID='" & cboYear.Text & "'" & _
                " AND HeadID=" & lstDedID.Items.Item(j).Text

                
                
                Dim DhAReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
                While DhAReader.Read
                    DHAmount = DhAReader(0)
                    DSum += DHAmount
                End While
                DhAReader.Close()

                sqlStr = "Insert into rptSalarySlipDeductionPart Values(" & _
                "'" & lstDedName.Items.Item(j).Text & "'," & _
                DHAmount & ")"

                lstInsert.Items.Add(sqlStr)
            Next

            sqlStr = "Insert into rptSalarySlipHeadPart Values(" & _
            Esum - DSum & "," & _
            "'" & EmpName & "'," & _
            "'" & DeptName & "'," & _
            "'" & DesgName & "'," & _
            "'" & GradeName & "'," & _
            "'" & AGPName & "'," & _
            "'" & AccNo & "')"

            lstInsert.Items.Add(sqlStr)

        Next

        For i = 0 To lstInsert.Items.Count - 1
            sqlStr = lstInsert.Items.Item(i).Text
            
            ExecuteQuery_Update(SqlStr)
            System.Threading.Thread.Sleep(300)
        Next

        
        

        Dim mySchoolName As String = FindSchoolDetails(1)
        Dim MyHeaderTitle As String = "Salary Slip for " & cboMonth.Text & "-" & cboYear.Text
        Dim params(1) As Microsoft.Reporting.WebForms.ReportParameter
        params(0) = New Microsoft.Reporting.WebForms.ReportParameter("SchoolName", mySchoolName, True)
        params(1) = New Microsoft.Reporting.WebForms.ReportParameter("Header", MyHeaderTitle, True)

        Me.ReportViewer1.LocalReport.SetParameters(params)
        ReportViewer1.Visible = True
        ReportViewer1.LocalReport.Refresh()

    End Sub

End Class