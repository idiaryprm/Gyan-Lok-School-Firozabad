Imports iDiary_V3.iDiary.CLS_idiary
Imports iDiary_V3.iDiary.CLS_iDiary_Exam
Imports System.Data.SqlClient

Public Class TTConfig
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("ActiveTab") = 5
        If IsPostBack = False Then
            InitControls()
        End If
        If Request.QueryString("type") = 0 Then
            PanelEmployee.Visible = False
            PanelSubject.Visible = True
        ElseIf Request.QueryString("type") = 1 Then
            PanelEmployee.Visible = True
            PanelSubject.Visible = False
        Else
            PanelEmployee.Visible = False
            PanelSubject.Visible = False
        End If
    End Sub

    Private Sub InitControls()
        LoadMasterInfo(2, cboClass)
        cboSection.Items.Clear()
        cboSubject.Items.Clear()
        txtContinPeriods.Text = ""
        txtTotalPeriodsWeek.Text = ""
        txtName.Text = ""
        txtEmpCode.Text = ""
        txtContinPeriods.Text = ""
        txtEmpConsePeriods.Text = ""
        txtEmpDayLoad.Text = ""
        txtEmpID.Text = ""
        txtEmpWeekLoad.Text = ""
        LoadMasterInfo(25, cboDepartment)
        LoadMasterInfo(26, cboDesignation)
    End Sub
    Protected Sub cboClass_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboClass.SelectedIndexChanged
        LoadClassSection("", cboClass.Text, cboSection)
        cboClass.Focus()
    End Sub

    Protected Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        txtContinPeriods.Text = ""
        txtTotalPeriodsDay.Text = ""
        txtTotalPeriodsWeek.Text = ""
        cboWeightage.SelectedIndex = -1

        If Trim(cboClass.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please select Class..');", True)
            cboClass.Focus()
            Exit Sub
        End If
        If Trim(cboSection.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please select Section..');", True)
            cboSection.Focus()
            Exit Sub
        End If

        Dim cssid As Integer = FindCSSID("", cboClass.Text, cboSection.Text, "")
        Dim SubID As Integer = cboSubject.SelectedItem.Value
        Dim SqlStr As String = "Select * from TTSubjectConfig Where CSSID=" & cssid & " AND SubjectID=" & SubID & " AND ASID=" & Request.Cookies("ASID").Value
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(SqlStr)
        While myReader.Read
            txtTotalPeriodsDay.Text = myReader("MaxPeriodDay")
            txtTotalPeriodsWeek.Text = myReader("TotalPeriodWeek")
            txtContinPeriods.Text = myReader("maxContinuousPeriods")
        End While
        SqlStr = "select Top 1 TTweightage from vw_examsubjectmapping where ClassName='" & cboClass.SelectedItem.Text & "' And SecName='" & cboSection.SelectedItem.Text & "' AND SubjectID='" & cboSubject.SelectedItem.Value & "' AND ASID=" & Request.Cookies("ASID").Value
        Try
            cboWeightage.Text = ExecuteQuery_ExecuteScalar(SqlStr)
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub btnNameSearch_Click(sender As Object, e As EventArgs) Handles btnNameSearch.Click
        SqlDataSource1.SelectCommand = "SELECT EmpCode, EmpName, DeptName, DesgName FROM vw_Employees WHERE EmpName Like '%" & txtName.Text & "%'"
        GridView1.DataBind()
    End Sub

    Private Sub ShowInfo(ByVal myEmpCode As String)
        If myEmpCode = "" Then
            Exit Sub
        End If
        Dim sqlStr As String = "Select * From vw_Employees Where EmpCode='" & myEmpCode & "'"

        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            txtEmpID.Text = myReader("EmpID")
            txtEmpCode.Text = myReader("EmpCode")
            txtName.Text = myReader("EmpName")
            cboDepartment.Text = myReader("DeptName")
            cboDesignation.Text = myReader("DesgName")
        End While
        myReader.Close()

        sqlStr = "Select * from TTEmployeeConfig Where EmpID=" & txtEmpID.Text
        myReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            txtEmpConsePeriods.Text = myReader("maxConsecutivePeriods")
            txtEmpDayLoad.Text = myReader("MaxLoadDayWise")
            txtEmpWeekLoad.Text = myReader("MaxloadWeekly")
        End While
        myReader.Close()
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged
        ShowInfo(GridView1.SelectedRow.Cells(1).Text)
    End Sub

    Protected Sub cboSection_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSection.SelectedIndexChanged
        LoadSubjectClassWise(cboSubject, cboClass.Text, cboSection.Text, Request.Cookies("ASID").Value, 0, 1)
        cboSection.Focus()
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSaveSub.Click
        Dim sqlstr As String = ""
        Dim cssid As Integer = FindCSSID("", cboClass.Text, cboSection.Text, "")
        Dim SubID As Integer = FindSubjectID(cboSubject.SelectedItem.Text)
        sqlstr = "Select Count(*) from TTSubjectConfig Where CSSID=" & cssid & " AND SubjectID=" & SubID & " AND ASID=" & Request.Cookies("ASID").Value
        If ExecuteQuery_ExecuteScalar(sqlstr) > 0 Then
            sqlstr = "UPDATE TTSubjectConfig Set MaxPeriodDay='" & txtTotalPeriodsDay.Text & "',TotalPeriodWeek='" & txtTotalPeriodsWeek.Text & "',maxContinuousPeriods='" & txtContinPeriods.Text & "'  Where CSSID=" & cssid & " AND SubjectID=" & SubID & " AND ASID=" & Request.Cookies("ASID").Value
            ExecuteQuery_Update(sqlstr)
        Else
            sqlstr = "Insert into TTSubjectConfig(SubjectID,CSSID,MaxPeriodDay,TotalPeriodWeek,maxContinuousPeriods,ASID) Values (" & _
            SubID & "," & cssid & ",'" & txtTotalPeriodsDay.Text & "','" & txtTotalPeriodsWeek.Text & "','" & txtContinPeriods.Text & "','" & Request.Cookies("ASID").Value & "')"
            ExecuteQuery_Update(sqlstr)
        End If
        sqlstr = "Update ExamSubJEctMapping Set TTWeightage=" & cboWeightage.Text & " Where CSSID=" & cssid & " AND SubjectID=" & SubID & " AND ASID=" & Request.Cookies("ASID").Value
        ExecuteQuery_Update(sqlstr)

        txtContinPeriods.Text = ""
        txtTotalPeriodsDay.Text = ""
        txtTotalPeriodsWeek.Text = ""
        cboWeightage.SelectedIndex = -1
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Configuration saved Successfully...');", True)
    End Sub

    Protected Sub btnSaveEmp_Click(sender As Object, e As EventArgs) Handles btnSaveEmp.Click
        Dim sqlstr As String = ""
        'Dim cssid As Integer = FindCSSID(cboClass.Text, cboSection.Text, "")
        'Dim SubID As Integer = FindSubjectID(cboSubject.SelectedItem.Text)
        sqlstr = "Select Count(*) from TTEmployeeConfig Where EmpID=" & txtEmpID.Text
        If ExecuteQuery_ExecuteScalar(sqlstr) > 0 Then
            sqlstr = "UPDATE TTEmployeeConfig Set MaxConsecutivePeriods='" & txtEmpConsePeriods.Text & "',MaxLoadDayWise='" & txtEmpDayLoad.Text & "',MaxloadWeekly='" & txtEmpWeekLoad.Text & "'  Where EmpID=" & txtEmpID.Text
            ExecuteQuery_Update(sqlstr)
        Else
            sqlstr = "INsert into TTEmployeeConfig(EmpID,MaxConsecutivePeriods,MaxLoadDayWise,MaxloadWeekly) Values (" & _
            txtEmpID.Text & ",'" & txtEmpConsePeriods.Text & "','" & txtEmpDayLoad.Text & "','" & txtEmpWeekLoad.Text & "')"
            ExecuteQuery_Update(sqlstr)
        End If
        InitControls()

        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Configuration saved Successfully...');", True)
    End Sub

    Protected Sub btnEmpNext_Click(sender As Object, e As EventArgs) Handles btnEmpNext.Click
        ShowInfo(txtEmpCode.Text)
    End Sub
End Class