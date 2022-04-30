Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary

Public Class GVs
    Public Shared lstSID As New List(Of String)
End Class

Public Class SendSMS
    Inherits System.Web.UI.Page
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try
            If Request.Cookies("UType").Value.ToString.Contains("Admin") Or Request.Cookies("UType").Value.ToString.Contains("SMS") Then
                'Allow
            Else
                Response.Redirect("~/AccessDenied.aspx", False)
            End If
        Catch ex As Exception
            Response.Redirect("~/Login.aspx")
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            InitControls()
        End If
        'If Request.Cookies("UType").Value.ToString.Contains("Admin-1") = False Or Request.Cookies("UType").Value.ToString.Contains("SMS-1") = False Then
        '    btnSend.Enabled = False
        'End If
        '''''''''''''''''''''''
        ''''''''''''''''''''''' DONT ALLOW SORTING OF GRID ITEMS, ELSE USE OTHER MANNER FOR SID
    End Sub

    Private Sub InitControls()

        rblSmsReceiver.SelectedIndex = 0
        PanelStudent.Visible = True
        PanelEmployee.Visible = False
        PanelIndividual.Visible = False
        LoadMasterInfo(19, cboFrom)

        LoadMasterInfo(71, cboSchoolName, Request.Cookies("SchoolIDs").Value)

        LoadMasterInfo(2, cboClass, cboSchoolName.Text)
        cboFrom.Text = FindDefault(67)

        cboClass.Items.Add("ALL")
        cboSection.Items.Clear()
        LoadMasterInfo(29, cboEmpStatus)
        '     cboEmpStatus.Items.Add("ALL")
        LoadMasterInfo(30, cboEmpCat)
        cboEmpStatus.Items.Add("ALL")
        gvStudent.DataBind()
        gvEmployee.DataBind()

        LoadSMSTemplate(cboMessageTemplate)
        txtMessage.Text = ""

        If SMSFaciltyOpted() = 0 Then
            btnSend.Visible = False
        Else
            btnSend.Visible = True
        End If
        Dim TotalSMS As Integer = Val(TotalSMSAvailable())
        lblTotalSMS.Text = "Total Cradits: " & TotalSMS
        If TotalSMS < 5000 Then
        End If

        lblStatus.Text = ""
        cboFrom.Focus()
    End Sub


    Protected Sub btnSend_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSend.Click

        If cboFrom.Text = "" Then
            lblStatus.Text = "Invalid Sender..."
            cboFrom.Focus()
            Exit Sub
        End If

        If txtMessage.Text.Length <= 0 Then
            lblStatus.Text = "Invalid Message..."
            txtMessage.Focus()
            Exit Sub
        End If
        Dim MobNo As String = "", Email As String = "", SMSResponse As String = ""
        Dim i As Integer = 0, MyCount As Integer = 0
        Dim lstMobile As New List(Of String)
        Dim lstselecteID As New List(Of String)

        If rblSmsReceiver.SelectedIndex = 0 Then
            For i = 0 To gvStudent.Rows.Count - 1
                Dim chk As CheckBox = DirectCast(gvStudent.Rows(i).FindControl("chkSelect"), CheckBox)
                If chk.Checked = False Then Continue For
                MyCount += 1
                MobNo &= gvStudent.Rows(i).Cells(6).Text & ","
                lstMobile.Add(gvStudent.Rows(i).Cells(6).Text)
                lstselecteID.Add(gvStudent.DataKeys(i).Value)
                'Email Part
            Next
        ElseIf rblSmsReceiver.SelectedIndex = 1 Then
            For i = 0 To gvEmployee.Rows.Count - 1
                Dim chk As CheckBox = DirectCast(gvEmployee.Rows(i).FindControl("chkSelectEmp"), CheckBox)
                If chk.Checked = False Then Continue For
                MyCount += 1
                MobNo &= gvEmployee.Rows(i).Cells(5).Text & ","
                lstMobile.Add(gvEmployee.Rows(i).Cells(5).Text)
                lstselecteID.Add(gvEmployee.DataKeys(i).Value)
                'Email Part
            Next
        ElseIf rblSmsReceiver.SelectedIndex = 2 Then
            txtMobNo.Text = txtMobNo.Text.Replace(vbLf, "")
            Dim tmpLst() As String = txtMobNo.Text.Split(vbNewLine)
            Dim mob As String = ""
            Try
                txtMobNoInvalid.Text = ""
                For i = 0 To tmpLst.Count - 1
                    lstselecteID.Add(0)
                    mob = Trim(tmpLst(i))
                    If Trim(mob).Length = 10 And IsNumeric(Trim(mob)) Then
                        lstMobile.Add(mob)
                        txtMobNoInvalid.Text &= mob & vbNewLine
                    Else
                        'lblStatusMsg.Text = "Invalid Mobile No. "&  at index " & i + 1
                        txtMobNoInvalid.Text &= mob & " ----INVALID " & vbNewLine
                    End If

                Next
                If txtMobNoInvalid.Text = "" Then
                    txtMobNoInvalid.Visible = False
                    Exit Sub
                Else
                    txtMobNoInvalid.Visible = True
                End If
            Catch ex As Exception
                lstMobile.Add(txtMobNo.Text)
            End Try

        End If
        Dim isUnicode As Boolean = False
        If Trim(cboMessageType.Text) = "Unicode" Then
            isUnicode = True
        Else
            isUnicode = False
        End If
        Dim TotalSMS As Integer = Val(TotalSMSAvailable())
        If MyCount > TotalSMS Then
            lblStatus.Text = "Insufficiant Cradits..."
            gvStudent.Focus()
            Exit Sub
        End If
        
        If MyCount <= 0 And rblSmsReceiver.SelectedIndex < 2 Then
            lblStatus.Text = "Select atleast one recepient to continue..."
            gvStudent.Focus()
            Exit Sub
        Else
            'MobNo = MobNo.Substring(0, MobNo.Length - 1), isUnicode
            SMSResponse = SendMySMS(cboFrom.Text, lstMobile, txtMessage.Text)

            '   Dim userID As Integer = FindEmployeeID(Request.Cookies("UID").Value)

            'SaveMessagetoDB(lstMobile, cboFrom.Text, lstselecteID, rblSmsReceiver.SelectedIndex, txtMessage.Text, Request.Cookies("UserID").Value, cboFrom.Text)
            InitControls()
            lblStatus.Text = "SMS Sent Successfully..."
            chkCheckAll.Checked = False
        End If
    End Sub

    Protected Sub cboMessageTemplate_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboMessageTemplate.SelectedIndexChanged
        txtMessage.Text = FindTemplateMessage(cboMessageTemplate.Text)
        txtMessage.Focus()
    End Sub

    Protected Sub chkCheckAll_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkCheckAll.CheckedChanged

        Dim myVal As Boolean = False
        Dim i As Integer = 0, myCount As Integer = 0

        If chkCheckAll.Checked = True Then myVal = True


        If rblSmsReceiver.SelectedIndex = 0 Then
            For i = 0 To gvStudent.Rows.Count - 1
                DirectCast(gvStudent.Rows(i).FindControl("chkSelect"), CheckBox).Checked = myVal
                myCount += 1
            Next
        Else
            For i = 0 To gvEmployee.Rows.Count - 1
                DirectCast(gvEmployee.Rows(i).FindControl("chkSelectEmp"), CheckBox).Checked = myVal
                myCount += 1
            Next
        End If



    End Sub

    Protected Sub cboClass_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboClass.SelectedIndexChanged
        If cboClass.Text = "ALL" Then
            cboSection.Items.Clear()
            cboSection.Items.Add("ALL")
        Else
            LoadClassSection(cboSchoolName.Text, cboClass.Text, cboSection)
            cboSection.Items.Add("ALL")
        End If
        
        gvStudent.DataBind()
        cboClass.Focus()
    End Sub

    Protected Sub cboSection_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSection.SelectedIndexChanged

    End Sub


    Protected Sub chkSelect_CheckedChanged(sender As Object, e As EventArgs)
        Dim MyCount As Integer = 0
        For i = 0 To gvStudent.Rows.Count - 1
            Dim chk As CheckBox = DirectCast(gvStudent.Rows(i).FindControl("chkSelect"), CheckBox)
            If chk.Checked = False Then Continue For
            MyCount += 1
            lblCount.Text = "Total Selection : " & MyCount
        Next
    End Sub
    Protected Sub chkSelectEmp_CheckedChanged(sender As Object, e As EventArgs)
        Dim MyCount As Integer = 0
        For i = 0 To gvEmployee.Rows.Count - 1
            Dim chk As CheckBox = DirectCast(gvEmployee.Rows(i).FindControl("chkSelectEmp"), CheckBox)
            If chk.Checked = False Then Continue For
            MyCount += 1
            lblCount.Text = "Total Selection : " & MyCount
        Next
    End Sub


    Protected Sub rblSmsReceiver_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rblSmsReceiver.SelectedIndexChanged
        If rblSmsReceiver.SelectedIndex = 0 Then
            PanelStudent.Visible = True
            PanelIndividual.Visible = False
            PanelEmployee.Visible = False
            gvEmployee.Visible = False
            gvStudent.Visible = True
            chkCheckAll.Visible = True
            lblCount.Text = ""
            txtMobNo.Text = ""
            txtMobNoInvalid.Text = ""
        ElseIf rblSmsReceiver.SelectedIndex = 1 Then
            PanelStudent.Visible = False
            PanelIndividual.Visible = False
            PanelEmployee.Visible = True
            gvEmployee.Visible = True
            gvStudent.Visible = False
            chkCheckAll.Visible = True
            lblCount.Text = ""
            txtMobNo.Text = ""
            txtMobNoInvalid.Text = ""
        ElseIf rblSmsReceiver.SelectedIndex = 2 Then
            PanelStudent.Visible = False
            PanelIndividual.Visible = True
            PanelEmployee.Visible = False
            gvEmployee.Visible = False
            gvStudent.Visible = False
            chkCheckAll.Visible = False
            lblCount.Text = ""
            txtMobNo.Text = ""
            txtMobNoInvalid.Text = ""
        End If
    End Sub

    Protected Sub btnStudent_Click(sender As Object, e As EventArgs) Handles btnStudent.Click
        'Dim myConnStr As String = System.Configuration.ConfigurationManager.ConnectionStrings("iDiaryConnectionString").ToString
        'Dim myConn As New SqlConnection(myConnStr)
        'myConn.Open()

        'Dim myCommand As New SqlCommand
        Dim sqlStr As String = ""
        Dim sqlstrSID As String = "SELECT SID FROM vw_Student WHERE SchoolName='" & cboSchoolName.Text & "' and ASID = '" & Request.Cookies("ASID").Value & "' AND ClassName = '" & cboClass.Text & "' AND SecName = '" & cboSection.Text & "' AND StatusName='Active' "
        sqlStr = "SELECT SID,SName,ClassRollNO, FName, ClassName, SecName, MobNo FROM vw_Student WHERE ASID=" & Request.Cookies("ASID").Value & " AND StatusName='Active'  and  SchoolName='" & cboSchoolName.Text & "'"
        sqlstrSID = "SELECT SID FROM vw_Student WHERE ASID=" & Request.Cookies("ASID").Value & " AND StatusName='Active'  and  SchoolName='" & cboSchoolName.Text & "'"
        If cboClass.Text = "ALL" Then
           
        Else
            sqlStr &= " AND ClassName='" & cboClass.Text & "'"
            sqlstrSID &= " AND ClassName='" & cboClass.Text & "'"
            If cboSection.Text = "ALL" Then
                'Do Nothing
            Else
                sqlStr &= " AND SecName='" & cboSection.Text & "'"
                sqlstrSID &= " AND SecName='" & cboSection.Text & "'"
            End If
            sqlStr &= " Order By ClassName, SecName, CONVERT(int,classrollno),SName"
            sqlstrSID &= " Order By ClassName, SecName, CONVERT(int,classrollno),SName"

        End If
        gvStudent.Visible = True
        SqlDataSource1.SelectCommand = sqlStr
        ' gvStudent.DataSource = SqlDataSource1
        gvStudent.DataBind()
        cboSection.Focus()

        'myCommand.Connection = myConn
        'myCommand.CommandText = sqlstrSID
        'Dim SIDreader As SqlDataReader = myCommand.ExecuteReader
        'While SIDreader.Read
        '    GVs.lstSID.Add(SIDreader("SID"))
        'End While
        'SIDreader.Close()
        'myConn.Close()
    End Sub

    Protected Sub btnEmployee_Click(sender As Object, e As EventArgs) Handles btnEmployee.Click
        'Dim myConnStr As String = System.Configuration.ConfigurationManager.ConnectionStrings("iDiaryConnectionString").ToString
        'Dim myConn As New SqlConnection(myConnStr)
        'myConn.Open()

        'Dim myCommand As New SqlCommand
        Dim sqlStr As String = ""
        Dim sqlstrID As String = ""
        If cboEmpCat.Text = "ALL" Then
            sqlStr = "SELECT EMPID,EmpName, EmpCode, DeptName, DesgName, Mob FROM vw_Employees WHERE (StatusName = '" & cboEmpStatus.Text & "') "
            sqlstrID = "SELECT EmpID FROM vw_Employees WHERE (StatusName = '" & cboEmpStatus.Text & "') "
        Else
            sqlStr = "SELECT EMPID,EmpName, EmpCode, DeptName, DesgName, Mob FROM vw_Employees WHERE (StatusName = '" & cboEmpStatus.Text & "') AND (EmpCatName = '" & cboEmpCat.Text & "')"
            sqlstrID = "Select EmpID FROM vw_Employees WHERE (StatusName = '" & cboEmpStatus.Text & "') AND (EmpCatName = '" & cboEmpCat.Text & "')"
        End If
        gvEmployee.Visible = True

        SqlDataSource2.SelectCommand = sqlStr
        'gvEmployee.DataSource = SqlDataSource2
        gvEmployee.DataBind()

        'myCommand.Connection = myConn
        'myCommand.CommandText = sqlstrID
        'Dim SIDreader As SqlDataReader = myCommand.ExecuteReader
        'While SIDreader.Read
        '    GVs.lstSID.Add(SIDreader("EmpID"))
        'End While
        'SIDreader.Close()
        'myConn.Close()
    End Sub

    Protected Sub cboSchoolName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSchoolName.SelectedIndexChanged
        LoadMasterInfo(2, cboClass, cboSchoolName.Text)
        cboClass.Items.Add("ALL")
        cboSchoolName.Focus()
        Dim SchoolID = FindMasterID(71, cboSchoolName.SelectedItem.Text)
        Dim Sqlst = "select SMSSender from SMSSender where SchoolID = '" & SchoolID & "' "
        Dim SenderID = ""

        Dim SMSReader As SqlDataReader = ExecuteQuery_ExecuteReader(Sqlst)
        While SMSReader.Read
            SenderID = SMSReader(0)
        End While
        cboFrom.SelectedItem.Text = SenderID
        SMSReader.Close()


    End Sub
End Class