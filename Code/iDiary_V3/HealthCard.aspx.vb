Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary

Public Class HealthCard
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            InitControls()
            InitVaccine()
            InitHealthHistory()
        End If

        If txtSID.Text = "" Then
            btnSave.Enabled = False
            btnSaveHistory.Enabled = False
        Else
            btnSave.Enabled = True
            btnSaveHistory.Enabled = True
        End If

        If Trim(cboPhysicalActivity.Text) = "Yes" Then
            txtPhysicalActivity.Enabled = True
        Else
            txtPhysicalActivity.Enabled = False
            txtPhysicalActivity.Text = ""
        End If

    End Sub

    Private Sub InitVaccine()
        LoadMasterInfo(42, cbovacCode)
        txtDueDate.Text = Now.Date.ToString("dd/MM/yyyy")
        txtPerformedDate.Text = Now.Date.ToString("dd/MM/yyyy")
        txtremarks.Text = ""
    End Sub

    Private Sub InitHealthHistory()
        LoadMasterInfo(43, cboSeverity)
        LoadMasterInfo(44, cboAllergy)
        txtMedication.Text = ""
        txtEffect.Text = ""
        txtHappendON.Text = Now.Date.ToString("dd/MM/yyyy")
    End Sub

    Private Sub InitControls()
        txtMobile.Text = ""
        txtDOB.Text = ""
        txtFatherAddress.Text = ""
        txtFName.Text = ""
        txtMName.Text = ""
        txtName.Text = ""
        txtPhoneOffice.Text = ""
        txtPhoneResd.Text = ""
        txtSRNo.Text = ""
    End Sub

    Protected Sub btnNameSearch_Click(sender As Object, e As ImageClickEventArgs) Handles btnNameSearch.Click
        GridView2.Visible = True
        SqlDataSource2.SelectCommand = "SELECT RegNo,SID, SName, ClassName, SecName FROM vw_Student WHERE ASID = " & Request.Cookies("ASID").Value & " AND SName Like '%" & txtName.Text & "%'"
        GridView2.DataBind()
    End Sub

    Protected Sub btnNext_Click(sender As Object, e As ImageClickEventArgs) Handles btnNext.Click
        ShowStudentData(txtSRNo.Text)
    End Sub

    Private Sub ShowStudentData(ByVal srno As String)
        InitControls()

       
       
       

        Dim sqlstr As String = ""
        

        sqlstr = "select * from vw_Student Where RegNo='" & srno & "'"
        
        

        Dim myreader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myreader.Read
            txtSRNo.Text = myreader("RegNo")
            txtSID.Text = myreader("SID")
            txtName.Text = myreader("Sname")
            txtFName.Text = myreader("FName")
            txtMName.Text = myreader("Mname")
            txtDOB.Text = myreader("DOB")
            txtMobile.Text = myreader("MobNo")
            txtPhoneOffice.Text = myreader("PhoneOffice")
            txtPhoneResd.Text = myreader("PhoneResd")
            txtFatherAddress.Text = myreader("tempAddress")
            txtBloodGroup.Text = myreader("BGname")
        End While
        myreader.Close()

        gvHealthHistory.Visible = True
        gvVaccination.Visible = True
        gvHealthHistory.DataBind()
        gvHealthHistory.Columns(0).Visible = False

        gvVaccination.DataBind()
        gvVaccination.Columns(0).Visible = False
        btnSave.Enabled = True
        btnSaveHistory.Enabled = True

        sqlstr = "select * from StudentHealthRecord where SID='" & txtSID.Text & "' and ASID='" & Request.Cookies("ASID").Value & "'"
        
        

        myreader = ExecuteQuery_ExecuteReader(sqlStr)
        While myreader.Read
            cboPhysicalActivity.SelectedIndex = Val(myreader("isPhysicalProblem")) + 1
            txtPhysicalActivity.Text = myreader("ProblemDetails")
        End While
        myreader.Close()
        

    End Sub

    Protected Sub GridView2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView2.SelectedIndexChanged
        ShowStudentData(GridView2.SelectedRow.Cells(1).Text)
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If Trim(cbovacCode.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Vaccination Code is required...');", True)
            cbovacCode.Focus()
            Exit Sub
        End If
        If Trim(txtremarks.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Vaccination Remark is required...');", True)
            txtremarks.Focus()
            Exit Sub
        End If

       
       
       

        Dim sqlstr As String = ""
        
        Dim vacID As Integer = FindMasterID(42, cbovacCode.Text)
        If txtsvID.Text = "" Then
            sqlstr = "Insert into VaccinationDetails Values('" & txtSID.Text & "','" & vacID & "','" & txtDueDate.Text.Substring(6, 4) & "/" & txtDueDate.Text.Substring(3, 2) & "/" & txtDueDate.Text.Substring(0, 2) & "','" & txtPerformedDate.Text.Substring(6, 4) & "/" & txtPerformedDate.Text.Substring(3, 2) & "/" & txtPerformedDate.Text.Substring(0, 2) & "','" & txtremarks.Text & "')"
        Else
            sqlstr = "Update VaccinationDetails Set vacID='" & vacID & "', Due_Date='" & txtDueDate.Text.Substring(6, 4) & "/" & txtDueDate.Text.Substring(3, 2) & "/" & txtDueDate.Text.Substring(0, 2) & "', Performed_date='" & txtPerformedDate.Text.Substring(6, 4) & "/" & txtPerformedDate.Text.Substring(3, 2) & "/" & txtPerformedDate.Text.Substring(0, 2) & "', Remarks='" & txtremarks.Text & "' Where svID='" & txtsvID.Text & "'"
        End If

        
        
        ExecuteQuery_Update(SqlStr)

        gvVaccination.DataBind()
        gvVaccination.SelectedIndex = -1
        InitVaccine()

    End Sub

    Protected Sub btnSaveHistory_Click(sender As Object, e As EventArgs) Handles btnSaveHistory.Click
        If Trim(cboAllergy.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Allergy Name is required...');", True)
            cboAllergy.Focus()
            Exit Sub
        End If
        If Trim(cboSeverity.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Allergy Severity is required...');", True)
            cboSeverity.Focus()
            Exit Sub
        End If
        If Trim(txtMedication.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Medication taken is required...');", True)
            txtMedication.Focus()
            Exit Sub
        End If
        If Trim(txtEffect.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Effect is required...');", True)
            txtEffect.Focus()
            Exit Sub
        End If

       
       
       

        Dim sqlstr As String = ""
        
        Dim alrgID As Integer = FindMasterID(44, cboAllergy.Text)
        Dim severityid As Integer = FindMasterID(43, cboSeverity.Text)
        If txthhID.Text = "" Then
            sqlstr = "Insert into StudentHealthHistory Values('" & txtSID.Text & "','" & alrgID & "','" & txtEffect.Text & "','" & txtHappendON.Text.Substring(6, 4) & "/" & txtHappendON.Text.Substring(3, 2) & "/" & txtHappendON.Text.Substring(0, 2) & "','" & severityid & "','" & txtMedication.Text & "')"
        Else
            sqlstr = "Update StudentHealthHistory Set alrgID='" & alrgID & "', What_Happened='" & txtEffect.Text & "', Happened_ON='" & txtHappendON.Text.Substring(6, 4) & "/" & txtHappendON.Text.Substring(3, 2) & "/" & txtHappendON.Text.Substring(0, 2) & "', SeverityID='" & severityid & "', MedicationTaken='" & txtMedication.Text & "'Where hhID='" & txthhID.Text & "'"

        End If

        
        
        ExecuteQuery_Update(SqlStr)
        gvHealthHistory.DataBind()
        gvHealthHistory.SelectedIndex = -1
        InitHealthHistory()
    End Sub

    Protected Sub btnSaveHistory0_Click(sender As Object, e As EventArgs) Handles btnSaveHistory0.Click

        If Trim(cboPhysicalActivity.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Is any Physical activity problem ?');", True)
            cboPhysicalActivity.Focus()
            Exit Sub
        End If
        If Trim(txtPhysicalActivity.Text) = "" And Trim(cboPhysicalActivity.Text) = "Yes" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Physical Activity Problem Detail is required...');", True)
            txtPhysicalActivity.Focus()
            Exit Sub
        End If

       
       
       

        Dim sqlstr As String = ""
        
        ' Dim alrgID As Integer = FindMasterID(44, cboAllergy.Text)
        'Dim severityid As Integer = FindMasterID(43, cboSeverity.Text)
        sqlstr = "Insert into StudentHealthRecord Values('" & txtSID.Text & "','" & Request.Cookies("ASID").Value & "','" & (cboPhysicalActivity.SelectedIndex - 1) & "','" & txtPhysicalActivity.Text & "')"
        
        
        ExecuteQuery_Update(SqlStr)
    End Sub

    Protected Sub gvVaccination_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvVaccination.SelectedIndexChanged
        gvVaccination.Columns(0).Visible = True
        gvVaccination.DataBind()

        txtsvID.Text = gvVaccination.SelectedRow.Cells(1).Text
        cbovacCode.Text = gvVaccination.SelectedRow.Cells(2).Text
        txtPerformedDate.Text = gvVaccination.SelectedRow.Cells(4).Text
        txtDueDate.Text = gvVaccination.SelectedRow.Cells(5).Text
        txtremarks.Text = gvVaccination.SelectedRow.Cells(6).Text
        gvVaccination.Columns(0).Visible = False
    End Sub

    Protected Sub gvHealthHistory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvHealthHistory.SelectedIndexChanged
        gvHealthHistory.Columns(0).Visible = True
        gvHealthHistory.DataBind()

        txthhID.Text = gvHealthHistory.SelectedRow.Cells(1).Text
        cboAllergy.Text = gvHealthHistory.SelectedRow.Cells(2).Text
        cboSeverity.Text = gvHealthHistory.SelectedRow.Cells(3).Text
        txtMedication.Text = gvHealthHistory.SelectedRow.Cells(4).Text
        txtEffect.Text = gvHealthHistory.SelectedRow.Cells(5).Text
        txtHappendON.Text = gvHealthHistory.SelectedRow.Cells(6).Text
        gvVaccination.Columns(0).Visible = False
    End Sub

    
End Class