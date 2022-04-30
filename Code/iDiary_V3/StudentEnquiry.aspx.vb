Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Imports iDiary_V3.iDiary_Fee.CLS_iDiary_Fee
Imports System.IO
Imports Microsoft.Reporting.WebForms
Imports System.Drawing
Public Class StudentEnquiry
    Inherits System.Web.UI.Page

    Dim sqlstr As String = ""
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try
            If Request.Cookies("UType").Value.ToString.Contains("Admission") Or Request.Cookies("UType").Value.ToString.Contains("Admin") Then
                'Allow
            Else
                Response.Redirect("/./AccessDenied.aspx", False)
            End If
        Catch ex As Exception
            Response.Redirect("~/Login.aspx")
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("ActiveTab") = 1
        If CheckBoxNotification.Checked = True Then
            Panel1.Visible = True
        Else
            Panel1.Visible = False
        End If
        If Not IsPostBack Then
            InitControls()
            If Request.QueryString("type") = 0 Then
                PanelEnquiry.Visible = True
                btnUpdate.Visible = False
                CheckBoxNotification.Visible = False
                Panel1.Visible = False
            Else
                Panel1.Visible = True
                btnSubmit.Visible = False
                btnUpdate.Visible = False
                'panelEnquiryNo.Visible = True
                PanelEnquiry.Visible = True
                CheckBoxNotification.Visible = True
                Panel1.Visible = False
            End If
        End If
        If Request.Cookies("UType").Value.ToString.Contains("Student-1") = False And Request.Cookies("UType").Value.ToString.Contains("Admin-1") = False Then
            btnSubmit.Enabled = False
            btnUpdate.Enabled = False
            btnSendSMS.Enabled = False
        End If
    End Sub

    Private Sub InitControls()
        'PanelEnquiry.Visible = False
        'panelEnquiryNo.Visible = False
        LoadMasterInfo(71, cboSchoolName, Request.Cookies("SchoolIDs").Value)
        txtDoB.Text = Now.Date.ToString("dd/MM/yyyy")
        txtDate.Text = Now.Date.ToString("dd/MM/yyyy")
        txtAddress.Text = ""
        txtEmail.Text = ""
        txtEnquiry.Text = ""
        txtFname.Text = ""
        txtMname.Text = ""
        txtMobNo.Text = ""
        txtSname.Text = ""
        'txtEnquiryNo.Text = ""
        txtTime.Text = DateTime.Now.ToString("HH:mm tt")
        LoadMasterInfo(2, cboClass, cboSchoolName.Text)
        LoadMasterInfo(47, cboStatus)
        LoadMasterInfo(48, cboCategory)
        CheckBoxNotification.Checked = False
        txtFormNo.Text = ""
        lblAge.Text = ""
        lblAgeOn.Text = ""
        btnUpdate.Visible = False
        btnSubmit.Visible = True
        LoadMasterInfo(12, cboPaymentMode)
        txtFormNo.Enabled = True
        Try
            cboPaymentMode.Text = FindDefault(12)
        Catch ex As Exception

        End Try
        txtRegDate.Text = Now.Date.ToString("yyyy-MM-dd")
        CheckChqTextbox()
        txtAmount.Text = ""
        txtReceiptNo.Text = ""
        txtChqNo.Text = ""
        txtChqDate.Text = ""
        txtBankName.Text = ""
        txtBranchName.Text = ""
        txtPaymentRemarks.Text = ""
        txtID.Text = ""
        'If Request.QueryString("type") = 0 Then
        txtFormNo.Focus()
        'Else
        '    txtEnquiryNo.Focus()
        'End If
    End Sub

    Private Function getEnquiryNo() As Integer

        sqlstr = "Select MAX(EnquiryID) from StudentEnquiry"
        Dim rv As Integer = 0
        Try
            rv = ExecuteQuery_ExecuteScalar(sqlstr)
        Catch ex As Exception

        End Try

        Return rv
    End Function

    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        If Trim(cboSchoolName.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('School Name is required...');", True)
            cboSchoolName.Focus()
            Exit Sub
        End If
        If Trim(txtFormNo.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Form no. is required...');", True)
            txtFormNo.Focus()
            Exit Sub
        End If
        If Trim(txtSname.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Student Name is required...');", True)
            txtSname.Focus()
            Exit Sub
        End If
        If Trim(txtFname.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Student's Father Name is required...');", True)
            txtFname.Focus()
            Exit Sub
        End If
        If Trim(cboClass.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Class is required...');", True)
            cboClass.Focus()
            Exit Sub
        End If
        If Trim(txtMobNo.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Mobile no. is required...');", True)
            txtMobNo.Focus()
            Exit Sub
        End If
        Dim DOB As Date = Now.Date
        If Trim(txtDoB.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Date of Birth is required...');", True)
            txtDoB.Focus()
            Exit Sub
        End If
        Try
            If txtDoB.Text.Contains("/") Then
                DOB = txtDoB.Text.Split("/")(2) & "/" & txtDoB.Text.Split("/")(1) & "/" & txtDoB.Text.Split("/")(0)
            Else
                DOB = txtDoB.Text.Split("-")(2) & "/" & txtDoB.Text.Split("-")(1) & "/" & txtDoB.Text.Split("-")(0)
            End If
        Catch ex As Exception
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid DOB...');", True)
            'lblStatus.Text = "Invalid Date of Birth..."
            txtDoB.Focus()
            Exit Sub
        End Try

        Dim RegDate As Date = Now.Date
        'If Trim(txtRegDate.Text) = "" Then
        '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Registration Date is required...');", True)
        '    txtRegDate.Focus()
        '    Exit Sub
        'End If
        If txtRegDate.Text <> "" Then
            Try
                RegDate = txtRegDate.Text
                'If txtRegDate.Text.Contains("/") Then
                '    RegDate = txtRegDate.Text.Split("/")(2) & "/" & txtRegDate.Text.Split("/")(1) & "/" & txtRegDate.Text.Split("/")(0)
                'Else
                '    RegDate = txtRegDate.Text.Split("-")(2) & "/" & txtRegDate.Text.Split("-")(1) & "/" & txtRegDate.Text.Split("-")(0)
                'End If
            Catch ex As Exception
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Registration Date...');", True)
                'lblStatus.Text = "Invalid Date of Birth..."
                txtRegDate.Focus()
                Exit Sub
            End Try
        End If

        


        Dim ChqDate As Date = Now.Date
        If cboPaymentMode.Text.Contains("Cash") = False Then
            If Trim(txtChqNo.Text) = "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Enter Chq/DD No...');", True)
                'lblStatus.Text = "Please Enter Chq/DD/Transaction No..."
                txtChqNo.Focus()
                Exit Sub
            End If
            Try
                Try
                    ChqDate = txtChqDate.Text
                    'If txtChqDate.Text.Contains("/") Then
                    '    ChqDate = txtChqDate.Text.Split("/")(2) & "/" & txtChqDate.Text.Split("/")(1) & "/" & txtChqDate.Text.Split("/")(0)
                    'Else
                    '    ChqDate = txtChqDate.Text.Split("-")(2) & "/" & txtChqDate.Text.Split("-")(1) & "/" & txtChqDate.Text.Split("-")(0)
                    'End If
                Catch ex As Exception
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Chq/DD/Transaction Date...');", True)
                    'lblStatus.Text = "Invalid Date of Birth..."
                    txtChqDate.Focus()
                    Exit Sub
                End Try
            Catch ex As Exception
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Chq/DD/Transaction Date...');", True)
                'lblStatus.Text = "Invalid Chq/DD/Transaction Date..."
                txtChqDate.Focus()
                Exit Sub
            End Try
            If Trim(txtBankName.Text) = "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Enter Bank Name...');", True)
                'lblStatus.Text = "Please Enter Bank Name..."
                txtBankName.Focus()
                Exit Sub
            End If
            If Trim(txtBranchName.Text) = "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Enter Branch Name...');", True)
                'lblStatus.Text = "Please Enter Branch Name..."
                txtBranchName.Focus()
                Exit Sub
            End If
        End If
        'If Trim(txtEmail.Text) = "" Then
        '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Email ID is required...');", True)
        '    txtEmail.Focus()
        '    Exit Sub
        'End If
        'If Trim(txtAddress.Text) = "" Then
        '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Address is required...');", True)
        '    txtAddress.Focus()
        '    Exit Sub
        'End If
        'If Trim(txtEnquiry.Text) = "" Then
        '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Enquiry text is required...');", True)
        '    txtEnquiry.Focus()
        '    Exit Sub
        'End If
        Dim PaymentModeID As Integer = FindMasterID(12, cboPaymentMode.Text)
        Dim classID As Integer = FindMasterID(2, cboClass.Text)
        Dim SchoolID As Integer = FindMasterID(71, cboSchoolName.Text)
        Dim enqStatusID As Integer = FindMasterID(47, cboStatus.Text)
        Dim enqTypeID As Integer = FindMasterID(48, cboCategory.Text)

        sqlstr = "select Count(EnquiryNo) from StudentEnquiry where EnquiryNo = '" & txtFormNo.Text & "'"
        Dim tmp As Integer = ExecuteQuery_ExecuteScalar(sqlstr)
        '  Dim EnquiryNo As String = "EN" & Now.Year & "/" & tmp + 1
        If tmp > 0 Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Form no. already exists...');", True)
            txtFormNo.Focus()
            Exit Sub
        End If

        sqlstr = "Insert Into StudentEnquiry (EnquiryNo,SchoolID,Sname,Fname,Mname,DoB,Gender,ClassID,MobNo,Email,address,Enquiry,EnquiryStatusID,enquiryYear,EnquiryTypeID,ASID, RegistrationAmount, RegistrationDate, PaymentModeID, RegistrationRecieptNo, ChqDDNo, ChqDDDate, BankName, BranchName, RegistrationRemarks) Values(" & _
        "'" & txtFormNo.Text & "'," & _
               "'" & SchoolID & "'," & _
        "'" & txtSname.Text & "'," & _
        "'" & txtFname.Text & "'," & _
        "'" & txtMname.Text & "'," & _
        "'" & DOB.ToString("yyyy/MM/dd") & "'," & _
        "'" & cboGender.SelectedIndex & "'," & _
"'" & classID & "'," & _
        "'" & txtMobNo.Text & "'," & _
        "'" & txtEmail.Text & "'," & _
        "'" & txtAddress.Text & "'," & _
        "'" & txtEnquiry.Text & "'," & _
        "'" & enqStatusID & "'," & _
        "'" & Now.Date.ToString("yyyy-MM-dd") & "'," & _
        "'" & enqTypeID & "'," & _
        "'" & Request.Cookies("ASID").Value & "'," & _
         "'" & Val(txtAmount.Text) & "',"
        If txtRegDate.Text <> "" Then
            sqlstr += "'" & RegDate.ToString("yyyy/MM/dd") & "',"
        Else
            sqlstr += "null,"
        End If
        sqlstr += "'" & PaymentModeID & "'," & _
        "'" & txtReceiptNo.Text & "'," & _
        "'" & txtChqNo.Text & "',"
        If txtChqDate.Text <> "" And txtChqDate.Enabled = True Then
            sqlstr += "'" & ChqDate.ToString("yyyy/MM/dd") & "',"
        Else
            sqlstr += "null,"
        End If
        sqlstr += "'" & txtBankName.Text & "'," & _
    "'" & txtBranchName.Text & "'," & _
    "'" & txtPaymentRemarks.Text & "')"

        ExecuteQuery_Update(sqlstr)
        Save_Log("ENQUIRY INSERT")
        'sqlstr = "select enquiryNo from StudentEnquiry Where enquiryID=(select max(enquiryID) from StudentEnquiry)"
        'Dim enquiryNumber As String = ExecuteQuery_ExecuteScalar(sqlstr)
        If chkprint.Checked = True Then
            GenFeeReceipt(txtFormNo.Text, Val(txtAmount.Text))
        End If

        Dim msg As String = "Form No : " & txtFormNo.Text & " saved successfully."
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('" & msg & "');", True)
        InitControls()
    End Sub

    Private Sub GenFeeReceipt(EnquiryNo As String, Amount As Double)
       
        Dim SchoolName As String = ""
        Dim Address As String = ""
       
        Dim ClassGroupID As Integer = FindClassGroupID(cboClass.Text)
        SchoolName = FindSchoolDetails(1, ClassGroupID)
        Address = FindSchoolDetails(2, ClassGroupID)
       

        Dim totalWord As String = GetNumberAsWords(Amount)
        Dim className As String = "Test"
        Dim lr As New LocalReport()
        Dim Sql = "Select * From vw_StudentEnquiry where EnquiryNo = '" & EnquiryNo & "' and ASID='" & Request.Cookies("ASID").Value & "'"
        Dim ds As New DataSet
        ds = ExecuteQuery_DataSet(Sql, "tbl")
        Dim rd As New ReportDataSource("dsReceipt", ds.Tables(0))
        lr.DataSources.Add(rd)

        lr.ReportPath = "Report/rptRegistrationFeeReceipt.rdlc"

        'lr.ReportPath = ReportViewer1.LocalReport.ReportPath
        Dim params(3) As Microsoft.Reporting.WebForms.ReportParameter
        params(0) = New Microsoft.Reporting.WebForms.ReportParameter("TotalWord", totalWord, Visible)
        params(1) = New Microsoft.Reporting.WebForms.ReportParameter("Installment", "xyz", Visible)
        params(2) = New Microsoft.Reporting.WebForms.ReportParameter("SchoolName", SchoolName, Visible)
        params(3) = New Microsoft.Reporting.WebForms.ReportParameter("SchoolAddress", Address, Visible)

        'Me.ReportViewer1.LocalReport.SetParameters(params)
        lr.SetParameters(params)
        'Dim reportType As String = id
        Dim mimeType As String
        Dim encoding As String
        Dim fileNameExtension As String
        Dim deviceInfo As String = (Convert.ToString("<DeviceInfo>" + "  <OutputFormat>") & "pdf") + "</OutputFormat>" + "  <PageWidth>8.27in</PageWidth>" + "  <PageHeight>4.0685in</PageHeight>" + "  <MarginTop>0.2in</MarginTop>" + "  <MarginLeft>.5in</MarginLeft>" + "  <MarginRight>.2in</MarginRight>" + "  <MarginBottom>0.2in</MarginBottom>" + "</DeviceInfo>"
        'Dim deviceInfo As String = (Convert.ToString("<DeviceInfo>" + "  <OutputFormat>") & "pdf") + "</OutputFormat>" + "  <PageWidth>8.27in</PageWidth>" + "  <PageHeight>11.67in</PageHeight>" + "  <MarginTop>0.2in</MarginTop>" + "  <MarginLeft>.5in</MarginLeft>" + "  <MarginRight>0in</MarginRight>" + "  <MarginBottom>0.2in</MarginBottom>" + "</DeviceInfo>"

        deviceInfo = (Convert.ToString("<DeviceInfo>" + "  <OutputFormat>") & "pdf") + "</OutputFormat>" + "  <PageWidth>8.27in</PageWidth>" + "  <PageHeight>11.67in</PageHeight>" + "  <MarginTop>0.1in</MarginTop>" + "  <MarginLeft>0.1in</MarginLeft>" + "  <MarginRight>0in</MarginRight>" + "  <MarginBottom>0.1in</MarginBottom>" + "</DeviceInfo>"


        Dim warnings As Warning()
        Dim streams As String()
        Dim renderedBytes As Byte()

        renderedBytes = lr.Render("pdf", deviceInfo, mimeType, encoding, fileNameExtension, streams, warnings)
        Using fs As FileStream = System.IO.File.Create(Server.MapPath("~") & "Registrationrcpt.pdf")
            fs.Write(renderedBytes, 0, renderedBytes.Length)
        End Using
        Dim url As String = "Registrationrcpt.pdf"
        'ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "OpenWindow", "window.open('" & url & "','_newtab');", True)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "OpenWindow", "window.open('" & url & "','_newtab');", True)
        'Return File(renderedBytes, mimeType)
    End Sub
    Protected Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        If Trim(cboSchoolName.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('School Name is required...');", True)
            cboSchoolName.Focus()
            Exit Sub
        End If
        If Trim(txtFormNo.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Form no. is required...');", True)
            txtFormNo.Focus()
            Exit Sub
        End If
        If Trim(txtSname.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Student Name is required...');", True)
            txtSname.Focus()
            Exit Sub
        End If
        If Trim(txtFname.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Student's Father Name is required...');", True)
            txtFname.Focus()
            Exit Sub
        End If
        If Trim(cboClass.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Class is required...');", True)
            cboClass.Focus()
            Exit Sub
        End If
        If Trim(txtMobNo.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Mobile no. is required...');", True)
            txtMobNo.Focus()
            Exit Sub
        End If
        Dim DOB As Date = Now.Date
        If Trim(txtDoB.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Date of Birth is required...');", True)
            txtDoB.Focus()
            Exit Sub
        End If
        Try
            If txtDoB.Text.Contains("/") Then
                DOB = txtDoB.Text.Split("/")(2) & "/" & txtDoB.Text.Split("/")(1) & "/" & txtDoB.Text.Split("/")(0)
            Else
                DOB = txtDoB.Text.Split("-")(2) & "/" & txtDoB.Text.Split("-")(1) & "/" & txtDoB.Text.Split("-")(0)
            End If
        Catch ex As Exception
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid DOB...');", True)
            'lblStatus.Text = "Invalid Date of Birth..."
            txtDoB.Focus()
            Exit Sub
        End Try
        Dim RegDate As Date = Now.Date
        'If Trim(txtRegDate.Text) = "" Then
        '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Registration Date is required...');", True)
        '    txtRegDate.Focus()
        '    Exit Sub
        'End If
        If txtRegDate.Text <> "" Then
            Try
                RegDate = txtRegDate.Text
                'If txtRegDate.Text.Contains("/") Then
                '    RegDate = txtRegDate.Text.Split("/")(2) & "/" & txtRegDate.Text.Split("/")(1) & "/" & txtRegDate.Text.Split("/")(0)
                'Else
                '    RegDate = txtRegDate.Text.Split("-")(2) & "/" & txtRegDate.Text.Split("-")(1) & "/" & txtRegDate.Text.Split("-")(0)
                'End If
            Catch ex As Exception
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Registration Date...');", True)
                'lblStatus.Text = "Invalid Date of Birth..."
                txtRegDate.Focus()
                Exit Sub
            End Try
        End If


        If IsNumeric(txtAmount.Text) = False Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Enter Valid Amount...');", True)
            'lblStatus.Text = "Please Enter Branch Name..."
            txtAmount.Focus()
            Exit Sub
        End If

        Dim ChqDate As Date = Now.Date
        If cboPaymentMode.Text.Contains("Cash") = False Then
            If Trim(txtChqNo.Text) = "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Enter Chq/DD No...');", True)
                'lblStatus.Text = "Please Enter Chq/DD/Transaction No..."
                txtChqNo.Focus()
                Exit Sub
            End If
            Try
                Try
                    ChqDate = txtChqDate.Text
                    'If txtChqDate.Text.Contains("/") Then
                    '    ChqDate = txtChqDate.Text.Split("/")(2) & "/" & txtChqDate.Text.Split("/")(1) & "/" & txtChqDate.Text.Split("/")(0)
                    'Else
                    '    ChqDate = txtChqDate.Text.Split("-")(2) & "/" & txtChqDate.Text.Split("-")(1) & "/" & txtChqDate.Text.Split("-")(0)
                    'End If
                Catch ex As Exception
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Chq/DD/Transaction Date...');", True)
                    'lblStatus.Text = "Invalid Date of Birth..."
                    txtChqDate.Focus()
                    Exit Sub
                End Try
            Catch ex As Exception
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Chq/DD/Transaction Date...');", True)
                'lblStatus.Text = "Invalid Chq/DD/Transaction Date..."
                txtChqDate.Focus()
                Exit Sub
            End Try
            If Trim(txtBankName.Text) = "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Enter Bank Name...');", True)
                'lblStatus.Text = "Please Enter Bank Name..."
                txtBankName.Focus()
                Exit Sub
            End If
            If Trim(txtBranchName.Text) = "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Enter Branch Name...');", True)
                'lblStatus.Text = "Please Enter Branch Name..."
                txtBranchName.Focus()
                Exit Sub
            End If
        End If
        If Trim(txtReceiptNo.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Enter Receipt No...');", True)
            'lblStatus.Text = "Please Enter Branch Name..."
            txtReceiptNo.Focus()
            Exit Sub
        End If
        Dim PaymentModeID As Integer = FindMasterID(12, cboPaymentMode.Text)
        Dim SchoolID As Integer = FindMasterID(71, cboSchoolName.Text)
        Dim classID As Integer = FindMasterID(2, cboClass.Text)
        Dim enqStatusID As Integer = FindMasterID(47, cboStatus.Text)
        Dim enqTypeID As Integer = FindMasterID(48, cboCategory.Text)

        sqlstr = "Update StudentEnquiry Set " & _
        "EnquiryNo='" & txtFormNo.Text & "'," & _
           "SchoolID='" & SchoolID & "'," & _
        "Sname='" & txtSname.Text & "'," & _
        "Fname='" & txtFname.Text & "'," & _
        "Mname='" & txtMname.Text & "'," & _
        "DoB='" & DOB.ToString("yyyy/MM/dd") & "'," & _
        "Gender='" & cboGender.SelectedIndex & "'," & _
        "classID='" & classID & "'," & _
        "MobNo='" & txtMobNo.Text & "'," & _
        "Email='" & txtEmail.Text & "'," & _
        "address='" & txtAddress.Text & "'," & _
        "Enquiry='" & txtEnquiry.Text & "'," & _
        "EnquiryStatusID='" & enqStatusID & "'," & _
        "enquiryYear='" & Now.Date.ToString("yyyy-MM-dd") & "'," & _
        "EnquiryTypeID='" & enqTypeID & "',"
        '"ASID='" & Request.Cookies("ASID").Value & "'," & _
        sqlstr += "RegistrationAmount='" & Val(txtAmount.Text) & "',"
        If txtRegDate.Text <> "" Then
            sqlstr += "RegistrationDate='" & RegDate.ToString("yyyy/MM/dd") & "',"
        Else
            sqlstr += "RegistrationDate=null,"
        End If
        sqlstr += "PaymentModeID='" & PaymentModeID & "'," & _
"RegistrationRecieptNo='" & txtReceiptNo.Text & "'," & _
"ChqDDNo='" & txtChqNo.Text & "',"
        If txtChqDate.Text <> "" And txtChqDate.Enabled = True Then
            sqlstr += "ChqDDDate='" & ChqDate.ToString("yyyy/MM/dd") & "',"
        Else
            sqlstr += "ChqDDDate=null,"
        End If
        sqlstr += "BankName='" & txtBankName.Text & "'," & _
    "BranchName='" & txtBranchName.Text & "'," & _
    "RegistrationRemarks='" & txtPaymentRemarks.Text & "' where EnquiryID=" & txtID.Text

       
        ExecuteQuery_Update(sqlstr)
        Save_Log("ENQUIRY UPDATE")
        If chkprint.Checked = True Then
            GenFeeReceipt(txtFormNo.Text, Val(txtAmount.Text))
        End If
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Enquiry Updated Successfully.');", True)
        InitControls()
    End Sub

    Protected Sub CheckBoxNotification_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxNotification.CheckedChanged
        If CheckBoxNotification.Checked = True Then
            Panel1.Visible = True
        Else
            Panel1.Visible = False
        End If
    End Sub

    Protected Sub btnSendSMS_Click(sender As Object, e As EventArgs) Handles btnSendSMS.Click
        If Trim(txtDate.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Date is required...');", True)
            txtDate.Focus()
            Exit Sub
        End If
        If Trim(txtTime.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Time is required...');", True)
            txtTime.Focus()
            Exit Sub
        End If
        Dim SenderID As String = ""
        sqlstr = "Select SMSSender From SMSSender"
        Dim SMSReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)
        While SMSReader.Read
            SenderID = SMSReader(0)
        End While
        SMSReader.Close()
        'If cboStatus.Text = "Called for Interview" Then
        '    sqlstr = "Select TemplateMessage from SMSTemplates where TemplateCode LIKE 'Enquiry101'"
        'ElseIf cboStatus.Text = "Accepted" Then
        '    sqlstr = "Select TemplateMessage from SMSTemplates where TemplateCode LIKE 'Enquiry102'"
        'Else
        '    Exit Sub
        'End If
        Dim sms As String = txtMessage.Text
        sms = sms.Replace("(*)", txtSname.Text)
        sms = sms.Replace("dd/MM/yyyy", txtDate.Text)
        sms = sms.Replace("HH:mm", txtTime.Text)

        SendMySMS(SenderID, txtMobNo.Text, sms)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('SMS has been Sent...');", True)
    End Sub

    Protected Sub btnEnquiry_Click(sender As Object, e As EventArgs) Handles btnEnquiry.Click
        Dim Count As Integer = 0
        Dim ChqDate As Date = Now.Date
        Dim RegDate As Date = Now.Date
        Dim sqlstr As String = ""
        sqlstr = "Select * from vw_StudentEnquiry where enquiryNo='" & txtFormNo.Text & "' and ASID=" & Request.Cookies("ASID").Value
        Dim myreader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)
        While myreader.Read
            Count = 1
            Try
                txtID.Text = myreader("enquiryID")
            Catch ex As Exception

            End Try
            Try
                txtFormNo.Text = myreader("enquiryNo")
            Catch ex As Exception

            End Try
            txtSname.Text = myreader("sname")
            txtFname.Text = myreader("fname")
            txtMname.Text = myreader("mname")
            Try
                Dim tmpDate As Date = myreader("DoB")
                txtDoB.Text = tmpDate.ToString("dd/MM/yyyy")
            Catch ex As Exception
                txtDoB.Text = Today.Date.ToString("dd/MM/yyyy")
            End Try
            Try
                cboGender.SelectedIndex = myreader("Gender")
            Catch ex As Exception

            End Try
            cboSchoolName.Text = myreader("SchoolName")
            LoadMasterInfo(2, cboClass, cboSchoolName.Text)
            cboClass.Text = myreader("className")
            txtMobNo.Text = myreader("mobNo")
            txtEmail.Text = myreader("email")
            txtAddress.Text = myreader("address")
            txtEnquiry.Text = myreader("EnQuiry")
            cboStatus.Text = myreader("statusName")
            cboCategory.Text = myreader("TypeName")
            'Payment Details
            Try
                cboPaymentMode.Text = myreader("PMName")
            Catch ex As Exception

            End Try
            Try
                txtAmount.Text = myreader("RegistrationAmount")
            Catch ex As Exception

            End Try
            Try
                RegDate = myreader("RegistrationDate")
                txtRegDate.Text = RegDate.ToString("yyyy-MM-dd")
            Catch ex As Exception

            End Try
            Try
                txtReceiptNo.Text = myreader("RegistrationRecieptNo")
            Catch ex As Exception

            End Try
            If cboPaymentMode.Text <> "Cash" Then
                Try
                    txtChqNo.Text = myreader("ChqDDNo")
                Catch ex As Exception

                End Try
                Try
                    ChqDate = myreader("ChqDDDate")
                    txtChqDate.Text = ChqDate.ToString("yyyy-MM-dd")
                Catch ex As Exception

                End Try
                Try
                    txtBankName.Text = myreader("BankName")
                Catch ex As Exception

                End Try
                Try
                    txtBranchName.Text = myreader("BranchName")
                Catch ex As Exception

                End Try
            End If

            Try
                txtPaymentRemarks.Text = myreader("RegistrationRemarks")
            Catch ex As Exception

            End Try
        End While
        myreader.Close()
        If Count = 1 Then
            txtFormNo.Enabled = False
            CheckChqTextbox()
            sqlstr = ""
            'If cboStatus.Text = "Called" Then
            '    sqlstr = "Select TemplateMessage from SMSTemplates where TemplateCode LIKE 'Enquiry101'"
            'ElseIf cboStatus.Text = "Selected" Then
            '    sqlstr = "Select TemplateMessage from SMSTemplates where TemplateCode LIKE 'Enquiry102'"
            'End If
            'If sqlstr <> "" Then
            '    Panel1.Visible = True
            '    Dim sms As String = ExecuteQuery_ExecuteScalar(sqlstr)
            '    txtMessage.Text = sms
            'End If
            btnUpdate.Visible = True
            btnSubmit.Visible = False
        Else
            txtFormNo.Enabled = True
            Panel1.Visible = False
            btnUpdate.Visible = False
            btnSubmit.Visible = True
        End If
        Dim AgeOnDate As String = GetAgeOnDate()
        lblAgeOn.Text = "Age On " & AgeOnDate
        'lblAge.Text = GetAge(txtDOB.Text, Now.Date.ToString("dd/MM/yyyy"))
        lblAge.Text = GetAge(txtDoB.Text, AgeOnDate)
    End Sub
    Private Sub CheckChqTextbox()
        If cboPaymentMode.Text.Contains("Cash") = False Then
            txtChqNo.Enabled = True
            txtChqDate.Enabled = True
            txtBankName.Enabled = True
            txtBranchName.Enabled = True
        Else
            txtChqNo.Enabled = False
            txtChqDate.Enabled = False
            txtBankName.Enabled = False
            txtBranchName.Enabled = False
        End If
    End Sub
    Protected Sub cboStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboStatus.SelectedIndexChanged
        If cboStatus.Text = "Selected" Or cboStatus.Text = "Called" Then
            Panel1.Visible = True
            If cboStatus.Text = "Called" Then
                sqlstr = "Select TemplateMessage from SMSTemplates where TemplateCode LIKE 'Enquiry101'"
            ElseIf cboStatus.Text = "Selected" Then
                sqlstr = "Select TemplateMessage from SMSTemplates where TemplateCode LIKE 'Enquiry102'"
            End If
            Dim sms As String = ExecuteQuery_ExecuteScalar(sqlstr)
            txtMessage.Text = sms
        Else
            Panel1.Visible = False
        End If
        cboStatus.Focus()
    End Sub

    Protected Sub txtDoB_TextChanged(sender As Object, e As EventArgs) Handles txtDoB.TextChanged
        Try
            If IsDate(getDateYYMMDD(txtDoB.Text)) Then
                Dim AgeOnDate As String = GetAgeOnDate()
                lblAgeOn.Text = "Age On " & AgeOnDate
                'lblAge.Text = GetAge(txtDOB.Text, Now.Date.ToString("dd/MM/yyyy"))
                lblAge.Text = GetAge(txtDoB.Text, AgeOnDate)
            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Enter a valid date...');", True)
            End If
        Catch ex As Exception

        End Try
        txtDoB.Focus()
    End Sub
    Public Shared Function getDateYYMMDD(ByVal dtString As String) As String
        ' input is in dd MM yyyy
        Dim dt As Date = Now.Date
        Try
            dt = New Date(dtString.Substring(6, 4), dtString.Substring(3, 2), dtString.Substring(0, 2))
        Catch ex As Exception

        End Try
        Return dt.ToString("yyyy-MM-dd")
    End Function
    Private Sub Save_Log(ByVal type As String)

        Dim log1 As String = ""
        log1 = " #### " & type & ": " & "FormNo : " & txtFormNo.Text & ", Name : " & txtSname.Text & ", Class : " & cboClass.Text
        sqlstr = "Insert Into Event_log(logTime,EventType,Details,UserId,Visible) Values('" & System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "','" & type & "','" & log1 & "','" & Request.Cookies("UserID").Value & "','1')"
        ExecuteQuery_Update(sqlstr)
    End Sub

    Protected Sub cboSchoolName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSchoolName.SelectedIndexChanged
        LoadMasterInfo(2, cboClass, cboSchoolName.Text)
        cboSchoolName.Focus()
    End Sub
End Class