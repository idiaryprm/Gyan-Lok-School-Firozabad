Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary

Public Class EmployeeMaster
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try
            If Request.Cookies("UType").Value.ToString.Contains("Admin") Or Request.Cookies("UType").Value.ToString.Contains("Payroll") Then
                'Allow
            Else
                Response.Redirect("/./AccessDenied.aspx", False)
            End If
        Catch ex As Exception
            response.redirect("~/Login.aspx")
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("ActiveTab") = 4
        If IsPostBack = False Then InitControls()
        If Request.Cookies("UType").Value.ToString.Contains("Admin-1") = False And Request.Cookies("UType").Value.ToString.Contains("Payroll-1") = False Then
            btnSave.Enabled = False
        End If
    End Sub

    Private Sub InitControls()
        LoadMasterInfo(71, cboSchoolName, Request.Cookies("SchoolIDs").Value)
        LoadMasterInfo(66, cboMaritalStatus)
        cboMaritalStatus.Text = FindDefault(66)

        LoadMasterInfo(35, cboPerState)
        cboPerState.Text = FindDefault(35)

        Dim StateID As Integer = FindMasterID(35, cboPerState.Text)
        LoadStateCity(StateID, cbocity)
        cbocity.Text = FindDefault(45)

        LoadMasterInfo(35, cboCommState)
        cboCommState.Text = FindDefault(35)

        LoadMasterInfo(25, cboDepartment)
        cboDepartment.Text = FindDefault(25)

        LoadMasterInfo(26, cboDesignation)
        cboDesignation.Text = FindDefault(26)

        LoadMasterInfo(27, cboQualification)
        cboQualification.Text = FindDefault(27)

        LoadMasterInfo(5, cboReligion)
        cboReligion.Text = FindDefault(5)

        LoadMasterInfo(6, cboCaste)
        cboCaste.Text = FindDefault(6)

        LoadMasterInfo(28, cboNationality)
        cboNationality.Text = FindDefault(28)

        LoadMasterInfo(20, cboGrade)
        cboGrade.Text = FindDefault(20)

        LoadMasterInfo(29, cboStatus)
        cboStatus.Text = FindDefault(29)

        LoadMasterInfo(21, cboGradePay)
        cboGradePay.Text = FindDefault(21)

        LoadMasterInfo(22, cboBank)
        cboBank.Text = FindDefault(22)

        LoadMasterInfo(30, cboEmpCat)
        cboEmpCat.Text = FindDefault(30)

        LoadMasterInfo(64, cboEmpType)
        cboEmpType.Text = FindDefault(64)

        txtPerCity.Text = ""
        txtCommCity.Text = ""
        txtEmpID.Text = ""
        txtName.Text = ""
        txtEmpCode.Text = ""
        txtBiometricCode.Text = ""
        txtFName.Text = ""
        txtMName.Text = ""
        txtDOB.Text = Now.Date.ToString("dd/MM/yyyy")
        txtDOJ.Text = Now.Date.ToString("dd/MM/yyyy")
        txtDOI.Text = Now.Date.ToString("dd/MM/yyyy")
        txtBasicPay.Text = ""
        'txtBasicPay5.Text = ""
        txtPerAdd.Text = ""
        txtCommAdd.Text = ""
        txtPhone.Text = ""
        txtMob.Text = ""
        txtEmail.Text = ""
        txtwebPage.Text = ""
        txtAccNo.Text = ""
        txtPAN.Text = ""
        txtRemarks.Text = ""
        txtPerPinCode.Text = ""
        txtCommPinCode.Text = ""
        GridView1.Visible = False
        Image1.ImageUrl = "~/images/EmpDummy.jpg"
        txtName.Focus()
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If cboSchoolName.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('School Name is required...');", True)
            cboSchoolName.Focus()
            Exit Sub
        End If
        If txtName.Text.Length <= 0 Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Provide Valid Name of Employee...');", True)
            txtName.Focus()
            Exit Sub
        End If
        If txtEmpCode.Text.Length <= 0 Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Employee Code...');", True)
            txtEmpCode.Focus()
            Exit Sub
        End If
        If CheckEmpCode(txtEmpCode.Text, 1, Val(txtEmpID.Text)) > 1 Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Employee Code allready Exist...');", True)
            txtEmpCode.Focus()
            Exit Sub
        End If
        If Trim(txtBiometricCode.Text) <> "" Then
            If CheckEmpCode(txtBiometricCode.Text, 2, Val(txtEmpID.Text)) > 1 Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Biometric Code allready Exist...');", True)
                txtBiometricCode.Focus()
                Exit Sub
            End If
        End If

        Dim DOB As Date
        If txtDOB.Text <> "" Then
            Try
                If txtDOB.Text.Contains("/") Then
                    DOB = CDate(txtDOB.Text.Split("/")(2) & "/" & txtDOB.Text.Split("/")(1) & "/" & txtDOB.Text.Split("/")(0))
                ElseIf txtDOB.Text.Contains(".") Then
                    DOB = CDate(txtDOB.Text.Split(".")(2) & "/" & txtDOB.Text.Split(".")(1) & "/" & txtDOB.Text.Split(".")(0))
                Else
                    DOB = CDate(txtDOB.Text.Split("-")(2) & "/" & txtDOB.Text.Split("-")(1) & "/" & txtDOB.Text.Split("-")(0))
                End If

            Catch ex As Exception
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Date Of Birth...');", True)
                txtDOB.Focus()
                Exit Sub
            End Try
        End If
        'If Trim(txtPerAdd.Text) = "" Then
        '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Enter Address...');", True)
        '    txtPerAdd.Focus()
        '    Exit Sub
        'End If
        'If Trim(txtPerCity.Text) = "" Then
        '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Enter City...');", True)
        '    txtPerCity.Focus()
        '    Exit Sub
        'End If
        If Trim(txtMob.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Enter Mobile...');", True)
            txtMob.Focus()
            Exit Sub
        End If

        If txtEmail.Text.Contains("@") And txtEmail.Text.Contains(".") Or txtEmail.Text = "" Then
        Else
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Enter Valid Email ID...');", True)
            txtEmail.Focus()
            Exit Sub
        End If
        'If cboDepartment.Text.Length <= 0 Then
        '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Department');", True)
        '    cboDepartment.Focus()
        '    Exit Sub
        'End If
        'If cboDesignation.Text.Length <= 0 Then
        '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Designation');", True)
        '    cboDesignation.Focus()
        '    Exit Sub
        'End If
        'If cboQualification.Text.Length <= 0 Then
        '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Qualification');", True)
        '    cboQualification.Focus()
        '    Exit Sub
        'End If
        'If cboReligion.Text.Length <= 0 Then
        '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Religion');", True)
        '    cboReligion.Focus()
        '    Exit Sub
        'End If
        'If cboCaste.Text.Length <= 0 Then
        '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Caste');", True)
        '    cboCaste.Focus()
        '    Exit Sub
        'End If
        'If cboNationality.Text.Length <= 0 Then
        '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Nationality');", True)
        '    cboNationality.Focus()
        '    Exit Sub
        'End If
        'If cboGrade.Text.Length <= 0 Then
        '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Pay Scale');", True)
        '    cboGrade.Focus()
        '    Exit Sub
        'End If
        'If cboGradePay.Text.Length <= 0 Then
        '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Grade Pay');", True)
        '    cboGradePay.Focus()
        '    Exit Sub
        'End If
        'If cboStatus.Text.Length <= 0 Then
        '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Status');", True)
        '    cboStatus.Focus()
        '    Exit Sub
        'End If
        'If cboBank.Text.Length <= 0 Then
        '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Bank');", True)
        '    cboBank.Focus()
        '    Exit Sub
        'End If
        'If cboEmpCat.Text.Length <= 0 Then
        '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Employee Category');", True)
        '    cboEmpCat.Focus()
        '    Exit Sub
        'End If


        'If txtBasicPay.Text.Length <= 0 Or IsNumeric(txtBasicPay.Text) = False Then
        '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Provide Valid Amount for Basic Pay...');", True)
        '    txtBasicPay.Focus()
        '    Exit Sub
        'End If
        'If cboEmpType.Text.Length <= 0 Then
        '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Employee Type');", True)
        '    cboEmpType.Focus()
        '    Exit Sub
        'End If
        Dim DOI As Date
        If txtDOI.Text <> "" Then
            Try
                If txtDOI.Text.Contains("/") Then
                    DOI = CDate(txtDOI.Text.Split("/")(2) & "/" & txtDOI.Text.Split("/")(1) & "/" & txtDOI.Text.Split("/")(0))
                ElseIf txtDOI.Text.Contains(".") Then
                    DOI = CDate(txtDOI.Text.Split(".")(2) & "/" & txtDOI.Text.Split(".")(1) & "/" & txtDOI.Text.Split(".")(0))
                Else
                    DOI = CDate(txtDOI.Text.Split("-")(2) & "/" & txtDOI.Text.Split("-")(1) & "/" & txtDOI.Text.Split("-")(0))
                End If

            Catch ex As Exception
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Date Of Increament...');", True)
                txtDOI.Focus()
                Exit Sub
            End Try
        End If

        Dim DOJ As Date
        If txtDOJ.Text <> "" Then
            Try
                If txtDOJ.Text.Contains("/") Then
                    DOJ = CDate(txtDOJ.Text.Split("/")(2) & "/" & txtDOJ.Text.Split("/")(1) & "/" & txtDOJ.Text.Split("/")(0))
                ElseIf txtDOJ.Text.Contains(".") Then
                    DOJ = CDate(txtDOJ.Text.Split(".")(2) & "/" & txtDOJ.Text.Split(".")(1) & "/" & txtDOJ.Text.Split(".")(0))
                Else
                    DOJ = CDate(txtDOJ.Text.Split("-")(2) & "/" & txtDOJ.Text.Split("-")(1) & "/" & txtDOJ.Text.Split("-")(0))
                End If

            Catch ex As Exception
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Date Of Joinning...');", True)
                txtDOJ.Focus()
                Exit Sub
            End Try
        End If

        Dim sqlStr As String = ""

        Dim DeptID As Integer = FindMasterID(25, cboDepartment.Text)
        Dim DesgID As Integer = FindMasterID(26, cboDesignation.Text)
        Dim QualID As Integer = FindMasterID(27, cboQualification.Text)
        Dim RelID As Integer = FindMasterID(5, cboReligion.Text)
        Dim CasteID As Integer = FindMasterID(6, cboCaste.Text)
        Dim NatID As Integer = FindMasterID(28, cboNationality.Text)
        Dim GradeID As Integer = FindMasterID(20, cboGrade.Text)
        Dim AGPID As Integer = FindMasterID(21, cboGradePay.Text)
        Dim StatusID As Integer = FindMasterID(29, cboStatus.Text)
        Dim BankID As Integer = FindMasterID(22, cboBank.Text)
        Dim EmpCatID As Integer = FindMasterID(30, cboEmpCat.Text)
        Dim EmpTypeID As Integer = FindMasterID(64, cboEmpType.Text)
        Dim PerStateID As Integer = FindMasterID(35, cboPerState.Text)
        Dim CommStateID As Integer = FindMasterID(35, cboCommState.Text)
        Dim MaritalStatusID As Integer = FindMasterID(66, cboMaritalStatus.Text)
        Dim SchoolID As Integer = FindMasterID(71, cboSchoolName.Text)
        Dim FinalMessage As String = ""
        Dim PhotoPath As String = txtEmpCode.Text & ".jpg"







        If txtEmpID.Text = "" Then


            sqlStr = "Insert into EmployeeMaster(SchoolID,EmpCode, BiometricCode, EmpName, FName, MName, MaritalStatusID, DeptID, DesgID, QualID, Gender,"
            If txtDOB.Text <> "" Then
                sqlStr += " DOB,"
            End If
            If txtDOJ.Text <> "" Then
                sqlStr += "DOJ,"
            End If
            If txtDOI.Text <> "" Then
                sqlStr += "DOI,"
            End If
            sqlStr += "RelID, CasteID, NatID, GradeID, AGPID, BasicPay, PerAdd, PerCity, PerState, PerPincode, CommAdd, CommCity, CommState, CommPincode, Phone, Mob, Email, Web, BankID, AccNo, PAN,AadharNo,PFAccNo, Status, Remark, PhotoPath, EmpCatID,"
            sqlStr += "EmpTypeID, LibraryMemID,FileNo, IsDeleted) Values(" & _
   "'" & SchoolID & "'," & _
   "'" & SQLFixup(txtEmpCode.Text) & "'," & _
                  "'" & SQLFixup(txtBiometricCode.Text) & "'," & _
            "'" & SQLFixup(txtName.Text) & "'," & _
            "'" & SQLFixup(txtFName.Text) & "'," & _
            "'" & SQLFixup(txtMName.Text) & "'," & _
            "'" & MaritalStatusID & "'," & _
            DeptID & "," & _
            DesgID & "," & _
            QualID & "," & _
            cboGender.SelectedIndex & ","
            If txtDOB.Text <> "" Then
                sqlStr += "'" & DOB.ToString("yyyy/MM/dd") & "',"
            End If
            If txtDOJ.Text <> "" Then
                sqlStr += "'" & DOJ.ToString("yyyy/MM/dd") & "',"
            End If

            If txtDOI.Text <> "" Then
                sqlStr += "'" & DOI.ToString("yyyy/MM/dd") & "',"
            End If
            sqlStr += RelID & "," & _
            CasteID & "," & _
            NatID & "," & _
            GradeID & "," & _
            AGPID & "," & _
                  Val(txtBasicPay.Text) & "," & _
            "'" & SQLFixup(txtPerAdd.Text) & "'," & _
            "'" & SQLFixup(cbocity.Text) & "'," & _
              "'" & PerStateID & "'," & _
                "'" & SQLFixup(txtPerPinCode.Text) & "'," & _
            "'" & SQLFixup(txtCommAdd.Text) & "'," & _
              "'" & SQLFixup(txtCommCity.Text) & "'," & _
                "'" & CommStateID & "'," & _
                  "'" & SQLFixup(txtCommPinCode.Text) & "'," & _
            "'" & SQLFixup(txtPhone.Text) & "'," & _
            "'" & SQLFixup(txtMob.Text) & "'," & _
            "'" & SQLFixup(txtEmail.Text) & "'," & _
            "'" & SQLFixup(txtwebPage.Text) & "'," & _
            BankID & "," & _
            "'" & SQLFixup(txtAccNo.Text) & "'," & _
            "'" & SQLFixup(txtPAN.Text) & "'," & _
              "'" & SQLFixup(txtAadharNo.Text) & "'," & _
            "'" & SQLFixup(txtPFNo.Text) & "'," & _
            StatusID & "," & _
            "'" & SQLFixup(txtRemarks.Text) & "'," & _
            "'" & SQLFixup(PhotoPath) & "'," & _
            EmpCatID & "," & EmpTypeID & ",'0'," & _
            "'" & SQLFixup(txtFileNo.Text) & "','0')"
        Else
            sqlStr = "Update EmployeeMaster Set " & _
 "SchoolID='" & SchoolID & "'," & _
 "EmpCode='" & SQLFixup(txtEmpCode.Text) & "'," & _
            "BiometricCode='" & SQLFixup(txtBiometricCode.Text) & "'," & _
            "EmpName='" & SQLFixup(txtName.Text) & "'," & _
            "FName='" & SQLFixup(txtFName.Text) & "'," & _
            "MName='" & SQLFixup(txtMName.Text) & "'," & _
            "MaritalStatusID='" & MaritalStatusID & "'," & _
            "DeptID =" & DeptID & "," & _
            "DesgID=" & DesgID & "," & _
            "QualID=" & QualID & "," & _
            "Gender=" & cboGender.SelectedIndex & ","
            If txtDOB.Text <> "" Then
                sqlStr += "DOB='" & DOB.ToString("yyyy/MM/dd") & "',"
            End If
            If txtDOJ.Text <> "" Then
                sqlStr += "DOJ='" & DOJ.ToString("yyyy/MM/dd") & "',"
            End If

            If txtDOI.Text <> "" Then
                sqlStr += "DOI='" & DOI.ToString("yyyy/MM/dd") & "',"
            End If
            sqlStr += "RelID=" & RelID & "," & _
            "CasteID=" & CasteID & "," & _
            "NatID=" & NatID & "," & _
            "GradeID=" & GradeID & "," & _
            "AGPID=" & AGPID & "," & _
            "BasicPay=" & Val(txtBasicPay.Text) & "," & _
            "PerAdd='" & SQLFixup(txtPerAdd.Text) & "'," & _
            "PerCity='" & SQLFixup(cbocity.Text) & "'," & _
            "PerState='" & PerStateID & "'," & _
            "PerPincode='" & SQLFixup(txtPerPinCode.Text) & "'," & _
            "CommAdd='" & SQLFixup(txtCommAdd.Text) & "'," & _
            "CommCity='" & SQLFixup(txtCommCity.Text) & "'," & _
            "CommState='" & CommStateID & "'," & _
            "CommPincode='" & SQLFixup(txtCommPinCode.Text) & "'," & _
            "Phone='" & SQLFixup(txtPhone.Text) & "'," & _
            "Mob='" & SQLFixup(txtMob.Text) & "'," & _
            "Email='" & SQLFixup(txtEmail.Text) & "'," & _
            "Web='" & SQLFixup(txtwebPage.Text) & "'," & _
            "BankID=" & BankID & "," & _
            "AccNo='" & SQLFixup(txtAccNo.Text) & "'," & _
            "PAN='" & SQLFixup(txtPAN.Text) & "'," & _
             "AadharNo='" & SQLFixup(txtAadharNo.Text) & "'," & _
            "PFAccNo='" & SQLFixup(txtPFNo.Text) & "'," & _
            "Status=" & StatusID & "," & _
            "Remark='" & SQLFixup(txtRemarks.Text) & "'," & _
            "PhotoPath='" & SQLFixup(PhotoPath) & "', " & _
            "EmpCatID=" & EmpCatID & ", " & _
            "EmpTypeID=" & EmpTypeID & ", " & _
            "FileNo='" & SQLFixup(txtFileNo.Text) & "'" & _
            " Where EmpID=" & Val(txtEmpID.Text)
        End If
        ExecuteQuery_Update(sqlStr)
        'If txtEmpID.Text <> "" And txtEmpCode.Text <> "" Then
        '    sqlStr = "update users set LoginID='" & txtEmpCode.Text & "' where EmpID=" & txtEmpID.Text
        '    ExecuteQuery_Update(sqlStr)
        'End If
        FinalMessage = "Details of " & txtName.Text & " successfully Saved..."
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('" & FinalMessage & "');", True)
        InitControls()

    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        InitControls()
    End Sub

    Private Sub ShowInfo(ByVal myEmpCode As String)
        If myEmpCode = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Select Employee');", True)
            txtName.Focus()
            Exit Sub
        End If
        txtEmpID.Text = ""
        Dim sqlStr As String = ""
      
        sqlStr = "Select * From vw_Employees Where EmpCode='" & myEmpCode & "' and SchoolID in (" & Request.Cookies("SchoolIDs").Value & ")"
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)

        While myReader.Read
            txtEmpID.Text = myReader("EmpID")
            Try
                cboSchoolName.Text = myReader("SchoolName")
            Catch ex As Exception

            End Try
            Try
                txtEmpCode.Text = myReader("EmpCode")
            Catch ex As Exception

            End Try
            Try
                txtName.Text = myReader("EmpName")
            Catch ex As Exception

            End Try

            Try
                txtBiometricCode.Text = myReader("BiometricCode")
            Catch ex As Exception

            End Try
            Dim tmpdate As New Date
            Try
                tmpdate = CDate(myReader("DOB"))
                txtDOB.Text = tmpdate.ToString("dd/MM/yyyy")
            Catch ex As Exception

            End Try
            Try
                txtMName.Text = myReader("MName")
            Catch ex As Exception

            End Try
            Try
                txtFName.Text = myReader("FName")
            Catch ex As Exception

            End Try
            Try
                cboGender.SelectedIndex = myReader("Gender")
            Catch ex As Exception

            End Try
            Try
                cboMaritalStatus.Text = myReader("MaritalStatusName")
            Catch ex As Exception

            End Try
            Try
                cboReligion.Text = myReader("RelName")
            Catch ex As Exception

            End Try
            Try
                cboCaste.Text = myReader("CasteName")
            Catch ex As Exception

            End Try
            Try
                cboDepartment.Text = myReader("DeptName")
            Catch ex As Exception

            End Try
            Try
                cboQualification.Text = myReader("QualName")
            Catch ex As Exception

            End Try
            Try
                cboDesignation.Text = myReader("DesgName")
            Catch ex As Exception

            End Try
            Try
                cboStatus.Text = myReader("StatusName")
            Catch ex As Exception

            End Try
            Try
                cboEmpCat.Text = myReader("EmpCatName")
            Catch ex As Exception

            End Try

            Try
                cboEmpType.Text = myReader("EmpTypeName")
            Catch ex As Exception
                cboEmpType.Text = ""
            End Try
            Try
                txtPerAdd.Text = myReader("PerAdd")
            Catch ex As Exception

            End Try
            Try
                cbocity.Text = myReader("PerCity")
            Catch ex As Exception
                cbocity.SelectedIndex = -1
                Dim StateID As Integer = FindMasterID(35, cboPerState.Text)
                LoadStateCity(StateID, cbocity)
            End Try
            Try
                cboPerState.Text = myReader("StateName")
            Catch ex As Exception

            End Try
            Try
                txtPerPinCode.Text = myReader("PerPincode")
            Catch ex As Exception

            End Try
            Try
                txtCommAdd.Text = myReader("CommAdd")
            Catch ex As Exception

            End Try
            Try
                txtCommCity.Text = myReader("CommCity")
            Catch ex As Exception

            End Try
            Try
                cboCommState.Text = myReader("CommStateName")
            Catch ex As Exception

            End Try
            Try
                txtCommPinCode.Text = myReader("CommPincode")
            Catch ex As Exception

            End Try
            Try
                txtPhone.Text = myReader("Phone")
            Catch ex As Exception

            End Try
            Try
                txtMob.Text = myReader("Mob")
            Catch ex As Exception

            End Try
            Try
                txtEmail.Text = myReader("Email")
            Catch ex As Exception

            End Try
            Try
                txtwebPage.Text = myReader("Web")
            Catch ex As Exception

            End Try
            Try
                cboNationality.Text = myReader("NatName")
            Catch ex As Exception

            End Try
            Try
                cboBank.Text = myReader("BankName")
            Catch ex As Exception

            End Try
            Try
                txtAccNo.Text = myReader("AccNo")
            Catch ex As Exception

            End Try
            Try
                txtPAN.Text = myReader("PAN")
            Catch ex As Exception

            End Try
            Try
                txtAccNo.Text = myReader("AadharNo")
            Catch ex As Exception

            End Try
            Try
                txtPAN.Text = myReader("PFAccNo")
            Catch ex As Exception

            End Try
            Try
                txtRemarks.Text = myReader("Remark")
            Catch ex As Exception

            End Try
            Try
                Image1.ImageUrl = "~\EmpPhotos\" & myReader("PhotoPath")
            Catch ex As Exception

            End Try
            Try
                tmpdate = CDate(myReader("DOJ"))
                txtDOJ.Text = tmpdate.ToString("dd/MM/yyyy")
            Catch ex As Exception

            End Try
            Try
                tmpdate = CDate(myReader("DOI"))
                txtDOI.Text = tmpdate.ToString("dd/MM/yyyy")
            Catch ex As Exception

            End Try
            Try
                cboGrade.Text = myReader("PayScaleName")
            Catch ex As Exception

            End Try
            Try
                cboGradePay.Text = myReader("AGPName")
            Catch ex As Exception

            End Try
            Try
                txtBasicPay.Text = myReader("BasicPay")
            Catch ex As Exception

            End Try

            Try
                txtFileNo.Text = myReader("FileNo")
            Catch ex As Exception

            End Try

        End While
        myReader.Close()
        If txtEmpID.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Employee Code does not exist!!!');", True)
            txtEmpCode.Focus()
            Exit Sub
        End If
    End Sub

    Private Function CheckEmpCode(ByVal myEmpCode As String, type As Integer, EmpID As Integer)
        Dim sqlStr As String = ""
        Dim rv As Integer = 0
        If type = 1 Then
            sqlStr = "Select Count(*) From vw_Employees Where EmpCode='" & myEmpCode & "'"
        Else
            sqlStr = "Select Count(*) From vw_Employees Where BiometricCode='" & myEmpCode & "'"
        End If
        If EmpID > 0 Then
            sqlStr += " and EmpID<>" & EmpID
        End If
        Try
            rv = ExecuteQuery_ExecuteScalar(sqlStr)
        Catch ex As Exception

        End Try
        Return rv
    End Function
    Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        If txtEmpCode.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Enter Employee Code...');", True)
        Else
            Dim fp1 As String = myFile.PostedFile.FileName
            If fp1.ToString() <> "" Then
                Dim fn1 As String = fp1.Substring(fp1.LastIndexOf("\\") + 1)
                Dim sp1 As String = ""
                sp1 = Server.MapPath("EmpPhotos")
                If sp1.EndsWith("\\") = False Then
                    sp1 += "\"
                End If

                sp1 += txtEmpCode.Text & ".jpg" ' & "_" & fn1
                myFile.PostedFile.SaveAs(sp1)
                Image1.ImageUrl = "~\EmpPhotos\" & txtEmpCode.Text & ".jpg"
            End If
        End If
       
    End Sub

    Protected Sub btnNameSearch_Click(sender As Object, e As EventArgs) Handles btnNameSearch.Click
        SqlDataSource1.SelectCommand = "SELECT EmpCode, EmpName, DeptName, DesgName,SchoolName FROM vw_Employees WHERE EmpName Like '%" & txtName.Text & "%'"
        GridView1.DataBind()
        GridView1.Visible = True
        If GridView1.Rows.Count = 0 Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('No Employee Found!!!');", True)
        End If
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged
        ShowInfo(GridView1.SelectedRow.Cells(1).Text)
        GridView1.Visible = False
    End Sub

    Protected Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        ShowInfo(txtEmpCode.Text)
    End Sub

    Protected Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If txtEmpID.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Select Employee to be Deleted!!!');", True)
            txtName.Focus()
            Exit Sub
        End If
        Dim sqlStr As String = ""
        sqlStr = "update EmployeeMaster set IsDeleted=1 Where EmpID=" & txtEmpID.Text
        ExecuteQuery_Update(sqlStr)
        Dim FinalMessage As String = "Details of " & txtName.Text & " successfully Deleted..."
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('" & FinalMessage & "');", True)
        InitControls()
    End Sub

    Protected Sub cboPerState_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboPerState.SelectedIndexChanged
        Dim StateID As Integer = FindMasterID(35, cboPerState.Text)
        LoadStateCity(StateID, cbocity)
    End Sub
End Class