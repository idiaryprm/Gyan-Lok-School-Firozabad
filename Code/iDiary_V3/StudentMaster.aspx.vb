Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Imports iDiary_V3.iDiary_Student.CLS_iDiary_Student
Imports iDiary_V3.iDiary_Fee.CLS_iDiary_Fee
Imports System.IO
Imports Microsoft.Reporting.WebForms
Imports iDiary_V3.iDiary_Date.CLS_iDiary_Date
'Imports System.String

Public Class StudentMaster
    Inherits System.Web.UI.Page

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
        For Each entry As System.Collections.DictionaryEntry In HttpContext.Current.Cache
            HttpContext.Current.Cache.Remove(DirectCast(entry.Key, String))
        Next
        If Request.Cookies("UType").Value.ToString.Contains("Admin") Then
            btnSave.Text = "Save & Authorize"
            btnRemove.Visible = True
        End If
        Session("ActiveTab") = 2
        If IsPostBack = False Then
            InitControls()
            If Request.QueryString("RegNo") <> "" Then
                Try
                    txtSRNo.Text = Request.QueryString("RegNo")
                    ShowStudentRecord("1", Request.QueryString("Regno"))
                Catch ex As Exception

                End Try
            End If
        End If

        Dim u As String = Request.Cookies("UID").Value
        If Request.Cookies("UType").Value.ToString.Contains("Student-1") = False And Request.Cookies("UType").Value.ToString.Contains("Admin-1") = False Then
            btnSave.Enabled = False
            btnRemove.Enabled = False
        End If

        Dim enquiryNo As String = Request.QueryString("EnquiryNo")
        If enquiryNo <> "" Then
            Dim sqlstr As String = ""




            sqlstr = "Select sname,fname,mname,dob,className,mobno,email,address,EnQuiry,statusName,TypeName from vw_StudentEnquiry where enquiryNo='" & enquiryNo & "'"


            Dim myreader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)
            While myreader.Read
                txtName.Text = myreader("sname")
                txtFathername.Text = myreader("fname")
                txtMotherName.Text = myreader("mname")
                Try
                    Dim tmpDate As Date = myreader("DoB")
                    txtDOB.Text = tmpDate.ToString("dd/MM/yyyy")
                Catch ex As Exception
                    txtDOB.Text = Today.Date.ToString("dd/MM/yyyy")
                End Try
                cboClass.Text = myreader("className")
                Try
                    lblSecStrength.Text = GetSectionStrength(cboSchoolName.Text, cboClass.Text, Request.Cookies("ASID").Value)
                Catch ex As Exception

                End Try
                txtMobile.Text = myreader("mobNo")
                txtEmail.Text = myreader("email")
                txtAddressFather.Text = myreader("address")
                '    txtEnquiry.Text = myreader("EnQuiry")
                '  cboStatus.Text = myreader("statusName")
                '  cboCategory.Text = myreader("TypeName")
            End While
            myreader.Close()


        End If
    End Sub

    Private Sub InitControls()
        lblSecStrength.Text = ""
        cboOnlyChild.Text = "No"
        txtSID.Text = ""
        txtStudentID.Text = ""
        txtLastSR.Text = LastSRNoUsed(Request.Cookies("ASID").Value)
        txtSRNo.Text = ""
        txtFeeBookNo.Text = ""
        If FeeBookSameAsSRNo() = 0 Then
            txtFeeBookNo.Enabled = True
        Else
            txtFeeBookNo.Enabled = False
        End If
        LoadMasterInfo(71, cboSchoolName, Request.Cookies("SchoolIDs").Value)

        LoadMasterInfo(2, cboClass, cboSchoolName.Text)
        cboClass.Text = ""
        cboSection.Items.Clear()
        cboSubSection.Items.Clear()

        LoadMasterInfo(5, cboRel)

        'cboRel.Text = FindDefault(5)
        LoadMasterInfo(6, cboCaste)
        cboCaste.Text = FindDefault(6)
        LoadMasterInfo(4, cboHouse)
        cboHouse.Text = FindDefault(4)
        LoadMasterInfo(41, cboMotherTongue)
        cboMotherTongue.Text = FindDefault(41)
        LoadMasterInfo(34, cboCategory)
        cboCategory.Text = FindDefault(34)
        LoadMasterInfo(35, cboState)
        cboState.Text = FindDefault(35)
        Dim StateID As Integer = FindMasterID(35, cboState.Text)
        LoadStateCity(StateID, cbocity)
        cbocity.Text = FindDefault(45)
        LoadMasterInfo(16, cboBloodGroup)
        cboBloodGroup.Text = FindDefault(16)
        LoadMasterInfo(28, cboNationality)
        Try
            cboNationality.Text = FindDefault(28)
        Catch ex As Exception

        End Try
        LoadMasterInfo(10, cboStatus)
        cboStatus.Text = FindDefault(10)
        LoadMasterInfo(7, cboFOcc)
        Try
            cboFOcc.Text = FindDefault(7)
        Catch ex As Exception

        End Try
        LoadMasterInfo(8, cboFDept)
        Try
            cboFDept.Text = FindDefault(8)

        Catch ex As Exception

        End Try
        LoadMasterInfo(9, cboFPost)
        Try
            cboFPost.Text = FindDefault(9)
        Catch ex As Exception

        End Try

        LoadMasterInfo(7, cboMOcc)
        Try
            cboMOcc.Text = FindDefault(7)
        Catch ex As Exception

        End Try
        LoadMasterInfo(8, cboMDept)
        Try
            cboMDept.Text = FindDefault(8)
        Catch ex As Exception

        End Try
        LoadMasterInfo(9, cboMPost)
        Try
            cboMPost.Text = FindDefault(9)
        Catch ex As Exception

        End Try
        LoadMasterInfo(37, cboLastboard)
        Try
            cboLastboard.Text = FindDefault(37)
        Catch ex As Exception

        End Try
        txtAdmissionDate.Text = Now.Date.ToString("dd/MM/yyyy")
        cboGender.SelectedIndex = 0
        txtFormNo.Text = ""
        txtName.Text = ""
        txtClassRollNo.Text = ""
        txtDOB.Text = ""
        txtboardRollNO.Text = ""
        txtFathername.Text = ""
        txtMotherName.Text = ""
        txtGuardianName.Text = ""
        txtGuardianRelation.Text = ""
        txtAddressFather.Text = ""
        txtAddressFather.Text = ""
        txtPhoneResd.Text = ""
        txtPhoneOffice.Text = ""
        txtPhoneOfficeM.Text = ""
        txtFathername.Text = ""
        txtMotherName.Text = ""
        txtMobile.Text = ""
        txtEmail.Text = ""
        txtEmailM.Text = ""
        txtMobileGuardian.Text = ""
        'LoadFeeConcessionTypeMaster(cboConcessionType)
        imgPhoto.ImageUrl = ""
        ClearTempSiblingTable()
        GridView2.DataBind()
        txtFIncome.Text = 0
        txtAddressGuardian.Text = ""
        txtMIncome.Text = 0
        txtAddressFather.Text = ""
        txtReceiptNo.Text = ""
        txtDistrict.Text = ""
        txtPincode.Text = "202001"
        txtLastSchool.Text = ""
        txtLastSchoolAddress.Text = ""
        txtFEmpNo.Text = ""
        txtMEmpNo.Text = ""
        lblAge.Text = ""
        lblAgeOn.Text = ""
        txtAadharNo.Text = ""
        gvSb.Visible = False
        imgPhoto.ImageUrl = "~/images/StudentDummy.jpg"
        txtSRNo.Focus()
        If Request.QueryString("Regno") = "" Then
        Else
            Try
                ShowStudentRecord("1", Request.QueryString("Regno"))
            Catch ex As Exception

            End Try
        End If
        Dim SenderID As String = ""
        Dim SchoolID = FindMasterID(71, cboSchoolName.Text)
        Dim sqlStr = "Select SMSSender From SMSSender where SchoolID='" & SchoolID & "'"


        Dim SMSReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While SMSReader.Read
            SenderID = SMSReader(0)
        End While
        txtSenderID.Text = SenderID
        SMSReader.Close()
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If cboSchoolName.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('School Name is required...');", True)
            cboSchoolName.Focus()
            Exit Sub
        End If
        If Trim(txtSRNo.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Admission/SR No. is required...');", True)
            txtSRNo.Focus()
            Exit Sub
        End If
        'If Trim(txtFeeBookNo.Text) = "" And txtFeeBookNo.Enabled = True Then
        '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Fee Book No is required...');", True)
        '    txtFeeBookNo.Focus()
        '    Exit Sub
        'End If

        If CheckSRExist(txtSRNo.Text, Request.Cookies("ASID").Value, Val(txtSID.Text)) = True Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('SR No / Admin No already exists...');", True)
            txtSRNo.Focus()
            Exit Sub
        End If
        If txtFeeBookNo.Enabled = False Then txtFeeBookNo.Text = txtSRNo.Text
        If Trim(txtFeeBookNo.Text) <> "" Then
            If CheckFeeBookNoExist(txtFeeBookNo.Text, Request.Cookies("ASID").Value, Val(txtSID.Text)) = True Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Fee Book No already exists...');", True)
                txtFeeBookNo.Focus()
                Exit Sub
            End If
        End If
        'If CheckFormNoExist(txtFormNo.Text, Request.Cookies("ASID").Value) = True Then
        '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Form No already exists...');", True)
        '    txtFormNo.Focus()
        '    Exit Sub
        'End If
        Dim AdmissionDate As Date = Now.Date
        Dim DOB As Date = Now.Date
        If Trim(txtAdmissionDate.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Admission Date is required...');", True)
            txtAdmissionDate.Focus()
            Exit Sub
        End If
        Try
            AdmissionDate = CDate(txtAdmissionDate.Text.Substring(6, 4) & "/" & txtAdmissionDate.Text.Substring(3, 2) & "/" & txtAdmissionDate.Text.Substring(0, 2))
        Catch ex As Exception
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Admission Date...');", True)
            txtAdmissionDate.Focus()
        End Try
        If Trim(txtName.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Student Name is required...');", True)
            txtName.Focus()
            Exit Sub
        End If
        If Trim(txtDOB.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Date of Birth is required...');", True)
            txtDOB.Focus()
            Exit Sub
        End If
        Try
            DOB = CDate(txtDOB.Text.Substring(6, 4) & "/" & txtDOB.Text.Substring(3, 2) & "/" & txtDOB.Text.Substring(0, 2))
        Catch ex As Exception
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid DOB...');", True)
            txtDOB.Focus()
        End Try
        If Trim(cboMotherTongue.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Mother Tongue is required...');", True)
            cboMotherTongue.Focus()
            Exit Sub
        End If

        If Trim(cboBloodGroup.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Blood Group is required...');", True)
            cboBloodGroup.Focus()
            Exit Sub
        End If

        If Trim(cboGender.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Gender is required...');", True)
            cboGender.Focus()
            Exit Sub
        End If
        If Trim(cboRel.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Religion is required...');", True)
            cboRel.Focus()
            Exit Sub
        End If
        If Trim(cboCaste.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Caste is required...');", True)
            cboCaste.Focus()
            Exit Sub
        End If


        If Trim(cboHouse.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('House is required...');", True)
            cboHouse.Focus()
            Exit Sub
        End If

        If Trim(cboCategory.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Social Category is required...');", True)
            cboCategory.Focus()
            Exit Sub
        End If

        If Trim(txtFathername.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Father Name is required...');", True)
            txtFathername.Focus()
            Exit Sub
        End If
        If Trim(txtMotherName.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Mother Name is required...');", True)
            txtMotherName.Focus()
            Exit Sub
        End If
        If Trim(txtAddressFather.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Address is required...');", True)
            txtAddressFather.Focus()
            Exit Sub
        End If
        If Trim(cboState.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('State is required...');", True)
            cboState.Focus()
            Exit Sub
        End If
        If Trim(txtMobile.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Mobile No. is required...');", True)
            txtMobile.Focus()
            Exit Sub
        End If
        If Trim(cboOnlyChild.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Only Child Field is required...');", True)
            cboOnlyChild.Focus()
            Exit Sub
        End If

        If Trim(cboStatus.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Status Name is required...');", True)
            cboStatus.Focus()
            Exit Sub
        End If
        If Trim(cboClass.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Class is required...');", True)
            cboClass.Focus()
            Exit Sub
        End If
        If Trim(cboSection.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Section is required...');", True)
            cboSection.Focus()
            Exit Sub
        End If
        Dim CSSID As Integer = FindCSSID(cboSchoolName.Text, cboClass.Text, cboSection.Text, cboSubSection.Text)
        If CSSID = 0 Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Class Section Configuration not found...');", True)
            cboSection.Focus()
            Exit Sub
        End If

        If Trim(cboFOcc.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Father Occupation is required...');", True)
            cboFOcc.Focus()
            Exit Sub
        End If
        If Trim(cboFDept.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Father Department is required...');", True)
            cboFDept.Focus()
            Exit Sub
        End If
        If Trim(cboFPost.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Father Designation is required...');", True)
            cboFPost.Focus()
            Exit Sub
        End If
        If Trim(cboMOcc.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Mother Occupation is required...');", True)
            cboMOcc.Focus()
            Exit Sub
        End If
        If Trim(cboMDept.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Mother Department is required...');", True)
            cboMDept.Focus()
            Exit Sub
        End If
        If Trim(cboMPost.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Mother Designation is required...');", True)
            cboMPost.Focus()
            Exit Sub
        End If

        Dim onlychild As Integer
        If cboOnlyChild.Text = "Yes" Then
            onlychild = 1

        Else
            onlychild = 0

        End If

        Dim RelID As Integer = FindMasterID(5, cboRel.Text)
        Dim CasteID As Integer = FindMasterID(6, cboCaste.Text)
        Dim HouseID As Integer = FindMasterID(4, cboHouse.Text)
        Dim StateID As Integer = FindMasterID(35, cboState.Text)
        Dim NationalityID As Integer = FindMasterID(28, cboNationality.Text)
        Dim BGID As Integer = FindMasterID(16, cboBloodGroup.Text)
        Dim StatusID As Integer = FindMasterID(10, cboStatus.Text)
        Dim PhotoPath As String = imgPhoto.ImageUrl.Replace("~\", "")
        ''txtSRNo.Text & "_" & Request.Cookies("ASID").Value & ".jpg"
        Dim FOccID As Integer = FindMasterID(7, cboFOcc.Text)
        Dim FDeptID As Integer = FindMasterID(8, cboFDept.Text)
        Dim FDesgID As Integer = FindMasterID(9, cboFPost.Text)
        Dim MOccID As Integer = FindMasterID(7, cboMOcc.Text)
        Dim MDeptID As Integer = FindMasterID(8, cboMDept.Text)
        Dim MDesgID As Integer = FindMasterID(9, cboMPost.Text)
        Dim CategoryID As Integer = FindMasterID(34, cboCategory.Text)
        Dim BoardID As Integer = FindMasterID(37, cboLastboard.Text)
        Dim MotherTongueID As Integer = FindMasterID(41, cboMotherTongue.Text)
        Dim SchoolID As Integer = FindMasterID(71, cboSchoolName.Text)

        Dim SID As Integer = 0
        'Dim ConcessionType As Integer = FindFeeConcessionTypeMasterID(cboConcessionType.Text)

        Dim sqlstr As String = ""

        If txtSID.Text = "" Then
            Save_Log("STUDENT INSERT")
        Else
            Save_Log("STUDENT UPDATE")
        End If

        'Insert Records into Student Table
        If txtSID.Text = "" Then    'Insert Command
            sqlstr = "Insert into StudentBasicInfo " & _
            "(ReceiptNo,FormNo,SName,BloodGroupID,MotherTougueID,AdmissionDate,Gender,RelID,CasteID,DOB,FName,MName,GuardianName,GuardianRelation,GuardianAddress,"
            sqlstr += "GuardianMobile,FatherAddress,District,StateID,PINCODE,PhoneResd,PhoneOfficeFather,PhoneOfficeMother,MobNo,EmailFather,EmailMother,CategoryID,onlychild,"
            sqlstr += "StatusID,LastSchoolName,LastSchoolAddress,LastSchoolBoardID,LastSchoolPercentage,FOccID,FDeptID,FDesgID,FIncome,FEmpNo,MOccID,MDeptID,MDesgID,MIncome,MEmpNo,"
            sqlstr += "NationalityID,AadharNo,UserID,IsDeleted,IsAuthorize) Values(" & _
            "'" & SQLFixup(txtReceiptNo.Text) & "'," & _
            "'" & SQLFixup(txtFormNo.Text) & "'," & _
            "'" & SQLFixup(txtName.Text) & "'," & _
            BGID & "," & _
            MotherTongueID & "," & _
            "'" & AdmissionDate.ToString("yyyy/MM/dd") & "'," & _
            cboGender.SelectedIndex & "," & _
            RelID & "," & _
            CasteID & "," & _
            "'" & DOB.ToString("yyyy/MM/dd") & "'," & _
            "'" & SQLFixup(txtFathername.Text) & "'," & _
            "'" & SQLFixup(txtMotherName.Text) & "'," & _
            "'" & SQLFixup(txtGuardianName.Text) & "'," & _
            "'" & SQLFixup(txtGuardianRelation.Text) & "'," & _
            "'" & SQLFixup(txtAddressGuardian.Text) & "'," & _
            "'" & SQLFixup(txtMobileGuardian.Text) & "'," & _
            "'" & SQLFixup(txtAddressFather.Text) & "'," & _
            "'" & SQLFixup(cbocity.Text) & "'," & _
            "'" & StateID & "'," & _
            "'" & SQLFixup(txtPincode.Text) & "'," & _
            "'" & SQLFixup(txtPhoneResd.Text) & "'," & _
            "'" & SQLFixup(txtPhoneOffice.Text) & "'," & _
            "'" & SQLFixup(txtPhoneOfficeM.Text) & "'," & _
            "'" & SQLFixup(txtMobile.Text) & "'," & _
            "'" & SQLFixup(txtEmail.Text) & "'," & _
            "'" & SQLFixup(txtEmailM.Text) & "'," & _
            "'" & CategoryID & "'," & _
            "'" & onlychild & "'," & _
            StatusID & "," & _
            "'" & SQLFixup(txtLastSchool.Text) & "'," & _
            "'" & SQLFixup(txtLastSchoolAddress.Text) & "'," & _
            "'" & BoardID & "'," & _
            "'" & SQLFixup(txtLastSchoolPercentage.Text) & "'," & _
            FOccID & "," & _
            FDeptID & "," & _
            FDesgID & "," & _
            Val(txtFIncome.Text) & "," & _
            "'" & SQLFixup(txtFEmpNo.Text) & "'," & _
            MOccID & "," & _
            MDeptID & "," & _
            MDesgID & "," & _
            Val(txtMIncome.Text) & "," & _
            "'" & SQLFixup(txtMEmpNo.Text) & "'," & _
            NationalityID & "," & _
            "'" & SQLFixup(txtAadharNo.Text) & "'," & _
            Request.Cookies("UserID").Value & ",0"
            If Request.Cookies("UType").Value.ToString.Contains("Admin") Then
                sqlstr += ",1)"
            Else
                sqlstr += ",0)"
            End If
            ExecuteQuery_Update(sqlstr)

            Dim TempStudentID As Integer = 0
            sqlstr = "Select Max(StudentID) from StudentBasicInfo where SName='" & SQLFixup(txtName.Text) & "' and FName='" & SQLFixup(txtFathername.Text) & "' and MobNo='" & SQLFixup(txtMobile.Text) & "'"
            TempStudentID = ExecuteQuery_ExecuteScalar(sqlstr)
            Dim ClassRollNo As Integer = 0
            If txtClassRollNo.Text = "" Then
                ClassRollNo = 0
            Else
                ClassRollNo = txtClassRollNo.Text

            End If
            sqlstr = "Insert into Student" & _
           "(SchoolID,StudentID,RegNo,ClassRollno,BoardRegNo,FeeBookNo,CSSID,HouseID,ASID,Promoted,PhotoPath,FeeConfigType,UserID) Values(" & _
 "'" & SchoolID & "'," & _
 "'" & TempStudentID & "'," & _
            "'" & SQLFixup(txtSRNo.Text) & "'," & _
            "'" & SQLFixup(ClassRollNo) & "'," & _
            "'" & SQLFixup(txtboardRollNO.Text) & "'," & _
            "'" & SQLFixup(txtFeeBookNo.Text) & "'," & _
            CSSID & "," & _
            HouseID & "," & _
            Request.Cookies("ASID").Value & "," & _
            "0," & _
            "'" & PhotoPath & "'," & _
            0 & "," & _
            Request.Cookies("UserID").Value & ")"
            ExecuteQuery_Update(sqlstr)
            SID = ExecuteQuery_ExecuteScalar("Select MAX(SID) from Student where ASID='" & Request.Cookies("ASID").Value & "' AND StudentID='" & TempStudentID & "'")

            SendSmsToStudent()
        Else    'Update Command
            sqlstr = "Update StudentBasicInfo Set " & _
            "ReceiptNo='" & SQLFixup(txtReceiptNo.Text) & "'," & _
            "FormNo='" & SQLFixup(txtFormNo.Text) & "'," & _
            "SName='" & SQLFixup(txtName.Text) & "'," & _
            "BloodGroupID= " & BGID & "," & _
            "MotherTougueID=" & MotherTongueID & "," & _
            "AdmissionDate='" & AdmissionDate.ToString("yyyy/MM/dd") & "'," & _
            "Gender=" & cboGender.SelectedIndex & "," & _
            "RelID=" & RelID & "," & _
            "CasteID=" & CasteID & "," & _
            "DOB='" & DOB.ToString("yyyy/MM/dd") & "'," & _
            "FName='" & SQLFixup(txtFathername.Text) & "'," & _
            "MName='" & SQLFixup(txtMotherName.Text) & "'," & _
            "GuardianName='" & SQLFixup(txtGuardianName.Text) & "'," & _
            "GuardianRelation='" & SQLFixup(txtGuardianRelation.Text) & "'," & _
            "GuardianAddress='" & SQLFixup(txtAddressGuardian.Text) & "'," & _
            "GuardianMobile='" & SQLFixup(txtMobileGuardian.Text) & "'," & _
            " FatherAddress='" & SQLFixup(txtAddressFather.Text) & "'," & _
            "District='" & SQLFixup(cbocity.Text) & "'," & _
            "StateID=" & StateID & "," & _
            "PINCODE='" & SQLFixup(txtPincode.Text) & "'," &
            "PhoneResd='" & SQLFixup(txtPhoneResd.Text) & "'," & _
            "PhoneOfficeFather='" & SQLFixup(txtPhoneOffice.Text) & "'," & _
            "PhoneOfficeMother='" & SQLFixup(txtPhoneOfficeM.Text) & "'," & _
            "MobNo='" & SQLFixup(txtMobile.Text) & "'," & _
            "EmailFather='" & SQLFixup(txtEmail.Text) & "'," & _
            "EmailMother='" & SQLFixup(txtEmailM.Text) & "'," & _
            "CategoryID=" & CategoryID & "," & _
            "OnlyChild=" & onlychild & "," & _
            "StatusID=" & StatusID & "," & _
            "LastSchoolName='" & SQLFixup(txtLastSchool.Text) & "'," & _
            "LastSchoolAddress='" & SQLFixup(txtLastSchoolAddress.Text) & "'," & _
            "LastSchoolBoardID='" & BoardID & "'," & _
            "LastSchoolPercentage='" & SQLFixup(txtLastSchoolPercentage.Text) & "'," & _
            "FOccID=" & FOccID & "," & _
            "FDeptID=" & FDeptID & "," & _
            "FDesgID=" & FDesgID & "," & _
            "FIncome=" & Val(txtFIncome.Text) & "," & _
            "FEmpNo='" & SQLFixup(txtFEmpNo.Text) & "'," & _
            "MOccID=" & MOccID & "," & _
            "MDeptID=" & MDeptID & "," & _
            "MDesgID=" & MDesgID & "," & _
            "MIncome=" & Val(txtMIncome.Text) & "," & _
            "MEmpNo='" & SQLFixup(txtMEmpNo.Text) & "'," & _
            "NationalityID='" & NationalityID & "'," & _
            "AadharNo='" & SQLFixup(txtAadharNo.Text) & "'"

            If Request.Cookies("UType").Value.ToString.Contains("Admin") Then
                sqlstr += ", IsAuthorize=1"
            End If
            sqlstr += " Where StudentID=" & Val(txtStudentID.Text)
            ExecuteQuery_Update(sqlstr)

            sqlstr = "Update Student Set " & _
  "SchoolID='" & SchoolID & "'," & _
  "StudentID='" & SQLFixup(txtStudentID.Text) & "'," & _
                "RegNo='" & SQLFixup(txtSRNo.Text) & "'," & _
                "BoardRegNo='" & SQLFixup(txtboardRollNO.Text) & "'," & _
                "FeeBookNo='" & SQLFixup(txtFeeBookNo.Text) & "'," & _
                "ClassRollNo='" & SQLFixup(txtClassRollNo.Text) & "'," & _
                "CSSID=" & CSSID & "," & _
                "HouseID=" & HouseID & "," & _
                "Photopath='" & PhotoPath & "'" & _
                               " Where SID=" & Val(txtSID.Text) & " AND ASID=" & Request.Cookies("ASID").Value
            ExecuteQuery_Update(sqlstr)
        End If

        'For Authorize

        '''''''''''''''''''''''''''''''''''''''''     Student Entry from Enquiry
        Dim enquiryNo As String = Request.QueryString("EnquiryNo")
        If enquiryNo <> "" Then
            sqlstr = "Insert into tmpEnqStudent(EnquiryNo,SID,convesiondate) Values('" & enquiryNo & "','" & txtSID.Text & "','" & Now.Date.ToString("yyyy/MM/dd") & "')"
            ExecuteQuery_Update(sqlstr)
        End If

        Dim FinalMessage As String = ""
        If txtSID.Text = "" Then
            If chkAddmissionSlip.Checked = True Then
                GenAdmissionSlip(SID)
            End If
            FinalMessage = "Student Record Inserted..."
        Else
            If chkAddmissionSlip.Checked = True Then
                GenAdmissionSlip(txtSID.Text)
            End If
            FinalMessage = "Student Record updated..."
        End If
        Dim tempSchoolName As String = cboSchoolName.Text
        Dim tempClassName As String = cboClass.Text
        Dim tempSecName As String = cboSection.Text
        Dim tempHouse As String = cboHouse.Text

        InitControls()
        'LoadMasterInfo(71, cboSchoolName, Request.Cookies("SchoolIDs").Value)
        cboSchoolName.Text = tempSchoolName
        LoadMasterInfo(2, cboClass, tempSchoolName)
        cboClass.Text = tempClassName
        LoadClassSection(cboSchoolName.Text, cboClass.Text, cboSection)
        cboSection.Text = tempSecName
        cboHouse.Text = tempHouse

        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('" & FinalMessage & "');", True)
        Try
            Dim nvc = HttpUtility.ParseQueryString(Request.Url.Query)
            nvc.Remove("Regno")
            Dim url As String = Request.Url.AbsolutePath + "?" + nvc.ToString()
            Response.Redirect(url)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        Dim sqlstr As String = ""

        Dim FinalMessage As String = ""

        sqlstr = "update StudentBasicInfo set IsDeleted=1 Where StudentID=" & Val(txtStudentID.Text)
        ExecuteQuery_Update(sqlstr)
        FinalMessage = "Selected Registration No. removed successfully..."
        Save_Log("STUDENT DELETE")
        InitControls()
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('" & FinalMessage & "');", True)

    End Sub

    Private Sub ShowStudentRecord(ByVal SearchType As Integer, SearchVal As String)
        txtSID.Text = ""
        Dim sqlstr As String = ""
        If SearchType = 1 Then
            sqlstr = "Select * From vw_Student Where RegNo='" & SearchVal & "' AND ASID=" & Request.Cookies("ASID").Value & " AND SchoolID in (" & Request.Cookies("SchoolIDs").Value & ")"
        ElseIf SearchType = 2 Then
            sqlstr = "Select * From vw_Student Where SName='" & SearchVal & "' AND ASID=" & Request.Cookies("ASID").Value & " AND SchoolID in (" & Request.Cookies("SchoolIDs").Value & ")"
        End If
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)
        Dim TempSection As String = ""
        Dim SibStudentID As String = "0"
        'Dim TempClass As String = ""
        While myReader.Read
            txtSID.Text = myReader("SID")
            cboSchoolName.Text = myReader("SchoolName")
            txtStudentID.Text = myReader("StudentID")
            txtSRNo.Text = myReader("RegNo")
            txtFeeBookNo.Text = myReader("FeeBookNo")
            txtFormNo.Text = myReader("FormNo")
            txtClassRollNo.Text = myReader("ClassRollNo")

            txtName.Text = myReader("SName")
            Dim tmpDate As Date = Now.Date
            Try
                tmpDate = myReader("AdmissionDate")
                txtAdmissionDate.Text = tmpDate.ToString("dd/MM/yyyy")
            Catch ex As Exception

            End Try
            txtReceiptNo.Text = myReader("ReceiptNo")
            Try
                tmpDate = myReader("DOB")
            Catch ex As Exception

            End Try

            txtDOB.Text = tmpDate.ToString("dd/MM/yyyy")
            cboGender.SelectedIndex = myReader("Gender")
            cboRel.Text = myReader("RelName")
            cboCaste.Text = myReader("CasteName")
            cboCategory.Text = myReader("CategoryName")
            cboBloodGroup.Text = myReader("BGName")
            cboNationality.Text = myReader("NatName")
            cboMotherTongue.Text = myReader("languageName")
            txtFathername.Text = myReader("FName")
            txtMotherName.Text = myReader("MName")
            txtAddressFather.Text = myReader("FatherAddress")
            Try
                cbocity.Text = myReader("District")
            Catch ex As Exception
                cbocity.SelectedIndex = -1
                Dim StateID As Integer = FindMasterID(35, cboState.Text)
                LoadStateCity(StateID, cbocity)
            End Try

            cboState.Text = myReader("Statename")
            txtPincode.Text = myReader("PINCODE")
            txtPhoneResd.Text = myReader("PhoneResd")
            txtAadharNo.Text = myReader("AadharNo")
            txtMobile.Text = myReader("MobNo")
            Try
                SibStudentID = myReader("SiblingStudentID")
            Catch ex As Exception
                SibStudentID = "0"
            End Try

            'Photo
            Try
                imgPhoto.ImageUrl = myReader("PhotoPath")
                '"~\photos\" &
            Catch ex As Exception
                imgPhoto.ImageUrl = ""
            End Try


            'Academic Detail
            LoadMasterInfo(2, cboClass, cboSchoolName.Text)
            cboClass.Text = myReader("ClassName")
            Try
                lblSecStrength.Text = GetSectionStrength(cboSchoolName.Text, cboClass.Text, Request.Cookies("ASID").Value)
            Catch ex As Exception

            End Try
            LoadClassSection(cboSchoolName.Text, cboClass.Text, cboSection)
            cboSection.Text = myReader("SecName")
            LoadClassSubSection(cboSchoolName.Text, cboClass.Text, cboSection.Text, cboSubSection)
            Try
                cboSubSection.Text = myReader("SubSecName")
            Catch ex As Exception

            End Try
            TempSection = myReader("SecName")
            cboHouse.Text = myReader("HouseName")
            'Try
            '    cboConcessionType.Text = myReader("FCTypeName")
            'Catch ex As Exception

            'End Try

            cboStatus.Text = myReader("StatusName")
            Try
                cboOnlyChild.SelectedIndex = myReader("OnlyChild")
            Catch ex As Exception

            End Try

            'Father Detail
            cboFOcc.Text = myReader("OccName")
            cboFDept.Text = myReader("DeptName")
            cboFPost.Text = myReader("DesgName")
            txtPhoneOffice.Text = myReader("PhoneOfficeFather")
            txtFEmpNo.Text = myReader("FEmpNo")
            txtEmail.Text = myReader("EmailFather")
            txtFIncome.Text = myReader("FIncome")

            'Mother Detail
            cboMOcc.Text = myReader("MOccName")
            cboMDept.Text = myReader("DeptName")
            cboMPost.Text = myReader("DesgName")
            txtPhoneOfficeM.Text = myReader("PhoneOfficeMother")
            txtMEmpNo.Text = myReader("MEmpNo")
            txtEmailM.Text = myReader("EmailMother")
            txtMIncome.Text = myReader("MIncome")

            'Gaurdian Detail
            txtGuardianName.Text = myReader("GuardianName")
            txtGuardianRelation.Text = myReader("GuardianRelation")
            txtAddressGuardian.Text = myReader("GuardianAddress")
            txtMobileGuardian.Text = myReader("GuardianMobile")


            'Last School Detail
            Try
                txtLastSchool.Text = myReader("LastSchoolName")
                txtLastSchoolAddress.Text = myReader("LastSchoolAddress")
                txtLastSchoolPercentage.Text = myReader("LastSchoolPercentage")
            Catch ex As Exception
            End Try
            Try
                cboLastboard.Text = myReader("BoardName")
            Catch ex As Exception

            End Try
            Try
                txtboardRollNO.Text = myReader("BoardRegNo")
            Catch ex As Exception
                txtboardRollNO.Text = ""
            End Try
        End While
        myReader.Close()
        If txtSID.Text <> "" And cboOnlyChild.SelectedIndex = 1 Then
            If SibStudentID = "" Then
                SqlDataSourcesiblings.SelectCommand = "SELECT * FROM vw_Student WHERE StudentID in (0)  AND ASID='" & Request.Cookies("ASID").Value & "' and SID<>" & txtSID.Text
            Else
                SqlDataSourcesiblings.SelectCommand = "SELECT * FROM vw_Student WHERE StudentID in (" & SibStudentID & ")  AND ASID='" & Request.Cookies("ASID").Value & "' and SID<>" & txtSID.Text
            End If

            gvSb.DataBind()
            gvSb.Visible = True
        Else
            gvSb.Visible = False
        End If
    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        InitControls()
    End Sub

    Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click

        Dim fp1 As String = myFile.PostedFile.FileName
        If fp1.ToString() <> "" Then
            Dim fn1 As String = fp1.Substring(fp1.LastIndexOf("\\") + 1)
            Dim sp1 As String = ""
            sp1 = Server.MapPath("Photos")
            If sp1.EndsWith("\\") = False Then
                sp1 += "\"
            End If

            sp1 &= txtSRNo.Text.Replace("/", "") & "_" & Request.Cookies("ASID").Value & ".jpg"
            myFile.PostedFile.SaveAs(sp1)
            imgPhoto.ImageUrl = "Photos\" & txtSRNo.Text.Replace("/", "") & "_" & Request.Cookies("ASID").Value & ".jpg"
        End If
        txtFeeBookNo.Focus()
    End Sub

    Protected Sub cboClass_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboClass.SelectedIndexChanged
        LoadClassSection(cboSchoolName.Text, cboClass.Text, cboSection)
        cboClass.Focus()
        'LoadClassGroups()
        lblSecStrength.Text = GetSectionStrength(cboSchoolName.Text, cboClass.Text, Request.Cookies("ASID").Value)
    End Sub


    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        LoadMasterInfo(2, cboClass)
        lblSecStrength.Text = ""
    End Sub

    'Protected Sub ImageButton2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton2.Click
    '    LoadClassSection(cboClass.Text, cboSection)
    'End Sub

    Protected Sub ImageButton3_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton3.Click
        LoadMasterInfo(5, cboRel)
        cboRel.Text = FindDefault(5)
    End Sub

    Protected Sub ImageButton4_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton4.Click
        LoadMasterInfo(6, cboCaste)
        cboCaste.Text = FindDefault(6)
    End Sub

    Protected Sub ImageButton5_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton5.Click
        LoadMasterInfo(4, cboHouse)
        cboHouse.Text = FindDefault(4)
    End Sub

    Protected Sub ImageButton9_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton9.Click
        LoadMasterInfo(7, cboFOcc)
        cboFOcc.Text = FindDefault(7)
    End Sub

    Protected Sub ImageButton13_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton13.Click
        LoadMasterInfo(7, cboMOcc)
        cboMOcc.Text = FindDefault(7)
    End Sub

    Protected Sub ImageButton10_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton10.Click
        LoadMasterInfo(8, cboFDept)
        cboFDept.Text = FindDefault(8)
    End Sub

    Protected Sub ImageButton14_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton14.Click
        LoadMasterInfo(8, cboMDept)
        cboMDept.Text = FindDefault(8)
    End Sub

    Protected Sub ImageButton11_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton11.Click
        LoadMasterInfo(9, cboFPost)
        cboFPost.Text = FindDefault(9)
    End Sub

    Protected Sub ImageButton15_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton15.Click
        LoadMasterInfo(9, cboMPost)
        cboMPost.Text = FindDefault(9)
    End Sub

    Protected Sub GridView2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView2.SelectedIndexChanged
        ShowStudentRecord(1, GridView2.SelectedRow.Cells(1).Text)
        GridView2.Visible = False
    End Sub

    Protected Sub ImageButton18_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton18.Click
        LoadMasterInfo(10, cboStatus)
        cboStatus.Text = FindDefault(10)
    End Sub

    Protected Sub ImageButton17_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton17.Click
        LoadMasterInfo(34, cboCategory)
        cboCategory.Text = FindDefault(34)
    End Sub

    Protected Sub ImageButton19_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton19.Click
        LoadMasterInfo(35, cboState)
        cboState.Text = FindDefault(35)
    End Sub

    Protected Sub ImageButton21_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton21.Click
        LoadMasterInfo(41, cboMotherTongue)
        cboMotherTongue.Text = FindDefault(41)
    End Sub

    Private Sub Save_Log(ByVal type As String)
        Dim sqlStr As String = ""
        sqlStr = "Select RegNo,Sname,ClassName,SecName from vw_Student Where SID='" & txtSID.Text & "'"

        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        Dim log1 As String = ""
        While myReader.Read
            log1 = "RegNo : " & myReader(0) & ", Name : " & myReader(1) & ", Class : " & myReader(2) & " Section : " & myReader(3)
        End While
        myReader.Close()

        log1 += " #### " & "RegNo : " & txtSRNo.Text & ", Name : " & txtName.Text & ", Class : " & cboClass.Text & " Section : " & cboSection.Text
        sqlStr = "Insert Into Event_log(logTime,EventType,Details,UserId,Visible) Values('" & System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "','" & type & "','" & log1 & "','" & Request.Cookies("UserID").Value & "','1')"
        ExecuteQuery_Update(sqlStr)
    End Sub

    Protected Sub txtDOB_TextChanged(sender As Object, e As EventArgs) Handles txtDOB.TextChanged
        Try
            If IsDate(getDateYYMMDD(txtDOB.Text)) Then
                '    Dim AgeOnDate As String = GetAgeOnDate()
                lblAgeOn.Text = "Age On " & Now.Date.ToString("dd/MM/yyyy") & ": "
                'lblAge.Text = GetAge(txtDOB.Text, Now.Date.ToString("dd/MM/yyyy"))
                lblAge.Text = GetAge(txtDOB.Text, Now.Date.ToString("dd/MM/yyyy"))
            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Enter a valid date...');", True)
            End If
        Catch ex As Exception

        End Try
        txtMobile.Focus()
    End Sub

    Private Sub GenAdmissionSlip(SID As Integer)

        Page.ClientScript.RegisterStartupScript(Me.[GetType](), "OpenWindow", "window.open('AddmissionSlip.aspx?SID=" & SID & "','_newtab');", True)

    End Sub

    Protected Sub cboSection_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSection.SelectedIndexChanged
        LoadClassSubSection(cboSchoolName.Text, cboClass.Text, cboSection.Text, cboSubSection)
        cboSection.Focus()
    End Sub
    Protected Sub btnAdmissionSlip_Click(sender As Object, e As EventArgs) Handles btnAdmissionSlip.Click
        If txtSID.Text = "" Then

            Exit Sub

        End If
        Dim Sql As String = "Select * from vw_Student where SID=" & txtSID.Text
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
            .LocalReport.ReportPath = "rptAddmissionSlip.rdlc"
            .LocalReport.DataSources.Add(rds)
        End With
        Dim params(0) As Microsoft.Reporting.WebForms.ReportParameter
        params(0) = New Microsoft.Reporting.WebForms.ReportParameter("param", "param", Visible)

        Me.ReportViewer1.LocalReport.SetParameters(params)

        ReportViewer1.Visible = True
        ReportViewer1.LocalReport.Refresh()
    End Sub

    Protected Sub btnBonafide_Click(sender As Object, e As EventArgs) Handles btnBonafide.Click
        If txtSID.Text = "" Then
            Exit Sub
        End If

        Dim sqlStr As String = "Select DOB From Student Where SID='" & txtSID.Text & "'"
        Dim rv As Date = Now.Date
        rv = ExecuteQuery_ExecuteScalar(sqlStr)

        Dim param As String = ConvertDateToWords(rv.Day.ToString("00"), rv.Month.ToString("00"), rv.Year.ToString("0000"))
        Dim Sql As String = "Select * from vw_Student where SID=" & txtSID.Text
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
            .LocalReport.ReportPath = "rptBonafied_Cert.rdlc"
            .LocalReport.DataSources.Add(rds)
        End With
        Dim params(0) As Microsoft.Reporting.WebForms.ReportParameter
        params(0) = New Microsoft.Reporting.WebForms.ReportParameter("param", param, Visible)


        Me.ReportViewer1.LocalReport.SetParameters(params)
        ReportViewer1.Visible = True

        ReportViewer1.LocalReport.Refresh()

    End Sub

    Protected Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click
        If txtSRNo.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid SR/Admin No.');", True)
            txtSRNo.Focus()
            Exit Sub
        End If

        Dim TempSRNo As String = txtSRNo.Text
        InitControls()
        txtSRNo.Text = TempSRNo

        ShowStudentRecord(1, txtSRNo.Text)
        If txtSID.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid SR/Admin No.');", True)
            txtSRNo.Focus()
            Exit Sub
        End If
        txtSRNo.Focus()
    End Sub

    Protected Sub btnNameSearch_Click1(sender As Object, e As EventArgs) Handles btnNameSearch.Click
        SqlDataSource2.SelectCommand = "SELECT RegNo, SName, ClassName, SecName FROM vw_Student WHERE ASID = " & Request.Cookies("ASID").Value & " AND SName Like '%" & txtName.Text & "%'"
        GridView2.DataBind()
        GridView2.Visible = True
    End Sub

    Protected Sub ImageButton25_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton25.Click
        LoadMasterInfo(16, cboBloodGroup)
        cboBloodGroup.Text = FindDefault(16)
    End Sub

    Protected Sub ImageButton26_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton26.Click
        LoadMasterInfo(28, cboNationality)
        cboNationality.Text = FindDefault(28)
    End Sub

    Protected Sub ImageButton2_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton2.Click
        LoadClassSection(cboSchoolName.Text, cboClass.Text, cboSection)
        cboSection.Focus()
    End Sub

    Protected Sub ImageButton24_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton24.Click
        LoadClassSubSection(cboSchoolName.Text, cboClass.Text, cboSection.Text, cboSubSection)
    End Sub
    Private Sub SendSmsToStudent()
        Dim sqlstr As String = ""
        Dim SenderName As String = txtSenderID.Text
        Dim Message As String = ""
        'sqlstr = "select * from SMSSender"
        'Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)
        'While myReader.Read
        '    SenderName = myReader("SMSSender")
        'End While
        'myReader.Close()

        sqlstr = "Select * from SMSTemplates Where TemplateCode='Student Insert'"
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)
        While myReader.Read
            Message = myReader("TemplateMessage")
        End While
        myReader.Close()

        Message = Message.Replace("(*)", txtName.Text)

        Dim lstMobile As New List(Of String)
        lstMobile.Add(txtMobile.Text)
        Dim SMSResponse As String = ""
        If Message <> "" Then
            SMSResponse = SendMySMS(SenderName, lstMobile, Message)
        End If
    End Sub

    Protected Sub cboState_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboState.SelectedIndexChanged
        Dim StateID As Integer = FindMasterID(35, cboState.Text)
        LoadStateCity(StateID, cbocity)
    End Sub

    Protected Sub cboSchoolName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSchoolName.SelectedIndexChanged
        LoadMasterInfo(2, cboClass, cboSchoolName.Text)
        cboClass.Text = ""
        cboSection.Items.Clear()
        cboSubSection.Items.Clear()
        cboSchoolName.Focus()
        Dim SchoolID = FindMasterID(71, cboSchoolName.SelectedItem.Text)
        Dim Sqlstr = "select SMSSender  from SMSSender where SchoolID = '" & SchoolID & "'"
        Dim SenderID = ""
        Dim SMSReader As SqlDataReader = ExecuteQuery_ExecuteReader(Sqlstr)
        While SMSReader.Read
            SenderID = SMSReader(0).ToString()
        End While
        txtSenderID.Text = SenderID
        SMSReader.Close()
    End Sub


End Class