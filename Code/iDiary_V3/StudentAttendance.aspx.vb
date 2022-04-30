Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Imports iDiary_V3.iDiary_Date.CLS_iDiary_Date

Partial Class StudentAttendance
    Inherits System.Web.UI.Page
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try
            If Request.Cookies("UType").Value.ToString.Contains("Admin") Or Request.Cookies("UType").Value.ToString.Contains("Attendance") Then
                'Allow
            Else
                Response.Redirect("~/AccessDenied.aspx", False)
            End If
        Catch ex As Exception
            Response.Redirect("~/Login.aspx")
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("ActiveTab") = 15
        If IsPostBack = False Then InitControls()
        'If Request.Cookies("UType").Value.ToString.Contains("Admin-1") = False Then
        '    btnSave.Enabled = False
        'End If
    End Sub

    Private Sub InitControls()
        txtDate.Text = Now.Date().ToString("dd/MM/yyyy")

        ' txtTime.Text = Now.TimeOfDay.Hours & ":" & Now.TimeOfDay.Minutes
        LoadMasterInfo(71, cboSchoolName, Request.Cookies("SchoolIDs").Value)
        Dim SchoolID = FindMasterID(71, cboSchoolName.Text)
        LoadMasterInfo(2, cboClass, cboSchoolName.Text)
        ' LoadClassSections()

        chkAll.Checked = False
        If SMSFaciltyOpted() = 0 Then
            chkSMS.Checked = False
            chkSMS.Enabled = False
        Else
            chkSMS.Checked = True
            chkSMS.Enabled = True
        End If


        chkEmail.Checked = True
        cboSection.Items.Clear()
        lblStatus.Text = ""
        Dim SenderID As String = ""

        Dim sqlStr = "Select SMSSender From SMSSender where SchoolID='" & SchoolID & "'"


        Dim SMSReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While SMSReader.Read
            SenderID = SMSReader(0)
        End While
        txtSenderID.Text = SenderID
        SMSReader.Close()
        Dim myMessage = ""
        'Load Absent Template Description
        sqlStr = "Select MessageTemplateDesc From MessageTemplates Where MessageSubject Like '%101%'"

        Dim MessageReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While MessageReader.Read
            myMessage = MessageReader(0)
        End While
        MessageReader.Close()
        txtMessage.Text = myMessage
    End Sub
    Protected Sub chkSelect_CheckedChanged(sender As Object, e As EventArgs)
        Dim MyCount As Integer = 0
        For i = 0 To gvStudent.Rows.Count - 1
            Dim chk As CheckBox = DirectCast(gvStudent.Rows(i).FindControl("chkSelect"), CheckBox)
            If chk.Checked = False Then
                ' Dim no As Integer = gvStudent.SelectedRow.DataItemIndex
                chk.Focus()
                Continue For
            End If
            MyCount += 1
            lblCount.Text = "Total Selection : " & MyCount
        Next
    End Sub
    Protected Sub cboClassSection_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboClass.SelectedIndexChanged
        LoadClassSection(cboSchoolName.Text, cboClass.Text, cboSection)

    End Sub

    Private Sub LoadStudents()

        Dim sqlStr As String = ""
        sqlStr = "SELECT SID,SName,ClassRollNo, FName, ClassName, SecName, MobNo FROM vw_Student WHERE SchoolName='" & cboSchoolName.Text & "' and ASID=" & Request.Cookies("ASID").Value & " AND StatusName='Active'"
        If cboClass.Text = "ALL" Then

        Else
            sqlStr &= " AND ClassName='" & cboClass.Text & "'"
        
            If cboSection.Text = "ALL" Then
                'Do Nothing
            Else
                sqlStr &= " AND SecName='" & cboSection.Text & "'"

            End If
            sqlStr &= " Order By ClassName, SecName,CONVERT(int,classrollno), SName"


        End If
        gvStudent.Visible = True
        SqlDataSource1.SelectCommand = sqlStr
        ' gvStudent.DataSource = SqlDataSource1
        gvStudent.DataBind()
        cboSection.Focus()

      
      

    End Sub
   


    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If cboClass.Text = "" Then
            lblStatus.Text = "Please choose a class..."
            cboClass.Focus()
            Exit Sub
        End If
        If cboSection.Text = "" Then
            lblStatus.Text = "Please choose a section..."
            cboSection.Focus()
            Exit Sub
        End If

        'Save Attendnace in Student Database
        ProcessAttendance()

        Dim myClassName As String = cboClass.Text & "-" & cboSection.Text
        Dim myDate As String = txtDate.Text
        InitControls()
        lblStatus.Text = "Attendance of Class: " & myClassName & " for " & myDate & "  saved sucessfully..."




    End Sub

    Private Sub ProcessAttendance()

        Dim sqlStr As String = ""
        Dim myMessage As String = SQLFixup(txtMessage.Text), MyMessage1 As String = ""
        Dim rv As String = ""
        Dim rvInt As Integer = 0
        Dim SenderID As String = SQLFixup(txtSenderID.Text)

        'sqlStr = "Select SMSSender From SMSSender"


        'Dim SMSReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        'While SMSReader.Read
        '    SenderID = SMSReader(0)
        'End While
        'SMSReader.Close()

        ''Load Absent Template Description
        'sqlStr = "Select MessageTemplateDesc From MessageTemplates Where MessageSubject Like '%101%'"

        'Dim MessageReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        'While MessageReader.Read
        '    myMessage = MessageReader(0)
        'End While
        'MessageReader.Close()
        Dim MyCount As Integer = 0
        Dim SentSMS As String = ""
        Dim i As Integer = 0, mobNO As String = "", SName As String = ""
        'For i = 0 To gvStudent.Rows.Count - 1
        '    Dim chk As CheckBox = DirectCast(gvStudent.Rows(i).FindControl("chkSelect"), CheckBox)
        '    If chk.Checked = False Then Continue For
        '    MyCount += 1

        '    'Email Part
        'Next



        For i = 0 To gvStudent.Rows.Count - 1

            Dim SID As Integer = gvStudent.DataKeys(i).Value
            Dim chk As CheckBox = DirectCast(gvStudent.Rows(i).FindControl("chkSelect"), CheckBox)
            sqlStr = "Select Count(*) From Attendance Where SID=" & SID & " AND AttDate='" & getDateYYMMDD(txtDate.Text) & "'"

            Try
                rvInt = ExecuteQuery_ExecuteScalar(sqlStr)
            Catch ex As Exception
                rvInt = 0
            End Try


            If rvInt > 0 Then
                sqlStr = "Update Attendance Set "
                If cboShift.SelectedItem.Text = "Morning" Then
                    sqlStr &= " IsPresentM= "
                Else
                    sqlStr &= " IsPresentE= "
                End If

                If chk.Checked = True Then
                    sqlStr &= "1"
                Else
                    sqlStr &= "0"
                End If
                sqlStr &= " Where SID=" & SID & " AND " & _
                "AttDate='" & getDateYYMMDD(txtDate.Text) & "'"

                ExecuteQuery_Update(sqlStr)
            Else
                sqlStr = "Insert into Attendance (SID,UserID,InputDate,AttDate,"
                If cboShift.SelectedItem.Text = "Morning" Then
                    sqlStr &= " IsPresentM) "
                Else
                    sqlStr &= " IsPresentE) "
                End If
                sqlStr &= "  Values (" & SID & "," & Request.Cookies("UserID").Value & ",'" & Now.Date.ToString("yyyy-MM-dd HH:mm:ss") & "','" & getDateYYMMDD(txtDate.Text) & "',"


                If chk.Checked = True Then
                    sqlStr &= "1)"
                Else
                    sqlStr &= "0)"
                End If

                ExecuteQuery_Update(sqlStr)
            End If

            If chkSMS.Checked = True Then
                If chk.Checked = True Then    'Present
                    Continue For

                Else    'Absent
                    mobNO = gvStudent.Rows(i).Cells(6).Text
                    SName = gvStudent.Rows(i).Cells(2).Text
                    SentSMS = myMessage.Replace("*", SName)
                    SendMySMS(SenderID, mobNO, SentSMS)

                    'Extract Mobile No, Email
                End If
            End If
         

        Next

        System.Threading.Thread.Sleep(500)

    End Sub

    Protected Sub chkAll_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkAll.CheckedChanged
        PerformSelectAll()
    End Sub

    Private Sub PerformSelectAll()
        Dim myVal As Boolean = False
        Dim i As Integer = 0, myCount As Integer = 0

        If chkAll.Checked = True Then myVal = True

        For i = 0 To gvStudent.Rows.Count - 1
            DirectCast(gvStudent.Rows(i).FindControl("chkSelect"), CheckBox).Checked = myVal
            myCount += 1
        Next
        
    End Sub


    Protected Sub cboSection_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSection.SelectedIndexChanged
        LoadStudents()
        chkAll.Checked = True
        PerformSelectAll()
    End Sub

    Protected Sub cboSchoolName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSchoolName.SelectedIndexChanged
        LoadMasterInfo(2, cboClass, cboSchoolName.Text)
        'cboClass.Items.Add("ALL")
        cboSchoolName.Focus()
        Dim SenderID As String = ""
        Dim SchoolID = FindMasterID(71, cboSchoolName.SelectedItem.Text)
        Dim sqlStr = "Select SMSSender From SMSSender where SchoolID='" & SchoolID & "'"


        Dim SMSReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While SMSReader.Read
            SenderID = SMSReader(0)
        End While
        txtSenderID.Text = SenderID
        SMSReader.Close()
        Dim myMessage = ""
        'Load Absent Template Description
        sqlStr = "Select MessageTemplateDesc From MessageTemplates Where MessageSubject Like '%101%'"

        Dim MessageReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While MessageReader.Read
            myMessage = MessageReader(0)
        End While
        MessageReader.Close()
        txtMessage.Text = myMessage
    End Sub

 
End Class
