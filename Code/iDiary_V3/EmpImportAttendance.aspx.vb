Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Imports System.IO

Public Class EmpImportAttendance
    Inherits System.Web.UI.Page
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try
            If Request.Cookies("UType").Value.ToString.Contains("Admin") Or Request.Cookies("UType").Value.ToString.Contains("Payroll") Then
                'Allow
            Else
                Response.Redirect("/./AccessDenied.aspx", False)
            End If
        Catch ex As Exception
            Response.Redirect("~/Login.aspx")
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then InitControls()

    End Sub
    Private Sub InitControls()
        'LoadMasterInfo(33, cboSession)
        txtFromDate.Text = Now.Date.ToString("dd-MM-yyyy")
        txtToDate.Text = Now.Date.ToString("dd-MM-yyyy")
        LoadMasterInfo(30, cboEmpCat)
        cboEmpCat.Items.Add("ALL")
        LoadMasterInfo(29, cboStatus)
        'txtToDate.Text = Now.Date.ToString("dd/MM/yyyy")
        'txtFromDate.Text = Now.Date.ToString("dd/MM/yyyy")
        FillTime()
        txtMessage.Text = GetMessage("Emp-Absent")
        Try
            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader("Select * from EmpParams")
            Dim InTime As String = "", OutTime As String = ""

            While myReader.Read
                InTime = myReader("InTime")
                OutTime = myReader("outTime")
            End While
            myReader.Close()
            ddlInHH.Text = InTime.Split(":")(0)
            ddlInMM.Text = InTime.Split(":")(1)
            ddlInMer.Text = InTime.Split(":")(2)
            ddlOutHH.Text = OutTime.Split(":")(0)
            ddlOutMM.Text = OutTime.Split(":")(1)
            ddlOutMer.Text = OutTime.Split(":")(2)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub FillTime()
        ddlInHH.Items.Add("HH")
        ddlOutHH.Items.Add("HH")
        ddlInMM.Items.Add("mm")
        ddlOutMM.Items.Add("mm")
        For i = 0 To 59
            ddlInMM.Items.Add(i.ToString("00"))
            ddlOutMM.Items.Add(i.ToString("00"))
            If i > 11 Then
                Continue For
            Else
                ddlInHH.Items.Add(i.ToString("00"))
                ddlOutHH.Items.Add(i.ToString("00"))
            End If
        Next
    End Sub
    Protected Sub btnImport_Click(sender As Object, e As EventArgs) Handles btnImport.Click
        'If Trim(cboSession.Text) = "" Then
        '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Session can't be Blank...');", True)
        '    cboSession.Focus()
        '    Exit Sub
        'End If
        'If Trim(cboEmpCat.Text) = "" Then
        '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Employee category can't be Blank...');", True)
        '    cboEmpCat.Focus()
        '    Exit Sub
        'End If
        If Trim(cboEmpCat.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Employee category can't be Blank...');", True)
            lblStatus.Text = "Employee category can't be Blank..."
            cboEmpCat.Focus()
            Exit Sub
        End If
        If myFile.PostedFile.FileName = Nothing Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Choose File From Date ..');", True)
            lblStatus.Text = "Please Choose File From Date .."
            myFile.Focus()
            Exit Sub
        End If
        If Trim(txtFromDate.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Enter From Date ..');", True)
            lblStatus.Text = "Please Enter From Date .."
            txtFromDate.Focus()
            Exit Sub
        End If
        If txtToDate.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Enter To Date ..');", True)
            lblStatus.Text = "Please Enter To Date .."
            txtToDate.Focus()
            Exit Sub
        End If
        Dim StartDate As Date = FormatDate(txtFromDate.Text)
        Dim EndDate As Date = FormatDate(txtToDate.Text)
        If StartDate > EndDate Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('To Date can not less than From Date ..');", True)
            lblStatus.Text = "To Date can not less than From Date .."
            txtToDate.Focus()
            Exit Sub
        End If
        If Not IsNumeric(ddlInHH.Text) Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Select In time Hours ..');", True)
            lblStatus.Text = "Select In time Hours .."
            ddlInHH.Focus()
            Exit Sub
        End If
        If Not IsNumeric(ddlInMM.Text) Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Select In time minutes..');", True)
            lblStatus.Text = "Select In time minutes.."
            ddlInMM.Focus()
            Exit Sub
        End If
        If Not IsNumeric(ddlOutHH.Text) Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Select out time Hours ..');", True)
            lblStatus.Text = "Select In time minutes.."
            ddlOutHH.Focus()
            Exit Sub
        End If
        If Not IsNumeric(ddlOutMM.Text) Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Select Out time minutes..');", True)
            lblStatus.Text = "Select Out time minutes.."
            ddlOutMM.Focus()
            Exit Sub
        End If
        lblStatus.Text = ""
       
        Dim statusID As Integer = FindMasterID(29, cboStatus.Text)
        Dim EmpCatID As Integer = FindMasterID(30, cboEmpCat.Text)
        Dim lstFileNames As New List(Of String)
        Dim CurrD As DateTime = StartDate
        Dim DateList As New List(Of String)
        While (CurrD <= EndDate)
            lstFileNames.Add(CurrD.ToString("ddMMyy") & "01.dat")
            CurrD = CurrD.AddDays(1)
        End While

        Dim sqlStr As String = "Select EmpID From EmployeeMaster Where Status='" & statusID & "'"
        If cboEmpCat.Text <> "ALL" Then
            sqlStr &= " AND EmpCatID=" & EmpCatID & ""
        End If
        Dim lstEmpID As New List(Of Integer)

        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            lstEmpID.Add(myReader(0))
        End While
        myReader.Close()

        Dim Count As Integer = 0
        ''''''''''from DAT files
        '  Dim fp1 As String = myFile.FileName.Split(".")(0) & "_" & Now.Date.ToString("yyyyMMddhhmm") & "." & myFile.FileName.Split(".")(1)
        Dim directoryPath As String = "", folderName As String = Now.Date.ToString("yyyyMMdd")
        '  Dim fn1 As String = fp1.Substring(fp1.LastIndexOf("\\") + 1)

        'create directory
        Dim sp1 As String = ""
        sp1 = Server.MapPath("EmpAttFile")
        If sp1.EndsWith("\\") = False Then
            sp1 += "\"
        End If
        directoryPath = sp1 & folderName
        If Not Directory.Exists(directoryPath) Then
            Directory.CreateDirectory(directoryPath)
        End If
        '  sp1 = directoryPath & "\" & fp1

        'copy files


        '   Dim SourceFile As String = System.IO.Path.GetFullPath(myFile.PostedFile.FileName)
        Dim fileName As String = System.IO.Path.GetFullPath(myFile.PostedFile.FileName)
        '   SourceFile = SourceFile.Substring(0, SourceFile.Length - fileName.Length)

      
        Dim DestFile As String = directoryPath & "\"

        For fl As Integer = 0 To myFile.PostedFiles.Count - 1
            fileName = myFile.PostedFiles.Item(fl).FileName
            If lstFileNames.Contains(fileName) = True Then
                myFile.PostedFile.SaveAs(DestFile & fileName)
                processFile(DestFile, fileName, lstEmpID)
            End If
        Next


        '       lblStatus.Text = "Attendance Data for date " & entrydate.Date.ToString("dd-MM-yyyy") & " has been Imported.."
    End Sub
    Private Function processFile(filePath As String, fileName As String, lstEmpID As List(Of Integer)) As String
        Dim sqlStr As String = "", rv As String = ""
        '  Dim fileName As String = filePath.Substring(filePath.LastIndexOf("/"), filePath.Length - filePath.LastIndexOf("/"))
        ' Dim File As String = "E:\SHARED FOLDER\" & fileName
        'Dim File As String = Server.MapPath("E:\SHARED FOLDER\" & fileName)
        Dim entrydate As Date = Now.Date
        Try
            entrydate = New Date("20" & fileName.Substring(4, 2), fileName.Substring(2, 2), fileName.Substring(0, 2))
        Catch ex As Exception

        End Try
        Dim lstBiometric As New List(Of String)
        Dim EmpASID As Integer = 0
        Try
            EmpASID = Request.Cookies("EmpASID").Value
        Catch ex As Exception
            Return rv
        End Try
        'feed default attendance
        Dim sqlDelete As String = "delete from EmployeeAttendance where EmpASID=" & EmpASID & " AND AttDate Like '" & entrydate.ToString("yyyy-MM-dd") & "%' and empid in (" & sqlStr & ")"
        ExecuteQuery_Update(sqlDelete)

        For i = 0 To lstEmpID.Count - 1
            sqlStr = "Insert into EmployeeAttendance (EmpASID,EmpID, AttDate,Att,IsLate,InTimeConfig,OutTimeConfig) Values(" & EmpASID & "," & lstEmpID.Item(i) & ",'" & entrydate.ToString("yyyy-MM-dd") & "',0,'0','" & Now.TimeOfDay.ToString() & "','" & Now.TimeOfDay.ToString() & "')"
            'sqlStr = "Insert into EmployeeAttendance (EmpASID,EmpID, AttDate,Att,InTime,OutTime,IsLate,InTimeConfig,OutTimeConfig) Values(" & EmpASID & "," & lstEmpID.Item(i) & ",'" & entrydate.ToString("yyyy-MM-dd") & "',0,'" & entrydate.ToString("yyyy-MM-dd") & "','" & entrydate.ToString("yyyy-MM-dd") & "','0','" & Now.TimeOfDay.ToString() & "','" & Now.TimeOfDay.ToString() & "')"
            ExecuteQuery_Update(sqlStr)
        Next

        Dim sr As StreamReader = New StreamReader(filePath & fileName)
        Dim machineEntry As String = ""
        Dim InStamp As String = "", entryType As Integer = 0
        Dim myLine As String = ""
        Do
            myLine = sr.ReadLine()
            machineEntry = myLine
            If IsNothing(InStamp) = True Then Exit Do
            Dim BiometricCode As String = "", timeStampIN As String = ""
            Try
                timeStampIN = machineEntry.Substring(4, 4)
                BiometricCode = machineEntry.Substring(8, 8)
            Catch ex As Exception

            End Try
            If lstBiometric.Contains(BiometricCode) Then
                entryType = 1
            Else
                lstBiometric.Add(BiometricCode)
                entryType = 0
            End If
            Dim EmpID As Integer = FindEmployeeIDfromBioMetric(BiometricCode)
            If lstEmpID.Contains(EmpID) = True Then
                If timeStampIN = "" Then
                Else
                    PushEmployeeAttendance(timeStampIN, EmpID, entrydate, entryType)
                End If
            End If

        Loop Until myLine Is Nothing
        If ckbSMS.Checked = True Then
            Dim Mob As String = "", EmpName As String = "", SentSMS As String = ""
            Dim IsLate As String = 0
            Dim Att As String = 0
            Dim SMSSender As String = ExecuteQuery_ExecuteScalar("Select SMSSender From SMSSender where IsDefault=1")
            sqlStr = "select Mob,EmpName,Att,IsLate from vw_Employee_Attendance Where AttDate='" & entrydate.ToString("yyyy/MM/dd") & "'"
            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)

            While myReader.Read
                Try
                    Mob = myReader(0)
                    EmpName = myReader(1)
                    Att = myReader("Att")
                    IsLate = myReader("IsLate")
                Catch ex As Exception

                End Try
                Try
                    If IsLate = "1" Then
                        SentSMS = txtMessageLate.Text.Replace("<*>", EmpName)
                        SentSMS = SentSMS.Replace("<**>", Now.Date.ToString("dd-MM-yyyy"))
                        SendMySMS(SMSSender, Mob, SentSMS)
                    ElseIf Att = "0" Then
                        SentSMS = txtMessage.Text.Replace("<*>", EmpName)
                        SentSMS = SentSMS.Replace("<**>", Now.Date.ToString("dd-MM-yyyy"))
                        SendMySMS(SMSSender, Mob, SentSMS)
                    End If
                Catch ex As Exception

                End Try

            End While
            myReader.Close()
        End If

        sqlStr = "Update EmpParams Set Intime='" & ddlInHH.Text & ":" & ddlInMM.Text & ":" & ddlInMer.Text & "', Outtime='" & ddlOutHH.Text & ":" & ddlOutMM.Text & ":" & ddlOutMer.Text & "'"
        ExecuteQuery_Update(sqlStr)
        sqlStr = "Update MessageTemplates Set MessageTemplateDesc='" & SQLFixup(txtMessage.Text) & "' Where MessageSubject='Emp-Absent'"
        ExecuteQuery_Update(sqlStr)
        rv = entrydate.ToString("dd-MM-yyyy")
        Return rv
    End Function
    Public Shared Function FindEmployeeIDfromBioMetric(ByVal BioMetricCode As String) As Integer
        Dim sqlStr As String = "Select max(EmpID) From vw_Employees Where BioMetricCode='" & BioMetricCode & "' "
        Dim rv As Integer = 0
        Try
            rv = ExecuteQuery_ExecuteScalar(sqlStr)
        Catch ex As Exception
            rv = 0
        End Try
        Return rv
    End Function
    Private Sub PushEmployeeAttendance(ByVal timeStamp As String, ByVal EmpID As Integer, entryDate As Date, entryType As Integer)
        'entryType >>  0-IN,1-OUT
        'Dim EmpSessionID As Integer = FindMasterID(33, cboSession.Text)
        Dim sqlStr As String = ""


        Dim inDate As DateTime = New DateTime(entryDate.Year, entryDate.Month, entryDate.Day, timeStamp.Substring(0, 2), timeStamp.Substring(2, 2), "00")
        '  Dim outDate As Date = New DateTime(entryDate.Year, entryDate.Month, entryDate.Day, timeStampOUT.Substring(0, 2), timeStampOUT.Substring(2, 2), "00")

        Dim Att As Double = 0
        Att = 1
        '  If EntryExists(EmpID, inDate) Then Exit Sub 'if already imported skip the import
        Dim IsLate As Integer = 0
        Dim inTime As String = inDate.ToString("yyyy/MM/dd")
        Dim outTime As String = inDate.ToString("yyyy/MM/dd")

        inTime += " " & ddlInHH.Text & ":" & ddlInMM.Text & ": 00 " & ddlInMer.Text
        outTime += " " & ddlOutHH.Text & ":" & ddlOutMM.Text & ": 00 " & ddlOutMer.Text


        '    Dim duration As TimeSpan = outDate - inDate
        Dim inTimeTmp As DateTime = DateTime.Parse(inTime)
        Dim outTimeTmp As DateTime = DateTime.Parse(outTime)

        If inDate.TimeOfDay <= inTimeTmp.TimeOfDay Then
            IsLate = 0
            Att = 1
            ' Emp satisfy time zone
        ElseIf inDate.TimeOfDay > inTimeTmp.TimeOfDay Then
            IsLate = 1
            Att = 1
        Else
            IsLate = 0
            Att = 0
        End If

        'sqlInsert = "Insert into EmployeeAttendance (EmpASID, EmpID, AttDate,InTime,OutTime) Values(" & EmpSessionID & "," & EmpID & ",'" & inDate.Date.ToString("yyyy/MM/dd") & "','" & inDate.TimeOfDay.ToString() & "','" & outDate.TimeOfDay.ToString() & "')"
        If entryType = 0 Then
            sqlStr = "Update EmployeeAttendance Set Att=" & Att & ",InTime='" & inDate.TimeOfDay.ToString() & "',IsLate='" & IsLate & "',InTimeConfig='" & inTimeTmp.TimeOfDay.ToString() & "',OutTimeConfig='" & outTimeTmp.TimeOfDay.ToString() & "' Where EmpASID=" & Request.Cookies("EmpASID").Value & " AND EmpID=" & EmpID & " AND AttDate='" & inDate.Date.ToString("yyyy/MM/dd") & "'"
        Else
            sqlStr = "Update EmployeeAttendance Set OutTime='" & inDate.TimeOfDay.ToString() & "' Where EmpASID=" & Request.Cookies("EmpASID").Value & " AND EmpID=" & EmpID & " AND AttDate='" & inDate.Date.ToString("yyyy/MM/dd") & "'"
        End If

        ExecuteQuery_Update(sqlStr)

    End Sub

    Private Function EntryExists(ByVal empID As Integer, ByVal entryDate As Date) As Boolean
        Dim rv As Boolean = False
        Dim sqlStr As String = "Select Count(empID) From EmployeeAttendance Where EmpID='" & empID & "' AND Attdate='" & entryDate.ToString("yyyy/MM/dd") & "'"
        Dim cnt As Integer = 0
        Try
            cnt = ExecuteQuery_ExecuteScalar(sqlStr)
        Catch ex As Exception
            cnt = 0
        End Try


        If cnt = 0 Then
            rv = False
        Else
            rv = True
        End If

        Return rv

    End Function

    Private Function FormatDate(ByVal fdate As String) As String
        Dim marker As String = ""
        If fdate.Contains("/") Then
            marker = "/"
        ElseIf fdate.Contains("-") Then
            marker = "-"
        End If
        Dim tdate() As String = fdate.Split(marker)

        Return tdate(2) & "-" & tdate(1) & "-" & tdate(0)
    End Function

    Protected Sub btnProcess_Click(sender As Object, e As EventArgs) Handles btnProcess.Click
        'If Trim(cboSession.Text) = "" Then
        '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Session can't be Blank...');", True)
        '    cboSession.Focus()
        '    Exit Sub
        'End If
        'If Trim(cboEmpCat.Text) = "" Then
        '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Employee category can't be Blank...');", True)
        '    cboEmpCat.Focus()
        '    Exit Sub
        'End If
        If Trim(cboEmpCat.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Employee category can't be Blank...');", True)
            lblStatus.Text = "Employee category can't be Blank..."
            cboEmpCat.Focus()
            Exit Sub
        End If
        'If Trim(txtFromDate.Text) = "" Then
        '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Enter From Date ..');", True)
        '    lblStatus.Text = "Please Enter From Date .."
        '    txtFromDate.Focus()
        '    Exit Sub
        'End If
        'If txtToDate.Text = "" Then
        '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Enter To Date ..');", True)
        '    lblStatus.Text = "Please Enter To Date .."
        '    txtToDate.Focus()
        '    Exit Sub
        'End If
        'Dim StartDate As Date = FormatDate(txtFromDate.Text)
        'Dim EndDate As Date = FormatDate(txtToDate.Text)
        'If StartDate > EndDate Then
        '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('To Date can not less than From Date ..');", True)
        '    lblStatus.Text = "To Date can not less than From Date .."
        '    txtToDate.Focus()
        '    Exit Sub
        'End If
        If Not IsNumeric(ddlInHH.Text) Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Select In time Hours ..');", True)
            lblStatus.Text = "Select In time Hours .."
            ddlInHH.Focus()
            Exit Sub
        End If
        If Not IsNumeric(ddlInMM.Text) Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Select In time minutes..');", True)
            lblStatus.Text = "Select In time minutes.."
            ddlInMM.Focus()
            Exit Sub
        End If
        If Not IsNumeric(ddlOutHH.Text) Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Select out time Hours ..');", True)
            lblStatus.Text = "Select In time minutes.."
            ddlOutHH.Focus()
            Exit Sub
        End If
        If Not IsNumeric(ddlOutMM.Text) Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Select Out time minutes..');", True)
            lblStatus.Text = "Select Out time minutes.."
            ddlOutMM.Focus()
            Exit Sub
        End If
        lblStatus.Text = ""
        '''''''''''''''process



        Dim statusID As Integer = FindMasterID(29, cboStatus.Text)
        Dim EmpCatID As Integer = FindMasterID(30, cboEmpCat.Text)


        Dim sqlStr As String = "Select EmpID From EmployeeMaster Where Status='" & statusID & "'"
        If cboEmpCat.Text <> "ALL" Then
            sqlStr &= " AND EmpCatID=" & EmpCatID & ""
        End If
        Dim lstEmpID As New List(Of Integer)
        Dim inTime As String = ""
        Dim outTime As String = ""
        If ddlInMer.Text = "AM" Then
            inTime = ddlInHH.Text & ":" & ddlInMM.Text
        Else
            inTime = (CInt(ddlInHH.Text) + 12) & ":" & ddlInMM.Text
        End If
        If ddlOutMer.Text = "AM" Then
            outTime = ddlOutHH.Text & ":" & ddlOutMM.Text
        Else
            outTime = (CInt(ddlOutHH.Text) + 12) & ":" & ddlOutMM.Text
        End If


        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            lstEmpID.Add(myReader(0))
        End While
        myReader.Close()

        For i = 0 To lstEmpID.Count - 1
            '   Dim AttStatus As Integer = ProcessEmployeeAttendance(lstEmpID.Item(i))

            'sqlStr = "Update employeeattendance set Att=1 where Intime<= '" & inTime & "' AND OutTime>= '" & outTime & "' AND Attdate between '" & StartDate.ToString("yyyy/MM/dd") & "' AND '" & EndDate.ToString("yyyy/MM/dd") & "' AND EmpID=" & lstEmpID.Item(i)


            ExecuteQuery_Update(sqlStr)

            'sqlStr = "Update employeeattendance set Att=0 where Att is NULL and EmpID=" & lstEmpID.Item(i)
            '
            '
            'ExecuteQuery_Update(SqlStr)

        Next


        lblStatus.Text = "Attendance has been Processed.."
    End Sub

    Private Function ProcessEmployeeAttendance(ByVal empID As Integer) As Integer
        Dim rv As Integer = 0

        Return rv
    End Function
End Class