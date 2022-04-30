Imports System.Data.SqlClient
Imports iDiary_V3.iDiary_Security.CLS_iDiary_Security
Imports iDiary_V3.iDiary.CLS_idiary

Public Class CreateUsers
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try
            If Request.Cookies("UType").Value.ToString.Contains("Admin") Then
                'Allow
            Else
                Response.Redirect("/./AccessDenied.aspx", False)
            End If
        Catch ex As Exception
            Response.Redirect("~/Login.aspx")
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            InitControls()
        Else
            If ViewState("myTable") = True Then
                CreateTable()
            End If
        End If
        If Request.Cookies("UType").Value.ToString.Contains("Admin-1") = False Then
            btnSave.Enabled = False
            btnUpdate.Enabled = False
        End If
    End Sub

    Private Sub InitControls()
        txtUserName.Text = ""
        txtLoginID.Text = ""
        txtPass.Text = ""
        txtRePass.Text = ""
        'LoadGroups(chkGroups)
        CreateTable()
        LoadSchool(chkSchool)
        btnSave.Visible = False
        btnUpdate.Visible = False
        txtLoginID.Focus()
        lblEmpID.Text = ""
    End Sub
    Private Sub CreateTable()
        myTable.Rows.Clear()
        Dim tr1 As New TableRow

        Dim td10 As New TableCell
        td10.Text = "<B>Group Name</B>"
        td10.HorizontalAlign = HorizontalAlign.Left
        tr1.Cells.Add(td10)

        Dim td11 As New TableCell
        td11.Text = "<B>Read</B>"
        td11.HorizontalAlign = HorizontalAlign.Center
        tr1.Cells.Add(td11)

        Dim td12 As New TableCell
        td12.Text = "<B>Write</B>"
        td12.HorizontalAlign = HorizontalAlign.Center
        tr1.Cells.Add(td12)
        myTable.Rows.Add(tr1)

        Dim sqlStr As String = ""

        
        If Request.Cookies("UType").Value.ToString.Contains("Executive") Then
            sqlStr = "Select GroupName From Groups where IsUsed=1 order by DisplayOrder"
        Else
            sqlStr = "Select GroupName From Groups Where GroupName<>'Executive' and IsUsed=1 order by DisplayOrder"
        End If

        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        Dim myTxtBoxNumber As Integer = 1
        While myReader.Read
            Dim trx As New TableRow

            'Dim tdx0 As New TableCell
            'tdx0.Text = myTxtBoxNumber
            'tdx0.HorizontalAlign = HorizontalAlign.Center
            'trx.Cells.Add(tdx0)

            Dim txtG As New CheckBox()
            txtG.Text = myReader(0)
            txtG.ID = "txtG" & myTxtBoxNumber
            'txtR.Width = 40
            txtG.TabIndex = -1
            Dim tdx1 As New TableCell
            tdx1.Controls.Add(txtG)
            txtG.AutoPostBack = True
            AddHandler txtG.CheckedChanged, AddressOf GroupCheckedChange
            tdx1.HorizontalAlign = HorizontalAlign.Left
            'If GradeMarks > 1 Then tdx3.Enabled = False
            trx.Cells.Add(tdx1)

            Dim txtR As New CheckBox()
            txtR.ID = "txtR" & myTxtBoxNumber
            'txtR.Width = 40
            txtR.TabIndex = -1
            Dim tdx3 As New TableCell
            tdx3.Controls.Add(txtR)
            txtR.AutoPostBack = True
            AddHandler txtR.CheckedChanged, AddressOf ReadCheckedChange
            tdx3.HorizontalAlign = HorizontalAlign.Center
            'If GradeMarks > 1 Then tdx3.Enabled = False
            trx.Cells.Add(tdx3)

            Dim txtW As New CheckBox()
            txtW.ID = "txtW" & myTxtBoxNumber
            'txtW.Width = 40
            txtW.TabIndex = -1
            Dim tdx4 As New TableCell
            tdx4.Controls.Add(txtW)
            txtW.AutoPostBack = True
            AddHandler txtW.CheckedChanged, AddressOf WriteCheckedChange
            tdx4.HorizontalAlign = HorizontalAlign.Center
            'If GradeMarks > 1 Then tdx3.Enabled = False
            trx.Cells.Add(tdx4)
           
            myTable.Rows.Add(trx)

            myTxtBoxNumber += 1
        End While
        myReader.Close()

        'Dim SubjectID As Integer = 0
        'SubjectID = FindSubjectID(cboSubject.Text, cboSubjectType.Text)

        'Dim i As Integer = 0
        If txtLoginID.Text <> "" Then

            For i = 1 To myTable.Rows.Count - 1

                Dim myGroupName As String = CType(myTable.FindControl("txtG" & i), CheckBox).Text

                sqlStr = "Select R,W From vwUserGroups Where LoginID='" & txtLoginID.Text & "' and GroupName='" & myGroupName & "'"
                
                

                Dim myReader1 As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
                While myReader1.Read
                    CType(myTable.FindControl("txtG" & i), CheckBox).Checked = 1
                    Try
                        CType(myTable.FindControl("txtR" & i), CheckBox).Checked = myReader1(0)
                    Catch ex As Exception

                    End Try
                    Try
                        CType(myTable.FindControl("txtW" & i), CheckBox).Checked = myReader1(1)
                    Catch ex As Exception

                    End Try

                End While
                myReader1.Close()
            Next
        End If
        
        

        myTable.EnableViewState = True
        ViewState("myTable") = True
    End Sub
    Private Sub GroupCheckedChange(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim cbo As CheckBox
        cbo = CType(sender, CheckBox)
        If cbo.Checked = False Then
            Dim txtRID As String = cbo.ID.Substring(4, cbo.ID.Length - 4)
            If txtRID <> "" Then
                CType(myTable.FindControl("txtR" & Convert.ToInt32(txtRID)), CheckBox).Checked = False
                CType(myTable.FindControl("txtW" & Convert.ToInt32(txtRID)), CheckBox).Checked = False
            End If
        End If
    End Sub
    Private Sub WriteCheckedChange(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim cbo As CheckBox
        cbo = CType(sender, CheckBox)
        If cbo.Checked = True Then
            Dim txtRID As String = cbo.ID.Substring(4, cbo.ID.Length - 4)
            If txtRID <> "" Then
                CType(myTable.FindControl("txtR" & Convert.ToInt32(txtRID)), CheckBox).Checked = True
                CType(myTable.FindControl("txtG" & Convert.ToInt32(txtRID)), CheckBox).Checked = True
            End If
        End If
    End Sub
    Private Sub ReadCheckedChange(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim cbo As CheckBox
        cbo = CType(sender, CheckBox)
        Dim txtRID As String = cbo.ID.Substring(4, cbo.ID.Length - 4)
        If cbo.Checked = True Then
            If txtRID <> "" Then
                'CType(myTable.FindControl("txtR" & Convert.ToInt32(txtRID)), CheckBox).Checked = True
                CType(myTable.FindControl("txtG" & Convert.ToInt32(txtRID)), CheckBox).Checked = True
            End If
        End If
        If CType(myTable.FindControl("txtW" & Convert.ToInt32(txtRID)), CheckBox).Checked = True Then
            CType(myTable.FindControl("txtR" & Convert.ToInt32(txtRID)), CheckBox).Checked = True
        End If
    End Sub
    Private Function CheckReadWrite() As Boolean
        Dim CheckG As Integer = 0
        Dim CheckRW As Integer = 0
        Dim TempGroup As String = ""
        For i = 1 To myTable.Rows.Count - 1
            If CType(myTable.FindControl("txtG" & i), CheckBox).Checked = True Then
                CheckG = 1
                If CType(myTable.FindControl("txtR" & i), CheckBox).Checked = False And CType(myTable.FindControl("txtW" & i), CheckBox).Checked = False Then
                    CheckRW = i
                    TempGroup = CType(myTable.FindControl("txtG" & i), CheckBox).Text
                End If
            End If
        Next
        
        If CheckG = 0 Then
            lblStatus.Text = "Select Atleast one Group"
        End If
        If CheckRW > 0 Then
            lblStatus.Text = "Select Atleast one Read/Write Permission for " & TempGroup
            Try
                CType(myTable.FindControl("txtG" & CheckRW), CheckBox).Focus()
            Catch ex As Exception

            End Try
        End If
        If CheckG = 0 Or CheckRW > 0 Then
            Return False
        Else
            Return True
        End If
    End Function
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        If CheckLoginAvailability(txtLoginID.Text) >= 1 Then
            lblStatus.Text = "LoginID Already in Use..."
            txtLoginID.Focus()
            Exit Sub
        End If
        If CheckReadWrite() = False Then
            Exit Sub
        End If

        If txtPass.Text <> txtRePass.Text Or txtPass.Text = "" Then
            lblStatus.Text = "Password mismatch..."
            txtPass.Text = ""
            txtRePass.Text = ""
            txtPass.Focus()
            Exit Sub
        End If

        Dim SchoolList As String = ""
        For k = 0 To chkSchool.Items.Count - 1
            If chkSchool.Items(k).Selected = True Then
                SchoolList += FindMasterID(71, chkSchool.Items(k).Text) & ","
            End If
        Next
        If SchoolList = "" Then
            lblStatus.Text = "Please Select atleast one School"
            'txtDispOrder.Focus()
            Exit Sub
        Else
            SchoolList = SchoolList.Substring(0, SchoolList.Length - 1)
        End If

        Dim i As Integer = 0
        Dim sqlStr As String = ""
        Dim EMPID As Integer = lblEmpID.Text
        'FindEmployeeIDfromCode(txtLoginID.Text)
        sqlStr = "Insert into Users(loginID,loginPass,UserName,UserCreatedBy,EMPID,ASID,SchoolIDs) Values(" & _
        "'" & txtLoginID.Text & "'," & _
        "'" & txtPass.Text & "'," & _
        "'" & txtUserName.Text & "'," & Request.Cookies("UserID").Value & ",'" & EMPID & "','" & Request.Cookies("ASID").Value & "','" & SchoolList & "')"

        ExecuteQuery_Update(sqlStr)

        SaveGroups()
        'For k = 0 To chkSchool.Items.Count - 1
        '    chkSchool.Items(k).Selected = False
        'Next
        'InitControls()
    End Sub

    Private Sub SaveGroups()
        Dim sqlStr As String = ""
        
        Dim UserID As Integer = 0

        sqlStr = "Select Max(UserID) From Users Where LoginID='" & txtLoginID.Text & "'"
        
        
        UserID = ExecuteQuery_ExecuteScalar(SqlStr)

        'For i = 0 To chkGroups.Items.Count - 1
        For i = 1 To myTable.Rows.Count - 1
            Dim GroupName As String = CType(myTable.FindControl("txtG" & i), CheckBox).Text
            Dim R As Integer = 0
            Dim W As Integer = 0
            If CType(myTable.FindControl("txtR" & i), CheckBox).Checked = True Then
                R = 1
            End If
            If CType(myTable.FindControl("txtW" & i), CheckBox).Checked = True Then
                W = 1
            End If
            'Next
            Dim GroupID As Integer = 0
            sqlStr = "Select Max(GroupID) From Groups Where GroupName='" & GroupName & "'"
            
            
            GroupID = ExecuteQuery_ExecuteScalar(SqlStr)

            sqlStr = "Delete From UserGroupMapping Where UserID=" & UserID & " AND GroupID=" & GroupID
            
            
            ExecuteQuery_Update(SqlStr)

            If CType(myTable.FindControl("txtG" & i), CheckBox).Checked = True Then

                sqlStr = "Insert into UserGroupMapping Values(" & UserID & "," & GroupID & ",1," & R & "," & W & ")"
                
                
                ExecuteQuery_Update(SqlStr)

            End If
        Next

        System.Threading.Thread.Sleep(500)
        
        

        Dim tempLoginID As String = txtLoginID.Text
        InitControls()
        lblStatus.Text = "New User (" & tempLoginID & ") Created"
    End Sub

    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        'If txtPass.Text <> "" Then
        If CheckReadWrite() = False Then
            Exit Sub
        End If

        If txtPass.Text <> txtRePass.Text Or txtPass.Text = "" Then
            lblStatus.Text = "Password mismatch..."
            txtPass.Text = ""
            txtRePass.Text = ""
            txtPass.Focus()
            Exit Sub
        End If
        Dim SchoolList As String = ""
        For k = 0 To chkSchool.Items.Count - 1
            If chkSchool.Items(k).Selected = True Then
                SchoolList += FindMasterID(71, chkSchool.Items(k).Text) & ","
            End If
        Next
        If SchoolList = "" Then
            lblStatus.Text = "Please Select atleast one School"
            'txtDispOrder.Focus()
            Exit Sub
        Else
            SchoolList = SchoolList.Substring(0, SchoolList.Length - 1)
        End If
       
        
       

        Dim sqlStr As String = ""
        
        Dim UserID As Integer = 0

        sqlStr = "Update Users Set LoginPass='" & txtPass.Text & "',loginID='" & txtLoginID.Text & "' ,SchoolIDs='" & SchoolList & "' Where EmpID='" & Val(lblEmpID.Text) & "'"
        
        
        ExecuteQuery_Update(SqlStr)
        'End If
        Dim tempLoginID As String = txtLoginID.Text
        SaveGroups()
        InitControls()
        lblStatus.Text = "Permission for " & tempLoginID & " Updated"

    End Sub

    Protected Sub btnShow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnShow.Click
        showUser(1)
    End Sub

    Private Sub showUser(ByVal type As Integer)
       
        type = 0
        Dim sqlStr As String = ""
        Dim myCount As Integer = 0
        
        sqlStr = "Select * From vwUserGroups Where LoginID='" & txtLoginID.Text & "'"
        
        Dim SchoolIDs As String = ""
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            lblEmpID.Text = myReader("EmpID")
            txtUserName.Text = myReader("UserName")
            SchoolIDs = myReader("SchoolIDs")
            'chkGroups.Items.FindByText(myReader("GroupName")).Selected = True
            myCount += 1
        End While
        CreateTable()
        myReader.Close()

        If SchoolIDs <> "" Then
            Dim SchoolListtmp() As String
            SchoolListtmp = SchoolIDs.Split(",")

            For k = 0 To chkSchool.Items.Count - 1
                chkSchool.Items(k).Selected = False
            Next
            For k = 0 To chkSchool.Items.Count - 1
                For j = 0 To SchoolListtmp.Count - 1
                    If FindMasterID(71, chkSchool.Items(k).Text) = SchoolListtmp(j) Then
                        chkSchool.Items(k).Selected = True
                    End If
                Next
            Next
        End If
        
        If myCount > 0 Then
            btnSave.Visible = False
            btnUpdate.Visible = True
        Else
            btnSave.Visible = True
            btnUpdate.Visible = False
        End If
        
        If myCount <= 0 And type = 1 Then
            InitControls()
            lblStatus.Text = "Login ID Not Found..."
        End If
    End Sub

    '--------Importent Remark-------------
    'User Table Entries - admin,admin,Administrator,0
    'Groups - 1-Admin, 2-Student, 3-Fee, 4-Library, 5-Bus, 6-Exam (all system created-0)
    'UserGroupMapping - ID of Admin from User, ID of Admin From Group, 0
    '-------------------------------------

    Protected Sub btnNameSearch_Click(sender As Object, e As EventArgs) Handles btnNameSearch.Click
        SqlDataSource1.SelectCommand = "SELECT EmpCode, EmpName, DeptName, DesgName,EmpID,SchoolName FROM vw_Employees WHERE EmpName Like '%" & txtEmpName.Text & "%'"
        GridView1.DataBind()
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged
        lblEmpID.Text = GridView1.DataKeys(0).Value
        txtLoginID.Text = GridView1.SelectedRow.Cells(2).Text
        txtUserName.Text = GridView1.SelectedRow.Cells(2).Text
        showUser(0)
    End Sub
    Protected Sub GridView2_SelectedIndexChanged1(sender As Object, e As EventArgs) Handles GridView2.SelectedIndexChanged
        Try
            lblEmpID.Text = GridView2.DataKeys(0).Value
            txtLoginID.Text = GridView2.SelectedRow.Cells(6).Text
            txtUserName.Text = GridView2.SelectedRow.Cells(2).Text
            showUser(0)
        Catch ex As Exception

        End Try

    End Sub
End Class